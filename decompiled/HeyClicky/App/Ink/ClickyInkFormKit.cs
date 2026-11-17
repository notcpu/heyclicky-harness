using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200021C RID: 540
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkFormKit
  {
    // Token: 0x06001963 RID: 6499 RVA: 0x005D2484 File Offset: 0x005D2484
    public static InkTextView Text(string text, PaperFont font, Color color, double tracking = 0.0, double minimumScale = 1.0, int? lineLimit = 1)
    {
      return InkTextView.Plain(text, font, color).With(delegate(InkTextView t)
      {
        t.Tracking = tracking;
        t.MinimumScale = minimumScale;
        t.LineLimit = lineLimit;
      });
    }

    // Token: 0x06001964 RID: 6500 RVA: 0x005D24C8 File Offset: 0x005D24C8
    public static FrameworkElement MaxWidthOf(this FrameworkElement e, double width)
    {
      e.MaxWidth = width;
      return e;
    }

    // Token: 0x06001965 RID: 6501 RVA: 0x005D24D4 File Offset: 0x005D24D4
    public static FrameworkElement Pill(string words, PaperFont font, Color color, Color stroke, double padding, double lineWidth = 1.0, double degrees = -1.5)
    {
      InkTextView inkTextView = ClickyInkFormKit.Text(words, font, color, 0.0, 1.0, new int?(1));
      inkTextView.Margin = new Thickness(padding, 0.0, padding, 0.0);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(ClickyShape.Capsule, stroke, lineWidth);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView })
      {
        HorizontalAlignment = HorizontalAlignment.Left
      };
      if (degrees != 0.0)
      {
        clickyInkOverlay.Transformed(0.0, 0.0, degrees, 1.0, null);
      }
      return clickyInkOverlay;
    }

    // Token: 0x06001966 RID: 6502 RVA: 0x005D2584 File Offset: 0x005D2584
    public static FrameworkElement Chip(string words, Color color)
    {
      return ClickyInkFormKit.Pill(words, ClickyPaperFonts.Hand(12.5), color, color.Opacity(0.45), 4.0, 0.8, 0.0);
    }

    // Token: 0x06001967 RID: 6503 RVA: 0x005D25D0 File Offset: 0x005D25D0
    [return: Nullable(2)]
    public static FrameworkElement Changes(ClickyInkDashboardBoard.ChangeCounts changes, ClickyInkStyle style, double size, double padding)
    {
      ClickyInkFormKit.<>c__DisplayClass4_0 CS$<>8__locals1;
      CS$<>8__locals1.size = size;
      CS$<>8__locals1.padding = padding;
      if (changes.Total <= 0)
      {
        return null;
      }
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      if (changes.New > 0)
      {
        UIElementCollection children = stackPanel.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.New);
        defaultInterpolatedStringHandler.AppendLiteral(" new");
        children.Add(ClickyInkFormKit.<Changes>g__Piece|4_0(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperInk.DoneGreen, ref CS$<>8__locals1));
      }
      if (changes.Updated > 0)
      {
        UIElementCollection children2 = stackPanel.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.Updated);
        defaultInterpolatedStringHandler.AppendLiteral(" updated");
        children2.Add(ClickyInkFormKit.<Changes>g__Piece|4_0(defaultInterpolatedStringHandler.ToStringAndClear(), style.Fresh, ref CS$<>8__locals1));
      }
      if (changes.Gone > 0)
      {
        UIElementCollection children3 = stackPanel.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.Gone);
        defaultInterpolatedStringHandler.AppendLiteral(" gone");
        children3.Add(ClickyInkFormKit.<Changes>g__Piece|4_0(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyInkDashboardFormNeeds.Down, ref CS$<>8__locals1));
      }
      return stackPanel;
    }

    // Token: 0x06001968 RID: 6504 RVA: 0x005D26F8 File Offset: 0x005D26F8
    public static FrameworkElement SourceLink(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, double arrowSize, double wordsSize)
    {
      bool flag = block.IsStruck(clock);
      InkTextView inkTextView = new InkTextView(ClickyInkWords.Text(block.Words, clock, style, false, new Color?(flag ? style.Quiet : style.Ink.Opacity(0.85)), null), ClickyPaperFonts.Typed(wordsSize, new FontWeight?(FontWeights.Medium)), new Color?(style.Ink))
      {
        LineLimit = new int?(1)
      };
      if (flag)
      {
        inkTextView.Strikethrough = new Color?(style.Fresh);
      }
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(4.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        ClickyInkFormKit.Text("↗", ClickyPaperFonts.Typed(arrowSize, new FontWeight?(FontWeights.Bold)), style.Fresh, 0.0, 1.0, new int?(1)),
        inkTextView
      });
      Pressable pressable = new Pressable(true);
      pressable.Children.Add(clickyInkHStack);
      pressable.Click += delegate
      {
        string text = block.Source["url"];
        if (text != null)
        {
          actions.Open(text);
        }
      };
      return pressable;
    }

    // Token: 0x06001969 RID: 6505 RVA: 0x005D2824 File Offset: 0x005D2824
    public static FrameworkElement Squiggle(int seed, Color color)
    {
      return ClickyInkHStack.Flexible<ClickyInkFill>(new ClickyInkFill(ClickyShapeView.Stroke(new ClickyInkSquiggle(seed), new PaperStroke(color, 1.2, PenLineCap.Flat, PenLineJoin.Miter, null)), 6.0));
    }

    // Token: 0x0600196A RID: 6506 RVA: 0x005D2858 File Offset: 0x005D2858
    public static PaperWash Dots(double spacing, double radius)
    {
      return new PaperWash(PaperFill.Of(PaperColor.Hex("#3C5AA0").Opacity(0.16)), null, true, delegate(Size size)
      {
        PaperPath paperPath = new PaperPath();
        for (double num = spacing; num < size.Height; num += spacing)
        {
          for (double num2 = spacing; num2 < size.Width; num2 += spacing)
          {
            paperPath.AddEllipse(new Rect(num2 - radius, num - radius, radius * 2.0, radius * 2.0));
          }
        }
        return paperPath;
      });
    }

    // Token: 0x0600196B RID: 6507 RVA: 0x005D28AC File Offset: 0x005D28AC
    public static double MeasureHeight(FrameworkElement view, double width)
    {
      Grid grid = new Grid();
      grid.Width = width;
      grid.Children.Add(view);
      grid.Measure(new Size(width, double.PositiveInfinity));
      return grid.DesiredSize.Height;
    }

    // Token: 0x0600196C RID: 6508 RVA: 0x005D28F4 File Offset: 0x005D28F4
    public static void WithoutPenRenderer(UIElement root)
    {
      InkTextView inkTextView = root as InkTextView;
      if (inkTextView != null)
      {
        ClickyInkTextContent content = inkTextView.Content;
        if (content.PenRun < 0)
        {
          if (!content.Runs.Any<ClickyInkRun>((ClickyInkRun r) => r.IsLink))
          {
            return;
          }
        }
        ClickyInkTextContent clickyInkTextContent = new ClickyInkTextContent(content.Runs.Select<ClickyInkRun, ClickyInkRun>(delegate(ClickyInkRun r)
        {
          ClickyInkRun clickyInkRun = r;
          clickyInkRun.IsLink = false;
          return clickyInkRun;
        }));
        inkTextView.Content = clickyInkTextContent;
        return;
      }
      Panel panel = root as Panel;
      if (panel != null)
      {
        foreach (UIElement uielement in panel.Children)
        {
          ClickyInkFormKit.WithoutPenRenderer(uielement);
        }
        return;
      }
      Border border = root as Border;
      if (border != null)
      {
        UIElement child = border.Child;
        if (child == null)
        {
          return;
        }
        ClickyInkFormKit.WithoutPenRenderer(child);
        return;
      }
      else
      {
        ContentControl contentControl = root as ContentControl;
        if (contentControl == null)
        {
          return;
        }
        UIElement uielement2 = contentControl.Content as UIElement;
        if (uielement2 == null)
        {
          return;
        }
        ClickyInkFormKit.WithoutPenRenderer(uielement2);
        return;
      }
    }

    // Token: 0x0600196D RID: 6509 RVA: 0x005D2A24 File Offset: 0x005D2A24
    [CompilerGenerated]
    internal static FrameworkElement <Changes>g__Piece|4_0(string words, Color color, ref ClickyInkFormKit.<>c__DisplayClass4_0 A_2)
    {
      return ClickyInkFormKit.Pill(words, ClickyPaperFonts.Hand(A_2.size), color, color.Opacity(0.45), A_2.padding, 1.0, -1.5);
    }
  }
}
