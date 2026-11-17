using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001EF RID: 495
  public sealed class BrainTurnException : Exception
  {
    // Token: 0x06001CEA RID: 7402 RVA: 0x003E5164 File Offset: 0x003E5164
    [NullableContext(1)]
    public BrainTurnException(string message)
      : base(message)
    {
    }
  }
}
