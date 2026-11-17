using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000219 RID: 537
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkDashboardRows
  {
    // Token: 0x0600195D RID: 6493 RVA: 0x005D1734 File Offset: 0x005D1734
    public static ClickyInkDashboardSplit.Rows For(ClickyInkLiveNote note, ClickyHomeSpreadPaper paper)
    {
      string text = ClickyInkDashboardSplit.Key(note);
      ClickyInkDashboardSplit.Rows rows;
      if (ClickyInkDashboardRows.Cache.TryGetValue(text, out rows))
      {
        return rows;
      }
      double clock = note.FinishedClock + 30.0;
      ClickyInkStyle style = new ClickyInkStyle(paper.Ink, paper.Quiet, new Color?(paper.Deep), note.LatestEdit(clock));
      ClickyInkDashboardSplit.Rows rows2 = ClickyInkDashboardSplit.Plan(note, (ClickyInkGroup group) => ClickyInkFormKit.MeasureHeight(ClickyInkGroupView.Make(group, clock, style, paper, new ClickyInkActions()), 378.0) + 12.0);
      ClickyInkDashboardRows.Cache[text] = rows2;
      if (ClickyInkDashboardRows.Cache.Count > 100)
      {
        ClickyInkDashboardRows.Cache.Clear();
      }
      return rows2;
    }

    // Token: 0x04000C59 RID: 3161
    private static readonly Dictionary<string, ClickyInkDashboardSplit.Rows> Cache = new Dictionary<string, ClickyInkDashboardSplit.Rows>();
  }
}
