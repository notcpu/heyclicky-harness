using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Windows.Foundation;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000015 RID: 21
  internal static class IEnumerator_Windows_Foundation_Point
  {
    // Token: 0x17000014 RID: 20
    // (get) Token: 0x0600008A RID: 138 RVA: 0x0050FE58 File Offset: 0x0050FE58
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Windows_Foundation_Point._initialized;
      }
    }

    // Token: 0x0600008B RID: 139 RVA: 0x0050FE60 File Offset: 0x0050FE60
    private static bool Init()
    {
      return IEnumeratorMethods<Point, Point>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600008C RID: 140 RVA: 0x0050FE80 File Offset: 0x0050FE80
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Point>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600008D RID: 141 RVA: 0x0050FEC4 File Offset: 0x0050FEC4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Point[] array = MarshalBlittable<Point>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<Point>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x0600008E RID: 142 RVA: 0x0050FF20 File Offset: 0x0050FF20
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, Point* __return_value__)
    {
      Point point = default(Point);
      *__return_value__ = default(Point);
      try
      {
        point = IEnumeratorMethods<Point>.Abi_get_Current_0(thisPtr);
        *__return_value__ = point;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600008F RID: 143 RVA: 0x0050FF70 File Offset: 0x0050FF70
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Point>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000014 RID: 20
    private static readonly bool _initialized = IEnumerator_Windows_Foundation_Point.Init();
  }
}
