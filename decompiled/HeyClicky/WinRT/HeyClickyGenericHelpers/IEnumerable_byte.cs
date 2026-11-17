using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000B RID: 11
  internal static class IEnumerable_byte
  {
    // Token: 0x1700000A RID: 10
    // (get) Token: 0x06000035 RID: 53 RVA: 0x0050EC94 File Offset: 0x0050EC94
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_byte._initialized;
      }
    }

    // Token: 0x06000036 RID: 54 RVA: 0x0050EC9C File Offset: 0x0050EC9C
    private static bool Init()
    {
      return IEnumerableMethods<byte, byte>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000037 RID: 55 RVA: 0x0050ECAC File Offset: 0x0050ECAC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<byte>>.FromManaged(IEnumerableMethods<byte>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000A RID: 10
    private static readonly bool _initialized = IEnumerable_byte.Init();
  }
}
