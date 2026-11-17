using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000E RID: 14
  internal static class IEnumerator_System_DateTimeOffset
  {
    // Token: 0x1700000D RID: 13
    // (get) Token: 0x0600004C RID: 76 RVA: 0x0050F108 File Offset: 0x0050F108
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_DateTimeOffset._initialized;
      }
    }

    // Token: 0x0600004D RID: 77 RVA: 0x0050F110 File Offset: 0x0050F110
    private static bool Init()
    {
      return IEnumeratorMethods<DateTimeOffset, DateTimeOffset>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600004E RID: 78 RVA: 0x0050F130 File Offset: 0x0050F130
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<DateTimeOffset>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600004F RID: 79 RVA: 0x0050F174 File Offset: 0x0050F174
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      DateTimeOffset[] array = MarshalNonBlittable<DateTimeOffset>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<DateTimeOffset>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x06000050 RID: 80 RVA: 0x0050F1D0 File Offset: 0x0050F1D0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, DateTimeOffset* __return_value__)
    {
      DateTimeOffset dateTimeOffset = default(DateTimeOffset);
      *__return_value__ = default(DateTimeOffset);
      try
      {
        dateTimeOffset = IEnumeratorMethods<DateTimeOffset>.Abi_get_Current_0(thisPtr);
        *__return_value__ = DateTimeOffset.FromManaged(dateTimeOffset);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000051 RID: 81 RVA: 0x0050F224 File Offset: 0x0050F224
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<DateTimeOffset>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000D RID: 13
    private static readonly bool _initialized = IEnumerator_System_DateTimeOffset.Init();
  }
}
