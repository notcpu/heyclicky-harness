using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000222 RID: 546
  public static class ClickyInkDeltaTag
  {
    // Token: 0x0600198C RID: 6540 RVA: 0x005D3410 File Offset: 0x005D3410
    [NullableContext(1)]
    [return: Nullable(2)]
    public static FrameworkElement Create(ClickyInkDocument.Block block, double clock, [Nullable(2)] string given, [TupleElementNames(new string[] { "Text", "Value" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, double>? computed = null, double size = 11.5, string suffix = "", bool shrinksToFit = false)
    {
      double num = ClickyInkChange.Progress(block, clock, 0.5, 0.4);
      string text = ClickyInkDashboardViewMath.DeltaWordsWithoutAChange(given);
      if (text != null)
      {
        InkTextView inkTextView = InkTextView.Plain(text + suffix, ClickyPaperFonts.Rounded(size, FontWeights.Medium), ClickyInkDashboardPalette.Level).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          t.MinimumScale = (shrinksToFit ? 0.8 : 1.0);
        });
        InkTextView inkTextView2 = (shrinksToFit ? inkTextView : InkDashboardLayout.Fixed(inkTextView));
        inkTextView2.Opacity = num;
        return inkTextView2;
      }
      ValueTuple<string, double>? valueTuple = ClickyInkDashboardViewMath.ResolvedDelta(given, computed);
      if (valueTuple != null)
      {
        ValueTuple<string, double> valueOrDefault = valueTuple.GetValueOrDefault();
        PaperFont paperFont = ClickyPaperFonts.Rounded(size, FontWeights.SemiBold);
        Color color = ClickyInkDashboardPalette.DeltaColor(valueOrDefault.Item2, block);
        FrameworkElement frameworkElement = InkDashboardLayout.Fixed(InkTextView.Plain(ClickyInkDashboardViewMath.DeltaArrow(valueOrDefault.Item2), paperFont, color));
        InkTextView inkTextView3 = InkTextView.Plain(ClickyInkDashboardViewMath.DeltaWords(valueOrDefault.Item1) + suffix, paperFont, color).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          t.MinimumScale = (shrinksToFit ? 0.8 : 1.0);
        });
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(2.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
        {
          frameworkElement,
          shrinksToFit ? inkTextView3 : InkDashboardLayout.Fixed(inkTextView3)
        });
        ClickyInkHStack clickyInkHStack2 = (shrinksToFit ? clickyInkHStack : InkDashboardLayout.Fixed(clickyInkHStack));
        clickyInkHStack2.Opacity = num;
        return clickyInkHStack2;
      }
      return null;
    }
  }
}
