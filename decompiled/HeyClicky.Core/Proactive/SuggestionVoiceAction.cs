using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D7 RID: 215
  [NullableContext(1)]
  [Nullable(0)]
  public static class SuggestionVoiceAction
  {
    // Token: 0x06000DBD RID: 3517 RVA: 0x00383510 File Offset: 0x00383510
    public static string Reference(Guid id)
    {
      return "[Approve suggested task " + id.ToString("D").ToUpperInvariant() + "]";
    }

    // Token: 0x06000DBE RID: 3518 RVA: 0x00383534 File Offset: 0x00383534
    public static string SkipReference(Guid id)
    {
      return "[Skip suggested task " + id.ToString("D").ToUpperInvariant() + "]";
    }

    // Token: 0x06000DBF RID: 3519 RVA: 0x00383558 File Offset: 0x00383558
    public static SuggestionVoiceAction.Request Parse(string task)
    {
      string[] array = task.Trim().Split('\n', StringSplitOptions.None);
      string text = array[0];
      bool flag = text.StartsWith("[Skip suggested task", StringComparison.Ordinal);
      if (!flag && !text.StartsWith("[Approve suggested task", StringComparison.Ordinal))
      {
        return new SuggestionVoiceAction.Request(SuggestionVoiceAction.Kind.NotSuggestionAction, default(Guid), null);
      }
      string text2 = (flag ? "[Skip suggested task " : "[Approve suggested task ");
      if (text.StartsWith(text2, StringComparison.Ordinal) && text.EndsWith(']'))
      {
        string text3 = text;
        int length = text2.Length;
        Guid guid;
        if (Guid.TryParse(text3.Substring(length, text3.Length - 1 - length), out guid))
        {
          string text4 = string.Join("\n", array.Skip<string>(1)).Trim();
          return new SuggestionVoiceAction.Request(flag ? SuggestionVoiceAction.Kind.Skip : SuggestionVoiceAction.Kind.Approve, guid, (text4.Length == 0) ? null : text4);
        }
      }
      return new SuggestionVoiceAction.Request(SuggestionVoiceAction.Kind.Invalid, default(Guid), null);
    }

    // Token: 0x06000DC0 RID: 3520 RVA: 0x0038363C File Offset: 0x0038363C
    public static string ContextNote([Nullable(2)] ProactiveSuggestionCard card, DateTimeOffset now)
    {
      if (card == null || card.IsExpired(now))
      {
        return "[Suggestions page] The user is looking at Suggestions, but there is no available task on screen to approve or skip. Do not act on an earlier suggestion reference or invent a new task.";
      }
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["title"] = card.Title;
      dictionary["description"] = card.Description;
      string text = JsonSerializer.Serialize<Dictionary<string, string>>(dictionary, ProactiveJsonContext.Default.DictionaryStringString);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2013, 3);
      defaultInterpolatedStringHandler.AppendLiteral("[Suggestions page — current turn] The user is reading this proposed task: ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\nInterpret their intent naturally; the examples below are not a fixed phrase list.\nAPPROVE: If they approve it (\"go do it\", \"yes\", \"start this\"), call sessions_spawn with task exactly \"");
      defaultInterpolatedStringHandler.AppendFormatted(SuggestionVoiceAction.Reference(card.Id));
      defaultInterpolatedStringHandler.AppendLiteral("\". If they explicitly approve with changes, append a newline and only their requested changes. The app resolves the full brief, owning Clicky and routine, records approval in that Clicky's chat, and advances to the next suggestion. Do not copy the brief into an ordinary spawn or send it to a previous chat.\nSKIP: If they reject this proposal or ask for the next one (\"skip this\", \"show me the next\", \"I don't want to do this\", \"I don't like this\"), call sessions_spawn with task exactly \"");
      defaultInterpolatedStringHandler.AppendFormatted(SuggestionVoiceAction.SkipReference(card.Id));
      defaultInterpolatedStringHandler.AppendLiteral("\". This is an app UI action: it skips the card and shows the next without starting any agent. If they gave a reason, append a newline and only that reason in their words; do not invent feedback or ask them to fill out a reason box. A bare rejection is enough to skip.\nADJUST: If they want to change this task, ask what they want different and any necessary follow-up one question at a time. The opening request to adjust is never permission to launch. Once their changes are clear and they are ready, use the APPROVE reference above with their changes after a newline; the app preserves the full original brief and advances only this card.\nQuestions about the proposal are not approval or a request to skip. A requested adjustment is not rejection. If their intent is ambiguous, clarify without changing the card. Act only on the current card, once for this user turn; never approve or skip another card from earlier context or continue through the deck without a new user request. After approval, say only the short acknowledgment specified by the tool result, with no recap or follow-up. After skipping, briefly acknowledge its actual result (the next card shown or no suggestions left). Do not navigate away from Suggestions or automatically generate more tasks.");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000DC1 RID: 3521 RVA: 0x00383704 File Offset: 0x00383704
    public static string AdjustmentBudget(int answerCount)
    {
      string text;
      if (answerCount > 0)
      {
        if (answerCount != 1)
        {
          text = "The user answered question 2 of 2. The question budget is exhausted: ask NO further questions, including confirmation or permission to proceed. Start this suggestion now with their stated adjustments using the APPROVE reference. Preserve the original brief for anything unchanged; do not invent missing facts. If essential details remain unknown, include them as open inputs for the task. An explicit cancellation or skip still takes priority over starting.";
        }
        else
        {
          text = "The user answered question 1 of 2. If their adjustment is clear, start it now. Otherwise ask exactly ONE final clarification (question 2 of 2), then wait. Never bundle multiple questions.";
        }
      }
      else
      {
        text = "Question 1 of 2: ask one short question about what they want changed, then wait. Do not start yet.";
      }
      return text;
    }

    // Token: 0x040005C9 RID: 1481
    private const string ApprovalPrefix = "[Approve suggested task ";

    // Token: 0x040005CA RID: 1482
    private const string SkipPrefix = "[Skip suggested task ";

    // Token: 0x020003E6 RID: 998
    [NullableContext(0)]
    public enum Kind
    {
      // Token: 0x04001A33 RID: 6707
      NotSuggestionAction,
      // Token: 0x04001A34 RID: 6708
      Invalid,
      // Token: 0x04001A35 RID: 6709
      Approve,
      // Token: 0x04001A36 RID: 6710
      Skip
    }

    // Token: 0x020003E7 RID: 999
    [NullableContext(2)]
    [Nullable(0)]
    public readonly struct Request : IEquatable<SuggestionVoiceAction.Request>
    {
      // Token: 0x06002AFC RID: 11004 RVA: 0x004285B4 File Offset: 0x004285B4
      public Request(SuggestionVoiceAction.Kind Kind, Guid CardId = default(Guid), string Detail = null)
      {
        this.Kind = Kind;
        this.CardId = CardId;
        this.Detail = Detail;
      }

      // Token: 0x17000838 RID: 2104
      // (get) Token: 0x06002AFD RID: 11005 RVA: 0x004285CC File Offset: 0x004285CC
      // (set) Token: 0x06002AFE RID: 11006 RVA: 0x004285D4 File Offset: 0x004285D4
      public SuggestionVoiceAction.Kind Kind { get; set; }

      // Token: 0x17000839 RID: 2105
      // (get) Token: 0x06002AFF RID: 11007 RVA: 0x004285E0 File Offset: 0x004285E0
      // (set) Token: 0x06002B00 RID: 11008 RVA: 0x004285E8 File Offset: 0x004285E8
      public Guid CardId { get; set; }

      // Token: 0x1700083A RID: 2106
      // (get) Token: 0x06002B01 RID: 11009 RVA: 0x004285F4 File Offset: 0x004285F4
      // (set) Token: 0x06002B02 RID: 11010 RVA: 0x004285FC File Offset: 0x004285FC
      public string Detail { get; set; }

      // Token: 0x06002B03 RID: 11011 RVA: 0x00428608 File Offset: 0x00428608
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Request");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002B04 RID: 11012 RVA: 0x00428654 File Offset: 0x00428654
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", CardId = ");
        builder.Append(this.CardId.ToString());
        builder.Append(", Detail = ");
        builder.Append(this.Detail);
        return true;
      }

      // Token: 0x06002B05 RID: 11013 RVA: 0x004286CC File Offset: 0x004286CC
      [CompilerGenerated]
      public static bool operator !=(SuggestionVoiceAction.Request left, SuggestionVoiceAction.Request right)
      {
        return !(left == right);
      }

      // Token: 0x06002B06 RID: 11014 RVA: 0x004286D8 File Offset: 0x004286D8
      [CompilerGenerated]
      public static bool operator ==(SuggestionVoiceAction.Request left, SuggestionVoiceAction.Request right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002B07 RID: 11015 RVA: 0x004286E4 File Offset: 0x004286E4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<SuggestionVoiceAction.Kind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(this.<CardId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Detail>k__BackingField);
      }

      // Token: 0x06002B08 RID: 11016 RVA: 0x00428724 File Offset: 0x00428724
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is SuggestionVoiceAction.Request && this.Equals((SuggestionVoiceAction.Request)obj);
      }

      // Token: 0x06002B09 RID: 11017 RVA: 0x0042873C File Offset: 0x0042873C
      [CompilerGenerated]
      public bool Equals(SuggestionVoiceAction.Request other)
      {
        return EqualityComparer<SuggestionVoiceAction.Kind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<Guid>.Default.Equals(this.<CardId>k__BackingField, other.<CardId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Detail>k__BackingField, other.<Detail>k__BackingField);
      }

      // Token: 0x06002B0A RID: 11018 RVA: 0x00428794 File Offset: 0x00428794
      [CompilerGenerated]
      public void Deconstruct(out SuggestionVoiceAction.Kind Kind, out Guid CardId, out string Detail)
      {
        Kind = this.Kind;
        CardId = this.CardId;
        Detail = this.Detail;
      }
    }
  }
}
