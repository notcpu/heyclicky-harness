using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Shell;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Web.WebView2.Core;
using Windows.Storage.Streams;

namespace HeyClicky.App.Home
{
  // Token: 0x020002BB RID: 699
  [NullableContext(1)]
  [Nullable(0)]
  public static class WebPageSnapshotter
  {
    // Token: 0x06001D1D RID: 7453 RVA: 0x005FD920 File Offset: 0x005FD920
    public static bool IsWebPageFile(string path)
    {
      string text = Path.GetExtension(path).ToLowerInvariant();
      return text == ".html" || text == ".htm" || text == ".xhtml";
    }

    // Token: 0x06001D1E RID: 7454 RVA: 0x005FD968 File Offset: 0x005FD968
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<ImageSource> SnapshotAsync(string path)
    {
      await WebPageSnapshotter.Turn.WaitAsync();
      ImageSource imageSource;
      try
      {
        Task<bool> task;
        if ((task = WebPageSnapshotter.ready) == null)
        {
          task = (WebPageSnapshotter.ready = WebPageSnapshotter.CreateAsync());
        }
        TaskAwaiter<bool> taskAwaiter = task.GetAwaiter();
        if (!taskAwaiter.IsCompleted)
        {
          await taskAwaiter;
          TaskAwaiter<bool> taskAwaiter2;
          taskAwaiter = taskAwaiter2;
          taskAwaiter2 = default(TaskAwaiter<bool>);
        }
        if (taskAwaiter.GetResult())
        {
          WebView2 webView = WebPageSnapshotter.view;
          CoreWebView2 core = ((webView != null) ? webView.CoreWebView2 : null);
          if (core != null)
          {
            WebPageSnapshotter.navigation = new TaskCompletionSource<bool>();
            Task<bool> loading = WebPageSnapshotter.navigation.Task;
            core.Navigate(new Uri(Path.GetFullPath(path)).AbsoluteUri);
            TaskAwaiter<Task> taskAwaiter3 = Task.WhenAny(loading, Task.Delay(WebPageSnapshotter.Timeout)).GetAwaiter();
            if (!taskAwaiter3.IsCompleted)
            {
              await taskAwaiter3;
              TaskAwaiter<Task> taskAwaiter4;
              taskAwaiter3 = taskAwaiter4;
              taskAwaiter4 = default(TaskAwaiter<Task>);
            }
            bool flag = taskAwaiter3.GetResult() == loading && loading.Result;
            WebPageSnapshotter.navigation = null;
            if (!flag)
            {
              core.Stop();
              core.Navigate("about:blank");
              Log.Info("home", "page preview: " + Path.GetFileName(path) + " didn't load in time");
              return null;
            }
            await Task.Delay(WebPageSnapshotter.SettleDelay);
            using (InMemoryRandomAccessStream png = new InMemoryRandomAccessStream())
            {
              await core.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, png);
              core.Navigate("about:blank");
              png.Seek(0UL);
              BitmapImage bitmap = new BitmapImage
              {
                DecodePixelWidth = 720
              };
              await bitmap.SetSourceAsync(png);
              return bitmap;
            }
          }
        }
        imageSource = null;
      }
      catch (Exception ex)
      {
        Log.Info("home", "page preview of " + Path.GetFileName(path) + " failed: " + ex.Message);
        imageSource = null;
      }
      finally
      {
        WebPageSnapshotter.Turn.Release();
      }
      return imageSource;
    }

