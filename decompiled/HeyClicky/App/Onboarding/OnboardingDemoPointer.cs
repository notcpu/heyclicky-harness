using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core;
using HeyClicky.Core.Net;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000125 RID: 293
  [NullableContext(1)]
  [Nullable(0)]
  internal static class OnboardingDemoPointer
  {
    // Token: 0x06001014 RID: 4116 RVA: 0x0058198C File Offset: 0x0058198C
    public static async Task RunAsync(CoreServices core, IScreenCaptureService capture, ICursorOverlay cursor, Func<VoiceState> voiceState)
    {
      VoiceState voiceState2 = voiceState();
      if (voiceState2 == VoiceState.Idle || voiceState2 == VoiceState.Responding)
      {
        if (Interlocked.Exchange(ref OnboardingDemoPointer.running, 1) == 1)
        {
          Log.Info("video", "pointing demo: one is already in flight");
        }
        else
        {
          try
          {
            string text = await core.Auth.GetValidAccessTokenAsync(default(CancellationToken)).ConfigureAwait(false);
            string token = text;
            if (string.IsNullOrEmpty(token))
            {
              Log.Info("video", "pointing demo skipped: not signed in");
            }
            else
            {
              IReadOnlyList<ScreenCaptureFrame> readOnlyList = await capture.CaptureAllAsync(1280, 80, default(CancellationToken)).ConfigureAwait(false);
              int num = readOnlyList.ToList<ScreenCaptureFrame>().FindIndex((ScreenCaptureFrame f) => f.ContainsCursor);
              if (num < 0)
              {
                Log.Info("video", "pointing demo: no screen under the pointer");
              }
              else
              {
                ScreenCaptureFrame frame = readOnlyList[num];
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
                string text2;
                if (readOnlyList.Count != 1)
                {
                  defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("screen ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(num + 1);
                  defaultInterpolatedStringHandler.AppendLiteral(" of ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(readOnlyList.Count);
                  defaultInterpolatedStringHandler.AppendLiteral(" — cursor is on this screen (primary focus)");
                  text2 = defaultInterpolatedStringHandler.ToStringAndClear();
                }
                else
                {
                  text2 = "user's screen (cursor is here)";
                }
                defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
                defaultInterpolatedStringHandler.AppendLiteral(" (image dimensions: ");
                defaultInterpolatedStringHandler.AppendFormatted<int>(frame.PixelWidth);
                defaultInterpolatedStringHandler.AppendLiteral("x");
                defaultInterpolatedStringHandler.AppendFormatted<int>(frame.PixelHeight);
                defaultInterpolatedStringHandler.AppendLiteral(" pixels)");
                string text3 = text2 + defaultInterpolatedStringHandler.ToStringAndClear();
                string text4 = await OnboardingDemoPointer.AskAsync(core.Http, token, frame.Jpeg, text3).ConfigureAwait(false);
                ValueTuple<string, double, double, string>? valueTuple = OnboardingDemoPointer.Parse(text4);
                if (valueTuple != null)
                {
                  ValueTuple<string, double, double, string> valueOrDefault = valueTuple.GetValueOrDefault();
                  RectD boundsPx = frame.Display.BoundsPx;
                  double num2 = Math.Clamp(valueOrDefault.Item2, 0.0, (double)frame.PixelWidth);
                  double num3 = Math.Clamp(valueOrDefault.Item3, 0.0, (double)frame.PixelHeight);
                  PointD pointD = new PointD(boundsPx.X + num2 * boundsPx.Width / (double)Math.Max(1, frame.PixelWidth), boundsPx.Y + num3 * boundsPx.Height / (double)Math.Max(1, frame.PixelHeight));
                  string text5 = "video";
                  defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("pointing demo: at \"");
                  defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.Item4 ?? "element");
                  defaultInterpolatedStringHandler.AppendLiteral("\" with \"");
                  defaultInterpolatedStringHandler.AppendFormatted(valueOrDefault.Item1);
                  defaultInterpolatedStringHandler.AppendLiteral("\"");
                  Log.Info(text5, defaultInterpolatedStringHandler.ToStringAndClear());
                  await cursor.PointAtAsync(pointD, valueOrDefault.Item1, default(CancellationToken)).ConfigureAwait(false);
                  token = null;
                  frame = null;
                }
                else
                {
                  Log.Info("video", "pointing demo: nothing to point at (\"" + text4.Trim() + "\")");
                }
              }
            }
          }
          catch (Exception ex)
          {
            Log.Warn("video", "pointing demo: " + ex.Message);
          }
          finally
          {
            Volatile.Write(ref OnboardingDemoPointer.running, 0);
          }
        }
      }
    }

    // Token: 0x06001015 RID: 4117 RVA: 0x005819E8 File Offset: 0x005819E8
    [return: TupleElementNames(new string[] { "Comment", "X", "Y", "Label" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    internal static ValueTuple<string, double, double, string>? Parse(string reply)
    {
      string text = reply.Trim();
      Match match = OnboardingDemoPointer.PointTag.Match(text);
      if (!match.Success || !match.Groups[1].Success)
      {
        return null;
      }
      double num = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
      double num2 = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
      return new ValueTuple<string, double, double, string>?(new ValueTuple<string, double, double, string>(text.Substring(0, match.Index).Trim(), num, num2, match.Groups[3].Success ? match.Groups[3].Value.Trim() : null));
    }

    // Token: 0x06001016 RID: 4118 RVA: 0x00581AB0 File Offset: 0x00581AB0
    internal static async Task<string> AskAsync(ClickyHttp http, string token, byte[] jpeg, string imageLabel)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["prompt_profile"] = "onboarding_demo";
      jsonObject["response_model_selection"] = "standard";
      jsonObject["user_prompt"] = "look around my screen and find something interesting to point at";
      string text2 = "images";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["base64_image_data"] = Convert.ToBase64String(jpeg);
      jsonObject2["image_label"] = imageLabel;
      jsonObject2["image_media_type"] = "image/jpeg";
      JsonNode jsonNode = jsonObject2;
      jsonObject[text2] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode));
      JsonObject jsonObject3 = jsonObject;
      string text5;
      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, http.WorkerUrl("/chat"))
      {
        Content = new StringContent(jsonObject3.ToJsonString(null), Encoding.UTF8, "application/json")
      })
      {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        using (CancellationTokenSource timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30L)))
        {
          HttpResponseMessage httpResponseMessage = await http.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, timeout.Token).ConfigureAwait(false);
          using (HttpResponseMessage response = httpResponseMessage)
          {
            if (!response.IsSuccessStatusCode)
            {
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 1);
              defaultInterpolatedStringHandler.AppendLiteral("/chat answered ");
              defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
              throw new HttpRequestException(defaultInterpolatedStringHandler.ToStringAndClear());
            }
            using (StreamReader reader = new StreamReader(await response.Content.ReadAsStreamAsync(timeout.Token).ConfigureAwait(false)))
            {
              StringBuilder text = new StringBuilder();
              for (;;)
              {
                string text3 = await reader.ReadLineAsync(timeout.Token).ConfigureAwait(false);
                if (text3 == null)
                {
                  break;
                }
                if (text3.StartsWith("data: ", StringComparison.Ordinal))
                {
                  string text4 = text3.Substring(6, text3.Length - 6);
                  if (!(text4 == "[DONE]"))
                  {
                    try
                    {
                      using (JsonDocument jsonDocument = JsonDocument.Parse(text4, default(JsonDocumentOptions)))
                      {
                        JsonElement rootElement = jsonDocument.RootElement;
                        JsonElement jsonElement;
                        JsonElement jsonElement2;
                        JsonElement jsonElement3;
                        JsonElement jsonElement4;
                        JsonElement jsonElement5;
                        JsonElement jsonElement6;
                        if (rootElement.TryGetProperty("type", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String && jsonElement.GetString() == "content_block_delta" && rootElement.TryGetProperty("delta", out jsonElement2) && jsonElement2.TryGetProperty("text", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.String)
                        {
                          text.Append(jsonElement3.GetString());
                        }
                        else if (rootElement.TryGetProperty("choices", out jsonElement4) && jsonElement4.ValueKind == JsonValueKind.Array && jsonElement4.GetArrayLength() > 0 && jsonElement4[0].TryGetProperty("delta", out jsonElement5) && jsonElement5.TryGetProperty("content", out jsonElement6) && jsonElement6.ValueKind == JsonValueKind.String)
                        {
                          text.Append(jsonElement6.GetString());
                        }
                      }
                      continue;
                    }
                    catch (JsonException)
                    {
                      continue;
                    }
                    break;
                  }
                  break;
                }
              }
              text5 = text.ToString();
            }
          }
        }
      }
      return text5;
    }

    // Token: 0x040006EE RID: 1774
    private const string UserPrompt = "look around my screen and find something interesting to point at";

    // Token: 0x040006EF RID: 1775
    private static int running;

    // Token: 0x040006F0 RID: 1776
    private static readonly Regex PointTag = new Regex("\\[POINT:(?:none|(\\d+(?:\\.\\d+)?)\\s*,\\s*(\\d+(?:\\.\\d+)?)(?::([^\\]:\\s][^\\]:]*?))?(?::screen(\\d+))?)\\]\\s*$", RegexOptions.CultureInvariant);
  }
}
