using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

namespace HeyClicky.Core.Documents
{
  // Token: 0x0200019A RID: 410
  [NullableContext(1)]
  [Nullable(0)]
  public static class PdfText
  {
    // Token: 0x0600183E RID: 6206 RVA: 0x003C8A44 File Offset: 0x003C8A44
    public static PdfText.Result Extract(string path, int maximumCharacters = 60000)
    {
      return PdfText.Extract(File.ReadAllBytes(path), maximumCharacters);
    }

    // Token: 0x0600183F RID: 6207 RVA: 0x003C8A54 File Offset: 0x003C8A54
    public static PdfText.Result Extract(byte[] bytes, int maximumCharacters = 60000)
    {
      string @string = Encoding.Latin1.GetString(bytes);
      if (@string.Contains("/Encrypt", StringComparison.Ordinal) && PdfText.EncryptReference().IsMatch(@string))
      {
        return new PdfText.Result("", null, false);
      }
      Dictionary<int, PdfText.PdfObject> dictionary = PdfText.ParseObjects(bytes, @string);
      List<int> list = PdfText.PageOrder(dictionary, @string);
      StringBuilder stringBuilder = new StringBuilder();
      Dictionary<string, PdfText.FontDecoder> dictionary2 = new Dictionary<string, PdfText.FontDecoder>();
      bool flag = false;
      foreach (int num in list)
      {
        if (stringBuilder.Length >= maximumCharacters)
        {
          flag = true;
          break;
        }
        PdfText.PdfObject pdfObject;
        if (dictionary.TryGetValue(num, out pdfObject))
        {
          Dictionary<string, PdfText.FontDecoder> dictionary3 = PdfText.PageFonts(dictionary, num, pdfObject, dictionary2);
          foreach (int num2 in PdfText.ReferencesOf(pdfObject.Dictionary, "Contents", dictionary))
          {
            PdfText.PdfObject pdfObject2;
            if (dictionary.TryGetValue(num2, out pdfObject2))
            {
              byte[] stream = pdfObject2.Stream;
              if (stream != null)
              {
                PdfText.ReadContent(stream, dictionary3, stringBuilder);
              }
            }
          }
          stringBuilder.Append('\n');
        }
      }
      return new PdfText.Result(PdfText.CleanUp(stringBuilder.ToString()), (list.Count > 0) ? new int?(list.Count) : null, flag);
    }

    // Token: 0x06001840 RID: 6208 RVA: 0x003C8BD0 File Offset: 0x003C8BD0
    private static string CleanUp(string text)
    {
      IEnumerable<string> enumerable = from line in text.Split('\n', StringSplitOptions.None)
        select PdfText.MultipleSpaces().Replace(line, " ").TrimEnd();
      return PdfText.BlankLines().Replace(string.Join("\n", enumerable), "\n\n").Trim();
    }

    // Token: 0x06001841 RID: 6209 RVA: 0x003C8C2C File Offset: 0x003C8C2C
    private static Dictionary<int, PdfText.PdfObject> ParseObjects(byte[] bytes, string latin)
    {
      Dictionary<int, PdfText.PdfObject> dictionary = new Dictionary<int, PdfText.PdfObject>();
      foreach (object obj in PdfText.ObjectHeader().Matches(latin))
      {
        Match match = (Match)obj;
        int num = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        int num2 = match.Index + match.Length;
        int num3 = latin.IndexOf("endobj", num2, StringComparison.Ordinal);
        if (num3 < 0)
        {
          num3 = latin.Length;
        }
        int num4 = latin.IndexOf("stream", num2, StringComparison.Ordinal);
        byte[] array = null;
        string text;
        if (num4 >= 0 && num4 < num3 && !PdfText.IsEndStream(latin, num4))
        {
          int num5 = num2;
          text = latin.Substring(num5, num4 - num5);
          int num6 = num4 + 6;
          if (num6 < latin.Length && latin[num6] == '\r')
          {
            num6++;
          }
          if (num6 < latin.Length && latin[num6] == '\n')
          {
            num6++;
          }
          int? num7 = PdfText.DirectLength(text);
          if (num7 == null)
          {
            goto IL_012D;
          }
          int valueOrDefault = num7.GetValueOrDefault();
          if (num6 + valueOrDefault > bytes.Length || !PdfText.LooksLikeEndStream(latin, num6 + valueOrDefault))
          {
            goto IL_012D;
          }
          int num8 = num6 + valueOrDefault;
          IL_0146:
          array = RuntimeHelpers.GetSubArray<byte>(bytes, new Range(num6, Math.Max(num6, num8)));
          goto IL_017D;
          IL_012D:
          num8 = latin.IndexOf("endstream", num6, StringComparison.Ordinal);
          if (num8 < 0)
          {
            num8 = num3;
            goto IL_0146;
          }
          goto IL_0146;
        }
        else
        {
          int num5 = num2;
          text = latin.Substring(num5, num3 - num5);
        }
        IL_017D:
        dictionary[num] = new PdfText.PdfObject(text, array);
      }
      foreach (PdfText.PdfObject pdfObject in dictionary.Values.Where<PdfText.PdfObject>((PdfText.PdfObject o) => o.Dictionary.Contains("/ObjStm", StringComparison.Ordinal)).ToList<PdfText.PdfObject>())
      {
        byte[] stream = pdfObject.Stream;
        if (stream != null)
        {
          string @string = Encoding.Latin1.GetString(stream);
          int? num9 = PdfText.DirectInt(pdfObject.Dictionary, "N");
          if (num9 != null)
          {
            int valueOrDefault2 = num9.GetValueOrDefault();
            num9 = PdfText.DirectInt(pdfObject.Dictionary, "First");
            if (num9 != null)
            {
              int valueOrDefault3 = num9.GetValueOrDefault();
              string[] array2 = @string.Substring(0, Math.Min(valueOrDefault3, @string.Length)).Split(null, StringSplitOptions.RemoveEmptyEntries);
              List<ValueTuple<int, int>> list = new List<ValueTuple<int, int>>();
              int num10 = 0;
              while (num10 + 1 < array2.Length && list.Count < valueOrDefault2)
              {
                int num11;
                int num12;
                if (int.TryParse(array2[num10], out num11) && int.TryParse(array2[num10 + 1], out num12))
                {
                  list.Add(new ValueTuple<int, int>(num11, num12));
                }
                num10 += 2;
              }
              for (int i = 0; i < list.Count; i++)
              {
                int num13 = valueOrDefault3 + list[i].Item2;
                int num14 = ((i + 1 < list.Count) ? (valueOrDefault3 + list[i + 1].Item2) : @string.Length);
                if (num13 >= 0 && num13 < @string.Length && num14 > num13)
                {
                  Dictionary<int, PdfText.PdfObject> dictionary2 = dictionary;
                  int item = list[i].Item1;
                  string text2 = @string;
                  int num5 = num13;
                  dictionary2.TryAdd(item, new PdfText.PdfObject(text2.Substring(num5, Math.Min(num14, @string.Length) - num5), null));
                }
              }
            }
          }
        }
      }
      return dictionary;
    }

