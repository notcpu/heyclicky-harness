using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.UI
{
  // Token: 0x020000A3 RID: 163
  [NullableContext(1)]
  [Nullable(0)]
  public static class Anim
  {
    // Token: 0x17000114 RID: 276
    // (get) Token: 0x060006C0 RID: 1728 RVA: 0x00532158 File Offset: 0x00532158
    private static Compositor C
    {
      get
      {
        return CompositionTarget.GetCompositorForCurrentThread();
      }
    }

    // Token: 0x17000115 RID: 277
    // (get) Token: 0x060006C1 RID: 1729 RVA: 0x00532160 File Offset: 0x00532160
    // (set) Token: 0x060006C2 RID: 1730 RVA: 0x00532168 File Offset: 0x00532168
    public static bool Instant { get; set; }

    // Token: 0x17000116 RID: 278
    // (get) Token: 0x060006C3 RID: 1731 RVA: 0x00532170 File Offset: 0x00532170
    public static bool ReduceMotion
    {
      get
      {
        return MotionPreference.Reduced;
      }
    }

    // Token: 0x17000117 RID: 279
    // (get) Token: 0x060006C4 RID: 1732 RVA: 0x00532178 File Offset: 0x00532178
    private static bool Jump
    {
      get
      {
        return Anim.Instant || Anim.ReduceMotion;
      }
    }

    // Token: 0x060006C5 RID: 1733 RVA: 0x00532188 File Offset: 0x00532188
    public static void ScaleSpring(UIElement e, float to, SwiftSpring spring, Vector3? center = null)
    {
      if (Anim.Jump)
      {
        if (center != null)
        {
          Vector3 valueOrDefault = center.GetValueOrDefault();
          e.CenterPoint = valueOrDefault;
        }
        Anim.Stop(e, "Scale");
        e.Scale = new Vector3(to, to, 1f);
        return;
      }
      if (center != null)
      {
        Vector3 valueOrDefault2 = center.GetValueOrDefault();
        e.CenterPoint = valueOrDefault2;
      }
      e.StartAnimation(Anim.Target<SpringVector3NaturalMotionAnimation>(spring.Vector3(Anim.C, new Vector3(to, to, 1f)), "Scale"));
    }

    // Token: 0x060006C6 RID: 1734 RVA: 0x00532214 File Offset: 0x00532214
    public static void OpacitySpring(UIElement e, float to, SwiftSpring spring)
    {
      if (Anim.Instant)
      {
        Anim.Stop(e, "Opacity");
        e.Opacity = (double)to;
        return;
      }
      if (Anim.ReduceMotion)
      {
        Anim.Opacity(e, to, 0.12, null, 0.0);
        return;
      }
      e.StartAnimation(Anim.Target<SpringScalarNaturalMotionAnimation>(spring.Scalar(Anim.C, to), "Opacity"));
    }

    // Token: 0x060006C7 RID: 1735 RVA: 0x0053227C File Offset: 0x0053227C
    public static void TranslationSpring(UIElement e, Vector3 to, SwiftSpring spring)
    {
      if (Anim.Jump)
      {
        Anim.Stop(e, "Translation");
        e.Translation = to;
        return;
      }
      e.StartAnimation(Anim.Target<SpringVector3NaturalMotionAnimation>(spring.Vector3(Anim.C, to), "Translation"));
    }

    // Token: 0x060006C8 RID: 1736 RVA: 0x005322B8 File Offset: 0x005322B8
    public static void Scale(UIElement e, float to, double seconds, [Nullable(2)] CompositionEasingFunction easing = null, Vector3? center = null)
    {
      if (Anim.Jump)
      {
        if (center != null)
        {
          Vector3 valueOrDefault = center.GetValueOrDefault();
          e.CenterPoint = valueOrDefault;
        }
        Anim.Stop(e, "Scale");
        e.Scale = new Vector3(to, to, 1f);
        return;
      }
      if (center != null)
      {
        Vector3 valueOrDefault2 = center.GetValueOrDefault();
        e.CenterPoint = valueOrDefault2;
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = Anim.C.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, new Vector3(to, to, 1f), easing ?? Easing.EaseOut(Anim.C));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      vector3KeyFrameAnimation.Target = "Scale";
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x060006C9 RID: 1737 RVA: 0x0053236C File Offset: 0x0053236C
    public static void Opacity(UIElement e, float to, double seconds, [Nullable(2)] CompositionEasingFunction easing = null, double delay = 0.0)
    {
      if (Anim.Instant)
      {
        Anim.Stop(e, "Opacity");
        e.Opacity = (double)to;
        return;
      }
      if (Anim.ReduceMotion)
      {
        seconds = Math.Min(seconds, 0.12);
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, to, easing ?? Easing.EaseOut(Anim.C));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(Math.Max(seconds, 0.001));
      if (delay > 0.0)
      {
        scalarKeyFrameAnimation.DelayTime = TimeSpan.FromSeconds(delay);
        scalarKeyFrameAnimation.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      }
      scalarKeyFrameAnimation.Target = "Opacity";
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x060006CA RID: 1738 RVA: 0x00532420 File Offset: 0x00532420
    public static void ReducedMotionFade(UIElement e, float to, double seconds)
    {
      if (Anim.Instant)
      {
        Anim.Stop(e, "Opacity");
        e.Opacity = (double)to;
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, to, Easing.EaseOut(Anim.C));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(Math.Max(seconds, 0.001));
      scalarKeyFrameAnimation.Target = "Opacity";
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x060006CB RID: 1739 RVA: 0x00532498 File Offset: 0x00532498
    public static void Translate(UIElement e, Vector3 to, double seconds, [Nullable(2)] CompositionEasingFunction easing = null)
    {
      if (Anim.Jump)
      {
        Anim.Stop(e, "Translation");
        e.Translation = to;
        return;
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = Anim.C.CreateVector3KeyFrameAnimation();
      vector3KeyFrameAnimation.InsertKeyFrame(1f, to, easing ?? Easing.EaseOut(Anim.C));
      vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      vector3KeyFrameAnimation.Target = "Translation";
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x060006CC RID: 1740 RVA: 0x00532504 File Offset: 0x00532504
    public static void Rotate(UIElement e, float degrees, double seconds, [Nullable(2)] CompositionEasingFunction easing = null)
    {
      if (Anim.Jump)
      {
        Anim.Stop(e, "Rotation");
        e.Rotation = degrees;
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, degrees, easing ?? Easing.EaseOut(Anim.C));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      scalarKeyFrameAnimation.Target = "Rotation";
      e.StartAnimation(scalarKeyFrameAnimation);
    }

    // Token: 0x060006CD RID: 1741 RVA: 0x00532570 File Offset: 0x00532570
    public static void LoopScalar(UIElement e, string target, double period, Func<double, float> f, int samples = 48, double phaseOffset = 0.0)
    {
      if (!Anim.ReduceMotion || Anim.Instant)
      {
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
        LinearEasingFunction linearEasingFunction = Anim.C.CreateLinearEasingFunction();
        for (int i = 0; i <= samples; i++)
        {
          double num = (double)i / (double)samples;
          scalarKeyFrameAnimation.InsertKeyFrame((float)num, f(num * period + phaseOffset), linearEasingFunction);
        }
        scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(period);
        scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
        scalarKeyFrameAnimation.Target = target;
        e.StartAnimation(scalarKeyFrameAnimation);
        return;
      }
      float num2 = f(phaseOffset);
      Anim.Stop(e, target);
      if (target == "Opacity")
      {
        e.Opacity = (double)num2;
        return;
      }
      if (target == "Rotation")
      {
        e.Rotation = num2;
      }
    }

    // Token: 0x060006CE RID: 1742 RVA: 0x0053262C File Offset: 0x0053262C
    public static void LoopVector3(UIElement e, string target, double period, Func<double, Vector3> f, int samples = 48, double phaseOffset = 0.0)
    {
      if (!Anim.ReduceMotion || Anim.Instant)
      {
        Vector3KeyFrameAnimation vector3KeyFrameAnimation = Anim.C.CreateVector3KeyFrameAnimation();
        LinearEasingFunction linearEasingFunction = Anim.C.CreateLinearEasingFunction();
        for (int i = 0; i <= samples; i++)
        {
          double num = (double)i / (double)samples;
          vector3KeyFrameAnimation.InsertKeyFrame((float)num, f(num * period + phaseOffset), linearEasingFunction);
        }
        vector3KeyFrameAnimation.Duration = TimeSpan.FromSeconds(period);
        vector3KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
        vector3KeyFrameAnimation.Target = target;
        e.StartAnimation(vector3KeyFrameAnimation);
        return;
      }
      Vector3 vector = f(phaseOffset);
      Anim.Stop(e, target);
      if (target == "Translation")
      {
        e.Translation = vector;
        return;
      }
      if (target == "Scale")
      {
        e.Scale = vector;
      }
    }

    // Token: 0x060006CF RID: 1743 RVA: 0x005326E4 File Offset: 0x005326E4
    public static void KeyframesScalar(UIElement e, string target, [TupleElementNames(new string[] { "value", "seconds" })] [Nullable(new byte[] { 1, 0 })] IReadOnlyList<ValueTuple<float, double>> frames, [Nullable(2)] Func<float, float> map = null)
    {
      if (Anim.Jump && frames.Count > 0)
      {
        float item = frames[frames.Count - 1].Item1;
        float num = ((map == null) ? item : map(item));
        Anim.Stop(e, target);
        if (target == "Opacity")
        {
          e.Opacity = (double)num;
          return;
        }
        if (target == "Rotation")
        {
          e.Rotation = num;
        }
        return;
      }
      else
      {
        double num2 = frames.Sum<ValueTuple<float, double>>(([TupleElementNames(new string[] { "value", "seconds" })] ValueTuple<float, double> f) => f.Item2);
        if (num2 <= 0.0)
        {
          return;
        }
        ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
        CubicBezierEasingFunction cubicBezierEasingFunction = Easing.EaseInOut(Anim.C);
        double num3 = 0.0;
        foreach (ValueTuple<float, double> valueTuple in frames)
        {
          float item2 = valueTuple.Item1;
          double item3 = valueTuple.Item2;
          num3 += item3;
          scalarKeyFrameAnimation.InsertKeyFrame((float)(num3 / num2), (map == null) ? item2 : map(item2), cubicBezierEasingFunction);
        }
        scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(num2);
        scalarKeyFrameAnimation.Target = target;
        e.StartAnimation(scalarKeyFrameAnimation);
        return;
      }
    }

    // Token: 0x060006D0 RID: 1744 RVA: 0x00532830 File Offset: 0x00532830
    public static void Stop(UIElement e, string target)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = Anim.C.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.Target = target;
      try
      {
        e.StopAnimation(scalarKeyFrameAnimation);
      }
      catch
      {
      }
    }

    // Token: 0x060006D1 RID: 1745 RVA: 0x0053286C File Offset: 0x0053286C
    private static T Target<[Nullable(0)] T>(T a, string target) where T : CompositionAnimation
    {
      a.Target = target;
      return a;
    }

    // Token: 0x060006D2 RID: 1746 RVA: 0x0053287C File Offset: 0x0053287C
    public static void ScaleUpFrom(UIElement e, float from, SwiftSpring spring, Vector3? center = null)
    {
      if (center != null)
      {
        Vector3 valueOrDefault = center.GetValueOrDefault();
        e.CenterPoint = valueOrDefault;
      }
      if (Anim.Jump)
      {
        Anim.Stop(e, "Scale");
        e.Scale = Vector3.One;
        return;
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = SpringCurve.Scale(Anim.C, spring, from, 1f);
      vector3KeyFrameAnimation.Target = "Scale";
      e.StartAnimation(vector3KeyFrameAnimation);
    }

    // Token: 0x060006D3 RID: 1747 RVA: 0x005328E4 File Offset: 0x005328E4
    public static void ContentIn(FrameworkElement e, SwiftSpring spring)
    {
      Anim.<>c__DisplayClass25_0 CS$<>8__locals1 = new Anim.<>c__DisplayClass25_0();
      CS$<>8__locals1.e = e;
      CS$<>8__locals1.spring = spring;
      CS$<>8__locals1.e.Opacity = 0.0;
      if (CS$<>8__locals1.e.ActualWidth > 0.0)
      {
        CS$<>8__locals1.<ContentIn>g__Run|0();
        return;
      }
      CS$<>8__locals1.e.SizeChanged += CS$<>8__locals1.<ContentIn>g__OnSize|1;
    }

    // Token: 0x060006D4 RID: 1748 RVA: 0x00532950 File Offset: 0x00532950
    public static void ContentOut(FrameworkElement e, SwiftSpring spring)
    {
      e.CenterPoint = new Vector3((float)(e.ActualWidth / 2.0), 0f, 0f);
      Anim.ScaleSpring(e, 0.92f, spring, null);
      Anim.OpacitySpring(e, 0f, spring);
    }

    // Token: 0x060006D5 RID: 1749 RVA: 0x005329A4 File Offset: 0x005329A4
    public static void ContentBack(FrameworkElement e, SwiftSpring spring)
    {
      e.CenterPoint = new Vector3((float)(e.ActualWidth / 2.0), 0f, 0f);
      Anim.ScaleSpring(e, 1f, spring, null);
      Anim.OpacitySpring(e, 1f, spring);
    }

    // Token: 0x040001A7 RID: 423
    private const double ReducedFadeSeconds = 0.12;
  }
}
