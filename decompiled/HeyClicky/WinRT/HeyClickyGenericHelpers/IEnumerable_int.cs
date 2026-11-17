using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000034 RID: 52
  internal static class IEnumerable_int
  {
    // Token: 0x17000033 RID: 51
    // (get) Token: 0x06000165 RID: 357 RVA: 0x005128D0 File Offset: 0x005128D0
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_int._initialized;
      }
    }

    // Token: 0x06000166 RID: 358 RVA: 0x005128D8 File Offset: 0x005128D8
    private static bool Init()
    {
      return IEnumerableMethods<int, int>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000167 RID: 359 RVA: 0x005128E8 File Offset: 0x005128E8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<int>>.FromManaged(IEnumerableMethods<int>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000033 RID: 51
    private static readonly bool _initialized = IEnumerable_int.Init();
  }
}
