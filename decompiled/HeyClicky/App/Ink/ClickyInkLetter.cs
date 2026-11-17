using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200027A RID: 634
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkLetter
  {
    // Token: 0x06001ADB RID: 6875 RVA: 0x005E897C File Offset: 0x005E897C
    private static List<string> Choices([Nullable(2)] string text)
    {
      return (from c in (text ?? "").Split('|', StringSplitOptions.None)
        select c.TrimWs() into c
        where c.Length > 0
        select c).ToList<string>();
    }

    // Token: 0x06001ADC RID: 6876 RVA: 0x005E89E8 File Offset: 0x005E89E8
    public unsafe static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkActionFeedback feedback = actions.Feedback;
      bool flag;
      if (feedback.IsPending(block.Source))
      {
        ClickyInkSentAction pending = feedback.Pending;
        flag = pending != null && pending.Kind == ClickyInkNoteAction.ActionKind.Send;
      }
      else
      {
        flag = false;
      }
      bool flag2 = flag;
      bool flag3 = flag2 || feedback.AnswerFor(block.Source) != null;
      bool flag4;
      if (feedback.IsPending(block.Source))
      {
        ClickyInkSentAction pending2 = feedback.Pending;
        flag4 = pending2 != null && pending2.Kind == ClickyInkNoteAction.ActionKind.Ask;
      }
      else
      {
        flag4 = false;
      }
      bool flag5 = flag4;
      List<string> list = ClickyInkLetter.Choices(block.Source["tones"]);
      List<string> list2;
      if (list.Count != 0)
      {
        list2 = list.Take<string>(3).ToList<string>();
      }
      else
      {
        int num = 3;
        List<string> list3 = new List<string>(num);
        CollectionsMarshal.SetCount<string>(list3, num);
        list2 = list3;
        Span<string> span = CollectionsMarshal.AsSpan<string>(list3);
        int num2 = 0;
        *span[num2] = "Shorter";
        num2++;
        *span[num2] = "Warmer";
        num2++;
        *span[num2] = "More formal";
      }
      List<string> list4 = list2;
      Grid grid = new Grid
      {
        Height = 26.0
      };
      grid.Children.Add(ClickyShapeView.Fill(new ClickyInkFlap(), PaperColor.Hex("#E9DCC3")));
      grid.Children.Add(ClickyShapeView.Stroke(new ClickyInkFlap(), PaperColor.Hex("#C9B791"), 1.0));
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 6.0,
        Padding = new Thickness(14.0),
        Width = contentWidth - 10.0
      };
      string text = block.Source["to"];
      if (text != null)
      {
        stackPanel.Children.Add(ClickyInkLetter.Header("To", text, style));
      }
      string text2 = block.Source["subject"];
      if (text2 != null)
      {
        stackPanel.Children.Add(ClickyInkLetter.Header("Subject", text2, style));
      }
      stackPanel.Children.Add(new Border
      {
        Height = 1.0,
        Margin = new Thickness(0.0, 2.0, 0.0, 2.0),
        Background = Ui.Brush(style.Ink.Opacity(0.1))
      });
      bool isLong = block.Words.Current.CharacterCount() > 1100;
      stackPanel.Children.Add(ClickyInkWordsView.Make(block.Words, clock, style, ClickyInkObjectFonts.SerifFont(isLong ? 11.5 : 12.5, null), true, new Color?(ClickyInkLetter.LetterInk), null).With(delegate(InkTextView t)
      {
        t.LineSpacing = (double)(isLong ? 2 : 3);
      }));
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, Array.Empty<UIElement>())
      {
        Margin = new Thickness(0.0, 4.0, 0.0, 0.0)
      };
      if (feedback.AnswerFor(block.Source) == null)
      {
        using (List<string>.Enumerator enumerator = list4.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string tone = enumerator.Current;
            InkTextView inkTextView = InkTextView.Plain(tone, ClickyPaperFonts.Typed(11.5, new FontWeight?(FontWeights.Medium)), ClickyInkLetter.LetterInk.Opacity(flag5 ? 0.3 : 0.55));
            Pressable pressable = new Pressable(true)
            {
              Children = { inkTextView }
            };
            pressable.Enabled = !flag5 && !flag2;
            pressable.Click += delegate
            {
              actions.Perform(ClickyInkNoteActions.Retone(block.Source, tone));
            };
            AutomationProperties.SetName(pressable, tone);
            clickyInkHStack.Children.Add(pressable);
          }
        }
      }
      clickyInkHStack.Children.Add(new ClickyInkSpacer());
      clickyInkHStack.Children.Add(ClickyInkLetter.CopyButton(block, actions));
      string send = block.Source["send"] ?? block.Source["ask"];
      if (send != null)
      {
        ClickyPaperGelButton clickyPaperGelButton = new ClickyPaperGelButton(flag2 ? "Sending…" : "Send", delegate
        {
          actions.Send(send);
        }, ClickyPaperGel.Blue, 26.0, 13.0, 12.0, null);
        clickyPaperGelButton.Enabled = !flag3;
        clickyPaperGelButton.Opacity = ((flag3 && !feedback.IsPending(block.Source)) ? 0.45 : 1.0);
        clickyInkHStack.Children.Add(clickyPaperGelButton);
      }
      clickyInkHStack.Opacity = clock >= block.Words.FinishedAt;
      stackPanel.Children.Add(clickyInkHStack);
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(ClickyShape.Rectangle, "rect", PaperColor.Hex("#FFFEFA"), new PaperShadow(PaperColor.Black.Opacity(0.12), 3.0, 0.0, 2.0), null));
      return new StackPanel
      {
        Width = contentWidth - 10.0,
        Margin = new Thickness(4.0, 0.0, 0.0, 0.0),
        HorizontalAlignment = HorizontalAlignment.Left,
        Children = { grid, clickyInkOverlay }
      };
    }

    // Token: 0x06001ADD RID: 6877 RVA: 0x005E9060 File Offset: 0x005E9060
    private static FrameworkElement CopyButton(ClickyInkLiveNote.Block block, ClickyInkActions actions)
    {
      DateTimeOffset dateTimeOffset;
      bool flag = ClickyInkLetter.CopiedAt.TryGetValue(block.Id, out dateTimeOffset) && (DateTimeOffset.Now - dateTimeOffset).TotalSeconds < 1.6;
      ClickyPaperGelButton button = new ClickyPaperGelButton(flag ? "Copied" : "Copy", null, ClickyPaperGel.White, 26.0, 13.0, 12.0, flag ? "checkmark" : null);
      button.Click += delegate
      {
        DataPackage dataPackage = new DataPackage();
        dataPackage.SetText(block.Words.Current);
        Clipboard.SetContent(dataPackage);
        actions.Track("letter_copied", ClickyInkDocument.Kind.Letter, new JsonObject(null));
        DateTimeOffset pressedAt = DateTimeOffset.Now;
        ClickyInkLetter.CopiedAt[block.Id] = pressedAt;
        button.AnimateTitle("Copied", "checkmark", true);
        Once.After(1.6, delegate
        {
          DateTimeOffset dateTimeOffset2;
          if (!ClickyInkLetter.CopiedAt.TryGetValue(block.Id, out dateTimeOffset2) || dateTimeOffset2 != pressedAt)
          {
            return;
          }
          ClickyInkLetter.CopiedAt.Remove(block.Id);
          button.AnimateTitle("Copy", null, false);
        }, DispatcherQueue.GetForCurrentThread());
      };
      return button;
    }

    // Token: 0x06001ADE RID: 6878 RVA: 0x005E911C File Offset: 0x005E911C
    private static FrameworkElement Header(string label, string value, ClickyInkStyle style)
    {
      InkTextView inkTextView = InkTextView.Plain(label, ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.SemiBold)), style.Quiet).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      inkTextView.Width = 52.0;
      double num = 8.0;
      InkVerticalAlignment inkVerticalAlignment = InkVerticalAlignment.Center;
      UIElement[] array = new UIElement[2];
      array[0] = inkTextView;
      array[1] = InkTextView.Plain(value, ClickyPaperFonts.Typed(12.5, new FontWeight?(FontWeights.Medium)), style.Ink).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      return new ClickyInkHStack(num, inkVerticalAlignment, array);
    }

    // Token: 0x04000CDD RID: 3293
    private static readonly Color LetterInk = PaperColor.Hex("#2B2620");

    // Token: 0x04000CDE RID: 3294
    private static readonly Dictionary<string, DateTimeOffset> CopiedAt = new Dictionary<string, DateTimeOffset>();

    // Token: 0x04000CDF RID: 3295
    private const double CopiedShowsForSeconds = 1.6;
  }
}
