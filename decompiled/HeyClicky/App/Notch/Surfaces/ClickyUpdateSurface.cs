using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Portraits;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015A RID: 346
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyUpdateSurface : NotchSurface
  {
    // Token: 0x17000378 RID: 888
    // (get) Token: 0x0600130D RID: 4877 RVA: 0x0059D21C File Offset: 0x0059D21C
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x0600130E RID: 4878 RVA: 0x0059D224 File Offset: 0x0059D224
    public ClickyUpdateSurface()
    {
      this.content.Margin = new Thickness(12.0, ClickyUpdateLayout.ContentTopPadding, 12.0, 14.0);
      this.view.Children.Add(this.content);
      this.row.Margin = new Thickness(0.0, -2.0, 12.0, 0.0);
      this.view.Children.Add(this.row);
    }

    // Token: 0x0600130F RID: 4879 RVA: 0x0059D300 File Offset: 0x0059D300
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      return liveEvent != null && liveEvent.Event is NotchLiveEvent.ClickyUpdate;
    }

    // Token: 0x06001310 RID: 4880 RVA: 0x0059D328 File Offset: 0x0059D328
    public override void Update(NotchPresentation p)
    {
      this.Build();
    }

    // Token: 0x06001311 RID: 4881 RVA: 0x0059D330 File Offset: 0x0059D330
    protected override void OnAttached()
    {
      this.presenter = base.Host.Services.Presenter as NotchPresenter;
      if (this.presenter != null)
      {
        this.presenter.Updates.RemainingChanged += this.OnRemaining;
        this.presenter.Updates.NoteShowing(true, base.Host.IsHovering);
      }
      base.Host.HoverChanged += this.OnHover;
      INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
      if (updateContent != null)
      {
        updateContent.Changed += this.OnContentChanged;
      }
      this.Build();
    }

    // Token: 0x06001312 RID: 4882 RVA: 0x0059D3D0 File Offset: 0x0059D3D0
    public override void Detached()
    {
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter != null)
      {
        notchPresenter.Updates.NoteShowing(false, false);
      }
      if (this.presenter != null)
      {
        this.presenter.Updates.RemainingChanged -= this.OnRemaining;
      }
      base.Host.HoverChanged -= this.OnHover;
      INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
      if (updateContent != null)
      {
        updateContent.Changed -= this.OnContentChanged;
      }
    }

    // Token: 0x06001313 RID: 4883 RVA: 0x0059D44C File Offset: 0x0059D44C
    private void OnHover(bool hovering)
    {
      NotchPresenter notchPresenter = this.presenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.Updates.NoteShowing(true, hovering);
    }

    // Token: 0x06001314 RID: 4884 RVA: 0x0059D468 File Offset: 0x0059D468
    private void OnContentChanged()
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        NotchUpdate notchUpdate = this.shown;
        if (notchUpdate != null && notchUpdate.Kind != NotchUpdateKind.Done)
        {
          INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
          NotchUpdateContent notchUpdateContent = ((updateContent != null) ? updateContent.Content(notchUpdate.RailKey) : null);
          if (notchUpdateContent != null && notchUpdateContent.Request == null)
          {
            NotchPresenter notchPresenter = this.presenter;
            if (notchPresenter != null)
            {
              notchPresenter.Updates.Track("request_resolved", null);
            }
            NotchPresenter notchPresenter2 = this.presenter;
            if (notchPresenter2 == null)
            {
              return;
            }
            notchPresenter2.Updates.DismissCurrent();
            return;
          }
        }
        this.Build();
      });
    }

    // Token: 0x06001315 RID: 4885 RVA: 0x0059D488 File Offset: 0x0059D488
    private void OnRemaining()
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        if (this.close != null)
        {
          NotchPresenter notchPresenter = this.presenter;
          TimeSpan? timeSpan = ((notchPresenter != null) ? notchPresenter.Updates.RemainingTime : null);
          if (timeSpan != null)
          {
            TimeSpan valueOrDefault = timeSpan.GetValueOrDefault();
            this.close.GlideTo(Math.Clamp(valueOrDefault.TotalSeconds / NotchUpdates.AutoDismiss.TotalSeconds, 0.0, 1.0), 0.1);
            return;
          }
        }
      });
    }

    // Token: 0x06001316 RID: 4886 RVA: 0x0059D4A8 File Offset: 0x0059D4A8
    private unsafe static string Look(NotchUpdate update, NotchUpdateContent card)
    {
      string text = "|";
      <>y__InlineArray11<object> <>y__InlineArray = default(<>y__InlineArray11<object>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 0) = update.RailKey;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 1) = update.Kind;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 2) = update.ClosesOnItsOwn;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 3) = card.Title;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 4) = card.Summary;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 5) = card.IsStopped;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 6) = card.RoutineBadge;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 7) = card.RoutineTitle;
      ref object ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 8);
      NotchUpdateReadyOutput readyOutput = card.ReadyOutput;
      ptr = ((readyOutput != null) ? readyOutput.ToString() : null);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 9) = string.Join(",", card.Files);
      ref object ptr2 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray11<object>, object>(ref <>y__InlineArray, 10);
      NotchUpdateRequest request = card.Request;
      string text2;
      if (request == null)
      {
        text2 = "";
      }
      else
      {
        string[] array = new string[5];
        array[0] = request.Title;
        array[1] = "/";
        array[2] = request.Detail;
        array[3] = "/";
        array[4] = string.Join(",", request.Buttons.Select<NotchUpdateButton, string>((NotchUpdateButton b) => b.Title + b.IsPrimary.ToString() + RuntimeHelpers.GetHashCode(b.Action).ToString()));
        text2 = string.Concat(array);
      }
      ptr2 = text2;
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray11<object>, object>(in <>y__InlineArray, 11));
    }

    // Token: 0x06001317 RID: 4887 RVA: 0x0059D60C File Offset: 0x0059D60C
    private void Build()
    {
      NotchPresenter notchPresenter = this.presenter;
      NotchUpdate notchUpdate = ((notchPresenter != null) ? notchPresenter.Updates.Current : null);
      NotchUpdateContent notchUpdateContent;
      if (notchUpdate != null)
      {
        INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
        notchUpdateContent = ((updateContent != null) ? updateContent.Content(notchUpdate.RailKey) : null);
      }
      else
      {
        notchUpdateContent = null;
      }
      NotchUpdateContent notchUpdateContent2 = notchUpdateContent;
      string text = ((notchUpdate == null || notchUpdateContent2 == null) ? null : ClickyUpdateSurface.Look(notchUpdate, notchUpdateContent2));
      if (text != null && text == this.builtLook && notchUpdate == this.shown)
      {
        return;
      }
      this.builtLook = text;
      this.content.Children.Clear();
      this.row.Children.Clear();
      this.shown = notchUpdate;
      if (this.shown != null && notchUpdateContent2 != null)
      {
        NotchUpdateContent notchUpdateContent3 = notchUpdateContent2;
        this.close = new RingCloseButton(this.shown.ClosesOnItsOwn)
        {
          HorizontalAlignment = HorizontalAlignment.Right,
          VerticalAlignment = VerticalAlignment.Center
        };
        this.close.Click += delegate
        {
          NotchPresenter notchPresenter3 = this.presenter;
          if (notchPresenter3 != null)
          {
            notchPresenter3.Updates.Track("dismissed", null);
          }
          NotchPresenter notchPresenter4 = this.presenter;
          if (notchPresenter4 == null)
          {
            return;
          }
          notchPresenter4.Updates.DismissCurrent();
        };
        this.close.Tip("Dismiss");
        this.row.Children.Add(this.close);
        NotchPresenter notchPresenter2 = this.presenter;
        TimeSpan? timeSpan = ((notchPresenter2 != null) ? notchPresenter2.Updates.RemainingTime : null);
        if (timeSpan != null)
        {
          TimeSpan valueOrDefault = timeSpan.GetValueOrDefault();
          this.close.SetProgress(Math.Clamp(valueOrDefault.TotalSeconds / NotchUpdates.AutoDismiss.TotalSeconds, 0.0, 1.0));
        }
        if (this.shown.Kind != NotchUpdateKind.Done)
        {
          NotchUpdateRequest request = notchUpdateContent3.Request;
          if (request != null)
          {
            this.content.Children.Add(this.Ask(notchUpdateContent3, request));
            goto IL_01BC;
          }
        }
        this.content.Children.Add(this.Finished(this.shown, notchUpdateContent3));
        IL_01BC:
        this.TrackCardShownIfNeeded(this.shown, notchUpdateContent3);
        return;
      }
    }

    // Token: 0x06001318 RID: 4888 RVA: 0x0059D7E4 File Offset: 0x0059D7E4
    private void TrackCardShownIfNeeded(NotchUpdate update, NotchUpdateContent card)
    {
      if (this.presenter != null)
      {
        INotchUpdateContentSource updateContent = NotchExtensions.UpdateContent;
        ValueTuple<string, JsonObject>? valueTuple = ((updateContent != null) ? updateContent.ShownAnalytics(update.RailKey, update.Kind) : null);
        if (valueTuple != null)
        {
          ValueTuple<string, JsonObject> valueOrDefault = valueTuple.GetValueOrDefault();
          JsonObject item = valueOrDefault.Item2;
          JsonNode jsonNode = item["output_kind"];
          this.outputAnalyticsKind = ((jsonNode != null) ? jsonNode.GetValue<string>() : null);
          if (update.Kind == NotchUpdateKind.Done)
          {
            item["has_preview"] = ClickyUpdateSurface.PreviewEntry(update, card) != null;
            item["has_summary"] = ClickyUpdateLayout.SummaryText(card) != null;
          }
          this.presenter.Updates.TrackShownIfNeeded(valueOrDefault.Item1, item);
          return;
        }
      }
    }

    // Token: 0x06001319 RID: 4889 RVA: 0x0059D8B0 File Offset: 0x0059D8B0
    [return: Nullable(2)]
    private static string PreviewEntry(NotchUpdate update, NotchUpdateContent card)
    {
      if (update.Kind != NotchUpdateKind.Done)
      {
        return null;
      }
      return card.Files.FirstOrDefault<string>((string f) => !ArtifactPaths.IsWebLink(f) && File.Exists(f));
    }

    // Token: 0x0600131A RID: 4890 RVA: 0x0059D8E8 File Offset: 0x0059D8E8
    private FrameworkElement Ask(NotchUpdateContent card, NotchUpdateRequest request)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0
      };
      stackPanel.Children.Add(Ui.Text(request.Title, 14.0, FontWeights.SemiBold, DS.White(0.95), TextAlignment.Left, 1, false, false));
      TextBlock textBlock = Ui.Text(request.Detail, 11.0, FontWeights.Normal, DS.White(0.62), TextAlignment.Left, 0, true, false);
      textBlock.MaxLines = 4;
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      stackPanel.Children.Add(textBlock);
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
      UIElementCollection children = grid.Children;
      UIElement uielement;
      if (request.IsChromeAsk)
      {
        FrameworkElement frameworkElement = ChromeIcon.Make(32.0);
        if (frameworkElement != null)
        {
          uielement = frameworkElement;
          goto IL_010F;
        }
      }
      uielement = ClickyUpdateSurface.Chip(card);
      IL_010F:
      children.Add(uielement);
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        Margin = new Thickness(42.0, 0.0, 0.0, 0.0)
      };
      using (IEnumerator<NotchUpdateButton> enumerator = request.Buttons.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          NotchUpdateButton b = enumerator.Current;
          GelButton gelButton = new GelButton(b.Title, b.IsPrimary ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 100.0, null, null, null)
          {
            Height = 34.0
          };
          gelButton.Click += delegate
          {
            b.Action();
            NotchPresenter notchPresenter = this.presenter;
            if (notchPresenter != null)
            {
              notchPresenter.Updates.Track("request_resolved", null);
            }
            NotchPresenter notchPresenter2 = this.presenter;
            if (notchPresenter2 == null)
            {
              return;
            }
            notchPresenter2.Updates.DismissCurrent();
          };
          stackPanel2.Children.Add(gelButton);
        }
      }
      return new StackPanel
      {
        Spacing = 9.0,
        Children = { grid, stackPanel2 }
      };
    }

    // Token: 0x0600131B RID: 4891 RVA: 0x0059DB64 File Offset: 0x0059DB64
    private FrameworkElement Finished(NotchUpdate update, NotchUpdateContent card)
    {
      string text = ClickyUpdateLayout.SummaryText(card);
      string clickySlug = card.ClickySlug;
      string text2;
      if (clickySlug == null)
      {
        text2 = null;
      }
      else
      {
        RosterClicky rosterClicky = ClickyRoster.Shared.Clicky(clickySlug);
        text2 = ((rosterClicky != null) ? rosterClicky.Name : null);
      }
      string text3 = text2;
      string text4 = ((update.Kind == NotchUpdateKind.Done) ? ClickyUpdateLayout.FinishedTitle(card, text3) : card.Title);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0
      };
      TextBlock textBlock = Ui.Text(text4, 14.0, FontWeights.SemiBold, DS.White(0.95), TextAlignment.Left, 1, false, false);
      textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
      stackPanel.Children.Add(textBlock);
      int num = ChatStamp.Seed(update.RailKey);
      UIElementCollection children = stackPanel.Children;
      NotchUpdateKind notchUpdateKind = update.Kind;
      FrameworkElement frameworkElement;
      if (notchUpdateKind != NotchUpdateKind.NeedsYou)
      {
        if (notchUpdateKind == NotchUpdateKind.Done)
        {
          frameworkElement = ChatStamp.Create(ChatStamp.Status.Ready, "done", num);
        }
        else
        {
          frameworkElement = (card.IsStopped ? ChatStamp.Create(ChatStamp.Status.Paused, "stopped", num) : ChatStamp.Create(ChatStamp.Status.NeedsYou, "oops", num));
        }
      }
      else
      {
        frameworkElement = ChatStamp.Create(ChatStamp.Status.NeedsYou, "you", num);
      }
      children.Add(frameworkElement);
      string routineBadge = card.RoutineBadge;
      if (routineBadge != null)
      {
        TextBlock textBlock2 = Icons.Glyph("\ue72c", 9.0, DS.White(0.7));
        textBlock2.FontWeight = FontWeights.Bold;
        stackPanel.Children.Add(new Border
        {
          CornerRadius = new CornerRadius(100.0),
          Background = Ui.Brush(DS.White(0.1)),
          Padding = new Thickness(7.0, 3.0, 7.0, 3.0),
          VerticalAlignment = VerticalAlignment.Center,
          Child = Ui.H(4.0, new UIElement[]
          {
            textBlock2,
            Ui.Text(routineBadge, 10.5, FontWeights.SemiBold, DS.White(0.7), TextAlignment.Left, 1, false, false)
          })
        }.Capsule().Tip("Routine: " + card.RoutineTitle));
      }
      string text5 = text;
      if (text5 == null)
      {
        notchUpdateKind = update.Kind;
        string text6;
        if (notchUpdateKind != NotchUpdateKind.Failed)
        {
          if (notchUpdateKind == NotchUpdateKind.Done)
          {
            text6 = "Done";
          }
          else
          {
            text6 = "Needs you";
          }
        }
        else
        {
          text6 = (card.IsStopped ? "Interrupted" : "Didn't finish");
        }
        text5 = text6;
      }
      TextBlock textBlock3 = Ui.Text(text5, 11.0, FontWeights.Normal, DS.White(0.62), TextAlignment.Left, 0, true, false);
      textBlock3.MaxLines = ((text == null) ? 1 : 3);
      textBlock3.TextTrimming = TextTrimming.CharacterEllipsis;
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = (double)((text == null) ? 1 : 3),
        Children = { stackPanel, textBlock3 }
      };
      Grid grid = new Grid
      {
        ColumnSpacing = 10.0,
        MinHeight = 32.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      FrameworkElement frameworkElement2 = ClickyUpdateSurface.Chip(card);
      frameworkElement2.VerticalAlignment = ((text == null) ? VerticalAlignment.Center : VerticalAlignment.Top);
      grid.Children.Add(frameworkElement2);
      stackPanel2.VerticalAlignment = ((text == null) ? VerticalAlignment.Center : VerticalAlignment.Top);
      Grid.SetColumn(stackPanel2, 1);
      grid.Children.Add(stackPanel2);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = (double)((text == null) ? 12 : 9),
        Children = 
        {
          grid,
          this.FinishedButtons(card)
        }
      };
      string preview = ClickyUpdateSurface.PreviewEntry(update, card);
      if (preview == null)
      {
        return stackPanel3;
      }
      Grid grid2 = new Grid();
      grid2.ColumnSpacing = 14.0;
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.Children.Add(stackPanel3);
      ValueTuple<double, double> valueTuple = ClickyUpdateLayout.PreviewSize(text);
      int moreCount = card.Files.Count - 1;
      FrameworkElement frameworkElement3 = ClickyUpdateSurface.Preview(preview, moreCount, valueTuple, delegate
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter != null)
        {
          NotchUpdates updates = notchPresenter.Updates;
          string text7 = "button_pressed";
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["button"] = "file_preview";
          jsonObject["artifact_kind"] = ClickyAnalytics.ProductArtifactKind(preview);
          jsonObject["more_count"] = moreCount;
          updates.Track(text7, jsonObject);
        }
        NotchUpdateReadyOutput readyOutput = card.ReadyOutput;
        if (readyOutput != null)
        {
          readyOutput.Open();
        }
        NotchPresenter notchPresenter2 = this.presenter;
        if (notchPresenter2 != null)
        {
          notchPresenter2.Updates.DismissCurrent();
        }
        Action<NotchUpdateContent> announceFinishedWorkOnRequest = NotchExtensions.AnnounceFinishedWorkOnRequest;
        if (announceFinishedWorkOnRequest == null)
        {
          return;
        }
        announceFinishedWorkOnRequest(card);
      });
      Grid.SetColumn(frameworkElement3, 1);
      grid2.Children.Add(frameworkElement3);
      return grid2;
    }

    // Token: 0x0600131C RID: 4892 RVA: 0x0059E058 File Offset: 0x0059E058
    private FrameworkElement FinishedButtons(NotchUpdateContent card)
    {
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 10.0,
        Margin = new Thickness(42.0, 0.0, 0.0, 0.0)
      };
      ChatHandOffs handOffs = NotchChat.HandOffs;
      string text = ((handOffs != null) ? handOffs.LatestReplyEntryId(card.JobId, card.ClickySlug) : null);
      bool flag;
      if (text != null)
      {
        NotchUpdateReadyOutput readyOutput = card.ReadyOutput;
        flag = readyOutput == null || readyOutput.OpensFullReplyOnly;
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      NotchUpdateReadyOutput output = card.ReadyOutput;
      if (output != null && !flag2)
      {
        GelButton gelButton = new GelButton(output.ButtonTitle, GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, output.Glyph, null, null)
        {
          Height = 34.0
        };
        gelButton.Click += delegate
        {
          NotchPresenter notchPresenter = this.presenter;
          if (notchPresenter != null)
          {
            NotchUpdates updates = notchPresenter.Updates;
            string text2 = "button_pressed";
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["button"] = "open_output";
            string text3 = "output_kind";
            string text4 = this.outputAnalyticsKind;
            jsonObject[text3] = ((text4 != null && text4 != "none") ? text4 : (output.IsLink ? "link" : (output.OpensFullReplyOnly ? "several" : "file")));
            updates.Track(text2, jsonObject);
          }
          output.Open();
          NotchPresenter notchPresenter2 = this.presenter;
          if (notchPresenter2 != null)
          {
            notchPresenter2.Updates.DismissCurrent();
          }
          Action<NotchUpdateContent> announceFinishedWorkOnRequest = NotchExtensions.AnnounceFinishedWorkOnRequest;
          if (announceFinishedWorkOnRequest == null)
          {
            return;
          }
          announceFinishedWorkOnRequest(card);
        };
        stackPanel.Children.Add(gelButton);
      }
      if (text != null)
      {
        string entryId = text;
        GelButton gelButton2 = new GelButton("Full report", flag2 ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 100.0, "\ue8a5", null, null)
        {
          Height = 34.0
        };
        gelButton2.Click += delegate
        {
          NotchPresenter notchPresenter3 = this.presenter;
          if (notchPresenter3 != null)
          {
            NotchUpdates updates2 = notchPresenter3.Updates;
            string text5 = "button_pressed";
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["button"] = "full_report";
            updates2.Track(text5, jsonObject2);
          }
          NotchPresenter notchPresenter4 = this.presenter;
          if (notchPresenter4 != null)
          {
            notchPresenter4.Updates.DismissCurrent();
          }
          Action<NotchUpdateContent> markFinishedWorkSeen = NotchExtensions.MarkFinishedWorkSeen;
          if (markFinishedWorkSeen != null)
          {
            markFinishedWorkSeen(card);
          }
          NotchPresenter notchPresenter5 = this.presenter;
          if (notchPresenter5 != null)
          {
            notchPresenter5.Tabs.ShowFullReply(entryId, null);
          }
          Action<NotchUpdateContent> announceFinishedWorkOnRequest2 = NotchExtensions.AnnounceFinishedWorkOnRequest;
          if (announceFinishedWorkOnRequest2 == null)
          {
            return;
          }
          announceFinishedWorkOnRequest2(card);
        };
        stackPanel.Children.Add(gelButton2);
      }
      return stackPanel;
    }

    // Token: 0x0600131D RID: 4893 RVA: 0x0059E204 File Offset: 0x0059E204
    private static FrameworkElement Chip(NotchUpdateContent card)
    {
      ChatHandOff chatHandOff = new ChatHandOff
      {
        Id = card.RailKey,
        Kind = ChatHandOffKind.CameBack,
        JobId = card.JobId,
        ClickySlug = card.ClickySlug,
        Title = card.Title,
        CreatedAt = DateTimeOffset.UtcNow
      };
      IChatLiveWork liveWork = NotchExtensions.LiveWork;
      FrameworkElement frameworkElement = ((liveWork != null) ? liveWork.Chip(chatHandOff, 32.0) : null);
      if (frameworkElement != null)
      {
        return frameworkElement;
      }
      TextBlock textBlock = Icons.Glyph("\ue73e", 14.0, DS.Black(0.85));
      textBlock.FontWeight = FontWeights.Bold;
      return new Grid
      {
        Width = 32.0,
        Height = 32.0,
        VerticalAlignment = VerticalAlignment.Top,
        Children = 
        {
          new Ellipse
          {
            Fill = Ui.Brush(Colors.White)
          },
          textBlock
        }
      };
    }

    // Token: 0x0600131E RID: 4894 RVA: 0x0059E300 File Offset: 0x0059E300
    private static FrameworkElement Preview(string path, int moreCount, [TupleElementNames(new string[] { "W", "H" })] [Nullable(0)] ValueTuple<double, double> size, Action open)
    {
      NotchDocumentStyle notchDocumentStyle = NotchDocumentStyle.For(path, false);
      Pressable p = new Pressable(true)
      {
        Width = size.Item1,
        Height = size.Item2,
        CornerRadius = new CornerRadius(9.0),
        VerticalAlignment = VerticalAlignment.Top
      };
      p.Children.Add(new ShadowLayer(9.0, DS.Black(0.35), 6.0, 2.0, 0.0));
      p.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(9.0),
        Background = Ui.Brush(Colors.White)
      });
      Border picture = new Border
      {
        Margin = new Thickness(3.0),
        CornerRadius = new CornerRadius(6.0),
        Background = Ui.Brush(notchDocumentStyle.Tint.WithAlpha(0.14))
      };
      if (ChatBlockViews.IsImage(path))
      {
        picture.Background = new ImageBrush
        {
          ImageSource = new BitmapImage(new Uri(path))
          {
            DecodePixelWidth = 400
          },
          Stretch = Stretch.UniformToFill,
          AlignmentY = AlignmentY.Top
        };
      }
      else
      {
        TextBlock textBlock = Icons.Glyph(notchDocumentStyle.Glyph, 22.0, notchDocumentStyle.Tint.WithAlpha(0.9));
        textBlock.FontWeight = FontWeights.SemiBold;
        picture.Child = textBlock;
        ArtifactThumbnails.GetAsync(path, 400).ContinueWith(delegate([Nullable(new byte[] { 1, 2 })] Task<ImageSource> t)
        {
          ImageSource src = t.Result;
          if (src != null)
          {
            p.DispatcherQueue.TryEnqueue(delegate
            {
              picture.Background = new ImageBrush
              {
                ImageSource = src,
                Stretch = Stretch.UniformToFill,
                AlignmentY = AlignmentY.Top
              };
            });
          }
        }, TaskScheduler.Default);
      }
      p.Children.Add(picture);
      TextBlock textBlock2 = Icons.Glyph(notchDocumentStyle.Glyph, 9.0, Colors.Black);
      textBlock2.FontWeight = FontWeights.SemiBold;
      Grid grid = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(6.0)
      };
      grid.Children.Add(new GelSurface(GelAppearance.SecondaryWhite, 100.0));
      TextBlock textBlock3 = Ui.Text(notchDocumentStyle.KindLabel, 10.0, FontWeights.SemiBold, Colors.Black, TextAlignment.Left, 1, false, false);
      grid.Children.Add(new Border
      {
        Padding = new Thickness(8.0, 3.5, 8.0, 3.5),
        Child = Ui.H(4.0, new UIElement[]
        {
          Ui.Embossed(textBlock2, 0.8, 1.5, 1.0),
          Ui.Embossed(textBlock3, 0.8, 1.5, 1.0)
        })
      });
      p.Children.Add(grid);
      if (moreCount > 0)
      {
        UIElementCollection children = p.Children;
        Border border = new Border();
        border.CornerRadius = new CornerRadius(100.0);
        border.Background = Ui.Brush(DS.Black(0.62));
        border.Padding = new Thickness(6.0, 2.5, 6.0, 2.5);
        border.Margin = new Thickness(6.0);
        border.HorizontalAlignment = HorizontalAlignment.Right;
        border.VerticalAlignment = VerticalAlignment.Top;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("+");
        defaultInterpolatedStringHandler.AppendFormatted<int>(moreCount);
        border.Child = Ui.Text(defaultInterpolatedStringHandler.ToStringAndClear(), 9.5, FontWeights.Bold, Colors.White, TextAlignment.Left, 0, false, false);
        children.Add(border.Capsule());
      }
      p.Rotation = (float)(Anim.ReduceMotion ? 0 : 2);
      p.Loaded += delegate(object _, RoutedEventArgs _)
      {
        p.CenterPoint = new Vector3((float)size.Item1 / 2f, (float)size.Item2 / 2f, 0f);
      };
      SwiftSpring spring = new SwiftSpring(0.28, 0.75);
      p.HoverChanged += delegate(bool h2)
      {
        if (Anim.ReduceMotion)
        {
          return;
        }
        p.Rotation = (float)(h2 ? 0 : 2);
        Anim.ScaleSpring(p, h2 ? 1.03f : 1f, spring, null);
      };
      p.Click += open;
      p.Tip("Open " + Path.GetFileName(path));
      return p;
    }

    // Token: 0x040008C7 RID: 2247
    private readonly Grid view = new Grid();

    // Token: 0x040008C8 RID: 2248
    private readonly Grid content = new Grid();

    // Token: 0x040008C9 RID: 2249
    private readonly Grid row = new Grid
    {
      Height = 30.0,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x040008CA RID: 2250
    [Nullable(2)]
    private NotchPresenter presenter;

    // Token: 0x040008CB RID: 2251
    [Nullable(2)]
    private NotchUpdate shown;

    // Token: 0x040008CC RID: 2252
    [Nullable(2)]
    private RingCloseButton close;

    // Token: 0x040008CD RID: 2253
    [Nullable(2)]
    private string outputAnalyticsKind;

    // Token: 0x040008CE RID: 2254
    [Nullable(2)]
    private string builtLook;
  }
}
