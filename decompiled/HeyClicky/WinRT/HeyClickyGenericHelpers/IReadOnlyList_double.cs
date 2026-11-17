using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003D RID: 61
  internal static class IReadOnlyList_double
  {
    // Token: 0x1700003C RID: 60
    // (get) Token: 0x060001A3 RID: 419 RVA: 0x00513494 File Offset: 0x00513494
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_double._initialized;
      }
    }

    // Token: 0x060001A4 RID: 420 RVA: 0x0051349C File Offset: 0x0051349C
    private static bool Init()
    {
      return IReadOnlyListMethods<double, double>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060001A5 RID: 421 RVA: 0x005134BC File Offset: 0x005134BC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, double* __return_value__)
    {
      *__return_value__ = 0.0;
      try
      {
        double num = IReadOnlyListMethods<double>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001A6 RID: 422 RVA: 0x0051350C File Offset: 0x0051350C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, double value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<double>.Abi_IndexOf_2(thisPtr, value, out num);
        *index = num;
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001A7 RID: 423 RVA: 0x0051355C File Offset: 0x0051355C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      double[] array = MarshalBlittable<double>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<double>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalBlittable<double>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001A8 RID: 424 RVA: 0x005135BC File Offset: 0x005135BC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<double>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400003C RID: 60
    private static readonly bool _initialized = IReadOnlyList_double.Init();
  }
}
