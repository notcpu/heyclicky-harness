using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200029C RID: 668
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSketchPainter
  {
    // Token: 0x06001B98 RID: 7064 RVA: 0x005F2810 File Offset: 0x005F2810
    [NullableContext(2)]
    public static Color ColorOf(string name, Color ink)
    {
      if (name != null)
      {
        switch (name.Length)
        {
        case 3:
          if (name == "red")
          {
            return PaperColor.Hex("#D84A44");
          }
          break;
        case 4:
        {
          char c = name[2];
          if (c <= 'e')
          {
            if (c != 'a')
            {
              if (c != 'e')
              {
                break;
              }
              if (!(name == "grey"))
              {
                break;
              }
            }
            else if (!(name == "gray"))
            {
              break;
            }
            return PaperColor.Hex("#80848C");
          }
          if (c != 'n')
          {
            if (c == 'u')
            {
              if (name == "blue")
              {
                return PaperColor.Hex("#2F6FD6");
              }
            }
          }
          else if (name == "pink")
          {
            return PaperColor.Hex("#D9508F");
          }
          break;
        }
        case 5:
          if (name == "green")
          {
            return PaperColor.Hex("#2E9A5E");
          }
          break;
        case 6:
        {
          char c = name[0];
          if (c != 'o')
          {
            if (c != 'p')
            {
              if (c == 'y')
              {
                if (name == "yellow")
                {
                  return PaperColor.Hex("#C99A06");
                }
              }
            }
            else if (name == "purple")
            {
              return PaperColor.Hex("#7E57C8");
            }
          }
          else if (name == "orange")
          {
            return PaperColor.Hex("#E2782C");
          }
          break;
        }
        }
      }
      return ink;
    }

    // Token: 0x06001B99 RID: 7065 RVA: 0x005F29AC File Offset: 0x005F29AC
    public static PaperFont Font(ClickyInkSketchModel.Size size, bool labelOnShape)
    {
      PaperFont paperFont;
      if (size != ClickyInkSketchModel.Size.Small)
      {
        if (size != ClickyInkSketchModel.Size.Normal)
        {
          paperFont = ClickyPaperFonts.Hand((double)(labelOnShape ? 19 : 22));
        }
        else
        {
          paperFont = ClickyPaperFonts.Hand((double)(labelOnShape ? 15 : 16));
        }
      }
      else
      {
        paperFont = ClickyPaperFonts.Hand((double)(labelOnShape ? 12 : 13));
      }
      return paperFont;
    }

    // Token: 0x06001B9A RID: 7066 RVA: 0x005F29F8 File Offset: 0x005F29F8
    public static void Draw(ClickyInkSketchModel.Piece piece, double progress, int seed, Color ink, Canvas canvas)
    {
      ClickyInkSketchRandom clickyInkSketchRandom = new ClickyInkSketchRandom(seed);
      Color color = ClickyInkSketchPainter.ColorOf(piece.Color, ink);
      Color color2 = color;
      double num = ((piece.Size == ClickyInkSketchModel.Size.Big) ? 2.6 : 2.0);
      PenLineCap penLineCap = PenLineCap.Round;
      PenLineJoin penLineJoin = PenLineJoin.Round;
      double[] array;
      if (!piece.IsDashed)
      {
        array = null;
      }
      else
      {
        double[] array2 = new double[2];
        array2[0] = 6.0;
        array = array2;
        array2[1] = 6.0;
      }
      PaperStroke paperStroke = new PaperStroke(color2, num, penLineCap, penLineJoin, array);
      double num2 = Math.Min(1.0, progress / 0.8);
      double num3 = Math.Max(0.0, (progress - 0.35) / 0.65);
      ClickyInkSketchModel.Shape shape = piece.Shape;
      ClickyInkSketchModel.Shape.Box box = shape as ClickyInkSketchModel.Shape.Box;
      if (box == null)
      {
        ClickyInkSketchModel.Shape.Circle circle = shape as ClickyInkSketchModel.Shape.Circle;
        if (circle == null)
        {
          ClickyInkSketchModel.Shape.Line line = shape as ClickyInkSketchModel.Shape.Line;
          if (line == null)
          {
            ClickyInkSketchModel.Shape.Arrow arrow = shape as ClickyInkSketchModel.Shape.Arrow;
            if (arrow == null)
            {
              ClickyInkSketchModel.Shape.Path path = shape as ClickyInkSketchModel.Shape.Path;
              if (path == null)
              {
                ClickyInkSketchModel.Shape.Text text = shape as ClickyInkSketchModel.Shape.Text;
                if (text != null)
                {
                  ClickyInkSketchPainter.DrawWords(piece.Label ?? "", text.At, new Point(0.0, 0.5), 380.0 - text.At.X, ClickyInkSketchPainter.Font(piece.Size, false), color, progress, canvas);
                  return;
                }
                ClickyInkSketchModel.Shape.Icon icon = shape as ClickyInkSketchModel.Shape.Icon;
                if (icon == null)
                {
                  ClickyInkSketchModel.Shape.Dot dot = shape as ClickyInkSketchModel.Shape.Dot;
                  if (dot == null)
                  {
                    ClickyInkSketchModel.Shape.Badge badge = shape as ClickyInkSketchModel.Shape.Badge;
                    if (badge == null)
                    {
                      return;
                    }
                    ClickyInkSketchPainter.Fill(canvas, ClickyInkSketchPainter.HandCircle(badge.At, 11.0, ref clickyInkSketchRandom), color.Opacity(0.9 * progress));
                    string label = piece.Label;
                    if (label != null)
                    {
                      ClickyInkSketchPainter.DrawWords(label, badge.At, new Point(0.5, 0.5), 24.0, ClickyPaperFonts.Hand(14.0), PaperColor.White, num3, canvas);
                    }
                  }
                  else
                  {
                    double num4 = ((piece.Size == ClickyInkSketchModel.Size.Big) ? 6.0 : ((piece.Size == ClickyInkSketchModel.Size.Small) ? 2.5 : 4.0));
                    ClickyInkSketchPainter.Fill(canvas, new PaperPath().AddEllipse(new Rect(dot.At.X - num4, dot.At.Y - num4, num4 * 2.0, num4 * 2.0)), color.Opacity(progress));
                    string label2 = piece.Label;
                    if (label2 != null)
                    {
                      ClickyInkSketchPainter.DrawWords(label2, new Point(dot.At.X + num4 + 5.0, dot.At.Y), new Point(0.0, 0.5), 160.0, ClickyInkSketchPainter.Font(piece.Size, false), ink, num3, canvas);
                      return;
                    }
                  }
                }
                else
                {
                  TextBlock textBlock = ClickyInkIcon.Glyph(icon.Name, icon.Size * 0.92, color, false);
                  if (textBlock != null)
                  {
                    Grid grid = new Grid
                    {
                      Width = icon.Size,
                      Height = icon.Size,
                      Opacity = progress,
                      Children = { textBlock }
                    };
                    Canvas.SetLeft(grid, icon.At.X - icon.Size / 2.0);
                    Canvas.SetTop(grid, icon.At.Y - icon.Size / 2.0);
                    canvas.Children.Add(grid);
                  }
                  string label3 = piece.Label;
                  if (label3 != null)
                  {
                    ClickyInkSketchPainter.DrawWords(label3, new Point(icon.At.X, icon.At.Y + icon.Size / 2.0 + 9.0), new Point(0.5, 0.5), 120.0, ClickyInkSketchPainter.Font(ClickyInkSketchModel.Size.Small, true), ink, num3, canvas);
                    return;
                  }
                }
              }
              else
              {
                IReadOnlyList<Point> points = path.Points;
                PaperPath paperPath = ClickyInkSketchPainter.HandPath(points, piece.IsCurved, ref clickyInkSketchRandom);
                if (piece.IsFilled)
                {
                  ClickyInkSketchPainter.Fill(canvas, paperPath, color.Opacity(0.16 * num2));
                }
                ClickyInkSketchPainter.Stroke(canvas, paperPath.Trimmed(0.0, num2), paperStroke);
                string label4 = piece.Label;
                if (label4 != null && points.Count > 0)
                {
                  string text2 = label4;
                  IReadOnlyList<Point> readOnlyList = points;
                  double num5 = readOnlyList[readOnlyList.Count - 1].X + 6.0;
                  IReadOnlyList<Point> readOnlyList2 = points;
                  ClickyInkSketchPainter.DrawWords(text2, new Point(num5, readOnlyList2[readOnlyList2.Count - 1].Y), new Point(0.0, 0.5), 160.0, ClickyInkSketchPainter.Font(piece.Size, false), color, num3, canvas);
                  return;
                }
              }
            }
            else
            {
              double num6 = (piece.IsCurved ? 0.2 : 0.03);
              PaperPath paperPath2 = ClickyInkSketchPainter.HandLine(arrow.From, arrow.To, num6, ref clickyInkSketchRandom);
              ClickyInkSketchPainter.Stroke(canvas, paperPath2.Trimmed(0.0, num2), paperStroke);
              if (num2 >= 1.0)
              {
                Point point = ClickyInkSketchPainter.BowControl(arrow.From, arrow.To, num6);
                double num7 = Math.Atan2(arrow.To.Y - point.Y, arrow.To.X - point.X);
                PaperPath paperPath3 = new PaperPath();
                double[] array3 = new double[] { -1.0, 1.0 };
                for (int i = 0; i < array3.Length; i++)
                {
                  double num8 = array3[i] * 0.5;
                  paperPath3.MoveTo(arrow.To);
                  paperPath3.LineTo(arrow.To.X - 10.0 * Math.Cos(num7 + num8), arrow.To.Y - 10.0 * Math.Sin(num7 + num8));
                }
                ClickyInkSketchPainter.Stroke(canvas, paperPath3, new PaperStroke(color, paperStroke.Width, PenLineCap.Round, PenLineJoin.Miter, null));
              }
              string label5 = piece.Label;
              if (label5 != null)
              {
                ClickyInkSketchPainter.DrawMidLabel(label5, arrow.From, arrow.To, ink, num3, canvas);
                return;
              }
            }
          }
          else
          {
            PaperPath paperPath4 = ClickyInkSketchPainter.HandLine(line.From, line.To, piece.IsCurved ? 0.18 : 0.03, ref clickyInkSketchRandom);
            ClickyInkSketchPainter.Stroke(canvas, paperPath4.Trimmed(0.0, num2), paperStroke);
            string label6 = piece.Label;
            if (label6 != null)
            {
              ClickyInkSketchPainter.DrawMidLabel(label6, line.From, line.To, ink, num3, canvas);
              return;
            }
          }
        }
        else
        {
          PaperPath paperPath5 = ClickyInkSketchPainter.HandCircle(circle.Center, circle.Radius, ref clickyInkSketchRandom);
          if (piece.IsFilled)
          {
            ClickyInkSketchPainter.Fill(canvas, paperPath5, color.Opacity(0.16 * num2));
          }
          ClickyInkSketchPainter.Stroke(canvas, paperPath5.Trimmed(0.0, num2), paperStroke);
          string label7 = piece.Label;
          if (label7 != null)
          {
            ClickyInkSketchPainter.DrawWords(label7, circle.Center, new Point(0.5, 0.5), circle.Radius * 1.8, ClickyInkSketchPainter.Font(piece.Size, true), piece.IsFilled ? color : ink, num3, canvas);
            return;
          }
        }
      }
      else
      {
        Rect rect = box.Rect;
        PaperPath paperPath6 = ClickyInkSketchPainter.HandRect(rect, ref clickyInkSketchRandom);
        if (piece.IsFilled)
        {
          ClickyInkSketchPainter.Fill(canvas, paperPath6, color.Opacity(0.16 * num2));
        }
        ClickyInkSketchPainter.Stroke(canvas, paperPath6.Trimmed(0.0, num2), paperStroke);
        string label8 = piece.Label;
        if (label8 != null)
        {
          ClickyInkSketchPainter.DrawWords(label8, new Point(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0), new Point(0.5, 0.5), rect.Width - 8.0, ClickyInkSketchPainter.Font(piece.Size, true), piece.IsFilled ? color : ink, num3, canvas);
          return;
        }
      }
    }

    // Token: 0x06001B9B RID: 7067 RVA: 0x005F32F8 File Offset: 0x005F32F8
    private static void Fill(Canvas canvas, PaperPath path, Color color)
    {
      canvas.Children.Add(new Path
      {
        Data = path.ToGeometry(),
        Fill = Ui.Brush(color)
      });
    }

    // Token: 0x06001B9C RID: 7068 RVA: 0x005F3324 File Offset: 0x005F3324
    private static void Stroke(Canvas canvas, PaperPath path, PaperStroke stroke)
    {
      Path path2 = new Path
      {
        Data = path.ToGeometry(),
        Stroke = Ui.Brush(stroke.Color),
        StrokeThickness = stroke.Width,
        StrokeStartLineCap = stroke.Cap,
        StrokeEndLineCap = stroke.Cap,
        StrokeDashCap = stroke.Cap,
        StrokeLineJoin = stroke.Join
      };
      double[] dash = stroke.Dash;
      if (dash != null)
      {
        DoubleCollection doubleCollection = new DoubleCollection();
        foreach (double num in dash)
        {
          doubleCollection.Add(num / stroke.Width);
        }
        path2.StrokeDashArray = doubleCollection;
      }
      canvas.Children.Add(path2);
    }

    // Token: 0x06001B9D RID: 7069 RVA: 0x005F33DC File Offset: 0x005F33DC
    private static void DrawMidLabel(string label, Point from, Point to, Color ink, double progress, Canvas canvas)
    {
      Point point = new Point((from.X + to.X) / 2.0, (from.Y + to.Y) / 2.0 - 11.0);
      ClickyInkSketchPainter.DrawWords(label, point, new Point(0.5, 0.5), 150.0, ClickyInkSketchPainter.Font(ClickyInkSketchModel.Size.Small, false), ink.Opacity(0.8), progress, canvas);
    }

    // Token: 0x06001B9E RID: 7070 RVA: 0x005F3470 File Offset: 0x005F3470
    private static void DrawWords(string words, Point point, Point anchor, double maxWidth, PaperFont font, Color color, double progress, Canvas canvas)
    {
      if (progress <= 0.0 || words.Length == 0)
      {
        return;
      }
      InkTextView inkTextView = new InkTextView(ClickyInkTextContent.Plain(words, null), font, new Color?(color));
      inkTextView.Measure(new Size(Math.Max(20.0, maxWidth), 200.0));
      Size desiredSize = inkTextView.DesiredSize;
      double num = point.X - desiredSize.Width * anchor.X;
      double num2 = point.Y - desiredSize.Height * anchor.Y;
      inkTextView.Width = desiredSize.Width;
      inkTextView.Height = desiredSize.Height;
      Grid grid = new Grid
      {
        Width = desiredSize.Width,
        Height = desiredSize.Height,
        Children = { inkTextView },
        Clip = new RectangleGeometry
        {
          Rect = new Rect(-2.0, -4.0, Math.Max(0.0, (desiredSize.Width + 4.0) * progress), desiredSize.Height + 8.0)
        }
      };
      Canvas.SetLeft(grid, num);
      Canvas.SetTop(grid, num2);
      canvas.Children.Add(grid);
    }

    // Token: 0x06001B9F RID: 7071 RVA: 0x005F35CC File Offset: 0x005F35CC
    private static double Wobble(ref ClickyInkSketchRandom random, double amount = 1.2)
    {
      return (random.Next() * 2.0 - 1.0) * amount;
    }

    // Token: 0x06001BA0 RID: 7072 RVA: 0x005F35EC File Offset: 0x005F35EC
    private static PaperPath HandRect(Rect rect, ref ClickyInkSketchRandom random)
    {
      Point[] array = new Point[]
      {
        new Point(rect.X, rect.Y),
        new Point(rect.X + rect.Width, rect.Y),
        new Point(rect.X + rect.Width, rect.Y + rect.Height),
        new Point(rect.X, rect.Y + rect.Height)
      };
      PaperPath paperPath = new PaperPath();
      Point point = new Point(array[0].X + ClickyInkSketchPainter.Wobble(ref random, 1.2), array[0].Y + ClickyInkSketchPainter.Wobble(ref random, 1.2));
      paperPath.MoveTo(point);
      for (int i = 1; i <= 4; i++)
      {
        Point point2 = array[i - 1];
        Point point3 = array[i % 4];
        Point point4 = ((i == 4) ? new Point(point3.X + 5.0 + ClickyInkSketchPainter.Wobble(ref random, 1.2), point3.Y + ClickyInkSketchPainter.Wobble(ref random, 1.2)) : new Point(point3.X + ClickyInkSketchPainter.Wobble(ref random, 1.2), point3.Y + ClickyInkSketchPainter.Wobble(ref random, 1.2)));
        Point point5 = new Point((point2.X + point3.X) / 2.0 + ClickyInkSketchPainter.Wobble(ref random, 1.8), (point2.Y + point3.Y) / 2.0 + ClickyInkSketchPainter.Wobble(ref random, 1.8));
        paperPath.QuadTo(point4, point5);
      }
      return paperPath;
    }

    // Token: 0x06001BA1 RID: 7073 RVA: 0x005F37DC File Offset: 0x005F37DC
    private static PaperPath HandCircle(Point center, double radius, ref ClickyInkSketchRandom random)
    {
      PaperPath paperPath = new PaperPath();
      double num = random.Next() * 3.141592653589793 * 2.0;
      for (int i = 0; i <= 39; i++)
      {
        double num2 = num + (double)i / 36.0 * 3.141592653589793 * 2.0;
        double num3 = radius * (1.0 + (random.Next() * 2.0 - 1.0) * 0.025);
        Point point = new Point(center.X + num3 * Math.Cos(num2), center.Y + num3 * Math.Sin(num2));
        if (i == 0)
        {
          paperPath.MoveTo(point);
        }
        else
        {
          paperPath.LineTo(point);
        }
      }
      return paperPath;
    }

    // Token: 0x06001BA2 RID: 7074 RVA: 0x005F38B4 File Offset: 0x005F38B4
    private static Point BowControl(Point from, Point to, double bow)
    {
      Point point = new Point((from.X + to.X) / 2.0, (from.Y + to.Y) / 2.0);
      double num = to.X - from.X;
      double num2 = to.Y - from.Y;
      double num3 = Math.Max(1.0, Math.Sqrt(num * num + num2 * num2));
      return new Point(point.X - num2 / num3 * num3 * bow, point.Y + num / num3 * num3 * bow);
    }

    // Token: 0x06001BA3 RID: 7075 RVA: 0x005F3958 File Offset: 0x005F3958
    private static PaperPath HandLine(Point from, Point to, double bow, ref ClickyInkSketchRandom random)
    {
      Point point = ClickyInkSketchPainter.BowControl(from, to, bow);
      PaperPath paperPath = new PaperPath();
      paperPath.MoveTo(from.X + ClickyInkSketchPainter.Wobble(ref random, 0.8), from.Y + ClickyInkSketchPainter.Wobble(ref random, 0.8));
      paperPath.QuadTo(to, new Point(point.X + ClickyInkSketchPainter.Wobble(ref random, 1.5), point.Y + ClickyInkSketchPainter.Wobble(ref random, 1.5)));
      return paperPath;
    }

    // Token: 0x06001BA4 RID: 7076 RVA: 0x005F39E4 File Offset: 0x005F39E4
    private static PaperPath HandPath(IReadOnlyList<Point> points, bool curved, ref ClickyInkSketchRandom random)
    {
      PaperPath paperPath = new PaperPath();
      if (points.Count == 0)
      {
        return paperPath;
      }
      paperPath.MoveTo(points[0]);
      if (curved && points.Count > 2)
      {
        for (int i = 1; i < points.Count; i++)
        {
          Point point = points[i - 1];
          Point point2 = points[i];
          Point point3 = new Point((point.X + point2.X) / 2.0, (point.Y + point2.Y) / 2.0);
          paperPath.QuadTo(point3, point);
          if (i == points.Count - 1)
          {
            paperPath.LineTo(point2);
          }
        }
      }
      else
      {
        foreach (Point point4 in points.Skip<Point>(1))
        {
          paperPath.LineTo(point4.X + ClickyInkSketchPainter.Wobble(ref random, 0.8), point4.Y + ClickyInkSketchPainter.Wobble(ref random, 0.8));
        }
      }
      return paperPath;
    }
  }
}
