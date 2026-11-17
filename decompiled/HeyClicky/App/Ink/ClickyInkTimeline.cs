using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A8 RID: 680
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkTimeline : Grid
  {
    // Token: 0x06001BE4 RID: 7140 RVA: 0x005F6EB0 File Offset: 0x005F6EB0
    public ClickyInkTimeline(Func<DateTimeOffset, FrameworkElement> build, TimeSpan? minimumInterval)
    {
      this.build = build;
      this.dispatcher = DispatcherQueue.GetForCurrentThread();
      this.interval = minimumInterval;
      this.Draw();
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.isLoaded = true;
        this.Schedule();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.isLoaded = false;
        this.StopTimer();
      };
    }

    // Token: 0x06001BE5 RID: 7141 RVA: 0x005F6F08 File Offset: 0x005F6F08
    public static TimeSpan? Interval(double? seconds)
    {
      if (seconds != null)
      {
        double valueOrDefault = seconds.GetValueOrDefault();
        return new TimeSpan?(TimeSpan.FromSeconds(valueOrDefault));
      }
      return null;
    }

    // Token: 0x170004CC RID: 1228
    // (get) Token: 0x06001BE6 RID: 7142 RVA: 0x005F6F40 File Offset: 0x005F6F40
    // (set) Token: 0x06001BE7 RID: 7143 RVA: 0x005F6F48 File Offset: 0x005F6F48
    public TimeSpan? MinimumInterval
    {
      get
      {
        return this.interval;
      }
      set
      {
        if (this.interval == value)
        {
          return;
        }
        this.interval = value;
        this.Draw();
        this.Schedule();
      }
    }

    // Token: 0x06001BE8 RID: 7144 RVA: 0x005F6FA8 File Offset: 0x005F6FA8
    public void Refresh()
    {
      this.Draw();
    }

    // Token: 0x06001BE9 RID: 7145 RVA: 0x005F6FB0 File Offset: 0x005F6FB0
    private void Draw()
    {
      DateTimeOffset now = DateTimeOffset.Now;
      IClickyInkClocked clickyInkClocked = this.shown as IClickyInkClocked;
      if (clickyInkClocked != null)
      {
        clickyInkClocked.Render(now);
        return;
      }
      FrameworkElement frameworkElement = this.build(now);
      if (frameworkElement == this.shown)
      {
        return;
      }
      base.Children.Clear();
      base.Children.Add(frameworkElement);
      this.shown = frameworkElement;
    }

    // Token: 0x06001BEA RID: 7146 RVA: 0x005F7010 File Offset: 0x005F7010
    private void Schedule()
    {
      if (this.isLoaded)
      {
        TimeSpan? timeSpan = this.interval;
        if (timeSpan != null && !Anim.ReduceMotion)
        {
          TimeSpan value = this.interval.Value;
          if (this.timer == null)
          {
            this.timer = this.dispatcher.CreateTimer();
            this.timer.IsRepeating = true;
            this.timer.Tick += delegate(DispatcherQueueTimer _, object _)
            {
              this.Draw();
            };
          }
          this.timer.Interval = value;
          if (!this.timer.IsRunning)
          {
            this.timer.Start();
          }
          return;
        }
      }
      this.StopTimer();
    }

    // Token: 0x06001BEB RID: 7147 RVA: 0x005F70B0 File Offset: 0x005F70B0
    private void StopTimer()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.timer;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x04000D66 RID: 3430
    private readonly Func<DateTimeOffset, FrameworkElement> build;

    // Token: 0x04000D67 RID: 3431
    private readonly DispatcherQueue dispatcher;

    // Token: 0x04000D68 RID: 3432
    [Nullable(2)]
    private DispatcherQueueTimer timer;

    // Token: 0x04000D69 RID: 3433
    [Nullable(2)]
    private FrameworkElement shown;

    // Token: 0x04000D6A RID: 3434
    private TimeSpan? interval;

    // Token: 0x04000D6B RID: 3435
    private bool isLoaded;
  }
}
