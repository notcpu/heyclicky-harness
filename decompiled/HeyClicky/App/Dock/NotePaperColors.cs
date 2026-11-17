using System;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Ink;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000319 RID: 793
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotePaperColors
  {
    // Token: 0x06002265 RID: 8805 RVA: 0x00622BF4 File Offset: 0x00622BF4
    public static PaperStock Stock(string key)
    {
      PaperStock paperStock;
      switch (ClickyNotePaperColors.Stock(key))
      {
      case ClickyPaperStock.Sky:
        paperStock = PaperStock.Sky;
        break;
      case ClickyPaperStock.Butter:
        paperStock = PaperStock.Butter;
        break;
      case ClickyPaperStock.Mint:
        paperStock = PaperStock.Mint;
        break;
      case ClickyPaperStock.Periwinkle:
        paperStock = PaperStock.Periwinkle;
        break;
      case ClickyPaperStock.Peach:
        paperStock = PaperStock.Peach;
        break;
      case ClickyPaperStock.Blush:
        paperStock = PaperStock.Blush;
        break;
      case ClickyPaperStock.Kraft:
        paperStock = PaperStock.Kraft;
        break;
      default:
        paperStock = PaperStock.Cream;
        break;
      }
      return paperStock;
    }

    // Token: 0x06002266 RID: 8806 RVA: 0x00622C4C File Offset: 0x00622C4C
    public static string Key(string railKey)
    {
      string text = ClickyJob.JobIdFromRailKey(railKey);
      if (text == null)
      {
        return "clicky:" + railKey;
      }
      return "job:" + text;
    }

    // Token: 0x06002267 RID: 8807 RVA: 0x00622C7C File Offset: 0x00622C7C
    public static int Seed(string key)
    {
      int num = 7;
      foreach (char c in key)
      {
        num = (num * 31 + (int)c) & 65535;
      }
      return num;
    }
  }
}
