using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000289 RID: 649
  public static class ClickyInkWasValue
  {
    // Token: 0x06001B45 RID: 6981 RVA: 0x005ECDA8 File Offset: 0x005ECDA8
    [NullableContext(1)]
    public static FrameworkElement Make(string value, ClickyInkStyle style)
    {
      InkTextView inkTextView = InkTextView.Plain(value, ClickyPaperFonts.BigNumber(17.0), style.Quiet).With(delegate(InkTextView t)
      {
        t.Strikethrough = new Color?(style.Fresh);
        t.LineLimit = new int?(1);
      });
      inkTextView.Transformed(0.0, 0.0, -6.0, 1.0, null);
      inkTextView.Margin = new Thickness(0.0, 3.0, 0.0, 3.0);
      inkTextView.HorizontalAlignment = HorizontalAlignment.Left;
      return inkTextView;
    }
  }
}
