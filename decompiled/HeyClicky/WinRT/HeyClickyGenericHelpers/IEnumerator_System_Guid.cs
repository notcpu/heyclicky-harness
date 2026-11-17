using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003B RID: 59
  internal static class IEnumerator_System_Guid
  {
    // Token: 0x1700003A RID: 58
    // (get) Token: 0x06000195 RID: 405 RVA: 0x005131D0 File Offset: 0x005131D0
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Guid._initialized;
      }
    }

    // Token: 0x06000196 RID: 406 RVA: 0x005131D8 File Offset: 0x005131D8
    private static bool Init()
    {
      return IEnumeratorMethods<Guid, Guid>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000197 RID: 407 RVA: 0x005131F8 File Offset: 0x005131F8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Guid>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000198 RID: 408 RVA: 0x0051323C File Offset: 0x0051323C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Guid[] array = MarshalBlittable<Guid>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<Guid>.Abi_GetMany_3(thisPtr, ref array);
        MarshalBlittable<Guid>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000199 RID: 409 RVA: 0x00513298 File Offset: 0x00513298
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, Guid* __return_value__)
    {
      Guid guid = default(Guid);
      *__return_value__ = default(Guid);
      try
      {
        guid = IEnumeratorMethods<Guid>.Abi_get_Current_0(thisPtr);
        *__return_value__ = guid;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600019A RID: 410 RVA: 0x005132E8 File Offset: 0x005132E8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Guid>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400003A RID: 58
    private static readonly bool _initialized = IEnumerator_System_Guid.Init();
  }
}
