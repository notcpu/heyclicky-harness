using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D7 RID: 471
  public sealed class ClickyPaperProgressBar : Grid
  {
    // Token: 0x060017E5 RID: 6117 RVA: 0x005C7BA4 File Offset: 0x005C7BA4
    public ClickyPaperProgressBar(double fraction, double width = 74.0, double height = 4.0, Color? color = null, Color? track = null)
    {
      Color color2 = color ?? PaperColor.Hex("#2F7CF6");
      base.Width = width;
      base.Height = height;
      base.IsHitTestVisible = false;
      base.Children.Add(new Border
      {
        Background = Ui.Brush(track ?? PaperColor.Hex("#2F7CF6").Opacity(0.18)),
        CornerRadius = new CornerRadius(height / 2.0)
      });
      base.Children.Add(new Border
      {
        Background = Ui.Brush(color2),
        CornerRadius = new CornerRadius(height / 2.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        Width = Math.Max(height, width * Math.Min(1.0, Math.Max(0.0, fraction)))
      });
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)(fraction * 100.0));
      defaultInterpolatedStringHandler.AppendLiteral(" percent");
      AutomationProperties.SetItemStatus(this, defaultInterpolatedStringHandler.ToStringAndClear());
    }
  }
}
