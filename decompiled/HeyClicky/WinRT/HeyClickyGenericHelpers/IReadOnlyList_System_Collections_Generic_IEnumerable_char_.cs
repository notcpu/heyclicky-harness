using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002F RID: 47
  internal static class IReadOnlyList_System_Collections_Generic_IEnumerable_char_
  {
    // Token: 0x1700002E RID: 46
    // (get) Token: 0x06000142 RID: 322 RVA: 0x00512204 File Offset: 0x00512204
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IEnumerable_char_._initialized;
      }
    }

    // Token: 0x06000143 RID: 323 RVA: 0x0051220C File Offset: 0x0051220C
    private static bool Init()
    {
      return IReadOnlyListMethods<IEnumerable<char>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000144 RID: 324 RVA: 0x0051222C File Offset: 0x0051222C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<char> enumerable = IReadOnlyListMethods<IEnumerable<char>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IEnumerable<char>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000145 RID: 325 RVA: 0x00512274 File Offset: 0x00512274
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IEnumerable<char>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IEnumerable<char>>.FromAbi(value), out num);
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

    // Token: 0x06000146 RID: 326 RVA: 0x005122C8 File Offset: 0x005122C8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<char>[] array = MarshalInterface<IEnumerable<char>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<char>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IEnumerable<char>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000147 RID: 327 RVA: 0x00512328 File Offset: 0x00512328
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<char>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002E RID: 46
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IEnumerable_char_.Init();
  }
}
