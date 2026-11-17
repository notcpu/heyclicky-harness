using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004E RID: 78
  internal static class IEnumerable_System_Collections_Generic_IEnumerable_byte_
  {
    // Token: 0x1700004D RID: 77
    // (get) Token: 0x06000211 RID: 529 RVA: 0x00514898 File Offset: 0x00514898
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IEnumerable_byte_._initialized;
      }
    }

    // Token: 0x06000212 RID: 530 RVA: 0x005148A0 File Offset: 0x005148A0
    private static bool Init()
    {
      return IEnumerableMethods<IEnumerable<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000213 RID: 531 RVA: 0x005148B0 File Offset: 0x005148B0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IEnumerable<byte>>>.FromManaged(IEnumerableMethods<IEnumerable<byte>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004D RID: 77
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IEnumerable_byte_.Init();
  }
}
