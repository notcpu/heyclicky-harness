using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000FA RID: 250
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyFace
  {
    // Token: 0x06000D39 RID: 3385 RVA: 0x00565A2C File Offset: 0x00565A2C
    public static PortraitView Create(string seed, double size, PortraitMood mood = PortraitMood.Happy, bool showsAvatarBackground = true, bool tracksPointer = true, bool frozen = false)
    {
      return new PortraitView(ClickyRoster.Shared.AppearanceFor(seed), size, size, mood.Expression(), showsAvatarBackground, null, 0.0)
      {
        AnimatesWhileIdle = (size >= 64.0 && !frozen),
        TracksPointer = tracksPointer
      };
    }

    // Token: 0x06000D3A RID: 3386 RVA: 0x00565A88 File Offset: 0x00565A88
    public static PortraitView NotchBadge(string seed, double size = 26.0)
    {
      return ClickyFace.Create(seed, size, PortraitMood.Running, false, false, true);
    }

    // Token: 0x06000D3B RID: 3387 RVA: 0x00565A98 File Offset: 0x00565A98
    public static string SeedFor(AgentTaskSnapshot task)
    {
      return task.TaskId;
    }
  }
}
