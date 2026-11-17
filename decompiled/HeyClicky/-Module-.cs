using System;
using Microsoft.Windows.ApplicationModel.WindowsAppRuntime.Common;
using Sentry.Generated;
using WinRT.HeyClickyGenericHelpers;

// Token: 0x02000001 RID: 1
internal class <Module>
{
  // Token: 0x06000001 RID: 1 RVA: 0x0050E3E8 File Offset: 0x0050E3E8
  static <Module>()
  {
    AutoInitialize.InitializeWindowsAppSDK();
    BuildPropertyInitializer.Initialize();
    GlobalVtableLookup.InitializeGlobalVtableLookup();
  }
}
