using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000013 RID: 19
  internal static class IList_System_DateTimeOffset
  {
    // Token: 0x17000012 RID: 18
    // (get) Token: 0x06000074 RID: 116 RVA: 0x0050F93C File Offset: 0x0050F93C
    internal static bool Initialized
    {
      get
      {
        return IList_System_DateTimeOffset._initialized;
      }
    }

    // Token: 0x06000075 RID: 117 RVA: 0x0050F944 File Offset: 0x0050F944
    private static bool Init()
    {
      return IListMethods<DateTimeOffset, DateTimeOffset>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x06000076 RID: 118 RVA: 0x0050F9A0 File Offset: 0x0050F9A0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, DateTimeOffset* __return_value__)
    {
      DateTimeOffset dateTimeOffset = default(DateTimeOffset);
      *__return_value__ = default(DateTimeOffset);
      try
      {
        dateTimeOffset = IListMethods<DateTimeOffset>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = DateTimeOffset.FromManaged(dateTimeOffset);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000077 RID: 119 RVA: 0x0050F9F4 File Offset: 0x0050F9F4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<DateTimeOffset> readOnlyList = IListMethods<DateTimeOffset>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<DateTimeOffset>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000078 RID: 120 RVA: 0x0050FA3C File Offset: 0x0050FA3C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, DateTimeOffset value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<DateTimeOffset>.Abi_IndexOf_3(thisPtr, DateTimeOffset.FromAbi(value), out num);
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

    // Token: 0x06000079 RID: 121 RVA: 0x0050FA90 File Offset: 0x0050FA90
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, DateTimeOffset value)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_SetAt_4(thisPtr, index, DateTimeOffset.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007A RID: 122 RVA: 0x0050FAD0 File Offset: 0x0050FAD0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, DateTimeOffset value)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_InsertAt_5(thisPtr, index, DateTimeOffset.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007B RID: 123 RVA: 0x0050FB10 File Offset: 0x0050FB10
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007C RID: 124 RVA: 0x0050FB48 File Offset: 0x0050FB48
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, DateTimeOffset value)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_Append_7(thisPtr, DateTimeOffset.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007D RID: 125 RVA: 0x0050FB84 File Offset: 0x0050FB84
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007E RID: 126 RVA: 0x0050FBBC File Offset: 0x0050FBBC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600007F RID: 127 RVA: 0x0050FBF4 File Offset: 0x0050FBF4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      DateTimeOffset[] array = MarshalNonBlittable<DateTimeOffset>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<DateTimeOffset>.Abi_GetMany_10(thisPtr, startIndex, ref array);
        MarshalNonBlittable<DateTimeOffset>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000080 RID: 128 RVA: 0x0050FC54 File Offset: 0x0050FC54
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<DateTimeOffset>.Abi_ReplaceAll_11(thisPtr, MarshalNonBlittable<DateTimeOffset>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000081 RID: 129 RVA: 0x0050FC9C File Offset: 0x0050FC9C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<DateTimeOffset>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000012 RID: 18
    private static readonly bool _initialized = IList_System_DateTimeOffset.Init();
  }
}
