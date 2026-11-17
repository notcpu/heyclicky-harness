using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E9 RID: 489
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LoopbackHttpServer : IDisposable
  {
    // Token: 0x17000657 RID: 1623
    // (get) Token: 0x06001C9D RID: 7325 RVA: 0x003E42C4 File Offset: 0x003E42C4
    public string BearerToken { get; } = LoopbackHttpServer.RandomToken();

    // Token: 0x17000658 RID: 1624
    // (get) Token: 0x06001C9E RID: 7326 RVA: 0x003E42CC File Offset: 0x003E42CC
    public string PathPrefix { get; }

    // Token: 0x06001C9F RID: 7327 RVA: 0x003E42D4 File Offset: 0x003E42D4
    public LoopbackHttpServer(Func<LoopbackHttpServer.Request, Task<LoopbackHttpServer.Response>> handler, string path = "/mcp")
    {
      this.handler = handler;
      this.PathPrefix = path;
    }

    // Token: 0x17000659 RID: 1625
    // (get) Token: 0x06001CA0 RID: 7328 RVA: 0x003E4300 File Offset: 0x003E4300
    public int? Port
    {
      get
      {
        object obj = this.gate;
        int? num;
        lock (obj)
        {
          num = this.port;
        }
        return num;
      }
    }

    // Token: 0x1700065A RID: 1626
    // (get) Token: 0x06001CA1 RID: 7329 RVA: 0x003E4344 File Offset: 0x003E4344
    [Nullable(2)]
    public string Url
    {
      [NullableContext(2)]
      get
      {
        int? num = this.Port;
        if (num != null)
        {
          int valueOrDefault = num.GetValueOrDefault();
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
          defaultInterpolatedStringHandler.AppendLiteral("http://127.0.0.1:");
          defaultInterpolatedStringHandler.AppendFormatted<int>(valueOrDefault);
          defaultInterpolatedStringHandler.AppendFormatted(this.PathPrefix);
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
        return null;
      }
    }

    // Token: 0x06001CA2 RID: 7330 RVA: 0x003E43A0 File Offset: 0x003E43A0
    public void Start()
    {
      object obj = this.gate;
      lock (obj)
      {
        int? num = this.port;
        if (num == null)
        {
          TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 0);
          tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, true);
          tcpListener.Start();
          this.listener = tcpListener;
          this.stop = new CancellationTokenSource();
          this.port = new int?(((IPEndPoint)tcpListener.LocalEndpoint).Port);
          this.AcceptLoopAsync(tcpListener, this.stop.Token);
        }
      }
    }

    // Token: 0x06001CA3 RID: 7331 RVA: 0x003E4454 File Offset: 0x003E4454
    public void Stop()
    {
      object obj = this.gate;
      lock (obj)
      {
        CancellationTokenSource cancellationTokenSource = this.stop;
        if (cancellationTokenSource != null)
        {
          cancellationTokenSource.Cancel();
        }
        try
        {
          TcpListener tcpListener = this.listener;
          if (tcpListener != null)
          {
            tcpListener.Stop();
          }
        }
        catch
        {
        }
        this.listener = null;
        this.port = null;
      }
    }

    // Token: 0x06001CA4 RID: 7332 RVA: 0x003E44D8 File Offset: 0x003E44D8
    public void Dispose()
    {
      this.Stop();
    }

    // Token: 0x1700065B RID: 1627
    // (get) Token: 0x06001CA5 RID: 7333 RVA: 0x003E44E0 File Offset: 0x003E44E0
    public bool IsListening
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          int? num = this.port;
          flag2 = num != null && this.listening;
        }
        return flag2;
      }
    }

    // Token: 0x06001CA6 RID: 7334 RVA: 0x003E4538 File Offset: 0x003E4538
    private async Task AcceptLoopAsync(TcpListener l, CancellationToken ct)
    {
      LoopbackHttpServer.<>c__DisplayClass26_0 CS$<>8__locals1 = new LoopbackHttpServer.<>c__DisplayClass26_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.ct = ct;
      object obj = this.gate;
      lock (obj)
      {
        this.listening = true;
      }
      int consecutiveFailures = 0;
      try
      {
        while (!CS$<>8__locals1.ct.IsCancellationRequested)
        {
          LoopbackHttpServer.<>c__DisplayClass26_1 CS$<>8__locals2 = new LoopbackHttpServer.<>c__DisplayClass26_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          int num = 0;
          bool flag;
          object obj3;
          Exception ex2;
          bool flag2;
          Exception ex;
          try
          {
            TcpClient tcpClient = await l.AcceptTcpClientAsync(CS$<>8__locals2.CS$<>8__locals1.ct).ConfigureAwait(false);
            CS$<>8__locals2.client = tcpClient;
          }
          catch (Exception obj2) when (CS$<>8__locals2.CS$<>8__locals1.ct.IsCancellationRequested)
          {
            break;
          }
          catch (ObjectDisposedException)
          {
            break;
          }
          catch when (delegate
          {
            // Failed to create a 'catch-when' expression
            ex2 = obj3 as Exception;
            if (ex2 == null)
            {
              flag2 = false;
            }
            else
            {
              obj = ex2;
              ex = (Exception)obj;
              flag = ex is SocketException || ex is IOException || ex is InvalidOperationException;
              flag2 = flag > false;
            }
            endfilter(flag2);
          })
          {
            num = 1;
          }
          if (num == 1)
          {
            consecutiveFailures++;
            string text = "Browser";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
            defaultInterpolatedStringHandler.AppendLiteral("loopback accept failed (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(consecutiveFailures);
            defaultInterpolatedStringHandler.AppendLiteral("): ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
            Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
            if (consecutiveFailures >= 20)
            {
              break;
            }
            try
            {
              await Task.Delay(TimeSpan.FromMilliseconds((long)Math.Min(1000, 50 * consecutiveFailures), 0L), CS$<>8__locals2.CS$<>8__locals1.ct).ConfigureAwait(false);
              continue;
            }
            catch (OperationCanceledException)
            {
              break;
            }
          }
          ex = null;
          consecutiveFailures = 0;
          Task.Run(() => CS$<>8__locals2.CS$<>8__locals1.<>4__this.HandleAsync(CS$<>8__locals2.client, CS$<>8__locals2.CS$<>8__locals1.ct));
          CS$<>8__locals2 = null;
        }
      }
      finally
      {
        obj = this.gate;
        bool flag = false;
        try
        {
          Monitor.Enter(obj, ref flag);
          if (this.listener == l)
          {
            this.listening = false;
          }
        }
        finally
        {
          int num2;
          if (num2 < 0 && flag)
          {
            Monitor.Exit(obj);
          }
        }
      }
    }

    // Token: 0x06001CA7 RID: 7335 RVA: 0x003E458C File Offset: 0x003E458C
    private unsafe async Task HandleAsync(TcpClient client, CancellationToken ct)
    {
      using (client)
      {
        try
        {
          client.NoDelay = true;
          NetworkStream stream = client.GetStream();
          MemoryStream buffer = new MemoryStream();
          byte[] chunk = new byte[65536];
          int i = -1;
          while (i < 0)
          {
            int num = await stream.ReadAsync(chunk, ct).ConfigureAwait(false);
            if (num <= 0)
            {
              return;
            }
            buffer.Write(chunk, 0, num);
            i = LoopbackHttpServer.IndexOf(buffer.GetBuffer(), (int)buffer.Length, new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.E7E3A98491005582E7319689C26E4D9D16277B1FA442771172995BC2EFA1822B), 4));
            if (i < 0 && buffer.Length >= 262144L)
            {
              return;
            }
          }
          byte[] buffer2 = buffer.GetBuffer();
          string[] array = Encoding.UTF8.GetString(buffer2, 0, i).Split("\r\n", StringSplitOptions.None);
          string[] array2 = array[0].Split(' ', StringSplitOptions.None);
          string method = ((array2.Length != 0) ? array2[0] : "GET");
          string target = ((array2.Length > 1) ? array2[1] : "/");
          Dictionary<string, string> headers = new Dictionary<string, string>(StringComparer.Ordinal);
          foreach (string text in array.Skip<string>(1))
          {
            int num2 = text.IndexOf(':');
            if (num2 > 0)
            {
              Dictionary<string, string> dictionary = headers;
              string text2 = text.Substring(0, num2).Trim().ToLowerInvariant();
              int num3 = num2 + 1;
              dictionary[text2] = text.Substring(num3, text.Length - num3).Trim();
            }
          }
          int num4;
          int contentLength = (int.TryParse(headers.GetValueOrDefault("content-length"), out num4) ? num4 : 0);
          if (contentLength < 0 || contentLength > 4194304)
          {
            return;
          }
          int bodyStart = i + 4;
          while (buffer.Length - (long)bodyStart < (long)contentLength)
          {
            int num5 = await stream.ReadAsync(chunk, ct).ConfigureAwait(false);
            if (num5 <= 0)
            {
              return;
            }
            buffer.Write(chunk, 0, num5);
          }
          byte[] array3 = new byte[contentLength];
          Array.Copy(buffer.GetBuffer(), bodyStart, array3, 0, contentLength);
          int num6 = target.IndexOfAny(new char[] { '?', '#' });
          string text3 = ((num6 >= 0) ? target.Substring(0, num6) : target);
          LoopbackHttpServer.Response response = await this.RouteAsync(new LoopbackHttpServer.Request(method, text3, headers, array3)).ConfigureAwait(false);
          await LoopbackHttpServer.SendAsync(stream, response, ct).ConfigureAwait(false);
          stream = null;
          buffer = null;
          chunk = null;
          method = null;
          target = null;
          headers = null;
        }
        catch
        {
        }
      }
      TcpClient tcpClient = null;
    }

    // Token: 0x06001CA8 RID: 7336 RVA: 0x003E45E0 File Offset: 0x003E45E0
    private async Task<LoopbackHttpServer.Response> RouteAsync(LoopbackHttpServer.Request request)
    {
      LoopbackHttpServer.Response response;
      if (request.Path != this.PathPrefix && request.Path != this.PathPrefix + "/")
      {
        int num = 404;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["error"] = "not_found";
        response = LoopbackHttpServer.Response.Json(num, jsonObject, null);
      }
      else if (!LoopbackHttpServer.FixedTimeEquals(request.Headers.GetValueOrDefault("authorization"), "Bearer " + this.BearerToken))
      {
        int num2 = 401;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["error"] = "unauthorized";
        response = LoopbackHttpServer.Response.Json(num2, jsonObject2, null);
      }
      else
      {
        response = await this.handler(request).ConfigureAwait(false);
      }
      return response;
    }

    // Token: 0x06001CA9 RID: 7337 RVA: 0x003E462C File Offset: 0x003E462C
    private static bool FixedTimeEquals([Nullable(2)] string a, string b)
    {
      return a != null && CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
    }

    // Token: 0x06001CAA RID: 7338 RVA: 0x003E4658 File Offset: 0x003E4658
    private static async Task SendAsync(NetworkStream stream, LoopbackHttpServer.Response response, CancellationToken ct)
    {
      int status = response.Status;
      string text;
      if (status != 200)
      {
        if (status != 202)
        {
          switch (status)
          {
          case 400:
            text = "Bad Request";
            goto IL_0095;
          case 401:
            text = "Unauthorized";
            goto IL_0095;
          case 404:
            text = "Not Found";
            goto IL_0095;
          case 405:
            text = "Method Not Allowed";
            goto IL_0095;
          }
          text = "Status";
        }
        else
        {
          text = "Accepted";
        }
      }
      else
      {
        text = "OK";
      }
      IL_0095:
      string text2 = text;
      StringBuilder stringBuilder = new StringBuilder();
      StringBuilder stringBuilder2 = stringBuilder;
      StringBuilder stringBuilder3 = stringBuilder2;
      StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(12, 2, stringBuilder2);
      appendInterpolatedStringHandler.AppendLiteral("HTTP/1.1 ");
      appendInterpolatedStringHandler.AppendFormatted<int>(response.Status);
      appendInterpolatedStringHandler.AppendLiteral(" ");
      appendInterpolatedStringHandler.AppendFormatted(text2);
      appendInterpolatedStringHandler.AppendLiteral("\r\n");
      stringBuilder3.Append(ref appendInterpolatedStringHandler);
      stringBuilder.Append("Content-Type: application/json\r\n");
      stringBuilder2 = stringBuilder;
      StringBuilder stringBuilder4 = stringBuilder2;
      appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(18, 1, stringBuilder2);
      appendInterpolatedStringHandler.AppendLiteral("Content-Length: ");
      appendInterpolatedStringHandler.AppendFormatted<int>(response.Body.Length);
      appendInterpolatedStringHandler.AppendLiteral("\r\n");
      stringBuilder4.Append(ref appendInterpolatedStringHandler);
      foreach (KeyValuePair<string, string> keyValuePair in response.Headers)
      {
        string text3;
        keyValuePair.Deconstruct(out text, out text3);
        string text4 = text;
        string text5 = text3;
        stringBuilder2 = stringBuilder;
        StringBuilder stringBuilder5 = stringBuilder2;
        appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(4, 2, stringBuilder2);
        appendInterpolatedStringHandler.AppendFormatted(text4);
        appendInterpolatedStringHandler.AppendLiteral(": ");
        appendInterpolatedStringHandler.AppendFormatted(text5);
        appendInterpolatedStringHandler.AppendLiteral("\r\n");
        stringBuilder5.Append(ref appendInterpolatedStringHandler);
      }
      stringBuilder.Append("Cache-Control: no-store\r\n");
      stringBuilder.Append("Connection: close\r\n\r\n");
      await stream.WriteAsync(Encoding.UTF8.GetBytes(stringBuilder.ToString()), ct).ConfigureAwait(false);
      if (response.Body.Length != 0)
      {
        await stream.WriteAsync(response.Body, ct).ConfigureAwait(false);
      }
      await stream.FlushAsync(ct).ConfigureAwait(false);
    }

    // Token: 0x06001CAB RID: 7339 RVA: 0x003E46AC File Offset: 0x003E46AC
    [NullableContext(0)]
    private static int IndexOf([Nullable(1)] byte[] haystack, int length, ReadOnlySpan<byte> needle)
    {
      return haystack.AsSpan<byte>(0, length).IndexOf<byte>(needle);
    }

    // Token: 0x06001CAC RID: 7340 RVA: 0x003E46BC File Offset: 0x003E46BC
    public static string RandomToken()
    {
      return Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLowerInvariant();
    }

    // Token: 0x04000C48 RID: 3144
    private const int MaxHeaderBytes = 262144;

    // Token: 0x04000C49 RID: 3145
    private const int MaxBodyBytes = 4194304;

    // Token: 0x04000C4A RID: 3146
    private readonly Func<LoopbackHttpServer.Request, Task<LoopbackHttpServer.Response>> handler;

    // Token: 0x04000C4B RID: 3147
    private readonly object gate = new object();

    // Token: 0x04000C4C RID: 3148
    [Nullable(2)]
    private TcpListener listener;

    // Token: 0x04000C4D RID: 3149
    [Nullable(2)]
    private CancellationTokenSource stop;

    // Token: 0x04000C4E RID: 3150
    private int? port;

    // Token: 0x04000C51 RID: 3153
    private bool listening;

    // Token: 0x0200062C RID: 1580
    [Nullable(0)]
    public sealed class Request : IEquatable<LoopbackHttpServer.Request>
    {
      // Token: 0x060038A0 RID: 14496 RVA: 0x0045190C File Offset: 0x0045190C
      public Request(string Method, string Path, IReadOnlyDictionary<string, string> Headers, byte[] Body)
      {
        this.Method = Method;
        this.Path = Path;
        this.Headers = Headers;
        this.Body = Body;
        base..ctor();
      }

      // Token: 0x170009C7 RID: 2503
      // (get) Token: 0x060038A1 RID: 14497 RVA: 0x00451934 File Offset: 0x00451934
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(LoopbackHttpServer.Request);
        }
      }

      // Token: 0x170009C8 RID: 2504
      // (get) Token: 0x060038A2 RID: 14498 RVA: 0x00451940 File Offset: 0x00451940
      // (set) Token: 0x060038A3 RID: 14499 RVA: 0x00451948 File Offset: 0x00451948
      public string Method { get; set; }

      // Token: 0x170009C9 RID: 2505
      // (get) Token: 0x060038A4 RID: 14500 RVA: 0x00451954 File Offset: 0x00451954
      // (set) Token: 0x060038A5 RID: 14501 RVA: 0x0045195C File Offset: 0x0045195C
      public string Path { get; set; }

      // Token: 0x170009CA RID: 2506
      // (get) Token: 0x060038A6 RID: 14502 RVA: 0x00451968 File Offset: 0x00451968
      // (set) Token: 0x060038A7 RID: 14503 RVA: 0x00451970 File Offset: 0x00451970
      public IReadOnlyDictionary<string, string> Headers { get; set; }

      // Token: 0x170009CB RID: 2507
      // (get) Token: 0x060038A8 RID: 14504 RVA: 0x0045197C File Offset: 0x0045197C
      // (set) Token: 0x060038A9 RID: 14505 RVA: 0x00451984 File Offset: 0x00451984
      public byte[] Body { get; set; }

      // Token: 0x060038AA RID: 14506 RVA: 0x00451990 File Offset: 0x00451990
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Request");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060038AB RID: 14507 RVA: 0x004519DC File Offset: 0x004519DC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Method = ");
        builder.Append(this.Method);
        builder.Append(", Path = ");
        builder.Append(this.Path);
        builder.Append(", Headers = ");
        builder.Append(this.Headers);
        builder.Append(", Body = ");
        builder.Append(this.Body);
        return true;
      }

      // Token: 0x060038AC RID: 14508 RVA: 0x00451A54 File Offset: 0x00451A54
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(LoopbackHttpServer.Request left, LoopbackHttpServer.Request right)
      {
        return !(left == right);
      }

      // Token: 0x060038AD RID: 14509 RVA: 0x00451A60 File Offset: 0x00451A60
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(LoopbackHttpServer.Request left, LoopbackHttpServer.Request right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060038AE RID: 14510 RVA: 0x00451A74 File Offset: 0x00451A74
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Method>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<Headers>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Body>k__BackingField);
      }

      // Token: 0x060038AF RID: 14511 RVA: 0x00451AF0 File Offset: 0x00451AF0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as LoopbackHttpServer.Request);
      }

      // Token: 0x060038B0 RID: 14512 RVA: 0x00451B00 File Offset: 0x00451B00
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(LoopbackHttpServer.Request other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Method>k__BackingField, other.<Method>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<Headers>k__BackingField, other.<Headers>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField));
      }

      // Token: 0x060038B2 RID: 14514 RVA: 0x00451B94 File Offset: 0x00451B94
      [CompilerGenerated]
      private Request(LoopbackHttpServer.Request original)
      {
        this.Method = original.<Method>k__BackingField;
        this.Path = original.<Path>k__BackingField;
        this.Headers = original.<Headers>k__BackingField;
        this.Body = original.<Body>k__BackingField;
      }

      // Token: 0x060038B3 RID: 14515 RVA: 0x00451BCC File Offset: 0x00451BCC
      [CompilerGenerated]
      public void Deconstruct(out string Method, out string Path, out IReadOnlyDictionary<string, string> Headers, out byte[] Body)
      {
        Method = this.Method;
        Path = this.Path;
        Headers = this.Headers;
        Body = this.Body;
      }
    }

    // Token: 0x0200062D RID: 1581
    [Nullable(0)]
    public sealed class Response : IEquatable<LoopbackHttpServer.Response>
    {
      // Token: 0x060038B4 RID: 14516 RVA: 0x00451BF0 File Offset: 0x00451BF0
      public Response(int Status, IReadOnlyDictionary<string, string> Headers, byte[] Body)
      {
        this.Status = Status;
        this.Headers = Headers;
        this.Body = Body;
        base..ctor();
      }

      // Token: 0x170009CC RID: 2508
      // (get) Token: 0x060038B5 RID: 14517 RVA: 0x00451C10 File Offset: 0x00451C10
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(LoopbackHttpServer.Response);
        }
      }

      // Token: 0x170009CD RID: 2509
      // (get) Token: 0x060038B6 RID: 14518 RVA: 0x00451C1C File Offset: 0x00451C1C
      // (set) Token: 0x060038B7 RID: 14519 RVA: 0x00451C24 File Offset: 0x00451C24
      public int Status { get; set; }

      // Token: 0x170009CE RID: 2510
      // (get) Token: 0x060038B8 RID: 14520 RVA: 0x00451C30 File Offset: 0x00451C30
      // (set) Token: 0x060038B9 RID: 14521 RVA: 0x00451C38 File Offset: 0x00451C38
      public IReadOnlyDictionary<string, string> Headers { get; set; }

      // Token: 0x170009CF RID: 2511
      // (get) Token: 0x060038BA RID: 14522 RVA: 0x00451C44 File Offset: 0x00451C44
      // (set) Token: 0x060038BB RID: 14523 RVA: 0x00451C4C File Offset: 0x00451C4C
      public byte[] Body { get; set; }

      // Token: 0x060038BC RID: 14524 RVA: 0x00451C58 File Offset: 0x00451C58
      public static LoopbackHttpServer.Response Json(int status, [Nullable(2)] JsonObject body, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> headers = null)
      {
        return new LoopbackHttpServer.Response(status, headers ?? new Dictionary<string, string>(), (body == null) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(body.ToJsonString(null)));
      }

      // Token: 0x060038BD RID: 14525 RVA: 0x00451C88 File Offset: 0x00451C88
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Response");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060038BE RID: 14526 RVA: 0x00451CD4 File Offset: 0x00451CD4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Status = ");
        builder.Append(this.Status.ToString());
        builder.Append(", Headers = ");
        builder.Append(this.Headers);
        builder.Append(", Body = ");
        builder.Append(this.Body);
        return true;
      }

      // Token: 0x060038BF RID: 14527 RVA: 0x00451D40 File Offset: 0x00451D40
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(LoopbackHttpServer.Response left, LoopbackHttpServer.Response right)
      {
        return !(left == right);
      }

      // Token: 0x060038C0 RID: 14528 RVA: 0x00451D4C File Offset: 0x00451D4C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(LoopbackHttpServer.Response left, LoopbackHttpServer.Response right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060038C1 RID: 14529 RVA: 0x00451D60 File Offset: 0x00451D60
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<Headers>k__BackingField)) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Body>k__BackingField);
      }

      // Token: 0x060038C2 RID: 14530 RVA: 0x00451DC4 File Offset: 0x00451DC4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as LoopbackHttpServer.Response);
      }

      // Token: 0x060038C3 RID: 14531 RVA: 0x00451DD4 File Offset: 0x00451DD4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(LoopbackHttpServer.Response other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<Headers>k__BackingField, other.<Headers>k__BackingField) && EqualityComparer<byte[]>.Default.Equals(this.<Body>k__BackingField, other.<Body>k__BackingField));
      }

      // Token: 0x060038C5 RID: 14533 RVA: 0x00451E50 File Offset: 0x00451E50
      [CompilerGenerated]
      private Response(LoopbackHttpServer.Response original)
      {
        this.Status = original.<Status>k__BackingField;
        this.Headers = original.<Headers>k__BackingField;
        this.Body = original.<Body>k__BackingField;
      }

      // Token: 0x060038C6 RID: 14534 RVA: 0x00451E7C File Offset: 0x00451E7C
      [CompilerGenerated]
      public void Deconstruct(out int Status, out IReadOnlyDictionary<string, string> Headers, out byte[] Body)
      {
        Status = this.Status;
        Headers = this.Headers;
        Body = this.Body;
      }
    }
  }
}
