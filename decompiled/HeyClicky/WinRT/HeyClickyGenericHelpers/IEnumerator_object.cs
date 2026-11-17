using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000051 RID: 81
  internal static class IEnumerator_object
  {
    // Token: 0x17000050 RID: 80
    // (get) Token: 0x06000221 RID: 545 RVA: 0x00514B1C File Offset: 0x00514B1C
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_object._initialized;
      }
    }

    // Token: 0x06000222 RID: 546 RVA: 0x00514B24 File Offset: 0x00514B24
    private static bool Init()
    {
      return IEnumeratorMethods<object, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000223 RID: 547 RVA: 0x00514B44 File Offset: 0x00514B44
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<object>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000224 RID: 548 RVA: 0x00514B88 File Offset: 0x00514B88
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      object[] array = MarshalInspectable<object>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<object>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x06000225 RID: 549 RVA: 0x00514BE8 File Offset: 0x00514BE8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        object obj = IEnumeratorMethods<object>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInspectable<object>.FromManaged(obj, true);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000226 RID: 550 RVA: 0x00514C30 File Offset: 0x00514C30
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<object>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000050 RID: 80
    private static readonly bool _initialized = IEnumerator_object.Init();
  }
}
