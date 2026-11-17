using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Billing;
using HeyClicky.App.Services.Updates;

namespace HeyClicky.App.Services
{
  // Token: 0x020000DD RID: 221
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class AppServices
  {
    // Token: 0x1700019E RID: 414
    // (get) Token: 0x06000A6A RID: 2666 RVA: 0x005586F4 File Offset: 0x005586F4
    // (set) Token: 0x06000A6B RID: 2667 RVA: 0x005586FC File Offset: 0x005586FC
    [RequiredMember]
    public INotchPresenter Presenter { get; set; }

    // Token: 0x1700019F RID: 415
    // (get) Token: 0x06000A6C RID: 2668 RVA: 0x00558708 File Offset: 0x00558708
    // (set) Token: 0x06000A6D RID: 2669 RVA: 0x00558710 File Offset: 0x00558710
    [RequiredMember]
    public ICompanionSession Companion { get; set; }

    // Token: 0x170001A0 RID: 416
    // (get) Token: 0x06000A6E RID: 2670 RVA: 0x0055871C File Offset: 0x0055871C
    // (set) Token: 0x06000A6F RID: 2671 RVA: 0x00558724 File Offset: 0x00558724
    [RequiredMember]
    public IAgentService Agents { get; set; }

    // Token: 0x170001A1 RID: 417
    // (get) Token: 0x06000A70 RID: 2672 RVA: 0x00558730 File Offset: 0x00558730
    // (set) Token: 0x06000A71 RID: 2673 RVA: 0x00558738 File Offset: 0x00558738
    [RequiredMember]
    public IAuthService Auth { get; set; }

    // Token: 0x170001A2 RID: 418
    // (get) Token: 0x06000A72 RID: 2674 RVA: 0x00558744 File Offset: 0x00558744
    // (set) Token: 0x06000A73 RID: 2675 RVA: 0x0055874C File Offset: 0x0055874C
    [RequiredMember]
    public IPermissionService Permissions { get; set; }

    // Token: 0x170001A3 RID: 419
    // (get) Token: 0x06000A74 RID: 2676 RVA: 0x00558758 File Offset: 0x00558758
    // (set) Token: 0x06000A75 RID: 2677 RVA: 0x00558760 File Offset: 0x00558760
    [RequiredMember]
    public IMicrophoneCapture Microphone { get; set; }

    // Token: 0x170001A4 RID: 420
    // (get) Token: 0x06000A76 RID: 2678 RVA: 0x0055876C File Offset: 0x0055876C
    // (set) Token: 0x06000A77 RID: 2679 RVA: 0x00558774 File Offset: 0x00558774
    [RequiredMember]
    public IAudioOutput Audio { get; set; }

    // Token: 0x170001A5 RID: 421
    // (get) Token: 0x06000A78 RID: 2680 RVA: 0x00558780 File Offset: 0x00558780
    // (set) Token: 0x06000A79 RID: 2681 RVA: 0x00558788 File Offset: 0x00558788
    [RequiredMember]
    public IShortcutMonitor Shortcuts { get; set; }

    // Token: 0x170001A6 RID: 422
    // (get) Token: 0x06000A7A RID: 2682 RVA: 0x00558794 File Offset: 0x00558794
    // (set) Token: 0x06000A7B RID: 2683 RVA: 0x0055879C File Offset: 0x0055879C
    [RequiredMember]
    public IScreenCaptureService Capture { get; set; }

    // Token: 0x170001A7 RID: 423
    // (get) Token: 0x06000A7C RID: 2684 RVA: 0x005587A8 File Offset: 0x005587A8
    // (set) Token: 0x06000A7D RID: 2685 RVA: 0x005587B0 File Offset: 0x005587B0
    [RequiredMember]
    public ICursorOverlay Cursor { get; set; }

    // Token: 0x170001A8 RID: 424
    // (get) Token: 0x06000A7E RID: 2686 RVA: 0x005587BC File Offset: 0x005587BC
    // (set) Token: 0x06000A7F RID: 2687 RVA: 0x005587C4 File Offset: 0x005587C4
    [RequiredMember]
    public IHighlightOverlay Highlight { get; set; }

    // Token: 0x170001A9 RID: 425
    // (get) Token: 0x06000A80 RID: 2688 RVA: 0x005587D0 File Offset: 0x005587D0
    // (set) Token: 0x06000A81 RID: 2689 RVA: 0x005587D8 File Offset: 0x005587D8
    [RequiredMember]
    public IAppSettings Settings { get; set; }

    // Token: 0x170001AA RID: 426
    // (get) Token: 0x06000A82 RID: 2690 RVA: 0x005587E4 File Offset: 0x005587E4
    // (set) Token: 0x06000A83 RID: 2691 RVA: 0x005587EC File Offset: 0x005587EC
    public IUpdateService Updates { get; set; } = NullUpdateService.Instance;

    // Token: 0x170001AB RID: 427
    // (get) Token: 0x06000A84 RID: 2692 RVA: 0x005587F8 File Offset: 0x005587F8
    // (set) Token: 0x06000A85 RID: 2693 RVA: 0x00558800 File Offset: 0x00558800
    [Nullable(2)]
    public BillingUi Billing
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000A86 RID: 2694 RVA: 0x0055880C File Offset: 0x0055880C
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public AppServices()
    {
    }
  }
}
