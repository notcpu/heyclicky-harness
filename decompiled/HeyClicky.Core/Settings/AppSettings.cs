using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using HeyClicky.Core.Onboarding;

namespace HeyClicky.Core.Settings
{
  // Token: 0x020000B3 RID: 179
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AppSettings
  {
    // Token: 0x17000252 RID: 594
    // (get) Token: 0x06000B62 RID: 2914 RVA: 0x0037ACB8 File Offset: 0x0037ACB8
    // (set) Token: 0x06000B63 RID: 2915 RVA: 0x0037ACC0 File Offset: 0x0037ACC0
    [Nullable(1)]
    public string CursorColorHex
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "#3380FF";

    // Token: 0x17000253 RID: 595
    // (get) Token: 0x06000B64 RID: 2916 RVA: 0x0037ACCC File Offset: 0x0037ACCC
    // (set) Token: 0x06000B65 RID: 2917 RVA: 0x0037ACD4 File Offset: 0x0037ACD4
    public string SquadMemberId { get; set; }

    // Token: 0x17000254 RID: 596
    // (get) Token: 0x06000B66 RID: 2918 RVA: 0x0037ACE0 File Offset: 0x0037ACE0
    // (set) Token: 0x06000B67 RID: 2919 RVA: 0x0037ACE8 File Offset: 0x0037ACE8
    [Nullable(1)]
    public string VoiceIdentifier
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "cedar";

    // Token: 0x17000255 RID: 597
    // (get) Token: 0x06000B68 RID: 2920 RVA: 0x0037ACF4 File Offset: 0x0037ACF4
    // (set) Token: 0x06000B69 RID: 2921 RVA: 0x0037ACFC File Offset: 0x0037ACFC
    public double SpeechSpeed { get; set; } = 1.0;

    // Token: 0x17000256 RID: 598
    // (get) Token: 0x06000B6A RID: 2922 RVA: 0x0037AD08 File Offset: 0x0037AD08
    // (set) Token: 0x06000B6B RID: 2923 RVA: 0x0037AD10 File Offset: 0x0037AD10
    public string VoiceLanguageCode { get; set; }

    // Token: 0x17000257 RID: 599
    // (get) Token: 0x06000B6C RID: 2924 RVA: 0x0037AD1C File Offset: 0x0037AD1C
    // (set) Token: 0x06000B6D RID: 2925 RVA: 0x0037AD24 File Offset: 0x0037AD24
    public bool ScreenHighlightEnabled { get; set; } = true;

    // Token: 0x17000258 RID: 600
    // (get) Token: 0x06000B6E RID: 2926 RVA: 0x0037AD30 File Offset: 0x0037AD30
    // (set) Token: 0x06000B6F RID: 2927 RVA: 0x0037AD38 File Offset: 0x0037AD38
    public bool CursorDocked { get; set; }

    // Token: 0x17000259 RID: 601
    // (get) Token: 0x06000B70 RID: 2928 RVA: 0x0037AD44 File Offset: 0x0037AD44
    // (set) Token: 0x06000B71 RID: 2929 RVA: 0x0037AD4C File Offset: 0x0037AD4C
    public bool ShakeToDetachEnabled { get; set; }

    // Token: 0x1700025A RID: 602
    // (get) Token: 0x06000B72 RID: 2930 RVA: 0x0037AD58 File Offset: 0x0037AD58
    // (set) Token: 0x06000B73 RID: 2931 RVA: 0x0037AD60 File Offset: 0x0037AD60
    public bool LaunchAtLogin { get; set; } = true;

    // Token: 0x1700025B RID: 603
    // (get) Token: 0x06000B74 RID: 2932 RVA: 0x0037AD6C File Offset: 0x0037AD6C
    // (set) Token: 0x06000B75 RID: 2933 RVA: 0x0037AD74 File Offset: 0x0037AD74
    public bool ShowInTaskbar { get; set; } = true;

    // Token: 0x1700025C RID: 604
    // (get) Token: 0x06000B76 RID: 2934 RVA: 0x0037AD80 File Offset: 0x0037AD80
    // (set) Token: 0x06000B77 RID: 2935 RVA: 0x0037AD88 File Offset: 0x0037AD88
    public bool TypedTurnsRespondWithVoice { get; set; }

    // Token: 0x1700025D RID: 605
    // (get) Token: 0x06000B78 RID: 2936 RVA: 0x0037AD94 File Offset: 0x0037AD94
    // (set) Token: 0x06000B79 RID: 2937 RVA: 0x0037AD9C File Offset: 0x0037AD9C
    [Nullable(1)]
    public string DictationLanguageCode
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "multi";

    // Token: 0x1700025E RID: 606
    // (get) Token: 0x06000B7A RID: 2938 RVA: 0x0037ADA8 File Offset: 0x0037ADA8
    // (set) Token: 0x06000B7B RID: 2939 RVA: 0x0037ADB0 File Offset: 0x0037ADB0
    [Nullable(1)]
    public string DictationSpecificLanguageCode
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    } = "en";

