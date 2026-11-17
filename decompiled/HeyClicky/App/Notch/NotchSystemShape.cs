using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using HeyClicky.App.Design;
using Microsoft.UI;
using Windows.UI.Composition;

namespace HeyClicky.App.Notch
{
  // Token: 0x0200013B RID: 315
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchSystemShape
  {
    // Token: 0x17000327 RID: 807
    // (get) Token: 0x0600111E RID: 4382 RVA: 0x0058AD68 File Offset: 0x0058AD68
    public CompositionPropertySet Props { get; }

    // Token: 0x17000328 RID: 808
    // (get) Token: 0x0600111F RID: 4383 RVA: 0x0058AD70 File Offset: 0x0058AD70
    public ContainerVisual Root { get; }

    // Token: 0x17000329 RID: 809
    // (get) Token: 0x06001120 RID: 4384 RVA: 0x0058AD78 File Offset: 0x0058AD78
    public CompositionRoundedRectangleGeometry ClipGeometry { get; }

    // Token: 0x06001121 RID: 4385 RVA: 0x0058AD80 File Offset: 0x0058AD80
    public NotchSystemShape(Compositor compositor, double envelopeWidth)
    {
      this.c = compositor;
      this.Props = this.c.CreatePropertySet();
      this.Props.InsertScalar("Width", 60f);
      this.Props.InsertScalar("Height", 8f);
      this.Props.InsertScalar("Radius", 14f);
      this.Props.InsertScalar("Gap", 4f);
      this.Props.InsertScalar("Concavity", 0f);
      this.Props.InsertScalar("Fill", 1f);
      this.Props.InsertScalar("Sliver", 1f);
      this.Props.InsertScalar("EnvW", (float)envelopeWidth);
      this.Props.InsertScalar("Px", 0.5f);
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
      this.Bind(containerVisual, "Opacity", "p.Fill");
      this.Root.Children.InsertAtTop(containerVisual);
      ShapeVisual shapeVisual2 = this.c.CreateShapeVisual();
      shapeVisual2.RelativeSizeAdjustment = Vector2.One;
      CompositionRoundedRectangleGeometry compositionRoundedRectangleGeometry2 = this.c.CreateRoundedRectangleGeometry();
      this.BindHairline(compositionRoundedRectangleGeometry2);
      this.sliverBorder = this.c.CreateSpriteShape(compositionRoundedRectangleGeometry2);
      this.sliverBorder.StrokeBrush = this.c.CreateColorBrush(DS.White(0.24));
      this.sliverBorder.StrokeThickness = 0.5f;
      shapeVisual2.Shapes.Add(this.sliverBorder);
      this.Bind(shapeVisual2, "Opacity", "p.Sliver * Clamp((16 - p.Height) / 8, 0, 1)");
      this.Root.Children.InsertAtTop(shapeVisual2);
      this.ClipGeometry = this.c.CreateRoundedRectangleGeometry();
      this.Bind(this.ClipGeometry, "Offset", "Vector2((p.EnvW - p.Width) / 2 + 10 * p.Concavity, p.Gap - p.Radius * p.Concavity)");
      this.Bind(this.ClipGeometry, "Size", "Vector2(p.Width - 20 * p.Concavity, p.Height + p.Radius * p.Concavity)");
      this.Bind(this.ClipGeometry, "CornerRadius", "Vector2((0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2), (0.854 + 0.146 * p.Sliver) * Min(p.Radius, Min(p.Width - 20 * p.Concavity, p.Height) / 2))");
    }

    // Token: 0x06001122 RID: 4386 RVA: 0x0058B084 File Offset: 0x0058B084
    public void BindHairline(CompositionRoundedRectangleGeometry geometry)
    {
      this.Bind(geometry, "Offset", "Vector2((p.EnvW - p.Width) / 2 + 10 * p.Concavity + p.Px / 2, p.Gap + p.Px / 2)");
      this.Bind(geometry, "Size", "Vector2(p.Width - 20 * p.Concavity - p.Px, p.Height - p.Px)");
      this.Bind(geometry, "CornerRadius", "Vector2((Min(p.Width - 20 * p.Concavity, p.Height) - p.Px) / 2, (Min(p.Width - 20 * p.Concavity, p.Height) - p.Px) / 2)");
    }

    // Token: 0x1700032A RID: 810
    // (get) Token: 0x06001123 RID: 4387 RVA: 0x0058B0BC File Offset: 0x0058B0BC
    // (set) Token: 0x06001124 RID: 4388 RVA: 0x0058B0C4 File Offset: 0x0058B0C4
    public float Hairline { get; private set; } = 0.5f;

    // Token: 0x14000044 RID: 68
    // (add) Token: 0x06001125 RID: 4389 RVA: 0x0058B0D0 File Offset: 0x0058B0D0
    // (remove) Token: 0x06001126 RID: 4390 RVA: 0x0058B108 File Offset: 0x0058B108
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<float> HairlineChanged;

    // Token: 0x06001127 RID: 4391 RVA: 0x0058B140 File Offset: 0x0058B140
    public void SetHairline(float hairline)
    {
      this.Hairline = hairline;
      this.Props.InsertScalar("Px", hairline);
      this.sliverBorder.StrokeThickness = hairline;
      Action<float> hairlineChanged = this.HairlineChanged;
      if (hairlineChanged == null)
      {
        return;
      }
      hairlineChanged(hairline);
    }

    // Token: 0x06001128 RID: 4392 RVA: 0x0058B178 File Offset: 0x0058B178
    public void Play(string knob, SpringMove move)
    {
      this.Props.StartAnimation(knob, move.Animation(this.c));
    }

    // Token: 0x06001129 RID: 4393 RVA: 0x0058B194 File Offset: 0x0058B194
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

    // Token: 0x0600112A RID: 4394 RVA: 0x0058B28C File Offset: 0x0058B28C
    private void Bind(CompositionObject target, string property, string expression)
    {
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(expression);
      expressionAnimation.SetReferenceParameter("p", this.Props);
      target.StartAnimation(property, expressionAnimation);
    }

    // Token: 0x04000796 RID: 1942
    private readonly Compositor c;

    // Token: 0x04000797 RID: 1943
    private readonly CompositionSpriteShape sliverBorder;

    // Token: 0x04000798 RID: 1944
    private const float TopRadius = 10f;
  }
}
