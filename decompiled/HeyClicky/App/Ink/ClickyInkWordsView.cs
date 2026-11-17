using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A1 RID: 673
  public static class ClickyInkWordsView
  {
    // Token: 0x06001BCD RID: 7117 RVA: 0x005F4FC4 File Offset: 0x005F4FC4
    [NullableContext(1)]
    public static InkTextView Make(ClickyInkLiveNote.Words words, double clock, ClickyInkStyle style, PaperFont font, bool pencil = true, Color? color = null, InkRange? struckOver = null)
    {
      return new InkTextView(ClickyInkWords.Text(words, clock, style, pencil, color, struckOver), font, new Color?(style.Ink));
    }
  }
}
