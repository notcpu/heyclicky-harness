using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000124 RID: 292
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkSentAction : IEquatable<ClickyInkSentAction>
  {
    // Token: 0x06001315 RID: 4885 RVA: 0x003A594C File Offset: 0x003A594C
    public ClickyInkSentAction(ClickyInkNoteAction.ActionKind Kind, string ObjectKey, string MarginWords, DateTimeOffset SentAt, bool WasSentWhileRunning, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> ReviewedItems)
    {
      this.Kind = Kind;
      this.ObjectKey = ObjectKey;
      this.MarginWords = MarginWords;
      this.SentAt = SentAt;
      this.WasSentWhileRunning = WasSentWhileRunning;
      this.ReviewedItems = ReviewedItems;
      base..ctor();
    }

    // Token: 0x17000476 RID: 1142
    // (get) Token: 0x06001316 RID: 4886 RVA: 0x003A5984 File Offset: 0x003A5984
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkSentAction);
      }
    }

    // Token: 0x17000477 RID: 1143
    // (get) Token: 0x06001317 RID: 4887 RVA: 0x003A5990 File Offset: 0x003A5990
    // (set) Token: 0x06001318 RID: 4888 RVA: 0x003A5998 File Offset: 0x003A5998
    public ClickyInkNoteAction.ActionKind Kind { get; set; }

    // Token: 0x17000478 RID: 1144
    // (get) Token: 0x06001319 RID: 4889 RVA: 0x003A59A4 File Offset: 0x003A59A4
    // (set) Token: 0x0600131A RID: 4890 RVA: 0x003A59AC File Offset: 0x003A59AC
    public string ObjectKey { get; set; }

    // Token: 0x17000479 RID: 1145
    // (get) Token: 0x0600131B RID: 4891 RVA: 0x003A59B8 File Offset: 0x003A59B8
    // (set) Token: 0x0600131C RID: 4892 RVA: 0x003A59C0 File Offset: 0x003A59C0
    public string MarginWords { get; set; }

    // Token: 0x1700047A RID: 1146
    // (get) Token: 0x0600131D RID: 4893 RVA: 0x003A59CC File Offset: 0x003A59CC
    // (set) Token: 0x0600131E RID: 4894 RVA: 0x003A59D4 File Offset: 0x003A59D4
    public DateTimeOffset SentAt { get; set; }

    // Token: 0x1700047B RID: 1147
    // (get) Token: 0x0600131F RID: 4895 RVA: 0x003A59E0 File Offset: 0x003A59E0
    // (set) Token: 0x06001320 RID: 4896 RVA: 0x003A59E8 File Offset: 0x003A59E8
    public bool WasSentWhileRunning { get; set; }

    // Token: 0x1700047C RID: 1148
    // (get) Token: 0x06001321 RID: 4897 RVA: 0x003A59F4 File Offset: 0x003A59F4
    // (set) Token: 0x06001322 RID: 4898 RVA: 0x003A59FC File Offset: 0x003A59FC
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> ReviewedItems
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06001323 RID: 4899 RVA: 0x003A5A08 File Offset: 0x003A5A08
    public ClickyInkSentAction(ClickyInkNoteAction action, DateTimeOffset sentAt, bool wasSentWhileRunning)
      : this(action.Kind, action.ObjectKey, action.MarginWords, sentAt, wasSentWhileRunning, action.ReviewedItems)
    {
    }

    // Token: 0x06001324 RID: 4900 RVA: 0x003A5A2C File Offset: 0x003A5A2C
    [NullableContext(2)]
    public bool Equals(ClickyInkSentAction other)
    {
      return other != null && this.Kind == other.Kind && this.ObjectKey == other.ObjectKey && this.MarginWords == other.MarginWords && this.SentAt == other.SentAt && this.WasSentWhileRunning == other.WasSentWhileRunning && ClickyInkNoteAction.SameItems(this.ReviewedItems, other.ReviewedItems);
    }

    // Token: 0x06001325 RID: 4901 RVA: 0x003A5AA4 File Offset: 0x003A5AA4
    public override int GetHashCode()
    {
      return HashCode.Combine<ClickyInkNoteAction.ActionKind, string, DateTimeOffset>(this.Kind, this.ObjectKey, this.SentAt);
    }

    // Token: 0x06001326 RID: 4902 RVA: 0x003A5AC0 File Offset: 0x003A5AC0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkSentAction");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001327 RID: 4903 RVA: 0x003A5B0C File Offset: 0x003A5B0C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", ObjectKey = ");
      builder.Append(this.ObjectKey);
      builder.Append(", MarginWords = ");
      builder.Append(this.MarginWords);
      builder.Append(", SentAt = ");
      builder.Append(this.SentAt.ToString());
      builder.Append(", WasSentWhileRunning = ");
      builder.Append(this.WasSentWhileRunning.ToString());
      builder.Append(", ReviewedItems = ");
      builder.Append(this.ReviewedItems);
      return true;
    }

    // Token: 0x06001328 RID: 4904 RVA: 0x003A5BE0 File Offset: 0x003A5BE0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkSentAction left, ClickyInkSentAction right)
    {
      return !(left == right);
    }

    // Token: 0x06001329 RID: 4905 RVA: 0x003A5BEC File Offset: 0x003A5BEC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkSentAction left, ClickyInkSentAction right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600132A RID: 4906 RVA: 0x003A5C00 File Offset: 0x003A5C00
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkSentAction);
    }

    // Token: 0x0600132C RID: 4908 RVA: 0x003A5C18 File Offset: 0x003A5C18
    [CompilerGenerated]
    private ClickyInkSentAction(ClickyInkSentAction original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.ObjectKey = original.<ObjectKey>k__BackingField;
      this.MarginWords = original.<MarginWords>k__BackingField;
      this.SentAt = original.<SentAt>k__BackingField;
      this.WasSentWhileRunning = original.<WasSentWhileRunning>k__BackingField;
      this.ReviewedItems = original.<ReviewedItems>k__BackingField;
    }

    // Token: 0x0600132D RID: 4909 RVA: 0x003A5C74 File Offset: 0x003A5C74
    [CompilerGenerated]
    public void Deconstruct(out ClickyInkNoteAction.ActionKind Kind, out string ObjectKey, out string MarginWords, out DateTimeOffset SentAt, out bool WasSentWhileRunning, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> ReviewedItems)
    {
      Kind = this.Kind;
      ObjectKey = this.ObjectKey;
      MarginWords = this.MarginWords;
      SentAt = this.SentAt;
      WasSentWhileRunning = this.WasSentWhileRunning;
      ReviewedItems = this.ReviewedItems;
    }
  }
}
