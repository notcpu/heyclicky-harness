using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000280 RID: 640
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkImages
  {
    // Token: 0x170004A2 RID: 1186
    // (get) Token: 0x06001AFE RID: 6910 RVA: 0x005E9D18 File Offset: 0x005E9D18
    // (set) Token: 0x06001AFF RID: 6911 RVA: 0x005E9D20 File Offset: 0x005E9D20
    [Nullable(new byte[] { 1, 1, 2 })]
    public static Func<string, byte[]> Loader
    {
      [return: Nullable(new byte[] { 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 1, 2 })]
      set;
    } = delegate(string source)
    {
      if (!Path.IsPathRooted(source) && !source.StartsWith('~'))
      {
        return null;
      }
      string text = (source.StartsWith('~') ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), source.Substring(1, source.Length - 1).TrimStart(new char[] { '/', '\\' })) : source);
      byte[] array;
      try
      {
        array = (File.Exists(text) ? File.ReadAllBytes(text) : null);
      }
      catch (Exception)
      {
        array = null;
      }
      return array;
    };

    // Token: 0x170004A3 RID: 1187
    // (get) Token: 0x06001B00 RID: 6912 RVA: 0x005E9D28 File Offset: 0x005E9D28
    // (set) Token: 0x06001B01 RID: 6913 RVA: 0x005E9D30 File Offset: 0x005E9D30
    [Nullable(new byte[] { 2, 1, 1, 2 })]
    public static Func<string, CancellationToken, Task<byte[]>> PhotoSearch
    {
      [return: Nullable(new byte[] { 2, 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1, 2 })]
      set;
    }

    // Token: 0x170004A4 RID: 1188
    // (get) Token: 0x06001B02 RID: 6914 RVA: 0x005E9D38 File Offset: 0x005E9D38
    // (set) Token: 0x06001B03 RID: 6915 RVA: 0x005E9D40 File Offset: 0x005E9D40
    public static string WorkerBaseUrl { get; set; } = "https://api.heyclicky.com";

    // Token: 0x06001B04 RID: 6916 RVA: 0x005E9D48 File Offset: 0x005E9D48
    private static HttpClient CreateHttp()
    {
      HttpClient httpClient = new HttpClient();
      httpClient.Timeout = TimeSpan.FromSeconds(20L);
      httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0) HeyClicky");
      return httpClient;
    }

    // Token: 0x06001B05 RID: 6917 RVA: 0x005E9D74 File Offset: 0x005E9D74
    [NullableContext(2)]
    public static ClickyInkPicture Image(string source)
    {
      if (string.IsNullOrEmpty(source))
      {
        return null;
      }
      Dictionary<string, ClickyInkPicture> cache = ClickyInkImages.Cache;
      ClickyInkPicture clickyInkPicture;
      lock (cache)
      {
        ClickyInkPicture clickyInkPicture2;
        clickyInkPicture = (ClickyInkImages.Cache.TryGetValue(source, out clickyInkPicture2) ? clickyInkPicture2 : null);
      }
      return clickyInkPicture;
    }

    // Token: 0x06001B06 RID: 6918 RVA: 0x005E9DCC File Offset: 0x005E9DCC
    public static void Remember(ClickyInkPicture picture, string source)
    {
      Dictionary<string, ClickyInkPicture> cache = ClickyInkImages.Cache;
      lock (cache)
      {
        if (ClickyInkImages.Cache.Count >= 48)
        {
          ClickyInkImages.Cache.Clear();
        }
        ClickyInkImages.Cache[source] = picture;
      }
    }

    // Token: 0x06001B07 RID: 6919 RVA: 0x005E9E2C File Offset: 0x005E9E2C
    [NullableContext(2)]
    [return: Nullable(new byte[] { 1, 2 })]
    public static Task<ClickyInkPicture> LoadAsync(string source)
    {
      if (string.IsNullOrEmpty(source))
      {
        return Task.FromResult<ClickyInkPicture>(null);
      }
      ClickyInkPicture clickyInkPicture = ClickyInkImages.Image(source);
      if (clickyInkPicture != null)
      {
        return Task.FromResult<ClickyInkPicture>(clickyInkPicture);
      }
      if (ClickyInkImages.Failed.Contains(source))
      {
        return Task.FromResult<ClickyInkPicture>(null);
      }
      Task<ClickyInkPicture> task;
      if (ClickyInkImages.Loads.TryGetValue(source, out task))
      {
        return task;
      }
      DispatcherQueue dispatcher = DispatcherQueue.GetForCurrentThread();
      Task<ClickyInkPicture> task2 = ClickyInkImages.LoadUncachedAsync(source, dispatcher);
      ClickyInkImages.Loads[source] = task2;
      task2.ContinueWith(delegate([Nullable(new byte[] { 1, 2 })] Task<ClickyInkPicture> t)
      {
        dispatcher.TryEnqueue(delegate
        {
          ClickyInkImages.Loads.Remove(source);
          if (!t.IsCompletedSuccessfully || t.Result == null)
          {
            ClickyInkImages.Failed.Add(source);
          }
        });
      }, TaskScheduler.Default);
      return task2;
    }

    // Token: 0x06001B08 RID: 6920 RVA: 0x005E9EE8 File Offset: 0x005E9EE8
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<ClickyInkPicture> LoadUncachedAsync(string source, DispatcherQueue dispatcher)
    {
      ClickyInkImages.<>c__DisplayClass22_0 CS$<>8__locals1 = new ClickyInkImages.<>c__DisplayClass22_0();
      CS$<>8__locals1.source = source;
      byte[] array = await Task.Run<byte[]>(delegate
      {
        ClickyInkImages.<>c__DisplayClass22_0.<<LoadUncachedAsync>b__1>d <<LoadUncachedAsync>b__1>d;
        <<LoadUncachedAsync>b__1>d.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
        <<LoadUncachedAsync>b__1>d.<>4__this = CS$<>8__locals1;
        <<LoadUncachedAsync>b__1>d.<>1__state = -1;
        <<LoadUncachedAsync>b__1>d.<>t__builder.Start<ClickyInkImages.<>c__DisplayClass22_0.<<LoadUncachedAsync>b__1>d>(ref <<LoadUncachedAsync>b__1>d);
        return <<LoadUncachedAsync>b__1>d.<>t__builder.Task;
      });
      CS$<>8__locals1.bytes = array;
      ClickyInkPicture clickyInkPicture;
      if (CS$<>8__locals1.bytes == null || CS$<>8__locals1.bytes.Length == 0)
      {
        clickyInkPicture = null;
      }
      else
      {
        ValueTuple<byte[], byte[], int, int>? valueTuple = await Task.Run<ValueTuple<byte[], byte[], int, int>?>(() => ClickyInkImages.Decode(CS$<>8__locals1.bytes));
        if (valueTuple == null)
        {
          clickyInkPicture = null;
        }
        else
        {
          ValueTuple<byte[], byte[], int, int> value = valueTuple.Value;
          CS$<>8__locals1.pixels = value.Item1;
          CS$<>8__locals1.gray = value.Item2;
          CS$<>8__locals1.width = value.Item3;
          CS$<>8__locals1.height = value.Item4;
          CS$<>8__locals1.done = new TaskCompletionSource<ClickyInkPicture>();
          if (!dispatcher.TryEnqueue(delegate
          {
            try
            {
              ClickyInkPicture clickyInkPicture2 = new ClickyInkPicture(ClickyInkImages.Bitmap(CS$<>8__locals1.pixels, CS$<>8__locals1.width, CS$<>8__locals1.height), ClickyInkImages.Bitmap(CS$<>8__locals1.gray, CS$<>8__locals1.width, CS$<>8__locals1.height), CS$<>8__locals1.width, CS$<>8__locals1.height);
              ClickyInkImages.Remember(clickyInkPicture2, CS$<>8__locals1.source);
              CS$<>8__locals1.done.SetResult(clickyInkPicture2);
            }
            catch (Exception ex)
            {
              CS$<>8__locals1.done.SetException(ex);
            }
          }))
          {
            clickyInkPicture = null;
          }
          else
          {
            clickyInkPicture = await CS$<>8__locals1.done.Task;
          }
        }
      }
      return clickyInkPicture;
    }

    // Token: 0x06001B09 RID: 6921 RVA: 0x005E9F34 File Offset: 0x005E9F34
    private static WriteableBitmap Bitmap(byte[] pixels, int width, int height)
    {
      WriteableBitmap writeableBitmap = new WriteableBitmap(width, height);
      using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
      {
        stream.Write(pixels, 0, pixels.Length);
      }
      writeableBitmap.Invalidate();
      return writeableBitmap;
    }

    // Token: 0x06001B0A RID: 6922 RVA: 0x005E9F84 File Offset: 0x005E9F84
    [return: TupleElementNames(new string[] { "Pixels", "Gray", "Width", "Height" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<byte[], byte[], int, int>? Decode(byte[] bytes)
    {
      ValueTuple<byte[], byte[], int, int>? valueTuple;
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        using (IRandomAccessStream randomAccessStream = new MemoryStream(bytes).AsRandomAccessStream())
        {
          using (CanvasBitmap result = CanvasBitmap.LoadAsync(sharedDevice, randomAccessStream, 96f).AsTask<CanvasBitmap>().GetAwaiter()
            .GetResult())
          {
            BitmapSize sizeInPixels = result.SizeInPixels;
            double num = Math.Min(1.0, 1200.0 / Math.Max(sizeInPixels.Width, sizeInPixels.Height));
            int num2 = Math.Max(1, (int)Math.Round(sizeInPixels.Width * num));
            int num3 = Math.Max(1, (int)Math.Round(sizeInPixels.Height * num));
            using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)num2, (float)num3, 96f))
            {
              using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
              {
                canvasDrawingSession.Clear(Colors.Transparent);
                canvasDrawingSession.DrawImage(result, new Rect(0f, 0f, (float)num2, (float)num3), new Rect(0f, 0f, sizeInPixels.Width, sizeInPixels.Height), 1f, CanvasImageInterpolation.HighQualityCubic);
              }
              byte[] pixelBytes = canvasRenderTarget.GetPixelBytes();
              byte[] array = new byte[pixelBytes.Length];
              int num4 = 0;
              while (num4 + 3 < pixelBytes.Length)
              {
                byte b = (byte)Math.Round(0.0722 * (double)pixelBytes[num4] + 0.7152 * (double)pixelBytes[num4 + 1] + 0.2126 * (double)pixelBytes[num4 + 2]);
                array[num4] = (array[num4 + 1] = (array[num4 + 2] = b));
                array[num4 + 3] = pixelBytes[num4 + 3];
                num4 += 4;
              }
              valueTuple = new ValueTuple<byte[], byte[], int, int>?(new ValueTuple<byte[], byte[], int, int>(pixelBytes, array, num2, num3));
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "picture decode: " + ex.Message);
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x04000CE6 RID: 3302
    public const int MaximumCachedImages = 48;

    // Token: 0x04000CE7 RID: 3303
    private const int MaximumPixelSide = 1200;

    // Token: 0x04000CEB RID: 3307
    private static readonly HttpClient Http = ClickyInkImages.CreateHttp();

    // Token: 0x04000CEC RID: 3308
    private static readonly Dictionary<string, ClickyInkPicture> Cache = new Dictionary<string, ClickyInkPicture>();

    // Token: 0x04000CED RID: 3309
    [Nullable(new byte[] { 1, 1, 1, 2 })]
    private static readonly Dictionary<string, Task<ClickyInkPicture>> Loads = new Dictionary<string, Task<ClickyInkPicture>>();

    // Token: 0x04000CEE RID: 3310
    private static readonly HashSet<string> Failed = new HashSet<string>();
  }
}
