using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Clickys
{
  // Token: 0x02000068 RID: 104
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInfo : IEquatable<ClickyInfo>
  {
    // Token: 0x0600082C RID: 2092 RVA: 0x000FFA0C File Offset: 0x000FFA0C
    public ClickyInfo(string Slug, string Name, string RoleText, string OneLiner, [Nullable(2)] string ThreadId, DateTimeOffset CreatedAt, bool IsArchived = false)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.RoleText = RoleText;
      this.OneLiner = OneLiner;
      this.ThreadId = ThreadId;
      this.CreatedAt = CreatedAt;
      this.IsArchived = IsArchived;
      base..ctor();
    }

    // Token: 0x1700020D RID: 525
    // (get) Token: 0x0600082D RID: 2093 RVA: 0x000FFA4C File Offset: 0x000FFA4C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInfo);
      }
    }

    // Token: 0x1700020E RID: 526
    // (get) Token: 0x0600082E RID: 2094 RVA: 0x000FFA58 File Offset: 0x000FFA58
    // (set) Token: 0x0600082F RID: 2095 RVA: 0x000FFA60 File Offset: 0x000FFA60
    public string Slug { get; set; }

    // Token: 0x1700020F RID: 527
    // (get) Token: 0x06000830 RID: 2096 RVA: 0x000FFA6C File Offset: 0x000FFA6C
    // (set) Token: 0x06000831 RID: 2097 RVA: 0x000FFA74 File Offset: 0x000FFA74
    public string Name { get; set; }

    // Token: 0x17000210 RID: 528
    // (get) Token: 0x06000832 RID: 2098 RVA: 0x000FFA80 File Offset: 0x000FFA80
    // (set) Token: 0x06000833 RID: 2099 RVA: 0x000FFA88 File Offset: 0x000FFA88
    public string RoleText { get; set; }

    // Token: 0x17000211 RID: 529
    // (get) Token: 0x06000834 RID: 2100 RVA: 0x000FFA94 File Offset: 0x000FFA94
    // (set) Token: 0x06000835 RID: 2101 RVA: 0x000FFA9C File Offset: 0x000FFA9C
    public string OneLiner { get; set; }

    // Token: 0x17000212 RID: 530
    // (get) Token: 0x06000836 RID: 2102 RVA: 0x000FFAA8 File Offset: 0x000FFAA8
    // (set) Token: 0x06000837 RID: 2103 RVA: 0x000FFAB0 File Offset: 0x000FFAB0
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000213 RID: 531
    // (get) Token: 0x06000838 RID: 2104 RVA: 0x000FFABC File Offset: 0x000FFABC
    // (set) Token: 0x06000839 RID: 2105 RVA: 0x000FFAC4 File Offset: 0x000FFAC4
    public DateTimeOffset CreatedAt { get; set; }

    // Token: 0x17000214 RID: 532
    // (get) Token: 0x0600083A RID: 2106 RVA: 0x000FFAD0 File Offset: 0x000FFAD0
    // (set) Token: 0x0600083B RID: 2107 RVA: 0x000FFAD8 File Offset: 0x000FFAD8
    public bool IsArchived { get; set; }

    // Token: 0x0600083C RID: 2108 RVA: 0x000FFAE4 File Offset: 0x000FFAE4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600083D RID: 2109 RVA: 0x000FFB30 File Offset: 0x000FFB30
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Slug = ");
      builder.Append(this.Slug);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", RoleText = ");
      builder.Append(this.RoleText);
      builder.Append(", OneLiner = ");
      builder.Append(this.OneLiner);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", IsArchived = ");
      builder.Append(this.IsArchived.ToString());
      return true;
    }

    // Token: 0x0600083E RID: 2110 RVA: 0x000FFC10 File Offset: 0x000FFC10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInfo left, ClickyInfo right)
    {
      return !(left == right);
    }

    // Token: 0x0600083F RID: 2111 RVA: 0x000FFC1C File Offset: 0x000FFC1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInfo left, ClickyInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000840 RID: 2112 RVA: 0x000FFC30 File Offset: 0x000FFC30
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoleText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OneLiner>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsArchived>k__BackingField);
    }

    // Token: 0x06000841 RID: 2113 RVA: 0x000FFCF0 File Offset: 0x000FFCF0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInfo);
    }

    // Token: 0x06000842 RID: 2114 RVA: 0x000FFD00 File Offset: 0x000FFD00
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoleText>k__BackingField, other.<RoleText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OneLiner>k__BackingField, other.<OneLiner>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsArchived>k__BackingField, other.<IsArchived>k__BackingField));
    }

    // Token: 0x06000844 RID: 2116 RVA: 0x000FFDE8 File Offset: 0x000FFDE8
    [CompilerGenerated]
    private ClickyInfo(ClickyInfo original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.RoleText = original.<RoleText>k__BackingField;
      this.OneLiner = original.<OneLiner>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.IsArchived = original.<IsArchived>k__BackingField;
    }

    // Token: 0x06000845 RID: 2117 RVA: 0x000FFE50 File Offset: 0x000FFE50
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, out string RoleText, out string OneLiner, [Nullable(2)] out string ThreadId, out DateTimeOffset CreatedAt, out bool IsArchived)
    {
      Slug = this.Slug;
      Name = this.Name;
      RoleText = this.RoleText;
      OneLiner = this.OneLiner;
      ThreadId = this.ThreadId;
      CreatedAt = this.CreatedAt;
      IsArchived = this.IsArchived;
    }
  }
}
