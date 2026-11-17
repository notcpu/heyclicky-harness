using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200000A RID: 10
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationWorkApp : IEquatable<DictationWorkApp>
  {
    // Token: 0x0600008D RID: 141 RVA: 0x0001D8D4 File Offset: 0x0001D8D4
    public DictationWorkApp(int ProcessId, IntPtr WindowHandle, string AppName, [Nullable(2)] string SiteHost, bool AllowsBlindPaste)
    {
      this.ProcessId = ProcessId;
      this.WindowHandle = WindowHandle;
      this.AppName = AppName;
      this.SiteHost = SiteHost;
      this.AllowsBlindPaste = AllowsBlindPaste;
      base..ctor();
    }

    // Token: 0x1700002B RID: 43
    // (get) Token: 0x0600008E RID: 142 RVA: 0x0001D904 File Offset: 0x0001D904
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationWorkApp);
      }
    }

    // Token: 0x1700002C RID: 44
    // (get) Token: 0x0600008F RID: 143 RVA: 0x0001D910 File Offset: 0x0001D910
    // (set) Token: 0x06000090 RID: 144 RVA: 0x0001D918 File Offset: 0x0001D918
    public int ProcessId { get; set; }

    // Token: 0x1700002D RID: 45
    // (get) Token: 0x06000091 RID: 145 RVA: 0x0001D924 File Offset: 0x0001D924
    // (set) Token: 0x06000092 RID: 146 RVA: 0x0001D92C File Offset: 0x0001D92C
    public IntPtr WindowHandle { get; set; }

    // Token: 0x1700002E RID: 46
    // (get) Token: 0x06000093 RID: 147 RVA: 0x0001D938 File Offset: 0x0001D938
    // (set) Token: 0x06000094 RID: 148 RVA: 0x0001D940 File Offset: 0x0001D940
    public string AppName { get; set; }

    // Token: 0x1700002F RID: 47
    // (get) Token: 0x06000095 RID: 149 RVA: 0x0001D94C File Offset: 0x0001D94C
    // (set) Token: 0x06000096 RID: 150 RVA: 0x0001D954 File Offset: 0x0001D954
    [Nullable(2)]
    public string SiteHost
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000030 RID: 48
    // (get) Token: 0x06000097 RID: 151 RVA: 0x0001D960 File Offset: 0x0001D960
    // (set) Token: 0x06000098 RID: 152 RVA: 0x0001D968 File Offset: 0x0001D968
    public bool AllowsBlindPaste { get; set; }

    // Token: 0x06000099 RID: 153 RVA: 0x0001D974 File Offset: 0x0001D974
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationWorkApp");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600009A RID: 154 RVA: 0x0001D9C0 File Offset: 0x0001D9C0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ProcessId = ");
      builder.Append(this.ProcessId.ToString());
      builder.Append(", WindowHandle = ");
      builder.Append(this.WindowHandle.ToString());
      builder.Append(", AppName = ");
      builder.Append(this.AppName);
      builder.Append(", SiteHost = ");
      builder.Append(this.SiteHost);
      builder.Append(", AllowsBlindPaste = ");
      builder.Append(this.AllowsBlindPaste.ToString());
      return true;
    }

    // Token: 0x0600009B RID: 155 RVA: 0x0001DA7C File Offset: 0x0001DA7C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationWorkApp left, DictationWorkApp right)
    {
      return !(left == right);
    }

    // Token: 0x0600009C RID: 156 RVA: 0x0001DA88 File Offset: 0x0001DA88
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationWorkApp left, DictationWorkApp right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600009D RID: 157 RVA: 0x0001DA9C File Offset: 0x0001DA9C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ProcessId>k__BackingField)) * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(this.<WindowHandle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SiteHost>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AllowsBlindPaste>k__BackingField);
    }

    // Token: 0x0600009E RID: 158 RVA: 0x0001DB2C File Offset: 0x0001DB2C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationWorkApp);
    }

    // Token: 0x0600009F RID: 159 RVA: 0x0001DB3C File Offset: 0x0001DB3C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationWorkApp other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<int>.Default.Equals(this.<ProcessId>k__BackingField, other.<ProcessId>k__BackingField) && EqualityComparer<IntPtr>.Default.Equals(this.<WindowHandle>k__BackingField, other.<WindowHandle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppName>k__BackingField, other.<AppName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SiteHost>k__BackingField, other.<SiteHost>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AllowsBlindPaste>k__BackingField, other.<AllowsBlindPaste>k__BackingField));
    }

    // Token: 0x060000A1 RID: 161 RVA: 0x0001DBEC File Offset: 0x0001DBEC
    [CompilerGenerated]
    private DictationWorkApp(DictationWorkApp original)
    {
      this.ProcessId = original.<ProcessId>k__BackingField;
      this.WindowHandle = original.<WindowHandle>k__BackingField;
      this.AppName = original.<AppName>k__BackingField;
      this.SiteHost = original.<SiteHost>k__BackingField;
      this.AllowsBlindPaste = original.<AllowsBlindPaste>k__BackingField;
    }

    // Token: 0x060000A2 RID: 162 RVA: 0x0001DC3C File Offset: 0x0001DC3C
    [CompilerGenerated]
    public void Deconstruct(out int ProcessId, out IntPtr WindowHandle, out string AppName, [Nullable(2)] out string SiteHost, out bool AllowsBlindPaste)
    {
      ProcessId = this.ProcessId;
      WindowHandle = this.WindowHandle;
      AppName = this.AppName;
      SiteHost = this.SiteHost;
      AllowsBlindPaste = this.AllowsBlindPaste;
    }
  }
}
