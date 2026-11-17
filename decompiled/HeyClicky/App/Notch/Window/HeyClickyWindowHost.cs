using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Services;
using HeyClicky.Core.Notch;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000140 RID: 320
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class HeyClickyWindowHost : INotchHost, IHeyClickyWindowHost
  {
    // Token: 0x06001208 RID: 4616 RVA: 0x00594518 File Offset: 0x00594518
    public HeyClickyWindowHost(INotchHost notch, HeyClickyWindow window, IntPtr hwnd)
    {
    }

    // Token: 0x1700034A RID: 842
    // (get) Token: 0x06001209 RID: 4617 RVA: 0x00594538 File Offset: 0x00594538
    public AppServices Services
    {
      get
      {
        return this.<notch>P.Services;
      }
    }

    // Token: 0x1700034B RID: 843
    // (get) Token: 0x0600120A RID: 4618 RVA: 0x00594548 File Offset: 0x00594548
    public IAppActions Actions
    {
      get
      {
        return this.<notch>P.Actions;
      }
    }

    // Token: 0x1700034C RID: 844
    // (get) Token: 0x0600120B RID: 4619 RVA: 0x00594558 File Offset: 0x00594558
    public NotchGeometry.SizeContext SizeContext
    {
      get
      {
        return this.<notch>P.SizeContext;
      }
    }

    // Token: 0x0600120C RID: 4620 RVA: 0x00594568 File Offset: 0x00594568
    public void Relayout()
    {
    }

    // Token: 0x0600120D RID: 4621 RVA: 0x0059456C File Offset: 0x0059456C
    public void BindToEdges(UIElement element, double ax, double ay)
    {
    }

    // Token: 0x1700034D RID: 845
    // (get) Token: 0x0600120E RID: 4622 RVA: 0x00594570 File Offset: 0x00594570
    public bool IsHovering
    {
      get
      {
        return false;
      }
    }

    // Token: 0x1700034E RID: 846
    // (get) Token: 0x0600120F RID: 4623 RVA: 0x00594574 File Offset: 0x00594574
    public IntPtr WindowHandle
    {
      get
      {
        return this.<hwnd>P;
      }
    } = hwnd;

    // Token: 0x06001210 RID: 4624 RVA: 0x0059457C File Offset: 0x0059457C
    public void RequestKeyboard()
    {
      Native.SetForegroundWindow(this.<hwnd>P);
    }

    // Token: 0x06001211 RID: 4625 RVA: 0x0059458C File Offset: 0x0059458C
    public void ReleaseKeyboard()
    {
    }

    // Token: 0x06001212 RID: 4626 RVA: 0x00594590 File Offset: 0x00594590
    public void ReleaseKeyboardIfUnheld()
    {
    }

    // Token: 0x1700034F RID: 847
    // (get) Token: 0x06001213 RID: 4627 RVA: 0x00594594 File Offset: 0x00594594
    public bool HasKeyboard
    {
      get
      {
        return this.active;
      }
    }

    // Token: 0x1400004C RID: 76
    // (add) Token: 0x06001214 RID: 4628 RVA: 0x0059459C File Offset: 0x0059459C
    // (remove) Token: 0x06001215 RID: 4629 RVA: 0x005945D4 File Offset: 0x005945D4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> KeyboardChanged;

    // Token: 0x1400004D RID: 77
    // (add) Token: 0x06001216 RID: 4630 RVA: 0x0059460C File Offset: 0x0059460C
    // (remove) Token: 0x06001217 RID: 4631 RVA: 0x00594610 File Offset: 0x00594610
    [Nullable(2)]
    public event Action<bool> HoverChanged
    {
      [NullableContext(2)]
      add
      {
      }
      [NullableContext(2)]
      remove
      {
      }
    }

    // Token: 0x06001218 RID: 4632 RVA: 0x00594614 File Offset: 0x00594614
    public IReadOnlyList<string> TakePendingFiles()
    {
      return Array.Empty<string>();
    }

    // Token: 0x17000350 RID: 848
    // (get) Token: 0x06001219 RID: 4633 RVA: 0x0059461C File Offset: 0x0059461C
    public MultiAgentRotationCursor MultiAgentRotation
    {
      get
      {
        return this.<notch>P.MultiAgentRotation;
      }
    }

    // Token: 0x1400004E RID: 78
    // (add) Token: 0x0600121A RID: 4634 RVA: 0x0059462C File Offset: 0x0059462C
    // (remove) Token: 0x0600121B RID: 4635 RVA: 0x0059463C File Offset: 0x0059463C
    [Nullable(2)]
    public event Action MultiAgentRotated
    {
      [NullableContext(2)]
      add
      {
        this.<notch>P.MultiAgentRotated += value;
      }
      [NullableContext(2)]
      remove
      {
        this.<notch>P.MultiAgentRotated -= value;
      }
    }

    // Token: 0x0600121C RID: 4636 RVA: 0x0059464C File Offset: 0x0059464C
    public void NoteActive(bool isActive)
    {
      this.active = isActive;
      Action<bool> keyboardChanged = this.KeyboardChanged;
      if (keyboardChanged == null)
      {
        return;
      }
      keyboardChanged(isActive);
    }

    // Token: 0x0600121D RID: 4637 RVA: 0x00594668 File Offset: 0x00594668
    public void OpenAgentForEntry(string entryId)
    {
      this.<window>P.OpenAgentForEntry(entryId);
    }

    // Token: 0x04000837 RID: 2103
    [CompilerGenerated]
    private INotchHost <notch>P = notch;

    // Token: 0x04000838 RID: 2104
    [CompilerGenerated]
    private HeyClickyWindow <window>P = window;

    // Token: 0x04000839 RID: 2105
    [CompilerGenerated]
    private IntPtr <hwnd>P;

    // Token: 0x0400083A RID: 2106
    private bool active;
  }
}
