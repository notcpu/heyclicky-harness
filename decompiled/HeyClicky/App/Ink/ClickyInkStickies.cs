using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000265 RID: 613
  public static class ClickyInkStickies
  {
    // Token: 0x06001A95 RID: 6805 RVA: 0x005E3BF0 File Offset: 0x005E3BF0
    [NullableContext(1)]
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      int num = Math.Min(blocks.Count, 3);
      double num2 = ((num == 1) ? 210.0 : ((contentWidth - (double)((num - 1) * 12)) / (double)num));
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(12.0, InkVerticalAlignment.Top, Array.Empty<UIElement>())
      {
        Margin = new Thickness(0.0, 6.0, 0.0, 6.0)
      };
      for (int i = 0; i < num; i++)
      {
        ClickyInkLiveNote.Block block = blocks[i];
        double num3 = ClickyInkClock.Progress(clock, block.LandsAt, block.LandsAt + 0.55);
        InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand((num == 1) ? 19.0 : 16.5), true, new Color?(ClickyInkStickies.StickyInk), null);
        inkTextView.Margin = new Thickness(12.0, 16.0, 12.0, 14.0);
        inkTextView.HorizontalAlignment = HorizontalAlignment.Left;
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(new Grid
        {
          Width = num2,
          MinHeight = 92.0,
          VerticalAlignment = VerticalAlignment.Top,
          Children = { inkTextView }
        }, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.Rectangle, "rect", ClickyInkStickies.Color(block.Source["color"], i), new PaperShadow(PaperColor.Black.Opacity(0.14), 3.0, 0.0, 2.0), null));
        Border border = new Border
        {
          Width = 44.0,
          Height = 13.0,
          Background = Ui.Brush(PaperColor.White.Opacity(0.55)),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Top
        };
        border.Transformed(0.0, -6.0, (double)((i % 2 == 0) ? (-3) : 4), 1.0, null);
        clickyInkOverlay.Children.Add(border);
        clickyInkOverlay.Transformed(0.0, 0.0, ClickyInkStickies.Tilt(i) * (1.0 + 1.5 * (1.0 - num3)), 1.0 + 0.12 * (1.0 - num3), null);
        clickyInkOverlay.Opacity = clock >= block.LandsAt;
        clickyInkHStack.Children.Add(clickyInkOverlay);
      }
      return ClickyInkObjectKit.Leading(clickyInkHStack, contentWidth);
    }

    // Token: 0x06001A96 RID: 6806 RVA: 0x005E3EF8 File Offset: 0x005E3EF8
    public static double Tilt(int index)
    {
      return (new double[] { -2.2, 1.6, -1.1 })[index % 3];
    }

    // Token: 0x06001A97 RID: 6807 RVA: 0x005E3F10 File Offset: 0x005E3F10
    [NullableContext(2)]
    public static Color Color(string name, int index)
    {
      string text = ((name != null) ? name.ToLowerInvariant() : null);
      if (text != null)
      {
        switch (text.Length)
        {
        case 4:
        {
          char c = text[0];
          if (c != 'b')
          {
            if (c != 'm')
            {
              if (c != 'p')
              {
                goto IL_0144;
              }
              if (!(text == "pink"))
              {
                goto IL_0144;
              }
              return PaperColor.Hex("#FFD3DE");
            }
            else if (!(text == "mint"))
            {
              goto IL_0144;
            }
          }
          else
          {
            if (!(text == "blue"))
            {
              goto IL_0144;
            }
            return PaperColor.Hex("#CFE6FF");
          }
          break;
        }
        case 5:
        {
          char c = text[0];
          if (c != 'g')
          {
            if (c != 'l')
            {
              if (c != 'p')
              {
                goto IL_0144;
              }
              if (!(text == "peach"))
              {
                goto IL_0144;
              }
              goto IL_012A;
            }
            else
            {
              if (!(text == "lilac"))
              {
                goto IL_0144;
              }
              goto IL_0137;
            }
          }
          else if (!(text == "green"))
          {
            goto IL_0144;
          }
          break;
        }
        case 6:
        {
          char c = text[0];
          if (c != 'o')
          {
            if (c != 'p')
            {
              goto IL_0144;
            }
            if (!(text == "purple"))
            {
              goto IL_0144;
            }
            goto IL_0137;
          }
          else
          {
            if (!(text == "orange"))
            {
              goto IL_0144;
            }
            goto IL_012A;
          }
          break;
        }
        default:
          goto IL_0144;
        }
        return PaperColor.Hex("#CDEFD6");
        IL_012A:
        return PaperColor.Hex("#FFD9BF");
        IL_0137:
        return PaperColor.Hex("#E2DAFC");
      }
      IL_0144:
      return (new Color[]
      {
        PaperColor.Hex("#FFE98A"),
        PaperColor.Hex("#FFD3DE"),
        PaperColor.Hex("#CFE6FF")
      })[index % 3];
    }

    // Token: 0x04000CC0 RID: 3264
    private static readonly Color StickyInk = PaperColor.Hex("#3A3226");
  }
}
