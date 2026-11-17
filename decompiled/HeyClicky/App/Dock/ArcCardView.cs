using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.Ink;
using HeyClicky.App.Notch.Surfaces;
using HeyClicky.App.UI;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002E9 RID: 745
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ArcCardView : Grid
  {
    // Token: 0x170005E3 RID: 1507
    // (get) Token: 0x06002004 RID: 8196 RVA: 0x00612184 File Offset: 0x00612184
    public string RailKey
    {
      get
      {
        return this.content.RailKey;
      }
    }

    // Token: 0x170005E4 RID: 1508
    // (get) Token: 0x06002005 RID: 8197 RVA: 0x00612194 File Offset: 0x00612194
    public ArcCardContent Content
    {
      get
      {
        return this.content;
      }
    }

    // Token: 0x06002006 RID: 8198 RVA: 0x0061219C File Offset: 0x0061219C
    public ArcCardView(ArcDock dock, ArcCardContent content)
    {
      ArcCardView <>4__this = this;
      this.dock = dock;
      this.content = content;
      base.Width = 494.0;
      base.Height = 314.0;
      ValueTuple<double, double> valueTuple = StickyPreviewLayout.CornerRadii(null);
      double item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      base.Children.Add(new ShadowLayer(item2, DS.Black(0.35), 20.0, 10.0, 0.0));
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(item2),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.Rgb(0.16, 0.16, 0.16, 1.0)),
          new ValueTuple<double, Color>(1.0, DS.Rgb(0.08, 0.08, 0.08, 1.0))
        }),
        BorderBrush = Ui.Brush(DS.White(0.16)),
        BorderThickness = new Thickness(1.0)
      });
      this.screen = new Grid
      {
        Margin = new Thickness(7.0),
        Width = 480.0,
        Height = 300.0
      };
      this.screen.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(34.0)
      });
      this.screen.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      Grid grid = new Grid
      {
        Children = 
        {
          this.paperFill,
          ArcPaperTexture.Create()
        }
      };
      Grid.SetRowSpan(grid, 2);
      this.screen.Children.Add(grid);
      this.titleBar = new Grid();
      this.titleBar.Children.Add(new Border
      {
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, ArcColors.TitleGreyEdge),
          new ValueTuple<double, Color>(0.5, ArcColors.TitleGreyMiddle),
          new ValueTuple<double, Color>(1.0, ArcColors.TitleGreyEdge)
        })
      });
      this.titleBar.Children.Add(this.titleWash);
      this.titleBar.Children.Add(new Border
      {
        Height = 0.5,
        VerticalAlignment = VerticalAlignment.Bottom,
        Background = Ui.Brush(DS.Black(0.08))
      });
      this.nameLabel = Ui.Text("", 12.5, FontWeights.SemiBold, ArcColors.Ink.WithAlpha(0.85), TextAlignment.Left, 0, false, false);
      this.nameLabel.VerticalAlignment = VerticalAlignment.Center;
      this.nameLabel.TextTrimming = TextTrimming.CharacterEllipsis;
      this.nameLabel.TextWrapping = TextWrapping.NoWrap;
      this.nameDot = Ui.Text("·", 12.5, FontWeights.SemiBold, ArcColors.Ink.WithAlpha(0.35), TextAlignment.Left, 0, false, false);
      this.nameDot.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(this.nameDot, 1);
      this.stateLabel = Ui.Text("", 12.5, FontWeights.SemiBold, ArcColors.Ink, TextAlignment.Left, 0, false, false);
      this.stateLabel.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(this.stateLabel, 2);
      this.stateRow = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        ColumnSpacing = 5.0,
        IsHitTestVisible = false
      };
      for (int i = 0; i < 4; i++)
      {
        this.stateRow.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
      }
      this.stateRow.Children.Add(this.nameLabel);
      this.stateRow.Children.Add(this.nameDot);
      this.stateRow.Children.Add(this.stateLabel);
      this.titleBar.Children.Add(this.stateRow);
      this.titleBar.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        <>4__this.FitName();
      };
      this.closeButton = new CaptionButton(CaptionButton.Kind.Close, 34.0, ArcColors.Ink.WithAlpha(0.8), "Remove from the dock")
      {
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top
      };
      this.closeButton.Click += delegate
      {
        bool canStopWork = <>4__this.CanStopWork;
        string text = "removed_from_dock";
        ArcCardContent arcCardContent = <>4__this.content;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["stops_work"] = canStopWork;
        ArcDock.TrackWindowAction(text, arcCardContent, jsonObject);
        if (canStopWork)
        {
          dock.StopWork(<>4__this.content);
        }
        dock.RemoveFromDock(<>4__this.content.RailKey);
      };
      this.titleBar.Children.Add(this.closeButton);
      this.expandButton = new CaptionButton(CaptionButton.Kind.Maximize, 34.0, ArcColors.Ink.WithAlpha(0.8), "Open in the HeyClicky window")
      {
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top
      };
      this.expandButton.Click += delegate
      {
        ArcDock.TrackWindowAction("opened_in_window", <>4__this.content, null);
        dock.OpenInWindow(<>4__this.content.RailKey);
      };
      this.titleBar.Children.Add(this.expandButton);
      this.screen.Children.Add(this.titleBar);
      this.contentStack = new Grid();
      this.contentStack.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      this.contentStack.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      this.bodyPress = new Pressable(true)
      {
        Children = { this.bodyHost },
        ShowsDefaultHover = false
      };
      this.bodyPress.Click += delegate
      {
        if (<>4__this.OpensOnClick)
        {
          <>4__this.OpenFullResponse("card_click");
        }
      };
      this.contextMenu = this.ContextMenu();
      Ui.ClipRounded(this.bodyPress, 0.0);
      this.contentStack.Children.Add(this.bodyPress);
      Grid.SetRow(this.footerHost, 1);
      this.contentStack.Children.Add(this.footerHost);
      Grid grid2 = new Grid
      {
        Children = { this.contentStack, this.coverHost }
      };
      Ui.ClipRounded(grid2, 0.0);
      Grid.SetRow(grid2, 1);
      this.screen.Children.Add(grid2);
      Ui.ClipRounded(this.screen, item);
      base.Children.Add(this.screen);
      this.Apply(content, true);
    }

    // Token: 0x06002007 RID: 8199 RVA: 0x00612918 File Offset: 0x00612918
    private MenuFlyout ContextMenu()
    {
      MenuFlyout menuFlyout = new MenuFlyout();
      MenuFlyoutItem item = new MenuFlyoutItem();
      item.Click += delegate(object _, RoutedEventArgs _)
      {
        this.OpenFullResponse("context_menu");
      };
      menuFlyout.Items.Add(item);
      menuFlyout.Opening += delegate([Nullable(2)] object _, object _)
      {
        item.Text = (this.dock.CanShowFullResponse(this.content) ? "Full report" : "Show in chat");
      };
      return menuFlyout;
    }

    // Token: 0x170005E5 RID: 1509
    // (get) Token: 0x06002008 RID: 8200 RVA: 0x0061297C File Offset: 0x0061297C
    private bool OpensOnClick
    {
      get
      {
        return !this.content.IsWorking && !this.content.IsFinalizing;
      }
    }

    // Token: 0x06002009 RID: 8201 RVA: 0x0061299C File Offset: 0x0061299C
    private void ApplyOpensOnClick()
    {
      bool opensOnClick = this.OpensOnClick;
      this.bodyPress.Enabled = opensOnClick;
      this.bodyPress.ContextFlyout = (opensOnClick ? this.contextMenu : null);
    }

    // Token: 0x0600200A RID: 8202 RVA: 0x006129D4 File Offset: 0x006129D4
    private void OpenFullResponse(string via)
    {
      if (this.content.State == ArcCardState.Suggested)
      {
        string text = "start_now";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["via"] = via;
        this.TrackArcAction(text, jsonObject);
      }
      else
      {
        JsonObject jsonObject2 = ArcDock.WindowProperties(this.content);
        jsonObject2["action"] = "arc_show_full_response";
        jsonObject2["via"] = via;
        jsonObject2["has_reply"] = this.dock.CanShowFullResponse(this.content);
        ClickyAnalytics.TrackProductEvent("notch_peek_action", this.content.ClickySlug, this.content.ThreadId, jsonObject2);
      }
      this.dock.OpenFullResponse(this.content);
    }

    // Token: 0x0600200B RID: 8203 RVA: 0x00612AA4 File Offset: 0x00612AA4
    private void TrackArcAction(string action, [Nullable(2)] JsonObject properties = null)
    {
      if (!this.isLive)
      {
        return;
      }
      ArcDock.TrackWindowAction(action, this.content, properties);
    }

    // Token: 0x0600200C RID: 8204 RVA: 0x00612ABC File Offset: 0x00612ABC
    public void SetPresentation(bool showsCover, bool isLive, bool animated = true)
    {
      bool flag = showsCover != this.showsCover;
      this.showsCover = showsCover;
      if (this.isLive != isLive)
      {
        this.isLive = isLive;
        this.ApplyTitleBar();
        if (this.shownNote != null)
        {
          this.BuildBody();
        }
      }
      if (!flag)
      {
        return;
      }
      this.BuildCover(animated);
    }

    // Token: 0x0600200D RID: 8205 RVA: 0x00612B0C File Offset: 0x00612B0C
    public void Update(ArcCardContent next)
    {
      this.Apply(next, false);
    }

    // Token: 0x0600200E RID: 8206 RVA: 0x00612B18 File Offset: 0x00612B18
    private void Apply(ArcCardContent next, bool force)
    {
      ArcCardContent arcCardContent = this.content;
      this.content = next;
      this.paperFill.Background = Ui.Brush(next.Paper.Fill);
      this.titleWash.Background = Ui.Brush(next.Paper.TitleWash.WithAlpha(0.62));
      this.footerWash.Background = Ui.Brush(next.Paper.TitleWash.WithAlpha(0.62));
      this.ApplyTitleBar();
      this.ApplyOpensOnClick();
      if (force || next.Shape != this.shape)
      {
        this.shape = next.Shape;
        this.BuildBody();
        this.BuildFooter();
        this.BuildCover(false);
        return;
      }
      if (this.ShowsTourLine() != this.footerShowsTourLine)
      {
        this.BuildFooter();
      }
      PlanRows planRows = this.planRows;
      if (planRows != null)
      {
        planRows.Update(next.Plan, next.LiveLine);
      }
      ArcProgressBar arcProgressBar = this.footerProgress;
      if (arcProgressBar != null)
      {
        arcProgressBar.Update(next.Plan);
      }
      ArcProgressBar arcProgressBar2 = this.coverProgress;
      if (arcProgressBar2 != null)
      {
        arcProgressBar2.Update(next.Plan);
      }
      if (!object.Equals(this.BodyNote(), this.shownNote))
      {
        this.BuildBody();
        return;
      }
      if (this.shownNote == null && this.planRows == null && next.IsWorking && (arcCardContent.CurrentStepLine != next.CurrentStepLine || !ArcCardView.LiveLineLooksSame(arcCardContent.LiveLine, next.LiveLine)))
      {
        bool flag;
        if (next.LiveLine != null)
        {
          ClickyLiveLine liveLine = arcCardContent.LiveLine;
          if (!object.Equals((liveLine != null) ? liveLine.Text : null, next.LiveLine.Text))
          {
            flag = !ArcCardView.IsSameBatch(arcCardContent.LiveLine, next.LiveLine);
            goto IL_01C7;
          }
        }
        flag = false;
        IL_01C7:
        this.liveLineRisesIn = flag;
        this.BuildBody();
        this.liveLineRisesIn = false;
      }
    }

    // Token: 0x0600200F RID: 8207 RVA: 0x00612D00 File Offset: 0x00612D00
    [NullableContext(2)]
    private static bool LiveLineLooksSame(ClickyLiveLine a, ClickyLiveLine b)
    {
      if (object.Equals((a != null) ? a.Text : null, (b != null) ? b.Text : null) && (((a != null) ? a.Icons : null) ?? Array.Empty<ClickyLiveLineIcon>()).SequenceEqual<ClickyLiveLineIcon>(((b != null) ? b.Icons : null) ?? Array.Empty<ClickyLiveLineIcon>()))
      {
        int? num = ((a != null) ? new int?(a.MoreIcons) : null);
        int? num2 = ((b != null) ? new int?(b.MoreIcons) : null);
        return (num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null));
      }
      return false;
    }

    // Token: 0x06002010 RID: 8208 RVA: 0x00612DB8 File Offset: 0x00612DB8
    [NullableContext(2)]
    internal static bool IsSameBatch(ClickyLiveLine a, ClickyLiveLine b)
    {
      string text = ((a != null) ? a.BatchId : null);
      return text != null && ((b != null) ? b.BatchId : null) == text;
    }

    // Token: 0x170005E6 RID: 1510
    // (get) Token: 0x06002011 RID: 8209 RVA: 0x00612DEC File Offset: 0x00612DEC
    private bool IsQuietResult
    {
      get
      {
        if (this.content.IsDone)
        {
          ClickyAnswer answer = this.content.Answer;
          return answer != null && answer.IsQuiet;
        }
        return false;
      }
    }

    // Token: 0x170005E7 RID: 1511
    // (get) Token: 0x06002012 RID: 8210 RVA: 0x00612E14 File Offset: 0x00612E14
    private bool CanRemoveFromDock
    {
      get
      {
        return this.isLive && !this.content.IsFinalizing && this.content.State != ArcCardState.Pinning && (!this.content.IsWorking || this.CanStopWork);
      }
    }

    // Token: 0x170005E8 RID: 1512
    // (get) Token: 0x06002013 RID: 8211 RVA: 0x00612E50 File Offset: 0x00612E50
    private bool CanStopWork
    {
      get
      {
        return !this.content.IsFinalizing && this.content.TaskIsRunning;
      }
    }

    // Token: 0x170005E9 RID: 1513
    // (get) Token: 0x06002014 RID: 8212 RVA: 0x00612E6C File Offset: 0x00612E6C
    private bool CanOpenInWindow
    {
      get
      {
        bool flag = this.isLive;
        if (flag)
        {
          ArcCardState state = this.content.State;
          bool flag2 = state - ArcCardState.Pinning <= 1;
          flag = !flag2;
        }
        return flag;
      }
    }

    // Token: 0x06002015 RID: 8213 RVA: 0x00612EA4 File Offset: 0x00612EA4
    private void FitName()
    {
      double num = ((this.titleBar.ActualWidth > 0.0) ? this.titleBar.ActualWidth : 480.0);
      double num2 = 98.0;
      Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
      this.stateLabel.Measure(size);
      this.nameDot.Measure(size);
      double num3 = this.stateLabel.DesiredSize.Width + this.nameDot.DesiredSize.Width + 2.0 * this.stateRow.ColumnSpacing;
      if (this.stateDots != null)
      {
        this.stateDots.Measure(size);
        num3 += this.stateDots.DesiredSize.Width + this.stateRow.ColumnSpacing;
      }
      this.nameLabel.MaxWidth = Math.Max(0.0, num - 2.0 * num2 - num3);
    }

    // Token: 0x06002016 RID: 8214 RVA: 0x00612FB8 File Offset: 0x00612FB8
    private void ApplyTitleBar()
    {
      string text = (this.IsQuietResult ? "Checked" : (this.content.IsFinalizing ? "Finalizing" : null));
      bool isFinalizing = this.content.IsFinalizing;
      Color color = ((text == null || isFinalizing) ? ArcColors.State(this.content.State) : ArcColors.Ink.WithAlpha(0.5));
      this.stateLabel.Text = text ?? ArcCardStates.Title(this.content.State);
      this.stateLabel.Foreground = Ui.Brush(color);
      string text2 = this.content.Title.Replace('\n', ' ').Trim();
      this.nameLabel.Text = text2;
      this.nameLabel.Visibility = (this.nameDot.Visibility = ((text2.Length > 0) ? Visibility.Visible : Visibility.Collapsed));
      bool flag = ArcCardStates.IsBusy(this.content.State);
      if (flag && this.stateDots == null)
      {
        this.stateDots = new TypingDots(ArcColors.State(this.content.State), 0.5)
        {
          Margin = new Thickness(0.0, 2.0, 0.0, 0.0)
        };
        Grid.SetColumn(this.stateDots, 3);
        this.stateRow.Children.Add(this.stateDots);
      }
      else if (!flag && this.stateDots != null)
      {
        this.stateRow.Children.Remove(this.stateDots);
        this.stateDots = null;
      }
      AutomationProperties.SetName(this.stateRow, (text2.Length > 0) ? (text2 + ", " + this.stateLabel.Text) : this.stateLabel.Text);
      this.FitName();
      bool canRemoveFromDock = this.CanRemoveFromDock;
      this.closeButton.Visibility = (canRemoveFromDock ? Visibility.Visible : Visibility.Collapsed);
      bool canOpenInWindow = this.CanOpenInWindow;
      this.expandButton.Visibility = (canOpenInWindow ? Visibility.Visible : Visibility.Collapsed);
      this.expandButton.Margin = new Thickness(0.0, 0.0, canRemoveFromDock ? 46.0 : 0.0, 0.0);
      string text3 = (this.CanStopWork ? "Stop and remove from the dock" : "Remove from the dock");
      ToolTipService.SetToolTip(this.closeButton, text3);
      AutomationProperties.SetName(this.closeButton, text3);
    }

    // Token: 0x06002017 RID: 8215 RVA: 0x00613248 File Offset: 0x00613248
    private void BuildBody()
    {
      this.bodyHost.Children.Clear();
      this.planRows = null;
      this.face = null;
      this.shownNote = this.BodyNote();
      if (this.content.IsWorking)
      {
        ArcLiveNote arcLiveNote = this.shownNote;
        if (arcLiveNote != null)
        {
          this.bodyHost.Children.Add(ArcDock.TopOfNote(arcLiveNote, true, this.isLive));
          return;
        }
      }
      if (this.content.IsDone)
      {
        ClickyAnswer answer = this.content.Answer;
        if (answer != null)
        {
          this.bodyHost.Children.Add(this.AnswerFace(answer));
          return;
        }
      }
      if (this.content.IsDone)
      {
        ArcLiveNote arcLiveNote2 = this.shownNote;
        if (arcLiveNote2 != null)
        {
          this.bodyHost.Children.Add(this.DoneNote(arcLiveNote2));
          return;
        }
      }
      if (this.content.IsDone)
      {
        string summary = this.content.Summary;
        if (summary != null)
        {
          this.bodyHost.Children.Add(this.AnswerFace(new ClickyAnswer
          {
            Line = ClickyAnswer.Clean(summary, 220)
          }));
          return;
        }
      }
      this.bodyHost.Children.Add((this.content.State == ArcCardState.Suggested) ? this.SuggestedBody() : this.RegularBody());
    }

    // Token: 0x06002018 RID: 8216 RVA: 0x00613390 File Offset: 0x00613390
    [NullableContext(2)]
    private ArcLiveNote BodyNote()
    {
      if (!this.content.IsWorking && (!this.content.IsDone || this.content.Answer != null))
      {
        return null;
      }
      return this.dock.LiveNote(this.content);
    }

    // Token: 0x06002019 RID: 8217 RVA: 0x006133CC File Offset: 0x006133CC
    private FrameworkElement DoneNote(ArcLiveNote note)
    {
      Grid grid = new Grid();
      FileCardStack fileCardStack = this.CardFiles();
      ClickyInkTopOfNote clickyInkTopOfNote = ArcDock.TopOfNote(note, false, this.isLive);
      clickyInkTopOfNote.Margin = new Thickness(0.0, 0.0, (fileCardStack == null) ? 0.0 : 132.0, 0.0);
      grid.Children.Add(clickyInkTopOfNote);
      if (fileCardStack != null)
      {
        grid.Children.Add(fileCardStack);
      }
      return grid;
    }

    // Token: 0x0600201A RID: 8218 RVA: 0x00613450 File Offset: 0x00613450
    private FrameworkElement AnswerFace(ClickyAnswer answer)
    {
      Grid grid = new Grid();
      FileCardStack fileCardStack = this.CardFiles();
      this.face = new ResultCardFace(this.content.Title, answer, this.content.Paper.Ink, this.content.Paper.TitleWash, this.dock.ResultCardWhenLine(this.content, answer), this.isDraftCopied, this.sentChoice, delegate(ClickyAnswer.Choice choice)
      {
        if (this.sentChoice != null)
        {
          return;
        }
        this.sentChoice = choice.Reply;
        ResultCardFace resultCardFace = this.face;
        if (resultCardFace != null)
        {
          resultCardFace.Update(this.isDraftCopied, this.sentChoice);
        }
        this.dock.AnswerResultCardQuestion(this.content, choice);
      }, delegate(string target)
      {
        string text = "result_link_opened";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(target);
        this.TrackArcAction(text, jsonObject);
        this.dock.OpenResultCardTarget(target, this.content);
      })
      {
        Margin = new Thickness(0.0, 0.0, (fileCardStack == null) ? 0.0 : 132.0, 0.0)
      };
      grid.Children.Add(this.face);
      if (fileCardStack != null)
      {
        grid.Children.Add(fileCardStack);
      }
      if (!Anim.ReduceMotion)
      {
        this.face.Opacity = 0.0;
        this.face.Translation = new Vector3(0f, 8f, 0f);
        Anim.Opacity(this.face, 1f, 0.3, null, 0.0);
        Anim.Translate(this.face, Vector3.Zero, 0.3, null);
      }
      return grid;
    }

    // Token: 0x0600201B RID: 8219 RVA: 0x006135B4 File Offset: 0x006135B4
    [NullableContext(2)]
    private FileCardStack CardFiles()
    {
      return FileCardStack.Make(ArcCardContentSource.CardEntries(this.content), this.content.WorkspacePath, delegate(string entry)
      {
        string text = "file_opened";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["via"] = "file_card";
        jsonObject["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(entry);
        this.TrackArcAction(text, jsonObject);
        this.dock.OpenCardFile(entry, this.content);
      });
    }

    // Token: 0x0600201C RID: 8220 RVA: 0x006135E0 File Offset: 0x006135E0
    private FrameworkElement SuggestedBody()
    {
      FirstThatFits firstThatFits = new FirstThatFits
      {
        Margin = new Thickness(26.0, 20.0, 26.0, 10.0)
      };
      foreach (ValueTuple<double, double, double> valueTuple in ArcCover.Suggested)
      {
        firstThatFits.Children.Add(this.SuggestedBlock(valueTuple));
      }
      UIElementCollection children = firstThatFits.Children;
      ValueTuple<double, double, double>[] suggested2 = ArcCover.Suggested;
      children.Add(new ShrinkToFit(this.SuggestedBlock(suggested2[suggested2.Length - 1]), 0.6, 0));
      return firstThatFits;
    }

    // Token: 0x0600201D RID: 8221 RVA: 0x00613680 File Offset: 0x00613680
    [NullableContext(0)]
    [return: Nullable(1)]
    private FrameworkElement SuggestedBlock([TupleElementNames(new string[] { "Title", "Body", "Spacing" })] ValueTuple<double, double, double> sizes)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = sizes.Item3
      };
      TextBlock textBlock = Ui.Text(this.content.Title, sizes.Item1, FontWeights.Bold, this.content.Paper.Ink, TextAlignment.Left, 0, true, false);
      textBlock.FontFamily = DS.Fonts.Display;
      stackPanel.Children.Add(textBlock);
      string summary = this.content.Summary;
      if (summary != null)
      {
        stackPanel.Children.Add(Ui.Text(ArcCover.CardPitch(summary, 200), sizes.Item2, FontWeights.Medium, this.content.Paper.Ink.WithAlpha(0.72), TextAlignment.Left, 5, true, false));
      }
      return stackPanel;
    }

    // Token: 0x0600201E RID: 8222 RVA: 0x00613744 File Offset: 0x00613744
    private FrameworkElement RegularBody()
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 12.0,
        Margin = new Thickness(26.0, 20.0, 26.0, 10.0)
      };
      TextBlock textBlock = Ui.Text(this.content.Title, 26.0, FontWeights.Bold, this.content.Paper.Ink, TextAlignment.Left, 2, false, false);
      textBlock.FontFamily = DS.Fonts.Display;
      stackPanel.Children.Add(new ShrinkToFit(textBlock, 0.75, 2));
      Color ink = this.content.Paper.Ink;
      if (this.content.IsWorking)
      {
        stackPanel.Children.Add(this.WorkingBody());
      }
      else
      {
        if (this.content.IsDone)
        {
          using (IEnumerator<FrameworkElement> enumerator = this.DoneBody().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              FrameworkElement frameworkElement = enumerator.Current;
              stackPanel.Children.Add(frameworkElement);
            }
            return stackPanel;
          }
        }
        CardFooterRequest request = this.content.Request;
        if (request != null)
        {
          stackPanel.Children.Add(Ui.Text(request.Detail, 20.0, FontWeights.Medium, ink.WithAlpha(0.72), TextAlignment.Left, 3, false, false));
        }
        else if (this.content.State == ArcCardState.Pinning)
        {
          stackPanel.Children.Add(Ui.Text("Pinning…", 20.0, FontWeights.Medium, ink.WithAlpha(0.6), TextAlignment.Left, 0, false, false));
        }
        else
        {
          stackPanel.Children.Add(Ui.Text("Nothing running. Hold the voice keys and give it something to do.", 19.0, FontWeights.Medium, ink.WithAlpha(0.55), TextAlignment.Left, 2, false, false));
        }
      }
      return stackPanel;
    }

    // Token: 0x0600201F RID: 8223 RVA: 0x0061394C File Offset: 0x0061394C
    private FrameworkElement WorkingBody()
    {
      Color ink = this.content.Paper.Ink;
      ClickyArcPlan plan = this.content.Plan;
      if (plan != null && !plan.IsEmpty)
      {
        this.planRows = new PlanRows(plan, this.content.LiveLine, ink, false);
        return this.planRows;
      }
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0
      };
      FrameworkElement frameworkElement = ActivityIndicators.ThinkingDots(ink.WithAlpha(0.6), 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0);
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Children.Add(frameworkElement);
      ClickyLiveLine liveLine = this.content.LiveLine;
      if (liveLine != null)
      {
        stackPanel.Children.Add(new LiveLineView(liveLine, ink, 18.0, this.liveLineRisesIn)
        {
          VerticalAlignment = VerticalAlignment.Center
        });
      }
      else
      {
        TextBlock textBlock = Ui.Text(this.content.CurrentStepLine ?? "Getting started…", 19.0, FontWeights.Medium, ink.WithAlpha(0.65), TextAlignment.Left, 2, false, false);
        textBlock.VerticalAlignment = VerticalAlignment.Center;
        textBlock.MaxWidth = 390.0;
        stackPanel.Children.Add(textBlock);
      }
      return stackPanel;
    }

    // Token: 0x06002020 RID: 8224 RVA: 0x00613AD8 File Offset: 0x00613AD8
    private IEnumerable<FrameworkElement> DoneBody()
    {
      ArcCardView.<DoneBody>d__67 <DoneBody>d__ = new ArcCardView.<DoneBody>d__67(-2);
      <DoneBody>d__.<>4__this = this;
      return <DoneBody>d__;
    }

    // Token: 0x06002021 RID: 8225 RVA: 0x00613AE8 File Offset: 0x00613AE8
    private bool ShowsTourLine()
    {
      return this.content.State == ArcCardState.Suggested && this.dock.Host.IsTourRunning();
    }

    // Token: 0x06002022 RID: 8226 RVA: 0x00613B10 File Offset: 0x00613B10
    private void BuildFooter()
    {
      this.footerProgress = null;
      this.footerShowsTourLine = this.ShowsTourLine();
      Grid grid = new Grid();
      grid.Children.Add(new Border
      {
        Background = Ui.Horizontal(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, ArcColors.TitleGreyEdge),
          new ValueTuple<double, Color>(0.5, ArcColors.TitleGreyMiddle),
          new ValueTuple<double, Color>(1.0, ArcColors.TitleGreyEdge)
        })
      });
      Panel panel = this.footerWash.Parent as Panel;
      if (panel != null)
      {
        panel.Children.Remove(this.footerWash);
      }
      grid.Children.Add(this.footerWash);
      grid.Children.Add(new Border
      {
        Height = 1.0,
        VerticalAlignment = VerticalAlignment.Top,
        Background = Ui.Brush(DS.Black(0.09))
      });
      FrameworkElement frameworkElement = null;
      if (this.content.IsWorking)
      {
        Grid grid2 = new Grid
        {
          ColumnSpacing = 12.0,
          Padding = new Thickness(22.0, (double)(this.CanStopWork ? 8 : 12), (double)(this.CanStopWork ? 14 : 22), (double)(this.CanStopWork ? 8 : 12))
        };
        grid2.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = new GridLength(1.0, GridUnitType.Star)
        });
        grid2.ColumnDefinitions.Add(new ColumnDefinition
        {
          Width = GridLength.Auto
        });
        this.footerProgress = new ArcProgressBar(this.content.Plan, this.content.Paper.Ink, false, true, false)
        {
          VerticalAlignment = VerticalAlignment.Center
        };
        grid2.Children.Add(this.footerProgress);
        if (this.CanStopWork)
        {
          ArcStopButton arcStopButton = new ArcStopButton(delegate
          {
            this.TrackArcAction("stop", null);
            this.dock.StopWork(this.content);
          });
          Grid.SetColumn(arcStopButton, 1);
          grid2.Children.Add(arcStopButton);
        }
        frameworkElement = grid2;
      }
      else
      {
        CardFooterRequest request = this.content.Request;
        if (request != null)
        {
          frameworkElement = new FooterRequestRow(this.dock, this.content, request, new double?((double)34), "dock_window", false);
        }
        else if (this.footerShowsTourLine)
        {
          Color color = DS.Hex(3112182U, 1.0);
          Grid grid3 = new Grid();
          grid3.ColumnSpacing = 7.0;
          grid3.MinHeight = 32.0;
          grid3.Padding = new Thickness(18.0, 9.0, 18.0, 9.0);
          grid3.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = GridLength.Auto
          });
          grid3.ColumnDefinitions.Add(new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          FrameworkElement frameworkElement2 = ArcSymbols.Make("clock", 13.0, color, true);
          frameworkElement2.VerticalAlignment = VerticalAlignment.Center;
          grid3.Children.Add(frameworkElement2);
          FrameworkElement frameworkElement3 = Ui.Shrink(Ui.Text("Start it from your notch after the tour", 14.0, FontWeights.SemiBold, color, TextAlignment.Left, 0, false, false), HorizontalAlignment.Left, 0.8);
          frameworkElement3.VerticalAlignment = VerticalAlignment.Center;
          Grid.SetColumn(frameworkElement3, 1);
          grid3.Children.Add(frameworkElement3);
          frameworkElement = grid3;
        }
        else if (this.content.State == ArcCardState.Suggested)
        {
          GelButton gelButton = ArcButtons.Gel("Start now", "play.fill", GelAppearance.PrimaryBlue, new double?(32.0), new double?(13.5), GelButton.Sizing.Normal, delegate
          {
            string text = "start_now";
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["via"] = "button";
            this.TrackArcAction(text, jsonObject);
            this.dock.OpenFullResponse(this.content);
          });
          frameworkElement = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 8.0,
            Padding = new Thickness(16.0, 9.0, 16.0, 9.0),
            Children = { gelButton }
          };
        }
        else if (this.content.IsDone || this.dock.CanShowFullResponse(this.content))
        {
          StackPanel stackPanel = new StackPanel
          {
            Orientation = Orientation.Horizontal,
            Spacing = 8.0,
            Padding = new Thickness(16.0, 9.0, 16.0, 9.0)
          };
          bool flag = this.NextStepButton() != null;
          if (flag)
          {
            stackPanel.Children.Add(this.NextStepButton());
          }
          if (this.dock.CanShowFullResponse(this.content))
          {
            stackPanel.Children.Add(ArcButtons.Gel("Full report", "doc.text", flag ? GelAppearance.SecondaryWhite : GelAppearance.PrimaryBlue, new double?(32.0), new double?(13.5), GelButton.Sizing.Normal, delegate
            {
              this.OpenFullResponse("full_report_button");
            }));
          }
          frameworkElement = stackPanel;
        }
      }
      if (frameworkElement == null)
      {
        this.footerHost.Child = null;
        return;
      }
      grid.Children.Add(frameworkElement);
      this.footerHost.Child = grid;
    }

    // Token: 0x06002023 RID: 8227 RVA: 0x00614074 File Offset: 0x00614074
    [NullableContext(2)]
    private FrameworkElement NextStepButton()
    {
      return ArcNextStep.Button(this.dock, this.content, this.isDraftCopied, delegate
      {
        this.isDraftCopied = true;
        ResultCardFace resultCardFace = this.face;
        if (resultCardFace != null)
        {
          resultCardFace.Update(this.isDraftCopied, this.sentChoice);
        }
        this.BuildFooter();
      }, delegate(string action, JsonObject properties)
      {
        this.TrackArcAction(action, properties);
      }, true, 32.0, 13.5);
    }

    // Token: 0x06002024 RID: 8228 RVA: 0x006140C4 File Offset: 0x006140C4
    private void BuildCover(bool animated)
    {
      this.coverProgress = null;
      bool flag = this.showsCover;
      SwiftSpring swiftSpring = new SwiftSpring(0.4, 0.82);
      this.contentStack.CenterPoint = new Vector3(240f, 133f, 0f);
      if (animated && !Anim.ReduceMotion)
      {
        Anim.ScaleSpring(this.contentStack, flag ? 0.97f : 1f, swiftSpring, null);
      }
      else
      {
        this.contentStack.Scale = (flag ? new Vector3(0.97f, 0.97f, 1f) : Vector3.One);
      }
      if (flag)
      {
        this.coverHost.Children.Clear();
        FrameworkElement frameworkElement = this.Cover();
        this.coverHost.Children.Add(frameworkElement);
        if (animated)
        {
          frameworkElement.Opacity = 0.0;
          frameworkElement.Scale = new Vector3(1.06f, 1.06f, 1f);
          frameworkElement.Translation = new Vector3(0f, -14f, 0f);
          ArcCardView.Lift(frameworkElement, false, swiftSpring);
        }
        return;
      }
      if (this.coverHost.Children.Count == 0)
      {
        return;
      }
      if (!animated)
      {
        this.coverHost.Children.Clear();
        return;
      }
      List<UIElement> leaving = this.coverHost.Children.ToList<UIElement>();
      foreach (UIElement uielement in leaving.OfType<UIElement>())
      {
        ArcCardView.Lift(uielement, true, swiftSpring);
      }
      DockTimer.After(0.45, delegate
      {
        if (!this.showsCover)
        {
          foreach (UIElement uielement2 in leaving)
          {
            this.coverHost.Children.Remove(uielement2);
          }
        }
      });
    }

    // Token: 0x06002025 RID: 8229 RVA: 0x0061429C File Offset: 0x0061429C
    private static void Lift(UIElement e, bool off, SwiftSpring spring)
    {
      e.CenterPoint = new Vector3(240f, 133f, 0f);
      if (Anim.ReduceMotion)
      {
        Anim.Opacity(e, !off, 0.18, null, 0.0);
        return;
      }
      Anim.OpacitySpring(e, !off, spring);
      Anim.ScaleSpring(e, off ? 1.06f : 1f, spring, null);
      Anim.TranslationSpring(e, new Vector3(0f, (float)(off ? (-14) : 0), 0f), spring);
    }

    // Token: 0x06002026 RID: 8230 RVA: 0x00614334 File Offset: 0x00614334
    private FrameworkElement Cover()
    {
      Grid grid = new Grid();
      grid.Children.Add(new Border
      {
        Background = Ui.Brush(this.content.Paper.Fill),
        Opacity = 0.95
      });
      grid.Children.Add(new Grid
      {
        Opacity = 0.95,
        Children = { ArcPaperTexture.Create() }
      });
      FirstThatFits firstThatFits = new FirstThatFits
      {
        Margin = new Thickness(30.0, 20.0, 30.0, 20.0),
        VerticalAlignment = VerticalAlignment.Center
      };
      ValueTuple<double, double>[] array = ArcCover.LevelsFor(this.content.State);
      foreach (ValueTuple<double, double> valueTuple in array)
      {
        firstThatFits.Children.Add(this.CoverStack(valueTuple));
      }
      UIElementCollection children = firstThatFits.Children;
      ValueTuple<double, double>[] array3 = array;
      children.Add(new ShrinkToFit(this.CoverStack(array3[array3.Length - 1]), 0.6, 0));
      grid.Children.Add(firstThatFits);
      string text = (this.content.IsFinalizing ? "Finalizing" : ArcCover.Title(this.content.State));
      AutomationProperties.SetName(grid, text + ". " + this.content.Title);
      return grid;
    }

    // Token: 0x06002027 RID: 8231 RVA: 0x006144B0 File Offset: 0x006144B0
    [NullableContext(0)]
    [return: Nullable(1)]
    private FrameworkElement CoverStack([TupleElementNames(new string[] { "State", "Title" })] ValueTuple<double, double> level)
    {
      bool flag = ArcCover.IsQuiet(this.content.State);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      double num = (flag ? 24.0 : level.Item1);
      Color color = ArcColors.State(this.content.State);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = num * 0.28,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      string text = ArcCover.Symbol(this.content.State);
      if (text != null)
      {
        stackPanel2.Children.Add(ArcSymbols.Make(text, num * 0.72, color, true));
      }
      TextBlock textBlock = Ui.Text(this.content.IsFinalizing ? "Finalizing" : ArcCover.Title(this.content.State), num, FontWeights.ExtraBold, color, TextAlignment.Left, 0, false, false);
      textBlock.FontFamily = DS.Fonts.Display;
      stackPanel2.Children.Add(Ui.Shrink(textBlock, HorizontalAlignment.Left, 0.6));
      stackPanel.Children.Add(stackPanel2);
      TextBlock textBlock2 = Ui.Text(this.content.Title, level.Item2, FontWeights.ExtraBold, this.content.Paper.Ink.WithAlpha(flag ? 1.0 : 0.85), TextAlignment.Center, 0, true, false);
      textBlock2.FontFamily = DS.Fonts.Display;
      textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
      stackPanel.Children.Add(textBlock2);
      if (this.content.IsWorking)
      {
        ClickyArcPlan plan = this.content.Plan;
        Color ink = this.content.Paper.Ink;
        bool flag2 = true;
        ClickyArcPlan plan2 = this.content.Plan;
        ArcProgressBar arcProgressBar = new ArcProgressBar(plan, ink, flag2, plan2 != null && !plan2.IsEmpty, false)
        {
          MaxWidth = 340.0,
          Margin = new Thickness(0.0, 8.0, 0.0, 0.0),
          HorizontalAlignment = HorizontalAlignment.Stretch
        };
        if (this.coverProgress == null)
        {
          this.coverProgress = arcProgressBar;
        }
        stackPanel.Children.Add(arcProgressBar);
      }
      return stackPanel;
    }

    // Token: 0x04000F67 RID: 3943
    private const double TitleBarHeight = 34.0;

    // Token: 0x04000F68 RID: 3944
    private readonly ArcDock dock;

    // Token: 0x04000F69 RID: 3945
    private readonly Grid screen;

    // Token: 0x04000F6A RID: 3946
    private readonly Grid titleBar;

    // Token: 0x04000F6B RID: 3947
    private readonly TextBlock nameLabel;

    // Token: 0x04000F6C RID: 3948
    private readonly TextBlock nameDot;

    // Token: 0x04000F6D RID: 3949
    private readonly TextBlock stateLabel;

    // Token: 0x04000F6E RID: 3950
    private readonly Grid stateRow;

    // Token: 0x04000F6F RID: 3951
    [Nullable(2)]
    private TypingDots stateDots;

    // Token: 0x04000F70 RID: 3952
    private readonly CaptionButton closeButton;

    // Token: 0x04000F71 RID: 3953
    private readonly CaptionButton expandButton;

    // Token: 0x04000F72 RID: 3954
    private readonly Grid bodyHost = new Grid();

    // Token: 0x04000F73 RID: 3955
    private readonly Border footerHost = new Border();

    // Token: 0x04000F74 RID: 3956
    private readonly Grid contentStack;

    // Token: 0x04000F75 RID: 3957
    private readonly Grid coverHost = new Grid
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000F76 RID: 3958
    private readonly Pressable bodyPress;

    // Token: 0x04000F77 RID: 3959
    private readonly MenuFlyout contextMenu;

    // Token: 0x04000F78 RID: 3960
    private readonly Border paperFill = new Border();

    // Token: 0x04000F79 RID: 3961
    private readonly Border titleWash = new Border();

    // Token: 0x04000F7A RID: 3962
    private readonly Border footerWash = new Border();

    // Token: 0x04000F7B RID: 3963
    private ArcCardContent content;

    // Token: 0x04000F7C RID: 3964
    [Nullable(2)]
    private string shape;

    // Token: 0x04000F7D RID: 3965
    private bool showsCover;

    // Token: 0x04000F7E RID: 3966
    private bool isLive;

    // Token: 0x04000F7F RID: 3967
    [Nullable(2)]
    private ArcProgressBar footerProgress;

    // Token: 0x04000F80 RID: 3968
    [Nullable(2)]
    private ArcProgressBar coverProgress;

    // Token: 0x04000F81 RID: 3969
    [Nullable(2)]
    private PlanRows planRows;

    // Token: 0x04000F82 RID: 3970
    private bool liveLineRisesIn;

    // Token: 0x04000F83 RID: 3971
    [Nullable(2)]
    private ResultCardFace face;

    // Token: 0x04000F84 RID: 3972
    [Nullable(2)]
    private string sentChoice;

    // Token: 0x04000F85 RID: 3973
    private bool isDraftCopied;

    // Token: 0x04000F86 RID: 3974
    [Nullable(2)]
    private ArcLiveNote shownNote;

    // Token: 0x04000F87 RID: 3975
    private bool footerShowsTourLine;
  }
}
