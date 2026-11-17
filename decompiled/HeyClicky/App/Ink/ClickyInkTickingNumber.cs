using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000221 RID: 545
  public static class ClickyInkTickingNumber
  {
    // Token: 0x0600198B RID: 6539 RVA: 0x005D30F8 File Offset: 0x005D30F8
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkDocument.Block block, string key, double clock, ClickyInkStyle style, double size = 30.0, double delay = 0.0, [Nullable(2)] string value = null, [Nullable(2)] string previous = null, bool showsWas = true)
    {
      string text = value ?? block[key] ?? "";
      string text2 = previous ?? ClickyInkChange.Previous(block, key);
      double num = ClickyInkChange.Progress(block, clock, delay, 0.9);
      object obj = text2 != null && text2 != text && num < 1.0;
      bool flag;
      if (text2 != null && text2 != text)
      {
        int? num2 = ClickyInkChange.ChangedEdit(block);
        if (num2 != null)
        {
          int valueOrDefault = num2.GetValueOrDefault();
          int num3 = valueOrDefault;
          num2 = style.LatestEdit;
          flag = (num3 == num2.GetValueOrDefault()) & (num2 != null);
          goto IL_00A9;
        }
      }
      flag = false;
      IL_00A9:
      bool flag2 = flag;
      object obj2 = obj;
      string text3;
      if (obj2 == null)
      {
        text3 = text;
      }
      else if ((text3 = ClickyInkNumber.Interpolated(text2, text, num)) == null)
      {
        text3 = ((num < 0.35) ? (text2 ?? text) : text);
      }
      string text4 = text3;
      double valueOrDefault2 = ClickyInkChange.ChangedAt(block).GetValueOrDefault();
      double highlight = (flag2 ? ClickyInkClock.Progress(clock, valueOrDefault2 + delay + 0.7, valueOrDefault2 + delay + 1.1) : 0.0);
      Color color = (((obj2 | flag2) != null) ? style.Fresh : ((text4.Length == 0) ? style.Quiet : style.Ink));
      InkTextView inkTextView = InkTextView.Plain((text4.Length == 0) ? "–" : text4, ClickyPaperFonts.BigNumber(size), color).With(delegate(InkTextView t)
      {
        t.Tracking = -0.8;
        t.MonospacedDigit = true;
        t.LineLimit = new int?(1);
        t.MinimumScale = 0.45;
      });
      FrameworkElement frameworkElement = inkTextView;
      if (highlight > 0.0)
      {
        ClickyShapeView clickyShapeView = new ClickyShapeView(new ClickyShape((Rect r) => InkDashboardLayout.RotatedRoundedRect(new Rect(r.X - 2.0, r.Y + r.Height * 0.3, r.Width * highlight + 4.0, r.Height * 0.62), 3.0, -1.0)), Ui.Brush(style.Highlighter), null, false);
        frameworkElement = new ClickyInkOverlay(inkTextView, Array.Empty<UIElement>()).Behind(clickyShapeView);
      }
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(5.0, InkVerticalAlignment.FirstBaseline, new UIElement[] { frameworkElement });
      if (showsWas && flag2 && text2 != null && text2.Length > 0)
      {
        bool single = text2.CharacterCount() == 1;
        PaperFont paperFont = (single ? ClickyPaperFonts.Hand(Math.Max(12.0, size * 0.5)) : ClickyPaperFonts.BigNumber(Math.Max(11.0, size * 0.42)));
        InkTextView inkTextView2 = InkTextView.Plain(single ? ("from " + text2) : text2, paperFont, style.Quiet).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          if (!single)
          {
            t.Strikethrough = new Color?(style.Fresh);
          }
        });
        inkTextView2.Opacity = ClickyInkClock.Progress(clock, valueOrDefault2 + delay, valueOrDefault2 + delay + 0.3);
        clickyInkHStack.Children.Add(InkDashboardLayout.Fixed(inkTextView2));
      }
      return clickyInkHStack;
    }
  }
}
