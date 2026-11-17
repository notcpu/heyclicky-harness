using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000214 RID: 532
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingUsageSection : IEquatable<BillingUsageSection>
  {
    // Token: 0x06001F1A RID: 7962 RVA: 0x003F0DB4 File Offset: 0x003F0DB4
    public BillingUsageSection(string Title, string CountText, [Nullable(2)] string CapText, double Progress, bool IsSpent)
    {
      this.Title = Title;
      this.CountText = CountText;
      this.CapText = CapText;
      this.Progress = Progress;
      this.IsSpent = IsSpent;
      base..ctor();
    }

    // Token: 0x170006F2 RID: 1778
    // (get) Token: 0x06001F1B RID: 7963 RVA: 0x003F0DE4 File Offset: 0x003F0DE4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BillingUsageSection);
      }
    }

    // Token: 0x170006F3 RID: 1779
    // (get) Token: 0x06001F1C RID: 7964 RVA: 0x003F0DF0 File Offset: 0x003F0DF0
    // (set) Token: 0x06001F1D RID: 7965 RVA: 0x003F0DF8 File Offset: 0x003F0DF8
    public string Title { get; set; }

    // Token: 0x170006F4 RID: 1780
    // (get) Token: 0x06001F1E RID: 7966 RVA: 0x003F0E04 File Offset: 0x003F0E04
    // (set) Token: 0x06001F1F RID: 7967 RVA: 0x003F0E0C File Offset: 0x003F0E0C
    public string CountText { get; set; }

    // Token: 0x170006F5 RID: 1781
    // (get) Token: 0x06001F20 RID: 7968 RVA: 0x003F0E18 File Offset: 0x003F0E18
    // (set) Token: 0x06001F21 RID: 7969 RVA: 0x003F0E20 File Offset: 0x003F0E20
    [Nullable(2)]
    public string CapText
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170006F6 RID: 1782
    // (get) Token: 0x06001F22 RID: 7970 RVA: 0x003F0E2C File Offset: 0x003F0E2C
    // (set) Token: 0x06001F23 RID: 7971 RVA: 0x003F0E34 File Offset: 0x003F0E34
    public double Progress { get; set; }

    // Token: 0x170006F7 RID: 1783
    // (get) Token: 0x06001F24 RID: 7972 RVA: 0x003F0E40 File Offset: 0x003F0E40
    // (set) Token: 0x06001F25 RID: 7973 RVA: 0x003F0E48 File Offset: 0x003F0E48
    public bool IsSpent { get; set; }

    // Token: 0x06001F26 RID: 7974 RVA: 0x003F0E54 File Offset: 0x003F0E54
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingUsageSection");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F27 RID: 7975 RVA: 0x003F0EA0 File Offset: 0x003F0EA0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", CountText = ");
      builder.Append(this.CountText);
      builder.Append(", CapText = ");
      builder.Append(this.CapText);
      builder.Append(", Progress = ");
      builder.Append(this.Progress.ToString());
      builder.Append(", IsSpent = ");
      builder.Append(this.IsSpent.ToString());
      return true;
    }

    // Token: 0x06001F28 RID: 7976 RVA: 0x003F0F4C File Offset: 0x003F0F4C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BillingUsageSection left, BillingUsageSection right)
    {
      return !(left == right);
    }

    // Token: 0x06001F29 RID: 7977 RVA: 0x003F0F58 File Offset: 0x003F0F58
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BillingUsageSection left, BillingUsageSection right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F2A RID: 7978 RVA: 0x003F0F6C File Offset: 0x003F0F6C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CountText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CapText>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Progress>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSpent>k__BackingField);
    }

    // Token: 0x06001F2B RID: 7979 RVA: 0x003F0FFC File Offset: 0x003F0FFC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingUsageSection);
    }

    // Token: 0x06001F2C RID: 7980 RVA: 0x003F100C File Offset: 0x003F100C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BillingUsageSection other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CountText>k__BackingField, other.<CountText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CapText>k__BackingField, other.<CapText>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Progress>k__BackingField, other.<Progress>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSpent>k__BackingField, other.<IsSpent>k__BackingField));
    }

    // Token: 0x06001F2E RID: 7982 RVA: 0x003F10BC File Offset: 0x003F10BC
    [CompilerGenerated]
    private BillingUsageSection(BillingUsageSection original)
    {
      this.Title = original.<Title>k__BackingField;
      this.CountText = original.<CountText>k__BackingField;
      this.CapText = original.<CapText>k__BackingField;
      this.Progress = original.<Progress>k__BackingField;
      this.IsSpent = original.<IsSpent>k__BackingField;
    }

    // Token: 0x06001F2F RID: 7983 RVA: 0x003F110C File Offset: 0x003F110C
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string CountText, [Nullable(2)] out string CapText, out double Progress, out bool IsSpent)
    {
      Title = this.Title;
      CountText = this.CountText;
      CapText = this.CapText;
      Progress = this.Progress;
      IsSpent = this.IsSpent;
    }
  }
}
