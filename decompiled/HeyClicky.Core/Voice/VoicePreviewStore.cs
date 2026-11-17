using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Configuration;
using HeyClicky.Core.Net;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000091 RID: 145
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class VoicePreviewStore
  {
    // Token: 0x170001ED RID: 493
    // (get) Token: 0x060008C5 RID: 2245 RVA: 0x0036BDA4 File Offset: 0x0036BDA4
    // (set) Token: 0x060008C6 RID: 2246 RVA: 0x0036BDAC File Offset: 0x0036BDAC
    [Nullable(2)]
    public static VoicePreviewStore Shared
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x14000014 RID: 20
    // (add) Token: 0x060008C7 RID: 2247 RVA: 0x0036BDB4 File Offset: 0x0036BDB4
    // (remove) Token: 0x060008C8 RID: 2248 RVA: 0x0036BDEC File Offset: 0x0036BDEC
    [Nullable(new byte[] { 2, 1, 1 })]
    [field: Nullable(new byte[] { 2, 1, 1 })]
    public event Action<string, string> PreviewMade;

    // Token: 0x060008C9 RID: 2249 RVA: 0x0036BE24 File Offset: 0x0036BE24
    public VoicePreviewStore(ClickyHttp http, [Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> accessToken, [Nullable(2)] string directory = null)
      : this(accessToken, (string token, byte[] body, CancellationToken ct) => VoicePreviewStore.PostAsync(http, token, body, ct), directory)
    {
    }

    // Token: 0x060008CA RID: 2250 RVA: 0x0036BE54 File Offset: 0x0036BE54
    [NullableContext(2)]
    internal VoicePreviewStore([Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> accessToken, [Nullable(new byte[] { 1, 1, 1, 1, 2 })] Func<string, byte[], CancellationToken, Task<byte[]>> fetch, string directory)
    {
      this.accessToken = accessToken;
      this.fetch = fetch;
      this.directory = directory ?? Path.Combine(AppBundleConfiguration.AppDataDirectory, "VoicePreviews");
    }

    // Token: 0x060008CB RID: 2251 RVA: 0x0036BEA8 File Offset: 0x0036BEA8
    public string FilePath(string voiceId, string languageCode)
    {
      string text = new string((VoiceCatalog.GeminiName(voiceId).ToLowerInvariant() + "-" + languageCode).Select<char, char>(delegate(char c)
      {
        bool flag = char.IsAsciiLetterOrDigit(c);
        if (!flag)
        {
          bool flag2 = c == '-' || c == '_';
          flag = flag2;
        }
        if (!flag)
        {
          return '_';
        }
        return c;
      }).ToArray<char>());
      return Path.Combine(this.directory, text + ".wav");
    }

    // Token: 0x060008CC RID: 2252 RVA: 0x0036BF10 File Offset: 0x0036BF10
    [return: Nullable(2)]
    public string Cached(string voiceId, string languageCode)
    {
      string text = this.FilePath(voiceId, languageCode);
      if (!File.Exists(text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x060008CD RID: 2253 RVA: 0x0036BF34 File Offset: 0x0036BF34
    public bool IsMaking(string voiceId, string languageCode)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        Task<string> task;
        flag2 = this.inFlight.TryGetValue(VoicePreviewStore.Key(voiceId, languageCode), out task) && !task.IsCompleted;
      }
      return flag2;
    }

    // Token: 0x060008CE RID: 2254 RVA: 0x0036BF94 File Offset: 0x0036BF94
    [return: Nullable(new byte[] { 1, 2 })]
    public Task<string> PreviewAsync(string voiceId, string languageCode, CancellationToken ct = default(CancellationToken))
    {
      string text = this.Cached(voiceId, languageCode);
      if (text != null)
      {
        return Task.FromResult<string>(text);
      }
      string text2 = VoicePreviewStore.Key(voiceId, languageCode);
      object obj = this.gate;
      Task<string> task2;
      lock (obj)
      {
        Task<string> task;
        if (this.inFlight.TryGetValue(text2, out task) && !task.IsCompleted)
        {
          task2 = task;
        }
        else
        {
          Task<string> task3 = this.MakeAsync(voiceId, languageCode, ct);
          this.inFlight[text2] = task3;
          task2 = task3;
        }
      }
      return task2;
    }

    // Token: 0x060008CF RID: 2255 RVA: 0x0036C028 File Offset: 0x0036C028
    public Task PrefetchAsync(IReadOnlyList<string> voiceIds, string languageCode)
    {
      VoicePreviewStore.<>c__DisplayClass20_0 CS$<>8__locals1 = new VoicePreviewStore.<>c__DisplayClass20_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.languageCode = languageCode;
      CS$<>8__locals1.cts = new CancellationTokenSource();
      CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.prefetch, CS$<>8__locals1.cts);
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CS$<>8__locals1.missing = voiceIds.Where<string>((string id) => CS$<>8__locals1.<>4__this.Cached(id, CS$<>8__locals1.languageCode) == null).ToList<string>();
      if (CS$<>8__locals1.missing.Count == 0)
      {
        return Task.CompletedTask;
      }
      string text = "VoicePreview";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
      defaultInterpolatedStringHandler.AppendLiteral("prefetching ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.missing.Count);
      defaultInterpolatedStringHandler.AppendLiteral(" preview(s) in ");
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.languageCode);
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      return Task.Run(delegate
      {
        VoicePreviewStore.<>c__DisplayClass20_0.<<PrefetchAsync>b__1>d <<PrefetchAsync>b__1>d;
        <<PrefetchAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<PrefetchAsync>b__1>d.<>4__this = CS$<>8__locals1;
        <<PrefetchAsync>b__1>d.<>1__state = -1;
        <<PrefetchAsync>b__1>d.<>t__builder.Start<VoicePreviewStore.<>c__DisplayClass20_0.<<PrefetchAsync>b__1>d>(ref <<PrefetchAsync>b__1>d);
        return <<PrefetchAsync>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x060008D0 RID: 2256 RVA: 0x0036C104 File Offset: 0x0036C104
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<string> MakeAsync(string voiceId, string languageCode, CancellationToken ct)
    {
      string text2;
      try
      {
        string text = await this.accessToken(ct).ConfigureAwait(false);
        if (string.IsNullOrEmpty(text))
        {
          text2 = null;
        }
        else
        {
          using (MemoryStream buffer = new MemoryStream())
          {
            using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(buffer, default(JsonWriterOptions)))
            {
              utf8JsonWriter.WriteStartObject();
              utf8JsonWriter.WriteString("voice", VoiceCatalog.GeminiName(voiceId));
              utf8JsonWriter.WriteString("language", languageCode);
              utf8JsonWriter.WriteEndObject();
            }
            byte[] array = buffer.ToArray();
            byte[] array2 = await this.fetch(text, array, ct).ConfigureAwait(false);
            if (array2 == null || array2.Length <= 44 || Encoding.ASCII.GetString(array2, 0, 4) != "RIFF")
            {
              Log.Info("VoicePreview", voiceId + " in " + languageCode + " not made");
              text2 = null;
            }
            else
            {
              string path = this.FilePath(voiceId, languageCode);
              Directory.CreateDirectory(this.directory);
              string temporary = path + ".part";
              await File.WriteAllBytesAsync(temporary, array2, ct).ConfigureAwait(false);
              File.Move(temporary, path, true);
              Action<string, string> previewMade = this.PreviewMade;
              if (previewMade != null)
              {
                previewMade(voiceId, languageCode);
              }
              text2 = path;
            }
          }
        }
      }
      catch (Exception ex)
      {
        string text3 = "VoicePreview";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 3);
        defaultInterpolatedStringHandler.AppendFormatted(voiceId);
        defaultInterpolatedStringHandler.AppendLiteral(" in ");
        defaultInterpolatedStringHandler.AppendFormatted(languageCode);
        defaultInterpolatedStringHandler.AppendLiteral(" failed: ");
        defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
        Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060008D1 RID: 2257 RVA: 0x0036C160 File Offset: 0x0036C160
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<byte[]> PostAsync(ClickyHttp http, string token, byte[] body, CancellationToken ct)
    {
      byte[] array;
      using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, http.WorkerUrl("/agent/realtime/voice-preview")))
      {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new ByteArrayContent(body);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using (CancellationTokenSource timeout = CancellationTokenSource.CreateLinkedTokenSource(ct))
        {
          timeout.CancelAfter(TimeSpan.FromSeconds(30L));
          HttpResponseMessage httpResponseMessage = await http.Client.SendAsync(request, timeout.Token).ConfigureAwait(false);
          using (HttpResponseMessage response = httpResponseMessage)
          {
            array = ((!response.IsSuccessStatusCode) ? null : (await response.Content.ReadAsByteArrayAsync(timeout.Token).ConfigureAwait(false)));
          }
        }
      }
      return array;
    }

    // Token: 0x060008D2 RID: 2258 RVA: 0x0036C1BC File Offset: 0x0036C1BC
    private static string Key(string voiceId, string languageCode)
    {
      return voiceId + "|" + languageCode;
    }

    // Token: 0x0400041C RID: 1052
    public const int PrefetchConcurrency = 3;

    // Token: 0x0400041D RID: 1053
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<CancellationToken, Task<string>> accessToken;

    // Token: 0x0400041E RID: 1054
    [Nullable(new byte[] { 1, 1, 1, 1, 2 })]
    private readonly Func<string, byte[], CancellationToken, Task<byte[]>> fetch;

    // Token: 0x0400041F RID: 1055
    private readonly string directory;

    // Token: 0x04000420 RID: 1056
    private readonly object gate = new object();

    // Token: 0x04000421 RID: 1057
    [Nullable(new byte[] { 1, 1, 1, 2 })]
    private readonly Dictionary<string, Task<string>> inFlight = new Dictionary<string, Task<string>>();

    // Token: 0x04000422 RID: 1058
    [Nullable(2)]
    private CancellationTokenSource prefetch;
  }
}
