using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x0200003E RID: 62
  internal static class IEnumerable_Microsoft_UI_Composition_IAnimationObject
  {
    // Token: 0x1700003D RID: 61
    // (get) Token: 0x060001AA RID: 426 RVA: 0x00513608 File Offset: 0x00513608
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Composition_IAnimationObject._initialized;
      }
    }

    // Token: 0x060001AB RID: 427 RVA: 0x00513610 File Offset: 0x00513610
    private static bool Init()
    {
      return IEnumerableMethods<IAnimationObject, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060001AC RID: 428 RVA: 0x00513620 File Offset: 0x00513620
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IAnimationObject>>.FromManaged(IEnumerableMethods<IAnimationObject>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x0400003D RID: 61
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Composition_IAnimationObject.Init();
  }
}
