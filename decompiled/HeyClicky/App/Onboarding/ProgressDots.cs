using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200012F RID: 303
  public sealed class ProgressDots : StackPanel
  {
    // Token: 0x0600103D RID: 4157 RVA: 0x0058354C File Offset: 0x0058354C
    [NullableContext(2)]
    public ProgressDots(int count, int current, Action<int> jump, int furthest)
    {
      base.Orientation = Orientation.Horizontal;
      base.Spacing = 7.0;
      base.HorizontalAlignment = HorizontalAlignment.Center;
      for (int i = 0; i < count; i++)
      {
        bool flag = i <= current;
        GelSurface gelSurface = new GelSurface(flag ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, 3.0)
        {
          Width = (double)((i == current) ? 22 : 6),
          Height = 6.0,
          Opacity = (flag ? 1.0 : 0.3)
        };
        if (i > furthest)
        {
          gelSurface.Enabled = false;
        }
        Pressable pressable = new Pressable(true)
        {
          Padding = new Thickness(2.0, 5.0, 2.0, 5.0),
          Enabled = (i <= furthest)
        };
        pressable.Children.Add(gelSurface);
        int idx = i;
        if (jump != null && i <= furthest)
        {
          pressable.Click += delegate
          {
            jump(idx);
          };
        }
        DependencyObject dependencyObject = pressable;
        string text;
        if (i > furthest)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Step ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
          defaultInterpolatedStringHandler.AppendLiteral(", not reached yet");
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 1);
          defaultInterpolatedStringHandler.AppendLiteral("Go to step ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        AutomationProperties.SetName(dependencyObject, text);
        base.Children.Add(pressable);
      }
    }
  }
}
