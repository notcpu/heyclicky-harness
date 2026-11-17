using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using HeyClicky.App.Design;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;

namespace HeyClicky.App.Ink
{
  // Token: 0x020001E4 RID: 484
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyPaperTextures
  {
    // Token: 0x06001824 RID: 6180 RVA: 0x005C8C24 File Offset: 0x005C8C24
    private static byte[] MakeGrainTile(int size, ulong seed)
    {
      ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom(seed);
      double[] array = new double[size * size];
      for (int i = 0; i < array.Length; i++)
      {
        array[i] = clickyPaperRandom.NextUnit();
      }
      double[] array2 = new double[size * size];
      for (int j = 0; j < size; j++)
      {
        for (int k = 0; k < size; k++)
        {
          double num = 0.0;
          for (int l = -1; l <= 1; l++)
          {
            for (int m = -1; m <= 1; m++)
            {
              int num2 = (k + m + size) % size;
              int num3 = (j + l + size) % size;
              num += array[num3 * size + num2] * (double)((m == 0 && l == 0) ? 4 : 1);
            }
          }
          array2[j * size + k] = num / 12.0;
        }
      }
      byte[] array3 = new byte[size * size * 4];
      for (int n = 0; n < size * size; n++)
      {
        double num4 = Math.Pow(array2[n], 2.2);
        double num5 = Math.Min(1.0, num4 * 0.3);
        array3[n * 4] = (byte)(107.0 * num5);
        array3[n * 4 + 1] = (byte)(102.0 * num5);
        array3[n * 4 + 2] = (byte)(92.0 * num5);
        array3[n * 4 + 3] = (byte)(255.0 * num5);
      }
      return array3;
    }

    // Token: 0x17000447 RID: 1095
    // (get) Token: 0x06001825 RID: 6181 RVA: 0x005C8DA8 File Offset: 0x005C8DA8
    private static byte[] GrainTileBgra
    {
      get
      {
        byte[] array = new byte[ClickyPaperTextures.GrainTileRgba.Length];
        for (int i = 0; i < array.Length; i += 4)
        {
          array[i] = ClickyPaperTextures.GrainTileRgba[i + 2];
          array[i + 1] = ClickyPaperTextures.GrainTileRgba[i + 1];
          array[i + 2] = ClickyPaperTextures.GrainTileRgba[i];
          array[i + 3] = ClickyPaperTextures.GrainTileRgba[i + 3];
        }
        return array;
      }
    }

    // Token: 0x06001826 RID: 6182 RVA: 0x005C8E08 File Offset: 0x005C8E08
    public static CanvasBitmap GrainBitmap(CanvasDevice device)
    {
      Dictionary<CanvasDevice, CanvasBitmap> dictionary = ClickyPaperTextures.grainBitmaps;
      CanvasBitmap canvasBitmap2;
      lock (dictionary)
      {
        CanvasBitmap canvasBitmap;
        if (ClickyPaperTextures.grainBitmaps.TryGetValue(device, out canvasBitmap))
        {
          canvasBitmap2 = canvasBitmap;
        }
        else
        {
          canvasBitmap = CanvasBitmap.CreateFromBytes(device, ClickyPaperTextures.GrainTileBgra, 160, 160, DirectXPixelFormat.B8G8R8A8UIntNormalized, 192f, CanvasAlphaMode.Premultiplied);
          ClickyPaperTextures.grainBitmaps[device] = canvasBitmap;
          device.DeviceLost += delegate(CanvasDevice _, object _)
          {
            Dictionary<CanvasDevice, CanvasBitmap> dictionary2 = ClickyPaperTextures.grainBitmaps;
            lock (dictionary2)
            {
              ClickyPaperTextures.grainBitmaps.Remove(device);
            }
          };
          canvasBitmap2 = canvasBitmap;
        }
      }
      return canvasBitmap2;
    }

    // Token: 0x06001827 RID: 6183 RVA: 0x005C8EB8 File Offset: 0x005C8EB8
    public static void DrawGrain(CanvasDrawingSession ds, Rect bounds, double opacity)
    {
      CanvasBitmap canvasBitmap = ClickyPaperTextures.GrainBitmap(ds.Device);
      double num = 80.0;
      for (double num2 = bounds.Y - (bounds.Y % num + num) % num; num2 < bounds.Bottom; num2 += num)
      {
        for (double num3 = bounds.X - (bounds.X % num + num) % num; num3 < bounds.Right; num3 += num)
        {
          ds.DrawImage(canvasBitmap, new Rect(num3, num2, num, num), canvasBitmap.Bounds, (float)opacity, CanvasImageInterpolation.NearestNeighbor);
        }
      }
    }

    // Token: 0x06001828 RID: 6184 RVA: 0x005C8F40 File Offset: 0x005C8F40
    public static ICanvasImage GrainImage(ICanvasResourceCreator device, Rect bounds, double opacity)
    {
      CanvasCommandList canvasCommandList = new CanvasCommandList(device);
      using (CanvasDrawingSession canvasDrawingSession = canvasCommandList.CreateDrawingSession())
      {
        ClickyPaperTextures.DrawGrain(canvasDrawingSession, bounds, opacity);
      }
      return canvasCommandList;
    }

    // Token: 0x06001829 RID: 6185 RVA: 0x005C8F80 File Offset: 0x005C8F80
    private static PaperFont StampFont(double fontSize)
    {
      return new PaperFont(DS.Fonts.For(fontSize), (fontSize < 12.0) ? "Segoe UI Variable Small" : ((fontSize >= 20.0) ? "Segoe UI Variable Display" : "Segoe UI Variable Text"), fontSize, FontWeights.ExtraBold);
    }

    // Token: 0x0600182A RID: 6186 RVA: 0x005C8FC0 File Offset: 0x005C8FC0
    public static ClickyPaperTextures.StampImage Stamp(string word, Color color, double fontSize)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted(word);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<Color>(color);
      defaultInterpolatedStringHandler.AppendLiteral("|");
      defaultInterpolatedStringHandler.AppendFormatted<double>(fontSize);
      string text = defaultInterpolatedStringHandler.ToStringAndClear();
      Dictionary<string, ClickyPaperTextures.StampImage> dictionary = ClickyPaperTextures.stampCache;
      lock (dictionary)
      {
        ClickyPaperTextures.StampImage stampImage;
        if (ClickyPaperTextures.stampCache.TryGetValue(text, out stampImage))
        {
          return stampImage;
        }
      }
      CanvasDevice sharedDevice = CanvasDevice.GetSharedDevice();
      double num = fontSize * 0.09;
      ClickyPaperTextures.StampImage stampImage3;
      using (CanvasTextFormat canvasTextFormat = ClickyPaperTextures.StampFont(fontSize).TextFormat())
      {
        using (CanvasTextLayout canvasTextLayout = new CanvasTextLayout(sharedDevice, word, canvasTextFormat, 10000f, 1000f))
        {
          if (word.Length > 0)
          {
            canvasTextLayout.SetCharacterSpacing(0, word.Length, 0f, (float)num, 0f);
          }
          double num2 = canvasTextLayout.LayoutBoundsIncludingTrailingWhitespace.Width - num;
          Size size = new Size(Math.Ceiling(num2 + 16.0), Math.Ceiling(fontSize + 10.0));
          using (CanvasRenderTarget canvasRenderTarget = new CanvasRenderTarget(sharedDevice, (float)size.Width, (float)size.Height, 288f))
          {
            using (CanvasDrawingSession canvasDrawingSession = canvasRenderTarget.CreateDrawingSession())
            {
              canvasDrawingSession.Clear(Colors.Transparent);
              canvasDrawingSession.DrawRoundedRectangle(new Rect(1.0, 1.0, size.Width - 2.0, size.Height - 2.0), 3.5f, 3.5f, color, 1.7f);
              double num3 = (size.Height - fontSize * 0.72) / 2.0;
              double num4 = size.Height - num3;
              double num5 = ((canvasTextLayout.LineMetrics.Length != 0) ? ((double)canvasTextLayout.LineMetrics[0].Baseline) : (fontSize * 0.8));
              canvasDrawingSession.DrawTextLayout(canvasTextLayout, new Vector2((float)((size.Width - num2) / 2.0), (float)(num4 - num5)), color);
              ClickyPaperRandom clickyPaperRandom = new ClickyPaperRandom(ClickyPaperRandom.StableSeed(word) + 11UL);
              canvasDrawingSession.Blend = CanvasBlend.Copy;
              int num6 = (int)(size.Width * size.Height / 26.0);
              for (int i = 0; i < num6; i++)
              {
                double num7 = clickyPaperRandom.NextUnit() * size.Width;
                double num8 = clickyPaperRandom.NextUnit() * size.Height;
                double num9 = 0.18 + clickyPaperRandom.NextUnit() * 0.42;
                double num10 = 0.6 + clickyPaperRandom.NextUnit() * 0.8;
                double num11 = size.Height - (num8 - num9 + 2.0 * num9 * num10);
                canvasDrawingSession.FillEllipse(new Vector2((float)num7, (float)(num11 + num9 * num10)), (float)num9, (float)(num9 * num10), Colors.Transparent);
              }
            }
            ClickyPaperTextures.StampImage stampImage2 = new ClickyPaperTextures.StampImage(canvasRenderTarget.GetPixelBytes(), (int)canvasRenderTarget.SizeInPixels.Width, (int)canvasRenderTarget.SizeInPixels.Height, size);
            dictionary = ClickyPaperTextures.stampCache;
            lock (dictionary)
            {
              ClickyPaperTextures.stampCache[text] = stampImage2;
            }
            stampImage3 = stampImage2;
          }
        }
      }
      return stampImage3;
    }

