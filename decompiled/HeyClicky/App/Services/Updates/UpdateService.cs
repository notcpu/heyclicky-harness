using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Identity;
using HeyClicky.Core.Telemetry;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Velopack;
using Velopack.Logging;

namespace HeyClicky.App.Services.Updates
{
  // Token: 0x020000EF RID: 239
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UpdateService : IUpdateService, IDisposable
  {
    // Token: 0x1400002B RID: 43
    // (add) Token: 0x06000B92 RID: 2962 RVA: 0x0055BB5C File Offset: 0x0055BB5C
    // (remove) Token: 0x06000B93 RID: 2963 RVA: 0x0055BB94 File Offset: 0x0055BB94
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x17000205 RID: 517
    // (get) Token: 0x06000B94 RID: 2964 RVA: 0x0055BBCC File Offset: 0x0055BBCC
    public string CurrentVersion { get; }

    // Token: 0x17000206 RID: 518
    // (get) Token: 0x06000B95 RID: 2965 RVA: 0x0055BBD4 File Offset: 0x0055BBD4
    // (set) Token: 0x06000B96 RID: 2966 RVA: 0x0055BBDC File Offset: 0x0055BBDC
    public UpdateState State { get; private set; }

    // Token: 0x17000207 RID: 519
    // (get) Token: 0x06000B97 RID: 2967 RVA: 0x0055BBE8 File Offset: 0x0055BBE8
    // (set) Token: 0x06000B98 RID: 2968 RVA: 0x0055BBF0 File Offset: 0x0055BBF0
    [Nullable(2)]
    public string AvailableVersion
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x17000208 RID: 520
    // (get) Token: 0x06000B99 RID: 2969 RVA: 0x0055BBFC File Offset: 0x0055BBFC
    // (set) Token: 0x06000B9A RID: 2970 RVA: 0x0055BC04 File Offset: 0x0055BC04
    public int DownloadPercent { get; private set; }

    // Token: 0x17000209 RID: 521
    // (get) Token: 0x06000B9B RID: 2971 RVA: 0x0055BC10 File Offset: 0x0055BC10
    // (set) Token: 0x06000B9C RID: 2972 RVA: 0x0055BC18 File Offset: 0x0055BC18
    [Nullable(2)]
    public Action QuitApp
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700020A RID: 522
    // (get) Token: 0x06000B9D RID: 2973 RVA: 0x0055BC24 File Offset: 0x0055BC24
    // (set) Token: 0x06000B9E RID: 2974 RVA: 0x0055BC2C File Offset: 0x0055BC2C
    [Nullable(2)]
    public Func<bool, IdleInstallContext> ReadIdleContext
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700020B RID: 523
    // (get) Token: 0x06000B9F RID: 2975 RVA: 0x0055BC38 File Offset: 0x0055BC38
    public static string PendingInstallFile
    {
      get
      {
        return Path.Combine(AppBundleConfiguration.AppDataDirectory, "pending-update.json");
      }
    }

    // Token: 0x06000BA0 RID: 2976 RVA: 0x0055BC4C File Offset: 0x0055BC4C
    public UpdateService(DispatcherQueue ui, [Nullable(2)] TelemetryConfiguration config = null)
    {
      this.ui = ui;
      if (config == null)
      {
        config = TelemetryConfiguration.Current;
      }
      this.feed = config.UpdateFeedUrl;
      this.feedIsPlaceholder = config.IsPlaceholderFeed;
      try
      {
        this.manager = new UpdateManager(this.feed, new UpdateOptions
        {
          AllowVersionDowngrade = false
        }, null);
        if (!this.manager.IsInstalled)
        {
          this.manager = null;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "updater unavailable: " + ex.Message);
        this.manager = null;
      }
      UpdateManager updateManager = this.manager;
      string text;
      if (updateManager == null)
      {
        text = null;
      }
      else
      {
        SemanticVersion currentVersion = updateManager.CurrentVersion;
        text = ((currentVersion != null) ? currentVersion.ToString() : null);
      }
      this.CurrentVersion = text ?? ClickyClientIdentity.Current.AppVersion;
      this.State = ((this.manager == null) ? UpdateState.Unavailable : UpdateState.Idle);
      if (this.manager != null)
      {
        VelopackAsset updatePendingRestart = this.manager.UpdatePendingRestart;
        if (updatePendingRestart != null)
        {
          this.AvailableVersion = updatePendingRestart.Version.ToString();
          this.State = UpdateState.ReadyToInstall;
        }
      }
      string text2 = "updates";
      string text3;
      if (this.manager != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 3);
        defaultInterpolatedStringHandler.AppendLiteral("v");
        defaultInterpolatedStringHandler.AppendFormatted(this.CurrentVersion);
        defaultInterpolatedStringHandler.AppendLiteral(" (installed), feed ");
        defaultInterpolatedStringHandler.AppendFormatted(this.feed);
        defaultInterpolatedStringHandler.AppendFormatted(this.feedIsPlaceholder ? " (placeholder: no releases published yet)" : "");
        text3 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text3 = "v" + this.CurrentVersion + ": not an installed copy, updates off";
      }
      Log.Info(text2, text3);
    }

