using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A0 RID: 672
  public static class ClickyInkLine
  {
    // Token: 0x06001BCC RID: 7116 RVA: 0x005F4F54 File Offset: 0x005F4F54
    [NullableContext(1)]
    public static InkTextView Make(ClickyInkLiveNote.Words words, double clock, ClickyInkStyle style, PaperFont font, Color? color = null, double? goneAt = null, int? lineLimit = null, bool fillsWidth = true)
    {
      bool flag = true;
      InkRange? inkRange;
      if (goneAt != null)
      {
        double valueOrDefault = goneAt.GetValueOrDefault();
        inkRange = new InkRange?(new InkRange(valueOrDefault, valueOrDefault + 0.3));
      }
      else
      {
        inkRange = null;
      }
      return new InkTextView(ClickyInkWords.Text(words, clock, style, flag, color, inkRange), font, new Color?(style.Ink))
      {
        LineLimit = lineLimit,
        FillsWidth = fillsWidth
      };
    }
  }
}
