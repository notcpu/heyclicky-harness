using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200004D RID: 77
  internal static class IReadOnlyList_Microsoft_UI_Xaml_DependencyObject
  {
    // Token: 0x1700004C RID: 76
    // (get) Token: 0x0600020A RID: 522 RVA: 0x00514724 File Offset: 0x00514724
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Xaml_DependencyObject._initialized;
      }
    }

    // Token: 0x0600020B RID: 523 RVA: 0x0051472C File Offset: 0x0051472C
    private static bool Init()
    {
      return IReadOnlyListMethods<Microsoft.UI.Xaml.DependencyObject, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600020C RID: 524 RVA: 0x0051474C File Offset: 0x0051474C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.DependencyObject dependencyObject = IReadOnlyListMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ABI.Microsoft.UI.Xaml.DependencyObject.FromManaged(dependencyObject);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600020D RID: 525 RVA: 0x00514794 File Offset: 0x00514794
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_IndexOf_2(thisPtr, ABI.Microsoft.UI.Xaml.DependencyObject.FromAbi(value), out num);
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

    // Token: 0x0600020E RID: 526 RVA: 0x005147E8 File Offset: 0x005147E8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.DependencyObject[] array = ABI.Microsoft.UI.Xaml.DependencyObject.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_GetMany_3(thisPtr, startIndex, ref array);
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

    // Token: 0x0600020F RID: 527 RVA: 0x0051484C File Offset: 0x0051484C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.DependencyObject>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400004C RID: 76
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Xaml_DependencyObject.Init();
  }
}
