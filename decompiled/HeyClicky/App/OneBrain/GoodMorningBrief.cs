using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Proactive;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Drawing.Overlay;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000118 RID: 280
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GoodMorningBrief
  {
    // Token: 0x170002B6 RID: 694
    // (get) Token: 0x06000EDA RID: 3802 RVA: 0x00576A34 File Offset: 0x00576A34
    // (set) Token: 0x06000EDB RID: 3803 RVA: 0x00576A3C File Offset: 0x00576A3C
    [Nullable(2)]
    public static GoodMorningBrief Shared
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170002B7 RID: 695
    // (get) Token: 0x06000EDC RID: 3804 RVA: 0x00576A44 File Offset: 0x00576A44
    // (set) Token: 0x06000EDD RID: 3805 RVA: 0x00576A4C File Offset: 0x00576A4C
    public string ResearchStatusLine { get; private set; } = "";

    // Token: 0x170002B8 RID: 696
    // (get) Token: 0x06000EDE RID: 3806 RVA: 0x00576A58 File Offset: 0x00576A58
    // (set) Token: 0x06000EDF RID: 3807 RVA: 0x00576A60 File Offset: 0x00576A60
    public bool IsResearching { get; private set; }

    // Token: 0x170002B9 RID: 697
    // (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00576A6C File Offset: 0x00576A6C
    // (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00576A74 File Offset: 0x00576A74
    public bool HasSavedResearch { get; private set; }

    // Token: 0x170002BA RID: 698
    // (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00576A80 File Offset: 0x00576A80
    // (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00576A88 File Offset: 0x00576A88
    public bool IsPreparingMorning { get; private set; }

    // Token: 0x170002BB RID: 699
    // (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00576A94 File Offset: 0x00576A94
    // (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00576A9C File Offset: 0x00576A9C
    [Nullable(2)]
    public string NotchHeadline
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170002BC RID: 700
    // (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00576AA8 File Offset: 0x00576AA8
    // (set) Token: 0x06000EE7 RID: 3815 RVA: 0x00576AB0 File Offset: 0x00576AB0
    [Nullable(2)]
    public string NotchHeadlineLine
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x170002BD RID: 701
    // (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00576ABC File Offset: 0x00576ABC
    // (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00576AC4 File Offset: 0x00576AC4
    public bool HasPaperToday { get; private set; }

    // Token: 0x14000033 RID: 51
    // (add) Token: 0x06000EEA RID: 3818 RVA: 0x00576AD0 File Offset: 0x00576AD0
    // (remove) Token: 0x06000EEB RID: 3819 RVA: 0x00576B08 File Offset: 0x00576B08
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x06000EEC RID: 3820 RVA: 0x00576B40 File Offset: 0x00576B40
    public GoodMorningBrief(AppHost host, RealStack real)
    {
      this.host = host;
      this.real = real;
      GoodMorningBrief.Shared = this;
      this.RefreshStatusFromDisk(null);
    }

    // Token: 0x06000EED RID: 3821 RVA: 0x00576B70 File Offset: 0x00576B70
    [NullableContext(2)]
    public static JsonObject ConsumePendingBrief()
    {
      object obj = GoodMorningBrief.briefGate;
      JsonObject jsonObject2;
      lock (obj)
      {
        JsonObject jsonObject = GoodMorningBrief.pendingBrief;
        GoodMorningBrief.pendingBrief = null;
        jsonObject2 = jsonObject;
      }
      return jsonObject2;
    }

    // Token: 0x170002BE RID: 702
    // (get) Token: 0x06000EEE RID: 3822 RVA: 0x00576BB8 File Offset: 0x00576BB8
    public static bool IsMorningPaperEnabledByDefault
    {
      get
      {
        return false;
      }
    }

    // Token: 0x06000EEF RID: 3823 RVA: 0x00576BBC File Offset: 0x00576BBC
    public static bool IsMorningPaperEnabled(SettingsStore settings)
    {
      bool? flag = settings.Read<bool?>((AppSettings s) => s.MorningPaperEnabled);
      if (flag == null)
      {
        return GoodMorningBrief.IsMorningPaperEnabledByDefault;
      }
      return flag.GetValueOrDefault();
    }

    // Token: 0x170002BF RID: 703
    // (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00576C08 File Offset: 0x00576C08
    private bool PaperEnabled
    {
      get
      {
        return GoodMorningBrief.IsMorningPaperEnabled(this.real.Core.Settings);
      }
    }

    // Token: 0x06000EF1 RID: 3825 RVA: 0x00576C20 File Offset: 0x00576C20
    public void NoteSettledSweep(ProactiveJobStatus status, string userId, IReadOnlyList<ProactiveSuggestionCard> cards)
    {
      string curatedEvidence = status.CuratedEvidence;
      if (curatedEvidence == null || curatedEvidence.Length <= 0)
      {
        return;
      }
      if (status.MorningHeadline == null && GoodMorningBrief.ParsedEvidence(curatedEvidence).Findings.Count == 0)
      {
        return;
      }
      GoodMorningBrief.SavedResearch savedResearch = new GoodMorningBrief.SavedResearch();
      savedResearch.UserId = userId;
      savedResearch.SavedAt = DateTimeOffset.Now;
      savedResearch.JobId = status.JobId;
      savedResearch.CuratedEvidence = curatedEvidence;
      savedResearch.Headline = status.MorningHeadline;
      savedResearch.HeadlineLine = status.MorningHeadlineLine;
      List<GoodMorningBrief.SavedCard> list;
      if (cards.Count != 0)
      {
        Func<ProactiveSuggestionCard, GoodMorningBrief.SavedCard> func;
        if ((func = GoodMorningBrief.<>O.<0>__Saved) == null)
        {
          func = (GoodMorningBrief.<>O.<0>__Saved = new Func<ProactiveSuggestionCard, GoodMorningBrief.SavedCard>(GoodMorningBrief.Saved));
        }
        list = cards.Select<ProactiveSuggestionCard, GoodMorningBrief.SavedCard>(func).ToList<GoodMorningBrief.SavedCard>();
      }
      else
      {
        list = null;
      }
      savedResearch.PlateCards = list;
      GoodMorningBrief.Save(savedResearch);
      this.host.DispatcherQueue.TryEnqueue(delegate
      {
        this.RefreshStatusFromDisk(userId);
      });
      Log.Info("good morning", "morning paper research kept: " + (status.MorningHeadline ?? "no headline"));
    }

    // Token: 0x06000EF2 RID: 3826 RVA: 0x00576D34 File Offset: 0x00576D34
    private static GoodMorningBrief.SavedCard Saved(ProactiveSuggestionCard c)
    {
      string title = c.Title;
      string description = c.Description;
      string agentPrompt = c.AgentPrompt;
      string clickySlug = c.ClickySlug;
      ProactiveRoutine routine = c.Routine;
      int? num = ((routine != null) ? new int?(routine.EveryMinutes) : null);
      ProactiveRoutine routine2 = c.Routine;
      return new GoodMorningBrief.SavedCard(title, description, agentPrompt, clickySlug, num, (routine2 != null) ? routine2.Title : null, c.IntegrationName);
    }

    // Token: 0x06000EF3 RID: 3827 RVA: 0x00576D98 File Offset: 0x00576D98
    public static bool HasTodaysResearch(SettingsStore settings, [Nullable(2)] string userId)
    {
      if (GoodMorningBrief.IsMorningPaperEnabled(settings) && userId != null)
      {
        GoodMorningBrief.SavedResearch savedResearch = GoodMorningBrief.LoadSavedResearch(userId);
        if (savedResearch != null)
        {
          return GoodMorningBrief.IsToday(savedResearch.SavedAt);
        }
      }
      return false;
    }

    // Token: 0x06000EF4 RID: 3828 RVA: 0x00576DC8 File Offset: 0x00576DC8
    [return: TupleElementNames(new string[] { "Headline", "Line" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    public static ValueTuple<string, string>? TodaysHeadline(SettingsStore settings, [Nullable(2)] string userId)
    {
      if (GoodMorningBrief.IsMorningPaperEnabled(settings) && userId != null)
      {
        GoodMorningBrief.SavedResearch savedResearch = GoodMorningBrief.LoadSavedResearch(userId);
        if (savedResearch != null && GoodMorningBrief.IsToday(savedResearch.SavedAt))
        {
          string headline = savedResearch.Headline;
          if (headline != null && headline.Length > 0)
          {
            return new ValueTuple<string, string>?(new ValueTuple<string, string>(headline, savedResearch.HeadlineLine));
          }
        }
      }
      return null;
    }

    // Token: 0x06000EF5 RID: 3829 RVA: 0x00576E28 File Offset: 0x00576E28
    private static bool IsToday(DateTimeOffset at)
    {
      return at.LocalDateTime.Date == DateTime.Now.Date;
    }

    // Token: 0x170002C0 RID: 704
    // (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00576E58 File Offset: 0x00576E58
    public static string ResearchFilePath
    {
      get
      {
        return AppPaths.Combine(new string[] { "GoodMorningResearch.json" });
      }
    }

    // Token: 0x06000EF7 RID: 3831 RVA: 0x00576E70 File Offset: 0x00576E70
    [NullableContext(2)]
    public static GoodMorningBrief.SavedResearch LoadSavedResearch(string userId)
    {
      GoodMorningBrief.SavedResearch savedResearch;
      try
      {
        if (!File.Exists(GoodMorningBrief.ResearchFilePath))
        {
          savedResearch = null;
        }
        else
        {
          GoodMorningBrief.SavedResearch savedResearch2 = JsonSerializer.Deserialize<GoodMorningBrief.SavedResearch>(File.ReadAllText(GoodMorningBrief.ResearchFilePath), GoodMorningBrief.FileJson);
          if (savedResearch2 == null || (userId != null && savedResearch2.UserId != userId))
          {
            savedResearch = null;
          }
          else
          {
            savedResearch = savedResearch2;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Info("good morning", "saved research unreadable: " + ex.Message);
        savedResearch = null;
      }
      return savedResearch;
    }

    // Token: 0x06000EF8 RID: 3832 RVA: 0x00576EF0 File Offset: 0x00576EF0
    private static void Save(GoodMorningBrief.SavedResearch research)
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(GoodMorningBrief.ResearchFilePath));
        string text = GoodMorningBrief.ResearchFilePath + ".tmp";
        File.WriteAllText(text, JsonSerializer.Serialize<GoodMorningBrief.SavedResearch>(research, GoodMorningBrief.FileJson));
        File.Move(text, GoodMorningBrief.ResearchFilePath, true);
      }
      catch (Exception ex)
      {
        Log.Warn("good morning", "research not saved: " + ex.Message);
      }
    }

    // Token: 0x06000EF9 RID: 3833 RVA: 0x00576F68 File Offset: 0x00576F68
    [NullableContext(2)]
    public void RefreshStatusFromDisk(string userId = null)
    {
      if (this.IsResearching)
      {
        return;
      }
      if (userId == null)
      {
        userId = this.real.Core.Auth.UserId;
      }
      GoodMorningBrief.SavedResearch savedResearch = GoodMorningBrief.LoadSavedResearch(userId);
      bool flag = savedResearch != null && this.PaperEnabled && GoodMorningBrief.IsToday(savedResearch.SavedAt);
      this.NotchHeadline = (flag ? savedResearch.Headline : null);
      this.NotchHeadlineLine = (flag ? savedResearch.HeadlineLine : null);
      this.HasPaperToday = flag;
      if (savedResearch != null)
      {
        this.HasSavedResearch = true;
        int count = GoodMorningBrief.ParsedEvidence(savedResearch.CuratedEvidence).Findings.Count;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Research saved ");
        defaultInterpolatedStringHandler.AppendFormatted(GoodMorningBrief.RelativeTime(savedResearch.SavedAt, null));
        defaultInterpolatedStringHandler.AppendLiteral(" · ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" finding");
        defaultInterpolatedStringHandler.AppendFormatted((count == 1) ? "" : "s");
        this.ResearchStatusLine = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        this.HasSavedResearch = false;
        this.ResearchStatusLine = "No research saved yet.";
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06000EFA RID: 3834 RVA: 0x005770A4 File Offset: 0x005770A4
    public void StartResearch()
    {
      GoodMorningBrief.<>c__DisplayClass61_0 CS$<>8__locals1 = new GoodMorningBrief.<>c__DisplayClass61_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.IsResearching)
      {
        return;
      }
      CS$<>8__locals1.userId = this.real.Core.Auth.UserId;
      if (CS$<>8__locals1.userId == null)
      {
        this.ResearchStatusLine = "Sign in first.";
        Action changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
        return;
      }
      else
      {
        CS$<>8__locals1.proactive = this.host.Proactive;
        if (CS$<>8__locals1.proactive == null)
        {
          return;
        }
        this.IsResearching = true;
        this.ResearchStatusLine = "Reading your apps and the news… (a few minutes)";
        Action changed2 = this.Changed;
        if (changed2 != null)
        {
          changed2();
        }
        CS$<>8__locals1.started = DateTimeOffset.Now;
        Task.Run(delegate
        {
          GoodMorningBrief.<>c__DisplayClass61_0.<<StartResearch>b__0>d <<StartResearch>b__0>d;
          <<StartResearch>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
          <<StartResearch>b__0>d.<>4__this = CS$<>8__locals1;
          <<StartResearch>b__0>d.<>1__state = -1;
          <<StartResearch>b__0>d.<>t__builder.Start<GoodMorningBrief.<>c__DisplayClass61_0.<<StartResearch>b__0>d>(ref <<StartResearch>b__0>d);
          return <<StartResearch>b__0>d.<>t__builder.Task;
        });
        return;
      }
    }

    // Token: 0x06000EFB RID: 3835 RVA: 0x00577160 File Offset: 0x00577160
    private async Task<GoodMorningBrief.SavedResearch> RunResearchAsync(ProactiveCoordinator proactive, string userId, DateTimeOffset started, Action<string> onProgress)
    {
      ProactiveRequestInputs proactiveRequestInputs = await proactive.BuildRequestInputsAsync(ProactiveCheckReason.Debug, CancellationToken.None).ConfigureAwait(false);
      ProactiveRequestInputs proactiveRequestInputs2 = proactiveRequestInputs.<Clone>$();
      proactiveRequestInputs2.ResearchOnlyForGoodMorning = true;
      proactiveRequestInputs = proactiveRequestInputs2;
      ProactiveAgentsClient client = new ProactiveAgentsClient(this.real.Core.Worker);
      ProactiveJobStatus status = await client.StartJobAsync(proactiveRequestInputs, DateTimeOffset.Now, default(CancellationToken)).ConfigureAwait(false);
      string text = "good morning";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 2);
      defaultInterpolatedStringHandler.AppendLiteral("research job ");
      defaultInterpolatedStringHandler.AppendFormatted(status.JobId);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted<ProactiveJobState>(status.State);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      while (!status.IsSettled)
      {
        if (DateTimeOffset.Now - started > TimeSpan.FromMinutes(20L))
        {
          throw new InvalidOperationException("Took over 20 minutes.");
        }
        await Task.Delay(5000).ConfigureAwait(false);
        int num = (int)(DateTimeOffset.Now - started).TotalMinutes;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Reading your apps and the news… ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" min (");
        defaultInterpolatedStringHandler.AppendFormatted(status.State.ToString().ToLowerInvariant());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        onProgress(defaultInterpolatedStringHandler.ToStringAndClear());
        try
        {
          status = await client.PollJobAsync(status.JobId, ProactiveCheckReason.Debug, DateTimeOffset.Now, default(CancellationToken)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Info("good morning", "research poll: " + ex.Message);
        }
      }
      if (status.State == ProactiveJobState.Failed)
      {
        throw new InvalidOperationException(status.Error ?? "The research job failed.");
      }
      string curatedEvidence = status.CuratedEvidence;
      if (curatedEvidence == null || curatedEvidence.Length <= 0)
      {
        throw new InvalidOperationException("A suggestion sweep was already running, so this joined it (no research came back). Try again once it's done.");
      }
      return new GoodMorningBrief.SavedResearch
      {
        UserId = userId,
        SavedAt = DateTimeOffset.Now,
        JobId = status.JobId,
        CuratedEvidence = curatedEvidence
      };
    }

    // Token: 0x06000EFC RID: 3836 RVA: 0x005771C4 File Offset: 0x005771C4
    public void Play(string source = "notch_read_it")
    {
      if (!this.real.Core.Auth.IsSignedIn)
      {
        return;
      }
      GoodMorningBrief.SavedResearch savedResearch = GoodMorningBrief.LoadSavedResearch(this.real.Core.Auth.UserId);
      if (savedResearch == null)
      {
        this.ResearchStatusLine = "Research first: nothing saved yet.";
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
        string text = "play_skipped";
        bool flag = true;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["source"] = source;
        jsonObject["reason"] = "no_research";
        ClickyTourJobCards.TrackFlowAction(text, flag, jsonObject);
        return;
      }
      this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AgentPeek));
      if (this.host.Services.Presenter.Presentation is NotchPresentation.TextInput)
      {
        this.host.Services.Presenter.Present(new NotchPresentation.Idle());
      }
      this.PlayAsync(savedResearch, source);
    }

    // Token: 0x06000EFD RID: 3837 RVA: 0x005772C4 File Offset: 0x005772C4
    private async Task PlayAsync(GoodMorningBrief.SavedResearch research, string source)
    {
      GoodMorningBrief.IsPlaying = true;
      try
      {
        IReadOnlyList<string> readOnlyList = await this.TodaysCalendarLinesAsync();
        ProactiveCoordinator proactive = this.host.Proactive;
        List<ProactiveSuggestionCard> list = (((proactive != null) ? proactive.LiveCards(new ProactiveCheckReason[]
        {
          ProactiveCheckReason.Morning,
          ProactiveCheckReason.Debug
        }) : null) ?? Array.Empty<ProactiveSuggestionCard>()).Take<ProactiveSuggestionCard>(4).ToList<ProactiveSuggestionCard>();
        DrawingSuggestionCards.Set(list.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id));
        ClickyTourJobCards.Shared.DismissAll();
        ClickyTourJobCards.Shared.SuggestionCheckReason = ProactiveCheckReason.Morning;
        DrawnOnboardingTour.PlannedCardsById = list.Select<ProactiveSuggestionCard, ValueTuple<string, ProactiveSuggestionCard>>(delegate(ProactiveSuggestionCard c, int i)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(1, 1);
          defaultInterpolatedStringHandler2.AppendLiteral("c");
          defaultInterpolatedStringHandler2.AppendFormatted<int>(i + 1);
          return new ValueTuple<string, ProactiveSuggestionCard>(defaultInterpolatedStringHandler2.ToStringAndClear(), c);
        }).ToDictionary<ValueTuple<string, ProactiveSuggestionCard>, string, ProactiveSuggestionCard>(([TupleElementNames(new string[] { "Key", "Card" })] ValueTuple<string, ProactiveSuggestionCard> p) => p.Item1, ([TupleElementNames(new string[] { "Key", "Card" })] ValueTuple<string, ProactiveSuggestionCard> p) => p.Item2);
        object obj = GoodMorningBrief.briefGate;
        lock (obj)
        {
          GoodMorningBrief.pendingBrief = GoodMorningBrief.MorningBrief(this.FirstName(), research, readOnlyList, list);
        }
        string text = "good morning";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
        defaultInterpolatedStringHandler.AppendLiteral("playing from research saved ");
        defaultInterpolatedStringHandler.AppendFormatted(GoodMorningBrief.RelativeTime(research.SavedAt, null));
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" card(s))");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        long playStartedAt = Environment.TickCount64;
        string text2 = "played";
        bool flag2 = true;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["source"] = source;
        jsonObject["card_count"] = list.Count;
        jsonObject["story_count"] = GoodMorningBrief.ParsedEvidence(research.CuratedEvidence).Findings.Count;
        jsonObject["has_headline"] = !string.IsNullOrEmpty(research.Headline);
        jsonObject["calendar_event_count"] = readOnlyList.Count;
        jsonObject["research_age_minutes"] = (int)((DateTimeOffset.Now - research.SavedAt).TotalSeconds / 60.0);
        ClickyTourJobCards.TrackFlowAction(text2, flag2, jsonObject);
        await Task.Delay(700);
        await this.real.Companion.SubmitFactAsync("[App] It's the user's first moment at their PC this morning. Draw their good morning now, as the note says.", false);
        GoodMorningBrief.ConsumePendingBrief();
        long waitStarted = Environment.TickCount64;
        while (Environment.TickCount64 - waitStarted < 180000L && (this.real.Canvas.IsStreamingNarration || this.real.Companion.State == VoiceState.Responding))
        {
          await Task.Delay(300);
        }
        bool flag3 = Environment.TickCount64 - waitStarted >= 180000L;
        string text3 = "ended";
        bool flag4 = true;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["source"] = source;
        jsonObject2["outcome"] = (flag3 ? "narration_timeout" : ((this.real.Canvas.DrawingDisplay == null) ? "no_drawing" : "finished"));
        jsonObject2["duration_seconds"] = ClickyAnalytics.RoundedSeconds((double)(Environment.TickCount64 - playStartedAt) / 1000.0);
        jsonObject2["cards_shown_count"] = ClickyTourJobCards.Shared.PlacedCardCount;
        ClickyTourJobCards.TrackFlowAction(text3, flag4, jsonObject2);
        ClickyTourJobCards.Shared.KeepCardsUntilQuestionsGo();
        DrawnOnboardingTour.PlannedCardsById = new Dictionary<string, ProactiveSuggestionCard>();
      }
      catch (Exception ex)
      {
        Log.Warn("good morning", "play failed: " + ex.Message);
      }
      finally
      {
        GoodMorningBrief.IsPlaying = false;
      }
    }

    // Token: 0x06000EFE RID: 3838 RVA: 0x00577318 File Offset: 0x00577318
    public static JsonObject MorningBrief([Nullable(2)] string firstName, GoodMorningBrief.SavedResearch research, IReadOnlyList<string> todaysEvents, IReadOnlyList<ProactiveSuggestionCard> plateCards)
    {
      return DrawnTourBriefs.MorningBrief(firstName, GoodMorningBrief.RelativeTime(research.SavedAt, null), research.CuratedEvidence, research.Headline, todaysEvents, plateCards);
    }

    // Token: 0x06000EFF RID: 3839 RVA: 0x00577350 File Offset: 0x00577350
    public static GoodMorningBrief.Evidence ParsedEvidence(string json)
    {
      GoodMorningBrief.Evidence evidence;
      try
      {
        JsonObject jsonObject = JsonNode.Parse(json, null, default(JsonDocumentOptions)) as JsonObject;
        if (jsonObject == null)
        {
          evidence = new GoodMorningBrief.Evidence(Array.Empty<JsonObject>(), Array.Empty<ValueTuple<string, string>>());
        }
        else
        {
          JsonArray jsonArray = jsonObject["findings"] as JsonArray;
          List<JsonObject> list = ((jsonArray != null) ? jsonArray.OfType<JsonObject>().ToList<JsonObject>() : null) ?? new List<JsonObject>();
          List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>();
          JsonArray jsonArray2 = jsonObject["summaries"] as JsonArray;
          foreach (JsonObject jsonObject2 in (((jsonArray2 != null) ? jsonArray2.OfType<JsonObject>() : null) ?? Array.Empty<JsonObject>()))
          {
            JsonValue jsonValue = jsonObject2["source"] as JsonValue;
            string text;
            if (jsonValue != null && jsonValue.TryGetValue<string>(out text))
            {
              JsonValue jsonValue2 = jsonObject2["summary"] as JsonValue;
              string text2;
              if (jsonValue2 != null && jsonValue2.TryGetValue<string>(out text2) && text2.Length > 0)
              {
                list2.Add(new ValueTuple<string, string>(text, text2));
              }
            }
          }
          evidence = new GoodMorningBrief.Evidence(list, list2);
        }
      }
      catch (JsonException)
      {
        evidence = new GoodMorningBrief.Evidence(Array.Empty<JsonObject>(), Array.Empty<ValueTuple<string, string>>());
      }
      return evidence;
    }

    // Token: 0x06000F00 RID: 3840 RVA: 0x005774C8 File Offset: 0x005774C8
    private async Task<IReadOnlyList<string>> TodaysCalendarLinesAsync()
    {
      UpcomingCalendarFetch upcomingCalendarFetch = await new UpcomingMeetingsClient(this.real.Core.Worker).FetchAsync(default(CancellationToken));
      IReadOnlyList<string> readOnlyList;
      if (upcomingCalendarFetch == null)
      {
        readOnlyList = Array.Empty<string>();
      }
      else
      {
        readOnlyList = upcomingCalendarFetch.Events.Where<UpcomingCalendarEvent>((UpcomingCalendarEvent e) => e.StartsAt.LocalDateTime.Date == DateTime.Now.Date).Take<UpcomingCalendarEvent>(8).Select<UpcomingCalendarEvent, string>(delegate(UpcomingCalendarEvent e)
        {
          string text = (e.IsAllDay ? "all day" : e.StartsAt.LocalDateTime.ToString("h:mm tt", CultureInfo.GetCultureInfo("en-US")));
          string location = e.Location;
          string text2 = ((location != null) ? (" (" + location + ")") : ((e.MeetingUrl != null) ? " (video call)" : ""));
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
          defaultInterpolatedStringHandler.AppendLiteral("- ");
          defaultInterpolatedStringHandler.AppendFormatted(text);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(e.Title);
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          return defaultInterpolatedStringHandler.ToStringAndClear();
        })
          .ToList<string>();
      }
      return readOnlyList;
    }

    // Token: 0x06000F01 RID: 3841 RVA: 0x0057750C File Offset: 0x0057750C
    [NullableContext(2)]
    private string FirstName()
    {
      UserProfile user = this.real.Core.Auth.User;
      string text;
      if (user == null)
      {
        text = null;
      }
      else
      {
        string displayName = user.DisplayName;
        text = ((displayName != null) ? displayName.Trim() : null);
      }
      string text2 = text;
      if (text2 != null && text2.Length > 0)
      {
        string[] array = text2.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (array != null && array.Length > 0)
        {
          return array[0];
        }
      }
      return null;
    }

    // Token: 0x06000F02 RID: 3842 RVA: 0x0057756C File Offset: 0x0057756C
    public static string RelativeTime(DateTimeOffset date, DateTimeOffset? now = null)
    {
      return DrawnTourBriefs.RelativeTime(date, now ?? DateTimeOffset.Now);
    }

    // Token: 0x04000611 RID: 1553
    private const string Area = "good morning";

    // Token: 0x04000613 RID: 1555
    private readonly AppHost host;

    // Token: 0x04000614 RID: 1556
    private readonly RealStack real;

    // Token: 0x0400061D RID: 1565
    [Nullable(2)]
    private static JsonObject pendingBrief;

    // Token: 0x0400061E RID: 1566
    private static readonly object briefGate = new object();

    // Token: 0x0400061F RID: 1567
    public static volatile bool IsPlaying;

    // Token: 0x04000620 RID: 1568
    private static readonly JsonSerializerOptions FileJson = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    // Token: 0x0200060C RID: 1548
    [Nullable(0)]
    [RequiredMember]
    public sealed class SavedResearch : IEquatable<GoodMorningBrief.SavedResearch>
    {
      // Token: 0x17000765 RID: 1893
      // (get) Token: 0x06003078 RID: 12408 RVA: 0x006686C8 File Offset: 0x006686C8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(GoodMorningBrief.SavedResearch);
        }
      }

      // Token: 0x17000766 RID: 1894
      // (get) Token: 0x06003079 RID: 12409 RVA: 0x006686D4 File Offset: 0x006686D4
      // (set) Token: 0x0600307A RID: 12410 RVA: 0x006686DC File Offset: 0x006686DC
      [RequiredMember]
      [JsonPropertyName("userID")]
      public string UserId { get; set; }

      // Token: 0x17000767 RID: 1895
      // (get) Token: 0x0600307B RID: 12411 RVA: 0x006686E8 File Offset: 0x006686E8
      // (set) Token: 0x0600307C RID: 12412 RVA: 0x006686F0 File Offset: 0x006686F0
      [RequiredMember]
      [JsonPropertyName("savedAt")]
      public DateTimeOffset SavedAt { get; set; }

      // Token: 0x17000768 RID: 1896
      // (get) Token: 0x0600307D RID: 12413 RVA: 0x006686FC File Offset: 0x006686FC
      // (set) Token: 0x0600307E RID: 12414 RVA: 0x00668704 File Offset: 0x00668704
      [RequiredMember]
      [JsonPropertyName("jobID")]
      public string JobId { get; set; }

      // Token: 0x17000769 RID: 1897
      // (get) Token: 0x0600307F RID: 12415 RVA: 0x00668710 File Offset: 0x00668710
      // (set) Token: 0x06003080 RID: 12416 RVA: 0x00668718 File Offset: 0x00668718
      [RequiredMember]
      [JsonPropertyName("curatedEvidence")]
      public string CuratedEvidence { get; set; }

      // Token: 0x1700076A RID: 1898
      // (get) Token: 0x06003081 RID: 12417 RVA: 0x00668724 File Offset: 0x00668724
      // (set) Token: 0x06003082 RID: 12418 RVA: 0x0066872C File Offset: 0x0066872C
      [Nullable(2)]
      [JsonPropertyName("headline")]
      public string Headline
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700076B RID: 1899
      // (get) Token: 0x06003083 RID: 12419 RVA: 0x00668738 File Offset: 0x00668738
      // (set) Token: 0x06003084 RID: 12420 RVA: 0x00668740 File Offset: 0x00668740
      [Nullable(2)]
      [JsonPropertyName("headlineLine")]
      public string HeadlineLine
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x1700076C RID: 1900
      // (get) Token: 0x06003085 RID: 12421 RVA: 0x0066874C File Offset: 0x0066874C
      // (set) Token: 0x06003086 RID: 12422 RVA: 0x00668754 File Offset: 0x00668754
      [Nullable(new byte[] { 2, 1 })]
      [JsonPropertyName("plateCards")]
      public List<GoodMorningBrief.SavedCard> PlateCards
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x06003087 RID: 12423 RVA: 0x00668760 File Offset: 0x00668760
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SavedResearch");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003088 RID: 12424 RVA: 0x006687AC File Offset: 0x006687AC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("UserId = ");
        builder.Append(this.UserId);
        builder.Append(", SavedAt = ");
        builder.Append(this.SavedAt.ToString());
        builder.Append(", JobId = ");
        builder.Append(this.JobId);
        builder.Append(", CuratedEvidence = ");
        builder.Append(this.CuratedEvidence);
        builder.Append(", Headline = ");
        builder.Append(this.Headline);
        builder.Append(", HeadlineLine = ");
        builder.Append(this.HeadlineLine);
        builder.Append(", PlateCards = ");
        builder.Append(this.PlateCards);
        return true;
      }

      // Token: 0x06003089 RID: 12425 RVA: 0x0066887C File Offset: 0x0066887C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(GoodMorningBrief.SavedResearch left, GoodMorningBrief.SavedResearch right)
      {
        return !(left == right);
      }

      // Token: 0x0600308A RID: 12426 RVA: 0x00668888 File Offset: 0x00668888
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(GoodMorningBrief.SavedResearch left, GoodMorningBrief.SavedResearch right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600308B RID: 12427 RVA: 0x0066889C File Offset: 0x0066889C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<SavedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CuratedEvidence>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Headline>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<HeadlineLine>k__BackingField)) * -1521134295 + EqualityComparer<List<GoodMorningBrief.SavedCard>>.Default.GetHashCode(this.<PlateCards>k__BackingField);
      }

      // Token: 0x0600308C RID: 12428 RVA: 0x0066895C File Offset: 0x0066895C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as GoodMorningBrief.SavedResearch);
      }

      // Token: 0x0600308D RID: 12429 RVA: 0x0066896C File Offset: 0x0066896C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(GoodMorningBrief.SavedResearch other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<UserId>k__BackingField, other.<UserId>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<SavedAt>k__BackingField, other.<SavedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CuratedEvidence>k__BackingField, other.<CuratedEvidence>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Headline>k__BackingField, other.<Headline>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<HeadlineLine>k__BackingField, other.<HeadlineLine>k__BackingField) && EqualityComparer<List<GoodMorningBrief.SavedCard>>.Default.Equals(this.<PlateCards>k__BackingField, other.<PlateCards>k__BackingField));
      }

      // Token: 0x0600308F RID: 12431 RVA: 0x00668A54 File Offset: 0x00668A54
      [CompilerGenerated]
      [SetsRequiredMembers]
      private SavedResearch(GoodMorningBrief.SavedResearch original)
      {
        this.UserId = original.<UserId>k__BackingField;
        this.SavedAt = original.<SavedAt>k__BackingField;
        this.JobId = original.<JobId>k__BackingField;
        this.CuratedEvidence = original.<CuratedEvidence>k__BackingField;
        this.Headline = original.<Headline>k__BackingField;
        this.HeadlineLine = original.<HeadlineLine>k__BackingField;
        this.PlateCards = original.<PlateCards>k__BackingField;
      }

      // Token: 0x06003090 RID: 12432 RVA: 0x00668ABC File Offset: 0x00668ABC
      [Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
      [CompilerFeatureRequired("RequiredMembers")]
      public SavedResearch()
      {
      }
    }

    // Token: 0x0200060D RID: 1549
    [Nullable(0)]
    public sealed class SavedCard : IEquatable<GoodMorningBrief.SavedCard>
    {
      // Token: 0x06003091 RID: 12433 RVA: 0x00668AC4 File Offset: 0x00668AC4
      public SavedCard(string Title, string Description, string AgentPrompt, [Nullable(2)] string ClickySlug, int? RoutineEveryMinutes, [Nullable(2)] string RoutineTitle, [Nullable(2)] string IntegrationName)
      {
        this.Title = Title;
        this.Description = Description;
        this.AgentPrompt = AgentPrompt;
        this.ClickySlug = ClickySlug;
        this.RoutineEveryMinutes = RoutineEveryMinutes;
        this.RoutineTitle = RoutineTitle;
        this.IntegrationName = IntegrationName;
        base..ctor();
      }

      // Token: 0x1700076D RID: 1901
      // (get) Token: 0x06003092 RID: 12434 RVA: 0x00668B04 File Offset: 0x00668B04
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(GoodMorningBrief.SavedCard);
        }
      }

      // Token: 0x1700076E RID: 1902
      // (get) Token: 0x06003093 RID: 12435 RVA: 0x00668B10 File Offset: 0x00668B10
      // (set) Token: 0x06003094 RID: 12436 RVA: 0x00668B18 File Offset: 0x00668B18
      public string Title { get; set; }

      // Token: 0x1700076F RID: 1903
      // (get) Token: 0x06003095 RID: 12437 RVA: 0x00668B24 File Offset: 0x00668B24
      // (set) Token: 0x06003096 RID: 12438 RVA: 0x00668B2C File Offset: 0x00668B2C
      public string Description { get; set; }

      // Token: 0x17000770 RID: 1904
      // (get) Token: 0x06003097 RID: 12439 RVA: 0x00668B38 File Offset: 0x00668B38
      // (set) Token: 0x06003098 RID: 12440 RVA: 0x00668B40 File Offset: 0x00668B40
      public string AgentPrompt { get; set; }

      // Token: 0x17000771 RID: 1905
      // (get) Token: 0x06003099 RID: 12441 RVA: 0x00668B4C File Offset: 0x00668B4C
      // (set) Token: 0x0600309A RID: 12442 RVA: 0x00668B54 File Offset: 0x00668B54
      [Nullable(2)]
      public string ClickySlug
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000772 RID: 1906
      // (get) Token: 0x0600309B RID: 12443 RVA: 0x00668B60 File Offset: 0x00668B60
      // (set) Token: 0x0600309C RID: 12444 RVA: 0x00668B68 File Offset: 0x00668B68
      public int? RoutineEveryMinutes { get; set; }

      // Token: 0x17000773 RID: 1907
      // (get) Token: 0x0600309D RID: 12445 RVA: 0x00668B74 File Offset: 0x00668B74
      // (set) Token: 0x0600309E RID: 12446 RVA: 0x00668B7C File Offset: 0x00668B7C
      [Nullable(2)]
      public string RoutineTitle
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000774 RID: 1908
      // (get) Token: 0x0600309F RID: 12447 RVA: 0x00668B88 File Offset: 0x00668B88
      // (set) Token: 0x060030A0 RID: 12448 RVA: 0x00668B90 File Offset: 0x00668B90
      [Nullable(2)]
      public string IntegrationName
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x060030A1 RID: 12449 RVA: 0x00668B9C File Offset: 0x00668B9C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SavedCard");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060030A2 RID: 12450 RVA: 0x00668BE8 File Offset: 0x00668BE8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Title = ");
        builder.Append(this.Title);
        builder.Append(", Description = ");
        builder.Append(this.Description);
        builder.Append(", AgentPrompt = ");
        builder.Append(this.AgentPrompt);
        builder.Append(", ClickySlug = ");
        builder.Append(this.ClickySlug);
        builder.Append(", RoutineEveryMinutes = ");
        builder.Append(this.RoutineEveryMinutes.ToString());
        builder.Append(", RoutineTitle = ");
        builder.Append(this.RoutineTitle);
        builder.Append(", IntegrationName = ");
        builder.Append(this.IntegrationName);
        return true;
      }

      // Token: 0x060030A3 RID: 12451 RVA: 0x00668CB8 File Offset: 0x00668CB8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(GoodMorningBrief.SavedCard left, GoodMorningBrief.SavedCard right)
      {
        return !(left == right);
      }

      // Token: 0x060030A4 RID: 12452 RVA: 0x00668CC4 File Offset: 0x00668CC4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(GoodMorningBrief.SavedCard left, GoodMorningBrief.SavedCard right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060030A5 RID: 12453 RVA: 0x00668CD8 File Offset: 0x00668CD8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AgentPrompt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<RoutineEveryMinutes>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoutineTitle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<IntegrationName>k__BackingField);
      }

      // Token: 0x060030A6 RID: 12454 RVA: 0x00668D98 File Offset: 0x00668D98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as GoodMorningBrief.SavedCard);
      }

      // Token: 0x060030A7 RID: 12455 RVA: 0x00668DA8 File Offset: 0x00668DA8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(GoodMorningBrief.SavedCard other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AgentPrompt>k__BackingField, other.<AgentPrompt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<RoutineEveryMinutes>k__BackingField, other.<RoutineEveryMinutes>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoutineTitle>k__BackingField, other.<RoutineTitle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<IntegrationName>k__BackingField, other.<IntegrationName>k__BackingField));
      }

      // Token: 0x060030A9 RID: 12457 RVA: 0x00668E90 File Offset: 0x00668E90
      [CompilerGenerated]
      private SavedCard(GoodMorningBrief.SavedCard original)
      {
        this.Title = original.<Title>k__BackingField;
        this.Description = original.<Description>k__BackingField;
        this.AgentPrompt = original.<AgentPrompt>k__BackingField;
        this.ClickySlug = original.<ClickySlug>k__BackingField;
        this.RoutineEveryMinutes = original.<RoutineEveryMinutes>k__BackingField;
        this.RoutineTitle = original.<RoutineTitle>k__BackingField;
        this.IntegrationName = original.<IntegrationName>k__BackingField;
      }

      // Token: 0x060030AA RID: 12458 RVA: 0x00668EF8 File Offset: 0x00668EF8
      [CompilerGenerated]
      public void Deconstruct(out string Title, out string Description, out string AgentPrompt, [Nullable(2)] out string ClickySlug, out int? RoutineEveryMinutes, [Nullable(2)] out string RoutineTitle, [Nullable(2)] out string IntegrationName)
      {
        Title = this.Title;
        Description = this.Description;
        AgentPrompt = this.AgentPrompt;
        ClickySlug = this.ClickySlug;
        RoutineEveryMinutes = this.RoutineEveryMinutes;
        RoutineTitle = this.RoutineTitle;
        IntegrationName = this.IntegrationName;
      }
    }

    // Token: 0x0200060E RID: 1550
    [Nullable(0)]
    public sealed class Evidence : IEquatable<GoodMorningBrief.Evidence>
    {
      // Token: 0x060030AB RID: 12459 RVA: 0x00668F48 File Offset: 0x00668F48
      public Evidence(IReadOnlyList<JsonObject> Findings, [TupleElementNames(new string[] { "Source", "Summary" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> Summaries)
      {
        this.Findings = Findings;
        this.Summaries = Summaries;
        base..ctor();
      }

      // Token: 0x17000775 RID: 1909
      // (get) Token: 0x060030AC RID: 12460 RVA: 0x00668F60 File Offset: 0x00668F60
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(GoodMorningBrief.Evidence);
        }
      }

      // Token: 0x17000776 RID: 1910
      // (get) Token: 0x060030AD RID: 12461 RVA: 0x00668F6C File Offset: 0x00668F6C
      // (set) Token: 0x060030AE RID: 12462 RVA: 0x00668F74 File Offset: 0x00668F74
      public IReadOnlyList<JsonObject> Findings { get; set; }

      // Token: 0x17000777 RID: 1911
      // (get) Token: 0x060030AF RID: 12463 RVA: 0x00668F80 File Offset: 0x00668F80
      // (set) Token: 0x060030B0 RID: 12464 RVA: 0x00668F88 File Offset: 0x00668F88
      [TupleElementNames(new string[] { "Source", "Summary" })]
      [Nullable(new byte[] { 1, 0, 1, 1 })]
      public IReadOnlyList<ValueTuple<string, string>> Summaries
      {
        [return: TupleElementNames(new string[] { "Source", "Summary" })]
        [return: Nullable(new byte[] { 1, 0, 1, 1 })]
        get;
        [param: TupleElementNames(new string[] { "Source", "Summary" })]
        [param: Nullable(new byte[] { 1, 0, 1, 1 })]
        set;
      }

      // Token: 0x060030B1 RID: 12465 RVA: 0x00668F94 File Offset: 0x00668F94
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Evidence");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060030B2 RID: 12466 RVA: 0x00668FE0 File Offset: 0x00668FE0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Findings = ");
        builder.Append(this.Findings);
        builder.Append(", Summaries = ");
        builder.Append(this.Summaries);
        return true;
      }

      // Token: 0x060030B3 RID: 12467 RVA: 0x0066901C File Offset: 0x0066901C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(GoodMorningBrief.Evidence left, GoodMorningBrief.Evidence right)
      {
        return !(left == right);
      }

      // Token: 0x060030B4 RID: 12468 RVA: 0x00669028 File Offset: 0x00669028
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(GoodMorningBrief.Evidence left, GoodMorningBrief.Evidence right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060030B5 RID: 12469 RVA: 0x0066903C File Offset: 0x0066903C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<JsonObject>>.Default.GetHashCode(this.<Findings>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.GetHashCode(this.<Summaries>k__BackingField);
      }

      // Token: 0x060030B6 RID: 12470 RVA: 0x0066907C File Offset: 0x0066907C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as GoodMorningBrief.Evidence);
      }

      // Token: 0x060030B7 RID: 12471 RVA: 0x0066908C File Offset: 0x0066908C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(GoodMorningBrief.Evidence other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<JsonObject>>.Default.Equals(this.<Findings>k__BackingField, other.<Findings>k__BackingField) && EqualityComparer<IReadOnlyList<ValueTuple<string, string>>>.Default.Equals(this.<Summaries>k__BackingField, other.<Summaries>k__BackingField));
      }

      // Token: 0x060030B9 RID: 12473 RVA: 0x006690F0 File Offset: 0x006690F0
      [CompilerGenerated]
      private Evidence(GoodMorningBrief.Evidence original)
      {
        this.Findings = original.<Findings>k__BackingField;
        this.Summaries = original.<Summaries>k__BackingField;
      }

      // Token: 0x060030BA RID: 12474 RVA: 0x00669110 File Offset: 0x00669110
      [CompilerGenerated]
      public void Deconstruct(out IReadOnlyList<JsonObject> Findings, [TupleElementNames(new string[] { "Source", "Summary" })] [Nullable(new byte[] { 1, 0, 1, 1 })] out IReadOnlyList<ValueTuple<string, string>> Summaries)
      {
        Findings = this.Findings;
        Summaries = this.Summaries;
      }
    }

    // Token: 0x0200060F RID: 1551
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001BC4 RID: 7108
      [Nullable(0)]
      public static Func<ProactiveSuggestionCard, GoodMorningBrief.SavedCard> <0>__Saved;
    }
  }
}
