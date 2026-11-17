using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000030 RID: 48
  internal static class IEnumerable_System_Collections_Generic_KeyValuePair_int__string_
  {
    // Token: 0x1700002F RID: 47
    // (get) Token: 0x06000149 RID: 329 RVA: 0x00512374 File Offset: 0x00512374
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_KeyValuePair_int__string_._initialized;
      }
    }

    // Token: 0x0600014A RID: 330 RVA: 0x0051237C File Offset: 0x0051237C
    private static bool Init()
    {
      return IEnumerableMethods<KeyValuePair<int, string>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600014B RID: 331 RVA: 0x0051238C File Offset: 0x0051238C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<KeyValuePair<int, string>>>.FromManaged(IEnumerableMethods<KeyValuePair<int, string>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002F RID: 47
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_KeyValuePair_int__string_.Init();
  }
}
