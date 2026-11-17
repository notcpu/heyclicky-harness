using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005D RID: 93
  internal static class IEnumerable_bool
  {
    // Token: 0x1700005C RID: 92
    // (get) Token: 0x06000275 RID: 629 RVA: 0x00515B50 File Offset: 0x00515B50
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_bool._initialized;
      }
    }

    // Token: 0x06000276 RID: 630 RVA: 0x00515B58 File Offset: 0x00515B58
    private static bool Init()
    {
      return IEnumerableMethods<bool, byte>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000277 RID: 631 RVA: 0x00515B68 File Offset: 0x00515B68
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<bool>>.FromManaged(IEnumerableMethods<bool>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400005C RID: 92
    private static readonly bool _initialized = IEnumerable_bool.Init();
  }
}
