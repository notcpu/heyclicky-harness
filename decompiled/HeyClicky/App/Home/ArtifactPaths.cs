using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Home
{
  // Token: 0x020002A9 RID: 681
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArtifactPaths
  {
    // Token: 0x06001BEF RID: 7151 RVA: 0x005F70EC File Offset: 0x005F70EC
    public static bool IsWebLink(string entry)
    {
      return entry.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || entry.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x06001BF0 RID: 7152 RVA: 0x005F710C File Offset: 0x005F710C
    public static string Resolve(string entry, [Nullable(2)] string workspace)
    {
      if (ArtifactPaths.IsWebLink(entry))
      {
        return entry;
      }
      string text = entry.Trim();
      Uri uri;
      if (text.StartsWith("file://", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(text, UriKind.Absolute, out uri) && uri.IsFile)
      {
        text = uri.LocalPath;
      }
      if (text.StartsWith('~'))
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string text2 = text;
        text = Path.Combine(folderPath, text2.Substring(1, text2.Length - 1).TrimStart(new char[] { '/', '\\' }));
      }
      if (Path.IsPathRooted(text))
      {
        return Path.GetFullPath(text);
      }
      if (workspace != null)
      {
        return Path.GetFullPath(Path.Combine(workspace, text));
      }
      return text;
    }

    // Token: 0x06001BF1 RID: 7153 RVA: 0x005F71B0 File Offset: 0x005F71B0
    public static bool SupportsInline(string path)
    {
      bool flag = !ArtifactPaths.IsWebLink(path) && File.Exists(path);
      if (flag)
      {
        string text = Path.GetExtension(path).ToLowerInvariant();
        bool flag2 = text == ".html" || text == ".htm" || text == ".xhtml";
        flag = !flag2;
      }
      return flag;
    }

    // Token: 0x06001BF2 RID: 7154 RVA: 0x005F7214 File Offset: 0x005F7214
    public static void Reveal(string path)
    {
      if (ArtifactPaths.IsWebLink(path))
      {
        return;
      }
      if (File.Exists(path) || Directory.Exists(path))
      {
        ChatBlockViews.RevealPath(path);
        return;
      }
      string text = Path.GetDirectoryName(path);
      while (text != null && !Directory.Exists(text))
      {
        text = Path.GetDirectoryName(text);
      }
      if (text != null)
      {
        ChatBlockViews.OpenPath(text);
      }
    }
  }
}
