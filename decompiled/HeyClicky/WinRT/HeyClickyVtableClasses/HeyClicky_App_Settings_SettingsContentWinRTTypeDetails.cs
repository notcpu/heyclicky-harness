using System;
using System.Runtime.InteropServices;
using ABI.Microsoft.UI.Composition;
using ABI.Microsoft.UI.Xaml;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000065 RID: 101
  internal sealed class HeyClicky_App_Settings_SettingsContentWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x06000299 RID: 665 RVA: 0x0051ADC0 File Offset: 0x0051ADC0
    public unsafe ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IUIElementOverridesMethods.IID,
          Vtable = IUIElementOverridesMethods.AbiToProjectionVftablePtr
        },
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IAnimationObjectMethods.IID,
          Vtable = IAnimationObjectMethods.AbiToProjectionVftablePtr
        },
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IVisualElementMethods.IID,
          Vtable = IVisualElementMethods.AbiToProjectionVftablePtr
        },
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IVisualElement2Methods.IID,
          Vtable = IVisualElement2Methods.AbiToProjectionVftablePtr
        },
        new ComWrappers.ComInterfaceEntry
        {
          IID = *IFrameworkElementOverridesMethods.IID,
          Vtable = IFrameworkElementOverridesMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
