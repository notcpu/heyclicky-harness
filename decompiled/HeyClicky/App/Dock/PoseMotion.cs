using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace HeyClicky.App.Dock
{
  // Token: 0x0200032E RID: 814
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class PoseMotion
  {
    // Token: 0x060022BE RID: 8894 RVA: 0x00627568 File Offset: 0x00627568
    public PoseMotion(UIElement element, [Nullable(2)] Visual scaler, Vector2 anchor)
    {
      this.element = element;
      this.scaler = scaler;
      this.anchor = anchor;
    }

    // Token: 0x17000669 RID: 1641
    // (get) Token: 0x060022BF RID: 8895 RVA: 0x006275BC File Offset: 0x006275BC
    public static double Now
    {
      get
      {
        return PointerEvaluationDriver.Now;
      }
    }

    // Token: 0x1700066A RID: 1642
    // (get) Token: 0x060022C0 RID: 8896 RVA: 0x006275C4 File Offset: 0x006275C4
    public DeckPose Target
    {
      get
      {
        return this.to;
      }
    }

    // Token: 0x1700066B RID: 1643
    // (get) Token: 0x060022C1 RID: 8897 RVA: 0x006275CC File Offset: 0x006275CC
    public bool HasPose
    {
      get
      {
        return this.hasPose;
      }
    }

    // Token: 0x1700066C RID: 1644
    // (get) Token: 0x060022C2 RID: 8898 RVA: 0x006275D4 File Offset: 0x006275D4
    public bool IsMoving
    {
      get
      {
        return this.hasPose && PoseMotion.Now < this.endsAt;
      }
    }

    // Token: 0x060022C3 RID: 8899 RVA: 0x006275F0 File Offset: 0x006275F0
    public DeckPose PoseAt(double time)
    {
      double num = time - this.startsAt;
      if (!this.hasPose || num <= 0.0)
      {
        return this.from;
      }
      if (time >= this.endsAt)
      {
        return this.to;
      }
      ValueTuple<double, double, double, double> valueTuple = SpringCurve.Basis(this.spring, num);
      double item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      return this.to + (this.from - this.to) * item + this.velocity * item2;
    }

    // Token: 0x060022C4 RID: 8900 RVA: 0x00627680 File Offset: 0x00627680
    private DeckPose VelocityAt(double time)
    {
      double num = time - this.startsAt;
      if (!this.hasPose || num <= 0.0 || time >= this.endsAt)
      {
        return DeckPose.Zero;
      }
      ValueTuple<double, double, double, double> valueTuple = SpringCurve.Basis(this.spring, num);
      double item = valueTuple.Item3;
      double item2 = valueTuple.Item4;
      return (this.from - this.to) * item + this.velocity * item2;
    }

    // Token: 0x060022C5 RID: 8901 RVA: 0x006276FC File Offset: 0x006276FC
    public void SetRaster(double scale)
    {
      if (scale <= 0.0 || Math.Abs(scale - this.raster) < 1E-09)
      {
        return;
      }
      this.raster = scale;
      this.element.Scale = new Vector3((float)scale, (float)scale, 1f);
      if (!this.hasPose)
      {
        return;
      }
      double now = PoseMotion.Now;
      if (now < this.endsAt)
      {
        this.Play(this.PoseAt(now), this.VelocityAt(now), this.to, this.spring, Math.Max(0.0, this.startsAt - now), false, true);
        return;
      }
      this.Play(this.to, DeckPose.Zero, this.to, this.spring, 0.0, true, false);
    }

    // Token: 0x060022C6 RID: 8902 RVA: 0x006277C8 File Offset: 0x006277C8
    public void Jump(DeckPose target)
    {
      this.Play(target, DeckPose.Zero, target, this.spring, 0.0, true, false);
    }

    // Token: 0x060022C7 RID: 8903 RVA: 0x006277E8 File Offset: 0x006277E8
    public void SpringTo(DeckPose target, SwiftSpring next, double delay = 0.0)
    {
      if (!this.hasPose)
      {
        this.Jump(target);
        return;
      }
      double now = PoseMotion.Now;
      DeckPose deckPose = this.PoseAt(now);
      DeckPose deckPose2 = this.VelocityAt(now);
      if (target == this.to && deckPose2 == DeckPose.Zero && deckPose == this.to)
      {
        return;
      }
      this.Play(deckPose, deckPose2, target, next, delay, false, false);
    }

    // Token: 0x060022C8 RID: 8904 RVA: 0x00627854 File Offset: 0x00627854
    private void Play(DeckPose start, DeckPose speed, DeckPose target, SwiftSpring next, double delay, bool jump = false, bool rescale = false)
    {
      double now = PoseMotion.Now;
      DeckPose deckPose = (this.hasPose ? this.PoseAt(now) : start);
      this.from = start;
      this.velocity = speed;
      this.to = target;
      this.spring = next;
      this.hasPose = true;
      double num = (jump ? 0.0 : PoseMotion.Duration(start - target, speed, next));
      this.startsAt = now + delay;
      this.endsAt = this.startsAt + num;
      if (this.to.Opacity > 0.0 && this.element.Opacity <= 0.0)
      {
        this.element.Opacity = 1.0;
      }
      this.Emit(deckPose, num, delay, rescale);
      if (this.settleTimer == null)
      {
        this.settleTimer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        this.settleTimer.IsRepeating = false;
        this.settleTimer.Tick += delegate(DispatcherQueueTimer _, object _)
        {
          if (PoseMotion.Now < this.endsAt)
          {
            return;
          }
          this.element.Translation = this.Translation(this.to);
          this.element.Rotation = (float)this.to.Degrees;
          this.element.Opacity = Math.Clamp(this.to.Opacity, 0.0, 1.0);
        };
      }
      this.settleTimer.Stop();
      this.settleTimer.Interval = TimeSpan.FromSeconds(delay + num + 0.05);
      this.settleTimer.Start();
    }

    // Token: 0x060022C9 RID: 8905 RVA: 0x00627990 File Offset: 0x00627990
    private static double Duration(DeckPose offset, DeckPose speed, SwiftSpring spring)
    {
      PoseMotion.<>c__DisplayClass28_0 CS$<>8__locals1;
      CS$<>8__locals1.spring = spring;
      CS$<>8__locals1.longest = 0.0;
      PoseMotion.<Duration>g__Take|28_0(offset.X, speed.X, PoseMotion.Epsilon.X, ref CS$<>8__locals1);
      PoseMotion.<Duration>g__Take|28_0(offset.Y, speed.Y, PoseMotion.Epsilon.Y, ref CS$<>8__locals1);
      PoseMotion.<Duration>g__Take|28_0(offset.Scale, speed.Scale, PoseMotion.Epsilon.Scale, ref CS$<>8__locals1);
      PoseMotion.<Duration>g__Take|28_0(offset.Degrees, speed.Degrees, PoseMotion.Epsilon.Degrees, ref CS$<>8__locals1);
      PoseMotion.<Duration>g__Take|28_0(offset.Opacity, speed.Opacity, PoseMotion.Epsilon.Opacity, ref CS$<>8__locals1);
      return CS$<>8__locals1.longest;
    }

    // Token: 0x060022CA RID: 8906 RVA: 0x00627A58 File Offset: 0x00627A58
    private Vector3 Translation(DeckPose p)
    {
      return new Vector3((float)(p.X - (double)this.anchor.X), (float)(p.Y - (double)this.anchor.Y), 0f);
    }

    // Token: 0x060022CB RID: 8907 RVA: 0x00627A90 File Offset: 0x00627A90
    private void Emit(DeckPose previous, double duration, double delay, bool rescale)
    {
      PoseMotion.<>c__DisplayClass31_0 CS$<>8__locals1;
      CS$<>8__locals1.previous = previous;
      CS$<>8__locals1.<>4__this = this;
      Compositor compositor = Ui.Compositor;
      if (PoseMotion.linear == null)
      {
        PoseMotion.linear = compositor.CreateLinearEasingFunction();
      }
      bool flag = duration <= 0.0;
      int num = (flag ? 1 : Math.Clamp((int)Math.Ceiling(duration * 70.0), 6, 48));
      double[] array = new double[num + 1];
      DeckPose[] array2 = new DeckPose[num + 1];
      for (int i = 0; i <= num; i++)
      {
        double num2 = (double)i / (double)num;
        array[i] = (flag ? num2 : Math.Pow(num2, 1.6));
        array2[i] = ((flag || i == num) ? this.to : this.PoseAt(this.startsAt + array[i] * duration));
      }
      if (flag)
      {
        array2[0] = this.to;
      }
      TimeSpan timeSpan = TimeSpan.FromSeconds(Math.Max(duration, 0.001));
      TimeSpan timeSpan2 = TimeSpan.FromSeconds(Math.Max(0.0, delay));
      if (!flag)
      {
        if (!this.<Emit>g__Moves|31_0((DeckPose p) => p.X, PoseMotion.Epsilon.X, ref CS$<>8__locals1))
        {
          if (!this.<Emit>g__Moves|31_0((DeckPose p) => p.Y, PoseMotion.Epsilon.Y, ref CS$<>8__locals1))
          {
            goto IL_01BD;
          }
        }
      }
      Vector3KeyFrameAnimation vector3KeyFrameAnimation = compositor.CreateVector3KeyFrameAnimation();
      for (int j = 0; j <= num; j++)
      {
        vector3KeyFrameAnimation.InsertKeyFrame((float)array[j], this.Translation(array2[j]), PoseMotion.linear);
      }
      this.Start(vector3KeyFrameAnimation, "Translation", timeSpan, timeSpan2);
      IL_01BD:
      if (!flag)
      {
        if (!this.<Emit>g__Moves|31_0((DeckPose p) => p.Degrees, PoseMotion.Epsilon.Degrees, ref CS$<>8__locals1))
        {
          goto IL_023D;
        }
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = compositor.CreateScalarKeyFrameAnimation();
      for (int k = 0; k <= num; k++)
      {
        scalarKeyFrameAnimation.InsertKeyFrame((float)array[k], (float)array2[k].Degrees, PoseMotion.linear);
      }
      this.Start(scalarKeyFrameAnimation, "Rotation", timeSpan, timeSpan2);
      IL_023D:
      if (!flag)
      {
        if (!this.<Emit>g__Moves|31_0((DeckPose p) => p.Opacity, PoseMotion.Epsilon.Opacity, ref CS$<>8__locals1))
        {
          goto IL_02D4;
        }
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation2 = compositor.CreateScalarKeyFrameAnimation();
      for (int l = 0; l <= num; l++)
      {
        scalarKeyFrameAnimation2.InsertKeyFrame((float)array[l], (float)Math.Clamp(array2[l].Opacity, 0.0, 1.0), PoseMotion.linear);
      }
      this.Start(scalarKeyFrameAnimation2, "Opacity", timeSpan, timeSpan2);
      IL_02D4:
      if (this.scaler != null)
      {
        if (!flag && !rescale)
        {
          if (!this.<Emit>g__Moves|31_0((DeckPose p) => p.Scale, PoseMotion.Epsilon.Scale, ref CS$<>8__locals1))
          {
            return;
          }
        }
        Vector3KeyFrameAnimation vector3KeyFrameAnimation2 = compositor.CreateVector3KeyFrameAnimation();
        for (int m = 0; m <= num; m++)
        {
          float num3 = (float)(array2[m].Scale / this.raster);
          vector3KeyFrameAnimation2.InsertKeyFrame((float)array[m], new Vector3(num3, num3, 1f), PoseMotion.linear);
        }
        vector3KeyFrameAnimation2.Duration = timeSpan;
        vector3KeyFrameAnimation2.DelayTime = timeSpan2;
        vector3KeyFrameAnimation2.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
        this.scaler.StartAnimation("Scale", vector3KeyFrameAnimation2);
      }
    }

    // Token: 0x060022CC RID: 8908 RVA: 0x00627E34 File Offset: 0x00627E34
    private void Start(KeyFrameAnimation a, string target, TimeSpan span, TimeSpan wait)
    {
      a.Duration = span;
      a.DelayTime = wait;
      a.DelayBehavior = AnimationDelayBehavior.SetInitialValueBeforeDelay;
      a.Target = target;
      this.element.StartAnimation(a);
    }

    // Token: 0x060022CF RID: 8911 RVA: 0x00627F14 File Offset: 0x00627F14
    [CompilerGenerated]
    internal static void <Duration>g__Take|28_0(double distance, double rate, double epsilon, ref PoseMotion.<>c__DisplayClass28_0 A_3)
    {
      A_3.longest = Math.Max(A_3.longest, SpringCurve.SettleSeconds(A_3.spring, distance, rate, epsilon));
    }

    // Token: 0x060022D0 RID: 8912 RVA: 0x00627F38 File Offset: 0x00627F38
    [CompilerGenerated]
    private bool <Emit>g__Moves|31_0(Func<DeckPose, double> channel, double epsilon, ref PoseMotion.<>c__DisplayClass31_0 A_3)
    {
      return Math.Abs(channel(A_3.previous) - channel(this.to)) > epsilon || Math.Abs(channel(this.from) - channel(this.to)) > epsilon || channel(this.velocity) != 0.0;
    }

    // Token: 0x040010DB RID: 4315
    private readonly UIElement element;

    // Token: 0x040010DC RID: 4316
    [Nullable(2)]
    private readonly Visual scaler;

    // Token: 0x040010DD RID: 4317
    private readonly Vector2 anchor;

    // Token: 0x040010DE RID: 4318
    private double raster = 1.0;

    // Token: 0x040010DF RID: 4319
    private DeckPose from;

    // Token: 0x040010E0 RID: 4320
    private DeckPose velocity;

    // Token: 0x040010E1 RID: 4321
    private DeckPose to;

    // Token: 0x040010E2 RID: 4322
    private SwiftSpring spring = new SwiftSpring(0.36, 0.86);

    // Token: 0x040010E3 RID: 4323
    private double startsAt;

    // Token: 0x040010E4 RID: 4324
    private double endsAt;

    // Token: 0x040010E5 RID: 4325
    private bool hasPose;

    // Token: 0x040010E6 RID: 4326
    private static readonly DeckPose Epsilon = new DeckPose(0.02, 0.02, 0.0002, 0.005, 0.001);

    // Token: 0x040010E7 RID: 4327
    [Nullable(2)]
    private DispatcherQueueTimer settleTimer;

    // Token: 0x040010E8 RID: 4328
    [Nullable(2)]
    private static CompositionEasingFunction linear;
  }
}
