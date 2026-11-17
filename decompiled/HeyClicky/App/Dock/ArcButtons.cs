using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000325 RID: 805
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArcButtons
  {
    // Token: 0x06002294 RID: 8852 RVA: 0x00625E9C File Offset: 0x00625E9C
    public static GelButton Gel(string title, [Nullable(2)] string symbol, GelAppearance appearance, double? minimumHeight = 32.0, double? labelFontSize = 13.5, GelButton.Sizing size = GelButton.Sizing.Normal, [Nullable(2)] Action click = null)
    {
      GelButton gelButton = new GelButton(title, appearance, size, 100.0, null, labelFontSize, minimumHeight);
      if (symbol != null)
      {
        StackPanel stackPanel = ArcButtons.FindRow(gelButton);
        if (stackPanel != null)
        {
          double num = labelFontSize.GetValueOrDefault(12.5) * 0.93;
          stackPanel.Children.Insert(0, ArcSymbols.Make(symbol, num, (appearance == GelAppearance.SecondaryOutline) ? DS.White(0.86) : Colors.Black, true));
          stackPanel.Spacing = 6.0;
        }
      }
      if (click != null)
      {
        gelButton.Click += click;
      }
      return gelButton;
    }

    // Token: 0x06002295 RID: 8853 RVA: 0x00625F34 File Offset: 0x00625F34
    [return: Nullable(2)]
    private static StackPanel FindRow(Grid button)
    {
      foreach (UIElement uielement in button.Children)
      {
        Grid grid = uielement as Grid;
        if (grid != null && grid.Children.Count > 0)
        {
          StackPanel stackPanel = grid.Children[0] as StackPanel;
          if (stackPanel != null)
          {
            return stackPanel;
          }
        }
      }
      return null;
    }
  }
}
