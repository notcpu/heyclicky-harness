using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BE RID: 190
  [NullableContext(1)]
  [Nullable(0)]
  public static class MorningSuggestionsInvitationPolicy
  {
    // Token: 0x06000C9A RID: 3226 RVA: 0x0037F2E4 File Offset: 0x0037F2E4
    public static bool IsResting(MorningDismissalHistory history, DateTimeOffset now)
    {
      DateTimeOffset? restingUntil = history.RestingUntil;
      if (restingUntil != null)
      {
        DateTimeOffset valueOrDefault = restingUntil.GetValueOrDefault();
        return now < valueOrDefault;
      }
      return false;
    }

    // Token: 0x06000C9B RID: 3227 RVA: 0x0037F314 File Offset: 0x0037F314
    public static MorningDismissalHistory RecordingDismissal(MorningDismissalHistory history, DateTimeOffset now)
    {
      DateTimeOffset? restingUntil = history.RestingUntil;
      DateTimeOffset? dateTimeOffset;
      if (restingUntil != null)
      {
        DateTimeOffset valueOrDefault = restingUntil.GetValueOrDefault();
        if (now >= valueOrDefault)
        {
          dateTimeOffset = null;
          goto IL_0034;
        }
      }
      dateTimeOffset = history.RestingUntil;
      IL_0034:
      DateTimeOffset? dateTimeOffset2 = dateTimeOffset;
      DateTime date = now.LocalDateTime.Date;
      List<DateTime> list = history.DismissedDays.ToList<DateTime>();
      if (list.Count != 0)
      {
        List<DateTime> list2 = list;
        if (!(list2[list2.Count - 1] != date))
        {
          goto IL_0077;
        }
      }
      list.Add(date);
      IL_0077:
      list = list.TakeLast<DateTime>(3).ToList<DateTime>();
      bool flag;
      if (list.Count == 3)
      {
        flag = list.Zip<DateTime, DateTime>(list.Skip<DateTime>(1)).All<ValueTuple<DateTime, DateTime>>(([TupleElementNames(new string[] { "First", "Second" })] ValueTuple<DateTime, DateTime> p) => (p.Item2 - p.Item1).TotalDays == 1.0);
      }
      else
      {
        flag = false;
      }
      if (!flag)
      {
        return new MorningDismissalHistory(list, dateTimeOffset2);
      }
      return new MorningDismissalHistory(new List<DateTime>(), new DateTimeOffset?(now + MorningSuggestionsInvitationPolicy.RestAfterRepeatedDismissals));
    }

    // Token: 0x06000C9C RID: 3228 RVA: 0x0037F408 File Offset: 0x0037F408
    public static MorningDismissalHistory RecordingApproval(MorningDismissalHistory history)
    {
      return MorningDismissalHistory.Empty;
    }

    // Token: 0x06000C9D RID: 3229 RVA: 0x0037F410 File Offset: 0x0037F410
    public static IReadOnlyList<ProactiveSuggestionCard> QualifyingMorningCards(IReadOnlyList<ProactiveSuggestionCard> cards, DateTimeOffset now)
    {
      return cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.CheckReason == ProactiveCheckReason.Morning && c.CreatedAt.LocalDateTime.Date == now.LocalDateTime.Date && !c.IsExpired(now)).ToList<ProactiveSuggestionCard>();
    }

    // Token: 0x06000C9E RID: 3230 RVA: 0x0037F444 File Offset: 0x0037F444
    public static bool IsSettled(DateTimeOffset? settledAt, DateTimeOffset now)
    {
      return settledAt != null && settledAt.GetValueOrDefault().LocalDateTime.Date == now.LocalDateTime.Date;
    }

    // Token: 0x06000C9F RID: 3231 RVA: 0x0037F488 File Offset: 0x0037F488
    public static DateTimeOffset AnchorDate(DateTimeOffset? greetingPresentedAt, DateTimeOffset? sweepStartedAt, DateTimeOffset? batchLandedAt, DateTimeOffset now)
    {
      if (greetingPresentedAt != null)
      {
        DateTimeOffset valueOrDefault = greetingPresentedAt.GetValueOrDefault();
        if (valueOrDefault.LocalDateTime.Date == now.LocalDateTime.Date)
        {
          return valueOrDefault;
        }
      }
      foreach (DateTimeOffset dateTimeOffset in new DateTimeOffset?[] { sweepStartedAt, batchLandedAt })
      {
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
          if (valueOrDefault2.LocalDateTime.Date == now.LocalDateTime.Date && valueOrDefault2 <= now)
          {
            return valueOrDefault2;
          }
        }
      }
      return now;
    }

    // Token: 0x06000CA0 RID: 3232 RVA: 0x0037F540 File Offset: 0x0037F540
    public static MorningSuggestionsInvitationPolicy.RebuildDecision Decide(bool hasActiveInvitation, bool isLaneEnabled, DateTimeOffset? settledAt, bool isReadingSuggestions, IReadOnlyList<ProactiveSuggestionCard> cards, DateTimeOffset? greetingPresentedAt, DateTimeOffset? sweepStartedAt, DateTimeOffset now, bool isRestingAfterRepeatedDismissals = false)
    {
      if (hasActiveInvitation || !isLaneEnabled || isRestingAfterRepeatedDismissals || MorningSuggestionsInvitationPolicy.IsSettled(settledAt, now))
      {
        return new MorningSuggestionsInvitationPolicy.RebuildDecision(MorningSuggestionsInvitationPolicy.RebuildKind.NothingToDo, null, default(DateTimeOffset), default(DateTimeOffset));
      }
      IReadOnlyList<ProactiveSuggestionCard> readOnlyList = MorningSuggestionsInvitationPolicy.QualifyingMorningCards(cards, now);
      if (readOnlyList.Count == 0)
      {
        return new MorningSuggestionsInvitationPolicy.RebuildDecision(MorningSuggestionsInvitationPolicy.RebuildKind.NothingToDo, null, default(DateTimeOffset), default(DateTimeOffset));
      }
      if (isReadingSuggestions)
      {
        return new MorningSuggestionsInvitationPolicy.RebuildDecision(MorningSuggestionsInvitationPolicy.RebuildKind.SettleForToday, null, default(DateTimeOffset), default(DateTimeOffset));
      }
      DateTimeOffset dateTimeOffset = readOnlyList.Min<ProactiveSuggestionCard, DateTimeOffset>((ProactiveSuggestionCard c) => c.CreatedAt);
      DateTimeOffset dateTimeOffset2 = MorningSuggestionsInvitationPolicy.AnchorDate(greetingPresentedAt, sweepStartedAt, new DateTimeOffset?(dateTimeOffset), now);
      return new MorningSuggestionsInvitationPolicy.RebuildDecision(MorningSuggestionsInvitationPolicy.RebuildKind.Rebuild, readOnlyList.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id).ToHashSet<Guid>(), dateTimeOffset, dateTimeOffset2 + MorningSuggestionsInvitationPolicy.DelayAfterGreeting);
    }

    // Token: 0x0400055D RID: 1373
    public static readonly TimeSpan DelayAfterGreeting = TimeSpan.FromSeconds(30L);

    // Token: 0x0400055E RID: 1374
    public static readonly TimeSpan SnoozeAfterPreemption = TimeSpan.FromMinutes(5L);

    // Token: 0x0400055F RID: 1375
    public const int ConsecutiveDismissalDaysBeforeRest = 3;

    // Token: 0x04000560 RID: 1376
    public static readonly TimeSpan RestAfterRepeatedDismissals = TimeSpan.FromDays(5);

    // Token: 0x020003D3 RID: 979
    [NullableContext(0)]
    public enum RebuildKind
    {
      // Token: 0x040019E6 RID: 6630
      NothingToDo,
      // Token: 0x040019E7 RID: 6631
      SettleForToday,
      // Token: 0x040019E8 RID: 6632
      Rebuild
    }

    // Token: 0x020003D4 RID: 980
    [NullableContext(2)]
    [Nullable(0)]
    public readonly struct RebuildDecision : IEquatable<MorningSuggestionsInvitationPolicy.RebuildDecision>
    {
      // Token: 0x06002AB5 RID: 10933 RVA: 0x00427434 File Offset: 0x00427434
      public RebuildDecision(MorningSuggestionsInvitationPolicy.RebuildKind Kind, IReadOnlySet<Guid> CardIds = null, DateTimeOffset CreatedAt = default(DateTimeOffset), DateTimeOffset PresentNotBefore = default(DateTimeOffset))
      {
        this.Kind = Kind;
        this.CardIds = CardIds;
        this.CreatedAt = CreatedAt;
        this.PresentNotBefore = PresentNotBefore;
      }

      // Token: 0x17000834 RID: 2100
      // (get) Token: 0x06002AB6 RID: 10934 RVA: 0x00427454 File Offset: 0x00427454
      // (set) Token: 0x06002AB7 RID: 10935 RVA: 0x0042745C File Offset: 0x0042745C
      public MorningSuggestionsInvitationPolicy.RebuildKind Kind { get; set; }

      // Token: 0x17000835 RID: 2101
      // (get) Token: 0x06002AB8 RID: 10936 RVA: 0x00427468 File Offset: 0x00427468
      // (set) Token: 0x06002AB9 RID: 10937 RVA: 0x00427470 File Offset: 0x00427470
      public IReadOnlySet<Guid> CardIds { get; set; }

      // Token: 0x17000836 RID: 2102
      // (get) Token: 0x06002ABA RID: 10938 RVA: 0x0042747C File Offset: 0x0042747C
      // (set) Token: 0x06002ABB RID: 10939 RVA: 0x00427484 File Offset: 0x00427484
      public DateTimeOffset CreatedAt { get; set; }

      // Token: 0x17000837 RID: 2103
      // (get) Token: 0x06002ABC RID: 10940 RVA: 0x00427490 File Offset: 0x00427490
      // (set) Token: 0x06002ABD RID: 10941 RVA: 0x00427498 File Offset: 0x00427498
      public DateTimeOffset PresentNotBefore { get; set; }

      // Token: 0x06002ABE RID: 10942 RVA: 0x004274A4 File Offset: 0x004274A4
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RebuildDecision");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002ABF RID: 10943 RVA: 0x004274F0 File Offset: 0x004274F0
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", CardIds = ");
        builder.Append(this.CardIds);
        builder.Append(", CreatedAt = ");
        builder.Append(this.CreatedAt.ToString());
        builder.Append(", PresentNotBefore = ");
        builder.Append(this.PresentNotBefore.ToString());
        return true;
      }

      // Token: 0x06002AC0 RID: 10944 RVA: 0x0042758C File Offset: 0x0042758C
      [CompilerGenerated]
      public static bool operator !=(MorningSuggestionsInvitationPolicy.RebuildDecision left, MorningSuggestionsInvitationPolicy.RebuildDecision right)
      {
        return !(left == right);
      }

      // Token: 0x06002AC1 RID: 10945 RVA: 0x00427598 File Offset: 0x00427598
      [CompilerGenerated]
      public static bool operator ==(MorningSuggestionsInvitationPolicy.RebuildDecision left, MorningSuggestionsInvitationPolicy.RebuildDecision right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002AC2 RID: 10946 RVA: 0x004275A4 File Offset: 0x004275A4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<MorningSuggestionsInvitationPolicy.RebuildKind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<IReadOnlySet<Guid>>.Default.GetHashCode(this.<CardIds>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<PresentNotBefore>k__BackingField);
      }

      // Token: 0x06002AC3 RID: 10947 RVA: 0x00427608 File Offset: 0x00427608
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is MorningSuggestionsInvitationPolicy.RebuildDecision && this.Equals((MorningSuggestionsInvitationPolicy.RebuildDecision)obj);
      }

      // Token: 0x06002AC4 RID: 10948 RVA: 0x00427620 File Offset: 0x00427620
      [CompilerGenerated]
      public bool Equals(MorningSuggestionsInvitationPolicy.RebuildDecision other)
      {
        return EqualityComparer<MorningSuggestionsInvitationPolicy.RebuildKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<IReadOnlySet<Guid>>.Default.Equals(this.<CardIds>k__BackingField, other.<CardIds>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<PresentNotBefore>k__BackingField, other.<PresentNotBefore>k__BackingField);
      }

      // Token: 0x06002AC5 RID: 10949 RVA: 0x00427690 File Offset: 0x00427690
      [CompilerGenerated]
      public void Deconstruct(out MorningSuggestionsInvitationPolicy.RebuildKind Kind, out IReadOnlySet<Guid> CardIds, out DateTimeOffset CreatedAt, out DateTimeOffset PresentNotBefore)
      {
        Kind = this.Kind;
        CardIds = this.CardIds;
        CreatedAt = this.CreatedAt;
        PresentNotBefore = this.PresentNotBefore;
      }
    }
  }
}