    // Token: 0x1700025F RID: 607
    // (get) Token: 0x06000B7C RID: 2940 RVA: 0x0037ADBC File Offset: 0x0037ADBC
    // (set) Token: 0x06000B7D RID: 2941 RVA: 0x0037ADC4 File Offset: 0x0037ADC4
    public double? DictationLastCompletedAtEpochSeconds { get; set; }

    // Token: 0x17000260 RID: 608
    // (get) Token: 0x06000B7E RID: 2942 RVA: 0x0037ADD0 File Offset: 0x0037ADD0
    // (set) Token: 0x06000B7F RID: 2943 RVA: 0x0037ADD8 File Offset: 0x0037ADD8
    [Nullable(1)]
    public List<string> DictationCustomWords
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000261 RID: 609
    // (get) Token: 0x06000B80 RID: 2944 RVA: 0x0037ADE4 File Offset: 0x0037ADE4
    // (set) Token: 0x06000B81 RID: 2945 RVA: 0x0037ADEC File Offset: 0x0037ADEC
    public bool DictationClaudeSeeded { get; set; }

    // Token: 0x17000262 RID: 610
    // (get) Token: 0x06000B82 RID: 2946 RVA: 0x0037ADF8 File Offset: 0x0037ADF8
    // (set) Token: 0x06000B83 RID: 2947 RVA: 0x0037AE00 File Offset: 0x0037AE00
    public string DictationAccountNameSeeded { get; set; }

    // Token: 0x17000263 RID: 611
    // (get) Token: 0x06000B84 RID: 2948 RVA: 0x0037AE0C File Offset: 0x0037AE0C
    // (set) Token: 0x06000B85 RID: 2949 RVA: 0x0037AE14 File Offset: 0x0037AE14
    public string DiscoveryChannelPendingSync { get; set; }

    // Token: 0x17000264 RID: 612
    // (get) Token: 0x06000B86 RID: 2950 RVA: 0x0037AE20 File Offset: 0x0037AE20
    // (set) Token: 0x06000B87 RID: 2951 RVA: 0x0037AE28 File Offset: 0x0037AE28
    public string MicrophoneDeviceName { get; set; }

    // Token: 0x17000265 RID: 613
    // (get) Token: 0x06000B88 RID: 2952 RVA: 0x0037AE34 File Offset: 0x0037AE34
    // (set) Token: 0x06000B89 RID: 2953 RVA: 0x0037AE3C File Offset: 0x0037AE3C
    public bool AgentAnnouncementsVoiceEnabled { get; set; }

    // Token: 0x17000266 RID: 614
    // (get) Token: 0x06000B8A RID: 2954 RVA: 0x0037AE48 File Offset: 0x0037AE48
    // (set) Token: 0x06000B8B RID: 2955 RVA: 0x0037AE50 File Offset: 0x0037AE50
    public int DocumentAccessNoticeShowCount { get; set; }

    // Token: 0x17000267 RID: 615
    // (get) Token: 0x06000B8C RID: 2956 RVA: 0x0037AE5C File Offset: 0x0037AE5C
    // (set) Token: 0x06000B8D RID: 2957 RVA: 0x0037AE64 File Offset: 0x0037AE64
    public bool DocumentAccessNoticeSuppressed { get; set; }

