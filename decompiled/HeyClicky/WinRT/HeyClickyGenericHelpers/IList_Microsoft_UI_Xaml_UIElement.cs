using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200000C RID: 12
  internal static class IList_Microsoft_UI_Xaml_UIElement
  {
    // Token: 0x1700000B RID: 11
    // (get) Token: 0x06000039 RID: 57 RVA: 0x0050ECFC File Offset: 0x0050ECFC
    internal static bool Initialized
    {
      get
      {
        return IList_Microsoft_UI_Xaml_UIElement._initialized;
      }
    }

    // Token: 0x0600003A RID: 58 RVA: 0x0050ED04 File Offset: 0x0050ED04
    private static bool Init()
    {
      return IListMethods<Microsoft.UI.Xaml.UIElement, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_GetView_2), ldftn(Do_Abi_IndexOf_3), ldftn(Do_Abi_SetAt_4), ldftn(Do_Abi_InsertAt_5), ldftn(Do_Abi_RemoveAt_6), ldftn(Do_Abi_Append_7), ldftn(Do_Abi_RemoveAtEnd_8), ldftn(Do_Abi_Clear_9), ldftn(Do_Abi_GetMany_10), ldftn(Do_Abi_ReplaceAll_11));
    }

    // Token: 0x0600003B RID: 59 RVA: 0x0050ED60 File Offset: 0x0050ED60
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        Microsoft.UI.Xaml.UIElement uielement = IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = ABI.Microsoft.UI.Xaml.UIElement.FromManaged(uielement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600003C RID: 60 RVA: 0x0050EDA8 File Offset: 0x0050EDA8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetView_2(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IReadOnlyList<Microsoft.UI.Xaml.UIElement> readOnlyList = IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetView_2(thisPtr);
        *__return_value__ = MarshalInterface<IReadOnlyList<Microsoft.UI.Xaml.UIElement>>.FromManaged(readOnlyList);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600003D RID: 61 RVA: 0x0050EDF0 File Offset: 0x0050EDF0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_3(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_IndexOf_3(thisPtr, ABI.Microsoft.UI.Xaml.UIElement.FromAbi(value), out num);
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

    // Token: 0x0600003E RID: 62 RVA: 0x0050EE44 File Offset: 0x0050EE44
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_SetAt_4(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_SetAt_4(thisPtr, index, ABI.Microsoft.UI.Xaml.UIElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0600003F RID: 63 RVA: 0x0050EE84 File Offset: 0x0050EE84
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_InsertAt_5(IntPtr thisPtr, uint index, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_InsertAt_5(thisPtr, index, ABI.Microsoft.UI.Xaml.UIElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000040 RID: 64 RVA: 0x0050EEC4 File Offset: 0x0050EEC4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAt_6(IntPtr thisPtr, uint index)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_RemoveAt_6(thisPtr, index);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000041 RID: 65 RVA: 0x0050EEFC File Offset: 0x0050EEFC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Append_7(IntPtr thisPtr, IntPtr value)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_Append_7(thisPtr, ABI.Microsoft.UI.Xaml.UIElement.FromAbi(value));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000042 RID: 66 RVA: 0x0050EF38 File Offset: 0x0050EF38
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_RemoveAtEnd_8(IntPtr thisPtr)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_RemoveAtEnd_8(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000043 RID: 67 RVA: 0x0050EF70 File Offset: 0x0050EF70
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_Clear_9(IntPtr thisPtr)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_Clear_9(thisPtr);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000044 RID: 68 RVA: 0x0050EFA8 File Offset: 0x0050EFA8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_10(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      Microsoft.UI.Xaml.UIElement[] array = ABI.Microsoft.UI.Xaml.UIElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_GetMany_10(thisPtr, startIndex, ref array);
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

    // Token: 0x06000045 RID: 69 RVA: 0x0050F00C File Offset: 0x0050F00C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private static int Do_Abi_ReplaceAll_11(IntPtr thisPtr, int __itemsSize, IntPtr items)
    {
      try
      {
        IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_ReplaceAll_11(thisPtr, ABI.Microsoft.UI.Xaml.UIElement.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items)));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000046 RID: 70 RVA: 0x0050F054 File Offset: 0x0050F054
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IListMethods<Microsoft.UI.Xaml.UIElement>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400000B RID: 11
    private static readonly bool _initialized = IList_Microsoft_UI_Xaml_UIElement.Init();
  }
}
