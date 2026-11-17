using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000052 RID: 82
  internal static class IEnumerator_Microsoft_UI_Xaml_DependencyObject
  {
    // Token: 0x17000051 RID: 81
    // (get) Token: 0x06000228 RID: 552 RVA: 0x00514C80 File Offset: 0x00514C80
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Xaml_DependencyObject._initialized;
      }
    }

    // Token: 0x06000229 RID: 553 RVA: 0x00514C88 File Offset: 0x00514C88
    private static bool Init()
    {
      return IEnumeratorMethods<Microsoft.UI.Xaml.DependencyObject, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600022A RID: 554 RVA: 0x00514CA8 File Offset: 0x00514CA8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600022B RID: 555 RVA: 0x00514CEC File Offset: 0x00514CEC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.DependencyObject[] array = ABI.Microsoft.UI.Xaml.DependencyObject.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_GetMany_3(thisPtr, ref array);
        Marshaler<Microsoft.UI.Xaml.DependencyObject>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600022C RID: 556 RVA: 0x00514D4C File Offset: 0x00514D4C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.DependencyObject dependencyObject = IEnumeratorMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_get_Current_0(thisPtr);
        *__return_value__ = ABI.Microsoft.UI.Xaml.DependencyObject.FromManaged(dependencyObject);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600022D RID: 557 RVA: 0x00514D94 File Offset: 0x00514D94
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000051 RID: 81
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Xaml_DependencyObject.Init();
  }
}
