using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Design;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000271 RID: 625
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkFileCard
  {
    // Token: 0x06001AC1 RID: 6849 RVA: 0x005E6430 File Offset: 0x005E6430
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      ClickyInkFileEntry clickyInkFileEntry = ClickyInkFileEntry.Of(block, actions);
      FrameworkElement frameworkElement = ClickyInkFileCard.Card(block, clickyInkFileEntry, actions);
      InkTextView inkTextView = ClickyInkWordsView.Make(block.Words, clock, style, ClickyPaperFonts.Hand(16.0), true, new Color?(style.Quiet), null).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(5);
      });
      inkTextView.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
      return ClickyInkObjectKit.Leading(new ClickyInkHStack(14.0, InkVerticalAlignment.Top, new UIElement[]
      {
        frameworkElement,
        ClickyInkHStack.Flexible<InkTextView>(inkTextView)
      }), contentWidth);
    }

    // Token: 0x06001AC2 RID: 6850 RVA: 0x005E6508 File Offset: 0x005E6508
    private static FrameworkElement Card(ClickyInkLiveNote.Block block, ClickyInkFileEntry entry, ClickyInkActions actions)
    {
      Grid grid = new Grid
      {
        Width = 182.0,
        Height = 124.0,
        CornerRadius = new CornerRadius(6.0)
      };
      grid.Children.Add(ClickyInkFileCard.Preview(block, entry));
      Grid grid2 = new Grid
      {
        Width = 182.0,
        Height = 124.0,
        Children = { grid }
      };
      grid2.Children.Add(ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(6.0), new PaperStroke(PaperColor.Black.Opacity(0.22), 1.0, PenLineCap.Flat, PenLineJoin.Miter, null)));
      PaperFont paperFont = ClickyPaperFonts.Typed(12.5, new FontWeight?(FontWeights.Bold));
      InkTextView inkTextView = InkTextView.Plain(ClickyInkMiddleTruncation.Fit(entry.Name, paperFont, 176.0), paperFont, ClickyInkFileCard.CardInk).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      StackPanel stackPanel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0
      };
      TextBlock textBlock = Icons.Glyph(entry.Style.Glyph, 9.5, entry.Style.Tint);
      textBlock.FontWeight = FontWeights.SemiBold;
      stackPanel.Children.Add(entry.IsWebLink ? ClickyInkFileCard.WithCorners(ClickyInkObjectKit.Favicon(entry.RawPath, 11.0, textBlock), 2.5) : textBlock);
      stackPanel.Children.Add(InkTextView.Plain(entry.Style.KindLabel + " · Open", ClickyPaperFonts.Typed(10.5, new FontWeight?(FontWeights.Medium)), ClickyInkFileCard.CardInk.Opacity(0.5)).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      }));
      StackPanel stackPanel2 = new StackPanel
      {
        Spacing = 3.0,
        Margin = new Thickness(3.0, 4.0, 3.0, 6.0),
        Children = { inkTextView, stackPanel }
      };
      StackPanel stackPanel3 = new StackPanel();
      stackPanel3.Spacing = 7.0;
      stackPanel3.Padding = new Thickness(7.0);
      stackPanel3.Width = 196.0;
      stackPanel3.Children.Add(grid2);
      stackPanel3.Children.Add(stackPanel2);
      IClickyShape clickyShape = ClickyShape.RoundedRectangle(10.0);
      ClickyInkShadowedShape restingShadow = new ClickyInkShadowedShape(clickyShape, "rr10", PaperColor.White, new PaperShadow(PaperColor.Black.Opacity(0.16), 5.0, 0.0, 3.0), null);
      ClickyInkShadowedShape hoverShadow = new ClickyInkShadowedShape(clickyShape, "rr10", PaperColor.White, new PaperShadow(PaperColor.Black.Opacity(0.24), 10.0, 0.0, 6.0), null)
      {
        Opacity = 0.0
      };
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel3, new UIElement[] { ClickyShapeView.StrokeBorder(clickyShape, new PaperStroke(PaperColor.Black.Opacity(0.07), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null)) }).Behind(hoverShadow).Behind(restingShadow);
      Pressable button = new Pressable(true)
      {
        Children = { clickyInkOverlay },
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Top
      };
      button.Enabled = entry.RawPath.Length > 0;
      button.Click += delegate
      {
        if (entry.RawPath.Length > 0)
        {
          actions.Open(entry.RawPath);
        }
      };
      ToolTipService.SetToolTip(button, "Open " + entry.Name);
      AutomationProperties.SetName(button, "Open " + entry.Name + ", " + entry.Style.KindLabel);
      ClickyInkObjectKit.FileMenu(button, entry.RawPath, entry.ResolvedPath, entry.IsWebLink, delegate
      {
        actions.Open(entry.RawPath);
      });
      button.Rotation = -1.5f;
      button.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        button.CenterPoint = new Vector3((float)button.ActualWidth / 2f, (float)button.ActualHeight / 2f, 0f);
      };
      SwiftSpring spring = new SwiftSpring(0.28, 0.8);
      button.HoverChanged += delegate(bool hovering)
      {
        Anim.ScaleSpring(button, hovering ? 1.03f : 1f, spring, null);
        Anim.Rotate(button, hovering ? 0f : (-1.5f), 0.28, null);
        Anim.Opacity(hoverShadow, hovering > false, 0.28, null, 0.0);
        Anim.Opacity(restingShadow, !hovering, 0.28, null, 0.0);
      };
      return button;
    }

    // Token: 0x06001AC3 RID: 6851 RVA: 0x005E6A68 File Offset: 0x005E6A68
    private static FrameworkElement WithCorners(FrameworkElement view, double radius)
    {
      return new Grid
      {
        CornerRadius = new CornerRadius(radius),
        Children = { view }
      };
    }

    // Token: 0x06001AC4 RID: 6852 RVA: 0x005E6A88 File Offset: 0x005E6A88
    private static FrameworkElement Preview(ClickyInkLiveNote.Block block, ClickyInkFileEntry entry)
    {
      ClickyInkFileCard.<>c__DisplayClass6_0 CS$<>8__locals1 = new ClickyInkFileCard.<>c__DisplayClass6_0();
      if (entry.IsWebLink)
      {
        return ClickyInkLinkPreviewPlate.Make(entry.RawPath, 40.0, null);
      }
      CS$<>8__locals1.host = new Grid();
      ClickyInkPicture clickyInkPicture = ClickyInkImages.Image(block.Source["thumb"]);
      if (clickyInkPicture != null)
      {
        CS$<>8__locals1.<Preview>g__ShowImage|0(clickyInkPicture.Color);
        return CS$<>8__locals1.host;
      }
      ImageSource imageSource = ArtifactThumbnails.Cached(entry.ResolvedPath);
      if (imageSource != null)
      {
        CS$<>8__locals1.<Preview>g__ShowImage|0(imageSource);
        return CS$<>8__locals1.host;
      }
      CS$<>8__locals1.host.Children.Add(new Border
      {
        Background = Ui.Brush(entry.Style.Tint.Opacity(0.14))
      });
      TextBlock textBlock = Icons.Glyph(entry.Style.Glyph, 30.0, entry.Style.Tint);
      textBlock.FontWeight = FontWeights.SemiBold;
      CS$<>8__locals1.host.Children.Add(textBlock);
      CS$<>8__locals1.dispatcher = DispatcherQueue.GetForCurrentThread();
      string text = block.Source["thumb"];
      if (text != null)
      {
        ClickyInkImages.LoadAsync(text).ContinueWith(delegate([Nullable(new byte[] { 1, 2 })] Task<ClickyInkPicture> t)
        {
          if (t.IsCompletedSuccessfully)
          {
            ClickyInkPicture p = t.Result;
            if (p != null)
            {
              CS$<>8__locals1.dispatcher.TryEnqueue(delegate
              {
                CS$<>8__locals1.<Preview>g__ShowImage|0(p.Color);
              });
            }
          }
        }, TaskScheduler.Default);
      }
      else if (entry.ResolvedPath.Length > 0)
      {
        ClickyInkFileCard.LoadThumbnail(entry.ResolvedPath, (int)Math.Ceiling(364.0), new Action<ImageSource>(CS$<>8__locals1.<Preview>g__ShowImage|0));
      }
      return CS$<>8__locals1.host;
    }

    // Token: 0x06001AC5 RID: 6853 RVA: 0x005E6C14 File Offset: 0x005E6C14
    internal static async Task LoadThumbnail(string path, int pixelWidth, Action<ImageSource> show)
    {
      try
      {
        ImageSource imageSource = await ArtifactThumbnails.GetAsync(path, pixelWidth);
        if (imageSource != null)
        {
          show(imageSource);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("ink", "file thumbnail " + Path.GetFileName(path) + ": " + ex.Message);
      }
    }

    // Token: 0x04000CD1 RID: 3281
    public const double CardWidth = 196.0;

    // Token: 0x04000CD2 RID: 3282
    private const double PreviewHeight = 124.0;

    // Token: 0x04000CD3 RID: 3283
    private static readonly Color CardInk = Color.FromArgb(byte.MaxValue, 28, 30, 36);
  }
}
