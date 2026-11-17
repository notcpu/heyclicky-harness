using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000333 RID: 819
  [NullableContext(1)]
  [Nullable(0)]
  internal static class ClickyReadMarks
  {
    // Token: 0x17000670 RID: 1648
    // (get) Token: 0x060022F0 RID: 8944 RVA: 0x00629714 File Offset: 0x00629714
    private static Dictionary<string, DateTimeOffset> Marks
    {
      get
      {
        Dictionary<string, DateTimeOffset> dictionary;
        if ((dictionary = ClickyReadMarks.marks) == null)
        {
          dictionary = (ClickyReadMarks.marks = AgentDefaults.Get<Dictionary<string, DateTimeOffset>>("clicky.dock.clickyLastSeen.v1") ?? new Dictionary<string, DateTimeOffset>());
        }
        return dictionary;
      }
    }

    // Token: 0x060022F1 RID: 8945 RVA: 0x00629738 File Offset: 0x00629738
    public static IReadOnlyList<string> Unread(AgentService agents)
    {
      List<string> list = new List<string>();
      bool flag = false;
      foreach (ClickyInfo clickyInfo in agents.Clickys)
      {
        DateTimeOffset dateTimeOffset = (from w in agents.ClickyApps.Works(clickyInfo.Slug)
          select w.FinishedAt).DefaultIfEmpty(DateTimeOffset.MinValue).Max<DateTimeOffset>();
        DateTimeOffset? finishedAt = agents.LiveStatusForClicky(clickyInfo.Slug).FinishedAt;
        if (finishedAt != null)
        {
          DateTimeOffset valueOrDefault = finishedAt.GetValueOrDefault();
          if (valueOrDefault > dateTimeOffset)
          {
            dateTimeOffset = valueOrDefault;
          }
        }
        DateTimeOffset dateTimeOffset2;
        if (!ClickyReadMarks.Marks.TryGetValue(clickyInfo.Slug, out dateTimeOffset2))
        {
          ClickyReadMarks.Marks[clickyInfo.Slug] = DateTimeOffset.Now;
          flag = true;
        }
        else if (dateTimeOffset > dateTimeOffset2)
        {
          list.Add(clickyInfo.Slug);
        }
      }
      if (flag)
      {
        AgentDefaults.Set<Dictionary<string, DateTimeOffset>>("clicky.dock.clickyLastSeen.v1", ClickyReadMarks.Marks);
      }
      return list;
    }

    // Token: 0x060022F2 RID: 8946 RVA: 0x00629860 File Offset: 0x00629860
    public static void MarkRead(string slug)
    {
      ClickyReadMarks.Marks[slug] = DateTimeOffset.Now;
      AgentDefaults.Set<Dictionary<string, DateTimeOffset>>("clicky.dock.clickyLastSeen.v1", ClickyReadMarks.Marks);
    }

    // Token: 0x060022F3 RID: 8947 RVA: 0x00629884 File Offset: 0x00629884
    public static void MarkAllRead(IEnumerable<string> slugs)
    {
      foreach (string text in slugs)
      {
        ClickyReadMarks.Marks[text] = DateTimeOffset.Now;
      }
      AgentDefaults.Set<Dictionary<string, DateTimeOffset>>("clicky.dock.clickyLastSeen.v1", ClickyReadMarks.Marks);
    }

    // Token: 0x04001100 RID: 4352
    private const string DefaultsKey = "clicky.dock.clickyLastSeen.v1";

    // Token: 0x04001101 RID: 4353
    [Nullable(new byte[] { 2, 1 })]
    private static Dictionary<string, DateTimeOffset> marks;
  }
}
