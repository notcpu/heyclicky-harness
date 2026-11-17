using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BB RID: 187
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntegrationCandidate : IEquatable<IntegrationCandidate>
  {
    // Token: 0x06000C5D RID: 3165 RVA: 0x0037D3F8 File Offset: 0x0037D3F8
    public IntegrationCandidate(string ToolkitSlug, string DisplayName, string SourceApplicationName, [Nullable(2)] string SourceHost)
    {
      this.ToolkitSlug = ToolkitSlug;
      this.DisplayName = DisplayName;
      this.SourceApplicationName = SourceApplicationName;
      this.SourceHost = SourceHost;
      base..ctor();
    }

    // Token: 0x170002B2 RID: 690
    // (get) Token: 0x06000C5E RID: 3166 RVA: 0x0037D420 File Offset: 0x0037D420
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(IntegrationCandidate);
      }
    }

    // Token: 0x170002B3 RID: 691
    // (get) Token: 0x06000C5F RID: 3167 RVA: 0x0037D42C File Offset: 0x0037D42C
    // (set) Token: 0x06000C60 RID: 3168 RVA: 0x0037D434 File Offset: 0x0037D434
    public string ToolkitSlug { get; set; }

    // Token: 0x170002B4 RID: 692
    // (get) Token: 0x06000C61 RID: 3169 RVA: 0x0037D440 File Offset: 0x0037D440
    // (set) Token: 0x06000C62 RID: 3170 RVA: 0x0037D448 File Offset: 0x0037D448
    public string DisplayName { get; set; }

    // Token: 0x170002B5 RID: 693
    // (get) Token: 0x06000C63 RID: 3171 RVA: 0x0037D454 File Offset: 0x0037D454
    // (set) Token: 0x06000C64 RID: 3172 RVA: 0x0037D45C File Offset: 0x0037D45C
    public string SourceApplicationName { get; set; }

    // Token: 0x170002B6 RID: 694
    // (get) Token: 0x06000C65 RID: 3173 RVA: 0x0037D468 File Offset: 0x0037D468
    // (set) Token: 0x06000C66 RID: 3174 RVA: 0x0037D470 File Offset: 0x0037D470
    [Nullable(2)]
    public string SourceHost
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000C67 RID: 3175 RVA: 0x0037D47C File Offset: 0x0037D47C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("IntegrationCandidate");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000C68 RID: 3176 RVA: 0x0037D4C8 File Offset: 0x0037D4C8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ToolkitSlug = ");
      builder.Append(this.ToolkitSlug);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", SourceApplicationName = ");
      builder.Append(this.SourceApplicationName);
      builder.Append(", SourceHost = ");
      builder.Append(this.SourceHost);
      return true;
    }

    // Token: 0x06000C69 RID: 3177 RVA: 0x0037D540 File Offset: 0x0037D540
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(IntegrationCandidate left, IntegrationCandidate right)
    {
      return !(left == right);
    }

    // Token: 0x06000C6A RID: 3178 RVA: 0x0037D54C File Offset: 0x0037D54C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(IntegrationCandidate left, IntegrationCandidate right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000C6B RID: 3179 RVA: 0x0037D560 File Offset: 0x0037D560
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ToolkitSlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceApplicationName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SourceHost>k__BackingField);
    }

    // Token: 0x06000C6C RID: 3180 RVA: 0x0037D5DC File Offset: 0x0037D5DC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as IntegrationCandidate);
    }

    // Token: 0x06000C6D RID: 3181 RVA: 0x0037D5EC File Offset: 0x0037D5EC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(IntegrationCandidate other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ToolkitSlug>k__BackingField, other.<ToolkitSlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceApplicationName>k__BackingField, other.<SourceApplicationName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<SourceHost>k__BackingField, other.<SourceHost>k__BackingField));
    }

    // Token: 0x06000C6F RID: 3183 RVA: 0x0037D680 File Offset: 0x0037D680
    [CompilerGenerated]
    private IntegrationCandidate(IntegrationCandidate original)
    {
      this.ToolkitSlug = original.<ToolkitSlug>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.SourceApplicationName = original.<SourceApplicationName>k__BackingField;
      this.SourceHost = original.<SourceHost>k__BackingField;
    }

    // Token: 0x06000C70 RID: 3184 RVA: 0x0037D6B8 File Offset: 0x0037D6B8
    [CompilerGenerated]
    public void Deconstruct(out string ToolkitSlug, out string DisplayName, out string SourceApplicationName, [Nullable(2)] out string SourceHost)
    {
      ToolkitSlug = this.ToolkitSlug;
      DisplayName = this.DisplayName;
      SourceApplicationName = this.SourceApplicationName;
      SourceHost = this.SourceHost;
    }
  }
}