    // Token: 0x06001842 RID: 6210 RVA: 0x003C9008 File Offset: 0x003C9008
    private static bool IsEndStream(string latin, int at)
    {
      return at >= 3 && string.CompareOrdinal(latin, at - 3, "end", 0, 3) == 0;
    }

    // Token: 0x06001843 RID: 6211 RVA: 0x003C9024 File Offset: 0x003C9024
    private static bool LooksLikeEndStream(string latin, int at)
    {
      for (int i = at; i < Math.Min(latin.Length, at + 4); i++)
      {
        if (latin[i] == 'e')
        {
          return string.CompareOrdinal(latin, i, "endstream", 0, 9) == 0;
        }
      }
      return false;
    }

    // Token: 0x06001844 RID: 6212 RVA: 0x003C906C File Offset: 0x003C906C
    private static int? DirectLength(string dictionary)
    {
      Match match = PdfText.DirectLengthPattern().Match(dictionary);
      if (!match.Success)
      {
        return null;
      }
      return new int?(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
    }

    // Token: 0x06001845 RID: 6213 RVA: 0x003C90B8 File Offset: 0x003C90B8
    private static int? DirectInt(string dictionary, string key)
    {
      Match match = Regex.Match(dictionary, "/" + key + "\\s+(\\d+)(?!\\s+\\d+\\s+R)");
      if (!match.Success)
      {
        return null;
      }
      return new int?(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
    }

    // Token: 0x06001846 RID: 6214 RVA: 0x003C9110 File Offset: 0x003C9110
    [NullableContext(2)]
    private static byte[] Decode([Nullable(1)] string dictionary, byte[] raw)
    {
      if (raw == null)
      {
        return null;
      }
      if (!dictionary.Contains("/Filter", StringComparison.Ordinal))
      {
        return raw;
      }
      if (!dictionary.Contains("FlateDecode", StringComparison.Ordinal) && !dictionary.Contains("/Fl ", StringComparison.Ordinal))
      {
        return null;
      }
      byte[] array = PdfText.Inflate(raw);
      if (array == null)
      {
        return null;
      }
      int? num = PdfText.DirectInt(dictionary, "Predictor");
      if (num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        if (valueOrDefault >= 10)
        {
          array = PdfText.Unpredict(array, PdfText.DirectInt(dictionary, "Columns").GetValueOrDefault(1));
        }
      }
      return array;
    }

    // Token: 0x06001847 RID: 6215 RVA: 0x003C919C File Offset: 0x003C919C
    [return: Nullable(2)]
    private static byte[] Inflate(byte[] raw)
    {
      foreach (bool flag in new bool[]
      {
        default(bool),
        true
      })
      {
        try
        {
          using (MemoryStream memoryStream = new MemoryStream(raw, flag ? 2 : 0, raw.Length - (flag ? 2 : 0)))
          {
            using (Stream stream = (flag ? new DeflateStream(memoryStream, CompressionMode.Decompress) : new ZLibStream(memoryStream, CompressionMode.Decompress)))
            {
              using (MemoryStream memoryStream2 = new MemoryStream())
              {
                byte[] array2 = new byte[16384];
                try
                {
                  int num;
                  while ((num = stream.Read(array2, 0, array2.Length)) > 0)
                  {
                    memoryStream2.Write(array2, 0, num);
                  }
                }
                catch (InvalidDataException obj) when (memoryStream2.Length > 0L)
                {
                }
                if (memoryStream2.Length > 0L)
                {
                  return memoryStream2.ToArray();
                }
              }
            }
          }
        }
        catch (Exception)
        {
        }
      }
      return null;
    }

    // Token: 0x06001848 RID: 6216 RVA: 0x003C92D8 File Offset: 0x003C92D8
    private static byte[] Unpredict(byte[] data, int columns)
    {
      int num = columns + 1;
      int num2 = data.Length / num;
      byte[] array = new byte[num2 * columns];
      byte[] array2 = new byte[columns];
      for (int i = 0; i < num2; i++)
      {
        byte b = data[i * num];
        for (int j = 0; j < columns; j++)
        {
          byte b2 = data[i * num + 1 + j];
          byte b3 = ((j > 0) ? array[i * columns + j - 1] : 0);
          byte b4 = array2[j];
          byte b5 = ((j > 0) ? array2[j - 1] : 0);
          int num3;
          switch (b)
          {
          case 1:
            num3 = (int)(b2 + b3);
            break;
          case 2:
            num3 = (int)(b2 + b4);
            break;
          case 3:
            num3 = (int)(b2 + (b3 + b4) / 2);
            break;
          case 4:
            num3 = (int)b2 + PdfText.Paeth((int)b3, (int)b4, (int)b5);
            break;
          default:
            num3 = (int)b2;
            break;
          }
          int num4 = num3;
          array[i * columns + j] = (byte)num4;
        }
        Array.Copy(array, i * columns, array2, 0, columns);
      }
      return array;
    }

    // Token: 0x06001849 RID: 6217 RVA: 0x003C93DC File Offset: 0x003C93DC
    private static int Paeth(int a, int b, int c)
    {
      int num = a + b - c;
      int num2 = Math.Abs(num - a);
      int num3 = Math.Abs(num - b);
      int num4 = Math.Abs(num - c);
      if (num2 <= num3 && num2 <= num4)
      {
        return a;
      }
      if (num3 > num4)
      {
        return c;
      }
      return b;
    }

    // Token: 0x0600184A RID: 6218 RVA: 0x003C941C File Offset: 0x003C941C
    private static List<int> PageOrder(Dictionary<int, PdfText.PdfObject> objects, string latin)
    {
      PdfText.<>c__DisplayClass14_0 CS$<>8__locals1;
      CS$<>8__locals1.objects = objects;
      CS$<>8__locals1.pages = new List<int>();
      Match match = PdfText.RootReference().Matches(latin).LastOrDefault<Match>();
      PdfText.PdfObject pdfObject;
      if (match != null && CS$<>8__locals1.objects.TryGetValue(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture), out pdfObject))
      {
        int? num = PdfText.ReferenceOf(pdfObject.Dictionary, "Pages");
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          PdfText.<>c__DisplayClass14_1 CS$<>8__locals2;
          CS$<>8__locals2.visited = new HashSet<int>();
          PdfText.<PageOrder>g__Walk|14_3(valueOrDefault, 0, ref CS$<>8__locals1, ref CS$<>8__locals2);
        }
      }
      if (CS$<>8__locals1.pages.Count == 0)
      {
        CS$<>8__locals1.pages.AddRange(from o in CS$<>8__locals1.objects
          where PdfText.PageType().IsMatch(o.Value.Dictionary)
          select o.Key into n
          orderby n
          select n);
      }
      return CS$<>8__locals1.pages;
    }

