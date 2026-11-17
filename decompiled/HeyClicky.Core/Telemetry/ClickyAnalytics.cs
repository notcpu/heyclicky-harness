using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Identity;
using HeyClicky.Core.Voice;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AF RID: 175
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyAnalytics
  {
    // Token: 0x06000A94 RID: 2708 RVA: 0x00375778 File Offset: 0x00375778
    public static void TrackRealtimeAudioOutputRouteChange(string fromTransport, string toTransport, string fromDevice, string toDevice, int droppedBuffers, bool engineWasRunning, bool restartSucceeded)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["from_transport"] = fromTransport;
      jsonObject["to_transport"] = toTransport;
      jsonObject["transport_switched"] = fromTransport != toTransport;
      jsonObject["from_device"] = fromDevice;
      jsonObject["to_device"] = toDevice;
      jsonObject["device_switched"] = fromDevice != toDevice;
      jsonObject["dropped_buffers"] = droppedBuffers;
      jsonObject["engine_was_running"] = engineWasRunning;
      jsonObject["restart_succeeded"] = restartSucceeded;
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.Breadcrumb("audio", "realtime_output_route_changed", restartSucceeded ? CrashReportLevel.Info : CrashReportLevel.Warning, (JsonObject)jsonObject2.DeepClone());
      ClickyAnalytics.Capture("realtime_audio_route_changed", jsonObject2, null, null);
    }

    // Token: 0x06000A95 RID: 2709 RVA: 0x0037586C File Offset: 0x0037586C
    public static void ReportRealtimeAudioRouteRebuildFailed(string transport, string device, int droppedBuffers)
    {
      string text = "realtime_audio_route_rebuild_failed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["transport"] = transport;
      jsonObject["device"] = device;
      jsonObject["dropped_buffers"] = droppedBuffers;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      ICrashReporter crashReporter = CrashReporter.Current;
      if (!crashReporter.IsEnabled || !ClickyAnalytics.ShouldEmitCrashReporterEvent("realtime-audio-route-rebuild-failed"))
      {
        return;
      }
      try
      {
        ICrashReporter crashReporter2 = crashReporter;
        string text2 = "Realtime voice engine failed to recover from an output route change";
        CrashReportLevel crashReportLevel = CrashReportLevel.Warning;
        IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>("realtime-audio-route-rebuild-failed");
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["issue"] = "realtime_audio_rebuild_failed";
        dictionary["output_transport"] = transport;
        dictionary["output_device"] = device;
        string text3 = "realtime_audio_route";
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["transport"] = transport;
        jsonObject2["device"] = device;
        jsonObject2["dropped_buffers"] = droppedBuffers;
        crashReporter2.CaptureMessage(text2, crashReportLevel, readOnlyList, dictionary, text3, jsonObject2, null);
      }
      catch (Exception ex)
      {
        Log.Warn("audio", "crash reporter capture failed: " + ex.Message);
      }
    }

    // Token: 0x06000A96 RID: 2710 RVA: 0x0037599C File Offset: 0x0037599C
    public static void TrackAuthCallbackRefused(string reason, bool wasSignedIn)
    {
      string text = "auth_callback_refused";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["reason"] = reason;
      jsonObject["was_signed_in"] = wasSignedIn;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000A97 RID: 2711 RVA: 0x003759E8 File Offset: 0x003759E8
    [NullableContext(2)]
    public static void TrackChromeJobAction([Nullable(1)] string action, string threadId = null, JsonObject properties = null)
    {
      JsonObject jsonObject;
      if ((jsonObject = ((properties != null) ? properties.DeepClone().AsObject() : null)) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      jsonObject2["action"] = action;
      ClickyAnalytics.TrackProductEvent("job_action", null, threadId, jsonObject2);
    }

    // Token: 0x17000249 RID: 585
    // (get) Token: 0x06000A98 RID: 2712 RVA: 0x00375A38 File Offset: 0x00375A38
    public static IAnalyticsSink Sink
    {
      get
      {
        return ClickyAnalytics.sink;
      }
    }

    // Token: 0x1700024A RID: 586
    // (get) Token: 0x06000A99 RID: 2713 RVA: 0x00375A40 File Offset: 0x00375A40
    public static bool IsSendingToPostHog
    {
      get
      {
        return ClickyAnalytics.sink is PostHogAnalyticsSink;
      }
    }

    // Token: 0x1700024B RID: 587
    // (get) Token: 0x06000A9A RID: 2714 RVA: 0x00375A50 File Offset: 0x00375A50
    public static string DistinctId
    {
      get
      {
        object gate = ClickyAnalytics.Gate;
        string text;
        lock (gate)
        {
          text = ClickyAnalytics.identifiedId ?? ClickyAnalytics.anonymousId;
        }
        return text;
      }
    }

    // Token: 0x1700024C RID: 588
    // (get) Token: 0x06000A9B RID: 2715 RVA: 0x00375A9C File Offset: 0x00375A9C
    public static string AppVersion
    {
      get
      {
        return ClickyAnalytics.appVersion;
      }
    }

    // Token: 0x1700024D RID: 589
    // (get) Token: 0x06000A9C RID: 2716 RVA: 0x00375AA4 File Offset: 0x00375AA4
    // (set) Token: 0x06000A9D RID: 2717 RVA: 0x00375AAC File Offset: 0x00375AAC
    [Nullable(2)]
    public static Func<JsonObject> FrontmostApplicationProperties
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000A9E RID: 2718 RVA: 0x00375AB4 File Offset: 0x00375AB4
    public static void Configure(TelemetryConfiguration config, ClickyEndpoints endpoints, ClientIdentity identity, [Nullable(2)] HttpClient http = null, [Nullable(2)] IAnalyticsSink overrideSink = null, [Nullable(2)] string identityFile = null)
    {
      if (identityFile != null)
      {
        ClickyAnalytics.identityFilePath = identityFile;
      }
      ClickyAnalytics.appVersion = identity.AppVersion;
      ClickyAnalytics.appBuild = identity.AppBuild;
      IAnalyticsSink analyticsSink;
      string text;
      if (overrideSink != null)
      {
        analyticsSink = overrideSink;
        text = "given";
      }
      else if (Environment.GetEnvironmentVariable("HEYCLICKY_ANALYTICS_DEBUG") == "1")
      {
        analyticsSink = new LogAnalyticsSink();
        text = "HEYCLICKY_ANALYTICS_DEBUG=1";
      }
      else if (!config.HasPostHogKey)
      {
        analyticsSink = NullAnalyticsSink.Instance;
        text = "no PostHog key configured";
      }
      else if (AppBundleConfiguration.IsLocalDevelopmentConfiguration(endpoints))
      {
        analyticsSink = NullAnalyticsSink.Instance;
        text = "loopback endpoints";
      }
      else
      {
        analyticsSink = new PostHogAnalyticsSink(http ?? new HttpClient(), config.PostHogHost, config.PostHogApiKey);
        text = config.PostHogHost;
      }
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        IDisposable disposable = ClickyAnalytics.sink as IDisposable;
        if (disposable != null && disposable != analyticsSink)
        {
          disposable.Dispose();
        }
        ClickyAnalytics.sink = analyticsSink;
        ClickyAnalytics.LoadIdentity();
      }
      ClickyAnalytics.RegisterClientIdentitySuperProperties(identity);
      string text2 = "analytics";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
      defaultInterpolatedStringHandler.AppendLiteral("sink: ");
      defaultInterpolatedStringHandler.AppendFormatted(analyticsSink.Name);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06000A9F RID: 2719 RVA: 0x00375C14 File Offset: 0x00375C14
    [NullableContext(2)]
    private static void RegisterClientIdentitySuperProperties(ClientIdentity identity = null)
    {
      if (identity == null)
      {
        identity = ClickyAnalytics.clientIdentity ?? ClickyClientIdentity.Current;
      }
      ClickyAnalytics.clientIdentity = identity;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["clicky_device_key"] = identity.DeviceId;
      jsonObject["clicky_platform"] = identity.Platform;
      ClickyAnalytics.Register(jsonObject);
    }

    // Token: 0x06000AA0 RID: 2720 RVA: 0x00375C7C File Offset: 0x00375C7C
    [NullableContext(2)]
    public static void Capture([Nullable(1)] string eventName, JsonObject properties = null, JsonObject set = null, JsonObject setOnce = null)
    {
      try
      {
        JsonObject jsonObject = new JsonObject(null);
        object gate = ClickyAnalytics.Gate;
        IAnalyticsSink analyticsSink;
        string text;
        lock (gate)
        {
          analyticsSink = ClickyAnalytics.sink;
          text = ClickyAnalytics.identifiedId ?? ClickyAnalytics.anonymousId;
          foreach (KeyValuePair<string, JsonNode> keyValuePair in ClickyAnalytics.SuperProperties)
          {
            string text2;
            JsonNode jsonNode;
            keyValuePair.Deconstruct(out text2, out jsonNode);
            string text3 = text2;
            JsonNode jsonNode2 = jsonNode;
            jsonObject[text3] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
          }
        }
        if (!(analyticsSink is NullAnalyticsSink))
        {
          jsonObject["$lib"] = "heyclicky-windows";
          jsonObject["$lib_version"] = ClickyAnalytics.appVersion;
          jsonObject["$app_version"] = ClickyAnalytics.appVersion;
          jsonObject["$app_build"] = ClickyAnalytics.appBuild;
          jsonObject["$os"] = "Windows";
          jsonObject["$os_version"] = ClickyClientIdentity.OsVersion();
          jsonObject["$device_type"] = "Desktop";
          if (properties != null)
          {
            foreach (KeyValuePair<string, JsonNode> keyValuePair in properties)
            {
              string text2;
              JsonNode jsonNode;
              keyValuePair.Deconstruct(out text2, out jsonNode);
              string text4 = text2;
              JsonNode jsonNode3 = jsonNode;
              jsonObject[text4] = ((jsonNode3 != null) ? jsonNode3.DeepClone() : null);
            }
          }
          if (set != null)
          {
            jsonObject["$set"] = set.DeepClone();
          }
          if (setOnce != null)
          {
            jsonObject["$set_once"] = setOnce.DeepClone();
          }
          analyticsSink.Enqueue(new AnalyticsEvent(eventName, text, jsonObject, DateTimeOffset.UtcNow, Guid.NewGuid()));
        }
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "capture " + eventName + " failed: " + ex.Message);
      }
    }

    // Token: 0x06000AA1 RID: 2721 RVA: 0x00375EE8 File Offset: 0x00375EE8
    public static void Register(JsonObject properties)
    {
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        foreach (KeyValuePair<string, JsonNode> keyValuePair in properties)
        {
          string text;
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text, out jsonNode);
          string text2 = text;
          JsonNode jsonNode2 = jsonNode;
          ClickyAnalytics.SuperProperties[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
        }
      }
    }

    // Token: 0x06000AA2 RID: 2722 RVA: 0x00375F7C File Offset: 0x00375F7C
    public static void Unregister(string key)
    {
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        ClickyAnalytics.SuperProperties.Remove(key);
      }
    }

    // Token: 0x06000AA3 RID: 2723 RVA: 0x00375FC4 File Offset: 0x00375FC4
    public static void Identify(string userId, [Nullable(2)] JsonObject userProperties = null)
    {
      object gate = ClickyAnalytics.Gate;
      string text;
      lock (gate)
      {
        text = ClickyAnalytics.identifiedId ?? ClickyAnalytics.anonymousId;
        if (ClickyAnalytics.identifiedId == userId && userProperties == null)
        {
          return;
        }
        ClickyAnalytics.identifiedId = userId;
        ClickyAnalytics.SaveIdentity();
      }
      JsonObject jsonObject = new JsonObject(null);
      if (text != userId)
      {
        jsonObject["$anon_distinct_id"] = text;
      }
      ClickyAnalytics.Capture("$identify", jsonObject, userProperties, null);
    }

    // Token: 0x06000AA4 RID: 2724 RVA: 0x00376060 File Offset: 0x00376060
    public static void Reset()
    {
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        ClickyAnalytics.identifiedId = null;
        ClickyAnalytics.anonymousId = Guid.NewGuid().ToString();
        ClickyAnalytics.SuperProperties.Clear();
        ClickyAnalytics.SaveIdentity();
      }
    }

    // Token: 0x06000AA5 RID: 2725 RVA: 0x003760C8 File Offset: 0x003760C8
    public static Task FlushAsync()
    {
      return ClickyAnalytics.sink.FlushAsync();
    }

    // Token: 0x06000AA6 RID: 2726 RVA: 0x003760D4 File Offset: 0x003760D4
    private static void LoadIdentity()
    {
      try
      {
        if (!File.Exists(ClickyAnalytics.identityFilePath))
        {
          ClickyAnalytics.SaveIdentity();
        }
        else
        {
          using (JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllBytes(ClickyAnalytics.identityFilePath), default(JsonDocumentOptions)))
          {
            JsonElement rootElement = jsonDocument.RootElement;
            JsonElement jsonElement;
            if (rootElement.TryGetProperty("anonymous_id", out jsonElement))
            {
              string @string = jsonElement.GetString();
              if (@string != null && @string.Length > 0)
              {
                ClickyAnalytics.anonymousId = @string;
              }
            }
            JsonElement jsonElement2;
            ClickyAnalytics.identifiedId = ((rootElement.TryGetProperty("distinct_id", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.String) ? jsonElement2.GetString() : null);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "identity file ignored: " + ex.Message);
      }
    }

    // Token: 0x06000AA7 RID: 2727 RVA: 0x003761BC File Offset: 0x003761BC
    private static void SaveIdentity()
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(ClickyAnalytics.identityFilePath));
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["anonymous_id"] = ClickyAnalytics.anonymousId;
        jsonObject["distinct_id"] = ClickyAnalytics.identifiedId;
        JsonObject jsonObject2 = jsonObject;
        File.WriteAllText(ClickyAnalytics.identityFilePath, jsonObject2.ToJsonString(null));
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "identity file not saved: " + ex.Message);
      }
    }

    // Token: 0x06000AA8 RID: 2728 RVA: 0x00376254 File Offset: 0x00376254
    private static JsonObject WithFrontmostApplication(JsonObject properties)
    {
      try
      {
        Func<JsonObject> frontmostApplicationProperties = ClickyAnalytics.FrontmostApplicationProperties;
        JsonObject jsonObject = ((frontmostApplicationProperties != null) ? frontmostApplicationProperties() : null);
        if (jsonObject != null)
        {
          foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject)
          {
            string text;
            JsonNode jsonNode;
            keyValuePair.Deconstruct(out text, out jsonNode);
            string text2 = text;
            JsonNode jsonNode2 = jsonNode;
            if (!properties.ContainsKey(text2))
            {
              properties[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "foreground app: " + ex.Message);
      }
      return properties;
    }

    // Token: 0x06000AA9 RID: 2729 RVA: 0x00376308 File Offset: 0x00376308
    private static JsonObject Merge(JsonObject properties, [Nullable(2)] JsonObject extra)
    {
      if (extra != null)
      {
        foreach (KeyValuePair<string, JsonNode> keyValuePair in extra)
        {
          string text;
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text, out jsonNode);
          string text2 = text;
          JsonNode jsonNode2 = jsonNode;
          properties[text2] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
        }
      }
      return properties;
    }

    // Token: 0x06000AAA RID: 2730 RVA: 0x00376374 File Offset: 0x00376374
    [NullableContext(2)]
    private static void Breadcrumb([Nullable(1)] string category, string message = null, CrashReportLevel level = CrashReportLevel.Info, JsonObject data = null)
    {
      try
      {
        ICrashReporter crashReporter = CrashReporter.Current;
        if (crashReporter.IsEnabled)
        {
          crashReporter.AddBreadcrumb(category, message, level, data);
        }
      }
      catch
      {
      }
    }

    // Token: 0x06000AAB RID: 2731 RVA: 0x003763B0 File Offset: 0x003763B0
    private static JsonArray Array(IEnumerable<string> items)
    {
      JsonArray jsonArray = new JsonArray(null);
      foreach (string text in items)
      {
        jsonArray.Add<string>(text);
      }
      return jsonArray;
    }

    // Token: 0x06000AAC RID: 2732 RVA: 0x00376408 File Offset: 0x00376408
    public static void TrackComputerUseConsent(bool approved, bool always)
    {
      string text = "computer_use_consent";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["approved"] = approved;
      jsonObject["always"] = always;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AAD RID: 2733 RVA: 0x00376454 File Offset: 0x00376454
    public static void TrackOneBrainNothingHeard(double heldSeconds, bool earsFailed, bool showedMicrophoneTrouble)
    {
      string text = "one_brain_nothing_heard";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["held_seconds"] = Math.Round(heldSeconds, 1);
      jsonObject["ears_failed"] = earsFailed;
      jsonObject["showed_microphone_trouble"] = showedMicrophoneTrouble;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x1700024E RID: 590
    // (get) Token: 0x06000AAE RID: 2734 RVA: 0x003764B4 File Offset: 0x003764B4
    // (set) Token: 0x06000AAF RID: 2735 RVA: 0x003764BC File Offset: 0x003764BC
    [TupleElementNames(new string[] { "UserId", "Email" })]
    [Nullable(new byte[] { 2, 0, 2, 2 })]
    public static Func<ValueTuple<string, string>> CurrentAccount
    {
      [return: TupleElementNames(new string[] { "UserId", "Email" })]
      [return: Nullable(new byte[] { 2, 0, 2, 2 })]
      get;
      [param: TupleElementNames(new string[] { "UserId", "Email" })]
      [param: Nullable(new byte[] { 2, 0, 2, 2 })]
      set;
    }

    // Token: 0x1700024F RID: 591
    // (get) Token: 0x06000AB0 RID: 2736 RVA: 0x003764C4 File Offset: 0x003764C4
    // (set) Token: 0x06000AB1 RID: 2737 RVA: 0x003764CC File Offset: 0x003764CC
    [Nullable(new byte[] { 2, 1 })]
    public static Action<ErrorReport> ErrorRelay
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x06000AB2 RID: 2738 RVA: 0x003764D4 File Offset: 0x003764D4
    public static IReadOnlyList<ClickyAnalytics.RecentAppError> RecentAppErrorsSnapshot()
    {
      object recentAppErrorsGate = ClickyAnalytics.RecentAppErrorsGate;
      IReadOnlyList<ClickyAnalytics.RecentAppError> readOnlyList;
      lock (recentAppErrorsGate)
      {
        readOnlyList = ClickyAnalytics.RecentAppErrorsStorage.ToArray();
      }
      return readOnlyList;
    }

    // Token: 0x06000AB3 RID: 2739 RVA: 0x0037651C File Offset: 0x0037651C
    public static void TrackAppError(string stage, string kind, string errorMessage, [Nullable(2)] string errorDomain, int errorCode, string userFacingMessage, bool relayToWorker = true)
    {
      string text = "app_error";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["stage"] = stage;
      jsonObject["error_kind"] = kind;
      jsonObject["error_message"] = errorMessage;
      jsonObject["error_domain"] = errorDomain ?? "";
      jsonObject["error_code"] = errorCode;
      jsonObject["user_facing_message"] = userFacingMessage;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      string text2 = "app_error";
      string text3 = stage + ": " + errorMessage;
      CrashReportLevel crashReportLevel = CrashReportLevel.Error;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["stage"] = stage;
      jsonObject2["error_kind"] = kind;
      jsonObject2["error_code"] = errorCode;
      ClickyAnalytics.Breadcrumb(text2, text3, crashReportLevel, jsonObject2);
      object recentAppErrorsGate = ClickyAnalytics.RecentAppErrorsGate;
      lock (recentAppErrorsGate)
      {
        ClickyAnalytics.RecentAppErrorsStorage.Add(new ClickyAnalytics.RecentAppError(DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"), stage, kind, errorMessage, errorDomain, errorCode, userFacingMessage));
        if (ClickyAnalytics.RecentAppErrorsStorage.Count > 25)
        {
          ClickyAnalytics.RecentAppErrorsStorage.RemoveRange(0, ClickyAnalytics.RecentAppErrorsStorage.Count - 25);
        }
      }
      ClickyAnalytics.CaptureAppErrorToCrashReporter(stage, kind, errorMessage, errorDomain, errorCode, userFacingMessage);
      string text4 = "app_error";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 6);
      defaultInterpolatedStringHandler.AppendFormatted(stage);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(kind);
      defaultInterpolatedStringHandler.AppendLiteral(", ");
      defaultInterpolatedStringHandler.AppendFormatted(errorDomain ?? "none");
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted<int>(errorCode);
      defaultInterpolatedStringHandler.AppendLiteral("): ");
      defaultInterpolatedStringHandler.AppendFormatted(errorMessage);
      defaultInterpolatedStringHandler.AppendLiteral(" | shown: \"");
      defaultInterpolatedStringHandler.AppendFormatted(userFacingMessage);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      Log.Warn(text4, defaultInterpolatedStringHandler.ToStringAndClear());
      if (!relayToWorker)
      {
        return;
      }
      try
      {
        Func<ValueTuple<string, string>> currentAccount = ClickyAnalytics.CurrentAccount;
        ValueTuple<string, string> valueTuple = ((currentAccount != null) ? currentAccount() : new ValueTuple<string, string>(null, null));
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        Action<ErrorReport> errorRelay = ClickyAnalytics.ErrorRelay;
        if (errorRelay != null)
        {
          errorRelay(new ErrorReport(stage, kind, errorMessage, userFacingMessage, ClickyAnalytics.appVersion, errorCode, errorDomain, item, item2));
        }
      }
      catch (Exception ex)
      {
        Log.Warn("app_error", "relay failed: " + ex.Message);
      }
    }

    // Token: 0x06000AB4 RID: 2740 RVA: 0x003767B4 File Offset: 0x003767B4
    public static void TrackAppError(string stage, Exception error, string userFacingMessage, string kind = "generic")
    {
      ClickyAnalytics.TrackAppError(stage, kind, ClickyAnalytics.DiagnosticMessage(error), ClickyAnalytics.ErrorDomain(error), ClickyAnalytics.ErrorCode(error), userFacingMessage, true);
    }

    // Token: 0x06000AB5 RID: 2741 RVA: 0x003767D4 File Offset: 0x003767D4
    public static string DiagnosticMessage(Exception error)
    {
      List<string> list = new List<string> { ClickyAnalytics.Cap(error.Message) };
      WorkerHttpException ex = error as WorkerHttpException;
      if (ex != null)
      {
        list.Add("failing_url=" + ex.Path);
      }
      Exception innerException = error.InnerException;
      if (innerException != null)
      {
        list.Add("underlying=" + innerException.GetType().Name + ": " + ClickyAnalytics.Cap(innerException.Message));
      }
      return string.Join(" · ", list);
    }

    // Token: 0x06000AB6 RID: 2742 RVA: 0x00376858 File Offset: 0x00376858
    public static string ErrorDomain(Exception error)
    {
      string text;
      if (error is HttpRequestException || error is TaskCanceledException || error is TimeoutException || error is SocketException || error is WebSocketException)
      {
        text = "network";
      }
      else
      {
        text = error.GetType().FullName ?? error.GetType().Name;
      }
      return text;
    }

    // Token: 0x06000AB7 RID: 2743 RVA: 0x003768B4 File Offset: 0x003768B4
    public static int ErrorCode(Exception error)
    {
      WorkerHttpException ex = error as WorkerHttpException;
      int num;
      if (ex == null)
      {
        HttpRequestException ex2 = error as HttpRequestException;
        if (ex2 != null)
        {
          HttpStatusCode? statusCode = ex2.StatusCode;
          if (statusCode != null)
          {
            return (int)statusCode.GetValueOrDefault();
          }
        }
        num = error.HResult;
      }
      else
      {
        num = ex.Status;
      }
      return num;
    }

    // Token: 0x06000AB8 RID: 2744 RVA: 0x00376908 File Offset: 0x00376908
    private static string Cap(string text)
    {
      if (text.Length > 500)
      {
        return text.Substring(0, 500);
      }
      return text;
    }

    // Token: 0x06000AB9 RID: 2745 RVA: 0x00376928 File Offset: 0x00376928
    private static bool ShouldEmitCrashReporterEvent(string fingerprintKey)
    {
      DateTimeOffset now = DateTimeOffset.UtcNow;
      Dictionary<string, DateTimeOffset> lastCapturedAt = ClickyAnalytics.LastCapturedAt;
      bool flag2;
      lock (lastCapturedAt)
      {
        DateTimeOffset dateTimeOffset;
        if (ClickyAnalytics.LastCapturedAt.TryGetValue(fingerprintKey, out dateTimeOffset) && now - dateTimeOffset < ClickyAnalytics.ThrottleWindow)
        {
          flag2 = false;
        }
        else
        {
          ClickyAnalytics.LastCapturedAt[fingerprintKey] = now;
          IEnumerable<KeyValuePair<string, DateTimeOffset>> lastCapturedAt2 = ClickyAnalytics.LastCapturedAt;
          Func<KeyValuePair<string, DateTimeOffset>, bool> <>9__0;
          Func<KeyValuePair<string, DateTimeOffset>, bool> func;
          if ((func = <>9__0) == null)
          {
            func = (<>9__0 = (KeyValuePair<string, DateTimeOffset> p) => now - p.Value >= ClickyAnalytics.ThrottleWindow);
          }
          foreach (string text in (from p in lastCapturedAt2.Where<KeyValuePair<string, DateTimeOffset>>(func)
            select p.Key).ToList<string>())
          {
            ClickyAnalytics.LastCapturedAt.Remove(text);
          }
          flag2 = true;
        }
      }
      return flag2;
    }

    // Token: 0x06000ABA RID: 2746 RVA: 0x00376A50 File Offset: 0x00376A50
    private static void CaptureAppErrorToCrashReporter(string stage, string kind, string errorMessage, [Nullable(2)] string errorDomain, int errorCode, string userFacingMessage)
    {
      ICrashReporter crashReporter = CrashReporter.Current;
      if (!crashReporter.IsEnabled || kind == "credits_exhausted")
      {
        return;
      }
      string text = errorDomain ?? "none";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 3);
      defaultInterpolatedStringHandler.AppendLiteral("app_error|");
      defaultInterpolatedStringHandler.AppendFormatted(stage);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(errorCode);
      if (!ClickyAnalytics.ShouldEmitCrashReporterEvent(defaultInterpolatedStringHandler.ToStringAndClear()))
      {
        return;
      }
      try
      {
        ICrashReporter crashReporter2 = crashReporter;
        string text2 = "App Error: " + stage;
        CrashReportLevel crashReportLevel = ((text == "network") ? CrashReportLevel.Warning : CrashReportLevel.Error);
        IReadOnlyList<string> readOnlyList = new <>z__ReadOnlyArray<string>(new string[]
        {
          "app_error",
          stage,
          text,
          errorCode.ToString()
        });
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["app_error_stage"] = stage;
        dictionary["app_error_kind"] = kind;
        dictionary["app_error_domain"] = text;
        dictionary["app_error_code"] = errorCode.ToString();
        string text3 = "app_error";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["stage"] = stage;
        jsonObject["kind"] = kind;
        jsonObject["error_message"] = errorMessage;
        jsonObject["error_domain"] = errorDomain ?? "";
        jsonObject["error_code"] = errorCode;
        jsonObject["user_facing_message"] = userFacingMessage;
        crashReporter2.CaptureMessage(text2, crashReportLevel, readOnlyList, dictionary, text3, jsonObject, null);
      }
      catch (Exception ex)
      {
        Log.Warn("app_error", "crash reporter capture failed: " + ex.Message);
      }
    }

    // Token: 0x06000ABB RID: 2747 RVA: 0x00376C20 File Offset: 0x00376C20
    [NullableContext(2)]
    public static void ReportInternalError([Nullable(1)] string subsystem, [Nullable(1)] string stage, Exception error = null, string detail = null, CrashReportLevel severity = CrashReportLevel.Error, string distinguishingKey = null, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> extraTags = null)
    {
      string text = ((error == null) ? null : ClickyAnalytics.ErrorDomain(error));
      int num = ((error == null) ? 0 : ClickyAnalytics.ErrorCode(error));
      string text2 = ((error != null) ? ClickyAnalytics.DiagnosticMessage(error) : (detail ?? "no error object"));
      string text3 = "internal_error";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
      defaultInterpolatedStringHandler.AppendFormatted(subsystem);
      defaultInterpolatedStringHandler.AppendLiteral("/");
      defaultInterpolatedStringHandler.AppendFormatted(stage);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      string text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["subsystem"] = subsystem;
      jsonObject["stage"] = stage;
      jsonObject["error_domain"] = text ?? "";
      jsonObject["error_code"] = num;
      ClickyAnalytics.Breadcrumb(text3, text4, severity, jsonObject);
      ICrashReporter crashReporter = CrashReporter.Current;
      if (!crashReporter.IsEnabled)
      {
        return;
      }
      string text5 = text ?? "none";
      string text6 = ((distinguishingKey == null) ? "" : ("|" + distinguishingKey));
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 5);
      defaultInterpolatedStringHandler.AppendLiteral("internal_error|");
      defaultInterpolatedStringHandler.AppendFormatted(subsystem);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(stage);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted(text5);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<int>(num);
      defaultInterpolatedStringHandler.AppendFormatted(text6);
      if (!ClickyAnalytics.ShouldEmitCrashReporterEvent(defaultInterpolatedStringHandler.ToStringAndClear()))
      {
        return;
      }
      List<string> list = new List<string>
      {
        "internal_error",
        subsystem,
        stage,
        text5,
        num.ToString()
      };
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["internal_error_subsystem"] = subsystem;
      dictionary["internal_error_stage"] = stage;
      dictionary["internal_error_domain"] = text5;
      dictionary["internal_error_code"] = num.ToString();
      Dictionary<string, string> dictionary2 = dictionary;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["subsystem"] = subsystem;
      jsonObject2["stage"] = stage;
      jsonObject2["error_message"] = text2;
      jsonObject2["error_domain"] = text ?? "";
      jsonObject2["error_code"] = num;
      JsonObject jsonObject3 = jsonObject2;
      if (detail != null)
      {
        jsonObject3["detail"] = detail;
      }
      if (distinguishingKey != null)
      {
        list.Add(distinguishingKey);
        dictionary2["internal_error_distinguisher"] = distinguishingKey;
        jsonObject3["distinguisher"] = distinguishingKey;
      }
      if (extraTags != null)
      {
        foreach (KeyValuePair<string, string> keyValuePair in extraTags)
        {
          string text7;
          string text8;
          keyValuePair.Deconstruct(out text7, out text8);
          string text9 = text7;
          string text10 = text8;
          dictionary2.TryAdd(text9, text10);
        }
      }
      try
      {
        crashReporter.CaptureMessage("Internal Error: " + subsystem + "/" + stage, severity, list, dictionary2, "internal_error", jsonObject3, null);
      }
      catch (Exception ex)
      {
        Log.Warn("internal_error", "crash reporter capture failed: " + ex.Message);
      }
    }

    // Token: 0x06000ABC RID: 2748 RVA: 0x00376F84 File Offset: 0x00376F84
    public static void TrackAppOpened()
    {
      string text = "app_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["app_version"] = ClickyAnalytics.appVersion;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ABD RID: 2749 RVA: 0x00376FC0 File Offset: 0x00376FC0
    public static void TrackLocateClickyClicked()
    {
      ClickyAnalytics.Capture("locate_clicky_clicked", null, null, null);
    }

    // Token: 0x06000ABE RID: 2750 RVA: 0x00376FD0 File Offset: 0x00376FD0
    public static void TrackLibraryModeToggled(bool isEnabled)
    {
      ClickyAnalytics.Capture(isEnabled ? "library_mode_enabled" : "library_mode_disabled", null, null, null);
    }

    // Token: 0x06000ABF RID: 2751 RVA: 0x00376FEC File Offset: 0x00376FEC
    public static void TrackPreSignInIntroDismissed()
    {
      ClickyAnalytics.Capture("pre_sign_in_intro_dismissed", null, null, null);
    }

    // Token: 0x06000AC0 RID: 2752 RVA: 0x00376FFC File Offset: 0x00376FFC
    public static void TrackPreSignInIntroStepViewed(string step)
    {
      string text = "intro_viewed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["step"] = step;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AC1 RID: 2753 RVA: 0x00377034 File Offset: 0x00377034
    public static void TrackPreSignInIntroButtonTapped(string step, string action, [Nullable(2)] string value = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["step"] = step;
      jsonObject["action"] = action;
      JsonObject jsonObject2 = jsonObject;
      if (value != null)
      {
        jsonObject2["value"] = value;
      }
      ClickyAnalytics.Capture("intro_button_tapped", jsonObject2, null, null);
    }

    // Token: 0x06000AC2 RID: 2754 RVA: 0x00377094 File Offset: 0x00377094
    public static void TrackPreSignInIntroAttentionRequested(string step)
    {
      string text = "intro_attention_requested";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["step"] = step;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AC3 RID: 2755 RVA: 0x003770CC File Offset: 0x003770CC
    public static void TrackPreSignInIntroNotchWelcomeShown()
    {
      ClickyAnalytics.Capture("intro_notch_welcome_shown", null, null, null);
    }

    // Token: 0x06000AC4 RID: 2756 RVA: 0x003770DC File Offset: 0x003770DC
    public static void TrackDockIconPanelOpened()
    {
      ClickyAnalytics.Capture("dock_reopen_panel_shown", null, null, null);
    }

    // Token: 0x06000AC5 RID: 2757 RVA: 0x003770EC File Offset: 0x003770EC
    public static void TrackPreSignInIntroNotchFinalSetupTapped()
    {
      ClickyAnalytics.Capture("intro_notch_final_setup_tapped", null, null, null);
    }

    // Token: 0x06000AC6 RID: 2758 RVA: 0x003770FC File Offset: 0x003770FC
    public static void TrackDiscoveryChannelSelected(string channel, [Nullable(2)] string otherDetail = null)
    {
      string text = (string.IsNullOrEmpty(otherDetail) ? null : otherDetail);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["channel"] = channel;
      JsonObject jsonObject2 = jsonObject;
      if (text != null)
      {
        jsonObject2["channel_other"] = text;
      }
      ClickyAnalytics.Capture("discovery_channel_selected", jsonObject2, null, null);
      string text2 = ((text == null) ? channel : (channel + "|" + text));
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        if (ClickyAnalytics.lastSyncedDiscoveryChannel == text2)
        {
          return;
        }
        ClickyAnalytics.lastSyncedDiscoveryChannel = text2;
      }
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["discovery_channel"] = channel;
      ClickyAnalytics.Register(jsonObject3);
      if (text != null)
      {
        string text3 = "$set";
        JsonObject jsonObject4 = null;
        JsonObject jsonObject5 = new JsonObject(null);
        jsonObject5["discovery_channel"] = channel;
        jsonObject5["discovery_channel_other"] = text;
        ClickyAnalytics.Capture(text3, jsonObject4, jsonObject5, null);
        return;
      }
      string text4 = "$set";
      JsonObject jsonObject6 = new JsonObject(null);
      string text5 = "$unset";
      JsonNode jsonNode = "discovery_channel_other";
      jsonObject6[text5] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode));
      JsonObject jsonObject7 = new JsonObject(null);
      jsonObject7["discovery_channel"] = channel;
      ClickyAnalytics.Capture(text4, jsonObject6, jsonObject7, null);
    }

    // Token: 0x06000AC7 RID: 2759 RVA: 0x00377278 File Offset: 0x00377278
    [NullableContext(2)]
    public static void IdentifyUser([Nullable(1)] string supabaseUserId, string email, string name)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["email"] = email ?? "";
      jsonObject["name"] = name ?? "";
      ClickyAnalytics.Identify(supabaseUserId, jsonObject);
      ClickyAnalytics.FlushAsync();
      try
      {
        if (CrashReporter.Current.IsEnabled)
        {
          CrashReporter.Current.SetUser(supabaseUserId, string.IsNullOrEmpty(email) ? null : email, string.IsNullOrEmpty(name) ? null : name);
        }
      }
      catch
      {
      }
    }

    // Token: 0x06000AC8 RID: 2760 RVA: 0x00377318 File Offset: 0x00377318
    public static void UpdateUserPlanProperties(string planName, string planDisplayName, bool isPaidUser)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["plan"] = planName;
      jsonObject["plan_name"] = planDisplayName;
      jsonObject["is_paid_user"] = isPaidUser;
      ClickyAnalytics.Register(jsonObject);
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        ValueTuple<string, string, bool>? valueTuple = ClickyAnalytics.lastSyncedPlan;
        bool flag2 = valueTuple != null;
        bool flag3;
        if (!flag2)
        {
          flag3 = false;
        }
        else if (!flag2)
        {
          flag3 = true;
        }
        else
        {
          ValueTuple<string, string, bool> valueOrDefault = valueTuple.GetValueOrDefault();
          flag3 = valueOrDefault.Item1 == planName && valueOrDefault.Item2 == planDisplayName && valueOrDefault.Item3 == isPaidUser;
        }
        if (flag3)
        {
          return;
        }
        ClickyAnalytics.lastSyncedPlan = new ValueTuple<string, string, bool>?(new ValueTuple<string, string, bool>(planName, planDisplayName, isPaidUser));
      }
      string text = "$set";
      JsonObject jsonObject2 = null;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["plan"] = planName;
      jsonObject3["plan_name"] = planDisplayName;
      jsonObject3["is_paid_user"] = isPaidUser;
      ClickyAnalytics.Capture(text, jsonObject2, jsonObject3, null);
    }

    // Token: 0x06000AC9 RID: 2761 RVA: 0x00377458 File Offset: 0x00377458
    public static void ResetIdentity()
    {
      ClickyAnalytics.Unregister("plan");
      ClickyAnalytics.Unregister("plan_name");
      ClickyAnalytics.Unregister("is_paid_user");
      object obj = ClickyAnalytics.Gate;
      lock (obj)
      {
        ClickyAnalytics.lastSyncedPlan = null;
        ClickyAnalytics.lastSyncedDiscoveryChannel = null;
      }
      ClickyAnalytics.Reset();
      ClickyAnalytics.RegisterClientIdentitySuperProperties(null);
      try
      {
        if (CrashReporter.Current.IsEnabled)
        {
          CrashReporter.Current.SetUser(null, null, null);
          CrashReporter.Current.ClearBreadcrumbs();
        }
      }
      catch
      {
      }
      obj = ClickyAnalytics.RecentAppErrorsGate;
      lock (obj)
      {
        ClickyAnalytics.RecentAppErrorsStorage.Clear();
      }
    }

    // Token: 0x06000ACA RID: 2762 RVA: 0x00377534 File Offset: 0x00377534
    public static void TrackGoogleSignUp(string email)
    {
      string text = "google_sign_up";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["email"] = email;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ACB RID: 2763 RVA: 0x0037756C File Offset: 0x0037756C
    public static void TrackGoogleSignIn(string email)
    {
      string text = "google_sign_in";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["email"] = email;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ACC RID: 2764 RVA: 0x003775A4 File Offset: 0x003775A4
    public static void TrackAccountDeleted()
    {
      ClickyAnalytics.Capture("account_deleted", null, null, null);
    }

    // Token: 0x06000ACD RID: 2765 RVA: 0x003775B4 File Offset: 0x003775B4
    public static void FlushPendingEvents()
    {
      ClickyAnalytics.FlushAsync();
    }

    // Token: 0x06000ACE RID: 2766 RVA: 0x003775BC File Offset: 0x003775BC
    public static void TrackOnboardingStarted()
    {
      ClickyAnalytics.Capture("onboarding_started", null, null, null);
    }

    // Token: 0x06000ACF RID: 2767 RVA: 0x003775CC File Offset: 0x003775CC
    [NullableContext(2)]
    public static void TrackOnboardingVideoStarted(JsonObject properties = null)
    {
      ClickyAnalytics.Capture("onboarding_video_started", properties, null, null);
    }

    // Token: 0x06000AD0 RID: 2768 RVA: 0x003775DC File Offset: 0x003775DC
    public static void TrackOnboardingVideoEscapePressed(string pressType, double secondsRemaining)
    {
      string text = "onboarding_video_escape_pressed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["press_type"] = pressType;
      jsonObject["seconds_remaining"] = Math.Max(0.0, secondsRemaining);
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AD1 RID: 2769 RVA: 0x00377634 File Offset: 0x00377634
    public static void TrackOnboardingReplayed()
    {
      ClickyAnalytics.Capture("onboarding_replayed", null, null, null);
    }

    // Token: 0x06000AD2 RID: 2770 RVA: 0x00377644 File Offset: 0x00377644
    public static void TrackOnboardingVideoCompleted()
    {
      ClickyAnalytics.Capture("onboarding_video_completed", null, null, null);
    }

    // Token: 0x06000AD3 RID: 2771 RVA: 0x00377654 File Offset: 0x00377654
    public static void TrackHostedVideoFailure(JsonObject properties)
    {
      ClickyAnalytics.Capture("hosted_video_failed", properties, null, null);
    }

    // Token: 0x06000AD4 RID: 2772 RVA: 0x00377664 File Offset: 0x00377664
    public static void TrackOnboardingDemoTriggered()
    {
      ClickyAnalytics.Capture("onboarding_demo_triggered", null, null, null);
    }

    // Token: 0x06000AD5 RID: 2773 RVA: 0x00377674 File Offset: 0x00377674
    public static void TrackOnboardingTutorialInvitePresented()
    {
      ClickyAnalytics.Capture("onboarding_tutorial_invite_presented", null, null, null);
    }

    // Token: 0x06000AD6 RID: 2774 RVA: 0x00377684 File Offset: 0x00377684
    public static void TrackOnboardingTutorialStarted()
    {
      ClickyAnalytics.Capture("onboarding_tutorial_started", null, null, null);
    }

    // Token: 0x06000AD7 RID: 2775 RVA: 0x00377694 File Offset: 0x00377694
    public static void TrackOnboardingTutorialStepCompleted(string step)
    {
      string text = "onboarding_tutorial_step_completed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["step"] = step;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AD8 RID: 2776 RVA: 0x003776CC File Offset: 0x003776CC
    public static void TrackOnboardingTutorialResolved(string outcome, [Nullable(2)] string lastStep, int? durationSeconds = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["outcome"] = outcome;
      JsonObject jsonObject2 = jsonObject;
      if (lastStep != null)
      {
        jsonObject2["last_step"] = lastStep;
      }
      if (durationSeconds != null)
      {
        int valueOrDefault = durationSeconds.GetValueOrDefault();
        jsonObject2["duration_seconds"] = valueOrDefault;
      }
      ClickyAnalytics.Capture("onboarding_tutorial_resolved", jsonObject2, null, null);
    }

    // Token: 0x06000AD9 RID: 2777 RVA: 0x0037773C File Offset: 0x0037773C
    public static void TrackOnboardingTutorialStepViewed(string step, string navigation)
    {
      string text = "onboarding_tutorial_step_viewed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["step"] = step;
      jsonObject["navigation"] = navigation;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ADA RID: 2778 RVA: 0x00377788 File Offset: 0x00377788
    public static void TrackOnboardingTutorialMusicToggled(bool muted)
    {
      string text = "onboarding_tutorial_music_toggled";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["muted"] = muted;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ADB RID: 2779 RVA: 0x003777C0 File Offset: 0x003777C0
    public static void TrackOnboardingTutorialResumedFromNotch()
    {
      ClickyAnalytics.Capture("onboarding_tutorial_resumed_from_notch", null, null, null);
    }

    // Token: 0x06000ADC RID: 2780 RVA: 0x003777D0 File Offset: 0x003777D0
    public static void TrackOnboardingTutorialMicDeviceSwitched()
    {
      ClickyAnalytics.Capture("onboarding_tutorial_mic_device_switched", null, null, null);
    }

    // Token: 0x06000ADD RID: 2781 RVA: 0x003777E0 File Offset: 0x003777E0
    public static void TrackOnboardingTutorialMicCheckPassedByFallback(double secondsOnStep)
    {
      string text = "onboarding_tutorial_mic_check_passed_by_fallback";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["seconds_on_step"] = Math.Round(secondsOnStep, 1);
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000ADE RID: 2782 RVA: 0x00377820 File Offset: 0x00377820
    public static void TrackOnboardingTutorialDrawToolRan(string outcome, int? visualCount = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["outcome"] = outcome;
      JsonObject jsonObject2 = jsonObject;
      if (visualCount != null)
      {
        int valueOrDefault = visualCount.GetValueOrDefault();
        jsonObject2["visual_count"] = valueOrDefault;
      }
      ClickyAnalytics.Capture("onboarding_tutorial_draw_tool_ran", jsonObject2, null, null);
    }

    // Token: 0x06000ADF RID: 2783 RVA: 0x0037787C File Offset: 0x0037787C
    public static void TrackOnboardingTutorialSpeakerVolumeFixed(string via)
    {
      string text = "onboarding_tutorial_speaker_volume_fixed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["via"] = via;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AE0 RID: 2784 RVA: 0x003778B4 File Offset: 0x003778B4
    public static void TrackNotchFirstShown(string entryContext)
    {
      string text = "notch_first_shown";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["entry_context"] = entryContext;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AE1 RID: 2785 RVA: 0x003778EC File Offset: 0x003778EC
    public static void TrackNotchExpanded(string trigger)
    {
      string text = "notch_expanded";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["trigger"] = trigger;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AE2 RID: 2786 RVA: 0x00377924 File Offset: 0x00377924
    public static void TrackNotchCollapsed(int dwellMs)
    {
      string text = "notch_collapsed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["dwell_ms"] = dwellMs;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AE3 RID: 2787 RVA: 0x0037795C File Offset: 0x0037795C
    public static void TrackPushToTalkStarted()
    {
      ClickyAnalytics.Breadcrumb("voice", "push_to_talk_started", CrashReportLevel.Info, null);
    }

    // Token: 0x06000AE4 RID: 2788 RVA: 0x00377970 File Offset: 0x00377970
    public static void TrackPushToTalkReleased()
    {
      ClickyAnalytics.Capture("push_to_talk_released", null, null, null);
      ClickyAnalytics.Breadcrumb("voice", "push_to_talk_released", CrashReportLevel.Info, null);
    }

    // Token: 0x06000AE5 RID: 2789 RVA: 0x00377990 File Offset: 0x00377990
    public static void TrackUserMessageSent(int characterCount, string source = "voice", string route = "assistant", bool isFollowUp = false, string aiPipeline = "legacy", bool libraryMode = false, [Nullable(2)] JsonObject interactionProperties = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["character_count"] = characterCount;
      jsonObject["source"] = source;
      jsonObject["route"] = route;
      jsonObject["is_follow_up"] = isFollowUp;
      jsonObject["ai_pipeline"] = aiPipeline;
      jsonObject["library_mode"] = libraryMode;
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Merge(jsonObject2, interactionProperties);
      ClickyAnalytics.Capture("user_message_sent", jsonObject2, null, null);
      string text = "message";
      string text2 = "user_message_sent";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["source"] = source;
      jsonObject3["route"] = route;
      jsonObject3["is_follow_up"] = isFollowUp;
      jsonObject3["ai_pipeline"] = aiPipeline;
      jsonObject3["character_count"] = characterCount;
      jsonObject3["library_mode"] = libraryMode;
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
    }

    // Token: 0x06000AE6 RID: 2790 RVA: 0x00377AB8 File Offset: 0x00377AB8
    public static void TrackDictationCompleted(int wordCount, int characterCount, string outcome, [Nullable(2)] string clipboardFallbackReason, bool handsFree, bool usedBatchFallback, string engine, bool usedWarmConnection, bool usedLateFocusRecapture, int? releaseToInsertMilliseconds, int? transcriptWaitMilliseconds, int? cleanupMilliseconds)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["word_count"] = wordCount;
      jsonObject["character_count"] = characterCount;
      jsonObject["outcome"] = outcome;
      jsonObject["hands_free"] = handsFree;
      jsonObject["used_batch_fallback"] = usedBatchFallback;
      jsonObject["engine"] = engine;
      jsonObject["used_warm_connection"] = usedWarmConnection;
      jsonObject["used_late_focus_recapture"] = usedLateFocusRecapture;
      JsonObject jsonObject2 = jsonObject;
      if (clipboardFallbackReason != null)
      {
        jsonObject2["clipboard_fallback_reason"] = clipboardFallbackReason;
      }
      if (releaseToInsertMilliseconds != null)
      {
        int valueOrDefault = releaseToInsertMilliseconds.GetValueOrDefault();
        jsonObject2["release_to_insert_ms"] = valueOrDefault;
      }
      if (transcriptWaitMilliseconds != null)
      {
        int valueOrDefault2 = transcriptWaitMilliseconds.GetValueOrDefault();
        jsonObject2["transcript_wait_ms"] = valueOrDefault2;
      }
      if (cleanupMilliseconds != null)
      {
        int valueOrDefault3 = cleanupMilliseconds.GetValueOrDefault();
        jsonObject2["cleanup_ms"] = valueOrDefault3;
      }
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("dictation_completed", jsonObject2, null, null);
      string text = "dictation";
      string text2 = "dictation_completed";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["word_count"] = wordCount;
      jsonObject3["character_count"] = characterCount;
      jsonObject3["outcome"] = outcome;
      jsonObject3["clipboard_fallback_reason"] = clipboardFallbackReason ?? "none";
      jsonObject3["hands_free"] = handsFree;
      jsonObject3["used_batch_fallback"] = usedBatchFallback;
      jsonObject3["engine"] = engine;
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
    }

    // Token: 0x06000AE7 RID: 2791 RVA: 0x00377C94 File Offset: 0x00377C94
    public static void TrackWebSearchTriggered()
    {
      ClickyAnalytics.Capture("web_search_triggered", null, null, null);
    }

    // Token: 0x06000AE8 RID: 2792 RVA: 0x00377CA4 File Offset: 0x00377CA4
    public static void TrackImageSearchTriggered(string query)
    {
      string text = "image_search_triggered";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["query"] = query;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AE9 RID: 2793 RVA: 0x00377CDC File Offset: 0x00377CDC
    public static void TrackPlacesSearchTriggered()
    {
      ClickyAnalytics.Capture("places_search_triggered", null, null, null);
    }

    // Token: 0x06000AEA RID: 2794 RVA: 0x00377CEC File Offset: 0x00377CEC
    public static void TrackStockQuoteTriggered()
    {
      ClickyAnalytics.Capture("stock_quote_triggered", null, null, null);
    }

    // Token: 0x06000AEB RID: 2795 RVA: 0x00377CFC File Offset: 0x00377CFC
    public static void TrackAIResponseReceived(int characterCount, string aiPipeline = "legacy")
    {
      string text = "ai_response_received";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["character_count"] = characterCount;
      jsonObject["ai_pipeline"] = aiPipeline;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AEC RID: 2796 RVA: 0x00377D48 File Offset: 0x00377D48
    [NullableContext(2)]
    public static void TrackElementPointed(string elementLabel)
    {
      string text = "element_pointed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["element_label"] = elementLabel ?? "unknown";
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AED RID: 2797 RVA: 0x00377D8C File Offset: 0x00377D8C
    public static void TrackRealtimeSessionOpened(int mintMs, int socketOpenMs, int totalMs, bool isFirstSessionOfProcess)
    {
      string text = "realtime_session_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["mint_ms"] = mintMs;
      jsonObject["socket_open_ms"] = socketOpenMs;
      jsonObject["total_ms"] = totalMs;
      jsonObject["is_first_session_of_process"] = isFirstSessionOfProcess;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AEE RID: 2798 RVA: 0x00377DF8 File Offset: 0x00377DF8
    public static void TrackResponseError(string error)
    {
      string text = "response_error";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["error"] = error;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      string text2 = "response";
      string text3 = "response_error";
      CrashReportLevel crashReportLevel = CrashReportLevel.Error;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["error"] = error;
      ClickyAnalytics.Breadcrumb(text2, text3, crashReportLevel, jsonObject2);
    }

    // Token: 0x06000AEF RID: 2799 RVA: 0x00377E60 File Offset: 0x00377E60
    public static void TrackTTSError(string error)
    {
      string text = "tts_error";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["error"] = error;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AF0 RID: 2800 RVA: 0x00377E98 File Offset: 0x00377E98
    public static void TrackAnnotationWalkthroughCompleted(string source, int beatCount, int followUpRound)
    {
      string text = "annotation_walkthrough_completed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      jsonObject["beat_count"] = beatCount;
      jsonObject["follow_up_round"] = followUpRound;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000AF1 RID: 2801 RVA: 0x00377EF4 File Offset: 0x00377EF4
    public static void TrackBugReportSubmitted(string reportIdentifier)
    {
      string text = "bug_report_submitted";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["report_id"] = reportIdentifier;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      ClickyAnalytics.Breadcrumb("support", "bug_report_submitted", CrashReportLevel.Info, null);
    }

    // Token: 0x06000AF2 RID: 2802 RVA: 0x00377F40 File Offset: 0x00377F40
    public static void TrackFeatureRequestOpened()
    {
      ClickyAnalytics.Capture("feature_request_opened", null, null, null);
      ClickyAnalytics.Breadcrumb("support", "feature_request_opened", CrashReportLevel.Info, null);
    }

    // Token: 0x06000AF3 RID: 2803 RVA: 0x00377F60 File Offset: 0x00377F60
    public static void TrackChangelogOpened(string surface)
    {
      string text = "changelog_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      string text2 = "support";
      string text3 = "changelog_opened";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["surface"] = surface;
      ClickyAnalytics.Breadcrumb(text2, text3, crashReportLevel, jsonObject2);
    }

    // Token: 0x06000AF4 RID: 2804 RVA: 0x00377FC8 File Offset: 0x00377FC8
    public static void TrackCommunityLinkOpened(string platform)
    {
      string text = "community_link_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["platform"] = platform;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      string text2 = "support";
      string text3 = "community_link_opened";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["platform"] = platform;
      ClickyAnalytics.Breadcrumb(text2, text3, crashReportLevel, jsonObject2);
    }

    // Token: 0x06000AF5 RID: 2805 RVA: 0x00378030 File Offset: 0x00378030
    private static void AddAgentTranscriptProperties(JsonObject properties, [Nullable(2)] string transcript)
    {
      if (string.IsNullOrWhiteSpace(transcript))
      {
        return;
      }
      properties["transcript_character_count"] = transcript.Length;
    }

    // Token: 0x06000AF6 RID: 2806 RVA: 0x00378054 File Offset: 0x00378054
    private static void AddAgentComputerUseProperties(JsonObject properties, bool usedComputerUse, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> toolNames)
    {
      List<string> list = (toolNames ?? global::System.Array.Empty<string>()).Order<string>(StringComparer.Ordinal).ToList<string>();
      properties["used_computer_use"] = usedComputerUse;
      properties["computer_use_tool_names"] = ClickyAnalytics.Array(list);
      properties["computer_use_tool_count"] = list.Count;
    }

    // Token: 0x06000AF7 RID: 2807 RVA: 0x003780B4 File Offset: 0x003780B4
    private static void AddAgentIntegrationsUsedProperties(JsonObject properties, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> integrationsUsed)
    {
      List<string> list = (integrationsUsed ?? global::System.Array.Empty<string>()).Order<string>(StringComparer.Ordinal).ToList<string>();
      properties["used_integrations"] = list.Count > 0;
      properties["integrations_used"] = ClickyAnalytics.Array(list);
      properties["integrations_count"] = list.Count;
    }

    // Token: 0x06000AF8 RID: 2808 RVA: 0x0037811C File Offset: 0x0037811C
    public static void TrackAgentStarted(string transcript, string source, [Nullable(2)] JsonObject interactionProperties = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["character_count"] = transcript.Length;
      jsonObject["source"] = source;
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("agent_started", ClickyAnalytics.Merge(jsonObject2, interactionProperties), null, null);
    }

    // Token: 0x06000AF9 RID: 2809 RVA: 0x0037817C File Offset: 0x0037817C
    public static void TrackAgentFollowedUp(string transcript, string source, [Nullable(2)] string threadIdentifier, [Nullable(2)] JsonObject interactionProperties = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["character_count"] = transcript.Length;
      jsonObject["source"] = source;
      JsonObject jsonObject2 = jsonObject;
      if (threadIdentifier != null)
      {
        jsonObject2["thread_id"] = threadIdentifier;
      }
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("agent_followed_up", ClickyAnalytics.Merge(jsonObject2, interactionProperties), null, null);
    }

    // Token: 0x06000AFA RID: 2810 RVA: 0x003781F0 File Offset: 0x003781F0
    [NullableContext(2)]
    public static void TrackAgentUsedIntegration(string threadIdentifier, string transcript, [Nullable(1)] string integrationProviderKey)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["integration"] = integrationProviderKey;
      JsonObject jsonObject2 = jsonObject;
      if (threadIdentifier != null)
      {
        jsonObject2["thread_id"] = threadIdentifier;
      }
      ClickyAnalytics.AddAgentTranscriptProperties(jsonObject2, transcript);
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("agent_used_integration", jsonObject2, null, null);
    }

    // Token: 0x06000AFB RID: 2811 RVA: 0x0037824C File Offset: 0x0037824C
    [NullableContext(2)]
    public static void TrackAgentFinished([Nullable(1)] string finalMessage, string threadIdentifier, int? durationMilliseconds, int? tokenCount, string transcript = null, bool usedComputerUse = false, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> computerUseToolNames = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> integrationsUsed = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["character_count"] = finalMessage.Length;
      jsonObject["final_message_character_count"] = finalMessage.Length;
      JsonObject jsonObject2 = jsonObject;
      if (threadIdentifier != null)
      {
        jsonObject2["thread_id"] = threadIdentifier;
      }
      if (durationMilliseconds != null)
      {
        int valueOrDefault = durationMilliseconds.GetValueOrDefault();
        jsonObject2["duration_ms"] = valueOrDefault;
      }
      if (tokenCount != null)
      {
        int valueOrDefault2 = tokenCount.GetValueOrDefault();
        jsonObject2["token_count"] = valueOrDefault2;
      }
      ClickyAnalytics.AddAgentTranscriptProperties(jsonObject2, transcript);
      ClickyAnalytics.AddAgentComputerUseProperties(jsonObject2, usedComputerUse, computerUseToolNames);
      ClickyAnalytics.AddAgentIntegrationsUsedProperties(jsonObject2, integrationsUsed);
      ClickyAnalytics.Capture("agent_finished", jsonObject2, null, null);
      string text = "agent";
      string text2 = "agent_finished";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["thread_id"] = threadIdentifier ?? "";
      jsonObject3["duration_ms"] = durationMilliseconds.GetValueOrDefault(-1);
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
    }

    // Token: 0x06000AFC RID: 2812 RVA: 0x00378364 File Offset: 0x00378364
    [NullableContext(2)]
    public static void TrackAgentFailed([Nullable(1)] string errorSummary, [Nullable(1)] string phase, string threadIdentifier, string transcript, bool usedComputerUse = false, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> computerUseToolNames = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> integrationsUsed = null, bool isExpectedApprovalPause = false, string diagnostic = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["error_summary"] = errorSummary;
      jsonObject["phase"] = phase;
      JsonObject jsonObject2 = jsonObject;
      if (diagnostic != null)
      {
        jsonObject2["error_diagnostic"] = diagnostic;
      }
      if (threadIdentifier != null)
      {
        jsonObject2["thread_id"] = threadIdentifier;
      }
      ClickyAnalytics.AddAgentTranscriptProperties(jsonObject2, transcript);
      ClickyAnalytics.AddAgentComputerUseProperties(jsonObject2, usedComputerUse, computerUseToolNames);
      ClickyAnalytics.AddAgentIntegrationsUsedProperties(jsonObject2, integrationsUsed);
      ClickyAnalytics.Capture("agent_failed", jsonObject2, null, null);
      string text = "agent";
      string text2 = "agent_failed: " + errorSummary;
      CrashReportLevel crashReportLevel = CrashReportLevel.Error;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["phase"] = phase;
      jsonObject3["thread_id"] = threadIdentifier ?? "";
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
      bool flag = phase == "launch" || phase == "run";
      if (flag && !isExpectedApprovalPause)
      {
        ClickyAnalytics.ReportInternalError("codex_agent", "agent_failed", null, diagnostic ?? errorSummary, CrashReportLevel.Error, phase + ":" + AgentFailureReason.Key(diagnostic ?? errorSummary), null);
      }
    }

    // Token: 0x06000AFD RID: 2813 RVA: 0x00378498 File Offset: 0x00378498
    [NullableContext(2)]
    public static void TrackAgentInterrupted([Nullable(1)] string summary, string threadIdentifier, string transcript = null, bool usedComputerUse = false, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> computerUseToolNames = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> integrationsUsed = null, bool unexpected = false)
    {
      if (unexpected)
      {
        ClickyAnalytics.ReportInternalError("codex_agent", "agent_interrupted", null, summary, CrashReportLevel.Warning, AgentFailureReason.Key(summary), null);
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["summary_character_count"] = summary.Length;
      JsonObject jsonObject2 = jsonObject;
      if (threadIdentifier != null)
      {
        jsonObject2["thread_id"] = threadIdentifier;
      }
      ClickyAnalytics.AddAgentTranscriptProperties(jsonObject2, transcript);
      ClickyAnalytics.AddAgentComputerUseProperties(jsonObject2, usedComputerUse, computerUseToolNames);
      ClickyAnalytics.AddAgentIntegrationsUsedProperties(jsonObject2, integrationsUsed);
      ClickyAnalytics.Capture("agent_interrupted", jsonObject2, null, null);
    }

    // Token: 0x06000AFE RID: 2814 RVA: 0x00378520 File Offset: 0x00378520
    [NullableContext(2)]
    public static void TrackCodexThreadArchived(string threadIdentifier)
    {
      ClickyAnalytics.Capture("agent_thread_archived", ClickyAnalytics.ThreadOnly(threadIdentifier), null, null);
    }

    // Token: 0x06000AFF RID: 2815 RVA: 0x00378534 File Offset: 0x00378534
    [NullableContext(2)]
    public static void TrackAgentExtraEffortApprovalShown(string threadIdentifier)
    {
      ClickyAnalytics.Capture("agent_extra_effort_approval_shown", ClickyAnalytics.ThreadOnly(threadIdentifier), null, null);
    }

    // Token: 0x06000B00 RID: 2816 RVA: 0x00378548 File Offset: 0x00378548
    [NullableContext(2)]
    public static void TrackAgentExtraEffortApproved(string threadIdentifier)
    {
      ClickyAnalytics.Capture("agent_extra_effort_approved", ClickyAnalytics.ThreadOnly(threadIdentifier), null, null);
    }

    // Token: 0x06000B01 RID: 2817 RVA: 0x0037855C File Offset: 0x0037855C
    private static JsonObject ThreadOnly([Nullable(2)] string threadIdentifier)
    {
      JsonObject jsonObject = new JsonObject(null);
      if (threadIdentifier != null)
      {
        jsonObject["thread_id"] = threadIdentifier;
      }
      return jsonObject;
    }

    // Token: 0x06000B02 RID: 2818 RVA: 0x00378590 File Offset: 0x00378590
    [NullableContext(2)]
    public static void TrackCheckoutReturned(bool isCanceled, bool isPortal, string checkoutSessionIdentifier)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["is_canceled"] = isCanceled;
      jsonObject["is_portal"] = isPortal;
      JsonObject jsonObject2 = jsonObject;
      if (!string.IsNullOrEmpty(checkoutSessionIdentifier))
      {
        jsonObject2["checkout_session_id"] = checkoutSessionIdentifier;
        jsonObject2["$insert_id"] = "checkout_returned:" + checkoutSessionIdentifier;
      }
      ClickyAnalytics.Capture("checkout_returned", jsonObject2, null, null);
    }

    // Token: 0x06000B03 RID: 2819 RVA: 0x00378610 File Offset: 0x00378610
    public static void TrackCustomerPortalOpened(string source)
    {
      string text = "customer_portal_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
      ClickyAnalytics.FlushPendingEvents();
    }

    // Token: 0x06000B04 RID: 2820 RVA: 0x00378650 File Offset: 0x00378650
    public static void NoteReferralPageEntryPoint(string entryPoint)
    {
      object gate = ClickyAnalytics.Gate;
      lock (gate)
      {
        ClickyAnalytics.pendingReferralPageEntryPoint = entryPoint;
      }
    }

    // Token: 0x06000B05 RID: 2821 RVA: 0x00378690 File Offset: 0x00378690
    [NullableContext(2)]
    public static void TrackReferralPageOpened(string entryPoint)
    {
      object gate = ClickyAnalytics.Gate;
      string text;
      lock (gate)
      {
        text = entryPoint ?? ClickyAnalytics.pendingReferralPageEntryPoint ?? "unknown";
        ClickyAnalytics.pendingReferralPageEntryPoint = null;
      }
      string text2 = "referral_page_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["entry_point"] = text;
      ClickyAnalytics.Capture(text2, jsonObject, null, null);
    }

    // Token: 0x06000B06 RID: 2822 RVA: 0x00378710 File Offset: 0x00378710
    public static void TrackReferralShareCopied(string item)
    {
      string text = "referral_share_copied";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["item"] = item;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B07 RID: 2823 RVA: 0x00378748 File Offset: 0x00378748
    public static void TrackReferralPayoutRequested(string surface, bool isAboveMinimum)
    {
      string text = "referral_payout_requested";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      jsonObject["is_above_minimum"] = isAboveMinimum;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B08 RID: 2824 RVA: 0x00378794 File Offset: 0x00378794
    public static void TrackReferralUsernameChanged(string outcome)
    {
      string text = "referral_username_change_result";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["outcome"] = outcome;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B09 RID: 2825 RVA: 0x003787CC File Offset: 0x003787CC
    public static void TrackReferralFriendCodeClaimResult(string source, string outcome)
    {
      string text = "referral_friend_code_claim_result";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      jsonObject["outcome"] = outcome;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0A RID: 2826 RVA: 0x00378818 File Offset: 0x00378818
    public static void TrackReferralDeepLinkOpened(bool isSignedIn)
    {
      string text = "referral_deep_link_opened";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["is_signed_in"] = isSignedIn;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0B RID: 2827 RVA: 0x00378850 File Offset: 0x00378850
    public static void TrackUpdateFound(string fromVersion, string toVersion)
    {
      string text = "update_found";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["from_version"] = fromVersion;
      jsonObject["to_version"] = toVersion;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0C RID: 2828 RVA: 0x0037889C File Offset: 0x0037889C
    public static void TrackCheckForUpdatesClicked(string surface)
    {
      string text = "check_for_updates_clicked";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0D RID: 2829 RVA: 0x003788D4 File Offset: 0x003788D4
    public static void TrackUpdateInstallClicked(string fromVersion, string toVersion, string surface)
    {
      string text = "update_install_clicked";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["from_version"] = fromVersion;
      jsonObject["to_version"] = toVersion;
      jsonObject["surface"] = surface;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0E RID: 2830 RVA: 0x00378930 File Offset: 0x00378930
    public static void TrackUpdateInstallCompleted(string fromVersion, string toVersion)
    {
      string text = "update_install_completed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["from_version"] = fromVersion;
      jsonObject["to_version"] = toVersion;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B0F RID: 2831 RVA: 0x0037897C File Offset: 0x0037897C
    public static void TrackAgentHarnessTurn(string harness, string place, string status, double durationSeconds, IReadOnlyDictionary<string, double> stats)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["harness"] = harness;
      jsonObject["place"] = place;
      jsonObject["status"] = status;
      jsonObject["duration_seconds"] = Math.Round(durationSeconds * 10.0) / 10.0;
      JsonObject jsonObject2 = jsonObject;
      foreach (KeyValuePair<string, double> keyValuePair in stats)
      {
        string text;
        double num;
        keyValuePair.Deconstruct(out text, out num);
        string text2 = text;
        double num2 = num;
        jsonObject2["h1_" + text2] = num2;
      }
      ClickyAnalytics.Capture("agent_harness_turn", jsonObject2, null, null);
    }

    // Token: 0x06000B10 RID: 2832 RVA: 0x00378A64 File Offset: 0x00378A64
    public static string ProductClickyIdentifier(string slug)
    {
      return Convert.ToHexStringLower(SHA256.HashData(Encoding.UTF8.GetBytes(slug)));
    }

    // Token: 0x06000B11 RID: 2833 RVA: 0x00378A7C File Offset: 0x00378A7C
    [NullableContext(2)]
    public static void TrackProductEvent([Nullable(1)] string productEvent, string clickySlug = null, string threadId = null, JsonObject properties = null)
    {
      JsonObject jsonObject;
      if ((jsonObject = ((properties != null) ? properties.DeepClone().AsObject() : null)) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      jsonObject2["product_event_version"] = 1;
      jsonObject2["app_version"] = ClickyAnalytics.appVersion;
      jsonObject2["app_build"] = ClickyAnalytics.appBuild;
      jsonObject2["is_debug_build"] = false;
      if (clickySlug != null)
      {
        jsonObject2["clicky_id"] = ClickyAnalytics.ProductClickyIdentifier(clickySlug);
      }
      if (threadId != null)
      {
        jsonObject2["thread_id"] = threadId;
      }
      ClickyAnalytics.Capture(productEvent, jsonObject2, null, null);
    }

    // Token: 0x06000B12 RID: 2834 RVA: 0x00378B30 File Offset: 0x00378B30
    public static void TrackOnboardingSkippedByShake()
    {
      ClickyAnalytics.Capture("onboarding_skipped_by_shake", null, null, null);
    }

    // Token: 0x06000B13 RID: 2835 RVA: 0x00378B40 File Offset: 0x00378B40
    public static string PermissionAnalyticsName(PermissionKind kind)
    {
      if (kind != PermissionKind.Microphone)
      {
        return "screen_recording";
      }
      return "microphone";
    }

    // Token: 0x06000B14 RID: 2836 RVA: 0x00378B50 File Offset: 0x00378B50
    private static JsonObject PermissionProperties(PermissionKind kind, [Nullable(2)] string probeSource = null)
    {
      int num = global::System.Array.FindIndex<ValueTuple<PermissionKind, string>>(ClickyAnalytics.PermissionSteps, ([TupleElementNames(new string[] { "Kind", "Name" })] ValueTuple<PermissionKind, string> s) => s.Item1 == kind);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["permission"] = ClickyAnalytics.PermissionAnalyticsName(kind);
      jsonObject["permission_step_index"] = num + 1;
      jsonObject["permission_step_count"] = ClickyAnalytics.PermissionSteps.Length;
      jsonObject["permission_category"] = "system";
      jsonObject["is_system_permission"] = true;
      JsonObject jsonObject2 = jsonObject;
      if (probeSource != null)
      {
        jsonObject2["probe_source"] = probeSource;
      }
      return jsonObject2;
    }

    // Token: 0x06000B15 RID: 2837 RVA: 0x00378C10 File Offset: 0x00378C10
    private static JsonObject PermissionFlowProperties([Nullable(2)] string entryContext = null)
    {
      List<string> list = ClickyAnalytics.PermissionSteps.Select<ValueTuple<PermissionKind, string>, string>(([TupleElementNames(new string[] { "Kind", "Name" })] ValueTuple<PermissionKind, string> s) => s.Item2).ToList<string>();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["required_permission_count"] = ClickyAnalytics.PermissionSteps.Length;
      jsonObject["required_permissions"] = ClickyAnalytics.Array(list);
      jsonObject["system_permission_count"] = ClickyAnalytics.PermissionSteps.Length;
      jsonObject["system_permissions"] = ClickyAnalytics.Array(list);
      jsonObject["includes_screen_content"] = false;
      JsonObject jsonObject2 = jsonObject;
      if (entryContext != null)
      {
        jsonObject2["entry_context"] = entryContext;
      }
      return jsonObject2;
    }

    // Token: 0x06000B16 RID: 2838 RVA: 0x00378CD4 File Offset: 0x00378CD4
    public static void TrackPermissionFlowShown(string entryContext)
    {
      ClickyAnalytics.Capture("permission_flow_shown", ClickyAnalytics.PermissionFlowProperties(entryContext), null, null);
    }

    // Token: 0x06000B17 RID: 2839 RVA: 0x00378CE8 File Offset: 0x00378CE8
    public static void TrackPermissionFlowCompleted(string entryContext)
    {
      ClickyAnalytics.Capture("permission_flow_completed", ClickyAnalytics.PermissionFlowProperties(entryContext), null, null);
    }

    // Token: 0x06000B18 RID: 2840 RVA: 0x00378CFC File Offset: 0x00378CFC
    public static void TrackCorePermissionsGranted()
    {
      ClickyAnalytics.Capture("core_permissions_granted", ClickyAnalytics.PermissionFlowProperties(null), null, null);
    }

    // Token: 0x06000B19 RID: 2841 RVA: 0x00378D10 File Offset: 0x00378D10
    public static void TrackAllPermissionsGranted()
    {
      ClickyAnalytics.Capture("all_permissions_granted", ClickyAnalytics.PermissionFlowProperties(null), null, null);
    }

    // Token: 0x06000B1A RID: 2842 RVA: 0x00378D24 File Offset: 0x00378D24
    public static void TrackPermissionStepShown(PermissionKind kind)
    {
      ClickyAnalytics.Capture("permission_step_shown", ClickyAnalytics.PermissionProperties(kind, null), null, null);
    }

    // Token: 0x06000B1B RID: 2843 RVA: 0x00378D3C File Offset: 0x00378D3C
    public static void TrackPermissionSettingsOpened(PermissionKind kind)
    {
      ClickyAnalytics.Capture("permission_settings_opened", ClickyAnalytics.PermissionProperties(kind, null), null, null);
    }

    // Token: 0x06000B1C RID: 2844 RVA: 0x00378D54 File Offset: 0x00378D54
    public static void TrackPermissionNativePromptTriggered(PermissionKind kind)
    {
      ClickyAnalytics.Capture("permission_native_prompt_triggered", ClickyAnalytics.PermissionProperties(kind, null), null, null);
    }

    // Token: 0x06000B1D RID: 2845 RVA: 0x00378D6C File Offset: 0x00378D6C
    [NullableContext(2)]
    public static void TrackPermissionGranted(PermissionKind kind, string probeSource = null)
    {
      ClickyAnalytics.Capture("permission_granted", ClickyAnalytics.PermissionProperties(kind, probeSource), null, null);
    }

    // Token: 0x06000B1E RID: 2846 RVA: 0x00378D84 File Offset: 0x00378D84
    [NullableContext(2)]
    public static void TrackPermissionDenied(PermissionKind kind, string probeSource = null)
    {
      ClickyAnalytics.Capture("permission_denied", ClickyAnalytics.PermissionProperties(kind, probeSource), null, null);
    }

    // Token: 0x06000B1F RID: 2847 RVA: 0x00378D9C File Offset: 0x00378D9C
    public static void TrackPermissionRestartTapped(PermissionKind kind)
    {
      ClickyAnalytics.Capture("permission_restart_tapped", ClickyAnalytics.PermissionProperties(kind, null), null, null);
    }

    // Token: 0x06000B20 RID: 2848 RVA: 0x00378DB4 File Offset: 0x00378DB4
    public static void TrackPaywallShown(string surface, [Nullable(2)] string reason, int currentUsage, int limit, [Nullable(2)] string priceDisplay, string presentationSource, bool offersFreeOption, IEnumerable<string> offeredPlans, bool offersYearly, [Nullable(2)] string defaultInterval)
    {
      List<string> list = offeredPlans.ToList<string>();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      jsonObject["reason"] = reason ?? "unknown";
      jsonObject["current_usage"] = currentUsage;
      jsonObject["limit"] = limit;
      jsonObject["price_display"] = priceDisplay ?? "";
      jsonObject["presentation_source"] = presentationSource;
      jsonObject["offers_free_option"] = offersFreeOption;
      jsonObject["offered_plan_count"] = list.Count;
      jsonObject["offered_plans"] = ClickyAnalytics.Array(list);
      jsonObject["offers_yearly"] = offersYearly;
      jsonObject["default_interval"] = defaultInterval ?? "month";
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.Capture("paywall_opened", jsonObject2, null, null);
      ClickyAnalytics.Capture("paywall_shown", jsonObject2, null, null);
    }

    // Token: 0x06000B21 RID: 2849 RVA: 0x00378ED4 File Offset: 0x00378ED4
    public static void TrackPaywallIntervalToggled(string surface, string interval, string defaultInterval, string presentationSource = "limit_hit")
    {
      string text = "paywall_interval_toggled";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      jsonObject["interval"] = interval;
      jsonObject["default_interval"] = defaultInterval;
      jsonObject["presentation_source"] = presentationSource;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B22 RID: 2850 RVA: 0x00378F40 File Offset: 0x00378F40
    public static void TrackAnnualBillingSwitchClicked(string plan)
    {
      string text = "annual_billing_switch_clicked";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["plan"] = plan;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B23 RID: 2851 RVA: 0x00378F78 File Offset: 0x00378F78
    public static void TrackPaywallCTAClicked(string surface, [Nullable(2)] string plan = null, [Nullable(2)] string interval = null, string presentationSource = "limit_hit")
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["surface"] = surface;
      jsonObject["presentation_source"] = presentationSource;
      JsonObject jsonObject2 = jsonObject;
      if (plan != null)
      {
        jsonObject2["plan"] = plan;
      }
      if (interval != null)
      {
        jsonObject2["interval"] = interval;
      }
      ClickyAnalytics.Capture("paywall_cta_clicked", jsonObject2, null, null);
    }

    // Token: 0x06000B24 RID: 2852 RVA: 0x00378FEC File Offset: 0x00378FEC
    public static void TrackPaywallFreeSelected(string presentationSource)
    {
      string text = "paywall_free_selected";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["presentation_source"] = presentationSource;
      jsonObject["plan"] = "free";
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B25 RID: 2853 RVA: 0x0037903C File Offset: 0x0037903C
    public static void TrackPaywallDismissed(string presentationSource)
    {
      string text = "paywall_dismissed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["presentation_source"] = presentationSource;
      jsonObject["dismiss_method"] = "close_button";
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B26 RID: 2854 RVA: 0x0037908C File Offset: 0x0037908C
    [NullableContext(2)]
    public static void TrackCheckoutStarted([Nullable(1)] string source, string surface, string plan, string interval, string presentationSource)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      JsonObject jsonObject2 = jsonObject;
      if (surface != null)
      {
        jsonObject2["surface"] = surface;
      }
      if (plan != null)
      {
        jsonObject2["plan"] = plan;
      }
      if (interval != null)
      {
        jsonObject2["interval"] = interval;
      }
      if (presentationSource != null)
      {
        jsonObject2["presentation_source"] = presentationSource;
      }
      ClickyAnalytics.Capture("checkout_started", jsonObject2, null, null);
      ClickyAnalytics.FlushPendingEvents();
      try
      {
        JsonObject jsonObject3 = jsonObject2.DeepClone().AsObject();
        jsonObject3["started_at"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        Directory.CreateDirectory(Path.GetDirectoryName(ClickyAnalytics.CheckoutContextPath));
        File.WriteAllText(ClickyAnalytics.CheckoutContextPath, jsonObject3.ToJsonString(null));
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "checkout context not saved: " + ex.Message);
      }
    }

    // Token: 0x06000B27 RID: 2855 RVA: 0x00379198 File Offset: 0x00379198
    [NullableContext(2)]
    public static void TrackSubscriptionActivated(string checkoutSessionId, string tierDisplayName)
    {
      JsonObject jsonObject = new JsonObject(null);
      if (!string.IsNullOrEmpty(checkoutSessionId))
      {
        jsonObject["checkout_session_id"] = checkoutSessionId;
        jsonObject["$insert_id"] = "subscription_activated:" + checkoutSessionId;
      }
      if (tierDisplayName != null)
      {
        jsonObject["tier"] = tierDisplayName;
      }
      string text = null;
      try
      {
        if (File.Exists(ClickyAnalytics.CheckoutContextPath))
        {
          JsonObject jsonObject2 = JsonNode.Parse(File.ReadAllText(ClickyAnalytics.CheckoutContextPath), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject2 != null)
          {
            File.Delete(ClickyAnalytics.CheckoutContextPath);
            JsonValue jsonValue = jsonObject2["started_at"] as JsonValue;
            long num;
            DateTimeOffset? dateTimeOffset = ((jsonValue != null && jsonValue.TryGetValue<long>(out num)) ? new DateTimeOffset?(DateTimeOffset.FromUnixTimeSeconds(num)) : null);
            TimeSpan timeSpan;
            if (dateTimeOffset != null)
            {
              DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
              timeSpan = DateTimeOffset.UtcNow - valueOrDefault;
            }
            else
            {
              timeSpan = TimeSpan.MaxValue;
            }
            TimeSpan timeSpan2 = timeSpan;
            if (timeSpan2 >= TimeSpan.Zero && timeSpan2 <= ClickyAnalytics.CheckoutContextLifetime)
            {
              foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject2)
              {
                string text2;
                JsonNode jsonNode;
                keyValuePair.Deconstruct(out text2, out jsonNode);
                string text3 = text2;
                JsonNode jsonNode2 = jsonNode;
                if (text3 != "started_at")
                {
                  JsonValue jsonValue2 = jsonNode2 as JsonValue;
                  string text4;
                  if (jsonValue2 != null && jsonValue2.TryGetValue<string>(out text4))
                  {
                    jsonObject["checkout_" + text3] = text4;
                  }
                }
              }
              jsonObject["minutes_since_checkout_started"] = (int)timeSpan2.TotalMinutes;
              JsonNode jsonNode3 = jsonObject2["presentation_source"] ?? jsonObject2["source"];
              text = ((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("analytics", "checkout context: " + ex.Message);
      }
      string text5 = "subscription_activated";
      JsonObject jsonObject3 = jsonObject;
      JsonObject jsonObject4 = null;
      JsonObject jsonObject5;
      if (text != null)
      {
        (jsonObject5 = new JsonObject(null))["first_subscription_entry_point"] = text;
      }
      else
      {
        jsonObject5 = null;
      }
      ClickyAnalytics.Capture(text5, jsonObject3, jsonObject4, jsonObject5);
    }

    // Token: 0x06000B28 RID: 2856 RVA: 0x0037940C File Offset: 0x0037940C
    private static JsonObject IntegrationProperties(string integrationId, string integrationName, IReadOnlyList<string> toolkitSlugs, string source, [Nullable(2)] string status, int? connectedCount, int? totalCount, int? deletedAccountCount)
    {
      string text = toolkitSlugs.FirstOrDefault<string>() ?? integrationId;
      bool flag = integrationId.StartsWith("custom:", StringComparison.Ordinal);
      bool flag2 = integrationId.Equals("obsidian", StringComparison.OrdinalIgnoreCase);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["integration_id"] = integrationId;
      jsonObject["integration_name"] = integrationName;
      jsonObject["toolkit_slug"] = text;
      string text2 = "toolkit_slugs";
      IEnumerable<string> enumerable;
      if (toolkitSlugs.Count != 0)
      {
        enumerable = toolkitSlugs;
      }
      else
      {
        IReadOnlyList<string> readOnlyList = new <>z__ReadOnlySingleElementList<string>(text);
        enumerable = readOnlyList;
      }
      jsonObject[text2] = ClickyAnalytics.Array(enumerable);
      jsonObject["provider"] = (flag ? "custom_mcp" : (flag2 ? "local" : "composio"));
      jsonObject["source"] = source;
      jsonObject["is_google_integration"] = ClickyAnalytics.GoogleIntegrationIds.Contains(integrationId.ToLowerInvariant()) || ClickyAnalytics.GoogleIntegrationIds.Contains(text.ToLowerInvariant());
      jsonObject["is_local_integration"] = flag || flag2;
      JsonObject jsonObject2 = jsonObject;
      if (status != null)
      {
        jsonObject2["integration_status"] = status;
      }
      if (connectedCount != null)
      {
        int valueOrDefault = connectedCount.GetValueOrDefault();
        jsonObject2["connected_count"] = valueOrDefault;
      }
      if (totalCount != null)
      {
        int valueOrDefault2 = totalCount.GetValueOrDefault();
        jsonObject2["total_count"] = valueOrDefault2;
      }
      if (deletedAccountCount != null)
      {
        int valueOrDefault3 = deletedAccountCount.GetValueOrDefault();
        jsonObject2["deleted_account_count"] = valueOrDefault3;
      }
      return ClickyAnalytics.WithFrontmostApplication(jsonObject2);
    }

    // Token: 0x06000B29 RID: 2857 RVA: 0x003795B4 File Offset: 0x003795B4
    public static void TrackIntegrationInitiated(string integrationId, string integrationName, IReadOnlyList<string> toolkitSlugs, string source = "settings", [Nullable(2)] string status = null, int? connectedCount = null, int? totalCount = null)
    {
      ClickyAnalytics.Capture("integration_initiated", ClickyAnalytics.IntegrationProperties(integrationId, integrationName, toolkitSlugs, source, status, connectedCount, totalCount, null), null, null);
    }

    // Token: 0x06000B2A RID: 2858 RVA: 0x003795E8 File Offset: 0x003795E8
    public static void TrackIntegrationConnected(string integrationId, string integrationName, IReadOnlyList<string> toolkitSlugs, string source = "settings", [Nullable(2)] string status = null, int? connectedCount = null, int? totalCount = null)
    {
      ClickyAnalytics.Capture("integration_connected", ClickyAnalytics.IntegrationProperties(integrationId, integrationName, toolkitSlugs, source, status, connectedCount, totalCount, null), null, null);
    }

    // Token: 0x06000B2B RID: 2859 RVA: 0x0037961C File Offset: 0x0037961C
    public static void TrackIntegrationDisconnected(string integrationId, string integrationName, IReadOnlyList<string> toolkitSlugs, string source = "settings", [Nullable(2)] string status = null, int? connectedCount = null, int? totalCount = null, int? deletedAccountCount = null)
    {
      ClickyAnalytics.Capture("integration_disconnected", ClickyAnalytics.IntegrationProperties(integrationId, integrationName, toolkitSlugs, source, status, connectedCount, totalCount, deletedAccountCount), null, null);
    }

    // Token: 0x06000B2C RID: 2860 RVA: 0x00379648 File Offset: 0x00379648
    private static JsonObject IntegrationSuggestionProperties(string toolkitSlug, string displayName, IReadOnlyList<string> useCases, [Nullable(2)] string logoUrl, [Nullable(2)] string sourceApplicationName, [Nullable(2)] string sourceHost)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["toolkit_slug"] = toolkitSlug;
      jsonObject["integration_name"] = displayName;
      jsonObject["source"] = "integration_suggestion";
      jsonObject["use_case_count"] = useCases.Count;
      jsonObject["use_cases"] = ClickyAnalytics.Array(useCases);
      JsonObject jsonObject2 = jsonObject;
      if (!string.IsNullOrEmpty(logoUrl))
      {
        jsonObject2["logo_url"] = logoUrl;
      }
      if (!string.IsNullOrEmpty(sourceApplicationName))
      {
        jsonObject2["source_application_name"] = sourceApplicationName;
      }
      if (!string.IsNullOrEmpty(sourceHost))
      {
        jsonObject2["source_host"] = sourceHost;
      }
      return ClickyAnalytics.WithFrontmostApplication(jsonObject2);
    }

    // Token: 0x06000B2D RID: 2861 RVA: 0x00379718 File Offset: 0x00379718
    public static void TrackIntegrationSuggestion(string outcome, string toolkitSlug, string displayName, IReadOnlyList<string> useCases, [Nullable(2)] string logoUrl, [Nullable(2)] string sourceApplicationName, [Nullable(2)] string sourceHost)
    {
      ClickyAnalytics.Capture("integration_suggestion_" + outcome, ClickyAnalytics.IntegrationSuggestionProperties(toolkitSlug, displayName, useCases, logoUrl, sourceApplicationName, sourceHost), null, null);
    }

    // Token: 0x06000B2E RID: 2862 RVA: 0x0037973C File Offset: 0x0037973C
    [NullableContext(2)]
    public static void TrackAgentTabThreadHiddenFromHistory(string threadIdentifier)
    {
      ClickyAnalytics.Capture("agent_tab_thread_hidden_from_history", ClickyAnalytics.WithFrontmostApplication(ClickyAnalytics.ThreadOnly(threadIdentifier)), null, null);
    }

    // Token: 0x06000B2F RID: 2863 RVA: 0x00379758 File Offset: 0x00379758
    public static void TrackNotchFileDragOpenedComposer()
    {
      ClickyAnalytics.Capture("notch_file_drag_composer_opened", null, null, null);
    }

    // Token: 0x06000B30 RID: 2864 RVA: 0x00379768 File Offset: 0x00379768
    public static void TrackNotchFilesDropped(int fileCount, string composerOpenSource)
    {
      string text = "notch_files_dropped";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["file_count"] = fileCount;
      jsonObject["composer_open_source"] = composerOpenSource;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B31 RID: 2865 RVA: 0x003797B4 File Offset: 0x003797B4
    public static void TrackNotchFileDragAbandoned()
    {
      ClickyAnalytics.Capture("notch_file_drag_abandoned", null, null, null);
    }

    // Token: 0x06000B32 RID: 2866 RVA: 0x003797C4 File Offset: 0x003797C4
    public static void TrackNotchDroppedFilesAttached(int fileCount)
    {
      string text = "notch_dropped_files_attached";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["file_count"] = fileCount;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B33 RID: 2867 RVA: 0x003797FC File Offset: 0x003797FC
    public static void TrackNotchDroppedAttachmentsDiscarded(int fileCount)
    {
      string text = "notch_dropped_attachments_discarded";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["file_count"] = fileCount;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B34 RID: 2868 RVA: 0x00379834 File Offset: 0x00379834
    public static void TrackHomeSpaceEarlierAgentMovedIntoClicky(bool becameChat, bool foundedNewClicky = false)
    {
      string text = "agent_space_earlier_agent_moved_into_clicky";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["became_chat"] = becameChat;
      jsonObject["founded_new_clicky"] = foundedNewClicky;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B35 RID: 2869 RVA: 0x00379880 File Offset: 0x00379880
    public static void TrackHomeSpaceNewClickyInterviewStarted(string mode, string interviewId)
    {
      string text = "home_space_new_clicky_interview_started";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["mode"] = mode;
      jsonObject["interview_id"] = interviewId;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B36 RID: 2870 RVA: 0x003798CC File Offset: 0x003798CC
    public static void TrackHomeSpaceNewClickyInterviewEnded(bool founded, string mode, int answerCount, bool routedToExisting, string interviewId, double durationSeconds)
    {
      string text = "home_space_new_clicky_interview_ended";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["founded"] = founded;
      jsonObject["mode"] = mode;
      jsonObject["answer_count"] = answerCount;
      jsonObject["routed_to_existing"] = routedToExisting;
      jsonObject["interview_id"] = interviewId;
      jsonObject["duration_seconds"] = Math.Max(0.0, durationSeconds);
      jsonObject["outcome"] = ((mode == "onboarding" && founded) ? "planning" : (routedToExisting ? "routed_to_existing" : (founded ? "founded" : "left")));
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B37 RID: 2871 RVA: 0x003799A8 File Offset: 0x003799A8
    public static string ProductArtifactKind(string artifact)
    {
      if (artifact.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || artifact.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
      {
        return "web_link";
      }
      string text;
      try
      {
        text = Path.GetExtension(artifact).TrimStart('.').ToLowerInvariant();
      }
      catch
      {
        text = "";
      }
      if (text != null)
      {
        switch (text.Length)
        {
        case 2:
          if (!(text == "md"))
          {
            goto IL_03C4;
          }
          break;
        case 3:
          switch (text[0])
          {
          case 'c':
            if (!(text == "csv"))
            {
              goto IL_03C4;
            }
            goto IL_0394;
          case 'd':
            if (!(text == "doc"))
            {
              goto IL_03C4;
            }
            goto IL_03A4;
          case 'e':
          case 'f':
          case 'i':
          case 'l':
          case 'n':
          case 'o':
          case 'q':
          case 'u':
          case 'v':
            goto IL_03C4;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_03C4;
            }
            goto IL_038C;
          case 'h':
            if (!(text == "htm"))
            {
              goto IL_03C4;
            }
            goto IL_03AC;
          case 'j':
            if (!(text == "jpg"))
            {
              goto IL_03C4;
            }
            goto IL_038C;
          case 'k':
            if (!(text == "key"))
            {
              goto IL_03C4;
            }
            goto IL_039C;
          case 'm':
            if (text == "mp4" || text == "mov")
            {
              goto IL_03B4;
            }
            if (!(text == "mp3") && !(text == "m4a"))
            {
              goto IL_03C4;
            }
            break;
          case 'p':
            if (text == "pdf")
            {
              return "pdf";
            }
            if (text == "png")
            {
              goto IL_038C;
            }
            if (!(text == "ppt"))
            {
              goto IL_03C4;
            }
            goto IL_039C;
          case 'r':
            if (!(text == "rtf"))
            {
              goto IL_03C4;
            }
            goto IL_03A4;
          case 's':
            if (!(text == "svg"))
            {
              goto IL_03C4;
            }
            goto IL_038C;
          case 't':
            if (text == "tsv")
            {
              goto IL_0394;
            }
            if (!(text == "txt"))
            {
              goto IL_03C4;
            }
            goto IL_03A4;
          case 'w':
            if (!(text == "wav"))
            {
              goto IL_03C4;
            }
            break;
          case 'x':
            if (!(text == "xls"))
            {
              goto IL_03C4;
            }
            goto IL_0394;
          default:
            goto IL_03C4;
          }
          return "audio";
        case 4:
        {
          char c = text[2];
          if (c <= 'i')
          {
            switch (c)
            {
            case 'b':
              if (text == "webp")
              {
                goto IL_038C;
              }
              if (!(text == "webm"))
              {
                goto IL_03C4;
              }
              goto IL_03B4;
            case 'c':
              if (!(text == "docx"))
              {
                goto IL_03C4;
              }
              goto IL_03A4;
            case 'd':
              goto IL_03C4;
            case 'e':
              if (!(text == "jpeg"))
              {
                goto IL_03C4;
              }
              goto IL_038C;
            default:
              if (c != 'i')
              {
                goto IL_03C4;
              }
              if (!(text == "heic"))
              {
                goto IL_03C4;
              }
              goto IL_038C;
            }
          }
          else if (c != 'm')
          {
            if (c != 's')
            {
              if (c != 't')
              {
                goto IL_03C4;
              }
              if (!(text == "pptx"))
              {
                goto IL_03C4;
              }
              goto IL_039C;
            }
            else
            {
              if (!(text == "xlsx"))
              {
                goto IL_03C4;
              }
              goto IL_0394;
            }
          }
          else
          {
            if (!(text == "html"))
            {
              goto IL_03C4;
            }
            goto IL_03AC;
          }
          break;
        }
        case 5:
          if (!(text == "pages"))
          {
            goto IL_03C4;
          }
          goto IL_03A4;
        case 6:
          goto IL_03C4;
        case 7:
          if (!(text == "numbers"))
          {
            goto IL_03C4;
          }
          goto IL_0394;
        case 8:
          if (!(text == "markdown"))
          {
            goto IL_03C4;
          }
          break;
        default:
          goto IL_03C4;
        }
        return "markdown";
        IL_038C:
        return "image";
        IL_0394:
        return "spreadsheet";
        IL_039C:
        return "presentation";
        IL_03A4:
        return "document";
        IL_03AC:
        return "html";
        IL_03B4:
        return "video";
      }
      IL_03C4:
      return "other";
    }

    // Token: 0x06000B38 RID: 2872 RVA: 0x00379D90 File Offset: 0x00379D90
    public static void TrackArtifactAction(string action, string artifact, [Nullable(2)] string threadId = null, [Nullable(2)] JsonObject properties = null)
    {
      JsonObject jsonObject;
      if ((jsonObject = ((properties != null) ? properties.DeepClone().AsObject() : null)) == null)
      {
        jsonObject = new JsonObject(null);
      }
      JsonObject jsonObject2 = jsonObject;
      jsonObject2["action"] = action;
      jsonObject2["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(artifact);
      ClickyAnalytics.TrackProductEvent("agent_artifact_action", null, threadId, jsonObject2);
    }

    // Token: 0x06000B39 RID: 2873 RVA: 0x00379DF8 File Offset: 0x00379DF8
    public static JsonObject InteractionContext(string inputMethod, string inputSurface, string presentation = "unknown", bool homeWasOpen = false, bool homeWasVisible = false, bool notchWasExpanded = false)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["interaction_context_version"] = 1;
      jsonObject["input_method"] = inputMethod;
      jsonObject["input_surface"] = inputSurface;
      jsonObject["home_space_presentation"] = presentation;
      JsonObject jsonObject2 = jsonObject;
      if (presentation != "unknown")
      {
        jsonObject2["home_space_was_open"] = homeWasOpen;
        jsonObject2["home_space_was_visible"] = homeWasVisible;
        jsonObject2["notch_was_expanded"] = notchWasExpanded;
      }
      return jsonObject2;
    }

    // Token: 0x06000B3A RID: 2874 RVA: 0x00379E9C File Offset: 0x00379E9C
    public static void TrackSuggestionInteraction(string productEvent, ClickyAnalytics.SuggestionCardInfo card, [Nullable(2)] string action = null, bool hasFeedback = false, [Nullable(2)] JsonObject context = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["suggestion_id"] = card.SuggestionId;
      jsonObject["slot"] = card.Slot;
      jsonObject["creates_clicky"] = card.CreatesClicky;
      jsonObject["has_routine"] = card.HasRoutine;
      jsonObject["has_feedback"] = hasFeedback;
      JsonObject jsonObject2 = jsonObject;
      if (action != null)
      {
        jsonObject2["action"] = action;
      }
      ClickyAnalytics.Merge(jsonObject2, context);
      ClickyAnalytics.TrackProductEvent(productEvent, card.ClickySlug, null, jsonObject2);
    }

    // Token: 0x06000B3B RID: 2875 RVA: 0x00379F4C File Offset: 0x00379F4C
    public static JsonObject SuggestionInteractionProperties(ClickyAnalytics.SuggestionCardInfo card, JsonObject context)
    {
      JsonObject jsonObject = context.DeepClone().AsObject();
      jsonObject["suggestion_id"] = card.SuggestionId;
      jsonObject["has_routine"] = card.HasRoutine;
      jsonObject["creates_clicky"] = card.CreatesClicky;
      if (card.ClickySlug != null)
      {
        jsonObject["clicky_id"] = ClickyAnalytics.ProductClickyIdentifier(card.ClickySlug);
      }
      return jsonObject;
    }

    // Token: 0x06000B3C RID: 2876 RVA: 0x00379FCC File Offset: 0x00379FCC
    public static void TrackProactiveAgentApproved(string agentTitle, string agentDescription, double secondsSinceRecommended, bool dispatched, string slot, [Nullable(2)] JsonObject interactionProperties = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["agent_title"] = agentTitle;
      jsonObject["agent_description"] = agentDescription;
      jsonObject["seconds_since_recommended"] = (int)secondsSinceRecommended;
      jsonObject["dispatched"] = dispatched;
      jsonObject["slot"] = slot;
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.Capture("proactive_agent_approved", ClickyAnalytics.Merge(jsonObject2, interactionProperties), null, null);
    }

    // Token: 0x06000B3D RID: 2877 RVA: 0x0037A054 File Offset: 0x0037A054
    public static void TrackProactiveAgentDenied(string agentTitle, string agentDescription, double secondsSinceRecommended, string slot, [Nullable(2)] string denyReason = null, [Nullable(2)] JsonObject interactionProperties = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["agent_title"] = agentTitle;
      jsonObject["agent_description"] = agentDescription;
      jsonObject["seconds_since_recommended"] = (int)secondsSinceRecommended;
      jsonObject["slot"] = slot;
      jsonObject["provided_reason"] = denyReason != null;
      JsonObject jsonObject2 = jsonObject;
      if (denyReason != null)
      {
        jsonObject2["deny_reason"] = denyReason;
      }
      ClickyAnalytics.Capture("proactive_agent_denied", ClickyAnalytics.Merge(jsonObject2, interactionProperties), null, null);
    }

    // Token: 0x06000B3E RID: 2878 RVA: 0x0037A0F4 File Offset: 0x0037A0F4
    public static int RoundedSeconds(double seconds)
    {
      return (int)Math.Round(seconds, MidpointRounding.AwayFromZero);
    }

    // Token: 0x06000B3F RID: 2879 RVA: 0x0037A100 File Offset: 0x0037A100
    public static JsonObject JobReportProperties(int jobCount, int attempts, int cardsShown, int blockCount, int cardErrors, int lineCount, bool raisedHeadsUp, bool playedChime, long durationMs)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["job_count"] = jobCount;
      jsonObject["attempts"] = attempts;
      jsonObject["has_card"] = cardsShown > 0;
      jsonObject["cards_shown"] = cardsShown;
      jsonObject["jobs_without_card"] = jobCount - cardsShown;
      jsonObject["block_count"] = blockCount;
      jsonObject["card_errors"] = cardErrors;
      jsonObject["line_count"] = lineCount;
      jsonObject["raised_heads_up"] = raisedHeadsUp;
      jsonObject["played_chime"] = playedChime;
      jsonObject["duration_ms"] = (int)durationMs;
      return jsonObject;
    }

    // Token: 0x06000B40 RID: 2880 RVA: 0x0037A1E4 File Offset: 0x0037A1E4
    public static void TrackCursorDetached()
    {
      ClickyAnalytics.Capture("cursor_detached", null, null, null);
    }

    // Token: 0x06000B41 RID: 2881 RVA: 0x0037A1F4 File Offset: 0x0037A1F4
    public static void TrackCursorDetachedClickStart()
    {
      ClickyAnalytics.Capture("cursor_detached_click_start", null, null, null);
    }

    // Token: 0x06000B42 RID: 2882 RVA: 0x0037A204 File Offset: 0x0037A204
    public static void TrackCursorDetachedClickStop()
    {
      ClickyAnalytics.Capture("cursor_detached_click_stop", null, null, null);
    }

    // Token: 0x06000B43 RID: 2883 RVA: 0x0037A214 File Offset: 0x0037A214
    public static void TrackCursorDetachedAutoReattach()
    {
      ClickyAnalytics.Capture("cursor_detached_auto_reattach", null, null, null);
    }

    // Token: 0x06000B44 RID: 2884 RVA: 0x0037A224 File Offset: 0x0037A224
    [NullableContext(2)]
    public static void TrackDictationPasteVerification([Nullable(1)] string result, int waitMilliseconds, string targetAppName, string targetAppBundleId)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["result"] = result;
      jsonObject["wait_ms"] = waitMilliseconds;
      JsonObject jsonObject2 = jsonObject;
      if (targetAppName != null)
      {
        jsonObject2["target_app_name"] = targetAppName;
      }
      if (targetAppBundleId != null)
      {
        jsonObject2["target_app_bundle_id"] = targetAppBundleId;
      }
      ClickyAnalytics.Capture("dictation_paste_verification", jsonObject2, null, null);
    }

    // Token: 0x06000B45 RID: 2885 RVA: 0x0037A298 File Offset: 0x0037A298
    [NullableContext(2)]
    public static void TrackMeetingCountdownPresented(string actionKind, int secondsUntilStart)
    {
      string text = "meeting_countdown_presented";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action_kind"] = actionKind ?? "none";
      jsonObject["seconds_until_start"] = secondsUntilStart;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B46 RID: 2886 RVA: 0x0037A2EC File Offset: 0x0037A2EC
    [NullableContext(2)]
    public static void TrackMeetingCountdownActionClicked(string actionKind, int secondsUntilStart)
    {
      string text = "meeting_countdown_action_clicked";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action_kind"] = actionKind ?? "none";
      jsonObject["seconds_until_start"] = secondsUntilStart;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B47 RID: 2887 RVA: 0x0037A340 File Offset: 0x0037A340
    public static void TrackMeetingCountdownDismissed(string reason, [Nullable(2)] string actionKind, int secondsUntilStart)
    {
      string text = "meeting_countdown_dismissed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["reason"] = reason;
      jsonObject["action_kind"] = actionKind ?? "none";
      jsonObject["seconds_until_start"] = secondsUntilStart;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B48 RID: 2888 RVA: 0x0037A3A4 File Offset: 0x0037A3A4
    public static void TrackProactiveAgentsRecommended(IReadOnlyList<string> agentTitles, string slot)
    {
      string text = "proactive_agent_recommended";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["agent_count"] = agentTitles.Count;
      jsonObject["agent_titles"] = ClickyAnalytics.TitlesArray(agentTitles);
      jsonObject["slot"] = slot;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B49 RID: 2889 RVA: 0x0037A404 File Offset: 0x0037A404
    public static void TrackProactiveAgentsUnactioned(IReadOnlyList<string> agentTitles, string reason, string slot)
    {
      string text = "proactive_agent_unactioned";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["agent_count"] = agentTitles.Count;
      jsonObject["agent_titles"] = ClickyAnalytics.TitlesArray(agentTitles);
      jsonObject["reason"] = reason;
      jsonObject["slot"] = slot;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B4A RID: 2890 RVA: 0x0037A474 File Offset: 0x0037A474
    private static JsonArray TitlesArray(IReadOnlyList<string> titles)
    {
      JsonArray jsonArray = new JsonArray(null);
      foreach (string text in titles)
      {
        jsonArray.Add<string>(text);
      }
      return jsonArray;
    }

    // Token: 0x06000B4B RID: 2891 RVA: 0x0037A4CC File Offset: 0x0037A4CC
    public static void TrackVoiceSettings(string voiceId, [Nullable(2)] string languageCode, double speechSpeed)
    {
      string text = "$set";
      JsonObject jsonObject = null;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["voice_id"] = voiceId;
      jsonObject2["voice_provider"] = (VoiceCatalog.IsGemini(voiceId) ? "gemini" : "openai");
      jsonObject2["voice_language"] = languageCode ?? "automatic";
      jsonObject2["speech_speed"] = speechSpeed;
      ClickyAnalytics.Capture(text, jsonObject, jsonObject2, null);
    }

    // Token: 0x06000B4C RID: 2892 RVA: 0x0037A554 File Offset: 0x0037A554
    public static void TrackScreenDrawingCompleted(string model, string outcome, int linesSpoken, int markCount, bool targetArmed, int followUpRound, int displayCount, bool drewOnOtherDisplay, bool continuedDrawing, bool startedFromTypedTurn, int textDeliveries, int? firstLineMilliseconds, int totalMilliseconds)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["model"] = model;
      jsonObject["outcome"] = outcome;
      jsonObject["lines_spoken"] = linesSpoken;
      jsonObject["mark_count"] = markCount;
      jsonObject["target_armed"] = targetArmed;
      jsonObject["follow_up_round"] = followUpRound;
      jsonObject["display_count"] = displayCount;
      jsonObject["drew_on_other_display"] = drewOnOtherDisplay;
      jsonObject["continued_drawing"] = continuedDrawing;
      jsonObject["started_from_typed_turn"] = startedFromTypedTurn;
      jsonObject["text_deliveries"] = textDeliveries;
      jsonObject["total_ms"] = totalMilliseconds;
      JsonObject jsonObject2 = jsonObject;
      if (firstLineMilliseconds != null)
      {
        int valueOrDefault = firstLineMilliseconds.GetValueOrDefault();
        jsonObject2["first_line_ms"] = valueOrDefault;
      }
      ClickyAnalytics.Capture("screen_drawing_completed", jsonObject2, null, null);
    }

    // Token: 0x06000B4D RID: 2893 RVA: 0x0037A674 File Offset: 0x0037A674
    public static void TrackAnnotationMarkShown(string source, [Nullable(2)] string label, bool isHover, int radius, int screen = 1)
    {
      string text = "annotation_mark_shown";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      jsonObject["kind"] = (isHover ? "hover" : "target");
      jsonObject["label"] = label ?? "";
      jsonObject["screen"] = screen;
      jsonObject["is_hover"] = isHover;
      jsonObject["radius"] = radius;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B4E RID: 2894 RVA: 0x0037A71C File Offset: 0x0037A71C
    [NullableContext(2)]
    public static void TrackAnnotationTargetArmed(string label, bool isHover, int followUpRound)
    {
      string text = "annotation_target_armed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["label"] = label ?? "target";
      jsonObject["is_hover"] = isHover;
      jsonObject["follow_up_round"] = followUpRound;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B4F RID: 2895 RVA: 0x0037A780 File Offset: 0x0037A780
    [NullableContext(2)]
    public static void TrackAnnotationTargetCompleted(string label, bool isHover, bool completedByHoverRest, int followUpRound)
    {
      string text = "annotation_target_completed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["label"] = label ?? "target";
      jsonObject["is_hover"] = isHover;
      jsonObject["completed_by_hover_rest"] = completedByHoverRest;
      jsonObject["follow_up_round"] = followUpRound;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B50 RID: 2896 RVA: 0x0037A7F4 File Offset: 0x0037A7F4
    [NullableContext(2)]
    public static void TrackAnnotationTargetMissed(string label, int missCount, bool sameDisplay)
    {
      string text = "annotation_target_missed";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["label"] = label ?? "target";
      jsonObject["miss_count"] = missCount;
      jsonObject["same_display"] = sameDisplay;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B51 RID: 2897 RVA: 0x0037A858 File Offset: 0x0037A858
    public static void TrackAnnotationFollowUpRequested(bool clickedOnTarget, int followUpRound)
    {
      string text = "annotation_follow_up_requested";
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["clicked_on_target"] = clickedOnTarget;
      jsonObject["follow_up_round"] = followUpRound;
      ClickyAnalytics.Capture(text, jsonObject, null, null);
    }

    // Token: 0x06000B52 RID: 2898 RVA: 0x0037A8A4 File Offset: 0x0037A8A4
    public static void TrackTextInjectionCompleted(string outcome, int characterCount, int wordCount, string source, [Nullable(2)] string targetAppName = null, [Nullable(2)] string failureReason = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["outcome"] = outcome;
      jsonObject["character_count"] = characterCount;
      jsonObject["word_count"] = wordCount;
      jsonObject["source"] = source;
      JsonObject jsonObject2 = jsonObject;
      if (!string.IsNullOrEmpty(targetAppName))
      {
        jsonObject2["target_app_name"] = targetAppName;
      }
      if (!string.IsNullOrEmpty(failureReason))
      {
        jsonObject2["failure_reason"] = failureReason;
      }
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("text_injection_completed", jsonObject2, null, null);
      string text = "text_injection";
      string text2 = "text_injection_completed";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["outcome"] = outcome;
      jsonObject3["character_count"] = characterCount;
      jsonObject3["word_count"] = wordCount;
      jsonObject3["source"] = source;
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
    }

    // Token: 0x06000B53 RID: 2899 RVA: 0x0037A9B0 File Offset: 0x0037A9B0
    public static string TextInjectionOutcomeName(string status, [Nullable(2)] string reason)
    {
      string text;
      if (!(status == "inserted"))
      {
        if (!(status == "copied_to_clipboard"))
        {
          if (!(status == "unconfirmed"))
          {
            if (!(status == "refused"))
            {
              text = "failed";
            }
            else
            {
              string text2;
              if (!(reason == "secure_field"))
              {
                if (!(reason == "address_bar"))
                {
                  if (!(reason == "no_focused_text_field"))
                  {
                    if (reason != null)
                    {
                      if (reason.Length > 0)
                      {
                        text2 = "refused_" + reason;
                        goto IL_00B1;
                      }
                    }
                    text2 = "refused_no_text_input";
                  }
                  else
                  {
                    text2 = "refused_no_text_input";
                  }
                }
                else
                {
                  text2 = "refused_address_bar";
                }
              }
              else
              {
                text2 = "refused_secure_field";
              }
              IL_00B1:
              text = text2;
            }
          }
          else
          {
            text = "typed_unconfirmed";
          }
        }
        else
        {
          text = "copied_to_clipboard";
        }
      }
      else
      {
        text = "typed";
      }
      return text;
    }

    // Token: 0x06000B54 RID: 2900 RVA: 0x0037AA7C File Offset: 0x0037AA7C
    public static void TrackClipboardCopyCompleted(string source, int characterCount, int wordCount, bool succeeded)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      jsonObject["character_count"] = characterCount;
      jsonObject["word_count"] = wordCount;
      jsonObject["succeeded"] = succeeded;
      JsonObject jsonObject2 = jsonObject;
      ClickyAnalytics.WithFrontmostApplication(jsonObject2);
      ClickyAnalytics.Capture("clipboard_copy_completed", jsonObject2, null, null);
      string text = "clipboard_copy";
      string text2 = "clipboard_copy_completed";
      CrashReportLevel crashReportLevel = CrashReportLevel.Info;
      JsonObject jsonObject3 = new JsonObject(null);
      jsonObject3["source"] = source;
      jsonObject3["character_count"] = characterCount;
      jsonObject3["word_count"] = wordCount;
      jsonObject3["succeeded"] = succeeded;
      ClickyAnalytics.Breadcrumb(text, text2, crashReportLevel, jsonObject3);
    }

    // Token: 0x06000B55 RID: 2901 RVA: 0x0037AB54 File Offset: 0x0037AB54
    public static int WordCount(string text)
    {
      return text.Split(null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // Token: 0x040004C6 RID: 1222
    public const string JobActionEvent = "job_action";

    // Token: 0x040004C7 RID: 1223
    private static readonly object Gate = new object();

    // Token: 0x040004C8 RID: 1224
    private static readonly JsonObject SuperProperties = new JsonObject(null);

    // Token: 0x040004C9 RID: 1225
    private static IAnalyticsSink sink = NullAnalyticsSink.Instance;

    // Token: 0x040004CA RID: 1226
    private static string anonymousId = Guid.NewGuid().ToString();

    // Token: 0x040004CB RID: 1227
    [Nullable(2)]
    private static string identifiedId;

    // Token: 0x040004CC RID: 1228
    private static string identityFilePath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "analytics-identity.json");

    // Token: 0x040004CD RID: 1229
    private static string appVersion = "unknown";

    // Token: 0x040004CE RID: 1230
    private static string appBuild = "0";

    // Token: 0x040004D0 RID: 1232
    [Nullable(2)]
    private static ClientIdentity clientIdentity;

    // Token: 0x040004D3 RID: 1235
    private static readonly object RecentAppErrorsGate = new object();

    // Token: 0x040004D4 RID: 1236
    private static readonly List<ClickyAnalytics.RecentAppError> RecentAppErrorsStorage = new List<ClickyAnalytics.RecentAppError>();

    // Token: 0x040004D5 RID: 1237
    private const int MaxRecentAppErrorCount = 25;

    // Token: 0x040004D6 RID: 1238
    private static readonly TimeSpan ThrottleWindow = TimeSpan.FromSeconds(30L);

    // Token: 0x040004D7 RID: 1239
    private static readonly Dictionary<string, DateTimeOffset> LastCapturedAt = new Dictionary<string, DateTimeOffset>();

    // Token: 0x040004D8 RID: 1240
    [Nullable(2)]
    private static string lastSyncedDiscoveryChannel;

    // Token: 0x040004D9 RID: 1241
    [TupleElementNames(new string[] { "Plan", "PlanName", "IsPaid" })]
    [Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<string, string, bool>? lastSyncedPlan;

    // Token: 0x040004DA RID: 1242
    [Nullable(2)]
    private static string pendingReferralPageEntryPoint;

    // Token: 0x040004DB RID: 1243
    [TupleElementNames(new string[] { "Kind", "Name" })]
    [Nullable(new byte[] { 1, 0, 1 })]
    private static readonly ValueTuple<PermissionKind, string>[] PermissionSteps = new ValueTuple<PermissionKind, string>[]
    {
      new ValueTuple<PermissionKind, string>(PermissionKind.Microphone, "microphone"),
      new ValueTuple<PermissionKind, string>(PermissionKind.ScreenCapture, "screen_recording")
    };

    // Token: 0x040004DC RID: 1244
    internal static string CheckoutContextPath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "analytics-checkout-context.json");

    // Token: 0x040004DD RID: 1245
    private static readonly TimeSpan CheckoutContextLifetime = TimeSpan.FromHours(48);

    // Token: 0x040004DE RID: 1246
    private static readonly HashSet<string> GoogleIntegrationIds = new HashSet<string> { "gmail", "googlesheets", "googlecalendar", "googledrive", "googledocs" };

    // Token: 0x020003B7 RID: 951
    [Nullable(0)]
    public static class AppErrorKind
    {
      // Token: 0x04001944 RID: 6468
      public const string CreditsExhausted = "credits_exhausted";

      // Token: 0x04001945 RID: 6469
      public const string Generic = "generic";
    }

    // Token: 0x020003B8 RID: 952
    [Nullable(0)]
    public sealed class RecentAppError : IEquatable<ClickyAnalytics.RecentAppError>
    {
      // Token: 0x060029E9 RID: 10729 RVA: 0x00424FB0 File Offset: 0x00424FB0
      public RecentAppError(string TimestampIso8601, string Stage, string Kind, string ErrorMessage, [Nullable(2)] string ErrorDomain, int ErrorCode, string UserFacingMessage)
      {
        this.TimestampIso8601 = TimestampIso8601;
        this.Stage = Stage;
        this.Kind = Kind;
        this.ErrorMessage = ErrorMessage;
        this.ErrorDomain = ErrorDomain;
        this.ErrorCode = ErrorCode;
        this.UserFacingMessage = UserFacingMessage;
        base..ctor();
      }

      // Token: 0x1700080D RID: 2061
      // (get) Token: 0x060029EA RID: 10730 RVA: 0x00424FF0 File Offset: 0x00424FF0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnalytics.RecentAppError);
        }
      }

      // Token: 0x1700080E RID: 2062
      // (get) Token: 0x060029EB RID: 10731 RVA: 0x00424FFC File Offset: 0x00424FFC
      // (set) Token: 0x060029EC RID: 10732 RVA: 0x00425004 File Offset: 0x00425004
      public string TimestampIso8601 { get; set; }

      // Token: 0x1700080F RID: 2063
      // (get) Token: 0x060029ED RID: 10733 RVA: 0x00425010 File Offset: 0x00425010
      // (set) Token: 0x060029EE RID: 10734 RVA: 0x00425018 File Offset: 0x00425018
      public string Stage { get; set; }

      // Token: 0x17000810 RID: 2064
      // (get) Token: 0x060029EF RID: 10735 RVA: 0x00425024 File Offset: 0x00425024
      // (set) Token: 0x060029F0 RID: 10736 RVA: 0x0042502C File Offset: 0x0042502C
      public string Kind { get; set; }

      // Token: 0x17000811 RID: 2065
      // (get) Token: 0x060029F1 RID: 10737 RVA: 0x00425038 File Offset: 0x00425038
      // (set) Token: 0x060029F2 RID: 10738 RVA: 0x00425040 File Offset: 0x00425040
      public string ErrorMessage { get; set; }

      // Token: 0x17000812 RID: 2066
      // (get) Token: 0x060029F3 RID: 10739 RVA: 0x0042504C File Offset: 0x0042504C
      // (set) Token: 0x060029F4 RID: 10740 RVA: 0x00425054 File Offset: 0x00425054
      [Nullable(2)]
      public string ErrorDomain
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000813 RID: 2067
      // (get) Token: 0x060029F5 RID: 10741 RVA: 0x00425060 File Offset: 0x00425060
      // (set) Token: 0x060029F6 RID: 10742 RVA: 0x00425068 File Offset: 0x00425068
      public int ErrorCode { get; set; }

      // Token: 0x17000814 RID: 2068
      // (get) Token: 0x060029F7 RID: 10743 RVA: 0x00425074 File Offset: 0x00425074
      // (set) Token: 0x060029F8 RID: 10744 RVA: 0x0042507C File Offset: 0x0042507C
      public string UserFacingMessage { get; set; }

      // Token: 0x060029F9 RID: 10745 RVA: 0x00425088 File Offset: 0x00425088
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("RecentAppError");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060029FA RID: 10746 RVA: 0x004250D4 File Offset: 0x004250D4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("TimestampIso8601 = ");
        builder.Append(this.TimestampIso8601);
        builder.Append(", Stage = ");
        builder.Append(this.Stage);
        builder.Append(", Kind = ");
        builder.Append(this.Kind);
        builder.Append(", ErrorMessage = ");
        builder.Append(this.ErrorMessage);
        builder.Append(", ErrorDomain = ");
        builder.Append(this.ErrorDomain);
        builder.Append(", ErrorCode = ");
        builder.Append(this.ErrorCode.ToString());
        builder.Append(", UserFacingMessage = ");
        builder.Append(this.UserFacingMessage);
        return true;
      }

      // Token: 0x060029FB RID: 10747 RVA: 0x004251A4 File Offset: 0x004251A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyAnalytics.RecentAppError left, ClickyAnalytics.RecentAppError right)
      {
        return !(left == right);
      }

      // Token: 0x060029FC RID: 10748 RVA: 0x004251B0 File Offset: 0x004251B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyAnalytics.RecentAppError left, ClickyAnalytics.RecentAppError right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060029FD RID: 10749 RVA: 0x004251C4 File Offset: 0x004251C4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TimestampIso8601>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Stage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorMessage>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ErrorDomain>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ErrorCode>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserFacingMessage>k__BackingField);
      }

      // Token: 0x060029FE RID: 10750 RVA: 0x00425284 File Offset: 0x00425284
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnalytics.RecentAppError);
      }

      // Token: 0x060029FF RID: 10751 RVA: 0x00425294 File Offset: 0x00425294
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyAnalytics.RecentAppError other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TimestampIso8601>k__BackingField, other.<TimestampIso8601>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Stage>k__BackingField, other.<Stage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorMessage>k__BackingField, other.<ErrorMessage>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ErrorDomain>k__BackingField, other.<ErrorDomain>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ErrorCode>k__BackingField, other.<ErrorCode>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserFacingMessage>k__BackingField, other.<UserFacingMessage>k__BackingField));
      }

      // Token: 0x06002A01 RID: 10753 RVA: 0x0042537C File Offset: 0x0042537C
      [CompilerGenerated]
      private RecentAppError(ClickyAnalytics.RecentAppError original)
      {
        this.TimestampIso8601 = original.<TimestampIso8601>k__BackingField;
        this.Stage = original.<Stage>k__BackingField;
        this.Kind = original.<Kind>k__BackingField;
        this.ErrorMessage = original.<ErrorMessage>k__BackingField;
        this.ErrorDomain = original.<ErrorDomain>k__BackingField;
        this.ErrorCode = original.<ErrorCode>k__BackingField;
        this.UserFacingMessage = original.<UserFacingMessage>k__BackingField;
      }

      // Token: 0x06002A02 RID: 10754 RVA: 0x004253E4 File Offset: 0x004253E4
      [CompilerGenerated]
      public void Deconstruct(out string TimestampIso8601, out string Stage, out string Kind, out string ErrorMessage, [Nullable(2)] out string ErrorDomain, out int ErrorCode, out string UserFacingMessage)
      {
        TimestampIso8601 = this.TimestampIso8601;
        Stage = this.Stage;
        Kind = this.Kind;
        ErrorMessage = this.ErrorMessage;
        ErrorDomain = this.ErrorDomain;
        ErrorCode = this.ErrorCode;
        UserFacingMessage = this.UserFacingMessage;
      }
    }

    // Token: 0x020003B9 RID: 953
    [Nullable(0)]
    public static class ProductEvent
    {
      // Token: 0x0400194D RID: 6477
      public const string DestinationViewed = "home_space_destination_viewed";

      // Token: 0x0400194E RID: 6478
      public const string PresentationChanged = "home_space_presentation_changed";

      // Token: 0x0400194F RID: 6479
      public const string ComposerModeChanged = "home_space_composer_mode_changed";

      // Token: 0x04001950 RID: 6480
      public const string AttachmentsAdded = "home_space_attachments_added";

      // Token: 0x04001951 RID: 6481
      public const string AttachmentRemoved = "home_space_attachment_removed";

      // Token: 0x04001952 RID: 6482
      public const string ClickyCreated = "clicky_created";

      // Token: 0x04001953 RID: 6483
      public const string ClickyArchived = "clicky_archived";

      // Token: 0x04001954 RID: 6484
      public const string ClickyRestored = "clicky_restored";

      // Token: 0x04001955 RID: 6485
      public const string ClickyPinned = "clicky_pin_changed";

      // Token: 0x04001956 RID: 6486
      public const string ProfileViewed = "clicky_profile_viewed";

      // Token: 0x04001957 RID: 6487
      public const string CharacterEditorOpened = "clicky_character_editor_opened";

      // Token: 0x04001958 RID: 6488
      public const string CharacterEditorClosed = "clicky_character_editor_closed";

      // Token: 0x04001959 RID: 6489
      public const string CharacterSaved = "clicky_character_saved";

      // Token: 0x0400195A RID: 6490
      public const string CharacterRandomized = "clicky_character_randomized";

      // Token: 0x0400195B RID: 6491
      public const string CharacterPackSelected = "clicky_character_pack_selected";

      // Token: 0x0400195C RID: 6492
      public const string FirstAskSelected = "clicky_first_ask_selected";

      // Token: 0x0400195D RID: 6493
      public const string ArtifactAction = "agent_artifact_action";

      // Token: 0x0400195E RID: 6494
      public const string HudAction = "agent_hud_action";

      // Token: 0x0400195F RID: 6495
      public const string ConversationAction = "home_space_conversation_action";

      // Token: 0x04001960 RID: 6496
      public const string TaskControl = "agent_task_control_clicked";

      // Token: 0x04001961 RID: 6497
      public const string RoutineCreated = "clicky_routine_created";

      // Token: 0x04001962 RID: 6498
      public const string RoutineUpdated = "clicky_routine_updated";

      // Token: 0x04001963 RID: 6499
      public const string RoutinePauseChanged = "clicky_routine_pause_changed";

      // Token: 0x04001964 RID: 6500
      public const string RoutineDeleted = "clicky_routine_deleted";

      // Token: 0x04001965 RID: 6501
      public const string RoutineRunRequested = "clicky_routine_run_requested";

      // Token: 0x04001966 RID: 6502
      public const string RoutineRunDispatched = "clicky_routine_run_dispatched";

      // Token: 0x04001967 RID: 6503
      public const string RoutineRunEnded = "clicky_routine_run_ended";

      // Token: 0x04001968 RID: 6504
      public const string InterviewAnswerSubmitted = "clicky_interview_answer_submitted";

      // Token: 0x04001969 RID: 6505
      public const string InterviewSkippedByVoice = "onboarding_interview_skipped_by_voice";

      // Token: 0x0400196A RID: 6506
      public const string StarterPlanStarted = "onboarding_starter_plan_started";

      // Token: 0x0400196B RID: 6507
      public const string StarterPlanFinished = "onboarding_starter_plan_finished";

      // Token: 0x0400196C RID: 6508
      public const string OnboardingRevealStarted = "onboarding_reveal_started";

      // Token: 0x0400196D RID: 6509
      public const string OnboardingRevealEnded = "onboarding_reveal_ended";

      // Token: 0x0400196E RID: 6510
      public const string SuggestionViewed = "home_space_suggestion_viewed";

      // Token: 0x0400196F RID: 6511
      public const string SuggestionAction = "home_space_suggestion_action";

      // Token: 0x04001970 RID: 6512
      public const string MorningInvitationAction = "morning_suggestions_invitation_action";

      // Token: 0x04001971 RID: 6513
      public const string MorningInvitationHeldBack = "morning_suggestions_invitation_held_back";

      // Token: 0x04001972 RID: 6514
      public const string SettingChanged = "setting_changed";

      // Token: 0x04001973 RID: 6515
      public const string ShortcutHelpViewed = "shortcut_help_viewed";

      // Token: 0x04001974 RID: 6516
      public const string ConnectorAction = "custom_connector_action";

      // Token: 0x04001975 RID: 6517
      public const string NotchPeekAction = "notch_peek_action";

      // Token: 0x04001976 RID: 6518
      public const string DashboardAction = "clicky_dashboard_action";

      // Token: 0x04001977 RID: 6519
      public const string JobAction = "job_action";

      // Token: 0x04001978 RID: 6520
      public const string ArcDockAction = "arc_dock_action";

      // Token: 0x04001979 RID: 6521
      public const string NotchUpdateCardAction = "notch_update_card_action";

      // Token: 0x0400197A RID: 6522
      public const string ClickyDriveAction = "clicky_drive_action";

      // Token: 0x0400197B RID: 6523
      public const string BrowserConnectionAction = "browser_connection_action";

      // Token: 0x0400197C RID: 6524
      public const string OnboardingFinaleAction = "onboarding_finale_action";

      // Token: 0x0400197D RID: 6525
      public const string OnboardingDriveStepAction = "onboarding_drive_step_action";

      // Token: 0x0400197E RID: 6526
      public const string MorningBriefAction = "morning_brief_action";

      // Token: 0x0400197F RID: 6527
      public const string NoteObjectAction = "note_object_action";

      // Token: 0x04001980 RID: 6528
      public const string JobReportAction = "job_report_action";

      // Token: 0x04001981 RID: 6529
      public const string NotchChatAction = "notch_chat_action";

      // Token: 0x04001982 RID: 6530
      public const string AgentRequestAnswered = "agent_request_answered";
    }

    // Token: 0x020003BA RID: 954
    [Nullable(0)]
    public sealed class SuggestionCardInfo : IEquatable<ClickyAnalytics.SuggestionCardInfo>
    {
      // Token: 0x06002A03 RID: 10755 RVA: 0x00425424 File Offset: 0x00425424
      public SuggestionCardInfo(string SuggestionId, string Slot, bool CreatesClicky, bool HasRoutine, [Nullable(2)] string ClickySlug)
      {
        this.SuggestionId = SuggestionId;
        this.Slot = Slot;
        this.CreatesClicky = CreatesClicky;
        this.HasRoutine = HasRoutine;
        this.ClickySlug = ClickySlug;
        base..ctor();
      }

      // Token: 0x17000815 RID: 2069
      // (get) Token: 0x06002A04 RID: 10756 RVA: 0x00425454 File Offset: 0x00425454
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyAnalytics.SuggestionCardInfo);
        }
      }

      // Token: 0x17000816 RID: 2070
      // (get) Token: 0x06002A05 RID: 10757 RVA: 0x00425460 File Offset: 0x00425460
      // (set) Token: 0x06002A06 RID: 10758 RVA: 0x00425468 File Offset: 0x00425468
      public string SuggestionId { get; set; }

      // Token: 0x17000817 RID: 2071
      // (get) Token: 0x06002A07 RID: 10759 RVA: 0x00425474 File Offset: 0x00425474
      // (set) Token: 0x06002A08 RID: 10760 RVA: 0x0042547C File Offset: 0x0042547C
      public string Slot { get; set; }

      // Token: 0x17000818 RID: 2072
      // (get) Token: 0x06002A09 RID: 10761 RVA: 0x00425488 File Offset: 0x00425488
      // (set) Token: 0x06002A0A RID: 10762 RVA: 0x00425490 File Offset: 0x00425490
      public bool CreatesClicky { get; set; }

      // Token: 0x17000819 RID: 2073
      // (get) Token: 0x06002A0B RID: 10763 RVA: 0x0042549C File Offset: 0x0042549C
      // (set) Token: 0x06002A0C RID: 10764 RVA: 0x004254A4 File Offset: 0x004254A4
      public bool HasRoutine { get; set; }

      // Token: 0x1700081A RID: 2074
      // (get) Token: 0x06002A0D RID: 10765 RVA: 0x004254B0 File Offset: 0x004254B0
      // (set) Token: 0x06002A0E RID: 10766 RVA: 0x004254B8 File Offset: 0x004254B8
      [Nullable(2)]
      public string ClickySlug
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06002A0F RID: 10767 RVA: 0x004254C4 File Offset: 0x004254C4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SuggestionCardInfo");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A10 RID: 10768 RVA: 0x00425510 File Offset: 0x00425510
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("SuggestionId = ");
        builder.Append(this.SuggestionId);
        builder.Append(", Slot = ");
        builder.Append(this.Slot);
        builder.Append(", CreatesClicky = ");
        builder.Append(this.CreatesClicky.ToString());
        builder.Append(", HasRoutine = ");
        builder.Append(this.HasRoutine.ToString());
        builder.Append(", ClickySlug = ");
        builder.Append(this.ClickySlug);
        return true;
      }

      // Token: 0x06002A11 RID: 10769 RVA: 0x004255BC File Offset: 0x004255BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyAnalytics.SuggestionCardInfo left, ClickyAnalytics.SuggestionCardInfo right)
      {
        return !(left == right);
      }

      // Token: 0x06002A12 RID: 10770 RVA: 0x004255C8 File Offset: 0x004255C8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyAnalytics.SuggestionCardInfo left, ClickyAnalytics.SuggestionCardInfo right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A13 RID: 10771 RVA: 0x004255DC File Offset: 0x004255DC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SuggestionId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slot>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<CreatesClicky>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HasRoutine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField);
      }

      // Token: 0x06002A14 RID: 10772 RVA: 0x0042566C File Offset: 0x0042566C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyAnalytics.SuggestionCardInfo);
      }

      // Token: 0x06002A15 RID: 10773 RVA: 0x0042567C File Offset: 0x0042567C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyAnalytics.SuggestionCardInfo other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SuggestionId>k__BackingField, other.<SuggestionId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Slot>k__BackingField, other.<Slot>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<CreatesClicky>k__BackingField, other.<CreatesClicky>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HasRoutine>k__BackingField, other.<HasRoutine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField));
      }

      // Token: 0x06002A17 RID: 10775 RVA: 0x0042572C File Offset: 0x0042572C
      [CompilerGenerated]
      private SuggestionCardInfo(ClickyAnalytics.SuggestionCardInfo original)
      {
        this.SuggestionId = original.<SuggestionId>k__BackingField;
        this.Slot = original.<Slot>k__BackingField;
        this.CreatesClicky = original.<CreatesClicky>k__BackingField;
        this.HasRoutine = original.<HasRoutine>k__BackingField;
        this.ClickySlug = original.<ClickySlug>k__BackingField;
      }

      // Token: 0x06002A18 RID: 10776 RVA: 0x0042577C File Offset: 0x0042577C
      [CompilerGenerated]
      public void Deconstruct(out string SuggestionId, out string Slot, out bool CreatesClicky, out bool HasRoutine, [Nullable(2)] out string ClickySlug)
      {
        SuggestionId = this.SuggestionId;
        Slot = this.Slot;
        CreatesClicky = this.CreatesClicky;
        HasRoutine = this.HasRoutine;
        ClickySlug = this.ClickySlug;
      }
    }
  }
}
