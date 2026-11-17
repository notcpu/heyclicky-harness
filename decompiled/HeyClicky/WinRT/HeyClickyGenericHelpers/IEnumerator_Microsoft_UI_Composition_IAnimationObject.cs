using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000038 RID: 56
  internal static class IEnumerator_Microsoft_UI_Composition_IAnimationObject
  {
    // Token: 0x17000037 RID: 55
    // (get) Token: 0x06000183 RID: 387 RVA: 0x00512EA4 File Offset: 0x00512EA4
    internal static bool Initialized
    {
      get
      {
        return IEnumerator_Microsoft_UI_Composition_IAnimationObject._initialized;
      }
    }

    // Token: 0x06000184 RID: 388 RVA: 0x00512EAC File Offset: 0x00512EAC
    private static bool Init()
    {
      return IEnumeratorMethods<IAnimationObject, IntPtr>.InitCcw(ldftn(Do_Abi_get_Current_0), ldftn(Do_Abi_get_HasCurrent_1), ldftn(Do_Abi_MoveNext_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x06000185 RID: 389 RVA: 0x00512ECC File Offset: 0x00512ECC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_MoveNext_2(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IAnimationObject>.Abi_MoveNext_2(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000186 RID: 390 RVA: 0x00512F10 File Offset: 0x00512F10
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IAnimationObject[] array = MarshalInterface<IAnimationObject>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IEnumeratorMethods<IAnimationObject>.Abi_GetMany_3(thisPtr, ref array);
        MarshalInterface<IAnimationObject>.CopyManagedArray(array, items);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000187 RID: 391 RVA: 0x00512F6C File Offset: 0x00512F6C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Current_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IAnimationObject animationObject = IEnumeratorMethods<IAnimationObject>.Abi_get_Current_0(thisPtr);
        *__return_value__ = MarshalInterface<IAnimationObject>.FromManaged(animationObject);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x06000188 RID: 392 RVA: 0x00512FB4 File Offset: 0x00512FB4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_HasCurrent_1(IntPtr thisPtr, byte* __return_value__)
    {
      *__return_value__ = 0;
      try
      {
        bool flag = IEnumeratorMethods<IAnimationObject>.Abi_get_HasCurrent_1(thisPtr);
        *__return_value__ = ((flag > false) ? 1 : 0);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000037 RID: 55
    private static readonly bool _initialized = IEnumerator_Microsoft_UI_Composition_IAnimationObject.Init();
  }
}
