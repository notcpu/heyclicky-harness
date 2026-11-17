using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.UI;
using Microsoft.UI.Composition;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000137 RID: 311
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchSilhouette
  {
    // Token: 0x17000314 RID: 788
    // (get) Token: 0x060010E6 RID: 4326 RVA: 0x0058A104 File Offset: 0x0058A104
    public CompositionPropertySet Props { get; }

    // Token: 0x17000315 RID: 789
    // (get) Token: 0x060010E7 RID: 4327 RVA: 0x0058A10C File Offset: 0x0058A10C
    public ContainerVisual Root { get; }

    // Token: 0x17000316 RID: 790
    // (get) Token: 0x060010E8 RID: 4328 RVA: 0x0058A114 File Offset: 0x0058A114
    public CompositionRoundedRectangleGeometry ClipGeometry { get; }

    // Token: 0x060010E9 RID: 4329 RVA: 0x0058A11C File Offset: 0x0058A11C
    public NotchSilhouette(Compositor compositor, double envelopeWidth)
    {
      Dictionary<string, SpringValue> dictionary = new Dictionary<string, SpringValue>();
      dictionary["Width"] = new SpringValue(60.0, 0.05);
      dictionary["Height"] = new SpringValue(8.0, 0.05);
      dictionary["Radius"] = new SpringValue(14.0, 0.05);
      dictionary["Gap"] = new SpringValue(4.0, 0.05);
      dictionary["Concavity"] = new SpringValue(0.0, 0.001);
      dictionary["Fill"] = new SpringValue(1.0, 0.001);
      dictionary["Sliver"] = new SpringValue(1.0, 0.001);
      this.knobs = dictionary;
      base..ctor();
      this.c = compositor;
      this.envelopeWidth = (float)envelopeWidth;
      this.Props = this.c.CreatePropertySet();
      this.Props.InsertScalar("Width", 60f);
      this.Props.InsertScalar("Height", 8f);
      this.Props.InsertScalar("Radius", 14f);
      this.Props.InsertScalar("Gap", 4f);
      this.Props.InsertScalar("Concavity", 0f);
      this.Props.InsertScalar("Fill", 1f);
      this.Props.InsertScalar("Sliver", 1f);
      this.Props.InsertScalar("EnvW", this.envelopeWidth);
      this.Props.InsertScalar("Px", this.Hairline);
      this.Root = this.c.CreateContainerVisual();
      this.Root.RelativeSizeAdjustment = Vector2.One;
      ContainerVisual containerVisual = this.c.CreateContainerVisual();
      containerVisual.RelativeSizeAdjustment = Vector2.One;
      ShapeVisual shapeVisual = this.c.CreateShapeVisual();
      shapeVisual.RelativeSizeAdjustment = Vector2.One;
      containerVisual.Children.InsertAtTop(shapeVisual);
      CompositionColorBrush compositionColorBrush = this.c.CreateColorBrush(Colors.Black);
      containerVisual.Children.InsertAtTop(this.Shoulder(true, compositionColorBrush));
      containerVisual.Children.InsertAtTop(this.Shoulder(false, compositionColorBrush));
      CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry = this.c.CreateRoundedRectangleGeometry();
      this.Bind(compositionRoundedRectangleGeometry, "Offset", "Vector2((p.EnvW - p.Width) / 2 + 10 * p.Concavity, p.Gap - p.Radius * p.Concavity)");
      this.Bind(compositionRoundedRectangleGeometry, "Size", "Vector2(p.Width - 20 * p.Concavity, p.Height + p.Radius * p.Concavity)");
      this.Bind(compositionRoundedRectangleGeometry, "CornerRadius", "Vector2((0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2), (0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2))");
      CompositionSpriteShape compositionSpriteShape = this.c.CreateSpriteShape(compositionRoundedRectangleGeometry);
      compositionSpriteShape.FillBrush = compositionColorBrush;
      shapeVisual.Shapes.Add(compositionSpriteShape);
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation("p.Fill");
      expressionAnimation.SetReferenceParameter("p", this.Props);
      containerVisual.StartAnimation("Opacity", expressionAnimation);
      this.Root.Children.InsertAtTop(containerVisual);
      ShapeVisual shapeVisual2 = this.c.CreateShapeVisual();
      shapeVisual2.RelativeSizeAdjustment = Vector2.One;
      CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry2 = this.c.CreateRoundedRectangleGeometry();
      this.BindHairline(compositionRoundedRectangleGeometry2);
      this.sliverBorder = this.c.CreateSpriteShape(compositionRoundedRectangleGeometry2);
      this.sliverBorder.StrokeBrush = this.c.CreateColorBrush(DS.White(0.24));
      this.sliverBorder.StrokeThickness = this.Hairline;
      shapeVisual2.Shapes.Add(this.sliverBorder);
      ExpressionAnimation expressionAnimation2 = this.c.CreateExpressionAnimation("p.Sliver * Clamp((16 - p.Height) / 8, 0, 1)");
      expressionAnimation2.SetReferenceParameter("p", this.Props);
      shapeVisual2.StartAnimation("Opacity", expressionAnimation2);
      this.Root.Children.InsertAtTop(shapeVisual2);
      this.ClipGeometry = this.c.CreateRoundedRectangleGeometry();
      this.Bind(this.ClipGeometry, "Offset", "Vector2((p.EnvW - p.Width) / 2 + 10 * p.Concavity, p.Gap - p.Radius * p.Concavity)");
      this.Bind(this.ClipGeometry, "Size", "Vector2(p.Width - 20 * p.Concavity, p.Height + p.Radius * p.Concavity)");
      this.Bind(this.ClipGeometry, "CornerRadius", "Vector2((0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2), (0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2))");
    }

    // Token: 0x060010EA RID: 4330 RVA: 0x0058A568 File Offset: 0x0058A568
    public void BindHairline(CompositionRoundedRectangleGeometry geometry)
    {
      this.Bind(geometry, "Offset", "Vector2((p.EnvW - p.Width) / 2 + 10 * p.Concavity + p.Px / 2, p.Gap + p.Px / 2)");
      this.Bind(geometry, "Size", "Vector2(p.Width - 20 * p.Concavity - p.Px, p.Height - p.Px)");
      this.Bind(geometry, "CornerRadius", "Vector2((Min(p.Width - 20 * p.Concavity, p.Height) - p.Px) / 2, (Min(p.Width - 20 * p.Concavity, p.Height) - p.Px) / 2)");
    }

    // Token: 0x17000317 RID: 791
    // (get) Token: 0x060010EB RID: 4331 RVA: 0x0058A5A0 File Offset: 0x0058A5A0
    // (set) Token: 0x060010EC RID: 4332 RVA: 0x0058A5A8 File Offset: 0x0058A5A8
    public float Hairline { get; private set; } = 0.5f;

    // Token: 0x060010ED RID: 4333 RVA: 0x0058A5B4 File Offset: 0x0058A5B4
    public void SetDeviceScale(double scale)
    {
      this.Hairline = (float)(1.0 / Math.Max(1.0, scale));
      this.Props.InsertScalar("Px", this.Hairline);
      this.sliverBorder.StrokeThickness = this.Hairline;
      Action<float> hairlineChanged = this.HairlineChanged;
      if (hairlineChanged != null)
      {
        hairlineChanged(this.Hairline);
      }
      NotchSystemShape notchSystemShape = this.twin;
      if (notchSystemShape == null)
      {
        return;
      }
      notchSystemShape.SetHairline(this.Hairline);
    }

    // Token: 0x060010EE RID: 4334 RVA: 0x0058A638 File Offset: 0x0058A638
    public void AttachTwin(NotchSystemShape shape)
    {
      this.twin = shape;
      foreach (KeyValuePair<string, SpringValue> keyValuePair in this.knobs)
      {
        string text;
        SpringValue springValue;
        keyValuePair.Deconstruct(out text, out springValue);
        string text2 = text;
        SpringValue springValue2 = springValue;
        shape.Props.InsertScalar(text2, (float)springValue2.Target);
      }
      shape.SetHairline(this.Hairline);
    }

    // Token: 0x14000040 RID: 64
    // (add) Token: 0x060010EF RID: 4335 RVA: 0x0058A6BC File Offset: 0x0058A6BC
    // (remove) Token: 0x060010F0 RID: 4336 RVA: 0x0058A6F4 File Offset: 0x0058A6F4
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<float> HairlineChanged;

    // Token: 0x060010F1 RID: 4337 RVA: 0x0058A72C File Offset: 0x0058A72C
    private ShapeVisual Shoulder(bool left, CompositionBrush fill)
    {
      ShapeVisual shapeVisual = this.c.CreateShapeVisual();
      shapeVisual.Size = new Vector2(10f, 10f);
      shapeVisual.Clip = this.c.CreateInsetClip();
      CompositionEllipseGeometry compositionEllipseGeometry = this.c.CreateEllipseGeometry();
      compositionEllipseGeometry.Center = new Vector2(left ? 0f : 10f, 10f);
      compositionEllipseGeometry.Radius = new Vector2(13f);
      CompositionSpriteShape compositionSpriteShape = this.c.CreateSpriteShape(compositionEllipseGeometry);
      compositionSpriteShape.StrokeBrush = fill;
      compositionSpriteShape.StrokeThickness = 6f;
      shapeVisual.Shapes.Add(compositionSpriteShape);
      shapeVisual.CenterPoint = new Vector3(left ? 10f : 0f, 0f, 0f);
      this.Bind(shapeVisual, "Offset", left ? "Vector3((p.EnvW - p.Width) / 2, p.Gap, 0)" : "Vector3((p.EnvW + p.Width) / 2 - 10, p.Gap, 0)");
      this.Bind(shapeVisual, "Scale", "Vector3(p.Concavity, p.Concavity, 1)");
      return shapeVisual;
    }

    // Token: 0x060010F2 RID: 4338 RVA: 0x0058A824 File Offset: 0x0058A824
    private void Bind(CompositionObject target, string property, string expression)
    {
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(expression);
      expressionAnimation.SetReferenceParameter("p", this.Props);
      target.StartAnimation(property, expressionAnimation);
    }

    // Token: 0x060010F3 RID: 4339 RVA: 0x0058A858 File Offset: 0x0058A858
    public void AnimateTo(double width, double height, double radius, double gap, double concavity, double fill, double sliver, SwiftSpring? spring)
    {
      NotchSilhouette.<>c__DisplayClass32_0 CS$<>8__locals1;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.spring = spring;
      if (Anim.Instant || Anim.ReduceMotion)
      {
        CS$<>8__locals1.spring = null;
      }
      if (concavity > 0.0)
      {
        width = Math.Round(width / (double)(2f * this.Hairline)) * 2.0 * (double)this.Hairline;
      }
      this.<AnimateTo>g__Go|32_0("Width", width, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Height", height, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Radius", radius, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Gap", gap, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Concavity", concavity, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Fill", fill, ref CS$<>8__locals1);
      this.<AnimateTo>g__Go|32_0("Sliver", sliver, ref CS$<>8__locals1);
    }

    // Token: 0x060010F4 RID: 4340 RVA: 0x0058A92C File Offset: 0x0058A92C
    [CompilerGenerated]
    private void <AnimateTo>g__Go|32_0(string name, double v, ref NotchSilhouette.<>c__DisplayClass32_0 A_3)
    {
      SpringMove springMove = this.knobs[name].Move(v, A_3.spring);
      if (springMove == null)
      {
        return;
      }
      this.Props.StartAnimation(name, springMove.Animation(this.c));
      NotchSystemShape notchSystemShape = this.twin;
      if (notchSystemShape == null)
      {
        return;
      }
      notchSystemShape.Play(name, springMove);
    }

    // Token: 0x04000784 RID: 1924
    private readonly Compositor c;

    // Token: 0x04000785 RID: 1925
    private readonly float envelopeWidth;

    // Token: 0x04000786 RID: 1926
    private const float TopRadius = 10f;

    // Token: 0x04000787 RID: 1927
    public const double SliverFill = 1.0;

    // Token: 0x04000788 RID: 1928
    internal const double SliverBorderAlpha = 0.24;

    // Token: 0x04000789 RID: 1929
    [Nullable(2)]
    private NotchSystemShape twin;

    // Token: 0x0400078A RID: 1930
    private readonly CompositionSpriteShape sliverBorder;

    // Token: 0x0400078B RID: 1931
    internal const string RoundedCorners = "Vector2((0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2), (0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2))";

    // Token: 0x0400078C RID: 1932
    public const string OnBar = "p.Sliver * Clamp((16 - p.Height) / 8, 0, 1)";

    // Token: 0x0400078F RID: 1935
    private readonly Dictionary<string, SpringValue> knobs;
  }
}
