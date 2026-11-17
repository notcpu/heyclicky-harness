using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200031F RID: 799
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LiveLineView : Grid
  {
    // Token: 0x0600227A RID: 8826 RVA: 0x006244D8 File Offset: 0x006244D8
    public static string GlyphPath(ClickyLiveLine line)
    {
      string text;
      if (line.Kind == ClickyLiveLine.LineKind.Thinking)
      {
        text = "M1.4 10 L4.9 5.6 L12.2 18.2 M12.2 18.2 L19 18.2 M14.3 10 L18.6 5.6 L22.6 10";
      }
      else
      {
        ClickyInkWorkStep.StepKind? stepKind = line.StepKind;
        string text2;
        if (stepKind != null)
        {
          switch (stepKind.GetValueOrDefault())
          {
          case ClickyInkWorkStep.StepKind.Search:
            text2 = "M10.4 4.3c3.5-.1 6.3 2.7 6.2 6.1-.1 3.3-2.9 6-6.2 5.9-3.4-.1-6-2.9-5.9-6.2.1-3.2 2.7-5.7 5.9-5.8Z M15.1 15.3l4.8 4.6";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.Read:
            text2 = "M3.4 6.3c2.9-.9 5.9-.6 8.6 1.2 2.7-1.8 5.7-2.1 8.6-1.2v11.8c-2.9-.8-5.9-.5-8.6 1.2-2.7-1.7-5.7-2-8.6-1.2Z M12 7.5v11.6";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.Look:
            text2 = "M6.2 3.6h7.6l3.8 3.8v4.4 M6.2 3.6v16.6h5.6 M9 9.3h4.6 M9 12.6h3 M16.3 13.4c1.7 0 3 1.3 3 3s-1.3 3-3 3-3-1.3-3-3 1.3-3 3-3Z M18.5 18.6l2.1 2";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.Math:
            text2 = "M4.6 8.2h6 M7.6 5.2v6 M13.6 6.8h5.6 M13.6 10h5.6 M4.8 16.8h14.4";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.Note:
            text2 = "M4.6 19.4l1-4.1 9.9-9.9a1.9 1.9 0 0 1 2.7 0l.4.4a1.9 1.9 0 0 1 0 2.7l-9.9 9.9Z M13.7 7.2l3.1 3.1";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.File:
            text2 = "M6.4 3.6h7.4l4 4v12.8H6.4Z M13.8 3.6v4h4 M9.2 12.4h5.6 M9.2 15.6h4";
            goto IL_008A;
          case ClickyInkWorkStep.StepKind.Apps:
            text2 = "M5 5.2h5.1v5.1H5Z M13.9 5h5.1v5.1h-5.1Z M5 13.9h5.1V19H5Z M13.9 13.8h5.1v5.1h-5.1Z";
            goto IL_008A;
          }
        }
        text2 = "M6.2 4.2l12 7.2-5.4 1.3 3 5.9-2.2 1.1-3-5.9-4.2 3.6Z";
        IL_008A:
        text = text2;
      }
      return text;
    }

    // Token: 0x0600227B RID: 8827 RVA: 0x00624574 File Offset: 0x00624574
    public LiveLineView(ClickyLiveLine line, Color ink, double fontSize = 11.5, bool risesIn = false)
    {
      if (risesIn && !Anim.Instant)
      {
        base.Opacity = 0.0;
        base.Loaded += delegate(object _, RoutedEventArgs _)
        {
          CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseOut(Ui.Compositor);
          if (Anim.ReduceMotion)
          {
            Anim.ReducedMotionFade(this, 1f, 0.2);
            return;
          }
          base.Translation = new Vector3(0f, 4f, 0f);
          Anim.Opacity(this, 1f, 0.32, cubicBezierEasingFunction, 0.0);
          Anim.Translate(this, Vector3.Zero, 0.32, cubicBezierEasingFunction);
        };
      }
      base.ColumnSpacing = 5.0;
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      double num = fontSize + 3.5;
      Path path = Icons.StrokeIcon(LiveLineView.GlyphPath(line), num, ink.WithAlpha(0.62), 1.6);
      path.Stretch = Stretch.None;
      path.RenderTransform = new ScaleTransform
      {
        ScaleX = num / 24.0,
        ScaleY = num / 24.0
      };
      path.StrokeThickness = 38.400000000000006 / num;
      Grid grid = new Grid
      {
        Width = num,
        Height = num,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { path }
      };
      base.Children.Add(grid);
      BreathingText breathingText = new BreathingText(line.Text, fontSize, ink.WithAlpha(0.5), ink.WithAlpha(0.95), !Anim.ReduceMotion);
      if (line.Icons.Count > 0)
      {
        base.ColumnDefinitions[1].Width = GridLength.Auto;
        base.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        base.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        LiveLineIconStack liveLineIconStack = new LiveLineIconStack(line.Icons, line.MoreIcons, fontSize + 2.5, ink)
        {
          VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetColumn(liveLineIconStack, 2);
        base.Children.Add(liveLineIconStack);
      }
      Grid.SetColumn(breathingText, 1);
      base.Children.Add(breathingText);
      AutomationProperties.SetName(this, line.Text);
    }

    // Token: 0x040010AE RID: 4270
    private const string ThinkingPath = "M1.4 10 L4.9 5.6 L12.2 18.2 M12.2 18.2 L19 18.2 M14.3 10 L18.6 5.6 L22.6 10";
  }
}
