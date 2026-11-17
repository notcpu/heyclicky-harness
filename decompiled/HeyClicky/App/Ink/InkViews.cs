using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000248 RID: 584
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkViews
  {
    // Token: 0x06001A02 RID: 6658 RVA: 0x005DC214 File Offset: 0x005DC214
    public static void Detach(UIElement element)
    {
      Panel panel;
      if (InkViews.holders.TryGetValue(element, out panel))
      {
        int num = panel.Children.IndexOf(element);
        if (num >= 0)
        {
          panel.Children.RemoveAt(num);
        }
        InkViews.holders.Remove(element);
      }
      FrameworkElement frameworkElement = element as FrameworkElement;
      DependencyObject dependencyObject = ((frameworkElement != null) ? frameworkElement.Parent : null) ?? VisualTreeHelper.GetParent(element);
      Panel panel2 = dependencyObject as Panel;
      if (panel2 == null)
      {
        Border border = dependencyObject as Border;
        if (border == null)
        {
          ContentControl contentControl = dependencyObject as ContentControl;
          if (contentControl == null)
          {
            return;
          }
          if (contentControl.Content == element)
          {
            contentControl.Content = null;
          }
        }
        else if (border.Child == element)
        {
          border.Child = null;
          return;
        }
        return;
      }
      panel2.Children.Remove(element);
    }

    // Token: 0x06001A03 RID: 6659 RVA: 0x005DC2D4 File Offset: 0x005DC2D4
    public static T Hold<[Nullable(0)] T>(Panel panel, T element) where T : UIElement
    {
      InkViews.Detach(element);
      panel.Children.Add(element);
      InkViews.holders.AddOrUpdate(element, panel);
      return element;
    }

    // Token: 0x06001A04 RID: 6660 RVA: 0x005DC304 File Offset: 0x005DC304
    public static T Width<[Nullable(0)] T>(this T e, double width) where T : FrameworkElement
    {
      e.Width = Math.Max(0.0, width);
      e.HorizontalAlignment = HorizontalAlignment.Left;
      return e;
    }

    // Token: 0x06001A05 RID: 6661 RVA: 0x005DC330 File Offset: 0x005DC330
    public static StackPanel VStack(double spacing, [Nullable(new byte[] { 1, 2 })] params UIElement[] children)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = spacing,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
      foreach (UIElement uielement in children)
      {
        if (uielement != null)
        {
          stackPanel.Children.Add(uielement);
        }
      }
      return stackPanel;
    }

    // Token: 0x06001A06 RID: 6662 RVA: 0x005DC378 File Offset: 0x005DC378
    public static InkTextView Words(ClickyInkLiveNote.Words words, double clock, ClickyInkStyle style, PaperFont font, Color? color = null, bool pencil = true, [Nullable(new byte[] { 2, 1 })] Action<InkTextView> configure = null)
    {
      InkTextView inkTextView = ClickyInkWordsView.Make(words, clock, style, font, pencil, color, null);
      if (configure != null)
      {
        inkTextView.With(configure);
      }
      return inkTextView;
    }

    // Token: 0x06001A07 RID: 6663 RVA: 0x005DC3AC File Offset: 0x005DC3AC
    public static InkTextView Plain(string text, PaperFont font, Color color, [Nullable(new byte[] { 2, 1 })] Action<InkTextView> configure = null)
    {
      InkTextView inkTextView = InkTextView.Plain(text, font, color);
      if (configure != null)
      {
        inkTextView.With(configure);
      }
      return inkTextView;
    }

    // Token: 0x06001A08 RID: 6664 RVA: 0x005DC3D0 File Offset: 0x005DC3D0
    public static Pressable PlainButton(UIElement label, Action action, [Nullable(2)] string accessibilityName = null)
    {
      Pressable pressable = new Pressable(true)
      {
        Background = Ui.Transparent,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { label }
      };
      pressable.Click += action;
      if (accessibilityName != null)
      {
        AutomationProperties.SetName(pressable, accessibilityName);
      }
      return pressable;
    }

    // Token: 0x06001A09 RID: 6665 RVA: 0x005DC414 File Offset: 0x005DC414
    public static ClickyPaperGelButton Gel(string title, ClickyPaperGel gel, double height, double fontSize, Action action, double horizontalPadding = 13.0, [Nullable(2)] string systemImage = null, bool isDisabled = false, double? disabledOpacity = null)
    {
      ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(title, action, gel, height, horizontalPadding, fontSize, systemImage);
      if (isDisabled)
      {
        clickyPaperGelButton.IsHitTestVisible = false;
        if (disabledOpacity != null)
        {
          double valueOrDefault = disabledOpacity.GetValueOrDefault();
          clickyPaperGelButton.Opacity = valueOrDefault;
        }
      }
      return clickyPaperGelButton;
    }

    // Token: 0x06001A0A RID: 6666 RVA: 0x005DC458 File Offset: 0x005DC458
    public static FrameworkElement Spinner(double size)
    {
      return new ProgressRing
      {
        Width = size,
        Height = size,
        IsActive = true,
        Foreground = Ui.Brush(PaperColor.Black.Opacity(0.5)),
        VerticalAlignment = VerticalAlignment.Center
      };
    }

    // Token: 0x06001A0B RID: 6667 RVA: 0x005DC4A4 File Offset: 0x005DC4A4
    [return: Nullable(2)]
    public static FrameworkElement Symbol(string name, double size, Color color, bool bold = false)
    {
      TextBlock textBlock = ClickyInkIcon.Glyph(name, size, color, bold);
      if (textBlock != null)
      {
        return textBlock;
      }
      char? c;
      if (name != null)
      {
        int length = name.Length;
        switch (length)
        {
        case 8:
          if (!(name == "waveform"))
          {
            goto IL_0127;
          }
          c = new char?('\ue8d6');
          goto IL_0132;
        case 9:
          if (!(name == "film.fill"))
          {
            goto IL_0127;
          }
          c = new char?('\ue714');
          goto IL_0132;
        case 10:
          if (!(name == "doc.zipper"))
          {
            goto IL_0127;
          }
          c = new char?('\uf012');
          goto IL_0132;
        case 11:
          if (!(name == "curlybraces"))
          {
            goto IL_0127;
          }
          break;
        case 12:
        case 13:
          goto IL_0127;
        case 14:
        {
          char c2 = name[0];
          if (c2 != 'a')
          {
            if (c2 != 't')
            {
              goto IL_0127;
            }
            if (!(name == "text.alignleft"))
            {
              goto IL_0127;
            }
            c = new char?('\ue8e4');
            goto IL_0132;
          }
          else
          {
            if (!(name == "app.badge.fill"))
            {
              goto IL_0127;
            }
            c = new char?('\ue71d');
            goto IL_0132;
          }
          break;
        }
        default:
          if (length != 39)
          {
            goto IL_0127;
          }
          if (!(name == "chevron.left.forwardslash.chevron.right"))
          {
            goto IL_0127;
          }
          break;
        }
        c = new char?('\ue943');
        goto IL_0132;
      }
      IL_0127:
      c = null;
      IL_0132:
      char? c3 = c;
      if (c3 != null)
      {
        char valueOrDefault = c3.GetValueOrDefault();
        return new TextBlock
        {
          Text = valueOrDefault.ToString(),
          FontFamily = DS.Fonts.Symbols,
          FontSize = size,
          FontWeight = (bold ? FontWeights.Bold : FontWeights.Normal),
          Foreground = Ui.Brush(color),
          IsTextScaleFactorEnabled = false,
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Center,
          IsHitTestVisible = false
        };
      }
      return null;
    }

    // Token: 0x04000C8A RID: 3210
    private static readonly ConditionalWeakTable<UIElement, Panel> holders = new ConditionalWeakTable<UIElement, Panel>();
  }
}
