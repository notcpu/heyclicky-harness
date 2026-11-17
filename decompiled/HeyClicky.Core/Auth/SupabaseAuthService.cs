using System;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Net;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Auth
{
  // Token: 0x0200022C RID: 556
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SupabaseAuthService : IAuthService
  {
    // Token: 0x14000041 RID: 65
    // (add) Token: 0x0600205F RID: 8287 RVA: 0x003F526C File Offset: 0x003F526C
    // (remove) Token: 0x06002060 RID: 8288 RVA: 0x003F52A4 File Offset: 0x003F52A4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x14000042 RID: 66
    // (add) Token: 0x06002061 RID: 8289 RVA: 0x003F52DC File Offset: 0x003F52DC
    // (remove) Token: 0x06002062 RID: 8290 RVA: 0x003F5314 File Offset: 0x003F5314
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler SignInErrorChanged;

    // Token: 0x14000043 RID: 67
    // (add) Token: 0x06002063 RID: 8291 RVA: 0x003F534C File Offset: 0x003F534C
    // (remove) Token: 0x06002064 RID: 8292 RVA: 0x003F5384 File Offset: 0x003F5384
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> ReferralCodeReceived;

    // Token: 0x06002065 RID: 8293 RVA: 0x003F53BC File Offset: 0x003F53BC
    public SupabaseAuthService(ClickyHttp http, [Nullable(2)] AuthTokenStore store = null, [Nullable(new byte[] { 2, 1 })] Func<Uri, bool> openBrowser = null)
    {
      this.http = http;
      this.store = store ?? new AuthTokenStore(AppBundleConfiguration.AppDataDirectory, AppBundleConfiguration.IsLocalUrl(http.Endpoints.SupabaseUrl));
      Func<Uri, bool> func = openBrowser;
      if (openBrowser == null && (func = SupabaseAuthService.<>O.<0>__OpenInDefaultBrowser) == null)
      {
        func = (SupabaseAuthService.<>O.<0>__OpenInDefaultBrowser = new Func<Uri, bool>(SupabaseAuthService.OpenInDefaultBrowser));
      }
      this.openBrowser = func;
      this.RestoreSession();
    }

    // Token: 0x1700074A RID: 1866
    // (get) Token: 0x06002066 RID: 8294 RVA: 0x003F5440 File Offset: 0x003F5440
    public bool IsSignedIn
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.accessToken != null;
        }
        return flag2;
      }
    }

    // Token: 0x1700074B RID: 1867
    // (get) Token: 0x06002067 RID: 8295 RVA: 0x003F5488 File Offset: 0x003F5488
    [Nullable(2)]
    public string LastSignInErrorMessage
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.lastSignInErrorMessage;
        }
        return text;
      }
    }

    // Token: 0x1700074C RID: 1868
    // (get) Token: 0x06002068 RID: 8296 RVA: 0x003F54CC File Offset: 0x003F54CC
    [Nullable(2)]
    public UserProfile User
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        UserProfile userProfile;
        lock (obj)
        {
          userProfile = this.user;
        }
        return userProfile;
      }
    }

    // Token: 0x1700074D RID: 1869
    // (get) Token: 0x06002069 RID: 8297 RVA: 0x003F5510 File Offset: 0x003F5510
    [Nullable(2)]
    public string UserId
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          UserProfile userProfile = this.user;
          text = ((userProfile != null) ? userProfile.UserId : null);
        }
        return text;
      }
    }

    // Token: 0x1700074E RID: 1870
    // (get) Token: 0x0600206A RID: 8298 RVA: 0x003F5560 File Offset: 0x003F5560
    public DateTimeOffset? SignupDate
    {
      get
      {
        object obj = this.gate;
        DateTimeOffset? dateTimeOffset2;
        lock (obj)
        {
          double? num = this.signupDateEpochSeconds;
          DateTimeOffset? dateTimeOffset;
          if (num != null)
          {
            double valueOrDefault = num.GetValueOrDefault();
            dateTimeOffset = new DateTimeOffset?(DateTimeOffset.FromUnixTimeMilliseconds((long)(valueOrDefault * 1000.0)));
          }
          else
          {
            dateTimeOffset2 = null;
            dateTimeOffset = dateTimeOffset2;
          }
          dateTimeOffset2 = dateTimeOffset;
        }
        return dateTimeOffset2;
      }
    }

    // Token: 0x1700074F RID: 1871
    // (get) Token: 0x0600206B RID: 8299 RVA: 0x003F55DC File Offset: 0x003F55DC
    public bool SupportsPasswordSignIn
    {
      get
      {
        return AppBundleConfiguration.IsLocalUrl(this.http.Endpoints.SupabaseUrl);
      }
    }

    // Token: 0x0600206C RID: 8300 RVA: 0x003F55F4 File Offset: 0x003F55F4
    public Uri GoogleSignInStartUrl(string state)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
      defaultInterpolatedStringHandler.AppendFormatted<Uri>(new Uri(this.http.Endpoints.WorkerBaseUrl, "/auth/start"));
      defaultInterpolatedStringHandler.AppendLiteral("?state=");
      defaultInterpolatedStringHandler.AppendFormatted(Uri.EscapeDataString(state));
      return new Uri(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600206D RID: 8301 RVA: 0x003F5650 File Offset: 0x003F5650
    public async Task<bool> SignInWithGoogleAsync(CancellationToken ct = default(CancellationToken))
    {
      SupabaseAuthService.<>c__DisplayClass45_0 CS$<>8__locals1 = new SupabaseAuthService.<>c__DisplayClass45_0();
      CS$<>8__locals1.completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
      string text = SignInCallbackGuard.MakeState();
      object obj = this.gate;
      TaskCompletionSource<bool> taskCompletionSource;
      lock (obj)
      {
        taskCompletionSource = this.pendingGoogleSignIn;
        this.pendingGoogleSignIn = CS$<>8__locals1.completion;
      }
      if (taskCompletionSource != null)
      {
        taskCompletionSource.TrySetResult(false);
      }
      Uri uri = this.GoogleSignInStartUrl(text);
      this.SavePendingSignIn(text, DateTimeOffset.UtcNow);
      Log.Info("Auth", "Starting Google sign-in flow in default browser");
      this.SetLastSignInErrorMessage(null);
      bool flag2;
      if (!this.openBrowser(uri))
      {
        Log.Error("Auth", "could not open the browser for sign-in", null);
        this.EndPendingGoogleSignIn(CS$<>8__locals1.completion, true);
        flag2 = false;
      }
      else
      {
        using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          timeout.CancelAfter(SignInCallbackGuard.PendingSignInLifetime);
          CancellationTokenRegistration cancellationTokenRegistration = timeout.Token.Register(delegate
          {
            CS$<>8__locals1.completion.TrySetResult(false);
          });
          object obj2 = null;
          int num = 0;
          bool flag4;
          try
          {
            bool flag3 = await CS$<>8__locals1.completion.Task.ConfigureAwait(false);
            this.EndPendingGoogleSignIn(CS$<>8__locals1.completion, timeout.IsCancellationRequested);
            flag4 = flag3;
            num = 1;
          }
          catch (object obj)
          {
            obj2 = obj;
          }
          await cancellationTokenRegistration.DisposeAsync();
          obj = obj2;
          if (obj != null)
          {
            Exception ex = obj as Exception;
            if (ex == null)
            {
              throw obj;
            }
            ExceptionDispatchInfo.Capture(ex).Throw();
          }
          if (num == 1)
          {
            return flag4;
          }
          obj2 = null;
          cancellationTokenRegistration = default(CancellationTokenRegistration);
        }
        CS$<>8__locals1 = null;
        CancellationTokenSource timeout = null;
      }
      return flag2;
    }

    // Token: 0x0600206E RID: 8302 RVA: 0x003F569C File Offset: 0x003F569C
    private void EndPendingGoogleSignIn(TaskCompletionSource<bool> completion, bool spendSignInState)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.pendingGoogleSignIn == completion;
        if (flag2)
        {
          this.pendingGoogleSignIn = null;
        }
      }
      if (flag2 && spendSignInState)
      {
        this.ClearPendingSignIn();
      }
    }

    // Token: 0x0600206F RID: 8303 RVA: 0x003F56F8 File Offset: 0x003F56F8
    public async Task<bool> HandleDeepLinkAsync(Uri uri)
    {
      bool flag;
      if (!AuthDeepLink.IsAuthCallback(uri))
      {
        flag = false;
      }
      else
      {
        Log.Info("Auth", "Received auth callback");
        AuthCallback authCallback = AuthDeepLink.Parse(uri);
        if (authCallback == null)
        {
          Log.Error("Auth", "No access_token in callback URL", null);
          this.NoteSignInCallbackFailed(AuthDeepLink.ParseError(uri) ?? new AuthCallbackError(null, null, null));
          this.CompletePendingGoogleSignIn(false);
          flag = true;
        }
        else
        {
          bool isSignedIn = this.IsSignedIn;
          SignInCallbackGuard.Verdict verdict = this.DecideCallback(authCallback.State, DateTimeOffset.UtcNow);
          if (!SignInCallbackGuard.AcceptsCallback(verdict))
          {
            string text = SignInCallbackGuard.AnalyticsReason(verdict);
            Log.Warn("Auth", "Refused an auth callback: " + text);
            ClickyAnalytics.TrackAuthCallbackRefused(text, isSignedIn);
            this.CompletePendingGoogleSignIn(false);
            flag = false;
          }
          else
          {
            this.SetLastSignInErrorMessage(null);
            this.AdoptFreshSession(authCallback.AccessToken, authCallback.RefreshToken);
            string referralCode = authCallback.ReferralCode;
            if (referralCode != null)
            {
              EventHandler<string> referralCodeReceived = this.ReferralCodeReceived;
              if (referralCodeReceived != null)
              {
                referralCodeReceived(this, referralCode);
              }
            }
            await this.FetchUserProfileAsync(true, CancellationToken.None, true).ConfigureAwait(false);
            this.CompletePendingGoogleSignIn(true);
            flag = true;
          }
        }
      }
      return flag;
    }

    // Token: 0x06002070 RID: 8304 RVA: 0x003F5744 File Offset: 0x003F5744
    public async Task<bool> SignInWithPasswordAsync(string email, string password, CancellationToken ct = default(CancellationToken))
    {
      bool flag;
      if (!this.SupportsPasswordSignIn)
      {
        Log.Warn("Auth", "password sign-in refused: Supabase is not a local address");
        flag = false;
      }
      else
      {
        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.SupabaseUrl("/auth/v1/token?grant_type=password")))
        {
          request.Headers.TryAddWithoutValidation("apikey", this.http.Endpoints.SupabaseAnonKey);
          request.Content = SupabaseAuthService.JsonBody(delegate(Utf8JsonWriter writer)
          {
            writer.WriteString("email", email);
            writer.WriteString("password", password);
          });
          HttpResponseMessage httpResponseMessage = await this.http.Client.SendAsync(request, ct).ConfigureAwait(false);
          using (HttpResponseMessage response = httpResponseMessage)
          {
            byte[] array = await response.Content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.OK)
            {
              string text = "Auth";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
              defaultInterpolatedStringHandler.AppendLiteral("password sign-in failed: HTTP ");
              defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
              defaultInterpolatedStringHandler.AppendLiteral(" ");
              defaultInterpolatedStringHandler.AppendFormatted(Encoding.UTF8.GetString(array.AsSpan<byte>(0, Math.Min(200, array.Length))));
              Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
              flag = false;
            }
            else
            {
              using (JsonDocument document = JsonDocument.Parse(array, default(JsonDocumentOptions)))
              {
                JsonElement rootElement = document.RootElement;
                JsonElement jsonElement;
                string text2 = (rootElement.TryGetProperty("access_token", out jsonElement) ? jsonElement.GetString() : null);
                if (string.IsNullOrEmpty(text2))
                {
                  flag = false;
                }
                else
                {
                  JsonElement jsonElement2;
                  this.AdoptFreshSession(text2, rootElement.TryGetProperty("refresh_token", out jsonElement2) ? jsonElement2.GetString() : null);
                  JsonElement jsonElement3;
                  if (rootElement.TryGetProperty("user", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.Object)
                  {
                    this.ApplyProfile(jsonElement3);
                  }
                  else
                  {
                    await this.FetchUserProfileAsync(true, ct, true).ConfigureAwait(false);
                  }
                  flag = true;
                }
              }
            }
          }
        }
      }
      return flag;
    }

    // Token: 0x06002071 RID: 8305 RVA: 0x003F57A0 File Offset: 0x003F57A0
    private void NoteSignInCallbackFailed(AuthCallbackError error)
    {
      if (this.IsSignedIn)
      {
        Log.Info("Auth", "ignoring a failed sign-in callback while already signed in");
        return;
      }
      string userFacingMessage = error.UserFacingMessage;
      Log.Warn("Auth", "sign-in callback failed: " + error.DiagnosticMessage);
      this.SetLastSignInErrorMessage(userFacingMessage);
      ClickyAnalytics.TrackAppError("auth_callback", "generic", error.DiagnosticMessage, error.ReportedErrorDomain, 0, userFacingMessage, true);
    }

    // Token: 0x06002072 RID: 8306 RVA: 0x003F580C File Offset: 0x003F580C
    [NullableContext(2)]
    private void SetLastSignInErrorMessage(string message)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.lastSignInErrorMessage == message)
        {
          return;
        }
        this.lastSignInErrorMessage = message;
      }
      try
      {
        EventHandler signInErrorChanged = this.SignInErrorChanged;
        if (signInErrorChanged != null)
        {
          signInErrorChanged(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Auth", "SignInErrorChanged handler threw: " + ex.Message);
      }
    }

    // Token: 0x06002073 RID: 8307 RVA: 0x003F58A0 File Offset: 0x003F58A0
    private void CompletePendingGoogleSignIn(bool result)
    {
      object obj = this.gate;
      TaskCompletionSource<bool> taskCompletionSource;
      lock (obj)
      {
        taskCompletionSource = this.pendingGoogleSignIn;
      }
      if (taskCompletionSource != null)
      {
        taskCompletionSource.TrySetResult(result);
      }
    }

    // Token: 0x06002074 RID: 8308 RVA: 0x003F58EC File Offset: 0x003F58EC
    private void AdoptFreshSession(string newAccessToken, [Nullable(2)] string newRefreshToken)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.accessToken = newAccessToken;
        this.refreshToken = newRefreshToken;
        this.lastRefreshFailureAt = null;
        long num;
        string text;
        string text2;
        JwtPayload.TryRead(newAccessToken, out num, out text, out text2);
        if (!string.IsNullOrEmpty(text))
        {
          UserProfile userProfile = this.user;
          if (((userProfile != null) ? userProfile.UserId : null) != text)
          {
            this.user = new UserProfile(text, text2 ?? "", null, null);
          }
        }
      }
      this.SaveSession();
      this.RaiseChanged();
    }

    // Token: 0x06002075 RID: 8309 RVA: 0x003F5994 File Offset: 0x003F5994
    public Task SignOutAsync()
    {
      Log.Info("Auth", "Signing out");
      object obj = this.gate;
      TaskCompletionSource<bool> taskCompletionSource;
      lock (obj)
      {
        this.accessToken = null;
        this.refreshToken = null;
        this.user = null;
        this.signupDateEpochSeconds = null;
        this.refreshTask = null;
        taskCompletionSource = this.pendingGoogleSignIn;
        this.pendingGoogleSignIn = null;
      }
      if (taskCompletionSource != null)
      {
        taskCompletionSource.TrySetResult(false);
      }
      this.store.Delete();
      ClickyAnalytics.ResetIdentity();
      this.RaiseChanged();
      return Task.CompletedTask;
    }

    // Token: 0x06002076 RID: 8310 RVA: 0x003F5A3C File Offset: 0x003F5A3C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> GetValidAccessTokenAsync(CancellationToken ct = default(CancellationToken))
    {
      return await this.GetValidAccessTokenAsync(false, ct).ConfigureAwait(false);
    }

    // Token: 0x06002077 RID: 8311 RVA: 0x003F5A88 File Offset: 0x003F5A88
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<string> GetValidAccessTokenAsync(bool forceRefresh, CancellationToken ct = default(CancellationToken))
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        text = this.accessToken;
      }
      string text2;
      if (string.IsNullOrEmpty(text))
      {
        text2 = null;
      }
      else
      {
        if (forceRefresh || SupabaseAuthService.ShouldRefreshBeforeRequest(text))
        {
          if (forceRefresh || SupabaseAuthService.IsExpired(text) || !this.IsWithinRefreshCooldown())
          {
            bool flag2 = await this.RefreshAccessTokenAsync().WaitAsync(ct).ConfigureAwait(false);
            obj = this.gate;
            lock (obj)
            {
              text = this.accessToken;
            }
            if (!flag2 && text != null && SupabaseAuthService.IsExpired(text))
            {
              return null;
            }
          }
        }
        else if (this.ShouldStartProactiveRefresh(text))
        {
          this.RefreshAccessTokenAsync();
        }
        obj = this.gate;
        lock (obj)
        {
          text2 = (string.IsNullOrEmpty(this.accessToken) ? null : this.accessToken);
        }
      }
      return text2;
    }

    // Token: 0x06002078 RID: 8312 RVA: 0x003F5ADC File Offset: 0x003F5ADC
    private static bool ShouldRefreshBeforeRequest(string token)
    {
      long num;
      string text;
      string text2;
      return JwtPayload.TryRead(token, out num, out text, out text2) && num > 0L && DateTimeOffset.FromUnixTimeSeconds(num) - DateTimeOffset.UtcNow <= SupabaseAuthService.RefreshLeadTime;
    }

    // Token: 0x06002079 RID: 8313 RVA: 0x003F5B18 File Offset: 0x003F5B18
    private static bool IsExpired(string token)
    {
      long num;
      string text;
      string text2;
      return JwtPayload.TryRead(token, out num, out text, out text2) && num > 0L && DateTimeOffset.FromUnixTimeSeconds(num) - DateTimeOffset.UtcNow <= SupabaseAuthService.ExpiryLeeway;
    }

    // Token: 0x0600207A RID: 8314 RVA: 0x003F5B54 File Offset: 0x003F5B54
    private bool ShouldStartProactiveRefresh(string token)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.refreshTask != null)
        {
          return false;
        }
      }
      long num;
      string text;
      string text2;
      return JwtPayload.TryRead(token, out num, out text, out text2) && num > 0L && DateTimeOffset.FromUnixTimeSeconds(num) - DateTimeOffset.UtcNow <= SupabaseAuthService.ProactiveRefreshLeadTime && !this.IsWithinRefreshCooldown();
    }

    // Token: 0x0600207B RID: 8315 RVA: 0x003F5BD8 File Offset: 0x003F5BD8
    private bool IsWithinRefreshCooldown()
    {
      object obj = this.gate;
      bool flag3;
      lock (obj)
      {
        DateTimeOffset? dateTimeOffset = this.lastRefreshFailureAt;
        bool flag2;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          flag2 = DateTimeOffset.UtcNow - valueOrDefault < SupabaseAuthService.RefreshRetryCooldown;
        }
        else
        {
          flag2 = false;
        }
        flag3 = flag2;
      }
      return flag3;
    }

    // Token: 0x0600207C RID: 8316 RVA: 0x003F5C48 File Offset: 0x003F5C48
    private Task<bool> RefreshAccessTokenAsync()
    {
      object obj = this.gate;
      Task<bool> task2;
      lock (obj)
      {
        if (this.refreshTask != null)
        {
          task2 = this.refreshTask;
        }
        else
        {
          Task<bool> task = this.PerformRefreshAsync();
          this.refreshTask = task;
          task.ContinueWith(delegate(Task<bool> t)
          {
            object obj2 = this.gate;
            lock (obj2)
            {
              this.lastRefreshFailureAt = ((t.IsCompletedSuccessfully && t.Result) ? null : new DateTimeOffset?(DateTimeOffset.UtcNow));
              if (this.refreshTask == task)
              {
                this.refreshTask = null;
              }
            }
          }, TaskScheduler.Default);
          task2 = task;
        }
      }
      return task2;
    }

    // Token: 0x0600207D RID: 8317 RVA: 0x003F5CE0 File Offset: 0x003F5CE0
    private async Task<bool> PerformRefreshAsync()
    {
      await Task.Yield();
      object obj = this.gate;
      string refreshTokenForRequest;
      lock (obj)
      {
        refreshTokenForRequest = this.refreshToken;
      }
      bool flag2;
      if (refreshTokenForRequest == null)
      {
        Log.Warn("Auth", "No refresh token available, signing out");
        await this.SignOutAsync().ConfigureAwait(false);
        flag2 = false;
      }
      else
      {
        bool flag;
        Exception ex;
        object obj2;
        bool flag3;
        try
        {
          using (CancellationTokenSource cts = new CancellationTokenSource(SupabaseAuthService.RefreshTimeout))
          {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.SupabaseUrl("/auth/v1/token?grant_type=refresh_token")))
            {
              request.Headers.TryAddWithoutValidation("apikey", this.http.Endpoints.SupabaseAnonKey);
              request.Content = SupabaseAuthService.JsonBody(delegate(Utf8JsonWriter writer)
              {
                writer.WriteString("refresh_token", refreshTokenForRequest);
              });
              using (HttpResponseMessage response = await this.http.Client.SendAsync(request, cts.Token).ConfigureAwait(false))
              {
                byte[] array = await response.Content.ReadAsByteArrayAsync(cts.Token).ConfigureAwait(false);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                  string text = SupabaseAuthService.RefreshErrorText(array);
                  if (SupabaseAuthService.IsHardRefreshTokenRejection((int)response.StatusCode, text))
                  {
                    Log.Error("Auth", "Token refresh was rejected by Supabase (" + text + "); signing out", null);
                    await this.SignOutAsync().ConfigureAwait(false);
                  }
                  else
                  {
                    string text2 = "Auth";
                    DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 2);
                    defaultInterpolatedStringHandler.AppendLiteral("Token refresh failed with HTTP ");
                    defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
                    defaultInterpolatedStringHandler.AppendLiteral(" (");
                    defaultInterpolatedStringHandler.AppendFormatted(text);
                    defaultInterpolatedStringHandler.AppendLiteral("); keeping cached session for retry");
                    Log.Error(text2, defaultInterpolatedStringHandler.ToStringAndClear(), null);
                  }
                  flag2 = false;
                }
                else
                {
                  using (JsonDocument jsonDocument = JsonDocument.Parse(array, default(JsonDocumentOptions)))
                  {
                    JsonElement rootElement = jsonDocument.RootElement;
                    JsonElement jsonElement;
                    string text3 = ((rootElement.TryGetProperty("access_token", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String) ? jsonElement.GetString() : null);
                    if (string.IsNullOrEmpty(text3))
                    {
                      Log.Error("Auth", "Token refresh returned 200 without a usable access token; keeping cached session for retry", null);
                      flag2 = false;
                    }
                    else
                    {
                      obj = this.gate;
                      lock (obj)
                      {
                        if (this.refreshToken != refreshTokenForRequest)
                        {
                          Log.Info("Auth", "Token refresh response ignored because the local session changed before it completed");
                          return false;
                        }
                        this.accessToken = text3;
                        JsonElement jsonElement2;
                        if (rootElement.TryGetProperty("refresh_token", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.String)
                        {
                          this.refreshToken = jsonElement2.GetString();
                        }
                      }
                      this.SaveSession();
                      Log.Info("Auth", "Token refresh succeeded");
                      this.RaiseChanged();
                      flag2 = true;
                    }
                  }
                }
              }
            }
          }
        }
        catch when (delegate
        {
          // Failed to create a 'catch-when' expression
          ex = obj2 as Exception;
          if (ex == null)
          {
            flag3 = false;
          }
          else
          {
            flag = ex is HttpRequestException || ex is TaskCanceledException || ex is OperationCanceledException || ex is JsonException;
            flag3 = flag > false;
          }
          endfilter(flag3);
        })
        {
          Log.Error("Auth", "Token refresh request failed: " + ex.Message + "; keeping cached session for retry", null);
          flag2 = false;
        }
      }
      return flag2;
    }

    // Token: 0x0600207E RID: 8318 RVA: 0x003F5D24 File Offset: 0x003F5D24
    internal static bool IsHardRefreshTokenRejection(int status, string normalizedErrorText)
    {
      bool flag = status < 400 || status > 499 || status - 408 <= 1 || status == 425 || status == 429;
      return !flag && !string.IsNullOrEmpty(normalizedErrorText) && SupabaseAuthService.HardRefreshRejectionSignals.Any<string>(new Func<string, bool>(normalizedErrorText.Contains));
    }

    // Token: 0x0600207F RID: 8319 RVA: 0x003F5D88 File Offset: 0x003F5D88
    private static string RefreshErrorText(byte[] body)
    {
      string text;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(body, default(JsonDocumentOptions)))
        {
          SupabaseAuthService.<>c__DisplayClass63_0 CS$<>8__locals1;
          CS$<>8__locals1.root = jsonDocument.RootElement;
          if (CS$<>8__locals1.root.ValueKind != JsonValueKind.Object)
          {
            text = "";
          }
          else
          {
            char c = ' ';
            string[] array = new string[3];
            array[0] = SupabaseAuthService.<RefreshErrorText>g__Read|63_0("error", ref CS$<>8__locals1);
            array[1] = SupabaseAuthService.<RefreshErrorText>g__Read|63_0("error_code", ref CS$<>8__locals1);
            int num = 2;
            string text2;
            if ((text2 = SupabaseAuthService.<RefreshErrorText>g__Read|63_0("error_description", ref CS$<>8__locals1)) == null)
            {
              text2 = SupabaseAuthService.<RefreshErrorText>g__Read|63_0("msg", ref CS$<>8__locals1) ?? SupabaseAuthService.<RefreshErrorText>g__Read|63_0("message", ref CS$<>8__locals1);
            }
            array[num] = text2;
            text = string.Join<string>(c, array.Where<string>((string s) => s != null)).ToLowerInvariant();
          }
        }
      }
      catch
      {
        text = "";
      }
      return text;
    }

    // Token: 0x06002080 RID: 8320 RVA: 0x003F5E88 File Offset: 0x003F5E88
    private async Task FetchUserProfileAsync(bool isFreshSignIn, CancellationToken ct, bool attemptRefreshOnAuthFailure = true)
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        text = this.accessToken;
      }
      if (text != null)
      {
        bool flag;
        Exception ex;
        object obj2;
        bool flag2;
        try
        {
          using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, this.http.SupabaseUrl("/auth/v1/user")))
          {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", text);
            request.Headers.TryAddWithoutValidation("apikey", this.http.Endpoints.SupabaseAnonKey);
            using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
              cts.CancelAfter(TimeSpan.FromSeconds(30L));
              HttpResponseMessage httpResponseMessage = await this.http.Client.SendAsync(request, cts.Token).ConfigureAwait(false);
              using (HttpResponseMessage response = httpResponseMessage)
              {
                byte[] array = await response.Content.ReadAsByteArrayAsync(cts.Token).ConfigureAwait(false);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                  using (JsonDocument jsonDocument = JsonDocument.Parse(array, default(JsonDocumentOptions)))
                  {
                    this.ApplyProfile(jsonDocument.RootElement);
                    this.TrackIdentity(jsonDocument.RootElement, isFreshSignIn);
                    string text2 = "Auth";
                    string text3 = "Authenticated as ";
                    UserProfile userProfile = this.User;
                    Log.Info(text2, text3 + (((userProfile != null) ? userProfile.Email : null) ?? "unknown email"));
                    return;
                  }
                }
                string text4 = SupabaseAuthService.TryReadString(array, "error_code");
                if (statusCode == 401 || (statusCode == 403 && text4 == "bad_jwt"))
                {
                  Log.Warn("Auth", "Access token rejected by Supabase (" + (text4 ?? "unknown_error") + "), attempting refresh");
                  flag = attemptRefreshOnAuthFailure;
                  if (flag)
                  {
                    flag = await this.RefreshAccessTokenAsync().ConfigureAwait(false);
                  }
                  if (flag)
                  {
                    await this.FetchUserProfileAsync(isFreshSignIn, ct, false).ConfigureAwait(false);
                  }
                  return;
                }
                string text5 = "Auth";
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 2);
                defaultInterpolatedStringHandler.AppendLiteral("Unexpected status ");
                defaultInterpolatedStringHandler.AppendFormatted<int>(statusCode);
                defaultInterpolatedStringHandler.AppendLiteral(" from /auth/v1/user (");
                defaultInterpolatedStringHandler.AppendFormatted(text4 ?? "unknown_error");
                defaultInterpolatedStringHandler.AppendLiteral(")");
                Log.Error(text5, defaultInterpolatedStringHandler.ToStringAndClear(), null);
              }
            }
          }
          HttpRequestMessage request = null;
          CancellationTokenSource cts = null;
          HttpResponseMessage response = null;
        }
        catch when (delegate
        {
          // Failed to create a 'catch-when' expression
          ex = obj2 as Exception;
          if (ex == null)
          {
            flag2 = false;
          }
          else
          {
            flag = ex is HttpRequestException || ex is OperationCanceledException || ex is JsonException;
            flag2 = flag > false;
          }
          endfilter(flag2);
        })
        {
          Log.Error("Auth", "Failed to fetch user profile: " + ex.Message, null);
        }
      }
    }

    // Token: 0x06002081 RID: 8321 RVA: 0x003F5EE4 File Offset: 0x003F5EE4
    private void ApplyProfile(JsonElement json)
    {
      if (json.ValueKind != JsonValueKind.Object)
      {
        return;
      }
      string text = SupabaseAuthService.<ApplyProfile>g__Read|65_0(json, "id");
      string text2 = SupabaseAuthService.<ApplyProfile>g__Read|65_0(json, "email");
      JsonElement jsonElement;
      json.TryGetProperty("user_metadata", out jsonElement);
      string text3 = SupabaseAuthService.<ApplyProfile>g__Read|65_0(jsonElement, "full_name") ?? SupabaseAuthService.<ApplyProfile>g__Read|65_0(jsonElement, "name");
      string text4 = SupabaseAuthService.<ApplyProfile>g__Read|65_0(jsonElement, "avatar_url") ?? SupabaseAuthService.<ApplyProfile>g__Read|65_0(jsonElement, "picture");
      if (text4 != null && !text4.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      {
        text4 = null;
      }
      double? num = null;
      string text5 = SupabaseAuthService.<ApplyProfile>g__Read|65_0(json, "created_at");
      DateTimeOffset dateTimeOffset;
      if (text5 != null && DateTimeOffset.TryParse(text5, out dateTimeOffset))
      {
        num = new double?((double)dateTimeOffset.ToUnixTimeMilliseconds() / 1000.0);
      }
      object obj = this.gate;
      lock (obj)
      {
        if (this.accessToken == null)
        {
          return;
        }
        string text6;
        if ((text6 = text) == null)
        {
          UserProfile userProfile = this.user;
          text6 = ((userProfile != null) ? userProfile.UserId : null);
        }
        string text7 = text6;
        if (text7 == null)
        {
          return;
        }
        string text8 = text7;
        string text9;
        if ((text9 = text2) == null)
        {
          UserProfile userProfile2 = this.user;
          text9 = ((userProfile2 != null) ? userProfile2.Email : null) ?? "";
        }
        this.user = new UserProfile(text8, text9, text3, text4);
        if (num != null)
        {
          this.signupDateEpochSeconds = num;
        }
      }
      this.SaveSession();
      this.RaiseChanged();
    }

    // Token: 0x06002082 RID: 8322 RVA: 0x003F6058 File Offset: 0x003F6058
    private void TrackIdentity(JsonElement json, bool isFreshSignIn)
    {
      UserProfile userProfile = this.User;
      if (userProfile == null)
      {
        return;
      }
      ClickyAnalytics.IdentifyUser(userProfile.UserId, string.IsNullOrEmpty(userProfile.Email) ? null : userProfile.Email, userProfile.DisplayName);
      if (!isFreshSignIn || string.IsNullOrEmpty(userProfile.Email))
      {
        return;
      }
      JsonElement jsonElement;
      DateTimeOffset dateTimeOffset;
      if (json.ValueKind == JsonValueKind.Object && json.TryGetProperty("created_at", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String && DateTimeOffset.TryParse(jsonElement.GetString(), out dateTimeOffset) && (DateTimeOffset.UtcNow - dateTimeOffset).Duration() < TimeSpan.FromSeconds(60L))
      {
        ClickyAnalytics.TrackGoogleSignUp(userProfile.Email);
        return;
      }
      ClickyAnalytics.TrackGoogleSignIn(userProfile.Email);
    }

    // Token: 0x06002083 RID: 8323 RVA: 0x003F6118 File Offset: 0x003F6118
    [return: Nullable(2)]
    private static string TryReadString(byte[] body, string name)
    {
      string text;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(body, default(JsonDocumentOptions)))
        {
          JsonElement jsonElement;
          text = ((jsonDocument.RootElement.ValueKind == JsonValueKind.Object && jsonDocument.RootElement.TryGetProperty(name, out jsonElement) && jsonElement.ValueKind == JsonValueKind.String) ? jsonElement.GetString() : null);
        }
      }
      catch
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06002084 RID: 8324 RVA: 0x003F61A4 File Offset: 0x003F61A4
    private void SavePendingSignIn(string state, DateTimeOffset startedAt)
    {
      object obj = this.pendingSignInGate;
      lock (obj)
      {
        this.pendingSignInState = state;
        this.pendingSignInStartedAt = new DateTimeOffset?(startedAt);
        this.store.SavePendingSignIn(new StoredPendingSignIn
        {
          State = state,
          StartedAtEpochSeconds = new double?((double)startedAt.ToUnixTimeMilliseconds() / 1000.0)
        });
      }
    }

    // Token: 0x06002085 RID: 8325 RVA: 0x003F6228 File Offset: 0x003F6228
    [return: TupleElementNames(new string[] { "State", "StartedAt" })]
    [return: Nullable(new byte[] { 0, 2 })]
    private ValueTuple<string, DateTimeOffset?> LoadPendingSignIn()
    {
      object obj = this.pendingSignInGate;
      ValueTuple<string, DateTimeOffset?> valueTuple;
      lock (obj)
      {
        if (this.pendingSignInState != null)
        {
          valueTuple = new ValueTuple<string, DateTimeOffset?>(this.pendingSignInState, this.pendingSignInStartedAt);
        }
        else
        {
          StoredPendingSignIn storedPendingSignIn = this.store.LoadPendingSignIn();
          if (storedPendingSignIn != null)
          {
            string state = storedPendingSignIn.State;
            if (state != null && state.Length > 0)
            {
              double? startedAtEpochSeconds = storedPendingSignIn.StartedAtEpochSeconds;
              if (startedAtEpochSeconds != null)
              {
                double valueOrDefault = startedAtEpochSeconds.GetValueOrDefault();
                return new ValueTuple<string, DateTimeOffset?>(state, new DateTimeOffset?(DateTimeOffset.FromUnixTimeMilliseconds((long)Math.Round(valueOrDefault * 1000.0))));
              }
            }
          }
          valueTuple = new ValueTuple<string, DateTimeOffset?>(null, null);
        }
      }
      return valueTuple;
    }

    // Token: 0x06002086 RID: 8326 RVA: 0x003F62FC File Offset: 0x003F62FC
    private void ClearPendingSignIn()
    {
      object obj = this.pendingSignInGate;
      lock (obj)
      {
        this.pendingSignInState = null;
        this.pendingSignInStartedAt = null;
        this.store.DeletePendingSignIn();
      }
    }

    // Token: 0x06002087 RID: 8327 RVA: 0x003F6358 File Offset: 0x003F6358
    [NullableContext(2)]
    internal SignInCallbackGuard.Verdict DecideCallback(string callbackState, DateTimeOffset now)
    {
      object obj = this.pendingSignInGate;
      SignInCallbackGuard.Verdict verdict2;
      lock (obj)
      {
        ValueTuple<string, DateTimeOffset?> valueTuple = this.LoadPendingSignIn();
        string item = valueTuple.Item1;
        DateTimeOffset? item2 = valueTuple.Item2;
        SignInCallbackGuard.Verdict verdict = SignInCallbackGuard.Decide(item, item2, callbackState, AppBundleConfiguration.IsLocalUrl(this.http.Endpoints.SupabaseUrl), now);
        if (SignInCallbackGuard.ConsumesPendingSignIn(verdict))
        {
          this.ClearPendingSignIn();
        }
        verdict2 = verdict;
      }
      return verdict2;
    }

    // Token: 0x06002088 RID: 8328 RVA: 0x003F63D8 File Offset: 0x003F63D8
    private void RestoreSession()
    {
      StoredAuthSession storedAuthSession = this.store.Load();
      if (((storedAuthSession != null) ? storedAuthSession.AccessToken : null) == null || storedAuthSession.RefreshToken == null)
      {
        Log.Info("Auth", "No stored session found");
        return;
      }
      Log.Info("Auth", "Found stored session, validating...");
      long num;
      string text;
      string text2;
      JwtPayload.TryRead(storedAuthSession.AccessToken, out num, out text, out text2);
      object obj = this.gate;
      lock (obj)
      {
        this.accessToken = storedAuthSession.AccessToken;
        this.refreshToken = storedAuthSession.RefreshToken;
        string text3 = text ?? storedAuthSession.UserId;
        UserProfile userProfile;
        if (text3 != null)
        {
          string text4 = text3;
          string text5;
          if ((text5 = storedAuthSession.Email) == null)
          {
            text5 = text2 ?? "";
          }
          userProfile = new UserProfile(text4, text5, storedAuthSession.DisplayName, storedAuthSession.AvatarUrl);
        }
        else
        {
          userProfile = null;
        }
        this.user = userProfile;
        this.signupDateEpochSeconds = storedAuthSession.SignupDateEpochSeconds;
      }
      Task.Run(() => this.FetchUserProfileAsync(false, CancellationToken.None, true));
    }

    // Token: 0x06002089 RID: 8329 RVA: 0x003F64E4 File Offset: 0x003F64E4
    private void SaveSession()
    {
      object obj = this.gate;
      StoredAuthSession storedAuthSession2;
      lock (obj)
      {
        if (this.accessToken == null)
        {
          return;
        }
        StoredAuthSession storedAuthSession = new StoredAuthSession();
        storedAuthSession.AccessToken = this.accessToken;
        storedAuthSession.RefreshToken = this.refreshToken;
        UserProfile userProfile = this.user;
        storedAuthSession.UserId = ((userProfile != null) ? userProfile.UserId : null);
        UserProfile userProfile2 = this.user;
        storedAuthSession.Email = ((userProfile2 != null) ? userProfile2.Email : null);
        UserProfile userProfile3 = this.user;
        storedAuthSession.DisplayName = ((userProfile3 != null) ? userProfile3.DisplayName : null);
        UserProfile userProfile4 = this.user;
        storedAuthSession.AvatarUrl = ((userProfile4 != null) ? userProfile4.AvatarUrl : null);
        storedAuthSession.SignupDateEpochSeconds = this.signupDateEpochSeconds;
        storedAuthSession2 = storedAuthSession;
      }
      this.store.Save(storedAuthSession2);
    }

    // Token: 0x0600208A RID: 8330 RVA: 0x003F65C4 File Offset: 0x003F65C4
    private void RaiseChanged()
    {
      try
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "Changed handler threw", ex);
      }
    }

    // Token: 0x0600208B RID: 8331 RVA: 0x003F6610 File Offset: 0x003F6610
    private static ByteArrayContent JsonBody(Action<Utf8JsonWriter> write)
    {
      ArrayBufferWriter<byte> arrayBufferWriter = new ArrayBufferWriter<byte>(256);
      using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter, default(JsonWriterOptions)))
      {
        utf8JsonWriter.WriteStartObject();
        write(utf8JsonWriter);
        utf8JsonWriter.WriteEndObject();
      }
      return new ByteArrayContent(arrayBufferWriter.WrittenSpan.ToArray())
      {
        Headers = 
        {
          ContentType = new MediaTypeHeaderValue("application/json")
        }
      };
    }

    // Token: 0x0600208C RID: 8332 RVA: 0x003F6690 File Offset: 0x003F6690
    private static bool OpenInDefaultBrowser(Uri url)
    {
      bool flag;
      try
      {
        ShellOpen.Start(url.AbsoluteUri);
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Error("Auth", "opening the browser failed", ex);
        flag = false;
      }
      return flag;
    }

    // Token: 0x0600208E RID: 8334 RVA: 0x003F6790 File Offset: 0x003F6790
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <RefreshErrorText>g__Read|63_0(string name, ref SupabaseAuthService.<>c__DisplayClass63_0 A_1)
    {
      JsonElement jsonElement;
      if (!A_1.root.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return jsonElement.GetString();
    }

    // Token: 0x0600208F RID: 8335 RVA: 0x003F67C0 File Offset: 0x003F67C0
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ApplyProfile>g__Read|65_0(JsonElement element, string name)
    {
      JsonElement jsonElement;
      if (element.ValueKind != JsonValueKind.Object || !element.TryGetProperty(name, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return jsonElement.GetString();
    }

    // Token: 0x04000DAC RID: 3500
    private const string Area = "Auth";

    // Token: 0x04000DAD RID: 3501
    private static readonly TimeSpan RefreshLeadTime = TimeSpan.FromSeconds(60L);

    // Token: 0x04000DAE RID: 3502
    private static readonly TimeSpan ProactiveRefreshLeadTime = TimeSpan.FromMinutes(5L);

    // Token: 0x04000DAF RID: 3503
    private static readonly TimeSpan ExpiryLeeway = TimeSpan.FromSeconds(5L);

    // Token: 0x04000DB0 RID: 3504
    private static readonly TimeSpan RefreshRetryCooldown = TimeSpan.FromSeconds(30L);

    // Token: 0x04000DB1 RID: 3505
    private static readonly TimeSpan RefreshTimeout = TimeSpan.FromSeconds(20L);

    // Token: 0x04000DB2 RID: 3506
    private static readonly string[] HardRefreshRejectionSignals = new string[]
    {
      "invalid_grant", "invalid refresh token", "refresh token not found", "refresh token revoked", "refresh token has been revoked", "refresh token already used", "reuse detected", "token reuse", "session not found", "session expired",
      "no valid session", "user not found"
    };

    // Token: 0x04000DB3 RID: 3507
    private readonly ClickyHttp http;

    // Token: 0x04000DB4 RID: 3508
    private readonly AuthTokenStore store;

    // Token: 0x04000DB5 RID: 3509
    private readonly Func<Uri, bool> openBrowser;

    // Token: 0x04000DB6 RID: 3510
    private readonly object gate = new object();

    // Token: 0x04000DB7 RID: 3511
    private readonly object pendingSignInGate = new object();

    // Token: 0x04000DB8 RID: 3512
    [Nullable(2)]
    private string accessToken;

    // Token: 0x04000DB9 RID: 3513
    [Nullable(2)]
    private string refreshToken;

    // Token: 0x04000DBA RID: 3514
    [Nullable(2)]
    private UserProfile user;

    // Token: 0x04000DBB RID: 3515
    private double? signupDateEpochSeconds;

    // Token: 0x04000DBC RID: 3516
    [Nullable(2)]
    private Task<bool> refreshTask;

    // Token: 0x04000DBD RID: 3517
    private DateTimeOffset? lastRefreshFailureAt;

    // Token: 0x04000DBE RID: 3518
    [Nullable(2)]
    private TaskCompletionSource<bool> pendingGoogleSignIn;

    // Token: 0x04000DBF RID: 3519
    [Nullable(2)]
    private string lastSignInErrorMessage;

    // Token: 0x04000DC0 RID: 3520
    [Nullable(2)]
    private string pendingSignInState;

    // Token: 0x04000DC1 RID: 3521
    private DateTimeOffset? pendingSignInStartedAt;

    // Token: 0x02000678 RID: 1656
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002566 RID: 9574
      [Nullable(new byte[] { 0, 1 })]
      public static Func<Uri, bool> <0>__OpenInDefaultBrowser;
    }
  }
}
