using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Composition;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000004 RID: 4
  internal static class IEnumerable_Microsoft_UI_Composition_IVisualElement2
  {
    // Token: 0x17000003 RID: 3
    // (get) Token: 0x0600000D RID: 13 RVA: 0x0050E5D8 File Offset: 0x0050E5D8
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Composition_IVisualElement2._initialized;
      }
    }

    // Token: 0x0600000E RID: 14 RVA: 0x0050E5E0 File Offset: 0x0050E5E0
    private static bool Init()
    {
      return IEnumerableMethods<IVisualElement2, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600000F RID: 15 RVA: 0x0050E5F0 File Offset: 0x0050E5F0
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<IVisualElement2>>.FromManaged(IEnumerableMethods<IVisualElement2>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000003 RID: 3
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Composition_IVisualElement2.Init();
  }
}
