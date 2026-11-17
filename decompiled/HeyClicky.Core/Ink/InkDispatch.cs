using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000188 RID: 392
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkDispatch
  {
    // Token: 0x0600176B RID: 5995 RVA: 0x003C3A14 File Offset: 0x003C3A14
    public static void After(double seconds, Action action)
    {
      SynchronizationContext context = SynchronizationContext.Current;
      Task.Delay(TimeSpan.FromSeconds(Math.Max(0.0, seconds))).ContinueWith(delegate(Task _)
      {
        InkDispatch.Post(context, action);
      }, TaskScheduler.Default);
    }

    // Token: 0x0600176C RID: 5996 RVA: 0x003C3A6C File Offset: 0x003C3A6C
    private static void Post([Nullable(2)] SynchronizationContext context, Action action)
    {
      InkDispatch.<>c__DisplayClass1_0 CS$<>8__locals1 = new InkDispatch.<>c__DisplayClass1_0();
      CS$<>8__locals1.action = action;
      if (context == null)
      {
        CS$<>8__locals1.<Post>g__Run|0();
        return;
      }
      context.Post(delegate(object _)
      {
        base.<Post>g__Run|0();
      }, null);
    }

    // Token: 0x02000566 RID: 1382
    [Nullable(0)]
    public sealed class RepeatingTimer : IDisposable
    {
      // Token: 0x060034C3 RID: 13507 RVA: 0x00440898 File Offset: 0x00440898
      public RepeatingTimer(double intervalSeconds, Action tick)
      {
        this.context = SynchronizationContext.Current;
        this.tick = tick;
        TimeSpan timeSpan = TimeSpan.FromSeconds(intervalSeconds);
        this.timer = new Timer(delegate(object _)
        {
          this.Fire();
        }, null, timeSpan, timeSpan);
      }

      // Token: 0x060034C4 RID: 13508 RVA: 0x004408E0 File Offset: 0x004408E0
      private void Fire()
      {
        if (Interlocked.Exchange(ref this.queued, 1) == 1)
        {
          return;
        }
        InkDispatch.Post(this.context, delegate
        {
          try
          {
            this.tick();
          }
          finally
          {
            Interlocked.Exchange(ref this.queued, 0);
          }
        });
      }

      // Token: 0x060034C5 RID: 13509 RVA: 0x0044090C File Offset: 0x0044090C
      public void Dispose()
      {
        this.timer.Dispose();
      }

      // Token: 0x0400206A RID: 8298
      private readonly Timer timer;

      // Token: 0x0400206B RID: 8299
      [Nullable(2)]
      private readonly SynchronizationContext context;

      // Token: 0x0400206C RID: 8300
      private readonly Action tick;

      // Token: 0x0400206D RID: 8301
      private int queued;
    }

    // Token: 0x02000567 RID: 1383
    [Nullable(0)]
    public sealed class SerialQueue
    {
      // Token: 0x060034C8 RID: 13512 RVA: 0x0044095C File Offset: 0x0044095C
      public void Async(Action work)
      {
        object obj = this.gate;
        lock (obj)
        {
          this.last = this.last.ContinueWith(delegate(Task _)
          {
            try
            {
              work();
            }
            catch (Exception ex)
            {
              Log.Warn("ink", "a background ink write failed: " + ex.Message);
            }
          }, TaskScheduler.Default);
        }
      }

      // Token: 0x060034C9 RID: 13513 RVA: 0x004409C8 File Offset: 0x004409C8
      public void Sync()
      {
        object obj = this.gate;
        Task task;
        lock (obj)
        {
          task = this.last;
        }
        task.Wait();
      }

      // Token: 0x0400206E RID: 8302
      private readonly object gate = new object();

      // Token: 0x0400206F RID: 8303
      private Task last = Task.CompletedTask;
    }
  }
}
