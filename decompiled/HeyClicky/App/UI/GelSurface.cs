using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A7 RID: 167
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class GelSurface : Grid
  {
    // Token: 0x06000703 RID: 1795 RVA: 0x00533F74 File Offset: 0x00533F74
    public GelSurface(GelAppearance appearance, double cornerRadius)
    {
      this.appearance = appearance;
      this.radius = cornerRadius;
      base.IsHitTestVisible = false;
      this.fill = new Border
      {
        CornerRadius = new CornerRadius(cornerRadius),
        BorderThickness = new Thickness(1.0)
      };
      this.shade1 = new Border
      {
        Height = 12.0,
        Margin = new Thickness(4.0, 0.0, 4.0, 2.0),
        VerticalAlignment = VerticalAlignment.Bottom,
        CornerRadius = new CornerRadius(6.0),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, Colors.Transparent),
          new ValueTuple<double, Color>(1.0, DS.Hex(4414087U, 0.26))
        })
      };
      this.shade2 = new Border
      {
        Height = 3.0,
        Margin = new Thickness(10.0, 0.0, 10.0, 3.0),
        VerticalAlignment = VerticalAlignment.Bottom,
        CornerRadius = new CornerRadius(1.5),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, Colors.Transparent),
          new ValueTuple<double, Color>(0.5, DS.White(0.32)),
          new ValueTuple<double, Color>(1.0, Colors.Transparent)
        })
      };
      this.gloss = new Border
      {
        Height = 7.0,
        Margin = new Thickness(12.0, 3.0, 12.0, 0.0),
        VerticalAlignment = VerticalAlignment.Top,
        CornerRadius = new CornerRadius(3.5)
      };
      this.overlays = new Grid();
      this.overlays.Children.Add(this.shade1);
      this.overlays.Children.Add(this.shade2);
      this.overlays.Children.Add(this.gloss);
      this.overlays.Children.Add(this.sheen);
      this.plate.Children.Add(this.fill);
      this.plate.Children.Add(this.overlays);
      base.Children.Add(this.plate);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Layout();
      };
      this.Apply();
    }

    // Token: 0x06000704 RID: 1796 RVA: 0x00534300 File Offset: 0x00534300
    private void ShowSheen()
    {
      Anim.Opacity(this.sheen, (float)((this.hovered && this.enabled && this.ShowsHoverSheen && this.appearance != GelAppearance.SecondaryOutline) ? 1 : 0), 0.083, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x17000120 RID: 288
    // (get) Token: 0x06000705 RID: 1797 RVA: 0x0053435C File Offset: 0x0053435C
    // (set) Token: 0x06000706 RID: 1798 RVA: 0x00534364 File Offset: 0x00534364
    public bool ShowsHoverSheen { get; set; } = true;

    // Token: 0x06000707 RID: 1799 RVA: 0x00534370 File Offset: 0x00534370
    private void Layout()
    {
      double actualWidth = base.ActualWidth;
      double actualHeight = base.ActualHeight;
      this.gloss.Visibility = ((actualWidth > 26.0 && actualHeight > 12.0) ? Visibility.Visible : Visibility.Collapsed);
      this.shade1.Visibility = ((actualHeight >= 16.0 && actualWidth > 8.0) ? Visibility.Visible : Visibility.Collapsed);
      this.shade2.Visibility = ((actualHeight >= 16.0 && actualWidth > 20.0) ? Visibility.Visible : Visibility.Collapsed);
      double num = Math.Min(this.radius, Math.Min(actualWidth, actualHeight) / 2.0);
      this.fill.CornerRadius = new CornerRadius(num);
      Ui.ClipRounded(this.overlays, num);
    }

    // Token: 0x17000121 RID: 289
    // (get) Token: 0x06000708 RID: 1800 RVA: 0x0053443C File Offset: 0x0053443C
    // (set) Token: 0x06000709 RID: 1801 RVA: 0x00534444 File Offset: 0x00534444
    public GelAppearance Appearance
    {
      get
      {
        return this.appearance;
      }
      set
      {
        this.appearance = value;
        this.Apply();
      }
    }

    // Token: 0x17000122 RID: 290
    // (get) Token: 0x0600070A RID: 1802 RVA: 0x00534454 File Offset: 0x00534454
    // (set) Token: 0x0600070B RID: 1803 RVA: 0x0053445C File Offset: 0x0053445C
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        this.enabled = value;
        this.Apply();
        if (this.hovered)
        {
          this.ShowSheen();
        }
      }
    }

    // Token: 0x17000123 RID: 291
    // (get) Token: 0x0600070C RID: 1804 RVA: 0x0053447C File Offset: 0x0053447C
    // (set) Token: 0x0600070D RID: 1805 RVA: 0x00534484 File Offset: 0x00534484
    public bool Hovered
    {
      get
      {
        return this.hovered;
      }
      set
      {
        if (this.hovered == value)
        {
          return;
        }
        this.hovered = value;
        this.Apply();
        this.ShowSheen();
      }
    }

    // Token: 0x0600070E RID: 1806 RVA: 0x005344A4 File Offset: 0x005344A4
    private void Apply()
    {
      switch (this.appearance)
      {
      case GelAppearance.PrimaryBlue:
        this.fill.Background = (this.enabled ? Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, 1.0)),
          new ValueTuple<double, Color>(0.18, DS.Hex(14084095U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, 1.0)),
          new ValueTuple<double, Color>(0.8, DS.Hex(10079224U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, 1.0))
        }) : Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(12110044U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(10466768U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(9415364U, 1.0))
        }));
        this.fill.BorderBrush = Ui.Brush(this.enabled ? DS.Hex(2234520U, 1.0) : DS.Hex(7374755U, 1.0));
        break;
      case GelAppearance.SecondaryWhite:
        this.fill.Background = (this.enabled ? Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(16514043U, 1.0)),
          new ValueTuple<double, Color>(0.42, DS.Hex(15592941U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(15000804U, 1.0))
        }) : Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(13948892U, 1.0)),
          new ValueTuple<double, Color>(0.42, DS.Hex(12962513U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(12305098U, 1.0))
        }));
        this.fill.BorderBrush = Ui.Brush(this.enabled ? DS.Hex(6447714U, 1.0) : DS.Hex(9213087U, 1.0));
        break;
      case GelAppearance.SecondaryOutline:
      {
        double num = (this.enabled ? (this.hovered ? 0.16 : 0.08) : 0.04);
        this.fill.Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(15594751U, num)),
          new ValueTuple<double, Color>(0.18, DS.Hex(14084095U, num)),
          new ValueTuple<double, Color>(0.5, DS.Hex(10668031U, num)),
          new ValueTuple<double, Color>(0.8, DS.Hex(10079224U, num)),
          new ValueTuple<double, Color>(1.0, DS.Hex(10670839U, num))
        });
        this.fill.BorderBrush = (this.enabled ? (this.hovered ? Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.38)),
          new ValueTuple<double, Color>(1.0, DS.White(0.22))
        }) : Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.24)),
          new ValueTuple<double, Color>(1.0, DS.White(0.12))
        })) : Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.12)),
          new ValueTuple<double, Color>(1.0, DS.White(0.06))
        }));
        break;
      }
      case GelAppearance.Mint:
        this.fill.Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(15989491U, 1.0)),
          new ValueTuple<double, Color>(0.18, DS.Hex(14873571U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(14085847U, 1.0)),
          new ValueTuple<double, Color>(0.82, DS.Hex(14085847U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(14348507U, 1.0))
        });
        this.fill.BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.9)),
          new ValueTuple<double, Color>(1.0, DS.Black(0.22))
        });
        break;
      case GelAppearance.Orange:
        this.fill.Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(16573385U, 1.0)),
          new ValueTuple<double, Color>(0.18, DS.Hex(16301441U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(16097355U, 1.0)),
          new ValueTuple<double, Color>(0.82, DS.Hex(16097355U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(16165213U, 1.0))
        });
        this.fill.BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.9)),
          new ValueTuple<double, Color>(1.0, DS.Black(0.22))
        });
        break;
      case GelAppearance.Peach:
        this.fill.Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Hex(16774379U, 1.0)),
          new ValueTuple<double, Color>(0.18, DS.Hex(16769735U, 1.0)),
          new ValueTuple<double, Color>(0.5, DS.Hex(16763289U, 1.0)),
          new ValueTuple<double, Color>(0.82, DS.Hex(16763289U, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Hex(16764835U, 1.0))
        });
        this.fill.BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.9)),
          new ValueTuple<double, Color>(1.0, DS.Black(0.22))
        });
        break;
      }
      bool flag = this.appearance == GelAppearance.SecondaryOutline;
      this.shade1.Opacity = (this.shade2.Opacity = (flag ? 0.0 : (this.enabled ? 1.0 : 0.2)));
      double num2 = (this.enabled ? (this.hovered ? 0.95 : 0.85) : 0.12);
      this.gloss.Background = Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.White(num2)),
        new ValueTuple<double, Color>(1.0, DS.White(0.02))
      });
      this.gloss.Opacity = (flag ? 0.12 : 1.0);
      this.plate.Opacity = (this.enabled ? 1.0 : 0.8);
    }

    // Token: 0x040001D4 RID: 468
    private readonly Border fill;

    // Token: 0x040001D5 RID: 469
    private readonly Border shade1;

    // Token: 0x040001D6 RID: 470
    private readonly Border shade2;

    // Token: 0x040001D7 RID: 471
    private readonly Border gloss;

    // Token: 0x040001D8 RID: 472
    private readonly Grid overlays;

    // Token: 0x040001D9 RID: 473
    private readonly Grid plate = new Grid();

    // Token: 0x040001DA RID: 474
    private GelAppearance appearance;

    // Token: 0x040001DB RID: 475
    private bool enabled = true;

    // Token: 0x040001DC RID: 476
    private bool hovered;

    // Token: 0x040001DD RID: 477
    private readonly double radius;

    // Token: 0x040001DE RID: 478
    private readonly Border sheen = new Border
    {
      IsHitTestVisible = false,
      Opacity = 0.0,
      Background = Ui.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, DS.White(0.3)),
        new ValueTuple<double, Color>(1.0, DS.White(0.12))
      })
    };
  }
}
