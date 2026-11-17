using System;

namespace HeyClicky.Core.Documents.Markdown
{
  // Token: 0x020001BE RID: 446
  public struct MarkdownViewerTableScrollGesture
  {
    // Token: 0x060019DE RID: 6622 RVA: 0x003D4A44 File Offset: 0x003D4A44
    public MarkdownViewerTableScrollGesture.Axis AxisFor(double deltaX, double deltaY, MarkdownViewerTableScrollGesture.Phase phase, MarkdownViewerTableScrollGesture.Phase momentumPhase)
    {
      if (phase.HasFlag(MarkdownViewerTableScrollGesture.Phase.Began) || phase.HasFlag(MarkdownViewerTableScrollGesture.Phase.MayBegin))
      {
        this.gestureAxis = null;
      }
      MarkdownViewerTableScrollGesture.Axis axis = ((Math.Abs(deltaX) > Math.Abs(deltaY)) ? MarkdownViewerTableScrollGesture.Axis.Horizontal : MarkdownViewerTableScrollGesture.Axis.Vertical);
      if (phase == MarkdownViewerTableScrollGesture.Phase.None && momentumPhase == MarkdownViewerTableScrollGesture.Phase.None)
      {
        return axis;
      }
      MarkdownViewerTableScrollGesture.Axis? axis2 = this.gestureAxis;
      if (axis2 == null && (deltaX != 0.0 || deltaY != 0.0))
      {
        this.gestureAxis = new MarkdownViewerTableScrollGesture.Axis?(axis);
      }
      MarkdownViewerTableScrollGesture.Axis valueOrDefault = this.gestureAxis.GetValueOrDefault(axis);
      if (phase.HasFlag(MarkdownViewerTableScrollGesture.Phase.Cancelled) || momentumPhase.HasFlag(MarkdownViewerTableScrollGesture.Phase.Ended))
      {
        this.gestureAxis = null;
      }
      return valueOrDefault;
    }

    // Token: 0x04000B01 RID: 2817
    private MarkdownViewerTableScrollGesture.Axis? gestureAxis;

    // Token: 0x020005BB RID: 1467
    public enum Axis
    {
      // Token: 0x04002164 RID: 8548
      Horizontal,
      // Token: 0x04002165 RID: 8549
      Vertical
    }

    // Token: 0x020005BC RID: 1468
    [Flags]
    public enum Phase
    {
      // Token: 0x04002167 RID: 8551
      None = 0,
      // Token: 0x04002168 RID: 8552
      Began = 1,
      // Token: 0x04002169 RID: 8553
      Stationary = 2,
      // Token: 0x0400216A RID: 8554
      Changed = 4,
      // Token: 0x0400216B RID: 8555
      Ended = 8,
      // Token: 0x0400216C RID: 8556
      Cancelled = 16,
      // Token: 0x0400216D RID: 8557
      MayBegin = 32
    }
  }
}
