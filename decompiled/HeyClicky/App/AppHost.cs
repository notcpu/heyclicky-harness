using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Harness;
using HeyClicky.Agents.Jobs;
using HeyClicky.Agents.Runtime;
using HeyClicky.Agents.Turns;
using HeyClicky.App.Billing;
using HeyClicky.App.Browser;
using HeyClicky.App.Demo;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Documents;
using HeyClicky.App.Drive;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.Notch.Window;
using HeyClicky.App.Onboarding;
using HeyClicky.App.OneBrain;
using HeyClicky.App.Portraits;
using HeyClicky.App.Proactive;
using HeyClicky.App.Services;
using HeyClicky.App.Services.Updates;
using HeyClicky.App.Settings;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Browser;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Dictation;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Ink;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using HeyClicky.Drawing;
using HeyClicky.Drawing3D;
using HeyClicky.Platform.Audio;
using HeyClicky.Platform.Input;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualBasic.FileIO;
using Windows.Graphics;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;

namespace HeyClicky.App
{
  // Token: 0x02000077 RID: 119
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppHost : INotchCardActions, IAppActions, INotchActions
  {
    // Token: 0x17000062 RID: 98
    // (get) Token: 0x060002CB RID: 715 RVA: 0x0051BA80 File Offset: 0x0051BA80
    // (set) Token: 0x060002CC RID: 716 RVA: 0x0051BA88 File Offset: 0x0051BA88
    [Nullable(2)]
    public static AgentTurnCostLedger AgentCostLedger
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x060002CD RID: 717 RVA: 0x0051BA90 File Offset: 0x0051BA90
    private void WireAgentCostLedger(RealStack real)
    {
    }

    // Token: 0x060002CE RID: 718 RVA: 0x0051BA94 File Offset: 0x0051BA94
    private void RefreshAgentCostLane(RealStack real)
    {
      AppHost.<>c__DisplayClass6_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass6_0();
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.ledger = AppHost.AgentCostLedger;
      if (CS$<>8__locals1.ledger == null || CS$<>8__locals1.ledger.HasReadCurrentLane || !CS$<>8__locals1.real.Core.Auth.IsSignedIn)
      {
        return;
      }
      if (Interlocked.Exchange(ref this.agentCostLaneReadInFlight, 1) == 1)
      {
        return;
      }
      Task.Run(delegate
      {
        AppHost.<>c__DisplayClass6_0.<<RefreshAgentCostLane>b__0>d <<RefreshAgentCostLane>b__0>d;
        <<RefreshAgentCostLane>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<RefreshAgentCostLane>b__0>d.<>4__this = CS$<>8__locals1;
        <<RefreshAgentCostLane>b__0>d.<>1__state = -1;
        <<RefreshAgentCostLane>b__0>d.<>t__builder.Start<AppHost.<>c__DisplayClass6_0.<<RefreshAgentCostLane>b__0>d>(ref <<RefreshAgentCostLane>b__0>d);
        return <<RefreshAgentCostLane>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x060002CF RID: 719 RVA: 0x0051BB10 File Offset: 0x0051BB10
    private void StartAmbient()
    {
      this.Services.Agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncAgentActivity));
      };
      this.ambientTimer = this.DispatcherQueue.CreateTimer();
      this.ambientTimer.Interval = TimeSpan.FromMilliseconds(500L, 0L);
      this.ambientTimer.IsRepeating = true;
      this.ambientTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.AmbientTick();
      };
      this.ambientTimer.Start();
      FocusWatch.Start();
      FocusWatch.ForegroundChanged += this.OnForegroundChanged;
      this.SyncAgentActivity();
    }

    // Token: 0x060002D0 RID: 720 RVA: 0x0051BBAC File Offset: 0x0051BBAC
    private void SyncAgentActivity()
    {
      List<AgentTaskSnapshot> list = this.Services.Agents.Tasks.Where<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
      {
        AgentTaskStatus status = t.Status;
        return status <= AgentTaskStatus.Running;
      }).ToList<AgentTaskSnapshot>();
      string text;
      if (list.Count != 0)
      {
        text = string.Join("|", list.Select<AgentTaskSnapshot, string>((AgentTaskSnapshot t) => t.TaskId + ":" + t.Title));
      }
      else
      {
        text = "";
      }
      string text2 = text;
      if (text2 == this.lastAgentSignature)
      {
        return;
      }
      this.lastAgentSignature = text2;
      if (list.Count == 0)
      {
        this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AgentMultiActivity));
        return;
      }
      string text3 = (from t in list
        select AppHost.<SyncAgentActivity>g__Clamp|11_4(t.Title) into t
        orderby t.Length descending
        select t).First<string>();
      this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.AgentMultiActivity(text3, list.Count), null);
    }

    // Token: 0x060002D1 RID: 721 RVA: 0x0051BCE0 File Offset: 0x0051BCE0
    private void AmbientTick()
    {
      if (NotchView.IsAmbient(this.Services.Presenter.Presentation) && !this.NotchUi.IsHovering)
      {
        this.NotchUi.FollowCursorDisplay();
      }
      bool flag = QuietContext.IsFullscreenOn(this.NotchUi.MonitorHandle);
      if (flag != this.lastFullscreenOnNotch)
      {
        this.lastFullscreenOnNotch = flag;
        NotchPresenter corePresenter = this.CorePresenter;
        if (corePresenter != null)
        {
          corePresenter.SetAmbientSuppressed(flag);
        }
        this.NotchUi.SetAmbientHidden(flag);
        IntPtr foregroundWindow = Native.GetForegroundWindow();
        string text = "notch";
        string text2;
        if (!flag)
        {
          text2 = "fullscreen ended: ambient notch states back";
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
          defaultInterpolatedStringHandler.AppendLiteral("fullscreen app: ambient notch states hidden (");
          defaultInterpolatedStringHandler.AppendFormatted((foregroundWindow == 0) ? "nothing in front" : FullscreenGuard.Describe(foregroundWindow));
          defaultInterpolatedStringHandler.AppendLiteral(", ");
          defaultInterpolatedStringHandler.AppendFormatted(FullscreenGuard.NotificationState());
          defaultInterpolatedStringHandler.AppendLiteral(")");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        Log.Info(text, text2);
      }
      FullscreenGuard.GameWindow();
    }

    // Token: 0x060002D2 RID: 722 RVA: 0x0051BDD8 File Offset: 0x0051BDD8
    private void StartArtifactAutoOpen(RealStack real)
    {
      real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        if (e.Kind == AgentTaskEventKind.Finished)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.AutoOpenPrimaryArtifact(real, e.TaskId);
          });
        }
      };
      DispatcherQueueHandler <>9__3;
      real.Tutorial.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs e)
      {
        if (e.PropertyName == "IsEngaged")
        {
          DispatcherQueue dispatcherQueue = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__3) == null)
          {
            dispatcherQueueHandler = (<>9__3 = delegate
            {
              if (!real.Tutorial.IsEngaged)
              {
                this.DrainHeldArtifactOpens(real);
              }
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        }
      };
    }

    // Token: 0x060002D3 RID: 723 RVA: 0x0051BE34 File Offset: 0x0051BE34
    private void AutoOpenPrimaryArtifact(RealStack real, string taskId)
    {
      try
      {
        AgentTaskDetail taskDetail = real.Agents.GetTaskDetail(taskId);
        if (taskDetail != null)
        {
          if (taskDetail.JobId == null)
          {
            HashSet<string> hashSet = this.autoOpenedTurns;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
            defaultInterpolatedStringHandler.AppendFormatted(taskId);
            defaultInterpolatedStringHandler.AppendLiteral("|");
            defaultInterpolatedStringHandler.AppendFormatted<int>(taskDetail.UserPrompts.Count);
            defaultInterpolatedStringHandler.AppendLiteral("|");
            defaultInterpolatedStringHandler.AppendFormatted(taskDetail.TurnId);
            if (hashSet.Add(defaultInterpolatedStringHandler.ToStringAndClear()))
            {
              string text = taskDetail.Artifacts.FirstOrDefault<string>();
              string text2 = ((text != null) ? text.Trim() : null);
              if (!string.IsNullOrEmpty(text2))
              {
                string clickySlug = taskDetail.ClickySlug;
                string text3 = ((clickySlug != null) ? (real.Agents.WorkspacePath(clickySlug) ?? taskDetail.WorkspacePath) : taskDetail.WorkspacePath);
                if (taskDetail.IsDemo || this.onboardingDemoTaskIds.ContainsKey(taskId))
                {
                  Log.Info("agents", "artifact auto_open skipped for demo task " + taskId + ": " + text2);
                }
                else if (real.Tutorial.IsEngaged)
                {
                  this.heldArtifactOpens.Add(new ValueTuple<string, string, string>(taskId, text2, text3));
                  Log.Info("agents", "artifact auto_open held for the tour: " + text2);
                }
                else
                {
                  bool flag = this.OpenAgentArtifact(taskId, text2, text3);
                  string text4 = "agents";
                  defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 3);
                  defaultInterpolatedStringHandler.AppendLiteral("artifact auto_open opened=");
                  defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
                  defaultInterpolatedStringHandler.AppendLiteral(" task=");
                  defaultInterpolatedStringHandler.AppendFormatted(taskId);
                  defaultInterpolatedStringHandler.AppendLiteral(" artifact=");
                  defaultInterpolatedStringHandler.AppendFormatted(text2);
                  Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("agents", "artifact auto_open failed: " + ex.Message);
      }
    }

    // Token: 0x060002D4 RID: 724 RVA: 0x0051C024 File Offset: 0x0051C024
    private bool OpenAgentArtifact(string taskId, string artifact, [Nullable(2)] string workspace)
    {
      string text = ArtifactPaths.Resolve(artifact, workspace);
      if (ArtifactPaths.IsWebLink(text))
      {
        ChatBlockViews.OpenPath(text);
        this.FoldNotchForOpenedLink();
        return true;
      }
      if (!File.Exists(text) && !Directory.Exists(text))
      {
        return false;
      }
      string text2 = text;
      RealStack real = this.Real;
      string text3;
      if (real == null)
      {
        text3 = null;
      }
      else
      {
        AgentTaskDetail taskDetail = real.Agents.GetTaskDetail(taskId);
        text3 = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null);
      }
      if (this.OpenDocumentInNotch(text2, text3))
      {
        return true;
      }
      ChatBlockViews.OpenPath(text);
      return true;
    }

    // Token: 0x060002D5 RID: 725 RVA: 0x0051C09C File Offset: 0x0051C09C
    private void DrainHeldArtifactOpens(RealStack real)
    {
      if (this.heldArtifactOpens.Count == 0 || this.heldArtifactDrain != null)
      {
        return;
      }
      this.heldArtifactDrain = this.DispatcherQueue.CreateTimer();
      this.heldArtifactDrain.Interval = TimeSpan.FromSeconds(10L);
      this.heldArtifactDrain.Tick += delegate(DispatcherQueueTimer timer, object _)
      {
        if (real.Tutorial.IsEngaged || this.heldArtifactOpens.Count == 0)
        {
          timer.Stop();
          this.heldArtifactDrain = null;
          return;
        }
        ValueTuple<string, string, string> valueTuple = this.heldArtifactOpens[0];
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string item3 = valueTuple.Item3;
        this.heldArtifactOpens.RemoveAt(0);
        bool flag = this.OpenAgentArtifact(item, item2, item3);
        string text = "agents";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
        defaultInterpolatedStringHandler.AppendLiteral("artifact held_open opened=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        defaultInterpolatedStringHandler.AppendLiteral(" artifact=");
        defaultInterpolatedStringHandler.AppendFormatted(item2);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        if (this.heldArtifactOpens.Count == 0)
        {
          timer.Stop();
          this.heldArtifactDrain = null;
        }
      };
      this.heldArtifactDrain.Start();
    }

    // Token: 0x17000063 RID: 99
    // (get) Token: 0x060002D6 RID: 726 RVA: 0x0051C11C File Offset: 0x0051C11C
    [Nullable(2)]
    public BillingUi Billing
    {
      [NullableContext(2)]
      get
      {
        return this.billing;
      }
    }

    // Token: 0x060002D7 RID: 727 RVA: 0x0051C124 File Offset: 0x0051C124
    public bool ShowPaywall(PaywallSource source)
    {
      BillingUi billingUi = this.billing;
      return billingUi != null && billingUi.ShowPaywall(source);
    }

    // Token: 0x060002D8 RID: 728 RVA: 0x0051C138 File Offset: 0x0051C138
    public Task<bool> ShowOnboardingPaywallAsync()
    {
      BillingUi billingUi = this.billing;
      return ((billingUi != null) ? billingUi.ShowOnboardingPaywallAsync() : null) ?? Task.FromResult<bool>(false);
    }

    // Token: 0x060002D9 RID: 729 RVA: 0x0051C158 File Offset: 0x0051C158
    private void StartBilling(RealStack real)
    {
      BillingUi billingUi = new BillingUi(real.Core.Billing, real.Core.Worker, this.DispatcherQueue, new Action<string>(this.OpenUrl), new Action(this.TuckAwayForPaywall), this.ExcludeFromCapture)
      {
        CanSayLine = () => this.Services.Settings.HasCompletedOnboarding && !real.Tutorial.IsEngaged && real.Companion.State == VoiceState.Idle
      };
      billingUi.HoldShortcutDescription = () => this.Services.Settings.PushToTalkDisplay.ToLowerInvariant();
      BillingUi billingUi2 = billingUi;
      billingUi2.CursorLine += this.SayBillingLine;
      this.billing = billingUi2;
      this.Services.Billing = billingUi2;
      real.Agents.PaywallReached += delegate([Nullable(2)] object _, AgentPaywallEventArgs e)
      {
        BillingPaywallDetector.HandleIfPaywall(e.StatusCode, Encoding.UTF8.GetBytes(e.Body), e.Presents);
      };
      real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        AgentTaskEventKind kind = e.Kind;
        bool flag = kind == AgentTaskEventKind.Finished || kind == AgentTaskEventKind.Stopped;
        if (flag)
        {
          _ = real.Core.Billing.RefreshAsync();
        }
      };
      real.Core.Billing.RefreshAsync();
    }

    // Token: 0x060002DA RID: 730 RVA: 0x0051C270 File Offset: 0x0051C270
    private void TuckAwayForPaywall()
    {
      NotchPresentation presentation = this.Services.Presenter.Presentation;
      bool flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
      if (flag)
      {
        this.Services.Presenter.Present(new NotchPresentation.Idle());
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      corePresenter.DismissNotchAgentPeek();
    }

    // Token: 0x060002DB RID: 731 RVA: 0x0051C2CC File Offset: 0x0051C2CC
    private void SayBillingLine(string line)
    {
      this.SayAtCursorAsync(line, true, 5.0, default(CancellationToken));
    }

    // Token: 0x17000064 RID: 100
    // (get) Token: 0x060002DC RID: 732 RVA: 0x0051C2F4 File Offset: 0x0051C2F4
    // (set) Token: 0x060002DD RID: 733 RVA: 0x0051C2FC File Offset: 0x0051C2FC
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> PresentTaskNeedsYouHook
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000065 RID: 101
    // (get) Token: 0x060002DE RID: 734 RVA: 0x0051C308 File Offset: 0x0051C308
    // (set) Token: 0x060002DF RID: 735 RVA: 0x0051C310 File Offset: 0x0051C310
    [Nullable(2)]
    public Action DismissNotchUpdateHook
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000066 RID: 102
    // (get) Token: 0x060002E0 RID: 736 RVA: 0x0051C31C File Offset: 0x0051C31C
    // (set) Token: 0x060002E1 RID: 737 RVA: 0x0051C324 File Offset: 0x0051C324
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<string, string> NoteClickyEventForOneBrain
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x060002E2 RID: 738 RVA: 0x0051C330 File Offset: 0x0051C330
    private void StartBrowserConnection(RealStack real)
    {
      AppHost.<>c__DisplayClass41_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass41_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.bridge = ClickyBrowserBridge.Shared;
      CS$<>8__locals1.flow = ChromeSetupFlow.Shared;
      CS$<>8__locals1.agents = CS$<>8__locals1.real.Agents;
      if (this.NoteClickyEventForOneBrain == null)
      {
        this.NoteClickyEventForOneBrain = delegate(string threadId, string eventText)
        {
          CS$<>8__locals1.<>4__this.NoteJobEventForOneBrain(CS$<>8__locals1.real, threadId, eventText);
        };
      }
      CS$<>8__locals1.bridge.CursorColorHex = () => CS$<>8__locals1.<>4__this.Services.Settings.CursorColorHex;
      ClickyBrowserBridge clickyBrowserBridge = CS$<>8__locals1.bridge;
      Func<string, string, Task<bool>> func;
      if ((func = AppHost.<>O.<0>__Open) == null)
      {
        func = (AppHost.<>O.<0>__Open = new Func<string, string, Task<bool>>(ChromeInternalPages.Open));
      }
      clickyBrowserBridge.OpenInBrowserExecutable = func;
      CS$<>8__locals1.bridge.ScreenDriveIsAvailable = delegate
      {
        IChromeClickyDrive drive = CS$<>8__locals1.flow.Drive;
        return drive != null && drive.IsAvailable;
      };
      CS$<>8__locals1.flow.Attach(this);
      CS$<>8__locals1.agents.ChromeAnswerMeaningsForBrain = () => CS$<>8__locals1.flow.AnswerMeaningsForBrain;
      CS$<>8__locals1.bridge.ThreadSkipsChrome = new Func<string, bool>(CS$<>8__locals1.flow.SkipsChrome);
      CS$<>8__locals1.bridge.UploadApprovalHandler = delegate(IReadOnlyList<string> names, [Nullable(2)] string site, string title)
      {
        if (CS$<>8__locals1.real.Core.Settings.Current.AgentAlwaysApproveFileUploads)
        {
          return Task.FromResult<UploadApprovalAnswer>(UploadApprovalAnswer.Allowed);
        }
        TaskCompletionSource<UploadApprovalAnswer> answer = new TaskCompletionSource<UploadApprovalAnswer>(TaskCreationOptions.RunContinuationsAsynchronously);
        if (!CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.uploadApproval.Ask(names, site, title, DS.CursorColor(CS$<>8__locals1.<>4__this.Services.Settings.CursorColorHex), new Action(CS$<>8__locals1.<StartBrowserConnection>g__SaveAlways|15), answer);
        }))
        {
          answer.TrySetResult(UploadApprovalAnswer.Unanswered);
        }
        return answer.Task;
      };
      CS$<>8__locals1.bridge.AgentTurnLookup = delegate(string thread, string turn)
      {
        AgentBrowserTurn agentBrowserTurn = CS$<>8__locals1.agents.BrowserTurn(thread, turn);
        if (agentBrowserTurn == null)
        {
          return null;
        }
        return new BrowserTurnContext(agentBrowserTurn.Title, ClickyBrowserBridge.GroupTitle(agentBrowserTurn.GroupLabel), ClickyBrowserBridge.ProgressLine(agentBrowserTurn.LatestCommentary, agentBrowserTurn.ThinkingHeading), agentBrowserTurn.IsActive);
      };
      CS$<>8__locals1.agents.BrowserMcpServerProvider = delegate(CancellationToken _)
      {
        ValueTuple<string, string>? valueTuple = CS$<>8__locals1.bridge.EnsureAgentEndpoint();
        AgentHarnessMcpServer agentHarnessMcpServer;
        if (valueTuple != null)
        {
          ValueTuple<string, string> valueOrDefault = valueTuple.GetValueOrDefault();
          agentHarnessMcpServer = new AgentHarnessMcpServer("clicky_browser", valueOrDefault.Item1, valueOrDefault.Item2);
        }
        else
        {
          agentHarnessMcpServer = null;
        }
        return Task.FromResult<AgentHarnessMcpServer>(agentHarnessMcpServer);
      };
      CS$<>8__locals1.agents.BrowserTurnEnded += CS$<>8__locals1.bridge.Finish;
      CS$<>8__locals1.bridge.AgentNeedsChromeConnection += CS$<>8__locals1.agents.NoteFoundChromeNotConnected;
      CS$<>8__locals1.agents.ChromeIsConnected = () => CS$<>8__locals1.bridge.IsConnected;
      CS$<>8__locals1.agents.ThreadSkipsChrome = new Func<string, bool>(CS$<>8__locals1.flow.SkipsChrome);
      CS$<>8__locals1.agents.TaskNeedsChromeConnection += delegate(string thread)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.flow.JobNeedsChrome(thread);
        });
      };
      CS$<>8__locals1.agents.TaskNoLongerWaitsOnChrome += delegate(string thread)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.flow.TurnEndedWithoutChrome(thread);
        });
      };
      CS$<>8__locals1.bridge.ExtensionConnected += delegate(bool isFirstConnection)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          string text = CS$<>8__locals1.bridge.SetupBrowserName.ToLowerInvariant();
          CS$<>8__locals1.<>4__this.SayAtCursorAsync(isFirstConnection ? (text + "'s connected! i can use the sites you're signed into now ^_^") : (text + "'s connected again"), true, 7.0, default(CancellationToken));
        });
      };
      CS$<>8__locals1.bridge.SetupFailed += delegate(string message)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.SayAtCursorAsync(message, true, 12.0, default(CancellationToken));
        });
      };
      CS$<>8__locals1.bridge.Changed += delegate
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          SettingsContent shownContent = NotchSettingsTab.ShownContent;
          if (shownContent == null)
          {
            return;
          }
          shownContent.OnBrowserConnectionChanged();
        });
      };
      CS$<>8__locals1.flow.Changed += delegate
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          SettingsContent shownContent2 = NotchSettingsTab.ShownContent;
          if (shownContent2 == null)
          {
            return;
          }
          shownContent2.OnBrowserConnectionChanged();
        });
      };
      CS$<>8__locals1.bridge.RetireUnpackedPreviewCopyIfNeeded();
      Task.Run(new Func<Task>(CS$<>8__locals1.bridge.StartIfEnabledAsync));
      if (this.openBrowserSettingsWhenReady)
      {
        this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
        {
          CS$<>8__locals1.<>4__this.openBrowserSettingsWhenReady = false;
          CS$<>8__locals1.<>4__this.OpenBrowserSettings();
        });
      }
    }

    // Token: 0x060002E3 RID: 739 RVA: 0x0051C5A8 File Offset: 0x0051C5A8
    private void NoteJobEventForOneBrain(RealStack real, string threadId, string eventText)
    {
      AgentService agents = real.Agents;
      string text = agents.TaskIdForThread(threadId);
      if (text != null)
      {
        AgentTaskDetail taskDetail = agents.GetTaskDetail(text);
        if (taskDetail != null && !taskDetail.IsDemo)
        {
          real.Core.BrainSession.DropNoteForNextTurn(ClickyNotes.ProgressNoteKey(taskDetail.ClickySlug, threadId, text));
          if (ClickyJobsPreferences.IsEnabled)
          {
            string text2;
            if ((text2 = taskDetail.JobId) == null)
            {
              ClickyJob clickyJob = agents.Jobs.JobForThread(threadId);
              text2 = ((clickyJob != null) ? clickyJob.Id : null);
            }
            string text3 = text2;
            string text4;
            if (text3 != null)
            {
              ClickyJob clickyJob2 = agents.Jobs.Job(text3);
              if (clickyJob2 != null)
              {
                string title = clickyJob2.Title;
                if (title != null && title.Length > 0)
                {
                  text4 = title;
                  goto IL_00B8;
                }
              }
            }
            text4 = taskDetail.ClickyName ?? taskDetail.Snapshot.Title;
            IL_00B8:
            string text5 = text4;
            real.Core.BrainSession.NoteForNextTurn(ClickyNotes.JobEventNote(text5, threadId, eventText, null, null), null);
            return;
          }
          real.Core.BrainSession.NoteForNextTurn(ClickyNotes.EventNote(taskDetail.ClickyName, taskDetail.ClickySlug, taskDetail.Snapshot.Title, eventText, null, false, null, null, null), null);
          return;
        }
      }
    }

    // Token: 0x060002E4 RID: 740 RVA: 0x0051C6C0 File Offset: 0x0051C6C0
    private void OpenBrowserSettings()
    {
      this.OpenSettings(NotchSettingsSection.Integrations);
    }

    // Token: 0x060002E5 RID: 741 RVA: 0x0051C6CC File Offset: 0x0051C6CC
    internal void AnnounceJobNeedsChrome(string threadId)
    {
      RealStack real = this.Real;
      string text;
      if (real == null)
      {
        text = null;
      }
      else
      {
        AgentTaskDetail taskDetail = real.Agents.GetTaskDetail(this.Real.Agents.TaskIdForThread(threadId) ?? "");
        text = ((taskDetail != null) ? taskDetail.Snapshot.Title.Trim() : null);
      }
      string text2 = text ?? "";
      Action<string, string> noteClickyEventForOneBrain = this.NoteClickyEventForOneBrain;
      if (noteClickyEventForOneBrain != null)
      {
        noteClickyEventForOneBrain(threadId, "is waiting for them to choose about connecting their Chrome");
      }
      this.Services.Audio.PlayEarcon("agentNeedsYou");
      this.AnnounceLineAsync(ChromeSetupFlow.Shared.NeedsChromeLine((text2.Length == 0) ? null : text2));
      this.PresentTaskNeedsYou(threadId);
    }

    // Token: 0x060002E6 RID: 742 RVA: 0x0051C77C File Offset: 0x0051C77C
    internal void PresentTaskNeedsYou(string threadId)
    {
      Action<string> presentTaskNeedsYouHook = this.PresentTaskNeedsYouHook;
      if (presentTaskNeedsYouHook != null)
      {
        presentTaskNeedsYouHook(threadId);
        return;
      }
      Log.Info("Browser", "needs-you card for " + threadId + ": no dock on Windows yet");
    }

    // Token: 0x060002E7 RID: 743 RVA: 0x0051C7B8 File Offset: 0x0051C7B8
    internal void DismissCurrentNotchCard()
    {
      Action dismissNotchUpdateHook = this.DismissNotchUpdateHook;
      if (dismissNotchUpdateHook == null)
      {
        return;
      }
      dismissNotchUpdateHook();
    }

    // Token: 0x060002E8 RID: 744 RVA: 0x0051C7CC File Offset: 0x0051C7CC
    internal void ClearDrawing()
    {
      RealStack real = this.Real;
      if (real != null)
      {
        real.Canvas.Clear(true);
      }
    }

    // Token: 0x060002E9 RID: 745 RVA: 0x0051C7F0 File Offset: 0x0051C7F0
    internal Task<bool> AnnounceLineAsync(string line)
    {
      RealStack real = this.Real;
      return ((real != null) ? real.Companion.AnnounceAsync(line, true, default(CancellationToken)) : null) ?? Task.FromResult<bool>(false);
    }

    // Token: 0x17000067 RID: 103
    // (get) Token: 0x060002EA RID: 746 RVA: 0x0051C82C File Offset: 0x0051C82C
    internal VoiceState VoiceState
    {
      get
      {
        return this.Services.Companion.State;
      }
    }

    // Token: 0x060002EB RID: 747 RVA: 0x0051C840 File Offset: 0x0051C840
    internal void StartAppFactTurn(string fact)
    {
      RealStack real = this.Real;
      if (real != null)
      {
        real.Companion.SubmitFactAsync(fact, false);
      }
    }

    // Token: 0x060002EC RID: 748 RVA: 0x0051C868 File Offset: 0x0051C868
    private bool HandleBrowserDeepLink(Uri uri)
    {
      if (!(uri.Host.ToLowerInvariant() == "settings") || !(uri.AbsolutePath.TrimEnd('/') == "/browser"))
      {
        return false;
      }
      RealStack real = this.Real;
      if (real != null && !real.IsComplete)
      {
        this.openBrowserSettingsWhenReady = true;
        return true;
      }
      this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OpenBrowserSettings));
      return true;
    }

    // Token: 0x060002ED RID: 749 RVA: 0x0051C8DC File Offset: 0x0051C8DC
    private void StartBundle(RealStack real)
    {
      real.Agents.DeveloperModelSelection = delegate(string slug)
      {
        UserProfile user = this.Services.Auth.User;
        string text = ((user != null) ? user.Email : null);
        if (text == null || text.Length <= 0)
        {
          return null;
        }
        return DeveloperChatModels.Load(real.Core.Settings, text, slug);
      };
      BillingUi billingUi = this.billing;
      if (billingUi != null)
      {
        billingUi.PaywallShown += delegate(PaywallSource source, PaywallPayload _)
        {
          if (source == PaywallSource.LimitHit)
          {
            this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.PresentPaywallIntroVideoIfNeeded));
          }
        };
        billingUi.PaywallClosed += delegate(PaywallSource _, PaywallCloseKind _)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.DismissPaywallIntroVideo));
        };
      }
      if (!AppPaths.IsOverridden)
      {
        Task.Run(delegate
        {
          try
          {
            FileOpenRegistration.Register(null, null);
          }
          catch (Exception ex)
          {
            Log.Warn("startup", "open-with registration: " + ex.Message);
          }
        });
      }
      List<string> list = AppHost.HandedFilesIn(Program.Args);
      if (list != null && list.Count > 0)
      {
        this.OpenHandedFilesWhenReadyAsync(list);
      }
    }

    // Token: 0x060002EE RID: 750 RVA: 0x0051C994 File Offset: 0x0051C994
    public void PresentPaywallIntroVideoIfNeeded()
    {
      RealStack real = this.Real;
      if (real != null)
      {
        if (!real.Core.Settings.Read<bool>((AppSettings s) => s.HasShownPaywallIntroVideo))
        {
          if (this.CursorCompanion.IsPlayingVideo)
          {
            return;
          }
          string text = AppHost.AssetPath(new string[] { "Video", "paywall-intro-v2.mp4" });
          if (!this.CursorCompanion.PlayVideo(text, Array.Empty<CursorCompanion.Cue>(), delegate
          {
            this.paywallIntroVideoPlaying = false;
            Log.Info("video", "paywall intro over");
          }, 1.0, 15.0, 0.0, false))
          {
            return;
          }
          this.paywallIntroVideoPlaying = true;
          real.Core.Settings.Update(delegate(AppSettings s)
          {
            s.HasShownPaywallIntroVideo = true;
          });
          Log.Info("video", "paywall intro video presented");
          return;
        }
      }
    }

    // Token: 0x060002EF RID: 751 RVA: 0x0051CA88 File Offset: 0x0051CA88
    private void DismissPaywallIntroVideo()
    {
      if (!this.paywallIntroVideoPlaying)
      {
        return;
      }
      this.paywallIntroVideoPlaying = false;
      this.CursorCompanion.DismissVideo();
    }

    // Token: 0x17000068 RID: 104
    // (get) Token: 0x060002F0 RID: 752 RVA: 0x0051CAA8 File Offset: 0x0051CAA8
    public bool IsPaywallIntroVideoPlaying
    {
      get
      {
        return this.paywallIntroVideoPlaying && this.CursorCompanion.IsPlayingVideo;
      }
    }

    // Token: 0x060002F1 RID: 753 RVA: 0x0051CAC0 File Offset: 0x0051CAC0
    public void ResetPaywallIntroVideo()
    {
      RealStack real = this.Real;
      if (real != null)
      {
        real.Core.Settings.Update(delegate(AppSettings s)
        {
          s.HasShownPaywallIntroVideo = false;
        });
      }
      Log.Info("video", "paywall intro video reset");
    }

    // Token: 0x060002F2 RID: 754 RVA: 0x0051CB18 File Offset: 0x0051CB18
    public void PreviewOnboardingPaywall()
    {
      BillingUi billingUi = this.billing;
      if (billingUi == null)
      {
        return;
      }
      PlanSnapshot plan = billingUi.State.Plan;
      PaywallPayload paywallPayload = PaywallPayloads.ManualUpgrade("free", (plan != null) ? plan.ProAgentsCap : null, (plan != null) ? plan.MaxAgentsCap : null, ((plan != null) ? plan.ProYearlyAvailable : null).GetValueOrDefault(), ((plan != null) ? plan.MaxYearlyAvailable : null).GetValueOrDefault());
      if (paywallPayload == null)
      {
        Log.Warn("Billing", "preview paywall: no payload");
        return;
      }
      billingUi.ShowPaywall(paywallPayload, PaywallSource.DeveloperOnboardingPreview);
    }

    // Token: 0x060002F3 RID: 755 RVA: 0x0051CBC8 File Offset: 0x0051CBC8
    public void PopSuggestionsNotchForDemo()
    {
      ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
      if (proactiveCoordinator != null)
      {
        proactiveCoordinator.PreviewMorningNotch();
        return;
      }
      Log.Warn("proactive", "suggestions notch demo: no suggestions lane");
    }

    // Token: 0x060002F4 RID: 756 RVA: 0x0051CBF4 File Offset: 0x0051CBF4
    public void PresentNewAgentsUiDemo()
    {
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsRunning)
      {
        tutorialWindow.BringForward();
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.DismissNotchAgentPeek();
      }
      if (this.tutorial == null)
      {
        this.tutorial = this.CreateTutorial();
      }
      this.newAgentsUiDemoRun = true;
      RealStack real = this.Real;
      if (real != null)
      {
        real.Tutorial.SkipsDrawingSteps = true;
      }
      this.Interview.UsesNewAgentsUiDemoPlan = true;
      StarterCast.Outfit();
      this.tutorial.Show(TutorialWindow.Step.VoiceHello, false);
      Log.Info("tutorial", "new agents UI demo: tour opened on voice hello; drawing steps skipped; fixed starter set");
    }

    // Token: 0x060002F5 RID: 757 RVA: 0x0051CC8C File Offset: 0x0051CC8C
    private void EndNewAgentsUiDemoRun()
    {
      if (!this.newAgentsUiDemoRun)
      {
        return;
      }
      this.newAgentsUiDemoRun = false;
      RealStack real = this.Real;
      if (real != null)
      {
        real.Tutorial.SkipsDrawingSteps = false;
      }
      InterviewController interview = this.Interview;
      if (interview != null)
      {
        interview.UsesNewAgentsUiDemoPlan = false;
      }
    }

    // Token: 0x17000069 RID: 105
    // (get) Token: 0x060002F6 RID: 758 RVA: 0x0051CCD0 File Offset: 0x0051CCD0
    public bool IsNewAgentsUiDemoRun
    {
      get
      {
        return this.newAgentsUiDemoRun;
      }
    }

    // Token: 0x060002F7 RID: 759 RVA: 0x0051CCD8 File Offset: 0x0051CCD8
    private static List<string> HandedFilesIn(IEnumerable<string> args)
    {
      return (from a in args
        where !a.StartsWith("--", StringComparison.Ordinal) && !a.Contains("://", StringComparison.Ordinal)
        select a.Trim('"') into a
        where a.Length > 2 && Path.IsPathRooted(a) && File.Exists(a)
        select a).Distinct<string>(StringComparer.OrdinalIgnoreCase).ToList<string>();
    }

    // Token: 0x060002F8 RID: 760 RVA: 0x0051CD64 File Offset: 0x0051CD64
    private bool OpenHandedFiles(IEnumerable<string> args)
    {
      List<string> list = AppHost.HandedFilesIn(args);
      if (list.Count == 0)
      {
        return false;
      }
      this.OpenHandedFilesWhenReadyAsync(list);
      return true;
    }

    // Token: 0x060002F9 RID: 761 RVA: 0x0051CD8C File Offset: 0x0051CD8C
    private async Task OpenHandedFilesWhenReadyAsync(IReadOnlyList<string> files)
    {
      for (int i = 0; i < 100; i++)
      {
        RealStack real = this.Real;
        if (real == null || real.IsComplete)
        {
          break;
        }
        await Task.Delay(100);
      }
      this.DispatcherQueue.TryEnqueue(delegate
      {
        this.OpenFilesHandedToHeyClicky(files);
      });
    }

    // Token: 0x060002FA RID: 762 RVA: 0x0051CDD8 File Offset: 0x0051CDD8
    private void OpenFilesHandedToHeyClicky(IReadOnlyList<string> files)
    {
      RealStack real = this.Real;
      bool flag = real != null && real.Core.Auth.IsSignedIn && this.Services.Settings.HasCompletedOnboarding;
      foreach (string text in files)
      {
        string text2 = "notch";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
        defaultInterpolatedStringHandler.AppendLiteral("file handed over: ");
        defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(text));
        defaultInterpolatedStringHandler.AppendLiteral(" (notch viewer: ");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        if (!flag || !this.OpenDocumentInNotch(text, null))
        {
          AppHost.OpenOutsideHeyClicky(text);
        }
      }
    }

    // Token: 0x060002FB RID: 763 RVA: 0x0051CEB0 File Offset: 0x0051CEB0
    private static void OpenOutsideHeyClicky(string file)
    {
      try
      {
        string text = FileOpenRegistration.DefaultHandlerFor(file);
        bool flag;
        if (text != null)
        {
          string processPath = Environment.ProcessPath;
          if (processPath != null)
          {
            flag = string.Equals(Path.GetFullPath(text), Path.GetFullPath(processPath), StringComparison.OrdinalIgnoreCase);
            goto IL_0028;
          }
        }
        flag = false;
        IL_0028:
        if (flag || text == null)
        {
          Process.Start(new ProcessStartInfo("rundll32.exe", "shell32.dll,OpenAs_RunDLL " + file)
          {
            UseShellExecute = false
          });
        }
        else
        {
          ShellOpen.Start(file);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("notch", "open " + Path.GetFileName(file) + " outside: " + ex.Message);
      }
    }

    // Token: 0x060002FC RID: 764 RVA: 0x0051CF50 File Offset: 0x0051CF50
    public void AnswerIntegrationSuggestion(string toolkit, IntegrationAnswer answer)
    {
      string text = "integrations";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
      defaultInterpolatedStringHandler.AppendLiteral("suggestion for ");
      defaultInterpolatedStringHandler.AppendFormatted(toolkit);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted<IntegrationAnswer>(answer);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ProactiveCoordinator proactive = this.Proactive;
      if (proactive != null)
      {
        proactive.AnswerIntegrationSuggestion(toolkit, answer);
        return;
      }
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.IntegrationSuggestion));
    }

    // Token: 0x060002FD RID: 765 RVA: 0x0051CFD0 File Offset: 0x0051CFD0
    public void RemoveDictionaryWord(string word)
    {
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.DictionaryWordLearned));
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      Predicate<string> <>9__1;
      real.Core.Settings.Update(delegate(AppSettings s)
      {
        List<string> dictationCustomWords = s.DictationCustomWords;
        Predicate<string> predicate;
        if ((predicate = <>9__1) == null)
        {
          predicate = (<>9__1 = (string w) => string.Equals(w, word, StringComparison.OrdinalIgnoreCase));
        }
        dictationCustomWords.RemoveAll(predicate);
      });
      Log.Info("dictation", "removed \"" + word + "\" from the dictionary");
    }

    // Token: 0x060002FE RID: 766 RVA: 0x0051D04C File Offset: 0x0051D04C
    public void RestoreOutputVolume()
    {
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.VoiceReplyMutedCopiedToClipboard));
      Task.Run<bool>(() => SystemAudioOutputVolume.RestoreAudibleOutput(0.5f));
    }

    // Token: 0x060002FF RID: 767 RVA: 0x0051D098 File Offset: 0x0051D098
    public void ResumeTutorial()
    {
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialResume));
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null)
      {
        if (tutorialWindow.IsRunning)
        {
          ClickyAnalytics.TrackOnboardingTutorialResumedFromNotch();
        }
        tutorialWindow.BringForward();
        return;
      }
      this.StartTutorial();
    }

    // Token: 0x06000300 RID: 768 RVA: 0x0051D0E4 File Offset: 0x0051D0E4
    private void ShowTutorialResumeCard()
    {
      NotchGeometry.SizeContext sizeContext = this.NotchUi.SizeContext;
      TutorialWindow tutorialWindow = this.tutorial;
      sizeContext.TutorialResumeShowsContinue = tutorialWindow == null || tutorialWindow.IsBuried;
      this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.OnboardingTutorialResume(), null);
      this.NotchUi.HoverChanged -= this.DismissResumeOnHoverOut;
      this.NotchUi.HoverChanged += this.DismissResumeOnHoverOut;
    }

    // Token: 0x06000301 RID: 769 RVA: 0x0051D160 File Offset: 0x0051D160
    private void DismissResumeOnHoverOut(bool hovering)
    {
      if (hovering)
      {
        return;
      }
      this.NotchUi.HoverChanged -= this.DismissResumeOnHoverOut;
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialResume));
    }

    // Token: 0x06000302 RID: 770 RVA: 0x0051D198 File Offset: 0x0051D198
    public void SendAgentFollowUp(string taskId, string text, IReadOnlyList<string> files)
    {
      string text2;
      if (files.Count != 0)
      {
        text2 = ((text.Length > 0) ? (text + "\n\n") : "") + "Attached files:\n" + string.Join("\n", files.Select<string, string>((string f) => "- " + f));
      }
      else
      {
        text2 = text;
      }
      string text3 = text2;
      string text4 = "agents";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
      defaultInterpolatedStringHandler.AppendLiteral("notch follow-up to ");
      defaultInterpolatedStringHandler.AppendFormatted(taskId);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
      defaultInterpolatedStringHandler.AppendLiteral(" chars, ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(files.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" file(s))");
      Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
      this.Services.Agents.SteerAsync(taskId, text3, default(CancellationToken)).ContinueWith(delegate(Task t)
      {
        if (t.IsFaulted)
        {
          string text5 = "agents";
          string text6 = "notch follow-up to ";
          string taskId2 = taskId;
          string text7 = " failed: ";
          AggregateException exception = t.Exception;
          Log.Warn(text5, text6 + taskId2 + text7 + ((exception != null) ? exception.GetBaseException().Message : null));
        }
      }, TaskScheduler.Default);
    }

    // Token: 0x1700006A RID: 106
    // (get) Token: 0x06000303 RID: 771 RVA: 0x0051D2BC File Offset: 0x0051D2BC
    public AppServices Services { get; }

    // Token: 0x1700006B RID: 107
    // (get) Token: 0x06000304 RID: 772 RVA: 0x0051D2C4 File Offset: 0x0051D2C4
    public AppOptions Options { get; }

    // Token: 0x1700006C RID: 108
    // (get) Token: 0x06000305 RID: 773 RVA: 0x0051D2CC File Offset: 0x0051D2CC
    [Nullable(2)]
    public RealStack Real
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x1700006D RID: 109
    // (get) Token: 0x06000306 RID: 774 RVA: 0x0051D2D4 File Offset: 0x0051D2D4
    [Nullable(2)]
    public OneBrainJobReports JobReports
    {
      [NullableContext(2)]
      get
      {
        RealStack real = this.Real;
        if (real == null)
        {
          return null;
        }
        return real.JobReports;
      }
    }

    // Token: 0x1700006E RID: 110
    // (get) Token: 0x06000307 RID: 775 RVA: 0x0051D2E8 File Offset: 0x0051D2E8
    public DispatcherQueue DispatcherQueue { get; }

    // Token: 0x1700006F RID: 111
    // (get) Token: 0x06000308 RID: 776 RVA: 0x0051D2F0 File Offset: 0x0051D2F0
    // (set) Token: 0x06000309 RID: 777 RVA: 0x0051D2F8 File Offset: 0x0051D2F8
    public NotchView NotchUi { get; private set; }

    // Token: 0x17000070 RID: 112
    // (get) Token: 0x0600030A RID: 778 RVA: 0x0051D304 File Offset: 0x0051D304
    [Nullable(2)]
    internal HotkeyRouter Hotkeys
    {
      [NullableContext(2)]
      get
      {
        return this.hotkeys;
      }
    }

    // Token: 0x0600030B RID: 779 RVA: 0x0051D30C File Offset: 0x0051D30C
    public AppHost(AppServices services, AppOptions options, [Nullable(2)] RealStack real)
    {
      this.Services = services;
      this.Options = options;
      this.Real = real;
      this.DispatcherQueue = DispatcherQueue.GetForCurrentThread();
      if (options.UseFakes)
      {
        ClickyRoster.UseDirectory(AppPaths.Combine(new string[] { "demo" }));
      }
      ClickyRoster.Shared.AssignedPackId = () => services.Settings.SquadPack;
    }

    // Token: 0x17000071 RID: 113
    // (get) Token: 0x0600030C RID: 780 RVA: 0x0051D438 File Offset: 0x0051D438
    [Nullable(2)]
    internal NotchPresenter CorePresenter
    {
      [NullableContext(2)]
      get
      {
        return this.Services.Presenter as NotchPresenter;
      }
    }

    // Token: 0x0600030D RID: 781 RVA: 0x0051D44C File Offset: 0x0051D44C
    public void Start()
    {
      Anim.Instant = Snapshot.Directory != null;
      ComposerBox.CalibrateLineHeight();
      this.NotchUi = new NotchView(this.Services, this, !this.Options.AllowCapture);
      ScreenCaptureYield.Register(this.NotchUi.Window.Hwnd);
      SingleInstance.NoteMainWindow(this.NotchUi.Window.Hwnd);
      RealStack real = this.Real;
      ScreenDrawingCanvas screenDrawingCanvas = ((real != null) ? real.Canvas : null);
      if (screenDrawingCanvas != null)
      {
        IntPtr notchWindow = this.NotchUi.Window.Hwnd;
        screenDrawingCanvas.NotchWindowProvider = () => notchWindow;
      }
      this.NotchUi.FirstFrameShown += delegate
      {
        string text = "startup";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
        defaultInterpolatedStringHandler.AppendLiteral("cold start → notch visible: ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(Program.MillisecondsSinceProcessStart(), "0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms (since Main: ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(Program.SinceStart.Elapsed.TotalMilliseconds, "0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms)");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        UpdateWindow.SignalAppReady();
        RealStack real = this.Real;
        if (real != null)
        {
          this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, delegate
          {
            this.CompleteRealServices(real);
          });
        }
      };
      ShellOpen.UserIsTypingInHeyClicky = () => this.NotchUi.HasKeyboard;
      this.NotchUi.HoverDwellCompleted += this.OnNotchHoverDwell;
      this.NotchUi.HoverChanged += delegate(bool inside)
      {
        this.notchWindowHovered = inside;
        this.SyncPointerOnNotch();
      };
      this.NotchUi.IdleClicked += this.OnNotchIdleClicked;
      RealStack real2 = this.Real;
      bool flag;
      if (real2 == null)
      {
        flag = false;
      }
      else
      {
        flag = real2.Core.Settings.Read<bool>((AppSettings s) => s.StreamMode);
      }
      if (flag)
      {
        this.NotchUi.SetStreamMode(true);
      }
      this.NotchUi.Show();
      Uri deepLink = this.Options.DeepLink;
      if (deepLink != null)
      {
        if (this.Real == null)
        {
          this.HandleDeepLinkSafely(deepLink);
        }
        else
        {
          this.pendingLaunchArgs.Add(Program.Args);
        }
      }
      if (this.Real != null)
      {
        return;
      }
      this.bridge = new CompanionNotchBridge(this.Services, this.DispatcherQueue);
      this.StartNotch();
      this.Services.Shortcuts.Start();
      this.StartAuxiliary();
      this.StartAmbient();
      this.StartCursorModes();
      if (this.Options.DemoStates || this.Options.DemoOnly != null)
      {
        new DemoRunner(this).Run(this.Options.DemoOnly);
        return;
      }
      this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, new DispatcherQueueHandler(this.StartFirstRunFlow));
    }

    // Token: 0x0600030E RID: 782 RVA: 0x0051D680 File Offset: 0x0051D680
    private void OnNotchIdleClicked()
    {
      if (DrawnOnboardingTour.HoldsNotch)
      {
        DrawnOnboardingTour.NudgeNotchClicked(this);
        return;
      }
      if (this.introSequenceActive)
      {
        return;
      }
      this.OpenNotchHome();
    }

    // Token: 0x0600030F RID: 783 RVA: 0x0051D6A0 File Offset: 0x0051D6A0
    internal void SimulateNotchIdleClick()
    {
      this.OnNotchIdleClicked();
    }

    // Token: 0x06000310 RID: 784 RVA: 0x0051D6A8 File Offset: 0x0051D6A8
    private void CompleteRealServices(RealStack real)
    {
      try
      {
        real.Complete(this.DispatcherQueue, SynchronizationContext.Current);
        CompositionRoot.FillFromRealStack(this.Services, real);
        this.hotkeys = new HotkeyRouter(this.Services, real, this.DispatcherQueue);
        this.WireRealServices(real);
        this.StartBrowserConnection(real);
        this.StartClickyDrive(real);
        this.StartSystemServices(real);
        this.StartDock(real);
        this.StartNotch();
        this.StartProactive(real);
        this.StartDrawnTour(real);
        this.StartAuxiliary();
        this.StartApplyingSettings();
        this.StartInkNotes(real);
        this.StartAmbient();
        this.StartCursorModes();
        this.StartTaskbarButton(real);
        this.StartNotchPolish(real);
        this.StartArtifactAutoOpen(real);
        this.cursorShownForOnboarding = this.HasCompletedOnboardingNow;
        real.StartBackground(this.Services.Settings.CursorColorHex, this.cursorShownForOnboarding);
        real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncCursorWithOnboarding));
        };
        string text = "startup";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
        defaultInterpolatedStringHandler.AppendLiteral("all services up: ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(Program.MillisecondsSinceProcessStart(), "0");
        defaultInterpolatedStringHandler.AppendLiteral(" ms since process start");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      catch (Exception ex)
      {
        Log.Error("startup", "real services failed to start", ex);
      }
      this.realServicesStarted = true;
      foreach (string[] array in this.pendingLaunchArgs)
      {
        this.HandleForwardedArgs(array);
      }
      this.pendingLaunchArgs.Clear();
      this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, new DispatcherQueueHandler(this.StartFirstRunFlow));
      string scenario = this.Options.E2E;
      if (scenario != null)
      {
        this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
        {
          new EndToEnd(this).RunAsync(scenario);
        });
      }
    }

    // Token: 0x06000311 RID: 785 RVA: 0x0051D8CC File Offset: 0x0051D8CC
    private void SyncCursorWithOnboarding()
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      bool hasCompletedOnboardingNow = this.HasCompletedOnboardingNow;
      if (hasCompletedOnboardingNow == this.cursorShownForOnboarding)
      {
        return;
      }
      this.cursorShownForOnboarding = hasCompletedOnboardingNow;
      if (hasCompletedOnboardingNow)
      {
        real.Cursor.Show();
      }
      else
      {
        real.Cursor.Hide();
      }
      Log.Info("cursor", hasCompletedOnboardingNow ? "onboarding complete: companion cursor shown" : "onboarding reset: companion cursor hidden");
    }

    // Token: 0x06000312 RID: 786 RVA: 0x0051D930 File Offset: 0x0051D930
    private void WireRealServices(RealStack real)
    {
      WasapiMicrophoneCapture wasapiMicrophoneCapture = real.Microphone as WasapiMicrophoneCapture;
      if (wasapiMicrophoneCapture != null)
      {
        wasapiMicrophoneCapture.Trouble += delegate(string message)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            if (message == null)
            {
              this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.MicrophoneTrouble));
              return;
            }
            this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.MicrophoneTrouble(message, null, true), null);
          });
        };
      }
      this.accountWasSignedIn = real.Core.Auth.IsSignedIn;
      real.Core.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OnAuthChanged));
      };
      real.Companion.UnhandledDrawingAction += delegate([Nullable(2)] object _, DrawingAction action)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.OnDrawingAction(action);
        });
      };
      real.Companion.Failed += delegate([Nullable(2)] object _, CompanionError error)
      {
        Log.Warn("companion", "failed: " + error.Code + ": " + error.Message);
      };
      real.Companion.TurnCompleted += delegate([Nullable(2)] object _, CompanionTurnMetrics m)
      {
        this.NoteTurnKind(m.Typed);
      };
      real.Companion.TurnCompleted += delegate([Nullable(2)] object _, CompanionTurnMetrics m)
      {
        string text = "latency";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(125, 9);
        defaultInterpolatedStringHandler.AppendLiteral("turn ");
        defaultInterpolatedStringHandler.AppendFormatted(m.Typed ? "typed" : "voice");
        defaultInterpolatedStringHandler.AppendLiteral(": transcript ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(m.TranscriptMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, capture ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(m.CaptureMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, brain first byte ");
        defaultInterpolatedStringHandler.AppendFormatted<int?>(m.BrainFirstByteMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first say line ");
        defaultInterpolatedStringHandler.AppendFormatted<int?>(m.FirstSayLineMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first audio ");
        defaultInterpolatedStringHandler.AppendFormatted<int?>(m.FirstAudioMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, first spoken ");
        defaultInterpolatedStringHandler.AppendFormatted<int?>(m.FirstSpokenMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms, total ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(m.TotalMs);
        defaultInterpolatedStringHandler.AppendLiteral(" ms (");
        defaultInterpolatedStringHandler.AppendFormatted(m.Model);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      };
      DispatcherQueueHandler <>9__14;
      real.Tutorial.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs e)
      {
        string propertyName = e.PropertyName;
        bool flag = propertyName == "IsRestrictingNormalUse" || propertyName == "Phase" || propertyName == "CurrentStep";
        if (flag)
        {
          DispatcherQueue dispatcherQueue = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__14) == null)
          {
            dispatcherQueueHandler = (<>9__14 = delegate
            {
              NotchPresenter corePresenter = this.CorePresenter;
              if (corePresenter == null)
              {
                return;
              }
              corePresenter.SetTutorialRestrictingNormalUse(real.Tutorial.IsRestrictingNormalUse);
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        }
      };
      if (this.hotkeys != null)
      {
        this.hotkeys.AllowsPushToTalk = () => this.AllowsShortcut(real, false);
        this.hotkeys.AllowsTextMode = () => this.AllowsShortcut(real, true);
        this.hotkeys.InterceptReleaseAfterPress = delegate
        {
          TutorialWindow t = this.tutorial;
          if (t == null || !t.IsOnDriveStep)
          {
            return false;
          }
          this.DispatcherQueue.TryEnqueue(delegate
          {
            t.InterceptPushToTalkRelease();
          });
          return true;
        };
      }
      DictationHooks.TutorialAllowsPress = () => this.AllowsShortcut(real, AppHost.ShortcutKind.Dictation);
      DictationHooks.ConsumeTutorialTranscript = delegate(string transcript)
      {
        if (!real.Tutorial.IsOnDictationStep)
        {
          return false;
        }
        this.DispatcherQueue.TryEnqueue(delegate
        {
          if (!real.Tutorial.ConsumeDictationTranscript(transcript))
          {
            Log.Info("tutorial", "a dictation came back after the dictation step was left: dropped");
          }
        });
        return true;
      };
      this.WireRoutinePower(real);
      this.StartBilling(real);
      this.StartBundle(real);
      this.oneBrainStatsPanel = new OneBrainStatsPanel(real.Core.Settings, !this.Options.AllowCapture);
      this.oneBrainStatsPanel.Start();
    }

    // Token: 0x06000313 RID: 787 RVA: 0x0051DB30 File Offset: 0x0051DB30
    private bool AllowsShortcut(RealStack real, bool text)
    {
      return this.AllowsShortcut(real, text ? AppHost.ShortcutKind.Text : AppHost.ShortcutKind.PushToTalk);
    }

    // Token: 0x06000314 RID: 788 RVA: 0x0051DB40 File Offset: 0x0051DB40
    private bool AllowsShortcut(RealStack real, AppHost.ShortcutKind kind)
    {
      if (kind == AppHost.ShortcutKind.PushToTalk && this.RoutedPressToMicrophonePermissionCard(real))
      {
        return false;
      }
      if (real.Tutorial.Phase == OnboardingTutorialPhase.InvitePresented)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          if (real.Tutorial.Phase == OnboardingTutorialPhase.InvitePresented)
          {
            AppHost.ShortcutKind kind3 = kind;
            string text;
            if (kind3 != AppHost.ShortcutKind.Text)
            {
              if (kind3 != AppHost.ShortcutKind.Dictation)
              {
                text = "invite_bypassed_by_ptt";
              }
              else
              {
                text = "invite_bypassed_by_dictation";
              }
            }
            else
            {
              text = "invite_bypassed_by_text";
            }
            ClickyAnalytics.TrackOnboardingTutorialResolved(text, null, null);
          }
          real.Tutorial.BypassInvite();
          this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialInvite));
        });
        return true;
      }
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsDriveDemoDriving)
      {
        return false;
      }
      if (this.TourHoldsHome || (this.introSequenceActive && kind != AppHost.ShortcutKind.Dictation))
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.TourRefusal("sorry, i'll let you use the shortcut right after i'm done!");
        });
        return false;
      }
      AppHost.ShortcutKind kind2 = kind;
      bool flag;
      if (kind2 != AppHost.ShortcutKind.Text)
      {
        if (kind2 != AppHost.ShortcutKind.Dictation)
        {
          flag = real.Tutorial.AllowsPushToTalk;
        }
        else
        {
          flag = real.Tutorial.AllowsDictationPress;
        }
      }
      else
      {
        flag = real.Tutorial.AllowsTextMode;
      }
      return flag;
    }

    // Token: 0x06000315 RID: 789 RVA: 0x0051DC40 File Offset: 0x0051DC40
    internal bool RoutedPressToMicrophonePermissionCard(RealStack real)
    {
      if (!this.Services.Settings.HasCompletedOnboarding)
      {
        return false;
      }
      PermissionStatus permissionStatus = real.Permissions.RefreshMicrophoneStatus();
      if (permissionStatus != PermissionStatus.WaitingInSettings && permissionStatus != PermissionStatus.Denied)
      {
        return false;
      }
      string text = "permissions";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(67, 1);
      defaultInterpolatedStringHandler.AppendLiteral("a press with the microphone ");
      defaultInterpolatedStringHandler.AppendFormatted<PermissionStatus>(permissionStatus);
      defaultInterpolatedStringHandler.AppendLiteral(": the permission card instead of a turn");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.DispatcherQueue.TryEnqueue(delegate
      {
        NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
        if (notchOnboardingCardViewModel == null)
        {
          return;
        }
        notchOnboardingCardViewModel.Refresh();
        this.SyncOnboardingPin();
        if (notchOnboardingCardViewModel.Kind == NotchOnboardingCardKind.Permission)
        {
          int? num = this.CardStep(notchOnboardingCardViewModel);
          if (num != null)
          {
            int valueOrDefault = num.GetValueOrDefault();
            this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(valueOrDefault));
          }
        }
      });
      return true;
    }

    // Token: 0x06000316 RID: 790 RVA: 0x0051DCD4 File Offset: 0x0051DCD4
    private void OnAuthChanged()
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      bool isSignedIn = real.Core.Auth.IsSignedIn;
      string text = "auth";
      string text2;
      if (!isSignedIn)
      {
        text2 = "signed out";
      }
      else
      {
        string text3 = "signed in as ";
        UserProfile user = real.Core.Auth.User;
        text2 = text3 + ((user != null) ? user.Email : null);
      }
      Log.Info(text, text2);
      this.OnboardingAuthChanged(isSignedIn);
      if (isSignedIn)
      {
        this.NoteAccountSignedIn();
        real.Agents.PrewarmAsync(default(CancellationToken));
        real.Core.StartAsync(default(CancellationToken));
        real.BrainContext.Refresh();
        ConnectedApps.RefreshAsync();
        ConnectorAuth.OnLaunchAsync();
        this.ClaimPendingReferralCodeAsync();
        real.Companion.Prewarm();
        return;
      }
      real.Agents.SignOutAsync();
      CompanionConversationBuffer.Shared.Clear();
      this.ForgetSignedOutAccount(real);
    }

    // Token: 0x06000317 RID: 791 RVA: 0x0051DDB8 File Offset: 0x0051DDB8
    private void OnDrawingAction(DrawingAction action)
    {
      string kind = action.Kind;
      if (!(kind == "button"))
      {
        if (kind == "figurePart")
        {
          string text = AppHost.<OnDrawingAction>g__Str|119_0(action.Payload, "name") ?? "this part";
          JsonElement jsonElement;
          string text2 = ((action.Payload.ValueKind == JsonValueKind.Object && action.Payload.TryGetProperty("isLabelled", out jsonElement) && jsonElement.ValueKind == JsonValueKind.True) ? ("the part labelled \"" + text + "\"") : text);
          Log.Info("drawing", "3D part clicked: " + text2);
          this.Services.Companion.AskFromDrawingAsync("What's this? (I just clicked " + text2 + " on the 3D model you drew.)", "figure_part");
          return;
        }
        if (!this.HandleTourDrawingAction(action))
        {
          Log.Info("drawing", "unhandled drawing action " + action.Kind);
        }
      }
      else
      {
        string text3 = AppHost.<OnDrawingAction>g__Str|119_0(action.Payload, "kind") ?? "ask";
        string text4 = AppHost.<OnDrawingAction>g__Str|119_0(action.Payload, "text");
        string text5 = AppHost.<OnDrawingAction>g__Str|119_0(action.Payload, "value");
        string text6 = "drawing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 2);
        defaultInterpolatedStringHandler.AppendLiteral("button pressed: \"");
        defaultInterpolatedStringHandler.AppendFormatted(text4);
        defaultInterpolatedStringHandler.AppendLiteral("\" (");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text6, defaultInterpolatedStringHandler.ToStringAndClear());
        if (text3 == "card")
        {
          Guid guid;
          if (Guid.TryParse(text5, out guid))
          {
            ProactiveCoordinator proactive = this.Proactive;
            if (((proactive != null) ? proactive.Approve(guid, null, false, "button") : null) != null)
            {
              RealStack real = this.Real;
              if (real == null)
              {
                return;
              }
              real.Core.BrainSession.NoteForNextTurn("[clicky drawing] The user pressed \"" + text4 + "\" on the drawing, which started that suggested task in the background.", null);
              return;
            }
          }
        }
        else if (text3.Equals("handoff", StringComparison.OrdinalIgnoreCase))
        {
          RealStack real2 = this.Real;
          if (real2 != null)
          {
            string text7;
            if ((text7 = text5) == null)
            {
              text7 = text4 ?? "";
            }
            string text8 = AgentTools.HandOffArguments(text7);
            if (text8 != null)
            {
              real2.Tools.Agents.RunAsync("sessions_spawn", new ToolArgs(text8), CompanionToolContext.None, default(CancellationToken)).ContinueWith(delegate(Task<string> t)
              {
                string text11 = "drawing";
                string text12 = "hand-off: ";
                string text13;
                if (!t.IsCompletedSuccessfully)
                {
                  AggregateException exception = t.Exception;
                  text13 = ((exception != null) ? exception.GetBaseException().Message : null);
                }
                else
                {
                  text13 = t.Result;
                }
                Log.Info(text11, text12 + text13);
              }, TaskScheduler.Default);
              return;
            }
          }
          string text9 = text5 ?? text4;
          if (text9 != null && text9.Length > 0)
          {
            this.Services.Agents.LaunchAsync(text9, null, default(CancellationToken));
            return;
          }
        }
        else
        {
          string text10 = text5 ?? text4;
          if (text10 != null && text10.Length > 0)
          {
            this.Services.Companion.AskFromDrawingAsync(text10, "button");
            return;
          }
        }
      }
    }

    // Token: 0x06000318 RID: 792 RVA: 0x0051E0AC File Offset: 0x0051E0AC
    public void HandleForwardedArgs(string[] args)
    {
      if (this.Real != null && !this.realServicesStarted)
      {
        this.pendingLaunchArgs.Add(args);
        return;
      }
      AppOptions appOptions = AppOptions.Parse(args);
      if (this.OpenHandedFiles(args))
      {
        return;
      }
      Uri deepLink = appOptions.DeepLink;
      if (deepLink != null)
      {
        this.HandleDeepLinkSafely(deepLink);
        return;
      }
      if (appOptions.Background)
      {
        Log.Info("app", "second launch at login: staying quiet");
        return;
      }
      this.HandleReopen();
    }

    // Token: 0x06000319 RID: 793 RVA: 0x0051E118 File Offset: 0x0051E118
    public void RecoverFromLayoutCycle()
    {
      long tickCount = Environment.TickCount64;
      if (tickCount - this.lastLayoutCycleRecoveryAt < 5000L)
      {
        Log.Warn("layout", "another layout cycle right after rebuilding: not rebuilding again yet");
        return;
      }
      this.lastLayoutCycleRecoveryAt = tickCount;
      this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
      {
        List<string> list = new List<string>();
        try
        {
          this.NotchUi.RebuildAfterLayoutCycle();
          list.Add("notch");
        }
        catch (Exception ex)
        {
          Log.Warn("layout", "rebuilding the notch failed: " + ex.Message);
        }
        try
        {
          ArcDock dock = this.Dock;
          if (dock != null)
          {
            dock.RebuildAfterLayoutCycle();
            list.Add("dock");
          }
        }
        catch (Exception ex2)
        {
          Log.Warn("layout", "rebuilding the dock failed: " + ex2.Message);
        }
        Log.Warn("layout", "layout cycle: rebuilt " + string.Join(", ", list.DefaultIfEmpty("nothing")));
        ClickyAnalytics.ReportInternalError("ui", "layout_cycle_recovered", null, "rebuilt: " + string.Join(", ", list.DefaultIfEmpty("nothing")) + "; presentation: " + NotchView.Describe(this.Services.Presenter.Presentation), CrashReportLevel.Warning, null, null);
      });
    }

    // Token: 0x0600031A RID: 794 RVA: 0x0051E16C File Offset: 0x0051E16C
    private void HandleDeepLinkSafely(Uri uri)
    {
      try
      {
        this.HandleDeepLink(uri);
      }
      catch (Exception ex)
      {
        CrashReporting.ReportUiException(ex);
      }
    }

    // Token: 0x0600031B RID: 795 RVA: 0x0051E19C File Offset: 0x0051E19C
    private void HandleDeepLink(Uri uri)
    {
      Log.Info("deeplink", uri.Scheme + "://" + uri.Host + uri.AbsolutePath);
      if (this.Real != null && this.HandleBrowserDeepLink(uri))
      {
        return;
      }
      if (uri.Host.Equals("settings", StringComparison.OrdinalIgnoreCase))
      {
        this.OpenSettings(uri.AbsolutePath.TrimEnd('/').Equals("/browser", StringComparison.OrdinalIgnoreCase) ? NotchSettingsSection.Integrations : NotchSettingsSection.Account);
        return;
      }
      if (uri.Host.Equals("tutorial", StringComparison.OrdinalIgnoreCase))
      {
        this.StartTutorial();
        return;
      }
      if (!uri.Host.Equals("auth-callback", StringComparison.OrdinalIgnoreCase) && this.HandleSystemDeepLink(uri))
      {
        return;
      }
      this.HandleAccountLinkAsync(uri);
    }

    // Token: 0x0600031C RID: 796 RVA: 0x0051E25C File Offset: 0x0051E25C
    private async Task HandleAccountLinkAsync(Uri uri)
    {
      try
      {
        AppHost.<>c__DisplayClass127_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass127_0();
        TaskAwaiter<bool> taskAwaiter = this.Services.Auth.HandleDeepLinkAsync(uri).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        if (!taskAwaiter.GetResult())
        {
          if (AuthDeepLink.IsAuthCallback(uri))
          {
            if (!this.Services.Auth.IsSignedIn)
            {
              this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.ShowSignInAgain));
            }
          }
          else
          {
            CS$<>8__locals1.checkout = WorkerClient.ParseCheckoutCompleteLink(uri);
            if (CS$<>8__locals1.checkout != null)
            {
              string text = "deeplink";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 1);
              defaultInterpolatedStringHandler.AppendLiteral("checkout complete: ");
              defaultInterpolatedStringHandler.AppendFormatted<CheckoutCompleteLink>(CS$<>8__locals1.checkout);
              Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
              int i = 0;
              while (i < 100 && this.billing == null && this.Real != null)
              {
                await Task.Delay(100);
                i++;
              }
              CS$<>8__locals1.b = this.billing;
              if (CS$<>8__locals1.b != null)
              {
                this.DispatcherQueue.TryEnqueue(delegate
                {
                  CS$<>8__locals1.b.HandleCheckoutComplete(CS$<>8__locals1.checkout);
                });
              }
              else
              {
                RealStack real = this.Real;
                if (real != null)
                {
                  await real.Core.Billing.HandleCheckoutCompleteAsync(CS$<>8__locals1.checkout);
                }
              }
            }
            else
            {
              Log.Info("deeplink", "unhandled link: " + uri.Scheme + "://" + uri.Host);
            }
            CS$<>8__locals1 = null;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("deeplink", ex.Message);
      }
    }

    // Token: 0x0600031D RID: 797 RVA: 0x0051E2A8 File Offset: 0x0051E2A8
    public void OpenUrl(string url)
    {
      try
      {
        ShellOpen.Start(url);
      }
      catch (Exception ex)
      {
        Log.Warn("app", "open url failed: " + ex.Message);
      }
    }

    // Token: 0x0600031E RID: 798 RVA: 0x0051E2EC File Offset: 0x0051E2EC
    public void SetPaused(bool paused)
    {
      this.Services.Settings.IsPaused = paused;
      if (paused)
      {
        this.Services.Companion.Cancel();
        this.Services.Presenter.Present(new NotchPresentation.Idle());
      }
    }

    // Token: 0x0600031F RID: 799 RVA: 0x0051E328 File Offset: 0x0051E328
    public async void Quit()
    {
      if (!this.quitting)
      {
        this.quitting = true;
        Log.Info("app", "quit");
        SingleInstance.Release();
        Stopwatch quitStarted = Stopwatch.StartNew();
        new Thread(delegate
        {
          Thread.Sleep(TimeSpan.FromSeconds(15L));
          string text2 = "shutdown";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(44, 1);
          defaultInterpolatedStringHandler2.AppendLiteral("still running ");
          defaultInterpolatedStringHandler2.AppendFormatted<double>(quitStarted.Elapsed.TotalSeconds, "0");
          defaultInterpolatedStringHandler2.AppendLiteral(" s after quit: terminating now");
          Log.Warn(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
          Program.Terminate();
        })
        {
          IsBackground = true,
          Name = "QuitWatchdog"
        }.Start();
        AppHost.<Quit>g__Step|130_0("letting go of keys a drive left down", delegate
        {
          ClickyScreenActuator.ReleaseStrandedKeys("quitting", true);
        });
        AppHost.<Quit>g__Step|130_0("stopping hotkeys", delegate
        {
          HotkeyRouter hotkeyRouter = this.hotkeys;
          if (hotkeyRouter == null)
          {
            return;
          }
          hotkeyRouter.Dispose();
        });
        AppHost.<Quit>g__Step|130_0("stopping the notch bridge", delegate
        {
          CompanionNotchBridge companionNotchBridge = this.bridge;
          if (companionNotchBridge == null)
          {
            return;
          }
          companionNotchBridge.Dispose();
        });
        AppHost.<Quit>g__Step|130_0("stopping proactive", delegate
        {
          ProactiveCoordinator proactive = this.Proactive;
          if (proactive == null)
          {
            return;
          }
          proactive.Dispose();
        });
        AppHost.<Quit>g__Step|130_0("stopping shortcuts", delegate
        {
          if (this.Services.Shortcuts != null)
          {
            this.Services.Shortcuts.Stop();
          }
        });
        AppHost.<Quit>g__Step|130_0("stopping the tray", new Action(this.DisposeAuxiliary));
        AppHost.<Quit>g__Step|130_0("stopping updates", delegate
        {
          UpdateService updateService = this.updates;
          if (updateService != null)
          {
            updateService.ApplyOnQuitIfReady();
          }
          UpdateService updateService2 = this.updates;
          if (updateService2 == null)
          {
            return;
          }
          updateService2.Dispose();
        });
        Log.Info("shutdown", "flushing analytics");
        try
        {
          await Task.WhenAny(ClickyAnalytics.FlushAsync(), Task.Delay(2000));
        }
        catch
        {
        }
        Log.Info("shutdown", "flushing crash reports");
        try
        {
          await CrashReporter.Current.FlushAsync(TimeSpan.FromSeconds(2L));
        }
        catch
        {
        }
        RealStack real = this.Real;
        if (real != null)
        {
          Log.Info("shutdown", "disposing services");
          try
          {
            await real.DisposeAsync();
          }
          catch (Exception ex)
          {
            Log.Warn("shutdown", ex.Message);
          }
        }
        string text = "shutdown";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
        defaultInterpolatedStringHandler.AppendLiteral("services stopped in ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(quitStarted.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms; exiting");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        Application.Current.Exit();
        Task.Delay(1500).ContinueWith(delegate(Task _)
        {
          Log.Info("shutdown", "exited");
          Program.Exit();
        }, TaskScheduler.Default);
      }
    }

    // Token: 0x06000320 RID: 800 RVA: 0x0051E360 File Offset: 0x0051E360
    public Task SayAtCursorAsync(string text, bool streamed = true, double holdSeconds = 6.0, CancellationToken ct = default(CancellationToken))
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return Task.CompletedTask;
      }
      if (AppHost.CursorLinesSuppressed())
      {
        Log.Info("cursor", "line beside the cursor skipped: presentation / fullscreen game");
        return Task.CompletedTask;
      }
      string text2 = "cursor";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
      defaultInterpolatedStringHandler.AppendLiteral("line beside the cursor (");
      defaultInterpolatedStringHandler.AppendFormatted(streamed ? "streamed" : "cue");
      defaultInterpolatedStringHandler.AppendLiteral(", hold ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(holdSeconds, "0.#");
      defaultInterpolatedStringHandler.AppendLiteral(" s): \"");
      defaultInterpolatedStringHandler.AppendFormatted(AppHost.Shorten(text, 80));
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      return this.Services.Cursor.ShowMessageAsync(text, streamed, holdSeconds, ct);
    }

    // Token: 0x06000321 RID: 801 RVA: 0x0051E42C File Offset: 0x0051E42C
    private void StartCursorModes()
    {
      ICursorOverlay cursor = this.Services.Cursor;
      cursor.ShakeToDetachEnabled = this.Services.Settings.ShakeToDetachEnabled;
      this.Services.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        cursor.ShakeToDetachEnabled = this.Services.Settings.ShakeToDetachEnabled;
      };
      cursor.DetachedCursorClicked += delegate
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OnDetachedCursorClicked));
      };
      ICursorOverlay cursor3 = cursor;
      Action action;
      if ((action = AppHost.<>O.<1>__TrackCursorDetached) == null)
      {
        action = (AppHost.<>O.<1>__TrackCursorDetached = new Action(ClickyAnalytics.TrackCursorDetached));
      }
      cursor3.CursorDetached += action;
      ICursorOverlay cursor2 = cursor;
      Action action2;
      if ((action2 = AppHost.<>O.<2>__TrackCursorDetachedAutoReattach) == null)
      {
        action2 = (AppHost.<>O.<2>__TrackCursorDetachedAutoReattach = new Action(ClickyAnalytics.TrackCursorDetachedAutoReattach));
      }
      cursor2.DetachedCursorAutoReattached += action2;
      if (this.Services.Settings.CursorDocked)
      {
        cursor.DockAsync(default(PointD));
      }
      RealStack real = this.Real;
      if (real != null)
      {
        real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
        {
          if (e.Kind == AgentTaskEventKind.StatusChanged && e.Text == "Starting")
          {
            this.DispatcherQueue.TryEnqueue(delegate
            {
              this.OnAgentEventForCursor(e);
            });
          }
        };
      }
    }

    // Token: 0x06000322 RID: 802 RVA: 0x0051E53C File Offset: 0x0051E53C
    private void OnDetachedCursorClicked()
    {
      ICompanionSession companion = this.Services.Companion;
      VoiceState state = companion.State;
      if (state != VoiceState.Idle)
      {
        if (state != VoiceState.Listening)
        {
          this.Services.Cursor.EndDetach();
          return;
        }
        Log.Info("cursor", "detached cursor clicked again: sending");
        ClickyAnalytics.TrackCursorDetachedClickStop();
        companion.EndPushToTalkAsync();
        this.Services.Cursor.EndDetach();
        return;
      }
      else
      {
        ClickyAnalytics.TrackCursorDetachedClickStart();
        RealStack real = this.Real;
        if (real != null && this.RoutedPressToMicrophonePermissionCard(real))
        {
          this.Services.Cursor.EndDetach();
          return;
        }
        Log.Info("cursor", "detached cursor clicked: listening");
        RealStack real2 = this.Real;
        if (real2 != null)
        {
          real2.MicrophoneStartCue.Play("textOpen", null);
        }
        else
        {
          this.Services.Audio.PlayEarcon("textOpen");
        }
        companion.BeginPushToTalkAsync();
        return;
      }
    }

    // Token: 0x06000323 RID: 803 RVA: 0x0051E624 File Offset: 0x0051E624
    public async void SetCursorDocked(bool dock)
    {
      if (dock != this.Services.Settings.CursorDocked && !this.cursorDockFlightInProgress)
      {
        if (dock)
        {
          if (AppHost.CursorLinesSuppressed())
          {
            this.Services.Settings.CursorDocked = true;
            await this.Services.Cursor.DockAsync(default(PointD));
            Log.Info("cursor", "hidden (no goodbye: visuals suppressed)");
          }
          else
          {
            this.cursorDockFlightInProgress = true;
            Log.Info("cursor", "hiding: saying goodbye");
            try
            {
              await this.Services.Cursor.SayGoodbyeAndDockAsync("okay see you <3");
            }
            catch (Exception ex)
            {
              Log.Warn("cursor", "goodbye: " + ex.Message);
            }
            this.Services.Settings.CursorDocked = true;
            this.cursorDockFlightInProgress = false;
            Log.Info("cursor", "hidden");
          }
        }
        else
        {
          this.Services.Settings.CursorDocked = false;
          this.cursorDockFlightInProgress = true;
          Log.Info("cursor", "showing again");
          try
          {
            await this.Services.Cursor.UndockAsync(default(PointD));
          }
          catch (Exception ex2)
          {
            Log.Warn("cursor", "show again: " + ex2.Message);
          }
          this.cursorDockFlightInProgress = false;
          Log.Info("cursor", "shown");
        }
      }
    }

    // Token: 0x06000324 RID: 804 RVA: 0x0051E664 File Offset: 0x0051E664
    private static bool CursorLinesSuppressed()
    {
      bool flag = QuietContext.ShellReportsFullscreen();
      if (flag)
      {
        QuietContext.NotificationState notificationState = QuietContext.QueryNotificationState();
        bool flag2 = notificationState - QuietContext.NotificationState.RunningD3DFullScreen <= 1;
        flag = flag2;
      }
      return flag;
    }

    // Token: 0x06000325 RID: 805 RVA: 0x0051E690 File Offset: 0x0051E690
    public Task PlayCursorWelcomeAsync()
    {
      Log.Info("cursor", "welcome line");
      return Task.WhenAny(this.Services.Cursor.PlayWelcomeAsync(), Task.Delay(6000));
    }

    // Token: 0x06000326 RID: 806 RVA: 0x0051E6C0 File Offset: 0x0051E6C0
    private void OnAgentEventForCursor(AgentTaskEvent e)
    {
      RealStack real = this.Real;
      AgentService agentService = ((real != null) ? real.Agents : null);
      if (agentService == null)
      {
        return;
      }
      AgentTaskDetail taskDetail = agentService.GetTaskDetail(e.TaskId);
      if (taskDetail == null || taskDetail.IsRoutineRun)
      {
        return;
      }
      if (e.Kind == AgentTaskEventKind.StatusChanged && e.Text == "Starting")
      {
        if (!(this.Services.Presenter.Presentation is NotchPresentation.TextResponse) && (this.Services.Companion.State != VoiceState.Idle || !this.lastTurnTyped))
        {
          return;
        }
        if (HeyClickyWindow.KeepsAgentLinesOffCursor)
        {
          return;
        }
        if (taskDetail.IsSentFromItsChat)
        {
          return;
        }
        string text = taskDetail.UserPrompts.LastOrDefault<string>() ?? taskDetail.Snapshot.Title;
        bool flag = taskDetail.UserPrompts.Count <= 1;
        this.ShowStartCueAsync(agentService, text, flag);
      }
    }

    // Token: 0x06000327 RID: 807 RVA: 0x0051E7A0 File Offset: 0x0051E7A0
    internal void NoteTurnKind(bool typed)
    {
      this.lastTurnTyped = typed;
    }

    // Token: 0x06000328 RID: 808 RVA: 0x0051E7AC File Offset: 0x0051E7AC
    private async Task ShowStartCueAsync(AgentService agents, string prompt, bool fresh)
    {
      string text = await agents.TextStartCueAsync(prompt, fresh, default(CancellationToken));
      string cue = text;
      if (!string.IsNullOrWhiteSpace(cue) && !HeyClickyWindow.KeepsAgentLinesOffCursor)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.SayAtCursorAsync(cue, false, 4.0, default(CancellationToken));
        });
      }
    }

    // Token: 0x06000329 RID: 809 RVA: 0x0051E808 File Offset: 0x0051E808
    private void ScheduleFinishCue(string taskId, string reply)
    {
      AppHost.<>c__DisplayClass143_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass143_0();
      CS$<>8__locals1.<>4__this = this;
      CancellationTokenSource cancellationTokenSource = this.pendingFinishCue;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.cts = (this.pendingFinishCue = new CancellationTokenSource());
      CS$<>8__locals1.text = AppHost.CursorText(reply);
      Task.Run(delegate
      {
        AppHost.<>c__DisplayClass143_0.<<ScheduleFinishCue>b__0>d <<ScheduleFinishCue>b__0>d;
        <<ScheduleFinishCue>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<ScheduleFinishCue>b__0>d.<>4__this = CS$<>8__locals1;
        <<ScheduleFinishCue>b__0>d.<>1__state = -1;
        <<ScheduleFinishCue>b__0>d.<>t__builder.Start<AppHost.<>c__DisplayClass143_0.<<ScheduleFinishCue>b__0>d>(ref <<ScheduleFinishCue>b__0>d);
        return <<ScheduleFinishCue>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600032A RID: 810 RVA: 0x0051E864 File Offset: 0x0051E864
    private static string CursorText(string markdown)
    {
      string text = Regex.Replace(markdown, "```[\\s\\S]*?```", " ");
      text = Regex.Replace(text, "!\\[[^\\]]*\\]\\([^)]*\\)", " ");
      text = Regex.Replace(text, "\\[([^\\]]+)\\]\\([^)]*\\)", "$1");
      text = Regex.Replace(text, "[*_`#>]+", "");
      text = Regex.Replace(text, "^\\s*[-•]\\s+", "", RegexOptions.Multiline);
      text = Regex.Replace(text, "\\s+", " ").Trim();
      if (text.Length <= 280)
      {
        return text;
      }
      int num = text.LastIndexOfAny(new char[] { '.', '!', '?' }, 279);
      if (num <= 120)
      {
        return text.Substring(0, 279).TrimEnd() + "…";
      }
      return text.Substring(0, num + 1);
    }

    // Token: 0x0600032B RID: 811 RVA: 0x0051E934 File Offset: 0x0051E934
    private static string Shorten(string s, int max)
    {
      if (s.Length > max)
      {
        return s.Substring(0, max - 1) + "…";
      }
      return s;
    }

    // Token: 0x0600032C RID: 812 RVA: 0x0051E958 File Offset: 0x0051E958
    public void DebugLaunchOnboardingWebsiteAgent()
    {
      this.LaunchOnboardingDemoAgent(StarterCast.Shippy, "Can you make me a website for the beautiful person watching this video right now, and open it in the browser when you're done?", AppHost.ShippyPrompt);
    }

    // Token: 0x0600032D RID: 813 RVA: 0x0051E970 File Offset: 0x0051E970
    public void DebugLaunchOnboardingSpaceXReportAgent()
    {
      this.LaunchOnboardingDemoAgent(StarterCast.ResearchBuddy, "I'm the CEO of this rocket company you see on my screen. Can you research my competitors and save it as a PDF?", AppHost.ResearchBuddyPrompt);
    }

    // Token: 0x0600032E RID: 814 RVA: 0x0051E988 File Offset: 0x0051E988
    public async void StartHomeTourForDebug()
    {
      if (!this.TourHoldsHome && this.Real != null)
      {
        Log.Info("tour", "debug: Start Home tour");
        this.tour = new HomeTour(this, this.Interview)
        {
          IsDebugPreview = true
        };
        this.CursorCompanion.WatchEscape(true);
        try
        {
          TaskAwaiter<bool> taskAwaiter = this.tour.RunAsync().GetAwaiter();
          if (!taskAwaiter.IsCompleted)
          {
            await taskAwaiter;
            TaskAwaiter<bool> taskAwaiter2;
            taskAwaiter = taskAwaiter2;
            taskAwaiter2 = default(TaskAwaiter<bool>);
          }
          if (taskAwaiter.GetResult() && !this.tour.WasAborted)
          {
            this.StreamHoldToTalkLineAfterOnboarding();
          }
        }
        catch (Exception ex)
        {
          Log.Warn("tour", "debug Home tour: " + ex.Message);
        }
        finally
        {
          this.tour = null;
          this.CursorCompanion.WatchEscape(false);
          HeaderPills.RaiseChanged();
          this.SyncRestingMark();
          this.lastAgentSignature = null;
          this.SyncAgentActivity();
        }
      }
    }

    // Token: 0x0600032F RID: 815 RVA: 0x0051E9C0 File Offset: 0x0051E9C0
    public void TestClickyInterview()
    {
      this.OpenTutorialForDebug(TutorialWindow.Step.ClickyInterview);
    }

    // Token: 0x06000330 RID: 816 RVA: 0x0051E9CC File Offset: 0x0051E9CC
    public void JumpToTutorialFinale()
    {
      StarterCast.Unlock();
      this.OpenTutorialForDebug(TutorialWindow.Step.Finale);
    }

    // Token: 0x06000331 RID: 817 RVA: 0x0051E9DC File Offset: 0x0051E9DC
    public void OpenTutorialForDebug(TutorialWindow.Step step)
    {
      string text = "tutorial";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
      defaultInterpolatedStringHandler.AppendLiteral("debug: opening the tour at ");
      defaultInterpolatedStringHandler.AppendFormatted<TutorialWindow.Step>(step);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      DrawnOnboardingNotch.DismissPeek(this);
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsRunning)
      {
        tutorialWindow.BringForward();
        return;
      }
      this.EndNewAgentsUiDemoRun();
      if (this.tutorial == null)
      {
        this.tutorial = this.CreateTutorial();
      }
      this.tutorial.Show(step, false);
    }

    // Token: 0x06000332 RID: 818 RVA: 0x0051EA60 File Offset: 0x0051EA60
    public void DeleteAllLocalClickys()
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.DismissNotchAgentPeek();
      }
      IReadOnlyList<string> workspaces;
      try
      {
        workspaces = real.Agents.DeleteAllLocalClickys();
      }
      catch (InvalidOperationException ex)
      {
        this.SayAtCursorAsync(ex.Message.ToLowerInvariant(), true, 3.0, default(CancellationToken));
        return;
      }
      ClickyRoster.Shared.ForgetAll();
      string root = Path.GetFullPath(real.Agents.ProjectsRoot);
      Task.Run(delegate
      {
        foreach (string text in workspaces)
        {
          if (Path.GetFullPath(text).StartsWith(root, StringComparison.OrdinalIgnoreCase))
          {
            try
            {
              FileSystem.DeleteDirectory(text, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
              Log.Info("debug", text + " → Recycle Bin");
            }
            catch (Exception ex2)
            {
              Log.Warn("debug", "recycle " + text + ": " + ex2.Message);
            }
          }
        }
      });
    }

    // Token: 0x06000333 RID: 819 RVA: 0x0051EB10 File Offset: 0x0051EB10
    public void SetUseProductionBackend(bool on)
    {
      try
      {
        if (on)
        {
          File.WriteAllText(AppBundleConfiguration.UseProductionBackendFlagPath, "on\n");
        }
        else if (File.Exists(AppBundleConfiguration.UseProductionBackendFlagPath))
        {
          File.Delete(AppBundleConfiguration.UseProductionBackendFlagPath);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "use production backend: " + ex.Message);
        return;
      }
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
      defaultInterpolatedStringHandler.AppendLiteral("use production backend = ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(on);
      defaultInterpolatedStringHandler.AppendLiteral("; relaunching");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.Relaunch();
    }

    // Token: 0x17000072 RID: 114
    // (get) Token: 0x06000334 RID: 820 RVA: 0x0051EBB8 File Offset: 0x0051EBB8
    // (set) Token: 0x06000335 RID: 821 RVA: 0x0051EBC0 File Offset: 0x0051EBC0
    [Nullable(2)]
    public ArcDock Dock
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06000336 RID: 822 RVA: 0x0051EBCC File Offset: 0x0051EBCC
    private void SyncPointerOnNotch()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      corePresenter.SetPointerInside(this.notchWindowHovered || this.deckPointerOnNotch);
    }

    // Token: 0x06000337 RID: 823 RVA: 0x0051EBF0 File Offset: 0x0051EBF0
    private void StartDock(RealStack real)
    {
      AppHost.<>c__DisplayClass166_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass166_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.agents = CS$<>8__locals1.real.Agents;
      CS$<>8__locals1.conversation = CompanionConversationBuffer.Shared;
      CS$<>8__locals1.real.Companion.TurnFinished += delegate([Nullable(2)] object _, CompanionTurnFinish finish)
      {
        string userWords = finish.UserWords;
        if (userWords != null)
        {
          InterviewController interview = CS$<>8__locals1.<>4__this.Interview;
          if (interview == null || !interview.IsActive)
          {
            CS$<>8__locals1.conversation.Append(userWords, finish.SpokenText);
          }
        }
      };
      CS$<>8__locals1.agents.CompanionTurnsSinceLastMessage = new Func<string, IReadOnlyList<ValueTuple<string, string>>>(CS$<>8__locals1.conversation.SinceLastMessage);
      CS$<>8__locals1.agents.CompanionTurnsForFreshLaunch = new Func<string, IReadOnlyList<ValueTuple<string, string>>>(CS$<>8__locals1.conversation.ConsumeForFreshLaunch);
      AppHost.<>c__DisplayClass166_0 CS$<>8__locals2 = CS$<>8__locals1;
      ArcDockHost arcDockHost = new ArcDockHost();
      arcDockHost.NotchSurfacePx = delegate
      {
        ValueTuple<int, int, int, int> valueTuple = CS$<>8__locals1.<>4__this.NotchUi.SurfaceRectPx();
        if (valueTuple.Item3 <= 0)
        {
          return null;
        }
        return new ValueTuple<int, int, int, int>?(valueTuple);
      };
      arcDockHost.NotchIsExpanded = delegate
      {
        NotchPresenter corePresenter2 = CS$<>8__locals1.<>4__this.CorePresenter;
        return corePresenter2 != null && corePresenter2.IsShowingNotchAgentPeek;
      };
      arcDockHost.NotchIsOpenOnHome = delegate
      {
        NotchPresenter corePresenter3 = CS$<>8__locals1.<>4__this.CorePresenter;
        return corePresenter3 != null && corePresenter3.IsShowingNotchAgentPeek && corePresenter3.Tabs.Selected == NotchTab.Home;
      };
      arcDockHost.NotchTabIsHome = delegate
      {
        NotchPresenter corePresenter4 = CS$<>8__locals1.<>4__this.CorePresenter;
        return (((corePresenter4 != null) ? new NotchTab?(corePresenter4.Tabs.Selected) : null) ?? NotchTab.Home) == NotchTab.Home;
      };
      arcDockHost.NotchHoldsOpenWithoutArc = delegate
      {
        NotchPresenter corePresenter5 = CS$<>8__locals1.<>4__this.CorePresenter;
        return corePresenter5 != null && corePresenter5.HoldsNotchOpenWithoutArc;
      };
      arcDockHost.SetNotchHeldOpenByArc = delegate(bool held)
      {
        NotchPresenter corePresenter6 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter6 == null)
        {
          return;
        }
        corePresenter6.SetNotchHeldOpenByArc(held);
      };
      arcDockHost.OpenNotchHome = new Action(this.OpenNotchHome);
      arcDockHost.DismissNotch = delegate
      {
        NotchPresenter corePresenter7 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter7 == null)
        {
          return;
        }
        corePresenter7.DismissNotchAgentPeek();
      };
      arcDockHost.OpenAgentInWindow = delegate(string agentKey)
      {
        CS$<>8__locals1.<>4__this.ExpandNotchIntoWindow("arc_card", agentKey);
      };
      arcDockHost.IsFullscreenOnDockDisplay = () => CS$<>8__locals1.<>4__this.NotchUi.IsAmbientHidden;
      arcDockHost.HasLatestReply = delegate(string jobId, string slug)
      {
        ChatHandOffs handOffs = NotchChat.HandOffs;
        return ((handOffs != null) ? handOffs.LatestReplyEntryId(jobId, slug) : null) != null;
      };
      arcDockHost.ShowFullReply = delegate(string jobId, string slug)
      {
        NotchPresenter corePresenter8 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter8 != null)
        {
          ChatHandOffs handOffs2 = NotchChat.HandOffs;
          string text = ((handOffs2 != null) ? handOffs2.LatestReplyEntryId(jobId, slug) : null);
          if (text != null)
          {
            corePresenter8.Tabs.ShowFullReply(text, null);
            return;
          }
        }
      };
      arcDockHost.ShowChat = new Action<string, string>(this.ShowInChat);
      arcDockHost.RequestComposerFocus = delegate
      {
        NotchPresenter corePresenter9 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter9 == null)
        {
          return;
        }
        corePresenter9.Tabs.RequestComposerFocus();
      };
      arcDockHost.LatestChatEventAt = delegate(string target)
      {
        ChatHandOffs handOffs3 = NotchChat.HandOffs;
        if (handOffs3 == null)
        {
          return null;
        }
        ChatHandOff chatHandOff = handOffs3.Entries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == target);
        if (chatHandOff == null)
        {
          return null;
        }
        return new DateTimeOffset?(chatHandOff.CreatedAt);
      };
      arcDockHost.LatestReplyAnswer = delegate(string jobId, string slug)
      {
        ChatHandOffs handOffs4 = NotchChat.HandOffs;
        if (handOffs4 != null)
        {
          string text2 = handOffs4.LatestReplyEntryId(jobId, slug);
          if (text2 != null)
          {
            ChatHandOff chatHandOff2 = handOffs4.Entry(text2);
            JsonObject jsonObject = ((chatHandOff2 != null) ? chatHandOff2.Answer : null);
            if (jsonObject != null)
            {
              return ClickyAnswer.Parse(jsonObject.ToJsonString(null));
            }
          }
        }
        return null;
      };
      arcDockHost.OpensInNotchDocumentViewer = (string path) => !ArtifactPaths.IsWebLink(path) && ArtifactPaths.SupportsInline(path);
      arcDockHost.OpenInNotchViewer = ([Nullable(1)] string path, string slug, string threadId) => !ArtifactPaths.IsWebLink(path) && CS$<>8__locals1.<>4__this.OpenAgentDocumentInViewer(path, threadId, slug);
      arcDockHost.PresentNotchUpdate = delegate(string railKey, string kind)
      {
        NotchPresenter corePresenter10 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter10 != null)
        {
          NotchUpdates notchUpdates = corePresenter10.Updates;
          NotchUpdateKind notchUpdateKind;
          if (!(kind == "needs_you"))
          {
            if (!(kind == "failed"))
            {
              notchUpdateKind = NotchUpdateKind.Done;
            }
            else
            {
              notchUpdateKind = NotchUpdateKind.Failed;
            }
          }
          else
          {
            notchUpdateKind = NotchUpdateKind.NeedsYou;
          }
          notchUpdates.Present(railKey, notchUpdateKind);
        }
      };
      arcDockHost.DismissNotchUpdate = delegate(string railKey)
      {
        NotchPresenter corePresenter11 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter11 == null)
        {
          return;
        }
        corePresenter11.Updates.Dismiss(railKey);
      };
      arcDockHost.HasCompletedOnboarding = () => CS$<>8__locals1.<>4__this.HasCompletedOnboardingNow;
      arcDockHost.OnboardingHidesDock = () => CS$<>8__locals1.<>4__this.introSequenceActive || (CS$<>8__locals1.real.Tutorial.IsEngaged && !DrawnOnboardingNotchShowcase.Shared.ShowsDock);
      arcDockHost.SuppressesVisualSurfaces = () => CS$<>8__locals1.<>4__this.Services.Settings.IsPaused;
      arcDockHost.IsTourRunning = () => CS$<>8__locals1.<>4__this.TourHoldsHome;
      arcDockHost.PlaysSounds = () => CS$<>8__locals1.real.Core.Settings.Read<bool>((AppSettings s) => s.DrawingSoundsEnabled);
      arcDockHost.VoiceHoldDescription = () => CS$<>8__locals1.<>4__this.Services.Settings.PushToTalkDisplay;
      arcDockHost.IsListening = () => CS$<>8__locals1.<>4__this.Services.Companion.State == VoiceState.Listening;
      arcDockHost.UnreadClickySlugs = () => ClickyReadMarks.Unread(CS$<>8__locals1.agents);
      Action<string> action;
      if ((action = AppHost.<>O.<3>__MarkRead) == null)
      {
        action = (AppHost.<>O.<3>__MarkRead = new Action<string>(ClickyReadMarks.MarkRead));
      }
      arcDockHost.MarkClickyRead = action;
      Func<string, string> func;
      if ((func = AppHost.<>O.<4>__NoteTitle) == null)
      {
        func = (AppHost.<>O.<4>__NoteTitle = new Func<string, string>(ClickyArcPlanNotes.NoteTitle));
      }
      arcDockHost.NoteTitle = func;
      arcDockHost.NoteChecklistPlan = (string notePath) => ClickyArcPlanNotes.Read(CS$<>8__locals1.agents.LiveSteps, notePath);
      arcDockHost.StartWaitingJob = delegate(ClickyJob job)
      {
        Guid? suggestionCardId = job.SuggestionCardId;
        if (suggestionCardId != null)
        {
          Guid valueOrDefault = suggestionCardId.GetValueOrDefault();
          ISet<Guid> set = CS$<>8__locals1.<>4__this.LiveSuggestionCardIds();
          if (set != null && set.Contains(valueOrDefault))
          {
            ProactiveCoordinator proactive = CS$<>8__locals1.<>4__this.Proactive;
            return ((proactive != null) ? proactive.Approve(valueOrDefault, null, false, "button") : null) != null;
          }
        }
        return false;
      };
      arcDockHost.LiveSuggestionCardIds = new Func<ISet<Guid>>(this.LiveSuggestionCardIds);
      CS$<>8__locals2.host = arcDockHost;
      CS$<>8__locals1.dock = (this.Dock = new ArcDock(CS$<>8__locals1.agents, CS$<>8__locals1.host, () => CS$<>8__locals1.real.JobReports, this.ExcludeFromCapture));
      HeyClickyWindow.Shared.Configure(this.NotchUi, CS$<>8__locals1.dock);
      ArcDockView.NotchHwnd = () => CS$<>8__locals1.<>4__this.NotchUi.Window.Hwnd;
      this.NotchUi.IsNonTypingWindow = (IntPtr hwnd) => hwnd == CS$<>8__locals1.dock.DeckHwnd;
      this.NotchUi.RaisedToTop += CS$<>8__locals1.dock.KeepAboveNotch;
      CS$<>8__locals1.dock.PointerOnNotchChanged += delegate(bool on)
      {
        CS$<>8__locals1.<>4__this.deckPointerOnNotch = on;
        CS$<>8__locals1.<>4__this.SyncPointerOnNotch();
      };
      CS$<>8__locals1.dock.SubmitFact = (string fact) => CS$<>8__locals1.real.Companion.SubmitFactAsync(fact, false);
      ShortcutMonitor shortcuts = CS$<>8__locals1.real.Shortcuts;
      if (shortcuts != null)
      {
        shortcuts.ConsumesArrow = delegate(int step)
        {
          if (CS$<>8__locals1.<>4__this.Services.Settings.IsPaused || !CS$<>8__locals1.dock.ArrowKeysTurnDeck)
          {
            return false;
          }
          CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
          {
            CS$<>8__locals1.dock.TurnDeckWithArrowKey(step);
          });
          return true;
        };
      }
      IReadOnlyList<string> readOnlyList = CS$<>8__locals1.agents.TakeLegacyClickyMigration();
      if (readOnlyList != null)
      {
        ClickyReadMarks.MarkAllRead(readOnlyList);
      }
      IReadOnlyList<string> readOnlyList2 = CS$<>8__locals1.agents.TakeLegacyClickysToDock();
      if (readOnlyList2 != null)
      {
        ClickyReadMarks.MarkAllRead(readOnlyList2);
        CS$<>8__locals1.dock.DockLegacyClickys(readOnlyList2);
      }
      CS$<>8__locals1.agents.BackfillLegacyClickyWorkIfNeeded();
      NotchExtensions.HomeDock = new ArcNotchHomeDock(CS$<>8__locals1.dock, delegate
      {
        NotchPresenter corePresenter12 = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter12 == null)
        {
          return null;
        }
        return new NotchTab?(corePresenter12.Tabs.Selected);
      });
      DrawnOnboardingNotchShowcase shared = DrawnOnboardingNotchShowcase.Shared;
      DrawnOnboardingNotchShowcase.SetDockTourShowcase = delegate(bool on)
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          CS$<>8__locals1.dock.IsTourShowcase = on;
        });
      };
      shared.Changed += delegate
      {
        CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(CS$<>8__locals1.dock.Refresh));
      };
      CS$<>8__locals1.real.Tutorial.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs e)
      {
        string propertyName = e.PropertyName;
        bool flag = propertyName == "IsEngaged" || propertyName == "Phase";
        if (flag)
        {
          CS$<>8__locals1.dock.NoteMembershipInputsChanged();
        }
      };
      CS$<>8__locals1.real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        CS$<>8__locals1.dock.NoteMembershipInputsChanged();
      };
      this.Services.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        CS$<>8__locals1.dock.NoteMembershipInputsChanged();
      };
      ClickyTourJobCards.ArcCardFactory = delegate(TourJobCardContent card, bool showsCover)
      {
        ArcCardView arcCardView = new ArcCardView(CS$<>8__locals1.dock, new ArcCardContent
        {
          RailKey = card.RailKey,
          Title = card.Title,
          Summary = card.Summary,
          State = ArcCardState.Suggested,
          Paper = new ArcPaper((PaperStock)card.Paper)
        });
        arcCardView.SetPresentation(showsCover, false, false);
        return arcCardView;
      };
      NotchExtensions.RestingMarkState = delegate
      {
        if (!CS$<>8__locals1.<>4__this.EdgeDockIsRunning)
        {
          return new NotchRestingMarkState(0, "idle");
        }
        ValueTuple<int, string> valueTuple2 = CS$<>8__locals1.dock.RestingMark();
        int item = valueTuple2.Item1;
        string item2 = valueTuple2.Item2;
        return new NotchRestingMarkState(item, item2);
      };
      NotchExtensions.LiveWork = new ArcChatLiveWork(CS$<>8__locals1.dock, CS$<>8__locals1.agents, new WorkChatLiveWork(CS$<>8__locals1.agents, delegate
      {
        RealStack real2 = CS$<>8__locals1.<>4__this.Real;
        if (real2 == null)
        {
          return null;
        }
        return real2.JobReports;
      }));
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
        {
          CS$<>8__locals1.dock.NotchChanged();
        };
        corePresenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
        {
          CS$<>8__locals1.dock.NoteMembershipInputsChanged();
        };
        ArcVoicePill arcPill = new ArcVoicePill(corePresenter, CS$<>8__locals1.dock, this.NotchUi, this.ExcludeFromCapture);
        corePresenter.VoicePillChanged += delegate
        {
          CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(arcPill.Sync));
        };
        corePresenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
        {
          CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(arcPill.Sync));
        };
        corePresenter.Tabs.SelectionChanged += delegate(NotchTab tab)
        {
          if (CS$<>8__locals1.<>4__this.DispatcherQueue.HasThreadAccess)
          {
            base.<StartDock>g__NoteTab|64(tab);
            return;
          }
          CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
          {
            CS$<>8__locals1.<StartDock>g__NoteTab|64(tab);
          });
        };
      }
      ClickyBrowserBridge.Shared.WorkHandler = delegate(string thread, string tool, JsonObject arguments)
      {
        if (tool == "read_job")
        {
          return CS$<>8__locals1.agents.ReadJobAsync(AppHost.<StartDock>g__Text|166_67(arguments["job_id"]) ?? "", AppHost.<StartDock>g__Text|166_67(arguments["detail"]) ?? "report");
        }
        JsonValue jsonValue = arguments["limit"] as JsonValue;
        int num2;
        int num = ((jsonValue != null && jsonValue.TryGetValue<int>(out num2)) ? num2 : 5);
        return CS$<>8__locals1.agents.FindJobsAsync(AppHost.<StartDock>g__Text|166_67(arguments["query"]) ?? "", num, true, thread);
      };
      AgentDriveJobs agentDriveJobs = new AgentDriveJobs(CS$<>8__locals1.agents, delegate(string taskId, string reason)
      {
        CS$<>8__locals1.<>4__this.PresentEdgeDockWindow(CS$<>8__locals1.agents, taskId, reason);
      }, delegate(string threadId, string eventText)
      {
        Action<string, string> noteClickyEventForOneBrain = CS$<>8__locals1.<>4__this.NoteClickyEventForOneBrain;
        if (noteClickyEventForOneBrain == null)
        {
          return;
        }
        noteClickyEventForOneBrain(threadId, eventText);
      }, new Func<string, Task>(this.AnnounceLineAsync));
      if (this.ClickyDrive != null)
      {
        AgentDriveConsent consent = this.AttachAgentJobs(agentDriveJobs);
        CS$<>8__locals1.dock.DriveConsent = consent;
        ClickyBrowserBridge.Shared.MacStepHandler = new Func<string, string, string, string, Task<JsonObject>>(consent.AnswerMacStepToolCallAsync);
        CS$<>8__locals1.real.ToolHost.ApproveComputerUseHook = new Action<string>(consent.ApproveComputerUse);
        CS$<>8__locals1.real.ToolHost.DeclineComputerUseHook = new Action<string>(consent.DeclineComputerUse);
        CS$<>8__locals1.real.JobReports.IsPcStepMomentInProgress = () => consent.IsPcStepMomentInProgress;
        CS$<>8__locals1.agents.IsClickyDriveAvailable = () => consent.AgentClickyDriveIsAvailable;
        CS$<>8__locals1.agents.TakeUnansweredComputerUseStep = new Func<string, string>(MacStepWaiters.Shared.TakeUnansweredStep);
        CS$<>8__locals1.agents.WasComputerUseStepRecentlyDeclined = new Func<string, string, bool>(MacStepWaiters.Shared.WasRecentlyDeclined);
        CS$<>8__locals1.agents.ForgetDeclinedComputerUseSteps = new Action<string>(MacStepWaiters.Shared.ForgetDeclined);
        consent.ShowCursorLine = delegate(string line)
        {
          CS$<>8__locals1.<>4__this.SayAtCursorAsync(line, true, 5.0, default(CancellationToken));
        };
        this.AnnounceComputerUseAsk = delegate(string taskId)
        {
          consent.AnnounceAgentAsksToUsePcIfNeeded(new DriveJob(CS$<>8__locals1.agents, taskId), "end_of_turn");
        };
      }
      ChromeSetupFlow.Shared.Jobs = new ChromeSetupJobs(CS$<>8__locals1.agents, delegate(string railKey)
      {
        Action<string, string> presentNotchUpdate = CS$<>8__locals1.host.PresentNotchUpdate;
        if (presentNotchUpdate == null)
        {
          return;
        }
        presentNotchUpdate(railKey, "needs_you");
      });
      ClickyBrowserBridge.Shared.JobFolderForThread = delegate(string thread)
      {
        ClickyJob clickyJob = CS$<>8__locals1.agents.Jobs.JobForThread(thread);
        if (clickyJob == null)
        {
          return null;
        }
        return clickyJob.FolderPath;
      };
      AgentToolHooks.HoldJobForChrome = (AgentToolLaunch launch) => CS$<>8__locals1.<>4__this.HoldJobForChrome(CS$<>8__locals1.agents, launch, CS$<>8__locals1.real.Companion.CurrentTurnSpokenLines);
      AgentToolHooks.AnswerChromeSetup = ([Nullable(2)] string threadId, string choice) => CS$<>8__locals1.<>4__this.OnUiAsync<string>(delegate
      {
        ChromeSetupFlow shared2 = ChromeSetupFlow.Shared;
        ChromeSetupFlow.Target target = shared2.TargetFor(threadId);
        if (target != null)
        {
          ChromeSetupFlow.Choice? choice = ChromeSetupFlow.Parse(choice);
          if (choice != null)
          {
            ChromeSetupFlow.Choice valueOrDefault2 = choice.GetValueOrDefault();
            ChromeSetupFlow.Choice choice2 = ((valueOrDefault2 == ChromeSetupFlow.Choice.DoItForMe && !shared2.CanDoItForThem) ? ChromeSetupFlow.Choice.ShowMeHow : valueOrDefault2);
            shared2.Choose(choice2, target);
            return ChromeSetupFlow.Raw(choice2);
          }
        }
        return null;
      });
      AgentToolHooks.StartWaitingJobNamed = (string label, string task) => CS$<>8__locals1.<>4__this.OnUiAsync<string>(() => CS$<>8__locals1.<>4__this.StartWaitingJobNamed(CS$<>8__locals1.dock, CS$<>8__locals1.agents, label, task));
      FlowHooks.EdgeDockFocusNote = (bool typed) => CS$<>8__locals1.dock.FocusNoteForOneBrainTurn(typed);
      FlowHooks.TakeSpokenWordsDirectly = delegate(string words)
      {
        string key = CS$<>8__locals1.dock.TakeWindowDirectVoiceKey();
        if (key == null)
        {
          return Task.FromResult<bool>(false);
        }
        TaskCompletionSource<bool> sent = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        if (!CS$<>8__locals1.<>4__this.DispatcherQueue.TryEnqueue(delegate
        {
          try
          {
            bool flag2 = CS$<>8__locals1.<>4__this.SendWindowAgentMessageDirectly(words, Array.Empty<string>(), key, true);
            if (flag2)
            {
              CS$<>8__locals1.dock.DropOneBrainFocusedWindow();
            }
            sent.TrySetResult(flag2);
          }
          catch (Exception ex)
          {
            sent.TrySetException(ex);
          }
        }))
        {
          sent.TrySetResult(false);
        }
        return sent.Task;
      };
      if (this.hotkeys != null)
      {
        Func<bool> allows = this.hotkeys.AllowsPushToTalk;
        this.hotkeys.AllowsPushToTalk = delegate
        {
          if (!allows())
          {
            return false;
          }
          if (!CS$<>8__locals1.dock.NotePushToTalkDown() && !CS$<>8__locals1.real.Tutorial.IsEngaged)
          {
            string text3 = CS$<>8__locals1.<>4__this.NotchFullReplyFollowUpKey(CS$<>8__locals1.agents);
            if (text3 != null)
            {
              CS$<>8__locals1.dock.NoteFullReplyVoiceFollowUp(text3);
            }
            else
            {
              string text4 = HeyClickyWindow.Shared.AgentKeyUnderPointer();
              if (text4 != null)
              {
                CS$<>8__locals1.dock.NoteWindowVoiceFollowUp(text4);
              }
            }
          }
          return true;
        };
      }
      this.Services.Companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (CS$<>8__locals1.<>4__this.Services.Companion.State == VoiceState.Idle)
        {
          CS$<>8__locals1.dock.ClearVoiceFollowUp();
        }
        CS$<>8__locals1.dock.NoteVoiceStateChanged();
      };
      CS$<>8__locals1.agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue = CS$<>8__locals1.<>4__this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = CS$<>8__locals1.<>9__76) == null)
        {
          dispatcherQueueHandler = (CS$<>8__locals1.<>9__76 = delegate
          {
            CS$<>8__locals1.<>4__this.SyncLaunchFlights(CS$<>8__locals1.agents);
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
      this.SyncLaunchFlights(CS$<>8__locals1.agents);
      CS$<>8__locals1.dock.Start();
      Log.Info("dock", "the agent dock hangs from the notch's Home tab");
    }

    // Token: 0x06000338 RID: 824 RVA: 0x0051F508 File Offset: 0x0051F508
    [return: Nullable(2)]
    private string NotchFullReplyFollowUpKey(AgentService agents)
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null || !corePresenter.IsShowingNotchAgentPeek)
      {
        return null;
      }
      NotchTabs tabs = corePresenter.Tabs;
      string expandedReplyEntryId = tabs.ExpandedReplyEntryId;
      ChatHandOff chatHandOff;
      if (expandedReplyEntryId == null)
      {
        if (tabs.Selected == NotchTab.Document)
        {
          string documentPath = tabs.DocumentPath;
          if (documentPath != null)
          {
            chatHandOff = NotchDocumentViewer.ReplyEntry(documentPath, tabs.DocumentThreadId);
            goto IL_005E;
          }
        }
        chatHandOff = null;
      }
      else
      {
        ChatHandOffs handOffs = NotchChat.HandOffs;
        chatHandOff = ((handOffs != null) ? handOffs.Entry(expandedReplyEntryId) : null);
      }
      IL_005E:
      ChatHandOff chatHandOff2 = chatHandOff;
      if (chatHandOff2 == null)
      {
        return null;
      }
      string jobId = chatHandOff2.JobId;
      if (jobId != null)
      {
        ClickyJob clickyJob = agents.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          return clickyJob.RailKey;
        }
      }
      string clickySlug = chatHandOff2.ClickySlug;
      if (clickySlug != null && agents.FindClicky(clickySlug) != null)
      {
        return clickySlug;
      }
      return null;
    }

    // Token: 0x06000339 RID: 825 RVA: 0x0051F5BC File Offset: 0x0051F5BC
    [return: Nullable(2)]
    private string HoldJobForChrome(AgentService agents, AgentToolLaunch launch, IReadOnlyList<string> spokenLines)
    {
      if (!ChromeSetupFlow.Shared.HoldsNewWebsiteJobs || !AgentToolHooks.BrainAskedAboutChrome(spokenLines))
      {
        return null;
      }
      ClickyJob job = agents.Jobs.CreateJob(launch.NewClickyName ?? launch.Prompt, launch.Prompt, agents.ProjectsRoot, true);
      Action<Task<string>> <>9__2;
      Action<bool> <>9__1;
      this.DispatcherQueue.TryEnqueue(delegate
      {
        ChromeSetupFlow shared = ChromeSetupFlow.Shared;
        string id = job.Id;
        Action<bool> action;
        if ((action = <>9__1) == null)
        {
          action = (<>9__1 = delegate(bool withoutChrome)
          {
            if (withoutChrome)
            {
              agents.Jobs.SetSkipsChrome(job.Id);
            }
            Task<string> task = agents.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = launch.Prompt,
              JobId = job.Id,
              LaunchSource = launch.LaunchSource,
              ImagesJpeg = launch.ImagesJpeg,
              AttachedFilePaths = launch.AttachedFilePaths,
              IsGeneratedTaskBrief = launch.IsGeneratedTaskBrief,
              LaunchContext = (withoutChrome ? (launch.LaunchContext + " They chose not to connect Chrome for this job: don't ask about Chrome again. For a step on a website they're signed into, call mac_step and HeyClicky does it on their screen while they watch.") : launch.LaunchContext),
              BuildsOnJobIds = launch.BuildsOnJobIds
            }, default(CancellationToken));
            string text = "job_action";
            string text2 = null;
            string text3 = null;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["action"] = "started";
            ClickyAnalytics.TrackProductEvent(text, text2, text3, jsonObject);
            Action<Task<string>> action2;
            if ((action2 = <>9__2) == null)
            {
              action2 = (<>9__2 = delegate(Task<string> t)
              {
                if (t.IsCompletedSuccessfully)
                {
                  this.DispatcherQueue.TryEnqueue(delegate
                  {
                    this.NoteHandOffStarted(agents, t.Result, launch.Prompt, false, 0);
                  });
                  return;
                }
                string text4 = "chrome";
                string text5 = "held job \"";
                string title = job.Title;
                string text6 = "\" didn't start: ";
                AggregateException exception = t.Exception;
                Log.Warn(text4, text5 + title + text6 + ((exception != null) ? exception.GetBaseException().Message : null));
              });
            }
            task.ContinueWith(action2, TaskScheduler.Default);
          });
        }
        shared.Hold(id, action);
      });
      return job.Title;
    }

    // Token: 0x0600033A RID: 826 RVA: 0x0051F664 File Offset: 0x0051F664
    [NullableContext(2)]
    private ISet<Guid> LiveSuggestionCardIds()
    {
      ProactiveCoordinator proactive = this.Proactive;
      if (proactive == null)
      {
        return null;
      }
      return proactive.VisibleCards.Select<ProactiveSuggestionCard, Guid>((ProactiveSuggestionCard c) => c.Id).ToHashSet<Guid>();
    }

    // Token: 0x0600033B RID: 827 RVA: 0x0051F6A0 File Offset: 0x0051F6A0
    [return: Nullable(2)]
    private string StartWaitingJobNamed(ArcDock dock, AgentService agents, string label, string task)
    {
      string name = label.Trim();
      if (name.Length == 0)
      {
        return null;
      }
      ClickyJob clickyJob = dock.ShelfJobs().FirstOrDefault<ClickyJob>((ClickyJob j) => j.IsWaitingToStart && string.Equals(j.Title.Trim(), name, StringComparison.OrdinalIgnoreCase));
      Guid? guid = ((clickyJob != null) ? clickyJob.SuggestionCardId : null);
      if (guid == null)
      {
        return null;
      }
      Guid valueOrDefault = guid.GetValueOrDefault();
      string text = task.Trim();
      string text2 = ((text.Length == 0 || string.Equals(text, name, StringComparison.OrdinalIgnoreCase)) ? null : text);
      ProactiveCoordinator proactive = this.Proactive;
      if (((proactive != null) ? proactive.Approve(valueOrDefault, text2, false, "button") : null) == null)
      {
        return null;
      }
      ClickyJob clickyJob2 = agents.Jobs.Job(clickyJob.Id);
      if (clickyJob2 != null && clickyJob2.IsWaitingToStart)
      {
        agents.Jobs.Dismiss(clickyJob.Id);
      }
      Log.Info("dock", "\"" + clickyJob.Title + "\" started by the voice's yes (no second job)");
      return clickyJob.Title;
    }

    // Token: 0x0600033C RID: 828 RVA: 0x0051F7AC File Offset: 0x0051F7AC
    [return: Nullable(new byte[] { 1, 2 })]
    private Task<T> OnUiAsync<T>([Nullable(new byte[] { 1, 2 })] Func<T> work) where T : class
    {
      if (this.DispatcherQueue.HasThreadAccess)
      {
        return Task.FromResult<T>(work());
      }
      TaskCompletionSource<T> done = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
      if (!this.DispatcherQueue.TryEnqueue(delegate
      {
        try
        {
          done.TrySetResult(work());
        }
        catch (Exception ex)
        {
          done.TrySetException(ex);
        }
      }))
      {
        done.TrySetResult(default(T));
      }
      return done.Task;
    }

    // Token: 0x0600033D RID: 829 RVA: 0x0051F82C File Offset: 0x0051F82C
    private void SyncLaunchFlights(AgentService agents)
    {
      foreach (AgentTaskSnapshot agentTaskSnapshot in agents.Tasks)
      {
        bool flag = this.dockKnownTasks.Add(agentTaskSnapshot.TaskId);
        AgentTaskStatus status = agentTaskSnapshot.Status;
        bool flag2 = status <= AgentTaskStatus.Running;
        bool flag3 = flag2;
        AgentTaskStatus agentTaskStatus;
        flag2 = this.dockLastStatus.TryGetValue(agentTaskSnapshot.TaskId, out agentTaskStatus);
        if (flag2)
        {
          bool flag4 = agentTaskStatus <= AgentTaskStatus.Running;
          flag2 = flag4;
        }
        bool flag5 = flag2;
        this.dockLastStatus[agentTaskSnapshot.TaskId] = agentTaskSnapshot.Status;
        int num = (flag ? this.AssignLaunchPaletteSlot(agents, agentTaskSnapshot.TaskId) : ((flag3 && !flag5) ? this.PromoteLaunchPaletteSlot(agentTaskSnapshot.TaskId) : (-1)));
        if (flag && !this.dockFirstSync && flag3 && !flag5)
        {
          AgentTaskDetail taskDetail = agents.GetTaskDetail(agentTaskSnapshot.TaskId);
          if (taskDetail != null && !taskDetail.IsRoutineRun)
          {
            ArcDock dock = this.Dock;
            if ((dock == null || !dock.IsLaunchingFromOpenWindow) && !this.Services.Settings.IsPaused)
            {
              RealStack real = this.Real;
              PointD? pointD = ((real != null) ? real.Cursor.RenderedPositionPx() : null);
              ArcDock dock2 = this.Dock;
              PointD pointD2 = ((dock2 != null) ? dock2.LaunchLandingPointPx() : null) ?? AppHost.FallbackLaunchLandingPx(pointD);
              uint num2 = AgentLaunchPalette.SwatchRgb[Math.Clamp(num, 0, AgentLaunchPalette.SlotCount - 1)];
              Color color = Color.FromArgb(byte.MaxValue, (byte)(num2 >> 16), (byte)(num2 >> 8), (byte)num2);
              if (this.launchFlight == null)
              {
                this.launchFlight = new DockLaunchFlight(this.ExcludeFromCapture);
              }
              this.launchFlight.Fly(pointD2, color, delegate
              {
                this.PlayAgentEarcon("agentLaunch", 0.55f);
              }, pointD);
            }
          }
        }
      }
      this.dockFirstSync = false;
    }

    // Token: 0x0600033E RID: 830 RVA: 0x0051FA50 File Offset: 0x0051FA50
    private int AssignLaunchPaletteSlot(AgentService agents, string taskId)
    {
      HashSet<int> hashSet = (from s in agents.Tasks.Where<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
        {
          bool flag = t.TaskId != taskId;
          if (flag)
          {
            AgentTaskStatus status = t.Status;
            bool flag2 = status <= AgentTaskStatus.Running;
            flag = flag2;
          }
          return flag;
        }).Select<AgentTaskSnapshot, int>(delegate(AgentTaskSnapshot t)
        {
          ValueTuple<string, int> valueTuple = this.launchPaletteSlots.FirstOrDefault<ValueTuple<string, int>>(([TupleElementNames(new string[] { "TaskId", "Slot" })] ValueTuple<string, int> s) => s.Item1 == t.TaskId);
          if (valueTuple.Item1 == null)
          {
            return -1;
          }
          return valueTuple.Item2;
        })
        where s >= 0
        select s).ToHashSet<int>();
      int num = AgentLaunchPalette.NextIndex(this.launchPaletteSlots.Select<ValueTuple<string, int>, int>(([TupleElementNames(new string[] { "TaskId", "Slot" })] ValueTuple<string, int> s) => s.Item2).ToList<int>(), hashSet);
      this.launchPaletteSlots.Insert(0, new ValueTuple<string, int>(taskId, num));
      if (this.launchPaletteSlots.Count > 64)
      {
        this.launchPaletteSlots.RemoveAt(this.launchPaletteSlots.Count - 1);
      }
      return num;
    }

    // Token: 0x0600033F RID: 831 RVA: 0x0051FB3C File Offset: 0x0051FB3C
    private int PromoteLaunchPaletteSlot(string taskId)
    {
      int num = this.launchPaletteSlots.FindIndex(([TupleElementNames(new string[] { "TaskId", "Slot" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<string, int> s) => s.Item1 == taskId);
      if (num < 0)
      {
        return -1;
      }
      ValueTuple<string, int> valueTuple = this.launchPaletteSlots[num];
      this.launchPaletteSlots.RemoveAt(num);
      this.launchPaletteSlots.Insert(0, valueTuple);
      return valueTuple.Item2;
    }

    // Token: 0x06000340 RID: 832 RVA: 0x0051FBA0 File Offset: 0x0051FBA0
    private static PointD FallbackLaunchLandingPx(PointD? start)
    {
      Displays.Monitor monitor;
      if (start != null)
      {
        PointD valueOrDefault = start.GetValueOrDefault();
        monitor = Displays.At((int)valueOrDefault.X, (int)valueOrDefault.Y);
      }
      else
      {
        monitor = Displays.UnderCursor();
      }
      Displays.Monitor monitor2 = monitor;
      return new PointD((double)monitor2.Work.Right - 38.0 * monitor2.Scale, (double)monitor2.Work.Top + 38.0 * monitor2.Scale);
    }

    // Token: 0x17000073 RID: 115
    // (get) Token: 0x06000341 RID: 833 RVA: 0x0051FC20 File Offset: 0x0051FC20
    // (set) Token: 0x06000342 RID: 834 RVA: 0x0051FC28 File Offset: 0x0051FC28
    internal bool InterviewSendOffInProgress { get; private set; }

    // Token: 0x17000074 RID: 116
    // (get) Token: 0x06000343 RID: 835 RVA: 0x0051FC34 File Offset: 0x0051FC34
    [Nullable(2)]
    public GoodMorningBrief MorningBrief
    {
      [NullableContext(2)]
      get
      {
        return this.morningBrief;
      }
    }

    // Token: 0x06000344 RID: 836 RVA: 0x0051FC3C File Offset: 0x0051FC3C
    private void StartDrawnTour(RealStack real)
    {
      ClickyTourJobCards.Shared.Attach(this);
      this.morningBrief = new GoodMorningBrief(this, real);
      this.WireTour660Hooks(real);
      FlowHooks.TakeFlowBrief = () => DrawnOnboardingTour.ConsumePendingBrief() ?? GoodMorningBrief.ConsumePendingBrief();
      FlowHooks.TakePillPlanNote = new Func<string>(ClickyTourJobCards.Shared.TakePendingPillPlanNote);
      FlowHooks.IsDrawnTourRunning = () => DrawnOnboardingTour.IsRunning;
      FlowHooks.IsInterviewSendOffInProgress = () => this.InterviewSendOffInProgress;
      FlowHooks.TourStepNote = delegate
      {
        OnboardingTutorialStep? currentStep = real.Tutorial.CurrentStep;
        if (currentStep != null)
        {
          OnboardingTutorialStep valueOrDefault = currentStep.GetValueOrDefault();
          if (real.Tutorial.Phase == OnboardingTutorialPhase.Running)
          {
            OnboardingTutorialStep onboardingTutorialStep = valueOrDefault;
            InterviewController interview = this.Interview;
            return TourStepNotes.Note(onboardingTutorialStep, ((interview != null) ? interview.Transcript : null) ?? Array.Empty<InterviewLine>(), this.SpokenTalkKeys());
          }
        }
        return null;
      };
      real.Companion.OneBrainTurnEnded += delegate
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.PrefetchDrawnOnboardingFinaleIfReady));
      };
      BillingUi billingUi = this.Services.Billing;
      if (billingUi != null)
      {
        billingUi.PaywallClosed += delegate(PaywallSource _, PaywallCloseKind _)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            DrawnOnboardingTour.ReleaseNotch();
            ClickyTourJobCards.Shared.PopHeldNextQuestions(0.9);
          });
        };
      }
    }

    // Token: 0x06000345 RID: 837 RVA: 0x0051FD4C File Offset: 0x0051FD4C
    internal string SpokenTalkKeys()
    {
      return this.Services.Settings.PushToTalkDisplay.ToLowerInvariant().Replace(" + ", " and ");
    }

    // Token: 0x17000075 RID: 117
    // (get) Token: 0x06000346 RID: 838 RVA: 0x0051FD74 File Offset: 0x0051FD74
    [Nullable(2)]
    internal string AccountFirstNameForTour
    {
      [NullableContext(2)]
      get
      {
        UserProfile user = this.Services.Auth.User;
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
    }

    // Token: 0x06000347 RID: 839 RVA: 0x0051FDD0 File Offset: 0x0051FDD0
    private bool HandleTourDrawingAction(DrawingAction action)
    {
      string text = action.Kind;
      if (!(text == "tourtuck") && !(text == "tourasks") && !(text == "tourjob") && !(text == "tournotch") && !(text == "tourfade"))
      {
        return false;
      }
      JsonElement jsonElement;
      int num2;
      int num = ((action.Payload.ValueKind == JsonValueKind.Object && action.Payload.TryGetProperty("line", out jsonElement) && jsonElement.TryGetInt32(out num2)) ? num2 : 0);
      JsonElement jsonElement2;
      string text2 = ((action.Payload.ValueKind == JsonValueKind.Object && action.Payload.TryGetProperty("text", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.String) ? (jsonElement2.GetString() ?? "") : "");
      if (!DrawnOnboardingTour.IsRunning && !GoodMorningBrief.IsPlaying)
      {
        string text3 = "drawing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
        defaultInterpolatedStringHandler.AppendFormatted(action.Kind);
        defaultInterpolatedStringHandler.AppendLiteral(" in beat ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
        defaultInterpolatedStringHandler.AppendLiteral(" not run: no tour or morning is running");
        Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        return true;
      }
      text = action.Kind;
      if (!(text == "tourtuck"))
      {
        if (!(text == "tournotch"))
        {
          if (!(text == "tourfade"))
          {
            if (!(text == "tourasks"))
            {
              if (text == "tourjob")
              {
                ClickyTourJob clickyTourJob = ClickyTourJob.FromPayload(text2);
                if (clickyTourJob == null)
                {
                  string text4 = "drawing";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("tourJob in beat ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
                  defaultInterpolatedStringHandler.AppendLiteral(" skipped: unreadable ");
                  defaultInterpolatedStringHandler.AppendFormatted((text2.Length > 120) ? text2.Substring(0, 120) : text2);
                  Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
                }
                else
                {
                  string text5 = "drawing";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("tourJob in beat ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
                  defaultInterpolatedStringHandler.AppendLiteral(": ");
                  defaultInterpolatedStringHandler.AppendFormatted(clickyTourJob.Title);
                  Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
                  ClickyTourJobCards shared = ClickyTourJobCards.Shared;
                  ClickyTourJob clickyTourJob2 = clickyTourJob;
                  RealStack real = this.Real;
                  shared.Show(clickyTourJob2, (real != null) ? real.Canvas.DrawingDisplay : null);
                }
              }
            }
            else
            {
              ClickyTourJobCards.Shared.NoteNextQuestions(text2);
              string text6 = "drawing";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
              defaultInterpolatedStringHandler.AppendLiteral("nextQuestions in beat ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
              Log.Info(text6, defaultInterpolatedStringHandler.ToStringAndClear());
            }
          }
          else
          {
            string text7 = "drawing";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
            defaultInterpolatedStringHandler.AppendLiteral("fadeAway in beat ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
            Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
            DrawnOnboardingNotchShowcase.Shared.FadeDrawingAway(this);
          }
        }
        else
        {
          string text8 = "drawing";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
          defaultInterpolatedStringHandler.AppendLiteral("showNotch(");
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral(") in beat ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
          Log.Info(text8, defaultInterpolatedStringHandler.ToStringAndClear());
          if (text2 == "apps")
          {
            DrawnOnboardingNotchShowcase.Shared.PopApps(this, true);
          }
          else
          {
            DrawnOnboardingNotchShowcase.Shared.Show(this);
          }
        }
      }
      else
      {
        string text9 = "drawing";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
        defaultInterpolatedStringHandler.AppendLiteral("tuckJobs in beat ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
        Log.Info(text9, defaultInterpolatedStringHandler.ToStringAndClear());
        ClickyTourJobCards.Shared.TuckIntoNotchNowAsync();
      }
      return true;
    }

    // Token: 0x06000348 RID: 840 RVA: 0x00520184 File Offset: 0x00520184
    internal async Task<bool> RunDrawnOnboardingFinaleAsync()
    {
      RealStack real = this.Real;
      if (real != null)
      {
        InterviewController interview = this.Interview;
        if (interview != null)
        {
          IReadOnlyList<InterviewLine> transcript = interview.Transcript;
          if (!transcript.Any<InterviewLine>((InterviewLine l) => l.Role == "user"))
          {
            return false;
          }
          DrawnOnboardingNotch.DismissPeek(this);
          DrawnOnboardingTour.HoldNotchUntilPaywallAnswered(this);
          ClickyTourJobCards.Shared.DismissAll();
          DrawnOnboardingNotchShowcase.Shared.Reset(this);
          ClickyTourJobCards.Shared.SuggestionCheckReason = ProactiveCheckReason.Onboarding;
          DrawnOnboardingTour.IsRunning = true;
          DrawnOnboardingFinalePrefetch.Prefetched prefetched = DrawnOnboardingFinalePrefetch.Shared.Take(real.Core.Auth.UserId);
          int num;
          if (prefetched != null)
          {
            num = prefetched.PlannedCardCount;
          }
          else
          {
            ProactiveCoordinator proactive = this.Proactive;
            List<ProactiveSuggestionCard> list = (((proactive != null) ? proactive.VisibleCards : null) ?? Array.Empty<ProactiveSuggestionCard>()).Where<ProactiveSuggestionCard>((ProactiveSuggestionCard c) => c.CheckReason == ProactiveCheckReason.Onboarding).Take<ProactiveSuggestionCard>(3).ToList<ProactiveSuggestionCard>();
            num = list.Count;
            DrawnOnboardingTour.Arm(DrawnOnboardingTour.TourBrief(this.AccountFirstNameForTour ?? "there", transcript, this.SpokenTalkKeys(), list));
          }
          DrawnOnboardingTour.TrackFinaleStarted("onboarding", num, transcript.Count<InterviewLine>((InterviewLine l) => l.Role == "user"), prefetched != null);
          string text = "drawn tour";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 1);
          defaultInterpolatedStringHandler.AppendLiteral("finale: drawing the tour from the interview (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num);
          defaultInterpolatedStringHandler.AppendLiteral(" planned card(s)");
          string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          string text3;
          if (prefetched != null)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
            defaultInterpolatedStringHandler.AppendLiteral(", prefetched ");
            defaultInterpolatedStringHandler.AppendFormatted<int>((int)(DateTimeOffset.Now - prefetched.StartedAt).TotalSeconds);
            defaultInterpolatedStringHandler.AppendLiteral("s ago, ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(prefetched.CodeCharactersReady);
            defaultInterpolatedStringHandler.AppendLiteral(" characters ready)");
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text3 = ")";
          }
          Log.Info(text, text2 + text3);
          await Task.Delay((prefetched == null) ? 1300 : 300);
          if (prefetched != null)
          {
            await real.Companion.PlayPrefetchedTurnAsync(prefetched.Turn);
          }
          else
          {
            await real.Companion.SubmitFactAsync("[App] The welcome tour's last moment: the user pressed Show me. Draw their welcome now, as the note says.", false);
          }
          DrawnOnboardingTour.ConsumePendingBrief();
          DrawnOnboardingFinalePrefetch.Shared.Reset("the finale is over");
          await this.FinishDrawnOnboardingTourAsync(ProactiveCheckReason.Onboarding, false);
          DrawnOnboardingTour.PlannedCardsById = new Dictionary<string, ProactiveSuggestionCard>();
          await Task.Delay(600);
          return true;
        }
      }
      return false;
    }

    // Token: 0x17000076 RID: 118
    // (get) Token: 0x06000349 RID: 841 RVA: 0x005201C8 File Offset: 0x005201C8
    internal bool IsDrawnOnboardingFinalePrefetchReady
    {
      get
      {
        if (DrawnOnboardingTour.IsFinaleEnabled)
        {
          TutorialWindow tutorialWindow = this.tutorial;
          if (tutorialWindow != null && tutorialWindow.IsRunning && tutorialWindow.CurrentStep == TutorialWindow.Step.Finale && !this.InterviewSendOffInProgress)
          {
            RealStack real = this.Real;
            if (real != null && real.Core.Auth.IsSignedIn)
            {
              InterviewController interview = this.Interview;
              if (interview != null)
              {
                if (interview.Transcript.Any<InterviewLine>((InterviewLine l) => l.Role == "user") && (interview.HasBuiltPlan || !interview.IsPlanInFlight))
                {
                  return real.Companion.ActiveTurnCount == 0;
                }
              }
            }
          }
        }
        return false;
      }
    }

    // Token: 0x0600034A RID: 842 RVA: 0x0052027C File Offset: 0x0052027C
    internal void PrefetchDrawnOnboardingFinaleIfReady()
    {
      DrawnOnboardingFinalePrefetch.Shared.StartIfReady(this);
    }

    // Token: 0x17000077 RID: 119
    // (get) Token: 0x0600034B RID: 843 RVA: 0x0052028C File Offset: 0x0052028C
    [Nullable(2)]
    internal PanelWindow TourPanelWindow
    {
      [NullableContext(2)]
      get
      {
        TutorialWindow tutorialWindow = this.tutorial;
        if (tutorialWindow == null)
        {
          return null;
        }
        return tutorialWindow.Window;
      }
    }

    // Token: 0x0600034C RID: 844 RVA: 0x005202A0 File Offset: 0x005202A0
    private void OnTourStepChanged(TutorialWindow.Step? step)
    {
      if (step.GetValueOrDefault() == TutorialWindow.Step.Finale)
      {
        this.PrefetchDrawnOnboardingFinaleIfReady();
        return;
      }
      if (step == null && !this.InterviewSendOffInProgress)
      {
        DrawnOnboardingFinalePrefetch.Shared.DiscardUnlessAdopted("the tour ended");
      }
    }

    // Token: 0x0600034D RID: 845 RVA: 0x005202D4 File Offset: 0x005202D4
    internal async Task FinishDrawnOnboardingTourAsync(ProactiveCheckReason suggestionCheckReason, bool streamsHoldToTalkLine = true)
    {
      RealStack real = this.Real;
      if (real != null)
      {
        ScreenDrawingCanvas canvas = real.Canvas;
        long started = Environment.TickCount64;
        long? quietSince = null;
        bool narrationTimedOut = true;
        while (Environment.TickCount64 - started < 180000L)
        {
          await Task.Delay(150);
          if (canvas.IsStreamingNarration || real.Companion.State == VoiceState.Responding)
          {
            quietSince = null;
          }
          else
          {
            quietSince.GetValueOrDefault();
            if (quietSince == null)
            {
              quietSince = new long?(Environment.TickCount64);
            }
            long? num = Environment.TickCount64 - quietSince;
            long num2 = 600L;
            if ((num.GetValueOrDefault() >= num2) & (num != null))
            {
              narrationTimedOut = false;
              break;
            }
          }
        }
        DrawnOnboardingNotchShowcase showcase = DrawnOnboardingNotchShowcase.Shared;
        showcase.Close(this, "tour_end");
        DisplayInfo display = canvas.DrawingDisplay;
        int cardsShownCount = ClickyTourJobCards.Shared.PlacedCardCount;
        canvas.FadeDrawingKeepingAskButtons();
        try
        {
          ClickyTourJobCards.Shared.SuggestionCheckReason = suggestionCheckReason;
          await ClickyTourJobCards.Shared.TuckRemainingCardsIntoSuggestionsAsync();
          Log.Info("drawn tour", "ended");
          ClickyTourJobCards.Shared.HoldNextQuestions(display);
          DrawnOnboardingTour.TrackFinaleEnded(narrationTimedOut ? "narration_timeout" : ((display == null) ? "no_drawing" : "finished"), cardsShownCount, ClickyTourJobCards.Shared.HeldNextQuestionCount);
          DrawnOnboardingTour.IsRunning = false;
          this.FadeOutOnboardingMusic(3.0);
          if (streamsHoldToTalkLine)
          {
            await Task.Delay(900);
            this.StreamHoldToTalkLineAfterOnboarding();
          }
        }
        finally
        {
          showcase.Reset(this);
        }
      }
    }

    // Token: 0x0600034E RID: 846 RVA: 0x00520328 File Offset: 0x00520328
    public void PresentDrawnOnboardingTourForDebugTest()
    {
      RealStack real = this.Real;
      if (real == null || !real.Core.Auth.IsSignedIn)
      {
        Log.Info("drawn tour", "not signed in: nothing to run");
        return;
      }
      DrawnOnboardingNotch.DismissPeek(this);
      if (this.Services.Presenter.Presentation is NotchPresentation.TextInput)
      {
        this.Services.Presenter.Present(new NotchPresentation.Idle());
      }
      ClickyTourJobCards.Shared.DismissAll();
      DrawnOnboardingNotchShowcase.Shared.Reset(this);
      ClickyTourJobCards.Shared.SuggestionCheckReason = ProactiveCheckReason.Debug;
      DrawnOnboardingTour.IsRunning = true;
      ValueTuple<string, IReadOnlyList<InterviewLine>, int?> valueTuple = DrawnOnboardingTour.TestInterview();
      DrawnOnboardingTour.Arm(DrawnOnboardingTour.TourBrief(valueTuple.Item1, valueTuple.Item2, this.SpokenTalkKeys(), null));
      DrawnOnboardingTour.TrackFinaleStarted("developer_test", 0, valueTuple.Item2.Count<InterviewLine>((InterviewLine l) => l.Role == "user"), false);
      string text = "drawn tour";
      string text2 = "test: drawing the tour for ";
      int? item = valueTuple.Item3;
      string text3;
      if (item != null)
      {
        int valueOrDefault = item.GetValueOrDefault();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
        defaultInterpolatedStringHandler.AppendLiteral("real interview #");
        defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault + 1);
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "the made-up interview (Sam)";
      }
      Log.Info(text, text2 + text3);
      this.RunDrawnTourTestAsync(real, valueTuple.Item1);
    }

    // Token: 0x0600034F RID: 847 RVA: 0x0052047C File Offset: 0x0052047C
    private async Task RunDrawnTourTestAsync(RealStack real, string name)
    {
      await Task.Delay(700);
      this.SetTourMusic(true);
      CompanionSession companion = real.Companion;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(161, 2);
      defaultInterpolatedStringHandler.AppendLiteral("[App] Developer test of the drawn welcome tour for another person's interview, ");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral("; nothing in this turn is about the real user. Draw ");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendLiteral("'s tour now, as the note says.");
      await companion.SubmitFactAsync(defaultInterpolatedStringHandler.ToStringAndClear(), false);
      DrawnOnboardingTour.ConsumePendingBrief();
      await this.FinishDrawnOnboardingTourAsync(ProactiveCheckReason.Debug, true);
    }

    // Token: 0x06000350 RID: 848 RVA: 0x005204D0 File Offset: 0x005204D0
    public void PresentOnboardingDrawStepForDebugTest()
    {
      DrawnOnboardingNotch.DismissPeek(this);
      this.OpenTutorialForDebug(TutorialWindow.Step.DrawDemo);
      ValueTuple<string, IReadOnlyList<InterviewLine>, int?> valueTuple = DrawnOnboardingTour.TestInterview();
      InterviewController interview = this.Interview;
      if (interview != null)
      {
        interview.UseTranscriptForDrawStepTest(valueTuple.Item2);
      }
      string text = "tutorial";
      string text2 = "draw step test: drawing step for ";
      int? item = valueTuple.Item3;
      string text3;
      if (item != null)
      {
        int valueOrDefault = item.GetValueOrDefault();
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 1);
        defaultInterpolatedStringHandler.AppendLiteral("real interview #");
        defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault + 1);
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "the made-up interview (Sam)";
      }
      Log.Info(text, text2 + text3);
    }

    // Token: 0x17000078 RID: 120
    // (get) Token: 0x06000351 RID: 849 RVA: 0x00520564 File Offset: 0x00520564
    // (set) Token: 0x06000352 RID: 850 RVA: 0x0052056C File Offset: 0x0052056C
    [Nullable(2)]
    internal ClickyTakeOverController ClickyDrive
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x17000079 RID: 121
    // (get) Token: 0x06000353 RID: 851 RVA: 0x00520578 File Offset: 0x00520578
    // (set) Token: 0x06000354 RID: 852 RVA: 0x00520580 File Offset: 0x00520580
    [Nullable(2)]
    internal AgentDriveConsent ClickyDriveConsent
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06000355 RID: 853 RVA: 0x0052058C File Offset: 0x0052058C
    private void StartClickyDrive(RealStack real)
    {
      AppHost.<>c__DisplayClass208_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass208_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.drive = new ClickyTakeOverController(CS$<>8__locals1.real.Companion, CS$<>8__locals1.real.Capture, this.DispatcherQueue)
      {
        Accent = () => DS.CursorColor(CS$<>8__locals1.<>4__this.Services.Settings.CursorColorHex),
        IsAvailable = delegate
        {
          string text = CS$<>8__locals1.real.Core.Settings.Read<string>(([Nullable(1)] AppSettings s) => s.OneBrainModel);
          return text == null || text.Length <= 0 || text.StartsWith("gpt-", StringComparison.Ordinal);
        },
        RefundEarlyStop = delegate(string leaseId)
        {
          AppHost.<>c__DisplayClass208_0.<<StartClickyDrive>b__5>d <<StartClickyDrive>b__5>d;
          <<StartClickyDrive>b__5>d.<>t__builder = AsyncTaskMethodBuilder.Create();
          <<StartClickyDrive>b__5>d.<>4__this = CS$<>8__locals1;
          <<StartClickyDrive>b__5>d.leaseId = leaseId;
          <<StartClickyDrive>b__5>d.<>1__state = -1;
          <<StartClickyDrive>b__5>d.<>t__builder.Start<AppHost.<>c__DisplayClass208_0.<<StartClickyDrive>b__5>d>(ref <<StartClickyDrive>b__5>d);
          return <<StartClickyDrive>b__5>d.<>t__builder.Task;
        },
        RefreshBilling = () => CS$<>8__locals1.real.Core.Billing.RefreshAsync()
      };
      CS$<>8__locals1.real.Companion.AttachDrive(CS$<>8__locals1.drive);
      this.ClickyDrive = CS$<>8__locals1.drive;
      ChromeSetupFlow.Shared.Drive = new ChromeSetupDrive(CS$<>8__locals1.drive, CS$<>8__locals1.real.Companion);
      CS$<>8__locals1.drive.ChromeSetupIsRunning = () => ChromeAutoInstall.IsRunning;
      if (this.hotkeys != null)
      {
        this.hotkeys.DriveClaimsEscape = () => CS$<>8__locals1.drive.ClaimsEscape;
        this.hotkeys.DriveEscape = new Func<bool>(CS$<>8__locals1.drive.HandleEscape);
      }
      DriveNotchOwnership.OwnsNotchChanged = delegate(bool owns)
      {
        NotchPresenter corePresenter = CS$<>8__locals1.<>4__this.CorePresenter;
        if (corePresenter == null)
        {
          return;
        }
        corePresenter.SetClickyDriveShowing(owns);
      };
      Log.Info("drive", "Clicky drive ready");
    }

    // Token: 0x06000356 RID: 854 RVA: 0x005206E4 File Offset: 0x005206E4
    internal AgentDriveConsent AttachAgentJobs(IAgentDriveJobs jobs)
    {
      if (this.ClickyDrive == null)
      {
        throw new InvalidOperationException("Clicky drive isn't up yet");
      }
      return this.ClickyDriveConsent = new AgentDriveConsent(this.ClickyDrive, jobs, null);
    }

    // Token: 0x06000357 RID: 855 RVA: 0x0052071C File Offset: 0x0052071C
    internal void PreviewClickyDrive()
    {
      ClickyTakeOverController clickyDrive = this.ClickyDrive;
      if (clickyDrive == null)
      {
        return;
      }
      clickyDrive.PreviewForDebug();
    }

    // Token: 0x1700007A RID: 122
    // (get) Token: 0x06000358 RID: 856 RVA: 0x00520730 File Offset: 0x00520730
    // (set) Token: 0x06000359 RID: 857 RVA: 0x00520738 File Offset: 0x00520738
    [Nullable(2)]
    public AgentsInkNoteHost InkNoteHost
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x0600035A RID: 858 RVA: 0x00520744 File Offset: 0x00520744
    private void StartInkNotes(RealStack real)
    {
      AppHost.<>c__DisplayClass216_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass216_0();
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.<>4__this = this;
      ClickyLiveNotePreferences.Settings = CS$<>8__locals1.real.Core.Settings;
      ClickyInkLinkPreviewPlate.WorkerBaseUrl = (ClickyInkImages.WorkerBaseUrl = CS$<>8__locals1.real.Core.Endpoints.WorkerBaseUrl.AbsoluteUri);
      ClickyInkImages.PhotoSearch = (string query, CancellationToken ct) => CS$<>8__locals1.real.Canvas.PhotoAsync(query, ct, null);
      CS$<>8__locals1.agents = CS$<>8__locals1.real.Agents;
      ClickyInkNoteStore.ClearLiveSteps = delegate(string key)
      {
        CS$<>8__locals1.agents.LiveSteps.Clear(key);
      };
      Func<IReadOnlyList<string>, ISet<string>> func = (IReadOnlyList<string> paths) => ClickyMainArtifacts.SupportingPaths(paths);
      Func<string, bool> func2;
      if ((func2 = AppHost.<>O.<5>__IsMeaningfulFile) == null)
      {
        func2 = (AppHost.<>O.<5>__IsMeaningfulFile = new Func<string, bool>(ClickyMainArtifacts.IsMeaningfulFile));
      }
      ClickyInkPaths.MainArtifacts = new ClickyInkPaths.FileRules(func, func2);
      CS$<>8__locals1.agents.LiveNotes = new ClickyInkLiveNoteTurns(CS$<>8__locals1.agents, this.DispatcherQueue);
      this.InkNoteHost = new AgentsInkNoteHost(CS$<>8__locals1.agents)
      {
        FindJob = delegate(string jobId)
        {
          ClickyJob clickyJob = CS$<>8__locals1.agents.Jobs.Job(jobId);
          if (clickyJob == null)
          {
            return null;
          }
          return ClickyInkLiveNoteTurns.InkJob(CS$<>8__locals1.agents, clickyJob);
        },
        LiveStatus = (string slug) => AgentsInkNoteHost.InkLive(CS$<>8__locals1.agents.LiveStatusForClicky(slug)),
        IsUnread = (string slug) => ClickyReadMarks.Unread(CS$<>8__locals1.agents).Contains(slug),
        SetInterval = delegate(string routineId, int minutes, string source)
        {
          CS$<>8__locals1.agents.Routines.SetInterval(routineId, minutes, source);
        },
        OpenInViewer = ([Nullable(1)] string target, string slug, string threadId) => CS$<>8__locals1.<>4__this.OpenAgentDocumentInViewer(target, threadId, slug),
        OpenJobArtifactHandler = delegate(string target, string jobId)
        {
          AppHost <>4__this = CS$<>8__locals1.<>4__this;
          ClickyJob clickyJob2 = CS$<>8__locals1.agents.Jobs.Job(jobId);
          if (!<>4__this.OpenAgentDocumentInViewer(target, (clickyJob2 != null) ? clickyJob2.ThreadId : null, null))
          {
            ClickyInkNoteBridge.OpenInItsApp(target);
          }
        },
        SendOnThread = delegate(string threadId, string message, string launchContext)
        {
          ClickyJob clickyJob3 = CS$<>8__locals1.agents.Jobs.JobForThread(threadId);
          if (clickyJob3 == null)
          {
            Log.Warn("ink", "no job for thread " + threadId);
            return;
          }
          CS$<>8__locals1.agents.LaunchAsync(new AgentLaunchRequest
          {
            Prompt = message,
            JobId = clickyJob3.Id,
            TargetThreadId = threadId,
            LaunchContext = launchContext,
            LaunchSource = "text",
            UsageContext = "agent_chat"
          }, default(CancellationToken));
        }
      };
      if (ClickyInkNoteBridge.Host == null)
      {
        ClickyInkNoteBridge.Host = this.InkNoteHost;
      }
      AppHost.<>c__DisplayClass216_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInkNoteConnect clickyInkNoteConnect = new ClickyInkNoteConnect(CS$<>8__locals1.real.Agents, this.DispatcherQueue, new Action<string>(this.OpenUrl));
      clickyInkNoteConnect.BringAppToFront = delegate
      {
        Native.ForceForeground(CS$<>8__locals1.<>4__this.NotchUi.Window.Hwnd);
      };
      ClickyInkNoteConnect clickyInkNoteConnect2 = clickyInkNoteConnect;
      this.inkConnect = clickyInkNoteConnect;
      CS$<>8__locals2.connect = clickyInkNoteConnect2;
      ClickyInkNoteConnect connect = CS$<>8__locals1.connect;
      AgentIntegrationCatalog catalog = ConnectedApps.Catalog;
      connect.Sync((catalog != null) ? catalog.Integrations : null);
      ConnectedApps.Changed += delegate
      {
        DispatcherQueue dispatcherQueue = CS$<>8__locals1.<>4__this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = CS$<>8__locals1.<>9__12) == null)
        {
          dispatcherQueueHandler = (CS$<>8__locals1.<>9__12 = delegate
          {
            ClickyInkNoteConnect connect2 = CS$<>8__locals1.connect;
            AgentIntegrationCatalog catalog2 = ConnectedApps.Catalog;
            connect2.Sync((catalog2 != null) ? catalog2.Integrations : null);
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
    }

    // Token: 0x0600035B RID: 859 RVA: 0x00520948 File Offset: 0x00520948
    [NullableContext(2)]
    private bool InkNoteSignInReturned(string toolkit, bool failed)
    {
      ClickyInkNoteConnect clickyInkNoteConnect = this.inkConnect;
      return clickyInkNoteConnect != null && clickyInkNoteConnect.OnAuthCallback(toolkit, failed);
    }

    // Token: 0x1700007B RID: 123
    // (get) Token: 0x0600035C RID: 860 RVA: 0x00520960 File Offset: 0x00520960
    public CursorCompanion CursorCompanion
    {
      get
      {
        CursorCompanion cursorCompanion;
        if ((cursorCompanion = this.cursorCompanion) == null)
        {
          cursorCompanion = (this.cursorCompanion = this.CreateCursorCompanion());
        }
        return cursorCompanion;
      }
    }

    // Token: 0x0600035D RID: 861 RVA: 0x00520988 File Offset: 0x00520988
    private CursorCompanion CreateCursorCompanion()
    {
      CursorCompanion cursorCompanion = new CursorCompanion(this.ExcludeFromCapture, () => this.Services.Settings.CursorColorHex);
      cursorCompanion.DoubleEscape += delegate
      {
        HomeTour homeTour = this.tour;
        if (homeTour != null && homeTour.IsRunning)
        {
          homeTour.AbortForEscape();
        }
      };
      return cursorCompanion;
    }

    // Token: 0x1700007C RID: 124
    // (get) Token: 0x0600035E RID: 862 RVA: 0x005209B4 File Offset: 0x005209B4
    public bool IsIntroSequenceActive
    {
      get
      {
        return this.introSequenceActive;
      }
    }

    // Token: 0x0600035F RID: 863 RVA: 0x005209BC File Offset: 0x005209BC
    private void SetIntroSequenceActive(bool active)
    {
      this.introSequenceActive = active;
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.SetOnboardingIntroOwnsStage(active);
      }
      this.SyncNotchRouting();
    }

    // Token: 0x06000360 RID: 864 RVA: 0x005209E0 File Offset: 0x005209E0
    private static string AssetPath(params string[] parts)
    {
      string baseDirectory = AppContext.BaseDirectory;
      string text = "Assets";
      int num = 0;
      string[] array = new string[2 + parts.Length];
      array[num] = baseDirectory;
      num++;
      array[num] = text;
      num++;
      ReadOnlySpan<string> readOnlySpan = new ReadOnlySpan<string>(parts);
      readOnlySpan.CopyTo(new Span<string>(array).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      return Path.Combine(new ReadOnlySpan<string>(array));
    }

    // Token: 0x06000361 RID: 865 RVA: 0x00520A58 File Offset: 0x00520A58
    public void DemoIntroSequence(bool isReplay)
    {
      this.BeginOnboardingIntroSequence(isReplay);
    }

    // Token: 0x06000362 RID: 866 RVA: 0x00520A64 File Offset: 0x00520A64
    public void ReplayOnboardingExperienceFromSettings()
    {
      if (this.Services.Auth.IsSignedIn && !this.introSequenceActive)
      {
        RealStack real = this.Real;
        if (real == null || !real.Tutorial.IsEngaged)
        {
          TutorialWindow tutorialWindow = this.tutorial;
          if (tutorialWindow == null || !tutorialWindow.IsRunning)
          {
            Log.Info("video", "replaying the welcome tour from Settings");
            ClickyAnalytics.TrackOnboardingReplayed();
            NotchPresenter corePresenter = this.CorePresenter;
            if (corePresenter != null)
            {
              corePresenter.DismissNotchAgentPeek();
            }
            this.Services.Companion.Cancel();
            RealStack real2 = this.Real;
            if (real2 != null)
            {
              real2.Core.Settings.Update(delegate(AppSettings s)
              {
                s.Onboarding.TutorialCompletedAtEpochSeconds = null;
              });
            }
            else
            {
              this.Services.Settings.HasCompletedTutorial = false;
            }
            this.BeginOnboardingIntroSequence(true);
            return;
          }
        }
      }
      Log.Info("video", "replay refused: not signed in, or the intro or the tour is already running");
    }

    // Token: 0x06000363 RID: 867 RVA: 0x00520B50 File Offset: 0x00520B50
    private void BeginOnboardingIntroSequence(bool isReplay = false)
    {
      if (this.introSequenceActive)
      {
        return;
      }
      this.SetIntroSequenceActive(true);
      string text = "video";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 1);
      defaultInterpolatedStringHandler.AppendLiteral("intro sequence started (replay: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(isReplay);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      ScreenDrawingCanvas.WarmUpFigures();
      RealStack real = this.Real;
      if (real != null && !this.cursorShownForOnboarding)
      {
        this.cursorShownForOnboarding = true;
        real.Cursor.Show();
      }
      HeaderPills.RaiseChanged();
      this.Services.Presenter.ReturnToIdle();
      this.StartOnboardingMusic(0.3);
      this.WelcomeThenVideoAsync(isReplay);
    }

    // Token: 0x06000364 RID: 868 RVA: 0x00520C00 File Offset: 0x00520C00
    private async Task WelcomeThenVideoAsync(bool isReplay)
    {
      try
      {
        await this.PlayCursorWelcomeAsync();
      }
      catch (Exception ex)
      {
        Log.Warn("video", "welcome: " + ex.Message);
      }
      this.AfterSeconds(0.5, delegate
      {
        this.SetupOnboardingVideo(isReplay);
      });
    }

    // Token: 0x06000365 RID: 869 RVA: 0x00520C4C File Offset: 0x00520C4C
    private void SetupOnboardingVideo(bool isReplay)
    {
      IReadOnlyList<CursorCompanion.Cue> readOnlyList;
      if (!(Environment.GetEnvironmentVariable("HEYCLICKY_NO_VIDEO_CUES") == "1"))
      {
        readOnlyList = this.OnboardingVideoCues(isReplay);
      }
      else
      {
        IReadOnlyList<CursorCompanion.Cue> readOnlyList2 = Array.Empty<CursorCompanion.Cue>();
        readOnlyList = readOnlyList2;
      }
      IReadOnlyList<CursorCompanion.Cue> readOnlyList3 = readOnlyList;
      ValueTuple<MediaPlayer, IntroVideoSourceKind>? valueTuple = this.TakeIntroPlayer();
      if (valueTuple != null)
      {
        ValueTuple<MediaPlayer, IntroVideoSourceKind> valueOrDefault = valueTuple.GetValueOrDefault();
        if (this.PlayIntroReel(valueOrDefault, readOnlyList3, false))
        {
          if (this.music != null)
          {
            this.music.Volume = 0.2;
          }
          return;
        }
      }
      this.FinishOnboardingVideoAndShowPrompt();
    }

    // Token: 0x06000366 RID: 870 RVA: 0x00520CC8 File Offset: 0x00520CC8
    private void FinishOnboardingVideoAndShowPrompt()
    {
      this.FadeOutOnboardingMusic(3.0);
      this.AfterSeconds(2.0, delegate
      {
        this.SetIntroSequenceActive(false);
        HeaderPills.RaiseChanged();
        RealStack real = this.Real;
        if ((real != null) ? real.Tutorial.ShouldOfferAfterVideo(false, real.Core.Auth.IsSignedIn) : (!this.Services.Settings.HasCompletedTutorial))
        {
          RealStack real2 = this.Real;
          if (real2 == null || real2.Tutorial.PresentInvite())
          {
            this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.OnboardingTutorialInvite(), null);
            Sounds.Play("clicky-question", 0.4);
            RealStack real3 = this.Real;
            if (real3 != null)
            {
              real3.Companion.Mouth.EnsureOpenAsync(default(CancellationToken));
            }
            ClickyAnalytics.TrackOnboardingTutorialInvitePresented();
            return;
          }
        }
        this.SayAtCursorAsync("press " + this.Services.Settings.PushToTalkDisplay.ToLowerInvariant() + " to introduce yourself in your language", true, 6.0, default(CancellationToken));
      });
    }

    // Token: 0x06000367 RID: 871 RVA: 0x00520CF4 File Offset: 0x00520CF4
    private IReadOnlyList<CursorCompanion.Cue> OnboardingVideoCues(bool isReplay)
    {
      return new <>z__ReadOnlyArray<CursorCompanion.Cue>(new CursorCompanion.Cue[]
      {
        new CursorCompanion.Cue(36.0, delegate
        {
          ClickyAnalytics.TrackOnboardingDemoTriggered();
          this.PointAtSomethingInterestingAsync();
        }),
        new CursorCompanion.Cue(60.0, delegate
        {
          this.OpenUrl("https://www.spacex.com/vehicles/starship");
        }),
        new CursorCompanion.Cue(67.0, delegate
        {
          if (!isReplay)
          {
            this.LaunchOnboardingDemoAgent(StarterCast.ResearchBuddy, "I'm the CEO of this rocket company you see on my screen. Can you research my competitors and save it as a PDF?", AppHost.ResearchBuddyPrompt);
          }
        }),
        new CursorCompanion.Cue(85.0, delegate
        {
          if (!isReplay)
          {
            this.LaunchOnboardingDemoAgent(StarterCast.Shippy, "Can you make me a website for the beautiful person watching this video right now, and open it in the browser when you're done?", AppHost.ShippyPrompt);
          }
        }),
        new CursorCompanion.Cue(110.0, delegate
        {
          this.OpenUrl("https://mail.google.com/mail/?view=cm&fs=1&to=hi@heyclicky.com&su=hi%20clicky%20team&body=%3Cjk%20my%20twitter%20dm%27s%20got%20way%20too%20crazy%2C%20so%20email%20me%20here%3E");
        })
      });
    }

    // Token: 0x06000368 RID: 872 RVA: 0x00520DB4 File Offset: 0x00520DB4
    private async Task PointAtSomethingInterestingAsync()
    {
      RealStack real = this.Real;
      if (real != null && real.Core.Auth.IsSignedIn)
      {
        await OnboardingDemoPointer.RunAsync(real.Core, real.Capture, this.Services.Cursor, () => real.Companion.State);
      }
    }

    // Token: 0x06000369 RID: 873 RVA: 0x00520DF8 File Offset: 0x00520DF8
    private void LaunchOnboardingDemoAgent(StarterCast.Member member, string spokenAsk, string prompt)
    {
      AppHost.<>c__DisplayClass240_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass240_0();
      CS$<>8__locals1.prompt = prompt;
      CS$<>8__locals1.spokenAsk = spokenAsk;
      CS$<>8__locals1.member = member;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = this.Real;
      if (CS$<>8__locals1.real == null || !CS$<>8__locals1.real.Core.Auth.IsSignedIn)
      {
        Log.Warn("video", "demo agent skipped: not signed in");
        return;
      }
      CS$<>8__locals1.context = "HeyClicky launched this job programmatically as part of the onboarding demo reel. Treat it as a canned demo, not a live user request. The user is brand new and is watching your progress, so your FIRST commentary message must be a short friendly line (two sentences, no list) that says what you're about to make for them. Every later progress message is one short plain line, like a text to a friend.";
      CS$<>8__locals1.slug = null;
      Task.Run(delegate
      {
        AppHost.<>c__DisplayClass240_0.<<LaunchOnboardingDemoAgent>b__0>d <<LaunchOnboardingDemoAgent>b__0>d;
        <<LaunchOnboardingDemoAgent>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<LaunchOnboardingDemoAgent>b__0>d.<>4__this = CS$<>8__locals1;
        <<LaunchOnboardingDemoAgent>b__0>d.<>1__state = -1;
        <<LaunchOnboardingDemoAgent>b__0>d.<>t__builder.Start<AppHost.<>c__DisplayClass240_0.<<LaunchOnboardingDemoAgent>b__0>d>(ref <<LaunchOnboardingDemoAgent>b__0>d);
        return <<LaunchOnboardingDemoAgent>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x0600036A RID: 874 RVA: 0x00520E88 File Offset: 0x00520E88
    private void StartOnboardingMusic(double volume)
    {
      if (Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1")
      {
        return;
      }
      try
      {
        string text = AppHost.AssetPath(new string[] { "Audio", "ff.m4a" });
        if (File.Exists(text))
        {
          MediaPlayer mediaPlayer = this.music;
          if (mediaPlayer != null)
          {
            mediaPlayer.Dispose();
          }
          this.music = new MediaPlayer
          {
            Source = MediaSource.CreateFromUri(new Uri(text)),
            IsLoopingEnabled = true,
            Volume = volume,
            AudioCategory = MediaPlayerAudioCategory.Media
          };
          this.music.Play();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("music", ex.Message);
      }
    }

    // Token: 0x0600036B RID: 875 RVA: 0x00520F48 File Offset: 0x00520F48
    private void FadeOutOnboardingMusic(double seconds)
    {
      MediaPlayer mp = this.music;
      if (mp == null)
      {
        return;
      }
      this.music = null;
      double from = mp.Volume;
      int step = 0;
      DispatcherQueueTimer t = this.DispatcherQueue.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(seconds / 30.0);
      this.hostTimers.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        int step2 = step;
        step = step2 + 1;
        mp.Volume = Math.Max(0.0, from * (1.0 - (double)step / 30.0));
        if (step < 30)
        {
          return;
        }
        t.Stop();
        this.hostTimers.Remove(t);
        try
        {
          mp.Pause();
          mp.Dispose();
        }
        catch (Exception ex)
        {
          Log.Warn("music", "stop: " + ex.Message);
        }
      };
      t.Start();
    }

    // Token: 0x0600036C RID: 876 RVA: 0x00520FFC File Offset: 0x00520FFC
    private void AfterSeconds(double seconds, Action a)
    {
      DispatcherQueueTimer t = this.DispatcherQueue.CreateTimer().Rooted();
      t.Interval = TimeSpan.FromSeconds(Math.Max(0.001, seconds));
      t.IsRepeating = false;
      this.hostTimers.Add(t);
      t.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.hostTimers.Remove(t);
        a();
      };
      t.Start();
    }

    // Token: 0x0600036D RID: 877 RVA: 0x00521094 File Offset: 0x00521094
    [NullableContext(2)]
    private static Uri IntroVideoUri(IntroVideoSourceKind kind)
    {
      if (kind == IntroVideoSourceKind.Hosted)
      {
        return new Uri("https://stream.mux.com/7bOYF1PGo02RKwcQW00L00Aa01iRKElKXoch4xhtsJzv8QU.m3u8");
      }
      string text = AppHost.AssetPath(new string[] { "Video", "onboarding-intro-v2-windows.mp4" });
      if (!File.Exists(text))
      {
        return null;
      }
      return new Uri(text);
    }

    // Token: 0x0600036E RID: 878 RVA: 0x005210DC File Offset: 0x005210DC
    [return: Nullable(2)]
    private MediaPlayer MakeIntroPlayer(IntroVideoSourceKind kind, string phase, bool muted)
    {
      Uri uri = AppHost.IntroVideoUri(kind);
      if (uri == null)
      {
        this.introVideoFailures.Report(new HostedVideoFailure("onboarding", phase, "bundled_resource_missing", "Assets/Video/onboarding-intro-v2-windows.mp4", "ClickyBundledVideo", -1, "Bundled onboarding video resource is missing", "none", "none", "none", null));
        return null;
      }
      MediaPlayer mediaPlayer;
      try
      {
        mediaPlayer = CursorCompanion.CreatePlayer(uri, muted);
      }
      catch (Exception ex)
      {
        this.introVideoFailures.Report(new HostedVideoFailure("onboarding", phase, "player_create_failed", IntroVideoSources.Redacted(uri.ToString()), "MediaPlayer", ex.HResult, ex.Message, "none", "none", "none", null));
        mediaPlayer = null;
      }
      return mediaPlayer;
    }

    // Token: 0x0600036F RID: 879 RVA: 0x005211AC File Offset: 0x005211AC
    [return: TupleElementNames(new string[] { "Player", "Source" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<MediaPlayer, IntroVideoSourceKind>? MakeIntroPlayerInOrder(string phase, bool muted)
    {
      IReadOnlyList<IntroVideoSourceKind> readOnlyList = IntroVideoSources.PlaybackOrder(IntroVideoSources.PrefersHosted);
      MediaPlayer mediaPlayer = this.MakeIntroPlayer(readOnlyList[0], phase, muted);
      if (mediaPlayer != null)
      {
        return new ValueTuple<MediaPlayer, IntroVideoSourceKind>?(new ValueTuple<MediaPlayer, IntroVideoSourceKind>(mediaPlayer, readOnlyList[0]));
      }
      MediaPlayer mediaPlayer2 = this.MakeIntroPlayer(readOnlyList[1], phase + "_" + IntroVideoSources.AnalyticsName(readOnlyList[1]) + "_fallback", muted);
      if (mediaPlayer2 != null)
      {
        return new ValueTuple<MediaPlayer, IntroVideoSourceKind>?(new ValueTuple<MediaPlayer, IntroVideoSourceKind>(mediaPlayer2, readOnlyList[1]));
      }
      return null;
    }

    // Token: 0x06000370 RID: 880 RVA: 0x00521234 File Offset: 0x00521234
    private void PreloadOnboardingVideoIfNeeded()
    {
      if (this.preloadedIntroPlayer != null || this.HasCompletedOnboardingNow || this.Options.UseFakes)
      {
        return;
      }
      ValueTuple<MediaPlayer, IntroVideoSourceKind>? valueTuple = this.MakeIntroPlayerInOrder("preload", true);
      if (valueTuple != null)
      {
        ValueTuple<MediaPlayer, IntroVideoSourceKind> valueOrDefault = valueTuple.GetValueOrDefault();
        this.AdoptPreload(valueOrDefault.Item1, valueOrDefault.Item2, true);
        return;
      }
    }

    // Token: 0x06000371 RID: 881 RVA: 0x00521294 File Offset: 0x00521294
    private void AdoptPreload(MediaPlayer mp, IntroVideoSourceKind source, bool fallbackAllowed)
    {
      this.preloadedIntroPlayer = mp;
      this.preloadedIntroSource = source;
      Log.Info("video", "intro reel preloading (" + IntroVideoSources.AnalyticsName(source) + ")");
      mp.MediaOpened += delegate(MediaPlayer _, object _)
      {
        Log.Info("video", "intro reel preloaded (" + IntroVideoSources.AnalyticsName(source) + ")");
      };
      mp.MediaFailed += delegate(MediaPlayer _, MediaPlayerFailedEventArgs e)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          if (this.preloadedIntroPlayer != mp)
          {
            return;
          }
          this.introVideoFailures.Report(AppHost.Failure("preload", (source == IntroVideoSourceKind.Hosted) ? "player_status_failed" : "bundled_player_status_failed", source, e));
          this.preloadedIntroPlayer = null;
          try
          {
            mp.Dispose();
          }
          catch
          {
          }
          IntroVideoSourceKind introVideoSourceKind = IntroVideoSources.Alternate(source);
          if (fallbackAllowed)
          {
            MediaPlayer mediaPlayer = this.MakeIntroPlayer(introVideoSourceKind, (source == IntroVideoSourceKind.Hosted) ? "preload_fallback" : "preload_hosted_fallback", true);
            if (mediaPlayer != null)
            {
              this.AdoptPreload(mediaPlayer, introVideoSourceKind, false);
            }
          }
        });
      };
    }

    // Token: 0x06000372 RID: 882 RVA: 0x00521330 File Offset: 0x00521330
    [return: TupleElementNames(new string[] { "Player", "Source" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<MediaPlayer, IntroVideoSourceKind>? TakeIntroPlayer()
    {
      MediaPlayer mediaPlayer = this.preloadedIntroPlayer;
      if (mediaPlayer != null)
      {
        this.preloadedIntroPlayer = null;
        return new ValueTuple<MediaPlayer, IntroVideoSourceKind>?(new ValueTuple<MediaPlayer, IntroVideoSourceKind>(mediaPlayer, this.preloadedIntroSource));
      }
      return this.MakeIntroPlayerInOrder("playback", false);
    }

    // Token: 0x06000373 RID: 883 RVA: 0x0052136C File Offset: 0x0052136C
    private bool PlayIntroReel([TupleElementNames(new string[] { "Player", "Source" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<MediaPlayer, IntroVideoSourceKind> made, IReadOnlyList<CursorCompanion.Cue> cues, bool isFallback)
    {
      AppHost.<>c__DisplayClass254_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass254_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.cues = cues;
      CS$<>8__locals1.isFallback = isFallback;
      CS$<>8__locals1.mp = made.Item1;
      CS$<>8__locals1.source = made.Item2;
      if (!CS$<>8__locals1.isFallback)
      {
        this.introVideoFailures.Reset();
      }
      CursorCompanion cursorCompanion = this.CursorCompanion;
      MediaPlayer mp = CS$<>8__locals1.mp;
      Uri uri = AppHost.IntroVideoUri(CS$<>8__locals1.source);
      bool flag = cursorCompanion.PlayVideo(mp, IntroVideoSources.Redacted((uri != null) ? uri.ToString() : null), IntroVideoSources.AnalyticsName(CS$<>8__locals1.source), CS$<>8__locals1.cues, new Action(this.FinishOnboardingVideoAndShowPrompt), new Func<MediaPlayerFailedEventArgs, bool>(CS$<>8__locals1.<PlayIntroReel>g__Failed|0), 2.0, 60.0, 5.0, true);
      if (flag && !CS$<>8__locals1.isFallback && CS$<>8__locals1.source == IntroVideoSourceKind.Hosted && IntroVideoSources.ForcesHostedFailure)
      {
        this.AfterSeconds(0.1, delegate
        {
          if (CS$<>8__locals1.<>4__this.CursorCompanion.CurrentPlayer != CS$<>8__locals1.mp)
          {
            return;
          }
          HostedVideoFailure hostedVideoFailure = new HostedVideoFailure("onboarding", "playback", "debug_forced_hosted_failure", "https://stream.mux.com/7bOYF1PGo02RKwcQW00L00Aa01iRKElKXoch4xhtsJzv8QU.m3u8", "ClickyHostedVideoDebug", -9001, "Debug-forced hosted intro video failure", "none", "none", "none", null);
          if (!CS$<>8__locals1.<>4__this.FallBackAfterIntroFailure(CS$<>8__locals1.mp, CS$<>8__locals1.source, hostedVideoFailure, CS$<>8__locals1.cues, false))
          {
            CS$<>8__locals1.<>4__this.CursorCompanion.DismissVideo();
          }
        });
      }
      return flag;
    }

    // Token: 0x06000374 RID: 884 RVA: 0x0052146C File Offset: 0x0052146C
    private bool FallBackAfterIntroFailure(MediaPlayer failed, IntroVideoSourceKind source, HostedVideoFailure failure, IReadOnlyList<CursorCompanion.Cue> cues, bool isFallback)
    {
      this.introVideoFailures.Report(failure);
      if (isFallback || this.CursorCompanion.CurrentPlayer != failed)
      {
        return false;
      }
      IntroVideoSourceKind introVideoSourceKind = IntroVideoSources.Alternate(source);
      string text = ((source == IntroVideoSourceKind.Hosted) ? "playback_fallback_after_hosted_failure" : "playback_fallback_after_bundled_failure");
      MediaPlayer mediaPlayer = this.MakeIntroPlayer(introVideoSourceKind, text, true);
      if (mediaPlayer == null)
      {
        return false;
      }
      Log.Info("video", "intro reel: " + IntroVideoSources.AnalyticsName(source) + " failed, starting over on " + IntroVideoSources.AnalyticsName(introVideoSourceKind));
      return this.PlayIntroReel(new ValueTuple<MediaPlayer, IntroVideoSourceKind>(mediaPlayer, introVideoSourceKind), cues, true);
    }

    // Token: 0x06000375 RID: 885 RVA: 0x005214FC File Offset: 0x005214FC
    public void StartOnboardingVideoDemoDryRun()
    {
      foreach (DispatcherQueueTimer dispatcherQueueTimer in this.dryRunTimers)
      {
        dispatcherQueueTimer.Stop();
      }
      this.dryRunTimers.Clear();
      IReadOnlyList<CursorCompanion.Cue> readOnlyList = this.OnboardingVideoCues(false);
      string text = "video";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
      defaultInterpolatedStringHandler.AppendLiteral("dry run with ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" cues");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      using (IEnumerator<CursorCompanion.Cue> enumerator2 = readOnlyList.GetEnumerator())
      {
        while (enumerator2.MoveNext())
        {
          CursorCompanion.Cue cue = enumerator2.Current;
          DispatcherQueueTimer t = this.DispatcherQueue.CreateTimer().Rooted();
          t.Interval = TimeSpan.FromSeconds(cue.AtSeconds);
          t.IsRepeating = false;
          t.Tick += delegate(DispatcherQueueTimer _, object _)
          {
            this.dryRunTimers.Remove(t);
            string text2 = "video";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(15, 1);
            defaultInterpolatedStringHandler2.AppendLiteral("dry run: cue @");
            defaultInterpolatedStringHandler2.AppendFormatted<double>(cue.AtSeconds, "0");
            defaultInterpolatedStringHandler2.AppendLiteral("s");
            Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
            try
            {
              cue.Fire();
            }
            catch (Exception ex)
            {
              string text3 = "video";
              defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(15, 2);
              defaultInterpolatedStringHandler2.AppendLiteral("dry run cue @");
              defaultInterpolatedStringHandler2.AppendFormatted<double>(cue.AtSeconds);
              defaultInterpolatedStringHandler2.AppendLiteral(": ");
              defaultInterpolatedStringHandler2.AppendFormatted(ex.Message);
              Log.Warn(text3, defaultInterpolatedStringHandler2.ToStringAndClear());
            }
          };
          this.dryRunTimers.Add(t);
          t.Start();
        }
      }
    }

    // Token: 0x06000376 RID: 886 RVA: 0x00521664 File Offset: 0x00521664
    private static HostedVideoFailure Failure(string phase, string reason, IntroVideoSourceKind source, MediaPlayerFailedEventArgs e)
    {
      string text = "onboarding";
      Uri uri = AppHost.IntroVideoUri(source);
      string text2 = IntroVideoSources.Redacted((uri != null) ? uri.ToString() : null);
      string text3 = "MediaPlayer";
      Exception extendedErrorCode = e.ExtendedErrorCode;
      return new HostedVideoFailure(text, phase, reason, text2, text3, (extendedErrorCode != null) ? extendedErrorCode.HResult : ((int)e.Error), string.IsNullOrEmpty(e.ErrorMessage) ? e.Error.ToString() : e.ErrorMessage, "failed", "failed", "none", null);
    }

    // Token: 0x1700007D RID: 125
    // (get) Token: 0x06000377 RID: 887 RVA: 0x005216F4 File Offset: 0x005216F4
    [Nullable(2)]
    public LessonService Lessons
    {
      [NullableContext(2)]
      get
      {
        LessonService lessonService;
        if ((lessonService = this.lessons) == null)
        {
          RealStack real = this.Real;
          object obj;
          if (real == null || !real.IsComplete)
          {
            obj = null;
          }
          else
          {
            (obj = new LessonService(real, this.DispatcherQueue, this.ExcludeFromCapture, () => this.Services.Settings.CursorColorHex)).NotchPoint = delegate
            {
              Displays.Monitor monitor = Displays.ForWindow(this.NotchUi.Window.Hwnd);
              return new ValueTuple<int, int>(monitor.Bounds.Left + monitor.Bounds.Width / 2, monitor.Bounds.Top);
            };
          }
          lessonService = (this.lessons = obj);
        }
        return lessonService;
      }
    }

    // Token: 0x06000378 RID: 888 RVA: 0x0052175C File Offset: 0x0052175C
    private void StartNotch()
    {
      if (!NotchExtensions.DocumentRenderers.OfType<NotchDocumentRenderer>().Any<NotchDocumentRenderer>())
      {
        NotchDocumentRenderer.Register();
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.HoverOpenedNotch += delegate
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            Sounds.Play("clicky-text-open", 0.22);
          });
        };
        NotchPresenter notchPresenter = corePresenter;
        Action action;
        if ((action = AppHost.<>O.<6>__TrackNotchFileDragAbandoned) == null)
        {
          action = (AppHost.<>O.<6>__TrackNotchFileDragAbandoned = new Action(ClickyAnalytics.TrackNotchFileDragAbandoned));
        }
        notchPresenter.DragOpenedChatAbandoned += action;
        corePresenter.Tabs.SelectionChanged += delegate(NotchTab _)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.EndChatSessionIfLeftBehind));
        };
        corePresenter.NotchSessionChanged += delegate
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchSizeContext));
        };
        corePresenter.Tabs.EntryLookup = delegate(string id)
        {
          ChatHandOffs handOffs = NotchChat.HandOffs;
          if (handOffs == null)
          {
            return null;
          }
          return handOffs.Entry(id);
        };
        corePresenter.Tabs.OpensFullReplyInWindow = delegate(string id)
        {
          ChatHandOffs handOffs2 = NotchChat.HandOffs;
          ChatHandOff chatHandOff = ((handOffs2 != null) ? handOffs2.Entry(id) : null);
          return chatHandOff != null && this.ShowInHeyClickyWindowIfOpen(this.WindowAgentKey(chatHandOff.JobId, chatHandOff.ClickySlug, chatHandOff.ThreadId), null, null, "full_reply");
        };
        corePresenter.Updates.JobThreadId = delegate(string id)
        {
          RealStack real3 = this.Real;
          if (real3 == null)
          {
            return null;
          }
          ClickyJob clickyJob = real3.Agents.Jobs.Job(id);
          if (clickyJob == null)
          {
            return null;
          }
          return clickyJob.ThreadId;
        };
        corePresenter.PresentationChanged += delegate([Nullable(2)] object _, EventArgs _)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OnNotchPresentationForDock));
        };
        this.NotchUi.SizeContext.ClickyUpdateHeight = new Func<NotchLiveEvent.ClickyUpdate, double>(this.ClickyUpdateHeight);
      }
      if (NotchExtensions.UpdateContent == null)
      {
        RealStack real6 = this.Real;
        NotchExtensions.UpdateContent = ((real6 != null) ? new WorkNotchUpdates(real6.Agents, delegate
        {
          RealStack real4 = this.Real;
          if (real4 == null)
          {
            return null;
          }
          return real4.JobReports;
        }, this) : null);
      }
      if (NotchExtensions.RestingMarkState == null)
      {
        NotchExtensions.RestingMarkState = new Func<NotchRestingMarkState>(this.RestingMarkFromWork);
      }
      if (NotchExtensions.LiveWork == null)
      {
        RealStack real2 = this.Real;
        NotchExtensions.LiveWork = ((real2 != null) ? new WorkChatLiveWork(real2.Agents, delegate
        {
          RealStack real5 = this.Real;
          if (real5 == null)
          {
            return null;
          }
          return real5.JobReports;
        }) : null);
      }
      this.AttachClickyRoster();
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      real.Agents.Routines.ProductEvent += this.TrackRoutineEvent;
      real.Agents.ClickyFounded += delegate(string slug)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          ClickyRoster.Shared.TrackCreated(slug, "agent", 0);
        });
      };
      NotchChat.History = new ConversationHistory(() => real.Core.Auth.UserId, ConversationHistory.WorkerFetcher(real.Core.Worker), null, null);
      ChatHandOffs chatHandOffs = new ChatHandOffs(() => real.Core.Auth.UserId, null, null);
      chatHandOffs.Directory = new WorkHandOffDirectory(real.Agents);
      chatHandOffs.RefreshHistorySoon = delegate
      {
        ConversationHistory history = NotchChat.History;
        if (history == null)
        {
          return;
        }
        history.Refresh();
      };
      chatHandOffs.TakeRestartInterruptions = () => (from t in real.Agents.RestartInterruptions.TakeInterruptionsForChat()
        select new ValueTuple<string, string, DateTimeOffset?>(t.Slug, t.ThreadId, t.InterruptedAt)).ToList<ValueTuple<string, string, DateTimeOffset?>>();
      NotchChat.HandOffs = chatHandOffs;
      NotchChat.History.Changed += delegate
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.EndChatSessionIfLeftBehind));
      };
      NotchChat.Lessons = this.Lessons;
      Func<string, string> func;
      if ((func = AppHost.<>O.<7>__Strip) == null)
      {
        func = (AppHost.<>O.<7>__Strip = new Func<string, string>(CitationMarkers.Strip));
      }
      ChatReplyPreview.StripCitationMarkers = func;
      FlowHooks.RendersTextInNotchChat = delegate
      {
        NotchPresenter corePresenter2 = this.CorePresenter;
        return corePresenter2 != null && corePresenter2.RendersTextResponsesInNotchChat;
      };
      if (!this.observingNotchLinkOpens)
      {
        this.observingNotchLinkOpens = true;
        Markdown.LinkOpened += delegate([Nullable(2)] XamlRoot _, Uri _)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.FoldNotchForOpenedLink));
        };
      }
      FlowHooks.StepNotchChatAsideForDrawing = delegate
      {
        NotchPresenter corePresenter3 = this.CorePresenter;
        if (corePresenter3 == null || !corePresenter3.IsNotchChatSessionOpen)
        {
          return false;
        }
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.StepNotchChatAsideForDrawing));
        return true;
      };
      FlowHooks.NotchNote = new Func<string>(this.NotchFullReplyFocusNote);
      real.OpenHome = delegate(string _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OpenNotchHome));
        return true;
      };
      DispatcherQueueHandler <>9__36;
      real.CloseHome = delegate
      {
        DispatcherQueue dispatcherQueue = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = <>9__36) == null)
        {
          dispatcherQueueHandler = (<>9__36 = delegate
          {
            NotchPresenter corePresenter4 = this.CorePresenter;
            if (corePresenter4 == null)
            {
              return;
            }
            corePresenter4.DismissNotchAgentPeek();
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        return true;
      };
      AgentToolHost.Launched = delegate(AgentToolLaunch launch, string taskId)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.NoteHandOffStarted(real.Agents, taskId, launch.Prompt, false, 0);
        });
      };
      AgentToolHost.Steered = delegate(string taskId, string text)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.NoteHandOffStarted(real.Agents, taskId, text, true, 0);
        });
      };
      real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        AgentTaskEventKind kind = e.Kind;
        bool flag = kind == AgentTaskEventKind.StatusChanged || kind - AgentTaskEventKind.Finished <= 3;
        if (flag)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.OnAgentTaskStatus(real.Agents, e.TaskId);
          });
        }
      };
      real.Agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncRestingMark));
      };
      real.Agents.Jobs.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncRestingMark));
      };
      OneBrainJobReports jobReports = real.JobReports;
      if (jobReports != null)
      {
        jobReports.PresentingChanged += delegate
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncRestingMark));
        };
      }
      this.WireFinishedWork(real);
      DispatcherQueueHandler <>9__40;
      real.Companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue2 = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler2;
        if ((dispatcherQueueHandler2 = <>9__40) == null)
        {
          dispatcherQueueHandler2 = (<>9__40 = delegate
          {
            this.FinishChatAnswerIfDone(real);
          });
        }
        dispatcherQueue2.TryEnqueue(dispatcherQueueHandler2);
      };
      real.Companion.TextDelivered += delegate(string text, [Nullable(2)] string app, bool copied)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          ChatHandOffs handOffs3 = NotchChat.HandOffs;
          if (handOffs3 == null)
          {
            return;
          }
          handOffs3.NoteTextDelivered(text, app, copied);
        });
      };
      real.Companion.TurnFinished += delegate([Nullable(2)] object _, CompanionTurnFinish finish)
      {
        if (!finish.WordedTurn)
        {
          return;
        }
        double[] array = new double[] { 1.5, 4.0 };
        for (int i = 0; i < array.Length; i++)
        {
          _ = Task.Delay(TimeSpan.FromSeconds(array[i])).ContinueWith(delegate(Task _)
          {
            ConversationHistory history2 = NotchChat.History;
            if (history2 == null)
            {
              return;
            }
            history2.Refresh();
          }, TaskScheduler.Default);
        }
      };
      real.Core.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchRouting));
      };
      real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchRouting));
      };
      real.Tutorial.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchRouting));
      };
      this.Services.Permissions.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchRouting));
      };
      this.SyncNotchRouting();
      this.SyncRestingMark();
    }

    // Token: 0x06000379 RID: 889 RVA: 0x00521C44 File Offset: 0x00521C44
    private void AttachClickyRoster()
    {
      if (this.rosterAttached)
      {
        return;
      }
      this.rosterAttached = true;
      ClickyRoster shared = ClickyRoster.Shared;
      RealStack real = this.Real;
      AgentService agentService = ((real != null) ? real.Agents : null);
      IHomeSource homeSource2;
      if (agentService == null)
      {
        IHomeSource homeSource = new DemoHomeSource();
        homeSource2 = homeSource;
      }
      else
      {
        IHomeSource homeSource = new AgentHomeSource(agentService);
        homeSource2 = homeSource;
      }
      shared.Attach(homeSource2);
      ClickyRoster.IsOnboardingEngaged = delegate
      {
        RealStack real3 = this.Real;
        if (real3 == null)
        {
          return false;
        }
        OnboardingTutorialViewModel onboardingTutorialViewModel = real3.Tutorial;
        return ((onboardingTutorialViewModel != null) ? new bool?(onboardingTutorialViewModel.IsEngaged) : null).GetValueOrDefault();
      };
      RealStack real2 = this.Real;
      AgentService agentService2 = ((real2 != null) ? real2.Agents : null);
      if (agentService2 != null)
      {
        agentService2.Jobs.CloudColorsInUseByClickys = new Func<IReadOnlyList<string>>(ClickyRoster.Shared.CloudColorsInUse);
      }
    }

    // Token: 0x0600037A RID: 890 RVA: 0x00521CD4 File Offset: 0x00521CD4
    private void TrackRoutineEvent(string productEvent, [Nullable(2)] string clickySlug, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> properties)
    {
      JsonObject jsonObject = new JsonObject(null);
      foreach (KeyValuePair<string, object> keyValuePair in properties)
      {
        string text;
        object obj;
        keyValuePair.Deconstruct(out text, out obj);
        string text2 = text;
        object obj2 = obj;
        JsonObject jsonObject2 = jsonObject;
        text = text2;
        JsonValue jsonValue;
        if (obj2 != null)
        {
          if (obj2 is bool)
          {
            bool flag = (bool)obj2;
            jsonValue = JsonValue.Create(flag, null);
          }
          else if (obj2 is int)
          {
            int num = (int)obj2;
            jsonValue = JsonValue.Create(num, null);
          }
          else if (obj2 is long)
          {
            long num2 = (long)obj2;
            jsonValue = JsonValue.Create(num2, null);
          }
          else if (obj2 is double)
          {
            double num3 = (double)obj2;
            jsonValue = JsonValue.Create(num3, null);
          }
          else
          {
            string text3 = obj2 as string;
            if (text3 == null)
            {
              jsonValue = JsonValue.Create(obj2.ToString(), null);
            }
            else
            {
              jsonValue = JsonValue.Create(text3, null);
            }
          }
        }
        else
        {
          jsonValue = null;
        }
        jsonObject2[text] = jsonValue;
      }
      if (productEvent == "clicky_routine_created")
      {
        JsonNode jsonNode = jsonObject;
        string text4 = "is_onboarding";
        RealStack real = this.Real;
        bool flag2;
        if (real == null)
        {
          flag2 = false;
        }
        else
        {
          OnboardingTutorialViewModel onboardingTutorialViewModel = real.Tutorial;
          flag2 = ((onboardingTutorialViewModel != null) ? new bool?(onboardingTutorialViewModel.IsEngaged) : null).GetValueOrDefault();
        }
        jsonNode[text4] = flag2;
      }
      ClickyAnalytics.TrackProductEvent(productEvent, clickySlug, null, jsonObject);
    }

    // Token: 0x0600037B RID: 891 RVA: 0x00521E8C File Offset: 0x00521E8C
    private void SyncNotchRouting()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      bool isSignedIn = this.Services.Auth.IsSignedIn;
      bool hasCompletedOnboardingNow = this.HasCompletedOnboardingNow;
      RealStack real = this.Real;
      bool flag = real != null && real.Tutorial.IsEngaged;
      corePresenter.SetHoverRoutesToNotch(isSignedIn && hasCompletedOnboardingNow && this.AllPermissionsGranted && !DrawnOnboardingTour.SuppressesNotchHover);
      corePresenter.SetRoutesTextModeToNotchChat(isSignedIn && hasCompletedOnboardingNow && !flag && !this.introSequenceActive);
      this.SyncRestingMark();
    }

    // Token: 0x0600037C RID: 892 RVA: 0x00521F14 File Offset: 0x00521F14
    private void OnNotchHoverDwell()
    {
      if (!this.Services.Auth.IsSignedIn)
      {
        return;
      }
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
      if ((notchOnboardingCardViewModel != null && notchOnboardingCardViewModel.IsIntroHomeWelcomeActive) || this.fakeIntroWelcomeActive)
      {
        return;
      }
      ProactiveCoordinator proactive = this.Proactive;
      if (proactive != null)
      {
        proactive.RefreshMeetingsForNotchExpand();
      }
      RealStack real = this.Real;
      OnboardingTutorialViewModel onboardingTutorialViewModel = ((real != null) ? real.Tutorial : null);
      if (onboardingTutorialViewModel != null && onboardingTutorialViewModel.IsRestrictingNormalUse)
      {
        this.ShowTutorialResumeCard();
      }
    }

    // Token: 0x0600037D RID: 893 RVA: 0x00521F84 File Offset: 0x00521F84
    internal void SimulateNotchHoverDwell()
    {
      this.OnNotchHoverDwell();
    }

    // Token: 0x0600037E RID: 894 RVA: 0x00521F8C File Offset: 0x00521F8C
    public void OpenSettings()
    {
      this.OpenSettings(NotchSettingsSection.Account);
    }

    // Token: 0x0600037F RID: 895 RVA: 0x00521F98 File Offset: 0x00521F98
    public void OpenSettings(NotchSettingsSection section)
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      INotchHomeDock homeDock = NotchExtensions.HomeDock;
      if (homeDock != null)
      {
        homeDock.NotchShowsHome(false);
      }
      corePresenter.Tabs.ShowSettings(section);
      Log.Info("notch", "settings: " + section.RawValue());
    }

    // Token: 0x06000380 RID: 896 RVA: 0x00521FE8 File Offset: 0x00521FE8
    public void OpenSettings(SettingsWindow.Page page)
    {
      this.OpenSettings(NotchSettingsTab.ToSection(page));
    }

    // Token: 0x1700007E RID: 126
    // (get) Token: 0x06000381 RID: 897 RVA: 0x00521FF8 File Offset: 0x00521FF8
    private bool NotchShowsSettings
    {
      get
      {
        NotchPresenter corePresenter = this.CorePresenter;
        return corePresenter != null && corePresenter.IsShowingNotchAgentPeek && corePresenter.Tabs.Selected == NotchTab.Settings;
      }
    }

    // Token: 0x06000382 RID: 898 RVA: 0x00522028 File Offset: 0x00522028
    public void OpenNotchHome()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null || this.introSequenceActive)
      {
        return;
      }
      corePresenter.OpenNotchAgentPeekOnRequest();
    }

    // Token: 0x06000383 RID: 899 RVA: 0x00522050 File Offset: 0x00522050
    [NullableContext(2)]
    public void ShowInChat(string jobId, string clickySlug)
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      string text = ((handOffs != null) ? handOffs.LatestEntryId(jobId, clickySlug) : null);
      corePresenter.Tabs.Show(NotchTab.Chat, text);
      corePresenter.OpenNotchAgentPeekOnRequest();
    }

    // Token: 0x06000384 RID: 900 RVA: 0x00522090 File Offset: 0x00522090
    public bool OpenDocumentInNotch(string path, [Nullable(2)] string threadId)
    {
      if (!ArtifactPaths.SupportsInline(path))
      {
        return false;
      }
      HeyClickyWindow shared = HeyClickyWindow.Shared;
      if (shared != null && shared.IsOpen && shared.IsInFront)
      {
        shared.ShowDocument(Path.GetFullPath(path), threadId);
        return true;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return false;
      }
      corePresenter.Tabs.ShowDocument(Path.GetFullPath(path), threadId);
      return true;
    }

    // Token: 0x06000385 RID: 901 RVA: 0x005220F0 File Offset: 0x005220F0
    [NullableContext(2)]
    public bool OpenAgentDocumentInViewer([Nullable(1)] string path, string threadId, string clickySlug)
    {
      return ArtifactPaths.SupportsInline(path) && (this.ShowInHeyClickyWindowIfOpen(this.WindowAgentKey(null, clickySlug, threadId), Path.GetFullPath(path), threadId, "document") || this.OpenDocumentInNotch(path, threadId));
    }

    // Token: 0x06000386 RID: 902 RVA: 0x00522124 File Offset: 0x00522124
    [NullableContext(2)]
    private bool ShowInHeyClickyWindowIfOpen(string agentKey, string documentPath = null, string documentThreadId = null, [Nullable(1)] string from = "notch")
    {
      HeyClickyWindow shared = HeyClickyWindow.Shared;
      if (!shared.IsOpenOnScreen || agentKey == null)
      {
        return false;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.IsShowingNotchAgentPeek)
      {
        corePresenter.DismissNotchAgentPeek();
      }
      shared.Open(agentKey, false, documentPath, documentThreadId, from, null, SettingsWindow.Page.Account);
      return true;
    }

    // Token: 0x06000387 RID: 903 RVA: 0x0052216C File Offset: 0x0052216C
    [NullableContext(2)]
    private string WindowAgentKey(string jobId, string clickySlug, string threadId)
    {
      RealStack real = this.Real;
      AgentService agentService = ((real != null) ? real.Agents : null);
      if (agentService == null)
      {
        return null;
      }
      if (jobId != null)
      {
        ClickyJob clickyJob = agentService.Jobs.Job(jobId);
        if (clickyJob != null)
        {
          return clickyJob.RailKey;
        }
      }
      if (clickySlug != null && agentService.FindClicky(clickySlug) != null)
      {
        return clickySlug;
      }
      if (threadId == null)
      {
        return null;
      }
      ClickyJob clickyJob2 = agentService.Jobs.JobForThread(threadId);
      if (clickyJob2 != null)
      {
        return clickyJob2.RailKey;
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      string text;
      if (handOffs == null)
      {
        text = null;
      }
      else
      {
        IChatHandOffDirectory directory = handOffs.Directory;
        text = ((directory != null) ? directory.ClickySlugForThread(threadId) : null);
      }
      string text2 = text;
      if (text2 == null || agentService.FindClicky(text2) == null)
      {
        return null;
      }
      return text2;
    }

    // Token: 0x06000388 RID: 904 RVA: 0x00522204 File Offset: 0x00522204
    public void FoldNotchForOpenedLink()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.IsShowingNotchAgentPeek)
      {
        corePresenter.DismissNotchAgentPeek();
      }
    }

    // Token: 0x06000389 RID: 905 RVA: 0x0052222C File Offset: 0x0052222C
    private void OnNotchPresentationForDock()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      bool flag = corePresenter != null && corePresenter.IsShowingNotchAgentPeek;
      if (flag == this.notchWasOpen)
      {
        return;
      }
      this.notchWasOpen = flag;
      if (!flag)
      {
        INotchHomeDock homeDock = NotchExtensions.HomeDock;
        if (homeDock == null)
        {
          return;
        }
        homeDock.NotchFolded("the notch folded away");
        return;
      }
      else
      {
        INotchHomeDock homeDock2 = NotchExtensions.HomeDock;
        if (homeDock2 == null)
        {
          return;
        }
        NotchPresenter corePresenter2 = this.CorePresenter;
        homeDock2.NotchShowsHome(corePresenter2 != null && corePresenter2.Tabs.Selected == NotchTab.Home);
        return;
      }
    }

    // Token: 0x0600038A RID: 906 RVA: 0x005222A0 File Offset: 0x005222A0
    private void SyncNotchSizeContext()
    {
      this.NotchUi.Relayout();
    }

    // Token: 0x0600038B RID: 907 RVA: 0x005222B0 File Offset: 0x005222B0
    internal bool ToggleNotchChatFromShortcut()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return false;
      }
      if (!corePresenter.RoutesTextModeToNotchChat && !corePresenter.IsNotchChatSessionOpen)
      {
        return false;
      }
      if (corePresenter.IsNotchChatSessionOpen && corePresenter.NotchChatComposerHasKeyboard && this.NotchUi.HasKeyboard)
      {
        corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.DoubleControl);
        this.NotchUi.ReleaseKeyboard();
        this.Services.Audio.PlayEarcon("textClose");
      }
      else if (corePresenter.IsNotchChatSessionOpen)
      {
        corePresenter.Tabs.ShowChatForTyping(corePresenter.IsShowingNotchAgentPeek);
        corePresenter.Tabs.RequestComposerFocus();
        this.Services.Audio.PlayEarcon("textOpen");
      }
      else
      {
        corePresenter.OpenNotchChatSession(NotchChatSessionOpenVia.DoubleControl);
        this.Services.Audio.PlayEarcon("textOpen");
      }
      return true;
    }

    // Token: 0x0600038C RID: 908 RVA: 0x0052237C File Offset: 0x0052237C
    internal bool HandleNotchEscape()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return false;
      }
      if (corePresenter.Tabs.IsShowingDocument)
      {
        corePresenter.Tabs.CloseDocument();
        return true;
      }
      if (corePresenter.Tabs.ExpandedReplyEntryId != null && corePresenter.Tabs.Selected == NotchTab.Chat && corePresenter.IsShowingNotchAgentPeek)
      {
        corePresenter.Tabs.CloseFullReply(new NotchChatFullReplyCloseVia?(NotchChatFullReplyCloseVia.Escape));
        return true;
      }
      if (!corePresenter.IsNotchChatSessionOpen)
      {
        return false;
      }
      bool flag = corePresenter.IsTextResponseInProgress;
      if (!flag)
      {
        VoiceState state = this.Services.Companion.State;
        bool flag2 = state - VoiceState.Processing <= 1;
        flag = flag2;
      }
      if (flag)
      {
        this.Services.Companion.Cancel();
        corePresenter.Present(new NotchPresentation.Idle());
        ConversationHistory history = NotchChat.History;
        if (history != null)
        {
          history.NoteAnswerStopped();
        }
      }
      else
      {
        corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.Escape);
        this.NotchUi.ReleaseKeyboard();
        this.Services.Audio.PlayEarcon("textClose");
      }
      return true;
    }

    // Token: 0x0600038D RID: 909 RVA: 0x00522470 File Offset: 0x00522470
    internal bool NotchEscapeWouldClaim()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      return corePresenter != null && (corePresenter.IsNotchChatSessionOpen || this.NotchViewerEscapeWouldClaim());
    }

    // Token: 0x0600038E RID: 910 RVA: 0x0052249C File Offset: 0x0052249C
    internal bool NotchViewerEscapeWouldClaim()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      return corePresenter != null && (corePresenter.IsNotchDocumentOpen || (corePresenter.Tabs.ExpandedReplyEntryId != null && corePresenter.IsShowingNotchAgentPeek));
    }

    // Token: 0x0600038F RID: 911 RVA: 0x005224D4 File Offset: 0x005224D4
    private void EndChatSessionIfLeftBehind()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      ConversationHistory history = NotchChat.History;
      corePresenter.EndNotchChatSessionIfLeftBehind(history != null && history.IsAwaitingAnswerToSentMessage);
    }

    // Token: 0x06000390 RID: 912 RVA: 0x005224F8 File Offset: 0x005224F8
    private void FinishChatAnswerIfDone(RealStack real)
    {
      this.EndChatSessionIfLeftBehind();
      if (real.Companion.State == VoiceState.Idle)
      {
        NotchPresenter corePresenter = this.CorePresenter;
        if (corePresenter != null && corePresenter.RendersTextResponsesInNotchChat)
        {
          NotchPresentation.TextResponse currentTextResponse = corePresenter.CurrentTextResponse;
          if (currentTextResponse == null)
          {
            return;
          }
          if (!currentTextResponse.IsThinking && currentTextResponse.Answer.Trim().Length > 0)
          {
            ConversationHistory history = NotchChat.History;
            if (history != null)
            {
              history.NoteAssistantReplyFinished(currentTextResponse.Answer);
            }
            ConversationHistory history2 = NotchChat.History;
            if (history2 != null)
            {
              history2.RefreshSoon();
            }
          }
          else
          {
            ConversationHistory history3 = NotchChat.History;
            if (history3 != null)
            {
              history3.NoteAnswerStopped();
            }
          }
          corePresenter.Present(new NotchPresentation.Idle());
          return;
        }
      }
    }

    // Token: 0x06000391 RID: 913 RVA: 0x00522594 File Offset: 0x00522594
    private void StepNotchChatAsideForDrawing()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null || !corePresenter.IsNotchChatSessionOpen)
      {
        return;
      }
      if (corePresenter.RendersTextResponsesInNotchChat)
      {
        NotchPresentation.TextResponse currentTextResponse = corePresenter.CurrentTextResponse;
        if (currentTextResponse != null && !currentTextResponse.IsThinking && currentTextResponse.Answer.Trim().Length > 0)
        {
          ConversationHistory history = NotchChat.History;
          if (history != null)
          {
            history.NoteAssistantReplyFinished(currentTextResponse.Answer);
          }
          ConversationHistory history2 = NotchChat.History;
          if (history2 != null)
          {
            history2.RefreshSoon();
          }
          corePresenter.Present(new NotchPresentation.Idle());
        }
      }
      Log.Info("notch", "the answer draws on the screen: the chat steps aside");
      corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.DrawingCaption);
    }

    // Token: 0x06000392 RID: 914 RVA: 0x00522628 File Offset: 0x00522628
    internal bool AllowsChatSubmission()
    {
      if (!this.TourHoldsHome)
      {
        return true;
      }
      Log.Info("tour", "chat send refused during the reveal");
      this.TourRefusal("sorry, let me finish the tour first. you can send this right after!");
      return false;
    }

    // Token: 0x06000393 RID: 915 RVA: 0x00522650 File Offset: 0x00522650
    public void SubmitChatText(string text, IReadOnlyList<string> files)
    {
      ProactiveCoordinator proactive = this.Proactive;
      if (proactive != null && proactive.SubmitRemoteTaskModification(text))
      {
        return;
      }
      this.Services.Companion.SubmitTextAsync(text, files);
    }

    // Token: 0x06000394 RID: 916 RVA: 0x0052267C File Offset: 0x0052267C
    [NullableContext(2)]
    private string NotchFullReplyFocusNote()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.IsShowingNotchAgentPeek)
      {
        string expandedReplyEntryId = corePresenter.Tabs.ExpandedReplyEntryId;
        if (expandedReplyEntryId != null)
        {
          ChatHandOffs handOffs = NotchChat.HandOffs;
          ChatHandOff chatHandOff = ((handOffs != null) ? handOffs.Entry(expandedReplyEntryId) : null);
          if (chatHandOff == null)
          {
            return null;
          }
          string clickySlug = chatHandOff.ClickySlug;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
          if (clickySlug != null)
          {
            RealStack real = this.Real;
            ClickyInfo clickyInfo = ((real != null) ? real.Agents.FindClicky(clickySlug) : null);
            if (clickyInfo != null)
            {
              string text = clickyInfo.Name.Replace("\"", "'");
              defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(319, 3);
              defaultInterpolatedStringHandler.AppendLiteral("[Notch] The user held the voice keys while a reply from \"");
              defaultInterpolatedStringHandler.AppendFormatted(text);
              defaultInterpolatedStringHandler.AppendLiteral("\" (clicky_slug: ");
              defaultInterpolatedStringHandler.AppendFormatted(clickyInfo.Slug);
              defaultInterpolatedStringHandler.AppendLiteral(") was open in full in the notch. ");
              defaultInterpolatedStringHandler.AppendLiteral("Their words are most likely for or about ");
              defaultInterpolatedStringHandler.AppendFormatted(text);
              defaultInterpolatedStringHandler.AppendLiteral(". Use judgment: an instruction, a change, or an answer to what it asked goes to it with sessions_send (that clicky_slug), and you say in a few words that you passed it on; ");
              return defaultInterpolatedStringHandler.ToStringAndClear() + "a question about what it did or found you answer yourself (clicky_memory); if they clearly mean another Clicky or something else, do that instead.";
            }
          }
          if (chatHandOff.JobId == null)
          {
            return null;
          }
          string text2 = (chatHandOff.TaskTitle ?? chatHandOff.Title).Replace("\"", "'");
          if (text2.Length > 80)
          {
            text2 = text2.Substring(0, 80);
          }
          if (chatHandOff.ThreadId == null)
          {
            return "[Notch] The user held the voice keys while the job \"" + text2 + "\"'s reply was open in full in the notch. Their words are most likely about it.";
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(104, 2);
          defaultInterpolatedStringHandler.AppendLiteral("[Notch] The user held the voice keys while the job \"");
          defaultInterpolatedStringHandler.AppendFormatted(text2);
          defaultInterpolatedStringHandler.AppendLiteral("\" (job_id: ");
          defaultInterpolatedStringHandler.AppendFormatted(chatHandOff.JobId);
          defaultInterpolatedStringHandler.AppendLiteral(")'s reply was open in full in the notch. ");
          return defaultInterpolatedStringHandler.ToStringAndClear() + "Their words are most likely for or about this job. Use judgment: an instruction, a change, or an answer to what it asked goes to it with sessions_send (that job_id), and you say in a few words that you passed it on; a question about its progress or result you answer yourself (clicky_memory); if they clearly mean something else, do that instead.";
        }
      }
      return null;
    }

    // Token: 0x06000395 RID: 917 RVA: 0x0052283C File Offset: 0x0052283C
    [NullableContext(2)]
    public void OpenChatArtifact([Nullable(1)] string entry, string threadId, string clickySlug)
    {
      string text;
      if (clickySlug == null)
      {
        if (threadId == null)
        {
          text = null;
        }
        else
        {
          RealStack real = this.Real;
          if (real == null)
          {
            text = null;
          }
          else
          {
            ClickyJob clickyJob = real.Agents.Jobs.JobForThread(threadId);
            text = ((clickyJob != null) ? clickyJob.FolderPath : null);
          }
        }
      }
      else
      {
        RealStack real2 = this.Real;
        text = ((real2 != null) ? real2.Agents.WorkspacePath(clickySlug) : null);
      }
      string text2 = text;
      string path = ArtifactPaths.Resolve(entry, text2);
      if (!ArtifactPaths.IsWebLink(path) && this.OpenAgentDocumentInViewer(path, threadId, clickySlug))
      {
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.DismissNotchAgentPeek();
      }
      this.DispatcherQueue.TryEnqueue(delegate
      {
        if (ArtifactPaths.IsWebLink(path) || File.Exists(path) || Directory.Exists(path))
        {
          this.OpenUrl(path);
          return;
        }
        ArtifactPaths.Reveal(path);
      });
    }

    // Token: 0x06000396 RID: 918 RVA: 0x005228F8 File Offset: 0x005228F8
    internal bool SendAgentMessageThroughBrain(string text, IReadOnlyList<string> files, string agentKey, AgentFocusPlace place)
    {
      string text2 = text.Trim();
      if (text2.Length != 0)
      {
        ArcDock dock = this.Dock;
        if (dock != null && !this.TourHoldsHome)
        {
          if (place == AgentFocusPlace.Window)
          {
            dock.NoteTypedAgentMessageInWindow(agentKey);
          }
          else
          {
            dock.NoteTypedAgentMessage(agentKey, place == AgentFocusPlace.NotchFullReply);
          }
          if (place != AgentFocusPlace.Window)
          {
            NotchPresenter corePresenter = this.CorePresenter;
            if (corePresenter != null && !corePresenter.IsNotchChatSessionOpen && corePresenter.IsShowingNotchAgentPeek)
            {
              corePresenter.OpenNotchChatSession(NotchChatSessionOpenVia.Send);
            }
          }
          ConversationHistory history = NotchChat.History;
          if (history != null)
          {
            history.NoteUserSentText(text2, files);
          }
          NotchPresenter corePresenter2 = this.CorePresenter;
          if (corePresenter2 != null)
          {
            corePresenter2.NoteTypedChatMessageSent(files.Count);
          }
          this.Services.Companion.SubmitTextAsync(text2, files);
          return true;
        }
      }
      return false;
    }

    // Token: 0x06000397 RID: 919 RVA: 0x005229A8 File Offset: 0x005229A8
    public bool SendWindowAgentMessageDirectly(string text, IReadOnlyList<string> files, string agentKey, bool spoken)
    {
      RealStack real = this.Real;
      if (real == null || !this.AllowsChatSubmission())
      {
        return false;
      }
      string text2 = text.Trim();
      if (text2.Length == 0 && files.Count == 0)
      {
        return false;
      }
      string text3 = (spoken ? "said" : "typed");
      string text4 = "The user " + text3 + " this to you in the HeyClicky window, in your conversation.";
      string text5 = ((text2.Length > 0) ? text2 : Path.GetFileName(files[0]));
      IReadOnlyList<string> readOnlyList = ((files.Count > 0) ? files : null);
      string text7;
      string text8;
      string text9;
      try
      {
        string text6 = ClickyJob.JobIdFromRailKey(agentKey);
        if (text6 != null)
        {
          ClickyJob clickyJob = real.Agents.Jobs.Job(text6);
          if (clickyJob != null)
          {
            string threadId = clickyJob.ThreadId;
            if (threadId == null)
            {
              return false;
            }
            AppHost.ObserveLaunch(real.Agents.LaunchAsync(new AgentLaunchRequest
            {
              Prompt = text2,
              JobId = clickyJob.Id,
              TargetThreadId = threadId,
              LaunchContext = text4,
              LaunchSource = (spoken ? "voice" : "text"),
              AttachedFilePaths = readOnlyList,
              IsHudFollowUp = true
            }, default(CancellationToken)), agentKey);
            ChatHandOffs handOffs = NotchChat.HandOffs;
            if (handOffs != null)
            {
              handOffs.NoteStarted(null, threadId, true, text5, true, text2, readOnlyList);
            }
            real.Agents.Jobs.MarkSeen(clickyJob.Id);
            text7 = clickyJob.DisplayName;
            text8 = "the job \"" + clickyJob.DisplayName.Replace("\"", "'") + "\"";
            text9 = "job_id: " + clickyJob.Id;
            goto IL_02AB;
          }
        }
        ClickyInfo clickyInfo = real.Agents.FindClicky(agentKey);
        if (clickyInfo == null)
        {
          return false;
        }
        AppHost.ObserveLaunch(real.Agents.LaunchAsync(new AgentLaunchRequest
        {
          Prompt = text2,
          ClickySlug = clickyInfo.Slug,
          LaunchContext = text4,
          LaunchSource = (spoken ? "voice" : "text"),
          AttachedFilePaths = readOnlyList,
          IsHudFollowUp = true
        }, default(CancellationToken)), agentKey);
        ChatHandOffs handOffs2 = NotchChat.HandOffs;
        if (handOffs2 != null)
        {
          handOffs2.NoteStarted(clickyInfo.Slug, null, true, text5, true, text2, readOnlyList);
        }
        ClickyReadMarks.MarkRead(clickyInfo.Slug);
        text7 = clickyInfo.Name;
        text8 = "\"" + clickyInfo.Name.Replace("\"", "'") + "\"";
        string threadId2 = clickyInfo.ThreadId;
        text9 = ((threadId2 != null) ? ("job_id: " + real.Agents.OneBrainJobId(threadId2)) : ("clicky_slug: " + clickyInfo.Slug));
        IL_02AB:;
      }
      catch (Exception ex)
      {
        Log.Warn("window", "a message straight to " + agentKey + " didn't go: " + ex.Message);
        return false;
      }
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.NoteUserSentStraightToAgent(text2, files, text7, spoken);
      }
      string text10 = ((text2.Length > 1500) ? text2.Substring(0, 1500) : text2).Replace("\"", "'");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      string text11;
      if (files.Count != 0)
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 2);
        defaultInterpolatedStringHandler.AppendLiteral(" (with ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(files.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" file");
        defaultInterpolatedStringHandler.AppendFormatted((files.Count == 1) ? "" : "s");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        text11 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text11 = "";
      }
      string text12 = text11;
      BrainSession brainSession = real.Core.BrainSession;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(156, 5);
      defaultInterpolatedStringHandler.AppendLiteral("[App] In the HeyClicky window the user ");
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral(" a message straight to ");
      defaultInterpolatedStringHandler.AppendFormatted(text8);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(text9);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      defaultInterpolatedStringHandler.AppendFormatted(text12);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendLiteral("and HeyClicky passed it on as is: \"");
      defaultInterpolatedStringHandler.AppendFormatted(text10);
      defaultInterpolatedStringHandler.AppendLiteral("\". You didn't answer it; it is that agent's to handle.");
      brainSession.NoteForNextTurn(defaultInterpolatedStringHandler.ToStringAndClear(), null);
      if (!spoken)
      {
        Sounds.Play("clicky-text-send", 0.2);
      }
      string text13 = "notch_peek_action";
      string text14 = null;
      string text15 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = "window_message_sent";
      jsonObject["has_files"] = files.Count > 0;
      jsonObject["via"] = (spoken ? "voice_direct" : "typed_direct");
      ClickyAnalytics.TrackProductEvent(text13, text14, text15, jsonObject);
      return true;
    }

    // Token: 0x06000398 RID: 920 RVA: 0x00522E84 File Offset: 0x00522E84
    private static void ObserveLaunch(Task<string> launch, string agentKey)
    {
      launch.ContinueWith(delegate(Task<string> t)
      {
        string text = "window";
        string text2 = "a message straight to ";
        string agentKey2 = agentKey;
        string text3 = " failed: ";
        AggregateException exception = t.Exception;
        Log.Warn(text, text2 + agentKey2 + text3 + ((exception != null) ? exception.GetBaseException().Message : null));
      }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
    }

    // Token: 0x06000399 RID: 921 RVA: 0x00522EC0 File Offset: 0x00522EC0
    public void SendChatFollowUp(ChatHandOff entry, string followUp)
    {
      this.SendFollowUpToAgent(entry, followUp, Array.Empty<string>(), true, false);
    }

    // Token: 0x0600039A RID: 922 RVA: 0x00522ED4 File Offset: 0x00522ED4
    public void SendTypedChatFollowUp(ChatHandOff entry, string text, IReadOnlyList<string> files, bool fromWindow = false)
    {
      this.SendFollowUpToAgent(entry, text, files, false, fromWindow);
    }

    // Token: 0x0600039B RID: 923 RVA: 0x00522EE4 File Offset: 0x00522EE4
    private void SendFollowUpToAgent(ChatHandOff entry, string text, IReadOnlyList<string> files, bool tapped, bool fromWindow)
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      text = text.Trim();
      if (text.Length == 0 && files.Count == 0)
      {
        return;
      }
      string text2 = (tapped ? "The user tapped one of the follow-ups you suggested under your last reply." : "The user typed this follow-up under your last reply.");
      string text3 = ((text.Length > 0) ? text : Path.GetFileName(files[0]));
      bool flag = false;
      string jobId = entry.JobId;
      string text4;
      if (jobId == null)
      {
        text4 = null;
      }
      else
      {
        ClickyJob clickyJob = real.Agents.Jobs.Job(jobId);
        text4 = ((clickyJob != null) ? clickyJob.RailKey : null);
      }
      string text5;
      if ((text5 = text4) == null)
      {
        string clickySlug = entry.ClickySlug;
        text5 = ((clickySlug != null && real.Agents.FindClicky(clickySlug) != null) ? clickySlug : null);
      }
      string text6 = text5;
      if (fromWindow && text6 != null && (this.SendWindowAgentMessageDirectly(text, files, text6, false) || this.TourHoldsHome))
      {
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      bool flag2 = ((corePresenter != null) ? corePresenter.Tabs.ExpandedReplyEntryId : null) == entry.Id;
      AgentFocusPlace agentFocusPlace = (fromWindow ? AgentFocusPlace.Window : (flag2 ? AgentFocusPlace.NotchFullReply : AgentFocusPlace.Notch));
      if (text6 != null && this.SendAgentMessageThroughBrain(text, files, text6, agentFocusPlace))
      {
        flag = true;
      }
      else
      {
        string clickySlug2 = entry.ClickySlug;
        if (clickySlug2 != null && real.Agents.FindClicky(clickySlug2) != null)
        {
          real.Agents.LaunchAsync(new AgentLaunchRequest
          {
            Prompt = text,
            ClickySlug = clickySlug2,
            LaunchContext = text2,
            LaunchSource = "text",
            AttachedFilePaths = ((files.Count > 0) ? files : null),
            IsHudFollowUp = true
          }, default(CancellationToken));
          ChatHandOffs handOffs = NotchChat.HandOffs;
          if (handOffs != null)
          {
            handOffs.NoteStarted(clickySlug2, null, true, text3, false, null, null);
          }
          flag = true;
        }
        else
        {
          string text7;
          if ((text7 = entry.JobId) == null)
          {
            string threadId = entry.ThreadId;
            if (threadId == null)
            {
              text7 = null;
            }
            else
            {
              ClickyJob clickyJob2 = real.Agents.Jobs.JobForThread(threadId);
              text7 = ((clickyJob2 != null) ? clickyJob2.Id : null);
            }
          }
          string text8 = text7;
          if (text8 != null)
          {
            ClickyJob clickyJob3 = real.Agents.Jobs.Job(text8);
            if (clickyJob3 != null)
            {
              real.Agents.LaunchAsync(new AgentLaunchRequest
              {
                Prompt = text,
                JobId = clickyJob3.Id,
                TargetThreadId = clickyJob3.ThreadId,
                LaunchContext = text2,
                LaunchSource = "text",
                AttachedFilePaths = ((files.Count > 0) ? files : null),
                IsHudFollowUp = true
              }, default(CancellationToken));
              ChatHandOffs handOffs2 = NotchChat.HandOffs;
              if (handOffs2 != null)
              {
                handOffs2.NoteStarted(null, clickyJob3.ThreadId, true, text3, false, null, null);
              }
              flag = true;
            }
          }
        }
      }
      if (!flag)
      {
        return;
      }
      Sounds.Play("clicky-text-send", 0.2);
      if (tapped)
      {
        ClickyAnalytics.TrackProductEvent("notch_peek_action", entry.ClickySlug, entry.ThreadId, NotchChatAnalytics.ChatFollowUpSuggestion(entry, text, flag2));
      }
      else
      {
        string text9 = "chat_follow_up_typed";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["from_full_reply"] = flag2;
        string text10 = "from_document";
        NotchPresenter corePresenter2 = this.CorePresenter;
        jsonObject[text10] = corePresenter2 != null && corePresenter2.Tabs.Selected == NotchTab.Document;
        jsonObject["from_window"] = fromWindow;
        jsonObject["is_job"] = entry.JobId != null;
        jsonObject["file_count"] = files.Count;
        jsonObject["has_text"] = text.Length > 0;
        NotchPeekSurface.PeekAction(text9, jsonObject);
      }
      if (fromWindow)
      {
        return;
      }
      if (flag2)
      {
        NotchPresenter corePresenter3 = this.CorePresenter;
        if (corePresenter3 != null)
        {
          corePresenter3.Tabs.CloseFullReply(new NotchChatFullReplyCloseVia?(NotchChatFullReplyCloseVia.FollowUpSent));
        }
      }
      NotchPresenter corePresenter4 = this.CorePresenter;
      if (corePresenter4 != null && corePresenter4.Tabs.Selected == NotchTab.Document)
      {
        this.CorePresenter.Tabs.CloseDocument();
      }
    }

    // Token: 0x0600039C RID: 924 RVA: 0x005232B8 File Offset: 0x005232B8
    public void ExpandNotchIntoWindow(string from)
    {
      this.ExpandNotchIntoWindow(from, null);
    }

    // Token: 0x0600039D RID: 925 RVA: 0x005232C4 File Offset: 0x005232C4
    public void ExpandNotchIntoWindow(string from, [Nullable(2)] string agentKeyToOpen)
    {
      AppHost.<>c__DisplayClass303_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass303_0();
      CS$<>8__locals1.from = from;
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      NotchTabs tabs = corePresenter.Tabs;
      CS$<>8__locals1.window = HeyClickyWindow.Shared;
      CS$<>8__locals1.agentKey = agentKeyToOpen;
      CS$<>8__locals1.document = null;
      CS$<>8__locals1.documentThread = null;
      CS$<>8__locals1.settings = false;
      if (agentKeyToOpen == null && tabs.Selected == NotchTab.Chat)
      {
        string expandedReplyEntryId = tabs.ExpandedReplyEntryId;
        if (expandedReplyEntryId != null)
        {
          ChatHandOffs handOffs = NotchChat.HandOffs;
          ChatHandOff chatHandOff = ((handOffs != null) ? handOffs.Entry(expandedReplyEntryId) : null);
          if (chatHandOff != null)
          {
            CS$<>8__locals1.agentKey = HeyClickyWindow.AgentKeyForEntry(chatHandOff);
            goto IL_00E0;
          }
        }
      }
      if (agentKeyToOpen == null && tabs.Selected == NotchTab.Settings)
      {
        CS$<>8__locals1.settings = true;
      }
      else if (agentKeyToOpen == null && tabs.Selected == NotchTab.Document)
      {
        string documentPath = tabs.DocumentPath;
        if (documentPath != null)
        {
          CS$<>8__locals1.document = documentPath;
          CS$<>8__locals1.documentThread = tabs.DocumentThreadId;
          if (CS$<>8__locals1.documentThread != null)
          {
            CS$<>8__locals1.agentKey = this.WindowAgentKey(null, null, CS$<>8__locals1.documentThread);
          }
        }
      }
      IL_00E0:
      CS$<>8__locals1.flight = null;
      if (!CS$<>8__locals1.window.IsOpen && !HeyClickyWindowOpeningFlight.IsFlying)
      {
        RectInt32? openFrame = NotchVisibleSurfaces.OpenFrame;
        if (openFrame != null)
        {
          RectInt32 valueOrDefault = openFrame.GetValueOrDefault();
          CS$<>8__locals1.flight = HeyClickyWindowOpeningFlight.Begin(new ValueTuple<int, int, int, int>(valueOrDefault.X, valueOrDefault.Y, valueOrDefault.Width, valueOrDefault.Height), this.NotchUi.Monitor, this.NotchUi.Window.Hwnd, HeyClickyWindowPalette.Current().Ground, this.ExcludeFromCapture);
        }
      }
      CS$<>8__locals1.settingsPage = NotchSettingsTab.ToPage(tabs.SettingsSection);
      corePresenter.FoldIntoWindow();
      if (CS$<>8__locals1.flight != null)
      {
        CS$<>8__locals1.flight.AfterCover(new Action(CS$<>8__locals1.<ExpandNotchIntoWindow>g__OpenWindow|0));
        return;
      }
      this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(CS$<>8__locals1.<ExpandNotchIntoWindow>g__OpenWindow|0));
    }

    // Token: 0x0600039E RID: 926 RVA: 0x0052348C File Offset: 0x0052348C
    public void OpenInItsApp(string path, [Nullable(2)] string threadId)
    {
      string text = "open_in_app";
      string text2 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["success"] = true;
      ClickyAnalytics.TrackArtifactAction(text, path, text2, jsonObject);
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.DismissNotchAgentPeek();
      }
      ChatBlockViews.OpenPath(path);
    }

    // Token: 0x0600039F RID: 927 RVA: 0x005234DC File Offset: 0x005234DC
    public void RevealInExplorer(string path)
    {
      ClickyAnalytics.TrackArtifactAction("reveal_in_finder", path, null, null);
      ArtifactPaths.Reveal(path);
    }

    // Token: 0x060003A0 RID: 928 RVA: 0x005234F4 File Offset: 0x005234F4
    public void PlayDrawing(string lessonId, bool onWhiteBoard)
    {
      LessonService service = this.Lessons;
      if (service != null)
      {
        StoredLesson lesson = service.Store.List().FirstOrDefault<StoredLesson>((StoredLesson l) => l.Data.Id == lessonId);
        if (lesson != null)
        {
          NotchPresenter corePresenter = this.CorePresenter;
          if (corePresenter != null)
          {
            corePresenter.DismissNotchAgentPeek();
          }
          this.DispatcherQueue.TryEnqueue(delegate
          {
            if (onWhiteBoard)
            {
              service.PlayAsync(lesson, true, true);
              return;
            }
            service.PlayInWindow(lesson);
          });
          return;
        }
      }
    }

    // Token: 0x060003A1 RID: 929 RVA: 0x00523584 File Offset: 0x00523584
    private void PlayAgentEarcon(string name, float volume)
    {
      if (!AppHost.EarconsMuted)
      {
        RealStack real = this.Real;
        if (real == null)
        {
          return;
        }
        real.Audio.PlayEarcon(name, volume);
      }
    }

    // Token: 0x1700007F RID: 127
    // (get) Token: 0x060003A2 RID: 930 RVA: 0x005235A4 File Offset: 0x005235A4
    private bool EdgeDockIsRunning
    {
      get
      {
        if (this.HasCompletedOnboardingNow && !this.introSequenceActive)
        {
          RealStack real = this.Real;
          return real == null || !real.Tutorial.IsEngaged;
        }
        return false;
      }
    }

    // Token: 0x060003A3 RID: 931 RVA: 0x005235D4 File Offset: 0x005235D4
    private void WireFinishedWork(RealStack real)
    {
      AgentService agents = real.Agents;
      agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        AgentTaskEventKind kind = e.Kind;
        bool flag = kind == AgentTaskEventKind.Failed || kind == AgentTaskEventKind.NeedsExtraUsageApproval;
        if (flag)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.PlayAttentionCue(agents, e.TaskId, e.Kind);
          });
        }
      };
      OneBrainJobReports jobReports = real.JobReports;
      if (jobReports != null)
      {
        jobReports.ReportPresented += delegate(IReadOnlyList<string> _, IReadOnlyList<string> lines)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            ConversationHistory history = NotchChat.History;
            if (history != null)
            {
              history.NoteAssistantReport(string.Join("\n\n", lines));
            }
            ConversationHistory history2 = NotchChat.History;
            if (history2 == null)
            {
              return;
            }
            history2.RefreshSoon();
          });
        };
        jobReports.HeadsUpReady += delegate(string taskId)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.PresentEdgeDockWindow(agents, taskId, "finished");
          });
        };
      }
      agents.ResultCardShown += delegate(string threadId)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          ClickyAnswer clickyAnswer = agents.ResultCard(threadId);
          if (clickyAnswer != null)
          {
            JsonObject jsonObject = AppHost.AnswerJson(clickyAnswer);
            if (jsonObject != null)
            {
              ChatHandOffs handOffs = NotchChat.HandOffs;
              if (handOffs == null)
              {
                return;
              }
              handOffs.NoteCard(jsonObject, threadId);
            }
          }
        });
      };
      if (this.PresentTaskNeedsYouHook == null)
      {
        this.PresentTaskNeedsYouHook = delegate(string threadId)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            string text = agents.TaskIdForThread(threadId);
            if (text != null)
            {
              this.PresentEdgeDockWindow(agents, text, "needs_you");
              return;
            }
            ClickyJob clickyJob = agents.Jobs.JobForThread(threadId);
            if (clickyJob != null)
            {
              this.PresentJobUpdate(agents, clickyJob, NotchUpdateKind.NeedsYou, "needs_you");
            }
          });
        };
      }
      if (this.DismissNotchUpdateHook == null)
      {
        DispatcherQueueHandler <>9__12;
        this.DismissNotchUpdateHook = delegate
        {
          DispatcherQueue dispatcherQueue = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__12) == null)
          {
            dispatcherQueueHandler = (<>9__12 = delegate
            {
              NotchPresenter corePresenter = this.CorePresenter;
              if (corePresenter == null)
              {
                return;
              }
              corePresenter.Updates.DismissCurrent();
            });
          }
          dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        };
      }
      real.AgentVoice.FinishShowsOnNotchCard = new Func<AgentTaskDetail, bool>(this.FinishShowsOnNotchCard);
      NotchExtensions.AnnounceFinishedWorkOnRequest = new Action<NotchUpdateContent>(this.AnnounceFinishedWorkOnRequest);
      NotchExtensions.MarkFinishedWorkSeen = delegate(NotchUpdateContent content)
      {
        string jobId = content.JobId;
        if (jobId != null)
        {
          agents.Jobs.MarkSeen(jobId);
          return;
        }
        string clickySlug = content.ClickySlug;
        if (clickySlug != null)
        {
          ClickyReadMarks.MarkRead(clickySlug);
        }
      };
    }

    // Token: 0x060003A4 RID: 932 RVA: 0x005236C8 File Offset: 0x005236C8
    [NullableContext(2)]
    private static JsonObject AnswerJson(ClickyAnswer answer)
    {
      if (answer != null)
      {
        return JsonSerializer.SerializeToNode<ClickyAnswer>(answer, null) as JsonObject;
      }
      return null;
    }

    // Token: 0x060003A5 RID: 933 RVA: 0x005236DC File Offset: 0x005236DC
    private void PlayAttentionCue(AgentService agents, string taskId, AgentTaskEventKind kind)
    {
      AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
      if (taskDetail == null)
      {
        return;
      }
      if (taskDetail.IsDemo)
      {
        RealStack real = this.Real;
        if (real != null && real.Tutorial.IsEngaged)
        {
          return;
        }
      }
      if (kind == AgentTaskEventKind.NeedsExtraUsageApproval)
      {
        if (!this.announcedExtraUsage.Add(taskId))
        {
          return;
        }
        this.PlayAgentEarcon("agentNeedsYou", 0.8f);
        return;
      }
      else
      {
        if (taskDetail.RequiresExtraUsageApproval)
        {
          return;
        }
        if (taskDetail != null && taskDetail.IsRoutineRun)
        {
          CompanionError error = taskDetail.Error;
          if (error != null && error.Code == "agent_offline")
          {
            return;
          }
        }
        this.PlayAgentEarcon("clickySurprised", 0.7f);
        return;
      }
    }

    // Token: 0x060003A6 RID: 934 RVA: 0x00523780 File Offset: 0x00523780
    private static bool QuietSilencesAgentFinish(bool isScheduledRoutineRun)
    {
      return !QuietContext.IgnoredForTests && (QuietContext.IsAnotherAppUsingMicrophone() || QuietContext.QueryNotificationState() == QuietContext.NotificationState.PresentationMode || (isScheduledRoutineRun && QuietContext.IsDoNotDisturbOn()));
    }

    // Token: 0x060003A7 RID: 935 RVA: 0x005237A8 File Offset: 0x005237A8
    private void NoteHandOffStarted(AgentService agents, string taskId, string words, bool isFollowUp, int attempt = 0)
    {
      AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
      if (!isFollowUp)
      {
        string text = ((taskDetail != null) ? taskDetail.JobId : null);
        if (text != null)
        {
          ClickyJob clickyJob = agents.Jobs.Job(text);
          if (clickyJob != null)
          {
            ChatHandOffs handOffs = NotchChat.HandOffs;
            if (handOffs == null)
            {
              return;
            }
            handOffs.NoteStartedJob(clickyJob.Id, clickyJob.Title);
            return;
          }
        }
      }
      if (((taskDetail != null) ? taskDetail.ClickySlug : null) == null && ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null) == null && attempt < 15)
      {
        DispatcherQueueTimer dispatcherQueueTimer = this.DispatcherQueue.CreateTimer().Rooted();
        dispatcherQueueTimer.Interval = TimeSpan.FromMilliseconds(200L, 0L);
        dispatcherQueueTimer.IsRepeating = false;
        dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          this.NoteHandOffStarted(agents, taskId, words, isFollowUp, attempt + 1);
        };
        dispatcherQueueTimer.Start();
        return;
      }
      ChatHandOffs handOffs2 = NotchChat.HandOffs;
      if (handOffs2 == null)
      {
        return;
      }
      handOffs2.NoteStarted((taskDetail != null) ? taskDetail.ClickySlug : null, (taskDetail != null) ? taskDetail.Snapshot.ThreadId : null, isFollowUp, words, false, null, null);
    }

    // Token: 0x060003A8 RID: 936 RVA: 0x005238EC File Offset: 0x005238EC
    private void OnAgentTaskStatus(AgentService agents, string taskId)
    {
      AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
      if (taskDetail == null)
      {
        return;
      }
      AgentTaskStatus status = taskDetail.Snapshot.Status;
      AgentTaskStatus agentTaskStatus;
      if (this.lastAgentStatus.TryGetValue(taskId, out agentTaskStatus) && agentTaskStatus == status)
      {
        return;
      }
      this.lastAgentStatus[taskId] = status;
      ChatHandOffOutcome? chatHandOffOutcome;
      switch (status)
      {
      case AgentTaskStatus.WaitingForUser:
        chatHandOffOutcome = new ChatHandOffOutcome?(ChatHandOffOutcome.NeedsYou);
        break;
      case AgentTaskStatus.Finished:
        chatHandOffOutcome = new ChatHandOffOutcome?(ChatHandOffOutcome.Finished);
        break;
      case AgentTaskStatus.Failed:
        chatHandOffOutcome = new ChatHandOffOutcome?(ChatHandOffOutcome.Failed);
        break;
      case AgentTaskStatus.Stopped:
        chatHandOffOutcome = new ChatHandOffOutcome?(ChatHandOffOutcome.Stopped);
        break;
      default:
        chatHandOffOutcome = null;
        break;
      }
      ChatHandOffOutcome? chatHandOffOutcome2 = chatHandOffOutcome;
      this.SyncRestingMark();
      if (chatHandOffOutcome2 == null)
      {
        return;
      }
      ChatHandOffOutcome valueOrDefault = chatHandOffOutcome2.GetValueOrDefault();
      string threadId = taskDetail.Snapshot.ThreadId;
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        ChatTaskEnd chatTaskEnd = new ChatTaskEnd();
        chatTaskEnd.JobId = taskDetail.JobId;
        chatTaskEnd.ClickySlug = taskDetail.ClickySlug;
        chatTaskEnd.ThreadId = threadId;
        chatTaskEnd.IsDemo = taskDetail.IsDemo;
        ChatTaskEnd chatTaskEnd2 = chatTaskEnd;
        ClickyJobUserStep pendingUserStep = taskDetail.PendingUserStep;
        string text;
        if ((text = ((pendingUserStep != null) ? pendingUserStep.Step : null)) == null)
        {
          text = ((status == AgentTaskStatus.WaitingForUser && !taskDetail.RequiresExtraUsageApproval) ? (taskDetail.Summary ?? taskDetail.ProgressSummary) : null);
        }
        chatTaskEnd2.PendingUserStep = text;
        chatTaskEnd.NeedsChromeConnection = threadId != null && agents.NeedsChromeConnection(threadId);
        ChatTaskEnd chatTaskEnd3 = chatTaskEnd;
        ChatHandOffOutcome chatHandOffOutcome3 = valueOrDefault;
        string text3;
        if (chatHandOffOutcome3 != ChatHandOffOutcome.Failed)
        {
          if (chatHandOffOutcome3 == ChatHandOffOutcome.Stopped)
          {
            string text2 = taskDetail.ProgressSummary.Trim();
            if (text2 != null && text2.Length > 0 && text2 != "Stopped.")
            {
              text3 = text2;
              goto IL_01A9;
            }
          }
          text3 = taskDetail.Summary;
        }
        else
        {
          text3 = taskDetail.FailureMessage ?? taskDetail.Summary;
        }
        IL_01A9:
        chatTaskEnd3.FinalSummary = text3;
        chatTaskEnd.FinalResponseMarkdown = taskDetail.Snapshot.FinalResponseMarkdown;
        ChatTaskEnd chatTaskEnd4 = chatTaskEnd;
        string text4 = agents.FullReplyText(taskId);
        IReadOnlyList<string> readOnlyList2;
        if (text4 == null || text4.Length <= 0)
        {
          string finalResponseMarkdown = taskDetail.Snapshot.FinalResponseMarkdown;
          if (finalResponseMarkdown == null || finalResponseMarkdown.Length <= 0)
          {
            IReadOnlyList<string> readOnlyList = Array.Empty<string>();
            readOnlyList2 = readOnlyList;
          }
          else
          {
            IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>(finalResponseMarkdown);
            readOnlyList2 = readOnlyList;
          }
        }
        else
        {
          IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>(text4);
          readOnlyList2 = readOnlyList;
        }
        chatTaskEnd4.CompletedAssistantMessages = readOnlyList2;
        chatTaskEnd.UserFacingArtifactPaths = taskDetail.Artifacts;
        chatTaskEnd.SuggestedNextActions = taskDetail.NextActions;
        chatTaskEnd.FinalAnswer = AppHost.AnswerJson(taskDetail.Answer);
        chatTaskEnd.IsScheduledRoutineRun = taskDetail.IsRoutineRun;
        chatTaskEnd.CompletedThreadName = taskDetail.DoneTitle ?? taskDetail.Snapshot.Title;
        chatTaskEnd.SitesRead = taskDetail.SitesRead ?? Array.Empty<string>();
        handOffs.NoteTaskEnded(chatTaskEnd, (valueOrDefault == ChatHandOffOutcome.NeedsYou) ? ChatHandOffOutcome.Finished : valueOrDefault);
      }
      if (taskDetail.IsDemo)
      {
        return;
      }
      switch (status)
      {
      case AgentTaskStatus.WaitingForUser:
        this.PresentEdgeDockWindow(agents, taskId, "needs_you");
        break;
      case AgentTaskStatus.Finished:
      {
        if (agents.PendingComputerUseRequest(taskId) == null)
        {
          this.AnnounceFinishOnCard(agents, taskDetail);
          return;
        }
        this.PresentEdgeDockWindow(agents, taskId, "needs_you");
        Action<string> announceComputerUseAsk = this.AnnounceComputerUseAsk;
        if (announceComputerUseAsk == null)
        {
          return;
        }
        announceComputerUseAsk(taskId);
        return;
      }
      case AgentTaskStatus.Failed:
        this.PresentEdgeDockWindow(agents, taskId, taskDetail.RequiresExtraUsageApproval ? "needs_you" : "failed");
        return;
      case AgentTaskStatus.Stopped:
        if (taskDetail.RequiresExtraUsageApproval)
        {
          this.PresentEdgeDockWindow(agents, taskId, "needs_you");
          return;
        }
        break;
      default:
        return;
      }
    }

    // Token: 0x060003A9 RID: 937 RVA: 0x00523C30 File Offset: 0x00523C30
    private void AnnounceFinishOnCard(AgentService agents, AgentTaskDetail detail)
    {
      string taskId = detail.Snapshot.TaskId;
      if (agents.IsTaskWaitingOnUserAnswer(taskId))
      {
        return;
      }
      if (AppHost.QuietSilencesAgentFinish(detail.IsRoutineRun))
      {
        Log.Info("notch", "finish of " + taskId + " not announced: quiet (a call, a screen share, or Do Not Disturb for a routine run)");
        if (detail.PendingUserStep != null)
        {
          this.PresentEdgeDockWindow(agents, taskId, "needs_you");
        }
        return;
      }
      RealStack real = this.Real;
      bool flag;
      if (real == null)
      {
        flag = true;
      }
      else
      {
        flag = !real.Core.Settings.Read<bool>((AppSettings s) => s.AgentAnnouncementsVoiceEnabled);
      }
      bool flag2;
      if (flag)
      {
        RealStack real2 = this.Real;
        if (real2 == null)
        {
          flag2 = true;
        }
        else
        {
          flag2 = !real2.Core.Settings.Read<bool>((AppSettings s) => s.AgentCursorTextAnnouncementsEnabled);
        }
      }
      else
      {
        flag2 = false;
      }
      bool flag3 = flag2;
      bool flag4 = !detail.IsRoutineRun && !flag3;
      bool flag5 = detail.JobId != null && ClickyJobsPreferences.IsEnabled;
      bool flag6 = !flag5 && !detail.IsDemo && !detail.IsRoutineRun && detail.ClickySlug != null;
      bool flag7 = this.FinishOpensInNotchViewer(detail);
      bool flag8 = flag7 || ((flag5 || flag6) && this.DockShowsFinishes);
      bool flag9;
      if (flag8 && !flag7)
      {
        RealStack real3 = this.Real;
        if (real3 == null)
        {
          flag9 = false;
        }
        else
        {
          OneBrainJobReports jobReports = real3.JobReports;
          flag9 = ((jobReports != null) ? new bool?(jobReports.HoldHeadsUpForCard(OneBrainJobReports.TurnKey(taskId, detail.TurnId), flag4)) : null).GetValueOrDefault();
        }
      }
      else
      {
        flag9 = false;
      }
      bool flag10 = flag9;
      string text = "notch";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(115, 7);
      defaultInterpolatedStringHandler.AppendLiteral("finish of ");
      defaultInterpolatedStringHandler.AppendFormatted(taskId);
      defaultInterpolatedStringHandler.AppendLiteral(": on card ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag8);
      defaultInterpolatedStringHandler.AppendLiteral(" (job ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag5);
      defaultInterpolatedStringHandler.AppendLiteral(", docked Clicky ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag6);
      defaultInterpolatedStringHandler.AppendLiteral(", dock shows finishes ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(this.DockShowsFinishes);
      defaultInterpolatedStringHandler.AppendLiteral("), in viewer ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag7);
      defaultInterpolatedStringHandler.AppendLiteral(", heads-up waits for the brain's card ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(flag10);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      if (flag4 && !flag10)
      {
        this.PlayAgentEarcon("agentDone", 0.7f);
      }
      if (flag8 && !flag7 && !flag10)
      {
        this.PresentEdgeDockWindow(agents, taskId, "finished");
      }
    }

    // Token: 0x17000080 RID: 128
    // (get) Token: 0x060003AA RID: 938 RVA: 0x00523EA8 File Offset: 0x00523EA8
    private bool DockShowsFinishes
    {
      get
      {
        if (this.EdgeDockIsRunning)
        {
          ArcDock dock = this.Dock;
          return dock == null || !dock.IsTabTuckedNow;
        }
        return false;
      }
    }

    // Token: 0x060003AB RID: 939 RVA: 0x00523EC8 File Offset: 0x00523EC8
    internal bool FinishShowsOnNotchCard(AgentTaskDetail detail)
    {
      if (detail.IsDemo || detail.IsRoutineRun)
      {
        return false;
      }
      if (this.FinishOpensInNotchViewer(detail))
      {
        return true;
      }
      bool flag = detail.JobId != null && ClickyJobsPreferences.IsEnabled;
      bool flag2 = !flag && detail.ClickySlug != null;
      return (flag || flag2) && this.DockShowsFinishes;
    }

    // Token: 0x060003AC RID: 940 RVA: 0x00523F24 File Offset: 0x00523F24
    private bool FinishOpensInNotchViewer(AgentTaskDetail detail)
    {
      RealStack real = this.Real;
      if (real == null || this.CorePresenter == null || detail.IsDemo || this.onboardingDemoTaskIds.ContainsKey(detail.Snapshot.TaskId) || real.Tutorial.IsEngaged)
      {
        return false;
      }
      if (detail.JobId != null)
      {
        return false;
      }
      string text = detail.Artifacts.FirstOrDefault<string>();
      string text2 = ((text != null) ? text.Trim() : null);
      if (string.IsNullOrEmpty(text2))
      {
        return false;
      }
      string clickySlug = detail.ClickySlug;
      string text3 = ((clickySlug != null) ? (real.Agents.WorkspacePath(clickySlug) ?? detail.WorkspacePath) : detail.WorkspacePath);
      string text4 = ArtifactPaths.Resolve(text2, text3);
      return !ArtifactPaths.IsWebLink(text4) && File.Exists(text4) && ArtifactPaths.SupportsInline(text4);
    }

    // Token: 0x060003AD RID: 941 RVA: 0x00523FEC File Offset: 0x00523FEC
    private void AnnounceFinishedWorkOnRequest(NotchUpdateContent content)
    {
      RealStack real = this.Real;
      if (real != null)
      {
        if (real.Core.Settings.Read<bool>((AppSettings s) => s.AgentAnnouncementsVoiceEnabled))
        {
          OneBrainJobReports jobReports = real.JobReports;
          if (jobReports != null && jobReports.SpeakOnRequest(content.ThreadId))
          {
            return;
          }
          if (real.Companion.State != VoiceState.Idle)
          {
            return;
          }
          string text = ClickyNotes.FinishedLine(content.Title, content.Summary ?? "", false, false);
          Log.Info("notch", "finished work said on request: " + content.RailKey);
          real.Companion.AnnounceAsync(text, real.Core.Settings.Read<bool>((AppSettings s) => s.AgentCursorTextAnnouncementsEnabled), default(CancellationToken));
          return;
        }
      }
    }

    // Token: 0x060003AE RID: 942 RVA: 0x005240DC File Offset: 0x005240DC
    internal void PresentEdgeDockWindow(AgentService agents, string taskId, string reason)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (this.CorePresenter != null && this.EdgeDockIsRunning)
      {
        AgentTaskDetail taskDetail = agents.GetTaskDetail(taskId);
        if (taskDetail != null && !taskDetail.IsDemo)
        {
          string threadId = taskDetail.Snapshot.ThreadId;
          string jobId = taskDetail.JobId;
          ClickyJob clickyJob = ((jobId != null) ? agents.Jobs.Job(jobId) : ((threadId == null) ? null : agents.Jobs.JobForThread(threadId)));
          string text;
          if ((text = taskDetail.ClickySlug) == null)
          {
            if (threadId != null)
            {
              ClickyInfo clickyInfo = agents.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId);
              text = ((clickyInfo != null) ? clickyInfo.Slug : null);
            }
            else
            {
              text = null;
            }
          }
          string text2 = text;
          bool flag = agents.PendingComputerUseRequest(taskId) != null || taskDetail.RequiresExtraUsageApproval || taskDetail.PendingUserStep != null || (threadId != null && agents.NeedsChromeConnection(threadId)) || ((text2 != null) ? (agents.LiveStatusForClicky(text2).Phase == ClickyAppLiveStatus.Phases.NeedsYou && reason != "failed") : (clickyJob != null && agents.RailPhase(clickyJob) == ClickyJobRailPhase.NeedsYou));
          NotchUpdateKind notchUpdateKind = ((reason == "needs_you" || flag) ? NotchUpdateKind.NeedsYou : ((reason == "failed") ? NotchUpdateKind.Failed : NotchUpdateKind.Done));
          string text3 = "notch";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 5);
          defaultInterpolatedStringHandler.AppendLiteral("presentEdgeDockWindow reason=");
          defaultInterpolatedStringHandler.AppendFormatted(reason);
          defaultInterpolatedStringHandler.AppendLiteral(" task=");
          defaultInterpolatedStringHandler.AppendFormatted(threadId ?? "nil");
          defaultInterpolatedStringHandler.AppendLiteral(" job=");
          defaultInterpolatedStringHandler.AppendFormatted(((clickyJob != null) ? clickyJob.Id : null) ?? "nil");
          defaultInterpolatedStringHandler.AppendLiteral(" slug=");
          defaultInterpolatedStringHandler.AppendFormatted(text2 ?? "nil");
          defaultInterpolatedStringHandler.AppendLiteral(" kind=");
          defaultInterpolatedStringHandler.AppendFormatted(notchUpdateKind.RawValue());
          Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
          if (text2 != null)
          {
            this.CorePresenter.Updates.Present(text2, notchUpdateKind);
            string text4 = "clicky_dashboard_action";
            string text5 = text2;
            string text6 = null;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["action"] = "notch_card_shown";
            jsonObject["reason"] = reason;
            ClickyAnalytics.TrackProductEvent(text4, text5, text6, jsonObject);
            return;
          }
          if (clickyJob == null)
          {
            return;
          }
          this.PresentJobUpdate(agents, clickyJob, notchUpdateKind, reason);
          return;
        }
      }
      string text7 = "notch";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(73, 4);
      defaultInterpolatedStringHandler.AppendLiteral("presentEdgeDockWindow reason=");
      defaultInterpolatedStringHandler.AppendFormatted(reason);
      defaultInterpolatedStringHandler.AppendLiteral(" task=");
      defaultInterpolatedStringHandler.AppendFormatted(taskId);
      defaultInterpolatedStringHandler.AppendLiteral(": skipped (dock running ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(this.EdgeDockIsRunning);
      defaultInterpolatedStringHandler.AppendLiteral(", task known ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(agents.GetTaskDetail(taskId) != null);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060003AF RID: 943 RVA: 0x005243C8 File Offset: 0x005243C8
    private void PresentJobUpdate(AgentService agents, ClickyJob job, NotchUpdateKind kind, string reason)
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      if (job.IsDismissed && kind == NotchUpdateKind.Done)
      {
        return;
      }
      corePresenter.Updates.Present(job.RailKey, kind);
      string text = "notch_card_shown";
      string threadId = job.ThreadId;
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["reason"] = reason;
      agents.TrackJobAction(text, threadId, dictionary);
    }

    // Token: 0x060003B0 RID: 944 RVA: 0x00524424 File Offset: 0x00524424
    private double ClickyUpdateHeight(NotchLiveEvent.ClickyUpdate update)
    {
      NotchUpdateKind notchUpdateKind = NotchUpdateKindNames.Parse(update.Kind);
      INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
      NotchUpdateContent notchUpdateContent = ((updateContent != null) ? updateContent.Content(update.RailKey) : null);
      if (notchUpdateContent == null)
      {
        return ClickyUpdateLayout.PanelHeight(notchUpdateKind, null, false);
      }
      string text = ClickyUpdateLayout.Detail(notchUpdateKind, notchUpdateContent);
      bool flag;
      if (notchUpdateKind == NotchUpdateKind.Done)
      {
        flag = notchUpdateContent.Files.Any<string>((string f) => !ArtifactPaths.IsWebLink(f) && File.Exists(f));
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      return ClickyUpdateLayout.PanelHeight(notchUpdateKind, text, flag2);
    }

    // Token: 0x060003B1 RID: 945 RVA: 0x005244A4 File Offset: 0x005244A4
    private NotchRestingMarkState RestingMarkFromWork()
    {
      AppHost.<>c__DisplayClass328_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass328_0();
      AppHost.<>c__DisplayClass328_0 CS$<>8__locals2 = CS$<>8__locals1;
      RealStack real = this.Real;
      CS$<>8__locals2.agents = ((real != null) ? real.Agents : null);
      if (CS$<>8__locals1.agents == null || !this.EdgeDockIsRunning)
      {
        return new NotchRestingMarkState(0, "idle");
      }
      RealStack real2 = this.Real;
      OneBrainJobReports oneBrainJobReports = ((real2 != null) ? real2.JobReports : null);
      int num = 0;
      bool flag = false;
      bool flag2 = false;
      foreach (string text in (from t in CS$<>8__locals1.agents.Tasks
        select CS$<>8__locals1.agents.GetTaskDetail(t.TaskId) into d
        where d != null && !d.IsDemo
        select d.ClickySlug).OfType<string>().Distinct<string>())
      {
        switch (CS$<>8__locals1.agents.LiveStatusForClicky(text).Phase)
        {
        case ClickyAppLiveStatus.Phases.Working:
          num++;
          break;
        case ClickyAppLiveStatus.Phases.NeedsYou:
          flag = true;
          break;
        case ClickyAppLiveStatus.Phases.Finished:
          if (oneBrainJobReports != null && oneBrainJobReports.IsFinalizingResult(text))
          {
            num++;
          }
          break;
        case ClickyAppLiveStatus.Phases.Failed:
          flag2 = true;
          break;
        }
      }
      foreach (ClickyJob clickyJob in CS$<>8__locals1.agents.Jobs.RailJobs)
      {
        switch (CS$<>8__locals1.agents.RailPhase(clickyJob))
        {
        case ClickyJobRailPhase.Queued:
        case ClickyJobRailPhase.Working:
          num++;
          break;
        case ClickyJobRailPhase.NeedsYou:
          flag = true;
          break;
        case ClickyJobRailPhase.Done:
          if (oneBrainJobReports != null && oneBrainJobReports.IsFinalizingResult(clickyJob))
          {
            num++;
          }
          break;
        case ClickyJobRailPhase.Failed:
          flag2 = true;
          break;
        }
      }
      return new NotchRestingMarkState(num, flag ? "needsYou" : (flag2 ? "failed" : "idle"));
    }

    // Token: 0x060003B2 RID: 946 RVA: 0x005246AC File Offset: 0x005246AC
    private void SyncRestingMark()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      Func<NotchRestingMarkState> restingMarkState = NotchExtensions.RestingMarkState;
      NotchRestingMarkState notchRestingMarkState = ((restingMarkState != null) ? restingMarkState() : null) ?? this.RestingMarkFromWork();
      corePresenter.SetRestingMarkState(this.EdgeDockIsRunning, notchRestingMarkState.WorkingCount > 0);
      NotchExtensions.RaiseRestingMarkChanged();
    }

    // Token: 0x17000081 RID: 129
    // (get) Token: 0x060003B3 RID: 947 RVA: 0x005246FC File Offset: 0x005246FC
    [Nullable(2)]
    private PreSignInIntroViewModel IntroVm
    {
      [NullableContext(2)]
      get
      {
        RealStack real = this.Real;
        if (real == null)
        {
          return null;
        }
        PreSignInIntroViewModel preSignInIntroViewModel;
        if ((preSignInIntroViewModel = this.introVm) == null)
        {
          preSignInIntroViewModel = (this.introVm = this.CreateIntroVm(real));
        }
        return preSignInIntroViewModel;
      }
    }

    // Token: 0x17000082 RID: 130
    // (get) Token: 0x060003B4 RID: 948 RVA: 0x00524730 File Offset: 0x00524730
    [Nullable(2)]
    internal NotchOnboardingCardViewModel CardVm
    {
      [NullableContext(2)]
      get
      {
        RealStack real = this.Real;
        if (real == null)
        {
          return null;
        }
        NotchOnboardingCardViewModel notchOnboardingCardViewModel;
        if ((notchOnboardingCardViewModel = this.cardVm) == null)
        {
          notchOnboardingCardViewModel = (this.cardVm = this.CreateCardVm(real));
        }
        return notchOnboardingCardViewModel;
      }
    }

    // Token: 0x060003B5 RID: 949 RVA: 0x00524764 File Offset: 0x00524764
    private PreSignInIntroViewModel CreateIntroVm(RealStack real)
    {
      PreSignInIntroViewModel preSignInIntroViewModel = new PreSignInIntroViewModel(real.Core.Settings, (string channel, [Nullable(2)] string other) => real.Core.Worker.SetDiscoveryChannelAsync(channel, other, default(CancellationToken)), ([Nullable(2)] string region) => real.Core.Worker.GetLanguageHintsAsync(region, default(CancellationToken)));
      preSignInIntroViewModel.IsSignedIn = real.Core.Auth.IsSignedIn;
      preSignInIntroViewModel.UserFirstName = AppHost.FirstName(real.Core.Auth.User);
      preSignInIntroViewModel.SignInRequested += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.SignIn();
      };
      return preSignInIntroViewModel;
    }

    // Token: 0x060003B6 RID: 950 RVA: 0x00524800 File Offset: 0x00524800
    private NotchOnboardingCardViewModel CreateCardVm(RealStack real)
    {
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = new NotchOnboardingCardViewModel(real.Core.Settings, real.Permissions);
      notchOnboardingCardViewModel.IsSignedIn = real.Core.Auth.IsSignedIn;
      notchOnboardingCardViewModel.SignInRequested += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SignIn));
      };
      notchOnboardingCardViewModel.OnboardingTriggered += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(async delegate
        {
          this.BeginOnboardingIntroSequence(false);
        });
      };
      notchOnboardingCardViewModel.IntroWelcomeFinished += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          IntroWindow introWindow = this.intro;
          if (introWindow == null)
          {
            return;
          }
          introWindow.Hide();
        });
      };
      notchOnboardingCardViewModel.PropertyChanged += this.OnCardVmChanged;
      return notchOnboardingCardViewModel;
    }

    // Token: 0x060003B7 RID: 951 RVA: 0x00524884 File Offset: 0x00524884
    private void OnCardVmChanged([Nullable(2)] object sender, PropertyChangedEventArgs e)
    {
      string propertyName = e.PropertyName;
      bool flag = propertyName == "Kind" || propertyName == "ActivePermission" || propertyName == "ActivePermissionStatus" || propertyName == "NeedsPin";
      if (flag)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncOnboardingPin));
      }
    }

    // Token: 0x060003B8 RID: 952 RVA: 0x005248F0 File Offset: 0x005248F0
    private int? CardStep(NotchOnboardingCardViewModel vm)
    {
      int? num;
      switch (vm.Kind)
      {
      case NotchOnboardingCardKind.SignedOut:
        num = new int?(-1);
        break;
      case NotchOnboardingCardKind.PermissionIntro:
        num = new int?(0);
        break;
      case NotchOnboardingCardKind.Permission:
        num = new int?((vm.ActivePermission.GetValueOrDefault() == PermissionKind.ScreenCapture) ? 2 : 1);
        break;
      case NotchOnboardingCardKind.MeetClicky:
        num = new int?(3);
        break;
      default:
        num = null;
        break;
      }
      return num;
    }

    // Token: 0x060003B9 RID: 953 RVA: 0x00524960 File Offset: 0x00524960
    private void SyncOnboardingPin()
    {
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
      if (notchOnboardingCardViewModel != null)
      {
        NotchPresenter corePresenter = this.CorePresenter;
        if (corePresenter != null)
        {
          IntroWindow introWindow = this.intro;
          int? num = ((introWindow != null && introWindow.Window.Visible && !notchOnboardingCardViewModel.IsIntroHomeWelcomeActive) ? null : this.CardStep(notchOnboardingCardViewModel));
          if (notchOnboardingCardViewModel.Kind == NotchOnboardingCardKind.SignedOut && this.Services.Settings.HasCompletedOnboarding)
          {
            num = null;
          }
          corePresenter.SetIntroHomeWelcomeActive(notchOnboardingCardViewModel.IsIntroHomeWelcomeActive);
          corePresenter.SetOnboardingPin(num);
          RealStack real = this.Real;
          if (real != null)
          {
            NotchOnboardingCardKind kind = notchOnboardingCardViewModel.Kind;
            bool flag = kind - NotchOnboardingCardKind.PermissionIntro <= 1;
            if (flag)
            {
              real.Permissions.StartPolling();
              return;
            }
            real.Permissions.StopPolling();
          }
          return;
        }
      }
    }

    // Token: 0x060003BA RID: 954 RVA: 0x00524A30 File Offset: 0x00524A30
    private void OnboardingAuthChanged(bool signedIn)
    {
      PreSignInIntroViewModel preSignInIntroViewModel = this.introVm;
      if (preSignInIntroViewModel != null)
      {
        preSignInIntroViewModel.IsSignedIn = signedIn;
        PreSignInIntroViewModel preSignInIntroViewModel2 = preSignInIntroViewModel;
        RealStack real = this.Real;
        preSignInIntroViewModel2.UserFirstName = AppHost.FirstName((real != null) ? real.Core.Auth.User : null);
      }
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.cardVm;
      if (notchOnboardingCardViewModel != null)
      {
        notchOnboardingCardViewModel.IsSignedIn = signedIn;
      }
      IntroWindow introWindow = this.intro;
      if (introWindow != null)
      {
        introWindow.OnSignedInChanged(signedIn);
      }
      this.SyncOnboardingPin();
    }

    // Token: 0x060003BB RID: 955 RVA: 0x00524AA0 File Offset: 0x00524AA0
    [NullableContext(2)]
    private static string FirstName(UserProfile user)
    {
      if (user == null)
      {
        return null;
      }
      string text = ((!string.IsNullOrWhiteSpace(user.DisplayName)) ? user.DisplayName.Trim().Split(' ', StringSplitOptions.None)[0] : user.Email.Split('@', StringSplitOptions.None)[0].Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.E087D87E930FD023CB679FE3A3031DC2A4A4F4FAFFDDF74D1F5AC8267743B9E42).FieldHandle))[0]);
      if (text.Length != 0)
      {
        char c = char.ToUpperInvariant(text[0]);
        ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
        string text2 = text;
        return readOnlySpan + text2.Substring(1, text2.Length - 1);
      }
      return null;
    }

    // Token: 0x060003BC RID: 956 RVA: 0x00524B34 File Offset: 0x00524B34
    public void SignIn()
    {
      this.Services.Auth.SignInWithGoogleAsync(default(CancellationToken));
    }

    // Token: 0x17000083 RID: 131
    // (get) Token: 0x060003BD RID: 957 RVA: 0x00524B5C File Offset: 0x00524B5C
    public bool DevSignInAvailable
    {
      get
      {
        RealStack real = this.Real;
        return real != null && real.Core.Auth.SupportsPasswordSignIn && AppBundleConfiguration.IsLocalUrl(real.Core.Endpoints.SupabaseUrl);
      }
    }

    // Token: 0x060003BE RID: 958 RVA: 0x00524B9C File Offset: 0x00524B9C
    public async Task<bool> DevSignInAsync()
    {
      bool flag2;
      if (this.DevSignInAvailable)
      {
        RealStack real = this.Real;
        if (real != null)
        {
          string text = Environment.GetEnvironmentVariable("HEYCLICKY_DEV_TEST_USER");
          if (string.IsNullOrWhiteSpace(text) || !File.Exists(text))
          {
            text = null;
            for (DirectoryInfo directoryInfo = new DirectoryInfo(AppContext.BaseDirectory); directoryInfo != null; directoryInfo = directoryInfo.Parent)
            {
              string text2 = Path.Combine(directoryInfo.FullName, "windows", "devstack", "test-user.json");
              if (File.Exists(text2))
              {
                text = text2;
                break;
              }
              text2 = Path.Combine(directoryInfo.FullName, "devstack", "test-user.json");
              if (File.Exists(text2))
              {
                text = text2;
                break;
              }
            }
          }
          if (text == null)
          {
            Log.Warn("auth", "dev sign-in: test-user.json not found (set HEYCLICKY_DEV_TEST_USER)");
            return false;
          }
          try
          {
            string text3 = await File.ReadAllTextAsync(text, default(CancellationToken));
            using (JsonDocument doc = JsonDocument.Parse(text3, default(JsonDocumentOptions)))
            {
              string @string = doc.RootElement.GetProperty("email").GetString();
              string string2 = doc.RootElement.GetProperty("password").GetString();
              bool flag = await real.Core.Auth.SignInWithPasswordAsync(@string, string2, default(CancellationToken));
              Log.Info("auth", flag ? "dev sign-in succeeded (local test account)" : "dev sign-in failed");
              flag2 = flag;
            }
          }
          catch (Exception ex)
          {
            Log.Error("auth", "dev sign-in", ex);
            flag2 = false;
          }
          return flag2;
        }
      }
      flag2 = false;
      return flag2;
    }

    // Token: 0x060003BF RID: 959 RVA: 0x00524BE0 File Offset: 0x00524BE0
    public void DismissOnboardingPin()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.DismissOnboardingPin();
        return;
      }
      this.Services.Presenter.ReturnToIdle();
    }

    // Token: 0x060003C0 RID: 960 RVA: 0x00524C10 File Offset: 0x00524C10
    private void StartNotchPolish(RealStack real)
    {
      real.Shortcuts.MouseDown += delegate(PointD p)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.OnMouseDownAnywhere(p);
        });
      };
      HotkeyRouter hotkeyRouter = this.hotkeys;
      if (hotkeyRouter != null)
      {
        hotkeyRouter.NotchChatToggle = new Func<bool>(this.ToggleNotchChatFromShortcut);
        hotkeyRouter.NotchEscape = new Func<bool>(this.HandleNotchEscape);
        hotkeyRouter.NotchEscapeWouldClaim = new Func<bool>(this.NotchEscapeWouldClaim);
        hotkeyRouter.NotchViewerEscapeWouldClaim = new Func<bool>(this.NotchViewerEscapeWouldClaim);
        hotkeyRouter.LessonReplaying = delegate
        {
          LessonService lessonService = this.lessons;
          return lessonService != null && lessonService.IsReplaying;
        };
        hotkeyRouter.StopLessonReplay = delegate
        {
          LessonService lessonService2 = this.lessons;
          if (lessonService2 == null)
          {
            return;
          }
          lessonService2.StopReplayForEscape();
        };
      }
      this.NotchUi.HoverChanged += delegate(bool inside)
      {
        if (inside && this.HoverDwellWillOpenSomething())
        {
          this.Services.Cursor.SetHoverDwell(new double?(NotchPresenter.HoverDwell.TotalSeconds));
          return;
        }
        this.Services.Cursor.SetHoverDwell(null);
      };
      this.NotchUi.HoverDwellCompleted += delegate
      {
        this.Services.Cursor.SetHoverDwell(null);
      };
      HeaderPills.HidesPromotionalButtons = () => this.HidesHomePromotionalButtons;
      this.Services.Updates.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = AppHost.<>O.<8>__RaiseChanged) == null)
        {
          dispatcherQueueHandler = (AppHost.<>O.<8>__RaiseChanged = new DispatcherQueueHandler(HeaderPills.RaiseChanged));
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
      BillingUi billingUi = this.Services.Billing;
      if (billingUi != null)
      {
        billingUi.Changed += delegate
        {
          DispatcherQueue dispatcherQueue2 = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler2;
          if ((dispatcherQueueHandler2 = AppHost.<>O.<8>__RaiseChanged) == null)
          {
            dispatcherQueueHandler2 = (AppHost.<>O.<8>__RaiseChanged = new DispatcherQueueHandler(HeaderPills.RaiseChanged));
          }
          dispatcherQueue2.TryEnqueue(dispatcherQueueHandler2);
        };
      }
      real.Tutorial.PropertyChanged += delegate([Nullable(2)] object _, PropertyChangedEventArgs e)
      {
        string propertyName = e.PropertyName;
        bool flag = propertyName == "IsEngaged" || propertyName == "Phase" || propertyName == "IsRestrictingNormalUse";
        if (flag)
        {
          DispatcherQueue dispatcherQueue3 = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler3;
          if ((dispatcherQueueHandler3 = AppHost.<>O.<8>__RaiseChanged) == null)
          {
            dispatcherQueueHandler3 = (AppHost.<>O.<8>__RaiseChanged = new DispatcherQueueHandler(HeaderPills.RaiseChanged));
          }
          dispatcherQueue3.TryEnqueue(dispatcherQueueHandler3);
        }
      };
    }

    // Token: 0x060003C1 RID: 961 RVA: 0x00524D40 File Offset: 0x00524D40
    public void OpenMicrophoneSettings()
    {
      this.OpenSettings(NotchSettingsSection.Microphone);
    }

    // Token: 0x060003C2 RID: 962 RVA: 0x00524D4C File Offset: 0x00524D4C
    private bool HoverDwellWillOpenSomething()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.IsShowingNotchAgentPeek)
      {
        return false;
      }
      NotchPresenter corePresenter2 = this.CorePresenter;
      if (corePresenter2 != null && corePresenter2.RequiresPointerExitBeforeHoverExpansion)
      {
        return false;
      }
      if (!this.Services.Auth.IsSignedIn || this.introSequenceActive || DrawnOnboardingTour.SuppressesNotchHover)
      {
        return false;
      }
      RealStack real = this.Real;
      OnboardingTutorialViewModel onboardingTutorialViewModel = ((real != null) ? real.Tutorial : null);
      if (onboardingTutorialViewModel != null && onboardingTutorialViewModel.IsRestrictingNormalUse)
      {
        return true;
      }
      if (!this.TourHoldsHome)
      {
        NotchPresenter corePresenter3 = this.CorePresenter;
        if (corePresenter3 == null || !corePresenter3.IsTextResponseInProgress)
        {
          NotchPresentation presentation = this.Services.Presenter.Presentation;
          bool flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
          return !flag;
        }
      }
      return false;
    }

    // Token: 0x17000084 RID: 132
    // (get) Token: 0x060003C3 RID: 963 RVA: 0x00524E10 File Offset: 0x00524E10
    public bool HidesHomePromotionalButtons
    {
      get
      {
        if (!this.introSequenceActive && !this.TourHoldsHome)
        {
          RealStack real = this.Real;
          return real != null && real.Tutorial.IsEngaged;
        }
        return true;
      }
    }

    // Token: 0x060003C4 RID: 964 RVA: 0x00524E3C File Offset: 0x00524E3C
    private void CheckForUpdatesFromMenu(string surface)
    {
      AppHost.<>c__DisplayClass355_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass355_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.updates = this.Services.Updates;
      if (CS$<>8__locals1.updates.State == UpdateState.ReadyToInstall || HeaderPills.PreviewUpdateReady)
      {
        CS$<>8__locals1.<CheckForUpdatesFromMenu>g__ShowUpdatesRow|0();
        return;
      }
      if (CS$<>8__locals1.updates.State != UpdateState.Unavailable)
      {
        if (this.manualUpdateProbe != null)
        {
          CS$<>8__locals1.updates.Changed -= this.manualUpdateProbe;
        }
        CS$<>8__locals1.started = DateTimeOffset.Now;
        this.manualUpdateProbe = delegate([Nullable(2)] object _, EventArgs _)
        {
          UpdateState state = CS$<>8__locals1.updates.State;
          bool flag = state - UpdateState.Idle <= 1;
          if (flag && DateTimeOffset.Now - CS$<>8__locals1.started < TimeSpan.FromMinutes(2L))
          {
            return;
          }
          CS$<>8__locals1.updates.Changed -= CS$<>8__locals1.<>4__this.manualUpdateProbe;
          CS$<>8__locals1.<>4__this.manualUpdateProbe = null;
          CS$<>8__locals1.<>4__this.FinishManualUpdateProbe(state);
        };
        CS$<>8__locals1.updates.Changed += this.manualUpdateProbe;
        CS$<>8__locals1.updates.CheckNowAsync(surface);
        return;
      }
      Log.Info("updates", "check from " + surface + ": not an installed copy");
      if (!this.NotchShowsSettings)
      {
        this.SayAtCursorAsync("this copy of heyclicky can't update itself. the installed app checks every hour.", true, 5.0, default(CancellationToken));
        return;
      }
      CS$<>8__locals1.<CheckForUpdatesFromMenu>g__ShowUpdatesRow|0();
    }

    // Token: 0x060003C5 RID: 965 RVA: 0x00524F30 File Offset: 0x00524F30
    private void FinishManualUpdateProbe(UpdateState state)
    {
      bool flag = state - UpdateState.Downloading <= 1;
      if (flag)
      {
        this.OpenSettings(NotchSettingsSection.General);
        return;
      }
      if (this.NotchShowsSettings)
      {
        return;
      }
      string text;
      if (state != UpdateState.UpToDate)
      {
        if (state != UpdateState.Failed)
        {
          text = null;
        }
        else
        {
          text = "i couldn't check for updates just now. try again in a bit.";
        }
      }
      else
      {
        text = "you're up to date, version " + this.Services.Updates.CurrentVersion + " is the newest.";
      }
      string text2 = text;
      if (text2 != null)
      {
        this.SayAtCursorAsync(text2, true, 5.0, default(CancellationToken));
      }
    }

    // Token: 0x060003C6 RID: 966 RVA: 0x00524FB4 File Offset: 0x00524FB4
    private void OnMouseDownAnywhere(PointD p)
    {
      if (this.NotchUi == null || this.NotchUi.ContainsScreenPointPx(p))
      {
        return;
      }
      if (Native.IsOwnWindowAt((int)p.X, (int)p.Y))
      {
        return;
      }
      NotchPresentation presentation = this.Services.Presenter.Presentation;
      bool flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
      if (flag)
      {
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.IsNotchChatSessionOpen && corePresenter.IsNotchChatAwaitingAnswer)
      {
        this.MoveNotchChatAnswerToCaptions();
        return;
      }
      bool flag2 = false;
      NotchPresentation.LiveEvent liveEvent = presentation as NotchPresentation.LiveEvent;
      if (liveEvent != null && liveEvent.Event is NotchLiveEvent.OnboardingTutorialResume)
      {
        this.NotchUi.HoverChanged -= this.DismissResumeOnHoverOut;
        this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialResume));
        flag2 = true;
      }
      NotchPresenter corePresenter2 = this.CorePresenter;
      if (corePresenter2 != null && corePresenter2.CollapseHoverAfterOutsideClick())
      {
        flag2 = true;
      }
      if (flag2)
      {
        Log.Info("notch", "click outside: the hover-opened card closed");
      }
    }

    // Token: 0x060003C7 RID: 967 RVA: 0x005250B4 File Offset: 0x005250B4
    private void MoveNotchChatAnswerToCaptions()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      RealStack real = this.Real;
      if (real != null)
      {
        real.Companion.MoveTypedAnswerToCaptions();
      }
      corePresenter.SuppressHoverUntilPointerReentry();
      if (corePresenter.RendersTextResponsesInNotchChat)
      {
        ConversationHistory history = NotchChat.History;
        if (history != null)
        {
          history.NoteAnswerStopped();
        }
      }
      corePresenter.Present(new NotchPresentation.Idle());
      corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.ClickElsewhereDuringAnswer);
      Log.Info("notch", "click outside mid-answer: the chat closed, the rest of the answer comes as captions");
    }

    // Token: 0x060003C8 RID: 968 RVA: 0x00525124 File Offset: 0x00525124
    private void OnForegroundChanged(bool previousIsOurs, bool nextIsOurs)
    {
      if (previousIsOurs || nextIsOurs || this.NotchUi == null || this.NotchUi.IsHovering)
      {
        return;
      }
      NotchPresentation presentation = this.Services.Presenter.Presentation;
      bool flag = presentation is NotchPresentation.TextInput || presentation is NotchPresentation.TextResponse;
      if (flag)
      {
        return;
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.CollapseHoverAfterOutsideClick())
      {
        Log.Info("notch", "another app came to the front: the open notch folded");
      }
    }

    // Token: 0x17000085 RID: 133
    // (get) Token: 0x060003C9 RID: 969 RVA: 0x0052519C File Offset: 0x0052519C
    // (set) Token: 0x060003CA RID: 970 RVA: 0x005251A4 File Offset: 0x005251A4
    [Nullable(2)]
    public ProactiveCoordinator Proactive
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x060003CB RID: 971 RVA: 0x005251B0 File Offset: 0x005251B0
    private void StartProactive(RealStack real)
    {
      this.Proactive = new ProactiveCoordinator(this, real);
      this.Proactive.Changed += delegate
      {
        ArcDock dock = this.Dock;
        if (dock == null)
        {
          return;
        }
        dock.NoteMembershipInputsChanged();
      };
      this.Proactive.Start();
    }

    // Token: 0x060003CC RID: 972 RVA: 0x005251E4 File Offset: 0x005251E4
    internal void CollapseNotchForPreview()
    {
      if (this.Services.Presenter.Presentation is NotchPresentation.TextInput)
      {
        this.Services.Presenter.Present(new NotchPresentation.Idle());
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.CollapseHoverAfterOutsideClick();
      }
      NotchPresenter corePresenter2 = this.CorePresenter;
      if (corePresenter2 == null)
      {
        return;
      }
      corePresenter2.SuppressHoverUntilPointerReentry();
    }

    // Token: 0x060003CD RID: 973 RVA: 0x00525240 File Offset: 0x00525240
    public void OpenRemoteTaskComposer(string title)
    {
      Log.Info("Notifications", "modify \"" + title + "\" by text: composer open");
      this.Services.Audio.PlayEarcon("textOpen");
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null && corePresenter.RoutesTextModeToNotchChat)
      {
        corePresenter.OpenNotchChatSession(NotchChatSessionOpenVia.DoubleControl);
        return;
      }
      this.Services.Presenter.Present(new NotchPresentation.TextInput("", new NotchAgentFollowUp(null, "this agent")));
    }

    // Token: 0x17000086 RID: 134
    // (get) Token: 0x060003CE RID: 974 RVA: 0x005252BC File Offset: 0x005252BC
    private bool IntroOwnsFlow
    {
      get
      {
        if (this.IsPreSignInIntroShowing)
        {
          return true;
        }
        NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
        if ((notchOnboardingCardViewModel != null && notchOnboardingCardViewModel.IsIntroHomeWelcomeActive) || this.fakeIntroWelcomeActive)
        {
          return false;
        }
        RealStack real = this.Real;
        if (real != null)
        {
          return PreSignInIntroViewModel.ShouldPresent(real.Core.Settings.Read<OnboardingProgress>(([Nullable(1)] AppSettings s) => s.Onboarding), real.Core.Auth.IsSignedIn);
        }
        return !this.Services.Settings.HasCompletedIntro;
      }
    }

    // Token: 0x17000087 RID: 135
    // (get) Token: 0x060003CF RID: 975 RVA: 0x00525354 File Offset: 0x00525354
    private bool HasCompletedOnboardingNow
    {
      get
      {
        RealStack real = this.Real;
        if (real == null)
        {
          return this.Services.Settings.HasCompletedOnboarding;
        }
        return real.Core.Settings.Read<bool>((AppSettings s) => s.Onboarding.HasCompletedOnboarding);
      }
    }

    // Token: 0x060003D0 RID: 976 RVA: 0x005253AC File Offset: 0x005253AC
    private void RequestIntroAttention(string source)
    {
      if (this.intro == null)
      {
        this.intro = this.CreateIntro();
      }
      if (this.intro.IsShowing)
      {
        this.intro.Window.BringToFrontRecentered();
      }
      else
      {
        this.intro.Show(this.intro.CurrentStep);
      }
      ClickyAnalytics.TrackPreSignInIntroAttentionRequested(IntroWindow.AnalyticsName(this.intro.CurrentStep));
      Log.Info("reopen", source + ": the intro comes to the front (" + IntroWindow.AnalyticsName(this.intro.CurrentStep) + ")");
    }

    // Token: 0x060003D1 RID: 977 RVA: 0x00525444 File Offset: 0x00525444
    private void HandleReopen()
    {
      ClickyAnalytics.TrackDockIconPanelOpened();
      if (this.IntroOwnsFlow)
      {
        this.RequestIntroAttention("second launch");
        return;
      }
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsRunning)
      {
        tutorialWindow.BringForward();
        Log.Info("reopen", "second launch: the tour comes to the front");
        return;
      }
      if (!this.HasCompletedOnboardingNow)
      {
        NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
        if (notchOnboardingCardViewModel != null)
        {
          int? num = this.CardStep(notchOnboardingCardViewModel);
          if (num != null)
          {
            int valueOrDefault = num.GetValueOrDefault();
            this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(valueOrDefault));
          }
        }
        Log.Info("reopen", "second launch: the notch's onboarding card");
        return;
      }
      HeyClickyWindow shared = HeyClickyWindow.Shared;
      if (shared.IsOpen && !shared.WasInFrontJustNow && shared.BringOpenWindowToFront())
      {
        Log.Info("reopen", "second launch: the open HeyClicky window comes to the front");
        return;
      }
      this.OpenNotchHome();
      Log.Info("reopen", "second launch: the notch's Home");
    }

    // Token: 0x060003D2 RID: 978 RVA: 0x00525528 File Offset: 0x00525528
    internal void SimulateSecondLaunch()
    {
      this.HandleReopen();
    }

    // Token: 0x060003D3 RID: 979 RVA: 0x00525530 File Offset: 0x00525530
    private void ShowSignInAgain()
    {
      if (this.Services.Auth.IsSignedIn)
      {
        return;
      }
      if (this.IntroOwnsFlow)
      {
        this.RequestIntroAttention("refused sign-in callback");
        return;
      }
      if (this.HasCompletedOnboardingNow)
      {
        Log.Info("reopen", "refused sign-in callback: onboarded, the sign-in card stays on hover");
        return;
      }
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
      if (notchOnboardingCardViewModel != null)
      {
        int? num = this.CardStep(notchOnboardingCardViewModel);
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(valueOrDefault));
        }
      }
      Log.Info("reopen", "refused sign-in callback: the notch's sign-in card");
    }

    // Token: 0x060003D4 RID: 980 RVA: 0x005255C4 File Offset: 0x005255C4
    private void HandleTrayClick()
    {
      if (this.IntroOwnsFlow)
      {
        this.RequestIntroAttention("tray click");
        return;
      }
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsRunning)
      {
        tutorialWindow.BringForward();
        Log.Info("reopen", "tray click: the tour comes to the front");
        return;
      }
      this.OpenSettings();
    }

    // Token: 0x060003D5 RID: 981 RVA: 0x00525614 File Offset: 0x00525614
    public void Relaunch()
    {
      Log.Info("app", "relaunch requested");
      if (!AppRelaunch.ScheduleRelaunch(null))
      {
        Log.Warn("app", "relaunch helper didn't start: staying open");
        return;
      }
      this.Quit();
    }

    // Token: 0x060003D6 RID: 982 RVA: 0x00525644 File Offset: 0x00525644
    public async void ResetAllDataAndQuit()
    {
      AppHost.<>c__DisplayClass378_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass378_0();
      if (!this.resetting)
      {
        this.resetting = true;
        Log.Info("app", "reset all data and quit");
        CS$<>8__locals1.customFolder = null;
        try
        {
          AppHost.<>c__DisplayClass378_0 CS$<>8__locals2 = CS$<>8__locals1;
          RealStack real = this.Real;
          CS$<>8__locals2.customFolder = ((real != null) ? real.Core.Settings.Current.AgentFolderPath : null);
        }
        catch
        {
        }
        try
        {
          RealStack real2 = this.Real;
          if (real2 != null)
          {
            await real2.Agents.SignOutAsync();
          }
        }
        catch (Exception ex)
        {
          Log.Warn("reset", "agents: " + ex.Message);
        }
        try
        {
          await this.Services.Auth.SignOutAsync();
        }
        catch (Exception ex2)
        {
          Log.Warn("reset", "sign-out: " + ex2.Message);
        }
        try
        {
          ClickyAnalytics.ResetIdentity();
        }
        catch
        {
        }
        await Task.Run(delegate
        {
          List<string> list = new List<string>();
          if (CS$<>8__locals1.customFolder == null)
          {
            list.Add(AgentFolders.Default);
          }
          else if (string.Equals(Path.GetFullPath(CS$<>8__locals1.customFolder).TrimEnd('\\'), AgentFolders.LegacyDocuments, StringComparison.OrdinalIgnoreCase))
          {
            list.Add(Path.Combine(AgentFolders.LegacyDocuments, "agents"));
          }
          foreach (string text in list)
          {
            if (Directory.Exists(text))
            {
              try
              {
                FileSystem.DeleteDirectory(text, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                Log.Info("reset", text + " → Recycle Bin");
              }
              catch (Exception ex3)
              {
                Log.Warn("reset", text + ": " + ex3.Message);
              }
            }
          }
        });
        string appDataDirectory = AppBundleConfiguration.AppDataDirectory;
        if (Path.GetFileName(appDataDirectory.TrimEnd('\\')) == "HeyClicky" && !AppPaths.IsOverridden && appDataDirectory.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), StringComparison.OrdinalIgnoreCase))
        {
          AppRelaunch.RunAfterExit("Remove-Item -LiteralPath " + AppRelaunch.Quote(appDataDirectory) + " -Recurse -Force -ErrorAction SilentlyContinue", "reset");
        }
        Log.Info("reset", appDataDirectory + " is erased once HeyClicky has quit");
        await Task.Delay(250);
        this.Quit();
      }
    }

    // Token: 0x060003D7 RID: 983 RVA: 0x0052567C File Offset: 0x0052567C
    private void WireRoutinePower(RealStack real)
    {
      real.Agents.Routines.Wake.DisplayOffMeansStandby = AppHost.IsModernStandbyPc();
      this.NotchUi.Window.RawMessage += delegate(uint msg, IntPtr w, IntPtr l)
      {
        if (msg != 536U)
        {
          return;
        }
        if (w == (IntPtr)4)
        {
          real.Agents.NotifySystemSleeping();
          return;
        }
        long num = (long)w;
        bool flag = num == 7L || num == 18L;
        if (flag)
        {
          real.Agents.NotifyResumedFromSleep();
          return;
        }
        if (w == (IntPtr)32787 && l != 0)
        {
          bool? flag2 = AppHost.DisplayStateFrom(l);
          if (flag2 != null)
          {
            bool valueOrDefault = flag2.GetValueOrDefault();
            Log.Info("power", "display " + (valueOrDefault ? "on" : "off"));
            real.Agents.NotifyDisplayPower(valueOrDefault);
          }
        }
      };
      this.displayPowerNotification = AppHost.RegisterPowerSettingNotification(this.NotchUi.Window.Hwnd, in AppHost.GUID_CONSOLE_DISPLAY_STATE, 0);
      if (this.displayPowerNotification == 0)
      {
        string text = "power";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 1);
        defaultInterpolatedStringHandler.AppendLiteral("display power notifications unavailable (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Marshal.GetLastWin32Error());
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060003D8 RID: 984 RVA: 0x0052573C File Offset: 0x0052573C
    private static bool? DisplayStateFrom(IntPtr setting)
    {
      if (Marshal.PtrToStructure<Guid>(setting) != AppHost.GUID_CONSOLE_DISPLAY_STATE)
      {
        return null;
      }
      if (Marshal.ReadInt32(setting, 16) < 4)
      {
        return null;
      }
      return new bool?(Marshal.ReadInt32(setting, 20) != 0);
    }

    // Token: 0x060003D9 RID: 985 RVA: 0x0052578C File Offset: 0x0052578C
    private static bool IsModernStandbyPc()
    {
      byte[] array = new byte[76];
      bool flag;
      try
      {
        if (AppHost.CallNtPowerInformation(4, (IntPtr)0, 0, array, array.Length) != 0U)
        {
          flag = false;
        }
        else
        {
          bool flag2 = array[20] > 0;
          Log.Info("power", flag2 ? "Modern Standby PC: the display going off is the sleep for routines" : "S3 sleep PC");
          flag = flag2;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("power", "power capabilities: " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x060003DA RID: 986
    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, in Guid powerSettingGuid, int flags);

    // Token: 0x060003DB RID: 987
    [DllImport("powrprof.dll")]
    private static extern uint CallNtPowerInformation(int informationLevel, IntPtr inputBuffer, int inputBufferLength, byte[] outputBuffer, int outputBufferLength);

    // Token: 0x060003DC RID: 988 RVA: 0x0052580C File Offset: 0x0052580C
    internal static void PrepareStoredSettings(SettingsStore store, AgentRuntimeOptions agentOptions)
    {
      store.Update(delegate(AppSettings s)
      {
        if (s.AgentFolderPath == null && Directory.Exists(Path.Combine(AgentFolders.LegacyDocuments, "agents")) && !Directory.Exists(Path.Combine(AgentFolders.Default, "agents")))
        {
          s.AgentFolderPath = AgentFolders.LegacyDocuments;
          Log.Info("settings", "agent folder: keeping the existing Clickys in " + AgentFolders.LegacyDocuments);
        }
      });
      AppSettings appSettings = store.Current;
      agentOptions.ProjectsRoot = appSettings.AgentFolderPath ?? AgentFolders.Default;
      agentOptions.AutoApproveExtraUsage = appSettings.AgentAutoApproveExtraUsage;
      string obsidianVaultPath = appSettings.ObsidianVaultPath;
      agentOptions.ObsidianVaultPath = ((obsidianVaultPath != null && Directory.Exists(obsidianVaultPath)) ? obsidianVaultPath : null);
      AudioDevices.PreferredCaptureId = appSettings.MicrophoneDeviceId;
      AudioDevices.PreferredRenderId = appSettings.OutputDeviceId;
      ScreenDrawingCanvas.JavaScriptJitEnabled = appSettings.DrawingJavaScriptJit;
      OverlayWindow.ShowInScreenRecordings = appSettings.ShowInScreenRecordings;
      ScreenDrawingCanvas.ShowInScreenRecordings = appSettings.ShowInScreenRecordings;
      CaptureExclusion.ShowInScreenRecordings = appSettings.ShowInScreenRecordings;
      DispatcherQueue ui = DispatcherQueue.GetForCurrentThread();
      CaptureExclusion.SetHiddenFromOwnCapture = (bool hidden) => AppHost.HideFromOwnCaptureAsync(hidden, ui);
      ScreenDrawingCanvas.FiguresFloat = appSettings.StreamMode;
      ThreeStage.StreamMode = appSettings.StreamMode;
    }

    // Token: 0x060003DD RID: 989 RVA: 0x005258FC File Offset: 0x005258FC
    private static async Task<bool> HideFromOwnCaptureAsync(bool hidden, [Nullable(2)] DispatcherQueue ui)
    {
      bool flag = await ScreenDrawingCanvas.HideFromOwnCaptureAsync(hidden).ConfigureAwait(false);
      bool drawingChanged = flag;
      TaskCompletionSource<bool> drive = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
      if (ui == null || !ui.TryEnqueue(DispatcherQueuePriority.High, delegate
      {
        drive.TrySetResult(OverlayWindow.SetHiddenFromOwnCapture(hidden));
      }))
      {
        drive.TrySetResult(false);
      }
      ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(drive.Task, Task.Delay(250)).ConfigureAwait(false).GetAwaiter();
      if (!configuredTaskAwaiter.IsCompleted)
      {
        await configuredTaskAwaiter;
        ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        configuredTaskAwaiter = configuredTaskAwaiter2;
        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
      }
      bool flag2 = configuredTaskAwaiter.GetResult() == drive.Task && drive.Task.Result;
      return drawingChanged || flag2;
    }

    // Token: 0x060003DE RID: 990 RVA: 0x00525948 File Offset: 0x00525948
    private void StartApplyingSettings()
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return;
      }
      SettingsStore store = real.Core.Settings;
      this.ApplySettings(store.Current, true);
      DispatcherQueueHandler <>9__7;
      store.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = <>9__7) == null)
        {
          dispatcherQueueHandler = (<>9__7 = delegate
          {
            this.ApplySettings(store.Current, false);
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
      real.Agents.AutoApproveExtraUsageChanged += delegate(bool on)
      {
        store.Update(delegate(AppSettings s)
        {
          s.AgentAutoApproveExtraUsage = on;
        });
      };
      CustomConnectorStore.Changed += delegate
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.ApplyCustomConnectors));
      };
      ObsidianVault.Initialize(store);
      DispatcherQueueHandler <>9__9;
      ConnectorAuth.Initialize(real.Agents, new Action<string>(this.OpenConnectorAuthorization), delegate
      {
        DispatcherQueue dispatcherQueue2 = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler2;
        if ((dispatcherQueueHandler2 = <>9__9) == null)
        {
          dispatcherQueueHandler2 = (<>9__9 = delegate
          {
            this.OpenSettings(NotchSettingsSection.Integrations);
          });
        }
        dispatcherQueue2.TryEnqueue(dispatcherQueueHandler2);
      });
      ConnectedApps.Initialize(real.Agents, () => real.Core.Endpoints.WorkerBaseUrl.AbsoluteUri);
      if (real.Core.Auth.IsSignedIn)
      {
        ConnectorAuth.OnLaunchAsync();
        ConnectedApps.RefreshAsync();
      }
      real.AgentVoice.ShowAtCursor = delegate(string line)
      {
        real.Cursor.ShowMessage(line);
      };
      this.IntegrationAuthCallback = delegate(string toolkit, bool failed)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          if (this.InkNoteSignInReturned(toolkit, failed))
          {
            return;
          }
          ProactiveCoordinator proactive = this.Proactive;
          if (proactive != null && proactive.OnIntegrationAuthCallback(toolkit, failed))
          {
            return;
          }
          this.OpenSettings(NotchSettingsSection.Integrations);
          NotchSettingsTab.NoteIntegrationCallback(toolkit);
          real.Agents.InvalidateConnectedApps();
        });
      };
      this.ApplyCustomConnectors();
      if (AppPaths.IsOverridden)
      {
        Log.Info("settings", "launch at login not written: the data folder is moved (a test run)");
        return;
      }
      try
      {
        if (LaunchAtLogin.IsEnabled("HeyClicky") != store.Current.LaunchAtLogin)
        {
          LaunchAtLogin.SetEnabled(store.Current.LaunchAtLogin, "HeyClicky", null);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "launch at login: " + ex.Message);
      }
    }

    // Token: 0x060003DF RID: 991 RVA: 0x00525B08 File Offset: 0x00525B08
    private void OpenConnectorAuthorization(string url)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_DEV_AUTH_URL_FILE");
      if (environmentVariable != null && environmentVariable.Length > 0)
      {
        try
        {
          File.AppendAllText(environmentVariable, url + Environment.NewLine);
          Log.Info("connectors", "authorization URL handed to the test harness");
        }
        catch (Exception ex)
        {
          Log.Warn("connectors", "dev auth url file: " + ex.Message);
        }
        return;
      }
      this.DispatcherQueue.TryEnqueue(delegate
      {
        this.FoldNotchForOpenedLink();
        this.OpenUrl(url);
      });
    }

    // Token: 0x060003E0 RID: 992 RVA: 0x00525BB0 File Offset: 0x00525BB0
    private void ApplyCustomConnectors()
    {
      RealStack real = this.Real;
      if (real == null || !real.IsComplete)
      {
        return;
      }
      IReadOnlyList<CustomConnectorRegistration> readOnlyList = CustomConnectorStore.Registrations();
      real.Agents.SetCustomConnectors(readOnlyList);
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
      defaultInterpolatedStringHandler.AppendLiteral("custom connectors → agents: ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060003E1 RID: 993 RVA: 0x00525C18 File Offset: 0x00525C18
    private void ApplySettings(AppSettings now, bool initial)
    {
      AppHost.<>c__DisplayClass395_0 CS$<>8__locals1;
      CS$<>8__locals1.initial = initial;
      CS$<>8__locals1.now = now;
      RealStack real = this.Real;
      if (real == null || !real.IsComplete)
      {
        return;
      }
      CS$<>8__locals1.before = this.appliedSettings;
      this.appliedSettings = CS$<>8__locals1.now;
      if (AppHost.<ApplySettings>g__Changed|395_0<HotkeyBindings>(([Nullable(1)] AppSettings s) => s.EffectiveHotkeys, ref CS$<>8__locals1))
      {
        real.Shortcuts.Configure(CS$<>8__locals1.now.EffectiveHotkeys);
        HotkeyBindings effectiveHotkeys = CS$<>8__locals1.now.EffectiveHotkeys;
        real.Dictation.HasDedicatedHandsFreeShortcut = effectiveHotkeys.HandsFreeDictation.Trigger != HotkeyTrigger.DoubleTap || effectiveHotkeys.HandsFreeDictation.Modifiers != effectiveHotkeys.Dictation.Modifiers;
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<string>(([Nullable(1)] AppSettings s) => s.MicrophoneDeviceId, ref CS$<>8__locals1))
      {
        AudioDevices.PreferredCaptureId = CS$<>8__locals1.now.MicrophoneDeviceId;
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<string>(([Nullable(1)] AppSettings s) => s.OutputDeviceId, ref CS$<>8__locals1))
      {
        AudioDevices.PreferredRenderId = CS$<>8__locals1.now.OutputDeviceId;
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<ValueTuple<bool, bool>>(([Nullable(1)] AppSettings s) => new ValueTuple<bool, bool>(s.DrawingSoundsEnabled, s.DrawingPenSoundsEnabled), ref CS$<>8__locals1))
      {
        real.Canvas.PlaysSounds = CS$<>8__locals1.now.DrawingSoundsEnabled;
        real.Canvas.PlaysPenSounds = CS$<>8__locals1.now.DrawingSoundsEnabled && CS$<>8__locals1.now.DrawingPenSoundsEnabled;
        Log.Info("settings", "drawing sounds " + (CS$<>8__locals1.now.DrawingSoundsEnabled ? "on" : "off") + ", pen sounds " + ((CS$<>8__locals1.now.DrawingSoundsEnabled && CS$<>8__locals1.now.DrawingPenSoundsEnabled) ? "on" : "off"));
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.DrawingSubtitlesEffective, ref CS$<>8__locals1))
      {
        real.Canvas.ShowsSubtitles = CS$<>8__locals1.now.DrawingSubtitlesEffective;
        Log.Info("settings", "drawing subtitles " + (CS$<>8__locals1.now.DrawingSubtitlesEffective ? "on" : "off"));
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.ScreenHighlightEnabled, ref CS$<>8__locals1))
      {
        real.Highlight.IsEnabled = CS$<>8__locals1.now.ScreenHighlightEnabled;
        if (!CS$<>8__locals1.now.ScreenHighlightEnabled && !CS$<>8__locals1.initial)
        {
          real.Highlight.Clear();
        }
        Log.Info("settings", "screen highlight " + (CS$<>8__locals1.now.ScreenHighlightEnabled ? "on" : "off"));
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<string>(([Nullable(1)] AppSettings s) => s.CursorColorHex, ref CS$<>8__locals1))
      {
        real.Cursor.SetColor(CS$<>8__locals1.now.CursorColorHex);
        TrayIcon trayIcon = this.tray;
        if (trayIcon != null)
        {
          trayIcon.SetColor(AppHost.TrayColor(CS$<>8__locals1.now.CursorColorHex));
        }
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.ShowInScreenRecordings, ref CS$<>8__locals1) && !CS$<>8__locals1.initial)
      {
        OverlayWindow.ShowInScreenRecordings = CS$<>8__locals1.now.ShowInScreenRecordings;
        ScreenDrawingCanvas.ShowInScreenRecordings = CS$<>8__locals1.now.ShowInScreenRecordings;
        CaptureExclusion.ShowInScreenRecordings = CS$<>8__locals1.now.ShowInScreenRecordings;
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<string>(([Nullable(1)] AppSettings s) => s.AgentFolderPath, ref CS$<>8__locals1) && !CS$<>8__locals1.initial)
      {
        real.Agents.SetProjectsRoot(CS$<>8__locals1.now.AgentFolderPath ?? AgentFolders.Default);
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.AgentAutoApproveExtraUsage, ref CS$<>8__locals1))
      {
        real.Agents.AutoApproveExtraUsage = CS$<>8__locals1.now.AgentAutoApproveExtraUsage;
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<string>(([Nullable(1)] AppSettings s) => s.ObsidianVaultPath, ref CS$<>8__locals1) && !CS$<>8__locals1.initial)
      {
        AgentService agents = real.Agents;
        string obsidianVaultPath = CS$<>8__locals1.now.ObsidianVaultPath;
        agents.SetObsidianVaultPath((obsidianVaultPath != null && Directory.Exists(obsidianVaultPath)) ? obsidianVaultPath : null);
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.ShowAgentLabelsInActivityStrip, ref CS$<>8__locals1))
      {
        this.NotchUi.SizeContext.AgentLabelsHidden = !CS$<>8__locals1.now.ShowAgentLabelsInActivityStrip;
        if (!CS$<>8__locals1.initial)
        {
          this.NotchUi.Apply(this.Services.Presenter.Presentation, true, false);
        }
      }
      if (!CS$<>8__locals1.initial)
      {
        if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.DrawingJavaScriptJit, ref CS$<>8__locals1))
        {
          Log.Info("settings", "JavaScript JIT change takes effect on the next launch");
        }
      }
      if (AppHost.<ApplySettings>g__Changed|395_0<bool>((AppSettings s) => s.StreamMode, ref CS$<>8__locals1))
      {
        this.NotchUi.SetStreamMode(CS$<>8__locals1.now.StreamMode);
        ScreenDrawingCanvas.FiguresFloat = CS$<>8__locals1.now.StreamMode;
        ThreeStage.StreamMode = CS$<>8__locals1.now.StreamMode;
      }
    }

    // Token: 0x060003E2 RID: 994 RVA: 0x005261B4 File Offset: 0x005261B4
    private static uint TrayColor(string hex)
    {
      uint num = 4278190080U;
      Color color = DS.CursorColor(hex);
      return num | (uint)(((int)color.R << 16) | ((int)color.G << 8) | (int)color.B);
    }

    // Token: 0x060003E3 RID: 995 RVA: 0x005261EC File Offset: 0x005261EC
    private void ForgetSignedOutAccount(RealStack real)
    {
      if (!this.accountWasSignedIn)
      {
        return;
      }
      this.accountWasSignedIn = false;
      Log.Info("auth", "signed out: folding the notch and clearing this account's local state");
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.Unspecified);
      }
      NotchPresenter corePresenter2 = this.CorePresenter;
      if (corePresenter2 != null)
      {
        corePresenter2.DismissNotchAgentPeek();
      }
      NotchPresenter corePresenter3 = this.CorePresenter;
      if (corePresenter3 != null)
      {
        corePresenter3.Tabs.ResetToHome();
      }
      NotchChatComposer.ClearEveryDraft();
      NotchPresenter corePresenter4 = this.CorePresenter;
      if (corePresenter4 != null)
      {
        corePresenter4.Updates.Reset();
      }
      try
      {
        HeyClickyWindow.Shared.Close();
      }
      catch (Exception ex)
      {
        Log.Warn("auth", "window: " + ex.Message);
      }
      try
      {
        ClickyBrowserBridge.Shared.Shutdown();
      }
      catch (Exception ex2)
      {
        Log.Warn("auth", "browser: " + ex2.Message);
      }
      SettingsContent.ClearParkedConnectorDraft();
      try
      {
        ClickyLiveNoteStore.Shared.RemoveAllForSignOut();
      }
      catch (Exception ex3)
      {
        Log.Warn("auth", "ink notes: " + ex3.Message);
      }
      try
      {
        ArcDock dock = this.Dock;
        if (dock != null)
        {
          dock.RemoveAllForSignOut();
        }
      }
      catch (Exception ex4)
      {
        Log.Warn("auth", "dock: " + ex4.Message);
      }
      real.Core.Settings.Update(delegate(AppSettings s)
      {
        s.Onboarding.DiscoveryChannel = null;
        s.Onboarding.DiscoveryChannelOther = null;
        s.Onboarding.DiscoveryChannelSynced = false;
        s.DiscoveryChannelPendingSync = null;
      });
      try
      {
        real.Dictation.CancelActiveSession();
      }
      catch (Exception ex5)
      {
        Log.Warn("auth", "dictation: " + ex5.Message);
      }
      try
      {
        if (this.Services.Companion.State != VoiceState.Idle)
        {
          this.Services.Companion.Cancel();
        }
      }
      catch (Exception ex6)
      {
        Log.Warn("auth", "companion: " + ex6.Message);
      }
      this.UpdateTrayTooltip();
    }

    // Token: 0x060003E4 RID: 996 RVA: 0x00526414 File Offset: 0x00526414
    private void NoteAccountSignedIn()
    {
      this.accountWasSignedIn = true;
    }

    // Token: 0x17000088 RID: 136
    // (get) Token: 0x060003E5 RID: 997 RVA: 0x00526420 File Offset: 0x00526420
    // (set) Token: 0x060003E6 RID: 998 RVA: 0x00526428 File Offset: 0x00526428
    [Nullable(new byte[] { 2, 1 })]
    public Action<string> BrowserHandoffLineSink
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x17000089 RID: 137
    // (get) Token: 0x060003E7 RID: 999 RVA: 0x00526434 File Offset: 0x00526434
    // (set) Token: 0x060003E8 RID: 1000 RVA: 0x0052643C File Offset: 0x0052643C
    [Nullable(2)]
    public Action<string, bool> IntegrationAuthCallback
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060003E9 RID: 1001 RVA: 0x00526448 File Offset: 0x00526448
    private void StartSystemServices(RealStack real)
    {
      Func<JsonObject> func;
      if ((func = AppHost.<>O.<9>__FrontmostApplicationProperties) == null)
      {
        func = (AppHost.<>O.<9>__FrontmostApplicationProperties = new Func<JsonObject>(AppHost.FrontmostApplicationProperties));
      }
      ClickyAnalytics.FrontmostApplicationProperties = func;
      ClickyAnalytics.TrackAppOpened();
      MainThreadStallMonitor.Start(this.DispatcherQueue);
      MemoryMonitor.Start();
      this.updates = new UpdateService(this.DispatcherQueue, null);
      this.updates.QuitApp = new Action(this.Quit);
      this.updates.ReadIdleContext = (bool readQuiet) => this.ReadIdleInstallContext(real, readQuiet);
      this.WireIdleInstallBusySignals(this.updates, real);
      this.Services.Updates = this.updates;
      this.updates.Start();
      string newVersion = this.updates.ConsumePendingInstallCompletion();
      if (newVersion != null)
      {
        DispatcherQueueHandler <>9__3;
        Task.Delay(TimeSpan.FromSeconds(3L)).ContinueWith<bool>(delegate(Task _)
        {
          DispatcherQueue dispatcherQueue = this.DispatcherQueue;
          DispatcherQueueHandler dispatcherQueueHandler;
          if ((dispatcherQueueHandler = <>9__3) == null)
          {
            dispatcherQueueHandler = (<>9__3 = delegate
            {
              this.PresentAppUpdatedNotice(newVersion);
            });
          }
          return dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
        }, TaskScheduler.Default);
      }
      Action action;
      if ((action = AppHost.<>O.<10>__ReportPreviousCrashes) == null)
      {
        action = (AppHost.<>O.<10>__ReportPreviousCrashes = new Action(CrashReporting.ReportPreviousCrashes));
      }
      Task.Run(action);
      this.RefreshAppConfigAsync();
      this.WireTelemetry(real);
      this.WirePower();
      real.Core.Auth.ReferralCodeReceived += delegate([Nullable(2)] object _, string code)
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          this.ReceiveFriendReferralCode(code, true);
        });
      };
      if (real.Core.Auth.IsSignedIn)
      {
        this.ClaimPendingReferralCodeAsync();
      }
    }

    // Token: 0x060003EA RID: 1002 RVA: 0x005265C4 File Offset: 0x005265C4
    [NullableContext(2)]
    private static JsonObject FrontmostApplicationProperties()
    {
      ForegroundApp foregroundApp = ForegroundAppInfo.Capture();
      if (foregroundApp == null)
      {
        return null;
      }
      JsonObject jsonObject = new JsonObject(null);
      if (!string.IsNullOrEmpty(foregroundApp.ProcessName))
      {
        jsonObject["focused_app_bundle_id"] = foregroundApp.ProcessName;
      }
      if (!string.IsNullOrEmpty(foregroundApp.AppName))
      {
        jsonObject["focused_app_name"] = foregroundApp.AppName;
      }
      return jsonObject;
    }

    // Token: 0x060003EB RID: 1003 RVA: 0x00526634 File Offset: 0x00526634
    private IdleInstallContext ReadIdleInstallContext(RealStack real, bool readQuiet)
    {
      bool flag = readQuiet && !QuietContext.IgnoredForTests;
      bool flag2 = this.AgentWorking();
      bool flag3 = this.Services.Companion.State != VoiceState.Idle || real.Dictation.IsSessionActive;
      bool flag4;
      if (!ClickyDriveState.IsDrivingNow)
      {
        ClickyTakeOverController clickyDrive = this.ClickyDrive;
        if (clickyDrive == null || clickyDrive.CurrentPhase.Kind == ClickyTakeOverController.PhaseKind.Idle)
        {
          flag4 = ChromeSetupFlow.Shared.IsShowingHow;
          goto IL_0063;
        }
      }
      flag4 = true;
      IL_0063:
      NotchPresenter corePresenter = this.CorePresenter;
      bool flag5 = corePresenter != null && corePresenter.IsShowingNotchAgentPeek;
      bool isRecording = ClickyDrawingWindowViewer.Shared.IsRecording;
      bool flag6 = !this.Services.Settings.HasCompletedOnboarding || this.introSequenceActive || this.TourHoldsHome || real.Tutorial.IsEngaged;
      bool flag7 = flag && QuietContext.IsFullscreenOn((IntPtr)0);
      bool flag8 = flag && QuietContext.QueryNotificationState() == QuietContext.NotificationState.PresentationMode;
      bool flag9 = flag && QuietContext.IsAnotherAppUsingMicrophone();
      NotchPresenter corePresenter2 = this.CorePresenter;
      bool flag10 = corePresenter2 != null && corePresenter2.IsTextResponseInProgress;
      bool flag11;
      if (!real.Canvas.IsShowingDrawing)
      {
        BillingUi billingUi = this.billing;
        flag11 = billingUi != null && billingUi.IsPaywallVisible;
      }
      else
      {
        flag11 = true;
      }
      return new IdleInstallContext(flag2, flag3, flag4, flag5, isRecording, flag6, flag7, flag8, flag9, flag10, flag11);
    }

    // Token: 0x060003EC RID: 1004 RVA: 0x00526750 File Offset: 0x00526750
    private bool AgentWorking()
    {
      return this.Services.Agents.Tasks.Any<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
      {
        AgentTaskStatus status = t.Status;
        return status <= AgentTaskStatus.WaitingForUser;
      });
    }

    // Token: 0x060003ED RID: 1005 RVA: 0x00526788 File Offset: 0x00526788
    private void WireIdleInstallBusySignals(UpdateService updates, RealStack real)
    {
      this.Services.Companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (this.Services.Companion.State != VoiceState.Idle)
        {
          updates.NoteHeyClickyBusy();
        }
      };
      this.Services.Agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (this.AgentWorking())
        {
          updates.NoteHeyClickyBusy();
        }
      };
      real.Dictation.Completed += delegate(DictationResult _)
      {
        updates.NoteHeyClickyBusy();
      };
    }

    // Token: 0x060003EE RID: 1006 RVA: 0x005267F8 File Offset: 0x005267F8
    public void PresentAppUpdatedNotice(string version)
    {
      if (!this.Services.Settings.HasCompletedOnboarding)
      {
        Log.Info("updates", "updated to v" + version + "; the card is skipped mid-onboarding");
        return;
      }
      Log.Info("updates", "showing the \"HeyClicky updated\" card for v" + version);
      this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.AppUpdated(version), null);
    }

    // Token: 0x1700008A RID: 138
    // (get) Token: 0x060003EF RID: 1007 RVA: 0x00526868 File Offset: 0x00526868
    public static string ChangelogUrl
    {
      get
      {
        return AppRemoteConfig.Shared.Links.Changelog;
      }
    }

    // Token: 0x060003F0 RID: 1008 RVA: 0x0052687C File Offset: 0x0052687C
    private Task RefreshAppConfigAsync()
    {
      RealStack real = this.Real;
      if (real == null)
      {
        return Task.CompletedTask;
      }
      return AppRemoteConfig.Shared.RefreshAsync(real.Core.Worker, default(CancellationToken));
    }

    // Token: 0x060003F1 RID: 1009 RVA: 0x005268B8 File Offset: 0x005268B8
    public void OpenChangelog(string surface)
    {
      ClickyAnalytics.TrackChangelogOpened(surface);
      this.OpenUrl(AppHost.ChangelogUrl);
    }

    // Token: 0x060003F2 RID: 1010 RVA: 0x005268CC File Offset: 0x005268CC
    private void WireTelemetry(RealStack real)
    {
      this.Services.Shortcuts.ShortcutTriggered += delegate(object _, ShortcutAction action)
      {
        if (this.Services.Settings.IsPaused)
        {
          return;
        }
        if (action == ShortcutAction.PushToTalkDown)
        {
          ClickyAnalytics.TrackPushToTalkStarted();
          return;
        }
        if (action == ShortcutAction.PushToTalkUp)
        {
          ClickyAnalytics.TrackPushToTalkReleased();
        }
      };
      real.Companion.MessageSent += delegate(object _, [TupleElementNames(new string[] { "CharacterCount", "Typed", "Trigger" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<int, bool, string> m)
      {
        NotchPresentation presentation = this.Services.Presenter.Presentation;
        bool flag;
        if (!(presentation is NotchPresentation.OnboardingPinned) && !(presentation is NotchPresentation.TextInput) && !(presentation is NotchPresentation.TextResponse))
        {
          NotchPresenter corePresenter = this.CorePresenter;
          flag = corePresenter != null && corePresenter.IsShowingNotchAgentPeek;
        }
        else
        {
          flag = true;
        }
        bool flag2 = flag;
        string text = (m.Item2 ? "text" : "voice");
        string text2;
        if (!m.Item2)
        {
          text2 = "global_voice_input";
        }
        else
        {
          NotchPresenter corePresenter2 = this.CorePresenter;
          text2 = ((corePresenter2 != null && corePresenter2.IsNotchChatSessionOpen) ? "notch_chat" : "notch_text_box");
        }
        JsonObject jsonObject = ClickyAnalytics.InteractionContext(text, text2, "closed", false, false, flag2);
        jsonObject["trigger"] = m.Item3;
        int item = m.Item1;
        string text3 = (m.Item2 ? "text" : "voice");
        string item2 = m.Item3;
        bool flag3 = item2 == "voice" || item2 == "typed" || item2 == "text";
        ClickyAnalytics.TrackUserMessageSent(item, text3, "assistant", !flag3, "one_brain", this.Services.Settings.LibraryModeEnabled, jsonObject);
      };
      real.Companion.Failed += delegate([Nullable(2)] object _, CompanionError error)
      {
        AppHost.TrackCompanionFailure(real, error);
      };
      real.Dictation.Completed += delegate(DictationResult result)
      {
        if (result.Outcome == DictationDeliveryOutcome.Nothing)
        {
          return;
        }
        int num = result.Text.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
        int length = result.Text.Length;
        DictationDeliveryOutcome outcome = result.Outcome;
        string text4;
        if (outcome != DictationDeliveryOutcome.Inserted)
        {
          if (outcome != DictationDeliveryOutcome.PastedBlind)
          {
            text4 = "clipboard";
          }
          else
          {
            text4 = "pasted_blind";
          }
        }
        else
        {
          text4 = "inserted";
        }
        ClickyAnalytics.TrackDictationCompleted(num, length, text4, result.ClipboardReason, result.HandsFree, result.UsedBatch, result.UsedBatch ? "batch" : "gemini", result.UsedWarmConnection, false, new int?(result.ReleaseToInsertMs), null, null);
      };
      real.Agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        this.TrackAgentEvent(real.Agents, e);
      };
      this.WireAgentCostLedger(real);
      real.Core.Billing.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        if (real.Core.Auth.IsSignedIn)
        {
          PlanSnapshot plan2 = real.Core.Billing.Plan;
          if (plan2 != null)
          {
            AppHost.TrackPlan(plan2);
          }
        }
      };
      PlanSnapshot plan = real.Core.Billing.Plan;
      if (plan != null)
      {
        AppHost.TrackPlan(plan);
      }
    }

    // Token: 0x060003F3 RID: 1011 RVA: 0x005269D8 File Offset: 0x005269D8
    private static void TrackCompanionFailure(RealStack real, CompanionError error)
    {
      string code = error.Code;
      string text;
      if (!(code == "turn_failed"))
      {
        if (!(code == "ears_failed"))
        {
          if (!(code == "voice_unavailable"))
          {
            if (!(code == "no_screenshot"))
            {
              text = null;
            }
            else
            {
              text = "screen_capture";
            }
          }
          else
          {
            text = "tts_playback";
          }
        }
        else
        {
          text = "dictation_recognition";
        }
      }
      else
      {
        text = (real.Companion.LastTurnWasTyped ? "realtime_typed_turn" : "voice_response");
      }
      string text2 = text;
      if (text2 == null)
      {
        return;
      }
      string text3 = (error.Message.Contains("credit", StringComparison.OrdinalIgnoreCase) ? "credits_exhausted" : "generic");
      ClickyAnalytics.TrackAppError(text2, text3, error.Message, error.Code, 0, AppHost.UserFacingLine(error), true);
      if (error.Code == "turn_failed")
      {
        ClickyAnalytics.TrackResponseError(error.Message);
      }
    }

    // Token: 0x060003F4 RID: 1012 RVA: 0x00526AB8 File Offset: 0x00526AB8
    private static string UserFacingLine(CompanionError error)
    {
      string text;
      if (error.Code == "turn_failed")
      {
        text = (error.Message.StartsWith("HTTP 429", StringComparison.Ordinal) ? "You're asking me a lot right now. Give me a minute, then try again." : (BrainSession.IsSafetyRefusal(error.Message) ? "My AI provider's safety filter blocked this because it looks like hands-on security work. I can still explain the concepts." : "Sorry, I couldn't get an answer just now. Try asking me again."));
      }
      else
      {
        text = error.Message;
      }
      return text;
    }

    // Token: 0x060003F5 RID: 1013 RVA: 0x00526B18 File Offset: 0x00526B18
    private static string AgentAnalyticsSource([Nullable(2)] string launchSource)
    {
      string text;
      if (!(launchSource == "voice"))
      {
        if (!(launchSource == "routine"))
        {
          if (!(launchSource == "suggestion"))
          {
            if (!(launchSource == "suggested_action"))
            {
              text = "text";
            }
            else
            {
              text = "suggested_action";
            }
          }
          else
          {
            text = "proactive_suggestion";
          }
        }
        else
        {
          text = "routine";
        }
      }
      else
      {
        text = "voice";
      }
      return text;
    }

    // Token: 0x060003F6 RID: 1014 RVA: 0x00526B84 File Offset: 0x00526B84
    private void TrackAgentEvent(AgentService agents, AgentTaskEvent e)
    {
      try
      {
        AgentTaskDetail taskDetail = agents.GetTaskDetail(e.TaskId);
        string text = ((taskDetail != null) ? taskDetail.UserPrompts.LastOrDefault<string>() : null);
        string text2 = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null);
        AgentTaskEventKind agentTaskEventKind = e.Kind;
        if (agentTaskEventKind != AgentTaskEventKind.StatusChanged)
        {
          switch (agentTaskEventKind)
          {
          case AgentTaskEventKind.Finished:
          {
            string text3;
            if ((text3 = ((taskDetail != null) ? taskDetail.Snapshot.FinalResponseMarkdown : null)) == null && (text3 = ((taskDetail != null) ? taskDetail.Summary : null)) == null)
            {
              text3 = e.Text ?? "";
            }
            int? num = ((taskDetail == null) ? null : new int?((int)(DateTimeOffset.Now - taskDetail.Snapshot.StartedAt).TotalMilliseconds));
            int? num2 = ((taskDetail == null) ? null : new int?((int)Math.Min(2147483647L, taskDetail.InputTokens + taskDetail.OutputTokens)));
            ClickyAnalytics.TrackAgentFinished(text3, text2, num, num2, text, false, null, null);
            break;
          }
          case AgentTaskEventKind.Failed:
          {
            bool flag = ((taskDetail != null) ? taskDetail.Snapshot.ThreadId : null) != null || ((taskDetail != null) ? taskDetail.TurnId : null) != null;
            string text4;
            if ((text4 = e.Text) == null)
            {
              text4 = ((taskDetail != null) ? taskDetail.FailureMessage : null) ?? "unknown";
            }
            ClickyAnalytics.TrackAgentFailed(text4, flag ? "run" : "launch", text2, text, false, null, null, false, (taskDetail != null) ? taskDetail.FailureDiagnostic : null);
            break;
          }
          case AgentTaskEventKind.Stopped:
            ClickyAnalytics.TrackAgentInterrupted(e.Text ?? "Stopped.", text2, text, false, null, null, taskDetail != null && taskDetail.InterruptedUnexpectedly);
            break;
          case AgentTaskEventKind.NeedsExtraUsageApproval:
            ClickyAnalytics.TrackAgentExtraEffortApprovalShown(text2);
            break;
          case AgentTaskEventKind.SteerAccepted:
          {
            string text5 = e.Text ?? "";
            string text6 = "text";
            string text7 = text2;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["is_job"] = ((taskDetail != null) ? taskDetail.JobId : null) != null;
            jsonObject["is_follow_up"] = true;
            jsonObject["is_new_job_run"] = false;
            jsonObject["is_steer"] = true;
            ClickyAnalytics.TrackAgentFollowedUp(text5, text6, text7, jsonObject);
            break;
          }
          case AgentTaskEventKind.IntegrationUsed:
          {
            string text8 = e.Text;
            if (text8 != null && text8.Length > 0)
            {
              ClickyAnalytics.TrackAgentUsedIntegration(text2, text, text8);
            }
            break;
          }
          case AgentTaskEventKind.ExtraUsageApproved:
            ClickyAnalytics.TrackAgentExtraEffortApproved(text2);
            break;
          }
        }
        else if (e.Text == "Starting")
        {
          HashSet<string> hashSet = this.agentsSeen;
          bool flag3;
          lock (hashSet)
          {
            flag3 = this.agentsSeen.Add(e.TaskId);
          }
          if (((taskDetail != null) ? taskDetail.LaunchSource : null) == "extra_effort")
          {
            return;
          }
          bool flag4 = taskDetail != null && taskDetail.IsNewJobRun;
          if (!flag3 || text2 != null || flag4)
          {
            string text9 = text ?? "";
            string text10 = AppHost.AgentAnalyticsSource((taskDetail != null) ? taskDetail.LaunchSource : null);
            string text11 = text2;
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["is_job"] = ((taskDetail != null) ? taskDetail.JobId : null) != null;
            jsonObject2["is_follow_up"] = true;
            jsonObject2["is_new_job_run"] = flag4;
            jsonObject2["is_steer"] = false;
            ClickyAnalytics.TrackAgentFollowedUp(text9, text10, text11, jsonObject2);
          }
          else
          {
            string text12 = text ?? "";
            string text13 = ((taskDetail != null && taskDetail.IsRoutineRun) ? "routine" : "text");
            JsonObject jsonObject3 = new JsonObject(null);
            jsonObject3["is_job"] = ((taskDetail != null) ? taskDetail.JobId : null) != null;
            jsonObject3["is_follow_up"] = false;
            jsonObject3["is_new_job_run"] = false;
            ClickyAnalytics.TrackAgentStarted(text12, text13, jsonObject3);
          }
        }
        agentTaskEventKind = e.Kind;
        bool flag2 = agentTaskEventKind - AgentTaskEventKind.Finished <= 2;
        if (flag2 && taskDetail != null && text2 != null)
        {
          ClickyAITracing.Mode mode = ClickyAITracing.Mode.Agent;
          string text14 = text2;
          JsonObject jsonObject4 = new JsonObject(null);
          jsonObject4["clicky_agent_thread_title"] = taskDetail.Snapshot.Title;
          ClickyAITracing.EmitTraceStartIfNeeded(mode, text14, jsonObject4);
          bool flag5 = e.Kind == AgentTaskEventKind.Finished;
          string text15 = (flag5 ? "completed" : ((e.Kind == AgentTaskEventKind.Stopped) ? "interrupted" : "failed"));
          string text16 = text2;
          string text17 = "codex_agent_turn";
          string text18 = null;
          string text19 = text ?? "";
          string text20;
          if ((text20 = taskDetail.Snapshot.FinalResponseMarkdown) == null)
          {
            text20 = taskDetail.Summary ?? "";
          }
          long? num3 = ((taskDetail.InputTokens > 0L) ? new long?(taskDetail.InputTokens) : null);
          long? num4 = ((taskDetail.OutputTokens > 0L) ? new long?(taskDetail.OutputTokens) : null);
          double? num5 = new double?((DateTimeOffset.Now - taskDetail.Snapshot.StartedAt).TotalSeconds);
          bool flag6 = !flag5;
          string text21;
          if (!flag5)
          {
            if ((text21 = e.Text) == null)
            {
              text21 = taskDetail.FailureMessage ?? text15;
            }
          }
          else
          {
            text21 = null;
          }
          JsonObject jsonObject5 = new JsonObject(null);
          jsonObject5["clicky_turn_status"] = text15;
          jsonObject5["clicky_screenshot_count"] = 0;
          jsonObject5["clicky_used_computer_use"] = false;
          jsonObject5["clicky_computer_use_tool_names"] = new JsonArray(null);
          jsonObject5["clicky_computer_use_tool_count"] = 0;
          jsonObject5["clicky_integrations_used"] = new JsonArray(null);
          jsonObject5["clicky_integrations_used_count"] = 0;
          ClickyAITracing.EmitAgentGeneration(text16, text17, text18, text19, text20, num3, num4, num5, flag6, text21, jsonObject5);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "agent event: " + ex.Message);
      }
    }

    // Token: 0x060003F7 RID: 1015 RVA: 0x005271C4 File Offset: 0x005271C4
    private static void TrackPlan(PlanSnapshot plan)
    {
      string effectiveTier = plan.EffectiveTier;
      string text;
      if (!(effectiveTier == "max"))
      {
        if (!(effectiveTier == "pro"))
        {
          text = "Free";
        }
        else
        {
          text = "HeyClicky Pro";
        }
      }
      else
      {
        text = "HeyClicky Max";
      }
      string text2 = text;
      ClickyAnalytics.UpdateUserPlanProperties(plan.Plan, text2, plan.Plan == "unlimited");
    }

    // Token: 0x060003F8 RID: 1016 RVA: 0x00527228 File Offset: 0x00527228
    private void WirePower()
    {
      this.NotchUi.Window.RawMessage += delegate(uint msg, IntPtr w, IntPtr _)
      {
        if (msg == 26U)
        {
          MotionPreference.Refresh();
          return;
        }
        if (msg != 536U)
        {
          return;
        }
        if (w == (IntPtr)4)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OnSuspend));
          return;
        }
        long num = (long)w;
        bool flag = num == 7L || num == 18L;
        if (flag)
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OnResume));
        }
      };
    }

    // Token: 0x060003F9 RID: 1017 RVA: 0x00527248 File Offset: 0x00527248
    private void OnSuspend()
    {
      Log.Info("power", "going to sleep: stopping voice and dictation");
      try
      {
        RealStack real = this.Real;
        if (real != null)
        {
          real.Dictation.CancelActiveSession();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("power", "dictation: " + ex.Message);
      }
      try
      {
        if (this.Services.Companion.State != VoiceState.Idle)
        {
          this.Services.Companion.Cancel();
        }
      }
      catch (Exception ex2)
      {
        Log.Warn("power", "companion: " + ex2.Message);
      }
      try
      {
        RealStack real2 = this.Real;
        if (real2 != null)
        {
          real2.Highlight.Clear();
        }
      }
      catch
      {
      }
    }

    // Token: 0x060003FA RID: 1018 RVA: 0x00527320 File Offset: 0x00527320
    private void OnResume()
    {
      if (DateTimeOffset.UtcNow - this.lastResume < TimeSpan.FromSeconds(5L))
      {
        return;
      }
      this.lastResume = DateTimeOffset.UtcNow;
      UpdateService updateService = this.updates;
      if (updateService != null)
      {
        updateService.InstallIfIdle();
      }
      Log.Info("power", "woke up: re-placing windows, warming the voice, refreshing the plan");
      this.ReplaceSurfacesAfterDisplayChange();
      RealStack real = this.Real;
      if (real != null && real.Core.Auth.IsSignedIn)
      {
        real.Companion.Prewarm();
        real.Core.Billing.RefreshAsync();
      }
      bool flag = this.updates != null;
      if (flag)
      {
        UpdateState state = this.updates.State;
        bool flag2 = state == UpdateState.Idle || state == UpdateState.UpToDate || state == UpdateState.Failed;
        flag = flag2;
      }
      if (flag)
      {
        Task.Run(() => this.updates.CheckAfterWakeAsync());
      }
    }

    // Token: 0x060003FB RID: 1019 RVA: 0x005273FC File Offset: 0x005273FC
    private void ReplaceSurfacesAfterDisplayChange()
    {
      try
      {
        Native.SendMessage(this.NotchUi.Window.Hwnd, 126U, (IntPtr)0, (IntPtr)0);
      }
      catch (Exception ex)
      {
        Log.Warn("power", "notch: " + ex.Message);
      }
    }

    // Token: 0x060003FC RID: 1020 RVA: 0x00527454 File Offset: 0x00527454
    private bool HandleSystemDeepLink(Uri uri)
    {
      string text = uri.Host.ToLowerInvariant();
      string text2 = uri.AbsolutePath.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault<string>();
      if (text != null && text.Length == 0)
      {
        this.SayBrowserHandoffLine("(・ω・)ノ hi, i'm here!");
        return true;
      }
      if (!(text == "referral"))
      {
        if (text == "skill")
        {
          Log.Info("deeplink", "skill/" + ((text2 == null) ? "" : Uri.UnescapeDataString(text2)) + ": skills are deprecated; ignored");
          return true;
        }
        if (!(text == "integrations-auth-callback"))
        {
          if (!(text == "checkout-complete"))
          {
            return false;
          }
          CheckoutCompleteLink checkoutCompleteLink = WorkerClient.ParseCheckoutCompleteLink(uri);
          if (checkoutCompleteLink != null)
          {
            ClickyAnalytics.TrackCheckoutReturned(checkoutCompleteLink.IsCanceled, checkoutCompleteLink.IsPortal, checkoutCompleteLink.CheckoutSessionId);
          }
          return false;
        }
        else
        {
          Dictionary<string, string> parameters = AuthDeepLink.ParseParameters(uri.OriginalString);
          string text3 = null;
          foreach (string text4 in new string[] { "integration", "toolkit", "integration_id" })
          {
            string text5;
            if (parameters.TryGetValue(text4, out text5) && !string.IsNullOrWhiteSpace(text5))
            {
              text3 = text5.Trim().ToLowerInvariant();
              break;
            }
          }
          bool flag = parameters.Keys.Any<string>(delegate(string k)
          {
            string text8 = k.ToLowerInvariant();
            bool flag5 = text8 == "error" || text8 == "error_description" || text8 == "error_code";
            return flag5 && !string.IsNullOrEmpty(parameters[k]);
          });
          if (!flag)
          {
            string text6;
            bool flag2 = parameters.TryGetValue("status", out text6);
            if (flag2)
            {
              string text7 = text6.Trim().ToLowerInvariant();
              bool flag3 = text7 == "error" || text7 == "failed" || text7 == "failure" || text7 == "cancelled" || text7 == "canceled";
              flag2 = flag3;
            }
            flag = flag2;
          }
          bool flag4 = flag;
          Log.Info("deeplink", "integrations auth callback: " + (text3 ?? "unknown integration") + " " + (flag4 ? "failed" : "connected"));
          if (!this.Services.Auth.IsSignedIn)
          {
            return true;
          }
          Action<string, bool> integrationAuthCallback = this.IntegrationAuthCallback;
          if (integrationAuthCallback != null)
          {
            integrationAuthCallback(text3, flag4);
          }
          return true;
        }
      }
      else
      {
        if (string.IsNullOrWhiteSpace(text2))
        {
          Log.Info("deeplink", "referral link with no code");
          return true;
        }
        bool isSignedIn = this.Services.Auth.IsSignedIn;
        ClickyAnalytics.TrackReferralDeepLinkOpened(isSignedIn);
        this.ReceiveFriendReferralCode(Uri.UnescapeDataString(text2), false);
        if (isSignedIn)
        {
          ClickyAnalytics.NoteReferralPageEntryPoint("deep_link");
          this.OpenSettings(NotchSettingsSection.Referral);
        }
        return true;
      }
    }

    // Token: 0x060003FD RID: 1021 RVA: 0x00527704 File Offset: 0x00527704
    private void SayBrowserHandoffLine(string line)
    {
      Log.Info("deeplink", "handoff line: " + line);
      Action<string> browserHandoffLineSink = this.BrowserHandoffLineSink;
      if (browserHandoffLineSink != null)
      {
        browserHandoffLineSink(line);
        return;
      }
      this.SayAtCursorAsync(line, true, 5.0, default(CancellationToken));
    }

    // Token: 0x1700008B RID: 139
    // (get) Token: 0x060003FE RID: 1022 RVA: 0x00527754 File Offset: 0x00527754
    private static string PendingReferralFile
    {
      get
      {
        return Path.Combine(AppBundleConfiguration.AppDataDirectory, "pending-referral.json");
      }
    }

    // Token: 0x060003FF RID: 1023 RVA: 0x00527768 File Offset: 0x00527768
    private void ReceiveFriendReferralCode(string code, bool fromAuthCallback)
    {
      code = code.Trim();
      if (code.Length == 0)
      {
        return;
      }
      Log.Info("referral", "friend code received (" + (fromAuthCallback ? "auth callback" : "deep link") + ")");
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(AppHost.PendingReferralFile));
        string text;
        if (!this.Services.Auth.IsSignedIn)
        {
          text = null;
        }
        else
        {
          RealStack real = this.Real;
          text = ((real != null) ? real.Core.Auth.UserId : null);
        }
        string text2 = text;
        string pendingReferralFile = AppHost.PendingReferralFile;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["code"] = code;
        jsonObject["owner"] = text2;
        File.WriteAllText(pendingReferralFile, jsonObject.ToJsonString(null));
      }
      catch (Exception ex)
      {
        Log.Warn("referral", "pending code not saved: " + ex.Message);
      }
      if (this.Services.Auth.IsSignedIn)
      {
        this.ClaimPendingReferralCodeAsync();
      }
    }

    // Token: 0x06000400 RID: 1024 RVA: 0x00527878 File Offset: 0x00527878
    private async Task ClaimPendingReferralCodeAsync()
    {
      RealStack real = this.Real;
      if (real != null && real.Core.Auth.IsSignedIn)
      {
        string userId = real.Core.Auth.UserId;
        if (userId != null)
        {
          if (Interlocked.Exchange(ref this.claimingReferral, 1) != 1)
          {
            try
            {
              if (File.Exists(AppHost.PendingReferralFile))
              {
                JsonObject jsonObject = JsonNode.Parse(await File.ReadAllTextAsync(AppHost.PendingReferralFile, default(CancellationToken)).ConfigureAwait(false), null, default(JsonDocumentOptions)) as JsonObject;
                string text;
                if (jsonObject == null)
                {
                  text = null;
                }
                else
                {
                  JsonNode jsonNode = jsonObject["code"];
                  text = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
                }
                string text2 = text;
                string text3;
                if (jsonObject == null)
                {
                  text3 = null;
                }
                else
                {
                  JsonNode jsonNode2 = jsonObject["owner"];
                  text3 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
                }
                string text4 = text3;
                if (string.IsNullOrWhiteSpace(text2))
                {
                  File.Delete(AppHost.PendingReferralFile);
                }
                else if (text4 != null && text4 != userId)
                {
                  Log.Info("referral", "pending code belongs to another account; left for it");
                }
                else
                {
                  try
                  {
                    ReferralOutcomeResponse referralOutcomeResponse = await real.Core.Worker.ClaimReferralAsync(text2, "deep_link", default(CancellationToken)).ConfigureAwait(false);
                    string text5 = ((referralOutcomeResponse != null) ? referralOutcomeResponse.Outcome : null) ?? "request_failed";
                    ClickyAnalytics.TrackReferralFriendCodeClaimResult("deep_link", text5);
                    string text6 = "referral";
                    string text7 = "claim answered: ";
                    string text8 = ((referralOutcomeResponse != null) ? referralOutcomeResponse.Message : null);
                    Log.Info(text6, text7 + text5 + ((text8 != null) ? (" (" + text8 + ")") : ""));
                    if (referralOutcomeResponse != null)
                    {
                      File.Delete(AppHost.PendingReferralFile);
                    }
                  }
                  catch (Exception ex)
                  {
                    ClickyAnalytics.TrackReferralFriendCodeClaimResult("deep_link", "request_failed");
                    Log.Warn("referral", "claim failed (kept for next time): " + ex.Message);
                  }
                }
              }
            }
            catch (Exception ex2)
            {
              Log.Warn("referral", "pending code: " + ex2.Message);
            }
            finally
            {
              Interlocked.Exchange(ref this.claimingReferral, 0);
            }
          }
        }
      }
    }

    // Token: 0x06000401 RID: 1025 RVA: 0x005278BC File Offset: 0x005278BC
    private void StartTaskbarButton(RealStack real)
    {
      if (OverlayWindow.Offscreen)
      {
        return;
      }
      this.taskbarButton = new TaskbarButton("HeyClicky", Path.Combine(AppContext.BaseDirectory, "Assets", "AppIcon.ico"));
      this.taskbarButton.Clicked += delegate
      {
        Log.Info("taskbar", "button clicked");
        this.HandleReopen();
      };
      this.taskbarButton.CloseRequested += delegate
      {
        Log.Info("taskbar", "quit from the taskbar button");
        this.Quit();
      };
      this.SyncTaskbarButton(real);
      DispatcherQueueHandler <>9__3;
      real.Core.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        DispatcherQueue dispatcherQueue = this.DispatcherQueue;
        DispatcherQueueHandler dispatcherQueueHandler;
        if ((dispatcherQueueHandler = <>9__3) == null)
        {
          dispatcherQueueHandler = (<>9__3 = delegate
          {
            this.SyncTaskbarButton(real);
          });
        }
        dispatcherQueue.TryEnqueue(dispatcherQueueHandler);
      };
    }

    // Token: 0x06000402 RID: 1026 RVA: 0x00527964 File Offset: 0x00527964
    private void SyncTaskbarButton(RealStack real)
    {
      TaskbarButton taskbarButton = this.taskbarButton;
      if (taskbarButton == null)
      {
        return;
      }
      taskbarButton.SetShown(real.Core.Settings.Read<bool>((AppSettings s) => s.ShowInTaskbar));
    }

    // Token: 0x1700008C RID: 140
    // (get) Token: 0x06000403 RID: 1027 RVA: 0x005279B0 File Offset: 0x005279B0
    public bool TourHoldsHome
    {
      get
      {
        HomeTour homeTour = this.tour;
        return (homeTour != null && homeTour.IsRunning) || this.InterviewSendOffInProgress;
      }
    }

    // Token: 0x06000404 RID: 1028 RVA: 0x005279D8 File Offset: 0x005279D8
    public bool CanRunFinaleReveal()
    {
      InterviewController interview = this.Interview;
      return (interview != null && (interview.HasBuiltPlan || interview.IsPlanInFlight)) || StarterCast.PresentSlugs().Count > 0;
    }

    // Token: 0x06000405 RID: 1029 RVA: 0x00527A10 File Offset: 0x00527A10
    public bool CanRunDrawnFinale()
    {
      if (!DrawnOnboardingTour.IsFinaleEnabled || this.InterviewSendOffInProgress)
      {
        return false;
      }
      InterviewController interview = this.Interview;
      if (interview == null)
      {
        return false;
      }
      return interview.Transcript.Any<InterviewLine>((InterviewLine l) => l.Role == "user");
    }

    // Token: 0x06000406 RID: 1030 RVA: 0x00527A64 File Offset: 0x00527A64
    public void TourRefusal(string line)
    {
      if (Environment.TickCount64 - this.lastRefusalAt < 6000L)
      {
        return;
      }
      this.lastRefusalAt = Environment.TickCount64;
      this.SayAtCursorAsync(line, true, 2.5, default(CancellationToken));
    }

    // Token: 0x06000407 RID: 1031 RVA: 0x00527AAC File Offset: 0x00527AAC
    public async Task RunDrawnFinaleAsync()
    {
      if (!this.InterviewSendOffInProgress)
      {
        this.InterviewSendOffInProgress = true;
        bool drew = false;
        try
        {
          bool flag = await this.RunDrawnOnboardingFinaleAsync();
          drew = flag;
        }
        catch (Exception ex)
        {
          Log.Error("drawn tour", "finale failed", ex);
        }
        finally
        {
          this.InterviewSendOffInProgress = false;
        }
        this.CompleteTutorialAfterFinaleReveal(drew, false, true);
        this.AfterFinale();
      }
    }

    // Token: 0x06000408 RID: 1032 RVA: 0x00527AF0 File Offset: 0x00527AF0
    public async Task RunFinaleRevealAsync()
    {
      if (!this.TourHoldsHome)
      {
        DrawnOnboardingNotch.DismissPeek(this);
        this.tour = new HomeTour(this, this.Interview);
        this.CursorCompanion.WatchEscape(true);
        bool flag = false;
        try
        {
          flag = await this.tour.RunAsync();
        }
        finally
        {
          this.CursorCompanion.WatchEscape(false);
        }
        this.CompleteTutorialAfterFinaleReveal(true, this.tour.WasAborted, flag && !this.tour.WasAborted);
        this.tour = null;
        this.AfterFinale();
      }
    }

    // Token: 0x06000409 RID: 1033 RVA: 0x00527B34 File Offset: 0x00527B34
    private void AfterFinale()
    {
      HeaderPills.RaiseChanged();
      ArcDock dock = this.Dock;
      if (dock != null)
      {
        dock.Refresh();
      }
      this.SyncRestingMark();
      this.SyncNotchRouting();
      this.lastAgentSignature = null;
      this.SyncAgentActivity();
    }

    // Token: 0x0600040A RID: 1034 RVA: 0x00527B68 File Offset: 0x00527B68
    private void CompleteTutorialAfterFinaleReveal(bool revealRan, bool aborted, bool sendOffLineDue)
    {
      string text = "tour";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 3);
      defaultInterpolatedStringHandler.AppendLiteral("finale over (ran: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(revealRan);
      defaultInterpolatedStringHandler.AppendLiteral(", aborted: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(aborted);
      defaultInterpolatedStringHandler.AppendLiteral(", line due: ");
      defaultInterpolatedStringHandler.AppendFormatted<bool>(sendOffLineDue);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      bool flag = this.newAgentsUiDemoRun;
      this.finaleSuppressesEndLine = revealRan;
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null)
      {
        tutorialWindow.FinishAfterReveal();
      }
      if (!revealRan)
      {
        return;
      }
      this.EndNewAgentsUiDemoRun();
      this.FadeOutOnboardingMusic(3.0);
      if (aborted)
      {
        DrawnOnboardingTour.ReleaseNotch();
        return;
      }
      if (flag)
      {
        DrawnOnboardingTour.ReleaseNotch();
        if (sendOffLineDue)
        {
          this.StreamHoldToTalkLineAfterOnboarding();
        }
        return;
      }
      this.PaywallThenAsync(delegate
      {
        DrawnOnboardingTour.ReleaseNotch();
        if (sendOffLineDue)
        {
          this.StreamHoldToTalkLineAfterOnboarding();
        }
      });
    }

    // Token: 0x0600040B RID: 1035 RVA: 0x00527C5C File Offset: 0x00527C5C
    private async Task PaywallThenAsync(Action then)
    {
      BillingUi billingUi = this.Services.Billing;
      if (billingUi != null && !this.paywallLineWired)
      {
        this.paywallLineWired = true;
        billingUi.PaywallClosed += delegate(PaywallSource source, PaywallCloseKind kind)
        {
          if (source != PaywallSource.OnboardingCompleted || kind == PaywallCloseKind.HandedOff)
          {
            return;
          }
          Action next = this.afterOnboardingPaywall;
          this.afterOnboardingPaywall = null;
          this.DispatcherQueue.TryEnqueue(delegate
          {
            Action next2 = next;
            if (next2 == null)
            {
              return;
            }
            next2();
          });
        };
      }
      this.afterOnboardingPaywall = then;
      bool shown = false;
      try
      {
        bool flag = await this.ShowOnboardingPaywallAsync();
        shown = flag;
      }
      catch (Exception ex)
      {
        Log.Warn("tour", "onboarding paywall: " + ex.Message);
      }
      Log.Info("tour", "onboarding paywall " + (shown ? "shown" : "not shown"));
      if (!shown)
      {
        this.afterOnboardingPaywall = null;
        then();
      }
    }

    // Token: 0x0600040C RID: 1036 RVA: 0x00527CA8 File Offset: 0x00527CA8
    private void StreamHoldToTalkLineAfterOnboarding()
    {
      ClickyTourJobCards.Shared.PopHeldNextQuestions(0.15);
      this.SayAtCursorAsync("(^_^) hold " + this.Services.Settings.PushToTalkDisplay.ToLowerInvariant() + " and ask me something.", true, 25.0, default(CancellationToken));
    }

    // Token: 0x0600040D RID: 1037 RVA: 0x00527D08 File Offset: 0x00527D08
    public void SetTourMusic(bool playing)
    {
      if (playing)
      {
        if (this.music == null)
        {
          this.StartOnboardingMusic(0.12);
          return;
        }
      }
      else
      {
        this.FadeOutOnboardingMusic(0.6);
      }
    }

    // Token: 0x0600040E RID: 1038 RVA: 0x00527D34 File Offset: 0x00527D34
    private void WireTour660Hooks(RealStack real)
    {
      this.WireDrawnTourNotch();
      this.WireTourDrive(real);
      this.WireTourJobs(real);
      FlowHooks.BrowserStateNote = (bool canDrive) => ClickyBrowserBridge.Shared.BrowserStateNote(canDrive);
      real.Companion.TextAnswerHandedToCaptions += delegate([Nullable(2)] object _, EventArgs _)
      {
        NotchPresenter corePresenter = this.CorePresenter;
        if (corePresenter == null)
        {
          return;
        }
        corePresenter.SuppressHoverUntilPointerReentry();
        if (corePresenter.RendersTextResponsesInNotchChat)
        {
          ConversationHistory history = NotchChat.History;
          if (history != null)
          {
            history.NoteAnswerStopped();
          }
        }
        corePresenter.CloseNotchChatSession(NotchChatSessionCloseReason.DrawingCaption);
      };
    }

    // Token: 0x0600040F RID: 1039 RVA: 0x00527D94 File Offset: 0x00527D94
    private void WireDrawnTourNotch()
    {
      DrawnOnboardingNotchShowcase.OpenNotchOnHome = delegate
      {
        NotchPresenter corePresenter = this.CorePresenter;
        if (corePresenter == null)
        {
          return;
        }
        corePresenter.SetTourShowcaseShowing(true);
        corePresenter.Tabs.Select(NotchTab.Home);
        corePresenter.OpenNotchAgentPeekOnRequest();
        corePresenter.SetNotchHeldOpenForPresentation(true);
        NotchExtensions.RaiseShowcaseChanged();
        this.KeepNotchAboveTheDrawing();
      };
      DrawnOnboardingNotchShowcase.FoldNotch = delegate
      {
        NotchPresenter corePresenter2 = this.CorePresenter;
        if (corePresenter2 == null)
        {
          return;
        }
        corePresenter2.SetNotchHeldOpenForPresentation(false);
        corePresenter2.DismissNotchAgentPeek();
        corePresenter2.SetTourShowcaseShowing(false);
        NotchExtensions.RaiseShowcaseChanged();
      };
      DrawnOnboardingNotchShowcase.Shared.Changed += delegate
      {
        this.DispatcherQueue.TryEnqueue(delegate
        {
          NotchExtensions.ShowcaseSampleIntegrationIds = DrawnOnboardingNotchShowcase.Shared.SampleIntegrationIds.ToList<string>();
          NotchExtensions.RaiseShowcaseChanged();
        });
      };
      DrawnOnboardingTour.NotchHoverSuppressionChanged += delegate
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.SyncNotchRouting));
      };
    }

    // Token: 0x06000410 RID: 1040 RVA: 0x00527DEC File Offset: 0x00527DEC
    private void KeepNotchAboveTheDrawing()
    {
      this.<KeepNotchAboveTheDrawing>g__Raise|465_0();
      if (this.notchAboveDrawingTimer == null)
      {
        DispatcherQueueTimer timer = (this.notchAboveDrawingTimer = this.DispatcherQueue.CreateTimer());
        timer.Interval = TimeSpan.FromMilliseconds(200L, 0L);
        timer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          NotchPresenter corePresenter = this.CorePresenter;
          if (corePresenter == null || !corePresenter.IsTourShowcaseShowing)
          {
            timer.Stop();
            return;
          }
          this.<KeepNotchAboveTheDrawing>g__Raise|465_0();
        };
      }
      this.notchAboveDrawingTimer.Start();
    }

    // Token: 0x06000411 RID: 1041 RVA: 0x00527E68 File Offset: 0x00527E68
    private void WireTourDrive(RealStack real)
    {
      AppHost.<>c__DisplayClass466_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass466_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.real = real;
      CS$<>8__locals1.demo = OnboardingDriveDemo.Shared;
      TourDrive.StepContent = delegate(Color accent, FrameworkElement askRow, Func<bool> stepCompleted)
      {
        AppHost.<>c__DisplayClass466_1 CS$<>8__locals2 = new AppHost.<>c__DisplayClass466_1();
        CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
        CS$<>8__locals2.askRow = askRow;
        CS$<>8__locals2.stepCompleted = stepCompleted;
        if (CS$<>8__locals1.demo.State != OnboardingDriveDemo.DemoState.Driving && !CS$<>8__locals2.stepCompleted())
        {
          CS$<>8__locals1.demo.Reset();
        }
        FrameworkElement frameworkElement = CS$<>8__locals1.demo.CreateWindowView(DS.CursorColor(CS$<>8__locals1.<>4__this.Services.Settings.CursorColorHex));
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
        CS$<>8__locals2.status = new Grid
        {
          Height = 14.0
        };
        StackPanel stackPanel = new StackPanel();
        stackPanel.Spacing = 12.0;
        stackPanel.Children.Add(frameworkElement);
        stackPanel.Children.Add(CS$<>8__locals2.askRow);
        stackPanel.Children.Add(CS$<>8__locals2.status);
        stackPanel.Loaded += delegate(object _, RoutedEventArgs _)
        {
          CS$<>8__locals2.CS$<>8__locals1.demo.Changed += base.<WireTourDrive>g__OnChanged|4;
          CS$<>8__locals2.CS$<>8__locals1.real.Companion.StateChanged += base.<WireTourDrive>g__OnState|5;
          base.<WireTourDrive>g__Refresh|3();
        };
        stackPanel.Unloaded += delegate(object _, RoutedEventArgs _)
        {
          CS$<>8__locals2.CS$<>8__locals1.demo.Changed -= base.<WireTourDrive>g__OnChanged|4;
          CS$<>8__locals2.CS$<>8__locals1.real.Companion.StateChanged -= base.<WireTourDrive>g__OnState|5;
        };
        CS$<>8__locals2.<WireTourDrive>g__Refresh|3();
        return stackPanel;
      };
      TourDrive.Run = delegate
      {
        TutorialWindow tutorialWindow = CS$<>8__locals1.<>4__this.tutorial;
        IntPtr intPtr = ((tutorialWindow != null) ? tutorialWindow.Window.Hwnd : ((IntPtr)0));
        OnboardingDriveDemo demo = CS$<>8__locals1.demo;
        OnboardingDriveContext onboardingDriveContext = new OnboardingDriveContext();
        onboardingDriveContext.Display = ((intPtr != 0) ? Displays.ForWindow(intPtr) : Displays.Primary());
        onboardingDriveContext.Accent = DS.CursorColor(CS$<>8__locals1.<>4__this.Services.Settings.CursorColorHex);
        Func<string, Task> func;
        if ((func = CS$<>8__locals1.<>9__8) == null)
        {
          func = (CS$<>8__locals1.<>9__8 = (string line) => CS$<>8__locals1.real.Companion.SpeakVerbatimLineAsync(line, default(CancellationToken)));
        }
        onboardingDriveContext.SpeakLine = func;
        Func<int> func2;
        if ((func2 = CS$<>8__locals1.<>9__9) == null)
        {
          func2 = (CS$<>8__locals1.<>9__9 = () => CS$<>8__locals1.real.Companion.AudioPlaybackBeganCount);
        }
        onboardingDriveContext.AudioPlaybackBeganCount = func2;
        Func<bool> func3;
        if ((func3 = CS$<>8__locals1.<>9__10) == null)
        {
          func3 = (CS$<>8__locals1.<>9__10 = () => CS$<>8__locals1.real.Companion.State == VoiceState.Responding);
        }
        onboardingDriveContext.IsResponding = func3;
        onboardingDriveContext.FirstName = CS$<>8__locals1.<>4__this.AccountFirstNameForTour;
        onboardingDriveContext.StepCompleted = delegate
        {
          Action completed = TourDrive.Completed;
          if (completed == null)
          {
            return;
          }
          completed();
        };
        Action<string, double> action;
        if ((action = AppHost.<>O.<11>__Play) == null)
        {
          action = (AppHost.<>O.<11>__Play = new Action<string, double>(Sounds.Play));
        }
        onboardingDriveContext.PlayChime = action;
        onboardingDriveContext.PlayDrawingSound = new Action<string, float>(CS$<>8__locals1.real.Canvas.PlayOneShot);
        demo.Run(onboardingDriveContext);
      };
      TourDrive.IsDriving = () => CS$<>8__locals1.demo.State == OnboardingDriveDemo.DemoState.Driving;
      TourDrive.Cancel = new Action(CS$<>8__locals1.demo.Cancel);
    }

    // Token: 0x06000412 RID: 1042 RVA: 0x00527EDC File Offset: 0x00527EDC
    private void WireTourJobs(RealStack real)
    {
      AgentService agents = real.Agents;
      ClickyTourJobCards.CreateWaitingJob = delegate(string title, string pitch, Guid cardId)
      {
        try
        {
          agents.Jobs.CreateWaitingJob(title, pitch, cardId, agents.ProjectsRoot);
        }
        catch (Exception ex)
        {
          Log.Warn("drawn tour", "waiting job for \"" + title + "\": " + ex.Message);
        }
      };
      ProactiveHooks.LaunchApprovedCardAsJob = delegate(ProactiveJobLaunch launch)
      {
        ProactiveSuggestionCard card = launch.Card;
        ClickyJob clickyJob = agents.Jobs.WaitingJob(card.Id);
        ClickyJob clickyJob2 = ((clickyJob != null) ? agents.Jobs.BeginWaitingJob(clickyJob.Id) : null);
        string result;
        try
        {
          AgentService agents3 = agents;
          AgentLaunchRequest agentLaunchRequest = new AgentLaunchRequest();
          agentLaunchRequest.Prompt = launch.Transcript;
          agentLaunchRequest.JobId = ((clickyJob2 != null) ? clickyJob2.Id : null);
          agentLaunchRequest.NewClickyName = card.Title;
          agentLaunchRequest.LaunchContext = launch.LaunchContext;
          agentLaunchRequest.LaunchSource = "suggestion";
          agentLaunchRequest.UsageContext = "companion";
          agentLaunchRequest.IsGeneratedTaskBrief = true;
          int? serverSuggestionId = card.ServerSuggestionId;
          agentLaunchRequest.ProactiveSuggestionId = ((serverSuggestionId != null) ? new long?((long)serverSuggestionId.GetValueOrDefault()) : null);
          result = agents3.LaunchAsync(agentLaunchRequest, default(CancellationToken)).GetAwaiter().GetResult();
        }
        catch (Exception ex2)
        {
          Log.Warn("proactive", "\"" + card.Title + "\" as a job: " + ex2.Message);
          if (clickyJob2 != null)
          {
            agents.Jobs.Dismiss(clickyJob2.Id);
          }
          return false;
        }
        AgentService agents2 = agents;
        string text = "started";
        string text2 = null;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["from"] = "suggestion";
        agents2.TrackJobAction(text, text2, dictionary);
        try
        {
          Action<AgentToolLaunch, string> launched = AgentToolHost.Launched;
          if (launched != null)
          {
            launched(new AgentToolLaunch(launch.Transcript, null, card.Title, "Helper", null, null, "voice", null, null, "companion", true, null, null), result);
          }
        }
        catch (Exception ex3)
        {
          Log.Warn("proactive", "hand-off note: " + ex3.Message);
        }
        ProactiveRoutine routine = card.Routine;
        if (routine != null)
        {
          string text3;
          if ((text3 = ((clickyJob2 != null) ? clickyJob2.Id : null)) == null)
          {
            AgentTaskDetail taskDetail = agents.GetTaskDetail(result);
            text3 = ((taskDetail != null) ? taskDetail.JobId : null);
          }
          string text4 = text3;
          if (text4 != null)
          {
            agents.Routines.RegisterApprovedSuggestion("", string.IsNullOrWhiteSpace(routine.Title) ? card.Title : routine.Title, card.AgentPrompt, routine.ClampedIntervalMinutes, result, text4);
          }
        }
        return true;
      };
    }

    // Token: 0x06000413 RID: 1043 RVA: 0x00527F14 File Offset: 0x00527F14
    private void WireTrayTooltip()
    {
      if (this.trayTooltipWired)
      {
        return;
      }
      this.trayTooltipWired = true;
      this.Services.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.UpdateTrayTooltip));
      };
      this.Services.Settings.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.UpdateTrayTooltip));
      };
      this.Services.Agents.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.UpdateTrayTooltip));
      };
      this.UpdateTrayTooltip();
    }

    // Token: 0x06000414 RID: 1044 RVA: 0x00527F8C File Offset: 0x00527F8C
    private void UpdateTrayTooltip()
    {
      if (this.tray == null)
      {
        return;
      }
      try
      {
        List<string> list = (from t in this.Services.Agents.Tasks.Where<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
          {
            AgentTaskStatus status = t.Status;
            return status <= AgentTaskStatus.Running;
          })
          select t.Title).ToList<string>();
        this.tray.SetTooltip(TrayTooltip.Text(this.Services.Auth.IsSignedIn, this.Services.Settings.IsPaused, list));
      }
      catch (Exception ex)
      {
        Log.Warn("tray", "tooltip: " + ex.Message);
      }
    }

    // Token: 0x1700008D RID: 141
    // (get) Token: 0x06000415 RID: 1045 RVA: 0x00528064 File Offset: 0x00528064
    private bool ExcludeFromCapture
    {
      get
      {
        return !this.Options.AllowCapture;
      }
    }

    // Token: 0x06000416 RID: 1046 RVA: 0x00528074 File Offset: 0x00528074
    private void StartAuxiliary()
    {
      this.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, delegate
      {
        this.tray = new TrayIcon("HeyClicky", AppHost.TrayColor(this.Services.Settings.CursorColorHex));
        this.tray.OpenSettings += delegate
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.OpenSettings));
        };
        this.tray.Clicked += delegate
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.HandleTrayClick));
        };
        this.tray.ReportBug += delegate
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            BugReport.Run(new SettingsHost(this.Services, this));
          });
        };
        this.tray.RequestFeature += delegate
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            ClickyAnalytics.TrackFeatureRequestOpened();
            this.OpenUrl(SettingsHost.Links.FeatureRequest ?? "");
          });
        };
        this.tray.WhatsNew += delegate
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            ClickyAnalytics.TrackChangelogOpened("tray_menu");
            this.OpenUrl(SettingsHost.Links.Changelog ?? "");
          });
        };
        this.tray.CheckForUpdates += delegate
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.CheckForUpdatesFromMenu("tray_menu");
          });
        };
        this.tray.Quit += delegate
        {
          this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.Quit));
        };
        this.tray.PauseToggled += delegate(bool p)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            this.SetPaused(p);
          });
        };
        this.tray.IsPaused = () => this.Services.Settings.IsPaused;
        this.WireTrayTooltip();
      });
      this.Services.Permissions.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.AutoAdvancePermissions));
      };
    }

    // Token: 0x06000417 RID: 1047 RVA: 0x005280AC File Offset: 0x005280AC
    private void DisposeAuxiliary()
    {
      TrayIcon trayIcon = this.tray;
      if (trayIcon != null)
      {
        trayIcon.Dispose();
      }
      this.tray = null;
    }

    // Token: 0x06000418 RID: 1048 RVA: 0x005280C8 File Offset: 0x005280C8
    private void StartFirstRunFlow()
    {
      RealStack real = this.Real;
      if (real != null)
      {
        OnboardingProgress onboardingProgress = real.Core.Settings.Read<OnboardingProgress>(([Nullable(1)] AppSettings s) => s.Onboarding);
        this.PreloadOnboardingVideoIfNeeded();
        if (this.Options.ShowIntro || PreSignInIntroViewModel.ShouldPresent(onboardingProgress, real.Core.Auth.IsSignedIn))
        {
          this.TrackNotchFirstShown("pre_sign_in_intro");
          this.ShowIntro();
          return;
        }
        this.TrackNotchFirstShown((!onboardingProgress.HasCompletedOnboarding || !this.AllPermissionsGranted) ? "onboarding" : "post_onboarding");
        if (this.Options.ShowTutorial)
        {
          this.StartTutorial();
          return;
        }
        this.SyncOnboardingPin();
        return;
      }
      else
      {
        if (this.Options.ShowIntro || !this.Services.Settings.HasCompletedIntro)
        {
          this.TrackNotchFirstShown("pre_sign_in_intro");
          this.ShowIntro();
          return;
        }
        this.TrackNotchFirstShown((!this.Services.Settings.HasCompletedOnboarding || !this.AllPermissionsGranted) ? "onboarding" : "post_onboarding");
        if (this.Options.ShowTutorial)
        {
          this.StartTutorial();
          return;
        }
        if (!this.Services.Settings.HasCompletedOnboarding)
        {
          this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(0));
        }
        return;
      }
    }

    // Token: 0x06000419 RID: 1049 RVA: 0x00528228 File Offset: 0x00528228
    private void TrackNotchFirstShown(string entryContext)
    {
      if (this.notchFirstShownTracked)
      {
        return;
      }
      this.notchFirstShownTracked = true;
      ClickyAnalytics.TrackNotchFirstShown(entryContext);
    }

    // Token: 0x1700008E RID: 142
    // (get) Token: 0x0600041A RID: 1050 RVA: 0x00528240 File Offset: 0x00528240
    private bool AllPermissionsGranted
    {
      get
      {
        return this.Services.Permissions.GetStatus(PermissionKind.Microphone) == PermissionStatus.Granted && this.Services.Permissions.GetStatus(PermissionKind.ScreenCapture) == PermissionStatus.Granted;
      }
    }

    // Token: 0x0600041B RID: 1051 RVA: 0x0052826C File Offset: 0x0052826C
    public void StartTutorial()
    {
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null && tutorialWindow.IsRunning)
      {
        tutorialWindow.BringForward();
        return;
      }
      this.EndNewAgentsUiDemoRun();
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.AgentPeek));
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.CollapseHoverAfterOutsideClick();
      }
      if (this.tutorial == null)
      {
        this.tutorial = this.CreateTutorial();
      }
      this.tutorial.Show(TutorialWindow.Step.MicCheck, false);
    }

    // Token: 0x0600041C RID: 1052 RVA: 0x005282E8 File Offset: 0x005282E8
    private TutorialWindow CreateTutorial()
    {
      if (this.Interview == null)
      {
        this.Interview = new InterviewController(this.Services, this.Real);
      }
      this.Interview.PlanLanded += delegate(bool _)
      {
        this.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.PrefetchDrawnOnboardingFinaleIfReady));
      };
      TutorialWindow tutorialWindow = new TutorialWindow(this.Services, this, this.ExcludeFromCapture);
      RealStack real = this.Real;
      tutorialWindow.Vm = ((real != null) ? real.Tutorial : null);
      tutorialWindow.Interview = this.Interview;
      tutorialWindow.StepChanged = new Action<TutorialWindow.Step?>(this.OnTourStepChanged);
      tutorialWindow.StopVoiceAndMarks = delegate
      {
        this.Services.Companion.Cancel();
        RealStack real4 = this.Real;
        if (real4 != null)
        {
          real4.Highlight.Clear();
        }
        RealStack real5 = this.Real;
        if (real5 != null)
        {
          real5.Canvas.Clear(true);
        }
      };
      tutorialWindow.DrawingShown = delegate
      {
        RealStack real6 = this.Real;
        return real6 != null && real6.Canvas.IsShowingDrawing;
      };
      tutorialWindow.CanReveal = new Func<bool>(this.CanRunFinaleReveal);
      tutorialWindow.RunReveal = new Func<Task>(this.RunFinaleRevealAsync);
      tutorialWindow.Music = new Action<bool>(this.SetTourMusic);
      tutorialWindow.CanDrawFinale = new Func<bool>(this.CanRunDrawnFinale);
      tutorialWindow.RunDrawnFinale = new Func<Task>(this.RunDrawnFinaleAsync);
      RealStack real2 = this.Real;
      tutorialWindow.Canvas = ((real2 != null) ? real2.Canvas : null);
      tutorialWindow.MicrophoneName = delegate
      {
        RealStack real7 = this.Real;
        if (real7 == null)
        {
          return null;
        }
        return real7.Core.Settings.Read<string>(([Nullable(1)] AppSettings x) => x.MicrophoneDeviceName);
      };
      tutorialWindow.MicrophonePicked = delegate([Nullable(2)] string id, string label)
      {
        RealStack real8 = this.Real;
        if (real8 == null)
        {
          return;
        }
        real8.Core.Settings.Update(delegate(AppSettings x)
        {
          x.MicrophoneDeviceId = id;
          x.MicrophoneDeviceName = ((id == null) ? null : label);
        });
        string text = "tutorial";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
        defaultInterpolatedStringHandler.AppendLiteral("microphone: ");
        defaultInterpolatedStringHandler.AppendFormatted(label);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(id ?? "system default");
        defaultInterpolatedStringHandler.AppendLiteral(")");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      };
      tutorialWindow.Ended += delegate
      {
        this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.OnboardingTutorialResume));
        this.Services.Presenter.Present(new NotchPresentation.Idle());
        if (!this.TourHoldsHome)
        {
          this.FadeOutOnboardingMusic(3.0);
        }
      };
      RealStack real3 = this.Real;
      if (real3 != null && !this.tutorialVmWired)
      {
        this.tutorialVmWired = true;
        real3.Companion.TurnFinished += delegate([Nullable(2)] object _, CompanionTurnFinish finish)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            TutorialWindow tutorialWindow2 = this.tutorial;
            if (tutorialWindow2 == null)
            {
              return;
            }
            tutorialWindow2.NoteCompanionTurnFinished(finish);
          });
        };
        real3.Tutorial.StepCompletedChime += delegate([Nullable(2)] object _, EventArgs _)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            Sounds.Play("clicky-text-receive", 0.3);
          });
        };
        real3.Tutorial.SuccessLine += delegate([Nullable(2)] object _, string line)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            _ = this.SayAtCursorAsync(line, true, 2.5, default(CancellationToken));
          });
        };
        real3.Tutorial.Ended += delegate(object _, [TupleElementNames(new string[] { "Reason", "CursorLine" })] [Nullable(new byte[] { 0, 1, 1 })] ValueTuple<string, string> e)
        {
          this.DispatcherQueue.TryEnqueue(delegate
          {
            if (this.finaleSuppressesEndLine)
            {
              this.finaleSuppressesEndLine = false;
              return;
            }
            if (e.Item1 == "finished")
            {
              Sounds.Play("agent-done", 0.4);
            }
            string line = e.Item2.Replace("ctrl + alt", this.Services.Settings.PushToTalkDisplay.ToLowerInvariant());
            bool flag = this.newAgentsUiDemoRun;
            this.EndNewAgentsUiDemoRun();
            if (flag)
            {
              _ = this.SayAtCursorAsync(line, true, 5.0, default(CancellationToken));
              return;
            }
            _ = this.PaywallThenAsync(delegate
            {
              _ = this.SayAtCursorAsync(line, true, 5.0, default(CancellationToken));
            });
          });
        };
      }
      return tutorialWindow;
    }

    // Token: 0x1700008F RID: 143
    // (get) Token: 0x0600041D RID: 1053 RVA: 0x005284B8 File Offset: 0x005284B8
    // (set) Token: 0x0600041E RID: 1054 RVA: 0x005284C0 File Offset: 0x005284C0
    [Nullable(2)]
    public InterviewController Interview
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x0600041F RID: 1055 RVA: 0x005284CC File Offset: 0x005284CC
    private void NarrateVerbatim(IReadOnlyList<string> lines)
    {
      AppHost.<>c__DisplayClass492_0 CS$<>8__locals1 = new AppHost.<>c__DisplayClass492_0();
      CS$<>8__locals1.lines = lines;
      CS$<>8__locals1.real = this.Real;
      if (CS$<>8__locals1.real == null)
      {
        return;
      }
      Task.Run(delegate
      {
        AppHost.<>c__DisplayClass492_0.<<NarrateVerbatim>b__0>d <<NarrateVerbatim>b__0>d;
        <<NarrateVerbatim>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<NarrateVerbatim>b__0>d.<>4__this = CS$<>8__locals1;
        <<NarrateVerbatim>b__0>d.<>1__state = -1;
        <<NarrateVerbatim>b__0>d.<>t__builder.Start<AppHost.<>c__DisplayClass492_0.<<NarrateVerbatim>b__0>d>(ref <<NarrateVerbatim>b__0>d);
        return <<NarrateVerbatim>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x17000090 RID: 144
    // (get) Token: 0x06000420 RID: 1056 RVA: 0x00528510 File Offset: 0x00528510
    public bool IsPreSignInIntroShowing
    {
      get
      {
        IntroWindow introWindow = this.intro;
        return introWindow != null && introWindow.IsShowing;
      }
    }

    // Token: 0x06000421 RID: 1057 RVA: 0x00528524 File Offset: 0x00528524
    public void ShowIntro()
    {
      if (this.intro == null)
      {
        this.intro = this.CreateIntro();
      }
      this.intro.Show(IntroWindow.Step.WhatIsClicky);
    }

    // Token: 0x06000422 RID: 1058 RVA: 0x00528548 File Offset: 0x00528548
    private IntroWindow CreateIntro()
    {
      IntroWindow i = new IntroWindow(this.Services, this, this.ExcludeFromCapture)
      {
        Vm = this.IntroVm
      };
      i.FlightRequested += delegate(IntroWindow.Point2 p)
      {
        this.Fly(p.X, p.Y);
      };
      i.ReducedMotionHandOff += delegate
      {
        Sounds.Play("clicky-surprised", 0.95);
        i.HandOffComplete();
        this.ActivateIntroHomeWelcome();
      };
      return i;
    }

    // Token: 0x06000423 RID: 1059 RVA: 0x005285BC File Offset: 0x005285BC
    private void Fly(double x, double y)
    {
      if (this.flight == null)
      {
        this.flight = new HatchFlightOverlay(this.ExcludeFromCapture);
      }
      Color color = DS.CursorColor(this.Services.Settings.CursorColorHex);
      this.flight.NotchShouldExpand += this.<Fly>g__OnExpand|497_0;
      this.flight.Landed += this.<Fly>g__OnLanded|497_1;
      this.flight.PeekFinishing += this.<Fly>g__OnPeekFinishing|497_2;
      this.flight.Fly(x, y, color);
    }

    // Token: 0x06000424 RID: 1060 RVA: 0x0052864C File Offset: 0x0052864C
    private void ActivateIntroHomeWelcome()
    {
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter != null)
      {
        corePresenter.SetIntroHomeWelcomeAutoShownPin(true);
      }
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
      if (notchOnboardingCardViewModel != null)
      {
        notchOnboardingCardViewModel.IsIntroHomeWelcomeActive = true;
        this.SyncOnboardingPin();
      }
      else
      {
        if (!this.fakeIntroWelcomeActive)
        {
          ClickyAnalytics.TrackPreSignInIntroNotchWelcomeShown();
        }
        this.fakeIntroWelcomeActive = true;
        NotchPresenter corePresenter2 = this.CorePresenter;
        if (corePresenter2 != null)
        {
          corePresenter2.SetIntroHomeWelcomeActive(true);
        }
        this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(0));
      }
      Log.Info("intro", "notch welcome pinned (auto-shown until one hover in and out)");
    }

    // Token: 0x06000425 RID: 1061 RVA: 0x005286D0 File Offset: 0x005286D0
    public void OnboardingCardAction(int step)
    {
      NotchOnboardingCardViewModel notchOnboardingCardViewModel = this.CardVm;
      if (notchOnboardingCardViewModel != null)
      {
        notchOnboardingCardViewModel.PrimaryActionAsync();
        return;
      }
      if (step != 0)
      {
        if (step != 3)
        {
          return;
        }
        ClickyAnalytics.TrackOnboardingStarted();
        this.Services.Settings.HasCompletedOnboarding = true;
        this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.OnboardingTutorialInvite(), null);
        ClickyAnalytics.TrackOnboardingTutorialInvitePresented();
        return;
      }
      else
      {
        if (this.fakeIntroWelcomeActive)
        {
          this.fakeIntroWelcomeActive = false;
          NotchPresenter corePresenter = this.CorePresenter;
          if (corePresenter != null)
          {
            corePresenter.SetIntroHomeWelcomeActive(false);
          }
          ClickyAnalytics.TrackPreSignInIntroNotchFinalSetupTapped();
          if (!this.fakeIntroDismissedTracked)
          {
            ClickyAnalytics.TrackPreSignInIntroDismissed();
          }
          this.fakeIntroDismissedTracked = true;
        }
        int num = this.NextPermissionStep(1);
        if (num == 3)
        {
          this.OnboardingCardAction(num);
          return;
        }
        this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(num));
        return;
      }
    }

    // Token: 0x06000426 RID: 1062 RVA: 0x00528798 File Offset: 0x00528798
    private int NextPermissionStep(int from)
    {
      if (from <= 1 && this.Services.Permissions.GetStatus(PermissionKind.Microphone) != PermissionStatus.Granted)
      {
        return 1;
      }
      if (from <= 2 && this.Services.Permissions.GetStatus(PermissionKind.ScreenCapture) != PermissionStatus.Granted)
      {
        return 2;
      }
      return 3;
    }

    // Token: 0x06000427 RID: 1063 RVA: 0x005287D0 File Offset: 0x005287D0
    private void AutoAdvancePermissions()
    {
      if (this.Real != null)
      {
        return;
      }
      NotchPresentation.OnboardingPinned onboardingPinned = this.Services.Presenter.Presentation as NotchPresentation.OnboardingPinned;
      if (onboardingPinned == null)
      {
        return;
      }
      int st = onboardingPinned.Step;
      PermissionKind? permissionKind = ((st == 1) ? new PermissionKind?(PermissionKind.Microphone) : ((st == 2) ? new PermissionKind?(PermissionKind.ScreenCapture) : null));
      if (permissionKind == null || this.Services.Permissions.GetStatus(permissionKind.Value) != PermissionStatus.Granted)
      {
        return;
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(1.2);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        NotchPresentation.OnboardingPinned onboardingPinned2 = this.Services.Presenter.Presentation as NotchPresentation.OnboardingPinned;
        if (onboardingPinned2 != null)
        {
          int step = onboardingPinned2.Step;
          if (step == st)
          {
            this.Services.Presenter.Present(new NotchPresentation.OnboardingPinned(this.NextPermissionStep(st + 1)));
          }
        }
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x06000428 RID: 1064 RVA: 0x005288B0 File Offset: 0x005288B0
    public IntroWindow DemoIntro(IntroWindow.Step step)
    {
      if (this.intro == null)
      {
        this.intro = this.CreateIntro();
      }
      this.intro.Show(step);
      return this.intro;
    }

    // Token: 0x06000429 RID: 1065 RVA: 0x005288D8 File Offset: 0x005288D8
    public TutorialWindow DemoTutorial(TutorialWindow.Step step)
    {
      if (this.tutorial == null)
      {
        this.tutorial = this.CreateTutorial();
      }
      this.tutorial.Show(step, false);
      return this.tutorial;
    }

    // Token: 0x0600042A RID: 1066 RVA: 0x00528904 File Offset: 0x00528904
    public SettingsWindow DemoSettings(SettingsWindow.Page page)
    {
      if (this.settings == null)
      {
        this.settings = new SettingsWindow(this.Services, this, this.ExcludeFromCapture);
      }
      this.settings.Show(new SettingsWindow.Page?(page));
      return this.settings;
    }

    // Token: 0x0600042B RID: 1067 RVA: 0x00528940 File Offset: 0x00528940
    [NullableContext(2)]
    public UIElement DemoIntroRoot()
    {
      IntroWindow introWindow = this.intro;
      if (introWindow == null)
      {
        return null;
      }
      return introWindow.Window.Root;
    }

    // Token: 0x0600042C RID: 1068 RVA: 0x00528958 File Offset: 0x00528958
    [NullableContext(2)]
    public UIElement DemoTutorialRoot()
    {
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow == null)
      {
        return null;
      }
      return tutorialWindow.Window.Root;
    }

    // Token: 0x0600042D RID: 1069 RVA: 0x00528970 File Offset: 0x00528970
    [NullableContext(2)]
    public TutorialWindow DemoTutorialWindow()
    {
      return this.tutorial;
    }

    // Token: 0x0600042E RID: 1070 RVA: 0x00528978 File Offset: 0x00528978
    [NullableContext(2)]
    public UIElement DemoSettingsRoot()
    {
      SettingsWindow settingsWindow = this.settings;
      if (settingsWindow == null)
      {
        return null;
      }
      return settingsWindow.Window.Root;
    }

    // Token: 0x0600042F RID: 1071 RVA: 0x00528990 File Offset: 0x00528990
    public void HideAuxWindows()
    {
      IntroWindow introWindow = this.intro;
      if (introWindow != null)
      {
        introWindow.Hide();
      }
      TutorialWindow tutorialWindow = this.tutorial;
      if (tutorialWindow != null)
      {
        tutorialWindow.Hide();
      }
      SettingsWindow settingsWindow = this.settings;
      if (settingsWindow != null)
      {
        settingsWindow.Window.HideWindow();
      }
      NotchPresenter corePresenter = this.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      corePresenter.DismissNotchAgentPeek();
    }

    // Token: 0x06000433 RID: 1075 RVA: 0x00528A4C File Offset: 0x00528A4C
    [CompilerGenerated]
    internal static string <SyncAgentActivity>g__Clamp|11_4(string s)
    {
      s = s.Trim();
      if (s.Length > 25)
      {
        return s.Substring(0, 24) + "…";
      }
      return s;
    }

    // Token: 0x06000439 RID: 1081 RVA: 0x00528BC8 File Offset: 0x00528BC8
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <OnDrawingAction>g__Str|119_0(JsonElement e, string name)
    {
      JsonElement jsonElement;
      if (e.ValueKind != JsonValueKind.Object || !e.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return jsonElement.GetString();
    }

    // Token: 0x0600043B RID: 1083 RVA: 0x00528D14 File Offset: 0x00528D14
    [CompilerGenerated]
    internal static void <Quit>g__Step|130_0(string what, Action stop)
    {
      Log.Info("shutdown", what);
      try
      {
        stop();
      }
      catch (Exception ex)
      {
        Log.Warn("shutdown", what + ": " + ex.Message);
      }
    }

    // Token: 0x0600043C RID: 1084 RVA: 0x00528D64 File Offset: 0x00528D64
    [NullableContext(2)]
    [CompilerGenerated]
    internal static string <StartDock>g__Text|166_67(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000453 RID: 1107 RVA: 0x005291E8 File Offset: 0x005291E8
    [CompilerGenerated]
    internal static bool <ApplySettings>g__Changed|395_0<T>([Nullable(new byte[] { 1, 1, 0 })] Func<AppSettings, T> read, ref AppHost.<>c__DisplayClass395_0 A_1)
    {
      return A_1.initial || A_1.before == null || !EqualityComparer<T>.Default.Equals(read(A_1.before), read(A_1.now));
    }

    // Token: 0x0600045C RID: 1116 RVA: 0x005293F0 File Offset: 0x005293F0
    [CompilerGenerated]
    private void <KeepNotchAboveTheDrawing>g__Raise|465_0()
    {
      Native.SetWindowPos(this.NotchUi.Window.Hwnd, FullscreenGuard.TopmostInsertAfter(), 0, 0, 0, 0, 19U);
      ArcDock dock = this.Dock;
      if (dock == null)
      {
        return;
      }
      dock.KeepAboveNotch();
    }

    // Token: 0x06000479 RID: 1145 RVA: 0x005299C4 File Offset: 0x005299C4
    [CompilerGenerated]
    private void <Fly>g__OnExpand|497_0()
    {
      this.flight.NotchShouldExpand -= this.<Fly>g__OnExpand|497_0;
      this.Services.Presenter.ShowLiveEvent(new NotchLiveEvent.IntroHatchPeek(), null);
    }

    // Token: 0x0600047A RID: 1146 RVA: 0x00529A08 File Offset: 0x00529A08
    [CompilerGenerated]
    private void <Fly>g__OnLanded|497_1()
    {
      this.flight.Landed -= this.<Fly>g__OnLanded|497_1;
      Sounds.Play("clicky-surprised", 0.95);
      DispatcherQueueTimer dispatcherQueueTimer = this.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.75);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        IntroWindow introWindow = this.intro;
        if (introWindow == null)
        {
          return;
        }
        introWindow.HandOffComplete();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x0600047C RID: 1148 RVA: 0x00529A98 File Offset: 0x00529A98
    [CompilerGenerated]
    private void <Fly>g__OnPeekFinishing|497_2()
    {
      this.flight.PeekFinishing -= this.<Fly>g__OnPeekFinishing|497_2;
      this.Services.Presenter.DismissLiveEvent(typeof(NotchLiveEvent.IntroHatchPeek));
      this.ActivateIntroHomeWelcome();
    }

    // Token: 0x04000072 RID: 114
    private int agentCostLaneReadInFlight;

    // Token: 0x04000073 RID: 115
    [Nullable(2)]
    private DispatcherQueueTimer ambientTimer;

    // Token: 0x04000074 RID: 116
    [Nullable(2)]
    private string lastAgentSignature;

    // Token: 0x04000075 RID: 117
    private bool lastFullscreenOnNotch;

    // Token: 0x04000076 RID: 118
    private readonly HashSet<string> autoOpenedTurns = new HashSet<string>();

    // Token: 0x04000077 RID: 119
    [TupleElementNames(new string[] { "TaskId", "Artifact", "Workspace" })]
    [Nullable(new byte[] { 1, 0, 1, 1, 2 })]
    private readonly List<ValueTuple<string, string, string>> heldArtifactOpens = new List<ValueTuple<string, string, string>>();

    // Token: 0x04000078 RID: 120
    [Nullable(2)]
    private DispatcherQueueTimer heldArtifactDrain;

    // Token: 0x04000079 RID: 121
    [Nullable(2)]
    private BillingUi billing;

    // Token: 0x0400007A RID: 122
    private readonly UploadApprovalCard uploadApproval = new UploadApprovalCard();

    // Token: 0x0400007E RID: 126
    private bool openBrowserSettingsWhenReady;

    // Token: 0x0400007F RID: 127
    private bool paywallIntroVideoPlaying;

    // Token: 0x04000080 RID: 128
    private bool newAgentsUiDemoRun;

    // Token: 0x04000086 RID: 134
    [Nullable(2)]
    private CompanionNotchBridge bridge;

    // Token: 0x04000087 RID: 135
    [Nullable(2)]
    private HotkeyRouter hotkeys;

    // Token: 0x04000088 RID: 136
    private bool quitting;

    // Token: 0x04000089 RID: 137
    private bool cursorShownForOnboarding;

    // Token: 0x0400008A RID: 138
    [Nullable(2)]
    private OneBrainStatsPanel oneBrainStatsPanel;

    // Token: 0x0400008B RID: 139
    private bool realServicesStarted;

    // Token: 0x0400008C RID: 140
    private readonly List<string[]> pendingLaunchArgs = new List<string[]>();

    // Token: 0x0400008D RID: 141
    private long lastLayoutCycleRecoveryAt = -4611686018427387904L;

    // Token: 0x0400008E RID: 142
    private bool cursorDockFlightInProgress;

    // Token: 0x0400008F RID: 143
    [Nullable(2)]
    private CancellationTokenSource pendingFinishCue;

    // Token: 0x04000090 RID: 144
    private bool lastTurnTyped;

    // Token: 0x04000092 RID: 146
    [Nullable(2)]
    private DockLaunchFlight launchFlight;

    // Token: 0x04000093 RID: 147
    private bool notchWindowHovered;

    // Token: 0x04000094 RID: 148
    private bool deckPointerOnNotch;

    // Token: 0x04000095 RID: 149
    private readonly HashSet<string> dockKnownTasks = new HashSet<string>();

    // Token: 0x04000096 RID: 150
    private readonly Dictionary<string, AgentTaskStatus> dockLastStatus = new Dictionary<string, AgentTaskStatus>();

    // Token: 0x04000097 RID: 151
    private bool dockFirstSync = true;

    // Token: 0x04000098 RID: 152
    [Nullable(new byte[] { 2, 1 })]
    private Action<string> AnnounceComputerUseAsk;

    // Token: 0x04000099 RID: 153
    [TupleElementNames(new string[] { "TaskId", "Slot" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    private readonly List<ValueTuple<string, int>> launchPaletteSlots = new List<ValueTuple<string, int>>();

    // Token: 0x0400009B RID: 155
    [Nullable(2)]
    private GoodMorningBrief morningBrief;

    // Token: 0x0400009E RID: 158
    [Nullable(2)]
    private ClickyInkNoteConnect inkConnect;

    // Token: 0x040000A0 RID: 160
    [Nullable(2)]
    private CursorCompanion cursorCompanion;

    // Token: 0x040000A1 RID: 161
    [Nullable(2)]
    private MediaPlayer music;

    // Token: 0x040000A2 RID: 162
    private bool introSequenceActive;

    // Token: 0x040000A3 RID: 163
    private static readonly string ResearchBuddyPrompt = "I am a CEO of SpaceX, the rocket company. Do a simple competitor reserach for this rocket company, save it as a PDF inside your own projects folder (the cwd you were started in; do NOT save to Downloads, Desktop, Documents, or anywhere else). Do NOT open the file yourself, just list it in your ARTIFACTS block and HeyClicky will open the finished file automatically once you're done. Don't overcomplicate this, max 2 pages. Include a picture of a newest NASA rocket. Don't spend more than 1 minute on this. ";

    // Token: 0x040000A4 RID: 164
    private static readonly string ShippyPrompt = "agent, create a very simple but fun looking personalized welcome website on this computer: one plain .html file (CSS baked in, no frameworks, no javascript, no server). It must be a SINGLE non-scrolling screen: everything fits in one 100vh viewport, body overflow hidden, no scrollbars. Top/center: a big friendly hero that says `hello {name}`; learn the name of the user from the context you know (their HeyClicky signup name, maybe name of the user on this PC), and you can add one short personal line under it if you know anything about me. Along the BOTTOM: a small friendly section heading like `you can use HeyClicky with any app`, then one compact horizontal row of 4-5 apps ACTUALLY installed on this PC (check the Start menu shortcuts under %ProgramData%\\Microsoft\\Windows\\Start Menu\\Programs and %AppData%\\Microsoft\\Windows\\Start Menu\\Programs), each as a small card: the app's REAL icon, the app name, and 2-3 punchy bullet points (max ~7 words each) on using HeyClicky with THAT app. Write bullets ONLY from HeyClicky's real capabilities, matched to what the app is for: hold ctrl+alt and talk about anything on screen; circle something while asking and HeyClicky sees exactly what you marked; it points at buttons and walks you through apps step by step; or hand it a one-command background task (agents research the web, write files/PDFs/spreadsheets, build and preview websites, do git and GitHub work, and use connected apps like Gmail, Calendar, Notion, and Linear). Never promise scheduled automation, always-on monitoring, or image/video generation. REAL icons only, never emoji or drawn placeholders: extract each icon from the app's .exe or .lnk target with PowerShell (System.Drawing.Icon ExtractAssociatedIcon, saved as a 128px png), save the pngs NEXT to the .html, and reference them with relative paths. Save the .html inside your own projects folder (the cwd you were started in; do NOT save to Downloads, Desktop, Documents, or anywhere else). Do NOT open the file yourself, just list the plain .html file (file://...) in your ARTIFACTS block and HeyClicky will open it automatically once you're done. Do NOT spin up a local server or open a localhost URL. Ignore github or anything like this, just a plain demo .html file right where you are. (Ignore the rocket topic.)";

    // Token: 0x040000A5 RID: 165
    private const string ResearchBuddyAsk = "I'm the CEO of this rocket company you see on my screen. Can you research my competitors and save it as a PDF?";

    // Token: 0x040000A6 RID: 166
    private const string ShippyAsk = "Can you make me a website for the beautiful person watching this video right now, and open it in the browser when you're done?";

    // Token: 0x040000A7 RID: 167
    private readonly HashSet<DispatcherQueueTimer> hostTimers = new HashSet<DispatcherQueueTimer>();

    // Token: 0x040000A8 RID: 168
    [Nullable(2)]
    private MediaPlayer preloadedIntroPlayer;

    // Token: 0x040000A9 RID: 169
    private IntroVideoSourceKind preloadedIntroSource;

    // Token: 0x040000AA RID: 170
    private readonly HostedVideoFailureReporter introVideoFailures = new HostedVideoFailureReporter();

    // Token: 0x040000AB RID: 171
    private readonly List<DispatcherQueueTimer> dryRunTimers = new List<DispatcherQueueTimer>();

    // Token: 0x040000AC RID: 172
    [Nullable(2)]
    private LessonService lessons;

    // Token: 0x040000AD RID: 173
    private bool rosterAttached;

    // Token: 0x040000AE RID: 174
    private bool observingNotchLinkOpens;

    // Token: 0x040000AF RID: 175
    private bool notchWasOpen;

    // Token: 0x040000B0 RID: 176
    private static readonly bool EarconsMuted = Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1";

    // Token: 0x040000B1 RID: 177
    private readonly HashSet<string> announcedExtraUsage = new HashSet<string>();

    // Token: 0x040000B2 RID: 178
    private readonly Dictionary<string, AgentTaskStatus> lastAgentStatus = new Dictionary<string, AgentTaskStatus>();

    // Token: 0x040000B3 RID: 179
    [Nullable(2)]
    private PreSignInIntroViewModel introVm;

    // Token: 0x040000B4 RID: 180
    [Nullable(2)]
    private NotchOnboardingCardViewModel cardVm;

    // Token: 0x040000B5 RID: 181
    private readonly ConcurrentDictionary<string, byte> onboardingDemoTaskIds = new ConcurrentDictionary<string, byte>();

    // Token: 0x040000B6 RID: 182
    [Nullable(2)]
    private EventHandler manualUpdateProbe;

    // Token: 0x040000B8 RID: 184
    private bool resetting;

    // Token: 0x040000B9 RID: 185
    private const IntPtr PBT_POWERSETTINGCHANGE = 32787;

    // Token: 0x040000BA RID: 186
    private static readonly Guid GUID_CONSOLE_DISPLAY_STATE = new Guid("6FE69556-704A-47A0-8F24-C28D936FDA47");

    // Token: 0x040000BB RID: 187
    private IntPtr displayPowerNotification;

    // Token: 0x040000BC RID: 188
    private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0;

    // Token: 0x040000BD RID: 189
    private const int SystemPowerCapabilities = 4;

    // Token: 0x040000BE RID: 190
    [Nullable(2)]
    private AppSettings appliedSettings;

    // Token: 0x040000BF RID: 191
    private bool accountWasSignedIn;

    // Token: 0x040000C0 RID: 192
    [Nullable(2)]
    private UpdateService updates;

    // Token: 0x040000C3 RID: 195
    private readonly HashSet<string> agentsSeen = new HashSet<string>();

    // Token: 0x040000C4 RID: 196
    private const uint WM_POWERBROADCAST = 536U;

    // Token: 0x040000C5 RID: 197
    private const IntPtr PBT_APMSUSPEND = 4;

    // Token: 0x040000C6 RID: 198
    private const IntPtr PBT_APMRESUMESUSPEND = 7;

    // Token: 0x040000C7 RID: 199
    private const IntPtr PBT_APMRESUMEAUTOMATIC = 18;

    // Token: 0x040000C8 RID: 200
    private DateTimeOffset lastResume;

    // Token: 0x040000C9 RID: 201
    private int claimingReferral;

    // Token: 0x040000CA RID: 202
    [Nullable(2)]
    private TaskbarButton taskbarButton;

    // Token: 0x040000CB RID: 203
    [Nullable(2)]
    private HomeTour tour;

    // Token: 0x040000CC RID: 204
    private long lastRefusalAt;

    // Token: 0x040000CD RID: 205
    private bool finaleSuppressesEndLine;

    // Token: 0x040000CE RID: 206
    private bool paywallLineWired;

    // Token: 0x040000CF RID: 207
    [Nullable(2)]
    private Action afterOnboardingPaywall;

    // Token: 0x040000D0 RID: 208
    [Nullable(2)]
    private DispatcherQueueTimer notchAboveDrawingTimer;

    // Token: 0x040000D1 RID: 209
    private bool trayTooltipWired;

    // Token: 0x040000D2 RID: 210
    [Nullable(2)]
    private TrayIcon tray;

    // Token: 0x040000D3 RID: 211
    [Nullable(2)]
    private SettingsWindow settings;

    // Token: 0x040000D4 RID: 212
    [Nullable(2)]
    private IntroWindow intro;

    // Token: 0x040000D5 RID: 213
    [Nullable(2)]
    private TutorialWindow tutorial;

    // Token: 0x040000D6 RID: 214
    [Nullable(2)]
    private HatchFlightOverlay flight;

    // Token: 0x040000D7 RID: 215
    private bool notchFirstShownTracked;

    // Token: 0x040000D8 RID: 216
    private bool tutorialVmWired;

    // Token: 0x040000DA RID: 218
    private bool fakeIntroWelcomeActive;

    // Token: 0x040000DB RID: 219
    private bool fakeIntroDismissedTracked;

    // Token: 0x0200037F RID: 895
    [NullableContext(0)]
    private enum ShortcutKind
    {
      // Token: 0x04001279 RID: 4729
      PushToTalk,
      // Token: 0x0400127A RID: 4730
      Text,
      // Token: 0x0400127B RID: 4731
      Dictation
    }

    // Token: 0x02000381 RID: 897
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400127F RID: 4735
      [Nullable(new byte[] { 0, 1, 1, 1 })]
      public static Func<string, string, Task<bool>> <0>__Open;

      // Token: 0x04001280 RID: 4736
      [Nullable(0)]
      public static Action <1>__TrackCursorDetached;

      // Token: 0x04001281 RID: 4737
      [Nullable(0)]
      public static Action <2>__TrackCursorDetachedAutoReattach;

      // Token: 0x04001282 RID: 4738
      [Nullable(new byte[] { 0, 1 })]
      public static Action<string> <3>__MarkRead;

      // Token: 0x04001283 RID: 4739
      [Nullable(new byte[] { 0, 1, 2 })]
      public static Func<string, string> <4>__NoteTitle;

      // Token: 0x04001284 RID: 4740
      [Nullable(new byte[] { 0, 1 })]
      public static Func<string, bool> <5>__IsMeaningfulFile;

      // Token: 0x04001285 RID: 4741
      [Nullable(0)]
      public static Action <6>__TrackNotchFileDragAbandoned;

      // Token: 0x04001286 RID: 4742
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<string, string> <7>__Strip;

      // Token: 0x04001287 RID: 4743
      [Nullable(0)]
      public static DispatcherQueueHandler <8>__RaiseChanged;

      // Token: 0x04001288 RID: 4744
      [Nullable(new byte[] { 0, 2 })]
      public static Func<JsonObject> <9>__FrontmostApplicationProperties;

      // Token: 0x04001289 RID: 4745
      [Nullable(0)]
      public static Action <10>__ReportPreviousCrashes;

      // Token: 0x0400128A RID: 4746
      [Nullable(new byte[] { 0, 1 })]
      public static Action<string, double> <11>__Play;
    }
  }
}
