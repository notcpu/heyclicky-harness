using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006A RID: 106
  [NullableContext(1)]
  [Nullable(0)]
  public static class Attachments
  {
    // Token: 0x06000575 RID: 1397 RVA: 0x00358904 File Offset: 0x00358904
    public static IReadOnlyList<string> NormalizedPaths([Nullable(new byte[] { 2, 1 })] IEnumerable<string> paths)
    {
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<string> list = new List<string>();
      foreach (string text in (paths ?? Array.Empty<string>()))
      {
        string text2 = ((text != null) ? text.Trim() : null) ?? "";
        if (text2.Length != 0 && Path.IsPathFullyQualified(text2))
        {
          string fullPath;
          try
          {
            fullPath = Path.GetFullPath(text2);
          }
          catch
          {
            continue;
          }
          if (hashSet.Add(fullPath))
          {
            list.Add(fullPath);
          }
        }
      }
      return list;
    }

    // Token: 0x06000576 RID: 1398 RVA: 0x003589B4 File Offset: 0x003589B4
    public static bool IsImage(string path)
    {
      return Attachments.ImageExtensions.Contains(Path.GetExtension(path).TrimStart('.'));
    }

    // Token: 0x06000577 RID: 1399 RVA: 0x003589D0 File Offset: 0x003589D0
    public static bool RequiresAgent(IReadOnlyList<string> paths)
    {
      IReadOnlyList<string> readOnlyList = Attachments.NormalizedPaths(paths);
      if (readOnlyList.Count == 0)
      {
        return true;
      }
      IEnumerable<string> enumerable = readOnlyList;
      Func<string, bool> func;
      if ((func = Attachments.<>O.<0>__IsImage) == null)
      {
        func = (Attachments.<>O.<0>__IsImage = new Func<string, bool>(Attachments.IsImage));
      }
      bool flag = enumerable.All<string>(func);
      IEnumerable<string> enumerable2 = readOnlyList;
      Func<string, long> func2;
      if ((func2 = Attachments.<>O.<1>__FileSize) == null)
      {
        func2 = (Attachments.<>O.<1>__FileSize = new Func<string, long>(Attachments.FileSize));
      }
      long num = enumerable2.Sum<string>(func2);
      return !flag || readOnlyList.Count > 2 || num > 5000000L;
    }

    // Token: 0x06000578 RID: 1400 RVA: 0x00358A48 File Offset: 0x00358A48
    public static string SummaryLine(IReadOnlyList<string> paths)
    {
      return string.Join(", ", Attachments.NormalizedPaths(paths).Select<string, string>(delegate(string path)
      {
        string text = Path.GetExtension(path).TrimStart('.').ToLowerInvariant();
        string text2 = (Attachments.IsImage(path) ? "image" : ((text.Length == 0) ? "file" : text));
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 3);
        defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(path));
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(Attachments.FormatBytes(Attachments.FileSize(path)));
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }));
    }

    // Token: 0x06000579 RID: 1401 RVA: 0x00358A80 File Offset: 0x00358A80
    public static string WordsWithSummary(string words, IReadOnlyList<string> paths, bool imagesRideAlong)
    {
      IReadOnlyList<string> readOnlyList = Attachments.NormalizedPaths(paths);
      if (readOnlyList.Count == 0)
      {
        return words;
      }
      string text = (imagesRideAlong ? "The images are attached for you to look at; for work with them (editing, saving, sending) start a job for it with sessions_spawn." : "You can't open these files yourself: for anything with them, start a job for it with sessions_spawn (it gets the files).");
      string text2 = (string.IsNullOrWhiteSpace(words) ? "[They attached files with no message." : "[They attached files.");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 4);
      defaultInterpolatedStringHandler.AppendFormatted(words);
      defaultInterpolatedStringHandler.AppendLiteral("\n\n");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" Files: ");
      defaultInterpolatedStringHandler.AppendFormatted(Attachments.SummaryLine(readOnlyList));
      defaultInterpolatedStringHandler.AppendLiteral(". ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("]");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x0600057A RID: 1402 RVA: 0x00358B2C File Offset: 0x00358B2C
    public static async Task<IReadOnlyList<byte[]>> AttachedImagesAsync(IReadOnlyList<string> paths)
    {
      IReadOnlyList<string> readOnlyList = Attachments.NormalizedPaths(paths);
      IReadOnlyList<byte[]> readOnlyList2;
      if (readOnlyList.Count == 0 || Attachments.RequiresAgent(readOnlyList))
      {
        readOnlyList2 = Array.Empty<byte[]>();
      }
      else
      {
        List<byte[]> images = new List<byte[]>();
        IEnumerable<string> enumerable = readOnlyList;
        Func<string, bool> func;
        if ((func = Attachments.<>O.<0>__IsImage) == null)
        {
          func = (Attachments.<>O.<0>__IsImage = new Func<string, bool>(Attachments.IsImage));
        }
        foreach (string text in enumerable.Where<string>(func).Take<string>(2))
        {
          byte[] array = await Attachments.JpegAsync(text, 0.85, 4096).ConfigureAwait(false);
          if (array != null)
          {
            images.Add(array);
          }
        }
        IEnumerator<string> enumerator = null;
        readOnlyList2 = images;
      }
      return readOnlyList2;
    }

    // Token: 0x0600057B RID: 1403 RVA: 0x00358B70 File Offset: 0x00358B70
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<byte[]> JpegAsync(string path, double quality = 0.85, int maxDimension = 4096)
    {
      byte[] array2;
      try
      {
        byte[] array = await File.ReadAllBytesAsync(path, default(CancellationToken)).ConfigureAwait(false);
        using (InMemoryRandomAccessStream input = new InMemoryRandomAccessStream())
        {
          await input.WriteAsync(array.AsBuffer());
          input.Seek(0UL);
          object obj = await BitmapDecoder.CreateAsync(input);
          uint pixelWidth = obj.PixelWidth;
          uint pixelHeight = obj.PixelHeight;
          double num = Math.Min(1.0, (double)maxDimension / Math.Max(pixelWidth, pixelHeight));
          BitmapTransform transform = new BitmapTransform
          {
            ScaledWidth = Math.Max(1U, (uint)Math.Round(pixelWidth * num)),
            ScaledHeight = Math.Max(1U, (uint)Math.Round(pixelHeight * num)),
            InterpolationMode = BitmapInterpolationMode.Fant
          };
          PixelDataProvider pixelDataProvider = await obj.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied, transform, ExifOrientationMode.RespectExifOrientation, ColorManagementMode.ColorManageToSRgb);
          byte[] data = pixelDataProvider.DetachPixelData();
          using (InMemoryRandomAccessStream output = new InMemoryRandomAccessStream())
          {
            BitmapPropertySet bitmapPropertySet = new BitmapPropertySet();
            bitmapPropertySet["ImageQuality"] = new BitmapTypedValue(quality, PropertyType.Single);
            BitmapPropertySet bitmapPropertySet2 = bitmapPropertySet;
            BitmapEncoder bitmapEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, output, bitmapPropertySet2);
            for (int i = 0; i < data.Length; i += 4)
            {
              byte b = data[i + 3];
              if (b != 255)
              {
                int num2 = (int)(byte.MaxValue - b);
                data[i] = (byte)Math.Min(255, (int)data[i] + num2);
                data[i + 1] = (byte)Math.Min(255, (int)data[i + 1] + num2);
                data[i + 2] = (byte)Math.Min(255, (int)data[i + 2] + num2);
                data[i + 3] = byte.MaxValue;
              }
            }
            bitmapEncoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, transform.ScaledWidth, transform.ScaledHeight, 96.0, 96.0, data);
            await bitmapEncoder.FlushAsync();
            byte[] result = new byte[output.Size];
            output.Seek(0UL);
            await output.ReadAsync(result.AsBuffer(), (uint)result.Length, InputStreamOptions.None);
            array2 = result;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Attachments", "image " + Path.GetFileName(path) + " not read: " + ex.Message);
        array2 = null;
      }
      return array2;
    }

    // Token: 0x0600057C RID: 1404 RVA: 0x00358BC4 File Offset: 0x00358BC4
    public static long FileSize(string path)
    {
      long num;
      try
      {
        num = new FileInfo(path).Length;
      }
      catch
      {
        num = 0L;
      }
      return num;
    }

    // Token: 0x0600057D RID: 1405 RVA: 0x00358BF8 File Offset: 0x00358BF8
    public static string FormatBytes(long bytes)
    {
      if (bytes <= 0L)
      {
        return "Zero KB";
      }
      if (bytes < 1000L)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendFormatted<long>(bytes);
        defaultInterpolatedStringHandler.AppendLiteral(" bytes");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string[] array = new string[] { "KB", "MB", "GB", "TB" };
      double num = (double)bytes;
      int num2 = -1;
      do
      {
        num /= 1000.0;
        num2++;
      }
      while (num >= 1000.0 && num2 < array.Length - 1);
      string text = ((num2 == 0 || num >= 100.0) ? "0" : ((num >= 10.0) ? "0.#" : "0.#"));
      return num.ToString(text, CultureInfo.InvariantCulture) + " " + array[num2];
    }

    // Token: 0x04000285 RID: 645
    public const string FilesOnlyWords = "Please review and act on the attached files.";

    // Token: 0x04000286 RID: 646
    private const int ImageLaneMaxFileCount = 2;

    // Token: 0x04000287 RID: 647
    private const long ImageLaneMaxTotalBytes = 5000000L;

    // Token: 0x04000288 RID: 648
    private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
      "avif", "bmp", "gif", "heic", "heif", "jpeg", "jpg", "png", "tif", "tiff",
      "webp"
    };

    // Token: 0x020002AE RID: 686
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001277 RID: 4727
      [Nullable(0)]
      public static Func<string, bool> <0>__IsImage;

      // Token: 0x04001278 RID: 4728
      [Nullable(0)]
      public static Func<string, long> <1>__FileSize;
    }
  }
}
