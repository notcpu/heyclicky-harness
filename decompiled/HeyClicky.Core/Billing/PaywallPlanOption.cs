using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000216 RID: 534
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallPlanOption : IEquatable<PaywallPlanOption>
  {
    // Token: 0x06001F4B RID: 8011 RVA: 0x003F1764 File Offset: 0x003F1764
    public PaywallPlanOption(string Key, string Name, string PriceDisplay, int AgentsCap, string Tagline, string CtaLabel, bool Recommended, [Nullable(2)] string YearlyPriceDisplay, [Nullable(2)] string YearlyBilledSummary)
    {
      this.Key = Key;
      this.Name = Name;
      this.PriceDisplay = PriceDisplay;
      this.AgentsCap = AgentsCap;
      this.Tagline = Tagline;
      this.CtaLabel = CtaLabel;
      this.Recommended = Recommended;
      this.YearlyPriceDisplay = YearlyPriceDisplay;
      this.YearlyBilledSummary = YearlyBilledSummary;
      base..ctor();
    }

    // Token: 0x170006FF RID: 1791
    // (get) Token: 0x06001F4C RID: 8012 RVA: 0x003F17BC File Offset: 0x003F17BC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaywallPlanOption);
      }
    }

    // Token: 0x17000700 RID: 1792
    // (get) Token: 0x06001F4D RID: 8013 RVA: 0x003F17C8 File Offset: 0x003F17C8
    // (set) Token: 0x06001F4E RID: 8014 RVA: 0x003F17D0 File Offset: 0x003F17D0
    public string Key { get; set; }

    // Token: 0x17000701 RID: 1793
    // (get) Token: 0x06001F4F RID: 8015 RVA: 0x003F17DC File Offset: 0x003F17DC
    // (set) Token: 0x06001F50 RID: 8016 RVA: 0x003F17E4 File Offset: 0x003F17E4
    public string Name { get; set; }

    // Token: 0x17000702 RID: 1794
    // (get) Token: 0x06001F51 RID: 8017 RVA: 0x003F17F0 File Offset: 0x003F17F0
    // (set) Token: 0x06001F52 RID: 8018 RVA: 0x003F17F8 File Offset: 0x003F17F8
    public string PriceDisplay { get; set; }

    // Token: 0x17000703 RID: 1795
    // (get) Token: 0x06001F53 RID: 8019 RVA: 0x003F1804 File Offset: 0x003F1804
    // (set) Token: 0x06001F54 RID: 8020 RVA: 0x003F180C File Offset: 0x003F180C
    public int AgentsCap { get; set; }

    // Token: 0x17000704 RID: 1796
    // (get) Token: 0x06001F55 RID: 8021 RVA: 0x003F1818 File Offset: 0x003F1818
    // (set) Token: 0x06001F56 RID: 8022 RVA: 0x003F1820 File Offset: 0x003F1820
    public string Tagline { get; set; }

    // Token: 0x17000705 RID: 1797
    // (get) Token: 0x06001F57 RID: 8023 RVA: 0x003F182C File Offset: 0x003F182C
    // (set) Token: 0x06001F58 RID: 8024 RVA: 0x003F1834 File Offset: 0x003F1834
    public string CtaLabel { get; set; }

    // Token: 0x17000706 RID: 1798
    // (get) Token: 0x06001F59 RID: 8025 RVA: 0x003F1840 File Offset: 0x003F1840
    // (set) Token: 0x06001F5A RID: 8026 RVA: 0x003F1848 File Offset: 0x003F1848
    public bool Recommended { get; set; }

    // Token: 0x17000707 RID: 1799
    // (get) Token: 0x06001F5B RID: 8027 RVA: 0x003F1854 File Offset: 0x003F1854
    // (set) Token: 0x06001F5C RID: 8028 RVA: 0x003F185C File Offset: 0x003F185C
    [Nullable(2)]
    public string YearlyPriceDisplay
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000708 RID: 1800
    // (get) Token: 0x06001F5D RID: 8029 RVA: 0x003F1868 File Offset: 0x003F1868
    // (set) Token: 0x06001F5E RID: 8030 RVA: 0x003F1870 File Offset: 0x003F1870
    [Nullable(2)]
    public string YearlyBilledSummary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000709 RID: 1801
    // (get) Token: 0x06001F5F RID: 8031 RVA: 0x003F187C File Offset: 0x003F187C
    [JsonIgnore]
    public bool SupportsYearly
    {
      get
      {
        return this.YearlyPriceDisplay != null;
      }
    }

    // Token: 0x06001F60 RID: 8032 RVA: 0x003F188C File Offset: 0x003F188C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaywallPlanOption");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F61 RID: 8033 RVA: 0x003F18D8 File Offset: 0x003F18D8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Key = ");
      builder.Append(this.Key);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", PriceDisplay = ");
      builder.Append(this.PriceDisplay);
      builder.Append(", AgentsCap = ");
      builder.Append(this.AgentsCap.ToString());
      builder.Append(", Tagline = ");
      builder.Append(this.Tagline);
      builder.Append(", CtaLabel = ");
      builder.Append(this.CtaLabel);
      builder.Append(", Recommended = ");
      builder.Append(this.Recommended.ToString());
      builder.Append(", YearlyPriceDisplay = ");
      builder.Append(this.YearlyPriceDisplay);
      builder.Append(", YearlyBilledSummary = ");
      builder.Append(this.YearlyBilledSummary);
      builder.Append(", SupportsYearly = ");
      builder.Append(this.SupportsYearly.ToString());
      return true;
    }

    // Token: 0x06001F62 RID: 8034 RVA: 0x003F1A10 File Offset: 0x003F1A10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaywallPlanOption left, PaywallPlanOption right)
    {
      return !(left == right);
    }

    // Token: 0x06001F63 RID: 8035 RVA: 0x003F1A1C File Offset: 0x003F1A1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaywallPlanOption left, PaywallPlanOption right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F64 RID: 8036 RVA: 0x003F1A30 File Offset: 0x003F1A30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PriceDisplay>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<AgentsCap>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Tagline>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CtaLabel>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Recommended>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<YearlyPriceDisplay>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<YearlyBilledSummary>k__BackingField);
    }

    // Token: 0x06001F65 RID: 8037 RVA: 0x003F1B1C File Offset: 0x003F1B1C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaywallPlanOption);
    }

    // Token: 0x06001F66 RID: 8038 RVA: 0x003F1B2C File Offset: 0x003F1B2C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaywallPlanOption other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PriceDisplay>k__BackingField, other.<PriceDisplay>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<AgentsCap>k__BackingField, other.<AgentsCap>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Tagline>k__BackingField, other.<Tagline>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CtaLabel>k__BackingField, other.<CtaLabel>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Recommended>k__BackingField, other.<Recommended>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<YearlyPriceDisplay>k__BackingField, other.<YearlyPriceDisplay>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<YearlyBilledSummary>k__BackingField, other.<YearlyBilledSummary>k__BackingField));
    }

    // Token: 0x06001F68 RID: 8040 RVA: 0x003F1C48 File Offset: 0x003F1C48
    [CompilerGenerated]
    private PaywallPlanOption(PaywallPlanOption original)
    {
      this.Key = original.<Key>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.PriceDisplay = original.<PriceDisplay>k__BackingField;
      this.AgentsCap = original.<AgentsCap>k__BackingField;
      this.Tagline = original.<Tagline>k__BackingField;
      this.CtaLabel = original.<CtaLabel>k__BackingField;
      this.Recommended = original.<Recommended>k__BackingField;
      this.YearlyPriceDisplay = original.<YearlyPriceDisplay>k__BackingField;
      this.YearlyBilledSummary = original.<YearlyBilledSummary>k__BackingField;
    }

    // Token: 0x06001F69 RID: 8041 RVA: 0x003F1CC8 File Offset: 0x003F1CC8
    [CompilerGenerated]
    public void Deconstruct(out string Key, out string Name, out string PriceDisplay, out int AgentsCap, out string Tagline, out string CtaLabel, out bool Recommended, [Nullable(2)] out string YearlyPriceDisplay, [Nullable(2)] out string YearlyBilledSummary)
    {
      Key = this.Key;
      Name = this.Name;
      PriceDisplay = this.PriceDisplay;
      AgentsCap = this.AgentsCap;
      Tagline = this.Tagline;
      CtaLabel = this.CtaLabel;
      Recommended = this.Recommended;
      YearlyPriceDisplay = this.YearlyPriceDisplay;
      YearlyBilledSummary = this.YearlyBilledSummary;
    }
  }
}
