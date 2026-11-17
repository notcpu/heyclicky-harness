using System;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E2 RID: 482
  public static class ClickyPaperGelStyle
  {
    // Token: 0x0600181C RID: 6172 RVA: 0x005C88AC File Offset: 0x005C88AC
    [return: TupleElementNames(new string[] { "Offset", "Color" })]
    [return: Nullable(new byte[] { 1, 0 })]
    public static ValueTuple<double, Color>[] Stops(this ClickyPaperGel gel)
    {
      ValueTuple<double, Color>[] array;
      switch (gel)
      {
      case ClickyPaperGel.Blue:
        array = new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Hex("#EDF4FF")),
          new ValueTuple<double, Color>(0.18, PaperColor.Hex("#D6E7FF")),
          new ValueTuple<double, Color>(0.5, PaperColor.Hex("#A2C7FF")),
          new ValueTuple<double, Color>(0.8, PaperColor.Hex("#99CBF8")),
          new ValueTuple<double, Color>(1.0, PaperColor.Hex("#A2D2F7"))
        };
        break;
      case ClickyPaperGel.White:
        array = new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FBFBFB")),
          new ValueTuple<double, Color>(0.42, PaperColor.Hex("#EDEDED")),
          new ValueTuple<double, Color>(1.0, PaperColor.Hex("#E4E4E4"))
        };
        break;
      case ClickyPaperGel.Butter:
        array = new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FFF6D2")),
          new ValueTuple<double, Color>(0.4, PaperColor.Hex("#FFE48A")),
          new ValueTuple<double, Color>(1.0, PaperColor.Hex("#F8CB4A"))
        };
        break;
      default:
        array = new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Hex("#ECFBEE")),
          new ValueTuple<double, Color>(0.45, PaperColor.Hex("#C6EECB")),
          new ValueTuple<double, Color>(1.0, PaperColor.Hex("#9BD9A4"))
        };
        break;
      }
      return array;
    }

    // Token: 0x0600181D RID: 6173 RVA: 0x005C8AAC File Offset: 0x005C8AAC
    public static Color Rim(this ClickyPaperGel gel)
    {
      string text;
      switch (gel)
      {
      case ClickyPaperGel.Blue:
        text = "#221898";
        break;
      case ClickyPaperGel.White:
        text = "#626262";
        break;
      case ClickyPaperGel.Butter:
        text = "#7A4D00";
        break;
      default:
        text = "#1E5E2D";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x0600181E RID: 6174 RVA: 0x005C8AF4 File Offset: 0x005C8AF4
    public static Color Words(this ClickyPaperGel gel)
    {
      Color color;
      if (gel > ClickyPaperGel.White)
      {
        if (gel != ClickyPaperGel.Butter)
        {
          color = PaperColor.Hex("#0F2E17");
        }
        else
        {
          color = PaperColor.Hex("#3A2600");
        }
      }
      else
      {
        color = PaperColor.Black.Opacity(0.86);
      }
      return color;
    }
  }
}
