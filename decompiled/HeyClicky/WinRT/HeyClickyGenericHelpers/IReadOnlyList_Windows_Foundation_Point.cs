using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Windows.Foundation;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000002 RID: 2
  internal static class IReadOnlyList_Windows_Foundation_Point
  {
    // Token: 0x17000001 RID: 1
    // (get) Token: 0x06000002 RID: 2 RVA: 0x0050E3FC File Offset: 0x0050E3FC
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Windows_Foundation_Point._initialized;
      }
    }

    // Token: 0x06000003 RID: 3 RVA: 0x0050E404 File Offset: 0x0050E404
    private static bool Init()
    {
      return IReadOnlyListMethods<Point, Point>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000004 RID: 4 RVA: 0x0050E424 File Offset: 0x0050E424
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, Point* __return_value__)
    {
      Point point = default(Point);
      *__return_value__ = default(Point);
      try
      {
        point = IReadOnlyListMethods<Point>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = point;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000005 RID: 5 RVA: 0x0050E474 File Offset: 0x0050E474
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, Point value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<Point>.Abi_IndexOf_2(thisPtr, value, out num);
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

    // Token: 0x06000006 RID: 6 RVA: 0x0050E4C4 File Offset: 0x0050E4C4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Point[] array = MarshalBlittable<Point>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<Point>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalBlittable<Point>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000007 RID: 7 RVA: 0x0050E524 File Offset: 0x0050E524
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<Point>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000001 RID: 1
    private static readonly bool _initialized = IReadOnlyList_Windows_Foundation_Point.Init();
  }
}
