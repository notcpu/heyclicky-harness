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
  // Token: 0x02000281 RID: 641
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkShadowedShape : Panel
  {
    // Token: 0x06001B0C RID: 6924 RVA: 0x005EA268 File Offset: 0x005EA268
    [NullableContext(2)]
    public ClickyInkShadowedShape([Nullable(1)] IClickyShape shape, [Nullable(1)] string shapeKey, Brush fill, PaperShadow shadow = null, PaperStroke stroke = null)
    {
      this.shape = shape;
      this.shapeKey = shapeKey;
      this.fill = fill;
      this.stroke = stroke;
      this.shadow = shadow;
      base.IsHitTestVisible = false;
      base.Children.Add(this.shadowImage);
      base.Children.Add(this.fillPath);
      base.Children.Add(this.strokePath);
      this.fillPath.Fill = fill;
      if (stroke != null)
      {
        this.strokePath.Stroke = Ui.Brush(stroke.Color);
        this.strokePath.StrokeThickness = stroke.Width;
        this.strokePath.StrokeStartLineCap = (this.strokePath.StrokeEndLineCap = stroke.Cap);
        this.strokePath.StrokeLineJoin = stroke.Join;
      }
    }

    // Token: 0x06001B0D RID: 6925 RVA: 0x005EA384 File Offset: 0x005EA384
    public ClickyInkShadowedShape(IClickyShape shape, string shapeKey, Color fill, [Nullable(2)] PaperShadow shadow = null, [Nullable(2)] PaperStroke stroke = null)
      : this(shape, shapeKey, Ui.Brush(fill), shadow, stroke)
    {
    }

    // Token: 0x06001B0E RID: 6926 RVA: 0x005EA398 File Offset: 0x005EA398
    protected override Size MeasureOverride(Size availableSize)
    {
      foreach (UIElement uielement in base.Children)
      {
        uielement.Measure(new Size(0f, 0f));
      }
      return new Size(0f, 0f);
    }

    // Token: 0x06001B0F RID: 6927 RVA: 0x005EA400 File Offset: 0x005EA400
    protected override Size ArrangeOverride(Size finalSize)
    {
      if (finalSize != this.drawnSize)
      {
        this.drawnSize = finalSize;
        Rect rect = new Rect(0.0, 0.0, finalSize.Width, finalSize.Height);
        PaperPath paperPath = this.shape.PathIn(rect);
        this.fillPath.Data = paperPath.ToGeometry();
        if (this.stroke != null)
        {
          this.strokePath.Data = this.shape.PathIn(rect).ToGeometry();
        }
        PaperShadow paperShadow = this.shadow;
        if (paperShadow != null && finalSize.Width > 0.0 && finalSize.Height > 0.0)
        {
          ValueTuple<ImageSource, double> valueTuple = ClickyInkShadowPictures.Picture(this.shape, this.shapeKey, finalSize, paperShadow);
          ImageSource item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          this.shadowImage.Source = item;
          this.shadowImage.Tag = item2;
        }
      }
      foreach (UIElement uielement in base.Children)
      {
        if (uielement == this.shadowImage)
        {
          PaperShadow paperShadow2 = this.shadow;
          if (paperShadow2 != null)
          {
            object tag = this.shadowImage.Tag;
            if (tag is double)
            {
              double num = (double)tag;
              uielement.Arrange(new Rect(paperShadow2.X - num, paperShadow2.Y - num, finalSize.Width + 2.0 * num, finalSize.Height + 2.0 * num));
              continue;
            }
          }
        }
        uielement.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      }
      return finalSize;
    }

    // Token: 0x04000CEF RID: 3311
    private readonly IClickyShape shape;

    // Token: 0x04000CF0 RID: 3312
    private readonly string shapeKey;

    // Token: 0x04000CF1 RID: 3313
    [Nullable(2)]
    private readonly Brush fill;

    // Token: 0x04000CF2 RID: 3314
    [Nullable(2)]
    private readonly PaperStroke stroke;

    // Token: 0x04000CF3 RID: 3315
    [Nullable(2)]
    private readonly PaperShadow shadow;

    // Token: 0x04000CF4 RID: 3316
    private readonly Image shadowImage = new Image
    {
      Stretch = Stretch.Fill,
      IsHitTestVisible = false
    };

    // Token: 0x04000CF5 RID: 3317
    private readonly Path fillPath = new Path
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000CF6 RID: 3318
    private readonly Path strokePath = new Path
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000CF7 RID: 3319
    private Size drawnSize;
  }
}
