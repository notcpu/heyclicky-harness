using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FF RID: 767
  public static class TopArcLayout
  {
    // Token: 0x0600218D RID: 8589 RVA: 0x0061CAEC File Offset: 0x0061CAEC
    private static double WidthFit(ArcScreenMetrics m)
    {
      return m.PanelWidth / 1512.0;
    }

    // Token: 0x0600218E RID: 8590 RVA: 0x0061CB00 File Offset: 0x0061CB00
    private static double ScaleFit(ArcScreenMetrics m)
    {
      return Math.Min(1.0, Math.Min(TopArcLayout.WidthFit(m), (m.PanelHeight - 120.0) / 760.0));
    }

    // Token: 0x0600218F RID: 8591 RVA: 0x0061CB38 File Offset: 0x0061CB38
    private static double ScaleAt(int distance, double fit)
    {
      return TopArcLayout.Scales[distance] * (0.85 + 0.15 * fit);
    }

    // Token: 0x06002190 RID: 8592 RVA: 0x0061CB58 File Offset: 0x0061CB58
    [return: TupleElementNames(new string[] { "W", "H" })]
    public static ValueTuple<double, double> FrontCardSize(double panelWidth, double panelHeight)
    {
      double num = TopArcLayout.ScaleAt(0, TopArcLayout.ScaleFit(new ArcScreenMetrics(panelWidth, panelHeight, 0.0, null, null)));
      return new ValueTuple<double, double>(494.0 * num, 314.0 * num);
    }

    // Token: 0x06002191 RID: 8593 RVA: 0x0061CBB0 File Offset: 0x0061CBB0
    public static double FrontTop(ArcScreenMetrics m)
    {
      double item = TopArcLayout.FrontCardSize(m.PanelWidth, m.PanelHeight).Item2;
      ArcRect? arcRect;
      return Math.Max(m.MenuBarHeight + 6.0, ((m.NotchSurface != null) ? arcRect.GetValueOrDefault().MaxY : 0.0) - 22.0 - 44.0 - item);
    }

    // Token: 0x06002192 RID: 8594 RVA: 0x0061CC30 File Offset: 0x0061CC30
    [return: TupleElementNames(new string[] { "X", "Y" })]
    public static ValueTuple<double, double> NotchMouth(ArcScreenMetrics m)
    {
      ArcRect? notchSurface = m.NotchSurface;
      ArcRect? arcRect = ((notchSurface != null) ? notchSurface : m.HardwareNotch);
      return new ValueTuple<double, double>((arcRect != null) ? arcRect.GetValueOrDefault().MidX : (m.PanelWidth / 2.0), Math.Max((arcRect != null) ? arcRect.GetValueOrDefault().MaxY : m.MenuBarHeight, m.MenuBarHeight) - 8.0);
    }

    // Token: 0x06002193 RID: 8595 RVA: 0x0061CCC0 File Offset: 0x0061CCC0
    public static double ArcDepth(ArcScreenMetrics m)
    {
      return TopArcLayout.FrontTop(m) + 314.0 * TopArcLayout.ScaleAt(0, TopArcLayout.ScaleFit(m)) + 90.0;
    }

    // Token: 0x06002194 RID: 8596 RVA: 0x0061CCEC File Offset: 0x0061CCEC
    public static ArcPlacement Placement(int index, int focus, int? hovered, ArcScreenMetrics m, bool isOpen)
    {
      double num = TopArcLayout.ScaleFit(m);
      double num2 = Math.Min(1.3, Math.Max(0.7, TopArcLayout.WidthFit(m)));
      int num3 = index - focus;
      int num4 = Math.Min(Math.Abs(num3), 3);
      int? num5 = hovered;
      bool flag = ((num5.GetValueOrDefault() == index) & (num5 != null)) && num3 != 0 && TopArcLayout.Opacities[num4] > 0.0;
      double num6 = TopArcLayout.ScaleAt(num4, num);
      double num7 = (flag ? Math.Max(num6, 0.44 * (0.85 + 0.15 * num)) : num6);
      ValueTuple<double, double> valueTuple = TopArcLayout.NotchMouth(m);
      double item = valueTuple.Item1;
      double num8 = ((num3 < 0) ? (item - 16.0) : (m.PanelWidth - 16.0 - item));
      double num9 = TopArcLayout.Spreads[2] * num2 + 494.0 * TopArcLayout.ScaleAt(2, num) / 2.0;
      double num10 = Math.Min(1.0, Math.Max(0.55, num8 / num9));
      double num11 = (flag ? (494.0 * (num7 - num6) / 2.0) : 0.0);
      double num12 = TopArcLayout.Spreads[num4] * num2 * num10 + num11;
      double num13 = 314.0 * num7;
      double num14 = TopArcLayout.FrontTop(m) - TopArcLayout.Lifts[num4] * num + (flag ? (10.0 * num) : 0.0);
      double num15 = (double)(-(double)Math.Max(-3, Math.Min(3, num3))) * 4.5 * (flag ? 0.35 : 1.0);
      if (!isOpen)
      {
        return new ArcPlacement(valueTuple.Item1, valueTuple.Item2, num7 * 0.2, 0.0, 0.0, (double)(10 - num4));
      }
      return new ArcPlacement(item + ((num3 < 0) ? (-num12) : num12), num14 + num13 / 2.0, num7, num15, TopArcLayout.Opacities[num4], (double)(10 - num4));
    }

    // Token: 0x06002195 RID: 8597 RVA: 0x0061CF40 File Offset: 0x0061CF40
    public static ArcRect VisibleFrame(int index, int focus, int? hovered, ArcScreenMetrics m)
    {
      TopArcLayout.<>c__DisplayClass23_0 CS$<>8__locals1;
      CS$<>8__locals1.focus = focus;
      CS$<>8__locals1.hovered = hovered;
      CS$<>8__locals1.m = m;
      ArcRect arcRect = TopArcLayout.<VisibleFrame>g__Frame|23_0(index, ref CS$<>8__locals1);
      int num = index - CS$<>8__locals1.focus;
      if (num == 0)
      {
        return arcRect;
      }
      ArcRect arcRect2 = TopArcLayout.<VisibleFrame>g__Frame|23_0(index + ((num < 0) ? 1 : (-1)), ref CS$<>8__locals1);
      if (num < 0)
      {
        return new ArcRect(arcRect.X, arcRect.Y, Math.Max(6.0, arcRect2.X - arcRect.X), arcRect.H);
      }
      double num2 = Math.Max(arcRect.X, arcRect2.MaxX);
      return new ArcRect(num2, arcRect.Y, Math.Max(6.0, arcRect.MaxX - num2), arcRect.H);
    }

    // Token: 0x06002196 RID: 8598 RVA: 0x0061D00C File Offset: 0x0061D00C
    [return: TupleElementNames(new string[] { "X", "Y" })]
    public static ValueTuple<double, double> MorePillCenter(ArcPlacement outermost, int direction, double panelWidth)
    {
      ArcRect frame = outermost.Frame;
      return new ValueTuple<double, double>((direction < 0) ? Math.Max(52.0, frame.X - 46.0) : Math.Min(panelWidth - 52.0, frame.MaxX + 46.0), frame.MidY);
    }

    // Token: 0x06002198 RID: 8600 RVA: 0x0061D0DC File Offset: 0x0061D0DC
    [CompilerGenerated]
    internal static ArcRect <VisibleFrame>g__Frame|23_0(int i, ref TopArcLayout.<>c__DisplayClass23_0 A_1)
    {
      return TopArcLayout.Placement(i, A_1.focus, A_1.hovered, A_1.m, true).Frame;
    }

    // Token: 0x0400101A RID: 4122
    [Nullable(1)]
    public static readonly double[] Scales = new double[] { 0.7, 0.27, 0.2, 0.16 };

    // Token: 0x0400101B RID: 4123
    public const double PeekScale = 0.44;

    // Token: 0x0400101C RID: 4124
    public const double PeekDrop = 10.0;

    // Token: 0x0400101D RID: 4125
    public const double PeekTiltKept = 0.35;

    // Token: 0x0400101E RID: 4126
    [Nullable(1)]
    public static readonly double[] Spreads = new double[] { 0.0, 275.0, 400.0, 510.0 };

    // Token: 0x0400101F RID: 4127
    [Nullable(1)]
    public static readonly double[] Lifts = new double[] { 0.0, 8.0, 15.0, 22.0 };

    // Token: 0x04001020 RID: 4128
    [Nullable(1)]
    public static readonly double[] Opacities = new double[] { 1.0, 1.0, 1.0, 0.0 };

    // Token: 0x04001021 RID: 4129
    public const int VisibleSideCount = 2;

    // Token: 0x04001022 RID: 4130
    public const double DegreesPerStep = 4.5;

    // Token: 0x04001023 RID: 4131
    public const double TopGap = 6.0;

    // Token: 0x04001024 RID: 4132
    public const double CardBottomPadding = 22.0;

    // Token: 0x04001025 RID: 4133
    public const double FooterHeight = 44.0;

    // Token: 0x04001026 RID: 4134
    public const double CardTopGap = 20.0;

    // Token: 0x04001027 RID: 4135
    public const double CardSidePadding = 28.0;

    // Token: 0x04001028 RID: 4136
    public const double EdgeMargin = 16.0;
  }
}
