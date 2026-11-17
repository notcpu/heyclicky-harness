using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200005E RID: 94
  internal static class IReadOnlyList_Microsoft_UI_Xaml_UIElement
  {
    // Token: 0x1700005D RID: 93
    // (get) Token: 0x06000279 RID: 633 RVA: 0x00515BB8 File Offset: 0x00515BB8
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Xaml_UIElement._initialized;
      }
    }

    // Token: 0x0600027A RID: 634 RVA: 0x00515BC0 File Offset: 0x00515BC0
    private static bool Init()
    {
      return IReadOnlyListMethods<Microsoft.UI.Xaml.UIElement, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600027B RID: 635 RVA: 0x00515BE0 File Offset: 0x00515BE0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.UIElement uielement = IReadOnlyListMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ABI.Microsoft.UI.Xaml.UIElement.FromManaged(uielement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600027C RID: 636 RVA: 0x00515C28 File Offset: 0x00515C28
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<Microsoft.UI.Xaml.UIElement>.Abi_IndexOf_2(thisPtr, ABI.Microsoft.UI.Xaml.UIElement.FromAbi(value), out num);
        *index = num;
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600027D RID: 637 RVA: 0x00515C7C File Offset: 0x00515C7C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.UIElement[] array = ABI.Microsoft.UI.Xaml.UIElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetMany_3(thisPtr, startIndex, ref array);
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

    // Token: 0x0600027E RID: 638 RVA: 0x00515CE0 File Offset: 0x00515CE0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.UIElement>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400005D RID: 93
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Xaml_UIElement.Init();
  }
}
