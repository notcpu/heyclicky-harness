using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace HeyClicky
{
  // Token: 0x02000002 RID: 2
  [NullableContext(1)]
  [Nullable(0)]
  public static class AppPaths
  {
    // Token: 0x17000001 RID: 1
    // (get) Token: 0x06000001 RID: 1 RVA: 0x0001C1A0 File Offset: 0x0001C1A0
    public static string Root { get; } = AppPaths.overrideRoot ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HeyClicky");

    // Token: 0x17000002 RID: 2
    // (get) Token: 0x06000002 RID: 2 RVA: 0x0001C1A8 File Offset: 0x0001C1A8
    public static string DocumentsRoot { get; }

    // Token: 0x17000003 RID: 3
    // (get) Token: 0x06000003 RID: 3 RVA: 0x0001C1B0 File Offset: 0x0001C1B0
    public static bool IsOverridden
    {
      get
      {
        return AppPaths.overrideRoot != null;
      }
    }

    // Token: 0x06000004 RID: 4 RVA: 0x0001C1C0 File Offset: 0x0001C1C0
    public static string Combine(params string[] parts)
    {
      string root = AppPaths.Root;
      int num = 0;
      string[] array = new string[1 + parts.Length];
      array[num] = root;
      num++;
      ReadOnlySpan<string> readOnlySpan = new ReadOnlySpan<string>(parts);
      readOnlySpan.CopyTo(new Span<string>(array).Slice(num, readOnlySpan.Length));
      num += readOnlySpan.Length;
      return Path.Combine(new ReadOnlySpan<string>(array));
    }

    // Token: 0x06000005 RID: 5 RVA: 0x0001C228 File Offset: 0x0001C228
    [NullableContext(2)]
    private static string Clean(string value)
    {
      if (!string.IsNullOrWhiteSpace(value))
      {
        return Path.GetFullPath(value.Trim());
      }
      return null;
    }

    // Token: 0x06000006 RID: 6 RVA: 0x0001C240 File Offset: 0x0001C240
    // Note: this type is marked as 'beforefieldinit'.
    static AppPaths()
    {
      string text = AppPaths.overrideRoot;
      AppPaths.DocumentsRoot = ((text != null) ? Path.Combine(text, "Documents") : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "HeyClicky"));
    }

    // Token: 0x04000001 RID: 1
    [Nullable(2)]
    private static readonly string overrideRoot = AppPaths.Clean(Environment.GetEnvironmentVariable("HEYCLICKY_DATA_DIR"));
  }
}
