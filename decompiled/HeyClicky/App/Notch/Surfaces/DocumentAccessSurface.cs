using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Interop;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000161 RID: 353
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DocumentAccessSurface : NotchSurface
  {
    // Token: 0x0600136E RID: 4974 RVA: 0x005A1DA4 File Offset: 0x005A1DA4
    public DocumentAccessSurface(NotchLiveEvent.DocumentFolderAccessDenied e)
    {
      DocumentAccessSurface.<>c__DisplayClass1_0 CS$<>8__locals1 = new DocumentAccessSurface.<>c__DisplayClass1_0();
      CS$<>8__locals1.e = e;
      base..ctor();
      CS$<>8__locals1.<>4__this = this;
      TextBlock textBlock = Icons.Glyph("\ue8b7", 14.0, DS.Hex(2450411U, 1.0));
      textBlock.FontWeight = FontWeights.SemiBold;
      TextBlock textBlock2 = Ui.Text("?", 7.0, FontWeights.Black, DS.Hex(2450411U, 1.0), TextAlignment.Center, 0, false, false);
      textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
      textBlock2.VerticalAlignment = VerticalAlignment.Center;
      Grid grid = new Grid
      {
        Width = 8.0,
        Height = 8.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Bottom,
        Margin = new Thickness(0.0, 0.0, -3.0, -1.0),
        Children = 
        {
          new Ellipse
          {
            Fill = Ui.Brush(DS.White(0.94))
          },
          textBlock2
        }
      };
      Grid grid2 = CardParts.Tile(new Grid
      {
        Width = 16.0,
        Height = 14.0,
        Children = { textBlock, grid }
      }, 34.0);
      StackPanel stackPanel = CardParts.TwoLines("HeyClicky can't read this document", CS$<>8__locals1.e.Subtitle, false);
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 11.0
      };
      if (CS$<>8__locals1.e.ShowsDontShowAgain)
      {
        GelActionButton gelActionButton = new GelActionButton("Don't show again", null, GelAppearance.SecondaryWhite, 30.0, 14.0, 0.0, 12.0, 10.0, 5.0, false).Tip("Never show this reminder again");
        gelActionButton.Click += delegate
        {
          Action suppressDocumentAccessNotice = FlowHooks.SuppressDocumentAccessNotice;
          if (suppressDocumentAccessNotice != null)
          {
            suppressDocumentAccessNotice();
          }
          base.<.ctor>g__Dismiss|0();
        };
        stackPanel2.Children.Add(gelActionButton);
      }
      GelActionButton gelActionButton2 = new GelActionButton("Give permission", null, GelAppearance.PrimaryBlue, 30.0, 14.0, 0.0, 12.0, 10.0, 5.0, false).Tip("Open this file's Security settings");
      gelActionButton2.Click += delegate
      {
        string path = CS$<>8__locals1.e.Path;
        if (path == null || !FileSecurityProperties.Show(path))
        {
          CS$<>8__locals1.<>4__this.Host.Actions.OpenUrl("ms-settings:privacy-documents");
        }
        base.<.ctor>g__Dismiss|0();
      };
      stackPanel2.Children.Add(gelActionButton2);
      Pressable pressable = new Pressable(true)
      {
        Width = 24.0,
        Height = 24.0
      };
      pressable.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(DS.White(0.1)),
        Stroke = Ui.Brush(DS.White(0.14)),
        StrokeThickness = 0.5
      });
      TextBlock textBlock3 = Icons.Glyph("\ue711", 8.5, DS.Colors.TextSecondary);
      textBlock3.FontWeight = FontWeights.Bold;
      pressable.Children.Add(textBlock3);
      pressable.WithIconHover(24.0);
      pressable.Click += CS$<>8__locals1.<.ctor>g__Dismiss|0;
      pressable.Tip("Dismiss for now");
      this.view = CardParts.HeadsUpRow(grid2, stackPanel, stackPanel2, pressable);
    }

    // Token: 0x1700038A RID: 906
    // (get) Token: 0x0600136F RID: 4975 RVA: 0x005A2130 File Offset: 0x005A2130
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x04000903 RID: 2307
    private readonly Grid view;
  }
}
