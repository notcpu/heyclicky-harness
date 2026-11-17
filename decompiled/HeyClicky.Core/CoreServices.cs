using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Brain;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Dictation;
using HeyClicky.Core.Identity;
using HeyClicky.Core.Net;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core
{
  // Token: 0x02000036 RID: 54
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CoreServices : IAsyncDisposable
  {
    // Token: 0x1700004E RID: 78
    // (get) Token: 0x060001B0 RID: 432 RVA: 0x0034EF74 File Offset: 0x0034EF74
    public ClickyEndpoints Endpoints { get; }

    // Token: 0x1700004F RID: 79
    // (get) Token: 0x060001B1 RID: 433 RVA: 0x0034EF7C File Offset: 0x0034EF7C
    public ClientIdentity Identity { get; }

    // Token: 0x17000050 RID: 80
    // (get) Token: 0x060001B2 RID: 434 RVA: 0x0034EF84 File Offset: 0x0034EF84
    public ClickyHttp Http { get; }

    // Token: 0x17000051 RID: 81
    // (get) Token: 0x060001B3 RID: 435 RVA: 0x0034EF8C File Offset: 0x0034EF8C
    public SettingsStore Settings { get; }

    // Token: 0x17000052 RID: 82
    // (get) Token: 0x060001B4 RID: 436 RVA: 0x0034EF94 File Offset: 0x0034EF94
    public SupabaseAuthService Auth { get; }

    // Token: 0x17000053 RID: 83
    // (get) Token: 0x060001B5 RID: 437 RVA: 0x0034EF9C File Offset: 0x0034EF9C
    public WorkerClient Worker { get; }

    // Token: 0x17000054 RID: 84
    // (get) Token: 0x060001B6 RID: 438 RVA: 0x0034EFA4 File Offset: 0x0034EFA4
    public BrainClient BrainClient { get; }

    // Token: 0x17000055 RID: 85
    // (get) Token: 0x060001B7 RID: 439 RVA: 0x0034EFAC File Offset: 0x0034EFAC
    public BrainSession BrainSession { get; }

    // Token: 0x17000056 RID: 86
    // (get) Token: 0x060001B8 RID: 440 RVA: 0x0034EFB4 File Offset: 0x0034EFB4
    public NotchPresenter Notch { get; }

    // Token: 0x17000057 RID: 87
    // (get) Token: 0x060001B9 RID: 441 RVA: 0x0034EFBC File Offset: 0x0034EFBC
    public BillingStateService Billing { get; }

    // Token: 0x060001BA RID: 442 RVA: 0x0034EFC4 File Offset: 0x0034EFC4
    private CoreServices(ClickyEndpoints endpoints, ClientIdentity identity, [Nullable(2)] SettingsStore settings, [Nullable(new byte[] { 2, 1 })] Func<Uri, bool> openBrowser)
    {
      this.Endpoints = endpoints;
      this.Identity = identity;
      this.Http = new ClickyHttp(endpoints, identity);
      this.Settings = settings ?? new SettingsStore(null);
      ClickyAnalytics.Configure(TelemetryConfiguration.Current, endpoints, identity, this.Http.Client, null, null);
      this.Auth = new SupabaseAuthService(this.Http, null, openBrowser);
      this.Worker = new WorkerClient(this.Http, this.Auth);
      ClickyAnalytics.ErrorRelay = new Action<ErrorReport>(this.Worker.ReportError);
      ClickyAnalytics.CurrentAccount = delegate
      {
        string userId = this.Auth.UserId;
        UserProfile user = this.Auth.User;
        return new ValueTuple<string, string>(userId, (user != null) ? user.Email : null);
      };
      this.BrainClient = new BrainClient(this.Http);
      this.BrainSession = new BrainSession(this.BrainClient, (CancellationToken ct) => this.Auth.GetValidAccessTokenAsync(ct), null, null);
      this.Notch = new NotchPresenter(null, true);
      this.Billing = new BillingStateService(this.Worker, this.Auth, null);
      this.Notch.SetSignedIn(this.Auth.IsSignedIn);
      this.Auth.Changed += delegate([Nullable(2)] object _, EventArgs _)
      {
        this.Notch.SetSignedIn(this.Auth.IsSignedIn);
        if (this.Auth.IsSignedIn)
        {
          this.BrainSession.AdoptAccount(this.Auth.UserId);
        }
        else
        {
          this.BrainSession.ResetForSignOut();
        }
        if (this.Auth.IsSignedIn)
        {
          DictationVocabulary.AddBuiltInTermsIfNeeded(this.Settings);
        }
        if (this.Auth.IsSignedIn)
        {
          UserProfile user2 = this.Auth.User;
          string text = ((user2 != null) ? user2.DisplayName : null);
          if (text != null)
          {
            DictationVocabulary.AddAccountNameIfNeeded(this.Settings, text);
          }
        }
      };
    }

    // Token: 0x060001BB RID: 443 RVA: 0x0034F0F0 File Offset: 0x0034F0F0
    [NullableContext(2)]
    [return: Nullable(1)]
    public static CoreServices Create(ClickyEndpoints endpoints = null, ClientIdentity identity = null, SettingsStore settings = null, [Nullable(new byte[] { 2, 1 })] Func<Uri, bool> openBrowser = null)
    {
      return new CoreServices(endpoints ?? AppBundleConfiguration.Load(null), identity ?? ClickyClientIdentity.Current, settings, openBrowser);
    }

    // Token: 0x060001BC RID: 444 RVA: 0x0034F110 File Offset: 0x0034F110
    public CompanionSession CreateCompanionSession(IMicrophoneCapture microphone, IAudioOutput audio, IScreenCaptureService screens, IScreenDrawingCanvas canvas, ICursorOverlay cursor, [Nullable(2)] IHighlightOverlay highlight, [Nullable(2)] CompanionSessionOptions options = null)
    {
      return new CompanionSession(this.Http, this.Auth, this.BrainClient, this.BrainSession, this.Settings, microphone, audio, screens, canvas, cursor, highlight, this.Notch, options);
    }

    // Token: 0x060001BD RID: 445 RVA: 0x0034F154 File Offset: 0x0034F154
    public async Task StartAsync(CancellationToken ct = default(CancellationToken))
    {
      this.Http.PrewarmAll();
      if (this.Auth.IsSignedIn)
      {
        this.BrainSession.AdoptAccount(this.Auth.UserId);
        try
        {
          await this.Worker.RegisterThisDeviceIfNeededAsync(ct).ConfigureAwait(false);
          await PreSignInIntroViewModel.FlushPendingDiscoveryChannelAsync(this.Settings, (string channel, [Nullable(2)] string other) => this.Worker.SetDiscoveryChannelAsync(channel, other, ct)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Warn("Core", "launch work: " + ex.Message);
        }
      }
    }

    // Token: 0x060001BE RID: 446 RVA: 0x0034F1A0 File Offset: 0x0034F1A0
    public ValueTask DisposeAsync()
    {
      this.Billing.Dispose();
      this.Notch.Dispose();
      this.BrainSession.Dispose();
      this.Settings.Dispose();
      this.Http.Dispose();
      return ValueTask.CompletedTask;
    }
  }
}
