using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace HeyClicky.App.Home
{
  // Token: 0x020002AA RID: 682
  [NullableContext(1)]
  [Nullable(0)]
  public static class ArtifactThumbnails
  {
    // Token: 0x06001BF3 RID: 7155 RVA: 0x005F7268 File Offset: 0x005F7268
    [return: Nullable(2)]
    public static ImageSource Cached(string path)
    {
      ArtifactThumbnails.Entry entry;
      if (!ArtifactThumbnails.Cache.TryGetValue(path, out entry))
      {
        return null;
      }
      return entry.Image;
    }

    // Token: 0x06001BF4 RID: 7156 RVA: 0x005F728C File Offset: 0x005F728C
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<ImageSource> GetAsync(string path, int pixelWidth)
    {
      Guid refreshId = Guid.NewGuid();
      ArtifactThumbnails.LatestRefresh[path] = refreshId;
      ImageSource imageSource;
      try
      {
        string text = await Task.Run<string>(() => ArtifactThumbnails.Revision(path));
        string revision = text;
        ArtifactThumbnails.Entry entry;
        if (revision == null)
        {
          imageSource = ArtifactThumbnails.Cached(path);
        }
        else if (ArtifactThumbnails.Cache.TryGetValue(path, out entry) && entry.Revision == revision && entry.PixelWidth >= pixelWidth)
        {
          ArtifactThumbnails.Touch(path);
          imageSource = entry.Image;
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
          defaultInterpolatedStringHandler.AppendFormatted(path);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted(revision);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted<int>(pixelWidth);
          string key = defaultInterpolatedStringHandler.ToStringAndClear();
          Task<ImageSource> task;
          bool owns = !ArtifactThumbnails.InFlight.TryGetValue(key, out task);
          if (owns)
          {
            task = (ArtifactThumbnails.InFlight[key] = ArtifactThumbnails.GenerateAsync(path, pixelWidth));
          }
          ImageSource imageSource2;
          try
          {
            imageSource2 = await task;
          }
          finally
          {
            if (owns)
            {
              ArtifactThumbnails.InFlight.Remove(key);
            }
          }
          if (imageSource2 != null && ArtifactThumbnails.LatestRefresh.GetValueOrDefault(path) == refreshId)
          {
            ArtifactThumbnails.Store(path, new ArtifactThumbnails.Entry(revision, pixelWidth, imageSource2));
          }
          imageSource = imageSource2 ?? ArtifactThumbnails.Cached(path);
        }
      }
      finally
      {
        if (ArtifactThumbnails.LatestRefresh.GetValueOrDefault(path) == refreshId)
        {
          ArtifactThumbnails.LatestRefresh.Remove(path);
        }
      }
      return imageSource;
    }

    // Token: 0x06001BF5 RID: 7157 RVA: 0x005F72D8 File Offset: 0x005F72D8
    [return: Nullable(2)]
    private static string Revision(string path)
    {
      string text2;
      try
      {
        FileInfo fileInfo = new FileInfo(path);
        string text;
        if (!fileInfo.Exists)
        {
          text = null;
        }
        else
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
          defaultInterpolatedStringHandler.AppendFormatted<long>(fileInfo.LastWriteTimeUtc.Ticks);
          defaultInterpolatedStringHandler.AppendLiteral("|");
          defaultInterpolatedStringHandler.AppendFormatted<long>(fileInfo.Length);
          text = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        text2 = text;
      }
      catch (Exception)
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x06001BF6 RID: 7158 RVA: 0x005F7350 File Offset: 0x005F7350
    private static void Store(string path, ArtifactThumbnails.Entry entry)
    {
      ArtifactThumbnails.Cache[path] = entry;
      ArtifactThumbnails.Touch(path);
      while (ArtifactThumbnails.Cache.Count > 128)
      {
        LinkedListNode<string> last = ArtifactThumbnails.Recency.Last;
        if (last == null)
        {
          break;
        }
        ArtifactThumbnails.Recency.RemoveLast();
        ArtifactThumbnails.Cache.Remove(last.Value);
      }
    }

    // Token: 0x06001BF7 RID: 7159 RVA: 0x005F73AC File Offset: 0x005F73AC
    private static void Touch(string path)
    {
      ArtifactThumbnails.Recency.Remove(path);
      ArtifactThumbnails.Recency.AddFirst(path);
    }

    // Token: 0x06001BF8 RID: 7160 RVA: 0x005F73C8 File Offset: 0x005F73C8
    [return: Nullable(new byte[] { 1, 2 })]
    private static async Task<ImageSource> GenerateAsync(string path, int pixelWidth)
    {
      bool flag = WebPageSnapshotter.IsWebPageFile(path);
      ImageSource imageSource;
      if (flag)
      {
        imageSource = await WebPageSnapshotter.SnapshotAsync(path);
        flag = imageSource != null;
      }
      ImageSource imageSource2;
      if (flag)
      {
        imageSource2 = imageSource;
      }
      else
      {
        await ArtifactThumbnails.DecodeSlots.WaitAsync();
        try
        {
          StorageFile storageFile = await StorageFile.GetFileFromPathAsync(path);
          BitmapImage bitmap = new BitmapImage();
          if (ArtifactThumbnails.RasterExtensions.Contains(Path.GetExtension(path)))
          {
            bitmap.DecodePixelWidth = pixelWidth;
            using (IRandomAccessStreamWithContentType stream = await storageFile.OpenReadAsync())
            {
              await bitmap.SetSourceAsync(stream);
              return bitmap;
            }
          }
          if (path.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
          {
            PdfDocument pdfDocument = await PdfDocument.LoadFromFileAsync(storageFile);
            if (pdfDocument.PageCount == 0U)
            {
              return null;
            }
            using (PdfPage page = pdfDocument.GetPage(0U))
            {
              using (InMemoryRandomAccessStream rendered = new InMemoryRandomAccessStream())
              {
                await page.RenderToStreamAsync(rendered, new PdfPageRenderOptions
                {
                  DestinationWidth = (uint)pixelWidth
                });
                rendered.Seek(0UL);
                await bitmap.SetSourceAsync(rendered);
                return bitmap;
              }
            }
          }
          using (StorageItemThumbnail thumbnail = await storageFile.GetThumbnailAsync(ThumbnailMode.DocumentsView, (uint)pixelWidth, ThumbnailOptions.ResizeThumbnail))
          {
            if (thumbnail == null || thumbnail.Type != ThumbnailType.Image)
            {
              imageSource2 = null;
            }
            else
            {
              await bitmap.SetSourceAsync(thumbnail);
              imageSource2 = bitmap;
            }
          }
        }
        catch (Exception ex)
        {
          Log.Info("home", "no thumbnail for " + Path.GetFileName(path) + ": " + ex.Message);
          imageSource2 = null;
        }
        finally
        {
          ArtifactThumbnails.DecodeSlots.Release();
        }
      }
      return imageSource2;
    }

    // Token: 0x04000D6C RID: 3436
    private const int MaxEntries = 128;

    // Token: 0x04000D6D RID: 3437
    private static readonly Dictionary<string, ArtifactThumbnails.Entry> Cache = new Dictionary<string, ArtifactThumbnails.Entry>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x04000D6E RID: 3438
    private static readonly LinkedList<string> Recency = new LinkedList<string>();

    // Token: 0x04000D6F RID: 3439
    [Nullable(new byte[] { 1, 1, 1, 2 })]
    private static readonly Dictionary<string, Task<ImageSource>> InFlight = new Dictionary<string, Task<ImageSource>>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x04000D70 RID: 3440
    private static readonly Dictionary<string, Guid> LatestRefresh = new Dictionary<string, Guid>(StringComparer.OrdinalIgnoreCase);

    // Token: 0x04000D71 RID: 3441
    private static readonly SemaphoreSlim DecodeSlots = new SemaphoreSlim(2, 2);

    // Token: 0x04000D72 RID: 3442
    private static readonly HashSet<string> RasterExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".png", ".jpg", ".jpeg", ".gif", ".tiff", ".tif", ".heic", ".webp", ".bmp" };

    // Token: 0x02000884 RID: 2180
    [Nullable(0)]
    private sealed class Entry : IEquatable<ArtifactThumbnails.Entry>
    {
      // Token: 0x060039A0 RID: 14752 RVA: 0x00689CF4 File Offset: 0x00689CF4
      public Entry(string Revision, int PixelWidth, ImageSource Image)
      {
        this.Revision = Revision;
        this.PixelWidth = PixelWidth;
        this.Image = Image;
        base..ctor();
      }

      // Token: 0x17000803 RID: 2051
      // (get) Token: 0x060039A1 RID: 14753 RVA: 0x00689D14 File Offset: 0x00689D14
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ArtifactThumbnails.Entry);
        }
      }

      // Token: 0x17000804 RID: 2052
      // (get) Token: 0x060039A2 RID: 14754 RVA: 0x00689D20 File Offset: 0x00689D20
      // (set) Token: 0x060039A3 RID: 14755 RVA: 0x00689D28 File Offset: 0x00689D28
      public string Revision { get; set; }

      // Token: 0x17000805 RID: 2053
      // (get) Token: 0x060039A4 RID: 14756 RVA: 0x00689D34 File Offset: 0x00689D34
      // (set) Token: 0x060039A5 RID: 14757 RVA: 0x00689D3C File Offset: 0x00689D3C
      public int PixelWidth { get; set; }

      // Token: 0x17000806 RID: 2054
      // (get) Token: 0x060039A6 RID: 14758 RVA: 0x00689D48 File Offset: 0x00689D48
      // (set) Token: 0x060039A7 RID: 14759 RVA: 0x00689D50 File Offset: 0x00689D50
      public ImageSource Image { get; set; }

      // Token: 0x060039A8 RID: 14760 RVA: 0x00689D5C File Offset: 0x00689D5C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Entry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060039A9 RID: 14761 RVA: 0x00689DA8 File Offset: 0x00689DA8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Revision = ");
        builder.Append(this.Revision);
        builder.Append(", PixelWidth = ");
        builder.Append(this.PixelWidth.ToString());
        builder.Append(", Image = ");
        builder.Append(this.Image);
        return true;
      }

      // Token: 0x060039AA RID: 14762 RVA: 0x00689E14 File Offset: 0x00689E14
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ArtifactThumbnails.Entry left, ArtifactThumbnails.Entry right)
      {
        return !(left == right);
      }

      // Token: 0x060039AB RID: 14763 RVA: 0x00689E20 File Offset: 0x00689E20
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ArtifactThumbnails.Entry left, ArtifactThumbnails.Entry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060039AC RID: 14764 RVA: 0x00689E34 File Offset: 0x00689E34
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Revision>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<PixelWidth>k__BackingField)) * -1521134295 + EqualityComparer<ImageSource>.Default.GetHashCode(this.<Image>k__BackingField);
      }

      // Token: 0x060039AD RID: 14765 RVA: 0x00689E98 File Offset: 0x00689E98
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ArtifactThumbnails.Entry);
      }

      // Token: 0x060039AE RID: 14766 RVA: 0x00689EA8 File Offset: 0x00689EA8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ArtifactThumbnails.Entry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Revision>k__BackingField, other.<Revision>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<PixelWidth>k__BackingField, other.<PixelWidth>k__BackingField) && EqualityComparer<ImageSource>.Default.Equals(this.<Image>k__BackingField, other.<Image>k__BackingField));
      }

      // Token: 0x060039B0 RID: 14768 RVA: 0x00689F24 File Offset: 0x00689F24
      [CompilerGenerated]
      private Entry(ArtifactThumbnails.Entry original)
      {
        this.Revision = original.<Revision>k__BackingField;
        this.PixelWidth = original.<PixelWidth>k__BackingField;
        this.Image = original.<Image>k__BackingField;
      }

      // Token: 0x060039B1 RID: 14769 RVA: 0x00689F50 File Offset: 0x00689F50
      [CompilerGenerated]
      public void Deconstruct(out string Revision, out int PixelWidth, out ImageSource Image)
      {
        Revision = this.Revision;
        PixelWidth = this.PixelWidth;
        Image = this.Image;
      }
    }
  }
}
