using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005F RID: 95
  internal static class IEnumerable_System_DateTimeOffset
  {
    // Token: 0x1700005E RID: 94
    // (get) Token: 0x06000280 RID: 640 RVA: 0x00515D2C File Offset: 0x00515D2C
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_DateTimeOffset._initialized;
      }
    }

    // Token: 0x06000281 RID: 641 RVA: 0x00515D34 File Offset: 0x00515D34
    private static bool Init()
    {
      return IEnumerableMethods<DateTimeOffset, DateTimeOffset>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000282 RID: 642 RVA: 0x00515D44 File Offset: 0x00515D44
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<DateTimeOffset>>.FromManaged(IEnumerableMethods<DateTimeOffset>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400005E RID: 94
    private static readonly bool _initialized = IEnumerable_System_DateTimeOffset.Init();
  }
}
