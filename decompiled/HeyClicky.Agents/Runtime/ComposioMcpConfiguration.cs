using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000020 RID: 32
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ComposioMcpConfiguration : IEquatable<ComposioMcpConfiguration>
  {
    // Token: 0x060003B3 RID: 947 RVA: 0x000E68AC File Offset: 0x000E68AC
    public ComposioMcpConfiguration(string Url, IReadOnlyList<string> Toolkits, IReadOnlyList<string> MultiAccountLines)
    {
      this.Url = Url;
      this.Toolkits = Toolkits;
      this.MultiAccountLines = MultiAccountLines;
      base..ctor();
    }

    // Token: 0x170000FF RID: 255
    // (get) Token: 0x060003B4 RID: 948 RVA: 0x000E68CC File Offset: 0x000E68CC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ComposioMcpConfiguration);
      }
    }

    // Token: 0x17000100 RID: 256
    // (get) Token: 0x060003B5 RID: 949 RVA: 0x000E68D8 File Offset: 0x000E68D8
    // (set) Token: 0x060003B6 RID: 950 RVA: 0x000E68E0 File Offset: 0x000E68E0
    public string Url { get; set; }

    // Token: 0x17000101 RID: 257
    // (get) Token: 0x060003B7 RID: 951 RVA: 0x000E68EC File Offset: 0x000E68EC
    // (set) Token: 0x060003B8 RID: 952 RVA: 0x000E68F4 File Offset: 0x000E68F4
    public IReadOnlyList<string> Toolkits { get; set; }

    // Token: 0x17000102 RID: 258
    // (get) Token: 0x060003B9 RID: 953 RVA: 0x000E6900 File Offset: 0x000E6900
    // (set) Token: 0x060003BA RID: 954 RVA: 0x000E6908 File Offset: 0x000E6908
    public IReadOnlyList<string> MultiAccountLines { get; set; }

    // Token: 0x060003BB RID: 955 RVA: 0x000E6914 File Offset: 0x000E6914
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ComposioMcpConfiguration");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003BC RID: 956 RVA: 0x000E6960 File Offset: 0x000E6960
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Url = ");
      builder.Append(this.Url);
      builder.Append(", Toolkits = ");
      builder.Append(this.Toolkits);
      builder.Append(", MultiAccountLines = ");
      builder.Append(this.MultiAccountLines);
      return true;
    }

    // Token: 0x060003BD RID: 957 RVA: 0x000E69C0 File Offset: 0x000E69C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ComposioMcpConfiguration left, ComposioMcpConfiguration right)
    {
      return !(left == right);
    }

    // Token: 0x060003BE RID: 958 RVA: 0x000E69CC File Offset: 0x000E69CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ComposioMcpConfiguration left, ComposioMcpConfiguration right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003BF RID: 959 RVA: 0x000E69E0 File Offset: 0x000E69E0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Toolkits>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<MultiAccountLines>k__BackingField);
    }

    // Token: 0x060003C0 RID: 960 RVA: 0x000E6A44 File Offset: 0x000E6A44
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ComposioMcpConfiguration);
    }

    // Token: 0x060003C1 RID: 961 RVA: 0x000E6A54 File Offset: 0x000E6A54
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ComposioMcpConfiguration other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Toolkits>k__BackingField, other.<Toolkits>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<MultiAccountLines>k__BackingField, other.<MultiAccountLines>k__BackingField));
    }

    // Token: 0x060003C3 RID: 963 RVA: 0x000E6AD0 File Offset: 0x000E6AD0
    [CompilerGenerated]
    private ComposioMcpConfiguration(ComposioMcpConfiguration original)
    {
      this.Url = original.<Url>k__BackingField;
      this.Toolkits = original.<Toolkits>k__BackingField;
      this.MultiAccountLines = original.<MultiAccountLines>k__BackingField;
    }

    // Token: 0x060003C4 RID: 964 RVA: 0x000E6AFC File Offset: 0x000E6AFC
    [CompilerGenerated]
    public void Deconstruct(out string Url, out IReadOnlyList<string> Toolkits, out IReadOnlyList<string> MultiAccountLines)
    {
      Url = this.Url;
      Toolkits = this.Toolkits;
      MultiAccountLines = this.MultiAccountLines;
    }
  }
}
