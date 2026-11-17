using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004F RID: 79
  internal static class KeyValuePair_string_string
  {
    // Token: 0x1700004E RID: 78
    // (get) Token: 0x06000215 RID: 533 RVA: 0x00514900 File Offset: 0x00514900
    internal static bool Initialized
    {
      get
      {
        return KeyValuePair_string_string._initialized;
      }
    }

    // Token: 0x06000216 RID: 534 RVA: 0x00514908 File Offset: 0x00514908
    private static bool Init()
    {
      return KeyValuePairMethods<string, IntPtr, string, IntPtr>.InitCcw(ldftn(Do_Abi_get_Key_0), ldftn(Do_Abi_get_Value_1));
    }

    // Token: 0x06000217 RID: 535 RVA: 0x0051491C File Offset: 0x0051491C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Key_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = KeyValuePairMethods<string, string>.Abi_get_Key_0(thisPtr);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000218 RID: 536 RVA: 0x00514964 File Offset: 0x00514964
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Value_1(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = KeyValuePairMethods<string, string>.Abi_get_Value_1(thisPtr);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004E RID: 78
    private static readonly bool _initialized = KeyValuePair_string_string.Init();
  }
}
