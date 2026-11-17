using System;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000129 RID: 297
  public static class DrawStepStagePolicy
  {
    // Token: 0x0600102A RID: 4138 RVA: 0x005821FC File Offset: 0x005821FC
    public static bool PanelStepsAside(bool keysHeld, bool narrationStreaming, bool showingDrawing, bool sawThisPressNarrate)
    {
      return !keysHeld && (narrationStreaming || (showingDrawing && sawThisPressNarrate));
    }
  }
}
