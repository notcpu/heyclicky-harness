using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000202 RID: 514
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class PaperStroke : IEquatable<PaperStroke>
  {
    // Token: 0x06001891 RID: 6289 RVA: 0x005CCF48 File Offset: 0x005CCF48
    public PaperStroke(Color Color, double Width = 1.0, PenLineCap Cap = PenLineCap.Flat, PenLineJoin Join = PenLineJoin.Miter, double[] Dash = null)
    {
      this.Color = Color;
      this.Width = Width;
      this.Cap = Cap;
      this.Join = Join;
      this.Dash = Dash;
      base..ctor();
    }

    // Token: 0x17000452 RID: 1106
    // (get) Token: 0x06001892 RID: 6290 RVA: 0x005CCF78 File Offset: 0x005CCF78
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(PaperStroke);
      }
    }

    // Token: 0x17000453 RID: 1107
    // (get) Token: 0x06001893 RID: 6291 RVA: 0x005CCF84 File Offset: 0x005CCF84
    // (set) Token: 0x06001894 RID: 6292 RVA: 0x005CCF8C File Offset: 0x005CCF8C
    public Color Color { get; set; }

    // Token: 0x17000454 RID: 1108
    // (get) Token: 0x06001895 RID: 6293 RVA: 0x005CCF98 File Offset: 0x005CCF98
    // (set) Token: 0x06001896 RID: 6294 RVA: 0x005CCFA0 File Offset: 0x005CCFA0
    public double Width { get; set; }

    // Token: 0x17000455 RID: 1109
    // (get) Token: 0x06001897 RID: 6295 RVA: 0x005CCFAC File Offset: 0x005CCFAC
    // (set) Token: 0x06001898 RID: 6296 RVA: 0x005CCFB4 File Offset: 0x005CCFB4
    public PenLineCap Cap { get; set; }

    // Token: 0x17000456 RID: 1110
    // (get) Token: 0x06001899 RID: 6297 RVA: 0x005CCFC0 File Offset: 0x005CCFC0
    // (set) Token: 0x0600189A RID: 6298 RVA: 0x005CCFC8 File Offset: 0x005CCFC8
    public PenLineJoin Join { get; set; }

    // Token: 0x17000457 RID: 1111
    // (get) Token: 0x0600189B RID: 6299 RVA: 0x005CCFD4 File Offset: 0x005CCFD4
    // (set) Token: 0x0600189C RID: 6300 RVA: 0x005CCFDC File Offset: 0x005CCFDC
    public double[] Dash { get; set; }

    // Token: 0x0600189D RID: 6301 RVA: 0x005CCFE8 File Offset: 0x005CCFE8
    [NullableContext(1)]
    public static PaperStroke Round(Color color, double width)
    {
      return new PaperStroke(color, width, PenLineCap.Round, PenLineJoin.Round, null);
    }

    // Token: 0x0600189E RID: 6302 RVA: 0x005CCFF4 File Offset: 0x005CCFF4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaperStroke");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600189F RID: 6303 RVA: 0x005CD040 File Offset: 0x005CD040
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Color = ");
      builder.Append(this.Color.ToString());
      builder.Append(", Width = ");
      builder.Append(this.Width.ToString());
      builder.Append(", Cap = ");
      builder.Append(this.Cap.ToString());
      builder.Append(", Join = ");
      builder.Append(this.Join.ToString());
      builder.Append(", Dash = ");
      builder.Append(this.Dash);
      return true;
    }

    // Token: 0x060018A0 RID: 6304 RVA: 0x005CD108 File Offset: 0x005CD108
    [CompilerGenerated]
    public static bool operator !=(PaperStroke left, PaperStroke right)
    {
      return !(left == right);
    }

    // Token: 0x060018A1 RID: 6305 RVA: 0x005CD114 File Offset: 0x005CD114
    [CompilerGenerated]
    public static bool operator ==(PaperStroke left, PaperStroke right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060018A2 RID: 6306 RVA: 0x005CD128 File Offset: 0x005CD128
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Width>k__BackingField)) * -1521134295 + EqualityComparer<PenLineCap>.Default.GetHashCode(this.<Cap>k__BackingField)) * -1521134295 + EqualityComparer<PenLineJoin>.Default.GetHashCode(this.<Join>k__BackingField)) * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(this.<Dash>k__BackingField);
    }

    // Token: 0x060018A3 RID: 6307 RVA: 0x005CD1B8 File Offset: 0x005CD1B8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaperStroke);
    }

    // Token: 0x060018A4 RID: 6308 RVA: 0x005CD1C8 File Offset: 0x005CD1C8
    [CompilerGenerated]
    public bool Equals(PaperStroke other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Width>k__BackingField, other.<Width>k__BackingField) && EqualityComparer<PenLineCap>.Default.Equals(this.<Cap>k__BackingField, other.<Cap>k__BackingField) && EqualityComparer<PenLineJoin>.Default.Equals(this.<Join>k__BackingField, other.<Join>k__BackingField) && EqualityComparer<double[]>.Default.Equals(this.<Dash>k__BackingField, other.<Dash>k__BackingField));
    }

    // Token: 0x060018A6 RID: 6310 RVA: 0x005CD278 File Offset: 0x005CD278
    [CompilerGenerated]
    private PaperStroke([Nullable(1)] PaperStroke original)
    {
      this.Color = original.<Color>k__BackingField;
      this.Width = original.<Width>k__BackingField;
      this.Cap = original.<Cap>k__BackingField;
      this.Join = original.<Join>k__BackingField;
      this.Dash = original.<Dash>k__BackingField;
    }

    // Token: 0x060018A7 RID: 6311 RVA: 0x005CD2C8 File Offset: 0x005CD2C8
    [CompilerGenerated]
    public void Deconstruct(out Color Color, out double Width, out PenLineCap Cap, out PenLineJoin Join, out double[] Dash)
    {
      Color = this.Color;
      Width = this.Width;
      Cap = this.Cap;
      Join = this.Join;
      Dash = this.Dash;
    }
  }
}
