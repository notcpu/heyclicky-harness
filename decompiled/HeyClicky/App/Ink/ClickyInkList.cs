using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200028C RID: 652
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkList
  {
    // Token: 0x06001B48 RID: 6984 RVA: 0x005ED130 File Offset: 0x005ED130
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, double? blockGoneAt = null)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 7.0,
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      for (int i = 0; i < block.Items.Count; i++)
      {
        ClickyInkLiveNote.Item item = block.Items[i];
        stackPanel.Children.Add(ClickyInkGrow.Around(ClickyInkList.Row(block, item, i, clock, style, actions, blockGoneAt), clock, item.LandsAt, item.VanishesAt));
      }
      return stackPanel;
    }

    // Token: 0x06001B49 RID: 6985 RVA: 0x005ED1BC File Offset: 0x005ED1BC
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, int offset, double clock, ClickyInkStyle style, ClickyInkActions actions, double? blockGoneAt)
    {
      bool flag = item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Gone;
      double? num = item.GoneAt;
      double? num3;
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        double num2;
        if (blockGoneAt != null)
        {
          double valueOrDefault2 = blockGoneAt.GetValueOrDefault();
          num2 = Math.Min(valueOrDefault, valueOrDefault2);
        }
        else
        {
          num2 = valueOrDefault;
        }
        num3 = new double?(num2);
      }
      else
      {
        num3 = blockGoneAt;
      }
      double? num4 = num3;
      bool flag2;
      if (num4 != null)
      {
        double valueOrDefault3 = num4.GetValueOrDefault();
        if (clock >= valueOrDefault3)
        {
          flag2 = true;
          goto IL_009B;
        }
      }
      flag2 = flag && clock >= item.Words.FinishedAt;
      IL_009B:
      bool flag3 = flag2;
      ClickyInkLiveNote.Words words = item.Words;
      PaperFont paperFont = ClickyPaperFonts.Hand(19.0);
      num = (flag ? new double?(item.Words.FinishedAt) : num4);
      InkTextView inkTextView = ClickyInkLine.Make(words, clock, style, paperFont, null, num, null, false);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(6.0, InkVerticalAlignment.FirstBaseline, new UIElement[] { inkTextView });
      if (item.Source.Url != null && !flag3 && clock >= item.Words.FinishedAt && !item.Words.Current.Contains('\ue005'))
      {
        TextBlock textBlock = ClickyInkIcon.Glyph("arrow.up.right", 10.0, ClickyPaperInk.ClickyBlue.Opacity(0.8), true);
        AutomationProperties.SetName(textBlock, "link");
        clickyInkHStack.Children.Add(textBlock.BaselineFromBottom(1.0));
      }
      ClickyInkDocument.Mark? mark = item.Source.Mark;
      if (mark != null)
      {
        ClickyInkDocument.Mark valueOrDefault4 = mark.GetValueOrDefault();
        if ((!flag3 || flag) && clock >= item.Words.FinishedAt)
        {
          FrameworkElement frameworkElement = ClickyInkList.Tag(valueOrDefault4, style);
          if (valueOrDefault4 == ClickyInkDocument.Mark.Now)
          {
            num = style.RestingSince(clock);
            if (num != null)
            {
              double valueOrDefault5 = num.GetValueOrDefault();
              FrameworkElement frameworkElement2 = ClickyInkRestingPencil.Make(clock, valueOrDefault5, style.Ink, 15.0);
              frameworkElement2.HorizontalAlignment = HorizontalAlignment.Right;
              frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
              frameworkElement2.Transformed(42.0, 0.0, 0.0, 1.0, null);
              frameworkElement = new ClickyInkOverlay(frameworkElement, new UIElement[] { frameworkElement2 });
            }
          }
          clickyInkHStack.Children.Add(frameworkElement);
        }
      }
      clickyInkHStack.Children.Add(new ClickyInkSpacer());
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 2.0
      };
      stackPanel.Children.Add(clickyInkHStack);
      ClickyInkLiveNote.Words detail = item.Detail;
      if (detail != null && ClickyInkWords.IsStarted(detail, clock))
      {
        stackPanel.Children.Add(new InkTextView(ClickyInkWords.Text(detail, clock, style, false, new Color?(style.Quiet), null), ClickyPaperFonts.Typed(12.0, null), new Color?(style.Ink))
        {
          LineLimit = new int?(3)
        });
      }
      ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(9.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        ClickyInkList.Marker(block, item, offset, clock, style),
        ClickyInkHStack.Flexible<StackPanel>(stackPanel)
      });
      string url = item.Source.Url;
      if (url != null)
      {
        Pressable pressable = new Pressable(true);
        pressable.Children.Add(clickyInkHStack2);
        pressable.Click += delegate
        {
          actions.Open(url);
        };
        return pressable;
      }
      return clickyInkHStack2;
    }

    // Token: 0x06001B4A RID: 6986 RVA: 0x005ED564 File Offset: 0x005ED564
    private static FrameworkElement Marker(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, int offset, double clock, ClickyInkStyle style)
    {
      ClickyInkDocument.Kind kind = block.Source.Kind;
      if (kind == ClickyInkDocument.Kind.Checklist)
      {
        Grid grid = new Grid
        {
          Width = 16.0,
          Height = 16.0
        };
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkBox(offset), style.Ink.Opacity(0.55), 1.3);
        clickyShapeView.Width = 14.0;
        clickyShapeView.Height = 14.0;
        grid.Children.Add(clickyShapeView);
        if (item.Source.IsChecked.GetValueOrDefault())
        {
          double num = item.Since((ClickyInkDocument.Item i) => i.IsChecked.GetValueOrDefault()) ?? item.LandsAt;
          ClickyShapeView clickyShapeView2 = ClickyShapeView.Stroke(new ClickyInkTick().Trim(0.0, ClickyInkClock.EaseInOut(ClickyInkClock.Progress(clock, num, num + 0.3))), new PaperStroke(ClickyPaperInk.DoneGreen, 2.2, PenLineCap.Round, PenLineJoin.Round, null));
          clickyShapeView2.Width = 15.0;
          clickyShapeView2.Height = 14.0;
          clickyShapeView2.Transformed(2.0, -2.0, 0.0, 1.0, null);
          grid.Children.Add(clickyShapeView2);
        }
        else if (item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Now)
        {
          grid.Children.Add(new Ellipse
          {
            Width = 6.0,
            Height = 6.0,
            Fill = Ui.Brush(style.Fresh)
          });
        }
        return grid.BaselineFromBottom(2.0);
      }
      if (kind == ClickyInkDocument.Kind.Numbered)
      {
        InkTextView inkTextView = InkTextView.Plain((item.Source.Number ?? (offset + 1)).ToString(CultureInfo.InvariantCulture), ClickyPaperFonts.Hand(15.0), style.Fresh);
        inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
        inkTextView.VerticalAlignment = VerticalAlignment.Center;
        ClickyShapeView clickyShapeView3 = ClickyShapeView.Stroke(new ClickyHandRing(offset, 0.16), style.Fresh.Opacity(0.6), 1.2);
        return new ClickyInkOverlay(new ClickyInkCentered(inkTextView, 20.0, 20.0), new UIElement[] { clickyShapeView3 });
      }
      if (item.Source.Mark.GetValueOrDefault() != ClickyInkDocument.Mark.Pick)
      {
        return InkTextView.Plain("–", ClickyPaperFonts.Hand(19.0), style.Quiet);
      }
      return InkTextView.Plain("★", ClickyPaperFonts.Typed(13.0, null), style.Fresh);
    }

    // Token: 0x06001B4B RID: 6987 RVA: 0x005ED888 File Offset: 0x005ED888
    public static FrameworkElement Tag(ClickyInkDocument.Mark mark, ClickyInkStyle style)
    {
      FrameworkElement frameworkElement;
      switch (mark)
      {
      case ClickyInkDocument.Mark.New:
        frameworkElement = ClickyInkTag.Make("new", ClickyPaperInk.DoneGreen);
        break;
      case ClickyInkDocument.Mark.Needs:
        frameworkElement = ClickyInkTag.Make("needs you", PaperColor.Hex("#B86A00"));
        break;
      case ClickyInkDocument.Mark.Done:
        frameworkElement = ClickyInkTag.Make("done", ClickyPaperInk.DoneGreen);
        break;
      case ClickyInkDocument.Mark.Pick:
        frameworkElement = ClickyInkTag.Make("the pick", style.Fresh);
        break;
      case ClickyInkDocument.Mark.Now:
        frameworkElement = ClickyInkTag.Make("now", style.Fresh);
        break;
      default:
        frameworkElement = ClickyInkTag.Make("ruled out", PaperColor.Hex("#C2410C"));
        break;
      }
      return frameworkElement;
    }
  }
}
