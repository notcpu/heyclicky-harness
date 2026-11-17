using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000341 RID: 833
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class FakePermissionService : IPermissionService
  {
    // Token: 0x14000086 RID: 134
    // (add) Token: 0x06002387 RID: 9095 RVA: 0x0062EE64 File Offset: 0x0062EE64
    // (remove) Token: 0x06002388 RID: 9096 RVA: 0x0062EE9C File Offset: 0x0062EE9C
    public event EventHandler Changed;

    // Token: 0x06002389 RID: 9097 RVA: 0x0062EED4 File Offset: 0x0062EED4
    public PermissionStatus GetStatus(PermissionKind kind)
    {
      return this.status[kind];
    }

    // Token: 0x0600238A RID: 9098 RVA: 0x0062EEE4 File Offset: 0x0062EEE4
    public void Set(PermissionKind kind, PermissionStatus s)
    {
      this.status[kind] = s;
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x17000681 RID: 1665
    // (get) Token: 0x0600238B RID: 9099 RVA: 0x0062EF0C File Offset: 0x0062EF0C
    // (set) Token: 0x0600238C RID: 9100 RVA: 0x0062EF14 File Offset: 0x0062EF14
    public MicrophoneBlock MicrophoneBlockedBy { get; set; }

    // Token: 0x0600238D RID: 9101 RVA: 0x0062EF20 File Offset: 0x0062EF20
    [NullableContext(1)]
    public async Task<PermissionStatus> RequestAsync(PermissionKind kind)
    {
      this.Set(kind, PermissionStatus.Prompting);
      await Task.Delay(800);
      this.Set(kind, PermissionStatus.Granted);
      return PermissionStatus.Granted;
    }

    // Token: 0x0600238E RID: 9102 RVA: 0x0062EF6C File Offset: 0x0062EF6C
    public void OpenSystemSettings(PermissionKind kind)
    {
      string text = ((kind == PermissionKind.Microphone) ? "ms-settings:privacy-microphone" : "ms-settings:privacy-graphicsCaptureProgrammatic");
      try
      {
        Process.Start(new ProcessStartInfo(text)
        {
          UseShellExecute = true
        });
      }
      catch
      {
      }
    }

    // Token: 0x0600238F RID: 9103 RVA: 0x0062EFB4 File Offset: 0x0062EFB4
    public void StartPolling()
    {
    }

    // Token: 0x06002390 RID: 9104 RVA: 0x0062EFB8 File Offset: 0x0062EFB8
    public void StopPolling()
    {
    }

    // Token: 0x06002391 RID: 9105 RVA: 0x0062EFBC File Offset: 0x0062EFBC
    public FakePermissionService()
    {
      Dictionary<PermissionKind, PermissionStatus> dictionary = new Dictionary<PermissionKind, PermissionStatus>();
      dictionary[PermissionKind.Microphone] = PermissionStatus.NotDetermined;
      dictionary[PermissionKind.ScreenCapture] = PermissionStatus.NotDetermined;
      this.status = dictionary;
      base..ctor();
    }

    // Token: 0x04001166 RID: 4454
    [Nullable(1)]
    private readonly Dictionary<PermissionKind, PermissionStatus> status;
  }
}
