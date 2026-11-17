using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000036 RID: 54
  internal static class IList_string
  {
    // Token: 0x17000035 RID: 53
    // (get) Token: 0x0600016D RID: 365 RVA: 0x005129A0 File Offset: 0x005129A0
    internal static bool Initialized
    {
      get
      {
        return IList_string._initialized;
      }
    }

    // Token: 0x0600016E RID: 366 RVA: 0x005129A8 File Offset: 0x005129A8
    private static bool Init()
    {
      return IListMethods<string, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x0600016F RID: 367 RVA: 0x00512A04 File Offset: 0x00512A04
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        string text = IListMethods<string>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalString.FromManaged(text);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000170 RID: 368 RVA: 0x00512A4C File Offset: 0x00512A4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<string> readOnlyList = IListMethods<string>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<string>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000171 RID: 369 RVA: 0x00512A94 File Offset: 0x00512A94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<string>.Abi_IndexOf_3(thisPtr, MarshalString.FromAbi(value), out num);
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

    // Token: 0x06000172 RID: 370 RVA: 0x00512AE8 File Offset: 0x00512AE8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<string>.Abi_SetAt_4(thisPtr, index, MarshalString.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000173 RID: 371 RVA: 0x00512B28 File Offset: 0x00512B28
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<string>.Abi_InsertAt_5(thisPtr, index, MarshalString.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000174 RID: 372 RVA: 0x00512B68 File Offset: 0x00512B68
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<string>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000175 RID: 373 RVA: 0x00512BA0 File Offset: 0x00512BA0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, IntPtr value)
    {
      try
      {
        IListMethods<string>.Abi_Append_7(thisPtr, MarshalString.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000176 RID: 374 RVA: 0x00512BDC File Offset: 0x00512BDC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<string>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000177 RID: 375 RVA: 0x00512C14 File Offset: 0x00512C14
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<string>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000178 RID: 376 RVA: 0x00512C4C File Offset: 0x00512C4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      string[] array = MarshalString.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<string>.Abi_GetMany_10(thisPtr, startIndex, ref array);
        Marshaler<string>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000179 RID: 377 RVA: 0x00512CB0 File Offset: 0x00512CB0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<string>.Abi_ReplaceAll_11(thisPtr, MarshalString.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600017A RID: 378 RVA: 0x00512CF8 File Offset: 0x00512CF8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<string>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000035 RID: 53
    private static readonly bool _initialized = IList_string.Init();
  }
}
