using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Connectors
{
  // Token: 0x020001DA RID: 474
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class McpToolCatalog : IEquatable<McpToolCatalog>
  {
    // Token: 0x06001B3A RID: 6970 RVA: 0x003DA7E8 File Offset: 0x003DA7E8
    public McpToolCatalog([Nullable(2)] string ServerTitle, IReadOnlyList<string> ToolNames)
    {
      this.ServerTitle = ServerTitle;
      this.ToolNames = ToolNames;
      base..ctor();
    }

    // Token: 0x170005FF RID: 1535
    // (get) Token: 0x06001B3B RID: 6971 RVA: 0x003DA800 File Offset: 0x003DA800
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(McpToolCatalog);
      }
    }

    // Token: 0x17000600 RID: 1536
    // (get) Token: 0x06001B3C RID: 6972 RVA: 0x003DA80C File Offset: 0x003DA80C
    // (set) Token: 0x06001B3D RID: 6973 RVA: 0x003DA814 File Offset: 0x003DA814
    [Nullable(2)]
    public string ServerTitle
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000601 RID: 1537
    // (get) Token: 0x06001B3E RID: 6974 RVA: 0x003DA820 File Offset: 0x003DA820
    // (set) Token: 0x06001B3F RID: 6975 RVA: 0x003DA828 File Offset: 0x003DA828
    public IReadOnlyList<string> ToolNames { get; set; }

    // Token: 0x06001B40 RID: 6976 RVA: 0x003DA834 File Offset: 0x003DA834
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("McpToolCatalog");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001B41 RID: 6977 RVA: 0x003DA880 File Offset: 0x003DA880
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ServerTitle = ");
      builder.Append(this.ServerTitle);
      builder.Append(", ToolNames = ");
      builder.Append(this.ToolNames);
      return true;
    }

    // Token: 0x06001B42 RID: 6978 RVA: 0x003DA8BC File Offset: 0x003DA8BC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(McpToolCatalog left, McpToolCatalog right)
    {
      return !(left == right);
    }

    // Token: 0x06001B43 RID: 6979 RVA: 0x003DA8C8 File Offset: 0x003DA8C8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(McpToolCatalog left, McpToolCatalog right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001B44 RID: 6980 RVA: 0x003DA8DC File Offset: 0x003DA8DC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ServerTitle>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<ToolNames>k__BackingField);
    }

    // Token: 0x06001B45 RID: 6981 RVA: 0x003DA91C File Offset: 0x003DA91C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as McpToolCatalog);
    }

    // Token: 0x06001B46 RID: 6982 RVA: 0x003DA92C File Offset: 0x003DA92C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(McpToolCatalog other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ServerTitle>k__BackingField, other.<ServerTitle>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<ToolNames>k__BackingField, other.<ToolNames>k__BackingField));
    }

    // Token: 0x06001B48 RID: 6984 RVA: 0x003DA990 File Offset: 0x003DA990
    [CompilerGenerated]
    private McpToolCatalog(McpToolCatalog original)
    {
      this.ServerTitle = original.<ServerTitle>k__BackingField;
      this.ToolNames = original.<ToolNames>k__BackingField;
    }

    // Token: 0x06001B49 RID: 6985 RVA: 0x003DA9B0 File Offset: 0x003DA9B0
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string ServerTitle, out IReadOnlyList<string> ToolNames)
    {
      ServerTitle = this.ServerTitle;
      ToolNames = this.ToolNames;
    }
  }
}
