using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Routines;
using HeyClicky.App.Notch;
using HeyClicky.App.OneBrain;
using HeyClicky.App.Portraits;
using HeyClicky.App.Services;
using HeyClicky.App.Settings;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using WinRT;
using WinRT.HeyClickyVtableClasses;

namespace HeyClicky.App.Proactive
{
  // Token: 0x020000F2 RID: 242
  [NullableContext(1)]
  [Nullable(0)]
  [WinRTRuntimeClassName("Windows.Foundation.IClosable")]
  [WinRTExposedType(typeof(HeyClicky_App_Proactive_ProactiveCoordinatorWinRTTypeDetails))]
  public sealed class ProactiveCoordinator : IDisposable
  {
    // Token: 0x17000218 RID: 536
    // (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0055CD54 File Offset: 0x0055CD54
    // (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0055CD5C File Offset: 0x0055CD5C
    [Nullable(2)]
    public static ProactiveCoordinator Current
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x1400002D RID: 45
    // (add) Token: 0x06000BD2 RID: 3026 RVA: 0x0055CD64 File Offset: 0x0055CD64
    // (remove) Token: 0x06000BD3 RID: 3027 RVA: 0x0055CD9C File Offset: 0x0055CD9C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x17000219 RID: 537
    // (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0055CDD4 File Offset: 0x0055CDD4
    // (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0055CDDC File Offset: 0x0055CDDC
    public bool IsFetching { get; private set; }

    // Token: 0x1700021A RID: 538
    // (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0055CDE8 File Offset: 0x0055CDE8
    // (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0055CDF0 File Offset: 0x0055CDF0
    [Nullable(2)]
    public string FetchError
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x1700021B RID: 539
    // (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0055CDFC File Offset: 0x0055CDFC
    // (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0055CE04 File Offset: 0x0055CE04
    public DateTimeOffset? LastFetchedAt { get; private set; }

    // Token: 0x1700021C RID: 540
    // (get) Token: 0x06000BDA RID: 3034 RVA: 0x0055CE10 File Offset: 0x0055CE10
    // (set) Token: 0x06000BDB RID: 3035 RVA: 0x0055CE18 File Offset: 0x0055CE18
    public Guid? AdjustingCardId { get; private set; }

    // Token: 0x1700021D RID: 541
    // (get) Token: 0x06000BDC RID: 3036 RVA: 0x0055CE24 File Offset: 0x0055CE24
    // (set) Token: 0x06000BDD RID: 3037 RVA: 0x0055CE2C File Offset: 0x0055CE2C
    public Guid? CurrentCardId
    {
      get
      {
        return this.currentCardId;
      }
      set
      {
        this.currentCardId = value;
        if (this.IsSuggestionsPageVisible)
        {
          this.UpdateLiveStateHook();
        }
      }
    }

    // Token: 0x1700021E RID: 542
    // (get) Token: 0x06000BDE RID: 3038 RVA: 0x0055CE44 File Offset: 0x0055CE44
    // (set) Token: 0x06000BDF RID: 3039 RVA: 0x0055CE4C File Offset: 0x0055CE4C
    public bool IsSuggestionsPageVisible { get; set; }

