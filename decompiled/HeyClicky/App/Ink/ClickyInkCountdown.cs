using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000230 RID: 560
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkCountdown
  {
    // Token: 0x060019AA RID: 6570 RVA: 0x005D61C0 File Offset: 0x005D61C0
    public static Color Color(ClickyInkCountdownReading reading, ClickyInkStyle style)
    {
      if (reading.IsPast)
      {
        return ClickyInkDashboardPalette.Word(ClickyInkStatusState.Bad);
      }
      if (!reading.IsClose)
      {
        return style.Ink;
      }
      return ClickyInkDashboardPalette.Word(ClickyInkStatusState.Watch);
    }

    // Token: 0x060019AB RID: 6571 RVA: 0x005D61E8 File Offset: 0x005D61E8
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width)
    {
      ClickyInkDocument.Block source = block.Source;
      ClickyInkCountdownReading clickyInkCountdownReading = new ClickyInkCountdownReading(source);
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.15, block.LandsAt + 0.75);
      Color color = ClickyInkCountdown.Color(clickyInkCountdownReading, style);
      InkTextView inkTextView = InkTextView.Plain((clickyInkCountdownReading.Big.Length == 0) ? "?" : clickyInkCountdownReading.Big, ClickyPaperFonts.BigNumber((double)((clickyInkCountdownReading.Big.CharacterCount() > 2) ? 22 : 28)), color).With(delegate(InkTextView t)
      {
        t.MonospacedDigit = true;
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      InkTextView inkTextView2 = InkTextView.Plain(clickyInkCountdownReading.Unit, ClickyPaperFonts.Hand(13.0), style.Quiet).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
        t.MinimumScale = 0.7;
      });
      inkTextView2.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView2.Margin = new Thickness(0.0, -2.0, 0.0, 0.0);
      StackPanel stackPanel = new StackPanel
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { inkTextView, inkTextView2 }
      };
      Grid grid = new Grid
      {
        Width = 64.0,
        Height = 64.0,
        Children = { stackPanel }
      };
      grid.Children.Add(ClickyShapeView.Stroke(new ClickyHandRing(block.Id.CharacterCount(), 0.16).Trim(0.0, num), new PaperStroke((clickyInkCountdownReading.IsPast || clickyInkCountdownReading.IsClose) ? color : style.Fresh.Opacity(0.75), 2.0, PenLineCap.Round, PenLineJoin.Miter, null)));
      StackPanel stackPanel2 = new StackPanel();
      stackPanel2.Spacing = 2.0;
      stackPanel2.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(20.0), true, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      }));
      StackPanel stackPanel3 = stackPanel2;
      string dateLine = clickyInkCountdownReading.DateLine;
      if (dateLine != null)
      {
        stackPanel3.Children.Add(InkTextView.Plain(dateLine, ClickyPaperFonts.Typed(12.0, new FontWeight?(FontWeights.Medium)), style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
      }
      if (clickyInkCountdownReading.IsPast)
      {
        stackPanel3.Children.Add(ClickyInkTag.Make("past due", ClickyInkDashboardPalette.Word(ClickyInkStatusState.Bad)));
      }
      else
      {
        string text = source["note"];
        if (text != null && text.Length > 0)
        {
          stackPanel3.Children.Add(InkTextView.Plain(text, ClickyPaperFonts.Hand(15.0), style.Fresh).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
        }
      }
      return InkDashboardLayout.Width<ClickyInkHStack>(InkDashboardLayout.HStack(14.0, InkVerticalAlignment.Center, new UIElement[]
      {
        grid,
        ClickyInkHStack.Flexible<StackPanel>(stackPanel3),
        new ClickyInkSpacer()
      }), width);
    }
  }
}
