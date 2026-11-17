using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.Core.Onboarding;
using Windows.UI;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000111 RID: 273
  public static class TourPaperStocks
  {
    // Token: 0x06000E5D RID: 3677 RVA: 0x005735B4 File Offset: 0x005735B4
    [NullableContext(2)]
    public static TourPaperStock? Parse(string raw)
    {
      string text = ((raw != null) ? raw.Trim().ToLowerInvariant() : null);
      if (text != null)
      {
        switch (text.Length)
        {
        case 3:
          if (text == "sky")
          {
            return new TourPaperStock?(TourPaperStock.Sky);
          }
          break;
        case 4:
          if (text == "mint")
          {
            return new TourPaperStock?(TourPaperStock.Mint);
          }
          break;
        case 5:
        {
          char c = text[0];
          if (c <= 'c')
          {
            if (c != 'b')
            {
              if (c == 'c')
              {
                if (text == "cream")
                {
                  return new TourPaperStock?(TourPaperStock.Cream);
                }
              }
            }
            else if (text == "blush")
            {
              return new TourPaperStock?(TourPaperStock.Blush);
            }
          }
          else if (c != 'k')
          {
            if (c == 'p')
            {
              if (text == "peach")
              {
                return new TourPaperStock?(TourPaperStock.Peach);
              }
            }
          }
          else if (text == "kraft")
          {
            return new TourPaperStock?(TourPaperStock.Kraft);
          }
          break;
        }
        case 6:
          if (text == "butter")
          {
            return new TourPaperStock?(TourPaperStock.Butter);
          }
          break;
        case 10:
          if (text == "periwinkle")
          {
            return new TourPaperStock?(TourPaperStock.Periwinkle);
          }
          break;
        }
      }
      return null;
    }

    // Token: 0x06000E5E RID: 3678 RVA: 0x00573714 File Offset: 0x00573714
    [NullableContext(1)]
    public static TourPaperStock ForTitle(string title, string summary = "")
    {
      return TourPaperStocks.Parse(DrawnTourBriefs.PaperForTitle(title, summary)).GetValueOrDefault();
    }

    // Token: 0x06000E5F RID: 3679 RVA: 0x00573738 File Offset: 0x00573738
    public static Color Paper(this TourPaperStock s)
    {
      Color color;
      switch (s)
      {
      case TourPaperStock.Sky:
        color = DS.Hex(15462909U, 1.0);
        break;
      case TourPaperStock.Butter:
        color = DS.Hex(16578019U, 1.0);
        break;
      case TourPaperStock.Mint:
        color = DS.Hex(15333100U, 1.0);
        break;
      case TourPaperStock.Periwinkle:
        color = DS.Hex(15789821U, 1.0);
        break;
      case TourPaperStock.Peach:
        color = DS.Hex(16641765U, 1.0);
        break;
      case TourPaperStock.Blush:
        color = DS.Hex(16574961U, 1.0);
        break;
      case TourPaperStock.Kraft:
        color = DS.Hex(16051938U, 1.0);
        break;
      default:
        color = DS.Hex(16513004U, 1.0);
        break;
      }
      return color;
    }

    // Token: 0x06000E60 RID: 3680 RVA: 0x00573824 File Offset: 0x00573824
    public static Color Ink(this TourPaperStock s)
    {
      Color color;
      switch (s)
      {
      case TourPaperStock.Sky:
        color = DS.Hex(924191U, 1.0);
        break;
      case TourPaperStock.Butter:
        color = DS.Hex(1840646U, 1.0);
        break;
      case TourPaperStock.Mint:
        color = DS.Hex(992279U, 1.0);
        break;
      case TourPaperStock.Periwinkle:
        color = DS.Hex(1317432U, 1.0);
        break;
      case TourPaperStock.Peach:
        color = DS.Hex(2955531U, 1.0);
        break;
      case TourPaperStock.Blush:
        color = DS.Hex(2888728U, 1.0);
        break;
      case TourPaperStock.Kraft:
        color = DS.Hex(2826516U, 1.0);
        break;
      default:
        color = DS.Hex(2827808U, 1.0);
        break;
      }
      return color;
    }
  }
}