    // Token: 0x06000BA1 RID: 2977 RVA: 0x0055BE04 File Offset: 0x0055BE04
    public static bool IsVelopackInstall()
    {
      bool flag;
      try
      {
        flag = new UpdateManager("https://localhost.invalid/", null, null).IsInstalled;
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06000BA2 RID: 2978 RVA: 0x0055BE3C File Offset: 0x0055BE3C
    public void Start()
    {
      if (this.manager == null)
      {
        return;
      }
      if (this.feedIsPlaceholder)
      {
        Log.Info("updates", "scheduled checks off: the update feed is still the placeholder (set updateFeedUrl, RELEASING.md)");
        return;
      }
      this.timer = new Timer(delegate(object _)
      {
        _ = this.CheckAsync(false, null);
      }, null, UpdateService.FirstCheckDelay, UpdateService.CheckInterval);
      this.idleInstallTimer = new Timer(delegate(object _)
      {
        this.ui.TryEnqueue(new DispatcherQueueHandler(this.InstallIfIdle));
      }, null, UpdateService.IdleInstallCheckInterval, UpdateService.IdleInstallCheckInterval);
    }

    // Token: 0x06000BA3 RID: 2979 RVA: 0x0055BEB0 File Offset: 0x0055BEB0
    public void NoteHeyClickyBusy()
    {
      Interlocked.Exchange(ref this.lastBusyTicks, Environment.TickCount64);
    }

    // Token: 0x06000BA4 RID: 2980 RVA: 0x0055BEC4 File Offset: 0x0055BEC4
    public void InstallIfIdle()
    {
      if (!this.installOnQuitArmed)
      {
        Func<bool, IdleInstallContext> readIdleContext = this.ReadIdleContext;
        if (readIdleContext != null)
        {
          string text;
          try
          {
            bool flag = this.State == UpdateState.ReadyToInstall;
            IdleInstallContext idleInstallContext = readIdleContext(flag);
            if (IdleInstallPolicy.BusyReason(idleInstallContext) != null)
            {
              this.NoteHeyClickyBusy();
            }
            if (!flag)
            {
              return;
            }
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(Environment.TickCount64 - Interlocked.Read(ref this.lastBusyTicks), 0L);
            text = IdleInstallPolicy.WhyWait(idleInstallContext, timeSpan, UpdateService.IdleInstallAfter);
          }
          catch (Exception ex)
          {
            Log.Warn("updates", "idle install check failed: " + ex.Message);
            return;
          }
          if (text != null)
          {
            if (text != this.idleInstallWait)
            {
              Log.Info("updates", "v" + this.AvailableVersion + " waits to install: " + text);
            }
            this.idleInstallWait = text;
            return;
          }
          string text2 = "updates";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
          defaultInterpolatedStringHandler.AppendLiteral("HeyClicky idle for ");
          defaultInterpolatedStringHandler.AppendFormatted<double>(UpdateService.IdleInstallAfter.TotalMinutes, "0.#");
          defaultInterpolatedStringHandler.AppendLiteral("+ min: installing v");
          defaultInterpolatedStringHandler.AppendFormatted(this.AvailableVersion);
          defaultInterpolatedStringHandler.AppendLiteral(" by itself");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          this.InstallAndRelaunch("idle_auto");
          return;
        }
      }
    }

    // Token: 0x06000BA5 RID: 2981 RVA: 0x0055C010 File Offset: 0x0055C010
    public Task CheckAfterWakeAsync()
    {
      if (!this.feedIsPlaceholder)
      {
        return this.CheckAsync(false, null);
      }
      return Task.CompletedTask;
    }

    // Token: 0x06000BA6 RID: 2982 RVA: 0x0055C028 File Offset: 0x0055C028
    public Task CheckNowAsync(string surface)
    {
      ClickyAnalytics.TrackCheckForUpdatesClicked(surface);
      return this.CheckAsync(true, surface);
    }

    // Token: 0x06000BA7 RID: 2983 RVA: 0x0055C038 File Offset: 0x0055C038
    private async Task CheckAsync(bool manual, [Nullable(2)] string surface)
    {
      if (this.manager == null)
      {
        if (manual)
        {
          Log.Info("updates", "check skipped: not an installed copy");
        }
      }
      else if (this.State != UpdateState.ReadyToInstall)
      {
        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.busy.WaitAsync(0).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
        }
        if (configuredTaskAwaiter.GetResult())
        {
          try
          {
            UpdateService.<>c__DisplayClass52_0 CS$<>8__locals1 = new UpdateService.<>c__DisplayClass52_0();
            CS$<>8__locals1.<>4__this = this;
            if (manual)
            {
              this.Set(UpdateState.Checking);
            }
            Log.Info("updates", "checking (" + (manual ? ("manual, " + surface) : "scheduled") + ")");
            UpdateInfo info = await this.manager.CheckForUpdatesAsync().ConfigureAwait(false);
            if (info == null)
            {
              Log.Info("updates", "up to date (v" + this.CurrentVersion + ")");
              if (manual)
              {
                this.Set(UpdateState.UpToDate);
                this.ScheduleUpToDateReset();
              }
              else
              {
                UpdateState state = this.State;
                if (state == UpdateState.Checking || state == UpdateState.Failed)
                {
                  this.Set(UpdateState.Idle);
                }
              }
            }
            else
            {
              CS$<>8__locals1.target = info.TargetFullRelease.Version.ToString();
              string text = "updates";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
              defaultInterpolatedStringHandler.AppendLiteral("found v");
              defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.target);
              defaultInterpolatedStringHandler.AppendLiteral(" (");
              defaultInterpolatedStringHandler.AppendFormatted<int>(info.DeltasToTarget.Length);
              defaultInterpolatedStringHandler.AppendLiteral(" delta(s) from v");
              defaultInterpolatedStringHandler.AppendFormatted(this.CurrentVersion);
              defaultInterpolatedStringHandler.AppendLiteral(")");
              Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
              ClickyAnalytics.TrackUpdateFound(this.CurrentVersion, CS$<>8__locals1.target);
              this.AvailableVersion = CS$<>8__locals1.target;
              this.DownloadPercent = 0;
              this.Set(UpdateState.Downloading);
              CS$<>8__locals1.lastLogged = -10;
              await this.manager.DownloadUpdatesAsync(info, delegate(int percent)
              {
                CS$<>8__locals1.<>4__this.DownloadPercent = percent;
                if (percent - CS$<>8__locals1.lastLogged >= 10)
                {
                  CS$<>8__locals1.lastLogged = percent;
                  string text2 = "updates";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(16, 2);
                  defaultInterpolatedStringHandler2.AppendLiteral("downloading v");
                  defaultInterpolatedStringHandler2.AppendFormatted(CS$<>8__locals1.target);
                  defaultInterpolatedStringHandler2.AppendLiteral(": ");
                  defaultInterpolatedStringHandler2.AppendFormatted<int>(percent);
                  defaultInterpolatedStringHandler2.AppendLiteral("%");
                  Log.Info(text2, defaultInterpolatedStringHandler2.ToStringAndClear());
                }
                CS$<>8__locals1.<>4__this.Post();
              }, default(CancellationToken)).ConfigureAwait(false);
              this.pending = info;
              UpdateService.RecordPendingInstall(CS$<>8__locals1.target);
              Log.Info("updates", "v" + CS$<>8__locals1.target + " downloaded; installs on quit or with Install & Relaunch");
              this.Set(UpdateState.ReadyToInstall);
              if (Environment.GetEnvironmentVariable("HEYCLICKY_TEST_INSTALL_AND_RELAUNCH") == "1")
              {
                this.ui.TryEnqueue(delegate
                {
                  this.InstallAndRelaunch("update_flow_test");
                });
              }
              CS$<>8__locals1 = null;
              info = null;
            }
          }
          catch (Exception ex)
          {
            Log.Warn("updates", "check failed: " + ex.Message);
            if (manual || this.State == UpdateState.Downloading)
            {
              this.Set(UpdateState.Failed);
            }
            if (!manual && !this.feedIsPlaceholder)
            {
              ClickyAnalytics.ReportInternalError("updates", "scheduled_check", ex, null, CrashReportLevel.Warning, null, null);
            }
          }
          finally
          {
            this.busy.Release();
          }
        }
      }
    }

