using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Drive;
using Microsoft.Graphics.Canvas;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C5 RID: 709
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TakeOverConsentCard
  {
    // Token: 0x17000553 RID: 1363
    // (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00603568 File Offset: 0x00603568
    public bool IsShown
    {
      get
      {
        OverlayWindow overlayWindow = this.window;
        return overlayWindow != null && overlayWindow.IsShown;
      }
    }

    // Token: 0x06001DF1 RID: 7665 RVA: 0x0060357C File Offset: 0x0060357C
    internal void AcceptForTest()
    {
      if (this.IsShown)
      {
        Action action = this.acceptForTest;
        if (action == null)
        {
          return;
        }
        action();
      }
    }

    // Token: 0x06001DF2 RID: 7666 RVA: 0x00603598 File Offset: 0x00603598
    public void Present(string goal, [Nullable(2)] string line, Color accent, Action onAccept, Action onDecline, [Nullable(new byte[] { 2, 1 })] Action<string, double> playChime = null, [Nullable(2)] Action onTimedOut = null, string note = "Hands off the mouse while I drive. Esc stops me.", string acceptTitle = "Go ahead", bool asksTheUser = false, int timeoutSeconds = 45, [Nullable(2)] string alwaysTitle = null, [Nullable(2)] Action onAlways = null)
    {
      TakeOverConsentCard.<>c__DisplayClass8_0 CS$<>8__locals1 = new TakeOverConsentCard.<>c__DisplayClass8_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.onAccept = onAccept;
      CS$<>8__locals1.onAlways = onAlways;
      CS$<>8__locals1.onDecline = onDecline;
      CS$<>8__locals1.onTimedOut = onTimedOut;
      this.Dismiss(false);
      DriveNotchOwnership.SetConsentShown(true);
      TakeOverConsentCard.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.generation + 1;
      this.generation = num;
      CS$<>8__locals2.presentGeneration = num;
      Displays.Monitor monitor = Displays.UnderCursor();
      double scale = monitor.Scale;
      double num2 = ((alwaysTitle == null) ? Math.Max(360.0, 500.0) : Math.Max(460.0, 600.0));
      double num3 = 8.0;
      double num4 = num2 - 60.0 - 42.0;
      string text2;
      if (line != null)
      {
        string text = ClickyDriveLines.Sentence(line);
        text2 = ((text != null && text.Length > 0) ? text : null);
      }
      else
      {
        text2 = null;
      }
      string text3 = text2;
      TextBlock textBlock = Ui.Text(text3 ?? ClickyDriveLines.Brief(goal), (double)((text3 == null) ? 11 : 14), (text3 == null) ? FontWeights.Normal : FontWeights.SemiBold, Colors.White, TextAlignment.Left, 0, true, false);
      textBlock.Measure(new Size((text3 == null) ? num4 : (num4 - 48.0), double.PositiveInfinity));
      double height = textBlock.DesiredSize.Height;
      TextBlock textBlock2 = Ui.Text(note, 11.0, FontWeights.Normal, Colors.White, TextAlignment.Left, 0, false, false);
      textBlock2.TextWrapping = TextWrapping.WrapWholeWords;
      textBlock2.Measure(new Size(num4, double.PositiveInfinity));
      double num5 = (double)((textBlock2.DesiredSize.Height > 16.0) ? 14 : 0);
      double num6 = num3 + num5 + (double)((text3 == null) ? ((height > 16.0) ? 138 : 124) : ((height > 20.0) ? 128 : 110));
      if (this.window == null)
      {
        this.window = new OverlayWindow("HeyClicky drive", true, false, false, true, false, true);
      }
      int num7 = monitor.Bounds.Left + (int)Math.Round(((double)monitor.Bounds.Width - num2 * scale) / 2.0);
      this.window.PlacePx(num7, monitor.Bounds.Top, (int)Math.Ceiling(num2 * scale), (int)Math.Ceiling((num6 + 30.0) * scale));
      this.window.SetRegionPx(0, 0, (int)Math.Ceiling(num2 * scale), (int)Math.Ceiling((num6 + 30.0) * scale));
      Grid grid = new Grid
      {
        Width = num2,
        Height = num6 + 30.0,
        VerticalAlignment = VerticalAlignment.Top
      };
      Grid grid2 = new Grid
      {
        Width = num2,
        Height = num6,
        VerticalAlignment = VerticalAlignment.Top
      };
      grid.Children.Add(grid2);
      Compositor compositor = Ui.Compositor;
      CompositionPath compositionPath = new CompositionPath(DriveArt.NotchAttachedPath(CanvasDevice.GetSharedDevice(), num2, num6, 26.0));
      ShapeVisual shapeVisual = compositor.CreateShapeVisual();
      shapeVisual.Size = new Vector2((float)num2, (float)num6);
      CompositionSpriteShape compositionSpriteShape = compositor.CreateSpriteShape(compositor.CreatePathGeometry(compositionPath));
      compositionSpriteShape.FillBrush = compositor.CreateColorBrush(Colors.Black);
      shapeVisual.Shapes.Add(compositionSpriteShape);
      ShapeVisual shapeVisual2 = compositor.CreateShapeVisual();
      shapeVisual2.Size = shapeVisual.Size;
      CompositionSpriteShape compositionSpriteShape2 = compositor.CreateSpriteShape(compositor.CreatePathGeometry(compositionPath));
      compositionSpriteShape2.FillBrush = compositor.CreateColorBrush(Colors.Black);
      shapeVisual2.Shapes.Add(compositionSpriteShape2);
      CompositionVisualSurface compositionVisualSurface = compositor.CreateVisualSurface();
      compositionVisualSurface.SourceVisual = shapeVisual2;
      compositionVisualSurface.SourceSize = shapeVisual.Size;
      CompositionSurfaceBrush compositionSurfaceBrush = compositor.CreateSurfaceBrush(compositionVisualSurface);
      compositionSurfaceBrush.Stretch = CompositionStretch.None;
      DropShadow dropShadow = compositor.CreateDropShadow();
      dropShadow.Color = DS.Black(0.45);
      dropShadow.BlurRadius = 36f;
      dropShadow.Offset = new Vector3(0f, 8f, 0f);
      dropShadow.Mask = compositionSurfaceBrush;
      SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
      spriteVisual.Size = shapeVisual.Size;
      spriteVisual.Shadow = dropShadow;
      ContainerVisual containerVisual = compositor.CreateContainerVisual();
      containerVisual.Size = shapeVisual.Size;
      containerVisual.Children.InsertAtTop(spriteVisual);
      containerVisual.Children.InsertAtTop(shapeVisual);
      Grid grid3 = new Grid();
      ElementCompositionPreview.SetElementChildVisual(grid3, containerVisual);
      grid2.Children.Add(grid3);
      Image image = new Image
      {
        Width = 44.0,
        Height = 44.0,
        Margin = new Thickness(-6.0),
        Source = (asksTheUser ? DriveArt.YourTurnChip(32.0, 44.0, scale, 11) : DriveArt.DriveChip(32.0, 44.0, scale, 11)),
        VerticalAlignment = VerticalAlignment.Top
      };
      TextBlock textBlock3 = Ui.Text(text3 ?? "Want me to do it?", 14.0, FontWeights.SemiBold, DS.White(0.95), TextAlignment.Left, 2, false, false);
      textBlock3.TextWrapping = TextWrapping.WrapWholeWords;
      ValueTuple<WriteableBitmap, double, double> valueTuple = DriveArt.Stamp("YOU", DriveArt.NeedsYouOnBlack, 7.5, scale);
      WriteableBitmap item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      double item3 = valueTuple.Item3;
      Image image2 = new Image
      {
        Source = item,
        Width = item2,
        Height = item3,
        Opacity = 0.92,
        VerticalAlignment = VerticalAlignment.Top,
        Rotation = -5f,
        CenterPoint = new Vector3((float)item2 / 2f, (float)item3 / 2f, 0f)
      };
      Grid grid4 = new Grid
      {
        ColumnSpacing = 8.0
      };
      grid4.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Auto)
      });
      grid4.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid4.Children.Add(textBlock3);
      Grid.SetColumn(image2, 1);
      image2.HorizontalAlignment = HorizontalAlignment.Left;
      grid4.Children.Add(image2);
      textBlock3.MaxWidth = num4 - item2 - 8.0;
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 3.0
      };
      stackPanel.Children.Add(grid4);
      if (text3 == null)
      {
        TextBlock textBlock4 = Ui.Text(ClickyDriveLines.Brief(goal), 11.0, FontWeights.Normal, DS.White(0.62), TextAlignment.Left, 2, false, false);
        stackPanel.Children.Add(textBlock4);
      }
      TextBlock textBlock5 = Ui.Text(note, 11.0, FontWeights.Normal, DS.White(0.42), TextAlignment.Left, 2, false, false);
      textBlock5.TextWrapping = TextWrapping.WrapWholeWords;
      stackPanel.Children.Add(textBlock5);
      Grid grid5 = new Grid
      {
        ColumnSpacing = 10.0
      };
      grid5.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = GridLength.Auto
      });
      grid5.ColumnDefinitions.Add(new ColumnDefinition
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      grid5.Children.Add(image);
      Grid.SetColumn(stackPanel, 1);
      grid5.Children.Add(stackPanel);
      GelButton gelButton = ((alwaysTitle == null) ? null : new GelButton(alwaysTitle, GelAppearance.PrimaryBlue, GelButton.Sizing.Normal, 100.0, "infinity", new double?((double)13), new double?((double)34)));
      GelButton gelButton2 = new GelButton(acceptTitle, (gelButton == null) ? GelAppearance.PrimaryBlue : GelAppearance.SecondaryWhite, GelButton.Sizing.Normal, 100.0, null, new double?((double)13), new double?((double)34));
      GelButton gelButton3 = new GelButton("Not now", (gelButton == null) ? GelAppearance.SecondaryWhite : GelAppearance.SecondaryOutline, GelButton.Sizing.Normal, 100.0, null, new double?((double)13), new double?((double)34));
      StackPanel stackPanel2 = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 8.0,
        Margin = new Thickness(42.0, 0.0, 0.0, 0.0)
      };
      if (gelButton != null)
      {
        stackPanel2.Children.Add(gelButton);
      }
      stackPanel2.Children.Add(gelButton2);
      stackPanel2.Children.Add(gelButton3);
      StackPanel stackPanel3 = new StackPanel
      {
        Spacing = 9.0,
        VerticalAlignment = VerticalAlignment.Top,
        Margin = new Thickness(30.0, num3 + 10.0, 30.0, 0.0)
      };
      stackPanel3.Children.Add(grid5);
      stackPanel3.Children.Add(stackPanel2);
      grid2.Children.Add(stackPanel3);
      CS$<>8__locals1.answered = false;
      gelButton2.Click += CS$<>8__locals1.<Present>g__Accept|0;
      this.acceptForTest = new Action(CS$<>8__locals1.<Present>g__Accept|0);
      if (gelButton != null)
      {
        gelButton.Click += delegate
        {
          if (CS$<>8__locals1.answered)
          {
            return;
          }
          CS$<>8__locals1.answered = true;
          CS$<>8__locals1.<>4__this.Dismiss(false);
          Action onAlways2 = CS$<>8__locals1.onAlways;
          if (onAlways2 == null)
          {
            return;
          }
          onAlways2();
        };
      }
      gelButton3.Click += delegate
      {
        if (CS$<>8__locals1.answered)
        {
          return;
        }
        CS$<>8__locals1.answered = true;
        CS$<>8__locals1.<>4__this.Dismiss(true);
        CS$<>8__locals1.onDecline();
      };
      this.window.Root.Children.Clear();
      this.window.Root.Children.Add(grid);
      this.window.ShowNoActivate();
      if (playChime != null)
      {
        playChime("agent-needs-you", 0.5);
      }
      SwiftSpring swiftSpring = new SwiftSpring(0.42, 0.78);
      grid2.CenterPoint = new Vector3((float)num2 / 2f, 0f, 0f);
      stackPanel3.Opacity = 0.0;
      if (Anim.ReduceMotion)
      {
        Anim.OpacitySpring(stackPanel3, 1f, swiftSpring);
      }
      else
      {
        grid2.Scale = Vector3.One;
        Vector3KeyFrameAnimation vector3KeyFrameAnimation = SpringCurve.Scale(compositor, swiftSpring, new Vector3(0.4f, 0.2f, 1f), Vector3.One);
        vector3KeyFrameAnimation.Target = "Scale";
        grid2.StartAnimation(vector3KeyFrameAnimation);
        Anim.OpacitySpring(stackPanel3, 1f, swiftSpring);
      }
      DispatcherQueueTimer dispatcherQueueTimer = this.window.DispatcherQueue.CreateTimer();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds((long)timeoutSeconds);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (!((CS$<>8__locals1.<>4__this.generation != CS$<>8__locals1.presentGeneration) | CS$<>8__locals1.answered))
        {
          OverlayWindow overlayWindow = CS$<>8__locals1.<>4__this.window;
          if (overlayWindow != null && overlayWindow.IsShown)
          {
            CS$<>8__locals1.answered = true;
            CS$<>8__locals1.<>4__this.Dismiss(true);
            (CS$<>8__locals1.onTimedOut ?? CS$<>8__locals1.onDecline)();
            return;
          }
        }
      };
      dispatcherQueueTimer.Start();
      this.timeout = new TakeOverConsentCard.DispatcherQueueTimerHandle(dispatcherQueueTimer);
    }

    // Token: 0x06001DF3 RID: 7667 RVA: 0x0060406C File Offset: 0x0060406C
    public void Dismiss(bool animated = true)
    {
      TakeOverConsentCard.<>c__DisplayClass9_0 CS$<>8__locals1 = new TakeOverConsentCard.<>c__DisplayClass9_0();
      CS$<>8__locals1.<>4__this = this;
      TakeOverConsentCard.DispatcherQueueTimerHandle dispatcherQueueTimerHandle = this.timeout;
      if (dispatcherQueueTimerHandle != null)
      {
        dispatcherQueueTimerHandle.Stop();
      }
      this.timeout = null;
      DriveNotchOwnership.SetConsentShown(false);
      if (this.window == null || !this.window.IsShown)
      {
        return;
      }
      TakeOverConsentCard.<>c__DisplayClass9_0 CS$<>8__locals2 = CS$<>8__locals1;
      int num = this.generation + 1;
      this.generation = num;
      CS$<>8__locals2.dismissGeneration = num;
      CS$<>8__locals1.closing = this.window;
      if (!animated)
      {
        CS$<>8__locals1.closing.HideWindow();
        CS$<>8__locals1.closing.Root.Children.Clear();
        return;
      }
      UIElement uielement = CS$<>8__locals1.closing.Root.Children.FirstOrDefault<UIElement>();
      if (uielement != null)
      {
        Anim.Opacity(uielement, 0f, 0.18, null, 0.0);
      }
      DispatcherQueueTimer dispatcherQueueTimer = CS$<>8__locals1.closing.DispatcherQueue.CreateTimer();
      dispatcherQueueTimer.Interval = TimeSpan.FromSeconds(0.18);
      dispatcherQueueTimer.IsRepeating = false;
      dispatcherQueueTimer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        if (CS$<>8__locals1.<>4__this.generation != CS$<>8__locals1.dismissGeneration)
        {
          return;
        }
        CS$<>8__locals1.closing.HideWindow();
        CS$<>8__locals1.closing.Root.Children.Clear();
      };
      dispatcherQueueTimer.Start();
    }

    // Token: 0x04000E5C RID: 3676
    public const string DrivingNote = "Hands off the mouse while I drive. Esc stops me.";

    // Token: 0x04000E5D RID: 3677
    [Nullable(2)]
    private OverlayWindow window;

    // Token: 0x04000E5E RID: 3678
    [Nullable(2)]
    private TakeOverConsentCard.DispatcherQueueTimerHandle timeout;

    // Token: 0x04000E5F RID: 3679
    private int generation;

    // Token: 0x04000E60 RID: 3680
    [Nullable(2)]
    private Action acceptForTest;

    // Token: 0x020008E5 RID: 2277
    [Nullable(0)]
    private sealed class DispatcherQueueTimerHandle
    {
      // Token: 0x06003AE3 RID: 15075 RVA: 0x00692894 File Offset: 0x00692894
      public DispatcherQueueTimerHandle(DispatcherQueueTimer timer)
      {
      }

      // Token: 0x06003AE4 RID: 15076 RVA: 0x006928A4 File Offset: 0x006928A4
      public void Stop()
      {
        this.<timer>P.Stop();
      }

      // Token: 0x04002593 RID: 9619
      [CompilerGenerated]
      private DispatcherQueueTimer <timer>P = timer;
    }
  }
}
