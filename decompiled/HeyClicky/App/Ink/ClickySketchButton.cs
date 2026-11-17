using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001F4 RID: 500
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickySketchButton : Pressable
  {
    // Token: 0x0600184A RID: 6218 RVA: 0x005CA2B4 File Offset: 0x005CA2B4
    public static double ButtonWidth(string title, double fontSize, double horizontalPadding, bool hasImage)
    {
      double num = Math.Floor((double)(PaperText.CharacterCount(title) + (hasImage ? 3 : 0)) * fontSize * 0.46 + 2.0 * horizontalPadding);
      double num2 = ClickyHandLineBreaker.Width(title, ClickyPaperFonts.Hand(fontSize)) + (hasImage ? (fontSize * 1.1) : 0.0);
      return Math.Max(num, Math.Ceiling(num2 + 2.0 * horizontalPadding));
    }

    // Token: 0x0600184B RID: 6219 RVA: 0x005CA32C File Offset: 0x005CA32C
    public ClickySketchButton(string title, Action action, ClickySketchButtonKind kind = ClickySketchButtonKind.Plain, double height = 34.0, double horizontalPadding = 16.0, double fontSize = 18.0, long seed = 4L, [Nullable(2)] string systemImage = null)
      : base(true)
    {
      base.Width = ClickySketchButton.ButtonWidth(title, fontSize, horizontalPadding, systemImage != null);
      base.Height = height;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      this.normalPlate = new ClickySketchShapeView(kind.Fill(), kind.Pencil(), 2.0, seed, 1.2, kind == ClickySketchButtonKind.Teal, false);
      this.hoverPlate = new ClickySketchShapeView(kind.Fill().Opacity(0.75), kind.Pencil(), 2.0, seed, 1.2, kind == ClickySketchButtonKind.Teal, false)
      {
        Opacity = 0.0
      };
      base.Children.Add(this.normalPlate);
      base.Children.Add(this.hoverPlate);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = fontSize * 0.42,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      if (systemImage != null)
      {
        TextBlock textBlock = ClickyInkIcon.Glyph(systemImage, fontSize * 0.62, kind.Words(), true);
        if (textBlock != null)
        {
          stackPanel.Children.Add(textBlock);
        }
      }
      stackPanel.Children.Add(new ClickyHandText(title, fontSize, 1.0, new int?(1), 1.0, TextAlignment.Left)
      {
        Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
      }.WithForeground(kind.Words()));
      base.Children.Add(stackPanel);
      base.HoverChanged += delegate(bool h)
      {
        this.normalPlate.Opacity = !h;
        this.hoverPlate.Opacity = h > false;
      };
      base.Click += action;
      ClickySketchPressStyle.Apply(this);
      AutomationProperties.SetName(this, title);
    }

    // Token: 0x1700044A RID: 1098
    // (get) Token: 0x0600184C RID: 6220 RVA: 0x005CA500 File Offset: 0x005CA500
    // (set) Token: 0x0600184D RID: 6221 RVA: 0x005CA508 File Offset: 0x005CA508
    public bool IsActionEnabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        base.Opacity = (value ? 1.0 : 0.5);
      }
    }

    // Token: 0x04000C02 RID: 3074
    private readonly ClickySketchShapeView normalPlate;

    // Token: 0x04000C03 RID: 3075
    private readonly ClickySketchShapeView hoverPlate;
  }
}