    // Token: 0x0600184B RID: 6219 RVA: 0x003C9548 File Offset: 0x003C9548
    private static int? ReferenceOf(string dictionary, string key)
    {
      Match match = Regex.Match(dictionary, "/" + key + "\\s+(\\d+)\\s+\\d+\\s+R");
      if (!match.Success)
      {
        return null;
      }
      return new int?(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
    }

    // Token: 0x0600184C RID: 6220 RVA: 0x003C95A0 File Offset: 0x003C95A0
    private static IEnumerable<int> ReferencesOf(string dictionary, string key, Dictionary<int, PdfText.PdfObject> objects)
    {
      Match match = Regex.Match(dictionary, "/" + key + "\\s*\\[([^\\]]*)\\]");
      if (match.Success)
      {
        return PdfText.ArrayReferences(match.Groups[1].Value);
      }
      int? num = PdfText.ReferenceOf(dictionary, key);
      if (num == null)
      {
        return Array.Empty<int>();
      }
      int valueOrDefault = num.GetValueOrDefault();
      PdfText.PdfObject pdfObject;
      if (objects.TryGetValue(valueOrDefault, out pdfObject) && pdfObject.RawStream == null && pdfObject.Dictionary.TrimStart().StartsWith('['))
      {
        return PdfText.ArrayReferences(pdfObject.Dictionary);
      }
      return new <>z__ReadOnlySingleElementList<int>(valueOrDefault);
    }

    // Token: 0x0600184D RID: 6221 RVA: 0x003C963C File Offset: 0x003C963C
    private static IEnumerable<int> ArrayReferences(string text)
    {
      return (from m in PdfText.IndirectReference().Matches(text)
        select int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture)).ToList<int>();
    }

    // Token: 0x0600184E RID: 6222 RVA: 0x003C9674 File Offset: 0x003C9674
    private static char PdfDocEncoding(byte b)
    {
      if (b >= 32)
      {
        if (b == 133)
        {
          return '…';
        }
        switch (b)
        {
        case 145:
          return '‘';
        case 146:
          return '’';
        case 147:
          return '“';
        case 148:
          return '”';
        case 149:
          return '•';
        case 150:
          return '–';
        case 151:
          return '—';
        }
      }
      else if (b - 9 > 1 && b != 13)
      {
        return ' ';
      }
      return (char)b;
    }

    // Token: 0x0600184F RID: 6223 RVA: 0x003C9718 File Offset: 0x003C9718
    private static Dictionary<string, PdfText.FontDecoder> PageFonts(Dictionary<int, PdfText.PdfObject> objects, int page, PdfText.PdfObject pageObject, Dictionary<string, PdfText.FontDecoder> cache)
    {
      Dictionary<string, PdfText.FontDecoder> dictionary = new Dictionary<string, PdfText.FontDecoder>();
      string text = null;
      PdfText.PdfObject pdfObject = pageObject;
      int num = 0;
      while (num < 32 && pdfObject != null && text == null)
      {
        text = PdfText.InlineOrReferencedDictionary(pdfObject.Dictionary, "Resources", objects);
        int? num2 = PdfText.ReferenceOf(pdfObject.Dictionary, "Parent");
        if (num2 == null)
        {
          goto IL_0051;
        }
        int valueOrDefault = num2.GetValueOrDefault();
        PdfText.PdfObject pdfObject2;
        if (!objects.TryGetValue(valueOrDefault, out pdfObject2))
        {
          goto IL_0051;
        }
        PdfText.PdfObject pdfObject3 = pdfObject2;
        IL_0056:
        pdfObject = pdfObject3;
        num++;
        continue;
        IL_0051:
        pdfObject3 = null;
        goto IL_0056;
      }
      if (text == null)
      {
        return dictionary;
      }
      string text2 = PdfText.InlineOrReferencedDictionary(text, "Font", objects);
      if (text2 == null)
      {
        return dictionary;
      }
      foreach (object obj in PdfText.FontEntry().Matches(text2))
      {
        Match match = (Match)obj;
        string value = match.Groups[1].Value;
        string value2 = match.Groups[2].Value;
        PdfText.FontDecoder fontDecoder;
        if (!cache.TryGetValue(value2, out fontDecoder))
        {
          fontDecoder = new PdfText.FontDecoder();
          PdfText.PdfObject pdfObject4;
          if (objects.TryGetValue(int.Parse(value2, CultureInfo.InvariantCulture), out pdfObject4))
          {
            if (pdfObject4.Dictionary.Contains("/Type0", StringComparison.Ordinal))
            {
              fontDecoder.CodeBytes = 2;
            }
            int? num2 = PdfText.ReferenceOf(pdfObject4.Dictionary, "ToUnicode");
            if (num2 != null)
            {
              int valueOrDefault2 = num2.GetValueOrDefault();
              PdfText.PdfObject pdfObject5;
              if (objects.TryGetValue(valueOrDefault2, out pdfObject5))
              {
                byte[] stream = pdfObject5.Stream;
                if (stream != null)
                {
                  PdfText.ReadCMap(Encoding.Latin1.GetString(stream), fontDecoder);
                }
              }
            }
          }
          cache[value2] = fontDecoder;
        }
        dictionary[value] = fontDecoder;
      }
      return dictionary;
    }

