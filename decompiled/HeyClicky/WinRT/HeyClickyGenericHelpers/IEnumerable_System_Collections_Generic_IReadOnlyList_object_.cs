using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003F RID: 63
  internal static class IEnumerable_System_Collections_Generic_IReadOnlyList_object_
  {
    // Token: 0x1700003E RID: 62
    // (get) Token: 0x060001AE RID: 430 RVA: 0x00513670 File Offset: 0x00513670
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IReadOnlyList_object_._initialized;
      }
    }

    // Token: 0x060001AF RID: 431 RVA: 0x00513678 File Offset: 0x00513678
    private static bool Init()
    {
      return IEnumerableMethods<IReadOnlyList<object>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060001B0 RID: 432 RVA: 0x00513688 File Offset: 0x00513688
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IReadOnlyList<object>>>.FromManaged(IEnumerableMethods<IReadOnlyList<object>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400003E RID: 62
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IReadOnlyList_object_.Init();
  }
}
