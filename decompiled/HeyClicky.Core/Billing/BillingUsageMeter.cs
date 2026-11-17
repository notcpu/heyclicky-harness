using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200020C RID: 524
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingUsageMeter : IEquatable<BillingUsageMeter>
  {
    // Token: 0x06001E94 RID: 7828 RVA: 0x003EED98 File Offset: 0x003EED98
    public BillingUsageMeter(string Title, string ValueText, double Progress)
    {
      this.Title = Title;
      this.ValueText = ValueText;
      this.Progress = Progress;
      base..ctor();
    }

    // Token: 0x170006C7 RID: 1735
    // (get) Token: 0x06001E95 RID: 7829 RVA: 0x003EEDB8 File Offset: 0x003EEDB8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BillingUsageMeter);
      }
    }

    // Token: 0x170006C8 RID: 1736
    // (get) Token: 0x06001E96 RID: 7830 RVA: 0x003EEDC4 File Offset: 0x003EEDC4
    // (set) Token: 0x06001E97 RID: 7831 RVA: 0x003EEDCC File Offset: 0x003EEDCC
    public string Title { get; set; }

    // Token: 0x170006C9 RID: 1737
    // (get) Token: 0x06001E98 RID: 7832 RVA: 0x003EEDD8 File Offset: 0x003EEDD8
    // (set) Token: 0x06001E99 RID: 7833 RVA: 0x003EEDE0 File Offset: 0x003EEDE0
    public string ValueText { get; set; }

    // Token: 0x170006CA RID: 1738
    // (get) Token: 0x06001E9A RID: 7834 RVA: 0x003EEDEC File Offset: 0x003EEDEC
    // (set) Token: 0x06001E9B RID: 7835 RVA: 0x003EEDF4 File Offset: 0x003EEDF4
    public double Progress { get; set; }

    // Token: 0x06001E9C RID: 7836 RVA: 0x003EEE00 File Offset: 0x003EEE00
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingUsageMeter");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E9D RID: 7837 RVA: 0x003EEE4C File Offset: 0x003EEE4C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", ValueText = ");
      builder.Append(this.ValueText);
      builder.Append(", Progress = ");
      builder.Append(this.Progress.ToString());
      return true;
    }

    // Token: 0x06001E9E RID: 7838 RVA: 0x003EEEB8 File Offset: 0x003EEEB8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BillingUsageMeter left, BillingUsageMeter right)
    {
      return !(left == right);
    }

    // Token: 0x06001E9F RID: 7839 RVA: 0x003EEEC4 File Offset: 0x003EEEC4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BillingUsageMeter left, BillingUsageMeter right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001EA0 RID: 7840 RVA: 0x003EEED8 File Offset: 0x003EEED8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ValueText>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Progress>k__BackingField);
    }

    // Token: 0x06001EA1 RID: 7841 RVA: 0x003EEF3C File Offset: 0x003EEF3C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingUsageMeter);
    }

    // Token: 0x06001EA2 RID: 7842 RVA: 0x003EEF4C File Offset: 0x003EEF4C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BillingUsageMeter other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ValueText>k__BackingField, other.<ValueText>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Progress>k__BackingField, other.<Progress>k__BackingField));
    }

    // Token: 0x06001EA4 RID: 7844 RVA: 0x003EEFC8 File Offset: 0x003EEFC8
    [CompilerGenerated]
    private BillingUsageMeter(BillingUsageMeter original)
    {
      this.Title = original.<Title>k__BackingField;
      this.ValueText = original.<ValueText>k__BackingField;
      this.Progress = original.<Progress>k__BackingField;
    }

    // Token: 0x06001EA5 RID: 7845 RVA: 0x003EEFF4 File Offset: 0x003EEFF4
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string ValueText, out double Progress)
    {
      Title = this.Title;
      ValueText = this.ValueText;
      Progress = this.Progress;
    }
  }
}
