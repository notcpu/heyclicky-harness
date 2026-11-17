using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.Home;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000273 RID: 627
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkSupportingFiles
  {
    // Token: 0x06001AC9 RID: 6857 RVA: 0x005E6D84 File Offset: 0x005E6D84
    public static FrameworkElement Create(List<ClickyInkLiveNote.Block> blocks, double clock, ClickyInkStyle style, ClickyInkActions actions)
    {
      ClickyInkSupportingFiles.<>c__DisplayClass2_0 CS$<>8__locals1 = new ClickyInkSupportingFiles.<>c__DisplayClass2_0();
      CS$<>8__locals1.blocks = blocks;
      CS$<>8__locals1.clock = clock;
      CS$<>8__locals1.style = style;
      CS$<>8__locals1.actions = actions;
      CS$<>8__locals1.inkWidth = ClickyInkEnvironment.Current.ContentWidth;
      CS$<>8__locals1.host = new Grid
      {
        Width = CS$<>8__locals1.inkWidth,
        HorizontalAlignment = HorizontalAlignment.Left
      };
      CS$<>8__locals1.key = ((CS$<>8__locals1.blocks.Count > 0) ? CS$<>8__locals1.blocks[0].Id : "");
      CS$<>8__locals1.<Create>g__Build|0();
      return CS$<>8__locals1.host;
    }

    // Token: 0x06001ACA RID: 6858 RVA: 0x005E6E1C File Offset: 0x005E6E1C
    private static FrameworkElement Row(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style, ClickyInkActions actions, double width)
    {
      ClickyInkSupportingFiles.<>c__DisplayClass4_0 CS$<>8__locals1 = new ClickyInkSupportingFiles.<>c__DisplayClass4_0();
      CS$<>8__locals1.actions = actions;
      CS$<>8__locals1.entry = ClickyInkFileEntry.Of(block, CS$<>8__locals1.actions);
      bool flag = block.IsStruck(clock);
      CS$<>8__locals1.thumbnail = new Grid
      {
        Width = 26.0,
        Height = 26.0,
        CornerRadius = new CornerRadius(4.0)
      };
      if (!CS$<>8__locals1.entry.IsWebLink)
      {
        ImageSource imageSource = ArtifactThumbnails.Cached(CS$<>8__locals1.entry.ResolvedPath);
        if (imageSource != null)
        {
          CS$<>8__locals1.<Row>g__ShowImage|0(imageSource);
          goto IL_015C;
        }
      }
      CS$<>8__locals1.thumbnail.Children.Add(new Border
      {
        Background = Ui.Brush(CS$<>8__locals1.entry.Style.Tint.Opacity(0.14))
      });
      TextBlock textBlock = Icons.Glyph(CS$<>8__locals1.entry.Style.Glyph, 11.0, CS$<>8__locals1.entry.Style.Tint);
      textBlock.FontWeight = FontWeights.SemiBold;
      CS$<>8__locals1.thumbnail.Children.Add(textBlock);
      if (!CS$<>8__locals1.entry.IsWebLink && CS$<>8__locals1.entry.ResolvedPath.Length > 0)
      {
        ClickyInkFileCard.LoadThumbnail(CS$<>8__locals1.entry.ResolvedPath, 52, new Action<ImageSource>(CS$<>8__locals1.<Row>g__ShowImage|0));
      }
      IL_015C:
      Grid grid = new Grid
      {
        Width = 26.0,
        Height = 26.0,
        Children = { CS$<>8__locals1.thumbnail }
      };
      grid.Children.Add(ClickyShapeView.StrokeBorder(ClickyShape.RoundedRectangle(4.0), new PaperStroke(PaperColor.Black.Opacity(0.14), 0.5, PenLineCap.Flat, PenLineJoin.Miter, null)));
      PaperFont paperFont = ClickyPaperFonts.Typed(12.5, new FontWeight?(FontWeights.Medium));
      InkTextView inkTextView = InkTextView.Plain(CS$<>8__locals1.entry.Style.KindLabel, ClickyPaperFonts.Typed(11.0, null), style.Quiet).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      double num = Math.Max(0.0, width - 8.0 - 26.0 - 8.0 - 8.0);
      InkTextView inkTextView2 = InkTextView.Plain(ClickyInkMiddleTruncation.Fit(CS$<>8__locals1.entry.Name, paperFont, num), paperFont, flag ? style.Quiet : style.Ink).With(delegate(InkTextView t)
      {
        t.LineLimit = new int?(1);
      });
      if (flag)
      {
        inkTextView2.Strikethrough = new Color?(style.Fresh);
      }
      ClickyInkHStack clickyInkHStack = new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
      {
        grid,
        inkTextView2,
        inkTextView,
        new ClickyInkSpacer()
      })
      {
        Margin = new Thickness(4.0, 2.0, 4.0, 2.0)
      };
      CS$<>8__locals1.hover = ClickyShapeView.Fill(ClickyShape.RoundedRectangle(6.0), style.Ink.Opacity(0.06));
      CS$<>8__locals1.hover.Opacity = 0.0;
      Pressable pressable = new Pressable(true);
      pressable.Children.Add(CS$<>8__locals1.hover);
      pressable.Children.Add(clickyInkHStack);
      pressable.Enabled = CS$<>8__locals1.entry.RawPath.Length > 0;
      pressable.HoverChanged += delegate(bool hovering)
      {
        CS$<>8__locals1.hover.Opacity = hovering > false;
      };
      pressable.Click += delegate
      {
        if (CS$<>8__locals1.entry.RawPath.Length > 0)
        {
          CS$<>8__locals1.actions.Open(CS$<>8__locals1.entry.RawPath);
        }
      };
      ToolTipService.SetToolTip(pressable, "Open " + CS$<>8__locals1.entry.Name);
      AutomationProperties.SetName(pressable, "Open " + CS$<>8__locals1.entry.Name + ", " + CS$<>8__locals1.entry.Style.KindLabel);
      ClickyInkObjectKit.FileMenu(pressable, CS$<>8__locals1.entry.RawPath, CS$<>8__locals1.entry.ResolvedPath, CS$<>8__locals1.entry.IsWebLink, delegate
      {
        CS$<>8__locals1.actions.Open(CS$<>8__locals1.entry.RawPath);
      });
      return pressable;
    }

    // Token: 0x04000CD4 RID: 3284
    private const int CollapsedRowCount = 6;

    // Token: 0x04000CD5 RID: 3285
    private static readonly HashSet<string> ShowingAll = new HashSet<string>();

    // Token: 0x04000CD6 RID: 3286
    private const double ThumbnailSide = 26.0;
  }
}
