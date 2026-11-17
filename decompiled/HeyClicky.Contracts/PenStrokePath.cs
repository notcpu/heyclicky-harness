using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000026 RID: 38
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PenStrokePath : IEquatable<PenStrokePath>
  {
    // Token: 0x060001E1 RID: 481 RVA: 0x0001F934 File Offset: 0x0001F934
    public PenStrokePath(IReadOnlyList<PointD> PointsPx, double BeginTime, double Duration, double X1, double Y1, double X2, double Y2)
    {
      this.PointsPx = PointsPx;
      this.BeginTime = BeginTime;
      this.Duration = Duration;
      this.X1 = X1;
      this.Y1 = Y1;
      this.X2 = X2;
      this.Y2 = Y2;
      base..ctor();
    }

    // Token: 0x1700007F RID: 127
    // (get) Token: 0x060001E2 RID: 482 RVA: 0x0001F974 File Offset: 0x0001F974
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PenStrokePath);
      }
    }

    // Token: 0x17000080 RID: 128
    // (get) Token: 0x060001E3 RID: 483 RVA: 0x0001F980 File Offset: 0x0001F980
    // (set) Token: 0x060001E4 RID: 484 RVA: 0x0001F988 File Offset: 0x0001F988
    public IReadOnlyList<PointD> PointsPx { get; set; }

    // Token: 0x17000081 RID: 129
    // (get) Token: 0x060001E5 RID: 485 RVA: 0x0001F994 File Offset: 0x0001F994
    // (set) Token: 0x060001E6 RID: 486 RVA: 0x0001F99C File Offset: 0x0001F99C
    public double BeginTime { get; set; }

    // Token: 0x17000082 RID: 130
    // (get) Token: 0x060001E7 RID: 487 RVA: 0x0001F9A8 File Offset: 0x0001F9A8
    // (set) Token: 0x060001E8 RID: 488 RVA: 0x0001F9B0 File Offset: 0x0001F9B0
    public double Duration { get; set; }

    // Token: 0x17000083 RID: 131
    // (get) Token: 0x060001E9 RID: 489 RVA: 0x0001F9BC File Offset: 0x0001F9BC
    // (set) Token: 0x060001EA RID: 490 RVA: 0x0001F9C4 File Offset: 0x0001F9C4
    public double X1 { get; set; }

    // Token: 0x17000084 RID: 132
    // (get) Token: 0x060001EB RID: 491 RVA: 0x0001F9D0 File Offset: 0x0001F9D0
    // (set) Token: 0x060001EC RID: 492 RVA: 0x0001F9D8 File Offset: 0x0001F9D8
    public double Y1 { get; set; }

    // Token: 0x17000085 RID: 133
    // (get) Token: 0x060001ED RID: 493 RVA: 0x0001F9E4 File Offset: 0x0001F9E4
    // (set) Token: 0x060001EE RID: 494 RVA: 0x0001F9EC File Offset: 0x0001F9EC
    public double X2 { get; set; }

    // Token: 0x17000086 RID: 134
    // (get) Token: 0x060001EF RID: 495 RVA: 0x0001F9F8 File Offset: 0x0001F9F8
    // (set) Token: 0x060001F0 RID: 496 RVA: 0x0001FA00 File Offset: 0x0001FA00
    public double Y2 { get; set; }

    // Token: 0x060001F1 RID: 497 RVA: 0x0001FA0C File Offset: 0x0001FA0C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PenStrokePath");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060001F2 RID: 498 RVA: 0x0001FA58 File Offset: 0x0001FA58
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("PointsPx = ");
      builder.Append(this.PointsPx);
      builder.Append(", BeginTime = ");
      builder.Append(this.BeginTime.ToString());
      builder.Append(", Duration = ");
      builder.Append(this.Duration.ToString());
      builder.Append(", X1 = ");
      builder.Append(this.X1.ToString());
      builder.Append(", Y1 = ");
      builder.Append(this.Y1.ToString());
      builder.Append(", X2 = ");
      builder.Append(this.X2.ToString());
      builder.Append(", Y2 = ");
      builder.Append(this.Y2.ToString());
      return true;
    }

    // Token: 0x060001F3 RID: 499 RVA: 0x0001FB70 File Offset: 0x0001FB70
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PenStrokePath left, PenStrokePath right)
    {
      return !(left == right);
    }

    // Token: 0x060001F4 RID: 500 RVA: 0x0001FB7C File Offset: 0x0001FB7C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PenStrokePath left, PenStrokePath right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060001F5 RID: 501 RVA: 0x0001FB90 File Offset: 0x0001FB90
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<PointD>>.Default.GetHashCode(this.<PointsPx>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<BeginTime>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Duration>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<X1>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y1>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<X2>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Y2>k__BackingField);
    }

    // Token: 0x060001F6 RID: 502 RVA: 0x0001FC50 File Offset: 0x0001FC50
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PenStrokePath);
    }

    // Token: 0x060001F7 RID: 503 RVA: 0x0001FC60 File Offset: 0x0001FC60
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PenStrokePath other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<PointD>>.Default.Equals(this.<PointsPx>k__BackingField, other.<PointsPx>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<BeginTime>k__BackingField, other.<BeginTime>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Duration>k__BackingField, other.<Duration>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<X1>k__BackingField, other.<X1>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y1>k__BackingField, other.<Y1>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<X2>k__BackingField, other.<X2>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Y2>k__BackingField, other.<Y2>k__BackingField));
    }

    // Token: 0x060001F9 RID: 505 RVA: 0x0001FD48 File Offset: 0x0001FD48
    [CompilerGenerated]
    private PenStrokePath(PenStrokePath original)
    {
      this.PointsPx = original.<PointsPx>k__BackingField;
      this.BeginTime = original.<BeginTime>k__BackingField;
      this.Duration = original.<Duration>k__BackingField;
      this.X1 = original.<X1>k__BackingField;
      this.Y1 = original.<Y1>k__BackingField;
      this.X2 = original.<X2>k__BackingField;
      this.Y2 = original.<Y2>k__BackingField;
    }

    // Token: 0x060001FA RID: 506 RVA: 0x0001FDB0 File Offset: 0x0001FDB0
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<PointD> PointsPx, out double BeginTime, out double Duration, out double X1, out double Y1, out double X2, out double Y2)
    {
      PointsPx = this.PointsPx;
      BeginTime = this.BeginTime;
      Duration = this.Duration;
      X1 = this.X1;
      Y1 = this.Y1;
      X2 = this.X2;
      Y2 = this.Y2;
    }
  }
}
