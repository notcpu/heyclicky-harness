using System;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000014 RID: 20
  [NullableContext(2)]
  public interface IMicrophoneCapture : IDisposable
  {
    // Token: 0x14000002 RID: 2
    // (add) Token: 0x060000F1 RID: 241
    // (remove) Token: 0x060000F2 RID: 242
    [Nullable(new byte[] { 2, 0 })]
    event Action<ReadOnlyMemory<byte>> Frame;

    // Token: 0x17000043 RID: 67
    // (get) Token: 0x060000F3 RID: 243
    int SampleRate { get; }

    // Token: 0x17000044 RID: 68
    // (get) Token: 0x060000F4 RID: 244
    float Level { get; }

    // Token: 0x17000045 RID: 69
    // (get) Token: 0x060000F5 RID: 245
    bool IsRunning { get; }

    // Token: 0x060000F6 RID: 246
    void Start();

    // Token: 0x060000F7 RID: 247
    void Stop();

    // Token: 0x17000046 RID: 70
    // (get) Token: 0x060000F8 RID: 248 RVA: 0x0001E6CC File Offset: 0x0001E6CC
    string DeviceName
    {
      get
      {
        return null;
      }
    }

    // Token: 0x17000047 RID: 71
    // (get) Token: 0x060000F9 RID: 249 RVA: 0x0001E6D0 File Offset: 0x0001E6D0
    bool IsBluetoothDevice
    {
      get
      {
        return false;
      }
    }

    // Token: 0x14000003 RID: 3
    // (add) Token: 0x060000FA RID: 250 RVA: 0x0001E6D4 File Offset: 0x0001E6D4
    // (remove) Token: 0x060000FB RID: 251 RVA: 0x0001E6D8 File Offset: 0x0001E6D8
    [Nullable(new byte[] { 2, 1 })]
    public virtual event Action<string> DeviceProvedDead
    {
      add
      {
      }
      remove
      {
      }
    }

    // Token: 0x14000004 RID: 4
    // (add) Token: 0x060000FC RID: 252 RVA: 0x0001E6DC File Offset: 0x0001E6DC
    // (remove) Token: 0x060000FD RID: 253 RVA: 0x0001E6E0 File Offset: 0x0001E6E0
    public virtual event Action StreamStarted
    {
      add
      {
      }
      remove
      {
      }
    }

    // Token: 0x060000FE RID: 254 RVA: 0x0001E6E4 File Offset: 0x0001E6E4
    [NullableContext(1)]
    [return: Nullable(2)]
    string FailOverToBuiltInMicrophone(string deadDeviceName)
    {
      return null;
    }

    // Token: 0x060000FF RID: 255 RVA: 0x0001E6E8 File Offset: 0x0001E6E8
    void StopKeepingDeviceOpen()
    {
      this.Stop();
    }

    // Token: 0x06000100 RID: 256 RVA: 0x0001E6F0 File Offset: 0x0001E6F0
    void ReleaseHeldDevice()
    {
    }
  }
}
