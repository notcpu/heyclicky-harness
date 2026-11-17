using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000206 RID: 518
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PaperShadow : IEquatable<PaperShadow>
  {
    // Token: 0x060018CC RID: 6348 RVA: 0x005CDA44 File Offset: 0x005CDA44
    public PaperShadow(Color Color, double Radius, double X = 0.0, double Y = 0.0)
    {
      this.Color = Color;
      this.Radius = Radius;
      this.X = X;
      this.Y = Y;
      base..ctor();
    }

    // Token: 0x1700045C RID: 1116
    // (get) Token: 0x060018CD RID: 6349 RVA: 0x005CDA6C File Offset: 0x005CDA6C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PaperShadow);
      }
    }

    // Token: 0x1700045D RID: 1117
    // (get) Token: 0x060018CE RID: 6350 RVA: 0x005CDA78 File Offset: 0x005CDA78
    // (set) Token: 0x060018CF RID: 6351 RVA: 0x005CDA80 File Offset: 0x005CDA80
    public Color Color { get; set; }

    // Token: 0x1700045E RID: 1118
    // (get) Token: 0x060018D0 RID: 6352 RVA: 0x005CDA8C File Offset: 0x005CDA8C
    // (set) Token: 0x060018D1 RID: 6353 RVA: 0x005CDA94 File Offset: 0x005CDA94
    public double Radius { get; set; }

    // Token: 0x1700045F RID: 1119
    // (get) Token: 0x060018D2 RID: 6354 RVA: 0x005CDAA0 File Offset: 0x005CDAA0
    // (set) Token: 0x060018D3 RID: 6355 RVA: 0x005CDAA8 File Offset: 0x005CDAA8
    public double X { get; set; }

    // Token: 0x17000460 RID: 1120
    // (get) Token: 0x060018D4 RID: 6356 RVA: 0x005CDAB4 File Offset: 0x005CDAB4
    // (set) Token: 0x060018D5 RID: 6357 RVA: 0x005CDABC File Offset: 0x005CDABC
    public double Y { get; set; }

    // Token: 0x060018D6 RID: 6358 RVA: 0x005CDAC8 File Offset: 0x005CDAC8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PaperShadow");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060018D7 RID: 6359 RVA: 0x005CDB14 File Offset: 0x005CDB14
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Color = ");
      builder.Append(this.Color.ToString());
      builder.Append(", Radius = ");
      builder.Append(this.Radius.ToString());
      builder.Append(", X = ");
      builder.Append(this.X.ToString());
      builder.Append(", Y = ");
      builder.Append(this.Y.ToString());
      return true;
    }

    // Token: 0x060018D8 RID: 6360 RVA: 0x005CDBC4 File Offset: 0x005CDBC4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PaperShadow left, PaperShadow right)
    {
      return !(left == right);
    }

    // Token: 0x060018D9 RID: 6361 RVA: 0x005CDBD0 File Offset: 0x005CDBD0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PaperShadow left, PaperShadow right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060018DA RID: 6362 RVA: 0x005CDBE4 File Offset: 0x005CDBE4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Radius>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField);
    }

    // Token: 0x060018DB RID: 6363 RVA: 0x005CDC60 File Offset: 0x005CDC60
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PaperShadow);
    }

    // Token: 0x060018DC RID: 6364 RVA: 0x005CDC70 File Offset: 0x005CDC70
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PaperShadow other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Radius>k__BackingField, other.<Radius>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField));
    }

    // Token: 0x060018DE RID: 6366 RVA: 0x005CDD04 File Offset: 0x005CDD04
    [CompilerGenerated]
    private PaperShadow(PaperShadow original)
    {
      this.Color = original.<Color>k__BackingField;
      this.Radius = original.<Radius>k__BackingField;
      this.X = original.<X>k__BackingField;
      this.Y = original.<Y>k__BackingField;
    }

    // Token: 0x060018DF RID: 6367 RVA: 0x005CDD3C File Offset: 0x005CDD3C
    [CompilerGenerated]
    public void Deconstruct(out Color Color, out double Radius, out double X, out double Y)
    {
      Color = this.Color;
      Radius = this.Radius;
      X = this.X;
      Y = this.Y;
    }

    // Token: 0x04000C28 RID: 3112
    public const double SigmaPerRadius = 0.5;
  }
}
