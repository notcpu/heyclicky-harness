using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007B RID: 123
  [NullableContext(1)]
  [Nullable(0)]
  public static class HighlightCompositor
  {
    // Token: 0x06000776 RID: 1910 RVA: 0x003644A8 File Offset: 0x003644A8
    public static bool StrokesTouch(DisplayInfo display, IReadOnlyList<HighlightStroke> strokes)
    {
      double num = 8.0 * display.Scale;
      RectD area = new RectD(display.BoundsPx.X - num, display.BoundsPx.Y - num, display.BoundsPx.Width + 2.0 * num, display.BoundsPx.Height + 2.0 * num);
      return strokes.Any<HighlightStroke>((HighlightStroke s) => s.PointsPx.Any<PointD>(new Func<PointD, bool>(area.Contains)));
    }

    // Token: 0x06000777 RID: 1911 RVA: 0x00364540 File Offset: 0x00364540
    public static async Task<ScreenCaptureFrame> CompositeAsync(ScreenCaptureFrame frame, IReadOnlyList<HighlightStroke> strokes, string colorHex)
    {
      List<HighlightStroke> drawn = strokes.Where<HighlightStroke>((HighlightStroke s) => s.PointsPx.Count >= 2).ToList<HighlightStroke>();
      ScreenCaptureFrame screenCaptureFrame;
      if (drawn.Count == 0 || !HighlightCompositor.StrokesTouch(frame.Display, drawn))
      {
        screenCaptureFrame = frame;
      }
      else
      {
        try
        {
          using (InMemoryRandomAccessStream input = new InMemoryRandomAccessStream())
          {
            await input.WriteAsync(frame.Jpeg.AsBuffer());
            input.Seek(0UL);
            BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(input);
            int width = (int)bitmapDecoder.PixelWidth;
            int height = (int)bitmapDecoder.PixelHeight;
            PixelDataProvider pixelDataProvider = await bitmapDecoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, new BitmapTransform(), ExifOrientationMode.IgnoreExifOrientation, ColorManagementMode.DoNotColorManage);
            byte[] pixels = pixelDataProvider.DetachPixelData();
            ValueTuple<byte, byte, byte> valueTuple = HighlightCompositor.ParseHex(colorHex);
            byte item = valueTuple.Item1;
            byte item2 = valueTuple.Item2;
            byte item3 = valueTuple.Item3;
            double scaleX = (double)width / Math.Max(1.0, frame.Display.BoundsPx.Width);
            double scaleY = (double)height / Math.Max(1.0, frame.Display.BoundsPx.Height);
            double num = 8.0 * frame.Display.Scale * scaleX;
            Func<PointD, ValueTuple<double, double>> <>9__2;
            List<ValueTuple<double, double>[]> list = drawn.Select<HighlightStroke, ValueTuple<double, double>[]>(delegate(HighlightStroke s)
            {
              IEnumerable<PointD> pointsPx = s.PointsPx;
              Func<PointD, ValueTuple<double, double>> func;
              if ((func = <>9__2) == null)
              {
                func = (<>9__2 = (PointD p) => new ValueTuple<double, double>((p.X - frame.Display.BoundsPx.X) * scaleX, (p.Y - frame.Display.BoundsPx.Y) * scaleY));
              }
              return pointsPx.Select<PointD, ValueTuple<double, double>>(func).ToArray<ValueTuple<double, double>>();
            }).ToList<ValueTuple<double, double>[]>();
            HighlightCompositor.PaintPass(pixels, width, height, list, num * 1.35, 0.22, item, item2, item3);
            HighlightCompositor.PaintPass(pixels, width, height, list, num, 0.55, item, item2, item3);
            using (InMemoryRandomAccessStream output = new InMemoryRandomAccessStream())
            {
              BitmapPropertySet bitmapPropertySet = new BitmapPropertySet();
              bitmapPropertySet["ImageQuality"] = new BitmapTypedValue(0.8, PropertyType.Single);
              BitmapPropertySet bitmapPropertySet2 = bitmapPropertySet;
              object obj = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, output, bitmapPropertySet2);
              obj.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)width, (uint)height, 96.0, 96.0, pixels);
              await obj.FlushAsync();
              byte[] bytes = new byte[output.Size];
              output.Seek(0UL);
              await output.ReadAsync(bytes.AsBuffer(), (uint)bytes.Length, InputStreamOptions.None);
              ScreenCaptureFrame screenCaptureFrame2 = frame.<Clone>$();
              screenCaptureFrame2.Jpeg = bytes;
              screenCaptureFrame = screenCaptureFrame2;
            }
          }
        }
        catch (Exception ex)
        {
          Log.Warn("Highlight", "highlight not composited: " + ex.Message);
          screenCaptureFrame = frame;
        }
      }
      return screenCaptureFrame;
    }

    // Token: 0x06000778 RID: 1912 RVA: 0x00364594 File Offset: 0x00364594
    internal static void PaintPass(byte[] bgra, int width, int height, [TupleElementNames(new string[] { "X", "Y" })] [Nullable(new byte[] { 1, 1, 0 })] List<ValueTuple<double, double>[]> paths, double lineWidth, double alpha, byte r, byte g, byte b)
    {
      double num = Math.Max(0.5, lineWidth / 2.0);
      byte[] array = new byte[width * height];
      foreach (ValueTuple<double, double>[] array2 in paths)
      {
        for (int i = 0; i < array2.Length - 1; i++)
        {
          ValueTuple<double, double> valueTuple = array2[i];
          double item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          ValueTuple<double, double> valueTuple2 = array2[i + 1];
          double item3 = valueTuple2.Item1;
          double item4 = valueTuple2.Item2;
          int num2 = Math.Max(0, (int)Math.Floor(Math.Min(item, item3) - num - 1.0));
          int num3 = Math.Min(width - 1, (int)Math.Ceiling(Math.Max(item, item3) + num + 1.0));
          int num4 = Math.Max(0, (int)Math.Floor(Math.Min(item2, item4) - num - 1.0));
          int num5 = Math.Min(height - 1, (int)Math.Ceiling(Math.Max(item2, item4) + num + 1.0));
          double num6 = item3 - item;
          double num7 = item4 - item2;
          double num8 = num6 * num6 + num7 * num7;
          for (int j = num4; j <= num5; j++)
          {
            for (int k = num2; k <= num3; k++)
            {
              double num9 = (double)k + 0.5 - item;
              double num10 = (double)j + 0.5 - item2;
              double num11 = ((num8 > 0.0) ? Math.Clamp((num9 * num6 + num10 * num7) / num8, 0.0, 1.0) : 0.0);
              double num12 = num9 - num11 * num6;
              double num13 = num10 - num11 * num7;
              double num14 = Math.Sqrt(num12 * num12 + num13 * num13);
              double num15 = Math.Clamp(num + 0.5 - num14, 0.0, 1.0);
              if (num15 > 0.0)
              {
                byte b2 = (byte)(num15 * 255.0);
                int num16 = j * width + k;
                if (b2 > array[num16])
                {
                  array[num16] = b2;
                }
              }
            }
          }
        }
      }
      for (int l = 0; l < array.Length; l++)
      {
        if (array[l] != 0)
        {
          double num17 = alpha * (double)array[l] / 255.0;
          int num18 = l * 4;
          bgra[num18] = (byte)((double)bgra[num18] * (1.0 - num17) + (double)b * num17);
          bgra[num18 + 1] = (byte)((double)bgra[num18 + 1] * (1.0 - num17) + (double)g * num17);
          bgra[num18 + 2] = (byte)((double)bgra[num18 + 2] * (1.0 - num17) + (double)r * num17);
        }
      }
    }

    // Token: 0x06000779 RID: 1913 RVA: 0x003648AC File Offset: 0x003648AC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "R", "G", "B" })]
    private static ValueTuple<byte, byte, byte> ParseHex([Nullable(1)] string hex)
    {
      string text = hex.TrimStart('#');
      uint num;
      if (text.Length < 6 || !uint.TryParse(text.AsSpan(0, 6), NumberStyles.HexNumber, null, out num))
      {
        return new ValueTuple<byte, byte, byte>(51, 128, byte.MaxValue);
      }
      return new ValueTuple<byte, byte, byte>((byte)(num >> 16), (byte)(num >> 8), (byte)num);
    }

    // Token: 0x0600077A RID: 1914 RVA: 0x00364904 File Offset: 0x00364904
    [return: Nullable(2)]
    public static string GestureSummary(IReadOnlyList<HighlightStroke> strokes, IReadOnlyList<DisplayInfo> displays)
    {
      List<HighlightStroke> list = strokes.Where<HighlightStroke>((HighlightStroke s) => s.PointsPx.Count >= 2).ToList<HighlightStroke>();
      if (list.Count == 0)
      {
        return null;
      }
      List<string> list2 = list.Select<HighlightStroke, string>((HighlightStroke s) => HighlightCompositor.DescribeStroke(s, displays)).ToList<string>();
      string text;
      if (list2.Count == 1)
      {
        text = "the user " + list2[0];
      }
      else
      {
        string[] ordinals = new string[] { "first", "then", "then", "then", "then", "then" };
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
        defaultInterpolatedStringHandler.AppendLiteral("the user drew ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list2.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" marks in order — ");
        text = defaultInterpolatedStringHandler.ToStringAndClear() + string.Join(", ", list2.Select<string, string>((string d, int i) => ((i < ordinals.Length) ? ordinals[i] : "then") + " " + d));
      }
      return "While asking, " + text + ". Treat this as exactly what they are pointing at.";
    }

    // Token: 0x0600077B RID: 1915 RVA: 0x00364A38 File Offset: 0x00364A38
    private static string DescribeStroke(HighlightStroke stroke, IReadOnlyList<DisplayInfo> displays)
    {
      IReadOnlyList<PointD> pointsPx = stroke.PointsPx;
      HighlightCompositor.StrokeShape strokeShape = HighlightCompositor.ClassifyShape(pointsPx);
      string text = HighlightCompositor.RegionLabel(HighlightCompositor.Centroid(pointsPx), displays);
      string text2 = HighlightCompositor.RegionLabel(pointsPx[0], displays);
      string text3 = HighlightCompositor.RegionLabel(pointsPx[pointsPx.Count - 1], displays);
      string text4;
      if (strokeShape != HighlightCompositor.StrokeShape.Circle)
      {
        if (strokeShape != HighlightCompositor.StrokeShape.Line)
        {
          text4 = "scribbled over the " + text + " area";
        }
        else
        {
          text4 = ((text2 == text3) ? ("drew a line across the " + text + " area") : ("drew a line from the " + text2 + " toward the " + text3));
        }
      }
      else
      {
        text4 = "circled the " + text + " area";
      }
      string text5 = text4;
      if (strokeShape != HighlightCompositor.StrokeShape.Line && text2 != text3)
      {
        text5 = string.Concat(new string[] { text5, ", moving from the ", text2, " toward the ", text3 });
      }
      if (stroke.DwellsPx.Count > 0)
      {
        text5 = text5 + ", pausing on the " + HighlightCompositor.RegionLabel(stroke.DwellsPx[0], displays);
      }
      return text5;
    }

    // Token: 0x0600077C RID: 1916 RVA: 0x00364B4C File Offset: 0x00364B4C
    private static HighlightCompositor.StrokeShape ClassifyShape(IReadOnlyList<PointD> points)
    {
      if (points.Count < 2)
      {
        return HighlightCompositor.StrokeShape.Scribble;
      }
      double num = points.Min<PointD>((PointD p) => p.X);
      double num2 = points.Max<PointD>((PointD p) => p.X);
      double num3 = points.Min<PointD>((PointD p) => p.Y);
      double num4 = points.Max<PointD>((PointD p) => p.Y);
      double num5 = num2 - num;
      double num6 = num4 - num3;
      double num7 = Math.Sqrt(num5 * num5 + num6 * num6);
      if (num7 <= 1.0)
      {
        return HighlightCompositor.StrokeShape.Scribble;
      }
      double num8 = Math.Max(num5, num6);
      double num9 = Math.Min(num5, num6);
      if (num8 > 1.0 && num9 / num8 < 0.2)
      {
        return HighlightCompositor.StrokeShape.Line;
      }
      PointD pointD = HighlightCompositor.Centroid(points);
      double num10 = 0.0;
      double num11 = Math.Atan2(points[0].Y - pointD.Y, points[0].X - pointD.X);
      for (int i = 1; i < points.Count; i++)
      {
        double num12 = Math.Atan2(points[i].Y - pointD.Y, points[i].X - pointD.X);
        double num13;
        for (num13 = num12 - num11; num13 > 3.141592653589793; num13 -= 6.283185307179586)
        {
        }
        while (num13 < -3.141592653589793)
        {
          num13 += 6.283185307179586;
        }
        num10 += num13;
        num11 = num12;
      }
      double num14 = Math.Sqrt(Math.Pow(points[points.Count - 1].X - points[0].X, 2.0) + Math.Pow(points[points.Count - 1].Y - points[0].Y, 2.0));
      if (Math.Abs(num10) < 3.9269908169872414 && num14 >= num7 * 0.45)
      {
        return HighlightCompositor.StrokeShape.Scribble;
      }
      return HighlightCompositor.StrokeShape.Circle;
    }

    // Token: 0x0600077D RID: 1917 RVA: 0x00364DD8 File Offset: 0x00364DD8
    private static PointD Centroid(IReadOnlyList<PointD> points)
    {
      return new PointD(points.Average<PointD>((PointD p) => p.X), points.Average<PointD>((PointD p) => p.Y));
    }

    // Token: 0x0600077E RID: 1918 RVA: 0x00364E34 File Offset: 0x00364E34
    private static string RegionLabel(PointD point, IReadOnlyList<DisplayInfo> displays)
    {
      DisplayInfo displayInfo = displays.FirstOrDefault<DisplayInfo>((DisplayInfo d) => d.BoundsPx.Contains(point));
      RectD rectD;
      if (displayInfo == null)
      {
        DisplayInfo displayInfo2 = displays.FirstOrDefault<DisplayInfo>();
        rectD = ((displayInfo2 != null) ? displayInfo2.BoundsPx : new RectD(0.0, 0.0, 1.0, 1.0));
      }
      else
      {
        rectD = displayInfo.BoundsPx;
      }
      RectD rectD2 = rectD;
      double num = (point.X - rectD2.X) / Math.Max(1.0, rectD2.Width);
      double num2 = (point.Y - rectD2.Y) / Math.Max(1.0, rectD2.Height);
      string text = ((num < 0.34) ? "left" : ((num < 0.67) ? "center" : "right"));
      string text2 = ((num2 < 0.34) ? "top" : ((num2 < 0.67) ? "middle" : "bottom"));
      string text3 = text2;
      string text4 = text;
      string text5;
      if (text3 == "middle")
      {
        if (!(text4 == "center"))
        {
          text5 = text4;
        }
        else
        {
          text5 = "center";
        }
      }
      else if (!(text4 == "center"))
      {
        text5 = text2 + "-" + text;
      }
      else
      {
        text5 = text3;
      }
      return text5;
    }

    // Token: 0x0400039A RID: 922
    private const double HighlighterLineWidthPoints = 8.0;

    // Token: 0x02000332 RID: 818
    [NullableContext(0)]
    private enum StrokeShape
    {
      // Token: 0x040016A4 RID: 5796
      Circle,
      // Token: 0x040016A5 RID: 5797
      Line,
      // Token: 0x040016A6 RID: 5798
      Scribble
    }
  }
}
