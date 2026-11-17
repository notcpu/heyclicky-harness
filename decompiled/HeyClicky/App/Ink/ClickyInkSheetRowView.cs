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
  // Token: 0x02000298 RID: 664
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSheetRowView
  {
    // Token: 0x06001B90 RID: 7056 RVA: 0x005F1EBC File Offset: 0x005F1EBC
    public static FrameworkElement Make(ClickyInkSheetRowKind kind, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper, ClickyInkActions actions)
    {
      ClickyInkEnvironment clickyInkEnvironment = ClickyInkEnvironment.Current;
      bool isWindowPreview = clickyInkEnvironment.IsWindowPreview;
      double contentWidth = clickyInkEnvironment.ContentWidth;
      ClickyInkSheetRowKind.Title title = kind as ClickyInkSheetRowKind.Title;
      if (title != null)
      {
        InkTextView inkTextView = ClickyInkWordsView.Make(title.Words, clock, style, ClickyPaperFonts.Hand((double)(isWindowPreview ? 24 : 31)), true, null, null);
        inkTextView.Margin = new Thickness(0.0, 0.0, 0.0, (double)(isWindowPreview ? 4 : 14));
        return ClickyInkGrow.Around(inkTextView.Column(contentWidth), clock, title.Words.StartsAt.GetValueOrDefault() - 0.3, null);
      }
      ClickyInkSheetRowKind.PageName pageName = kind as ClickyInkSheetRowKind.PageName;
      if (pageName != null)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 9.0,
          Margin = new Thickness(0.0, (double)((pageName.Index == 0) ? 0 : 6), 0.0, 9.0)
        };
        if (pageName.Index > 0)
        {
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkSquiggle(pageName.PageID.CharacterCount()), new PaperStroke(style.Ink.Opacity(0.16), 1.1, PenLineCap.Flat, PenLineJoin.Miter, null));
          clickyShapeView.Width = contentWidth;
          clickyShapeView.Height = 6.0;
          stackPanel.Children.Add(clickyShapeView);
        }
        stackPanel.Children.Add(ClickyInkWordsView.Make(pageName.Name, clock, style, ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Bold)), true, new Color?(style.Quiet), null).With(delegate(InkTextView t)
        {
          t.Tracking = 1.1;
          t.Uppercase = true;
          t.LineLimit = new int?(1);
        }));
        return ClickyInkGrow.Around(stackPanel.Column(contentWidth), clock, pageName.LandsAt, pageName.VanishesAt);
      }
      ClickyInkSheetRowKind.Group group = kind as ClickyInkSheetRowKind.Group;
      if (group == null)
      {
        return new Grid();
      }
      ClickyInkSheetRowKind.Resize resized = group.Resized;
      if (resized != null)
      {
        FrameworkElement frameworkElement = ClickyInkSheetRowView.GroupView(group.Value, clock, style, paper, actions, isWindowPreview, contentWidth);
        FrameworkElement frameworkElement2 = ClickyInkSheetRowView.GroupView(resized.Before, resized.At - 0.001, style, paper, actions, isWindowPreview, contentWidth);
        frameworkElement2.Opacity = 0.0;
        frameworkElement2.IsHitTestVisible = false;
        ClickyInkHeightMorph clickyInkHeightMorph = new ClickyInkHeightMorph(ClickyInkClock.Progress(clock, resized.At, resized.At + 0.4), frameworkElement, frameworkElement2);
        ClickyInkBottomClip.Apply(clickyInkHeightMorph);
        return ClickyInkGrow.Around(clickyInkHeightMorph, clock, group.Value.LandsAt, group.Value.VanishesAt);
      }
      return ClickyInkGrow.Around(ClickyInkSheetRowView.GroupView(group.Value, clock, style, paper, actions, isWindowPreview, contentWidth), clock, group.Value.LandsAt, group.Value.VanishesAt);
    }

    // Token: 0x06001B91 RID: 7057 RVA: 0x005F21BC File Offset: 0x005F21BC
    private static FrameworkElement GroupView(ClickyInkGroup group, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper, ClickyInkActions actions, bool isWindowPreview, double inkWidth)
    {
      bool flag = group.Kind == ClickyInkDocument.Kind.Answer;
      FrameworkElement frameworkElement = ClickyInkGroupView.Make(group, clock, style, paper, actions);
      double num = (double)(flag ? (isWindowPreview ? 8 : 10) : 0);
      FrameworkElement frameworkElement2 = new Grid
      {
        Padding = new Thickness(0.0, num, 0.0, num),
        Children = { frameworkElement }
      };
      if (flag)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Fill(ClickyShape.RoundedRectangle(10.0), PaperColor.Hex("#E7DBBE").Opacity(0.42));
        clickyShapeView.Margin = new Thickness(-12.0, 0.0, -12.0, 0.0);
        frameworkElement2 = new ClickyInkOverlay(frameworkElement2, Array.Empty<UIElement>()).Behind(clickyShapeView);
      }
      double num2 = (double)(flag ? (isWindowPreview ? 6 : 18) : (isWindowPreview ? 10 : 14));
      return new Grid
      {
        Width = inkWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(0.0, 0.0, 0.0, num2),
        Children = { frameworkElement2 }
      };
    }
  }
}
