using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.OneBrain;
using HeyClicky.Core.Notch;

namespace HeyClicky.App
{
  // Token: 0x0200007A RID: 122
  internal static class DrawnOnboardingNotch
  {
    // Token: 0x06000487 RID: 1159 RVA: 0x00529C8C File Offset: 0x00529C8C
    [NullableContext(1)]
    public static void DismissPeek(AppHost host)
    {
      Action foldNotch = DrawnOnboardingNotchShowcase.FoldNotch;
      if (foldNotch != null && DrawnOnboardingNotchShowcase.Shared.IsShowing)
      {
        foldNotch();
      }
      NotchPresenter corePresenter = host.CorePresenter;
      if (corePresenter == null)
      {
        return;
      }
      corePresenter.DismissNotchAgentPeek();
    }
  }
}
