using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200001A RID: 26
  internal static class IEnumerator_System_Collections_Generic_KeyValuePair_string__bool_
  {
    // Token: 0x17000019 RID: 25
    // (get) Token: 0x060000B2 RID: 178 RVA: 0x0051067C File Offset: 0x0051067C
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_KeyValuePair_string__bool_._initialized;
      }
    }

    // Token: 0x060000B3 RID: 179 RVA: 0x00510684 File Offset: 0x00510684
    private static bool Init()
    {
      return IEnumeratorMethods<KeyValuePair<string, bool>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000B4 RID: 180 RVA: 0x005106A4 File Offset: 0x005106A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, bool>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000B5 RID: 181 RVA: 0x005106E8 File Offset: 0x005106E8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      KeyValuePair<string, bool>[] array = MarshalInterface<KeyValuePair<string, bool>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<KeyValuePair<string, bool>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<KeyValuePair<string, bool>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000B6 RID: 182 RVA: 0x00510744 File Offset: 0x00510744
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      KeyValuePair<string, bool> keyValuePair = default(KeyValuePair<string, bool>);
      *__return_value__ = (IntPtr)0;
      try
      {
        keyValuePair = IEnumeratorMethods<KeyValuePair<string, bool>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<KeyValuePair<string, bool>>.FromManaged(keyValuePair);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000B7 RID: 183 RVA: 0x00510790 File Offset: 0x00510790
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, bool>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000019 RID: 25
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_KeyValuePair_string__bool_.Init();
  }
}
