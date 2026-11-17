using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Documents
{
  // Token: 0x02000199 RID: 409
  [NullableContext(1)]
  [Nullable(0)]
  public static class DocumentText
  {
    // Token: 0x06001822 RID: 6178 RVA: 0x003C75B8 File Offset: 0x003C75B8
    public static bool LikelyYieldsText(string path)
    {
      string text = Path.GetExtension(path).TrimStart('.');
      return text.Equals("pdf", StringComparison.OrdinalIgnoreCase) || DocumentText.SystemDocumentExtensions.Contains(text) || DocumentText.TextExtensions.Contains(text);
    }

    // Token: 0x06001823 RID: 6179 RVA: 0x003C75FC File Offset: 0x003C75FC
    public static PageContext Extract(string path, int maximumCharacters = 60000)
    {
      string text = Path.GetExtension(path).TrimStart('.').ToLowerInvariant();
      string text2 = ((text == "pdf") ? "pdf" : "file");
      try
      {
        using (new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
        {
        }
      }
      catch (UnauthorizedAccessException)
      {
        return new PageContext(text2, path, null, false, null, true);
      }
      catch (Exception)
      {
        return new PageContext(text2, path, null, false, null, false);
      }
      try
      {
        if (text == "pdf")
        {
          PdfText.Result result = PdfText.Extract(path, maximumCharacters);
          ValueTuple<string, bool> valueTuple = DocumentText.Cap(result.Text, maximumCharacters);
          string item = valueTuple.Item1;
          bool item2 = valueTuple.Item2;
          return new PageContext("pdf", path, item, item2 || result.StoppedEarly, result.PageCount, false);
        }
        if (DocumentText.SystemDocumentExtensions.Contains(text))
        {
          string text3 = DocumentText.ReadSystemDocument(path, text);
          if (text3 != null)
          {
            ValueTuple<string, bool> valueTuple2 = DocumentText.Cap(text3, maximumCharacters);
            string item3 = valueTuple2.Item1;
            bool item4 = valueTuple2.Item2;
            if (item3 != null)
            {
              return new PageContext("text_file", path, item3, item4, null, false);
            }
          }
        }
        if (DocumentText.TextExtensions.Contains(text) || (text.Length == 0 && DocumentText.LooksLikeText(path)))
        {
          long length = new FileInfo(path).Length;
          string text4 = DocumentText.Decode(DocumentText.ReadLeading(path, 2000000));
          if (text4 != null)
          {
            ValueTuple<string, bool> valueTuple3 = DocumentText.Cap(text4, maximumCharacters);
            string item5 = valueTuple3.Item1;
            bool item6 = valueTuple3.Item2;
            if (item5 != null)
            {
              return new PageContext("text_file", path, item5, item6 || length > 2000000L, null, false);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("DocumentText", Path.GetFileName(path) + ": " + ex.Message);
      }
      return new PageContext(text2, path, null, false, null, false);
    }

    // Token: 0x06001824 RID: 6180 RVA: 0x003C7860 File Offset: 0x003C7860
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Text", "Cut" })]
    [return: Nullable(new byte[] { 0, 2 })]
    internal static ValueTuple<string, bool> Cap(string raw, int maximum)
    {
      string text = ((raw != null) ? raw.Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        return new ValueTuple<string, bool>(null, false);
      }
      if (text.Length <= maximum)
      {
        return new ValueTuple<string, bool>(text, false);
      }
      return new ValueTuple<string, bool>(text.Substring(0, maximum), true);
    }

    // Token: 0x06001825 RID: 6181 RVA: 0x003C78AC File Offset: 0x003C78AC
    private static byte[] ReadLeading(string path, int maximum)
    {
      byte[] array2;
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
      {
        int num = (int)Math.Min((long)maximum, fileStream.Length);
        byte[] array = new byte[num];
        int i;
        int num2;
        for (i = 0; i < num; i += num2)
        {
          num2 = fileStream.Read(array, i, num - i);
          if (num2 <= 0)
          {
            break;
          }
        }
        array2 = ((i == num) ? array : RuntimeHelpers.GetSubArray<byte>(array, Range.EndAt(i)));
      }
      return array2;
    }

    // Token: 0x06001826 RID: 6182 RVA: 0x003C7930 File Offset: 0x003C7930
    private static bool LooksLikeText(string path)
    {
      byte[] array = DocumentText.ReadLeading(path, 4096);
      return array.Length != 0 && Array.IndexOf<byte>(array, 0) < 0;
    }

    // Token: 0x06001827 RID: 6183 RVA: 0x003C795C File Offset: 0x003C795C
    [return: Nullable(2)]
    internal static string Decode(byte[] bytes)
    {
      if (bytes.Length >= 3 && bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
      {
        return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
      }
      if (bytes.Length >= 2 && bytes[0] == 255 && bytes[1] == 254)
      {
        return Encoding.Unicode.GetString(bytes, 2, (bytes.Length - 2) & -2);
      }
      if (bytes.Length >= 2 && bytes[0] == 254 && bytes[1] == 255)
      {
        return Encoding.BigEndianUnicode.GetString(bytes, 2, (bytes.Length - 2) & -2);
      }
      UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
      int num = 0;
      while (num <= 3 && num < bytes.Length)
      {
        try
        {
          return utf8Encoding.GetString(bytes, 0, bytes.Length - num);
        }
        catch (DecoderFallbackException)
        {
        }
        num++;
      }
      if (Array.IndexOf<byte>(bytes, 0) >= 0)
      {
        return null;
      }
      return Encoding.Latin1.GetString(bytes);
    }

    // Token: 0x06001828 RID: 6184 RVA: 0x003C7A54 File Offset: 0x003C7A54
    [return: Nullable(2)]
    private static string ReadSystemDocument(string path, string extension)
    {
      if (extension == "rtf")
      {
        return DocumentText.RtfToText(DocumentText.Decode(DocumentText.ReadLeading(path, 16000000)) ?? "");
      }
      if (new FileInfo(path).Length > 67108864L)
      {
        return null;
      }
      string text;
      using (ZipArchive zipArchive = ZipFile.OpenRead(path))
      {
        if (!(extension == "docx") && !(extension == "docm") && !(extension == "dotx"))
        {
          if (!(extension == "odt"))
          {
            if (!(extension == "pptx"))
            {
              if (!(extension == "xlsx"))
              {
                text = null;
              }
              else
              {
                ZipArchiveEntry entry = zipArchive.GetEntry("xl/sharedStrings.xml");
                if (entry == null)
                {
                  text = null;
                }
                else
                {
                  text = string.Join("\n", from m in DocumentText.SharedStringItem().Matches(DocumentText.ReadEntry(entry))
                    select DocumentText.XmlUnescape(string.Concat(from t in DocumentText.PlainTextRun().Matches(m.Value)
                      select t.Groups[1].Value)));
                }
              }
            }
            else
            {
              List<ZipArchiveEntry> list = (from e in zipArchive.Entries
                where DocumentText.SlidePath().IsMatch(e.FullName)
                orderby int.Parse(DocumentText.SlidePath().Match(e.FullName).Groups[1].Value)
                select e).ToList<ZipArchiveEntry>();
              if (list.Count == 0)
              {
                text = null;
              }
              else
              {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (ZipArchiveEntry zipArchiveEntry in list)
                {
                  IEnumerable<string> enumerable = from m in DocumentText.DrawingTextRun().Matches(DocumentText.ReadEntry(zipArchiveEntry))
                    select DocumentText.XmlUnescape(m.Groups[1].Value);
                  stringBuilder.AppendLine(string.Join(" ", enumerable));
                }
                text = stringBuilder.ToString();
              }
            }
          }
          else
          {
            ZipArchiveEntry entry2 = zipArchive.GetEntry("content.xml");
            text = ((entry2 != null) ? DocumentText.OpenDocumentXmlToText(DocumentText.ReadEntry(entry2)) : null);
          }
        }
        else
        {
          ZipArchiveEntry entry3 = zipArchive.GetEntry("word/document.xml");
          text = ((entry3 != null) ? DocumentText.WordXmlToText(DocumentText.ReadEntry(entry3)) : null);
        }
      }
      return text;
    }

    // Token: 0x06001829 RID: 6185 RVA: 0x003C7CD0 File Offset: 0x003C7CD0
    private static string ReadEntry(ZipArchiveEntry entry)
    {
      string text;
      using (Stream stream = entry.Open())
      {
        using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
        {
          text = streamReader.ReadToEnd();
        }
      }
      return text;
    }

    // Token: 0x0600182A RID: 6186 RVA: 0x003C7D2C File Offset: 0x003C7D2C
    internal static string WordXmlToText(string xml)
    {
      string text = DocumentText.WordParagraphEnd().Replace(xml, "\n");
      text = DocumentText.WordTab().Replace(text, "\t");
      text = DocumentText.WordBreak().Replace(text, "\n");
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object obj in DocumentText.WordTextOrBreak().Matches(text))
      {
        Match match = (Match)obj;
        stringBuilder.Append(match.Groups[1].Success ? DocumentText.XmlUnescape(match.Groups[1].Value) : match.Value);
      }
      return stringBuilder.ToString();
    }

    // Token: 0x0600182B RID: 6187 RVA: 0x003C7E00 File Offset: 0x003C7E00
    internal static string OpenDocumentXmlToText(string xml)
    {
      string text = DocumentText.OdtBlockEnd().Replace(xml, "\n");
      text = DocumentText.OdtTab().Replace(text, "\t");
      text = DocumentText.AnyTag().Replace(text, "");
      return DocumentText.XmlUnescape(text);
    }

    // Token: 0x0600182C RID: 6188 RVA: 0x003C7E48 File Offset: 0x003C7E48
    internal static string RtfToText(string rtf)
    {
      StringBuilder stringBuilder = new StringBuilder(rtf.Length / 2);
      Stack<bool> stack = new Stack<bool>();
      bool flag = false;
      int num = 1;
      int num2 = 0;
      for (int i = 0; i < rtf.Length; i++)
      {
        char c = rtf[i];
        if (c == '{')
        {
          stack.Push(flag);
        }
        else if (c == '}')
        {
          flag = stack.Count > 0 && stack.Pop();
        }
        else if (c == '\\')
        {
          if (i + 1 >= rtf.Length)
          {
            break;
          }
          char c2 = rtf[i + 1];
          bool flag2 = c2 == '\\' || c2 == '{' || c2 == '}';
          if (flag2)
          {
            if (!flag)
            {
              stringBuilder.Append(c2);
            }
            i++;
          }
          else if (c2 == '\'')
          {
            int num3;
            if (i + 3 < rtf.Length && int.TryParse(rtf.AsSpan(i + 2, 2), NumberStyles.HexNumber, null, out num3))
            {
              if (num2 > 0)
              {
                num2--;
              }
              else if (!flag)
              {
                StringBuilder stringBuilder2 = stringBuilder;
                Encoding latin = Encoding.Latin1;
                byte b = (byte)num3;
                stringBuilder2.Append(latin.GetString(new ReadOnlySpan<byte>(ref b)));
              }
            }
            i += 3;
          }
          else if (c2 == '*')
          {
            flag = true;
            i++;
          }
          else
          {
            flag2 = c2 == '\n' || c2 == '\r';
            if (flag2)
            {
              if (!flag)
              {
                stringBuilder.Append('\n');
              }
              i++;
            }
            else
            {
              int num4 = i + 1;
              while (num4 < rtf.Length && char.IsAsciiLetter(rtf[num4]))
              {
                num4++;
              }
              int num5 = i + 1;
              string text = rtf.Substring(num5, num4 - num5);
              int num6 = num4;
              if (num6 < rtf.Length && (rtf[num6] == '-' || char.IsAsciiDigit(rtf[num6])))
              {
                num6++;
                while (num6 < rtf.Length && char.IsAsciiDigit(rtf[num6]))
                {
                  num6++;
                }
              }
              num5 = num4;
              string text2 = rtf.Substring(num5, num6 - num5);
              if (num6 < rtf.Length && rtf[num6] == ' ')
              {
                num6++;
              }
              i = num6 - 1;
              if (text != null)
              {
                switch (text.Length)
                {
                case 1:
                {
                  if (!(text == "u"))
                  {
                    goto IL_0613;
                  }
                  int num7;
                  if (int.TryParse(text2, out num7) && !flag)
                  {
                    stringBuilder.Append((char)((num7 < 0) ? (num7 + 65536) : num7));
                  }
                  num2 = num;
                  goto IL_0613;
                }
                case 2:
                {
                  if (!(text == "uc"))
                  {
                    goto IL_0613;
                  }
                  int num8;
                  if (int.TryParse(text2, out num8))
                  {
                    num = num8;
                    goto IL_0613;
                  }
                  goto IL_0613;
                }
                case 3:
                  switch (text[0])
                  {
                  case 'p':
                    if (!(text == "par"))
                    {
                      goto IL_0613;
                    }
                    break;
                  case 'q':
                  case 's':
                    goto IL_0613;
                  case 'r':
                    if (!(text == "row"))
                    {
                      goto IL_0613;
                    }
                    break;
                  case 't':
                    if (!(text == "tab"))
                    {
                      goto IL_0613;
                    }
                    goto IL_0594;
                  default:
                    goto IL_0613;
                  }
                  break;
                case 4:
                {
                  char c3 = text[0];
                  if (c3 <= 'i')
                  {
                    if (c3 != 'c')
                    {
                      if (c3 != 'i')
                      {
                        goto IL_0613;
                      }
                      if (!(text == "info"))
                      {
                        goto IL_0613;
                      }
                      goto IL_05DE;
                    }
                    else
                    {
                      if (!(text == "cell"))
                      {
                        goto IL_0613;
                      }
                      goto IL_0594;
                    }
                  }
                  else if (c3 != 'l')
                  {
                    if (c3 != 'p')
                    {
                      goto IL_0613;
                    }
                    if (!(text == "pict"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  else if (!(text == "line"))
                  {
                    goto IL_0613;
                  }
                  break;
                }
                case 5:
                case 11:
                case 13:
                case 14:
                case 15:
                case 16:
                  goto IL_0613;
                case 6:
                {
                  char c3 = text[0];
                  if (c3 != 'f')
                  {
                    if (c3 != 'h')
                    {
                      goto IL_0613;
                    }
                    if (!(text == "header"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  else
                  {
                    if (!(text == "footer"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  break;
                }
                case 7:
                {
                  char c3 = text[0];
                  if (c3 != 'f')
                  {
                    if (c3 != 'r')
                    {
                      goto IL_0613;
                    }
                    if (!(text == "rsidtbl"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  else
                  {
                    if (!(text == "fonttbl"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  break;
                }
                case 8:
                {
                  char c3 = text[0];
                  if (c3 != 'c')
                  {
                    if (c3 != 'x')
                    {
                      goto IL_0613;
                    }
                    if (!(text == "xmlnstbl"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  else
                  {
                    if (!(text == "colortbl"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  break;
                }
                case 9:
                {
                  char c3 = text[0];
                  if (c3 <= 'g')
                  {
                    if (c3 != 'd')
                    {
                      if (c3 != 'g')
                      {
                        goto IL_0613;
                      }
                      if (!(text == "generator"))
                      {
                        goto IL_0613;
                      }
                      goto IL_05DE;
                    }
                    else
                    {
                      if (!(text == "datastore"))
                      {
                        goto IL_0613;
                      }
                      goto IL_05DE;
                    }
                  }
                  else if (c3 != 'l')
                  {
                    if (c3 != 't')
                    {
                      goto IL_0613;
                    }
                    if (!(text == "themedata"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  else
                  {
                    if (!(text == "listtable"))
                    {
                      goto IL_0613;
                    }
                    goto IL_05DE;
                  }
                  break;
                }
                case 10:
                  if (!(text == "stylesheet"))
                  {
                    goto IL_0613;
                  }
                  goto IL_05DE;
                case 12:
                  if (!(text == "latentstyles"))
                  {
                    goto IL_0613;
                  }
                  goto IL_05DE;
                case 17:
                  if (!(text == "listoverridetable"))
                  {
                    goto IL_0613;
                  }
                  goto IL_05DE;
                case 18:
                  if (!(text == "colorschememapping"))
                  {
                    goto IL_0613;
                  }
                  goto IL_05DE;
                default:
                  goto IL_0613;
                }
                if (!flag)
                {
                  stringBuilder.Append('\n');
                  goto IL_0613;
                }
                goto IL_0613;
                IL_0594:
                if (!flag)
                {
                  stringBuilder.Append('\t');
                  goto IL_0613;
                }
                goto IL_0613;
                IL_05DE:
                flag = true;
              }
            }
          }
        }
        else if (c != '\n' && c != '\r')
        {
          if (num2 > 0)
          {
            num2--;
          }
          else if (!flag)
          {
            stringBuilder.Append(c);
          }
        }
        IL_0613:;
      }
      return stringBuilder.ToString();
    }

    // Token: 0x0600182D RID: 6189 RVA: 0x003C8484 File Offset: 0x003C8484
    public static string TextWithoutMarkup(string html)
    {
      string text = DocumentText.ScriptBlock().Replace(html, " ");
      text = DocumentText.StyleBlock().Replace(text, " ");
      text = DocumentText.AnyTag().Replace(text, " ");
      text = text.Replace("&nbsp;", " ").Replace("&amp;", "&").Replace("&lt;", "<")
        .Replace("&gt;", ">")
        .Replace("&quot;", "\"")
        .Replace("&#39;", "'");
      return DocumentText.Whitespace().Replace(text, " ").Trim();
    }

    // Token: 0x0600182E RID: 6190 RVA: 0x003C8538 File Offset: 0x003C8538
    private static string XmlUnescape(string text)
    {
      return WebUtility.HtmlDecode(text);
    }

    // Token: 0x0600182F RID: 6191 RVA: 0x003C8540 File Offset: 0x003C8540
    [GeneratedRegex("</w:p>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex WordParagraphEnd()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordParagraphEnd_4.Instance;
    }

    // Token: 0x06001830 RID: 6192 RVA: 0x003C8548 File Offset: 0x003C8548
    [GeneratedRegex("<w:tab\\s*/>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex WordTab()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTab_5.Instance;
    }

    // Token: 0x06001831 RID: 6193 RVA: 0x003C8550 File Offset: 0x003C8550
    [GeneratedRegex("<w:(br|cr)\\b[^>]*/>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex WordBreak()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordBreak_6.Instance;
    }

    // Token: 0x06001832 RID: 6194 RVA: 0x003C8558 File Offset: 0x003C8558
    [GeneratedRegex("<w:t(?:\\s[^>]*)?>([^<]*)</w:t>|\\n|\\t")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex WordTextOrBreak()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__WordTextOrBreak_7.Instance;
    }

    // Token: 0x06001833 RID: 6195 RVA: 0x003C8560 File Offset: 0x003C8560
    [GeneratedRegex("</text:(p|h)>|<text:line-break\\s*/>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex OdtBlockEnd()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtBlockEnd_8.Instance;
    }

    // Token: 0x06001834 RID: 6196 RVA: 0x003C8568 File Offset: 0x003C8568
    [GeneratedRegex("<text:tab\\s*/>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex OdtTab()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9.Instance;
    }

    // Token: 0x06001835 RID: 6197 RVA: 0x003C8570 File Offset: 0x003C8570
    [GeneratedRegex("<[^>]+>", RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex AnyTag()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10.Instance;
    }

    // Token: 0x06001836 RID: 6198 RVA: 0x003C8578 File Offset: 0x003C8578
    [GeneratedRegex("^ppt/slides/slide(\\d+)\\.xml$")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex SlidePath()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SlidePath_11.Instance;
    }

    // Token: 0x06001837 RID: 6199 RVA: 0x003C8580 File Offset: 0x003C8580
    [GeneratedRegex("<a:t>([^<]*)</a:t>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex DrawingTextRun()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DrawingTextRun_12.Instance;
    }

    // Token: 0x06001838 RID: 6200 RVA: 0x003C8588 File Offset: 0x003C8588
    [GeneratedRegex("<si>.*?</si>", RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex SharedStringItem()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__SharedStringItem_13.Instance;
    }

    // Token: 0x06001839 RID: 6201 RVA: 0x003C8590 File Offset: 0x003C8590
    [GeneratedRegex("<t(?:\\s[^>]*)?>([^<]*)</t>")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex PlainTextRun()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__PlainTextRun_14.Instance;
    }

    // Token: 0x0600183A RID: 6202 RVA: 0x003C8598 File Offset: 0x003C8598
    [GeneratedRegex("<script.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex ScriptBlock()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__ScriptBlock_15.Instance;
    }

    // Token: 0x0600183B RID: 6203 RVA: 0x003C85A0 File Offset: 0x003C85A0
    [GeneratedRegex("<style.*?</style>", RegexOptions.IgnoreCase | RegexOptions.Singleline)]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex StyleBlock()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__StyleBlock_16.Instance;
    }

    // Token: 0x0600183C RID: 6204 RVA: 0x003C85A8 File Offset: 0x003C85A8
    [GeneratedRegex("\\s+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex Whitespace()
    {
      return <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Whitespace_17.Instance;
    }

    // Token: 0x04000A60 RID: 2656
    public const int MaximumCharacters = 60000;

    // Token: 0x04000A61 RID: 2657
    private const int MaximumTextFileBytes = 2000000;

    // Token: 0x04000A62 RID: 2658
    private const long MaximumDocumentBytes = 67108864L;

    // Token: 0x04000A63 RID: 2659
    private static readonly HashSet<string> TextExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
      "txt", "text", "md", "markdown", "mdx", "csv", "tsv", "log", "env", "conf",
      "cfg", "ini", "toml", "yaml", "yml", "json", "jsonl", "xml", "html", "htm",
      "xhtml", "svg", "css", "scss", "sass", "less", "js", "mjs", "cjs", "jsx",
      "ts", "tsx", "py", "rb", "php", "java", "kt", "kts", "swift", "m",
      "mm", "c", "h", "cc", "cpp", "cxx", "hpp", "hh", "cs", "fs",
      "vb", "go", "rs", "lua", "pl", "r", "scala", "sql", "sh", "bash",
      "zsh", "fish", "ps1", "psm1", "bat", "cmd", "tex", "bib", "rst", "adoc",
      "org", "srt", "vtt", "properties", "gradle", "dart", "vue", "svelte", "graphql", "proto",
      "dockerfile", "makefile", "gitignore", "editorconfig", "csproj", "sln", "props"
    };

    // Token: 0x04000A64 RID: 2660
    private static readonly HashSet<string> SystemDocumentExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "docx", "docm", "dotx", "odt", "rtf", "pptx", "xlsx" };
  }
}
