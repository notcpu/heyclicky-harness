using System;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200026F RID: 623
  public static class ClickyInkQuestion
  {
    // Token: 0x06001AAB RID: 6827 RVA: 0x005E5D84 File Offset: 0x005E5D84
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        Padding = new Thickness(13.0)
      };
      stackPanel.Children.Add(new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
      {
        InkTextView.Plain("needs you", ClickyPaperFonts.Hand(17.0), ClickyInkQuestion.Amber),
        ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Typed(14.0, new FontWeight?(FontWeights.SemiBold)), true, null, null)
      }));
      string text = block.Source["since"] ?? block.Source["asked"];
      if (text != null && text.Length > 0)
      {
        stackPanel.Children.Add(InkTextView.Plain("waiting since " + text, ClickyPaperFonts.Hand(14.0), ClickyInkQuestion.Amber.Opacity(0.75)));
      }
      if (block.Source["yes"] != null || block.Source["no"] != null)
      {
        StackPanel stackPanel2 = new StackPanel
        {
          Orientation = Orientation.Horizontal,
          Spacing = 8.0
        };
        string yes = block.Source["yes"];
        if (yes != null)
        {
          stackPanel2.Children.Add(new ClickyPaperGelButton(block.Source["yes_label"] ?? "Yes", delegate
          {
            actions.Send(yes);
          }, ClickyPaperGel.Blue, 28.0, 13.0, 12.5, null));
        }
        string no = block.Source["no"];
        if (no != null)
        {
          stackPanel2.Children.Add(new ClickyPaperGelButton(block.Source["no_label"] ?? "No", delegate
          {
            actions.Send(no);
          }, ClickyPaperGel.White, 28.0, 13.0, 12.5, null));
        }
        stackPanel.Children.Add(stackPanel2);
      }
      if (ClickyInkQuestionAnswers.HasAnswers(block.Source))
      {
        stackPanel.Children.Add(ClickyInkQuestionAnswers.Make(block, actions));
      }
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = 
        {
          ClickyShapeView.Fill(ClickyShape.RoundedRectangle(10.0), PaperColor.Hex("#FFE9B0").Opacity(0.7)),
          stackPanel,
          ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(10.0), new PaperStroke(PaperColor.Hex("#E0A030").Opacity(0.6), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null))
        }
      };
    }

    // Token: 0x04000CCB RID: 3275
    private static readonly Color Amber = PaperColor.Hex("#8A5300");
  }
}
