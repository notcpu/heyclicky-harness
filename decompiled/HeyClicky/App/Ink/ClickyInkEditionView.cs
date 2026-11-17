using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
  // Token: 0x0200023D RID: 573
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkEditionView : Grid
  {
    // Token: 0x060019CC RID: 6604 RVA: 0x005D9A54 File Offset: 0x005D9A54
    public ClickyInkEditionView(ClickyInkLiveNote note, double clock, int edition, string dateLine, ClickyHomeSpreadPaper? paper = null)
    {
      ClickyHomeSpreadPaper clickyHomeSpreadPaper = paper ?? ClickyHomeSpreadPaper.Note;
      ClickyInkStyle clickyInkStyle = new ClickyInkStyle(PaperColor.Hex("#1E1B16"), PaperColor.Hex("#6B655A"), new Color?(ClickyPaperInk.ClickyBlue), note.LatestEdit(clock));
      List<ClickyInkGroup> list = note.Pages.SelectMany<ClickyInkLiveNote.Page, ClickyInkGroup>((ClickyInkLiveNote.Page page) => ClickyInkGroup.Groups(page, clock)).ToList<ClickyInkGroup>();
      ClickyInkGroup headline = list.FirstOrDefault<ClickyInkGroup>((ClickyInkGroup g) => g.Kind == ClickyInkDocument.Kind.Answer);
      List<ClickyInkGroup> list2 = list.Where<ClickyInkGroup>(delegate(ClickyInkGroup g)
      {
        string id = g.Id;
        ClickyInkGroup headline2 = headline;
        return id != ((headline2 != null) ? headline2.Id : null);
      }).ToList<ClickyInkGroup>();
      base.Width = 900.0;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      base.Children.Add(new PaperSurface(ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#F2EDE1")), new double?(0.55)).WithShadow(PaperColor.Black.Opacity(0.3), 12.0, 0.0, 5.0));
      StackPanel stackPanel = new StackPanel
      {
        Padding = new Thickness(26.0)
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 4.0,
        Margin = new Thickness(0.0, 0.0, 0.0, 14.0)
      };
      ClickyInkLiveNote.Words title = note.Title;
      InkTextView inkTextView = InkTextView.Plain((title != null) ? ClickyInkParser.Emphasis.Strip(title.Current) : "The Note", ClickyInkEditionView.Masthead, clickyInkStyle.Ink).With(delegate(InkTextView x)
      {
        x.LineLimit = new int?(1);
        x.MinimumScale = 0.6;
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel2.Children.Add(inkTextView);
      stackPanel2.Children.Add(ClickyInkEditionView.Rule(clickyInkStyle.Ink, 1.4));
      Grid grid = new Grid();
      foreach (GridLength gridLength in new GridLength[]
      {
        GridLength.Auto,
        new GridLength(1.0, GridUnitType.Star),
        GridLength.Auto,
        new GridLength(1.0, GridUnitType.Star),
        GridLength.Auto
      })
      {
        grid.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = gridLength
        });
      }
      string[] array2 = new string[3];
      int num = 0;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
      defaultInterpolatedStringHandler.AppendLiteral("EDITION ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(edition);
      array2[num] = defaultInterpolatedStringHandler.ToStringAndClear();
      array2[1] = dateLine.ToUpperInvariant();
      array2[2] = "BY CLICKY";
      string[] array3 = array2;
      for (int j = 0; j < array3.Length; j++)
      {
        InkTextView inkTextView2 = InkTextView.Plain(array3[j], ClickyPaperFonts.Typed(9.5, new FontWeight?(FontWeights.Bold)), clickyInkStyle.Quiet).With(delegate(InkTextView x)
        {
          x.Tracking = 1.4;
        });
        Grid.SetColumn(inkTextView2, j * 2);
        grid.Children.Add(inkTextView2);
      }
      stackPanel2.Children.Add(grid);
      stackPanel2.Children.Add(ClickyInkEditionView.Rule(clickyInkStyle.Ink, 0.8));
      Rectangle rectangle = ClickyInkEditionView.Rule(clickyInkStyle.Ink, 0.8);
      rectangle.Margin = new Thickness(0.0, 1.5, 0.0, 0.0);
      stackPanel2.Children.Add(rectangle);
      stackPanel.Children.Add(stackPanel2);
      if (headline != null)
      {
        ClickyInkLiveNote.Block block = headline.Blocks[0];
        double num2 = 16.0;
        InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Top;
        UIElement[] array4 = new UIElement[1];
        array4[0] = ClickyInkHStack.Flexible<InkTextView>(ClickyInkWordsView.Make(block.Words, clock, clickyInkStyle, ClickyInkEditionView.Headline, true, null, null).With(delegate(InkTextView x)
        {
          x.LineSpacing = 2.0;
        }));
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num2, inkVerticalAlignment, array4);
        string text = block.Source["big"];
        if (text != null && text.Length > 0)
        {
          StackPanel stackPanel3 = new StackPanel
          {
            Spacing = 2.0
          };
          InkTextView inkTextView3 = InkTextView.Plain(text, ClickyPaperFonts.BigNumber(36.0), clickyInkStyle.Ink);
          inkTextView3.HorizontalAlignment = HorizontalAlignment.Center;
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(5, 0.16), clickyInkStyle.Fresh.Opacity(0.75), 2.0);
          clickyShapeView.Margin = new Thickness(-5.0);
          stackPanel3.Children.Add(new ClickyInkOverlay(inkTextView3, new UIElement[] { clickyShapeView })
          {
            HorizontalAlignment = HorizontalAlignment.Center
          });
          string text2 = block.Source["note"];
          if (text2 != null)
          {
            InkTextView inkTextView4 = InkTextView.Plain(text2, ClickyPaperFonts.Hand(14.0), clickyInkStyle.Fresh);
            inkTextView4.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel3.Children.Add(inkTextView4);
          }
          clickyInkHStack.Children.Add(stackPanel3);
        }
        clickyInkHStack.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
        stackPanel.Children.Add(clickyInkHStack);
        Rectangle rectangle2 = ClickyInkEditionView.Rule(clickyInkStyle.Ink.Opacity(0.4), 0.8);
        rectangle2.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
        stackPanel.Children.Add(rectangle2);
      }
      Grid grid2 = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left
      };
      foreach (double num3 in new double[] { 415.0, 18.0, 0.8, 18.0, 415.0 })
      {
        grid2.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(num3)
        });
      }
      grid2.Margin = new Thickness(0.0, 0.0, -18.799999999999955, 0.0);
      FrameworkElement frameworkElement = ClickyInkEditionView.Column(list2.Where<ClickyInkGroup>((ClickyInkGroup _, int i) => i % 2 == 0), clock, clickyInkStyle, clickyHomeSpreadPaper);
      FrameworkElement frameworkElement2 = ClickyInkEditionView.Column(list2.Where<ClickyInkGroup>((ClickyInkGroup _, int i) => i % 2 == 1), clock, clickyInkStyle, clickyHomeSpreadPaper);
      Rectangle rectangle3 = new Rectangle
      {
        Fill = Ui.Brush(clickyInkStyle.Ink.Opacity(0.25)),
        Width = 0.8
      };
      Grid.SetColumn(rectangle3, 2);
      Grid.SetColumn(frameworkElement2, 4);
      grid2.Children.Add(frameworkElement);
      grid2.Children.Add(rectangle3);
      grid2.Children.Add(frameworkElement2);
      stackPanel.Children.Add(grid2);
      base.Children.Add(stackPanel);
    }

    // Token: 0x060019CD RID: 6605 RVA: 0x005DA24C File Offset: 0x005DA24C
    private static Rectangle Rule(Color color, double height)
    {
      return new Rectangle
      {
        Fill = Ui.Brush(color),
        Height = height,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };
    }

    // Token: 0x060019CE RID: 6606 RVA: 0x005DA270 File Offset: 0x005DA270
    private static FrameworkElement Column(IEnumerable<ClickyInkGroup> groups, double clock, ClickyInkStyle style, ClickyHomeSpreadPaper paper)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 12.0,
        Width = 415.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };
      foreach (ClickyInkGroup clickyInkGroup in groups)
      {
        stackPanel.Children.Add(ClickyInkGroupView.Make(clickyInkGroup, clock, style, paper, new ClickyInkActions()).Column(378.0));
      }
      return stackPanel;
    }

    // Token: 0x04000C74 RID: 3188
    private static readonly PaperFont Masthead = new PaperFont(new FontFamily("Georgia"), "Georgia", 38.0, FontWeights.Bold);

    // Token: 0x04000C75 RID: 3189
    private static readonly PaperFont Headline = new PaperFont(new FontFamily("Georgia"), "Georgia", 25.0, FontWeights.Bold);
  }
}
