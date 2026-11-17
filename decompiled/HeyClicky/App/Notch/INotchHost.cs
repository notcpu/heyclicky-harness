using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Services;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000138 RID: 312
  [NullableContext(1)]
  public interface INotchHost
  {
    // Token: 0x17000318 RID: 792
    // (get) Token: 0x060010F5 RID: 4341
    AppServices Services { get; }

    // Token: 0x17000319 RID: 793
    // (get) Token: 0x060010F6 RID: 4342
    IAppActions Actions { get; }

    // Token: 0x1700031A RID: 794
    // (get) Token: 0x060010F7 RID: 4343
    NotchGeometry.SizeContext SizeContext { get; }

    // Token: 0x060010F8 RID: 4344
    void Relayout();

    // Token: 0x060010F9 RID: 4345
    void BindToEdges(UIElement element, double ax, double ay);

    // Token: 0x1700031B RID: 795
    // (get) Token: 0x060010FA RID: 4346
    bool IsHovering { get; }

    // Token: 0x1700031C RID: 796
    // (get) Token: 0x060010FB RID: 4347
    IntPtr WindowHandle { get; }

    // Token: 0x060010FC RID: 4348
    void RequestKeyboard();

    // Token: 0x060010FD RID: 4349
    void ReleaseKeyboard();

    // Token: 0x060010FE RID: 4350
    void ReleaseKeyboardIfUnheld();

    // Token: 0x1700031D RID: 797
    // (get) Token: 0x060010FF RID: 4351
    bool HasKeyboard { get; }

    // Token: 0x14000041 RID: 65
    // (add) Token: 0x06001100 RID: 4352
    // (remove) Token: 0x06001101 RID: 4353
    [Nullable(2)]
    event Action<bool> KeyboardChanged;

    // Token: 0x14000042 RID: 66
    // (add) Token: 0x06001102 RID: 4354
    // (remove) Token: 0x06001103 RID: 4355
    [Nullable(2)]
    event Action<bool> HoverChanged;

    // Token: 0x06001104 RID: 4356
    IReadOnlyList<string> TakePendingFiles();

    // Token: 0x1700031E RID: 798
    // (get) Token: 0x06001105 RID: 4357
    MultiAgentRotationCursor MultiAgentRotation { get; }

    // Token: 0x14000043 RID: 67
    // (add) Token: 0x06001106 RID: 4358
    // (remove) Token: 0x06001107 RID: 4359
    [Nullable(2)]
    event Action MultiAgentRotated;
  }
}
