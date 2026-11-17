using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D6 RID: 214
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyFoundingMarker
  {
    // Token: 0x06000DB9 RID: 3513 RVA: 0x003832D4 File Offset: 0x003832D4
    public static string GreetingLine(string clickyName)
    {
      return "Hi there, I'm " + clickyName + ". I'm very happy you summoned me into your world <3";
    }

    // Token: 0x06000DBA RID: 3514 RVA: 0x003832E8 File Offset: 0x003832E8
    public unsafe static string Compose(string clickyName, string firstMessage, string body)
    {
      string text = clickyName.Replace("\n", " ").Replace("\"", "'");
      string text2 = string.Join(" ", from l in firstMessage.Split('\n', StringSplitOptions.None)
        select l.Trim() into l
        where l.Length > 0
        select l);
      string text3 = "\n";
      <>y__InlineArray4<string> <>y__InlineArray = default(<>y__InlineArray4<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 0) = "[New Clicky \"" + text + "\" founded]";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 1) = text2;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 2) = "";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<string>, string>(ref <>y__InlineArray, 3) = body;
      return string.Join(text3, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<string>, string>(in <>y__InlineArray, 4));
    }

    // Token: 0x06000DBB RID: 3515 RVA: 0x003833C8 File Offset: 0x003833C8
    [return: TupleElementNames(new string[] { "Name", "FirstMessage" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string>? Parse(string message)
    {
      string[] array = message.Trim().Split('\n', StringSplitOptions.None);
      if (!array[0].StartsWith("[New Clicky \"", StringComparison.Ordinal) || !array[0].EndsWith("\" founded]", StringComparison.Ordinal))
      {
        return null;
      }
      string text = array[0];
      int length = "[New Clicky \"".Length;
      int length2 = "\" founded]".Length;
      return new ValueTuple<string, string>?(new ValueTuple<string, string>(text.Substring(length, text.Length - length2 - length), string.Join(" ", (from l in array.Skip<string>(1)
        select l.Trim()).TakeWhile<string>((string l) => l.Length > 0))));
    }

    // Token: 0x06000DBC RID: 3516 RVA: 0x003834A0 File Offset: 0x003834A0
    public static string DisplayText(string message)
    {
      ValueTuple<string, string>? valueTuple = SuggestionApprovalMarker.Parse(message);
      if (valueTuple == null)
      {
        ValueTuple<string, string>? valueTuple2 = ClickyFoundingMarker.Parse(message);
        if (valueTuple2 != null)
        {
          ValueTuple<string, string> valueOrDefault = valueTuple2.GetValueOrDefault();
          if (valueOrDefault.Item2.Length > 0)
          {
            return valueOrDefault.Item2;
          }
        }
        return message;
      }
      ValueTuple<string, string> valueOrDefault2 = valueTuple.GetValueOrDefault();
      if (valueOrDefault2.Item2.Length <= 0)
      {
        return valueOrDefault2.Item1;
      }
      return valueOrDefault2.Item2;
    }

    // Token: 0x040005C7 RID: 1479
    private const string HeaderPrefix = "[New Clicky \"";

    // Token: 0x040005C8 RID: 1480
    private const string HeaderSuffix = "\" founded]";
  }
}
