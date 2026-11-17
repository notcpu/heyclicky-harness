using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HeyClicky.Core.Documents.Spreadsheet;

namespace HeyClicky.App.Documents
{
  // Token: 0x020002CB RID: 715
  [NullableContext(1)]
  [Nullable(0)]
  public static class SpreadsheetAssetCache
  {
    // Token: 0x06001E1C RID: 7708 RVA: 0x00605DDC File Offset: 0x00605DDC
    [return: Nullable(new byte[] { 1, 2 })]
    public static async Task<SpreadsheetWorkbook> LoadAsync(string path)
    {
      string text = await Task.Run<string>(delegate
      {
        string text3;
        try
        {
          FileInfo fileInfo = new FileInfo(path);
          string text2;
          if (!fileInfo.Exists)
          {
            text2 = null;
          }
          else
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
            defaultInterpolatedStringHandler.AppendFormatted<long>(fileInfo.LastWriteTimeUtc.Ticks);
            defaultInterpolatedStringHandler.AppendLiteral("|");
            defaultInterpolatedStringHandler.AppendFormatted<long>(fileInfo.Length);
            text2 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          text3 = text2;
        }
        catch (Exception)
        {
          text3 = null;
        }
        return text3;
      });
      string revision = text;
      SpreadsheetWorkbook spreadsheetWorkbook;
      if (revision == null)
      {
        spreadsheetWorkbook = null;
      }
      else
      {
        object obj = SpreadsheetAssetCache.gate;
        lock (obj)
        {
          SpreadsheetAssetCache.Entry entry = SpreadsheetAssetCache.entries.FirstOrDefault<SpreadsheetAssetCache.Entry>((SpreadsheetAssetCache.Entry e) => e.Path == path);
          if (entry != null && entry.Revision == revision)
          {
            SpreadsheetAssetCache.entries.Remove(entry);
            SpreadsheetAssetCache.entries.Insert(0, entry);
            return entry.Workbook;
          }
        }
        SpreadsheetWorkbook spreadsheetWorkbook2 = await Task.Run<SpreadsheetWorkbook>(() => SpreadsheetWorkbook.Read(path));
        obj = SpreadsheetAssetCache.gate;
        lock (obj)
        {
          SpreadsheetAssetCache.entries.RemoveAll((SpreadsheetAssetCache.Entry e) => e.Path == path);
          SpreadsheetAssetCache.entries.Insert(0, new SpreadsheetAssetCache.Entry(path, revision, spreadsheetWorkbook2));
          while (SpreadsheetAssetCache.entries.Count > 3)
          {
            SpreadsheetAssetCache.entries.RemoveAt(SpreadsheetAssetCache.entries.Count - 1);
          }
        }
        spreadsheetWorkbook = spreadsheetWorkbook2;
      }
      return spreadsheetWorkbook;
    }

    // Token: 0x04000E7E RID: 3710
    private static readonly List<SpreadsheetAssetCache.Entry> entries = new List<SpreadsheetAssetCache.Entry>();

    // Token: 0x04000E7F RID: 3711
    private static readonly object gate = new object();

    // Token: 0x020008ED RID: 2285
    [Nullable(0)]
    private sealed class Entry : IEquatable<SpreadsheetAssetCache.Entry>
    {
      // Token: 0x06003AF9 RID: 15097 RVA: 0x00692C4C File Offset: 0x00692C4C
      public Entry(string Path, string Revision, [Nullable(2)] SpreadsheetWorkbook Workbook)
      {
        this.Path = Path;
        this.Revision = Revision;
        this.Workbook = Workbook;
        base..ctor();
      }

      // Token: 0x17000823 RID: 2083
      // (get) Token: 0x06003AFA RID: 15098 RVA: 0x00692C6C File Offset: 0x00692C6C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(SpreadsheetAssetCache.Entry);
        }
      }

      // Token: 0x17000824 RID: 2084
      // (get) Token: 0x06003AFB RID: 15099 RVA: 0x00692C78 File Offset: 0x00692C78
      // (set) Token: 0x06003AFC RID: 15100 RVA: 0x00692C80 File Offset: 0x00692C80
      public string Path { get; set; }

      // Token: 0x17000825 RID: 2085
      // (get) Token: 0x06003AFD RID: 15101 RVA: 0x00692C8C File Offset: 0x00692C8C
      // (set) Token: 0x06003AFE RID: 15102 RVA: 0x00692C94 File Offset: 0x00692C94
      public string Revision { get; set; }

      // Token: 0x17000826 RID: 2086
      // (get) Token: 0x06003AFF RID: 15103 RVA: 0x00692CA0 File Offset: 0x00692CA0
      // (set) Token: 0x06003B00 RID: 15104 RVA: 0x00692CA8 File Offset: 0x00692CA8
      [Nullable(2)]
      public SpreadsheetWorkbook Workbook
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x06003B01 RID: 15105 RVA: 0x00692CB4 File Offset: 0x00692CB4
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

      // Token: 0x06003B02 RID: 15106 RVA: 0x00692D00 File Offset: 0x00692D00
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Path = ");
        builder.Append(this.Path);
        builder.Append(", Revision = ");
        builder.Append(this.Revision);
        builder.Append(", Workbook = ");
        builder.Append(this.Workbook);
        return true;
      }

      // Token: 0x06003B03 RID: 15107 RVA: 0x00692D60 File Offset: 0x00692D60
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(SpreadsheetAssetCache.Entry left, SpreadsheetAssetCache.Entry right)
      {
        return !(left == right);
      }

      // Token: 0x06003B04 RID: 15108 RVA: 0x00692D6C File Offset: 0x00692D6C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(SpreadsheetAssetCache.Entry left, SpreadsheetAssetCache.Entry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003B05 RID: 15109 RVA: 0x00692D80 File Offset: 0x00692D80
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Revision>k__BackingField)) * -1521134295 + EqualityComparer<SpreadsheetWorkbook>.Default.GetHashCode(this.<Workbook>k__BackingField);
      }

      // Token: 0x06003B06 RID: 15110 RVA: 0x00692DE4 File Offset: 0x00692DE4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as SpreadsheetAssetCache.Entry);
      }

      // Token: 0x06003B07 RID: 15111 RVA: 0x00692DF4 File Offset: 0x00692DF4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(SpreadsheetAssetCache.Entry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Revision>k__BackingField, other.<Revision>k__BackingField) && EqualityComparer<SpreadsheetWorkbook>.Default.Equals(this.<Workbook>k__BackingField, other.<Workbook>k__BackingField));
      }

      // Token: 0x06003B09 RID: 15113 RVA: 0x00692E70 File Offset: 0x00692E70
      [CompilerGenerated]
      private Entry(SpreadsheetAssetCache.Entry original)
      {
        this.Path = original.<Path>k__BackingField;
        this.Revision = original.<Revision>k__BackingField;
        this.Workbook = original.<Workbook>k__BackingField;
      }

      // Token: 0x06003B0A RID: 15114 RVA: 0x00692E9C File Offset: 0x00692E9C
      [CompilerGenerated]
      public void Deconstruct(out string Path, out string Revision, [Nullable(2)] out SpreadsheetWorkbook Workbook)
      {
        Path = this.Path;
        Revision = this.Revision;
        Workbook = this.Workbook;
      }
    }
  }
}
