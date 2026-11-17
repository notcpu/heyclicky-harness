using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Platform
{
  // Token: 0x020000E5 RID: 229
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class TextInsertOutcome : IEquatable<TextInsertOutcome>
  {
    // Token: 0x06000EC2 RID: 3778 RVA: 0x0038C2F8 File Offset: 0x0038C2F8
    public TextInsertOutcome([Nullable(1)] string Status, string Reason = null, string App = null)
    {
      this.Status = Status;
      this.Reason = Reason;
      this.App = App;
      base..ctor();
    }

    // Token: 0x17000357 RID: 855
    // (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0038C318 File Offset: 0x0038C318
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(TextInsertOutcome);
      }
    }

    // Token: 0x17000358 RID: 856
    // (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0038C324 File Offset: 0x0038C324
    // (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0038C32C File Offset: 0x0038C32C
    [Nullable(1)]
    public string Status
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000359 RID: 857
    // (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0038C338 File Offset: 0x0038C338
    // (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0038C340 File Offset: 0x0038C340
    public string Reason { get; set; }

    // Token: 0x1700035A RID: 858
    // (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0038C34C File Offset: 0x0038C34C
    // (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0038C354 File Offset: 0x0038C354
    public string App { get; set; }

    // Token: 0x06000ECA RID: 3786 RVA: 0x0038C360 File Offset: 0x0038C360
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("TextInsertOutcome");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000ECB RID: 3787 RVA: 0x0038C3AC File Offset: 0x0038C3AC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Status = ");
      builder.Append(this.Status);
      builder.Append(", Reason = ");
      builder.Append(this.Reason);
      builder.Append(", App = ");
      builder.Append(this.App);
      return true;
    }

    // Token: 0x06000ECC RID: 3788 RVA: 0x0038C40C File Offset: 0x0038C40C
    [CompilerGenerated]
    public static bool operator !=(TextInsertOutcome left, TextInsertOutcome right)
    {
      return !(left == right);
    }

    // Token: 0x06000ECD RID: 3789 RVA: 0x0038C418 File Offset: 0x0038C418
    [CompilerGenerated]
    public static bool operator ==(TextInsertOutcome left, TextInsertOutcome right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000ECE RID: 3790 RVA: 0x0038C42C File Offset: 0x0038C42C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reason>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<App>k__BackingField);
    }

    // Token: 0x06000ECF RID: 3791 RVA: 0x0038C490 File Offset: 0x0038C490
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as TextInsertOutcome);
    }

    // Token: 0x06000ED0 RID: 3792 RVA: 0x0038C4A0 File Offset: 0x0038C4A0
    [CompilerGenerated]
    public bool Equals(TextInsertOutcome other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Reason>k__BackingField, other.<Reason>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<App>k__BackingField, other.<App>k__BackingField));
    }

    // Token: 0x06000ED2 RID: 3794 RVA: 0x0038C51C File Offset: 0x0038C51C
    [CompilerGenerated]
    private TextInsertOutcome([Nullable(1)] TextInsertOutcome original)
    {
      this.Status = original.<Status>k__BackingField;
      this.Reason = original.<Reason>k__BackingField;
      this.App = original.<App>k__BackingField;
    }

    // Token: 0x06000ED3 RID: 3795 RVA: 0x0038C548 File Offset: 0x0038C548
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Status, out string Reason, out string App)
    {
      Status = this.Status;
      Reason = this.Reason;
      App = this.App;
    }
  }
}
