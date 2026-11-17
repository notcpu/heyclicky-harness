using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.UI.Composition;

namespace HeyClicky.App.Design
{
  // Token: 0x0200034A RID: 842
  public sealed class SpringValue
  {
    // Token: 0x060023E0 RID: 9184 RVA: 0x00630188 File Offset: 0x00630188
    public SpringValue(double initial, double epsilon)
    {
    }

    // Token: 0x17000694 RID: 1684
    // (get) Token: 0x060023E1 RID: 9185 RVA: 0x006301C4 File Offset: 0x006301C4
    public double Target
    {
      get
      {
        return this.to;
      }
    }

    // Token: 0x060023E2 RID: 9186 RVA: 0x006301CC File Offset: 0x006301CC
    public double ValueAt(double time)
    {
      double num = time - this.startsAt;
      if (num <= 0.0)
      {
        return this.from;
      }
      if (time >= this.endsAt)
      {
        return this.to;
      }
      ValueTuple<double, double, double, double> valueTuple = SpringCurve.Basis(this.spring, num);
      double item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      return this.to + (this.from - this.to) * item + this.velocity * item2;
    }

    // Token: 0x060023E3 RID: 9187 RVA: 0x00630240 File Offset: 0x00630240
    private double VelocityAt(double time)
    {
      double num = time - this.startsAt;
      if (num <= 0.0 || time >= this.endsAt)
      {
        return 0.0;
      }
      ValueTuple<double, double, double, double> valueTuple = SpringCurve.Basis(this.spring, num);
      double item = valueTuple.Item3;
      double item2 = valueTuple.Item4;
      return (this.from - this.to) * item + this.velocity * item2;
    }

    // Token: 0x060023E4 RID: 9188 RVA: 0x006302A8 File Offset: 0x006302A8
    [NullableContext(1)]
    [return: Nullable(2)]
    public KeyFrameAnimation To(Compositor c, double target, SwiftSpring? next)
    {
      SpringMove springMove = this.Move(target, next);
      if (springMove == null)
      {
        return null;
      }
      return springMove.Animation(c);
    }

    // Token: 0x060023E5 RID: 9189 RVA: 0x006302C0 File Offset: 0x006302C0
    [NullableContext(2)]
    public SpringMove Move(double target, SwiftSpring? next)
    {
      double now = SpringCurve.Now;
      if (Math.Abs(target - this.to) < this.<epsilon>P && (next == null || now >= this.endsAt || next == this.spring))
      {
        return null;
      }
      if (next != null)
      {
        SwiftSpring valueOrDefault = next.GetValueOrDefault();
        double num = this.ValueAt(now);
        double num2 = this.VelocityAt(now);
        this.from = num;
        this.velocity = num2;
        this.to = target;
        this.spring = valueOrDefault;
        this.startsAt = now;
        double num3 = Math.Max(0.016, SpringCurve.SettleSeconds(valueOrDefault, num - target, num2, this.<epsilon>P));
        this.endsAt = now + num3;
        List<float> list = new List<float>();
        List<float> list2 = new List<float>();
        foreach (ValueTuple<float, double> valueTuple in SpringCurve.Samples(num3))
        {
          float item = valueTuple.Item1;
          double item2 = valueTuple.Item2;
          list.Add(item);
          list2.Add((item >= 1f) ? ((float)target) : ((float)this.ValueAt(now + item2)));
        }
        return new SpringMove(list.ToArray(), list2.ToArray(), TimeSpan.FromSeconds(num3));
      }
      this.to = target;
      this.from = target;
      this.velocity = 0.0;
      this.startsAt = (this.endsAt = now);
      return new SpringMove(new float[] { 0f, 1f }, new float[]
      {
        (float)target,
        (float)target
      }, TimeSpan.FromMilliseconds(1L, 0L));
    }

    // Token: 0x04001178 RID: 4472
    [CompilerGenerated]
    private double <epsilon>P = epsilon;

    // Token: 0x04001179 RID: 4473
    private double from = initial;

    // Token: 0x0400117A RID: 4474
    private double velocity;

    // Token: 0x0400117B RID: 4475
    private double to = initial;

    // Token: 0x0400117C RID: 4476
    private double startsAt;

    // Token: 0x0400117D RID: 4477
    private double endsAt;

    // Token: 0x0400117E RID: 4478
    private SwiftSpring spring = new SwiftSpring(0.42, 0.86);
  }
}
