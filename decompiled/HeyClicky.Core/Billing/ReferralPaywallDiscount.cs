using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000222 RID: 546
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ReferralPaywallDiscount : IEquatable<ReferralPaywallDiscount>
  {
    // Token: 0x06001FE7 RID: 8167 RVA: 0x003F3800 File Offset: 0x003F3800
    public ReferralPaywallDiscount(double Percent, int Months, bool AppliesToYearlyPlans, string ReferrerFirstName, string ReferrerAvatarUrl)
    {
      this.Percent = Percent;
      this.Months = Months;
      this.AppliesToYearlyPlans = AppliesToYearlyPlans;
      this.ReferrerFirstName = ReferrerFirstName;
      this.ReferrerAvatarUrl = ReferrerAvatarUrl;
      base..ctor();
    }

    // Token: 0x17000729 RID: 1833
    // (get) Token: 0x06001FE8 RID: 8168 RVA: 0x003F3830 File Offset: 0x003F3830
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ReferralPaywallDiscount);
      }
    }

    // Token: 0x1700072A RID: 1834
    // (get) Token: 0x06001FE9 RID: 8169 RVA: 0x003F383C File Offset: 0x003F383C
    // (set) Token: 0x06001FEA RID: 8170 RVA: 0x003F3844 File Offset: 0x003F3844
    public double Percent { get; set; }

    // Token: 0x1700072B RID: 1835
    // (get) Token: 0x06001FEB RID: 8171 RVA: 0x003F3850 File Offset: 0x003F3850
    // (set) Token: 0x06001FEC RID: 8172 RVA: 0x003F3858 File Offset: 0x003F3858
    public int Months { get; set; }

    // Token: 0x1700072C RID: 1836
    // (get) Token: 0x06001FED RID: 8173 RVA: 0x003F3864 File Offset: 0x003F3864
    // (set) Token: 0x06001FEE RID: 8174 RVA: 0x003F386C File Offset: 0x003F386C
    public bool AppliesToYearlyPlans { get; set; }

    // Token: 0x1700072D RID: 1837
    // (get) Token: 0x06001FEF RID: 8175 RVA: 0x003F3878 File Offset: 0x003F3878
    // (set) Token: 0x06001FF0 RID: 8176 RVA: 0x003F3880 File Offset: 0x003F3880
    public string ReferrerFirstName { get; set; }

    // Token: 0x1700072E RID: 1838
    // (get) Token: 0x06001FF1 RID: 8177 RVA: 0x003F388C File Offset: 0x003F388C
    // (set) Token: 0x06001FF2 RID: 8178 RVA: 0x003F3894 File Offset: 0x003F3894
    public string ReferrerAvatarUrl { get; set; }

    // Token: 0x06001FF3 RID: 8179 RVA: 0x003F38A0 File Offset: 0x003F38A0
    public static ReferralPaywallDiscount FromSnapshot(JsonObject snapshot)
    {
      JsonObject jsonObject = ((snapshot != null) ? snapshot["referred_by"] : null) as JsonObject;
      if (jsonObject == null)
      {
        return null;
      }
      if (!ReferralPaywallDiscount.Bool(jsonObject["is_discount_available"]))
      {
        return null;
      }
      double valueOrDefault = ReferralPaywallDiscount.Number(jsonObject["discount_percent"]).GetValueOrDefault();
      if (valueOrDefault <= 0.0)
      {
        return null;
      }
      int num = Math.Max(1, (int)ReferralPaywallDiscount.Number(jsonObject["discount_months"]).GetValueOrDefault(1.0));
      JsonObject jsonObject2 = snapshot["terms"] as JsonObject;
      bool flag = jsonObject2 != null && ReferralPaywallDiscount.Bool(jsonObject2["customer_discount_applies_to_yearly_plans"]);
      JsonValue jsonValue = jsonObject["referrer_first_name"] as JsonValue;
      string text = ((jsonValue != null) ? jsonValue.ToString().Trim() : null);
      JsonValue jsonValue2 = jsonObject["referrer_avatar_url"] as JsonValue;
      string text2 = ((jsonValue2 != null) ? jsonValue2.ToString() : null);
      Uri uri;
      return new ReferralPaywallDiscount(valueOrDefault, num, flag, string.IsNullOrEmpty(text) ? null : text, Uri.TryCreate(text2, UriKind.Absolute, out uri) ? text2 : null);
    }

    // Token: 0x1700072F RID: 1839
    // (get) Token: 0x06001FF4 RID: 8180 RVA: 0x003F39C4 File Offset: 0x003F39C4
    [Nullable(1)]
    public string PercentText
    {
      [NullableContext(1)]
      get
      {
        if (this.Percent != Math.Round(this.Percent))
        {
          return this.Percent.ToString("0.0", CultureInfo.InvariantCulture) + "%";
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.Percent);
        defaultInterpolatedStringHandler.AppendLiteral("%");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x17000730 RID: 1840
    // (get) Token: 0x06001FF5 RID: 8181 RVA: 0x003F3A34 File Offset: 0x003F3A34
    [Nullable(1)]
    public string PeriodText
    {
      [NullableContext(1)]
      get
      {
        if (this.Months != 1)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
          defaultInterpolatedStringHandler.AppendLiteral("your first ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.Months);
          defaultInterpolatedStringHandler.AppendLiteral(" months");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "your first month";
      }
    }

    // Token: 0x06001FF6 RID: 8182 RVA: 0x003F3A88 File Offset: 0x003F3A88
    [NullableContext(1)]
    [return: Nullable(2)]
    public string DiscountedAmountText(string amountText)
    {
      int num = -1;
      int num2 = -1;
      for (int i = 0; i < amountText.Length; i++)
      {
        if (char.IsDigit(amountText[i]))
        {
          if (num < 0)
          {
            num = i;
          }
          num2 = i;
        }
      }
      if (num < 0)
      {
        return null;
      }
      int num3 = num;
      string text = amountText.Substring(num3, num2 + 1 - num3);
      double num4;
      if (!double.TryParse(text.Replace(",", ""), NumberStyles.Float, CultureInfo.InvariantCulture, out num4))
      {
        return null;
      }
      double num5 = Math.Round(num4 * (100.0 - this.Percent) / 100.0 * 100.0) / 100.0;
      bool flag = num5 == Math.Round(num5);
      string text2 = (text.Contains(',') ? "#,0" : "0") + (flag ? "" : ".00");
      string text3 = amountText.Substring(0, num);
      string text4 = num5.ToString(text2, CultureInfo.InvariantCulture);
      num3 = num2 + 1;
      return text3 + text4 + amountText.Substring(num3, amountText.Length - num3);
    }

    // Token: 0x06001FF7 RID: 8183 RVA: 0x003F3BAC File Offset: 0x003F3BAC
    private static bool Bool(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      bool flag;
      return jsonValue != null && jsonValue.TryGetValue<bool>(out flag) && flag;
    }

    // Token: 0x06001FF8 RID: 8184 RVA: 0x003F3BD0 File Offset: 0x003F3BD0
    private static double? Number(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      if (jsonValue == null)
      {
        return null;
      }
      double num;
      if (jsonValue.TryGetValue<double>(out num))
      {
        return new double?(num);
      }
      int num2;
      if (jsonValue.TryGetValue<int>(out num2))
      {
        return new double?((double)num2);
      }
      if (!double.TryParse(jsonValue.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
      {
        return null;
      }
      return new double?(num);
    }

    // Token: 0x06001FF9 RID: 8185 RVA: 0x003F3C40 File Offset: 0x003F3C40
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ReferralPaywallDiscount");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001FFA RID: 8186 RVA: 0x003F3C8C File Offset: 0x003F3C8C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Percent = ");
      builder.Append(this.Percent.ToString());
      builder.Append(", Months = ");
      builder.Append(this.Months.ToString());
      builder.Append(", AppliesToYearlyPlans = ");
      builder.Append(this.AppliesToYearlyPlans.ToString());
      builder.Append(", ReferrerFirstName = ");
      builder.Append(this.ReferrerFirstName);
      builder.Append(", ReferrerAvatarUrl = ");
      builder.Append(this.ReferrerAvatarUrl);
      builder.Append(", PercentText = ");
      builder.Append(this.PercentText);
      builder.Append(", PeriodText = ");
      builder.Append(this.PeriodText);
      return true;
    }

    // Token: 0x06001FFB RID: 8187 RVA: 0x003F3D78 File Offset: 0x003F3D78
    [CompilerGenerated]
    public static bool operator !=(ReferralPaywallDiscount left, ReferralPaywallDiscount right)
    {
      return !(left == right);
    }

    // Token: 0x06001FFC RID: 8188 RVA: 0x003F3D84 File Offset: 0x003F3D84
    [CompilerGenerated]
    public static bool operator ==(ReferralPaywallDiscount left, ReferralPaywallDiscount right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001FFD RID: 8189 RVA: 0x003F3D98 File Offset: 0x003F3D98
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Percent>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Months>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AppliesToYearlyPlans>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReferrerFirstName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ReferrerAvatarUrl>k__BackingField);
    }

    // Token: 0x06001FFE RID: 8190 RVA: 0x003F3E28 File Offset: 0x003F3E28
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ReferralPaywallDiscount);
    }

    // Token: 0x06001FFF RID: 8191 RVA: 0x003F3E38 File Offset: 0x003F3E38
    [CompilerGenerated]
    public bool Equals(ReferralPaywallDiscount other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<Percent>k__BackingField, other.<Percent>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Months>k__BackingField, other.<Months>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AppliesToYearlyPlans>k__BackingField, other.<AppliesToYearlyPlans>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReferrerFirstName>k__BackingField, other.<ReferrerFirstName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ReferrerAvatarUrl>k__BackingField, other.<ReferrerAvatarUrl>k__BackingField));
    }

    // Token: 0x06002001 RID: 8193 RVA: 0x003F3EE8 File Offset: 0x003F3EE8
    [CompilerGenerated]
    private ReferralPaywallDiscount([Nullable(1)] ReferralPaywallDiscount original)
    {
      this.Percent = original.<Percent>k__BackingField;
      this.Months = original.<Months>k__BackingField;
      this.AppliesToYearlyPlans = original.<AppliesToYearlyPlans>k__BackingField;
      this.ReferrerFirstName = original.<ReferrerFirstName>k__BackingField;
      this.ReferrerAvatarUrl = original.<ReferrerAvatarUrl>k__BackingField;
    }

    // Token: 0x06002002 RID: 8194 RVA: 0x003F3F38 File Offset: 0x003F3F38
    [CompilerGenerated]
    public void Deconstruct(out double Percent, out int Months, out bool AppliesToYearlyPlans, out string ReferrerFirstName, out string ReferrerAvatarUrl)
    {
      Percent = this.Percent;
      Months = this.Months;
      AppliesToYearlyPlans = this.AppliesToYearlyPlans;
      ReferrerFirstName = this.ReferrerFirstName;
      ReferrerAvatarUrl = this.ReferrerAvatarUrl;
    }
  }
}
