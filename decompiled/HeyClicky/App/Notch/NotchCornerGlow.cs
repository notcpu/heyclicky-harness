using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Design;
using HeyClicky.App.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.UI;
using Windows.UI;
using Windows.UI.Composition;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000134 RID: 308
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchCornerGlow
  {
    // Token: 0x17000310 RID: 784
    // (get) Token: 0x060010C0 RID: 4288 RVA: 0x00587E80 File Offset: 0x00587E80
    public ContainerVisual Root { get; }

    // Token: 0x060010C1 RID: 4289 RVA: 0x00587E88 File Offset: 0x00587E88
    public NotchCornerGlow(Compositor compositor, CompositionPropertySet silhouetteProps, CompositionGeometry clip)
    {
      this.c = compositor;
      this.props = silhouetteProps;
      this.Root = this.c.CreateContainerVisual();
      this.Root.RelativeSizeAdjustment = Vector2.One;
      this.Root.Opacity = 0f;
      this.pulseLayer = this.c.CreateContainerVisual();
      this.pulseLayer.RelativeSizeAdjustment = Vector2.One;
      this.Root.Children.InsertAtTop(this.pulseLayer);
      ContainerVisual containerVisual = this.Part(this.c.CreateGeometricClip(clip));
      ContainerVisual containerVisual2 = this.Part(this.TrailingShoulderClip());
      for (int i = 0; i < 3; i++)
      {
        ValueTuple<float, float, float, float> valueTuple = NotchCornerGlow.Specs[i];
        float item = valueTuple.Item1;
        float item2 = valueTuple.Item2;
        float item3 = valueTuple.Item3;
        CompositionRadialGradientBrush compositionRadialGradientBrush = this.c.CreateRadialGradientBrush();
        compositionRadialGradientBrush.MappingMode = CompositionMappingMode.Absolute;
        this.blobStops[i] = new CompositionColorGradientStop[NotchCornerGlow.BlobStops.Length];
        for (int j = 0; j < NotchCornerGlow.BlobStops.Length; j++)
        {
          this.blobStops[i][j] = this.c.CreateColorGradientStop(NotchCornerGlow.BlobStops[j], Colors.Transparent);
          compositionRadialGradientBrush.ColorStops.Add(this.blobStops[i][j]);
        }
        this.Bind(compositionRadialGradientBrush, "EllipseCenter", FormattableString.Invariant(FormattableStringFactory.Create("Vector2(p.Width * {0:0.###}, p.Height * {1:0.###})", new object[]
        {
          1f - item,
          item2
        })));
        this.Bind(compositionRadialGradientBrush, "EllipseRadius", FormattableString.Invariant(FormattableStringFactory.Create("Vector2(Max(p.Height, 40) * {0:0.###}, Max(p.Height, 40) * {1:0.###})", new object[] { item3, item3 })));
        CompositionRadialGradientBrush compositionRadialGradientBrush2 = this.c.CreateRadialGradientBrush();
        compositionRadialGradientBrush2.MappingMode = CompositionMappingMode.Absolute;
        for (int k = 0; k < NotchCornerGlow.MaskStops.Length; k++)
        {
          compositionRadialGradientBrush2.ColorStops.Add(this.c.CreateColorGradientStop(NotchCornerGlow.MaskStops[k], Color.FromArgb((byte)(NotchCornerGlow.MaskAlphas[k] * 255f), byte.MaxValue, byte.MaxValue, byte.MaxValue)));
        }
        this.Bind(compositionRadialGradientBrush2, "EllipseCenter", "Vector2(p.Width, p.Height)");
        this.Bind(compositionRadialGradientBrush2, "EllipseRadius", "Vector2(p.Height * 1.6, p.Height * 1.6)");
        CompositionMaskBrush compositionMaskBrush = this.c.CreateMaskBrush();
        compositionMaskBrush.Source = compositionRadialGradientBrush;
        compositionMaskBrush.Mask = compositionRadialGradientBrush2;
        SpriteVisual spriteVisual = this.Sprite(compositionMaskBrush);
        containerVisual.Children.InsertAtTop(spriteVisual);
        containerVisual2.Children.InsertAtTop(this.Sprite(compositionMaskBrush));
        this.blobs[i] = new ValueTuple<SpriteVisual, CompositionRadialGradientBrush>(spriteVisual, compositionRadialGradientBrush);
      }
    }

    // Token: 0x060010C2 RID: 4290 RVA: 0x00588158 File Offset: 0x00588158
    private ContainerVisual Part(CompositionClip clip)
    {
      ContainerVisual containerVisual = this.c.CreateContainerVisual();
      containerVisual.RelativeSizeAdjustment = Vector2.One;
      containerVisual.Clip = clip;
      this.pulseLayer.Children.InsertAtTop(containerVisual);
      return containerVisual;
    }

    // Token: 0x060010C3 RID: 4291 RVA: 0x00588198 File Offset: 0x00588198
    private SpriteVisual Sprite(CompositionBrush brush)
    {
      SpriteVisual spriteVisual = this.c.CreateSpriteVisual();
      spriteVisual.Brush = brush;
      this.Bind(spriteVisual, "Offset", "Vector3((p.EnvW - p.Width) / 2, p.Gap, 0)");
      this.Bind(spriteVisual, "Size", "Vector2(p.Width, p.Height)");
      return spriteVisual;
    }

    // Token: 0x060010C4 RID: 4292 RVA: 0x005881DC File Offset: 0x005881DC
    private CompositionGeometricClip TrailingShoulderClip()
    {
      float num = 10f;
      CanvasPathBuilder canvasPathBuilder = new CanvasPathBuilder(CanvasDevice.GetSharedDevice());
      canvasPathBuilder.BeginFigure(0f, 0f);
      canvasPathBuilder.AddLine(num, 0f);
      canvasPathBuilder.AddArc(new Vector2(0f, num), num, num, 0f, CanvasSweepDirection.CounterClockwise, CanvasArcSize.Small);
      canvasPathBuilder.EndFigure(CanvasFigureLoop.Closed);
      CompositionGeometricClip compositionGeometricClip = this.c.CreateGeometricClip(this.c.CreatePathGeometry(new CompositionPath(CanvasGeometry.CreatePath(canvasPathBuilder))));
      this.Bind(compositionGeometricClip, "Offset", FormattableString.Invariant(FormattableStringFactory.Create("Vector2((p.EnvW + p.Width) / 2 - {0}, p.Gap)", new object[] { num })));
      this.Bind(compositionGeometricClip, "Scale", "Vector2(p.Concavity, p.Concavity)");
      return compositionGeometricClip;
    }

    // Token: 0x060010C5 RID: 4293 RVA: 0x00588298 File Offset: 0x00588298
    private void Bind(CompositionObject target, string property, string expression)
    {
      ExpressionAnimation expressionAnimation = this.c.CreateExpressionAnimation(expression);
      expressionAnimation.SetReferenceParameter("p", this.props);
      target.StartAnimation(property, expressionAnimation);
    }

    // Token: 0x060010C6 RID: 4294 RVA: 0x005882CC File Offset: 0x005882CC
    [return: Nullable(2)]
    private static NotchCornerGlow.Appearance Resolve(NotchPresentation p, Color? agentTint)
    {
      NotchPresentation.LiveEvent liveEvent = p as NotchPresentation.LiveEvent;
      if (liveEvent != null)
      {
        NotchLiveEvent @event = liveEvent.Event;
        if (!NotchGeometry.IsWorkingBar(p))
        {
          if (@event is NotchLiveEvent.ActivityListening)
          {
            return new NotchCornerGlow.Appearance(DS.Rgb(0.1, 0.92, 1.0, 1.0), DS.Rgb(0.2, 0.5, 1.0, 1.0), DS.Rgb(0.55, 1.0, 1.0, 1.0), 1.0, NotchCornerGlow.Style.Wave, 6.0, 8.0, 3.0, 0.92, 0.18);
          }
          if (!(@event is NotchLiveEvent.ActivityThinking) && !(@event is NotchLiveEvent.ActivityCustomStatus) && !(@event is NotchLiveEvent.AgentMultiActivity))
          {
            if (@event is NotchLiveEvent.Dictation)
            {
              return new NotchCornerGlow.Appearance(DS.Rgb(0.129, 0.773, 0.369, 1.0), DS.Rgb(0.05, 0.6, 0.4, 1.0), DS.Rgb(0.55, 0.95, 0.65, 1.0), 1.0, NotchCornerGlow.Style.Wave, 5.0, 8.0, 2.6, 0.92, 0.18);
            }
            if (@event is NotchLiveEvent.ActivitySpeaking)
            {
              return new NotchCornerGlow.Appearance(DS.Rgb(1.0, 0.5, 0.2, 1.0), DS.Rgb(1.0, 0.25, 0.45, 1.0), DS.Rgb(1.0, 0.75, 0.35, 1.0), 1.0, NotchCornerGlow.Style.Pulse, 6.0, 10.0, 3.0, 0.92, 0.14);
            }
            if (@event is NotchLiveEvent.RestingMark)
            {
              Color color = DS.Hex(1755979U, 1.0);
              return new NotchCornerGlow.Appearance(color, color.WithAlpha(0.85), color.WithAlpha(0.95), 1.0, NotchCornerGlow.Style.Orbit, 11.0, 10.0, 5.5, 0.92, 0.18);
            }
            if (!(@event is NotchLiveEvent.AgentTask))
            {
              if (@event is NotchLiveEvent.IntegrationConnected || @event is NotchLiveEvent.HandoffQueued || @event is NotchLiveEvent.HandoffSent)
              {
                return new NotchCornerGlow.Appearance(DS.Rgb(0.35, 0.95, 0.35, 1.0), DS.Rgb(0.65, 1.0, 0.4, 1.0), DS.Rgb(0.3, 0.9, 0.55, 1.0), 1.0, NotchCornerGlow.Style.Pulse, 5.0, 7.0, 2.5, 0.9, 0.18);
              }
              if (!(@event is NotchLiveEvent.IntegrationSuggestion) && !(@event is NotchLiveEvent.SidecarLearningSuggestion) && !(@event is NotchLiveEvent.SpotifyVoiceTip))
              {
                return null;
              }
              return new NotchCornerGlow.Appearance(DS.Colors.CursorBlue, DS.Rgb(0.25, 0.75, 1.0, 1.0), DS.Rgb(0.7, 0.9, 1.0, 1.0), 0.85, NotchCornerGlow.Style.Orbit, 12.0, 6.0, 6.0, 0.9, 0.12);
            }
            else
            {
              if (agentTint != null)
              {
                Color valueOrDefault = agentTint.GetValueOrDefault();
                return new NotchCornerGlow.Appearance(valueOrDefault, valueOrDefault.WithAlpha(0.85), valueOrDefault.WithAlpha(0.95), 1.0, NotchCornerGlow.Style.Orbit, 9.0, 10.0, 4.5, 0.93, 0.16);
              }
              return new NotchCornerGlow.Appearance(DS.Rgb(0.2, 0.95, 0.55, 1.0), DS.Rgb(0.3, 0.85, 1.0, 1.0), DS.Rgb(0.65, 1.0, 0.4, 1.0), 1.0, NotchCornerGlow.Style.Orbit, 9.0, 10.0, 4.5, 0.93, 0.16);
            }
          }
          else
          {
            if (agentTint != null)
            {
              Color valueOrDefault2 = agentTint.GetValueOrDefault();
              return new NotchCornerGlow.Appearance(valueOrDefault2, valueOrDefault2.WithAlpha(0.85), valueOrDefault2.WithAlpha(0.95), 1.0, NotchCornerGlow.Style.Orbit, 11.0, 10.0, 5.5, 0.92, 0.18);
            }
            return new NotchCornerGlow.Appearance(DS.Rgb(0.85, 0.15, 1.0, 1.0), DS.Rgb(1.0, 0.2, 0.7, 1.0), DS.Rgb(0.55, 0.35, 1.0, 1.0), 1.0, NotchCornerGlow.Style.Orbit, 11.0, 10.0, 5.5, 0.92, 0.18);
          }
        }
      }
      return null;
    }

    // Token: 0x060010C7 RID: 4295 RVA: 0x005889C8 File Offset: 0x005889C8
    public void Update(NotchPresentation p, Color? agentTint = null)
    {
      NotchCornerGlow.Appearance appearance = NotchCornerGlow.Resolve(p, agentTint);
      if (appearance == this.current)
      {
        return;
      }
      NotchCornerGlow.Appearance appearance2 = this.current;
      this.current = appearance;
      if (appearance == null)
      {
        this.FadeTo(0f, 0.34);
        return;
      }
      bool flag = appearance2 != null && !Anim.Instant;
      Color[] array = new Color[] { appearance.Primary, appearance.Secondary, appearance.Tertiary };
      for (int i = 0; i < 3; i++)
      {
        Color color = array[i];
        for (int j = 0; j < NotchCornerGlow.BlobStops.Length; j++)
        {
          this.SetStopColor(this.blobStops[i][j], Color.FromArgb((byte)Math.Round((double)((float)color.A * NotchCornerGlow.Specs[i].Item4 * NotchCornerGlow.BlobFactors[j])), color.R, color.G, color.B), flag);
        }
      }
      if (!this.motionInstalled || appearance2 == null || !NotchCornerGlow.SameMotion(appearance2, appearance))
      {
        for (int k = 0; k < 3; k++)
        {
          this.InstallMotion(this.blobs[k].Item2, appearance, k);
        }
        this.InstallPulse(appearance);
        this.motionInstalled = true;
      }
      this.FadeTo((float)appearance.Opacity, 0.34);
    }

    // Token: 0x060010C8 RID: 4296 RVA: 0x00588B3C File Offset: 0x00588B3C
    private static bool SameMotion(NotchCornerGlow.Appearance x, NotchCornerGlow.Appearance y)
    {
      return x.Style == y.Style && x.Cycle == y.Cycle && x.Amplitude == y.Amplitude && x.PulseCycle == y.PulseCycle && x.PulseCenter == y.PulseCenter && x.PulseAmplitude == y.PulseAmplitude;
    }

    // Token: 0x060010C9 RID: 4297 RVA: 0x00588BA0 File Offset: 0x00588BA0
    private void SetStopColor(CompositionColorGradientStop stop, Color color, bool crossFade)
    {
      if (!crossFade)
      {
        stop.StopAnimation("Color");
        stop.Color = color;
        return;
      }
      ColorKeyFrameAnimation colorKeyFrameAnimation = this.c.CreateColorKeyFrameAnimation();
      colorKeyFrameAnimation.InsertKeyFrame(1f, color, SystemEasing.Expressive(this.c));
      colorKeyFrameAnimation.Duration = TimeSpan.FromSeconds(0.34);
      stop.StartAnimation("Color", colorKeyFrameAnimation);
    }

    // Token: 0x060010CA RID: 4298 RVA: 0x00588C08 File Offset: 0x00588C08
    private void FadeTo(float opacity, double seconds)
    {
      if (Anim.Instant)
      {
        this.Root.StopAnimation("Opacity");
        this.Root.Opacity = opacity;
        return;
      }
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      scalarKeyFrameAnimation.InsertKeyFrame(1f, opacity, SystemEasing.Expressive(this.c));
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(seconds);
      this.Root.StartAnimation("Opacity", scalarKeyFrameAnimation);
    }

    // Token: 0x060010CB RID: 4299 RVA: 0x00588C7C File Offset: 0x00588C7C
    private void InstallMotion(CompositionRadialGradientBrush brush, NotchCornerGlow.Appearance a, int blobIndex)
    {
      if (a.Amplitude == 0.0 || a.Cycle <= 0.0)
      {
        brush.StopAnimation("Offset");
        brush.Offset = Vector2.Zero;
        return;
      }
      double num = (double)blobIndex * 3.141592653589793 / 2.0;
      double amplitude = a.Amplitude;
      NotchCornerGlow.Style style = a.Style;
      ValueTuple<double, double, double, double> valueTuple;
      if (style != NotchCornerGlow.Style.Wave)
      {
        if (style != NotchCornerGlow.Style.Orbit)
        {
          valueTuple = new ValueTuple<double, double, double, double>(amplitude * 0.45 * -1.0, amplitude * 0.45, num, a.Cycle);
        }
        else
        {
          valueTuple = new ValueTuple<double, double, double, double>(amplitude * -1.0, amplitude, num, a.Cycle);
        }
      }
      else
      {
        valueTuple = new ValueTuple<double, double, double, double>(amplitude * -1.0, -amplitude * 0.18, num - 1.5707963267948966, a.Cycle);
      }
      ValueTuple<double, double, double, double> valueTuple2 = valueTuple;
      Vector2KeyFrameAnimation vector2KeyFrameAnimation = this.c.CreateVector2KeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = this.c.CreateLinearEasingFunction();
      for (int i = 0; i <= 64; i++)
      {
        double num2 = (double)i / 64.0 * valueTuple2.Item4 * 2.0 * 3.141592653589793 / valueTuple2.Item4 + valueTuple2.Item3;
        vector2KeyFrameAnimation.InsertKeyFrame((float)i / 64f, new Vector2((float)(valueTuple2.Item1 * Math.Cos(num2)), (float)(valueTuple2.Item2 * Math.Sin(num2))), linearEasingFunction);
      }
      vector2KeyFrameAnimation.Duration = TimeSpan.FromSeconds(valueTuple2.Item4);
      vector2KeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      brush.StartAnimation("Offset", vector2KeyFrameAnimation);
    }

    // Token: 0x060010CC RID: 4300 RVA: 0x00588E2C File Offset: 0x00588E2C
    private void InstallPulse(NotchCornerGlow.Appearance a)
    {
      ScalarKeyFrameAnimation scalarKeyFrameAnimation = this.c.CreateScalarKeyFrameAnimation();
      LinearEasingFunction linearEasingFunction = this.c.CreateLinearEasingFunction();
      for (int i = 0; i <= 64; i++)
      {
        double num = (double)i / 64.0 * a.PulseCycle;
        double num2 = a.PulseCenter + Math.Sin(num * 2.0 * 3.141592653589793 / a.PulseCycle) * a.PulseAmplitude;
        scalarKeyFrameAnimation.InsertKeyFrame((float)i / 64f, (float)Math.Clamp(num2, 0.0, 1.0), linearEasingFunction);
      }
      scalarKeyFrameAnimation.Duration = TimeSpan.FromSeconds(a.PulseCycle);
      scalarKeyFrameAnimation.IterationBehavior = AnimationIterationBehavior.Forever;
      this.pulseLayer.StartAnimation("Opacity", scalarKeyFrameAnimation);
    }

    // Token: 0x060010CD RID: 4301 RVA: 0x00588EF8 File Offset: 0x00588EF8
    public void Quiesce()
    {
      this.pulseLayer.StopAnimation("Opacity");
      ValueTuple<SpriteVisual, CompositionRadialGradientBrush>[] array = this.blobs;
      for (int i = 0; i < array.Length; i++)
      {
        array[i].Item2.StopAnimation("Offset");
      }
      this.motionInstalled = false;
    }

    // Token: 0x04000759 RID: 1881
    private readonly Compositor c;

    // Token: 0x0400075A RID: 1882
    private readonly CompositionPropertySet props;

    // Token: 0x0400075B RID: 1883
    private readonly ContainerVisual pulseLayer;

    // Token: 0x0400075C RID: 1884
    [TupleElementNames(new string[] { "sprite", "blob" })]
    [Nullable(new byte[] { 1, 0, 1, 1 })]
    private readonly ValueTuple<SpriteVisual, CompositionRadialGradientBrush>[] blobs = new ValueTuple<SpriteVisual, CompositionRadialGradientBrush>[3];

    // Token: 0x0400075D RID: 1885
    private readonly CompositionColorGradientStop[][] blobStops = new CompositionColorGradientStop[3][];

    // Token: 0x0400075E RID: 1886
    [Nullable(2)]
    private NotchCornerGlow.Appearance current;

    // Token: 0x0400075F RID: 1887
    private bool motionInstalled;

    // Token: 0x04000760 RID: 1888
    [TupleElementNames(new string[] { "ax", "ay", "k", "intensity" })]
    [Nullable(new byte[] { 1, 0 })]
    private static readonly ValueTuple<float, float, float, float>[] Specs = new ValueTuple<float, float, float, float>[]
    {
      new ValueTuple<float, float, float, float>(0.05f, 1f, 1.05f, 1f),
      new ValueTuple<float, float, float, float>(0.22f, 0.95f, 0.85f, 1f),
      new ValueTuple<float, float, float, float>(0.42f, 0.92f, 0.65f, 0.9f)
    };

    // Token: 0x04000761 RID: 1889
    private static readonly float[] BlobStops = new float[] { 0f, 0.35f, 0.65f, 0.85f, 1f };

    // Token: 0x04000762 RID: 1890
    private static readonly float[] BlobFactors = new float[] { 1f, 0.95f, 0.7f, 0.3f, 0f };

    // Token: 0x04000763 RID: 1891
    private static readonly float[] MaskStops = new float[] { 0f, 0.42f, 0.55f, 0.68f, 0.8f, 0.9f, 0.97f, 1f };

    // Token: 0x04000764 RID: 1892
    private static readonly float[] MaskAlphas = new float[] { 1f, 1f, 0.88f, 0.65f, 0.38f, 0.18f, 0.05f, 0f };

    // Token: 0x0200067C RID: 1660
    [NullableContext(0)]
    private enum Style
    {
      // Token: 0x04001D9E RID: 7582
      Wave,
      // Token: 0x04001D9F RID: 7583
      Orbit,
      // Token: 0x04001DA0 RID: 7584
      Pulse
    }

    // Token: 0x0200067D RID: 1661
    [Nullable(0)]
    private sealed class Appearance : IEquatable<NotchCornerGlow.Appearance>
    {
      // Token: 0x06003227 RID: 12839 RVA: 0x0067162C File Offset: 0x0067162C
      public Appearance(Color Primary, Color Secondary, Color Tertiary, double Opacity, NotchCornerGlow.Style Style, double Cycle, double Amplitude, double PulseCycle, double PulseCenter, double PulseAmplitude)
      {
        this.Primary = Primary;
        this.Secondary = Secondary;
        this.Tertiary = Tertiary;
        this.Opacity = Opacity;
        this.Style = Style;
        this.Cycle = Cycle;
        this.Amplitude = Amplitude;
        this.PulseCycle = PulseCycle;
        this.PulseCenter = PulseCenter;
        this.PulseAmplitude = PulseAmplitude;
        base..ctor();
      }

      // Token: 0x1700078E RID: 1934
      // (get) Token: 0x06003228 RID: 12840 RVA: 0x0067168C File Offset: 0x0067168C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(NotchCornerGlow.Appearance);
        }
      }

      // Token: 0x1700078F RID: 1935
      // (get) Token: 0x06003229 RID: 12841 RVA: 0x00671698 File Offset: 0x00671698
      // (set) Token: 0x0600322A RID: 12842 RVA: 0x006716A0 File Offset: 0x006716A0
      public Color Primary { get; set; }

      // Token: 0x17000790 RID: 1936
      // (get) Token: 0x0600322B RID: 12843 RVA: 0x006716AC File Offset: 0x006716AC
      // (set) Token: 0x0600322C RID: 12844 RVA: 0x006716B4 File Offset: 0x006716B4
      public Color Secondary { get; set; }

      // Token: 0x17000791 RID: 1937
      // (get) Token: 0x0600322D RID: 12845 RVA: 0x006716C0 File Offset: 0x006716C0
      // (set) Token: 0x0600322E RID: 12846 RVA: 0x006716C8 File Offset: 0x006716C8
      public Color Tertiary { get; set; }

      // Token: 0x17000792 RID: 1938
      // (get) Token: 0x0600322F RID: 12847 RVA: 0x006716D4 File Offset: 0x006716D4
      // (set) Token: 0x06003230 RID: 12848 RVA: 0x006716DC File Offset: 0x006716DC
      public double Opacity { get; set; }

      // Token: 0x17000793 RID: 1939
      // (get) Token: 0x06003231 RID: 12849 RVA: 0x006716E8 File Offset: 0x006716E8
      // (set) Token: 0x06003232 RID: 12850 RVA: 0x006716F0 File Offset: 0x006716F0
      public NotchCornerGlow.Style Style { get; set; }

      // Token: 0x17000794 RID: 1940
      // (get) Token: 0x06003233 RID: 12851 RVA: 0x006716FC File Offset: 0x006716FC
      // (set) Token: 0x06003234 RID: 12852 RVA: 0x00671704 File Offset: 0x00671704
      public double Cycle { get; set; }

      // Token: 0x17000795 RID: 1941
      // (get) Token: 0x06003235 RID: 12853 RVA: 0x00671710 File Offset: 0x00671710
      // (set) Token: 0x06003236 RID: 12854 RVA: 0x00671718 File Offset: 0x00671718
      public double Amplitude { get; set; }

      // Token: 0x17000796 RID: 1942
      // (get) Token: 0x06003237 RID: 12855 RVA: 0x00671724 File Offset: 0x00671724
      // (set) Token: 0x06003238 RID: 12856 RVA: 0x0067172C File Offset: 0x0067172C
      public double PulseCycle { get; set; }

      // Token: 0x17000797 RID: 1943
      // (get) Token: 0x06003239 RID: 12857 RVA: 0x00671738 File Offset: 0x00671738
      // (set) Token: 0x0600323A RID: 12858 RVA: 0x00671740 File Offset: 0x00671740
      public double PulseCenter { get; set; }

      // Token: 0x17000798 RID: 1944
      // (get) Token: 0x0600323B RID: 12859 RVA: 0x0067174C File Offset: 0x0067174C
      // (set) Token: 0x0600323C RID: 12860 RVA: 0x00671754 File Offset: 0x00671754
      public double PulseAmplitude { get; set; }

      // Token: 0x0600323D RID: 12861 RVA: 0x00671760 File Offset: 0x00671760
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Appearance");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600323E RID: 12862 RVA: 0x006717AC File Offset: 0x006717AC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Primary = ");
        builder.Append(this.Primary.ToString());
        builder.Append(", Secondary = ");
        builder.Append(this.Secondary.ToString());
        builder.Append(", Tertiary = ");
        builder.Append(this.Tertiary.ToString());
        builder.Append(", Opacity = ");
        builder.Append(this.Opacity.ToString());
        builder.Append(", Style = ");
        builder.Append(this.Style.ToString());
        builder.Append(", Cycle = ");
        builder.Append(this.Cycle.ToString());
        builder.Append(", Amplitude = ");
        builder.Append(this.Amplitude.ToString());
        builder.Append(", PulseCycle = ");
        builder.Append(this.PulseCycle.ToString());
        builder.Append(", PulseCenter = ");
        builder.Append(this.PulseCenter.ToString());
        builder.Append(", PulseAmplitude = ");
        builder.Append(this.PulseAmplitude.ToString());
        return true;
      }

      // Token: 0x0600323F RID: 12863 RVA: 0x00671948 File Offset: 0x00671948
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(NotchCornerGlow.Appearance left, NotchCornerGlow.Appearance right)
      {
        return !(left == right);
      }

      // Token: 0x06003240 RID: 12864 RVA: 0x00671954 File Offset: 0x00671954
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(NotchCornerGlow.Appearance left, NotchCornerGlow.Appearance right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003241 RID: 12865 RVA: 0x00671968 File Offset: 0x00671968
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Primary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Secondary>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Tertiary>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Opacity>k__BackingField)) * -1521134295 + EqualityComparer<NotchCornerGlow.Style>.Default.GetHashCode(this.<Style>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Cycle>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Amplitude>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<PulseCycle>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<PulseCenter>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<PulseAmplitude>k__BackingField);
      }

      // Token: 0x06003242 RID: 12866 RVA: 0x00671A6C File Offset: 0x00671A6C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as NotchCornerGlow.Appearance);
      }

      // Token: 0x06003243 RID: 12867 RVA: 0x00671A7C File Offset: 0x00671A7C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(NotchCornerGlow.Appearance other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Color>.Default.Equals(this.<Primary>k__BackingField, other.<Primary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Secondary>k__BackingField, other.<Secondary>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Tertiary>k__BackingField, other.<Tertiary>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Opacity>k__BackingField, other.<Opacity>k__BackingField) && EqualityComparer<NotchCornerGlow.Style>.Default.Equals(this.<Style>k__BackingField, other.<Style>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Cycle>k__BackingField, other.<Cycle>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Amplitude>k__BackingField, other.<Amplitude>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<PulseCycle>k__BackingField, other.<PulseCycle>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<PulseCenter>k__BackingField, other.<PulseCenter>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<PulseAmplitude>k__BackingField, other.<PulseAmplitude>k__BackingField));
      }

      // Token: 0x06003245 RID: 12869 RVA: 0x00671BB4 File Offset: 0x00671BB4
      [CompilerGenerated]
      private Appearance(NotchCornerGlow.Appearance original)
      {
        this.Primary = original.<Primary>k__BackingField;
        this.Secondary = original.<Secondary>k__BackingField;
        this.Tertiary = original.<Tertiary>k__BackingField;
        this.Opacity = original.<Opacity>k__BackingField;
        this.Style = original.<Style>k__BackingField;
        this.Cycle = original.<Cycle>k__BackingField;
        this.Amplitude = original.<Amplitude>k__BackingField;
        this.PulseCycle = original.<PulseCycle>k__BackingField;
        this.PulseCenter = original.<PulseCenter>k__BackingField;
        this.PulseAmplitude = original.<PulseAmplitude>k__BackingField;
      }

      // Token: 0x06003246 RID: 12870 RVA: 0x00671C40 File Offset: 0x00671C40
      [CompilerGenerated]
      public void Deconstruct(out Color Primary, out Color Secondary, out Color Tertiary, out double Opacity, out NotchCornerGlow.Style Style, out double Cycle, out double Amplitude, out double PulseCycle, out double PulseCenter, out double PulseAmplitude)
      {
        Primary = this.Primary;
        Secondary = this.Secondary;
        Tertiary = this.Tertiary;
        Opacity = this.Opacity;
        Style = this.Style;
        Cycle = this.Cycle;
        Amplitude = this.Amplitude;
        PulseCycle = this.PulseCycle;
        PulseCenter = this.PulseCenter;
        PulseAmplitude = this.PulseAmplitude;
      }
    }
  }
}
