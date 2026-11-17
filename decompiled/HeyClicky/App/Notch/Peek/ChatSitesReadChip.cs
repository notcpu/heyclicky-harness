using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Ink;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200017A RID: 378
  public sealed class ChatSitesReadChip : StackPanel
  {
    // Token: 0x060014B3 RID: 5299 RVA: 0x005AF73C File Offset: 0x005AF73C
    [NullableContext(1)]
    public ChatSitesReadChip(IReadOnlyList<string> hosts)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      base.Spacing = 6.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      string text;
      if (hosts.Count != 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
        defaultInterpolatedStringHandler.AppendLiteral("Read ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(hosts.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" sites");
        text = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text = "Read " + hosts[0];
      }
      string text2 = text;
      TextBlock chevron = Icons.Glyph("\ue70d", 8.0, messagesPalette.TertiaryText);
      chevron.FontWeight = FontWeights.Bold;
      chevron.RenderTransformOrigin = new Point(0.5, 0.5);
      chevron.RenderTransform = new RotateTransform();
      TextBlock textBlock = Ui.Text(text2, 11.0, FontWeights.Medium, messagesPalette.SecondaryText, TextAlignment.Left, 1, false, false);
      IEnumerable<string> enumerable = hosts.Take<string>(4);
      Func<string, ClickyLiveLineIcon> func;
      if ((func = ChatSitesReadChip.<>O.<0>__Site) == null)
      {
        func = (ChatSitesReadChip.<>O.<0>__Site = new Func<string, ClickyLiveLineIcon>(ClickyLiveLineIcon.Site));
      }
      LiveLineIconStack liveLineIconStack = new LiveLineIconStack(enumerable.Select<string, ClickyLiveLineIcon>(func).ToList<ClickyLiveLineIcon>(), Math.Max(0, hosts.Count - 4), 15.0, messagesPalette.PrimaryText);
      StackPanel stackPanel = Ui.H(6.0, new UIElement[] { liveLineIconStack, textBlock, chevron });
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      Pressable button = new Pressable(true)
      {
        CornerRadius = new CornerRadius(999.0),
        Padding = new Thickness(5.0, 4.0, 9.0, 4.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        Background = Ui.Brush(Color.FromArgb(17, byte.MaxValue, byte.MaxValue, byte.MaxValue))
      };
      button.Children.Add(stackPanel);
      button.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        button.Background = Ui.Brush(Color.FromArgb(30, byte.MaxValue, byte.MaxValue, byte.MaxValue));
      };
      button.PointerExited += delegate(object _, PointerRoutedEventArgs _)
      {
        button.Background = Ui.Brush(Color.FromArgb(17, byte.MaxValue, byte.MaxValue, byte.MaxValue));
      };
      AutomationProperties.SetName(button, text2);
      base.Children.Add(button);
      WrapPanel list = new WrapPanel
      {
        HorizontalSpacing = 5.0,
        VerticalSpacing = 5.0,
        MaxWidth = 420.0,
        Visibility = Visibility.Collapsed
      };
      foreach (string text3 in hosts)
      {
        FrameworkElement frameworkElement = ClickyInkObjectKit.Favicon("https://" + text3, 12.0, Icons.Glyph("\ue774", 9.0, messagesPalette.TertiaryText));
        TextBlock textBlock2 = Ui.Text(text3, 10.5, FontWeights.Medium, messagesPalette.SecondaryText, TextAlignment.Left, 1, false, false);
        list.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(999.0),
          Padding = new Thickness(7.0, 3.0, 7.0, 3.0),
          Background = Ui.Brush(Color.FromArgb(12, byte.MaxValue, byte.MaxValue, byte.MaxValue)),
          Child = Ui.H(5.0, new UIElement[] { frameworkElement, textBlock2 })
        });
      }
      base.Children.Add(list);
      button.Click += delegate
      {
        bool flag = list.Visibility > Visibility.Visible;
        list.Visibility = (flag ? Visibility.Visible : Visibility.Collapsed);
        ((RotateTransform)chevron.RenderTransform).Angle = (double)(flag ? 180 : 0);
      };
    }

    // Token: 0x0200070C RID: 1804
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001F9D RID: 8093
      public static Func<string, ClickyLiveLineIcon> <0>__Site;
    }
  }
}
