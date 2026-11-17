using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000024 RID: 36
  internal static class IReadOnlyList_Microsoft_UI_Composition_IVisualElement
  {
    // Token: 0x17000023 RID: 35
    // (get) Token: 0x060000F5 RID: 245 RVA: 0x005112E8 File Offset: 0x005112E8
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Composition_IVisualElement._initialized;
      }
    }

    // Token: 0x060000F6 RID: 246 RVA: 0x005112F0 File Offset: 0x005112F0
    private static bool Init()
    {
      return IReadOnlyListMethods<IVisualElement, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060000F7 RID: 247 RVA: 0x00511310 File Offset: 0x00511310
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IVisualElement visualElement = IReadOnlyListMethods<IVisualElement>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IVisualElement>.FromManaged(visualElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000F8 RID: 248 RVA: 0x00511358 File Offset: 0x00511358
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IVisualElement>.Abi_IndexOf_2(thisPtr, MarshalInterface<IVisualElement>.FromAbi(value), out num);
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

    // Token: 0x060000F9 RID: 249 RVA: 0x005113AC File Offset: 0x005113AC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IVisualElement[] array = MarshalInterface<IVisualElement>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IVisualElement>.Abi_GetMany_3(thisPtr, startIndex, ref array);
        MarshalInterface<IVisualElement>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060000FA RID: 250 RVA: 0x0051140C File Offset: 0x0051140C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IVisualElement>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000023 RID: 35
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Composition_IVisualElement.Init();
  }
}
