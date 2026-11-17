using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000053 RID: 83
  internal static class IReadOnlyList_byte
  {
    // Token: 0x17000052 RID: 82
    // (get) Token: 0x0600022F RID: 559 RVA: 0x00514DE4 File Offset: 0x00514DE4
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_byte._initialized;
      }
    }

    // Token: 0x06000230 RID: 560 RVA: 0x00514DEC File Offset: 0x00514DEC
    private static bool Init()
    {
      return IReadOnlyListMethods<byte, byte>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000231 RID: 561 RVA: 0x00514E0C File Offset: 0x00514E0C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        byte b = IReadOnlyListMethods<byte>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = b;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000232 RID: 562 RVA: 0x00514E4C File Offset: 0x00514E4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, byte value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<byte>.Abi_IndexOf_2(thisPtr, value, out num);
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

    // Token: 0x06000233 RID: 563 RVA: 0x00514E9C File Offset: 0x00514E9C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      byte[] array = MarshalBlittable<byte>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<byte>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalBlittable<byte>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000234 RID: 564 RVA: 0x00514EFC File Offset: 0x00514EFC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<byte>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000052 RID: 82
    private static readonly bool _initialized = IReadOnlyList_byte.Init();
  }
}
