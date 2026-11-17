using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E3 RID: 483
  public struct ClickyPaperRandom
  {
    // Token: 0x0600181F RID: 6175 RVA: 0x005C8B3C File Offset: 0x005C8B3C
    public ClickyPaperRandom(ulong seed)
    {
      this.state = (seed * 11400714819323198485UL) | 1UL;
    }

    // Token: 0x06001820 RID: 6176 RVA: 0x005C8B54 File Offset: 0x005C8B54
    public ulong Next()
    {
      this.state ^= this.state >> 12;
      this.state ^= this.state << 25;
      this.state ^= this.state >> 27;
      return this.state * 2685821657736338717UL;
    }

    // Token: 0x06001821 RID: 6177 RVA: 0x005C8BB4 File Offset: 0x005C8BB4
    [NullableContext(1)]
    public static ulong StableSeed(string text)
    {
      ulong num = 14695981039346656037UL;
      foreach (byte b in Encoding.UTF8.GetBytes(text))
      {
        num = (num ^ (ulong)b) * 1099511628211UL;
      }
      return num;
    }

    // Token: 0x06001822 RID: 6178 RVA: 0x005C8BFC File Offset: 0x005C8BFC
    public double NextUnit()
    {
      return (this.Next() >> 11) / 9007199254740992.0;
    }

    // Token: 0x06001823 RID: 6179 RVA: 0x005C8C14 File Offset: 0x005C8C14
    public double Next(double low, double high)
    {
      return low + this.NextUnit() * (high - low);
    }

    // Token: 0x04000BDA RID: 3034
    private ulong state;
  }
}
