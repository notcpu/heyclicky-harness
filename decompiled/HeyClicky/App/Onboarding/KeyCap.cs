using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200012C RID: 300
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class KeyCap : Grid
  {
    // Token: 0x0600102E RID: 4142 RVA: 0x005823B4 File Offset: 0x005823B4
    public KeyCap(string symbolText, string name, KeyCap.Size size, Color accent)
    {
      this.accent = accent;
      ValueTuple<double, double, double, double, double> valueTuple;
      if (size != KeyCap.Size.Regular)
      {
        if (size != KeyCap.Size.Compact)
        {
          valueTuple = new ValueTuple<double, double, double, double, double>(44.0, 14.0, 9.0, 3.0, 8.0);
        }
        else
        {
          valueTuple = new ValueTuple<double, double, double, double, double>(56.0, 18.0, 9.0, 3.0, 14.0);
        }
      }
      else
      {
        valueTuple = new ValueTuple<double, double, double, double, double>(84.0, 26.0, 11.0, 6.0, 14.0);
      }
      ValueTuple<double, double, double, double, double> valueTuple2 = valueTuple;
      double item = valueTuple2.Item1;
      double item2 = valueTuple2.Item2;
      double item3 = valueTuple2.Item3;
      double item4 = valueTuple2.Item4;
      double item5 = valueTuple2.Item5;
      double num = ((symbolText.Length > 2) ? Math.Min(item2, item * 0.62 / ((double)symbolText.Length * 0.58)) : item2);
      base.Width = item;
      base.Height = item;
      this.restShadow = new ShadowLayer(item5, DS.Black(0.35), 4.0, 3.0, 0.0);
      this.pressShadow = new ShadowLayer(item5, accent.WithAlpha(0.35), 10.0, 1.0, 0.0)
      {
        Opacity = 0.0
      };
      this.plate = new Border
      {
        CornerRadius = new CornerRadius(item5),
        BorderThickness = new Thickness(0.5)
      };
      this.symbol = Ui.Text(symbolText, num, FontWeights.SemiBold, DS.Colors.TextPrimary, TextAlignment.Center, 0, false, false);
      TextBlock textBlock = Ui.Text(name, item3, FontWeights.Medium, DS.Colors.TextSecondary, TextAlignment.Center, 0, false, false);
      UIElement uielement = this.symbol;
      if (symbolText == "Win")
      {
        double num2 = Math.Round(item2 * 0.82);
        double num3 = Math.Max(1.0, Math.Round(num2 * 0.08));
        double num4 = (num2 - num3) / 2.0;
        Grid grid = new Grid
        {
          Width = num2,
          Height = num2,
          HorizontalAlignment = HorizontalAlignment.Center,
          Margin = new Thickness(0.0, 2.0, 0.0, 2.0)
        };
        for (int i = 0; i < 2; i++)
        {
          for (int j = 0; j < 2; j++)
          {
            Rectangle rectangle = new Rectangle
            {
              Width = num4,
              Height = num4,
              RadiusX = num4 * 0.12,
              RadiusY = num4 * 0.12,
              HorizontalAlignment = HorizontalAlignment.Left,
              VerticalAlignment = VerticalAlignment.Top,
              Margin = new Thickness((double)j * (num4 + num3), (double)i * (num4 + num3), 0.0, 0.0)
            };
            this.logoTiles.Add(rectangle);
            grid.Children.Add(rectangle);
          }
        }
        AutomationProperties.SetName(grid, "Windows key");
        uielement = grid;
      }
      StackPanel stackPanel = new StackPanel
      {
        Spacing = item4,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { uielement, textBlock }
      };
      base.Children.Add(this.restShadow);
      base.Children.Add(this.pressShadow);
      base.Children.Add(this.plate);
      base.Children.Add(stackPanel);
      base.CenterPoint = new Vector3((float)item / 2f, (float)item / 2f, 0f);
      this.Apply(false, false);
    }

    // Token: 0x170002F2 RID: 754
    // (get) Token: 0x0600102F RID: 4143 RVA: 0x005827DC File Offset: 0x005827DC
    // (set) Token: 0x06001030 RID: 4144 RVA: 0x005827E4 File Offset: 0x005827E4
    public bool Pressed
    {
      get
      {
        return this.pressed;
      }
      set
      {
        if (value != this.pressed)
        {
          this.pressed = value;
          this.Apply(value, true);
        }
      }
    }

    // Token: 0x170002F3 RID: 755
    // (get) Token: 0x06001031 RID: 4145 RVA: 0x00582800 File Offset: 0x00582800
    // (set) Token: 0x06001032 RID: 4146 RVA: 0x00582808 File Offset: 0x00582808
    public int? VirtualKey { get; set; }

    // Token: 0x06001033 RID: 4147 RVA: 0x00582814 File Offset: 0x00582814
    public static List<KeyCap> ForBinding(HotkeyBinding binding, KeyCap.Size size, Color accent)
    {
      List<KeyCap> list = new List<KeyCap>();
      if (binding.Modifiers.HasFlag(HotkeyModifiers.Control))
      {
        list.Add(new KeyCap("Ctrl", "control", size, accent)
        {
          VirtualKey = new int?(17)
        });
      }
      if (binding.Modifiers.HasFlag(HotkeyModifiers.Alt))
      {
        list.Add(new KeyCap("Alt", "alt", size, accent)
        {
          VirtualKey = new int?(18)
        });
      }
      if (binding.Modifiers.HasFlag(HotkeyModifiers.Shift))
      {
        list.Add(new KeyCap("Shift", "shift", size, accent)
        {
          VirtualKey = new int?(16)
        });
      }
      if (binding.Modifiers.HasFlag(HotkeyModifiers.Win))
      {
        list.Add(new KeyCap("Win", "windows", size, accent)
        {
          VirtualKey = new int?(91)
        });
      }
      return list;
    }

    // Token: 0x06001034 RID: 4148 RVA: 0x00582918 File Offset: 0x00582918
    private void Apply(bool down, bool animate)
    {
      this.plate.Background = (down ? Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(1710618U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(2302755U, 1.0))
      }) : Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.Hex(3355443U, 1.0)),
        new ValueTuple<double, Color>(1.0, DS.Hex(2039583U, 1.0))
      }));
      this.plate.BorderBrush = Ui.Brush(down ? this.accent.WithAlpha(0.8) : DS.White(0.14));
      this.plate.BorderThickness = new Thickness(down ? 1.5 : 0.5);
      this.symbol.Foreground = Ui.Brush(down ? this.accent : DS.Colors.TextPrimary);
      foreach (Rectangle rectangle in this.logoTiles)
      {
        rectangle.Fill = Ui.Brush(down ? this.accent : DS.Colors.TextPrimary);
      }
      this.restShadow.Opacity = !down;
      this.pressShadow.Opacity = down > false;
      SwiftSpring swiftSpring = new SwiftSpring(0.18, 0.7);
      if (animate)
      {
        Anim.ScaleSpring(this, down ? 0.94f : 1f, swiftSpring, null);
        Anim.TranslationSpring(this, new Vector3(0f, (float)(down ? 2 : 0), 0f), swiftSpring);
      }
    }

    // Token: 0x04000701 RID: 1793
    private readonly Border plate;

    // Token: 0x04000702 RID: 1794
    private readonly TextBlock symbol;

    // Token: 0x04000703 RID: 1795
    private readonly List<Rectangle> logoTiles = new List<Rectangle>();

    // Token: 0x04000704 RID: 1796
    private readonly ShadowLayer restShadow;

    // Token: 0x04000705 RID: 1797
    private readonly ShadowLayer pressShadow;

    // Token: 0x04000706 RID: 1798
    private readonly Color accent;

    // Token: 0x04000707 RID: 1799
    private bool pressed;

    // Token: 0x04000708 RID: 1800
    public const string WindowsKeySymbol = "Win";

    // Token: 0x02000663 RID: 1635
    [NullableContext(0)]
    public enum Size
    {
      // Token: 0x04001D46 RID: 7494
      Regular,
      // Token: 0x04001D47 RID: 7495
      Compact,
      // Token: 0x04001D48 RID: 7496
      Mini
    }
  }
}
