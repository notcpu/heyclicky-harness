using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001AB RID: 427
  public static class ClickyEdgeStackInkLook
  {
    // Token: 0x060016FA RID: 5882 RVA: 0x005BE4FC File Offset: 0x005BE4FC
    public static Color TapeColor(ClickyEdgeStackInkReading.State state)
    {
      string text;
      switch (state)
      {
      case ClickyEdgeStackInkReading.State.Writing:
        text = "#3A7BF0";
        break;
      case ClickyEdgeStackInkReading.State.NeedsYou:
        text = "#F2A21E";
        break;
      case ClickyEdgeStackInkReading.State.Ready:
        text = "#1F9D57";
        break;
      default:
        text = "#8E93A3";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060016FB RID: 5883 RVA: 0x005BE544 File Offset: 0x005BE544
    [NullableContext(1)]
    public static ClickyInkStyle Style(ClickyInkLiveNote note, ClickyHomeSpreadPaper paper, double clock, ClickyEdgeStackInkReading reading)
    {
      int? num = note.LatestEdit(clock);
      if (num != null)
      {
        int valueOrDefault = num.GetValueOrDefault();
        if (reading.ChangeCounts.IsEmpty && !reading.LeadChanged && !reading.TitleChanged && valueOrDefault - 1 >= 0 && valueOrDefault - 1 < note.EditStarts.Count && !reading.IsPenBusy)
        {
          num = null;
        }
      }
      return new ClickyInkStyle(paper.Ink, paper.Quiet, new Color?(paper.Deep), num).Resting(note, null);
    }

    // Token: 0x04000B40 RID: 2880
    public static readonly Color WaitingAmber = PaperColor.Hex("#8A5300");

    // Token: 0x04000B41 RID: 2881
    public static readonly Color Highlighter = PaperColor.Hex("#FFE27A").Opacity(0.62);
  }
}
