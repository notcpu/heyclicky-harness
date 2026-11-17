using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000007 RID: 7
  internal static class IEnumerable_Microsoft_UI_Xaml_UIElement
  {
    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600001C RID: 28 RVA: 0x0050E804 File Offset: 0x0050E804
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Xaml_UIElement._initialized;
      }
    }

    // Token: 0x0600001D RID: 29 RVA: 0x0050E80C File Offset: 0x0050E80C
    private static bool Init()
    {
      return IEnumerableMethods<UIElement, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x0600001E RID: 30 RVA: 0x0050E81C File Offset: 0x0050E81C
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<UIElement>>.FromManaged(IEnumerableMethods<UIElement>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000006 RID: 6
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Xaml_UIElement.Init();
  }
}
