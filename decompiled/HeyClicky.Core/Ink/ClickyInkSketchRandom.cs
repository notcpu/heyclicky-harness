using System;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000180 RID: 384
  public struct ClickyInkSketchRandom
  {
    // Token: 0x060016FB RID: 5883 RVA: 0x003C1DD0 File Offset: 0x003C1DD0
    public ClickyInkSketchRandom(int seed)
    {
      this.state = (ulong)((long)seed + -7046029254386353131L);
    }

    // Token: 0x060016FC RID: 5884 RVA: 0x003C1DE4 File Offset: 0x003C1DE4
    public double Next()
    {
      this.state += 11400714819323198485UL;
      ulong num = this.state;
      ulong num2 = (num ^ (num >> 30)) * 13787848793156543929UL;
      ulong num3 = (num2 ^ (num2 >> 27)) * 10723151780598845931UL;
      return ((num3 ^ (num3 >> 31)) >> 11) / 9007199254740992.0;
    }

    // Token: 0x040009FA RID: 2554
    private ulong state;
  }
}
