using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000189 RID: 393
  [NullableContext(1)]
  [Nullable(0)]
  public static class InkFiles
  {
    // Token: 0x0600176D RID: 5997 RVA: 0x003C3AA4 File Offset: 0x003C3AA4
    [return: Nullable(2)]
    public static string ReadUtf8(string path)
    {
      string text;
      try
      {
        text = File.ReadAllText(path, InkFiles.StrictUtf8);
      }
      catch (Exception)
      {
        text = null;
      }
      return text;
    }

    // Token: 0x0600176E RID: 5998 RVA: 0x003C3AD8 File Offset: 0x003C3AD8
    public static void WriteUtf8Atomically(string path, string text)
    {
      string text2 = Path.Combine(Path.GetDirectoryName(path) ?? ".", string.Concat(new string[]
      {
        ".",
        Path.GetFileName(path),
        ".",
        Guid.NewGuid().ToString("N"),
        ".tmp"
      }));
      File.WriteAllText(text2, text, new UTF8Encoding(false));
      File.Move(text2, path, true);
    }

    // Token: 0x0600176F RID: 5999 RVA: 0x003C3B50 File Offset: 0x003C3B50
    public static void WriteBytesAtomically(string path, byte[] bytes)
    {
      string directoryName = Path.GetDirectoryName(path);
      if (directoryName != null)
      {
        Directory.CreateDirectory(directoryName);
      }
      string text = Path.Combine(directoryName ?? ".", string.Concat(new string[]
      {
        ".",
        Path.GetFileName(path),
        ".",
        Guid.NewGuid().ToString("N"),
        ".tmp"
      }));
      File.WriteAllBytes(text, bytes);
      File.Move(text, path, true);
    }

    // Token: 0x06001770 RID: 6000 RVA: 0x003C3BCC File Offset: 0x003C3BCC
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "ModifiedAt", "Size" })]
    public static ValueTuple<DateTimeOffset, long>? Stat([Nullable(1)] string path)
    {
      ValueTuple<DateTimeOffset, long>? valueTuple;
      try
      {
        FileInfo fileInfo = new FileInfo(path);
        if (!fileInfo.Exists)
        {
          valueTuple = null;
          valueTuple = valueTuple;
        }
        else
        {
          valueTuple = new ValueTuple<DateTimeOffset, long>?(new ValueTuple<DateTimeOffset, long>(new DateTimeOffset(fileInfo.LastWriteTimeUtc, TimeSpan.Zero), fileInfo.Length));
        }
      }
      catch (Exception)
      {
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x06001771 RID: 6001 RVA: 0x003C3C38 File Offset: 0x003C3C38
    public static double SecondsSince1970(DateTimeOffset date)
    {
      return (date - DateTimeOffset.UnixEpoch).TotalSeconds;
    }

    // Token: 0x06001772 RID: 6002 RVA: 0x003C3C58 File Offset: 0x003C3C58
    public static DateTimeOffset FromSecondsSince1970(double seconds)
    {
      return DateTimeOffset.UnixEpoch.AddTicks((long)Math.Round(seconds * 10000000.0));
    }

    // Token: 0x04000A21 RID: 2593
    private static readonly UTF8Encoding StrictUtf8 = new UTF8Encoding(false, true);
  }
}
