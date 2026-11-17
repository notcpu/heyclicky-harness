using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000017 RID: 23
  internal static class IReadOnlyList_Microsoft_UI_Xaml_FrameworkElement
  {
    // Token: 0x17000016 RID: 22
    // (get) Token: 0x06000095 RID: 149 RVA: 0x00510028 File Offset: 0x00510028
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Xaml_FrameworkElement._initialized;
      }
    }

    // Token: 0x06000096 RID: 150 RVA: 0x00510030 File Offset: 0x00510030
    private static bool Init()
    {
      return IReadOnlyListMethods<Microsoft.UI.Xaml.FrameworkElement, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000097 RID: 151 RVA: 0x00510050 File Offset: 0x00510050
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.FrameworkElement frameworkElement = IReadOnlyListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ABI.Microsoft.UI.Xaml.FrameworkElement.FromManaged(frameworkElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000098 RID: 152 RVA: 0x00510098 File Offset: 0x00510098
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_IndexOf_2(thisPtr, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbi(value), out num);
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

    // Token: 0x06000099 RID: 153 RVA: 0x005100EC File Offset: 0x005100EC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.FrameworkElement[] array = ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetMany_3(thisPtr, startIndex, ref array);
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

    // Token: 0x0600009A RID: 154 RVA: 0x00510150 File Offset: 0x00510150
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000016 RID: 22
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Xaml_FrameworkElement.Init();
  }
}
