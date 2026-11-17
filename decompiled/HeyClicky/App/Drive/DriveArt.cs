using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C2 RID: 706
  [NullableContext(1)]
  [Nullable(0)]
  internal static class DriveArt
  {
    // Token: 0x06001DB3 RID: 7603 RVA: 0x00601018 File Offset: 0x00601018
    [NullableContext(2)]
    public static WriteableBitmap DriveChip(double chipSize, double frameSize, double scale, int seed = 11)
    {
      return DriveArt.HandoffChip("M5.59 10.49 L8.49 7.07 L14.17 16.81 M14.17 16.81 L19.45 16.81 M15.80 10.49 L19.14 7.07 L22.35 10.49 M1.2 11.2h2.4 M1.8 14.6h2", DriveArt.SkyWash, DriveArt.SkyDeep, chipSize, frameSize, scale, seed, null, true);
    }

    // Token: 0x06001DB4 RID: 7604 RVA: 0x00601048 File Offset: 0x00601048
    [NullableContext(2)]
    public static WriteableBitmap YourTurnChip(double chipSize, double frameSize, double scale, int seed = 11)
    {
      return DriveArt.HandoffChip(HandoffGlyphs.Path(HandoffGlyph.YourTurn), DriveArt.SkyWash, DriveArt.SkyDeep, chipSize, frameSize, scale, seed, null, true);
    }

    // Token: 0x06001DB5 RID: 7605 RVA: 0x00601078 File Offset: 0x00601078
    [NullableContext(2)]
    public static WriteableBitmap HandoffChip(string glyphPath, Color wash, Color deep, double chipSize, double frameSize, double scale, int seed, int? turnSeed = null, bool showsTape = true)
    {
      WriteableBitmap writeableBitmap;
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        int num = Math.Max(1, (int)Math.Ceiling(frameSize * scale));
        using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)num, (float)num, 96f))
        {
          using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
          {
            canvasDrawingSession.Clear(Color.FromArgb(0, 0, 0, 0));
            canvasDrawingSession.Antialiasing = CanvasAntialiasing.Antialiased;
            canvasDrawingSession.Transform = Matrix3x2.CreateScale((float)scale);
            float num2 = (float)((frameSize - chipSize) / 2.0);
            float num3 = (float)chipSize;
            Vector2 vector = new Vector2(num2 + num3 / 2f, num2 + num3 / 2f);
            Matrix3x2 matrix3x = Matrix3x2.CreateRotation((float)((double)(turnSeed.GetValueOrDefault(seed) % 7 - 3) * 3.141592653589793 / 180.0), vector) * Matrix3x2.CreateScale((float)scale);
            canvasDrawingSession.Transform = matrix3x;
            Rect rect = new Rect(num2, num2, num3, num3);
            using (CanvasGeometry canvasGeometry = DriveArt.SketchRoundedRect(sharedDevice, rect, (double)(5f * num3 / 24f), seed, 0.7))
            {
              canvasDrawingSession.FillGeometry(canvasGeometry, wash);
            }
            using (CanvasGeometry canvasGeometry2 = DriveArt.SketchRoundedRect(sharedDevice, rect, (double)(5f * num3 / 24f), seed + 9, 0.6))
            {
              canvasDrawingSession.DrawGeometry(canvasGeometry2, deep.WithAlpha(0.55), 1.1f, new CanvasStrokeStyle
              {
                LineJoin = CanvasLineJoin.Round
              });
            }
            float num4 = num3 * 0.68f;
            float num5 = num4 / 24f;
            if (glyphPath != null)
            {
              using (CanvasGeometry canvasGeometry3 = DriveArt.SvgStrokes(sharedDevice, glyphPath))
              {
                Matrix3x2 matrix3x2 = Matrix3x2.CreateScale(num5) * Matrix3x2.CreateTranslation(vector.X - num4 / 2f, vector.Y - num4 / 2f) * matrix3x;
                canvasDrawingSession.Transform = matrix3x2;
                canvasDrawingSession.DrawGeometry(canvasGeometry3, deep, 1.9f / num5, new CanvasStrokeStyle
                {
                  StartCap = CanvasCapStyle.Round,
                  EndCap = CanvasCapStyle.Round,
                  LineJoin = CanvasLineJoin.Round
                });
              }
            }
            if (showsTape)
            {
              float num6 = num3 * 0.62f;
              float num7 = num3 * 0.24f;
              Vector2 vector2 = new Vector2(num2 + num6 / 2f - num3 * 0.16f, num2 + num7 / 2f - num3 * 0.02f);
              canvasDrawingSession.Transform = Matrix3x2.CreateRotation(-0.55850536f, vector2) * Matrix3x2.CreateScale((float)scale);
              Rect rect2 = new Rect(vector2.X - num6 / 2f, vector2.Y - num7 / 2f, num6, num7);
              using (CanvasCommandList canvasCommandList = new CanvasCommandList(sharedDevice))
              {
                using (CanvasDrawingSession canvasDrawingSession2 = canvasCommandList.CreateDrawingSession())
                {
                  canvasDrawingSession2.FillRectangle(rect2, DS.Black(0.25));
                }
                using (GaussianBlurEffect gaussianBlurEffect = new GaussianBlurEffect
                {
                  Source = canvasCommandList,
                  BlurAmount = 0.3f * (float)scale,
                  BorderMode = EffectBorderMode.Soft
                })
                {
                  canvasDrawingSession.DrawImage(gaussianBlurEffect, new Vector2(0f, 0.4f));
                }
              }
              canvasDrawingSession.FillRectangle(rect2, wash.WithAlpha(0.9));
            }
          }
          writeableBitmap = DriveArt.Copy(canvasRenderTarget);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "chip not drawn: " + ex.Message);
        writeableBitmap = null;
      }
      return writeableBitmap;
    }

    // Token: 0x06001DB6 RID: 7606 RVA: 0x006014EC File Offset: 0x006014EC
    private static CanvasGeometry SketchRoundedRect(CanvasDevice device, Rect rect, double radius, int seed, double jitter)
    {
      DriveArt.SketchRandom random = new DriveArt.SketchRandom(seed);
      double drift = 0.0;
      List<Vector2> list = DriveArt.RoundedRectPoints(rect, radius, 7.0).Select<Vector2, Vector2>(delegate(Vector2 point)
      {
        drift = drift * 0.6 + random.Uniform(-jitter, jitter) * 0.4;
        return new Vector2((float)((double)point.X + drift + random.Uniform(-0.25, 0.25)), (float)((double)point.Y + drift * 0.8 + random.Uniform(-0.25, 0.25)));
      }).ToList<Vector2>();
      list.AddRange(list.Take<Vector2>(2).ToList<Vector2>());
      CanvasGeometry canvasGeometry;
      using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(device))
      {
        canvasPathBuilder.BeginFigure((list[0] + list[1]) / 2f);
        for (int i = 1; i < list.Count - 1; i++)
        {
          canvasPathBuilder.AddQuadraticBezier(list[i], (list[i] + list[i + 1]) / 2f);
        }
        canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
        canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
      }
      return canvasGeometry;
    }

    // Token: 0x06001DB7 RID: 7607 RVA: 0x006015F4 File Offset: 0x006015F4
    private static List<Vector2> RoundedRectPoints(Rect rect, double radius, double step = 7.0)
    {
      DriveArt.<>c__DisplayClass8_0 CS$<>8__locals1;
      CS$<>8__locals1.step = step;
      CS$<>8__locals1.r = Math.Min(radius, Math.Min(rect.Width / 2.0, rect.Height / 2.0));
      CS$<>8__locals1.points = new List<Vector2>();
      double x = rect.X;
      double y = rect.Y;
      double width = rect.Width;
      double height = rect.Height;
      DriveArt.<RoundedRectPoints>g__Edge|8_0(x + CS$<>8__locals1.r, y, x + width - CS$<>8__locals1.r, y, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Arc|8_1(x + width - CS$<>8__locals1.r, y + CS$<>8__locals1.r, -1.5707963267948966, 0.0, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Edge|8_0(x + width, y + CS$<>8__locals1.r, x + width, y + height - CS$<>8__locals1.r, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Arc|8_1(x + width - CS$<>8__locals1.r, y + height - CS$<>8__locals1.r, 0.0, 1.5707963267948966, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Edge|8_0(x + width - CS$<>8__locals1.r, y + height, x + CS$<>8__locals1.r, y + height, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Arc|8_1(x + CS$<>8__locals1.r, y + height - CS$<>8__locals1.r, 1.5707963267948966, 3.141592653589793, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Edge|8_0(x, y + height - CS$<>8__locals1.r, x, y + CS$<>8__locals1.r, ref CS$<>8__locals1);
      DriveArt.<RoundedRectPoints>g__Arc|8_1(x + CS$<>8__locals1.r, y + CS$<>8__locals1.r, 3.141592653589793, 4.71238898038469, ref CS$<>8__locals1);
      return CS$<>8__locals1.points;
    }

    // Token: 0x06001DB8 RID: 7608 RVA: 0x00601798 File Offset: 0x00601798
    internal static CanvasGeometry SvgStrokes(CanvasDevice device, string data)
    {
      CanvasGeometry canvasGeometry;
      using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(device))
      {
        DriveArt.<>c__DisplayClass9_0 CS$<>8__locals1;
        CS$<>8__locals1.tokens = (from m in Regex.Matches(data, "[MLHVCAZmlhvcaz]|-?\\d*\\.?\\d+")
          select m.Value).ToList<string>();
        Vector2 vector = default(Vector2);
        Vector2 vector2 = default(Vector2);
        bool flag = false;
        char c = 'M';
        DriveArt.<>c__DisplayClass9_1 CS$<>8__locals2;
        CS$<>8__locals2.i = 0;
        while (CS$<>8__locals2.i < CS$<>8__locals1.tokens.Count)
        {
          int num;
          if (char.IsLetter(CS$<>8__locals1.tokens[CS$<>8__locals2.i][0]))
          {
            List<string> tokens = CS$<>8__locals1.tokens;
            num = CS$<>8__locals2.i;
            CS$<>8__locals2.i = num + 1;
            c = tokens[num][0];
          }
          if (c <= 'Z')
          {
            if (c <= 'H')
            {
              if (c == 'A')
              {
                goto IL_023C;
              }
              if (c == 'C')
              {
                goto IL_01B9;
              }
              if (c != 'H')
              {
                goto IL_03A0;
              }
              goto IL_033A;
            }
            else if (c <= 'M')
            {
              if (c == 'L')
              {
                goto IL_0305;
              }
              if (c != 'M')
              {
                goto IL_03A0;
              }
            }
            else
            {
              if (c == 'V')
              {
                goto IL_036D;
              }
              if (c != 'Z')
              {
                goto IL_03A0;
              }
              goto IL_02EC;
            }
          }
          else if (c <= 'h')
          {
            if (c == 'a')
            {
              goto IL_023C;
            }
            if (c == 'c')
            {
              goto IL_01B9;
            }
            if (c != 'h')
            {
              goto IL_03A0;
            }
            goto IL_033A;
          }
          else if (c <= 'm')
          {
            if (c == 'l')
            {
              goto IL_0305;
            }
            if (c != 'm')
            {
              goto IL_03A0;
            }
          }
          else
          {
            if (c == 'v')
            {
              goto IL_036D;
            }
            if (c != 'z')
            {
              goto IL_03A0;
            }
            goto IL_02EC;
          }
          Vector2 vector3 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          if (c == 'm')
          {
            vector3 += vector;
          }
          if (flag)
          {
            canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
          }
          canvasPathBuilder.BeginFigure(vector3);
          flag = true;
          vector2 = (vector = vector3);
          c = ((c == 'M') ? 'L' : 'l');
          continue;
          IL_01B9:
          Vector2 vector4 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          Vector2 vector5 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          Vector2 vector6 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          if (c == 'c')
          {
            vector4 += vector;
            vector5 += vector;
            vector6 += vector;
          }
          canvasPathBuilder.AddCubicBezier(vector4, vector5, vector6);
          vector = vector6;
          continue;
          IL_023C:
          float num2 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2);
          float num3 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2);
          float num4 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2);
          bool flag2 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2) != 0f;
          bool flag3 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2) != 0f;
          Vector2 vector7 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          if (c == 'a')
          {
            vector7 += vector;
          }
          canvasPathBuilder.AddArc(vector7, num2, num3, (float)((double)num4 * 3.141592653589793 / 180.0), flag3 ? CanvasSweepDirection.Clockwise : CanvasSweepDirection.CounterClockwise, flag2 ? CanvasArcSize.Large : CanvasArcSize.Small);
          vector = vector7;
          continue;
          IL_02EC:
          if (flag)
          {
            canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
          }
          flag = false;
          vector = vector2;
          c = 'M';
          continue;
          IL_0305:
          Vector2 vector8 = new Vector2(DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2), DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
          vector = ((c == 'l') ? (vector + vector8) : vector8);
          canvasPathBuilder.AddLine(vector);
          continue;
          IL_033A:
          float num5 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2);
          vector = new Vector2((c == 'h') ? (vector.X + num5) : num5, vector.Y);
          canvasPathBuilder.AddLine(vector);
          continue;
          IL_036D:
          float num6 = DriveArt.<SvgStrokes>g__Next|9_1(ref CS$<>8__locals1, ref CS$<>8__locals2);
          vector = new Vector2(vector.X, (c == 'v') ? (vector.Y + num6) : num6);
          canvasPathBuilder.AddLine(vector);
          continue;
          IL_03A0:
          num = CS$<>8__locals2.i;
          CS$<>8__locals2.i = num + 1;
        }
        if (flag)
        {
          canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
        }
        canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
      }
      return canvasGeometry;
    }

    // Token: 0x06001DB9 RID: 7609 RVA: 0x00601BB0 File Offset: 0x00601BB0
    [return: TupleElementNames(new string[] { "Image", "Width", "Height" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<WriteableBitmap, double, double> Stamp(string word, Color color, double fontSize, double scale)
    {
      ValueTuple<WriteableBitmap, double, double> valueTuple;
      try
      {
        CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
        double num = fontSize * 0.09;
        using (CanvasTextFormat canvasTextFormat = new CanvasTextFormat
        {
          FontFamily = "Segoe UI Variable Display",
          FontSize = (float)fontSize,
          FontWeight = FontWeights.ExtraBold,
          WordWrapping = CanvasWordWrapping.NoWrap
        })
        {
          using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(sharedDevice, word, canvasTextFormat, 1000f, 100f))
          {
            canvasTextLayout.SetCharacterSpacing(0, word.Length, 0f, (float)num, 0f);
            double num2 = canvasTextLayout.LayoutBounds.Width - num;
            double num3 = Math.Ceiling(num2 + 16.0);
            double num4 = Math.Ceiling(fontSize + 10.0);
            float num5 = (float)(scale * 3.0 / 3.0);
            using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)Math.Ceiling(num3 * scale), (float)Math.Ceiling(num4 * scale), 96f))
            {
              using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
              {
                canvasDrawingSession.Clear(Color.FromArgb(0, 0, 0, 0));
                canvasDrawingSession.Transform = Matrix3x2.CreateScale(num5);
                canvasDrawingSession.DrawRoundedRectangle(new Rect(1.0, 1.0, num3 - 2.0, num4 - 2.0), 3.5f, 3.5f, color, 1.7f);
                Rect drawBounds = canvasTextLayout.DrawBounds;
                canvasDrawingSession.DrawTextLayout(canvasTextLayout, new Vector2((float)((num3 - num2) / 2.0 - canvasTextLayout.LayoutBounds.X), (float)(num4 / 2.0 - (drawBounds.Y + drawBounds.Height / 2.0))), color);
                canvasDrawingSession.Blend = CanvasBlend.Copy;
                DriveArt.PaperRandom paperRandom = new DriveArt.PaperRandom(DriveArt.PaperRandom.StableSeed(word) + 11UL);
                int num6 = (int)(num3 * num4 / 26.0);
                for (int i = 0; i < num6; i++)
                {
                  double num7 = paperRandom.NextUnit() * num3;
                  double num8 = num4 - paperRandom.NextUnit() * num4;
                  double num9 = 0.18 + paperRandom.NextUnit() * 0.42;
                  double num10 = num9 * (0.6 + paperRandom.NextUnit() * 0.8);
                  canvasDrawingSession.FillEllipse((float)num7, (float)num8, (float)num9, (float)num10, Color.FromArgb(0, 0, 0, 0));
                }
              }
              valueTuple = new ValueTuple<WriteableBitmap, double, double>(DriveArt.Copy(canvasRenderTarget), num3, num4);
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "stamp not drawn: " + ex.Message);
        valueTuple = new ValueTuple<WriteableBitmap, double, double>(null, 0.0, 0.0);
      }
      return valueTuple;
    }

    // Token: 0x06001DBA RID: 7610 RVA: 0x00601F0C File Offset: 0x00601F0C
    public static CanvasGeometry NotchAttachedPath(CanvasDevice device, double width, double height, double bottomRadius)
    {
      float num = (float)width;
      float num2 = (float)height;
      float num3 = 10f;
      float num4 = (float)Math.Min(bottomRadius, (height - 10.0) / 2.0);
      CanvasGeometry canvasGeometry;
      using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(device))
      {
        canvasPathBuilder.BeginFigure(0f, 0f);
        canvasPathBuilder.AddQuadraticBezier(new Vector2(num3, 0f), new Vector2(num3, num3));
        canvasPathBuilder.AddLine(num3, num2 - num4);
        canvasPathBuilder.AddQuadraticBezier(new Vector2(num3, num2), new Vector2(num3 + num4, num2));
        canvasPathBuilder.AddLine(num - num3 - num4, num2);
        canvasPathBuilder.AddQuadraticBezier(new Vector2(num - num3, num2), new Vector2(num - num3, num2 - num4));
        canvasPathBuilder.AddLine(num - num3, num3);
        canvasPathBuilder.AddQuadraticBezier(new Vector2(num - num3, 0f), new Vector2(num, 0f));
        canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
        canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
      }
      return canvasGeometry;
    }

    // Token: 0x06001DBB RID: 7611 RVA: 0x0060201C File Offset: 0x0060201C
    public static Color[] CloudColors(Color accent)
    {
      Color color = DS.Rgb(1.0, 0.62, 0.81, 1.0);
      Color color2 = DS.Rgb(1.0, 0.86, 0.48, 1.0);
      Color color3 = DS.Rgb(0.55, 0.79, 1.0, 1.0);
      Color color4 = DS.Rgb(0.78, 0.66, 1.0, 1.0);
      return new Color[] { accent, color, color2, accent, color3, color4, accent };
    }

    // Token: 0x06001DBC RID: 7612 RVA: 0x00602110 File Offset: 0x00602110
    [return: TupleElementNames(new string[] { "Bgra", "Width", "Height" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public static ValueTuple<byte[], int, int> EdgeGlowMask(double widthDips, double heightDips, double depth)
    {
      int num = Math.Max(2, (int)(widthDips * 0.5));
      int num2 = Math.Max(2, (int)(heightDips * 0.5));
      double num3 = Math.Max(0.5, depth * 0.5 * 0.95 / 2.0);
      double num4 = Math.Max(0.5, depth * 0.5 * 0.32 / 2.0);
      double[] array = DriveArt.<EdgeGlowMask>g__Inside|14_0(num, num3);
      double[] array2 = DriveArt.<EdgeGlowMask>g__Inside|14_0(num2, num3);
      double[] array3 = DriveArt.<EdgeGlowMask>g__Inside|14_0(num, num4);
      double[] array4 = DriveArt.<EdgeGlowMask>g__Inside|14_0(num2, num4);
      byte[] array5 = new byte[num * num2 * 4];
      for (int i = 0; i < num2; i++)
      {
        for (int j = 0; j < num; j++)
        {
          double num5 = 1.0 - array[j] * array2[i];
          double num6 = 0.55 * (1.0 - array3[j] * array4[i]);
          byte b = (byte)Math.Round(Math.Clamp(num5 + num6 * (1.0 - num5), 0.0, 1.0) * 255.0);
          int num7 = (i * num + j) * 4;
          array5[num7] = b;
          array5[num7 + 1] = b;
          array5[num7 + 2] = b;
          array5[num7 + 3] = b;
        }
      }
      return new ValueTuple<byte[], int, int>(array5, num, num2);
    }

    // Token: 0x06001DBD RID: 7613 RVA: 0x006022A4 File Offset: 0x006022A4
    public static byte[] ConicGradient(int size, Color[] colors)
    {
      byte[] array = new byte[size * size * 4];
      double num = (double)size / 2.0;
      int num2 = colors.Length - 1;
      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          double num3 = Math.Atan2(num - ((double)j + 0.5), (double)i + 0.5 - num);
          double num4 = ((num3 < 0.0) ? (num3 + 6.283185307179586) : num3) / 6.283185307179586 * (double)num2;
          int num5 = Math.Min(num2 - 1, (int)num4);
          double num6 = num4 - (double)num5;
          Color color = colors[num5];
          Color color2 = colors[num5 + 1];
          int num7 = (i * size + j) * 4;
          array[num7] = (byte)Math.Round((double)color.B + (double)(color2.B - color.B) * num6);
          array[num7 + 1] = (byte)Math.Round((double)color.G + (double)(color2.G - color.G) * num6);
          array[num7 + 2] = (byte)Math.Round((double)color.R + (double)(color2.R - color.R) * num6);
          array[num7 + 3] = byte.MaxValue;
        }
      }
      return array;
    }

    // Token: 0x06001DBE RID: 7614 RVA: 0x00602400 File Offset: 0x00602400
    private static double Phi(double x)
    {
      return 0.5 * (1.0 + DriveArt.Erf(x / Math.Sqrt(2.0)));
    }

    // Token: 0x06001DBF RID: 7615 RVA: 0x0060242C File Offset: 0x0060242C
    private static double Erf(double x)
    {
      double num = (double)Math.Sign(x);
      x = Math.Abs(x);
      double num2 = 1.0 / (1.0 + 0.3275911 * x);
      double num3 = 1.0 - ((((1.061405429 * num2 - 1.453152027) * num2 + 1.421413741) * num2 - 0.284496736) * num2 + 0.254829592) * num2 * Math.Exp(-x * x);
      return num * num3;
    }

    // Token: 0x06001DC0 RID: 7616 RVA: 0x006024BC File Offset: 0x006024BC
    internal static WriteableBitmap Copy(CanvasRenderTarget target)
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

    // Token: 0x06001DC2 RID: 7618 RVA: 0x00602580 File Offset: 0x00602580
    [CompilerGenerated]
    internal static void <RoundedRectPoints>g__Edge|8_0(double fx, double fy, double tx, double ty, ref DriveArt.<>c__DisplayClass8_0 A_4)
    {
      int num = Math.Max(2, (int)(Math.Sqrt((tx - fx) * (tx - fx) + (ty - fy) * (ty - fy)) / A_4.step));
      for (int i = 0; i < num; i++)
      {
        double num2 = (double)i / (double)num;
        A_4.points.Add(new Vector2((float)(fx + (tx - fx) * num2), (float)(fy + (ty - fy) * num2)));
      }
    }

    // Token: 0x06001DC3 RID: 7619 RVA: 0x006025E4 File Offset: 0x006025E4
    [CompilerGenerated]
    internal static void <RoundedRectPoints>g__Arc|8_1(double cx, double cy, double start, double end, ref DriveArt.<>c__DisplayClass8_0 A_4)
    {
      int num = Math.Max(2, (int)(Math.Abs(end - start) * A_4.r / A_4.step) + 1);
      for (int i = 0; i < num; i++)
      {
        double num2 = start + (end - start) * (double)i / (double)num;
        A_4.points.Add(new Vector2((float)(cx + A_4.r * Math.Cos(num2)), (float)(cy + A_4.r * Math.Sin(num2))));
      }
    }

    // Token: 0x06001DC4 RID: 7620 RVA: 0x00602660 File Offset: 0x00602660
    [CompilerGenerated]
    internal static float <SvgStrokes>g__Next|9_1(ref DriveArt.<>c__DisplayClass9_0 A_0, ref DriveArt.<>c__DisplayClass9_1 A_1)
    {
      List<string> tokens = A_0.tokens;
      int i = A_1.i;
      A_1.i = i + 1;
      return float.Parse(tokens[i], CultureInfo.InvariantCulture);
    }

    // Token: 0x06001DC5 RID: 7621 RVA: 0x00602694 File Offset: 0x00602694
    [CompilerGenerated]
    internal static double[] <EdgeGlowMask>g__Inside|14_0(int count, double sigma)
    {
      double[] array = new double[count];
      for (int i = 0; i < count; i++)
      {
        double num = (double)i + 0.5;
        array[i] = DriveArt.Phi(num / sigma) + DriveArt.Phi(((double)count - num) / sigma) - 1.0;
      }
      return array;
    }

    // Token: 0x04000E3F RID: 3647
    public static readonly Color SkyWash = DS.Hex(14149119U, 1.0);

    // Token: 0x04000E40 RID: 3648
    public static readonly Color SkyDeep = DS.Hex(1784460U, 1.0);

    // Token: 0x04000E41 RID: 3649
    public static readonly Color NeedsYouOnBlack = DS.Hex(16758087U, 1.0);

    // Token: 0x04000E42 RID: 3650
    private const string DriveGlyphPath = "M5.59 10.49 L8.49 7.07 L14.17 16.81 M14.17 16.81 L19.45 16.81 M15.80 10.49 L19.14 7.07 L22.35 10.49 M1.2 11.2h2.4 M1.8 14.6h2";

    // Token: 0x04000E43 RID: 3651
    public const double ShoulderRadius = 10.0;

    // Token: 0x020008D6 RID: 2262
    [NullableContext(0)]
    private struct SketchRandom
    {
      // Token: 0x06003AC6 RID: 15046 RVA: 0x006913F4 File Offset: 0x006913F4
      public SketchRandom(int seed)
      {
        this.state = (ulong)((long)seed + -7046029254386353131L);
      }

      // Token: 0x06003AC7 RID: 15047 RVA: 0x00691408 File Offset: 0x00691408
      public double Next()
      {
        this.state += 11400714819323198485UL;
        ulong num = this.state;
        ulong num2 = (num ^ (num >> 30)) * 13787848793156543929UL;
        ulong num3 = (num2 ^ (num2 >> 27)) * 10723151780598845931UL;
        return ((num3 ^ (num3 >> 31)) >> 11) / 9007199254740992.0;
      }

      // Token: 0x06003AC8 RID: 15048 RVA: 0x00691464 File Offset: 0x00691464
      public double Uniform(double low, double high)
      {
        return low + (high - low) * this.Next();
      }

      // Token: 0x04002556 RID: 9558
      private ulong state;
    }

    // Token: 0x020008D7 RID: 2263
    [NullableContext(0)]
    internal struct PaperRandom
    {
      // Token: 0x06003AC9 RID: 15049 RVA: 0x00691474 File Offset: 0x00691474
      public PaperRandom(ulong seed)
      {
        this.state = (seed * 11400714819323198485UL) | 1UL;
      }

      // Token: 0x06003ACA RID: 15050 RVA: 0x0069148C File Offset: 0x0069148C
      private ulong NextBits()
      {
        this.state ^= this.state >> 12;
        this.state ^= this.state << 25;
        this.state ^= this.state >> 27;
        return this.state * 2685821657736338717UL;
      }

      // Token: 0x06003ACB RID: 15051 RVA: 0x006914EC File Offset: 0x006914EC
      public double NextUnit()
      {
        return (this.NextBits() >> 11) / 9007199254740992.0;
      }

      // Token: 0x06003ACC RID: 15052 RVA: 0x00691504 File Offset: 0x00691504
      [NullableContext(1)]
      public static ulong StableSeed(string text)
      {
        ulong num = 14695981039346656037UL;
        foreach (byte b in Encoding.UTF8.GetBytes(text))
        {
          num = (num ^ (ulong)b) * 1099511628211UL;
        }
        return num;
      }

      // Token: 0x04002557 RID: 9559
      private ulong state;
    }
  }
}
