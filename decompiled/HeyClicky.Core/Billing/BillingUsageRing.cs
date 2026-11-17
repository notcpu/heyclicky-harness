using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Billing
{
  // Token: 0x02000215 RID: 533
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BillingUsageRing : IEquatable<BillingUsageRing>
  {
    // Token: 0x06001F30 RID: 7984 RVA: 0x003F1138 File Offset: 0x003F1138
    public BillingUsageRing(double Progress, bool IsSpent, BillingUsageSection Agents, BillingUsageSection Voice, [Nullable(2)] string ResetsLine, string AccessibilityValue)
    {
      this.Progress = Progress;
      this.IsSpent = IsSpent;
      this.Agents = Agents;
      this.Voice = Voice;
      this.ResetsLine = ResetsLine;
      this.AccessibilityValue = AccessibilityValue;
      base..ctor();
    }

    // Token: 0x170006F8 RID: 1784
    // (get) Token: 0x06001F31 RID: 7985 RVA: 0x003F1170 File Offset: 0x003F1170
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(BillingUsageRing);
      }
    }

    // Token: 0x170006F9 RID: 1785
    // (get) Token: 0x06001F32 RID: 7986 RVA: 0x003F117C File Offset: 0x003F117C
    // (set) Token: 0x06001F33 RID: 7987 RVA: 0x003F1184 File Offset: 0x003F1184
    public double Progress { get; set; }

    // Token: 0x170006FA RID: 1786
    // (get) Token: 0x06001F34 RID: 7988 RVA: 0x003F1190 File Offset: 0x003F1190
    // (set) Token: 0x06001F35 RID: 7989 RVA: 0x003F1198 File Offset: 0x003F1198
    public bool IsSpent { get; set; }

    // Token: 0x170006FB RID: 1787
    // (get) Token: 0x06001F36 RID: 7990 RVA: 0x003F11A4 File Offset: 0x003F11A4
    // (set) Token: 0x06001F37 RID: 7991 RVA: 0x003F11AC File Offset: 0x003F11AC
    public BillingUsageSection Agents { get; set; }

    // Token: 0x170006FC RID: 1788
    // (get) Token: 0x06001F38 RID: 7992 RVA: 0x003F11B8 File Offset: 0x003F11B8
    // (set) Token: 0x06001F39 RID: 7993 RVA: 0x003F11C0 File Offset: 0x003F11C0
    public BillingUsageSection Voice { get; set; }

    // Token: 0x170006FD RID: 1789
    // (get) Token: 0x06001F3A RID: 7994 RVA: 0x003F11CC File Offset: 0x003F11CC
    // (set) Token: 0x06001F3B RID: 7995 RVA: 0x003F11D4 File Offset: 0x003F11D4
    [Nullable(2)]
    public string ResetsLine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170006FE RID: 1790
    // (get) Token: 0x06001F3C RID: 7996 RVA: 0x003F11E0 File Offset: 0x003F11E0
    // (set) Token: 0x06001F3D RID: 7997 RVA: 0x003F11E8 File Offset: 0x003F11E8
    public string AccessibilityValue { get; set; }

    // Token: 0x06001F3E RID: 7998 RVA: 0x003F11F4 File Offset: 0x003F11F4
    [NullableContext(2)]
    public static BillingUsageRing From(PlanSnapshot plan, string resetsPhrase)
    {
      if (plan == null || plan.Usage.Agents.Cap <= 0)
      {
        return null;
      }
      PlanUsageBucket agents = plan.Usage.Agents;
      PlanUsageBucket messages = plan.Usage.Messages;
      bool flag = plan.Plan == "unlimited";
      BillingUsageSection billingUsageSection = BillingUsageRing.Section("AGENT MESSAGES", agents, false);
      BillingUsageSection billingUsageSection2 = BillingUsageRing.Section("VOICE MESSAGES", messages, flag);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(BillingUsageRing.Displayed(agents));
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(agents.Cap);
      defaultInterpolatedStringHandler.AppendLiteral(" agent messages. ");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      string text2;
      if (!flag)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(BillingUsageRing.Displayed(messages));
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(messages.Cap);
        defaultInterpolatedStringHandler.AppendLiteral(" voice messages.");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text2 = "Unlimited voice messages.";
      }
      string text3 = text + text2;
      return new BillingUsageRing(billingUsageSection.Progress, billingUsageSection.IsSpent, billingUsageSection, billingUsageSection2, (resetsPhrase == null || resetsPhrase.Length <= 0) ? null : ("Resets " + resetsPhrase), text3);
    }

    // Token: 0x06001F3F RID: 7999 RVA: 0x003F132C File Offset: 0x003F132C
    private static int Displayed(PlanUsageBucket b)
    {
      if (b.Cap <= 0)
      {
        return Math.Max(b.Used, 0);
      }
      return Math.Min(Math.Max(b.Used, 0), b.Cap);
    }

    // Token: 0x06001F40 RID: 8000 RVA: 0x003F135C File Offset: 0x003F135C
    private static BillingUsageSection Section(string title, PlanUsageBucket b, bool isUnlimited)
    {
      int num = BillingUsageRing.Displayed(b);
      double num2 = (isUnlimited ? 1.0 : ((b.Cap > 0) ? ((double)num / (double)b.Cap) : 0.0));
      bool flag = !isUnlimited && b.Cap > 0 && b.Used >= b.Cap;
      string text = (isUnlimited ? "Unlimited" : num.ToString(CultureInfo.InvariantCulture));
      string text2;
      if (!isUnlimited)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
        defaultInterpolatedStringHandler.AppendLiteral("/ ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(b.Cap);
        defaultInterpolatedStringHandler.AppendLiteral(" this month");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text2 = null;
      }
      return new BillingUsageSection(title, text, text2, num2, flag);
    }

    // Token: 0x06001F41 RID: 8001 RVA: 0x003F1418 File Offset: 0x003F1418
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingUsageRing");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001F42 RID: 8002 RVA: 0x003F1464 File Offset: 0x003F1464
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Progress = ");
      builder.Append(this.Progress.ToString());
      builder.Append(", IsSpent = ");
      builder.Append(this.IsSpent.ToString());
      builder.Append(", Agents = ");
      builder.Append(this.Agents);
      builder.Append(", Voice = ");
      builder.Append(this.Voice);
      builder.Append(", ResetsLine = ");
      builder.Append(this.ResetsLine);
      builder.Append(", AccessibilityValue = ");
      builder.Append(this.AccessibilityValue);
      return true;
    }

    // Token: 0x06001F43 RID: 8003 RVA: 0x003F152C File Offset: 0x003F152C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(BillingUsageRing left, BillingUsageRing right)
    {
      return !(left == right);
    }

    // Token: 0x06001F44 RID: 8004 RVA: 0x003F1538 File Offset: 0x003F1538
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(BillingUsageRing left, BillingUsageRing right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001F45 RID: 8005 RVA: 0x003F154C File Offset: 0x003F154C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Progress>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSpent>k__BackingField)) * -1521134295 + EqualityComparer<BillingUsageSection>.Default.GetHashCode(this.<Agents>k__BackingField)) * -1521134295 + EqualityComparer<BillingUsageSection>.Default.GetHashCode(this.<Voice>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ResetsLine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccessibilityValue>k__BackingField);
    }

    // Token: 0x06001F46 RID: 8006 RVA: 0x003F15F4 File Offset: 0x003F15F4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingUsageRing);
    }

    // Token: 0x06001F47 RID: 8007 RVA: 0x003F1604 File Offset: 0x003F1604
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(BillingUsageRing other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<Progress>k__BackingField, other.<Progress>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSpent>k__BackingField, other.<IsSpent>k__BackingField) && EqualityComparer<BillingUsageSection>.Default.Equals(this.<Agents>k__BackingField, other.<Agents>k__BackingField) && EqualityComparer<BillingUsageSection>.Default.Equals(this.<Voice>k__BackingField, other.<Voice>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ResetsLine>k__BackingField, other.<ResetsLine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AccessibilityValue>k__BackingField, other.<AccessibilityValue>k__BackingField));
    }

    // Token: 0x06001F49 RID: 8009 RVA: 0x003F16D0 File Offset: 0x003F16D0
    [CompilerGenerated]
    private BillingUsageRing(BillingUsageRing original)
    {
      this.Progress = original.<Progress>k__BackingField;
      this.IsSpent = original.<IsSpent>k__BackingField;
      this.Agents = original.<Agents>k__BackingField;
      this.Voice = original.<Voice>k__BackingField;
      this.ResetsLine = original.<ResetsLine>k__BackingField;
      this.AccessibilityValue = original.<AccessibilityValue>k__BackingField;
    }

    // Token: 0x06001F4A RID: 8010 RVA: 0x003F172C File Offset: 0x003F172C
    [CompilerGenerated]
    public void Deconstruct(out double Progress, out bool IsSpent, out BillingUsageSection Agents, out BillingUsageSection Voice, [Nullable(2)] out string ResetsLine, out string AccessibilityValue)
    {
      Progress = this.Progress;
      IsSpent = this.IsSpent;
      Agents = this.Agents;
      Voice = this.Voice;
      ResetsLine = this.ResetsLine;
      AccessibilityValue = this.AccessibilityValue;
    }

    // Token: 0x04000D5D RID: 3421
    public const double MinimumDrawnProgress = 0.03;
  }
}
