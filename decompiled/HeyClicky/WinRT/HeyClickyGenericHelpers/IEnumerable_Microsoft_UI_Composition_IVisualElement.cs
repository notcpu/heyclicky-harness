using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000054 RID: 84
  internal static class IEnumerable_Microsoft_UI_Composition_IVisualElement
  {
    // Token: 0x17000053 RID: 83
    // (get) Token: 0x06000236 RID: 566 RVA: 0x00514F48 File Offset: 0x00514F48
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Composition_IVisualElement._initialized;
      }
    }

    // Token: 0x06000237 RID: 567 RVA: 0x00514F50 File Offset: 0x00514F50
    private static bool Init()
    {
      return IEnumerableMethods<IVisualElement, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000238 RID: 568 RVA: 0x00514F60 File Offset: 0x00514F60
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IVisualElement>>.FromManaged(IEnumerableMethods<IVisualElement>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000053 RID: 83
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Composition_IVisualElement.Init();
  }
}
