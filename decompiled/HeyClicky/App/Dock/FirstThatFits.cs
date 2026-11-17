using System;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200033A RID: 826
  public sealed class FirstThatFits : Panel
  {
    // Token: 0x06002334 RID: 9012 RVA: 0x0062D8E4 File Offset: 0x0062D8E4
    protected override Size MeasureOverride(Size available)
    {
      int num = this.decision.FirstCandidate(available, base.Children, this.chosen);
      this.chosen = -1;
      for (int i = 0; i < base.Children.Count; i++)
      {
        UIElement uielement = base.Children[i];
        uielement.Measure(new Size(available.Width, double.PositiveInfinity));
        if (this.chosen < 0 && i >= num && uielement.DesiredSize.Height <= available.Height + 0.5)
        {
          this.chosen = i;
        }
      }
      if (this.chosen < 0)
      {
        this.chosen = base.Children.Count - 1;
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
      Size desiredSize = base.Children[this.chosen].DesiredSize;
      return new Size(double.IsInfinity(available.Width) ? desiredSize.Width : available.Width, Math.Min(desiredSize.Height, available.Height));
    }

    // Token: 0x06002335 RID: 9013 RVA: 0x0062DAC4 File Offset: 0x0062DAC4
    protected override Size ArrangeOverride(Size final)
    {
      for (int i = 0; i < base.Children.Count; i++)
      {
        base.Children[i].Arrange((i == this.chosen) ? new Rect(0.0, 0.0, final.Width, final.Height) : new Rect(0f, 0f, 0f, 0f));
      }
      return final;
    }

    // Token: 0x04001131 RID: 4401
    private int chosen = -1;

    // Token: 0x04001132 RID: 4402
    [Nullable(1)]
    private readonly FitDecision decision = new FitDecision();
  }
}
