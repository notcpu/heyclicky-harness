using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000274 RID: 628
  public static class ClickyInkMadeLinkCard
  {
    // Token: 0x06001ACC RID: 6860 RVA: 0x005E7298 File Offset: 0x005E7298
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, double? width = null)
    {
      double valueOrDefault = width.GetValueOrDefault(378.0);
      string url = block.Source["url"] ?? "";
      ClickyInkMadeLink.Kind kind = ClickyInkMadeLink.KindOf(url, block.Words.Current);
      Color color = PaperColor.Hex(kind.TintHex);
      bool flag = block.IsStruck(clock);
      Grid grid = new Grid
      {
        Width = 86.0,
        Height = 68.0
      };
      grid.Children.Add(ClickyShapeView.Fill(ClickyShape.RoundedRectangle(6.0), PaperColor.White.Opacity(0.85)));
      grid.Children.Add(ClickyShapeView.Fill(ClickyShape.RoundedRectangle(6.0), color.Opacity(0.08)));
      TextBlock textBlock = ClickyInkIcon.Glyph(kind.Symbol, 26.0, color.Opacity(0.85), false);
      if (textBlock != null)
      {
        grid.Children.Add(textBlock);
      }
      grid.Children.Add(ClickyShapeView.Stroke(ClickyShape.RoundedRectangle(6.0), style.Ink.Opacity(0.12), 1.0));
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(grid, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.RoundedRectangle(6.0), "rr6", PaperColor.Clear, new PaperShadow(PaperColor.Black.Opacity(0.1), 2.0, 0.0, 1.0), null));
      clickyInkOverlay.Transformed(0.0, 0.0, -1.5, 1.0, null);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 4.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel.Children.Add(InkTextView.Plain(kind.Name.ToUpperInvariant(), ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.SemiBold)), style.Quiet).With(delegate(InkTextView t)
      {
        t.Tracking = 0.6;
        t.LineLimit = new int?(1);
      }));
      InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(18.0), true, flag ? new Color?(style.Quiet) : null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      });
      if (flag)
      {
        inkTextView.Strikethrough = new Color?(style.Fresh);
      }
      stackPanel.Children.Add(inkTextView);
      ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton("Open", delegate
      {
        if (url.Length > 0)
        {
          actions.Open(url);
        }
      }, ClickyPaperGel.Blue, 26.0, 13.0, 12.0, null)
      {
        Enabled = (!flag && url.Length != 0)
      };
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[] { clickyPaperGelButton });
      if (url.Length > 0)
      {
        clickyInkHStack.Children.Add(InkTextView.Plain(ClickyInkClipping.Domain(url), ClickyPaperFonts.Typed(11.0, null), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
      }
      clickyInkHStack.Opacity = clock >= block.Words.FinishedAt;
      clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
      stackPanel.Children.Add(clickyInkHStack);
      return ClickyInkObjectKit.Leading(new ClickyInkHStack(12.0, InkVerticalAlignment.Center, new UIElement[]
      {
        clickyInkOverlay,
        ClickyInkHStack.Flexible<StackPanel>(stackPanel),
        new ClickyInkSpacer()
      }), valueOrDefault);
    }
  }
}
