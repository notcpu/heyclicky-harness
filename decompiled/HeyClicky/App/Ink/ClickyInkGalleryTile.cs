using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.App.Home;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000242 RID: 578
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkGalleryTile
  {
    // Token: 0x060019DE RID: 6622 RVA: 0x005DAED4 File Offset: 0x005DAED4
    public static FrameworkElement Make(ClickyInkLiveNote.Block block, ClickyInkLiveNote.Item item, int index, double width, ClickyInkStyle style, ClickyInkActions actions, bool isPicked, bool canPick, Action onPick)
    {
      ClickyInkGalleryTile.State state = InkObjectState.For<ClickyInkGalleryTile.State>(block, "gallery-tile|" + item.Id);
      if (!state.Subscribed)
      {
        state.Subscribed = true;
        ClickyInkAudioPlayer.Shared.Changed += state.Changed;
      }
      return new InkObjectHost(state, () => ClickyInkGalleryTile.Build(item, index, width, style, actions, isPicked, canPick, onPick, state));
    }

    // Token: 0x060019DF RID: 6623 RVA: 0x005DAF90 File Offset: 0x005DAF90
    private static FrameworkElement Build(ClickyInkLiveNote.Item item, int index, double width, ClickyInkStyle style, ClickyInkActions actions, bool isPicked, bool canPick, Action onPick, ClickyInkGalleryTile.State state)
    {
      ClickyInkGalleryTile.<>c__DisplayClass3_0 CS$<>8__locals1 = new ClickyInkGalleryTile.<>c__DisplayClass3_0();
      CS$<>8__locals1.actions = actions;
      CS$<>8__locals1.state = state;
      CS$<>8__locals1.rawPath = item.Source.Url ?? "";
      bool flag = ClickyInkGalleryRules.IsWebLink(CS$<>8__locals1.rawPath);
      CS$<>8__locals1.resolvedPath = (flag ? CS$<>8__locals1.rawPath : CS$<>8__locals1.actions.ResolvePath(CS$<>8__locals1.rawPath));
      bool flag2 = ClickyInkGalleryRules.IsAudio(CS$<>8__locals1.resolvedPath);
      CS$<>8__locals1.tilt = ClickyInkGalleryRules.Tilt(index);
      double num = width - 14.0;
      CS$<>8__locals1.isOut = item.Source.Mark.GetValueOrDefault() == ClickyInkDocument.Mark.Gone;
      if (!flag && CS$<>8__locals1.resolvedPath.Length > 0 && !flag2 && CS$<>8__locals1.state.LoadingPath != CS$<>8__locals1.resolvedPath)
      {
        CS$<>8__locals1.state.LoadingPath = CS$<>8__locals1.resolvedPath;
        CS$<>8__locals1.state.Thumbnail = ArtifactThumbnails.Cached(CS$<>8__locals1.resolvedPath);
        string resolvedPath = CS$<>8__locals1.resolvedPath;
        int num2 = (int)Math.Ceiling(num * 2.0);
        ClickyInkGalleryTile.LoadThumbnail(resolvedPath, num2, CS$<>8__locals1.state);
      }
      Grid grid = new Grid
      {
        Width = num,
        Height = num * 0.78
      };
      grid.Clip = new RectangleGeometry
      {
        Rect = new Rect(0.0, 0.0, num, num * 0.78)
      };
      grid.Children.Add(ClickyInkGalleryTile.Preview(CS$<>8__locals1.rawPath, CS$<>8__locals1.resolvedPath, flag, flag2, CS$<>8__locals1.state));
      if (flag2)
      {
        bool flag3 = ClickyInkAudioPlayer.Shared.PlayingPath == CS$<>8__locals1.resolvedPath;
        FrameworkElement frameworkElement = InkViews.Symbol(flag3 ? "pause.fill" : "play.fill", 16.0, PaperColor.White, true) ?? new Grid();
        Pressable pressable = InkViews.PlainButton(new Grid
        {
          Width = 40.0,
          Height = 40.0,
          Children = 
          {
            new Ellipse
            {
              Fill = Ui.Brush(PaperColor.Black.Opacity(0.55))
            },
            frameworkElement
          }
        }, delegate
        {
          if (ClickyInkAudioPlayer.Shared.PlayingPath != CS$<>8__locals1.resolvedPath)
          {
            CS$<>8__locals1.actions.Track("gallery_audio_played", ClickyInkDocument.Kind.Gallery, new JsonObject(null));
          }
          ClickyInkAudioPlayer.Shared.Toggle(CS$<>8__locals1.resolvedPath);
        }, flag3 ? "Pause" : "Play");
        pressable.HorizontalAlignment = HorizontalAlignment.Center;
        pressable.VerticalAlignment = VerticalAlignment.Center;
        grid.Children.Add(pressable);
      }
      InkTextView inkTextView = InkViews.Plain((item.Source.Text.Length == 0) ? " " : ClickyInkNoteActions.Plain(item.Source.Text), ClickyPaperFonts.Hand((width < 150.0) ? 13.5 : 15.5), ClickyInkGalleryTile.Ink.Opacity(CS$<>8__locals1.isOut ? 0.4 : 1.0), delegate(InkTextView t)
      {
        t.TextAlignment = TextAlignment.Center;
        t.LineLimit = new int?(2);
        if (CS$<>8__locals1.isOut)
        {
          t.Strikethrough = new Color?(ClickyInkGalleryTile.Ink.Opacity(0.4));
        }
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView.VerticalAlignment = VerticalAlignment.Center;
      Grid grid2 = new Grid
      {
        Width = num,
        Height = 38.0,
        Children = { inkTextView }
      };
      StackPanel stackPanel = InkViews.VStack(0.0, new UIElement[] { grid, grid2 });
      stackPanel.Margin = new Thickness(7.0, 7.0, 7.0, 0.0);
      CS$<>8__locals1.paper = new InkPaperSheet("gallery-polaroid", ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#FEFDF9")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.08), 2.0, 0.0, 1.0)
      }, null, null);
      CS$<>8__locals1.lifted = new InkPaperSheet("gallery-polaroid-up", ClickyShape.Rectangle, PaperFill.Of(PaperColor.Hex("#FEFDF9")), new double?(0.2), new PaperShadow[]
      {
        new PaperShadow(PaperColor.Black.Opacity(0.15), 5.0, 0.0, 3.0)
      }, null, null)
      {
        Opacity = 0.0
      };
      List<UIElement> list = new List<UIElement>();
      FrameworkElement frameworkElement2 = ClickyInkMaskingTape.Make(Math.Min(54.0, width * 0.4), -CS$<>8__locals1.tilt * 1.6, null);
      frameworkElement2.HorizontalAlignment = HorizontalAlignment.Center;
      frameworkElement2.VerticalAlignment = VerticalAlignment.Top;
      frameworkElement2.Transformed(0.0, -8.0, 0.0, 1.0, null);
      list.Add(frameworkElement2);
      if (isPicked)
      {
        ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyWobblyRect(index + 9, 2.2), new PaperStroke(style.Fresh.Opacity(0.7), 2.0, PenLineCap.Round, PenLineJoin.Round, null));
        clickyShapeView.Margin = new Thickness(-6.0);
        list.Add(clickyShapeView);
      }
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, list.ToArray()).Behind(CS$<>8__locals1.lifted).Behind(CS$<>8__locals1.paper);
      if (CS$<>8__locals1.isOut)
      {
        clickyInkOverlay.Opacity = 0.7;
        grid.Children.Add(new Border
        {
          Background = Ui.Brush(PaperColor.Hex("#8A8A8A").Opacity(0.55)),
          IsHitTestVisible = false
        });
      }
      CS$<>8__locals1.rotate = new RotateTransform();
      CS$<>8__locals1.scale = new ScaleTransform();
      clickyInkOverlay.RenderTransform = new TransformGroup
      {
        Children = { CS$<>8__locals1.scale, CS$<>8__locals1.rotate }
      };
      clickyInkOverlay.RenderTransformOrigin = new Point(0.5, 0.5);
      CS$<>8__locals1.<Build>g__ApplyHover|1();
      Pressable pressable2 = InkViews.PlainButton(clickyInkOverlay, delegate
      {
        CS$<>8__locals1.actions.Open(CS$<>8__locals1.rawPath);
      }, ClickyInkNoteActions.Plain(item.Source.Text));
      pressable2.HorizontalAlignment = HorizontalAlignment.Center;
      pressable2.HoverChanged += delegate(bool h)
      {
        CS$<>8__locals1.state.IsHovered = h;
        CS$<>8__locals1.state.HoverChangedAt = InkObjectState.Now;
        double until = InkObjectState.Now + InkSpring.SettleSeconds(0.3, 0.75);
        EventHandler<object> tick = null;
        tick = delegate([Nullable(2)] object _, object _)
        {
          CS$<>8__locals1.<Build>g__ApplyHover|1();
          if (InkObjectState.Now >= until)
          {
            CompositionTarget.Rendering -= tick;
          }
        };
        CompositionTarget.Rendering += tick;
      };
      if (CS$<>8__locals1.rawPath.Length == 0)
      {
        pressable2.IsHitTestVisible = false;
      }
      FrameworkElement frameworkElement3 = null;
      if (isPicked)
      {
        frameworkElement3 = ClickyInkRubberStamp.Make("picked", new Color?(style.Fresh), -3.0);
      }
      else if (canPick)
      {
        frameworkElement3 = new ClickyPaperGelButton("Pick this", onPick, ClickyPaperGel.Blue, 24.0, 11.0, 11.5, null);
      }
      if (frameworkElement3 != null)
      {
        frameworkElement3.HorizontalAlignment = HorizontalAlignment.Center;
      }
      Grid grid3 = new Grid
      {
        Width = width
      };
      if (frameworkElement3 != null)
      {
        grid3.Children.Add(frameworkElement3);
      }
      StackPanel stackPanel2 = InkViews.VStack(9.0, new UIElement[] { pressable2, grid3 });
      stackPanel2.Width = width;
      return stackPanel2;
    }

    // Token: 0x060019E0 RID: 6624 RVA: 0x005DB6F0 File Offset: 0x005DB6F0
    private static async Task LoadThumbnail(string path, int pixels, ClickyInkGalleryTile.State state)
    {
      ImageSource imageSource = await ArtifactThumbnails.GetAsync(path, pixels);
      if (!(state.LoadingPath != path))
      {
        state.Thumbnail = imageSource;
        state.Changed();
      }
    }

    // Token: 0x060019E1 RID: 6625 RVA: 0x005DB744 File Offset: 0x005DB744
    private static FrameworkElement Preview(string rawPath, string resolvedPath, bool isWebLink, bool isAudio, ClickyInkGalleryTile.State state)
    {
      if (ClickyInkGalleryRules.IsWebImage(rawPath))
      {
        Grid grid = new Grid();
        Grid placeholder = new Grid
        {
          Background = Ui.Brush(PaperColor.Black.Opacity(0.06)),
          Children = { InkViews.Spinner(16.0) }
        };
        ((FrameworkElement)placeholder.Children[0]).HorizontalAlignment = HorizontalAlignment.Center;
        grid.Children.Add(placeholder);
        BitmapImage bitmapImage = new BitmapImage(new Uri(rawPath));
        Image image = new Image
        {
          Source = bitmapImage,
          Stretch = Stretch.UniformToFill,
          Opacity = 0.0
        };
        bitmapImage.ImageOpened += delegate(object _, RoutedEventArgs _)
        {
          image.Opacity = 1.0;
          placeholder.Visibility = Visibility.Collapsed;
        };
        grid.Children.Add(image);
        return grid;
      }
      if (isWebLink)
      {
        return ClickyInkLinkPreviewPlate.Make(rawPath, 32.0, null);
      }
      ImageSource thumbnail = state.Thumbnail;
      if (thumbnail != null)
      {
        return new Image
        {
          Source = thumbnail,
          Stretch = Stretch.UniformToFill,
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center
        };
      }
      NotchDocumentStyle notchDocumentStyle = NotchDocumentStyle.For(resolvedPath, false);
      FrameworkElement frameworkElement;
      if (isAudio)
      {
        frameworkElement = InkViews.Symbol("waveform", 26.0, notchDocumentStyle.Tint, true) ?? new Grid();
      }
      else
      {
        TextBlock textBlock = Icons.Glyph(notchDocumentStyle.Glyph, 26.0, notchDocumentStyle.Tint);
        textBlock.FontWeight = FontWeights.Bold;
        frameworkElement = textBlock;
      }
      return new Grid
      {
        Background = Ui.Brush(notchDocumentStyle.Tint.Opacity(0.14)),
        Children = { frameworkElement }
      };
    }

    // Token: 0x04000C78 RID: 3192
    private static readonly Color Ink = PaperColor.Hex("#2B2620");

    // Token: 0x020007FF RID: 2047
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class State : InkObjectState
    {
      // Token: 0x0400222D RID: 8749
      public bool IsHovered;

      // Token: 0x0400222E RID: 8750
      public double HoverChangedAt = -1000.0;

      // Token: 0x0400222F RID: 8751
      public ImageSource Thumbnail;

      // Token: 0x04002230 RID: 8752
      public string LoadingPath;

      // Token: 0x04002231 RID: 8753
      public bool Subscribed;
    }
  }
}
