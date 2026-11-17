using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000035 RID: 53
  internal static class IEnumerable_object
  {
    // Token: 0x17000034 RID: 52
    // (get) Token: 0x06000169 RID: 361 RVA: 0x00512938 File Offset: 0x00512938
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_object._initialized;
      }
    }

    // Token: 0x0600016A RID: 362 RVA: 0x00512940 File Offset: 0x00512940
    private static bool Init()
    {
      return IEnumerableMethods<object, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600016B RID: 363 RVA: 0x00512950 File Offset: 0x00512950
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<object>>.FromManaged(IEnumerableMethods<object>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000034 RID: 52
    private static readonly bool _initialized = IEnumerable_object.Init();
  }
}