    // Token: 0x06000BA8 RID: 2984 RVA: 0x0055C08C File Offset: 0x0055C08C
    private void ScheduleUpToDateReset()
    {
      CancellationTokenSource cancellationTokenSource = this.upToDateReset;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource cancellationTokenSource2 = (this.upToDateReset = new CancellationTokenSource());
      Task.Delay(UpdateService.UpToDateDisplay, cancellationTokenSource2.Token).ContinueWith(delegate(Task t)
      {
        if (!t.IsCanceled && this.State == UpdateState.UpToDate)
        {
          this.Set(UpdateState.Idle);
        }
      }, TaskScheduler.Default);
    }

    // Token: 0x06000BA9 RID: 2985 RVA: 0x0055C0E0 File Offset: 0x0055C0E0
    public void PerformRowAction(string surface)
    {
      if (this.State == UpdateState.ReadyToInstall)
      {
        this.InstallAndRelaunch(surface);
        return;
      }
      UpdateState state = this.State;
      if (state != UpdateState.Checking && state != UpdateState.Downloading)
      {
        this.CheckNowAsync(surface);
      }
    }

    // Token: 0x06000BAA RID: 2986 RVA: 0x0055C120 File Offset: 0x0055C120
    public void InstallAndRelaunch(string surface)
    {
      if (this.manager == null || this.State != UpdateState.ReadyToInstall)
      {
        return;
      }
      string text = this.AvailableVersion ?? "unknown";
      ClickyAnalytics.TrackUpdateInstallClicked(this.CurrentVersion, text, surface);
      UpdateService.RecordPendingInstall(text);
      ClickyAnalytics.FlushAsync();
      try
      {
        Log.Info("updates", "installing v" + text + " and relaunching");
        UpdateInfo updateInfo = this.pending;
        VelopackAsset velopackAsset = ((updateInfo != null) ? updateInfo.TargetFullRelease : null) ?? this.manager.UpdatePendingRestart;
        this.manager.WaitExitThenApplyUpdates(velopackAsset, true, true, Array.Empty<string>());
        this.installOnQuitArmed = true;
        if (surface != "idle_auto")
        {
          UpdateWindow.TryShow((velopackAsset != null) ? velopackAsset.FileName : null);
        }
        Action quitApp = this.QuitApp;
        if (quitApp != null)
        {
          quitApp();
        }
      }
      catch (Exception ex)
      {
        Log.Error("updates", "install failed", ex);
        ClickyAnalytics.ReportInternalError("updates", "install", ex, null, CrashReportLevel.Error, null, null);
        this.Set(UpdateState.Failed);
      }
    }

