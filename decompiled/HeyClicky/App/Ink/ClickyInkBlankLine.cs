using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.System;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024F RID: 591
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkBlankLine : Grid
  {
    // Token: 0x06001A1F RID: 6687 RVA: 0x005DD344 File Offset: 0x005DD344
    public ClickyInkBlankLine(string placeholder, Color ink, [Nullable(2)] PaperFont font = null)
    {
      base.Height = 24.0;
      PaperFont paperFont = font ?? ClickyPaperFonts.Typed(13.0, null);
      this.box = new TextBox
      {
        PlaceholderText = placeholder,
        FontFamily = paperFont.Family,
        FontSize = paperFont.Size,
        FontWeight = paperFont.Weight,
        IsTextScaleFactorEnabled = false,
        Foreground = Ui.Brush(ink),
        PlaceholderForeground = Ui.Brush(PaperColor.Black.Opacity(0.25)),
        Padding = new Thickness(0.0),
        MinHeight = 0.0,
        MinWidth = 0.0,
        BorderThickness = new Thickness(0.0),
        Background = Ui.Transparent,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(0.0, 0.0, 0.0, 3.0),
        AcceptsReturn = false,
        TextWrapping = TextWrapping.NoWrap
      };
      ResourceDictionary resources = this.box.Resources;
      foreach (string text in new string[] { "TextControlBackground", "TextControlBackgroundPointerOver", "TextControlBackgroundFocused", "TextControlBackgroundDisabled", "TextControlBorderBrush", "TextControlBorderBrushPointerOver", "TextControlBorderBrushFocused", "TextControlBorderBrushDisabled" })
      {
        resources[text] = Ui.Transparent;
      }
      foreach (string text2 in new string[] { "TextControlForeground", "TextControlForegroundPointerOver", "TextControlForegroundFocused" })
      {
        resources[text2] = Ui.Brush(ink);
      }
      foreach (string text3 in new string[] { "TextControlPlaceholderForeground", "TextControlPlaceholderForegroundPointerOver", "TextControlPlaceholderForegroundFocused" })
      {
        resources[text3] = Ui.Brush(PaperColor.Black.Opacity(0.25));
      }
      resources["TextControlBorderThemeThickness"] = new Thickness(0.0);
      resources["TextControlBorderThemeThicknessFocused"] = new Thickness(0.0);
      resources["TextControlThemePadding"] = new Thickness(0.0);
      resources["TextControlThemeMinHeight"] = 0.0;
      this.box.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        Action<string> textChanged = this.TextChanged;
        if (textChanged == null)
        {
          return;
        }
        textChanged(this.box.Text);
      };
      this.box.KeyDown += delegate(object _, KeyRoutedEventArgs e)
      {
        if (e.Key != VirtualKey.Enter)
        {
          return;
        }
        e.Handled = true;
        Action submitted = this.Submitted;
        if (submitted == null)
        {
          return;
        }
        submitted();
      };
      base.Children.Add(this.box);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkBlankLeader(), new PaperStroke(ink.Opacity(0.4), 1.2, PenLineCap.Round, PenLineJoin.Miter, new double[] { 0.1, 3.5 }));
      clickyShapeView.Height = 1.5;
      clickyShapeView.VerticalAlignment = VerticalAlignment.Bottom;
      base.Children.Add(clickyShapeView);
      AutomationProperties.SetName(this.box, placeholder);
    }

    // Token: 0x17000489 RID: 1161
    // (get) Token: 0x06001A20 RID: 6688 RVA: 0x005DD6C4 File Offset: 0x005DD6C4
    // (set) Token: 0x06001A21 RID: 6689 RVA: 0x005DD6D4 File Offset: 0x005DD6D4
    public string Text
    {
      get
      {
        return this.box.Text;
      }
      set
      {
        this.box.Text = value;
      }
    }

    // Token: 0x06001A22 RID: 6690 RVA: 0x005DD6E4 File Offset: 0x005DD6E4
    protected override Size MeasureOverride(Size availableSize)
    {
      Size size = base.MeasureOverride(availableSize);
      return new Size(double.IsInfinity(availableSize.Width) ? size.Width : availableSize.Width, size.Height);
    }

    // Token: 0x14000065 RID: 101
    // (add) Token: 0x06001A23 RID: 6691 RVA: 0x005DD724 File Offset: 0x005DD724
    // (remove) Token: 0x06001A24 RID: 6692 RVA: 0x005DD75C File Offset: 0x005DD75C
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> TextChanged;

    // Token: 0x14000066 RID: 102
    // (add) Token: 0x06001A25 RID: 6693 RVA: 0x005DD794 File Offset: 0x005DD794
    // (remove) Token: 0x06001A26 RID: 6694 RVA: 0x005DD7CC File Offset: 0x005DD7CC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Submitted;

    // Token: 0x04000C9A RID: 3226
    private readonly TextBox box;
  }
}
