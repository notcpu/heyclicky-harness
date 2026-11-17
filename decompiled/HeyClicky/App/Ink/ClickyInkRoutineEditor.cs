using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000237 RID: 567
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkRoutineEditor
  {
    // Token: 0x060019B6 RID: 6582 RVA: 0x005D71D4 File Offset: 0x005D71D4
    public static FrameworkElement Create(IReadOnlyList<ClickyInkRoutineControl> routines, ClickyInkStyle style, ClickyInkActions actions, [Nullable(2)] Action changed = null)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 14.0,
        Margin = new Thickness(14.0, 12.0, 14.0, 12.0)
      };
      for (int i = 0; i < routines.Count; i++)
      {
        if (i > 0)
        {
          stackPanel.Children.Add(new Border
          {
            Height = 1.0,
            Background = Ui.Brush(style.Ink.Opacity(0.08))
          });
        }
        stackPanel.Children.Add(ClickyInkRoutineEditor.Row(routines[i], style, actions, changed));
      }
      return new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Children = 
        {
          ClickyShapeView.Fill(ClickyShape.RoundedRectangle(9.0), style.Ink.Opacity(0.035)),
          stackPanel,
          ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(9.0), new PaperStroke(style.Ink.Opacity(0.09), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null))
        }
      };
    }

    // Token: 0x060019B7 RID: 6583 RVA: 0x005D7320 File Offset: 0x005D7320
    private static FrameworkElement Row(ClickyInkRoutineControl routine, ClickyInkStyle style, ClickyInkActions actions, [Nullable(2)] Action changed)
    {
      bool flag2;
      bool flag = !(ClickyInkRoutineEditor.pausedOverrides.TryGetValue(routine.Id, out flag2) ? flag2 : routine.IsPaused);
      int num2;
      int num = (ClickyInkRoutineEditor.intervalOverrides.TryGetValue(routine.Id, out num2) ? num2 : routine.IntervalMinutes);
      bool hasStarted = ClickyInkRoutineEditor.startedRunIDs.Contains(routine.Id) || routine.IsRunning;
      InkTextView inkTextView = InkTextView.Plain((routine.Title.Length == 0) ? "Routine" : routine.Title, ClickyPaperFonts.Typed(13.5, new FontWeight?(FontWeights.SemiBold)), style.Ink).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(2);
      });
      InkTextView inkTextView2 = InkTextView.Plain(flag ? "On" : "Paused", ClickyPaperFonts.Typed(11.5, new FontWeight?(FontWeights.Medium)), style.Quiet);
      FrameworkElement frameworkElement = ClickyInkRoutineEditor.MiniSwitch(flag, flag ? ("Pause " + routine.Title) : ("Turn " + routine.Title + " back on"), delegate(bool value)
      {
        ClickyInkRoutineEditor.pausedOverrides[routine.Id] = !value;
        actions.SetRoutinePaused(routine.Id, !value);
        Action changed2 = changed;
        if (changed2 == null)
        {
          return;
        }
        changed2();
      });
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
      {
        inkTextView,
        new ClickyInkSpacer(),
        InkDashboardLayout.Fixed(inkTextView2),
        frameworkElement
      });
      InkTextView inkTextView3 = InkTextView.Plain("HOW OFTEN", ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.Bold)), style.Quiet).With(delegate(InkTextView t)
      {
        t.Tracking = 1.0;
      });
      ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
      {
        Spacing = 6.0,
        LineSpacing = 6.0,
        Opacity = (flag ? 1.0 : 0.55)
      };
      foreach (int num3 in ClickyInkDashboardViewMath.RoutineIntervalOptions(num))
      {
        bool isChosen = num3 == num;
        int m = num3;
        clickyNotebookFlow.Children.Add(new ClickyPaperGelButton(ClickyInkRoutineControl.CadenceWords(num3), delegate
        {
          if (isChosen)
          {
            return;
          }
          ClickyInkRoutineEditor.intervalOverrides[routine.Id] = m;
          actions.SetRoutineInterval(routine.Id, m);
          Action changed3 = changed;
          if (changed3 == null)
          {
            return;
          }
          changed3();
        }, isChosen ? ClickyPaperGel.Blue : ClickyPaperGel.White, 24.0, 10.0, 11.5, isChosen ? "checkmark" : null));
      }
      ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(hasStarted ? "Running" : "Run now", delegate
      {
        if (hasStarted)
        {
          return;
        }
        if (actions.RunRoutineNow(routine.Id))
        {
          ClickyInkRoutineEditor.startedRunIDs.Add(routine.Id);
          Action changed4 = changed;
          if (changed4 == null)
          {
            return;
          }
          changed4();
        }
      }, ClickyPaperGel.White, 24.0, 10.0, 11.5, hasStarted ? "checkmark" : "play.fill")
      {
        Enabled = !hasStarted
      };
      ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[] { clickyPaperGelButton });
      if (routine.RunCount > 0)
      {
        clickyInkHStack2.Children.Add(InkTextView.Plain(ClickyInkDashboardViewMath.RunCountWords(routine.RunCount), ClickyPaperFonts.Typed(11.0, null), style.Quiet));
      }
      clickyInkHStack2.HorizontalAlignment = HorizontalAlignment.Left;
      return new StackPanel
      {
        Spacing = 9.0,
        Children = { clickyInkHStack, inkTextView3, clickyNotebookFlow, clickyInkHStack2 }
      };
    }

    // Token: 0x060019B8 RID: 6584 RVA: 0x005D775C File Offset: 0x005D775C
    private static FrameworkElement MiniSwitch(bool isOn, string accessibilityLabel, Action<bool> set)
    {
      Pressable pressable = new Pressable(true);
      pressable.Width = 26.0;
      pressable.Height = 15.0;
      pressable.AccessibleToggleState = new bool?(isOn);
      pressable.VerticalAlignment = VerticalAlignment.Center;
      AutomationProperties.SetName(pressable, accessibilityLabel);
      ClickyShapeView clickyShapeView = ClickyShapeView.Fill(ClickyShape.Capsule, isOn ? PaperColor.Hex("#007AFF") : PaperColor.Black.Opacity(0.1));
      Grid grid = new Grid
      {
        Width = 13.0,
        Height = 13.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Center,
        Margin = new Thickness(isOn ? 12.0 : 1.0, 0.0, 0.0, 0.0),
        Children = 
        {
          ClickyShapeView.Fill(ClickyShape.Circle, PaperColor.White),
          ClickyShapeView.StrokeBorder(ClickyShape.Circle, new PaperStroke(PaperColor.Black.Opacity(0.12), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null))
        }
      };
      pressable.Children.Add(clickyShapeView);
      pressable.Children.Add(grid);
      pressable.Click += delegate
      {
        set(!isOn);
      };
      return pressable;
    }

    // Token: 0x04000C6B RID: 3179
    private static readonly Dictionary<string, bool> pausedOverrides = new Dictionary<string, bool>();

    // Token: 0x04000C6C RID: 3180
    private static readonly Dictionary<string, int> intervalOverrides = new Dictionary<string, int>();

    // Token: 0x04000C6D RID: 3181
    private static readonly HashSet<string> startedRunIDs = new HashSet<string>();
  }
}
