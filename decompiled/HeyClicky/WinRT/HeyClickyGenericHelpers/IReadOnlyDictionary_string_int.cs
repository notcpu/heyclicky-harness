using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000058 RID: 88
  internal static class IReadOnlyDictionary_string_int
  {
    // Token: 0x17000057 RID: 87
    // (get) Token: 0x0600024F RID: 591 RVA: 0x005153F0 File Offset: 0x005153F0
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyDictionary_string_int._initialized;
      }
    }

    // Token: 0x06000250 RID: 592 RVA: 0x005153F8 File Offset: 0x005153F8
    private static bool Init()
    {
      return IReadOnlyDictionaryMethods<string, IntPtr, int, int>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_Split_3));
    }

    // Token: 0x06000251 RID: 593 RVA: 0x00515418 File Offset: 0x00515418
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, int* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        int num = IReadOnlyDictionaryMethods<string, int>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000252 RID: 594 RVA: 0x00515460 File Offset: 0x00515460
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyDictionaryMethods<string, int>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000253 RID: 595 RVA: 0x005154AC File Offset: 0x005154AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Split_3(IntPtr thisPtr, IntPtr* first, IntPtr* second)
    {
      *first = (IntPtr)0;
      *second = (IntPtr)0;
      IntPtr intPtr = (IntPtr)0;
      IntPtr intPtr2 = (IntPtr)0;
      try
      {
        IReadOnlyDictionaryMethods<string, int>.Abi_Split_3(thisPtr, out intPtr, out intPtr2);
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

    // Token: 0x06000254 RID: 596 RVA: 0x005154FC File Offset: 0x005154FC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyDictionaryMethods<string, int>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000057 RID: 87
    private static readonly bool _initialized = IReadOnlyDictionary_string_int.Init();
  }
}
