using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000067 RID: 103
  internal sealed class HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlMetaDataProviderWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x0600029D RID: 669 RVA: 0x0051AF00 File Offset: 0x0051AF00
    public unsafe ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IXamlMetadataProviderMethods.IID,
          Vtable = IXamlMetadataProviderMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
