using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Net;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001EC RID: 492
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainClient
  {
    // Token: 0x06001CBD RID: 7357 RVA: 0x003E49B0 File Offset: 0x003E49B0
    public BrainClient(ClickyHttp http)
    {
      this.http = http;
    }

    // Token: 0x06001CBE RID: 7358 RVA: 0x003E49C0 File Offset: 0x003E49C0
    public async Task<BrainReply> StreamTurnAsync(Action<Utf8JsonWriter> writeBody, string accessToken, string usageContext, BrainReplyAccumulator accumulator, CancellationToken ct, [Nullable(2)] Action onResponseHeaders = null, [Nullable(2)] Action<int, bool> onRequestBody = null)
    {
      BrainClient.<>c__DisplayClass3_0 CS$<>8__locals1 = new BrainClient.<>c__DisplayClass3_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.accessToken = accessToken;
      CS$<>8__locals1.usageContext = usageContext;
      ArrayBufferWriter<byte> arrayBufferWriter = new ArrayBufferWriter<byte>(262144);
      using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter, new JsonWriterOptions
      {
        SkipValidation = true
      }))
      {
        writeBody(utf8JsonWriter);
      }
      byte[] bodyBytes = arrayBufferWriter.WrittenMemory.ToArray();
      ValueTuple<HttpContent, int, bool> valueTuple = BrainRequestCompression.Content(bodyBytes, bodyBytes.Length, "/brain/turn");
      CS$<>8__locals1.content = valueTuple.Item1;
      int sentBytes = valueTuple.Item2;
      bool isGzip = valueTuple.Item3;
      byte[] array = await CS$<>8__locals1.content.ReadAsByteArrayAsync(ct).ConfigureAwait(false);
      CS$<>8__locals1.sentBody = array;
      if (onRequestBody != null)
      {
        onRequestBody(sentBytes, isGzip);
      }
      if (isGzip)
      {
        string text = "OneBrain";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 2);
        defaultInterpolatedStringHandler.AppendLiteral("turn body gzip ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(bodyBytes.Length / 1024);
        defaultInterpolatedStringHandler.AppendLiteral(" KB → ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(sentBytes / 1024);
        defaultInterpolatedStringHandler.AppendLiteral(" KB");
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_BRAIN_DUMP_DIR");
      if (environmentVariable != null && environmentVariable.Length > 0)
      {
        Directory.CreateDirectory(environmentVariable);
        string text2 = environmentVariable;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
        defaultInterpolatedStringHandler.AppendLiteral("turn-");
        defaultInterpolatedStringHandler.AppendFormatted<DateTime>(DateTime.Now, "HHmmss-fff");
        defaultInterpolatedStringHandler.AppendLiteral(".json");
        File.WriteAllBytes(Path.Combine(text2, defaultInterpolatedStringHandler.ToStringAndClear()), bodyBytes);
      }
      Stream stream;
      byte[] buffer;
      using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
      {
        timeout.CancelAfter(TimeSpan.FromSeconds(120L));
        using (HttpResponseMessage response = await this.SendWithOneRetryAsync(new Func<HttpRequestMessage>(CS$<>8__locals1.<StreamTurnAsync>g__BuildRequest|0), timeout.Token, ct).ConfigureAwait(false))
        {
          if (onResponseHeaders != null)
          {
            onResponseHeaders();
          }
          BrainRequestCompression.NoteResponse(response, "/brain/turn");
          int status = (int)response.StatusCode;
          if (status != 200)
          {
            byte[] array2 = await response.Content.ReadAsByteArrayAsync(timeout.Token).ConfigureAwait(false);
            PaywallPayload paywallPayload;
            JsonElement jsonElement;
            if (BillingPaywallDetector.HandleIfPaywall(status, array2, true, out paywallPayload, out jsonElement))
            {
              throw new PaywallReachedException(paywallPayload, jsonElement);
            }
            string @string = Encoding.UTF8.GetString(array2.AsSpan<byte>(0, Math.Min(400, array2.Length)));
            if (status == 409 && @string.Contains("screenshot_files_unavailable", StringComparison.Ordinal))
            {
              throw new ScreenshotFilesUnavailableException();
            }
            throw new WorkerHttpException(status, @string, "/brain/turn");
          }
          else
          {
            stream = await response.Content.ReadAsStreamAsync(timeout.Token).ConfigureAwait(false);
            object obj = null;
            int num = 0;
            BrainReply brainReply;
            try
            {
              using (BrainSseParser parser = new BrainSseParser())
              {
                buffer = ArrayPool<byte>.Shared.Rent(16384);
                try
                {
                  for (;;)
                  {
                    int num2 = await stream.ReadAsync(buffer, timeout.Token).ConfigureAwait(false);
                    if (num2 == 0)
                    {
                      break;
                    }
                    parser.Feed(buffer.AsSpan<byte>(0, num2), accumulator);
                    BrainClient.ThrowIfError(accumulator.Reply);
                  }
                  parser.Complete(accumulator);
                }
                finally
                {
                  ArrayPool<byte>.Shared.Return(buffer, false);
                }
                BrainClient.ThrowIfError(accumulator.Reply);
                if (!accumulator.Reply.SawDone)
                {
                  throw new IOException("the connection ended before the answer was complete");
                }
                accumulator.Finish();
                brainReply = accumulator.Reply;
              }
              num = 1;
            }
            catch (object obj)
            {
            }
            if (stream != null)
            {
              await stream.DisposeAsync();
            }
            object obj2 = obj;
            if (obj2 != null)
            {
              Exception ex = obj2 as Exception;
              if (ex == null)
              {
                throw obj2;
              }
              ExceptionDispatchInfo.Capture(ex).Throw();
            }
            if (num == 1)
            {
              return brainReply;
            }
            obj = null;
            brainReply = null;
          }
        }
      }
      CS$<>8__locals1 = null;
      bodyBytes = null;
      CancellationTokenSource timeout = null;
      HttpResponseMessage response = null;
      stream = null;
      BrainSseParser parser = null;
      buffer = null;
      BrainReply brainReply2;
      return brainReply2;
    }

    // Token: 0x06001CBF RID: 7359 RVA: 0x003E4A40 File Offset: 0x003E4A40
    internal async Task<HttpResponseMessage> SendWithOneRetryAsync(Func<HttpRequestMessage> buildRequest, CancellationToken sendToken, CancellationToken callerToken)
    {
      Stopwatch started = Stopwatch.StartNew();
      HttpRequestMessage first = buildRequest();
      try
      {
        return await this.http.Client.SendAsync(first, HttpCompletionOption.ResponseHeadersRead, sendToken).ConfigureAwait(false);
      }
      catch (Exception ex) when (BrainClient.IsRetryableSendFailure(ex) && !sendToken.IsCancellationRequested && !callerToken.IsCancellationRequested && started.Elapsed < BrainClient.SendRetryWindow)
      {
        first.Dispose();
        string text = "Brain";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(72, 3);
        defaultInterpolatedStringHandler.AppendLiteral("/brain/turn send failed after ");
        defaultInterpolatedStringHandler.AppendFormatted<long>(started.ElapsedMilliseconds);
        defaultInterpolatedStringHandler.AppendLiteral(" ms before any answer (");
        defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
        Exception innerException = ex.InnerException;
        defaultInterpolatedStringHandler.AppendFormatted((innerException != null) ? (": " + innerException.Message) : "");
        defaultInterpolatedStringHandler.AppendLiteral("); sending it again");
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
      }
      await Task.Delay(BrainClient.SendRetryDelay, sendToken).ConfigureAwait(false);
      return await this.http.Client.SendAsync(buildRequest(), HttpCompletionOption.ResponseHeadersRead, sendToken).ConfigureAwait(false);
    }

    // Token: 0x06001CC0 RID: 7360 RVA: 0x003E4A9C File Offset: 0x003E4A9C
    internal static bool IsRetryableSendFailure(Exception ex)
    {
      return ex is HttpRequestException || ex is IOException;
    }

    // Token: 0x06001CC1 RID: 7361 RVA: 0x003E4AC0 File Offset: 0x003E4AC0
    private static void ThrowIfError(BrainReply reply)
    {
      string error = reply.Error;
      if (error == null)
      {
        return;
      }
      if (reply.ErrorCode == "screenshot_files_unavailable")
      {
        throw new ScreenshotFilesUnavailableException();
      }
      throw new BrainTurnException(error);
    }

    // Token: 0x06001CC2 RID: 7362 RVA: 0x003E4AF8 File Offset: 0x003E4AF8
    [return: TupleElementNames(new string[] { "Status", "Json" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public async Task<ValueTuple<int, JsonObject>?> PostJsonAsync(string path, Action<Utf8JsonWriter> writeBody, string accessToken, TimeSpan? timeout = null, [Nullable(2)] string usageContext = null, CancellationToken ct = default(CancellationToken))
    {
      BrainClient.<>c__DisplayClass9_0 CS$<>8__locals1 = new BrainClient.<>c__DisplayClass9_0();
      CS$<>8__locals1.writeBody = writeBody;
      ValueTuple<int, JsonObject>? valueTuple;
      try
      {
        using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, this.http.WorkerUrl(path)))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
          if (usageContext != null)
          {
            request.Headers.TryAddWithoutValidation("X-Clicky-Usage-Context", usageContext);
          }
          if (BrainRequestCompression.GzipPaths.Contains(path) && BrainRequestCompression.WorkerAcceptsGzip)
          {
            ArrayBufferWriter<byte> arrayBufferWriter = new ArrayBufferWriter<byte>(65536);
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter, new JsonWriterOptions
            {
              SkipValidation = true
            }))
            {
              CS$<>8__locals1.<PostJsonAsync>g__Write|0(utf8JsonWriter);
            }
            byte[] array = arrayBufferWriter.WrittenMemory.ToArray();
            request.Content = BrainRequestCompression.Content(array, array.Length, path).Item1;
          }
          else
          {
            request.Content = new JsonWriterContent(new Action<Utf8JsonWriter>(CS$<>8__locals1.<PostJsonAsync>g__Write|0));
          }
          using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
          {
            cts.CancelAfter(timeout ?? TimeSpan.FromSeconds(120L));
            HttpResponseMessage httpResponseMessage = await this.http.Client.SendAsync(request, cts.Token).ConfigureAwait(false);
            using (HttpResponseMessage response = httpResponseMessage)
            {
              BrainRequestCompression.NoteResponse(response, path);
              byte[] array2 = await response.Content.ReadAsByteArrayAsync(cts.Token).ConfigureAwait(false);
              JsonObject jsonObject2;
              try
              {
                JsonObject jsonObject;
                if ((jsonObject = ((array2.Length != 0) ? (JsonNode.Parse(array2, null, default(JsonDocumentOptions)) as JsonObject) : null)) == null)
                {
                  jsonObject = new JsonObject(null);
                }
                jsonObject2 = jsonObject;
              }
              catch (JsonException)
              {
                jsonObject2 = new JsonObject(null);
              }
              valueTuple = new ValueTuple<int, JsonObject>?(new ValueTuple<int, JsonObject>((int)response.StatusCode, jsonObject2));
            }
          }
        }
      }
      catch (Exception ex) when (ex is HttpRequestException || ex is OperationCanceledException || ex is IOException)
      {
        Log.Warn("Brain", path + " failed: " + ex.Message);
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x06001CC3 RID: 7363 RVA: 0x003E4B70 File Offset: 0x003E4B70
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<BrainCompactReply> CompactAsync(IReadOnlyList<JsonNode> thread, string sessionId, int inputTokens, string accessToken, CancellationToken ct = default(CancellationToken))
    {
      ValueTuple<int, JsonObject>? valueTuple = await this.PostJsonAsync("/brain/compact", delegate(Utf8JsonWriter writer)
      {
        BrainClient.WriteThread(writer, "thread", thread);
        writer.WriteString("session_id", sessionId);
        writer.WriteNumber("input_tokens", inputTokens);
      }, accessToken, new TimeSpan?(TimeSpan.FromSeconds(180L)), null, ct).ConfigureAwait(false);
      BrainCompactReply brainCompactReply;
      if (valueTuple != null)
      {
        ValueTuple<int, JsonObject> valueOrDefault = valueTuple.GetValueOrDefault();
        JsonObject jsonObject = valueOrDefault.Item2["note_item"] as JsonObject;
        JsonNode jsonNode = valueOrDefault.Item2["drop_leading_items"];
        double? num = ((jsonNode != null) ? new double?(jsonNode.AsValue().GetValue<double>()) : null);
        int valueOrDefault2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null).GetValueOrDefault();
        if (valueOrDefault.Item1 == 200 && (jsonObject != null || valueOrDefault2 > 0))
        {
          JsonValue jsonValue = valueOrDefault.Item2["keep_from_index"] as JsonValue;
          if (jsonValue != null)
          {
            JsonNode jsonNode2 = ((jsonObject != null) ? jsonObject.DeepClone() : null);
            int num2 = (int)jsonValue.GetValue<double>();
            JsonNode jsonNode3 = valueOrDefault.Item2["keep_leading_items"];
            num = ((jsonNode3 != null) ? new double?(jsonNode3.AsValue().GetValue<double>()) : null);
            return new BrainCompactReply(new BrainCompactResult(jsonNode2, num2, ((num != null) ? new int?((int)num.GetValueOrDefault()) : null).GetValueOrDefault(), valueOrDefault2), null);
          }
        }
        string text = "Brain";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
        defaultInterpolatedStringHandler.AppendLiteral("no compaction (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault.Item1);
        defaultInterpolatedStringHandler.AppendLiteral("): ");
        defaultInterpolatedStringHandler.AppendFormatted<JsonNode>(valueOrDefault.Item2["reason"] ?? valueOrDefault.Item2["error"]);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        if (valueOrDefault.Item1 == 200)
        {
          JsonValue jsonValue2 = valueOrDefault.Item2["tokens_until_fold"] as JsonValue;
          if (jsonValue2 != null)
          {
            return new BrainCompactReply(null, new int?((int)jsonValue2.GetValue<double>()));
          }
        }
        brainCompactReply = null;
      }
      else
      {
        Log.Warn("Brain", "compaction request failed");
        brainCompactReply = null;
      }
      return brainCompactReply;
    }

    // Token: 0x06001CC4 RID: 7364 RVA: 0x003E4BE0 File Offset: 0x003E4BE0
    public static void WriteThread(Utf8JsonWriter writer, string propertyName, IReadOnlyList<JsonNode> thread)
    {
      writer.WritePropertyName(propertyName);
      writer.WriteStartArray();
      foreach (JsonNode jsonNode in thread)
      {
        jsonNode.WriteTo(writer, null);
      }
      writer.WriteEndArray();
    }

    // Token: 0x04000C59 RID: 3161
    private const string Area = "Brain";

    // Token: 0x04000C5A RID: 3162
    private readonly ClickyHttp http;

    // Token: 0x04000C5B RID: 3163
    internal static readonly TimeSpan SendRetryWindow = TimeSpan.FromSeconds(8L);

    // Token: 0x04000C5C RID: 3164
    internal static readonly TimeSpan SendRetryDelay = TimeSpan.FromMilliseconds(150L, 0L);
  }
}
