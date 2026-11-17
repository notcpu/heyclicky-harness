using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001D9 RID: 473
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LoopbackCallbackServer : IDisposable
  {
    // Token: 0x06001B34 RID: 6964 RVA: 0x003DA464 File Offset: 0x003DA464
    public LoopbackCallbackServer(int port, string path, string expectedState)
    {
      this.path = path;
      this.expectedState = expectedState;
      foreach (IPAddress ipaddress in new IPAddress[]
      {
        IPAddress.Loopback,
        IPAddress.IPv6Loopback
      })
      {
        try
        {
          TcpListener tcpListener = new TcpListener(ipaddress, port);
          tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, true);
          tcpListener.Start();
          this.listeners.Add(tcpListener);
        }
        catch (SocketException obj) when (ipaddress.Equals(IPAddress.IPv6Loopback))
        {
        }
        catch (SocketException)
        {
          this.Dispose();
          McpOAuthFailure mcpOAuthFailure = McpOAuthFailure.CallbackPortUnavailable;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(59, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Port ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(port);
          defaultInterpolatedStringHandler.AppendLiteral(" is in use, so the sign-in can't receive its callback.");
          throw new McpOAuthException(mcpOAuthFailure, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      foreach (TcpListener tcpListener2 in this.listeners)
      {
        this.AcceptLoopAsync(tcpListener2);
      }
    }

    // Token: 0x06001B35 RID: 6965 RVA: 0x003DA5C8 File Offset: 0x003DA5C8
    public async Task<Dictionary<string, string>> WaitAsync(TimeSpan timeout, CancellationToken ct)
    {
      Dictionary<string, string> dictionary;
      using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
      {
        cts.CancelAfter(timeout);
        try
        {
          dictionary = await this.result.Task.WaitAsync(cts.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException obj) when (!ct.IsCancellationRequested)
        {
          throw new McpOAuthException(McpOAuthFailure.TimedOut, "The sign-in wasn't completed in time.");
        }
      }
      return dictionary;
    }

    // Token: 0x06001B36 RID: 6966 RVA: 0x003DA61C File Offset: 0x003DA61C
    private async Task AcceptLoopAsync(TcpListener listener)
    {
      while (!this.stop.IsCancellationRequested)
      {
        TcpClient tcpClient;
        try
        {
          tcpClient = await listener.AcceptTcpClientAsync(this.stop.Token).ConfigureAwait(false);
        }
        catch
        {
          break;
        }
        this.HandleAsync(tcpClient);
      }
    }

    // Token: 0x06001B37 RID: 6967 RVA: 0x003DA668 File Offset: 0x003DA668
    private async Task HandleAsync(TcpClient client)
    {
      using (client)
      {
        try
        {
          NetworkStream stream = client.GetStream();
          byte[] buffer = new byte[16384];
          int total = 0;
          Dictionary<string, string> parameters;
          byte[] bytes;
          using (CancellationTokenSource read = CancellationTokenSource.CreateLinkedTokenSource(this.stop.Token))
          {
            read.CancelAfter(TimeSpan.FromSeconds(10L));
            while (total < buffer.Length)
            {
              int num = await stream.ReadAsync(buffer.AsMemory<byte>(total), read.Token).ConfigureAwait(false);
              if (num == 0)
              {
                break;
              }
              total += num;
              if (Encoding.ASCII.GetString(buffer, 0, total).Contains("\r\n\r\n", StringComparison.Ordinal))
              {
                break;
              }
            }
            string text = Encoding.ASCII.GetString(buffer, 0, total).Split("\r\n", 2, StringSplitOptions.None)[0].Split(' ', StringSplitOptions.None).ElementAtOrDefault<string>(1) ?? "/";
            Uri uri = new Uri("http://localhost" + (text.StartsWith('/') ? text : ("/" + text)));
            parameters = LoopbackCallbackServer.ParseQuery(uri.Query);
            bool isCallback = uri.AbsolutePath == this.path && parameters.GetValueOrDefault("state") == this.expectedState;
            string text2 = (isCallback ? (parameters.ContainsKey("error") ? "<h2>Sign-in didn't complete.</h2><p>You can close this window and try again in HeyClicky.</p>" : "<h2>Signed in.</h2><p>You can close this window and go back to HeyClicky.</p>") : "<h2>Not found.</h2>");
            string text3 = "<!doctype html><html><head><meta charset=\"utf-8\"><title>HeyClicky</title><style>body{font-family:'Segoe UI',system-ui;background:#111;color:#eee;display:flex;align-items:center;justify-content:center;height:100vh;margin:0;text-align:center}</style></head><body><div>" + text2 + "</div></body></html>";
            bytes = Encoding.UTF8.GetBytes(text3);
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(90, 2);
            defaultInterpolatedStringHandler.AppendLiteral("HTTP/1.1 ");
            defaultInterpolatedStringHandler.AppendFormatted(isCallback ? "200 OK" : "404 Not Found");
            defaultInterpolatedStringHandler.AppendLiteral("\r\nContent-Type: text/html; charset=utf-8\r\nContent-Length: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(bytes.Length);
            defaultInterpolatedStringHandler.AppendLiteral("\r\nConnection: close\r\n\r\n");
            string text4 = defaultInterpolatedStringHandler.ToStringAndClear();
            await stream.WriteAsync(Encoding.ASCII.GetBytes(text4), default(CancellationToken)).ConfigureAwait(false);
            await stream.WriteAsync(bytes, default(CancellationToken)).ConfigureAwait(false);
            await stream.FlushAsync().ConfigureAwait(false);
            if (isCallback)
            {
              this.result.TrySetResult(parameters);
            }
          }
          stream = null;
          buffer = null;
          CancellationTokenSource read = null;
          parameters = null;
          bytes = null;
        }
        catch
        {
        }
      }
      TcpClient tcpClient = null;
    }

    // Token: 0x06001B38 RID: 6968 RVA: 0x003DA6B4 File Offset: 0x003DA6B4
    public static Dictionary<string, string> ParseQuery(string query)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
      foreach (string text in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
      {
        int num = text.IndexOf('=');
        string text2 = Uri.UnescapeDataString(((num < 0) ? text : text.Substring(0, num)).Replace('+', ' '));
        string text4;
        if (num >= 0)
        {
          string text3 = text;
          int num2 = num + 1;
          text4 = Uri.UnescapeDataString(text3.Substring(num2, text3.Length - num2).Replace('+', ' '));
        }
        else
        {
          text4 = "";
        }
        string text5 = text4;
        dictionary[text2] = text5;
      }
      return dictionary;
    }

    // Token: 0x06001B39 RID: 6969 RVA: 0x003DA760 File Offset: 0x003DA760
    public void Dispose()
    {
      this.stop.Cancel();
      foreach (TcpListener tcpListener in this.listeners)
      {
        try
        {
          tcpListener.Stop();
        }
        catch
        {
        }
      }
      this.listeners.Clear();
      this.result.TrySetCanceled();
    }

    // Token: 0x04000B9F RID: 2975
    private readonly string path;

    // Token: 0x04000BA0 RID: 2976
    private readonly string expectedState;

    // Token: 0x04000BA1 RID: 2977
    private readonly List<TcpListener> listeners = new List<TcpListener>();

    // Token: 0x04000BA2 RID: 2978
    private readonly TaskCompletionSource<Dictionary<string, string>> result = new TaskCompletionSource<Dictionary<string, string>>(TaskCreationOptions.RunContinuationsAsynchronously);

    // Token: 0x04000BA3 RID: 2979
    private readonly CancellationTokenSource stop = new CancellationTokenSource();
  }
}
