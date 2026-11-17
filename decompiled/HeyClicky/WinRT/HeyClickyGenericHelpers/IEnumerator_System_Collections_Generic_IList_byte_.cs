using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000043 RID: 67
  internal static class IEnumerator_System_Collections_Generic_IList_byte_
  {
    // Token: 0x17000042 RID: 66
    // (get) Token: 0x060001CF RID: 463 RVA: 0x00513D28 File Offset: 0x00513D28
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_IList_byte_._initialized;
      }
    }

    // Token: 0x060001D0 RID: 464 RVA: 0x00513D30 File Offset: 0x00513D30
    private static bool Init()
    {
      return IEnumeratorMethods<IList<byte>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060001D1 RID: 465 RVA: 0x00513D50 File Offset: 0x00513D50
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IList<byte>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001D2 RID: 466 RVA: 0x00513D94 File Offset: 0x00513D94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IList<byte>[] array = MarshalInterface<IList<byte>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IList<byte>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IList<byte>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001D3 RID: 467 RVA: 0x00513DF0 File Offset: 0x00513DF0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IList<byte> list = IEnumeratorMethods<IList<byte>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IList<byte>>.FromManaged(list);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001D4 RID: 468 RVA: 0x00513E38 File Offset: 0x00513E38
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IList<byte>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000042 RID: 66
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_IList_byte_.Init();
  }
}
