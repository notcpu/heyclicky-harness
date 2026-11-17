using System;
using System.Globalization;

namespace Microsoft.Windows.Foundation.UndockedRegFreeWinRTCS
{
  // Token: 0x02000075 RID: 117
  internal class AutoInitialize
  {
    // Token: 0x060002BD RID: 701 RVA: 0x0051B71C File Offset: 0x0051B71C
    internal static void AccessWindowsAppSDK()
    {
      Environment.SetEnvironmentVariable("MICROSOFT_WINDOWSAPPRUNTIME_BASE_DIRECTORY", AppContext.BaseDirectory);
      Environment.SetEnvironmentVariable("MICROSOFT_WINDOWSAPPRUNTIME_BASE_DIRECTORY_PID", Environment.ProcessId.ToString(CultureInfo.InvariantCulture));
      NativeMethods.WindowsAppRuntime_EnsureIsLoaded();
    }
  }
}
