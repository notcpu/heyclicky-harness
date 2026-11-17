using System;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000164 RID: 356
  public static class ClickyInkClock
  {
    // Token: 0x06001598 RID: 5528 RVA: 0x003B8EF8 File Offset: 0x003B8EF8
    public static double Progress(double clock, double start, double end)
    {
      if (end <= start)
      {
        return clock >= start;
      }
      return Math.Min(1.0, Math.Max(0.0, (clock - start) / (end - start)));
    }

    // Token: 0x06001599 RID: 5529 RVA: 0x003B8F2C File Offset: 0x003B8F2C
    public static double EaseInOut(double t)
    {
      double num = Math.Min(1.0, Math.Max(0.0, t));
      return num * num * (3.0 - 2.0 * num);
    }

    // Token: 0x0600159A RID: 5530 RVA: 0x003B8F70 File Offset: 0x003B8F70
    public static double EaseOut(double t)
    {
      double num = Math.Min(1.0, Math.Max(0.0, t));
      return 1.0 - Math.Pow(1.0 - num, 3.0);
    }
  }
}