    // Token: 0x17000268 RID: 616
    // (get) Token: 0x06000B8E RID: 2958 RVA: 0x0037AE70 File Offset: 0x0037AE70
    // (set) Token: 0x06000B8F RID: 2959 RVA: 0x0037AE78 File Offset: 0x0037AE78
    [Nullable(1)]
    public OnboardingProgress Onboarding
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000269 RID: 617
    // (get) Token: 0x06000B90 RID: 2960 RVA: 0x0037AE84 File Offset: 0x0037AE84
    // (set) Token: 0x06000B91 RID: 2961 RVA: 0x0037AE8C File Offset: 0x0037AE8C
    public HotkeyBindings Hotkeys { get; set; }

    // Token: 0x1700026A RID: 618
    // (get) Token: 0x06000B92 RID: 2962 RVA: 0x0037AE98 File Offset: 0x0037AE98
    // (set) Token: 0x06000B93 RID: 2963 RVA: 0x0037AEA0 File Offset: 0x0037AEA0
    public string MicrophoneDeviceId { get; set; }

    // Token: 0x1700026B RID: 619
    // (get) Token: 0x06000B94 RID: 2964 RVA: 0x0037AEAC File Offset: 0x0037AEAC
    // (set) Token: 0x06000B95 RID: 2965 RVA: 0x0037AEB4 File Offset: 0x0037AEB4
    public string OutputDeviceId { get; set; }

    // Token: 0x1700026C RID: 620
    // (get) Token: 0x06000B96 RID: 2966 RVA: 0x0037AEC0 File Offset: 0x0037AEC0
    // (set) Token: 0x06000B97 RID: 2967 RVA: 0x0037AEC8 File Offset: 0x0037AEC8
    public bool DrawingSoundsEnabled { get; set; }

    // Token: 0x1700026D RID: 621
    // (get) Token: 0x06000B98 RID: 2968 RVA: 0x0037AED4 File Offset: 0x0037AED4
    // (set) Token: 0x06000B99 RID: 2969 RVA: 0x0037AEDC File Offset: 0x0037AEDC
    public bool DrawingVoiceCaptions { get; set; }

    // Token: 0x1700026E RID: 622
    // (get) Token: 0x06000B9A RID: 2970 RVA: 0x0037AEE8 File Offset: 0x0037AEE8
    // (set) Token: 0x06000B9B RID: 2971 RVA: 0x0037AEF0 File Offset: 0x0037AEF0
    public bool? KeepDrawingsInChat { get; set; }

    // Token: 0x1700026F RID: 623
    // (get) Token: 0x06000B9C RID: 2972 RVA: 0x0037AEFC File Offset: 0x0037AEFC
    [JsonIgnore]
    public bool KeepDrawingsInChatEffective
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000270 RID: 624
    // (get) Token: 0x06000B9D RID: 2973 RVA: 0x0037AF00 File Offset: 0x0037AF00
    // (set) Token: 0x06000B9E RID: 2974 RVA: 0x0037AF08 File Offset: 0x0037AF08
    public bool DrawingPenSoundsEnabled { get; set; }

    // Token: 0x17000271 RID: 625
    // (get) Token: 0x06000B9F RID: 2975 RVA: 0x0037AF14 File Offset: 0x0037AF14
    // (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0037AF1C File Offset: 0x0037AF1C
    public bool DrawingJavaScriptJit { get; set; }

    // Token: 0x17000272 RID: 626
    // (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0037AF28 File Offset: 0x0037AF28
    // (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0037AF30 File Offset: 0x0037AF30
    public bool? DrawingSubtitles { get; set; }

    // Token: 0x17000273 RID: 627
    // (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0037AF3C File Offset: 0x0037AF3C
    [JsonIgnore]
    public bool DrawingSubtitlesEffective
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000274 RID: 628
    // (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0037AF40 File Offset: 0x0037AF40
    // (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0037AF48 File Offset: 0x0037AF48
    public bool SaveDrawings { get; set; }

