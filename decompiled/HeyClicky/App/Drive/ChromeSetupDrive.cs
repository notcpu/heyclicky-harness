using System;
using System.Runtime.CompilerServices;
using System.Threading;
using HeyClicky.App.Browser;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Voice;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002BF RID: 703
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ChromeSetupDrive : IChromeClickyDrive
  {
    // Token: 0x06001D49 RID: 7497 RVA: 0x005FE27C File Offset: 0x005FE27C
    public ChromeSetupDrive(ClickyTakeOverController drive, CompanionSession companion)
    {
      this.drive = drive;
      this.companion = companion;
    }

    // Token: 0x17000531 RID: 1329
    // (get) Token: 0x06001D4A RID: 7498 RVA: 0x005FE294 File Offset: 0x005FE294
    public bool IsAvailable
    {
      get
      {
        return this.drive.IsAvailable();
      }
    }

    // Token: 0x17000532 RID: 1330
    // (get) Token: 0x06001D4B RID: 7499 RVA: 0x005FE2A8 File Offset: 0x005FE2A8
    public bool IsDrivingNow
    {
      get
      {
        return this.drive.IsDriving;
      }
    }

    // Token: 0x06001D4C RID: 7500 RVA: 0x005FE2B8 File Offset: 0x005FE2B8
    public bool DriveForApp(string goal, [Nullable(2)] string targetProcessName, Action<ClickyDriveEnding> ended)
    {
      return this.drive.DriveForApp(goal, targetProcessName, delegate(AgentDriveOutcome outcome)
      {
        Action<ClickyDriveEnding> ended2 = ended;
        ClickyDriveEnding clickyDriveEnding;
        switch (outcome.Ending)
        {
        case AgentDriveEnding.Finished:
          clickyDriveEnding = ClickyDriveEnding.Finished;
          break;
        case AgentDriveEnding.UserTookOver:
          clickyDriveEnding = ClickyDriveEnding.UserTookOver;
          break;
        case AgentDriveEnding.SafetyStop:
          clickyDriveEnding = ClickyDriveEnding.SafetyStop;
          break;
        case AgentDriveEnding.CouldNotSee:
          clickyDriveEnding = ClickyDriveEnding.CouldNotSee;
          break;
        case AgentDriveEnding.OutOfAgentMessages:
          clickyDriveEnding = ClickyDriveEnding.OutOfAgentMessages;
          break;
        default:
          clickyDriveEnding = ClickyDriveEnding.Failed;
          break;
        }
        ended2(clickyDriveEnding);
      });
    }

    // Token: 0x06001D4D RID: 7501 RVA: 0x005FE2EC File Offset: 0x005FE2EC
    public bool HandleEscape()
    {
      return this.drive.HandleEscape();
    }

    // Token: 0x06001D4E RID: 7502 RVA: 0x005FE2FC File Offset: 0x005FE2FC
    public void SayVerbatim(string line)
    {
      this.companion.SpeakVerbatimLineAsync(line, default(CancellationToken));
    }

    // Token: 0x04000E00 RID: 3584
    private readonly ClickyTakeOverController drive;

    // Token: 0x04000E01 RID: 3585
    private readonly CompanionSession companion;
  }
}
