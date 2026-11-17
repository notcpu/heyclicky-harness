using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Notch.Window;
using HeyClicky.App.Proactive;
using HeyClicky.App.Shell;
using HeyClicky.Core;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Dictation;
using HeyClicky.Core.Net;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Tools;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;
using HeyClicky.Drawing;
using HeyClicky.Platform.Audio;
using HeyClicky.Platform.Capture;
using HeyClicky.Platform.Input;
using HeyClicky.Platform.Overlay;
using HeyClicky.Platform.Permissions;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Microsoft.Win32;
using Windows.System.UserProfile;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009E RID: 158
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RealStack : IAsyncDisposable
  {
    // Token: 0x170000FC RID: 252
    // (get) Token: 0x06000663 RID: 1635 RVA: 0x005306A8 File Offset: 0x005306A8
    // (set) Token: 0x06000664 RID: 1636 RVA: 0x005306B0 File Offset: 0x005306B0
    public CoreServices Core { get; private set; }

    // Token: 0x170000FD RID: 253
    // (get) Token: 0x06000665 RID: 1637 RVA: 0x005306BC File Offset: 0x005306BC
    // (set) Token: 0x06000666 RID: 1638 RVA: 0x005306C4 File Offset: 0x005306C4
    public PermissionService Permissions { get; private set; }

    // Token: 0x170000FE RID: 254
    // (get) Token: 0x06000667 RID: 1639 RVA: 0x005306D0 File Offset: 0x005306D0
    // (set) Token: 0x06000668 RID: 1640 RVA: 0x005306D8 File Offset: 0x005306D8
    public IMicrophoneCapture Microphone { get; private set; }

    // Token: 0x170000FF RID: 255
    // (get) Token: 0x06000669 RID: 1641 RVA: 0x005306E4 File Offset: 0x005306E4
    // (set) Token: 0x0600066A RID: 1642 RVA: 0x005306EC File Offset: 0x005306EC
    public WasapiAudioOutput Audio { get; private set; }

    // Token: 0x17000100 RID: 256
    // (get) Token: 0x0600066B RID: 1643 RVA: 0x005306F8 File Offset: 0x005306F8
    // (set) Token: 0x0600066C RID: 1644 RVA: 0x00530700 File Offset: 0x00530700
    public MicrophoneStartCue MicrophoneStartCue { get; private set; }

    // Token: 0x17000101 RID: 257
    // (get) Token: 0x0600066D RID: 1645 RVA: 0x0053070C File Offset: 0x0053070C
    // (set) Token: 0x0600066E RID: 1646 RVA: 0x00530714 File Offset: 0x00530714
    public ScreenCaptureService Capture { get; private set; }

    // Token: 0x17000102 RID: 258
    // (get) Token: 0x0600066F RID: 1647 RVA: 0x00530720 File Offset: 0x00530720
    // (set) Token: 0x06000670 RID: 1648 RVA: 0x00530728 File Offset: 0x00530728
    public DeferredCursorOverlay Cursor { get; private set; }

    // Token: 0x17000103 RID: 259
    // (get) Token: 0x06000671 RID: 1649 RVA: 0x00530734 File Offset: 0x00530734
    // (set) Token: 0x06000672 RID: 1650 RVA: 0x0053073C File Offset: 0x0053073C
    public DeferredHighlightOverlay Highlight { get; private set; }

    // Token: 0x17000104 RID: 260
    // (get) Token: 0x06000673 RID: 1651 RVA: 0x00530748 File Offset: 0x00530748
    // (set) Token: 0x06000674 RID: 1652 RVA: 0x00530750 File Offset: 0x00530750
    public ScreenDrawingCanvas Canvas { get; private set; }

    // Token: 0x17000105 RID: 261
    // (get) Token: 0x06000675 RID: 1653 RVA: 0x0053075C File Offset: 0x0053075C
    // (set) Token: 0x06000676 RID: 1654 RVA: 0x00530764 File Offset: 0x00530764
    public AgentService Agents { get; private set; }

    // Token: 0x17000106 RID: 262
    // (get) Token: 0x06000677 RID: 1655 RVA: 0x00530770 File Offset: 0x00530770
    // (set) Token: 0x06000678 RID: 1656 RVA: 0x00530778 File Offset: 0x00530778
    public CompanionSession Companion { get; private set; }

    // Token: 0x17000107 RID: 263
    // (get) Token: 0x06000679 RID: 1657 RVA: 0x00530784 File Offset: 0x00530784
    // (set) Token: 0x0600067A RID: 1658 RVA: 0x0053078C File Offset: 0x0053078C
    public DictationController Dictation { get; private set; }

    // Token: 0x17000108 RID: 264
    // (get) Token: 0x0600067B RID: 1659 RVA: 0x00530798 File Offset: 0x00530798
    // (set) Token: 0x0600067C RID: 1660 RVA: 0x005307A0 File Offset: 0x005307A0
    public ShortcutMonitor Shortcuts { get; private set; }

    // Token: 0x17000109 RID: 265
    // (get) Token: 0x0600067D RID: 1661 RVA: 0x005307AC File Offset: 0x005307AC
    // (set) Token: 0x0600067E RID: 1662 RVA: 0x005307B4 File Offset: 0x005307B4
    public OnboardingTutorialViewModel Tutorial { get; private set; }

    // Token: 0x1700010A RID: 266
    // (get) Token: 0x0600067F RID: 1663 RVA: 0x005307C0 File Offset: 0x005307C0
    // (set) Token: 0x06000680 RID: 1664 RVA: 0x005307C8 File Offset: 0x005307C8
    public BrainContext BrainContext { get; private set; }

    // Token: 0x1700010B RID: 267
    // (get) Token: 0x06000681 RID: 1665 RVA: 0x005307D4 File Offset: 0x005307D4
    // (set) Token: 0x06000682 RID: 1666 RVA: 0x005307DC File Offset: 0x005307DC
    public CompanionTools Tools { get; private set; }

    // Token: 0x1700010C RID: 268
    // (get) Token: 0x06000683 RID: 1667 RVA: 0x005307E8 File Offset: 0x005307E8
    // (set) Token: 0x06000684 RID: 1668 RVA: 0x005307F0 File Offset: 0x005307F0
    public AgentVoiceRelay AgentVoice { get; private set; }

    // Token: 0x1700010D RID: 269
    // (get) Token: 0x06000685 RID: 1669 RVA: 0x005307FC File Offset: 0x005307FC
    // (set) Token: 0x06000686 RID: 1670 RVA: 0x00530804 File Offset: 0x00530804
    public OneBrainJobReports JobReports { get; private set; }

    // Token: 0x1700010E RID: 270
    // (get) Token: 0x06000687 RID: 1671 RVA: 0x00530810 File Offset: 0x00530810
    // (set) Token: 0x06000688 RID: 1672 RVA: 0x00530818 File Offset: 0x00530818
    public AgentToolHost ToolHost { get; private set; }

    // Token: 0x1700010F RID: 271
    // (get) Token: 0x06000689 RID: 1673 RVA: 0x00530824 File Offset: 0x00530824
    // (set) Token: 0x0600068A RID: 1674 RVA: 0x0053082C File Offset: 0x0053082C
    [Nullable(2)]
    public Func<string, bool> OpenHome
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000110 RID: 272
    // (get) Token: 0x0600068B RID: 1675 RVA: 0x00530838 File Offset: 0x00530838
    // (set) Token: 0x0600068C RID: 1676 RVA: 0x00530840 File Offset: 0x00530840
    [Nullable(2)]
    public Func<bool> CloseHome
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600068D RID: 1677 RVA: 0x0053084C File Offset: 0x0053084C
    [return: TupleElementNames(new string[] { "Status", "Body" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    private async Task<ValueTuple<int, string>?> PostToWorkerAsync(string path, string jsonBody, CancellationToken ct)
    {
      ValueTuple<int, string>? valueTuple;
      try
      {
        byte[] array = await this.Core.Worker.SendRawAsync(HttpMethod.Post, path, new StringContent(jsonBody, Encoding.UTF8, "application/json"), true, ct, new TimeSpan?(TimeSpan.FromSeconds(30L)), "companion").ConfigureAwait(false);
        valueTuple = ((array == null) ? null : new ValueTuple<int, string>?(new ValueTuple<int, string>(200, Encoding.UTF8.GetString(array))));
      }
      catch (WorkerHttpException ex)
      {
        valueTuple = new ValueTuple<int, string>?(new ValueTuple<int, string>(ex.Status, ex.Body));
      }
      catch (PaywallReachedException)
      {
        valueTuple = new ValueTuple<int, string>?(new ValueTuple<int, string>(402, "{\"error\":\"paywall\"}"));
      }
      return valueTuple;
    }

    // Token: 0x0600068E RID: 1678 RVA: 0x005308A8 File Offset: 0x005308A8
    private static string IanaTimeZone()
    {
      string text;
      if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(TimeZoneInfo.Local.Id, out text))
      {
        return TimeZoneInfo.Local.Id;
      }
      return text;
    }

    // Token: 0x0600068F RID: 1679 RVA: 0x005308D4 File Offset: 0x005308D4
    private static IReadOnlyList<string> SystemLanguageTagsForEars()
    {
      ValueTuple<IReadOnlyList<string>, long>? valueTuple = RealStack.cachedEarsSystemLanguages;
      if (valueTuple != null)
      {
        ValueTuple<IReadOnlyList<string>, long> valueOrDefault = valueTuple.GetValueOrDefault();
        if (Environment.TickCount64 - valueOrDefault.Item2 < 600000L)
        {
          return valueOrDefault.Item1;
        }
      }
      List<string> list = new List<string>();
      try
      {
        list.AddRange(GlobalizationPreferences.Languages);
      }
      catch
      {
        list.Add(CultureInfo.CurrentUICulture.Name);
      }
      list.AddRange(KeyboardLayoutInfo.InstalledLayoutLanguageTags());
      IReadOnlyList<string> readOnlyList = list.Distinct<string>(StringComparer.OrdinalIgnoreCase).ToList<string>();
      RealStack.cachedEarsSystemLanguages = new ValueTuple<IReadOnlyList<string>, long>?(new ValueTuple<IReadOnlyList<string>, long>(readOnlyList, Environment.TickCount64));
      return readOnlyList;
    }

    // Token: 0x06000690 RID: 1680 RVA: 0x0053097C File Offset: 0x0053097C
    private static bool OpenUri(string uri)
    {
      bool flag;
      try
      {
        ShellOpen.Start(uri);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("Tools", "open " + uri + ": " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06000691 RID: 1681 RVA: 0x005309CC File Offset: 0x005309CC
    [NullableContext(2)]
    private static string TestSpeakerSilence()
    {
      return null;
    }

    // Token: 0x06000692 RID: 1682 RVA: 0x005309D0 File Offset: 0x005309D0
    private static bool SpotifyAppInstalled()
    {
      bool flag;
      using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("spotify\\shell\\open\\command"))
      {
        flag = registryKey != null;
      }
      return flag;
    }

    // Token: 0x06000693 RID: 1683 RVA: 0x00530A10 File Offset: 0x00530A10
    private bool ShowDocumentAccessCard(CoreServices core, string path)
    {
      if (core.Settings.Read<bool>((AppSettings s) => s.DocumentAccessNoticeSuppressed))
      {
        return false;
      }
      if (core.Notch.HasLiveEvent(typeof(NotchLiveEvent.DocumentFolderAccessDenied)))
      {
        return true;
      }
      int count = 0;
      core.Settings.Update(delegate(AppSettings s)
      {
        int num = s.DocumentAccessNoticeShowCount + 1;
        s.DocumentAccessNoticeShowCount = num;
        count = num;
      });
      string text = RealStack.ProtectedFolderName(path);
      core.Notch.ShowLiveEvent(new NotchLiveEvent.DocumentFolderAccessDenied((text == null) ? "Allow your account to open this file" : ("Allow your account to open this file in " + text), count >= 2, path), null);
      string text2 = "PageRead";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
      defaultInterpolatedStringHandler.AppendLiteral("document access card (");
      defaultInterpolatedStringHandler.AppendFormatted(text ?? "unrecognized folder");
      defaultInterpolatedStringHandler.AppendLiteral(", show #");
      defaultInterpolatedStringHandler.AppendFormatted<int>(count);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      return true;
    }

    // Token: 0x06000694 RID: 1684 RVA: 0x00530B28 File Offset: 0x00530B28
    internal bool RaiseDocumentAccessCardForTests(string path)
    {
      return this.ShowDocumentAccessCard(this.Core, path);
    }

    // Token: 0x06000695 RID: 1685 RVA: 0x00530B38 File Offset: 0x00530B38
    [return: Nullable(2)]
    private static string ProtectedFolderName(string path)
    {
      foreach (ValueTuple<Environment.SpecialFolder, string> valueTuple in new ValueTuple<Environment.SpecialFolder, string>[]
      {
        new ValueTuple<Environment.SpecialFolder, string>(Environment.SpecialFolder.Personal, "Documents"),
        new ValueTuple<Environment.SpecialFolder, string>(Environment.SpecialFolder.Desktop, "Desktop"),
        new ValueTuple<Environment.SpecialFolder, string>(Environment.SpecialFolder.MyPictures, "Pictures"),
        new ValueTuple<Environment.SpecialFolder, string>(Environment.SpecialFolder.MyMusic, "Music"),
        new ValueTuple<Environment.SpecialFolder, string>(Environment.SpecialFolder.MyVideos, "Videos")
      })
      {
        Environment.SpecialFolder item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        string folderPath = Environment.GetFolderPath(item);
        if (folderPath.Length > 0 && path.StartsWith(folderPath + "\\", StringComparison.OrdinalIgnoreCase))
        {
          return item2;
        }
      }
      string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
      if (!path.StartsWith(text + "\\", StringComparison.OrdinalIgnoreCase))
      {
        return null;
      }
      return "Downloads";
    }

    // Token: 0x06000696 RID: 1686 RVA: 0x00530C24 File Offset: 0x00530C24
    public static RealStack CreateCore()
    {
      Stopwatch stopwatch = Stopwatch.StartNew();
      CoreServices core = CoreServices.Create(null, null, null, null);
      PermissionService permissionService = new PermissionService();
      permissionService.StartPolling();
      VoicePreviewStore.Shared = new VoicePreviewStore(core.Http, (CancellationToken ct) => core.Auth.GetValidAccessTokenAsync(ct), null);
      string text = "startup";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 1);
      defaultInterpolatedStringHandler.AppendLiteral("core services in ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(stopwatch.Elapsed.TotalMilliseconds, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      RealStack realStack = new RealStack();
      realStack.Core = core;
      realStack.Permissions = permissionService;
      realStack.Tutorial = new OnboardingTutorialViewModel(core.Settings, () => SystemVolume.EffectiveLevel());
      return realStack;
    }

    // Token: 0x17000111 RID: 273
    // (get) Token: 0x06000697 RID: 1687 RVA: 0x00530D18 File Offset: 0x00530D18
    public bool IsComplete
    {
      get
      {
        return this.Companion != null;
      }
    }

    // Token: 0x06000698 RID: 1688 RVA: 0x00530D28 File Offset: 0x00530D28
    public void Complete(DispatcherQueue ui, [Nullable(2)] SynchronizationContext uiContext)
    {
      RealStack.<>c__DisplayClass97_0 CS$<>8__locals1 = new RealStack.<>c__DisplayClass97_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.sw = Stopwatch.StartNew();
      CS$<>8__locals1.core = this.Core;
      Environment.GetEnvironmentVariable("HEYCLICKY_TEST_MIC_WAV");
      WasapiMicrophoneCapture wasapiMicrophoneCapture = new WasapiMicrophoneCapture();
      wasapiMicrophoneCapture.Prewarm();
      IMicrophoneCapture microphoneCapture = wasapiMicrophoneCapture;
      AudioTopologyMonitor shared = AudioTopologyMonitor.Shared;
      AudioOutputRoute.Changed += delegate(AudioOutputRouteChange change)
      {
        if (change.StreamName != "voice")
        {
          return;
        }
        ClickyAnalytics.TrackRealtimeAudioOutputRouteChange(change.FromTransport, change.ToTransport, change.FromDevice, change.ToDevice, change.DroppedBuffers, change.EngineWasRunning, change.RestartSucceeded);
      };
      AudioOutputRoute.OutputUnavailable += delegate(string stream, string transport, string device)
      {
        if (stream == "voice")
        {
          ClickyAnalytics.ReportRealtimeAudioRouteRebuildFailed(transport, device, 0);
        }
      };
      CS$<>8__locals1.audio = new WasapiAudioOutput();
      Task.Run(new Action(CS$<>8__locals1.audio.PreloadEarcons));
      CS$<>8__locals1.microphoneStartCue = new MicrophoneStartCue(microphoneCapture, delegate(string name, float? volume)
      {
        if (volume != null)
        {
          float valueOrDefault = volume.GetValueOrDefault();
          CS$<>8__locals1.audio.PlayEarcon(name, valueOrDefault);
          return;
        }
        CS$<>8__locals1.audio.PlayEarcon(name);
      });
      CS$<>8__locals1.capture = new ScreenCaptureService();
      double num = CS$<>8__locals1.<Complete>g__Lap|0();
      CS$<>8__locals1.cursor = new DeferredCursorOverlay();
      DeferredHighlightOverlay deferredHighlightOverlay = new DeferredHighlightOverlay();
      ScreenDrawingCanvas screenDrawingCanvas = new ScreenDrawingCanvas((CancellationToken ct) => CS$<>8__locals1.core.Auth.GetValidAccessTokenAsync(ct), CS$<>8__locals1.core.Endpoints.WorkerBaseUrl, ui, delegate(HttpRequestMessage request)
      {
        ClickyClientHeadersHandler.Apply(request.Headers, CS$<>8__locals1.core.Identity);
      });
      screenDrawingCanvas.PenStrokes += delegate([Nullable(2)] object _, IReadOnlyList<PenStrokePath> strokes)
      {
        CS$<>8__locals1.cursor.RidePen(strokes);
      };
      screenDrawingCanvas.Cleared += delegate([Nullable(2)] object _, EventArgs _)
      {
        CS$<>8__locals1.cursor.EndPenRide();
      };
      screenDrawingCanvas.PenHandedBack += delegate([Nullable(2)] object _, EventArgs _)
      {
        CS$<>8__locals1.cursor.EndPenRide();
      };
      screenDrawingCanvas.ScreenChangeWatchFactory = (DisplayInfo display, Action<string> onChanged) => new ScreenDrawingStaleWatch(display, onChanged);
      double num2 = CS$<>8__locals1.<Complete>g__Lap|0();
      AgentRuntimeOptions agentRuntimeOptions = new AgentRuntimeOptions();
      AppHost.PrepareStoredSettings(CS$<>8__locals1.core.Settings, agentRuntimeOptions);
      CS$<>8__locals1.agents = new AgentService(CS$<>8__locals1.core.Endpoints, (CancellationToken ct) => CS$<>8__locals1.core.Auth.GetValidAccessTokenAsync(ct), CS$<>8__locals1.core.Http.Client, CS$<>8__locals1.core.Identity, agentRuntimeOptions);
      CS$<>8__locals1.agents.CurrentUserId = delegate
      {
        if (!CS$<>8__locals1.core.Auth.IsSignedIn)
        {
          return null;
        }
        return CS$<>8__locals1.core.Auth.UserId;
      };
      CS$<>8__locals1.agents.Routines.AgentAllowanceResetsAt = () => CS$<>8__locals1.core.Billing.UsageResetsDate;
      AgentToolHost agentToolHost = (this.ToolHost = new AgentToolHost(CS$<>8__locals1.agents, delegate(string threadTaskId)
      {
        Func<string, bool> openHome = CS$<>8__locals1.<>4__this.OpenHome;
        return openHome != null && openHome(threadTaskId);
      }, delegate
      {
        Func<bool> closeHome = CS$<>8__locals1.<>4__this.CloseHome;
        return closeHome != null && closeHome();
      }));
      CS$<>8__locals1.brainContext = new BrainContext(CS$<>8__locals1.core, agentToolHost);
      AgentTools agentTools = new AgentTools(agentToolHost);
      LocalRootTools localRootTools = new LocalRootTools(() => CS$<>8__locals1.agents.ProjectsRoot, null);
      WorkerPost workerPost = new WorkerPost(this.PostToWorkerAsync);
      Func<string> func;
      if ((func = RealStack.<>O.<0>__IanaTimeZone) == null)
      {
        func = (RealStack.<>O.<0>__IanaTimeZone = new Func<string>(RealStack.IanaTimeZone));
      }
      Func<string, bool> func2;
      if ((func2 = RealStack.<>O.<1>__OpenUri) == null)
      {
        func2 = (RealStack.<>O.<1>__OpenUri = new Func<string, bool>(RealStack.OpenUri));
      }
      Func<bool> func3;
      if ((func3 = RealStack.<>O.<2>__SpotifyAppInstalled) == null)
      {
        func3 = (RealStack.<>O.<2>__SpotifyAppInstalled = new Func<bool>(RealStack.SpotifyAppInstalled));
      }
      CompanionTools companionTools = new CompanionTools(localRootTools, new WorkerTools(workerPost, func, func2, func3), agentTools);
      CS$<>8__locals1.tutorial = this.Tutorial;
      IAudioOutput audioOutput2;
      if (!(Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1"))
      {
        IAudioOutput audioOutput = CS$<>8__locals1.audio;
        audioOutput2 = audioOutput;
      }
      else
      {
        IAudioOutput audioOutput = new MutedAudioOutput(CS$<>8__locals1.audio);
        audioOutput2 = audioOutput;
      }
      IAudioOutput audioOutput3 = audioOutput2;
      RealStack.<>c__DisplayClass97_0 CS$<>8__locals2 = CS$<>8__locals1;
      CoreServices core = CS$<>8__locals1.core;
      IMicrophoneCapture microphoneCapture2 = microphoneCapture;
      IAudioOutput audioOutput4 = audioOutput3;
      IScreenCaptureService capture = CS$<>8__locals1.capture;
      IScreenDrawingCanvas screenDrawingCanvas2 = screenDrawingCanvas;
      ICursorOverlay cursor = CS$<>8__locals1.cursor;
      IHighlightOverlay highlightOverlay = deferredHighlightOverlay;
      CompanionSessionOptions companionSessionOptions = new CompanionSessionOptions();
      companionSessionOptions.UiContext = uiContext;
      companionSessionOptions.DesktopActions = new PlatformDesktopActions();
      companionSessionOptions.ToolCallHandler = new Func<BrainToolCall, CompanionToolContext, CancellationToken, Task<string>>(companionTools.HandleAsync);
      companionSessionOptions.UserContext = new Func<BrainUserContext>(CS$<>8__locals1.brainContext.UserContext);
      companionSessionOptions.RosterNote = new Func<string>(CS$<>8__locals1.brainContext.RosterNote);
      companionSessionOptions.PlanNote = new Func<string>(CS$<>8__locals1.brainContext.PlanNote);
      companionSessionOptions.PageReader = new WindowsPageReader();
      companionSessionOptions.DocumentAccessDenied = (string path) => CS$<>8__locals1.<>4__this.ShowDocumentAccessCard(CS$<>8__locals1.core, path);
      CS$<>8__locals1.silence = RealStack.TestSpeakerSilence();
      Func<string> func4;
      if (CS$<>8__locals1.silence == null)
      {
        if ((func4 = RealStack.<>O.<3>__CurrentSilenceKind) == null)
        {
          func4 = (RealStack.<>O.<3>__CurrentSilenceKind = new Func<string>(SystemAudioOutputVolume.CurrentSilenceKind));
        }
      }
      else
      {
        func4 = () => CS$<>8__locals1.silence;
      }
      companionSessionOptions.OutputSilenceKind = func4;
      companionSessionOptions.IsOnboardingTutorialRestrictingNormalUse = () => CS$<>8__locals1.tutorial.IsRestrictingNormalUse;
      companionSessionOptions.UsageContext = delegate
      {
        if (!CS$<>8__locals1.tutorial.IsEngaged)
        {
          return "companion";
        }
        return "onboarding";
      };
      companionSessionOptions.LiveStateNote = delegate
      {
        Func<bool> isInterviewSendOffInProgress = FlowHooks.IsInterviewSendOffInProgress;
        string text2;
        if (isInterviewSendOffInProgress == null || !isInterviewSendOffInProgress())
        {
          Func<string> liveStateNote = FlowHooks.LiveStateNote;
          if ((text2 = ((liveStateNote != null) ? liveStateNote() : null)) == null)
          {
            Func<string> liveStateNote2 = ProactiveHooks.LiveStateNote;
            if ((text2 = ((liveStateNote2 != null) ? liveStateNote2() : null)) == null)
            {
              string text3;
              if (!CS$<>8__locals1.tutorial.IsRestrictingNormalUse)
              {
                text3 = null;
              }
              else
              {
                Func<string> tourStepNote = FlowHooks.TourStepNote;
                text3 = ((tourStepNote != null) ? tourStepNote() : null);
              }
              if ((text2 = text3) == null)
              {
                Func<string> notchNote = FlowHooks.NotchNote;
                if (notchNote == null)
                {
                  return null;
                }
                return notchNote();
              }
            }
          }
        }
        else
        {
          text2 = null;
        }
        return text2;
      };
      companionSessionOptions.TurnFlow = delegate
      {
        Func<string> turnFlow = FlowHooks.TurnFlow;
        string text4;
        if ((text4 = ((turnFlow != null) ? turnFlow() : null)) == null)
        {
          ProactiveCoordinator proactiveCoordinator = ProactiveCoordinator.Current;
          Guid? guid = ((proactiveCoordinator != null) ? proactiveCoordinator.AdjustingCardId : null);
          if ((text4 = ((guid != null) ? "suggestion_adjust" : null)) == null)
          {
            if (!CS$<>8__locals1.tutorial.IsEngaged)
            {
              Func<bool> isInterviewSendOffInProgress2 = FlowHooks.IsInterviewSendOffInProgress;
              if (isInterviewSendOffInProgress2 == null || !isInterviewSendOffInProgress2())
              {
                return null;
              }
            }
            text4 = "onboarding_tour";
          }
        }
        return text4;
      };
      companionSessionOptions.SnapTargetToControl = (RectD modelBox, DisplayInfo display, CancellationToken ct) => UIAutomationHelpers.ControlFrameNearAsync(modelBox, display.BoundsPx, display.Scale, ct);
      companionSessionOptions.PointerInput = new PointerInput();
      companionSessionOptions.RefuseTurnOverMessageCap = delegate(string _)
      {
        if (!CS$<>8__locals1.core.Billing.ShouldBlockFreeUserMessageTurnLocally())
        {
          return false;
        }
        PaywallPayload paywallPayload = CS$<>8__locals1.core.Billing.MakeLocalMessagesLimitPaywallPayload();
        if (paywallPayload != null)
        {
          BillingPaywallDetector.Present(paywallPayload);
        }
        return true;
      };
      companionSessionOptions.MessageCounted = delegate(bool counted)
      {
        if (counted)
        {
          CS$<>8__locals1.core.Billing.RegisterLocalMessageTurnConsumed();
          return;
        }
        CS$<>8__locals1.core.Billing.RollbackLocalMessageTurnConsumed();
      };
      companionSessionOptions.KeepMouthWarm = () => CS$<>8__locals1.agents.Tasks.Any<AgentTaskSnapshot>(delegate(AgentTaskSnapshot t)
      {
        AgentTaskStatus status = t.Status;
        return status <= AgentTaskStatus.Running;
      });
      companionSessionOptions.HighlightText = delegate(IReadOnlyList<HighlightStroke> strokes, CancellationToken ct)
      {
        RealStack.<>c__DisplayClass97_0.<<Complete>b__29>d <<Complete>b__29>d;
        <<Complete>b__29>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
        <<Complete>b__29>d.<>4__this = CS$<>8__locals1;
        <<Complete>b__29>d.strokes = strokes;
        <<Complete>b__29>d.ct = ct;
        <<Complete>b__29>d.<>1__state = -1;
        <<Complete>b__29>d.<>t__builder.Start<RealStack.<>c__DisplayClass97_0.<<Complete>b__29>d>(ref <<Complete>b__29>d);
        return <<Complete>b__29>d.<>t__builder.Task;
      };
      companionSessionOptions.MutedSpeakerReplyExempt = delegate
      {
        Func<bool> mutedSpeakerReplyExempt = FlowHooks.MutedSpeakerReplyExempt;
        return mutedSpeakerReplyExempt != null && mutedSpeakerReplyExempt();
      };
      companionSessionOptions.TakeFlowBrief = delegate
      {
        Func<JsonObject> takeFlowBrief = FlowHooks.TakeFlowBrief;
        if (takeFlowBrief == null)
        {
          return null;
        }
        return takeFlowBrief();
      };
      companionSessionOptions.TakePillPlanNote = delegate
      {
        Func<string> takePillPlanNote = FlowHooks.TakePillPlanNote;
        if (takePillPlanNote == null)
        {
          return null;
        }
        return takePillPlanNote();
      };
      companionSessionOptions.EdgeDockFocusNote = delegate(bool typed)
      {
        Func<bool, string> edgeDockFocusNote = FlowHooks.EdgeDockFocusNote;
        if (edgeDockFocusNote == null)
        {
          return null;
        }
        return edgeDockFocusNote(typed);
      };
      companionSessionOptions.TakeSpokenWordsDirectly = delegate(string words)
      {
        Func<string, Task<bool>> takeSpokenWordsDirectly = FlowHooks.TakeSpokenWordsDirectly;
        return ((takeSpokenWordsDirectly != null) ? takeSpokenWordsDirectly(words) : null) ?? Task.FromResult<bool>(false);
      };
      companionSessionOptions.BrowserStateNote = delegate(bool canDrive)
      {
        Func<bool, string> browserStateNote = FlowHooks.BrowserStateNote;
        if (browserStateNote == null)
        {
          return null;
        }
        return browserStateNote(canDrive);
      };
      companionSessionOptions.IsOnboardingTurn = delegate
      {
        if (!CS$<>8__locals1.tutorial.IsEngaged)
        {
          Func<bool> isInterviewSendOffInProgress3 = FlowHooks.IsInterviewSendOffInProgress;
          if (isInterviewSendOffInProgress3 == null || !isInterviewSendOffInProgress3())
          {
            Func<bool> isDrawnTourRunning = FlowHooks.IsDrawnTourRunning;
            return isDrawnTourRunning != null && isDrawnTourRunning();
          }
        }
        return true;
      };
      companionSessionOptions.CaptureSettleSeconds = delegate
      {
        Func<double> captureSettleSeconds = FlowHooks.CaptureSettleSeconds;
        if (captureSettleSeconds == null)
        {
          return 0.0;
        }
        return captureSettleSeconds();
      };
      companionSessionOptions.RendersTextInNotchChat = delegate
      {
        Func<bool> rendersTextInNotchChat = FlowHooks.RendersTextInNotchChat;
        return rendersTextInNotchChat != null && rendersTextInNotchChat();
      };
      companionSessionOptions.StepNotchChatAsideForDrawing = delegate
      {
        Func<bool> stepNotchChatAsideForDrawing = FlowHooks.StepNotchChatAsideForDrawing;
        return stepNotchChatAsideForDrawing != null && stepNotchChatAsideForDrawing();
      };
      companionSessionOptions.ChatHistory = () => NotchChat.History;
      Func<IReadOnlyList<string>> func5;
      if ((func5 = RealStack.<>O.<4>__SystemLanguageTagsForEars) == null)
      {
        func5 = (RealStack.<>O.<4>__SystemLanguageTagsForEars = new Func<IReadOnlyList<string>>(RealStack.SystemLanguageTagsForEars));
      }
      companionSessionOptions.SystemLanguageTags = func5;
      CS$<>8__locals2.companion = core.CreateCompanionSession(microphoneCapture2, audioOutput4, capture, screenDrawingCanvas2, cursor, highlightOverlay, companionSessionOptions);
      CS$<>8__locals1.companion.Prewarm();
      CS$<>8__locals1.permissions = this.Permissions;
      ClickyHttp http = CS$<>8__locals1.core.Http;
      SupabaseAuthService auth = CS$<>8__locals1.core.Auth;
      SettingsStore settings = CS$<>8__locals1.core.Settings;
      IMicrophoneCapture microphoneCapture3 = microphoneCapture;
      IDictationTextTarget dictationTextTarget = new DictationTextTarget();
      NotchPresenter notch = CS$<>8__locals1.core.Notch;
      DictationControllerOptions dictationControllerOptions = new DictationControllerOptions();
      dictationControllerOptions.UsageContext = delegate
      {
        if (!CS$<>8__locals1.tutorial.IsEngaged)
        {
          return "companion";
        }
        return "onboarding";
      };
      dictationControllerOptions.IsOnboardingTutorialRestricting = () => CS$<>8__locals1.tutorial.IsRestrictingNormalUse;
      dictationControllerOptions.OnboardingTutorialAllowsPress = delegate
      {
        Func<bool> tutorialAllowsPress = DictationHooks.TutorialAllowsPress;
        if (tutorialAllowsPress == null)
        {
          return CS$<>8__locals1.tutorial.AllowsDictationPress;
        }
        return tutorialAllowsPress();
      };
      dictationControllerOptions.IsOnboardingTutorialDictationStepActive = () => CS$<>8__locals1.tutorial.IsOnDictationStep;
      dictationControllerOptions.ConsumeOnboardingTutorialTranscript = delegate(string transcript)
      {
        Func<string, bool> consumeTutorialTranscript = DictationHooks.ConsumeTutorialTranscript;
        return consumeTutorialTranscript != null && consumeTutorialTranscript(transcript);
      };
      dictationControllerOptions.CanStartRecording = () => CS$<>8__locals1.companion.State != VoiceState.Listening;
      dictationControllerOptions.IsMicrophoneDenied = () => CS$<>8__locals1.permissions.GetStatus(PermissionKind.Microphone) == PermissionStatus.Denied;
      Action<string, float> action;
      if (!(Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1"))
      {
        action = delegate(string name, float volume)
        {
          CS$<>8__locals1.audio.PlayEarcon(name, volume);
        };
      }
      else
      {
        action = delegate(string _, float _)
        {
        };
      }
      dictationControllerOptions.PlayCue = action;
      Action<string, float> action2;
      if (!(Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1"))
      {
        action2 = delegate(string name, float volume)
        {
          CS$<>8__locals1.microphoneStartCue.Play(name, new float?(volume));
        };
      }
      else
      {
        action2 = delegate(string _, float _)
        {
        };
      }
      dictationControllerOptions.PlayMicrophoneStartCue = action2;
      DictationController dictationController = new DictationController(http, auth, settings, microphoneCapture3, dictationTextTarget, notch, dictationControllerOptions);
      DictationHooks.RemoveLearnedWord = new Action<string>(dictationController.RemoveLearnedWord);
      double num3 = CS$<>8__locals1.<Complete>g__Lap|0();
      this.Microphone = microphoneCapture;
      this.Audio = CS$<>8__locals1.audio;
      this.MicrophoneStartCue = CS$<>8__locals1.microphoneStartCue;
      this.Capture = CS$<>8__locals1.capture;
      this.Cursor = CS$<>8__locals1.cursor;
      this.Highlight = deferredHighlightOverlay;
      this.Canvas = screenDrawingCanvas;
      this.Agents = CS$<>8__locals1.agents;
      this.Companion = CS$<>8__locals1.companion;
      this.Dictation = dictationController;
      this.Shortcuts = new ShortcutMonitor();
      CS$<>8__locals1.companion.StateChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        VoiceState state = CS$<>8__locals1.companion.State;
        bool flag = state - VoiceState.Listening <= 1;
        if (flag)
        {
          ScreenDrawingCanvas.WarmUpFigures();
        }
      };
      this.BrainContext = CS$<>8__locals1.brainContext;
      this.Tools = companionTools;
      this.AgentVoice = new AgentVoiceRelay(CS$<>8__locals1.agents, CS$<>8__locals1.core, CS$<>8__locals1.companion, CS$<>8__locals1.brainContext);
      this.WireJobsAndHarness(CS$<>8__locals1.core, CS$<>8__locals1.agents, CS$<>8__locals1.companion, CS$<>8__locals1.brainContext, CS$<>8__locals1.audio);
      CS$<>8__locals1.agents.TaskEvent += delegate([Nullable(2)] object _, AgentTaskEvent e)
      {
        AgentTaskEventKind kind = e.Kind;
        bool flag2 = kind - AgentTaskEventKind.Finished <= 1;
        if (flag2)
        {
          CS$<>8__locals1.brainContext.Refresh();
        }
      };
      FlowHooks.SuppressDocumentAccessNotice = delegate
      {
        CS$<>8__locals1.core.Settings.Update(delegate(AppSettings s)
        {
          s.DocumentAccessNoticeSuppressed = true;
        });
      };
      ForegroundTracker.Start();
      Action<IntPtr> action3;
      if ((action3 = RealStack.<>O.<5>__NoteForegroundWindow) == null)
      {
        action3 = (RealStack.<>O.<5>__NoteForegroundWindow = new Action<IntPtr>(ScreenCaptureYield.NoteForegroundWindow));
      }
      ForegroundTracker.ExternalWindowCameForward += action3;
      if (CS$<>8__locals1.core.Auth.IsSignedIn)
      {
        CS$<>8__locals1.brainContext.Refresh();
      }
      string text = "startup";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 3);
      defaultInterpolatedStringHandler.AppendLiteral("real services: audio+capture ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms, canvas ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num2, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms, agents+companion ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(num3, "0");
      defaultInterpolatedStringHandler.AppendLiteral(" ms");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06000699 RID: 1689 RVA: 0x005316F4 File Offset: 0x005316F4
    public void StartBackground(string cursorColorHex, bool showCursor = true)
    {
      if (!AppPaths.IsOverridden)
      {
        Task.Run(delegate
        {
          try
          {
            ProtocolRegistration.Register(null);
          }
          catch (Exception ex)
          {
            Log.Warn("startup", "protocol registration: " + ex.Message);
          }
        });
      }
      else
      {
        Log.Info("startup", "clicky:// and Open-with registration skipped: the data folder is moved (a test run)");
      }
      new Thread(delegate
      {
        if (OverlayWindow.Offscreen)
        {
          Log.Info("startup", "offscreen run: cursor and highlight overlays skipped");
          return;
        }
        Stopwatch stopwatch = Stopwatch.StartNew();
        try
        {
          CursorOverlay cursorOverlay = new CursorOverlay();
          cursorOverlay.SetColor(cursorColorHex);
          this.Cursor.Attach(cursorOverlay);
          if (showCursor)
          {
            this.Cursor.Show();
          }
          this.Highlight.Attach(new HighlightOverlay());
          string text = "startup";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
          defaultInterpolatedStringHandler.AppendLiteral("overlays ready in ");
          defaultInterpolatedStringHandler.AppendFormatted<long>(stopwatch.ElapsedMilliseconds);
          defaultInterpolatedStringHandler.AppendLiteral(" ms");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
        catch (Exception ex2)
        {
          Log.Error("startup", "overlays failed", ex2);
        }
      })
      {
        IsBackground = true,
        Name = "OverlayStartup"
      }.Start();
      this.Core.StartAsync(default(CancellationToken));
      if (this.Core.Auth.IsSignedIn)
      {
        this.Agents.PrewarmAsync(default(CancellationToken));
      }
      if (!OverlayWindow.Offscreen)
      {
        this.Shortcuts.Start();
      }
    }

    // Token: 0x0600069A RID: 1690 RVA: 0x005317D0 File Offset: 0x005317D0
    public async ValueTask DisposeAsync()
    {
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        ShortcutMonitor shortcuts = this.Shortcuts;
        if (shortcuts == null)
        {
          return;
        }
        shortcuts.Dispose();
      }, "shortcuts");
      await RealStack.<DisposeAsync>g__TryAsync|99_1(this.Dictation, "dictation");
      await RealStack.<DisposeAsync>g__TryAsync|99_1(this.Companion, "companion");
      await RealStack.<DisposeAsync>g__TryAsync|99_1(this.Agents, "agents");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        ScreenDrawingCanvas canvas = this.Canvas;
        if (canvas == null)
        {
          return;
        }
        canvas.Dispose();
      }, "canvas");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        DeferredHighlightOverlay highlight = this.Highlight;
        if (highlight == null)
        {
          return;
        }
        highlight.Dispose();
      }, "highlight");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        DeferredCursorOverlay cursor = this.Cursor;
        if (cursor == null)
        {
          return;
        }
        cursor.Dispose();
      }, "cursor");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        ScreenCaptureService capture = this.Capture;
        if (capture == null)
        {
          return;
        }
        capture.Dispose();
      }, "capture");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        MicrophoneStartCue microphoneStartCue = this.MicrophoneStartCue;
        if (microphoneStartCue == null)
        {
          return;
        }
        microphoneStartCue.Dispose();
      }, "microphone start cue");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        WasapiAudioOutput audio = this.Audio;
        if (audio == null)
        {
          return;
        }
        audio.Dispose();
      }, "audio");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        IMicrophoneCapture microphone = this.Microphone;
        if (microphone == null)
        {
          return;
        }
        microphone.Dispose();
      }, "microphone");
      RealStack.<DisposeAsync>g__Try|99_0(delegate
      {
        PermissionService permissions = this.Permissions;
        if (permissions == null)
        {
          return;
        }
        permissions.Dispose();
      }, "permissions");
      await RealStack.<DisposeAsync>g__TryAsync|99_1(this.Core, "core");
    }

    // Token: 0x0600069B RID: 1691 RVA: 0x00531814 File Offset: 0x00531814
    private void WireJobsAndHarness(CoreServices core, AgentService agents, CompanionSession companion, BrainContext brainContext, WasapiAudioOutput audio)
    {
      RealStack.<>c__DisplayClass100_0 CS$<>8__locals1 = new RealStack.<>c__DisplayClass100_0();
      CS$<>8__locals1.companion = companion;
      CS$<>8__locals1.core = core;
      CS$<>8__locals1.audio = audio;
      CS$<>8__locals1.agents = agents;
      CS$<>8__locals1.muted = Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1";
      this.JobReports = new OneBrainJobReports(CS$<>8__locals1.agents, CS$<>8__locals1.companion)
      {
        Speak = (string line) => CS$<>8__locals1.companion.AnnounceAsync(line, CS$<>8__locals1.core.Settings.Read<bool>((AppSettings s) => s.AgentCursorTextAnnouncementsEnabled) && !HeyClickyWindow.KeepsAgentLinesOffCursor, default(CancellationToken)),
        PlayDoneChime = delegate
        {
          if (!CS$<>8__locals1.muted)
          {
            CS$<>8__locals1.audio.PlayEarcon("agentDone", 0.7f);
          }
        }
      };
      this.AgentVoice.JobReports = this.JobReports;
      AgentToolHooks.ShowResult = new Func<string, string>(this.JobReports.ShowResult);
      AgentToolHooks.Routines = delegate(string argumentsJson)
      {
        RealStack.<>c__DisplayClass100_0.<<WireJobsAndHarness>b__0>d <<WireJobsAndHarness>b__0>d;
        <<WireJobsAndHarness>b__0>d.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
        <<WireJobsAndHarness>b__0>d.<>4__this = CS$<>8__locals1;
        <<WireJobsAndHarness>b__0>d.argumentsJson = argumentsJson;
        <<WireJobsAndHarness>b__0>d.<>1__state = -1;
        <<WireJobsAndHarness>b__0>d.<>t__builder.Start<RealStack.<>c__DisplayClass100_0.<<WireJobsAndHarness>b__0>d>(ref <<WireJobsAndHarness>b__0>d);
        return <<WireJobsAndHarness>b__0>d.<>t__builder.Task;
      };
      AgentToolHooks.JobIdForThread = new Func<string, string>(CS$<>8__locals1.agents.OneBrainJobId);
      CS$<>8__locals1.agents.StepWatcher.FrontmostBrowserAddress = delegate
      {
        string text;
        try
        {
          ForegroundApp result = ForegroundAppInfo.CaptureUserAppAsync(true, default(CancellationToken)).WaitAsync(TimeSpan.FromSeconds(2L)).GetAwaiter()
            .GetResult();
          text = ((result != null && ForegroundAppInfo.IsBrowser(result.ProcessName)) ? result.BrowserUrl : null);
        }
        catch (Exception)
        {
          text = null;
        }
        return text;
      };
      brainContext.JobsNote = () => CS$<>8__locals1.agents.WorkContextNoteForBrainTurn(ProactiveHooks.LiveCardIds());
      CS$<>8__locals1.agents.HarnessTurnEnded += delegate(string harness, string place, string status, double seconds, IReadOnlyDictionary<string, double> stats)
      {
        ClickyAnalytics.TrackAgentHarnessTurn(harness, place, status, seconds, stats);
      };
      CS$<>8__locals1.agents.InternalError += delegate(string subsystem, string stage, string detail, string key)
      {
        ClickyAnalytics.ReportInternalError(subsystem, stage, null, detail, CrashReportLevel.Error, key, null);
      };
      CS$<>8__locals1.agents.JobAction += delegate(string _, [Nullable(2)] string threadId, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> properties)
      {
        ClickyAnalytics.TrackProductEvent("job_action", null, threadId, RealStack.Json(properties));
      };
    }

    // Token: 0x0600069C RID: 1692 RVA: 0x005319B0 File Offset: 0x005319B0
    private static JsonObject Json([Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> properties)
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
      return jsonObject;
    }

    // Token: 0x0600069E RID: 1694 RVA: 0x00531B14 File Offset: 0x00531B14
    [CompilerGenerated]
    internal static void <DisposeAsync>g__Try|99_0(Action a, string what)
    {
      Log.Info("shutdown", "stopping " + what);
      try
      {
        a();
      }
      catch (Exception ex)
      {
        Log.Warn("shutdown", what + ": " + ex.Message);
      }
    }

    // Token: 0x0600069F RID: 1695 RVA: 0x00531B6C File Offset: 0x00531B6C
    [CompilerGenerated]
    internal static async Task <DisposeAsync>g__TryAsync|99_1([Nullable(2)] IAsyncDisposable service, string what)
    {
      Log.Info("shutdown", "stopping " + what);
      try
      {
        if (service != null)
        {
          await service.DisposeAsync();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("shutdown", what + ": " + ex.Message);
      }
    }

    // Token: 0x040001A3 RID: 419
    [TupleElementNames(new string[] { "Tags", "ReadAtTicks" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<IReadOnlyList<string>, long>? cachedEarsSystemLanguages;

    // Token: 0x0200043D RID: 1085
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001540 RID: 5440
      [Nullable(new byte[] { 0, 1 })]
      public static Func<string> <0>__IanaTimeZone;

      // Token: 0x04001541 RID: 5441
      [Nullable(new byte[] { 0, 1 })]
      public static Func<string, bool> <1>__OpenUri;

      // Token: 0x04001542 RID: 5442
      [Nullable(0)]
      public static Func<bool> <2>__SpotifyAppInstalled;

      // Token: 0x04001543 RID: 5443
      [Nullable(new byte[] { 0, 2 })]
      public static Func<string> <3>__CurrentSilenceKind;

      // Token: 0x04001544 RID: 5444
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<IReadOnlyList<string>> <4>__SystemLanguageTagsForEars;

      // Token: 0x04001545 RID: 5445
      [Nullable(0)]
      public static Action<IntPtr> <5>__NoteForegroundWindow;
    }
  }
}
