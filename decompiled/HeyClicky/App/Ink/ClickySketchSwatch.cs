using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001FC RID: 508
  public sealed class ClickySketchSwatch : Pressable
  {
    // Token: 0x06001866 RID: 6246 RVA: 0x005CB430 File Offset: 0x005CB430
    [NullableContext(1)]
    public ClickySketchSwatch(Color color, bool isSelected, Action action, double diameter = 24.0, long seed = 13L, string accessibilityTitle = "")
      : base(true)
    {
      base.Width = (base.Height = diameter + 10.0);
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      double num = diameter + 12.0;
      double num2 = (diameter + 10.0) / num;
      Grid grid = new Grid
      {
        Width = num,
        Height = num,
        RenderTransform = new ScaleTransform
        {
          ScaleX = num2,
          ScaleY = num2
        }
      };
      PaperDrawing paperDrawing = new PaperDrawing();
      double num3 = num / 2.0;
      if (isSelected)
      {
        PaperPath paperPath = new ClickySketchRect((diameter + 9.0) / 2.0, seed + 4L, 0.45).PathIn(new Rect(num3 - (diameter + 9.0) / 2.0, num3 - (diameter + 9.0) / 2.0, diameter + 9.0, diameter + 9.0));
        paperDrawing.Stroke(paperPath, new PaperStroke(PaperColor.Hex("#2C3A68"), 1.6, PenLineCap.Flat, PenLineJoin.Miter, null));
      }
      PaperPath paperPath2 = new ClickySketchRect(diameter / 2.0, seed, 0.35).PathIn(new Rect(num3 - diameter / 2.0, num3 - diameter / 2.0, diameter, diameter));
      paperDrawing.Fill(paperPath2, color);
      paperDrawing.Stroke(paperPath2, new PaperStroke(PaperColor.Black.Opacity(0.25), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      grid.Children.Add(paperDrawing);
      base.Children.Add(grid);
      base.Click += action;
      ClickySketchPressStyle.Apply(this);
      AutomationProperties.SetName(this, accessibilityTitle);
    }
  }
}
