using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000019 RID: 25
  internal static class IEnumerator_System_Collections_Generic_IEnumerable_object_
  {
    // Token: 0x17000018 RID: 24
    // (get) Token: 0x060000AB RID: 171 RVA: 0x0051051C File Offset: 0x0051051C
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IEnumerable_object_._initialized;
      }
    }

    // Token: 0x060000AC RID: 172 RVA: 0x00510524 File Offset: 0x00510524
    private static bool Init()
    {
      return IEnumeratorMethods<IEnumerable<object>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000AD RID: 173 RVA: 0x00510544 File Offset: 0x00510544
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<object>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000AE RID: 174 RVA: 0x00510588 File Offset: 0x00510588
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<object>[] array = MarshalInterface<IEnumerable<object>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IEnumerable<object>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IEnumerable<object>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000AF RID: 175 RVA: 0x005105E4 File Offset: 0x005105E4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<object> enumerable = IEnumeratorMethods<IEnumerable<object>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IEnumerable<object>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000B0 RID: 176 RVA: 0x0051062C File Offset: 0x0051062C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<object>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000018 RID: 24
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IEnumerable_object_.Init();
  }
}
