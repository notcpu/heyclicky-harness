using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ABI.System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace WinRT.HeyClickyGenericHelpers
{
  // Token: 0x02000049 RID: 73
  internal static class IEnumerable_Microsoft_UI_Xaml_DependencyObject
  {
    // Token: 0x17000048 RID: 72
    // (get) Token: 0x060001F9 RID: 505 RVA: 0x0051453C File Offset: 0x0051453C
    internal static bool Initialized
    {
      get
      {
        return IEnumerable_Microsoft_UI_Xaml_DependencyObject._initialized;
      }
    }

    // Token: 0x060001FA RID: 506 RVA: 0x00514544 File Offset: 0x00514544
    private static bool Init()
    {
      return IEnumerableMethods<DependencyObject, IntPtr>.InitCcw(ldftn(Do_Abi_First_0));
    }

    // Token: 0x060001FB RID: 507 RVA: 0x00514554 File Offset: 0x00514554
    [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
    private unsafe static int Do_Abi_First_0(IntPtr thisPtr, IntPtr* __return_value__)
    {
      *__return_value__ = (IntPtr)0;
      try
      {
        *__return_value__ = MarshalInterface<IEnumerator<DependencyObject>>.FromManaged(IEnumerableMethods<DependencyObject>.Abi_First_0(thisPtr));
      }
      catch (Exception ex)
      {
        ExceptionHelpers.SetErrorInfo(ex);
        return ExceptionHelpers.GetHRForException(ex);
      }
      return 0;
    }

    // Token: 0x04000048 RID: 72
    private static readonly bool _initialized = IEnumerable_Microsoft_UI_Xaml_DependencyObject.Init();
  }
}
