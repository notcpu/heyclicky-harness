using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000107 RID: 263
  [NullableContext(1)]
  [Nullable(0)]
  public static class ComposerAttachmentPolicy
  {
    // Token: 0x06001139 RID: 4409 RVA: 0x0039AFD4 File Offset: 0x0039AFD4
    [return: TupleElementNames(new string[] { "Accepted", "ExceededCap" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<IReadOnlyList<string>, bool> Admit(int attachedCount, IEnumerable<string> incoming)
    {
      List<string> list = incoming.Where<string>((string p) => !string.IsNullOrEmpty(p)).ToList<string>();
      int num = Math.Max(0, 10 - attachedCount);
      return new ValueTuple<IReadOnlyList<string>, bool>(list.Take<string>(num).ToList<string>(), list.Count > num);
    }

    // Token: 0x0600113A RID: 4410 RVA: 0x0039B034 File Offset: 0x0039B034
    public static IReadOnlyList<string> PastableImageFiles(IEnumerable<string> copiedPaths)
    {
      return copiedPaths.Where<string>((string p) => !string.IsNullOrEmpty(p) && ComposerAttachmentPolicy.ImageExtensions.Contains(Path.GetExtension(p))).ToList<string>();
    }

    // Token: 0x0600113B RID: 4411 RVA: 0x0039B060 File Offset: 0x0039B060
    // Note: this type is marked as 'beforefieldinit'.
    static ComposerAttachmentPolicy()
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
      defaultInterpolatedStringHandler.AppendLiteral("You can attach up to ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(10);
      defaultInterpolatedStringHandler.AppendLiteral(" files");
      ComposerAttachmentPolicy.CapNotice = defaultInterpolatedStringHandler.ToStringAndClear();
      ComposerAttachmentPolicy.CapNoticeDuration = TimeSpan.FromSeconds(2.5);
      ComposerAttachmentPolicy.ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".png", ".jpg", ".jpeg", ".gif", ".heic", ".tiff", ".tif", ".bmp", ".webp" };
    }

    // Token: 0x04000778 RID: 1912
    public const int MaxAttachmentCount = 10;

    // Token: 0x04000779 RID: 1913
    public static readonly string CapNotice;

    // Token: 0x0400077A RID: 1914
    public static readonly TimeSpan CapNoticeDuration;

    // Token: 0x0400077B RID: 1915
    private static readonly HashSet<string> ImageExtensions;
  }
}
