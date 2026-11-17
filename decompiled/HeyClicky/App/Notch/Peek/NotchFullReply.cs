using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Documents;
using HeyClicky.App.Home;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200018D RID: 397
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchFullReply : Grid
  {
    // Token: 0x0600159F RID: 5535 RVA: 0x005B5514 File Offset: 0x005B5514
    public NotchFullReply(INotchHost host, string entryId)
    {
      NotchFullReply.<>c__DisplayClass4_0 CS$<>8__locals1 = new NotchFullReply.<>c__DisplayClass4_0();
      CS$<>8__locals1.host = host;
      CS$<>8__locals1.entryId = entryId;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      NotchFullReply.<>c__DisplayClass4_1 CS$<>8__locals2 = new NotchFullReply.<>c__DisplayClass4_1();
      CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
      this.host = CS$<>8__locals2.CS$<>8__locals1.host;
      this.entryId = CS$<>8__locals2.CS$<>8__locals1.entryId;
      NotchFullReply.<>c__DisplayClass4_1 CS$<>8__locals3 = CS$<>8__locals2;
      ChatHandOffs handOffs = NotchChat.HandOffs;
      CS$<>8__locals3.entry = ((handOffs != null) ? handOffs.Entry(CS$<>8__locals2.CS$<>8__locals1.entryId) : null);
      string text = ChatReplyPreview.FullReplyMarkdown(CS$<>8__locals2.entry);
      base.Margin = new Thickness(-18.0, -6.0, -18.0, -6.0);
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(34.0)
      });
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      base.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      Grid grid = new Grid
      {
        IsHitTestVisible = false
      };
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(34.0)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(56.0)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      Border border = new Border
      {
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, Colors.Black),
          new ValueTuple<double, Color>(1.0, HomeTheme.PaneFill)
        })
      };
      Border border2 = new Border
      {
        Background = Ui.Brush(HomeTheme.PaneFill)
      };
      Grid.SetRow(border, 1);
      Grid.SetRow(border2, 2);
      grid.Children.Add(new Border
      {
        Background = Ui.Brush(Colors.Black)
      });
      grid.Children.Add(border);
      grid.Children.Add(border2);
      Grid.SetRowSpan(grid, 3);
      base.Children.Add(grid);
      Pressable pressable = new Pressable(true)
      {
        Width = 26.0,
        Height = 26.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Bottom
      };
      CS$<>8__locals2.disc = new Ellipse
      {
        Fill = Ui.Brush(DS.White(0.09))
      };
      CS$<>8__locals2.x = Icons.Glyph("\ue711", 11.0, DS.White(0.7));
      CS$<>8__locals2.x.FontWeight = FontWeights.Bold;
      pressable.Children.Add(CS$<>8__locals2.disc);
      pressable.Children.Add(CS$<>8__locals2.x);
      pressable.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals2.disc.Fill = Ui.Brush(DS.White(h ? 0.16 : 0.09));
        CS$<>8__locals2.x.Foreground = Ui.Brush(DS.White(h ? 0.95 : 0.7));
      };
      pressable.Click += delegate
      {
        NotchPresenter notchPresenter = CS$<>8__locals2.CS$<>8__locals1.host.Services.Presenter as NotchPresenter;
        if (notchPresenter == null)
        {
          return;
        }
        notchPresenter.Tabs.CloseFullReply(new NotchChatFullReplyCloseVia?(NotchChatFullReplyCloseVia.CloseButton));
      };
      pressable.Tip("Back to the chat (esc)");
      pressable.Margin = new Thickness(0.0, 0.0, 30.0, 2.0);
      base.Children.Add(pressable);
      double num = NotchPeekLayout.FullReplyContentWidth(text) - 24.0;
      this.column.Width = num;
      this.column.Margin = new Thickness(0.0, 8.0, 0.0, 20.0);
      this.column.HorizontalAlignment = HorizontalAlignment.Center;
      this.column.VerticalAlignment = VerticalAlignment.Top;
      if (CS$<>8__locals2.entry != null)
      {
        TextBlock textBlock = Ui.Text(CS$<>8__locals2.entry.TaskTitle ?? CS$<>8__locals2.entry.Title, 15.0, FontWeights.SemiBold, HomeTheme.PrimaryText, TextAlignment.Left, 2, false, false);
        textBlock.TextWrapping = TextWrapping.Wrap;
        TextBlock textBlock2 = Ui.Text(((CS$<>8__locals2.entry.TaskTitle != null && CS$<>8__locals2.entry.TaskTitle != CS$<>8__locals2.entry.Title) ? (CS$<>8__locals2.entry.Title + "  ·  ") : "") + ChatBubbles.ShortTime(CS$<>8__locals2.entry.CreatedAt), 11.0, FontWeights.Normal, HomeTheme.TertiaryText, TextAlignment.Left, 0, false, false);
        this.column.Children.Add(new StackPanel
        {
          Spacing = 2.0,
          Children = { textBlock, textBlock2 }
        });
      }
      if (text != null)
      {
        this.column.Children.Add(new MarkdownViewerInlineDocument(text, null));
      }
      ChatHandOff entry = CS$<>8__locals2.entry;
      IReadOnlyList<string> readOnlyList = ((entry != null) ? entry.ArtifactEntries : null);
      if (readOnlyList != null && readOnlyList.Count > 0)
      {
        IChatLiveWork liveWork = NotchExtensions.LiveWork;
        FrameworkElement frameworkElement = ((liveWork != null) ? liveWork.FileCards(readOnlyList, new Action<string>(CS$<>8__locals2.<.ctor>g__Open|6)) : null) ?? ChatHandOffRow.FilePills(readOnlyList, new Action<string>(CS$<>8__locals2.<.ctor>g__Open|6));
        frameworkElement.Margin = new Thickness(0.0, -8.0, 0.0, 0.0);
        this.column.Children.Add(frameworkElement);
      }
      if (CS$<>8__locals2.entry != null)
      {
        this.column.Children.Add(NotchFullReply.Footer(CS$<>8__locals2.entry));
        ChatHandOffs handOffs2 = NotchChat.HandOffs;
        IReadOnlyList<string> readOnlyList2 = ((handOffs2 != null) ? handOffs2.ShownFollowUps(CS$<>8__locals2.entry.Id) : null) ?? Array.Empty<string>();
        if (readOnlyList2.Count > 0)
        {
          WrapPanel wrapPanel = new WrapPanel
          {
            HorizontalSpacing = 6.0,
            VerticalSpacing = 6.0
          };
          foreach (string text2 in readOnlyList2)
          {
            wrapPanel.Children.Add(this.Pill(CS$<>8__locals2.entry, text2));
          }
          this.column.Children.Add(wrapPanel);
        }
      }
      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = this.column,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        Padding = new Thickness(30.0, 0.0, 30.0, 0.0)
      };
      Grid.SetRow(scrollViewer, 1);
      base.Children.Add(scrollViewer);
      this.column.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        NotchPresenter notchPresenter2 = CS$<>8__locals2.CS$<>8__locals1.host.Services.Presenter as NotchPresenter;
        NotchTabs notchTabs = ((notchPresenter2 != null) ? notchPresenter2.Tabs : null);
        double num2 = Math.Ceiling(e.NewSize.Height + CS$<>8__locals2.CS$<>8__locals1.<>4__this.column.Margin.Top + CS$<>8__locals2.CS$<>8__locals1.<>4__this.column.Margin.Bottom);
        if (notchTabs != null && notchTabs.ExpandedReplyEntryId == CS$<>8__locals2.CS$<>8__locals1.entryId)
        {
          double? fullReplyContentHeight = notchTabs.FullReplyContentHeight;
          double num3 = num2;
          if (!((fullReplyContentHeight.GetValueOrDefault() == num3) & (fullReplyContentHeight != null)))
          {
            notchTabs.FullReplyContentHeight = new double?(num2);
            CS$<>8__locals2.CS$<>8__locals1.host.Relayout();
          }
        }
      };
      FrameworkElement frameworkElement2 = this.FollowUpFooter(CS$<>8__locals2.entry);
      Grid.SetRow(frameworkElement2, 2);
      base.Children.Add(frameworkElement2);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        ChatHandOffs handOffs3 = NotchChat.HandOffs;
        if (handOffs3 != null)
        {
          handOffs3.Changed += base.<.ctor>g__OnHandOffsChanged|3;
        }
        CS$<>8__locals2.CS$<>8__locals1.<>4__this.CloseIfEntryGone();
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        ChatHandOffs handOffs4 = NotchChat.HandOffs;
        if (handOffs4 != null)
        {
          handOffs4.Changed -= base.<.ctor>g__OnHandOffsChanged|3;
        }
      };
    }

    // Token: 0x060015A0 RID: 5536 RVA: 0x005B5C90 File Offset: 0x005B5C90
    private void CloseIfEntryGone()
    {
      ChatHandOffs handOffs = NotchChat.HandOffs;
      if (((handOffs != null) ? handOffs.Entry(this.entryId) : null) != null)
      {
        return;
      }
      NotchPresenter notchPresenter = this.host.Services.Presenter as NotchPresenter;
      NotchTabs notchTabs = ((notchPresenter != null) ? notchPresenter.Tabs : null);
      if (notchTabs != null && notchTabs.ExpandedReplyEntryId == this.entryId)
      {
        notchTabs.CloseFullReply(null);
      }
    }

    // Token: 0x060015A1 RID: 5537 RVA: 0x005B5D00 File Offset: 0x005B5D00
    private static FrameworkElement Footer(ChatHandOff entry)
    {
      TextBlock textBlock = Ui.Text(ChatBubbles.ShortTime(entry.CreatedAt), 11.0, FontWeights.Normal, HomeTheme.TertiaryText, TextAlignment.Left, 0, false, false);
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      Pressable pressable = new Pressable(true)
      {
        Width = 22.0,
        Height = 22.0
      };
      TextBlock glyph = Icons.Glyph("\ue8c8", 11.0, HomeTheme.TertiaryText);
      pressable.Children.Add(glyph);
      pressable.HoverChanged += delegate(bool h)
      {
        glyph.Foreground = Ui.Brush(h ? HomeTheme.PrimaryText : HomeTheme.TertiaryText);
      };
      pressable.Click += delegate
      {
        DataPackage dataPackage = new DataPackage();
        string text;
        if ((text = entry.FullReply) == null)
        {
          text = entry.ReplyText ?? "";
        }
        dataPackage.SetText(text);
        Clipboard.SetContent(dataPackage);
        glyph.Text = "\ue73e";
      };
      pressable.Tip("Copy");
      StackPanel stackPanel = Ui.H(6.0, new UIElement[] { textBlock, pressable });
      stackPanel.Margin = new Thickness(0.0, -8.0, 0.0, 0.0);
      return stackPanel;
    }

    // Token: 0x060015A2 RID: 5538 RVA: 0x005B5E20 File Offset: 0x005B5E20
    private FrameworkElement FollowUpFooter([Nullable(2)] ChatHandOff entry)
    {
      Grid grid = new Grid();
      if (entry != null)
      {
        grid.Children.Add(new NotchChatComposer(this.host, entry.Id, false, null, null)
        {
          Margin = new Thickness(18.0, 0.0, 18.0, 6.0)
        });
      }
      else
      {
        grid.Height = 64.0;
      }
      grid.Children.Add(new Border
      {
        Height = 1.0,
        Background = Ui.Brush(DS.White(0.08)),
        VerticalAlignment = VerticalAlignment.Top,
        IsHitTestVisible = false
      });
      return grid;
    }

    // Token: 0x060015A3 RID: 5539 RVA: 0x005B5EDC File Offset: 0x005B5EDC
    private FrameworkElement Pill(ChatHandOff entry, string followUp)
    {
      Pressable p = new Pressable(true)
      {
        Height = 24.0,
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(DS.White(0.07)),
        BorderBrush = Ui.Brush(DS.White(0.1)),
        BorderThickness = new Thickness(1.0)
      };
      TextBlock label = Ui.Text(followUp, 12.0, FontWeights.Medium, HomeTheme.SecondaryText, TextAlignment.Left, 1, false, false);
      label.Margin = new Thickness(11.0, 0.0, 11.0, 0.0);
      label.VerticalAlignment = VerticalAlignment.Center;
      p.Children.Add(label);
      p.HoverChanged += delegate(bool h)
      {
        p.Background = Ui.Brush(DS.White(h ? 0.13 : 0.07));
        p.BorderBrush = Ui.Brush(DS.White(h ? 0.18 : 0.1));
        label.Foreground = Ui.Brush(h ? HomeTheme.PrimaryText : HomeTheme.SecondaryText);
      };
      p.Tip("Send \"" + followUp + "\"");
      AutomationProperties.SetName(p, "Ask: " + followUp);
      p.Click += delegate
      {
        INotchActions notchActions = this.host.Actions as INotchActions;
        if (notchActions == null)
        {
          return;
        }
        notchActions.SendChatFollowUp(entry, followUp);
      };
      return p;
    }

    // Token: 0x04000A4A RID: 2634
    private const double TextInset = 12.0;

    // Token: 0x04000A4B RID: 2635
    private readonly INotchHost host;

    // Token: 0x04000A4C RID: 2636
    private readonly string entryId;

    // Token: 0x04000A4D RID: 2637
    private readonly StackPanel column = new StackPanel
    {
      Spacing = 14.0
    };
  }
}
