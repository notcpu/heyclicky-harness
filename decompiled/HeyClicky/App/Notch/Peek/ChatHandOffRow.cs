using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using HeyClicky.App.Design;
using HeyClicky.App.Dock;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000179 RID: 377
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ChatHandOffRow : StackPanel
  {
    // Token: 0x060014A8 RID: 5288 RVA: 0x005AE8A0 File Offset: 0x005AE8A0
    public ChatHandOffRow(INotchHost host, ChatHandOff entry, IReadOnlyList<ChatHandOff> allEntries)
    {
      ChatHandOffRow.<>c__DisplayClass0_0 CS$<>8__locals1 = new ChatHandOffRow.<>c__DisplayClass0_0();
      CS$<>8__locals1.entry = entry;
      CS$<>8__locals1.host = host;
      base..ctor();
      base.Spacing = 6.0;
      IChatLiveWork liveWork = NotchExtensions.LiveWork;
      ChatHandOff chatHandOff = allEntries.LastOrDefault<ChatHandOff>((ChatHandOff e) => e.TargetKey == CS$<>8__locals1.entry.TargetKey);
      bool flag = ((chatHandOff != null) ? chatHandOff.Id : null) == CS$<>8__locals1.entry.Id;
      bool flag2 = CS$<>8__locals1.entry.Kind == ChatHandOffKind.Started && flag;
      string text = ((liveWork != null) ? liveWork.DisplayName(CS$<>8__locals1.entry) : null) ?? CS$<>8__locals1.entry.Title;
      FrameworkElement frameworkElement = ChatHandOffRow.Header(CS$<>8__locals1.entry, text, flag2, liveWork);
      IHeyClickyWindowHost window = CS$<>8__locals1.host as IHeyClickyWindowHost;
      bool flag3 = window != null;
      if (flag3)
      {
        ChatHandOffKind kind = CS$<>8__locals1.entry.Kind;
        bool flag4 = kind <= ChatHandOffKind.CameBack;
        flag3 = flag4;
      }
      if (flag3)
      {
        Pressable pressable = new Pressable(true);
        pressable.Background = Ui.Transparent;
        pressable.Children.Add(frameworkElement);
        pressable.Tip("Open " + text + "'s chat");
        pressable.Click += delegate
        {
          NotchPeekSurface.PeekAction("window_chat_event_opened", null);
          window.OpenAgentForEntry(CS$<>8__locals1.entry.Id);
        };
        frameworkElement = pressable;
      }
      base.Children.Add(frameworkElement);
      if (flag2)
      {
        FrameworkElement frameworkElement2 = ((liveWork != null) ? liveWork.WorkCard(CS$<>8__locals1.entry) : null);
        if (frameworkElement2 != null)
        {
          frameworkElement2.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
          base.Children.Add(frameworkElement2);
        }
      }
      if (CS$<>8__locals1.entry.Kind != ChatHandOffKind.CameBack)
      {
        return;
      }
      int count = base.Children.Count;
      FrameworkElement frameworkElement3 = null;
      ChatHandOffOutcome? chatHandOffOutcome;
      FrameworkElement frameworkElement4;
      if (flag)
      {
        chatHandOffOutcome = CS$<>8__locals1.entry.Outcome;
        ChatHandOffOutcome chatHandOffOutcome2 = ChatHandOffOutcome.Finished;
        if ((chatHandOffOutcome.GetValueOrDefault() == chatHandOffOutcome2) & (chatHandOffOutcome != null))
        {
          frameworkElement4 = ((liveWork != null) ? liveWork.NextStepButton(CS$<>8__locals1.entry) : null);
          goto IL_023C;
        }
      }
      frameworkElement4 = null;
      IL_023C:
      FrameworkElement frameworkElement5 = frameworkElement4;
      string fullReply = CS$<>8__locals1.entry.FullReply;
      if (fullReply != null && fullReply.Length > 0)
      {
        base.Children.Add(frameworkElement3 = ChatHandOffRow.ReplyBlock(CS$<>8__locals1.host, CS$<>8__locals1.entry.Id, fullReply, frameworkElement5));
      }
      else
      {
        string replyText = CS$<>8__locals1.entry.ReplyText;
        if (replyText != null && replyText.Length > 0)
        {
          FrameworkElement frameworkElement6 = ChatBubbles.Bubble(replyText, false, true, null);
          frameworkElement6.MaxWidth = 420.0;
          frameworkElement6.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
          base.Children.Add(frameworkElement3 = frameworkElement6);
        }
      }
      IReadOnlyList<string> sitesRead = CS$<>8__locals1.entry.SitesRead;
      if (sitesRead != null && sitesRead.Count > 0)
      {
        base.Children.Add(new ChatSitesReadChip(sitesRead)
        {
          Margin = new Thickness(30.0, 0.0, 0.0, 0.0)
        });
      }
      IReadOnlyList<string> artifactEntries = CS$<>8__locals1.entry.ArtifactEntries;
      if (artifactEntries != null && artifactEntries.Count > 0)
      {
        FrameworkElement frameworkElement7 = ((liveWork != null) ? liveWork.FileCards(artifactEntries, new Action<string>(CS$<>8__locals1.<.ctor>g__Open|2)) : null) ?? ChatHandOffRow.FilePills(artifactEntries, new Action<string>(CS$<>8__locals1.<.ctor>g__Open|2));
        frameworkElement7.Margin = new Thickness(30.0, 4.0, 0.0, 0.0);
        base.Children.Add(frameworkElement7);
      }
      chatHandOffOutcome = CS$<>8__locals1.entry.Outcome;
      bool flag5;
      if (chatHandOffOutcome != null)
      {
        ChatHandOffOutcome valueOrDefault = chatHandOffOutcome.GetValueOrDefault();
        flag5 = valueOrDefault > ChatHandOffOutcome.Finished;
      }
      else
      {
        flag5 = false;
      }
      if (flag5 && flag)
      {
        FrameworkElement frameworkElement8 = ((liveWork != null) ? liveWork.WorkCard(CS$<>8__locals1.entry) : null);
        if (frameworkElement8 != null)
        {
          frameworkElement8.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
          base.Children.Add(frameworkElement8);
        }
      }
      this.StartFoldIfFresh(CS$<>8__locals1.entry, liveWork, count, frameworkElement3);
    }

    // Token: 0x060014A9 RID: 5289 RVA: 0x005AED5C File Offset: 0x005AED5C
    [NullableContext(2)]
    private void StartFoldIfFresh([Nullable(1)] ChatHandOff entry, IChatLiveWork live, int replyStart, FrameworkElement reply)
    {
      bool flag = (entry.Outcome ?? ChatHandOffOutcome.Finished) == ChatHandOffOutcome.Finished;
      if (!flag || Anim.ReduceMotion || live == null || DateTimeOffset.UtcNow - entry.CreatedAt >= TimeSpan.FromSeconds(6L) || ChatHandOffRow.foldedEntryIds.Contains(entry.Id))
      {
        return;
      }
      List<UIElement> replyPart = base.Children.Skip<UIElement>(replyStart).ToList<UIElement>();
      FrameworkElement fold = null;
      fold = live.PlanFold(entry, delegate
      {
        this.Children.Remove(fold);
        foreach (UIElement uielement2 in replyPart)
        {
          uielement2.Visibility = Visibility.Visible;
        }
        if (reply == null)
        {
          return;
        }
        SwiftSpring swiftSpring = new SwiftSpring(0.42, 0.86);
        reply.Opacity = 0.0;
        reply.Translation = new Vector3(0f, 10f, 0f);
        Anim.OpacitySpring(reply, 1f, swiftSpring);
        Anim.TranslationSpring(reply, Vector3.Zero, swiftSpring);
      });
      if (fold == null)
      {
        return;
      }
      ChatHandOffRow.foldedEntryIds.Add(entry.Id);
      fold.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
      foreach (UIElement uielement in replyPart)
      {
        uielement.Visibility = Visibility.Collapsed;
      }
      base.Children.Insert(replyStart, fold);
    }

    // Token: 0x060014AA RID: 5290 RVA: 0x005AEEB8 File Offset: 0x005AEEB8
    private static FrameworkElement Header(ChatHandOff entry, string displayName, bool isNewestStart, [Nullable(2)] IChatLiveWork live)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      Grid grid = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      FrameworkElement frameworkElement = ((live != null) ? live.Chip(entry, 22.0) : null) ?? ChatHandOffRow.PlainChip(entry);
      grid.Children.Add(frameworkElement);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      ChatHandOffRow.<>c__DisplayClass3_0 CS$<>8__locals1;
      CS$<>8__locals1.text = new TextBlock
      {
        FontSize = 11.5,
        FontWeight = FontWeights.Medium,
        FontFamily = DS.Fonts.Small,
        IsTextScaleFactorEnabled = false,
        TextTrimming = TextTrimming.CharacterEllipsis,
        TextWrapping = TextWrapping.NoWrap,
        VerticalAlignment = VerticalAlignment.Center
      };
      Color primaryText = messagesPalette.PrimaryText;
      ChatHandOffKind chatHandOffKind = entry.Kind;
      if (chatHandOffKind <= ChatHandOffKind.CameBack)
      {
        ChatHandOffRow.<Header>g__Part|3_0(displayName, primaryText, true, ref CS$<>8__locals1);
      }
      else
      {
        ChatHandOffRow.<Header>g__Part|3_0(entry.Title, primaryText, false, ref CS$<>8__locals1);
      }
      stackPanel.Children.Add(CS$<>8__locals1.text);
      int num = ChatStamp.Seed(entry.Id);
      chatHandOffKind = entry.Kind;
      FrameworkElement frameworkElement2;
      if (chatHandOffKind != ChatHandOffKind.Started)
      {
        if (chatHandOffKind != ChatHandOffKind.CameBack)
        {
          frameworkElement2 = null;
        }
        else
        {
          frameworkElement2 = ChatHandOffRow.CameBackMark(entry, num);
        }
      }
      else
      {
        frameworkElement2 = ChatHandOffRow.StartStamp(entry, isNewestStart, live, num);
      }
      FrameworkElement frameworkElement3 = frameworkElement2;
      if (frameworkElement3 != null)
      {
        stackPanel.Children.Add(frameworkElement3);
      }
      stackPanel.Children.Add(new TextBlock
      {
        Text = ChatBubbles.ShortTime(entry.CreatedAt),
        FontSize = 11.5,
        FontWeight = FontWeights.Medium,
        FontFamily = DS.Fonts.Small,
        IsTextScaleFactorEnabled = false,
        Foreground = Ui.Brush(messagesPalette.TertiaryText),
        VerticalAlignment = VerticalAlignment.Center
      });
      Grid.SetColumn(stackPanel, 1);
      grid.Children.Add(stackPanel);
      return grid;
    }

    // Token: 0x060014AB RID: 5291 RVA: 0x005AF0D4 File Offset: 0x005AF0D4
    private static FrameworkElement StartStamp(ChatHandOff entry, bool isNewestStart, [Nullable(2)] IChatLiveWork live, int seed)
    {
      FrameworkElement frameworkElement = ChatStamp.Create(ChatStamp.Status.Working, entry.IsFollowUp ? "sent" : "started", seed);
      if (!isNewestStart || live == null)
      {
        return frameworkElement;
      }
      return new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          frameworkElement,
          new ChatHandOffRow.StartMark(entry, live, seed)
        }
      };
    }

    // Token: 0x060014AC RID: 5292 RVA: 0x005AF148 File Offset: 0x005AF148
    private static FrameworkElement CameBackMark(ChatHandOff entry, int seed)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      FrameworkElement frameworkElement;
      switch (entry.Outcome.GetValueOrDefault())
      {
      case ChatHandOffOutcome.Finished:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.Ready, "done", seed);
        break;
      case ChatHandOffOutcome.Failed:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.NeedsYou, "oops", seed);
        break;
      case ChatHandOffOutcome.Stopped:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.Paused, "stopped", seed);
        break;
      default:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.NeedsYou, "you", seed);
        break;
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      if (!entry.IsRoutineRun.GetValueOrDefault())
      {
        return frameworkElement2;
      }
      StackPanel stackPanel = new StackPanel();
      stackPanel.Orientation = Orientation.Horizontal;
      stackPanel.Spacing = 6.0;
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock = Icons.Glyph("\ue72c", 10.0, messagesPalette.TertiaryText);
      ToolTipService.SetToolTip(textBlock, "A routine run");
      stackPanel.Children.Add(textBlock);
      stackPanel.Children.Add(frameworkElement2);
      return stackPanel;
    }

    // Token: 0x060014AD RID: 5293 RVA: 0x005AF230 File Offset: 0x005AF230
    private static FrameworkElement BusyPencil()
    {
      TextBlock pencil = Icons.Glyph("\ue70f", 11.0, ChatStamp.OnBlack(ChatStamp.Status.Working));
      pencil.FontWeight = FontWeights.SemiBold;
      pencil.Loaded += delegate(object _, RoutedEventArgs _)
      {
        if (!Anim.ReduceMotion)
        {
          Anim.LoopScalar(pencil, "Opacity", 1.2, (double x) => (float)(0.45 + 0.55 * Math.Abs(Math.Cos(3.141592653589793 * x / 1.2))), 24, 0.0);
        }
      };
      ToolTipService.SetToolTip(pencil, "Working on it");
      return pencil;
    }

    // Token: 0x060014AE RID: 5294 RVA: 0x005AF2A0 File Offset: 0x005AF2A0
    private static FrameworkElement PlainChip(ChatHandOff entry)
    {
      MessagesPalette messagesPalette = MessagesPalette.Current;
      Border border = new Border();
      border.Width = 22.0;
      border.Height = 22.0;
      border.CornerRadius = new CornerRadius(6.6);
      border.Background = Ui.Brush(messagesPalette.AvatarPlate);
      border.VerticalAlignment = VerticalAlignment.Center;
      TextBlock textBlock = Icons.Glyph((entry.JobId != null) ? "\ue821" : "\ue77b", 10.0, messagesPalette.SecondaryText);
      textBlock.FontWeight = FontWeights.SemiBold;
      border.Child = textBlock;
      return border;
    }

    // Token: 0x060014AF RID: 5295 RVA: 0x005AF340 File Offset: 0x005AF340
    private static FrameworkElement ReplyBlock(INotchHost host, string entryId, string markdown, [Nullable(2)] FrameworkElement nextStep = null)
    {
      ValueTuple<string, bool> valueTuple = ChatReplyPreview.Parts(markdown);
      ValueTuple<string, bool> valueTuple2 = ChatReplyPreview.Preview(ChatReplyPreview.BubbleText(ChatReplyPreview.WithoutCitations(valueTuple.Item1 ?? markdown)));
      StackPanel stackPanel = new StackPanel();
      stackPanel.Spacing = 4.0;
      stackPanel.MaxWidth = 420.0;
      stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
      stackPanel.Margin = new Thickness(30.0, 0.0, 0.0, 0.0);
      stackPanel.Children.Add(ChatBubbles.Bubble(valueTuple2.Item1, false, true, null));
      bool hadMore = valueTuple2.Item2 || (valueTuple.Item1 != null && valueTuple.Item2);
      GelButton gelButton = ArcButtons.Gel("Full report", "doc.text", GelAppearance.SecondaryWhite, new double?((double)30), new double?((double)13), GelButton.Sizing.Normal, delegate
      {
        string text = "chat_reply_show_more";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["had_more"] = hadMore;
        NotchPeekSurface.PeekAction(text, jsonObject);
        IHeyClickyWindowHost heyClickyWindowHost = host as IHeyClickyWindowHost;
        if (heyClickyWindowHost != null)
        {
          heyClickyWindowHost.OpenAgentForEntry(entryId);
          return;
        }
        NotchPresenter notchPresenter = host.Services.Presenter as NotchPresenter;
        if (notchPresenter != null)
        {
          notchPresenter.Tabs.TrackFullReplyOpened(entryId, NotchChatFullReplyOpenSource.ShowMore);
          notchPresenter.Tabs.ExpandedReplyEntryId = entryId;
        }
      });
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        Margin = new Thickness(0.0, 4.0, 0.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Left
      };
      if (nextStep != null)
      {
        stackPanel2.Children.Add(nextStep);
      }
      stackPanel2.Children.Add(gelButton);
      stackPanel.Children.Add(stackPanel2);
      return stackPanel;
    }

    // Token: 0x060014B0 RID: 5296 RVA: 0x005AF4C4 File Offset: 0x005AF4C4
    public static FrameworkElement FilePills(IReadOnlyList<string> entries, Action<string> open)
    {
      WrapPanel wrapPanel = new WrapPanel
      {
        HorizontalSpacing = 6.0,
        VerticalSpacing = 6.0
      };
      using (IEnumerator<string> enumerator = entries.Take<string>(8).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string entry = enumerator.Current;
          Uri uri;
          string text = (ArtifactPaths.IsWebLink(entry) ? (Uri.TryCreate(entry, UriKind.Absolute, out uri) ? uri.Host : entry) : Path.GetFileName(entry.TrimEnd(new char[] { '/', '\\' })));
          Pressable pressable = new Pressable(true)
          {
            CornerRadius = new CornerRadius(9.0),
            Background = Ui.Brush(Colors.White),
            Padding = new Thickness(8.0, 5.0, 10.0, 5.0)
          };
          TextBlock textBlock = Icons.Glyph(ArtifactPaths.IsWebLink(entry) ? "\ue774" : "\ue8a5", 11.0, DS.Black(0.7));
          TextBlock textBlock2 = Ui.Text(text, 11.5, FontWeights.SemiBold, DS.Black(0.85), TextAlignment.Left, 1, false, false);
          textBlock2.MaxWidth = 220.0;
          textBlock2.TextTrimming = TextTrimming.CharacterEllipsis;
          pressable.Children.Add(Ui.H(6.0, new UIElement[] { textBlock, textBlock2 }));
          pressable.WithHoverScale(1.03f, 0.15, false);
          pressable.Click += delegate
          {
            open(entry);
          };
          pressable.Tip(entry);
          wrapPanel.Children.Add(pressable);
        }
      }
      return wrapPanel;
    }

    // Token: 0x060014B2 RID: 5298 RVA: 0x005AF6FC File Offset: 0x005AF6FC
    [CompilerGenerated]
    internal static void <Header>g__Part|3_0(string s, Color c, bool bold = false, ref ChatHandOffRow.<>c__DisplayClass3_0 A_3)
    {
      A_3.text.Inlines.Add(new Run
      {
        Text = s,
        Foreground = Ui.Brush(c),
        FontWeight = (bold ? FontWeights.Bold : FontWeights.Medium)
      });
    }

    // Token: 0x040009F9 RID: 2553
    private static readonly HashSet<string> foldedEntryIds = new HashSet<string>();

    // Token: 0x02000702 RID: 1794
    [Nullable(0)]
    private sealed class StartMark : Grid
    {
      // Token: 0x060033F8 RID: 13304 RVA: 0x00679330 File Offset: 0x00679330
      public StartMark(ChatHandOff entry, IChatLiveWork live, int seed)
      {
        ChatHandOffRow.StartMark <>4__this = this;
        this.entry = entry;
        this.live = live;
        this.seed = seed;
        base.VerticalAlignment = VerticalAlignment.Center;
        this.Update();
        base.Loaded += delegate(object _, RoutedEventArgs _)
        {
          if (!<>4__this.listening)
          {
            <>4__this.listening = true;
            live.Changed += <>4__this.OnWorkChanged;
          }
          <>4__this.Update();
        };
        base.Unloaded += delegate(object _, RoutedEventArgs _)
        {
          if (<>4__this.listening && !<>4__this.IsLoaded)
          {
            <>4__this.listening = false;
            live.Changed -= <>4__this.OnWorkChanged;
          }
        };
      }

      // Token: 0x060033F9 RID: 13305 RVA: 0x006793A4 File Offset: 0x006793A4
      private void OnWorkChanged()
      {
        if (base.DispatcherQueue.HasThreadAccess)
        {
          this.Update();
          return;
        }
        base.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.Update));
      }

      // Token: 0x060033FA RID: 13306 RVA: 0x006793D4 File Offset: 0x006793D4
      private void Update()
      {
        bool flag = this.live.IsWorking(this.entry);
        bool? flag2 = this.showsPencil;
        bool flag3 = flag;
        if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
        {
          return;
        }
        this.showsPencil = new bool?(flag);
        base.Children.Clear();
        if (flag)
        {
          base.Children.Add(ChatHandOffRow.BusyPencil());
        }
        base.Visibility = ((base.Children.Count > 0) ? Visibility.Visible : Visibility.Collapsed);
      }

      // Token: 0x04001F86 RID: 8070
      private readonly ChatHandOff entry;

      // Token: 0x04001F87 RID: 8071
      private readonly IChatLiveWork live;

      // Token: 0x04001F88 RID: 8072
      private readonly int seed;

      // Token: 0x04001F89 RID: 8073
      private bool? showsPencil;

      // Token: 0x04001F8A RID: 8074
      private bool listening;
    }
  }
}
