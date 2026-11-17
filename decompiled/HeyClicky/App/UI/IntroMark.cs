using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AC RID: 172
  [NullableContext(1)]
  [Nullable(0)]
  public static class IntroMark
  {
    // Token: 0x06000722 RID: 1826 RVA: 0x005358F8 File Offset: 0x005358F8
    public static Path Create(double size, Color fill, double rotationDegrees = 0.0)
    {
      Path path = new Path
      {
        Data = IntroMark.Geometry(size, 0.09),
        Fill = Ui.Brush(fill),
        Width = size,
        Height = size
      };
      if (rotationDegrees != 0.0)
      {
        path.RenderTransform = new RotateTransform
        {
          Angle = rotationDegrees,
          CenterX = size / 2.0,
          CenterY = size / 2.0
        };
      }
      return path;
    }

    // Token: 0x06000723 RID: 1827 RVA: 0x0053597C File Offset: 0x0053597C
    public static PathGeometry Geometry(double size, double cornerFraction = 0.09)
    {
      double num = size / 2.0;
      double num2 = size / 2.0;
      double num3 = size * 0.5;
      double num4 = size * Math.Sqrt(3.0) / 4.0;
      return IntroMark.Rounded(new Vector2[]
      {
        new Vector2((float)(num + num4), (float)num2),
        new Vector2((float)(num - num4), (float)(num2 + num3)),
        new Vector2((float)(num - num4), (float)(num2 - num3))
      }, size * cornerFraction);
    }

    // Token: 0x06000724 RID: 1828 RVA: 0x00535A18 File Offset: 0x00535A18
    public static Path Cursor(double size, Color fill, double rotationDegrees)
    {
      double num = size * Math.Sqrt(3.0) / 2.0;
      double num2 = size / 2.0;
      double num3 = size / 2.0;
      PathGeometry pathGeometry = IntroMark.Rounded(new Vector2[]
      {
        new Vector2((float)num2, (float)(num3 - num / 1.5)),
        new Vector2((float)(num2 + size / 2.0), (float)(num3 + num / 3.0)),
        new Vector2((float)(num2 - size / 2.0), (float)(num3 + num / 3.0))
      }, size * 0.09);
      return new Path
      {
        Data = pathGeometry,
        Fill = Ui.Brush(fill),
        Width = size,
        Height = size,
        RenderTransform = new RotateTransform
        {
          Angle = rotationDegrees,
          CenterX = size / 2.0,
          CenterY = size / 2.0
        }
      };
    }

    // Token: 0x06000725 RID: 1829 RVA: 0x00535B38 File Offset: 0x00535B38
    private static PathGeometry Rounded(Vector2[] v, double r)
    {
      double num = r * Math.Sqrt(3.0);
      PathFigure pathFigure = new PathFigure
      {
        IsClosed = true,
        IsFilled = true
      };
      PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
      for (int i = 0; i < 3; i++)
      {
        Vector2 vector = v[(i + 2) % 3];
        Vector2 vector2 = v[i];
        Vector2 vector3 = v[(i + 1) % 3];
        Vector2 vector4 = Vector2.Normalize(vector2 - vector);
        Vector2 vector5 = Vector2.Normalize(vector3 - vector2);
        Vector2 vector6 = vector2 - vector4 * (float)num;
        Vector2 vector7 = vector2 + vector5 * (float)num;
        if (i == 0)
        {
          pathFigure.StartPoint = IntroMark.<Rounded>g__P|3_0(vector6);
        }
        else
        {
          pathSegmentCollection.Add(new LineSegment
          {
            Point = IntroMark.<Rounded>g__P|3_0(vector6)
          });
        }
        pathSegmentCollection.Add(new ArcSegment
        {
          Point = IntroMark.<Rounded>g__P|3_0(vector7),
          Size = new Size(r, r),
          SweepDirection = SweepDirection.Clockwise
        });
      }
      pathFigure.Segments = pathSegmentCollection;
      return new PathGeometry
      {
        Figures = { pathFigure }
      };
    }

    // Token: 0x06000726 RID: 1830 RVA: 0x00535C54 File Offset: 0x00535C54
    [CompilerGenerated]
    internal static Point <Rounded>g__P|3_0(Vector2 p)
    {
      return new Point(p.X, p.Y);
    }
  }
}
