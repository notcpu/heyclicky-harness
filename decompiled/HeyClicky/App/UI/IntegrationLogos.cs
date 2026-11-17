using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.Core.Auth;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000B1 RID: 177
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntegrationLogos
  {
    // Token: 0x0600073D RID: 1853 RVA: 0x00536658 File Offset: 0x00536658
    [GeneratedRegex("[^a-z0-9]+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex NonAlnum()
    {
      return <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NonAlnum_0.Instance;
    }

    // Token: 0x0600073E RID: 1854 RVA: 0x00536660 File Offset: 0x00536660
    private static string Normalized([Nullable(2)] string value)
    {
      return IntegrationLogos.NonAlnum().Replace((value ?? "").Trim().ToLowerInvariant(), " ").Trim();
    }

    // Token: 0x0600073F RID: 1855 RVA: 0x0053668C File Offset: 0x0053668C
    [NullableContext(2)]
    public static string AssetFor([Nullable(new byte[] { 1, 2 })] params string[] identifiers)
    {
      for (int i = 0; i < identifiers.Length; i++)
      {
        string text = IntegrationLogos.Normalized(identifiers[i]);
        if (text.Length != 0)
        {
          string text2 = text.Replace(" ", "");
          if (text2 == "x")
          {
            return "x-logo";
          }
          foreach (ValueTuple<string, string> valueTuple in IntegrationLogos.Map)
          {
            string item = valueTuple.Item1;
            string item2 = valueTuple.Item2;
            if (text2 == item || (item == "google" && text2.StartsWith(item, StringComparison.Ordinal)))
            {
              return item2;
            }
          }
        }
      }
      return null;
    }

    // Token: 0x06000740 RID: 1856 RVA: 0x00536740 File Offset: 0x00536740
    private static string PathFor(string asset)
    {
      return Path.Combine(AppContext.BaseDirectory, "Assets", "Logos", asset + (IntegrationLogos.PngAssets.Contains(asset) ? ".png" : ".svg"));
    }

    // Token: 0x06000741 RID: 1857 RVA: 0x00536778 File Offset: 0x00536778
    public static FrameworkElement Image(string asset, double size, Color? templateTint = null, double scale = 2.0)
    {
      int num = (int)Math.Ceiling(size * scale);
      ImageSource imageSource;
      if (IntegrationLogos.PngAssets.Contains(asset))
      {
        imageSource = new BitmapImage(new Uri(IntegrationLogos.PathFor(asset)))
        {
          DecodePixelWidth = Math.Max(num, 16)
        };
      }
      else
      {
        if (templateTint != null)
        {
          Color valueOrDefault = templateTint.GetValueOrDefault();
          if (IntegrationLogos.TemplateAssets.Contains(asset))
          {
            SvgImageSource svgImageSource = new SvgImageSource();
            svgImageSource.RasterizePixelWidth = (double)num;
            svgImageSource.RasterizePixelHeight = (double)num;
            IntegrationLogos.LoadTintedAsync(svgImageSource, asset, valueOrDefault);
            imageSource = svgImageSource;
            goto IL_0098;
          }
        }
        imageSource = new SvgImageSource(new Uri(IntegrationLogos.PathFor(asset)))
        {
          RasterizePixelWidth = (double)num,
          RasterizePixelHeight = (double)num
        };
      }
      IL_0098:
      return new Image
      {
        Source = imageSource,
        Width = size,
        Height = size,
        Stretch = Stretch.Uniform
      };
    }

    // Token: 0x06000742 RID: 1858 RVA: 0x00536840 File Offset: 0x00536840
    private static async Task LoadTintedAsync(SvgImageSource svg, string asset, Color tint)
    {
      try
      {
        string text = await File.ReadAllTextAsync(IntegrationLogos.PathFor(asset), default(CancellationToken));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
        defaultInterpolatedStringHandler.AppendLiteral("#");
        defaultInterpolatedStringHandler.AppendFormatted<byte>(tint.R, "X2");
        defaultInterpolatedStringHandler.AppendFormatted<byte>(tint.G, "X2");
        defaultInterpolatedStringHandler.AppendFormatted<byte>(tint.B, "X2");
        string text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        text = Regex.Replace(text, "fill=\"(?!none)[^\"]*\"", "fill=\"" + text2 + "\"");
        text = Regex.Replace(text, "<svg ", "<svg fill=\"" + text2 + "\" ", RegexOptions.None, TimeSpan.FromSeconds(1L));
        using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(text)))
        {
          await svg.SetSourceAsync(stream.AsRandomAccessStream());
        }
        MemoryStream stream = null;
      }
      catch (Exception ex)
      {
        Log.Warn("logos", asset + ": " + ex.Message);
      }
    }

    // Token: 0x06000743 RID: 1859 RVA: 0x00536894 File Offset: 0x00536894
    [NullableContext(2)]
    [return: Nullable(1)]
    public static FrameworkElement Artwork(string toolkitSlug, string displayName, double size, double fallbackIconSize, string logoUrl = null)
    {
      string text = IntegrationLogos.AssetFor(new string[] { toolkitSlug, displayName });
      if (text != null)
      {
        return IntegrationLogos.Image(text, size, null, 2.0);
      }
      if (logoUrl != null && logoUrl.Length > 0)
      {
        return IntegrationLogos.Remote(logoUrl, size, IntegrationLogos.Artwork(null, null, size, fallbackIconSize, null));
      }
      TextBlock textBlock = Icons.Glyph("\uf0e2", fallbackIconSize, DS.Colors.TextSecondary);
      textBlock.FontWeight = FontWeights.SemiBold;
      return new Grid
      {
        Width = size,
        Height = size,
        Children = { textBlock }
      };
    }

    // Token: 0x1700012A RID: 298
    // (get) Token: 0x06000744 RID: 1860 RVA: 0x00536930 File Offset: 0x00536930
    private static string CacheDirectory
    {
      get
      {
        return AppPaths.Combine(new string[] { "logo-cache" });
      }
    }

    // Token: 0x06000745 RID: 1861 RVA: 0x00536948 File Offset: 0x00536948
    public static FrameworkElement Remote(string url, double size, FrameworkElement fallback)
    {
      IntegrationLogos.<>c__DisplayClass14_0 CS$<>8__locals1 = new IntegrationLogos.<>c__DisplayClass14_0();
      CS$<>8__locals1.size = size;
      CS$<>8__locals1.fallback = fallback;
      CS$<>8__locals1.host = new Grid
      {
        Width = CS$<>8__locals1.size,
        Height = CS$<>8__locals1.size
      };
      CS$<>8__locals1.host.Children.Add(CS$<>8__locals1.fallback);
      bool flag = !Uri.TryCreate(url, UriKind.Absolute, out CS$<>8__locals1.uri);
      if (!flag)
      {
        string scheme = CS$<>8__locals1.uri.Scheme;
        bool flag2 = scheme == "https" || scheme == "http";
        flag = !flag2;
      }
      if (flag)
      {
        return CS$<>8__locals1.host;
      }
      CS$<>8__locals1.dq = DispatcherQueue.GetForCurrentThread();
      Task<string> orAdd = IntegrationLogos.remoteLoads.GetOrAdd(CS$<>8__locals1.uri.AbsoluteUri, delegate(string _)
      {
        Func<Task<string>> func;
        if ((func = CS$<>8__locals1.<>9__3) == null)
        {
          func = (CS$<>8__locals1.<>9__3 = () => IntegrationLogos.CachedPathAsync(CS$<>8__locals1.uri));
        }
        return Task.Run<string>(func);
      });
      if (orAdd.IsCompletedSuccessfully)
      {
        CS$<>8__locals1.<Remote>g__Show|2(orAdd.Result);
      }
      else
      {
        orAdd.ContinueWith<bool?>(delegate([Nullable(new byte[] { 1, 2 })] Task<string> t)
        {
          DispatcherQueue dq = CS$<>8__locals1.dq;
          if (dq == null)
          {
            return null;
          }
          return new bool?(dq.TryEnqueue(delegate
          {
            CS$<>8__locals1.<Remote>g__Show|2(t.IsCompletedSuccessfully ? t.Result : null);
          }));
        }, TaskScheduler.Default);
      }
      return CS$<>8__locals1.host;
    }

    // Token: 0x06000746 RID: 1862 RVA: 0x00536A58 File Offset: 0x00536A58
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<string> CachedPathAsync(Uri uri)
    {
      string key = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(uri.AbsoluteUri))).Substring(0, 24).ToLowerInvariant();
      string text4;
      try
      {
        if (Directory.Exists(IntegrationLogos.CacheDirectory))
        {
          foreach (string text in new string[] { ".svg", ".png", ".jpg", ".gif", ".webp", ".ico" })
          {
            string text2 = Path.Combine(IntegrationLogos.CacheDirectory, key + text);
            if (File.Exists(text2) && new FileInfo(text2).Length > 0L)
            {
              return text2;
            }
          }
        }
        HttpResponseMessage httpResponseMessage = await IntegrationLogos.RemoteHttp.GetAsync(uri).ConfigureAwait(false);
        using (HttpResponseMessage response = httpResponseMessage)
        {
          if (!response.IsSuccessStatusCode)
          {
            string text3 = "logos";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 3);
            defaultInterpolatedStringHandler.AppendFormatted(uri.Host);
            defaultInterpolatedStringHandler.AppendFormatted(uri.AbsolutePath);
            defaultInterpolatedStringHandler.AppendLiteral(": HTTP ");
            defaultInterpolatedStringHandler.AppendFormatted<int>((int)response.StatusCode);
            Log.Warn(text3, defaultInterpolatedStringHandler.ToStringAndClear());
            text4 = null;
          }
          else
          {
            byte[] array2 = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            if (array2.Length == 0 || array2.Length > 2000000)
            {
              text4 = null;
            }
            else
            {
              byte[] array3 = array2;
              MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
              string text5 = IntegrationLogos.SniffExtension(array3, (contentType != null) ? contentType.MediaType : null);
              if (text5 == null)
              {
                Log.Warn("logos", uri.Host + uri.AbsolutePath + ": not an image");
                text4 = null;
              }
              else
              {
                Directory.CreateDirectory(IntegrationLogos.CacheDirectory);
                string text6 = Path.Combine(IntegrationLogos.CacheDirectory, key + text5);
                AtomicFile.WriteAllBytes(text6, array2);
                string text7 = "logos";
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 4);
                defaultInterpolatedStringHandler.AppendLiteral("cached ");
                defaultInterpolatedStringHandler.AppendFormatted(uri.Host);
                defaultInterpolatedStringHandler.AppendFormatted(uri.AbsolutePath);
                defaultInterpolatedStringHandler.AppendLiteral(" (");
                defaultInterpolatedStringHandler.AppendFormatted<int>(array2.Length);
                defaultInterpolatedStringHandler.AppendLiteral(" bytes, ");
                defaultInterpolatedStringHandler.AppendFormatted(text5);
                defaultInterpolatedStringHandler.AppendLiteral(")");
                Log.Info(text7, defaultInterpolatedStringHandler.ToStringAndClear());
                text4 = text6;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("logos", uri.Host + uri.AbsolutePath + ": " + ex.Message);
        text4 = null;
      }
      return text4;
    }

    // Token: 0x06000747 RID: 1863 RVA: 0x00536A9C File Offset: 0x00536A9C
    [NullableContext(2)]
    internal static string SniffExtension([Nullable(1)] byte[] b, string mediaType)
    {
      if (b.Length >= 8 && b[0] == 137 && b[1] == 80 && b[2] == 78 && b[3] == 71)
      {
        return ".png";
      }
      if (b.Length >= 3 && b[0] == 255 && b[1] == 216 && b[2] == 255)
      {
        return ".jpg";
      }
      if (b.Length >= 4 && b[0] == 71 && b[1] == 73 && b[2] == 70)
      {
        return ".gif";
      }
      if (b.Length >= 12 && b[0] == 82 && b[1] == 73 && b[2] == 70 && b[3] == 70 && b[8] == 87 && b[9] == 69 && b[10] == 66 && b[11] == 80)
      {
        return ".webp";
      }
      if (b.Length >= 4 && b[0] == 0 && b[1] == 0 && b[2] == 1 && b[3] == 0)
      {
        return ".ico";
      }
      string text = Encoding.UTF8.GetString(b, 0, Math.Min(b.Length, 512)).TrimStart(new char[] { '\ufeff', ' ', '\r', '\n', '\t' });
      if (text.StartsWith("<svg", StringComparison.OrdinalIgnoreCase) || (text.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase) && text.Contains("<svg", StringComparison.OrdinalIgnoreCase)) || mediaType == "image/svg+xml")
      {
        return ".svg";
      }
      return null;
    }

    // Token: 0x04000228 RID: 552
    [TupleElementNames(new string[] { "Key", "Asset" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private static readonly ValueTuple<string, string>[] Map = new ValueTuple<string, string>[]
    {
      new ValueTuple<string, string>("gmail", "gmail-logo"),
      new ValueTuple<string, string>("googlemeet", "google-meet-logo"),
      new ValueTuple<string, string>("meet", "google-meet-logo"),
      new ValueTuple<string, string>("google", "google-logo"),
      new ValueTuple<string, string>("github", "github-logo"),
      new ValueTuple<string, string>("notion", "notion-logo"),
      new ValueTuple<string, string>("linear", "linear-logo"),
      new ValueTuple<string, string>("discord", "discord-logo"),
      new ValueTuple<string, string>("airtable", "airtable-logo"),
      new ValueTuple<string, string>("spotify", "spotify-logo"),
      new ValueTuple<string, string>("zoom", "zoom-logo"),
      new ValueTuple<string, string>("microsoftteams", "ms-teams-logo"),
      new ValueTuple<string, string>("msteams", "ms-teams-logo"),
      new ValueTuple<string, string>("teams", "ms-teams-logo"),
      new ValueTuple<string, string>("whatsapp", "whatsapp-logo"),
      new ValueTuple<string, string>("imessage", "imessage-logo"),
      new ValueTuple<string, string>("obsidian", "obsidian-logo"),
      new ValueTuple<string, string>("excalidraw", "excalidraw-logo"),
      new ValueTuple<string, string>("powerpoint", "powerpoint-logo"),
      new ValueTuple<string, string>("word", "word-logo"),
      new ValueTuple<string, string>("reddit", "reddit-logo"),
      new ValueTuple<string, string>("twitter", "x-logo"),
      new ValueTuple<string, string>("youtube", "youtube-logo"),
      new ValueTuple<string, string>("linkedin", "linkedin-logo"),
      new ValueTuple<string, string>("instagram", "instagram-logo"),
      new ValueTuple<string, string>("tiktok", "tiktok-logo"),
      new ValueTuple<string, string>("polymarket", "polymarket-logo"),
      new ValueTuple<string, string>("applenotes", "apple-notes-logo"),
      new ValueTuple<string, string>("applereminders", "apple-reminders-logo"),
      new ValueTuple<string, string>("reminders", "apple-reminders-logo"),
      new ValueTuple<string, string>("findmy", "findmy-logo"),
      new ValueTuple<string, string>("chatgpt", "chatgpt-logo"),
      new ValueTuple<string, string>("openai", "openai-logo"),
      new ValueTuple<string, string>("claudecode", "claude-code-logo"),
      new ValueTuple<string, string>("claude", "claude-logo"),
      new ValueTuple<string, string>("heyclicky", "heyclicky-logo")
    };

    // Token: 0x04000229 RID: 553
    private static readonly HashSet<string> TemplateAssets = new HashSet<string> { "chatgpt-logo", "openai-logo", "github-logo", "discord-logo" };

    // Token: 0x0400022A RID: 554
    private static readonly HashSet<string> PngAssets = new HashSet<string> { "gmail-logo", "google-meet-logo", "ms-teams-logo", "whatsapp-logo", "zoom-logo", "findmy-logo", "apple-reminders-logo", "heyclicky-logo" };

    // Token: 0x0400022B RID: 555
    private static readonly HttpClient RemoteHttp = new HttpClient
    {
      Timeout = TimeSpan.FromSeconds(15L)
    };

    // Token: 0x0400022C RID: 556
    [Nullable(new byte[] { 1, 1, 1, 2 })]
    private static readonly ConcurrentDictionary<string, Task<string>> remoteLoads = new ConcurrentDictionary<string, Task<string>>();
  }
}
