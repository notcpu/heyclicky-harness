using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001DF RID: 479
  [NullableContext(1)]
  [Nullable(0)]
  public static class BrowserErrorKinds
  {
    // Token: 0x06001B91 RID: 7057 RVA: 0x003DC6D4 File Offset: 0x003DC6D4
    public static string Kind(string message)
    {
      string text = message.ToLowerInvariant();
      Func<string, bool> <>9__0;
      foreach (ValueTuple<string, string[]> valueTuple in BrowserErrorKinds.Kinds)
      {
        string item = valueTuple.Item1;
        IEnumerable<string> item2 = valueTuple.Item2;
        Func<string, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (string phrase) => text.Contains(phrase, StringComparison.Ordinal));
        }
        if (item2.Any<string>(func))
        {
          return item;
        }
      }
      return "other";
    }

    // Token: 0x06001B92 RID: 7058 RVA: 0x003DC74C File Offset: 0x003DC74C
    [return: Nullable(2)]
    public static string DetachReason(string message)
    {
      int num = message.IndexOf("detached the debugger", StringComparison.Ordinal);
      if (num < 0)
      {
        return null;
      }
      int num2 = message.IndexOf('(', num);
      if (num2 < 0)
      {
        return null;
      }
      int num3 = message.IndexOf(')', num2 + 1);
      if (num3 < 0)
      {
        return null;
      }
      int num4 = num2 + 1;
      string text = message.Substring(num4, num3 - num4);
      if (text.Length != 0 && text.Length <= 40)
      {
        if (text.All<char>((char c) => char.IsLetter(c) || c == '_'))
        {
          return text;
        }
      }
      return null;
    }

    // Token: 0x04000BBD RID: 3005
    [TupleElementNames(new string[] { "Kind", "Phrases" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 1 })]
    private static readonly ValueTuple<string, string[]>[] Kinds = new ValueTuple<string, string[]>[]
    {
      new ValueTuple<string, string[]>("detached", new string[] { "chrome detached the debugger" }),
      new ValueTuple<string, string[]>("tab_closed", new string[] { "was closed" }),
      new ValueTuple<string, string[]>("access_stopped", new string[] { "browser access stopped", "browser access is unavailable", "browser access for this turn has ended", "browser actions timed out" }),
      new ValueTuple<string, string[]>("action_timeout", new string[] { "this browser action took too long" }),
      new ValueTuple<string, string[]>("read_timeout", new string[] { "didn't answer in time", "did not respond in time" }),
      new ValueTuple<string, string[]>("page_loading", new string[] { "still loading", "is navigating" }),
      new ValueTuple<string, string[]>("stale_ref", new string[] { "stale or unknown reference", "reference no longer applies", "element disappeared", "page changed" }),
      new ValueTuple<string, string[]>("not_clickable", new string[] { "not clickable", "is obscured", "element action failed" }),
      new ValueTuple<string, string[]>("dialog_open", new string[] { "a page dialog is open", "no page dialog" }),
      new ValueTuple<string, string[]>("script_threw", new string[] { "the code threw" }),
      new ValueTuple<string, string[]>("not_connected", new string[] { "chrome isn't connected", "browser connection changed" }),
      new ValueTuple<string, string[]>("other_clicky_tab", new string[] { "belongs to another running clicky" }),
      new ValueTuple<string, string[]>("bad_request", new string[] { "needs ", "is required", "unsupported", "must be", "only a tab", "only http" })
    };
  }
}
