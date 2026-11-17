using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Turns
{
  // Token: 0x0200001C RID: 28
  [NullableContext(1)]
  [Nullable(0)]
  public static class CitationMarkers
  {
    // Token: 0x06000383 RID: 899 RVA: 0x000E5C74 File Offset: 0x000E5C74
    public static string StrippingBareCitationTokens(string text)
    {
      if (text.Contains("](turn", StringComparison.Ordinal))
      {
        text = CitationMarkers.CitationLink.Replace(text, "$1");
      }
      if (!text.Contains("turn", StringComparison.Ordinal) || !text.Contains("cite", StringComparison.Ordinal))
      {
        return text;
      }
      return CitationMarkers.BareCitation.Replace(text, "");
    }

    // Token: 0x06000384 RID: 900 RVA: 0x000E5CD0 File Offset: 0x000E5CD0
    public static string Strip(string text)
    {
      return CitationMarkers.StrippingBareCitationTokens(CitationMarkers.StripMarkers(text));
    }

    // Token: 0x06000385 RID: 901 RVA: 0x000E5CE0 File Offset: 0x000E5CE0
    private static string StripMarkers(string text)
    {
      Func<char, bool> func;
      if ((func = CitationMarkers.<>O.<0>__IsMarker) == null)
      {
        func = (CitationMarkers.<>O.<0>__IsMarker = new Func<char, bool>(CitationMarkers.IsMarker));
      }
      if (!text.Any<char>(func))
      {
        return text;
      }
      StringBuilder stringBuilder = new StringBuilder(text.Length);
      for (int i = 0; i < text.Length; i++)
      {
        char c = text[i];
        if (c != '\ue200')
        {
          if (!CitationMarkers.IsMarker(c))
          {
            stringBuilder.Append(c);
          }
        }
        else
        {
          int num = text.IndexOf('\ue201', i + 1);
          if (num < 0)
          {
            break;
          }
          int num2 = i + 1;
          string text2 = CitationMarkers.EntityName(text.Substring(num2, num - num2));
          if (text2 != null)
          {
            stringBuilder.Append(text2);
          }
          else
          {
            while (stringBuilder.Length > 0)
            {
              StringBuilder stringBuilder2 = stringBuilder;
              if (stringBuilder2[stringBuilder2.Length - 1] != ' ')
              {
                break;
              }
              StringBuilder stringBuilder3 = stringBuilder;
              num2 = stringBuilder3.Length;
              stringBuilder3.Length = num2 - 1;
            }
          }
          i = num;
        }
      }
      return stringBuilder.ToString();
    }

    // Token: 0x06000386 RID: 902 RVA: 0x000E5DC8 File Offset: 0x000E5DC8
    private static bool IsMarker(char c)
    {
      return c >= '\ue200' && c <= '\ue2ff';
    }

    // Token: 0x06000387 RID: 903 RVA: 0x000E5DE0 File Offset: 0x000E5DE0
    [return: Nullable(2)]
    private static string EntityName(string body)
    {
      int num = body.IndexOf('\ue202');
      if (num < 0 || body.Substring(0, num) != "entity")
      {
        return null;
      }
      try
      {
        string text = body;
        int num2 = num + 1;
        JsonArray jsonArray = JsonNode.Parse(text.Substring(num2, text.Length - num2), null, default(JsonDocumentOptions)) as JsonArray;
        if (jsonArray != null && jsonArray.Count >= 2)
        {
          JsonNode jsonNode = jsonArray[1];
          string text2 = ((jsonNode != null) ? jsonNode.ToString() : null);
          if (text2 != null && text2.Trim().Length > 0)
          {
            return text2;
          }
        }
      }
      catch (JsonException)
      {
      }
      return null;
    }

    // Token: 0x04000149 RID: 329
    private const char Open = '\ue200';

    // Token: 0x0400014A RID: 330
    private const char Close = '\ue201';

    // Token: 0x0400014B RID: 331
    private const char Separator = '\ue202';

    // Token: 0x0400014C RID: 332
    private static readonly Regex BareCitation = new Regex("\\s*(?:file)?cite(?:\\s*turn\\d+(?:search|news|view|fetch|open|image|file)\\d+)+", RegexOptions.CultureInvariant);

    // Token: 0x0400014D RID: 333
    private static readonly Regex CitationLink = new Regex("\\[([^\\]\\n]+)\\]\\(turn\\d+(?:search|news|view|fetch|open|image|file)\\d+\\)", RegexOptions.CultureInvariant);

    // Token: 0x020000FA RID: 250
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000589 RID: 1417
      [Nullable(0)]
      public static Func<char, bool> <0>__IsMarker;
    }
  }
}
