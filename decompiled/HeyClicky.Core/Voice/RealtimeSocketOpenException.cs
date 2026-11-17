using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000082 RID: 130
  public sealed class RealtimeSocketOpenException : Exception
  {
    // Token: 0x060007F0 RID: 2032 RVA: 0x00367A20 File Offset: 0x00367A20
    [NullableContext(1)]
    public RealtimeSocketOpenException(Exception inner)
      : base("the realtime socket didn't open: " + inner.Message, inner)
    {
    }
  }
}
