using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Services;
using HeyClicky.App.Shell;
using Microsoft.UI;
using Windows.UI;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D8 RID: 216
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SettingsWindow
  {
    // Token: 0x17000197 RID: 407
    // (get) Token: 0x06000A43 RID: 2627 RVA: 0x0055791C File Offset: 0x0055791C
    public PanelWindow Window
    {
      get
      {
        return this.window;
      }
    }

    // Token: 0x17000198 RID: 408
    // (get) Token: 0x06000A44 RID: 2628 RVA: 0x00557924 File Offset: 0x00557924
    public SettingsContent Content
    {
      get
      {
        return this.content;
      }
    }

    // Token: 0x06000A45 RID: 2629 RVA: 0x0055792C File Offset: 0x0055792C
    public SettingsWindow(AppServices services, IAppActions actions, bool excludeFromCapture)
    {
      this.window = new PanelWindow("HeyClicky Settings", excludeFromCapture, true, 14.0, new Color?(Colors.Black), 0.12);
      this.window.SetPanelSize(900.0, 640.0);
      this.content = new SettingsContent(services, actions, delegate
      {
        this.window.HideWindow();
      });
      this.window.Panel.Children.Add(this.content);
    }

    // Token: 0x06000A46 RID: 2630 RVA: 0x005579C0 File Offset: 0x005579C0
    public void Show(SettingsWindow.Page? at = null)
    {
      this.content.Show(at);
      this.window.ShowCentered();
    }

    // Token: 0x040003BD RID: 957
    private readonly PanelWindow window;

    // Token: 0x040003BE RID: 958
    private readonly SettingsContent content;

    // Token: 0x02000545 RID: 1349
    [NullableContext(0)]
    public enum Page
    {
      // Token: 0x0400190E RID: 6414
      Account,
      // Token: 0x0400190F RID: 6415
      General,
      // Token: 0x04001910 RID: 6416
      Referral,
      // Token: 0x04001911 RID: 6417
      Voice,
      // Token: 0x04001912 RID: 6418
      Microphone,
      // Token: 0x04001913 RID: 6419
      Dictation,
      // Token: 0x04001914 RID: 6420
      Shortcuts,
      // Token: 0x04001915 RID: 6421
      Cursor,
      // Token: 0x04001916 RID: 6422
      Agents,
      // Token: 0x04001917 RID: 6423
      Routines,
      // Token: 0x04001918 RID: 6424
      Integrations,
      // Token: 0x04001919 RID: 6425
      Developer
    }
  }
}
