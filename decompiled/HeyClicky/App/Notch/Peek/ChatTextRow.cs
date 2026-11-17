using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200017B RID: 379
  public sealed class ChatTextRow : StackPanel
  {
    // Token: 0x060014B4 RID: 5300 RVA: 0x005AFB48 File Offset: 0x005AFB48
    [NullableContext(1)]
    public ChatTextRow(ChatHandOff entry)
    {
      ChatTextRow <>4__this = this;
      MessagesPalette messagesPalette = MessagesPalette.Current;
      base.Spacing = 6.0;
      string delivered = entry.DeliveredText;
      if (delivered == null || delivered.Length <= 0)
      {
        return;
      }
      bool valueOrDefault = entry.WasCopied.GetValueOrDefault();
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
      Border border = new Border
      {
        Width = 22.0,
        Height = 22.0,
        CornerRadius = new CornerRadius(6.6),
        Background = Ui.Brush(messagesPalette.InkAt(0.08))
      };
      TextBlock textBlock = Icons.Glyph(valueOrDefault ? "\ue77f" : "\ue8d2", 10.5, messagesPalette.SecondaryText);
      textBlock.FontWeight = FontWeights.SemiBold;
      border.Child = textBlock;
      grid.Children.Add(border);
      TextBlock textBlock2 = new TextBlock
      {
        FontSize = 11.5,
        FontWeight = FontWeights.Medium,
        FontFamily = DS.Fonts.Small,
        IsTextScaleFactorEnabled = false,
        TextTrimming = TextTrimming.CharacterEllipsis,
        VerticalAlignment = VerticalAlignment.Center
      };
      if (valueOrDefault)
      {
        textBlock2.Inlines.Add(new Run
        {
          Text = "Copied to your clipboard",
          Foreground = Ui.Brush(messagesPalette.SecondaryText)
        });
      }
      else
      {
        string deliveredToApp = entry.DeliveredToApp;
        if (deliveredToApp != null && deliveredToApp.Length > 0)
        {
          textBlock2.Inlines.Add(new Run
          {
            Text = "Typed into ",
            Foreground = Ui.Brush(messagesPalette.SecondaryText)
          });
          textBlock2.Inlines.Add(new Run
          {
            Text = deliveredToApp,
            Foreground = Ui.Brush(messagesPalette.PrimaryText),
            FontWeight = FontWeights.Bold
          });
        }
        else
        {
          textBlock2.Inlines.Add(new Run
          {
            Text = "Typed it in",
            Foreground = Ui.Brush(messagesPalette.SecondaryText)
          });
        }
      }
      textBlock2.Inlines.Add(new Run
      {
        Text = "  ·  " + ChatBubbles.ShortTime(entry.CreatedAt),
        Foreground = Ui.Brush(messagesPalette.TertiaryText)
      });
      Grid.SetColumn(textBlock2, 1);
      grid.Children.Add(textBlock2);
      base.Children.Add(grid);
      Grid grid2 = new Grid
      {
        ColumnSpacing = 10.0,
        Margin = new Thickness(30.0, 2.0, 8.0, 2.0)
      };
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(3.0)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid2.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid2.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(1.5),
        Background = Ui.Brush(messagesPalette.InkAt(0.28))
      });
      TextBlock textBlock3 = new TextBlock
      {
        Text = delivered,
        FontSize = 13.0,
        FontFamily = DS.Fonts.Text,
        Foreground = Ui.Brush(messagesPalette.PrimaryText.WithAlpha(0.88)),
        TextWrapping = TextWrapping.Wrap,
        MaxLines = 14,
        TextTrimming = TextTrimming.CharacterEllipsis,
        IsTextSelectionEnabled = true,
        IsTextScaleFactorEnabled = false,
        LineStackingStrategy = LineStackingStrategy.BlockLineHeight,
        LineHeight = 20.0
      };
      Grid.SetColumn(textBlock3, 1);
      grid2.Children.Add(textBlock3);
      Pressable copy = new Pressable(true)
      {
        Width = 22.0,
        Height = 22.0,
        Opacity = 0.0,
        VerticalAlignment = VerticalAlignment.Top
      };
      TextBlock copyGlyph = Icons.Glyph("\ue8c8", 10.5, messagesPalette.SecondaryText);
      copyGlyph.FontWeight = FontWeights.SemiBold;
      copy.Children.Add(copyGlyph);
      copy.Tip("Copy");
      AutomationProperties.SetName(copy, "Copy the text");
      bool didCopy = false;
      TypedEventHandler<DispatcherQueueTimer, object> <>9__3;
      copy.Click += delegate
      {
        DataPackage dataPackage = new DataPackage();
        dataPackage.SetText(delivered);
        Clipboard.SetContent(dataPackage);
        didCopy = true;
        copyGlyph.Text = "\ue73e";
        copy.Opacity = 1.0;
        DispatcherQueueTimer dispatcherQueueTimer = <>4__this.DispatcherQueue.CreateTimer().Rooted();
        dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(1.5);
        dispatcherQueueTimer.IsRepeating = false;
        TypedEventHandler<DispatcherQueueTimer, object> typedEventHandler;
        if ((typedEventHandler = <>9__3) == null)
        {
          typedEventHandler = (<>9__3 = delegate(DispatcherQueueTimer _, object _)
          {
            didCopy = false;
            copyGlyph.Text = "\ue8c8";
          });
        }
        dispatcherQueueTimer.Tick += typedEventHandler;
        dispatcherQueueTimer.Start();
      };
      Grid.SetColumn(copy, 2);
      grid2.Children.Add(copy);
      base.Children.Add(grid2);
      base.PointerEntered += delegate(object _, PointerRoutedEventArgs _)
      {
        copy.Opacity = 1.0;
      };
      base.PointerExited += delegate(object _, PointerRoutedEventArgs e)
      {
        if (PointerFrames.IsStale(e))
        {
          return;
        }
        if (!didCopy)
        {
          copy.Opacity = 0.0;
        }
      };
    }
  }
}
