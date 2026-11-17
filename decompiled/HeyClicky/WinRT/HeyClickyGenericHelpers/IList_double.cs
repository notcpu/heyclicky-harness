using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000042 RID: 66
  internal static class IList_double
  {
    // Token: 0x17000041 RID: 65
    // (get) Token: 0x060001C0 RID: 448 RVA: 0x00513998 File Offset: 0x00513998
    internal static bool Initialized
    {
      get
      {
        return IList_double._initialized;
      }
    }

    // Token: 0x060001C1 RID: 449 RVA: 0x005139A0 File Offset: 0x005139A0
    private static bool Init()
    {
      return IListMethods<double, double>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x060001C2 RID: 450 RVA: 0x005139FC File Offset: 0x005139FC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, double* __return_value__)
    {
      *__return_value__ = 0.0;
      try
      {
        double num = IListMethods<double>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C3 RID: 451 RVA: 0x00513A4C File Offset: 0x00513A4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<double> readOnlyList = IListMethods<double>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<double>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C4 RID: 452 RVA: 0x00513A94 File Offset: 0x00513A94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, double value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<double>.Abi_IndexOf_3(thisPtr, value, out num);
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

    // Token: 0x060001C5 RID: 453 RVA: 0x00513AE4 File Offset: 0x00513AE4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, double value)
    {
      try
      {
        IListMethods<double>.Abi_SetAt_4(thisPtr, index, value);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C6 RID: 454 RVA: 0x00513B1C File Offset: 0x00513B1C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, double value)
    {
      try
      {
        IListMethods<double>.Abi_InsertAt_5(thisPtr, index, value);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C7 RID: 455 RVA: 0x00513B54 File Offset: 0x00513B54
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<double>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C8 RID: 456 RVA: 0x00513B8C File Offset: 0x00513B8C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, double value)
    {
      try
      {
        IListMethods<double>.Abi_Append_7(thisPtr, value);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001C9 RID: 457 RVA: 0x00513BC4 File Offset: 0x00513BC4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<double>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001CA RID: 458 RVA: 0x00513BFC File Offset: 0x00513BFC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<double>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001CB RID: 459 RVA: 0x00513C34 File Offset: 0x00513C34
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      double[] array = MarshalBlittable<double>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<double>.Abi_GetMany_10(thisPtr, startIndex, ref array);
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

    // Token: 0x060001CC RID: 460 RVA: 0x00513C94 File Offset: 0x00513C94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<double>.Abi_ReplaceAll_11(thisPtr, MarshalBlittable<double>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001CD RID: 461 RVA: 0x00513CDC File Offset: 0x00513CDC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<double>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000041 RID: 65
    private static readonly bool _initialized = IList_double.Init();
  }
}
