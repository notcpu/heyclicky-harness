using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000047 RID: 71
  internal static class IReadOnlyList_Microsoft_UI_Composition_IAnimationObject
  {
    // Token: 0x17000046 RID: 70
    // (get) Token: 0x060001EB RID: 491 RVA: 0x0051427C File Offset: 0x0051427C
    internal static bool Initialized
    {
      get
      {
        return IReadOnlyList_Microsoft_UI_Composition_IAnimationObject._initialized;
      }
    }

    // Token: 0x060001EC RID: 492 RVA: 0x00514284 File Offset: 0x00514284
    private static bool Init()
    {
      return IReadOnlyListMethods<IAnimationObject, IntPtr>.InitCcw(ldftn(Do_Abi_GetAt_0), ldftn(Do_Abi_get_Size_1), ldftn(Do_Abi_IndexOf_2), ldftn(Do_Abi_GetMany_3));
    }

    // Token: 0x060001ED RID: 493 RVA: 0x005142A4 File Offset: 0x005142A4
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetAt_0(IntPtr thisPtr, uint index, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        IAnimationObject animationObject = IReadOnlyListMethods<IAnimationObject>.Abi_GetAt_0(thisPtr, index);
        *__return_value__ = MarshalInterface<IAnimationObject>.FromManaged(animationObject);
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x060001EE RID: 494 RVA: 0x005142EC File Offset: 0x005142EC
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_IndexOf_2(IntPtr thisPtr, IntPtr value, uint* index, byte* __return_value__)
    {
      *index = 0U;
      *__return_value__ = 0;
      uint num = 0U;
      try
      {
        bool flag = IReadOnlyListMethods<IAnimationObject>.Abi_IndexOf_2(thisPtr, MarshalInterface<IAnimationObject>.FromAbi(value), out num);
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

    // Token: 0x060001EF RID: 495 RVA: 0x00514340 File Offset: 0x00514340
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_GetMany_3(IntPtr thisPtr, uint startIndex, int __itemsSize, IntPtr items, uint* __return_value__)
    {
      *__return_value__ = 0U;
      IAnimationObject[] array = MarshalInterface<IAnimationObject>.FromAbiArray(new ValueTuple<int, IntPtr>(__itemsSize, items));
      try
      {
        uint num = IReadOnlyListMethods<IAnimationObject>.Abi_GetMany_3(thisPtr, startIndex, ref array);
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

    // Token: 0x060001F0 RID: 496 RVA: 0x005143A0 File Offset: 0x005143A0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_get_Size_1(IntPtr thisPtr, uint* __return_value__)
    {
      *__return_value__ = 0U;
      try
      {
        uint num = IReadOnlyListMethods<IAnimationObject>.Abi_get_Size_1(thisPtr);
        *__return_value__ = num;
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000046 RID: 70
    private static readonly bool _initialized = IReadOnlyList_Microsoft_UI_Composition_IAnimationObject.Init();
  }
}
