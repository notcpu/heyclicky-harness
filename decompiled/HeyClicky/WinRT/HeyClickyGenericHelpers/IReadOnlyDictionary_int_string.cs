using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000048 RID: 72
  internal static class IReadOnlyDictionary_int_string
  {
    // Token: 0x17000047 RID: 71
    // (get) Token: 0x060001F2 RID: 498 RVA: 0x005143EC File Offset: 0x005143EC
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyDictionary_int_string._initialized;
      }
    }

    // Token: 0x060001F3 RID: 499 RVA: 0x005143F4 File Offset: 0x005143F4
    private static bool Init()
    {
      return IReadOnlyDictionaryMethods<int, int, string, IntPtr>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_Split_3));
    }

    // Token: 0x060001F4 RID: 500 RVA: 0x00514414 File Offset: 0x00514414
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, int key, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = IReadOnlyDictionaryMethods<int, string>.Abi_Lookup_0(thisPtr, key);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001F5 RID: 501 RVA: 0x0051445C File Offset: 0x0051445C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, int key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyDictionaryMethods<int, string>.Abi_HasKey_2(thisPtr, key);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001F6 RID: 502 RVA: 0x005144A0 File Offset: 0x005144A0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Split_3(IntPtr thisPtr, IntPtr* first, IntPtr* second)
    {
      *first = (IntPtr)0;
      *second = (IntPtr)0;
      IntPtr intPtr = (IntPtr)0;
      IntPtr intPtr2 = (IntPtr)0;
      try
      {
        IReadOnlyDictionaryMethods<int, string>.Abi_Split_3(thisPtr, out intPtr, out intPtr2);
        *first = intPtr;
        *second = intPtr2;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001F7 RID: 503 RVA: 0x005144F0 File Offset: 0x005144F0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyDictionaryMethods<int, string>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000047 RID: 71
    private static readonly bool _initialized = IReadOnlyDictionary_int_string.Init();
  }
}
