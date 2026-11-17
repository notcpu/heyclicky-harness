using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using HeyClicky.App.Shell;
using Windows.UI.Composition;
using Windows.UI.Composition.Desktop;
using WinRT;

namespace HeyClicky.App.Notch
{
  // Token: 0x0200013C RID: 316
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchSystemLayer
  {
    // Token: 0x1700032B RID: 811
    // (get) Token: 0x0600112B RID: 4395 RVA: 0x0058B2C0 File Offset: 0x0058B2C0
    public Compositor Compositor { get; }

    // Token: 0x0600112C RID: 4396 RVA: 0x0058B2C8 File Offset: 0x0058B2C8
    private NotchSystemLayer(Compositor compositor, DesktopWindowTarget target)
    {
      this.Compositor = compositor;
      this.target = target;
      this.root = compositor.CreateContainerVisual();
      target.Root = this.root;
    }

    // Token: 0x0600112D RID: 4397 RVA: 0x0058B2F8 File Offset: 0x0058B2F8
    [NullableContext(2)]
    public unsafe static NotchSystemLayer Create(IntPtr window)
    {
      NotchSystemLayer notchSystemLayer;
      try
      {
        Compositor systemCompositor = TransparentBackdrop.SystemCompositor;
        IntPtr thisPtr = ((IWinRTObject)systemCompositor).NativeObject.ThisPtr;
        Guid guid = new Guid("29E691FA-4567-4DCA-B319-D0F207EB6807");
        IntPtr intPtr;
        Marshal.ThrowExceptionForHR(Marshal.QueryInterface(thisPtr, in guid, out intPtr));
        IntPtr intPtr2;
        int num;
        try
        {
          delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int, IntPtr*, int> system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*) = *(*intPtr + (IntPtr)3 * (IntPtr)sizeof(IntPtr));
          delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int, IntPtr*, int> system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*)2 = system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*);
          num = calli(System.Int32(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*), intPtr, window, 0, &intPtr2, system.Int32_u0020(System.IntPtr,System.IntPtr,System.Int32,System.IntPtr*)2);
        }
        finally
        {
          Marshal.Release(intPtr);
        }
        if (num < 0)
        {
          string text = "notch";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 1);
          defaultInterpolatedStringHandler.AppendLiteral("no system layer under the content (0x");
          defaultInterpolatedStringHandler.AppendFormatted<int>(num, "X8");
          defaultInterpolatedStringHandler.AppendLiteral("): the shape draws on WinUI's compositor");
          Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
          notchSystemLayer = null;
        }
        else
        {
          try
          {
            notchSystemLayer = new NotchSystemLayer(systemCompositor, DesktopWindowTarget.FromAbi(intPtr2));
          }
          finally
          {
            Marshal.Release(intPtr2);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error("notch", "no system layer under the content: the shape draws on WinUI's compositor", ex);
        notchSystemLayer = null;
      }
      return notchSystemLayer;
    }

    // Token: 0x0600112E RID: 4398 RVA: 0x0058B404 File Offset: 0x0058B404
    public void Add(Visual visual)
    {
      this.root.Children.InsertAtTop(visual);
    }

    // Token: 0x0600112F RID: 4399 RVA: 0x0058B418 File Offset: 0x0058B418
    public void SetScale(double scale, double width, double height)
    {
      this.root.Scale = new Vector3((float)scale, (float)scale, 1f);
      this.root.Size = new Vector2((float)width, (float)height);
    }

    // Token: 0x0400079C RID: 1948
    private readonly DesktopWindowTarget target;

    // Token: 0x0400079D RID: 1949
    private readonly ContainerVisual root;
  }
}
