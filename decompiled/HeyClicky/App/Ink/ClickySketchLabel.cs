using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001EF RID: 495
  public sealed class ClickySketchLabel : Grid
  {
    // Token: 0x06001844 RID: 6212 RVA: 0x005CA0C4 File Offset: 0x005CA0C4
    [NullableContext(1)]
    public ClickySketchLabel(string text, ClickySketchTone tone = ClickySketchTone.Blue1)
    {
      base.Children.Add(new ClickyHandText(text, 18.0, 1.2, new int?(1), 1.0, TextAlignment.Left).WithForeground(tone.SoftWords()));
    }
  }
}
