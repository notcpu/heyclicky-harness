using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000089 RID: 137
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntegrationAccounts : IEquatable<IntegrationAccounts>
  {
    // Token: 0x06000847 RID: 2119 RVA: 0x0036921C File Offset: 0x0036921C
    public IntegrationAccounts(string Integration, IReadOnlyList<IntegrationAccount> Accounts)
    {
      this.Integration = Integration;
      this.Accounts = Accounts;
      base..ctor();
    }

    // Token: 0x170001D1 RID: 465
    // (get) Token: 0x06000848 RID: 2120 RVA: 0x00369234 File Offset: 0x00369234
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(IntegrationAccounts);
      }
    }

    // Token: 0x170001D2 RID: 466
    // (get) Token: 0x06000849 RID: 2121 RVA: 0x00369240 File Offset: 0x00369240
    // (set) Token: 0x0600084A RID: 2122 RVA: 0x00369248 File Offset: 0x00369248
    public string Integration { get; set; }

    // Token: 0x170001D3 RID: 467
    // (get) Token: 0x0600084B RID: 2123 RVA: 0x00369254 File Offset: 0x00369254
    // (set) Token: 0x0600084C RID: 2124 RVA: 0x0036925C File Offset: 0x0036925C
    public IReadOnlyList<IntegrationAccount> Accounts { get; set; }

    // Token: 0x0600084D RID: 2125 RVA: 0x00369268 File Offset: 0x00369268
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("IntegrationAccounts");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600084E RID: 2126 RVA: 0x003692B4 File Offset: 0x003692B4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Integration = ");
      builder.Append(this.Integration);
      builder.Append(", Accounts = ");
      builder.Append(this.Accounts);
      return true;
    }

    // Token: 0x0600084F RID: 2127 RVA: 0x003692F0 File Offset: 0x003692F0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(IntegrationAccounts left, IntegrationAccounts right)
    {
      return !(left == right);
    }

    // Token: 0x06000850 RID: 2128 RVA: 0x003692FC File Offset: 0x003692FC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(IntegrationAccounts left, IntegrationAccounts right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000851 RID: 2129 RVA: 0x00369310 File Offset: 0x00369310
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Integration>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<IntegrationAccount>>.Default.GetHashCode(this.<Accounts>k__BackingField);
    }

    // Token: 0x06000852 RID: 2130 RVA: 0x00369350 File Offset: 0x00369350
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as IntegrationAccounts);
    }

    // Token: 0x06000853 RID: 2131 RVA: 0x00369360 File Offset: 0x00369360
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(IntegrationAccounts other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Integration>k__BackingField, other.<Integration>k__BackingField) && EqualityComparer<IReadOnlyList<IntegrationAccount>>.Default.Equals(this.<Accounts>k__BackingField, other.<Accounts>k__BackingField));
    }

    // Token: 0x06000855 RID: 2133 RVA: 0x003693C4 File Offset: 0x003693C4
    [CompilerGenerated]
    private IntegrationAccounts(IntegrationAccounts original)
    {
      this.Integration = original.<Integration>k__BackingField;
      this.Accounts = original.<Accounts>k__BackingField;
    }

    // Token: 0x06000856 RID: 2134 RVA: 0x003693E4 File Offset: 0x003693E4
    [CompilerGenerated]
    public void Deconstruct(out string Integration, out IReadOnlyList<IntegrationAccount> Accounts)
    {
      Integration = this.Integration;
      Accounts = this.Accounts;
    }
  }
}
