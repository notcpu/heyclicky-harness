using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Portraits
{
  // Token: 0x0200010D RID: 269
  [NullableContext(1)]
  [Nullable(0)]
  public static class PortraitWash
  {
    // Token: 0x06000E47 RID: 3655 RVA: 0x00572CCC File Offset: 0x00572CCC
    [NullableContext(0)]
    [return: Nullable(2)]
    public static WriteableBitmap For([Nullable(1)] PortraitAppearance a, [TupleElementNames(new string[] { "X", "Y", "W", "H" })] ValueTuple<double, double, double, double>? crop)
    {
      ValueTuple<Hairstyle, Colorway, int> valueTuple = new ValueTuple<Hairstyle, Colorway, int>(a.Hairstyle, a.HairColor, ((crop != null) > false) ? 1 : 0);
      WriteableBitmap writeableBitmap;
      if (PortraitWash.cache.TryGetValue(valueTuple, out writeableBitmap))
      {
        return writeableBitmap;
      }
      WriteableBitmap writeableBitmap4;
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        using (CanvasRenderTarget canvasRenderTarget = PortraitWash.Render(sharedDevice, a))
        {
          WriteableBitmap writeableBitmap2;
          if (crop != null)
          {
            ValueTuple<double, double, double, double> valueOrDefault = crop.GetValueOrDefault();
            writeableBitmap2 = PortraitWash.Crop(sharedDevice, canvasRenderTarget, valueOrDefault);
          }
          else
          {
            writeableBitmap2 = PortraitWash.Copy(canvasRenderTarget);
          }
          WriteableBitmap writeableBitmap3 = writeableBitmap2;
          if (PortraitWash.cache.Count > 64)
          {
            PortraitWash.cache.Clear();
          }
          PortraitWash.cache[valueTuple] = writeableBitmap3;
          writeableBitmap4 = writeableBitmap3;
        }
      }
      catch (Exception ex)
      {
        Log.Warn("portrait", "wash failed: " + ex.Message);
        writeableBitmap4 = null;
      }
      return writeableBitmap4;
    }

    // Token: 0x06000E48 RID: 3656 RVA: 0x00572DB4 File Offset: 0x00572DB4
    private static CanvasRenderTarget Render(CanvasDevice device, PortraitAppearance a)
    {
      CloudFaceTraits cloudFaceTraits = PortraitArt.ClassicTraits(a);
      ValueTuple<uint, uint, uint> valueTuple = a.HairColor.ClassicCloudPalette();
      CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(device, 640f, 474f, 96f);
      CanvasRenderTarget canvasRenderTarget2;
      using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
      {
        canvasDrawingSession.Clear(PortraitData.Rgb(valueTuple.Item1));
        int num = 0;
        foreach (CloudFaceTraits.Blob blob in cloudFaceTraits.ColorBlobs)
        {
          float num2 = (float)(320.0 * blob.Diameter) * 2f;
          Vector2 vector = new Vector2((float)(320.0 * blob.CenterX), (float)(237.0 * blob.CenterY)) * 2f;
          Color color = PortraitData.Rgb((num == 0) ? valueTuple.Item2 : ((num == 1) ? valueTuple.Item3 : valueTuple.Item1));
          using (CanvasCommandList canvasCommandList = new CanvasCommandList(device))
          {
            using (CanvasDrawingSession canvasDrawingSession2 = canvasCommandList.CreateDrawingSession())
            {
              canvasDrawingSession2.FillCircle(vector, num2 / 2f, color);
            }
            using (GaussianBlurEffect gaussianBlurEffect = new GaussianBlurEffect
            {
              Source = canvasCommandList,
              BlurAmount = 102.399994f,
              BorderMode = EffectBorderMode.Soft,
              Optimization = EffectOptimization.Quality
            })
            {
              canvasDrawingSession.DrawImage(gaussianBlurEffect);
              num++;
            }
          }
        }
        canvasRenderTarget2 = canvasRenderTarget;
      }
      return canvasRenderTarget2;
    }

    // Token: 0x06000E49 RID: 3657 RVA: 0x00572FC0 File Offset: 0x00572FC0
    private static WriteableBitmap Crop(CanvasDevice device, CanvasRenderTarget wash, [TupleElementNames(new string[] { "X", "Y", "W", "H" })] [Nullable(0)] ValueTuple<double, double, double, double> crop)
    {
      ValueTuple<double, double, double, double> classicBodyRect = PortraitArt.ClassicBodyRect;
      int num = Math.Max(1, (int)Math.Ceiling(crop.Item3 * 2.0));
      int num2 = Math.Max(1, (int)Math.Ceiling(crop.Item4 * 2.0));
      WriteableBitmap writeableBitmap;
      using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(device, (float)num, (float)num2, 96f))
      {
        using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
        {
          canvasDrawingSession.Clear(<PortraitWash>F2FF3F8DF461DCABE6D955F65C17EB9EF429F49A32432E29B2D7E392AB32ECE00__Colors.Transparent);
          Rect rect = new Rect((classicBodyRect.Item1 - crop.Item1) * 2.0, (classicBodyRect.Item2 - crop.Item2) * 2.0, classicBodyRect.Item3 * 2.0, classicBodyRect.Item4 * 2.0);
          canvasDrawingSession.DrawImage(wash, rect, wash.Bounds, 1f, CanvasImageInterpolation.HighQualityCubic);
        }
        writeableBitmap = PortraitWash.Copy(canvasRenderTarget);
      }
      return writeableBitmap;
    }

    // Token: 0x06000E4A RID: 3658 RVA: 0x005730DC File Offset: 0x005730DC
    private static WriteableBitmap Copy(CanvasRenderTarget target)
    {
      byte[] pixelBytes = target.GetPixelBytes();
      WriteableBitmap writeableBitmap = new WriteableBitmap((int)target.SizeInPixels.Width, (int)target.SizeInPixels.Height);
      using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
      {
        stream.Write(pixelBytes, 0, pixelBytes.Length);
      }
      writeableBitmap.Invalidate();
      return writeableBitmap;
    }

    // Token: 0x040005AD RID: 1453
    private const int Width = 320;

    // Token: 0x040005AE RID: 1454
    private const int Height = 237;

    // Token: 0x040005AF RID: 1455
    private const float Supersample = 2f;

    // Token: 0x040005B0 RID: 1456
    [Nullable(new byte[] { 1, 0, 1 })]
    private static readonly Dictionary<ValueTuple<Hairstyle, Colorway, int>, WriteableBitmap> cache = new Dictionary<ValueTuple<Hairstyle, Colorway, int>, WriteableBitmap>();
  }
}
