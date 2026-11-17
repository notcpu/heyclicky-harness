using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000A8 RID: 168
  public static class AgentFailureReason
  {
    // Token: 0x06000A56 RID: 2646 RVA: 0x003744F4 File Offset: 0x003744F4
    [NullableContext(1)]
    public static string Key(string summary)
    {
      string text = summary.ToLowerInvariant();
      foreach (ValueTuple<Regex, string> valueTuple in AgentFailureReason.Replacements)
      {
        Regex item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        text = item.Replace(text, item2);
      }
      text = text.Trim();
      if (text.Length > 120)
      {
        text = text.Substring(0, 120).TrimEnd();
      }
      if (text.Length != 0)
      {
        return text;
      }
      return "no_reason";
    }

    // Token: 0x040004A8 RID: 1192
    private const int MaximumKeyLength = 120;

    // Token: 0x040004A9 RID: 1193
    [TupleElementNames(new string[] { "Pattern", "Replacement" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<Regex, string>[] Replacements = new ValueTuple<Regex, string>[]
    {
      new ValueTuple<Regex, string>(new Regex("[a-z][a-z0-9+.-]*://\\S+", RegexOptions.CultureInvariant), "<url>"),
      new ValueTuple<Regex, string>(new Regex("[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]+", RegexOptions.CultureInvariant), "<email>"),
      new ValueTuple<Regex, string>(new Regex("[a-z]:\\\\\\S+", RegexOptions.CultureInvariant), "<path>"),
      new ValueTuple<Regex, string>(new Regex("(?:/[^\\s/'\"`,:;()]+){2,}/?", RegexOptions.CultureInvariant), "<path>"),
      new ValueTuple<Regex, string>(new Regex("\\b(?=[a-z0-9_-]*\\d)[a-z0-9_-]{16,}\\b", RegexOptions.CultureInvariant), "<id>"),
      new ValueTuple<Regex, string>(new Regex("\\b(?=[0-9a-f]*\\d)[0-9a-f]{8,}\\b", RegexOptions.CultureInvariant), "<id>"),
      new ValueTuple<Regex, string>(new Regex("\\d+", RegexOptions.CultureInvariant), "#"),
      new ValueTuple<Regex, string>(new Regex("[{}\\[\\]\"\\\\\\s]+", RegexOptions.CultureInvariant), " ")
    };
  }
}
