using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000037 RID: 55
  internal static class IEnumerator_System_Collections_Generic_IReadOnlyList_byte_
  {
    // Token: 0x17000036 RID: 54
    // (get) Token: 0x0600017C RID: 380 RVA: 0x00512D44 File Offset: 0x00512D44
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IReadOnlyList_byte_._initialized;
      }
    }

    // Token: 0x0600017D RID: 381 RVA: 0x00512D4C File Offset: 0x00512D4C
    private static bool Init()
    {
      return IEnumeratorMethods<IReadOnlyList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600017E RID: 382 RVA: 0x00512D6C File Offset: 0x00512D6C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IReadOnlyList<byte>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600017F RID: 383 RVA: 0x00512DB0 File Offset: 0x00512DB0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IReadOnlyList<byte>[] array = MarshalInterface<IReadOnlyList<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IReadOnlyList<byte>>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x06000180 RID: 384 RVA: 0x00512E0C File Offset: 0x00512E0C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<byte> readOnlyList = IEnumeratorMethods<IReadOnlyList<byte>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<byte>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000181 RID: 385 RVA: 0x00512E54 File Offset: 0x00512E54
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IReadOnlyList<byte>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000036 RID: 54
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IReadOnlyList_byte_.Init();
  }
}
