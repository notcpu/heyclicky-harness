using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000012 RID: 18
  internal static class IList_Microsoft_UI_Xaml_FrameworkElement
  {
    // Token: 0x17000011 RID: 17
    // (get) Token: 0x06000065 RID: 101 RVA: 0x0050F598 File Offset: 0x0050F598
    internal static bool Initialized
    {
      get
      {
        return IList_Microsoft_UI_Xaml_FrameworkElement._initialized;
      }
    }

    // Token: 0x06000066 RID: 102 RVA: 0x0050F5A0 File Offset: 0x0050F5A0
    private static bool Init()
    {
      return IListMethods<Microsoft.UI.Xaml.FrameworkElement, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x06000067 RID: 103 RVA: 0x0050F5FC File Offset: 0x0050F5FC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.FrameworkElement frameworkElement = IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ABI.Microsoft.UI.Xaml.FrameworkElement.FromManaged(frameworkElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000068 RID: 104 RVA: 0x0050F644 File Offset: 0x0050F644
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<Microsoft.UI.Xaml.FrameworkElement> readOnlyList = IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<Microsoft.UI.Xaml.FrameworkElement>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000069 RID: 105 RVA: 0x0050F68C File Offset: 0x0050F68C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_IndexOf_3(thisPtr, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbi(value), out num);
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

    // Token: 0x0600006A RID: 106 RVA: 0x0050F6E0 File Offset: 0x0050F6E0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_SetAt_4(thisPtr, index, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600006B RID: 107 RVA: 0x0050F720 File Offset: 0x0050F720
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_InsertAt_5(thisPtr, index, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600006C RID: 108 RVA: 0x0050F760 File Offset: 0x0050F760
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600006D RID: 109 RVA: 0x0050F798 File Offset: 0x0050F798
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_Append_7(thisPtr, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600006E RID: 110 RVA: 0x0050F7D4 File Offset: 0x0050F7D4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600006F RID: 111 RVA: 0x0050F80C File Offset: 0x0050F80C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000070 RID: 112 RVA: 0x0050F844 File Offset: 0x0050F844
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.FrameworkElement[] array = ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_GetMany_10(thisPtr, startIndex, ref array);
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

    // Token: 0x06000071 RID: 113 RVA: 0x0050F8A8 File Offset: 0x0050F8A8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_ReplaceAll_11(thisPtr, ABI.Microsoft.UI.Xaml.FrameworkElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000072 RID: 114 RVA: 0x0050F8F0 File Offset: 0x0050F8F0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<Microsoft.UI.Xaml.FrameworkElement>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000011 RID: 17
    private static readonly bool _initialized = IList_Microsoft_UI_Xaml_FrameworkElement.Init();
  }
}
