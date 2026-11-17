using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002E RID: 46
  internal static class IReadOnlyList_System_Collections_Generic_IList_byte_
  {
    // Token: 0x1700002D RID: 45
    // (get) Token: 0x0600013B RID: 315 RVA: 0x00512094 File Offset: 0x00512094
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IList_byte_._initialized;
      }
    }

    // Token: 0x0600013C RID: 316 RVA: 0x0051209C File Offset: 0x0051209C
    private static bool Init()
    {
      return IReadOnlyListMethods<IList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600013D RID: 317 RVA: 0x005120BC File Offset: 0x005120BC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IList<byte> list = IReadOnlyListMethods<IList<byte>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IList<byte>>.FromManaged(list);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600013E RID: 318 RVA: 0x00512104 File Offset: 0x00512104
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IList<byte>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IList<byte>>.FromAbi(value), out num);
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

    // Token: 0x0600013F RID: 319 RVA: 0x00512158 File Offset: 0x00512158
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IList<byte>[] array = MarshalInterface<IList<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IList<byte>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IList<byte>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000140 RID: 320 RVA: 0x005121B8 File Offset: 0x005121B8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IList<byte>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002D RID: 45
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IList_byte_.Init();
  }
}
