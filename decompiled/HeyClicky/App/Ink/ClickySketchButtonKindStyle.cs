using System;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F2 RID: 498
  public static class ClickySketchButtonKindStyle
  {
    // Token: 0x06001846 RID: 6214 RVA: 0x005CA16C File Offset: 0x005CA16C
    public static Color Fill(this ClickySketchButtonKind kind)
    {
      Color color;
      if (kind != ClickySketchButtonKind.Teal)
      {
        if (kind != ClickySketchButtonKind.Plain)
        {
          color = PaperColor.White.Opacity(0.55);
        }
        else
        {
          color = PaperColor.White.Opacity(0.92);
        }
      }
      else
      {
        color = ClickySketchInk.Teal;
      }
      return color;
    }

    // Token: 0x06001847 RID: 6215 RVA: 0x005CA1B8 File Offset: 0x005CA1B8
    public static Color Pencil(this ClickySketchButtonKind kind)
    {
      Color color;
      if (kind != ClickySketchButtonKind.Teal)
      {
        if (kind != ClickySketchButtonKind.Plain)
        {
          color = PaperColor.Hex("#9AA3B5");
        }
        else
        {
          color = PaperColor.Hex("#8FA9C9");
        }
      }
      else
      {
        color = ClickySketchInk.TealLine;
      }
      return color;
    }

    // Token: 0x06001848 RID: 6216 RVA: 0x005CA1F0 File Offset: 0x005CA1F0
    public static Color Words(this ClickySketchButtonKind kind)
    {
      Color color;
      if (kind != ClickySketchButtonKind.Teal)
      {
        if (kind != ClickySketchButtonKind.Plain)
        {
          color = PaperColor.Hex("#4A5570");
        }
        else
        {
          color = PaperColor.Hex("#2E4A72");
        }
      }
      else
      {
        color = ClickySketchInk.TealWords;
      }
      return color;
    }
  }
}