    // Token: 0x06001850 RID: 6224 RVA: 0x003C98D0 File Offset: 0x003C98D0
    [return: Nullable(2)]
    private static string InlineOrReferencedDictionary(string dictionary, string key, Dictionary<int, PdfText.PdfObject> objects)
    {
      int? num = PdfText.ReferenceOf(dictionary, key);
      if (num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        PdfText.PdfObject pdfObject;
        if (!objects.TryGetValue(valueOrDefault, out pdfObject))
        {
          return null;
        }
        return pdfObject.Dictionary;
      }
      else
      {
        int num2 = dictionary.IndexOf("/" + key, StringComparison.Ordinal);
        if (num2 < 0)
        {
          return null;
        }
        int num3 = dictionary.IndexOf("<<", num2, StringComparison.Ordinal);
        if (num3 < 0)
        {
          return null;
        }
        int num4 = 0;
        for (int i = num3; i < dictionary.Length - 1; i++)
        {
          if (dictionary[i] == '<' && dictionary[i + 1] == '<')
          {
            num4++;
            i++;
          }
          else if (dictionary[i] == '>' && dictionary[i + 1] == '>')
          {
            num4--;
            i++;
            if (num4 == 0)
            {
              int num5 = num3;
              return dictionary.Substring(num5, i + 1 - num5);
            }
          }
        }
        return null;
      }
    }

    // Token: 0x06001851 RID: 6225 RVA: 0x003C99B4 File Offset: 0x003C99B4
    private static void ReadCMap(string cmap, PdfText.FontDecoder decoder)
    {
      Match match = PdfText.CodespaceRange().Match(cmap);
      if (match.Success)
      {
        decoder.CodeBytes = Math.Max(1, match.Groups[1].Value.Length / 2);
      }
      foreach (object obj in PdfText.BfCharBlock().Matches(cmap))
      {
        Match match2 = (Match)obj;
        foreach (object obj2 in PdfText.HexPair().Matches(match2.Groups[1].Value))
        {
          Match match3 = (Match)obj2;
          decoder.Map[PdfText.ParseHex(match3.Groups[1].Value)] = PdfText.HexToUnicode(match3.Groups[2].Value);
        }
      }
      foreach (object obj3 in PdfText.BfRangeBlock().Matches(cmap))
      {
        Match match4 = (Match)obj3;
        foreach (object obj4 in PdfText.BfRangeLine().Matches(match4.Groups[1].Value))
        {
          Match match5 = (Match)obj4;
          int num = PdfText.ParseHex(match5.Groups[1].Value);
          int num2 = PdfText.ParseHex(match5.Groups[2].Value);
          if (num2 - num <= 65536 && num2 >= num)
          {
            if (match5.Groups[3].Success)
            {
              string text = PdfText.HexToUnicode(match5.Groups[3].Value);
              if (text.Length != 0)
              {
                string text2 = text;
                char c = text2[text2.Length - 1];
                for (int i = num; i <= num2; i++)
                {
                  Dictionary<int, string> map = decoder.Map;
                  int num3 = i;
                  string text3 = text;
                  ReadOnlySpan<char> readOnlySpan = text3.Substring(0, text3.Length - 1);
                  char c2 = (char)((int)c + (i - num));
                  map[num3] = readOnlySpan + new ReadOnlySpan<char>(ref c2);
                }
              }
            }
            else
            {
              List<string> list = (from m in PdfText.HexString().Matches(match5.Groups[4].Value)
                select PdfText.HexToUnicode(m.Groups[1].Value)).ToList<string>();
              int num4 = num;
              while (num4 <= num2 && num4 - num < list.Count)
              {
                decoder.Map[num4] = list[num4 - num];
                num4++;
              }
            }
          }
        }
      }
    }

