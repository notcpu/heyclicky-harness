using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Services;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.UI
{
  // Token: 0x020000AA RID: 170
  public static class DevSignIn
  {
    // Token: 0x17000127 RID: 295
    // (get) Token: 0x0600071B RID: 1819 RVA: 0x00535650 File Offset: 0x00535650
    private static bool Shown
    {
      get
      {
        return Environment.GetEnvironmentVariable("HEYCLICKY_SHOW_DEV_SIGNIN") == "1";
      }
    }

    // Token: 0x0600071C RID: 1820 RVA: 0x00535668 File Offset: 0x00535668
    [NullableContext(1)]
    [return: Nullable(2)]
    public static GelButton Button(IAppActions actions, GelButton.Sizing size = GelButton.Sizing.Normal)
    {
      if (!DevSignIn.Shown || !actions.DevSignInAvailable)
      {
        return null;
      }
      GelButton gelButton = new GelButton("Sign in with local test account", GelAppearance.SecondaryOutline, size, 100.0, null, null, null);
      gelButton.HorizontalAlignment = HorizontalAlignment.Center;
      gelButton.Click += delegate
      {
        actions.DevSignInAsync();
      };
      return gelButton;
    }
  }
}
