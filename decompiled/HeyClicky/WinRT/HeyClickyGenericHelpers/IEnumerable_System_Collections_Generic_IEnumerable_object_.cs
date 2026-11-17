using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000F RID: 15
  internal static class IEnumerable_System_Collections_Generic_IEnumerable_object_
  {
    // Token: 0x1700000E RID: 14
    // (get) Token: 0x06000053 RID: 83 RVA: 0x0050F274 File Offset: 0x0050F274
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IEnumerable_object_._initialized;
      }
    }

    // Token: 0x06000054 RID: 84 RVA: 0x0050F27C File Offset: 0x0050F27C
    private static bool Init()
    {
      return IEnumerableMethods<IEnumerable<object>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000055 RID: 85 RVA: 0x0050F28C File Offset: 0x0050F28C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IEnumerable<object>>>.FromManaged(IEnumerableMethods<IEnumerable<object>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000E RID: 14
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IEnumerable_object_.Init();
  }
}
