using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000121 RID: 289
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNoteAction : IEquatable<ClickyInkNoteAction>
  {
    // Token: 0x060012DE RID: 4830 RVA: 0x003A3F8C File Offset: 0x003A3F8C
    public ClickyInkNoteAction(ClickyInkNoteAction.ActionKind Kind, string ObjectKey, string Message, string LaunchContext, string MarginWords, string ClickyLine, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> ReviewedItems = null)
    {
      this.Kind = Kind;
      this.ObjectKey = ObjectKey;
      this.Message = Message;
      this.LaunchContext = LaunchContext;
      this.MarginWords = MarginWords;
      this.ClickyLine = ClickyLine;
      this.ReviewedItems = ReviewedItems;
      this.AnalyticsAction = "";
      this.AnalyticsCounts = new Dictionary<string, int>();
      base..ctor();
    }

    // Token: 0x1700046A RID: 1130
    // (get) Token: 0x060012DF RID: 4831 RVA: 0x003A3FEC File Offset: 0x003A3FEC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkNoteAction);
      }
    }

    // Token: 0x1700046B RID: 1131
    // (get) Token: 0x060012E0 RID: 4832 RVA: 0x003A3FF8 File Offset: 0x003A3FF8
    // (set) Token: 0x060012E1 RID: 4833 RVA: 0x003A4000 File Offset: 0x003A4000
    public ClickyInkNoteAction.ActionKind Kind { get; set; }

    // Token: 0x1700046C RID: 1132
    // (get) Token: 0x060012E2 RID: 4834 RVA: 0x003A400C File Offset: 0x003A400C
    // (set) Token: 0x060012E3 RID: 4835 RVA: 0x003A4014 File Offset: 0x003A4014
    public string ObjectKey { get; set; }

    // Token: 0x1700046D RID: 1133
    // (get) Token: 0x060012E4 RID: 4836 RVA: 0x003A4020 File Offset: 0x003A4020
    // (set) Token: 0x060012E5 RID: 4837 RVA: 0x003A4028 File Offset: 0x003A4028
    public string Message { get; set; }

    // Token: 0x1700046E RID: 1134
    // (get) Token: 0x060012E6 RID: 4838 RVA: 0x003A4034 File Offset: 0x003A4034
    // (set) Token: 0x060012E7 RID: 4839 RVA: 0x003A403C File Offset: 0x003A403C
    public string LaunchContext { get; set; }

    // Token: 0x1700046F RID: 1135
    // (get) Token: 0x060012E8 RID: 4840 RVA: 0x003A4048 File Offset: 0x003A4048
    // (set) Token: 0x060012E9 RID: 4841 RVA: 0x003A4050 File Offset: 0x003A4050
    public string MarginWords { get; set; }

    // Token: 0x17000470 RID: 1136
    // (get) Token: 0x060012EA RID: 4842 RVA: 0x003A405C File Offset: 0x003A405C
    // (set) Token: 0x060012EB RID: 4843 RVA: 0x003A4064 File Offset: 0x003A4064
    public string ClickyLine { get; set; }

    // Token: 0x17000471 RID: 1137
    // (get) Token: 0x060012EC RID: 4844 RVA: 0x003A4070 File Offset: 0x003A4070
    // (set) Token: 0x060012ED RID: 4845 RVA: 0x003A4078 File Offset: 0x003A4078
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> ReviewedItems
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000472 RID: 1138
    // (get) Token: 0x060012EE RID: 4846 RVA: 0x003A4084 File Offset: 0x003A4084
    // (set) Token: 0x060012EF RID: 4847 RVA: 0x003A408C File Offset: 0x003A408C
    public string AnalyticsAction { get; set; }

    // Token: 0x17000473 RID: 1139
    // (get) Token: 0x060012F0 RID: 4848 RVA: 0x003A4098 File Offset: 0x003A4098
    // (set) Token: 0x060012F1 RID: 4849 RVA: 0x003A40A0 File Offset: 0x003A40A0
    [Nullable(2)]
    public string AnalyticsMethod
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000474 RID: 1140
    // (get) Token: 0x060012F2 RID: 4850 RVA: 0x003A40AC File Offset: 0x003A40AC
    // (set) Token: 0x060012F3 RID: 4851 RVA: 0x003A40B4 File Offset: 0x003A40B4
    public IReadOnlyDictionary<string, int> AnalyticsCounts { get; set; }

    // Token: 0x17000475 RID: 1141
    // (get) Token: 0x060012F4 RID: 4852 RVA: 0x003A40C0 File Offset: 0x003A40C0
    public string AnalyticsObject
    {
      get
      {
        string text = this.ObjectKey.Split('|', 2, StringSplitOptions.None)[0];
        if (text == null || text.Length <= 0)
        {
          return "unknown";
        }
        return text;
      }
    }

    // Token: 0x060012F5 RID: 4853 RVA: 0x003A40F4 File Offset: 0x003A40F4
    public ClickyInkNoteAction ForAnalytics(string action, [Nullable(2)] string method = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyDictionary<string, int> counts = null)
    {
      ClickyInkNoteAction clickyInkNoteAction = this.<Clone>$();
      clickyInkNoteAction.AnalyticsAction = action;
      clickyInkNoteAction.AnalyticsMethod = method;
      clickyInkNoteAction.AnalyticsCounts = counts ?? new Dictionary<string, int>();
      return clickyInkNoteAction;
    }

    // Token: 0x060012F6 RID: 4854 RVA: 0x003A411C File Offset: 0x003A411C
    [NullableContext(2)]
    public bool Equals(ClickyInkNoteAction other)
    {
      return other != null && this.Kind == other.Kind && this.ObjectKey == other.ObjectKey && this.Message == other.Message && this.LaunchContext == other.LaunchContext && this.MarginWords == other.MarginWords && this.ClickyLine == other.ClickyLine && ClickyInkNoteAction.SameItems(this.ReviewedItems, other.ReviewedItems) && this.AnalyticsAction == other.AnalyticsAction && this.AnalyticsMethod == other.AnalyticsMethod && this.AnalyticsCounts.Count == other.AnalyticsCounts.Count && this.AnalyticsCounts.All<KeyValuePair<string, int>>(delegate(KeyValuePair<string, int> kv)
      {
        int num;
        return other.AnalyticsCounts.TryGetValue(kv.Key, out num) && num == kv.Value;
      });
    }

    // Token: 0x060012F7 RID: 4855 RVA: 0x003A425C File Offset: 0x003A425C
    public override int GetHashCode()
    {
      return HashCode.Combine<ClickyInkNoteAction.ActionKind, string, string>(this.Kind, this.ObjectKey, this.Message);
    }

    // Token: 0x060012F8 RID: 4856 RVA: 0x003A4278 File Offset: 0x003A4278
    internal static bool SameItems([Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> a, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> b)
    {
      if (a != null)
      {
        return b != null && a.SequenceEqual<string>(b);
      }
      return b == null;
    }

    // Token: 0x060012F9 RID: 4857 RVA: 0x003A4290 File Offset: 0x003A4290
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkNoteAction");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060012FA RID: 4858 RVA: 0x003A42DC File Offset: 0x003A42DC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", ObjectKey = ");
      builder.Append(this.ObjectKey);
      builder.Append(", Message = ");
      builder.Append(this.Message);
      builder.Append(", LaunchContext = ");
      builder.Append(this.LaunchContext);
      builder.Append(", MarginWords = ");
      builder.Append(this.MarginWords);
      builder.Append(", ClickyLine = ");
      builder.Append(this.ClickyLine);
      builder.Append(", ReviewedItems = ");
      builder.Append(this.ReviewedItems);
      builder.Append(", AnalyticsAction = ");
      builder.Append(this.AnalyticsAction);
      builder.Append(", AnalyticsMethod = ");
      builder.Append(this.AnalyticsMethod);
      builder.Append(", AnalyticsCounts = ");
      builder.Append(this.AnalyticsCounts);
      builder.Append(", AnalyticsObject = ");
      builder.Append(this.AnalyticsObject);
      return true;
    }

    // Token: 0x060012FB RID: 4859 RVA: 0x003A4410 File Offset: 0x003A4410
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkNoteAction left, ClickyInkNoteAction right)
    {
      return !(left == right);
    }

    // Token: 0x060012FC RID: 4860 RVA: 0x003A441C File Offset: 0x003A441C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkNoteAction left, ClickyInkNoteAction right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060012FD RID: 4861 RVA: 0x003A4430 File Offset: 0x003A4430
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkNoteAction);
    }

    // Token: 0x060012FF RID: 4863 RVA: 0x003A4448 File Offset: 0x003A4448
    [CompilerGenerated]
    private ClickyInkNoteAction(ClickyInkNoteAction original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.ObjectKey = original.<ObjectKey>k__BackingField;
      this.Message = original.<Message>k__BackingField;
      this.LaunchContext = original.<LaunchContext>k__BackingField;
      this.MarginWords = original.<MarginWords>k__BackingField;
      this.ClickyLine = original.<ClickyLine>k__BackingField;
      this.ReviewedItems = original.<ReviewedItems>k__BackingField;
      this.AnalyticsAction = original.<AnalyticsAction>k__BackingField;
      this.AnalyticsMethod = original.<AnalyticsMethod>k__BackingField;
      this.AnalyticsCounts = original.<AnalyticsCounts>k__BackingField;
    }

    // Token: 0x06001300 RID: 4864 RVA: 0x003A44D4 File Offset: 0x003A44D4
    [CompilerGenerated]
    public void Deconstruct(out ClickyInkNoteAction.ActionKind Kind, out string ObjectKey, out string Message, out string LaunchContext, out string MarginWords, out string ClickyLine, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> ReviewedItems)
    {
      Kind = this.Kind;
      ObjectKey = this.ObjectKey;
      Message = this.Message;
      LaunchContext = this.LaunchContext;
      MarginWords = this.MarginWords;
      ClickyLine = this.ClickyLine;
      ReviewedItems = this.ReviewedItems;
    }

    // Token: 0x02000491 RID: 1169
    [NullableContext(0)]
    public enum ActionKind
    {
      // Token: 0x04001D55 RID: 7509
      Ask,
      // Token: 0x04001D56 RID: 7510
      Answer,
      // Token: 0x04001D57 RID: 7511
      Send,
      // Token: 0x04001D58 RID: 7512
      Review
    }
  }
}
