using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x0200002E RID: 46
  public sealed class RoutineWakeGate
  {
    // Token: 0x1700012E RID: 302
    // (get) Token: 0x060004B7 RID: 1207 RVA: 0x000EDC5C File Offset: 0x000EDC5C
    // (set) Token: 0x060004B8 RID: 1208 RVA: 0x000EDC64 File Offset: 0x000EDC64
    public bool IsDisplayOn { get; private set; } = true;

    // Token: 0x1700012F RID: 303
    // (get) Token: 0x060004B9 RID: 1209 RVA: 0x000EDC70 File Offset: 0x000EDC70
    // (set) Token: 0x060004BA RID: 1210 RVA: 0x000EDC78 File Offset: 0x000EDC78
    public bool DisplayOffMeansStandby { get; set; }

    // Token: 0x060004BB RID: 1211 RVA: 0x000EDC84 File Offset: 0x000EDC84
    public void NoteSleeping()
    {
      using (this.gate.EnterScope())
      {
        this.awaitingDisplaySinceSleep = true;
        this.awaitingFullWakeSinceSleep = true;
        this.displayOnFirstObservedAt = null;
      }
    }

    // Token: 0x060004BC RID: 1212 RVA: 0x000EDCD4 File Offset: 0x000EDCD4
    public bool NoteDisplay(bool isOn)
    {
      bool isDisplayOn;
      using (this.gate.EnterScope())
      {
        isDisplayOn = this.IsDisplayOn;
        this.IsDisplayOn = isOn;
      }
      if (!isOn)
      {
        if (isDisplayOn && this.DisplayOffMeansStandby)
        {
          this.NoteSleeping();
        }
        return false;
      }
      bool flag2;
      using (this.gate.EnterScope())
      {
        bool flag = this.awaitingDisplaySinceSleep;
        this.awaitingDisplaySinceSleep = false;
        flag2 = flag;
      }
      return flag2;
    }

    // Token: 0x17000130 RID: 304
    // (get) Token: 0x060004BD RID: 1213 RVA: 0x000EDD5C File Offset: 0x000EDD5C
    public bool IsInDarkWake
    {
      get
      {
        bool flag;
        using (this.gate.EnterScope())
        {
          flag = this.awaitingDisplaySinceSleep && !this.IsDisplayOn;
        }
        return flag;
      }
    }

    // Token: 0x060004BE RID: 1214 RVA: 0x000EDDA8 File Offset: 0x000EDDA8
    public bool ConsumeFullWake()
    {
      bool flag;
      using (this.gate.EnterScope())
      {
        if (!this.awaitingFullWakeSinceSleep)
        {
          flag = false;
        }
        else
        {
          this.awaitingFullWakeSinceSleep = false;
          this.displayOnFirstObservedAt = null;
          flag = true;
        }
      }
      return flag;
    }

    // Token: 0x060004BF RID: 1215 RVA: 0x000EDE00 File Offset: 0x000EDE00
    public bool ShouldHoldForFullWake(DateTimeOffset now, TimeSpan failOpenAfter)
    {
      bool flag;
      using (this.gate.EnterScope())
      {
        if (!this.awaitingFullWakeSinceSleep)
        {
          flag = false;
        }
        else if (!this.IsDisplayOn)
        {
          flag = true;
        }
        else
        {
          DateTimeOffset? dateTimeOffset = this.displayOnFirstObservedAt;
          if (dateTimeOffset != null)
          {
            DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
            if (now - valueOrDefault < failOpenAfter)
            {
              flag = true;
            }
            else
            {
              this.awaitingFullWakeSinceSleep = false;
              this.displayOnFirstObservedAt = null;
              flag = false;
            }
          }
          else
          {
            this.displayOnFirstObservedAt = new DateTimeOffset?(now);
            flag = true;
          }
        }
      }
      return flag;
    }

    // Token: 0x040001BF RID: 447
    [Nullable(1)]
    private readonly Lock gate = new Lock();

    // Token: 0x040001C0 RID: 448
    private bool awaitingDisplaySinceSleep;

    // Token: 0x040001C1 RID: 449
    private bool awaitingFullWakeSinceSleep;

    // Token: 0x040001C2 RID: 450
    private DateTimeOffset? displayOnFirstObservedAt;
  }
}
