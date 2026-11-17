using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B0 RID: 432
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpreadsheetColors
  {
    // Token: 0x0600194E RID: 6478 RVA: 0x003CF7D4 File Offset: 0x003CF7D4
    public static SpreadsheetColor? FromHex(string rawHex)
    {
      string text = SwiftText.TrimWhitespace(rawHex);
      if (text.StartsWith('#'))
      {
        string text2 = text;
        text = text2.Substring(1, text2.Length - 1);
      }
      if (text.Length == 8)
      {
        string text3 = text;
        int length = text3.Length;
        int num = length - 6;
        text = text3.Substring(num, length - num);
      }
      uint num2;
      if (text.Length != 6 || !uint.TryParse(text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num2))
      {
        return null;
      }
      return new SpreadsheetColor?(new SpreadsheetColor(((num2 >> 16) & 255U) / 255.0, ((num2 >> 8) & 255U) / 255.0, (num2 & 255U) / 255.0, 1.0));
    }

    // Token: 0x0600194F RID: 6479 RVA: 0x003CF8A0 File Offset: 0x003CF8A0
    public static SpreadsheetColor? FromNode([Nullable(2)] SpreadsheetXmlNode node, SpreadsheetThemeColors theme, IReadOnlyList<SpreadsheetColor> indexedPalette)
    {
      if (node == null)
      {
        return null;
      }
      if (node.Attribute("auto") == "1")
      {
        return null;
      }
      SpreadsheetColor? spreadsheetColor = null;
      string text = node.Attribute("rgb");
      if (text != null)
      {
        spreadsheetColor = SpreadsheetColors.FromHex(text);
      }
      else
      {
        int? num = SwiftText.ParseInt(node.Attribute("theme"));
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          spreadsheetColor = theme.Color(valueOrDefault);
        }
        else
        {
          num = SwiftText.ParseInt(node.Attribute("indexed"));
          if (num != null)
          {
            int valueOrDefault2 = num.GetValueOrDefault();
            if (valueOrDefault2 == 64)
            {
              return null;
            }
            if (valueOrDefault2 == 65)
            {
              return null;
            }
            spreadsheetColor = ((valueOrDefault2 >= 0 && valueOrDefault2 < indexedPalette.Count) ? new SpreadsheetColor?(indexedPalette[valueOrDefault2]) : null);
          }
        }
      }
      if (spreadsheetColor != null)
      {
        SpreadsheetColor valueOrDefault3 = spreadsheetColor.GetValueOrDefault();
        double? num2 = SwiftText.ParseDouble(node.Attribute("tint"));
        if (num2 != null)
        {
          double valueOrDefault4 = num2.GetValueOrDefault();
          if (valueOrDefault4 != 0.0)
          {
            return new SpreadsheetColor?(SpreadsheetColors.ApplyingTint(valueOrDefault4, valueOrDefault3));
          }
        }
        return new SpreadsheetColor?(valueOrDefault3);
      }
      return null;
    }

    // Token: 0x06001950 RID: 6480 RVA: 0x003CFA00 File Offset: 0x003CFA00
    public static SpreadsheetColor ApplyingTint(double tint, SpreadsheetColor color)
    {
      double red = color.Red;
      double green = color.Green;
      double blue = color.Blue;
      double num = Math.Max(red, Math.Max(green, blue));
      double num2 = Math.Min(red, Math.Min(green, blue));
      double num3 = (num + num2) / 2.0;
      double num4 = 0.0;
      double num5 = 0.0;
      if (num != num2)
      {
        double num6 = num - num2;
        num5 = ((num3 > 0.5) ? (num6 / (2.0 - num - num2)) : (num6 / (num + num2)));
        if (num == red)
        {
          num4 = (green - blue) / num6 + (double)((green < blue) ? 6 : 0);
        }
        else if (num == green)
        {
          num4 = (blue - red) / num6 + 2.0;
        }
        else
        {
          num4 = (red - green) / num6 + 4.0;
        }
        num4 /= 6.0;
      }
      num3 = ((tint < 0.0) ? (num3 * (1.0 + tint)) : (num3 * (1.0 - tint) + tint));
      if (num5 == 0.0)
      {
        return new SpreadsheetColor(num3, num3, num3, 1.0);
      }
      double num7 = ((num3 < 0.5) ? (num3 * (1.0 + num5)) : (num3 + num5 - num3 * num5));
      double num8 = 2.0 * num3 - num7;
      return new SpreadsheetColor(SpreadsheetColors.<ApplyingTint>g__Channel|2_0(num8, num7, num4 + 0.3333333333333333), SpreadsheetColors.<ApplyingTint>g__Channel|2_0(num8, num7, num4), SpreadsheetColors.<ApplyingTint>g__Channel|2_0(num8, num7, num4 - 0.3333333333333333), 1.0);
    }

    // Token: 0x06001952 RID: 6482 RVA: 0x003CFE54 File Offset: 0x003CFE54
    [CompilerGenerated]
    internal static double <ApplyingTint>g__Channel|2_0(double p, double q, double t0)
    {
      double num = t0;
      if (num < 0.0)
      {
        num += 1.0;
      }
      if (num > 1.0)
      {
        num -= 1.0;
      }
      if (num < 0.16666666666666666)
      {
        return p + (q - p) * 6.0 * num;
      }
      if (num < 0.5)
      {
        return q;
      }
      if (num < 0.6666666666666666)
      {
        return p + (q - p) * (0.6666666666666666 - num) * 6.0;
      }
      return p;
    }

    // Token: 0x04000ACD RID: 2765
    public static readonly IReadOnlyList<SpreadsheetColor> DefaultIndexedPalette = new string[]
    {
      "000000", "FFFFFF", "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000", "FFFFFF",
      "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "800000", "008000", "000080", "808000",
      "800080", "008080", "C0C0C0", "808080", "9999FF", "993366", "FFFFCC", "CCFFFF", "660066", "FF8080",
      "0066CC", "CCCCFF", "000080", "FF00FF", "FFFF00", "00FFFF", "800080", "800000", "008080", "0000FF",
      "00CCFF", "CCFFFF", "CCFFCC", "FFFF99", "99CCFF", "FF99CC", "CC99FF", "FFCC99", "3366FF", "33CCCC",
      "99CC00", "FFCC00", "FF9900", "FF6600", "666699", "969696", "003366", "339966", "003300", "333300",
      "993300", "993366", "333399", "333333"
    }.Select<string, SpreadsheetColor>((string h) => SpreadsheetColors.FromHex(h).Value).ToList<SpreadsheetColor>();

    // Token: 0x04000ACE RID: 2766
    public static readonly SpreadsheetColor Black = new SpreadsheetColor(0.0, 0.0, 0.0, 1.0);
  }
}
