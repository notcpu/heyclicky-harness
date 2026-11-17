using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using WinRT;

namespace HeyClicky.App
{
  // Token: 0x02000083 RID: 131
  internal static class XamlGeneratedProgram
  {
    // Token: 0x060004FE RID: 1278 RVA: 0x0052C06C File Offset: 0x0052C06C
    [GeneratedCode("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2609")]
    [DebuggerNonUserCode]
    internal static void XamlGeneratedMain()
    {
      ComWrappersSupport.InitializeComWrappers(null);
      Application.Start(delegate(ApplicationInitializationCallbackParams p)
      {
        SynchronizationContext.SetSynchronizationContext(new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread()));
        App.XamlGeneratedCreateApplicationInstance();
      });
    }
  }
}
