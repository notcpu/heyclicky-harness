using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x0200019B RID: 411
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpreadsheetDelimitedTextReader
  {
    // Token: 0x0600186B RID: 6251 RVA: 0x003CA8B8 File Offset: 0x003CA8B8
    public static SpreadsheetWorkbook Read(string path, bool isTabSeparated)
    {
      byte[] array;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
      {
        if (fileStream.Length > 83886080L)
        {
          throw new SpreadsheetReadException(SpreadsheetReadErrorKind.TooLarge, null);
        }
        array = new byte[fileStream.Length];
        fileStream.ReadExactly(array);
      }
      string text = SwiftText.DecodeText(array);
      if (text == null)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.Unreadable, null);
      }
      return SpreadsheetDelimitedTextReader.Read(text, Path.GetFileNameWithoutExtension(path), isTabSeparated);
    }

    // Token: 0x0600186C RID: 6252 RVA: 0x003CA938 File Offset: 0x003CA938
    public unsafe static SpreadsheetWorkbook Read(string text, string sheetName, bool isTabSeparated)
    {
      char c = (isTabSeparated ? '\t' : SpreadsheetDelimitedTextReader.SniffDelimiter(text));
      List<List<string>> list = SpreadsheetDelimitedTextReader.ParseRows(text, c);
      Dictionary<int, Dictionary<int, SpreadsheetCell>> dictionary = new Dictionary<int, Dictionary<int, SpreadsheetCell>>();
      Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
      int num = 0;
      for (int i = 0; i < list.Count; i++)
      {
        List<string> list2 = list[i];
        Dictionary<int, SpreadsheetCell> dictionary3 = new Dictionary<int, SpreadsheetCell>();
        for (int j = 0; j < list2.Count; j++)
        {
          string text2 = list2[j];
          if (text2.Length != 0)
          {
            if (num >= 1500000)
            {
              break;
            }
            double? num2 = SpreadsheetDelimitedTextReader.NumericValue(SwiftText.TrimWhitespace(text2));
            dictionary3[j] = new SpreadsheetCell(text2, text2, (num2 == null) ? SpreadsheetCellKind.Text : SpreadsheetCellKind.Number, num2, 0);
            num++;
            if (i < 200)
            {
              IEnumerable<string> enumerable = text2.Split('\n', StringSplitOptions.RemoveEmptyEntries);
              Func<string, int> func;
              if ((func = SpreadsheetDelimitedTextReader.<>O.<0>__CharacterCount) == null)
              {
                func = (SpreadsheetDelimitedTextReader.<>O.<0>__CharacterCount = new Func<string, int>(SwiftText.CharacterCount));
              }
              int num3 = enumerable.Select<string, int>(func).DefaultIfEmpty(0).Max();
              dictionary2[j] = Math.Max(dictionary2.GetValueOrDefault(j), num3);
            }
          }
        }
        if (dictionary3.Count > 0)
        {
          dictionary[i] = dictionary3;
        }
      }
      Dictionary<int, double> dictionary4 = new Dictionary<int, double>();
      int num4;
      int num5;
      foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
      {
        keyValuePair.Deconstruct(out num4, out num5);
        int num6 = num4;
        int num7 = num5;
        dictionary4[num6] = Math.Min(320.0, Math.Max(64.0, (double)num7 * 7.2 + 16.0));
      }
      Dictionary<int, Dictionary<int, SpreadsheetCell>> dictionary5 = dictionary;
      Dictionary<int, double> dictionary6 = dictionary4;
      double num8 = 100.0;
      Dictionary<int, double> dictionary7 = new Dictionary<int, double>();
      double num9 = 21.0;
      HashSet<int> hashSet = new HashSet<int>();
      HashSet<int> hashSet2 = new HashSet<int>();
      List<SpreadsheetCellRange> list3 = new List<SpreadsheetCellRange>();
      int num10 = 0;
      int num11 = 0;
      bool flag = true;
      Dictionary<SpreadsheetCellAddress, string> dictionary8 = new Dictionary<SpreadsheetCellAddress, string>();
      num5 = 1;
      List<SpreadsheetCellStyle> list4 = new List<SpreadsheetCellStyle>(num5);
      CollectionsMarshal.SetCount<SpreadsheetCellStyle>(list4, num5);
      Span<SpreadsheetCellStyle> span = CollectionsMarshal.AsSpan<SpreadsheetCellStyle>(list4);
      num4 = 0;
      *span[num4] = new SpreadsheetCellStyle();
      return new SpreadsheetWorkbook(new <>z__ReadOnlySingleElementList<SpreadsheetSheet>(new SpreadsheetSheet(sheetName, dictionary5, dictionary6, num8, dictionary7, num9, hashSet, hashSet2, list3, num10, num11, flag, dictionary8, list4)), 0);
    }

    // Token: 0x0600186D RID: 6253 RVA: 0x003CAB7C File Offset: 0x003CAB7C
    internal static double? NumericValue(string text)
    {
      if (text.Length == 0 || SwiftText.CharacterCount(text) >= 40)
      {
        return null;
      }
      double? num = SwiftText.ParseDouble(text);
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        return new double?(valueOrDefault);
      }
      string text2 = text.Replace(",", "");
      double num2 = 1.0;
      if (text2.EndsWith('%'))
      {
        string text3 = text2;
        text2 = text3.Substring(0, text3.Length - 1);
        num2 = 0.01;
      }
      foreach (string text4 in SpreadsheetDelimitedTextReader.CurrencySymbols)
      {
        if (text2.StartsWith(text4, StringComparison.Ordinal) || text2.StartsWith("-" + text4, StringComparison.Ordinal))
        {
          text2 = text2.Replace(text4, "");
        }
      }
      num = SwiftText.ParseDouble(text2);
      if (num != null)
      {
        double valueOrDefault2 = num.GetValueOrDefault();
        return new double?(valueOrDefault2 * num2);
      }
      return null;
    }

    // Token: 0x0600186E RID: 6254 RVA: 0x003CAC88 File Offset: 0x003CAC88
    private static char SniffDelimiter(string text)
    {
      string text2 = ((text.Length > 4000) ? text.Substring(0, 4000) : text).Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>() ?? "";
      int num = text2.Count<char>((char c) => c == ',');
      int num2 = text2.Count<char>((char c) => c == ';');
      int num3 = text2.Count<char>((char c) => c == '\t');
      if (num3 > num && num3 > num2)
      {
        return '\t';
      }
      if (num2 <= num)
      {
        return ',';
      }
      return ';';
    }

    // Token: 0x0600186F RID: 6255 RVA: 0x003CAD4C File Offset: 0x003CAD4C
    internal static List<List<string>> ParseRows(string text, char delimiter)
    {
      SpreadsheetDelimitedTextReader.<>c__DisplayClass5_0 CS$<>8__locals1;
      CS$<>8__locals1.rows = new List<List<string>>();
      CS$<>8__locals1.currentRow = new List<string>();
      CS$<>8__locals1.currentField = new StringBuilder();
      bool flag = false;
      bool flag2 = false;
      int i = ((text.Length > 0 && text[0] == '\ufeff') ? 1 : 0);
      while (i < text.Length)
      {
        char c = text[i];
        if (!flag)
        {
          goto IL_008E;
        }
        if (flag2)
        {
          flag2 = false;
          if (c != '"')
          {
            flag = false;
            goto IL_008E;
          }
          CS$<>8__locals1.currentField.Append('"');
        }
        else if (c == '"')
        {
          flag2 = true;
        }
        else
        {
          CS$<>8__locals1.currentField.Append(c);
        }
        IL_00D6:
        i++;
        continue;
        IL_008E:
        if (c == '"' && CS$<>8__locals1.currentField.Length == 0)
        {
          flag = true;
          goto IL_00D6;
        }
        if (c == delimiter)
        {
          SpreadsheetDelimitedTextReader.<ParseRows>g__FinishField|5_0(ref CS$<>8__locals1);
          goto IL_00D6;
        }
        if (c == '\n')
        {
          SpreadsheetDelimitedTextReader.<ParseRows>g__FinishRow|5_1(ref CS$<>8__locals1);
          goto IL_00D6;
        }
        if (c != '\r')
        {
          CS$<>8__locals1.currentField.Append(c);
          goto IL_00D6;
        }
        goto IL_00D6;
      }
      if (CS$<>8__locals1.currentField.Length > 0 || CS$<>8__locals1.currentRow.Count > 0)
      {
        SpreadsheetDelimitedTextReader.<ParseRows>g__FinishRow|5_1(ref CS$<>8__locals1);
      }
      return CS$<>8__locals1.rows;
    }

    // Token: 0x06001871 RID: 6257 RVA: 0x003CAEA0 File Offset: 0x003CAEA0
    [CompilerGenerated]
    internal static void <ParseRows>g__FinishField|5_0(ref SpreadsheetDelimitedTextReader.<>c__DisplayClass5_0 A_0)
    {
      A_0.currentRow.Add(A_0.currentField.ToString());
      A_0.currentField.Clear();
    }

    // Token: 0x06001872 RID: 6258 RVA: 0x003CAEC4 File Offset: 0x003CAEC4
    [CompilerGenerated]
    internal static void <ParseRows>g__FinishRow|5_1(ref SpreadsheetDelimitedTextReader.<>c__DisplayClass5_0 A_0)
    {
      SpreadsheetDelimitedTextReader.<ParseRows>g__FinishField|5_0(ref A_0);
      A_0.rows.Add(A_0.currentRow);
      A_0.currentRow = new List<string>();
    }

    // Token: 0x04000A65 RID: 2661
    private static readonly string[] CurrencySymbols = new string[] { "$", "€", "£", "¥", "₹" };

    // Token: 0x02000589 RID: 1417
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040020BE RID: 8382
      [Nullable(0)]
      public static Func<string, int> <0>__CharacterCount;
    }
  }
}
