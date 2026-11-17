using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Platform.Overlay;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x02000119 RID: 281
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class CompositorSpringMotion
  {
    // Token: 0x170002C1 RID: 705
    // (get) Token: 0x06000F04 RID: 3844 RVA: 0x005775B8 File Offset: 0x005775B8
    // (set) Token: 0x06000F05 RID: 3845 RVA: 0x005775C0 File Offset: 0x005775C0
    public int Moves { get; private set; }

    // Token: 0x170002C2 RID: 706
    // (get) Token: 0x06000F06 RID: 3846 RVA: 0x005775CC File Offset: 0x005775CC
    // (set) Token: 0x06000F07 RID: 3847 RVA: 0x005775D4 File Offset: 0x005775D4
    public int SlotWaits { get; private set; }

    // Token: 0x06000F08 RID: 3848 RVA: 0x005775E0 File Offset: 0x005775E0
    public void ResetCounts()
    {
      this.Moves = 0;
      this.SlotWaits = 0;
    }

    // Token: 0x06000F09 RID: 3849 RVA: 0x005775F0 File Offset: 0x005775F0
    public CompositorSpringMotion(UIElement element)
    {
      ElementCompositionPreview.SetIsTranslationEnabled(element, true);
      Visual elementVisual = ElementCompositionPreview.GetElementVisual(element);
      this.compositor = elementVisual.Compositor;
      this.props = this.compositor.CreatePropertySet();
      this.sums = this.compositor.CreatePropertySet();
      this.props.InsertVector3("Rest", Vector3.Zero);
      for (int i = 0; i < 64; i++)
      {
        this.props.InsertVector3("D" + i.ToString(), Vector3.Zero);
        this.props.InsertScalar("G" + i.ToString(), 0f);
      }
      StringBuilder stringBuilder = new StringBuilder("p.Rest");
      for (int j = 0; j < 8; j++)
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        for (int k = 0; k < 8; k++)
        {
          int num = j * 8 + k;
          if (k > 0)
          {
            stringBuilder2.Append('+');
          }
          stringBuilder2.Append("p.D").Append(num).Append("*p.G")
            .Append(num);
        }
        this.sums.InsertVector3("S" + j.ToString(), Vector3.Zero);
        ExpressionAnimation expressionAnimation = this.compositor.CreateExpressionAnimation(stringBuilder2.ToString());
        expressionAnimation.SetReferenceParameter("p", this.props);
        this.sums.StartAnimation("S" + j.ToString(), expressionAnimation);
        stringBuilder.Append("+s.S").Append(j);
      }
      ExpressionAnimation expressionAnimation2 = this.compositor.CreateExpressionAnimation(stringBuilder.ToString());
      expressionAnimation2.SetReferenceParameter("p", this.props);
      expressionAnimation2.SetReferenceParameter("s", this.sums);
      elementVisual.StartAnimation("Translation", expressionAnimation2);
    }

    // Token: 0x06000F0A RID: 3850 RVA: 0x005777E8 File Offset: 0x005777E8
    public void Snap(Vector3 to)
    {
      for (int i = 0; i < 64; i++)
      {
        this.props.StopAnimation("G" + i.ToString());
        this.props.InsertScalar("G" + i.ToString(), 0f);
        this.props.InsertVector3("D" + i.ToString(), Vector3.Zero);
        this.slotFreeAt[i] = 0.0;
      }
      this.rest = to;
      this.props.InsertVector3("Rest", this.rest);
    }

    // Token: 0x06000F0B RID: 3851 RVA: 0x00577890 File Offset: 0x00577890
    public void Jump(Vector3 delta)
    {
      this.rest += delta;
      this.props.InsertVector3("Rest", this.rest);
    }

    // Token: 0x06000F0C RID: 3852 RVA: 0x005778BC File Offset: 0x005778BC
    public bool Move(Vector3 delta, OverlayCursorSpring spring, double now)
    {
      if (delta.LengthSquared() < 1E-06f)
      {
        return true;
      }
      int num = -1;
      for (int i = 0; i < 64; i++)
      {
        int num2 = (this.nextSlot + i) % 64;
        if (this.slotFreeAt[num2] <= now)
        {
          num = num2;
          break;
        }
      }
      int num3;
      if (num < 0)
      {
        num3 = this.SlotWaits;
        this.SlotWaits = num3 + 1;
        return false;
      }
      this.nextSlot = (num + 1) % 64;
      ValueTuple<ScalarKeyFrameAnimation, double> valueTuple = this.Curve(spring);
      ScalarKeyFrameAnimation item = valueTuple.Item1;
      double item2 = valueTuple.Item2;
      this.props.InsertVector3("D" + num.ToString(), -delta);
      this.props.InsertScalar("G" + num.ToString(), 1f);
      this.props.StartAnimation("G" + num.ToString(), item);
      this.slotFreeAt[num] = now + item2 + 0.03;
      this.rest += delta;
      this.props.InsertVector3("Rest", this.rest);
      num3 = this.Moves;
      this.Moves = num3 + 1;
      return true;
    }

    // Token: 0x06000F0D RID: 3853 RVA: 0x005779F0 File Offset: 0x005779F0
    [return: Nullable(new byte[] { 0, 1 })]
    private ValueTuple<ScalarKeyFrameAnimation, double> Curve(OverlayCursorSpring spring)
    {
      ValueTuple<double, double> valueTuple = new ValueTuple<double, double>(spring.Response, spring.DampingFraction);
      ValueTuple<ScalarKeyFrameAnimation, double> valueTuple2;
      if (this.curves.TryGetValue(valueTuple, out valueTuple2))
      {
        return valueTuple2;
      }
      double num = 6.283185307179586 / spring.Response;
      double dampingFraction = spring.DampingFraction;
      double num2 = ((dampingFraction < 1.0) ? (dampingFraction * num) : (num * (dampingFraction - Math.Sqrt(dampingFraction * dampingFraction - 1.0))));
      double num3 = Math.Clamp(Math.Log(40000.0) / num2, 0.2, 1.2);
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.compositor.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(num3);
      LinearEasingFunction linearEasingFunction = this.compositor.CreateLinearEasingFunction();
      for (int i = 0; i <= 48; i++)
      {
        float num4 = (float)i / 48f;
        float num5 = ((i == 48) ? 0f : ((float)spring.ReleasedDisplacement((double)num4 * num3).Item1));
        scalarKeyFrameAnimation.InsertKeyFrame(num4, num5, linearEasingFunction);
      }
      this.curves[valueTuple] = new ValueTuple<ScalarKeyFrameAnimation, double>(scalarKeyFrameAnimation, num3);
      return new ValueTuple<ScalarKeyFrameAnimation, double>(scalarKeyFrameAnimation, num3);
    }

    // Token: 0x04000621 RID: 1569
    private const int Groups = 8;

    // Token: 0x04000622 RID: 1570
    private const int PerGroup = 8;

    // Token: 0x04000623 RID: 1571
    private const int Slots = 64;

    // Token: 0x04000624 RID: 1572
    private const int Samples = 48;

    // Token: 0x04000625 RID: 1573
    private readonly Compositor compositor;

    // Token: 0x04000626 RID: 1574
    private readonly CompositionPropertySet props;

    // Token: 0x04000627 RID: 1575
    private readonly CompositionPropertySet sums;

    // Token: 0x04000628 RID: 1576
    private readonly double[] slotFreeAt = new double[64];

    // Token: 0x04000629 RID: 1577
    [TupleElementNames(new string[] { null, null, "Curve", "Seconds" })]
    [Nullable(new byte[] { 1, 0, 0, 1 })]
    private readonly Dictionary<ValueTuple<double, double>, ValueTuple<ScalarKeyFrameAnimation, double>> curves = new Dictionary<ValueTuple<double, double>, ValueTuple<ScalarKeyFrameAnimation, double>>();

    // Token: 0x0400062A RID: 1578
    private int nextSlot;

    // Token: 0x0400062B RID: 1579
    private Vector3 rest;
  }
}
