using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Design;
using HeyClicky.App.Documents;
using HeyClicky.App.Home;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Window
{
  // Token: 0x02000141 RID: 321
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class HeyClickyWindowConversation : Grid
  {
    // Token: 0x0600121E RID: 4638 RVA: 0x00594678 File Offset: 0x00594678
    public HeyClickyWindowConversation(INotchHost host, string agentKey)
    {
      this.host = host;
      this.agentKey = agentKey;
      this.scroller = new ScrollViewer
      {
        Content = new Grid
        {
          Children = { this.list }
        },
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
      };
      this.list.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.scroller.ViewChanged += delegate([Nullable(2)] object _, ScrollViewerViewChangedEventArgs _)
      {
        this.OnScrolled();
      };
      this.list.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        if (!this.isAwayFromBottom && base.IsLoaded)
        {
          this.ScrollToNewest();
        }
      };
      base.Children.Add(this.scroller);
      base.Children.Add(this.backing);
      ChatHandOff chatHandOff = this.Entries().LastOrDefault<ChatHandOff>();
      this.composerEntryId = ((chatHandOff != null) ? chatHandOff.Id : null);
      this.composer = new NotchChatComposer(host, this.composerEntryId, true, HeyClickyWindowRules.AgentComposerPlaceholder(this.AgentName()), HeyClickyWindowConversation.StandInEntry(agentKey));
      this.composer.ExtraHeightChanged += delegate(double _)
      {
        this.LayoutComposerBand();
      };
      base.Children.Add(this.composer);
      this.jumpDisc = NotchChatView.JumpDisc(this.look, delegate
      {
        NotchPeekSurface.PeekAction("window_chat_scroll_to_bottom", null);
        this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, false);
      });
      this.jumpDisc.Scale = new Vector3(0.8f, 0.8f, 1f);
      base.Children.Add(this.jumpDisc);
      this.LayoutComposerBand();
      this.Refresh();
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.ScrollToNewest();
      };
    }

    // Token: 0x17000351 RID: 849
    // (get) Token: 0x0600121F RID: 4639 RVA: 0x00594880 File Offset: 0x00594880
    public string AgentKey
    {
      get
      {
        return this.agentKey;
      }
    }

    // Token: 0x06001220 RID: 4640 RVA: 0x00594888 File Offset: 0x00594888
    [NullableContext(2)]
    public void SetStopAction(Action stop)
    {
      this.composer.SetStopAction(stop);
    }

    // Token: 0x06001221 RID: 4641 RVA: 0x00594898 File Offset: 0x00594898
    public void SetDropTargeting(bool on)
    {
      this.composer.SetDropTargeting(on);
    }

    // Token: 0x06001222 RID: 4642 RVA: 0x005948A8 File Offset: 0x005948A8
    public void AttachDroppedFiles(IReadOnlyList<string> paths)
    {
      if (this.composer.AddAttachments(paths) > 0)
      {
        this.composer.FocusField();
      }
    }

    // Token: 0x06001223 RID: 4643 RVA: 0x005948C4 File Offset: 0x005948C4
    private void LayoutComposerBand()
    {
      this.backing.Layout(this.composer.ExtraHeight);
      Thickness padding = this.list.Padding;
      double num = HeyClickyWindowRules.BottomRoom(64.0, this.composer.ExtraHeight, true);
      this.list.Padding = new Thickness(padding.Left, padding.Top, padding.Right, num);
      this.jumpDisc.Margin = new Thickness(0.0, 0.0, 18.0, num + 8.0);
      if (!this.isAwayFromBottom && base.IsLoaded)
      {
        this.ScrollToNewest();
      }
    }

    // Token: 0x06001224 RID: 4644 RVA: 0x00594980 File Offset: 0x00594980
    private void OnScrolled()
    {
      bool flag = this.scroller.ExtentHeight - this.scroller.VerticalOffset - this.scroller.ViewportHeight > 120.0;
      if (flag == this.isAwayFromBottom)
      {
        return;
      }
      this.isAwayFromBottom = flag;
      this.jumpDisc.IsHitTestVisible = flag;
      Anim.Opacity(this.jumpDisc, flag > false, 0.18, Easing.EaseOut(Ui.Compositor), 0.0);
      Anim.Scale(this.jumpDisc, flag ? 1f : 0.8f, 0.18, Easing.EaseOut(Ui.Compositor), new Vector3?(new Vector3(17f, 17f, 0f)));
    }

    // Token: 0x06001225 RID: 4645 RVA: 0x00594A4C File Offset: 0x00594A4C
    [NullableContext(2)]
    private string AgentName()
    {
      AgentService agentService = this.host.Services.Agents as AgentService;
      if (agentService == null)
      {
        return null;
      }
      string text = ClickyJob.JobIdFromRailKey(this.agentKey);
      if (text != null)
      {
        ClickyJob clickyJob = agentService.Jobs.Job(text);
        if (clickyJob == null)
        {
          return null;
        }
        return clickyJob.DisplayName;
      }
      else
      {
        ClickyInfo clickyInfo = agentService.FindClicky(this.agentKey);
        if (clickyInfo == null)
        {
          return null;
        }
        return clickyInfo.Name;
      }
    }

    // Token: 0x06001226 RID: 4646 RVA: 0x00594AB4 File Offset: 0x00594AB4
    internal static ChatHandOff StandInEntry(string agentKey)
    {
      string text = ClickyJob.JobIdFromRailKey(agentKey);
      return new ChatHandOff
      {
        Id = "window-" + agentKey,
        Kind = ChatHandOffKind.Started,
        JobId = text,
        ClickySlug = ((text == null) ? agentKey : null),
        Title = "",
        CreatedAt = DateTimeOffset.UtcNow
      };
    }

    // Token: 0x06001227 RID: 4647 RVA: 0x00594B10 File Offset: 0x00594B10
    private IReadOnlyList<ChatHandOff> Entries()
    {
      HeyClickyWindowConversation.<>c__DisplayClass22_0 CS$<>8__locals1 = new HeyClickyWindowConversation.<>c__DisplayClass22_0();
      HeyClickyWindowConversation.<>c__DisplayClass22_0 CS$<>8__locals2 = CS$<>8__locals1;
      string text = ClickyJob.JobIdFromRailKey(this.agentKey);
      CS$<>8__locals2.target = ((text != null) ? ("job:" + text) : ("clicky:" + this.agentKey));
      ChatHandOffs handOffs = NotchChat.HandOffs;
      return ((handOffs != null) ? handOffs.Entries.Where<ChatHandOff>(delegate(ChatHandOff e)
      {
        bool flag = e.TargetKey == CS$<>8__locals1.target;
        if (flag)
        {
          ChatHandOffKind kind = e.Kind;
          bool flag2 = kind <= ChatHandOffKind.CameBack;
          flag = flag2;
        }
        return flag;
      }).ToList<ChatHandOff>() : null) ?? new List<ChatHandOff>();
    }

    // Token: 0x06001228 RID: 4648 RVA: 0x00594B88 File Offset: 0x00594B88
    public void Refresh()
    {
      using (MessagesPalette.Use(this.look))
      {
        if (!this.AgentExists())
        {
          this.ShowGone();
        }
        else
        {
          IReadOnlyList<ChatHandOff> readOnlyList = this.Entries();
          IChatLiveWork liveWork = NotchExtensions.LiveWork;
          List<ChatHandOff> list = readOnlyList.Skip<ChatHandOff>(Math.Max(0, readOnlyList.Count - this.shownCount)).ToList<ChatHandOff>();
          ChatHandOff chatHandOff = readOnlyList.LastOrDefault<ChatHandOff>();
          string text = ((chatHandOff != null) ? chatHandOff.Id : null);
          List<FrameworkElement> list2 = new List<FrameworkElement>(list.Count + 4);
          if (readOnlyList.Count > list.Count)
          {
            List<FrameworkElement> list3 = list2;
            FrameworkElement frameworkElement;
            if ((frameworkElement = this.earlierButton) == null)
            {
              frameworkElement = (this.earlierButton = this.EarlierButton());
            }
            list3.Add(frameworkElement);
          }
          if (readOnlyList.Count == 0)
          {
            List<FrameworkElement> list4 = list2;
            List<FrameworkElement> list5;
            if ((list5 = this.recordViews) == null)
            {
              list5 = (this.recordViews = this.FromJobRecord().ToList<FrameworkElement>());
            }
            list4.AddRange(list5);
          }
          HashSet<string> keep = new HashSet<string>();
          bool flag = this.built.Count == 0;
          foreach (ChatHandOff chatHandOff2 in list)
          {
            keep.Add(chatHandOff2.Id);
            bool flag2 = chatHandOff2.Id == text;
            string text2 = HeyClickyWindowConversation.RowLook(chatHandOff2, flag2);
            bool flag3 = !this.built.ContainsKey(chatHandOff2.Id);
            ValueTuple<FrameworkElement, string> valueTuple;
            if (!this.built.TryGetValue(chatHandOff2.Id, out valueTuple) || valueTuple.Item2 != text2)
            {
              FrameworkElement frameworkElement2 = ((chatHandOff2.Kind == ChatHandOffKind.Started) ? this.Sent(chatHandOff2) : this.Reply(chatHandOff2, flag2, liveWork));
              if (!flag && flag3 && flag2)
              {
                HeyClickyWindowConversation.FadeIn(frameworkElement2);
              }
              valueTuple = new ValueTuple<FrameworkElement, string>(frameworkElement2, text2);
              this.built[chatHandOff2.Id] = valueTuple;
            }
            list2.Add(valueTuple.Item1);
            if (flag2 && chatHandOff2.Kind == ChatHandOffKind.Started)
            {
              FrameworkElement frameworkElement3 = this.LiveCard(chatHandOff2, liveWork);
              if (frameworkElement3 != null)
              {
                list2.Add(frameworkElement3);
              }
            }
          }
          IEnumerable<string> keys = this.built.Keys;
          Func<string, bool> <>9__0;
          Func<string, bool> func;
          if ((func = <>9__0) == null)
          {
            func = (<>9__0 = (string k) => !keep.Contains(k));
          }
          foreach (string text3 in keys.Where<string>(func).ToList<string>())
          {
            this.built.Remove(text3);
          }
          if (readOnlyList.Count == 0)
          {
            FrameworkElement frameworkElement4 = this.LiveCard(HeyClickyWindowConversation.StandInEntry(this.agentKey), liveWork);
            if (frameworkElement4 != null)
            {
              list2.Add(frameworkElement4);
            }
            if (this.recordViews.Count == 0)
            {
              list2.Add(this.EmptyLine());
            }
          }
          this.ReconcileKeepingPlace(list2);
          this.SyncComposer(readOnlyList.LastOrDefault<ChatHandOff>());
        }
      }
    }

    // Token: 0x06001229 RID: 4649 RVA: 0x00594ECC File Offset: 0x00594ECC
    private unsafe static string RowLook(ChatHandOff e, bool isNewest)
    {
      string text = "\u001f";
      <>y__InlineArray8<object> <>y__InlineArray = default(<>y__InlineArray8<object>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 0) = e.Kind;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 1) = e.Outcome;
      ref object ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 2);
      string fullReply = e.FullReply;
      int num;
      if (fullReply == null)
      {
        string replyText = e.ReplyText;
        num = ((replyText != null) ? replyText.Length : 0);
      }
      else
      {
        num = fullReply.Length;
      }
      ptr = num;
      ref object ptr2 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 3);
      IReadOnlyList<string> artifactEntries = e.ArtifactEntries;
      ptr2 = ((artifactEntries != null) ? artifactEntries.Count : 0);
      ref object ptr3 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 4);
      string sentText = e.SentText;
      ptr3 = ((sentText != null) ? sentText.Length : (-1));
      ref object ptr4 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 5);
      IReadOnlyList<string> sentFiles = e.SentFiles;
      ptr4 = ((sentFiles != null) ? sentFiles.Count : 0);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 6) = e.TaskTitle ?? e.Title;
      ref object ptr5 = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray8<object>, object>(ref <>y__InlineArray, 7);
      bool flag;
      if (e.Kind > ChatHandOffKind.Started && isNewest)
      {
        ChatHandOffOutcome? outcome = e.Outcome;
        if (outcome != null)
        {
          ChatHandOffOutcome valueOrDefault = outcome.GetValueOrDefault();
          flag = valueOrDefault > ChatHandOffOutcome.Finished;
          goto IL_0113;
        }
      }
      flag = false;
      IL_0113:
      ptr5 = flag;
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray8<object>, object>(in <>y__InlineArray, 8));
    }

    // Token: 0x0600122A RID: 4650 RVA: 0x00595000 File Offset: 0x00595000
    [NullableContext(2)]
    private FrameworkElement LiveCard([Nullable(1)] ChatHandOff entry, IChatLiveWork live)
    {
      if (this.liveCard == null)
      {
        FrameworkElement frameworkElement = ((live != null) ? live.WorkCard(entry, true) : null);
        if (frameworkElement != null)
        {
          this.liveCard = frameworkElement;
          frameworkElement.RegisterPropertyChangedCallback(UIElement.VisibilityProperty, delegate(DependencyObject _, DependencyProperty _)
          {
            this.FollowLiveCard();
          });
        }
      }
      return this.liveCard;
    }

    // Token: 0x0600122B RID: 4651 RVA: 0x0059504C File Offset: 0x0059504C
    private void FollowLiveCard()
    {
      if (this.emptyLine != null)
      {
        UIElement uielement = this.emptyLine;
        FrameworkElement frameworkElement = this.liveCard;
        uielement.Visibility = ((frameworkElement != null && frameworkElement.Visibility == Visibility.Visible && this.list.Children.Contains(this.liveCard)) ? Visibility.Collapsed : Visibility.Visible);
      }
    }

    // Token: 0x0600122C RID: 4652 RVA: 0x0059509C File Offset: 0x0059509C
    private FrameworkElement EmptyLine()
    {
      if (this.emptyLine != null)
      {
        return this.emptyLine;
      }
      TextBlock textBlock = Ui.Text("Nothing here yet. Send it a message below.", 12.0, FontWeights.Normal, this.look.SecondaryText, TextAlignment.Left, 0, false, false);
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock.Margin = new Thickness(0.0, 40.0, 0.0, 0.0);
      return this.emptyLine = textBlock;
    }

    // Token: 0x0600122D RID: 4653 RVA: 0x00595120 File Offset: 0x00595120
    private static void FadeIn(FrameworkElement view)
    {
      if (Anim.ReduceMotion)
      {
        return;
      }
      view.Opacity = 0.0;
      Anim.Opacity(view, 1f, 0.22, Easing.EaseOut(Ui.Compositor), 0.0);
    }

    // Token: 0x0600122E RID: 4654 RVA: 0x00595160 File Offset: 0x00595160
    private void ReconcileKeepingPlace(List<FrameworkElement> views)
    {
      HashSet<UIElement> hashSet = new HashSet<UIElement>(views);
      FrameworkElement frameworkElement = null;
      double num = 0.0;
      UIElement uielement = (UIElement)this.scroller.Content;
      if (this.isAwayFromBottom && base.IsLoaded)
      {
        foreach (FrameworkElement frameworkElement2 in this.list.Children.OfType<FrameworkElement>())
        {
          if (hashSet.Contains(frameworkElement2) && frameworkElement2.ActualHeight > 0.0)
          {
            double y = frameworkElement2.TransformToVisual(uielement).TransformPoint(default(Point)).Y;
            if (y + frameworkElement2.ActualHeight > this.scroller.VerticalOffset)
            {
              frameworkElement = frameworkElement2;
              num = y;
              break;
            }
          }
        }
      }
      bool flag = false;
      UIElementCollection children = this.list.Children;
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
          int num2 = children.IndexOf(views[j]);
          if (num2 >= 0)
          {
            children.RemoveAt(num2);
          }
          children.Insert(j, views[j]);
          flag = true;
        }
      }
      this.FollowLiveCard();
      if (!flag || !base.IsLoaded)
      {
        return;
      }
      if (!this.isAwayFromBottom)
      {
        this.ScrollToNewest();
        return;
      }
      if (frameworkElement == null)
      {
        return;
      }
      this.scroller.UpdateLayout();
      double num3 = frameworkElement.TransformToVisual(uielement).TransformPoint(default(Point)).Y - num;
      if (Math.Abs(num3) > 0.5)
      {
        this.scroller.ChangeView(null, new double?(Math.Max(0.0, this.scroller.VerticalOffset + num3)), null, true);
      }
    }

    // Token: 0x0600122F RID: 4655 RVA: 0x005953A0 File Offset: 0x005953A0
    private bool AgentExists()
    {
      AgentService agentService = this.host.Services.Agents as AgentService;
      if (agentService == null)
      {
        return true;
      }
      string text = ClickyJob.JobIdFromRailKey(this.agentKey);
      if (text != null)
      {
        return agentService.Jobs.Job(text) != null;
      }
      return agentService.FindClicky(this.agentKey) != null;
    }

    // Token: 0x17000352 RID: 850
    // (get) Token: 0x06001230 RID: 4656 RVA: 0x005953FC File Offset: 0x005953FC
    // (set) Token: 0x06001231 RID: 4657 RVA: 0x00595404 File Offset: 0x00595404
    public bool IsGone { get; private set; }

    // Token: 0x06001232 RID: 4658 RVA: 0x00595410 File Offset: 0x00595410
    private void ShowGone()
    {
      if (this.IsGone)
      {
        return;
      }
      this.IsGone = true;
      base.Children.Clear();
      TextBlock textBlock = Ui.Text("This chat is gone.", 13.0, FontWeights.Normal, this.look.SecondaryText, TextAlignment.Left, 0, false, false);
      textBlock.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      base.Children.Add(textBlock);
    }

    // Token: 0x06001233 RID: 4659 RVA: 0x0059547C File Offset: 0x0059547C
    private IEnumerable<FrameworkElement> FromJobRecord()
    {
      HeyClickyWindowConversation.<FromJobRecord>d__41 <FromJobRecord>d__ = new HeyClickyWindowConversation.<FromJobRecord>d__41(-2);
      <FromJobRecord>d__.<>4__this = this;
      return <FromJobRecord>d__;
    }

    // Token: 0x06001234 RID: 4660 RVA: 0x0059548C File Offset: 0x0059548C
    private FrameworkElement EarlierButton()
    {
      Pressable pressable = new Pressable(true);
      pressable.HorizontalAlignment = HorizontalAlignment.Center;
      pressable.Padding = new Thickness(12.0, 5.0, 12.0, 5.0);
      pressable.CornerRadius = new CornerRadius(12.0);
      pressable.Background = Ui.Brush(this.look.InkAt(0.07));
      pressable.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
      pressable.Children.Add(Ui.Text("Earlier messages", 11.5, FontWeights.Medium, this.look.SecondaryText, TextAlignment.Left, 0, false, false));
      pressable.Click += delegate
      {
        this.shownCount += 40;
        this.Refresh();
      };
      return pressable;
    }

    // Token: 0x06001235 RID: 4661 RVA: 0x0059557C File Offset: 0x0059557C
    private FrameworkElement Sent(ChatHandOff entry)
    {
      if (entry.SentText == null && entry.SentFiles == null)
      {
        return HeyClickyWindowConversation.SentBubble(entry.TaskTitle ?? entry.Title, entry.CreatedAt, null, null);
      }
      return HeyClickyWindowConversation.SentBubble(entry.SentText, entry.CreatedAt, entry.SentFiles, new Action<string>(this.<Sent>g__Open|43_0));
    }

    // Token: 0x06001236 RID: 4662 RVA: 0x005955DC File Offset: 0x005955DC
    private static FrameworkElement SentBubble([Nullable(2)] string text, DateTimeOffset at, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> files = null, [Nullable(new byte[] { 2, 1 })] Action<string> open = null)
    {
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        MaxWidth = 520.0
      };
      if (files != null && files.Count > 0 && open != null)
      {
        stackPanel.Children.Add(new ChatSentAttachments(files, delegate(string path)
        {
          NotchPeekSurface.PeekAction("window_sent_attachment_opened", null);
          open(path);
        })
        {
          HorizontalAlignment = HorizontalAlignment.Right,
          Margin = new Thickness(0.0, 0.0, 0.0, (double)((text == null) ? 0 : 3))
        });
      }
      if (text != null)
      {
        FrameworkElement frameworkElement = ChatBubbles.Bubble(text, true, true, null);
        frameworkElement.HorizontalAlignment = HorizontalAlignment.Right;
        stackPanel.Children.Add(frameworkElement);
      }
      TextBlock textBlock = Ui.Text(ChatBubbles.ShortTime(at), 10.5, FontWeights.Normal, MessagesPalette.Current.TertiaryText, TextAlignment.Left, 0, false, false);
      textBlock.HorizontalAlignment = HorizontalAlignment.Right;
      textBlock.Margin = new Thickness(0.0, 0.0, 6.0, 0.0);
      stackPanel.Children.Add(textBlock);
      return stackPanel;
    }

    // Token: 0x06001237 RID: 4663 RVA: 0x00595714 File Offset: 0x00595714
    private static FrameworkElement Markdown(string markdown)
    {
      return new MarkdownViewerInlineDocument(markdown, null);
    }

    // Token: 0x06001238 RID: 4664 RVA: 0x00595720 File Offset: 0x00595720
    private FrameworkElement Reply(ChatHandOff entry, bool isNewest, [Nullable(2)] IChatLiveWork live)
    {
      HeyClickyWindowConversation.<>c__DisplayClass46_0 CS$<>8__locals1 = new HeyClickyWindowConversation.<>c__DisplayClass46_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.entry = entry;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 10.0
      };
      FrameworkElement frameworkElement;
      switch (CS$<>8__locals1.entry.Outcome.GetValueOrDefault())
      {
      case ChatHandOffOutcome.Finished:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.Ready, "done", ChatStamp.Seed(CS$<>8__locals1.entry.Id));
        break;
      case ChatHandOffOutcome.Failed:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.NeedsYou, "oops", ChatStamp.Seed(CS$<>8__locals1.entry.Id));
        break;
      case ChatHandOffOutcome.Stopped:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.Paused, "stopped", ChatStamp.Seed(CS$<>8__locals1.entry.Id));
        break;
      default:
        frameworkElement = ChatStamp.Create(ChatStamp.Status.NeedsYou, "you", ChatStamp.Seed(CS$<>8__locals1.entry.Id));
        break;
      }
      FrameworkElement frameworkElement2 = frameworkElement;
      TextBlock textBlock = Ui.Text(ChatBubbles.ShortTime(CS$<>8__locals1.entry.CreatedAt), 11.0, FontWeights.Medium, this.look.TertiaryText, TextAlignment.Left, 0, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      stackPanel.Children.Add(Ui.H(8.0, new UIElement[] { frameworkElement2, textBlock }));
      string fullReply = CS$<>8__locals1.entry.FullReply;
      if (fullReply != null && fullReply.Length > 0)
      {
        stackPanel.Children.Add(HeyClickyWindowConversation.Markdown(ChatReplyPreview.FullReplyMarkdown(CS$<>8__locals1.entry) ?? fullReply));
      }
      else
      {
        string replyText = CS$<>8__locals1.entry.ReplyText;
        if (replyText != null && replyText.Length > 0)
        {
          stackPanel.Children.Add(ChatBubbles.Bubble(replyText, false, true, null));
        }
      }
      IReadOnlyList<string> artifactEntries = CS$<>8__locals1.entry.ArtifactEntries;
      if (artifactEntries != null && artifactEntries.Count > 0)
      {
        stackPanel.Children.Add(((live != null) ? live.FileCards(artifactEntries, new Action<string>(CS$<>8__locals1.<Reply>g__Open|0)) : null) ?? ChatHandOffRow.FilePills(artifactEntries, new Action<string>(CS$<>8__locals1.<Reply>g__Open|0)));
      }
      if (isNewest)
      {
        ChatHandOffOutcome? outcome = CS$<>8__locals1.entry.Outcome;
        if (outcome != null)
        {
          ChatHandOffOutcome valueOrDefault = outcome.GetValueOrDefault();
          if (valueOrDefault != ChatHandOffOutcome.Finished)
          {
            FrameworkElement frameworkElement3 = ((live != null) ? live.WorkCard(CS$<>8__locals1.entry, true) : null);
            if (frameworkElement3 != null)
            {
              stackPanel.Children.Add(frameworkElement3);
            }
          }
        }
      }
      return HeyClickyWindowConversation.LeadingNineTenths(stackPanel);
    }

    // Token: 0x06001239 RID: 4665 RVA: 0x00595978 File Offset: 0x00595978
    private static FrameworkElement LeadingNineTenths(FrameworkElement content)
    {
      return new Grid
      {
        ColumnDefinitions = 
        {
          new ColumnDefinition
          {
            Width = new GridLength(9.0, GridUnitType.Star)
          },
          new ColumnDefinition
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          }
        },
        Children = { content }
      };
    }

    // Token: 0x0600123A RID: 4666 RVA: 0x005959E0 File Offset: 0x005959E0
    [NullableContext(2)]
    private void SyncComposer(ChatHandOff newest)
    {
      if (newest == null || newest.Id == this.composerEntryId)
      {
        return;
      }
      this.composerEntryId = newest.Id;
      this.composer.RetargetFollowUp(newest.Id);
    }

    // Token: 0x0600123B RID: 4667 RVA: 0x00595A18 File Offset: 0x00595A18
    private void ScrollToNewest()
    {
      base.DispatcherQueue.TryEnqueue(delegate
      {
        this.scroller.UpdateLayout();
        this.scroller.ChangeView(null, new double?(this.scroller.ScrollableHeight), null, true);
      });
    }

    // Token: 0x06001243 RID: 4675 RVA: 0x00595ACC File Offset: 0x00595ACC
    [CompilerGenerated]
    private void <Sent>g__Open|43_0(string path)
    {
      INotchActions notchActions = this.host.Actions as INotchActions;
      if (notchActions == null)
      {
        return;
      }
      notchActions.OpenChatArtifact(path, null, null);
    }

    // Token: 0x0400083C RID: 2108
    private const int PageSize = 40;

    // Token: 0x0400083D RID: 2109
    private readonly INotchHost host;

    // Token: 0x0400083E RID: 2110
    private readonly string agentKey;

    // Token: 0x0400083F RID: 2111
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x04000840 RID: 2112
    private readonly StackPanel list = new StackPanel
    {
      Spacing = 10.0,
      Padding = new Thickness(24.0, 18.0, 24.0, 0.0),
      MaxWidth = 728.0
    };

    // Token: 0x04000841 RID: 2113
    private readonly ScrollViewer scroller;

    // Token: 0x04000842 RID: 2114
    private readonly HeyClickyWindowComposerBacking backing = new HeyClickyWindowComposerBacking();

    // Token: 0x04000843 RID: 2115
    private readonly NotchChatComposer composer;

    // Token: 0x04000844 RID: 2116
    private readonly Pressable jumpDisc;

    // Token: 0x04000845 RID: 2117
    [Nullable(2)]
    private string composerEntryId;

    // Token: 0x04000846 RID: 2118
    private int shownCount = 40;

    // Token: 0x04000847 RID: 2119
    private bool isAwayFromBottom;

    // Token: 0x04000848 RID: 2120
    [TupleElementNames(new string[] { "View", "Look" })]
    [Nullable(new byte[] { 1, 1, 0, 1, 1 })]
    private readonly Dictionary<string, ValueTuple<FrameworkElement, string>> built = new Dictionary<string, ValueTuple<FrameworkElement, string>>();

    // Token: 0x04000849 RID: 2121
    [Nullable(2)]
    private FrameworkElement earlierButton;

    // Token: 0x0400084A RID: 2122
    [Nullable(2)]
    private FrameworkElement liveCard;

    // Token: 0x0400084B RID: 2123
    [Nullable(2)]
    private FrameworkElement emptyLine;

    // Token: 0x0400084C RID: 2124
    [Nullable(new byte[] { 2, 1 })]
    private List<FrameworkElement> recordViews;
  }
}
