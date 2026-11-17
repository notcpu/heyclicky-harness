using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200021F RID: 543
  public static class ClickyInkDashboardPalette
  {
    // Token: 0x06001981 RID: 6529 RVA: 0x005D2CA8 File Offset: 0x005D2CA8
    public static Color Lamp(ClickyInkStatusState state)
    {
      string text;
      switch (state)
      {
      case ClickyInkStatusState.Ok:
        text = "#35A85E";
        break;
      case ClickyInkStatusState.Watch:
        text = "#F2A532";
        break;
      case ClickyInkStatusState.Bad:
        text = "#E5484D";
        break;
      default:
        text = "#A9AEBA";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x06001982 RID: 6530 RVA: 0x005D2CF0 File Offset: 0x005D2CF0
    public static Color Word(ClickyInkStatusState state)
    {
      Color color;
      switch (state)
      {
      case ClickyInkStatusState.Ok:
        color = ClickyPaperInk.DoneGreen;
        break;
      case ClickyInkStatusState.Watch:
        color = PaperColor.Hex("#B05F00");
        break;
      case ClickyInkStatusState.Bad:
        color = PaperColor.Hex("#C2302B");
        break;
      default:
        color = PaperColor.Hex("#6B7285");
        break;
      }
      return color;
    }

    // Token: 0x06001983 RID: 6531 RVA: 0x005D2D40 File Offset: 0x005D2D40
    [NullableContext(1)]
    public static Color DeltaColor(double delta, ClickyInkDocument.Block block)
    {
      bool? flag = ClickyInkDashboardViewMath.DeltaIsGoodNews(delta, block);
      Color color;
      if (flag != null)
      {
        if (!flag.GetValueOrDefault())
        {
          color = ClickyInkDashboardPalette.Down;
        }
        else
        {
          color = ClickyInkDashboardPalette.Up;
        }
      }
      else
      {
        color = ClickyInkDashboardPalette.Level;
      }
      return color;
    }

    // Token: 0x04000C5D RID: 3165
    public static readonly Color Up = ClickyPaperInk.DoneGreen;

    // Token: 0x04000C5E RID: 3166
    public static readonly Color Down = PaperColor.Hex("#C2410C");

    // Token: 0x04000C5F RID: 3167
    public static readonly Color Pencil = PaperColor.Hex("#3A3630");

    // Token: 0x04000C60 RID: 3168
    public static readonly Color Level = PaperColor.Hex("#6B7285");
  }
}
