using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000174 RID: 372
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkReceiptRules
  {
    // Token: 0x0600168E RID: 5774 RVA: 0x003BF430 File Offset: 0x003BF430
    public static bool IsTotalLine(ClickyInkDocument.Item item)
    {
      string text = ((item.Cells.Count > 0) ? item.Cells[0] : item.Text).ToLowerInvariant().TrimWs();
      return text == "total" || text.HasPrefix("total ") || text == "grand total";
    }

    // Token: 0x0600168F RID: 5775 RVA: 0x003BF494 File Offset: 0x003BF494
    public static double Width(ClickyInkDocument.Block block)
    {
      int num;
      if (block.Items.Count != 0)
      {
        num = block.Items.Max<ClickyInkDocument.Item>((ClickyInkDocument.Item i) => ((i.Cells.Count > 0) ? i.Cells[0] : i.Text).CharacterCount() + ((i.Cells.Count > 1) ? i.Cells[1].CharacterCount() : 0));
      }
      else
      {
        num = 0;
      }
      return (double)((num > 30) ? 300 : 250);
    }

    // Token: 0x06001690 RID: 5776 RVA: 0x003BF4EC File Offset: 0x003BF4EC
    [return: Nullable(2)]
    public static string Total(ClickyInkDocument.Block block)
    {
      string text = block["total"];
      if (text != null && text.Length > 0)
      {
        return text;
      }
      IEnumerable<ClickyInkDocument.Item> items = block.Items;
      Func<ClickyInkDocument.Item, bool> func;
      if ((func = ClickyInkReceiptRules.<>O.<0>__IsTotalLine) == null)
      {
        func = (ClickyInkReceiptRules.<>O.<0>__IsTotalLine = new Func<ClickyInkDocument.Item, bool>(ClickyInkReceiptRules.IsTotalLine));
      }
      ClickyInkDocument.Item item = items.FirstOrDefault<ClickyInkDocument.Item>(func);
      if (item != null && item.Cells.Count > 1)
      {
        return item.Cells[1];
      }
      double num = 0.0;
      string text2 = "";
      foreach (ClickyInkDocument.Item item2 in block.Items)
      {
        if (item2.Cells.Count <= 1)
        {
          return null;
        }
        string[] array = item2.Cells[1].Characters();
        string text3 = string.Concat(array.TakeWhile<string>((string c) => !InkText.IsNumber(c) && c != "-")).TrimWs();
        double? num2 = InkText.ParseDouble(string.Concat(array.Where<string>((string c) => InkText.IsNumber(c) || c == ".")));
        if (num2 == null)
        {
          return null;
        }
        double valueOrDefault = num2.GetValueOrDefault();
        if (text2.Length == 0)
        {
          text2 = text3;
        }
        else if (text2 != text3)
        {
          return null;
        }
        num += valueOrDefault;
      }
      if (num <= 0.0)
      {
        return null;
      }
      int num3 = ((num % 1.0 == 0.0) ? 0 : 2);
      return text2 + ClickyInkNumberFormatting.Decimal(num, num3, true);
    }

    // Token: 0x02000546 RID: 1350
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002019 RID: 8217
      [Nullable(0)]
      public static Func<ClickyInkDocument.Item, bool> <0>__IsTotalLine;
    }
  }
}
