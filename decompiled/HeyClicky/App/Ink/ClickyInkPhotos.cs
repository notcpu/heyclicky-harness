using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000275 RID: 629
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPhotos
  {
    // Token: 0x06001ACD RID: 6861 RVA: 0x005E76E0 File Offset: 0x005E76E0
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      int num = Math.Min(blocks.Count, 3);
      double num2 = (double)((num == 1) ? 230 : ((num == 2) ? 172 : 116));
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack((double)((num == 3) ? 6 : 14), InkVerticalAlignment.Top, Array.Empty<UIElement>())
      {
        Margin = new Thickness(0.0, 4.0, 0.0, 4.0)
      };
      for (int i = 0; i < num; i++)
      {
        ClickyInkLiveNote.Block block = blocks[i];
        double num3 = ClickyInkClock.Progress(clock, block.LandsAt + 0.2, block.LandsAt + 1.6);
        string text;
        if ((text = block.Source["src"]) == null && (text = block.Source["path"]) == null)
        {
          text = block.Source["url"] ?? block.Source["query"];
        }
        FrameworkElement frameworkElement = ClickyInkPolaroidPicture.Make(text, block.Source["query"], num3, num2 - 14.0, (num2 - 14.0) * 0.78);
        InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand((double)((num == 3) ? 13 : 16)), true, new Color?(PaperColor.Hex("#2B2620")), null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
          t.TextAlignment = TextAlignment.Center;
        });
        inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
        inkTextView.VerticalAlignment = VerticalAlignment.Center;
        Grid grid = new Grid
        {
          Width = num2 - 14.0,
          Height = (double)((num == 3) ? 30 : 36),
          Children = { inkTextView }
        };
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(new StackPanel
        {
          Spacing = 6.0,
          Padding = new Thickness(7.0, 7.0, 7.0, 0.0),
          Children = { frameworkElement, grid }
        }, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.Rectangle, "rect", PaperColor.Hex("#FBFAF6"), new PaperShadow(PaperColor.Black.Opacity(0.16), 3.0, 0.0, 2.0), null));
        clickyInkOverlay.Transformed(0.0, 0.0, ClickyInkPhotos.Tilts[i % 3], 1.0, null);
        clickyInkOverlay.Opacity = clock >= block.LandsAt;
        clickyInkHStack.Children.Add(clickyInkOverlay);
      }
      clickyInkHStack.HorizontalAlignment = ((num == 1) ? HorizontalAlignment.Center : HorizontalAlignment.Left);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkHStack }
      };
    }

    // Token: 0x04000CD7 RID: 3287
    private static readonly double[] Tilts = new double[] { -2.5, 1.8, -1.2 };
  }
}
