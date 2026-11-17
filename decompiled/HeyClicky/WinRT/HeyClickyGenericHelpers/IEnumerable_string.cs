using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005A RID: 90
  internal static class IEnumerable_string
  {
    // Token: 0x17000059 RID: 89
    // (get) Token: 0x0600025B RID: 603 RVA: 0x005155FC File Offset: 0x005155FC
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_string._initialized;
      }
    }

    // Token: 0x0600025C RID: 604 RVA: 0x00515604 File Offset: 0x00515604
    private static bool Init()
    {
      return IEnumerableMethods<string, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600025D RID: 605 RVA: 0x00515614 File Offset: 0x00515614
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<string>>.FromManaged(IEnumerableMethods<string>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000059 RID: 89
    private static readonly bool _initialized = IEnumerable_string.Init();
  }
}
