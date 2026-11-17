using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200001F RID: 31
  internal static class IEnumerable_System_Collections_Generic_IList_byte_
  {
    // Token: 0x1700001E RID: 30
    // (get) Token: 0x060000D3 RID: 211 RVA: 0x00510CBC File Offset: 0x00510CBC
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IList_byte_._initialized;
      }
    }

    // Token: 0x060000D4 RID: 212 RVA: 0x00510CC4 File Offset: 0x00510CC4
    private static bool Init()
    {
      return IEnumerableMethods<IList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060000D5 RID: 213 RVA: 0x00510CD4 File Offset: 0x00510CD4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IList<byte>>>.FromManaged(IEnumerableMethods<IList<byte>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400001E RID: 30
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IList_byte_.Init();
  }
}
