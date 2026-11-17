using System;
using System.Runtime.CompilerServices;
using Microsoft.Windows.Foundation.UndockedRegFreeWinRTCS;

namespace Microsoft.Windows.ApplicationModel.WindowsAppRuntime.Common
{
  // Token: 0x02000073 RID: 115
  internal class AutoInitialize
  {
    // Token: 0x060002BA RID: 698 RVA: 0x0051B70C File Offset: 0x0051B70C
    [ModuleInitializer]
    internal static void InitializeWindowsAppSDK()
    {
      AutoInitialize.AccessWindowsAppSDK();
    }
  }
}
