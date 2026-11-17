using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Interop;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.System;
using Windows.UI;
using Windows.UI.Composition;

namespace HeyClicky.App.Shell
{
  // Token: 0x020000C2 RID: 194
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TransparentBackdrop : SystemBackdrop
  {
    // Token: 0x17000147 RID: 327
    // (get) Token: 0x0600080F RID: 2063 RVA: 0x0053C918 File Offset: 0x0053C918
    internal static Windows.UI.Composition.Compositor SystemCompositor
    {
      get
      {
        if (TransparentBackdrop.systemCompositor == null)
        {
          if (Windows.System.DispatcherQueue.GetForCurrentThread() == null)
          {
            Native.CreateDispatcherQueueController(new Native.DispatcherQueueOptions
            {
              dwSize = Marshal.SizeOf<Native.DispatcherQueueOptions>(),
              threadType = 2,
              apartmentType = 0
            }, out TransparentBackdrop.dispatcherQueueController);
          }
          TransparentBackdrop.systemCompositor = new Windows.UI.Composition.Compositor();
        }
        return TransparentBackdrop.systemCompositor;
      }
    }

    // Token: 0x06000810 RID: 2064 RVA: 0x0053C974 File Offset: 0x0053C974
    protected override void OnTargetConnected(Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop connectedTarget, XamlRoot xamlRoot)
    {
      base.OnTargetConnected(connectedTarget, xamlRoot);
      try
      {
        connectedTarget.SystemBackdrop = TransparentBackdrop.SystemCompositor.CreateColorBrush(Color.FromArgb(0, 0, 0, 0));
        Log.Info("backdrop", "transparent backdrop connected");
      }
      catch (Exception ex)
      {
        Log.Error("backdrop", "connect failed", ex);
      }
    }

    // Token: 0x06000811 RID: 2065 RVA: 0x0053C9D8 File Offset: 0x0053C9D8
    protected override void OnDefaultSystemBackdropConfigurationChanged(Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop target, XamlRoot xamlRoot)
    {
    }

    // Token: 0x06000812 RID: 2066 RVA: 0x0053C9DC File Offset: 0x0053C9DC
    protected override void OnTargetDisconnected(Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop disconnectedTarget)
    {
      base.OnTargetDisconnected(disconnectedTarget);
      disconnectedTarget.SystemBackdrop = null;
    }

    // Token: 0x040002B6 RID: 694
    [Nullable(2)]
    private static Windows.UI.Composition.Compositor systemCompositor;

    // Token: 0x040002B7 RID: 695
    private static IntPtr dispatcherQueueController;
  }
}
