using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Core.Proactive;

namespace HeyClicky.App.Proactive
{
  // Token: 0x020000F4 RID: 244
  [NullableContext(2)]
  [Nullable(0)]
  public static class ProactiveHooks
  {
    // Token: 0x06000CC9 RID: 3273 RVA: 0x00563424 File Offset: 0x00563424
    public static ISet<Guid> LiveCardIds()
    {
      ISet<Guid> set2;
      try
      {
        ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
        ISet<Guid> set;
        if (proactiveCoordinator == null)
        {
          set = null;
        }
        else
        {
          set = proactiveCoordinator.VisibleCards.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id).ToHashSet<Guid>();
        }
        set2 = set;
      }
      catch (InvalidOperationException)
      {
        set2 = null;
      }
      return set2;
    }

    // Token: 0x06000CCA RID: 3274 RVA: 0x00563484 File Offset: 0x00563484
    [NullableContext(1)]
    [return: Nullable(new byte[] { 1, 2 })]
    public static Task<T> RunOnUiAsync<T>(Func<ProactiveCoordinator, T> work) where T : class
    {
      ProactiveCoordinator c = ProactiveCoordinator.Current;
      if (c == null)
      {
        return Task.FromResult<T>(default(T));
      }
      TaskCompletionSource<T> done = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
      if (!c.Dispatcher.TryEnqueue(delegate
      {
        try
        {
          done.TrySetResult(work(c));
        }
        catch (Exception ex)
        {
          done.TrySetException(ex);
        }
      }))
      {
        done.TrySetResult(default(T));
      }
      return done.Task;
    }

    // Token: 0x040004A6 RID: 1190
    public static volatile Func<string> LiveStateNote;

    // Token: 0x040004A7 RID: 1191
    [Nullable(new byte[] { 2, 1 })]
    public static volatile Func<ProactiveJobLaunch, bool> LaunchApprovedCardAsJob;
  }
}
