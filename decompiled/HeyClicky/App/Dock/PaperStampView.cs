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
  // Token: 0x02000328 RID: 808
  public sealed class PaperStampView : Grid
  {
    // Token: 0x06002298 RID: 8856 RVA: 0x00626538 File Offset: 0x00626538
    [NullableContext(1)]
    public PaperStampView(string word, Color color, double fontSize, double degrees)
    {
      PaperStampView <>4__this = this;
      base.IsHitTestVisible = false;
      base.VerticalAlignment = VerticalAlignment.Center;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        XamlRoot xamlRoot = <>4__this.XamlRoot;
        double num = ((xamlRoot != null) ? xamlRoot.RasterizationScale : 1.0);
        ValueTuple<WriteableBitmap, double, double> valueTuple = DriveArt.Stamp(word.ToUpperInvariant(), color, fontSize, num * 2.0);
        WriteableBitmap item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        double item3 = valueTuple.Item3;
        if (item == null)
        {
          return;
        }
        <>4__this.Width = item2;
        <>4__this.Height = item3;
        <>4__this.Children.Add(new Image
        {
          Source = item,
          Width = item2,
          Height = item3,
          Stretch = Stretch.Fill
        });
        <>4__this.RenderTransform = new RotateTransform
        {
          Angle = degrees,
          CenterX = item2 / 2.0,
          CenterY = item3 / 2.0
        };
      };
    }
  }
}
