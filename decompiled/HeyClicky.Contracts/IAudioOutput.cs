using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000015 RID: 21
  public interface IAudioOutput : IDisposable
  {
    // Token: 0x17000048 RID: 72
    // (get) Token: 0x06000101 RID: 257
    int SampleRate { get; }

    // Token: 0x06000102 RID: 258
    void Enqueue(ReadOnlySpan<byte> pcm16Mono);

    // Token: 0x06000103 RID: 259
    void Flush();

    // Token: 0x17000049 RID: 73
    // (get) Token: 0x06000104 RID: 260
    TimeSpan Buffered { get; }

    // Token: 0x1700004A RID: 74
    // (get) Token: 0x06000105 RID: 261
    double PlayedSeconds { get; }

    // Token: 0x1700004B RID: 75
    // (get) Token: 0x06000106 RID: 262
    float Level { get; }

    // Token: 0x06000107 RID: 263
    [NullableContext(1)]
    void PlayEarcon(string name);

    // Token: 0x06000108 RID: 264 RVA: 0x0001E6F4 File Offset: 0x0001E6F4
    void PrepareForUpcomingAudio()
    {
    }
  }
}
