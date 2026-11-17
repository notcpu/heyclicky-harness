using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000EA RID: 234
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntroVideoSources
  {
    // Token: 0x1700035C RID: 860
    // (get) Token: 0x06000F00 RID: 3840 RVA: 0x0038D5F4 File Offset: 0x0038D5F4
    public static bool PrefersHosted
    {
      get
      {
        return IntroVideoSources.IsTruthy(Environment.GetEnvironmentVariable("HEYCLICKY_PREFER_HOSTED_INTRO_VIDEOS"));
      }
    }

    // Token: 0x1700035D RID: 861
    // (get) Token: 0x06000F01 RID: 3841 RVA: 0x0038D608 File Offset: 0x0038D608
    public static bool ForcesHostedFailure
    {
      get
      {
        return IntroVideoSources.IsTruthy(Environment.GetEnvironmentVariable("HEYCLICKY_FORCE_HOSTED_INTRO_VIDEO_FAILURE"));
      }
    }

    // Token: 0x06000F02 RID: 3842 RVA: 0x0038D61C File Offset: 0x0038D61C
    public static IReadOnlyList<IntroVideoSourceKind> PlaybackOrder(bool preferHosted)
    {
      if (!preferHosted)
      {
        return new <>z__ReadOnlyArray<IntroVideoSourceKind>(new IntroVideoSourceKind[]
        {
          IntroVideoSourceKind.Bundled,
          IntroVideoSourceKind.Hosted
        });
      }
      IntroVideoSourceKind[] array = new IntroVideoSourceKind[2];
      array[0] = IntroVideoSourceKind.Hosted;
      return new <>z__ReadOnlyArray<IntroVideoSourceKind>(array);
    }

    // Token: 0x06000F03 RID: 3843 RVA: 0x0038D650 File Offset: 0x0038D650
    public static IntroVideoSourceKind Alternate(IntroVideoSourceKind kind)
    {
      if (kind != IntroVideoSourceKind.Hosted)
      {
        return IntroVideoSourceKind.Hosted;
      }
      return IntroVideoSourceKind.Bundled;
    }

    // Token: 0x06000F04 RID: 3844 RVA: 0x0038D65C File Offset: 0x0038D65C
    public static string AnalyticsName(IntroVideoSourceKind kind)
    {
      if (kind != IntroVideoSourceKind.Hosted)
      {
        return "bundled";
      }
      return "hosted";
    }

    // Token: 0x06000F05 RID: 3845 RVA: 0x0038D670 File Offset: 0x0038D670
    [NullableContext(2)]
    public static bool IsTruthy(string value)
    {
      string text = ((value != null) ? value.Trim().ToLowerInvariant() : null);
      return text == "1" || text == "true" || text == "yes" || text == "y" || text == "on";
    }

    // Token: 0x06000F06 RID: 3846 RVA: 0x0038D6D8 File Offset: 0x0038D6D8
    public static string Redacted([Nullable(2)] string url)
    {
      if (string.IsNullOrEmpty(url))
      {
        return "";
      }
      Uri uri;
      if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
      {
        return url;
      }
      if (uri.IsFile)
      {
        return "Assets/Video/" + Path.GetFileName(uri.LocalPath);
      }
      return uri.GetLeftPart(UriPartial.Path);
    }

    // Token: 0x04000668 RID: 1640
    public const string HostedOnboardingVideoUrl = "https://stream.mux.com/7bOYF1PGo02RKwcQW00L00Aa01iRKElKXoch4xhtsJzv8QU.m3u8";

    // Token: 0x04000669 RID: 1641
    public const string BundledOnboardingVideoFileName = "onboarding-intro-v2-windows.mp4";

    // Token: 0x0400066A RID: 1642
    public const string PreferHostedEnvironmentVariable = "HEYCLICKY_PREFER_HOSTED_INTRO_VIDEOS";

    // Token: 0x0400066B RID: 1643
    public const string ForceHostedFailureEnvironmentVariable = "HEYCLICKY_FORCE_HOSTED_INTRO_VIDEO_FAILURE";
  }
}
