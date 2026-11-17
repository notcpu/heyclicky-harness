using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000003 RID: 3
  internal static class IEnumerable_char
  {
    // Token: 0x17000002 RID: 2
    // (get) Token: 0x06000009 RID: 9 RVA: 0x0050E570 File Offset: 0x0050E570
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_char._initialized;
      }
    }

    // Token: 0x0600000A RID: 10 RVA: 0x0050E578 File Offset: 0x0050E578
    private static bool Init()
    {
      return IEnumerableMethods<char, ushort>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600000B RID: 11 RVA: 0x0050E588 File Offset: 0x0050E588
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<char>>.FromManaged(IEnumerableMethods<char>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000002 RID: 2
    private static readonly bool _initialized = IEnumerable_char.Init();
  }
}
