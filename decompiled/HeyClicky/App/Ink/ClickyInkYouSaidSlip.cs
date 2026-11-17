using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000215 RID: 533
  public static class ClickyInkYouSaidSlip
  {
    // Token: 0x06001953 RID: 6483 RVA: 0x005D10C8 File Offset: 0x005D10C8
    [NullableContext(1)]
    public static FrameworkElement Make(string words, bool isPending, double clock, ClickyInkStyle style)
    {
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
      clickyInkHStack.Children.Add(InkTextView.Plain("↳", ClickyPaperFonts.Hand(18.0), style.Fresh));
      clickyInkHStack.Children.Add(InkTextView.Plain(words, ClickyPaperFonts.Hand(17.0), style.Fresh).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      }));
      if (isPending)
      {
        FrameworkElement frameworkElement = ClickyInkPencilGlyph.Make(13.0, style.Quiet, false);
        frameworkElement.RenderTransform = new RotateTransform
        {
          Angle = Math.Sin(clock * 7.0) * 6.0,
          CenterX = 0.0,
          CenterY = 13.0
        };
        ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(3.0, InkVerticalAlignment.Center, new UIElement[]
        {
          frameworkElement,
          InkTextView.Plain("on it", ClickyPaperFonts.Hand(15.0), style.Quiet)
        });
        clickyInkHStack.Children.Add(clickyInkHStack2);
      }
      clickyInkHStack.Transformed(0.0, 0.0, -1.0, 1.0, null);
      clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkHStack.Margin = new Thickness(16.0, 0.0, 0.0, 0.0);
      return new Grid
      {
        Width = 378.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkHStack }
      };
    }
  }
}
