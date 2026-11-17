using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002ED RID: 749
  internal sealed class FirstThatFitsHorizontally : Panel
  {
    // Token: 0x0600203D RID: 8253 RVA: 0x00615864 File Offset: 0x00615864
    protected override Size MeasureOverride(Size available)
    {
      int num = this.decision.FirstCandidate(available, base.Children, this.chosen);
      this.chosen = -1;
      for (int i = 0; i < base.Children.Count; i++)
      {
        bool flag = i == base.Children.Count - 1;
        base.Children[i].Measure(flag ? available : new Size(double.PositiveInfinity, available.Height));
        if (this.chosen < 0 && i >= num && (flag || base.Children[i].DesiredSize.Width <= available.Width + 0.5))
        {
          this.chosen = i;
        }
      }
      this.decision.Remember(available, base.Children, this.chosen);
      for (int j = 0; j < base.Children.Count; j++)
      {
        double num2 = j == this.chosen;
        if (base.Children[j].Opacity != num2)
        {
          base.Children[j].Opacity = num2;
        }
        if (base.Children[j].IsHitTestVisible != (j == this.chosen))
        {
          base.Children[j].IsHitTestVisible = j == this.chosen;
        }
      }
      if (this.chosen < 0)
      {
        return new Size(0f, 0f);
      }
      base.Children[this.chosen].Measure(available);
      return base.Children[this.chosen].DesiredSize;
    }

    // Token: 0x0600203E RID: 8254 RVA: 0x00615A1C File Offset: 0x00615A1C
    protected override Size ArrangeOverride(Size final)
    {
      for (int i = 0; i < base.Children.Count; i++)
      {
        base.Children[i].Arrange((i == this.chosen) ? new Rect(0.0, 0.0, final.Width, final.Height) : new Rect(0f, 0f, 0f, 0f));
      }
      return final;
    }

    // Token: 0x04000F9C RID: 3996
    private int chosen = -1;

    // Token: 0x04000F9D RID: 3997
    [Nullable(1)]
    private readonly FitDecision decision = new FitDecision();
  }
}
