using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D4 RID: 212
  [NullableContext(1)]
  [Nullable(0)]
  public static class AgentFolders
  {
    // Token: 0x17000181 RID: 385
    // (get) Token: 0x06000A0D RID: 2573 RVA: 0x005558DC File Offset: 0x005558DC
    public static string Default
    {
      get
      {
        return AppPaths.Combine(new string[] { "Clickys" });
      }
    }

    // Token: 0x17000182 RID: 386
    // (get) Token: 0x06000A0E RID: 2574 RVA: 0x005558F4 File Offset: 0x005558F4
    public static string LegacyDocuments
    {
      get
      {
        return AppPaths.DocumentsRoot;
      }
    }

    // Token: 0x06000A0F RID: 2575 RVA: 0x005558FC File Offset: 0x005558FC
    public static string Abbreviate(string path)
    {
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      if (!path.StartsWith(folderPath, StringComparison.OrdinalIgnoreCase))
      {
        return path;
      }
      string text = "~";
      int length = folderPath.Length;
      return text + path.Substring(length, path.Length - length);
    }
  }
}
