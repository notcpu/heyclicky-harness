using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004D RID: 77
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyMainArtifacts
  {
    // Token: 0x060006D8 RID: 1752 RVA: 0x000F8564 File Offset: 0x000F8564
    public static bool IsMeaningfulFile(string entry)
    {
      if (entry.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      {
        return true;
      }
      string text = entry.ToLowerInvariant().Replace('\\', '/');
      string fileName = Path.GetFileName(text);
      if (fileName.StartsWith('.') || text.Contains("/tmp/") || text.Contains("/node_modules/") || text.Contains("/dashboard/"))
      {
        return false;
      }
      string text2 = Path.GetExtension(fileName).TrimStart('.');
      if (ClickyMainArtifacts.ScratchExtensions.Contains(text2))
      {
        return false;
      }
      bool flag = text2 == "app" || text2 == "exe";
      if (flag)
      {
        return true;
      }
      ClickyJobKind clickyJobKind = ClickyJobKinds.Infer(new <>z__ReadOnlySingleElementList<string>(entry), fileName, "");
      return clickyJobKind - ClickyJobKind.Code > 1;
    }

    // Token: 0x060006D9 RID: 1753 RVA: 0x000F8630 File Offset: 0x000F8630
    public static ISet<string> SupportingPaths(IEnumerable<string> paths)
    {
      Func<string, bool> func;
      if ((func = ClickyMainArtifacts.<>O.<0>__IsPathFullyQualified) == null)
      {
        func = (ClickyMainArtifacts.<>O.<0>__IsPathFullyQualified = new Func<string, bool>(Path.IsPathFullyQualified));
      }
      IEnumerable<string> enumerable = paths.Where<string>(func);
      Func<string, string> func2;
      if ((func2 = ClickyMainArtifacts.<>O.<1>__Standardized) == null)
      {
        func2 = (ClickyMainArtifacts.<>O.<1>__Standardized = new Func<string, string>(ClickyMainArtifacts.Standardized));
      }
      HashSet<string> hashSet = enumerable.Select<string, string>(func2).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
      HashSet<string> supporting = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
      IEnumerable<string> enumerable2 = hashSet;
      Func<string, bool> func3;
      if ((func3 = ClickyMainArtifacts.<>O.<2>__IsReferencingPage) == null)
      {
        func3 = (ClickyMainArtifacts.<>O.<2>__IsReferencingPage = new Func<string, bool>(ClickyMainArtifacts.IsReferencingPage));
      }
      foreach (string text in enumerable2.Where<string>(func3))
      {
        HashSet<string> hashSet2 = ClickyMainArtifacts.References(text);
        hashSet2.IntersectWith(hashSet);
        hashSet2.Remove(text);
        dictionary[text] = hashSet2;
      }
      foreach (KeyValuePair<string, HashSet<string>> keyValuePair in dictionary)
      {
        string text2;
        HashSet<string> hashSet3;
        keyValuePair.Deconstruct(out text2, out hashSet3);
        string text3 = text2;
        foreach (string text4 in hashSet3)
        {
          HashSet<string> hashSet4;
          if (!dictionary.TryGetValue(text4, out hashSet4) || !hashSet4.Contains(text3) || Path.GetFileName(text3).Equals("index.html", StringComparison.OrdinalIgnoreCase))
          {
            supporting.Add(text4);
          }
        }
      }
      IEnumerable<string> enumerable3 = hashSet;
      Func<string, bool> <>9__0;
      Func<string, bool> func4;
      if ((func4 = <>9__0) == null)
      {
        func4 = (<>9__0 = (string p) => !supporting.Contains(p));
      }
      foreach (string text5 in enumerable3.Where<string>(func4))
      {
        string text6 = Path.GetFileName(text5).ToLowerInvariant();
        string text7 = Path.GetExtension(text5).TrimStart('.').ToLowerInvariant();
        bool flag = text7 == "app" || text7 == "exe";
        if (!flag && !(text6 == "index.html") && !ClickyMainArtifacts.KeptInsideProjectsExtensions.Contains(text7) && ClickyMainArtifacts.ProjectRoot(text5) != null)
        {
          supporting.Add(text5);
        }
      }
      return supporting;
    }

    // Token: 0x060006DA RID: 1754 RVA: 0x000F88C4 File Offset: 0x000F88C4
    public static IReadOnlyList<string> MainPaths(IReadOnlyList<string> paths)
    {
      ISet<string> supporting = ClickyMainArtifacts.SupportingPaths(paths);
      if (supporting.Count != 0)
      {
        return paths.Where<string>((string p) => !supporting.Contains(ClickyMainArtifacts.Standardized(p))).ToList<string>();
      }
      return paths;
    }

    // Token: 0x060006DB RID: 1755 RVA: 0x000F890C File Offset: 0x000F890C
    public static IReadOnlyList<string> MainEntries(IReadOnlyList<string> entries, Func<string, string> resolve)
    {
      List<string> list = entries.Select<string, string>(resolve).ToList<string>();
      ISet<string> supporting = ClickyMainArtifacts.SupportingPaths(list);
      if (supporting.Count == 0)
      {
        return entries;
      }
      return (from p in entries.Zip<string, string>(list)
        where !supporting.Contains(ClickyMainArtifacts.Standardized(p.Item2))
        select p.Item1).ToList<string>();
    }

    // Token: 0x060006DC RID: 1756 RVA: 0x000F8988 File Offset: 0x000F8988
    private static string Standardized(string path)
    {
      string text;
      try
      {
        text = Path.GetFullPath(path);
      }
      catch (Exception)
      {
        text = path;
      }
      return text;
    }

    // Token: 0x060006DD RID: 1757 RVA: 0x000F89B4 File Offset: 0x000F89B4
    private static bool IsReferencingPage(string path)
    {
      return ClickyMainArtifacts.PageExtensions.Contains(Path.GetExtension(path).TrimStart('.').ToLowerInvariant());
    }

    // Token: 0x060006DE RID: 1758 RVA: 0x000F89D4 File Offset: 0x000F89D4
    public static HashSet<string> References(string path)
    {
      HashSet<string> hashSet = ClickyMainArtifacts.DirectReferences(path);
      foreach (string text in hashSet.ToList<string>())
      {
        if (Path.GetExtension(text).Equals(".css", StringComparison.OrdinalIgnoreCase) && !text.Equals(path, StringComparison.OrdinalIgnoreCase))
        {
          hashSet.UnionWith(ClickyMainArtifacts.DirectReferences(text));
        }
      }
      return hashSet;
    }

    // Token: 0x060006DF RID: 1759 RVA: 0x000F8A54 File Offset: 0x000F8A54
    private static HashSet<string> DirectReferences(string path)
    {
      FileInfo fileInfo;
      try
      {
        fileInfo = new FileInfo(path);
        if (!fileInfo.Exists || fileInfo.Length > 2000000L)
        {
          return new HashSet<string>();
        }
      }
      catch (Exception)
      {
        return new HashSet<string>();
      }
      object obj = ClickyMainArtifacts.CacheLock;
      HashSet<string> hashSet;
      lock (obj)
      {
        ValueTuple<DateTime, HashSet<string>> valueTuple;
        if (ClickyMainArtifacts.ReferencesCache.TryGetValue(path, out valueTuple) && valueTuple.Item1 == fileInfo.LastWriteTimeUtc)
        {
          hashSet = new HashSet<string>();
          foreach (string text in valueTuple.Item2)
          {
            hashSet.Add(text);
          }
          return hashSet;
        }
      }
      string text2;
      try
      {
        byte[] array = File.ReadAllBytes(path);
        try
        {
          text2 = new UTF8Encoding(false, true).GetString(array);
        }
        catch (DecoderFallbackException)
        {
          text2 = Encoding.Latin1.GetString(array);
        }
      }
      catch (Exception)
      {
        return new HashSet<string>();
      }
      HashSet<string> hashSet2 = ClickyMainArtifacts.ReferencedPaths(text2, Path.GetExtension(path).TrimStart('.').ToLowerInvariant(), Path.GetDirectoryName(path) ?? "");
      obj = ClickyMainArtifacts.CacheLock;
      lock (obj)
      {
        if (ClickyMainArtifacts.ReferencesCache.Count > 400)
        {
          ClickyMainArtifacts.ReferencesCache.Clear();
        }
        ClickyMainArtifacts.ReferencesCache[path] = new ValueTuple<DateTime, HashSet<string>>(fileInfo.LastWriteTimeUtc, hashSet2);
      }
      hashSet = new HashSet<string>();
      foreach (string text in hashSet2)
      {
        hashSet.Add(text);
      }
      return hashSet;
    }

    // Token: 0x060006E0 RID: 1760 RVA: 0x000F8C74 File Offset: 0x000F8C74
    public static HashSet<string> ReferencedPaths(string text, string pageExtension, string pageDirectory)
    {
      ClickyMainArtifacts.<>c__DisplayClass12_0 CS$<>8__locals1;
      CS$<>8__locals1.text = text;
      CS$<>8__locals1.raw = new List<ValueTuple<string, bool>>();
      if (!(pageExtension == "md") && !(pageExtension == "markdown"))
      {
        if (!(pageExtension == "css"))
        {
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("\\s(?:src|poster|data-src)\\s*=\\s*(?:\"([^\"]*)\"|'([^']*)')", false, ref CS$<>8__locals1);
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("\\ssrcset\\s*=\\s*(?:\"([^\"]*)\"|'([^']*)')", false, ref CS$<>8__locals1);
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("\\shref\\s*=\\s*(?:\"([^\"]*)\"|'([^']*)')", true, ref CS$<>8__locals1);
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("url\\(\\s*(?:\"([^\"]*)\"|'([^']*)'|([^)\\s]+))\\s*\\)", false, ref CS$<>8__locals1);
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("[\"'`]([^\"'`\\s<>()]+\\.(?:png|jpe?g|gif|webp|avif|svg|ico|mp4|webm|mov|m4v|mp3|wav|m4a|woff2?|ttf|otf|css|js|json))[\"'`]", false, ref CS$<>8__locals1);
        }
        else
        {
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("url\\(\\s*(?:\"([^\"]*)\"|'([^']*)'|([^)\\s]+))\\s*\\)", false, ref CS$<>8__locals1);
          ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("@import\\s+(?:\"([^\"]*)\"|'([^']*)')", false, ref CS$<>8__locals1);
        }
      }
      else
      {
        ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("!\\[[^\\]]*\\]\\(\\s*<?([^)\\s>]+)", false, ref CS$<>8__locals1);
        ClickyMainArtifacts.<ReferencedPaths>g__Collect|12_0("<img[^>]*\\ssrc\\s*=\\s*(?:\"([^\"]*)\"|'([^']*)')", false, ref CS$<>8__locals1);
      }
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      foreach (ValueTuple<string, bool> valueTuple in CS$<>8__locals1.raw)
      {
        string item = valueTuple.Item1;
        bool item2 = valueTuple.Item2;
        IEnumerable<string> enumerable2;
        if (!item.Contains(',') || item2)
        {
          IEnumerable<string> enumerable = new <>z__ReadOnlySingleElementList<string>(item);
          enumerable2 = enumerable;
        }
        else
        {
          enumerable2 = from v in item.Split(',', StringSplitOptions.None)
            select v.Trim().Split(' ', StringSplitOptions.None).FirstOrDefault<string>() ?? "";
        }
        foreach (string text2 in enumerable2)
        {
          string text3 = ClickyMainArtifacts.LocalPath(text2, pageDirectory);
          if (text3 != null)
          {
            bool flag = item2;
            if (flag)
            {
              string text4 = Path.GetExtension(text3).TrimStart('.').ToLowerInvariant();
              bool flag2 = text4 == "html" || text4 == "htm" || text4 == "css";
              flag = !flag2;
            }
            if (!flag)
            {
              hashSet.Add(text3);
            }
          }
        }
      }
      return hashSet;
    }

    // Token: 0x060006E1 RID: 1761 RVA: 0x000F8E98 File Offset: 0x000F8E98
    [return: Nullable(2)]
    private static string LocalPath(string reference, string directory)
    {
      string text = reference.Trim();
      string lowered = text.ToLowerInvariant();
      if (text.Length == 0 || text.StartsWith('#') || text.StartsWith("//", StringComparison.Ordinal))
      {
        return null;
      }
      if (new string[] { "http:", "https:", "data:", "mailto:", "tel:", "javascript:", "blob:" }.Any<string>((string p) => lowered.StartsWith(p, StringComparison.Ordinal)))
      {
        return null;
      }
      string text2;
      if (lowered.StartsWith("file://", StringComparison.Ordinal))
      {
        Uri uri;
        string text3;
        if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
        {
          text2 = text;
          text3 = text2.Substring(7, text2.Length - 7);
        }
        else
        {
          text3 = uri.LocalPath;
        }
        text = text3;
      }
      int num = text.IndexOfAny(new char[] { '?', '#' });
      if (num >= 0)
      {
        text = text.Substring(0, num);
      }
      try
      {
        text = Uri.UnescapeDataString(text);
      }
      catch (Exception)
      {
      }
      if (text.Length == 0)
      {
        return null;
      }
      try
      {
        text2 = Path.GetFullPath(Path.IsPathFullyQualified(text) ? text : Path.Combine(directory, text.Replace('/', Path.DirectorySeparatorChar)));
      }
      catch (Exception)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060006E2 RID: 1762 RVA: 0x000F8FF8 File Offset: 0x000F8FF8
    [return: Nullable(2)]
    public static string ProjectRoot(string path)
    {
      string text = Path.GetDirectoryName(path);
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      int i = 0;
      while (i < 5)
      {
        if (!string.IsNullOrEmpty(text) && !text.Equals(folderPath, StringComparison.OrdinalIgnoreCase))
        {
          string pathRoot = Path.GetPathRoot(text);
          if (pathRoot == null || !pathRoot.Equals(text, StringComparison.OrdinalIgnoreCase))
          {
            if (ClickyMainArtifacts.IsProjectFolder(text))
            {
              return text;
            }
            text = Path.GetDirectoryName(text);
            i++;
            continue;
          }
        }
        return null;
      }
      return null;
    }

    // Token: 0x060006E3 RID: 1763 RVA: 0x000F905C File Offset: 0x000F905C
    private static bool IsProjectFolder(string directory)
    {
      object obj = ClickyMainArtifacts.CacheLock;
      lock (obj)
      {
        ValueTuple<DateTimeOffset, bool> valueTuple;
        if (ClickyMainArtifacts.ProjectFolderCache.TryGetValue(directory, out valueTuple) && DateTimeOffset.Now - valueTuple.Item1 < TimeSpan.FromSeconds(60L))
        {
          return valueTuple.Item2;
        }
      }
      HashSet<string> hashSet;
      try
      {
        hashSet = (from e in Directory.EnumerateFileSystemEntries(directory)
          select Path.GetFileName(e).ToLowerInvariant()).ToHashSet<string>();
      }
      catch (Exception)
      {
        hashSet = new HashSet<string>();
      }
      bool flag2;
      if (!hashSet.Overlaps(ClickyMainArtifacts.ProjectMarkerNames))
      {
        if (!hashSet.Any<string>((string n) => ClickyMainArtifacts.ProjectMarkerExtensions.Contains(Path.GetExtension(n).TrimStart('.'))))
        {
          flag2 = hashSet.Overlaps(ClickyMainArtifacts.WeakProjectMarkerNames) && hashSet.Overlaps(ClickyMainArtifacts.SourceFolderNames);
          goto IL_00E7;
        }
      }
      flag2 = true;
      IL_00E7:
      bool flag3 = flag2;
      obj = ClickyMainArtifacts.CacheLock;
      lock (obj)
      {
        if (ClickyMainArtifacts.ProjectFolderCache.Count > 400)
        {
          ClickyMainArtifacts.ProjectFolderCache.Clear();
        }
        ClickyMainArtifacts.ProjectFolderCache[directory] = new ValueTuple<DateTimeOffset, bool>(DateTimeOffset.Now, flag3);
      }
      return flag3;
    }

    // Token: 0x060006E5 RID: 1765 RVA: 0x000F94E8 File Offset: 0x000F94E8
    [CompilerGenerated]
    internal static void <ReferencedPaths>g__Collect|12_0(string pattern, bool isHref, ref ClickyMainArtifacts.<>c__DisplayClass12_0 A_2)
    {
      foreach (object obj in Regex.Matches(A_2.text, pattern, RegexOptions.IgnoreCase))
      {
        Match match = (Match)obj;
        for (int i = 1; i < match.Groups.Count; i++)
        {
          if (match.Groups[i].Success && match.Groups[i].Length > 0)
          {
            A_2.raw.Add(new ValueTuple<string, bool>(match.Groups[i].Value, isHref));
            break;
          }
        }
      }
    }

    // Token: 0x04000278 RID: 632
    private static readonly HashSet<string> ScratchExtensions = new HashSet<string>
    {
      "json", "log", "jsonl", "lock", "yaml", "yml", "toml", "js", "css", "map",
      "py", "sh", "swift", "ts", "ps1", "cs"
    };

    // Token: 0x04000279 RID: 633
    private static readonly HashSet<string> PageExtensions = new HashSet<string> { "html", "htm", "css", "md", "markdown" };

    // Token: 0x0400027A RID: 634
    private static readonly object CacheLock = new object();

    // Token: 0x0400027B RID: 635
    [TupleElementNames(new string[] { "ModifiedAt", "References" })]
    [Nullable(new byte[] { 1, 1, 0, 1, 1 })]
    private static readonly Dictionary<string, ValueTuple<DateTime, HashSet<string>>> ReferencesCache = new Dictionary<string, ValueTuple<DateTime, HashSet<string>>>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x0400027C RID: 636
    private static readonly HashSet<string> KeptInsideProjectsExtensions = new HashSet<string>
    {
      "pdf", "doc", "docx", "pages", "xls", "xlsx", "numbers", "csv", "ppt", "pptx",
      "key", "mp4", "mov", "m4v", "webm"
    };

    // Token: 0x0400027D RID: 637
    private static readonly HashSet<string> ProjectMarkerNames = new HashSet<string> { "package.swift", "cargo.toml", "go.mod", "build.gradle", "build.gradle.kts", "pubspec.yaml", "project.yml" };

    // Token: 0x0400027E RID: 638
    private static readonly HashSet<string> ProjectMarkerExtensions = new HashSet<string> { "xcodeproj", "xcworkspace", "sln", "csproj" };

    // Token: 0x0400027F RID: 639
    private static readonly HashSet<string> WeakProjectMarkerNames = new HashSet<string> { "package.json", "pyproject.toml", "gemfile" };

    // Token: 0x04000280 RID: 640
    private static readonly HashSet<string> SourceFolderNames = new HashSet<string> { "src", "app", "pages", "public", "lib", "components" };

    // Token: 0x04000281 RID: 641
    [TupleElementNames(new string[] { "CheckedAt", "IsProject" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private static readonly Dictionary<string, ValueTuple<DateTimeOffset, bool>> ProjectFolderCache = new Dictionary<string, ValueTuple<DateTimeOffset, bool>>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x0200017F RID: 383
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000778 RID: 1912
      [Nullable(0)]
      public static Func<string, bool> <0>__IsPathFullyQualified;

      // Token: 0x04000779 RID: 1913
      [Nullable(0)]
      public static Func<string, string> <1>__Standardized;

      // Token: 0x0400077A RID: 1914
      [Nullable(0)]
      public static Func<string, bool> <2>__IsReferencingPage;
    }
  }
}
