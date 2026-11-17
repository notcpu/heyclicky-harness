using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D2 RID: 210
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ReferralEstimatorCard : StackPanel
  {
    // Token: 0x06000906 RID: 2310 RVA: 0x00543414 File Offset: 0x00543414
    public ReferralEstimatorCard(IReadOnlyList<ReferralEstimatorCard.Plan> plans, int maxReferralsPerMonth, int defaultMonthsRetained, double commissionPercent, int commissionMonths, ReferralEstimatorCard.State state)
    {
      ReferralEstimatorCard <>4__this = this;
      this.plans = plans;
      this.state = state;
      this.commissionPercent = commissionPercent;
      this.commissionMonths = commissionMonths;
      foreach (ReferralEstimatorCard.Plan plan2 in plans)
      {
        if (!state.ReferralsByPlan.ContainsKey(plan2.Key))
        {
          state.ReferralsByPlan[plan2.Key] = (double)plan2.DefaultReferralsPerMonth;
        }
      }
      ReferralEstimatorCard.State state2 = state;
      double? monthsRetained = state2.MonthsRetained;
      double num = monthsRetained.GetValueOrDefault();
      if (monthsRetained == null)
      {
        num = (double)Math.Min(defaultMonthsRetained, commissionMonths);
        ReferralEstimatorCard.State state3 = state2;
        double? num2 = new double?(num);
        state3.MonthsRetained = num2;
      }
      base.Spacing = 18.0;
      base.Padding = new Thickness(16.0);
      TextBlock textBlock = SK.MicroLabel("IN YOUR FIRST YEAR", 9.5, FontWeights.Bold, 80);
      this.total = new TextBlock
      {
        FontSize = 38.0,
        FontWeight = FontWeights.Black,
        IsTextScaleFactorEnabled = false,
        Foreground = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(10079224U, 1.0))
        })
      };
      Typography.SetNumeralAlignment(this.total, FontNumeralAlignment.Tabular);
      base.Children.Add(new StackPanel
      {
        Spacing = 2.0,
        Children = { textBlock, this.total }
      });
      ReferralEarningsMixEstimate referralEarningsMixEstimate = this.Estimate();
      this.chart = new MoneyBarChart(referralEarningsMixEstimate.FirstYearMonthlyUsdCents, Enumerable.Range(1, 12).Select<int, string>(delegate(int m)
      {
        if (m != 1 && m != 12)
        {
          return m.ToString();
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Month ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(m);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }).ToList<string>(), new <>z__ReadOnlySingleElementList<int>(11), true, 84.0);
      base.Children.Add(this.chart);
      for (int i = 0; i < plans.Count; i++)
      {
        ReferralEstimatorCard.Plan plan = plans[i];
        ValueTuple<GelAppearance, Color> valueTuple = ReferralEstimatorCard.PlanGels[i % ReferralEstimatorCard.PlanGels.Length];
        GelAppearance item = valueTuple.Item1;
        Color item2 = valueTuple.Item2;
        Run run;
        base.Children.Add(ReferralEstimatorCard.SliderRow("New " + plan.Name + " referrals per month", ReferralFormatting.CompactDollars(plan.MonthlyPriceUsdCents) + "/mo plan", "/ month", state.ReferralsByPlan[plan.Key], 0.0, (double)Math.Max(1, maxReferralsPerMonth), item, item2, out run, delegate(double v)
        {
          state.ReferralsByPlan[plan.Key] = v;
          <>4__this.Recompute(true);
        }));
        this.planValues.Add(new ValueTuple<Run, ReferralEstimatorCard.Plan>(run, plan));
      }
      Run run2;
      base.Children.Add(ReferralEstimatorCard.SliderRow("How long they stick around", "how long they stick around", "/ friend", state.MonthsRetained.Value, 1.0, (double)Math.Max(2, commissionMonths), GelAppearance.Mint, DS.Hex(13234383U, 1.0), out run2, delegate(double v)
      {
        state.MonthsRetained = new double?(v);
        <>4__this.Recompute(true);
      }));
      this.monthsValue = run2;
      this.summary = Ui.Text("", 12.0, FontWeights.Medium, SK.TextSecondary, TextAlignment.Left, 0, true, false);
      this.summary.LineHeight = 17.0;
      base.Children.Add(this.summary);
      this.Recompute(false);
    }

    // Token: 0x17000176 RID: 374
    // (get) Token: 0x06000907 RID: 2311 RVA: 0x00543894 File Offset: 0x00543894
    public string TotalText
    {
      get
      {
        return this.total.Text;
      }
    }

    // Token: 0x17000177 RID: 375
    // (get) Token: 0x06000908 RID: 2312 RVA: 0x005438A4 File Offset: 0x005438A4
    private int Months
    {
      get
      {
        return (int)Math.Round(this.state.MonthsRetained.GetValueOrDefault(1.0));
      }
    }

    // Token: 0x06000909 RID: 2313 RVA: 0x005438D4 File Offset: 0x005438D4
    private int ReferralsFor(ReferralEstimatorCard.Plan p)
    {
      return (int)Math.Round(this.state.ReferralsByPlan.GetValueOrDefault(p.Key));
    }

    // Token: 0x0600090A RID: 2314 RVA: 0x005438F4 File Offset: 0x005438F4
    private ReferralEarningsMixEstimate Estimate()
    {
      return ReferralEarningsEstimator.Estimate(this.plans.Select<ReferralEstimatorCard.Plan, ReferralEarningsPlanMix>((ReferralEstimatorCard.Plan p) => new ReferralEarningsPlanMix(p.MonthlyPriceUsdCents, this.ReferralsFor(p))).ToList<ReferralEarningsPlanMix>(), this.Months, this.commissionPercent, this.commissionMonths);
    }

    // Token: 0x0600090B RID: 2315 RVA: 0x0054392C File Offset: 0x0054392C
    private void Recompute(bool animate = true)
    {
      ReferralEarningsMixEstimate referralEarningsMixEstimate = this.Estimate();
      this.total.Text = ReferralFormatting.WholeDollars(referralEarningsMixEstimate.FirstYearUsdCents);
      if (animate)
      {
        this.chart.Update(referralEarningsMixEstimate.FirstYearMonthlyUsdCents, true);
      }
      foreach (ValueTuple<Run, ReferralEstimatorCard.Plan> valueTuple in this.planValues)
      {
        Run item = valueTuple.Item1;
        ReferralEstimatorCard.Plan item2 = valueTuple.Item2;
        item.Text = ReferralFormatting.Referrals(this.ReferralsFor(item2), item2.Name);
      }
      if (this.monthsValue != null)
      {
        this.monthsValue.Text = ReferralFormatting.Months(this.Months);
      }
      List<string> list = (from p in this.plans
        where this.ReferralsFor(p) > 0
        select ReferralFormatting.People(this.ReferralsFor(p)) + " on " + p.Name).ToList<string>();
      TextBlock textBlock = this.summary;
      string text;
      if (list.Count != 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Refer ");
        defaultInterpolatedStringHandler.AppendFormatted(string.Join(" and ", list));
        defaultInterpolatedStringHandler.AppendLiteral(" a month who each stay ");
        defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.Months(this.Months));
        defaultInterpolatedStringHandler.AppendLiteral(". ");
        defaultInterpolatedStringHandler.AppendFormatted(ReferralFormatting.FirstYearExplanation(referralEarningsMixEstimate, this.Months, this.commissionMonths));
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text = "Move a slider to see what your referrals could earn.";
      }
      textBlock.Text = text;
      AutomationProperties.SetName(this.total, "In your first year: " + this.total.Text);
    }

    // Token: 0x0600090C RID: 2316 RVA: 0x00543ACC File Offset: 0x00543ACC
    private static FrameworkElement SliderRow(string title, [Nullable(2)] string detail, string unit, double value, double min, double max, GelAppearance gel, Color glow, out Run valueRun, Action<double> changed)
    {
      TextBlock textBlock = new TextBlock
      {
        FontSize = 20.0,
        FontWeight = FontWeights.Bold,
        IsTextScaleFactorEnabled = false,
        TextTrimming = TextTrimming.CharacterEllipsis,
        MaxLines = 1
      };
      Typography.SetNumeralAlignment(textBlock, FontNumeralAlignment.Tabular);
      valueRun = new Run
      {
        Foreground = Ui.Brush(SK.TextPrimary)
      };
      textBlock.Inlines.Add(valueRun);
      textBlock.Inlines.Add(new Run
      {
        Text = " " + unit,
        Foreground = Ui.Brush(SK.TextTertiary)
      });
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition());
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.Children.Add(textBlock);
      if (detail != null)
      {
        TextBlock textBlock2 = Ui.Text(detail, 11.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Left, 1, false, false);
        textBlock2.VerticalAlignment = VerticalAlignment.Bottom;
        textBlock2.Margin = new Thickness(0.0, 0.0, 0.0, 3.0);
        Grid.SetColumn(textBlock2, 1);
        grid.Children.Add(textBlock2);
      }
      GelSlider gelSlider = new GelSlider(value, min, max, gel, glow, title)
      {
        Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
      };
      gelSlider.ValueChanged += changed;
      return new StackPanel
      {
        Spacing = 7.0,
        Children = { grid, gelSlider }
      };
    }

    // Token: 0x04000327 RID: 807
    [TupleElementNames(new string[] { "Gel", "Glow" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<GelAppearance, Color>[] PlanGels = new ValueTuple<GelAppearance, Color>[]
    {
      new ValueTuple<GelAppearance, Color>(GelAppearance.PrimaryBlue, DS.Hex(10336511U, 1.0)),
      new ValueTuple<GelAppearance, Color>(GelAppearance.Peach, DS.Hex(16763289U, 1.0))
    };

    // Token: 0x04000328 RID: 808
    private readonly IReadOnlyList<ReferralEstimatorCard.Plan> plans;

    // Token: 0x04000329 RID: 809
    private readonly ReferralEstimatorCard.State state;

    // Token: 0x0400032A RID: 810
    private readonly double commissionPercent;

    // Token: 0x0400032B RID: 811
    private readonly int commissionMonths;

    // Token: 0x0400032C RID: 812
    private readonly TextBlock total;

    // Token: 0x0400032D RID: 813
    private readonly TextBlock summary;

    // Token: 0x0400032E RID: 814
    private readonly MoneyBarChart chart;

    // Token: 0x0400032F RID: 815
    [TupleElementNames(new string[] { "Value", "Plan" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly List<ValueTuple<Run, ReferralEstimatorCard.Plan>> planValues = new List<ValueTuple<Run, ReferralEstimatorCard.Plan>>();

    // Token: 0x04000330 RID: 816
    [Nullable(2)]
    private Run monthsValue;

    // Token: 0x020004A3 RID: 1187
    [Nullable(0)]
    public sealed class Plan : IEquatable<ReferralEstimatorCard.Plan>
    {
      // Token: 0x06002B2F RID: 11055 RVA: 0x00653D40 File Offset: 0x00653D40
      public Plan(string Key, string Name, int MonthlyPriceUsdCents, int DefaultReferralsPerMonth)
      {
        this.Key = Key;
        this.Name = Name;
        this.MonthlyPriceUsdCents = MonthlyPriceUsdCents;
        this.DefaultReferralsPerMonth = DefaultReferralsPerMonth;
        base..ctor();
      }

      // Token: 0x17000713 RID: 1811
      // (get) Token: 0x06002B30 RID: 11056 RVA: 0x00653D68 File Offset: 0x00653D68
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ReferralEstimatorCard.Plan);
        }
      }

      // Token: 0x17000714 RID: 1812
      // (get) Token: 0x06002B31 RID: 11057 RVA: 0x00653D74 File Offset: 0x00653D74
      // (set) Token: 0x06002B32 RID: 11058 RVA: 0x00653D7C File Offset: 0x00653D7C
      public string Key { get; set; }

      // Token: 0x17000715 RID: 1813
      // (get) Token: 0x06002B33 RID: 11059 RVA: 0x00653D88 File Offset: 0x00653D88
      // (set) Token: 0x06002B34 RID: 11060 RVA: 0x00653D90 File Offset: 0x00653D90
      public string Name { get; set; }

      // Token: 0x17000716 RID: 1814
      // (get) Token: 0x06002B35 RID: 11061 RVA: 0x00653D9C File Offset: 0x00653D9C
      // (set) Token: 0x06002B36 RID: 11062 RVA: 0x00653DA4 File Offset: 0x00653DA4
      public int MonthlyPriceUsdCents { get; set; }

      // Token: 0x17000717 RID: 1815
      // (get) Token: 0x06002B37 RID: 11063 RVA: 0x00653DB0 File Offset: 0x00653DB0
      // (set) Token: 0x06002B38 RID: 11064 RVA: 0x00653DB8 File Offset: 0x00653DB8
      public int DefaultReferralsPerMonth { get; set; }

      // Token: 0x06002B39 RID: 11065 RVA: 0x00653DC4 File Offset: 0x00653DC4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Plan");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002B3A RID: 11066 RVA: 0x00653E10 File Offset: 0x00653E10
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Key = ");
        builder.Append(this.Key);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", MonthlyPriceUsdCents = ");
        builder.Append(this.MonthlyPriceUsdCents.ToString());
        builder.Append(", DefaultReferralsPerMonth = ");
        builder.Append(this.DefaultReferralsPerMonth.ToString());
        return true;
      }

      // Token: 0x06002B3B RID: 11067 RVA: 0x00653EA4 File Offset: 0x00653EA4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ReferralEstimatorCard.Plan left, ReferralEstimatorCard.Plan right)
      {
        return !(left == right);
      }

      // Token: 0x06002B3C RID: 11068 RVA: 0x00653EB0 File Offset: 0x00653EB0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ReferralEstimatorCard.Plan left, ReferralEstimatorCard.Plan right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002B3D RID: 11069 RVA: 0x00653EC4 File Offset: 0x00653EC4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Key>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<MonthlyPriceUsdCents>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DefaultReferralsPerMonth>k__BackingField);
      }

      // Token: 0x06002B3E RID: 11070 RVA: 0x00653F40 File Offset: 0x00653F40
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ReferralEstimatorCard.Plan);
      }

      // Token: 0x06002B3F RID: 11071 RVA: 0x00653F50 File Offset: 0x00653F50
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ReferralEstimatorCard.Plan other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Key>k__BackingField, other.<Key>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<MonthlyPriceUsdCents>k__BackingField, other.<MonthlyPriceUsdCents>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DefaultReferralsPerMonth>k__BackingField, other.<DefaultReferralsPerMonth>k__BackingField));
      }

      // Token: 0x06002B41 RID: 11073 RVA: 0x00653FE4 File Offset: 0x00653FE4
      [CompilerGenerated]
      private Plan(ReferralEstimatorCard.Plan original)
      {
        this.Key = original.<Key>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.MonthlyPriceUsdCents = original.<MonthlyPriceUsdCents>k__BackingField;
        this.DefaultReferralsPerMonth = original.<DefaultReferralsPerMonth>k__BackingField;
      }

      // Token: 0x06002B42 RID: 11074 RVA: 0x0065401C File Offset: 0x0065401C
      [CompilerGenerated]
      public void Deconstruct(out string Key, out string Name, out int MonthlyPriceUsdCents, out int DefaultReferralsPerMonth)
      {
        Key = this.Key;
        Name = this.Name;
        MonthlyPriceUsdCents = this.MonthlyPriceUsdCents;
        DefaultReferralsPerMonth = this.DefaultReferralsPerMonth;
      }
    }

    // Token: 0x020004A4 RID: 1188
    [Nullable(0)]
    public sealed class State
    {
      // Token: 0x17000718 RID: 1816
      // (get) Token: 0x06002B43 RID: 11075 RVA: 0x00654040 File Offset: 0x00654040
      public Dictionary<string, double> ReferralsByPlan { get; } = new Dictionary<string, double>();

      // Token: 0x17000719 RID: 1817
      // (get) Token: 0x06002B44 RID: 11076 RVA: 0x00654048 File Offset: 0x00654048
      // (set) Token: 0x06002B45 RID: 11077 RVA: 0x00654050 File Offset: 0x00654050
      public double? MonthsRetained { get; set; }
    }
  }
}
