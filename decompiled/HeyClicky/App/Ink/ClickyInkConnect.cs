using System;
using System.Collections.Generic;
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
  // Token: 0x02000256 RID: 598
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkConnect
  {
    // Token: 0x06001A42 RID: 6722 RVA: 0x005DEDAC File Offset: 0x005DEDAC
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkConnect.State state = InkObjectState.For<ClickyInkConnect.State>(block, "connect");
      if (!state.Subscribed)
      {
        state.Subscribed = true;
        ClickyInkConnections.Shared.Changed += delegate
        {
          state.Animate(InkSpring.SettleSeconds(0.32, 0.85));
        };
      }
      return new InkObjectHost(state, () => ClickyInkConnect.Build(block, style, actions, state));
    }

    // Token: 0x06001A43 RID: 6723 RVA: 0x005DEE30 File Offset: 0x005DEE30
    private static FrameworkElement Build(ClickyInkLiveNote.Block block, ClickyInkStyle style, ClickyInkActions actions, ClickyInkConnect.State state)
    {
      ClickyInkConnect.<>c__DisplayClass3_0 CS$<>8__locals1 = new ClickyInkConnect.<>c__DisplayClass3_0();
      CS$<>8__locals1.actions = actions;
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      CS$<>8__locals1.connections = ClickyInkConnections.Shared;
      CS$<>8__locals1.source = block.Source;
      string text = ClickyInkNoteActions.Plain(CS$<>8__locals1.source.Text);
      string text2 = ClickyInkNoteActions.Plain(CS$<>8__locals1.source["app"] ?? text);
      CS$<>8__locals1.catalogApp = CS$<>8__locals1.connections.AppNamed(text2) ?? CS$<>8__locals1.connections.AppNamed(text);
      ClickyInkConnections.App catalogApp = CS$<>8__locals1.catalogApp;
      string text3 = ((catalogApp != null) ? catalogApp.DisplayName : null) ?? ((text.Length == 0) ? text2 : text);
      ClickyInkConnect.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
      ClickyInkConnections.App catalogApp2 = CS$<>8__locals1.catalogApp;
      CS$<>8__locals2.slug = ((catalogApp2 != null) ? catalogApp2.Slug : null) ?? text2;
      CS$<>8__locals1.phase = CS$<>8__locals1.connections.PhaseFor(CS$<>8__locals1.slug);
      bool flag = CS$<>8__locals1.catalogApp != null && CS$<>8__locals1.catalogApp.IsConnected && !CS$<>8__locals1.catalogApp.NeedsReconnect;
      ClickyInkConnections.Phase phase = CS$<>8__locals1.phase;
      string text4 = text3;
      bool flag2 = flag;
      ClickyInkConnections.App catalogApp3 = CS$<>8__locals1.catalogApp;
      string text5 = ClickyInkConnectRules.Title(phase, text4, flag2, catalogApp3 != null && catalogApp3.NeedsReconnect);
      bool flag3 = ClickyInkConnectRules.ShowsConnectedStamp(CS$<>8__locals1.phase, flag);
      if (!state.Built)
      {
        state.Built = true;
        state.ShownPhase = CS$<>8__locals1.phase;
        state.StampShown = flag3;
      }
      if (!object.Equals(state.ShownPhase, CS$<>8__locals1.phase))
      {
        state.ShownPhase = CS$<>8__locals1.phase;
        state.PhaseChangedAt = InkObjectState.Now;
      }
      if (flag3 != state.StampShown)
      {
        state.StampShown = flag3;
        state.StampChangedAt = InkObjectState.Now;
      }
      StackPanel stackPanel = InkViews.VStack(7.0, new UIElement[]
      {
        ClickyInkRubberStamp.TypedLabel(ClickyInkConnectRules.Label(CS$<>8__locals1.phase, flag), ClickyInkConnect.Ink.Opacity(0.5)),
        InkViews.Plain(text5, ClickyPaperFonts.Typed(15.0, new FontWeight?(FontWeights.Bold)), ClickyInkConnect.Ink, null)
      });
      string text6 = CS$<>8__locals1.source["why"] ?? CS$<>8__locals1.source["note"];
      if (text6 != null && text6.Length > 0 && !(CS$<>8__locals1.phase is ClickyInkConnections.Phase.Connected))
      {
        stackPanel.Children.Add(InkViews.Plain(ClickyInkNoteActions.Plain(text6), ClickyPaperFonts.Hand(15.0), ClickyInkConnect.Ink.Opacity(0.7), null));
      }
      stackPanel.Children.Add(ClickyInkConnect.Controls(CS$<>8__locals1.phase, text5, text3, flag, style, delegate
      {
        CS$<>8__locals1.actions.Perform(ClickyInkNoteActions.Connected(CS$<>8__locals1.source).ForAnalytics("connect_confirmed", "already_connected", null));
      }, new Action(CS$<>8__locals1.<Build>g__StartConnecting|0)));
      UIElementCollection children = stackPanel.Children;
      children[children.Count - 1].Opacity = Math.Clamp(InkSpring.Value(0.32, 0.85, InkObjectState.Now - state.PhaseChangedAt), 0.0, 1.0);
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(13.0, InkVerticalAlignment.Top, new UIElement[]
      {
        ClickyInkConnect.Logo(CS$<>8__locals1.catalogApp),
        ClickyInkHStack.Flexible<StackPanel>(stackPanel),
        new ClickyInkSpacer()
      })
      {
        Margin = new Thickness(46.0, 15.0, 16.0, 15.0)
      };
      ClickyInkLuggageTagShape clickyInkLuggageTagShape = new ClickyInkLuggageTagShape();
      InkPaperSheet inkPaperSheet = new InkPaperSheet("connect-tag", clickyInkLuggageTagShape, new PaperFill.Linear(new ValueTuple<double, Color>[]
      {
        new ValueTuple<double, Color>(0.0, PaperColor.Hex("#FDF9F0")),
        new ValueTuple<double, Color>(1.0, PaperColor.Hex("#F7EFDE"))
      }, new Point(0f, 0f), new Point(1f, 1f)), new double?(0.3), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.1), 3.0, 0.0, 1.5)
      }, null, null);
      ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(clickyInkLuggageTagShape, new PaperStroke(PaperColor.Hex("#A9824B").Opacity(0.22), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      FrameworkElement frameworkElement = ClickyInkConnect.Twine();
      FrameworkElement frameworkElement2 = ClickyInkConnect.Grommet();
      List<UIElement> list = new List<UIElement> { clickyShapeView, frameworkElement, frameworkElement2 };
      if (flag3 || InkObjectState.Now - state.StampChangedAt < 1.5)
      {
        double num = Math.Clamp(InkSpring.Value(0.32, 0.85, InkObjectState.Now - state.StampChangedAt), 0.0, 1.2);
        double num2 = (flag3 ? num : (1.0 - Math.Min(1.0, num)));
        if (num2 > 0.001)
        {
          FrameworkElement frameworkElement3 = ClickyInkRubberStamp.Make("connected", new Color?(ClickyPaperInk.DoneGreen), 6.0);
          frameworkElement3.HorizontalAlignment = HorizontalAlignment.Right;
          frameworkElement3.VerticalAlignment = VerticalAlignment.Top;
          frameworkElement3.Margin = new Thickness(0.0, 10.0, 12.0, 0.0);
          Grid grid = new Grid
          {
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Top,
            Children = { frameworkElement3 }
          };
          grid.Transformed(0.0, 0.0, 0.0, 1.6 - 0.6 * num2, null);
          grid.Opacity = Math.Clamp(num2, 0.0, 1.0);
          list.Add(grid);
        }
      }
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(clickyInkHStack, list.ToArray()).Behind(inkPaperSheet);
      clickyInkOverlay.Width = contentWidth - 24.0;
      clickyInkOverlay.HorizontalAlignment = HorizontalAlignment.Left;
      clickyInkOverlay.Margin = new Thickness(18.0, 0.0, 0.0, 0.0);
      clickyInkOverlay.Transformed(0.0, 0.0, -0.3, 1.0, null);
      return new Grid
      {
        Width = contentWidth,
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { clickyInkOverlay }
      };
    }

    // Token: 0x06001A44 RID: 6724 RVA: 0x005DF500 File Offset: 0x005DF500
    private static FrameworkElement Grommet()
    {
      Grid grid = new Grid();
      grid.Width = 18.0;
      grid.Height = 18.0;
      grid.Margin = new Thickness(13.0, 0.0, 0.0, 0.0);
      grid.HorizontalAlignment = HorizontalAlignment.Left;
      grid.VerticalAlignment = VerticalAlignment.Center;
      grid.IsHitTestVisible = false;
      UIElementCollection children = grid.Children;
      string text = "grommet";
      IClickyShape circle = ClickyShape.Circle;
      PaperFill paperFill = PaperFill.Of(PaperColor.Hex("#C9A15E"));
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.2), 0.8, 0.0, 0.6)
      };
      PaperStroke paperStroke = new PaperStroke(PaperColor.Hex("#8A6A33"), 0.8, PenLineCap.Flat, PenLineJoin.Miter, null);
      children.Add(new InkPaperSheet(text, circle, paperFill, null, array, paperStroke, null));
      grid.Children.Add(new Ellipse
      {
        Width = 9.0,
        Height = 9.0,
        Fill = Ui.Brush(PaperColor.Hex("#5E4A2A").Opacity(0.55)),
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      });
      return grid;
    }

    // Token: 0x06001A45 RID: 6725 RVA: 0x005DF664 File Offset: 0x005DF664
    private static FrameworkElement Twine()
    {
      PaperPath paperPath = new PaperPath().MoveTo(52.0, 34.0).CubicTo(new Point(2f, 2f), new Point(28f, 40f), new Point(6f, 26f));
      PaperDrawing paperDrawing = new PaperDrawing().Stroke(paperPath, PaperStroke.Round(PaperColor.Hex("#8E6F45").Opacity(0.7), 1.5)).Sized(54.0, 40.0);
      paperDrawing.HorizontalAlignment = HorizontalAlignment.Left;
      paperDrawing.VerticalAlignment = VerticalAlignment.Center;
      paperDrawing.Transformed(-30.0, -17.0, 0.0, 1.0, null);
      return paperDrawing;
    }

    // Token: 0x06001A46 RID: 6726 RVA: 0x005DF74C File Offset: 0x005DF74C
    private static FrameworkElement Logo([Nullable(2)] ClickyInkConnections.App app)
    {
      FrameworkElement frameworkElement;
      if (app == null)
      {
        frameworkElement = InkViews.Symbol("powerplug.fill", 16.0, PaperColor.Hex("#8A5300"), true) ?? new Grid();
      }
      else
      {
        string slug = app.Slug;
        string displayName = app.DisplayName;
        double num = 28.0;
        double num2 = 16.0;
        Uri logoUrl = app.LogoUrl;
        frameworkElement = IntegrationLogos.Artwork(slug, displayName, num, num2, (logoUrl != null) ? logoUrl.AbsoluteUri : null);
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      frameworkElement2.Width = 28.0;
      frameworkElement2.Height = 28.0;
      frameworkElement2.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
      string text = "connect-logo";
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(10.0);
      PaperFill paperFill = PaperFill.Of(PaperColor.White);
      PaperShadow[] array = new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.06), 1.0, 0.0, 0.5)
      };
      InkPaperSheet inkPaperSheet = new InkPaperSheet(text, clickyShape, paperFill, null, array, null, null);
      ClickyShapeView clickyShapeView = ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(10.0), new PaperStroke(PaperColor.Hex("#A9824B").Opacity(0.4), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null));
      Grid grid = new Grid();
      grid.Width = 44.0;
      grid.Height = 44.0;
      grid.VerticalAlignment = VerticalAlignment.Top;
      grid.Children.Add(inkPaperSheet);
      grid.Children.Add(frameworkElement2);
      grid.Children.Add(clickyShapeView);
      grid.Transformed(0.0, 0.0, -1.5, 1.0, null);
      return grid;
    }

    // Token: 0x06001A47 RID: 6727 RVA: 0x005DF914 File Offset: 0x005DF914
    private static FrameworkElement Controls([Nullable(2)] ClickyInkConnections.Phase phase, string title, string appName, bool isAlreadyConnected, ClickyInkStyle style, Action carryOn, Action startConnecting)
    {
      if (phase is ClickyInkConnections.Phase.Opening)
      {
        return ClickyInkConnect.Progress("Opening the sign-in page…", style);
      }
      if (phase is ClickyInkConnections.Phase.WaitingForSignIn)
      {
        Pressable pressable = InkViews.PlainButton(InkViews.Plain("Open the sign-in page again", ClickyPaperFonts.Typed(11.5, new FontWeight?(FontWeights.Medium)), style.Fresh, null), startConnecting, null);
        return InkViews.VStack(6.0, new UIElement[]
        {
          ClickyInkConnect.Progress("Sign in to " + appName + " in your browser, then come back here.", style),
          pressable
        });
      }
      if (phase is ClickyInkConnections.Phase.Checking)
      {
        return ClickyInkConnect.Progress("Checking the connection…", style);
      }
      if (phase is ClickyInkConnections.Phase.Connected)
      {
        return InkViews.Plain("Done. The job is carrying on with it.", ClickyPaperFonts.Hand(15.0), ClickyPaperInk.DoneGreen, null);
      }
      ClickyInkConnections.Phase.Failed failed = phase as ClickyInkConnections.Phase.Failed;
      if (failed != null)
      {
        return InkViews.VStack(7.0, new UIElement[]
        {
          InkViews.Plain(failed.Message, ClickyPaperFonts.Typed(12.0, null), PaperColor.Hex("#C2410C"), null),
          new ClickyPaperGelButton("Try again", startConnecting, ClickyPaperGel.Blue, 28.0, 13.0, 12.5, "arrow.clockwise")
        });
      }
      if (!isAlreadyConnected)
      {
        return new ClickyPaperGelButton(title, startConnecting, ClickyPaperGel.Blue, 28.0, 13.0, 12.5, "link");
      }
      return new ClickyPaperGelButton("Carry on", carryOn, ClickyPaperGel.Blue, 28.0, 13.0, 12.5, null);
    }

    // Token: 0x06001A48 RID: 6728 RVA: 0x005DFAC4 File Offset: 0x005DFAC4
    private static FrameworkElement Progress(string words, ClickyInkStyle style)
    {
      return new ClickyInkHStack(7.0, InkVerticalAlignment.Center, new UIElement[]
      {
        InkViews.Spinner(16.0),
        ClickyInkHStack.Flexible<InkTextView>(InkViews.Plain(words, ClickyPaperFonts.Typed(12.0, null), style.Quiet, null))
      });
    }

    // Token: 0x04000CA4 RID: 3236
    private static readonly Color Ink = PaperColor.Hex("#3A2A12");

    // Token: 0x02000812 RID: 2066
    [NullableContext(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x04002275 RID: 8821
      public bool Subscribed;

      // Token: 0x04002276 RID: 8822
      public bool Built;

      // Token: 0x04002277 RID: 8823
      [Nullable(2)]
      public ClickyInkConnections.Phase ShownPhase;

      // Token: 0x04002278 RID: 8824
      public double PhaseChangedAt = -1000.0;

      // Token: 0x04002279 RID: 8825
      public bool StampShown;

      // Token: 0x0400227A RID: 8826
      public double StampChangedAt = -1000.0;
    }
  }
}
