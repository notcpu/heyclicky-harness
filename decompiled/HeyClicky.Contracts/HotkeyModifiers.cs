using System;

namespace HeyClicky
{
  // Token: 0x0200000D RID: 13
  [Flags]
  public enum HotkeyModifiers : byte
  {
    // Token: 0x0400003D RID: 61
    None = 0,
    // Token: 0x0400003E RID: 62
    Control = 1,
    // Token: 0x0400003F RID: 63
    Alt = 2,
    // Token: 0x04000040 RID: 64
    Shift = 4,
    // Token: 0x04000041 RID: 65
    Win = 8
  }
}
