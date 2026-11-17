using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002B RID: 43
  internal static class IEnumerable_double
  {
    // Token: 0x1700002A RID: 42
    // (get) Token: 0x06000129 RID: 297 RVA: 0x00511D4C File Offset: 0x00511D4C
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_double._initialized;
      }
    }

    // Token: 0x0600012A RID: 298 RVA: 0x00511D54 File Offset: 0x00511D54
    private static bool Init()
    {
      return IEnumerableMethods<double, double>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600012B RID: 299 RVA: 0x00511D64 File Offset: 0x00511D64
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<double>>.FromManaged(IEnumerableMethods<double>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002A RID: 42
    private static readonly bool _initialized = IEnumerable_double.Init();
  }
}
