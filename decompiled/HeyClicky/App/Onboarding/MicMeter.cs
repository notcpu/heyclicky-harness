using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200012E RID: 302
  public sealed class MicMeter : Grid
  {
    // Token: 0x0600103B RID: 4155 RVA: 0x00583344 File Offset: 0x00583344
    public MicMeter(Color tint)
    {
      this.tint = tint;
      base.Width = 234.0;
      base.Height = 132.0;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      for (int i = 0; i < 16; i++)
      {
        this.bars[i] = new Rectangle
        {
          Width = 9.0,
          Height = 26.0,
          RadiusX = 4.5,
          RadiusY = 4.5,
          Fill = Ui.Brush(DS.White(0.14)),
          VerticalAlignment = VerticalAlignment.Center
        };
        stackPanel.Children.Add(this.bars[i]);
      }
      base.Children.Add(stackPanel);
    }

    // Token: 0x0600103C RID: 4156 RVA: 0x00583448 File Offset: 0x00583448
    public void SetLevel(double level)
    {
      this.display = Math.Max(level, this.display * 0.82);
      double num = Math.Max(0.0, (this.display - 0.14) / 0.86);
      for (int i = 0; i < 16; i++)
      {
        double num2 = Math.Abs((double)i - 7.5) / 7.5;
        bool flag = num > num2 * 0.85;
        this.bars[i].Height = (flag ? (30.0 + num * (24.0 + (1.0 - num2) * 56.0)) : 26.0);
        this.bars[i].Fill = Ui.Brush(flag ? this.tint : DS.White(0.14));
      }
    }

    // Token: 0x0400070E RID: 1806
    [Nullable(1)]
    private readonly Rectangle[] bars = new Rectangle[16];

    // Token: 0x0400070F RID: 1807
    private readonly Color tint;

    // Token: 0x04000710 RID: 1808
    private double display;
  }
}
