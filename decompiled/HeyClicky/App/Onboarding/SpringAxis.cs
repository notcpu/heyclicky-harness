using System;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000127 RID: 295
  public sealed class SpringAxis
  {
    // Token: 0x170002EE RID: 750
    // (get) Token: 0x0600101D RID: 4125 RVA: 0x00581D9C File Offset: 0x00581D9C
    // (set) Token: 0x0600101E RID: 4126 RVA: 0x00581DA4 File Offset: 0x00581DA4
    public double Value { get; private set; }

    // Token: 0x170002EF RID: 751
    // (get) Token: 0x0600101F RID: 4127 RVA: 0x00581DB0 File Offset: 0x00581DB0
    // (set) Token: 0x06001020 RID: 4128 RVA: 0x00581DB8 File Offset: 0x00581DB8
    public double Velocity { get; private set; }

    // Token: 0x170002F0 RID: 752
    // (get) Token: 0x06001021 RID: 4129 RVA: 0x00581DC4 File Offset: 0x00581DC4
    // (set) Token: 0x06001022 RID: 4130 RVA: 0x00581DCC File Offset: 0x00581DCC
    public double Target { get; set; }

    // Token: 0x06001023 RID: 4131 RVA: 0x00581DD8 File Offset: 0x00581DD8
    public SpringAxis(double response, double dampingFraction, double value)
    {
      this.omega = 6.283185307179586 / response;
      this.zeta = dampingFraction;
      this.Target = value;
      this.Value = value;
    }

    // Token: 0x06001024 RID: 4132 RVA: 0x00581E14 File Offset: 0x00581E14
    public void Step(double seconds)
    {
      if (seconds <= 0.0)
      {
        return;
      }
      double num = this.Value - this.Target;
      double velocity = this.Velocity;
      double num6;
      double num7;
      if (this.zeta < 1.0)
      {
        double num2 = this.omega * Math.Sqrt(1.0 - this.zeta * this.zeta);
        double num3 = Math.Exp(-this.zeta * this.omega * seconds);
        double num4 = Math.Cos(num2 * seconds);
        double num5 = Math.Sin(num2 * seconds);
        num6 = num3 * (num * num4 + (velocity + this.zeta * this.omega * num) / num2 * num5);
        num7 = num3 * (velocity * num4 - (this.zeta * this.omega * velocity + this.omega * this.omega * num) / num2 * num5);
      }
      else if (this.zeta == 1.0)
      {
        double num8 = Math.Exp(-this.omega * seconds);
        double num9 = velocity + this.omega * num;
        num6 = num8 * (num + num9 * seconds);
        num7 = num8 * (velocity - this.omega * num9 * seconds);
      }
      else
      {
        double num10 = Math.Sqrt(this.zeta * this.zeta - 1.0);
        double num11 = -this.omega * (this.zeta - num10);
        double num12 = -this.omega * (this.zeta + num10);
        double num13 = (velocity - num11 * num) / (num12 - num11);
        double num14 = num - num13;
        num6 = num14 * Math.Exp(num11 * seconds) + num13 * Math.Exp(num12 * seconds);
        num7 = num14 * num11 * Math.Exp(num11 * seconds) + num13 * num12 * Math.Exp(num12 * seconds);
      }
      this.Value = this.Target + num6;
      this.Velocity = num7;
    }

    // Token: 0x170002F1 RID: 753
    // (get) Token: 0x06001025 RID: 4133 RVA: 0x00581FE8 File Offset: 0x00581FE8
    public bool IsSettled
    {
      get
      {
        return Math.Abs(this.Target - this.Value) < 0.05 && Math.Abs(this.Velocity) < 0.5;
      }
    }

    // Token: 0x06001026 RID: 4134 RVA: 0x00582020 File Offset: 0x00582020
    public void Settle()
    {
      this.Value = this.Target;
      this.Velocity = 0.0;
    }

    // Token: 0x06001027 RID: 4135 RVA: 0x00582040 File Offset: 0x00582040
    public static double UnitStep(double response, double dampingFraction, double t)
    {
      if (t <= 0.0)
      {
        return 0.0;
      }
      double num = 6.283185307179586 / response;
      if (dampingFraction < 1.0)
      {
        double num2 = num * Math.Sqrt(1.0 - dampingFraction * dampingFraction);
        return 1.0 - Math.Exp(-dampingFraction * num * t) * (Math.Cos(num2 * t) + dampingFraction * num / num2 * Math.Sin(num2 * t));
      }
      return 1.0 - Math.Exp(-num * t) * (1.0 + num * t);
    }

    // Token: 0x040006F4 RID: 1780
    private readonly double omega;

    // Token: 0x040006F5 RID: 1781
    private readonly double zeta;
  }
}
