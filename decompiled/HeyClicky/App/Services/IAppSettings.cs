using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Services
{
  // Token: 0x020000DF RID: 223
  [NullableContext(1)]
  public interface IAppSettings
  {
    // Token: 0x14000026 RID: 38
    // (add) Token: 0x06000A96 RID: 2710
    // (remove) Token: 0x06000A97 RID: 2711
    [Nullable(2)]
    event EventHandler Changed;

    // Token: 0x170001AD RID: 429
    // (get) Token: 0x06000A98 RID: 2712
    // (set) Token: 0x06000A99 RID: 2713
    string CursorColorHex { get; set; }

    // Token: 0x170001AE RID: 430
    // (get) Token: 0x06000A9A RID: 2714
    // (set) Token: 0x06000A9B RID: 2715
    bool LaunchAtLogin { get; set; }

    // Token: 0x170001AF RID: 431
    // (get) Token: 0x06000A9C RID: 2716
    // (set) Token: 0x06000A9D RID: 2717
    bool ScreenHighlightEnabled { get; set; }

    // Token: 0x170001B0 RID: 432
    // (get) Token: 0x06000A9E RID: 2718
    // (set) Token: 0x06000A9F RID: 2719
    bool LibraryModeEnabled { get; set; }

    // Token: 0x170001B1 RID: 433
    // (get) Token: 0x06000AA0 RID: 2720
    // (set) Token: 0x06000AA1 RID: 2721
    bool IsPaused { get; set; }

    // Token: 0x170001B2 RID: 434
    // (get) Token: 0x06000AA2 RID: 2722
    // (set) Token: 0x06000AA3 RID: 2723
    bool HasCompletedIntro { get; set; }

    // Token: 0x170001B3 RID: 435
    // (get) Token: 0x06000AA4 RID: 2724
    // (set) Token: 0x06000AA5 RID: 2725
    bool HasCompletedOnboarding { get; set; }

    // Token: 0x170001B4 RID: 436
    // (get) Token: 0x06000AA6 RID: 2726
    // (set) Token: 0x06000AA7 RID: 2727
    bool HasCompletedTutorial { get; set; }

    // Token: 0x170001B5 RID: 437
    // (get) Token: 0x06000AA8 RID: 2728
    // (set) Token: 0x06000AA9 RID: 2729
    [Nullable(2)]
    string SquadPack
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001B6 RID: 438
    // (get) Token: 0x06000AAA RID: 2730
    // (set) Token: 0x06000AAB RID: 2731
    [Nullable(2)]
    string DiscoveryChannel
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001B7 RID: 439
    // (get) Token: 0x06000AAC RID: 2732
    string PushToTalkDisplay { get; }

    // Token: 0x170001B8 RID: 440
    // (get) Token: 0x06000AAD RID: 2733 RVA: 0x00558820 File Offset: 0x00558820
    HotkeyBinding PushToTalkBinding
    {
      get
      {
        return HotkeyBinding.PushToTalkDefault;
      }
    }

    // Token: 0x170001B9 RID: 441
    // (get) Token: 0x06000AAE RID: 2734 RVA: 0x00558828 File Offset: 0x00558828
    HotkeyBinding DictationBinding
    {
      get
      {
        return HotkeyBinding.DictationDefault;
      }
    }

    // Token: 0x170001BA RID: 442
    // (get) Token: 0x06000AAF RID: 2735
    // (set) Token: 0x06000AB0 RID: 2736
    bool DictationAutoDetect { get; set; }

    // Token: 0x170001BB RID: 443
    // (get) Token: 0x06000AB1 RID: 2737
    // (set) Token: 0x06000AB2 RID: 2738
    string DictationSpecificLanguageCode { get; set; }

    // Token: 0x170001BC RID: 444
    // (get) Token: 0x06000AB3 RID: 2739
    // (set) Token: 0x06000AB4 RID: 2740
    string DictationCustomWordsText { get; set; }

    // Token: 0x170001BD RID: 445
    // (get) Token: 0x06000AB5 RID: 2741
    string TextModeDisplay { get; }

    // Token: 0x170001BE RID: 446
    // (get) Token: 0x06000AB6 RID: 2742
    // (set) Token: 0x06000AB7 RID: 2743
    bool CursorDocked { get; set; }

    // Token: 0x170001BF RID: 447
    // (get) Token: 0x06000AB8 RID: 2744
    // (set) Token: 0x06000AB9 RID: 2745
    bool ShakeToDetachEnabled { get; set; }
  }
}
