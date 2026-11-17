using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000209 RID: 521
  public readonly struct AgentModelPricing : IEquatable<AgentModelPricing>
  {
    // Token: 0x06001E4D RID: 7757 RVA: 0x003ED35C File Offset: 0x003ED35C
    public AgentModelPricing(double InputUsdPerMillion, double CachedInputUsdPerMillion, double OutputUsdPerMillion)
    {
      this.InputUsdPerMillion = InputUsdPerMillion;
      this.CachedInputUsdPerMillion = CachedInputUsdPerMillion;
      this.OutputUsdPerMillion = OutputUsdPerMillion;
    }

    // Token: 0x170006B8 RID: 1720
    // (get) Token: 0x06001E4E RID: 7758 RVA: 0x003ED374 File Offset: 0x003ED374
    // (set) Token: 0x06001E4F RID: 7759 RVA: 0x003ED37C File Offset: 0x003ED37C
    public double InputUsdPerMillion { get; set; }

    // Token: 0x170006B9 RID: 1721
    // (get) Token: 0x06001E50 RID: 7760 RVA: 0x003ED388 File Offset: 0x003ED388
    // (set) Token: 0x06001E51 RID: 7761 RVA: 0x003ED390 File Offset: 0x003ED390
    public double CachedInputUsdPerMillion { get; set; }

    // Token: 0x170006BA RID: 1722
    // (get) Token: 0x06001E52 RID: 7762 RVA: 0x003ED39C File Offset: 0x003ED39C
    // (set) Token: 0x06001E53 RID: 7763 RVA: 0x003ED3A4 File Offset: 0x003ED3A4
    public double OutputUsdPerMillion { get; set; }

    // Token: 0x06001E54 RID: 7764 RVA: 0x003ED3B0 File Offset: 0x003ED3B0
    [NullableContext(1)]
    public static AgentModelPricing? For(string model)
    {
      string text = model.ToLowerInvariant();
      if (text.Contains("astra"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt6Astra);
      }
      if (text.Contains("gpt-6-luna"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt6Luna);
      }
      if (text.Contains("gpt-6.1-sol"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt61Sol);
      }
      if (text.Contains("gpt-6-sol"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt6Sol);
      }
      if (text.Contains("sol"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt56Sol);
      }
      if (text.Contains("terra"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt56Terra);
      }
      if (text.Contains("luna"))
      {
        return new AgentModelPricing?(AgentModelPricing.Gpt56Luna);
      }
      return null;
    }

    // Token: 0x06001E55 RID: 7765 RVA: 0x003ED478 File Offset: 0x003ED478
    public double CostUsd(long inputTokens, long cachedInputTokens, long outputTokens)
    {
      return ((double)Math.Max(0L, inputTokens - cachedInputTokens) * this.InputUsdPerMillion + (double)cachedInputTokens * this.CachedInputUsdPerMillion + (double)outputTokens * this.OutputUsdPerMillion) / 1000000.0;
    }

    // Token: 0x06001E56 RID: 7766 RVA: 0x003ED4AC File Offset: 0x003ED4AC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentModelPricing");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E57 RID: 7767 RVA: 0x003ED4F8 File Offset: 0x003ED4F8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("InputUsdPerMillion = ");
      builder.Append(this.InputUsdPerMillion.ToString());
      builder.Append(", CachedInputUsdPerMillion = ");
      builder.Append(this.CachedInputUsdPerMillion.ToString());
      builder.Append(", OutputUsdPerMillion = ");
      builder.Append(this.OutputUsdPerMillion.ToString());
      return true;
    }

    // Token: 0x06001E58 RID: 7768 RVA: 0x003ED57C File Offset: 0x003ED57C
    [CompilerGenerated]
    public static bool operator !=(AgentModelPricing left, AgentModelPricing right)
    {
      return !(left == right);
    }

    // Token: 0x06001E59 RID: 7769 RVA: 0x003ED588 File Offset: 0x003ED588
    [CompilerGenerated]
    public static bool operator ==(AgentModelPricing left, AgentModelPricing right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001E5A RID: 7770 RVA: 0x003ED594 File Offset: 0x003ED594
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<double>.Default.GetHashCode(this.<InputUsdPerMillion>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<CachedInputUsdPerMillion>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<OutputUsdPerMillion>k__BackingField);
    }

    // Token: 0x06001E5B RID: 7771 RVA: 0x003ED5D4 File Offset: 0x003ED5D4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is AgentModelPricing && this.Equals((AgentModelPricing)obj);
    }

    // Token: 0x06001E5C RID: 7772 RVA: 0x003ED5EC File Offset: 0x003ED5EC
    [CompilerGenerated]
    public bool Equals(AgentModelPricing other)
    {
      return EqualityComparer<double>.Default.Equals(this.<InputUsdPerMillion>k__BackingField, other.<InputUsdPerMillion>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<CachedInputUsdPerMillion>k__BackingField, other.<CachedInputUsdPerMillion>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<OutputUsdPerMillion>k__BackingField, other.<OutputUsdPerMillion>k__BackingField);
    }

    // Token: 0x06001E5D RID: 7773 RVA: 0x003ED644 File Offset: 0x003ED644
    [CompilerGenerated]
    public void Deconstruct(out double InputUsdPerMillion, out double CachedInputUsdPerMillion, out double OutputUsdPerMillion)
    {
      InputUsdPerMillion = this.InputUsdPerMillion;
      CachedInputUsdPerMillion = this.CachedInputUsdPerMillion;
      OutputUsdPerMillion = this.OutputUsdPerMillion;
    }

    // Token: 0x04000D01 RID: 3329
    public static readonly AgentModelPricing Gpt56Sol = new AgentModelPricing(4.0, 0.4, 20.0);

    // Token: 0x04000D02 RID: 3330
    public static readonly AgentModelPricing Gpt56Terra = new AgentModelPricing(2.0, 0.2, 12.0);

    // Token: 0x04000D03 RID: 3331
    public static readonly AgentModelPricing Gpt56Luna = new AgentModelPricing(0.2, 0.02, 1.2);

    // Token: 0x04000D04 RID: 3332
    public static readonly AgentModelPricing Gpt6Astra = new AgentModelPricing(10.0, 1.0, 50.0);

    // Token: 0x04000D05 RID: 3333
    public static readonly AgentModelPricing Gpt61Sol = new AgentModelPricing(2.0, 0.1, 10.0);

    // Token: 0x04000D06 RID: 3334
    public static readonly AgentModelPricing Gpt6Sol = new AgentModelPricing(2.0, 0.2, 10.0);

    // Token: 0x04000D07 RID: 3335
    public static readonly AgentModelPricing Gpt6Luna = new AgentModelPricing(0.1, 0.01, 0.5);

    // Token: 0x04000D08 RID: 3336
    [TupleElementNames(new string[] { "Label", "Pricing" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    public static readonly ValueTuple<string, AgentModelPricing>[] ComparisonTiers = new ValueTuple<string, AgentModelPricing>[]
    {
      new ValueTuple<string, AgentModelPricing>("Sol", AgentModelPricing.Gpt61Sol),
      new ValueTuple<string, AgentModelPricing>("Luna", AgentModelPricing.Gpt6Luna)
    };
  }
}
