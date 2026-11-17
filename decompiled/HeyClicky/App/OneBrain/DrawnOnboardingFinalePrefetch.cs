using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.App.Onboarding;
using HeyClicky.App.Proactive;
using HeyClicky.App.Shell;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000117 RID: 279
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DrawnOnboardingFinalePrefetch
  {
    // Token: 0x170002B2 RID: 690
    // (get) Token: 0x06000ECF RID: 3791 RVA: 0x005767C4 File Offset: 0x005767C4
    public static DrawnOnboardingFinalePrefetch Shared { get; } = new DrawnOnboardingFinalePrefetch();

    // Token: 0x06000ED0 RID: 3792 RVA: 0x005767CC File Offset: 0x005767CC
    private DrawnOnboardingFinalePrefetch()
    {
    }

    // Token: 0x170002B3 RID: 691
    // (get) Token: 0x06000ED1 RID: 3793 RVA: 0x005767D4 File Offset: 0x005767D4
    private bool IsActive
    {
      get
      {
        if (!this.isStarting)
        {
          CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
          return prefetchedTurn != null && !prefetchedTurn.IsDropped;
        }
        return true;
      }
    }

    // Token: 0x170002B4 RID: 692
    // (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00576800 File Offset: 0x00576800
    public bool IsPrefetching
    {
      get
      {
        CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
        return prefetchedTurn != null && !prefetchedTurn.IsDropped && !prefetchedTurn.IsAdopted;
      }
    }

    // Token: 0x170002B5 RID: 693
    // (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0057682C File Offset: 0x0057682C
    public int CodeCharactersReady
    {
      get
      {
        CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
        if (prefetchedTurn == null)
        {
          return 0;
        }
        return prefetchedTurn.CodeCharactersReady;
      }
    }

    // Token: 0x06000ED4 RID: 3796 RVA: 0x00576840 File Offset: 0x00576840
    public void StartIfReady(AppHost host)
    {
      if (!this.IsActive && host.IsDrawnOnboardingFinalePrefetchReady)
      {
        RealStack real = host.Real;
        if (real != null)
        {
          this.isStarting = true;
          this.StartAsync(host, real, this.generation);
          return;
        }
      }
    }

    // Token: 0x06000ED5 RID: 3797 RVA: 0x00576880 File Offset: 0x00576880
    private async Task StartAsync(AppHost host, RealStack real, int generationAtStart)
    {
      CompanionSession.PrefetchedTurn prepared = null;
      PanelWindow panel = host.TourPanelWindow;
      PanelWindow panelWindow = panel;
      if (panelWindow != null)
      {
        panelWindow.SetTemporarilyHiddenFromOwnCapture(true);
      }
      try
      {
        CompanionSession.PrefetchedTurn prefetchedTurn = await real.Companion.PreparePrefetchedFactTurnAsync("[App] The welcome tour's last moment: the user pressed Show me. Draw their welcome now, as the note says.");
        prepared = prefetchedTurn;
      }
      catch (Exception ex)
      {
        Log.Warn("drawn tour", "finale prefetch not started: " + ex.Message);
      }
      finally
      {
        PanelWindow panelWindow2 = panel;
        if (panelWindow2 != null)
        {
          panelWindow2.SetTemporarilyHiddenFromOwnCapture(false);
        }
      }
      if (this.generation != generationAtStart || !host.IsDrawnOnboardingFinalePrefetchReady || prepared == null)
      {
        if (this.generation == generationAtStart)
        {
          this.isStarting = false;
        }
      }
      else
      {
        InterviewController interview = host.Interview;
        IReadOnlyList<InterviewLine> readOnlyList = ((interview != null) ? interview.Transcript : null) ?? Array.Empty<InterviewLine>();
        ProactiveCoordinator proactive = host.Proactive;
        List<ProactiveSuggestionCard> list = (((proactive != null) ? proactive.VisibleCards : null) ?? Array.Empty<ProactiveSuggestionCard>()).Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.CheckReason == ProactiveCheckReason.Onboarding).Take<ProactiveSuggestionCard>(3).ToList<ProactiveSuggestionCard>();
        JsonObject jsonObject = DrawnOnboardingTour.TourBrief(host.AccountFirstNameForTour ?? "there", readOnlyList, host.SpokenTalkKeys(), list);
        this.isStarting = false;
        if (real.Companion.StartPrefetchedTurn(prepared, jsonObject))
        {
          this.turn = prepared;
          this.companion = real.Companion;
          this.plannedCardCount = list.Count;
          string text = "drawn tour";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 1);
          defaultInterpolatedStringHandler.AppendLiteral("finale: prefetching on the finale page (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" planned card(s))");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
    }

    // Token: 0x06000ED6 RID: 3798 RVA: 0x005768DC File Offset: 0x005768DC
    [NullableContext(2)]
    public DrawnOnboardingFinalePrefetch.Prefetched Take(string currentAccountId)
    {
      CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
      if (prefetchedTurn != null)
      {
        CompanionSession companionSession = this.companion;
        if (companionSession != null && !prefetchedTurn.IsAdopted)
        {
          if (prefetchedTurn.IsDropped || prefetchedTurn.AccountId != currentAccountId || prefetchedTurn.DidFail)
          {
            if (prefetchedTurn.DidFail && !prefetchedTurn.IsDropped)
            {
              Log.Info("drawn tour", "finale prefetch failed (" + prefetchedTurn.Failure + "): Show me runs the turn the old way");
            }
            this.Reset("the finale is over");
            return null;
          }
          if (!companionSession.AdoptPrefetchedTurn(prefetchedTurn))
          {
            this.Reset("the finale is over");
            return null;
          }
          return new DrawnOnboardingFinalePrefetch.Prefetched(prefetchedTurn, this.plannedCardCount, prefetchedTurn.StartedAt, prefetchedTurn.CodeCharactersReady);
        }
      }
      return null;
    }

    // Token: 0x06000ED7 RID: 3799 RVA: 0x00576990 File Offset: 0x00576990
    public void DiscardUnlessAdopted(string reason)
    {
      if (this.IsActive)
      {
        CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
        if (prefetchedTurn == null || !prefetchedTurn.IsAdopted)
        {
          Log.Info("drawn tour", "finale prefetch dropped: " + reason);
          this.Reset(reason);
          return;
        }
      }
    }

    // Token: 0x06000ED8 RID: 3800 RVA: 0x005769CC File Offset: 0x005769CC
    public void Reset(string reason = "the finale is over")
    {
      this.generation++;
      CompanionSession.PrefetchedTurn prefetchedTurn = this.turn;
      if (prefetchedTurn != null && !prefetchedTurn.IsAdopted)
      {
        CompanionSession companionSession = this.companion;
        if (companionSession != null)
        {
          companionSession.DropPrefetchedTurn(reason);
        }
      }
      this.turn = null;
      this.companion = null;
      this.plannedCardCount = 0;
      this.isStarting = false;
    }

    // Token: 0x04000609 RID: 1545
    private const string Area = "drawn tour";

    // Token: 0x0400060B RID: 1547
    public const string FinaleFact = "[App] The welcome tour's last moment: the user pressed Show me. Draw their welcome now, as the note says.";

    // Token: 0x0400060C RID: 1548
    [Nullable(2)]
    private CompanionSession.PrefetchedTurn turn;

    // Token: 0x0400060D RID: 1549
    [Nullable(2)]
    private CompanionSession companion;

    // Token: 0x0400060E RID: 1550
    private int plannedCardCount;

    // Token: 0x0400060F RID: 1551
    private bool isStarting;

    // Token: 0x04000610 RID: 1552
    private int generation;

    // Token: 0x02000609 RID: 1545
    [Nullable(0)]
    public sealed class Prefetched : IEquatable<DrawnOnboardingFinalePrefetch.Prefetched>
    {
      // Token: 0x0600305F RID: 12383 RVA: 0x0066808C File Offset: 0x0066808C
      public Prefetched(CompanionSession.PrefetchedTurn Turn, int PlannedCardCount, DateTimeOffset StartedAt, int CodeCharactersReady)
      {
        this.Turn = Turn;
        this.PlannedCardCount = PlannedCardCount;
        this.StartedAt = StartedAt;
        this.CodeCharactersReady = CodeCharactersReady;
        base..ctor();
      }

      // Token: 0x17000760 RID: 1888
      // (get) Token: 0x06003060 RID: 12384 RVA: 0x006680B4 File Offset: 0x006680B4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(DrawnOnboardingFinalePrefetch.Prefetched);
        }
      }

      // Token: 0x17000761 RID: 1889
      // (get) Token: 0x06003061 RID: 12385 RVA: 0x006680C0 File Offset: 0x006680C0
      // (set) Token: 0x06003062 RID: 12386 RVA: 0x006680C8 File Offset: 0x006680C8
      public CompanionSession.PrefetchedTurn Turn { get; set; }

      // Token: 0x17000762 RID: 1890
      // (get) Token: 0x06003063 RID: 12387 RVA: 0x006680D4 File Offset: 0x006680D4
      // (set) Token: 0x06003064 RID: 12388 RVA: 0x006680DC File Offset: 0x006680DC
      public int PlannedCardCount { get; set; }

      // Token: 0x17000763 RID: 1891
      // (get) Token: 0x06003065 RID: 12389 RVA: 0x006680E8 File Offset: 0x006680E8
      // (set) Token: 0x06003066 RID: 12390 RVA: 0x006680F0 File Offset: 0x006680F0
      public DateTimeOffset StartedAt { get; set; }

      // Token: 0x17000764 RID: 1892
      // (get) Token: 0x06003067 RID: 12391 RVA: 0x006680FC File Offset: 0x006680FC
      // (set) Token: 0x06003068 RID: 12392 RVA: 0x00668104 File Offset: 0x00668104
      public int CodeCharactersReady { get; set; }

      // Token: 0x06003069 RID: 12393 RVA: 0x00668110 File Offset: 0x00668110
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Prefetched");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600306A RID: 12394 RVA: 0x0066815C File Offset: 0x0066815C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Turn = ");
        builder.Append(this.Turn);
        builder.Append(", PlannedCardCount = ");
        builder.Append(this.PlannedCardCount.ToString());
        builder.Append(", StartedAt = ");
        builder.Append(this.StartedAt.ToString());
        builder.Append(", CodeCharactersReady = ");
        builder.Append(this.CodeCharactersReady.ToString());
        return true;
      }

      // Token: 0x0600306B RID: 12395 RVA: 0x00668200 File Offset: 0x00668200
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(DrawnOnboardingFinalePrefetch.Prefetched left, DrawnOnboardingFinalePrefetch.Prefetched right)
      {
        return !(left == right);
      }

      // Token: 0x0600306C RID: 12396 RVA: 0x0066820C File Offset: 0x0066820C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(DrawnOnboardingFinalePrefetch.Prefetched left, DrawnOnboardingFinalePrefetch.Prefetched right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600306D RID: 12397 RVA: 0x00668220 File Offset: 0x00668220
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<CompanionSession.PrefetchedTurn>.Default.GetHashCode(this.<Turn>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PlannedCardCount>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CodeCharactersReady>k__BackingField);
      }

      // Token: 0x0600306E RID: 12398 RVA: 0x0066829C File Offset: 0x0066829C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as DrawnOnboardingFinalePrefetch.Prefetched);
      }

      // Token: 0x0600306F RID: 12399 RVA: 0x006682AC File Offset: 0x006682AC
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(DrawnOnboardingFinalePrefetch.Prefetched other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<CompanionSession.PrefetchedTurn>.Default.Equals(this.<Turn>k__BackingField, other.<Turn>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PlannedCardCount>k__BackingField, other.<PlannedCardCount>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CodeCharactersReady>k__BackingField, other.<CodeCharactersReady>k__BackingField));
      }

      // Token: 0x06003071 RID: 12401 RVA: 0x00668340 File Offset: 0x00668340
      [CompilerGenerated]
      private Prefetched(DrawnOnboardingFinalePrefetch.Prefetched original)
      {
        this.Turn = original.<Turn>k__BackingField;
        this.PlannedCardCount = original.<PlannedCardCount>k__BackingField;
        this.StartedAt = original.<StartedAt>k__BackingField;
        this.CodeCharactersReady = original.<CodeCharactersReady>k__BackingField;
      }

      // Token: 0x06003072 RID: 12402 RVA: 0x00668378 File Offset: 0x00668378
      [CompilerGenerated]
      public void Deconstruct(out CompanionSession.PrefetchedTurn Turn, out int PlannedCardCount, out DateTimeOffset StartedAt, out int CodeCharactersReady)
      {
        Turn = this.Turn;
        PlannedCardCount = this.PlannedCardCount;
        StartedAt = this.StartedAt;
        CodeCharactersReady = this.CodeCharactersReady;
      }
    }
  }
}
