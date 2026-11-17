using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000D RID: 13
  internal static class IEnumerable_System_Collections_Generic_KeyValuePair_string__string_
  {
    // Token: 0x1700000C RID: 12
    // (get) Token: 0x06000048 RID: 72 RVA: 0x0050F0A0 File Offset: 0x0050F0A0
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_KeyValuePair_string__string_._initialized;
      }
    }

    // Token: 0x06000049 RID: 73 RVA: 0x0050F0A8 File Offset: 0x0050F0A8
    private static bool Init()
    {
      return IEnumerableMethods<KeyValuePair<string, string>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600004A RID: 74 RVA: 0x0050F0B8 File Offset: 0x0050F0B8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<KeyValuePair<string, string>>>.FromManaged(IEnumerableMethods<KeyValuePair<string, string>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000C RID: 12
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_KeyValuePair_string__string_.Init();
  }
}
