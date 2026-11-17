using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Auth
{
  // Token: 0x0200022A RID: 554
  public static class AtomicFile
  {
    // Token: 0x06002056 RID: 8278 RVA: 0x003F4FB4 File Offset: 0x003F4FB4
    public static ReadOnlyMemory<byte> ReadJson([Nullable(1)] string path)
    {
      byte[] array = File.ReadAllBytes(path);
      return (array.Length >= 3 && array[0] == 239 && array[1] == 187 && array[2] == 191) ? array.AsMemory<byte>(3) : array;
    }

    // Token: 0x06002057 RID: 8279 RVA: 0x003F5000 File Offset: 0x003F5000
    [NullableContext(1)]
    public static void KeepUnreadable(string path)
    {
      try
      {
        if (File.Exists(path))
        {
          File.Copy(path, path + ".unreadable", true);
        }
      }
      catch
      {
      }
    }

    // Token: 0x06002058 RID: 8280 RVA: 0x003F503C File Offset: 0x003F503C
    public static void WriteAllBytes([Nullable(1)] string path, ReadOnlySpan<byte> bytes)
    {
      string directoryName = Path.GetDirectoryName(path);
      Directory.CreateDirectory(directoryName);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 3);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(path));
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted<int>(Environment.ProcessId);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted<Guid>(Guid.NewGuid(), "N");
      defaultInterpolatedStringHandler.AppendLiteral(".tmp");
      string text = Path.Combine(directoryName, defaultInterpolatedStringHandler.ToStringAndClear());
      using (FileStream fileStream = new FileStream(text, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
      {
        fileStream.Write(bytes);
      }
      if (File.Exists(path))
      {
        File.Replace(text, path, null, true);
        return;
      }
      File.Move(text, path);
    }
  }
}
