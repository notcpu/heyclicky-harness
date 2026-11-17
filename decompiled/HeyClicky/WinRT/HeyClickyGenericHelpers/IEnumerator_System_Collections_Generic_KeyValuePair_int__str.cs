using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000025 RID: 37
  internal static class IEnumerator_System_Collections_Generic_KeyValuePair_int__string_
  {
    // Token: 0x17000024 RID: 36
    // (get) Token: 0x060000FC RID: 252 RVA: 0x00511458 File Offset: 0x00511458
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_KeyValuePair_int__string_._initialized;
      }
    }

    // Token: 0x060000FD RID: 253 RVA: 0x00511460 File Offset: 0x00511460
    private static bool Init()
    {
      return IEnumeratorMethods<KeyValuePair<int, string>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000FE RID: 254 RVA: 0x00511480 File Offset: 0x00511480
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<int, string>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000FF RID: 255 RVA: 0x005114C4 File Offset: 0x005114C4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      KeyValuePair<int, string>[] array = MarshalInterface<KeyValuePair<int, string>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<KeyValuePair<int, string>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<KeyValuePair<int, string>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000100 RID: 256 RVA: 0x00511520 File Offset: 0x00511520
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      KeyValuePair<int, string> keyValuePair = default(KeyValuePair<int, string>);
      *__return_value__ = (IntPtr)0;
      try
      {
        keyValuePair = IEnumeratorMethods<KeyValuePair<int, string>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<KeyValuePair<int, string>>.FromManaged(keyValuePair);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000101 RID: 257 RVA: 0x0051156C File Offset: 0x0051156C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<int, string>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000024 RID: 36
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_KeyValuePair_int__string_.Init();
  }
}
