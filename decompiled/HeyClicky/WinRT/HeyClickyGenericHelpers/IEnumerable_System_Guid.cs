using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004C RID: 76
  internal static class IEnumerable_System_Guid
  {
    // Token: 0x1700004B RID: 75
    // (get) Token: 0x06000206 RID: 518 RVA: 0x005146BC File Offset: 0x005146BC
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Guid._initialized;
      }
    }

    // Token: 0x06000207 RID: 519 RVA: 0x005146C4 File Offset: 0x005146C4
    private static bool Init()
    {
      return IEnumerableMethods<Guid, Guid>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000208 RID: 520 RVA: 0x005146D4 File Offset: 0x005146D4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<Guid>>.FromManaged(IEnumerableMethods<Guid>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004B RID: 75
    private static readonly bool _initialized = IEnumerable_System_Guid.Init();
  }
}
