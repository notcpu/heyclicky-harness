using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000A RID: 10
  internal static class IEnumerator_double
  {
    // Token: 0x17000009 RID: 9
    // (get) Token: 0x0600002E RID: 46 RVA: 0x0050EB2C File Offset: 0x0050EB2C
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_double._initialized;
      }
    }

    // Token: 0x0600002F RID: 47 RVA: 0x0050EB34 File Offset: 0x0050EB34
    private static bool Init()
    {
      return IEnumeratorMethods<double, double>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000030 RID: 48 RVA: 0x0050EB54 File Offset: 0x0050EB54
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<double>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000031 RID: 49 RVA: 0x0050EB98 File Offset: 0x0050EB98
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      double[] array = MarshalBlittable<double>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<double>.Abi_GetMany_3(thisPtr, ref array);
        MarshalBlittable<double>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000032 RID: 50 RVA: 0x0050EBF4 File Offset: 0x0050EBF4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, double* __return_value__)
    {
      *__return_value__ = 0.0;
      try
      {
        double num = IEnumeratorMethods<double>.Abi_get_Current_0(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000033 RID: 51 RVA: 0x0050EC44 File Offset: 0x0050EC44
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<double>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000009 RID: 9
    private static readonly bool _initialized = IEnumerator_double.Init();
  }
}
