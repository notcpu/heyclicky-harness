using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200001E RID: 30
  internal static class KeyValuePair_string_int
  {
    // Token: 0x1700001D RID: 29
    // (get) Token: 0x060000CE RID: 206 RVA: 0x00510C0C File Offset: 0x00510C0C
    internal static bool Initialized
    {
      get
      {
        return KeyValuePair_string_int._initialized;
      }
    }

    // Token: 0x060000CF RID: 207 RVA: 0x00510C14 File Offset: 0x00510C14
    private static bool Init()
    {
      return KeyValuePairMethods<string, IntPtr, int, int>.InitCcw(ldftn(Do_Abi_get_Key_0), ldftn(Do_Abi_get_Value_1));
    }

    // Token: 0x060000D0 RID: 208 RVA: 0x00510C28 File Offset: 0x00510C28
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Key_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = KeyValuePairMethods<string, int>.Abi_get_Key_0(thisPtr);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000D1 RID: 209 RVA: 0x00510C70 File Offset: 0x00510C70
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Value_1(IntPtr thisPtr, int* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        int num = KeyValuePairMethods<string, int>.Abi_get_Value_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400001D RID: 29
    private static readonly bool _initialized = KeyValuePair_string_int.Init();
  }
}
