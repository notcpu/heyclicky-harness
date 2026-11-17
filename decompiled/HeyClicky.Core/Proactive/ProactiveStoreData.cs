using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000DD RID: 221
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveStoreData
  {
    // Token: 0x1700033A RID: 826
    // (get) Token: 0x06000E64 RID: 3684 RVA: 0x0038B148 File Offset: 0x0038B148
    // (set) Token: 0x06000E65 RID: 3685 RVA: 0x0038B150 File Offset: 0x0038B150
    public Dictionary<string, ProactiveAccountState> Accounts { get; set; } = new Dictionary<string, ProactiveAccountState>();

    // Token: 0x1700033B RID: 827
    // (get) Token: 0x06000E66 RID: 3686 RVA: 0x0038B15C File Offset: 0x0038B15C
    // (set) Token: 0x06000E67 RID: 3687 RVA: 0x0038B164 File Offset: 0x0038B164
    public List<ProactiveHistoryEntry> History { get; set; } = new List<ProactiveHistoryEntry>();

    // Token: 0x1700033C RID: 828
    // (get) Token: 0x06000E68 RID: 3688 RVA: 0x0038B170 File Offset: 0x0038B170
    // (set) Token: 0x06000E69 RID: 3689 RVA: 0x0038B178 File Offset: 0x0038B178
    [Nullable(2)]
    public string MorningGreetingLastDay
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700033D RID: 829
    // (get) Token: 0x06000E6A RID: 3690 RVA: 0x0038B184 File Offset: 0x0038B184
    // (set) Token: 0x06000E6B RID: 3691 RVA: 0x0038B18C File Offset: 0x0038B18C
    public DateTimeOffset? MorningGreetingLastPresentedAt { get; set; }

    // Token: 0x1700033E RID: 830
    // (get) Token: 0x06000E6C RID: 3692 RVA: 0x0038B198 File Offset: 0x0038B198
    // (set) Token: 0x06000E6D RID: 3693 RVA: 0x0038B1A0 File Offset: 0x0038B1A0
    public DateTimeOffset? MorningSettledAt { get; set; }

    // Token: 0x1700033F RID: 831
    // (get) Token: 0x06000E6E RID: 3694 RVA: 0x0038B1AC File Offset: 0x0038B1AC
    // (set) Token: 0x06000E6F RID: 3695 RVA: 0x0038B1B4 File Offset: 0x0038B1B4
    public MorningDismissalHistory MorningDismissals { get; set; } = MorningDismissalHistory.Empty;

    // Token: 0x17000340 RID: 832
    // (get) Token: 0x06000E70 RID: 3696 RVA: 0x0038B1C0 File Offset: 0x0038B1C0
    // (set) Token: 0x06000E71 RID: 3697 RVA: 0x0038B1C8 File Offset: 0x0038B1C8
    public DateTimeOffset? OnboardingFirstCompletedAt { get; set; }

    // Token: 0x17000341 RID: 833
    // (get) Token: 0x06000E72 RID: 3698 RVA: 0x0038B1D4 File Offset: 0x0038B1D4
    // (set) Token: 0x06000E73 RID: 3699 RVA: 0x0038B1DC File Offset: 0x0038B1DC
    public Dictionary<string, DateTimeOffset> IntegrationSnoozedUntil { get; set; } = new Dictionary<string, DateTimeOffset>();

    // Token: 0x17000342 RID: 834
    // (get) Token: 0x06000E74 RID: 3700 RVA: 0x0038B1E8 File Offset: 0x0038B1E8
    // (set) Token: 0x06000E75 RID: 3701 RVA: 0x0038B1F0 File Offset: 0x0038B1F0
    public List<string> IntegrationNeverAsk { get; set; } = new List<string>();

    // Token: 0x17000343 RID: 835
    // (get) Token: 0x06000E76 RID: 3702 RVA: 0x0038B1FC File Offset: 0x0038B1FC
    // (set) Token: 0x06000E77 RID: 3703 RVA: 0x0038B204 File Offset: 0x0038B204
    public List<DateTimeOffset> IntegrationPromptTimestamps { get; set; } = new List<DateTimeOffset>();

    // Token: 0x17000344 RID: 836
    // (get) Token: 0x06000E78 RID: 3704 RVA: 0x0038B210 File Offset: 0x0038B210
    // (set) Token: 0x06000E79 RID: 3705 RVA: 0x0038B218 File Offset: 0x0038B218
    public bool SpotifyVoiceTipShown { get; set; }
  }
}
