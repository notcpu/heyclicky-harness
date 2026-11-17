using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BA RID: 186
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ForegroundActivity : IEquatable<ForegroundActivity>
  {
    // Token: 0x06000C45 RID: 3141 RVA: 0x0037D000 File Offset: 0x0037D000
    public ForegroundActivity([Nullable(1)] string ProcessName, [Nullable(1)] string AppName, bool IsBrowser, string Host, string Path, string WindowTitle)
    {
      this.ProcessName = ProcessName;
      this.AppName = AppName;
      this.IsBrowser = IsBrowser;
      this.Host = Host;
      this.Path = Path;
      this.WindowTitle = WindowTitle;
      base..ctor();
    }

    // Token: 0x170002AB RID: 683
    // (get) Token: 0x06000C46 RID: 3142 RVA: 0x0037D038 File Offset: 0x0037D038
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ForegroundActivity);
      }
    }

    // Token: 0x170002AC RID: 684
    // (get) Token: 0x06000C47 RID: 3143 RVA: 0x0037D044 File Offset: 0x0037D044
    // (set) Token: 0x06000C48 RID: 3144 RVA: 0x0037D04C File Offset: 0x0037D04C
    [Nullable(1)]
    public string ProcessName
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002AD RID: 685
    // (get) Token: 0x06000C49 RID: 3145 RVA: 0x0037D058 File Offset: 0x0037D058
    // (set) Token: 0x06000C4A RID: 3146 RVA: 0x0037D060 File Offset: 0x0037D060
    [Nullable(1)]
    public string AppName
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002AE RID: 686
    // (get) Token: 0x06000C4B RID: 3147 RVA: 0x0037D06C File Offset: 0x0037D06C
    // (set) Token: 0x06000C4C RID: 3148 RVA: 0x0037D074 File Offset: 0x0037D074
    public bool IsBrowser { get; set; }

    // Token: 0x170002AF RID: 687
    // (get) Token: 0x06000C4D RID: 3149 RVA: 0x0037D080 File Offset: 0x0037D080
    // (set) Token: 0x06000C4E RID: 3150 RVA: 0x0037D088 File Offset: 0x0037D088
    public string Host { get; set; }

    // Token: 0x170002B0 RID: 688
    // (get) Token: 0x06000C4F RID: 3151 RVA: 0x0037D094 File Offset: 0x0037D094
    // (set) Token: 0x06000C50 RID: 3152 RVA: 0x0037D09C File Offset: 0x0037D09C
    public string Path { get; set; }

    // Token: 0x170002B1 RID: 689
    // (get) Token: 0x06000C51 RID: 3153 RVA: 0x0037D0A8 File Offset: 0x0037D0A8
    // (set) Token: 0x06000C52 RID: 3154 RVA: 0x0037D0B0 File Offset: 0x0037D0B0
    public string WindowTitle { get; set; }

    // Token: 0x06000C53 RID: 3155 RVA: 0x0037D0BC File Offset: 0x0037D0BC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ForegroundActivity");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000C54 RID: 3156 RVA: 0x0037D108 File Offset: 0x0037D108
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ProcessName = ");
      builder.Append(this.ProcessName);
      builder.Append(", AppName = ");
      builder.Append(this.AppName);
      builder.Append(", IsBrowser = ");
      builder.Append(this.IsBrowser.ToString());
      builder.Append(", Host = ");
      builder.Append(this.Host);
      builder.Append(", Path = ");
      builder.Append(this.Path);
      builder.Append(", WindowTitle = ");
      builder.Append(this.WindowTitle);
      return true;
    }

    // Token: 0x06000C55 RID: 3157 RVA: 0x0037D1C0 File Offset: 0x0037D1C0
    [CompilerGenerated]
    public static bool operator !=(ForegroundActivity left, ForegroundActivity right)
    {
      return !(left == right);
    }

    // Token: 0x06000C56 RID: 3158 RVA: 0x0037D1CC File Offset: 0x0037D1CC
    [CompilerGenerated]
    public static bool operator ==(ForegroundActivity left, ForegroundActivity right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000C57 RID: 3159 RVA: 0x0037D1E0 File Offset: 0x0037D1E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProcessName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppName>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsBrowser>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Host>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<WindowTitle>k__BackingField);
    }

    // Token: 0x06000C58 RID: 3160 RVA: 0x0037D288 File Offset: 0x0037D288
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ForegroundActivity);
    }

    // Token: 0x06000C59 RID: 3161 RVA: 0x0037D298 File Offset: 0x0037D298
    [CompilerGenerated]
    public bool Equals(ForegroundActivity other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ProcessName>k__BackingField, other.<ProcessName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppName>k__BackingField, other.<AppName>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsBrowser>k__BackingField, other.<IsBrowser>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Host>k__BackingField, other.<Host>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<WindowTitle>k__BackingField, other.<WindowTitle>k__BackingField));
    }

    // Token: 0x06000C5B RID: 3163 RVA: 0x0037D364 File Offset: 0x0037D364
    [CompilerGenerated]
    private ForegroundActivity([Nullable(1)] ForegroundActivity original)
    {
      this.ProcessName = original.<ProcessName>k__BackingField;
      this.AppName = original.<AppName>k__BackingField;
      this.IsBrowser = original.<IsBrowser>k__BackingField;
      this.Host = original.<Host>k__BackingField;
      this.Path = original.<Path>k__BackingField;
      this.WindowTitle = original.<WindowTitle>k__BackingField;
    }

    // Token: 0x06000C5C RID: 3164 RVA: 0x0037D3C0 File Offset: 0x0037D3C0
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string ProcessName, [Nullable(1)] out string AppName, out bool IsBrowser, out string Host, out string Path, out string WindowTitle)
    {
      ProcessName = this.ProcessName;
      AppName = this.AppName;
      IsBrowser = this.IsBrowser;
      Host = this.Host;
      Path = this.Path;
      WindowTitle = this.WindowTitle;
    }
  }
}
