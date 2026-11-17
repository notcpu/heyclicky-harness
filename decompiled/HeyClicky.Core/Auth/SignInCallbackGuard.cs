using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace HeyClicky.Core.Auth
{
  // Token: 0x0200022B RID: 555
  public static class SignInCallbackGuard
  {
    // Token: 0x06002059 RID: 8281 RVA: 0x003F5118 File Offset: 0x003F5118
    public static bool AcceptsCallback(SignInCallbackGuard.Verdict verdict)
    {
      return verdict <= SignInCallbackGuard.Verdict.AcceptLocalDevelopment;
    }

    // Token: 0x0600205A RID: 8282 RVA: 0x003F5130 File Offset: 0x003F5130
    public static bool ConsumesPendingSignIn(SignInCallbackGuard.Verdict verdict)
    {
      return SignInCallbackGuard.AcceptsCallback(verdict) || verdict == SignInCallbackGuard.Verdict.RefuseSignInExpired;
    }

    // Token: 0x0600205B RID: 8283 RVA: 0x003F5140 File Offset: 0x003F5140
    [NullableContext(1)]
    public static string AnalyticsReason(SignInCallbackGuard.Verdict verdict)
    {
      string text;
      switch (verdict)
      {
      case SignInCallbackGuard.Verdict.Accept:
        text = "accepted";
        break;
      case SignInCallbackGuard.Verdict.AcceptLocalDevelopment:
        text = "accepted_local_development";
        break;
      case SignInCallbackGuard.Verdict.RefuseNoSignInStarted:
        text = "no_sign_in_started";
        break;
      case SignInCallbackGuard.Verdict.RefuseSignInExpired:
        text = "sign_in_expired";
        break;
      case SignInCallbackGuard.Verdict.RefuseMissingState:
        text = "missing_state";
        break;
      default:
        text = "state_mismatch";
        break;
      }
      return text;
    }

    // Token: 0x0600205C RID: 8284 RVA: 0x003F5198 File Offset: 0x003F5198
    [NullableContext(1)]
    public static string MakeState()
    {
      return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)).TrimEnd('=').Replace('+', '-')
        .Replace('/', '_');
    }

    // Token: 0x0600205D RID: 8285 RVA: 0x003F51C0 File Offset: 0x003F51C0
    [NullableContext(2)]
    public static SignInCallbackGuard.Verdict Decide(string pendingState, DateTimeOffset? pendingStartedAt, string callbackState, bool isLocalSupabaseBackend, DateTimeOffset now)
    {
      if (isLocalSupabaseBackend)
      {
        return SignInCallbackGuard.Verdict.AcceptLocalDevelopment;
      }
      if (string.IsNullOrEmpty(pendingState) || pendingStartedAt == null)
      {
        return SignInCallbackGuard.Verdict.RefuseNoSignInStarted;
      }
      DateTimeOffset valueOrDefault = pendingStartedAt.GetValueOrDefault();
      TimeSpan timeSpan = now - valueOrDefault;
      if (timeSpan < -SignInCallbackGuard.ToleratedClockRollback || timeSpan > SignInCallbackGuard.PendingSignInLifetime)
      {
        return SignInCallbackGuard.Verdict.RefuseSignInExpired;
      }
      if (string.IsNullOrEmpty(callbackState))
      {
        return SignInCallbackGuard.Verdict.RefuseMissingState;
      }
      if (!CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(callbackState), Encoding.UTF8.GetBytes(pendingState)))
      {
        return SignInCallbackGuard.Verdict.RefuseStateMismatch;
      }
      return SignInCallbackGuard.Verdict.Accept;
    }

    // Token: 0x04000DAA RID: 3498
    public static readonly TimeSpan PendingSignInLifetime = TimeSpan.FromMinutes(15L);

    // Token: 0x04000DAB RID: 3499
    public static readonly TimeSpan ToleratedClockRollback = TimeSpan.FromMinutes(5L);

    // Token: 0x02000677 RID: 1655
    public enum Verdict
    {
      // Token: 0x04002560 RID: 9568
      Accept,
      // Token: 0x04002561 RID: 9569
      AcceptLocalDevelopment,
      // Token: 0x04002562 RID: 9570
      RefuseNoSignInStarted,
      // Token: 0x04002563 RID: 9571
      RefuseSignInExpired,
      // Token: 0x04002564 RID: 9572
      RefuseMissingState,
      // Token: 0x04002565 RID: 9573
      RefuseStateMismatch
    }
  }
}
