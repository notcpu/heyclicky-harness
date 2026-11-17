using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Documents.Markdown;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002D1 RID: 721
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerPreparedDocument
  {
    // Token: 0x17000569 RID: 1385
    // (get) Token: 0x06001E57 RID: 7767 RVA: 0x00606DC8 File Offset: 0x00606DC8
    public IReadOnlyList<MarkdownViewerBlock> Blocks { get; }

    // Token: 0x1700056A RID: 1386
    // (get) Token: 0x06001E58 RID: 7768 RVA: 0x00606DD0 File Offset: 0x00606DD0
    [Nullable(2)]
    public string BaseDirectory
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x1700056B RID: 1387
    // (get) Token: 0x06001E59 RID: 7769 RVA: 0x00606DD8 File Offset: 0x00606DD8
    public IReadOnlyDictionary<string, MarkdownViewerImage> Images { get; }

    // Token: 0x1700056C RID: 1388
    // (get) Token: 0x06001E5A RID: 7770 RVA: 0x00606DE0 File Offset: 0x00606DE0
    public IReadOnlyList<string> RemoteImageSourcesToLoad { get; }

    // Token: 0x06001E5B RID: 7771 RVA: 0x00606DE8 File Offset: 0x00606DE8
    private MarkdownViewerPreparedDocument(IReadOnlyList<MarkdownViewerBlock> blocks, [Nullable(2)] string baseDirectory, IReadOnlyDictionary<string, MarkdownViewerImage> images, IReadOnlyList<string> remote)
    {
      this.Blocks = blocks;
      this.BaseDirectory = baseDirectory;
      this.Images = images;
      this.RemoteImageSourcesToLoad = remote;
    }

    // Token: 0x06001E5C RID: 7772 RVA: 0x00606E10 File Offset: 0x00606E10
    private static HttpClient CreateHttp()
    {
      HttpClient httpClient = new HttpClient();
      httpClient.Timeout = TimeSpan.FromSeconds(30L);
      httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("HeyClicky/1.0 (Windows)");
      return httpClient;
    }

    // Token: 0x06001E5D RID: 7773 RVA: 0x00606E3C File Offset: 0x00606E3C
    public static bool IsRemote(string source)
    {
      return source.StartsWith("http://", StringComparison.Ordinal) || source.StartsWith("https://", StringComparison.Ordinal);
    }

    // Token: 0x06001E5E RID: 7774 RVA: 0x00606E5C File Offset: 0x00606E5C
    public static async Task<MarkdownViewerPreparedDocument> PrepareAsync(IReadOnlyList<MarkdownViewerBlock> blocks, [Nullable(2)] string baseDirectory, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, byte[]> remoteImagesBySource = null, CancellationToken ct = default(CancellationToken))
    {
      Dictionary<string, MarkdownViewerImage> images = new Dictionary<string, MarkdownViewerImage>();
      List<string> remote = new List<string>();
      foreach (MarkdownViewerBlock.Image image in MarkdownViewerPreparedDocument.ImageBlocks(blocks))
      {
        ct.ThrowIfCancellationRequested();
        if (!images.ContainsKey(image.Source))
        {
          if (MarkdownViewerPreparedDocument.IsRemote(image.Source))
          {
            byte[] bytes;
            bool flag = remoteImagesBySource != null && remoteImagesBySource.TryGetValue(image.Source, out bytes);
            ValueTuple<double, double> size;
            if (flag)
            {
              ValueTuple<double, double>? valueTuple = await MarkdownViewerPreparedDocument.MeasureAsync(bytes);
              bool flag2;
              if (valueTuple != null)
              {
                size = valueTuple.GetValueOrDefault();
                flag2 = true;
              }
              else
              {
                flag2 = false;
              }
              flag = flag2;
            }
            if (flag)
            {
              images[image.Source] = new MarkdownViewerImage(null, bytes, size.Item1, size.Item2);
            }
            else if (remoteImagesBySource == null || !remoteImagesBySource.ContainsKey(image.Source))
            {
              remote.Add(image.Source);
            }
            bytes = null;
          }
          else
          {
            MarkdownViewerImage markdownViewerImage = await MarkdownViewerPreparedDocument.LoadLocalImageAsync(image.Source, baseDirectory);
            if (markdownViewerImage != null)
            {
              images[image.Source] = markdownViewerImage;
            }
          }
          image = null;
        }
      }
      IEnumerator<MarkdownViewerBlock.Image> enumerator = null;
      return new MarkdownViewerPreparedDocument(blocks, baseDirectory, images, remote);
    }

    // Token: 0x06001E5F RID: 7775 RVA: 0x00606EB8 File Offset: 0x00606EB8
    public static async Task<Dictionary<string, byte[]>> DownloadAsync(IEnumerable<string> sources, CancellationToken ct)
    {
      Dictionary<string, byte[]> loaded = new Dictionary<string, byte[]>();
      foreach (string source in sources.Distinct<string>())
      {
        ct.ThrowIfCancellationRequested();
        try
        {
          Uri uri;
          if (!Uri.TryCreate(source, UriKind.Absolute, out uri))
          {
            continue;
          }
          byte[] array = await MarkdownViewerPreparedDocument.Http.GetByteArrayAsync(uri, ct);
          byte[] bytes = array;
          TaskAwaiter<ValueTuple<double, double>?> taskAwaiter = MarkdownViewerPreparedDocument.MeasureAsync(bytes).GetAwaiter();
          if (!taskAwaiter.IsCompleted)
          {
            await taskAwaiter;
            TaskAwaiter<ValueTuple<double, double>?> taskAwaiter2;
            taskAwaiter = taskAwaiter2;
            taskAwaiter2 = default(TaskAwaiter<ValueTuple<double, double>?>);
          }
          if (taskAwaiter.GetResult() != null)
          {
            loaded[source] = bytes;
          }
          bytes = null;
        }
        catch (OperationCanceledException obj) when (ct.IsCancellationRequested)
        {
          throw;
        }
        catch (Exception ex)
        {
          Log.Info("markdown", "image " + source + " not loaded: " + ex.Message);
        }
        source = null;
      }
      IEnumerator<string> enumerator = null;
      return loaded;
    }

    // Token: 0x06001E60 RID: 7776 RVA: 0x00606F04 File Offset: 0x00606F04
    private static IEnumerable<MarkdownViewerBlock.Image> ImageBlocks(IEnumerable<MarkdownViewerBlock> blocks)
    {
      MarkdownViewerPreparedDocument.<ImageBlocks>d__18 <ImageBlocks>d__ = new MarkdownViewerPreparedDocument.<ImageBlocks>d__18(-2);
      <ImageBlocks>d__.<>3__blocks = blocks;
      return <ImageBlocks>d__;
    }

    // Token: 0x06001E61 RID: 7777 RVA: 0x00606F14 File Offset: 0x00606F14
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<MarkdownViewerImage> LoadLocalImageAsync(string source, [Nullable(2)] string baseDirectory)
    {
      string text = source;
      Uri uri;
      if (text.StartsWith("file://", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(text, UriKind.Absolute, out uri))
      {
        text = uri.LocalPath;
      }
      List<string> list = new List<string> { text };
      string text2 = Uri.UnescapeDataString(text);
      if (text2 != text)
      {
        list.Add(text2);
      }
      foreach (string text3 in list)
      {
        string text4;
        if (!text3.StartsWith('~'))
        {
          text4 = text3;
        }
        else
        {
          string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
          string text5 = text3;
          text4 = Path.Combine(folderPath, text5.Substring(1, text5.Length - 1).TrimStart(new char[] { '/', '\\' }));
        }
        string text6 = text4;
        string resolved;
        if ((Path.IsPathRooted(text6) && !text6.StartsWith('\\')) || text6.StartsWith("\\\\", StringComparison.Ordinal))
        {
          resolved = text6;
        }
        else
        {
          if (baseDirectory == null)
          {
            continue;
          }
          resolved = Path.Combine(baseDirectory, text3.TrimStart(new char[] { '/', '\\' }));
        }
        try
        {
          if (!File.Exists(resolved))
          {
            continue;
          }
          ValueTuple<double, double>? valueTuple = await MarkdownViewerPreparedDocument.MeasureAsync(await File.ReadAllBytesAsync(resolved, default(CancellationToken)));
          if (valueTuple != null)
          {
            ValueTuple<double, double> valueOrDefault = valueTuple.GetValueOrDefault();
            return new MarkdownViewerImage(resolved, null, valueOrDefault.Item1, valueOrDefault.Item2);
          }
        }
        catch (Exception ex)
        {
          Log.Info("markdown", "image " + resolved + " not loaded: " + ex.Message);
        }
        resolved = null;
      }
      List<string>.Enumerator enumerator = default(List<string>.Enumerator);
      return null;
    }

    // Token: 0x06001E62 RID: 7778 RVA: 0x00606F60 File Offset: 0x00606F60
    [return: TupleElementNames(new string[] { "Width", "Height" })]
    [return: Nullable(new byte[] { 1, 0 })]
    private static async Task<ValueTuple<double, double>?> MeasureAsync(byte[] bytes)
    {
      ValueTuple<double, double>? valueTuple;
      try
      {
        using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
        {
          await stream.WriteAsync(bytes.AsBuffer());
          stream.Seek(0UL);
          BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(stream);
          double num = bitmapDecoder.PixelWidth;
          double num2 = bitmapDecoder.PixelHeight;
          double dpiX = bitmapDecoder.DpiX;
          if (dpiX > 1.0 && Math.Abs(dpiX - 72.0) > 1.0 && Math.Abs(dpiX - 96.0) > 1.0)
          {
            num = num * 72.0 / dpiX;
            num2 = num2 * 72.0 / bitmapDecoder.DpiY;
          }
          valueTuple = ((num > 0.0 && num2 > 0.0) ? new ValueTuple<double, double>?(new ValueTuple<double, double>(num, num2)) : null);
        }
      }
      catch
      {
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x04000EA3 RID: 3747
    private static readonly HttpClient Http = MarkdownViewerPreparedDocument.CreateHttp();
  }
}
