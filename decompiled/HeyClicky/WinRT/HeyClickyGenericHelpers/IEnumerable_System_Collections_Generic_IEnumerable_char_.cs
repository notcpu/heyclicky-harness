using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000022 RID: 34
  internal static class IEnumerable_System_Collections_Generic_IEnumerable_char_
  {
    // Token: 0x17000021 RID: 33
    // (get) Token: 0x060000ED RID: 237 RVA: 0x00511218 File Offset: 0x00511218
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IEnumerable_char_._initialized;
      }
    }

    // Token: 0x060000EE RID: 238 RVA: 0x00511220 File Offset: 0x00511220
    private static bool Init()
    {
      return IEnumerableMethods<IEnumerable<char>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060000EF RID: 239 RVA: 0x00511230 File Offset: 0x00511230
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IEnumerable<char>>>.FromManaged(IEnumerableMethods<IEnumerable<char>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000021 RID: 33
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IEnumerable_char_.Init();
  }
}
