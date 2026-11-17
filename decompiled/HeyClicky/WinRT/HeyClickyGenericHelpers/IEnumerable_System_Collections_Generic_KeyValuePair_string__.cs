using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000006 RID: 6
  internal static class IEnumerable_System_Collections_Generic_KeyValuePair_string__bool_
  {
    // Token: 0x17000005 RID: 5
    // (get) Token: 0x06000018 RID: 24 RVA: 0x0050E79C File Offset: 0x0050E79C
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_KeyValuePair_string__bool_._initialized;
      }
    }

    // Token: 0x06000019 RID: 25 RVA: 0x0050E7A4 File Offset: 0x0050E7A4
    private static bool Init()
    {
      return IEnumerableMethods<KeyValuePair<string, bool>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600001A RID: 26 RVA: 0x0050E7B4 File Offset: 0x0050E7B4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<KeyValuePair<string, bool>>>.FromManaged(IEnumerableMethods<KeyValuePair<string, bool>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000005 RID: 5
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_KeyValuePair_string__bool_.Init();
  }
}
