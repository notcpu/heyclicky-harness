using System;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007F RID: 127
  // (Invoke) Token: 0x060007A3 RID: 1955
  public delegate void LineAudioChunkHandler(int lineIndex, ReadOnlySpan<byte> pcm16Mono);
}
