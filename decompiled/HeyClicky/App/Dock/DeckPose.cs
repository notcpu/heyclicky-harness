using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032D RID: 813
  public readonly struct DeckPose : IEquatable<DeckPose>
  {
    // Token: 0x060022A7 RID: 8871 RVA: 0x0062712C File Offset: 0x0062712C
    public DeckPose(double X, double Y, double Scale, double Degrees, double Opacity)
    {
      this.X = X;
      this.Y = Y;
      this.Scale = Scale;
      this.Degrees = Degrees;
      this.Opacity = Opacity;
    }

    // Token: 0x17000664 RID: 1636
    // (get) Token: 0x060022A8 RID: 8872 RVA: 0x00627154 File Offset: 0x00627154
    // (set) Token: 0x060022A9 RID: 8873 RVA: 0x0062715C File Offset: 0x0062715C
    public double X { get; set; }

    // Token: 0x17000665 RID: 1637
    // (get) Token: 0x060022AA RID: 8874 RVA: 0x00627168 File Offset: 0x00627168
    // (set) Token: 0x060022AB RID: 8875 RVA: 0x00627170 File Offset: 0x00627170
    public double Y { get; set; }

    // Token: 0x17000666 RID: 1638
    // (get) Token: 0x060022AC RID: 8876 RVA: 0x0062717C File Offset: 0x0062717C
    // (set) Token: 0x060022AD RID: 8877 RVA: 0x00627184 File Offset: 0x00627184
    public double Scale { get; set; }

    // Token: 0x17000667 RID: 1639
    // (get) Token: 0x060022AE RID: 8878 RVA: 0x00627190 File Offset: 0x00627190
    // (set) Token: 0x060022AF RID: 8879 RVA: 0x00627198 File Offset: 0x00627198
    public double Degrees { get; set; }

    // Token: 0x17000668 RID: 1640
    // (get) Token: 0x060022B0 RID: 8880 RVA: 0x006271A4 File Offset: 0x006271A4
    // (set) Token: 0x060022B1 RID: 8881 RVA: 0x006271AC File Offset: 0x006271AC
    public double Opacity { get; set; }

    // Token: 0x060022B2 RID: 8882 RVA: 0x006271B8 File Offset: 0x006271B8
    public static DeckPose operator +(DeckPose a, DeckPose b)
    {
      return new DeckPose(a.X + b.X, a.Y + b.Y, a.Scale + b.Scale, a.Degrees + b.Degrees, a.Opacity + b.Opacity);
    }

    // Token: 0x060022B3 RID: 8883 RVA: 0x00627218 File Offset: 0x00627218
    public static DeckPose operator -(DeckPose a, DeckPose b)
    {
      return new DeckPose(a.X - b.X, a.Y - b.Y, a.Scale - b.Scale, a.Degrees - b.Degrees, a.Opacity - b.Opacity);
    }

    // Token: 0x060022B4 RID: 8884 RVA: 0x00627278 File Offset: 0x00627278
    public static DeckPose operator *(DeckPose a, double k)
    {
      return new DeckPose(a.X * k, a.Y * k, a.Scale * k, a.Degrees * k, a.Opacity * k);
    }

    // Token: 0x060022B5 RID: 8885 RVA: 0x006272AC File Offset: 0x006272AC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DeckPose");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060022B6 RID: 8886 RVA: 0x006272F8 File Offset: 0x006272F8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("X = ");
      builder.Append(this.X.ToString());
      builder.Append(", Y = ");
      builder.Append(this.Y.ToString());
      builder.Append(", Scale = ");
      builder.Append(this.Scale.ToString());
      builder.Append(", Degrees = ");
      builder.Append(this.Degrees.ToString());
      builder.Append(", Opacity = ");
      builder.Append(this.Opacity.ToString());
      return true;
    }

    // Token: 0x060022B7 RID: 8887 RVA: 0x006273CC File Offset: 0x006273CC
    [CompilerGenerated]
    public static bool operator !=(DeckPose left, DeckPose right)
    {
      return !(left == right);
    }

    // Token: 0x060022B8 RID: 8888 RVA: 0x006273D8 File Offset: 0x006273D8
    [CompilerGenerated]
    public static bool operator ==(DeckPose left, DeckPose right)
    {
      return left.Equals(right);
    }

    // Token: 0x060022B9 RID: 8889 RVA: 0x006273E4 File Offset: 0x006273E4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<double>.Default.GetHashCode(this.<X>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Scale>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Degrees>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Opacity>k__BackingField);
    }

    // Token: 0x060022BA RID: 8890 RVA: 0x00627460 File Offset: 0x00627460
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is DeckPose && this.Equals((DeckPose)obj);
    }

    // Token: 0x060022BB RID: 8891 RVA: 0x00627478 File Offset: 0x00627478
    [CompilerGenerated]
    public bool Equals(DeckPose other)
    {
      return EqualityComparer<double>.Default.Equals(this.<X>k__BackingField, other.<X>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y>k__BackingField, other.<Y>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Scale>k__BackingField, other.<Scale>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Degrees>k__BackingField, other.<Degrees>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Opacity>k__BackingField, other.<Opacity>k__BackingField);
    }

    // Token: 0x060022BC RID: 8892 RVA: 0x00627500 File Offset: 0x00627500
    [CompilerGenerated]
    public void Deconstruct(out double X, out double Y, out double Scale, out double Degrees, out double Opacity)
    {
      X = this.X;
      Y = this.Y;
      Scale = this.Scale;
      Degrees = this.Degrees;
      Opacity = this.Opacity;
    }

    // Token: 0x040010DA RID: 4314
    public static readonly DeckPose Zero = new DeckPose(0.0, 0.0, 0.0, 0.0, 0.0);
  }
}
