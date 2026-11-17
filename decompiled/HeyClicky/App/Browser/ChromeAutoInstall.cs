using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.Core.Browser;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.App.Browser
{
  // Token: 0x0200035B RID: 859
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChromeAutoInstall
  {
    // Token: 0x170006A4 RID: 1700
    // (get) Token: 0x06002561 RID: 9569 RVA: 0x0063AFEC File Offset: 0x0063AFEC
    // (set) Token: 0x06002562 RID: 9570 RVA: 0x0063AFF4 File Offset: 0x0063AFF4
    public static bool IsRunning { get; private set; }

    // Token: 0x06002563 RID: 9571 RVA: 0x0063AFFC File Offset: 0x0063AFFC
    [NullableContext(2)]
    public static void InstallByDriving([Nullable(1)] ChromeSetupFlow flow, string threadId, bool evenIfConnected, Action<bool, bool> finished = null)
    {
      ChromeAutoInstall.<>c__DisplayClass4_0 CS$<>8__locals1 = new ChromeAutoInstall.<>c__DisplayClass4_0();
      CS$<>8__locals1.flow = flow;
      CS$<>8__locals1.finished = finished;
      CS$<>8__locals1.evenIfConnected = evenIfConnected;
      CS$<>8__locals1.threadId = threadId;
      if (CS$<>8__locals1.finished == null)
      {
        CS$<>8__locals1.finished = delegate(bool connected, bool stoppedByUser)
        {
          if (!connected && !stoppedByUser)
          {
            CS$<>8__locals1.flow.ShowHow(true);
          }
        };
      }
      CS$<>8__locals1.bridge = ClickyBrowserBridge.Shared;
      CS$<>8__locals1.drive = CS$<>8__locals1.flow.Drive;
      if (CS$<>8__locals1.drive != null && CS$<>8__locals1.drive.IsAvailable && !CS$<>8__locals1.drive.IsDrivingNow && !ChromeAutoInstall.IsRunning)
      {
        ValueTuple<string, string>? valueTuple = ChromiumBrowsers.Preferred();
        if (valueTuple != null)
        {
          CS$<>8__locals1.browser = valueTuple.GetValueOrDefault();
          ChromeAutoInstall.IsRunning = true;
          CS$<>8__locals1.swapsOldCopy = CS$<>8__locals1.bridge.HasOldCopyToRemove;
          string text = "chrome_install_drive_started";
          string threadId2 = CS$<>8__locals1.threadId;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["browser"] = CS$<>8__locals1.browser.Item1;
          jsonObject["removes_old_copy"] = CS$<>8__locals1.swapsOldCopy;
          ClickyAnalytics.TrackChromeJobAction(text, threadId2, jsonObject);
          CS$<>8__locals1.process = Path.GetFileNameWithoutExtension(CS$<>8__locals1.browser.Item2);
          CS$<>8__locals1.<InstallByDriving>g__RunAsync|1();
          return;
        }
      }
      CS$<>8__locals1.finished(false, false);
    }

    // Token: 0x06002564 RID: 9572 RVA: 0x0063B13C File Offset: 0x0063B13C
    private static Task EndedAsync([Nullable(2)] string threadId, bool stoppedByUser, Action<bool, bool> finished)
    {
      ClickyBrowserBridge shared = ClickyBrowserBridge.Shared;
      ChromeAutoInstall.IsRunning = false;
      ClickyAnalytics.TrackChromeJobAction(shared.IsConnected ? "chrome_install_drive_connected" : "chrome_install_drive_fell_back", threadId, null);
      finished(shared.IsConnected, stoppedByUser && !shared.IsConnected);
      return Task.CompletedTask;
    }

    // Token: 0x06002565 RID: 9573 RVA: 0x0063B190 File Offset: 0x0063B190
    private static string EndingName(ClickyDriveEnding ending)
    {
      string text;
      switch (ending)
      {
      case ClickyDriveEnding.Finished:
        text = "finished";
        break;
      case ClickyDriveEnding.UserTookOver:
        text = "userTookOver";
        break;
      case ClickyDriveEnding.SafetyStop:
        text = "safetyStop";
        break;
      case ClickyDriveEnding.CouldNotSee:
        text = "couldNotSee";
        break;
      case ClickyDriveEnding.OutOfAgentMessages:
        text = "outOfAgentMessages";
        break;
      default:
        text = "failed";
        break;
      }
      return text;
    }
  }
}
