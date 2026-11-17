using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000224 RID: 548
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AuthCallbackError : IEquatable<AuthCallbackError>
  {
    // Token: 0x06002019 RID: 8217 RVA: 0x003F42E0 File Offset: 0x003F42E0
    public AuthCallbackError(string Error, string ErrorCode, string ErrorDescription)
    {
      this.Error = Error;
      this.ErrorCode = ErrorCode;
      this.ErrorDescription = ErrorDescription;
      base..ctor();
    }

    // Token: 0x17000737 RID: 1847
    // (get) Token: 0x0600201A RID: 8218 RVA: 0x003F4300 File Offset: 0x003F4300
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AuthCallbackError);
      }
    }

    // Token: 0x17000738 RID: 1848
    // (get) Token: 0x0600201B RID: 8219 RVA: 0x003F430C File Offset: 0x003F430C
    // (set) Token: 0x0600201C RID: 8220 RVA: 0x003F4314 File Offset: 0x003F4314
    public string Error { get; set; }

    // Token: 0x17000739 RID: 1849
    // (get) Token: 0x0600201D RID: 8221 RVA: 0x003F4320 File Offset: 0x003F4320
    // (set) Token: 0x0600201E RID: 8222 RVA: 0x003F4328 File Offset: 0x003F4328
    public string ErrorCode { get; set; }

    // Token: 0x1700073A RID: 1850
    // (get) Token: 0x0600201F RID: 8223 RVA: 0x003F4334 File Offset: 0x003F4334
    // (set) Token: 0x06002020 RID: 8224 RVA: 0x003F433C File Offset: 0x003F433C
    public string ErrorDescription { get; set; }

    // Token: 0x1700073B RID: 1851
    // (get) Token: 0x06002021 RID: 8225 RVA: 0x003F4348 File Offset: 0x003F4348
    [Nullable(1)]
    public string RedactedDescription
    {
      [NullableContext(1)]
      get
      {
        return AuthDeepLink.RedactSignInErrorText(this.ErrorDescription);
      }
    }

    // Token: 0x1700073C RID: 1852
    // (get) Token: 0x06002022 RID: 8226 RVA: 0x003F4358 File Offset: 0x003F4358
    [Nullable(1)]
    public string UserFacingMessage
    {
      [NullableContext(1)]
      get
      {
        if (this.Error == "access_denied")
        {
          return "Google sign-in was canceled. Try again when you're ready.";
        }
        string redactedDescription = this.RedactedDescription;
        if (redactedDescription.Length == 0)
        {
          return "Sign-in didn't finish. Try again.";
        }
        return "Sign-in didn't finish: " + redactedDescription.TrimEnd('.') + ". Try again.";
      }
    }

    // Token: 0x1700073D RID: 1853
    // (get) Token: 0x06002023 RID: 8227 RVA: 0x003F43AC File Offset: 0x003F43AC
    [Nullable(1)]
    public string DiagnosticMessage
    {
      [NullableContext(1)]
      get
      {
        string redactedDescription = this.RedactedDescription;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(37, 3);
        defaultInterpolatedStringHandler.AppendLiteral("error=");
        defaultInterpolatedStringHandler.AppendFormatted(this.Error ?? "none");
        defaultInterpolatedStringHandler.AppendLiteral(" error_code=");
        defaultInterpolatedStringHandler.AppendFormatted(this.ErrorCode ?? "none");
        defaultInterpolatedStringHandler.AppendLiteral(" error_description=");
        defaultInterpolatedStringHandler.AppendFormatted((redactedDescription.Length == 0) ? "none" : redactedDescription);
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x1700073E RID: 1854
    // (get) Token: 0x06002024 RID: 8228 RVA: 0x003F4438 File Offset: 0x003F4438
    [Nullable(1)]
    public string ReportedErrorDomain
    {
      [NullableContext(1)]
      get
      {
        string text = "supabase_auth.";
        string text2;
        if ((text2 = this.ErrorCode) == null)
        {
          text2 = this.Error ?? "no_access_token";
        }
        return text + text2;
      }
    }

    // Token: 0x06002025 RID: 8229 RVA: 0x003F4460 File Offset: 0x003F4460
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AuthCallbackError");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002026 RID: 8230 RVA: 0x003F44AC File Offset: 0x003F44AC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Error = ");
      builder.Append(this.Error);
      builder.Append(", ErrorCode = ");
      builder.Append(this.ErrorCode);
      builder.Append(", ErrorDescription = ");
      builder.Append(this.ErrorDescription);
      builder.Append(", RedactedDescription = ");
      builder.Append(this.RedactedDescription);
      builder.Append(", UserFacingMessage = ");
      builder.Append(this.UserFacingMessage);
      builder.Append(", DiagnosticMessage = ");
      builder.Append(this.DiagnosticMessage);
      builder.Append(", ReportedErrorDomain = ");
      builder.Append(this.ReportedErrorDomain);
      return true;
    }

    // Token: 0x06002027 RID: 8231 RVA: 0x003F4570 File Offset: 0x003F4570
    [CompilerGenerated]
    public static bool operator !=(AuthCallbackError left, AuthCallbackError right)
    {
      return !(left == right);
    }

    // Token: 0x06002028 RID: 8232 RVA: 0x003F457C File Offset: 0x003F457C
    [CompilerGenerated]
    public static bool operator ==(AuthCallbackError left, AuthCallbackError right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002029 RID: 8233 RVA: 0x003F4590 File Offset: 0x003F4590
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Error>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorDescription>k__BackingField);
    }

    // Token: 0x0600202A RID: 8234 RVA: 0x003F45F4 File Offset: 0x003F45F4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AuthCallbackError);
    }

    // Token: 0x0600202B RID: 8235 RVA: 0x003F4604 File Offset: 0x003F4604
    [CompilerGenerated]
    public bool Equals(AuthCallbackError other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Error>k__BackingField, other.<Error>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorCode>k__BackingField, other.<ErrorCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorDescription>k__BackingField, other.<ErrorDescription>k__BackingField));
    }

    // Token: 0x0600202D RID: 8237 RVA: 0x003F4680 File Offset: 0x003F4680
    [CompilerGenerated]
    private AuthCallbackError([Nullable(1)] AuthCallbackError original)
    {
      this.Error = original.<Error>k__BackingField;
      this.ErrorCode = original.<ErrorCode>k__BackingField;
      this.ErrorDescription = original.<ErrorDescription>k__BackingField;
    }

    // Token: 0x0600202E RID: 8238 RVA: 0x003F46AC File Offset: 0x003F46AC
    [CompilerGenerated]
    public void Deconstruct(out string Error, out string ErrorCode, out string ErrorDescription)
    {
      Error = this.Error;
      ErrorCode = this.ErrorCode;
      ErrorDescription = this.ErrorDescription;
    }
  }
}
