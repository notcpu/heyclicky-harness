using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000061 RID: 97
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentHarnessMcpServer : IEquatable<AgentHarnessMcpServer>
  {
    // Token: 0x060007E8 RID: 2024 RVA: 0x000FEF38 File Offset: 0x000FEF38
    public AgentHarnessMcpServer(string Name, string Url, [Nullable(2)] string BearerToken)
    {
      this.Name = Name;
      this.Url = Url;
      this.BearerToken = BearerToken;
      this.Headers = new Dictionary<string, string>();
      base..ctor();
    }

    // Token: 0x170001FE RID: 510
    // (get) Token: 0x060007E9 RID: 2025 RVA: 0x000FEF60 File Offset: 0x000FEF60
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentHarnessMcpServer);
      }
    }

    // Token: 0x170001FF RID: 511
    // (get) Token: 0x060007EA RID: 2026 RVA: 0x000FEF6C File Offset: 0x000FEF6C
    // (set) Token: 0x060007EB RID: 2027 RVA: 0x000FEF74 File Offset: 0x000FEF74
    public string Name { get; set; }

    // Token: 0x17000200 RID: 512
    // (get) Token: 0x060007EC RID: 2028 RVA: 0x000FEF80 File Offset: 0x000FEF80
    // (set) Token: 0x060007ED RID: 2029 RVA: 0x000FEF88 File Offset: 0x000FEF88
    public string Url { get; set; }

    // Token: 0x17000201 RID: 513
    // (get) Token: 0x060007EE RID: 2030 RVA: 0x000FEF94 File Offset: 0x000FEF94
    // (set) Token: 0x060007EF RID: 2031 RVA: 0x000FEF9C File Offset: 0x000FEF9C
    [Nullable(2)]
    public string BearerToken
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000202 RID: 514
    // (get) Token: 0x060007F0 RID: 2032 RVA: 0x000FEFA8 File Offset: 0x000FEFA8
    // (set) Token: 0x060007F1 RID: 2033 RVA: 0x000FEFB0 File Offset: 0x000FEFB0
    public IReadOnlyDictionary<string, string> Headers { get; set; }

    // Token: 0x17000203 RID: 515
    // (get) Token: 0x060007F2 RID: 2034 RVA: 0x000FEFBC File Offset: 0x000FEFBC
    // (set) Token: 0x060007F3 RID: 2035 RVA: 0x000FEFC4 File Offset: 0x000FEFC4
    public bool SendsTurnIdentity { get; set; }

    // Token: 0x17000204 RID: 516
    // (get) Token: 0x060007F4 RID: 2036 RVA: 0x000FEFD0 File Offset: 0x000FEFD0
    // (set) Token: 0x060007F5 RID: 2037 RVA: 0x000FEFD8 File Offset: 0x000FEFD8
    [Nullable(2)]
    public AgentHarnessLocalMcpCommand LocalCommand
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060007F6 RID: 2038 RVA: 0x000FEFE4 File Offset: 0x000FEFE4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentHarnessMcpServer");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060007F7 RID: 2039 RVA: 0x000FF030 File Offset: 0x000FF030
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Name = ");
      builder.Append(this.Name);
      builder.Append(", Url = ");
      builder.Append(this.Url);
      builder.Append(", BearerToken = ");
      builder.Append(this.BearerToken);
      builder.Append(", Headers = ");
      builder.Append(this.Headers);
      builder.Append(", SendsTurnIdentity = ");
      builder.Append(this.SendsTurnIdentity.ToString());
      builder.Append(", LocalCommand = ");
      builder.Append(this.LocalCommand);
      return true;
    }

    // Token: 0x060007F8 RID: 2040 RVA: 0x000FF0E8 File Offset: 0x000FF0E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentHarnessMcpServer left, AgentHarnessMcpServer right)
    {
      return !(left == right);
    }

    // Token: 0x060007F9 RID: 2041 RVA: 0x000FF0F4 File Offset: 0x000FF0F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentHarnessMcpServer left, AgentHarnessMcpServer right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060007FA RID: 2042 RVA: 0x000FF108 File Offset: 0x000FF108
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Url>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<BearerToken>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<Headers>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SendsTurnIdentity>k__BackingField)) * -1521134295 + EqualityComparer<AgentHarnessLocalMcpCommand>.Default.GetHashCode(this.<LocalCommand>k__BackingField);
    }

    // Token: 0x060007FB RID: 2043 RVA: 0x000FF1B0 File Offset: 0x000FF1B0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentHarnessMcpServer);
    }

    // Token: 0x060007FC RID: 2044 RVA: 0x000FF1C0 File Offset: 0x000FF1C0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentHarnessMcpServer other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Url>k__BackingField, other.<Url>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<BearerToken>k__BackingField, other.<BearerToken>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<Headers>k__BackingField, other.<Headers>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SendsTurnIdentity>k__BackingField, other.<SendsTurnIdentity>k__BackingField) && EqualityComparer<AgentHarnessLocalMcpCommand>.Default.Equals(this.<LocalCommand>k__BackingField, other.<LocalCommand>k__BackingField));
    }

    // Token: 0x060007FE RID: 2046 RVA: 0x000FF28C File Offset: 0x000FF28C
    [CompilerGenerated]
    private AgentHarnessMcpServer(AgentHarnessMcpServer original)
    {
      this.Name = original.<Name>k__BackingField;
      this.Url = original.<Url>k__BackingField;
      this.BearerToken = original.<BearerToken>k__BackingField;
      this.Headers = original.<Headers>k__BackingField;
      this.SendsTurnIdentity = original.<SendsTurnIdentity>k__BackingField;
      this.LocalCommand = original.<LocalCommand>k__BackingField;
    }

    // Token: 0x060007FF RID: 2047 RVA: 0x000FF2E8 File Offset: 0x000FF2E8
    [CompilerGenerated]
    public void Deconstruct(out string Name, out string Url, [Nullable(2)] out string BearerToken)
    {
      Name = this.Name;
      Url = this.Url;
      BearerToken = this.BearerToken;
    }
  }
}
