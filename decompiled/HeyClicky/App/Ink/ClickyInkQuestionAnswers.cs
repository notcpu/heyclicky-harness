using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000254 RID: 596
  public static class ClickyInkQuestionAnswers
  {
    // Token: 0x06001A39 RID: 6713 RVA: 0x005DDFB8 File Offset: 0x005DDFB8
    [NullableContext(1)]
    public static bool HasAnswers(ClickyInkDocument.Block block)
    {
      return ClickyInkQuestionAnswerRules.HasAnswers(block);
    }

    // Token: 0x06001A3A RID: 6714 RVA: 0x005DDFC0 File Offset: 0x005DDFC0
    [NullableContext(1)]
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, ClickyInkActions actions)
    {
      ClickyInkQuestionAnswers.<>c__DisplayClass2_0 CS$<>8__locals1 = new ClickyInkQuestionAnswers.<>c__DisplayClass2_0();
      CS$<>8__locals1.actions = actions;
      ClickyInkQuestionAnswers.State state = InkObjectState.For<ClickyInkQuestionAnswers.State>(block, "question-answers");
      CS$<>8__locals1.source = block.Source;
      StackPanel stackPanel = InkViews.VStack(8.0, Array.Empty<UIElement>());
      List<string> list = ClickyInkQuestionAnswerRules.Options(CS$<>8__locals1.source);
      if (list.Count > 0)
      {
        ClickyNotebookFlow clickyNotebookFlow = new ClickyNotebookFlow
        {
          Spacing = 8.0,
          LineSpacing = 8.0
        };
        for (int i = 0; i < list.Count; i++)
        {
          string option = list[i];
          clickyNotebookFlow.Children.Add(new ClickyPaperGelButton(option, delegate
          {
            ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.Answer(CS$<>8__locals1.source, option, false);
            if (clickyInkNoteAction != null)
            {
              CS$<>8__locals1.actions.Perform(clickyInkNoteAction);
            }
          }, (i == 0) ? ClickyPaperGel.Blue : ClickyPaperGel.White, 28.0, 13.0, 12.5, null));
        }
        stackPanel.Children.Add(clickyNotebookFlow);
      }
      CS$<>8__locals1.placeholder = ClickyInkQuestionAnswerRules.TypedPlaceholder(CS$<>8__locals1.source);
      if (CS$<>8__locals1.placeholder != null)
      {
        ClickyInkInlineAnswerField clickyInkInlineAnswerField = state.Field<ClickyInkInlineAnswerField>("typed", () => new ClickyInkInlineAnswerField(CS$<>8__locals1.placeholder, "Send"));
        clickyInkInlineAnswerField.OnSend = delegate(string words)
        {
          ClickyInkNoteAction clickyInkNoteAction2 = ClickyInkNoteActions.Answer(CS$<>8__locals1.source, words, true);
          if (clickyInkNoteAction2 != null)
          {
            CS$<>8__locals1.actions.Perform(clickyInkNoteAction2);
          }
        };
        InkViews.Hold<ClickyInkInlineAnswerField>(stackPanel, clickyInkInlineAnswerField);
      }
      string text = CS$<>8__locals1.source["file"];
      if (text != null)
      {
        ClickyInkQuestionAnswers.<>c__DisplayClass2_2 CS$<>8__locals3 = new ClickyInkQuestionAnswers.<>c__DisplayClass2_2();
        CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals1;
        CS$<>8__locals3.button = null;
        CS$<>8__locals3.button = new ClickyPaperGelButton((text.Length == 0) ? "Choose file…" : text, delegate
        {
          ClickyInkQuestionAnswers.<>c__DisplayClass2_2.<<Make>b__3>d <<Make>b__3>d;
          <<Make>b__3>d.<>t__builder = AsyncVoidMethodBuilder.Create();
          <<Make>b__3>d.<>4__this = CS$<>8__locals3;
          <<Make>b__3>d.<>1__state = -1;
          <<Make>b__3>d.<>t__builder.Start<ClickyInkQuestionAnswers.<>c__DisplayClass2_2.<<Make>b__3>d>(ref <<Make>b__3>d);
        }, ClickyPaperGel.White, 28.0, 13.0, 12.5, "paperclip");
        stackPanel.Children.Add(CS$<>8__locals3.button);
      }
      return stackPanel;
    }

    // Token: 0x02000808 RID: 2056
    private sealed class State : InkObjectState
    {
    }
  }
}
