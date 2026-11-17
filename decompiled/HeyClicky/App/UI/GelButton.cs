using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A8 RID: 168
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GelButton : Pressable
  {
    // Token: 0x06000710 RID: 1808 RVA: 0x00534ED4 File Offset: 0x00534ED4
    public GelButton(string title, GelAppearance appearance = GelAppearance.PrimaryBlue, GelButton.Sizing size = GelButton.Sizing.Normal, double cornerRadius = 15.0, [Nullable(2)] string iconGlyph = null, double? labelFontSize = null, double? minimumHeight = null)
      : base(true)
    {
      this.appearance = appearance;
      double? num = labelFontSize;
      double num2;
      if (num != null)
      {
        num2 = num.GetValueOrDefault();
      }
      else
      {
        double num3;
        if (size != GelButton.Sizing.Large)
        {
          if (size != GelButton.Sizing.Compact)
          {
            num3 = 12.5;
          }
          else
          {
            num3 = 11.0;
          }
        }
        else
        {
          num3 = 14.0;
        }
        num2 = num3;
      }
      double num4 = num2;
      if (labelFontSize != null)
      {
        double valueOrDefault = labelFontSize.GetValueOrDefault();
        num2 = valueOrDefault * 0.93;
      }
      else
      {
        double num3;
        if (size != GelButton.Sizing.Large)
        {
          if (size != GelButton.Sizing.Compact)
          {
            num3 = 10.5;
          }
          else
          {
            num3 = 8.5;
          }
        }
        else
        {
          num3 = 13.0;
        }
        num2 = num3;
      }
      double num5 = num2;
      ValueTuple<int, double> valueTuple;
      if (size != GelButton.Sizing.Large)
      {
        if (size != GelButton.Sizing.Compact)
        {
          valueTuple = new ValueTuple<int, double>(15, 8.0);
        }
        else
        {
          valueTuple = new ValueTuple<int, double>(11, 4.5);
        }
      }
      else
      {
        valueTuple = new ValueTuple<int, double>(22, 12.0);
      }
      ValueTuple<int, double> valueTuple2 = valueTuple;
      double num6 = (double)valueTuple2.Item1;
      double item = valueTuple2.Item2;
      bool flag = appearance == GelAppearance.SecondaryOutline;
      Color color = (flag ? DS.White(0.86) : Colors.Black);
      this.shadow = new ShadowLayer(cornerRadius, DS.Black(flag ? 0.0 : 0.11), 5.0, 4.0, 0.0);
      this.surface = new GelSurface(appearance, cornerRadius);
      this.label = Ui.Text(title, num4, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false);
      this.label.VerticalAlignment = VerticalAlignment.Center;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = (double)(string.IsNullOrEmpty(title) ? 0 : 6),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      if (iconGlyph != null)
      {
        this.icon = Icons.Glyph(iconGlyph, num5, color);
        this.icon.FontWeight = FontWeights.Bold;
        stackPanel.Children.Add(flag ? this.icon : Ui.Embossed(this.icon, 0.8, 1.5, 1.0));
      }
      if (!string.IsNullOrEmpty(title))
      {
        stackPanel.Children.Add(flag ? this.label : Ui.Embossed(this.label, 0.8, 1.5, 1.0));
      }
      this.content = new Grid
      {
        Padding = new Thickness(num6, item, num6, item)
      };
      this.content.Children.Add(stackPanel);
      base.Children.Add(this.shadow);
      base.Children.Add(this.surface);
      base.Children.Add(this.content);
      if (size == GelButton.Sizing.Large)
      {
        this.spinner = new ProgressRing
        {
          Width = 16.0,
          Height = 16.0,
          IsActive = false,
          Visibility = Visibility.Collapsed,
          Foreground = Ui.Brush(DS.Black(0.6)),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
        base.Children.Add(this.spinner);
      }
      base.HoverChanged += delegate(bool h)
      {
        this.surface.Hovered = h;
      };
      base.WithHoverScale(1.03f, 0.15, false);
      base.CornerRadius = new CornerRadius(0.0);
      if (minimumHeight != null)
      {
        double valueOrDefault2 = minimumHeight.GetValueOrDefault();
        base.MinHeight = valueOrDefault2;
      }
    }

    // Token: 0x17000124 RID: 292
    // (get) Token: 0x06000711 RID: 1809 RVA: 0x00535284 File Offset: 0x00535284
    // (set) Token: 0x06000712 RID: 1810 RVA: 0x00535294 File Offset: 0x00535294
    public string Title
    {
      get
      {
        return this.label.Text;
      }
      set
      {
        this.label.Text = value;
      }
    }

    // Token: 0x17000125 RID: 293
    // (get) Token: 0x06000713 RID: 1811 RVA: 0x005352A4 File Offset: 0x005352A4
    // (set) Token: 0x06000714 RID: 1812 RVA: 0x005352AC File Offset: 0x005352AC
    public bool IsEnabledGel
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        this.surface.Enabled = value;
        Color color = ((this.appearance == GelAppearance.SecondaryOutline) ? DS.White(value ? 0.86 : 0.4) : (value ? Colors.Black : DS.Colors.GelDisabledContent));
        this.label.Foreground = Ui.Brush(color);
        if (this.icon != null)
        {
          this.icon.Foreground = Ui.Brush(color);
        }
        TextBlock textBlock = this.icon;
        Grid grid = ((textBlock != null) ? textBlock.Parent : null) as Grid;
        if (grid != null)
        {
          Ui.SetEmboss(grid, value ? 0.8 : 0.18);
        }
        Grid grid2 = this.label.Parent as Grid;
        if (grid2 != null)
        {
          Ui.SetEmboss(grid2, value ? 0.8 : 0.18);
        }
        this.shadow.Opacity = (value ? 1.0 : 0.27);
      }
    }

    // Token: 0x17000126 RID: 294
    // (get) Token: 0x06000715 RID: 1813 RVA: 0x005353B8 File Offset: 0x005353B8
    // (set) Token: 0x06000716 RID: 1814 RVA: 0x005353C0 File Offset: 0x005353C0
    public bool IsLoading
    {
      get
      {
        return this.loading;
      }
      set
      {
        this.loading = value;
        this.content.Opacity = !value;
        if (this.spinner != null)
        {
          this.spinner.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);
          this.spinner.IsActive = value;
        }
      }
    }

    // Token: 0x040001E0 RID: 480
    private readonly GelSurface surface;

    // Token: 0x040001E1 RID: 481
    private readonly TextBlock label;

    // Token: 0x040001E2 RID: 482
    private readonly Grid content;

    // Token: 0x040001E3 RID: 483
    [Nullable(2)]
    private readonly TextBlock icon;

    // Token: 0x040001E4 RID: 484
    private readonly ShadowLayer shadow;

    // Token: 0x040001E5 RID: 485
    private readonly GelAppearance appearance;

    // Token: 0x040001E6 RID: 486
    private bool loading;

    // Token: 0x040001E7 RID: 487
    [Nullable(2)]
    private readonly ProgressRing spinner;

    // Token: 0x02000454 RID: 1108
    [NullableContext(0)]
    public enum Sizing
    {
      // Token: 0x040015B7 RID: 5559
      Normal,
      // Token: 0x040015B8 RID: 5560
      Large,
      // Token: 0x040015B9 RID: 5561
      Compact
    }
  }
}
