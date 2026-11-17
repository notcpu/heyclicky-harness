using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000008 RID: 8
  internal static class IEnumerator_System_Collections_IEnumerable
  {
    // Token: 0x17000007 RID: 7
    // (get) Token: 0x06000020 RID: 32 RVA: 0x0050E86C File Offset: 0x0050E86C
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_IEnumerable._initialized;
      }
    }

    // Token: 0x06000021 RID: 33 RVA: 0x0050E874 File Offset: 0x0050E874
    private static bool Init()
    {
      return IEnumeratorMethods<IEnumerable, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000022 RID: 34 RVA: 0x0050E894 File Offset: 0x0050E894
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000023 RID: 35 RVA: 0x0050E8D8 File Offset: 0x0050E8D8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable[] array = MarshalInterface<IEnumerable>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IEnumerable>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IEnumerable>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000024 RID: 36 RVA: 0x0050E934 File Offset: 0x0050E934
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable enumerable = IEnumeratorMethods<IEnumerable>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IEnumerable>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000025 RID: 37 RVA: 0x0050E97C File Offset: 0x0050E97C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000007 RID: 7
    private static readonly bool _initialized = IEnumerator_System_Collections_IEnumerable.Init();
  }
}
