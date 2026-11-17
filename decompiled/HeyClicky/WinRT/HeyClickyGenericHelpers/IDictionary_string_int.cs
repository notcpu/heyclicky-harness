using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000031 RID: 49
  internal static class IDictionary_string_int
  {
    // Token: 0x17000030 RID: 48
    // (get) Token: 0x0600014D RID: 333 RVA: 0x005123DC File Offset: 0x005123DC
    internal static bool Initialized
    {
      get
      {
        return IDictionary_string_int._initialized;
      }
    }

    // Token: 0x0600014E RID: 334 RVA: 0x005123E4 File Offset: 0x005123E4
    private static bool Init()
    {
      return IDictionaryMethods<string, IntPtr, int, int>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_GetView_3), ldftn(Do_Abi_Insert_4), ldftn(Do_Abi_Remove_5), ldftn(Do_Abi_Clear_6));
    }

    // Token: 0x0600014F RID: 335 RVA: 0x00512418 File Offset: 0x00512418
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, int* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        int num = IDictionaryMethods<string, int>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000150 RID: 336 RVA: 0x00512460 File Offset: 0x00512460
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, int>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000151 RID: 337 RVA: 0x005124AC File Offset: 0x005124AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_3(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyDictionary<string, int> readOnlyDictionary = IDictionaryMethods<string, int>.Abi_GetView_3(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyDictionary<string, int>>.FromManaged(readOnlyDictionary);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000152 RID: 338 RVA: 0x005124F4 File Offset: 0x005124F4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Insert_4(IntPtr thisPtr, IntPtr key, int value, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<string, int>.Abi_Insert_4(thisPtr, MarshalString.FromAbi(key), value);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000153 RID: 339 RVA: 0x00512540 File Offset: 0x00512540
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Remove_5(IntPtr thisPtr, IntPtr key)
    {
      try
      {
        IDictionaryMethods<string, int>.Abi_Remove_5(thisPtr, MarshalString.FromAbi(key));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000154 RID: 340 RVA: 0x0051257C File Offset: 0x0051257C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_6(IntPtr thisPtr)
    {
      try
      {
        IDictionaryMethods<string, int>.Abi_Clear_6(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000155 RID: 341 RVA: 0x005125B4 File Offset: 0x005125B4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IDictionaryMethods<string, int>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000030 RID: 48
    private static readonly bool _initialized = IDictionary_string_int.Init();
  }
}
