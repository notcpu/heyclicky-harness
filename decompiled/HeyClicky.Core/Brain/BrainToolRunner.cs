using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F4 RID: 500
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainToolRunner
  {
    // Token: 0x06001CF8 RID: 7416 RVA: 0x003E55D0 File Offset: 0x003E55D0
    public BrainToolRunner(Func<BrainToolCall, Task<string>> run)
    {
      this.run = run;
    }

    // Token: 0x06001CF9 RID: 7417 RVA: 0x003E5600 File Offset: 0x003E5600
    public void Start(BrainToolCall call)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (!this.tasks.ContainsKey(call.CallId))
        {
          this.startOrder.Add(call);
          Task<string> task;
          if (BrainToolCallPolicy.RunsInParallel(call.Name))
          {
            task = this.SafeRun(call);
          }
          else
          {
            Task<string> task2 = this.lastOrderedTask;
            task = this.RunAfter(task2, call);
            this.lastOrderedTask = task;
          }
          this.tasks[call.CallId] = task;
        }
      }
    }

    // Token: 0x06001CFA RID: 7418 RVA: 0x003E569C File Offset: 0x003E569C
    private async Task<string> RunAfter([Nullable(new byte[] { 2, 1 })] Task<string> previous, BrainToolCall call)
    {
      if (previous != null)
      {
        await previous.ConfigureAwait(false);
      }
      return await this.SafeRun(call).ConfigureAwait(false);
    }

    // Token: 0x06001CFB RID: 7419 RVA: 0x003E56F0 File Offset: 0x003E56F0
    private async Task<string> SafeRun(BrainToolCall call)
    {
      string text;
      try
      {
        text = await this.run(call).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        text = ToolOutput.Error(ex.Message);
      }
      return text;
    }

    // Token: 0x06001CFC RID: 7420 RVA: 0x003E573C File Offset: 0x003E573C
    public bool HasStarted(string callId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.tasks.ContainsKey(callId);
      }
      return flag2;
    }

    // Token: 0x06001CFD RID: 7421 RVA: 0x003E5784 File Offset: 0x003E5784
    public async Task<List<string>> OutputsAsync(IReadOnlyList<BrainToolCall> calls)
    {
      foreach (BrainToolCall brainToolCall in calls)
      {
        this.Start(brainToolCall);
      }
      List<string> outputs = new List<string>(calls.Count);
      foreach (BrainToolCall brainToolCall2 in calls)
      {
        object obj = this.gate;
        Task<string> task;
        lock (obj)
        {
          this.tasks.TryGetValue(brainToolCall2.CallId, out task);
        }
        List<string> list = outputs;
        string text;
        if (task == null)
        {
          text = "{\"status\":\"error\",\"message\":\"the call never ran\"}";
        }
        else
        {
          text = await task.ConfigureAwait(false);
        }
        list.Add(text);
        list = null;
      }
      IEnumerator<BrainToolCall> enumerator2 = null;
      return outputs;
    }

    // Token: 0x06001CFE RID: 7422 RVA: 0x003E57D0 File Offset: 0x003E57D0
    [return: TupleElementNames(new string[] { "Call", "Output" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public List<ValueTuple<BrainToolCall, string>> FinishedCallsWithOutputs()
    {
      object obj = this.gate;
      List<ValueTuple<BrainToolCall, string>> list;
      lock (obj)
      {
        list = (from c in this.startOrder
          where this.tasks[c.CallId].IsCompletedSuccessfully
          select new ValueTuple<BrainToolCall, string>(c, this.tasks[c.CallId].Result)).ToList<ValueTuple<BrainToolCall, string>>();
      }
      return list;
    }

    // Token: 0x04000C6F RID: 3183
    private readonly Func<BrainToolCall, Task<string>> run;

    // Token: 0x04000C70 RID: 3184
    private readonly Dictionary<string, Task<string>> tasks = new Dictionary<string, Task<string>>();

    // Token: 0x04000C71 RID: 3185
    private readonly List<BrainToolCall> startOrder = new List<BrainToolCall>();

    // Token: 0x04000C72 RID: 3186
    private readonly object gate = new object();

    // Token: 0x04000C73 RID: 3187
    [Nullable(new byte[] { 2, 1 })]
    private Task<string> lastOrderedTask;
  }
}
