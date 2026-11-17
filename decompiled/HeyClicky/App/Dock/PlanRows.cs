using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002EA RID: 746
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class PlanRows : StackPanel
  {
    // Token: 0x06002030 RID: 8240 RVA: 0x006148C0 File Offset: 0x006148C0
    public PlanRows(ClickyArcPlan plan, [Nullable(2)] ClickyLiveLine line, Color ink, bool chat = false)
    {
      this.ink = ink;
      this.plan = plan;
      this.line = line;
      this.chat = chat;
      base.Spacing = (double)(chat ? 9 : 8);
      this.shownDone = PlanCascade.Done(plan);
      this.Build();
    }

    // Token: 0x06002031 RID: 8241 RVA: 0x00614928 File Offset: 0x00614928
    private void Build()
    {
      base.Children.Clear();
      this.rows.Clear();
      ClickyArcPlan clickyArcPlan = PlanCascade.Displayed(this.plan, this.shownDone);
      int? currentIndex = clickyArcPlan.CurrentIndex;
      this.firstShown = PlanCascade.FirstShown(clickyArcPlan);
      for (int i = this.firstShown; i < Math.Min(clickyArcPlan.Steps.Count, this.firstShown + 4); i++)
      {
        ClickyArcPlan.Step step = clickyArcPlan.Steps[i];
        string text = step.Text;
        bool isDone = step.IsDone;
        int num = i;
        int? num2 = currentIndex;
        PlanStepRow planStepRow = new PlanStepRow(text, isDone, (num == num2.GetValueOrDefault()) & (num2 != null), this.ink, this.chat);
        this.rows.Add(planStepRow);
        base.Children.Add(planStepRow);
        int num3 = i;
        num2 = currentIndex;
        if ((num3 == num2.GetValueOrDefault()) & (num2 != null))
        {
          ClickyLiveLine clickyLiveLine = this.line;
          if (clickyLiveLine != null)
          {
            UIElementCollection children = base.Children;
            LiveLineView liveLineView;
            if (!this.chat)
            {
              (liveLineView = new LiveLineView(clickyLiveLine, this.ink, 14.5, this.lineRisesIn)).Margin = new Thickness(31.0, -4.0, 0.0, 0.0);
            }
            else
            {
              (liveLineView = new LiveLineView(clickyLiveLine, this.ink, 11.5, this.lineRisesIn)).Margin = new Thickness(24.0, -4.0, 0.0, 0.0);
            }
            children.Add(liveLineView);
          }
        }
      }
      this.lineRisesIn = false;
    }

    // Token: 0x06002032 RID: 8242 RVA: 0x00614AD4 File Offset: 0x00614AD4
    [NullableContext(2)]
    public void Update(ClickyArcPlan next, ClickyLiveLine nextLine)
    {
      if (next == null || next.IsEmpty)
      {
        return;
      }
      bool flag = !object.Equals(this.line, nextLine);
      this.lineRisesIn = flag && nextLine != null && !ArcCardView.IsSameBatch(this.line, nextLine);
      this.line = nextLine;
      if (!this.plan.Steps.Select<ClickyArcPlan.Step, string>((ClickyArcPlan.Step s) => s.Text).SequenceEqual<string>(next.Steps.Select<ClickyArcPlan.Step, string>((ClickyArcPlan.Step s) => s.Text)))
      {
        foreach (DispatcherQueueTimer dispatcherQueueTimer in this.cascade)
        {
          dispatcherQueueTimer.Stop();
        }
        this.cascade.Clear();
        this.plan = next;
        this.shownDone = PlanCascade.Done(next);
        this.Build();
        return;
      }
      this.plan = next;
      HashSet<int> hashSet = PlanCascade.Done(next);
      foreach (DispatcherQueueTimer dispatcherQueueTimer2 in this.cascade)
      {
        dispatcherQueueTimer2.Stop();
      }
      this.cascade.Clear();
      if (!this.shownDone.IsSubsetOf(hashSet))
      {
        this.shownDone.IntersectWith(hashSet);
      }
      List<int> list = (from i in hashSet.Except<int>(this.shownDone)
        orderby i
        select i).ToList<int>();
      if (list.Count != 0)
      {
        for (int j = 0; j < list.Count; j++)
        {
          int index = list[j];
          this.cascade.Add(DockTimer.After((double)j * 0.09 + 0.001, delegate
          {
            this.shownDone.Add(index);
            this.Refresh(true);
          }));
        }
        if (flag)
        {
          this.Build();
        }
        return;
      }
      if (flag)
      {
        this.Build();
        return;
      }
      this.Refresh(false);
    }

    // Token: 0x06002033 RID: 8243 RVA: 0x00614D1C File Offset: 0x00614D1C
    private void Refresh(bool animated)
    {
      ClickyArcPlan clickyArcPlan = PlanCascade.Displayed(this.plan, this.shownDone);
      if (PlanCascade.FirstShown(clickyArcPlan) != this.firstShown)
      {
        this.Build();
        return;
      }
      int? currentIndex = clickyArcPlan.CurrentIndex;
      for (int i = 0; i < this.rows.Count; i++)
      {
        int num = this.firstShown + i;
        if (num >= clickyArcPlan.Steps.Count)
        {
          break;
        }
        PlanStepRow planStepRow = this.rows[i];
        bool isDone = clickyArcPlan.Steps[num].IsDone;
        int num2 = num;
        int? num3 = currentIndex;
        planStepRow.Set(isDone, (num2 == num3.GetValueOrDefault()) & (num3 != null), animated);
      }
      if (this.line != null)
      {
        this.Build();
      }
    }

    // Token: 0x04000F88 RID: 3976
    private readonly Color ink;

    // Token: 0x04000F89 RID: 3977
    private ClickyArcPlan plan;

    // Token: 0x04000F8A RID: 3978
    [Nullable(2)]
    private ClickyLiveLine line;

    // Token: 0x04000F8B RID: 3979
    private bool lineRisesIn;

    // Token: 0x04000F8C RID: 3980
    private HashSet<int> shownDone;

    // Token: 0x04000F8D RID: 3981
    private readonly List<PlanStepRow> rows = new List<PlanStepRow>();

    // Token: 0x04000F8E RID: 3982
    private int firstShown;

    // Token: 0x04000F8F RID: 3983
    private readonly List<DispatcherQueueTimer> cascade = new List<DispatcherQueueTimer>();

    // Token: 0x04000F90 RID: 3984
    private readonly bool chat;
  }
}
