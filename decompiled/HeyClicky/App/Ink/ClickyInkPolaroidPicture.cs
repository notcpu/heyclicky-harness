using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000276 RID: 630
  public static class ClickyInkPolaroidPicture
  {
    // Token: 0x06001ACF RID: 6863 RVA: 0x005E7A38 File Offset: 0x005E7A38
    [NullableContext(2)]
    [return: Nullable(1)]
    public static FrameworkElement Make(string source, string query, double develop, double width, double height)
    {
      ClickyInkPolaroidPicture.<>c__DisplayClass0_0 CS$<>8__locals1 = new ClickyInkPolaroidPicture.<>c__DisplayClass0_0();
      CS$<>8__locals1.develop = develop;
      CS$<>8__locals1.frame = new Grid
      {
        Width = width,
        Height = height,
        Background = Ui.Brush(PaperColor.Hex("#2B2D33")),
        Clip = new RectangleGeometry
        {
          Rect = new Rect(0.0, 0.0, width, height)
        }
      };
      ClickyInkPicture clickyInkPicture = ClickyInkImages.Image(source);
      if (clickyInkPicture != null)
      {
        CS$<>8__locals1.<Make>g__Show|0(clickyInkPicture);
        return CS$<>8__locals1.frame;
      }
      TextBlock textBlock = ClickyInkIcon.Glyph("photo", 20.0, PaperColor.White.Opacity(0.35), false);
      if (textBlock != null)
      {
        CS$<>8__locals1.frame.Children.Add(textBlock);
      }
      if (source != null)
      {
        Task<ClickyInkPicture> task = ClickyInkImages.LoadAsync(source);
        DispatcherQueue dispatcher = DispatcherQueue.GetForCurrentThread();
        task.ContinueWith(delegate([Nullable(new byte[] { 1, 2 })] Task<ClickyInkPicture> t)
        {
          if (t.IsCompletedSuccessfully)
          {
            ClickyInkPicture found = t.Result;
            if (found != null)
            {
              dispatcher.TryEnqueue(delegate
              {
                CS$<>8__locals1.<Make>g__Show|0(found);
              });
            }
          }
        }, TaskScheduler.Default);
      }
      return CS$<>8__locals1.frame;
    }
  }
}
