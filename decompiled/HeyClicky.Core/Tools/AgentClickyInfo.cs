using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000092 RID: 146
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentClickyInfo : IEquatable<AgentClickyInfo>
  {
    // Token: 0x060008D3 RID: 2259 RVA: 0x0036C1CC File Offset: 0x0036C1CC
    public AgentClickyInfo(string Slug, string Name, string RoleText, string OneLiner, [Nullable(2)] string ThreadId, bool IsArchived = false)
    {
      this.Slug = Slug;
      this.Name = Name;
      this.RoleText = RoleText;
      this.OneLiner = OneLiner;
      this.ThreadId = ThreadId;
      this.IsArchived = IsArchived;
      base..ctor();
    }

    // Token: 0x170001EE RID: 494
    // (get) Token: 0x060008D4 RID: 2260 RVA: 0x0036C204 File Offset: 0x0036C204
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentClickyInfo);
      }
    }

    // Token: 0x170001EF RID: 495
    // (get) Token: 0x060008D5 RID: 2261 RVA: 0x0036C210 File Offset: 0x0036C210
    // (set) Token: 0x060008D6 RID: 2262 RVA: 0x0036C218 File Offset: 0x0036C218
    public string Slug { get; set; }

    // Token: 0x170001F0 RID: 496
    // (get) Token: 0x060008D7 RID: 2263 RVA: 0x0036C224 File Offset: 0x0036C224
    // (set) Token: 0x060008D8 RID: 2264 RVA: 0x0036C22C File Offset: 0x0036C22C
    public string Name { get; set; }

    // Token: 0x170001F1 RID: 497
    // (get) Token: 0x060008D9 RID: 2265 RVA: 0x0036C238 File Offset: 0x0036C238
    // (set) Token: 0x060008DA RID: 2266 RVA: 0x0036C240 File Offset: 0x0036C240
    public string RoleText { get; set; }

    // Token: 0x170001F2 RID: 498
    // (get) Token: 0x060008DB RID: 2267 RVA: 0x0036C24C File Offset: 0x0036C24C
    // (set) Token: 0x060008DC RID: 2268 RVA: 0x0036C254 File Offset: 0x0036C254
    public string OneLiner { get; set; }

    // Token: 0x170001F3 RID: 499
    // (get) Token: 0x060008DD RID: 2269 RVA: 0x0036C260 File Offset: 0x0036C260
    // (set) Token: 0x060008DE RID: 2270 RVA: 0x0036C268 File Offset: 0x0036C268
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001F4 RID: 500
    // (get) Token: 0x060008DF RID: 2271 RVA: 0x0036C274 File Offset: 0x0036C274
    // (set) Token: 0x060008E0 RID: 2272 RVA: 0x0036C27C File Offset: 0x0036C27C
    public bool IsArchived { get; set; }

    // Token: 0x060008E1 RID: 2273 RVA: 0x0036C288 File Offset: 0x0036C288
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentClickyInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060008E2 RID: 2274 RVA: 0x0036C2D4 File Offset: 0x0036C2D4
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
      builder.Append(", IsArchived = ");
      builder.Append(this.IsArchived.ToString());
      return true;
    }

    // Token: 0x060008E3 RID: 2275 RVA: 0x0036C38C File Offset: 0x0036C38C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentClickyInfo left, AgentClickyInfo right)
    {
      return !(left == right);
    }

    // Token: 0x060008E4 RID: 2276 RVA: 0x0036C398 File Offset: 0x0036C398
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentClickyInfo left, AgentClickyInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060008E5 RID: 2277 RVA: 0x0036C3AC File Offset: 0x0036C3AC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RoleText>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OneLiner>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsArchived>k__BackingField);
    }

    // Token: 0x060008E6 RID: 2278 RVA: 0x0036C454 File Offset: 0x0036C454
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentClickyInfo);
    }

    // Token: 0x060008E7 RID: 2279 RVA: 0x0036C464 File Offset: 0x0036C464
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentClickyInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RoleText>k__BackingField, other.<RoleText>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OneLiner>k__BackingField, other.<OneLiner>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsArchived>k__BackingField, other.<IsArchived>k__BackingField));
    }

    // Token: 0x060008E9 RID: 2281 RVA: 0x0036C530 File Offset: 0x0036C530
    [CompilerGenerated]
    private AgentClickyInfo(AgentClickyInfo original)
    {
      this.Slug = original.<Slug>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.RoleText = original.<RoleText>k__BackingField;
      this.OneLiner = original.<OneLiner>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.IsArchived = original.<IsArchived>k__BackingField;
    }

    // Token: 0x060008EA RID: 2282 RVA: 0x0036C58C File Offset: 0x0036C58C
    [CompilerGenerated]
    public void Deconstruct(out string Slug, out string Name, out string RoleText, out string OneLiner, [Nullable(2)] out string ThreadId, out bool IsArchived)
    {
      Slug = this.Slug;
      Name = this.Name;
      RoleText = this.RoleText;
      OneLiner = this.OneLiner;
      ThreadId = this.ThreadId;
      IsArchived = this.IsArchived;
    }
  }
}
