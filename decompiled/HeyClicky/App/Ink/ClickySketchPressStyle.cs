using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F3 RID: 499
  public static class ClickySketchPressStyle
  {
    // Token: 0x06001849 RID: 6217 RVA: 0x005CA228 File Offset: 0x005CA228
    [NullableContext(1)]
    public static Pressable Apply(Pressable pressable)
    {
      Border veil = new Border
      {
        Background = Ui.Brush(PaperColor.Black.Opacity(0.04)),
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      pressable.Children.Add(veil);
      pressable.WithPressScale(0.96f, 0.12, true);
      pressable.PressedChanged += delegate(bool pressed)
      {
        veil.Opacity = pressed > false;
      };
      return pressable;
    }
  }
}
