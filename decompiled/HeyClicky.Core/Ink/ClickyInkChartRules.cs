using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000172 RID: 370
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkChartRules
  {
    // Token: 0x06001688 RID: 5768 RVA: 0x003BF174 File Offset: 0x003BF174
    public static string ValueWithUnit(string value, string unit)
    {
      string text = value.TrimWs();
      string text2 = unit.TrimWs();
      if (text.Length != 0 && text2.Length != 0)
      {
        ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(text);
        if (clickyInkNumber != null)
        {
          ClickyInkNumber valueOrDefault = clickyInkNumber.GetValueOrDefault();
          if (valueOrDefault.Prefix.Length == 0 && valueOrDefault.Suffix.TrimWs().Length == 0)
          {
            if (new string[] { "$", "€", "£", "¥", "₹", "₩", "R$", "A$", "C$" }.Contains(text2))
            {
              return text2 + text;
            }
            string text3 = text2.Characters().FirstOrDefault<string>();
            return text + ((text3 != null && InkText.IsLetter(text3)) ? " " : "") + text2;
          }
        }
      }
      return text;
    }

    // Token: 0x06001689 RID: 5769 RVA: 0x003BF264 File Offset: 0x003BF264
    public static List<double> Values(ClickyInkDocument.Block block)
    {
      return block.Items.Select<ClickyInkDocument.Item, double>(delegate(ClickyInkDocument.Item item)
      {
        string text = ((item.Cells.Count > 1) ? item.Cells[1] : item.Text);
        ClickyInkNumber? clickyInkNumber = ClickyInkNumber.Parse(text);
        if (clickyInkNumber != null)
        {
          return clickyInkNumber.GetValueOrDefault().Value;
        }
        return InkText.ParseDouble(string.Concat(from c in text.Characters()
          where InkText.IsNumber(c) || c == "." || c == "-"
          select c)).GetValueOrDefault();
      }).ToList<double>();
    }

    // Token: 0x0600168A RID: 5770 RVA: 0x003BF298 File Offset: 0x003BF298
    public static double LabelWidth(ClickyInkDocument.Block block)
    {
      int num;
      if (block.Items.Count != 0)
      {
        num = block.Items.Max<ClickyInkDocument.Item>((ClickyInkDocument.Item i) => ((i.Cells.Count > 0) ? i.Cells[0] : i.Text).CharacterCount());
      }
      else
      {
        num = 8;
      }
      int num2 = num;
      return Math.Min(150.0, Math.Max(70.0, (double)num2 * 7.2));
    }
  }
}
