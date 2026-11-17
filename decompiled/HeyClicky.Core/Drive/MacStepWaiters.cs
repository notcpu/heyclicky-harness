using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000192 RID: 402
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MacStepWaiters
  {
    // Token: 0x060017CC RID: 6092 RVA: 0x003C5948 File Offset: 0x003C5948
    [NullableContext(2)]
    public MacStepWaiters(Func<DateTimeOffset> clock = null)
    {
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = MacStepWaiters.<>c.<>9__7_0) == null)
      {
        func = (MacStepWaiters.<>c.<>9__7_0 = () => DateTimeOffset.UtcNow);
      }
      this.now = func;
    }

    // Token: 0x060017CD RID: 6093 RVA: 0x003C59BC File Offset: 0x003C59BC
    public bool IsWaiting(string taskId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.waiting.ContainsKey(taskId);
      }
      return flag2;
    }

    // Token: 0x060017CE RID: 6094 RVA: 0x003C5A04 File Offset: 0x003C5A04
    public int WaitingCallCount(string taskId)
    {
      object obj = this.gate;
      int num;
      lock (obj)
      {
        MacStepWaiters.Waiting waiting;
        num = (this.waiting.TryGetValue(taskId, out waiting) ? waiting.Calls.Count : 0);
      }
      return num;
    }

    // Token: 0x060017CF RID: 6095 RVA: 0x003C5A60 File Offset: 0x003C5A60
    [return: Nullable(2)]
    public string Step(string taskId)
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        MacStepWaiters.Waiting waiting;
        text = (this.waiting.TryGetValue(taskId, out waiting) ? waiting.Step : null);
      }
      return text;
    }

    // Token: 0x060017D0 RID: 6096 RVA: 0x003C5AB8 File Offset: 0x003C5AB8
    public void MarkDriving(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.driving.Add(taskId);
      }
    }

    // Token: 0x060017D1 RID: 6097 RVA: 0x003C5B00 File Offset: 0x003C5B00
    public void EndDrive(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.driving.Remove(taskId);
      }
    }

    // Token: 0x060017D2 RID: 6098 RVA: 0x003C5B48 File Offset: 0x003C5B48
    public bool HasStepAsked(TimeSpan within)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.waiting.Values.Any<MacStepWaiters.Waiting>((MacStepWaiters.Waiting w) => this.now() - w.AskedAt < within);
      }
      return flag2;
    }

    // Token: 0x060017D3 RID: 6099 RVA: 0x003C5BB4 File Offset: 0x003C5BB4
    public void RememberUnanswered(string taskId, string request)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.unanswered[taskId] = request;
      }
    }

    // Token: 0x060017D4 RID: 6100 RVA: 0x003C5BFC File Offset: 0x003C5BFC
    public void ForgetUnanswered(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.unanswered.Remove(taskId);
      }
    }

    // Token: 0x060017D5 RID: 6101 RVA: 0x003C5C44 File Offset: 0x003C5C44
    public void RememberDeclined(string taskId, string step)
    {
      string text = step.Trim();
      if (text.Length == 0)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        List<ValueTuple<string, DateTimeOffset>> list;
        if (!this.declinedSteps.TryGetValue(taskId, out list))
        {
          list = (this.declinedSteps[taskId] = new List<ValueTuple<string, DateTimeOffset>>());
        }
        list.Add(new ValueTuple<string, DateTimeOffset>(text, this.now()));
      }
    }

    // Token: 0x060017D6 RID: 6102 RVA: 0x003C5CC8 File Offset: 0x003C5CC8
    public void ForgetDeclined(string taskId)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.declinedSteps.Remove(taskId);
      }
    }

    // Token: 0x060017D7 RID: 6103 RVA: 0x003C5D10 File Offset: 0x003C5D10
    public bool WasRecentlyDeclined(string taskId, string step)
    {
      string trimmed = step.Trim();
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        List<ValueTuple<string, DateTimeOffset>> list;
        if (!this.declinedSteps.TryGetValue(taskId, out list))
        {
          flag2 = false;
        }
        else
        {
          list.RemoveAll(([TupleElementNames(new string[] { "Step", "At" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, DateTimeOffset> d) => this.now() - d.Item2 >= MacStepWaiters.DeclinedStepMemory);
          if (list.Count == 0)
          {
            this.declinedSteps.Remove(taskId);
            flag2 = false;
          }
          else
          {
            flag2 = trimmed.Length > 0 && list.Any<ValueTuple<string, DateTimeOffset>>(([TupleElementNames(new string[] { "Step", "At" })] ValueTuple<string, DateTimeOffset> d) => d.Item1 == trimmed || AgentDriveReports.DriveGoalSharesWordsWithStep(d.Item1, trimmed));
          }
        }
      }
      return flag2;
    }

    // Token: 0x060017D8 RID: 6104 RVA: 0x003C5DCC File Offset: 0x003C5DCC
    [return: Nullable(2)]
    public string TakeUnansweredStep(string taskId)
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        string text2;
        text = (this.unanswered.Remove(taskId, out text2) ? text2 : null);
      }
      return text;
    }

    // Token: 0x060017D9 RID: 6105 RVA: 0x003C5E1C File Offset: 0x003C5E1C
    public Task<MacStepAnswer> WaitAsync(string taskId, string step, TimeSpan openCall, Func<bool> stillRunning, CancellationToken ct = default(CancellationToken))
    {
      TaskCompletionSource<MacStepAnswer> taskCompletionSource = new TaskCompletionSource<MacStepAnswer>(TaskCreationOptions.RunContinuationsAsynchronously);
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        MacStepWaiters.Waiting waiting;
        flag2 = !this.waiting.TryGetValue(taskId, out waiting);
        if (flag2)
        {
          waiting = (this.waiting[taskId] = new MacStepWaiters.Waiting(step, this.now()));
        }
        waiting.Calls.Add(taskCompletionSource);
      }
      if (flag2)
      {
        this.WatchAsync(taskId, openCall, stillRunning, ct);
      }
      return taskCompletionSource.Task;
    }

    // Token: 0x060017DA RID: 6106 RVA: 0x003C5EB8 File Offset: 0x003C5EB8
    private async Task WatchAsync(string taskId, TimeSpan openCall, Func<bool> stillRunning, CancellationToken ct)
    {
      DateTimeOffset deadline = this.now() + openCall;
      object obj;
      for (;;)
      {
        obj = this.gate;
        lock (obj)
        {
          if (!this.waiting.ContainsKey(taskId))
          {
            return;
          }
        }
        bool flag2;
        try
        {
          flag2 = stillRunning();
        }
        catch
        {
          flag2 = false;
        }
        if (!flag2 || ct.IsCancellationRequested)
        {
          break;
        }
        if (this.now() > deadline)
        {
          goto Block_4;
        }
        try
        {
          await Task.Delay(TimeSpan.FromMilliseconds(Math.Min(2000.0, Math.Max(50.0, openCall.TotalMilliseconds / 4.0))), CancellationToken.None).ConfigureAwait(false);
          continue;
        }
        catch
        {
        }
        return;
      }
      this.Resolve(taskId, MacStepAnswer.NoAnswerAnswer);
      return;
      Block_4:
      obj = this.gate;
      bool flag3;
      lock (obj)
      {
        flag3 = this.driving.Contains(taskId);
      }
      this.Resolve(taskId, flag3 ? MacStepAnswer.BeingDrivenAnswer : MacStepAnswer.NotYetAnswer);
    }

    // Token: 0x060017DB RID: 6107 RVA: 0x003C5F1C File Offset: 0x003C5F1C
    public void Resolve(string taskId, MacStepAnswer answer)
    {
      object obj = this.gate;
      List<TaskCompletionSource<MacStepAnswer>> calls;
      lock (obj)
      {
        if (!(answer is MacStepAnswer.BeingDriven))
        {
          this.driving.Remove(taskId);
        }
        MacStepWaiters.Waiting waiting;
        if (!this.waiting.Remove(taskId, out waiting))
        {
          return;
        }
        calls = waiting.Calls;
      }
      foreach (TaskCompletionSource<MacStepAnswer> taskCompletionSource in calls)
      {
        taskCompletionSource.TrySetResult(answer);
      }
    }

    // Token: 0x04000A41 RID: 2625
    public static readonly MacStepWaiters Shared = new MacStepWaiters(null);

    // Token: 0x04000A42 RID: 2626
    private readonly object gate = new object();

    // Token: 0x04000A43 RID: 2627
    private readonly Dictionary<string, MacStepWaiters.Waiting> waiting = new Dictionary<string, MacStepWaiters.Waiting>();

    // Token: 0x04000A44 RID: 2628
    private readonly HashSet<string> driving = new HashSet<string>();

    // Token: 0x04000A45 RID: 2629
    private readonly Dictionary<string, string> unanswered = new Dictionary<string, string>();

    // Token: 0x04000A46 RID: 2630
    private readonly Func<DateTimeOffset> now;

    // Token: 0x04000A47 RID: 2631
    public static readonly TimeSpan DeclinedStepMemory = TimeSpan.FromMinutes(30L);

    // Token: 0x04000A48 RID: 2632
    [TupleElementNames(new string[] { "Step", "At" })]
    [Nullable(new byte[] { 1, 1, 1, 0, 1 })]
    private readonly Dictionary<string, List<ValueTuple<string, DateTimeOffset>>> declinedSteps = new Dictionary<string, List<ValueTuple<string, DateTimeOffset>>>();

    // Token: 0x02000574 RID: 1396
    [Nullable(0)]
    private sealed class Waiting
    {
      // Token: 0x06003520 RID: 13600 RVA: 0x00441064 File Offset: 0x00441064
      public Waiting(string step, DateTimeOffset askedAt)
      {
      }

      // Token: 0x0400207C RID: 8316
      public readonly string Step = step;

      // Token: 0x0400207D RID: 8317
      public readonly DateTimeOffset AskedAt = askedAt;

      // Token: 0x0400207E RID: 8318
      public readonly List<TaskCompletionSource<MacStepAnswer>> Calls = new List<TaskCompletionSource<MacStepAnswer>>();
    }
  }
}
