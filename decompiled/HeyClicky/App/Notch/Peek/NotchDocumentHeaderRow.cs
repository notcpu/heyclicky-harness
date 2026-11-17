using System;
using System.IO;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.Services;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x02000180 RID: 384
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchDocumentHeaderRow : Grid
  {
    // Token: 0x060014E9 RID: 5353 RVA: 0x005B2A84 File Offset: 0x005B2A84
    public NotchDocumentHeaderRow(INotchHost host)
    {
      NotchDocumentHeaderRow.<>c__DisplayClass0_0 CS$<>8__locals1 = new NotchDocumentHeaderRow.<>c__DisplayClass0_0();
      CS$<>8__locals1.host = host;
      base..ctor();
      base.ColumnSpacing = 8.0;
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      base.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      NotchDocumentHeaderRow.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
      NotchPresenter notchPresenter = CS$<>8__locals1.host.Services.Presenter as NotchPresenter;
      CS$<>8__locals2.tabs = ((notchPresenter != null) ? notchPresenter.Tabs : null);
      NotchDocumentHeaderRow.<>c__DisplayClass0_0 CS$<>8__locals3 = CS$<>8__locals1;
      NotchTabs tabs = CS$<>8__locals1.tabs;
      CS$<>8__locals3.path = ((tabs != null) ? tabs.DocumentPath : null);
      if (CS$<>8__locals1.path == null)
      {
        return;
      }
      NotchDocumentStyle notchDocumentStyle = NotchDocumentStyle.For(CS$<>8__locals1.path, false);
      Border border = new Border
      {
        Width = 18.0,
        Height = 18.0,
        CornerRadius = new CornerRadius(5.0),
        Background = Ui.Brush(notchDocumentStyle.Tint),
        VerticalAlignment = VerticalAlignment.Center
      };
      TextBlock textBlock = Icons.Glyph(notchDocumentStyle.Glyph, 9.5, Colors.White);
      textBlock.FontWeight = FontWeights.SemiBold;
      border.Child = textBlock;
      TextBlock textBlock2 = Ui.Text(Path.GetFileName(CS$<>8__locals1.path), 12.5, FontWeights.SemiBold, HomeTheme.PrimaryText, TextAlignment.Left, 1, false, false);
      textBlock2.TextTrimming = TextTrimming.CharacterEllipsis;
      TextBlock textBlock3 = Ui.Text(notchDocumentStyle.KindLabel, 11.5, FontWeights.Normal, HomeTheme.SecondaryText, TextAlignment.Left, 1, false, false);
      Grid grid = new Grid
      {
        ColumnSpacing = 7.0,
        MaxWidth = 270.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      Grid.SetColumn(textBlock2, 1);
      Grid.SetColumn(textBlock3, 2);
      grid.Children.Add(border);
      grid.Children.Add(textBlock2);
      grid.Children.Add(textBlock3);
      grid.Tip(CS$<>8__locals1.path);
      base.Children.Add(grid);
      StackPanel stackPanel = Ui.H(0.0, new UIElement[]
      {
        NotchDocumentHeaderRow.HeaderButton("\ue8a7", "Open in its app", delegate
        {
          NotchPeekSurface.PeekAction("document_open_in_app", null);
          INotchActions notchActions = CS$<>8__locals1.host.Actions as INotchActions;
          if (notchActions == null)
          {
            return;
          }
          notchActions.OpenInItsApp(CS$<>8__locals1.path, CS$<>8__locals1.tabs.DocumentThreadId);
        }),
        NotchDocumentHeaderRow.HeaderButton("\ue8b7", "Show in Explorer", delegate
        {
          INotchActions notchActions2 = CS$<>8__locals1.host.Actions as INotchActions;
          if (notchActions2 == null)
          {
            return;
          }
          notchActions2.RevealInExplorer(CS$<>8__locals1.path);
        }),
        NotchDocumentHeaderRow.HeaderButton("\ue711", "Close (esc)", delegate
        {
          CS$<>8__locals1.tabs.CloseDocument();
        })
      });
      stackPanel.VerticalAlignment = VerticalAlignment.Center;
      Grid.SetColumn(stackPanel, 2);
      base.Children.Add(stackPanel);
    }

    // Token: 0x060014EA RID: 5354 RVA: 0x005B2DA0 File Offset: 0x005B2DA0
    private static Pressable HeaderButton(string glyph, string tip, Action action)
    {
      Pressable p = new Pressable(true)
      {
        Width = 28.0,
        Height = 28.0,
        CornerRadius = new CornerRadius(7.0)
      };
      TextBlock icon = Icons.Glyph(glyph, 12.5, HomeTheme.SecondaryText);
      icon.FontWeight = FontWeights.Medium;
      p.Children.Add(icon);
      p.HoverChanged += delegate(bool h)
      {
        icon.Foreground = Ui.Brush(h ? HomeTheme.PrimaryText : HomeTheme.SecondaryText);
        p.Background = Ui.Brush(DS.White(h ? 0.08 : 0.0));
      };
      p.Click += action;
      p.Tip(tip);
      AutomationProperties.SetName(p, tip);
      return p;
    }
  }
}
