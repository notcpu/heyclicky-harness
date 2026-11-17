using System;
using System.Runtime.CompilerServices;
using Windows.UI.ViewManagement;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B5 RID: 181
  public static class MotionPreference
  {
    // Token: 0x06000772 RID: 1906 RVA: 0x00538780 File Offset: 0x00538780
    static MotionPreference()
    {
      try
      {
        MotionPreference.Settings = new UISettings();
        MotionPreference.reduced = !MotionPreference.Settings.AnimationsEnabled;
        try
        {
          MotionPreference.Settings.AnimationsEnabledChanged += delegate(UISettings _, UISettingsAnimationsEnabledChangedEventArgs _)
          {
            MotionPreference.Refresh();
          };
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        Log.Warn("motion", "UISettings unavailable: " + ex.Message);
      }
      if (MotionPreference.reduced)
      {
        Log.Info("motion", "Windows animation effects are off: reduced motion");
      }
    }

    // Token: 0x17000133 RID: 307
    // (get) Token: 0x06000773 RID: 1907 RVA: 0x00538838 File Offset: 0x00538838
    public static bool Reduced
    {
      get
      {
        return MotionPreference.Forced || MotionPreference.reduced;
      }
    }

    // Token: 0x06000774 RID: 1908 RVA: 0x0053884C File Offset: 0x0053884C
    public static void Refresh()
    {
      try
      {
        if (MotionPreference.Settings != null)
        {
          bool flag = !MotionPreference.Settings.AnimationsEnabled;
          if (flag != MotionPreference.reduced)
          {
            MotionPreference.reduced = flag;
            Log.Info("motion", flag ? "animation effects turned off: reduced motion" : "animation effects turned on");
          }
        }
      }
      catch
      {
      }
    }

    // Token: 0x04000237 RID: 567
    [Nullable(2)]
    private static readonly UISettings Settings;

    // Token: 0x04000238 RID: 568
    private static volatile bool reduced;

    // Token: 0x04000239 RID: 569
    private static readonly bool Forced = Environment.GetEnvironmentVariable("HEYCLICKY_REDUCE_MOTION") == "1";
  }
}
