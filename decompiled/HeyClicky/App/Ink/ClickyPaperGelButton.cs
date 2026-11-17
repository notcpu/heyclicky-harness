using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;
using Windows.UI.Text;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001D3 RID: 467
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyPaperGelButton : Pressable
  {
    // Token: 0x060017D9 RID: 6105 RVA: 0x005C7250 File Offset: 0x005C7250
    [NullableContext(2)]
    public ClickyPaperGelButton([Nullable(1)] string title, Action action = null, ClickyPaperGel gel = ClickyPaperGel.Blue, double height = 26.0, double horizontalPadding = 13.0, double fontSize = 12.0, string systemImage = null)
      : base(true)
    {
      this.gel = gel;
      this.fontSize = fontSize;
      base.Height = height;
      base.HorizontalAlignment = HorizontalAlignment.Left;
      base.VerticalAlignment = VerticalAlignment.Top;
      this.scaled.Children.Add(new ClickyPaperGelPlate(gel, true));
      this.label = ClickyPaperFonts.Typed(fontSize, new FontWeight?(FontWeights.SemiBold)).Apply(new TextBlock
      {
        Text = title,
        Foreground = Ui.Brush(gel.Words()),
        TextWrapping = TextWrapping.NoWrap
      });
      this.row = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Spacing = 4.0,
        Margin = new Thickness(horizontalPadding, 0.0, horizontalPadding, 0.0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
      this.SetSymbol(systemImage);
      this.row.Children.Add(Ui.Embossed(this.label, 0.6, 1.0, 1.0));
      this.scaled.Children.Add(this.row);
      base.Children.Add(this.scaled);
      if (action != null)
      {
        base.Click += action;
      }
      base.HoverChanged += delegate(bool _)
      {
        this.Squash(0.12);
      };
      base.PressedChanged += delegate(bool _)
      {
        this.Squash(0.08);
      };
      AutomationProperties.SetName(this, title);
    }

    // Token: 0x060017DA RID: 6106 RVA: 0x005C73D8 File Offset: 0x005C73D8
    private void Squash(double seconds)
    {
      this.scaled.CenterPoint = new Vector3((float)base.ActualWidth / 2f, (float)base.ActualHeight / 2f, 0f);
      Anim.Scale(this.scaled, base.IsPressed ? 0.96f : (base.IsHovered ? 1.03f : 1f), seconds, Easing.EaseOut(Ui.Compositor), null);
    }

    // Token: 0x17000440 RID: 1088
    // (get) Token: 0x060017DB RID: 6107 RVA: 0x005C7458 File Offset: 0x005C7458
    // (set) Token: 0x060017DC RID: 6108 RVA: 0x005C7468 File Offset: 0x005C7468
    public string Title
    {
      get
      {
        return this.label.Text;
      }
      set
      {
        this.label.Text = value;
        UIElementCollection children = this.row.Children;
        Grid grid = children[children.Count - 1] as Grid;
        if (grid != null)
        {
          foreach (TextBlock textBlock in grid.Children.OfType<TextBlock>())
          {
            textBlock.Text = value;
          }
        }
      }
    }

    // Token: 0x060017DD RID: 6109 RVA: 0x005C74E8 File Offset: 0x005C74E8
    public void AnimateTitle(string title, [Nullable(2)] string systemImage, bool spring)
    {
      double actualWidth = base.ActualWidth;
      this.Title = title;
      this.SetSymbol(systemImage);
      AutomationProperties.SetName(this, title);
      if (actualWidth <= 0.0 || Anim.Instant || Anim.ReduceMotion)
      {
        return;
      }
      base.Measure(new Size(double.PositiveInfinity, base.ActualHeight));
      double width = base.DesiredSize.Width;
      if (spring)
      {
        this.row.Opacity = 0.0;
        Anim.OpacitySpring(this.row, 1f, new SwiftSpring(0.3, 0.8));
      }
      else
      {
        this.row.Opacity = 0.0;
        Anim.Opacity(this.row, 1f, 0.25, Easing.EaseOut(Ui.Compositor), 0.0);
      }
      if (Math.Abs(width - actualWidth) < 0.5)
      {
        return;
      }
      DoubleAnimation doubleAnimation = new DoubleAnimation
      {
        From = new double?(actualWidth),
        To = new double?(width),
        EnableDependentAnimation = true,
        Duration = TimeSpan.FromSeconds(spring ? 0.35 : 0.25),
        EasingFunction = new CubicEase
        {
          EasingMode = EasingMode.EaseOut
        }
      };
      Storyboard.SetTarget(doubleAnimation, this);
      Storyboard.SetTargetProperty(doubleAnimation, "Width");
      Storyboard storyboard = new Storyboard
      {
        Children = { doubleAnimation }
      };
      storyboard.Completed += delegate([Nullable(2)] object _, object _)
      {
        storyboard.Stop();
      };
      storyboard.Begin();
    }

    // Token: 0x060017DE RID: 6110 RVA: 0x005C769C File Offset: 0x005C769C
    [NullableContext(2)]
    public void SetSymbol(string systemImage)
    {
      if (this.row.Children.Count > 0)
      {
        TextBlock textBlock = this.row.Children[0] as TextBlock;
        if (textBlock != null)
        {
          string text = textBlock.Tag as string;
          if (text != null && text == "symbol")
          {
            this.row.Children.Remove(textBlock);
          }
        }
      }
      if (systemImage != null)
      {
        TextBlock textBlock2 = ClickyInkIcon.Glyph(systemImage, this.fontSize - 1.0, this.gel.Words(), true);
        if (textBlock2 != null)
        {
          textBlock2.Tag = "symbol";
          this.row.Children.Insert(0, textBlock2);
        }
      }
    }

    // Token: 0x04000BAD RID: 2989
    private readonly TextBlock label;

    // Token: 0x04000BAE RID: 2990
    private readonly StackPanel row;

    // Token: 0x04000BAF RID: 2991
    private readonly Grid scaled = new Grid();

    // Token: 0x04000BB0 RID: 2992
    private readonly ClickyPaperGel gel;

    // Token: 0x04000BB1 RID: 2993
    private readonly double fontSize;
  }
}
