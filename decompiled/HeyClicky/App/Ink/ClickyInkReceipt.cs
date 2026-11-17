using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x02000269 RID: 617
  public static class ClickyInkReceipt
  {
    // Token: 0x06001A9F RID: 6815 RVA: 0x005E4E78 File Offset: 0x005E4E78
    [NullableContext(1)]
    public static FrameworkElement Create(ClickyInkLiveNote.Block block, double clock, ClickyInkStyle style)
    {
      double contentWidth = ClickyInkEnvironment.Current.ContentWidth;
      double num = ClickyInkReceiptRules.Width(block.Source);
      StackPanel stackPanel = new StackPanel
      {
        Spacing = 5.0,
        Width = num,
        Padding = new Thickness(16.0, 14.0, 16.0, 20.0)
      };
      InkTextView inkTextView = InkTextView.Plain(((block.Words.Current.Length == 0) ? "Receipt" : block.Words.Current).ToUpperInvariant(), ClickyInkObjectFonts.Mono(11.0, new FontWeight?(FontWeights.Bold)), ClickyInkReceipt.Ink).With(delegate(InkTextView t)
      {
        t.Tracking = 1.5;
      });
      inkTextView.HorizontalAlignment = HorizontalAlignment.Center;
      inkTextView.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
      stackPanel.Children.Add(inkTextView);
      PaperFont paperFont = ClickyInkObjectFonts.Mono(11.5, null);
      foreach (ClickyInkLiveNote.Item item in block.Items)
      {
        if (!ClickyInkReceiptRules.IsTotalLine(item.Source) && clock >= item.LandsAt)
        {
          double? goneAt = item.GoneAt;
          bool flag;
          if (goneAt != null)
          {
            double valueOrDefault = goneAt.GetValueOrDefault();
            flag = clock >= valueOrDefault;
          }
          else
          {
            flag = false;
          }
          bool flag2 = flag;
          InkTextView inkTextView2 = InkTextView.Plain((item.Source.Cells.Count > 0) ? item.Source.Cells[0] : item.Source.Text, paperFont, ClickyInkReceipt.Ink).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(3);
          });
          if (flag2)
          {
            inkTextView2.Strikethrough = new Color?(style.Fresh);
          }
          InkTextView inkTextView3 = null;
          bool flag3 = false;
          if (item.Cells.Count > 1)
          {
            ClickyInkLiveNote.Words words = item.Cells[1];
            flag3 = words.Current.CharacterCount() > 12;
            inkTextView3 = new InkTextView(ClickyInkWords.Text(words, clock, style, false, new Color?(ClickyInkReceipt.Ink), null), paperFont, new Color?(ClickyInkReceipt.Ink));
            if (flag3)
            {
              inkTextView3.LineLimit = new int?(2);
              inkTextView3.TextAlignment = TextAlignment.Right;
            }
            if (flag2)
            {
              inkTextView3.Strikethrough = new Color?(style.Fresh);
            }
          }
          stackPanel.Children.Add(new ClickyInkReceiptLine(inkTextView2, inkTextView3, flag3 ? new double?((num - 32.0) * 0.48) : null));
        }
      }
      string text = ClickyInkReceiptRules.Total(block.Source);
      if (text != null)
      {
        double num2;
        if (block.Items.Count <= 0)
        {
          num2 = block.LandsAt;
        }
        else
        {
          List<ClickyInkLiveNote.Item> items = block.Items;
          num2 = items[items.Count - 1].LandsAt + 0.3;
        }
        if (clock >= num2)
        {
          stackPanel.Children.Add(new Border
          {
            Height = 1.0,
            Margin = new Thickness(0.0, 3.0, 0.0, 0.0),
            Background = Ui.Brush(ClickyInkReceipt.Ink.Opacity(0.6))
          });
          InkTextView inkTextView4 = InkTextView.Plain(text, ClickyInkObjectFonts.Mono(15.0, new FontWeight?(FontWeights.ExtraBold)), ClickyInkReceipt.Ink).With(delegate(InkTextView t)
          {
            t.LineLimit = new int?(1);
            t.MinimumScale = 0.7;
          });
          ClickyShapeView clickyShapeView = ClickyShapeView.Stroke(new ClickyHandRing(4, 0.16), style.Fresh.Opacity(0.7), 1.8);
          clickyShapeView.Margin = new Thickness(-5.0);
          stackPanel.Children.Add(new ClickyInkHStack(8.0, InkVerticalAlignment.Center, new UIElement[]
          {
            InkTextView.Plain("TOTAL", ClickyInkObjectFonts.Mono(12.0, new FontWeight?(FontWeights.Bold)), ClickyInkReceipt.Ink),
            new ClickyInkSpacer(),
            new ClickyInkOverlay(inkTextView4, new UIElement[] { clickyShapeView })
          }));
        }
      }
      ClickyInkOverlay clickyInkOverlay = new ClickyInkOverlay(stackPanel, Array.Empty<UIElement>()).Behind(new ClickyInkShadowedShape(new ClickyInkZigzagFoot(), "zigzag", PaperColor.Hex("#FDFDFB"), new PaperShadow(PaperColor.Black.Opacity(0.12), 3.0, 0.0, 2.0), null));
      clickyInkOverlay.Transformed(0.0, 0.0, 1.1, 1.0, null);
      FrameworkElement frameworkElement = ClickyInkObjectKit.Leading(clickyInkOverlay, contentWidth);
      frameworkElement.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
      return frameworkElement;
    }

    // Token: 0x04000CC3 RID: 3267
    private static readonly Color Ink = PaperColor.Hex("#2A2A2A");
  }
}
