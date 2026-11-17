using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000CE RID: 206
  [NullableContext(1)]
  [Nullable(0)]
  public static class ProactiveHistory
  {
    // Token: 0x06000D6E RID: 3438 RVA: 0x00382340 File Offset: 0x00382340
    public static List<ProactiveHistoryEntry> Record(IReadOnlyList<ProactiveHistoryEntry> entries, ProactiveHistoryEntry entry, DateTimeOffset now)
    {
      List<ProactiveHistoryEntry> list = ProactiveHistory.Recent(entries, now).ToList<ProactiveHistoryEntry>();
      list.Insert(0, entry);
      return list.Take<ProactiveHistoryEntry>(80).ToList<ProactiveHistoryEntry>();
    }

    // Token: 0x06000D6F RID: 3439 RVA: 0x00382364 File Offset: 0x00382364
    public static List<ProactiveHistoryEntry> RecordUndecided(IReadOnlyList<ProactiveHistoryEntry> entries, ProactiveSuggestionCard card, ProactiveResolution resolution, DateTimeOffset now)
    {
      if (card.ViewedAt != null)
      {
        return ProactiveHistory.Record(entries, new ProactiveHistoryEntry(card.Title, card.Description, resolution, now, null, new bool?(true)), now);
      }
      return entries.ToList<ProactiveHistoryEntry>();
    }

    // Token: 0x06000D70 RID: 3440 RVA: 0x003823AC File Offset: 0x003823AC
    public static IEnumerable<ProactiveHistoryEntry> Recent(IReadOnlyList<ProactiveHistoryEntry> entries, DateTimeOffset now)
    {
      return entries.Where<ProactiveHistoryEntry>((ProactiveHistoryEntry e) => e.DecidedAt > now - ProactiveHistory.MaximumEntryAge);
    }

    // Token: 0x06000D71 RID: 3441 RVA: 0x003823D8 File Offset: 0x003823D8
    public static IReadOnlyList<ProactiveHistoryEntry> WireEntries(IReadOnlyList<ProactiveHistoryEntry> entries, DateTimeOffset now)
    {
      List<ProactiveHistoryEntry> list = ProactiveHistory.Recent(entries, now).ToList<ProactiveHistoryEntry>();
      IEnumerable<ProactiveHistoryEntry> enumerable = list.Where<ProactiveHistoryEntry>(delegate(ProactiveHistoryEntry e)
      {
        ProactiveResolution resolution = e.Resolution;
        return resolution <= ProactiveResolution.Denied;
      }).Take<ProactiveHistoryEntry>(40);
      IEnumerable<ProactiveHistoryEntry> enumerable2 = list.Where<ProactiveHistoryEntry>(delegate(ProactiveHistoryEntry e)
      {
        ProactiveResolution resolution2 = e.Resolution;
        bool flag = resolution2 - ProactiveResolution.Expired <= 1;
        return flag && e.WasViewed.GetValueOrDefault();
      }).Take<ProactiveHistoryEntry>(10);
      return (from e in enumerable.Concat<ProactiveHistoryEntry>(enumerable2)
        orderby e.DecidedAt descending
        select e).ToList<ProactiveHistoryEntry>();
    }

    // Token: 0x040005AB RID: 1451
    public const int MaximumEntries = 80;

    // Token: 0x040005AC RID: 1452
    public static readonly TimeSpan MaximumEntryAge = TimeSpan.FromDays(30);

    // Token: 0x040005AD RID: 1453
    private const int MaximumDecidedWireEntries = 40;

    // Token: 0x040005AE RID: 1454
    private const int MaximumUndecidedWireEntries = 10;
  }
}
