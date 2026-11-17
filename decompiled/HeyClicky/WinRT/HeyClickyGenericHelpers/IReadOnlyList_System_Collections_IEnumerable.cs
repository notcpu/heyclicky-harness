using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000056 RID: 86
  internal static class IReadOnlyList_System_Collections_IEnumerable
  {
    // Token: 0x17000055 RID: 85
    // (get) Token: 0x06000241 RID: 577 RVA: 0x00515114 File Offset: 0x00515114
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_IEnumerable._initialized;
      }
    }

    // Token: 0x06000242 RID: 578 RVA: 0x0051511C File Offset: 0x0051511C
    private static bool Init()
    {
      return IReadOnlyListMethods<IEnumerable, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000243 RID: 579 RVA: 0x0051513C File Offset: 0x0051513C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable enumerable = IReadOnlyListMethods<IEnumerable>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IEnumerable>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000244 RID: 580 RVA: 0x00515184 File Offset: 0x00515184
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IEnumerable>.Abi_IndexOf_2(thisPtr, MarshalInterface<IEnumerable>.FromAbi(value), out num);
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

    // Token: 0x06000245 RID: 581 RVA: 0x005151D8 File Offset: 0x005151D8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable[] array = MarshalInterface<IEnumerable>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IEnumerable>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000246 RID: 582 RVA: 0x00515238 File Offset: 0x00515238
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000055 RID: 85
    private static readonly bool _initialized = IReadOnlyList_System_Collections_IEnumerable.Init();
  }
}
