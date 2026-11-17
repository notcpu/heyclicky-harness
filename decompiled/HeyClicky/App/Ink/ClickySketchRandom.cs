using System;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E9 RID: 489
  public struct ClickySketchRandom
  {
    // Token: 0x06001832 RID: 6194 RVA: 0x005C9640 File Offset: 0x005C9640
    public ClickySketchRandom(long seed)
    {
      this.state = (ulong)(seed + -7046029254386353131L);
    }

    // Token: 0x06001833 RID: 6195 RVA: 0x005C9654 File Offset: 0x005C9654
    public double Next()
    {
      this.state += 11400714819323198485UL;
      ulong num = this.state;
      ulong num2 = (num ^ (num >> 30)) * 13787848793156543929UL;
      ulong num3 = (num2 ^ (num2 >> 27)) * 10723151780598845931UL;
      return ((num3 ^ (num3 >> 31)) >> 11) / 9007199254740992.0;
    }

    // Token: 0x06001834 RID: 6196 RVA: 0x005C96B0 File Offset: 0x005C96B0
    public double Uniform(double low, double high)
    {
      return low + (high - low) * this.Next();
    }

    // Token: 0x04000BED RID: 3053
    private ulong state;
  }
}
