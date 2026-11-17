using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000205 RID: 517
  [NullableContext(1)]
  [Nullable(0)]
  public abstract class PaperFill : IEquatable<PaperFill>
  {
    // Token: 0x1700045B RID: 1115
    // (get) Token: 0x060018BC RID: 6332 RVA: 0x005CD7A8 File Offset: 0x005CD7A8
    [CompilerGenerated]
    protected virtual Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaperFill);
      }
    }

    // Token: 0x060018BD RID: 6333 RVA: 0x005CD7B4 File Offset: 0x005CD7B4
    public static PaperFill Of(Color color)
    {
      return new PaperFill.Solid(color);
    }

    // Token: 0x060018BE RID: 6334 RVA: 0x005CD7BC File Offset: 0x005CD7BC
    public static PaperFill Vertical([TupleElementNames(new string[] { "Offset", "Color" })] [Nullable(new byte[] { 1, 0 })] params ValueTuple<double, Color>[] stops)
    {
      return new PaperFill.Linear(stops, new Point(0.5, 0.0), new Point(0.5, 1.0));
    }

    // Token: 0x060018BF RID: 6335 RVA: 0x005CD7F4 File Offset: 0x005CD7F4
    public static PaperFill Horizontal([TupleElementNames(new string[] { "Offset", "Color" })] [Nullable(new byte[] { 1, 0 })] params ValueTuple<double, Color>[] stops)
    {
      return new PaperFill.Linear(stops, new Point(0.0, 0.5), new Point(1.0, 0.5));
    }

    // Token: 0x060018C0 RID: 6336 RVA: 0x005CD82C File Offset: 0x005CD82C
    public static PaperFill Evenly(Point start, Point end, params Color[] colors)
    {
      return new PaperFill.Linear(colors.Select<Color, ValueTuple<double, Color>>((Color c, int i) => new ValueTuple<double, Color>((colors.Length == 1) ? 0.0 : ((double)i / (double)(colors.Length - 1)), c)).ToArray<ValueTuple<double, Color>>(), start, end);
    }

    // Token: 0x060018C1 RID: 6337 RVA: 0x005CD86C File Offset: 0x005CD86C
    internal ICanvasBrush Brush(ICanvasResourceCreator creator, Rect frame)
    {
      PaperFill.Solid solid = this as PaperFill.Solid;
      ICanvasBrush canvasBrush;
      if (solid == null)
      {
        PaperFill.Linear linear = this as PaperFill.Linear;
        if (linear == null)
        {
          throw new InvalidOperationException();
        }
        canvasBrush = new CanvasLinearGradientBrush(creator, linear.Stops.Select<ValueTuple<double, Color>, CanvasGradientStop>(([TupleElementNames(new string[] { "Offset", "Color" })] ValueTuple<double, Color> st) => new CanvasGradientStop
        {
          Position = (float)st.Item1,
          Color = st.Item2
        }).ToArray<CanvasGradientStop>(), CanvasEdgeBehavior.Clamp, CanvasAlphaMode.Premultiplied)
        {
          StartPoint = new Vector2((float)(frame.X + linear.Start.X * frame.Width), (float)(frame.Y + linear.Start.Y * frame.Height)),
          EndPoint = new Vector2((float)(frame.X + linear.End.X * frame.Width), (float)(frame.Y + linear.End.Y * frame.Height))
        };
      }
      else
      {
        canvasBrush = new CanvasSolidColorBrush(creator, solid.Color);
      }
      return canvasBrush;
    }

    // Token: 0x060018C2 RID: 6338 RVA: 0x005CD980 File Offset: 0x005CD980
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaperFill");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018C3 RID: 6339 RVA: 0x005CD9CC File Offset: 0x005CD9CC
    [CompilerGenerated]
    protected virtual bool PrintMembers(StringBuilder builder)
    {
      return false;
    }

    // Token: 0x060018C4 RID: 6340 RVA: 0x005CD9D0 File Offset: 0x005CD9D0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaperFill left, PaperFill right)
    {
      return !(left == right);
    }

    // Token: 0x060018C5 RID: 6341 RVA: 0x005CD9DC File Offset: 0x005CD9DC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaperFill left, PaperFill right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060018C6 RID: 6342 RVA: 0x005CD9F0 File Offset: 0x005CD9F0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
    }

    // Token: 0x060018C7 RID: 6343 RVA: 0x005CDA04 File Offset: 0x005CDA04
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaperFill);
    }

    // Token: 0x060018C8 RID: 6344 RVA: 0x005CDA14 File Offset: 0x005CDA14
    [NullableContext(2)]
    [CompilerGenerated]
    public virtual bool Equals(PaperFill other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract);
    }

    // Token: 0x060018CA RID: 6346 RVA: 0x005CDA34 File Offset: 0x005CDA34
    [CompilerGenerated]
    protected PaperFill(PaperFill original)
    {
    }

    // Token: 0x060018CB RID: 6347 RVA: 0x005CDA3C File Offset: 0x005CDA3C
    protected PaperFill()
    {
    }

    // Token: 0x020007A3 RID: 1955
    [Nullable(0)]
    public sealed class Solid : PaperFill, IEquatable<PaperFill.Solid>
    {
      // Token: 0x0600364B RID: 13899 RVA: 0x0067E210 File Offset: 0x0067E210
      public Solid(Color Color)
      {
        this.Color = Color;
        base..ctor();
      }

      // Token: 0x170007D7 RID: 2007
      // (get) Token: 0x0600364C RID: 13900 RVA: 0x0067E220 File Offset: 0x0067E220
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(PaperFill.Solid);
        }
      }

      // Token: 0x170007D8 RID: 2008
      // (get) Token: 0x0600364D RID: 13901 RVA: 0x0067E22C File Offset: 0x0067E22C
      // (set) Token: 0x0600364E RID: 13902 RVA: 0x0067E234 File Offset: 0x0067E234
      public Color Color { get; set; }

      // Token: 0x0600364F RID: 13903 RVA: 0x0067E240 File Offset: 0x0067E240
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Solid");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003650 RID: 13904 RVA: 0x0067E28C File Offset: 0x0067E28C
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Color = ");
        builder.Append(this.Color.ToString());
        return true;
      }

      // Token: 0x06003651 RID: 13905 RVA: 0x0067E2D8 File Offset: 0x0067E2D8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(PaperFill.Solid left, PaperFill.Solid right)
      {
        return !(left == right);
      }

      // Token: 0x06003652 RID: 13906 RVA: 0x0067E2E4 File Offset: 0x0067E2E4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(PaperFill.Solid left, PaperFill.Solid right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003653 RID: 13907 RVA: 0x0067E2F8 File Offset: 0x0067E2F8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return base.GetHashCode() * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField);
      }

      // Token: 0x06003654 RID: 13908 RVA: 0x0067E318 File Offset: 0x0067E318
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as PaperFill.Solid);
      }

      // Token: 0x06003655 RID: 13909 RVA: 0x0067E328 File Offset: 0x0067E328
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(PaperFill other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003656 RID: 13910 RVA: 0x0067E334 File Offset: 0x0067E334
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(PaperFill.Solid other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField));
      }

      // Token: 0x06003658 RID: 13912 RVA: 0x0067E368 File Offset: 0x0067E368
      [CompilerGenerated]
      private Solid(PaperFill.Solid original)
        : base(original)
      {
        this.Color = original.<Color>k__BackingField;
      }

      // Token: 0x06003659 RID: 13913 RVA: 0x0067E380 File Offset: 0x0067E380
      [CompilerGenerated]
      public void Deconstruct(out Color Color)
      {
        Color = this.Color;
      }
    }

    // Token: 0x020007A4 RID: 1956
    [Nullable(0)]
    public sealed class Linear : PaperFill, IEquatable<PaperFill.Linear>
    {
      // Token: 0x0600365A RID: 13914 RVA: 0x0067E390 File Offset: 0x0067E390
      public Linear([TupleElementNames(new string[] { "Offset", "Color" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<double, Color>[] Stops, Point Start, Point End)
      {
        this.Stops = Stops;
        this.Start = Start;
        this.End = End;
        base..ctor();
      }

      // Token: 0x170007D9 RID: 2009
      // (get) Token: 0x0600365B RID: 13915 RVA: 0x0067E3B0 File Offset: 0x0067E3B0
      [CompilerGenerated]
      protected override Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(PaperFill.Linear);
        }
      }

      // Token: 0x170007DA RID: 2010
      // (get) Token: 0x0600365C RID: 13916 RVA: 0x0067E3BC File Offset: 0x0067E3BC
      // (set) Token: 0x0600365D RID: 13917 RVA: 0x0067E3C4 File Offset: 0x0067E3C4
      [TupleElementNames(new string[] { "Offset", "Color" })]
      [Nullable(new byte[] { 1, 0 })]
      public ValueTuple<double, Color>[] Stops
      {
        [return: TupleElementNames(new string[] { "Offset", "Color" })]
        [return: Nullable(new byte[] { 1, 0 })]
        get;
        [param: TupleElementNames(new string[] { "Offset", "Color" })]
        [param: Nullable(new byte[] { 1, 0 })]
        set;
      }

      // Token: 0x170007DB RID: 2011
      // (get) Token: 0x0600365E RID: 13918 RVA: 0x0067E3D0 File Offset: 0x0067E3D0
      // (set) Token: 0x0600365F RID: 13919 RVA: 0x0067E3D8 File Offset: 0x0067E3D8
      public Point Start { get; set; }

      // Token: 0x170007DC RID: 2012
      // (get) Token: 0x06003660 RID: 13920 RVA: 0x0067E3E4 File Offset: 0x0067E3E4
      // (set) Token: 0x06003661 RID: 13921 RVA: 0x0067E3EC File Offset: 0x0067E3EC
      public Point End { get; set; }

      // Token: 0x06003662 RID: 13922 RVA: 0x0067E3F8 File Offset: 0x0067E3F8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Linear");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003663 RID: 13923 RVA: 0x0067E444 File Offset: 0x0067E444
      [CompilerGenerated]
      protected override bool PrintMembers(StringBuilder builder)
      {
        if (base.PrintMembers(builder))
        {
          builder.Append(", ");
        }
        builder.Append("Stops = ");
        builder.Append(this.Stops);
        builder.Append(", Start = ");
        builder.Append(this.Start.ToString());
        builder.Append(", End = ");
        builder.Append(this.End.ToString());
        return true;
      }

      // Token: 0x06003664 RID: 13924 RVA: 0x0067E4D0 File Offset: 0x0067E4D0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(PaperFill.Linear left, PaperFill.Linear right)
      {
        return !(left == right);
      }

      // Token: 0x06003665 RID: 13925 RVA: 0x0067E4DC File Offset: 0x0067E4DC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(PaperFill.Linear left, PaperFill.Linear right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003666 RID: 13926 RVA: 0x0067E4F0 File Offset: 0x0067E4F0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((base.GetHashCode() * -1521134295 + EqualityComparer<ValueTuple<double, Color>[]>.Default.GetHashCode(this.<Stops>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<Start>k__BackingField)) * -1521134295 + EqualityComparer<Point>.Default.GetHashCode(this.<End>k__BackingField);
      }

      // Token: 0x06003667 RID: 13927 RVA: 0x0067E548 File Offset: 0x0067E548
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as PaperFill.Linear);
      }

      // Token: 0x06003668 RID: 13928 RVA: 0x0067E558 File Offset: 0x0067E558
      [NullableContext(2)]
      [CompilerGenerated]
      public sealed override bool Equals(PaperFill other)
      {
        return this.Equals(other);
      }

      // Token: 0x06003669 RID: 13929 RVA: 0x0067E564 File Offset: 0x0067E564
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(PaperFill.Linear other)
      {
        return this == other || (base.Equals(other) && EqualityComparer<ValueTuple<double, Color>[]>.Default.Equals(this.<Stops>k__BackingField, other.<Stops>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<Start>k__BackingField, other.<Start>k__BackingField) && EqualityComparer<Point>.Default.Equals(this.<End>k__BackingField, other.<End>k__BackingField));
      }

      // Token: 0x0600366B RID: 13931 RVA: 0x0067E5D0 File Offset: 0x0067E5D0
      [CompilerGenerated]
      private Linear(PaperFill.Linear original)
        : base(original)
      {
        this.Stops = original.<Stops>k__BackingField;
        this.Start = original.<Start>k__BackingField;
        this.End = original.<End>k__BackingField;
      }

      // Token: 0x0600366C RID: 13932 RVA: 0x0067E600 File Offset: 0x0067E600
      [CompilerGenerated]
      public void Deconstruct([TupleElementNames(new string[] { "Offset", "Color" })] [Nullable(new byte[] { 1, 0 })] out ValueTuple<double, Color>[] Stops, out Point Start, out Point End)
      {
        Stops = this.Stops;
        Start = this.Start;
        End = this.End;
      }
    }
  }
}
