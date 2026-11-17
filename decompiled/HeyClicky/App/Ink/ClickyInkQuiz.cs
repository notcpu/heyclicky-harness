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
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000259 RID: 601
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkQuiz
  {
    // Token: 0x06001A4F RID: 6735 RVA: 0x005E0474 File Offset: 0x005E0474
    public static void SetForGallery(ClickyInkLiveNote.Block block, IEnumerable<string> revealedItemIDs, IReadOnlyDictionary<string, bool> grades)
    {
      ClickyInkQuiz.State state = InkObjectState.For<ClickyInkQuiz.State>(block, "quiz");
      foreach (string text in revealedItemIDs)
      {
        state.Revealed.Add(text);
        state.TurnedAt[text] = -1000.0;
      }
      foreach (KeyValuePair<string, bool> keyValuePair in grades)
      {
        string text2;
        bool flag;
        keyValuePair.Deconstruct(out text2, out flag);
        string text3 = text2;
        bool flag2 = flag;
        state.Grades[text3] = flag2;
        state.GradedAt[text3] = -1000.0;
      }
    }

    // Token: 0x06001A50 RID: 6736 RVA: 0x005E054C File Offset: 0x005E054C
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkQuiz.State state = InkObjectState.For<ClickyInkQuiz.State>(block, "quiz");
      return new InkObjectHost(state, () => ClickyInkQuiz.Build(block, clock, style, actions, state));
    }

    // Token: 0x06001A51 RID: 6737 RVA: 0x005E05A8 File Offset: 0x005E05A8
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, ClickyInkQuiz.State state)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(9.0, InkVerticalAlignment.Center, new UIElement[]
      {
        ClickyInkRubberStamp.Make("flashcards", new Color?(PaperColor.Hex("#2D5BE3")), -3.0),
        ClickyInkHStack.Flexible<InkTextView>(InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), null, true, null)),
        new ClickyInkSpacer()
      });
      if (state.Grades.Count > 0)
      {
        UIElementCollection children = clickyInkHStack.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(state.Right);
        defaultInterpolatedStringHandler.AppendLiteral("/");
        defaultInterpolatedStringHandler.AppendFormatted<int>(state.Grades.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" right");
        children.Add(InkViews.Plain(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperFonts.Hand(16.0), style.Fresh, null));
      }
      StackPanel stackPanel = InkViews.VStack(14.0, new UIElement[] { clickyInkHStack });
      int i;
      for (i = 0; i < block.Items.Count; i++)
      {
        stackPanel.Children.Add(ClickyInkQuiz.Card(block.Items[i], i + 1, i, clock, style, state, actions));
      }
      if (state.Grades.Count > 0)
      {
        DateTimeOffset? sentAt = state.SentAt;
        bool flag = sentAt != null;
        Func<ClickyInkLiveNote.Item, bool> <>9__2;
        ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(flag ? "Sent" : "Send my score", delegate
        {
          DateTimeOffset? sentAt2 = state.SentAt;
          if (sentAt2 != null)
          {
            return;
          }
          IEnumerable<ClickyInkLiveNote.Item> items = block.Items;
          Func<ClickyInkLiveNote.Item, bool> func;
          if ((func = <>9__2) == null)
          {
            func = (<>9__2 = delegate(ClickyInkLiveNote.Item i)
            {
              bool flag2;
              return state.Grades.TryGetValue(i.Id, out flag2) && !flag2;
            });
          }
          List<string> list = (from i in items.Where<ClickyInkLiveNote.Item>(func)
            select i.Source.Text).ToList<string>();
          Action<ClickyInkNoteAction> perform = actions.Perform;
          ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.QuizResults(block.Source, state.Right, state.Grades.Count, list);
          string text = "quiz_score_sent";
          string text2 = null;
          Dictionary<string, int> dictionary = new Dictionary<string, int>();
          dictionary["right"] = state.Right;
          dictionary["total"] = state.Grades.Count;
          dictionary["cards"] = block.Items.Count;
          perform(clickyInkNoteAction.ForAnalytics(text, text2, dictionary));
          state.SentAt = new DateTimeOffset?(DateTimeOffset.Now);
          state.Animate(InkSpring.SettleSeconds(0.3, 0.8));
        }, flag ? ClickyPaperGel.Mint : ClickyPaperGel.Blue, 28.0, 13.0, 12.5, flag ? "checkmark" : null);
        if (flag)
        {
          clickyPaperGelButton.IsHitTestVisible = false;
        }
        Pressable pressable = InkViews.PlainButton(InkViews.Plain("Shuffle back", ClickyPaperFonts.Hand(15.0), style.Quiet, null), delegate
        {
          Action<string, ClickyInkDocument.Kind, JsonObject> track = actions.Track;
          string text3 = "quiz_reset";
          ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Quiz;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["graded"] = state.Grades.Count;
          jsonObject["cards"] = block.Items.Count;
          track(text3, kind, jsonObject);
          foreach (string text4 in state.Revealed)
          {
            state.TurnedAt[text4] = InkObjectState.Now;
          }
          state.Revealed.Clear();
          state.Grades.Clear();
          state.GradedAt.Clear();
          state.SentAt = null;
          state.Animate(InkSpring.SettleSeconds(0.4, 0.85));
        }, null);
        stackPanel.Children.Add(new ClickyInkHStack(10.0, InkVerticalAlignment.Center, new UIElement[] { clickyPaperGelButton, pressable }));
      }
      stackPanel.Width = contentWidth;
      stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
      return stackPanel;
    }

    // Token: 0x06001A52 RID: 6738 RVA: 0x005E0848 File Offset: 0x005E0848
    private static FrameworkElement Card(ClickyInkLiveNote.Item item, int number, int index, double clock, ClickyInkStyle style, ClickyInkQuiz.State state, ClickyInkActions actions)
    {
      bool flag = state.Revealed.Contains(item.Id);
      bool flag3;
      bool? flag2 = (state.Grades.TryGetValue(item.Id, out flag3) ? new bool?(flag3) : null);
      string detail = item.Source.Detail;
      string text = ((detail != null) ? ClickyInkNoteActions.Plain(detail) : "");
      bool flag4 = text.Length > 0 && clock >= item.Words.FinishedAt;
      double num = flag > false;
      double num2;
      if (state.TurnedAt.TryGetValue(item.Id, out num2))
      {
        double num3 = InkSpring.Value(0.45, 0.8, InkObjectState.Now - num2);
        num = (flag ? num3 : (1.0 - num3));
      }
      FrameworkElement frameworkElement = ClickyInkQuiz.Front(item, number, clock, style);
      FrameworkElement frameworkElement2 = ClickyInkQuiz.Back(text, number);
      frameworkElement.Opacity = Math.Clamp(1.0 - num, 0.0, 1.0);
      frameworkElement2.Opacity = Math.Clamp(num, 0.0, 1.0);
      Grid grid = new Grid
      {
        Children = { frameworkElement, frameworkElement2 }
      };
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      if (num > 0.001 && num < 0.999)
      {
        grid.Projection = new PlaneProjection
        {
          RotationY = -180.0 * num
        };
        frameworkElement2.Projection = new PlaneProjection
        {
          RotationY = 180.0
        };
      }
      string text2 = "quiz-under";
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(4.0);
      PaperFill paperFill = PaperFill.Of(PaperColor.Hex("#FBF8F0"));
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.05), 1.0, 0.0, 0.5)
      };
      InkPaperSheet inkPaperSheet = new InkPaperSheet(text2, clickyShape, paperFill, null, array, null, null);
      inkPaperSheet.Transformed(2.0, 2.0, (index % 2 == 0) ? 0.9 : (-0.8), 1.0, null);
      List<UIElement> list = new List<UIElement>();
      bool flag5 = flag2 != null;
      double valueOrDefault = state.GradedAt.GetValueOrDefault(item.Id, -1000.0);
      if (flag5)
      {
        double num4 = Math.Clamp(InkSpring.Value(0.3, 0.7, InkObjectState.Now - valueOrDefault), 0.0, 1.2);
        FrameworkElement frameworkElement3 = ClickyInkRubberStamp.Make(flag2.GetValueOrDefault() ? "got it" : "missed", new Color?(flag2.GetValueOrDefault() ? ClickyPaperInk.DoneGreen : ClickyInkQuiz.MissedRed), (double)(flag2.GetValueOrDefault() ? 7 : (-6)));
        Grid grid2 = new Grid
        {
          HorizontalAlignment = HorizontalAlignment.Right,
          VerticalAlignment = VerticalAlignment.Top,
          Margin = new Thickness(0.0, 9.0, 10.0, 0.0),
          Opacity = Math.Clamp(num4, 0.0, 1.0),
          Children = { frameworkElement3 }
        };
        grid2.Transformed(0.0, 0.0, 0.0, 1.7 - 0.7 * num4, null);
        list.Add(grid2);
      }
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(grid, list.ToArray()).Behind(inkPaperSheet);
      FrameworkElement frameworkElement4 = clickyInkOverlay;
      if (flag4)
      {
        Pressable pressable = InkViews.PlainButton(clickyInkOverlay, delegate
        {
          if (!state.Revealed.Contains(item.Id))
          {
            Action<string, ClickyInkDocument.Kind, JsonObject> track = actions.Track;
            string text3 = "quiz_card_revealed";
            ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Quiz;
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["card_index"] = index;
            track(text3, kind, jsonObject);
          }
          if (!state.Revealed.Remove(item.Id))
          {
            state.Revealed.Add(item.Id);
          }
          state.TurnedAt[item.Id] = InkObjectState.Now;
          state.Animate(InkSpring.SettleSeconds(0.45, 0.8));
        }, flag ? "Shows the question" : "Shows the answer");
        pressable.HorizontalAlignment = HorizontalAlignment.Stretch;
        frameworkElement4 = pressable;
      }
      StackPanel stackPanel = InkViews.VStack(8.0, new UIElement[] { frameworkElement4 });
      if (flag4)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 6.0,
          Margin = new Thickness(4.0, 0.0, 0.0, 0.0)
        };
        if (!flag)
        {
          stackPanel2.Children.Add(InkViews.Plain("tap to turn it over", ClickyPaperFonts.Hand(14.0), style.Quiet, null));
        }
        else if (flag2 == null)
        {
          stackPanel2.Children.Add(new ClickyPaperGelButton("Got it", delegate
          {
            base.<Card>g__Grade|1(true);
          }, ClickyPaperGel.Mint, 24.0, 10.0, 11.5, null));
          stackPanel2.Children.Add(new ClickyPaperGelButton("Missed it", delegate
          {
            base.<Card>g__Grade|1(false);
          }, ClickyPaperGel.White, 24.0, 10.0, 11.5, null));
        }
        stackPanel.Children.Add(stackPanel2);
      }
      stackPanel.Transformed(0.0, 0.0, (index % 2 == 0) ? (-0.3) : 0.25, 1.0, null);
      return stackPanel;
    }

    // Token: 0x06001A53 RID: 6739 RVA: 0x005E0E54 File Offset: 0x005E0E54
    private static FrameworkElement Front(ClickyInkLiveNote.Item item, int number, double clock, ClickyInkStyle style)
    {
      double num = 8.0;
      UIElement[] array = new UIElement[2];
      int num2 = 0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
      defaultInterpolatedStringHandler.AppendLiteral("Q");
      defaultInterpolatedStringHandler.AppendFormatted<int>(number);
      array[num2] = InkViews.Plain(defaultInterpolatedStringHandler.ToStringAndClear(), InkObjectFonts.Rounded(12.0, FontWeights.ExtraBold), ClickyInkQuiz.Red, null);
      array[1] = InkViews.Words(item.Words, clock, style, ClickyPaperFonts.Typed(14.5, new FontWeight?(FontWeights.Medium)), new Color?(ClickyInkQuiz.Ink), true, null);
      StackPanel stackPanel = InkViews.VStack(num, array);
      stackPanel.Margin = new Thickness(15.0, 12.0, 15.0, 12.0);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("quiz-front", ClickyShape.RoundedRectangle(4.0), PaperFill.Of(PaperColor.Hex("#FFFEF9")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.13), 2.5, 0.0, 1.5)
      }, null, null);
      Border border = new Border
      {
        Height = 1.0,
        Background = Ui.Brush(ClickyInkQuiz.Red.Opacity(0.4)),
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 30.0, 0.0, 0.0),
        IsHitTestVisible = false
      };
      return new Grid
      {
        MinHeight = 74.0,
        Children = { inkPaperSheet, border, stackPanel }
      };
    }

    // Token: 0x06001A54 RID: 6740 RVA: 0x005E103C File Offset: 0x005E103C
    private static FrameworkElement Back(string answer, int number)
    {
      double num = 6.0;
      UIElement[] array = new UIElement[2];
      int num2 = 0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
      defaultInterpolatedStringHandler.AppendLiteral("A");
      defaultInterpolatedStringHandler.AppendFormatted<int>(number);
      array[num2] = InkViews.Plain(defaultInterpolatedStringHandler.ToStringAndClear(), InkObjectFonts.Rounded(12.0, FontWeights.ExtraBold), ClickyPaperInk.DoneGreen, null);
      array[1] = InkViews.Plain(answer, ClickyPaperFonts.Hand(17.0), ClickyInkQuiz.Ink, null);
      StackPanel stackPanel = InkViews.VStack(num, array);
      stackPanel.Margin = new Thickness(15.0, 12.0, 15.0, 12.0);
      InkPaperSheet inkPaperSheet = new InkPaperSheet("quiz-back", ClickyShape.RoundedRectangle(4.0), PaperFill.Of(PaperColor.Hex("#FFF7D6")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.13), 2.5, 0.0, 1.5)
      }, null, null);
      return new Grid
      {
        MinHeight = 74.0,
        Children = { inkPaperSheet, stackPanel }
      };
    }

    // Token: 0x04000CA6 RID: 3238
    private static readonly Color Ink = PaperColor.Hex("#2B2620");

    // Token: 0x04000CA7 RID: 3239
    private static readonly Color Red = PaperColor.Hex("#D9453B");

    // Token: 0x04000CA8 RID: 3240
    private static readonly Color MissedRed = PaperColor.Hex("#C2410C");

    // Token: 0x02000819 RID: 2073
    [Nullable(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x170007F1 RID: 2033
      // (get) Token: 0x06003816 RID: 14358 RVA: 0x0068626C File Offset: 0x0068626C
      public int Right
      {
        get
        {
          return this.Grades.Values.Count<bool>((bool g) => g);
        }
      }

      // Token: 0x04002292 RID: 8850
      public readonly HashSet<string> Revealed = new HashSet<string>();

      // Token: 0x04002293 RID: 8851
      public readonly Dictionary<string, double> TurnedAt = new Dictionary<string, double>();

      // Token: 0x04002294 RID: 8852
      public readonly Dictionary<string, bool> Grades = new Dictionary<string, bool>();

      // Token: 0x04002295 RID: 8853
      public readonly Dictionary<string, double> GradedAt = new Dictionary<string, double>();

      // Token: 0x04002296 RID: 8854
      public DateTimeOffset? SentAt;
    }
  }
}
