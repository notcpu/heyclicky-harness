using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000EE RID: 238
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class OnboardingProgress
  {
    // Token: 0x1700036A RID: 874
    // (get) Token: 0x06000F2F RID: 3887 RVA: 0x0038E2CC File Offset: 0x0038E2CC
    // (set) Token: 0x06000F30 RID: 3888 RVA: 0x0038E2D4 File Offset: 0x0038E2D4
    public bool PreSignInIntroDismissed { get; set; }

    // Token: 0x1700036B RID: 875
    // (get) Token: 0x06000F31 RID: 3889 RVA: 0x0038E2E0 File Offset: 0x0038E2E0
    // (set) Token: 0x06000F32 RID: 3890 RVA: 0x0038E2E8 File Offset: 0x0038E2E8
    public bool HasAcknowledgedPermissionIntro { get; set; }

    // Token: 0x1700036C RID: 876
    // (get) Token: 0x06000F33 RID: 3891 RVA: 0x0038E2F4 File Offset: 0x0038E2F4
    // (set) Token: 0x06000F34 RID: 3892 RVA: 0x0038E2FC File Offset: 0x0038E2FC
    public bool HasCompletedOnboarding { get; set; }

    // Token: 0x1700036D RID: 877
    // (get) Token: 0x06000F35 RID: 3893 RVA: 0x0038E308 File Offset: 0x0038E308
    // (set) Token: 0x06000F36 RID: 3894 RVA: 0x0038E310 File Offset: 0x0038E310
    public double? TutorialCompletedAtEpochSeconds { get; set; }

    // Token: 0x1700036E RID: 878
    // (get) Token: 0x06000F37 RID: 3895 RVA: 0x0038E31C File Offset: 0x0038E31C
    // (set) Token: 0x06000F38 RID: 3896 RVA: 0x0038E324 File Offset: 0x0038E324
    public string DiscoveryChannel { get; set; }

    // Token: 0x1700036F RID: 879
    // (get) Token: 0x06000F39 RID: 3897 RVA: 0x0038E330 File Offset: 0x0038E330
    // (set) Token: 0x06000F3A RID: 3898 RVA: 0x0038E338 File Offset: 0x0038E338
    public string DiscoveryChannelOther { get; set; }

    // Token: 0x17000370 RID: 880
    // (get) Token: 0x06000F3B RID: 3899 RVA: 0x0038E344 File Offset: 0x0038E344
    // (set) Token: 0x06000F3C RID: 3900 RVA: 0x0038E34C File Offset: 0x0038E34C
    public bool DiscoveryChannelSynced { get; set; }

    // Token: 0x17000371 RID: 881
    // (get) Token: 0x06000F3D RID: 3901 RVA: 0x0038E358 File Offset: 0x0038E358
    // (set) Token: 0x06000F3E RID: 3902 RVA: 0x0038E360 File Offset: 0x0038E360
    public string SquadPackId { get; set; }
  }
}
