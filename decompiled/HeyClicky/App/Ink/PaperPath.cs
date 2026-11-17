using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FF RID: 511
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperPath
  {
    // Token: 0x1700044F RID: 1103
    // (get) Token: 0x06001869 RID: 6249 RVA: 0x005CB718 File Offset: 0x005CB718
    public bool IsEmpty
    {
      get
      {
        return this.commands.Count == 0;
      }
    }

    // Token: 0x17000450 RID: 1104
    // (get) Token: 0x0600186A RID: 6250 RVA: 0x005CB728 File Offset: 0x005CB728
    public Point CurrentPoint
    {
      get
      {
        return this.current;
      }
    }

    // Token: 0x0600186B RID: 6251 RVA: 0x005CB730 File Offset: 0x005CB730
    public PaperPath MoveTo(double x, double y)
    {
      return this.MoveTo(new Point(x, y));
    }

    // Token: 0x0600186C RID: 6252 RVA: 0x005CB740 File Offset: 0x005CB740
    public PaperPath MoveTo(Point p)
    {
      this.commands.Add(new PaperPath.Command(PaperPath.Kind.Move, p, default(Point), default(Point)));
      this.subpathStart = p;
      this.current = p;
      this.hasCurrent = true;
      return this;
    }

    // Token: 0x0600186D RID: 6253 RVA: 0x005CB78C File Offset: 0x005CB78C
    public PaperPath LineTo(double x, double y)
    {
      return this.LineTo(new Point(x, y));
    }

    // Token: 0x0600186E RID: 6254 RVA: 0x005CB79C File Offset: 0x005CB79C
    public PaperPath LineTo(Point p)
    {
      if (!this.hasCurrent)
      {
        return this.MoveTo(p);
      }
      this.commands.Add(new PaperPath.Command(PaperPath.Kind.Line, p, default(Point), default(Point)));
      this.current = p;
      return this;
    }

    // Token: 0x0600186F RID: 6255 RVA: 0x005CB7E8 File Offset: 0x005CB7E8
    public PaperPath QuadTo(Point to, Point control)
    {
      if (!this.hasCurrent)
      {
        this.MoveTo(control);
      }
      this.commands.Add(new PaperPath.Command(PaperPath.Kind.Quad, control, to, default(Point)));
      this.current = to;
      return this;
    }

    // Token: 0x06001870 RID: 6256 RVA: 0x005CB82C File Offset: 0x005CB82C
    public PaperPath CubicTo(Point to, Point control1, Point control2)
    {
      if (!this.hasCurrent)
      {
        this.MoveTo(control1);
      }
      this.commands.Add(new PaperPath.Command(PaperPath.Kind.Cubic, control1, control2, to));
      this.current = to;
      return this;
    }

    // Token: 0x06001871 RID: 6257 RVA: 0x005CB85C File Offset: 0x005CB85C
    public PaperPath Close()
    {
      if (!this.hasCurrent)
      {
        return this;
      }
      this.commands.Add(new PaperPath.Command(PaperPath.Kind.Close, default(Point), default(Point), default(Point)));
      this.current = this.subpathStart;
      return this;
    }

    // Token: 0x06001872 RID: 6258 RVA: 0x005CB8AC File Offset: 0x005CB8AC
    public PaperPath AddLines(IReadOnlyList<Point> points)
    {
      if (points.Count == 0)
      {
        return this;
      }
      this.MoveTo(points[0]);
      for (int i = 1; i < points.Count; i++)
      {
        this.LineTo(points[i]);
      }
      return this;
    }

    // Token: 0x06001873 RID: 6259 RVA: 0x005CB8F4 File Offset: 0x005CB8F4
    public PaperPath AddArc(Point center, double radius, double startAngle, double endAngle, bool clockwise)
    {
      double num = endAngle - startAngle;
      if (clockwise)
      {
        while (num > 0.0)
        {
          num -= 6.283185307179586;
        }
        while (num <= -6.283185307179586)
        {
          num += 6.283185307179586;
        }
      }
      else
      {
        while (num < 0.0)
        {
          num += 6.283185307179586;
        }
        while (num >= 6.283185307179586)
        {
          num -= 6.283185307179586;
        }
      }
      Point point = new Point(center.X + radius * Math.Cos(startAngle), center.Y + radius * Math.Sin(startAngle));
      if (this.hasCurrent)
      {
        this.LineTo(point);
      }
      else
      {
        this.MoveTo(point);
      }
      this.AppendArcBeziers(center, radius, radius, 0.0, startAngle, num);
      return this;
    }

    // Token: 0x06001874 RID: 6260 RVA: 0x005CB9C8 File Offset: 0x005CB9C8
    public PaperPath AddEllipse(Rect rect)
    {
      Point point = new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0);
      double num = rect.Width / 2.0;
      double num2 = rect.Height / 2.0;
      this.MoveTo(new Point(point.X + num, point.Y));
      this.AppendArcBeziers(point, num, num2, 0.0, 0.0, 6.283185307179586);
      return this.Close();
    }

    // Token: 0x06001875 RID: 6261 RVA: 0x005CBA78 File Offset: 0x005CBA78
    public PaperPath AddRoundedRect(Rect rect, double radius)
    {
      double num = Math.Max(0.0, Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2.0));
      if (num <= 0.0)
      {
        return this.AddRect(rect);
      }
      double x = rect.X;
      double y = rect.Y;
      double width = rect.Width;
      double height = rect.Height;
      this.MoveTo(new Point(x + num, y));
      this.LineTo(new Point(x + width - num, y));
      this.AppendArcBeziers(new Point(x + width - num, y + num), num, num, 0.0, -1.5707963267948966, 1.5707963267948966);
      this.LineTo(new Point(x + width, y + height - num));
      this.AppendArcBeziers(new Point(x + width - num, y + height - num), num, num, 0.0, 0.0, 1.5707963267948966);
      this.LineTo(new Point(x + num, y + height));
      this.AppendArcBeziers(new Point(x + num, y + height - num), num, num, 0.0, 1.5707963267948966, 1.5707963267948966);
      this.LineTo(new Point(x, y + num));
      this.AppendArcBeziers(new Point(x + num, y + num), num, num, 0.0, 3.141592653589793, 1.5707963267948966);
      return this.Close();
    }

    // Token: 0x06001876 RID: 6262 RVA: 0x005CBC10 File Offset: 0x005CBC10
    public PaperPath AddUnevenRoundedRect(Rect rect, double topLeading, double bottomLeading, double bottomTrailing, double topTrailing)
    {
      double x = rect.X;
      double y = rect.Y;
      double width = rect.Width;
      double height = rect.Height;
      double num = Math.Min(width, height) / 2.0;
      double num2 = Math.Min(topLeading, num);
      double num3 = Math.Min(bottomLeading, num);
      double num4 = Math.Min(bottomTrailing, num);
      double num5 = Math.Min(topTrailing, num);
      this.MoveTo(new Point(x + num2, y));
      this.LineTo(new Point(x + width - num5, y));
      if (num5 > 0.0)
      {
        this.AppendArcBeziers(new Point(x + width - num5, y + num5), num5, num5, 0.0, -1.5707963267948966, 1.5707963267948966);
      }
      this.LineTo(new Point(x + width, y + height - num4));
      if (num4 > 0.0)
      {
        this.AppendArcBeziers(new Point(x + width - num4, y + height - num4), num4, num4, 0.0, 0.0, 1.5707963267948966);
      }
      this.LineTo(new Point(x + num3, y + height));
      if (num3 > 0.0)
      {
        this.AppendArcBeziers(new Point(x + num3, y + height - num3), num3, num3, 0.0, 1.5707963267948966, 1.5707963267948966);
      }
      this.LineTo(new Point(x, y + num2));
      if (num2 > 0.0)
      {
        this.AppendArcBeziers(new Point(x + num2, y + num2), num2, num2, 0.0, 3.141592653589793, 1.5707963267948966);
      }
      return this.Close();
    }

    // Token: 0x06001877 RID: 6263 RVA: 0x005CBDE4 File Offset: 0x005CBDE4
    public PaperPath AddRect(Rect rect)
    {
      this.MoveTo(new Point(rect.X, rect.Y));
      this.LineTo(new Point(rect.X + rect.Width, rect.Y));
      this.LineTo(new Point(rect.X + rect.Width, rect.Y + rect.Height));
      this.LineTo(new Point(rect.X, rect.Y + rect.Height));
      return this.Close();
    }

    // Token: 0x06001878 RID: 6264 RVA: 0x005CBE80 File Offset: 0x005CBE80
    public PaperPath AddCapsule(Rect rect)
    {
      return this.AddRoundedRect(rect, Math.Min(rect.Width, rect.Height) / 2.0);
    }

    // Token: 0x06001879 RID: 6265 RVA: 0x005CBEA8 File Offset: 0x005CBEA8
    public PaperPath Transformed(double scaleX, double scaleY, double dx = 0.0, double dy = 0.0)
    {
      PaperPath.<>c__DisplayClass24_0 CS$<>8__locals1;
      CS$<>8__locals1.scaleX = scaleX;
      CS$<>8__locals1.dx = dx;
      CS$<>8__locals1.scaleY = scaleY;
      CS$<>8__locals1.dy = dy;
      PaperPath paperPath = new PaperPath();
      foreach (PaperPath.Command command in this.commands)
      {
        switch (command.Kind)
        {
        case PaperPath.Kind.Move:
          paperPath.MoveTo(PaperPath.<Transformed>g__T|24_0(command.A, ref CS$<>8__locals1));
          break;
        case PaperPath.Kind.Line:
          paperPath.LineTo(PaperPath.<Transformed>g__T|24_0(command.A, ref CS$<>8__locals1));
          break;
        case PaperPath.Kind.Quad:
          paperPath.QuadTo(PaperPath.<Transformed>g__T|24_0(command.B, ref CS$<>8__locals1), PaperPath.<Transformed>g__T|24_0(command.A, ref CS$<>8__locals1));
          break;
        case PaperPath.Kind.Cubic:
          paperPath.CubicTo(PaperPath.<Transformed>g__T|24_0(command.C, ref CS$<>8__locals1), PaperPath.<Transformed>g__T|24_0(command.A, ref CS$<>8__locals1), PaperPath.<Transformed>g__T|24_0(command.B, ref CS$<>8__locals1));
          break;
        case PaperPath.Kind.Close:
          paperPath.Close();
          break;
        }
      }
      return paperPath;
    }

    // Token: 0x0600187A RID: 6266 RVA: 0x005CBFD8 File Offset: 0x005CBFD8
    public PaperPath Append(PaperPath other)
    {
      foreach (PaperPath.Command command in other.commands)
      {
        switch (command.Kind)
        {
        case PaperPath.Kind.Move:
          this.MoveTo(command.A);
          break;
        case PaperPath.Kind.Line:
          this.LineTo(command.A);
          break;
        case PaperPath.Kind.Quad:
          this.QuadTo(command.B, command.A);
          break;
        case PaperPath.Kind.Cubic:
          this.CubicTo(command.C, command.A, command.B);
          break;
        case PaperPath.Kind.Close:
          this.Close();
          break;
        }
      }
      return this;
    }

    // Token: 0x0600187B RID: 6267 RVA: 0x005CC0AC File Offset: 0x005CC0AC
    private void AppendArcBeziers(Point center, double rx, double ry, double rotation, double startAngle, double sweep)
    {
      PaperPath.<>c__DisplayClass26_0 CS$<>8__locals1;
      CS$<>8__locals1.center = center;
      CS$<>8__locals1.rx = rx;
      CS$<>8__locals1.ry = ry;
      if (Math.Abs(sweep) < 1E-12)
      {
        return;
      }
      int num = Math.Max(1, (int)Math.Ceiling(Math.Abs(sweep) / 1.5707963267948966 - 1E-09));
      double num2 = sweep / (double)num;
      double num3 = 1.3333333333333333 * Math.Tan(num2 / 4.0);
      CS$<>8__locals1.cosPhi = Math.Cos(rotation);
      CS$<>8__locals1.sinPhi = Math.Sin(rotation);
      double num4 = startAngle;
      for (int i = 0; i < num; i++)
      {
        double num5 = num4 + num2;
        Point point = PaperPath.<AppendArcBeziers>g__On|26_0(num4, ref CS$<>8__locals1);
        Point point2 = PaperPath.<AppendArcBeziers>g__On|26_0(num5, ref CS$<>8__locals1);
        Point point3 = PaperPath.<AppendArcBeziers>g__D|26_1(num4, ref CS$<>8__locals1);
        Point point4 = PaperPath.<AppendArcBeziers>g__D|26_1(num5, ref CS$<>8__locals1);
        this.CubicTo(point2, new Point(point.X + num3 * point3.X, point.Y + num3 * point3.Y), new Point(point2.X - num3 * point4.X, point2.Y - num3 * point4.Y));
        num4 = num5;
      }
    }

    // Token: 0x0600187C RID: 6268 RVA: 0x005CC1F0 File Offset: 0x005CC1F0
    [return: TupleElementNames(new string[] { "Points", "Closed" })]
    [return: Nullable(new byte[] { 1, 0, 1 })]
    public List<ValueTuple<List<Point>, bool>> Flattened()
    {
      PaperPath.<>c__DisplayClass27_0 CS$<>8__locals1;
      CS$<>8__locals1.result = new List<ValueTuple<List<Point>, bool>>();
      CS$<>8__locals1.pts = null;
      CS$<>8__locals1.last = default(Point);
      CS$<>8__locals1.start = default(Point);
      foreach (PaperPath.Command command in this.commands)
      {
        switch (command.Kind)
        {
        case PaperPath.Kind.Move:
          PaperPath.<Flattened>g__Begin|27_0(command.A, ref CS$<>8__locals1);
          break;
        case PaperPath.Kind.Line:
          if (CS$<>8__locals1.pts == null)
          {
            PaperPath.<Flattened>g__Begin|27_0(CS$<>8__locals1.last, ref CS$<>8__locals1);
          }
          CS$<>8__locals1.pts.Add(command.A);
          CS$<>8__locals1.last = command.A;
          break;
        case PaperPath.Kind.Quad:
        {
          if (CS$<>8__locals1.pts == null)
          {
            PaperPath.<Flattened>g__Begin|27_0(CS$<>8__locals1.last, ref CS$<>8__locals1);
          }
          Point last = CS$<>8__locals1.last;
          int num = PaperPath.<Flattened>g__Steps|27_2(PaperPath.<Flattened>g__Distance|27_1(last, command.A) + PaperPath.<Flattened>g__Distance|27_1(command.A, command.B));
          for (int i = 1; i <= num; i++)
          {
            double num2 = (double)i / (double)num;
            double num3 = 1.0 - num2;
            CS$<>8__locals1.pts.Add(new Point(num3 * num3 * last.X + 2.0 * num3 * num2 * command.A.X + num2 * num2 * command.B.X, num3 * num3 * last.Y + 2.0 * num3 * num2 * command.A.Y + num2 * num2 * command.B.Y));
          }
          CS$<>8__locals1.last = command.B;
          break;
        }
        case PaperPath.Kind.Cubic:
        {
          if (CS$<>8__locals1.pts == null)
          {
            PaperPath.<Flattened>g__Begin|27_0(CS$<>8__locals1.last, ref CS$<>8__locals1);
          }
          Point last2 = CS$<>8__locals1.last;
          int num4 = PaperPath.<Flattened>g__Steps|27_2(PaperPath.<Flattened>g__Distance|27_1(last2, command.A) + PaperPath.<Flattened>g__Distance|27_1(command.A, command.B) + PaperPath.<Flattened>g__Distance|27_1(command.B, command.C));
          for (int j = 1; j <= num4; j++)
          {
            double num5 = (double)j / (double)num4;
            double num6 = 1.0 - num5;
            double num7 = num6 * num6 * num6;
            double num8 = 3.0 * num6 * num6 * num5;
            double num9 = 3.0 * num6 * num5 * num5;
            double num10 = num5 * num5 * num5;
            CS$<>8__locals1.pts.Add(new Point(num7 * last2.X + num8 * command.A.X + num9 * command.B.X + num10 * command.C.X, num7 * last2.Y + num8 * command.A.Y + num9 * command.B.Y + num10 * command.C.Y));
          }
          CS$<>8__locals1.last = command.C;
          break;
        }
        case PaperPath.Kind.Close:
          if (CS$<>8__locals1.pts != null && CS$<>8__locals1.pts.Count > 0)
          {
            CS$<>8__locals1.pts.Add(CS$<>8__locals1.start);
            CS$<>8__locals1.result.Add(new ValueTuple<List<Point>, bool>(CS$<>8__locals1.pts, true));
          }
          CS$<>8__locals1.pts = null;
          CS$<>8__locals1.last = CS$<>8__locals1.start;
          break;
        }
      }
      if (CS$<>8__locals1.pts != null && CS$<>8__locals1.pts.Count > 0)
      {
        CS$<>8__locals1.result.Add(new ValueTuple<List<Point>, bool>(CS$<>8__locals1.pts, false));
      }
      return CS$<>8__locals1.result;
    }

    // Token: 0x17000451 RID: 1105
    // (get) Token: 0x0600187D RID: 6269 RVA: 0x005CC608 File Offset: 0x005CC608
    public double Length
    {
      get
      {
        return this.Flattened().Sum<ValueTuple<List<Point>, bool>>(([TupleElementNames(new string[] { "Points", "Closed" })] ValueTuple<List<Point>, bool> s) => PaperPath.PolylineLength(s.Item1));
      }
    }

    // Token: 0x0600187E RID: 6270 RVA: 0x005CC634 File Offset: 0x005CC634
    private static double PolylineLength(List<Point> points)
    {
      double num = 0.0;
      for (int i = 1; i < points.Count; i++)
      {
        num += Math.Sqrt(Math.Pow(points[i].X - points[i - 1].X, 2.0) + Math.Pow(points[i].Y - points[i - 1].Y, 2.0));
      }
      return num;
    }

    // Token: 0x0600187F RID: 6271 RVA: 0x005CC6C4 File Offset: 0x005CC6C4
    public PaperPath Trimmed(double from, double to)
    {
      from = Math.Clamp(from, 0.0, 1.0);
      to = Math.Clamp(to, 0.0, 1.0);
      PaperPath paperPath = new PaperPath();
      if (to <= from)
      {
        return paperPath;
      }
      List<ValueTuple<List<Point>, bool>> list = this.Flattened();
      double num = list.Sum<ValueTuple<List<Point>, bool>>(([TupleElementNames(new string[] { "Points", "Closed" })] ValueTuple<List<Point>, bool> s) => PaperPath.PolylineLength(s.Item1));
      if (num > 0.0)
      {
        double num2 = from * num;
        double num3 = to * num;
        double num4 = 0.0;
        foreach (ValueTuple<List<Point>, bool> valueTuple in list)
        {
          List<Point> item = valueTuple.Item1;
          bool flag = false;
          for (int i = 1; i < item.Count; i++)
          {
            Point point = item[i - 1];
            Point point2 = item[i];
            double num5 = Math.Sqrt((point2.X - point.X) * (point2.X - point.X) + (point2.Y - point.Y) * (point2.Y - point.Y));
            double num6 = num4;
            if ((num4 += num5) < num2 || num6 > num3 || num5 <= 0.0)
            {
              if (num6 > num3)
              {
                return paperPath;
              }
            }
            else
            {
              double num7 = Math.Max(0.0, (num2 - num6) / num5);
              double num8 = Math.Min(1.0, (num3 - num6) / num5);
              Point point3 = new Point(point.X + (point2.X - point.X) * num7, point.Y + (point2.Y - point.Y) * num7);
              Point point4 = new Point(point.X + (point2.X - point.X) * num8, point.Y + (point2.Y - point.Y) * num8);
              if (!flag)
              {
                paperPath.MoveTo(point3);
                flag = true;
              }
              paperPath.LineTo(point4);
            }
          }
        }
        return paperPath;
      }
      if (from > 0.0 || to < 1.0)
      {
        return paperPath;
      }
      return this;
    }

    // Token: 0x06001880 RID: 6272 RVA: 0x005CC940 File Offset: 0x005CC940
    internal void AppendSvgArcBeziers(Point center, double rx, double ry, double rotation, double startAngle, double sweep)
    {
      this.AppendArcBeziers(center, rx, ry, rotation, startAngle, sweep);
    }

    // Token: 0x06001881 RID: 6273 RVA: 0x005CC954 File Offset: 0x005CC954
    public Geometry ToGeometry()
    {
      PathGeometry pathGeometry = new PathGeometry
      {
        FillRule = FillRule.Nonzero
      };
      PathFigure pathFigure = null;
      foreach (PaperPath.Command command in this.commands)
      {
        switch (command.Kind)
        {
        case PaperPath.Kind.Move:
          pathFigure = new PathFigure
          {
            StartPoint = command.A,
            IsFilled = true,
            IsClosed = false
          };
          pathGeometry.Figures.Add(pathFigure);
          break;
        case PaperPath.Kind.Line:
          pathFigure.Segments.Add(new LineSegment
          {
            Point = command.A
          });
          break;
        case PaperPath.Kind.Quad:
          pathFigure.Segments.Add(new QuadraticBezierSegment
          {
            Point1 = command.A,
            Point2 = command.B
          });
          break;
        case PaperPath.Kind.Cubic:
          pathFigure.Segments.Add(new BezierSegment
          {
            Point1 = command.A,
            Point2 = command.B,
            Point3 = command.C
          });
          break;
        case PaperPath.Kind.Close:
          if (pathFigure != null)
          {
            pathFigure.IsClosed = true;
          }
          pathFigure = null;
          break;
        }
      }
      return pathGeometry;
    }

    // Token: 0x06001882 RID: 6274 RVA: 0x005CCAB0 File Offset: 0x005CCAB0
    public CanvasGeometry ToCanvasGeometry(ICanvasResourceCreator creator)
    {
      CanvasGeometry canvasGeometry;
      using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(creator))
      {
        canvasPathBuilder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
        bool flag = false;
        foreach (PaperPath.Command command in this.commands)
        {
          switch (command.Kind)
          {
          case PaperPath.Kind.Move:
          {
            if (flag)
            {
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
            }
            canvasPathBuilder.BeginFigure(PaperPath.<ToCanvasGeometry>g__V|34_0(command.A));
            flag = true;
            Point a = command.A;
            break;
          }
          case PaperPath.Kind.Line:
          {
            canvasPathBuilder.AddLine(PaperPath.<ToCanvasGeometry>g__V|34_0(command.A));
            Point a2 = command.A;
            break;
          }
          case PaperPath.Kind.Quad:
          {
            canvasPathBuilder.AddQuadraticBezier(PaperPath.<ToCanvasGeometry>g__V|34_0(command.A), PaperPath.<ToCanvasGeometry>g__V|34_0(command.B));
            Point b = command.B;
            break;
          }
          case PaperPath.Kind.Cubic:
          {
            canvasPathBuilder.AddCubicBezier(PaperPath.<ToCanvasGeometry>g__V|34_0(command.A), PaperPath.<ToCanvasGeometry>g__V|34_0(command.B), PaperPath.<ToCanvasGeometry>g__V|34_0(command.C));
            Point c = command.C;
            break;
          }
          case PaperPath.Kind.Close:
            if (flag)
            {
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
            }
            flag = false;
            break;
          }
        }
        if (flag)
        {
          canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
        }
        canvasGeometry = CanvasGeometry.CreatePath(canvasPathBuilder);
      }
      return canvasGeometry;
    }

    // Token: 0x06001884 RID: 6276 RVA: 0x005CCC44 File Offset: 0x005CCC44
    [CompilerGenerated]
    internal static Point <Transformed>g__T|24_0(Point p, ref PaperPath.<>c__DisplayClass24_0 A_1)
    {
      return new Point(p.X * A_1.scaleX + A_1.dx, p.Y * A_1.scaleY + A_1.dy);
    }

    // Token: 0x06001885 RID: 6277 RVA: 0x005CCC78 File Offset: 0x005CCC78
    [CompilerGenerated]
    internal static Point <AppendArcBeziers>g__On|26_0(double t, ref PaperPath.<>c__DisplayClass26_0 A_1)
    {
      return new Point(A_1.center.X + A_1.rx * Math.Cos(t) * A_1.cosPhi - A_1.ry * Math.Sin(t) * A_1.sinPhi, A_1.center.Y + A_1.rx * Math.Cos(t) * A_1.sinPhi + A_1.ry * Math.Sin(t) * A_1.cosPhi);
    }

    // Token: 0x06001886 RID: 6278 RVA: 0x005CCCF4 File Offset: 0x005CCCF4
    [CompilerGenerated]
    internal static Point <AppendArcBeziers>g__D|26_1(double t, ref PaperPath.<>c__DisplayClass26_0 A_1)
    {
      return new Point(-A_1.rx * Math.Sin(t) * A_1.cosPhi - A_1.ry * Math.Cos(t) * A_1.sinPhi, -A_1.rx * Math.Sin(t) * A_1.sinPhi + A_1.ry * Math.Cos(t) * A_1.cosPhi);
    }

    // Token: 0x06001887 RID: 6279 RVA: 0x005CCD5C File Offset: 0x005CCD5C
    [CompilerGenerated]
    internal unsafe static void <Flattened>g__Begin|27_0(Point p, ref PaperPath.<>c__DisplayClass27_0 A_1)
    {
      if (A_1.pts != null && A_1.pts.Count > 0)
      {
        A_1.result.Add(new ValueTuple<List<Point>, bool>(A_1.pts, false));
      }
      int num = 1;
      List<Point> list = new List<Point>(num);
      CollectionsMarshal.SetCount<Point>(list, num);
      Span<Point> span = CollectionsMarshal.AsSpan<Point>(list);
      int num2 = 0;
      *span[num2] = p;
      A_1.pts = list;
      A_1.start = p;
      A_1.last = p;
    }

    // Token: 0x06001888 RID: 6280 RVA: 0x005CCDD4 File Offset: 0x005CCDD4
    [CompilerGenerated]
    internal static double <Flattened>g__Distance|27_1(Point a, Point b)
    {
      return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
    }

    // Token: 0x06001889 RID: 6281 RVA: 0x005CCE28 File Offset: 0x005CCE28
    [CompilerGenerated]
    internal static int <Flattened>g__Steps|27_2(double hull)
    {
      return Math.Clamp((int)Math.Ceiling(hull / 2.0), 4, 64);
    }

    // Token: 0x0600188A RID: 6282 RVA: 0x005CCE44 File Offset: 0x005CCE44
    [CompilerGenerated]
    internal static Vector2 <ToCanvasGeometry>g__V|34_0(Point p)
    {
      return new Vector2((float)p.X, (float)p.Y);
    }

    // Token: 0x04000C11 RID: 3089
    private readonly List<PaperPath.Command> commands = new List<PaperPath.Command>();

    // Token: 0x04000C12 RID: 3090
    private Point current;

    // Token: 0x04000C13 RID: 3091
    private Point subpathStart;

    // Token: 0x04000C14 RID: 3092
    private bool hasCurrent;

    // Token: 0x0200079A RID: 1946
    [NullableContext(0)]
    private enum Kind
    {
      // Token: 0x04002109 RID: 8457
      Move,
      // Token: 0x0400210A RID: 8458
      Line,
      // Token: 0x0400210B RID: 8459
      Quad,
      // Token: 0x0400210C RID: 8460
      Cubic,
      // Token: 0x0400210D RID: 8461
      Close
    }

    // Token: 0x0200079B RID: 1947
    [NullableContext(0)]
    private readonly struct Command : IEquatable<PaperPath.Command>
    {
      // Token: 0x0600362C RID: 13868 RVA: 0x0067DE48 File Offset: 0x0067DE48
      public Command(PaperPath.Kind Kind, Point A, Point B, Point C)
      {
        this.Kind = Kind;
        this.A = A;
        this.B = B;
        this.C = C;
      }

      // Token: 0x170007D3 RID: 2003
      // (get) Token: 0x0600362D RID: 13869 RVA: 0x0067DE68 File Offset: 0x0067DE68
      // (set) Token: 0x0600362E RID: 13870 RVA: 0x0067DE70 File Offset: 0x0067DE70
      public PaperPath.Kind Kind { get; set; }

      // Token: 0x170007D4 RID: 2004
      // (get) Token: 0x0600362F RID: 13871 RVA: 0x0067DE7C File Offset: 0x0067DE7C
      // (set) Token: 0x06003630 RID: 13872 RVA: 0x0067DE84 File Offset: 0x0067DE84
      public Point A { get; set; }

      // Token: 0x170007D5 RID: 2005
      // (get) Token: 0x06003631 RID: 13873 RVA: 0x0067DE90 File Offset: 0x0067DE90
      // (set) Token: 0x06003632 RID: 13874 RVA: 0x0067DE98 File Offset: 0x0067DE98
      public Point B { get; set; }

      // Token: 0x170007D6 RID: 2006
      // (get) Token: 0x06003633 RID: 13875 RVA: 0x0067DEA4 File Offset: 0x0067DEA4
      // (set) Token: 0x06003634 RID: 13876 RVA: 0x0067DEAC File Offset: 0x0067DEAC
      public Point C { get; set; }

      // Token: 0x06003635 RID: 13877 RVA: 0x0067DEB8 File Offset: 0x0067DEB8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Command");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003636 RID: 13878 RVA: 0x0067DF04 File Offset: 0x0067DF04
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", A = ");
        builder.Append(this.A.ToString());
        builder.Append(", B = ");
        builder.Append(this.B.ToString());
        builder.Append(", C = ");
        builder.Append(this.C.ToString());
        return true;
      }

      // Token: 0x06003637 RID: 13879 RVA: 0x0067DFB0 File Offset: 0x0067DFB0
      [CompilerGenerated]
      public static bool operator !=(PaperPath.Command left, PaperPath.Command right)
      {
        return !(left == right);
      }

      // Token: 0x06003638 RID: 13880 RVA: 0x0067DFBC File Offset: 0x0067DFBC
      [CompilerGenerated]
      public static bool operator ==(PaperPath.Command left, PaperPath.Command right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003639 RID: 13881 RVA: 0x0067DFC8 File Offset: 0x0067DFC8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<PaperPath.Kind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<A>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<B>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<C>k__BackingField);
      }

      // Token: 0x0600363A RID: 13882 RVA: 0x0067E02C File Offset: 0x0067E02C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is PaperPath.Command && this.Equals((PaperPath.Command)obj);
      }

      // Token: 0x0600363B RID: 13883 RVA: 0x0067E044 File Offset: 0x0067E044
      [CompilerGenerated]
      public bool Equals(PaperPath.Command other)
      {
        return EqualityComparer<PaperPath.Kind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<A>k__BackingField, other.<A>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<B>k__BackingField, other.<B>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<C>k__BackingField, other.<C>k__BackingField);
      }

      // Token: 0x0600363C RID: 13884 RVA: 0x0067E0B4 File Offset: 0x0067E0B4
      [CompilerGenerated]
      public void Deconstruct(out PaperPath.Kind Kind, out Point A, out Point B, out Point C)
      {
        Kind = this.Kind;
        A = this.A;
        B = this.B;
        C = this.C;
      }
    }
  }
}
