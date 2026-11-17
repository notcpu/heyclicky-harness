using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000217 RID: 535
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaywallPayload : IEquatable<PaywallPayload>
  {
    // Token: 0x06001F6A RID: 8042 RVA: 0x003F1D24 File Offset: 0x003F1D24
    public PaywallPayload([Nullable(2)] string Reason, string Surface, string Title, string Body, string CtaLabel, string PriceDisplay, int CurrentUsage, int Limit, [Nullable(2)] string ResetAt, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<PaywallPlanOption> Plans, [Nullable(2)] string YearlySavingsLabel, [Nullable(2)] string DefaultInterval)
    {
      this.Reason = Reason;
      this.Surface = Surface;
      this.Title = Title;
      this.Body = Body;
      this.CtaLabel = CtaLabel;
      this.PriceDisplay = PriceDisplay;
      this.CurrentUsage = CurrentUsage;
      this.Limit = Limit;
      this.ResetAt = ResetAt;
      this.Plans = Plans;
      this.YearlySavingsLabel = YearlySavingsLabel;
      this.DefaultInterval = DefaultInterval;
      base..ctor();
    }

    // Token: 0x1700070A RID: 1802
    // (get) Token: 0x06001F6B RID: 8043 RVA: 0x003F1D94 File Offset: 0x003F1D94
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaywallPayload);
      }
    }

    // Token: 0x1700070B RID: 1803
    // (get) Token: 0x06001F6C RID: 8044 RVA: 0x003F1DA0 File Offset: 0x003F1DA0
    // (set) Token: 0x06001F6D RID: 8045 RVA: 0x003F1DA8 File Offset: 0x003F1DA8
    [Nullable(2)]
    public string Reason
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700070C RID: 1804
    // (get) Token: 0x06001F6E RID: 8046 RVA: 0x003F1DB4 File Offset: 0x003F1DB4
    // (set) Token: 0x06001F6F RID: 8047 RVA: 0x003F1DBC File Offset: 0x003F1DBC
    public string Surface { get; set; }

    // Token: 0x1700070D RID: 1805
    // (get) Token: 0x06001F70 RID: 8048 RVA: 0x003F1DC8 File Offset: 0x003F1DC8
    // (set) Token: 0x06001F71 RID: 8049 RVA: 0x003F1DD0 File Offset: 0x003F1DD0
    public string Title { get; set; }

    // Token: 0x1700070E RID: 1806
    // (get) Token: 0x06001F72 RID: 8050 RVA: 0x003F1DDC File Offset: 0x003F1DDC
    // (set) Token: 0x06001F73 RID: 8051 RVA: 0x003F1DE4 File Offset: 0x003F1DE4
    public string Body { get; set; }

    // Token: 0x1700070F RID: 1807
    // (get) Token: 0x06001F74 RID: 8052 RVA: 0x003F1DF0 File Offset: 0x003F1DF0
    // (set) Token: 0x06001F75 RID: 8053 RVA: 0x003F1DF8 File Offset: 0x003F1DF8
    public string CtaLabel { get; set; }

    // Token: 0x17000710 RID: 1808
    // (get) Token: 0x06001F76 RID: 8054 RVA: 0x003F1E04 File Offset: 0x003F1E04
    // (set) Token: 0x06001F77 RID: 8055 RVA: 0x003F1E0C File Offset: 0x003F1E0C
    public string PriceDisplay { get; set; }

    // Token: 0x17000711 RID: 1809
    // (get) Token: 0x06001F78 RID: 8056 RVA: 0x003F1E18 File Offset: 0x003F1E18
    // (set) Token: 0x06001F79 RID: 8057 RVA: 0x003F1E20 File Offset: 0x003F1E20
    public int CurrentUsage { get; set; }

    // Token: 0x17000712 RID: 1810
    // (get) Token: 0x06001F7A RID: 8058 RVA: 0x003F1E2C File Offset: 0x003F1E2C
    // (set) Token: 0x06001F7B RID: 8059 RVA: 0x003F1E34 File Offset: 0x003F1E34
    public int Limit { get; set; }

    // Token: 0x17000713 RID: 1811
    // (get) Token: 0x06001F7C RID: 8060 RVA: 0x003F1E40 File Offset: 0x003F1E40
    // (set) Token: 0x06001F7D RID: 8061 RVA: 0x003F1E48 File Offset: 0x003F1E48
    [Nullable(2)]
    public string ResetAt
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000714 RID: 1812
    // (get) Token: 0x06001F7E RID: 8062 RVA: 0x003F1E54 File Offset: 0x003F1E54
    // (set) Token: 0x06001F7F RID: 8063 RVA: 0x003F1E5C File Offset: 0x003F1E5C
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<PaywallPlanOption> Plans
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000715 RID: 1813
    // (get) Token: 0x06001F80 RID: 8064 RVA: 0x003F1E68 File Offset: 0x003F1E68
    // (set) Token: 0x06001F81 RID: 8065 RVA: 0x003F1E70 File Offset: 0x003F1E70
    [Nullable(2)]
    public string YearlySavingsLabel
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000716 RID: 1814
    // (get) Token: 0x06001F82 RID: 8066 RVA: 0x003F1E7C File Offset: 0x003F1E7C
    // (set) Token: 0x06001F83 RID: 8067 RVA: 0x003F1E84 File Offset: 0x003F1E84
    [Nullable(2)]
    public string DefaultInterval
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000717 RID: 1815
    // (get) Token: 0x06001F84 RID: 8068 RVA: 0x003F1E90 File Offset: 0x003F1E90
    [JsonIgnore]
    public bool IsPaymentProblem
    {
      get
      {
        return this.Reason == "payment_problem";
      }
    }

    // Token: 0x17000718 RID: 1816
    // (get) Token: 0x06001F85 RID: 8069 RVA: 0x003F1EA4 File Offset: 0x003F1EA4
    [JsonIgnore]
    public IReadOnlyList<PaywallPlanOption> UpgradeOptions
    {
      get
      {
        return this.Plans ?? Array.Empty<PaywallPlanOption>();
      }
    }

    // Token: 0x17000719 RID: 1817
    // (get) Token: 0x06001F86 RID: 8070 RVA: 0x003F1EB8 File Offset: 0x003F1EB8
    [JsonIgnore]
    public bool SupportsYearly
    {
      get
      {
        return this.UpgradeOptions.Any<PaywallPlanOption>((PaywallPlanOption p) => p.SupportsYearly);
      }
    }

    // Token: 0x06001F87 RID: 8071 RVA: 0x003F1EE4 File Offset: 0x003F1EE4
    [NullableContext(0)]
    [return: Nullable(2)]
    public static PaywallPayload DecodeFromBody(ReadOnlySpan<byte> body)
    {
      PaywallPayload paywallPayload;
      JsonElement jsonElement;
      if (!PaywallPayload.TryDecode(body, out paywallPayload, out jsonElement))
      {
        return null;
      }
      return paywallPayload;
    }

    // Token: 0x06001F88 RID: 8072 RVA: 0x003F1F00 File Offset: 0x003F1F00
    [NullableContext(0)]
    public static bool TryDecode(ReadOnlySpan<byte> body, [Nullable(2)] out PaywallPayload payload, out JsonElement raw)
    {
      payload = null;
      raw = default(JsonElement);
      bool flag;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(body.ToArray(), default(JsonDocumentOptions)))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          JsonElement jsonElement;
          if (rootElement.ValueKind != JsonValueKind.Object || !rootElement.TryGetProperty("error", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String || jsonElement.GetString() != "paywall")
          {
            flag = false;
          }
          else
          {
            payload = rootElement.Deserialize<PaywallPayload>(CoreJsonContext.Default.PaywallPayload);
            if (payload == null)
            {
              flag = false;
            }
            else
            {
              raw = rootElement.Clone();
              flag = true;
            }
          }
        }
      }
      catch (JsonException)
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001F89 RID: 8073 RVA: 0x003F1FD0 File Offset: 0x003F1FD0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaywallPayload");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F8A RID: 8074 RVA: 0x003F201C File Offset: 0x003F201C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Reason = ");
      builder.Append(this.Reason);
      builder.Append(", Surface = ");
      builder.Append(this.Surface);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Body = ");
      builder.Append(this.Body);
      builder.Append(", CtaLabel = ");
      builder.Append(this.CtaLabel);
      builder.Append(", PriceDisplay = ");
      builder.Append(this.PriceDisplay);
      builder.Append(", CurrentUsage = ");
      builder.Append(this.CurrentUsage.ToString());
      builder.Append(", Limit = ");
      builder.Append(this.Limit.ToString());
      builder.Append(", ResetAt = ");
      builder.Append(this.ResetAt);
      builder.Append(", Plans = ");
      builder.Append(this.Plans);
      builder.Append(", YearlySavingsLabel = ");
      builder.Append(this.YearlySavingsLabel);
      builder.Append(", DefaultInterval = ");
      builder.Append(this.DefaultInterval);
      builder.Append(", IsPaymentProblem = ");
      builder.Append(this.IsPaymentProblem.ToString());
      builder.Append(", UpgradeOptions = ");
      builder.Append(this.UpgradeOptions);
      builder.Append(", SupportsYearly = ");
      builder.Append(this.SupportsYearly.ToString());
      return true;
    }

    // Token: 0x06001F8B RID: 8075 RVA: 0x003F21E0 File Offset: 0x003F21E0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaywallPayload left, PaywallPayload right)
    {
      return !(left == right);
    }

    // Token: 0x06001F8C RID: 8076 RVA: 0x003F21EC File Offset: 0x003F21EC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaywallPayload left, PaywallPayload right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F8D RID: 8077 RVA: 0x003F2200 File Offset: 0x003F2200
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Reason>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Surface>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Body>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CtaLabel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PriceDisplay>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CurrentUsage>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Limit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResetAt>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<PaywallPlanOption>>.Default.GetHashCode(this.<Plans>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<YearlySavingsLabel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DefaultInterval>k__BackingField);
    }

    // Token: 0x06001F8E RID: 8078 RVA: 0x003F2334 File Offset: 0x003F2334
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaywallPayload);
    }

    // Token: 0x06001F8F RID: 8079 RVA: 0x003F2344 File Offset: 0x003F2344
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaywallPayload other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Reason>k__BackingField, other.<Reason>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Surface>k__BackingField, other.<Surface>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CtaLabel>k__BackingField, other.<CtaLabel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PriceDisplay>k__BackingField, other.<PriceDisplay>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CurrentUsage>k__BackingField, other.<CurrentUsage>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Limit>k__BackingField, other.<Limit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ResetAt>k__BackingField, other.<ResetAt>k__BackingField) && EqualityComparer<IReadOnlyList<PaywallPlanOption>>.Default.Equals(this.<Plans>k__BackingField, other.<Plans>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<YearlySavingsLabel>k__BackingField, other.<YearlySavingsLabel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DefaultInterval>k__BackingField, other.<DefaultInterval>k__BackingField));
    }

    // Token: 0x06001F91 RID: 8081 RVA: 0x003F24B0 File Offset: 0x003F24B0
    [CompilerGenerated]
    private PaywallPayload(PaywallPayload original)
    {
      this.Reason = original.<Reason>k__BackingField;
      this.Surface = original.<Surface>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Body = original.<Body>k__BackingField;
      this.CtaLabel = original.<CtaLabel>k__BackingField;
      this.PriceDisplay = original.<PriceDisplay>k__BackingField;
      this.CurrentUsage = original.<CurrentUsage>k__BackingField;
      this.Limit = original.<Limit>k__BackingField;
      this.ResetAt = original.<ResetAt>k__BackingField;
      this.Plans = original.<Plans>k__BackingField;
      this.YearlySavingsLabel = original.<YearlySavingsLabel>k__BackingField;
      this.DefaultInterval = original.<DefaultInterval>k__BackingField;
    }

    // Token: 0x06001F92 RID: 8082 RVA: 0x003F2554 File Offset: 0x003F2554
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string Reason, out string Surface, out string Title, out string Body, out string CtaLabel, out string PriceDisplay, out int CurrentUsage, out int Limit, [Nullable(2)] out string ResetAt, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<PaywallPlanOption> Plans, [Nullable(2)] out string YearlySavingsLabel, [Nullable(2)] out string DefaultInterval)
    {
      Reason = this.Reason;
      Surface = this.Surface;
      Title = this.Title;
      Body = this.Body;
      CtaLabel = this.CtaLabel;
      PriceDisplay = this.PriceDisplay;
      CurrentUsage = this.CurrentUsage;
      Limit = this.Limit;
      ResetAt = this.ResetAt;
      Plans = this.Plans;
      YearlySavingsLabel = this.YearlySavingsLabel;
      DefaultInterval = this.DefaultInterval;
    }

    // Token: 0x04000D73 RID: 3443
    public const string ErrorSentinelValue = "paywall";
  }
}
