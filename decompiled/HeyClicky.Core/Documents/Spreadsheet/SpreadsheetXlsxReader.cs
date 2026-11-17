using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001AD RID: 429
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpreadsheetXlsxReader
  {
    // Token: 0x0600193A RID: 6458 RVA: 0x003CEDA0 File Offset: 0x003CEDA0
    public static SpreadsheetWorkbook Read(string path)
    {
      return SpreadsheetXlsxReader.Read(new SpreadsheetZipArchive(path));
    }

    // Token: 0x0600193B RID: 6459 RVA: 0x003CEDB0 File Offset: 0x003CEDB0
    public static SpreadsheetWorkbook Read(SpreadsheetZipArchive archive)
    {
      byte[] array = archive.Contents("xl/workbook.xml");
      SpreadsheetXmlNode spreadsheetXmlNode = ((array == null) ? null : SpreadsheetXmlNode.Parse(array));
      if (spreadsheetXmlNode == null)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.MissingPart, "xl/workbook.xml");
      }
      Dictionary<string, string> dictionary = SpreadsheetXlsxReader.Relationships(archive, "xl/workbook.xml");
      byte[] array2 = archive.Contents("xl/theme/theme1.xml");
      SpreadsheetThemeColors spreadsheetThemeColors = new SpreadsheetThemeColors((array2 == null) ? null : SpreadsheetXmlNode.Parse(array2));
      byte[] array3 = archive.Contents("xl/styles.xml");
      List<SpreadsheetCellStyle> list = SpreadsheetStylesReader.Styles((array3 == null) ? null : SpreadsheetXmlNode.Parse(array3), spreadsheetThemeColors);
      byte[] array4 = archive.Contents("xl/sharedStrings.xml");
      List<string> list2 = ((array4 == null) ? new List<string>() : SpreadsheetSharedStringsParser.Strings(array4));
      SpreadsheetXmlNode spreadsheetXmlNode2 = spreadsheetXmlNode.FirstChild("workbookPr");
      string text = ((spreadsheetXmlNode2 != null) ? spreadsheetXmlNode2.Attribute("date1904") : null) ?? "0";
      bool flag = text == "1" || text == "true";
      bool flag2 = flag;
      List<SpreadsheetSheet> list3 = new List<SpreadsheetSheet>();
      Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
      SpreadsheetXmlNode spreadsheetXmlNode3 = spreadsheetXmlNode.FirstChild("sheets");
      List<SpreadsheetXmlNode> list4 = ((spreadsheetXmlNode3 != null) ? spreadsheetXmlNode3.Children("sheet") : null) ?? new List<SpreadsheetXmlNode>();
      for (int i = 0; i < list4.Count; i++)
      {
        SpreadsheetXmlNode spreadsheetXmlNode4 = list4[i];
        string text2 = spreadsheetXmlNode4.Attribute("state");
        if (text2 == null || !(text2 != "visible"))
        {
          string text3 = spreadsheetXmlNode4.Attribute("r:id") ?? spreadsheetXmlNode4.Attribute("id");
          string text4;
          if (text3 != null && dictionary.TryGetValue(text3, out text4))
          {
            string text5 = SpreadsheetXlsxReader.ResolvedPartName(text4, "xl");
            byte[] array5 = archive.Contents(text5);
            if (array5 != null)
            {
              Dictionary<string, string> dictionary3 = SpreadsheetXlsxReader.Relationships(archive, text5);
              SpreadsheetSheetParser.Result result = new SpreadsheetSheetParser(list2, list, flag2, dictionary3).Parse(array5);
              dictionary2[i] = list3.Count;
              List<SpreadsheetSheet> list5 = list3;
              string text6;
              if ((text6 = spreadsheetXmlNode4.Attribute("name")) == null)
              {
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
                defaultInterpolatedStringHandler.AppendLiteral("Sheet");
                defaultInterpolatedStringHandler.AppendFormatted<int>(list3.Count + 1);
                text6 = defaultInterpolatedStringHandler.ToStringAndClear();
              }
              list5.Add(new SpreadsheetSheet(text6, result.CellsByRow, result.ColumnWidths, result.DefaultColumnWidth, result.RowHeights, result.DefaultRowHeight, result.HiddenRows, result.HiddenColumns, result.MergedRanges, result.FrozenRowCount, result.FrozenColumnCount, result.ShowsGridLines, result.Hyperlinks, list));
            }
          }
        }
      }
      if (list3.Count == 0)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.MissingPart, "worksheets");
      }
      SpreadsheetXmlNode spreadsheetXmlNode5 = spreadsheetXmlNode.FirstChild("bookViews");
      string text7;
      if (spreadsheetXmlNode5 == null)
      {
        text7 = null;
      }
      else
      {
        SpreadsheetXmlNode spreadsheetXmlNode6 = spreadsheetXmlNode5.FirstChild("workbookView");
        text7 = ((spreadsheetXmlNode6 != null) ? spreadsheetXmlNode6.Attribute("activeTab") : null);
      }
      int valueOrDefault = SwiftText.ParseInt(text7).GetValueOrDefault();
      int num;
      return new SpreadsheetWorkbook(list3, dictionary2.TryGetValue(valueOrDefault, out num) ? num : 0);
    }

    // Token: 0x0600193C RID: 6460 RVA: 0x003CF0B0 File Offset: 0x003CF0B0
    private static Dictionary<string, string> Relationships(SpreadsheetZipArchive archive, string partName)
    {
      int num = partName.LastIndexOf('/');
      string text = ((num >= 0) ? partName.Substring(0, num) : "");
      string text2;
      if (num < 0)
      {
        text2 = partName;
      }
      else
      {
        int num2 = num + 1;
        text2 = partName.Substring(num2, partName.Length - num2);
      }
      string text3 = text2;
      string text4 = text + "/_rels/" + text3 + ".rels";
      byte[] array = archive.Contents(text4);
      SpreadsheetXmlNode spreadsheetXmlNode = ((array == null) ? null : SpreadsheetXmlNode.Parse(array));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (spreadsheetXmlNode == null)
      {
        return dictionary;
      }
      foreach (SpreadsheetXmlNode spreadsheetXmlNode2 in spreadsheetXmlNode.Children("Relationship"))
      {
        string text5 = spreadsheetXmlNode2.Attribute("Id");
        if (text5 != null)
        {
          string text6 = spreadsheetXmlNode2.Attribute("Target");
          if (text6 != null)
          {
            dictionary[text5] = ((spreadsheetXmlNode2.Attribute("TargetMode") == "External") ? text6 : SpreadsheetXlsxReader.ResolvedPartName(text6, text));
          }
        }
      }
      return dictionary;
    }

    // Token: 0x0600193D RID: 6461 RVA: 0x003CF1D0 File Offset: 0x003CF1D0
    public static string ResolvedPartName(string target, string folder)
    {
      if (target.StartsWith('/'))
      {
        return target.Substring(1, target.Length - 1);
      }
      if (target.StartsWith("xl/", StringComparison.Ordinal))
      {
        return target;
      }
      List<string> list = folder.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList<string>();
      foreach (string text in target.Split('/', StringSplitOptions.RemoveEmptyEntries))
      {
        if (text == "..")
        {
          if (list.Count > 0)
          {
            list.RemoveAt(list.Count - 1);
          }
        }
        else if (text != ".")
        {
          list.Add(text);
        }
      }
      return string.Join<string>('/', list);
    }

    // Token: 0x0600193E RID: 6462 RVA: 0x003CF27C File Offset: 0x003CF27C
    internal static XmlReader CreateReader(byte[] data)
    {
      return XmlReader.Create(new MemoryStream(data), new XmlReaderSettings
      {
        DtdProcessing = DtdProcessing.Ignore,
        XmlResolver = null,
        IgnoreWhitespace = false,
        IgnoreComments = true,
        IgnoreProcessingInstructions = true,
        CheckCharacters = false
      });
    }

    // Token: 0x0600193F RID: 6463 RVA: 0x003CF2B8 File Offset: 0x003CF2B8
    internal static string LocalName(string qualifiedName)
    {
      int num = qualifiedName.LastIndexOf(':');
      if (num >= 0)
      {
        int num2 = num + 1;
        return qualifiedName.Substring(num2, qualifiedName.Length - num2);
      }
      return qualifiedName;
    }

    // Token: 0x06001940 RID: 6464 RVA: 0x003CF2EC File Offset: 0x003CF2EC
    internal static Dictionary<string, string> Attributes(XmlReader reader)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (reader.HasAttributes)
      {
        while (reader.MoveToNextAttribute())
        {
          dictionary[reader.Name] = reader.Value;
        }
        reader.MoveToElement();
      }
      return dictionary;
    }
  }
}
