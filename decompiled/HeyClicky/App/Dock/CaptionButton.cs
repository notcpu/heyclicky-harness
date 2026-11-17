using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031C RID: 796
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CaptionButton : Pressable
  {
    // Token: 0x0600226C RID: 8812 RVA: 0x006235C4 File Offset: 0x006235C4
    public CaptionButton(CaptionButton.Kind kind, double height, Color ink, string help)
      : base(false)
    {
      this.kind = kind;
      base.ProtectedCursor = Pressable.ArrowCursor;
      base.Width = 46.0;
      base.Height = height;
      this.restInk = Ui.Brush(ink);
      this.hoverInk = Ui.Brush((kind == CaptionButton.Kind.Close) ? Colors.White : ink);
      this.plate = new Border
      {
        Background = Ui.Brush((kind == CaptionButton.Kind.Close) ? CaptionButton.CloseRed : Colors.Black),
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      base.Children.Add(this.plate);
      this.glyph = new Path
      {
        Data = CaptionButton.Glyph(kind),
        Stroke = this.restInk,
        StrokeThickness = 1.0,
        Width = 10.0,
        Height = 10.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        IsHitTestVisible = false
      };
      base.Children.Add(this.glyph);
      base.HoverChanged += delegate(bool _)
      {
        this.ApplyLook();
      };
      base.PressedChanged += delegate(bool _)
      {
        this.ApplyLook();
      };
      ToolTipService.SetToolTip(this, help);
      AutomationProperties.SetName(this, help);
    }

    // Token: 0x0600226D RID: 8813 RVA: 0x00623718 File Offset: 0x00623718
    private static Geometry Glyph(CaptionButton.Kind kind)
    {
      CaptionButton.<>c__DisplayClass10_0 CS$<>8__locals1;
      CS$<>8__locals1.geometry = new PathGeometry();
      if (kind != CaptionButton.Kind.Close)
      {
        if (kind != CaptionButton.Kind.Maximize)
        {
          CaptionButton.<Glyph>g__Lines|10_0(true, new ValueTuple<double, double>[]
          {
            new ValueTuple<double, double>(0.5, 2.5),
            new ValueTuple<double, double>(7.5, 2.5),
            new ValueTuple<double, double>(7.5, 9.5),
            new ValueTuple<double, double>(0.5, 9.5)
          }, ref CS$<>8__locals1);
          CaptionButton.<Glyph>g__Lines|10_0(false, new ValueTuple<double, double>[]
          {
            new ValueTuple<double, double>(2.5, 2.5),
            new ValueTuple<double, double>(2.5, 0.5),
            new ValueTuple<double, double>(9.5, 0.5),
            new ValueTuple<double, double>(9.5, 7.5),
            new ValueTuple<double, double>(7.5, 7.5)
          }, ref CS$<>8__locals1);
        }
        else
        {
          CaptionButton.<Glyph>g__Lines|10_0(true, new ValueTuple<double, double>[]
          {
            new ValueTuple<double, double>(0.5, 0.5),
            new ValueTuple<double, double>(9.5, 0.5),
            new ValueTuple<double, double>(9.5, 9.5),
            new ValueTuple<double, double>(0.5, 9.5)
          }, ref CS$<>8__locals1);
        }
      }
      else
      {
        CaptionButton.<Glyph>g__Lines|10_0(false, new ValueTuple<double, double>[]
        {
          new ValueTuple<double, double>(0.0, 0.0),
          new ValueTuple<double, double>(10.0, 10.0)
        }, ref CS$<>8__locals1);
        CaptionButton.<Glyph>g__Lines|10_0(false, new ValueTuple<double, double>[]
        {
          new ValueTuple<double, double>(0.0, 10.0),
          new ValueTuple<double, double>(10.0, 0.0)
        }, ref CS$<>8__locals1);
      }
      return CS$<>8__locals1.geometry;
    }

    // Token: 0x0600226E RID: 8814 RVA: 0x00623994 File Offset: 0x00623994
    private void ApplyLook()
    {
      bool flag = base.IsHovered || base.IsPressed;
      this.plate.Opacity = ((!flag) ? 0.0 : ((this.kind == CaptionButton.Kind.Close) ? (base.IsPressed ? 0.9 : 1.0) : (base.IsPressed ? 0.04 : 0.06)));
      this.glyph.Stroke = (flag ? this.hoverInk : this.restInk);
      this.glyph.Opacity = (base.IsPressed ? 0.7 : 1.0);
    }

    // Token: 0x17000661 RID: 1633
    // (get) Token: 0x0600226F RID: 8815 RVA: 0x00623A54 File Offset: 0x00623A54
    public CaptionButton.Kind ButtonKind
    {
      get
      {
        return this.kind;
      }
    }

    // Token: 0x06002273 RID: 8819 RVA: 0x00623A88 File Offset: 0x00623A88
    [CompilerGenerated]
    internal static void <Glyph>g__Lines|10_0(bool closed, [TupleElementNames(new string[] { "X", "Y" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<double, double>[] points, ref CaptionButton.<>c__DisplayClass10_0 A_2)
    {
      PathFigure pathFigure = new PathFigure
      {
        StartPoint = new Point(points[0].Item1, points[0].Item2),
        IsClosed = closed
      };
      foreach (ValueTuple<double, double> valueTuple in points.Skip<ValueTuple<double, double>>(1))
      {
        double item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        pathFigure.Segments.Add(new LineSegment
        {
          Point = new Point(item, item2)
        });
      }
      A_2.geometry.Figures.Add(pathFigure);
    }

    // Token: 0x04001097 RID: 4247
    public const double ButtonWidth = 46.0;

    // Token: 0x04001098 RID: 4248
    private const double GlyphSize = 10.0;

    // Token: 0x04001099 RID: 4249
    private static readonly Color CloseRed = DS.Hex(12856092U, 1.0);

    // Token: 0x0400109A RID: 4250
    private readonly CaptionButton.Kind kind;

    // Token: 0x0400109B RID: 4251
    private readonly Border plate;

    // Token: 0x0400109C RID: 4252
    private readonly Path glyph;

    // Token: 0x0400109D RID: 4253
    private readonly Brush restInk;

    // Token: 0x0400109E RID: 4254
    private readonly Brush hoverInk;

    // Token: 0x02000969 RID: 2409
    [NullableContext(0)]
    public enum Kind
    {
      // Token: 0x0400278B RID: 10123
      Close,
      // Token: 0x0400278C RID: 10124
      Maximize,
      // Token: 0x0400278D RID: 10125
      Restore
    }
  }
}
