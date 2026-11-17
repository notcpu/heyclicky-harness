using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200023A RID: 570
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkCriteria
  {
    // Token: 0x060019C0 RID: 6592 RVA: 0x005D86E0 File Offset: 0x005D86E0
    public static void SetEditingForGallery(ClickyInkLiveNote.Block block)
    {
      ClickyInkCriteria.State state = InkObjectState.For<ClickyInkCriteria.State>(block, "criteria");
      ClickyInkCriteria.StartEditing(block, state);
      state.EditingChangedAt = -1000.0;
    }

    // Token: 0x060019C1 RID: 6593 RVA: 0x005D8710 File Offset: 0x005D8710
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkCriteria.State state = InkObjectState.For<ClickyInkCriteria.State>(block, "criteria");
      return new InkObjectHost(state, () => ClickyInkCriteria.Build(block, actions, state));
    }

    // Token: 0x060019C2 RID: 6594 RVA: 0x005D8760 File Offset: 0x005D8760
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, ClickyInkActions actions, ClickyInkCriteria.State state)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      InkTextView inkTextView = InkViews.Plain((block.Words.Current.Length == 0) ? "What I look for" : ClickyInkNoteActions.Plain(block.Words.Current), InkObjectFonts.Mono(14.0, new FontWeight?(FontWeights.Bold)), ClickyInkCriteria.Ink, null);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        ClickyInkHStack.Flexible<InkTextView>(inkTextView),
        new ClickyInkSpacer()
      });
      if (!state.IsEditing)
      {
        InkTextView inkTextView2 = InkViews.Plain("edit", InkObjectFonts.Mono(11.5, new FontWeight?(FontWeights.SemiBold)), ClickyInkCriteria.StampBlue, null);
        Border border = new Border
        {
          Height = 1.0,
          Background = Ui.Brush(ClickyInkCriteria.StampBlue),
          VerticalAlignment = VerticalAlignment.Bottom,
          Margin = new Thickness(0.0, 0.0, 0.0, 1.0)
        };
        clickyInkHStack.Children.Add(InkViews.PlainButton(new ClickyInkOverlay(inkTextView2, new UIElement[] { border }), delegate
        {
          ClickyInkCriteria.StartEditing(block, state);
          Action<string, ClickyInkDocument.Kind, JsonObject> track = actions.Track;
          string text = "criteria_edit_started";
          ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Criteria;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["fields"] = block.Items.Count;
          track(text, kind, jsonObject);
          state.Changed();
          state.Animate(0.2);
        }, "edit"));
      }
      double num = ClickyInkClock.EaseOut(ClickyInkClock.Progress(InkObjectState.Now, state.EditingChangedAt, state.EditingChangedAt + 0.2));
      StackPanel stackPanel = InkViews.VStack(10.0, new UIElement[]
      {
        clickyInkHStack,
        new Border
        {
          Height = 1.2,
          Background = Ui.Brush(PaperColor.Hex("#D9453B").Opacity(0.4))
        }
      });
      for (int i = 0; i < block.Items.Count; i++)
      {
        ClickyInkLiveNote.Item item = block.Items[i];
        InkTextView inkTextView3 = InkViews.Plain(ClickyInkNoteActions.Plain(item.Source.Text).ToUpperInvariant() + ":", InkObjectFonts.Mono(10.5, new FontWeight?(FontWeights.Bold)), ClickyInkCriteria.Ink.Opacity(0.55), null);
        Grid grid = new Grid
        {
          Width = 96.0,
          Children = { inkTextView3 }
        };
        FrameworkElement frameworkElement;
        if (state.IsEditing)
        {
          Action<string> <>9__2;
          ClickyInkBlankLine clickyInkBlankLine = state.Field<ClickyInkBlankLine>(item.Id, delegate
          {
            ClickyInkBlankLine clickyInkBlankLine2 = new ClickyInkBlankLine(item.Source.Text, ClickyInkCriteria.Ink, InkObjectFonts.Mono(12.5, null));
            Action<string> action;
            if ((action = <>9__2) == null)
            {
              action = (<>9__2 = delegate(string text)
              {
                state.Edits[item.Id] = text;
              });
            }
            clickyInkBlankLine2.TextChanged += action;
            return clickyInkBlankLine2;
          });
          string valueOrDefault = state.Edits.GetValueOrDefault(item.Id, "");
          if (clickyInkBlankLine.Text != valueOrDefault)
          {
            clickyInkBlankLine.Text = valueOrDefault;
          }
          frameworkElement = clickyInkBlankLine;
          frameworkElement.Opacity = num;
        }
        else
        {
          ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
          {
            Spacing = 7.0,
            LineSpacing = 7.0
          };
          List<string> list = ClickyInkCriteriaRules.Values(item.Source.Detail);
          for (int j = 0; j < list.Count; j++)
          {
            InkTextView inkTextView4 = InkViews.Plain(list[j], InkObjectFonts.Mono(11.5, new FontWeight?(FontWeights.SemiBold)), ClickyInkCriteria.StampBlue.Opacity(0.75), null);
            inkTextView4.Margin = new Thickness(6.0, 2.0, 6.0, 2.0);
            ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(inkTextView4, new UIElement[] { ClickyShapeView.Stroke(ClickyShape.RoundedRectangle(2.5), new PaperStroke(ClickyInkCriteria.StampBlue.Opacity(0.4), 1.1, PenLineCap.Flat, PenLineJoin.Miter, null)) });
            clickyInkOverlay.Transformed(0.0, 0.0, (double)((i * 3 + j) % 5) * 0.35 - 0.7, 1.0, null);
            clickyNotebookFlow.Children.Add(clickyInkOverlay);
          }
          frameworkElement = clickyNotebookFlow;
        }
        ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(10.0, state.IsEditing ? InkVerticalAlignment.Center : InkVerticalAlignment.FirstBaseline, new UIElement[] { grid });
        InkViews.Hold<FrameworkElement>(clickyInkHStack2, ClickyInkHStack.Flexible<FrameworkElement>(frameworkElement));
        stackPanel.Children.Add(clickyInkHStack2);
      }
      if (state.IsEditing)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 8.0,
          Margin = new Thickness(0.0, 2.0, 0.0, 0.0),
          Opacity = num,
          Children = 
          {
            new ClickyPaperGelButton("Save changes", delegate
            {
              ClickyInkCriteria.Save(block, actions, state);
            }, ClickyPaperGel.Blue, 27.0, 13.0, 12.5, null),
            new ClickyPaperGelButton("Cancel", delegate
            {
              actions.Track("criteria_edit_cancelled", ClickyInkDocument.Kind.Criteria, new JsonObject(null));
              state.IsEditing = false;
              state.EditingChangedAt = InkObjectState.Now;
              state.Changed();
              state.Animate(0.2);
            }, ClickyPaperGel.White, 27.0, 13.0, 12.5, null)
          }
        };
        stackPanel.Children.Add(stackPanel2);
      }
      stackPanel.Margin = new Thickness(18.0, 15.0, 18.0, 30.0);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("criteria-card", ClickyShape.RoundedRectangle(5.0), PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FDF9EF")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#F9F1DF"))
      }), new double?(0.25), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.07), 2.0, 0.0, 1.0)
      }, null, null);
      Grid grid2 = new Grid
      {
        Width = 11.0,
        Height = 11.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(0.0, 0.0, 0.0, 9.0),
        IsHitTestVisible = false,
        Children = 
        {
          new Ellipse
          {
            Fill = Ui.Brush(PaperColor.Hex("#6B5634").Opacity(0.2))
          },
          new Ellipse
          {
            Stroke = Ui.Brush(PaperColor.Hex("#A88D5C").Opacity(0.6)),
            StrokeThickness = 0.8
          }
        }
      };
      ClickyInkOverlay clickyInkOverlay2 = new ClickyInkOverlay(stackPanel, new UIElement[] { grid2 }).Behind(inkPaperSheet);
      clickyInkOverlay2.Width = contentWidth - 10.0;
      clickyInkOverlay2.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay2.Transformed(0.0, 0.0, 0.2, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(4.0, 0.0, 0.0, 0.0),
        Children = { clickyInkOverlay2 }
      };
    }

    // Token: 0x060019C3 RID: 6595 RVA: 0x005D8FE0 File Offset: 0x005D8FE0
    private static void StartEditing(ClickyInkLiveNote.Block block, ClickyInkCriteria.State state)
    {
      state.Edits.Clear();
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        state.Edits[item.Id] = ClickyInkNoteActions.Plain(item.Source.Detail ?? "");
      }
      state.IsEditing = true;
      state.EditingChangedAt = InkObjectState.Now;
    }

    // Token: 0x060019C4 RID: 6596 RVA: 0x005D9074 File Offset: 0x005D9074
    private static void Save(ClickyInkLiveNote.Block block, ClickyInkActions actions, ClickyInkCriteria.State state)
    {
      List<ValueTuple<string, string, string>> list = block.Items.Select<ClickyInkLiveNote.Item, ValueTuple<string, string, string>>((ClickyInkLiveNote.Item item) => new ValueTuple<string, string, string>(item.Source.Text, item.Source.Detail ?? "", state.Edits.GetValueOrDefault(item.Id, ""))).ToList<ValueTuple<string, string, string>>();
      ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.Criteria(block.Source, list);
      if (clickyInkNoteAction != null)
      {
        actions.Perform(clickyInkNoteAction);
      }
      else
      {
        actions.Track("criteria_saved_unchanged", ClickyInkDocument.Kind.Criteria, new JsonObject(null));
      }
      state.IsEditing = false;
      state.EditingChangedAt = InkObjectState.Now;
      state.Changed();
      state.Animate(0.2);
    }

    // Token: 0x04000C70 RID: 3184
    private static readonly Color Ink = PaperColor.Hex("#33291A");

    // Token: 0x04000C71 RID: 3185
    private static readonly Color StampBlue = PaperColor.Hex("#2F4FA0");

    // Token: 0x020007E9 RID: 2025
    [NullableContext(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x040021ED RID: 8685
      public bool IsEditing;

      // Token: 0x040021EE RID: 8686
      public double EditingChangedAt = -1000.0;

      // Token: 0x040021EF RID: 8687
      [Nullable(1)]
      public readonly Dictionary<string, string> Edits = new Dictionary<string, string>();
    }
  }
}
