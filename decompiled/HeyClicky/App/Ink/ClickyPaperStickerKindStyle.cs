using System;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001CD RID: 461
  public static class ClickyPaperStickerKindStyle
  {
    // Token: 0x060017D0 RID: 6096 RVA: 0x005C6200 File Offset: 0x005C6200
    public static Color Fill(this ClickyPaperStickerKind kind)
    {
      string text;
      if (kind != ClickyPaperStickerKind.Live)
      {
        if (kind != ClickyPaperStickerKind.New)
        {
          text = "#F5A524";
        }
        else
        {
          text = "#1F9D57";
        }
      }
      else
      {
        text = "#2F7CF6";
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017D1 RID: 6097 RVA: 0x005C6234 File Offset: 0x005C6234
    public static Color Words(this ClickyPaperStickerKind kind)
    {
      if (kind != ClickyPaperStickerKind.NeedsYou)
      {
        return PaperColor.White;
      }
      return PaperColor.Hex("#3A2300");
    }
  }
}
