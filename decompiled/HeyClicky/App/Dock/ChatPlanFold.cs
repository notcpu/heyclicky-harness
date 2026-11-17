using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032C RID: 812
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ChatPlanFold : Grid
  {
    // Token: 0x0600229E RID: 8862 RVA: 0x00626BF4 File Offset: 0x00626BF4
    public ChatPlanFold(ClickyArcPlan plan, HashSet<int> doneBefore, string line, Color ink, Color wash, Action onFinished)
    {
      this.plan = plan;
      this.doneBefore = doneBefore;
      this.onFinished = onFinished;
      base.MaxWidth = 420.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.RequestedTheme = ElementTheme.Light;
      base.Children.Add(ChatBlockViews.BubblePlate(Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(15526893U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(16250871U, 1.0))
      }), null, true, false, 17.0, 7.0));
      base.Children.Add(ChatBlockViews.BubblePlate(Ui.Brush(wash.WithAlpha(0.45)), null, true, false, 17.0, 7.0));
      for (int i = 0; i < plan.Steps.Count; i++)
      {
        PlanStepRow planStepRow = new PlanStepRow(plan.Steps[i].Text, doneBefore.Contains(i), false, ink, true);
        this.rows.Add(planStepRow);
        this.ticking.Children.Add(planStepRow);
      }
      this.answer = Ui.Text(line, 12.5, FontWeights.SemiBold, ink.WithAlpha(0.9), TextAlignment.Left, 3, false, false);
      this.answer.Opacity = 0.0;
      this.answer.Visibility = Visibility.Collapsed;
      Grid grid = new Grid
      {
        Padding = new Thickness(14.0, 12.0, 14.0, 19.0),
        Children = { this.ticking, this.answer }
      };
      base.Children.Add(grid);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Play();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        foreach (DispatcherQueueTimer dispatcherQueueTimer in this.timers)
        {
          dispatcherQueueTimer.Stop();
        }
        this.timers.Clear();
      };
    }

    // Token: 0x0600229F RID: 8863 RVA: 0x00626E50 File Offset: 0x00626E50
    private void After(double seconds, Action action)
    {
      this.timers.Add(DockTimer.After(seconds, action));
    }

    // Token: 0x060022A0 RID: 8864 RVA: 0x00626E64 File Offset: 0x00626E64
    private void Play()
    {
      if (this.timers.Count > 0)
      {
        return;
      }
      List<int> list = (from i in Enumerable.Range(0, this.plan.Steps.Count)
        where !this.doneBefore.Contains(i)
        select i).ToList<int>();
      for (int j = 0; j < list.Count; j++)
      {
        int index = list[j];
        this.After((double)j * 0.09 + 0.001, delegate
        {
          this.rows[index].Set(true, false, true);
        });
      }
      double num = 0.35 + (double)list.Count * 0.09 + 0.35;
      this.After(num, delegate
      {
        SwiftSpring swiftSpring = new SwiftSpring(0.42, 0.82);
        Anim.OpacitySpring(this.ticking, 0f, swiftSpring);
        Anim.TranslationSpring(this.ticking, new Vector3(0f, -6f, 0f), swiftSpring);
        this.answer.Visibility = Visibility.Visible;
        this.answer.Translation = new Vector3(0f, 8f, 0f);
        Anim.OpacitySpring(this.answer, 1f, swiftSpring);
        Anim.TranslationSpring(this.answer, Vector3.Zero, swiftSpring);
        this.After(0.45, delegate
        {
          this.ticking.Visibility = Visibility.Collapsed;
        });
      });
      this.After(num + 1.35, delegate
      {
        SwiftSpring swiftSpring2 = new SwiftSpring(0.42, 0.86);
        base.CenterPoint = new Vector3((float)base.ActualWidth / 2f, 0f, 0f);
        Anim.ScaleSpring(this, 0.97f, swiftSpring2, null);
        Anim.OpacitySpring(this, 0f, swiftSpring2);
      });
      this.After(num + 1.35 + 0.12, this.onFinished);
    }

    // Token: 0x040010CE RID: 4302
    private readonly ClickyArcPlan plan;

    // Token: 0x040010CF RID: 4303
    private readonly HashSet<int> doneBefore;

    // Token: 0x040010D0 RID: 4304
    private readonly Action onFinished;

    // Token: 0x040010D1 RID: 4305
    private readonly List<PlanStepRow> rows = new List<PlanStepRow>();

    // Token: 0x040010D2 RID: 4306
    private readonly StackPanel ticking = new StackPanel
    {
      Spacing = 9.0
    };

    // Token: 0x040010D3 RID: 4307
    private readonly TextBlock answer;

    // Token: 0x040010D4 RID: 4308
    private readonly List<DispatcherQueueTimer> timers = new List<DispatcherQueueTimer>();
  }
}
