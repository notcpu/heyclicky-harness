using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000029 RID: 41
  internal static class IDictionary_int_string
  {
    // Token: 0x17000028 RID: 40
    // (get) Token: 0x06000118 RID: 280 RVA: 0x005119C0 File Offset: 0x005119C0
    internal static bool Initialized
    {
      get
      {
        return IDictionary_int_string._initialized;
      }
    }

    // Token: 0x06000119 RID: 281 RVA: 0x005119C8 File Offset: 0x005119C8
    private static bool Init()
    {
      return IDictionaryMethods<int, int, string, IntPtr>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_GetView_3), ldftn(Do_Abi_Insert_4), ldftn(Do_Abi_Remove_5), ldftn(Do_Abi_Clear_6));
    }

    // Token: 0x0600011A RID: 282 RVA: 0x005119FC File Offset: 0x005119FC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, int key, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = IDictionaryMethods<int, string>.Abi_Lookup_0(thisPtr, key);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600011B RID: 283 RVA: 0x00511A44 File Offset: 0x00511A44
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, int key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<int, string>.Abi_HasKey_2(thisPtr, key);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600011C RID: 284 RVA: 0x00511A88 File Offset: 0x00511A88
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_3(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyDictionary<int, string> readOnlyDictionary = IDictionaryMethods<int, string>.Abi_GetView_3(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyDictionary<int, string>>.FromManaged(readOnlyDictionary);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600011D RID: 285 RVA: 0x00511AD0 File Offset: 0x00511AD0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Insert_4(IntPtr thisPtr, int key, IntPtr value, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IDictionaryMethods<int, string>.Abi_Insert_4(thisPtr, key, MarshalString.FromAbi(value));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600011E RID: 286 RVA: 0x00511B1C File Offset: 0x00511B1C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Remove_5(IntPtr thisPtr, int key)
    {
      try
      {
        IDictionaryMethods<int, string>.Abi_Remove_5(thisPtr, key);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600011F RID: 287 RVA: 0x00511B54 File Offset: 0x00511B54
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_6(IntPtr thisPtr)
    {
      try
      {
        IDictionaryMethods<int, string>.Abi_Clear_6(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000120 RID: 288 RVA: 0x00511B8C File Offset: 0x00511B8C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IDictionaryMethods<int, string>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000028 RID: 40
    private static readonly bool _initialized = IDictionary_int_string.Init();
  }
}
