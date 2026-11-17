using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005C RID: 92
  internal static class IList_bool
  {
    // Token: 0x1700005B RID: 91
    // (get) Token: 0x06000266 RID: 614 RVA: 0x005157BC File Offset: 0x005157BC
    internal static bool Initialized
    {
      get
      {
        return IList_bool._initialized;
      }
    }

    // Token: 0x06000267 RID: 615 RVA: 0x005157C4 File Offset: 0x005157C4
    private static bool Init()
    {
      return IListMethods<bool, byte>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x06000268 RID: 616 RVA: 0x00515820 File Offset: 0x00515820
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IListMethods<bool>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000269 RID: 617 RVA: 0x00515864 File Offset: 0x00515864
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<bool> readOnlyList = IListMethods<bool>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<bool>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600026A RID: 618 RVA: 0x005158AC File Offset: 0x005158AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, byte value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<bool>.Abi_IndexOf_3(thisPtr, value > 0, out num);
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

    // Token: 0x0600026B RID: 619 RVA: 0x00515900 File Offset: 0x00515900
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, byte value)
    {
      try
      {
        IListMethods<bool>.Abi_SetAt_4(thisPtr, index, value > 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600026C RID: 620 RVA: 0x0051593C File Offset: 0x0051593C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, byte value)
    {
      try
      {
        IListMethods<bool>.Abi_InsertAt_5(thisPtr, index, value > 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600026D RID: 621 RVA: 0x00515978 File Offset: 0x00515978
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<bool>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600026E RID: 622 RVA: 0x005159B0 File Offset: 0x005159B0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, byte value)
    {
      try
      {
        IListMethods<bool>.Abi_Append_7(thisPtr, value > 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600026F RID: 623 RVA: 0x005159EC File Offset: 0x005159EC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<bool>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000270 RID: 624 RVA: 0x00515A24 File Offset: 0x00515A24
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<bool>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000271 RID: 625 RVA: 0x00515A5C File Offset: 0x00515A5C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      bool[] array = MarshalNonBlittable<bool>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<bool>.Abi_GetMany_10(thisPtr, startIndex, ref array);
        MarshalNonBlittable<bool>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000272 RID: 626 RVA: 0x00515ABC File Offset: 0x00515ABC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<bool>.Abi_ReplaceAll_11(thisPtr, MarshalNonBlittable<bool>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000273 RID: 627 RVA: 0x00515B04 File Offset: 0x00515B04
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<bool>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400005B RID: 91
    private static readonly bool _initialized = IList_bool.Init();
  }
}
