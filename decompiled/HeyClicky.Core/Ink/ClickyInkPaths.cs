using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000178 RID: 376
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyInkPaths
  {
    // Token: 0x1700052F RID: 1327
    // (get) Token: 0x060016AD RID: 5805 RVA: 0x003C0448 File Offset: 0x003C0448
    // (set) Token: 0x060016AE RID: 5806 RVA: 0x003C0450 File Offset: 0x003C0450
    public static ClickyInkPaths.FileRules MainArtifacts { get; set; } = new ClickyInkPaths.FileRules((IReadOnlyList<string> _) => new HashSet<string>(StringComparer.OrdinalIgnoreCase), (string _) => true);

    // Token: 0x060016AF RID: 5807 RVA: 0x003C0458 File Offset: 0x003C0458
    public static bool IsWebLink(string entry)
    {
      return entry.StartsWith("http://", StringComparison.Ordinal) || entry.StartsWith("https://", StringComparison.Ordinal);
    }

    // Token: 0x060016B0 RID: 5808 RVA: 0x003C0478 File Offset: 0x003C0478
    public static string ExpandingTilde(string path)
    {
      if (path == "~")
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
      }
      if (path.StartsWith("~/", StringComparison.Ordinal) || path.StartsWith("~\\", StringComparison.Ordinal))
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), path.Substring(2, path.Length - 2));
      }
      return path;
    }

    // Token: 0x060016B1 RID: 5809 RVA: 0x003C04D8 File Offset: 0x003C04D8
    public static string Standardized(string path)
    {
      string text;
      try
      {
        text = Path.GetFullPath(path);
      }
      catch
      {
        text = path;
      }
      return text;
    }

    // Token: 0x060016B2 RID: 5810 RVA: 0x003C0504 File Offset: 0x003C0504
    public static string PathExtension(string path)
    {
      string text = path.TrimEnd(new char[] { '/', '\\' });
      int num = Math.Max(text.LastIndexOf('/'), text.LastIndexOf('\\'));
      string text2;
      if (num < 0)
      {
        text2 = text;
      }
      else
      {
        string text3 = text;
        int num2 = num + 1;
        text2 = text3.Substring(num2, text3.Length - num2);
      }
      string text4 = text2;
      int num3 = text4.LastIndexOf('.');
      if (num3 > 0 && num3 != text4.Length - 1)
      {
        string text3 = text4;
        int num2 = num3 + 1;
        return text3.Substring(num2, text3.Length - num2);
      }
      return "";
    }

    // Token: 0x0200054E RID: 1358
    [Nullable(0)]
    public sealed class FileRules : IEquatable<ClickyInkPaths.FileRules>
    {
      // Token: 0x060033C8 RID: 13256 RVA: 0x0043E19C File Offset: 0x0043E19C
      public FileRules(Func<IReadOnlyList<string>, ISet<string>> SupportingPaths, Func<string, bool> IsMeaningfulFile)
      {
        this.SupportingPaths = SupportingPaths;
        this.IsMeaningfulFile = IsMeaningfulFile;
        base..ctor();
      }

      // Token: 0x17000934 RID: 2356
      // (get) Token: 0x060033C9 RID: 13257 RVA: 0x0043E1B4 File Offset: 0x0043E1B4
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyInkPaths.FileRules);
        }
      }

      // Token: 0x17000935 RID: 2357
      // (get) Token: 0x060033CA RID: 13258 RVA: 0x0043E1C0 File Offset: 0x0043E1C0
      // (set) Token: 0x060033CB RID: 13259 RVA: 0x0043E1C8 File Offset: 0x0043E1C8
      public Func<IReadOnlyList<string>, ISet<string>> SupportingPaths { get; set; }

      // Token: 0x17000936 RID: 2358
      // (get) Token: 0x060033CC RID: 13260 RVA: 0x0043E1D4 File Offset: 0x0043E1D4
      // (set) Token: 0x060033CD RID: 13261 RVA: 0x0043E1DC File Offset: 0x0043E1DC
      public Func<string, bool> IsMeaningfulFile { get; set; }

      // Token: 0x060033CE RID: 13262 RVA: 0x0043E1E8 File Offset: 0x0043E1E8
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("FileRules");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060033CF RID: 13263 RVA: 0x0043E234 File Offset: 0x0043E234
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("SupportingPaths = ");
        builder.Append(this.SupportingPaths);
        builder.Append(", IsMeaningfulFile = ");
        builder.Append(this.IsMeaningfulFile);
        return true;
      }

      // Token: 0x060033D0 RID: 13264 RVA: 0x0043E270 File Offset: 0x0043E270
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyInkPaths.FileRules left, ClickyInkPaths.FileRules right)
      {
        return !(left == right);
      }

      // Token: 0x060033D1 RID: 13265 RVA: 0x0043E27C File Offset: 0x0043E27C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyInkPaths.FileRules left, ClickyInkPaths.FileRules right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060033D2 RID: 13266 RVA: 0x0043E290 File Offset: 0x0043E290
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Func<IReadOnlyList<string>, ISet<string>>>.Default.GetHashCode(this.<SupportingPaths>k__BackingField)) * -1521134295 + EqualityComparer<Func<string, bool>>.Default.GetHashCode(this.<IsMeaningfulFile>k__BackingField);
      }

      // Token: 0x060033D3 RID: 13267 RVA: 0x0043E2D0 File Offset: 0x0043E2D0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyInkPaths.FileRules);
      }

      // Token: 0x060033D4 RID: 13268 RVA: 0x0043E2E0 File Offset: 0x0043E2E0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyInkPaths.FileRules other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Func<IReadOnlyList<string>, ISet<string>>>.Default.Equals(this.<SupportingPaths>k__BackingField, other.<SupportingPaths>k__BackingField) && EqualityComparer<Func<string, bool>>.Default.Equals(this.<IsMeaningfulFile>k__BackingField, other.<IsMeaningfulFile>k__BackingField));
      }

      // Token: 0x060033D6 RID: 13270 RVA: 0x0043E344 File Offset: 0x0043E344
      [CompilerGenerated]
      private FileRules(ClickyInkPaths.FileRules original)
      {
        this.SupportingPaths = original.<SupportingPaths>k__BackingField;
        this.IsMeaningfulFile = original.<IsMeaningfulFile>k__BackingField;
      }

      // Token: 0x060033D7 RID: 13271 RVA: 0x0043E364 File Offset: 0x0043E364
      [CompilerGenerated]
      public void Deconstruct(out Func<IReadOnlyList<string>, ISet<string>> SupportingPaths, out Func<string, bool> IsMeaningfulFile)
      {
        SupportingPaths = this.SupportingPaths;
        IsMeaningfulFile = this.IsMeaningfulFile;
      }
    }
  }
}
