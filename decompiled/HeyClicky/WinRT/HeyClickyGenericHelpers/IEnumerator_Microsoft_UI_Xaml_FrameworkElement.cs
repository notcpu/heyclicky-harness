using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000050 RID: 80
  internal static class IEnumerator_Microsoft_UI_Xaml_FrameworkElement
  {
    // Token: 0x1700004F RID: 79
    // (get) Token: 0x0600021A RID: 538 RVA: 0x005149B8 File Offset: 0x005149B8
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Xaml_FrameworkElement._initialized;
      }
    }

    // Token: 0x0600021B RID: 539 RVA: 0x005149C0 File Offset: 0x005149C0
    private static bool Init()
    {
      return IEnumeratorMethods<Microsoft.UI.Xaml.FrameworkElement, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600021C RID: 540 RVA: 0x005149E0 File Offset: 0x005149E0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600021D RID: 541 RVA: 0x00514A24 File Offset: 0x00514A24
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.FrameworkElement[] array = ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetMany_3(thisPtr, ref array);
        Marshaler<Microsoft.UI.Xaml.FrameworkElement>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600021E RID: 542 RVA: 0x00514A84 File Offset: 0x00514A84
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.FrameworkElement frameworkElement = IEnumeratorMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_get_Current_0(thisPtr);
        *__return_value__ = ABI.Microsoft.UI.Xaml.FrameworkElement.FromManaged(frameworkElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600021F RID: 543 RVA: 0x00514ACC File Offset: 0x00514ACC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004F RID: 79
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Xaml_FrameworkElement.Init();
  }
}
