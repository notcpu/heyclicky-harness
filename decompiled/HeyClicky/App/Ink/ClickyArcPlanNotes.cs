using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200019C RID: 412
  [NullableContext(2)]
  [Nullable(0)]
  public static class ClickyArcPlanNotes
  {
    // Token: 0x0600165A RID: 5722 RVA: 0x005BA658 File Offset: 0x005BA658
    public static ClickyArcPlan Read([Nullable(1)] ClickyLiveSteps steps, string notePath)
    {
      if (notePath == null)
      {
        return null;
      }
      ClickyArcPlan clickyArcPlan = steps.Plan(notePath);
      if (clickyArcPlan != null && !clickyArcPlan.IsEmpty)
      {
        return clickyArcPlan;
      }
      string text = ClickyInkPlanReading.LatestText(notePath);
      if (text != null)
      {
        ClickyArcPlan clickyArcPlan2 = ClickyArcPlanNotes.ReadNoteText(text);
        if (clickyArcPlan2 != null)
        {
          steps.RememberLastPlan(clickyArcPlan2, notePath);
          return clickyArcPlan2;
        }
      }
      return null;
    }

    // Token: 0x0600165B RID: 5723 RVA: 0x005BA6A0 File Offset: 0x005BA6A0
    [NullableContext(1)]
    [return: Nullable(2)]
    public static ClickyArcPlan ReadNoteText(string noteText)
    {
      IReadOnlyList<ValueTuple<string, bool>> readOnlyList = ClickyInkPlanReading.ChecklistSteps(noteText);
      if (readOnlyList == null)
      {
        return null;
      }
      return new ClickyArcPlan(readOnlyList.Select<ValueTuple<string, bool>, ClickyArcPlan.Step>(([TupleElementNames(new string[] { "Text", "IsDone" })] ValueTuple<string, bool> i) => new ClickyArcPlan.Step(i.Item1, i.Item2, false)).ToList<ClickyArcPlan.Step>());
    }

    // Token: 0x0600165C RID: 5724 RVA: 0x005BA6E8 File Offset: 0x005BA6E8
    public static ClickyArcPlan FinalizingFor([Nullable(1)] ClickyLiveSteps steps, string key)
    {
      if (key == null)
      {
        return null;
      }
      ClickyArcPlan clickyArcPlan = steps.LastPlan(key) ?? ClickyArcPlanNotes.Read(steps, key);
      if (clickyArcPlan != null && !clickyArcPlan.IsEmpty)
      {
        return clickyArcPlan.Finalizing;
      }
      return null;
    }

    // Token: 0x0600165D RID: 5725 RVA: 0x005BA720 File Offset: 0x005BA720
    public static string NoteTitle(string notePath)
    {
      return ClickyInkPlanReading.NoteTitle(notePath);
    }
  }
}
