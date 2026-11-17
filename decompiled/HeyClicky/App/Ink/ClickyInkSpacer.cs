using System;
using Microsoft.UI.Xaml;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000260 RID: 608
  public sealed class ClickyInkSpacer : FrameworkElement
  {
    // Token: 0x06001A80 RID: 6784 RVA: 0x005E2BCC File Offset: 0x005E2BCC
    protected override Size MeasureOverride(Size availableSize)
    {
      return new Size(0f, 0f);
    }
  }
}
