using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C5 RID: 453
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickySVGPath
  {
    // Token: 0x060017B9 RID: 6073 RVA: 0x005C4CF8 File Offset: 0x005C4CF8
    public static PaperPath Path(string data)
    {
      PaperPath paperPath = new PaperPath();
      ClickySVGPath.<>c__DisplayClass1_0 CS$<>8__locals1;
      CS$<>8__locals1.tokens = new Queue<ClickySVGPath.Token>(ClickySVGPath.Tokenize(data));
      CS$<>8__locals1.current = new Point(0f, 0f);
      Point point = new Point(0f, 0f);
      Point? point2 = null;
      Point? point3 = null;
      char c = 'M';
      while (CS$<>8__locals1.tokens.Count > 0)
      {
        ClickySVGPath.Token.Command command = CS$<>8__locals1.tokens.Peek() as ClickySVGPath.Token.Command;
        if (command != null)
        {
          c = command.Letter;
          CS$<>8__locals1.tokens.Dequeue();
        }
        bool flag = char.IsLower(c);
        char c2 = char.ToUpperInvariant(c);
        if (c2 <= 'C')
        {
          if (c2 == 'A')
          {
            double? num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
            if (num != null)
            {
              double valueOrDefault = num.GetValueOrDefault();
              num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
              if (num != null)
              {
                double valueOrDefault2 = num.GetValueOrDefault();
                num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
                if (num != null)
                {
                  double valueOrDefault3 = num.GetValueOrDefault();
                  num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
                  if (num != null)
                  {
                    double valueOrDefault4 = num.GetValueOrDefault();
                    num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
                    if (num != null)
                    {
                      double valueOrDefault5 = num.GetValueOrDefault();
                      Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
                      if (point4 != null)
                      {
                        Point valueOrDefault6 = point4.GetValueOrDefault();
                        ClickySVGPath.AddArc(paperPath, CS$<>8__locals1.current, valueOrDefault6, valueOrDefault, valueOrDefault2, valueOrDefault3, valueOrDefault4 != 0.0, valueOrDefault5 != 0.0);
                        CS$<>8__locals1.current = valueOrDefault6;
                        point2 = null;
                        point3 = null;
                        continue;
                      }
                    }
                  }
                }
              }
            }
            return paperPath;
          }
          if (c2 == 'C')
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault7 = point4.GetValueOrDefault();
              point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
              if (point4 != null)
              {
                Point valueOrDefault8 = point4.GetValueOrDefault();
                point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
                if (point4 != null)
                {
                  Point valueOrDefault9 = point4.GetValueOrDefault();
                  paperPath.CubicTo(valueOrDefault9, valueOrDefault7, valueOrDefault8);
                  point2 = new Point?(valueOrDefault8);
                  CS$<>8__locals1.current = valueOrDefault9;
                  point3 = null;
                  continue;
                }
              }
            }
            return paperPath;
          }
        }
        else if (c2 != 'H')
        {
          switch (c2)
          {
          case 'L':
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault10 = point4.GetValueOrDefault();
              paperPath.LineTo(valueOrDefault10);
              CS$<>8__locals1.current = valueOrDefault10;
              point2 = null;
              point3 = null;
              continue;
            }
            return paperPath;
          }
          case 'M':
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault11 = point4.GetValueOrDefault();
              paperPath.MoveTo(valueOrDefault11);
              CS$<>8__locals1.current = valueOrDefault11;
              point = valueOrDefault11;
              c = (flag ? 'l' : 'L');
              point2 = null;
              point3 = null;
              continue;
            }
            return paperPath;
          }
          case 'N':
          case 'O':
          case 'P':
          case 'R':
          case 'U':
            break;
          case 'Q':
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault12 = point4.GetValueOrDefault();
              point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
              if (point4 != null)
              {
                Point valueOrDefault13 = point4.GetValueOrDefault();
                paperPath.QuadTo(valueOrDefault13, valueOrDefault12);
                point3 = new Point?(valueOrDefault12);
                CS$<>8__locals1.current = valueOrDefault13;
                point2 = null;
                continue;
              }
            }
            return paperPath;
          }
          case 'S':
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault14 = point4.GetValueOrDefault();
              point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
              if (point4 != null)
              {
                Point valueOrDefault15 = point4.GetValueOrDefault();
                Point point5;
                if (point2 != null)
                {
                  Point valueOrDefault16 = point2.GetValueOrDefault();
                  point5 = new Point(2.0 * CS$<>8__locals1.current.X - valueOrDefault16.X, 2.0 * CS$<>8__locals1.current.Y - valueOrDefault16.Y);
                }
                else
                {
                  point5 = CS$<>8__locals1.current;
                }
                Point point6 = point5;
                paperPath.CubicTo(valueOrDefault15, point6, valueOrDefault14);
                point2 = new Point?(valueOrDefault14);
                CS$<>8__locals1.current = valueOrDefault15;
                point3 = null;
                continue;
              }
            }
            return paperPath;
          }
          case 'T':
          {
            Point? point4 = ClickySVGPath.<Path>g__Pt|1_1(flag, ref CS$<>8__locals1);
            if (point4 != null)
            {
              Point valueOrDefault17 = point4.GetValueOrDefault();
              Point point7;
              if (point3 != null)
              {
                Point valueOrDefault18 = point3.GetValueOrDefault();
                point7 = new Point(2.0 * CS$<>8__locals1.current.X - valueOrDefault18.X, 2.0 * CS$<>8__locals1.current.Y - valueOrDefault18.Y);
              }
              else
              {
                point7 = CS$<>8__locals1.current;
              }
              Point point8 = point7;
              paperPath.QuadTo(valueOrDefault17, point8);
              point3 = new Point?(point8);
              CS$<>8__locals1.current = valueOrDefault17;
              point2 = null;
              continue;
            }
            return paperPath;
          }
          case 'V':
          {
            double? num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
            if (num != null)
            {
              double valueOrDefault19 = num.GetValueOrDefault();
              CS$<>8__locals1.current = new Point(CS$<>8__locals1.current.X, flag ? (CS$<>8__locals1.current.Y + valueOrDefault19) : valueOrDefault19);
              paperPath.LineTo(CS$<>8__locals1.current);
              point2 = null;
              point3 = null;
              continue;
            }
            return paperPath;
          }
          default:
            if (c2 == 'Z')
            {
              paperPath.Close();
              CS$<>8__locals1.current = point;
              point2 = null;
              point3 = null;
              if (CS$<>8__locals1.tokens.Count > 0 && CS$<>8__locals1.tokens.Peek() is ClickySVGPath.Token.Number)
              {
                c = 'L';
                continue;
              }
              continue;
            }
            break;
          }
        }
        else
        {
          double? num = ClickySVGPath.<Path>g__Number|1_0(ref CS$<>8__locals1);
          if (num != null)
          {
            double valueOrDefault20 = num.GetValueOrDefault();
            CS$<>8__locals1.current = new Point(flag ? (CS$<>8__locals1.current.X + valueOrDefault20) : valueOrDefault20, CS$<>8__locals1.current.Y);
            paperPath.LineTo(CS$<>8__locals1.current);
            point2 = null;
            point3 = null;
            continue;
          }
          return paperPath;
        }
        CS$<>8__locals1.tokens.Dequeue();
      }
      return paperPath;
    }

    // Token: 0x060017BA RID: 6074 RVA: 0x005C5344 File Offset: 0x005C5344
    private static List<ClickySVGPath.Token> Tokenize(string data)
    {
      ClickySVGPath.<>c__DisplayClass2_0 CS$<>8__locals1;
      CS$<>8__locals1.tokens = new List<ClickySVGPath.Token>();
      CS$<>8__locals1.buffer = new StringBuilder();
      foreach (char c in data)
      {
        if (char.IsLetter(c) && c != 'e' && c != 'E')
        {
          ClickySVGPath.<Tokenize>g__Flush|2_0(ref CS$<>8__locals1);
          CS$<>8__locals1.tokens.Add(new ClickySVGPath.Token.Command(c));
        }
        else if (c == '-')
        {
          bool flag = CS$<>8__locals1.buffer.Length > 0;
          if (flag)
          {
            StringBuilder buffer = CS$<>8__locals1.buffer;
            char c2 = buffer[buffer.Length - 1];
            bool flag2 = c2 == 'E' || c2 == 'e';
            flag = flag2;
          }
          if (flag)
          {
            CS$<>8__locals1.buffer.Append(c);
          }
          else
          {
            ClickySVGPath.<Tokenize>g__Flush|2_0(ref CS$<>8__locals1);
            CS$<>8__locals1.buffer.Append(c);
          }
        }
        else if (c == '.')
        {
          string text = CS$<>8__locals1.buffer.ToString();
          if (text.Contains('.') && !text.Contains('e'))
          {
            ClickySVGPath.<Tokenize>g__Flush|2_0(ref CS$<>8__locals1);
          }
          CS$<>8__locals1.buffer.Append(c);
        }
        else if (char.IsDigit(c) || c == 'e' || c == 'E')
        {
          CS$<>8__locals1.buffer.Append(c);
        }
        else
        {
          ClickySVGPath.<Tokenize>g__Flush|2_0(ref CS$<>8__locals1);
        }
      }
      ClickySVGPath.<Tokenize>g__Flush|2_0(ref CS$<>8__locals1);
      return CS$<>8__locals1.tokens;
    }

    // Token: 0x060017BB RID: 6075 RVA: 0x005C549C File Offset: 0x005C549C
    private static void AddArc(PaperPath path, Point start, Point end, double rxIn, double ryIn, double rotationDegrees, bool largeArc, bool sweep)
    {
      if (start == end)
      {
        return;
      }
      ClickySVGPath.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.rx = Math.Abs(rxIn);
      CS$<>8__locals1.ry = Math.Abs(ryIn);
      if (CS$<>8__locals1.rx <= 0.0 || CS$<>8__locals1.ry <= 0.0)
      {
        path.LineTo(end);
        return;
      }
      double num = rotationDegrees * 3.141592653589793 / 180.0;
      CS$<>8__locals1.cosPhi = Math.Cos(num);
      CS$<>8__locals1.sinPhi = Math.Sin(num);
      double num2 = (start.X - end.X) / 2.0;
      double num3 = (start.Y - end.Y) / 2.0;
      double num4 = CS$<>8__locals1.cosPhi * num2 + CS$<>8__locals1.sinPhi * num3;
      double num5 = -CS$<>8__locals1.sinPhi * num2 + CS$<>8__locals1.cosPhi * num3;
      double num6 = num4 * num4 / (CS$<>8__locals1.rx * CS$<>8__locals1.rx) + num5 * num5 / (CS$<>8__locals1.ry * CS$<>8__locals1.ry);
      if (num6 > 1.0)
      {
        CS$<>8__locals1.rx *= Math.Sqrt(num6);
        CS$<>8__locals1.ry *= Math.Sqrt(num6);
      }
      double num7 = (double)((largeArc == sweep) ? (-1) : 1);
      double num8 = Math.Max(0.0, CS$<>8__locals1.rx * CS$<>8__locals1.rx * CS$<>8__locals1.ry * CS$<>8__locals1.ry - CS$<>8__locals1.rx * CS$<>8__locals1.rx * num5 * num5 - CS$<>8__locals1.ry * CS$<>8__locals1.ry * num4 * num4);
      double num9 = CS$<>8__locals1.rx * CS$<>8__locals1.rx * num5 * num5 + CS$<>8__locals1.ry * CS$<>8__locals1.ry * num4 * num4;
      double num10 = num7 * Math.Sqrt(num8 / Math.Max(num9, 2.220446049250313E-16));
      double num11 = num10 * CS$<>8__locals1.rx * num5 / CS$<>8__locals1.ry;
      double num12 = -num10 * CS$<>8__locals1.ry * num4 / CS$<>8__locals1.rx;
      CS$<>8__locals1.cx = CS$<>8__locals1.cosPhi * num11 - CS$<>8__locals1.sinPhi * num12 + (start.X + end.X) / 2.0;
      CS$<>8__locals1.cy = CS$<>8__locals1.sinPhi * num11 + CS$<>8__locals1.cosPhi * num12 + (start.Y + end.Y) / 2.0;
      double num13 = ClickySVGPath.<AddArc>g__Angle|3_0(1.0, 0.0, (num4 - num11) / CS$<>8__locals1.rx, (num5 - num12) / CS$<>8__locals1.ry);
      double num14 = ClickySVGPath.<AddArc>g__Angle|3_0((num4 - num11) / CS$<>8__locals1.rx, (num5 - num12) / CS$<>8__locals1.ry, (-num4 - num11) / CS$<>8__locals1.rx, (-num5 - num12) / CS$<>8__locals1.ry);
      if (!sweep && num14 > 0.0)
      {
        num14 -= 6.283185307179586;
      }
      else if (sweep && num14 < 0.0)
      {
        num14 += 6.283185307179586;
      }
      int num15 = Math.Max(1, (int)Math.Ceiling(Math.Abs(num14) / 1.5707963267948966));
      double num16 = num14 / (double)num15;
      double num17 = 1.3333333333333333 * Math.Tan(num16 / 4.0);
      double num18 = num13;
      for (int i = 0; i < num15; i++)
      {
        double num19 = num18 + num16;
        Point point = ClickySVGPath.<AddArc>g__On|3_1(num18, ref CS$<>8__locals1);
        Point point2 = ClickySVGPath.<AddArc>g__On|3_1(num19, ref CS$<>8__locals1);
        Point point3 = ClickySVGPath.<AddArc>g__D|3_2(num18, ref CS$<>8__locals1);
        Point point4 = ClickySVGPath.<AddArc>g__D|3_2(num19, ref CS$<>8__locals1);
        path.CubicTo(point2, new Point(point.X + num17 * point3.X, point.Y + num17 * point3.Y), new Point(point2.X - num17 * point4.X, point2.Y - num17 * point4.Y));
        num18 = num19;
      }
    }

    // Token: 0x060017BC RID: 6076 RVA: 0x005C58AC File Offset: 0x005C58AC
    [CompilerGenerated]
    internal static double? <Path>g__Number|1_0(ref ClickySVGPath.<>c__DisplayClass1_0 A_0)
    {
      if (A_0.tokens.Count != 0)
      {
        ClickySVGPath.Token.Number number = A_0.tokens.Peek() as ClickySVGPath.Token.Number;
        if (number != null)
        {
          A_0.tokens.Dequeue();
          return new double?(number.Value);
        }
      }
      return null;
    }

    // Token: 0x060017BD RID: 6077 RVA: 0x005C58FC File Offset: 0x005C58FC
    [CompilerGenerated]
    internal static Point? <Path>g__Pt|1_1(bool relative, ref ClickySVGPath.<>c__DisplayClass1_0 A_1)
    {
      double? num = ClickySVGPath.<Path>g__Number|1_0(ref A_1);
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        num = ClickySVGPath.<Path>g__Number|1_0(ref A_1);
        if (num != null)
        {
          double valueOrDefault2 = num.GetValueOrDefault();
          return new Point?(relative ? new Point(A_1.current.X + valueOrDefault, A_1.current.Y + valueOrDefault2) : new Point(valueOrDefault, valueOrDefault2));
        }
      }
      return null;
    }

    // Token: 0x060017BE RID: 6078 RVA: 0x005C5978 File Offset: 0x005C5978
    [CompilerGenerated]
    internal static void <Tokenize>g__Flush|2_0(ref ClickySVGPath.<>c__DisplayClass2_0 A_0)
    {
      double num;
      if (A_0.buffer.Length > 0 && double.TryParse(A_0.buffer.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
      {
        A_0.tokens.Add(new ClickySVGPath.Token.Number(num));
      }
      A_0.buffer.Clear();
    }

    // Token: 0x060017BF RID: 6079 RVA: 0x005C59D0 File Offset: 0x005C59D0
    [CompilerGenerated]
    internal static double <AddArc>g__Angle|3_0(double ux, double uy, double vx, double vy)
    {
      double num = ux * vx + uy * vy;
      double num2 = Math.Sqrt((ux * ux + uy * uy) * (vx * vx + vy * vy));
      double num3 = Math.Acos(Math.Max(-1.0, Math.Min(1.0, num / num2)));
      if (ux * vy - uy * vx < 0.0)
      {
        num3 = -num3;
      }
      return num3;
    }

    // Token: 0x060017C0 RID: 6080 RVA: 0x005C5A38 File Offset: 0x005C5A38
    [CompilerGenerated]
    internal static Point <AddArc>g__On|3_1(double t, ref ClickySVGPath.<>c__DisplayClass3_0 A_1)
    {
      return new Point(A_1.cx + A_1.rx * Math.Cos(t) * A_1.cosPhi - A_1.ry * Math.Sin(t) * A_1.sinPhi, A_1.cy + A_1.rx * Math.Cos(t) * A_1.sinPhi + A_1.ry * Math.Sin(t) * A_1.cosPhi);
    }

    // Token: 0x060017C1 RID: 6081 RVA: 0x005C5AAC File Offset: 0x005C5AAC
    [CompilerGenerated]
    internal static Point <AddArc>g__D|3_2(double t, ref ClickySVGPath.<>c__DisplayClass3_0 A_1)
    {
      return new Point(-A_1.rx * Math.Sin(t) * A_1.cosPhi - A_1.ry * Math.Cos(t) * A_1.sinPhi, -A_1.rx * Math.Sin(t) * A_1.sinPhi + A_1.ry * Math.Cos(t) * A_1.cosPhi);
    }

    // Token: 0x02000786 RID: 1926
    [Nullable(0)]
    private abstract class Token : IEquatable<ClickySVGPath.Token>
    {
      // Token: 0x170007CC RID: 1996
      // (get) Token: 0x060035F2 RID: 13810 RVA: 0x0067D43C File Offset: 0x0067D43C
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickySVGPath.Token);
        }
      }

      // Token: 0x060035F3 RID: 13811 RVA: 0x0067D448 File Offset: 0x0067D448
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Token");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060035F4 RID: 13812 RVA: 0x0067D494 File Offset: 0x0067D494
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x060035F5 RID: 13813 RVA: 0x0067D498 File Offset: 0x0067D498
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickySVGPath.Token left, ClickySVGPath.Token right)
      {
        return !(left == right);
      }

      // Token: 0x060035F6 RID: 13814 RVA: 0x0067D4A4 File Offset: 0x0067D4A4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickySVGPath.Token left, ClickySVGPath.Token right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060035F7 RID: 13815 RVA: 0x0067D4B8 File Offset: 0x0067D4B8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x060035F8 RID: 13816 RVA: 0x0067D4CC File Offset: 0x0067D4CC
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickySVGPath.Token);
      }

      // Token: 0x060035F9 RID: 13817 RVA: 0x0067D4DC File Offset: 0x0067D4DC
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickySVGPath.Token other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x060035FB RID: 13819 RVA: 0x0067D4FC File Offset: 0x0067D4FC
      [CompilerGenerated]
      protected Token(ClickySVGPath.Token original)
      {
      }

      // Token: 0x060035FC RID: 13820 RVA: 0x0067D504 File Offset: 0x0067D504
      protected Token()
      {
      }

      // Token: 0x02000AFE RID: 2814
      [Nullable(0)]
      public sealed class Command : ClickySVGPath.Token, IEquatable<ClickySVGPath.Token.Command>
      {
        // Token: 0x060043AC RID: 17324 RVA: 0x006D0710 File Offset: 0x006D0710
        public Command(char Letter)
        {
          this.Letter = Letter;
          base..ctor();
        }

        // Token: 0x170008AB RID: 2219
        // (get) Token: 0x060043AD RID: 17325 RVA: 0x006D0720 File Offset: 0x006D0720
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickySVGPath.Token.Command);
          }
        }

        // Token: 0x170008AC RID: 2220
        // (get) Token: 0x060043AE RID: 17326 RVA: 0x006D072C File Offset: 0x006D072C
        // (set) Token: 0x060043AF RID: 17327 RVA: 0x006D0734 File Offset: 0x006D0734
        public char Letter { get; set; }

        // Token: 0x060043B0 RID: 17328 RVA: 0x006D0740 File Offset: 0x006D0740
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

        // Token: 0x060043B1 RID: 17329 RVA: 0x006D078C File Offset: 0x006D078C
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Letter = ");
          builder.Append(this.Letter.ToString());
          return true;
        }

        // Token: 0x060043B2 RID: 17330 RVA: 0x006D07D8 File Offset: 0x006D07D8
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickySVGPath.Token.Command left, ClickySVGPath.Token.Command right)
        {
          return !(left == right);
        }

        // Token: 0x060043B3 RID: 17331 RVA: 0x006D07E4 File Offset: 0x006D07E4
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickySVGPath.Token.Command left, ClickySVGPath.Token.Command right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060043B4 RID: 17332 RVA: 0x006D07F8 File Offset: 0x006D07F8
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<char>.Default.GetHashCode(this.<Letter>k__BackingField);
        }

        // Token: 0x060043B5 RID: 17333 RVA: 0x006D0818 File Offset: 0x006D0818
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickySVGPath.Token.Command);
        }

        // Token: 0x060043B6 RID: 17334 RVA: 0x006D0828 File Offset: 0x006D0828
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickySVGPath.Token other)
        {
          return this.Equals(other);
        }

        // Token: 0x060043B7 RID: 17335 RVA: 0x006D0834 File Offset: 0x006D0834
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickySVGPath.Token.Command other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<char>.Default.Equals(this.<Letter>k__BackingField, other.<Letter>k__BackingField));
        }

        // Token: 0x060043B9 RID: 17337 RVA: 0x006D0868 File Offset: 0x006D0868
        [CompilerGenerated]
        private Command(ClickySVGPath.Token.Command original)
          : base(original)
        {
          this.Letter = original.<Letter>k__BackingField;
        }

        // Token: 0x060043BA RID: 17338 RVA: 0x006D0880 File Offset: 0x006D0880
        [CompilerGenerated]
        public void Deconstruct(out char Letter)
        {
          Letter = this.Letter;
        }
      }

      // Token: 0x02000AFF RID: 2815
      [Nullable(0)]
      public sealed class Number : ClickySVGPath.Token, IEquatable<ClickySVGPath.Token.Number>
      {
        // Token: 0x060043BB RID: 17339 RVA: 0x006D088C File Offset: 0x006D088C
        public Number(double Value)
        {
          this.Value = Value;
          base..ctor();
        }

        // Token: 0x170008AD RID: 2221
        // (get) Token: 0x060043BC RID: 17340 RVA: 0x006D089C File Offset: 0x006D089C
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickySVGPath.Token.Number);
          }
        }

        // Token: 0x170008AE RID: 2222
        // (get) Token: 0x060043BD RID: 17341 RVA: 0x006D08A8 File Offset: 0x006D08A8
        // (set) Token: 0x060043BE RID: 17342 RVA: 0x006D08B0 File Offset: 0x006D08B0
        public double Value { get; set; }

        // Token: 0x060043BF RID: 17343 RVA: 0x006D08BC File Offset: 0x006D08BC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Number");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x060043C0 RID: 17344 RVA: 0x006D0908 File Offset: 0x006D0908
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Value = ");
          builder.Append(this.Value.ToString());
          return true;
        }

        // Token: 0x060043C1 RID: 17345 RVA: 0x006D0954 File Offset: 0x006D0954
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickySVGPath.Token.Number left, ClickySVGPath.Token.Number right)
        {
          return !(left == right);
        }

        // Token: 0x060043C2 RID: 17346 RVA: 0x006D0960 File Offset: 0x006D0960
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickySVGPath.Token.Number left, ClickySVGPath.Token.Number right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x060043C3 RID: 17347 RVA: 0x006D0974 File Offset: 0x006D0974
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Value>k__BackingField);
        }

        // Token: 0x060043C4 RID: 17348 RVA: 0x006D0994 File Offset: 0x006D0994
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickySVGPath.Token.Number);
        }

        // Token: 0x060043C5 RID: 17349 RVA: 0x006D09A4 File Offset: 0x006D09A4
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickySVGPath.Token other)
        {
          return this.Equals(other);
        }

        // Token: 0x060043C6 RID: 17350 RVA: 0x006D09B0 File Offset: 0x006D09B0
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickySVGPath.Token.Number other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField));
        }

        // Token: 0x060043C8 RID: 17352 RVA: 0x006D09E4 File Offset: 0x006D09E4
        [CompilerGenerated]
        private Number(ClickySVGPath.Token.Number original)
          : base(original)
        {
          this.Value = original.<Value>k__BackingField;
        }

        // Token: 0x060043C9 RID: 17353 RVA: 0x006D09FC File Offset: 0x006D09FC
        [CompilerGenerated]
        public void Deconstruct(out double Value)
        {
          Value = this.Value;
        }
      }
    }
  }
}
