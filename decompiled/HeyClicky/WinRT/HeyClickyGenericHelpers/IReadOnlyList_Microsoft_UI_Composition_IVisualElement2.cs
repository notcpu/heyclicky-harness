using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200002C RID: 44
  internal static class IReadOnlyList_Microsoft_UI_Composition_IVisualElement2
  {
    // Token: 0x1700002B RID: 43
    // (get) Token: 0x0600012D RID: 301 RVA: 0x00511DB4 File Offset: 0x00511DB4
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Composition_IVisualElement2._initialized;
      }
    }

    // Token: 0x0600012E RID: 302 RVA: 0x00511DBC File Offset: 0x00511DBC
    private static bool Init()
    {
      return IReadOnlyListMethods<IVisualElement2, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x0600012F RID: 303 RVA: 0x00511DDC File Offset: 0x00511DDC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IVisualElement2 visualElement = IReadOnlyListMethods<IVisualElement2>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IVisualElement2>.FromManaged(visualElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000130 RID: 304 RVA: 0x00511E24 File Offset: 0x00511E24
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IVisualElement2>.Abi_IndexOf_2(thisPtr, MarshalInterface<IVisualElement2>.FromAbi(value), out num);
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

    // Token: 0x06000131 RID: 305 RVA: 0x00511E78 File Offset: 0x00511E78
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IVisualElement2[] array = MarshalInterface<IVisualElement2>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IVisualElement2>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IVisualElement2>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000132 RID: 306 RVA: 0x00511ED8 File Offset: 0x00511ED8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IVisualElement2>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400002B RID: 43
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Composition_IVisualElement2.Init();
  }
}
