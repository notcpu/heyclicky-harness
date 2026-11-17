using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000342 RID: 834
  public sealed class FakeMicrophone : IMicrophoneCapture, IDisposable
  {
    // Token: 0x14000087 RID: 135
    // (add) Token: 0x06002392 RID: 9106 RVA: 0x0062EFE0 File Offset: 0x0062EFE0
    // (remove) Token: 0x06002393 RID: 9107 RVA: 0x0062F018 File Offset: 0x0062F018
    [Nullable(new byte[] { 2, 0 })]
    [field: Nullable(new byte[] { 2, 0 })]
    public event Action<ReadOnlyMemory<byte>> Frame;

    // Token: 0x17000682 RID: 1666
    // (get) Token: 0x06002394 RID: 9108 RVA: 0x0062F050 File Offset: 0x0062F050
    public int SampleRate
    {
      get
      {
        return 24000;
      }
    }

    // Token: 0x17000683 RID: 1667
    // (get) Token: 0x06002395 RID: 9109 RVA: 0x0062F058 File Offset: 0x0062F058
    // (set) Token: 0x06002396 RID: 9110 RVA: 0x0062F060 File Offset: 0x0062F060
    public bool IsRunning { get; private set; }

    // Token: 0x17000684 RID: 1668
    // (get) Token: 0x06002397 RID: 9111 RVA: 0x0062F06C File Offset: 0x0062F06C
    public float Level
    {
      get
      {
        double num = (double)(Environment.TickCount64 - this.start) / 1000.0;
        return (float)Math.Clamp(0.45 + 0.35 * Math.Sin(num * 5.3) * Math.Sin(num * 1.7), 0.0, 1.0);
      }
    }

    // Token: 0x06002398 RID: 9112 RVA: 0x0062F0E0 File Offset: 0x0062F0E0
    public void Start()
    {
      this.IsRunning = true;
    }

    // Token: 0x06002399 RID: 9113 RVA: 0x0062F0EC File Offset: 0x0062F0EC
    public void Stop()
    {
      this.IsRunning = false;
    }

    // Token: 0x0600239A RID: 9114 RVA: 0x0062F0F8 File Offset: 0x0062F0F8
    public void Dispose()
    {
    }

    // Token: 0x0400116B RID: 4459
    private readonly long start = Environment.TickCount64;
  }
}
