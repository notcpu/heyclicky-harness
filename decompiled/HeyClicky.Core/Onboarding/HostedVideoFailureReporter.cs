using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000EC RID: 236
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HostedVideoFailureReporter
  {
    // Token: 0x06000F29 RID: 3881 RVA: 0x0038DDB4 File Offset: 0x0038DDB4
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.reportedSignatures.Clear();
      }
    }

    // Token: 0x06000F2A RID: 3882 RVA: 0x0038DDFC File Offset: 0x0038DDFC
    public unsafe bool Report(HostedVideoFailure failure)
    {
      char c = '|';
      <>y__InlineArray7<object> <>y__InlineArray = default(<>y__InlineArray7<object>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 0) = failure.VideoKind;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 1) = failure.Phase;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 2) = failure.Reason;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 3) = failure.AssetUrl;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 4) = failure.ErrorDomain;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 5) = failure.ErrorCode;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray7<object>, object>(ref <>y__InlineArray, 6) = failure.HttpStatus.GetValueOrDefault();
      string text = string.Join(c, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray7<object>, object>(in <>y__InlineArray, 7));
      object obj = this.gate;
      lock (obj)
      {
        if (!this.reportedSignatures.Add(text))
        {
          return false;
        }
      }
      JsonObject jsonObject = HostedVideoFailureReporter.Properties(failure);
      string text2 = "video";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 5);
      defaultInterpolatedStringHandler.AppendLiteral("intro video failed: ");
      defaultInterpolatedStringHandler.AppendFormatted(failure.VideoKind);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(failure.Phase);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(failure.Reason);
      defaultInterpolatedStringHandler.AppendLiteral(": ");
      defaultInterpolatedStringHandler.AppendFormatted(failure.ErrorMessage);
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted(failure.AssetUrl);
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Warn(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      ClickyAnalytics.TrackHostedVideoFailure(jsonObject);
      ICrashReporter crashReporter = CrashReporter.Current;
      if (!crashReporter.IsEnabled)
      {
        return true;
      }
      int errorCode = failure.ErrorCode;
      bool flag = errorCode == -2147221231 || errorCode == -2147221164;
      if (flag)
      {
        return true;
      }
      try
      {
        ICrashReporter crashReporter2 = crashReporter;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Hosted video failed: ");
        defaultInterpolatedStringHandler.AppendFormatted(failure.VideoKind);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(failure.Phase);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(failure.Reason);
        string text3 = defaultInterpolatedStringHandler.ToStringAndClear();
        CrashReportLevel crashReportLevel = CrashReportLevel.Error;
        IReadOnlyList<string> readOnlyList = new <>z__ReadOnlyArray<string>(new string[]
        {
          "hosted_video",
          failure.VideoKind,
          failure.Phase,
          failure.Reason,
          failure.ErrorDomain,
          failure.ErrorCode.ToString()
        });
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary["video.kind"] = failure.VideoKind;
        dictionary["video.phase"] = failure.Phase;
        dictionary["video.failure_reason"] = failure.Reason;
        crashReporter2.CaptureMessage(text3, crashReportLevel, readOnlyList, dictionary, "hosted_video", (JsonObject)jsonObject.DeepClone(), null);
      }
      catch (Exception ex)
      {
        Log.Warn("video", "crash reporter capture failed: " + ex.Message);
      }
      return true;
    }

    // Token: 0x06000F2B RID: 3883 RVA: 0x0038E0F8 File Offset: 0x0038E0F8
    public static JsonObject Properties(HostedVideoFailure failure)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["video_kind"] = failure.VideoKind;
      jsonObject["phase"] = failure.Phase;
      jsonObject["reason"] = failure.Reason;
      jsonObject["player_status"] = failure.PlayerStatus;
      jsonObject["item_status"] = failure.ItemStatus;
      jsonObject["time_control_status"] = failure.TimeControlStatus;
      jsonObject["asset_url"] = failure.AssetUrl;
      jsonObject["error_domain"] = failure.ErrorDomain;
      jsonObject["error_code"] = failure.ErrorCode;
      jsonObject["error_message"] = failure.ErrorMessage;
      JsonObject jsonObject2 = jsonObject;
      int? httpStatus = failure.HttpStatus;
      if (httpStatus != null)
      {
        int valueOrDefault = httpStatus.GetValueOrDefault();
        if (valueOrDefault != 0)
        {
          jsonObject2["http_status"] = valueOrDefault;
        }
      }
      return jsonObject2;
    }

    // Token: 0x04000677 RID: 1655
    private readonly HashSet<string> reportedSignatures = new HashSet<string>();

    // Token: 0x04000678 RID: 1656
    private readonly object gate = new object();
  }
}
