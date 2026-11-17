using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002EB RID: 747
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class FooterRequestRow : StackPanel
  {
    // Token: 0x06002034 RID: 8244 RVA: 0x00614DCC File Offset: 0x00614DCC
    public FooterRequestRow(ArcDock dock, ArcCardContent content, CardFooterRequest request, double? buttonHeight, string analyticsSurface = "dock_window", bool buttonsOnly = false)
    {
      this.dock = dock;
      this.content = content;
      this.analyticsSurface = analyticsSurface;
      base.Padding = new Thickness(22.0, 12.0, 22.0, 12.0);
      base.Spacing = 9.0;
      if (request.Kind == CardFooterRequest.Kinds.Ready)
      {
        CardReadyOutput ready = request.Ready;
        if (ready != null)
        {
          base.Children.Add(this.ReadyButtons(ready, buttonHeight));
          return;
        }
      }
      if (buttonsOnly)
      {
        base.Padding = new Thickness(0.0);
        base.HorizontalAlignment = HorizontalAlignment.Left;
        FirstThatFitsHorizontally firstThatFitsHorizontally = new FirstThatFitsHorizontally();
        firstThatFitsHorizontally.Children.Add(this.Row(request, false, buttonHeight));
        DockFlow dockFlow = new DockFlow
        {
          HorizontalSpacing = 8.0,
          VerticalSpacing = 8.0
        };
        foreach (FrameworkElement frameworkElement in this.Buttons(request, false, buttonHeight))
        {
          dockFlow.Children.Add(frameworkElement);
        }
        firstThatFitsHorizontally.Children.Add(dockFlow);
        base.Children.Add(firstThatFitsHorizontally);
        AutomationProperties.SetName(this, request.Title + ". " + request.Detail);
        return;
      }
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      FrameworkElement frameworkElement3;
      if (request.Kind == CardFooterRequest.Kinds.ConnectChrome)
      {
        FrameworkElement frameworkElement2 = ChromeIcon.Make(32.0);
        if (frameworkElement2 != null)
        {
          frameworkElement3 = frameworkElement2;
          goto IL_0283;
        }
      }
      HandoffGlyph? glyph = request.Glyph;
      if (glyph != null)
      {
        HandoffGlyph valueOrDefault = glyph.GetValueOrDefault();
        frameworkElement3 = new HandoffChipView(valueOrDefault, content.RailKey, 32.0, null, true);
      }
      else
      {
        frameworkElement3 = new Grid
        {
          Width = 32.0,
          Height = 32.0,
          Children = 
          {
            new Ellipse
            {
              Fill = Ui.Brush(ArcColors.Of(request.IconBackground))
            },
            ArcSymbols.Make(request.Symbol, 14.0, Colors.White, true)
          }
        };
      }
      IL_0283:
      frameworkElement3.VerticalAlignment = VerticalAlignment.Top;
      grid.Children.Add(frameworkElement3);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0
      };
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      stackPanel2.Children.Add(Ui.Text(request.Title, 16.5, FontWeights.SemiBold, ArcColors.Ink, TextAlignment.Left, 0, false, false));
      if (request.Glyph != null)
      {
        stackPanel2.Children.Add(new PaperStampView("you", DS.Hex(12081670U, 1.0), 8.5, (double)(NotePaperColors.Seed(content.RailKey) % 5 - 7)));
      }
      stackPanel.Children.Add(stackPanel2);
      stackPanel.Children.Add(Ui.Text(request.Detail, 14.0, FontWeights.Normal, ArcColors.Ink.WithAlpha(0.6), TextAlignment.Left, 3, false, false));
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      base.Children.Add(grid);
      FirstThatFitsHorizontally firstThatFitsHorizontally2 = new FirstThatFitsHorizontally
      {
        Margin = new Thickness(42.0, 0.0, 0.0, 0.0)
      };
      if (buttonHeight == null)
      {
        firstThatFitsHorizontally2.Children.Add(this.Row(request, true, buttonHeight));
      }
      firstThatFitsHorizontally2.Children.Add(this.Row(request, false, buttonHeight));
      DockFlow dockFlow2 = new DockFlow
      {
        HorizontalSpacing = 8.0,
        VerticalSpacing = 8.0
      };
      foreach (FrameworkElement frameworkElement4 in this.Buttons(request, false, buttonHeight))
      {
        dockFlow2.Children.Add(frameworkElement4);
      }
      firstThatFitsHorizontally2.Children.Add(dockFlow2);
      base.Children.Add(firstThatFitsHorizontally2);
      AutomationProperties.SetName(this, request.Title + ". " + request.Detail);
    }

    // Token: 0x06002035 RID: 8245 RVA: 0x006152B4 File Offset: 0x006152B4
    private FrameworkElement Row(CardFooterRequest request, bool isLarge, double? buttonHeight)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      foreach (FrameworkElement frameworkElement in this.Buttons(request, isLarge, buttonHeight))
      {
        stackPanel.Children.Add(frameworkElement);
      }
      return stackPanel;
    }

    // Token: 0x06002036 RID: 8246 RVA: 0x00615328 File Offset: 0x00615328
    private IEnumerable<FrameworkElement> Buttons(CardFooterRequest request, bool isLarge, double? buttonHeight)
    {
      FooterRequestRow.<Buttons>d__6 <Buttons>d__ = new FooterRequestRow.<Buttons>d__6(-2);
      <Buttons>d__.<>4__this = this;
      <Buttons>d__.<>3__request = request;
      <Buttons>d__.<>3__isLarge = isLarge;
      <Buttons>d__.<>3__buttonHeight = buttonHeight;
      return <Buttons>d__;
    }

    // Token: 0x06002037 RID: 8247 RVA: 0x00615350 File Offset: 0x00615350
    private FrameworkElement ReadyButtons(CardReadyOutput output, double? buttonHeight)
    {
      string text = output.ButtonSymbol;
      if (text == null)
      {
        CardReadyOutput.Kinds kind = output.Kind;
        bool flag = kind == CardReadyOutput.Kinds.File || kind == CardReadyOutput.Kinds.Several;
        text = (flag ? output.Symbol : null);
      }
      string text2 = text;
      bool flag2 = buttonHeight == null;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      stackPanel.Children.Add(ArcButtons.Gel(output.ButtonTitle, text2, GelAppearance.PrimaryBlue, buttonHeight, new double?(flag2 ? 15.5 : 13.0), flag2 ? GelButton.Sizing.Large : GelButton.Sizing.Normal, delegate
      {
        this.TrackAnswer("open_output");
        this.dock.OpenReadyOutput(this.content.RailKey, output);
      }));
      string jobId = this.content.JobId;
      if (jobId != null)
      {
        stackPanel.Children.Add(ArcButtons.Gel("Show me", null, GelAppearance.SecondaryWhite, buttonHeight, new double?(flag2 ? 15.5 : 13.0), flag2 ? GelButton.Sizing.Large : GelButton.Sizing.Normal, delegate
        {
          this.TrackAnswer("show_me");
          this.dock.ShowJobThroughBrain("job:" + jobId);
        }));
      }
      AutomationProperties.SetName(stackPanel, output.Title + ". " + output.ButtonTitle);
      return stackPanel;
    }

    // Token: 0x06002038 RID: 8248 RVA: 0x006154B4 File Offset: 0x006154B4
    private void TrackAnswer(string answer)
    {
      this.dock.TrackRequestAnswered(answer, this.analyticsSurface, this.content.RailKey, this.content.TaskId);
    }

    // Token: 0x04000F91 RID: 3985
    private const double HorizontalInset = 22.0;

    // Token: 0x04000F92 RID: 3986
    private readonly ArcDock dock;

    // Token: 0x04000F93 RID: 3987
    private readonly ArcCardContent content;

    // Token: 0x04000F94 RID: 3988
    private readonly string analyticsSurface;
  }
}
