using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000055 RID: 85
  internal static class IEnumerator_System_Collections_Generic_KeyValuePair_string__int_
  {
    // Token: 0x17000054 RID: 84
    // (get) Token: 0x0600023A RID: 570 RVA: 0x00514FB0 File Offset: 0x00514FB0
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_System_Collections_Generic_KeyValuePair_string__int_._initialized;
      }
    }

    // Token: 0x0600023B RID: 571 RVA: 0x00514FB8 File Offset: 0x00514FB8
    private static bool Init()
    {
      return IEnumeratorMethods<KeyValuePair<string, int>, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600023C RID: 572 RVA: 0x00514FD8 File Offset: 0x00514FD8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, int>>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600023D RID: 573 RVA: 0x0051501C File Offset: 0x0051501C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      KeyValuePair<string, int>[] array = MarshalInterface<KeyValuePair<string, int>>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<KeyValuePair<string, int>>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<KeyValuePair<string, int>>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600023E RID: 574 RVA: 0x00515078 File Offset: 0x00515078
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      KeyValuePair<string, int> keyValuePair = default(KeyValuePair<string, int>);
      *__return_value__ = (IntPtr)0;
      try
      {
        keyValuePair = IEnumeratorMethods<KeyValuePair<string, int>>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<KeyValuePair<string, int>>.FromManaged(keyValuePair);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600023F RID: 575 RVA: 0x005150C4 File Offset: 0x005150C4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<KeyValuePair<string, int>>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000054 RID: 84
    private static readonly bool _initialized = IEnumerator_System_Collections_Generic_KeyValuePair_string__int_.Init();
  }
}
