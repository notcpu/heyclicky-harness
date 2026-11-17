using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000067 RID: 103
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyUserSkillFolders
  {
    // Token: 0x06000828 RID: 2088 RVA: 0x000FF804 File Offset: 0x000FF804
    public static Task<IReadOnlyList<string>> SkillDirectoriesAsync([Nullable(2)] string homeDirectory = null)
    {
      return Task.Run<IReadOnlyList<string>>(() => ClickyUserSkillFolders.SkillDirectoriesNow(homeDirectory ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));
    }

    // Token: 0x06000829 RID: 2089 RVA: 0x000FF824 File Offset: 0x000FF824
    public static IReadOnlyList<string> SkillDirectoriesNow(string homeDirectory)
    {
      List<string> list = new List<string>();
      if (string.IsNullOrEmpty(homeDirectory))
      {
        return list;
      }
      HashSet<string> hashSet = new HashSet<string>(OperatingSystem.IsWindows() ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
      string[] rootsRelativeToHome = ClickyUserSkillFolders.RootsRelativeToHome;
      int i = 0;
      while (i < rootsRelativeToHome.Length)
      {
        string text = rootsRelativeToHome[i];
        string text2 = Path.Combine(homeDirectory, text);
        string[] array;
        try
        {
          array = (Directory.Exists(text2) ? Directory.GetFileSystemEntries(text2) : Array.Empty<string>());
        }
        catch (Exception)
        {
          goto IL_010B;
        }
        goto IL_0066;
        IL_010B:
        i++;
        continue;
        IL_0066:
        foreach (string text3 in array.OrderBy<string, string>((string e) => Path.GetFileName(e), StringComparer.Ordinal))
        {
          if (!Path.GetFileName(text3).StartsWith('.'))
          {
            try
            {
              if (!File.Exists(Path.Combine(text3, "SKILL.md")))
              {
                continue;
              }
            }
            catch (Exception)
            {
              continue;
            }
            if (hashSet.Add(ClickyUserSkillFolders.RealPath(text3)))
            {
              list.Add(text3);
              if (list.Count == 40)
              {
                return list;
              }
            }
          }
        }
        goto IL_010B;
      }
      return list;
    }

    // Token: 0x0600082A RID: 2090 RVA: 0x000FF978 File Offset: 0x000FF978
    private static string RealPath(string directory)
    {
      string text;
      try
      {
        DirectoryInfo directoryInfo = new DirectoryInfo(directory);
        FileSystemInfo fileSystemInfo = directoryInfo.ResolveLinkTarget(true);
        text = Path.TrimEndingDirectorySeparator(Path.GetFullPath(((fileSystemInfo != null) ? fileSystemInfo.FullName : null) ?? directoryInfo.FullName));
      }
      catch (Exception)
      {
        text = Path.TrimEndingDirectorySeparator(Path.GetFullPath(directory));
      }
      return text;
    }

    // Token: 0x040002FC RID: 764
    public static readonly string[] RootsRelativeToHome = new string[]
    {
      Path.Combine(".agents", "skills"),
      Path.Combine(".claude", "skills")
    };

    // Token: 0x040002FD RID: 765
    public const int MaximumSkills = 40;
  }
}
