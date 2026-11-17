using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000011 RID: 17
  internal static class IEnumerator_System_Collections_Generic_IReadOnlyList_object_
  {
    // Token: 0x17000010 RID: 16
    // (get) Token: 0x0600005E RID: 94 RVA: 0x0050F438 File Offset: 0x0050F438
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IReadOnlyList_object_._initialized;
      }
    }

    // Token: 0x0600005F RID: 95 RVA: 0x0050F440 File Offset: 0x0050F440
    private static bool Init()
    {
      return IEnumeratorMethods<IReadOnlyList<object>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000060 RID: 96 RVA: 0x0050F460 File Offset: 0x0050F460
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IReadOnlyList<object>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000061 RID: 97 RVA: 0x0050F4A4 File Offset: 0x0050F4A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IReadOnlyList<object>[] array = MarshalInterface<IReadOnlyList<object>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IReadOnlyList<object>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IReadOnlyList<object>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000062 RID: 98 RVA: 0x0050F500 File Offset: 0x0050F500
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<object> readOnlyList = IEnumeratorMethods<IReadOnlyList<object>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<object>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000063 RID: 99 RVA: 0x0050F548 File Offset: 0x0050F548
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IReadOnlyList<object>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000010 RID: 16
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IReadOnlyList_object_.Init();
  }
}
