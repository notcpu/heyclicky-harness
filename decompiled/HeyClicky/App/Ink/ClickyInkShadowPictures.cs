using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000282 RID: 642
  public static class ClickyInkShadowPictures
  {
    // Token: 0x06001B10 RID: 6928 RVA: 0x005EA5F4 File Offset: 0x005EA5F4
    [NullableContext(1)]
    [return: TupleElementNames(new string[] { "Source", "Reach" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<ImageSource, double> Picture(IClickyShape shape, string shapeKey, Size size, PaperShadow shadow)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
      defaultInterpolatedStringHandler.AppendFormatted(shapeKey);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<double>(size.Width, "0.#");
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<double>(size.Height, "0.#");
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<Color>(shadow.Color);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<double>(shadow.Radius);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      ValueTuple<ImageSource, double> valueTuple;
      if (ClickyInkShadowPictures.Cache.TryGetValue(text, out valueTuple))
      {
        return valueTuple;
      }
      double num = shadow.Radius * 0.5;
      double num2 = Math.Ceiling(num * 3.0 + 1.0);
      int num3 = (int)Math.Ceiling((size.Width + 2.0 * num2) * 2.0);
      int num4 = (int)Math.Ceiling((size.Height + 2.0 * num2) * 2.0);
      CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
      ValueTuple<ImageSource, double> valueTuple2;
      using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)num3, (float)num4, 96f))
      {
        using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
        {
          canvasDrawingSession.Clear(Colors.Transparent);
          canvasDrawingSession.Transform = Matrix3x2.CreateScale(2f) * Matrix3x2.CreateTranslation(0f, 0f);
          using (CanvasGeometry canvasGeometry = shape.PathIn(new Rect(num2, num2, size.Width, size.Height)).ToCanvasGeometry(sharedDevice))
          {
            using (CanvasCommandList canvasCommandList = new CanvasCommandList(sharedDevice))
            {
              using (CanvasDrawingSession canvasDrawingSession2 = canvasCommandList.CreateDrawingSession())
              {
                canvasDrawingSession2.FillGeometry(canvasGeometry, Colors.Black);
              }
              using (ShadowEffect shadowEffect = new ShadowEffect
              {
                Source = canvasCommandList,
                BlurAmount = (float)num,
                ShadowColor = shadow.Color,
                Optimization = EffectOptimization.Quality
              })
              {
                canvasDrawingSession.DrawImage(shadowEffect);
              }
            }
          }
        }
        byte[] pixelBytes = canvasRenderTarget.GetPixelBytes();
        WriteableBitmap writeableBitmap = new WriteableBitmap(num3, num4);
        using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
        {
          stream.Write(pixelBytes, 0, pixelBytes.Length);
        }
        writeableBitmap.Invalidate();
        if (ClickyInkShadowPictures.Cache.Count > 300)
        {
          ClickyInkShadowPictures.Cache.Clear();
        }
        ClickyInkShadowPictures.Cache[text] = new ValueTuple<ImageSource, double>(writeableBitmap, num2);
        valueTuple2 = new ValueTuple<ImageSource, double>(writeableBitmap, num2);
      }
      return valueTuple2;
    }

    // Token: 0x04000CF8 RID: 3320
    private const double Scale = 2.0;

    // Token: 0x04000CF9 RID: 3321
    [TupleElementNames(new string[] { "Source", "Reach" })]
    [Nullable(new byte[] { 1, 1, 0, 1 })]
    private static readonly Dictionary<string, ValueTuple<ImageSource, double>> Cache = new Dictionary<string, ValueTuple<ImageSource, double>>();
  }
}
