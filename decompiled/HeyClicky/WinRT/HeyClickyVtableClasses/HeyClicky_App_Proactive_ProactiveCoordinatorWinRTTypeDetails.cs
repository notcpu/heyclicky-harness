using System;
using System.Runtime.InteropServices;
using ABI.System;

namespace WinRT.HeyClickyVtableClasses
{
  // Token: 0x02000066 RID: 102
  internal sealed class HeyClicky_App_Proactive_ProactiveCoordinatorWinRTTypeDetails : IWinRTExposedTypeDetails
  {
    // Token: 0x0600029B RID: 667 RVA: 0x0051AEBC File Offset: 0x0051AEBC
    public ComWrappers.ComInterfaceEntry[] GetExposedInterfaces()
    {
      return new ComWrappers.ComInterfaceEntry[]
      {
        new ComWrappers.ComInterfaceEntry
        {
          IID = IDisposableMethods.IID,
          Vtable = IDisposableMethods.AbiToProjectionVftablePtr
        }
      };
    }
  }
}
