using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.UI;
using HeyClicky.Core.Browser;
using Windows.UI;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C8 RID: 712
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class UploadApprovalCard
  {
    // Token: 0x06001E0B RID: 7691 RVA: 0x00605A74 File Offset: 0x00605A74
    public void Ask(IReadOnlyList<string> fileNames, [Nullable(2)] string site, string jobTitle, Color accent, Action saveAlways, TaskCompletionSource<UploadApprovalAnswer> answer)
    {
      if (this.isAsking || DriveNotchOwnership.OwnsNotch)
      {
        answer.TrySetResult(UploadApprovalAnswer.Busy);
        return;
      }
      this.isAsking = true;
      string text = BrowserUploads.ApprovalQuestion(fileNames, site);
      TakeOverConsentCard takeOverConsentCard = this.card;
      string text2 = text;
      string text3 = text;
      Action action = delegate
      {
        base.<Ask>g__Finish|0(UploadApprovalAnswer.Allowed);
      };
      Action action2 = delegate
      {
        base.<Ask>g__Finish|0(UploadApprovalAnswer.Declined);
      };
      Action<string, double> action3;
      if ((action3 = UploadApprovalCard.<>O.<0>__Play) == null)
      {
        action3 = (UploadApprovalCard.<>O.<0>__Play = new Action<string, double>(Sounds.Play));
      }
      takeOverConsentCard.Present(text2, text3, accent, action, action2, action3, delegate
      {
        base.<Ask>g__Finish|0(UploadApprovalAnswer.Unanswered);
      }, BrowserUploads.ApprovalNote(jobTitle), "Approve this time", true, 120, "Always approve", delegate
      {
        saveAlways();
        base.<Ask>g__Finish|0(UploadApprovalAnswer.Allowed);
      });
    }

    // Token: 0x04000E76 RID: 3702
    private readonly TakeOverConsentCard card = new TakeOverConsentCard();

    // Token: 0x04000E77 RID: 3703
    private bool isAsking;

    // Token: 0x04000E78 RID: 3704
    private const int TimeoutSeconds = 120;

    // Token: 0x020008EA RID: 2282
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040025A3 RID: 9635
      [Nullable(new byte[] { 0, 1 })]
      public static Action<string, double> <0>__Play;
    }
  }
}
