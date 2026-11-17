using System;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Foundation.UndockedRegFreeWinRTCS
{
  // Token: 0x02000074 RID: 116
  internal static class NativeMethods
  {
    // Token: 0x060002BC RID: 700
    [DllImport("Microsoft.WindowsAppRuntime.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
    internal static extern int WindowsAppRuntime_EnsureIsLoaded();
  }
}
