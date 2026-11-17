using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Telemetry;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x02000168 RID: 360
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TextResponseSurface : NotchSurface
  {
    // Token: 0x060013CD RID: 5069 RVA: 0x005A4E98 File Offset: 0x005A4E98
    public TextResponseSurface(NotchPresentation.TextResponse r)
    {
      this.question = r.Question;
      this.thinkingRow = Ui.Row(8.0, new UIElement[] { Ui.Semibold("Thinking", 11.0, DS.Colors.TextPrimary) }, new UIElement[] { ActivityIndicators.ThinkingDots(DS.Colors.ThinkingAccent, 4.0, 3.0, 2.8, 0.85, 0.55, 0.45, 0.4, 0.6, 14.0) }, 6.0);
      this.thinkingRow.Margin = new Thickness(14.0, 0.0, 14.0, 0.0);
      this.thinkingRow.Height = 18.0;
      this.thinkingRow.VerticalAlignment = VerticalAlignment.Top;
      this.copyGlyph = Icons.Glyph("\ue8c8", 10.0, DS.White(0.5));
      this.copyGlyph.FontWeight = FontWeights.SemiBold;
      this.copyGlyph.HorizontalAlignment = HorizontalAlignment.Left;
      this.copyGlyph.VerticalAlignment = VerticalAlignment.Top;
      this.copy = new Pressable(true)
      {
        Width = 16.0,
        Height = 16.0,
        HorizontalAlignment = HorizontalAlignment.Left,
        Margin = new Thickness(0.0, 0.0, 0.0, 8.0)
      };
      this.copy.Children.Add(this.copyGlyph);
      this.copy.HoverChanged += delegate(bool h)
      {
        if (this.copyGlyph.Text == "\ue8c8")
        {
          this.copyGlyph.Foreground = Ui.Brush(DS.White(h ? 0.9 : 0.5));
        }
      };
      this.copy.Click += this.CopyAnswer;
      this.copy.Tip("Copy response");
      this.gallery = new ScrollViewer
      {
        Content = this.galleryRow,
        Height = 88.0,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
        VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
        HorizontalScrollMode = ScrollMode.Enabled,
        VerticalScrollMode = ScrollMode.Disabled,
        Visibility = Visibility.Collapsed
      };
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 8.0,
        Children = { this.markdownSlot, this.copy }
      };
      this.prose.Children.Add(this.gallery);
      this.prose.Children.Add(stackPanel);
      this.scroll = new ScrollViewer
      {
        Content = new Border
        {
          Child = this.prose,
          Padding = new Thickness(14.0, 34.0, 14.0, 12.0)
        },
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
      };
      Grid grid = new Grid();
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = new GridLength(1.0, GridUnitType.Star)
      });
      grid.RowDefinitions.Add(new RowDefinition
      {
        Height = GridLength.Auto
      });
      grid.Children.Add(this.scroll);
      this.answerArea.Children.Add(grid);
      this.close = new Pressable(true)
      {
        Width = 24.0,
        Height = 24.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(0.0, -2.0, 8.0, 0.0)
      };
      this.close.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(DS.White(0.1)),
        Stroke = Ui.Brush(DS.White(0.08)),
        StrokeThickness = 0.5
      });
      TextBlock textBlock = Icons.Glyph("\ue711", 9.0, DS.White(0.75));
      textBlock.FontWeight = FontWeights.Bold;
      this.close.Children.Add(textBlock);
      this.close.Click += delegate
      {
        base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
      };
      this.close.Tip("Close");
      this.BuildCountdownChrome();
      this.view.Children.Add(this.thinkingRow);
      this.view.Children.Add(this.answerArea);
      this.view.Children.Add(this.close);
      this.view.Children.Add(this.countdownChrome);
      this.Apply(r);
    }

    // Token: 0x060013CE RID: 5070 RVA: 0x005A549C File Offset: 0x005A549C
    private void BuildCountdownChrome()
    {
      this.clock = new Pressable(true)
      {
        Width = 24.0,
        Height = 24.0,
        HorizontalAlignment = HorizontalAlignment.Right
      };
      this.clock.Children.Add(new Ellipse
      {
        Fill = Ui.Brush(DS.White(0.1))
      });
      this.clock.Children.Add(new Ellipse
      {
        Width = 26.0,
        Height = 26.0,
        Margin = new Thickness(-1.0),
        Stroke = Ui.Brush(DS.White(0.08)),
        StrokeThickness = 2.0
      });
      this.clockRing = new Ellipse
      {
        Width = 26.0,
        Height = 26.0,
        Margin = new Thickness(-1.0),
        Stroke = Ui.Brush(DS.Colors.Blue300.WithAlpha(0.95)),
        StrokeThickness = 2.0,
        StrokeDashCap = PenLineCap.Round,
        StrokeStartLineCap = PenLineCap.Round,
        StrokeEndLineCap = PenLineCap.Round,
        RenderTransformOrigin = new Point(0.5, 0.5),
        RenderTransform = new RotateTransform
        {
          Angle = -90.0
        }
      };
      this.clock.Children.Add(this.clockRing);
      TextBlock textBlock = Icons.Glyph("\ue823", 9.0, DS.White(0.78));
      textBlock.FontWeight = FontWeights.Bold;
      this.clock.Children.Add(textBlock);
      this.clock.Click += this.Dismiss;
      this.clock.Tip("Dismiss now");
      GelActionButton gelActionButton = TextResponseSurface.Pill("\ue769", "Pause", "Pause auto-dismiss");
      gelActionButton.Click += delegate
      {
        NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
        if (notchPresenter == null)
        {
          return;
        }
        notchPresenter.PauseTextResponseAutoDismiss();
      };
      GelActionButton gelActionButton2 = TextResponseSurface.Pill("\ue70e", "Dismiss", "Dismiss");
      gelActionButton2.Click += this.Dismiss;
      this.hoverPills = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 6.0,
        HorizontalAlignment = HorizontalAlignment.Right,
        Children = { gelActionButton, gelActionButton2 }
      };
      this.pausedDismiss = TextResponseSurface.Pill("\ue70e", "Dismiss", "Dismiss");
      this.pausedDismiss.HorizontalAlignment = HorizontalAlignment.Right;
      this.pausedDismiss.Click += this.Dismiss;
      this.countdownChrome.Children.Add(this.clock);
      this.countdownChrome.Children.Add(this.hoverPills);
      this.countdownChrome.Children.Add(this.pausedDismiss);
      this.countdownChrome.Visibility = Visibility.Collapsed;
    }

    // Token: 0x060013CF RID: 5071 RVA: 0x005A57C4 File Offset: 0x005A57C4
    private static GelActionButton Pill(string glyph, string title, string tip)
    {
      GelActionButton gelActionButton = new GelActionButton(title, glyph, GelAppearance.SecondaryWhite, 24.0, 8.0, 0.0, 11.0, 9.0, 4.0, false);
      gelActionButton.VerticalAlignment = VerticalAlignment.Top;
      gelActionButton.Tip(tip);
      return gelActionButton;
    }

    // Token: 0x060013D0 RID: 5072 RVA: 0x005A5820 File Offset: 0x005A5820
    private void Dismiss()
    {
      base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
    }

    // Token: 0x060013D1 RID: 5073 RVA: 0x005A583C File Offset: 0x005A583C
    public override bool AcceptFiles(IReadOnlyList<string> paths)
    {
      if (this.followUp != null)
      {
        TextResponseSurface.AttachDropped(this.followUp, paths);
      }
      else
      {
        this.pendingDrop.AddRange(paths);
      }
      return true;
    }

    // Token: 0x060013D2 RID: 5074 RVA: 0x005A5864 File Offset: 0x005A5864
    private static void AttachDropped(ComposerBox box, IReadOnlyList<string> paths)
    {
      if (paths.Count == 0)
      {
        return;
      }
      int num = box.AddAttachments(paths);
      if (num > 0)
      {
        ClickyAnalytics.TrackNotchDroppedFilesAttached(num);
      }
    }

    // Token: 0x060013D3 RID: 5075 RVA: 0x005A588C File Offset: 0x005A588C
    public override void SetDropTargeting(bool on)
    {
      ComposerBox composerBox = this.followUp;
      if (composerBox == null)
      {
        return;
      }
      composerBox.SetDropTargeting(on, DS.CursorColor(base.Host.Services.Settings.CursorColorHex));
    }

    // Token: 0x060013D4 RID: 5076 RVA: 0x005A58BC File Offset: 0x005A58BC
    private void UpdateChrome()
    {
      if (base.Host == null)
      {
        return;
      }
      NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
      ValueTuple<TimeSpan, TimeSpan, bool>? valueTuple = ((notchPresenter != null) ? notchPresenter.TextResponseCountdown : null);
      bool flag = this.finished && valueTuple != null;
      string text = (this.thinking ? "none" : ((!flag) ? "close" : (valueTuple.Value.Item3 ? "paused" : (base.Host.IsHovering ? "hover" : "clock"))));
      if (text != this.chromeMode)
      {
        this.chromeMode = text;
        this.close.Visibility = ((text == "close") ? Visibility.Visible : Visibility.Collapsed);
        Grid grid = this.countdownChrome;
        bool flag2 = text == "clock" || text == "hover" || text == "paused";
        grid.Visibility = (flag2 ? Visibility.Visible : Visibility.Collapsed);
        this.clock.Visibility = ((text == "clock") ? Visibility.Visible : Visibility.Collapsed);
        this.hoverPills.Visibility = ((text == "hover") ? Visibility.Visible : Visibility.Collapsed);
        this.pausedDismiss.Visibility = ((text == "paused") ? Visibility.Visible : Visibility.Collapsed);
        flag2 = text == "clock" || text == "hover" || text == "paused";
        if (flag2 && !Anim.Instant)
        {
          this.countdownChrome.Opacity = 0.0;
          Anim.Opacity(this.countdownChrome, 1f, 0.18, null, 0.0);
        }
      }
      if (flag && valueTuple != null)
      {
        ValueTuple<TimeSpan, TimeSpan, bool> valueOrDefault = valueTuple.GetValueOrDefault();
        if (valueOrDefault.Item2 > TimeSpan.Zero)
        {
          double num = Math.Clamp(valueOrDefault.Item1.TotalSeconds / valueOrDefault.Item2.TotalSeconds, 0.0, 1.0);
          double num2 = num * TextResponseSurface.RingCircumference / 2.0;
          this.clockRing.StrokeDashArray = new DoubleCollection
          {
            Math.Max(0.001, num2),
            1000.0
          };
          this.clockRing.Visibility = ((num <= 0.001) ? Visibility.Collapsed : Visibility.Visible);
        }
      }
    }

    // Token: 0x1700039D RID: 925
    // (get) Token: 0x060013D5 RID: 5077 RVA: 0x005A5B60 File Offset: 0x005A5B60
    public override FrameworkElement View
    {
      get
      {
        return this.view;
      }
    }

    // Token: 0x060013D6 RID: 5078 RVA: 0x005A5B68 File Offset: 0x005A5B68
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.TextResponse textResponse = p as NotchPresentation.TextResponse;
      return textResponse != null && !NotchGeometry.IsAwaitingProse(textResponse);
    }

    // Token: 0x060013D7 RID: 5079 RVA: 0x005A5B8C File Offset: 0x005A5B8C
    public override void Update(NotchPresentation p)
    {
      NotchPresentation.TextResponse textResponse = p as NotchPresentation.TextResponse;
      if (textResponse != null)
      {
        this.Apply(textResponse);
      }
    }

    // Token: 0x060013D8 RID: 5080 RVA: 0x005A5BAC File Offset: 0x005A5BAC
    protected override void OnAttached()
    {
      this.view.Height = double.NaN;
      this.view.VerticalAlignment = VerticalAlignment.Stretch;
      base.Host.Services.Companion.StateChanged += this.OnCompanionState;
      this.renderThrottle = DispatcherQueue.GetForCurrentThread().CreateTimer();
      this.renderThrottle.Interval = TimeSpan.FromMilliseconds(33L, 0L);
      this.renderThrottle.IsRepeating = false;
      this.renderThrottle.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.renderPending = false;
        this.RenderNow();
      };
      this.UpdateFinished();
      this.countdownTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
      this.countdownTimer.Interval = TimeSpan.FromMilliseconds(100L, 0L);
      this.countdownTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.UpdateChrome();
      };
      this.countdownTimer.Start();
      this.UpdateChrome();
      this.RenderNow();
    }

    // Token: 0x060013D9 RID: 5081 RVA: 0x005A5C9C File Offset: 0x005A5C9C
    public override void Detached()
    {
      if (this.pendingDrop.Count > 0)
      {
        ClickyAnalytics.TrackNotchDroppedAttachmentsDiscarded(this.pendingDrop.Count);
        this.pendingDrop.Clear();
      }
      base.Host.Services.Companion.StateChanged -= this.OnCompanionState;
      base.Host.SizeContext.ResponseFinishedStreaming = false;
      base.Host.SizeContext.ResponseHasGallery = false;
      base.Host.SizeContext.FollowUpExtraLines = 0;
      DispatcherQueueTimer dispatcherQueueTimer = this.countdownTimer;
      if (dispatcherQueueTimer != null)
      {
        dispatcherQueueTimer.Stop();
      }
      NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
      if (notchPresenter != null)
      {
        notchPresenter.SetTextResponseFollowUpActive(false);
      }
      if (this.followUp != null)
      {
        base.Host.Services.Presenter.PresentationChanged -= this.ReleaseIfLeaving;
      }
    }

    // Token: 0x060013DA RID: 5082 RVA: 0x005A5D84 File Offset: 0x005A5D84
    private void ReleaseIfLeaving([Nullable(2)] object s, EventArgs e)
    {
    }

    // Token: 0x060013DB RID: 5083 RVA: 0x005A5D88 File Offset: 0x005A5D88
    private void OnCompanionState([Nullable(2)] object s, EventArgs e)
    {
      this.view.DispatcherQueue.TryEnqueue(delegate
      {
        this.UpdateFinished();
        base.Host.Relayout();
      });
    }

    // Token: 0x060013DC RID: 5084 RVA: 0x005A5DA8 File Offset: 0x005A5DA8
    private void Apply(NotchPresentation.TextResponse r)
    {
      this.question = r.Question;
      this.answer = r.Answer ?? "";
      this.thinking = r.IsThinking || this.answer.Trim().Length == 0;
      this.thinkingRow.Visibility = (this.thinking ? Visibility.Visible : Visibility.Collapsed);
      this.answerArea.Visibility = (this.thinking ? Visibility.Collapsed : Visibility.Visible);
      this.close.Visibility = (this.thinking ? Visibility.Collapsed : Visibility.Visible);
      this.chromeMode = "";
      if (base.Host != null)
      {
        this.UpdateChrome();
      }
      if (base.Host == null)
      {
        return;
      }
      if (!this.renderPending)
      {
        this.renderPending = true;
        DispatcherQueueTimer dispatcherQueueTimer = this.renderThrottle;
        if (dispatcherQueueTimer != null)
        {
          dispatcherQueueTimer.Start();
        }
      }
      this.UpdateFinished();
    }

    // Token: 0x060013DD RID: 5085 RVA: 0x005A5E88 File Offset: 0x005A5E88
    private void RenderNow()
    {
      if (this.thinking)
      {
        return;
      }
      List<ValueTuple<string, string>> list;
      StackPanel stackPanel = Markdown.Render(this.answer, Markdown.NotchResponse, out list);
      this.markdownSlot.Content = stackPanel;
      this.copy.Visibility = ((this.answer.Trim().Length > 0) ? Visibility.Visible : Visibility.Collapsed);
      bool flag = list.Count > 0;
      if (flag && this.galleryRow.Children.Count != list.Count)
      {
        this.galleryRow.Children.Clear();
        foreach (ValueTuple<string, string> valueTuple in list)
        {
          string item = valueTuple.Item1;
          string link = valueTuple.Item2;
          Pressable pressable = new Pressable(true)
          {
            Width = 116.0,
            Height = 88.0,
            CornerRadius = new CornerRadius(8.0),
            Background = Ui.Brush(DS.White(0.06)),
            BorderBrush = Ui.Brush(DS.White(0.08)),
            BorderThickness = new Thickness(0.5)
          };
          Uri uri;
          if (Uri.TryCreate(item, UriKind.Absolute, out uri))
          {
            pressable.Children.Add(new Border
            {
              CornerRadius = new CornerRadius(8.0),
              Background = new ImageBrush
              {
                ImageSource = new BitmapImage(uri)
                {
                  DecodePixelWidth = 232
                },
                Stretch = Stretch.UniformToFill
              }
            });
          }
          pressable.Click += delegate
          {
            this.Host.Actions.OpenUrl(link);
          };
          this.galleryRow.Children.Add(pressable);
        }
      }
      this.gallery.Visibility = (flag ? Visibility.Visible : Visibility.Collapsed);
      if (base.Host.SizeContext.ResponseHasGallery != flag)
      {
        base.Host.SizeContext.ResponseHasGallery = flag;
        base.Host.Relayout();
      }
    }

    // Token: 0x060013DE RID: 5086 RVA: 0x005A60CC File Offset: 0x005A60CC
    private void UpdateFinished()
    {
      if (base.Host == null)
      {
        return;
      }
      bool flag = !this.thinking && base.Host.Services.Companion.State == VoiceState.Idle;
      if (flag == this.finished)
      {
        return;
      }
      this.finished = flag;
      base.Host.SizeContext.ResponseFinishedStreaming = this.finished;
      if (this.finished && this.followUp == null)
      {
        TextResponseSurface.<>c__DisplayClass47_0 CS$<>8__locals1 = new TextResponseSurface.<>c__DisplayClass47_0();
        this.followUp = new ComposerBox("ask a follow-up…", base.Host.Services.Auth.IsSignedIn)
        {
          Margin = new Thickness(8.0, 0.0, 8.0, 0.0)
        };
        Grid.SetRow(this.followUp, 1);
        ((Grid)this.answerArea.Children[0]).Children.Add(this.followUp);
        this.followUp.Opacity = 0.0;
        CS$<>8__locals1.box = this.followUp;
        this.followUp.SizeChanged += CS$<>8__locals1.<UpdateFinished>g__Grow|0;
        this.followUp.MetricsChanged += delegate(int extra, int files)
        {
          base.Host.SizeContext.FollowUpExtraLines = extra;
          base.Host.SizeContext.FollowUpAttachmentCount = files;
          base.Host.Relayout();
        };
        this.followUp.Input.GotFocus += delegate(object _, RoutedEventArgs _)
        {
          base.Host.RequestKeyboard();
        };
        this.followUp.Input.PointerPressed += delegate(object _, PointerRoutedEventArgs _)
        {
          base.Host.RequestKeyboard();
        };
        this.followUp.Input.TextChanged += delegate(object _, TextChangedEventArgs _)
        {
          this.<UpdateFinished>g__SyncSuspension|47_4();
        };
        this.followUp.AttachmentsChanged += this.<UpdateFinished>g__SyncSuspension|47_4;
        TextResponseSurface.AttachDropped(this.followUp, this.pendingDrop.ToList<string>());
        this.pendingDrop.Clear();
        this.followUp.Submitted += delegate(string text, IReadOnlyList<string> files)
        {
          base.Host.ReleaseKeyboard();
          if (base.Host.Services.Settings.LibraryModeEnabled)
          {
            base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
          }
          base.Host.Services.Companion.SubmitTextAsync(text, files);
        };
        this.followUp.Cancelled += delegate
        {
          base.Host.ReleaseKeyboard();
          base.Host.Services.Presenter.Present(new NotchPresentation.Idle());
        };
      }
    }

    // Token: 0x060013DF RID: 5087 RVA: 0x005A62D8 File Offset: 0x005A62D8
    private void CopyAnswer()
    {
      DataPackage dataPackage = new DataPackage();
      dataPackage.SetText(this.answer);
      Clipboard.SetContent(dataPackage);
      this.copyGlyph.Text = "\ue73e";
      this.copyGlyph.Foreground = Ui.Brush(DS.Colors.Blue300.WithAlpha(0.95));
      ToolTipService.SetToolTip(this.copy, "Copied");
      DispatcherQueueTimer dispatcherQueueTimer = DispatcherQueue.GetForCurrentThread().CreateTimer().Rooted();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(1.5);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        this.copyGlyph.Text = "\ue8c8";
        this.copyGlyph.Foreground = Ui.Brush(DS.White(this.copy.IsHovered ? 0.9 : 0.5));
        ToolTipService.SetToolTip(this.copy, "Copy response");
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x060013EA RID: 5098 RVA: 0x005A649C File Offset: 0x005A649C
    [CompilerGenerated]
    private void <UpdateFinished>g__SyncSuspension|47_4()
    {
      NotchPresenter notchPresenter = base.Host.Services.Presenter as NotchPresenter;
      if (notchPresenter == null)
      {
        return;
      }
      notchPresenter.SetTextResponseFollowUpActive(this.followUp.HasDraft);
    }

    // Token: 0x04000937 RID: 2359
    private readonly Grid view = new Grid();

    // Token: 0x04000938 RID: 2360
    private readonly Grid thinkingRow;

    // Token: 0x04000939 RID: 2361
    private readonly Grid answerArea = new Grid();

    // Token: 0x0400093A RID: 2362
    private readonly ScrollViewer scroll;

    // Token: 0x0400093B RID: 2363
    private readonly StackPanel prose = new StackPanel
    {
      Spacing = 12.0
    };

    // Token: 0x0400093C RID: 2364
    private readonly ContentControl markdownSlot = new ContentControl
    {
      HorizontalContentAlignment = HorizontalAlignment.Stretch,
      IsTabStop = false
    };

    // Token: 0x0400093D RID: 2365
    private readonly ScrollViewer gallery;

    // Token: 0x0400093E RID: 2366
    private readonly StackPanel galleryRow = new StackPanel
    {
      Orientation = Orientation.Horizontal,
      Spacing = 8.0
    };

    // Token: 0x0400093F RID: 2367
    private readonly Pressable copy;

    // Token: 0x04000940 RID: 2368
    private readonly TextBlock copyGlyph;

    // Token: 0x04000941 RID: 2369
    private readonly Pressable close;

    // Token: 0x04000942 RID: 2370
    [Nullable(2)]
    private ComposerBox followUp;

    // Token: 0x04000943 RID: 2371
    private string question = "";

    // Token: 0x04000944 RID: 2372
    private string answer = "";

    // Token: 0x04000945 RID: 2373
    private bool thinking = true;

    // Token: 0x04000946 RID: 2374
    private bool finished;

    // Token: 0x04000947 RID: 2375
    [Nullable(2)]
    private DispatcherQueueTimer renderThrottle;

    // Token: 0x04000948 RID: 2376
    private bool renderPending;

    // Token: 0x04000949 RID: 2377
    private readonly Grid countdownChrome = new Grid
    {
      HorizontalAlignment = HorizontalAlignment.Right,
      VerticalAlignment = VerticalAlignment.Top,
      Margin = new Thickness(0.0, -2.0, 8.0, 0.0),
      Height = 24.0
    };

    // Token: 0x0400094A RID: 2378
    private Pressable clock;

    // Token: 0x0400094B RID: 2379
    private Ellipse clockRing;

    // Token: 0x0400094C RID: 2380
    private StackPanel hoverPills;

    // Token: 0x0400094D RID: 2381
    private GelActionButton pausedDismiss;

    // Token: 0x0400094E RID: 2382
    [Nullable(2)]
    private DispatcherQueueTimer countdownTimer;

    // Token: 0x0400094F RID: 2383
    private string chromeMode = "";

    // Token: 0x04000950 RID: 2384
    private const double ClockDiameter = 24.0;

    // Token: 0x04000951 RID: 2385
    private const double RingThickness = 2.0;

    // Token: 0x04000952 RID: 2386
    private static readonly double RingCircumference = 75.39822368615503;

    // Token: 0x04000953 RID: 2387
    private readonly List<string> pendingDrop = new List<string>();
  }
}
