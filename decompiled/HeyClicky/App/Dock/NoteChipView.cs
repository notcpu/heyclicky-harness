using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Drive;
using HeyClicky.App.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F2 RID: 754
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class NoteChipView : Grid
  {
    // Token: 0x0600205A RID: 8282 RVA: 0x00616A8C File Offset: 0x00616A8C
    public NoteChipView(string paperKey, string symbol, int chipSeed, int tapeSeed, double size, bool showsTape = true)
    {
      NoteChipView <>4__this = this;
      base.Width = (base.Height = size);
      base.IsHitTestVisible = false;
      base.VerticalAlignment = VerticalAlignment.Top;
      ArcPaper paper = new ArcPaper(NotePaperColors.Stock(paperKey));
      double frame = size * 1.5;
      double num = (double)(tapeSeed % 7 - 3);
      TextBlock textBlock = ClickyInkIcon.Glyph(symbol, 11.0 * size / 24.0, paper.Deep, false);
      FrameworkElement frameworkElement;
      if (textBlock != null)
      {
        textBlock.FontWeight = FontWeights.SemiBold;
        frameworkElement = textBlock;
      }
      else
      {
        frameworkElement = ArcSymbols.Make(symbol, 11.0 * size / 24.0, paper.Deep, false);
      }
      frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      frameworkElement.RenderTransformOrigin = new Point(0.5, 0.5);
      frameworkElement.RenderTransform = new RotateTransform
      {
        Angle = num
      };
      bool painted = false;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        if (painted)
        {
          return;
        }
        painted = true;
        XamlRoot xamlRoot = <>4__this.XamlRoot;
        double num2 = ((xamlRoot != null) ? xamlRoot.RasterizationScale : 1.0);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 7);
        defaultInterpolatedStringHandler.AppendFormatted<Color>(paper.TitleWash);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<Color>(paper.Deep);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<double>(size);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<double>(num2);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<int>(chipSeed);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<int>(tapeSeed);
        defaultInterpolatedStringHandler.AppendLiteral("|");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(showsTape);
        string text = defaultInterpolatedStringHandler.ToStringAndClear();
        WriteableBitmap writeableBitmap;
        if (!NoteChipView.Pictures.TryGetValue(text, out writeableBitmap))
        {
          writeableBitmap = DriveArt.HandoffChip(null, paper.TitleWash, paper.Deep, size, frame, num2 * 2.0, chipSeed, new int?(tapeSeed), showsTape);
          if (writeableBitmap != null)
          {
            NoteChipView.Pictures[text] = writeableBitmap;
          }
        }
        if (writeableBitmap != null)
        {
          <>4__this.Children.Insert(0, new Image
          {
            Source = writeableBitmap,
            Width = frame,
            Height = frame,
            Margin = new Thickness(-(frame - size) / 2.0),
            Stretch = Stretch.Fill
          });
        }
      };
      base.Children.Add(frameworkElement);
    }

    // Token: 0x04000FAE RID: 4014
    private static readonly Dictionary<string, WriteableBitmap> Pictures = new Dictionary<string, WriteableBitmap>();
  }
}
