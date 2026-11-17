using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B3 RID: 435
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetSheetParser
  {
    // Token: 0x06001959 RID: 6489 RVA: 0x003D046C File Offset: 0x003D046C
    public SpreadsheetSheetParser(IReadOnlyList<string> sharedStrings, IReadOnlyList<SpreadsheetCellStyle> styles, bool usesDate1904, IReadOnlyDictionary<string, string> relationships)
    {
      this.sharedStrings = sharedStrings;
      this.styles = styles;
      this.usesDate1904 = usesDate1904;
      this.relationships = relationships;
    }

    // Token: 0x0600195A RID: 6490 RVA: 0x003D04E4 File Offset: 0x003D04E4
    public SpreadsheetSheetParser.Result Parse(byte[] data)
    {
      try
      {
        using (XmlReader xmlReader = SpreadsheetXlsxReader.CreateReader(data))
        {
          while (xmlReader.Read())
          {
            XmlNodeType nodeType = xmlReader.NodeType;
            if (nodeType <= XmlNodeType.CDATA)
            {
              if (nodeType != XmlNodeType.Element)
              {
                if (nodeType - XmlNodeType.Text > 1)
                {
                  continue;
                }
              }
              else
              {
                string text = SpreadsheetXlsxReader.LocalName(xmlReader.Name);
                bool isEmptyElement = xmlReader.IsEmptyElement;
                this.Start(text, SpreadsheetXlsxReader.Attributes(xmlReader));
                if (isEmptyElement)
                {
                  this.End(text);
                  continue;
                }
                continue;
              }
            }
            else if (nodeType - XmlNodeType.Whitespace > 1)
            {
              if (nodeType != XmlNodeType.EndElement)
              {
                continue;
              }
              this.End(SpreadsheetXlsxReader.LocalName(xmlReader.Name));
              continue;
            }
            switch (this.textTarget)
            {
            case SpreadsheetSheetParser.TextTarget.Value:
              this.currentValue.Append(xmlReader.Value);
              break;
            case SpreadsheetSheetParser.TextTarget.Formula:
              this.currentFormula.Append(xmlReader.Value);
              break;
            case SpreadsheetSheetParser.TextTarget.InlineText:
              this.currentInlineText.Append(xmlReader.Value);
              break;
            }
          }
        }
      }
      catch (XmlException)
      {
      }
      return this.result;
    }

    // Token: 0x0600195B RID: 6491 RVA: 0x003D05FC File Offset: 0x003D05FC
    [return: Nullable(2)]
    private static string Get(Dictionary<string, string> attributes, string key)
    {
      string text;
      if (!attributes.TryGetValue(key, out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x0600195C RID: 6492 RVA: 0x003D0618 File Offset: 0x003D0618
    private static double RowHeightPoints(double height)
    {
      return Math.Max(4.0, Math.Round(height * 1.3333333333333333, MidpointRounding.AwayFromZero));
    }

    // Token: 0x0600195D RID: 6493 RVA: 0x003D063C File Offset: 0x003D063C
    private void Start(string name, Dictionary<string, string> attributes)
    {
      if (name != null)
      {
        int length = name.Length;
        switch (length)
        {
        case 1:
        {
          char c = name[0];
          if (c <= 'f')
          {
            if (c == 'c')
            {
              string text = SpreadsheetSheetParser.Get(attributes, "r");
              if (text != null)
              {
                SpreadsheetCellAddress? spreadsheetCellAddress = SpreadsheetCellAddress.Parse(text);
                if (spreadsheetCellAddress != null)
                {
                  SpreadsheetCellAddress valueOrDefault = spreadsheetCellAddress.GetValueOrDefault();
                  this.currentRow = valueOrDefault.Row;
                  this.currentColumn = valueOrDefault.Column;
                  goto IL_059B;
                }
              }
              this.currentColumn++;
              IL_059B:
              this.currentCellType = SpreadsheetSheetParser.Get(attributes, "t") ?? "n";
              this.currentCellStyleIndex = SwiftText.ParseInt(SpreadsheetSheetParser.Get(attributes, "s")).GetValueOrDefault();
              this.currentValue.Clear();
              this.currentFormula.Clear();
              this.currentInlineText.Clear();
              return;
            }
            if (c != 'f')
            {
              return;
            }
            this.textTarget = SpreadsheetSheetParser.TextTarget.Formula;
            return;
          }
          else if (c != 't')
          {
            if (c != 'v')
            {
              return;
            }
            this.textTarget = SpreadsheetSheetParser.TextTarget.Value;
            return;
          }
          else if (this.isInsideInlineString && !this.isInsidePhonetic)
          {
            this.textTarget = SpreadsheetSheetParser.TextTarget.InlineText;
            return;
          }
          break;
        }
        case 2:
          if (!(name == "is"))
          {
            return;
          }
          this.isInsideInlineString = true;
          return;
        case 3:
        {
          char c = name[2];
          if (c != 'h')
          {
            if (c != 'l')
            {
              if (c != 'w')
              {
                return;
              }
              if (!(name == "row"))
              {
                return;
              }
              int? num = SwiftText.ParseInt(SpreadsheetSheetParser.Get(attributes, "r"));
              if (num != null)
              {
                int valueOrDefault2 = num.GetValueOrDefault();
                this.currentRow = valueOrDefault2 - 1;
              }
              else
              {
                this.currentRow++;
              }
              this.currentColumn = -1;
              double? num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "ht"));
              double valueOrDefault3;
              bool flag;
              if (num2 != null)
              {
                valueOrDefault3 = num2.GetValueOrDefault();
                flag = true;
              }
              else
              {
                flag = false;
              }
              bool flag2 = flag;
              string text2;
              if (flag2)
              {
                text2 = SpreadsheetSheetParser.Get(attributes, "customHeight");
                bool flag3 = text2 == "1" || text2 == "true";
                flag2 = flag3 || valueOrDefault3 > 0.0;
              }
              if (flag2)
              {
                this.result.RowHeights[this.currentRow] = SpreadsheetSheetParser.RowHeightPoints(valueOrDefault3);
              }
              text2 = SpreadsheetSheetParser.Get(attributes, "hidden");
              flag2 = text2 == "1" || text2 == "true";
              if (flag2)
              {
                this.result.HiddenRows.Add(this.currentRow);
                return;
              }
            }
            else
            {
              if (!(name == "col"))
              {
                return;
              }
              int? num = SwiftText.ParseInt(SpreadsheetSheetParser.Get(attributes, "min"));
              if (num != null)
              {
                int valueOrDefault4 = num.GetValueOrDefault();
                num = SwiftText.ParseInt(SpreadsheetSheetParser.Get(attributes, "max"));
                if (num != null)
                {
                  int valueOrDefault5 = num.GetValueOrDefault();
                  if (valueOrDefault4 >= 1 && valueOrDefault5 >= valueOrDefault4)
                  {
                    string text2 = SpreadsheetSheetParser.Get(attributes, "hidden");
                    bool flag2 = text2 == "1" || text2 == "true";
                    bool flag4 = flag2;
                    double? num3 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "width"));
                    for (int i = valueOrDefault4 - 1; i < Math.Min(valueOrDefault5, valueOrDefault4 - 1 + 2000); i++)
                    {
                      if (flag4)
                      {
                        this.result.HiddenColumns.Add(i);
                      }
                      if (num3 != null)
                      {
                        double valueOrDefault6 = num3.GetValueOrDefault();
                        this.result.ColumnWidths[i] = SpreadsheetMetrics.Points(valueOrDefault6);
                      }
                    }
                    return;
                  }
                }
              }
              return;
            }
          }
          else
          {
            if (!(name == "rPh"))
            {
              return;
            }
            this.isInsidePhonetic = true;
            return;
          }
          break;
        }
        case 4:
        {
          if (!(name == "pane"))
          {
            return;
          }
          string text3 = SpreadsheetSheetParser.Get(attributes, "state") ?? "split";
          if (!(text3 == "frozen") && !(text3 == "frozenSplit"))
          {
            return;
          }
          SpreadsheetSheetParser.Result result = this.result;
          double? num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "xSplit"));
          int num4;
          if (num2 != null)
          {
            double valueOrDefault7 = num2.GetValueOrDefault();
            num4 = (int)valueOrDefault7;
          }
          else
          {
            num4 = 0;
          }
          result.FrozenColumnCount = num4;
          SpreadsheetSheetParser.Result result2 = this.result;
          num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "ySplit"));
          int num5;
          if (num2 != null)
          {
            double valueOrDefault8 = num2.GetValueOrDefault();
            num5 = (int)valueOrDefault8;
          }
          else
          {
            num5 = 0;
          }
          result2.FrozenRowCount = num5;
          return;
        }
        case 5:
        case 6:
        case 7:
        case 8:
          break;
        case 9:
        {
          char c = name[0];
          if (c != 'h')
          {
            if (c != 'm')
            {
              if (c != 's')
              {
                return;
              }
              if (!(name == "sheetView"))
              {
                return;
              }
              if (this.didSeeFirstSheetView)
              {
                return;
              }
              this.didSeeFirstSheetView = true;
              string text4 = SpreadsheetSheetParser.Get(attributes, "showGridLines");
              if (text4 != null)
              {
                SpreadsheetSheetParser.Result result3 = this.result;
                bool flag2 = text4 == "0" || text4 == "false";
                result3.ShowsGridLines = !flag2;
                return;
              }
            }
            else
            {
              if (!(name == "mergeCell"))
              {
                return;
              }
              string text5 = SpreadsheetSheetParser.Get(attributes, "ref");
              if (text5 != null)
              {
                SpreadsheetCellRange? spreadsheetCellRange = SpreadsheetCellRange.Parse(text5);
                if (spreadsheetCellRange != null)
                {
                  SpreadsheetCellRange valueOrDefault9 = spreadsheetCellRange.GetValueOrDefault();
                  this.result.MergedRanges.Add(valueOrDefault9);
                  return;
                }
              }
            }
          }
          else
          {
            if (!(name == "hyperlink"))
            {
              return;
            }
            string text6 = SpreadsheetSheetParser.Get(attributes, "ref");
            if (text6 != null)
            {
              SpreadsheetCellRange? spreadsheetCellRange = SpreadsheetCellRange.Parse(text6);
              if (spreadsheetCellRange != null)
              {
                SpreadsheetCellRange valueOrDefault10 = spreadsheetCellRange.GetValueOrDefault();
                string text7 = null;
                string text8 = SpreadsheetSheetParser.Get(attributes, "r:id") ?? SpreadsheetSheetParser.Get(attributes, "id");
                if (text8 != null)
                {
                  string text9;
                  text7 = (this.relationships.TryGetValue(text8, out text9) ? text9 : null);
                }
                if (text7 == null)
                {
                  string text10 = SpreadsheetSheetParser.Get(attributes, "location");
                  if (text10 != null)
                  {
                    text7 = "#" + text10;
                  }
                }
                if (text7 != null)
                {
                  this.result.Hyperlinks[new SpreadsheetCellAddress(valueOrDefault10.FirstRow, valueOrDefault10.FirstColumn)] = text7;
                  break;
                }
                break;
              }
            }
            return;
          }
          break;
        }
        default:
        {
          if (length != 13)
          {
            return;
          }
          if (!(name == "sheetFormatPr"))
          {
            return;
          }
          double? num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "defaultRowHeight"));
          if (num2 != null)
          {
            double valueOrDefault11 = num2.GetValueOrDefault();
            if (valueOrDefault11 > 0.0)
            {
              this.result.DefaultRowHeight = SpreadsheetSheetParser.RowHeightPoints(valueOrDefault11);
            }
          }
          num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "defaultColWidth"));
          if (num2 != null)
          {
            double valueOrDefault12 = num2.GetValueOrDefault();
            this.result.DefaultColumnWidth = SpreadsheetMetrics.Points(valueOrDefault12);
            return;
          }
          num2 = SwiftText.ParseDouble(SpreadsheetSheetParser.Get(attributes, "baseColWidth"));
          if (num2 != null)
          {
            double valueOrDefault13 = num2.GetValueOrDefault();
            this.result.DefaultColumnWidth = SpreadsheetMetrics.Points(valueOrDefault13 + 0.71);
            return;
          }
          break;
        }
        }
      }
    }

    // Token: 0x0600195E RID: 6494 RVA: 0x003D0D7C File Offset: 0x003D0D7C
    private void End(string name)
    {
      if (!(name == "v") && !(name == "f"))
      {
        if (!(name == "t"))
        {
          if (name == "is")
          {
            this.isInsideInlineString = false;
            return;
          }
          if (name == "rPh")
          {
            this.isInsidePhonetic = false;
            return;
          }
          if (!(name == "c"))
          {
            return;
          }
          this.FinishCell();
        }
        else if (this.textTarget == SpreadsheetSheetParser.TextTarget.InlineText)
        {
          this.textTarget = SpreadsheetSheetParser.TextTarget.None;
          return;
        }
        return;
      }
      this.textTarget = SpreadsheetSheetParser.TextTarget.None;
    }

    // Token: 0x0600195F RID: 6495 RVA: 0x003D0E08 File Offset: 0x003D0E08
    private void FinishCell()
    {
      if (this.currentRow < 0 || this.currentColumn < 0 || this.cellCount >= 1500000)
      {
        return;
      }
      SpreadsheetCellStyle spreadsheetCellStyle = ((this.currentCellStyleIndex >= 0 && this.currentCellStyleIndex < this.styles.Count) ? this.styles[this.currentCellStyleIndex] : new SpreadsheetCellStyle());
      string text2;
      if (this.currentFormula.Length != 0)
      {
        string text = "=";
        StringBuilder stringBuilder = this.currentFormula;
        text2 = text + ((stringBuilder != null) ? stringBuilder.ToString() : null);
      }
      else
      {
        text2 = null;
      }
      string text3 = text2;
      string text4 = this.currentValue.ToString();
      string text5 = this.currentCellType;
      SpreadsheetCell spreadsheetCell;
      if (!(text5 == "s"))
      {
        if (!(text5 == "inlineStr"))
        {
          if (!(text5 == "str"))
          {
            if (!(text5 == "b"))
            {
              if (!(text5 == "e"))
              {
                if (!(text5 == "d"))
                {
                  string text6 = SwiftText.TrimWhitespaceAndNewlines(text4);
                  double? num = SwiftText.ParseDouble(text6);
                  if (num != null)
                  {
                    double valueOrDefault = num.GetValueOrDefault();
                    spreadsheetCell = new SpreadsheetCell(SpreadsheetNumberFormat.Format((this.usesDate1904 && SpreadsheetStylesReader.IsDateCode(spreadsheetCellStyle.NumberFormatCode)) ? (valueOrDefault + 1462.0) : valueOrDefault, spreadsheetCellStyle.NumberFormatCode), text3 ?? SpreadsheetNumberFormat.General(valueOrDefault), SpreadsheetCellKind.Number, new double?(valueOrDefault), this.currentCellStyleIndex);
                  }
                  else if (text6.Length > 0)
                  {
                    spreadsheetCell = new SpreadsheetCell(text6, text3 ?? text6, SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
                  }
                  else
                  {
                    SpreadsheetColor? fillColor = spreadsheetCellStyle.FillColor;
                    if (fillColor != null || spreadsheetCellStyle.HasBorders)
                    {
                      spreadsheetCell = new SpreadsheetCell("", text3 ?? "", SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
                    }
                    else
                    {
                      spreadsheetCell = null;
                    }
                  }
                }
                else
                {
                  spreadsheetCell = new SpreadsheetCell(text4, text3 ?? text4, SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
                }
              }
              else
              {
                spreadsheetCell = new SpreadsheetCell(text4, text3 ?? text4, SpreadsheetCellKind.Error, null, this.currentCellStyleIndex);
              }
            }
            else
            {
              string text7 = ((text4 == "1" || text4.ToLowerInvariant() == "true") ? "TRUE" : "FALSE");
              spreadsheetCell = new SpreadsheetCell(text7, text3 ?? text7, SpreadsheetCellKind.Boolean, null, this.currentCellStyleIndex);
            }
          }
          else
          {
            string text8 = SpreadsheetSheetParser.UnescapingExcelEscapes(text4);
            spreadsheetCell = new SpreadsheetCell(text8, text3 ?? text8, SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
          }
        }
        else
        {
          string text9 = SpreadsheetSheetParser.UnescapingExcelEscapes(this.currentInlineText.ToString());
          spreadsheetCell = new SpreadsheetCell(text9, text3 ?? text9, SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
        }
      }
      else
      {
        int? num2 = SwiftText.ParseInt(SwiftText.TrimWhitespace(text4));
        if (num2 != null)
        {
          int valueOrDefault2 = num2.GetValueOrDefault();
          if (valueOrDefault2 >= 0 && valueOrDefault2 < this.sharedStrings.Count)
          {
            string text10 = this.sharedStrings[valueOrDefault2];
            spreadsheetCell = new SpreadsheetCell(text10, text3 ?? text10, SpreadsheetCellKind.Text, null, this.currentCellStyleIndex);
            goto IL_034D;
          }
        }
        spreadsheetCell = null;
      }
      IL_034D:
      if (spreadsheetCell == null)
      {
        return;
      }
      Dictionary<int, SpreadsheetCell> dictionary;
      if (!this.result.CellsByRow.TryGetValue(this.currentRow, out dictionary))
      {
        dictionary = (this.result.CellsByRow[this.currentRow] = new Dictionary<int, SpreadsheetCell>());
      }
      dictionary[this.currentColumn] = spreadsheetCell;
      this.cellCount++;
    }

    // Token: 0x06001960 RID: 6496 RVA: 0x003D11BC File Offset: 0x003D11BC
    public static string UnescapingExcelEscapes(string text)
    {
      if (!text.Contains("_x", StringComparison.Ordinal))
      {
        return text;
      }
      StringBuilder stringBuilder = new StringBuilder(text.Length);
      int i = 0;
      while (i < text.Length)
      {
        uint num;
        if (text[i] == '_' && i + 7 <= text.Length && text[i + 1] == 'x' && text[i + 6] == '_' && uint.TryParse(text.AsSpan(i + 2, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num) && (num < 55296U || num > 57343U))
        {
          if (num != 13U)
          {
            stringBuilder.Append((char)num);
          }
          i += 7;
        }
        else
        {
          stringBuilder.Append(text[i]);
          i++;
        }
      }
      return stringBuilder.ToString();
    }

    // Token: 0x04000ACF RID: 2767
    public const int MaximumCellCount = 1500000;

    // Token: 0x04000AD0 RID: 2768
    private readonly IReadOnlyList<string> sharedStrings;

    // Token: 0x04000AD1 RID: 2769
    private readonly IReadOnlyList<SpreadsheetCellStyle> styles;

    // Token: 0x04000AD2 RID: 2770
    private readonly bool usesDate1904;

    // Token: 0x04000AD3 RID: 2771
    private readonly IReadOnlyDictionary<string, string> relationships;

    // Token: 0x04000AD4 RID: 2772
    private readonly SpreadsheetSheetParser.Result result = new SpreadsheetSheetParser.Result();

    // Token: 0x04000AD5 RID: 2773
    private int cellCount;

    // Token: 0x04000AD6 RID: 2774
    private int currentRow = -1;

    // Token: 0x04000AD7 RID: 2775
    private int currentColumn = -1;

    // Token: 0x04000AD8 RID: 2776
    private string currentCellType = "n";

    // Token: 0x04000AD9 RID: 2777
    private int currentCellStyleIndex;

    // Token: 0x04000ADA RID: 2778
    private readonly StringBuilder currentValue = new StringBuilder();

    // Token: 0x04000ADB RID: 2779
    private readonly StringBuilder currentFormula = new StringBuilder();

    // Token: 0x04000ADC RID: 2780
    private readonly StringBuilder currentInlineText = new StringBuilder();

    // Token: 0x04000ADD RID: 2781
    private SpreadsheetSheetParser.TextTarget textTarget;

    // Token: 0x04000ADE RID: 2782
    private bool isInsideInlineString;

    // Token: 0x04000ADF RID: 2783
    private bool isInsidePhonetic;

    // Token: 0x04000AE0 RID: 2784
    private bool didSeeFirstSheetView;

    // Token: 0x0200059E RID: 1438
    [Nullable(0)]
    public sealed class Result
    {
      // Token: 0x1700097A RID: 2426
      // (get) Token: 0x060035F2 RID: 13810 RVA: 0x004431B8 File Offset: 0x004431B8
      public Dictionary<int, Dictionary<int, SpreadsheetCell>> CellsByRow { get; } = new Dictionary<int, Dictionary<int, SpreadsheetCell>>();

      // Token: 0x1700097B RID: 2427
      // (get) Token: 0x060035F3 RID: 13811 RVA: 0x004431C0 File Offset: 0x004431C0
      public Dictionary<int, double> ColumnWidths { get; } = new Dictionary<int, double>();

      // Token: 0x1700097C RID: 2428
      // (get) Token: 0x060035F4 RID: 13812 RVA: 0x004431C8 File Offset: 0x004431C8
      // (set) Token: 0x060035F5 RID: 13813 RVA: 0x004431D0 File Offset: 0x004431D0
      public double DefaultColumnWidth { get; set; } = 100.0;

      // Token: 0x1700097D RID: 2429
      // (get) Token: 0x060035F6 RID: 13814 RVA: 0x004431DC File Offset: 0x004431DC
      public Dictionary<int, double> RowHeights { get; } = new Dictionary<int, double>();

      // Token: 0x1700097E RID: 2430
      // (get) Token: 0x060035F7 RID: 13815 RVA: 0x004431E4 File Offset: 0x004431E4
      // (set) Token: 0x060035F8 RID: 13816 RVA: 0x004431EC File Offset: 0x004431EC
      public double DefaultRowHeight { get; set; } = 21.0;

      // Token: 0x1700097F RID: 2431
      // (get) Token: 0x060035F9 RID: 13817 RVA: 0x004431F8 File Offset: 0x004431F8
      public HashSet<int> HiddenRows { get; } = new HashSet<int>();

      // Token: 0x17000980 RID: 2432
      // (get) Token: 0x060035FA RID: 13818 RVA: 0x00443200 File Offset: 0x00443200
      public HashSet<int> HiddenColumns { get; } = new HashSet<int>();

      // Token: 0x17000981 RID: 2433
      // (get) Token: 0x060035FB RID: 13819 RVA: 0x00443208 File Offset: 0x00443208
      public List<SpreadsheetCellRange> MergedRanges { get; } = new List<SpreadsheetCellRange>();

      // Token: 0x17000982 RID: 2434
      // (get) Token: 0x060035FC RID: 13820 RVA: 0x00443210 File Offset: 0x00443210
      // (set) Token: 0x060035FD RID: 13821 RVA: 0x00443218 File Offset: 0x00443218
      public int FrozenRowCount { get; set; }

      // Token: 0x17000983 RID: 2435
      // (get) Token: 0x060035FE RID: 13822 RVA: 0x00443224 File Offset: 0x00443224
      // (set) Token: 0x060035FF RID: 13823 RVA: 0x0044322C File Offset: 0x0044322C
      public int FrozenColumnCount { get; set; }

      // Token: 0x17000984 RID: 2436
      // (get) Token: 0x06003600 RID: 13824 RVA: 0x00443238 File Offset: 0x00443238
      // (set) Token: 0x06003601 RID: 13825 RVA: 0x00443240 File Offset: 0x00443240
      public bool ShowsGridLines { get; set; } = true;

      // Token: 0x17000985 RID: 2437
      // (get) Token: 0x06003602 RID: 13826 RVA: 0x0044324C File Offset: 0x0044324C
      public Dictionary<SpreadsheetCellAddress, string> Hyperlinks { get; } = new Dictionary<SpreadsheetCellAddress, string>();
    }

    // Token: 0x0200059F RID: 1439
    [NullableContext(0)]
    private enum TextTarget
    {
      // Token: 0x0400211F RID: 8479
      None,
      // Token: 0x04002120 RID: 8480
      Value,
      // Token: 0x04002121 RID: 8481
      Formula,
      // Token: 0x04002122 RID: 8482
      InlineText
    }
  }
}
