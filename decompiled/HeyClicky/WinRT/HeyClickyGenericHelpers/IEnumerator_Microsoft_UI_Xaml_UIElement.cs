using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200001D RID: 29
  internal static class IEnumerator_Microsoft_UI_Xaml_UIElement
  {
    // Token: 0x1700001C RID: 28
    // (get) Token: 0x060000C7 RID: 199 RVA: 0x00510AA8 File Offset: 0x00510AA8
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Xaml_UIElement._initialized;
      }
    }

    // Token: 0x060000C8 RID: 200 RVA: 0x00510AB0 File Offset: 0x00510AB0
    private static bool Init()
    {
      return IEnumeratorMethods<Microsoft.UI.Xaml.UIElement, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000C9 RID: 201 RVA: 0x00510AD0 File Offset: 0x00510AD0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.UIElement>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000CA RID: 202 RVA: 0x00510B14 File Offset: 0x00510B14
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.UIElement[] array = ABI.Microsoft.UI.Xaml.UIElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetMany_3(thisPtr, ref array);
        Marshaler<Microsoft.UI.Xaml.UIElement>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000CB RID: 203 RVA: 0x00510B74 File Offset: 0x00510B74
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.UIElement uielement = IEnumeratorMethods<Microsoft.UI.Xaml.UIElement>.Abi_get_Current_0(thisPtr);
        *__return_value__ = ABI.Microsoft.UI.Xaml.UIElement.FromManaged(uielement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000CC RID: 204 RVA: 0x00510BBC File Offset: 0x00510BBC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<Microsoft.UI.Xaml.UIElement>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400001C RID: 28
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Xaml_UIElement.Init();
  }
}
