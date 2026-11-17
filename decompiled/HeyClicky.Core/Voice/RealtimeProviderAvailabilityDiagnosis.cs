using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000083 RID: 131
  [NullableContext(1)]
  [Nullable(0)]
  public static class RealtimeProviderAvailabilityDiagnosis
  {
    // Token: 0x060007F1 RID: 2033 RVA: 0x00367A3C File Offset: 0x00367A3C
    [NullableContext(2)]
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<RealtimeProviderUnavailableReason> DiagnoseAsync(Exception sessionOpenError, [Nullable(new byte[] { 2, 1, 1 })] Func<Task<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome>> probe = null, Func<DateTimeOffset> clock = null)
    {
      RealtimeProviderUnavailableReason realtimeProviderUnavailableReason;
      if (sessionOpenError == null)
      {
        realtimeProviderUnavailableReason = null;
      }
      else
      {
        Func<DateTimeOffset> func = clock;
        if (clock == null && (func = RealtimeProviderAvailabilityDiagnosis.<>c.<>9__9_0) == null)
        {
          func = (RealtimeProviderAvailabilityDiagnosis.<>c.<>9__9_0 = () => DateTimeOffset.UtcNow);
        }
        DateTimeOffset now = func();
        RealtimeProviderUnavailableReason realtimeProviderUnavailableReason2 = RealtimeProviderAvailabilityDiagnosis.ReasonReportedByWorker(sessionOpenError);
        if (realtimeProviderUnavailableReason2 != null)
        {
          object obj = RealtimeProviderAvailabilityDiagnosis.rememberedGate;
          lock (obj)
          {
            RealtimeProviderAvailabilityDiagnosis.rememberedDiagnosis = new ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>?(new ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>(realtimeProviderUnavailableReason2, now));
          }
          realtimeProviderUnavailableReason = realtimeProviderUnavailableReason2;
        }
        else
        {
          RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind? sessionOpenFailureKind = RealtimeProviderAvailabilityDiagnosis.FailureKindWorthProbing(sessionOpenError);
          if (sessionOpenFailureKind != null)
          {
            RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind failureKind = sessionOpenFailureKind.GetValueOrDefault();
            object obj = RealtimeProviderAvailabilityDiagnosis.rememberedGate;
            lock (obj)
            {
              ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>? valueTuple = RealtimeProviderAvailabilityDiagnosis.rememberedDiagnosis;
              if (valueTuple != null)
              {
                ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
                if (now - valueOrDefault.Item2 < RealtimeProviderAvailabilityDiagnosis.PositiveDiagnosisLifetime)
                {
                  return valueOrDefault.Item1;
                }
              }
            }
            Func<Task<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome>> func2 = probe;
            if (probe == null && (func2 = RealtimeProviderAvailabilityDiagnosis.<>O.<0>__ProbeProviderAsync) == null)
            {
              func2 = (RealtimeProviderAvailabilityDiagnosis.<>O.<0>__ProbeProviderAsync = new Func<Task<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome>>(RealtimeProviderAvailabilityDiagnosis.ProbeProviderAsync));
            }
            RealtimeProviderUnavailableReason realtimeProviderUnavailableReason3 = RealtimeProviderAvailabilityDiagnosis.Reason(await func2().ConfigureAwait(false), failureKind);
            if (realtimeProviderUnavailableReason3 != null)
            {
              obj = RealtimeProviderAvailabilityDiagnosis.rememberedGate;
              lock (obj)
              {
                RealtimeProviderAvailabilityDiagnosis.rememberedDiagnosis = new ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>?(new ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>(realtimeProviderUnavailableReason3, now));
              }
            }
            realtimeProviderUnavailableReason = realtimeProviderUnavailableReason3;
          }
          else
          {
            realtimeProviderUnavailableReason = null;
          }
        }
      }
      return realtimeProviderUnavailableReason;
    }

    // Token: 0x060007F2 RID: 2034 RVA: 0x00367A90 File Offset: 0x00367A90
    internal static void ForgetRememberedDiagnosis()
    {
      object obj = RealtimeProviderAvailabilityDiagnosis.rememberedGate;
      lock (obj)
      {
        RealtimeProviderAvailabilityDiagnosis.rememberedDiagnosis = null;
      }
    }

    // Token: 0x060007F3 RID: 2035 RVA: 0x00367AD4 File Offset: 0x00367AD4
    [return: Nullable(2)]
    public static RealtimeProviderUnavailableReason ReasonReportedByWorker(Exception sessionOpenError)
    {
      WorkerHttpException ex = sessionOpenError as WorkerHttpException;
      if (ex == null)
      {
        return null;
      }
      RealtimeProviderUnavailableReason realtimeProviderUnavailableReason;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(ex.Body, default(JsonDocumentOptions)))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          JsonElement jsonElement;
          if (rootElement.ValueKind != JsonValueKind.Object || !rootElement.TryGetProperty("error_code", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String || jsonElement.GetString() != "provider_region_unsupported")
          {
            realtimeProviderUnavailableReason = null;
          }
          else
          {
            realtimeProviderUnavailableReason = new RealtimeProviderUnavailableReason.RegionUnsupported(RealtimeProviderAvailabilityDiagnosis.<ReasonReportedByWorker>g__Field|11_0(rootElement, "provider_name") ?? "OpenAI", RealtimeProviderAvailabilityDiagnosis.<ReasonReportedByWorker>g__Field|11_0(rootElement, "client_location_name"));
          }
        }
      }
      catch (JsonException)
      {
        realtimeProviderUnavailableReason = null;
      }
      return realtimeProviderUnavailableReason;
    }

    // Token: 0x060007F4 RID: 2036 RVA: 0x00367BA0 File Offset: 0x00367BA0
    public static RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind? FailureKindWorthProbing(Exception sessionOpenError)
    {
      WorkerHttpException ex = sessionOpenError as WorkerHttpException;
      RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind? sessionOpenFailureKind;
      if (ex == null)
      {
        if (!(sessionOpenError is RealtimeSocketOpenException))
        {
          sessionOpenFailureKind = null;
        }
        else
        {
          sessionOpenFailureKind = new RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind?(RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind.AppCouldNotReachProvider);
        }
      }
      else
      {
        sessionOpenFailureKind = ((ex.Status == 502) ? new RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind?(RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind.WorkerCouldNotReachProvider) : null);
      }
      return sessionOpenFailureKind;
    }

    // Token: 0x060007F5 RID: 2037 RVA: 0x00367BF8 File Offset: 0x00367BF8
    [return: Nullable(2)]
    public static RealtimeProviderUnavailableReason Reason(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome outcome, RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind failureKind)
    {
      RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind? failure = outcome.Failure;
      if (failure != null)
      {
        RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind valueOrDefault = failure.GetValueOrDefault();
        if (valueOrDefault == RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.TlsRefused)
        {
          return new RealtimeProviderUnavailableReason.NetworkBlocksProvider("OpenAI");
        }
        if (failureKind == RealtimeProviderAvailabilityDiagnosis.SessionOpenFailureKind.AppCouldNotReachProvider && valueOrDefault == RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.HostUnreachable)
        {
          return new RealtimeProviderUnavailableReason.NetworkBlocksProvider("OpenAI");
        }
        return null;
      }
      else
      {
        string text = outcome.BodyText.ToLowerInvariant();
        if (outcome.StatusCode != 403 || (!text.Contains("unsupported_country_region_territory") && !text.Contains("country, region, or territory not supported")))
        {
          return null;
        }
        return new RealtimeProviderUnavailableReason.RegionUnsupported("OpenAI", null);
      }
    }

    // Token: 0x060007F6 RID: 2038 RVA: 0x00367C88 File Offset: 0x00367C88
    public static RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind ClassifyConnectionFailure(Exception error)
    {
      Exception ex = error;
      while (ex != null)
      {
        if (ex is AuthenticationException)
        {
          return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.TlsRefused;
        }
        HttpRequestException ex2 = ex as HttpRequestException;
        if (ex2 != null)
        {
          HttpRequestError httpRequestError = ex2.HttpRequestError;
          if (httpRequestError - HttpRequestError.NameResolutionError <= 1)
          {
            return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.HostUnreachable;
          }
          if (httpRequestError == HttpRequestError.SecureConnectionError)
          {
            return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.TlsRefused;
          }
        }
        SocketException ex3 = ex as SocketException;
        if (ex3 != null)
        {
          SocketError socketErrorCode = ex3.SocketErrorCode;
          if (socketErrorCode <= SocketError.ConnectionRefused)
          {
            if (socketErrorCode != SocketError.NetworkUnreachable && socketErrorCode != SocketError.ConnectionRefused)
            {
              goto IL_007F;
            }
          }
          else if (socketErrorCode != SocketError.HostUnreachable && socketErrorCode != SocketError.HostNotFound && socketErrorCode != SocketError.NoData)
          {
            goto IL_007F;
          }
          bool flag = true;
          goto IL_0082;
          IL_007F:
          flag = false;
          IL_0082:
          if (!flag)
          {
            return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.Other;
          }
          return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.HostUnreachable;
        }
        else
        {
          ex = ex.InnerException;
        }
      }
      return RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind.Other;
    }

    // Token: 0x060007F7 RID: 2039 RVA: 0x00367D30 File Offset: 0x00367D30
    private static async Task<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome> ProbeProviderAsync()
    {
      RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome providerProbeOutcome;
      using (SocketsHttpHandler handler = new SocketsHttpHandler
      {
        PooledConnectionLifetime = TimeSpan.Zero,
        UseCookies = false
      })
      {
        using (HttpClient client = new HttpClient(handler)
        {
          Timeout = RealtimeProviderAvailabilityDiagnosis.ProviderProbeTimeout
        })
        {
          try
          {
            HttpResponseMessage httpResponseMessage = await client.GetAsync(RealtimeProviderAvailabilityDiagnosis.ProviderProbeUrl).ConfigureAwait(false);
            using (HttpResponseMessage response = httpResponseMessage)
            {
              string text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
              providerProbeOutcome = RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome.Answered((int)response.StatusCode, (text.Length > 2000) ? text.Substring(0, 2000) : text);
            }
          }
          catch (Exception ex)
          {
            providerProbeOutcome = RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome.Failed(RealtimeProviderAvailabilityDiagnosis.ClassifyConnectionFailure(ex));
          }
        }
      }
      return providerProbeOutcome;
    }

    // Token: 0x060007F9 RID: 2041 RVA: 0x00367DA0 File Offset: 0x00367DA0
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ReasonReportedByWorker>g__Field|11_0(JsonElement root, string name)
    {
      JsonElement jsonElement;
      if (root.TryGetProperty(name, out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
      {
        string text = jsonElement.GetString().Trim();
        if (text != null && text.Length > 0)
        {
          return text;
        }
      }
      return null;
    }

    // Token: 0x040003DC RID: 988
    private const string ProviderNameForMessages = "OpenAI";

    // Token: 0x040003DD RID: 989
    private static readonly Uri ProviderProbeUrl = new Uri("https://api.openai.com/v1/models");

    // Token: 0x040003DE RID: 990
    private static readonly TimeSpan ProviderProbeTimeout = TimeSpan.FromSeconds(4L);

    // Token: 0x040003DF RID: 991
    private static readonly TimeSpan PositiveDiagnosisLifetime = TimeSpan.FromMinutes(10L);

    // Token: 0x040003E0 RID: 992
    private static readonly object rememberedGate = new object();

    // Token: 0x040003E1 RID: 993
    [TupleElementNames(new string[] { "Reason", "MadeAt" })]
    [Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<RealtimeProviderUnavailableReason, DateTimeOffset>? rememberedDiagnosis;

    // Token: 0x02000356 RID: 854
    [NullableContext(0)]
    public enum SessionOpenFailureKind
    {
      // Token: 0x04001775 RID: 6005
      WorkerCouldNotReachProvider,
      // Token: 0x04001776 RID: 6006
      AppCouldNotReachProvider
    }

    // Token: 0x02000357 RID: 855
    [NullableContext(0)]
    public enum ProbeFailureKind
    {
      // Token: 0x04001778 RID: 6008
      TlsRefused,
      // Token: 0x04001779 RID: 6009
      HostUnreachable,
      // Token: 0x0400177A RID: 6010
      Other
    }

    // Token: 0x02000358 RID: 856
    [Nullable(0)]
    public sealed class ProviderProbeOutcome : IEquatable<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome>
    {
      // Token: 0x06002885 RID: 10373 RVA: 0x0041B7B8 File Offset: 0x0041B7B8
      public ProviderProbeOutcome(int StatusCode, string BodyText, RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind? Failure)
      {
        this.StatusCode = StatusCode;
        this.BodyText = BodyText;
        this.Failure = Failure;
        base..ctor();
      }

      // Token: 0x170007EE RID: 2030
      // (get) Token: 0x06002886 RID: 10374 RVA: 0x0041B7D8 File Offset: 0x0041B7D8
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome);
        }
      }

      // Token: 0x170007EF RID: 2031
      // (get) Token: 0x06002887 RID: 10375 RVA: 0x0041B7E4 File Offset: 0x0041B7E4
      // (set) Token: 0x06002888 RID: 10376 RVA: 0x0041B7EC File Offset: 0x0041B7EC
      public int StatusCode { get; set; }

      // Token: 0x170007F0 RID: 2032
      // (get) Token: 0x06002889 RID: 10377 RVA: 0x0041B7F8 File Offset: 0x0041B7F8
      // (set) Token: 0x0600288A RID: 10378 RVA: 0x0041B800 File Offset: 0x0041B800
      public string BodyText { get; set; }

      // Token: 0x170007F1 RID: 2033
      // (get) Token: 0x0600288B RID: 10379 RVA: 0x0041B80C File Offset: 0x0041B80C
      // (set) Token: 0x0600288C RID: 10380 RVA: 0x0041B814 File Offset: 0x0041B814
      public RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind? Failure { get; set; }

      // Token: 0x0600288D RID: 10381 RVA: 0x0041B820 File Offset: 0x0041B820
      public static RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome Answered(int statusCode, string bodyText)
      {
        return new RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome(statusCode, bodyText, null);
      }

      // Token: 0x0600288E RID: 10382 RVA: 0x0041B840 File Offset: 0x0041B840
      public static RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome Failed(RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind failure)
      {
        return new RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome(0, "", new RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind?(failure));
      }

      // Token: 0x0600288F RID: 10383 RVA: 0x0041B854 File Offset: 0x0041B854
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ProviderProbeOutcome");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002890 RID: 10384 RVA: 0x0041B8A0 File Offset: 0x0041B8A0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("StatusCode = ");
        builder.Append(this.StatusCode.ToString());
        builder.Append(", BodyText = ");
        builder.Append(this.BodyText);
        builder.Append(", Failure = ");
        builder.Append(this.Failure.ToString());
        return true;
      }

      // Token: 0x06002891 RID: 10385 RVA: 0x0041B91C File Offset: 0x0041B91C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome left, RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome right)
      {
        return !(left == right);
      }

      // Token: 0x06002892 RID: 10386 RVA: 0x0041B928 File Offset: 0x0041B928
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome left, RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002893 RID: 10387 RVA: 0x0041B93C File Offset: 0x0041B93C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<StatusCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BodyText>k__BackingField)) * -1521134295 + EqualityComparer<RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind?>.Default.GetHashCode(this.<Failure>k__BackingField);
      }

      // Token: 0x06002894 RID: 10388 RVA: 0x0041B9A0 File Offset: 0x0041B9A0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome);
      }

      // Token: 0x06002895 RID: 10389 RVA: 0x0041B9B0 File Offset: 0x0041B9B0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<StatusCode>k__BackingField, other.<StatusCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BodyText>k__BackingField, other.<BodyText>k__BackingField) && EqualityComparer<RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind?>.Default.Equals(this.<Failure>k__BackingField, other.<Failure>k__BackingField));
      }

      // Token: 0x06002897 RID: 10391 RVA: 0x0041BA2C File Offset: 0x0041BA2C
      [CompilerGenerated]
      private ProviderProbeOutcome(RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome original)
      {
        this.StatusCode = original.<StatusCode>k__BackingField;
        this.BodyText = original.<BodyText>k__BackingField;
        this.Failure = original.<Failure>k__BackingField;
      }

      // Token: 0x06002898 RID: 10392 RVA: 0x0041BA58 File Offset: 0x0041BA58
      [CompilerGenerated]
      public void Deconstruct(out int StatusCode, out string BodyText, out RealtimeProviderAvailabilityDiagnosis.ProbeFailureKind? Failure)
      {
        StatusCode = this.StatusCode;
        BodyText = this.BodyText;
        Failure = this.Failure;
      }
    }

    // Token: 0x02000359 RID: 857
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400177E RID: 6014
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<Task<RealtimeProviderAvailabilityDiagnosis.ProviderProbeOutcome>> <0>__ProbeProviderAsync;
    }
  }
}
