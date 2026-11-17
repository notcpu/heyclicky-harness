using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000255 RID: 597
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkForm
  {
    // Token: 0x06001A3B RID: 6715 RVA: 0x005DE1A8 File Offset: 0x005DE1A8
    public static void SetForGallery(ClickyInkLiveNote.Block block, IReadOnlyDictionary<string, string> valuesByLabel)
    {
      ClickyInkForm.State state = InkObjectState.For<ClickyInkForm.State>(block, "form");
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        string text;
        if (valuesByLabel.TryGetValue(ClickyInkNoteActions.Plain(item.Source.Text), out text))
        {
          state.Values[item.Id] = text;
        }
      }
    }

    // Token: 0x06001A3C RID: 6716 RVA: 0x005DE22C File Offset: 0x005DE22C
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkForm.State state = InkObjectState.For<ClickyInkForm.State>(block, "form");
      return new InkObjectHost(state, () => ClickyInkForm.Build(block, clock, style, actions, state));
    }

    // Token: 0x06001A3D RID: 6717 RVA: 0x005DE288 File Offset: 0x005DE288
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, ClickyInkForm.State state)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkDocument.Block source = block.Source;
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(9.0, InkVerticalAlignment.Center, new UIElement[] { ClickyInkRubberStamp.Make("needs you", null, -5.0) });
      if (block.Words.Current.Length > 0)
      {
        clickyInkHStack.Children.Add(InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Typed(14.5, new FontWeight?(FontWeights.SemiBold)), new Color?(ClickyInkForm.Ink), true, null));
      }
      StackPanel stackPanel = InkViews.VStack(13.0, new UIElement[] { clickyInkHStack });
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        stackPanel.Children.Add(ClickyInkForm.Field(item, style, state));
      }
      ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(ClickyInkNoteActions.Plain(source["send_label"] ?? "Send"), delegate
      {
        base.<Build>g__Submit|0(false);
      }, ClickyPaperGel.Blue, 28.0, 13.0, 12.5, null);
      state.Send = clickyPaperGelButton;
      state.RefreshSend();
      ClickyPaperGelButton clickyPaperGelButton2 = new ClickyPaperGelButton(ClickyInkNoteActions.Plain(source["defaults_label"] ?? "Use your best guess"), delegate
      {
        base.<Build>g__Submit|0(true);
      }, ClickyPaperGel.White, 28.0, 13.0, 12.5, null);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        Margin = new Thickness(0.0, 2.0, 0.0, 0.0),
        Children = { clickyPaperGelButton, clickyPaperGelButton2 }
      };
      stackPanel.Children.Add(stackPanel2);
      stackPanel.Margin = new Thickness(18.0, 26.0, 18.0, 16.0);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("form-sheet", ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#FFFDF8")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.1), 3.0, 0.0, 1.5)
      }, null, null);
      FrameworkElement frameworkElement = ClickyInkForm.Clip();
      frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement.VerticalAlignment = VerticalAlignment.Top;
      frameworkElement.Transformed(0.0, -8.0, 0.0, 1.0, null);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, new UIElement[] { frameworkElement }).Behind(inkPaperSheet);
      clickyInkOverlay.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
      clickyInkOverlay.Width = contentWidth - 6.0;
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay.Transformed(0.0, 0.0, -0.2, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(0.0, 6.0, 0.0, 0.0),
        Children = { clickyInkOverlay }
      };
    }

    // Token: 0x06001A3E RID: 6718 RVA: 0x005DE6CC File Offset: 0x005DE6CC
    private static FrameworkElement Clip()
    {
      string text = "form-clip";
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(5.0);
      PaperFill paperFill = PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#F2F3F6")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#A6ABB5"))
      });
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.12), 1.0, 0.0, 0.8)
      };
      PaperStroke paperStroke = new PaperStroke(PaperColor.Black.Opacity(0.18), 0.8, PenLineCap.Flat, PenLineJoin.Miter, null);
      InkPaperSheet inkPaperSheet = new InkPaperSheet(text, clickyShape, paperFill, null, array, paperStroke, null)
      {
        Width = 68.0,
        Height = 17.0
      };
      Border border = new Border
      {
        Width = 24.0,
        Height = 5.0,
        CornerRadius = new CornerRadius(2.5),
        Background = Ui.Brush(PaperColor.Black.Opacity(0.22)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      border.Transformed(0.0, -2.0, 0.0, 1.0, null);
      Grid grid = new Grid();
      grid.Width = 68.0;
      grid.Height = 17.0;
      grid.IsHitTestVisible = false;
      grid.Children.Add(inkPaperSheet);
      grid.Children.Add(border);
      AutomationProperties.SetAccessibilityView(grid, AccessibilityView.Raw);
      return grid;
    }

    // Token: 0x06001A3F RID: 6719 RVA: 0x005DE8AC File Offset: 0x005DE8AC
    private static FrameworkElement Field(ClickyInkLiveNote.Item item, ClickyInkStyle style, ClickyInkForm.State state)
    {
      ClickyInkForm.<>c__DisplayClass6_0 CS$<>8__locals1 = new ClickyInkForm.<>c__DisplayClass6_0();
      CS$<>8__locals1.state = state;
      CS$<>8__locals1.item = item;
      CS$<>8__locals1.label = ClickyInkNoteActions.Plain(CS$<>8__locals1.item.Source.Text);
      CS$<>8__locals1.hint = CS$<>8__locals1.item.Source.Detail ?? "";
      List<string> list = ClickyInkChoices.Of(CS$<>8__locals1.hint);
      StackPanel stackPanel = InkViews.VStack(5.0, new UIElement[] { ClickyInkRubberStamp.TypedLabel(CS$<>8__locals1.label, ClickyInkForm.Ink.Opacity(0.55)) });
      ClickyInkFormRules.FieldKind fieldKind = ClickyInkFormRules.Kind(CS$<>8__locals1.hint);
      if (fieldKind != ClickyInkFormRules.FieldKind.Choices)
      {
        if (fieldKind != ClickyInkFormRules.FieldKind.File)
        {
          ClickyInkBlankLine clickyInkBlankLine = CS$<>8__locals1.state.Field<ClickyInkBlankLine>(CS$<>8__locals1.item.Id, delegate
          {
            ClickyInkBlankLine clickyInkBlankLine2 = new ClickyInkBlankLine(CS$<>8__locals1.hint, ClickyInkForm.Ink, null);
            clickyInkBlankLine2.Text = CS$<>8__locals1.state.Values.GetValueOrDefault(CS$<>8__locals1.item.Id, "");
            Action<string> action;
            if ((action = CS$<>8__locals1.<>9__2) == null)
            {
              action = (CS$<>8__locals1.<>9__2 = delegate(string text)
              {
                CS$<>8__locals1.state.Values[CS$<>8__locals1.item.Id] = text;
                CS$<>8__locals1.state.RefreshSend();
              });
            }
            clickyInkBlankLine2.TextChanged += action;
            return clickyInkBlankLine2;
          });
          InkViews.Hold<ClickyInkBlankLine>(stackPanel, clickyInkBlankLine);
        }
        else
        {
          ClickyInkForm.<>c__DisplayClass6_1 CS$<>8__locals2 = new ClickyInkForm.<>c__DisplayClass6_1();
          CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
          CS$<>8__locals2.choose = null;
          CS$<>8__locals2.choose = new ClickyPaperGelButton("Choose " + CS$<>8__locals2.CS$<>8__locals1.hint.Lowered() + "…", delegate
          {
            ClickyInkForm.<>c__DisplayClass6_1.<<Field>b__0>d <<Field>b__0>d;
            <<Field>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
            <<Field>b__0>d.<>4__this = CS$<>8__locals2;
            <<Field>b__0>d.<>1__state = -1;
            <<Field>b__0>d.<>t__builder.Start<ClickyInkForm.<>c__DisplayClass6_1.<<Field>b__0>d>(ref <<Field>b__0>d);
          }, ClickyPaperGel.White, 25.0, 11.0, 12.0, "paperclip");
          clickyInkHStack.Children.Add(CS$<>8__locals2.choose);
          string valueOrDefault = CS$<>8__locals2.CS$<>8__locals1.state.Values.GetValueOrDefault(CS$<>8__locals2.CS$<>8__locals1.item.Id);
          if (valueOrDefault != null && valueOrDefault.Length > 0)
          {
            clickyInkHStack.Children.Add(ClickyInkHStack.Flexible<InkMiddleTruncatedText>(new InkMiddleTruncatedText(Path.GetFileName(valueOrDefault.TrimEnd(new char[] { '\\', '/' })), InkObjectFonts.Mono(12.0, null), ClickyInkForm.Ink)));
          }
          stackPanel.Children.Add(clickyInkHStack);
        }
      }
      else
      {
        ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
        {
          Spacing = 14.0,
          LineSpacing = 6.0
        };
        for (int i = 0; i < list.Count; i++)
        {
          clickyNotebookFlow.Children.Add(ClickyInkForm.Choice(CS$<>8__locals1.item, list[i], i, style, CS$<>8__locals1.state));
        }
        stackPanel.Children.Add(clickyNotebookFlow);
      }
      return stackPanel;
    }

    // Token: 0x06001A40 RID: 6720 RVA: 0x005DEB34 File Offset: 0x005DEB34
    private static FrameworkElement Choice(ClickyInkLiveNote.Item item, string choice, int index, ClickyInkStyle style, ClickyInkForm.State state)
    {
      bool isChosen = state.Values.GetValueOrDefault(item.Id) == choice;
      double num = isChosen > false;
      ValueTuple<string, double> valueTuple;
      if (state.ChoiceChanges.TryGetValue(item.Id, out valueTuple))
      {
        double num2 = InkSpring.Value(0.25, 0.75, InkObjectState.Now - valueTuple.Item2);
        if (isChosen)
        {
          num = num2;
        }
        else if (valueTuple.Item1 == choice)
        {
          num = 1.0 - num2;
        }
      }
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(index + 2, 0.08), new PaperStroke(ClickyInkForm.Ink.Opacity(0.55), 1.2, PenLineCap.Flat, PenLineJoin.Miter, null));
      Grid grid = new Grid
      {
        Width = 14.0,
        Height = 14.0,
        Children = { clickyShapeView }
      };
      if (num > 0.001)
      {
        Ellipse ellipse = new Ellipse
        {
          Width = 8.0,
          Height = 8.0,
          Fill = Ui.Brush(style.Fresh),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
        ellipse.Transformed(0.0, 0.0, 0.0, Math.Max(0.0, num), null);
        grid.Children.Add(ellipse);
      }
      InkTextView inkTextView = InkViews.Plain(choice, ClickyPaperFonts.Typed(13.0, new FontWeight?(isChosen ? FontWeights.SemiBold : FontWeights.Normal)), ClickyInkForm.Ink, null);
      Pressable pressable = InkViews.PlainButton(new ClickyInkHStack(6.0, InkVerticalAlignment.Center, new UIElement[] { grid, inkTextView }), delegate
      {
        string valueOrDefault = state.Values.GetValueOrDefault(item.Id, "");
        state.Values[item.Id] = (isChosen ? "" : choice);
        state.ChoiceChanges[item.Id] = new ValueTuple<string, double>(valueOrDefault, InkObjectState.Now);
        state.RefreshSend();
        state.Animate(InkSpring.SettleSeconds(0.25, 0.75));
      }, choice);
      if (isChosen)
      {
        AutomationProperties.SetItemStatus(pressable, "selected");
      }
      return pressable;
    }

    // Token: 0x04000CA3 RID: 3235
    private static readonly Color Ink = PaperColor.Hex("#2E2A24");

    // Token: 0x0200080C RID: 2060
    [NullableContext(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x170007F0 RID: 2032
      // (get) Token: 0x060037F3 RID: 14323 RVA: 0x00685CF0 File Offset: 0x00685CF0
      public bool HasAnything
      {
        get
        {
          return this.Values.Values.Any<string>((string v) => v.TrimWs().Length > 0);
        }
      }

      // Token: 0x060037F4 RID: 14324 RVA: 0x00685D24 File Offset: 0x00685D24
      public void RefreshSend()
      {
        if (this.Send == null)
        {
          return;
        }
        this.Send.Enabled = this.HasAnything;
        this.Send.Opacity = (this.HasAnything ? 1.0 : 0.5);
      }

      // Token: 0x0400225E RID: 8798
      [Nullable(1)]
      public readonly Dictionary<string, string> Values = new Dictionary<string, string>();

      // Token: 0x0400225F RID: 8799
      [TupleElementNames(new string[] { "Previous", "At" })]
      [Nullable(new byte[] { 1, 1, 0, 1 })]
      public readonly Dictionary<string, ValueTuple<string, double>> ChoiceChanges = new Dictionary<string, ValueTuple<string, double>>();

      // Token: 0x04002260 RID: 8800
      [Nullable(2)]
      public ClickyPaperGelButton Send;
    }
  }
}
