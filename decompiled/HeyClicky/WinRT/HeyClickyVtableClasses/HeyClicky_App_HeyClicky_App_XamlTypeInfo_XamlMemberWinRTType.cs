using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Xaml.Markup;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000068 RID: 104
  internal sealed class HeyClicky_App_HeyClicky_App_XamlTypeInfo_XamlMemberWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x0600029F RID: 671 RVA: 0x0051AF48 File Offset: 0x0051AF48
    public unsafe ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IXamlMemberMethods.IID,
          Vtable = IXamlMemberMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
