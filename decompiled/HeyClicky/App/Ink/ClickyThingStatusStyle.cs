using System;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001DD RID: 477
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyThingStatusStyle
  {
    // Token: 0x060017F7 RID: 6135 RVA: 0x005C8194 File Offset: 0x005C8194
    public static string RawValue(this ClickyThingStatus status)
    {
      string text;
      if (status == ClickyThingStatus.NeedsYou)
      {
        text = "needsYou";
      }
      else
      {
        text = status.ToString().ToLowerInvariant();
      }
      return text;
    }

    // Token: 0x060017F8 RID: 6136 RVA: 0x005C81C0 File Offset: 0x005C81C0
    public static Color Base(this ClickyThingStatus status)
    {
      string text;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        text = "#F5A524";
        break;
      case ClickyThingStatus.Working:
      case ClickyThingStatus.Watching:
        text = "#2F7CF6";
        break;
      case ClickyThingStatus.Ready:
        text = "#1F9D57";
        break;
      default:
        text = "#6B7285";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017F9 RID: 6137 RVA: 0x005C820C File Offset: 0x005C820C
    public static Color StampInk(this ClickyThingStatus status)
    {
      string text;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        text = "#B85A06";
        break;
      case ClickyThingStatus.Working:
      case ClickyThingStatus.Watching:
        text = "#2D5BE3";
        break;
      case ClickyThingStatus.Ready:
        text = "#1F8A4C";
        break;
      default:
        text = "#6B7285";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017FA RID: 6138 RVA: 0x005C8258 File Offset: 0x005C8258
    public static Color OnBlack(this ClickyThingStatus status)
    {
      string text;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        text = "#FFB547";
        break;
      case ClickyThingStatus.Working:
      case ClickyThingStatus.Watching:
        text = "#8EC5FF";
        break;
      case ClickyThingStatus.Ready:
        text = "#5BD08A";
        break;
      default:
        text = "#9093A0";
        break;
      }
      return PaperColor.Hex(text);
    }

    // Token: 0x060017FB RID: 6139 RVA: 0x005C82A4 File Offset: 0x005C82A4
    public static string StampWord(this ClickyThingStatus status)
    {
      string text;
      switch (status)
      {
      case ClickyThingStatus.NeedsYou:
        text = "NEEDS YOU";
        break;
      case ClickyThingStatus.Working:
        text = "WRITING";
        break;
      case ClickyThingStatus.Watching:
        text = "WATCHING";
        break;
      case ClickyThingStatus.Ready:
        text = "READY";
        break;
      case ClickyThingStatus.Done:
        text = "DONE";
        break;
      default:
        text = "PAUSED";
        break;
      }
      return text;
    }
  }
}
