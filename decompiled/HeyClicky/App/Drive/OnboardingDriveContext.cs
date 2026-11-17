using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Shell;
using Windows.UI;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C3 RID: 707
  [NullableContext(1)]
  [Nullable(0)]
  [RequiredMember]
  public sealed class OnboardingDriveContext
  {
    // Token: 0x17000547 RID: 1351
    // (get) Token: 0x06001DC6 RID: 7622 RVA: 0x006026E4 File Offset: 0x006026E4
    // (set) Token: 0x06001DC7 RID: 7623 RVA: 0x006026EC File Offset: 0x006026EC
    [RequiredMember]
    public Displays.Monitor Display { get; set; }

    // Token: 0x17000548 RID: 1352
    // (get) Token: 0x06001DC8 RID: 7624 RVA: 0x006026F8 File Offset: 0x006026F8
    // (set) Token: 0x06001DC9 RID: 7625 RVA: 0x00602700 File Offset: 0x00602700
    [RequiredMember]
    public Color Accent { get; set; }

    // Token: 0x17000549 RID: 1353
    // (get) Token: 0x06001DCA RID: 7626 RVA: 0x0060270C File Offset: 0x0060270C
    // (set) Token: 0x06001DCB RID: 7627 RVA: 0x00602714 File Offset: 0x00602714
    [RequiredMember]
    public Func<string, Task> SpeakLine { get; set; }

    // Token: 0x1700054A RID: 1354
    // (get) Token: 0x06001DCC RID: 7628 RVA: 0x00602720 File Offset: 0x00602720
    // (set) Token: 0x06001DCD RID: 7629 RVA: 0x00602728 File Offset: 0x00602728
    [RequiredMember]
    public Func<int> AudioPlaybackBeganCount { get; set; }

    // Token: 0x1700054B RID: 1355
    // (get) Token: 0x06001DCE RID: 7630 RVA: 0x00602734 File Offset: 0x00602734
    // (set) Token: 0x06001DCF RID: 7631 RVA: 0x0060273C File Offset: 0x0060273C
    [RequiredMember]
    public Func<bool> IsResponding { get; set; }

    // Token: 0x1700054C RID: 1356
    // (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00602748 File Offset: 0x00602748
    // (set) Token: 0x06001DD1 RID: 7633 RVA: 0x00602750 File Offset: 0x00602750
    [Nullable(2)]
    public string FirstName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700054D RID: 1357
    // (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0060275C File Offset: 0x0060275C
    // (set) Token: 0x06001DD3 RID: 7635 RVA: 0x00602764 File Offset: 0x00602764
    [RequiredMember]
    public Action StepCompleted { get; set; }

    // Token: 0x1700054E RID: 1358
    // (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00602770 File Offset: 0x00602770
    // (set) Token: 0x06001DD5 RID: 7637 RVA: 0x00602778 File Offset: 0x00602778
    [RequiredMember]
    public Action<string, double> PlayChime { get; set; }

    // Token: 0x1700054F RID: 1359
    // (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00602784 File Offset: 0x00602784
    // (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0060278C File Offset: 0x0060278C
    [Nullable(new byte[] { 2, 1 })]
    public Action<string, float> PlayDrawingSound
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06001DD8 RID: 7640 RVA: 0x00602798 File Offset: 0x00602798
    [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
    [CompilerFeatureRequired("RequiredMembers")]
    public OnboardingDriveContext()
    {
    }
  }
}
