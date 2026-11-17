using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000041 RID: 65
  internal static class IEnumerator_Microsoft_UI_Composition_IVisualElement2
  {
    // Token: 0x17000040 RID: 64
    // (get) Token: 0x060001B9 RID: 441 RVA: 0x00513838 File Offset: 0x00513838
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Composition_IVisualElement2._initialized;
      }
    }

    // Token: 0x060001BA RID: 442 RVA: 0x00513840 File Offset: 0x00513840
    private static bool Init()
    {
      return IEnumeratorMethods<IVisualElement2, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060001BB RID: 443 RVA: 0x00513860 File Offset: 0x00513860
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IVisualElement2>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001BC RID: 444 RVA: 0x005138A4 File Offset: 0x005138A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IVisualElement2[] array = MarshalInterface<IVisualElement2>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IVisualElement2>.Abi_GetMany_3(thisPtr, ref array);
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

    // Token: 0x060001BD RID: 445 RVA: 0x00513900 File Offset: 0x00513900
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IVisualElement2 visualElement = IEnumeratorMethods<IVisualElement2>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IVisualElement2>.FromManaged(visualElement);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001BE RID: 446 RVA: 0x00513948 File Offset: 0x00513948
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IVisualElement2>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000040 RID: 64
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Composition_IVisualElement2.Init();
  }
}
