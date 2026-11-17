using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000305 RID: 773
  [NullableContext(1)]
  [Nullable(0)]
  public static class DockOrder
  {
    // Token: 0x060021C4 RID: 8644 RVA: 0x0061DA38 File Offset: 0x0061DA38
    public static string JobSlotId(string jobId)
    {
      return "job-slot:" + jobId;
    }

    // Token: 0x060021C5 RID: 8645 RVA: 0x0061DA48 File Offset: 0x0061DA48
    public static IReadOnlyList<DockWindowKey> NewestFirst(IReadOnlyList<string> clickySlugs, IReadOnlyList<ClickyJob> jobs, Func<string, DockRecency> recency)
    {
      List<ValueTuple<DockWindowKey, DockRecency, int>> list = new List<ValueTuple<DockWindowKey, DockRecency, int>>();
      HashSet<string> hashSet = new HashSet<string>();
      foreach (string text in clickySlugs)
      {
        string text2 = "clicky:" + text;
        if (hashSet.Add(text2))
        {
          list.Add(new ValueTuple<DockWindowKey, DockRecency, int>(new DockWindowKey(text, text2, text, null), recency(text), list.Count));
        }
      }
      foreach (ClickyJob clickyJob in jobs)
      {
        string text3 = DockOrder.JobSlotId(clickyJob.Id);
        if (hashSet.Add(text3))
        {
          list.Add(new ValueTuple<DockWindowKey, DockRecency, int>(new DockWindowKey(clickyJob.RailKey, text3, null, clickyJob), recency(clickyJob.RailKey), list.Count));
        }
      }
      list.Sort(delegate([TupleElementNames(new string[] { "Window", "Recency", "Order" })] ValueTuple<DockWindowKey, DockRecency, int> a, [TupleElementNames(new string[] { "Window", "Recency", "Order" })] ValueTuple<DockWindowKey, DockRecency, int> b)
      {
        if (!(a.Item2 != b.Item2))
        {
          return a.Item3.CompareTo(b.Item3);
        }
        return b.Item2.CompareTo(a.Item2);
      });
      return list.Select<ValueTuple<DockWindowKey, DockRecency, int>, DockWindowKey>(([TupleElementNames(new string[] { "Window", "Recency", "Order" })] ValueTuple<DockWindowKey, DockRecency, int> w) => w.Item1).ToList<DockWindowKey>();
    }

    // Token: 0x060021C6 RID: 8646 RVA: 0x0061DB9C File Offset: 0x0061DB9C
    public static IReadOnlyList<DockWindowKey> DeckOrder(IReadOnlyList<string> clickySlugs, IReadOnlyList<ClickyJob> jobs, Func<string, DockRecency> recency)
    {
      List<DockWindowKey> list = DockOrder.NewestFirst(clickySlugs, jobs, recency).ToList<DockWindowKey>();
      list.Reverse();
      return list;
    }

    // Token: 0x060021C7 RID: 8647 RVA: 0x0061DBB4 File Offset: 0x0061DBB4
    [return: Nullable(2)]
    public static string NextFrontKeyAfterRemoving(IReadOnlyList<string> deckKeys, string removed)
    {
      int num = deckKeys.ToList<string>().IndexOf(removed);
      if (num < 0)
      {
        return null;
      }
      if (num + 1 < deckKeys.Count)
      {
        return deckKeys[num + 1];
      }
      if (num <= 0)
      {
        return null;
      }
      return deckKeys[num - 1];
    }
  }
}