    // Token: 0x06001852 RID: 6226 RVA: 0x003C9D2C File Offset: 0x003C9D2C
    private static int ParseHex(string hex)
    {
      int num;
      if (!int.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
      {
        return 0;
      }
      return num;
    }

    // Token: 0x06001853 RID: 6227 RVA: 0x003C9D50 File Offset: 0x003C9D50
    private static string HexToUnicode(string hex)
    {
      hex = hex.Trim();
      if (hex.Length % 4 != 0)
      {
        hex = hex.PadRight(hex.Length + 4 - hex.Length % 4, '0');
      }
      StringBuilder stringBuilder = new StringBuilder(hex.Length / 4);
      int num = 0;
      while (num + 3 < hex.Length)
      {
        stringBuilder.Append((char)PdfText.ParseHex(hex.Substring(num, 4)));
        num += 4;
      }
      return stringBuilder.ToString();
    }

    // Token: 0x06001854 RID: 6228 RVA: 0x003C9DC8 File Offset: 0x003C9DC8
    private static void ReadContent(byte[] content, Dictionary<string, PdfText.FontDecoder> fonts, StringBuilder output)
    {
      PdfText.<>c__DisplayClass25_0 CS$<>8__locals1;
      CS$<>8__locals1.output = output;
      PdfText.FontDecoder fontDecoder = new PdfText.FontDecoder();
      PdfText.FontDecoder fontDecoder2 = fontDecoder;
      List<object> list = new List<object>();
      CS$<>8__locals1.lineStart = CS$<>8__locals1.output.Length;
      int i = 0;
      while (i < content.Length)
      {
        char c = (char)content[i];
        if (char.IsWhiteSpace(c) || c == '\0')
        {
          i++;
        }
        else if (c == '%')
        {
          while (i < content.Length && content[i] != 10)
          {
            if (content[i] == 13)
            {
              break;
            }
            i++;
          }
        }
        else if (c == '(')
        {
          list.Add(PdfText.ReadLiteral(content, ref i));
        }
        else if (c == '<' && i + 1 < content.Length && content[i + 1] == 60)
        {
          i += 2;
          PdfText.SkipDictionary(content, ref i);
        }
        else if (c == '<')
        {
          list.Add(PdfText.ReadHex(content, ref i));
        }
        else if (c == '[')
        {
          list.Add(PdfText.ReadArray(content, ref i));
        }
        else if (c == '/')
        {
          list.Add("/" + PdfText.ReadToken(content, ref i, 1));
        }
        else
        {
          bool flag = char.IsAsciiDigit(c);
          if (!flag)
          {
            bool flag2;
            switch (c)
            {
            case '+':
            case '-':
            case '.':
              flag2 = true;
              break;
            case ',':
              goto IL_0147;
            default:
              goto IL_0147;
            }
            IL_014A:
            flag = flag2;
            goto IL_014E;
            IL_0147:
            flag2 = false;
            goto IL_014A;
          }
          IL_014E:
          if (flag)
          {
            string text = PdfText.ReadToken(content, ref i, 0);
            double num;
            list.Add(double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? num : 0.0);
          }
          else
          {
            string text2 = PdfText.ReadToken(content, ref i, 0);
            if (text2.Length == 0)
            {
              i++;
            }
            else
            {
              if (text2 != null)
              {
                int length = text2.Length;
                if (length != 1)
                {
                  if (length == 2)
                  {
                    char c2 = text2[1];
                    if (c2 <= 'J')
                    {
                      if (c2 <= 'D')
                      {
                        if (c2 != '*')
                        {
                          if (c2 != 'D')
                          {
                            goto IL_05BC;
                          }
                          if (!(text2 == "TD"))
                          {
                            goto IL_05BC;
                          }
                          goto IL_04FB;
                        }
                        else if (!(text2 == "T*"))
                        {
                          goto IL_05BC;
                        }
                      }
                      else if (c2 != 'I')
                      {
                        if (c2 != 'J')
                        {
                          goto IL_05BC;
                        }
                        if (!(text2 == "TJ"))
                        {
                          goto IL_05BC;
                        }
                        List<object> list2 = list.LastOrDefault<object>() as List<object>;
                        if (list2 == null)
                        {
                          goto IL_05BC;
                        }
                        using (List<object>.Enumerator enumerator = list2.GetEnumerator())
                        {
                          while (enumerator.MoveNext())
                          {
                            object obj = enumerator.Current;
                            byte[] array = obj as byte[];
                            if (array != null)
                            {
                              CS$<>8__locals1.output.Append(fontDecoder2.Decode(array));
                            }
                            else if (obj is double)
                            {
                              double num2 = (double)obj;
                              if (num2 < -180.0 && CS$<>8__locals1.output.Length > CS$<>8__locals1.lineStart)
                              {
                                StringBuilder output2 = CS$<>8__locals1.output;
                                if (output2[output2.Length - 1] != ' ')
                                {
                                  CS$<>8__locals1.output.Append(' ');
                                }
                              }
                            }
                          }
                          goto IL_05BC;
                        }
                      }
                      else
                      {
                        if (!(text2 == "BI"))
                        {
                          goto IL_05BC;
                        }
                        while (i + 1 < content.Length && (content[i] != 69 || content[i + 1] != 73 || (i != 0 && !char.IsWhiteSpace((char)content[i - 1]))))
                        {
                          i++;
                        }
                        i += 2;
                        goto IL_05BC;
                      }
                      PdfText.<ReadContent>g__NewLine|25_0(ref CS$<>8__locals1);
                      goto IL_05BC;
                    }
                    if (c2 <= 'd')
                    {
                      if (c2 != 'T')
                      {
                        if (c2 != 'd')
                        {
                          goto IL_05BC;
                        }
                        if (!(text2 == "Td"))
                        {
                          goto IL_05BC;
                        }
                      }
                      else
                      {
                        if (!(text2 == "ET"))
                        {
                          goto IL_05BC;
                        }
                        if (CS$<>8__locals1.output.Length <= CS$<>8__locals1.lineStart)
                        {
                          goto IL_05BC;
                        }
                        StringBuilder output3 = CS$<>8__locals1.output;
                        if (output3[output3.Length - 1] != ' ')
                        {
                          CS$<>8__locals1.output.Append(' ');
                          goto IL_05BC;
                        }
                        goto IL_05BC;
                      }
                    }
                    else if (c2 != 'f')
                    {
                      if (c2 != 'j')
                      {
                        if (c2 != 'm')
                        {
                          goto IL_05BC;
                        }
                        if (!(text2 == "Tm"))
                        {
                          goto IL_05BC;
                        }
                        PdfText.<ReadContent>g__NewLine|25_0(ref CS$<>8__locals1);
                        goto IL_05BC;
                      }
                      else
                      {
                        if (!(text2 == "Tj"))
                        {
                          goto IL_05BC;
                        }
                        byte[] array2 = list.LastOrDefault<object>() as byte[];
                        if (array2 != null)
                        {
                          CS$<>8__locals1.output.Append(fontDecoder2.Decode(array2));
                          goto IL_05BC;
                        }
                        goto IL_05BC;
                      }
                    }
                    else
                    {
                      if (!(text2 == "Tf"))
                      {
                        goto IL_05BC;
                      }
                      if (list.Count < 2)
                      {
                        goto IL_05BC;
                      }
                      List<object> list3 = list;
                      string text3 = list3[list3.Count - 2] as string;
                      if (text3 != null && text3.StartsWith('/'))
                      {
                        string text4 = text3;
                        PdfText.FontDecoder fontDecoder3;
                        fontDecoder2 = (fonts.TryGetValue(text4.Substring(1, text4.Length - 1), out fontDecoder3) ? fontDecoder3 : fontDecoder);
                        goto IL_05BC;
                      }
                      goto IL_05BC;
                    }
                    IL_04FB:
                    if (list.Count >= 2)
                    {
                      List<object> list4 = list;
                      object obj2 = list4[list4.Count - 1];
                      if (obj2 is double)
                      {
                        double num3 = (double)obj2;
                        if (Math.Abs(num3) > 0.01)
                        {
                          PdfText.<ReadContent>g__NewLine|25_0(ref CS$<>8__locals1);
                          goto IL_05BC;
                        }
                      }
                    }
                    if (CS$<>8__locals1.output.Length > CS$<>8__locals1.lineStart)
                    {
                      StringBuilder output4 = CS$<>8__locals1.output;
                      if (output4[output4.Length - 1] != ' ')
                      {
                        CS$<>8__locals1.output.Append(' ');
                      }
                    }
                  }
                }
                else
                {
                  char c2 = text2[0];
                  if (c2 == '"' || c2 == '\'')
                  {
                    PdfText.<ReadContent>g__NewLine|25_0(ref CS$<>8__locals1);
                    byte[] array3 = list.LastOrDefault<object>() as byte[];
                    if (array3 != null)
                    {
                      CS$<>8__locals1.output.Append(fontDecoder2.Decode(array3));
                    }
                  }
                }
              }
              IL_05BC:
              list.Clear();
            }
          }
        }
      }
      PdfText.<ReadContent>g__NewLine|25_0(ref CS$<>8__locals1);
    }

    // Token: 0x06001855 RID: 6229 RVA: 0x003CA3B8 File Offset: 0x003CA3B8
    private static string ReadToken(byte[] content, ref int i, int skip)
    {
      i += skip;
      int num = i;
      while (i < content.Length)
      {
        char c = (char)content[i];
        bool flag = char.IsWhiteSpace(c);
        if (!flag)
        {
          if (c <= '>')
          {
            if (c <= '/')
            {
              switch (c)
              {
              case '%':
              case '(':
              case ')':
                goto IL_006F;
              case '&':
              case '\'':
                goto IL_0073;
              default:
                if (c != '/')
                {
                  goto IL_0073;
                }
                goto IL_006F;
              }
            }
            else
            {
              if (c != '<' && c != '>')
              {
                goto IL_0073;
              }
              goto IL_006F;
            }
          }
          else if (c <= ']')
          {
            if (c != '[' && c != ']')
            {
              goto IL_0073;
            }
            goto IL_006F;
          }
          else
          {
            if (c == '{' || c == '}')
            {
              goto IL_006F;
            }
            goto IL_0073;
          }
          IL_0075:
          bool flag2;
          flag = flag2;
          goto IL_0077;
          IL_0073:
          flag2 = false;
          goto IL_0075;
          IL_006F:
          flag2 = true;
          goto IL_0075;
        }
        IL_0077:
        if (flag)
        {
          break;
        }
        i++;
      }
      return Encoding.Latin1.GetString(content, num, i - num);
    }

    // Token: 0x06001856 RID: 6230 RVA: 0x003CA45C File Offset: 0x003CA45C
    private static byte[] ReadLiteral(byte[] content, ref int i)
    {
      i++;
      int num = 1;
      List<byte> list = new List<byte>();
      while (i < content.Length)
      {
        int num2 = i;
        i = num2 + 1;
        byte b = content[num2];
        if (b == 92 && i < content.Length)
        {
          num2 = i;
          i = num2 + 1;
          byte b2 = content[num2];
          char c = (char)b2;
          if (c >= '0')
          {
            if (c <= '7')
            {
              int num3 = (int)(b2 - 48);
              int num4 = 0;
              while (num4 < 2 && i < content.Length)
              {
                byte b3 = content[i];
                if (b3 < 48 || b3 > 55)
                {
                  break;
                }
                int num5 = num3 * 8;
                num2 = i;
                i = num2 + 1;
                num3 = num5 + (int)(content[num2] - 48);
                num4++;
              }
              list.Add((byte)num3);
              continue;
            }
            if (c <= 'f')
            {
              if (c == 'b')
              {
                list.Add(8);
                continue;
              }
              if (c == 'f')
              {
                list.Add(12);
                continue;
              }
            }
            else
            {
              if (c == 'n')
              {
                list.Add(10);
                continue;
              }
              if (c == 'r')
              {
                list.Add(13);
                continue;
              }
              if (c == 't')
              {
                list.Add(9);
                continue;
              }
            }
          }
          else
          {
            if (c == '\n')
            {
              continue;
            }
            if (c == '\r')
            {
              if (i < content.Length && content[i] == 10)
              {
                i++;
                continue;
              }
              continue;
            }
          }
          list.Add(b2);
        }
        else
        {
          if (b == 40)
          {
            num++;
          }
          else if (b == 41 && --num == 0)
          {
            break;
          }
          list.Add(b);
        }
      }
      return list.ToArray();
    }

    // Token: 0x06001857 RID: 6231 RVA: 0x003CA5E4 File Offset: 0x003CA5E4
    private static byte[] ReadHex(byte[] content, ref int i)
    {
      i++;
      StringBuilder stringBuilder = new StringBuilder();
      while (i < content.Length && content[i] != 62)
      {
        int num = i;
        i = num + 1;
        char c = (char)content[num];
        if (Uri.IsHexDigit(c))
        {
          stringBuilder.Append(c);
        }
      }
      i++;
      if (stringBuilder.Length % 2 == 1)
      {
        stringBuilder.Append('0');
      }
      return Convert.FromHexString(stringBuilder.ToString());
    }

    // Token: 0x06001858 RID: 6232 RVA: 0x003CA650 File Offset: 0x003CA650
    private static List<object> ReadArray(byte[] content, ref int i)
    {
      i++;
      List<object> list = new List<object>();
      while (i < content.Length && content[i] != 93)
      {
        char c = (char)content[i];
        if (char.IsWhiteSpace(c))
        {
          i++;
        }
        else if (c == '(')
        {
          list.Add(PdfText.ReadLiteral(content, ref i));
        }
        else if (c == '<')
        {
          list.Add(PdfText.ReadHex(content, ref i));
        }
        else
        {
          string text = PdfText.ReadToken(content, ref i, 0);
          if (text.Length == 0)
          {
            i++;
          }
          else
          {
            double num;
            list.Add(double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? num : 0.0);
          }
        }
      }
      i++;
      return list;
    }

    // Token: 0x06001859 RID: 6233 RVA: 0x003CA708 File Offset: 0x003CA708
    private static void SkipDictionary(byte[] content, ref int i)
    {
      int num = 1;
      while (i + 1 < content.Length && num > 0)
      {
        if (content[i] == 60 && content[i + 1] == 60)
        {
          num++;
          i += 2;
        }
        else if (content[i] == 62 && content[i + 1] == 62)
        {
          num--;
          i += 2;
        }
        else
        {
          i++;
        }
      }
    }

    // Token: 0x0600185A RID: 6234 RVA: 0x003CA768 File Offset: 0x003CA768
    [GeneratedRegex("(?<![\\d])(\\d+)\\s+(\\d+)\\s+obj\\b")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex ObjectHeader()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ObjectHeader_20.Instance;
    }

    // Token: 0x0600185B RID: 6235 RVA: 0x003CA770 File Offset: 0x003CA770
    [GeneratedRegex("/Length\\s+(\\d+)(?!\\s+\\d+\\s+R)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex DirectLengthPattern()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DirectLengthPattern_21.Instance;
    }

    // Token: 0x0600185C RID: 6236 RVA: 0x003CA778 File Offset: 0x003CA778
    [GeneratedRegex("/Root\\s+(\\d+)\\s+\\d+\\s+R")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex RootReference()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__RootReference_22.Instance;
    }

    // Token: 0x0600185D RID: 6237 RVA: 0x003CA780 File Offset: 0x003CA780
    [GeneratedRegex("/Encrypt\\s+(\\d+\\s+\\d+\\s+R|<<)")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex EncryptReference()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__EncryptReference_23.Instance;
    }

    // Token: 0x0600185E RID: 6238 RVA: 0x003CA788 File Offset: 0x003CA788
    [GeneratedRegex("/Type\\s*/Page(?![s\\w])")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex PageType()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PageType_24.Instance;
    }

    // Token: 0x0600185F RID: 6239 RVA: 0x003CA790 File Offset: 0x003CA790
    [GeneratedRegex("(\\d+)\\s+\\d+\\s+R")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex IndirectReference()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__IndirectReference_25.Instance;
    }

    // Token: 0x06001860 RID: 6240 RVA: 0x003CA798 File Offset: 0x003CA798
    [GeneratedRegex("/([^\\s/<>\\[\\]()]+)\\s+(\\d+)\\s+\\d+\\s+R")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex FontEntry()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__FontEntry_26.Instance;
    }

    // Token: 0x06001861 RID: 6241 RVA: 0x003CA7A0 File Offset: 0x003CA7A0
    [GeneratedRegex("begincodespacerange\\s*<([0-9A-Fa-f]+)>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex CodespaceRange()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__CodespaceRange_27.Instance;
    }

    // Token: 0x06001862 RID: 6242 RVA: 0x003CA7A8 File Offset: 0x003CA7A8
    [GeneratedRegex("beginbfchar(.*?)endbfchar", RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BfCharBlock()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfCharBlock_28.Instance;
    }

    // Token: 0x06001863 RID: 6243 RVA: 0x003CA7B0 File Offset: 0x003CA7B0
    [GeneratedRegex("beginbfrange(.*?)endbfrange", RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BfRangeBlock()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeBlock_29.Instance;
    }

    // Token: 0x06001864 RID: 6244 RVA: 0x003CA7B8 File Offset: 0x003CA7B8
    [GeneratedRegex("<([0-9A-Fa-f]+)>\\s*<([0-9A-Fa-f]*)>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex HexPair()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexPair_30.Instance;
    }

    // Token: 0x06001865 RID: 6245 RVA: 0x003CA7C0 File Offset: 0x003CA7C0
    [GeneratedRegex("<([0-9A-Fa-f]+)>\\s*<([0-9A-Fa-f]+)>\\s*(?:<([0-9A-Fa-f]*)>|\\[([^\\]]*)\\])")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BfRangeLine()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BfRangeLine_31.Instance;
    }

    // Token: 0x06001866 RID: 6246 RVA: 0x003CA7C8 File Offset: 0x003CA7C8
    [GeneratedRegex("<([0-9A-Fa-f]*)>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex HexString()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__HexString_32.Instance;
    }

    // Token: 0x06001867 RID: 6247 RVA: 0x003CA7D0 File Offset: 0x003CA7D0
    [GeneratedRegex("[ \\t]{2,}")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex MultipleSpaces()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33.Instance;
    }

    // Token: 0x06001868 RID: 6248 RVA: 0x003CA7D8 File Offset: 0x003CA7D8
    [GeneratedRegex("\\n{3,}")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex BlankLines()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__BlankLines_34.Instance;
    }

    // Token: 0x06001869 RID: 6249 RVA: 0x003CA7E0 File Offset: 0x003CA7E0
    [CompilerGenerated]
    internal static void <PageOrder>g__Walk|14_3(int number, int depth, ref PdfText.<>c__DisplayClass14_0 A_2, ref PdfText.<>c__DisplayClass14_1 A_3)
    {
      PdfText.PdfObject pdfObject;
      if (depth > 64 || !A_3.visited.Add(number) || !A_2.objects.TryGetValue(number, out pdfObject))
      {
        return;
      }
      if (PdfText.PageType().IsMatch(pdfObject.Dictionary))
      {
        A_2.pages.Add(number);
        return;
      }
      foreach (int num in PdfText.ReferencesOf(pdfObject.Dictionary, "Kids", A_2.objects))
      {
        PdfText.<PageOrder>g__Walk|14_3(num, depth + 1, ref A_2, ref A_3);
      }
    }

    // Token: 0x0600186A RID: 6250 RVA: 0x003CA884 File Offset: 0x003CA884
    [CompilerGenerated]
    internal static void <ReadContent>g__NewLine|25_0(ref PdfText.<>c__DisplayClass25_0 A_0)
    {
      if (A_0.output.Length > A_0.lineStart)
      {
        A_0.output.Append('\n');
        A_0.lineStart = A_0.output.Length;
      }
    }

    // Token: 0x02000582 RID: 1410
    [Nullable(0)]
    public sealed class Result : IEquatable<PdfText.Result>
    {
      // Token: 0x06003573 RID: 13683 RVA: 0x00441A40 File Offset: 0x00441A40
      public Result(string Text, int? PageCount, bool StoppedEarly)
      {
        this.Text = Text;
        this.PageCount = PageCount;
        this.StoppedEarly = StoppedEarly;
        base..ctor();
      }

      // Token: 0x17000969 RID: 2409
      // (get) Token: 0x06003574 RID: 13684 RVA: 0x00441A60 File Offset: 0x00441A60
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(PdfText.Result);
        }
      }

      // Token: 0x1700096A RID: 2410
      // (get) Token: 0x06003575 RID: 13685 RVA: 0x00441A6C File Offset: 0x00441A6C
      // (set) Token: 0x06003576 RID: 13686 RVA: 0x00441A74 File Offset: 0x00441A74
      public string Text { get; set; }

      // Token: 0x1700096B RID: 2411
      // (get) Token: 0x06003577 RID: 13687 RVA: 0x00441A80 File Offset: 0x00441A80
      // (set) Token: 0x06003578 RID: 13688 RVA: 0x00441A88 File Offset: 0x00441A88
      public int? PageCount { get; set; }

      // Token: 0x1700096C RID: 2412
      // (get) Token: 0x06003579 RID: 13689 RVA: 0x00441A94 File Offset: 0x00441A94
      // (set) Token: 0x0600357A RID: 13690 RVA: 0x00441A9C File Offset: 0x00441A9C
      public bool StoppedEarly { get; set; }

      // Token: 0x0600357B RID: 13691 RVA: 0x00441AA8 File Offset: 0x00441AA8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Result");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600357C RID: 13692 RVA: 0x00441AF4 File Offset: 0x00441AF4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", PageCount = ");
        builder.Append(this.PageCount.ToString());
        builder.Append(", StoppedEarly = ");
        builder.Append(this.StoppedEarly.ToString());
        return true;
      }

      // Token: 0x0600357D RID: 13693 RVA: 0x00441B70 File Offset: 0x00441B70
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(PdfText.Result left, PdfText.Result right)
      {
        return !(left == right);
      }

      // Token: 0x0600357E RID: 13694 RVA: 0x00441B7C File Offset: 0x00441B7C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(PdfText.Result left, PdfText.Result right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600357F RID: 13695 RVA: 0x00441B90 File Offset: 0x00441B90
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<PageCount>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<StoppedEarly>k__BackingField);
      }

      // Token: 0x06003580 RID: 13696 RVA: 0x00441BF4 File Offset: 0x00441BF4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as PdfText.Result);
      }

      // Token: 0x06003581 RID: 13697 RVA: 0x00441C04 File Offset: 0x00441C04
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(PdfText.Result other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<PageCount>k__BackingField, other.<PageCount>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<StoppedEarly>k__BackingField, other.<StoppedEarly>k__BackingField));
      }

