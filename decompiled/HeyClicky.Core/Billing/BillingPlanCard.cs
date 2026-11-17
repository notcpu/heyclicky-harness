using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Billing
{
  // Token: 0x0200020B RID: 523
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BillingPlanCard : IEquatable<BillingPlanCard>
  {
    // Token: 0x06001E74 RID: 7796 RVA: 0x003EE4C4 File Offset: 0x003EE4C4
    public BillingPlanCard([Nullable(1)] string Title, string Description, string TeamLabel, string ErrorText, string CancellationLabel, [Nullable(1)] IReadOnlyList<BillingUsageMeter> Meters, [Nullable(1)] IReadOnlyList<BillingPlanAction> Actions)
    {
      this.Title = Title;
      this.Description = Description;
      this.TeamLabel = TeamLabel;
      this.ErrorText = ErrorText;
      this.CancellationLabel = CancellationLabel;
      this.Meters = Meters;
      this.Actions = Actions;
      base..ctor();
    }

    // Token: 0x170006BF RID: 1727
    // (get) Token: 0x06001E75 RID: 7797 RVA: 0x003EE504 File Offset: 0x003EE504
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(BillingPlanCard);
      }
    }

    // Token: 0x170006C0 RID: 1728
    // (get) Token: 0x06001E76 RID: 7798 RVA: 0x003EE510 File Offset: 0x003EE510
    // (set) Token: 0x06001E77 RID: 7799 RVA: 0x003EE518 File Offset: 0x003EE518
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006C1 RID: 1729
    // (get) Token: 0x06001E78 RID: 7800 RVA: 0x003EE524 File Offset: 0x003EE524
    // (set) Token: 0x06001E79 RID: 7801 RVA: 0x003EE52C File Offset: 0x003EE52C
    public string Description { get; set; }

    // Token: 0x170006C2 RID: 1730
    // (get) Token: 0x06001E7A RID: 7802 RVA: 0x003EE538 File Offset: 0x003EE538
    // (set) Token: 0x06001E7B RID: 7803 RVA: 0x003EE540 File Offset: 0x003EE540
    public string TeamLabel { get; set; }

    // Token: 0x170006C3 RID: 1731
    // (get) Token: 0x06001E7C RID: 7804 RVA: 0x003EE54C File Offset: 0x003EE54C
    // (set) Token: 0x06001E7D RID: 7805 RVA: 0x003EE554 File Offset: 0x003EE554
    public string ErrorText { get; set; }

    // Token: 0x170006C4 RID: 1732
    // (get) Token: 0x06001E7E RID: 7806 RVA: 0x003EE560 File Offset: 0x003EE560
    // (set) Token: 0x06001E7F RID: 7807 RVA: 0x003EE568 File Offset: 0x003EE568
    public string CancellationLabel { get; set; }

    // Token: 0x170006C5 RID: 1733
    // (get) Token: 0x06001E80 RID: 7808 RVA: 0x003EE574 File Offset: 0x003EE574
    // (set) Token: 0x06001E81 RID: 7809 RVA: 0x003EE57C File Offset: 0x003EE57C
    [Nullable(1)]
    public IReadOnlyList<BillingUsageMeter> Meters
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170006C6 RID: 1734
    // (get) Token: 0x06001E82 RID: 7810 RVA: 0x003EE588 File Offset: 0x003EE588
    // (set) Token: 0x06001E83 RID: 7811 RVA: 0x003EE590 File Offset: 0x003EE590
    [Nullable(1)]
    public IReadOnlyList<BillingPlanAction> Actions
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x06001E84 RID: 7812 RVA: 0x003EE59C File Offset: 0x003EE59C
    [NullableContext(1)]
    public static BillingPlanCard From(BillingStateService billing)
    {
      PlanSnapshot plan = billing.Plan;
      bool isFetching = billing.IsFetching;
      bool requiresPaymentResolution = billing.RequiresPaymentResolution;
      string text = ((billing.CurrentTier == "max") ? "HeyClicky Max" : "HeyClicky Pro");
      string text2 = ((plan == null) ? (isFetching ? "Checking your plan…" : "Couldn't load your plan.") : (requiresPaymentResolution ? ("There's a problem with your " + text + " payment.") : (billing.IsUnlimited ? ("You're on " + text + ".") : "You're on the Free plan.")));
      string text3 = null;
      if (plan != null)
      {
        if (requiresPaymentResolution)
        {
          text3 = "Update your payment method to keep using HeyClicky. Processing is paused until billing is fixed.";
        }
        else
        {
          string text4;
          if (!(plan.Plan == "unlimited"))
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 2);
            defaultInterpolatedStringHandler.AppendLiteral("Talk to HeyClicky ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Usage.Messages.Cap);
            defaultInterpolatedStringHandler.AppendLiteral(" times a month and send ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Usage.Agents.Cap);
            defaultInterpolatedStringHandler.AppendLiteral(" agent messages.");
            text4 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 1);
            defaultInterpolatedStringHandler.AppendLiteral("Chat with HeyClicky as much as you want and send ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(plan.Usage.Agents.Cap);
            defaultInterpolatedStringHandler.AppendLiteral(" agent messages a month.");
            text4 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          string text5 = text4;
          string usageResetsLabel = billing.UsageResetsLabel;
          text3 = ((usageResetsLabel != null) ? (text5 + " " + usageResetsLabel) : text5);
        }
      }
      string text6;
      if (!billing.IsTeamManagedPlan)
      {
        text6 = null;
      }
      else
      {
        string teamPlanTeamName = billing.TeamPlanTeamName;
        text6 = ((teamPlanTeamName != null && teamPlanTeamName.Length > 0) ? ("Included with your " + teamPlanTeamName + " team plan.") : "Included with your team plan.");
      }
      string text7 = text6;
      string text8 = null;
      BillingFetchError lastFetchError = billing.LastFetchError;
      if (lastFetchError != null)
      {
        if (lastFetchError.Kind == BillingFetchErrorKind.SignedOut)
        {
          text8 = "it seems like you got logged out. can you log in again for me and try again?";
        }
        else if (plan == null && !isFetching)
        {
          text8 = "HeyClicky couldn't reach the server. Your plan and usage will show once it's back.";
        }
      }
      List<BillingUsageMeter> list = new List<BillingUsageMeter>();
      if (plan != null && !requiresPaymentResolution)
      {
        PlanUsageBucket planUsageBucket = billing.DisplayedMessagesUsage ?? plan.Usage.Messages;
        list.Add((plan.Plan == "unlimited") ? new BillingUsageMeter("Talk to HeyClicky", BillingPlanCard.<From>g__Unlimited|31_3(planUsageBucket), 1.0) : new BillingUsageMeter("Talk to HeyClicky", BillingPlanCard.<From>g__Usage|31_2(planUsageBucket), BillingPlanCard.<From>g__Progress|31_1(planUsageBucket)));
        list.Add(new BillingUsageMeter("Agent messages", BillingPlanCard.<From>g__Usage|31_2(plan.Usage.Agents), BillingPlanCard.<From>g__Progress|31_1(plan.Usage.Agents)));
      }
      List<BillingPlanAction> list2 = new List<BillingPlanAction>();
      if (plan == null)
      {
        if (!isFetching)
        {
          list2.Add(new BillingPlanAction(BillingPlanActionKind.TryAgain, "Try again", BillingActionStyle.SecondaryWhite));
        }
      }
      else if (requiresPaymentResolution)
      {
        list2.Add(new BillingPlanAction(BillingPlanActionKind.FixPayment, "Fix payment", BillingActionStyle.Primary));
      }
      else if (billing.IsUnlimited)
      {
        if (billing.IsTeamManagedPlan)
        {
          list2.Add(new BillingPlanAction(BillingPlanActionKind.ViewTeam, "View your team", BillingActionStyle.SecondaryWhite));
        }
        else
        {
          if (billing.IsBillingEnforcementOn && billing.ManualUpgradePayload != null)
          {
            list2.Add(new BillingPlanAction(BillingPlanActionKind.Upgrade, (billing.CurrentTier == "pro") ? "Upgrade to Max" : "Upgrade HeyClicky", BillingActionStyle.Primary));
          }
          if (billing.IsBillingEnforcementOn)
          {
            string annualBillingSavingsCtaTitle = billing.AnnualBillingSavingsCtaTitle;
            if (annualBillingSavingsCtaTitle != null)
            {
              list2.Add(new BillingPlanAction(BillingPlanActionKind.SwitchToYearly, annualBillingSavingsCtaTitle, BillingActionStyle.SecondaryWhite));
            }
          }
          list2.Add(new BillingPlanAction(BillingPlanActionKind.ManageSubscription, "Manage subscription", BillingActionStyle.SecondaryOutline));
        }
      }
      else if (billing.IsBillingEnforcementOn)
      {
        list2.Add(new BillingPlanAction(BillingPlanActionKind.Upgrade, "Upgrade HeyClicky", BillingActionStyle.Primary));
      }
      return new BillingPlanCard(text2, text3, text7, text8, requiresPaymentResolution ? null : billing.SubscriptionCancellationLabel, list, list2);
    }

    // Token: 0x06001E85 RID: 7813 RVA: 0x003EE940 File Offset: 0x003EE940
    [NullableContext(1)]
    public static string ActionErrorText(bool signedOut)
    {
      if (!signedOut)
      {
        return "Oops, something's wrong. Email us at hi@heyclicky.com please.";
      }
      return "it seems like you got logged out. can you log in again for me and try again?";
    }

    // Token: 0x06001E86 RID: 7814 RVA: 0x003EE950 File Offset: 0x003EE950
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BillingPlanCard");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001E87 RID: 7815 RVA: 0x003EE99C File Offset: 0x003EE99C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", TeamLabel = ");
      builder.Append(this.TeamLabel);
      builder.Append(", ErrorText = ");
      builder.Append(this.ErrorText);
      builder.Append(", CancellationLabel = ");
      builder.Append(this.CancellationLabel);
      builder.Append(", Meters = ");
      builder.Append(this.Meters);
      builder.Append(", Actions = ");
      builder.Append(this.Actions);
      return true;
    }

    // Token: 0x06001E88 RID: 7816 RVA: 0x003EEA60 File Offset: 0x003EEA60
    [CompilerGenerated]
    public static bool operator !=(BillingPlanCard left, BillingPlanCard right)
    {
      return !(left == right);
    }

    // Token: 0x06001E89 RID: 7817 RVA: 0x003EEA6C File Offset: 0x003EEA6C
    [CompilerGenerated]
    public static bool operator ==(BillingPlanCard left, BillingPlanCard right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001E8A RID: 7818 RVA: 0x003EEA80 File Offset: 0x003EEA80
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TeamLabel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CancellationLabel>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<BillingUsageMeter>>.Default.GetHashCode(this.<Meters>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<BillingPlanAction>>.Default.GetHashCode(this.<Actions>k__BackingField);
    }

    // Token: 0x06001E8B RID: 7819 RVA: 0x003EEB40 File Offset: 0x003EEB40
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BillingPlanCard);
    }

    // Token: 0x06001E8C RID: 7820 RVA: 0x003EEB50 File Offset: 0x003EEB50
    [CompilerGenerated]
    public bool Equals(BillingPlanCard other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TeamLabel>k__BackingField, other.<TeamLabel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorText>k__BackingField, other.<ErrorText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CancellationLabel>k__BackingField, other.<CancellationLabel>k__BackingField) && EqualityComparer<IReadOnlyList<BillingUsageMeter>>.Default.Equals(this.<Meters>k__BackingField, other.<Meters>k__BackingField) && EqualityComparer<IReadOnlyList<BillingPlanAction>>.Default.Equals(this.<Actions>k__BackingField, other.<Actions>k__BackingField));
    }

    // Token: 0x06001E8E RID: 7822 RVA: 0x003EEC38 File Offset: 0x003EEC38
    [CompilerGenerated]
    private BillingPlanCard([Nullable(1)] BillingPlanCard original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.TeamLabel = original.<TeamLabel>k__BackingField;
      this.ErrorText = original.<ErrorText>k__BackingField;
      this.CancellationLabel = original.<CancellationLabel>k__BackingField;
      this.Meters = original.<Meters>k__BackingField;
      this.Actions = original.<Actions>k__BackingField;
    }

    // Token: 0x06001E8F RID: 7823 RVA: 0x003EECA0 File Offset: 0x003EECA0
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Title, out string Description, out string TeamLabel, out string ErrorText, out string CancellationLabel, [Nullable(1)] out IReadOnlyList<BillingUsageMeter> Meters, [Nullable(1)] out IReadOnlyList<BillingPlanAction> Actions)
    {
      Title = this.Title;
      Description = this.Description;
      TeamLabel = this.TeamLabel;
      ErrorText = this.ErrorText;
      CancellationLabel = this.CancellationLabel;
      Meters = this.Meters;
      Actions = this.Actions;
    }

    // Token: 0x06001E90 RID: 7824 RVA: 0x003EECE0 File Offset: 0x003EECE0
    [NullableContext(1)]
    [CompilerGenerated]
    internal static int <From>g__Clamped|31_0(PlanUsageBucket b)
    {
      if (b.Cap > 0)
      {
        return Math.Min(Math.Max(b.Used, 0), b.Cap);
      }
      return Math.Max(b.Used, 0);
    }

    // Token: 0x06001E91 RID: 7825 RVA: 0x003EED10 File Offset: 0x003EED10
    [NullableContext(1)]
    [CompilerGenerated]
    internal static double <From>g__Progress|31_1(PlanUsageBucket b)
    {
      if (b.Cap > 0)
      {
        return Math.Clamp((double)b.Used / (double)b.Cap, 0.0, 1.0);
      }
      return 0.0;
    }

    // Token: 0x06001E92 RID: 7826 RVA: 0x003EED4C File Offset: 0x003EED4C
    [NullableContext(1)]
    [CompilerGenerated]
    internal static string <From>g__Usage|31_2(PlanUsageBucket b)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(BillingPlanCard.<From>g__Clamped|31_0(b));
      defaultInterpolatedStringHandler.AppendLiteral(" / ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(b.Cap);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06001E93 RID: 7827 RVA: 0x003EED90 File Offset: 0x003EED90
    [NullableContext(1)]
    [CompilerGenerated]
    internal static string <From>g__Unlimited|31_3(PlanUsageBucket b)
    {
      return "Unlimited";
    }

    // Token: 0x04000D18 RID: 3352
    private const bool ShowsRealUsage = false;

    // Token: 0x04000D19 RID: 3353
    [Nullable(1)]
    public const string TeamDashboardUrl = "https://team.heyclicky.com";
  }
}
