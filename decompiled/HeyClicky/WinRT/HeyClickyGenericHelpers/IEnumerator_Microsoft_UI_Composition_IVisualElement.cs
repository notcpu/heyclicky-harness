using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000033 RID: 51
  internal static class IEnumerator_Microsoft_UI_Composition_IVisualElement
  {
    // Token: 0x17000032 RID: 50
    // (get) Token: 0x0600015E RID: 350 RVA: 0x00512770 File Offset: 0x00512770
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Composition_IVisualElement._initialized;
      }
    }

    // Token: 0x0600015F RID: 351 RVA: 0x00512778 File Offset: 0x00512778
    private static bool Init()
    {
      return IEnumeratorMethods<IVisualElement, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000160 RID: 352 RVA: 0x00512798 File Offset: 0x00512798
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IVisualElement>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000161 RID: 353 RVA: 0x005127DC File Offset: 0x005127DC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IVisualElement[] array = MarshalInterface<IVisualElement>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IVisualElement>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x06000162 RID: 354 RVA: 0x00512838 File Offset: 0x00512838
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IVisualElement visualElement = IEnumeratorMethods<IVisualElement>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IVisualElement>.FromManaged(visualElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000163 RID: 355 RVA: 0x00512880 File Offset: 0x00512880
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IVisualElement>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000032 RID: 50
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Composition_IVisualElement.Init();
  }
}
