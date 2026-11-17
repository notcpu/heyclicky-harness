using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005B RID: 91
  internal static class IEnumerator_int
  {
    // Token: 0x1700005A RID: 90
    // (get) Token: 0x0600025F RID: 607 RVA: 0x00515664 File Offset: 0x00515664
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_int._initialized;
      }
    }

    // Token: 0x06000260 RID: 608 RVA: 0x0051566C File Offset: 0x0051566C
    private static bool Init()
    {
      return IEnumeratorMethods<int, int>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000261 RID: 609 RVA: 0x0051568C File Offset: 0x0051568C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<int>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000262 RID: 610 RVA: 0x005156D0 File Offset: 0x005156D0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      int[] array = MarshalBlittable<int>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<int>.Abi_GetMany_3(thisPtr, ref array);
        MarshalBlittable<int>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000263 RID: 611 RVA: 0x0051572C File Offset: 0x0051572C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, int* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        int num = IEnumeratorMethods<int>.Abi_get_Current_0(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000264 RID: 612 RVA: 0x0051576C File Offset: 0x0051576C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<int>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400005A RID: 90
    private static readonly bool _initialized = IEnumerator_int.Init();
  }
}
