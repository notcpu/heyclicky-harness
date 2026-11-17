using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000010 RID: 16
  internal static class IReadOnlyDictionary_string_string
  {
    // Token: 0x1700000F RID: 15
    // (get) Token: 0x06000057 RID: 87 RVA: 0x0050F2DC File Offset: 0x0050F2DC
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyDictionary_string_string._initialized;
      }
    }

    // Token: 0x06000058 RID: 88 RVA: 0x0050F2E4 File Offset: 0x0050F2E4
    private static bool Init()
    {
      return IReadOnlyDictionaryMethods<string, IntPtr, string, IntPtr>.InitCcw(ldftn(Do_Abi_Lookup_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_HasKey_2), ldftn(Do_Abi_Split_3));
    }

    // Token: 0x06000059 RID: 89 RVA: 0x0050F304 File Offset: 0x0050F304
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Lookup_0(IntPtr thisPtr, IntPtr key, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = IReadOnlyDictionaryMethods<string, string>.Abi_Lookup_0(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600005A RID: 90 RVA: 0x0050F350 File Offset: 0x0050F350
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_HasKey_2(IntPtr thisPtr, IntPtr key, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyDictionaryMethods<string, string>.Abi_HasKey_2(thisPtr, MarshalString.FromAbi(key));
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600005B RID: 91 RVA: 0x0050F39C File Offset: 0x0050F39C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_Split_3(IntPtr thisPtr, IntPtr* first, IntPtr* second)
    {
      *first = (IntPtr)0;
      *second = (IntPtr)0;
      IntPtr intPtr = (IntPtr)0;
      IntPtr intPtr2 = (IntPtr)0;
      try
      {
        IReadOnlyDictionaryMethods<string, string>.Abi_Split_3(thisPtr, out intPtr, out intPtr2);
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

    // Token: 0x0600005C RID: 92 RVA: 0x0050F3EC File Offset: 0x0050F3EC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyDictionaryMethods<string, string>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000F RID: 15
    private static readonly bool _initialized = IReadOnlyDictionary_string_string.Init();
  }
}
