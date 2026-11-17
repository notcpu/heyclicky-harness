using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000084 RID: 132
  [NullableContext(1)]
  [Nullable(0)]
  public static class ScreenSettle
  {
    // Token: 0x060007FA RID: 2042 RVA: 0x00367DE0 File Offset: 0x00367DE0
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<byte[]> GraySampleAsync(byte[] jpeg)
    {
      byte[] array3;
      try
      {
        using (InMemoryRandomAccessStream input = new InMemoryRandomAccessStream())
        {
          await input.WriteAsync(jpeg.AsBuffer());
          input.Seek(0UL);
          BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(input);
          int width = Math.Min(192, (int)bitmapDecoder.PixelWidth);
          int height = Math.Max(1, (int)Math.Round(bitmapDecoder.PixelHeight * (double)width / Math.Max(1U, bitmapDecoder.PixelWidth)));
          BitmapTransform bitmapTransform = new BitmapTransform
          {
            ScaledWidth = (uint)width,
            ScaledHeight = (uint)height,
            InterpolationMode = BitmapInterpolationMode.Linear
          };
          byte[] array = (await bitmapDecoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, bitmapTransform, ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage)).DetachPixelData();
          byte[] array2 = new byte[width * height];
          for (int i = 0; i < array2.Length; i++)
          {
            array2[i] = (byte)(array[i * 4] * 29 + array[i * 4 + 1] * 150 + array[i * 4 + 2] * 77 >> 8);
          }
          array3 = array2;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ScreenSettle", "screenshot not sampled: " + ex.Message);
        array3 = null;
      }
      return array3;
    }

    // Token: 0x060007FB RID: 2043 RVA: 0x00367E24 File Offset: 0x00367E24
    public static double ChangedFraction(byte[] first, byte[] second)
    {
      if (first.Length != second.Length || first.Length == 0)
      {
        return 1.0;
      }
      int num = 0;
      for (int i = 0; i < first.Length; i++)
      {
        if (Math.Abs((int)(first[i] - second[i])) > 12)
        {
          num++;
        }
      }
      return (double)num / (double)first.Length;
    }

    // Token: 0x060007FC RID: 2044 RVA: 0x00367E74 File Offset: 0x00367E74
    public static async Task<bool> LookTheSameAsync(byte[] firstJpeg, byte[] secondJpeg, double tolerance)
    {
      byte[] array = await ScreenSettle.GraySampleAsync(firstJpeg).ConfigureAwait(false);
      byte[] first = array;
      byte[] array2 = await ScreenSettle.GraySampleAsync(secondJpeg).ConfigureAwait(false);
      return first != null && array2 != null && ScreenSettle.ChangedFraction(first, array2) <= tolerance;
    }

    // Token: 0x060007FD RID: 2045 RVA: 0x00367EC8 File Offset: 0x00367EC8
    public static async Task WaitForScreenToSettleAsync([Nullable(new byte[] { 1, 1, 2 })] Func<Task<byte[]>> sample, TimeSpan fallback, CancellationToken ct)
    {
      ScreenSettle.<>c__DisplayClass10_0 CS$<>8__locals1;
      CS$<>8__locals1.started = Environment.TickCount64;
      byte[] array = await ScreenSettle.SampleAsync(sample).ConfigureAwait(false);
      byte[] clickSample = array;
      if (clickSample == null)
      {
        await Task.Delay(fallback, ct).ConfigureAwait(false);
      }
      else
      {
        await Task.Delay(250, ct).ConfigureAwait(false);
        byte[] previous = clickSample;
        bool changedSinceClick = false;
        int calm = 0;
        int samples = 0;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        while (ScreenSettle.<WaitForScreenToSettleAsync>g__Elapsed|10_0(ref CS$<>8__locals1) < 4.0)
        {
          ct.ThrowIfCancellationRequested();
          byte[] array2 = await ScreenSettle.SampleAsync(sample).ConfigureAwait(false);
          if (array2 == null)
          {
            double num = fallback.TotalSeconds - ScreenSettle.<WaitForScreenToSettleAsync>g__Elapsed|10_0(ref CS$<>8__locals1);
            if (num > 0.0)
            {
              await Task.Delay(TimeSpan.FromSeconds(num), ct).ConfigureAwait(false);
            }
            return;
          }
          samples++;
          if (ScreenSettle.ChangedFraction(clickSample, array2) > 0.005)
          {
            changedSinceClick = true;
          }
          calm = ((ScreenSettle.ChangedFraction(previous, array2) <= 0.005) ? (calm + 1) : 0);
          previous = array2;
          if (calm >= 2 && (changedSinceClick || ScreenSettle.<WaitForScreenToSettleAsync>g__Elapsed|10_0(ref CS$<>8__locals1) >= 1.5))
          {
            string text = "ScreenSettle";
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 3);
            defaultInterpolatedStringHandler.AppendLiteral("screen settled after ");
            defaultInterpolatedStringHandler.AppendFormatted<double>(ScreenSettle.<WaitForScreenToSettleAsync>g__Elapsed|10_0(ref CS$<>8__locals1) * 1000.0, "0");
            defaultInterpolatedStringHandler.AppendLiteral(" ms (");
            defaultInterpolatedStringHandler.AppendFormatted<int>(samples);
            defaultInterpolatedStringHandler.AppendLiteral(" samples, changed=");
            defaultInterpolatedStringHandler.AppendFormatted<bool>(changedSinceClick);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
            return;
          }
          await Task.Delay(150, ct).ConfigureAwait(false);
        }
        string text2 = "ScreenSettle";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
        defaultInterpolatedStringHandler.AppendLiteral("screen still changing after ");
        defaultInterpolatedStringHandler.AppendFormatted<double>(4.0, "0");
        defaultInterpolatedStringHandler.AppendLiteral(" s: going on");
        Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
      }
    }

    // Token: 0x060007FE RID: 2046 RVA: 0x00367F1C File Offset: 0x00367F1C
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<byte[]> SampleAsync([Nullable(new byte[] { 1, 1, 2 })] Func<Task<byte[]>> sample)
    {
      byte[] array3;
      try
      {
        byte[] array = await sample().ConfigureAwait(false);
        byte[] array2;
        if (array != null)
        {
          array2 = await ScreenSettle.GraySampleAsync(array).ConfigureAwait(false);
        }
        else
        {
          array2 = null;
        }
        array3 = array2;
      }
      catch (Exception ex) when (!(ex is OperationCanceledException))
      {
        array3 = null;
      }
      return array3;
    }

    // Token: 0x060007FF RID: 2047 RVA: 0x00367F60 File Offset: 0x00367F60
    [CompilerGenerated]
    internal static double <WaitForScreenToSettleAsync>g__Elapsed|10_0(ref ScreenSettle.<>c__DisplayClass10_0 A_0)
    {
      return (double)(Environment.TickCount64 - A_0.started) / 1000.0;
    }

    // Token: 0x040003E2 RID: 994
    private const int SampleWidth = 192;

    // Token: 0x040003E3 RID: 995
    private const int PixelDifferenceThreshold = 12;

    // Token: 0x040003E4 RID: 996
    private const double SettleChangedFraction = 0.005;

    // Token: 0x040003E5 RID: 997
    private const double MaximumWaitSeconds = 4.0;

    // Token: 0x040003E6 RID: 998
    private const int SampleIntervalMilliseconds = 150;

    // Token: 0x040003E7 RID: 999
    private const int RequiredCalmSamples = 2;

    // Token: 0x040003E8 RID: 1000
    private const double NoVisibleChangeGiveUpSeconds = 1.5;
  }
}
