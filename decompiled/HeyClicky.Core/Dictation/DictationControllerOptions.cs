using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001BF RID: 447
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationControllerOptions
  {
    // Token: 0x170005B7 RID: 1463
    // (get) Token: 0x060019DF RID: 6623 RVA: 0x003D4B14 File Offset: 0x003D4B14
    // (set) Token: 0x060019E0 RID: 6624 RVA: 0x003D4B1C File Offset: 0x003D4B1C
    public Func<string> UsageContext { get; set; } = () => "companion";

    // Token: 0x170005B8 RID: 1464
    // (get) Token: 0x060019E1 RID: 6625 RVA: 0x003D4B28 File Offset: 0x003D4B28
    // (set) Token: 0x060019E2 RID: 6626 RVA: 0x003D4B30 File Offset: 0x003D4B30
    public Func<bool> IsOnboardingTutorialRestricting { get; set; } = () => false;

    // Token: 0x170005B9 RID: 1465
    // (get) Token: 0x060019E3 RID: 6627 RVA: 0x003D4B3C File Offset: 0x003D4B3C
    // (set) Token: 0x060019E4 RID: 6628 RVA: 0x003D4B44 File Offset: 0x003D4B44
    public Func<bool> OnboardingTutorialAllowsPress { get; set; } = () => true;

    // Token: 0x170005BA RID: 1466
    // (get) Token: 0x060019E5 RID: 6629 RVA: 0x003D4B50 File Offset: 0x003D4B50
    // (set) Token: 0x060019E6 RID: 6630 RVA: 0x003D4B58 File Offset: 0x003D4B58
    public Func<bool> IsOnboardingTutorialDictationStepActive { get; set; } = () => false;

    // Token: 0x170005BB RID: 1467
    // (get) Token: 0x060019E7 RID: 6631 RVA: 0x003D4B64 File Offset: 0x003D4B64
    // (set) Token: 0x060019E8 RID: 6632 RVA: 0x003D4B6C File Offset: 0x003D4B6C
    public Func<string, bool> ConsumeOnboardingTutorialTranscript { get; set; } = (string _) => false;

    // Token: 0x170005BC RID: 1468
    // (get) Token: 0x060019E9 RID: 6633 RVA: 0x003D4B78 File Offset: 0x003D4B78
    // (set) Token: 0x060019EA RID: 6634 RVA: 0x003D4B80 File Offset: 0x003D4B80
    public Func<bool> CanStartRecording { get; set; } = () => true;

    // Token: 0x170005BD RID: 1469
    // (get) Token: 0x060019EB RID: 6635 RVA: 0x003D4B8C File Offset: 0x003D4B8C
    // (set) Token: 0x060019EC RID: 6636 RVA: 0x003D4B94 File Offset: 0x003D4B94
    public Func<bool> IsMicrophoneDenied { get; set; } = () => false;

    // Token: 0x170005BE RID: 1470
    // (get) Token: 0x060019ED RID: 6637 RVA: 0x003D4BA0 File Offset: 0x003D4BA0
    // (set) Token: 0x060019EE RID: 6638 RVA: 0x003D4BA8 File Offset: 0x003D4BA8
    public Action<string, float> PlayCue { get; set; } = delegate(string _, float _)
    {
    };

    // Token: 0x170005BF RID: 1471
    // (get) Token: 0x060019EF RID: 6639 RVA: 0x003D4BB4 File Offset: 0x003D4BB4
    // (set) Token: 0x060019F0 RID: 6640 RVA: 0x003D4BBC File Offset: 0x003D4BBC
    [Nullable(new byte[] { 2, 1 })]
    public Action<string, float> PlayMicrophoneStartCue
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }
  }
}
