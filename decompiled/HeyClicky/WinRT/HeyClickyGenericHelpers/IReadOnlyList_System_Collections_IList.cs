using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002D RID: 45
  internal static class IReadOnlyList_System_Collections_IList
  {
    // Token: 0x1700002C RID: 44
    // (get) Token: 0x06000134 RID: 308 RVA: 0x00511F24 File Offset: 0x00511F24
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_IList._initialized;
      }
    }

    // Token: 0x06000135 RID: 309 RVA: 0x00511F2C File Offset: 0x00511F2C
    private static bool Init()
    {
      return IReadOnlyListMethods<IList, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000136 RID: 310 RVA: 0x00511F4C File Offset: 0x00511F4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IList list = IReadOnlyListMethods<IList>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IList>.FromManaged(list);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000137 RID: 311 RVA: 0x00511F94 File Offset: 0x00511F94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IList>.Abi_IndexOf_2(thisPtr, MarshalInterface<IList>.FromAbi(value), out num);
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

    // Token: 0x06000138 RID: 312 RVA: 0x00511FE8 File Offset: 0x00511FE8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IList[] array = MarshalInterface<IList>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IList>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IList>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000139 RID: 313 RVA: 0x00512048 File Offset: 0x00512048
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IList>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002C RID: 44
    private static readonly bool _initialized = IReadOnlyList_System_Collections_IList.Init();
  }
}
