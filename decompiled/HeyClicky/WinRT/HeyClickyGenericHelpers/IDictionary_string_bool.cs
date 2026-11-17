using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000026 RID: 38
  internal static class IDictionary_string_bool
  {
    // Token: 0x17000025 RID: 37
    // (get) Token: 0x06000103 RID: 259 RVA: 0x005115BC File Offset: 0x005115BC
    internal static bool Initialized
    {
      get
      {
        return IDictionary_string_bool._initialized;
      }
    }

    // Token: 0x06000104 RID: 260 RVA: 0x005115C4 File Offset: 0x005115C4
    private static bool Init()
    {
      return IDictionaryMethods<string, IntPtr, bool, byte>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_GetView_3), ldftn(Do_Abi_Insert_4), ldftn(Do_Abi_Remove_5), ldftn(Do_Abi_Clear_6));
    }

    // Token: 0x06000105 RID: 261 RVA: 0x005115F8 File Offset: 0x005115F8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, bool>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000106 RID: 262 RVA: 0x00511644 File Offset: 0x00511644
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, bool>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000107 RID: 263 RVA: 0x00511690 File Offset: 0x00511690
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_3(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyDictionary<string, bool> readOnlyDictionary = IDictionaryMethods<string, bool>.Abi_GetView_3(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyDictionary<string, bool>>.FromManaged(readOnlyDictionary);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000108 RID: 264 RVA: 0x005116D8 File Offset: 0x005116D8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Insert_4(IntPtr thisPtr, IntPtr key, byte value, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, bool>.Abi_Insert_4(thisPtr, MarshalString.FromAbi(key), value > 0);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000109 RID: 265 RVA: 0x00511728 File Offset: 0x00511728
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Remove_5(IntPtr thisPtr, IntPtr key)
    {
      try
      {
        IDictionaryMethods<string, bool>.Abi_Remove_5(thisPtr, MarshalString.FromAbi(key));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600010A RID: 266 RVA: 0x00511764 File Offset: 0x00511764
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_6(IntPtr thisPtr)
    {
      try
      {
        IDictionaryMethods<string, bool>.Abi_Clear_6(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600010B RID: 267 RVA: 0x0051179C File Offset: 0x0051179C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IDictionaryMethods<string, bool>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000025 RID: 37
    private static readonly bool _initialized = IDictionary_string_bool.Init();
  }
}
