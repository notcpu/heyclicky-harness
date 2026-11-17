using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Notch.Surfaces
{
  // Token: 0x0200015E RID: 350
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntroSkillPingSurface : NotchSurface
  {
    // Token: 0x0600135C RID: 4956 RVA: 0x005A0A98 File Offset: 0x005A0A98
    public IntroSkillPingSurface(NotchLiveEvent.IntroSkillPing e, Color fallbackColor)
    {
      this.state = e;
      string glyphColorHex = e.GlyphColorHex;
      this.color = ((glyphColorHex != null) ? DS.CursorColor(glyphColorHex) : fallbackColor);
      this.halo = Ui.GlowSpot(this.color, 1.0, 66.0, 0.25);
      this.mark = IntroMark.Create(34.0, this.color, 0.0);
      Grid grid = new Grid
      {
        Width = 34.0,
        Height = 34.0,
        IsHitTestVisible = false
      };
      this.markShadow = Ui.Compositor.CreateDropShadow();
      this.markShadow.Color = this.color;
      this.markShadow.Opacity = 0.36f;
      this.markShadow.BlurRadius = 10f;
      this.markShadow.Mask = this.mark.GetAlphaMask();
      SpriteVisual spriteVisual = Ui.Compositor.CreateSpriteVisual();
      spriteVisual.Size = new Vector2(34f, 34f);
      spriteVisual.Shadow = this.markShadow;
      ElementCompositionPreview.SetElementChildVisual(grid, spriteVisual);
      this.markHost.Children.Add(grid);
      this.markHost.Children.Add(this.mark);
      this.wiggle.Children.Add(this.halo);
      this.wiggle.Children.Add(this.markHost);
      this.wiggle.CenterPoint = new Vector3(70f, 70f, 0f);
      this.floatHost.Children.Add(this.wiggle);
      this.overlay.Children.Add(this.floatHost);
      this.overlay.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Position();
      };
      this.ApplyBias(false);
      if (!Anim.Instant)
      {
        Anim.LoopVector3(this.floatHost, "Translation", 3.5, (double t) => new Vector3(0f, (float)(-3.0 * Math.Cos((t / 3.5 + 0.21428571428571427) * 2.0 * 3.141592653589793)), 0f), 48, 0.0);
      }
    }

    // Token: 0x17000384 RID: 900
    // (get) Token: 0x0600135D RID: 4957 RVA: 0x005A0D8C File Offset: 0x005A0D8C
    public override FrameworkElement View
    {
      get
      {
        return this.empty;
      }
    }

    // Token: 0x17000385 RID: 901
    // (get) Token: 0x0600135E RID: 4958 RVA: 0x005A0D94 File Offset: 0x005A0D94
    [Nullable(2)]
    public override FrameworkElement Overlay
    {
      [NullableContext(2)]
      get
      {
        return this.overlay;
      }
    }

    // Token: 0x0600135F RID: 4959 RVA: 0x005A0D9C File Offset: 0x005A0D9C
    public override bool CanUpdate(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      return liveEvent != null && liveEvent.Event is NotchLiveEvent.IntroSkillPing;
    }

    // Token: 0x06001360 RID: 4960 RVA: 0x005A0DC4 File Offset: 0x005A0DC4
    public override void Update(NotchPresentation p)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent.IntroSkillPing introSkillPing = liveEvent.Event as NotchLiveEvent.IntroSkillPing;
        if (introSkillPing != null)
        {
          bool flag = introSkillPing.WiggleToken != this.state.WiggleToken;
          this.state = introSkillPing;
          string glyphColorHex = introSkillPing.GlyphColorHex;
          if (glyphColorHex != null)
          {
            Color color = DS.CursorColor(glyphColorHex);
            if (color != this.color)
            {
              this.color = color;
              this.mark.Fill = Ui.Brush(color);
              this.markShadow.Color = color;
            }
          }
          this.ApplyBias(true);
          this.Position();
          if (flag)
          {
            this.Wiggle(introSkillPing.LastToggleAdded);
          }
          return;
        }
      }
    }

    // Token: 0x17000386 RID: 902
    // (get) Token: 0x06001361 RID: 4961 RVA: 0x005A0E68 File Offset: 0x005A0E68
    private double PositiveBias
    {
      get
      {
        return Math.Max(0.0, this.state.ScaleBias);
      }
    }

    // Token: 0x06001362 RID: 4962 RVA: 0x005A0E84 File Offset: 0x005A0E84
    private void Position()
    {
      double num = ((this.overlay.ActualHeight > 0.0) ? this.overlay.ActualHeight : 104.0);
      double num2 = ((this.overlay.ActualWidth > 0.0) ? this.overlay.ActualWidth : 60.0);
      double num3 = Math.Max(64.0, num - (32.0 + this.PositiveBias * 8.0));
      this.floatHost.Margin = new Thickness(num2 / 2.0 - 70.0, num3 - 70.0, 0.0, 0.0);
    }

    // Token: 0x06001363 RID: 4963 RVA: 0x005A0F58 File Offset: 0x005A0F58
    private void ApplyBias(bool animated)
    {
      float num = (float)Math.Clamp(1.0 + this.state.ScaleBias, 0.78, 1.42);
      double num2 = Math.Max(0.07, 0.12 + this.state.ScaleBias * 0.1);
      float num3 = (float)((46.0 + this.PositiveBias * 18.0) / 46.0) * num;
      this.halo.CenterPoint = new Vector3(33f, 33f, 0f);
      if (animated && !Anim.Instant)
      {
        Anim.Scale(this.markHost, num, 0.18, Easing.EaseOut(Ui.Compositor), new Vector3?(new Vector3(17f, 17f, 0f)));
        Anim.Scale(this.halo, num3, 0.18, Easing.EaseOut(Ui.Compositor), null);
        Anim.Opacity(this.halo, (float)num2, 0.18, null, 0.0);
        return;
      }
      this.markHost.CenterPoint = new Vector3(17f, 17f, 0f);
      this.markHost.Scale = new Vector3(num, num, 1f);
      this.halo.Scale = new Vector3(num3, num3, 1f);
      this.halo.Opacity = num2;
    }

    // Token: 0x06001364 RID: 4964 RVA: 0x005A10F4 File Offset: 0x005A10F4
    private void Wiggle(bool adding)
    {
      if (Anim.Instant)
      {
        return;
      }
      ValueTuple<float, double>[] array2;
      if (!adding)
      {
        ValueTuple<float, double>[] array = new ValueTuple<float, double>[4];
        array[0] = new ValueTuple<float, double>(1f, 0.04);
        array[1] = new ValueTuple<float, double>(0.84f, 0.12);
        array[2] = new ValueTuple<float, double>(0.94f, 0.12);
        array2 = array;
        array[3] = new ValueTuple<float, double>(1f, 0.22);
      }
      else
      {
        ValueTuple<float, double>[] array3 = new ValueTuple<float, double>[4];
        array3[0] = new ValueTuple<float, double>(0.86f, 0.04);
        array3[1] = new ValueTuple<float, double>(1.12f, 0.14);
        array3[2] = new ValueTuple<float, double>(1.04f, 0.18);
        array2 = array3;
        array3[3] = new ValueTuple<float, double>(1f, 0.3);
      }
      ValueTuple<float, double>[] array4 = array2;
      ValueTuple<float, double>[] array6;
      if (!adding)
      {
        ValueTuple<float, double>[] array5 = new ValueTuple<float, double>[5];
        array5[0] = new ValueTuple<float, double>(0f, 0.04);
        array5[1] = new ValueTuple<float, double>(7f, 0.08);
        array5[2] = new ValueTuple<float, double>(-8f, 0.1);
        array5[3] = new ValueTuple<float, double>(3f, 0.08);
        array6 = array5;
        array5[4] = new ValueTuple<float, double>(0f, 0.18);
      }
      else
      {
        ValueTuple<float, double>[] array7 = new ValueTuple<float, double>[5];
        array7[0] = new ValueTuple<float, double>(0f, 0.04);
        array7[1] = new ValueTuple<float, double>(-7f, 0.1);
        array7[2] = new ValueTuple<float, double>(5f, 0.12);
        array7[3] = new ValueTuple<float, double>(-2f, 0.1);
        array6 = array7;
        array7[4] = new ValueTuple<float, double>(0f, 0.26);
      }
      ValueTuple<float, double>[] array8 = array6;
      ValueTuple<float, double>[] array10;
      if (!adding)
      {
        ValueTuple<float, double>[] array9 = new ValueTuple<float, double>[4];
        array9[0] = new ValueTuple<float, double>(0f, 0.04);
        array9[1] = new ValueTuple<float, double>(-2f, 0.1);
        array9[2] = new ValueTuple<float, double>(2f, 0.14);
        array10 = array9;
        array9[3] = new ValueTuple<float, double>(0f, 0.2);
      }
      else
      {
        ValueTuple<float, double>[] array11 = new ValueTuple<float, double>[4];
        array11[0] = new ValueTuple<float, double>(-7f, 0.04);
        array11[1] = new ValueTuple<float, double>(-11f, 0.12);
        array11[2] = new ValueTuple<float, double>(5f, 0.26);
        array10 = array11;
        array11[3] = new ValueTuple<float, double>(0f, 0.24);
      }
      ValueTuple<float, double>[] array12 = array10;
      ValueTuple<float, double>[] array14;
      if (!adding)
      {
        ValueTuple<float, double>[] array13 = new ValueTuple<float, double>[4];
        array13[0] = new ValueTuple<float, double>(0f, 0.04);
        array13[1] = new ValueTuple<float, double>(-2f, 0.08);
        array13[2] = new ValueTuple<float, double>(2f, 0.1);
        array14 = array13;
        array13[3] = new ValueTuple<float, double>(0f, 0.2);
      }
      else
      {
        ValueTuple<float, double>[] array15 = new ValueTuple<float, double>[4];
        array15[0] = new ValueTuple<float, double>(0f, 0.04);
        array15[1] = new ValueTuple<float, double>(2f, 0.1);
        array15[2] = new ValueTuple<float, double>(-1f, 0.14);
        array14 = array15;
        array15[3] = new ValueTuple<float, double>(0f, 0.28);
      }
      ValueTuple<float, double>[] array16 = array14;
      Compositor compositor = Ui.Compositor;
      CubicBezierEasingFunction ease = Easing.EaseInOut(compositor);
      Vector3KeyFrameAnimation sa = compositor.CreateVector3KeyFrameAnimation();
      TimeSpan timeSpan;
      IntroSkillPingSurface.Insert(array4, delegate(float k, float v)
      {
        sa.InsertKeyFrame(k, new Vector3(v, v, 1f), ease);
      }, out timeSpan);
      sa.Duration = timeSpan;
      sa.Target = "Scale";
      this.wiggle.StartAnimation(sa);
      Anim.KeyframesScalar(this.wiggle, "Rotation", array8, null);
      double num = Math.Max(array16.Sum<ValueTuple<float, double>>(([TupleElementNames(new string[] { "v", "s" })] ValueTuple<float, double> f) => f.Item2), array12.Sum<ValueTuple<float, double>>(([TupleElementNames(new string[] { "v", "s" })] ValueTuple<float, double> f) => f.Item2));
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = compositor.CreateLinearEasingFunction();
      for (double num2 = 0.0; num2 <= num + 1E-06; num2 += 0.02)
      {
        vector3KeyFrameAnimation.InsertKeyFrame((float)(num2 / num), new Vector3(IntroSkillPingSurface.Sample(array16, num2), IntroSkillPingSurface.Sample(array12, num2), 0f), linearEasingFunction);
      }
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(num);
      vector3KeyFrameAnimation.Target = "Translation";
      this.wiggle.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x06001365 RID: 4965 RVA: 0x005A1628 File Offset: 0x005A1628
    private static void Insert([TupleElementNames(new string[] { "v", "s" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<float, double>[] frames, Action<float, float> add, out TimeSpan duration)
    {
      double num = frames.Sum<ValueTuple<float, double>>(([TupleElementNames(new string[] { "v", "s" })] ValueTuple<float, double> f) => f.Item2);
      double num2 = 0.0;
      foreach (ValueTuple<float, double> valueTuple in frames)
      {
        float item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        num2 += item2;
        add((float)(num2 / num), item);
      }
      duration = TimeSpan.FromSeconds(num);
    }

    // Token: 0x06001366 RID: 4966 RVA: 0x005A16A8 File Offset: 0x005A16A8
    private static float Sample([TupleElementNames(new string[] { "v", "s" })] [Nullable(new byte[] { 1, 0 })] ValueTuple<float, double>[] frames, double t)
    {
      float num = 0f;
      double num2 = 0.0;
      foreach (ValueTuple<float, double> valueTuple in frames)
      {
        float item = valueTuple.Item1;
        double item2 = valueTuple.Item2;
        if (t <= num2 + item2)
        {
          double num3 = ((item2 <= 0.0) ? 1.0 : ((t - num2) / item2));
          num3 = ((num3 < 0.5) ? (2.0 * num3 * num3) : (1.0 - Math.Pow(-2.0 * num3 + 2.0, 2.0) / 2.0));
          return (float)((double)num + (double)(item - num) * num3);
        }
        num = item;
        num2 += item2;
      }
      return num;
    }

    // Token: 0x040008F6 RID: 2294
    private readonly Grid empty = new Grid();

    // Token: 0x040008F7 RID: 2295
    private readonly Grid overlay = new Grid
    {
      IsHitTestVisible = false
    };

    // Token: 0x040008F8 RID: 2296
    private readonly Grid floatHost = new Grid
    {
      Width = 140.0,
      Height = 140.0,
      HorizontalAlignment = HorizontalAlignment.Left,
      VerticalAlignment = VerticalAlignment.Top
    };

    // Token: 0x040008F9 RID: 2297
    private readonly Grid wiggle = new Grid
    {
      Width = 140.0,
      Height = 140.0
    };

    // Token: 0x040008FA RID: 2298
    private readonly Grid halo;

    // Token: 0x040008FB RID: 2299
    private readonly Path mark;

    // Token: 0x040008FC RID: 2300
    private readonly Grid markHost = new Grid
    {
      Width = 34.0,
      Height = 34.0,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center
    };

    // Token: 0x040008FD RID: 2301
    private NotchLiveEvent.IntroSkillPing state;

    // Token: 0x040008FE RID: 2302
    private Color color;

    // Token: 0x040008FF RID: 2303
    private readonly DropShadow markShadow;
  }
}
