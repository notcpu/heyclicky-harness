using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000239 RID: 569
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkEvent
  {
    // Token: 0x060019BB RID: 6587 RVA: 0x005D7A18 File Offset: 0x005D7A18
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, [Nullable(2)] ClickyInkActions actions = null)
    {
      ClickyInkEvent.State state = InkObjectState.For<ClickyInkEvent.State>(block, "event");
      return new InkObjectHost(state, () => ClickyInkEvent.Build(block, clock, style, state, actions));
    }

    // Token: 0x060019BC RID: 6588 RVA: 0x005D7A74 File Offset: 0x005D7A74
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkEvent.State state, [Nullable(2)] ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkDocument.Block source = block.Source;
      string text;
      if ((text = source["when"]) == null)
      {
        text = source["start"] ?? source["date"];
      }
      string text2 = text;
      DateTimeOffset? dateTimeOffset = ClickyInkEventDates.Date(text2);
      DateTimeOffset? dateTimeOffset2;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        dateTimeOffset2 = ClickyInkEventDates.End(source["end"], valueOrDefault);
      }
      else
      {
        dateTimeOffset2 = null;
      }
      DateTimeOffset? dateTimeOffset3 = dateTimeOffset2;
      bool flag = ClickyInkEventDates.IsDateOnly(text2);
      StackPanel stackPanel = InkViews.VStack(5.0, new UIElement[]
      {
        ClickyInkRubberStamp.TypedLabel("save the date", ClickyInkEvent.Gold),
        InkViews.Words(block.Words, clock, style, InkObjectFonts.Serif(16.0, new FontWeight?(FontWeights.SemiBold)), new Color?(ClickyInkEvent.Ink), true, null)
      });
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
        TextBlock textBlock = InkObjectFonts.Serif(12.5, null).Apply(new TextBlock
        {
          Text = ClickyInkEventText.When(valueOrDefault2, dateTimeOffset3, flag, null),
          FontStyle = FontStyle.Italic,
          Foreground = Ui.Brush(ClickyInkEvent.Ink.Opacity(0.7)),
          TextWrapping = TextWrapping.Wrap,
          LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
          LineHeight = 14.912500000000001
        });
        stackPanel.Children.Add(textBlock);
      }
      string text3 = source["where"] ?? source["place"];
      if (text3 != null && text3.Length > 0)
      {
        FrameworkElement frameworkElement = InkViews.Symbol("mappin.and.ellipse", 12.0, ClickyInkEvent.Ink.Opacity(0.7), false) ?? new Grid();
        double num = 5.0;
        InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
        UIElement[] array = new UIElement[2];
        array[0] = frameworkElement;
        array[1] = ClickyInkHStack.Flexible<InkTextView>(InkViews.Plain(ClickyInkNoteActions.Plain(text3), InkObjectFonts.Serif(12.0, null), ClickyInkEvent.Ink.Opacity(0.7), delegate(InkTextView t)
        {
          t.LineLimit = new int?(1);
        }));
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num, inkVerticalAlignment, array);
        stackPanel.Children.Add(clickyInkHStack);
      }
      if (dateTimeOffset != null)
      {
        ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(state.WasAdded ? "Opened in Calendar" : "Add to Calendar", delegate
        {
          ClickyInkEvent.AddToCalendar(block, state, actions);
        }, state.WasAdded ? ClickyPaperGel.Mint : ClickyPaperGel.Blue, 26.0, 13.0, 12.0, state.WasAdded ? "checkmark" : "calendar.badge.plus")
        {
          Margin = new Thickness(0.0, 5.0, 0.0, 0.0)
        };
        clickyPaperGelButton.Opacity = Math.Clamp(InkSpring.Value(0.3, 0.8, InkObjectState.Now - state.AddedAt), 0.0, 1.0);
        stackPanel.Children.Add(clickyPaperGelButton);
      }
      ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(16.0, InkVerticalAlignment.Top, new UIElement[]
      {
        ClickyInkEvent.CalendarPage(dateTimeOffset),
        ClickyInkHStack.Flexible<StackPanel>(stackPanel),
        new ClickyInkSpacer()
      })
      {
        Margin = new Thickness(20.0, 20.0, 20.0, 18.0)
      };
      InkPaperSheet inkPaperSheet = new InkPaperSheet("event-card", ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#FFFCF4")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.07), 2.0, 0.0, 1.0)
      }, null, null);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(ClickyShape.Rectangle, new PaperStroke(ClickyInkEvent.Gold.Opacity(0.45), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      clickyShapeView.Margin = new Thickness(6.0);
      ClickyShapeView clickyShapeView2 = ClickyShapeView.Stroke(ClickyShape.Rectangle, new PaperStroke(ClickyInkEvent.Gold.Opacity(0.25), 0.6, PenLineCap.Flat, PenLineJoin.Miter, null));
      clickyShapeView2.Margin = new Thickness(9.0);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(clickyInkHStack2, new UIElement[] { clickyShapeView, clickyShapeView2 }).Behind(inkPaperSheet);
      clickyInkOverlay.Width = contentWidth - 12.0;
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay.Transformed(0.0, 0.0, -0.25, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Padding = new Thickness(4.0, 0.0, 0.0, 0.0),
        Children = { clickyInkOverlay }
      };
    }

    // Token: 0x060019BD RID: 6589 RVA: 0x005D801C File Offset: 0x005D801C
    private static FrameworkElement CalendarPage(DateTimeOffset? start)
    {
      string text;
      string text2;
      string text3;
      if (start != null)
      {
        DateTimeOffset valueOrDefault = start.GetValueOrDefault();
        ValueTuple<string, string, string> valueTuple = ClickyInkEventText.Page(valueOrDefault, null);
        text = valueTuple.Item1;
        text2 = valueTuple.Item2;
        text3 = valueTuple.Item3;
      }
      else
      {
        text = "—";
        text2 = "?";
        text3 = "";
      }
      Color color = PaperColor.Hex("#2B2620");
      InkTextView inkTextView = InkViews.Plain(text, ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.ExtraBold)), PaperColor.White, delegate(InkTextView t)
      {
        t.Tracking = 1.0;
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Height = 21.0,
        Background = Ui.Brush(PaperColor.Hex("#DE5A52")),
        Children = { inkTextView }
      };
      InkTextView inkTextView2 = InkViews.Plain(text2, InkObjectFonts.Rounded(28.0, FontWeights.ExtraBold), color, null);
      inkTextView2.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView2.VerticalAlignment = VerticalAlignment.Center;
      Grid grid2 = new Grid
      {
        Height = 38.0,
        Children = { inkTextView2 }
      };
      InkTextView inkTextView3 = InkViews.Plain(text3, ClickyPaperFonts.Typed(9.0, new FontWeight?(FontWeights.SemiBold)), color.Opacity(0.55), delegate(InkTextView t)
      {
        t.Tracking = 0.8;
      });
      inkTextView3.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView3.VerticalAlignment = VerticalAlignment.Center;
      Grid grid3 = new Grid
      {
        Height = 15.0,
        Children = { inkTextView3 }
      };
      StackPanel stackPanel = InkViews.VStack(0.0, new UIElement[] { grid, grid2, grid3 });
      Grid grid4 = new Grid
      {
        Width = 62.0,
        CornerRadius = new CornerRadius(6.0),
        Background = Ui.Brush(PaperColor.White),
        Children = { stackPanel }
      };
      string text4 = "event-page";
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(6.0);
      PaperFill paperFill = PaperFill.Of(PaperColor.White);
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.08), 1.5, 0.0, 1.0)
      };
      InkPaperSheet inkPaperSheet = new InkPaperSheet(text4, clickyShape, paperFill, null, array, null, null);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 22.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Top
      };
      for (int i = 0; i < 2; i++)
      {
        Rectangle rectangle = new Rectangle
        {
          Width = 7.0,
          Height = 14.0,
          RadiusX = 3.5,
          RadiusY = 3.5,
          StrokeThickness = 2.4,
          Stroke = new LinearGradientBrush
          {
            StartPoint = new Point(0.5, 0.0),
            EndPoint = new Point(0.5, 1.0),
            GradientStops = 
            {
              new GradientStop
              {
                Color = PaperColor.Hex("#8E939C"),
                Offset = 0.0
              },
              new GradientStop
              {
                Color = PaperColor.Hex("#4E535C"),
                Offset = 1.0
              }
            }
          }
        };
        stackPanel2.Children.Add(rectangle);
      }
      stackPanel2.Transformed(0.0, -8.0, 0.0, 1.0, null);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(grid4, new UIElement[] { stackPanel2 }).Behind(inkPaperSheet);
      clickyInkOverlay.Width = 62.0;
      clickyInkOverlay.VerticalAlignment = VerticalAlignment.Top;
      clickyInkOverlay.Transformed(0.0, 0.0, -1.5, 1.0, null);
      clickyInkOverlay.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
      return clickyInkOverlay;
    }

    // Token: 0x060019BE RID: 6590 RVA: 0x005D84C8 File Offset: 0x005D84C8
    private static void AddToCalendar(ClickyInkLiveNote.Block block, ClickyInkEvent.State state, [Nullable(2)] ClickyInkActions actions)
    {
      ClickyInkDocument.Block source = block.Source;
      string text;
      if ((text = source["when"]) == null)
      {
        text = source["start"] ?? source["date"];
      }
      string text2 = text;
      DateTimeOffset? dateTimeOffset = ClickyInkEventDates.Date(text2);
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        DateTimeOffset? dateTimeOffset2 = ClickyInkEventDates.End(source["end"], valueOrDefault);
        string text3 = ClickyInkNoteActions.Plain(source.Text);
        string text4 = ClickyInkEventDates.Ics((text3.Length == 0) ? "Event" : text3, valueOrDefault, dateTimeOffset2, ClickyInkEventDates.IsDateOnly(text2), source["where"] ?? source["place"], source["note"] ?? source["url"]);
        try
        {
          string text5 = Path.Combine(Path.GetTempPath(), "clicky-events");
          Directory.CreateDirectory(text5);
          string text6 = Path.Combine(text5, Guid.NewGuid().ToString("N").Substring(0, 8)
            .ToUpperInvariant() + ".ics");
          File.WriteAllText(text6, text4, new UTF8Encoding(false));
          ShellOpen.Start(text6);
        }
        catch (Exception ex)
        {
          Log.Warn("ink", "add to calendar: " + ex.Message);
          return;
        }
        if (actions != null)
        {
          Action<string, ClickyInkDocument.Kind, JsonObject> track = actions.Track;
          string text7 = "event_added_to_calendar";
          ClickyInkDocument.Kind kind = ClickyInkDocument.Kind.Event;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["is_all_day"] = ClickyInkEventDates.IsDateOnly(text2);
          jsonObject["has_end"] = dateTimeOffset2 != null;
          jsonObject["was_added_before"] = state.WasAdded;
          track(text7, kind, jsonObject);
        }
        state.WasAdded = true;
        state.AddedAt = InkObjectState.Now;
        state.Changed();
        state.Animate(InkSpring.SettleSeconds(0.3, 0.8));
        return;
      }
    }

    // Token: 0x04000C6E RID: 3182
    private static readonly Color Ink = PaperColor.Hex("#3A2C1A");

    // Token: 0x04000C6F RID: 3183
    private static readonly Color Gold = PaperColor.Hex("#B89150");

    // Token: 0x020007E5 RID: 2021
    [NullableContext(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x040021DF RID: 8671
      public bool WasAdded;

      // Token: 0x040021E0 RID: 8672
      public double AddedAt = -1000.0;
    }
  }
}
