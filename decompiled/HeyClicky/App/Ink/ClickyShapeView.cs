using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000203 RID: 515
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyShapeView : Grid
  {
    // Token: 0x060018A8 RID: 6312 RVA: 0x005CD2F8 File Offset: 0x005CD2F8
    [NullableContext(2)]
    public ClickyShapeView([Nullable(1)] IClickyShape shape, Brush fill = null, PaperStroke stroke = null, bool strokeBorder = false)
    {
      this.shape = shape;
      this.fill = fill;
      this.stroke = stroke;
      this.strokeBorder = strokeBorder;
      base.IsHitTestVisible = false;
      base.Children.Add(new Canvas
      {
        IsHitTestVisible = false,
        Children = { this.path }
      });
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Rebuild();
      };
      this.Apply();
    }

    // Token: 0x060018A9 RID: 6313 RVA: 0x005CD384 File Offset: 0x005CD384
    public static ClickyShapeView Fill(IClickyShape shape, Color color)
    {
      return new ClickyShapeView(shape, Ui.Brush(color), null, false);
    }

    // Token: 0x060018AA RID: 6314 RVA: 0x005CD394 File Offset: 0x005CD394
    public static ClickyShapeView Fill(IClickyShape shape, Brush brush)
    {
      return new ClickyShapeView(shape, brush, null, false);
    }

    // Token: 0x060018AB RID: 6315 RVA: 0x005CD3A0 File Offset: 0x005CD3A0
    public static ClickyShapeView Stroke(IClickyShape shape, PaperStroke stroke)
    {
      return new ClickyShapeView(shape, null, stroke, false);
    }

    // Token: 0x060018AC RID: 6316 RVA: 0x005CD3AC File Offset: 0x005CD3AC
    public static ClickyShapeView Stroke(IClickyShape shape, Color color, double width)
    {
      return new ClickyShapeView(shape, null, new PaperStroke(color, width, PenLineCap.Flat, PenLineJoin.Miter, null), false);
    }

    // Token: 0x060018AD RID: 6317 RVA: 0x005CD3C0 File Offset: 0x005CD3C0
    public static ClickyShapeView StrokeBorder(IClickyShape shape, PaperStroke stroke)
    {
      return new ClickyShapeView(shape, null, stroke, true);
    }

    // Token: 0x17000458 RID: 1112
    // (get) Token: 0x060018AE RID: 6318 RVA: 0x005CD3CC File Offset: 0x005CD3CC
    // (set) Token: 0x060018AF RID: 6319 RVA: 0x005CD3D4 File Offset: 0x005CD3D4
    public IClickyShape Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        this.shape = value;
        this.Rebuild();
      }
    }

    // Token: 0x17000459 RID: 1113
    // (get) Token: 0x060018B0 RID: 6320 RVA: 0x005CD3E4 File Offset: 0x005CD3E4
    // (set) Token: 0x060018B1 RID: 6321 RVA: 0x005CD3EC File Offset: 0x005CD3EC
    [Nullable(2)]
    public Brush FillBrush
    {
      [NullableContext(2)]
      get
      {
        return this.fill;
      }
      [NullableContext(2)]
      set
      {
        this.fill = value;
        this.Apply();
      }
    }

    // Token: 0x1700045A RID: 1114
    // (get) Token: 0x060018B2 RID: 6322 RVA: 0x005CD3FC File Offset: 0x005CD3FC
    // (set) Token: 0x060018B3 RID: 6323 RVA: 0x005CD404 File Offset: 0x005CD404
    [Nullable(2)]
    public PaperStroke StrokeStyle
    {
      [NullableContext(2)]
      get
      {
        return this.stroke;
      }
      [NullableContext(2)]
      set
      {
        this.stroke = value;
        this.Apply();
        this.Rebuild();
      }
    }

    // Token: 0x060018B4 RID: 6324 RVA: 0x005CD41C File Offset: 0x005CD41C
    private void Apply()
    {
      this.path.Fill = this.fill;
      PaperStroke paperStroke = this.stroke;
      if (paperStroke == null)
      {
        this.path.Stroke = null;
        return;
      }
      this.path.Stroke = Ui.Brush(paperStroke.Color);
      this.path.StrokeThickness = paperStroke.Width;
      this.path.StrokeStartLineCap = (this.path.StrokeEndLineCap = (this.path.StrokeDashCap = paperStroke.Cap));
      this.path.StrokeLineJoin = paperStroke.Join;
      double[] dash = paperStroke.Dash;
      if (dash != null)
      {
        DoubleCollection doubleCollection = new DoubleCollection();
        foreach (double num in dash)
        {
          doubleCollection.Add(num / Math.Max(paperStroke.Width, 0.0001));
        }
        this.path.StrokeDashArray = doubleCollection;
        return;
      }
      this.path.StrokeDashArray = null;
    }

    // Token: 0x060018B5 RID: 6325 RVA: 0x005CD520 File Offset: 0x005CD520
    private void Rebuild()
    {
      if (base.ActualWidth <= 0.0 && base.ActualHeight <= 0.0 && double.IsNaN(base.Width))
      {
        return;
      }
      double num = ((base.ActualWidth > 0.0) ? base.ActualWidth : (double.IsNaN(base.Width) ? 0.0 : base.Width));
      double num2 = ((base.ActualHeight > 0.0) ? base.ActualHeight : (double.IsNaN(base.Height) ? 0.0 : base.Height));
      Rect rect = new Rect(0.0, 0.0, num, num2);
      if (this.strokeBorder)
      {
        PaperStroke paperStroke = this.stroke;
        if (paperStroke != null)
        {
          double num3 = paperStroke.Width / 2.0;
          rect = new Rect(num3, num3, Math.Max(0.0, num - 2.0 * num3), Math.Max(0.0, num2 - 2.0 * num3));
        }
      }
      this.path.Data = this.shape.PathIn(rect).ToGeometry();
    }

    // Token: 0x04000C1F RID: 3103
    private readonly Path path = new Path
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000C20 RID: 3104
    private IClickyShape shape;

    // Token: 0x04000C21 RID: 3105
    [Nullable(2)]
    private Brush fill;

    // Token: 0x04000C22 RID: 3106
    [Nullable(2)]
    private PaperStroke stroke;

    // Token: 0x04000C23 RID: 3107
    private bool strokeBorder;
  }
}
