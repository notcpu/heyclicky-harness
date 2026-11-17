using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000063 RID: 99
  internal sealed class HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlSystemBaseTypeWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x06000295 RID: 661 RVA: 0x0051AD00 File Offset: 0x0051AD00
    public unsafe ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IXamlTypeMethods.IID,
          Vtable = IXamlTypeMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
