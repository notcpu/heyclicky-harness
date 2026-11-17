using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000036 RID: 54
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyHelperFiles
  {
    // Token: 0x1700015D RID: 349
    // (get) Token: 0x0600054F RID: 1359 RVA: 0x000F07FC File Offset: 0x000F07FC
    // (set) Token: 0x06000550 RID: 1360 RVA: 0x000F0804 File Offset: 0x000F0804
    public static string FolderPath { get; set; } = AppPaths.Combine(new string[] { "helper" });

    // Token: 0x06000551 RID: 1361 RVA: 0x000F080C File Offset: 0x000F080C
    [return: Nullable(2)]
    public static string WriteNoteGuide(string guideText)
    {
      return ClickyHelperFiles.Write(guideText, "note-guide.md");
    }

    // Token: 0x06000552 RID: 1362 RVA: 0x000F081C File Offset: 0x000F081C
    [NullableContext(2)]
    public static string WriteUserProfile(string profileMarkdown)
    {
      if (!string.IsNullOrWhiteSpace(profileMarkdown))
      {
        return ClickyHelperFiles.Write(profileMarkdown, "about-the-user.md");
      }
      return null;
    }

    // Token: 0x06000553 RID: 1363 RVA: 0x000F0834 File Offset: 0x000F0834
    public static void RemoveAllForSignOut()
    {
      object gate = ClickyHelperFiles.Gate;
      lock (gate)
      {
        try
        {
          if (Directory.Exists(ClickyHelperFiles.FolderPath))
          {
            Directory.Delete(ClickyHelperFiles.FolderPath, true);
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Jobs", "helper files not removed: " + ex.Message);
        }
        ClickyHelperFiles.LastWrittenTextByPath.Clear();
      }
    }

    // Token: 0x06000554 RID: 1364 RVA: 0x000F08BC File Offset: 0x000F08BC
    [return: Nullable(2)]
    private static string Write(string text, string fileName)
    {
      string text2 = Path.Combine(ClickyHelperFiles.FolderPath, fileName);
      object gate = ClickyHelperFiles.Gate;
      string text4;
      lock (gate)
      {
        string text3;
        if (ClickyHelperFiles.LastWrittenTextByPath.TryGetValue(text2, out text3) && text3 == text && File.Exists(text2))
        {
          text4 = text2;
        }
        else
        {
          try
          {
            Directory.CreateDirectory(ClickyHelperFiles.FolderPath);
            string text5 = text2 + ".tmp";
            File.WriteAllText(text5, text, new UTF8Encoding(false));
            File.Move(text5, text2, true);
            ClickyHelperFiles.LastWrittenTextByPath[text2] = text;
            text4 = text2;
          }
          catch (Exception ex)
          {
            Log.Warn("Jobs", "couldn't write " + fileName + ": " + ex.Message);
            text4 = null;
          }
        }
      }
      return text4;
    }

    // Token: 0x040001F1 RID: 497
    private static readonly object Gate = new object();

    // Token: 0x040001F2 RID: 498
    private static readonly Dictionary<string, string> LastWrittenTextByPath = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
  }
}
