using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B1 RID: 433
  public static class SpreadsheetStylesReader
  {
    // Token: 0x06001953 RID: 6483 RVA: 0x003CFEE8 File Offset: 0x003CFEE8
    [NullableContext(1)]
    public unsafe static List<SpreadsheetCellStyle> Styles([Nullable(2)] SpreadsheetXmlNode root, SpreadsheetThemeColors theme)
    {
      SpreadsheetStylesReader.<>c__DisplayClass2_0 CS$<>8__locals1 = new SpreadsheetStylesReader.<>c__DisplayClass2_0();
      CS$<>8__locals1.theme = theme;
      if (root == null)
      {
        int num = 1;
        List<SpreadsheetCellStyle> list = new List<SpreadsheetCellStyle>(num);
        CollectionsMarshal.SetCount<SpreadsheetCellStyle>(list, num);
        Span<SpreadsheetCellStyle> span = CollectionsMarshal.AsSpan<SpreadsheetCellStyle>(list);
        int num2 = 0;
        *span[num2] = new SpreadsheetCellStyle();
        return list;
      }
      CS$<>8__locals1.indexedPalette = SpreadsheetColors.DefaultIndexedPalette;
      SpreadsheetXmlNode spreadsheetXmlNode = root.FirstChild("colors");
      List<SpreadsheetXmlNode> list2;
      if (spreadsheetXmlNode == null)
      {
        list2 = null;
      }
      else
      {
        SpreadsheetXmlNode spreadsheetXmlNode2 = spreadsheetXmlNode.FirstChild("indexedColors");
        list2 = ((spreadsheetXmlNode2 != null) ? spreadsheetXmlNode2.Children("rgbColor") : null);
      }
      List<SpreadsheetXmlNode> list3 = list2;
      if (list3 != null && list3.Count > 0)
      {
        CS$<>8__locals1.indexedPalette = (from c in list3.Select<SpreadsheetXmlNode, SpreadsheetColor?>(delegate(SpreadsheetXmlNode c)
          {
            string text2 = c.Attribute("rgb");
            if (text2 == null)
            {
              return null;
            }
            return SpreadsheetColors.FromHex(text2);
          })
          where c != null
          select c.Value).ToList<SpreadsheetColor>();
      }
      CS$<>8__locals1.numberFormats = new Dictionary<int, string>();
      SpreadsheetXmlNode spreadsheetXmlNode3 = root.FirstChild("numFmts");
      foreach (SpreadsheetXmlNode spreadsheetXmlNode4 in (((spreadsheetXmlNode3 != null) ? spreadsheetXmlNode3.Children("numFmt") : null) ?? new List<SpreadsheetXmlNode>()))
      {
        int? num3 = SwiftText.ParseInt(spreadsheetXmlNode4.Attribute("numFmtId"));
        if (num3 != null)
        {
          int valueOrDefault = num3.GetValueOrDefault();
          string text = spreadsheetXmlNode4.Attribute("formatCode");
          if (text != null)
          {
            CS$<>8__locals1.numberFormats[valueOrDefault] = text;
          }
        }
      }
      SpreadsheetStylesReader.<>c__DisplayClass2_0 CS$<>8__locals2 = CS$<>8__locals1;
      SpreadsheetXmlNode spreadsheetXmlNode5 = root.FirstChild("fonts");
      CS$<>8__locals2.fonts = (((spreadsheetXmlNode5 != null) ? spreadsheetXmlNode5.Children("font") : null) ?? new List<SpreadsheetXmlNode>()).Select<SpreadsheetXmlNode, SpreadsheetStylesReader.FontInfo>(delegate(SpreadsheetXmlNode font)
      {
        SpreadsheetStylesReader.FontInfo fontInfo = new SpreadsheetStylesReader.FontInfo();
        fontInfo.IsBold = SpreadsheetStylesReader.Flag(font.FirstChild("b"));
        fontInfo.IsItalic = SpreadsheetStylesReader.Flag(font.FirstChild("i"));
        SpreadsheetXmlNode spreadsheetXmlNode9 = font.FirstChild("u");
        fontInfo.IsUnderlined = spreadsheetXmlNode9 != null && spreadsheetXmlNode9.Attribute("val") != "none";
        fontInfo.IsStruckThrough = SpreadsheetStylesReader.Flag(font.FirstChild("strike"));
        SpreadsheetStylesReader.FontInfo fontInfo2 = fontInfo;
        SpreadsheetXmlNode spreadsheetXmlNode10 = font.FirstChild("sz");
        double? num4 = SwiftText.ParseDouble((spreadsheetXmlNode10 != null) ? spreadsheetXmlNode10.Attribute("val") : null);
        if (num4 != null)
        {
          double valueOrDefault2 = num4.GetValueOrDefault();
          fontInfo2.Size = valueOrDefault2;
        }
        fontInfo2.Color = SpreadsheetColors.FromNode(font.FirstChild("color"), CS$<>8__locals1.theme, CS$<>8__locals1.indexedPalette);
        return fontInfo2;
      }).ToList<SpreadsheetStylesReader.FontInfo>();
      SpreadsheetStylesReader.<>c__DisplayClass2_0 CS$<>8__locals3 = CS$<>8__locals1;
      SpreadsheetXmlNode spreadsheetXmlNode6 = root.FirstChild("fills");
      CS$<>8__locals3.fills = (((spreadsheetXmlNode6 != null) ? spreadsheetXmlNode6.Children("fill") : null) ?? new List<SpreadsheetXmlNode>()).Select<SpreadsheetXmlNode, SpreadsheetColor?>(delegate(SpreadsheetXmlNode fill)
      {
        SpreadsheetXmlNode spreadsheetXmlNode11 = fill.FirstChild("patternFill");
        if (spreadsheetXmlNode11 == null)
        {
          SpreadsheetXmlNode spreadsheetXmlNode12 = fill.FirstChild("gradientFill");
          SpreadsheetXmlNode spreadsheetXmlNode13 = ((spreadsheetXmlNode12 != null) ? spreadsheetXmlNode12.Children("stop").FirstOrDefault<SpreadsheetXmlNode>() : null);
          return SpreadsheetColors.FromNode((spreadsheetXmlNode13 != null) ? spreadsheetXmlNode13.FirstChild("color") : null, CS$<>8__locals1.theme, CS$<>8__locals1.indexedPalette);
        }
        string text3 = spreadsheetXmlNode11.Attribute("patternType") ?? "none";
        bool flag = text3 == "none" || text3 == "gray125" || text3 == "gray0625";
        if (flag)
        {
          return null;
        }
        SpreadsheetColor? spreadsheetColor = SpreadsheetColors.FromNode(spreadsheetXmlNode11.FirstChild("fgColor"), CS$<>8__locals1.theme, CS$<>8__locals1.indexedPalette);
        if (spreadsheetColor == null)
        {
          return SpreadsheetColors.FromNode(spreadsheetXmlNode11.FirstChild("bgColor"), CS$<>8__locals1.theme, CS$<>8__locals1.indexedPalette);
        }
        return spreadsheetColor;
      }).ToList<SpreadsheetColor?>();
      SpreadsheetStylesReader.<>c__DisplayClass2_0 CS$<>8__locals4 = CS$<>8__locals1;
      SpreadsheetXmlNode spreadsheetXmlNode7 = root.FirstChild("borders");
      CS$<>8__locals4.borders = (((spreadsheetXmlNode7 != null) ? spreadsheetXmlNode7.Children("border") : null) ?? new List<SpreadsheetXmlNode>()).Select<SpreadsheetXmlNode, SpreadsheetStylesReader.BorderInfo>(delegate(SpreadsheetXmlNode border)
      {
        SpreadsheetStylesReader.<>c__DisplayClass2_1 CS$<>8__locals5;
        CS$<>8__locals5.border = border;
        SpreadsheetBorderEdge? spreadsheetBorderEdge = base.<Styles>g__Edge|7("left", ref CS$<>8__locals5);
        SpreadsheetBorderEdge? spreadsheetBorderEdge2 = ((spreadsheetBorderEdge != null) ? spreadsheetBorderEdge : base.<Styles>g__Edge|7("start", ref CS$<>8__locals5));
        spreadsheetBorderEdge = base.<Styles>g__Edge|7("right", ref CS$<>8__locals5);
        return new SpreadsheetStylesReader.BorderInfo(spreadsheetBorderEdge2, (spreadsheetBorderEdge != null) ? spreadsheetBorderEdge : base.<Styles>g__Edge|7("end", ref CS$<>8__locals5), base.<Styles>g__Edge|7("top", ref CS$<>8__locals5), base.<Styles>g__Edge|7("bottom", ref CS$<>8__locals5));
      }).ToList<SpreadsheetStylesReader.BorderInfo>();
      SpreadsheetXmlNode spreadsheetXmlNode8 = root.FirstChild("cellXfs");
      List<SpreadsheetXmlNode> list4 = ((spreadsheetXmlNode8 != null) ? spreadsheetXmlNode8.Children("xf") : null) ?? new List<SpreadsheetXmlNode>();
      if (list4.Count == 0)
      {
        int num2 = 1;
        List<SpreadsheetCellStyle> list5 = new List<SpreadsheetCellStyle>(num2);
        CollectionsMarshal.SetCount<SpreadsheetCellStyle>(list5, num2);
        Span<SpreadsheetCellStyle> span = CollectionsMarshal.AsSpan<SpreadsheetCellStyle>(list5);
        int num = 0;
        *span[num] = new SpreadsheetCellStyle();
        return list5;
      }
      return list4.Select<SpreadsheetXmlNode, SpreadsheetCellStyle>(delegate(SpreadsheetXmlNode format)
      {
        SpreadsheetCellStyle spreadsheetCellStyle = new SpreadsheetCellStyle();
        int? num5 = SwiftText.ParseInt(format.Attribute("fontId"));
        if (num5 != null)
        {
          int valueOrDefault3 = num5.GetValueOrDefault();
          if (valueOrDefault3 >= 0 && valueOrDefault3 < CS$<>8__locals1.fonts.Count)
          {
            SpreadsheetStylesReader.FontInfo fontInfo3 = CS$<>8__locals1.fonts[valueOrDefault3];
            spreadsheetCellStyle.IsBold = fontInfo3.IsBold;
            spreadsheetCellStyle.IsItalic = fontInfo3.IsItalic;
            spreadsheetCellStyle.IsUnderlined = fontInfo3.IsUnderlined;
            spreadsheetCellStyle.IsStruckThrough = fontInfo3.IsStruckThrough;
            spreadsheetCellStyle.FontSize = Math.Round(fontInfo3.Size * 1.17, MidpointRounding.ToEven);
            spreadsheetCellStyle.TextColor = fontInfo3.Color;
          }
        }
        num5 = SwiftText.ParseInt(format.Attribute("fillId"));
        if (num5 != null)
        {
          int valueOrDefault4 = num5.GetValueOrDefault();
          if (valueOrDefault4 >= 0 && valueOrDefault4 < CS$<>8__locals1.fills.Count)
          {
            spreadsheetCellStyle.FillColor = CS$<>8__locals1.fills[valueOrDefault4];
          }
        }
        num5 = SwiftText.ParseInt(format.Attribute("borderId"));
        if (num5 != null)
        {
          int valueOrDefault5 = num5.GetValueOrDefault();
          if (valueOrDefault5 >= 0 && valueOrDefault5 < CS$<>8__locals1.borders.Count)
          {
            SpreadsheetStylesReader.BorderInfo borderInfo = CS$<>8__locals1.borders[valueOrDefault5];
            spreadsheetCellStyle.LeftBorder = borderInfo.Left;
            spreadsheetCellStyle.RightBorder = borderInfo.Right;
            spreadsheetCellStyle.TopBorder = borderInfo.Top;
            spreadsheetCellStyle.BottomBorder = borderInfo.Bottom;
          }
        }
        int valueOrDefault6 = SwiftText.ParseInt(format.Attribute("numFmtId")).GetValueOrDefault();
        string text4;
        spreadsheetCellStyle.NumberFormatCode = (CS$<>8__locals1.numberFormats.TryGetValue(valueOrDefault6, out text4) ? text4 : (SpreadsheetNumberFormat.BuiltInFormatCode(valueOrDefault6) ?? "General"));
        SpreadsheetXmlNode spreadsheetXmlNode14 = format.FirstChild("alignment");
        if (spreadsheetXmlNode14 != null)
        {
          string text5 = spreadsheetXmlNode14.Attribute("horizontal");
          SpreadsheetHorizontalAlignment spreadsheetHorizontalAlignment;
          if (text5 != null)
          {
            int length = text5.Length;
            switch (length)
            {
            case 4:
            {
              char c = text5[0];
              if (c != 'f')
              {
                if (c != 'l')
                {
                  goto IL_02B9;
                }
                if (!(text5 == "left"))
                {
                  goto IL_02B9;
                }
              }
              else if (!(text5 == "fill"))
              {
                goto IL_02B9;
              }
              break;
            }
            case 5:
              if (!(text5 == "right"))
              {
                goto IL_02B9;
              }
              spreadsheetHorizontalAlignment = SpreadsheetHorizontalAlignment.Right;
              goto IL_02BC;
            case 6:
              if (!(text5 == "center"))
              {
                goto IL_02B9;
              }
              goto IL_02AF;
            case 7:
              if (!(text5 == "justify"))
              {
                goto IL_02B9;
              }
              break;
            case 8:
            case 9:
            case 10:
              goto IL_02B9;
            case 11:
              if (!(text5 == "distributed"))
              {
                goto IL_02B9;
              }
              break;
            default:
              if (length != 16)
              {
                goto IL_02B9;
              }
              if (!(text5 == "centerContinuous"))
              {
                goto IL_02B9;
              }
              goto IL_02AF;
            }
            spreadsheetHorizontalAlignment = SpreadsheetHorizontalAlignment.Left;
            goto IL_02BC;
            IL_02AF:
            spreadsheetHorizontalAlignment = SpreadsheetHorizontalAlignment.Center;
            goto IL_02BC;
          }
          IL_02B9:
          spreadsheetHorizontalAlignment = SpreadsheetHorizontalAlignment.General;
          IL_02BC:
          spreadsheetCellStyle.HorizontalAlignment = spreadsheetHorizontalAlignment;
          text5 = spreadsheetXmlNode14.Attribute("vertical");
          SpreadsheetVerticalAlignment spreadsheetVerticalAlignment;
          if (!(text5 == "top"))
          {
            if (!(text5 == "center") && !(text5 == "justify") && !(text5 == "distributed"))
            {
              spreadsheetVerticalAlignment = SpreadsheetVerticalAlignment.Bottom;
            }
            else
            {
              spreadsheetVerticalAlignment = SpreadsheetVerticalAlignment.Center;
            }
          }
          else
          {
            spreadsheetVerticalAlignment = SpreadsheetVerticalAlignment.Top;
          }
          spreadsheetCellStyle.VerticalAlignment = spreadsheetVerticalAlignment;
          text5 = spreadsheetXmlNode14.Attribute("wrapText");
          bool flag2 = text5 == "1" || text5 == "true";
          spreadsheetCellStyle.WrapsText = flag2;
          spreadsheetCellStyle.IndentLevel = SwiftText.ParseInt(spreadsheetXmlNode14.Attribute("indent")).GetValueOrDefault();
        }
        return spreadsheetCellStyle;
      }).ToList<SpreadsheetCellStyle>();
    }

    // Token: 0x06001954 RID: 6484 RVA: 0x003D01DC File Offset: 0x003D01DC
    [NullableContext(2)]
    private static bool Flag(SpreadsheetXmlNode node)
    {
      if (node == null)
      {
        return false;
      }
      string text = node.Attribute("val");
      return text == null || (text == "1" || text == "true");
    }

    // Token: 0x06001955 RID: 6485 RVA: 0x003D0220 File Offset: 0x003D0220
    [NullableContext(1)]
    public static bool IsDateCode(string code)
    {
      string text = code.ToLowerInvariant();
      if (text == "general")
      {
        return false;
      }
      string text2 = Regex.Replace(Regex.Replace(text, "\"[^\"]*\"", ""), "\\[[^\\]]*\\]", "");
      return text2.Contains('y') || text2.Contains('d') || text2.Contains('h') || (text2.Contains('m') && !text2.Contains('0') && !text2.Contains('#'));
    }

    // Token: 0x02000598 RID: 1432
    private sealed class FontInfo
    {
      // Token: 0x040020F9 RID: 8441
      public bool IsBold;

      // Token: 0x040020FA RID: 8442
      public bool IsItalic;

      // Token: 0x040020FB RID: 8443
      public bool IsUnderlined;

      // Token: 0x040020FC RID: 8444
      public bool IsStruckThrough;

      // Token: 0x040020FD RID: 8445
      public double Size = 11.0;

      // Token: 0x040020FE RID: 8446
      public SpreadsheetColor? Color;
    }

    // Token: 0x02000599 RID: 1433
    private readonly struct BorderInfo : IEquatable<SpreadsheetStylesReader.BorderInfo>
    {
      // Token: 0x060035D6 RID: 13782 RVA: 0x00442734 File Offset: 0x00442734
      public BorderInfo(SpreadsheetBorderEdge? Left, SpreadsheetBorderEdge? Right, SpreadsheetBorderEdge? Top, SpreadsheetBorderEdge? Bottom)
      {
        this.Left = Left;
        this.Right = Right;
        this.Top = Top;
        this.Bottom = Bottom;
      }

      // Token: 0x17000976 RID: 2422
      // (get) Token: 0x060035D7 RID: 13783 RVA: 0x00442754 File Offset: 0x00442754
      // (set) Token: 0x060035D8 RID: 13784 RVA: 0x0044275C File Offset: 0x0044275C
      public SpreadsheetBorderEdge? Left { get; set; }

      // Token: 0x17000977 RID: 2423
      // (get) Token: 0x060035D9 RID: 13785 RVA: 0x00442768 File Offset: 0x00442768
      // (set) Token: 0x060035DA RID: 13786 RVA: 0x00442770 File Offset: 0x00442770
      public SpreadsheetBorderEdge? Right { get; set; }

      // Token: 0x17000978 RID: 2424
      // (get) Token: 0x060035DB RID: 13787 RVA: 0x0044277C File Offset: 0x0044277C
      // (set) Token: 0x060035DC RID: 13788 RVA: 0x00442784 File Offset: 0x00442784
      public SpreadsheetBorderEdge? Top { get; set; }

      // Token: 0x17000979 RID: 2425
      // (get) Token: 0x060035DD RID: 13789 RVA: 0x00442790 File Offset: 0x00442790
      // (set) Token: 0x060035DE RID: 13790 RVA: 0x00442798 File Offset: 0x00442798
      public SpreadsheetBorderEdge? Bottom { get; set; }

      // Token: 0x060035DF RID: 13791 RVA: 0x004427A4 File Offset: 0x004427A4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("BorderInfo");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035E0 RID: 13792 RVA: 0x004427F0 File Offset: 0x004427F0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Left = ");
        builder.Append(this.Left.ToString());
        builder.Append(", Right = ");
        builder.Append(this.Right.ToString());
        builder.Append(", Top = ");
        builder.Append(this.Top.ToString());
        builder.Append(", Bottom = ");
        builder.Append(this.Bottom.ToString());
        return true;
      }

      // Token: 0x060035E1 RID: 13793 RVA: 0x0044289C File Offset: 0x0044289C
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetStylesReader.BorderInfo left, SpreadsheetStylesReader.BorderInfo right)
      {
        return !(left == right);
      }

      // Token: 0x060035E2 RID: 13794 RVA: 0x004428A8 File Offset: 0x004428A8
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetStylesReader.BorderInfo left, SpreadsheetStylesReader.BorderInfo right)
      {
        return left.Equals(right);
      }

      // Token: 0x060035E3 RID: 13795 RVA: 0x004428B4 File Offset: 0x004428B4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<SpreadsheetBorderEdge?>.Default.GetHashCode(this.<Left>k__BackingField) * -1521134295 + EqualityComparer<SpreadsheetBorderEdge?>.Default.GetHashCode(this.<Right>k__BackingField)) * -1521134295 + EqualityComparer<SpreadsheetBorderEdge?>.Default.GetHashCode(this.<Top>k__BackingField)) * -1521134295 + EqualityComparer<SpreadsheetBorderEdge?>.Default.GetHashCode(this.<Bottom>k__BackingField);
      }

      // Token: 0x060035E4 RID: 13796 RVA: 0x00442918 File Offset: 0x00442918
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is SpreadsheetStylesReader.BorderInfo && this.Equals((SpreadsheetStylesReader.BorderInfo)obj);
      }

      // Token: 0x060035E5 RID: 13797 RVA: 0x00442930 File Offset: 0x00442930
      [CompilerGenerated]
      public bool Equals(SpreadsheetStylesReader.BorderInfo other)
      {
        return EqualityComparer<SpreadsheetBorderEdge?>.Default.Equals(this.<Left>k__BackingField, other.<Left>k__BackingField) && EqualityComparer<SpreadsheetBorderEdge?>.Default.Equals(this.<Right>k__BackingField, other.<Right>k__BackingField) && EqualityComparer<SpreadsheetBorderEdge?>.Default.Equals(this.<Top>k__BackingField, other.<Top>k__BackingField) && EqualityComparer<SpreadsheetBorderEdge?>.Default.Equals(this.<Bottom>k__BackingField, other.<Bottom>k__BackingField);
      }

      // Token: 0x060035E6 RID: 13798 RVA: 0x004429A0 File Offset: 0x004429A0
      [CompilerGenerated]
      public void Deconstruct(out SpreadsheetBorderEdge? Left, out SpreadsheetBorderEdge? Right, out SpreadsheetBorderEdge? Top, out SpreadsheetBorderEdge? Bottom)
      {
        Left = this.Left;
        Right = this.Right;
        Top = this.Top;
        Bottom = this.Bottom;
      }
    }
  }
}
