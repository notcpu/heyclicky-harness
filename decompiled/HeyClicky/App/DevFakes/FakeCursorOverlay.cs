using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000345 RID: 837
  public sealed class FakeCursorOverlay : ICursorOverlay, IDisposable
  {
    // Token: 0x060023A9 RID: 9129 RVA: 0x0062F1D8 File Offset: 0x0062F1D8
    public void Show()
    {
    }

    // Token: 0x060023AA RID: 9130 RVA: 0x0062F1DC File Offset: 0x0062F1DC
    public void Hide()
    {
    }

    // Token: 0x060023AB RID: 9131 RVA: 0x0062F1E0 File Offset: 0x0062F1E0
    [NullableContext(1)]
    public void SetColor(string hex)
    {
    }

    // Token: 0x060023AC RID: 9132 RVA: 0x0062F1E4 File Offset: 0x0062F1E4
    public void SetVoiceState(VoiceState state, float level)
    {
    }

    // Token: 0x060023AD RID: 9133 RVA: 0x0062F1E8 File Offset: 0x0062F1E8
    [NullableContext(1)]
    public Task PointAtAsync(PointD screenPx, [Nullable(2)] string label, CancellationToken ct = default(CancellationToken))
    {
      return Task.CompletedTask;
    }

    // Token: 0x060023AE RID: 9134 RVA: 0x0062F1F0 File Offset: 0x0062F1F0
    public void ReturnToCursor()
    {
    }

    // Token: 0x060023AF RID: 9135 RVA: 0x0062F1F4 File Offset: 0x0062F1F4
    public void Dispose()
    {
    }
  }
}
