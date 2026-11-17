using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000064 RID: 100
  internal sealed class HeyClicky_App_AppWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x06000297 RID: 663 RVA: 0x0051AD48 File Offset: 0x0051AD48
    public unsafe ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IApplicationOverridesMethods.IID,
          Vtable = IApplicationOverridesMethods.AbiToProjectionVftablePtr
        },
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IXamlMetadataProviderMethods.IID,
          Vtable = IXamlMetadataProviderMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
