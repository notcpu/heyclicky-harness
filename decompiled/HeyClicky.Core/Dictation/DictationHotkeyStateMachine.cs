using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C5 RID: 453
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationHotkeyStateMachine
  {
    // Token: 0x06001A3A RID: 6714 RVA: 0x003D633C File Offset: 0x003D633C
    public DictationHotkeyStateMachine(IDictationScheduler scheduler, Func<bool> isSessionActive, Action begin, Action end, Action lockedHandsFree)
    {
      this.scheduler = scheduler;
      this.isSessionActive = isSessionActive;
      this.begin = begin;
      this.end = end;
      this.lockedHandsFree = lockedHandsFree;
    }

    // Token: 0x170005CD RID: 1485
    // (get) Token: 0x06001A3B RID: 6715 RVA: 0x003D639C File Offset: 0x003D639C
    // (set) Token: 0x06001A3C RID: 6716 RVA: 0x003D63A4 File Offset: 0x003D63A4
    public DictationEngagement Engagement { get; private set; }

    // Token: 0x170005CE RID: 1486
    // (get) Token: 0x06001A3D RID: 6717 RVA: 0x003D63B0 File Offset: 0x003D63B0
    // (set) Token: 0x06001A3E RID: 6718 RVA: 0x003D63B8 File Offset: 0x003D63B8
    public bool SessionUsedHandsFree { get; private set; }

    // Token: 0x170005CF RID: 1487
    // (get) Token: 0x06001A3F RID: 6719 RVA: 0x003D63C4 File Offset: 0x003D63C4
    // (set) Token: 0x06001A40 RID: 6720 RVA: 0x003D63CC File Offset: 0x003D63CC
    public bool IsDedicatedHandsFreeShortcutConfigured { get; set; }

    // Token: 0x170005D0 RID: 1488
    // (get) Token: 0x06001A41 RID: 6721 RVA: 0x003D63D8 File Offset: 0x003D63D8
    // (set) Token: 0x06001A42 RID: 6722 RVA: 0x003D63E0 File Offset: 0x003D63E0
    public Func<bool> IsOnboardingTutorialActive { get; set; } = () => false;

    // Token: 0x170005D1 RID: 1489
    // (get) Token: 0x06001A43 RID: 6723 RVA: 0x003D63EC File Offset: 0x003D63EC
    public bool HasPendingQuickTapFinish
    {
      get
      {
        return this.pendingQuickTapFinish != null;
      }
    }

    // Token: 0x170005D2 RID: 1490
    // (get) Token: 0x06001A44 RID: 6724 RVA: 0x003D63FC File Offset: 0x003D63FC
    public bool HasPendingBounceRelease
    {
      get
      {
        return this.pendingBounceRelease != null;
      }
    }

    // Token: 0x06001A45 RID: 6725 RVA: 0x003D640C File Offset: 0x003D640C
    public void Pressed()
    {
      if (this.pendingBounceRelease != null)
      {
        this.pendingBounceRelease.Dispose();
        this.pendingBounceRelease = null;
        Log.Info("Dictation", "key bounce ignored; still holding");
        return;
      }
      if (this.pendingQuickTapFinish != null)
      {
        this.pendingQuickTapFinish.Dispose();
        this.pendingQuickTapFinish = null;
        this.holdPressStartTime = null;
        this.Engagement = DictationEngagement.HandsFreeLocked;
        this.SessionUsedHandsFree = true;
        this.lockedHandsFree();
        return;
      }
      if (this.Engagement == DictationEngagement.HandsFreeLocked)
      {
        this.Engagement = DictationEngagement.Idle;
        this.shouldIgnoreNextReleaseAfterLockStop = true;
        this.end();
        return;
      }
      if (this.Engagement != DictationEngagement.Idle)
      {
        return;
      }
      bool flag = this.isSessionActive();
      this.begin();
      if (this.isSessionActive() && !flag)
      {
        this.Engagement = DictationEngagement.Holding;
        this.SessionUsedHandsFree = false;
        this.holdPressStartTime = new DateTimeOffset?(this.scheduler.Now);
      }
    }

    // Token: 0x06001A46 RID: 6726 RVA: 0x003D64FC File Offset: 0x003D64FC
    public void Released()
    {
      if (this.shouldIgnoreNextReleaseAfterLockStop)
      {
        this.shouldIgnoreNextReleaseAfterLockStop = false;
        return;
      }
      if (this.Engagement != DictationEngagement.Holding)
      {
        return;
      }
      DateTimeOffset? dateTimeOffset = this.holdPressStartTime;
      TimeSpan timeSpan;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        timeSpan = this.scheduler.Now - valueOrDefault;
      }
      else
      {
        timeSpan = DictationHotkeyStateMachine.QuickTapMaxHold + TimeSpan.FromSeconds(1L);
      }
      TimeSpan timeSpan2 = timeSpan;
      if (timeSpan2 < DictationHotkeyStateMachine.KeyBounceWindow && this.pendingBounceRelease == null)
      {
        this.pendingBounceRelease = this.scheduler.Schedule(DictationHotkeyStateMachine.KeyBounceWindow, delegate
        {
          if (this.pendingBounceRelease == null)
          {
            return;
          }
          this.pendingBounceRelease = null;
          this.Released();
        });
        return;
      }
      this.holdPressStartTime = null;
      if (timeSpan2 >= DictationHotkeyStateMachine.QuickTapMaxHold || this.IsDedicatedHandsFreeShortcutConfigured || this.IsOnboardingTutorialActive())
      {
        this.Engagement = DictationEngagement.Idle;
        this.end();
        return;
      }
      this.pendingQuickTapFinish = this.scheduler.Schedule(DictationHotkeyStateMachine.LockDoubleTapMaxGap, delegate
      {
        if (this.pendingQuickTapFinish == null)
        {
          return;
        }
        this.pendingQuickTapFinish = null;
        this.Engagement = DictationEngagement.Idle;
        this.end();
      });
    }

    // Token: 0x06001A47 RID: 6727 RVA: 0x003D6600 File Offset: 0x003D6600
    public void ToggleHandsFree()
    {
      switch (this.Engagement)
      {
      case DictationEngagement.Idle:
      {
        bool flag = this.isSessionActive();
        this.begin();
        if (this.isSessionActive() && !flag)
        {
          this.Engagement = DictationEngagement.HandsFreeLocked;
          this.SessionUsedHandsFree = true;
          this.lockedHandsFree();
        }
        return;
      }
      case DictationEngagement.Holding:
      {
        IDisposable disposable = this.pendingQuickTapFinish;
        if (disposable != null)
        {
          disposable.Dispose();
        }
        this.pendingQuickTapFinish = null;
        IDisposable disposable2 = this.pendingBounceRelease;
        if (disposable2 != null)
        {
          disposable2.Dispose();
        }
        this.pendingBounceRelease = null;
        this.holdPressStartTime = null;
        this.Engagement = DictationEngagement.HandsFreeLocked;
        this.SessionUsedHandsFree = true;
        this.lockedHandsFree();
        return;
      }
      case DictationEngagement.HandsFreeLocked:
        this.Engagement = DictationEngagement.Idle;
        this.end();
        return;
      default:
        return;
      }
    }

    // Token: 0x06001A48 RID: 6728 RVA: 0x003D66D0 File Offset: 0x003D66D0
    public void Reset()
    {
      IDisposable disposable = this.pendingQuickTapFinish;
      if (disposable != null)
      {
        disposable.Dispose();
      }
      this.pendingQuickTapFinish = null;
      IDisposable disposable2 = this.pendingBounceRelease;
      if (disposable2 != null)
      {
        disposable2.Dispose();
      }
      this.pendingBounceRelease = null;
      this.holdPressStartTime = null;
      this.shouldIgnoreNextReleaseAfterLockStop = false;
      this.Engagement = DictationEngagement.Idle;
    }

    // Token: 0x04000B3F RID: 2879
    public static readonly TimeSpan QuickTapMaxHold = TimeSpan.FromSeconds(0.4);

    // Token: 0x04000B40 RID: 2880
    public static readonly TimeSpan LockDoubleTapMaxGap = TimeSpan.FromSeconds(0.4);

    // Token: 0x04000B41 RID: 2881
    public static readonly TimeSpan KeyBounceWindow = TimeSpan.FromSeconds(0.08);

    // Token: 0x04000B42 RID: 2882
    private readonly IDictationScheduler scheduler;

    // Token: 0x04000B43 RID: 2883
    private readonly Func<bool> isSessionActive;

    // Token: 0x04000B44 RID: 2884
    private readonly Action begin;

    // Token: 0x04000B45 RID: 2885
    private readonly Action end;

    // Token: 0x04000B46 RID: 2886
    private readonly Action lockedHandsFree;

    // Token: 0x04000B47 RID: 2887
    private DateTimeOffset? holdPressStartTime;

    // Token: 0x04000B48 RID: 2888
    [Nullable(2)]
    private IDisposable pendingQuickTapFinish;

    // Token: 0x04000B49 RID: 2889
    [Nullable(2)]
    private IDisposable pendingBounceRelease;

    // Token: 0x04000B4A RID: 2890
    private bool shouldIgnoreNextReleaseAfterLockStop;
  }
}
