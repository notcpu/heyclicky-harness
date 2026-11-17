using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000057 RID: 87
  internal static class IReadOnlyList_bool
  {
    // Token: 0x17000056 RID: 86
    // (get) Token: 0x06000248 RID: 584 RVA: 0x00515284 File Offset: 0x00515284
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_bool._initialized;
      }
    }

    // Token: 0x06000249 RID: 585 RVA: 0x0051528C File Offset: 0x0051528C
    private static bool Init()
    {
      return IReadOnlyListMethods<bool, byte>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600024A RID: 586 RVA: 0x005152AC File Offset: 0x005152AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IReadOnlyListMethods<bool>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600024B RID: 587 RVA: 0x005152F0 File Offset: 0x005152F0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, byte value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<bool>.Abi_IndexOf_2(thisPtr, value > 0, out num);
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

    // Token: 0x0600024C RID: 588 RVA: 0x00515344 File Offset: 0x00515344
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      bool[] array = MarshalNonBlittable<bool>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<bool>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalNonBlittable<bool>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600024D RID: 589 RVA: 0x005153A4 File Offset: 0x005153A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<bool>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000056 RID: 86
    private static readonly bool _initialized = IReadOnlyList_bool.Init();
  }
}
