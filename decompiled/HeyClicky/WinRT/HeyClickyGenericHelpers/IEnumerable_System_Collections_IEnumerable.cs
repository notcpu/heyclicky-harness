using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004B RID: 75
  internal static class IEnumerable_System_Collections_IEnumerable
  {
    // Token: 0x1700004A RID: 74
    // (get) Token: 0x06000202 RID: 514 RVA: 0x00514654 File Offset: 0x00514654
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_IEnumerable._initialized;
      }
    }

    // Token: 0x06000203 RID: 515 RVA: 0x0051465C File Offset: 0x0051465C
    private static bool Init()
    {
      return IEnumerableMethods<IEnumerable, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000204 RID: 516 RVA: 0x0051466C File Offset: 0x0051466C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IEnumerable>>.FromManaged(IEnumerableMethods<IEnumerable>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004A RID: 74
    private static readonly bool _initialized = IEnumerable_System_Collections_IEnumerable.Init();
  }
}
