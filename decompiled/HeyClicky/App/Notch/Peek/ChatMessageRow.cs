using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000176 RID: 374
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ChatMessageRow : Grid
  {
    // Token: 0x0600149D RID: 5277 RVA: 0x005ADC4C File Offset: 0x005ADC4C
    public ChatMessageRow(ConversationMessage message, bool isLastOfRun, Action<string> openAttachment)
    {
      this.outgoing = message.IsUser;
      this.frame = ChatMessageRow.Frame(message);
      this.lastOfRun = isLastOfRun;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0,
        HorizontalAlignment = (this.outgoing ? HorizontalAlignment.Right : HorizontalAlignment.Left),
        MaxWidth = 390.0
      };
      if (this.outgoing)
      {
        IReadOnlyList<string> attachmentPaths = message.AttachmentPaths;
        if (attachmentPaths != null && attachmentPaths.Count > 0)
        {
          ChatSentAttachments chatSentAttachments = new ChatSentAttachments(attachmentPaths, openAttachment)
          {
            HorizontalAlignment = HorizontalAlignment.Right
          };
          if (!message.BubbleIsOnlyAboutAttachments)
          {
            chatSentAttachments.Margin = new Thickness(0.0, 0.0, 0.0, 3.0);
          }
          stackPanel.Children.Add(chatSentAttachments);
        }
      }
      if (!message.BubbleIsOnlyAboutAttachments)
      {
        IReadOnlyList<string> bubbleTexts = message.BubbleTexts;
        StackPanel stackPanel2 = new StackPanel();
        stackPanel2.Spacing = 2.0;
        stackPanel2.HorizontalAlignment = (this.outgoing ? HorizontalAlignment.Right : HorizontalAlignment.Left);
        StackPanel stackPanel3 = stackPanel2;
        this.bubbleStack = stackPanel2;
        StackPanel stackPanel4 = stackPanel3;
        for (int i = 0; i < bubbleTexts.Count; i++)
        {
          stackPanel4.Children.Add(ChatBubbles.Bubble(bubbleTexts[i], this.outgoing, isLastOfRun && i == bubbleTexts.Count - 1, this.look));
        }
        this.texts.AddRange(bubbleTexts);
        stackPanel.Children.Add(stackPanel4);
      }
      string actionCaption = message.ActionCaption;
      if (actionCaption != null)
      {
        StackPanel stackPanel5 = new StackPanel
        {
          Spacing = 2.0,
          Margin = new Thickness(6.0, 0.0, 6.0, 0.0)
        };
        foreach (ChatActionCaption.Item item in ChatActionCaption.Items(actionCaption))
        {
          TextBlock textBlock = Icons.Glyph(ChatBubbles.Glyph(item.Symbol), 9.0, this.look.TertiaryText);
          textBlock.FontWeight = FontWeights.SemiBold;
          textBlock.Width = 11.0;
          textBlock.VerticalAlignment = VerticalAlignment.Top;
          textBlock.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
          TextBlock textBlock2 = Ui.Text(item.Text, 10.5, FontWeights.Normal, this.look.TertiaryText, TextAlignment.Left, 2, false, false);
          textBlock2.TextWrapping = TextWrapping.Wrap;
          stackPanel5.Children.Add(Ui.H(5.0, new UIElement[] { textBlock, textBlock2 }));
        }
        stackPanel.Children.Add(stackPanel5);
      }
      stackPanel.Margin = (this.outgoing ? new Thickness(56.0, 0.0, 0.0, 0.0) : new Thickness(0.0, 0.0, 56.0, 0.0));
      base.Children.Add(stackPanel);
    }

    // Token: 0x0600149E RID: 5278 RVA: 0x005ADFD0 File Offset: 0x005ADFD0
    private unsafe static string Frame(ConversationMessage m)
    {
      string text = "\u001f";
      <>y__InlineArray4<object> <>y__InlineArray = default(<>y__InlineArray4<object>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 0) = m.IsUser;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 1) = m.BubbleIsOnlyAboutAttachments;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 2) = m.ActionCaption;
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray4<object>, object>(ref <>y__InlineArray, 3) = string.Join("\u001e", m.AttachmentPaths ?? Array.Empty<string>());
      return string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray4<object>, object>(in <>y__InlineArray, 4));
    }

    // Token: 0x0600149F RID: 5279 RVA: 0x005AE050 File Offset: 0x005AE050
    public bool TryGrow(ConversationMessage message, bool isLastOfRun)
    {
      if (this.bubbleStack == null || ChatMessageRow.Frame(message) != this.frame)
      {
        return false;
      }
      IReadOnlyList<string> bubbleTexts = message.BubbleTexts;
      if (bubbleTexts.Count < this.texts.Count)
      {
        return false;
      }
      for (int i = 0; i < this.texts.Count; i++)
      {
        if (bubbleTexts[i] != this.texts[i])
        {
          return false;
        }
      }
      if (this.texts.Count > 0)
      {
        int num = this.texts.Count - 1;
        bool flag = this.lastOfRun;
        bool flag2 = isLastOfRun && num == bubbleTexts.Count - 1;
        if (flag != flag2)
        {
          this.bubbleStack.Children[num] = ChatBubbles.Bubble(this.texts[num], this.outgoing, flag2, this.look);
        }
      }
      for (int j = this.texts.Count; j < bubbleTexts.Count; j++)
      {
        FrameworkElement frameworkElement = ChatBubbles.Bubble(bubbleTexts[j], this.outgoing, isLastOfRun && j == bubbleTexts.Count - 1, this.look);
        this.bubbleStack.Children.Add(frameworkElement);
        this.GrowIn(frameworkElement);
        this.texts.Add(bubbleTexts[j]);
      }
      this.lastOfRun = isLastOfRun;
      return true;
    }

    // Token: 0x060014A0 RID: 5280 RVA: 0x005AE1B0 File Offset: 0x005AE1B0
    private void GrowIn(FrameworkElement bubble)
    {
      ChatMessageRow.<>c__DisplayClass9_0 CS$<>8__locals1 = new ChatMessageRow.<>c__DisplayClass9_0();
      CS$<>8__locals1.bubble = bubble;
      CS$<>8__locals1.<>4__this = this;
      if (Anim.ReduceMotion)
      {
        return;
      }
      CS$<>8__locals1.bubble.Opacity = 0.0;
      CS$<>8__locals1.bubble.SizeChanged += CS$<>8__locals1.<GrowIn>g__Go|0;
    }

    // Token: 0x040009F0 RID: 2544
    private readonly bool outgoing;

    // Token: 0x040009F1 RID: 2545
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x040009F2 RID: 2546
    private readonly string frame;

    // Token: 0x040009F3 RID: 2547
    [Nullable(2)]
    private readonly StackPanel bubbleStack;

    // Token: 0x040009F4 RID: 2548
    private readonly List<string> texts = new List<string>();

    // Token: 0x040009F5 RID: 2549
    private bool lastOfRun;
  }
}
