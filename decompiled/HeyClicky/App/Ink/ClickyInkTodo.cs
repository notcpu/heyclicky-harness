using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200025A RID: 602
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTodo
  {
    // Token: 0x06001A56 RID: 6742 RVA: 0x005E11C0 File Offset: 0x005E11C0
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkTodo.State state = InkObjectState.For<ClickyInkTodo.State>(block, "todo");
      return new InkObjectHost(state, () => ClickyInkTodo.Build(block, clock, style, actions, state));
    }

    // Token: 0x06001A57 RID: 6743 RVA: 0x005E121C File Offset: 0x005E121C
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, ClickyInkTodo.State state)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      int num = block.Items.Count<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Source.IsChecked.GetValueOrDefault());
      double num2 = ((block.Items.Count == 0) ? 0.0 : ((double)num / (double)block.Items.Count));
      int? shownDone = state.ShownDone;
      if (shownDone != null)
      {
        int valueOrDefault = shownDone.GetValueOrDefault();
        if (valueOrDefault != num)
        {
          state.FromFraction = ((block.Items.Count == 0) ? 0.0 : ((double)valueOrDefault / (double)block.Items.Count));
          state.ChangedAt = InkObjectState.Now;
          state.ShownDone = new int?(num);
          state.Animate(InkSpring.SettleSeconds(0.4, 0.8));
        }
      }
      int num3 = state.ShownDone.GetValueOrDefault();
      if (state.ShownDone == null)
      {
        num3 = num;
        state.ShownDone = new int?(num3);
      }
      double num4 = InkSpring.Between(state.FromFraction, num2, 0.4, 0.8, state.ChangedAt);
      if (state.ChangedAt == -1000.0)
      {
        num4 = num2;
      }
      double num5 = 8.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
      UIElement[] array = new UIElement[3];
      array[0] = ClickyInkHStack.Flexible<InkTextView>(InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), new Color?(ClickyInkTodo.Ink), true, null));
      array[1] = new ClickyInkSpacer();
      int num6 = 2;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>(num);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(block.Items.Count);
      array[num6] = InkViews.Plain(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperFonts.Hand(16.0), ClickyInkTodo.Ink.Opacity(0.6), null);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num5, inkVerticalAlignment, array)
      {
        Margin = new Thickness(0.0, 0.0, 0.0, 6.0)
      };
      ClickyInkTodo.ProgressStrip progressStrip = new ClickyInkTodo.ProgressStrip(num4)
      {
        Height = 7.0,
        Margin = new Thickness(0.0, 0.0, 0.0, 6.0)
      };
      StackPanel stackPanel = InkViews.VStack(0.0, new UIElement[]
      {
        clickyInkHStack,
        progressStrip,
        new Border
        {
          Height = 1.0,
          Background = Ui.Brush(ClickyInkTodo.RuleBlue)
        }
      });
      for (int j = 0; j < block.Items.Count; j++)
      {
        stackPanel.Children.Add(ClickyInkTodo.Row(block, block.Items[j], j, clock, style, actions, state));
        stackPanel.Children.Add(new Border
        {
          Height = 1.0,
          Background = Ui.Brush(ClickyInkTodo.RuleBlue)
        });
      }
      stackPanel.Margin = new Thickness(44.0, 12.0, 16.0, 14.0);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 3.0,
        Margin = new Thickness(32.0, 0.0, 0.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        IsHitTestVisible = false,
        Children = 
        {
          new Border
          {
            Width = 1.0,
            Background = Ui.Brush(ClickyInkTodo.MarginRed)
          },
          new Border
          {
            Width = 1.0,
            Background = Ui.Brush(ClickyInkTodo.MarginRed)
          }
        }
      };
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>()).Behind(stackPanel2);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("todo-glue", ClickyShape.UnevenRoundedRectangle(3.0, 0.0, 0.0, 3.0), PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#C46A62")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#B05A53"))
      }), new double?(0.3), null, null, null)
      {
        Height = 8.0
      };
      PaperDrawing paperDrawing = new PaperDrawing().Stroke(new PaperPath().MoveTo(0.0, 0.5).LineTo(2000.0, 0.5), new PaperStroke(PaperColor.Black.Opacity(0.1), 1.0, PenLineCap.Flat, PenLineJoin.Miter, new double[] { 2.0, 3.0 }));
      paperDrawing.Height = 1.0;
      paperDrawing.Clip = new RectangleGeometry
      {
        Rect = new Rect(0.0, -1.0, contentWidth - 8.0, 3.0)
      };
      StackPanel stackPanel3 = InkViews.VStack(0.0, new UIElement[] { inkPaperSheet, paperDrawing, clickyInkOverlay });
      InkPaperSheet inkPaperSheet2 = new InkPaperSheet("todo-pad", ClickyShape.RoundedRectangle(3.0), PaperFill.Of(PaperColor.Hex("#FFFCEC")), new double?(0.18), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.07), 2.0, 0.0, 1.0)
      }, null, null);
      ClickyInkOverlay clickyInkOverlay2 = new ClickyInkOverlay(stackPanel3, Array.Empty<UIElement>()).Behind(inkPaperSheet2);
      clickyInkOverlay2.Width = contentWidth - 8.0;
      clickyInkOverlay2.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay2.Transformed(0.0, 0.0, -0.2, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkOverlay2 }
      };
    }

    // Token: 0x06001A58 RID: 6744 RVA: 0x005E18DC File Offset: 0x005E18DC
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, int index, double clock, ClickyInkStyle style, ClickyInkActions actions, ClickyInkTodo.State state)
    {
      bool isChecked = item.Source.IsChecked.GetValueOrDefault();
      bool flag;
      if (state.ShownTicks.TryGetValue(item.Id, out flag) && flag != isChecked)
      {
        state.TickChangedAt[item.Id] = InkObjectState.Now;
        state.Animate(InkSpring.SettleSeconds(0.3, 0.7));
      }
      state.ShownTicks[item.Id] = isChecked;
      double num = isChecked > false;
      double num2;
      if (state.TickChangedAt.TryGetValue(item.Id, out num2))
      {
        num = InkSpring.Between(!isChecked, isChecked > false, 0.3, 0.7, num2);
      }
      bool flag2 = isChecked || num > 0.01;
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkBox(index + 3), new PaperStroke(ClickyInkTodo.Ink.Opacity(0.55), 1.3, PenLineCap.Flat, PenLineJoin.Miter, null));
      clickyShapeView.Width = 15.0;
      clickyShapeView.Height = 15.0;
      clickyShapeView.HorizontalAlignment = HorizontalAlignment.Center;
      clickyShapeView.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Width = 16.0,
        Height = 16.0,
        Children = { clickyShapeView }
      };
      if (flag2)
      {
        ClickyShapeView clickyShapeView2 = ClickyShapeView.Stroke(new ClickyInkTick(), new PaperStroke(ClickyPaperInk.DoneGreen, 2.2, PenLineCap.Round, PenLineJoin.Round, null));
        clickyShapeView2.Width = 15.0;
        clickyShapeView2.Height = 14.0;
        clickyShapeView2.HorizontalAlignment = HorizontalAlignment.Center;
        clickyShapeView2.VerticalAlignment = VerticalAlignment.Center;
        clickyShapeView2.Transformed(2.0, -2.0, 0.0, 1.0, null);
        if (num < 0.999)
        {
          double num3 = 0.4 + 0.6 * num;
          Grid grid2 = new Grid
          {
            Width = 15.0,
            Height = 14.0,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
          };
          clickyShapeView2.HorizontalAlignment = HorizontalAlignment.Stretch;
          clickyShapeView2.VerticalAlignment = VerticalAlignment.Stretch;
          grid2.Children.Add(clickyShapeView2);
          grid2.Opacity = Math.Clamp(num, 0.0, 1.0);
          grid2.RenderTransformOrigin = new Point(0f, 1f);
          grid2.RenderTransform = new ScaleTransform
          {
            ScaleX = num3,
            ScaleY = num3
          };
          grid.Children.Add(grid2);
        }
        else
        {
          grid.Children.Add(clickyShapeView2);
        }
      }
      InkTextView inkTextView = InkViews.Words(item.Words, clock, style, ClickyPaperFonts.Hand(17.0), new Color?(isChecked ? ClickyInkTodo.Ink.Opacity(0.45) : ClickyInkTodo.Ink), true, delegate(InkTextView t)
      {
        if (isChecked)
        {
          t.Strikethrough = new Color?(ClickyInkTodo.Ink.Opacity(0.45));
        }
      });
      StackPanel stackPanel = InkViews.VStack(1.0, new UIElement[] { inkTextView });
      ClickyInkLiveNote.Words detail = item.Detail;
      if (detail != null && ClickyInkWords.IsStarted(detail, clock))
      {
        stackPanel.Children.Add(InkViews.Words(detail, clock, style, ClickyPaperFonts.Typed(11.0, null), new Color?(ClickyInkTodo.Ink.Opacity(0.55)), false, null));
      }
      Pressable pressable = InkViews.PlainButton(new ClickyInkHStack(10.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        grid.BaselineFromBottom(2.0),
        ClickyInkHStack.Flexible<StackPanel>(stackPanel),
        new ClickyInkSpacer()
      })
      {
        Margin = new Thickness(0.0, 6.0, 0.0, 6.0)
      }, delegate
      {
        actions.Toggle(block.Id, item.Id);
      }, ClickyInkNoteActions.Plain(item.Source.Text));
      pressable.HorizontalAlignment = HorizontalAlignment.Stretch;
      AutomationProperties.SetItemStatus(pressable, isChecked ? "done" : "not done");
      return pressable;
    }

    // Token: 0x04000CA9 RID: 3241
    private static readonly Color Ink = PaperColor.Hex("#2E2A1F");

    // Token: 0x04000CAA RID: 3242
    private static readonly Color RuleBlue = PaperColor.Hex("#6E9BD6").Opacity(0.25);

    // Token: 0x04000CAB RID: 3243
    private static readonly Color MarginRed = PaperColor.Hex("#E5484D").Opacity(0.28);

    // Token: 0x04000CAC RID: 3244
    private const double TickResponse = 0.3;

    // Token: 0x04000CAD RID: 3245
    private const double TickDamping = 0.7;

    // Token: 0x0200081E RID: 2078
    [Nullable(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x040022A6 RID: 8870
      public int? ShownDone;

      // Token: 0x040022A7 RID: 8871
      public double FromFraction;

      // Token: 0x040022A8 RID: 8872
      public double ChangedAt = -1000.0;

      // Token: 0x040022A9 RID: 8873
      public readonly Dictionary<string, bool> ShownTicks = new Dictionary<string, bool>();

      // Token: 0x040022AA RID: 8874
      public readonly Dictionary<string, double> TickChangedAt = new Dictionary<string, double>();
    }

    // Token: 0x0200081F RID: 2079
    [NullableContext(0)]
    private sealed class ProgressStrip : Grid
    {
      // Token: 0x06003827 RID: 14375 RVA: 0x006867D4 File Offset: 0x006867D4
      public ProgressStrip(double fraction)
      {
        Border border = new Border
        {
          CornerRadius = new CornerRadius(3.5),
          Background = Ui.Brush(ClickyInkTodo.Ink.Opacity(0.07))
        };
        Border done = new Border
        {
          CornerRadius = new CornerRadius(3.5),
          Background = Ui.Brush(PaperColor.Hex("#A6D85A").Opacity(0.55)),
          HorizontalAlignment = HorizontalAlignment.Left
        };
        base.Children.Add(border);
        base.Children.Add(done);
        base.SizeChanged += delegate(object _, SizeChangedEventArgs e)
        {
          done.Width = Math.Max(0.0, e.NewSize.Width * fraction);
        };
      }
    }
  }
}
