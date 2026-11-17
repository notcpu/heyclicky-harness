using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using HeyClicky.Core.Drive;
using HeyClicky.Platform.Input;
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C4 RID: 708
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class OnboardingDriveDemo
  {
    // Token: 0x17000550 RID: 1360
    // (get) Token: 0x06001DD9 RID: 7641 RVA: 0x006027A0 File Offset: 0x006027A0
    // (set) Token: 0x06001DDA RID: 7642 RVA: 0x006027A8 File Offset: 0x006027A8
    public OnboardingDriveDemo.DemoState State { get; private set; }

    // Token: 0x06001DDB RID: 7643 RVA: 0x006027B4 File Offset: 0x006027B4
    private void TrackEnded(string outcome)
    {
      DateTimeOffset? dateTimeOffset = this.analyticsStartedAt;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        this.analyticsStartedAt = null;
        ClickyDriveAnalytics.TrackOnboardingDriveEnded(outcome, (DateTimeOffset.Now - valueOrDefault).TotalSeconds);
        return;
      }
    }

    // Token: 0x14000074 RID: 116
    // (add) Token: 0x06001DDC RID: 7644 RVA: 0x00602804 File Offset: 0x00602804
    // (remove) Token: 0x06001DDD RID: 7645 RVA: 0x0060283C File Offset: 0x0060283C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x17000551 RID: 1361
    // (get) Token: 0x06001DDE RID: 7646 RVA: 0x00602874 File Offset: 0x00602874
    public IReadOnlyList<string> Rows
    {
      get
      {
        return this.rows;
      }
    }

    // Token: 0x17000552 RID: 1362
    // (get) Token: 0x06001DDF RID: 7647 RVA: 0x0060287C File Offset: 0x0060287C
    public IReadOnlyList<bool> Checks
    {
      get
      {
        return this.checks;
      }
    }

    // Token: 0x06001DE0 RID: 7648 RVA: 0x00602884 File Offset: 0x00602884
    public void Reset()
    {
      CancellationTokenSource cancellationTokenSource = this.driveCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.driveCancellation = null;
      for (int i = 0; i < 3; i++)
      {
        this.rows[i] = "";
        this.checks[i] = false;
      }
      this.State = OnboardingDriveDemo.DemoState.Idle;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001DE1 RID: 7649 RVA: 0x006028E4 File Offset: 0x006028E4
    [NullableContext(2)]
    public string StatusLine(bool stepCompleted, bool listening)
    {
      if (this.State == OnboardingDriveDemo.DemoState.Driving)
      {
        return "driving… press esc to take it back";
      }
      if (stepCompleted)
      {
        return null;
      }
      if (!listening)
      {
        return "waiting for you…";
      }
      return "listening…";
    }

    // Token: 0x06001DE2 RID: 7650 RVA: 0x00602908 File Offset: 0x00602908
    public void Run(OnboardingDriveContext context)
    {
      if (this.State == OnboardingDriveDemo.DemoState.Driving)
      {
        return;
      }
      OnboardingDriveDemo.DemoState state = this.State;
      bool flag = state - OnboardingDriveDemo.DemoState.Done <= 1;
      bool flag2 = flag;
      this.Reset();
      this.State = OnboardingDriveDemo.DemoState.Driving;
      this.analyticsStartedAt = new DateTimeOffset?(DateTimeOffset.Now);
      ClickyDriveAnalytics.TrackOnboardingDriveStarted(flag2);
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      this.running = context;
      CancellationTokenSource cancellationTokenSource = (this.driveCancellation = new CancellationTokenSource());
      this.RunAsync(context, cancellationTokenSource.Token);
    }

    // Token: 0x06001DE3 RID: 7651 RVA: 0x0060298C File Offset: 0x0060298C
    private async Task RunAsync(OnboardingDriveContext context, CancellationToken ct)
    {
      DispatcherQueue dispatcher = DispatcherQueue.GetForCurrentThread();
      SynchronizationContext synchronizationContext = SynchronizationContext.Current;
      try
      {
        TakeOverOverlay pill = (this.overlay = new TakeOverOverlay());
        pill.Show(context.Display, context.Accent, "Getting started");
        context.PlayChime("agent-launch", 0.5);
        string firstName = context.FirstName;
        string text = ((firstName != null && firstName.Length > 0) ? ("Alright " + firstName + ", I'm taking over your mouse now. Just watch me.") : "Alright, I'm taking over your mouse now. Just watch me.");
        await OnboardingDriveDemo.SayStepAsync(context, text, "Taking over your mouse", pill, 1.6, ct);
        await OnboardingDriveDemo.WaitForLineToFinishAsync(context, 5.0, ct);
        if (!ct.IsCancellationRequested && this.State == OnboardingDriveDemo.DemoState.Driving)
        {
          ClickyUserInputBlocker.Shared.Start(45.0, delegate
          {
            dispatcher.TryEnqueue(new DispatcherQueueHandler(this.StopForEscape));
          }, delegate(Action action)
          {
            dispatcher.TryEnqueue(delegate
            {
              action();
            });
          });
          await Task.Delay(300, ct);
          int index = 0;
          while (index < OnboardingDriveDemo.DemoLines.Length && !ct.IsCancellationRequested)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
            defaultInterpolatedStringHandler.AppendLiteral("row");
            defaultInterpolatedStringHandler.AppendFormatted<int>(index);
            defaultInterpolatedStringHandler.AppendLiteral(".field");
            PointD? pointD = this.ScreenCenter(defaultInterpolatedStringHandler.ToStringAndClear());
            if (pointD == null)
            {
              break;
            }
            PointD point = pointD.GetValueOrDefault();
            await OnboardingDriveDemo.SayStepAsync(context, OnboardingDriveDemo.StepLines[index], null, pill, 1.6, ct);
            pill.SetActing(true);
            await ClickyScreenActuator.GlideAsync(point, (index == 0) ? 0.85 : 0.6, ct);
            await Task.Delay(180, ct);
            await ClickyScreenActuator.ClickAsync(point, ClickyMouseButton.Left, 1, ClickyModifierKeys.None);
            await Task.Delay(200, ct);
            foreach (char c in OnboardingDriveDemo.DemoLines[index])
            {
              if (ct.IsCancellationRequested)
              {
                break;
              }
              await ClickyScreenActuator.TypeAsync(c.ToString(), ct);
              await Task.Delay(Random.Shared.Next(15, 36), ct);
            }
            string text2 = null;
            pill.SetActing(false);
            await Task.Delay(350, ct);
            index++;
          }
          if (!ct.IsCancellationRequested)
          {
            await OnboardingDriveDemo.SayStepAsync(context, "Now I'll tick off the first two.", null, pill, 1.6, ct);
          }
          index = 0;
          while (index < 2 && !ct.IsCancellationRequested)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
            defaultInterpolatedStringHandler.AppendLiteral("row");
            defaultInterpolatedStringHandler.AppendFormatted<int>(index);
            defaultInterpolatedStringHandler.AppendLiteral(".check");
            PointD? pointD = this.ScreenCenter(defaultInterpolatedStringHandler.ToStringAndClear());
            if (pointD == null)
            {
              break;
            }
            PointD point = pointD.GetValueOrDefault();
            pill.SetActing(true);
            await ClickyScreenActuator.GlideAsync(point, 0.55, ct);
            await Task.Delay(180, ct);
            await ClickyScreenActuator.ClickAsync(point, ClickyMouseButton.Left, 1, ClickyModifierKeys.None);
            Action<string, float> playDrawingSound = context.PlayDrawingSound;
            if (playDrawingSound != null)
            {
              playDrawingSound("copy-tick", 0.22f);
            }
            pill.SetActing(false);
            await Task.Delay(400, ct);
            index++;
          }
          if (ct.IsCancellationRequested)
          {
            pill.Hide(false);
          }
          else
          {
            ClickyUserInputBlocker.Shared.Stop();
            context.PlayChime("agent-done", 0.55);
            pill.SetStatus("Done");
            await Task.Delay(600, ct);
            pill.Hide(true);
            this.overlay = null;
            this.State = OnboardingDriveDemo.DemoState.Done;
            this.driveCancellation = null;
            this.TrackEnded("completed");
            Action changed = this.Changed;
            if (changed != null)
            {
              changed();
            }
            await context.SpeakLine("That was me on your mouse. Ask me to do it for you, in any app, and I will.");
            context.StepCompleted();
            pill = null;
          }
        }
      }
      catch (OperationCanceledException)
      {
      }
      catch (Exception ex)
      {
        Log.Error("drive", "the tour's drive failed", ex);
      }
      finally
      {
        ClickyUserInputBlocker.Shared.Stop();
        if (ct.IsCancellationRequested)
        {
          TakeOverOverlay takeOverOverlay = this.overlay;
          if (takeOverOverlay != null)
          {
            takeOverOverlay.Hide(false);
          }
        }
      }
    }

    // Token: 0x06001DE4 RID: 7652 RVA: 0x006029E0 File Offset: 0x006029E0
    private static async Task SayStepAsync(OnboardingDriveContext context, string line, [Nullable(2)] string pillText, TakeOverOverlay pill, double maximumWaitSeconds, CancellationToken ct)
    {
      pill.SetStatus(pillText ?? line);
      int before = context.AudioPlaybackBeganCount();
      DateTimeOffset deadline = DateTimeOffset.Now.AddSeconds(maximumWaitSeconds);
      await context.SpeakLine(line);
      while (context.AudioPlaybackBeganCount() <= before && DateTimeOffset.Now < deadline)
      {
        await Task.Delay(50, ct);
      }
    }

    // Token: 0x06001DE5 RID: 7653 RVA: 0x00602A50 File Offset: 0x00602A50
    private static async Task WaitForLineToFinishAsync(OnboardingDriveContext context, double maximumSeconds, CancellationToken ct)
    {
      DateTimeOffset deadline = DateTimeOffset.Now.AddSeconds(maximumSeconds);
      DateTimeOffset? quietSince = null;
      while (DateTimeOffset.Now < deadline && !ct.IsCancellationRequested)
      {
        if (context.IsResponding())
        {
          quietSince = null;
        }
        else if (quietSince != null)
        {
          DateTimeOffset valueOrDefault = quietSince.GetValueOrDefault();
          if (DateTimeOffset.Now - valueOrDefault >= TimeSpan.FromSeconds(0.25))
          {
            return;
          }
        }
        else
        {
          quietSince = new DateTimeOffset?(DateTimeOffset.Now);
        }
        await Task.Delay(50, ct);
      }
    }

    // Token: 0x06001DE6 RID: 7654 RVA: 0x00602AA4 File Offset: 0x00602AA4
    private void StopForEscape()
    {
      if (this.State != OnboardingDriveDemo.DemoState.Driving)
      {
        return;
      }
      CancellationTokenSource cancellationTokenSource = this.driveCancellation;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.driveCancellation = null;
      TakeOverOverlay takeOverOverlay = this.overlay;
      if (takeOverOverlay != null)
      {
        takeOverOverlay.Hide(false);
      }
      this.overlay = null;
      OnboardingDriveContext onboardingDriveContext = this.running;
      if (onboardingDriveContext != null)
      {
        onboardingDriveContext.PlayChime("agent-close", 0.5);
      }
      this.State = OnboardingDriveDemo.DemoState.HandedBack;
      this.TrackEnded("escaped");
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      OnboardingDriveContext onboardingDriveContext2 = this.running;
      if (onboardingDriveContext2 == null)
      {
        return;
      }
      onboardingDriveContext2.StepCompleted();
    }

    // Token: 0x06001DE7 RID: 7655 RVA: 0x00602B48 File Offset: 0x00602B48
    public void Cancel()
    {
      if (this.State != OnboardingDriveDemo.DemoState.Driving)
      {
        return;
      }
      ClickyUserInputBlocker.Shared.Stop();
      TakeOverOverlay takeOverOverlay = this.overlay;
      if (takeOverOverlay != null)
      {
        takeOverOverlay.Hide(false);
      }
      this.overlay = null;
      this.TrackEnded("cancelled");
      this.Reset();
    }

    // Token: 0x06001DE8 RID: 7656 RVA: 0x00602B88 File Offset: 0x00602B88
    public PointD? ScreenCenter(string id)
    {
      WeakReference<FrameworkElement> weakReference;
      FrameworkElement frameworkElement;
      PointD? pointD;
      if (!this.targets.TryGetValue(id, out weakReference) || !weakReference.TryGetTarget(out frameworkElement) || frameworkElement.XamlRoot == null)
      {
        pointD = null;
        return pointD;
      }
      try
      {
        Point point = frameworkElement.TransformToVisual(null).TransformPoint(new Point(frameworkElement.ActualWidth / 2.0, frameworkElement.ActualHeight / 2.0));
        double rasterizationScale = frameworkElement.XamlRoot.RasterizationScale;
        IntPtr windowFromWindowId = Win32Interop.GetWindowFromWindowId(frameworkElement.XamlRoot.ContentIslandEnvironment.AppWindowId);
        OnboardingDriveDemo.NativePoint nativePoint = default(OnboardingDriveDemo.NativePoint);
        OnboardingDriveDemo.ClientToScreen(windowFromWindowId, ref nativePoint);
        pointD = new PointD?(new PointD((double)nativePoint.X + point.X * rasterizationScale, (double)nativePoint.Y + point.Y * rasterizationScale));
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "target " + id + " not placed: " + ex.Message);
        pointD = null;
      }
      return pointD;
    }

    // Token: 0x06001DE9 RID: 7657 RVA: 0x00602C9C File Offset: 0x00602C9C
    private void Register(FrameworkElement element, string id)
    {
      this.targets[id] = new WeakReference<FrameworkElement>(element);
    }

    // Token: 0x06001DEA RID: 7658 RVA: 0x00602CB0 File Offset: 0x00602CB0
    public FrameworkElement CreateWindowView(Color accent)
    {
      Grid grid = new Grid
      {
        Height = 30.0,
        Padding = new Thickness(12.0, 0.0, 0.0, 0.0),
        Background = Ui.Brush(DS.Rgb(0.92, 0.92, 0.92, 1.0)),
        IsHitTestVisible = false
      };
      TextBlock textBlock = Ui.Text("Your list", 12.0, FontWeights.SemiBold, DS.Black(0.55), TextAlignment.Left, 0, false, false);
      textBlock.HorizontalAlignment = HorizontalAlignment.Left;
      textBlock.VerticalAlignment = VerticalAlignment.Center;
      grid.Children.Add(textBlock);
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Right
      };
      foreach (string text in OnboardingDriveDemo.CaptionGlyphs)
      {
        Grid grid2 = new Grid
        {
          Width = 34.0,
          Height = 30.0
        };
        grid2.Children.Add(Icons.Glyph(text, 10.0, DS.Black(0.55)));
        stackPanel.Children.Add(grid2);
      }
      grid.Children.Add(stackPanel);
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 8.0,
        Padding = new Thickness(14.0)
      };
      for (int j = 0; j < 3; j++)
      {
        stackPanel2.Children.Add(this.Row(j, accent));
      }
      StackPanel stackPanel3 = new StackPanel();
      stackPanel3.Children.Add(grid);
      stackPanel3.Children.Add(stackPanel2);
      Border border = new Border
      {
        Width = 340.0,
        CornerRadius = new CornerRadius(12.0),
        Background = Ui.Brush(DS.Rgb(0.98, 0.98, 0.98, 1.0)),
        BorderBrush = Ui.Brush(DS.Black(0.12)),
        BorderThickness = new Thickness(1.0),
        Child = stackPanel3,
        RequestedTheme = ElementTheme.Light
      };
      Ui.ClipRounded(border, 12.0);
      return new Grid
      {
        Width = 340.0,
        HorizontalAlignment = HorizontalAlignment.Center,
        Children = 
        {
          new ShadowLayer(12.0, DS.Black(0.3), 14.0, 6.0, 0.0),
          border
        }
      };
    }

    // Token: 0x06001DEB RID: 7659 RVA: 0x00602FA0 File Offset: 0x00602FA0
    private FrameworkElement Row(int index, Color accent)
    {
      OnboardingDriveDemo.<>c__DisplayClass37_0 CS$<>8__locals1 = new OnboardingDriveDemo.<>c__DisplayClass37_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.index = index;
      CS$<>8__locals1.row = new Grid
      {
        Height = 34.0,
        Padding = new Thickness(10.0, 0.0, 10.0, 0.0),
        ColumnSpacing = 10.0,
        CornerRadius = new CornerRadius(8.0),
        Background = Ui.Brush(DS.Black(0.04))
      };
      CS$<>8__locals1.row.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      CS$<>8__locals1.row.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      Pressable pressable = new Pressable(true)
      {
        Width = 20.0,
        Height = 20.0,
        VerticalAlignment = VerticalAlignment.Center
      };
      CS$<>8__locals1.ring = new Ellipse
      {
        Width = 16.0,
        Height = 16.0,
        Stroke = Ui.Brush(DS.Black(0.3)),
        StrokeThickness = 1.6
      };
      CS$<>8__locals1.filled = Icons.Glyph("\uec61", 18.0, accent);
      CS$<>8__locals1.filled.HorizontalAlignment = HorizontalAlignment.Center;
      CS$<>8__locals1.filled.VerticalAlignment = VerticalAlignment.Center;
      pressable.Children.Add(CS$<>8__locals1.ring);
      pressable.Children.Add(CS$<>8__locals1.filled);
      pressable.Click += delegate
      {
        CS$<>8__locals1.<>4__this.checks[CS$<>8__locals1.index] = !CS$<>8__locals1.<>4__this.checks[CS$<>8__locals1.index];
        Action changed = CS$<>8__locals1.<>4__this.Changed;
        if (changed == null)
        {
          return;
        }
        changed();
      };
      CS$<>8__locals1.row.Children.Add(pressable);
      FrameworkElement frameworkElement = pressable;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      defaultInterpolatedStringHandler.AppendLiteral("row");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.index);
      defaultInterpolatedStringHandler.AppendLiteral(".check");
      this.Register(frameworkElement, defaultInterpolatedStringHandler.ToStringAndClear());
      CS$<>8__locals1.field = new TextBox
      {
        PlaceholderText = "New item",
        FontSize = 14.0,
        FontWeight = FontWeights.Medium,
        FontFamily = DS.Fonts.For(14.0),
        BorderThickness = new Thickness(0.0),
        Background = Ui.Transparent,
        Padding = new Thickness(0.0),
        MinHeight = 0.0,
        VerticalAlignment = VerticalAlignment.Center,
        IsSpellCheckEnabled = false,
        Foreground = Ui.Brush(DS.Black(0.85))
      };
      foreach (string text in new string[] { "TextControlBackgroundFocused", "TextControlBackgroundPointerOver", "TextControlBorderBrushFocused", "TextControlBorderBrushPointerOver", "TextControlBorderBrush" })
      {
        CS$<>8__locals1.field.Resources[text] = Ui.Transparent;
      }
      CS$<>8__locals1.field.Resources["TextControlForegroundFocused"] = Ui.Brush(DS.Black(0.85));
      CS$<>8__locals1.field.Resources["TextControlForegroundPointerOver"] = Ui.Brush(DS.Black(0.85));
      CS$<>8__locals1.field.TextChanged += delegate(object _, TextChangedEventArgs _)
      {
        CS$<>8__locals1.<>4__this.rows[CS$<>8__locals1.index] = CS$<>8__locals1.field.Text;
      };
      CS$<>8__locals1.field.Loaded += delegate(object _, RoutedEventArgs _)
      {
        FrameworkElement frameworkElement2 = OnboardingDriveDemo.FindDescendant(CS$<>8__locals1.field, "DeleteButton") as FrameworkElement;
        if (frameworkElement2 != null)
        {
          frameworkElement2.Width = 0.0;
          frameworkElement2.Opacity = 0.0;
        }
      };
      CS$<>8__locals1.struck = Ui.Text("", 14.0, FontWeights.Medium, DS.Black(0.4), TextAlignment.Left, 0, false, false);
      CS$<>8__locals1.struck.TextDecorations = TextDecorations.Strikethrough;
      CS$<>8__locals1.struck.VerticalAlignment = VerticalAlignment.Center;
      CS$<>8__locals1.struck.Visibility = Visibility.Collapsed;
      Grid grid = new Grid();
      grid.Children.Add(CS$<>8__locals1.field);
      grid.Children.Add(CS$<>8__locals1.struck);
      Grid.SetColumn(grid, 1);
      CS$<>8__locals1.row.Children.Add(grid);
      FrameworkElement field = CS$<>8__locals1.field;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      defaultInterpolatedStringHandler.AppendLiteral("row");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.index);
      defaultInterpolatedStringHandler.AppendLiteral(".field");
      this.Register(field, defaultInterpolatedStringHandler.ToStringAndClear());
      CS$<>8__locals1.<Row>g__Sync|3();
      this.Changed += delegate
      {
        CS$<>8__locals1.row.DispatcherQueue.TryEnqueue(new DispatcherQueueHandler(base.<Row>g__Sync|3));
      };
      return CS$<>8__locals1.row;
    }

    // Token: 0x06001DEC RID: 7660 RVA: 0x00603444 File Offset: 0x00603444
    [return: Nullable(2)]
    private static DependencyObject FindDescendant(DependencyObject root, string name)
    {
      for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++)
      {
        DependencyObject child = VisualTreeHelper.GetChild(root, i);
        FrameworkElement frameworkElement = child as FrameworkElement;
        if (frameworkElement != null)
        {
          string name2 = frameworkElement.Name;
          if (name2 == name)
          {
            return child;
          }
        }
        DependencyObject dependencyObject = OnboardingDriveDemo.FindDescendant(child, name);
        if (dependencyObject != null)
        {
          return dependencyObject;
        }
      }
      return null;
    }

    // Token: 0x06001DED RID: 7661
    [DllImport("user32.dll")]
    private static extern bool ClientToScreen(IntPtr hwnd, ref OnboardingDriveDemo.NativePoint point);

    // Token: 0x04000E4D RID: 3661
    public static readonly OnboardingDriveDemo Shared = new OnboardingDriveDemo();

    // Token: 0x04000E4E RID: 3662
    private readonly string[] rows = new string[] { "", "", "" };

    // Token: 0x04000E4F RID: 3663
    private readonly bool[] checks = new bool[3];

    // Token: 0x04000E50 RID: 3664
    private readonly Dictionary<string, WeakReference<FrameworkElement>> targets = new Dictionary<string, WeakReference<FrameworkElement>>();

    // Token: 0x04000E51 RID: 3665
    [Nullable(2)]
    private CancellationTokenSource driveCancellation;

    // Token: 0x04000E52 RID: 3666
    [Nullable(2)]
    private TakeOverOverlay overlay;

    // Token: 0x04000E53 RID: 3667
    [Nullable(2)]
    private OnboardingDriveContext running;

    // Token: 0x04000E54 RID: 3668
    private DateTimeOffset? analyticsStartedAt;

    // Token: 0x04000E57 RID: 3671
    private static readonly string[] DemoLines = new string[] { "Show Clicky what you're working on", "Ask Clicky when you're stuck", "Let Clicky do the clicking" };

    // Token: 0x04000E58 RID: 3672
    private static readonly string[] StepLines = new string[] { "First, I'll write you a little list.", "Next row: when to ask me.", "And the last one, the clicking." };

    // Token: 0x04000E59 RID: 3673
    public const double TitleBarHeight = 30.0;

    // Token: 0x04000E5A RID: 3674
    public const double CaptionButtonWidth = 34.0;

    // Token: 0x04000E5B RID: 3675
    private static readonly string[] CaptionGlyphs = new string[] { "\ue921", "\ue922", "\ue8bb" };

    // Token: 0x020008DD RID: 2269
    [NullableContext(0)]
    public enum DemoState
    {
      // Token: 0x04002563 RID: 9571
      Idle,
      // Token: 0x04002564 RID: 9572
      Driving,
      // Token: 0x04002565 RID: 9573
      Done,
      // Token: 0x04002566 RID: 9574
      HandedBack
    }

    // Token: 0x020008DE RID: 2270
    [NullableContext(0)]
    private struct NativePoint
    {
      // Token: 0x04002567 RID: 9575
      public int X;

      // Token: 0x04002568 RID: 9576
      public int Y;
    }
  }
}
