using System;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001C4 RID: 452
  public sealed class ClickyWritingCaret : Grid
  {
    // Token: 0x060017B7 RID: 6071 RVA: 0x005C4C58 File Offset: 0x005C4C58
    public ClickyWritingCaret(double height = 18.0)
    {
      base.Width = Math.Max(1.6, height / 10.0);
      base.Height = height;
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(base.Width / 2.0),
        Background = Ui.Brush(ClickyPaperInk.ClickyBlue)
      });
      base.IsHitTestVisible = false;
      AutomationProperties.SetAccessibilityView(this, AccessibilityView.Raw);
    }

    // Token: 0x1700043E RID: 1086
    // (get) Token: 0x060017B8 RID: 6072 RVA: 0x005C4CDC File Offset: 0x005C4CDC
    public double Baseline
    {
      get
      {
        return base.Height - base.Height * 0.18;
      }
    }
  }
}
