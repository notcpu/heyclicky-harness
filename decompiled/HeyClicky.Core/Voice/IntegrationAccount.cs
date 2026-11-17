using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000088 RID: 136
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class IntegrationAccount : IEquatable<IntegrationAccount>
  {
    // Token: 0x06000835 RID: 2101 RVA: 0x00368FB0 File Offset: 0x00368FB0
    public IntegrationAccount([Nullable(1)] string ConnectedAccountId, string AccountName, string Label)
    {
      this.ConnectedAccountId = ConnectedAccountId;
      this.AccountName = AccountName;
      this.Label = Label;
      base..ctor();
    }

    // Token: 0x170001CD RID: 461
    // (get) Token: 0x06000836 RID: 2102 RVA: 0x00368FD0 File Offset: 0x00368FD0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(IntegrationAccount);
      }
    }

    // Token: 0x170001CE RID: 462
    // (get) Token: 0x06000837 RID: 2103 RVA: 0x00368FDC File Offset: 0x00368FDC
    // (set) Token: 0x06000838 RID: 2104 RVA: 0x00368FE4 File Offset: 0x00368FE4
    [Nullable(1)]
    public string ConnectedAccountId
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170001CF RID: 463
    // (get) Token: 0x06000839 RID: 2105 RVA: 0x00368FF0 File Offset: 0x00368FF0
    // (set) Token: 0x0600083A RID: 2106 RVA: 0x00368FF8 File Offset: 0x00368FF8
    public string AccountName { get; set; }

    // Token: 0x170001D0 RID: 464
    // (get) Token: 0x0600083B RID: 2107 RVA: 0x00369004 File Offset: 0x00369004
    // (set) Token: 0x0600083C RID: 2108 RVA: 0x0036900C File Offset: 0x0036900C
    public string Label { get; set; }

    // Token: 0x0600083D RID: 2109 RVA: 0x00369018 File Offset: 0x00369018
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("IntegrationAccount");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600083E RID: 2110 RVA: 0x00369064 File Offset: 0x00369064
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ConnectedAccountId = ");
      builder.Append(this.ConnectedAccountId);
      builder.Append(", AccountName = ");
      builder.Append(this.AccountName);
      builder.Append(", Label = ");
      builder.Append(this.Label);
      return true;
    }

    // Token: 0x0600083F RID: 2111 RVA: 0x003690C4 File Offset: 0x003690C4
    [CompilerGenerated]
    public static bool operator !=(IntegrationAccount left, IntegrationAccount right)
    {
      return !(left == right);
    }

    // Token: 0x06000840 RID: 2112 RVA: 0x003690D0 File Offset: 0x003690D0
    [CompilerGenerated]
    public static bool operator ==(IntegrationAccount left, IntegrationAccount right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000841 RID: 2113 RVA: 0x003690E4 File Offset: 0x003690E4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ConnectedAccountId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccountName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField);
    }

    // Token: 0x06000842 RID: 2114 RVA: 0x00369148 File Offset: 0x00369148
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as IntegrationAccount);
    }

    // Token: 0x06000843 RID: 2115 RVA: 0x00369158 File Offset: 0x00369158
    [CompilerGenerated]
    public bool Equals(IntegrationAccount other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ConnectedAccountId>k__BackingField, other.<ConnectedAccountId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AccountName>k__BackingField, other.<AccountName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField));
    }

    // Token: 0x06000845 RID: 2117 RVA: 0x003691D4 File Offset: 0x003691D4
    [CompilerGenerated]
    private IntegrationAccount([Nullable(1)] IntegrationAccount original)
    {
      this.ConnectedAccountId = original.<ConnectedAccountId>k__BackingField;
      this.AccountName = original.<AccountName>k__BackingField;
      this.Label = original.<Label>k__BackingField;
    }

    // Token: 0x06000846 RID: 2118 RVA: 0x00369200 File Offset: 0x00369200
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string ConnectedAccountId, out string AccountName, out string Label)
    {
      ConnectedAccountId = this.ConnectedAccountId;
      AccountName = this.AccountName;
      Label = this.Label;
    }
  }
}
