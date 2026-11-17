using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.App.Interop
{
  // Token: 0x0200019B RID: 411
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class WindowSubclass : IDisposable
  {
    // Token: 0x06001653 RID: 5715
    [DllImport("comctl32.dll")]
    private static extern bool SetWindowSubclass(IntPtr hwnd, WindowSubclass.SubclassProc proc, UIntPtr id, UIntPtr refData);

    // Token: 0x06001654 RID: 5716
    [DllImport("comctl32.dll")]
    private static extern bool RemoveWindowSubclass(IntPtr hwnd, WindowSubclass.SubclassProc proc, UIntPtr id);

    // Token: 0x06001655 RID: 5717
    [DllImport("comctl32.dll")]
    private static extern IntPtr DefSubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    // Token: 0x06001656 RID: 5718 RVA: 0x005BA54C File Offset: 0x005BA54C
    public WindowSubclass(IntPtr hwnd, WindowSubclass.Handler handler)
    {
      this.hwnd = hwnd;
      this.handler = handler;
      this.id = WindowSubclass.nextId++;
      this.proc = new WindowSubclass.SubclassProc(this.Proc);
      WindowSubclass.SetWindowSubclass(hwnd, this.proc, this.id, (UIntPtr)((IntPtr)0));
    }

    // Token: 0x06001657 RID: 5719 RVA: 0x005BA5A8 File Offset: 0x005BA5A8
    private IntPtr Proc(IntPtr h, uint msg, IntPtr w, IntPtr l, UIntPtr _, UIntPtr __)
    {
      try
      {
        IntPtr? intPtr = this.handler(h, msg, w, l);
        if (intPtr != null)
        {
          return intPtr.Value;
        }
      }
      catch (Exception ex)
      {
        string text = "subclass";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendLiteral("msg 0x");
        defaultInterpolatedStringHandler.AppendFormatted<uint>(msg, "X");
        Log.Error(text, defaultInterpolatedStringHandler.ToStringAndClear(), ex);
      }
      return WindowSubclass.DefSubclassProc(h, msg, w, l);
    }

    // Token: 0x06001658 RID: 5720 RVA: 0x005BA630 File Offset: 0x005BA630
    public void Dispose()
    {
      WindowSubclass.RemoveWindowSubclass(this.hwnd, this.proc, this.id);
    }

    // Token: 0x04000AF9 RID: 2809
    private static UIntPtr nextId = (UIntPtr)((IntPtr)1);

    // Token: 0x04000AFA RID: 2810
    private readonly IntPtr hwnd;

    // Token: 0x04000AFB RID: 2811
    private readonly UIntPtr id;

    // Token: 0x04000AFC RID: 2812
    private readonly WindowSubclass.SubclassProc proc;

    // Token: 0x04000AFD RID: 2813
    private readonly WindowSubclass.Handler handler;

    // Token: 0x0200074E RID: 1870
    // (Invoke) Token: 0x060034D8 RID: 13528
    [NullableContext(0)]
    public delegate IntPtr? Handler(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    // Token: 0x0200074F RID: 1871
    // (Invoke) Token: 0x060034DC RID: 13532
    [NullableContext(0)]
    private delegate IntPtr SubclassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr id, UIntPtr refData);
  }
}
