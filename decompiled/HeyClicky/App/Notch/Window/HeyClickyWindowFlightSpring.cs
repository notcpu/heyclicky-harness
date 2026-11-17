using System;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000144 RID: 324
  public static class HeyClickyWindowFlightSpring
  {
    // Token: 0x17000353 RID: 851
    // (get) Token: 0x06001248 RID: 4680 RVA: 0x00595CC4 File Offset: 0x00595CC4
    private static double Omega
    {
      get
      {
        return 12.566370614359172;
      }
    }

    // Token: 0x17000354 RID: 852
    // (get) Token: 0x06001249 RID: 4681 RVA: 0x00595CD0 File Offset: 0x00595CD0
    private static double OmegaDamped
    {
      get
      {
        return HeyClickyWindowFlightSpring.Omega * Math.Sqrt(0.4524);
      }
    }

    // Token: 0x0600124A RID: 4682 RVA: 0x00595CE8 File Offset: 0x00595CE8
    public static double Progress(double t)
    {
      return 1.0 - Math.Exp(-0.74 * HeyClickyWindowFlightSpring.Omega * t) * (Math.Cos(HeyClickyWindowFlightSpring.OmegaDamped * t) + 0.74 * HeyClickyWindowFlightSpring.Omega / HeyClickyWindowFlightSpring.OmegaDamped * Math.Sin(HeyClickyWindowFlightSpring.OmegaDamped * t));
    }

    // Token: 0x0600124B RID: 4683 RVA: 0x00595D48 File Offset: 0x00595D48
    public static double FirstCrossing()
    {
      return (3.141592653589793 - Math.Atan(Math.Sqrt(0.4524) / 0.74)) / HeyClickyWindowFlightSpring.OmegaDamped;
    }

    // Token: 0x04000850 RID: 2128
    public const double Response = 0.5;

    // Token: 0x04000851 RID: 2129
    public const double Damping = 0.74;

    // Token: 0x04000852 RID: 2130
    public const double Duration = 0.95;
  }
}
