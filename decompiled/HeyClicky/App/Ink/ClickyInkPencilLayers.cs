using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.UI;
using HeyClicky.Core.Ink;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020002A4 RID: 676
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkPencilLayers : Grid
  {
    // Token: 0x06001BD6 RID: 7126 RVA: 0x005F5440 File Offset: 0x005F5440
    public ClickyInkPencilLayers(ClickyInkPencilLayers.Motion motion, Color tint, double size)
    {
      ClickyInkPencilLayers <>4__this = this;
      base.IsHitTestVisible = false;
      FrameworkElement pencil = ClickyInkPencilGlyph.Make(size, tint, true);
      ElementCompositionPreview.SetIsTranslationEnabled(pencil, true);
      Grid holder = new Grid
      {
        Width = size,
        Height = size,
        IsHitTestVisible = false,
        Children = { pencil }
      };
      ElementCompositionPreview.SetIsTranslationEnabled(holder, true);
      Canvas canvas = new Canvas();
      canvas.Children.Add(holder);
      base.Children.Add(canvas);
      List<FrameworkElement> dots = new List<FrameworkElement>();
      int i;
      if (motion is ClickyInkPencilLayers.Motion.Resting)
      {
        for (i = 0; i < 3; i++)
        {
          Ellipse ellipse = new Ellipse
          {
            Width = 2.8,
            Height = 2.8,
            Fill = Ui.Brush(tint),
            Opacity = 0.0
          };
          canvas.Children.Add(ellipse);
          dots.Add(ellipse);
        }
      }
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        if (motion is ClickyInkPencilLayers.Motion.Tapping)
        {
          Canvas.SetLeft(holder, (<>4__this.ActualWidth - size) / 2.0);
          Canvas.SetTop(holder, (<>4__this.ActualHeight - size) / 2.0);
          pencil.Opacity = 0.8;
          return;
        }
        double num = <>4__this.ActualHeight * 0.78;
        Canvas.SetLeft(holder, 0.0);
        Canvas.SetTop(holder, num - size - 1.0);
        for (int j = 0; j < dots.Count; j++)
        {
          Canvas.SetLeft(dots[j], size + 4.0 + (double)j * 5.5);
          Canvas.SetTop(dots[j], num - 2.8);
        }
      };
      Action <>9__7;
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        pencil.CenterPoint = new Vector3((float)(size / 2.0), (float)(size / 2.0), 0f);
        if (motion is ClickyInkPencilLayers.Motion.Tapping)
        {
          Anim.LoopScalar(pencil, "Rotation", 0.64, (double t) => (float)(-6.0 - 14.0 * ClickyInkPencilLayers.<.ctor>g__Ease|1_2(t)), 48, 0.0);
          Anim.LoopVector3(pencil, "Translation", 0.64, (double t) => new Vector3(0f, (float)(1.0 - 3.0 * ClickyInkPencilLayers.<.ctor>g__Ease|1_2(t)), 0f), 48, 0.0);
          return;
        }
        double elapsed = ((ClickyInkPencilLayers.Motion.Resting)motion).Elapsed;
        if (elapsed < 0.5)
        {
          pencil.Opacity = 0.6 * ClickyInkClock.EaseInOut(elapsed / 0.5);
          Anim.Opacity(pencil, 0.6f, 0.5 - elapsed, null, 0.0);
          holder.Translation = new Vector3(0f, (float)(-4.0 * (1.0 - ClickyInkClock.EaseInOut(elapsed / 0.5))), 0f);
          Anim.Translate(holder, Vector3.Zero, 0.5 - elapsed, null);
        }
        else
        {
          pencil.Opacity = 0.6;
        }
        if (elapsed < 2.4)
        {
          DispatcherQueue forCurrentThread = DispatcherQueue.GetForCurrentThread();
          double num2 = 2.4 - elapsed;
          Action action;
          if ((action = <>9__7) == null)
          {
            action = (<>9__7 = delegate
            {
              Anim.LoopVector3(pencil, "Translation", 2.4, (double t) => new Vector3(0f, (float)ClickyInkPencilLayers.<.ctor>g__Tap|1_5(t), 0f), 96, 0.0);
            });
          }
          Once.After(num2, action, forCurrentThread);
        }
        else
        {
          Anim.LoopVector3(pencil, "Translation", 2.4, (double t) => new Vector3(0f, (float)ClickyInkPencilLayers.<.ctor>g__Tap|1_5(t), 0f), 96, (elapsed - 2.4) % 2.4);
        }
        for (int k = 0; k < dots.Count; k++)
        {
          int i = k;
          Anim.LoopScalar(dots[i], "Opacity", 2.0, delegate(double t)
          {
            double num3 = (t * 0.9 - (double)i * 0.22) % 1.8;
            return (float)((num3 < 0.0) ? 0.0 : (Math.Max(0.0, Math.Sin(Math.Min(1.0, num3 / 1.2) * 3.141592653589793)) * 0.55));
          }, 48, elapsed % 2.0);
        }
      };
    }

    // Token: 0x06001BD7 RID: 7127 RVA: 0x005F55A0 File Offset: 0x005F55A0
    [CompilerGenerated]
    internal static double <.ctor>g__Ease|1_2(double t)
    {
      return (1.0 - Math.Cos(t * 3.141592653589793 / 0.32)) / 2.0;
    }

    // Token: 0x06001BD8 RID: 7128 RVA: 0x005F55D0 File Offset: 0x005F55D0
    [CompilerGenerated]
    internal static double <.ctor>g__Tap|1_5(double cycle)
    {
      if (cycle >= 0.5)
      {
        return 0.0;
      }
      return Math.Abs(Math.Sin(cycle / 0.25 * 3.141592653589793)) * 2.2;
    }

    // Token: 0x0200087A RID: 2170
    [Nullable(0)]
    public abstract class Motion : IEquatable<ClickyInkPencilLayers.Motion>
    {
      // Token: 0x17000802 RID: 2050
      // (get) Token: 0x06003976 RID: 14710 RVA: 0x00689560 File Offset: 0x00689560
      [CompilerGenerated]
      protected virtual Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkPencilLayers.Motion);
        }
      }

      // Token: 0x06003977 RID: 14711 RVA: 0x0068956C File Offset: 0x0068956C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Motion");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003978 RID: 14712 RVA: 0x006895B8 File Offset: 0x006895B8
      [CompilerGenerated]
      protected virtual bool PrintMembers(StringBuilder builder)
      {
        return false;
      }

      // Token: 0x06003979 RID: 14713 RVA: 0x006895BC File Offset: 0x006895BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkPencilLayers.Motion left, ClickyInkPencilLayers.Motion right)
      {
        return !(left == right);
      }

      // Token: 0x0600397A RID: 14714 RVA: 0x006895C8 File Offset: 0x006895C8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkPencilLayers.Motion left, ClickyInkPencilLayers.Motion right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600397B RID: 14715 RVA: 0x006895DC File Offset: 0x006895DC
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
      }

      // Token: 0x0600397C RID: 14716 RVA: 0x006895F0 File Offset: 0x006895F0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkPencilLayers.Motion);
      }

      // Token: 0x0600397D RID: 14717 RVA: 0x00689600 File Offset: 0x00689600
      [NullableContext(2)]
      [CompilerGenerated]
      public virtual bool Equals(ClickyInkPencilLayers.Motion other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract);
      }

      // Token: 0x0600397F RID: 14719 RVA: 0x00689620 File Offset: 0x00689620
      [CompilerGenerated]
      protected Motion(ClickyInkPencilLayers.Motion original)
      {
      }

      // Token: 0x06003980 RID: 14720 RVA: 0x00689628 File Offset: 0x00689628
      protected Motion()
      {
      }

      // Token: 0x02000B0E RID: 2830
      [Nullable(0)]
      public sealed class Tapping : ClickyInkPencilLayers.Motion, IEquatable<ClickyInkPencilLayers.Motion.Tapping>
      {
        // Token: 0x170008B1 RID: 2225
        // (get) Token: 0x06004404 RID: 17412 RVA: 0x006D12C0 File Offset: 0x006D12C0
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkPencilLayers.Motion.Tapping);
          }
        }

        // Token: 0x06004405 RID: 17413 RVA: 0x006D12CC File Offset: 0x006D12CC
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Tapping");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004406 RID: 17414 RVA: 0x006D1318 File Offset: 0x006D1318
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          return base.PrintMembers(builder);
        }

        // Token: 0x06004407 RID: 17415 RVA: 0x006D1324 File Offset: 0x006D1324
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkPencilLayers.Motion.Tapping left, ClickyInkPencilLayers.Motion.Tapping right)
        {
          return !(left == right);
        }

        // Token: 0x06004408 RID: 17416 RVA: 0x006D1330 File Offset: 0x006D1330
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkPencilLayers.Motion.Tapping left, ClickyInkPencilLayers.Motion.Tapping right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004409 RID: 17417 RVA: 0x006D1344 File Offset: 0x006D1344
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode();
        }

        // Token: 0x0600440A RID: 17418 RVA: 0x006D134C File Offset: 0x006D134C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkPencilLayers.Motion.Tapping);
        }

        // Token: 0x0600440B RID: 17419 RVA: 0x006D135C File Offset: 0x006D135C
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkPencilLayers.Motion other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600440C RID: 17420 RVA: 0x006D1368 File Offset: 0x006D1368
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkPencilLayers.Motion.Tapping other)
        {
          return this == other || base.Equals(other);
        }

        // Token: 0x0600440E RID: 17422 RVA: 0x006D1380 File Offset: 0x006D1380
        [CompilerGenerated]
        private Tapping(ClickyInkPencilLayers.Motion.Tapping original)
          : base(original)
        {
        }

        // Token: 0x0600440F RID: 17423 RVA: 0x006D138C File Offset: 0x006D138C
        public Tapping()
        {
        }
      }

      // Token: 0x02000B0F RID: 2831
      [Nullable(0)]
      public sealed class Resting : ClickyInkPencilLayers.Motion, IEquatable<ClickyInkPencilLayers.Motion.Resting>
      {
        // Token: 0x06004410 RID: 17424 RVA: 0x006D1394 File Offset: 0x006D1394
        public Resting(double Elapsed)
        {
          this.Elapsed = Elapsed;
          base..ctor();
        }

        // Token: 0x170008B2 RID: 2226
        // (get) Token: 0x06004411 RID: 17425 RVA: 0x006D13A4 File Offset: 0x006D13A4
        [CompilerGenerated]
        protected override Type EqualityContract
        {
          [CompilerGenerated]
          get
          {
            return typeof(ClickyInkPencilLayers.Motion.Resting);
          }
        }

        // Token: 0x170008B3 RID: 2227
        // (get) Token: 0x06004412 RID: 17426 RVA: 0x006D13B0 File Offset: 0x006D13B0
        // (set) Token: 0x06004413 RID: 17427 RVA: 0x006D13B8 File Offset: 0x006D13B8
        public double Elapsed { get; set; }

        // Token: 0x06004414 RID: 17428 RVA: 0x006D13C4 File Offset: 0x006D13C4
        [CompilerGenerated]
        public override string ToString()
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("Resting");
          stringBuilder.Append(" { ");
          if (this.PrintMembers(stringBuilder))
          {
            stringBuilder.Append(' ');
          }
          stringBuilder.Append('}');
          return stringBuilder.ToString();
        }

        // Token: 0x06004415 RID: 17429 RVA: 0x006D1410 File Offset: 0x006D1410
        [CompilerGenerated]
        protected override bool PrintMembers(StringBuilder builder)
        {
          if (base.PrintMembers(builder))
          {
            builder.Append(", ");
          }
          builder.Append("Elapsed = ");
          builder.Append(this.Elapsed.ToString());
          return true;
        }

        // Token: 0x06004416 RID: 17430 RVA: 0x006D145C File Offset: 0x006D145C
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator !=(ClickyInkPencilLayers.Motion.Resting left, ClickyInkPencilLayers.Motion.Resting right)
        {
          return !(left == right);
        }

        // Token: 0x06004417 RID: 17431 RVA: 0x006D1468 File Offset: 0x006D1468
        [NullableContext(2)]
        [CompilerGenerated]
        public static bool operator ==(ClickyInkPencilLayers.Motion.Resting left, ClickyInkPencilLayers.Motion.Resting right)
        {
          return left == right || (left != null && left.Equals(right));
        }

        // Token: 0x06004418 RID: 17432 RVA: 0x006D147C File Offset: 0x006D147C
        [CompilerGenerated]
        public override int GetHashCode()
        {
          return base.GetHashCode() * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Elapsed>k__BackingField);
        }

        // Token: 0x06004419 RID: 17433 RVA: 0x006D149C File Offset: 0x006D149C
        [NullableContext(2)]
        [CompilerGenerated]
        public override bool Equals(object obj)
        {
          return this.Equals(obj as ClickyInkPencilLayers.Motion.Resting);
        }

        // Token: 0x0600441A RID: 17434 RVA: 0x006D14AC File Offset: 0x006D14AC
        [NullableContext(2)]
        [CompilerGenerated]
        public sealed override bool Equals(ClickyInkPencilLayers.Motion other)
        {
          return this.Equals(other);
        }

        // Token: 0x0600441B RID: 17435 RVA: 0x006D14B8 File Offset: 0x006D14B8
        [NullableContext(2)]
        [CompilerGenerated]
        public bool Equals(ClickyInkPencilLayers.Motion.Resting other)
        {
          return this == other || (base.Equals(other) && EqualityComparer<double>.Default.Equals(this.<Elapsed>k__BackingField, other.<Elapsed>k__BackingField));
        }

        // Token: 0x0600441D RID: 17437 RVA: 0x006D14EC File Offset: 0x006D14EC
        [CompilerGenerated]
        private Resting(ClickyInkPencilLayers.Motion.Resting original)
          : base(original)
        {
          this.Elapsed = original.<Elapsed>k__BackingField;
        }

        // Token: 0x0600441E RID: 17438 RVA: 0x006D1504 File Offset: 0x006D1504
        [CompilerGenerated]
        public void Deconstruct(out double Elapsed)
        {
          Elapsed = this.Elapsed;
        }
      }
    }
  }
}
