using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000217 RID: 535
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkDashboardCanvas : Panel
  {
    // Token: 0x06001955 RID: 6485 RVA: 0x005D13A4 File Offset: 0x005D13A4
    public ClickyInkDashboardCanvas(Action<PaperDrawing, Size> draw, double? width = null, double? height = null, bool clips = true)
    {
      this.draw = draw;
      this.clips = clips;
      base.IsHitTestVisible = false;
      if (width != null)
      {
        double valueOrDefault = width.GetValueOrDefault();
        base.Width = Math.Max(0.0, valueOrDefault);
      }
      if (height != null)
      {
        double valueOrDefault2 = height.GetValueOrDefault();
        base.Height = Math.Max(0.0, valueOrDefault2);
      }
      base.Children.Add(this.drawing);
      if (!double.IsNaN(base.Width) && !double.IsNaN(base.Height))
      {
        this.Redraw(new Size(base.Width, base.Height));
      }
    }

    // Token: 0x06001956 RID: 6486 RVA: 0x005D1468 File Offset: 0x005D1468
    protected override Size MeasureOverride(Size availableSize)
    {
      this.drawing.Measure(new Size(0f, 0f));
      double num = ((!double.IsNaN(base.Width)) ? base.Width : (double.IsInfinity(availableSize.Width) ? 0.0 : availableSize.Width));
      double num2 = ((!double.IsNaN(base.Height)) ? base.Height : (double.IsInfinity(availableSize.Height) ? 0.0 : availableSize.Height));
      return new Size(num, num2);
    }

    // Token: 0x06001957 RID: 6487 RVA: 0x005D1500 File Offset: 0x005D1500
    protected override Size ArrangeOverride(Size finalSize)
    {
      this.drawing.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      if (!this.hasDrawn || Math.Abs(finalSize.Width - this.drawnAt.Width) > 0.01 || Math.Abs(finalSize.Height - this.drawnAt.Height) > 0.01)
      {
        this.Redraw(finalSize);
      }
      return finalSize;
    }

    // Token: 0x06001958 RID: 6488 RVA: 0x005D1594 File Offset: 0x005D1594
    private void Redraw(Size size)
    {
      this.drawnAt = size;
      this.hasDrawn = true;
      this.drawing.Children.Clear();
      object obj;
      if (!this.clips)
      {
        obj = null;
      }
      else
      {
        (obj = new RectangleGeometry()).Rect = new Rect(0.0, 0.0, size.Width, size.Height);
      }
      base.Clip = obj;
      if (size.Width <= 0.0 || size.Height <= 0.0)
      {
        return;
      }
      this.draw(this.drawing, size);
    }

    // Token: 0x06001959 RID: 6489 RVA: 0x005D1638 File Offset: 0x005D1638
    public static void DrawText(PaperDrawing drawing, TextBlock text, Point center)
    {
      text.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      Canvas.SetLeft(text, center.X - text.DesiredSize.Width / 2.0);
      Canvas.SetTop(text, center.Y - text.DesiredSize.Height / 2.0);
      drawing.Children.Add(text);
    }

    // Token: 0x04000C52 RID: 3154
    private readonly Action<PaperDrawing, Size> draw;

    // Token: 0x04000C53 RID: 3155
    private readonly PaperDrawing drawing = new PaperDrawing();

    // Token: 0x04000C54 RID: 3156
    private readonly bool clips;

    // Token: 0x04000C55 RID: 3157
    private Size drawnAt;

    // Token: 0x04000C56 RID: 3158
    private bool hasDrawn;
  }
}