      // Token: 0x06003583 RID: 13699 RVA: 0x00441C80 File Offset: 0x00441C80
      [CompilerGenerated]
      private Result(PdfText.Result original)
      {
        this.Text = original.<Text>k__BackingField;
        this.PageCount = original.<PageCount>k__BackingField;
        this.StoppedEarly = original.<StoppedEarly>k__BackingField;
      }

      // Token: 0x06003584 RID: 13700 RVA: 0x00441CAC File Offset: 0x00441CAC
      [CompilerGenerated]
      public void Deconstruct(out string Text, out int? PageCount, out bool StoppedEarly)
      {
        Text = this.Text;
        PageCount = this.PageCount;
        StoppedEarly = this.StoppedEarly;
      }
    }

    // Token: 0x02000583 RID: 1411
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class PdfObject
    {
      // Token: 0x06003585 RID: 13701 RVA: 0x00441CCC File Offset: 0x00441CCC
      [NullableContext(1)]
      public PdfObject(string dictionary, [Nullable(2)] byte[] stream)
      {
      }

      // Token: 0x1700096D RID: 2413
      // (get) Token: 0x06003586 RID: 13702 RVA: 0x00441CE4 File Offset: 0x00441CE4
      public byte[] Stream
      {
        get
        {
          byte[] array;
          if ((array = this.decoded) == null)
          {
            array = (this.decoded = PdfText.Decode(this.Dictionary, this.RawStream));
          }
          return array;
        }
      }

      // Token: 0x040020AC RID: 8364
      [Nullable(1)]
      public string Dictionary = dictionary;

      // Token: 0x040020AD RID: 8365
      public byte[] RawStream = stream;

      // Token: 0x040020AE RID: 8366
      private byte[] decoded;
    }

