using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x02000072 RID: 114
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTurnLease : IEquatable<AgentTurnLease>
  {
    // Token: 0x060008EC RID: 2284 RVA: 0x00102968 File Offset: 0x00102968
    public AgentTurnLease(string LeaseId, string TurnId, DateTimeOffset? ExpiresAt, int CreditsUsed, int IncludedCredits, double? CostUsd, double? CostLimitUsd, bool RequiresExtraEffort, [Nullable(2)] string Status)
    {
      this.LeaseId = LeaseId;
      this.TurnId = TurnId;
      this.ExpiresAt = ExpiresAt;
      this.CreditsUsed = CreditsUsed;
      this.IncludedCredits = IncludedCredits;
      this.CostUsd = CostUsd;
      this.CostLimitUsd = CostLimitUsd;
      this.RequiresExtraEffort = RequiresExtraEffort;
      this.Status = Status;
      base..ctor();
    }

    // Token: 0x17000245 RID: 581
    // (get) Token: 0x060008ED RID: 2285 RVA: 0x001029C0 File Offset: 0x001029C0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentTurnLease);
      }
    }

    // Token: 0x17000246 RID: 582
    // (get) Token: 0x060008EE RID: 2286 RVA: 0x001029CC File Offset: 0x001029CC
    // (set) Token: 0x060008EF RID: 2287 RVA: 0x001029D4 File Offset: 0x001029D4
    public string LeaseId { get; set; }

    // Token: 0x17000247 RID: 583
    // (get) Token: 0x060008F0 RID: 2288 RVA: 0x001029E0 File Offset: 0x001029E0
    // (set) Token: 0x060008F1 RID: 2289 RVA: 0x001029E8 File Offset: 0x001029E8
    public string TurnId { get; set; }

    // Token: 0x17000248 RID: 584
    // (get) Token: 0x060008F2 RID: 2290 RVA: 0x001029F4 File Offset: 0x001029F4
    // (set) Token: 0x060008F3 RID: 2291 RVA: 0x001029FC File Offset: 0x001029FC
    public DateTimeOffset? ExpiresAt { get; set; }

    // Token: 0x17000249 RID: 585
    // (get) Token: 0x060008F4 RID: 2292 RVA: 0x00102A08 File Offset: 0x00102A08
    // (set) Token: 0x060008F5 RID: 2293 RVA: 0x00102A10 File Offset: 0x00102A10
    public int CreditsUsed { get; set; }

    // Token: 0x1700024A RID: 586
    // (get) Token: 0x060008F6 RID: 2294 RVA: 0x00102A1C File Offset: 0x00102A1C
    // (set) Token: 0x060008F7 RID: 2295 RVA: 0x00102A24 File Offset: 0x00102A24
    public int IncludedCredits { get; set; }

    // Token: 0x1700024B RID: 587
    // (get) Token: 0x060008F8 RID: 2296 RVA: 0x00102A30 File Offset: 0x00102A30
    // (set) Token: 0x060008F9 RID: 2297 RVA: 0x00102A38 File Offset: 0x00102A38
    public double? CostUsd { get; set; }

    // Token: 0x1700024C RID: 588
    // (get) Token: 0x060008FA RID: 2298 RVA: 0x00102A44 File Offset: 0x00102A44
    // (set) Token: 0x060008FB RID: 2299 RVA: 0x00102A4C File Offset: 0x00102A4C
    public double? CostLimitUsd { get; set; }

    // Token: 0x1700024D RID: 589
    // (get) Token: 0x060008FC RID: 2300 RVA: 0x00102A58 File Offset: 0x00102A58
    // (set) Token: 0x060008FD RID: 2301 RVA: 0x00102A60 File Offset: 0x00102A60
    public bool RequiresExtraEffort { get; set; }

    // Token: 0x1700024E RID: 590
    // (get) Token: 0x060008FE RID: 2302 RVA: 0x00102A6C File Offset: 0x00102A6C
    // (set) Token: 0x060008FF RID: 2303 RVA: 0x00102A74 File Offset: 0x00102A74
    [Nullable(2)]
    public string Status
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000900 RID: 2304 RVA: 0x00102A80 File Offset: 0x00102A80
    [NullableContext(2)]
    internal static AgentTurnLease Parse(JsonObject o)
    {
      if (o != null)
      {
        JsonNode jsonNode = o["lease_id"];
        string text = ((jsonNode != null) ? jsonNode.ToString() : null);
        if (text != null && text.Length > 0)
        {
          string text2 = text;
          JsonNode jsonNode2 = o["turn_id"];
          string text3 = ((jsonNode2 != null) ? jsonNode2.ToString() : null) ?? "";
          JsonNode jsonNode3 = o["expires_at"];
          DateTimeOffset dateTimeOffset2;
          DateTimeOffset? dateTimeOffset = (DateTimeOffset.TryParse((jsonNode3 != null) ? jsonNode3.ToString() : null, out dateTimeOffset2) ? new DateTimeOffset?(dateTimeOffset2) : null);
          int num = (int)AgentTurnLease.<Parse>g__Num|39_0(o["credits_used"]).GetValueOrDefault();
          int num2 = (int)AgentTurnLease.<Parse>g__Num|39_0(o["included_credits"]).GetValueOrDefault(1.0);
          double? num3 = AgentTurnLease.<Parse>g__Num|39_0(o["cost_usd"]);
          double? num4 = AgentTurnLease.<Parse>g__Num|39_0(o["cost_limit_usd"]);
          JsonValue jsonValue = o["requires_extra_effort"] as JsonValue;
          bool flag2;
          bool flag = jsonValue != null && jsonValue.TryGetValue<bool>(out flag2) && flag2;
          JsonNode jsonNode4 = o["status"];
          return new AgentTurnLease(text2, text3, dateTimeOffset, num, num2, num3, num4, flag, (jsonNode4 != null) ? jsonNode4.ToString() : null);
        }
      }
      return null;
    }

    // Token: 0x06000901 RID: 2305 RVA: 0x00102BA4 File Offset: 0x00102BA4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTurnLease");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000902 RID: 2306 RVA: 0x00102BF0 File Offset: 0x00102BF0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("LeaseId = ");
      builder.Append(this.LeaseId);
      builder.Append(", TurnId = ");
      builder.Append(this.TurnId);
      builder.Append(", ExpiresAt = ");
      builder.Append(this.ExpiresAt.ToString());
      builder.Append(", CreditsUsed = ");
      builder.Append(this.CreditsUsed.ToString());
      builder.Append(", IncludedCredits = ");
      builder.Append(this.IncludedCredits.ToString());
      builder.Append(", CostUsd = ");
      builder.Append(this.CostUsd.ToString());
      builder.Append(", CostLimitUsd = ");
      builder.Append(this.CostLimitUsd.ToString());
      builder.Append(", RequiresExtraEffort = ");
      builder.Append(this.RequiresExtraEffort.ToString());
      builder.Append(", Status = ");
      builder.Append(this.Status);
      return true;
    }

    // Token: 0x06000903 RID: 2307 RVA: 0x00102D38 File Offset: 0x00102D38
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentTurnLease left, AgentTurnLease right)
    {
      return !(left == right);
    }

    // Token: 0x06000904 RID: 2308 RVA: 0x00102D44 File Offset: 0x00102D44
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentTurnLease left, AgentTurnLease right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000905 RID: 2309 RVA: 0x00102D58 File Offset: 0x00102D58
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LeaseId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TurnId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<ExpiresAt>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CreditsUsed>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<IncludedCredits>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<CostUsd>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<CostLimitUsd>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<RequiresExtraEffort>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField);
    }

    // Token: 0x06000906 RID: 2310 RVA: 0x00102E44 File Offset: 0x00102E44
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTurnLease);
    }

    // Token: 0x06000907 RID: 2311 RVA: 0x00102E54 File Offset: 0x00102E54
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentTurnLease other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<LeaseId>k__BackingField, other.<LeaseId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TurnId>k__BackingField, other.<TurnId>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<ExpiresAt>k__BackingField, other.<ExpiresAt>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CreditsUsed>k__BackingField, other.<CreditsUsed>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<IncludedCredits>k__BackingField, other.<IncludedCredits>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<CostUsd>k__BackingField, other.<CostUsd>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<CostLimitUsd>k__BackingField, other.<CostLimitUsd>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<RequiresExtraEffort>k__BackingField, other.<RequiresExtraEffort>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField));
    }

    // Token: 0x06000909 RID: 2313 RVA: 0x00102F70 File Offset: 0x00102F70
    [CompilerGenerated]
    private AgentTurnLease(AgentTurnLease original)
    {
      this.LeaseId = original.<LeaseId>k__BackingField;
      this.TurnId = original.<TurnId>k__BackingField;
      this.ExpiresAt = original.<ExpiresAt>k__BackingField;
      this.CreditsUsed = original.<CreditsUsed>k__BackingField;
      this.IncludedCredits = original.<IncludedCredits>k__BackingField;
      this.CostUsd = original.<CostUsd>k__BackingField;
      this.CostLimitUsd = original.<CostLimitUsd>k__BackingField;
      this.RequiresExtraEffort = original.<RequiresExtraEffort>k__BackingField;
      this.Status = original.<Status>k__BackingField;
    }

    // Token: 0x0600090A RID: 2314 RVA: 0x00102FF0 File Offset: 0x00102FF0
    [CompilerGenerated]
    public void Deconstruct(out string LeaseId, out string TurnId, out DateTimeOffset? ExpiresAt, out int CreditsUsed, out int IncludedCredits, out double? CostUsd, out double? CostLimitUsd, out bool RequiresExtraEffort, [Nullable(2)] out string Status)
    {
      LeaseId = this.LeaseId;
      TurnId = this.TurnId;
      ExpiresAt = this.ExpiresAt;
      CreditsUsed = this.CreditsUsed;
      IncludedCredits = this.IncludedCredits;
      CostUsd = this.CostUsd;
      CostLimitUsd = this.CostLimitUsd;
      RequiresExtraEffort = this.RequiresExtraEffort;
      Status = this.Status;
    }

    // Token: 0x0600090B RID: 2315 RVA: 0x00103058 File Offset: 0x00103058
    [NullableContext(2)]
    [CompilerGenerated]
    internal static double? <Parse>g__Num|39_0(JsonNode n)
    {
      if (n == null)
      {
        return null;
      }
      double num;
      if (!double.TryParse(n.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
      {
        return null;
      }
      return new double?(num);
    }
  }
}
