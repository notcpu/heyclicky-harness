using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.App.Dock;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Browser;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Tools;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.Browser
{
  // Token: 0x02000361 RID: 865
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ChromeSetupFlow
  {
    // Token: 0x170006A7 RID: 1703
    // (get) Token: 0x0600258A RID: 9610 RVA: 0x0063B914 File Offset: 0x0063B914
    public static ChromeSetupFlow Shared { get; } = new ChromeSetupFlow();

    // Token: 0x0600258B RID: 9611 RVA: 0x0063B91C File Offset: 0x0063B91C
    public static string Raw(ChromeSetupFlow.Choice choice)
    {
      string text;
      if (choice != ChromeSetupFlow.Choice.DoItForMe)
      {
        if (choice != ChromeSetupFlow.Choice.ShowMeHow)
        {
          text = "not_now";
        }
        else
        {
          text = "show_me_how";
        }
      }
      else
      {
        text = "do_it_for_me";
      }
      return text;
    }

    // Token: 0x0600258C RID: 9612 RVA: 0x0063B94C File Offset: 0x0063B94C
    [NullableContext(2)]
    public static ChromeSetupFlow.Choice? Parse(string raw)
    {
      ChromeSetupFlow.Choice? choice;
      if (!(raw == "do_it_for_me"))
      {
        if (!(raw == "show_me_how"))
        {
          if (!(raw == "not_now"))
          {
            choice = null;
          }
          else
          {
            choice = new ChromeSetupFlow.Choice?(ChromeSetupFlow.Choice.NotNow);
          }
        }
        else
        {
          choice = new ChromeSetupFlow.Choice?(ChromeSetupFlow.Choice.ShowMeHow);
        }
      }
      else
      {
        choice = new ChromeSetupFlow.Choice?(ChromeSetupFlow.Choice.DoItForMe);
      }
      return choice;
    }

    // Token: 0x170006A8 RID: 1704
    // (get) Token: 0x0600258D RID: 9613 RVA: 0x0063B9A8 File Offset: 0x0063B9A8
    // (set) Token: 0x0600258E RID: 9614 RVA: 0x0063B9B0 File Offset: 0x0063B9B0
    [Nullable(2)]
    public IChromeClickyDrive Drive
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170006A9 RID: 1705
    // (get) Token: 0x0600258F RID: 9615 RVA: 0x0063B9BC File Offset: 0x0063B9BC
    // (set) Token: 0x06002590 RID: 9616 RVA: 0x0063B9C4 File Offset: 0x0063B9C4
    [Nullable(2)]
    public IChromeSetupJobs Jobs
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170006AA RID: 1706
    // (get) Token: 0x06002591 RID: 9617 RVA: 0x0063B9D0 File Offset: 0x0063B9D0
    public bool IsShowingHow
    {
      get
      {
        return this.isShowingHow;
      }
    }

    // Token: 0x170006AB RID: 1707
    // (get) Token: 0x06002592 RID: 9618 RVA: 0x0063B9D8 File Offset: 0x0063B9D8
    public bool IsSettingUpForThem
    {
      get
      {
        return this.isSettingUpForThem;
      }
    }

    // Token: 0x170006AC RID: 1708
    // (get) Token: 0x06002593 RID: 9619 RVA: 0x0063B9E0 File Offset: 0x0063B9E0
    public bool IsBusy
    {
      get
      {
        return this.isShowingHow || this.isSettingUpForThem || ChromeAutoInstall.IsRunning;
      }
    }

    // Token: 0x170006AD RID: 1709
    // (get) Token: 0x06002594 RID: 9620 RVA: 0x0063B9FC File Offset: 0x0063B9FC
    public bool AddsAgain
    {
      get
      {
        return this.Bridge.HasOldCopyToRemove;
      }
    }

    // Token: 0x170006AE RID: 1710
    // (get) Token: 0x06002595 RID: 9621 RVA: 0x0063BA0C File Offset: 0x0063BA0C
    public bool CanDoItForThem
    {
      get
      {
        if (this.AddsAgain)
        {
          IChromeClickyDrive drive = this.Drive;
          return drive != null && drive.IsAvailable;
        }
        return false;
      }
    }

    // Token: 0x170006AF RID: 1711
    // (get) Token: 0x06002596 RID: 9622 RVA: 0x0063BA2C File Offset: 0x0063BA2C
    public string BrowserName
    {
      get
      {
        return this.Bridge.SetupBrowserName;
      }
    }

    // Token: 0x1400008A RID: 138
    // (add) Token: 0x06002597 RID: 9623 RVA: 0x0063BA3C File Offset: 0x0063BA3C
    // (remove) Token: 0x06002598 RID: 9624 RVA: 0x0063BA74 File Offset: 0x0063BA74
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x170006B0 RID: 1712
    // (get) Token: 0x06002599 RID: 9625 RVA: 0x0063BAAC File Offset: 0x0063BAAC
    private ClickyBrowserBridge Bridge
    {
      get
      {
        return ClickyBrowserBridge.Shared;
      }
    }

    // Token: 0x170006B1 RID: 1713
    // (get) Token: 0x0600259A RID: 9626 RVA: 0x0063BAB4 File Offset: 0x0063BAB4
    [Nullable(2)]
    private AgentService Agents
    {
      [NullableContext(2)]
      get
      {
        AppHost appHost = this.app;
        if (appHost == null)
        {
          return null;
        }
        RealStack real = appHost.Real;
        if (real == null)
        {
          return null;
        }
        return real.Agents;
      }
    }

    // Token: 0x0600259B RID: 9627 RVA: 0x0063BAD4 File Offset: 0x0063BAD4
    private ChromeSetupFlow()
    {
    }

    // Token: 0x170006B2 RID: 1714
    // (get) Token: 0x0600259C RID: 9628 RVA: 0x0063BB00 File Offset: 0x0063BB00
    [Nullable(2)]
    private DispatcherQueue Ui
    {
      [NullableContext(2)]
      get
      {
        AppHost appHost = this.app;
        if (appHost == null)
        {
          return null;
        }
        return appHost.DispatcherQueue;
      }
    }

    // Token: 0x0600259D RID: 9629 RVA: 0x0063BB14 File Offset: 0x0063BB14
    private void OnUi(Action action)
    {
      DispatcherQueue ui = this.Ui;
      if (ui != null)
      {
        ui.TryEnqueue(delegate
        {
          action();
        });
      }
    }

    // Token: 0x0600259E RID: 9630 RVA: 0x0063BB4C File Offset: 0x0063BB4C
    private void Notify()
    {
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x0600259F RID: 9631 RVA: 0x0063BB60 File Offset: 0x0063BB60
    public void Attach(AppHost host)
    {
      if (this.app != null)
      {
        return;
      }
      this.app = host;
      CardFooterRequest.ChromeSetup = () => new ValueTuple<string, bool>(this.BrowserName, this.AddsAgain);
      AgentToolHooks.ChromeSetupBrowserName = () => this.BrowserName;
      bool wasConnected = this.Bridge.IsConnected;
      this.Bridge.ConnectionChanged += delegate
      {
        bool isConnected = this.Bridge.IsConnected;
        if (isConnected == wasConnected)
        {
          return;
        }
        wasConnected = isConnected;
        if (isConnected)
        {
          this.OnUi(new Action(this.ChromeConnected));
        }
      };
    }

    // Token: 0x170006B3 RID: 1715
    // (get) Token: 0x060025A0 RID: 9632 RVA: 0x0063BBD8 File Offset: 0x0063BBD8
    public bool HoldsNewWebsiteJobs
    {
      get
      {
        return !this.Bridge.IsConnected && ChromiumBrowsers.InstalledNames.Count > 0;
      }
    }

    // Token: 0x170006B4 RID: 1716
    // (get) Token: 0x060025A1 RID: 9633 RVA: 0x0063BBF8 File Offset: 0x0063BBF8
    public string AnswerMeaningsForBrain
    {
      get
      {
        if (!this.CanDoItForThem)
        {
          return "approve or show_how = they add it from the Chrome Web Store with you showing them each click";
        }
        return "approve = you remove the old one and add the new version on their screen, show_how = they do it with you showing them each step";
      }
    }

    // Token: 0x060025A2 RID: 9634 RVA: 0x0063BC10 File Offset: 0x0063BC10
    public void Hold(string jobId, Action<bool> start)
    {
      this.Bridge.RefreshSetupBrowserName();
      this.heldStarts[jobId] = start;
      this.askedKeys.Add(jobId);
      IChromeSetupJobs jobs = this.Jobs;
      if (jobs != null)
      {
        jobs.SetWaitsForChrome(jobId, true);
      }
      IChromeSetupJobs jobs2 = this.Jobs;
      if (jobs2 != null)
      {
        jobs2.PresentNeedsYou(jobId);
      }
      string text = "chrome_choice_asked";
      string text2 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["when"] = "before_start";
      jsonObject["adds_again"] = this.AddsAgain;
      ClickyAnalytics.TrackChromeJobAction(text, text2, jsonObject);
      this.Notify();
    }

    // Token: 0x060025A3 RID: 9635 RVA: 0x0063BCB4 File Offset: 0x0063BCB4
    public void JobNeedsChrome(string threadId)
    {
      AgentService agents = this.Agents;
      if (agents == null || agents.TaskIdForThread(threadId) == null)
      {
        return;
      }
      this.Bridge.RefreshSetupBrowserName();
      IChromeSetupJobs jobs = this.Jobs;
      string text = ((jobs != null) ? jobs.JobIdForThread(threadId) : null) ?? threadId;
      if (this.askedKeys.Contains(text) || this.IsBusy)
      {
        return;
      }
      this.askedKeys.Add(text);
      AppHost appHost = this.app;
      if (appHost != null)
      {
        appHost.AnnounceJobNeedsChrome(threadId);
      }
      string text2 = "chrome_choice_asked";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["when"] = "while_working";
      jsonObject["adds_again"] = this.AddsAgain;
      ClickyAnalytics.TrackChromeJobAction(text2, threadId, jsonObject);
      this.Notify();
    }

    // Token: 0x060025A4 RID: 9636 RVA: 0x0063BD7C File Offset: 0x0063BD7C
    public void TurnEndedWithoutChrome(string threadId)
    {
      HashSet<string> hashSet = this.askedKeys;
      IChromeSetupJobs jobs = this.Jobs;
      hashSet.Remove(((jobs != null) ? jobs.JobIdForThread(threadId) : null) ?? threadId);
    }

    // Token: 0x060025A5 RID: 9637 RVA: 0x0063BDA4 File Offset: 0x0063BDA4
    [NullableContext(2)]
    public ChromeSetupFlow.Target TargetFor(string threadId)
    {
      AgentService agents = this.Agents;
      if (agents == null)
      {
        return null;
      }
      if (threadId != null && agents.NeedsChromeConnection(threadId))
      {
        IChromeSetupJobs jobs = this.Jobs;
        return new ChromeSetupFlow.Target((jobs != null) ? jobs.JobIdForThread(threadId) : null, threadId);
      }
      IChromeSetupJobs jobs2 = this.Jobs;
      string text = ((jobs2 != null) ? jobs2.HeldJobIdsNewestFirst().FirstOrDefault<string>() : null);
      if (text != null)
      {
        return new ChromeSetupFlow.Target(text, null);
      }
      string text2 = agents.ThreadsNeedingChrome().LastOrDefault<string>();
      if (text2 != null)
      {
        IChromeSetupJobs jobs3 = this.Jobs;
        return new ChromeSetupFlow.Target((jobs3 != null) ? jobs3.JobIdForThread(text2) : null, text2);
      }
      return null;
    }

    // Token: 0x170006B5 RID: 1717
    // (get) Token: 0x060025A6 RID: 9638 RVA: 0x0063BE30 File Offset: 0x0063BE30
    public bool HasWaitingJob
    {
      get
      {
        IChromeSetupJobs jobs = this.Jobs;
        if (((jobs != null) ? jobs.HeldJobIdsNewestFirst().Count : 0) <= 0)
        {
          AgentService agents = this.Agents;
          return ((agents != null) ? agents.ThreadsNeedingChrome().Count : 0) > 0;
        }
        return true;
      }
    }

    // Token: 0x060025A7 RID: 9639 RVA: 0x0063BE68 File Offset: 0x0063BE68
    public void Choose(ChromeSetupFlow.Choice choice, ChromeSetupFlow.Target target)
    {
      if (this.app == null)
      {
        return;
      }
      if (choice == ChromeSetupFlow.Choice.DoItForMe && !this.CanDoItForThem)
      {
        choice = ChromeSetupFlow.Choice.ShowMeHow;
      }
      string text = "chrome_choice";
      string threadId = target.ThreadId;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["choice"] = ChromeSetupFlow.Raw(choice);
      jsonObject["for_job"] = target.JobId != null || target.ThreadId != null;
      jsonObject["adds_again"] = this.AddsAgain;
      ClickyAnalytics.TrackChromeJobAction(text, threadId, jsonObject);
      this.app.DismissCurrentNotchCard();
      if (choice != ChromeSetupFlow.Choice.ShowMeHow && this.isShowingHow)
      {
        this.app.ClearDrawing();
        this.StopShowingHow();
      }
      switch (choice)
      {
      case ChromeSetupFlow.Choice.DoItForMe:
        if (this.Bridge.IsConnected && !this.AddsAgain)
        {
          this.ChromeConnected();
          return;
        }
        this.isSettingUpForThem = true;
        this.Notify();
        ChromeAutoInstall.InstallByDriving(this, target.ThreadId, false, delegate(bool connected, bool stoppedByUser)
        {
          this.OnUi(delegate
          {
            this.isSettingUpForThem = false;
            this.Notify();
            if (connected)
            {
              if (this.AddsAgain)
              {
                this.ShowHowToRemoveOldCopy();
              }
              this.CarryOnAfterConnecting();
              return;
            }
            if (stoppedByUser)
            {
              this.OfferAgain(target);
              return;
            }
            this.ShowHow(true);
          });
        });
        return;
      case ChromeSetupFlow.Choice.ShowMeHow:
        if (this.Bridge.IsConnected && !this.AddsAgain)
        {
          this.ChromeConnected();
          return;
        }
        this.ShowHow(false);
        return;
      case ChromeSetupFlow.Choice.NotNow:
        this.GoWithoutChrome(target);
        return;
      default:
        return;
      }
    }

    // Token: 0x060025A8 RID: 9640 RVA: 0x0063BFD4 File Offset: 0x0063BFD4
    public void PreviewShowMeHow()
    {
      this.isPreview = true;
      this.ShowHow(false);
    }

    // Token: 0x060025A9 RID: 9641 RVA: 0x0063BFE4 File Offset: 0x0063BFE4
    public void PreviewHeldJob()
    {
      ChromeSetupFlow.<>c__DisplayClass59_0 CS$<>8__locals1 = new ChromeSetupFlow.<>c__DisplayClass59_0();
      CS$<>8__locals1.<>4__this = this;
      if (this.app == null)
      {
        return;
      }
      ChromeSetupFlow.<>c__DisplayClass59_0 CS$<>8__locals2 = CS$<>8__locals1;
      IChromeSetupJobs jobs = this.Jobs;
      CS$<>8__locals2.jobId = ((jobs != null) ? jobs.CreatePreviewJob("Posting the song to Discord", "Post my song in the HeyClicky Discord's #general.") : null);
      if (CS$<>8__locals1.jobId == null)
      {
        Log.Info("Browser", "preview held job: jobs aren't ported on Windows yet; saying the line only");
        this.app.AnnounceLineAsync(this.NeedsChromeLine("Posting the song to Discord"));
        return;
      }
      this.Hold(CS$<>8__locals1.jobId, delegate(bool _)
      {
        IChromeSetupJobs jobs2 = CS$<>8__locals1.<>4__this.Jobs;
        if (jobs2 == null)
        {
          return;
        }
        jobs2.Dismiss(CS$<>8__locals1.jobId);
      });
      this.app.AnnounceLineAsync(this.NeedsChromeLine("Posting the song to Discord"));
    }

    // Token: 0x060025AA RID: 9642 RVA: 0x0063C088 File Offset: 0x0063C088
    public string NeedsChromeLine([Nullable(2)] string jobName)
    {
      return ClickyNotes.NeedsChromeLine(string.IsNullOrWhiteSpace(jobName) ? null : jobName.Trim(), this.BrowserName, this.AddsAgain, this.CanDoItForThem);
    }

    // Token: 0x060025AB RID: 9643 RVA: 0x0063C0B4 File Offset: 0x0063C0B4
    private void OfferAgain(ChromeSetupFlow.Target target)
    {
      string jobId = target.JobId;
      if (jobId != null)
      {
        IChromeSetupJobs jobs = this.Jobs;
        if (jobs != null && jobs.IsHeldForChrome(jobId))
        {
          this.Jobs.PresentNeedsYou(jobId);
          return;
        }
      }
      string threadId = target.ThreadId;
      if (threadId != null)
      {
        AppHost appHost = this.app;
        if (appHost == null)
        {
          return;
        }
        appHost.PresentTaskNeedsYou(threadId);
      }
    }

    // Token: 0x060025AC RID: 9644 RVA: 0x0063C108 File Offset: 0x0063C108
    public void Stop()
    {
      IChromeClickyDrive drive = this.Drive;
      if (drive != null)
      {
        drive.HandleEscape();
      }
      this.StopShowingHowForEscape();
    }

    // Token: 0x060025AD RID: 9645 RVA: 0x0063C124 File Offset: 0x0063C124
    public bool StopShowingHowForEscape()
    {
      if (!this.isShowingHow)
      {
        return false;
      }
      AppHost appHost = this.app;
      if (appHost != null)
      {
        appHost.ClearDrawing();
      }
      this.StopShowingHow();
      return true;
    }

    // Token: 0x060025AE RID: 9646 RVA: 0x0063C148 File Offset: 0x0063C148
    public void ShowHow(bool afterDrive)
    {
      if (this.app == null)
      {
        return;
      }
      this.isShowingHow = true;
      this.Notify();
      CancellationTokenSource cancellationTokenSource = this.showHowRun;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = (this.showHowRun = new CancellationTokenSource());
      this.ShowHowAsync(afterDrive, cancellationTokenSource2.Token);
    }

    // Token: 0x060025AF RID: 9647 RVA: 0x0063C19C File Offset: 0x0063C19C
    private async Task ShowHowAsync(bool afterDrive, CancellationToken ct)
    {
      bool flag = await this.Bridge.ConnectAsync(true);
      bool registered = flag;
      await this.OnUiAsync(delegate
      {
      });
      if (this.Bridge.IsConnected && !this.isPreview)
      {
        this.OnUi(new Action(this.ChromeConnected));
      }
      else if (!registered && !this.isPreview)
      {
        this.OnUi(new Action(this.StopShowingHow));
      }
      else
      {
        if (this.isPreview)
        {
          this.Bridge.OpenStoreListing();
        }
        try
        {
          await Task.Delay(1200, ct);
        }
        catch
        {
          return;
        }
        flag = !this.isShowingHow;
        if (!flag)
        {
          TaskAwaiter<bool> taskAwaiter = this.WaitForQuietVoiceAsync(ct).GetAwaiter();
          if (!taskAwaiter.IsCompleted)
          {
            await taskAwaiter;
            TaskAwaiter<bool> taskAwaiter2;
            taskAwaiter = taskAwaiter2;
            taskAwaiter2 = default(TaskAwaiter<bool>);
          }
          flag = !taskAwaiter.GetResult();
        }
        if (!flag)
        {
          this.app.StartAppFactTurn(ChromeSetupBriefs.ShowHowFact(this.BrowserName, this.AddsAgain, afterDrive));
        }
      }
    }

    // Token: 0x060025B0 RID: 9648 RVA: 0x0063C1F0 File Offset: 0x0063C1F0
    public void ShowHowToRemoveOldCopy()
    {
      if (this.app == null)
      {
        return;
      }
      this.isShowingHow = true;
      this.Bridge.SetHasOldCopyToRemove(false);
      this.Notify();
      CancellationTokenSource cancellationTokenSource = this.showHowRun;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = (this.showHowRun = new CancellationTokenSource());
      this.<ShowHowToRemoveOldCopy>g__RemoveOldCopyAsync|66_0(cancellationTokenSource2.Token);
    }

    // Token: 0x060025B1 RID: 9649 RVA: 0x0063C24C File Offset: 0x0063C24C
    private async Task<bool> WaitForQuietVoiceAsync(CancellationToken ct)
    {
      int i = 0;
      while (i < 30 && this.app.VoiceState != VoiceState.Idle)
      {
        if (this.app.VoiceState != VoiceState.Listening)
        {
          try
          {
            await Task.Delay(500, ct);
          }
          catch
          {
            return false;
          }
          i++;
          continue;
        }
        return false;
      }
      return this.app.VoiceState == VoiceState.Idle;
    }

    // Token: 0x060025B2 RID: 9650 RVA: 0x0063C298 File Offset: 0x0063C298
    private Task OnUiAsync(Action action)
    {
      TaskCompletionSource done = new TaskCompletionSource();
      DispatcherQueue ui = this.Ui;
      if (ui == null || !ui.TryEnqueue(delegate
      {
        try
        {
          action();
        }
        finally
        {
          done.TrySetResult();
        }
      }))
      {
        done.TrySetResult();
      }
      return done.Task;
    }

    // Token: 0x060025B3 RID: 9651 RVA: 0x0063C2F4 File Offset: 0x0063C2F4
    private void StopShowingHow()
    {
      this.isShowingHow = false;
      this.isPreview = false;
      CancellationTokenSource cancellationTokenSource = this.showHowRun;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.Notify();
    }

    // Token: 0x060025B4 RID: 9652 RVA: 0x0063C31C File Offset: 0x0063C31C
    private void ChromeConnected()
    {
      if (this.app == null || this.isPreview)
      {
        return;
      }
      if (this.isSettingUpForThem || ChromeAutoInstall.IsRunning)
      {
        return;
      }
      bool flag = this.isShowingHow;
      if (flag)
      {
        this.app.ClearDrawing();
      }
      this.StopShowingHow();
      if (this.AddsAgain)
      {
        if (flag)
        {
          this.ShowHowToRemoveOldCopy();
        }
        else
        {
          this.Bridge.SetHasOldCopyToRemove(false);
        }
      }
      this.CarryOnAfterConnecting();
    }

    // Token: 0x060025B5 RID: 9653 RVA: 0x0063C38C File Offset: 0x0063C38C
    private void CarryOnAfterConnecting()
    {
      if (this.app == null)
      {
        return;
      }
      IChromeSetupJobs jobs = this.Jobs;
      foreach (string text in (((jobs != null) ? jobs.HeldJobIdsNewestFirst() : null) ?? Array.Empty<string>()))
      {
        this.Start(text, false);
      }
      AgentService agents = this.Agents;
      foreach (string text2 in (((agents != null) ? agents.ThreadsNeedingChrome() : null) ?? Array.Empty<string>()))
      {
        this.Agents.ClearNeedsChromeConnection(text2);
        this.PassAnswer("HeyClicky is passing on news about your Chrome question: their Chrome is connected now, so the browser tool works. Carry on with the website part. If your work is already done, there's nothing more to do.", "Chrome is connected now. Carry on with the task.", text2);
      }
      this.askedKeys.Clear();
    }

    // Token: 0x060025B6 RID: 9654 RVA: 0x0063C46C File Offset: 0x0063C46C
    private void GoWithoutChrome(ChromeSetupFlow.Target target)
    {
      string jobId = target.JobId;
      if (jobId != null)
      {
        IChromeSetupJobs jobs = this.Jobs;
        if (jobs != null && jobs.IsHeldForChrome(jobId))
        {
          this.Jobs.SetSkipsChrome(jobId);
          this.Start(jobId, true);
          return;
        }
      }
      string threadId = target.ThreadId;
      if (threadId == null)
      {
        return;
      }
      string text;
      if ((text = target.JobId) == null)
      {
        IChromeSetupJobs jobs2 = this.Jobs;
        text = ((jobs2 != null) ? jobs2.JobIdForThread(threadId) : null);
      }
      string text2 = text;
      if (text2 != null)
      {
        IChromeSetupJobs jobs3 = this.Jobs;
        if (jobs3 != null)
        {
          jobs3.SetSkipsChrome(text2);
        }
      }
      else
      {
        this.skippedThreads.Add(threadId);
      }
      AgentService agents = this.Agents;
      if (agents != null)
      {
        agents.ClearNeedsChromeConnection(threadId);
      }
      this.PassAnswer("HeyClicky is passing on the user's answer to your Chrome question: they'd rather not connect Chrome now. Carry on without it: read public pages by fetching them or searching the web, and for a step on a website they're signed into, call mac_step (HeyClicky does it on their screen while they watch). What only their login shows (their home feed, inbox, cart, orders, settings) isn't on any public page, so a search or a guessed account won't find it: get that part with mac_step. Anything public, their own public posts included, search or fetch as usual. If your work is already done, there's nothing more to do.", "I'd rather not connect Chrome now. Carry on without it: read public pages by fetching them or searching the web, and for each step on a website I'm signed into, call mac_step and HeyClicky does it on my screen while I watch. What only my login shows (my home feed, inbox, cart, orders, settings) isn't on any public page, so a search or a guessed account won't find it: get that part with mac_step. Anything public, my own public posts included, search or fetch as usual.", threadId);
    }

    // Token: 0x060025B7 RID: 9655 RVA: 0x0063C520 File Offset: 0x0063C520
    private void Start(string jobId, bool withoutChrome)
    {
      IChromeSetupJobs jobs = this.Jobs;
      if (jobs != null)
      {
        jobs.SetWaitsForChrome(jobId, false);
      }
      Action<bool> action;
      if (this.heldStarts.Remove(jobId, out action))
      {
        action(withoutChrome);
        return;
      }
      IChromeSetupJobs jobs2 = this.Jobs;
      if (jobs2 == null)
      {
        return;
      }
      jobs2.StartFromSavedPrompt(jobId, withoutChrome);
    }

    // Token: 0x060025B8 RID: 9656 RVA: 0x0063C56C File Offset: 0x0063C56C
    private void PassAnswer(string steer, string message, string threadId)
    {
      ChromeSetupFlow.<>c__DisplayClass81_0 CS$<>8__locals1 = new ChromeSetupFlow.<>c__DisplayClass81_0();
      CS$<>8__locals1.threadId = threadId;
      CS$<>8__locals1.steer = steer;
      CS$<>8__locals1.message = message;
      CS$<>8__locals1.agents = this.Agents;
      if (CS$<>8__locals1.agents == null)
      {
        return;
      }
      CS$<>8__locals1.<PassAnswer>g__PassAsync|0();
    }

    // Token: 0x060025B9 RID: 9657 RVA: 0x0063C5B0 File Offset: 0x0063C5B0
    public bool SkipsChrome(string threadId)
    {
      if (!this.skippedThreads.Contains(threadId))
      {
        IChromeSetupJobs jobs = this.Jobs;
        string text = ((jobs != null) ? jobs.JobIdForThread(threadId) : null);
        return text != null && this.Jobs.SkipsChrome(text);
      }
      return true;
    }

    // Token: 0x060025BB RID: 9659 RVA: 0x0063C600 File Offset: 0x0063C600
    [CompilerGenerated]
    private async Task <ShowHowToRemoveOldCopy>g__RemoveOldCopyAsync|66_0(CancellationToken ct)
    {
      await this.Bridge.OpenExtensionsPageAsync();
      try
      {
        await Task.Delay(1200, ct);
      }
      catch
      {
        return;
      }
      if (this.isShowingHow)
      {
        await this.OnUiAsync(delegate
        {
          this.isShowingHow = false;
          this.Notify();
        });
        TaskAwaiter<bool> taskAwaiter = this.WaitForQuietVoiceAsync(ct).GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        if (taskAwaiter.GetResult())
        {
          this.app.StartAppFactTurn(ChromeSetupBriefs.RemoveOldCopyFact(this.BrowserName));
        }
      }
    }

    // Token: 0x040011CD RID: 4557
    private bool isShowingHow;

    // Token: 0x040011CE RID: 4558
    private bool isSettingUpForThem;

    // Token: 0x040011CF RID: 4559
    private bool isPreview;

    // Token: 0x040011D1 RID: 4561
    [Nullable(2)]
    private AppHost app;

    // Token: 0x040011D2 RID: 4562
    private readonly Dictionary<string, Action<bool>> heldStarts = new Dictionary<string, Action<bool>>();

    // Token: 0x040011D3 RID: 4563
    private readonly HashSet<string> askedKeys = new HashSet<string>();

    // Token: 0x040011D4 RID: 4564
    [Nullable(2)]
    private CancellationTokenSource showHowRun;

    // Token: 0x040011D5 RID: 4565
    private readonly HashSet<string> skippedThreads = new HashSet<string>();

    // Token: 0x040011D6 RID: 4566
    public const string WithoutChromeSteer = "HeyClicky is passing on the user's answer to your Chrome question: they'd rather not connect Chrome now. Carry on without it: read public pages by fetching them or searching the web, and for a step on a website they're signed into, call mac_step (HeyClicky does it on their screen while they watch). What only their login shows (their home feed, inbox, cart, orders, settings) isn't on any public page, so a search or a guessed account won't find it: get that part with mac_step. Anything public, their own public posts included, search or fetch as usual. If your work is already done, there's nothing more to do.";

    // Token: 0x040011D7 RID: 4567
    public const string WithoutChromeMessage = "I'd rather not connect Chrome now. Carry on without it: read public pages by fetching them or searching the web, and for each step on a website I'm signed into, call mac_step and HeyClicky does it on my screen while I watch. What only my login shows (my home feed, inbox, cart, orders, settings) isn't on any public page, so a search or a guessed account won't find it: get that part with mac_step. Anything public, my own public posts included, search or fetch as usual.";

    // Token: 0x040011D8 RID: 4568
    public const string ChromeConnectedSteer = "HeyClicky is passing on news about your Chrome question: their Chrome is connected now, so the browser tool works. Carry on with the website part. If your work is already done, there's nothing more to do.";

    // Token: 0x040011D9 RID: 4569
    public const string ChromeConnectedMessage = "Chrome is connected now. Carry on with the task.";

    // Token: 0x040011DA RID: 4570
    public const string ChromeAnswerLaunchContext = "HeyClicky is passing on the user's answer to your Chrome question (they chose it on HeyClicky's card; they didn't type this message).";

    // Token: 0x040011DB RID: 4571
    public const string HelperContextWithoutChrome = "They chose not to connect Chrome for this job: don't ask about Chrome again. For a step on a website they're signed into, call mac_step and HeyClicky does it on their screen while they watch.";

    // Token: 0x02000A72 RID: 2674
    [NullableContext(0)]
    public enum Choice
    {
      // Token: 0x04002D41 RID: 11585
      DoItForMe,
      // Token: 0x04002D42 RID: 11586
      ShowMeHow,
      // Token: 0x04002D43 RID: 11587
      NotNow
    }

    // Token: 0x02000A73 RID: 2675
    [NullableContext(2)]
    [Nullable(0)]
    public sealed class Target : IEquatable<ChromeSetupFlow.Target>
    {
      // Token: 0x06004125 RID: 16677 RVA: 0x006C5E48 File Offset: 0x006C5E48
      public Target(string JobId, string ThreadId)
      {
        this.JobId = JobId;
        this.ThreadId = ThreadId;
        base..ctor();
      }

      // Token: 0x1700087A RID: 2170
      // (get) Token: 0x06004126 RID: 16678 RVA: 0x006C5E60 File Offset: 0x006C5E60
      [Nullable(1)]
      [CompilerGenerated]
      private Type EqualityContract
      {
        [NullableContext(1)]
        [CompilerGenerated]
        get
        {
          return typeof(ChromeSetupFlow.Target);
        }
      }

      // Token: 0x1700087B RID: 2171
      // (get) Token: 0x06004127 RID: 16679 RVA: 0x006C5E6C File Offset: 0x006C5E6C
      // (set) Token: 0x06004128 RID: 16680 RVA: 0x006C5E74 File Offset: 0x006C5E74
      public string JobId { get; set; }

      // Token: 0x1700087C RID: 2172
      // (get) Token: 0x06004129 RID: 16681 RVA: 0x006C5E80 File Offset: 0x006C5E80
      // (set) Token: 0x0600412A RID: 16682 RVA: 0x006C5E88 File Offset: 0x006C5E88
      public string ThreadId { get; set; }

      // Token: 0x0600412B RID: 16683 RVA: 0x006C5E94 File Offset: 0x006C5E94
      [NullableContext(1)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Target");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600412C RID: 16684 RVA: 0x006C5EE0 File Offset: 0x006C5EE0
      [NullableContext(1)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("JobId = ");
        builder.Append(this.JobId);
        builder.Append(", ThreadId = ");
        builder.Append(this.ThreadId);
        return true;
      }

      // Token: 0x0600412D RID: 16685 RVA: 0x006C5F1C File Offset: 0x006C5F1C
      [CompilerGenerated]
      public static bool operator !=(ChromeSetupFlow.Target left, ChromeSetupFlow.Target right)
      {
        return !(left == right);
      }

      // Token: 0x0600412E RID: 16686 RVA: 0x006C5F28 File Offset: 0x006C5F28
      [CompilerGenerated]
      public static bool operator ==(ChromeSetupFlow.Target left, ChromeSetupFlow.Target right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600412F RID: 16687 RVA: 0x006C5F3C File Offset: 0x006C5F3C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField);
      }

      // Token: 0x06004130 RID: 16688 RVA: 0x006C5F7C File Offset: 0x006C5F7C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChromeSetupFlow.Target);
      }

      // Token: 0x06004131 RID: 16689 RVA: 0x006C5F8C File Offset: 0x006C5F8C
      [CompilerGenerated]
      public bool Equals(ChromeSetupFlow.Target other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField));
      }

      // Token: 0x06004133 RID: 16691 RVA: 0x006C5FF0 File Offset: 0x006C5FF0
      [CompilerGenerated]
      private Target([Nullable(1)] ChromeSetupFlow.Target original)
      {
        this.JobId = original.<JobId>k__BackingField;
        this.ThreadId = original.<ThreadId>k__BackingField;
      }

      // Token: 0x06004134 RID: 16692 RVA: 0x006C6010 File Offset: 0x006C6010
      [CompilerGenerated]
      public void Deconstruct(out string JobId, out string ThreadId)
      {
        JobId = this.JobId;
        ThreadId = this.ThreadId;
      }

      // Token: 0x04002D46 RID: 11590
      [Nullable(1)]
      public static readonly ChromeSetupFlow.Target None = new ChromeSetupFlow.Target(null, null);
    }
  }
}
