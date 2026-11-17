using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F0 RID: 496
  public sealed class ClickySketchTip : Grid
  {
    // Token: 0x06001845 RID: 6213 RVA: 0x005CA118 File Offset: 0x005CA118
    [NullableContext(1)]
    public ClickySketchTip(string text, ClickySketchTone tone = ClickySketchTone.Blue1)
    {
      base.Children.Add(new ClickyHandText(text, 16.0, 1.25, null, 1.0, TextAlignment.Left).WithForeground(tone.SoftWords()));
    }
  }
}
