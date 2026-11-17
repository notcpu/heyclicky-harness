using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B2 RID: 434
  internal static class SpreadsheetSharedStringsParser
  {
    // Token: 0x06001956 RID: 6486 RVA: 0x003D02A4 File Offset: 0x003D02A4
    [NullableContext(1)]
    public static List<string> Strings(byte[] data)
    {
      SpreadsheetSharedStringsParser.<>c__DisplayClass0_0 CS$<>8__locals1;
      CS$<>8__locals1.strings = new List<string>();
      CS$<>8__locals1.currentString = new StringBuilder();
      CS$<>8__locals1.isInsideText = false;
      CS$<>8__locals1.phoneticDepth = 0;
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
                SpreadsheetSharedStringsParser.<Strings>g__Start|0_0(text, ref CS$<>8__locals1);
                if (isEmptyElement)
                {
                  SpreadsheetSharedStringsParser.<Strings>g__End|0_1(text, ref CS$<>8__locals1);
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
              SpreadsheetSharedStringsParser.<Strings>g__End|0_1(SpreadsheetXlsxReader.LocalName(xmlReader.Name), ref CS$<>8__locals1);
              continue;
            }
            if (CS$<>8__locals1.isInsideText)
            {
              CS$<>8__locals1.currentString.Append(xmlReader.Value);
            }
          }
        }
      }
      catch (XmlException)
      {
      }
      return CS$<>8__locals1.strings;
    }

    // Token: 0x06001957 RID: 6487 RVA: 0x003D039C File Offset: 0x003D039C
    [NullableContext(1)]
    [CompilerGenerated]
    internal static void <Strings>g__Start|0_0(string name, ref SpreadsheetSharedStringsParser.<>c__DisplayClass0_0 A_1)
    {
      if (name == "si")
      {
        A_1.currentString.Clear();
        return;
      }
      if (name == "t")
      {
        A_1.isInsideText = A_1.phoneticDepth == 0;
        return;
      }
      if (!(name == "rPh"))
      {
        return;
      }
      int phoneticDepth = A_1.phoneticDepth;
      A_1.phoneticDepth = phoneticDepth + 1;
    }

    // Token: 0x06001958 RID: 6488 RVA: 0x003D0400 File Offset: 0x003D0400
    [NullableContext(1)]
    [CompilerGenerated]
    internal static void <Strings>g__End|0_1(string name, ref SpreadsheetSharedStringsParser.<>c__DisplayClass0_0 A_1)
    {
      if (name == "si")
      {
        A_1.strings.Add(SpreadsheetSheetParser.UnescapingExcelEscapes(A_1.currentString.ToString()));
        return;
      }
      if (name == "t")
      {
        A_1.isInsideText = false;
        return;
      }
      if (!(name == "rPh"))
      {
        return;
      }
      int phoneticDepth = A_1.phoneticDepth;
      A_1.phoneticDepth = phoneticDepth - 1;
    }
  }
}
