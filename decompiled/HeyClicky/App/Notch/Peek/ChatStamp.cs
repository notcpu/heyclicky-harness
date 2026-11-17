using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000178 RID: 376
  public static class ChatStamp
  {
    // Token: 0x060014A5 RID: 5285 RVA: 0x005AE69C File Offset: 0x005AE69C
    public static Color OnBlack(ChatStamp.Status status)
    {
      Color color;
      switch (status)
      {
      case ChatStamp.Status.NeedsYou:
        color = DS.Hex(16758087U, 1.0);
        break;
      case ChatStamp.Status.Working:
        color = DS.Hex(9356799U, 1.0);
        break;
      case ChatStamp.Status.Ready:
        color = DS.Hex(6017162U, 1.0);
        break;
      default:
        color = DS.Hex(9474976U, 1.0);
        break;
      }
      return color;
    }

    // Token: 0x060014A6 RID: 5286 RVA: 0x005AE714 File Offset: 0x005AE714
    [NullableContext(1)]
    public static FrameworkElement Create(ChatStamp.Status status, string word, int seed)
    {
      Color color = ChatStamp.OnBlack(status);
      TextBlock textBlock = new TextBlock
      {
        Text = word.ToUpperInvariant(),
        FontSize = 7.5,
        FontWeight = FontWeights.Black,
        FontFamily = DS.Fonts.Small,
        Foreground = Ui.Brush(color),
        CharacterSpacing = 90,
        IsTextScaleFactorEnabled = false,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      Border box = new Border
      {
        BorderBrush = Ui.Brush(color),
        BorderThickness = new Thickness(1.7),
        CornerRadius = new CornerRadius(3.5),
        Padding = new Thickness(7.0, 3.0, 7.0, 3.0),
        Child = textBlock,
        Opacity = 0.92,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(1.0)
      };
      box.Rotation = (float)(seed % 5 - 6);
      box.Loaded += delegate(object _, RoutedEventArgs _)
      {
        box.CenterPoint = new Vector3((float)box.ActualWidth / 2f, (float)box.ActualHeight / 2f, 0f);
      };
      AutomationProperties.SetName(box, word);
      return box;
    }

    // Token: 0x060014A7 RID: 5287 RVA: 0x005AE868 File Offset: 0x005AE868
    [NullableContext(1)]
    public static int Seed(string key)
    {
      int num = 17;
      foreach (char c in key)
      {
        num = num * 31 + (int)c;
      }
      return Math.Abs(num);
    }

    // Token: 0x02000700 RID: 1792
    public enum Status
    {
      // Token: 0x04001F81 RID: 8065
      NeedsYou,
      // Token: 0x04001F82 RID: 8066
      Working,
      // Token: 0x04001F83 RID: 8067
      Ready,
      // Token: 0x04001F84 RID: 8068
      Paused
    }
  }
}
