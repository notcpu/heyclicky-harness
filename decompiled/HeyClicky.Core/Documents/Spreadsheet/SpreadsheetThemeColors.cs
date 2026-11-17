using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AF RID: 431
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class SpreadsheetThemeColors
  {
    // Token: 0x0600194B RID: 6475 RVA: 0x003CF530 File Offset: 0x003CF530
    public SpreadsheetThemeColors(SpreadsheetXmlNode root)
    {
      string[] array = new string[]
      {
        "000000", "FFFFFF", "44546A", "E7E6E6", "4472C4", "ED7D31", "A5A5A5", "FFC000", "5B9BD5", "70AD47",
        "0563C1", "954F72"
      };
      this.schemeColors = array.Select<string, SpreadsheetColor>((string h) => SpreadsheetColors.FromHex(h).Value).ToList<SpreadsheetColor>();
      SpreadsheetXmlNode spreadsheetXmlNode = SpreadsheetThemeColors.FindColorScheme(root);
      if (spreadsheetXmlNode == null)
      {
        return;
      }
      string[] array2 = new string[]
      {
        "dk1", "lt1", "dk2", "lt2", "accent1", "accent2", "accent3", "accent4", "accent5", "accent6",
        "hlink", "folHlink"
      };
      for (int i = 0; i < array2.Length; i++)
      {
        SpreadsheetXmlNode spreadsheetXmlNode2 = spreadsheetXmlNode.FirstChild(array2[i]);
        if (spreadsheetXmlNode2 != null && spreadsheetXmlNode2.ChildNodes.Count != 0)
        {
          SpreadsheetXmlNode spreadsheetXmlNode3 = spreadsheetXmlNode2.ChildNodes[0];
          string text = ((spreadsheetXmlNode3.Name == "srgbClr") ? spreadsheetXmlNode3.Attribute("val") : null) ?? spreadsheetXmlNode3.Attribute("lastClr");
          if (text != null)
          {
            SpreadsheetColor? spreadsheetColor = SpreadsheetColors.FromHex(text);
            if (spreadsheetColor != null)
            {
              SpreadsheetColor valueOrDefault = spreadsheetColor.GetValueOrDefault();
              this.schemeColors[i] = valueOrDefault;
            }
          }
        }
      }
    }

    // Token: 0x0600194C RID: 6476 RVA: 0x003CF6F8 File Offset: 0x003CF6F8
    private static SpreadsheetXmlNode FindColorScheme(SpreadsheetXmlNode node)
    {
      if (node == null)
      {
        return null;
      }
      if (node.Name == "clrScheme")
      {
        return node;
      }
      foreach (SpreadsheetXmlNode spreadsheetXmlNode in node.ChildNodes)
      {
        SpreadsheetXmlNode spreadsheetXmlNode2 = SpreadsheetThemeColors.FindColorScheme(spreadsheetXmlNode);
        if (spreadsheetXmlNode2 != null)
        {
          return spreadsheetXmlNode2;
        }
      }
      return null;
    }

    // Token: 0x0600194D RID: 6477 RVA: 0x003CF76C File Offset: 0x003CF76C
    public SpreadsheetColor? Color(int themeIndex)
    {
      int num;
      switch (themeIndex)
      {
      case 0:
        num = 1;
        break;
      case 1:
        num = 0;
        break;
      case 2:
        num = 3;
        break;
      case 3:
        num = 2;
        break;
      default:
        num = themeIndex;
        break;
      }
      int num2 = num;
      if (num2 < 0 || num2 >= this.schemeColors.Count)
      {
        return null;
      }
      return new SpreadsheetColor?(this.schemeColors[num2]);
    }

    // Token: 0x04000ACC RID: 2764
    [Nullable(1)]
    private readonly List<SpreadsheetColor> schemeColors;
  }
}
