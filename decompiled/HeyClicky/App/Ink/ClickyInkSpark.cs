using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000229 RID: 553
  public static class ClickyInkSpark
  {
    // Token: 0x0600199A RID: 6554 RVA: 0x005D4AA8 File Offset: 0x005D4AA8
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      ClickyInkSparkReading clickyInkSparkReading = new ClickyInkSparkReading(source);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.1, block.LandsAt + 0.9);
      InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      });
      double num2 = 6.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      UIElement[] array = new UIElement[3];
      array[0] = ClickyInkTickingNumber.Create(source, "values", clock, style, 26.0, 0.5, clickyInkSparkReading.Last, clickyInkSparkReading.PreviousLast, true);
      int num3 = 1;
      string text = source["unit"];
      array[num3] = ((text != null && text.Length > 0) ? InkDashboardLayout.Fixed(InkTextView.Plain(text, ClickyPaperFonts.Hand(15.0), style.Quiet)) : null);
      array[2] = ClickyInkDeltaTag.Create(source, clock, source["delta"], clickyInkSparkReading.Step, 11.5, "", false);
      ClickyInkHStack clickyInkHStack = InkDashboardLayout.HStack(num2, inkVerticalAlignment, array);
      StackPanel stackPanel = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 2.0,
        Children = { inkTextView, clickyInkHStack }
      }, width * 0.4);
      IReadOnlyList<double> values = clickyInkSparkReading.Values;
      IReadOnlyList<double> readOnlyList = (ClickyInkChange.Animates(source, style.LatestEdit) ? clickyInkSparkReading.PreviousValues : null);
      double num4 = ClickyInkChange.Progress(source, clock, 0.0, 0.8);
      double num5 = num;
      string text2 = source["goal"];
      ClickyInkNumber? clickyInkNumber;
      double? num6 = ((text2 != null) ? ((ClickyInkNumber.Parse(text2) != null) ? new double?(clickyInkNumber.GetValueOrDefault().Value) : null) : null);
      int num7 = block.Id.CharacterCount();
      bool flag = ClickyInkChange.IsLatest(source, style.LatestEdit);
      double? num8 = new double?(Math.Max(0.0, width - width * 0.4 - 14.0));
      double? num9 = new double?((double)50);
      FrameworkElement frameworkElement = ClickyInkSparkLine.Create(values, readOnlyList, num4, num5, num6, style, null, num7, flag, num8, num9);
      ClickyInkHStack clickyInkHStack2 = InkDashboardLayout.HStack(14.0, InkVerticalAlignment.Center, new UIElement[] { stackPanel, frameworkElement });
      StackPanel stackPanel2 = InkDashboardLayout.Width<StackPanel>(new StackPanel
      {
        Spacing = 4.0,
        Children = { clickyInkHStack2 }
      }, width);
      if (clickyInkSparkReading.Labels.Count > 0 || source["note"] != null)
      {
        ClickyInkHStack clickyInkHStack3 = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        string text3 = source["note"];
        if (text3 != null && text3.Length > 0)
        {
          clickyInkHStack3.Children.Add(InkTextView.Plain(text3, ClickyPaperFonts.Hand(15.0), style.Fresh).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
        }
        clickyInkHStack3.Children.Add(new ClickyInkSpacer());
        if (clickyInkSparkReading.Labels.Count > 1)
        {
          UIElementCollection children = clickyInkHStack3.Children;
          string text4 = clickyInkSparkReading.Labels[0];
          string text5 = " → ";
          List<string> labels = clickyInkSparkReading.Labels;
          children.Add(InkDashboardLayout.Fixed(InkTextView.Plain(text4 + text5 + labels[labels.Count - 1], ClickyPaperFonts.Typed(10.5, null), style.Quiet).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          })));
        }
        clickyInkHStack3.Width = width;
        clickyInkHStack3.Opacity = num;
        stackPanel2.Children.Add(clickyInkHStack3);
      }
      return stackPanel2;
    }
  }
}
