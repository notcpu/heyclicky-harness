using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x02000349 RID: 841
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpringCurve
  {
    // Token: 0x17000693 RID: 1683
    // (get) Token: 0x060023D8 RID: 9176 RVA: 0x0062FBDC File Offset: 0x0062FBDC
    public static double Now
    {
      get
      {
        return (double)Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;
      }
    }

    // Token: 0x060023D9 RID: 9177 RVA: 0x0062FBEC File Offset: 0x0062FBEC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "A", "B", "DA", "DB" })]
    public static ValueTuple<double, double, double, double> Basis(SwiftSpring spring, double t)
    {
      if (t <= 0.0)
      {
        return new ValueTuple<double, double, double, double>(1.0, 0.0, 0.0, 1.0);
      }
      double num = 6.283185307179586 / spring.Response;
      double num2 = Math.Max(0.01, spring.DampingFraction);
      if (num2 < 0.999)
      {
        double num3 = num * Math.Sqrt(1.0 - num2 * num2);
        double num4 = Math.Exp(-num2 * num * t);
        double num5 = Math.Cos(num3 * t);
        double num6 = Math.Sin(num3 * t);
        return new ValueTuple<double, double, double, double>(num4 * (num5 + num2 * num / num3 * num6), num4 * num6 / num3, -num4 * num6 * num * num / num3, num4 * (num5 - num2 * num / num3 * num6));
      }
      if (num2 <= 1.001)
      {
        double num7 = Math.Exp(-num * t);
        return new ValueTuple<double, double, double, double>(num7 * (1.0 + num * t), num7 * t, -num * num * t * num7, num7 * (1.0 - num * t));
      }
      double num8 = Math.Sqrt(num2 * num2 - 1.0);
      double num9 = -num * (num2 - num8);
      double num10 = -num * (num2 + num8);
      double num11 = Math.Exp(num9 * t);
      double num12 = Math.Exp(num10 * t);
      double num13 = num10 - num9;
      double num14 = -num9 / num13;
      double num15 = 1.0 - num14;
      double num16 = 1.0 / num13;
      double num17 = -num16;
      return new ValueTuple<double, double, double, double>(num15 * num11 + num14 * num12, num17 * num11 + num16 * num12, num15 * num9 * num11 + num14 * num10 * num12, num17 * num9 * num11 + num16 * num10 * num12);
    }

    // Token: 0x060023DA RID: 9178 RVA: 0x0062FDC0 File Offset: 0x0062FDC0
    private static double DecayRate(SwiftSpring spring)
    {
      double num = 6.283185307179586 / spring.Response;
      double num2 = Math.Max(0.01, spring.DampingFraction);
      if (num2 >= 1.0)
      {
        return num * (num2 - Math.Sqrt(num2 * num2 - 1.0));
      }
      return num2 * num;
    }

    // Token: 0x060023DB RID: 9179 RVA: 0x0062FE1C File Offset: 0x0062FE1C
    public static double SettleSeconds(SwiftSpring spring, double distance, double speed, double epsilon)
    {
      double num = SpringCurve.DecayRate(spring);
      double num2 = 6.283185307179586 / spring.Response;
      double num3 = Math.Max(0.01, spring.DampingFraction);
      double num4 = ((num3 < 1.0) ? (num2 * Math.Sqrt(1.0 - num3 * num3)) : num);
      double num5 = Math.Abs(distance) * (1.0 + num / num4) + Math.Abs(speed) / num4;
      if (num5 <= epsilon)
      {
        return 0.0;
      }
      return Math.Min(3.0, Math.Log(num5 / epsilon) / num);
    }

    // Token: 0x060023DC RID: 9180 RVA: 0x0062FEC4 File Offset: 0x0062FEC4
    [return: TupleElementNames(new string[] { "Progress", "Seconds" })]
    [return: Nullable(new byte[] { 1, 0 })]
    public static IEnumerable<ValueTuple<float, double>> Samples(double duration)
    {
      SpringCurve.<Samples>d__5 <Samples>d__ = new SpringCurve.<Samples>d__5(-2);
      <Samples>d__.<>3__duration = duration;
      return <Samples>d__;
    }

    // Token: 0x060023DD RID: 9181 RVA: 0x0062FED4 File Offset: 0x0062FED4
    public static ScalarKeyFrameAnimation Scalar(Compositor c, SwiftSpring spring, float from, float to)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = c.CreateScalarKeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = c.CreateLinearEasingFunction();
      double num = Math.Max(0.016, SpringCurve.SettleSeconds(spring, (double)(from - to), 0.0, 0.0005));
      foreach (ValueTuple<float, double> valueTuple in SpringCurve.Samples(num))
      {
        float item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        scalarKeyFrameAnimation.InsertKeyFrame(item, (item >= 1f) ? to : ((float)((double)to + (double)(from - to) * SpringCurve.Basis(spring, item2).Item1)), linearEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(num);
      return scalarKeyFrameAnimation;
    }

    // Token: 0x060023DE RID: 9182 RVA: 0x0062FF9C File Offset: 0x0062FF9C
    public static Vector3KeyFrameAnimation Scale(Compositor c, SwiftSpring spring, float from, float to)
    {
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = c.CreateVector3KeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = c.CreateLinearEasingFunction();
      double num = Math.Max(0.016, SpringCurve.SettleSeconds(spring, (double)(from - to), 0.0, 0.0005));
      foreach (ValueTuple<float, double> valueTuple in SpringCurve.Samples(num))
      {
        float item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        float num2 = ((item >= 1f) ? to : ((float)((double)to + (double)(from - to) * SpringCurve.Basis(spring, item2).Item1)));
        vector3KeyFrameAnimation.InsertKeyFrame(item, new Vector3(num2, num2, 1f), linearEasingFunction);
      }
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(num);
      return vector3KeyFrameAnimation;
    }

    // Token: 0x060023DF RID: 9183 RVA: 0x00630074 File Offset: 0x00630074
    public static Vector3KeyFrameAnimation Scale(Compositor c, SwiftSpring spring, Vector3 from, Vector3 to)
    {
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = c.CreateVector3KeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = c.CreateLinearEasingFunction();
      float num = Math.Max(Math.Max(Math.Abs(from.X - to.X), Math.Abs(from.Y - to.Y)), Math.Abs(from.Z - to.Z));
      double num2 = Math.Max(0.016, SpringCurve.SettleSeconds(spring, (double)num, 0.0, 0.0005));
      foreach (ValueTuple<float, double> valueTuple in SpringCurve.Samples(num2))
      {
        float item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        vector3KeyFrameAnimation.InsertKeyFrame(item, (item >= 1f) ? to : (to + (from - to) * (float)SpringCurve.Basis(spring, item2).Item1), linearEasingFunction);
      }
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(num2);
      return vector3KeyFrameAnimation;
    }
  }
}
