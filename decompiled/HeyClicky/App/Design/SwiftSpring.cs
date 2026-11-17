using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x02000348 RID: 840
  public readonly struct SwiftSpring : IEquatable<SwiftSpring>
  {
    // Token: 0x060023C5 RID: 9157 RVA: 0x0062F8BC File Offset: 0x0062F8BC
    public SwiftSpring(double Response, double DampingFraction)
    {
      this.Response = Response;
      this.DampingFraction = DampingFraction;
    }

    // Token: 0x1700068E RID: 1678
    // (get) Token: 0x060023C6 RID: 9158 RVA: 0x0062F8CC File Offset: 0x0062F8CC
    // (set) Token: 0x060023C7 RID: 9159 RVA: 0x0062F8D4 File Offset: 0x0062F8D4
    public double Response { get; set; }

    // Token: 0x1700068F RID: 1679
    // (get) Token: 0x060023C8 RID: 9160 RVA: 0x0062F8E0 File Offset: 0x0062F8E0
    // (set) Token: 0x060023C9 RID: 9161 RVA: 0x0062F8E8 File Offset: 0x0062F8E8
    public double DampingFraction { get; set; }

    // Token: 0x17000690 RID: 1680
    // (get) Token: 0x060023CA RID: 9162 RVA: 0x0062F8F4 File Offset: 0x0062F8F4
    public TimeSpan Period
    {
      get
      {
        return TimeSpan.FromSeconds(this.Response / 6.283185307179586);
      }
    }

    // Token: 0x17000691 RID: 1681
    // (get) Token: 0x060023CB RID: 9163 RVA: 0x0062F90C File Offset: 0x0062F90C
    public float DampingRatio
    {
      get
      {
        return (float)this.DampingFraction;
      }
    }

    // Token: 0x17000692 RID: 1682
    // (get) Token: 0x060023CC RID: 9164 RVA: 0x0062F918 File Offset: 0x0062F918
    public TimeSpan SettleTime
    {
      get
      {
        double num = 6.283185307179586 / this.Response;
        double num2 = Math.Max(0.05, Math.Min(1.0, this.DampingFraction));
        return TimeSpan.FromSeconds(Math.Min(2.0, 6.9 / (num2 * num)));
      }
    }

    // Token: 0x060023CD RID: 9165 RVA: 0x0062F97C File Offset: 0x0062F97C
    [NullableContext(1)]
    public SpringScalarNaturalMotionAnimation Scalar(Compositor c, float to)
    {
      SpringScalarNaturalMotionAnimation springScalarNaturalMotionAnimation = c.CreateSpringScalarAnimation();
      springScalarNaturalMotionAnimation.DampingRatio = this.DampingRatio;
      springScalarNaturalMotionAnimation.Period = this.Period;
      springScalarNaturalMotionAnimation.FinalValue = new float?(to);
      springScalarNaturalMotionAnimation.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;
      return springScalarNaturalMotionAnimation;
    }

    // Token: 0x060023CE RID: 9166 RVA: 0x0062F9B0 File Offset: 0x0062F9B0
    [NullableContext(1)]
    public SpringVector2NaturalMotionAnimation Vector2(Compositor c, Vector2 to)
    {
      SpringVector2NaturalMotionAnimation springVector2NaturalMotionAnimation = c.CreateSpringVector2Animation();
      springVector2NaturalMotionAnimation.DampingRatio = this.DampingRatio;
      springVector2NaturalMotionAnimation.Period = this.Period;
      springVector2NaturalMotionAnimation.FinalValue = new Vector2?(to);
      springVector2NaturalMotionAnimation.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;
      return springVector2NaturalMotionAnimation;
    }

    // Token: 0x060023CF RID: 9167 RVA: 0x0062F9E4 File Offset: 0x0062F9E4
    [NullableContext(1)]
    public SpringVector3NaturalMotionAnimation Vector3(Compositor c, Vector3 to)
    {
      SpringVector3NaturalMotionAnimation springVector3NaturalMotionAnimation = c.CreateSpringVector3Animation();
      springVector3NaturalMotionAnimation.DampingRatio = this.DampingRatio;
      springVector3NaturalMotionAnimation.Period = this.Period;
      springVector3NaturalMotionAnimation.FinalValue = new Vector3?(to);
      springVector3NaturalMotionAnimation.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;
      return springVector3NaturalMotionAnimation;
    }

    // Token: 0x060023D0 RID: 9168 RVA: 0x0062FA18 File Offset: 0x0062FA18
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SwiftSpring");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060023D1 RID: 9169 RVA: 0x0062FA64 File Offset: 0x0062FA64
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Response = ");
      builder.Append(this.Response.ToString());
      builder.Append(", DampingFraction = ");
      builder.Append(this.DampingFraction.ToString());
      builder.Append(", Period = ");
      builder.Append(this.Period.ToString());
      builder.Append(", DampingRatio = ");
      builder.Append(this.DampingRatio.ToString());
      builder.Append(", SettleTime = ");
      builder.Append(this.SettleTime.ToString());
      return true;
    }

    // Token: 0x060023D2 RID: 9170 RVA: 0x0062FB38 File Offset: 0x0062FB38
    [CompilerGenerated]
    public static bool operator !=(SwiftSpring left, SwiftSpring right)
    {
      return !(left == right);
    }

    // Token: 0x060023D3 RID: 9171 RVA: 0x0062FB44 File Offset: 0x0062FB44
    [CompilerGenerated]
    public static bool operator ==(SwiftSpring left, SwiftSpring right)
    {
      return left.Equals(right);
    }

    // Token: 0x060023D4 RID: 9172 RVA: 0x0062FB50 File Offset: 0x0062FB50
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<double>.Default.GetHashCode(this.<Response>k__BackingField) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<DampingFraction>k__BackingField);
    }

    // Token: 0x060023D5 RID: 9173 RVA: 0x0062FB7C File Offset: 0x0062FB7C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is SwiftSpring && this.Equals((SwiftSpring)obj);
    }

    // Token: 0x060023D6 RID: 9174 RVA: 0x0062FB94 File Offset: 0x0062FB94
    [CompilerGenerated]
    public bool Equals(SwiftSpring other)
    {
      return EqualityComparer<double>.Default.Equals(this.<Response>k__BackingField, other.<Response>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<DampingFraction>k__BackingField, other.<DampingFraction>k__BackingField);
    }

    // Token: 0x060023D7 RID: 9175 RVA: 0x0062FBC8 File Offset: 0x0062FBC8
    [CompilerGenerated]
    public void Deconstruct(out double Response, out double DampingFraction)
    {
      Response = this.Response;
      DampingFraction = this.DampingFraction;
    }
  }
}
