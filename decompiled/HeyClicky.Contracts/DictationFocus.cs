using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000009 RID: 9
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationFocus : IEquatable<DictationFocus>
  {
    // Token: 0x0600006A RID: 106 RVA: 0x0001D1FC File Offset: 0x0001D1FC
    public DictationFocus(object Anchor, int ProcessId, IntPtr WindowHandle, string AppName, [Nullable(2)] string SiteHost, [Nullable(2)] string Role, bool IsSecure, bool IsSingleLine, string BeforeText, string SelectedText, string AfterText)
    {
      this.Anchor = Anchor;
      this.ProcessId = ProcessId;
      this.WindowHandle = WindowHandle;
      this.AppName = AppName;
      this.SiteHost = SiteHost;
      this.Role = Role;
      this.IsSecure = IsSecure;
      this.IsSingleLine = IsSingleLine;
      this.BeforeText = BeforeText;
      this.SelectedText = SelectedText;
      this.AfterText = AfterText;
      base..ctor();
    }

    // Token: 0x1700001E RID: 30
    // (get) Token: 0x0600006B RID: 107 RVA: 0x0001D264 File Offset: 0x0001D264
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationFocus);
      }
    }

    // Token: 0x1700001F RID: 31
    // (get) Token: 0x0600006C RID: 108 RVA: 0x0001D270 File Offset: 0x0001D270
    // (set) Token: 0x0600006D RID: 109 RVA: 0x0001D278 File Offset: 0x0001D278
    public object Anchor { get; set; }

    // Token: 0x17000020 RID: 32
    // (get) Token: 0x0600006E RID: 110 RVA: 0x0001D284 File Offset: 0x0001D284
    // (set) Token: 0x0600006F RID: 111 RVA: 0x0001D28C File Offset: 0x0001D28C
    public int ProcessId { get; set; }

    // Token: 0x17000021 RID: 33
    // (get) Token: 0x06000070 RID: 112 RVA: 0x0001D298 File Offset: 0x0001D298
    // (set) Token: 0x06000071 RID: 113 RVA: 0x0001D2A0 File Offset: 0x0001D2A0
    public IntPtr WindowHandle { get; set; }

    // Token: 0x17000022 RID: 34
    // (get) Token: 0x06000072 RID: 114 RVA: 0x0001D2AC File Offset: 0x0001D2AC
    // (set) Token: 0x06000073 RID: 115 RVA: 0x0001D2B4 File Offset: 0x0001D2B4
    public string AppName { get; set; }

    // Token: 0x17000023 RID: 35
    // (get) Token: 0x06000074 RID: 116 RVA: 0x0001D2C0 File Offset: 0x0001D2C0
    // (set) Token: 0x06000075 RID: 117 RVA: 0x0001D2C8 File Offset: 0x0001D2C8
    [Nullable(2)]
    public string SiteHost
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000024 RID: 36
    // (get) Token: 0x06000076 RID: 118 RVA: 0x0001D2D4 File Offset: 0x0001D2D4
    // (set) Token: 0x06000077 RID: 119 RVA: 0x0001D2DC File Offset: 0x0001D2DC
    [Nullable(2)]
    public string Role
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000025 RID: 37
    // (get) Token: 0x06000078 RID: 120 RVA: 0x0001D2E8 File Offset: 0x0001D2E8
    // (set) Token: 0x06000079 RID: 121 RVA: 0x0001D2F0 File Offset: 0x0001D2F0
    public bool IsSecure { get; set; }

    // Token: 0x17000026 RID: 38
    // (get) Token: 0x0600007A RID: 122 RVA: 0x0001D2FC File Offset: 0x0001D2FC
    // (set) Token: 0x0600007B RID: 123 RVA: 0x0001D304 File Offset: 0x0001D304
    public bool IsSingleLine { get; set; }

    // Token: 0x17000027 RID: 39
    // (get) Token: 0x0600007C RID: 124 RVA: 0x0001D310 File Offset: 0x0001D310
    // (set) Token: 0x0600007D RID: 125 RVA: 0x0001D318 File Offset: 0x0001D318
    public string BeforeText { get; set; }

    // Token: 0x17000028 RID: 40
    // (get) Token: 0x0600007E RID: 126 RVA: 0x0001D324 File Offset: 0x0001D324
    // (set) Token: 0x0600007F RID: 127 RVA: 0x0001D32C File Offset: 0x0001D32C
    public string SelectedText { get; set; }

    // Token: 0x17000029 RID: 41
    // (get) Token: 0x06000080 RID: 128 RVA: 0x0001D338 File Offset: 0x0001D338
    // (set) Token: 0x06000081 RID: 129 RVA: 0x0001D340 File Offset: 0x0001D340
    public string AfterText { get; set; }

    // Token: 0x1700002A RID: 42
    // (get) Token: 0x06000082 RID: 130 RVA: 0x0001D34C File Offset: 0x0001D34C
    public bool IsEditableInsertionTarget
    {
      get
      {
        return !this.IsSecure;
      }
    }

    // Token: 0x06000083 RID: 131 RVA: 0x0001D358 File Offset: 0x0001D358
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationFocus");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000084 RID: 132 RVA: 0x0001D3A4 File Offset: 0x0001D3A4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Anchor = ");
      builder.Append(this.Anchor);
      builder.Append(", ProcessId = ");
      builder.Append(this.ProcessId.ToString());
      builder.Append(", WindowHandle = ");
      builder.Append(this.WindowHandle.ToString());
      builder.Append(", AppName = ");
      builder.Append(this.AppName);
      builder.Append(", SiteHost = ");
      builder.Append(this.SiteHost);
      builder.Append(", Role = ");
      builder.Append(this.Role);
      builder.Append(", IsSecure = ");
      builder.Append(this.IsSecure.ToString());
      builder.Append(", IsSingleLine = ");
      builder.Append(this.IsSingleLine.ToString());
      builder.Append(", BeforeText = ");
      builder.Append(this.BeforeText);
      builder.Append(", SelectedText = ");
      builder.Append(this.SelectedText);
      builder.Append(", AfterText = ");
      builder.Append(this.AfterText);
      builder.Append(", IsEditableInsertionTarget = ");
      builder.Append(this.IsEditableInsertionTarget.ToString());
      return true;
    }

    // Token: 0x06000085 RID: 133 RVA: 0x0001D52C File Offset: 0x0001D52C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationFocus left, DictationFocus right)
    {
      return !(left == right);
    }

    // Token: 0x06000086 RID: 134 RVA: 0x0001D538 File Offset: 0x0001D538
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationFocus left, DictationFocus right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000087 RID: 135 RVA: 0x0001D54C File Offset: 0x0001D54C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<object>.Default.GetHashCode(this.<Anchor>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ProcessId>k__BackingField)) * -1521134295 + EqualityComparer<IntPtr>.Default.GetHashCode(this.<WindowHandle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SiteHost>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Role>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSecure>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsSingleLine>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BeforeText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SelectedText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AfterText>k__BackingField);
    }

    // Token: 0x06000088 RID: 136 RVA: 0x0001D668 File Offset: 0x0001D668
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationFocus);
    }

    // Token: 0x06000089 RID: 137 RVA: 0x0001D678 File Offset: 0x0001D678
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationFocus other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<object>.Default.Equals(this.<Anchor>k__BackingField, other.<Anchor>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ProcessId>k__BackingField, other.<ProcessId>k__BackingField) && EqualityComparer<IntPtr>.Default.Equals(this.<WindowHandle>k__BackingField, other.<WindowHandle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppName>k__BackingField, other.<AppName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SiteHost>k__BackingField, other.<SiteHost>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Role>k__BackingField, other.<Role>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSecure>k__BackingField, other.<IsSecure>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsSingleLine>k__BackingField, other.<IsSingleLine>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BeforeText>k__BackingField, other.<BeforeText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SelectedText>k__BackingField, other.<SelectedText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AfterText>k__BackingField, other.<AfterText>k__BackingField));
    }

    // Token: 0x0600008B RID: 139 RVA: 0x0001D7CC File Offset: 0x0001D7CC
    [CompilerGenerated]
    private DictationFocus(DictationFocus original)
    {
      this.Anchor = original.<Anchor>k__BackingField;
      this.ProcessId = original.<ProcessId>k__BackingField;
      this.WindowHandle = original.<WindowHandle>k__BackingField;
      this.AppName = original.<AppName>k__BackingField;
      this.SiteHost = original.<SiteHost>k__BackingField;
      this.Role = original.<Role>k__BackingField;
      this.IsSecure = original.<IsSecure>k__BackingField;
      this.IsSingleLine = original.<IsSingleLine>k__BackingField;
      this.BeforeText = original.<BeforeText>k__BackingField;
      this.SelectedText = original.<SelectedText>k__BackingField;
      this.AfterText = original.<AfterText>k__BackingField;
    }

    // Token: 0x0600008C RID: 140 RVA: 0x0001D864 File Offset: 0x0001D864
    [CompilerGenerated]
    public void Deconstruct(out object Anchor, out int ProcessId, out IntPtr WindowHandle, out string AppName, [Nullable(2)] out string SiteHost, [Nullable(2)] out string Role, out bool IsSecure, out bool IsSingleLine, out string BeforeText, out string SelectedText, out string AfterText)
    {
      Anchor = this.Anchor;
      ProcessId = this.ProcessId;
      WindowHandle = this.WindowHandle;
      AppName = this.AppName;
      SiteHost = this.SiteHost;
      Role = this.Role;
      IsSecure = this.IsSecure;
      IsSingleLine = this.IsSingleLine;
      BeforeText = this.BeforeText;
      SelectedText = this.SelectedText;
      AfterText = this.AfterText;
    }
  }
}
