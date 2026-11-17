using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008D RID: 141
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UiInvoker
  {
    // Token: 0x0600088A RID: 2186 RVA: 0x00369D14 File Offset: 0x00369D14
    [NullableContext(2)]
    public UiInvoker(SynchronizationContext context)
    {
    }

    // Token: 0x170001E3 RID: 483
    // (get) Token: 0x0600088B RID: 2187 RVA: 0x00369D24 File Offset: 0x00369D24
    public bool IsInline
    {
      get
      {
        return this.<context>P == null || SynchronizationContext.Current == this.<context>P;
      }
    }

    // Token: 0x0600088C RID: 2188 RVA: 0x00369D40 File Offset: 0x00369D40
    public Task<T> InvokeAsync<[Nullable(2)] T>(Func<T> work)
    {
      if (this.IsInline)
      {
        try
        {
          return Task.FromResult<T>(work());
        }
        catch (Exception ex)
        {
          return Task.FromException<T>(ex);
        }
      }
      TaskCompletionSource<T> completion = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
      this.<context>P.Post(delegate(object _)
      {
        try
        {
          completion.SetResult(work());
        }
        catch (Exception ex2)
        {
          completion.SetException(ex2);
        }
      }, null);
      return completion.Task;
    }

    // Token: 0x0600088D RID: 2189 RVA: 0x00369DC0 File Offset: 0x00369DC0
    public Task InvokeAsync(Action work)
    {
      return this.InvokeAsync<bool>(delegate
      {
        work();
        return true;
      });
    }

    // Token: 0x0600088E RID: 2190 RVA: 0x00369DEC File Offset: 0x00369DEC
    public void Post(Action work)
    {
      if (this.IsInline)
      {
        try
        {
          work();
        }
        catch (Exception ex)
        {
          Log.Error("Ui", "UI call failed", ex);
        }
        return;
      }
      this.<context>P.Post(delegate(object _)
      {
        try
        {
          work();
        }
        catch (Exception ex2)
        {
          Log.Error("Ui", "UI call failed", ex2);
        }
      }, null);
    }

    // Token: 0x0400040D RID: 1037
    [Nullable(2)]
    [CompilerGenerated]
    private SynchronizationContext <context>P = context;
  }
}
