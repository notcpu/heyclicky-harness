using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003C RID: 60
  internal static class IReadOnlyDictionary_string_bool
  {
    // Token: 0x1700003B RID: 59
    // (get) Token: 0x0600019C RID: 412 RVA: 0x00513338 File Offset: 0x00513338
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyDictionary_string_bool._initialized;
      }
    }

    // Token: 0x0600019D RID: 413 RVA: 0x00513340 File Offset: 0x00513340
    private static bool Init()
    {
      return IReadOnlyDictionaryMethods<string, IntPtr, bool, byte>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_Split_3));
    }

    // Token: 0x0600019E RID: 414 RVA: 0x00513360 File Offset: 0x00513360
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyDictionaryMethods<string, bool>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600019F RID: 415 RVA: 0x005133AC File Offset: 0x005133AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyDictionaryMethods<string, bool>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001A0 RID: 416 RVA: 0x005133F8 File Offset: 0x005133F8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Split_3(IntPtr thisPtr, IntPtr* first, IntPtr* second)
    {
      *first = (IntPtr)0;
      *second = (IntPtr)0;
      IntPtr intPtr = (IntPtr)0;
      IntPtr intPtr2 = (IntPtr)0;
      try
      {
        IReadOnlyDictionaryMethods<string, bool>.Abi_Split_3(thisPtr, out intPtr, out intPtr2);
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

    // Token: 0x060001A1 RID: 417 RVA: 0x00513448 File Offset: 0x00513448
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyDictionaryMethods<string, bool>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400003B RID: 59
    private static readonly bool _initialized = IReadOnlyDictionary_string_bool.Init();
  }
}
