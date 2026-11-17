using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.Core.Drive
{
  // Token: 0x02000198 RID: 408
  [NullableContext(1)]
  public interface IClickyDriveTurns
  {
    // Token: 0x1700055A RID: 1370
    // (get) Token: 0x06001813 RID: 6163
    bool OffersDrive { get; }

    // Token: 0x1700055B RID: 1371
    // (get) Token: 0x06001814 RID: 6164
    bool IsDriving { get; }

    // Token: 0x06001815 RID: 6165
    bool IsDriveToolName(string name);

    // Token: 0x06001816 RID: 6166
    JsonObject TakeDriveBillingRequestFields();

    // Token: 0x06001817 RID: 6167
    void NoteDriveLeaseId(string leaseId);

    // Token: 0x06001818 RID: 6168
    void NoteSpokenStep(string sentence);

    // Token: 0x06001819 RID: 6169
    [return: Nullable(new byte[] { 1, 2 })]
    Task<ScreenCaptureFrame> PerformComputerActionsAsync(IReadOnlyList<JsonObject> actions, int audioPlaybackBeganBefore);

    // Token: 0x0600181A RID: 6170
    void StopForSafetyCheck(IReadOnlyList<JsonObject> checks);

    // Token: 0x0600181B RID: 6171
    void StopForOutOfAgentMessages();

    // Token: 0x0600181C RID: 6172
    void NoteDriveRequestFailed();

    // Token: 0x1700055C RID: 1372
    // (get) Token: 0x0600181D RID: 6173
    IReadOnlyList<string> ActionLog { get; }

    // Token: 0x0600181E RID: 6174
    [NullableContext(2)]
    void NoteTurnBeforeTools(bool programSpoke, string turnToken);

    // Token: 0x0600181F RID: 6175
    Task<string> HandleToolAsync(string name, string argumentsJson);

    // Token: 0x06001820 RID: 6176
    void VoiceSilentToolTurn(string name, string argumentsJson);

    // Token: 0x06001821 RID: 6177
    void StopBecauseUserSpoke();
  }
}
