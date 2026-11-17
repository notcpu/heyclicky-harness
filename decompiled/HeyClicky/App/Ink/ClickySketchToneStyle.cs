using System;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E7 RID: 487
  public static class ClickySketchToneStyle
  {
    // Token: 0x0600182D RID: 6189 RVA: 0x005C9468 File Offset: 0x005C9468
    public static Color Fill(this ClickySketchTone tone)
    {
      string text;
      switch (tone)
      {
      case ClickySketchTone.Blue1:
        text = "#D3E5FA";
        break;
      case ClickySketchTone.Blue2:
        text = "#EEF5FD";
        break;
      case ClickySketchTone.Blue3:
        text = "#ECF0FC";
        break;
      case ClickySketchTone.Blue4:
        text = "#ECF6FB";
        break;
      default:
        text = "#E7EEF8";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x0600182E RID: 6190 RVA: 0x005C94BC File Offset: 0x005C94BC
    public static Color Pencil(this ClickySketchTone tone)
    {
      string text;
      switch (tone)
      {
      case ClickySketchTone.Blue1:
        text = "#5F84B4";
        break;
      case ClickySketchTone.Blue2:
        text = "#86A2C6";
        break;
      case ClickySketchTone.Blue3:
        text = "#8592C4";
        break;
      case ClickySketchTone.Blue4:
        text = "#83A6C1";
        break;
      default:
        text = "#9AB0CB";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x0600182F RID: 6191 RVA: 0x005C9510 File Offset: 0x005C9510
    public static Color Words(this ClickySketchTone tone)
    {
      string text;
      switch (tone)
      {
      case ClickySketchTone.Blue1:
        text = "#1F3657";
        break;
      case ClickySketchTone.Blue2:
        text = "#29436A";
        break;
      case ClickySketchTone.Blue3:
        text = "#2C3A68";
        break;
      case ClickySketchTone.Blue4:
        text = "#264660";
        break;
      default:
        text = "#33507A";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x06001830 RID: 6192 RVA: 0x005C9564 File Offset: 0x005C9564
    public static Color SoftWords(this ClickySketchTone tone)
    {
      string text;
      switch (tone)
      {
      case ClickySketchTone.Blue1:
        text = "#52729A";
        break;
      case ClickySketchTone.Blue2:
        text = "#62809F";
        break;
      case ClickySketchTone.Blue3:
        text = "#64729F";
        break;
      case ClickySketchTone.Blue4:
        text = "#5B7E97";
        break;
      default:
        text = "#6A84A6";
        break;
      }
      return PaperColor.Hex(text);
    }
  }
}
