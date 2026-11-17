using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000125 RID: 293
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkActionFeedback : IEquatable<ClickyInkActionFeedback>
  {
    // Token: 0x1700047D RID: 1149
    // (get) Token: 0x0600132E RID: 4910 RVA: 0x003A5CB0 File Offset: 0x003A5CB0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkActionFeedback);
      }
    }

    // Token: 0x1700047E RID: 1150
    // (get) Token: 0x0600132F RID: 4911 RVA: 0x003A5CBC File Offset: 0x003A5CBC
    // (set) Token: 0x06001330 RID: 4912 RVA: 0x003A5CC4 File Offset: 0x003A5CC4
    [Nullable(2)]
    public ClickyInkSentAction Pending
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700047F RID: 1151
    // (get) Token: 0x06001331 RID: 4913 RVA: 0x003A5CD0 File Offset: 0x003A5CD0
    // (set) Token: 0x06001332 RID: 4914 RVA: 0x003A5CD8 File Offset: 0x003A5CD8
    public IReadOnlyDictionary<string, ClickyInkSentAction> Answered { get; set; }

    // Token: 0x17000480 RID: 1152
    // (get) Token: 0x06001333 RID: 4915 RVA: 0x003A5CE4 File Offset: 0x003A5CE4
    public bool IsLocked
    {
      get
      {
        return this.Pending != null;
      }
    }

    // Token: 0x06001334 RID: 4916 RVA: 0x003A5CF4 File Offset: 0x003A5CF4
    public static ClickyInkActionFeedback Make(IReadOnlyList<ClickyInkSentAction> sent, DateTimeOffset? lastVersionAt, bool isRunning, DateTimeOffset? finishedAt, [Nullable(2)] IReadOnlyList<DateTimeOffset> editStartedAt = null, DateTimeOffset? now = null)
    {
      IReadOnlyList<DateTimeOffset> readOnlyList = editStartedAt ?? Array.Empty<DateTimeOffset>();
      DateTimeOffset dateTimeOffset = now ?? DateTimeOffset.Now;
      Dictionary<string, ClickyInkSentAction> dictionary = new Dictionary<string, ClickyInkSentAction>();
      List<ClickyInkSentAction> list = sent.OrderBy<ClickyInkSentAction, DateTimeOffset>((ClickyInkSentAction a) => a.SentAt).ToList<ClickyInkSentAction>();
      foreach (ClickyInkSentAction clickyInkSentAction in list)
      {
        if (clickyInkSentAction.Kind != ClickyInkNoteAction.ActionKind.Ask && ClickyInkActionFeedback.IsStillCurrent(clickyInkSentAction, readOnlyList))
        {
          dictionary[clickyInkSentAction.ObjectKey] = clickyInkSentAction;
        }
      }
      ClickyInkSentAction clickyInkSentAction2 = null;
      if (list.Count > 0)
      {
        List<ClickyInkSentAction> list2 = list;
        ClickyInkSentAction clickyInkSentAction3 = list2[list2.Count - 1];
        bool flag = (dateTimeOffset - clickyInkSentAction3.SentAt).TotalSeconds > 900.0;
        double num = (clickyInkSentAction3.WasSentWhileRunning ? 5.0 : 0.0);
        bool flag2;
        if (lastVersionAt != null)
        {
          DateTimeOffset valueOrDefault = lastVersionAt.GetValueOrDefault();
          flag2 = (valueOrDefault - clickyInkSentAction3.SentAt).TotalSeconds > num;
        }
        else
        {
          flag2 = false;
        }
        bool flag3 = flag2;
        bool flag4;
        if (!isRunning && finishedAt != null)
        {
          DateTimeOffset valueOrDefault2 = finishedAt.GetValueOrDefault();
          flag4 = valueOrDefault2 > clickyInkSentAction3.SentAt;
        }
        else
        {
          flag4 = false;
        }
        bool flag5 = flag4;
        if (!flag && !flag3 && !flag5)
        {
          clickyInkSentAction2 = clickyInkSentAction3;
        }
      }
      return new ClickyInkActionFeedback
      {
        Pending = clickyInkSentAction2,
        Answered = dictionary
      };
    }

    // Token: 0x06001335 RID: 4917 RVA: 0x003A5E94 File Offset: 0x003A5E94
    public static bool IsStillCurrent(ClickyInkSentAction action, IReadOnlyList<DateTimeOffset> editStartedAt)
    {
      return editStartedAt.Count<DateTimeOffset>((DateTimeOffset d) => d > action.SentAt) <= 1;
    }

    // Token: 0x06001336 RID: 4918 RVA: 0x003A5EC8 File Offset: 0x003A5EC8
    [return: Nullable(2)]
    public ClickyInkSentAction AnswerFor(ClickyInkDocument.Block block)
    {
      ClickyInkSentAction clickyInkSentAction;
      if (!this.Answered.TryGetValue(ClickyInkObjectKey.Of(block), out clickyInkSentAction))
      {
        return null;
      }
      return clickyInkSentAction;
    }

    // Token: 0x06001337 RID: 4919 RVA: 0x003A5EF0 File Offset: 0x003A5EF0
    public bool IsPending(ClickyInkDocument.Block block)
    {
      ClickyInkSentAction pending = this.Pending;
      return ((pending != null) ? pending.ObjectKey : null) == ClickyInkObjectKey.Of(block);
    }

    // Token: 0x06001338 RID: 4920 RVA: 0x003A5F10 File Offset: 0x003A5F10
    [NullableContext(2)]
    public bool Equals(ClickyInkActionFeedback other)
    {
      return other != null && object.Equals(this.Pending, other.Pending) && this.Answered.Count == other.Answered.Count && this.Answered.All<KeyValuePair<string, ClickyInkSentAction>>(delegate(KeyValuePair<string, ClickyInkSentAction> kv)
      {
        ClickyInkSentAction clickyInkSentAction;
        return other.Answered.TryGetValue(kv.Key, out clickyInkSentAction) && clickyInkSentAction.Equals(kv.Value);
      });
    }

    // Token: 0x06001339 RID: 4921 RVA: 0x003A5F80 File Offset: 0x003A5F80
    public override int GetHashCode()
    {
      return HashCode.Combine<ClickyInkSentAction, int>(this.Pending, this.Answered.Count);
    }

    // Token: 0x0600133A RID: 4922 RVA: 0x003A5F98 File Offset: 0x003A5F98
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkActionFeedback");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600133B RID: 4923 RVA: 0x003A5FE4 File Offset: 0x003A5FE4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Pending = ");
      builder.Append(this.Pending);
      builder.Append(", Answered = ");
      builder.Append(this.Answered);
      builder.Append(", IsLocked = ");
      builder.Append(this.IsLocked.ToString());
      return true;
    }

    // Token: 0x0600133C RID: 4924 RVA: 0x003A6050 File Offset: 0x003A6050
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkActionFeedback left, ClickyInkActionFeedback right)
    {
      return !(left == right);
    }

    // Token: 0x0600133D RID: 4925 RVA: 0x003A605C File Offset: 0x003A605C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkActionFeedback left, ClickyInkActionFeedback right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600133E RID: 4926 RVA: 0x003A6070 File Offset: 0x003A6070
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkActionFeedback);
    }

    // Token: 0x06001340 RID: 4928 RVA: 0x003A6088 File Offset: 0x003A6088
    [CompilerGenerated]
    private ClickyInkActionFeedback(ClickyInkActionFeedback original)
    {
      this.Pending = original.<Pending>k__BackingField;
      this.Answered = original.<Answered>k__BackingField;
    }

    // Token: 0x06001341 RID: 4929 RVA: 0x003A60A8 File Offset: 0x003A60A8
    public ClickyInkActionFeedback()
    {
      this.Answered = new Dictionary<string, ClickyInkSentAction>();
      base..ctor();
    }

    // Token: 0x04000897 RID: 2199
    public const double SteerPickupSeconds = 5.0;

    // Token: 0x04000898 RID: 2200
    public const double PendingGivesUpSeconds = 900.0;
  }
}
