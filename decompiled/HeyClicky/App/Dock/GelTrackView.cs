using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Dock
{
  // Token: 0x02000323 RID: 803
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class GelTrackView : Grid
  {
    // Token: 0x06002287 RID: 8839 RVA: 0x00624F98 File Offset: 0x00624F98
    public GelTrackView(int segmentCount, int doneCount, int? currentIndex, double height, Color ink, bool isIndeterminate = false)
    {
      this.height = height;
      this.ink = ink;
      this.segments = segmentCount;
      this.done = doneCount;
      this.current = currentIndex;
      this.indeterminate = isIndeterminate;
      base.Height = height;
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(height / 2.0),
        Background = Ui.Brush(DS.White(0.8)),
        Margin = new Thickness(0.0, 1.0, 0.0, -1.0)
      });
      base.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(height / 2.0),
        Background = Ui.Brush(GelTrackView.BlendOver(ink, 0.07)),
        BorderBrush = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, ink.WithAlpha(0.12)),
          new ValueTuple<double, Color>(1.0, ink.WithAlpha(0.04))
        }),
        BorderThickness = new Thickness(1.0)
      });
      base.Children.Add(this.layer);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Layout(false);
      };
    }

    // Token: 0x06002288 RID: 8840 RVA: 0x00625138 File Offset: 0x00625138
    private static Color BlendOver(Color ink, double alpha)
    {
      return ink.WithAlpha(alpha);
    }

    // Token: 0x06002289 RID: 8841 RVA: 0x00625144 File Offset: 0x00625144
    public void Update(int segmentCount, int doneCount, int? currentIndex)
    {
      if (segmentCount == this.segments && doneCount == this.done)
      {
        int? num = currentIndex;
        int? num2 = this.current;
        if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
        {
          return;
        }
      }
      this.segments = segmentCount;
      this.done = doneCount;
      this.current = currentIndex;
      this.Layout(true);
    }

    // Token: 0x0600228A RID: 8842 RVA: 0x006251AC File Offset: 0x006251AC
    [NullableContext(1)]
    private FrameworkElement Gel(double width, double gelHeight)
    {
      Color working = ArcColors.Working;
      Grid grid = new Grid
      {
        Width = width,
        Height = gelHeight
      };
      grid.Children.Add(new GelTrackView.Shadow(gelHeight / 2.0, working.WithAlpha(0.45), 3.0));
      grid.Children.Add(new Border
      {
        CornerRadius = new CornerRadius(gelHeight / 2.0),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, ArcColors.BlendWithWhite(working, 0.7)),
          new ValueTuple<double, Color>(0.18, ArcColors.BlendWithWhite(working, 0.3)),
          new ValueTuple<double, Color>(0.5, working),
          new ValueTuple<double, Color>(0.82, working),
          new ValueTuple<double, Color>(1.0, ArcColors.BlendWithWhite(working, 0.15))
        }),
        BorderBrush = Ui.Brush(GelTrackView.GelRim),
        BorderThickness = new Thickness(0.75)
      });
      double num = Math.Max(2.0, gelHeight * 0.42);
      grid.Children.Add(new Border
      {
        Height = num,
        VerticalAlignment = VerticalAlignment.Top,
        CornerRadius = new CornerRadius(num / 2.0),
        Margin = new Thickness(gelHeight * 0.45, Math.Max(1.0, gelHeight * 0.1), gelHeight * 0.45, 0.0),
        Background = Ui.Vertical(new ValueTuple<double, Color>[]
        {
          new ValueTuple<double, Color>(0.0, DS.White(0.85)),
          new ValueTuple<double, Color>(1.0, DS.White(0.05))
        })
      });
      return grid;
    }

    // Token: 0x0600228B RID: 8843 RVA: 0x006253E8 File Offset: 0x006253E8
    private void Layout(bool animated = false)
    {
      double actualWidth = base.ActualWidth;
      if (actualWidth <= 0.0)
      {
        return;
      }
      this.layer.Children.Clear();
      this.doneGel = (this.currentGel = (this.slider = null));
      this.shimmerClip = null;
      double num = 2.0;
      GelTrackView.<>c__DisplayClass17_0 CS$<>8__locals1;
      CS$<>8__locals1.gelHeight = this.height - num * 2.0;
      CS$<>8__locals1.inner = Math.Max(0.0, actualWidth - num * 2.0);
      CS$<>8__locals1.segment = CS$<>8__locals1.inner / (double)Math.Max(this.segments, 1);
      if (!this.indeterminate)
      {
        int? num2 = this.current;
        if (num2 != null)
        {
          int valueOrDefault = num2.GetValueOrDefault();
          if (valueOrDefault >= this.done && valueOrDefault < this.segments)
          {
            double num3 = GelTrackView.<Layout>g__Fill|17_0(valueOrDefault + 1, ref CS$<>8__locals1);
            double num4 = Math.Max(0.0, num3 - ((this.done > 0) ? GelTrackView.<Layout>g__Fill|17_0(this.done, ref CS$<>8__locals1) : 0.0));
            this.currentGel = this.Gel(num3, CS$<>8__locals1.gelHeight);
            Canvas.SetLeft(this.currentGel, num);
            Canvas.SetTop(this.currentGel, num);
            this.layer.Children.Add(this.currentGel);
            if (Anim.ReduceMotion)
            {
              this.currentGel.Opacity = 0.25;
            }
            else
            {
              Anim.LoopScalar(this.currentGel, "Opacity", 2.2, delegate(double t)
              {
                double num8 = t % 2.2 / 1.1;
                double num9 = ((num8 <= 1.0) ? num8 : (2.0 - num8));
                num9 = num9 * num9 * (3.0 - 2.0 * num9);
                return (float)(0.25 + 0.2 * num9);
              }, 48, 0.0);
            }
            if (!Anim.ReduceMotion && num4 > 0.0)
            {
              this.shimmerClip = new Grid
              {
                Width = num3,
                Height = CS$<>8__locals1.gelHeight
              };
              ShimmerSweep shimmerSweep = new ShimmerSweep(0.85, 1.3)
              {
                Width = num4,
                HorizontalAlignment = HorizontalAlignment.Right
              };
              this.shimmerClip.Children.Add(shimmerSweep);
              Ui.ClipRounded(this.shimmerClip, CS$<>8__locals1.gelHeight / 2.0);
              Canvas.SetLeft(this.shimmerClip, num);
              Canvas.SetTop(this.shimmerClip, num);
              this.layer.Children.Add(this.shimmerClip);
            }
          }
        }
        if (this.done > 0)
        {
          this.doneGel = this.Gel(GelTrackView.<Layout>g__Fill|17_0(this.done, ref CS$<>8__locals1), CS$<>8__locals1.gelHeight);
          Canvas.SetLeft(this.doneGel, num);
          Canvas.SetTop(this.doneGel, num);
          this.layer.Children.Add(this.doneGel);
          if (animated && !Anim.ReduceMotion)
          {
            this.doneGel.CenterPoint = new Vector3(0f, (float)CS$<>8__locals1.gelHeight / 2f, 0f);
            this.doneGel.Scale = new Vector3(Math.Max(0.1f, (float)(GelTrackView.<Layout>g__Fill|17_0(Math.Max(0, this.done - 1), ref CS$<>8__locals1) / GelTrackView.<Layout>g__Fill|17_0(this.done, ref CS$<>8__locals1))), 1f, 1f);
            Anim.ScaleSpring(this.doneGel, 1f, new SwiftSpring(0.5, 0.85), new Vector3?(new Vector3(0f, (float)CS$<>8__locals1.gelHeight / 2f, 0f)));
            this.doneGel.Scale = new Vector3(this.doneGel.Scale.X, 1f, 1f);
          }
        }
        if (this.segments > 1)
        {
          double num5 = Math.Max(1.1, this.height * 0.11);
          double num6 = ((this.done > 0) ? GelTrackView.<Layout>g__Fill|17_0(this.done, ref CS$<>8__locals1) : 0.0);
          for (int i = 1; i < this.segments; i++)
          {
            double num7 = num + CS$<>8__locals1.segment * (double)i;
            bool flag = num7 < num + num6 - num5;
            Ellipse ellipse = new Ellipse
            {
              Width = num5 * 2.0,
              Height = num5 * 2.0,
              Fill = Ui.Brush(flag ? GelTrackView.GelRim.WithAlpha(0.45) : this.ink.WithAlpha(0.2))
            };
            Canvas.SetLeft(ellipse, num7 - num5);
            Canvas.SetTop(ellipse, this.height / 2.0 - num5);
            this.layer.Children.Add(ellipse);
          }
        }
        return;
      }
      this.slider = this.Gel(CS$<>8__locals1.inner * 0.3, CS$<>8__locals1.gelHeight);
      Canvas.SetLeft(this.slider, num);
      Canvas.SetTop(this.slider, num);
      this.layer.Children.Add(this.slider);
      if (Anim.ReduceMotion)
      {
        this.slider.Translation = new Vector3((float)(CS$<>8__locals1.inner * 0.7 * 0.5), 0f, 0f);
        return;
      }
      Compositor compositor = Ui.Compositor;
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(0f, Vector3.Zero);
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3((float)(CS$<>8__locals1.inner * 0.7), 0f, 0f), Easing.EaseInOut(compositor));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(1.4);
      vector3KeyFrameAnimation.Direction = AnimationDirection.Alternate;
      vector3KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      vector3KeyFrameAnimation.Target = "Translation";
      this.slider.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x0600228E RID: 8846 RVA: 0x00625A2C File Offset: 0x00625A2C
    [CompilerGenerated]
    internal static double <Layout>g__Fill|17_0(int steps, ref GelTrackView.<>c__DisplayClass17_0 A_1)
    {
      return GelTrack.FillWidth(steps, A_1.inner, A_1.segment, A_1.gelHeight);
    }

    // Token: 0x040010B8 RID: 4280
    private static readonly Color GelRim = DS.Hex(948782U, 1.0);

    // Token: 0x040010B9 RID: 4281
    private readonly double height;

    // Token: 0x040010BA RID: 4282
    private readonly Color ink;

    // Token: 0x040010BB RID: 4283
    private int segments;

    // Token: 0x040010BC RID: 4284
    private int done;

    // Token: 0x040010BD RID: 4285
    private int? current;

    // Token: 0x040010BE RID: 4286
    private readonly bool indeterminate;

    // Token: 0x040010BF RID: 4287
    [Nullable(1)]
    private readonly Canvas layer = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x040010C0 RID: 4288
    private FrameworkElement doneGel;

    // Token: 0x040010C1 RID: 4289
    private FrameworkElement currentGel;

    // Token: 0x040010C2 RID: 4290
    private FrameworkElement slider;

    // Token: 0x040010C3 RID: 4291
    private Grid shimmerClip;

    // Token: 0x040010C4 RID: 4292
    private const double InsetPoints = 2.0;

    // Token: 0x0200096C RID: 2412
    [NullableContext(0)]
    private new sealed class Shadow : Grid
    {
      // Token: 0x06003D51 RID: 15697 RVA: 0x00699DE4 File Offset: 0x00699DE4
      public Shadow(double radius, Color color, double blur)
      {
        base.Children.Add(new ShadowLayer(radius, color, blur, 0.0, 0.0));
      }
    }
  }
}
