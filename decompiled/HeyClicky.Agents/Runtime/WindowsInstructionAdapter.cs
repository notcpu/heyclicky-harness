using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000028 RID: 40
  [NullableContext(1)]
  [Nullable(0)]
  internal static class WindowsInstructionAdapter
  {
    // Token: 0x06000416 RID: 1046 RVA: 0x000EA630 File Offset: 0x000EA630
    public static string Adapt(string macBrief)
    {
      string text = macBrief.Replace("\r\n", "\n");
      foreach (ValueTuple<string, string> valueTuple in WindowsInstructionAdapter.Replacements)
      {
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        text = text.Replace(item, item2, StringComparison.Ordinal);
      }
      text = text.Replace("their Mac", "their PC", StringComparison.Ordinal).Replace("this Mac", "this PC", StringComparison.Ordinal);
      int num = text.IndexOf("\n\n", StringComparison.Ordinal);
      if (num >= 0)
      {
        return text.Insert(num, "\n" + "\nThis PC runs Windows: your shell is PowerShell, paths use backslashes (C:\\Users\\...), and `~` is $env:USERPROFILE.\n".TrimEnd('\n'));
      }
      return text + "\nThis PC runs Windows: your shell is PowerShell, paths use backslashes (C:\\Users\\...), and `~` is $env:USERPROFILE.\n";
    }

    // Token: 0x06000417 RID: 1047 RVA: 0x000EA6E0 File Offset: 0x000EA6E0
    public static string AdaptPromptText(string text)
    {
      return text.Replace("while the Mac is awake", "while the PC is awake", StringComparison.Ordinal).Replace("while the Mac is asleep or off", "while the PC is asleep or off", StringComparison.Ordinal).Replace("the Mac", "the PC", StringComparison.Ordinal);
    }

    // Token: 0x04000184 RID: 388
    [TupleElementNames(new string[] { "Mac", "Windows" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<string, string>[] Replacements = new ValueTuple<string, string>[]
    {
      new ValueTuple<string, string>("You're a helper HeyClicky started on this person's Mac", "You're a helper HeyClicky started on this person's Windows PC"),
      new ValueTuple<string, string>("- To find a file of theirs, `mdfind -name invoice` (or `mdfind 'kMDItemTextContent == \"*lease*\"cd'`) searches the whole Mac without opening folders, and `rg` searches inside a folder you know. Don't walk Desktop, Documents or Downloads hunting for something (each asks macOS for permission the first time); when the ask is about one of those folders itself (tidy my Downloads, what's on my Desktop), work in it.", "- To find a file of theirs in a folder you know, `Get-ChildItem <folder> -Recurse -Filter '*invoice*'` finds it by name and `Select-String` searches inside files. Don't walk Desktop, Documents or Downloads hunting for something (they're private); ask where it is instead. When the ask is about one of those folders itself (tidy my Downloads, what's on my Desktop), work in it."),
      new ValueTuple<string, string>("- Never script clicks or keystrokes in their apps (System Events, `cliclick`) or drive them with `open`; an app's own scripting commands (AppleScript, Shortcuts) are fine.", "- Never script clicks or keystrokes in their apps (SendKeys, UI Automation) or drive them with `Start-Process`; an app's own command line or scripting interface is fine."),
      new ValueTuple<string, string>("(Python: `uv pip install --target tmp/pylib <package>`, then run with `PYTHONPATH=tmp/pylib`)", "(Python: `uv pip install --target tmp\\pylib <package>`, then run with `$env:PYTHONPATH = \"tmp\\pylib\"`)")
    };

    // Token: 0x04000185 RID: 389
    private const string ShellNote = "\nThis PC runs Windows: your shell is PowerShell, paths use backslashes (C:\\Users\\...), and `~` is $env:USERPROFILE.\n";
  }
}
