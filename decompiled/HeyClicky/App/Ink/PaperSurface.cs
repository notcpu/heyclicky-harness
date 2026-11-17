using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000208 RID: 520
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperSurface : Grid
  {
    // Token: 0x060018F4 RID: 6388 RVA: 0x005CE058 File Offset: 0x005CE058
    public PaperSurface(IClickyShape shape, PaperFill fill, double? grainOpacity = null)
    {
      this.shape = shape;
      this.Fill = fill;
      this.GrainOpacity = grainOpacity;
      base.IsHitTestVisible = false;
      base.Children.Add(this.canvas);
      this.canvas.Draw += delegate(CanvasControl _, CanvasDrawEventArgs e)
      {
        this.Draw(e.DrawingSession);
      };
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Layout();
      };
    }

    // Token: 0x17000466 RID: 1126
    // (get) Token: 0x060018F5 RID: 6389 RVA: 0x005CE0F8 File Offset: 0x005CE0F8
    // (set) Token: 0x060018F6 RID: 6390 RVA: 0x005CE100 File Offset: 0x005CE100
    public IClickyShape Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        this.shape = value;
        this.canvas.Invalidate();
      }
    }

    // Token: 0x17000467 RID: 1127
    // (get) Token: 0x060018F7 RID: 6391 RVA: 0x005CE114 File Offset: 0x005CE114
    // (set) Token: 0x060018F8 RID: 6392 RVA: 0x005CE11C File Offset: 0x005CE11C
    public PaperFill Fill { get; set; }

    // Token: 0x17000468 RID: 1128
    // (get) Token: 0x060018F9 RID: 6393 RVA: 0x005CE128 File Offset: 0x005CE128
    // (set) Token: 0x060018FA RID: 6394 RVA: 0x005CE130 File Offset: 0x005CE130
    public double? GrainOpacity { get; set; }

    // Token: 0x17000469 RID: 1129
    // (get) Token: 0x060018FB RID: 6395 RVA: 0x005CE13C File Offset: 0x005CE13C
    public List<PaperWash> Washes { get; } = new List<PaperWash>();

    // Token: 0x1700046A RID: 1130
    // (get) Token: 0x060018FC RID: 6396 RVA: 0x005CE144 File Offset: 0x005CE144
    // (set) Token: 0x060018FD RID: 6397 RVA: 0x005CE14C File Offset: 0x005CE14C
    [Nullable(2)]
    public PaperStroke Outline
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700046B RID: 1131
    // (get) Token: 0x060018FE RID: 6398 RVA: 0x005CE158 File Offset: 0x005CE158
    // (set) Token: 0x060018FF RID: 6399 RVA: 0x005CE160 File Offset: 0x005CE160
    public bool OutlineIsBorder { get; set; }

    // Token: 0x1700046C RID: 1132
    // (get) Token: 0x06001900 RID: 6400 RVA: 0x005CE16C File Offset: 0x005CE16C
    public List<PaperShadow> Shadows { get; } = new List<PaperShadow>();

    // Token: 0x1700046D RID: 1133
    // (get) Token: 0x06001901 RID: 6401 RVA: 0x005CE174 File Offset: 0x005CE174
    // (set) Token: 0x06001902 RID: 6402 RVA: 0x005CE17C File Offset: 0x005CE17C
    public double SheetOpacity { get; set; } = 1.0;

    // Token: 0x06001903 RID: 6403 RVA: 0x005CE188 File Offset: 0x005CE188
    public PaperSurface WithShadow(Color color, double radius, double x = 0.0, double y = 0.0)
    {
      this.Shadows.Add(new PaperShadow(color, radius, x, y));
      this.Layout();
      return this;
    }

    // Token: 0x06001904 RID: 6404 RVA: 0x005CE1A8 File Offset: 0x005CE1A8
    public PaperSurface WithWash(PaperFill fill, [Nullable(2)] Func<Size, Rect> frame = null, bool clipped = true)
    {
      this.Washes.Add(new PaperWash(fill, frame, clipped, null));
      this.canvas.Invalidate();
      return this;
    }

    // Token: 0x06001905 RID: 6405 RVA: 0x005CE1CC File Offset: 0x005CE1CC
    public PaperSurface WithOutline(PaperStroke outline, bool isBorder = false)
    {
      this.Outline = outline;
      this.OutlineIsBorder = isBorder;
      this.Layout();
      return this;
    }

    // Token: 0x06001906 RID: 6406 RVA: 0x005CE1E4 File Offset: 0x005CE1E4
    public void Redraw()
    {
      this.canvas.Invalidate();
    }

    // Token: 0x06001907 RID: 6407 RVA: 0x005CE1F4 File Offset: 0x005CE1F4
    private void Layout()
    {
      PaperStroke outline = this.Outline;
      this.reach = ((outline != null) ? outline.Width : 0.0);
      foreach (PaperShadow paperShadow in this.Shadows)
      {
        this.reach = Math.Max(this.reach, paperShadow.Radius * 0.5 * 3.0 + Math.Max(Math.Abs(paperShadow.X), Math.Abs(paperShadow.Y)) + 1.0);
      }
      this.reach = Math.Ceiling(this.reach);
      this.canvas.Margin = new Thickness(-this.reach);
      this.canvas.Invalidate();
    }

    // Token: 0x06001908 RID: 6408 RVA: 0x005CE2E4 File Offset: 0x005CE2E4
    private void Draw(CanvasDrawingSession ds)
    {
      double actualWidth = base.ActualWidth;
      double actualHeight = base.ActualHeight;
      if (actualWidth <= 0.0 || actualHeight <= 0.0)
      {
        return;
      }
      Rect rect = new Rect(0.0, 0.0, actualWidth, actualHeight);
      ds.Transform = Matrix3x2.CreateTranslation((float)this.reach, (float)this.reach);
      CanvasDevice device = ds.Device;
      using (CanvasGeometry canvasGeometry = this.shape.PathIn(rect).ToCanvasGeometry(device))
      {
        using ((this.SheetOpacity < 1.0) ? ds.CreateLayer((float)this.SheetOpacity) : null)
        {
          foreach (PaperShadow paperShadow in this.Shadows)
          {
            using (CanvasCommandList canvasCommandList = new CanvasCommandList(device))
            {
              using (CanvasDrawingSession canvasDrawingSession = canvasCommandList.CreateDrawingSession())
              {
                canvasDrawingSession.FillGeometry(canvasGeometry, Colors.Black);
              }
              using (ShadowEffect shadowEffect = new ShadowEffect
              {
                Source = canvasCommandList,
                BlurAmount = (float)(paperShadow.Radius * 0.5),
                ShadowColor = paperShadow.Color,
                Optimization = EffectOptimization.Quality
              })
              {
                ds.DrawImage(shadowEffect, (float)paperShadow.X, (float)paperShadow.Y);
              }
            }
          }
          using (ds.CreateLayer(1f, canvasGeometry))
          {
            using (CanvasCommandList canvasCommandList2 = new CanvasCommandList(device))
            {
              using (CanvasDrawingSession canvasDrawingSession2 = canvasCommandList2.CreateDrawingSession())
              {
                canvasDrawingSession2.FillRectangle(rect, this.Fill.Brush(device, rect));
              }
              double? grainOpacity = this.GrainOpacity;
              if (grainOpacity != null)
              {
                double valueOrDefault = grainOpacity.GetValueOrDefault();
                if (valueOrDefault > 0.0)
                {
                  using (ICanvasImage canvasImage = ClickyPaperTextures.GrainImage(device, rect, valueOrDefault))
                  {
                    using (BlendEffect blendEffect = new BlendEffect
                    {
                      Background = canvasCommandList2,
                      Foreground = canvasImage,
                      Mode = BlendEffectMode.Multiply
                    })
                    {
                      ds.DrawImage(blendEffect);
                      goto IL_0233;
                    }
                  }
                }
              }
              ds.DrawImage(canvasCommandList2);
              IL_0233:
              foreach (PaperWash paperWash in this.Washes.Where<PaperWash>((PaperWash x) => x.Clipped))
              {
                PaperSurface.DrawWash(ds, paperWash, rect);
              }
            }
          }
          foreach (PaperWash paperWash2 in this.Washes.Where<PaperWash>((PaperWash x) => !x.Clipped))
          {
            PaperSurface.DrawWash(ds, paperWash2, rect);
          }
          PaperStroke o = this.Outline;
          if (o != null)
          {
            CanvasStrokeStyle canvasStrokeStyle = new CanvasStrokeStyle();
            canvasStrokeStyle.StartCap = PaperSurface.Cap(o.Cap);
            canvasStrokeStyle.EndCap = PaperSurface.Cap(o.Cap);
            canvasStrokeStyle.DashCap = PaperSurface.Cap(o.Cap);
            canvasStrokeStyle.LineJoin = PaperSurface.Join(o.Join);
            double[] dash = o.Dash;
            canvasStrokeStyle.CustomDashStyle = ((dash != null) ? dash.Select<double, float>((double d) => (float)(d / Math.Max(o.Width, 0.0001))).ToArray<float>() : null);
            using (CanvasStrokeStyle canvasStrokeStyle2 = canvasStrokeStyle)
            {
              if (this.OutlineIsBorder)
              {
                double num = o.Width / 2.0;
                using (CanvasGeometry canvasGeometry2 = this.shape.PathIn(new Rect(num, num, Math.Max(0.0, actualWidth - 2.0 * num), Math.Max(0.0, actualHeight - 2.0 * num))).ToCanvasGeometry(device))
                {
                  ds.DrawGeometry(canvasGeometry2, o.Color, (float)o.Width, canvasStrokeStyle2);
                  return;
                }
              }
              ds.DrawGeometry(canvasGeometry, o.Color, (float)o.Width, canvasStrokeStyle2);
            }
          }
        }
      }
    }

    // Token: 0x06001909 RID: 6409 RVA: 0x005CE8F4 File Offset: 0x005CE8F4
    private static void DrawWash(CanvasDrawingSession ds, PaperWash wash, Rect sheet)
    {
      Func<Size, Rect> frame = wash.Frame;
      Rect rect = ((frame != null) ? frame(new Size(sheet.Width, sheet.Height)) : sheet);
      Func<Size, PaperPath> path = wash.Path;
      if (path != null)
      {
        using (CanvasGeometry canvasGeometry = path(new Size(sheet.Width, sheet.Height)).ToCanvasGeometry(ds.Device))
        {
          ds.FillGeometry(canvasGeometry, wash.Fill.Brush(ds.Device, rect));
          return;
        }
      }
      ds.FillRectangle(rect, wash.Fill.Brush(ds.Device, rect));
    }

    // Token: 0x0600190A RID: 6410 RVA: 0x005CE9A4 File Offset: 0x005CE9A4
    private static CanvasCapStyle Cap(PenLineCap cap)
    {
      CanvasCapStyle canvasCapStyle;
      switch (cap)
      {
      case PenLineCap.Square:
        canvasCapStyle = CanvasCapStyle.Square;
        break;
      case PenLineCap.Round:
        canvasCapStyle = CanvasCapStyle.Round;
        break;
      case PenLineCap.Triangle:
        canvasCapStyle = CanvasCapStyle.Triangle;
        break;
      default:
        canvasCapStyle = CanvasCapStyle.Flat;
        break;
      }
      return canvasCapStyle;
    }

    // Token: 0x0600190B RID: 6411 RVA: 0x005CE9D8 File Offset: 0x005CE9D8
    private static CanvasLineJoin Join(PenLineJoin join)
    {
      CanvasLineJoin canvasLineJoin;
      if (join != PenLineJoin.Bevel)
      {
        if (join == PenLineJoin.Round)
        {
          canvasLineJoin = CanvasLineJoin.Round;
        }
        else
        {
          canvasLineJoin = CanvasLineJoin.Miter;
        }
      }
      else
      {
        canvasLineJoin = CanvasLineJoin.Bevel;
      }
      return canvasLineJoin;
    }

    // Token: 0x04000C2D RID: 3117
    private readonly CanvasControl canvas = new CanvasControl
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000C2E RID: 3118
    private IClickyShape shape;

    // Token: 0x04000C2F RID: 3119
    private double reach;
  }
}
