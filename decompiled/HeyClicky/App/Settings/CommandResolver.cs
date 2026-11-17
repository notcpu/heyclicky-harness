using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CC RID: 204
  [NullableContext(1)]
  [Nullable(0)]
  public static class CommandResolver
  {
    // Token: 0x060008E0 RID: 2272 RVA: 0x00541B6C File Offset: 0x00541B6C
    public unsafe static string SearchPath()
    {
      CommandResolver.<>c__DisplayClass0_0 CS$<>8__locals1;
      CS$<>8__locals1.entries = new List<string>();
      CommandResolver.<SearchPath>g__Add|0_0(Environment.GetEnvironmentVariable("PATH"), ref CS$<>8__locals1);
      CommandResolver.<SearchPath>g__Add|0_0(Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User), ref CS$<>8__locals1);
      CommandResolver.<SearchPath>g__Add|0_0(Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine), ref CS$<>8__locals1);
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      string folderPath3 = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      char c = ';';
      <>y__InlineArray7<string> <>y__InlineArray = default(<>y__InlineArray7<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 0) = Path.Combine(folderPath, "npm");
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 1) = Path.Combine(folderPath2, "Programs", "nodejs");
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 2) = "C:\\Program Files\\nodejs";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 3) = Path.Combine(folderPath3, ".local", "bin");
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 4) = Path.Combine(folderPath3, ".cargo", "bin");
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 5) = Path.Combine(folderPath3, ".bun", "bin");
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<string>, string>(ref <>y__InlineArray, 6) = Path.Combine(folderPath2, "Volta", "bin");
      CommandResolver.<SearchPath>g__Add|0_0(string.Join(c, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray7<string>, string>(in <>y__InlineArray, 7)), ref CS$<>8__locals1);
      return string.Join<string>(';', CS$<>8__locals1.entries);
    }

    // Token: 0x060008E1 RID: 2273 RVA: 0x00541C9C File Offset: 0x00541C9C
    public static string Resolve(string command)
    {
      string text = command.Trim();
      if (text.Length == 0)
      {
        return text;
      }
      if (text.StartsWith("~\\", StringComparison.Ordinal) || text.StartsWith("~/", StringComparison.Ordinal))
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string text2 = text;
        text = Path.Combine(folderPath, text2.Substring(2, text2.Length - 2));
      }
      if (Path.IsPathFullyQualified(text))
      {
        string text3;
        if (!File.Exists(text))
        {
          if ((text3 = CommandResolver.WithExtension(text)) == null)
          {
            return text;
          }
        }
        else
        {
          text3 = text;
        }
        return text3;
      }
      if (text.Contains('\\') || text.Contains('/'))
      {
        return text;
      }
      foreach (string text4 in CommandResolver.SearchPath().Split(';', StringSplitOptions.None))
      {
        try
        {
          string text5 = CommandResolver.WithExtension(Path.Combine(text4, text));
          if (text5 != null)
          {
            return text5;
          }
        }
        catch (ArgumentException)
        {
        }
      }
      return text;
    }

    // Token: 0x060008E2 RID: 2274 RVA: 0x00541D7C File Offset: 0x00541D7C
    [return: Nullable(2)]
    private static string WithExtension(string candidate)
    {
      if (Path.HasExtension(candidate) && File.Exists(candidate))
      {
        return candidate;
      }
      foreach (string text in (Environment.GetEnvironmentVariable("PATHEXT") ?? ".COM;.EXE;.BAT;.CMD").Split(';', StringSplitOptions.RemoveEmptyEntries))
      {
        if (File.Exists(candidate + text.ToLowerInvariant()))
        {
          return candidate + text.ToLowerInvariant();
        }
      }
      return null;
    }

    // Token: 0x060008E3 RID: 2275 RVA: 0x00541DEC File Offset: 0x00541DEC
    public static bool IsResolvable(string command)
    {
      string text = CommandResolver.Resolve(command);
      return Path.IsPathFullyQualified(text) && File.Exists(text);
    }

    // Token: 0x060008E4 RID: 2276 RVA: 0x00541E10 File Offset: 0x00541E10
    [NullableContext(2)]
    [CompilerGenerated]
    internal static void <SearchPath>g__Add|0_0(string value, ref CommandResolver.<>c__DisplayClass0_0 A_1)
    {
      foreach (string text in (value ?? "").Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
      {
        if (!A_1.entries.Contains(text, StringComparer.OrdinalIgnoreCase))
        {
          A_1.entries.Add(text);
        }
      }
    }
  }
}
