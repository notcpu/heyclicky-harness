using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D5 RID: 213
  [NullableContext(1)]
  [Nullable(0)]
  public static class SuggestionApprovalMarker
  {
    // Token: 0x06000DB7 RID: 3511 RVA: 0x0038310C File Offset: 0x0038310C
    public unsafe static string Compose(string title, string description, string agentBrief)
    {
      string text = title.Replace("\n", " ").Replace("\"", "'");
      string text2 = string.Join(" ", from l in description.Split('\n', StringSplitOptions.None)
        select l.Trim() into l
        where l.Length > 0
        select l);
      string text3 = "\n";
      <>y__InlineArray4<string> <>y__InlineArray = default(<>y__InlineArray4<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 0) = "[Suggested task \"" + text + "\" approved]";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 1) = text2;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 2) = "";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 3) = agentBrief;
      return string.Join(text3, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<string>, string>(in <>y__InlineArray, 4));
    }

    // Token: 0x06000DB8 RID: 3512 RVA: 0x003831EC File Offset: 0x003831EC
    [return: TupleElementNames(new string[] { "Title", "Description" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string>? Parse(string message)
    {
      string text = message.Trim();
      if (!text.StartsWith("[Suggested task \"", StringComparison.Ordinal))
      {
        return null;
      }
      string[] array = text.Split('\n', StringSplitOptions.None);
      if (!array[0].EndsWith("\" approved]", StringComparison.Ordinal))
      {
        return null;
      }
      string text2 = array[0];
      int length = "[Suggested task \"".Length;
      int length2 = "\" approved]".Length;
      string text3 = text2.Substring(length, text2.Length - length2 - length);
      string text4 = string.Join(" ", (from l in array.Skip<string>(1)
        select l.Trim()).TakeWhile<string>((string l) => l.Length > 0));
      return new ValueTuple<string, string>?(new ValueTuple<string, string>(text3, text4));
    }

    // Token: 0x040005C5 RID: 1477
    private const string HeaderPrefix = "[Suggested task \"";

    // Token: 0x040005C6 RID: 1478
    private const string HeaderSuffix = "\" approved]";
  }
}
