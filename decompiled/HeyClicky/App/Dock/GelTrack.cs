using System;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000313 RID: 787
  public static class GelTrack
  {
    // Token: 0x06002230 RID: 8752 RVA: 0x00620918 File Offset: 0x00620918
    public static double FillWidth(int steps, double inner, double segment, double gelHeight)
    {
      return Math.Min(inner, Math.Max(gelHeight, segment * (double)steps));
    }

    // Token: 0x0400106B RID: 4203
    public const double Inset = 2.0;
  }
}
