using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000061 RID: 97
  internal static class IReadOnlyList_System_DateTimeOffset
  {
    // Token: 0x17000060 RID: 96
    // (get) Token: 0x0600028B RID: 651 RVA: 0x00515F08 File Offset: 0x00515F08
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_System_DateTimeOffset._initialized;
      }
    }

    // Token: 0x0600028C RID: 652 RVA: 0x00515F10 File Offset: 0x00515F10
    private static bool Init()
    {
      return IReadOnlyListMethods<DateTimeOffset, DateTimeOffset>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600028D RID: 653 RVA: 0x00515F30 File Offset: 0x00515F30
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, DateTimeOffset* __return_value__)
    {
      DateTimeOffset dateTimeOffset = default(DateTimeOffset);
      *__return_value__ = default(DateTimeOffset);
      try
      {
        dateTimeOffset = IReadOnlyListMethods<DateTimeOffset>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = DateTimeOffset.FromManaged(dateTimeOffset);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600028E RID: 654 RVA: 0x00515F84 File Offset: 0x00515F84
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, DateTimeOffset value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<DateTimeOffset>.Abi_IndexOf_2(thisPtr, DateTimeOffset.FromAbi(value), out num);
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

    // Token: 0x0600028F RID: 655 RVA: 0x00515FD8 File Offset: 0x00515FD8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      DateTimeOffset[] array = MarshalNonBlittable<DateTimeOffset>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<DateTimeOffset>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalNonBlittable<DateTimeOffset>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000290 RID: 656 RVA: 0x00516038 File Offset: 0x00516038
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<DateTimeOffset>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000060 RID: 96
    private static readonly bool _initialized = IReadOnlyList_System_DateTimeOffset.Init();
  }
}
