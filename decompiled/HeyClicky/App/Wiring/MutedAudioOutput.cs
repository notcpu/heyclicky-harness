using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Wiring
{
  // Token: 0x0200009A RID: 154
  public sealed class MutedAudioOutput : IAudioOutput, IDisposable
  {
    // Token: 0x06000617 RID: 1559 RVA: 0x0052F140 File Offset: 0x0052F140
    [NullableContext(1)]
    public MutedAudioOutput(IAudioOutput inner)
    {
    }

    // Token: 0x170000E8 RID: 232
    // (get) Token: 0x06000618 RID: 1560 RVA: 0x0052F150 File Offset: 0x0052F150
    public int SampleRate
    {
      get
      {
        return this.<inner>P.SampleRate;
      }
    }

    // Token: 0x06000619 RID: 1561 RVA: 0x0052F160 File Offset: 0x0052F160
    public void Enqueue(ReadOnlySpan<byte> pcm16Mono)
    {
      byte[] array = ArrayPool<byte>.Shared.Rent(pcm16Mono.Length);
      try
      {
        Array.Clear(array, 0, pcm16Mono.Length);
        this.<inner>P.Enqueue(array.AsSpan<byte>(0, pcm16Mono.Length));
      }
      finally
      {
        ArrayPool<byte>.Shared.Return(array, false);
      }
    }

    // Token: 0x0600061A RID: 1562 RVA: 0x0052F1CC File Offset: 0x0052F1CC
    public void Flush()
    {
      this.<inner>P.Flush();
    }

    // Token: 0x170000E9 RID: 233
    // (get) Token: 0x0600061B RID: 1563 RVA: 0x0052F1DC File Offset: 0x0052F1DC
    public TimeSpan Buffered
    {
      get
      {
        return this.<inner>P.Buffered;
      }
    }

    // Token: 0x170000EA RID: 234
    // (get) Token: 0x0600061C RID: 1564 RVA: 0x0052F1EC File Offset: 0x0052F1EC
    public double PlayedSeconds
    {
      get
      {
        return this.<inner>P.PlayedSeconds;
      }
    }

    // Token: 0x170000EB RID: 235
    // (get) Token: 0x0600061D RID: 1565 RVA: 0x0052F1FC File Offset: 0x0052F1FC
    public float Level
    {
      get
      {
        return 0f;
      }
    }

    // Token: 0x0600061E RID: 1566 RVA: 0x0052F204 File Offset: 0x0052F204
    [NullableContext(1)]
    public void PlayEarcon(string name)
    {
    }

    // Token: 0x0600061F RID: 1567 RVA: 0x0052F208 File Offset: 0x0052F208
    public void Dispose()
    {
    }

    // Token: 0x04000162 RID: 354
    [Nullable(1)]
    [CompilerGenerated]
    private IAudioOutput <inner>P = inner;
  }
}
