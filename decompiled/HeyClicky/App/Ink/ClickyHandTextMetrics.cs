using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001B1 RID: 433
  public readonly struct ClickyHandTextMetrics : IEquatable<ClickyHandTextMetrics>
  {
    // Token: 0x06001703 RID: 5891 RVA: 0x005BECDC File Offset: 0x005BECDC
    public ClickyHandTextMetrics(double Pitch, double HalfLeading)
    {
      this.Pitch = Pitch;
      this.HalfLeading = HalfLeading;
    }

    // Token: 0x17000412 RID: 1042
    // (get) Token: 0x06001704 RID: 5892 RVA: 0x005BECEC File Offset: 0x005BECEC
    // (set) Token: 0x06001705 RID: 5893 RVA: 0x005BECF4 File Offset: 0x005BECF4
    public double Pitch { get; set; }

    // Token: 0x17000413 RID: 1043
    // (get) Token: 0x06001706 RID: 5894 RVA: 0x005BED00 File Offset: 0x005BED00
    // (set) Token: 0x06001707 RID: 5895 RVA: 0x005BED08 File Offset: 0x005BED08
    public double HalfLeading { get; set; }

    // Token: 0x06001708 RID: 5896 RVA: 0x005BED14 File Offset: 0x005BED14
    [NullableContext(1)]
    public static ClickyHandTextMetrics For(PaperFont font, double size, double lineHeight)
    {
      double num = size * lineHeight;
      return new ClickyHandTextMetrics(num, (num - ClickyHandLineBreaker.NaturalLineHeight(font)) / 2.0);
    }

    // Token: 0x06001709 RID: 5897 RVA: 0x005BED30 File Offset: 0x005BED30
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyHandTextMetrics");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600170A RID: 5898 RVA: 0x005BED7C File Offset: 0x005BED7C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Pitch = ");
      builder.Append(this.Pitch.ToString());
      builder.Append(", HalfLeading = ");
      builder.Append(this.HalfLeading.ToString());
      return true;
    }

    // Token: 0x0600170B RID: 5899 RVA: 0x005BEDD8 File Offset: 0x005BEDD8
    [CompilerGenerated]
    public static bool operator !=(ClickyHandTextMetrics left, ClickyHandTextMetrics right)
    {
      return !(left == right);
    }

    // Token: 0x0600170C RID: 5900 RVA: 0x005BEDE4 File Offset: 0x005BEDE4
    [CompilerGenerated]
    public static bool operator ==(ClickyHandTextMetrics left, ClickyHandTextMetrics right)
    {
      return left.Equals(right);
    }

    // Token: 0x0600170D RID: 5901 RVA: 0x005BEDF0 File Offset: 0x005BEDF0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<Pitch>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<HalfLeading>k__BackingField);
    }

    // Token: 0x0600170E RID: 5902 RVA: 0x005BEE1C File Offset: 0x005BEE1C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyHandTextMetrics && this.Equals((ClickyHandTextMetrics)obj);
    }

    // Token: 0x0600170F RID: 5903 RVA: 0x005BEE34 File Offset: 0x005BEE34
    [CompilerGenerated]
    public bool Equals(ClickyHandTextMetrics other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Pitch>k__BackingField, other.<Pitch>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<HalfLeading>k__BackingField, other.<HalfLeading>k__BackingField);
    }

    // Token: 0x06001710 RID: 5904 RVA: 0x005BEE68 File Offset: 0x005BEE68
    [CompilerGenerated]
    public void Deconstruct(out double Pitch, out double HalfLeading)
    {
      Pitch = this.Pitch;
      HalfLeading = this.HalfLeading;
    }
  }
}
