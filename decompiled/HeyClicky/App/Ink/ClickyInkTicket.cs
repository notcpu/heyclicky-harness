using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000266 RID: 614
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkTicket
  {
    // Token: 0x06001A99 RID: 6809 RVA: 0x005E40B8 File Offset: 0x005E40B8
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      string text = block.Source["from"];
      string text2 = block.Source["to"];
      double num = ClickyInkClock.Progress(clock, block.LandsAt + 0.15, block.LandsAt + 0.9);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 6.0,
        Width = 270.0,
        Padding = new Thickness(16.0, 12.0, 12.0, 12.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      string text3;
      if ((text3 = block.Source["what"]) == null && (text3 = block.Source["carrier"]) == null)
      {
        text3 = block.Source["kind"] ?? "ticket";
      }
      string text4 = text3.ToUpperInvariant();
      double num2 = 8.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
      UIElement[] array = new UIElement[2];
      array[0] = InkTextView.Plain(text4, ClickyPaperFonts.Typed(10.0, new FontWeight?(FontWeights.Bold)), ClickyInkTicket.Blue).With(delegate(InkTextView t)
      {
        t.Tracking = 1.2;
        t.LineLimit = new int?(1);
      });
      array[1] = new ClickyInkSpacer();
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num2, inkVerticalAlignment, array);
      TextBlock textBlock = ClickyInkIcon.Glyph(ClickyInkTicketRules.Icon(block.Source["kind"], text != null), 12.0, ClickyInkTicket.Blue, true);
      if (textBlock != null)
      {
        clickyInkHStack.Children.Add(textBlock);
      }
      stackPanel.Children.Add(clickyInkHStack);
      if (text != null && text2 != null)
      {
        PaperFont paperFont = ClickyInkObjectFonts.Rounded(28.0, FontWeights.ExtraBold);
        Grid grid = new Grid
        {
          Width = 70.0,
          Height = 1.5
        };
        grid.Children.Add(new Border
        {
          Background = Ui.Brush(ClickyInkTicket.Navy.Opacity(0.2))
        });
        grid.Children.Add(new Border
        {
          Width = 70.0 * num,
          HorizontalAlignment = HorizontalAlignment.Left,
          Background = Ui.Brush(ClickyInkTicket.Blue)
        });
        InkTextView inkTextView = InkTextView.Plain(ClickyInkTicketRules.Code(text2), paperFont, ClickyInkTicket.Navy);
        inkTextView.Opacity = ((num > 0.95) ? 1.0 : 0.15);
        ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(10.0, InkVerticalAlignment.FirstBaseline, new UIElement[]
        {
          InkTextView.Plain(ClickyInkTicketRules.Code(text), paperFont, ClickyInkTicket.Navy),
          grid.BaselineFromBottom(0.0),
          inkTextView
        });
        stackPanel.Children.Add(clickyInkHStack2);
        ClickyInkHStack clickyInkHStack3 = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
        {
          ClickyInkObjectKit.Line(ClickyInkTicketRules.Place(text), ClickyPaperFonts.Typed(10.5, null), ClickyInkTicket.Navy.Opacity(0.6), new int?(1)),
          new ClickyInkSpacer(),
          ClickyInkObjectKit.Line(ClickyInkTicketRules.Place(text2), ClickyPaperFonts.Typed(10.5, null), ClickyInkTicket.Navy.Opacity(0.6), new int?(1))
        })
        {
          Width = 190.0,
          HorizontalAlignment = HorizontalAlignment.Left
        };
        stackPanel.Children.Add(clickyInkHStack3);
      }
      else
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyInkObjectFonts.Rounded(19.0, FontWeights.ExtraBold), true, new Color?(ClickyInkTicket.Navy), null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        }));
      }
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 14.0
      };
      string text5 = block.Source["when"] ?? block.Source["date"];
      if (text5 != null)
      {
        stackPanel2.Children.Add(ClickyInkTicket.Field("WHEN", text5));
      }
      string text6 = block.Source["price"];
      if (text6 != null)
      {
        stackPanel2.Children.Add(ClickyInkTicket.Field("PRICE", text6));
      }
      stackPanel.Children.Add(stackPanel2);
      if (text != null && block.Words.Current.Length > 0)
      {
        stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(15.0), false, new Color?(ClickyInkTicket.Navy.Opacity(0.75)), null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
      }
      PaperPath paperPath = new PaperPath();
      for (double num3 = 8.0; num3 < 126.0; num3 += 7.0)
      {
        paperPath.MoveTo(0.0, num3).LineTo(0.0, num3 + 3.5);
      }
      PaperDrawing paperDrawing = new PaperDrawing().Stroke(paperPath, new PaperStroke(ClickyInkTicket.Navy.Opacity(0.3), 1.2, PenLineCap.Flat, PenLineJoin.Miter, null)).Sized(1.0, 130.0);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 8.0,
        Padding = new Thickness(14.0, 0.0, 14.0, 0.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      string text7 = block.Source["seat"] ?? block.Source["gate"];
      if (text7 != null)
      {
        stackPanel3.Children.Add(ClickyInkTicket.Field((block.Source["seat"] != null) ? "SEAT" : "GATE", text7));
      }
      else
      {
        stackPanel3.Children.Add(ClickyInkTicket.Field("ADMIT", "1"));
      }
      stackPanel3.Children.Add(ClickyInkBarcode.Make(block.Id.CharacterCount(), 64.0, 30.0, ClickyInkTicket.Navy.Opacity(0.8)));
      Grid grid2 = new Grid();
      grid2.Width = 370.0;
      grid2.Height = 130.0;
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(270.0)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(paperDrawing, 1);
      Grid.SetColumn(stackPanel3, 2);
      grid2.Children.Add(stackPanel);
      grid2.Children.Add(paperDrawing);
      grid2.Children.Add(stackPanel3);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(grid2, new UIElement[] { ClickyShapeView.Stroke(new ClickyInkTicketShape(), ClickyInkTicket.Navy.Opacity(0.12), 1.0) }).Behind(new ClickyInkShadowedShape(new ClickyInkTicketShape(), "ticket", new LinearGradientBrush
      {
        StartPoint = new Point(0.5, 0.0),
        EndPoint = new Point(0.5, 1.0),
        GradientStops = 
        {
          new GradientStop
          {
            Color = PaperColor.Hex("#EAF2FF"),
            Offset = 0.0
          },
          new GradientStop
          {
            Color = PaperColor.Hex("#DCE8FB"),
            Offset = 1.0
          }
        }
      }, new PaperShadow(PaperColor.Black.Opacity(0.13), 4.0, 0.0, 2.0), null));
      clickyInkOverlay.Transformed(0.0, 0.0, -0.8, 1.0, null);
      clickyInkOverlay.Margin = new Thickness(0.0, 4.0, 0.0, 4.0);
      return ClickyInkObjectKit.Leading(clickyInkOverlay, contentWidth);
    }

    // Token: 0x06001A9A RID: 6810 RVA: 0x005E49E4 File Offset: 0x005E49E4
    private static FrameworkElement Field(string label, string value)
    {
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 1.0;
      stackPanel.Children.Add(InkTextView.Plain(label, ClickyPaperFonts.Typed(8.5, new FontWeight?(FontWeights.Bold)), ClickyInkTicket.Navy.Opacity(0.45)).With(delegate(InkTextView t)
      {
        t.Tracking = 1.0;
        t.LineLimit = new int?(1);
      }));
      stackPanel.Children.Add(InkTextView.Plain(value, ClickyPaperFonts.Typed(12.5, new FontWeight?(FontWeights.SemiBold)), ClickyInkTicket.Navy).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      }));
      return stackPanel;
    }

    // Token: 0x04000CC1 RID: 3265
    private static readonly Color Navy = PaperColor.Hex("#16213A");

    // Token: 0x04000CC2 RID: 3266
    private static readonly Color Blue = PaperColor.Hex("#1F4FA8");
  }
}
