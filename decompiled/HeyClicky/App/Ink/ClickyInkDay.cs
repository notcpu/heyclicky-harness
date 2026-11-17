using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000278 RID: 632
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDay
  {
    // Token: 0x06001AD4 RID: 6868 RVA: 0x005E8154 File Offset: 0x005E8154
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkDayLabel clickyInkDayLabel = new ClickyInkDayLabel(block.Words.Current);
      if (clickyInkDayLabel.Day == null)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(7.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        TextBlock textBlock = ClickyInkIcon.Glyph("calendar", 13.0, ClickyInkDay.Red, true);
        if (textBlock != null)
        {
          clickyInkHStack.Children.Add(textBlock);
        }
        clickyInkHStack.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(20.0), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
        return ClickyInkObjectKit.Leading(new StackPanel
        {
          Spacing = 8.0,
          Children = 
          {
            clickyInkHStack,
            ClickyInkDay.Entries(block, clock, style)
          }
        }, contentWidth);
      }
      string text;
      if ((text = clickyInkDayLabel.Weekday) == null)
      {
        text = clickyInkDayLabel.Month ?? "";
      }
      string text2 = text;
      string text3 = ((clickyInkDayLabel.Weekday != null) ? (clickyInkDayLabel.Month ?? "") : "");
      StackPanel stackPanel = new StackPanel
      {
        Width = 64.0
      };
      stackPanel.Children.Add(ClickyInkDay.Band(text2.ToUpperInvariant(), ClickyPaperFonts.Typed(10.0, new FontWeight?(FontWeights.Bold)), PaperColor.White, 1.0, ClickyInkDay.Red, 20.0));
      stackPanel.Children.Add(ClickyInkDay.Band(clickyInkDayLabel.Day, ClickyInkObjectFonts.Rounded(30.0, FontWeights.ExtraBold), ClickyInkDay.DayInk, 0.0, PaperColor.White, 44.0));
      stackPanel.Children.Add(ClickyInkDay.Band(text3, ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.SemiBold)), ClickyInkDay.DayInk.Opacity(0.55), 0.8, PaperColor.White, 16.0));
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(new Grid
      {
        Width = 64.0,
        CornerRadius = new CornerRadius(7.0),
        Children = { stackPanel }
      }, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.RoundedRectangle(7.0), "rr7", PaperColor.Clear, new PaperShadow(PaperColor.Black.Opacity(0.12), 2.5, 0.0, 1.5), null));
      clickyInkOverlay.Transformed(0.0, 0.0, -2.0, 1.0, null);
      UIElement uielement = clickyInkOverlay;
      string caption = clickyInkDayLabel.Caption;
      if (caption != null)
      {
        InkTextView inkTextView = InkTextView.Plain(caption, ClickyPaperFonts.Hand(13.0), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
          t.TextAlignment = TextAlignment.Center;
        });
        inkTextView.Width = 78.0;
        uielement = new ClickyInkUnder(clickyInkOverlay, inkTextView, 34.0);
      }
      FrameworkElement frameworkElement = ClickyInkDay.Entries(block, clock, style);
      frameworkElement.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
      return ClickyInkObjectKit.Leading(new ClickyInkHStack(14.0, InkVerticalAlignment.Top, new UIElement[]
      {
        uielement,
        ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement)
      }), contentWidth);
    }

    // Token: 0x06001AD5 RID: 6869 RVA: 0x005E8540 File Offset: 0x005E8540
    private static FrameworkElement Band(string text, PaperFont font, Color color, double tracking, Color fill, double height)
    {
      InkTextView inkTextView = InkTextView.Plain(text, font, color).With(delegate(InkTextView t)
      {
        t.Tracking = tracking;
        t.LineLimit = new int?(1);
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView.VerticalAlignment = VerticalAlignment.Center;
      return new Grid
      {
        Width = 64.0,
        Height = height,
        Background = Ui.Brush(fill),
        Children = { inkTextView }
      };
    }

    // Token: 0x06001AD6 RID: 6870 RVA: 0x005E85B8 File Offset: 0x005E85B8
    private static FrameworkElement Entries(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      bool flag = block.Source.Items.Any<ClickyInkDocument.Item>((ClickyInkDocument.Item i) => ((i.Cells.Count > 0) ? i.Cells[0] : "").Length > 0);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 7.0
      };
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        if (clock >= item.LandsAt)
        {
          FrameworkElement frameworkElement;
          if (flag)
          {
            InkTextView inkTextView = InkTextView.Plain((item.Source.Cells.Count > 0) ? item.Source.Cells[0] : "", ClickyInkObjectFonts.Rounded(11.5, FontWeights.SemiBold), style.Fresh).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
              t.TextAlignment = TextAlignment.Right;
              t.FillsWidth = true;
            });
            inkTextView.Width = 58.0;
            frameworkElement = inkTextView;
          }
          else
          {
            frameworkElement = InkTextView.Plain("–", ClickyPaperFonts.Hand(17.0), style.Quiet);
          }
          StackPanel stackPanel2 = new StackPanel
          {
            Spacing = 1.0
          };
          UIElementCollection children = stackPanel2.Children;
          ClickyInkLiveNote.Words words = item.Words;
          PaperFont paperFont = ClickyPaperFonts.Hand(18.0);
          double? goneAt = item.GoneAt;
          children.Add(ClickyInkLine.Make(words, clock, style, paperFont, null, goneAt, null, true));
          ClickyInkLiveNote.Words detail = item.Detail;
          if (detail != null && ClickyInkWords.IsStarted(detail, clock))
          {
            stackPanel2.Children.Add(new InkTextView(ClickyInkWords.Text(detail, clock, style, false, new Color?(style.Quiet), null), ClickyPaperFonts.Typed(11.5, null), new Color?(style.Ink))
            {
              LineLimit = new int?(2)
            });
          }
          stackPanel.Children.Add(new ClickyInkHStack(10.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
          {
            frameworkElement,
            ClickyInkHStack.Flexible<StackPanel>(stackPanel2)
          }));
        }
      }
      return stackPanel;
    }

    // Token: 0x04000CD8 RID: 3288
    private static readonly Color Red = PaperColor.Hex("#E5484D");

    // Token: 0x04000CD9 RID: 3289
    private static readonly Color DayInk = PaperColor.Hex("#2B2620");
  }
}
