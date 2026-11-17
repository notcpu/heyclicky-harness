using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001DB RID: 475
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyPaperStockStyle
  {
    // Token: 0x060017EF RID: 6127 RVA: 0x005C7F44 File Offset: 0x005C7F44
    public static string RawValue(this ClickyPaperStock stock)
    {
      return stock.ToString().ToLowerInvariant();
    }

    // Token: 0x060017F0 RID: 6128 RVA: 0x005C7F58 File Offset: 0x005C7F58
    [NullableContext(2)]
    public static ClickyPaperStock? FromRawValue(string raw)
    {
      return ClickyPaperStockStyle.AllCases.Cast<ClickyPaperStock?>().FirstOrDefault<ClickyPaperStock?>((ClickyPaperStock? s) => s.Value.RawValue() == raw);
    }

    // Token: 0x060017F1 RID: 6129 RVA: 0x005C7F90 File Offset: 0x005C7F90
    public static Color Paper(this ClickyPaperStock stock)
    {
      string text;
      switch (stock)
      {
      case ClickyPaperStock.Sky:
        text = "#EBF1FD";
        break;
      case ClickyPaperStock.Butter:
        text = "#FCF5E3";
        break;
      case ClickyPaperStock.Mint:
        text = "#E9F6EC";
        break;
      case ClickyPaperStock.Periwinkle:
        text = "#F0EEFD";
        break;
      case ClickyPaperStock.Peach:
        text = "#FDEEE5";
        break;
      case ClickyPaperStock.Blush:
        text = "#FCE9F1";
        break;
      case ClickyPaperStock.Kraft:
        text = "#F4EEE2";
        break;
      default:
        text = "#FBF7EC";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017F2 RID: 6130 RVA: 0x005C8008 File Offset: 0x005C8008
    public static Color Wash(this ClickyPaperStock stock)
    {
      string text;
      switch (stock)
      {
      case ClickyPaperStock.Sky:
        text = "#D7E5FF";
        break;
      case ClickyPaperStock.Butter:
        text = "#FEEEC7";
        break;
      case ClickyPaperStock.Mint:
        text = "#D3F0DC";
        break;
      case ClickyPaperStock.Periwinkle:
        text = "#E3DEFF";
        break;
      case ClickyPaperStock.Peach:
        text = "#FFDDC9";
        break;
      case ClickyPaperStock.Blush:
        text = "#FFC7E1";
        break;
      case ClickyPaperStock.Kraft:
        text = "#EEE3CF";
        break;
      default:
        text = "#F3E9CC";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017F3 RID: 6131 RVA: 0x005C8080 File Offset: 0x005C8080
    public static Color Ink(this ClickyPaperStock stock)
    {
      string text;
      switch (stock)
      {
      case ClickyPaperStock.Sky:
        text = "#0E1A1F";
        break;
      case ClickyPaperStock.Butter:
        text = "#1C1606";
        break;
      case ClickyPaperStock.Mint:
        text = "#0F2417";
        break;
      case ClickyPaperStock.Periwinkle:
        text = "#141A38";
        break;
      case ClickyPaperStock.Peach:
        text = "#2D190B";
        break;
      case ClickyPaperStock.Blush:
        text = "#2C1418";
        break;
      case ClickyPaperStock.Kraft:
        text = "#2B2114";
        break;
      default:
        text = "#2B2620";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017F4 RID: 6132 RVA: 0x005C80F8 File Offset: 0x005C80F8
    public static Color Deep(this ClickyPaperStock stock)
    {
      string text;
      switch (stock)
      {
      case ClickyPaperStock.Sky:
        text = "#1B3A8C";
        break;
      case ClickyPaperStock.Butter:
        text = "#5A3A00";
        break;
      case ClickyPaperStock.Mint:
        text = "#1D5A33";
        break;
      case ClickyPaperStock.Periwinkle:
        text = "#2B3990";
        break;
      case ClickyPaperStock.Peach:
        text = "#8A3B0F";
        break;
      case ClickyPaperStock.Blush:
        text = "#7A1F35";
        break;
      case ClickyPaperStock.Kraft:
        text = "#5A3D16";
        break;
      default:
        text = "#2D5BE3";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017F5 RID: 6133 RVA: 0x005C8170 File Offset: 0x005C8170
    public static Color QuietInk(this ClickyPaperStock stock)
    {
      return stock.Ink().Opacity(0.58);
    }

    // Token: 0x04000BC1 RID: 3009
    public static readonly ClickyPaperStock[] AllCases = Enum.GetValues<ClickyPaperStock>();
  }
}