    // Token: 0x06001D1F RID: 7455 RVA: 0x005FD9AC File Offset: 0x005FD9AC
    private static async Task<bool> CreateAsync()
    {
      bool flag;
      try
      {
        WebPageSnapshotter.window = new OverlayWindow("HeyClicky page preview", true, false, false, false, false, false);
        WebPageSnapshotter.view = new WebView2
        {
          Width = 1280.0,
          Height = 800.0,
          HorizontalAlignment = HorizontalAlignment.Left,
          VerticalAlignment = VerticalAlignment.Top
        };
        WebPageSnapshotter.window.Root.Children.Add(WebPageSnapshotter.view);
        double scale = Displays.Primary().Scale;
        int num = (int)Math.Ceiling(1280.0 * scale);
        int num2 = (int)Math.Ceiling(800.0 * scale);
        WebPageSnapshotter.window.PlacePx(WebPageSnapshotter.GetSystemMetrics(76) - num - 400, WebPageSnapshotter.GetSystemMetrics(77), num, num2);
        WebPageSnapshotter.window.ShowNoActivate();
        CoreWebView2EnvironmentOptions coreWebView2EnvironmentOptions = new CoreWebView2EnvironmentOptions
        {
          AdditionalBrowserArguments = "--disable-features=CalculateNativeWinOcclusion --autoplay-policy=user-gesture-required"
        };
        CoreWebView2Environment coreWebView2Environment = await CoreWebView2Environment.CreateWithOptionsAsync(null, AppPaths.Combine(new string[] { "WebView2", "PagePreviews" }), coreWebView2EnvironmentOptions);
        CoreWebView2ControllerOptions coreWebView2ControllerOptions = coreWebView2Environment.CreateCoreWebView2ControllerOptions();
        coreWebView2ControllerOptions.IsInPrivateModeEnabled = true;
        await WebPageSnapshotter.view.EnsureCoreWebView2Async(coreWebView2Environment, coreWebView2ControllerOptions);
        CoreWebView2 core = WebPageSnapshotter.view.CoreWebView2;
        core.IsMuted = true;
        core.Settings.AreDefaultContextMenusEnabled = false;
        core.Settings.AreDevToolsEnabled = false;
        core.Settings.IsStatusBarEnabled = false;
        core.NavigationStarting += delegate(CoreWebView2 _, CoreWebView2NavigationStartingEventArgs e)
        {
          if (!e.Uri.StartsWith("file:", StringComparison.OrdinalIgnoreCase) && !e.Uri.StartsWith("about:", StringComparison.OrdinalIgnoreCase))
          {
            e.Cancel = true;
          }
        };
        core.NewWindowRequested += delegate(CoreWebView2 _, CoreWebView2NewWindowRequestedEventArgs e)
        {
          e.Handled = true;
        };
        core.NavigationCompleted += delegate(CoreWebView2 _, CoreWebView2NavigationCompletedEventArgs e)
        {
          if (core.Source.StartsWith("about:", StringComparison.OrdinalIgnoreCase))
          {
            return;
          }
          TaskCompletionSource<bool> taskCompletionSource = WebPageSnapshotter.navigation;
          if (taskCompletionSource == null)
          {
            return;
          }
          taskCompletionSource.TrySetResult(e.IsSuccess);
        };
        flag = true;
      }
      catch (Exception ex)
      {
        Log.Warn("home", "page previews unavailable (WebView2): " + ex.Message);
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001D20 RID: 7456
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int index);

    // Token: 0x04000DF0 RID: 3568
    private const double ViewportWidth = 1280.0;

    // Token: 0x04000DF1 RID: 3569
    private const double ViewportHeight = 800.0;

    // Token: 0x04000DF2 RID: 3570
    private const int SnapshotPixelWidth = 720;

    // Token: 0x04000DF3 RID: 3571
    private static readonly TimeSpan SettleDelay = TimeSpan.FromSeconds(0.6);

    // Token: 0x04000DF4 RID: 3572
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(8L);

    // Token: 0x04000DF5 RID: 3573
    [Nullable(2)]
    private static OverlayWindow window;

    // Token: 0x04000DF6 RID: 3574
    [Nullable(2)]
    private static WebView2 view;

    // Token: 0x04000DF7 RID: 3575
    [Nullable(2)]
    private static Task<bool> ready;

    // Token: 0x04000DF8 RID: 3576
    private static readonly SemaphoreSlim Turn = new SemaphoreSlim(1, 1);

    // Token: 0x04000DF9 RID: 3577
    [Nullable(2)]
    private static TaskCompletionSource<bool> navigation;
  }
}
