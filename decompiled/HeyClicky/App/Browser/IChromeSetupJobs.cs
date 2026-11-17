using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Browser
{
  // Token: 0x02000360 RID: 864
  [NullableContext(1)]
  public interface IChromeSetupJobs
  {
    // Token: 0x06002580 RID: 9600
    [return: Nullable(2)]
    string JobIdForThread(string threadId);

    // Token: 0x06002581 RID: 9601
    bool IsHeldForChrome(string jobId);

    // Token: 0x06002582 RID: 9602
    IReadOnlyList<string> HeldJobIdsNewestFirst();

    // Token: 0x06002583 RID: 9603
    void SetWaitsForChrome(string jobId, bool waits);

    // Token: 0x06002584 RID: 9604
    void SetSkipsChrome(string jobId);

    // Token: 0x06002585 RID: 9605
    bool SkipsChrome(string jobId);

    // Token: 0x06002586 RID: 9606
    void PresentNeedsYou(string jobId);

    // Token: 0x06002587 RID: 9607
    void StartFromSavedPrompt(string jobId, bool withoutChrome);

    // Token: 0x06002588 RID: 9608
    [return: Nullable(2)]
    string CreatePreviewJob(string title, string prompt);

    // Token: 0x06002589 RID: 9609
    void Dismiss(string jobId);
  }
}
