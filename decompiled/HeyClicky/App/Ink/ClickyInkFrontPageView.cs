using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
  // Token: 0x0200023F RID: 575
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkFrontPageView
  {
    // Token: 0x17000482 RID: 1154
    // (get) Token: 0x060019D7 RID: 6615 RVA: 0x005DAA68 File Offset: 0x005DAA68
    public static Size SpreadSize
    {
      get
      {
        return ClickyInkFrontPagePlanner.SpreadSize;
      }
    }

    // Token: 0x060019D8 RID: 6616 RVA: 0x005DAA70 File Offset: 0x005DAA70
    public static ClickyInkDashboardBoard Board(ClickyInkLiveNote note, double clock)
    {
      return ClickyInkDashboardBoard.ForFrontPage(note, clock);
    }

    // Token: 0x060019D9 RID: 6617 RVA: 0x005DAA7C File Offset: 0x005DAA7C
    public static FrameworkElement Make(ClickyInkLiveNote note, double clock, ClickyInkFrontPageVariant variant = ClickyInkFrontPageVariant.Broadsheet, ClickyHomeSpreadPaper? paper = null, [Nullable(2)] ClickyInkActions actions = null, Size? size = null, int? runNumber = null, [Nullable(2)] string dateLine = null)
    {
      return new ClickyInkFrontPageView.Builder(note, clock, variant, paper ?? ClickyHomeSpreadPaper.Note, actions ?? new ClickyInkActions(), size ?? ClickyInkFrontPageView.SpreadSize, runNumber, dateLine).Body();
    }

    // Token: 0x020007F9 RID: 2041
    [Nullable(0)]
    private sealed class Builder
    {
      // Token: 0x060037A0 RID: 14240 RVA: 0x00681F94 File Offset: 0x00681F94
      public Builder(ClickyInkLiveNote rawNote, double clock, ClickyInkFrontPageVariant variant, ClickyHomeSpreadPaper paper, ClickyInkActions actions, Size size, int? runNumber, [Nullable(2)] string dateLine)
      {
      }

      // Token: 0x170007E9 RID: 2025
      // (get) Token: 0x060037A1 RID: 14241 RVA: 0x00681FF4 File Offset: 0x00681FF4
      private ClickyInkStyle Style
      {
        get
        {
          ClickyInkStyle clickyInkStyle;
          if ((clickyInkStyle = this.style) == null)
          {
            clickyInkStyle = (this.style = new ClickyInkStyle(this.<paper>P.Ink, this.<paper>P.Quiet, new Color?(this.<paper>P.Deep), this.note.LatestEdit(this.<clock>P)));
          }
          return clickyInkStyle;
        }
      }

      // Token: 0x170007EA RID: 2026
      // (get) Token: 0x060037A2 RID: 14242 RVA: 0x00682050 File Offset: 0x00682050
      private bool IsMission
      {
        get
        {
          return this.<variant>P == ClickyInkFrontPageVariant.MissionControl;
        }
      }

      // Token: 0x170007EB RID: 2027
      // (get) Token: 0x060037A3 RID: 14243 RVA: 0x0068205C File Offset: 0x0068205C
      private bool IsPenWriting
      {
        get
        {
          return ClickyInkPenSchedule.Of(this.note).IsWriting(this.<clock>P);
        }
      }

      // Token: 0x060037A4 RID: 14244 RVA: 0x00682074 File Offset: 0x00682074
      public FrameworkElement Body()
      {
        ClickyInkFrontPagePlanner.Plan plan = ClickyInkFrontPagePlans.For(this.note, this.<variant>P, this.<paper>P);
        ClickyInkDashboardBoard clickyInkDashboardBoard = ClickyInkFrontPageView.Board(this.note, this.<clock>P);
        ClickyInkFrontPageContent clickyInkFrontPageContent = new ClickyInkFrontPageContent(this.note, clickyInkDashboardBoard, plan, this.<clock>P);
        double num = Math.Min(this.<size>P.Width / plan.Canvas.Width, this.<size>P.Height / plan.Canvas.Height);
        ClickyInkFrontPageView.Builder.<>c__DisplayClass16_0 CS$<>8__locals1;
        CS$<>8__locals1.canvas = new Canvas
        {
          Width = plan.Canvas.Width,
          Height = plan.Canvas.Height
        };
        ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Sheet(plan), 0.0, 0.0, new double?(plan.Canvas.Width), new double?(plan.Canvas.Height), ref CS$<>8__locals1);
        ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Masthead(clickyInkDashboardBoard, plan), plan.Masthead.X, plan.Masthead.Y, new double?(plan.InnerWidth), new double?(plan.Masthead.Height), ref CS$<>8__locals1);
        ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Headline(clickyInkDashboardBoard, plan), plan.Headline.X, plan.Headline.Y, new double?(plan.Headline.Width), new double?(plan.Headline.Height), ref CS$<>8__locals1);
        Rect? rect = plan.Stats;
        if (rect != null)
        {
          Rect valueOrDefault = rect.GetValueOrDefault();
          ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Stats(clickyInkFrontPageContent.StatBlocks, valueOrDefault), valueOrDefault.X, valueOrDefault.Y, new double?(valueOrDefault.Width), new double?(valueOrDefault.Height), ref CS$<>8__locals1);
        }
        foreach (ClickyInkFrontPagePlanner.Band band in plan.Bands)
        {
          FrameworkElement frameworkElement = this.BandView(band, clickyInkFrontPageContent);
          if (frameworkElement != null)
          {
            ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(frameworkElement, band.Frame.X, band.Frame.Y, new double?(band.Frame.Width), new double?(band.Frame.Height), ref CS$<>8__locals1);
          }
        }
        rect = plan.Move;
        if (rect != null)
        {
          Rect valueOrDefault2 = rect.GetValueOrDefault();
          ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Move(clickyInkFrontPageContent.MoveGroups, valueOrDefault2), valueOrDefault2.X, valueOrDefault2.Y, new double?(valueOrDefault2.Width), null, ref CS$<>8__locals1);
        }
        if (!this.IsMission)
        {
          for (int i = 0; i < plan.Rules.Count; i++)
          {
            Rect rect2 = plan.Rules[i];
            ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(ClickyShapeView.Stroke(new ClickyInkRuledLine(new Point(0.5, 0.0), new Point(0.5, 1.0), 20 + i), PaperStroke.Round(this.<paper>P.Ink.Opacity(0.22), 1.0)), rect2.X, rect2.Y, new double?((double)1), new double?(Math.Max(0.0, rect2.Height)), ref CS$<>8__locals1);
          }
        }
        foreach (ClickyInkFrontPagePlanner.Column column in plan.Flow)
        {
          StackPanel stackPanel = new StackPanel
          {
            Width = plan.ColumnWidth
          };
          foreach (ClickyInkFrontPagePlanner.Chunk chunk in column.Chunks)
          {
            stackPanel.Children.Add(this.ChunkView(chunk, clickyInkFrontPageContent, plan, column.ExtraGap));
          }
          ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(stackPanel, column.X, column.Top, null, null, ref CS$<>8__locals1);
        }
        ClickyInkFrontPageView.Builder.<Body>g__Place|16_0(this.Footer(clickyInkDashboardBoard, plan), plan.Footer.X, plan.Footer.Y, new double?(plan.InnerWidth), new double?(plan.Footer.Height), ref CS$<>8__locals1);
        CS$<>8__locals1.canvas.RenderTransform = new ScaleTransform
        {
          ScaleX = num,
          ScaleY = num
        };
        return new Grid
        {
          Width = this.<size>P.Width,
          Height = this.<size>P.Height,
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Top,
          Clip = new RectangleGeometry
          {
            Rect = new Rect(0.0, 0.0, this.<size>P.Width, this.<size>P.Height)
          },
          Children = { CS$<>8__locals1.canvas }
        };
      }

      // Token: 0x060037A5 RID: 14245 RVA: 0x00682608 File Offset: 0x00682608
      private FrameworkElement Sheet(ClickyInkFrontPagePlanner.Plan plan)
      {
        if (this.IsMission)
        {
          return new PaperSurface(ClickyShape.RoundedRectangle(8.0), this.<paper>P.Fill, new double?(0.45))
          {
            Washes = { ClickyInkFormKit.Dots(22.0, 1.0) }
          };
        }
        double middle = plan.Canvas.Width / 2.0;
        return new PaperSurface(ClickyShape.RoundedRectangle(8.0), PaperFill.Of(PaperColor.Hex("#F4EEE0")), new double?(0.6)).WithWash(PaperFill.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Clear),
          new ValueTuple<double, Color>(1.0, PaperColor.Black.Opacity(0.05))
        }), (Size s) => new Rect(middle - 35.0 - 5.0, 0.0, 40.0, s.Height), true).WithWash(PaperFill.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, PaperColor.Black.Opacity(0.07)),
          new ValueTuple<double, Color>(1.0, PaperColor.Clear)
        }), (Size s) => new Rect(middle + 5.0 - 5.0, 0.0, 30.0, s.Height), true);
      }

      // Token: 0x060037A6 RID: 14246 RVA: 0x00682780 File Offset: 0x00682780
      private FrameworkElement Masthead(ClickyInkDashboardBoard board, ClickyInkFrontPagePlanner.Plan plan)
      {
        ClickyInkLiveNote.Block run = board.Run;
        ClickyInkRunLine clickyInkRunLine = ((run != null) ? new ClickyInkRunLine(run.Source) : null);
        string text = ((clickyInkRunLine != null) ? clickyInkRunLine.Number : null) ?? ((this.<runNumber>P != null) ? this.<runNumber>P.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : null);
        bool flag;
        if (board.Run == null)
        {
          int? num = this.<runNumber>P;
          flag = num != null;
        }
        else
        {
          flag = true;
        }
        bool flag2 = flag;
        string text2 = ((clickyInkRunLine != null) ? clickyInkRunLine.RanAt : null) ?? this.<dateLine>P;
        if (this.IsMission)
        {
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(16.0, InkVerticalAlignment.Center, Array.Empty<UIElement>())
          {
            Margin = new Thickness(18.0, 0.0, 18.0, 0.0),
            VerticalAlignment = VerticalAlignment.Center
          };
          clickyInkHStack.Children.Add(this.LiveLight(board));
          ClickyInkLiveNote.Words title = this.note.Title;
          if (title != null)
          {
            clickyInkHStack.Children.Add(ClickyInkWordsView.Make(title, this.<clock>P, this.Style, ClickyPaperFonts.Hand(40.0), true, null, null).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
              t.MinimumScale = 0.6;
            }));
          }
          clickyInkHStack.Children.Add(ClickyInkFormKit.Text(flag2 ? "KEPT BY CLICKY" : "MADE BY CLICKY", ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.Bold)), this.Style.Quiet, 1.6, 1.0, new int?(1)));
          clickyInkHStack.Children.Add(new ClickyInkSpacer());
          FrameworkElement frameworkElement = ClickyInkFormKit.Changes(board.Changes, this.Style, 16.0, 6.0);
          if (frameworkElement != null)
          {
            clickyInkHStack.Children.Add(frameworkElement);
          }
          string text3 = ((clickyInkRunLine != null) ? clickyInkRunLine.Next : null);
          if (text3 != null)
          {
            clickyInkHStack.Children.Add(ClickyInkFormKit.Text("next run " + text3, ClickyPaperFonts.Hand(17.0), this.Style.Quiet, 0.0, 1.0, new int?(1)));
          }
          if (text2 != null)
          {
            clickyInkHStack.Children.Add(ClickyInkFormKit.Text(text2, ClickyPaperFonts.Typed(13.0, new FontWeight?(FontWeights.SemiBold)), this.Style.Ink.Opacity(0.7), 0.0, 1.0, new int?(1)));
          }
          FrameworkElement frameworkElement2 = this.Stamp(board, text, 15.0);
          if (frameworkElement2 != null)
          {
            clickyInkHStack.Children.Add(frameworkElement2);
          }
          return new Grid
          {
            Height = plan.Masthead.Height,
            Children = 
            {
              ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), ClickyInkDashboardFormNeeds.Lamp(board.Overall), 1),
              clickyInkHStack
            }
          };
        }
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 5.0
        };
        Grid grid = new Grid
        {
          Height = 64.0
        };
        ClickyInkLiveNote.Words title2 = this.note.Title;
        if (title2 != null)
        {
          InkTextView inkTextView = ClickyInkWordsView.Make(title2, this.<clock>P, this.Style, ClickyPaperFonts.Hand(58.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.5;
          });
          inkTextView.MaxWidth = plan.InnerWidth * 0.62;
          inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
          inkTextView.VerticalAlignment = VerticalAlignment.Center;
          grid.Children.Add(inkTextView);
        }
        StackPanel stackPanel2 = new StackPanel
        {
          Spacing = 3.0,
          VerticalAlignment = VerticalAlignment.Center
        };
        stackPanel2.Children.Add(ClickyInkFormKit.Text(flag2 ? "KEPT BY CLICKY" : "MADE BY CLICKY", ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.Bold)), this.Style.Quiet, 1.6, 1.0, new int?(1)));
        string text4 = ((clickyInkRunLine != null) ? clickyInkRunLine.Next : null);
        if (text4 != null)
        {
          stackPanel2.Children.Add(ClickyInkFormKit.Text(("NEXT RUN " + text4).ToUpperInvariant(), ClickyPaperFonts.Typed(10.0, new FontWeight?(FontWeights.Bold)), this.Style.Quiet, 1.2, 1.0, new int?(1)));
        }
        ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
        {
          stackPanel2,
          new ClickyInkSpacer()
        });
        FrameworkElement frameworkElement3 = this.Stamp(board, text, 14.0);
        if (frameworkElement3 != null)
        {
          clickyInkHStack2.Children.Add(frameworkElement3);
        }
        clickyInkHStack2.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(clickyInkHStack2);
        stackPanel.Children.Add(grid);
        stackPanel.Children.Add(this.Ruled(true));
        List<string> list = (from p in this.note.Pages
          select ClickyInkParser.Emphasis.Strip(p.Name.Current) into n
          where n.Length > 0
          select n).ToList<string>();
        string text5;
        if (!flag2)
        {
          text5 = "inside: " + string.Join(" · ", list);
        }
        else
        {
          text5 = string.Join("  ·  ", new string[]
          {
            text2,
            (clickyInkRunLine != null) ? clickyInkRunLine.Extra : null
          }.Where<string>((string s) => s != null));
        }
        string text6 = text5;
        ClickyInkHStack clickyInkHStack3 = new ClickyInkHStack(14.0, InkVerticalAlignment.Center, new UIElement[]
        {
          ClickyInkFormKit.Text(text6.ToUpperInvariant(), ClickyPaperFonts.Typed(11.0, new FontWeight?(FontWeights.Bold)), this.Style.Quiet, 1.3, 1.0, new int?(1)),
          new ClickyInkSpacer()
        })
        {
          VerticalAlignment = VerticalAlignment.Center
        };
        FrameworkElement frameworkElement4 = ClickyInkFormKit.Changes(board.Changes, this.Style, 16.0, 6.0);
        if (frameworkElement4 != null)
        {
          clickyInkHStack3.Children.Add(frameworkElement4);
        }
        stackPanel.Children.Add(new Grid
        {
          Height = 22.0,
          Children = { clickyInkHStack3 }
        });
        stackPanel.Children.Add(this.Ruled(false));
        return stackPanel;
      }

      // Token: 0x060037A7 RID: 14247 RVA: 0x00682EAC File Offset: 0x00682EAC
      [NullableContext(2)]
      private FrameworkElement Stamp([Nullable(1)] ClickyInkDashboardBoard board, string number, double fontSize)
      {
        ClickyInkLiveNote.Block run = board.Run;
        if (run != null)
        {
          return ClickyInkDashboardFormNeeds.RunStampMark(number, run.Source, this.<clock>P, run.LandsAt, fontSize);
        }
        if (number != null)
        {
          return new ClickyPaperStamp(ClickyThingStatus.Watching, "RUN " + number, fontSize, -5.0);
        }
        if (this.<clock>P >= this.note.FinishedClock - 0.6 && !this.IsPenWriting)
        {
          return new ClickyPaperStamp(ClickyThingStatus.Ready, "DONE", fontSize, -5.0);
        }
        return null;
      }

      // Token: 0x060037A8 RID: 14248 RVA: 0x00682F38 File Offset: 0x00682F38
      private FrameworkElement LiveLight(ClickyInkDashboardBoard board)
      {
        bool isPenWriting = this.IsPenWriting;
        Color color = (isPenWriting ? ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatusState.Bad) : ClickyInkDashboardFormNeeds.Lamp(board.Overall));
        Grid grid = new Grid
        {
          Width = 12.0,
          Height = 12.0
        };
        grid.Children.Add(new Ellipse
        {
          Fill = Ui.Brush(color)
        });
        if (isPenWriting)
        {
          Ellipse ellipse = new Ellipse
          {
            Stroke = Ui.Brush(color.Opacity(0.35)),
            StrokeThickness = 5.0,
            Margin = new Thickness(-2.5)
          };
          ellipse.Transformed(0.0, 0.0, 0.0, 1.0 + 0.25 * (Math.Sin(this.<clock>P * 5.0) + 1.0), null);
          grid.Children.Add(ellipse);
        }
        InkTextView inkTextView = ClickyInkFormKit.Text(isPenWriting ? "LIVE" : ((board.Overall == ClickyInkStatusState.Ok) ? "ALL GOOD" : ((board.Overall == ClickyInkStatusState.Bad) ? "ALERT" : "NEEDS YOU")), ClickyPaperFonts.Typed(12.0, new FontWeight?(FontWeights.ExtraBold)), isPenWriting ? PaperColor.Hex("#C2302B") : ClickyInkDashboardFormNeeds.Word(board.Overall), 1.4, 1.0, new int?(1));
        return new ClickyInkOverlay(new ClickyInkHStack(7.0, InkVerticalAlignment.Center, new UIElement[] { grid, inkTextView })
        {
          Margin = new Thickness(10.0, 5.0, 10.0, 5.0)
        }, new UIElement[] { ClickyShapeView.Stroke(ClickyShape.Capsule, color.Opacity(0.5), 1.2) })
        {
          VerticalAlignment = VerticalAlignment.Center
        };
      }

      // Token: 0x060037A9 RID: 14249 RVA: 0x0068315C File Offset: 0x0068315C
      private FrameworkElement Ruled(bool doubled)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 2.5
        };
        stackPanel.Children.Add(new Border
        {
          Height = (doubled ? 1.6 : 0.8),
          Background = Ui.Brush(this.Style.Ink.Opacity(0.8))
        });
        if (doubled)
        {
          stackPanel.Children.Add(new Border
          {
            Height = 0.8,
            Background = Ui.Brush(this.Style.Ink.Opacity(0.8))
          });
        }
        return stackPanel;
      }

      // Token: 0x060037AA RID: 14250 RVA: 0x00683218 File Offset: 0x00683218
      private FrameworkElement Headline(ClickyInkDashboardBoard board, ClickyInkFrontPagePlanner.Plan plan)
      {
        double num = plan.Headline.Width - (double)(this.IsMission ? 36 : 0);
        FrameworkElement frameworkElement = null;
        ClickyInkGroup quiet = board.Quiet;
        if (quiet != null)
        {
          ClickyInkLiveNote.Block block = quiet.Blocks[0];
          ClickyInkGroup answer = board.Answer;
          frameworkElement = ClickyInkGrow.Around(this.QuietLine(block, (answer != null) ? answer.Blocks.FirstOrDefault<ClickyInkLiveNote.Block>() : null, num), this.<clock>P, quiet.LandsAt, quiet.VanishesAt);
        }
        else
        {
          ClickyInkGroup answer2 = board.Answer;
          if (answer2 != null)
          {
            frameworkElement = ClickyInkGrow.Around(this.AnswerLine(answer2.Blocks[0], board, num), this.<clock>P, answer2.LandsAt, answer2.VanishesAt);
          }
          else
          {
            string leadFallbackTitle = plan.LeadFallbackTitle;
            if (leadFallbackTitle != null)
            {
              frameworkElement = ClickyInkFormKit.Text(leadFallbackTitle, ClickyPaperFonts.Hand(30.0), this.Style.Quiet, 0.0, 1.0, new int?(1));
            }
          }
        }
        Grid grid = new Grid();
        if (this.IsMission)
        {
          FrameworkElement frameworkElement2 = ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), ClickyInkDashboardFormNeeds.Lamp(board.Overall), 2);
          frameworkElement2.Opacity = (double)((board.Answer != null || board.Quiet != null) ? 1 : 0);
          grid.Children.Add(frameworkElement2);
        }
        if (frameworkElement != null)
        {
          frameworkElement.Margin = new Thickness((double)(this.IsMission ? 18 : 0));
          frameworkElement.HorizontalAlignment = HorizontalAlignment.Left;
          frameworkElement.VerticalAlignment = (this.IsMission ? VerticalAlignment.Center : VerticalAlignment.Top);
          grid.Children.Add(frameworkElement);
        }
        return grid;
      }

      // Token: 0x060037AB RID: 14251 RVA: 0x006833B0 File Offset: 0x006833B0
      private FrameworkElement QuietLine(ClickyInkLiveNote.Block block, [Nullable(2)] ClickyInkLiveNote.Block lastNews, double width)
      {
        ClickyInkQuietReading clickyInkQuietReading = new ClickyInkQuietReading(block.Source);
        double num = ClickyInkClock.Progress(this.<clock>P, block.LandsAt + 0.05, block.LandsAt + 0.35);
        Grid grid = new Grid
        {
          Children = 
          {
            new ClickyPaperStamp(ClickyThingStatus.Ready, "ALL QUIET", 22.0, -6.0)
          }
        };
        grid.Transformed(0.0, 0.0, 0.0, 1.0 + 0.35 * (1.0 - num), null);
        grid.Opacity = ((num > 0.0) ? (0.3 + 0.7 * num) : 0.0);
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 4.0
        };
        stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, this.<clock>P, this.Style, ClickyPaperFonts.Hand((double)(this.IsMission ? 34 : 42)), true, null, null).With(delegate(InkTextView t)
        {
          t.LineLimit = new int?(2);
        }));
        string checkedLine = clickyInkQuietReading.CheckedLine;
        if (checkedLine != null)
        {
          stackPanel.Children.Add(ClickyInkFormKit.Text(checkedLine, ClickyPaperFonts.Typed(14.0, new FontWeight?(FontWeights.Medium)), this.Style.Quiet, 0.0, 1.0, new int?(1)));
        }
        if (lastNews != null)
        {
          double num2 = 8.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
          UIElement[] array = new UIElement[2];
          array[0] = ClickyInkFormKit.Pill("last news", ClickyPaperFonts.Hand(17.0), this.Style.Quiet, this.Style.Quiet.Opacity(0.5), 5.0, 1.0, -1.5);
          array[1] = ClickyInkWordsView.Make(lastNews.Words, this.<clock>P, this.Style, ClickyPaperFonts.Hand(21.0), false, new Color?(this.Style.Ink.Opacity(0.75)), null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(2);
          });
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(num2, inkVerticalAlignment, array);
          clickyInkHStack.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
          stackPanel.Children.Add(clickyInkHStack);
        }
        return new ClickyInkHStack(22.0, InkVerticalAlignment.Center, new UIElement[]
        {
          grid,
          ClickyInkHStack.Flexible<StackPanel>(stackPanel),
          new ClickyInkSpacer()
        })
        {
          Width = width
        };
      }

      // Token: 0x060037AC RID: 14252 RVA: 0x006836D4 File Offset: 0x006836D4
      private FrameworkElement AnswerLine(ClickyInkLiveNote.Block block, ClickyInkDashboardBoard board, double width)
      {
        double? num = ClickyInkChange.ChangedAt(block.Source);
        double num2;
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          num2 = block.Words.FinishOfWriting(valueOrDefault);
        }
        else
        {
          num2 = block.Words.FinishedAt;
        }
        double num3 = num2;
        int num4 = block.Words.Current.CharacterCount();
        double num5 = (double)(this.IsMission ? ((num4 > 70) ? 30 : 36) : ((num4 > 80) ? 36 : ((num4 > 50) ? 42 : 48)));
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(22.0, InkVerticalAlignment.Center, Array.Empty<UIElement>())
        {
          Width = width
        };
        if (this.IsMission)
        {
          clickyInkHStack.Children.Add(ClickyInkDashboardFormNeeds.LampView(board.Overall, 28.0, 4, ClickyInkClock.Progress(this.<clock>P, block.LandsAt, block.LandsAt + 0.5)));
        }
        bool flag = block.IsStruck(this.<clock>P);
        InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, this.<clock>P, this.Style, ClickyPaperFonts.Hand(num5), true, flag ? new Color?(this.Style.Quiet) : null, null);
        if (flag)
        {
          inkTextView.Strikethrough = new Color?(this.Style.Fresh);
        }
        inkTextView.LineSpacing = -2.0;
        clickyInkHStack.Children.Add(inkTextView);
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
        string text = block.Source["big"];
        if (text != null && text.Length > 0)
        {
          FrameworkElement frameworkElement = ClickyInkDashboardFormNeeds.TickingNumber(block.Source, "big", this.<clock>P, this.Style, (double)((text.CharacterCount() > 6) ? 46 : 60), Math.Max(0.0, num3 - num.GetValueOrDefault(num3)), text, ClickyInkChange.Previous(block.Source, "big") ?? block.Source["_was"], false);
          frameworkElement.Margin = new Thickness(12.0, 4.0, 12.0, 4.0);
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(text.CharacterCount(), 0.16).Trim(0.0, ClickyInkClock.Progress(this.<clock>P, num3, num3 + 0.5)), PaperStroke.Round(this.Style.Fresh.Opacity(0.8), 2.8));
          clickyShapeView.Margin = new Thickness(-5.0);
          StackPanel stackPanel = new StackPanel
          {
            Spacing = 2.0
          };
          stackPanel.Children.Add(new ClickyInkOverlay(frameworkElement, new UIElement[] { clickyShapeView })
          {
            HorizontalAlignment = HorizontalAlignment.Center
          });
          string text2 = block.Source["note"];
          if (text2 != null && text2.Length > 0)
          {
            InkTextView inkTextView2 = ClickyInkFormKit.Text((num != null && this.<clock>P < num3) ? (ClickyInkChange.Previous(block.Source, "note") ?? text2) : text2, ClickyPaperFonts.Hand(19.0), this.Style.Fresh, 0.0, 1.0, new int?(2)).With(delegate(InkTextView t)
            {
              t.TextAlignment = TextAlignment.Center;
            });
            inkTextView2.MaxWidth = 220.0;
            inkTextView2.HorizontalAlignment = HorizontalAlignment.Center;
            inkTextView2.Opacity = this.<clock>P >= num3;
            stackPanel.Children.Add(inkTextView2);
          }
          clickyInkHStack.Children.Add(stackPanel);
        }
        return clickyInkHStack;
      }

      // Token: 0x060037AD RID: 14253 RVA: 0x00683AD8 File Offset: 0x00683AD8
      private FrameworkElement Stats(List<ClickyInkLiveNote.Block> blocks, Rect frame)
      {
        if (this.IsMission)
        {
          int num = ClickyInkFrontPagePlanner.MissionTilesPerRow(blocks.Count, frame.Width);
          double num2 = (frame.Width - (double)((num - 1) * 14)) / (double)num;
          double num3 = ClickyInkFrontPagePlans.MissionTileHeight(blocks.Select<ClickyInkLiveNote.Block, ClickyInkDocument.Block>((ClickyInkLiveNote.Block b) => b.Source).ToList<ClickyInkDocument.Block>());
          StackPanel stackPanel = new StackPanel
          {
            Spacing = 14.0
          };
          int num4 = 0;
          while (num4 * num < blocks.Count)
          {
            StackPanel stackPanel2 = new StackPanel
            {
              Orientation = Orientation.Horizontal,
              Spacing = 14.0
            };
            List<ClickyInkLiveNote.Block> list = blocks.Skip<ClickyInkLiveNote.Block>(num4 * num).Take<ClickyInkLiveNote.Block>(num).ToList<ClickyInkLiveNote.Block>();
            for (int i = 0; i < list.Count; i++)
            {
              ClickyInkLiveNote.Block block = list[i];
              double num5 = ClickyInkClock.Progress(this.<clock>P, block.LandsAt, block.LandsAt + 0.5);
              FrameworkElement frameworkElement = ClickyInkDashboardFormNeeds.Tile(block, this.<clock>P, this.Style, num4 * num + i);
              frameworkElement.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
              frameworkElement.VerticalAlignment = VerticalAlignment.Top;
              frameworkElement.HorizontalAlignment = HorizontalAlignment.Left;
              Grid grid = new Grid
              {
                Width = num2,
                Height = num3
              };
              grid.Children.Add(ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), this.TileAccent(block), i + num4 * 5));
              grid.Children.Add(frameworkElement);
              grid.Opacity = ((this.<clock>P >= block.LandsAt) ? Math.Min(1.0, num5 * 2.0) : 0.0);
              stackPanel2.Children.Add(grid);
            }
            stackPanel.Children.Add(stackPanel2);
            num4++;
          }
          return stackPanel;
        }
        StackPanel stackPanel3 = new StackPanel();
        stackPanel3.Spacing = 4.0;
        InkTextView inkTextView = ClickyInkFormKit.Text("BY THE NUMBERS", ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.ExtraBold)), this.Style.Quiet, 1.8, 1.0, new int?(1));
        inkTextView.Opacity = blocks.Count != 0;
        stackPanel3.Children.Add(inkTextView);
        stackPanel3.Children.Add(ClickyInkDashboardFormNeeds.Tiles(blocks, this.<clock>P, this.Style, frame.Width, ClickyInkTiles.Look.Ruled));
        return stackPanel3;
      }

      // Token: 0x060037AE RID: 14254 RVA: 0x00683D98 File Offset: 0x00683D98
      private Color TileAccent(ClickyInkLiveNote.Block block)
      {
        ClickyInkDocument.Block source = block.Source;
        if (source.Kind == ClickyInkDocument.Kind.Status)
        {
          return ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatus.State(source["state"] ?? source["status"]));
        }
        string text = source["delta"];
        if (text != null && (text.StartsWith('-') || text.StartsWith('−')))
        {
          return ClickyInkDashboardFormNeeds.Down;
        }
        string text2 = source["delta"];
        if (text2 != null && text2.StartsWith('+'))
        {
          return ClickyInkDashboardFormNeeds.Up;
        }
        return this.Style.Fresh;
      }

      // Token: 0x060037AF RID: 14255 RVA: 0x00683E34 File Offset: 0x00683E34
      private FrameworkElement Move(List<ClickyInkGroup> groups, Rect frame)
      {
        double num;
        if (groups.Count != 0)
        {
          num = groups.Min<ClickyInkGroup>((ClickyInkGroup g) => g.LandsAt);
        }
        else
        {
          num = double.PositiveInfinity;
        }
        double num2 = num;
        StackPanel stackPanel = new StackPanel();
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
        if (this.IsMission)
        {
          clickyInkHStack.Children.Add(ClickyInkDashboardFormNeeds.LampView(ClickyInkStatusState.Watch, 16.0, 7, 1.0).BaselineFromBottom(3.0));
        }
        clickyInkHStack.Children.Add(ClickyInkFormKit.Text("your move", ClickyPaperFonts.Hand((double)(this.IsMission ? 23 : 26)), PaperColor.Hex("#B05F00"), 0.0, 1.0, new int?(1)));
        clickyInkHStack.Children.Add(ClickyInkFormKit.Squiggle(5, PaperColor.Hex("#B05F00").Opacity(0.3)));
        clickyInkHStack.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
        stackPanel.Children.Add(clickyInkHStack);
        foreach (ClickyInkGroup clickyInkGroup in groups)
        {
          stackPanel.Children.Add(this.PieceView(clickyInkGroup, 0.0));
        }
        stackPanel.Margin = new Thickness(this.IsMission ? 16.0 : 0.0, 16.0, this.IsMission ? 16.0 : 0.0, 4.0);
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>())
        {
          Width = frame.Width
        };
        if (this.IsMission)
        {
          clickyInkOverlay.Behind(ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFF6DA"), ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatusState.Watch), 9));
        }
        else
        {
          Grid grid = new Grid
          {
            Margin = new Thickness(-12.0, 0.0, -12.0, 0.0)
          };
          grid.Children.Add(new Border
          {
            Background = Ui.Brush(PaperColor.Hex("#FFF3D1").Opacity(0.55))
          });
          grid.Children.Add(ClickyShapeView.Stroke(new ClickyInkRuledBox(6, 4.0).Trim(0.0, ClickyInkClock.Progress(this.<clock>P, num2, num2 + 0.6)), PaperStroke.Round(PaperColor.Hex("#B05F00").Opacity(0.6), 1.4)));
          clickyInkOverlay.Behind(grid);
        }
        clickyInkOverlay.Opacity = this.<clock>P >= num2;
        return clickyInkOverlay;
      }

      // Token: 0x060037B0 RID: 14256 RVA: 0x00684164 File Offset: 0x00684164
      private FrameworkElement PieceView(ClickyInkGroup group, double extraGap)
      {
        FrameworkElement frameworkElement = ClickyInkGroupView.Make(group, this.<clock>P, this.Style, this.<paper>P, this.<actions>P);
        return ClickyInkGrow.Around(new Grid
        {
          Width = 378.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          Children = { frameworkElement },
          Margin = new Thickness(0.0, 0.0, 0.0, 12.0 + extraGap)
        }, this.<clock>P, group.LandsAt, group.VanishesAt);
      }

      // Token: 0x060037B1 RID: 14257 RVA: 0x00684200 File Offset: 0x00684200
      private FrameworkElement ChunkView(ClickyInkFrontPagePlanner.Chunk chunk, ClickyInkFrontPageContent content, ClickyInkFrontPagePlanner.Plan plan, double extraGap)
      {
        List<ClickyInkGroup> list = content.Groups(chunk);
        double num;
        if (list.Count != 0)
        {
          num = list.Min<ClickyInkGroup>((ClickyInkGroup g) => g.LandsAt);
        }
        else
        {
          num = double.PositiveInfinity;
        }
        double num2 = num;
        ClickyInkLiveNote.Words words = content.StoryName(chunk.StoryID);
        Color color = this.Accent(content, chunk.StoryID);
        StackPanel stackPanel = new StackPanel();
        if (this.IsMission)
        {
          ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
          clickyInkHStack.Children.Add(new Ellipse
          {
            Width = 9.0,
            Height = 9.0,
            Fill = Ui.Brush(color)
          });
          if (words != null)
          {
            clickyInkHStack.Children.Add(ClickyInkWordsView.Make(words, this.<clock>P, this.Style, ClickyPaperFonts.Hand(21.0), true, null, null).With(delegate(InkTextView t)
            {
              t.LineLimit = new int?(1);
            }));
          }
          if (chunk.IsContinuation)
          {
            clickyInkHStack.Children.Add(ClickyInkFormKit.Text("cont.", ClickyPaperFonts.Hand(15.0), this.Style.Quiet, 0.0, 1.0, new int?(1)));
          }
          clickyInkHStack.Children.Add(new ClickyInkSpacer());
          foreach (FrameworkElement frameworkElement in this.Tags(content.Counts(chunk.StoryID, this.<clock>P)))
          {
            clickyInkHStack.Children.Add(frameworkElement);
          }
          clickyInkHStack.VerticalAlignment = VerticalAlignment.Center;
          stackPanel.Children.Add(new Grid
          {
            Height = 30.0,
            Margin = new Thickness(0.0, 0.0, 0.0, 6.0),
            Children = { clickyInkHStack }
          });
        }
        else if (chunk.IsContinuation)
        {
          InkTextView inkTextView = ClickyInkFormKit.Text((((words != null) ? words.Current.ToLowerInvariant() : null) ?? "continued") + ", continued", ClickyPaperFonts.Hand(16.0), this.Style.Quiet, 0.0, 1.0, new int?(1));
          inkTextView.VerticalAlignment = VerticalAlignment.Top;
          stackPanel.Children.Add(new Grid
          {
            Height = 24.0,
            Children = { inkTextView }
          });
        }
        else if (!chunk.IsLead && words != null)
        {
          StackPanel stackPanel2 = new StackPanel
          {
            Spacing = 4.0,
            VerticalAlignment = VerticalAlignment.Top
          };
          stackPanel2.Children.Add(new Border
          {
            Height = 1.2,
            Background = Ui.Brush(this.<paper>P.Ink.Opacity(0.55))
          });
          double num3 = 8.0;
          InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.FirstBaseline;
          UIElement[] array = new UIElement[1];
          array[0] = ClickyInkWordsView.Make(words, this.<clock>P, this.Style, ClickyPaperFonts.Hand(25.0), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          });
          ClickyInkHStack clickyInkHStack2 = new ClickyInkHStack(num3, inkVerticalAlignment, array);
          foreach (FrameworkElement frameworkElement2 in this.Tags(content.Counts(chunk.StoryID, this.<clock>P)))
          {
            clickyInkHStack2.Children.Add(frameworkElement2);
          }
          clickyInkHStack2.Children.Add(new ClickyInkSpacer());
          stackPanel2.Children.Add(clickyInkHStack2);
          stackPanel.Children.Add(new Grid
          {
            Height = 44.0,
            Children = { stackPanel2 }
          });
        }
        foreach (ClickyInkGroup clickyInkGroup in list)
        {
          stackPanel.Children.Add(this.PieceView(clickyInkGroup, extraGap));
        }
        if (this.IsMission)
        {
          stackPanel.Children.Add(new Grid
          {
            Height = 4.0
          });
        }
        stackPanel.Margin = new Thickness(this.IsMission ? 16.0 : 0.0, (double)(this.IsMission ? 8 : 0), this.IsMission ? 16.0 : 0.0, 0.0);
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>())
        {
          Width = plan.ColumnWidth
        };
        if (this.IsMission)
        {
          clickyInkOverlay.Behind(ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), color, chunk.StoryID.CharacterCount() + (chunk.IsContinuation ? 3 : 0)));
        }
        clickyInkOverlay.Opacity = this.<clock>P >= num2;
        FrameworkElement frameworkElement3 = ClickyInkGrow.Around(clickyInkOverlay, this.<clock>P, num2, null);
        frameworkElement3.Margin = new Thickness(0.0, 0.0, 0.0, plan.StoryGap + extraGap);
        return frameworkElement3;
      }

      // Token: 0x060037B2 RID: 14258 RVA: 0x00684808 File Offset: 0x00684808
      [return: Nullable(2)]
      private FrameworkElement BandView(ClickyInkFrontPagePlanner.Band band, ClickyInkFrontPageContent content)
      {
        ClickyInkGroup clickyInkGroup = content.Group(band.GroupID);
        if (clickyInkGroup == null)
        {
          return null;
        }
        ClickyInkLiveNote.Words words = content.StoryName(band.StoryID);
        StackPanel stackPanel = new StackPanel();
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
        if (this.IsMission)
        {
          clickyInkHStack.Children.Add(new Ellipse
          {
            Width = 9.0,
            Height = 9.0,
            Fill = Ui.Brush(this.Accent(content, band.StoryID))
          });
        }
        if (words != null)
        {
          clickyInkHStack.Children.Add(ClickyInkWordsView.Make(words, this.<clock>P, this.Style, ClickyPaperFonts.Hand((double)(this.IsMission ? 21 : 25)), true, null, null).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
          }));
        }
        foreach (FrameworkElement frameworkElement in this.Tags(content.Counts(band.StoryID, this.<clock>P)))
        {
          clickyInkHStack.Children.Add(frameworkElement);
        }
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
        clickyInkHStack.VerticalAlignment = VerticalAlignment.Bottom;
        Grid grid = new Grid
        {
          Height = 36.0,
          Margin = new Thickness(0.0, 0.0, 0.0, 8.0),
          Children = { clickyInkHStack }
        };
        if (!this.IsMission)
        {
          grid.Children.Add(new Border
          {
            Height = 1.2,
            VerticalAlignment = VerticalAlignment.Top,
            Background = Ui.Brush(this.<paper>P.Ink.Opacity(0.55))
          });
        }
        stackPanel.Children.Add(grid);
        FrameworkElement frameworkElement2 = ClickyInkTable.Create(clickyInkGroup.Blocks[0], this.<clock>P, this.Style, band.Frame.Width - (this.IsMission ? 32.0 : 0.0));
        stackPanel.Children.Add(ClickyInkGrow.Around(frameworkElement2, this.<clock>P, clickyInkGroup.LandsAt, clickyInkGroup.VanishesAt));
        stackPanel.Margin = new Thickness(this.IsMission ? 16.0 : 0.0, (double)(this.IsMission ? 4 : 0), this.IsMission ? 16.0 : 0.0, 0.0);
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>())
        {
          VerticalAlignment = VerticalAlignment.Top
        };
        if (this.IsMission)
        {
          clickyInkOverlay.Behind(ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), this.Accent(content, band.StoryID), band.GroupID.CharacterCount()));
        }
        clickyInkOverlay.Opacity = this.<clock>P >= clickyInkGroup.LandsAt;
        return clickyInkOverlay;
      }

      // Token: 0x060037B3 RID: 14259 RVA: 0x00684B60 File Offset: 0x00684B60
      [NullableContext(0)]
      [return: Nullable(1)]
      private IEnumerable<FrameworkElement> Tags([TupleElementNames(new string[] { "New", "Needs" })] ValueTuple<int, int> counts)
      {
        ClickyInkFrontPageView.Builder.<Tags>d__31 <Tags>d__ = new ClickyInkFrontPageView.Builder.<Tags>d__31(-2);
        <Tags>d__.<>3__counts = counts;
        return <Tags>d__;
      }

      // Token: 0x060037B4 RID: 14260 RVA: 0x00684B70 File Offset: 0x00684B70
      private Color Accent(ClickyInkFrontPageContent content, string storyID)
      {
        ValueTuple<int, int> valueTuple = content.Counts(storyID, this.<clock>P);
        if (valueTuple.Item2 > 0)
        {
          return ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatusState.Watch);
        }
        if (valueTuple.Item1 > 0)
        {
          return ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatusState.Ok);
        }
        if (content.HasBadStatus(storyID))
        {
          return ClickyInkDashboardFormNeeds.Lamp(ClickyInkStatusState.Bad);
        }
        return this.Style.Fresh.Opacity(0.75);
      }

      // Token: 0x060037B5 RID: 14261 RVA: 0x00684BD4 File Offset: 0x00684BD4
      private FrameworkElement Footer(ClickyInkDashboardBoard board, ClickyInkFrontPagePlanner.Plan plan)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 5.0
        };
        if (!this.IsMission)
        {
          stackPanel.Children.Add(new Border
          {
            Height = 0.8,
            Background = Ui.Brush(this.Style.Ink.Opacity(0.5))
          });
        }
        ClickyInkHStack clickyInkHStack = new ClickyInkHStack(14.0, InkVerticalAlignment.FirstBaseline, Array.Empty<UIElement>());
        clickyInkHStack.Children.Add(ClickyInkFormKit.Text(this.IsMission ? "FEED" : "SOURCES", ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.ExtraBold)), this.Style.Quiet, 1.6, 1.0, new int?(1)));
        foreach (ClickyInkLiveNote.Block block in board.Sources.Take<ClickyInkLiveNote.Block>(6))
        {
          if (this.<clock>P >= block.LandsAt)
          {
            clickyInkHStack.Children.Add(ClickyInkFormKit.SourceLink(block, this.<clock>P, this.Style, this.<actions>P, 11.0, 12.5));
          }
        }
        if (plan.OverflowNames.Count > 0)
        {
          clickyInkHStack.Children.Add(ClickyInkFormKit.Text("also inside: " + string.Join(" · ", plan.OverflowNames), ClickyPaperFonts.Hand(16.0), this.Style.Quiet, 0.0, 1.0, new int?(1)));
        }
        clickyInkHStack.Children.Add(new ClickyInkSpacer());
        stackPanel.Children.Add(clickyInkHStack);
        stackPanel.Margin = new Thickness((double)(this.IsMission ? 14 : 0), (double)(this.IsMission ? 6 : 0), (double)(this.IsMission ? 14 : 0), (double)(this.IsMission ? 6 : 0));
        ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>())
        {
          VerticalAlignment = VerticalAlignment.Top
        };
        if (this.IsMission && board.Sources.Count > 0)
        {
          clickyInkOverlay.Behind(ClickyInkFrontPageView.Builder.PanelCard(PaperColor.Hex("#FFFDF6"), this.Style.Quiet, 11));
        }
        clickyInkOverlay.Opacity = (double)((board.Sources.Count == 0 && plan.OverflowNames.Count == 0) ? 0 : 1);
        return clickyInkOverlay;
      }

      // Token: 0x060037B6 RID: 14262 RVA: 0x00684E74 File Offset: 0x00684E74
      private static FrameworkElement PanelCard(Color tint, Color accent, int seed)
      {
        Grid grid = new Grid();
        grid.Children.Add(new PaperSurface(ClickyShape.RoundedRectangle(7.0), PaperFill.Of(tint), new double?(0.3)).WithWash(PaperFill.Of(accent.Opacity(0.85)), (Size s) => new Rect(0.0, 0.0, s.Width, 4.0), true).WithShadow(PaperColor.Black.Opacity(0.16), 4.0, 0.0, 2.0));
        grid.Children.Add(ClickyShapeView.Stroke(new ClickyInkRuledBox(seed, 0.0), ClickyInkDashboardFormNeeds.Pencil.Opacity(0.18), 1.0));
        return grid;
      }

      // Token: 0x060037B7 RID: 14263 RVA: 0x00684F60 File Offset: 0x00684F60
      [CompilerGenerated]
      internal static void <Body>g__Place|16_0(FrameworkElement e, double x, double y, double? width = null, double? height = null, ref ClickyInkFrontPageView.Builder.<>c__DisplayClass16_0 A_5)
      {
        if (width != null)
        {
          double valueOrDefault = width.GetValueOrDefault();
          e.Width = Math.Max(0.0, valueOrDefault);
        }
        if (height != null)
        {
          double valueOrDefault2 = height.GetValueOrDefault();
          e.Height = Math.Max(0.0, valueOrDefault2);
        }
        Canvas.SetLeft(e, x);
        Canvas.SetTop(e, y);
        A_5.canvas.Children.Add(e);
      }

      // Token: 0x0400221A RID: 8730
      [CompilerGenerated]
      private double <clock>P = clock;

      // Token: 0x0400221B RID: 8731
      [CompilerGenerated]
      private ClickyInkFrontPageVariant <variant>P = variant;

      // Token: 0x0400221C RID: 8732
      [CompilerGenerated]
      private ClickyHomeSpreadPaper <paper>P = paper;

      // Token: 0x0400221D RID: 8733
      [CompilerGenerated]
      private ClickyInkActions <actions>P = actions;

      // Token: 0x0400221E RID: 8734
      [CompilerGenerated]
      private Size <size>P = size;

      // Token: 0x0400221F RID: 8735
      [CompilerGenerated]
      private int? <runNumber>P = runNumber;

      // Token: 0x04002220 RID: 8736
      [Nullable(2)]
      [CompilerGenerated]
      private string <dateLine>P = dateLine;

      // Token: 0x04002221 RID: 8737
      private readonly ClickyInkLiveNote note = rawNote.FoldingStruckWords(2.5);

      // Token: 0x04002222 RID: 8738
      [Nullable(2)]
      private ClickyInkStyle style;
    }
  }
}
