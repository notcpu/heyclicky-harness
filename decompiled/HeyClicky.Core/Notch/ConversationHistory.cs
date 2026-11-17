using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000109 RID: 265
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ConversationHistory
  {
    // Token: 0x17000415 RID: 1045
    // (get) Token: 0x06001168 RID: 4456 RVA: 0x0039BB84 File Offset: 0x0039BB84
    public IReadOnlyList<ConversationMessage> Messages
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ConversationMessage> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.messages.ToList<ConversationMessage>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x17000416 RID: 1046
    // (get) Token: 0x06001169 RID: 4457 RVA: 0x0039BBCC File Offset: 0x0039BBCC
    // (set) Token: 0x0600116A RID: 4458 RVA: 0x0039BBD4 File Offset: 0x0039BBD4
    public bool HasEarlierMessages { get; private set; }

    // Token: 0x17000417 RID: 1047
    // (get) Token: 0x0600116B RID: 4459 RVA: 0x0039BBE0 File Offset: 0x0039BBE0
    // (set) Token: 0x0600116C RID: 4460 RVA: 0x0039BBE8 File Offset: 0x0039BBE8
    public bool IsLoadingNewest { get; private set; }

    // Token: 0x17000418 RID: 1048
    // (get) Token: 0x0600116D RID: 4461 RVA: 0x0039BBF4 File Offset: 0x0039BBF4
    // (set) Token: 0x0600116E RID: 4462 RVA: 0x0039BBFC File Offset: 0x0039BBFC
    public bool IsLoadingEarlier { get; private set; }

    // Token: 0x17000419 RID: 1049
    // (get) Token: 0x0600116F RID: 4463 RVA: 0x0039BC08 File Offset: 0x0039BC08
    // (set) Token: 0x06001170 RID: 4464 RVA: 0x0039BC10 File Offset: 0x0039BC10
    public bool HasLoadFailed { get; private set; }

    // Token: 0x1700041A RID: 1050
    // (get) Token: 0x06001171 RID: 4465 RVA: 0x0039BC1C File Offset: 0x0039BC1C
    // (set) Token: 0x06001172 RID: 4466 RVA: 0x0039BC24 File Offset: 0x0039BC24
    public DateTimeOffset? LastAnswerEndedAt { get; private set; }

    // Token: 0x14000025 RID: 37
    // (add) Token: 0x06001173 RID: 4467 RVA: 0x0039BC30 File Offset: 0x0039BC30
    // (remove) Token: 0x06001174 RID: 4468 RVA: 0x0039BC68 File Offset: 0x0039BC68
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06001175 RID: 4469 RVA: 0x0039BCA0 File Offset: 0x0039BCA0
    [NullableContext(2)]
    public ConversationHistory([Nullable(new byte[] { 1, 2 })] Func<string> accountId, [Nullable(new byte[] { 1, 1, 2 })] Func<long?, CancellationToken, Task<ConversationHistory.Page>> fetchPage, string cacheFolder = null, Func<DateTimeOffset> clock = null)
    {
      this.accountId = accountId;
      this.fetchPage = fetchPage;
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = ConversationHistory.<>c.<>9__41_0) == null)
      {
        func = (ConversationHistory.<>c.<>9__41_0 = () => DateTimeOffset.UtcNow);
      }
      this.clock = func;
      this.cacheFolder = cacheFolder ?? AppPaths.Combine(new string[] { "HeyClickyHistory" });
    }

    // Token: 0x06001176 RID: 4470 RVA: 0x0039BD20 File Offset: 0x0039BD20
    [return: Nullable(new byte[] { 1, 1, 2 })]
    public static Func<long?, CancellationToken, Task<ConversationHistory.Page>> WorkerFetcher(WorkerClient worker)
    {
      ConversationHistory.<>c__DisplayClass42_0 CS$<>8__locals1 = new ConversationHistory.<>c__DisplayClass42_0();
      CS$<>8__locals1.worker = worker;
      return delegate(long? beforeId, CancellationToken ct)
      {
        ConversationHistory.<>c__DisplayClass42_0.<<WorkerFetcher>b__0>d <<WorkerFetcher>b__0>d;
        <<WorkerFetcher>b__0>d.<>t__builder = AsyncTaskMethodBuilder<ConversationHistory.Page>.Create();
        <<WorkerFetcher>b__0>d.<>4__this = CS$<>8__locals1;
        <<WorkerFetcher>b__0>d.beforeId = beforeId;
        <<WorkerFetcher>b__0>d.ct = ct;
        <<WorkerFetcher>b__0>d.<>1__state = -1;
        <<WorkerFetcher>b__0>d.<>t__builder.Start<ConversationHistory.<>c__DisplayClass42_0.<<WorkerFetcher>b__0>d>(ref <<WorkerFetcher>b__0>d);
        return <<WorkerFetcher>b__0>d.<>t__builder.Task;
      };
    }

    // Token: 0x06001177 RID: 4471 RVA: 0x0039BD3C File Offset: 0x0039BD3C
    [return: Nullable(2)]
    public static ConversationHistory.Page ParsePage(JsonObject json)
    {
      JsonArray jsonArray = json["messages"] as JsonArray;
      if (jsonArray == null)
      {
        return null;
      }
      List<ConversationMessage> list = new List<ConversationMessage>();
      foreach (JsonNode jsonNode in jsonArray)
      {
        JsonObject jsonObject = jsonNode as JsonObject;
        if (jsonObject != null)
        {
          JsonNode jsonNode2 = jsonObject["id"];
          long? num = ((jsonNode2 != null) ? new long?(jsonNode2.GetValue<long>()) : null);
          if (num != null)
          {
            long valueOrDefault = num.GetValueOrDefault();
            JsonNode jsonNode3 = jsonObject["role"];
            string text = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null);
            bool flag = text == "user" || text == "assistant";
            if (flag)
            {
              JsonNode jsonNode4 = jsonObject["created_at"];
              DateTimeOffset dateTimeOffset;
              if (DateTimeOffset.TryParse((jsonNode4 != null) ? jsonNode4.GetValue<string>() : null, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
              {
                List<ConversationMessage> list2 = list;
                ConversationMessage conversationMessage = new ConversationMessage();
                conversationMessage.Id = "server-" + valueOrDefault.ToString();
                conversationMessage.ServerId = new long?(valueOrDefault);
                conversationMessage.Role = text;
                JsonNode jsonNode5 = jsonObject["content"];
                conversationMessage.Content = ((jsonNode5 != null) ? jsonNode5.GetValue<string>() : null) ?? "";
                JsonNode jsonNode6 = jsonObject["source"];
                conversationMessage.Source = ((jsonNode6 != null) ? jsonNode6.GetValue<string>() : null) ?? "voice";
                conversationMessage.CreatedAt = dateTimeOffset;
                list2.Add(conversationMessage);
              }
            }
          }
        }
      }
      IReadOnlyList<ConversationMessage> readOnlyList = list;
      JsonNode jsonNode7 = json["has_more"];
      return new ConversationHistory.Page(readOnlyList, jsonNode7 != null && jsonNode7.GetValue<bool>());
    }

    // Token: 0x1700041B RID: 1051
    // (get) Token: 0x06001178 RID: 4472 RVA: 0x0039BF08 File Offset: 0x0039BF08
    public bool IsAwaitingAnswerToSentMessage
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          ConversationMessage conversationMessage = this.messages.LastOrDefault<ConversationMessage>((ConversationMessage m) => m.IsUser && m.IsPending);
          if (conversationMessage == null || this.clock() - conversationMessage.CreatedAt >= TimeSpan.FromSeconds(90L))
          {
            flag2 = false;
          }
          else
          {
            DateTimeOffset? lastAnswerEndedAt = this.LastAnswerEndedAt;
            bool flag3;
            if (lastAnswerEndedAt != null)
            {
              DateTimeOffset valueOrDefault = lastAnswerEndedAt.GetValueOrDefault();
              flag3 = conversationMessage.CreatedAt > valueOrDefault;
            }
            else
            {
              flag3 = true;
            }
            flag2 = flag3;
          }
        }
        return flag2;
      }
    }

    // Token: 0x06001179 RID: 4473 RVA: 0x0039BFC8 File Offset: 0x0039BFC8
    public void Refresh()
    {
      ConversationHistory.<>c__DisplayClass46_0 CS$<>8__locals1 = new ConversationHistory.<>c__DisplayClass46_0();
      CS$<>8__locals1.<>4__this = this;
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        if (this.account == null || this.refreshRunning)
        {
          return;
        }
        DateTimeOffset? dateTimeOffset = this.lastRefreshStartedAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (this.clock() - valueOrDefault < TimeSpan.FromSeconds(2L))
          {
            return;
          }
        }
        this.lastRefreshStartedAt = new DateTimeOffset?(this.clock());
        this.refreshRunning = true;
        this.IsLoadingNewest = true;
        CS$<>8__locals1.requested = this.account;
      }
      this.RaiseChanged();
      Task.Run(delegate
      {
        ConversationHistory.<>c__DisplayClass46_0.<<Refresh>b__0>d <<Refresh>b__0>d;
        <<Refresh>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<Refresh>b__0>d.<>4__this = CS$<>8__locals1;
        <<Refresh>b__0>d.<>1__state = -1;
        <<Refresh>b__0>d.<>t__builder.Start<ConversationHistory.<>c__DisplayClass46_0.<<Refresh>b__0>d>(ref <<Refresh>b__0>d);
        return <<Refresh>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600117A RID: 4474 RVA: 0x0039C0B8 File Offset: 0x0039C0B8
    public void LoadEarlier()
    {
      ConversationHistory.<>c__DisplayClass47_0 CS$<>8__locals1 = new ConversationHistory.<>c__DisplayClass47_0();
      CS$<>8__locals1.<>4__this = this;
      object obj = this.gate;
      lock (obj)
      {
        if (!this.HasEarlierMessages || this.earlierRunning || this.account == null)
        {
          return;
        }
        ConversationMessage conversationMessage = this.messages.FirstOrDefault<ConversationMessage>((ConversationMessage m) => m.ServerId != null);
        long? num = ((conversationMessage != null) ? conversationMessage.ServerId : null);
        if (num == null)
        {
          return;
        }
        long valueOrDefault = num.GetValueOrDefault();
        CS$<>8__locals1.oldest = valueOrDefault;
        this.earlierRunning = true;
        this.IsLoadingEarlier = true;
        CS$<>8__locals1.requested = this.account;
      }
      this.RaiseChanged();
      Task.Run(delegate
      {
        ConversationHistory.<>c__DisplayClass47_0.<<LoadEarlier>b__0>d <<LoadEarlier>b__0>d;
        <<LoadEarlier>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<LoadEarlier>b__0>d.<>4__this = CS$<>8__locals1;
        <<LoadEarlier>b__0>d.<>1__state = -1;
        <<LoadEarlier>b__0>d.<>t__builder.Start<ConversationHistory.<>c__DisplayClass47_0.<<LoadEarlier>b__0>d>(ref <<LoadEarlier>b__0>d);
        return <<LoadEarlier>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600117B RID: 4475 RVA: 0x0039C1AC File Offset: 0x0039C1AC
    public void NoteUserSentText(string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachmentPaths = null)
    {
      IReadOnlyList<string> readOnlyList = attachmentPaths ?? Array.Empty<string>();
      string text2 = text;
      if (text2.Length == 0 && readOnlyList.Count > 0)
      {
        string text3 = "\ud83d\udcce ";
        string text4 = ", ";
        IEnumerable<string> enumerable = readOnlyList;
        Func<string, string> func;
        if ((func = ConversationHistory.<>O.<0>__GetFileName) == null)
        {
          func = (ConversationHistory.<>O.<0>__GetFileName = new Func<string, string>(Path.GetFileName));
        }
        text2 = text3 + string.Join(text4, enumerable.Select<string, string>(func));
      }
      if (text2.Length == 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        this.messages.Add(new ConversationMessage
        {
          Id = "local-" + Guid.NewGuid().ToString(),
          Role = "user",
          Content = text2,
          Source = "text",
          CreatedAt = this.clock(),
          AttachmentPaths = ((readOnlyList.Count == 0) ? null : readOnlyList.ToList<string>())
        });
      }
      this.RaiseChanged();
    }

    // Token: 0x0600117C RID: 4476 RVA: 0x0039C2D4 File Offset: 0x0039C2D4
    public void NoteUserSentStraightToAgent(string text, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachmentPaths, string agentName, bool spoken)
    {
      IReadOnlyList<string> readOnlyList = attachmentPaths ?? Array.Empty<string>();
      string text2 = text.Trim();
      if (text2.Length == 0 && readOnlyList.Count > 0)
      {
        string text3 = "\ud83d\udcce ";
        string text4 = ", ";
        IEnumerable<string> enumerable = readOnlyList;
        Func<string, string> func;
        if ((func = ConversationHistory.<>O.<0>__GetFileName) == null)
        {
          func = (ConversationHistory.<>O.<0>__GetFileName = new Func<string, string>(Path.GetFileName));
        }
        text2 = text3 + string.Join(text4, enumerable.Select<string, string>(func));
      }
      if (text2.Length == 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        this.messages.Add(new ConversationMessage
        {
          Id = "local-" + Guid.NewGuid().ToString(),
          Role = "user",
          Content = text2,
          Source = (spoken ? "voice" : "text"),
          CreatedAt = this.clock(),
          AttachmentPaths = ((readOnlyList.Count == 0) ? null : readOnlyList.ToList<string>()),
          SentToAgentName = agentName
        });
        this.SaveToDisk();
      }
      this.RaiseChanged();
    }

    // Token: 0x0600117D RID: 4477 RVA: 0x0039C418 File Offset: 0x0039C418
    public void NoteUserSpokeWords(string words)
    {
      string text = words.Trim();
      if (text.Length == 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        this.messages.Add(new ConversationMessage
        {
          Id = "local-" + Guid.NewGuid().ToString(),
          Role = "user",
          Content = text,
          Source = "voice",
          CreatedAt = this.clock()
        });
      }
      this.RaiseChanged();
    }

    // Token: 0x0600117E RID: 4478 RVA: 0x0039C4DC File Offset: 0x0039C4DC
    public unsafe void NoteSpokenLineHeard(string line)
    {
      string text = line.Trim();
      if (text.Length == 0)
      {
        return;
      }
      object obj = this.gate;
      bool flag = false;
      try
      {
        Monitor.Enter(obj, ref flag);
        this.AdoptAccount(this.accountId());
        string liveId = this.liveSpokenReplyId;
        int num = ((liveId != null) ? this.messages.FindIndex((ConversationMessage m) => m.Id == liveId) : (-1));
        if (num >= 0)
        {
          ConversationMessage conversationMessage = this.messages[num];
          IReadOnlyList<string> readOnlyList = conversationMessage.BubbleSegments ?? new <>z__ReadOnlySingleElementList<string>(conversationMessage.Content);
          int num2 = 1 + readOnlyList.Count;
          List<string> list = new List<string>(num2);
          CollectionsMarshal.SetCount<string>(list, num2);
          Span<string> span = CollectionsMarshal.AsSpan<string>(list);
          int num3 = 0;
          foreach (string text2 in readOnlyList)
          {
            *span[num3] = text2;
            num3++;
          }
          *span[num3] = text;
          List<string> list2 = list;
          List<ConversationMessage> list3 = this.messages;
          int num4 = num;
          ConversationMessage conversationMessage2 = conversationMessage.<Clone>$();
          conversationMessage2.Content = string.Join(" ", list2);
          conversationMessage2.BubbleSegments = list2;
          list3[num4] = conversationMessage2;
        }
        else
        {
          this.liveSpokenReplyId = "local-spoken-" + Guid.NewGuid().ToString();
          this.messages.Add(new ConversationMessage
          {
            Id = this.liveSpokenReplyId,
            Role = "assistant",
            Content = text,
            Source = "voice",
            CreatedAt = this.clock()
          });
        }
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj);
        }
      }
      this.RaiseChanged();
    }

    // Token: 0x0600117F RID: 4479 RVA: 0x0039C6DC File Offset: 0x0039C6DC
    public void NoteSpokenAnswerFinished()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.liveSpokenReplyId == null)
        {
          return;
        }
        this.liveSpokenReplyId = null;
        this.LastAnswerEndedAt = new DateTimeOffset?(this.clock());
      }
      this.RaiseChanged();
    }

    // Token: 0x06001180 RID: 4480 RVA: 0x0039C744 File Offset: 0x0039C744
    public void NoteAssistantReplyFinished(string text)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        this.LastAnswerEndedAt = new DateTimeOffset?(this.clock());
        string text2 = this.LiveReplyMessageIdCore();
        this.liveReplyId = null;
        List<string> list = ConversationMessage.BubbleSegmentsFrom(text);
        if (list.Count > 0)
        {
          this.messages.Add(new ConversationMessage
          {
            Id = text2,
            Role = "assistant",
            Content = string.Join(" ", list),
            Source = "text",
            CreatedAt = this.clock(),
            BubbleSegments = ((list.Count > 1) ? list : null)
          });
        }
      }
      this.RaiseChanged();
    }

    // Token: 0x06001181 RID: 4481 RVA: 0x0039C82C File Offset: 0x0039C82C
    public void NoteAssistantReport(string text)
    {
      List<string> list = ConversationMessage.BubbleSegmentsFrom(text);
      if (list.Count == 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.AdoptAccount(this.accountId());
        this.messages.Add(new ConversationMessage
        {
          Id = "local-" + Guid.NewGuid().ToString(),
          Role = "assistant",
          Content = string.Join(" ", list),
          Source = "voice",
          CreatedAt = this.clock(),
          BubbleSegments = ((list.Count > 1) ? list : null)
        });
      }
      this.RaiseChanged();
    }

    // Token: 0x06001182 RID: 4482 RVA: 0x0039C90C File Offset: 0x0039C90C
    public void NoteAnswerStopped()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.LastAnswerEndedAt = new DateTimeOffset?(this.clock());
        this.liveReplyId = null;
      }
      this.RaiseChanged();
    }

    // Token: 0x06001183 RID: 4483 RVA: 0x0039C96C File Offset: 0x0039C96C
    public string LiveReplyMessageId()
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        text = this.LiveReplyMessageIdCore();
      }
      return text;
    }

    // Token: 0x06001184 RID: 4484 RVA: 0x0039C9B0 File Offset: 0x0039C9B0
    private string LiveReplyMessageIdCore()
    {
      string text;
      if ((text = this.liveReplyId) == null)
      {
        text = (this.liveReplyId = "local-" + Guid.NewGuid().ToString());
      }
      return text;
    }

    // Token: 0x06001185 RID: 4485 RVA: 0x0039C9F0 File Offset: 0x0039C9F0
    public void RefreshSoon()
    {
      double[] array = new double[] { 2.5, 7.0 };
      for (int i = 0; i < array.Length; i++)
      {
        Task.Delay(TimeSpan.FromSeconds(array[i])).ContinueWith(delegate(Task _)
        {
          this.Refresh();
        }, TaskScheduler.Default);
      }
    }

    // Token: 0x06001186 RID: 4486 RVA: 0x0039CA50 File Offset: 0x0039CA50
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.ResetCore();
      }
      this.RaiseChanged();
    }

    // Token: 0x06001187 RID: 4487 RVA: 0x0039CA98 File Offset: 0x0039CA98
    private void ResetCore()
    {
      this.account = null;
      this.lastRefreshStartedAt = null;
      this.messages = new List<ConversationMessage>();
      this.HasEarlierMessages = false;
      this.IsLoadingNewest = false;
      this.IsLoadingEarlier = false;
      this.HasLoadFailed = false;
      this.LastAnswerEndedAt = null;
      this.liveReplyId = null;
      this.liveSpokenReplyId = null;
    }

    // Token: 0x06001188 RID: 4488 RVA: 0x0039CAFC File Offset: 0x0039CAFC
    private void Merge(IReadOnlyList<ConversationMessage> incoming)
    {
      Dictionary<long, ConversationMessage> dictionary = new Dictionary<long, ConversationMessage>();
      foreach (ConversationMessage conversationMessage in this.messages)
      {
        long? num = conversationMessage.ServerId;
        if (num != null)
        {
          long valueOrDefault = num.GetValueOrDefault();
          dictionary[valueOrDefault] = conversationMessage;
        }
      }
      foreach (ConversationMessage conversationMessage2 in incoming)
      {
        long? num = conversationMessage2.ServerId;
        if (num != null)
        {
          long valueOrDefault2 = num.GetValueOrDefault();
          ConversationMessage conversationMessage3 = conversationMessage2;
          ConversationMessage conversationMessage4;
          dictionary.TryGetValue(valueOrDefault2, out conversationMessage4);
          ConversationMessage conversationMessage5 = conversationMessage3.<Clone>$();
          conversationMessage5.AttachmentPaths = conversationMessage3.AttachmentPaths ?? ((conversationMessage4 != null) ? conversationMessage4.AttachmentPaths : null);
          conversationMessage5.BubbleSegments = conversationMessage3.BubbleSegments ?? ((conversationMessage4 != null) ? conversationMessage4.BubbleSegments : null);
          conversationMessage5.ReplacedLocalId = conversationMessage3.ReplacedLocalId ?? ((conversationMessage4 != null) ? conversationMessage4.ReplacedLocalId : null);
          conversationMessage3 = conversationMessage5;
          dictionary[valueOrDefault2] = conversationMessage3;
        }
      }
      List<ConversationMessage> saved = (from m in dictionary.Values
        orderby m.CreatedAt, m.ServerId.GetValueOrDefault()
        select m).ToList<ConversationMessage>();
      IEnumerable<ConversationMessage> enumerable = this.messages;
      Func<ConversationMessage, bool> <>9__8;
      Func<ConversationMessage, bool> func;
      if ((func = <>9__8) == null)
      {
        func = (<>9__8 = (ConversationMessage m) => m.IsPending && m.Id != this.liveSpokenReplyId);
      }
      using (IEnumerator<ConversationMessage> enumerator2 = enumerable.Where<ConversationMessage>(func).GetEnumerator())
      {
        while (enumerator2.MoveNext())
        {
          ConversationMessage pending2 = enumerator2.Current;
          int num2;
          if (pending2.IsUser)
          {
            string typedText = pending2.Content.Trim();
            bool isFilesOnly = typedText.StartsWith("\ud83d\udcce ", StringComparison.Ordinal);
            num2 = saved.FindIndex((ConversationMessage s) => s.IsUser && s.ReplacedLocalId == null && s.CreatedAt >= pending2.CreatedAt.AddSeconds((double)(isFilesOnly ? (-20) : (-120))) && (isFilesOnly || ConversationHistory.IsSavedCopy(s, typedText)));
          }
          else
          {
            string opening = ConversationHistory.MatchingOpening(pending2.BubbleText);
            num2 = saved.FindIndex((ConversationMessage s) => ConversationHistory.IsUnclaimedReplyFor(pending2, s) && ConversationHistory.SameOpening(pending2, opening, s));
            if (num2 < 0)
            {
              num2 = saved.FindIndex((ConversationMessage s) => ConversationHistory.IsUnclaimedReplyFor(pending2, s) && ConversationHistory.SavedAboutThen(pending2, s));
            }
          }
          if (num2 >= 0)
          {
            List<ConversationMessage> saved3 = saved;
            int num3 = num2;
            ConversationMessage conversationMessage6 = saved[num2].<Clone>$();
            conversationMessage6.ReplacedLocalId = pending2.Id;
            conversationMessage6.AttachmentPaths = saved[num2].AttachmentPaths ?? pending2.AttachmentPaths;
            conversationMessage6.BubbleSegments = saved[num2].BubbleSegments ?? pending2.BubbleSegments;
            saved3[num3] = conversationMessage6;
          }
        }
      }
      DateTimeOffset now = this.clock();
      List<ConversationMessage> list = this.messages.Where<ConversationMessage>(delegate(ConversationMessage pending)
      {
        if (!pending.IsPending || now - pending.CreatedAt >= ConversationHistory.PendingMessageLifetime)
        {
          return false;
        }
        if (pending.Id == this.liveSpokenReplyId)
        {
          return true;
        }
        string typedText = pending.Content.Trim();
        if (!pending.IsUser)
        {
          string opening = ConversationHistory.MatchingOpening(pending.BubbleText);
          return !saved.Any<ConversationMessage>((ConversationMessage s) => ConversationHistory.IsUnclaimedReplyFor(pending, s) && (ConversationHistory.SameOpening(pending, opening, s) || ConversationHistory.SavedAboutThen(pending, s)));
        }
        if (typedText.StartsWith("\ud83d\udcce ", StringComparison.Ordinal))
        {
          return !saved.Any<ConversationMessage>((ConversationMessage s) => s.IsUser && s.CreatedAt >= pending.CreatedAt.AddSeconds(-20.0));
        }
        return !saved.Any<ConversationMessage>((ConversationMessage s) => s.IsUser && s.CreatedAt >= pending.CreatedAt.AddSeconds(-120.0) && ConversationHistory.IsSavedCopy(s, typedText));
      }).ToList<ConversationMessage>();
      List<ConversationMessage> list2 = this.messages.Where<ConversationMessage>((ConversationMessage m) => m.SentToAgentName != null).ToList<ConversationMessage>();
      List<ConversationMessage> saved2 = saved;
      List<ConversationMessage> list3 = list;
      int num4 = saved2.Count + list3.Count;
      List<ConversationMessage> list4 = new List<ConversationMessage>(num4);
      CollectionsMarshal.SetCount<ConversationMessage>(list4, num4);
      Span<ConversationMessage> span = CollectionsMarshal.AsSpan<ConversationMessage>(list4);
      int num5 = 0;
      Span<ConversationMessage> span2 = CollectionsMarshal.AsSpan<ConversationMessage>(saved2);
      span2.CopyTo(span.Slice(num5, span2.Length));
      num5 += span2.Length;
      Span<ConversationMessage> span3 = CollectionsMarshal.AsSpan<ConversationMessage>(list3);
      span3.CopyTo(span.Slice(num5, span3.Length));
      num5 += span3.Length;
      List<ConversationMessage> list5 = list4;
      if (list2.Count > 0)
      {
        list5 = (from p in list5.Concat<ConversationMessage>(list2).Select<ConversationMessage, ValueTuple<ConversationMessage, int>>((ConversationMessage m, int order) => new ValueTuple<ConversationMessage, int>(m, order))
          orderby p.Item1.CreatedAt, p.Item2
          select p.Item1).ToList<ConversationMessage>();
      }
      this.messages = list5;
      this.SaveToDisk();
    }

    // Token: 0x06001189 RID: 4489 RVA: 0x0039D058 File Offset: 0x0039D058
    public static bool IsSavedCopy(ConversationMessage saved, string sentText)
    {
      string text = saved.TypedWords.Trim();
      if (text == sentText)
      {
        return true;
      }
      if (sentText.Length <= 4000)
      {
        return false;
      }
      if (text.EndsWith('\ufffd'))
      {
        string text2 = text;
        text = text2.Substring(0, text2.Length - 1);
      }
      return text.Length >= 3900 && sentText.StartsWith(text, StringComparison.Ordinal);
    }

    // Token: 0x0600118A RID: 4490 RVA: 0x0039D0C4 File Offset: 0x0039D0C4
    private static bool IsUnclaimedReplyFor(ConversationMessage pending, ConversationMessage saved)
    {
      return !saved.IsUser && (saved.ReplacedLocalId == null || saved.ReplacedLocalId == pending.Id);
    }

    // Token: 0x0600118B RID: 4491 RVA: 0x0039D0EC File Offset: 0x0039D0EC
    private static bool SameOpening(ConversationMessage pending, string opening, ConversationMessage saved)
    {
      return opening.Length > 0 && (saved.CreatedAt - pending.CreatedAt).TotalSeconds >= -180.0 && ConversationHistory.MatchingOpening(saved.BubbleText) == opening;
    }

    // Token: 0x0600118C RID: 4492 RVA: 0x0039D13C File Offset: 0x0039D13C
    private static bool SavedAboutThen(ConversationMessage pending, ConversationMessage saved)
    {
      return (saved.CreatedAt - pending.CreatedAt).TotalSeconds >= (pending.Id.StartsWith("local-spoken-", StringComparison.Ordinal) ? (-5.0) : (-20.0));
    }

    // Token: 0x0600118D RID: 4493 RVA: 0x0039D190 File Offset: 0x0039D190
    private static string MatchingOpening(string text)
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c in text.ToLowerInvariant())
      {
        if (char.IsWhiteSpace(c) || char.IsPunctuation(c))
        {
          if (stringBuilder.Length > 0)
          {
            list.Add(stringBuilder.ToString());
            stringBuilder.Clear();
          }
        }
        else
        {
          stringBuilder.Append(c);
        }
      }
      if (stringBuilder.Length > 0)
      {
        list.Add(stringBuilder.ToString());
      }
      string text3 = string.Join(" ", list);
      if (text3.Length <= 32)
      {
        return text3;
      }
      return text3.Substring(0, 32);
    }

    // Token: 0x0600118E RID: 4494 RVA: 0x0039D240 File Offset: 0x0039D240
    [NullableContext(2)]
    private void AdoptAccount(string next)
    {
      if (next == this.account)
      {
        return;
      }
      this.ResetCore();
      this.account = next;
      string cachePath = this.CachePath;
      if (cachePath == null || !File.Exists(cachePath))
      {
        return;
      }
      try
      {
        CachedConversation cachedConversation = JsonSerializer.Deserialize<CachedConversation>(File.ReadAllBytes(cachePath), NotchChatJsonContext.Default.CachedConversation);
        if (cachedConversation != null)
        {
          this.messages = cachedConversation.Messages.ToList<ConversationMessage>();
          this.HasEarlierMessages = cachedConversation.HasEarlierMessages;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ChatHistory", "cache unreadable: " + ex.Message);
      }
    }

    // Token: 0x1700041C RID: 1052
    // (get) Token: 0x0600118F RID: 4495 RVA: 0x0039D2EC File Offset: 0x0039D2EC
    [Nullable(2)]
    private string CachePath
    {
      [NullableContext(2)]
      get
      {
        if (string.IsNullOrEmpty(this.account))
        {
          return null;
        }
        string text = new string(this.account.Where<char>((char c) => char.IsLetterOrDigit(c) || c == '-').ToArray<char>());
        if (text.Length != 0)
        {
          return Path.Combine(this.cacheFolder, text + ".json");
        }
        return null;
      }
    }

    // Token: 0x06001190 RID: 4496 RVA: 0x0039D360 File Offset: 0x0039D360
    private void SaveToDisk()
    {
      string path = this.CachePath;
      if (path == null)
      {
        return;
      }
      List<ConversationMessage> list = this.messages.Where<ConversationMessage>((ConversationMessage m) => !m.IsPending).ToList<ConversationMessage>();
      List<ConversationMessage> list2 = list.Skip<ConversationMessage>(Math.Max(0, list.Count - 200)).ToList<ConversationMessage>();
      CachedConversation cachedConversation = new CachedConversation(list2, this.HasEarlierMessages || list2.Count < list.Count);
      byte[] bytes = JsonSerializer.SerializeToUtf8Bytes<CachedConversation>(cachedConversation, NotchChatJsonContext.Default.CachedConversation);
      Task.Run(delegate
      {
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(path));
          string text = path + ".tmp";
          File.WriteAllBytes(text, bytes);
          File.Move(text, path, true);
        }
        catch (Exception ex)
        {
          Log.Warn("ChatHistory", "cache write failed: " + ex.Message);
        }
      });
    }

    // Token: 0x06001191 RID: 4497 RVA: 0x0039D420 File Offset: 0x0039D420
    private void RaiseChanged()
    {
      try
      {
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ChatHistory", "Changed handler threw: " + ex.Message);
      }
    }

    // Token: 0x0400078A RID: 1930
    private const int PageSize = 40;

    // Token: 0x0400078B RID: 1931
    private const int CachedMessageLimit = 200;

    // Token: 0x0400078C RID: 1932
    private static readonly TimeSpan PendingMessageLifetime = TimeSpan.FromMinutes(10L);

    // Token: 0x0400078D RID: 1933
    private readonly object gate = new object();

    // Token: 0x0400078E RID: 1934
    [Nullable(new byte[] { 1, 2 })]
    private readonly Func<string> accountId;

    // Token: 0x0400078F RID: 1935
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<long?, CancellationToken, Task<ConversationHistory.Page>> fetchPage;

    // Token: 0x04000790 RID: 1936
    private readonly Func<DateTimeOffset> clock;

    // Token: 0x04000791 RID: 1937
    private readonly string cacheFolder;

    // Token: 0x04000792 RID: 1938
    private List<ConversationMessage> messages = new List<ConversationMessage>();

    // Token: 0x04000793 RID: 1939
    [Nullable(2)]
    private string account;

    // Token: 0x04000794 RID: 1940
    private DateTimeOffset? lastRefreshStartedAt;

    // Token: 0x04000795 RID: 1941
    private bool refreshRunning;

    // Token: 0x04000796 RID: 1942
    private bool earlierRunning;

    // Token: 0x04000797 RID: 1943
    [Nullable(2)]
    private string liveReplyId;

    // Token: 0x04000798 RID: 1944
    [Nullable(2)]
    private string liveSpokenReplyId;

    // Token: 0x0400079F RID: 1951
    private const int SavedWordsLimit = 4000;

    // Token: 0x040007A0 RID: 1952
    private const string SpokenReplyIdPrefix = "local-spoken-";

    // Token: 0x040007A1 RID: 1953
    private const double SpokenReplyClockSlackSeconds = 5.0;

    // Token: 0x02000449 RID: 1097
    [Nullable(0)]
    public sealed class Page : IEquatable<ConversationHistory.Page>
    {
      // Token: 0x06002E02 RID: 11778 RVA: 0x0042E9F4 File Offset: 0x0042E9F4
      public Page(IReadOnlyList<ConversationMessage> Messages, bool HasMore)
      {
        this.Messages = Messages;
        this.HasMore = HasMore;
        base..ctor();
      }

      // Token: 0x17000864 RID: 2148
      // (get) Token: 0x06002E03 RID: 11779 RVA: 0x0042EA0C File Offset: 0x0042EA0C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ConversationHistory.Page);
        }
      }

      // Token: 0x17000865 RID: 2149
      // (get) Token: 0x06002E04 RID: 11780 RVA: 0x0042EA18 File Offset: 0x0042EA18
      // (set) Token: 0x06002E05 RID: 11781 RVA: 0x0042EA20 File Offset: 0x0042EA20
      public IReadOnlyList<ConversationMessage> Messages { get; set; }

      // Token: 0x17000866 RID: 2150
      // (get) Token: 0x06002E06 RID: 11782 RVA: 0x0042EA2C File Offset: 0x0042EA2C
      // (set) Token: 0x06002E07 RID: 11783 RVA: 0x0042EA34 File Offset: 0x0042EA34
      public bool HasMore { get; set; }

      // Token: 0x06002E08 RID: 11784 RVA: 0x0042EA40 File Offset: 0x0042EA40
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Page");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002E09 RID: 11785 RVA: 0x0042EA8C File Offset: 0x0042EA8C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Messages = ");
        builder.Append(this.Messages);
        builder.Append(", HasMore = ");
        builder.Append(this.HasMore.ToString());
        return true;
      }

      // Token: 0x06002E0A RID: 11786 RVA: 0x0042EAE0 File Offset: 0x0042EAE0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ConversationHistory.Page left, ConversationHistory.Page right)
      {
        return !(left == right);
      }

      // Token: 0x06002E0B RID: 11787 RVA: 0x0042EAEC File Offset: 0x0042EAEC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ConversationHistory.Page left, ConversationHistory.Page right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002E0C RID: 11788 RVA: 0x0042EB00 File Offset: 0x0042EB00
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<ConversationMessage>>.Default.GetHashCode(this.<Messages>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasMore>k__BackingField);
      }

      // Token: 0x06002E0D RID: 11789 RVA: 0x0042EB40 File Offset: 0x0042EB40
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ConversationHistory.Page);
      }

      // Token: 0x06002E0E RID: 11790 RVA: 0x0042EB50 File Offset: 0x0042EB50
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ConversationHistory.Page other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<ConversationMessage>>.Default.Equals(this.<Messages>k__BackingField, other.<Messages>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasMore>k__BackingField, other.<HasMore>k__BackingField));
      }

      // Token: 0x06002E10 RID: 11792 RVA: 0x0042EBB4 File Offset: 0x0042EBB4
      [CompilerGenerated]
      private Page(ConversationHistory.Page original)
      {
        this.Messages = original.<Messages>k__BackingField;
        this.HasMore = original.<HasMore>k__BackingField;
      }

      // Token: 0x06002E11 RID: 11793 RVA: 0x0042EBD4 File Offset: 0x0042EBD4
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<ConversationMessage> Messages, out bool HasMore)
      {
        Messages = this.Messages;
        HasMore = this.HasMore;
      }
    }

    // Token: 0x0200044A RID: 1098
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001C9D RID: 7325
      [Nullable(0)]
      public static Func<string, string> <0>__GetFileName;
    }
  }
}
