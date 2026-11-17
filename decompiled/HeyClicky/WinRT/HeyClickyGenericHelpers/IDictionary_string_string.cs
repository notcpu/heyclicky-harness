using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000046 RID: 70
  internal static class IDictionary_string_string
  {
    // Token: 0x17000045 RID: 69
    // (get) Token: 0x060001E1 RID: 481 RVA: 0x00514050 File Offset: 0x00514050
    internal static bool Initialized
    {
      get
      {
        return IDictionary_string_string._initialized;
      }
    }

    // Token: 0x060001E2 RID: 482 RVA: 0x00514058 File Offset: 0x00514058
    private static bool Init()
    {
      return IDictionaryMethods<string, IntPtr, string, IntPtr>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_GetView_3), ldftn(Do_Abi_Insert_4), ldftn(Do_Abi_Remove_5), ldftn(Do_Abi_Clear_6));
    }

    // Token: 0x060001E3 RID: 483 RVA: 0x0051408C File Offset: 0x0051408C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = IDictionaryMethods<string, string>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E4 RID: 484 RVA: 0x005140D8 File Offset: 0x005140D8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, string>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E5 RID: 485 RVA: 0x00514124 File Offset: 0x00514124
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_3(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyDictionary<string, string> readOnlyDictionary = IDictionaryMethods<string, string>.Abi_GetView_3(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyDictionary<string, string>>.FromManaged(readOnlyDictionary);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E6 RID: 486 RVA: 0x0051416C File Offset: 0x0051416C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Insert_4(IntPtr thisPtr, IntPtr key, IntPtr value, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, string>.Abi_Insert_4(thisPtr, MarshalString.FromAbi(key), MarshalString.FromAbi(value));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E7 RID: 487 RVA: 0x005141BC File Offset: 0x005141BC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Remove_5(IntPtr thisPtr, IntPtr key)
    {
      try
      {
        IDictionaryMethods<string, string>.Abi_Remove_5(thisPtr, MarshalString.FromAbi(key));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E8 RID: 488 RVA: 0x005141F8 File Offset: 0x005141F8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_6(IntPtr thisPtr)
    {
      try
      {
        IDictionaryMethods<string, string>.Abi_Clear_6(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001E9 RID: 489 RVA: 0x00514230 File Offset: 0x00514230
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IDictionaryMethods<string, string>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000045 RID: 69
    private static readonly bool _initialized = IDictionary_string_string.Init();
  }
}