    // Token: 0x04000BDB RID: 3035
    public const int GrainTileSize = 160;

    // Token: 0x04000BDC RID: 3036
    public const double GrainTileScale = 2.0;

    // Token: 0x04000BDD RID: 3037
    public static readonly byte[] GrainTileRgba = ClickyPaperTextures.MakeGrainTile(160, 7UL);

    // Token: 0x04000BDE RID: 3038
    private static readonly Dictionary<CanvasDevice, CanvasBitmap> grainBitmaps = new Dictionary<CanvasDevice, CanvasBitmap>();

    // Token: 0x04000BDF RID: 3039
    private static readonly Dictionary<string, ClickyPaperTextures.StampImage> stampCache = new Dictionary<string, ClickyPaperTextures.StampImage>();

    // Token: 0x0200078F RID: 1935
    [Nullable(0)]
    public sealed class StampImage : IEquatable<ClickyPaperTextures.StampImage>
    {
      // Token: 0x06003604 RID: 13828 RVA: 0x0067D5B4 File Offset: 0x0067D5B4
      public StampImage(byte[] Bgra, int PixelWidth, int PixelHeight, Size Size)
      {
        this.Bgra = Bgra;
        this.PixelWidth = PixelWidth;
        this.PixelHeight = PixelHeight;
        this.Size = Size;
        base..ctor();
      }

