using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Billing;
using Microsoft.UI.Composition;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D0 RID: 208
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MoneyBarChart : Grid
  {
    // Token: 0x060008FB RID: 2299 RVA: 0x005428F0 File Offset: 0x005428F0
    public MoneyBarChart(IReadOnlyList<int> amountsUsdCents, IReadOnlyList<string> labels, IEnumerable<int> calloutIndices, bool wholeDollars = false, double chartHeight = 96.0)
    {
      MoneyBarChart <>4__this = this;
      this.chartHeight = chartHeight;
      this.wholeDollars = wholeDollars;
      this.callouts = calloutIndices.ToHashSet<int>();
      base.ColumnSpacing = 5.0;
      base.Padding = new Thickness(0.0, 16.0, 0.0, 0.0);
      AutomationProperties.SetAccessibilityView(this, AccessibilityView.Raw);
      for (int i = 0; i < amountsUsdCents.Count; i++)
      {
        base.ColumnDefinitions.Add(new ColumnDefinition());
        GelSurface gelSurface = new GelSurface(GelAppearance.PrimaryBlue, 5.0)
        {
          Height = chartHeight,
          VerticalAlignment = VerticalAlignment.Bottom
        };
        TextBlock textBlock = new TextBlock
        {
          FontSize = 9.5,
          FontWeight = FontWeights.Bold,
          Foreground = Ui.Brush(MoneyBarChart.AquaBlueText),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Bottom,
          IsTextScaleFactorEnabled = false,
          TextWrapping = TextWrapping.NoWrap
        };
        Typography.SetNumeralAlignment(textBlock, FontNumeralAlignment.Tabular);
        Grid grid = new Grid
        {
          Height = chartHeight,
          Children = { gelSurface, textBlock }
        };
        TextBlock textBlock2 = Ui.Text((i < labels.Count) ? labels[i] : "", 9.5, FontWeights.Medium, SK.TextTertiary, TextAlignment.Center, 1, false, false);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 5.0,
          Children = { grid, textBlock2 }
        };
        Grid.SetColumn(stackPanel, i);
        base.Children.Add(stackPanel);
        this.bars.Add(new ValueTuple<GelSurface, TextBlock>(gelSurface, textBlock));
      }
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        <>4__this.Update(amountsUsdCents, false);
      };
    }

    // Token: 0x060008FC RID: 2300 RVA: 0x00542AE8 File Offset: 0x00542AE8
    public void Update(IReadOnlyList<int> amountsUsdCents, bool animate = true)
    {
      int num = Math.Max(1, amountsUsdCents.DefaultIfEmpty(1).Max());
      int num2 = 0;
      while (num2 < this.bars.Count && num2 < amountsUsdCents.Count)
      {
        int num3 = amountsUsdCents[num2];
        float num4 = (float)Math.Max(0.03, (double)num3 / (double)num);
        ValueTuple<GelSurface, TextBlock> valueTuple = this.bars[num2];
        GelSurface item = valueTuple.Item1;
        TextBlock item2 = valueTuple.Item2;
        item.Opacity = ((num3 > 0) ? 1.0 : 0.25);
        item.CenterPoint = new Vector3((float)(item.ActualWidth / 2.0), (float)this.chartHeight, 0f);
        Vector3 vector = new Vector3(1f, num4, 1f);
        if (!animate || Anim.Instant || Anim.ReduceMotion)
        {
          Anim.Stop(item, "Scale");
          item.Scale = vector;
        }
        else
        {
          SpringVector3NaturalMotionAnimation springVector3NaturalMotionAnimation = new SwiftSpring(0.35, 0.85).Vector3(Ui.Compositor, vector);
          springVector3NaturalMotionAnimation.Target = "Scale";
          item.StartAnimation(springVector3NaturalMotionAnimation);
        }
        item2.Text = ((this.callouts.Contains(num2) && num3 > 0) ? (this.wholeDollars ? ReferralFormatting.WholeDollars(num3) : ReferralFormatting.CompactDollars(num3)) : "");
        item2.Margin = new Thickness(-40.0, 0.0, -40.0, 0.0);
        item2.Translation = new Vector3(0f, -(float)(this.chartHeight * (double)num4 + 4.0), 0f);
        num2++;
      }
    }

    // Token: 0x04000315 RID: 789
    public static readonly Color AquaBlueText = DS.Hex(10668031U, 1.0);

    // Token: 0x04000316 RID: 790
    private readonly double chartHeight;

    // Token: 0x04000317 RID: 791
    private readonly bool wholeDollars;

    // Token: 0x04000318 RID: 792
    private readonly HashSet<int> callouts;

    // Token: 0x04000319 RID: 793
    [TupleElementNames(new string[] { "Bar", "Callout" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly List<ValueTuple<GelSurface, TextBlock>> bars = new List<ValueTuple<GelSurface, TextBlock>>();
  }
}
