using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Windows.Foundation;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000045 RID: 69
  internal static class IEnumerable_Windows_Foundation_Point
  {
    // Token: 0x17000044 RID: 68
    // (get) Token: 0x060001DD RID: 477 RVA: 0x00513FE8 File Offset: 0x00513FE8
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Windows_Foundation_Point._initialized;
      }
    }

    // Token: 0x060001DE RID: 478 RVA: 0x00513FF0 File Offset: 0x00513FF0
    private static bool Init()
    {
      return IEnumerableMethods<Point, Point>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060001DF RID: 479 RVA: 0x00514000 File Offset: 0x00514000
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<Point>>.FromManaged(IEnumerableMethods<Point>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000044 RID: 68
    private static readonly bool _initialized = IEnumerable_Windows_Foundation_Point.Init();
  }
}
