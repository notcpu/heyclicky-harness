using System;
using System.Runtime.CompilerServices;
using HeyClicky.Drawing.Audio;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000336 RID: 822
  [NullableContext(1)]
  [Nullable(0)]
  public static class FileCardSound
  {
    // Token: 0x17000673 RID: 1651
    // (get) Token: 0x06002304 RID: 8964 RVA: 0x0062A5E4 File Offset: 0x0062A5E4
    // (set) Token: 0x06002305 RID: 8965 RVA: 0x0062A5EC File Offset: 0x0062A5EC
    public static Func<bool> PlaysSounds { get; set; } = () => true;

    // Token: 0x06002306 RID: 8966 RVA: 0x0062A5F4 File Offset: 0x0062A5F4
    public static void NoteFocusChanged()
    {
      FileCardSound.lastFocusChangeAt = DateTimeOffset.Now;
    }

    // Token: 0x06002307 RID: 8967 RVA: 0x0062A600 File Offset: 0x0062A600
    public static void PlayLift()
    {
      if (!FileCardSound.PlaysSounds())
      {
        return;
      }
      DateTimeOffset now = DateTimeOffset.Now;
      if ((now - FileCardSound.lastFocusChangeAt).TotalSeconds <= 0.45 || (now - FileCardSound.lastPlayedAt).TotalSeconds <= 0.12)
      {
        return;
      }
      FileCardSound.lastPlayedAt = now;
      try
      {
        ScreenDrawingSounds.Play("paper-place", 0.14f, 0.02, FileCardSound.owner);
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "paper sound: " + ex.Message);
      }
    }

    // Token: 0x0400110E RID: 4366
    private static readonly object owner = new object();

    // Token: 0x0400110F RID: 4367
    private static DateTimeOffset lastPlayedAt = DateTimeOffset.MinValue;

    // Token: 0x04001110 RID: 4368
    private static DateTimeOffset lastFocusChangeAt = DateTimeOffset.MinValue;
  }
}
