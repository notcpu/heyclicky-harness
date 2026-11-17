using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000028 RID: 40
  internal static class IEnumerable_System_Collections_IList
  {
    // Token: 0x17000027 RID: 39
    // (get) Token: 0x06000114 RID: 276 RVA: 0x00511958 File Offset: 0x00511958
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_IList._initialized;
      }
    }

    // Token: 0x06000115 RID: 277 RVA: 0x00511960 File Offset: 0x00511960
    private static bool Init()
    {
      return IEnumerableMethods<IList, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000116 RID: 278 RVA: 0x00511970 File Offset: 0x00511970
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IList>>.FromManaged(IEnumerableMethods<IList>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000027 RID: 39
    private static readonly bool _initialized = IEnumerable_System_Collections_IList.Init();
  }
}
