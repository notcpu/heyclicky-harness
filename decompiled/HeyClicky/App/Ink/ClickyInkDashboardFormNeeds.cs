using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200021E RID: 542
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardFormNeeds
  {
    // Token: 0x06001971 RID: 6513 RVA: 0x005D2B0C File Offset: 0x005D2B0C
    public static Color Lamp(ClickyInkStatusState state)
    {
      return ClickyInkDashboardPalette.Lamp(state);
    }

    // Token: 0x06001972 RID: 6514 RVA: 0x005D2B14 File Offset: 0x005D2B14
    public static Color Word(ClickyInkStatusState state)
    {
      return ClickyInkDashboardPalette.Word(state);
    }

    // Token: 0x1700047E RID: 1150
    // (get) Token: 0x06001973 RID: 6515 RVA: 0x005D2B1C File Offset: 0x005D2B1C
    public static Color Up
    {
      get
      {
        return ClickyInkDashboardPalette.Up;
      }
    }

    // Token: 0x1700047F RID: 1151
    // (get) Token: 0x06001974 RID: 6516 RVA: 0x005D2B24 File Offset: 0x005D2B24
    public static Color Down
    {
      get
      {
        return ClickyInkDashboardPalette.Down;
      }
    }

    // Token: 0x17000480 RID: 1152
    // (get) Token: 0x06001975 RID: 6517 RVA: 0x005D2B2C File Offset: 0x005D2B2C
    public static Color Pencil
    {
      get
      {
        return ClickyInkDashboardPalette.Pencil;
      }
    }

    // Token: 0x06001976 RID: 6518 RVA: 0x005D2B34 File Offset: 0x005D2B34
    public static Color CountdownColor(ClickyInkCountdownReading reading, ClickyInkStyle style)
    {
      return ClickyInkCountdown.Color(reading, style);
    }

    // Token: 0x06001977 RID: 6519 RVA: 0x005D2B40 File Offset: 0x005D2B40
    public static FrameworkElement RunStampMark([Nullable(2)] string number, ClickyInkDocument.Block block, double clock, double landsAt, double fontSize = 11.0)
    {
      return ClickyInkRunStampMark.Create(number, block, clock, landsAt, fontSize);
    }

    // Token: 0x06001978 RID: 6520 RVA: 0x005D2B50 File Offset: 0x005D2B50
    public static FrameworkElement TickingNumber(ClickyInkDocument.Block block, string key, double clock, ClickyInkStyle style, double size = 30.0, double delay = 0.0, [Nullable(2)] string value = null, [Nullable(2)] string previous = null, bool showsWas = true)
    {
      return ClickyInkTickingNumber.Create(block, key, clock, style, size, delay, value, previous, showsWas);
    }

    // Token: 0x06001979 RID: 6521 RVA: 0x005D2B70 File Offset: 0x005D2B70
    public static FrameworkElement LampView(ClickyInkStatusState state, double diameter = 22.0, int seed = 1, double appear = 1.0)
    {
      return ClickyInkLamp.Create(state, null, 1.0, diameter, seed, appear);
    }

    // Token: 0x0600197A RID: 6522 RVA: 0x005D2B98 File Offset: 0x005D2B98
    public static FrameworkElement Quiet(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, double width, bool isHeadline = false)
    {
      return ClickyInkQuiet.Create(block, clock, style, width, isHeadline);
    }

    // Token: 0x0600197B RID: 6523 RVA: 0x005D2BA8 File Offset: 0x005D2BA8
    public static FrameworkElement Tiles(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style, double width, ClickyInkTiles.Look look = ClickyInkTiles.Look.Ruled)
    {
      return ClickyInkTiles.Create(blocks, clock, style, width, null, look);
    }

    // Token: 0x0600197C RID: 6524 RVA: 0x005D2BCC File Offset: 0x005D2BCC
    public static FrameworkElement Tile(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, int index)
    {
      return ClickyInkTile.Create(block, clock, style, index, ClickyInkTiles.Look.Ruled, null);
    }

    // Token: 0x0600197D RID: 6525 RVA: 0x005D2BEC File Offset: 0x005D2BEC
    public static string TileStateWord(string word)
    {
      return ClickyInkTile.StateWord(word);
    }

    // Token: 0x0600197E RID: 6526 RVA: 0x005D2BF4 File Offset: 0x005D2BF4
    public static double TileHeight(ClickyInkDocument.Block block)
    {
      return ClickyInkTile.Height(block, null, false);
    }

    // Token: 0x0600197F RID: 6527 RVA: 0x005D2C14 File Offset: 0x005D2C14
    public static FrameworkElement SparkLine(IReadOnlyList<double> values, ClickyInkStyle style, int seed = 1, double height = 12.0)
    {
      IReadOnlyList<double> readOnlyList = null;
      double num = 1.0;
      double num2 = 1.0;
      double? num3 = new double?(height);
      return ClickyInkSparkLine.Create(values, readOnlyList, num, num2, null, style, null, seed, true, null, num3);
    }

    // Token: 0x06001980 RID: 6528 RVA: 0x005D2C68 File Offset: 0x005D2C68
    public static FrameworkElement ProgressBar(double fraction, double height, ClickyInkStyle style, int seed = 1)
    {
      return ClickyInkProgressBar.Create(fraction, null, 1.0, 1.0, height, style, seed, null);
    }
  }
}