    // Token: 0x06000BE0 RID: 3040 RVA: 0x0055CE58 File Offset: 0x0055CE58
    public ProactiveCoordinator(AppHost host, RealStack real)
    {
      ProactiveCoordinator.<>c__DisplayClass61_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass61_0();
      CS$<>8__locals1.real = real;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      this.host = host;
      this.real = CS$<>8__locals1.real;
      this.dq = host.DispatcherQueue;
      this.client = new ProactiveAgentsClient(CS$<>8__locals1.real.Core.Worker);
      this.backend = new AgentBackendClient(CS$<>8__locals1.real.Core.Http, delegate(bool force, CancellationToken ct)
      {
        ProactiveCoordinator.<>c__DisplayClass61_0.<<-ctor>b__0>d <<-ctor>b__0>d;
        <<-ctor>b__0>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
        <<-ctor>b__0>d.<>4__this = CS$<>8__locals1;
        <<-ctor>b__0>d.force = force;
        <<-ctor>b__0>d.ct = ct;
        <<-ctor>b__0>d.<>1__state = -1;
        <<-ctor>b__0>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass61_0.<<-ctor>b__0>d>(ref <<-ctor>b__0>d);
        return <<-ctor>b__0>d.<>t__builder.Task;
      });
      this.meetingsClient = new UpcomingMeetingsClient(CS$<>8__locals1.real.Core.Worker);
      this.clockOffset = ProactiveCoordinator.ReadTestClockOffset();
      this.slotsTimer = this.dq.CreateTimer();
      this.slotsTimer.Interval = ProactiveCoordinator.SlotsPollInterval;
      this.slotsTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        CS$<>8__locals1.<>4__this.EvaluateSlots();
      };
      this.expiryTimer = this.dq.CreateTimer();
      this.expiryTimer.IsRepeating = false;
      this.expiryTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        CS$<>8__locals1.<>4__this.ExpireOutdatedCards();
        CS$<>8__locals1.<>4__this.ScheduleExpiry();
      };
      ProactiveCoordinator.Current = this;
    }

    // Token: 0x1700021F RID: 543
    // (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0055CFF4 File Offset: 0x0055CFF4
    public DispatcherQueue Dispatcher
    {
      get
      {
        return this.dq;
      }
    }

    // Token: 0x06000BE2 RID: 3042 RVA: 0x0055CFFC File Offset: 0x0055CFFC
    private static double IdleSeconds()
    {
      return QuietContext.SecondsSinceLastInput();
    }

    // Token: 0x17000220 RID: 544
    // (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0055D004 File Offset: 0x0055D004
    public SettingsStore CoreSettings
    {
      get
      {
        return this.real.Core.Settings;
      }
    }

    // Token: 0x17000221 RID: 545
    // (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0055D018 File Offset: 0x0055D018
    public DateTimeOffset Now
    {
      get
      {
        return DateTimeOffset.Now + this.clockOffset;
      }
    }

    // Token: 0x06000BE5 RID: 3045 RVA: 0x0055D02C File Offset: 0x0055D02C
    private static TimeSpan ReadTestClockOffset()
    {
      return TimeSpan.Zero;
    }

    // Token: 0x06000BE6 RID: 3046 RVA: 0x0055D034 File Offset: 0x0055D034
    public void Start()
    {
      if (this.started)
      {
        return;
      }
      this.started = true;
      this.wasOnboardingCompleted = this.real.Core.Settings.Read<bool>((AppSettings s) => s.Onboarding.HasCompletedOnboarding);
      this.real.Core.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnAuthOrSettingsChanged));
      };
      this.real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnAuthOrSettingsChanged));
      };
      this.real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        this.dq.TryEnqueue(delegate
        {
          this.OnAgentTaskEvent(e);
        });
      };
      this.real.Agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.DrainQueue));
      };
      this.real.Companion.TurnCompleted += delegate([Nullable(2)] object _, CompanionTurnMetrics _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.NoteAdjustmentTurnCompleted));
      };
      ClickyRoster.Shared.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.RaiseChanged));
      };
      this.host.NotchUi.Window.RawMessage += delegate(uint msg, IntPtr w, IntPtr _)
      {
        if (msg == 536U && (w == (IntPtr)18 || w == (IntPtr)7))
        {
          this.dq.TryEnqueue(new DispatcherQueueHandler(this.OnResumedFromSleep));
        }
      };
      this.OnAuthOrSettingsChanged();
      string text = "Proactive";
      string text2 = "started";
      string text3;
      if (!(this.clockOffset == TimeSpan.Zero))
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(14, 1);
        defaultInterpolatedStringHandler.AppendLiteral(" (test clock ");
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(this.Now.LocalDateTime, "yyyy-MM-dd HH:mm");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "";
      }
      Log.Info(text, text2 + text3);
    }

    // Token: 0x06000BE7 RID: 3047 RVA: 0x0055D1D0 File Offset: 0x0055D1D0
    private void OnResumedFromSleep()
    {
      this.EvaluateSlots();
      this.ScheduleNextGreetingCheck(true);
    }

    // Token: 0x17000222 RID: 546
    // (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0055D1E0 File Offset: 0x0055D1E0
    private bool IsSignedIn
    {
      get
      {
        return this.real.Core.Auth.IsSignedIn && this.real.Core.Auth.UserId != null;
      }
    }

    // Token: 0x17000223 RID: 547
    // (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0055D218 File Offset: 0x0055D218
    public bool IsEnabled
    {
      get
      {
        return ProactiveLanePause.IsLaneEnabled(this.real.Core.Settings.Read<bool>((AppSettings s) => s.ProactiveAgentSuggestionsEnabled), true);
      }
    }

    // Token: 0x17000224 RID: 548
    // (get) Token: 0x06000BEA RID: 3050 RVA: 0x0055D254 File Offset: 0x0055D254
    private bool HasCompletedOnboarding
    {
      get
      {
        return this.real.Core.Settings.Read<bool>((AppSettings s) => s.Onboarding.HasCompletedOnboarding);
      }
    }

    // Token: 0x17000225 RID: 549
    // (get) Token: 0x06000BEB RID: 3051 RVA: 0x0055D28C File Offset: 0x0055D28C
    private bool CanAdvancePastPermissions
    {
      get
      {
        return this.host.Services.Permissions.GetStatus(PermissionKind.Microphone) == PermissionStatus.Granted && this.host.Services.Permissions.GetStatus(PermissionKind.ScreenCapture) == PermissionStatus.Granted;
      }
    }

    // Token: 0x17000226 RID: 550
    // (get) Token: 0x06000BEC RID: 3052 RVA: 0x0055D2C4 File Offset: 0x0055D2C4
    private bool IsTutorialEngaged
    {
      get
      {
        return this.real.Tutorial.IsEngaged;
      }
    }

    // Token: 0x17000227 RID: 551
    // (get) Token: 0x06000BED RID: 3053 RVA: 0x0055D2D8 File Offset: 0x0055D2D8
    private bool IsWithinPostOnboardingQuietPeriod
    {
      get
      {
        DateTimeOffset? dateTimeOffset = this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.OnboardingFirstCompletedAt);
        return dateTimeOffset != null && ProactiveCadenceRules.IsBeforeFirstMorning(dateTimeOffset.GetValueOrDefault().LocalDateTime, this.Now.LocalDateTime);
      }
    }

    // Token: 0x06000BEE RID: 3054 RVA: 0x0055D340 File Offset: 0x0055D340
    private void OnAuthOrSettingsChanged()
    {
      bool hasCompletedOnboarding = this.HasCompletedOnboarding;
      if (hasCompletedOnboarding && !this.wasOnboardingCompleted)
      {
        if (this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.OnboardingFirstCompletedAt) == null)
        {
          this.store.Update(delegate(ProactiveStoreData d)
          {
            d.OnboardingFirstCompletedAt = new DateTimeOffset?(this.Now);
          });
        }
      }
      this.wasOnboardingCompleted = hasCompletedOnboarding;
      string text = (this.IsSignedIn ? (this.real.Core.Endpoints.WorkerBaseUrl.AbsoluteUri.TrimEnd('/') + "|" + this.real.Core.Auth.UserId) : null);
      bool flag = false;
      if (text != this.accountKey)
      {
        flag = text != null;
        this.CancelJob();
        this.accountKey = text;
        List<ProactiveSuggestionCard> list;
        if (text != null)
        {
          list = this.store.ReadAccount<List<ProactiveSuggestionCard>>(text, ([Nullable(1)] ProactiveAccountState a) => a.Cards.ToList<ProactiveSuggestionCard>());
        }
        else
        {
          list = new List<ProactiveSuggestionCard>();
        }
        this.cards = list;
        List<QueuedProactiveTask> list2;
        if (text != null)
        {
          list2 = this.store.ReadAccount<List<QueuedProactiveTask>>(text, ([Nullable(1)] ProactiveAccountState a) => a.Queue.Select<QueuedProactiveTask, QueuedProactiveTask>(delegate(QueuedProactiveTask e)
          {
            QueuedProactiveTask queuedProactiveTask = e.<Clone>$();
            queuedProactiveTask.IsPaused = true;
            queuedProactiveTask.IsLaunching = false;
            return queuedProactiveTask;
          }).ToList<QueuedProactiveTask>());
        }
        else
        {
          list2 = new List<QueuedProactiveTask>();
        }
        this.queue = list2;
        DateTimeOffset? dateTimeOffset;
        if (text != null)
        {
          dateTimeOffset = this.store.ReadAccount<DateTimeOffset?>(text, (ProactiveAccountState a) => a.SuggestionsLastFetchedAt);
        }
        else
        {
          dateTimeOffset = null;
        }
        this.LastFetchedAt = dateTimeOffset;
        this.DismissMorningNotice();
        this.ResetMeetingsForAuthChange();
        this.StopNotifications(true);
        if (text != null)
        {
          this.PersistQueue();
        }
      }
      if (text == null || !this.IsEnabled)
      {
        this.StopSuggestionLane(text == null);
        if (text != null)
        {
          this.ExpireOutdatedCards();
        }
      }
      else
      {
        this.StartSuggestionLane();
      }
      if (flag)
      {
        this.real.Agents.Jobs.RelinkWaitingJobsToRestoredCards(this.cards.Select<ProactiveSuggestionCard, ValueTuple<Guid, string>>((ProactiveSuggestionCard c) => new ValueTuple<Guid, string>(c.Id, c.Description)).ToList<ValueTuple<Guid, string>>());
      }
      this.UpdateGreetingWatcher();
      this.UpdateMeetingsScheduler();
      this.UpdateNotificationPolling();
      this.UpdateIntegrationMonitor();
      this.RaiseChanged();
    }

    // Token: 0x06000BEF RID: 3055 RVA: 0x0055D580 File Offset: 0x0055D580
    private void StartSuggestionLane()
    {
      this.ExpireOutdatedCards();
      this.ScheduleExpiry();
      this.StartJob(ProactiveCheckReason.HomeSpace, true);
      this.RebuildMorningInvitationIfNeeded("launch");
      if (!this.slotsTimer.IsRunning)
      {
        this.slotsTimer.Start();
      }
      this.RefreshIntegrationsAsync();
      this.EvaluateSlots();
    }

    // Token: 0x06000BF0 RID: 3056 RVA: 0x0055D5D4 File Offset: 0x0055D5D4
    private void StopSuggestionLane(bool clearCards)
    {
      this.slotsTimer.Stop();
      this.CancelJob();
      this.DismissMorningNotice();
      if (clearCards)
      {
        this.cards = new List<ProactiveSuggestionCard>();
        this.queue = new List<QueuedProactiveTask>();
        this.expiryTimer.Stop();
      }
    }

    // Token: 0x06000BF1 RID: 3057 RVA: 0x0055D614 File Offset: 0x0055D614
    public void Dispose()
    {
      this.slotsTimer.Stop();
      this.expiryTimer.Stop();
      this.CancelJob();
      this.DisposeMeetings();
      this.StopNotifications(false);
      this.StopIntegrationMonitor();
      DispatcherQueueTimer dispatcherQueueTimer = this.greetingTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      if (ProactiveCoordinator.Current == this)
      {
        ProactiveCoordinator.Current = null;
      }
    }

    // Token: 0x06000BF2 RID: 3058 RVA: 0x0055D670 File Offset: 0x0055D670
    private void RaiseChanged()
    {
      if (this.AdjustingCardId != null || this.IsSuggestionsPageVisible)
      {
        this.UpdateLiveStateHook();
      }
      try
      {
        Action changed = this.Changed;
        if (changed != null)
        {
          changed();
        }
      }
      catch (Exception ex)
      {
        Log.Error("Proactive", "Changed handler threw", ex);
      }
    }

    // Token: 0x17000228 RID: 552
    // (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0055D6D4 File Offset: 0x0055D6D4
    private ProactiveCadence Cadence
    {
      get
      {
        return ProactiveCadence.Daily;
      }
    }

    // Token: 0x06000BF4 RID: 3060 RVA: 0x0055D6D8 File Offset: 0x0055D6D8
    private void EvaluateSlots()
    {
      if (this.accountKey == null || !this.IsEnabled)
      {
        return;
      }
      this.ExpireOutdatedCards();
      this.SyncStatusesFromServer(false);
      this.RebuildMorningInvitationIfNeeded("rebuilt from today's cards");
      if (ProactiveCoordinator.IdleSeconds() > ProactiveCoordinator.ActiveRecency.TotalSeconds)
      {
        return;
      }
      this.EnsureMorningSweepStarted();
    }

    // Token: 0x06000BF5 RID: 3061 RVA: 0x0055D728 File Offset: 0x0055D728
    private void EnsureMorningSweepStarted()
    {
      this.ExpireOutdatedCards();
      string text = this.accountKey;
      if (text == null || !this.IsEnabled || !this.HasCompletedOnboarding || !this.CanAdvancePastPermissions || this.IsTutorialEngaged || this.IsWithinPostOnboardingQuietPeriod || this.IsMorningLaneResting)
      {
        return;
      }
      ValueTuple<ProactiveSlotDayState, DateTimeOffset?> valueTuple = this.store.ReadAccount<ValueTuple<ProactiveSlotDayState, DateTimeOffset?>>(text, ([Nullable(1)] ProactiveAccountState a) => new ValueTuple<ProactiveSlotDayState, DateTimeOffset?>(a.Slots, a.IgnoredSuggestionsBreakUntil));
      DateTimeOffset? dateTimeOffset = valueTuple.Item2;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        if (this.Now < valueOrDefault)
        {
          return;
        }
      }
      ProactiveCadence cadence = this.Cadence;
      DateTime localDateTime = this.Now.LocalDateTime;
      dateTimeOffset = valueTuple.Item1.MorningFiredAt;
      DateTime? dateTime = ((dateTimeOffset != null) ? new DateTime?(dateTimeOffset.GetValueOrDefault().LocalDateTime) : null);
      dateTimeOffset = valueTuple.Item1.RetryAfter;
      if (!cadence.IsDue(localDateTime, dateTime, (dateTimeOffset != null) ? new DateTime?(dateTimeOffset.GetValueOrDefault().LocalDateTime) : null))
      {
        return;
      }
      this.StartJob(ProactiveCheckReason.Morning, false);
    }

    // Token: 0x06000BF6 RID: 3062 RVA: 0x0055D85C File Offset: 0x0055D85C
    private void RecordAttempt()
    {
      this.UpdateSlots(delegate(ProactiveSlotDayState s)
      {
        ProactiveSlotDayState proactiveSlotDayState = s.<Clone>$();
        proactiveSlotDayState.RetryAfter = new DateTimeOffset?(this.Now + TimeSpan.FromMinutes(5L));
        return proactiveSlotDayState;
      });
    }

    // Token: 0x06000BF7 RID: 3063 RVA: 0x0055D870 File Offset: 0x0055D870
    private void RecordFailure()
    {
      this.UpdateSlots(delegate(ProactiveSlotDayState s)
      {
        int num = s.ConsecutiveFailedAttempts.GetValueOrDefault() + 1;
        ProactiveSlotDayState proactiveSlotDayState = s.<Clone>$();
        proactiveSlotDayState.ConsecutiveFailedAttempts = new int?(num);
        proactiveSlotDayState.RetryAfter = new DateTimeOffset?(this.Now + ProactiveSlotDayState.RetryDelay(num));
        return proactiveSlotDayState;
      });
    }

    // Token: 0x06000BF8 RID: 3064 RVA: 0x0055D884 File Offset: 0x0055D884
    private void RecordSuccess()
    {
      this.UpdateSlots((ProactiveSlotDayState _) => new ProactiveSlotDayState(new DateTimeOffset?(this.Now), null, null));
    }

    // Token: 0x06000BF9 RID: 3065 RVA: 0x0055D898 File Offset: 0x0055D898
    private void UpdateSlots(Func<ProactiveSlotDayState, ProactiveSlotDayState> change)
    {
      string text = this.accountKey;
      if (text != null)
      {
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.Slots = change(a.Slots);
        });
      }
    }

    // Token: 0x06000BFA RID: 3066 RVA: 0x0055D8D4 File Offset: 0x0055D8D4
    public bool StartJob(ProactiveCheckReason reason, bool resumeOnly = false)
    {
      ProactiveCoordinator.<>c__DisplayClass97_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass97_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.jobCts == null)
      {
        CS$<>8__locals1.key = this.accountKey;
        if (CS$<>8__locals1.key != null && this.IsEnabled && this.HasCompletedOnboarding && this.CanAdvancePastPermissions && !this.IsTutorialEngaged)
        {
          CS$<>8__locals1.pending = this.store.ReadAccount<PendingSuggestionJob>(CS$<>8__locals1.key, ([Nullable(1)] ProactiveAccountState a) => a.PendingJob);
          if (CS$<>8__locals1.pending != null && DateTimeOffset.Now - CS$<>8__locals1.pending.StartedAt >= ProactiveCoordinator.JobReceiptLifetime)
          {
            this.store.UpdateAccount(CS$<>8__locals1.key, delegate(ProactiveAccountState a)
            {
              a.PendingJob = null;
            });
            CS$<>8__locals1.pending = null;
          }
          if (resumeOnly && CS$<>8__locals1.pending == null)
          {
            return false;
          }
          ProactiveCoordinator.<>c__DisplayClass97_0 CS$<>8__locals2 = CS$<>8__locals1;
          PendingSuggestionJob pending = CS$<>8__locals1.pending;
          CS$<>8__locals2.origin = ((pending != null) ? pending.CheckReason : reason);
          if (CS$<>8__locals1.origin == ProactiveCheckReason.Morning && this.IsWithinPostOnboardingQuietPeriod)
          {
            return false;
          }
          if (CS$<>8__locals1.origin == ProactiveCheckReason.Morning)
          {
            PendingSuggestionJob pending2 = CS$<>8__locals1.pending;
            this.morningSweepStartedAt = new DateTimeOffset?((pending2 != null) ? pending2.StartedAt : this.Now);
          }
          CS$<>8__locals1.runId = Guid.NewGuid();
          this.jobRunId = new Guid?(CS$<>8__locals1.runId);
          CS$<>8__locals1.cts = (this.jobCts = new CancellationTokenSource());
          this.IsFetching = true;
          this.FetchError = null;
          this.RecordAttempt();
          this.RaiseChanged();
          string text = "Proactive";
          string text2;
          if (CS$<>8__locals1.pending != null)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
            defaultInterpolatedStringHandler.AppendLiteral("resuming suggestion job ");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.pending.JobId);
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.origin.Wire());
            defaultInterpolatedStringHandler.AppendLiteral(")");
            text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text2 = "suggestion sweep starting (" + CS$<>8__locals1.origin.Wire() + ")";
          }
          Log.Info(text, text2);
          Task.Run(delegate
          {
            ProactiveCoordinator.<>c__DisplayClass97_0.<<StartJob>b__2>d <<StartJob>b__2>d;
            <<StartJob>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<StartJob>b__2>d.<>4__this = CS$<>8__locals1;
            <<StartJob>b__2>d.<>1__state = -1;
            <<StartJob>b__2>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass97_0.<<StartJob>b__2>d>(ref <<StartJob>b__2>d);
            return <<StartJob>b__2>d.<>t__builder.Task;
          });
          return true;
        }
      }
      return false;
    }

    // Token: 0x06000BFB RID: 3067 RVA: 0x0055DB08 File Offset: 0x0055DB08
    private void FinishJob(Guid runId, string key, ProactiveCheckReason origin, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ProactiveSuggestionCard> result, [Nullable(2)] string error, bool cancelled)
    {
      if (this.jobRunId != runId)
      {
        return;
      }
      this.jobRunId = null;
      this.jobCts = null;
      this.IsFetching = false;
      if (cancelled || this.accountKey != key)
      {
        this.RaiseChanged();
        return;
      }
      if (result == null)
      {
        this.RecordFailure();
        this.FetchError = error ?? "Could not load suggestions. We’ll try again in a little while.";
        this.RaiseChanged();
        return;
      }
      this.LastFetchedAt = new DateTimeOffset?(this.Now);
      this.store.UpdateAccount(key, delegate(ProactiveAccountState a)
      {
        a.SuggestionsLastFetchedAt = this.LastFetchedAt;
      });
      this.RecordSuccess();
      if (result.Count > 0)
      {
        this.PresentCards(result, origin == ProactiveCheckReason.Morning);
      }
      else
      {
        Log.Info("Proactive", "suggestion sweep chose silence (" + origin.Wire() + ")");
      }
      this.RaiseChanged();
    }

    // Token: 0x06000BFC RID: 3068 RVA: 0x0055DBFC File Offset: 0x0055DBFC
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private async Task<IReadOnlyList<ProactiveSuggestionCard>> RunJobAsync(ProactiveCheckReason reason, string key, CancellationToken ct)
    {
      ProactiveRequestInputs proactiveRequestInputs = await this.BuildRequestInputsAsync(reason, ct).ConfigureAwait(false);
      ProactiveJobStatus proactiveJobStatus = await this.client.StartJobAsync(proactiveRequestInputs, this.Now, ct).ConfigureAwait(false);
      IReadOnlyList<ProactiveSuggestionCard> readOnlyList;
      if (proactiveJobStatus.IsSettled)
      {
        readOnlyList = this.SettledCards(proactiveJobStatus, key);
      }
      else
      {
        PendingSuggestionJob job = new PendingSuggestionJob(proactiveJobStatus.JobId, DateTimeOffset.Now, reason);
        this.store.UpdateAccount(key, delegate(ProactiveAccountState a)
        {
          a.PendingJob = job;
        });
        string text = "Proactive";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
        defaultInterpolatedStringHandler.AppendLiteral("suggestion job ");
        defaultInterpolatedStringHandler.AppendFormatted(job.JobId);
        defaultInterpolatedStringHandler.AppendLiteral(" started (");
        defaultInterpolatedStringHandler.AppendFormatted(reason.Wire());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        readOnlyList = await this.PollJobAsync(job, key, ct).ConfigureAwait(false);
      }
      return readOnlyList;
    }

    // Token: 0x06000BFD RID: 3069 RVA: 0x0055DC58 File Offset: 0x0055DC58
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private async Task<IReadOnlyList<ProactiveSuggestionCard>> PollJobAsync(PendingSuggestionJob job, string key, CancellationToken ct)
    {
      int failures = 0;
      while (DateTimeOffset.Now - job.StartedAt < ProactiveCoordinator.JobReceiptLifetime)
      {
        await Task.Delay((DateTimeOffset.Now - job.StartedAt >= ProactiveCoordinator.JobFastPollWindow) ? ProactiveCoordinator.JobSlowPollInterval : ProactiveCoordinator.JobPollInterval, ct).ConfigureAwait(false);
        try
        {
          ProactiveJobStatus proactiveJobStatus = await this.client.PollJobAsync(job.JobId, job.CheckReason, this.Now, ct).ConfigureAwait(false);
          failures = 0;
          if (proactiveJobStatus.IsSettled)
          {
            this.store.UpdateAccount(key, delegate(ProactiveAccountState a)
            {
              a.PendingJob = null;
            });
            string text = "Proactive";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
            defaultInterpolatedStringHandler.AppendLiteral("suggestion job ");
            defaultInterpolatedStringHandler.AppendFormatted(job.JobId);
            defaultInterpolatedStringHandler.AppendLiteral(" settled: ");
            defaultInterpolatedStringHandler.AppendFormatted<ProactiveJobState>(proactiveJobStatus.State);
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            return this.SettledCards(proactiveJobStatus, key);
          }
        }
        catch (OperationCanceledException)
        {
          throw;
        }
        catch (Exception ex)
        {
          string text2 = "Proactive";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
          defaultInterpolatedStringHandler.AppendLiteral("suggestion job poll failed (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(++failures);
          defaultInterpolatedStringHandler.AppendLiteral("): ");
          defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          if (failures >= 8)
          {
            return null;
          }
        }
      }
      this.store.UpdateAccount(key, delegate(ProactiveAccountState a)
      {
        a.PendingJob = null;
      });
      Log.Info("Proactive", "suggestion job timed out client-side");
      return null;
    }

    // Token: 0x06000BFE RID: 3070 RVA: 0x0055DCB4 File Offset: 0x0055DCB4
    [return: Nullable(new byte[] { 2, 1 })]
    private IReadOnlyList<ProactiveSuggestionCard> SettledCards(ProactiveJobStatus status, string key)
    {
      DateTimeOffset? breakUntil = status.BreakUntil;
      if (breakUntil != null)
      {
        DateTimeOffset until = breakUntil.GetValueOrDefault();
        string text = "Proactive";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 1);
        defaultInterpolatedStringHandler.AppendLiteral("morning suggestions on an ignored-suggestions break until ");
        defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(until, "u");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        this.store.UpdateAccount(key, delegate(ProactiveAccountState a)
        {
          a.IgnoredSuggestionsBreakUntil = new DateTimeOffset?(until);
        });
      }
      if (status.State == ProactiveJobState.Failed)
      {
        Log.Info("Proactive", "suggestion job " + status.JobId + " failed: " + (status.Error ?? "unknown"));
        return null;
      }
      IReadOnlyList<ProactiveSuggestionCard> readOnlyList = status.Cards ?? Array.Empty<ProactiveSuggestionCard>();
      string userId = this.real.Core.Auth.UserId;
      if (userId != null)
      {
        GoodMorningBrief shared = GoodMorningBrief.Shared;
        if (shared != null)
        {
          shared.NoteSettledSweep(status, userId, readOnlyList);
        }
      }
      return readOnlyList;
    }

    // Token: 0x06000BFF RID: 3071 RVA: 0x0055DDAC File Offset: 0x0055DDAC
    private void CancelJob()
    {
      CancellationTokenSource cancellationTokenSource = this.jobCts;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.jobCts = null;
      this.jobRunId = null;
      this.IsFetching = false;
      this.FetchError = null;
    }

    // Token: 0x06000C00 RID: 3072 RVA: 0x0055DDE0 File Offset: 0x0055DDE0
    internal async Task<ProactiveRequestInputs> BuildRequestInputsAsync(ProactiveCheckReason reason, CancellationToken ct)
    {
      IReadOnlyList<AgentIntegrationInfo> readOnlyList;
      if (this.catalog.Count > 0)
      {
        readOnlyList = this.catalog;
      }
      else
      {
        readOnlyList = await this.RefreshIntegrationsAsync().ConfigureAwait(false);
      }
      List<string> list = (from i in readOnlyList
        where i.IsActiveConnection
        select i.DisplayName).ToList<string>();
      IReadOnlyList<ClickyRoutine> routines = this.real.Agents.Routines.Routines;
      if (ObsidianVault.ConfiguredPath != null)
      {
        list.Add("Obsidian");
      }
      List<ValueTuple<string, string>> list2 = (from c in CustomConnectorStore.Load()
        orderby c.CreatedAt
        select new ValueTuple<string, string>(c.Name, c.EffectiveCapabilityDescription)).ToList<ValueTuple<string, string>>();
      ProactiveRequestInputs proactiveRequestInputs = new ProactiveRequestInputs(list, this.store.Read<List<ProactiveHistoryEntry>>(([Nullable(1)] ProactiveStoreData d) => d.History.ToList<ProactiveHistoryEntry>()), reason, Array.Empty<ProactiveClickyDescriptor>(), list2);
      proactiveRequestInputs.ReadsForMorningPaper = reason <= ProactiveCheckReason.Debug && GoodMorningBrief.IsMorningPaperEnabled(this.real.Core.Settings);
      proactiveRequestInputs.ExistingRoutines = routines.Select<ClickyRoutine, ValueTuple<string, int>>((ClickyRoutine r) => new ValueTuple<string, int>(r.Title, r.IntervalMinutes)).ToList<ValueTuple<string, int>>();
      return proactiveRequestInputs;
    }

    // Token: 0x06000C01 RID: 3073 RVA: 0x0055DE2C File Offset: 0x0055DE2C
    private static Guid StarterId(StarterTaskCard s)
    {
      return new Guid(MD5.HashData(Encoding.UTF8.GetBytes("starter|" + s.Title + "|" + s.AgentPrompt)));
    }

    // Token: 0x06000C02 RID: 3074 RVA: 0x0055DE60 File Offset: 0x0055DE60
    private static ProactiveSuggestionCard FromStarter(StarterTaskCard s)
    {
      return new ProactiveSuggestionCard
      {
        Id = ProactiveCoordinator.StarterId(s),
        Title = s.Title,
        Description = s.Description,
        AgentPrompt = s.AgentPrompt,
        CheckReason = ProactiveCheckReason.Onboarding,
        ServerSuggestionId = s.SuggestionId,
        ClickySlug = s.ClickySlug,
        Routine = ((s.RoutineEveryMinutes > 0) ? new ProactiveRoutine(s.RoutineEveryMinutes.Value, s.RoutineTitle ?? s.Title) : null)
      };
    }

    // Token: 0x17000229 RID: 553
    // (get) Token: 0x06000C03 RID: 3075 RVA: 0x0055DF08 File Offset: 0x0055DF08
    public IReadOnlyList<ProactiveSuggestionCard> VisibleCards
    {
      get
      {
        DateTimeOffset now = this.Now;
        IEnumerable<StarterTaskCard> starterCards = ClickyRoster.Shared.StarterCards;
        Func<StarterTaskCard, ProactiveSuggestionCard> func;
        if ((func = ProactiveCoordinator.<>O.<0>__FromStarter) == null)
        {
          func = (ProactiveCoordinator.<>O.<0>__FromStarter = new Func<StarterTaskCard, ProactiveSuggestionCard>(ProactiveCoordinator.FromStarter));
        }
        return starterCards.Select<StarterTaskCard, ProactiveSuggestionCard>(func).Concat<ProactiveSuggestionCard>(this.cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => !c.IsExpired(now))).ToList<ProactiveSuggestionCard>();
      }
    }

    // Token: 0x1700022A RID: 554
    // (get) Token: 0x06000C04 RID: 3076 RVA: 0x0055DF74 File Offset: 0x0055DF74
    public IReadOnlyList<QueuedProactiveTask> Queue
    {
      get
      {
        return this.queue;
      }
    }

    // Token: 0x06000C05 RID: 3077 RVA: 0x0055DF7C File Offset: 0x0055DF7C
    [NullableContext(2)]
    private ProactiveSuggestionCard FindCard(Guid id)
    {
      ProactiveSuggestionCard proactiveSuggestionCard;
      if ((proactiveSuggestionCard = this.cards.FirstOrDefault<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.Id == id)) == null)
      {
        IEnumerable<StarterTaskCard> enumerable = ClickyRoster.Shared.StarterCards.Where<StarterTaskCard>((StarterTaskCard s) => ProactiveCoordinator.StarterId(s) == id);
        Func<StarterTaskCard, ProactiveSuggestionCard> func;
        if ((func = ProactiveCoordinator.<>O.<0>__FromStarter) == null)
        {
          func = (ProactiveCoordinator.<>O.<0>__FromStarter = new Func<StarterTaskCard, ProactiveSuggestionCard>(ProactiveCoordinator.FromStarter));
        }
        proactiveSuggestionCard = enumerable.Select<StarterTaskCard, ProactiveSuggestionCard>(func).FirstOrDefault<ProactiveSuggestionCard>();
      }
      return proactiveSuggestionCard;
    }

    // Token: 0x06000C06 RID: 3078 RVA: 0x0055DFF4 File Offset: 0x0055DFF4
    private void RemoveCard(Guid id)
    {
      StarterTaskCard starterTaskCard = ClickyRoster.Shared.StarterCards.FirstOrDefault<StarterTaskCard>((StarterTaskCard s) => ProactiveCoordinator.StarterId(s) == id);
      if (starterTaskCard != null)
      {
        ClickyRoster.Shared.RemoveStarterCard(starterTaskCard);
      }
      this.cards.RemoveAll((ProactiveSuggestionCard c) => c.Id == id);
      this.PersistCards();
      this.ReconcileMorningNotice();
    }

    // Token: 0x06000C07 RID: 3079 RVA: 0x0055E05C File Offset: 0x0055E05C
    private void PersistCards()
    {
      string text = this.accountKey;
      if (text != null)
      {
        List<ProactiveSuggestionCard> snapshot = this.cards.ToList<ProactiveSuggestionCard>();
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.Cards = snapshot;
        });
      }
    }

    // Token: 0x06000C08 RID: 3080 RVA: 0x0055E0A4 File Offset: 0x0055E0A4
    private void PersistQueue()
    {
      string text = this.accountKey;
      if (text != null)
      {
        List<QueuedProactiveTask> snapshot = this.queue.ToList<QueuedProactiveTask>();
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.Queue = snapshot;
        });
      }
    }

    // Token: 0x06000C09 RID: 3081 RVA: 0x0055E0EC File Offset: 0x0055E0EC
    private void PresentCards(IReadOnlyList<ProactiveSuggestionCard> fresh, bool announce)
    {
      Dictionary<int, ProactiveSuggestionCard> kept = this.cards.Where<ProactiveSuggestionCard>(delegate(ProactiveSuggestionCard c)
      {
        int? serverSuggestionId = c.ServerSuggestionId;
        if (serverSuggestionId != null)
        {
          int id = serverSuggestionId.GetValueOrDefault();
          return fresh.Any<ProactiveSuggestionCard>(delegate(ProactiveSuggestionCard f)
          {
            int? serverSuggestionId2 = f.ServerSuggestionId;
            int id2 = id;
            return (serverSuggestionId2.GetValueOrDefault() == id2) & (serverSuggestionId2 != null);
          });
        }
        return false;
      }).ToDictionary<ProactiveSuggestionCard, int>((ProactiveSuggestionCard c) => c.ServerSuggestionId.Value);
      fresh = fresh.Select<ProactiveSuggestionCard, ProactiveSuggestionCard>(delegate(ProactiveSuggestionCard f)
      {
        int? serverSuggestionId3 = f.ServerSuggestionId;
        if (serverSuggestionId3 != null)
        {
          int valueOrDefault = serverSuggestionId3.GetValueOrDefault();
          ProactiveSuggestionCard proactiveSuggestionCard;
          if (kept.TryGetValue(valueOrDefault, out proactiveSuggestionCard))
          {
            return proactiveSuggestionCard;
          }
        }
        return f;
      }).ToList<ProactiveSuggestionCard>();
      List<ProactiveSuggestionCard> stale = this.cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => !fresh.Any<ProactiveSuggestionCard>((ProactiveSuggestionCard f) => f.Id == c.Id)).ToList<ProactiveSuggestionCard>();
      DateTimeOffset now = this.Now;
      if (stale.Count > 0)
      {
        this.store.Update(delegate(ProactiveStoreData d)
        {
          foreach (ProactiveSuggestionCard proactiveSuggestionCard2 in stale)
          {
            d.History = ProactiveHistory.RecordUndecided(d.History, proactiveSuggestionCard2, ProactiveResolution.Replaced, now);
          }
        });
        this.Report(stale, "replaced", null);
        ClickyAnalytics.TrackProactiveAgentsUnactioned(stale.Select<ProactiveSuggestionCard, string>((ProactiveSuggestionCard c) => c.Title).ToList<string>(), "replaced", stale[0].CheckReason.Wire());
      }
      string text = "Proactive";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
      defaultInterpolatedStringHandler.AppendLiteral("presenting ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(fresh.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" suggestion card(s) (");
      defaultInterpolatedStringHandler.AppendFormatted(fresh[0].CheckReason.Wire());
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.cards = fresh.ToList<ProactiveSuggestionCard>();
      this.PersistCards();
      if (announce && fresh.Any<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.CheckReason == ProactiveCheckReason.Morning && !kept.ContainsKey(c.ServerSuggestionId.GetValueOrDefault(-1))))
      {
        this.QueueMorningNotice();
      }
      this.ScheduleExpiry();
      List<ProactiveSuggestionCard> list = fresh.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard f) => !kept.ContainsKey(f.ServerSuggestionId.GetValueOrDefault(-1))).ToList<ProactiveSuggestionCard>();
      this.Report(list, "presented", null);
      if (list.Count > 0)
      {
        ClickyAnalytics.TrackProactiveAgentsRecommended(list.Select<ProactiveSuggestionCard, string>((ProactiveSuggestionCard c) => c.Title).ToList<string>(), list[0].CheckReason.Wire());
      }
      this.RaiseChanged();
    }

    // Token: 0x06000C0A RID: 3082 RVA: 0x0055E344 File Offset: 0x0055E344
    public bool AppendConversationOffer(ProactiveSuggestionCard card)
    {
      if (this.cards.Any<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => string.Equals(c.Title, card.Title, StringComparison.OrdinalIgnoreCase)))
      {
        return false;
      }
      List<ProactiveSuggestionCard> list = this.cards;
      int num = 0;
      ProactiveSuggestionCard proactiveSuggestionCard = card.<Clone>$();
      proactiveSuggestionCard.CheckReason = ProactiveCheckReason.Conversation;
      proactiveSuggestionCard.CreatedAt = this.Now;
      list.Insert(num, proactiveSuggestionCard);
      this.PersistCards();
      this.Report(new <>z__ReadOnlySingleElementList<ProactiveSuggestionCard>(card), "presented", null);
      ClickyAnalytics.TrackProactiveAgentsRecommended(new <>z__ReadOnlySingleElementList<string>(card.Title), ProactiveCheckReason.Conversation.Wire());
      this.RaiseChanged();
      return true;
    }

    // Token: 0x06000C0B RID: 3083 RVA: 0x0055E3E4 File Offset: 0x0055E3E4
    private void Report(IReadOnlyList<ProactiveSuggestionCard> list, string status, [Nullable(2)] string denyReason = null)
    {
      List<int> list2 = (from c in list
        select c.ServerSuggestionId into i
        where i != null
        select i).Cast<int>().ToList<int>();
      if (list2.Count > 0)
      {
        this.client.ReportResolutionAsync(list2, status, denyReason);
      }
    }

    // Token: 0x06000C0C RID: 3084 RVA: 0x0055E460 File Offset: 0x0055E460
    public void MarkViewed(Guid id)
    {
      int num = this.cards.FindIndex((ProactiveSuggestionCard c) => c.Id == id);
      if (num < 0 || this.cards[num].ViewedAt != null)
      {
        return;
      }
      List<ProactiveSuggestionCard> list = this.cards;
      int num2 = num;
      ProactiveSuggestionCard proactiveSuggestionCard = this.cards[num].<Clone>$();
      proactiveSuggestionCard.ViewedAt = new DateTimeOffset?(this.Now);
      list[num2] = proactiveSuggestionCard;
      this.PersistCards();
      this.Report(new <>z__ReadOnlySingleElementList<ProactiveSuggestionCard>(this.cards[num]), "viewed", null);
    }

    // Token: 0x06000C0D RID: 3085 RVA: 0x0055E504 File Offset: 0x0055E504
    private void ScheduleExpiry()
    {
      this.expiryTimer.Stop();
      DateTimeOffset dateTimeOffset = (from c in this.cards
        select c.ExpiresAt into e
        where e != null
        select e.Value).DefaultIfEmpty<DateTimeOffset>().Min<DateTimeOffset>();
      if (dateTimeOffset == default(DateTimeOffset))
      {
        return;
      }
      TimeSpan timeSpan = dateTimeOffset - this.Now;
      this.expiryTimer.Interval = ((timeSpan < TimeSpan.FromSeconds(1L)) ? TimeSpan.FromSeconds(1L) : ((timeSpan > TimeSpan.FromHours(6)) ? TimeSpan.FromHours(6) : timeSpan));
      this.expiryTimer.Start();
    }

    // Token: 0x06000C0E RID: 3086 RVA: 0x0055E600 File Offset: 0x0055E600
    public void ExpireOutdatedCards()
    {
      DateTimeOffset now = this.Now;
      List<ProactiveSuggestionCard> expired = this.cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.IsExpired(now)).ToList<ProactiveSuggestionCard>();
      if (expired.Count == 0)
      {
        return;
      }
      this.store.Update(delegate(ProactiveStoreData d)
      {
        foreach (ProactiveSuggestionCard proactiveSuggestionCard in expired)
        {
          d.History = ProactiveHistory.RecordUndecided(d.History, proactiveSuggestionCard, ProactiveResolution.Expired, now);
        }
      });
      this.Report(expired, "expired", null);
      ClickyAnalytics.TrackProactiveAgentsUnactioned(expired.Select<ProactiveSuggestionCard, string>((ProactiveSuggestionCard c) => c.Title).ToList<string>(), "expired", expired[0].CheckReason.Wire());
      HashSet<Guid> ids = expired.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id).ToHashSet<Guid>();
      this.cards.RemoveAll((ProactiveSuggestionCard c) => ids.Contains(c.Id));
      this.PersistCards();
      this.ReconcileMorningNotice();
      string text = "Proactive";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>(expired.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" suggestion card(s) expired");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.RaiseChanged();
    }

    // Token: 0x06000C0F RID: 3087 RVA: 0x0055E760 File Offset: 0x0055E760
    public void SyncStatusesFromServer(bool force = false)
    {
      ProactiveCoordinator.<>c__DisplayClass120_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass120_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.statusSyncInFlight)
      {
        return;
      }
      if (!force)
      {
        DateTimeOffset? dateTimeOffset = this.statusSyncLastRunAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (DateTimeOffset.Now - valueOrDefault < ProactiveCoordinator.StatusSyncInterval)
          {
            return;
          }
        }
      }
      CS$<>8__locals1.ids = (from c in this.cards
        select c.ServerSuggestionId into i
        where i != null
        select i).Cast<int>().ToList<int>();
      if (CS$<>8__locals1.ids.Count == 0)
      {
        return;
      }
      this.statusSyncLastRunAt = new DateTimeOffset?(DateTimeOffset.Now);
      this.statusSyncInFlight = true;
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass120_0.<<SyncStatusesFromServer>b__2>d <<SyncStatusesFromServer>b__2>d;
        <<SyncStatusesFromServer>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<SyncStatusesFromServer>b__2>d.<>4__this = CS$<>8__locals1;
        <<SyncStatusesFromServer>b__2>d.<>1__state = -1;
        <<SyncStatusesFromServer>b__2>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass120_0.<<SyncStatusesFromServer>b__2>d>(ref <<SyncStatusesFromServer>b__2>d);
        return <<SyncStatusesFromServer>b__2>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C10 RID: 3088 RVA: 0x0055E84C File Offset: 0x0055E84C
    public static ClickyAnalytics.SuggestionCardInfo AnalyticsCard(ProactiveSuggestionCard card)
    {
      int? num;
      return new ClickyAnalytics.SuggestionCardInfo(((card.ServerSuggestionId != null) ? num.GetValueOrDefault().ToString() : null) ?? card.Id.ToString().ToUpperInvariant(), card.CheckReason.Wire(), card.NewClicky != null, card.Routine != null, card.ClickySlug);
    }

    // Token: 0x06000C11 RID: 3089 RVA: 0x0055E8C8 File Offset: 0x0055E8C8
    public JsonObject AnalyticsContext(string inputMethod, [Nullable(2)] string inputSurface = null)
    {
      string text = inputSurface ?? ((inputMethod == "scheduled") ? "routine" : ((inputMethod == "text") ? "notch_text_box" : ((inputMethod == "voice") ? "global_voice_input" : "hud_button")));
      NotchPresentation presentation = this.host.Services.Presenter.Presentation;
      bool flag;
      if (!(presentation is NotchPresentation.OnboardingPinned) && !(presentation is NotchPresentation.TextInput) && !(presentation is NotchPresentation.TextResponse))
      {
        NotchPresentation.LiveEvent liveEvent = this.host.Services.Presenter.Presentation as NotchPresentation.LiveEvent;
        flag = liveEvent != null && liveEvent.Event is NotchLiveEvent.AgentPeek;
      }
      else
      {
        flag = true;
      }
      bool flag2 = flag;
      return ClickyAnalytics.InteractionContext(inputMethod, text, "closed", false, false, flag2);
    }

    // Token: 0x06000C12 RID: 3090 RVA: 0x0055E998 File Offset: 0x0055E998
    public void TrackSuggestionAction(Guid cardId, string action, JsonObject context, bool hasFeedback = false)
    {
      ProactiveSuggestionCard proactiveSuggestionCard = this.FindCard(cardId);
      if (proactiveSuggestionCard != null)
      {
        ClickyAnalytics.TrackSuggestionInteraction("home_space_suggestion_action", ProactiveCoordinator.AnalyticsCard(proactiveSuggestionCard), action, hasFeedback, context);
      }
    }

    // Token: 0x06000C13 RID: 3091 RVA: 0x0055E9C4 File Offset: 0x0055E9C4
    [NullableContext(2)]
    public ProactiveCoordinator.ApprovalResult Approve(Guid cardId, string adjustmentText = null, bool announce = true, [Nullable(1)] string inputMethod = "button")
    {
      ProactiveSuggestionCard card = this.FindCard(cardId);
      if (card == null || card.IsExpired(this.Now))
      {
        return null;
      }
      JsonObject jsonObject = ClickyAnalytics.SuggestionInteractionProperties(ProactiveCoordinator.AnalyticsCard(card), this.AnalyticsContext(inputMethod, null));
      jsonObject["was_adjusted"] = !string.IsNullOrWhiteSpace(adjustmentText);
      this.NoteSuggestionApprovedForMorningStreak();
      if (this.AdjustingCardId == cardId)
      {
        this.AdjustingCardId = null;
        this.adjustmentAnswerCount = 0;
        this.UpdateLiveStateHook();
      }
      this.RemoveCard(cardId);
      this.ScheduleExpiry();
      DateTimeOffset now = this.Now;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.History = ProactiveHistory.Record(d.History, new ProactiveHistoryEntry(card.Title, card.Description, ProactiveResolution.Approved, now, null, new bool?(true)), now);
      });
      this.Report(new <>z__ReadOnlySingleElementList<ProactiveSuggestionCard>(card), "approved", null);
      Func<ProactiveJobLaunch, bool> launchApprovedCardAsJob = ProactiveHooks.LaunchApprovedCardAsJob;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (launchApprovedCardAsJob != null)
      {
        bool flag = launchApprovedCardAsJob(ProactiveCoordinator.JobLaunchFor(card, string.IsNullOrWhiteSpace(adjustmentText) ? null : adjustmentText.Trim()));
        jsonObject["queued"] = false;
        jsonObject["launch_dispatched"] = flag;
        jsonObject["launched_as_job"] = true;
        ClickyAnalytics.TrackProactiveAgentApproved(card.Title, card.Description, (DateTimeOffset.Now - card.CreatedAt).TotalSeconds, flag, card.CheckReason.Wire(), jsonObject);
        ProactiveQueueStatus proactiveQueueStatus = new ProactiveQueueStatus(flag ? ProactiveQueueStatusKind.Starting : ProactiveQueueStatusKind.Paused, false);
        string text = "Proactive";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 2);
        defaultInterpolatedStringHandler.AppendLiteral("approved \"");
        defaultInterpolatedStringHandler.AppendFormatted(card.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" → a job (");
        defaultInterpolatedStringHandler.AppendFormatted(proactiveQueueStatus.Title);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        if (flag)
        {
          this.real.Core.BrainSession.NoteForNextTurn("[App] \"" + card.Title + "\", the job its card offered, is running now (see [Your jobs]): never start it again.", null);
        }
        if (announce && flag)
        {
          Func<bool> isInterviewSendOffInProgress = FlowHooks.IsInterviewSendOffInProgress;
          if (isInterviewSendOffInProgress == null || !isInterviewSendOffInProgress())
          {
            this.Speak(proactiveQueueStatus.Acknowledgment);
          }
        }
        this.RaiseChanged();
        return new ProactiveCoordinator.ApprovalResult("", "a job on the right edge of the screen", proactiveQueueStatus);
      }
      ValueTuple<string, string, bool, bool> valueTuple = this.ResolveClicky(card);
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      bool item3 = valueTuple.Item3;
      bool item4 = valueTuple.Item4;
      QueuedProactiveTask queuedProactiveTask = new QueuedProactiveTask(card, item, string.IsNullOrWhiteSpace(adjustmentText) ? null : adjustmentText.Trim(), item3, item4);
      ProactiveQueueStatus proactiveQueueStatus2 = this.StatusFor(queuedProactiveTask);
      ProactiveQueueStatus proactiveQueueStatus3;
      bool flag2;
      if (proactiveQueueStatus2.Kind != ProactiveQueueStatusKind.Ready)
      {
        this.queue.Add(queuedProactiveTask);
        this.PersistQueue();
        proactiveQueueStatus3 = proactiveQueueStatus2;
        flag2 = true;
      }
      else if (this.Launch(queuedProactiveTask))
      {
        proactiveQueueStatus3 = new ProactiveQueueStatus(ProactiveQueueStatusKind.Starting, false);
        flag2 = true;
      }
      else
      {
        List<QueuedProactiveTask> list = this.queue;
        QueuedProactiveTask queuedProactiveTask2 = queuedProactiveTask.<Clone>$();
        queuedProactiveTask2.IsPaused = true;
        list.Add(queuedProactiveTask2);
        this.PersistQueue();
        proactiveQueueStatus3 = new ProactiveQueueStatus(ProactiveQueueStatusKind.Paused, false);
        flag2 = false;
      }
      string text2 = "Proactive";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 4);
      defaultInterpolatedStringHandler.AppendLiteral("approved \"");
      defaultInterpolatedStringHandler.AppendFormatted(card.Title);
      defaultInterpolatedStringHandler.AppendLiteral("\" → ");
      defaultInterpolatedStringHandler.AppendFormatted(item2);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(proactiveQueueStatus3.Title);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      defaultInterpolatedStringHandler.AppendFormatted((queuedProactiveTask.AdjustmentText == null) ? "" : " with an adjustment");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      jsonObject["queued"] = proactiveQueueStatus2.Kind > ProactiveQueueStatusKind.Ready;
      jsonObject["launch_dispatched"] = proactiveQueueStatus2.Kind == ProactiveQueueStatusKind.Ready && flag2;
      jsonObject["clicky_id"] = ClickyAnalytics.ProductClickyIdentifier(item);
      ClickyAnalytics.TrackProactiveAgentApproved(card.Title, card.Description, (DateTimeOffset.Now - card.CreatedAt).TotalSeconds, flag2, card.CheckReason.Wire(), jsonObject);
      if (announce)
      {
        this.Speak(proactiveQueueStatus3.Acknowledgment);
      }
      this.RaiseChanged();
      return new ProactiveCoordinator.ApprovalResult(item, item2, proactiveQueueStatus3);
    }

    // Token: 0x06000C14 RID: 3092 RVA: 0x0055EE68 File Offset: 0x0055EE68
    public void Deny(Guid cardId, [Nullable(2)] string reason = null, string inputMethod = "button")
    {
      ProactiveSuggestionCard card = this.FindCard(cardId);
      if (card == null)
      {
        return;
      }
      string text = ((reason != null) ? reason.Trim() : null);
      if (text != null && text.Length > 500)
      {
        text = text.Substring(0, 500);
      }
      string denyReason = (string.IsNullOrEmpty(text) ? null : text);
      JsonObject jsonObject = ClickyAnalytics.SuggestionInteractionProperties(ProactiveCoordinator.AnalyticsCard(card), this.AnalyticsContext(inputMethod, null));
      jsonObject["has_feedback"] = denyReason != null;
      ClickyAnalytics.TrackProactiveAgentDenied(card.Title, card.Description, (DateTimeOffset.Now - card.CreatedAt).TotalSeconds, card.CheckReason.Wire(), denyReason, jsonObject);
      if (this.AdjustingCardId == cardId)
      {
        this.FinishAdjusting();
      }
      this.RemoveCard(cardId);
      this.ScheduleExpiry();
      DateTimeOffset now = this.Now;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.History = ProactiveHistory.Record(d.History, new ProactiveHistoryEntry(card.Title, card.Description, ProactiveResolution.Denied, now, denyReason, new bool?(true)), now);
      });
      this.Report(new <>z__ReadOnlySingleElementList<ProactiveSuggestionCard>(card), "denied", denyReason);
      Log.Info("Proactive", "denied \"" + card.Title + "\"" + ((denyReason == null) ? "" : " with a reason"));
      this.RaiseChanged();
    }

    // Token: 0x06000C15 RID: 3093 RVA: 0x0055F008 File Offset: 0x0055F008
    [return: TupleElementNames(new string[] { "Slug", "Name", "Founded", "Restored" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private ValueTuple<string, string, bool, bool> ResolveClicky(ProactiveSuggestionCard card)
    {
      IReadOnlyList<ClickyInfo> clickys = this.real.Agents.Clickys;
      string requested = card.ClickySlug;
      if (requested != null)
      {
        ClickyInfo clickyInfo = clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.Slug == requested);
        if (clickyInfo != null)
        {
          return new ValueTuple<string, string, bool, bool>(clickyInfo.Slug, clickyInfo.Name, false, false);
        }
      }
      ProactiveNewClicky newClicky = card.NewClicky;
      string text;
      string text2;
      string text3;
      if (newClicky != null)
      {
        string name = newClicky.Name;
        if (name != null && name.Length > 0)
        {
          text = newClicky.Name;
          text2 = ((newClicky.Role.Length == 0) ? "Agent" : newClicky.Role);
          text3 = ((newClicky.Description.Length == 0) ? card.Description : newClicky.Description);
          goto IL_00E0;
        }
      }
      ValueTuple<string, string> valueTuple = FallbackClickyIdentity.For(card.Title);
      text = valueTuple.Item1;
      text2 = valueTuple.Item2;
      text3 = card.Description;
      IL_00E0:
      string normalized = FallbackClickyIdentity.NormalizedName(text);
      ClickyInfo clickyInfo2 = clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => FallbackClickyIdentity.NormalizedName(c.Name) == normalized);
      if (clickyInfo2 != null)
      {
        return new ValueTuple<string, string, bool, bool>(clickyInfo2.Slug, clickyInfo2.Name, false, false);
      }
      ClickyInfo clickyInfo3 = this.real.Agents.CreateClicky(text, text2, text3);
      bool flag = DateTimeOffset.Now - clickyInfo3.CreatedAt > TimeSpan.FromMinutes(1L);
      Log.Info("Proactive", flag ? ("revived archived Clicky " + clickyInfo3.Name + " for a suggestion") : ("founded Clicky " + clickyInfo3.Name + " for a suggestion"));
      if (flag)
      {
        ClickyAnalytics.TrackProductEvent("clicky_restored", clickyInfo3.Slug, null, null);
      }
      else
      {
        ClickyRoster.Shared.TrackCreated(clickyInfo3.Slug, "agent", 0);
      }
      return new ValueTuple<string, string, bool, bool>(clickyInfo3.Slug, clickyInfo3.Name, !flag, flag);
    }

    // Token: 0x06000C16 RID: 3094 RVA: 0x0055F1EC File Offset: 0x0055F1EC
    private ProactiveQueueStatus StatusFor(QueuedProactiveTask entry)
    {
      return ProactiveQueueStatus.Resolve(entry, this.queue, this.real.Agents.IsClickyBusy(entry.ClickySlug));
    }

    // Token: 0x06000C17 RID: 3095 RVA: 0x0055F210 File Offset: 0x0055F210
    public ProactiveQueueStatus QueueStatus(QueuedProactiveTask entry)
    {
      return this.StatusFor(entry);
    }

    // Token: 0x06000C18 RID: 3096 RVA: 0x0055F21C File Offset: 0x0055F21C
    private static ProactiveJobLaunch JobLaunchFor(ProactiveSuggestionCard card, [Nullable(2)] string adjustmentText)
    {
      string text = ((adjustmentText == null) ? card.AgentPrompt : (card.AgentPrompt + "\n\nThe user adjusted this task before starting it. In their words: \"" + adjustmentText + "\"\nFollow the adjustment wherever it differs from the brief above; keep everything it doesn't touch."));
      string text2 = SuggestionApprovalMarker.Compose(card.Title, card.Description, text);
      string text3 = "The user approved a proactively suggested background task on its card, so HeyClicky is launching this Codex agent with the prebuilt brief. The brief was written by HeyClicky from a signal it picked up about the user's world, not by the user, so it may rest on something HeyClicky never actually had; the user's Yes approves the work, it does not fill in those blanks — if the brief hinges on one, ask for it in the chat instead of guessing. Their Yes on the card is also their approval for exactly what its title and pitch say it will do, a message or post it names included: do that without asking again. Anything the card didn't name, and any payment, refund, deletion or archiving, still waits for their yes on the exact thing.";
      if (card.CheckReason == ProactiveCheckReason.Onboarding)
      {
        text3 += " This ask was written during onboarding to show what you can do, so it may reach for an app (Gmail, Notion, Google Calendar, Sheets, Linear, GitHub) the user has not connected. Assume NOTHING is connected unless your own tool list says otherwise. Never stall on that: do the strongest app-free version now with web research and files you make (the people in a CSV, the emails drafted right here in the chat, the plan as a doc), and then, plainly and eagerly, tell the user which one app to connect to finish the job and exactly what you would do the moment they do (\"Connect Gmail and I'll send these as drafts to your inbox\"). One sentence, at the end, no nagging. This is one of their very first jobs, picked during onboarding so it needs nothing from them: finish it start to finish without asking anything (this overrides any ask-first guidance above). Where something is open, make the sensible choice from the brief and the web and say in one line what you assumed; never stop to ask and never end on a question.";
      }
      ProactiveRoutine routine = card.Routine;
      if (routine != null)
      {
        text3 = text3 + " The card repeats (" + routine.CadenceLabel.ToLowerInvariant() + "): this is its first run, so do the whole task now; HeyClicky runs this job again on that schedule. Don't emit a <ROUTINE> block for it.";
      }
      return new ProactiveJobLaunch(card, text2, text3);
    }

    // Token: 0x06000C19 RID: 3097 RVA: 0x0055F2B0 File Offset: 0x0055F2B0
    private bool Launch(QueuedProactiveTask approval)
    {
      ClickyInfo clickyInfo = this.real.Agents.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.Slug == approval.ClickySlug);
      if (clickyInfo == null || this.real.Agents.IsClickyBusy(approval.ClickySlug))
      {
        return false;
      }
      ProactiveSuggestionCard card = approval.Card;
      string adjustmentText = approval.AdjustmentText;
      string text = ((adjustmentText != null) ? (card.AgentPrompt + "\n\nThe user adjusted this task before starting it. In their words: \"" + adjustmentText + "\"\nFollow the adjustment wherever it differs from the brief above; keep everything it doesn't touch.") : card.AgentPrompt);
      string text2 = "The user approved a proactively suggested background task on its card, so HeyClicky is launching this Codex agent with the prebuilt brief. The brief was written by HeyClicky from a signal it picked up about the user's world, not by the user, so it may rest on something HeyClicky never actually had (their channel or handle, which account, the specific project); the user's Yes approves the work, it does not fill in those blanks — if the brief hinges on one, ask for it in the chat instead of guessing.";
      if (approval.WasFounded)
      {
        text2 += " You were founded just now for exactly this. This is your first conversation. The chat ALREADY shows, in your voice, a hello from you and the pitch for this task, so do NOT introduce yourself again: open with one or two plain sentences about the work itself, specific to this brief, then get straight to it. Fill in your AGENTS.md persona from the brief as part of that work, quietly.";
      }
      ProactiveRoutine routine = card.Routine;
      if (routine != null)
      {
        text2 = text2 + " This task is a ROUTINE the user just approved: HeyClicky will re-run it " + routine.CadenceSpokenLabel + " on this PC, and this turn is run #1. Do the task fully now; later runs will be told to report only what is new. Do not set up any repeat yourself (no <ROUTINE> block) — HeyClicky has already registered it.";
      }
      if (card.CheckReason == ProactiveCheckReason.Onboarding)
      {
        text2 += " This ask was written during onboarding to show what you can do, so it may reach for an app (Gmail, Notion, Google Calendar, Sheets, Linear, GitHub) the user has not connected. Assume NOTHING is connected unless your own tool list says otherwise. Never stall on that: do the strongest app-free version now with web research and files you make (the people in a CSV, the emails drafted right here in the chat, the plan as a doc), and then, plainly and eagerly, tell the user which one app to connect to finish the job and exactly what you would do the moment they do (\"Connect Gmail and I'll send these as drafts to your inbox\"). One sentence, at the end, no nagging. This is one of their very first jobs, picked during onboarding so it needs nothing from them: finish it start to finish without asking anything (this overrides any ask-first guidance above). Where something is open, make the sensible choice from the brief and the web and say in one line what you assumed; never stop to ask and never end on a question.";
      }
      string text3 = SuggestionApprovalMarker.Compose(card.Title, card.Description, text);
      if (approval.WasFounded)
      {
        text3 = ClickyFoundingMarker.Compose(clickyInfo.Name, "", text3);
      }
      int num = this.queue.FindIndex((QueuedProactiveTask e) => e.Id == approval.Id);
      if (num >= 0)
      {
        List<QueuedProactiveTask> list = this.queue;
        int num2 = num;
        QueuedProactiveTask queuedProactiveTask = this.queue[num].<Clone>$();
        queuedProactiveTask.IsLaunching = true;
        list[num2] = queuedProactiveTask;
        this.PersistQueue();
      }
      string result;
      try
      {
        AgentService agents = this.real.Agents;
        AgentLaunchRequest agentLaunchRequest = new AgentLaunchRequest();
        agentLaunchRequest.Prompt = text3;
        agentLaunchRequest.ClickySlug = clickyInfo.Slug;
        agentLaunchRequest.LaunchContext = text2;
        agentLaunchRequest.LaunchSource = "suggestion";
        agentLaunchRequest.UsageContext = "companion";
        agentLaunchRequest.IsGeneratedTaskBrief = true;
        int? serverSuggestionId = card.ServerSuggestionId;
        agentLaunchRequest.ProactiveSuggestionId = ((serverSuggestionId != null) ? new long?((long)serverSuggestionId.GetValueOrDefault()) : null);
        result = agents.LaunchAsync(agentLaunchRequest, default(CancellationToken)).GetAwaiter().GetResult();
      }
      catch (Exception ex)
      {
        Log.Warn("Proactive", "approved suggestion did not dispatch: " + ex.Message);
        if (num >= 0)
        {
          int num3 = this.queue.FindIndex((QueuedProactiveTask e) => e.Id == approval.Id);
          if (num3 >= 0)
          {
            List<QueuedProactiveTask> list2 = this.queue;
            int num4 = num3;
            QueuedProactiveTask queuedProactiveTask2 = this.queue[num3].<Clone>$();
            queuedProactiveTask2.IsLaunching = false;
            list2[num4] = queuedProactiveTask2;
            this.PersistQueue();
          }
        }
        return false;
      }
      this.launchedTasks[result] = approval.Id;
      ClickyRoster.Shared.BindTask(result, clickyInfo.Slug);
      this.queue.RemoveAll((QueuedProactiveTask e) => e.Id == approval.Id);
      this.PersistQueue();
      ProactiveRoutine routine2 = card.Routine;
      if (routine2 != null)
      {
        this.real.Agents.Routines.RegisterApprovedSuggestion(clickyInfo.Slug, string.IsNullOrWhiteSpace(routine2.Title) ? card.Title : routine2.Title, card.AgentPrompt, routine2.ClampedIntervalMinutes, result, null);
      }
      string text4 = "Proactive";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
      defaultInterpolatedStringHandler.AppendLiteral("approved suggestion \"");
      defaultInterpolatedStringHandler.AppendFormatted(card.Title);
      defaultInterpolatedStringHandler.AppendLiteral("\" launched on ");
      defaultInterpolatedStringHandler.AppendFormatted(clickyInfo.Name);
      defaultInterpolatedStringHandler.AppendLiteral(" as ");
      defaultInterpolatedStringHandler.AppendFormatted(result);
      Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
      return true;
    }

    // Token: 0x06000C1A RID: 3098 RVA: 0x0055F64C File Offset: 0x0055F64C
    public void CancelQueued(Guid id)
    {
      this.queue.RemoveAll((QueuedProactiveTask e) => e.Id == id);
      this.PersistQueue();
      this.DrainQueue();
      this.RaiseChanged();
    }

    // Token: 0x06000C1B RID: 3099 RVA: 0x0055F690 File Offset: 0x0055F690
    public void ResumeQueued(Guid id)
    {
      QueuedProactiveTask queuedProactiveTask = this.queue.FirstOrDefault<QueuedProactiveTask>((QueuedProactiveTask e) => e.Id == id);
      if (queuedProactiveTask == null)
      {
        return;
      }
      for (int i = 0; i < this.queue.Count; i++)
      {
        if (this.queue[i].ClickySlug == queuedProactiveTask.ClickySlug)
        {
          List<QueuedProactiveTask> list = this.queue;
          int num = i;
          QueuedProactiveTask queuedProactiveTask2 = this.queue[i].<Clone>$();
          queuedProactiveTask2.IsPaused = false;
          list[num] = queuedProactiveTask2;
        }
      }
      this.PersistQueue();
      this.DrainQueue();
      this.RaiseChanged();
    }

    // Token: 0x06000C1C RID: 3100 RVA: 0x0055F730 File Offset: 0x0055F730
    private void DrainQueue()
    {
      if (this.accountKey == null || this.queue.Count == 0)
      {
        return;
      }
      bool flag = false;
      using (IEnumerator<QueuedProactiveTask> enumerator = QueuedProactiveTask.NextCandidates(this.queue).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          QueuedProactiveTask entry = enumerator.Current;
          if (this.StatusFor(entry).Kind == ProactiveQueueStatusKind.Ready)
          {
            flag = true;
            if (!this.Launch(entry))
            {
              int num = this.queue.FindIndex((QueuedProactiveTask e) => e.Id == entry.Id);
              if (num >= 0)
              {
                List<QueuedProactiveTask> list = this.queue;
                int num2 = num;
                QueuedProactiveTask queuedProactiveTask = this.queue[num].<Clone>$();
                queuedProactiveTask.IsPaused = true;
                queuedProactiveTask.IsLaunching = false;
                list[num2] = queuedProactiveTask;
                this.PersistQueue();
              }
            }
          }
        }
      }
      if (flag)
      {
        this.RaiseChanged();
      }
    }

    // Token: 0x06000C1D RID: 3101 RVA: 0x0055F824 File Offset: 0x0055F824
    private void OnAgentTaskEvent(AgentTaskEvent e)
    {
      AgentTaskEventKind kind = e.Kind;
      if (kind - AgentTaskEventKind.Finished > 2)
      {
        return;
      }
      if (e.Kind != AgentTaskEventKind.Finished && this.launchedTasks.ContainsKey(e.TaskId))
      {
        AgentTaskDetail taskDetail = this.real.Agents.GetTaskDetail(e.TaskId);
        string text = ((taskDetail != null) ? taskDetail.ClickySlug : null);
        for (int i = 0; i < this.queue.Count; i++)
        {
          if (this.queue[i].ClickySlug == text)
          {
            List<QueuedProactiveTask> list = this.queue;
            int num = i;
            QueuedProactiveTask queuedProactiveTask = this.queue[i].<Clone>$();
            queuedProactiveTask.IsPaused = true;
            queuedProactiveTask.IsLaunching = false;
            list[num] = queuedProactiveTask;
          }
        }
        this.PersistQueue();
      }
      this.launchedTasks.Remove(e.TaskId);
      DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromMilliseconds(400L, 0L);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.DrainQueue();
      };
      dispatcherQueueTimer.Start();
      this.RaiseChanged();
    }

    // Token: 0x06000C1E RID: 3102 RVA: 0x0055F948 File Offset: 0x0055F948
    public void BeginAdjusting(Guid cardId)
    {
      ProactiveSuggestionCard proactiveSuggestionCard = this.FindCard(cardId);
      if (proactiveSuggestionCard == null || proactiveSuggestionCard.IsExpired(this.Now) || !this.host.AllowsChatSubmission())
      {
        return;
      }
      ClickyAnalytics.TrackSuggestionInteraction("home_space_suggestion_action", ProactiveCoordinator.AnalyticsCard(proactiveSuggestionCard), "adjust", false, this.AnalyticsContext("button", null));
      this.FinishAdjusting();
      this.AdjustingCardId = new Guid?(cardId);
      this.adjustmentAnswerCount = 0;
      this.UpdateLiveStateHook();
      this.RaiseChanged();
      this.real.Companion.AskWithAppFactAsync("[App] The user clicked Adjust on the suggestion currently in view. Ask one short, natural question about what they would like to change, then wait for their answer. Do not read the proposal back or start the task.", "suggestion_adjust");
    }

    // Token: 0x06000C1F RID: 3103 RVA: 0x0055F9E0 File Offset: 0x0055F9E0
    public bool FinishAdjusting()
    {
      if (this.AdjustingCardId == null)
      {
        return false;
      }
      this.AdjustingCardId = null;
      this.adjustmentAnswerCount = 0;
      this.UpdateLiveStateHook();
      if (this.real.Companion.State != VoiceState.Idle)
      {
        this.real.Companion.Cancel();
      }
      this.RaiseChanged();
      return true;
    }

    // Token: 0x06000C20 RID: 3104 RVA: 0x0055FA44 File Offset: 0x0055FA44
    public bool CancelAdjustingFromEscape()
    {
      Guid? adjustingCardId = this.AdjustingCardId;
      if (adjustingCardId != null)
      {
        Guid valueOrDefault = adjustingCardId.GetValueOrDefault();
        this.TrackSuggestionAction(valueOrDefault, "cancel_adjustment", this.AnalyticsContext("keyboard", "escape_key"), false);
        return this.FinishAdjusting();
      }
      return false;
    }

    // Token: 0x06000C21 RID: 3105 RVA: 0x0055FA90 File Offset: 0x0055FA90
    [return: Nullable(2)]
    public ProactiveCoordinator.ApprovalResult ApproveWithTypedAdjustment(Guid cardId, string text)
    {
      return this.Approve(cardId, text, true, "text");
    }

    // Token: 0x06000C22 RID: 3106 RVA: 0x0055FAA0 File Offset: 0x0055FAA0
    [NullableContext(2)]
    private string AdjustmentNote()
    {
      Guid? adjustingCardId = this.AdjustingCardId;
      if (adjustingCardId != null)
      {
        Guid valueOrDefault = adjustingCardId.GetValueOrDefault();
        return SuggestionVoiceAction.ContextNote(this.FindCard(valueOrDefault), this.Now) + "\n" + SuggestionVoiceAction.AdjustmentBudget(this.adjustmentAnswerCount);
      }
      if (this.IsSuggestionsPageVisible)
      {
        adjustingCardId = this.CurrentCardId;
        if (adjustingCardId != null)
        {
          Guid valueOrDefault2 = adjustingCardId.GetValueOrDefault();
          return SuggestionVoiceAction.ContextNote(this.FindCard(valueOrDefault2), this.Now);
        }
      }
      return null;
    }

    // Token: 0x06000C23 RID: 3107 RVA: 0x0055FB28 File Offset: 0x0055FB28
    public void NoteAdjustmentTurnCompleted()
    {
      Guid? adjustingCardId = this.AdjustingCardId;
      if (adjustingCardId != null)
      {
        Guid valueOrDefault = adjustingCardId.GetValueOrDefault();
        if (this.adjustmentAnswerCount > 0)
        {
          this.TrackSuggestionAction(valueOrDefault, "adjustment_answer", this.AnalyticsContext("voice", null), true);
        }
        this.adjustmentAnswerCount++;
        this.UpdateLiveStateHook();
        return;
      }
    }

    // Token: 0x06000C24 RID: 3108 RVA: 0x0055FB88 File Offset: 0x0055FB88
    private void UpdateLiveStateHook()
    {
      this.liveNoteSnapshot = ((this.AdjustingCardId != null || this.IsSuggestionsPageVisible) ? this.AdjustmentNote() : null);
      ProactiveHooks.LiveStateNote = ((this.liveNoteSnapshot == null) ? null : new Func<string>(() => this.liveNoteSnapshot));
    }

    // Token: 0x06000C25 RID: 3109 RVA: 0x0055FBE0 File Offset: 0x0055FBE0
    public string HandleVoiceAction(SuggestionVoiceAction.Request request)
    {
      SuggestionVoiceAction.Kind kind = request.Kind;
      if (kind != SuggestionVoiceAction.Kind.Approve)
      {
        if (kind != SuggestionVoiceAction.Kind.Skip)
        {
          return "{\"status\":\"error\",\"message\":\"That suggestion reference is not valid.\"}";
        }
        if (this.FindCard(request.CardId) == null)
        {
          return "{\"status\":\"error\",\"message\":\"That suggestion is no longer available.\"}";
        }
        this.Deny(request.CardId, request.Detail, "voice");
        ProactiveSuggestionCard proactiveSuggestionCard = this.VisibleCards.FirstOrDefault<ProactiveSuggestionCard>();
        if (proactiveSuggestionCard != null)
        {
          return "{\"status\":\"skipped\",\"next\":" + JsonSerializer.Serialize<string>(proactiveSuggestionCard.Title, null) + "}";
        }
        return "{\"status\":\"skipped\",\"next\":null,\"message\":\"Skipped. There are no suggestions left.\"}";
      }
      else
      {
        ProactiveCoordinator.ApprovalResult approvalResult = this.Approve(request.CardId, request.Detail, false, "voice");
        if (approvalResult != null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
          defaultInterpolatedStringHandler.AppendLiteral("{\"status\":\"approved\",\"clicky_name\":");
          defaultInterpolatedStringHandler.AppendFormatted(JsonSerializer.Serialize<string>(approvalResult.ClickyName, null));
          defaultInterpolatedStringHandler.AppendLiteral(",\"message\":");
          defaultInterpolatedStringHandler.AppendFormatted(JsonSerializer.Serialize<string>("The suggested task was approved. Say exactly: \"" + approvalResult.Status.Acknowledgment + "\" Nothing else. Do not recap the task, introduce the Clicky, explain the routine, promise an update, or ask a follow-up question.", null));
          defaultInterpolatedStringHandler.AppendLiteral("}");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return "{\"status\":\"error\",\"message\":\"That suggestion is no longer available. Say so in one short line.\"}";
      }
    }

    // Token: 0x06000C26 RID: 3110 RVA: 0x0055FD04 File Offset: 0x0055FD04
    private void Speak(string line)
    {
      ProactiveCoordinator.<>c__DisplayClass147_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass147_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.line = line;
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass147_0.<<Speak>b__0>d <<Speak>b__0>d;
        <<Speak>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<Speak>b__0>d.<>4__this = CS$<>8__locals1;
        <<Speak>b__0>d.<>1__state = -1;
        <<Speak>b__0>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass147_0.<<Speak>b__0>d>(ref <<Speak>b__0>d);
        return <<Speak>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C27 RID: 3111 RVA: 0x0055FD2C File Offset: 0x0055FD2C
    public void DebugFetchNow()
    {
      this.StartJob(ProactiveCheckReason.Morning, false);
    }

    // Token: 0x06000C28 RID: 3112 RVA: 0x0055FD38 File Offset: 0x0055FD38
    public void OnDebugChord(ShortcutAction action)
    {
    }

    // Token: 0x06000C29 RID: 3113 RVA: 0x0055FD3C File Offset: 0x0055FD3C
    public void DebugReset()
    {
      this.CancelJob();
      this.DismissMorningNotice();
      this.cards = new List<ProactiveSuggestionCard>();
      string text = this.accountKey;
      if (text != null)
      {
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.PendingJob = null;
          a.Cards = new List<ProactiveSuggestionCard>();
          a.Slots = new ProactiveSlotDayState(null, null, null);
          a.SuggestionsLastFetchedAt = null;
          a.SuggestionsRowLastSeenAt = null;
          a.IgnoredSuggestionsBreakUntil = null;
        });
      }
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.History = new List<ProactiveHistoryEntry>();
        d.MorningSettledAt = null;
        d.MorningDismissals = MorningDismissalHistory.Empty;
      });
      this.LastFetchedAt = null;
      Log.Info("Proactive", "debug reset: suggestion state cleared");
      this.RaiseChanged();
    }

    // Token: 0x06000C2A RID: 3114 RVA: 0x0055FDE4 File Offset: 0x0055FDE4
    public void ForgetForAccountDeletion()
    {
      this.CancelJob();
      this.DismissMorningNotice();
      this.cards = new List<ProactiveSuggestionCard>();
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.Accounts.Clear();
        d.History = new List<ProactiveHistoryEntry>();
      });
      this.LastFetchedAt = null;
      this.RaiseChanged();
    }

    // Token: 0x1700022B RID: 555
    // (get) Token: 0x06000C2B RID: 3115 RVA: 0x0055FE48 File Offset: 0x0055FE48
    public bool HasUnseenCards
    {
      get
      {
        DateTimeOffset dateTimeOffset = this.VisibleCards.Select<ProactiveSuggestionCard, DateTimeOffset>((ProactiveSuggestionCard c) => c.CreatedAt).DefaultIfEmpty<DateTimeOffset>().Max<DateTimeOffset>();
        if (dateTimeOffset == default(DateTimeOffset))
        {
          return false;
        }
        string text = this.accountKey;
        DateTimeOffset? dateTimeOffset2;
        if (text == null)
        {
          dateTimeOffset2 = null;
        }
        else
        {
          dateTimeOffset2 = this.store.ReadAccount<DateTimeOffset?>(text, (ProactiveAccountState a) => a.SuggestionsRowLastSeenAt);
        }
        DateTimeOffset? dateTimeOffset3 = dateTimeOffset2;
        return dateTimeOffset3 == null || dateTimeOffset > dateTimeOffset3;
      }
    }

    // Token: 0x06000C2C RID: 3116 RVA: 0x0055FF08 File Offset: 0x0055FF08
    public void MarkRowSeen()
    {
      string text = this.accountKey;
      if (text != null)
      {
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.SuggestionsRowLastSeenAt = new DateTimeOffset?(this.Now);
        });
      }
    }

    // Token: 0x06000C2D RID: 3117 RVA: 0x0055FF38 File Offset: 0x0055FF38
    public void MarkRowUnread()
    {
      string text = this.accountKey;
      if (text != null)
      {
        this.store.UpdateAccount(text, delegate(ProactiveAccountState a)
        {
          a.SuggestionsRowLastSeenAt = new DateTimeOffset?(DateTimeOffset.MinValue);
        });
      }
      this.RaiseChanged();
    }

    // Token: 0x06000C2E RID: 3118 RVA: 0x0055FF80 File Offset: 0x0055FF80
    public void OnSuggestionsPageOpened()
    {
      this.IsSuggestionsPageVisible = true;
      this.UpdateLiveStateHook();
      this.SettleMorningNoticeForToday();
      this.SyncStatusesFromServer(true);
      this.ExpireOutdatedCards();
      this.StartJob(ProactiveCheckReason.HomeSpace, true);
      this.MarkRowSeen();
      if (this.catalog.Count == 0)
      {
        this.RefreshIntegrationsAsync();
      }
    }

    // Token: 0x06000C2F RID: 3119 RVA: 0x0055FFD0 File Offset: 0x0055FFD0
    public void OnSuggestionsPageClosed()
    {
      this.IsSuggestionsPageVisible = false;
      this.FinishAdjusting();
      this.UpdateLiveStateHook();
    }

    // Token: 0x06000C30 RID: 3120 RVA: 0x0055FFE8 File Offset: 0x0055FFE8
    public void RefreshFromSuggestions()
    {
      this.StartJob(ProactiveCheckReason.HomeSpace, false);
    }

    // Token: 0x06000C31 RID: 3121 RVA: 0x0055FFF4 File Offset: 0x0055FFF4
    public string HeaderStatusLine()
    {
      int count = this.VisibleCards.Count;
      if (this.IsFetching)
      {
        if (count != 0)
        {
          return "Looking for fresh ideas. This can take 5–10 minutes.";
        }
        return "Looking through your connected apps.";
      }
      else
      {
        string fetchError = this.FetchError;
        if (fetchError != null)
        {
          return fetchError;
        }
        string text;
        if (count != 0)
        {
          if (count != 1)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
            defaultInterpolatedStringHandler.AppendFormatted<int>(count);
            defaultInterpolatedStringHandler.AppendLiteral(" ideas, ready when you are.");
            text = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text = "One idea, ready when you are.";
          }
        }
        else
        {
          text = "No new suggestions right now.";
        }
        string text2 = text;
        DateTimeOffset? lastFetchedAt = this.LastFetchedAt;
        if (lastFetchedAt != null)
        {
          return text2 + " Last checked " + lastFetchedAt.GetValueOrDefault().LocalDateTime.ToString("h:mm tt", CultureInfo.GetCultureInfo("en-US")) + ".";
        }
        return text2;
      }
    }

    // Token: 0x06000C32 RID: 3122 RVA: 0x005600C4 File Offset: 0x005600C4
    [NullableContext(2)]
    public string ClickyName(string slug)
    {
      if (slug == null)
      {
        return null;
      }
      ClickyInfo clickyInfo = this.real.Agents.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.Slug == slug);
      if (clickyInfo == null)
      {
        return null;
      }
      return clickyInfo.Name;
    }

    // Token: 0x1700022C RID: 556
    // (get) Token: 0x06000C33 RID: 3123 RVA: 0x00560114 File Offset: 0x00560114
    public IReadOnlyList<AgentIntegrationInfo> Integrations
    {
      get
      {
        return this.catalog;
      }
    }

    // Token: 0x06000C34 RID: 3124 RVA: 0x0056011C File Offset: 0x0056011C
    [return: Nullable(2)]
    public ProactiveSuggestionCard AppendTourCard(ProactiveSuggestionCard card)
    {
      ProactiveSuggestionCard proactiveSuggestionCard = this.VisibleCards.FirstOrDefault<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.Id == card.Id) ?? this.VisibleCards.FirstOrDefault<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => string.Equals(c.Title, card.Title, StringComparison.OrdinalIgnoreCase));
      if (proactiveSuggestionCard != null)
      {
        return proactiveSuggestionCard;
      }
      ProactiveSuggestionCard proactiveSuggestionCard2 = card.<Clone>$();
      proactiveSuggestionCard2.CreatedAt = this.Now;
      ProactiveSuggestionCard proactiveSuggestionCard3 = proactiveSuggestionCard2;
      this.cards.Insert(0, proactiveSuggestionCard3);
      this.PersistCards();
      this.Report(new <>z__ReadOnlySingleElementList<ProactiveSuggestionCard>(proactiveSuggestionCard3), "presented", null);
      ClickyAnalytics.TrackProactiveAgentsRecommended(new <>z__ReadOnlySingleElementList<string>(proactiveSuggestionCard3.Title), proactiveSuggestionCard3.CheckReason.Wire());
      Log.Info("Proactive", "tour card added to Suggestions: " + proactiveSuggestionCard3.Title);
      this.RaiseChanged();
      return proactiveSuggestionCard3;
    }

    // Token: 0x06000C35 RID: 3125 RVA: 0x005601E8 File Offset: 0x005601E8
    public void RestoreCards(IReadOnlyList<ProactiveSuggestionCard> restored)
    {
      HashSet<string> live = this.VisibleCards.Select<ProactiveSuggestionCard, string>((ProactiveSuggestionCard c) => c.Title.ToLowerInvariant()).ToHashSet<string>();
      List<ProactiveSuggestionCard> list = restored.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => !live.Contains(c.Title.ToLowerInvariant())).ToList<ProactiveSuggestionCard>();
      if (list.Count == 0)
      {
        return;
      }
      this.cards.AddRange(list);
      this.PersistCards();
      this.ScheduleExpiry();
      this.RaiseChanged();
    }

    // Token: 0x06000C36 RID: 3126 RVA: 0x00560270 File Offset: 0x00560270
    public IReadOnlyList<ProactiveSuggestionCard> LiveCards(params ProactiveCheckReason[] reasons)
    {
      return this.cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => !c.IsExpired(this.Now) && reasons.Contains(c.CheckReason)).ToList<ProactiveSuggestionCard>();
    }

    // Token: 0x1700022D RID: 557
    // (get) Token: 0x06000C37 RID: 3127 RVA: 0x005602B0 File Offset: 0x005602B0
    public bool IsSweepRunning
    {
      get
      {
        return this.jobCts != null;
      }
    }

    // Token: 0x1700022E RID: 558
    // (get) Token: 0x06000C38 RID: 3128 RVA: 0x005602C0 File Offset: 0x005602C0
    [Nullable(2)]
    public string AccountKey
    {
      [NullableContext(2)]
      get
      {
        return this.accountKey;
      }
    }

    // Token: 0x1700022F RID: 559
    // (get) Token: 0x06000C39 RID: 3129 RVA: 0x005602C8 File Offset: 0x005602C8
    [Nullable(2)]
    public string UserId
    {
      [NullableContext(2)]
      get
      {
        return this.real.Core.Auth.UserId;
      }
    }

    // Token: 0x06000C3A RID: 3130 RVA: 0x005602E0 File Offset: 0x005602E0
    private Task<IReadOnlyList<AgentIntegrationInfo>> RefreshIntegrationsAsync()
    {
      Task<IReadOnlyList<AgentIntegrationInfo>> task = this.catalogLoad;
      if (task != null && !task.IsCompleted)
      {
        return this.catalogLoad;
      }
      return this.catalogLoad = this.<RefreshIntegrationsAsync>g__LoadAsync|185_0();
    }

    // Token: 0x06000C3B RID: 3131 RVA: 0x00560318 File Offset: 0x00560318
    private IReadOnlyList<string> UseCasesFor(string toolkit)
    {
      AgentIntegrationInfo agentIntegrationInfo = this.catalog.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.ToolkitSlugs.Contains(toolkit, StringComparer.OrdinalIgnoreCase) || i.Id.Equals(toolkit, StringComparison.OrdinalIgnoreCase));
      if (agentIntegrationInfo != null)
      {
        IReadOnlyList<string> useCases = agentIntegrationInfo.UseCases;
        if (useCases != null && useCases.Count > 0)
        {
          return agentIntegrationInfo.UseCases;
        }
      }
      return new <>z__ReadOnlyArray<string>(new string[] { "Answer questions with your data", "Automate busywork", "Draft and summarize", "Keep things in sync" });
    }

    // Token: 0x06000C3C RID: 3132 RVA: 0x00560394 File Offset: 0x00560394
    private void UpdateIntegrationMonitor()
    {
      if (this.accountKey == null)
      {
        this.StopIntegrationMonitor();
        return;
      }
      if (this.integrationTimer == null)
      {
        this.integrationTimer = this.dq.CreateTimer();
        this.integrationTimer.Interval = IntegrationSuggestionRules.PollInterval;
        this.integrationTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.SampleForegroundActivity();
        };
      }
      if (!this.integrationTimer.IsRunning)
      {
        this.integrationTimer.Start();
        Log.Info("Integrations", "suggestion monitor started");
      }
    }

    // Token: 0x06000C3D RID: 3133 RVA: 0x00560418 File Offset: 0x00560418
    private void StopIntegrationMonitor()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.integrationTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.integrationRules.ResetAll();
      if (this.activeIntegrationSuggestion != null)
      {
        this.activeIntegrationSuggestion = null;
        this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.IntegrationSuggestion));
      }
      if (this.spotifyTipActive)
      {
        this.spotifyTipActive = false;
        this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.SpotifyVoiceTip));
      }
    }

    // Token: 0x06000C3E RID: 3134 RVA: 0x005604A0 File Offset: 0x005604A0
    [NullableContext(2)]
    private string IntegrationSuppressionReason()
    {
      bool isSignedIn = this.IsSignedIn;
      bool canAdvancePastPermissions = this.CanAdvancePastPermissions;
      bool hasCompletedOnboarding = this.HasCompletedOnboarding;
      string text = ProactiveCoordinator.AmbientQuietReason();
      NotchPresentation presentation = this.host.Services.Presenter.Presentation;
      bool flag;
      if (!(presentation is NotchPresentation.TextInput) && !(presentation is NotchPresentation.TextResponse) && !(presentation is NotchPresentation.OnboardingPinned))
      {
        NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
        if (liveEvent == null || !(liveEvent.Event is NotchLiveEvent.AgentPeek))
        {
          flag = false;
          goto IL_006A;
        }
      }
      flag = true;
      IL_006A:
      IntegrationSuggestionRules.SuppressionInputs suppressionInputs = new IntegrationSuggestionRules.SuppressionInputs(isSignedIn, canAdvancePastPermissions, hasCompletedOnboarding, text, flag, this.spotifyTipActive, this.activeIntegrationSuggestion != null, this.real.Companion.State > VoiceState.Idle, this.host.IsPreSignInIntroShowing || this.host.IsIntroSequenceActive, this.IsTutorialEngaged, this.IsWithinPostOnboardingHeadsUpQuietPeriod);
      return IntegrationSuggestionRules.SuppressionReason(in suppressionInputs);
    }

    // Token: 0x06000C3F RID: 3135 RVA: 0x00560578 File Offset: 0x00560578
    [NullableContext(2)]
    private static string AmbientQuietReason()
    {
      if (!QuietContext.IgnoredForTests)
      {
        return IntegrationSuggestionRules.AmbientQuietReason(QuietContext.IsAnotherAppUsingMicrophone(), false);
      }
      return null;
    }

    // Token: 0x17000230 RID: 560
    // (get) Token: 0x06000C40 RID: 3136 RVA: 0x00560590 File Offset: 0x00560590
    private bool IsWithinPostOnboardingHeadsUpQuietPeriod
    {
      get
      {
        DateTimeOffset? dateTimeOffset = this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.OnboardingFirstCompletedAt);
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (this.Now - valueOrDefault < ProactiveCoordinator.PostOnboardingHeadsUpQuiet)
          {
            return true;
          }
        }
        double? num = this.real.Core.Settings.Read<double?>((AppSettings s) => s.Onboarding.TutorialCompletedAtEpochSeconds);
        if (num != null)
        {
          double valueOrDefault2 = num.GetValueOrDefault();
          return (double)DateTimeOffset.Now.ToUnixTimeSeconds() - valueOrDefault2 < ProactiveCoordinator.PostOnboardingHeadsUpQuiet.TotalSeconds;
        }
        return false;
      }
    }

    // Token: 0x06000C41 RID: 3137 RVA: 0x00560658 File Offset: 0x00560658
    private void SampleForegroundActivity()
    {
      if (this.integrationSampleInFlight)
      {
        return;
      }
      if (DateTimeOffset.Now - this.catalogFetchedAt > ProactiveCoordinator.CatalogRefreshInterval)
      {
        this.RefreshIntegrationsAsync();
      }
      string text = this.IntegrationSuppressionReason();
      if (text != this.lastSuppression)
      {
        this.lastSuppression = text;
        if (text != null)
        {
          Log.Info("Integrations", "sampling suppressed: " + text + ((text == "quietContext") ? (" (" + ProactiveCoordinator.AmbientQuietReason() + ")") : ""));
        }
      }
      if (text != null)
      {
        DateTimeOffset now = this.Now;
        this.integrationRules.Sample(null, now, true, this.store.Read<ProactiveStoreData>(([Nullable(1)] ProactiveStoreData d) => d));
        return;
      }
      this.integrationSampleInFlight = true;
      Task.Run(async delegate
      {
        ForegroundActivity activity = null;
        try
        {
          ForegroundApp foregroundApp = await ForegroundAppInfo.CaptureAsync(true, default(CancellationToken)).ConfigureAwait(false);
          if (foregroundApp != null && foregroundApp.ProcessId != Environment.ProcessId)
          {
            bool flag2;
            bool flag = ForegroundAppInfo.IsBrowserProcess(foregroundApp.ProcessName, out flag2);
            ValueTuple<string, string> valueTuple = ProactiveCoordinator.ParseAddress(foregroundApp.BrowserUrl);
            string item = valueTuple.Item1;
            string item2 = valueTuple.Item2;
            activity = new ForegroundActivity(foregroundApp.ProcessName, foregroundApp.AppName, flag, item, item2, foregroundApp.WindowTitle);
          }
        }
        catch (Exception ex)
        {
          Log.Info("Integrations", "foreground sample: " + ex.Message);
        }
        this.dq.TryEnqueue(delegate
        {
          this.integrationSampleInFlight = false;
          ProactiveStoreData proactiveStoreData = this.store.Read<ProactiveStoreData>(([Nullable(1)] ProactiveStoreData d) => d);
          ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate> valueTuple2 = this.integrationRules.Sample(activity, this.Now, this.IntegrationSuppressionReason() != null, proactiveStoreData);
          IntegrationSuggestionRules.SampleOutcome item3 = valueTuple2.Item1;
          IntegrationCandidate item4 = valueTuple2.Item2;
          if (item3 == IntegrationSuggestionRules.SampleOutcome.SpotifyTipReady)
          {
            this.PresentSpotifyTip();
            return;
          }
          if (item3 == IntegrationSuggestionRules.SampleOutcome.CandidateReady && item4 != null)
          {
            this.PresentIntegrationSuggestion(item4);
          }
        });
      });
    }

    // Token: 0x06000C42 RID: 3138 RVA: 0x0056074C File Offset: 0x0056074C
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Host", "Path" })]
    [return: Nullable(new byte[] { 0, 2, 2 })]
    private static ValueTuple<string, string> ParseAddress(string raw)
    {
      string text = ((raw != null) ? raw.Trim() : null);
      if (string.IsNullOrEmpty(text) || text.Contains(' '))
      {
        return new ValueTuple<string, string>(null, null);
      }
      if (!text.Contains("://", StringComparison.Ordinal))
      {
        text = "https://" + text;
      }
      Uri uri;
      if (!Uri.TryCreate(text, UriKind.Absolute, out uri) || string.IsNullOrEmpty(uri.Host))
      {
        return new ValueTuple<string, string>(null, null);
      }
      return new ValueTuple<string, string>(uri.Host.ToLowerInvariant().Trim('.'), uri.AbsolutePath);
    }

    // Token: 0x06000C43 RID: 3139 RVA: 0x005607D8 File Offset: 0x005607D8
    private void PresentIntegrationSuggestion(IntegrationCandidate candidate)
    {
      string text = this.IntegrationSuppressionReason();
      if (text != null)
      {
        Log.Info("Integrations", "suggestion suppressed before presentation (" + text + ") toolkit=" + candidate.ToolkitSlug);
        return;
      }
      AgentIntegrationInfo agentIntegrationInfo = this.catalog.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.ToolkitSlugs.Contains(candidate.ToolkitSlug, StringComparer.OrdinalIgnoreCase) || i.Id.Equals(candidate.ToolkitSlug, StringComparison.OrdinalIgnoreCase));
      if (agentIntegrationInfo == null || !agentIntegrationInfo.IsEligibleForConnectionSuggestion)
      {
        this.integrationRules.ResetDwell(candidate.ToolkitSlug);
        return;
      }
      IntegrationCandidate integrationCandidate = candidate.<Clone>$();
      integrationCandidate.ToolkitSlug = agentIntegrationInfo.PrimaryToolkitSlug;
      integrationCandidate.DisplayName = agentIntegrationInfo.DisplayName;
      this.activeIntegrationSuggestion = integrationCandidate;
      string text2 = "Integrations";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
      defaultInterpolatedStringHandler.AppendLiteral("presenting integration suggestion toolkit=");
      defaultInterpolatedStringHandler.AppendFormatted(agentIntegrationInfo.PrimaryToolkitSlug);
      defaultInterpolatedStringHandler.AppendLiteral(" (from ");
      defaultInterpolatedStringHandler.AppendFormatted(candidate.SourceApplicationName);
      defaultInterpolatedStringHandler.AppendFormatted((candidate.SourceHost == null) ? "" : (" " + candidate.SourceHost));
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.IntegrationSuggestion(agentIntegrationInfo.PrimaryToolkitSlug, agentIntegrationInfo.DisplayName), new TimeSpan?(ProactiveCoordinator.IntegrationSuggestionAutoHide));
      this.TrackIntegrationSuggestion("presented", this.activeIntegrationSuggestion);
      this.PlayEarcon("connectionQuestion", 0.85f);
      this.integrationRules.ResetDwell(candidate.ToolkitSlug);
      DateTimeOffset now = this.Now;
      Func<DateTimeOffset, bool> <>9__2;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        IEnumerable<DateTimeOffset> integrationPromptTimestamps = d.IntegrationPromptTimestamps;
        Func<DateTimeOffset, bool> func;
        if ((func = <>9__2) == null)
        {
          func = (<>9__2 = (DateTimeOffset t) => t >= now - IntegrationSuggestionRules.PromptsWindow);
        }
        d.IntegrationPromptTimestamps = integrationPromptTimestamps.Where<DateTimeOffset>(func).Append(now).ToList<DateTimeOffset>();
      });
      this.WatchForIntegrationCardGone();
    }

    // Token: 0x06000C44 RID: 3140 RVA: 0x005609A4 File Offset: 0x005609A4
    private void WatchForIntegrationCardGone()
    {
      DispatcherQueueTimer t = this.dq.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(1L);
      bool seen = false;
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        NotchPresentation.LiveEvent liveEvent = this.host.Services.Presenter.Presentation as NotchPresentation.LiveEvent;
        if (liveEvent != null && liveEvent.Event is NotchLiveEvent.IntegrationSuggestion)
        {
          seen = true;
          return;
        }
        if (!seen && this.activeIntegrationSuggestion != null)
        {
          return;
        }
        t.Stop();
        IntegrationCandidate integrationCandidate = this.activeIntegrationSuggestion;
        if ((integrationCandidate != null) & seen)
        {
          this.integrationRules.ResetDwell(integrationCandidate.ToolkitSlug);
          this.activeIntegrationSuggestion = null;
        }
      };
      t.Start();
    }

    // Token: 0x06000C45 RID: 3141 RVA: 0x00560A10 File Offset: 0x00560A10
    [NullableContext(2)]
    public void AnswerIntegrationSuggestion(string toolkit, IntegrationAnswer answer)
    {
      if (answer == IntegrationAnswer.Yes)
      {
        this.AcceptIntegrationSuggestion(toolkit);
        return;
      }
      if (answer != IntegrationAnswer.NotNow)
      {
        this.DeclineIntegrationSuggestion(toolkit);
        return;
      }
      this.SnoozeIntegrationSuggestion(toolkit);
    }

    // Token: 0x06000C46 RID: 3142 RVA: 0x00560A34 File Offset: 0x00560A34
    private void TrackIntegrationSuggestion(string outcome, [Nullable(2)] IntegrationCandidate suggestion)
    {
      if (suggestion == null)
      {
        return;
      }
      AgentIntegrationInfo agentIntegrationInfo = this.catalog.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.PrimaryToolkitSlug.Equals(suggestion.ToolkitSlug, StringComparison.OrdinalIgnoreCase));
      ClickyAnalytics.TrackIntegrationSuggestion(outcome, suggestion.ToolkitSlug, ((agentIntegrationInfo != null) ? agentIntegrationInfo.DisplayName : null) ?? suggestion.DisplayName, ((agentIntegrationInfo != null) ? agentIntegrationInfo.UseCases : null) ?? Array.Empty<string>(), (agentIntegrationInfo != null) ? agentIntegrationInfo.LogoUrl : null, suggestion.SourceApplicationName, suggestion.SourceHost);
    }

    // Token: 0x06000C47 RID: 3143 RVA: 0x00560AD4 File Offset: 0x00560AD4
    [NullableContext(2)]
    private string AnsweredSlug(string toolkit)
    {
      IntegrationCandidate integrationCandidate = this.activeIntegrationSuggestion;
      string text;
      if ((text = ((integrationCandidate != null) ? integrationCandidate.ToolkitSlug : null)) == null)
      {
        if (!string.IsNullOrWhiteSpace(toolkit))
        {
          return toolkit.ToLowerInvariant();
        }
        text = null;
      }
      return text;
    }

    // Token: 0x06000C48 RID: 3144 RVA: 0x00560AFC File Offset: 0x00560AFC
    [NullableContext(2)]
    public void AcceptIntegrationSuggestion(string toolkit = null)
    {
      ProactiveCoordinator.<>c__DisplayClass200_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass200_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.slug = this.AnsweredSlug(toolkit);
      if (CS$<>8__locals1.slug == null)
      {
        this.ClearIntegrationSuggestion();
        return;
      }
      this.TrackIntegrationSuggestion("accepted", this.activeIntegrationSuggestion);
      AgentIntegrationInfo agentIntegrationInfo = this.catalog.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.PrimaryToolkitSlug.Equals(CS$<>8__locals1.slug, StringComparison.OrdinalIgnoreCase));
      string text = ((agentIntegrationInfo != null) ? agentIntegrationInfo.Id : null) ?? CS$<>8__locals1.slug;
      string text2;
      if ((text2 = ((agentIntegrationInfo != null) ? agentIntegrationInfo.DisplayName : null)) == null)
      {
        IntegrationCandidate integrationCandidate = this.activeIntegrationSuggestion;
        text2 = ((integrationCandidate != null) ? integrationCandidate.DisplayName : null) ?? CS$<>8__locals1.slug;
      }
      ClickyAnalytics.TrackIntegrationInitiated(text, text2, ((agentIntegrationInfo != null) ? agentIntegrationInfo.ToolkitSlugs : null) ?? new <>z__ReadOnlySingleElementList<string>(CS$<>8__locals1.slug), "integration_suggestion", "not_connected", new int?(0), new int?(1));
      this.pendingSuggestionConnect = new ValueTuple<string, DateTimeOffset>?(new ValueTuple<string, DateTimeOffset>(CS$<>8__locals1.slug, DateTimeOffset.Now));
      this.Snooze(CS$<>8__locals1.slug);
      this.ClearIntegrationSuggestion();
      Log.Info("Integrations", "suggestion accepted: connecting " + CS$<>8__locals1.slug);
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass200_0.<<AcceptIntegrationSuggestion>b__1>d <<AcceptIntegrationSuggestion>b__1>d;
        <<AcceptIntegrationSuggestion>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<AcceptIntegrationSuggestion>b__1>d.<>4__this = CS$<>8__locals1;
        <<AcceptIntegrationSuggestion>b__1>d.<>1__state = -1;
        <<AcceptIntegrationSuggestion>b__1>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass200_0.<<AcceptIntegrationSuggestion>b__1>d>(ref <<AcceptIntegrationSuggestion>b__1>d);
        return <<AcceptIntegrationSuggestion>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C49 RID: 3145 RVA: 0x00560C2C File Offset: 0x00560C2C
    private static bool Matches(AgentIntegrationInfo i, string slug)
    {
      return i.Id.Equals(slug, StringComparison.OrdinalIgnoreCase) || i.ToolkitSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);
    }

    // Token: 0x06000C4A RID: 3146 RVA: 0x00560C50 File Offset: 0x00560C50
    [NullableContext(2)]
    public bool OnIntegrationAuthCallback(string toolkit, bool failed)
    {
      ProactiveCoordinator.PendingConnection pending = this.pendingConnection;
      if (pending == null)
      {
        return false;
      }
      if (toolkit != null && !toolkit.Equals(pending.Slug, StringComparison.OrdinalIgnoreCase) && !this.catalog.Any<AgentIntegrationInfo>((AgentIntegrationInfo i) => ProactiveCoordinator.Matches(i, pending.Slug) && ProactiveCoordinator.Matches(i, toolkit)))
      {
        return false;
      }
      if (failed)
      {
        Log.Info("Integrations", "suggested connection failed or was cancelled: " + pending.Slug);
        this.pendingConnection = null;
        return true;
      }
      this.VerifyPendingConnectionAsync();
      return true;
    }

    // Token: 0x06000C4B RID: 3147 RVA: 0x00560CF0 File Offset: 0x00560CF0
    private async Task PollPendingConnectionAsync()
    {
      for (;;)
      {
        ProactiveCoordinator.PendingConnection pending = this.pendingConnection;
        if (pending == null)
        {
          break;
        }
        await Task.Delay(4000);
        if (this.pendingConnection != pending)
        {
          break;
        }
        if (DateTimeOffset.Now - pending.Started > ProactiveCoordinator.PendingConnectionLifetime)
        {
          goto Block_2;
        }
        await this.VerifyPendingConnectionAsync();
      }
      return;
      Block_2:
      this.pendingConnection = null;
    }

    // Token: 0x06000C4C RID: 3148 RVA: 0x00560D34 File Offset: 0x00560D34
    private async Task VerifyPendingConnectionAsync()
    {
      if (this.pendingConnection != null)
      {
        IReadOnlyList<AgentIntegrationInfo> readOnlyList = await this.RefreshIntegrationsAsync();
        ProactiveCoordinator.PendingConnection pending = this.pendingConnection;
        if (pending != null)
        {
          AgentIntegrationInfo agentIntegrationInfo = readOnlyList.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => ProactiveCoordinator.Matches(i, pending.Slug));
          if (agentIntegrationInfo != null && agentIntegrationInfo.IsActiveConnection && agentIntegrationInfo.ConnectedCount > pending.Baseline)
          {
            this.pendingConnection = null;
            string text = "Integrations";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
            defaultInterpolatedStringHandler.AppendLiteral("connection verified: ");
            defaultInterpolatedStringHandler.AppendFormatted(agentIntegrationInfo.Id);
            defaultInterpolatedStringHandler.AppendLiteral(" (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(agentIntegrationInfo.ConnectedCount);
            defaultInterpolatedStringHandler.AppendLiteral(" account(s))");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            this.real.Agents.InvalidateConnectedApps();
            this.PresentIntegrationConnected(agentIntegrationInfo);
          }
        }
      }
    }

    // Token: 0x06000C4D RID: 3149 RVA: 0x00560D78 File Offset: 0x00560D78
    private void PresentIntegrationConnected(AgentIntegrationInfo entry)
    {
      string text = this.IntegrationConnectedRefusal();
      if (text != null)
      {
        Log.Info("Integrations", "connected toast held back (" + text + "): " + entry.Id);
        return;
      }
      Log.Info("Integrations", "presenting the connected toast: " + entry.DisplayName);
      this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.IntegrationConnected(entry.PrimaryToolkitSlug, entry.DisplayName, entry.LogoUrl), null);
    }

    // Token: 0x06000C4E RID: 3150 RVA: 0x00560E00 File Offset: 0x00560E00
    [NullableContext(2)]
    private string IntegrationConnectedRefusal()
    {
      if (this.host.IsPreSignInIntroShowing || this.host.IsIntroSequenceActive || this.host.TourHoldsHome)
      {
        return "intro";
      }
      if (!this.CanAdvancePastPermissions)
      {
        return "permissionsIncomplete";
      }
      if (!this.HasCompletedOnboarding)
      {
        return "onboardingIncomplete";
      }
      if (this.real.Companion.State != VoiceState.Idle)
      {
        return "voiceBusy";
      }
      NotchPresentation presentation = this.host.Services.Presenter.Presentation;
      string text;
      if (!(presentation is NotchPresentation.TextInput) && !(presentation is NotchPresentation.TextResponse) && !(presentation is NotchPresentation.OnboardingPinned))
      {
        NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
        if (liveEvent != null)
        {
          NotchLiveEvent @event = liveEvent.Event;
          if (@event is NotchLiveEvent.ActivityListening || @event is NotchLiveEvent.ActivityThinking || @event is NotchLiveEvent.ActivitySpeaking || @event is NotchLiveEvent.ActivityCustomStatus || @event is NotchLiveEvent.Dictation || @event is NotchLiveEvent.MeetingCountdown || @event is NotchLiveEvent.OnboardingTutorialInvite || @event is NotchLiveEvent.OnboardingTutorialResume || @event is NotchLiveEvent.MorningSuggestions || @event is NotchLiveEvent.AgentPeek)
          {
            return "notchBusy";
          }
        }
        text = null;
      }
      else
      {
        text = "notchExpanded";
      }
      return text;
    }

    // Token: 0x06000C4F RID: 3151 RVA: 0x00560F14 File Offset: 0x00560F14
    [NullableContext(2)]
    public void SnoozeIntegrationSuggestion(string toolkit = null)
    {
      string text = this.AnsweredSlug(toolkit);
      if (text == null)
      {
        this.ClearIntegrationSuggestion();
        return;
      }
      this.TrackIntegrationSuggestion("snoozed", this.activeIntegrationSuggestion);
      this.Snooze(text);
      this.ClearIntegrationSuggestion();
      Log.Info("Integrations", "suggestion snoozed: " + text);
    }

    // Token: 0x06000C50 RID: 3152 RVA: 0x00560F68 File Offset: 0x00560F68
    [NullableContext(2)]
    public void DeclineIntegrationSuggestion(string toolkit = null)
    {
      string slug = this.AnsweredSlug(toolkit);
      if (slug == null)
      {
        this.ClearIntegrationSuggestion();
        return;
      }
      this.TrackIntegrationSuggestion("declined", this.activeIntegrationSuggestion);
      this.store.Update(delegate(ProactiveStoreData d)
      {
        if (!d.IntegrationNeverAsk.Contains(slug))
        {
          d.IntegrationNeverAsk.Add(slug);
        }
      });
      this.integrationRules.ResetDwell(slug);
      this.ClearIntegrationSuggestion();
      Log.Info("Integrations", "suggestion declined: never asking about " + slug + " again");
    }

    // Token: 0x06000C51 RID: 3153 RVA: 0x00560FF8 File Offset: 0x00560FF8
    private void Snooze(string slug)
    {
      DateTimeOffset until = this.Now + IntegrationSuggestionRules.NotNowSnooze;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.IntegrationSnoozedUntil[slug] = until;
      });
      this.integrationRules.ResetDwell(slug);
    }

    // Token: 0x06000C52 RID: 3154 RVA: 0x00561050 File Offset: 0x00561050
    private void ClearIntegrationSuggestion()
    {
      this.activeIntegrationSuggestion = null;
      this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.IntegrationSuggestion));
    }

    // Token: 0x06000C53 RID: 3155 RVA: 0x00561078 File Offset: 0x00561078
    private void PresentSpotifyTip()
    {
      if (this.spotifyTipActive || this.IntegrationSuppressionReason() != null)
      {
        return;
      }
      this.spotifyTipActive = true;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.SpotifyVoiceTipShown = true;
      });
      this.integrationRules.ResetSpotifyDwell();
      Log.Info("Integrations", "presenting the Spotify voice tip");
      this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.SpotifyVoiceTip(), null);
      this.PlayEarcon("connectionQuestion", 0.85f);
    }

    // Token: 0x06000C54 RID: 3156 RVA: 0x00561114 File Offset: 0x00561114
    public void AcknowledgeSpotifyTip()
    {
      this.spotifyTipActive = false;
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.SpotifyVoiceTipShown = true;
      });
      this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.SpotifyVoiceTip));
    }

    // Token: 0x06000C55 RID: 3157 RVA: 0x00561174 File Offset: 0x00561174
    public void ResetIntegrationSuggestionPreferences()
    {
      this.StopIntegrationMonitor();
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.IntegrationNeverAsk.Clear();
        d.IntegrationSnoozedUntil.Clear();
        d.IntegrationPromptTimestamps.Clear();
        d.SpotifyVoiceTipShown = false;
      });
      this.UpdateIntegrationMonitor();
    }

    // Token: 0x17000231 RID: 561
    // (get) Token: 0x06000C56 RID: 3158 RVA: 0x005611AC File Offset: 0x005611AC
    public bool IsUpcomingMeetingsEnabled
    {
      get
      {
        return this.real.Core.Settings.Read<bool>((AppSettings s) => s.UpcomingMeetingsEnabled);
      }
    }

    // Token: 0x17000232 RID: 562
    // (get) Token: 0x06000C57 RID: 3159 RVA: 0x005611E4 File Offset: 0x005611E4
    public IReadOnlyList<UpcomingCalendarEvent> UpcomingEvents
    {
      get
      {
        return this.upcomingEvents;
      }
    }

    // Token: 0x17000233 RID: 563
    // (get) Token: 0x06000C58 RID: 3160 RVA: 0x005611EC File Offset: 0x005611EC
    [Nullable(2)]
    public UpcomingCalendarEvent ActiveMeeting
    {
      [NullableContext(2)]
      get
      {
        return this.activeMeeting;
      }
    }

    // Token: 0x06000C59 RID: 3161 RVA: 0x005611F4 File Offset: 0x005611F4
    private void UpdateMeetingsScheduler()
    {
      if (this.accountKey == null || !this.IsUpcomingMeetingsEnabled)
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.meetingsTimer;
        if (dispatcherQueueTimer != null)
        {
          dispatcherQueueTimer.Stop();
        }
        if (this.activeMeeting != null)
        {
          this.ClearActiveMeeting();
        }
        this.upcomingEvents = Array.Empty<UpcomingCalendarEvent>();
        return;
      }
      if (this.meetingsTimer == null)
      {
        this.meetingsTimer = this.dq.CreateTimer();
        this.meetingsTimer.Interval = MeetingCountdownRules.TickInterval;
        this.meetingsTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.TickMeetings();
        };
      }
      if (!this.meetingsTimer.IsRunning)
      {
        this.meetingsTimer.Start();
      }
      this.TickMeetings();
    }

    // Token: 0x06000C5A RID: 3162 RVA: 0x0056129C File Offset: 0x0056129C
    private void TickMeetings()
    {
      if (this.accountKey == null || !this.IsUpcomingMeetingsEnabled)
      {
        return;
      }
      this.RefreshMeetingsIfStale(false);
      this.UpdateActiveMeeting();
    }

    // Token: 0x06000C5B RID: 3163 RVA: 0x005612BC File Offset: 0x005612BC
    public void RefreshMeetingsForNotchExpand()
    {
      this.RefreshMeetingsIfStale(true);
    }

    // Token: 0x06000C5C RID: 3164 RVA: 0x005612C8 File Offset: 0x005612C8
    private void RefreshMeetingsIfStale(bool triggeredByNotchExpand = false)
    {
      ProactiveCoordinator.<>c__DisplayClass236_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass236_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.accountKey == null || !this.IsUpcomingMeetingsEnabled || this.meetingsRefreshInFlight)
      {
        return;
      }
      if (this.meetingsClient.TestResponse == null && this.catalog.Count > 0)
      {
        if (!this.catalog.Any<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.IsActiveConnection && i.PrimaryToolkitSlug.Equals("googlecalendar", StringComparison.OrdinalIgnoreCase)))
        {
          this.meetingsBackendConnected = false;
          this.upcomingEvents = Array.Empty<UpcomingCalendarEvent>();
          return;
        }
      }
      if (!MeetingCountdownRules.ShouldRefresh(DateTimeOffset.Now, this.meetingsRefreshedAt, this.meetingsFailedAt, this.meetingsBackendConnected, triggeredByNotchExpand))
      {
        return;
      }
      this.meetingsRefreshInFlight = true;
      CS$<>8__locals1.generation = this.meetingsAuthGeneration;
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass236_0.<<RefreshMeetingsIfStale>b__1>d <<RefreshMeetingsIfStale>b__1>d;
        <<RefreshMeetingsIfStale>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<RefreshMeetingsIfStale>b__1>d.<>4__this = CS$<>8__locals1;
        <<RefreshMeetingsIfStale>b__1>d.<>1__state = -1;
        <<RefreshMeetingsIfStale>b__1>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass236_0.<<RefreshMeetingsIfStale>b__1>d>(ref <<RefreshMeetingsIfStale>b__1>d);
        return <<RefreshMeetingsIfStale>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C5D RID: 3165 RVA: 0x0056139C File Offset: 0x0056139C
    private void UpdateActiveMeeting()
    {
      bool flag = this.HasCompletedOnboarding && this.CanAdvancePastPermissions && !this.IsTutorialEngaged;
      UpcomingCalendarEvent upcomingCalendarEvent = this.activeMeeting;
      DateTimeOffset now = this.Now;
      ValueTuple<UpcomingCalendarEvent, MeetingCountdownRules.Change> valueTuple = MeetingCountdownRules.Step(this.activeMeeting, this.upcomingEvents, this.announcedMeetings, now, flag);
      UpcomingCalendarEvent item = valueTuple.Item1;
      MeetingCountdownRules.Change item2 = valueTuple.Item2;
      bool flag2 = upcomingCalendarEvent != null;
      if (flag2)
      {
        bool flag3 = item2 == MeetingCountdownRules.Change.Cleared || item2 == MeetingCountdownRules.Change.Presented;
        flag2 = flag3;
      }
      if (flag2)
      {
        ClickyAnalytics.TrackMeetingCountdownDismissed((now > upcomingCalendarEvent.StartsAt && !upcomingCalendarEvent.IsOngoing(now)) ? "meeting_ended" : "removed_from_schedule", ProactiveCoordinator.ActionKindWire(upcomingCalendarEvent), ProactiveCoordinator.SecondsUntilStart(upcomingCalendarEvent, now));
      }
      if (item2 == MeetingCountdownRules.Change.Presented)
      {
        ClickyAnalytics.TrackMeetingCountdownPresented(ProactiveCoordinator.ActionKindWire(item), ProactiveCoordinator.SecondsUntilStart(item, now));
      }
      switch (item2)
      {
      case MeetingCountdownRules.Change.Cleared:
        Log.Info("Meetings", "countdown cleared (meeting ended or left the schedule)");
        this.activeMeeting = null;
        this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MeetingCountdown));
        if (item != null)
        {
          this.ShowMeeting(item, true);
          return;
        }
        break;
      case MeetingCountdownRules.Change.Updated:
        this.ShowMeeting(item, false);
        return;
      case MeetingCountdownRules.Change.Presented:
      {
        string text = "Meetings";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 3);
        defaultInterpolatedStringHandler.AppendLiteral("countdown: \"");
        defaultInterpolatedStringHandler.AppendFormatted(item.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" at ");
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(item.StartsAt.LocalDateTime, "HH:mm");
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        MeetingActionKind? meetingActionKind;
        defaultInterpolatedStringHandler.AppendFormatted(((item.PrimaryActionKind != null) ? meetingActionKind.GetValueOrDefault().ToString() : null) ?? "no action");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        this.ShowMeeting(item, true);
        break;
      }
      default:
        return;
      }
    }

    // Token: 0x06000C5E RID: 3166 RVA: 0x00561584 File Offset: 0x00561584
    private void ShowMeeting(UpcomingCalendarEvent meeting, bool chime)
    {
      this.activeMeeting = meeting;
      this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.MeetingCountdown(meeting.Title, meeting.StartsAt - this.clockOffset, meeting.PrimaryActionUrl, ProactiveCoordinator.ActionKindWire(meeting)), null);
      if (chime)
      {
        this.PlayEarcon("connectionQuestion", 0.85f);
      }
    }

    // Token: 0x06000C5F RID: 3167 RVA: 0x005615F4 File Offset: 0x005615F4
    [return: Nullable(2)]
    private static string ActionKindWire(UpcomingCalendarEvent meeting)
    {
      MeetingActionKind? primaryActionKind = meeting.PrimaryActionKind;
      if (primaryActionKind != null)
      {
        switch (primaryActionKind.GetValueOrDefault())
        {
        case MeetingActionKind.Join:
          return "join";
        case MeetingActionKind.Open:
          return "open";
        case MeetingActionKind.Directions:
          return "directions";
        }
      }
      return null;
    }

    // Token: 0x06000C60 RID: 3168 RVA: 0x00561648 File Offset: 0x00561648
    private static int SecondsUntilStart(UpcomingCalendarEvent meeting, DateTimeOffset now)
    {
      return (int)(meeting.StartsAt - now).TotalSeconds;
    }

    // Token: 0x06000C61 RID: 3169 RVA: 0x0056166C File Offset: 0x0056166C
    public void JoinActiveMeeting()
    {
      UpcomingCalendarEvent upcomingCalendarEvent = this.activeMeeting;
      if (upcomingCalendarEvent != null)
      {
        Uri primaryActionUrl = upcomingCalendarEvent.PrimaryActionUrl;
        if (primaryActionUrl != null)
        {
          ClickyAnalytics.TrackMeetingCountdownActionClicked(ProactiveCoordinator.ActionKindWire(upcomingCalendarEvent), ProactiveCoordinator.SecondsUntilStart(upcomingCalendarEvent, this.Now));
          this.host.OpenUrl(primaryActionUrl.ToString());
          this.ClearActiveMeeting();
          return;
        }
      }
      this.ClearActiveMeeting();
    }

    // Token: 0x06000C62 RID: 3170 RVA: 0x005616C4 File Offset: 0x005616C4
    public void DismissActiveMeeting()
    {
      UpcomingCalendarEvent upcomingCalendarEvent = this.activeMeeting;
      if (upcomingCalendarEvent != null)
      {
        ClickyAnalytics.TrackMeetingCountdownDismissed("closed_by_user", ProactiveCoordinator.ActionKindWire(upcomingCalendarEvent), ProactiveCoordinator.SecondsUntilStart(upcomingCalendarEvent, this.Now));
      }
      this.ClearActiveMeeting();
    }

    // Token: 0x06000C63 RID: 3171 RVA: 0x00561700 File Offset: 0x00561700
    private void ClearActiveMeeting()
    {
      this.activeMeeting = null;
      this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MeetingCountdown));
    }

    // Token: 0x06000C64 RID: 3172 RVA: 0x00561728 File Offset: 0x00561728
    private void ResetMeetingsForAuthChange()
    {
      this.meetingsAuthGeneration++;
      if (this.activeMeeting != null)
      {
        this.ClearActiveMeeting();
      }
      this.upcomingEvents = Array.Empty<UpcomingCalendarEvent>();
      this.meetingsBackendConnected = false;
      this.meetingsRefreshedAt = (this.meetingsFailedAt = null);
      this.announcedMeetings.Clear();
    }

    // Token: 0x06000C65 RID: 3173 RVA: 0x00561788 File Offset: 0x00561788
    private void DisposeMeetings()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.meetingsTimer;
      if (dispatcherQueueTimer == null)
      {
        return;
      }
      dispatcherQueueTimer.Stop();
    }

    // Token: 0x17000234 RID: 564
    // (get) Token: 0x06000C66 RID: 3174 RVA: 0x0056179C File Offset: 0x0056179C
    [Nullable(2)]
    public MorningSuggestionsNotice MorningNotice
    {
      [NullableContext(2)]
      get
      {
        return this.morningNotice;
      }
    }

    // Token: 0x17000235 RID: 565
    // (get) Token: 0x06000C67 RID: 3175 RVA: 0x005617A4 File Offset: 0x005617A4
    private bool MorningPaperCardWouldShow
    {
      get
      {
        return GoodMorningBrief.HasTodaysResearch(this.real.Core.Settings, this.real.Core.Auth.UserId);
      }
    }

    // Token: 0x06000C68 RID: 3176 RVA: 0x005617D0 File Offset: 0x005617D0
    private bool InvitationHasSomewhereToGo(MorningSuggestionsNotice notice)
    {
      return notice.IsDebugPreview;
    }

    // Token: 0x17000236 RID: 566
    // (get) Token: 0x06000C69 RID: 3177 RVA: 0x005617D8 File Offset: 0x005617D8
    private bool IsMorningLaneResting
    {
      get
      {
        return MorningSuggestionsInvitationPolicy.IsResting(this.store.Read<MorningDismissalHistory>(([Nullable(1)] ProactiveStoreData d) => d.MorningDismissals), this.Now);
      }
    }

    // Token: 0x17000237 RID: 567
    // (get) Token: 0x06000C6A RID: 3178 RVA: 0x00561810 File Offset: 0x00561810
    private DateTimeOffset? GreetingPresentedAtToday
    {
      get
      {
        DateTimeOffset? dateTimeOffset = this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.MorningGreetingLastPresentedAt);
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          if (valueOrDefault.LocalDateTime.Date == this.Now.LocalDateTime.Date)
          {
            return new DateTimeOffset?(valueOrDefault);
          }
        }
        return null;
      }
    }

    // Token: 0x06000C6B RID: 3179 RVA: 0x00561898 File Offset: 0x00561898
    private void QueueMorningNotice()
    {
      this.DismissMorningNotice();
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.MorningSettledAt = null;
      });
      this.RebuildMorningInvitationIfNeeded("queued");
    }

    // Token: 0x06000C6C RID: 3180 RVA: 0x005618D8 File Offset: 0x005618D8
    private void RebuildMorningInvitationIfNeeded(string reason)
    {
      MorningSuggestionsInvitationPolicy.RebuildDecision rebuildDecision = MorningSuggestionsInvitationPolicy.Decide(this.morningNotice != null, this.IsEnabled, this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.MorningSettledAt), this.IsSuggestionsPageVisible, this.cards, this.GreetingPresentedAtToday, this.morningSweepStartedAt, this.Now, this.IsMorningLaneResting);
      MorningSuggestionsInvitationPolicy.RebuildKind kind = rebuildDecision.Kind;
      if (kind == MorningSuggestionsInvitationPolicy.RebuildKind.SettleForToday)
      {
        this.store.Update(delegate(ProactiveStoreData d)
        {
          d.MorningSettledAt = new DateTimeOffset?(this.Now);
        });
        return;
      }
      if (kind != MorningSuggestionsInvitationPolicy.RebuildKind.Rebuild)
      {
        return;
      }
      this.morningNotice = new MorningSuggestionsNotice(Guid.NewGuid(), rebuildDecision.CardIds, rebuildDecision.CreatedAt, rebuildDecision.PresentNotBefore, null);
      this.heldBackReasons.Clear();
      string text = "Proactive";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 3);
      defaultInterpolatedStringHandler.AppendLiteral("morning invitation ");
      defaultInterpolatedStringHandler.AppendFormatted(reason);
      defaultInterpolatedStringHandler.AppendLiteral(" for ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(rebuildDecision.CardIds.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" card(s); presents no earlier than ");
      defaultInterpolatedStringHandler.AppendFormatted<DateTime>(rebuildDecision.PresentNotBefore.LocalDateTime, "HH:mm:ss");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.ScheduleReadiness(TimeSpan.Zero);
    }

    // Token: 0x06000C6D RID: 3181 RVA: 0x00561A30 File Offset: 0x00561A30
    private void ScheduleReadiness(TimeSpan delay)
    {
      MorningSuggestionsNotice morningSuggestionsNotice = this.morningNotice;
      if (morningSuggestionsNotice != null && !morningSuggestionsNotice.IsDebugPreview)
      {
        return;
      }
      if (this.readinessTimer == null)
      {
        this.readinessTimer = this.CreateReadinessTimer();
      }
      this.readinessTimer.Stop();
      this.readinessTimer.Interval = ((delay < TimeSpan.FromMilliseconds(50L, 0L)) ? TimeSpan.FromMilliseconds(50L, 0L) : delay);
      this.readinessTimer.Start();
    }

    // Token: 0x06000C6E RID: 3182 RVA: 0x00561AA4 File Offset: 0x00561AA4
    private DispatcherQueueTimer CreateReadinessTimer()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.CheckMorningReadiness();
      };
      return dispatcherQueueTimer;
    }

    // Token: 0x06000C6F RID: 3183 RVA: 0x00561AD0 File Offset: 0x00561AD0
    private void CheckMorningReadiness()
    {
      ProactiveCoordinator.<>c__DisplayClass269_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass269_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.notice = this.morningNotice;
      if (CS$<>8__locals1.notice == null)
      {
        return;
      }
      if (this.IsMorningShowing)
      {
        return;
      }
      if (!this.InvitationHasSomewhereToGo(CS$<>8__locals1.notice))
      {
        return;
      }
      DateTimeOffset now = this.Now;
      if (CS$<>8__locals1.notice.RemainingCardCount(this.cards, now) == 0 && !CS$<>8__locals1.notice.IsDebugPreview)
      {
        this.DismissMorningNotice();
        return;
      }
      if (now < CS$<>8__locals1.notice.PresentNotBefore)
      {
        this.ScheduleReadiness(CS$<>8__locals1.notice.PresentNotBefore - now);
        return;
      }
      if (!CS$<>8__locals1.notice.IsDebugPreview)
      {
        if (!ProactiveCoordinator.MorningInvitationHasASurface)
        {
          return;
        }
        if (ProactiveCoordinator.IdleSeconds() > ProactiveCoordinator.ActiveRecency.TotalSeconds)
        {
          this.HeldBack("user_away");
          this.ScheduleReadiness(ProactiveCoordinator.InvitationRecheck);
          return;
        }
        QuietReason quietReason = QuietContext.Current();
        if (quietReason != QuietReason.None)
        {
          this.HeldBack((quietReason == QuietReason.OnCall) ? "on_call" : ((quietReason == QuietReason.DoNotDisturb) ? "do_not_disturb" : "quiet_context"));
          this.ScheduleReadiness(ProactiveCoordinator.InvitationRecheck);
          return;
        }
        string text = this.BlockingReason();
        if (text != null)
        {
          this.HeldBack(text);
          this.ScheduleReadiness(ProactiveCoordinator.InvitationRecheck);
          return;
        }
        if (this.claimedNoticeId != CS$<>8__locals1.notice.Id)
        {
          ProactiveCoordinator.<>c__DisplayClass269_1 CS$<>8__locals2 = new ProactiveCoordinator.<>c__DisplayClass269_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          if (this.claimInFlight)
          {
            return;
          }
          this.claimInFlight = true;
          CS$<>8__locals2.ids = (from c in this.cards
            where CS$<>8__locals2.CS$<>8__locals1.notice.CardIds.Contains(c.Id)
            select c.ServerSuggestionId into i
            where i != null
            select i).Cast<int>().ToList<int>();
          Task.Run(delegate
          {
            ProactiveCoordinator.<>c__DisplayClass269_1.<<CheckMorningReadiness>b__3>d <<CheckMorningReadiness>b__3>d;
            <<CheckMorningReadiness>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
            <<CheckMorningReadiness>b__3>d.<>4__this = CS$<>8__locals2;
            <<CheckMorningReadiness>b__3>d.<>1__state = -1;
            <<CheckMorningReadiness>b__3>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass269_1.<<CheckMorningReadiness>b__3>d>(ref <<CheckMorningReadiness>b__3>d);
            return <<CheckMorningReadiness>b__3>d.<>t__builder.Task;
          });
          return;
        }
      }
      this.PresentMorningNotice(CS$<>8__locals1.notice);
      if (!this.IsMorningShowing)
      {
        this.HeldBack("notch_busy");
        this.ScheduleReadiness(ProactiveCoordinator.InvitationRecheck);
      }
    }

    // Token: 0x06000C70 RID: 3184 RVA: 0x00561D0C File Offset: 0x00561D0C
    private void HeldBack(string reason)
    {
      if (!this.heldBackReasons.Add(reason))
      {
        return;
      }
      Log.Info("Proactive", "morning invitation held: " + reason);
      MorningSuggestionsNotice morningSuggestionsNotice = this.morningNotice;
      if (morningSuggestionsNotice != null)
      {
        string text = "morning_suggestions_invitation_held_back";
        string text2 = null;
        string text3 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["reason"] = reason;
        jsonObject["invitation_id"] = morningSuggestionsNotice.Id.ToString().ToUpperInvariant();
        jsonObject["waiting_minutes"] = this.WaitingMinutes(morningSuggestionsNotice);
        jsonObject["suggestion_count"] = morningSuggestionsNotice.RemainingCardCount(this.cards, this.Now);
        ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      }
    }

    // Token: 0x06000C71 RID: 3185 RVA: 0x00561DD8 File Offset: 0x00561DD8
    private int WaitingMinutes(MorningSuggestionsNotice notice)
    {
      return Math.Max(0, (int)(this.Now - notice.PresentNotBefore).TotalMinutes);
    }

    // Token: 0x06000C72 RID: 3186 RVA: 0x00561E08 File Offset: 0x00561E08
    [NullableContext(2)]
    private string BlockingReason()
    {
      if (this.IsTutorialEngaged)
      {
        return "onboarding";
      }
      if (!this.HasCompletedOnboarding || !this.CanAdvancePastPermissions)
      {
        return "onboarding";
      }
      NotchPresentation presentation = this.host.Services.Presenter.Presentation;
      bool flag;
      if (!(presentation is NotchPresentation.TextInput) && !(presentation is NotchPresentation.TextResponse))
      {
        NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
        if (liveEvent == null || !(liveEvent.Event is NotchLiveEvent.AgentPeek))
        {
          flag = false;
          goto IL_0067;
        }
      }
      flag = true;
      IL_0067:
      if (flag)
      {
        return "notch_expanded";
      }
      if (this.IsWithinPostOnboardingQuietPeriod)
      {
        return "post_onboarding_quiet_hour";
      }
      if (MorningSuggestionsInvitationPolicy.IsSettled(this.store.Read<DateTimeOffset?>((ProactiveStoreData d) => d.MorningSettledAt), this.Now))
      {
        return "settled";
      }
      return null;
    }

    // Token: 0x17000238 RID: 568
    // (get) Token: 0x06000C73 RID: 3187 RVA: 0x00561ED4 File Offset: 0x00561ED4
    private bool IsMorningShowing
    {
      get
      {
        NotchPresentation.LiveEvent liveEvent = this.host.Services.Presenter.Presentation as NotchPresentation.LiveEvent;
        return liveEvent != null && liveEvent.Event is NotchLiveEvent.MorningSuggestions;
      }
    }

    // Token: 0x06000C74 RID: 3188 RVA: 0x00561F10 File Offset: 0x00561F10
    private void PresentMorningNotice(MorningSuggestionsNotice notice)
    {
      int num = notice.RemainingCardCount(this.cards, this.Now);
      if (num == 0)
      {
        return;
      }
      this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.MorningSuggestions(num), null);
      if (!this.IsMorningShowing)
      {
        return;
      }
      if (this.chimedNoticeId != notice.Id)
      {
        this.chimedNoticeId = new Guid?(notice.Id);
        this.PlayEarcon("clickyQuestion", 0.28f);
        string text = "Proactive";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
        defaultInterpolatedStringHandler.AppendLiteral("morning invitation shown (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" suggestion(s)");
        defaultInterpolatedStringHandler.AppendFormatted((this.heldBackReasons.Count > 0) ? (", held by " + string.Join(", ", this.heldBackReasons)) : "");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        string text2 = "morning_suggestions_invitation_action";
        string text3 = null;
        string text4 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["action"] = "shown";
        jsonObject["invitation_id"] = notice.Id.ToString().ToUpperInvariant();
        jsonObject["suggestion_count"] = num;
        jsonObject["waiting_minutes"] = this.WaitingMinutes(notice);
        jsonObject["held_back_reasons"] = new JsonArray((from r in this.heldBackReasons.Order<string>(StringComparer.Ordinal)
          select JsonValue.Create(r, null)).ToArray<JsonNode>());
        ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
      }
    }

    // Token: 0x06000C75 RID: 3189 RVA: 0x005620F8 File Offset: 0x005620F8
    private void ReconcileMorningNotice()
    {
      MorningSuggestionsNotice morningSuggestionsNotice = this.morningNotice;
      if (morningSuggestionsNotice != null && morningSuggestionsNotice.RemainingCardCount(this.cards, this.Now) == 0)
      {
        this.DismissMorningNotice();
        return;
      }
      MorningSuggestionsNotice morningSuggestionsNotice2 = this.morningNotice;
      if (morningSuggestionsNotice2 != null && this.IsMorningShowing)
      {
        this.host.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.MorningSuggestions(morningSuggestionsNotice2.RemainingCardCount(this.cards, this.Now)), null);
      }
    }

    // Token: 0x06000C76 RID: 3190 RVA: 0x00562174 File Offset: 0x00562174
    private void DismissMorningNotice()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.readinessTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      this.claimInFlight = false;
      this.morningNotice = null;
      this.host.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MorningSuggestions));
    }

    // Token: 0x06000C77 RID: 3191 RVA: 0x005621B4 File Offset: 0x005621B4
    private void SettleMorningNoticeForToday()
    {
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.MorningSettledAt = new DateTimeOffset?(this.Now);
      });
      this.DismissMorningNotice();
    }

    // Token: 0x06000C78 RID: 3192 RVA: 0x005621D4 File Offset: 0x005621D4
    public void OpenSuggestionsFromMorningNotch()
    {
      Log.Info("Proactive", "morning invitation: Show me");
      string text = "morning_suggestions_invitation_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "open";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      this.SettleMorningNoticeForToday();
      NotchPresenter corePresenter = this.host.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      corePresenter.SuppressHoverUntilPointerReentry();
    }

    // Token: 0x06000C79 RID: 3193 RVA: 0x0056223C File Offset: 0x0056223C
    public void PlayMorningPaperFromNotch()
    {
      Log.Info("Proactive", "morning invitation: Read it");
      string text = "morning_suggestions_invitation_action";
      string text2 = null;
      string text3 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "read_paper";
      ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
      this.SettleMorningNoticeForToday();
      NotchPresenter corePresenter = this.host.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.SuppressHoverUntilPointerReentry();
      }
      GoodMorningBrief shared = GoodMorningBrief.Shared;
      if (shared == null)
      {
        return;
      }
      shared.Play("notch_read_it");
    }

    // Token: 0x06000C7A RID: 3194 RVA: 0x005622B8 File Offset: 0x005622B8
    public void DismissMorningNoticeFromExplicitClose()
    {
      MorningSuggestionsNotice morningSuggestionsNotice = this.morningNotice;
      if (morningSuggestionsNotice != null && !morningSuggestionsNotice.IsDebugPreview)
      {
        MorningDismissalHistory updated = MorningSuggestionsInvitationPolicy.RecordingDismissal(this.store.Read<MorningDismissalHistory>(([Nullable(1)] ProactiveStoreData d) => d.MorningDismissals), this.Now);
        this.store.Update(delegate(ProactiveStoreData d)
        {
          d.MorningDismissals = updated;
        });
        DateTimeOffset? restingUntil = updated.RestingUntil;
        if (restingUntil != null)
        {
          DateTimeOffset valueOrDefault = restingUntil.GetValueOrDefault();
          string text = "Proactive";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(60, 1);
          defaultInterpolatedStringHandler.AppendLiteral("morning suggestions resting until ");
          defaultInterpolatedStringHandler.AppendFormatted<DateTime>(valueOrDefault.LocalDateTime, "g");
          defaultInterpolatedStringHandler.AppendLiteral(" after repeated dismissals");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          string text2 = "morning_suggestions_invitation_action";
          string text3 = null;
          string text4 = null;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["action"] = "rest_started";
          ClickyAnalytics.TrackProductEvent(text2, text3, text4, jsonObject);
        }
      }
      string text5 = "morning_suggestions_invitation_action";
      string text6 = null;
      string text7 = null;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["action"] = "dismiss";
      ClickyAnalytics.TrackProductEvent(text5, text6, text7, jsonObject2);
      Log.Info("Proactive", "morning invitation: dismissed");
      this.SettleMorningNoticeForToday();
    }

    // Token: 0x06000C7B RID: 3195 RVA: 0x00562410 File Offset: 0x00562410
    private void NoteSuggestionApprovedForMorningStreak()
    {
      MorningDismissalHistory history = this.store.Read<MorningDismissalHistory>(([Nullable(1)] ProactiveStoreData d) => d.MorningDismissals);
      if (history.DismissedDays.Count == 0 && history.RestingUntil == null)
      {
        return;
      }
      this.store.Update(delegate(ProactiveStoreData d)
      {
        d.MorningDismissals = MorningSuggestionsInvitationPolicy.RecordingApproval(history);
      });
    }

    // Token: 0x06000C7C RID: 3196 RVA: 0x00562494 File Offset: 0x00562494
    public void PreviewMorningNotch()
    {
      this.DismissMorningNotice();
      this.host.CollapseNotchForPreview();
      List<ProactiveSuggestionCard> list = this.cards.Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => !c.IsExpired(this.Now)).ToList<ProactiveSuggestionCard>();
      this.morningNotice = new MorningSuggestionsNotice(Guid.NewGuid(), list.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id).ToHashSet<Guid>(), this.Now, this.Now, new int?((list.Count == 0) ? 3 : list.Count));
      this.PresentMorningNotice(this.morningNotice);
    }

    // Token: 0x06000C7D RID: 3197 RVA: 0x00562538 File Offset: 0x00562538
    private void PlayEarcon(string name, float volume)
    {
      try
      {
        if (!(Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1"))
        {
          this.real.Audio.PlayEarcon(name, volume);
        }
      }
      catch (Exception ex)
      {
        Log.Info("Proactive", "earcon " + name + ": " + ex.Message);
      }
    }

    // Token: 0x06000C7E RID: 3198 RVA: 0x005625A4 File Offset: 0x005625A4
    private void UpdateGreetingWatcher()
    {
      if (this.accountKey != null)
      {
        this.ScheduleNextGreetingCheck(true);
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.greetingTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      DispatcherQueueTimer dispatcherQueueTimer2 = this.greetingArmTimer;
      if (dispatcherQueueTimer2 == null)
      {
        return;
      }
      dispatcherQueueTimer2.Stop();
    }

    // Token: 0x06000C7F RID: 3199 RVA: 0x005625D8 File Offset: 0x005625D8
    private void ScheduleNextGreetingCheck(bool evaluatingNow)
    {
      if (this.accountKey == null)
      {
        return;
      }
      if (evaluatingNow)
      {
        this.EvaluateGreeting();
      }
      if (this.greetingTimer == null)
      {
        this.greetingTimer = this.CreateGreetingTimer();
      }
      this.greetingTimer.Stop();
      TimeSpan timeSpan = MorningGreetingPolicy.UntilNextCheck(this.Now.LocalDateTime, this.HasGreetedToday);
      this.greetingTimer.Interval = ((timeSpan > TimeSpan.FromHours(6)) ? TimeSpan.FromHours(6) : timeSpan);
      this.greetingTimer.Start();
    }

    // Token: 0x06000C80 RID: 3200 RVA: 0x00562660 File Offset: 0x00562660
    private DispatcherQueueTimer CreateGreetingTimer()
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.ScheduleNextGreetingCheck(true);
      };
      return dispatcherQueueTimer;
    }

    // Token: 0x17000239 RID: 569
    // (get) Token: 0x06000C81 RID: 3201 RVA: 0x0056268C File Offset: 0x0056268C
    private bool HasGreetedToday
    {
      get
      {
        return this.store.Read<string>(([Nullable(1)] ProactiveStoreData d) => d.MorningGreetingLastDay) == MorningGreetingPolicy.DayKey(this.Now.LocalDateTime);
      }
    }

    // Token: 0x06000C82 RID: 3202 RVA: 0x005626DC File Offset: 0x005626DC
    private void EvaluateGreeting()
    {
      DateTime localDateTime = this.Now.LocalDateTime;
      if (!this.HasCompletedOnboarding || !this.CanAdvancePastPermissions || this.IsTutorialEngaged)
      {
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.greetingArmTimer;
      if ((dispatcherQueueTimer != null && dispatcherQueueTimer.IsRunning) || this.greetingFiring)
      {
        return;
      }
      if (!MorningGreetingPolicy.IsWithinWindow(localDateTime) || this.HasGreetedToday)
      {
        return;
      }
      double num = ProactiveCoordinator.IdleSeconds();
      if (num > MorningGreetingPolicy.ActiveRecency.TotalSeconds)
      {
        string text = "Proactive";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
        defaultInterpolatedStringHandler.AppendLiteral("greeting waits: no recent input (");
        defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0");
        defaultInterpolatedStringHandler.AppendLiteral("s)");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        return;
      }
      if (this.real.Companion.State != VoiceState.Idle)
      {
        Log.Info("Proactive", "greeting waits: voice busy");
        return;
      }
      this.EnsureMorningSweepStarted();
      string text2 = MorningGreetingPolicy.DayKey(localDateTime);
      if (this.greetingArmTimer == null)
      {
        this.greetingArmTimer = this.dq.CreateTimer();
      }
      this.greetingArmTimer.Stop();
      this.greetingArmTimer.IsRepeating = false;
      this.greetingArmTimer.Interval = MorningGreetingPolicy.ArmDelay;
      this.greetingArmTimer.Tick -= this.OnGreetingArmed;
      this.greetingArmTimer.Tick += this.OnGreetingArmed;
      this.armedGreetingDay = text2;
      this.greetingArmTimer.Start();
      Log.Info("Proactive", "greeting armed (10 s)");
    }

    // Token: 0x06000C83 RID: 3203 RVA: 0x00562854 File Offset: 0x00562854
    private void OnGreetingArmed(DispatcherQueueTimer sender, object args)
    {
      this.FireGreetingAsync();
    }

    // Token: 0x06000C84 RID: 3204 RVA: 0x00562860 File Offset: 0x00562860
    private async Task FireGreetingAsync()
    {
      this.greetingFiring = true;
      try
      {
        DateTime localDateTime = this.Now.LocalDateTime;
        if (MorningGreetingPolicy.IsWithinWindow(localDateTime) && !(MorningGreetingPolicy.DayKey(localDateTime) != this.armedGreetingDay) && !this.HasGreetedToday)
        {
          Func<QuietReason> func;
          if ((func = ProactiveCoordinator.<>O.<1>__Current) == null)
          {
            func = (ProactiveCoordinator.<>O.<1>__Current = new Func<QuietReason>(QuietContext.Current));
          }
          QuietReason quietReason = await Task.Run<QuietReason>(func);
          if (quietReason != QuietReason.None)
          {
            string text = "Proactive";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
            defaultInterpolatedStringHandler.AppendLiteral("greeting held: ");
            defaultInterpolatedStringHandler.AppendFormatted<QuietReason>(quietReason);
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          }
          else if (this.real.Companion.State != VoiceState.Idle)
          {
            Log.Info("Proactive", "greeting held: voice busy");
          }
          else
          {
            DateTimeOffset presentedAt = this.Now;
            this.store.Update(delegate(ProactiveStoreData d)
            {
              d.MorningGreetingLastDay = this.armedGreetingDay;
              d.MorningGreetingLastPresentedAt = new DateTimeOffset?(presentedAt);
            });
            string text2 = MorningGreetingPolicy.Pick(this.random);
            Log.Info("Proactive", "greeting: \"" + text2 + "\"");
            await this.host.SayAtCursorAsync(text2, true, MorningGreetingPolicy.PostStreamHold.TotalSeconds, default(CancellationToken));
            if (this.morningNotice != null && !this.morningNotice.IsDebugPreview)
            {
              MorningSuggestionsNotice morningSuggestionsNotice = this.morningNotice.<Clone>$();
              morningSuggestionsNotice.PresentNotBefore = presentedAt + MorningSuggestionsInvitationPolicy.DelayAfterGreeting;
              this.morningNotice = morningSuggestionsNotice;
              this.ScheduleReadiness(MorningSuggestionsInvitationPolicy.DelayAfterGreeting);
            }
          }
        }
      }
      finally
      {
        this.greetingFiring = false;
      }
    }

    // Token: 0x1700023A RID: 570
    // (get) Token: 0x06000C85 RID: 3205 RVA: 0x005628A4 File Offset: 0x005628A4
    public IReadOnlyList<AgentNotification> Notifications
    {
      get
      {
        return this.notifications;
      }
    }

    // Token: 0x1400002E RID: 46
    // (add) Token: 0x06000C86 RID: 3206 RVA: 0x005628AC File Offset: 0x005628AC
    // (remove) Token: 0x06000C87 RID: 3207 RVA: 0x005628E4 File Offset: 0x005628E4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action NotificationsChanged;

    // Token: 0x1700023B RID: 571
    // (get) Token: 0x06000C88 RID: 3208 RVA: 0x0056291C File Offset: 0x0056291C
    public bool IsRemoteTasksEnabled
    {
      get
      {
        return this.real.Core.Settings.Read<bool>((AppSettings s) => s.RemoteTasksEnabled);
      }
    }

    // Token: 0x1700023C RID: 572
    // (get) Token: 0x06000C89 RID: 3209 RVA: 0x00562954 File Offset: 0x00562954
    // (set) Token: 0x06000C8A RID: 3210 RVA: 0x0056295C File Offset: 0x0056295C
    [Nullable(2)]
    public string PendingRemoteTaskContext
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06000C8B RID: 3211 RVA: 0x00562968 File Offset: 0x00562968
    private void UpdateNotificationPolling()
    {
      if (this.accountKey == null || !this.IsRemoteTasksEnabled)
      {
        this.StopNotifications(true);
        return;
      }
      if (this.notificationsTimer == null)
      {
        this.notificationsTimer = this.dq.CreateTimer();
        this.notificationsTimer.Interval = AgentBackendClient.PollInterval;
        this.notificationsTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.PollNotificationsNow();
        };
      }
      if (!this.notificationsTimer.IsRunning)
      {
        this.notificationsTimer.Start();
        this.PollNotificationsNow();
      }
    }

    // Token: 0x06000C8C RID: 3212 RVA: 0x005629EC File Offset: 0x005629EC
    private void StopNotifications(bool clear)
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.notificationsTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      if (clear && this.notifications.Count > 0)
      {
        this.notifications = Array.Empty<AgentNotification>();
        Action notificationsChanged = this.NotificationsChanged;
        if (notificationsChanged == null)
        {
          return;
        }
        notificationsChanged();
      }
    }

    // Token: 0x06000C8D RID: 3213 RVA: 0x00562A2C File Offset: 0x00562A2C
    public void PollNotificationsNow()
    {
      if (this.accountKey == null || !this.IsRemoteTasksEnabled || this.notificationsPollInFlight)
      {
        return;
      }
      this.notificationsPollInFlight = true;
      Task.Run(async delegate
      {
        IReadOnlyList<AgentNotification> fetched = null;
        try
        {
          IReadOnlyList<AgentNotification> readOnlyList = await this.backend.FetchUnreadAsync(default(CancellationToken)).ConfigureAwait(false);
          fetched = readOnlyList;
        }
        catch (Exception ex)
        {
          Log.Info("Notifications", "poll failed: " + ex.Message);
        }
        this.dq.TryEnqueue(delegate
        {
          this.notificationsPollInFlight = false;
          if (fetched == null)
          {
            return;
          }
          bool flag;
          if (fetched.Count == this.notifications.Count)
          {
            flag = fetched.Zip<AgentNotification, AgentNotification>(this.notifications).Any<ValueTuple<AgentNotification, AgentNotification>>(([TupleElementNames(new string[] { "First", "Second" })] ValueTuple<AgentNotification, AgentNotification> p) => p.Item1.Id != p.Item2.Id);
          }
          else
          {
            flag = true;
          }
          this.notifications = fetched;
          if (flag)
          {
            string text = "Notifications";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
            defaultInterpolatedStringHandler.AppendFormatted<int>(fetched.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" unread notification(s)");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            Action notificationsChanged = this.NotificationsChanged;
            if (notificationsChanged == null)
            {
              return;
            }
            notificationsChanged();
          }
        });
      });
    }

    // Token: 0x06000C8E RID: 3214 RVA: 0x00562A60 File Offset: 0x00562A60
    public void MarkNotificationRead(AgentNotification n)
    {
      ProactiveCoordinator.<>c__DisplayClass315_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass315_0();
      CS$<>8__locals1.n = n;
      CS$<>8__locals1.<>4__this = this;
      this.notifications = this.notifications.Where<AgentNotification>((AgentNotification x) => x.Id != CS$<>8__locals1.n.Id).ToList<AgentNotification>();
      Action notificationsChanged = this.NotificationsChanged;
      if (notificationsChanged != null)
      {
        notificationsChanged();
      }
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass315_0.<<MarkNotificationRead>b__1>d <<MarkNotificationRead>b__1>d;
        <<MarkNotificationRead>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<MarkNotificationRead>b__1>d.<>4__this = CS$<>8__locals1;
        <<MarkNotificationRead>b__1>d.<>1__state = -1;
        <<MarkNotificationRead>b__1>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass315_0.<<MarkNotificationRead>b__1>d>(ref <<MarkNotificationRead>b__1>d);
        return <<MarkNotificationRead>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C8F RID: 3215 RVA: 0x00562AC8 File Offset: 0x00562AC8
    public void MarkAllNotificationsRead()
    {
      if (this.notifications.Count == 0)
      {
        return;
      }
      this.notifications = Array.Empty<AgentNotification>();
      Action notificationsChanged = this.NotificationsChanged;
      if (notificationsChanged != null)
      {
        notificationsChanged();
      }
      Task.Run(async delegate
      {
        try
        {
          await this.backend.MarkAllReadAsync(default(CancellationToken)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Info("Notifications", "read-all failed: " + ex.Message);
          this.dq.TryEnqueue(new DispatcherQueueHandler(this.PollNotificationsNow));
        }
      });
    }

    // Token: 0x06000C90 RID: 3216 RVA: 0x00562B08 File Offset: 0x00562B08
    private async Task<string> RemoteTaskContextAsync(AgentNotification n)
    {
      JsonObject jsonObject;
      if (n.SourceType == "cron")
      {
        string sourceId = n.SourceId;
        if (sourceId != null)
        {
          jsonObject = await this.backend.FetchCronDetailAsync(sourceId, default(CancellationToken)).ConfigureAwait(false);
          goto IL_00B9;
        }
      }
      jsonObject = null;
      IL_00B9:
      JsonObject jsonObject2 = jsonObject;
      return AgentBackendClient.BuildContext(n, jsonObject2);
    }

    // Token: 0x06000C91 RID: 3217 RVA: 0x00562B54 File Offset: 0x00562B54
    public void FixRemoteTask(AgentNotification n)
    {
      ProactiveCoordinator.<>c__DisplayClass318_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass318_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.n = n;
      if (!this.IsRemoteTasksEnabled)
      {
        return;
      }
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass318_0.<<FixRemoteTask>b__0>d <<FixRemoteTask>b__0>d;
        <<FixRemoteTask>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<FixRemoteTask>b__0>d.<>4__this = CS$<>8__locals1;
        <<FixRemoteTask>b__0>d.<>1__state = -1;
        <<FixRemoteTask>b__0>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass318_0.<<FixRemoteTask>b__0>d>(ref <<FixRemoteTask>b__0>d);
        return <<FixRemoteTask>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C92 RID: 3218 RVA: 0x00562B90 File Offset: 0x00562B90
    public void StartRemoteTaskTextModification(AgentNotification n)
    {
      ProactiveCoordinator.<>c__DisplayClass319_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass319_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.n = n;
      if (!this.IsRemoteTasksEnabled)
      {
        return;
      }
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass319_0.<<StartRemoteTaskTextModification>b__0>d <<StartRemoteTaskTextModification>b__0>d;
        <<StartRemoteTaskTextModification>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartRemoteTaskTextModification>b__0>d.<>4__this = CS$<>8__locals1;
        <<StartRemoteTaskTextModification>b__0>d.<>1__state = -1;
        <<StartRemoteTaskTextModification>b__0>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass319_0.<<StartRemoteTaskTextModification>b__0>d>(ref <<StartRemoteTaskTextModification>b__0>d);
        return <<StartRemoteTaskTextModification>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x1700023D RID: 573
    // (get) Token: 0x06000C93 RID: 3219 RVA: 0x00562BCC File Offset: 0x00562BCC
    public bool IsRemoteTaskVoiceActive
    {
      get
      {
        TapVoiceCapture tapVoiceCapture = this.remoteTaskVoice;
        return tapVoiceCapture != null && tapVoiceCapture.IsActive;
      }
    }

    // Token: 0x06000C94 RID: 3220 RVA: 0x00562BE0 File Offset: 0x00562BE0
    public void ToggleRemoteTaskVoiceModification(AgentNotification n)
    {
      ProactiveCoordinator.<>c__DisplayClass323_0 CS$<>8__locals1 = new ProactiveCoordinator.<>c__DisplayClass323_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.n = n;
      if (!this.IsRemoteTasksEnabled)
      {
        return;
      }
      if (this.remoteTaskVoice == null)
      {
        this.remoteTaskVoice = this.CreateRemoteTaskVoice();
      }
      if (this.remoteTaskVoice.IsActive)
      {
        this.remoteTaskVoice.Stop();
        return;
      }
      if (!this.remoteTaskVoice.Start(delegate(string words)
      {
        CS$<>8__locals1.<>4__this.dq.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.SubmitRemoteTaskModification(words);
        });
      }))
      {
        return;
      }
      Log.Info("Notifications", "modify \"" + CS$<>8__locals1.n.Title + "\" by voice: listening");
      this.PendingRemoteTaskContext = AgentBackendClient.BuildContext(CS$<>8__locals1.n, null);
      Task.Run(delegate
      {
        ProactiveCoordinator.<>c__DisplayClass323_0.<<ToggleRemoteTaskVoiceModification>b__1>d <<ToggleRemoteTaskVoiceModification>b__1>d;
        <<ToggleRemoteTaskVoiceModification>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ToggleRemoteTaskVoiceModification>b__1>d.<>4__this = CS$<>8__locals1;
        <<ToggleRemoteTaskVoiceModification>b__1>d.<>1__state = -1;
        <<ToggleRemoteTaskVoiceModification>b__1>d.<>t__builder.Start<ProactiveCoordinator.<>c__DisplayClass323_0.<<ToggleRemoteTaskVoiceModification>b__1>d>(ref <<ToggleRemoteTaskVoiceModification>b__1>d);
        return <<ToggleRemoteTaskVoiceModification>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000C95 RID: 3221 RVA: 0x00562C9C File Offset: 0x00562C9C
    private TapVoiceCapture CreateRemoteTaskVoice()
    {
      TapVoiceCapture tapVoiceCapture = new TapVoiceCapture(this.real);
      tapVoiceCapture.ActiveChanged += delegate(bool _)
      {
        this.dq.TryEnqueue(delegate
        {
          Action notificationsChanged = this.NotificationsChanged;
          if (notificationsChanged == null)
          {
            return;
          }
          notificationsChanged();
        });
      };
      return tapVoiceCapture;
    }

    // Token: 0x06000C96 RID: 3222 RVA: 0x00562CBC File Offset: 0x00562CBC
    public bool CancelRemoteTaskVoice()
    {
      TapVoiceCapture tapVoiceCapture = this.remoteTaskVoice;
      if (tapVoiceCapture == null || !tapVoiceCapture.Cancel())
      {
        return false;
      }
      this.PendingRemoteTaskContext = null;
      return true;
    }

    // Token: 0x06000C97 RID: 3223 RVA: 0x00562CE0 File Offset: 0x00562CE0
    public bool SubmitRemoteTaskModification(string words)
    {
      string pendingRemoteTaskContext = this.PendingRemoteTaskContext;
      if (pendingRemoteTaskContext == null || string.IsNullOrWhiteSpace(words))
      {
        return false;
      }
      this.PendingRemoteTaskContext = null;
      string text = AgentBackendClient.ComposePrompt(pendingRemoteTaskContext, words);
      this.real.Agents.LaunchAsync(new AgentLaunchRequest
      {
        Prompt = text,
        LaunchSource = "text",
        IsGeneratedTaskBrief = false
      }, default(CancellationToken)).ContinueWith(delegate(Task<string> t)
      {
        string text2 = "Notifications";
        string text3;
        if (!t.IsFaulted)
        {
          text3 = "modification launched as " + t.Result;
        }
        else
        {
          string text4 = "modification launch failed: ";
          AggregateException exception = t.Exception;
          text3 = text4 + ((exception != null) ? exception.GetBaseException().Message : null);
        }
        Log.Info(text2, text3);
      });
      return true;
    }

    // Token: 0x06000C98 RID: 3224 RVA: 0x00562D70 File Offset: 0x00562D70
    public void CancelRemoteTaskModification()
    {
      this.PendingRemoteTaskContext = null;
    }

    // Token: 0x06000CA9 RID: 3241 RVA: 0x00563010 File Offset: 0x00563010
    [CompilerGenerated]
    private async Task<IReadOnlyList<AgentIntegrationInfo>> <RefreshIntegrationsAsync>g__LoadAsync|185_0()
    {
      IReadOnlyList<AgentIntegrationInfo> list2;
      try
      {
        IReadOnlyList<AgentIntegrationInfo> readOnlyList = await this.backend.FetchIntegrationsAsync(default(CancellationToken)).ConfigureAwait(false);
        IReadOnlyList<AgentIntegrationInfo> list = readOnlyList;
        this.dq.TryEnqueue(delegate
        {
          this.catalog = list;
          this.catalogFetchedAt = DateTimeOffset.Now;
          ValueTuple<string, DateTimeOffset>? valueTuple = this.pendingSuggestionConnect;
          if (valueTuple != null)
          {
            ValueTuple<string, DateTimeOffset> pending = valueTuple.GetValueOrDefault();
            AgentIntegrationInfo agentIntegrationInfo = list.FirstOrDefault<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.PrimaryToolkitSlug.Equals(pending.Item1, StringComparison.OrdinalIgnoreCase));
            if (agentIntegrationInfo != null && agentIntegrationInfo.ConnectedCount > 0)
            {
              this.pendingSuggestionConnect = null;
              ClickyAnalytics.TrackIntegrationConnected(agentIntegrationInfo.Id, agentIntegrationInfo.DisplayName, agentIntegrationInfo.ToolkitSlugs, "integration_suggestion", agentIntegrationInfo.Status, new int?(agentIntegrationInfo.ConnectedCount), null);
              goto IL_0106;
            }
          }
          valueTuple = this.pendingSuggestionConnect;
          if (valueTuple != null)
          {
            ValueTuple<string, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
            if (DateTimeOffset.Now - valueOrDefault.Item2 > TimeSpan.FromMinutes(30L))
            {
              this.pendingSuggestionConnect = null;
            }
          }
          IL_0106:
          IEnumerable<string> enumerable = list.Where<AgentIntegrationInfo>((AgentIntegrationInfo i) => !i.IsLocal && i.IsEligibleForConnectionSuggestion).SelectMany<AgentIntegrationInfo, string>((AgentIntegrationInfo i) => i.ToolkitSlugs.Append(i.Id).Append(i.PrimaryToolkitSlug));
          this.integrationRules.SetAvailableToolkits(enumerable);
          NotchSurfaces.UseCasesFor = new Func<string, IReadOnlyList<string>>(this.UseCasesFor);
          string text = "Integrations";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 3);
          defaultInterpolatedStringHandler.AppendLiteral("catalog: ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" app(s), ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count<AgentIntegrationInfo>((AgentIntegrationInfo i) => i.IsActiveConnection));
          defaultInterpolatedStringHandler.AppendLiteral(" connected, ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.integrationRules.AvailableToolkitCount);
          defaultInterpolatedStringHandler.AppendLiteral(" promptable");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        });
        list2 = list;
      }
      catch (Exception ex)
      {
        Log.Info("Integrations", "catalog fetch failed: " + ex.Message);
        list2 = this.catalog;
      }
      return list2;
    }

    // Token: 0x0400044D RID: 1101
    private const string Area = "Proactive";

    // Token: 0x0400044E RID: 1102
    private static readonly TimeSpan SlotsPollInterval = TimeSpan.FromSeconds(30L);

    // Token: 0x0400044F RID: 1103
    private static readonly TimeSpan ActiveRecency = TimeSpan.FromSeconds(30L);

    // Token: 0x04000450 RID: 1104
    private static readonly TimeSpan JobPollInterval = TimeSpan.FromSeconds(4L);

    // Token: 0x04000451 RID: 1105
    private static readonly TimeSpan JobFastPollWindow = TimeSpan.FromMinutes(15L);

    // Token: 0x04000452 RID: 1106
    private static readonly TimeSpan JobSlowPollInterval = TimeSpan.FromSeconds(30L);

    // Token: 0x04000453 RID: 1107
    private static readonly TimeSpan JobReceiptLifetime = TimeSpan.FromHours(6);

    // Token: 0x04000454 RID: 1108
    private static readonly TimeSpan StatusSyncInterval = TimeSpan.FromSeconds(60L);

    // Token: 0x04000455 RID: 1109
    public const int DenyReasonMaximumCharacters = 500;

    // Token: 0x04000456 RID: 1110
    private readonly AppHost host;

    // Token: 0x04000457 RID: 1111
    private readonly RealStack real;

    // Token: 0x04000458 RID: 1112
    private readonly DispatcherQueue dq;

    // Token: 0x04000459 RID: 1113
    private readonly ProactiveStore store = new ProactiveStore(null);

    // Token: 0x0400045A RID: 1114
    private readonly ProactiveAgentsClient client;

    // Token: 0x0400045B RID: 1115
    private readonly AgentBackendClient backend;

    // Token: 0x0400045C RID: 1116
    private readonly DispatcherQueueTimer slotsTimer;

    // Token: 0x0400045D RID: 1117
    private readonly DispatcherQueueTimer expiryTimer;

    // Token: 0x0400045E RID: 1118
    private readonly Dictionary<string, Guid> launchedTasks = new Dictionary<string, Guid>();

    // Token: 0x0400045F RID: 1119
    private readonly Random random = new Random();

    // Token: 0x04000460 RID: 1120
    private readonly TimeSpan clockOffset;

    // Token: 0x04000461 RID: 1121
    private List<ProactiveSuggestionCard> cards = new List<ProactiveSuggestionCard>();

    // Token: 0x04000462 RID: 1122
    private List<QueuedProactiveTask> queue = new List<QueuedProactiveTask>();

    // Token: 0x04000463 RID: 1123
    [Nullable(2)]
    private string accountKey;

    // Token: 0x04000464 RID: 1124
    [Nullable(2)]
    private CancellationTokenSource jobCts;

    // Token: 0x04000465 RID: 1125
    private Guid? jobRunId;

    // Token: 0x04000466 RID: 1126
    private DateTimeOffset? statusSyncLastRunAt;

    // Token: 0x04000467 RID: 1127
    private bool statusSyncInFlight;

    // Token: 0x04000468 RID: 1128
    private bool wasOnboardingCompleted;

    // Token: 0x04000469 RID: 1129
    private bool started;

    // Token: 0x0400046F RID: 1135
    private int adjustmentAnswerCount;

    // Token: 0x04000470 RID: 1136
    private Guid? currentCardId;

    // Token: 0x04000472 RID: 1138
    private const string OnboardingFirstAskIntegrationNote = "This ask was written during onboarding to show what you can do, so it may reach for an app (Gmail, Notion, Google Calendar, Sheets, Linear, GitHub) the user has not connected. Assume NOTHING is connected unless your own tool list says otherwise. Never stall on that: do the strongest app-free version now with web research and files you make (the people in a CSV, the emails drafted right here in the chat, the plan as a doc), and then, plainly and eagerly, tell the user which one app to connect to finish the job and exactly what you would do the moment they do (\"Connect Gmail and I'll send these as drafts to your inbox\"). One sentence, at the end, no nagging.";

    // Token: 0x04000473 RID: 1139
    private const string OnboardingFirstJobRunsOnItsOwnNote = "This is one of their very first jobs, picked during onboarding so it needs nothing from them: finish it start to finish without asking anything (this overrides any ask-first guidance above). Where something is open, make the sensible choice from the brief and the web and say in one line what you assumed; never stop to ask and never end on a question.";

    // Token: 0x04000474 RID: 1140
    [Nullable(2)]
    private volatile string liveNoteSnapshot;

    // Token: 0x04000475 RID: 1141
    private static readonly TimeSpan PostOnboardingHeadsUpQuiet = TimeSpan.FromMinutes(5L);

    // Token: 0x04000476 RID: 1142
    private static readonly TimeSpan IntegrationSuggestionAutoHide = TimeSpan.FromSeconds(18L);

    // Token: 0x04000477 RID: 1143
    private static readonly TimeSpan CatalogRefreshInterval = TimeSpan.FromMinutes(30L);

    // Token: 0x04000478 RID: 1144
    private readonly IntegrationSuggestionRules integrationRules = new IntegrationSuggestionRules();

    // Token: 0x04000479 RID: 1145
    private IReadOnlyList<AgentIntegrationInfo> catalog = Array.Empty<AgentIntegrationInfo>();

    // Token: 0x0400047A RID: 1146
    private DateTimeOffset catalogFetchedAt;

    // Token: 0x0400047B RID: 1147
    [Nullable(new byte[] { 2, 1, 1 })]
    private Task<IReadOnlyList<AgentIntegrationInfo>> catalogLoad;

    // Token: 0x0400047C RID: 1148
    [Nullable(2)]
    private DispatcherQueueTimer integrationTimer;

    // Token: 0x0400047D RID: 1149
    private bool integrationSampleInFlight;

    // Token: 0x0400047E RID: 1150
    [Nullable(2)]
    private IntegrationCandidate activeIntegrationSuggestion;

    // Token: 0x0400047F RID: 1151
    private bool spotifyTipActive;

    // Token: 0x04000480 RID: 1152
    [Nullable(2)]
    private string lastSuppression;

    // Token: 0x04000481 RID: 1153
    [TupleElementNames(new string[] { "Slug", "At" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, DateTimeOffset>? pendingSuggestionConnect;

    // Token: 0x04000482 RID: 1154
    [Nullable(2)]
    private ProactiveCoordinator.PendingConnection pendingConnection;

    // Token: 0x04000483 RID: 1155
    private static readonly TimeSpan PendingConnectionLifetime = TimeSpan.FromMinutes(5L);

    // Token: 0x04000484 RID: 1156
    private readonly UpcomingMeetingsClient meetingsClient;

    // Token: 0x04000485 RID: 1157
    private readonly HashSet<string> announcedMeetings = new HashSet<string>();

    // Token: 0x04000486 RID: 1158
    [Nullable(2)]
    private DispatcherQueueTimer meetingsTimer;

    // Token: 0x04000487 RID: 1159
    private IReadOnlyList<UpcomingCalendarEvent> upcomingEvents = Array.Empty<UpcomingCalendarEvent>();

    // Token: 0x04000488 RID: 1160
    [Nullable(2)]
    private UpcomingCalendarEvent activeMeeting;

    // Token: 0x04000489 RID: 1161
    private DateTimeOffset? meetingsRefreshedAt;

    // Token: 0x0400048A RID: 1162
    private DateTimeOffset? meetingsFailedAt;

    // Token: 0x0400048B RID: 1163
    private bool meetingsBackendConnected;

    // Token: 0x0400048C RID: 1164
    private bool meetingsRefreshInFlight;

    // Token: 0x0400048D RID: 1165
    private int meetingsAuthGeneration;

    // Token: 0x0400048E RID: 1166
    private static readonly TimeSpan InvitationRecheck = TimeSpan.FromSeconds(30L);

    // Token: 0x0400048F RID: 1167
    [Nullable(2)]
    private MorningSuggestionsNotice morningNotice;

    // Token: 0x04000490 RID: 1168
    private Guid? claimedNoticeId;

    // Token: 0x04000491 RID: 1169
    private Guid? chimedNoticeId;

    // Token: 0x04000492 RID: 1170
    private DateTimeOffset? morningSweepStartedAt;

    // Token: 0x04000493 RID: 1171
    [Nullable(2)]
    private DispatcherQueueTimer readinessTimer;

    // Token: 0x04000494 RID: 1172
    private bool claimInFlight;

    // Token: 0x04000495 RID: 1173
    private readonly HashSet<string> heldBackReasons = new HashSet<string>();

    // Token: 0x04000496 RID: 1174
    public const bool MorningSuggestionsInvitationHasASurface = false;

    // Token: 0x04000497 RID: 1175
    public const bool IsMorningInvitationPaused = true;

    // Token: 0x04000498 RID: 1176
    public static readonly bool MorningInvitationHasASurface = false;

    // Token: 0x04000499 RID: 1177
    [Nullable(2)]
    private DispatcherQueueTimer greetingTimer;

    // Token: 0x0400049A RID: 1178
    [Nullable(2)]
    private DispatcherQueueTimer greetingArmTimer;

    // Token: 0x0400049B RID: 1179
    private bool greetingFiring;

    // Token: 0x0400049C RID: 1180
    [Nullable(2)]
    private string armedGreetingDay;

    // Token: 0x0400049D RID: 1181
    [Nullable(2)]
    private DispatcherQueueTimer notificationsTimer;

    // Token: 0x0400049E RID: 1182
    private bool notificationsPollInFlight;

    // Token: 0x0400049F RID: 1183
    private IReadOnlyList<AgentNotification> notifications = Array.Empty<AgentNotification>();

    // Token: 0x040004A2 RID: 1186
    [Nullable(2)]
    private TapVoiceCapture remoteTaskVoice;

    // Token: 0x02000571 RID: 1393
    [Nullable(0)]
    public sealed class ApprovalResult : IEquatable<ProactiveCoordinator.ApprovalResult>
    {
      // Token: 0x06002E44 RID: 11844 RVA: 0x0065FD3C File Offset: 0x0065FD3C
      public ApprovalResult(string ClickySlug, string ClickyName, ProactiveQueueStatus Status)
      {
        this.ClickySlug = ClickySlug;
        this.ClickyName = ClickyName;
        this.Status = Status;
        base..ctor();
      }

      // Token: 0x17000745 RID: 1861
      // (get) Token: 0x06002E45 RID: 11845 RVA: 0x0065FD5C File Offset: 0x0065FD5C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ProactiveCoordinator.ApprovalResult);
        }
      }

      // Token: 0x17000746 RID: 1862
      // (get) Token: 0x06002E46 RID: 11846 RVA: 0x0065FD68 File Offset: 0x0065FD68
      // (set) Token: 0x06002E47 RID: 11847 RVA: 0x0065FD70 File Offset: 0x0065FD70
      public string ClickySlug { get; set; }

      // Token: 0x17000747 RID: 1863
      // (get) Token: 0x06002E48 RID: 11848 RVA: 0x0065FD7C File Offset: 0x0065FD7C
      // (set) Token: 0x06002E49 RID: 11849 RVA: 0x0065FD84 File Offset: 0x0065FD84
      public string ClickyName { get; set; }

      // Token: 0x17000748 RID: 1864
      // (get) Token: 0x06002E4A RID: 11850 RVA: 0x0065FD90 File Offset: 0x0065FD90
      // (set) Token: 0x06002E4B RID: 11851 RVA: 0x0065FD98 File Offset: 0x0065FD98
      public ProactiveQueueStatus Status { get; set; }

      // Token: 0x06002E4C RID: 11852 RVA: 0x0065FDA4 File Offset: 0x0065FDA4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ApprovalResult");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002E4D RID: 11853 RVA: 0x0065FDF0 File Offset: 0x0065FDF0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("ClickySlug = ");
        builder.Append(this.ClickySlug);
        builder.Append(", ClickyName = ");
        builder.Append(this.ClickyName);
        builder.Append(", Status = ");
        builder.Append(this.Status.ToString());
        return true;
      }

      // Token: 0x06002E4E RID: 11854 RVA: 0x0065FE5C File Offset: 0x0065FE5C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ProactiveCoordinator.ApprovalResult left, ProactiveCoordinator.ApprovalResult right)
      {
        return !(left == right);
      }

      // Token: 0x06002E4F RID: 11855 RVA: 0x0065FE68 File Offset: 0x0065FE68
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ProactiveCoordinator.ApprovalResult left, ProactiveCoordinator.ApprovalResult right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002E50 RID: 11856 RVA: 0x0065FE7C File Offset: 0x0065FE7C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickyName>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveQueueStatus>.Default.GetHashCode(this.<Status>k__BackingField);
      }

      // Token: 0x06002E51 RID: 11857 RVA: 0x0065FEE0 File Offset: 0x0065FEE0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ProactiveCoordinator.ApprovalResult);
      }

      // Token: 0x06002E52 RID: 11858 RVA: 0x0065FEF0 File Offset: 0x0065FEF0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ProactiveCoordinator.ApprovalResult other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickyName>k__BackingField, other.<ClickyName>k__BackingField) && EqualityComparer<ProactiveQueueStatus>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField));
      }

      // Token: 0x06002E54 RID: 11860 RVA: 0x0065FF6C File Offset: 0x0065FF6C
      [CompilerGenerated]
      private ApprovalResult(ProactiveCoordinator.ApprovalResult original)
      {
        this.ClickySlug = original.<ClickySlug>k__BackingField;
        this.ClickyName = original.<ClickyName>k__BackingField;
        this.Status = original.<Status>k__BackingField;
      }

      // Token: 0x06002E55 RID: 11861 RVA: 0x0065FF98 File Offset: 0x0065FF98
      [CompilerGenerated]
      public void Deconstruct(out string ClickySlug, out string ClickyName, out ProactiveQueueStatus Status)
      {
        ClickySlug = this.ClickySlug;
        ClickyName = this.ClickyName;
        Status = this.Status;
      }
    }

    // Token: 0x02000572 RID: 1394
    [Nullable(0)]
    private sealed class PendingConnection : IEquatable<ProactiveCoordinator.PendingConnection>
    {
      // Token: 0x06002E56 RID: 11862 RVA: 0x0065FFB8 File Offset: 0x0065FFB8
      public PendingConnection(string Slug, int Baseline, DateTimeOffset Started)
      {
        this.Slug = Slug;
        this.Baseline = Baseline;
        this.Started = Started;
        base..ctor();
      }

      // Token: 0x17000749 RID: 1865
      // (get) Token: 0x06002E57 RID: 11863 RVA: 0x0065FFD8 File Offset: 0x0065FFD8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ProactiveCoordinator.PendingConnection);
        }
      }

      // Token: 0x1700074A RID: 1866
      // (get) Token: 0x06002E58 RID: 11864 RVA: 0x0065FFE4 File Offset: 0x0065FFE4
      // (set) Token: 0x06002E59 RID: 11865 RVA: 0x0065FFEC File Offset: 0x0065FFEC
      public string Slug { get; set; }

      // Token: 0x1700074B RID: 1867
      // (get) Token: 0x06002E5A RID: 11866 RVA: 0x0065FFF8 File Offset: 0x0065FFF8
      // (set) Token: 0x06002E5B RID: 11867 RVA: 0x00660000 File Offset: 0x00660000
      public int Baseline { get; set; }

      // Token: 0x1700074C RID: 1868
      // (get) Token: 0x06002E5C RID: 11868 RVA: 0x0066000C File Offset: 0x0066000C
      // (set) Token: 0x06002E5D RID: 11869 RVA: 0x00660014 File Offset: 0x00660014
      public DateTimeOffset Started { get; set; }

      // Token: 0x06002E5E RID: 11870 RVA: 0x00660020 File Offset: 0x00660020
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PendingConnection");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002E5F RID: 11871 RVA: 0x0066006C File Offset: 0x0066006C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", Baseline = ");
        builder.Append(this.Baseline.ToString());
        builder.Append(", Started = ");
        builder.Append(this.Started.ToString());
        return true;
      }

      // Token: 0x06002E60 RID: 11872 RVA: 0x006600E8 File Offset: 0x006600E8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ProactiveCoordinator.PendingConnection left, ProactiveCoordinator.PendingConnection right)
      {
        return !(left == right);
      }

      // Token: 0x06002E61 RID: 11873 RVA: 0x006600F4 File Offset: 0x006600F4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ProactiveCoordinator.PendingConnection left, ProactiveCoordinator.PendingConnection right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002E62 RID: 11874 RVA: 0x00660108 File Offset: 0x00660108
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Baseline>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<Started>k__BackingField);
      }

      // Token: 0x06002E63 RID: 11875 RVA: 0x0066016C File Offset: 0x0066016C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ProactiveCoordinator.PendingConnection);
      }

      // Token: 0x06002E64 RID: 11876 RVA: 0x0066017C File Offset: 0x0066017C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ProactiveCoordinator.PendingConnection other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Baseline>k__BackingField, other.<Baseline>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<Started>k__BackingField, other.<Started>k__BackingField));
      }

      // Token: 0x06002E66 RID: 11878 RVA: 0x006601F8 File Offset: 0x006601F8
      [CompilerGenerated]
      private PendingConnection(ProactiveCoordinator.PendingConnection original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.Baseline = original.<Baseline>k__BackingField;
        this.Started = original.<Started>k__BackingField;
      }

      // Token: 0x06002E67 RID: 11879 RVA: 0x00660224 File Offset: 0x00660224
      [CompilerGenerated]
      public void Deconstruct(out string Slug, out int Baseline, out DateTimeOffset Started)
      {
        Slug = this.Slug;
        Baseline = this.Baseline;
        Started = this.Started;
      }
    }

    // Token: 0x02000577 RID: 1399
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040019B7 RID: 6583
      [Nullable(0)]
      public static Func<StarterTaskCard, ProactiveSuggestionCard> <0>__FromStarter;

      // Token: 0x040019B8 RID: 6584
      [Nullable(0)]
      public static Func<QuietReason> <1>__Current;
    }
  }
}
