using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Drive;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000327 RID: 807
  public sealed class HandoffChipView : Grid
  {
    // Token: 0x06002297 RID: 8855 RVA: 0x00626484 File Offset: 0x00626484
    [NullableContext(1)]
    public HandoffChipView(HandoffGlyph glyph, string railKey, double size = 32.0, PaperStock? stock = null, bool showsTape = true)
    {
      HandoffChipView <>4__this = this;
      base.Width = (base.Height = size);
      base.IsHitTestVisible = false;
      ArcPaper paper = new ArcPaper(stock ?? NotePaperColors.Stock(NotePaperColors.Key(railKey)));
      int seed = NotePaperColors.Seed(railKey);
      double frame = size * 1.5;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        if (<>4__this.Children.Count > 0)
        {
          return;
        }
        XamlRoot xamlRoot = <>4__this.XamlRoot;
        double num = ((xamlRoot != null) ? xamlRoot.RasterizationScale : 1.0);
        string text = HandoffGlyphs.Path(glyph);
        Color titleWash = paper.TitleWash;
        Color deep = paper.Deep;
        double size2 = size;
        double frame2 = frame;
        double num2 = num * 2.0;
        int seed2 = seed;
        bool showsTape2 = showsTape;
        WriteableBitmap writeableBitmap = DriveArt.HandoffChip(text, titleWash, deep, size2, frame2, num2, seed2, null, showsTape2);
        if (writeableBitmap != null)
        {
          <>4__this.Children.Add(new Image
          {
            Source = writeableBitmap,
            Width = frame,
            Height = frame,
            Margin = new Thickness(-(frame - size) / 2.0),
            Stretch = Stretch.Fill
          });
        }
      };
    }
  }
}
