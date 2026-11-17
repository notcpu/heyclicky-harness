using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000241 RID: 577
  public static class ClickyInkGallery
  {
    // Token: 0x060019DD RID: 6621 RVA: 0x005DABBC File Offset: 0x005DABBC
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkSentAction clickyInkSentAction = actions.Feedback.AnswerFor(block.Source);
      int num = ClickyInkGalleryRules.Columns(block.Items.Count);
      double num2 = ClickyInkGalleryRules.TileWidth(contentWidth, num);
      StackPanel stackPanel = InkViews.VStack(12.0, Array.Empty<UIElement>());
      if (block.Words.Current.Length > 0)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(9.0, InkVerticalAlignment.Center, new UIElement[] { ClickyInkHStack.Flexible<InkTextView>(InkViews.Words(block.Words, clock, style, ClickyPaperFonts.Hand(19.0), null, true, null)) });
        if (clickyInkSentAction == null)
        {
          if (!block.Items.Any<ClickyInkLiveNote.Item>((ClickyInkLiveNote.Item i) => i.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Pick))
          {
            clickyInkHStack.Children.Add(ClickyInkRubberStamp.Make("pick one", new Color?(PaperColor.Hex("#2D5BE3")), -4.0));
          }
        }
        stackPanel.Children.Add(clickyInkHStack);
      }
      Grid grid = new Grid
      {
        ColumnSpacing = 16.0,
        RowSpacing = 20.0,
        Margin = new Thickness(4.0, 6.0, 0.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      for (int k = 0; k < num; k++)
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(num2)
        });
      }
      for (int j = 0; j < block.Items.Count; j++)
      {
        if (j % num == 0)
        {
          grid.RowDefinitions.Add(new RowDefinition
          {
            Height = GridLength.Auto
          });
        }
        ClickyInkLiveNote.Item item = block.Items[j];
        FrameworkElement frameworkElement = ClickyInkGalleryTile.Make(block, item, j, num2, style, actions, item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Pick, clickyInkSentAction == null && item.Source.Mark.GetValueOrDefault() != ClickyInkDocument.Mark.Gone, delegate
        {
          ClickyInkNoteAction clickyInkNoteAction = ClickyInkNoteActions.Pick(block.Source, ClickyInkGalleryRules.PickCaption(item.Source));
          if (clickyInkNoteAction != null)
          {
            actions.Perform(clickyInkNoteAction);
          }
        });
        frameworkElement.VerticalAlignment = VerticalAlignment.Top;
        Grid.SetRow(frameworkElement, j / num);
        Grid.SetColumn(frameworkElement, j % num);
        grid.Children.Add(frameworkElement);
      }
      stackPanel.Children.Add(grid);
      stackPanel.Width = contentWidth;
      stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
      return stackPanel;
    }
  }
}
