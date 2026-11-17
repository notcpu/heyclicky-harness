using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200023B RID: 571
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkFaceView : Grid
  {
    // Token: 0x060019C6 RID: 6598 RVA: 0x005D9144 File Offset: 0x005D9144
    public ClickyInkFaceView(ClickyInkLiveNote note, double clock, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ClickyInkActivity> activity = null, bool isWorking = true, ClickyHomeSpreadPaper? paper = null)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(clickyHomeSpreadPaper.Ink, clickyHomeSpreadPaper.Quiet, new Color?(clickyHomeSpreadPaper.Deep), note.LatestEdit(clock));
      base.Width = 330.0;
      base.Height = 210.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      base.Children.Add(new PaperSurface(ClickyShape.RoundedRectangle(10.0), clickyHomeSpreadPaper.Fill, new double?(0.45)).WithWash(PaperFill.Vertical(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Clear),
        new ValueTuple<double, Color>(1.0, PaperColor.Black.Opacity(0.06))
      }), (Size s) => new Rect(0.0, Math.Max(0.0, s.Height - 10.0), s.Width, Math.Min(10.0, s.Height)), true).WithShadow(PaperColor.Black.Opacity(0.3), 12.0, 0.0, 5.0));
      Grid grid = new Grid
      {
        Padding = new Thickness(16.0)
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      bool flag = false;
      ClickyInkLiveNote.Words title = note.Title;
      if (title != null)
      {
        grid.Children.Add(ClickyInkWordsView.Make(title, clock, clickyInkStyle, ClickyPaperFonts.Hand(23.0), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
        flag = true;
      }
      ClickyInkLiveNote.Block block = ClickyInkFaceView.Lead(note, clock);
      ClickyInkLiveNote.Block block2 = ((block != null) ? block.Resolved(clock) : null);
      if (block2 != null)
      {
        FrameworkElement frameworkElement = ClickyInkFaceView.LeadView(block2, clock, clickyInkStyle);
        if (flag)
        {
          frameworkElement.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
        }
        Grid.SetRow(frameworkElement, 1);
        grid.Children.Add(frameworkElement);
        flag = true;
      }
      if (flag)
      {
        grid.RowDefinitions[2].MinHeight = 8.0;
      }
      if (isWorking)
      {
        ClickyInkActivity clickyInkActivity = ((activity != null) ? activity.LastOrDefault<ClickyInkActivity>((ClickyInkActivity a) => a.At <= clock) : null);
        if (clickyInkActivity != null)
        {
          ClickyNotebookWritingPencil clickyNotebookWritingPencil = new ClickyNotebookWritingPencil(clickyHomeSpreadPaper).Transformed(0.0, 0.0, 0.0, 0.7, new Point?(new Point(0.0, 0.5)));
          double num = 6.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
          UIElement[] array = new UIElement[2];
          array[0] = new ClickyInkCenteredOverflow(clickyNotebookWritingPencil, 18.0);
          array[1] = ClickyInkHStack.Flexible<InkTextView>(InkTextView.Plain(clickyInkActivity.Text, ClickyPaperFonts.Hand(14.5), clickyInkStyle.Fresh).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num, inkVerticalAlignment, array);
          clickyInkHStack.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
          clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
          Grid.SetRow(clickyInkHStack, 3);
          grid.Children.Add(clickyInkHStack);
        }
      }
      base.Children.Add(grid);
    }

    // Token: 0x060019C7 RID: 6599 RVA: 0x005D9594 File Offset: 0x005D9594
    [return: Nullable(2)]
    private static ClickyInkLiveNote.Block Lead(ClickyInkLiveNote note, double clock)
    {
      List<ClickyInkLiveNote.Block> list = ((note.Pages.Count > 0) ? note.Pages[0].Blocks.Where<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.IsPresent(clock) && !b.IsStruck(clock)).ToList<ClickyInkLiveNote.Block>() : new List<ClickyInkLiveNote.Block>());
      ClickyInkLiveNote.Block block;
      if ((block = list.FirstOrDefault<ClickyInkLiveNote.Block>((ClickyInkLiveNote.Block b) => b.Source.Kind == ClickyInkDocument.Kind.Answer)) == null)
      {
        block = list.FirstOrDefault<ClickyInkLiveNote.Block>(delegate(ClickyInkLiveNote.Block b)
        {
          ClickyInkDocument.Kind kind = b.Source.Kind;
          return kind == ClickyInkDocument.Kind.Paragraph || kind - ClickyInkDocument.Kind.List <= 1;
        });
      }
      return block;
    }

    // Token: 0x060019C8 RID: 6600 RVA: 0x005D963C File Offset: 0x005D963C
    private static FrameworkElement LeadView(ClickyInkLiveNote.Block lead, double clock, ClickyInkStyle style)
    {
      if (lead.Source.Kind == ClickyInkDocument.Kind.Answer)
      {
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(10.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
        string text = lead.Source["big"];
        if (text != null && text.Length > 0)
        {
          InkTextView inkTextView = InkTextView.Plain(text, ClickyPaperFonts.BigNumber(26.0), style.Ink).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.6;
          });
          inkTextView.Margin = new Thickness(6.0, 0.0, 6.0, 0.0);
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(3, 0.16), style.Fresh.Opacity(0.75), 1.8);
          clickyShapeView.Margin = new Thickness(-3.0);
          clickyInkHStack.Children.Add(new ClickyInkOverlay(inkTextView, new UIElement[] { clickyShapeView }));
        }
        clickyInkHStack.Children.Add(ClickyInkHStack.Flexible<InkTextView>(ClickyInkWordsView.Make(lead.Words, clock, style, ClickyPaperFonts.Hand(16.0), false, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(3);
        })));
        clickyInkHStack.HorizontalAlignment = HorizontalAlignment.Left;
        return clickyInkHStack;
      }
      if (lead.Items.Count > 0)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 3.0
        };
        foreach (ClickyInkLiveNote.Item item in lead.Items.Take<ClickyInkLiveNote.Item>(3))
        {
          bool valueOrDefault = item.Source.IsChecked.GetValueOrDefault();
          FrameworkElement frameworkElement = ClickyInkIcon.Glyph(valueOrDefault ? "checkmark.square" : "square", 11.0, valueOrDefault ? ClickyPaperInk.DoneGreen : style.Quiet, false) ?? new Grid();
          double num = 6.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
          UIElement[] array = new UIElement[2];
          array[0] = frameworkElement;
          array[1] = ClickyInkHStack.Flexible<InkTextView>(ClickyInkWordsView.Make(item.Words, clock, style, ClickyPaperFonts.Hand(15.0), false, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
          ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(num, inkVerticalAlignment, array);
          clickyInkHStack2.HorizontalAlignment = HorizontalAlignment.Left;
          stackPanel.Children.Add(clickyInkHStack2);
        }
        return stackPanel;
      }
      return ClickyInkWordsView.Make(lead.Words, clock, style, ClickyPaperFonts.Hand(16.0), false, null, null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(3);
      });
    }
  }
}
