using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Drive;
using Microsoft.Graphics.Canvas;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Graphics.DirectX;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002EE RID: 750
  [NullableContext(1)]
  [Nullable(0)]
  internal static class ArcPaperTexture
  {
    // Token: 0x06002040 RID: 8256 RVA: 0x00615AB8 File Offset: 0x00615AB8
    public static FrameworkElement Create()
    {
      Grid host = new Grid
      {
        IsHitTestVisible = false
      };
      host.Loaded += delegate(object _, RoutedEventArgs _)
      {
        if (host.Children.Count > 0)
        {
          return;
        }
        double num = 1.0;
        XamlRoot xamlRoot = host.XamlRoot;
        WriteableBitmap writeableBitmap = ArcPaperTexture.Bitmap(Math.Max(num, (xamlRoot != null) ? xamlRoot.RasterizationScale : 1.0));
        if (writeableBitmap != null)
        {
          host.Children.Add(new Image
          {
            Source = writeableBitmap,
            Stretch = Stretch.Fill
          });
        }
      };
      return host;
    }

    // Token: 0x06002041 RID: 8257 RVA: 0x00615AFC File Offset: 0x00615AFC
    [NullableContext(2)]
    private static WriteableBitmap Bitmap(double scale)
    {
      int num = (int)Math.Round(scale * 100.0);
      WriteableBitmap writeableBitmap;
      if (ArcPaperTexture.cache.TryGetValue(num, out writeableBitmap))
      {
        return writeableBitmap;
      }
      WriteableBitmap writeableBitmap2 = null;
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        float num2 = 480f;
        float num3 = 300f;
        int num4 = (int)Math.Ceiling((double)num2 * scale);
        int num5 = (int)Math.Ceiling((double)num3 * scale);
        double[] array = ArcPaperTexture.GrainTile();
        byte[] array2 = new byte[num4 * num5 * 4];
        for (int i = 0; i < num5; i++)
        {
          for (int j = 0; j < num4; j++)
          {
            int num6 = (int)((double)j / scale * 2.0) % 160;
            int num7 = (int)((double)i / scale * 2.0) % 160;
            byte b = (byte)Math.Round(array[num7 * 160 + num6] * 0.45 * 0.6 * 255.0);
            array2[(i * num4 + j) * 4 + 3] = b;
          }
        }
        using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)num4, (float)num5, 96f))
        {
          using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
          {
            canvasDrawingSession.Clear(Color.FromArgb(0, 0, 0, 0));
            using (CanvasBitmap canvasBitmap = CanvasBitmap.CreateFromBytes(sharedDevice, array2, num4, num5, DirectXPixelFormat.B8G8R8A8UIntNormalized, 96f))
            {
              canvasDrawingSession.DrawImage(canvasBitmap);
            }
            canvasDrawingSession.Transform = Matrix3x2.CreateScale((float)scale);
            float num8 = 6.4f;
            for (float num9 = 16f; num9 <= num3 - num8; num9 += 16f)
            {
              for (float num10 = 16f; num10 <= num2 - num8; num10 += 16f)
              {
                canvasDrawingSession.FillCircle(num10, num9, 1.1f, DS.Black(0.11));
              }
            }
          }
          writeableBitmap2 = DriveArt.Copy(canvasRenderTarget);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("dock", "paper texture: " + ex.Message);
      }
      ArcPaperTexture.cache[num] = writeableBitmap2;
      return writeableBitmap2;
    }

    // Token: 0x06002042 RID: 8258 RVA: 0x00615D8C File Offset: 0x00615D8C
    private static double[] GrainTile()
    {
      DriveArt.PaperRandom paperRandom = new DriveArt.PaperRandom(7UL);
      double[] array = new double[25600];
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = paperRandom.NextUnit();
      }
      double[] array2 = new double[25600];
      for (int j = 0; j < 160; j++)
      {
        for (int k = 0; k < 160; k++)
        {
          double num = 0.0;
          for (int l = -1; l <= 1; l++)
          {
            for (int m = -1; m <= 1; m++)
            {
              num += array[(j + l + 160) % 160 * 160 + (k + m + 160) % 160] * (double)((m == 0 && l == 0) ? 4 : 1);
            }
          }
          array2[j * 160 + k] = Math.Min(1.0, Math.Pow(num / 12.0, 2.2) * 0.3);
        }
      }
      return array2;
    }

    // Token: 0x04000F9E RID: 3998
    [Nullable(new byte[] { 1, 2 })]
    private static readonly Dictionary<int, WriteableBitmap> cache = new Dictionary<int, WriteableBitmap>();
  }
}
