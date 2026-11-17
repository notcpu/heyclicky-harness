using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Portraits
{
  // Token: 0x0200010B RID: 267
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PPath
  {
    // Token: 0x06000DF6 RID: 3574 RVA: 0x0056E978 File Offset: 0x0056E978
    public PPath M(double x, double y)
    {
      Vector2 vector = PPath.V(x, y);
      this.cmds.Add(new PPath.Cmd(PPath.K.M, vector, default(Vector2), default(Vector2)));
      this.current = (this.figureStart = vector);
      this.open = true;
      return this;
    }

    // Token: 0x06000DF7 RID: 3575 RVA: 0x0056E9CC File Offset: 0x0056E9CC
    public PPath L(double x, double y)
    {
      this.EnsureOpen();
      Vector2 vector = PPath.V(x, y);
      this.cmds.Add(new PPath.Cmd(PPath.K.L, vector, default(Vector2), default(Vector2)));
      this.current = vector;
      return this;
    }

    // Token: 0x06000DF8 RID: 3576 RVA: 0x0056EA14 File Offset: 0x0056EA14
    public PPath C(double x1, double y1, double x2, double y2, double x, double y)
    {
      this.EnsureOpen();
      this.cmds.Add(new PPath.Cmd(PPath.K.C, PPath.V(x1, y1), PPath.V(x2, y2), PPath.V(x, y)));
      this.current = PPath.V(x, y);
      return this;
    }

    // Token: 0x06000DF9 RID: 3577 RVA: 0x0056EA60 File Offset: 0x0056EA60
    public PPath Q(double cx, double cy, double x, double y)
    {
      this.EnsureOpen();
      this.cmds.Add(new PPath.Cmd(PPath.K.Q, PPath.V(cx, cy), PPath.V(x, y), default(Vector2)));
      this.current = PPath.V(x, y);
      return this;
    }

    // Token: 0x06000DFA RID: 3578 RVA: 0x0056EAAC File Offset: 0x0056EAAC
    public PPath Z()
    {
      if (this.open)
      {
        this.cmds.Add(new PPath.Cmd(PPath.K.Z, default(Vector2), default(Vector2), default(Vector2)));
        this.current = this.figureStart;
        this.open = false;
      }
      return this;
    }

    // Token: 0x06000DFB RID: 3579 RVA: 0x0056EB04 File Offset: 0x0056EB04
    private void EnsureOpen()
    {
      if (!this.open)
      {
        this.M((double)this.current.X, (double)this.current.Y);
      }
    }

    // Token: 0x06000DFC RID: 3580 RVA: 0x0056EB30 File Offset: 0x0056EB30
    private static Vector2 V(double x, double y)
    {
      return new Vector2((float)x, (float)y);
    }

    // Token: 0x06000DFD RID: 3581 RVA: 0x0056EB3C File Offset: 0x0056EB3C
    public PPath Ellipse(double x, double y, double w, double h)
    {
      double num = w / 2.0;
      double num2 = h / 2.0;
      double num3 = x + num;
      double num4 = y + num2;
      double num5 = num * 0.5522847498;
      double num6 = num2 * 0.5522847498;
      this.M(num3 + num, num4);
      this.C(num3 + num, num4 + num6, num3 + num5, num4 + num2, num3, num4 + num2);
      this.C(num3 - num5, num4 + num2, num3 - num, num4 + num6, num3 - num, num4);
      this.C(num3 - num, num4 - num6, num3 - num5, num4 - num2, num3, num4 - num2);
      this.C(num3 + num5, num4 - num2, num3 + num, num4 - num6, num3 + num, num4);
      return this.Z();
    }

    // Token: 0x06000DFE RID: 3582 RVA: 0x0056EBFC File Offset: 0x0056EBFC
    public PPath RoundedRect(double x, double y, double w, double h, double r)
    {
      r = Math.Min(r, Math.Min(w, h) / 2.0);
      double num = r * 0.4477152502;
      this.M(x + r, y);
      this.L(x + w - r, y);
      this.C(x + w - num, y, x + w, y + num, x + w, y + r);
      this.L(x + w, y + h - r);
      this.C(x + w, y + h - num, x + w - num, y + h, x + w - r, y + h);
      this.L(x + r, y + h);
      this.C(x + num, y + h, x, y + h - num, x, y + h - r);
      this.L(x, y + r);
      this.C(x, y + num, x + num, y, x + r, y);
      return this.Z();
    }

    // Token: 0x06000DFF RID: 3583 RVA: 0x0056ECEC File Offset: 0x0056ECEC
    public static PPath Oval(double x, double y, double w, double h)
    {
      return new PPath().Ellipse(x, y, w, h);
    }

    // Token: 0x06000E00 RID: 3584 RVA: 0x0056ECFC File Offset: 0x0056ECFC
    public static PPath Rounded(double x, double y, double w, double h, double r)
    {
      return new PPath().RoundedRect(x, y, w, h, r);
    }

    // Token: 0x06000E01 RID: 3585 RVA: 0x0056ED10 File Offset: 0x0056ED10
    public PPath Append(PPath other)
    {
      foreach (PPath.Cmd cmd in other.cmds)
      {
        this.cmds.Add(cmd);
      }
      this.current = other.current;
      this.figureStart = other.figureStart;
      this.open = other.open;
      return this;
    }

    // Token: 0x06000E02 RID: 3586 RVA: 0x0056ED90 File Offset: 0x0056ED90
    public PPath Transformed(Matrix3x2 m)
    {
      PPath ppath = new PPath();
      foreach (PPath.Cmd cmd in this.cmds)
      {
        ppath.cmds.Add(new PPath.Cmd(cmd.Kind, Vector2.Transform(cmd.A, m), Vector2.Transform(cmd.B, m), Vector2.Transform(cmd.C, m)));
      }
      ppath.current = Vector2.Transform(this.current, m);
      ppath.figureStart = Vector2.Transform(this.figureStart, m);
      ppath.open = this.open;
      return ppath;
    }

    // Token: 0x17000287 RID: 647
    // (get) Token: 0x06000E03 RID: 3587 RVA: 0x0056EE54 File Offset: 0x0056EE54
    public bool IsEmpty
    {
      get
      {
        return this.cmds.Count == 0;
      }
    }

    // Token: 0x06000E04 RID: 3588 RVA: 0x0056EE64 File Offset: 0x0056EE64
    public PathGeometry ToXaml()
    {
      PathGeometry pathGeometry = new PathGeometry
      {
        FillRule = FillRule.Nonzero
      };
      PathFigure pathFigure = null;
      foreach (PPath.Cmd cmd in this.cmds)
      {
        switch (cmd.Kind)
        {
        case PPath.K.M:
          pathFigure = new PathFigure
          {
            StartPoint = PPath.<ToXaml>g__P|22_0(cmd.A),
            IsFilled = true,
            IsClosed = false
          };
          pathGeometry.Figures.Add(pathFigure);
          break;
        case PPath.K.L:
          pathFigure.Segments.Add(new LineSegment
          {
            Point = PPath.<ToXaml>g__P|22_0(cmd.A)
          });
          break;
        case PPath.K.C:
          pathFigure.Segments.Add(new BezierSegment
          {
            Point1 = PPath.<ToXaml>g__P|22_0(cmd.A),
            Point2 = PPath.<ToXaml>g__P|22_0(cmd.B),
            Point3 = PPath.<ToXaml>g__P|22_0(cmd.C)
          });
          break;
        case PPath.K.Q:
          pathFigure.Segments.Add(new QuadraticBezierSegment
          {
            Point1 = PPath.<ToXaml>g__P|22_0(cmd.A),
            Point2 = PPath.<ToXaml>g__P|22_0(cmd.B)
          });
          break;
        case PPath.K.Z:
          if (pathFigure != null)
          {
            pathFigure.IsClosed = true;
          }
          break;
        }
      }
      return pathGeometry;
    }

    // Token: 0x06000E05 RID: 3589 RVA: 0x0056EFE4 File Offset: 0x0056EFE4
    public CanvasGeometry ToWin2D(ICanvasResourceCreator device)
    {
      CanvasGeometry canvasGeometry;
      using (CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(device))
      {
        canvasPathBuilder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
        bool flag = false;
        foreach (PPath.Cmd cmd in this.cmds)
        {
          switch (cmd.Kind)
          {
          case PPath.K.M:
            if (flag)
            {
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Open);
            }
            canvasPathBuilder.BeginFigure(cmd.A);
            flag = true;
            break;
          case PPath.K.L:
            canvasPathBuilder.AddLine(cmd.A);
            break;
          case PPath.K.C:
            canvasPathBuilder.AddCubicBezier(cmd.A, cmd.B, cmd.C);
            break;
          case PPath.K.Q:
            canvasPathBuilder.AddQuadraticBezier(cmd.A, cmd.B);
            break;
          case PPath.K.Z:
            if (flag)
            {
              canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
              flag = false;
            }
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

    // Token: 0x06000E06 RID: 3590 RVA: 0x0056F104 File Offset: 0x0056F104
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y", "W", "H" })]
    public ValueTuple<double, double, double, double> Bounds()
    {
      PPath.<>c__DisplayClass24_0 CS$<>8__locals1;
      CS$<>8__locals1.minX = double.MaxValue;
      CS$<>8__locals1.minY = double.MaxValue;
      CS$<>8__locals1.maxX = double.MinValue;
      CS$<>8__locals1.maxY = double.MinValue;
      Vector2 vector = default(Vector2);
      Vector2 vector2 = default(Vector2);
      foreach (PPath.Cmd cmd in this.cmds)
      {
        switch (cmd.Kind)
        {
        case PPath.K.M:
          vector2 = (vector = cmd.A);
          PPath.<Bounds>g__Add|24_0(vector, ref CS$<>8__locals1);
          break;
        case PPath.K.L:
          PPath.<Bounds>g__Add|24_0(cmd.A, ref CS$<>8__locals1);
          vector = cmd.A;
          break;
        case PPath.K.C:
        {
          Vector2 vector3 = vector;
          Vector2 a = cmd.A;
          Vector2 b = cmd.B;
          Vector2 c = cmd.C;
          PPath.<Bounds>g__Add|24_0(c, ref CS$<>8__locals1);
          foreach (double num in PPath.CubicExtrema((double)vector3.X, (double)a.X, (double)b.X, (double)c.X).Concat<double>(PPath.CubicExtrema((double)vector3.Y, (double)a.Y, (double)b.Y, (double)c.Y)))
          {
            float num2 = 1f - (float)num;
            PPath.<Bounds>g__Add|24_0(num2 * num2 * num2 * vector3 + 3f * num2 * num2 * (float)num * a + 3f * num2 * (float)(num * num) * b + (float)(num * num * num) * c, ref CS$<>8__locals1);
          }
          vector = c;
          break;
        }
        case PPath.K.Q:
        {
          Vector2 vector4 = vector;
          Vector2 a2 = cmd.A;
          Vector2 b2 = cmd.B;
          for (int i = 1; i <= 24; i++)
          {
            float num3 = (float)i / 24f;
            PPath.<Bounds>g__Add|24_0((1f - num3) * (1f - num3) * vector4 + 2f * (1f - num3) * num3 * a2 + num3 * num3 * b2, ref CS$<>8__locals1);
          }
          vector = b2;
          break;
        }
        case PPath.K.Z:
          vector = vector2;
          break;
        }
      }
      if (CS$<>8__locals1.minX <= CS$<>8__locals1.maxX)
      {
        return new ValueTuple<double, double, double, double>(CS$<>8__locals1.minX, CS$<>8__locals1.minY, CS$<>8__locals1.maxX - CS$<>8__locals1.minX, CS$<>8__locals1.maxY - CS$<>8__locals1.minY);
      }
      return new ValueTuple<double, double, double, double>(0.0, 0.0, 0.0, 0.0);
    }

    // Token: 0x06000E07 RID: 3591 RVA: 0x0056F424 File Offset: 0x0056F424
    private static IEnumerable<double> CubicExtrema(double p0, double p1, double p2, double p3)
    {
      PPath.<CubicExtrema>d__25 <CubicExtrema>d__ = new PPath.<CubicExtrema>d__25(-2);
      <CubicExtrema>d__.<>3__p0 = p0;
      <CubicExtrema>d__.<>3__p1 = p1;
      <CubicExtrema>d__.<>3__p2 = p2;
      <CubicExtrema>d__.<>3__p3 = p3;
      return <CubicExtrema>d__;
    }

    // Token: 0x06000E08 RID: 3592 RVA: 0x0056F44C File Offset: 0x0056F44C
    public static PPath ParseSvg(string data)
    {
      PPath.<>c__DisplayClass26_0 CS$<>8__locals1;
      CS$<>8__locals1.data = data;
      PPath ppath = new PPath();
      CS$<>8__locals1.i = 0;
      while (CS$<>8__locals1.i < CS$<>8__locals1.data.Length)
      {
        char c = CS$<>8__locals1.data[CS$<>8__locals1.i];
        if (char.IsWhiteSpace(c) || c == ',')
        {
          int num = CS$<>8__locals1.i;
          CS$<>8__locals1.i = num + 1;
        }
        else
        {
          int num = CS$<>8__locals1.i;
          CS$<>8__locals1.i = num + 1;
          if (c <= 'L')
          {
            if (c != 'C')
            {
              if (c == 'L')
              {
                for (;;)
                {
                  double? num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                  if (num2 == null)
                  {
                    break;
                  }
                  double valueOrDefault = num2.GetValueOrDefault();
                  num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                  if (num2 == null)
                  {
                    break;
                  }
                  double valueOrDefault2 = num2.GetValueOrDefault();
                  ppath.L(valueOrDefault, valueOrDefault2);
                }
              }
            }
            else
            {
              for (;;)
              {
                double? num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault3 = num2.GetValueOrDefault();
                num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault4 = num2.GetValueOrDefault();
                num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault5 = num2.GetValueOrDefault();
                num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault6 = num2.GetValueOrDefault();
                num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault7 = num2.GetValueOrDefault();
                num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
                if (num2 == null)
                {
                  break;
                }
                double valueOrDefault8 = num2.GetValueOrDefault();
                ppath.C(valueOrDefault3, valueOrDefault4, valueOrDefault5, valueOrDefault6, valueOrDefault7, valueOrDefault8);
              }
            }
          }
          else if (c != 'M')
          {
            if (c == 'Z' || c == 'z')
            {
              ppath.Z();
            }
          }
          else
          {
            for (;;)
            {
              double? num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
              if (num2 == null)
              {
                break;
              }
              double valueOrDefault9 = num2.GetValueOrDefault();
              num2 = PPath.<ParseSvg>g__Num|26_0(ref CS$<>8__locals1);
              if (num2 == null)
              {
                break;
              }
              double valueOrDefault10 = num2.GetValueOrDefault();
              ppath.M(valueOrDefault9, valueOrDefault10);
            }
          }
        }
      }
      return ppath;
    }

    // Token: 0x06000E0A RID: 3594 RVA: 0x0056F670 File Offset: 0x0056F670
    [CompilerGenerated]
    internal static Point <ToXaml>g__P|22_0(Vector2 v)
    {
      return new Point(v.X, v.Y);
    }

    // Token: 0x06000E0B RID: 3595 RVA: 0x0056F684 File Offset: 0x0056F684
    [CompilerGenerated]
    internal static void <Bounds>g__Add|24_0(Vector2 p, ref PPath.<>c__DisplayClass24_0 A_1)
    {
      A_1.minX = Math.Min(A_1.minX, (double)p.X);
      A_1.minY = Math.Min(A_1.minY, (double)p.Y);
      A_1.maxX = Math.Max(A_1.maxX, (double)p.X);
      A_1.maxY = Math.Max(A_1.maxY, (double)p.Y);
    }

    // Token: 0x06000E0C RID: 3596 RVA: 0x0056F6F4 File Offset: 0x0056F6F4
    [CompilerGenerated]
    internal static double? <ParseSvg>g__Num|26_0(ref PPath.<>c__DisplayClass26_0 A_0)
    {
      while (A_0.i < A_0.data.Length && (A_0.data[A_0.i] == ' ' || A_0.data[A_0.i] == ',' || A_0.data[A_0.i] == '\n' || A_0.data[A_0.i] == '\r' || A_0.data[A_0.i] == '\t'))
      {
        int num = A_0.i;
        A_0.i = num + 1;
      }
      int i = A_0.i;
      if (A_0.i < A_0.data.Length && (A_0.data[A_0.i] == '-' || A_0.data[A_0.i] == '+'))
      {
        int num = A_0.i;
        A_0.i = num + 1;
      }
      while (A_0.i < A_0.data.Length && (char.IsDigit(A_0.data[A_0.i]) || A_0.data[A_0.i] == '.'))
      {
        int num = A_0.i;
        A_0.i = num + 1;
      }
      if (A_0.i < A_0.data.Length && (A_0.data[A_0.i] == 'e' || A_0.data[A_0.i] == 'E'))
      {
        int num = A_0.i;
        A_0.i = num + 1;
        if (A_0.i < A_0.data.Length && (A_0.data[A_0.i] == '-' || A_0.data[A_0.i] == '+'))
        {
          num = A_0.i;
          A_0.i = num + 1;
        }
        while (A_0.i < A_0.data.Length && char.IsDigit(A_0.data[A_0.i]))
        {
          num = A_0.i;
          A_0.i = num + 1;
        }
      }
      if (i == A_0.i)
      {
        return null;
      }
      return new double?(double.Parse(A_0.data.AsSpan(i, A_0.i - i), CultureInfo.InvariantCulture));
    }

    // Token: 0x04000584 RID: 1412
    private readonly List<PPath.Cmd> cmds = new List<PPath.Cmd>();

    // Token: 0x04000585 RID: 1413
    private Vector2 current;

    // Token: 0x04000586 RID: 1414
    private Vector2 figureStart;

    // Token: 0x04000587 RID: 1415
    private bool open;

    // Token: 0x04000588 RID: 1416
    private const double Kappa = 0.5522847498;

    // Token: 0x020005E3 RID: 1507
    [NullableContext(0)]
    private enum K : byte
    {
      // Token: 0x04001B18 RID: 6936
      M,
      // Token: 0x04001B19 RID: 6937
      L,
      // Token: 0x04001B1A RID: 6938
      C,
      // Token: 0x04001B1B RID: 6939
      Q,
      // Token: 0x04001B1C RID: 6940
      Z
    }

    // Token: 0x020005E4 RID: 1508
    [NullableContext(0)]
    private readonly struct Cmd : IEquatable<PPath.Cmd>
    {
      // Token: 0x06002FF8 RID: 12280 RVA: 0x006663B8 File Offset: 0x006663B8
      public Cmd(PPath.K Kind, Vector2 A, Vector2 B, Vector2 C)
      {
        this.Kind = Kind;
        this.A = A;
        this.B = B;
        this.C = C;
      }

      // Token: 0x17000757 RID: 1879
      // (get) Token: 0x06002FF9 RID: 12281 RVA: 0x006663D8 File Offset: 0x006663D8
      // (set) Token: 0x06002FFA RID: 12282 RVA: 0x006663E0 File Offset: 0x006663E0
      public PPath.K Kind { get; set; }

      // Token: 0x17000758 RID: 1880
      // (get) Token: 0x06002FFB RID: 12283 RVA: 0x006663EC File Offset: 0x006663EC
      // (set) Token: 0x06002FFC RID: 12284 RVA: 0x006663F4 File Offset: 0x006663F4
      public Vector2 A { get; set; }

      // Token: 0x17000759 RID: 1881
      // (get) Token: 0x06002FFD RID: 12285 RVA: 0x00666400 File Offset: 0x00666400
      // (set) Token: 0x06002FFE RID: 12286 RVA: 0x00666408 File Offset: 0x00666408
      public Vector2 B { get; set; }

      // Token: 0x1700075A RID: 1882
      // (get) Token: 0x06002FFF RID: 12287 RVA: 0x00666414 File Offset: 0x00666414
      // (set) Token: 0x06003000 RID: 12288 RVA: 0x0066641C File Offset: 0x0066641C
      public Vector2 C { get; set; }

      // Token: 0x06003001 RID: 12289 RVA: 0x00666428 File Offset: 0x00666428
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Cmd");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003002 RID: 12290 RVA: 0x00666474 File Offset: 0x00666474
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

      // Token: 0x06003003 RID: 12291 RVA: 0x00666520 File Offset: 0x00666520
      [CompilerGenerated]
      public static bool operator !=(PPath.Cmd left, PPath.Cmd right)
      {
        return !(left == right);
      }

      // Token: 0x06003004 RID: 12292 RVA: 0x0066652C File Offset: 0x0066652C
      [CompilerGenerated]
      public static bool operator ==(PPath.Cmd left, PPath.Cmd right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003005 RID: 12293 RVA: 0x00666538 File Offset: 0x00666538
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<PPath.K>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<A>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<B>k__BackingField)) * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(this.<C>k__BackingField);
      }

      // Token: 0x06003006 RID: 12294 RVA: 0x0066659C File Offset: 0x0066659C
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is PPath.Cmd && this.Equals((PPath.Cmd)obj);
      }

      // Token: 0x06003007 RID: 12295 RVA: 0x006665B4 File Offset: 0x006665B4
      [CompilerGenerated]
      public bool Equals(PPath.Cmd other)
      {
        return EqualityComparer<PPath.K>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<A>k__BackingField, other.<A>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<B>k__BackingField, other.<B>k__BackingField) && EqualityComparer<Vector2>.Default.Equals(this.<C>k__BackingField, other.<C>k__BackingField);
      }

      // Token: 0x06003008 RID: 12296 RVA: 0x00666624 File Offset: 0x00666624
      [CompilerGenerated]
      public void Deconstruct(out PPath.K Kind, out Vector2 A, out Vector2 B, out Vector2 C)
      {
        Kind = this.Kind;
        A = this.A;
        B = this.B;
        C = this.C;
      }
    }
  }
}