      // Token: 0x170007CD RID: 1997
      // (get) Token: 0x06003605 RID: 13829 RVA: 0x0067D5DC File Offset: 0x0067D5DC
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyPaperTextures.StampImage);
        }
      }

      // Token: 0x170007CE RID: 1998
      // (get) Token: 0x06003606 RID: 13830 RVA: 0x0067D5E8 File Offset: 0x0067D5E8
      // (set) Token: 0x06003607 RID: 13831 RVA: 0x0067D5F0 File Offset: 0x0067D5F0
      public byte[] Bgra { get; set; }

      // Token: 0x170007CF RID: 1999
      // (get) Token: 0x06003608 RID: 13832 RVA: 0x0067D5FC File Offset: 0x0067D5FC
      // (set) Token: 0x06003609 RID: 13833 RVA: 0x0067D604 File Offset: 0x0067D604
      public int PixelWidth { get; set; }

      // Token: 0x170007D0 RID: 2000
      // (get) Token: 0x0600360A RID: 13834 RVA: 0x0067D610 File Offset: 0x0067D610
      // (set) Token: 0x0600360B RID: 13835 RVA: 0x0067D618 File Offset: 0x0067D618
      public int PixelHeight { get; set; }

      // Token: 0x170007D1 RID: 2001
      // (get) Token: 0x0600360C RID: 13836 RVA: 0x0067D624 File Offset: 0x0067D624
      // (set) Token: 0x0600360D RID: 13837 RVA: 0x0067D62C File Offset: 0x0067D62C
      public Size Size { get; set; }

      // Token: 0x170007D2 RID: 2002
      // (get) Token: 0x0600360E RID: 13838 RVA: 0x0067D638 File Offset: 0x0067D638
      public WriteableBitmap Bitmap
      {
        get
        {
          if (this.bitmap != null)
          {
            return this.bitmap;
          }
          this.bitmap = new WriteableBitmap(this.PixelWidth, this.PixelHeight);
          using (Stream stream = this.bitmap.PixelBuffer.AsStream())
          {
            stream.Write(this.Bgra, 0, this.Bgra.Length);
          }
          this.bitmap.Invalidate();
          return this.bitmap;
        }
      }

      // Token: 0x0600360F RID: 13839 RVA: 0x0067D6C0 File Offset: 0x0067D6C0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("StampImage");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003610 RID: 13840 RVA: 0x0067D70C File Offset: 0x0067D70C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Bgra = ");
        builder.Append(this.Bgra);
        builder.Append(", PixelWidth = ");
        builder.Append(this.PixelWidth.ToString());
        builder.Append(", PixelHeight = ");
        builder.Append(this.PixelHeight.ToString());
        builder.Append(", Size = ");
        builder.Append(this.Size.ToString());
        builder.Append(", Bitmap = ");
        builder.Append(this.Bitmap);
        return true;
      }

      // Token: 0x06003611 RID: 13841 RVA: 0x0067D7C8 File Offset: 0x0067D7C8
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyPaperTextures.StampImage left, ClickyPaperTextures.StampImage right)
      {
        return !(left == right);
      }

      // Token: 0x06003612 RID: 13842 RVA: 0x0067D7D4 File Offset: 0x0067D7D4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyPaperTextures.StampImage left, ClickyPaperTextures.StampImage right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003613 RID: 13843 RVA: 0x0067D7E8 File Offset: 0x0067D7E8
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(this.<Bgra>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelWidth>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelHeight>k__BackingField)) * -1521134295 + EqualityComparer<Size>.Default.GetHashCode(this.<Size>k__BackingField)) * -1521134295 + EqualityComparer<WriteableBitmap>.Default.GetHashCode(this.bitmap);
      }

      // Token: 0x06003614 RID: 13844 RVA: 0x0067D878 File Offset: 0x0067D878
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyPaperTextures.StampImage);
      }

      // Token: 0x06003615 RID: 13845 RVA: 0x0067D888 File Offset: 0x0067D888
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyPaperTextures.StampImage other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<byte[]>.Default.Equals(this.<Bgra>k__BackingField, other.<Bgra>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelWidth>k__BackingField, other.<PixelWidth>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelHeight>k__BackingField, other.<PixelHeight>k__BackingField) && EqualityComparer<Size>.Default.Equals(this.<Size>k__BackingField, other.<Size>k__BackingField) && EqualityComparer<WriteableBitmap>.Default.Equals(this.bitmap, other.bitmap));
      }

      // Token: 0x06003617 RID: 13847 RVA: 0x0067D938 File Offset: 0x0067D938
      [CompilerGenerated]
      private StampImage(ClickyPaperTextures.StampImage original)
      {
        this.Bgra = original.<Bgra>k__BackingField;
        this.PixelWidth = original.<PixelWidth>k__BackingField;
        this.PixelHeight = original.<PixelHeight>k__BackingField;
        this.Size = original.<Size>k__BackingField;
        this.bitmap = original.bitmap;
      }

      // Token: 0x06003618 RID: 13848 RVA: 0x0067D988 File Offset: 0x0067D988
      [CompilerGenerated]
      public void Deconstruct(out byte[] Bgra, out int PixelWidth, out int PixelHeight, out Size Size)
      {
        Bgra = this.Bgra;
        PixelWidth = this.PixelWidth;
        PixelHeight = this.PixelHeight;
        Size = this.Size;
      }

      // Token: 0x040020F0 RID: 8432
      [Nullable(2)]
      private WriteableBitmap bitmap;
    }
  }
}
