using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002BD RID: 701
  [NullableContext(1)]
  public interface IAgentDriveJobs
  {
    // Token: 0x1700052C RID: 1324
    // (get) Token: 0x06001D30 RID: 7472
    IEnumerable<IAgentDriveJob> Jobs { get; }

    // Token: 0x06001D31 RID: 7473
    [return: Nullable(2)]
    IAgentDriveJob Find(string taskId);

    // Token: 0x06001D32 RID: 7474
    bool IsThreadActive(string threadId);

    // Token: 0x06001D33 RID: 7475
    void SubmitFollowUp(string threadId, string text, [Nullable(2)] string preauthorizedLeaseId, string launchContext);

    // Token: 0x06001D34 RID: 7476
    [return: Nullable(new byte[] { 1, 2 })]
    Task<string> ContinueLeaseAfterConsentAsync(string leaseId);

    // Token: 0x06001D35 RID: 7477
    void CompleteLease(string leaseId, string threadId);

    // Token: 0x06001D36 RID: 7478
    void PresentNeedsYou(IAgentDriveJob job);

    // Token: 0x06001D37 RID: 7479
    void NoteEventForBrain(IAgentDriveJob job, string eventText, string summary);

    // Token: 0x06001D38 RID: 7480
    void AnnounceVerbatim(string line);

    // Token: 0x06001D39 RID: 7481
    bool SkipsChrome(string threadId);

    // Token: 0x1700052D RID: 1325
    // (get) Token: 0x06001D3A RID: 7482
    bool ChromeIsConnected { get; }
  }
}
