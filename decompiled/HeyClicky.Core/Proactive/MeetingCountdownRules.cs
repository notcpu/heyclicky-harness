using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000E3 RID: 227
  public static class MeetingCountdownRules
  {
    // Token: 0x06000EBA RID: 3770 RVA: 0x0038C0DC File Offset: 0x0038C0DC
    [NullableContext(1)]
    [return: TupleElementNames(new string[] { "Active", "Change" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<UpcomingCalendarEvent, MeetingCountdownRules.Change> Step([Nullable(2)] UpcomingCalendarEvent active, IReadOnlyList<UpcomingCalendarEvent> schedule, ISet<string> announced, DateTimeOffset now, bool canPresent)
    {
      MeetingCountdownRules.Change change = MeetingCountdownRules.Change.None;
      if (active != null && now > active.StartsAt && !active.IsOngoing(now))
      {
        active = null;
        change = MeetingCountdownRules.Change.Cleared;
      }
      if (active != null)
      {
        UpcomingCalendarEvent upcomingCalendarEvent = schedule.FirstOrDefault<UpcomingCalendarEvent>((UpcomingCalendarEvent e) => e.AnnouncementKey == active.AnnouncementKey);
        if (upcomingCalendarEvent == null)
        {
          active = null;
          change = MeetingCountdownRules.Change.Cleared;
        }
        else if (upcomingCalendarEvent != active)
        {
          active = upcomingCalendarEvent;
          change = MeetingCountdownRules.Change.Updated;
        }
      }
      if (active != null || !canPresent)
      {
        return new ValueTuple<UpcomingCalendarEvent, MeetingCountdownRules.Change>(active, change);
      }
      UpcomingCalendarEvent upcomingCalendarEvent2 = schedule.FirstOrDefault<UpcomingCalendarEvent>(delegate(UpcomingCalendarEvent e)
      {
        if (e.IsAllDay || announced.Contains(e.AnnouncementKey))
        {
          return false;
        }
        TimeSpan timeSpan = e.StartsAt - now;
        return timeSpan <= MeetingCountdownRules.PresentWindow && (timeSpan > TimeSpan.Zero || e.IsOngoing(now));
      });
      if (upcomingCalendarEvent2 == null)
      {
        return new ValueTuple<UpcomingCalendarEvent, MeetingCountdownRules.Change>(null, change);
      }
      announced.Add(upcomingCalendarEvent2.AnnouncementKey);
      return new ValueTuple<UpcomingCalendarEvent, MeetingCountdownRules.Change>(upcomingCalendarEvent2, MeetingCountdownRules.Change.Presented);
    }

    // Token: 0x06000EBB RID: 3771 RVA: 0x0038C1D4 File Offset: 0x0038C1D4
    public static bool ShouldRefresh(DateTimeOffset now, DateTimeOffset? lastRefresh, DateTimeOffset? lastFailure, bool backendConnected, bool triggeredByNotchExpand)
    {
      TimeSpan timeSpan = (triggeredByNotchExpand ? MeetingCountdownRules.NotchExpandStaleness : ((lastRefresh != null && !backendConnected) ? MeetingCountdownRules.NotConnectedRecheck : MeetingCountdownRules.RefreshStaleness));
      if (lastRefresh != null)
      {
        DateTimeOffset valueOrDefault = lastRefresh.GetValueOrDefault();
        if (now - valueOrDefault < timeSpan)
        {
          return false;
        }
      }
      if (lastFailure != null)
      {
        DateTimeOffset valueOrDefault2 = lastFailure.GetValueOrDefault();
        if (now - valueOrDefault2 < MeetingCountdownRules.FailedFetchBackoff)
        {
          return false;
        }
      }
      return true;
    }

    // Token: 0x04000651 RID: 1617
    public static readonly TimeSpan PresentWindow = TimeSpan.FromSeconds(75L);

    // Token: 0x04000652 RID: 1618
    public static readonly TimeSpan TickInterval = TimeSpan.FromSeconds(15L);

    // Token: 0x04000653 RID: 1619
    public static readonly TimeSpan RefreshStaleness = TimeSpan.FromMinutes(5L);

    // Token: 0x04000654 RID: 1620
    public static readonly TimeSpan NotConnectedRecheck = TimeSpan.FromMinutes(30L);

    // Token: 0x04000655 RID: 1621
    public static readonly TimeSpan NotchExpandStaleness = TimeSpan.FromSeconds(30L);

    // Token: 0x04000656 RID: 1622
    public static readonly TimeSpan FailedFetchBackoff = TimeSpan.FromSeconds(60L);

    // Token: 0x020003F9 RID: 1017
    public enum Change
    {
      // Token: 0x04001B2D RID: 6957
      None,
      // Token: 0x04001B2E RID: 6958
      Cleared,
      // Token: 0x04001B2F RID: 6959
      Updated,
      // Token: 0x04001B30 RID: 6960
      Presented
    }
  }
}
