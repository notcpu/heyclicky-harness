using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200017E RID: 382
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchChatView : Grid
  {
    // Token: 0x060014B7 RID: 5303 RVA: 0x005B0414 File Offset: 0x005B0414
    private void OnChatSizeChanged()
    {
      if (this.isInWindow)
      {
        return;
      }
      double contentWidth = NotchChatSize.ContentWidth;
      double num = NotchChatSize.BodyHeight - 6.0;
      if (contentWidth == base.Width && num == base.Height)
      {
        return;
      }
      base.Width = contentWidth;
      base.Height = num;
      int num2 = base.Children.IndexOf(this.boardDots);
      this.boardDots = NotchChatView.BoardDots(contentWidth, num, DS.White(0.09));
      if (num2 >= 0)
      {
        base.Children[num2] = this.boardDots;
      }
    }

    // Token: 0x060014B8 RID: 5304 RVA: 0x005B04A4 File Offset: 0x005B04A4
    public NotchChatView(INotchHost host, bool isInWindow = false)
    {
      this.host = host;
      this.isInWindow = isInWindow;
      this.presenter = host.Services.Presenter as NotchPresenter;
      if (isInWindow)
      {
        base.HorizontalAlignment = HorizontalAlignment.Stretch;
        base.VerticalAlignment = VerticalAlignment.Stretch;
        this.boardDots = new Grid();
        this.list.Padding = new Thickness(24.0, 0.0, 24.0, 0.0);
        this.list.MaxWidth = 728.0;
      }
      else
      {
        base.Width = NotchChatSize.ContentWidth;
        base.Height = NotchChatSize.BodyHeight - 6.0;
        base.HorizontalAlignment = HorizontalAlignment.Center;
        base.VerticalAlignment = VerticalAlignment.Top;
        this.boardDots = NotchChatView.BoardDots(base.Width, base.Height, DS.White(0.09));
      }
      Ui.ClipRounded(this, 0.0);
      base.Children.Add(this.boardDots);
      StackPanel stackPanel = new StackPanel();
      stackPanel.Children.Add(new Border
      {
        Height = 40.0
      });
      stackPanel.Children.Add(this.earlierRow);
      stackPanel.Children.Add(this.list);
      stackPanel.Children.Add(this.bottomSpacer);
      this.scroller = new ScrollViewer
      {
        Content = stackPanel,
        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Disabled,
        IsTabStop = false
      };
      this.scroller.ViewChanged += delegate([Nullable(2)] object _, ScrollViewerViewChangedEventArgs _)
      {
        this.OnScrolled();
      };
      base.Children.Add(this.scroller);
      base.Children.Add(this.emptyState);
      if (!isInWindow)
      {
        base.Children.Add(NotchTopFade.Create(8.0, 52.0));
      }
      this.bottomFade = new Border
      {
        Height = 38.0,
        VerticalAlignment = VerticalAlignment.Bottom,
        IsHitTestVisible = false,
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, this.look.PaneFill.WithAlpha(0.0)),
          new ValueTuple<double, Color>(1.0, this.look.PaneFill)
        })
      };
      this.bottomFill = new Border
      {
        VerticalAlignment = VerticalAlignment.Bottom,
        IsHitTestVisible = false,
        Background = Ui.Brush(this.look.PaneFill)
      };
      base.Children.Add(this.bottomFade);
      base.Children.Add(this.bottomFill);
      this.composer = new NotchChatComposer(host, null, isInWindow, null, null);
      this.composer.ExtraHeightChanged += delegate(double _)
      {
        this.LayoutComposerBand();
      };
      base.Children.Add(this.composer);
      this.jumpDisc = NotchChatView.JumpDisc(this.look, delegate
      {
        NotchPeekSurface.PeekAction("chat_scroll_to_bottom", null);
        this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, false);
      });
      base.Children.Add(this.jumpDisc);
      this.LayoutComposerBand();
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Attach();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        if (!base.IsLoaded)
        {
          this.Detach();
        }
      };
    }

    // Token: 0x170003BA RID: 954
    // (get) Token: 0x060014B9 RID: 5305 RVA: 0x005B08D8 File Offset: 0x005B08D8
    public bool ComposerHoldsKeyboard
    {
      get
      {
        return this.composer.HoldsKeyboard;
      }
    }

    // Token: 0x060014BA RID: 5306 RVA: 0x005B08E8 File Offset: 0x005B08E8
    public void SetDropTargeting(bool on)
    {
      this.composer.SetDropTargeting(on);
    }

    // Token: 0x060014BB RID: 5307 RVA: 0x005B08F8 File Offset: 0x005B08F8
    public void AttachDroppedFiles(IReadOnlyList<string> paths)
    {
      if (this.composer.AddAttachments(paths) > 0)
      {
        this.composer.FocusField();
      }
    }

    // Token: 0x060014BC RID: 5308 RVA: 0x005B0914 File Offset: 0x005B0914
    public void Shown()
    {
      if (!this.isAwayFromBottom || this.newestWhenShown)
      {
        this.ScrollToBottom();
      }
      this.newestWhenShown = false;
      this.RenderSoon();
      this.composer.TakeFocusIfRequested();
    }

    // Token: 0x060014BD RID: 5309 RVA: 0x005B0944 File Offset: 0x005B0944
    public void Detached()
    {
      this.composer.Detached();
    }

    // Token: 0x060014BE RID: 5310 RVA: 0x005B0954 File Offset: 0x005B0954
    private void Attach()
    {
      if (this.attached)
      {
        return;
      }
      this.attached = true;
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.Changed += this.RenderSoon;
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        handOffs.Changed += this.RenderSoon;
      }
      if (this.presenter != null)
      {
        this.presenter.PresentationChanged += this.OnPresentationChanged;
        this.presenter.Tabs.Changed += this.RenderSoon;
        if (!this.isInWindow)
        {
          this.presenter.Tabs.ComposerFocusRequested += this.OnComposerFocusRequested;
          this.presenter.DroppedAttachmentsPending += this.OnDroppedAttachments;
          this.presenter.NotchSessionChanged += this.OnSessionChanged;
        }
      }
      this.host.Services.Companion.StateChanged += this.OnCompanionChanged;
      this.host.Services.Companion.ResponseTextUpdated += this.OnResponseText;
      NotchChatSize.Changed += this.OnChatSizeChanged;
      this.OnChatSizeChanged();
      this.Render();
      this.ScrollToBottom();
      this.newestWhenShown = base.Visibility > Visibility.Visible;
      if (this.isInWindow)
      {
        return;
      }
      this.composer.TakeFocusIfRequested();
      this.composer.DrainDroppedAttachments();
    }

    // Token: 0x060014BF RID: 5311 RVA: 0x005B0ACC File Offset: 0x005B0ACC
    private void Detach()
    {
      if (!this.attached)
      {
        return;
      }
      this.attached = false;
      ConversationHistory history = NotchChat.History;
      if (history != null)
      {
        history.Changed -= this.RenderSoon;
      }
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (handOffs != null)
      {
        handOffs.Changed -= this.RenderSoon;
      }
      if (this.presenter != null)
      {
        this.presenter.PresentationChanged -= this.OnPresentationChanged;
        this.presenter.Tabs.Changed -= this.RenderSoon;
        if (!this.isInWindow)
        {
          this.presenter.Tabs.ComposerFocusRequested -= this.OnComposerFocusRequested;
          this.presenter.DroppedAttachmentsPending -= this.OnDroppedAttachments;
          this.presenter.NotchSessionChanged -= this.OnSessionChanged;
        }
      }
      this.host.Services.Companion.StateChanged -= this.OnCompanionChanged;
      this.host.Services.Companion.ResponseTextUpdated -= this.OnResponseText;
      NotchChatSize.Changed -= this.OnChatSizeChanged;
      this.composer.Detached();
    }

    // Token: 0x060014C0 RID: 5312 RVA: 0x005B0C10 File Offset: 0x005B0C10
    private void OnPresentationChanged([Nullable(2)] object sender, EventArgs e)
    {
      this.RenderSoon();
    }

    // Token: 0x060014C1 RID: 5313 RVA: 0x005B0C18 File Offset: 0x005B0C18
    private void OnCompanionChanged([Nullable(2)] object sender, EventArgs e)
    {
      this.RenderSoon();
    }

    // Token: 0x060014C2 RID: 5314 RVA: 0x005B0C20 File Offset: 0x005B0C20
    private void OnResponseText([Nullable(2)] object sender, string e)
    {
      this.RenderSoon();
    }

    // Token: 0x060014C3 RID: 5315 RVA: 0x005B0C28 File Offset: 0x005B0C28
    private void OnComposerFocusRequested()
    {
      base.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.composer.TakeFocusIfRequested));
    }

    // Token: 0x060014C4 RID: 5316 RVA: 0x005B0C48 File Offset: 0x005B0C48
    private void OnDroppedAttachments()
    {
      base.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(this.composer.DrainDroppedAttachments));
    }

    // Token: 0x060014C5 RID: 5317 RVA: 0x005B0C68 File Offset: 0x005B0C68
    private void OnSessionChanged()
    {
      base.DispatcherQueue.TryEnqueue(delegate
      {
        NotchPresenter notchPresenter = this.presenter;
        if (notchPresenter != null && !notchPresenter.IsNotchChatSessionOpen)
        {
          this.composer.ChatSessionClosed();
        }
      });
    }

    // Token: 0x060014C6 RID: 5318 RVA: 0x005B0C84 File Offset: 0x005B0C84
    private void LayoutComposerBand()
    {
      double extraHeight = this.composer.ExtraHeight;
      double num = 54.0 + extraHeight;
      this.bottomFill.Height = num;
      this.bottomFade.Margin = new Thickness(0.0, 0.0, 0.0, num);
      this.bottomSpacer.Height = HeyClickyWindowRules.BottomRoom(64.0, extraHeight, this.isInWindow);
      this.jumpDisc.Margin = new Thickness(0.0, 0.0, 12.0, 64.0 + extraHeight + 8.0);
    }

    // Token: 0x060014C7 RID: 5319 RVA: 0x005B0D44 File Offset: 0x005B0D44
    private void RenderSoon()
    {
      if (this.renderQueued)
      {
        return;
      }
      this.renderQueued = true;
      base.DispatcherQueue.TryEnqueue(delegate
      {
        this.renderQueued = false;
        if (this.attached)
        {
          this.Render();
        }
      });
    }

    // Token: 0x060014C8 RID: 5320 RVA: 0x005B0D70 File Offset: 0x005B0D70
    private List<ChatRow> CurrentRows()
    {
      ConversationHistory history = NotchChat.History;
      ChatHandOffs handOffs = NotchChat.HandOffs;
      IReadOnlyList<ChatHandOff> readOnlyList = ((handOffs != null) ? handOffs.Entries : null) ?? Array.Empty<ChatHandOff>();
      ChatLiveReply chatLiveReply = null;
      if (this.presenter != null && history != null)
      {
        bool rendersTextResponsesInNotchChat = this.presenter.RendersTextResponsesInNotchChat;
        bool isTextResponseInProgress = this.presenter.IsTextResponseInProgress;
        NotchPresentation.TextResponse currentTextResponse = this.presenter.CurrentTextResponse;
        chatLiveReply = ChatTranscript.LiveReply(rendersTextResponsesInNotchChat, isTextResponseInProgress, ((currentTextResponse != null) ? currentTextResponse.Answer : null) ?? "", this.presenter.CustomStatusLabel, history.LiveReplyMessageId(), DateTimeOffset.UtcNow);
      }
      return ChatTranscript.Rows(((history != null) ? history.Messages : null) ?? Array.Empty<ConversationMessage>(), readOnlyList, chatLiveReply, (history != null) ? history.LastAnswerEndedAt : null, DateTimeOffset.UtcNow);
    }

    // Token: 0x060014C9 RID: 5321 RVA: 0x005B0E34 File Offset: 0x005B0E34
    private void Render()
    {
      using (MessagesPalette.Use(this.look))
      {
        ConversationHistory history = NotchChat.History;
        List<ChatRow> list = this.CurrentRows();
        this.RenderEmptyState(list.Count == 0, history);
        this.scroller.Visibility = ((list.Count == 0) ? Visibility.Collapsed : Visibility.Visible);
        List<ChatRow> list2 = list.Skip<ChatRow>(Math.Max(0, list.Count - this.shownRowCount)).ToList<ChatRow>();
        bool flag = list.Count > list2.Count || (history != null && history.HasEarlierMessages);
        this.RenderEarlierRow(flag, history, list.Count);
        double num = this.scroller.ExtentHeight - this.scroller.VerticalOffset - this.scroller.ViewportHeight;
        bool flag2 = !this.isAwayFromBottom;
        NotchPresenter notchPresenter = this.presenter;
        string text = ((notchPresenter != null) ? notchPresenter.Tabs.ChatFocusEntryId : null);
        ChatHandOffs handOffs = NotchChat.HandOffs;
        IReadOnlyList<ChatHandOff> readOnlyList = ((handOffs != null) ? handOffs.Entries : null) ?? Array.Empty<ChatHandOff>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (ChatHandOff chatHandOff in readOnlyList)
        {
          dictionary[chatHandOff.TargetKey] = chatHandOff.Id;
        }
        HashSet<string> hashSet = (from r in list2.TakeLast<ChatRow>(3)
          select r.Id).ToHashSet<string>();
        HashSet<string> keep = new HashSet<string>();
        List<FrameworkElement> list3 = new List<FrameworkElement>(list2.Count);
        bool flag3 = this.built.Count == 0;
        bool flag4 = false;
        int num2 = 0;
        int num3 = 0;
        foreach (ChatRow chatRow in list2)
        {
          keep.Add(chatRow.Id);
          ChatRow.HandOff handOff = chatRow as ChatRow.HandOff;
          string text2 = ((handOff != null && handOff.Entry.Id == text) ? text : null);
          string text3 = NotchChatView.RowLook(chatRow, text2, dictionary);
          ValueTuple<ChatRow, FrameworkElement, string> valueTuple;
          if (this.built.TryGetValue(chatRow.Id, out valueTuple))
          {
            if (valueTuple.Item3 == text3 && (chatRow is ChatRow.Message || valueTuple.Item1 == chatRow))
            {
              list3.Add(valueTuple.Item2);
              continue;
            }
            ChatRow.Message message = chatRow as ChatRow.Message;
            if (message != null)
            {
              ChatRow.Message message2 = valueTuple.Item1 as ChatRow.Message;
              if (message2 != null && message2.IsFirstOfRun == message.IsFirstOfRun)
              {
                ChatMessageRow chatMessageRow = valueTuple.Item2 as ChatMessageRow;
                if (chatMessageRow != null && chatMessageRow.TryGrow(message.Value, message.IsLastOfRun))
                {
                  this.built[chatRow.Id] = new ValueTuple<ChatRow, FrameworkElement, string>(chatRow, valueTuple.Item2, text3);
                  list3.Add(valueTuple.Item2);
                  flag4 = true;
                  num3++;
                  continue;
                }
              }
            }
          }
          FrameworkElement frameworkElement = this.Build(chatRow, text2, readOnlyList);
          num2++;
          if (!flag3 && !this.built.ContainsKey(chatRow.Id) && hashSet.Contains(chatRow.Id))
          {
            NotchChatView.Arrive(frameworkElement, chatRow);
          }
          this.built[chatRow.Id] = new ValueTuple<ChatRow, FrameworkElement, string>(chatRow, frameworkElement, text3);
          list3.Add(frameworkElement);
        }
        IEnumerable<string> keys = this.built.Keys;
        Func<string, bool> <>9__1;
        Func<string, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = (string k) => !keep.Contains(k));
        }
        foreach (string text4 in keys.Where<string>(func).ToList<string>())
        {
          this.built.Remove(text4);
        }
        if (NotchChatView.Reconcile(this.list.Children, list3) || flag4)
        {
          this.scroller.UpdateLayout();
          if (flag2)
          {
            this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, true);
          }
          else if (!double.IsNaN(num))
          {
            this.scroller.ChangeView(null, new double?(Math.Max(0.0, this.scroller.ExtentHeight - this.scroller.ViewportHeight - num)), null, true);
          }
        }
        if (text != null && text != this.lastFocusedEntry)
        {
          this.ScrollToFocusedEntry(text);
        }
        this.lastFocusedEntry = text;
      }
    }

    // Token: 0x060014CA RID: 5322 RVA: 0x005B1374 File Offset: 0x005B1374
    private unsafe static string RowLook(ChatRow row, [Nullable(2)] string focus, IReadOnlyDictionary<string, string> newestEntryIdForWork)
    {
      ChatRow.Message message = row as ChatRow.Message;
      string text;
      if (message == null)
      {
        ChatRow.HandOff handOff = row as ChatRow.HandOff;
        if (handOff == null)
        {
          ChatRow.LiveWork liveWork = row as ChatRow.LiveWork;
          if (liveWork == null)
          {
            text = row.ToString();
          }
          else
          {
            string text2 = "\u001f";
            <>y__InlineArray2<string> <>y__InlineArray = default(<>y__InlineArray2<string>);
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<string>, string>(ref <>y__InlineArray, 0) = "live";
            *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<string>, string>(ref <>y__InlineArray, 1) = liveWork.Entry.Id;
            text = string.Join(text2, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<string>, string>(in <>y__InlineArray, 2));
          }
        }
        else
        {
          string text3 = "\u001f";
          <>y__InlineArray4<object> <>y__InlineArray2 = default(<>y__InlineArray4<object>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray2, 0) = "handoff";
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray2, 1) = focus;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray2, 2) = newestEntryIdForWork.GetValueOrDefault(handOff.Entry.TargetKey) == handOff.Entry.Id;
          ref object ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray2, 3);
          IChatLiveWork liveWork2 = NotchExtensions.LiveWork;
          ptr = ((liveWork2 != null) ? liveWork2.DisplayName(handOff.Entry) : null);
          text = string.Join(text3, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<object>, object>(in <>y__InlineArray2, 4));
        }
      }
      else
      {
        string text4 = "\u001f";
        <>y__InlineArray8<object> <>y__InlineArray3 = default(<>y__InlineArray8<object>);
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 0) = "message";
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 1) = message.IsLastOfRun;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 2) = message.IsFirstOfRun;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 3) = message.Value.IsUser;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 4) = message.Value.BubbleIsOnlyAboutAttachments;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 5) = message.Value.ActionCaption;
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 6) = string.Join("\u001e", message.Value.AttachmentPaths ?? Array.Empty<string>());
        *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray3, 7) = string.Join("\u001e", message.Value.BubbleTexts);
        text = string.Join(text4, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray8<object>, object>(in <>y__InlineArray3, 8));
      }
      return text;
    }

    // Token: 0x060014CB RID: 5323 RVA: 0x005B1564 File Offset: 0x005B1564
    private static bool Reconcile(UIElementCollection children, List<FrameworkElement> views)
    {
      bool flag = false;
      HashSet<UIElement> hashSet = new HashSet<UIElement>(views);
      for (int i = children.Count - 1; i >= 0; i--)
      {
        if (!hashSet.Contains(children[i]))
        {
          children.RemoveAt(i);
          flag = true;
        }
      }
      for (int j = 0; j < views.Count; j++)
      {
        if (j >= children.Count || children[j] != views[j])
        {
          int num = children.IndexOf(views[j]);
          if (num >= 0)
          {
            children.RemoveAt(num);
          }
          children.Insert(j, views[j]);
          flag = true;
        }
      }
      return flag;
    }

    // Token: 0x060014CC RID: 5324 RVA: 0x005B15FC File Offset: 0x005B15FC
    private FrameworkElement Build(ChatRow row, [Nullable(2)] string focus, IReadOnlyList<ChatHandOff> allEntries)
    {
      ChatRow.Separator separator = row as ChatRow.Separator;
      if (separator != null)
      {
        return ChatBubbles.DateSeparator(separator.Date);
      }
      ChatRow.Message message = row as ChatRow.Message;
      if (message != null)
      {
        return new ChatMessageRow(message.Value, message.IsLastOfRun, delegate(string path)
        {
          NotchPeekSurface.PeekAction("chat_sent_attachment_opened", null);
          INotchActions notchActions = this.host.Actions as INotchActions;
          if (notchActions == null)
          {
            return;
          }
          notchActions.OpenChatArtifact(path, null, null);
        })
        {
          Margin = new Thickness(0.0, (double)(message.IsFirstOfRun ? 8 : 2), 0.0, 0.0)
        };
      }
      ChatRow.HandOff handOff = row as ChatRow.HandOff;
      if (handOff != null)
      {
        ChatHandOffKind kind = handOff.Entry.Kind;
        FrameworkElement frameworkElement;
        if (kind != ChatHandOffKind.Drawing)
        {
          if (kind != ChatHandOffKind.Text)
          {
            frameworkElement = new ChatHandOffRow(this.host, handOff.Entry, allEntries);
          }
          else
          {
            frameworkElement = new ChatTextRow(handOff.Entry);
          }
        }
        else
        {
          frameworkElement = new ChatDrawingRow(this.host, handOff.Entry);
        }
        FrameworkElement frameworkElement2 = frameworkElement;
        Grid grid = new Grid
        {
          Margin = new Thickness(0.0, 12.0, 0.0, 4.0),
          Tag = "handoff-" + handOff.Entry.Id
        };
        if (focus != null)
        {
          Border border = new Border
          {
            CornerRadius = new CornerRadius(14.0),
            Background = Ui.Brush(this.look.InkAt(0.06)),
            Margin = new Thickness(-6.0),
            IsHitTestVisible = false
          };
          grid.Children.Add(border);
        }
        grid.Children.Add(frameworkElement2);
        return grid;
      }
      ChatRow.Typing typing = row as ChatRow.Typing;
      if (typing != null)
      {
        StackPanel stackPanel = new StackPanel
        {
          Spacing = 4.0,
          Margin = new Thickness(0.0, 8.0, 0.0, 0.0),
          HorizontalAlignment = HorizontalAlignment.Left
        };
        stackPanel.Children.Add(ChatBubbles.TypingBubble());
        string statusText = typing.StatusText;
        if (statusText != null)
        {
          TextBlock textBlock = Ui.Text(statusText, 10.5, FontWeights.Normal, this.look.TertiaryText, TextAlignment.Left, 1, false, false);
          textBlock.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
          stackPanel.Children.Add(textBlock);
        }
        return stackPanel;
      }
      ChatRow.SentStatus sentStatus = row as ChatRow.SentStatus;
      if (sentStatus != null)
      {
        TextBlock textBlock2 = Ui.Text(sentStatus.Text, 11.0, FontWeights.Normal, this.look.TertiaryText, TextAlignment.Left, 0, false, false);
        textBlock2.HorizontalAlignment = HorizontalAlignment.Right;
        textBlock2.Margin = new Thickness(6.0, 2.0, 6.0, 0.0);
        return textBlock2;
      }
      ChatRow.LiveWork liveWork = row as ChatRow.LiveWork;
      if (liveWork == null)
      {
        return new Grid();
      }
      IChatLiveWork liveWork2 = NotchExtensions.LiveWork;
      FrameworkElement frameworkElement3 = ((liveWork2 != null) ? liveWork2.WorkCard(liveWork.Entry) : null);
      if (frameworkElement3 == null)
      {
        return new Grid();
      }
      frameworkElement3.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
      frameworkElement3.HorizontalAlignment = HorizontalAlignment.Left;
      return frameworkElement3;
    }

    // Token: 0x060014CD RID: 5325 RVA: 0x005B1968 File Offset: 0x005B1968
    private static void Arrive(FrameworkElement view, ChatRow row)
    {
      NotchChatView.<>c__DisplayClass46_0 CS$<>8__locals1 = new NotchChatView.<>c__DisplayClass46_0();
      CS$<>8__locals1.view = view;
      CS$<>8__locals1.row = row;
      if (Anim.ReduceMotion)
      {
        return;
      }
      NotchChatView.<>c__DisplayClass46_0 CS$<>8__locals2 = CS$<>8__locals1;
      ChatRow.Message message = CS$<>8__locals1.row as ChatRow.Message;
      bool flag;
      if (message != null)
      {
        ConversationMessage value = message.Value;
        if (value != null)
        {
          flag = value.IsUser;
          goto IL_003F;
        }
      }
      flag = false;
      IL_003F:
      CS$<>8__locals2.outgoing = flag;
      CS$<>8__locals1.view.Opacity = 0.0;
      CS$<>8__locals1.view.SizeChanged += CS$<>8__locals1.<Arrive>g__Go|0;
    }

    // Token: 0x060014CE RID: 5326 RVA: 0x005B19E4 File Offset: 0x005B19E4
    [NullableContext(2)]
    private void RenderEmptyState(bool empty, ConversationHistory history)
    {
      this.emptyState.Visibility = (empty ? Visibility.Visible : Visibility.Collapsed);
      if (!empty)
      {
        return;
      }
      string text = ((history != null && history.IsLoadingNewest) ? "loading" : ((history != null && history.HasLoadFailed) ? "failed" : "hello"));
      if (text == this.emptyStateShown && this.emptyState.Children.Count > 0)
      {
        return;
      }
      this.emptyStateShown = text;
      this.emptyState.Children.Clear();
      if (history != null && history.IsLoadingNewest)
      {
        this.emptyState.Children.Add(new ProgressRing
        {
          Width = 16.0,
          Height = 16.0,
          IsActive = true,
          Foreground = Ui.Brush(this.look.SecondaryText)
        });
        return;
      }
      if (history != null && history.HasLoadFailed)
      {
        TextBlock textBlock = Ui.Text("Couldn't load your conversation", 14.0, FontWeights.SemiBold, this.look.PrimaryText, TextAlignment.Left, 0, false, false);
        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        this.emptyState.Children.Add(textBlock);
        Pressable pressable = new Pressable(true)
        {
          Height = 28.0,
          HorizontalAlignment = HorizontalAlignment.Center
        };
        pressable.Children.Add(new GelSurface(GelAppearance.PrimaryBlue, 14.0));
        TextBlock textBlock2 = Ui.Text("Try again", 12.0, FontWeights.SemiBold, DS.Rgb(0.06, 0.16, 0.36, 1.0), TextAlignment.Left, 0, false, false);
        textBlock2.Margin = new Thickness(14.0, 0.0, 14.0, 0.0);
        textBlock2.VerticalAlignment = VerticalAlignment.Center;
        pressable.Children.Add(textBlock2);
        pressable.Click += delegate
        {
          ConversationHistory history2 = NotchChat.History;
          if (history2 == null)
          {
            return;
          }
          history2.Refresh();
        };
        this.emptyState.Children.Add(pressable);
        return;
      }
      TextBlock textBlock3 = Ui.Text("Say hi to HeyClicky", 15.0, FontWeights.SemiBold, this.look.PrimaryText, TextAlignment.Left, 0, false, false);
      textBlock3.HorizontalAlignment = HorizontalAlignment.Center;
      TextBlock textBlock4 = Ui.Text("Everything you ask, by voice or by typing, lives here.", 12.0, FontWeights.Normal, this.look.SecondaryText, TextAlignment.Center, 0, false, false);
      textBlock4.HorizontalAlignment = HorizontalAlignment.Center;
      this.emptyState.Children.Add(textBlock3);
      this.emptyState.Children.Add(textBlock4);
    }

    // Token: 0x060014CF RID: 5327 RVA: 0x005B1C98 File Offset: 0x005B1C98
    [NullableContext(2)]
    private void RenderEarlierRow(bool hasMoreAbove, ConversationHistory history, int totalRowCount)
    {
      string text = ((!hasMoreAbove) ? "none" : ((history != null && history.IsLoadingEarlier) ? "loading" : ("button:" + totalRowCount.ToString())));
      if (text == this.earlierRowShown)
      {
        return;
      }
      this.earlierRowShown = text;
      this.earlierRow.Children.Clear();
      this.earlierRow.Visibility = (hasMoreAbove ? Visibility.Visible : Visibility.Collapsed);
      if (!hasMoreAbove)
      {
        return;
      }
      if (history != null && history.IsLoadingEarlier)
      {
        this.earlierRow.Children.Add(new ProgressRing
        {
          Width = 14.0,
          Height = 14.0,
          IsActive = true,
          Foreground = Ui.Brush(this.look.TertiaryText)
        });
        return;
      }
      Pressable pressable = new Pressable(true)
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      pressable.Children.Add(Ui.Text("Earlier messages", 11.0, FontWeights.Medium, this.look.TertiaryText, TextAlignment.Left, 0, false, false));
      pressable.Click += delegate
      {
        this.ShowEarlierRows(totalRowCount, "button");
      };
      this.earlierRow.Children.Add(pressable);
    }

    // Token: 0x060014D0 RID: 5328 RVA: 0x005B1DF0 File Offset: 0x005B1DF0
    private void ShowEarlierRows(int totalRowCount, string trigger)
    {
      ConversationHistory history = NotchChat.History;
      string text;
      if (totalRowCount > this.shownRowCount)
      {
        this.shownRowCount = Math.Min(totalRowCount, this.shownRowCount + 40);
        this.RenderSoon();
        text = "held_rows";
      }
      else
      {
        if (history == null || !history.HasEarlierMessages || history.IsLoadingEarlier)
        {
          return;
        }
        this.waitingForEarlierPage = true;
        history.Changed += this.OnEarlierPage;
        history.LoadEarlier();
        text = "server";
      }
      this.earlierPagesLoadedCount++;
      ClickyAnalytics.TrackProductEvent("notch_chat_action", null, null, NotchChatAnalytics.EarlierMessagesLoaded(trigger, text, this.earlierPagesLoadedCount));
    }

    // Token: 0x060014D1 RID: 5329 RVA: 0x005B1E94 File Offset: 0x005B1E94
    private void OnEarlierPage()
    {
      ConversationHistory history = NotchChat.History;
      if (history == null || history.IsLoadingEarlier || !this.waitingForEarlierPage)
      {
        return;
      }
      history.Changed -= this.OnEarlierPage;
      this.waitingForEarlierPage = false;
      base.DispatcherQueue.TryEnqueue(delegate
      {
        this.shownRowCount += 40;
        this.Render();
      });
    }

    // Token: 0x060014D2 RID: 5330 RVA: 0x005B1EEC File Offset: 0x005B1EEC
    private void OnScrolled()
    {
      bool flag = this.scroller.ExtentHeight - this.scroller.VerticalOffset - this.scroller.ViewportHeight > 120.0;
      if (flag != this.isAwayFromBottom)
      {
        this.isAwayFromBottom = flag;
        this.jumpDisc.IsHitTestVisible = flag;
        Anim.Opacity(this.jumpDisc, flag > false, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
        if (!flag && this.shownRowCount > 40)
        {
          this.shownRowCount = 40;
          this.RenderSoon();
        }
        if (!flag)
        {
          this.earlierPagesLoadedCount = 0;
        }
      }
      bool flag2 = this.scroller.VerticalOffset < 240.0;
      if (flag2 && !this.nearTop)
      {
        this.ShowEarlierRows(this.CurrentRows().Count, "scroll");
      }
      this.nearTop = flag2;
    }

    // Token: 0x060014D3 RID: 5331 RVA: 0x005B1FD4 File Offset: 0x005B1FD4
    internal static Pressable JumpDisc(MessagesPalette look, Action jump)
    {
      Pressable disc = new Pressable(true)
      {
        Width = 34.0,
        Height = 34.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Bottom,
        Opacity = 0.0,
        IsHitTestVisible = false
      };
      Ellipse ring = new Ellipse
      {
        Fill = Ui.Brush(look.IncomingBubble),
        Stroke = Ui.Brush(DS.White(0.16)),
        StrokeThickness = 1.0
      };
      disc.Children.Add(new ShadowLayer(17.0, DS.Black(0.35), 8.0, 3.0, 0.0));
      disc.Children.Add(ring);
      TextBlock textBlock = Icons.Glyph("\ue74b", 13.0, Colors.White);
      textBlock.FontWeight = FontWeights.Bold;
      disc.Children.Add(textBlock);
      disc.HoverChanged += delegate(bool h)
      {
        ring.Stroke = Ui.Brush(DS.White(h ? 0.35 : 0.16));
        Anim.Scale(disc, h ? 1.06f : 1f, 0.15, Easing.EaseOut(Ui.Compositor), new Vector3?(new Vector3(17f, 17f, 0f)));
      };
      disc.Click += jump;
      disc.Tip("Jump to the newest message");
      return disc;
    }

    // Token: 0x060014D4 RID: 5332 RVA: 0x005B2148 File Offset: 0x005B2148
    private void ScrollToBottom()
    {
      this.scroller.UpdateLayout();
      this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, true);
    }

    // Token: 0x060014D5 RID: 5333 RVA: 0x005B2190 File Offset: 0x005B2190
    private void ScrollToFocusedEntry(string entryId)
    {
      string key = "handoff-" + entryId;
      List<ChatRow> list = this.CurrentRows();
      int num = list.FindIndex((ChatRow r) => r.Id == key);
      if (num >= 0 && list.Count - num > this.shownRowCount)
      {
        this.shownRowCount = list.Count - num;
        this.RenderSoon();
      }
      double[] array = new double[] { 0.05, 0.3, 0.65, 1.1 };
      Func<FrameworkElement, bool> <>9__3;
      for (int i = 0; i < array.Length; i++)
      {
        bool animated = i == 0;
        this.After(array[i], delegate
        {
          NotchPresenter notchPresenter = this.presenter;
          if (((notchPresenter != null) ? notchPresenter.Tabs.ChatFocusEntryId : null) != entryId)
          {
            return;
          }
          IEnumerable<FrameworkElement> enumerable = this.list.Children.OfType<FrameworkElement>();
          Func<FrameworkElement, bool> func;
          if ((func = <>9__3) == null)
          {
            func = (<>9__3 = (FrameworkElement c) => c.Tag as string == key);
          }
          FrameworkElement frameworkElement = enumerable.FirstOrDefault<FrameworkElement>(func);
          if (frameworkElement == null)
          {
            return;
          }
          Point point = frameworkElement.TransformToVisual((UIElement)this.scroller.Content).TransformPoint(new Point(0f, 0f));
          this.isAwayFromBottom = true;
          this.scroller.ChangeView(null, new double?(Math.Max(0.0, point.Y - 52.0 + 12.0)), null, !animated);
        });
      }
      this.After(2.6, delegate
      {
        NotchPresenter notchPresenter2 = this.presenter;
        if (((notchPresenter2 != null) ? notchPresenter2.Tabs.ChatFocusEntryId : null) == entryId)
        {
          this.presenter.Tabs.ChatFocusEntryId = null;
        }
      });
    }

    // Token: 0x060014D6 RID: 5334 RVA: 0x005B227C File Offset: 0x005B227C
    private void After(double seconds, Action action)
    {
      DispatcherQueueTimer dispatcherQueueTimer = base.DispatcherQueue.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(seconds);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        action();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x060014D7 RID: 5335 RVA: 0x005B22D0 File Offset: 0x005B22D0
    private static FrameworkElement BoardDots(double width, double height, Color color)
    {
      GeometryGroup geometryGroup = new GeometryGroup();
      for (double num = 16.0; num <= height - 6.4; num += 16.0)
      {
        for (double num2 = 16.0; num2 <= width - 6.4; num2 += 16.0)
        {
          geometryGroup.Children.Add(new EllipseGeometry
          {
            Center = new Point(num2, num),
            RadiusX = 1.1,
            RadiusY = 1.1
          });
        }
      }
      return new Path
      {
        Data = geometryGroup,
        Fill = Ui.Brush(color),
        IsHitTestVisible = false
      };
    }

    // Token: 0x040009FA RID: 2554
    private readonly INotchHost host;

    // Token: 0x040009FB RID: 2555
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x040009FC RID: 2556
    [Nullable(2)]
    private readonly NotchPresenter presenter;

    // Token: 0x040009FD RID: 2557
    private readonly ScrollViewer scroller;

    // Token: 0x040009FE RID: 2558
    private readonly StackPanel list = new StackPanel
    {
      Padding = new Thickness(12.0, 0.0, 12.0, 0.0)
    };

    // Token: 0x040009FF RID: 2559
    private readonly Border bottomSpacer = new Border();

    // Token: 0x04000A00 RID: 2560
    private readonly Grid earlierRow = new Grid
    {
      Height = 28.0
    };

    // Token: 0x04000A01 RID: 2561
    private readonly StackPanel emptyState = new StackPanel
    {
      Spacing = 8.0,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center,
      Margin = new Thickness(24.0, 0.0, 24.0, 64.0)
    };

    // Token: 0x04000A02 RID: 2562
    private readonly Border bottomFade;

    // Token: 0x04000A03 RID: 2563
    private readonly Border bottomFill;

    // Token: 0x04000A04 RID: 2564
    private readonly NotchChatComposer composer;

    // Token: 0x04000A05 RID: 2565
    private readonly Pressable jumpDisc;

    // Token: 0x04000A06 RID: 2566
    [TupleElementNames(new string[] { "Row", "View", "Look" })]
    [Nullable(new byte[] { 1, 1, 0, 1, 1, 1 })]
    private readonly Dictionary<string, ValueTuple<ChatRow, FrameworkElement, string>> built = new Dictionary<string, ValueTuple<ChatRow, FrameworkElement, string>>();

    // Token: 0x04000A07 RID: 2567
    private int shownRowCount = 40;

    // Token: 0x04000A08 RID: 2568
    private bool waitingForEarlierPage;

    // Token: 0x04000A09 RID: 2569
    private bool isAwayFromBottom;

    // Token: 0x04000A0A RID: 2570
    private bool nearTop;

    // Token: 0x04000A0B RID: 2571
    private bool attached;

    // Token: 0x04000A0C RID: 2572
    private bool renderQueued;

    // Token: 0x04000A0D RID: 2573
    private bool newestWhenShown;

    // Token: 0x04000A0E RID: 2574
    [Nullable(2)]
    private string lastFocusedEntry;

    // Token: 0x04000A0F RID: 2575
    private FrameworkElement boardDots;

    // Token: 0x04000A10 RID: 2576
    private readonly bool isInWindow;

    // Token: 0x04000A11 RID: 2577
    [Nullable(2)]
    private string emptyStateShown;

    // Token: 0x04000A12 RID: 2578
    [Nullable(2)]
    private string earlierRowShown;

    // Token: 0x04000A13 RID: 2579
    private int earlierPagesLoadedCount;
  }
}
