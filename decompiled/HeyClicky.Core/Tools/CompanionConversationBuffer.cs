using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009E RID: 158
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionConversationBuffer
  {
    // Token: 0x060009FD RID: 2557 RVA: 0x00372534 File Offset: 0x00372534
    [NullableContext(2)]
    public CompanionConversationBuffer(Func<DateTimeOffset> clock = null)
    {
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = CompanionConversationBuffer.<>c.<>9__10_0) == null)
      {
        func = (CompanionConversationBuffer.<>c.<>9__10_0 = () => DateTimeOffset.UtcNow);
      }
      this.now = func;
    }

    // Token: 0x17000232 RID: 562
    // (get) Token: 0x060009FE RID: 2558 RVA: 0x00372594 File Offset: 0x00372594
    public int Count
    {
      get
      {
        object obj = this.gate;
        int count;
        lock (obj)
        {
          count = this.turns.Count;
        }
        return count;
      }
    }

    // Token: 0x060009FF RID: 2559 RVA: 0x003725DC File Offset: 0x003725DC
    [NullableContext(2)]
    public void Append(string userMessage, string assistantReply)
    {
      string text = ((userMessage != null) ? userMessage.Trim() : null) ?? "";
      if (text.Length == 0)
      {
        return;
      }
      string text2 = ((assistantReply != null) ? assistantReply.Trim() : null) ?? "";
      object obj = this.gate;
      lock (obj)
      {
        this.turns.Add(new CompanionConversationBuffer.Turn((text.Length > 250) ? text.Substring(0, 250) : text, (text2.Length > 450) ? text2.Substring(0, 450) : text2, this.now()));
        if (this.turns.Count > 10)
        {
          this.turns.RemoveRange(0, this.turns.Count - 10);
        }
      }
    }

    // Token: 0x06000A00 RID: 2560 RVA: 0x003726C8 File Offset: 0x003726C8
    public void Clear()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.turns.Clear();
        this.lastFollowUpSentAtByThreadId.Clear();
      }
    }

    // Token: 0x06000A01 RID: 2561 RVA: 0x00372718 File Offset: 0x00372718
    [return: TupleElementNames(new string[] { "User", "Assistant" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public IReadOnlyList<ValueTuple<string, string>> ConsumeForFreshLaunch(string transcript)
    {
      if (!CompanionConversationBuffer.HasDeicticReferent(transcript))
      {
        return Array.Empty<ValueTuple<string, string>>();
      }
      object obj = this.gate;
      bool flag = false;
      IReadOnlyList<ValueTuple<string, string>> readOnlyList;
      try
      {
        Monitor.Enter(obj, ref flag);
        DateTimeOffset reference = this.now();
        List<CompanionConversationBuffer.Turn> list = this.turns.Where<CompanionConversationBuffer.Turn>(delegate(CompanionConversationBuffer.Turn t)
        {
          DateTimeOffset? agentPromptConsumedAt = t.AgentPromptConsumedAt;
          return agentPromptConsumedAt == null && reference - t.CreatedAt <= CompanionConversationBuffer.MaximumTurnAgeForFreshLaunch;
        }).TakeLast<CompanionConversationBuffer.Turn>(10).ToList<CompanionConversationBuffer.Turn>();
        foreach (CompanionConversationBuffer.Turn turn in list)
        {
          turn.AgentPromptConsumedAt = new DateTimeOffset?(reference);
        }
        readOnlyList = list.Select<CompanionConversationBuffer.Turn, ValueTuple<string, string>>((CompanionConversationBuffer.Turn t) => new ValueTuple<string, string>(t.UserMessage, t.AssistantReply)).ToList<ValueTuple<string, string>>();
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj);
        }
      }
      return readOnlyList;
    }

    // Token: 0x06000A02 RID: 2562 RVA: 0x00372810 File Offset: 0x00372810
    [return: TupleElementNames(new string[] { "User", "Assistant" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public IReadOnlyList<ValueTuple<string, string>> SinceLastMessage(string threadId)
    {
      object obj = this.gate;
      bool flag = false;
      IReadOnlyList<ValueTuple<string, string>> readOnlyList;
      try
      {
        Monitor.Enter(obj, ref flag);
        DateTimeOffset dateTimeOffset = this.now();
        DateTimeOffset dateTimeOffset2;
        DateTimeOffset? lastSentAt = (this.lastFollowUpSentAtByThreadId.TryGetValue(threadId, out dateTimeOffset2) ? new DateTimeOffset?(dateTimeOffset2) : null);
        this.lastFollowUpSentAtByThreadId[threadId] = dateTimeOffset;
        DateTimeOffset earliest = dateTimeOffset - CompanionConversationBuffer.MaximumTurnAgeForFollowUp;
        if (lastSentAt != null)
        {
          DateTimeOffset valueOrDefault = lastSentAt.GetValueOrDefault();
          if (valueOrDefault > earliest)
          {
            earliest = valueOrDefault;
          }
        }
        readOnlyList = (from t in this.turns.Where<CompanionConversationBuffer.Turn>(delegate(CompanionConversationBuffer.Turn t)
          {
            if (!(t.CreatedAt > earliest))
            {
              return false;
            }
            if (lastSentAt == null)
            {
              DateTimeOffset? agentPromptConsumedAt = t.AgentPromptConsumedAt;
              return agentPromptConsumedAt == null;
            }
            return true;
          }).TakeLast<CompanionConversationBuffer.Turn>(10)
          select new ValueTuple<string, string>(t.UserMessage, t.AssistantReply)).ToList<ValueTuple<string, string>>();
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj);
        }
      }
      return readOnlyList;
    }

    // Token: 0x06000A03 RID: 2563 RVA: 0x00372920 File Offset: 0x00372920
    public static bool HasDeicticReferent(string transcript)
    {
      string text = transcript.ToLowerInvariant();
      if (CompanionConversationBuffer.PhrasalReferents.Any<string>(new Func<string, bool>(text.Contains)))
      {
        return true;
      }
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c in text + " ")
      {
        if (char.IsLetterOrDigit(c))
        {
          stringBuilder.Append(c);
        }
        else
        {
          if (stringBuilder.Length > 0 && CompanionConversationBuffer.StandaloneReferents.Contains(stringBuilder.ToString()))
          {
            return true;
          }
          stringBuilder.Clear();
        }
      }
      return false;
    }

    // Token: 0x0400047E RID: 1150
    public static readonly CompanionConversationBuffer Shared = new CompanionConversationBuffer(null);

    // Token: 0x0400047F RID: 1151
    public const int MaximumRetainedTurns = 10;

    // Token: 0x04000480 RID: 1152
    public const int MaximumTurnsPerLaunch = 10;

    // Token: 0x04000481 RID: 1153
    public static readonly TimeSpan MaximumTurnAgeForFreshLaunch = TimeSpan.FromMinutes(3L);

    // Token: 0x04000482 RID: 1154
    public static readonly TimeSpan MaximumTurnAgeForFollowUp = TimeSpan.FromMinutes(30L);

    // Token: 0x04000483 RID: 1155
    private readonly object gate = new object();

    // Token: 0x04000484 RID: 1156
    private readonly List<CompanionConversationBuffer.Turn> turns = new List<CompanionConversationBuffer.Turn>();

    // Token: 0x04000485 RID: 1157
    private readonly Dictionary<string, DateTimeOffset> lastFollowUpSentAtByThreadId = new Dictionary<string, DateTimeOffset>();

    // Token: 0x04000486 RID: 1158
    private readonly Func<DateTimeOffset> now;

    // Token: 0x04000487 RID: 1159
    private static readonly string[] PhrasalReferents = new string[]
    {
      "do it", "do that", "do this", "do them", "go ahead", "let's do it", "lets do it", "now do", "now that", "what about",
      "and that", "and this"
    };

    // Token: 0x04000488 RID: 1160
    private static readonly HashSet<string> StandaloneReferents = new HashSet<string> { "it", "that", "this", "them", "those", "these" };

    // Token: 0x0200039E RID: 926
    [Nullable(0)]
    private sealed class Turn
    {
      // Token: 0x0600298F RID: 10639 RVA: 0x00422CCC File Offset: 0x00422CCC
      public Turn(string userMessage, string assistantReply, DateTimeOffset createdAt)
      {
      }

      // Token: 0x040018E0 RID: 6368
      public readonly string UserMessage = userMessage;

      // Token: 0x040018E1 RID: 6369
      public readonly string AssistantReply = assistantReply;

      // Token: 0x040018E2 RID: 6370
      public readonly DateTimeOffset CreatedAt = createdAt;

      // Token: 0x040018E3 RID: 6371
      public DateTimeOffset? AgentPromptConsumedAt;
    }
  }
}
