using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x02000013 RID: 19
  [NullableContext(1)]
  public interface IScreenCaptureService
  {
    // Token: 0x060000EE RID: 238
    IReadOnlyList<DisplayInfo> GetDisplays();

    // Token: 0x060000EF RID: 239
    DisplayInfo GetDisplayUnderCursor();

    // Token: 0x060000F0 RID: 240
    Task<IReadOnlyList<ScreenCaptureFrame>> CaptureAllAsync(int maxDimension = 1280, int jpegQuality = 80, CancellationToken ct = default(CancellationToken));
  }
}
