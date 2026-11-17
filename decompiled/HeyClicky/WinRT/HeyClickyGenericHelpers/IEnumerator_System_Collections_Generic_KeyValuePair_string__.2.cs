using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000021 RID: 33
  internal static class IEnumerator_System_Collections_Generic_KeyValuePair_string__string_
  {
    // Token: 0x17000020 RID: 32
    // (get) Token: 0x060000E6 RID: 230 RVA: 0x005110B4 File Offset: 0x005110B4
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_KeyValuePair_string__string_._initialized;
      }
    }

    // Token: 0x060000E7 RID: 231 RVA: 0x005110BC File Offset: 0x005110BC
    private static bool Init()
    {
      return IEnumeratorMethods<KeyValuePair<string, string>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000E8 RID: 232 RVA: 0x005110DC File Offset: 0x005110DC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, string>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000E9 RID: 233 RVA: 0x00511120 File Offset: 0x00511120
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      KeyValuePair<string, string>[] array = MarshalInterface<KeyValuePair<string, string>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<KeyValuePair<string, string>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<KeyValuePair<string, string>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000EA RID: 234 RVA: 0x0051117C File Offset: 0x0051117C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      KeyValuePair<string, string> keyValuePair = default(KeyValuePair<string, string>);
      *__return_value__ = (IntPtr)0;
      try
      {
        keyValuePair = IEnumeratorMethods<KeyValuePair<string, string>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<KeyValuePair<string, string>>.FromManaged(keyValuePair);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000EB RID: 235 RVA: 0x005111C8 File Offset: 0x005111C8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, string>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000020 RID: 32
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_KeyValuePair_string__string_.Init();
  }
}
