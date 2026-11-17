using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000037 RID: 55
  [NullableContext(1)]
  [Nullable(0)]
  public static class ShellOpenRules
  {
    // Token: 0x060002C8 RID: 712 RVA: 0x00021DE0 File Offset: 0x00021DE0
    public static bool IsWebUrl(string target)
    {
      Uri uri;
      return Uri.TryCreate(target, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp);
    }

    // Token: 0x060002C9 RID: 713 RVA: 0x00021E20 File Offset: 0x00021E20
    public static bool LooksLikeUri(string target)
    {
      int num = target.IndexOf(':');
      if (num < 2)
      {
        return false;
      }
      for (int i = 0; i < num; i++)
      {
        char c = target[i];
        bool flag = char.IsAsciiLetterOrDigit(c);
        if (!flag)
        {
          bool flag2;
          switch (c)
          {
          case '+':
          case '-':
          case '.':
            flag2 = true;
            break;
          case ',':
            goto IL_0045;
          default:
            goto IL_0045;
          }
          IL_0048:
          flag = flag2;
          goto IL_004B;
          IL_0045:
          flag2 = false;
          goto IL_0048;
        }
        IL_004B:
        if (!flag)
        {
          return false;
        }
      }
      return char.IsAsciiLetter(target[0]) && !target.StartsWith("file:", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x060002CA RID: 714 RVA: 0x00021EA4 File Offset: 0x00021EA4
    [return: TupleElementNames(new string[] { "Key", "IsProtocol" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public static ValueTuple<string, bool> AssociationKey(string target, bool isDirectory)
    {
      if (string.IsNullOrWhiteSpace(target))
      {
        return new ValueTuple<string, bool>(null, false);
      }
      if (isDirectory)
      {
        return new ValueTuple<string, bool>("Folder", false);
      }
      Uri uri;
      if (target.StartsWith("file:", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(target, UriKind.Absolute, out uri) && uri.IsFile)
      {
        return ShellOpenRules.AssociationKey(uri.LocalPath, false);
      }
      if (ShellOpenRules.LooksLikeUri(target))
      {
        return new ValueTuple<string, bool>(target.Substring(0, target.IndexOf(':')).ToLowerInvariant(), true);
      }
      string extension = Path.GetExtension(target.TrimEnd(new char[] { '\\', '/' }));
      if (!string.IsNullOrEmpty(extension))
      {
        return new ValueTuple<string, bool>(extension.ToLowerInvariant(), false);
      }
      return new ValueTuple<string, bool>(null, false);
    }

    // Token: 0x060002CB RID: 715 RVA: 0x00021F5C File Offset: 0x00021F5C
    public static bool TakesUserDataDir(string exeName)
    {
      return exeName.Equals("chrome.exe", StringComparison.OrdinalIgnoreCase) || exeName.Equals("msedge.exe", StringComparison.OrdinalIgnoreCase) || exeName.Equals("brave.exe", StringComparison.OrdinalIgnoreCase) || exeName.Equals("chromium.exe", StringComparison.OrdinalIgnoreCase);
    }

    // Token: 0x060002CC RID: 716 RVA: 0x00021F98 File Offset: 0x00021F98
    public static ShellOpenRules.TopWindow? PickOpenedWindow(IReadOnlyList<ShellOpenRules.TopWindow> before, IReadOnlyList<ShellOpenRules.TopWindow> now, [Nullable(2)] string handlerExeName, int ownProcessId, bool final)
    {
      Dictionary<IntPtr, string> dictionary = new Dictionary<IntPtr, string>();
      foreach (ShellOpenRules.TopWindow topWindow in before)
      {
        dictionary[topWindow.Handle] = topWindow.Title;
      }
      bool known = !string.IsNullOrEmpty(handlerExeName);
      List<ShellOpenRules.TopWindow> list = now.Where<ShellOpenRules.TopWindow>((ShellOpenRules.TopWindow w) => w.ProcessId != ownProcessId && (!known || string.Equals(w.ExeName, handlerExeName, StringComparison.OrdinalIgnoreCase))).ToList<ShellOpenRules.TopWindow>();
      foreach (ShellOpenRules.TopWindow topWindow2 in list)
      {
        if (!dictionary.ContainsKey(topWindow2.Handle))
        {
          return new ShellOpenRules.TopWindow?(topWindow2);
        }
      }
      if (!known)
      {
        return null;
      }
      foreach (ShellOpenRules.TopWindow topWindow3 in list)
      {
        string text;
        if (dictionary.TryGetValue(topWindow3.Handle, out text) && text != topWindow3.Title)
        {
          return new ShellOpenRules.TopWindow?(topWindow3);
        }
      }
      if (final)
      {
        ShellOpenRules.TopWindow topWindow4 = list.FirstOrDefault<ShellOpenRules.TopWindow>((ShellOpenRules.TopWindow w) => !w.Minimized);
        if (topWindow4.Handle != 0)
        {
          return new ShellOpenRules.TopWindow?(topWindow4);
        }
      }
      return null;
    }

    // Token: 0x040000CF RID: 207
    public const string FolderKey = "Folder";

    // Token: 0x02000066 RID: 102
    [Nullable(0)]
    public readonly struct TopWindow : IEquatable<ShellOpenRules.TopWindow>
    {
      // Token: 0x060004EA RID: 1258 RVA: 0x00025CD8 File Offset: 0x00025CD8
      public TopWindow(IntPtr Handle, int ProcessId, string ExeName, string Title, bool Minimized = false)
      {
        this.Handle = Handle;
        this.ProcessId = ProcessId;
        this.ExeName = ExeName;
        this.Title = Title;
        this.Minimized = Minimized;
      }

      // Token: 0x170000FF RID: 255
      // (get) Token: 0x060004EB RID: 1259 RVA: 0x00025D00 File Offset: 0x00025D00
      // (set) Token: 0x060004EC RID: 1260 RVA: 0x00025D08 File Offset: 0x00025D08
      public IntPtr Handle { get; set; }

      // Token: 0x17000100 RID: 256
      // (get) Token: 0x060004ED RID: 1261 RVA: 0x00025D14 File Offset: 0x00025D14
      // (set) Token: 0x060004EE RID: 1262 RVA: 0x00025D1C File Offset: 0x00025D1C
      public int ProcessId { get; set; }

      // Token: 0x17000101 RID: 257
      // (get) Token: 0x060004EF RID: 1263 RVA: 0x00025D28 File Offset: 0x00025D28
      // (set) Token: 0x060004F0 RID: 1264 RVA: 0x00025D30 File Offset: 0x00025D30
      public string ExeName { get; set; }

      // Token: 0x17000102 RID: 258
      // (get) Token: 0x060004F1 RID: 1265 RVA: 0x00025D3C File Offset: 0x00025D3C
      // (set) Token: 0x060004F2 RID: 1266 RVA: 0x00025D44 File Offset: 0x00025D44
      public string Title { get; set; }

      // Token: 0x17000103 RID: 259
      // (get) Token: 0x060004F3 RID: 1267 RVA: 0x00025D50 File Offset: 0x00025D50
      // (set) Token: 0x060004F4 RID: 1268 RVA: 0x00025D58 File Offset: 0x00025D58
      public bool Minimized { get; set; }

      // Token: 0x060004F5 RID: 1269 RVA: 0x00025D64 File Offset: 0x00025D64
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("TopWindow");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060004F6 RID: 1270 RVA: 0x00025DB0 File Offset: 0x00025DB0
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Handle = ");
        builder.Append(this.Handle.ToString());
        builder.Append(", ProcessId = ");
        builder.Append(this.ProcessId.ToString());
        builder.Append(", ExeName = ");
        builder.Append(this.ExeName);
        builder.Append(", Title = ");
        builder.Append(this.Title);
        builder.Append(", Minimized = ");
        builder.Append(this.Minimized.ToString());
        return true;
      }

      // Token: 0x060004F7 RID: 1271 RVA: 0x00025E68 File Offset: 0x00025E68
      [CompilerGenerated]
      public static bool operator !=(ShellOpenRules.TopWindow left, ShellOpenRules.TopWindow right)
      {
        return !(left == right);
      }

      // Token: 0x060004F8 RID: 1272 RVA: 0x00025E74 File Offset: 0x00025E74
      [CompilerGenerated]
      public static bool operator ==(ShellOpenRules.TopWindow left, ShellOpenRules.TopWindow right)
      {
        return left.Equals(right);
      }

      // Token: 0x060004F9 RID: 1273 RVA: 0x00025E80 File Offset: 0x00025E80
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<IntPtr>.Default.GetHashCode(this.<Handle>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ProcessId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ExeName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Minimized>k__BackingField);
      }

      // Token: 0x060004FA RID: 1274 RVA: 0x00025EFC File Offset: 0x00025EFC
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ShellOpenRules.TopWindow && this.Equals((ShellOpenRules.TopWindow)obj);
      }

      // Token: 0x060004FB RID: 1275 RVA: 0x00025F14 File Offset: 0x00025F14
      [CompilerGenerated]
      public bool Equals(ShellOpenRules.TopWindow other)
      {
        return EqualityComparer<IntPtr>.Default.Equals(this.<Handle>k__BackingField, other.<Handle>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ProcessId>k__BackingField, other.<ProcessId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ExeName>k__BackingField, other.<ExeName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Minimized>k__BackingField, other.<Minimized>k__BackingField);
      }

      // Token: 0x060004FC RID: 1276 RVA: 0x00025F9C File Offset: 0x00025F9C
      [CompilerGenerated]
      public void Deconstruct(out IntPtr Handle, out int ProcessId, out string ExeName, out string Title, out bool Minimized)
      {
        Handle = this.Handle;
        ProcessId = this.ProcessId;
        ExeName = this.ExeName;
        Title = this.Title;
        Minimized = this.Minimized;
      }
    }
  }
}
