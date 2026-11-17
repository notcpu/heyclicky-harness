using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Interop;
using HeyClicky.App.Shell;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000344 RID: 836
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FakeScreenCapture : IScreenCaptureService
  {
    // Token: 0x060023A5 RID: 9125 RVA: 0x0062F14C File Offset: 0x0062F14C
    public IReadOnlyList<DisplayInfo> GetDisplays()
    {
      List<DisplayInfo> list = new List<DisplayInfo>();
      Native.EnumDisplayMonitors((IntPtr)0, (IntPtr)0, delegate(IntPtr mon, IntPtr hdc, ref Native.RECT rc, IntPtr data)
      {
        Displays.Monitor monitor = Displays.From(mon);
        List<DisplayInfo> list2 = list;
        int count = list.Count;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
        defaultInterpolatedStringHandler.AppendLiteral("DISPLAY");
        defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count + 1);
        list2.Add(new DisplayInfo(count, defaultInterpolatedStringHandler.ToStringAndClear(), new RectD((double)monitor.Bounds.Left, (double)monitor.Bounds.Top, (double)monitor.Bounds.Width, (double)monitor.Bounds.Height), new RectD((double)monitor.Work.Left, (double)monitor.Work.Top, (double)monitor.Work.Width, (double)monitor.Work.Height), monitor.Scale, monitor.Bounds.Left == 0 && monitor.Bounds.Top == 0));
        return true;
      }, (IntPtr)0);
      return list;
    }

    // Token: 0x060023A6 RID: 9126 RVA: 0x0062F188 File Offset: 0x0062F188
    public DisplayInfo GetDisplayUnderCursor()
    {
      return this.GetDisplays().FirstOrDefault<DisplayInfo>((DisplayInfo d) => d.IsPrimary) ?? this.GetDisplays()[0];
    }

    // Token: 0x060023A7 RID: 9127 RVA: 0x0062F1C4 File Offset: 0x0062F1C4
    public Task<IReadOnlyList<ScreenCaptureFrame>> CaptureAllAsync(int maxDimension = 1280, int jpegQuality = 80, CancellationToken ct = default(CancellationToken))
    {
      return Task.FromResult<IReadOnlyList<ScreenCaptureFrame>>(Array.Empty<ScreenCaptureFrame>());
    }
  }
}
