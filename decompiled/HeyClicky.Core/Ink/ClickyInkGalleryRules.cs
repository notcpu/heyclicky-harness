using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000158 RID: 344
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkGalleryRules
  {
    // Token: 0x06001506 RID: 5382 RVA: 0x003B2EE0 File Offset: 0x003B2EE0
    public static int Columns(int itemCount)
    {
      if (itemCount < 3)
      {
        return Math.Max(itemCount, 1);
      }
      return 3;
    }

    // Token: 0x06001507 RID: 5383 RVA: 0x003B2EF0 File Offset: 0x003B2EF0
    public static double TileWidth(double inkWidth, int columns)
    {
      return (inkWidth - 8.0 - (double)(columns - 1) * 16.0) / (double)columns;
    }

    // Token: 0x06001508 RID: 5384 RVA: 0x003B2F10 File Offset: 0x003B2F10
    public static double Tilt(int index)
    {
      return ClickyInkGalleryRules.Tilts[index % ClickyInkGalleryRules.Tilts.Length];
    }

    // Token: 0x06001509 RID: 5385 RVA: 0x003B2F24 File Offset: 0x003B2F24
    public static bool IsWebLink(string entry)
    {
      return entry.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || entry.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x0600150A RID: 5386 RVA: 0x003B2F44 File Offset: 0x003B2F44
    public static bool IsAudio(string path)
    {
      return ClickyInkGalleryRules.AudioExtensions.Contains(ClickyInkGalleryRules.PathExtension(path));
    }

    // Token: 0x0600150B RID: 5387 RVA: 0x003B2F58 File Offset: 0x003B2F58
    public static bool IsWebImage(string rawPath)
    {
      Uri uri;
      return ClickyInkGalleryRules.IsWebLink(rawPath) && Uri.TryCreate(rawPath, UriKind.Absolute, out uri) && ClickyInkGalleryRules.WebImageExtensions.Contains(ClickyInkGalleryRules.PathExtension(uri.AbsolutePath));
    }

    // Token: 0x0600150C RID: 5388 RVA: 0x003B2F90 File Offset: 0x003B2F90
    public static string PickCaption(ClickyInkDocument.Item item)
    {
      if (item.Text.Length != 0)
      {
        return item.Text;
      }
      return ClickyInkGalleryRules.LastPathComponent(item.Url ?? "");
    }

    // Token: 0x0600150D RID: 5389 RVA: 0x003B2FBC File Offset: 0x003B2FBC
    public static string PathExtension(string path)
    {
      string text = ClickyInkGalleryRules.LastPathComponent(path);
      int num = text.LastIndexOf('.');
      if (num > 0)
      {
        string text2 = text;
        int num2 = num + 1;
        return text2.Substring(num2, text2.Length - num2).ToLowerInvariant();
      }
      return "";
    }

    // Token: 0x0600150E RID: 5390 RVA: 0x003B3000 File Offset: 0x003B3000
    public static string LastPathComponent(string path)
    {
      string text = path.TrimEnd(new char[] { '/', '\\' });
      int num = text.LastIndexOfAny(new char[] { '/', '\\' });
      if (num >= 0)
      {
        string text2 = text;
        int num2 = num + 1;
        return text2.Substring(num2, text2.Length - num2);
      }
      return text;
    }

    // Token: 0x04000923 RID: 2339
    public const double Spacing = 16.0;

    // Token: 0x04000924 RID: 2340
    private static readonly double[] Tilts = new double[] { -1.1, 0.8, -0.5, 1.0, -0.8, 0.6 };

    // Token: 0x04000925 RID: 2341
    private static readonly HashSet<string> AudioExtensions = new HashSet<string> { "mp3", "m4a", "wav", "aiff", "aif", "aac", "flac", "caf" };

    // Token: 0x04000926 RID: 2342
    private static readonly HashSet<string> WebImageExtensions = new HashSet<string> { "jpg", "jpeg", "png", "webp", "gif", "heic" };
  }
}
