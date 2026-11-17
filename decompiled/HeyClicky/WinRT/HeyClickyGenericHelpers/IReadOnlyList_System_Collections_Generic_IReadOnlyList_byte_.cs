using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000014 RID: 20
  internal static class IReadOnlyList_System_Collections_Generic_IReadOnlyList_byte_
  {
    // Token: 0x17000013 RID: 19
    // (get) Token: 0x06000083 RID: 131 RVA: 0x0050FCE8 File Offset: 0x0050FCE8
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_Collections_Generic_IReadOnlyList_byte_._initialized;
      }
    }

    // Token: 0x06000084 RID: 132 RVA: 0x0050FCF0 File Offset: 0x0050FCF0
    private static bool Init()
    {
      return IReadOnlyListMethods<IReadOnlyList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000085 RID: 133 RVA: 0x0050FD10 File Offset: 0x0050FD10
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<byte> readOnlyList = IReadOnlyListMethods<IReadOnlyList<byte>>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IReadOnlyList<byte>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000086 RID: 134 RVA: 0x0050FD58 File Offset: 0x0050FD58
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IReadOnlyList<byte>>.Abi_IndexOf_2(thisPtr, MarshalInterface<IReadOnlyList<byte>>.FromAbi(value), out num);
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

    // Token: 0x06000087 RID: 135 RVA: 0x0050FDAC File Offset: 0x0050FDAC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IReadOnlyList<byte>[] array = MarshalInterface<IReadOnlyList<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IReadOnlyList<byte>>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IReadOnlyList<byte>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000088 RID: 136 RVA: 0x0050FE0C File Offset: 0x0050FE0C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IReadOnlyList<byte>>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000013 RID: 19
    private static readonly bool _initialized = IReadOnlyList_System_Collections_Generic_IReadOnlyList_byte_.Init();
  }
}
