using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002A RID: 42
  internal static class IReadOnlyList_object
  {
    // Token: 0x17000029 RID: 41
    // (get) Token: 0x06000122 RID: 290 RVA: 0x00511BD8 File Offset: 0x00511BD8
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_object._initialized;
      }
    }

    // Token: 0x06000123 RID: 291 RVA: 0x00511BE0 File Offset: 0x00511BE0
    private static bool Init()
    {
      return IReadOnlyListMethods<object, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000124 RID: 292 RVA: 0x00511C00 File Offset: 0x00511C00
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        object obj = IReadOnlyListMethods<object>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInspectable<object>.FromManaged(obj, true);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000125 RID: 293 RVA: 0x00511C48 File Offset: 0x00511C48
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<object>.Abi_IndexOf_2(thisPtr, MarshalInspectable<object>.FromAbi(value), out num);
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

    // Token: 0x06000126 RID: 294 RVA: 0x00511C9C File Offset: 0x00511C9C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      object[] array = MarshalInspectable<object>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<object>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        Marshaler<object>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000127 RID: 295 RVA: 0x00511D00 File Offset: 0x00511D00
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<object>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000029 RID: 41
    private static readonly bool _initialized = IReadOnlyList_object.Init();
  }
}
