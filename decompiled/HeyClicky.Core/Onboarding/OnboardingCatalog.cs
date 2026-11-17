using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000F2 RID: 242
  [NullableContext(1)]
  [Nullable(0)]
  public static class OnboardingCatalog
  {
    // Token: 0x1700037D RID: 893
    // (get) Token: 0x06000F73 RID: 3955 RVA: 0x0038E9D8 File Offset: 0x0038E9D8
    public static SquadPack DefaultSquadPack
    {
      get
      {
        return OnboardingCatalog.AllSquadPacks[4];
      }
    }

    // Token: 0x06000F74 RID: 3956 RVA: 0x0038E9E8 File Offset: 0x0038E9E8
    public static string VoiceGender(string voiceId)
    {
      ExpressiveVoice expressiveVoice = VoiceCatalog.Expressive(voiceId);
      string text;
      if ((text = ((expressiveVoice != null) ? expressiveVoice.Gender : null)) == null)
      {
        text = OnboardingCatalog.Voices.FirstOrDefault<ValueTuple<string, string>>(([TupleElementNames(new string[] { "Id", "Gender" })] ValueTuple<string, string> v) => v.Item1 == voiceId).Item2 ?? "neutral";
      }
      return text;
    }

    // Token: 0x0400068B RID: 1675
    public static readonly IReadOnlyList<DiscoveryChannelOption> DiscoveryChannels = new <>z__ReadOnlyArray<DiscoveryChannelOption>(new DiscoveryChannelOption[]
    {
      new DiscoveryChannelOption("instagram_ad", "Instagram Ad"),
      new DiscoveryChannelOption("instagram", "Instagram"),
      new DiscoveryChannelOption("youtube_ad", "YouTube Ad"),
      new DiscoveryChannelOption("youtube", "YouTube"),
      new DiscoveryChannelOption("tiktok", "TikTok"),
      new DiscoveryChannelOption("x", "X/Twitter"),
      new DiscoveryChannelOption("linkedin", "LinkedIn"),
      new DiscoveryChannelOption("reddit", "Reddit"),
      new DiscoveryChannelOption("chatgpt", "ChatGPT"),
      new DiscoveryChannelOption("claude", "Claude"),
      new DiscoveryChannelOption("google", "Google"),
      new DiscoveryChannelOption("friend", "A friend"),
      new DiscoveryChannelOption("other", "Other")
    });

    // Token: 0x0400068C RID: 1676
    public static readonly IReadOnlyList<SquadPack> AllSquadPacks = new <>z__ReadOnlyArray<SquadPack>(new SquadPack[]
    {
      new SquadPack("puffPals", "Puff Pals", false),
      new SquadPack("kiCrew", "Ki Crew", false),
      new SquadPack("arcadeAllStars", "Arcade All-Stars", false),
      new SquadPack("cloudClub", "Cloud Club", true),
      new SquadPack("classicClouds", "Dreamy Cloud", true),
      new SquadPack("storybookSpirits", "Storybook Spirits", false)
    });

    // Token: 0x0400068D RID: 1677
    public static readonly IReadOnlyList<CursorColorOption> CursorColors = new <>z__ReadOnlyArray<CursorColorOption>(new CursorColorOption[]
    {
      new CursorColorOption("red", "#FF4C4C"),
      new CursorColorOption("blue", "#3380FF"),
      new CursorColorOption("yellow", "#D4A017"),
      new CursorColorOption("green", "#26C281")
    });

    // Token: 0x0400068E RID: 1678
    [TupleElementNames(new string[] { "Id", "Gender" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    public static readonly IReadOnlyList<ValueTuple<string, string>> Voices = new <>z__ReadOnlyArray<ValueTuple<string, string>>(new ValueTuple<string, string>[]
    {
      new ValueTuple<string, string>("cedar", "male"),
      new ValueTuple<string, string>("marin", "female"),
      new ValueTuple<string, string>("alloy", "neutral"),
      new ValueTuple<string, string>("ash", "male"),
      new ValueTuple<string, string>("ballad", "male"),
      new ValueTuple<string, string>("coral", "female"),
      new ValueTuple<string, string>("echo", "male"),
      new ValueTuple<string, string>("sage", "female"),
      new ValueTuple<string, string>("shimmer", "female"),
      new ValueTuple<string, string>("verse", "male")
    });
  }
}