    // Token: 0x06000BAB RID: 2987 RVA: 0x0055C230 File Offset: 0x0055C230
    public void ApplyOnQuitIfReady()
    {
      if (this.manager == null || this.installOnQuitArmed)
      {
        return;
      }
      UpdateInfo updateInfo = this.pending;
      VelopackAsset velopackAsset = ((updateInfo != null) ? updateInfo.TargetFullRelease : null) ?? this.manager.UpdatePendingRestart;
      if (velopackAsset == null)
      {
        return;
      }
      try
      {
        string text = "updates";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 1);
        defaultInterpolatedStringHandler.AppendLiteral("quitting with v");
        defaultInterpolatedStringHandler.AppendFormatted<SemanticVersion>(velopackAsset.Version);
        defaultInterpolatedStringHandler.AppendLiteral(" downloaded: installing it as the app exits");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        this.manager.WaitExitThenApplyUpdates(velopackAsset, true, false, Array.Empty<string>());
        this.installOnQuitArmed = true;
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "install on quit failed: " + ex.Message);
      }
    }

    // Token: 0x06000BAC RID: 2988 RVA: 0x0055C300 File Offset: 0x0055C300
    private static void RecordPendingInstall(string toVersion)
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(UpdateService.PendingInstallFile));
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["from_version"] = UpdateService.CurrentVersionStatic();
        jsonObject["to_version"] = toVersion;
        jsonObject["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        JsonObject jsonObject2 = jsonObject;
        File.WriteAllText(UpdateService.PendingInstallFile, jsonObject2.ToJsonString(null));
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "pending-install record not saved: " + ex.Message);
      }
    }

    // Token: 0x06000BAD RID: 2989 RVA: 0x0055C3B0 File Offset: 0x0055C3B0
    private static string CurrentVersionStatic()
    {
      string text;
      try
      {
        SemanticVersion currentVersion = new UpdateManager("https://localhost.invalid/", null, null).CurrentVersion;
        text = ((currentVersion != null) ? currentVersion.ToString() : null) ?? ClickyClientIdentity.Current.AppVersion;
      }
      catch
      {
        text = ClickyClientIdentity.Current.AppVersion;
      }
      return text;
    }

    // Token: 0x06000BAE RID: 2990 RVA: 0x0055C40C File Offset: 0x0055C40C
    [NullableContext(2)]
    public string ConsumePendingInstallCompletion()
    {
      if (this.manager == null)
      {
        return null;
      }
      string text;
      try
      {
        if (!File.Exists(UpdateService.PendingInstallFile))
        {
          text = null;
        }
        else
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(UpdateService.PendingInstallFile), null, default(JsonDocumentOptions)) as JsonObject;
          File.Delete(UpdateService.PendingInstallFile);
          string text2;
          if (jsonObject == null)
          {
            text2 = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject["from_version"];
            text2 = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
          }
          string text3 = text2;
          string text4;
          if (jsonObject == null)
          {
            text4 = null;
          }
          else
          {
            JsonNode jsonNode2 = jsonObject["to_version"];
            text4 = ((jsonNode2 != null) ? jsonNode2.GetValue<string>() : null);
          }
          string text5 = text4 ?? "unknown";
          if (text3 == null || text3 == this.CurrentVersion)
          {
            string text6 = "updates";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
            defaultInterpolatedStringHandler.AppendLiteral("pending install of v");
            defaultInterpolatedStringHandler.AppendFormatted(text5);
            defaultInterpolatedStringHandler.AppendLiteral(" didn't land (still v");
            defaultInterpolatedStringHandler.AppendFormatted(this.CurrentVersion);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            Log.Info(text6, defaultInterpolatedStringHandler.ToStringAndClear());
            text = null;
          }
          else
          {
            Log.Info("updates", "updated: v" + text3 + " → v" + this.CurrentVersion);
            ClickyAnalytics.TrackUpdateInstallCompleted(text3, text5);
            text = this.CurrentVersion;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "pending-install record unreadable: " + ex.Message);
        try
        {
          File.Delete(UpdateService.PendingInstallFile);
        }
        catch
        {
        }
        text = null;
      }
      return text;
    }

    // Token: 0x1700020C RID: 524
    // (get) Token: 0x06000BAF RID: 2991 RVA: 0x0055C5AC File Offset: 0x0055C5AC
    public string RowTitle
    {
      get
      {
        UpdateState state = this.State;
        string text;
        if (state != UpdateState.Downloading)
        {
          if (state == UpdateState.ReadyToInstall)
          {
            text = "Update ready to install";
          }
          else
          {
            text = "App updates";
          }
        }
        else
        {
          text = "Update available";
        }
        return text;
      }
    }

    // Token: 0x1700020D RID: 525
    // (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0055C5E0 File Offset: 0x0055C5E0
    public string RowSubtitle
    {
      get
      {
        switch (this.State)
        {
        case UpdateState.Unavailable:
          return "You're on version " + this.CurrentVersion + ". This copy wasn't installed with the HeyClicky installer, so it can't update itself.";
        case UpdateState.Checking:
          return "Checking for updates…";
        case UpdateState.UpToDate:
          return "You're up to date. You're on version " + this.CurrentVersion + ".";
        case UpdateState.Downloading:
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
          defaultInterpolatedStringHandler.AppendLiteral("Version ");
          defaultInterpolatedStringHandler.AppendFormatted(this.AvailableVersion);
          defaultInterpolatedStringHandler.AppendLiteral(" is downloading (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.DownloadPercent);
          defaultInterpolatedStringHandler.AppendLiteral("%).");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        case UpdateState.ReadyToInstall:
          return "Version " + this.AvailableVersion + " is ready. Installing relaunches HeyClicky.";
        case UpdateState.Failed:
          return "Couldn't check for updates just now. Try again in a bit.";
        }
        return "You're on version " + this.CurrentVersion + ".";
      }
    }

    // Token: 0x1700020E RID: 526
    // (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0055C6E0 File Offset: 0x0055C6E0
    public string ButtonTitle
    {
      get
      {
        switch (this.State)
        {
        case UpdateState.Checking:
          return "Checking…";
        case UpdateState.Downloading:
          return "Downloading…";
        case UpdateState.ReadyToInstall:
          return "Install & Relaunch";
        }
        return "Check for updates";
      }
    }

    // Token: 0x1700020F RID: 527
    // (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0055C730 File Offset: 0x0055C730
    public bool ButtonIsPrimary
    {
      get
      {
        UpdateState state = this.State;
        return state - UpdateState.Downloading <= 1;
      }
    }

    // Token: 0x06000BB3 RID: 2995 RVA: 0x0055C754 File Offset: 0x0055C754
    private void Set(UpdateState state)
    {
      this.State = state;
      this.Post();
    }

    // Token: 0x06000BB4 RID: 2996 RVA: 0x0055C764 File Offset: 0x0055C764
    private void Post()
    {
      if (!this.ui.TryEnqueue(delegate
      {
        EventHandler changed2 = this.Changed;
        if (changed2 == null)
        {
          return;
        }
        changed2(this, EventArgs.Empty);
      }))
      {
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      }
    }

    // Token: 0x06000BB5 RID: 2997 RVA: 0x0055C798 File Offset: 0x0055C798
    public void Dispose()
    {
      Timer timer = this.timer;
      if (timer != null)
      {
        timer.Dispose();
      }
      Timer timer2 = this.idleInstallTimer;
      if (timer2 != null)
      {
        timer2.Dispose();
      }
      CancellationTokenSource cancellationTokenSource = this.upToDateReset;
      if (cancellationTokenSource == null)
      {
        return;
      }
      cancellationTokenSource.Cancel();
    }

    // Token: 0x06000BB6 RID: 2998 RVA: 0x0055C7CC File Offset: 0x0055C7CC
    public static VelopackApp ConfigureVelopack(string[] args)
    {
      return VelopackApp.Build().SetArgs(args).SetLogger(new UpdateService.VelopackLog())
        .OnAfterInstallFastCallback(delegate(SemanticVersion _)
        {
          UpdateService.RegisterShellIntegration();
        })
        .OnAfterUpdateFastCallback(delegate(SemanticVersion _)
        {
          UpdateService.RegisterShellIntegration();
        })
        .OnBeforeUninstallFastCallback(delegate(SemanticVersion _)
        {
          UpdateService.RemoveShellIntegration();
        });
    }

    // Token: 0x06000BB7 RID: 2999 RVA: 0x0055C85C File Offset: 0x0055C85C
    private static void RegisterShellIntegration()
    {
      try
      {
        ProtocolRegistration.Register(null);
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "protocol registration: " + ex.Message);
      }
    }

    // Token: 0x06000BB8 RID: 3000 RVA: 0x0055C8A0 File Offset: 0x0055C8A0
    private static void RemoveShellIntegration()
    {
      try
      {
        if (ProtocolRegistration.IsRegistered(null))
        {
          ProtocolRegistration.Unregister();
        }
        FileOpenRegistration.Unregister(null);
        string command = LaunchAtLogin.GetCommand("HeyClicky");
        if (command != null)
        {
          string processPath = Environment.ProcessPath;
          if (processPath != null && command.Contains(processPath, StringComparison.OrdinalIgnoreCase))
          {
            LaunchAtLogin.SetEnabled(false, "HeyClicky", null);
          }
        }
        Log.Info("updates", "uninstall: deep links and launch at login removed");
      }
      catch (Exception ex)
      {
        Log.Warn("updates", "uninstall cleanup: " + ex.Message);
      }
    }

    // Token: 0x06000BB9 RID: 3001 RVA: 0x0055C92C File Offset: 0x0055C92C
    // Note: this type is marked as 'beforefieldinit'.
    static UpdateService()
    {
      double num;
      UpdateService.IdleInstallAfter = (double.TryParse(Environment.GetEnvironmentVariable("HEYCLICKY_IDLE_INSTALL_AFTER_SECONDS"), NumberStyles.Float, CultureInfo.InvariantCulture, out num) ? TimeSpan.FromSeconds(num) : IdleInstallPolicy.IdleFor);
      UpdateService.IdleInstallCheckInterval = ((UpdateService.IdleInstallAfter < TimeSpan.FromMinutes(1L)) ? TimeSpan.FromSeconds(5L) : TimeSpan.FromSeconds(15L));
    }

    // Token: 0x0400042F RID: 1071
    private const string Area = "updates";

    // Token: 0x04000430 RID: 1072
    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(1);

    // Token: 0x04000431 RID: 1073
    private static readonly TimeSpan FirstCheckDelay = TimeSpan.FromSeconds(20L);

    // Token: 0x04000432 RID: 1074
    private static readonly TimeSpan UpToDateDisplay = TimeSpan.FromSeconds(10L);

    // Token: 0x04000433 RID: 1075
    private static readonly TimeSpan IdleInstallAfter;

    // Token: 0x04000434 RID: 1076
    private static readonly TimeSpan IdleInstallCheckInterval;

    // Token: 0x04000435 RID: 1077
    private readonly DispatcherQueue ui;

    // Token: 0x04000436 RID: 1078
    [Nullable(2)]
    private readonly UpdateManager manager;

    // Token: 0x04000437 RID: 1079
    private readonly string feed;

    // Token: 0x04000438 RID: 1080
    private readonly bool feedIsPlaceholder;

    // Token: 0x04000439 RID: 1081
    private readonly SemaphoreSlim busy = new SemaphoreSlim(1, 1);

    // Token: 0x0400043A RID: 1082
    [Nullable(2)]
    private Timer timer;

    // Token: 0x0400043B RID: 1083
    [Nullable(2)]
    private Timer idleInstallTimer;

    // Token: 0x0400043C RID: 1084
    [Nullable(2)]
    private string idleInstallWait;

    // Token: 0x0400043D RID: 1085
    private long lastBusyTicks = Environment.TickCount64;

    // Token: 0x0400043E RID: 1086
    [Nullable(2)]
    private UpdateInfo pending;

    // Token: 0x0400043F RID: 1087
    [Nullable(2)]
    private CancellationTokenSource upToDateReset;

    // Token: 0x04000447 RID: 1095
    private bool installOnQuitArmed;

    // Token: 0x0200056D RID: 1389
    [NullableContext(0)]
    private sealed class VelopackLog : IVelopackLogger
    {
      // Token: 0x06002E39 RID: 11833 RVA: 0x0065F71C File Offset: 0x0065F71C
      [NullableContext(2)]
      public void Log(VelopackLogLevel level, string message, Exception exception)
      {
        if (level < VelopackLogLevel.Information)
        {
          return;
        }
        string text = ((exception == null) ? (message ?? "") : (message + ": " + exception.Message));
        if (level >= VelopackLogLevel.Error)
        {
          HeyClicky.Log.Warn("velopack", text);
          return;
        }
        HeyClicky.Log.Info("velopack", text);
      }
    }
  }
}
