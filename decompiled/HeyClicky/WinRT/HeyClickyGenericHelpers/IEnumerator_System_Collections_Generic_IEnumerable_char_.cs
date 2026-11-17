using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000009 RID: 9
  internal static class IEnumerator_System_Collections_Generic_IEnumerable_char_
  {
    // Token: 0x17000008 RID: 8
    // (get) Token: 0x06000027 RID: 39 RVA: 0x0050E9CC File Offset: 0x0050E9CC
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IEnumerable_char_._initialized;
      }
    }

    // Token: 0x06000028 RID: 40 RVA: 0x0050E9D4 File Offset: 0x0050E9D4
    private static bool Init()
    {
      return IEnumeratorMethods<IEnumerable<char>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000029 RID: 41 RVA: 0x0050E9F4 File Offset: 0x0050E9F4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<char>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600002A RID: 42 RVA: 0x0050EA38 File Offset: 0x0050EA38
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<char>[] array = MarshalInterface<IEnumerable<char>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IEnumerable<char>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IEnumerable<char>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600002B RID: 43 RVA: 0x0050EA94 File Offset: 0x0050EA94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<char> enumerable = IEnumeratorMethods<IEnumerable<char>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IEnumerable<char>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600002C RID: 44 RVA: 0x0050EADC File Offset: 0x0050EADC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<char>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000008 RID: 8
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IEnumerable_char_.Init();
  }
}
