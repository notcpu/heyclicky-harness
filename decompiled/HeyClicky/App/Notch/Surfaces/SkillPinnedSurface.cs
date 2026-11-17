using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015D RID: 349
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SkillPinnedSurface : NotchSurface
  {
    // Token: 0x06001358 RID: 4952 RVA: 0x005A06AC File Offset: 0x005A06AC
    public SkillPinnedSurface(NotchLiveEvent.SkillPinned e)
    {
      int num = Math.Clamp(e.Count, 1, 3);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = -8.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Center
      };
      for (int i = 0; i < num; i++)
      {
        IReadOnlyList<string> names = e.Names;
        object obj;
        if (names == null || i >= names.Count)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Skill ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
          obj = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          obj = names[i];
        }
        object obj2 = obj;
        FrameworkElement frameworkElement = SkillPinnedSurface.PowerUpAvatar(obj2, obj2, 20.0);
        Canvas.SetZIndex(frameworkElement, num - i);
        stackPanel.Children.Add(frameworkElement);
      }
      this.view = new Grid
      {
        Margin = new Thickness(12.0, 0.0, 6.0, 0.0),
        IsHitTestVisible = false,
        Children = { stackPanel }
      };
    }

    // Token: 0x06001359 RID: 4953 RVA: 0x005A07BC File Offset: 0x005A07BC
    public static FrameworkElement PowerUpAvatar(string identityId, string displayName, double size)
    {
      ValueTuple<uint, uint>[] array = new ValueTuple<uint, uint>[]
      {
        new ValueTuple<uint, uint>(6514417U, 9133302U),
        new ValueTuple<uint, uint>(15485081U, 16347926U),
        new ValueTuple<uint, uint>(440020U, 3900150U),
        new ValueTuple<uint, uint>(1096065U, 8702998U),
        new ValueTuple<uint, uint>(16096779U, 15680580U),
        new ValueTuple<uint, uint>(9133302U, 15485081U)
      };
      int num = 0;
      foreach (char c in identityId)
      {
        num = (num * 31 + (int)c) & int.MaxValue;
      }
      ValueTuple<uint, uint> valueTuple = array[num % array.Length];
      uint item = valueTuple.Item1;
      uint item2 = valueTuple.Item2;
      double num2 = size * 0.24;
      Grid grid = new Grid();
      grid.Width = size;
      grid.Height = size;
      grid.Children.Add(new ShadowLayer(num2, DS.Black(0.3), 2.0, 1.0, 0.0));
      Border border = new Border
      {
        CornerRadius = new CornerRadius(num2),
        Background = new LinearGradientBrush
        {
          StartPoint = new Point(0f, 0f),
          EndPoint = new Point(1f, 1f),
          GradientStops = 
          {
            new GradientStop
            {
              Color = DS.Hex(item, 1.0),
              Offset = 0.0
            },
            new GradientStop
            {
              Color = DS.Hex(item2, 1.0),
              Offset = 1.0
            }
          }
        },
        BorderBrush = Ui.Brush(DS.White(0.9)),
        BorderThickness = new Thickness(Math.Max(0.5, size * 0.04))
      };
      string[] array2 = displayName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      string text;
      if (array2.Length != 0)
      {
        if (array2.Length <= 1)
        {
          text = array2[0].Substring(0, 1).ToUpperInvariant();
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
          defaultInterpolatedStringHandler.AppendFormatted<char>(array2[0][0]);
          defaultInterpolatedStringHandler.AppendFormatted<char>(array2[1][0]);
          text = defaultInterpolatedStringHandler.ToStringAndClear().ToUpperInvariant();
        }
      }
      else
      {
        text = "•";
      }
      string text2 = text;
      border.Child = Ui.Text(text2, size * 0.42, FontWeights.Bold, Colors.White, TextAlignment.Center, 0, false, false).Center<TextBlock>();
      grid.Children.Add(border);
      return grid;
    }

    // Token: 0x17000382 RID: 898
    // (get) Token: 0x0600135A RID: 4954 RVA: 0x005A0A8C File Offset: 0x005A0A8C
    public override VerticalAlignment ContentAlignment
    {
      get
      {
        return VerticalAlignment.Center;
      }
    }

    // Token: 0x17000383 RID: 899
    // (get) Token: 0x0600135B RID: 4955 RVA: 0x005A0A90 File Offset: 0x005A0A90
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x040008F4 RID: 2292
    private const double AvatarSize = 20.0;

    // Token: 0x040008F5 RID: 2293
    private readonly Grid view;
  }
}
