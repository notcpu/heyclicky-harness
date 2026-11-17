using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;

namespace HeyClicky.App.Ink
{
  // Token: 0x0200024A RID: 586
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class InkPaperSheet : Grid
  {
    // Token: 0x06001A11 RID: 6673 RVA: 0x005DC700 File Offset: 0x005DC700
    public InkPaperSheet(string key, IClickyShape shape, PaperFill fill, double? grain = null, [Nullable(new byte[] { 2, 1 })] PaperShadow[] shadows = null, [Nullable(2)] PaperStroke outline = null, [Nullable(2)] PaperFill wash = null)
    {
      this.key = key;
      this.shape = shape;
      this.fill = fill;
      this.grain = grain;
      this.shadows = shadows ?? Array.Empty<PaperShadow>();
      this.outline = outline;
      this.wash = wash;
      base.IsHitTestVisible = false;
      this.canvas.Children.Add(this.image);
      base.Children.Add(this.canvas);
      base.SizeChanged += delegate(object _, SizeChangedEventArgs _)
      {
        this.Draw();
      };
      base.Loaded += delegate(object _, RoutedEventArgs _)
      {
        this.Draw();
      };
    }

    // Token: 0x17000488 RID: 1160
    // (get) Token: 0x06001A12 RID: 6674 RVA: 0x005DC7DC File Offset: 0x005DC7DC
    private new double Scale
    {
      get
      {
        XamlRoot xamlRoot = base.XamlRoot;
        double? num = ((xamlRoot != null) ? new double?(xamlRoot.RasterizationScale) : null);
        if (num != null)
        {
          double valueOrDefault = num.GetValueOrDefault();
          if (valueOrDefault > 0.0)
          {
            return valueOrDefault;
          }
        }
        return 2.0;
      }
    }

    // Token: 0x06001A13 RID: 6675 RVA: 0x005DC834 File Offset: 0x005DC834
    private void Draw()
    {
      double actualWidth = base.ActualWidth;
      double actualHeight = base.ActualHeight;
      if (actualWidth <= 0.0 || actualHeight <= 0.0)
      {
        return;
      }
      double scale = this.Scale;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
      defaultInterpolatedStringHandler.AppendFormatted(this.key);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<double>(actualWidth, "0.##");
      defaultInterpolatedStringHandler.AppendLiteral("x");
      defaultInterpolatedStringHandler.AppendFormatted<double>(actualHeight, "0.##");
      defaultInterpolatedStringHandler.AppendLiteral("@");
      defaultInterpolatedStringHandler.AppendFormatted<double>(scale, "0.##");
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      if (text == this.drawnKey)
      {
        return;
      }
      this.drawnKey = text;
      InkPaperSheet.Drawn drawn;
      if (!InkPaperSheet.cache.TryGetValue(text, out drawn))
      {
        try
        {
          drawn = this.Render(actualWidth, actualHeight, scale);
        }
        catch (Exception ex)
        {
          Log.Warn("ink", "paper sheet " + this.key + ": " + ex.Message);
          return;
        }
        InkPaperSheet.cache[text] = drawn;
        InkPaperSheet.recency.AddFirst(text);
        while (InkPaperSheet.recency.Count > 96)
        {
          InkPaperSheet.cache.Remove(InkPaperSheet.recency.Last.Value);
          InkPaperSheet.recency.RemoveLast();
        }
      }
      this.image.Source = drawn.Source;
      this.image.Width = actualWidth + 2.0 * drawn.Reach;
      this.image.Height = actualHeight + 2.0 * drawn.Reach;
      Canvas.SetLeft(this.image, -drawn.Reach);
      Canvas.SetTop(this.image, -drawn.Reach);
    }

    // Token: 0x06001A14 RID: 6676 RVA: 0x005DCA0C File Offset: 0x005DCA0C
    private InkPaperSheet.Drawn Render(double w, double h, double scale)
    {
      PaperStroke paperStroke = this.outline;
      double num = ((paperStroke != null) ? paperStroke.Width : 0.0);
      foreach (PaperShadow paperShadow in this.shadows)
      {
        num = Math.Max(num, paperShadow.Radius * 0.5 * 3.0 + Math.Max(Math.Abs(paperShadow.X), Math.Abs(paperShadow.Y)) + 1.0);
      }
      num = Math.Ceiling(num);
      CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
      CanvasImageSource canvasImageSource = new CanvasImageSource(sharedDevice, (float)(w + 2.0 * num), (float)(h + 2.0 * num), (float)(96.0 * scale));
      using (CanvasDrawingSession canvasDrawingSession = canvasImageSource.CreateDrawingSession(Colors.Transparent))
      {
        canvasDrawingSession.Transform = Matrix3x2.CreateTranslation((float)num, (float)num);
        Rect rect = new Rect(0.0, 0.0, w, h);
        using (CanvasGeometry canvasGeometry = this.shape.PathIn(rect).ToCanvasGeometry(sharedDevice))
        {
          foreach (PaperShadow paperShadow2 in this.shadows)
          {
            using (CanvasCommandList canvasCommandList = new CanvasCommandList(sharedDevice))
            {
              using (CanvasDrawingSession canvasDrawingSession2 = canvasCommandList.CreateDrawingSession())
              {
                canvasDrawingSession2.FillGeometry(canvasGeometry, Colors.Black);
              }
              using (ShadowEffect shadowEffect = new ShadowEffect
              {
                Source = canvasCommandList,
                BlurAmount = (float)(paperShadow2.Radius * 0.5),
                ShadowColor = paperShadow2.Color,
                Optimization = EffectOptimization.Quality
              })
              {
                canvasDrawingSession.DrawImage(shadowEffect, (float)paperShadow2.X, (float)paperShadow2.Y);
              }
            }
          }
          using (canvasDrawingSession.CreateLayer(1f, canvasGeometry))
          {
            using (CanvasCommandList canvasCommandList2 = new CanvasCommandList(sharedDevice))
            {
              using (CanvasDrawingSession canvasDrawingSession3 = canvasCommandList2.CreateDrawingSession())
              {
                canvasDrawingSession3.FillRectangle(rect, this.fill.Brush(sharedDevice, rect));
              }
              double? num2 = this.grain;
              if (num2 != null)
              {
                double valueOrDefault = num2.GetValueOrDefault();
                if (valueOrDefault > 0.0)
                {
                  using (ICanvasImage canvasImage = ClickyPaperTextures.GrainImage(sharedDevice, rect, valueOrDefault))
                  {
                    using (BlendEffect blendEffect = new BlendEffect
                    {
                      Background = canvasCommandList2,
                      Foreground = canvasImage,
                      Mode = BlendEffectMode.Multiply
                    })
                    {
                      canvasDrawingSession.DrawImage(blendEffect);
                      goto IL_0290;
                    }
                  }
                }
              }
              canvasDrawingSession.DrawImage(canvasCommandList2);
              IL_0290:
              if (this.wash != null)
              {
                canvasDrawingSession.FillRectangle(rect, this.wash.Brush(sharedDevice, rect));
              }
            }
          }
          PaperStroke paperStroke2 = this.outline;
          if (paperStroke2 != null)
          {
            using (CanvasStrokeStyle canvasStrokeStyle = new CanvasStrokeStyle
            {
              StartCap = ((paperStroke2.Cap == PenLineCap.Round) ? CanvasCapStyle.Round : CanvasCapStyle.Flat),
              EndCap = ((paperStroke2.Cap == PenLineCap.Round) ? CanvasCapStyle.Round : CanvasCapStyle.Flat),
              LineJoin = ((paperStroke2.Join == PenLineJoin.Round) ? CanvasLineJoin.Round : CanvasLineJoin.Miter)
            })
            {
              canvasDrawingSession.DrawGeometry(canvasGeometry, paperStroke2.Color, (float)paperStroke2.Width, canvasStrokeStyle);
            }
          }
        }
      }
      return new InkPaperSheet.Drawn(canvasImageSource, num);
    }

    // Token: 0x04000C8D RID: 3213
    private static readonly Dictionary<string, InkPaperSheet.Drawn> cache = new Dictionary<string, InkPaperSheet.Drawn>();

    // Token: 0x04000C8E RID: 3214
    private static readonly LinkedList<string> recency = new LinkedList<string>();

    // Token: 0x04000C8F RID: 3215
    private const int MaxEntries = 96;

    // Token: 0x04000C90 RID: 3216
    private readonly string key;

    // Token: 0x04000C91 RID: 3217
    private readonly IClickyShape shape;

    // Token: 0x04000C92 RID: 3218
    private readonly PaperFill fill;

    // Token: 0x04000C93 RID: 3219
    private readonly double? grain;

    // Token: 0x04000C94 RID: 3220
    private readonly PaperShadow[] shadows;

    // Token: 0x04000C95 RID: 3221
    [Nullable(2)]
    private readonly PaperStroke outline;

    // Token: 0x04000C96 RID: 3222
    [Nullable(2)]
    private readonly PaperFill wash;

    // Token: 0x04000C97 RID: 3223
    private readonly Canvas canvas = new Canvas
    {
      IsHitTestVisible = false
    };

    // Token: 0x04000C98 RID: 3224
    private readonly Image image = new Image
    {
      Stretch = Stretch.Fill,
      IsHitTestVisible = false
    };

    // Token: 0x04000C99 RID: 3225
    private string drawnKey = "";

    // Token: 0x02000805 RID: 2053
    [Nullable(0)]
    private sealed class Drawn : IEquatable<InkPaperSheet.Drawn>
    {
      // Token: 0x060037D4 RID: 14292 RVA: 0x006859CC File Offset: 0x006859CC
      public Drawn(ImageSource Source, double Reach)
      {
        this.Source = Source;
        this.Reach = Reach;
        base..ctor();
      }

      // Token: 0x170007ED RID: 2029
      // (get) Token: 0x060037D5 RID: 14293 RVA: 0x006859E4 File Offset: 0x006859E4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(InkPaperSheet.Drawn);
        }
      }

