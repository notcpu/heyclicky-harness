using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004A RID: 74
  internal static class KeyValuePair_int_string
  {
    // Token: 0x17000049 RID: 73
    // (get) Token: 0x060001FD RID: 509 RVA: 0x005145A4 File Offset: 0x005145A4
    internal static bool Initialized
    {
      get
      {
        return KeyValuePair_int_string._initialized;
      }
    }

    // Token: 0x060001FE RID: 510 RVA: 0x005145AC File Offset: 0x005145AC
    private static bool Init()
    {
      return KeyValuePairMethods<int, int, string, IntPtr>.InitCcw(ldftn(Do_Abi_get_Key_0), ldftn(Do_Abi_get_Value_1));
    }

    // Token: 0x060001FF RID: 511 RVA: 0x005145C0 File Offset: 0x005145C0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Key_0(IntPtr thisPtr, int* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        int num = KeyValuePairMethods<int, string>.Abi_get_Key_0(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000200 RID: 512 RVA: 0x00514600 File Offset: 0x00514600
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Value_1(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = KeyValuePairMethods<int, string>.Abi_get_Value_1(thisPtr);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000049 RID: 73
    private static readonly bool _initialized = KeyValuePair_int_string.Init();
  }
}
