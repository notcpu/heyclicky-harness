using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003A RID: 58
  internal static class IEnumerable_System_Collections_Generic_IReadOnlyList_byte_
  {
    // Token: 0x17000039 RID: 57
    // (get) Token: 0x06000191 RID: 401 RVA: 0x00513168 File Offset: 0x00513168
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_System_Collections_Generic_IReadOnlyList_byte_._initialized;
      }
    }

    // Token: 0x06000192 RID: 402 RVA: 0x00513170 File Offset: 0x00513170
    private static bool Init()
    {
      return IEnumerableMethods<IReadOnlyList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000193 RID: 403 RVA: 0x00513180 File Offset: 0x00513180
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IReadOnlyList<byte>>>.FromManaged(IEnumerableMethods<IReadOnlyList<byte>>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000039 RID: 57
    private static readonly bool _initialized = IEnumerable_System_Collections_Generic_IReadOnlyList_byte_.Init();
  }
}
