using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000023 RID: 35
  internal static class IEnumerable_System_Collections_Generic_KeyValuePair_string__int_
  {
    // Token: 0x17000022 RID: 34
    // (get) Token: 0x060000F1 RID: 241 RVA: 0x00511280 File Offset: 0x00511280
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_KeyValuePair_string__int_._initialized;
      }
    }

    // Token: 0x060000F2 RID: 242 RVA: 0x00511288 File Offset: 0x00511288
    private static bool Init()
    {
      return IEnumerableMethods<KeyValuePair<string, int>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060000F3 RID: 243 RVA: 0x00511298 File Offset: 0x00511298
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<KeyValuePair<string, int>>>.FromManaged(IEnumerableMethods<KeyValuePair<string, int>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000022 RID: 34
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_KeyValuePair_string__int_.Init();
  }
}
