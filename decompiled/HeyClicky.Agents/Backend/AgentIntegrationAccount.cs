using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Backend
{
  // Token: 0x0200006A RID: 106
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AgentIntegrationAccount : IEquatable<AgentIntegrationAccount>
  {
    // Token: 0x0600086E RID: 2158 RVA: 0x00101060 File Offset: 0x00101060
    public AgentIntegrationAccount([Nullable(1)] string ConnectedAccountId, string AccountName, string Label, string AvatarUrl, string AccountNameKind, [Nullable(1)] string Status, string ConnectedAt)
    {
      this.ConnectedAccountId = ConnectedAccountId;
      this.AccountName = AccountName;
      this.Label = Label;
      this.AvatarUrl = AvatarUrl;
      this.AccountNameKind = AccountNameKind;
      this.Status = Status;
      this.ConnectedAt = ConnectedAt;
      base..ctor();
    }

    // Token: 0x17000218 RID: 536
    // (get) Token: 0x0600086F RID: 2159 RVA: 0x001010A0 File Offset: 0x001010A0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AgentIntegrationAccount);
      }
    }

    // Token: 0x17000219 RID: 537
    // (get) Token: 0x06000870 RID: 2160 RVA: 0x001010AC File Offset: 0x001010AC
    // (set) Token: 0x06000871 RID: 2161 RVA: 0x001010B4 File Offset: 0x001010B4
    [Nullable(1)]
    public string ConnectedAccountId
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700021A RID: 538
    // (get) Token: 0x06000872 RID: 2162 RVA: 0x001010C0 File Offset: 0x001010C0
    // (set) Token: 0x06000873 RID: 2163 RVA: 0x001010C8 File Offset: 0x001010C8
    public string AccountName { get; set; }

    // Token: 0x1700021B RID: 539
    // (get) Token: 0x06000874 RID: 2164 RVA: 0x001010D4 File Offset: 0x001010D4
    // (set) Token: 0x06000875 RID: 2165 RVA: 0x001010DC File Offset: 0x001010DC
    public string Label { get; set; }

    // Token: 0x1700021C RID: 540
    // (get) Token: 0x06000876 RID: 2166 RVA: 0x001010E8 File Offset: 0x001010E8
    // (set) Token: 0x06000877 RID: 2167 RVA: 0x001010F0 File Offset: 0x001010F0
    public string AvatarUrl { get; set; }

    // Token: 0x1700021D RID: 541
    // (get) Token: 0x06000878 RID: 2168 RVA: 0x001010FC File Offset: 0x001010FC
    // (set) Token: 0x06000879 RID: 2169 RVA: 0x00101104 File Offset: 0x00101104
    public string AccountNameKind { get; set; }

    // Token: 0x1700021E RID: 542
    // (get) Token: 0x0600087A RID: 2170 RVA: 0x00101110 File Offset: 0x00101110
    // (set) Token: 0x0600087B RID: 2171 RVA: 0x00101118 File Offset: 0x00101118
    [Nullable(1)]
    public string Status
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x1700021F RID: 543
    // (get) Token: 0x0600087C RID: 2172 RVA: 0x00101124 File Offset: 0x00101124
    // (set) Token: 0x0600087D RID: 2173 RVA: 0x0010112C File Offset: 0x0010112C
    public string ConnectedAt { get; set; }

    // Token: 0x17000220 RID: 544
    // (get) Token: 0x0600087E RID: 2174 RVA: 0x00101138 File Offset: 0x00101138
    public bool IsActive
    {
      get
      {
        return this.Status == "connected";
      }
    }

    // Token: 0x17000221 RID: 545
    // (get) Token: 0x0600087F RID: 2175 RVA: 0x0010114C File Offset: 0x0010114C
    public bool IsOrganization
    {
      get
      {
        return this.AccountNameKind == "organization";
      }
    }

    // Token: 0x17000222 RID: 546
    // (get) Token: 0x06000880 RID: 2176 RVA: 0x00101160 File Offset: 0x00101160
    public string TrimmedAccountName
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(this.AccountName))
        {
          return this.AccountName.Trim();
        }
        return null;
      }
    }

    // Token: 0x17000223 RID: 547
    // (get) Token: 0x06000881 RID: 2177 RVA: 0x0010117C File Offset: 0x0010117C
    public string TrimmedLabel
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(this.Label))
        {
          return this.Label.Trim();
        }
        return null;
      }
    }

    // Token: 0x17000224 RID: 548
    // (get) Token: 0x06000882 RID: 2178 RVA: 0x00101198 File Offset: 0x00101198
    public DateTimeOffset? ConnectedDate
    {
      get
      {
        DateTimeOffset dateTimeOffset;
        if (!DateTimeOffset.TryParse(this.ConnectedAt, out dateTimeOffset))
        {
          return null;
        }
        return new DateTimeOffset?(dateTimeOffset);
      }
    }

    // Token: 0x06000883 RID: 2179 RVA: 0x001011C4 File Offset: 0x001011C4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentIntegrationAccount");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000884 RID: 2180 RVA: 0x00101210 File Offset: 0x00101210
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
      builder.Append(", AvatarUrl = ");
      builder.Append(this.AvatarUrl);
      builder.Append(", AccountNameKind = ");
      builder.Append(this.AccountNameKind);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ConnectedAt = ");
      builder.Append(this.ConnectedAt);
      builder.Append(", IsActive = ");
      builder.Append(this.IsActive.ToString());
      builder.Append(", IsOrganization = ");
      builder.Append(this.IsOrganization.ToString());
      builder.Append(", TrimmedAccountName = ");
      builder.Append(this.TrimmedAccountName);
      builder.Append(", TrimmedLabel = ");
      builder.Append(this.TrimmedLabel);
      builder.Append(", ConnectedDate = ");
      builder.Append(this.ConnectedDate.ToString());
      return true;
    }

    // Token: 0x06000885 RID: 2181 RVA: 0x0010137C File Offset: 0x0010137C
    [CompilerGenerated]
    public static bool operator !=(AgentIntegrationAccount left, AgentIntegrationAccount right)
    {
      return !(left == right);
    }

    // Token: 0x06000886 RID: 2182 RVA: 0x00101388 File Offset: 0x00101388
    [CompilerGenerated]
    public static bool operator ==(AgentIntegrationAccount left, AgentIntegrationAccount right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000887 RID: 2183 RVA: 0x0010139C File Offset: 0x0010139C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ConnectedAccountId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccountName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AvatarUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AccountNameKind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ConnectedAt>k__BackingField);
    }

    // Token: 0x06000888 RID: 2184 RVA: 0x0010145C File Offset: 0x0010145C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentIntegrationAccount);
    }

    // Token: 0x06000889 RID: 2185 RVA: 0x0010146C File Offset: 0x0010146C
    [CompilerGenerated]
    public bool Equals(AgentIntegrationAccount other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ConnectedAccountId>k__BackingField, other.<ConnectedAccountId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AccountName>k__BackingField, other.<AccountName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AvatarUrl>k__BackingField, other.<AvatarUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AccountNameKind>k__BackingField, other.<AccountNameKind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ConnectedAt>k__BackingField, other.<ConnectedAt>k__BackingField));
    }

    // Token: 0x0600088B RID: 2187 RVA: 0x00101554 File Offset: 0x00101554
    [CompilerGenerated]
    private AgentIntegrationAccount([Nullable(1)] AgentIntegrationAccount original)
    {
      this.ConnectedAccountId = original.<ConnectedAccountId>k__BackingField;
      this.AccountName = original.<AccountName>k__BackingField;
      this.Label = original.<Label>k__BackingField;
      this.AvatarUrl = original.<AvatarUrl>k__BackingField;
      this.AccountNameKind = original.<AccountNameKind>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ConnectedAt = original.<ConnectedAt>k__BackingField;
    }

    // Token: 0x0600088C RID: 2188 RVA: 0x001015BC File Offset: 0x001015BC
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string ConnectedAccountId, out string AccountName, out string Label, out string AvatarUrl, out string AccountNameKind, [Nullable(1)] out string Status, out string ConnectedAt)
    {
      ConnectedAccountId = this.ConnectedAccountId;
      AccountName = this.AccountName;
      Label = this.Label;
      AvatarUrl = this.AvatarUrl;
      AccountNameKind = this.AccountNameKind;
      Status = this.Status;
      ConnectedAt = this.ConnectedAt;
    }
  }
}