    // Token: 0x17000275 RID: 629
    // (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0037AF54 File Offset: 0x0037AF54
    [JsonIgnore]
    public bool SaveDrawingsEffective
    {
      get
      {
        return false;
      }
    }

    // Token: 0x17000276 RID: 630
    // (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0037AF58 File Offset: 0x0037AF58
    // (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0037AF60 File Offset: 0x0037AF60
    public bool ShowAgentLabelsInActivityStrip { get; set; }

    // Token: 0x17000277 RID: 631
    // (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0037AF6C File Offset: 0x0037AF6C
    // (set) Token: 0x06000BAA RID: 2986 RVA: 0x0037AF74 File Offset: 0x0037AF74
    public bool ShowInScreenRecordings { get; set; }

    // Token: 0x17000278 RID: 632
    // (get) Token: 0x06000BAB RID: 2987 RVA: 0x0037AF80 File Offset: 0x0037AF80
    // (set) Token: 0x06000BAC RID: 2988 RVA: 0x0037AF88 File Offset: 0x0037AF88
    public string AgentFolderPath { get; set; }

    // Token: 0x17000279 RID: 633
    // (get) Token: 0x06000BAD RID: 2989 RVA: 0x0037AF94 File Offset: 0x0037AF94
    // (set) Token: 0x06000BAE RID: 2990 RVA: 0x0037AF9C File Offset: 0x0037AF9C
    public string ObsidianVaultPath { get; set; }

    // Token: 0x1700027A RID: 634
    // (get) Token: 0x06000BAF RID: 2991 RVA: 0x0037AFA8 File Offset: 0x0037AFA8
    // (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0037AFB0 File Offset: 0x0037AFB0
    public bool AgentAutoApproveExtraUsage { get; set; }

    // Token: 0x1700027B RID: 635
    // (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0037AFBC File Offset: 0x0037AFBC
    // (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0037AFC4 File Offset: 0x0037AFC4
    public bool AgentAlwaysApproveFileUploads { get; set; }

    // Token: 0x1700027C RID: 636
    // (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0037AFD0 File Offset: 0x0037AFD0
    // (set) Token: 0x06000BB4 RID: 2996 RVA: 0x0037AFD8 File Offset: 0x0037AFD8
    public bool AgentCursorTextAnnouncementsEnabled { get; set; }

    // Token: 0x1700027D RID: 637
    // (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0037AFE4 File Offset: 0x0037AFE4
    // (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0037AFEC File Offset: 0x0037AFEC
    public string OneBrainModel { get; set; }

    // Token: 0x1700027E RID: 638
    // (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0037AFF8 File Offset: 0x0037AFF8
    // (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0037B000 File Offset: 0x0037B000
    public bool OneBrainReadsApps { get; set; }

    // Token: 0x1700027F RID: 639
    // (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0037B00C File Offset: 0x0037B00C
    // (set) Token: 0x06000BBA RID: 3002 RVA: 0x0037B014 File Offset: 0x0037B014
    public bool OneBrainKeepsScreensInHistory { get; set; }

    // Token: 0x17000280 RID: 640
    // (get) Token: 0x06000BBB RID: 3003 RVA: 0x0037B020 File Offset: 0x0037B020
    // (set) Token: 0x06000BBC RID: 3004 RVA: 0x0037B028 File Offset: 0x0037B028
    public bool ShowOneBrainStats { get; set; }

    // Token: 0x17000281 RID: 641
    // (get) Token: 0x06000BBD RID: 3005 RVA: 0x0037B034 File Offset: 0x0037B034
    // (set) Token: 0x06000BBE RID: 3006 RVA: 0x0037B03C File Offset: 0x0037B03C
    public bool? MorningPaperEnabled { get; set; }

    // Token: 0x17000282 RID: 642
    // (get) Token: 0x06000BBF RID: 3007 RVA: 0x0037B048 File Offset: 0x0037B048
    // (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0037B050 File Offset: 0x0037B050
    public bool StreamMode { get; set; }