    // Token: 0x02000584 RID: 1412
    [Nullable(0)]
    private sealed class FontDecoder
    {
      // Token: 0x1700096E RID: 2414
      // (get) Token: 0x06003587 RID: 13703 RVA: 0x00441D18 File Offset: 0x00441D18
      public bool HasMap
      {
        get
        {
          return this.Map.Count > 0;
        }
      }

      // Token: 0x06003588 RID: 13704 RVA: 0x00441D28 File Offset: 0x00441D28
      public string Decode(byte[] bytes)
      {
        StringBuilder stringBuilder = new StringBuilder(bytes.Length);
        if (this.HasMap)
        {
          int i = 0;
          while (i < bytes.Length)
          {
            if (this.CodeBytes == 2 && i + 1 < bytes.Length)
            {
              int num = ((int)bytes[i] << 8) | (int)bytes[i + 1];
              string text;
              if (this.Map.TryGetValue(num, out text))
              {
                stringBuilder.Append(text);
              }
              i += 2;
            }
            else
            {
              string text2;
              stringBuilder.Append(this.Map.TryGetValue((int)bytes[i], out text2) ? text2 : PdfText.PdfDocEncoding(bytes[i]).ToString());
              i++;
            }
          }
          return stringBuilder.ToString();
        }
        if (this.CodeBytes == 2)
        {
          int num2 = 0;
          while (num2 + 1 < bytes.Length)
          {
            stringBuilder.Append((char)(((int)bytes[num2] << 8) | (int)bytes[num2 + 1]));
            num2 += 2;
          }
          return stringBuilder.ToString();
        }
        foreach (byte b in bytes)
        {
          stringBuilder.Append(PdfText.PdfDocEncoding(b));
        }
        return stringBuilder.ToString();
      }

      // Token: 0x040020AF RID: 8367
      public readonly Dictionary<int, string> Map = new Dictionary<int, string>();

      // Token: 0x040020B0 RID: 8368
      public int CodeBytes = 1;
    }
  }
}
