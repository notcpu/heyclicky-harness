using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000EC RID: 236
  [NullableContext(2)]
  [Nullable(0)]
  public static class IdleInstallPolicy
  {
    // Token: 0x06000B82 RID: 2946 RVA: 0x0055BA6C File Offset: 0x0055BA6C
    public static string BusyReason(IdleInstallContext now)
    {
      if (now.AgentWorking)
      {
        return "an agent is working";
      }
      if (now.VoiceActive)
      {
        return "a voice turn is active";
      }
      if (now.Driving)
      {
        return "driving the screen";
      }
      if (now.NotchAnswering)
      {
        return "the notch is answering";
      }
      if (now.NotchOpen)
      {
        return "the notch is open";
      }
      if (now.ShowingOnScreen)
      {
        return "something is on screen";
      }
      if (now.Recording)
      {
        return "recording";
      }
      if (now.Onboarding)
      {
        return "onboarding";
      }
      return null;
    }

    // Token: 0x06000B83 RID: 2947 RVA: 0x0055BAF4 File Offset: 0x0055BAF4
    public static string WhyWait(IdleInstallContext now, TimeSpan heyClickyIdle, TimeSpan idleFor)
    {
      string text = IdleInstallPolicy.BusyReason(now);
      if (text != null)
      {
        return text;
      }
      if (heyClickyIdle < idleFor)
      {
        return "HeyClicky was busy recently";
      }
      if (now.FullScreen)
      {
        return "a fullscreen app";
      }
      if (now.Presentation)
      {
        return "a presentation";
      }
      if (now.OnCall)
      {
        return "on a call";
      }
      return null;
    }

    // Token: 0x04000426 RID: 1062
    public static readonly TimeSpan IdleFor = TimeSpan.FromMinutes(5L);
  }
}
