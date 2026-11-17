using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000343 RID: 835
  public sealed class FakeAudioOutput : IAudioOutput, IDisposable
  {
    // Token: 0x17000685 RID: 1669
    // (get) Token: 0x0600239C RID: 9116 RVA: 0x0062F110 File Offset: 0x0062F110
    public int SampleRate
    {
      get
      {
        return 24000;
      }
    }

    // Token: 0x0600239D RID: 9117 RVA: 0x0062F118 File Offset: 0x0062F118
    public void Enqueue(ReadOnlySpan<byte> pcm16Mono)
    {
    }

    // Token: 0x0600239E RID: 9118 RVA: 0x0062F11C File Offset: 0x0062F11C
    public void Flush()
    {
    }

    // Token: 0x17000686 RID: 1670
    // (get) Token: 0x0600239F RID: 9119 RVA: 0x0062F120 File Offset: 0x0062F120
    public TimeSpan Buffered
    {
      get
      {
        return TimeSpan.Zero;
      }
    }

    // Token: 0x17000687 RID: 1671
    // (get) Token: 0x060023A0 RID: 9120 RVA: 0x0062F128 File Offset: 0x0062F128
    public double PlayedSeconds
    {
      get
      {
        return 0.0;
      }
    }

    // Token: 0x17000688 RID: 1672
    // (get) Token: 0x060023A1 RID: 9121 RVA: 0x0062F134 File Offset: 0x0062F134
    public float Level
    {
      get
      {
        return 0f;
      }
    }

    // Token: 0x060023A2 RID: 9122 RVA: 0x0062F13C File Offset: 0x0062F13C
    [NullableContext(1)]
    public void PlayEarcon(string name)
    {
    }

    // Token: 0x060023A3 RID: 9123 RVA: 0x0062F140 File Offset: 0x0062F140
    public void Dispose()
    {
    }
  }
}
