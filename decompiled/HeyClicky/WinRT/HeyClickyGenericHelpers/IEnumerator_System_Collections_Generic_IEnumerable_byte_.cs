using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000044 RID: 68
  internal static class IEnumerator_System_Collections_Generic_IEnumerable_byte_
  {
    // Token: 0x17000043 RID: 67
    // (get) Token: 0x060001D6 RID: 470 RVA: 0x00513E88 File Offset: 0x00513E88
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IEnumerable_byte_._initialized;
      }
    }

    // Token: 0x060001D7 RID: 471 RVA: 0x00513E90 File Offset: 0x00513E90
    private static bool Init()
    {
      return IEnumeratorMethods<IEnumerable<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060001D8 RID: 472 RVA: 0x00513EB0 File Offset: 0x00513EB0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<byte>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001D9 RID: 473 RVA: 0x00513EF4 File Offset: 0x00513EF4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IEnumerable<byte>[] array = MarshalInterface<IEnumerable<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IEnumerable<byte>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IEnumerable<byte>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001DA RID: 474 RVA: 0x00513F50 File Offset: 0x00513F50
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IEnumerable<byte> enumerable = IEnumeratorMethods<IEnumerable<byte>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IEnumerable<byte>>.FromManaged(enumerable);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001DB RID: 475 RVA: 0x00513F98 File Offset: 0x00513F98
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IEnumerable<byte>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000043 RID: 67
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IEnumerable_byte_.Init();
  }
}