      // Token: 0x170007EE RID: 2030
      // (get) Token: 0x060037D6 RID: 14294 RVA: 0x006859F0 File Offset: 0x006859F0
      // (set) Token: 0x060037D7 RID: 14295 RVA: 0x006859F8 File Offset: 0x006859F8
      public ImageSource Source { get; set; }

      // Token: 0x170007EF RID: 2031
      // (get) Token: 0x060037D8 RID: 14296 RVA: 0x00685A04 File Offset: 0x00685A04
      // (set) Token: 0x060037D9 RID: 14297 RVA: 0x00685A0C File Offset: 0x00685A0C
      public double Reach { get; set; }

      // Token: 0x060037DA RID: 14298 RVA: 0x00685A18 File Offset: 0x00685A18
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Drawn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060037DB RID: 14299 RVA: 0x00685A64 File Offset: 0x00685A64
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Source = ");
        builder.Append(this.Source);
        builder.Append(", Reach = ");
        builder.Append(this.Reach.ToString());
        return true;
      }

      // Token: 0x060037DC RID: 14300 RVA: 0x00685AB8 File Offset: 0x00685AB8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(InkPaperSheet.Drawn left, InkPaperSheet.Drawn right)
      {
        return !(left == right);
      }

      // Token: 0x060037DD RID: 14301 RVA: 0x00685AC4 File Offset: 0x00685AC4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(InkPaperSheet.Drawn left, InkPaperSheet.Drawn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060037DE RID: 14302 RVA: 0x00685AD8 File Offset: 0x00685AD8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ImageSource>.Default.GetHashCode(this.<Source>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Reach>k__BackingField);
      }

      // Token: 0x060037DF RID: 14303 RVA: 0x00685B18 File Offset: 0x00685B18
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as InkPaperSheet.Drawn);
      }

      // Token: 0x060037E0 RID: 14304 RVA: 0x00685B28 File Offset: 0x00685B28
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(InkPaperSheet.Drawn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ImageSource>.Default.Equals(this.<Source>k__BackingField, other.<Source>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Reach>k__BackingField, other.<Reach>k__BackingField));
      }

      // Token: 0x060037E2 RID: 14306 RVA: 0x00685B8C File Offset: 0x00685B8C
      [CompilerGenerated]
      private Drawn(InkPaperSheet.Drawn original)
      {
        this.Source = original.<Source>k__BackingField;
        this.Reach = original.<Reach>k__BackingField;
      }

      // Token: 0x060037E3 RID: 14307 RVA: 0x00685BAC File Offset: 0x00685BAC
      [CompilerGenerated]
      public void Deconstruct(out ImageSource Source, out double Reach)
      {
        Source = this.Source;
        Reach = this.Reach;
      }
    }
  }
}
