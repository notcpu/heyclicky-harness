using System;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Settings;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000186 RID: 390
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyLiveNotePreferences
  {
    // Token: 0x1700054E RID: 1358
    // (get) Token: 0x06001757 RID: 5975 RVA: 0x003C338C File Offset: 0x003C338C
    // (set) Token: 0x06001758 RID: 5976 RVA: 0x003C3394 File Offset: 0x003C3394
    [Nullable(2)]
    public static SettingsStore Settings
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700054F RID: 1359
    // (get) Token: 0x06001759 RID: 5977 RVA: 0x003C339C File Offset: 0x003C339C
    public static bool IsEnabled
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000550 RID: 1360
    // (get) Token: 0x0600175A RID: 5978 RVA: 0x003C33A0 File Offset: 0x003C33A0
    public static bool StreamsPatches
    {
      get
      {
        if (ClickyLiveNotePreferences.IsEnabled)
        {
          SettingsStore settings = ClickyLiveNotePreferences.Settings;
          bool? flag;
          if (settings == null)
          {
            flag = null;
          }
          else
          {
            flag = settings.Read<bool?>((AppSettings s) => s.LiveNotesStreamPatches);
          }
          bool? flag2 = flag;
          return flag2.GetValueOrDefault(true);
        }
        return false;
      }
    }

    // Token: 0x0600175B RID: 5979 RVA: 0x003C33F8 File Offset: 0x003C33F8
    public static string NotePath(string folderPath)
    {
      return Path.GetFullPath(Path.Combine(folderPath, "note.md"));
    }

    // Token: 0x04000A12 RID: 2578
    public const string FileName = "note.md";

    // Token: 0x04000A13 RID: 2579
    public const string EnabledDefaultsKey = "ClickyLiveNotesEnabled";

    // Token: 0x04000A14 RID: 2580
    public const string StreamsPatchesDefaultsKey = "ClickyLiveNotesStreamPatches";
  }
}
