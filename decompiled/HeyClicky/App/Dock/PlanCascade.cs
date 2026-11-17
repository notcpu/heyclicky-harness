using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032B RID: 811
  [NullableContext(1)]
  [Nullable(0)]
  public static class PlanCascade
  {
    // Token: 0x0600229B RID: 8859 RVA: 0x00626AA8 File Offset: 0x00626AA8
    public static HashSet<int> Done(ClickyArcPlan plan)
    {
      HashSet<int> hashSet = new HashSet<int>();
      foreach (int num in from i in Enumerable.Range(0, plan.Steps.Count)
        where plan.Steps[i].IsDone
        select i)
      {
        hashSet.Add(num);
      }
      return hashSet;
    }

    // Token: 0x0600229C RID: 8860 RVA: 0x00626B2C File Offset: 0x00626B2C
    public static ClickyArcPlan Displayed(ClickyArcPlan plan, [Nullable(2)] IReadOnlySet<int> shown)
    {
      if (shown == null)
      {
        return plan;
      }
      bool flag = true;
      List<ClickyArcPlan.Step> list = new List<ClickyArcPlan.Step>();
      for (int i = 0; i < plan.Steps.Count; i++)
      {
        ClickyArcPlan.Step step = plan.Steps[i];
        bool flag2 = step.IsDone && shown.Contains(i);
        list.Add(new ClickyArcPlan.Step(step.Text, flag2, step.IsNow && flag));
        flag = flag && flag2;
      }
      return new ClickyArcPlan(list);
    }

    // Token: 0x0600229D RID: 8861 RVA: 0x00626BA4 File Offset: 0x00626BA4
    public static int FirstShown(ClickyArcPlan plan)
    {
      int num = plan.CurrentIndex ?? plan.Steps.Count;
      return Math.Max(0, Math.Min(num - 1, plan.Steps.Count - 4));
    }

    // Token: 0x040010CD RID: 4301
    public const double StepDelay = 0.09;
  }
}
