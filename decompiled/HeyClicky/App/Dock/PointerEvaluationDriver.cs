using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using HeyClicky.App.Shell;
using HeyClicky.Platform.Input;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000338 RID: 824
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class PointerEvaluationDriver
  {
    // Token: 0x17000675 RID: 1653
    // (get) Token: 0x0600230F RID: 8975 RVA: 0x0062AC08 File Offset: 0x0062AC08
    // (set) Token: 0x06002310 RID: 8976 RVA: 0x0062AC10 File Offset: 0x0062AC10
    public bool IsRunning { get; private set; }

    // Token: 0x06002311 RID: 8977 RVA: 0x0062AC1C File Offset: 0x0062AC1C
    public PointerEvaluationDriver(double maximumPointerRateHz, double slowCheckIntervalSeconds, Action evaluation)
    {
      this.maximumPointerRateHz = maximumPointerRateHz;
      this.evaluation = evaluation;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.slowCheckTimer = this.dq.CreateTimer();
      this.slowCheckTimer.Interval = TimeSpan.FromSeconds(slowCheckIntervalSeconds);
      this.slowCheckTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.RunEvaluation();
      };
      this.deadlineTimer = this.dq.CreateTimer();
      this.deadlineTimer.IsRepeating = false;
      this.deadlineTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.scheduledDeadline = null;
        this.RunEvaluation();
      };
      this.soonTimer = this.dq.CreateTimer();
      this.soonTimer.IsRepeating = false;
      this.soonTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.isEvaluationScheduled = false;
        this.RunEvaluation();
      };
    }

    // Token: 0x17000676 RID: 1654
    // (get) Token: 0x06002312 RID: 8978 RVA: 0x0062ACEC File Offset: 0x0062ACEC
    public static double Now
    {
      get
      {
        return (double)Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;
      }
    }

    // Token: 0x06002313 RID: 8979 RVA: 0x0062ACFC File Offset: 0x0062ACFC
    public void Start()
    {
      if (this.IsRunning)
      {
        return;
      }
      this.IsRunning = true;
      if (!OverlayWindow.Offscreen)
      {
        this.pointerFeed = new PointerMoveFeed(this.maximumPointerRateHz, delegate
        {
          if (Interlocked.Exchange(ref this.isMoveQueued, 1) == 0)
          {
            this.dq.TryEnqueue(delegate
            {
              Volatile.Write(ref this.isMoveQueued, 0);
              this.RunEvaluation();
            });
          }
        });
      }
      this.slowCheckTimer.Start();
      this.EvaluateSoon();
    }

    // Token: 0x06002314 RID: 8980 RVA: 0x0062AD50 File Offset: 0x0062AD50
    public void Stop()
    {
      this.IsRunning = false;
      PointerMoveFeed pointerMoveFeed = this.pointerFeed;
      if (pointerMoveFeed != null)
      {
        pointerMoveFeed.Dispose();
      }
      this.pointerFeed = null;
      this.slowCheckTimer.Stop();
      this.deadlineTimer.Stop();
      this.soonTimer.Stop();
      this.isEvaluationScheduled = false;
      this.scheduledDeadline = null;
    }

    // Token: 0x06002315 RID: 8981 RVA: 0x0062ADB0 File Offset: 0x0062ADB0
    public void EvaluateSoon()
    {
      if (!this.IsRunning || this.isEvaluationScheduled)
      {
        return;
      }
      this.isEvaluationScheduled = true;
      double num = Math.Max(0.0, this.lastEvaluationTime + 1.0 / this.maximumPointerRateHz - PointerEvaluationDriver.Now);
      if (num <= 0.0)
      {
        this.dq.TryEnqueue(DispatcherQueuePriority.Low, delegate
        {
          if (!this.isEvaluationScheduled)
          {
            return;
          }
          this.isEvaluationScheduled = false;
          this.RunEvaluation();
        });
        return;
      }
      this.soonTimer.Interval = TimeSpan.FromSeconds(num);
      this.soonTimer.Start();
    }

    // Token: 0x06002316 RID: 8982 RVA: 0x0062AE44 File Offset: 0x0062AE44
    public void EvaluateAt(double? deadline)
    {
      double? num = deadline;
      double? num2 = this.scheduledDeadline;
      if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
      {
        return;
      }
      this.deadlineTimer.Stop();
      this.scheduledDeadline = deadline;
      if (this.IsRunning && deadline != null)
      {
        double valueOrDefault = deadline.GetValueOrDefault();
        this.deadlineTimer.Interval = TimeSpan.FromSeconds(Math.Max(0.0, valueOrDefault - PointerEvaluationDriver.Now));
        this.deadlineTimer.Start();
        return;
      }
    }

    // Token: 0x06002317 RID: 8983 RVA: 0x0062AEDC File Offset: 0x0062AEDC
    private void RunEvaluation()
    {
      if (!this.IsRunning)
      {
        return;
      }
      this.lastEvaluationTime = PointerEvaluationDriver.Now;
      this.evaluation();
    }

    // Token: 0x04001118 RID: 4376
    private readonly double maximumPointerRateHz;

    // Token: 0x04001119 RID: 4377
    private readonly Action evaluation;

    // Token: 0x0400111A RID: 4378
    private readonly DispatcherQueue dq;

    // Token: 0x0400111B RID: 4379
    private readonly DispatcherQueueTimer slowCheckTimer;

    // Token: 0x0400111C RID: 4380
    private readonly DispatcherQueueTimer deadlineTimer;

    // Token: 0x0400111D RID: 4381
    private readonly DispatcherQueueTimer soonTimer;

    // Token: 0x0400111E RID: 4382
    [Nullable(2)]
    private PointerMoveFeed pointerFeed;

    // Token: 0x0400111F RID: 4383
    private double? scheduledDeadline;

    // Token: 0x04001120 RID: 4384
    private bool isEvaluationScheduled;

    // Token: 0x04001121 RID: 4385
    private double lastEvaluationTime;

    // Token: 0x04001122 RID: 4386
    private int isMoveQueued;
  }
}
