using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.App.Wiring
{
  // Token: 0x02000098 RID: 152
  [NullableContext(2)]
  [Nullable(0)]
  public static class FlowHooks
  {
    // Token: 0x0400014E RID: 334
    public static volatile Func<string> LiveStateNote;

    // Token: 0x0400014F RID: 335
    public static volatile Func<string> TurnFlow;

    // Token: 0x04000150 RID: 336
    public static volatile Func<string> TourStepNote;

    // Token: 0x04000151 RID: 337
    public static volatile Func<string> NotchNote;

    // Token: 0x04000152 RID: 338
    public static volatile Func<bool> RendersTextInNotchChat;

    // Token: 0x04000153 RID: 339
    public static volatile Func<bool> StepNotchChatAsideForDrawing;

    // Token: 0x04000154 RID: 340
    public static volatile Func<bool, string> EdgeDockFocusNote;

    // Token: 0x04000155 RID: 341
    [Nullable(new byte[] { 2, 1, 1 })]
    public static volatile Func<string, Task<bool>> TakeSpokenWordsDirectly;

    // Token: 0x04000156 RID: 342
    public static volatile Func<bool> SkipInterview;

    // Token: 0x04000157 RID: 343
    public static volatile Action SuppressDocumentAccessNotice;

    // Token: 0x04000158 RID: 344
    public static volatile Func<bool> MutedSpeakerReplyExempt;

    // Token: 0x04000159 RID: 345
    public static volatile Func<JsonObject> TakeFlowBrief;

    // Token: 0x0400015A RID: 346
    public static volatile Func<string> TakePillPlanNote;

    // Token: 0x0400015B RID: 347
    public static volatile Func<bool, string> BrowserStateNote;

    // Token: 0x0400015C RID: 348
    public static volatile Func<bool> IsDrawnTourRunning;

    // Token: 0x0400015D RID: 349
    public static volatile Func<bool> IsInterviewSendOffInProgress;

    // Token: 0x0400015E RID: 350
    public static volatile Func<double> CaptureSettleSeconds;
  }
}
