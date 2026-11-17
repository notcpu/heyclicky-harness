using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200001B RID: 27
  internal static class IReadOnlyList_System_Collections_Generic_IEnumerable_object_
  {
    // Token: 0x1700001A RID: 26
    // (get) Token: 0x060000B9 RID: 185 RVA: 0x005107E0 File Offset: 0x005107E0
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IEnumerable_object_._initialized;
      }
    }

    // Token: 0x060000BA RID: 186 RVA: 0x005107E8 File Offset: 0x005107E8
    private static bool Init()
    {
      return IReadOnlyListMethods<IEnumerable<object>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000BB RID: 187 RVA: 0x00510808 File Offset: 0x00510808
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<object> enumerable = IReadOnlyListMethods<IEnumerable<object>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IEnumerable<object>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000BC RID: 188 RVA: 0x00510850 File Offset: 0x00510850
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IEnumerable<object>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IEnumerable<object>>.FromAbi(value), out num);
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

    // Token: 0x060000BD RID: 189 RVA: 0x005108A4 File Offset: 0x005108A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<object>[] array = MarshalInterface<IEnumerable<object>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<object>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IEnumerable<object>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000BE RID: 190 RVA: 0x00510904 File Offset: 0x00510904
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<object>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400001A RID: 26
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IEnumerable_object_.Init();
  }
}
