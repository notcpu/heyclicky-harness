using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000059 RID: 89
  internal static class KeyValuePair_string_bool
  {
    // Token: 0x17000058 RID: 88
    // (get) Token: 0x06000256 RID: 598 RVA: 0x00515548 File Offset: 0x00515548
    internal static bool Initialized
    {
      get
      {
        return KeyValuePair_string_bool._initialized;
      }
    }

    // Token: 0x06000257 RID: 599 RVA: 0x00515550 File Offset: 0x00515550
    private static bool Init()
    {
      return KeyValuePairMethods<string, IntPtr, bool, byte>.InitCcw(ldftn(Do_Abi_get_Key_0), ldftn(Do_Abi_get_Value_1));
    }

    // Token: 0x06000258 RID: 600 RVA: 0x00515564 File Offset: 0x00515564
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Key_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = KeyValuePairMethods<string, bool>.Abi_get_Key_0(thisPtr);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000259 RID: 601 RVA: 0x005155AC File Offset: 0x005155AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Value_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = KeyValuePairMethods<string, bool>.Abi_get_Value_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000058 RID: 88
    private static readonly bool _initialized = KeyValuePair_string_bool.Init();
  }
}
