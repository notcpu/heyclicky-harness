using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000182 RID: 386
  public readonly struct ClickyInkPenAttribute : IEquatable<ClickyInkPenAttribute>
  {
    // Token: 0x0600171B RID: 5915 RVA: 0x003C24E4 File Offset: 0x003C24E4
    public ClickyInkPenAttribute(double Wet, Color Tint)
    {
      this.Wet = Wet;
      this.Tint = Tint;
    }

    // Token: 0x1700053E RID: 1342
    // (get) Token: 0x0600171C RID: 5916 RVA: 0x003C24F4 File Offset: 0x003C24F4
    // (set) Token: 0x0600171D RID: 5917 RVA: 0x003C24FC File Offset: 0x003C24FC
    public double Wet { get; set; }

    // Token: 0x1700053F RID: 1343
    // (get) Token: 0x0600171E RID: 5918 RVA: 0x003C2508 File Offset: 0x003C2508
    // (set) Token: 0x0600171F RID: 5919 RVA: 0x003C2510 File Offset: 0x003C2510
    public Color Tint { get; set; }

    // Token: 0x06001720 RID: 5920 RVA: 0x003C251C File Offset: 0x003C251C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkPenAttribute");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001721 RID: 5921 RVA: 0x003C2568 File Offset: 0x003C2568
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Wet = ");
      builder.Append(this.Wet.ToString());
      builder.Append(", Tint = ");
      builder.Append(this.Tint.ToString());
      return true;
    }

    // Token: 0x06001722 RID: 5922 RVA: 0x003C25C4 File Offset: 0x003C25C4
    [CompilerGenerated]
    public static bool operator !=(ClickyInkPenAttribute left, ClickyInkPenAttribute right)
    {
      return !(left == right);
    }

    // Token: 0x06001723 RID: 5923 RVA: 0x003C25D0 File Offset: 0x003C25D0
    [CompilerGenerated]
    public static bool operator ==(ClickyInkPenAttribute left, ClickyInkPenAttribute right)
    {
      return left.Equals(right);
    }

    // Token: 0x06001724 RID: 5924 RVA: 0x003C25DC File Offset: 0x003C25DC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<Wet>k__BackingField) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Tint>k__BackingField);
    }

    // Token: 0x06001725 RID: 5925 RVA: 0x003C2608 File Offset: 0x003C2608
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is ClickyInkPenAttribute && this.Equals((ClickyInkPenAttribute)obj);
    }

    // Token: 0x06001726 RID: 5926 RVA: 0x003C2620 File Offset: 0x003C2620
    [CompilerGenerated]
    public bool Equals(ClickyInkPenAttribute other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Wet>k__BackingField, other.<Wet>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Tint>k__BackingField, other.<Tint>k__BackingField);
    }

    // Token: 0x06001727 RID: 5927 RVA: 0x003C2654 File Offset: 0x003C2654
    [CompilerGenerated]
    public void Deconstruct(out double Wet, out Color Tint)
    {
      Wet = this.Wet;
      Tint = this.Tint;
    }
  }
}
