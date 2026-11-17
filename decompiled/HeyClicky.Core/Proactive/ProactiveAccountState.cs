using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000DC RID: 220
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ProactiveAccountState
  {
    // Token: 0x17000333 RID: 819
    // (get) Token: 0x06000E55 RID: 3669 RVA: 0x0038B06C File Offset: 0x0038B06C
    // (set) Token: 0x06000E56 RID: 3670 RVA: 0x0038B074 File Offset: 0x0038B074
    public List<ProactiveSuggestionCard> Cards { get; set; } = new List<ProactiveSuggestionCard>();

    // Token: 0x17000334 RID: 820
    // (get) Token: 0x06000E57 RID: 3671 RVA: 0x0038B080 File Offset: 0x0038B080
    // (set) Token: 0x06000E58 RID: 3672 RVA: 0x0038B088 File Offset: 0x0038B088
    public List<QueuedProactiveTask> Queue { get; set; } = new List<QueuedProactiveTask>();

    // Token: 0x17000335 RID: 821
    // (get) Token: 0x06000E59 RID: 3673 RVA: 0x0038B094 File Offset: 0x0038B094
    // (set) Token: 0x06000E5A RID: 3674 RVA: 0x0038B09C File Offset: 0x0038B09C
    [Nullable(2)]
    public PendingSuggestionJob PendingJob
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000336 RID: 822
    // (get) Token: 0x06000E5B RID: 3675 RVA: 0x0038B0A8 File Offset: 0x0038B0A8
    // (set) Token: 0x06000E5C RID: 3676 RVA: 0x0038B0B0 File Offset: 0x0038B0B0
    public ProactiveSlotDayState Slots { get; set; } = new ProactiveSlotDayState(null, null, null);

    // Token: 0x17000337 RID: 823
    // (get) Token: 0x06000E5D RID: 3677 RVA: 0x0038B0BC File Offset: 0x0038B0BC
    // (set) Token: 0x06000E5E RID: 3678 RVA: 0x0038B0C4 File Offset: 0x0038B0C4
    public DateTimeOffset? IgnoredSuggestionsBreakUntil { get; set; }

    // Token: 0x17000338 RID: 824
    // (get) Token: 0x06000E5F RID: 3679 RVA: 0x0038B0D0 File Offset: 0x0038B0D0
    // (set) Token: 0x06000E60 RID: 3680 RVA: 0x0038B0D8 File Offset: 0x0038B0D8
    public DateTimeOffset? SuggestionsLastFetchedAt { get; set; }

    // Token: 0x17000339 RID: 825
    // (get) Token: 0x06000E61 RID: 3681 RVA: 0x0038B0E4 File Offset: 0x0038B0E4
    // (set) Token: 0x06000E62 RID: 3682 RVA: 0x0038B0EC File Offset: 0x0038B0EC
    public DateTimeOffset? SuggestionsRowLastSeenAt { get; set; }
  }
}
