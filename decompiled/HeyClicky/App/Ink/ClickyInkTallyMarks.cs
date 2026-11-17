using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200022C RID: 556
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTallyMarks
  {
    // Token: 0x060019A2 RID: 6562 RVA: 0x005D546C File Offset: 0x005D546C
    public static int Capacity(double width, int rows)
    {
      return ClickyInkDashboardViewMath.TallyCapacity(width, rows);
    }

    // Token: 0x060019A3 RID: 6563 RVA: 0x005D5478 File Offset: 0x005D5478
    public static FrameworkElement Create(int count, int fresh = 0, double progress = 1.0, double appear = 1.0, double markHeight = 20.0, [Nullable(2)] ClickyInkStyle style = null, int seed = 1, double? width = null, double? height = null)
    {
      ClickyInkStyle inkStyle = style ?? new ClickyInkStyle(ClickyInkDashboardPalette.Pencil, ClickyInkDashboardPalette.Pencil.Opacity(0.58), null, null);
      return new ClickyInkDashboardCanvas(delegate(PaperDrawing drawing, Size size)
      {
        ClickyInkTallyMarks.Draw(drawing, size, count, fresh, progress, appear, markHeight, inkStyle, seed);
      }, width, height, true);
    }

    // Token: 0x060019A4 RID: 6564 RVA: 0x005D5508 File Offset: 0x005D5508
    private static void Draw(PaperDrawing drawing, Size size, int count, int fresh, double progress, double appear, double markHeight, ClickyInkStyle style, int seed)
    {
      int num = ClickyInkDashboardViewMath.TallyGroupsPerRow(size.Width);
      double num2 = markHeight + 8.0;
      int num3 = num * 5 * Math.Max(1, (int)((size.Height + 8.0) / num2));
      int num4 = Math.Min(count, num3);
      int num5 = num4 - Math.Min(fresh, num4);
      double num6 = (double)Math.Min(fresh, num4) * progress;
      for (int i = 0; i < num4; i++)
      {
        int num7 = i / 5;
        int num8 = i % 5;
        int num9 = num7 / num;
        double num10 = (double)(num7 % num) * 35.0;
        double num11 = (double)num9 * num2;
        ClickyInkTallyMarks.<>c__DisplayClass4_0 CS$<>8__locals1;
        CS$<>8__locals1.generator = new ClickyPaperRandom((ulong)((long)(seed * 131 + i * 7 + 3)));
        PaperPath paperPath = new PaperPath();
        if (num8 < 4)
        {
          double num12 = num10 + 3.0 + (double)num8 * 5.5;
          double num13 = num12 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          double num14 = num11 + 1.0 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.6, ref CS$<>8__locals1);
          paperPath.MoveTo(num13, num14);
          double num15 = num12 + 1.2 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          double num16 = num11 + markHeight + ClickyInkTallyMarks.<Draw>g__J|4_0(0.6, ref CS$<>8__locals1);
          double num17 = num12 + ClickyInkTallyMarks.<Draw>g__J|4_0(1.2, ref CS$<>8__locals1);
          paperPath.QuadTo(new Point(num15, num16), new Point(num17, num11 + markHeight / 2.0));
        }
        else
        {
          double num18 = num10 - 1.0 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          double num19 = num11 + markHeight * 0.78 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          paperPath.MoveTo(num18, num19);
          double num20 = num10 + 26.0 + 1.0 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          double num21 = num11 + markHeight * 0.18 + ClickyInkTallyMarks.<Draw>g__J|4_0(0.9, ref CS$<>8__locals1);
          paperPath.LineTo(num20, num21);
        }
        bool flag = i >= num5 && fresh > 0;
        double num22 = (flag ? Math.Min(1.0, num6 - (double)(i - num5)) : Math.Min(1.0, appear * (double)num4 / 1.5 - (double)i / 1.5 + 1.0));
        if (num22 > 0.0)
        {
          drawing.Stroke((num22 < 1.0) ? paperPath.Trimmed(0.0, num22) : paperPath, new PaperStroke(flag ? style.Fresh : ClickyInkDashboardPalette.Pencil.Opacity(0.85), 1.7, PenLineCap.Round, PenLineJoin.Miter, null));
        }
      }
      if (count > num3)
      {
        PaperFont paperFont = ClickyPaperFonts.Rounded(11.0, FontWeights.SemiBold);
        TextBlock textBlock = new TextBlock();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("+");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count - num3);
        textBlock.Text = defaultInterpolatedStringHandler.ToStringAndClear();
        textBlock.Foreground = Ui.Brush(style.Quiet);
        textBlock.IsTextScaleFactorEnabled = false;
        TextBlock textBlock2 = paperFont.Apply(textBlock);
        ClickyInkDashboardCanvas.DrawText(drawing, textBlock2, new Point(size.Width - 14.0, size.Height - 8.0));
      }
    }

    // Token: 0x060019A5 RID: 6565 RVA: 0x005D58AC File Offset: 0x005D58AC
    [CompilerGenerated]
    internal static double <Draw>g__J|4_0(double amount = 0.9, ref ClickyInkTallyMarks.<>c__DisplayClass4_0 A_1)
    {
      return A_1.generator.Next(-amount, amount);
    }

    // Token: 0x04000C66 RID: 3174
    public const double GroupWidth = 26.0;

    // Token: 0x04000C67 RID: 3175
    public const double GroupGap = 9.0;
  }
}
