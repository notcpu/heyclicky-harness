using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000032 RID: 50
  internal static class IReadOnlyList_System_Collections_Generic_IReadOnlyList_object_
  {
    // Token: 0x17000031 RID: 49
    // (get) Token: 0x06000157 RID: 343 RVA: 0x00512600 File Offset: 0x00512600
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IReadOnlyList_object_._initialized;
      }
    }

    // Token: 0x06000158 RID: 344 RVA: 0x00512608 File Offset: 0x00512608
    private static bool Init()
    {
      return IReadOnlyListMethods<IReadOnlyList<object>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000159 RID: 345 RVA: 0x00512628 File Offset: 0x00512628
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<object> readOnlyList = IReadOnlyListMethods<IReadOnlyList<object>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IReadOnlyList<object>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600015A RID: 346 RVA: 0x00512670 File Offset: 0x00512670
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IReadOnlyList<object>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IReadOnlyList<object>>.FromAbi(value), out num);
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

    // Token: 0x0600015B RID: 347 RVA: 0x005126C4 File Offset: 0x005126C4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IReadOnlyList<object>[] array = MarshalInterface<IReadOnlyList<object>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IReadOnlyList<object>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IReadOnlyList<object>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600015C RID: 348 RVA: 0x00512724 File Offset: 0x00512724
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IReadOnlyList<object>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000031 RID: 49
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IReadOnlyList_object_.Init();
  }
}