    // Token: 0x17000283 RID: 643
    // (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0037B05C File Offset: 0x0037B05C
    // (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0037B064 File Offset: 0x0037B064
    [Nullable(new byte[] { 2, 1, 1 })]
    public Dictionary<string, string> DeveloperChatModels
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x17000284 RID: 644
    // (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0037B070 File Offset: 0x0037B070
    // (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0037B078 File Offset: 0x0037B078
    public bool HasShownPaywallIntroVideo { get; set; }

    // Token: 0x17000285 RID: 645
    // (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0037B084 File Offset: 0x0037B084
    [Nullable(1)]
    [JsonIgnore]
    public HotkeyBindings EffectiveHotkeys
    {
      [NullableContext(1)]
      get
      {
        return this.Hotkeys ?? HotkeyBindings.Defaults;
      }
    }

    // Token: 0x17000286 RID: 646
    // (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0037B098 File Offset: 0x0037B098
    // (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0037B0A0 File Offset: 0x0037B0A0
    public bool ProactiveAgentSuggestionsEnabled { get; set; }

    // Token: 0x17000287 RID: 647
    // (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0037B0AC File Offset: 0x0037B0AC
    // (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0037B0B4 File Offset: 0x0037B0B4
    public bool UpcomingMeetingsEnabled { get; set; }

    // Token: 0x17000288 RID: 648
    // (get) Token: 0x06000BCA RID: 3018 RVA: 0x0037B0C0 File Offset: 0x0037B0C0
    // (set) Token: 0x06000BCB RID: 3019 RVA: 0x0037B0C8 File Offset: 0x0037B0C8
    public bool OnboardingLanguageStepEnabled { get; set; }

    // Token: 0x17000289 RID: 649
    // (get) Token: 0x06000BCC RID: 3020 RVA: 0x0037B0D4 File Offset: 0x0037B0D4
    // (set) Token: 0x06000BCD RID: 3021 RVA: 0x0037B0DC File Offset: 0x0037B0DC
    public bool RemoteTasksEnabled { get; set; }

    // Token: 0x1700028A RID: 650
    // (get) Token: 0x06000BCE RID: 3022 RVA: 0x0037B0E8 File Offset: 0x0037B0E8
    // (set) Token: 0x06000BCF RID: 3023 RVA: 0x0037B0F0 File Offset: 0x0037B0F0
    public bool ProactiveDebugChordEnabled { get; set; }

    // Token: 0x1700028B RID: 651
    // (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0037B0FC File Offset: 0x0037B0FC
    // (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0037B104 File Offset: 0x0037B104
    public int ProactiveDebugCadence { get; set; }

    // Token: 0x1700028C RID: 652
    // (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0037B110 File Offset: 0x0037B110
    // (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0037B118 File Offset: 0x0037B118
    public bool? LiveNotesEnabled { get; set; }

    // Token: 0x1700028D RID: 653
    // (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0037B124 File Offset: 0x0037B124
    // (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0037B12C File Offset: 0x0037B12C
    public bool? LiveNotesStreamPatches { get; set; }

    // Token: 0x06000BD6 RID: 3030 RVA: 0x0037B138 File Offset: 0x0037B138
    public unsafe AppSettings()
    {
      int num = 2;
      List<string> list = new List<string>(num);
      CollectionsMarshal.SetCount<string>(list, num);
      Span<string> span = CollectionsMarshal.AsSpan<string>(list);
      int num2 = 0;
      *span[num2] = "HeyClicky";
      num2++;
      *span[num2] = "Claude";
      this.DictationCustomWords = list;
      this.AgentAnnouncementsVoiceEnabled = true;
      this.Onboarding = new OnboardingProgress();
      this.DrawingSoundsEnabled = true;
      this.DrawingJavaScriptJit = true;
      this.ShowInScreenRecordings = true;
      this.AgentCursorTextAnnouncementsEnabled = true;
      this.OneBrainKeepsScreensInHistory = true;
      this.ProactiveAgentSuggestionsEnabled = true;
      this.ProactiveDebugCadence = 1;
      base..ctor();
    }

    // Token: 0x040004E6 RID: 1254
    [Nullable(1)]
    public const string DefaultCursorColorHex = "#3380FF";
  }
}
