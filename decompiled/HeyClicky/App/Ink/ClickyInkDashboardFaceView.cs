using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200021A RID: 538
  public static class ClickyInkDashboardFaceView
  {
    // Token: 0x0600195F RID: 6495 RVA: 0x005D1800 File Offset: 0x005D1800
    [NullableContext(1)]
    public static FrameworkElement Make(ClickyInkLiveNote note, double clock, ClickyHomeSpreadPaper? paper = null, int? runNumber = null)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(clickyHomeSpreadPaper.Ink, clickyHomeSpreadPaper.Quiet, new Color?(clickyHomeSpreadPaper.Deep), note.LatestEdit(clock));
      ClickyInkLiveNote clickyInkLiveNote = note.FoldingStruckWords(2.5);
      ClickyInkDashboardBoard clickyInkDashboardBoard = new ClickyInkDashboardBoard(clickyInkLiveNote, clock, "This run");
      ClickyInkLiveNote.Block run = clickyInkDashboardBoard.Run;
      ClickyInkRunLine clickyInkRunLine = ((run != null) ? new ClickyInkRunLine(run.Source) : null);
      ClickyInkDashboardFaceView.<>c__DisplayClass1_0 CS$<>8__locals1;
      CS$<>8__locals1.layout = new Grid
      {
        RowSpacing = 7.0,
        Padding = new Thickness(14.0)
      };
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
      ClickyInkLiveNote.Words title = clickyInkLiveNote.Title;
      if (title != null)
      {
        clickyInkHStack.Children.Add(ClickyInkWordsView.Make(title, clock, clickyInkStyle, ClickyPaperFonts.Hand(21.0), false, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
          t.MinimumScale = 0.7;
        }));
      }
      clickyInkHStack.Children.Add(new ClickyInkSpacer());
      string text = ((clickyInkRunLine != null) ? clickyInkRunLine.Number : null) ?? ((runNumber != null) ? runNumber.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : null);
      if (text != null)
      {
        UIElementCollection children = clickyInkHStack.Children;
        ClickyInkLiveNote.Block run2 = clickyInkDashboardBoard.Run;
        children.Add((run2 != null) ? ClickyInkDashboardFormNeeds.RunStampMark(text, run2.Source, clock, run2.LandsAt, 9.0) : new ClickyPaperStamp(ClickyThingStatus.Watching, "RUN " + text, 9.0, -5.0));
      }
      ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(clickyInkHStack, false, ref CS$<>8__locals1);
      ClickyInkGroup quiet = clickyInkDashboardBoard.Quiet;
      if (quiet != null)
      {
        double num = 7.0;
        InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
        UIElement[] array = new UIElement[2];
        array[0] = ClickyInkDashboardFormNeeds.LampView(ClickyInkStatusState.Ok, 11.0, 2, 1.0);
        array[1] = ClickyInkWordsView.Make(quiet.Blocks[0].Words, clock, clickyInkStyle, ClickyPaperFonts.Hand(15.0), false, new Color?(ClickyPaperInk.DoneGreen), null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        });
        ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(new ClickyInkHStack(num, inkVerticalAlignment, array), false, ref CS$<>8__locals1);
      }
      else
      {
        ClickyInkGroup answer = clickyInkDashboardBoard.Answer;
        if (answer != null)
        {
          FrameworkElement frameworkElement = ClickyInkDashboardFormNeeds.LampView(clickyInkDashboardBoard.Overall, 11.0, 2, 1.0);
          frameworkElement.Margin = new Thickness(0.0, 3.0, 0.0, 0.0);
          double num2 = 7.0;
          InkVerticalAlignment inkVerticalAlignment2 = InkVerticalAlignment.Top;
          UIElement[] array2 = new UIElement[2];
          array2[0] = frameworkElement;
          array2[1] = ClickyInkWordsView.Make(answer.Blocks[0].Words, clock, clickyInkStyle, ClickyPaperFonts.Hand(15.0), false, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(2);
          });
          ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(new ClickyInkHStack(num2, inkVerticalAlignment2, array2), false, ref CS$<>8__locals1);
        }
      }
      if (clickyInkDashboardBoard.Strip.Count > 0)
      {
        Grid grid = new Grid
        {
          Padding = new Thickness(0.0, 5.0, 0.0, 5.0)
        };
        List<ClickyInkLiveNote.Block> list = clickyInkDashboardBoard.Strip.Take<ClickyInkLiveNote.Block>(3).ToList<ClickyInkLiveNote.Block>();
        for (int i = 0; i < list.Count; i++)
        {
          grid.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          FrameworkElement frameworkElement2 = ClickyInkMiniTile.Make(list[i], clock, clickyInkStyle, i);
          frameworkElement2.VerticalAlignment = VerticalAlignment.Top;
          FrameworkElement frameworkElement3 = frameworkElement2;
          if (i > 0)
          {
            Border border = new Border
            {
              Width = 1.0,
              Background = Ui.Brush(ClickyInkDashboardFormNeeds.Pencil.Opacity(0.25)),
              Margin = new Thickness(0.0, 2.0, 0.0, 2.0),
              HorizontalAlignment = HorizontalAlignment.Left
            };
            frameworkElement3 = new Grid
            {
              Children = { frameworkElement2, border }
            };
          }
          Grid.SetColumn(frameworkElement3, i);
          grid.Children.Add(frameworkElement3);
        }
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyInkRuledBox(3, 3.0), ClickyInkDashboardFormNeeds.Pencil.Opacity(0.45), 1.0);
        ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(new ClickyInkOverlay(grid, new UIElement[] { clickyShapeView }), false, ref CS$<>8__locals1);
      }
      ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(new Grid(), true, ref CS$<>8__locals1);
      ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(5.0, InkVerticalAlignment.Center, Array.Empty<UIElement>());
      ClickyInkDashboardBoard.ChangeCounts changes = clickyInkDashboardBoard.Changes;
      if (changes.New > 0)
      {
        UIElementCollection children2 = clickyInkHStack2.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.New);
        defaultInterpolatedStringHandler.AppendLiteral(" new");
        children2.Add(ClickyInkFormKit.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyPaperInk.DoneGreen));
      }
      if (changes.Updated > 0)
      {
        UIElementCollection children3 = clickyInkHStack2.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.Updated);
        defaultInterpolatedStringHandler.AppendLiteral(" updated");
        children3.Add(ClickyInkFormKit.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), clickyInkStyle.Fresh));
      }
      if (changes.Gone > 0)
      {
        UIElementCollection children4 = clickyInkHStack2.Children;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(changes.Gone);
        defaultInterpolatedStringHandler.AppendLiteral(" gone");
        children4.Add(ClickyInkFormKit.Chip(defaultInterpolatedStringHandler.ToStringAndClear(), ClickyInkDashboardFormNeeds.Down));
      }
      clickyInkHStack2.Children.Add(new ClickyInkSpacer());
      string text2 = ((clickyInkRunLine != null) ? clickyInkRunLine.Next : null);
      if (text2 != null)
      {
        clickyInkHStack2.Children.Add(ClickyInkFormKit.Text("next " + text2, ClickyPaperFonts.Hand(13.0), clickyInkStyle.Quiet, 0.0, 1.0, new int?(1)));
      }
      ClickyInkDashboardFaceView.<Make>g__AddRow|1_0(clickyInkHStack2, false, ref CS$<>8__locals1);
      Grid grid2 = new Grid();
      grid2.Width = ClickyInkDashboardFaceView.Size.Width;
      grid2.Height = ClickyInkDashboardFaceView.Size.Height;
      grid2.HorizontalAlignment = HorizontalAlignment.Left;
      grid2.VerticalAlignment = VerticalAlignment.Top;
      grid2.Children.Add(new PaperSurface(ClickyShape.RoundedRectangle(10.0), clickyHomeSpreadPaper.Fill, new double?(0.45)).WithShadow(PaperColor.Black.Opacity(0.3), 12.0, 0.0, 5.0));
      grid2.Children.Add(CS$<>8__locals1.layout);
      ClickyInkFormKit.WithoutPenRenderer(grid2);
      return grid2;
    }

    // Token: 0x06001961 RID: 6497 RVA: 0x005D1F80 File Offset: 0x005D1F80
    [NullableContext(1)]
    [CompilerGenerated]
    internal static void <Make>g__AddRow|1_0(FrameworkElement e, bool fills = false, ref ClickyInkDashboardFaceView.<>c__DisplayClass1_0 A_2)
    {
      A_2.layout.RowDefinitions.Add(new RowDefinition
      {
        Height = (fills ? new GridLength(1.0, GridUnitType.Star) : GridLength.Auto)
      });
      Grid.SetRow(e, A_2.layout.RowDefinitions.Count - 1);
      A_2.layout.Children.Add(e);
    }

    // Token: 0x04000C5A RID: 3162
    public static readonly Size Size = new Size(330f, 210f);
  }
}
