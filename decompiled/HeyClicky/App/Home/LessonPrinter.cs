using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B9 RID: 697
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LessonPrinter
  {
    // Token: 0x06001CCE RID: 7374 RVA: 0x005FC008 File Offset: 0x005FC008
    public LessonPrinter(bool excludeFromCapture)
    {
      this.window = new OverlayWindow("HeyClicky print", excludeFromCapture, true, false, true, false, false);
      this.dq = DispatcherQueue.GetForCurrentThread();
    }

    // Token: 0x06001CCF RID: 7375 RVA: 0x005FC034 File Offset: 0x005FC034
    public void Print([Nullable(2)] byte[] poster, string title, DisplayInfo display, [TupleElementNames(new string[] { "X", "Y" })] [Nullable(0)] ValueTuple<int, int>? notch)
    {
      double s = display.Scale;
      double vw = display.BoundsPx.Width / s;
      double vh = display.BoundsPx.Height / s;
      this.window.PlacePx((int)display.BoundsPx.X, (int)display.BoundsPx.Y, (int)display.BoundsPx.Width, (int)display.BoundsPx.Height);
      this.window.Root.Children.Clear();
      Image image = new Image
      {
        Width = 360.0,
        Height = 360.0 * vh / vw,
        Stretch = Stretch.UniformToFill
      };
      if (poster != null)
      {
        BitmapImage bitmapImage = new BitmapImage();
        InMemoryRandomAccessStream inMemoryRandomAccessStream = new InMemoryRandomAccessStream();
        LessonPrinter.LoadAsync(bitmapImage, inMemoryRandomAccessStream, poster);
        image.Source = bitmapImage;
      }
      TextBlock textBlock = Ui.Text(title, 15.0, FontWeights.SemiBold, DS.Black(0.82), TextAlignment.Center, 2, true, false);
      textBlock.FontFamily = new FontFamily("Segoe Print, Segoe UI Variable Text");
      Border border = new Border
      {
        Background = Ui.Brush(DS.Hex(16513782U, 1.0)),
        Padding = new Thickness(14.0, 14.0, 14.0, 10.0),
        CornerRadius = new CornerRadius(4.0),
        Child = new StackPanel
        {
          Spacing = 10.0,
          Children = 
          {
            new Border
            {
              Child = image,
              CornerRadius = new CornerRadius(2.0)
            },
            textBlock
          }
        },
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center
      };
      Grid host = new Grid
      {
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = 
        {
          new ShadowLayer(4.0, DS.Black(0.35), 18.0, 10.0, 0.0),
          border
        }
      };
      this.window.Root.Children.Add(host);
      this.window.ShowNoActivate();
      Sounds.Play("clicky-text-send", 0.4);
      Action <>9__2;
      Action <>9__1;
      this.After(0.06, delegate
      {
        host.UpdateLayout();
        Vector2 actualSize = host.ActualSize;
        host.CenterPoint = new Vector3(actualSize.X / 2f, actualSize.Y / 2f, 0f);
        host.Opacity = 0.0;
        host.Scale = new Vector3(0.85f, 0.85f, 1f);
        host.Translation = new Vector3(0f, 60f, 0f);
        host.Rotation = -4f;
        Anim.Opacity(host, 1f, 0.25, null, 0.0);
        Anim.Scale(host, 1f, 0.45, null, new Vector3?(host.CenterPoint));
        Anim.Translate(host, Vector3.Zero, 0.45, null);
        Anim.Rotate(host, 2f, 0.45, null);
        LessonPrinter <>4__this = this;
        double num = 1.4;
        Action action;
        if ((action = <>9__1) == null)
        {
          action = (<>9__1 = delegate
          {
            double num2 = -(vh / 2.0) + 8.0;
            double num3;
            if (notch != null)
            {
              ValueTuple<int, int> valueOrDefault = notch.GetValueOrDefault();
              num3 = ((double)valueOrDefault.Item1 - display.BoundsPx.X) / s - vw / 2.0;
            }
            else
            {
              num3 = 0.0;
            }
            double num4 = num3;
            Anim.Translate(host, new Vector3((float)num4, (float)num2, 0f), 0.55, null);
            Anim.Scale(host, 0.06f, 0.55, null, new Vector3?(host.CenterPoint));
            Anim.Opacity(host, 0f, 0.55, null, 0.25);
            LessonPrinter <>4__this2 = this;
            double num5 = 0.75;
            Action action2;
            if ((action2 = <>9__2) == null)
            {
              action2 = (<>9__2 = delegate
              {
                this.window.HideWindow();
                this.window.Root.Children.Clear();
              });
            }
            <>4__this2.After(num5, action2);
          });
        }
        <>4__this.After(num, action);
      });
      Log.Info("lessons", "printing \"" + title + "\"");
    }

    // Token: 0x06001CD0 RID: 7376 RVA: 0x005FC354 File Offset: 0x005FC354
    private static async Task LoadAsync(BitmapImage bmp, InMemoryRandomAccessStream ms, byte[] png)
    {
      await ms.WriteAsync(png.AsBuffer());
      ms.Seek(0UL);
      await bmp.SetSourceAsync(ms);
    }

    // Token: 0x06001CD1 RID: 7377 RVA: 0x005FC3A8 File Offset: 0x005FC3A8
    private void After(double seconds, Action a)
    {
      DispatcherQueueTimer dispatcherQueueTimer = this.dq.CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(seconds);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        a();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x17000501 RID: 1281
    // (get) Token: 0x06001CD2 RID: 7378 RVA: 0x005FC3FC File Offset: 0x005FC3FC
    public IntPtr Hwnd
    {
      get
      {
        return this.window.Hwnd;
      }
    }

    // Token: 0x04000DCE RID: 3534
    private readonly OverlayWindow window;

    // Token: 0x04000DCF RID: 3535
    private readonly DispatcherQueue dq;
  }
}
