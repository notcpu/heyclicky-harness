using System;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C8 RID: 456
  public sealed class DictationPaywallReachedException : Exception
  {
    // Token: 0x06001A61 RID: 6753 RVA: 0x003D7364 File Offset: 0x003D7364
    public DictationPaywallReachedException()
      : base("Dictation word limit reached")
    {
    }
  }
}
