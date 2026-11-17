using System;
using System.Runtime.CompilerServices;
using System.Threading;
using HeyClicky.App.DevFakes;
using HeyClicky.App.Services;
using HeyClicky.App.Wiring;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App
{
  // Token: 0x0200007F RID: 127
  [NullableContext(1)]
  [Nullable(0)]
  public static class CompositionRoot
  {
    // Token: 0x060004C9 RID: 1225 RVA: 0x0052B32C File Offset: 0x0052B32C
    [return: TupleElementNames(new string[] { "Services", "Real" })]
    [return: Nullable(new byte[] { 0, 1, 2 })]
    public static ValueTuple<AppServices, RealStack> Build(AppOptions options, DispatcherQueue ui, [Nullable(2)] SynchronizationContext uiContext)
    {
      if (options.UseFakes)
      {
        return new ValueTuple<AppServices, RealStack>(CompositionRoot.BuildFakes(options), null);
      }
      RealStack realStack = RealStack.CreateCore();
      return new ValueTuple<AppServices, RealStack>(new AppServices
      {
        Presenter = realStack.Core.Notch,
        Auth = realStack.Core.Auth,
        Permissions = realStack.Permissions,
        Settings = new CoreAppSettings(realStack.Core.Settings),
        Companion = null,
        Agents = null,
        Microphone = null,
        Audio = null,
        Shortcuts = null,
        Capture = null,
        Cursor = null,
        Highlight = null
      }, realStack);
    }

    // Token: 0x060004CA RID: 1226 RVA: 0x0052B3DC File Offset: 0x0052B3DC
    public static void FillFromRealStack(AppServices services, RealStack real)
    {
      services.Companion = real.Companion;
      services.Agents = real.Agents;
      services.Microphone = real.Microphone;
      services.Audio = real.Audio;
      services.Shortcuts = real.Shortcuts;
      services.Capture = real.Capture;
      services.Cursor = real.Cursor;
      services.Highlight = real.Highlight;
    }

    // Token: 0x060004CB RID: 1227 RVA: 0x0052B44C File Offset: 0x0052B44C
    private static AppServices BuildFakes(AppOptions options)
    {
      string text = AppPaths.Combine(new string[] { "demo" });
      FakeNotchPresenter fakeNotchPresenter = new FakeNotchPresenter();
      return new AppServices
      {
        Presenter = fakeNotchPresenter,
        Companion = new FakeCompanionSession
        {
          Presenter = fakeNotchPresenter
        },
        Agents = new FakeAgentService(),
        Auth = new FakeAuthService(!options.SignedOut),
        Permissions = new FakePermissionService(),
        Microphone = new FakeMicrophone(),
        Audio = new FakeAudioOutput(),
        Shortcuts = new DevShortcutMonitor(),
        Capture = new FakeScreenCapture(),
        Cursor = new FakeCursorOverlay(),
        Highlight = new FakeHighlightOverlay(),
        Settings = new JsonAppSettings(text)
      };
    }
  }
}
