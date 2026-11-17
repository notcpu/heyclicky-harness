using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000016 RID: 22
  internal static class IEnumerable_Microsoft_UI_Xaml_FrameworkElement
  {
    // Token: 0x17000015 RID: 21
    // (get) Token: 0x06000091 RID: 145 RVA: 0x0050FFC0 File Offset: 0x0050FFC0
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Xaml_FrameworkElement._initialized;
      }
    }

    // Token: 0x06000092 RID: 146 RVA: 0x0050FFC8 File Offset: 0x0050FFC8
    private static bool Init()
    {
      return IEnumerableMethods<FrameworkElement, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x06000093 RID: 147 RVA: 0x0050FFD8 File Offset: 0x0050FFD8
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<FrameworkElement>>.FromManaged(IEnumerableMethods<FrameworkElement>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000015 RID: 21
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Xaml_FrameworkElement.Init();
  }
}
