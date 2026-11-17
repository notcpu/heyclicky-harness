using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000027 RID: 39
  internal static class IReadOnlyList_System_Collections_Generic_IEnumerable_byte_
  {
    // Token: 0x17000026 RID: 38
    // (get) Token: 0x0600010D RID: 269 RVA: 0x005117E8 File Offset: 0x005117E8
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IEnumerable_byte_._initialized;
      }
    }

    // Token: 0x0600010E RID: 270 RVA: 0x005117F0 File Offset: 0x005117F0
    private static bool Init()
    {
      return IReadOnlyListMethods<IEnumerable<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600010F RID: 271 RVA: 0x00511810 File Offset: 0x00511810
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<byte> enumerable = IReadOnlyListMethods<IEnumerable<byte>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IEnumerable<byte>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000110 RID: 272 RVA: 0x00511858 File Offset: 0x00511858
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IEnumerable<byte>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IEnumerable<byte>>.FromAbi(value), out num);
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

    // Token: 0x06000111 RID: 273 RVA: 0x005118AC File Offset: 0x005118AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<byte>[] array = MarshalInterface<IEnumerable<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<byte>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IEnumerable<byte>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000112 RID: 274 RVA: 0x0051190C File Offset: 0x0051190C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IEnumerable<byte>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000026 RID: 38
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IEnumerable_byte_.Init();
  }
}
