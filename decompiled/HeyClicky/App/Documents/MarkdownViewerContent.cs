using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CC RID: 716
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MarkdownViewerContent : Grid
  {
    // Token: 0x1700055B RID: 1371
    // (get) Token: 0x06001E1E RID: 7710 RVA: 0x00605E38 File Offset: 0x00605E38
    public string FilePath
    {
      get
      {
        return this.model.FilePath;
      }
    }

    // Token: 0x1700055C RID: 1372
    // (get) Token: 0x06001E1F RID: 7711 RVA: 0x00605E48 File Offset: 0x00605E48
    internal MarkdownViewerTextView TextView
    {
      get
      {
        return this.textView;
      }
    }

    // Token: 0x06001E20 RID: 7712 RVA: 0x00605E50 File Offset: 0x00605E50
    public MarkdownViewerContent(string path)
    {
      this.model = MarkdownViewerDocumentModel.RetainedDocument(path);
      base.RequestedTheme = (this.look.IsDark ? ElementTheme.Dark : ElementTheme.Light);
      this.page.Background = Ui.Brush(this.colors.PageBackground);
      this.textView.WholeDocumentText = () => this.model.MarkdownSource;
      this.page.Children.Add(new ScrollViewer
      {
        Content = this.textView,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Disabled
      });
      this.page.Children.Add(this.CopyWholeDocumentButton());
      this.page.Visibility = Visibility.Collapsed;
      ValueTuple<FrameworkElement, TextBlock, ProgressRing> valueTuple = this.LoadingView();
      this.loading = valueTuple.Item1;
      this.loadingText = valueTuple.Item2;
      this.loadingRing = valueTuple.Item3;
      base.Children.Add(this.page);
      base.Children.Add(this.loading);
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.model.Changed += this.Refresh;
        this.Refresh();
        this.model.ReloadFromDisk();
        if (this.watcher == null)
        {
          this.watcher = new MarkdownViewerFileWatcher(this.model.FilePath, new Action(this.model.ReloadFromDisk));
        }
      };
      base.Unloaded += delegate(object _, RoutedEventArgs _)
      {
        this.model.Changed -= this.Refresh;
        MarkdownViewerFileWatcher markdownViewerFileWatcher = this.watcher;
        if (markdownViewerFileWatcher != null)
        {
          markdownViewerFileWatcher.Dispose();
        }
        this.watcher = null;
        this.model.CancelLoading();
      };
    }

    // Token: 0x06001E21 RID: 7713 RVA: 0x00605FB8 File Offset: 0x00605FB8
    private void Refresh()
    {
      MarkdownViewerPreparedDocument prepared = this.model.Prepared;
      if (prepared != null)
      {
        if (prepared != this.installed)
        {
          this.textView.Install(prepared);
          this.installed = prepared;
        }
        this.page.Visibility = Visibility.Visible;
        this.loading.Visibility = Visibility.Collapsed;
        return;
      }
      this.page.Visibility = Visibility.Collapsed;
      this.loading.Visibility = Visibility.Visible;
      this.loadingText.Text = this.model.LoadingError ?? "Loading document…";
      this.loadingRing.Visibility = ((this.model.LoadingError == null) ? Visibility.Visible : Visibility.Collapsed);
    }

    // Token: 0x06001E22 RID: 7714 RVA: 0x0060605C File Offset: 0x0060605C
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    private ValueTuple<FrameworkElement, TextBlock, ProgressRing> LoadingView()
    {
      ProgressRing progressRing = new ProgressRing
      {
        IsActive = true,
        Width = 16.0,
        Height = 16.0,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      TextBlock textBlock = Ui.Text("Loading document…", 13.0, FontWeights.Normal, this.look.SecondaryText, TextAlignment.Center, 0, false, false);
      return new ValueTuple<FrameworkElement, TextBlock, ProgressRing>(new StackPanel
      {
        Spacing = 12.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        VerticalAlignment = VerticalAlignment.Center,
        Children = { progressRing, textBlock }
      }, textBlock, progressRing);
    }

    // Token: 0x06001E23 RID: 7715 RVA: 0x00606104 File Offset: 0x00606104
    private FrameworkElement CopyWholeDocumentButton()
    {
      TextBlock glyph = Icons.Glyph("\ue8c8", 11.0, this.colors.SecondaryTextColor);
      glyph.FontWeight = FontWeights.Medium;
      TextBlock copied = Ui.Text("Copied", 11.0, FontWeights.Medium, this.colors.SecondaryTextColor, TextAlignment.Left, 0, false, false);
      copied.Visibility = Visibility.Collapsed;
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 5.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      stackPanel.Children.Add(glyph);
      stackPanel.Children.Add(copied);
      Pressable button = new Pressable(true)
      {
        Padding = new Thickness(8.0, 6.0, 8.0, 6.0),
        Background = Ui.Brush(this.colors.RaisedChipBackgroundColor),
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, 18.0, 20.0, 0.0)
      };
      button.Children.Add(stackPanel);
      button.SizeChanged += delegate(object _, SizeChangedEventArgs e)
      {
        button.CornerRadius = new CornerRadius(e.NewSize.Height / 2.0);
      };
      AutomationProperties.SetName(button, "Copy markdown");
      DispatcherTimer reset = null;
      EventHandler<object> <>9__2;
      button.Click += delegate
      {
        DataPackage dataPackage = new DataPackage();
        dataPackage.SetText(this.model.MarkdownSource);
        Clipboard.SetContent(dataPackage);
        glyph.Text = "\ue73e";
        copied.Visibility = Visibility.Visible;
        DispatcherTimer reset4 = reset;
        if (reset4 != null)
        {
          reset4.Stop();
        }
        reset = new DispatcherTimer
        {
          Interval = TimeSpan.FromSeconds(1.4)
        };
        DispatcherTimer reset2 = reset;
        EventHandler<object> eventHandler;
        if ((eventHandler = <>9__2) == null)
        {
          eventHandler = (<>9__2 = delegate([Nullable(2)] object _, object _)
          {
            DispatcherTimer reset3 = reset;
            if (reset3 != null)
            {
              reset3.Stop();
            }
            glyph.Text = "\ue8c8";
            copied.Visibility = Visibility.Collapsed;
          });
        }
        reset2.Tick += eventHandler;
        reset.Start();
      };
      return button.Tip("Copy markdown");
    }

    // Token: 0x04000E80 RID: 3712
    private readonly MarkdownViewerDocumentModel model;

    // Token: 0x04000E81 RID: 3713
    private readonly MessagesPalette look = MessagesPalette.Current;

    // Token: 0x04000E82 RID: 3714
    private readonly MarkdownViewerPalette colors = MarkdownViewerPalette.Current;

    // Token: 0x04000E83 RID: 3715
    private readonly Grid page = new Grid();

    // Token: 0x04000E84 RID: 3716
    private readonly MarkdownViewerTextView textView = new MarkdownViewerTextView(true, 1.0);

    // Token: 0x04000E85 RID: 3717
    private readonly FrameworkElement loading;

    // Token: 0x04000E86 RID: 3718
    private readonly TextBlock loadingText;

    // Token: 0x04000E87 RID: 3719
    private readonly ProgressRing loadingRing;

    // Token: 0x04000E88 RID: 3720
    [Nullable(2)]
    private MarkdownViewerFileWatcher watcher;

    // Token: 0x04000E89 RID: 3721
    [Nullable(2)]
    private MarkdownViewerPreparedDocument installed;
  }
}
