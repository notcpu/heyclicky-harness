using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Documents.Spreadsheet
{
  // Token: 0x020001B6 RID: 438
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SpreadsheetZipArchive
  {
    // Token: 0x06001964 RID: 6500 RVA: 0x003D1300 File Offset: 0x003D1300
    public SpreadsheetZipArchive(string path)
    {
      using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete))
      {
        if (fileStream.Length > 83886080L)
        {
          throw new SpreadsheetReadException(SpreadsheetReadErrorKind.TooLarge, null);
        }
        this.bytes = new byte[fileStream.Length];
        fileStream.ReadExactly(this.bytes);
      }
      this.entriesByName = SpreadsheetZipArchive.ReadCentralDirectory(this.bytes);
    }

    // Token: 0x06001965 RID: 6501 RVA: 0x003D1384 File Offset: 0x003D1384
    public SpreadsheetZipArchive(byte[] data)
    {
      if (data.Length > 83886080)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.TooLarge, null);
      }
      this.bytes = data;
      this.entriesByName = SpreadsheetZipArchive.ReadCentralDirectory(this.bytes);
    }

    // Token: 0x170005A6 RID: 1446
    // (get) Token: 0x06001966 RID: 6502 RVA: 0x003D13B8 File Offset: 0x003D13B8
    public IEnumerable<string> EntryNames
    {
      get
      {
        return this.entriesByName.Keys;
      }
    }

    // Token: 0x06001967 RID: 6503 RVA: 0x003D13C8 File Offset: 0x003D13C8
    [return: Nullable(2)]
    public byte[] Contents(string entryName)
    {
      string text = (entryName.StartsWith('/') ? entryName.Substring(1, entryName.Length - 1) : entryName);
      SpreadsheetZipArchive.Entry entry;
      if (!this.entriesByName.TryGetValue(text, out entry))
      {
        return null;
      }
      int localHeaderOffset = entry.LocalHeaderOffset;
      if (localHeaderOffset < 0 || localHeaderOffset + 30 > this.bytes.Length || SpreadsheetZipArchive.ReadUInt32(this.bytes, localHeaderOffset) != 67324752U)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.UnsupportedEntry, entryName);
      }
      int num = (int)SpreadsheetZipArchive.ReadUInt16(this.bytes, localHeaderOffset + 26);
      int num2 = (int)SpreadsheetZipArchive.ReadUInt16(this.bytes, localHeaderOffset + 28);
      long num3 = (long)localHeaderOffset + 30L + (long)num + (long)num2;
      if (num3 + (long)entry.CompressedSize > (long)this.bytes.Length || entry.CompressedSize < 0)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.UnsupportedEntry, entryName);
      }
      ushort compressionMethod = entry.CompressionMethod;
      byte[] array;
      if (compressionMethod != 0)
      {
        if (compressionMethod != 8)
        {
          throw new SpreadsheetReadException(SpreadsheetReadErrorKind.UnsupportedEntry, entryName);
        }
        array = SpreadsheetZipArchive.Inflate(this.bytes, (int)num3, entry.CompressedSize, entry.UncompressedSize);
      }
      else
      {
        array = this.bytes.AsSpan<byte>((int)num3, entry.CompressedSize).ToArray();
      }
      return array;
    }

    // Token: 0x06001968 RID: 6504 RVA: 0x003D14F8 File Offset: 0x003D14F8
    private static Dictionary<string, SpreadsheetZipArchive.Entry> ReadCentralDirectory(byte[] bytes)
    {
      if (bytes.Length < 22)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.NotAZipArchive, null);
      }
      int num = Math.Max(0, bytes.Length - 65557);
      int? num2 = null;
      for (int i = bytes.Length - 22; i >= num; i--)
      {
        if (SpreadsheetZipArchive.ReadUInt32(bytes, i) == 101010256U)
        {
          num2 = new int?(i);
          break;
        }
      }
      if (num2 != null)
      {
        int valueOrDefault = num2.GetValueOrDefault();
        int num3 = (int)SpreadsheetZipArchive.ReadUInt16(bytes, valueOrDefault + 10);
        long num4 = (long)((ulong)SpreadsheetZipArchive.ReadUInt32(bytes, valueOrDefault + 16));
        Dictionary<string, SpreadsheetZipArchive.Entry> dictionary = new Dictionary<string, SpreadsheetZipArchive.Entry>(num3);
        for (int j = 0; j < num3; j++)
        {
          if (num4 + 46L > (long)bytes.Length || SpreadsheetZipArchive.ReadUInt32(bytes, (int)num4) != 33639248U)
          {
            throw new SpreadsheetReadException(SpreadsheetReadErrorKind.NotAZipArchive, null);
          }
          int num5 = (int)num4;
          ushort num6 = SpreadsheetZipArchive.ReadUInt16(bytes, num5 + 10);
          uint num7 = SpreadsheetZipArchive.ReadUInt32(bytes, num5 + 20);
          uint num8 = SpreadsheetZipArchive.ReadUInt32(bytes, num5 + 24);
          int num9 = (int)SpreadsheetZipArchive.ReadUInt16(bytes, num5 + 28);
          int num10 = (int)SpreadsheetZipArchive.ReadUInt16(bytes, num5 + 30);
          int num11 = (int)SpreadsheetZipArchive.ReadUInt16(bytes, num5 + 32);
          uint num12 = SpreadsheetZipArchive.ReadUInt32(bytes, num5 + 42);
          int num13 = num5 + 46;
          if (num13 + num9 > bytes.Length)
          {
            throw new SpreadsheetReadException(SpreadsheetReadErrorKind.NotAZipArchive, null);
          }
          Span<byte> span = bytes.AsSpan<byte>(num13, num9);
          string text;
          try
          {
            text = new UTF8Encoding(false, true).GetString(span);
          }
          catch (DecoderFallbackException)
          {
            text = Encoding.Latin1.GetString(span);
          }
          dictionary[text] = new SpreadsheetZipArchive.Entry(num6, (int)Math.Min(num7, 2147483647U), (int)Math.Min(num8, 2147483647U), (int)Math.Min(num12, 2147483647U));
          num4 = (long)(num13 + num9 + num10 + num11);
        }
        return dictionary;
      }
      throw new SpreadsheetReadException(SpreadsheetReadErrorKind.NotAZipArchive, null);
    }

    // Token: 0x06001969 RID: 6505 RVA: 0x003D16D8 File Offset: 0x003D16D8
    private static byte[] Inflate(byte[] bytes, int start, int count, int expectedSize)
    {
      if (expectedSize <= 0)
      {
        return Array.Empty<byte>();
      }
      if (expectedSize > 419430400)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.TooLarge, null);
      }
      byte[] array = new byte[expectedSize];
      int i = 0;
      try
      {
        using (DeflateStream deflateStream = new DeflateStream(new MemoryStream(bytes, start, count, false), CompressionMode.Decompress))
        {
          while (i < expectedSize)
          {
            int num = deflateStream.Read(array, i, expectedSize - i);
            if (num == 0)
            {
              break;
            }
            i += num;
          }
        }
      }
      catch (InvalidDataException)
      {
        i = 0;
      }
      if (i <= 0)
      {
        throw new SpreadsheetReadException(SpreadsheetReadErrorKind.Unreadable, null);
      }
      if (i >= expectedSize)
      {
        return array;
      }
      return RuntimeHelpers.GetSubArray<byte>(array, Range.EndAt(i));
    }

    // Token: 0x0600196A RID: 6506 RVA: 0x003D1784 File Offset: 0x003D1784
    private static ushort ReadUInt16(byte[] bytes, int offset)
    {
      return (ushort)((int)bytes[offset] | ((int)bytes[offset + 1] << 8));
    }

    // Token: 0x0600196B RID: 6507 RVA: 0x003D1794 File Offset: 0x003D1794
    private static uint ReadUInt32(byte[] bytes, int offset)
    {
      return (uint)((int)bytes[offset] | ((int)bytes[offset + 1] << 8) | ((int)bytes[offset + 2] << 16) | ((int)bytes[offset + 3] << 24));
    }

    // Token: 0x04000AE9 RID: 2793
    public const int MaximumArchiveBytes = 83886080;

    // Token: 0x04000AEA RID: 2794
    private const int MaximumEntryBytes = 419430400;

    // Token: 0x04000AEB RID: 2795
    private readonly byte[] bytes;

    // Token: 0x04000AEC RID: 2796
    private readonly Dictionary<string, SpreadsheetZipArchive.Entry> entriesByName;

    // Token: 0x020005A0 RID: 1440
    [NullableContext(0)]
    private readonly struct Entry : IEquatable<SpreadsheetZipArchive.Entry>
    {
      // Token: 0x06003604 RID: 13828 RVA: 0x004432DC File Offset: 0x004432DC
      public Entry(ushort CompressionMethod, int CompressedSize, int UncompressedSize, int LocalHeaderOffset)
      {
        this.CompressionMethod = CompressionMethod;
        this.CompressedSize = CompressedSize;
        this.UncompressedSize = UncompressedSize;
        this.LocalHeaderOffset = LocalHeaderOffset;
      }

      // Token: 0x17000986 RID: 2438
      // (get) Token: 0x06003605 RID: 13829 RVA: 0x004432FC File Offset: 0x004432FC
      // (set) Token: 0x06003606 RID: 13830 RVA: 0x00443304 File Offset: 0x00443304
      public ushort CompressionMethod { get; set; }

      // Token: 0x17000987 RID: 2439
      // (get) Token: 0x06003607 RID: 13831 RVA: 0x00443310 File Offset: 0x00443310
      // (set) Token: 0x06003608 RID: 13832 RVA: 0x00443318 File Offset: 0x00443318
      public int CompressedSize { get; set; }

      // Token: 0x17000988 RID: 2440
      // (get) Token: 0x06003609 RID: 13833 RVA: 0x00443324 File Offset: 0x00443324
      // (set) Token: 0x0600360A RID: 13834 RVA: 0x0044332C File Offset: 0x0044332C
      public int UncompressedSize { get; set; }

      // Token: 0x17000989 RID: 2441
      // (get) Token: 0x0600360B RID: 13835 RVA: 0x00443338 File Offset: 0x00443338
      // (set) Token: 0x0600360C RID: 13836 RVA: 0x00443340 File Offset: 0x00443340
      public int LocalHeaderOffset { get; set; }

      // Token: 0x0600360D RID: 13837 RVA: 0x0044334C File Offset: 0x0044334C
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

      // Token: 0x0600360E RID: 13838 RVA: 0x00443398 File Offset: 0x00443398
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("CompressionMethod = ");
        builder.Append(this.CompressionMethod.ToString());
        builder.Append(", CompressedSize = ");
        builder.Append(this.CompressedSize.ToString());
        builder.Append(", UncompressedSize = ");
        builder.Append(this.UncompressedSize.ToString());
        builder.Append(", LocalHeaderOffset = ");
        builder.Append(this.LocalHeaderOffset.ToString());
        return true;
      }

      // Token: 0x0600360F RID: 13839 RVA: 0x00443444 File Offset: 0x00443444
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetZipArchive.Entry left, SpreadsheetZipArchive.Entry right)
      {
        return !(left == right);
      }

      // Token: 0x06003610 RID: 13840 RVA: 0x00443450 File Offset: 0x00443450
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetZipArchive.Entry left, SpreadsheetZipArchive.Entry right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003611 RID: 13841 RVA: 0x0044345C File Offset: 0x0044345C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<ushort>.Default.GetHashCode(this.<CompressionMethod>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CompressedSize>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<UncompressedSize>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<LocalHeaderOffset>k__BackingField);
      }

      // Token: 0x06003612 RID: 13842 RVA: 0x004434C0 File Offset: 0x004434C0
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is SpreadsheetZipArchive.Entry && this.Equals((SpreadsheetZipArchive.Entry)obj);
      }

      // Token: 0x06003613 RID: 13843 RVA: 0x004434D8 File Offset: 0x004434D8
      [CompilerGenerated]
      public bool Equals(SpreadsheetZipArchive.Entry other)
      {
        return EqualityComparer<ushort>.Default.Equals(this.<CompressionMethod>k__BackingField, other.<CompressionMethod>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CompressedSize>k__BackingField, other.<CompressedSize>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<UncompressedSize>k__BackingField, other.<UncompressedSize>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<LocalHeaderOffset>k__BackingField, other.<LocalHeaderOffset>k__BackingField);
      }

      // Token: 0x06003614 RID: 13844 RVA: 0x00443548 File Offset: 0x00443548
      [CompilerGenerated]
      public void Deconstruct(out ushort CompressionMethod, out int CompressedSize, out int UncompressedSize, out int LocalHeaderOffset)
      {
        CompressionMethod = this.CompressionMethod;
        CompressedSize = this.CompressedSize;
        UncompressedSize = this.UncompressedSize;
        LocalHeaderOffset = this.LocalHeaderOffset;
      }
    }
  }
}
