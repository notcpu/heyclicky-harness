using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001CA RID: 458
  public sealed class ClickyBrandMark : Grid
  {
    // Token: 0x060017CB RID: 6091 RVA: 0x005C5ED4 File Offset: 0x005C5ED4
    public ClickyBrandMark(double width = 22.0, Color? color = null, double lineWidth = 3.0)
    {
      double num = width * ClickyBrandMarkShape.CropSize.Height / ClickyBrandMarkShape.CropSize.Width;
      base.Width = width;
      base.Height = num;
      base.IsHitTestVisible = false;
      double num2 = lineWidth * width / ClickyBrandMarkShape.CropSize.Width;
      Geometry geometry = new ClickyBrandMarkShape().PathIn(new Rect(0.0, 0.0, width, num)).ToGeometry();
      Brush brush;
      if (color != null)
      {
        Color valueOrDefault = color.GetValueOrDefault();
        brush = Ui.Brush(valueOrDefault);
      }
      else
      {
        LinearGradientBrush linearGradientBrush = new LinearGradientBrush
        {
          MappingMode = BrushMappingMode.Absolute,
          StartPoint = new Point(0f, 0f),
          EndPoint = new Point(width, num)
        };
        foreach (ValueTuple<double, Color> valueTuple in ClickyBrandMark.GradientStops)
        {
          double item = valueTuple.Item1;
          Color item2 = valueTuple.Item2;
          linearGradientBrush.GradientStops.Add(new GradientStop
          {
            Offset = item,
            Color = item2
          });
        }
        brush = linearGradientBrush;
      }
      base.Children.Add(new Path
      {
        Data = geometry,
        Stroke = brush,
        StrokeThickness = num2,
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round,
        StrokeLineJoin = PenLineJoin.Round,
        IsHitTestVisible = false
      });
    }

    // Token: 0x04000B9F RID: 2975
    [TupleElementNames(new string[] { "Offset", "Color" })]
    [Nullable(new byte[] { 1, 0 })]
    public static readonly ValueTuple<double, Color>[] GradientStops = new ValueTuple<double, Color>[]
    {
      new ValueTuple<double, Color>(0.0, PaperColor.White),
      new ValueTuple<double, Color>(0.55, PaperColor.Hex("#C2DBFF")),
      new ValueTuple<double, Color>(1.0, PaperColor.Hex("#86B6FF"))
    };
  }
}
