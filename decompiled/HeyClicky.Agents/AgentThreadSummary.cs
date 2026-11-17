using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents
{
  // Token: 0x0200000D RID: 13
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentThreadSummary : IEquatable<AgentThreadSummary>
  {
    // Token: 0x060000C6 RID: 198 RVA: 0x000D864C File Offset: 0x000D864C
    public AgentThreadSummary(string ThreadId, [Nullable(2)] string Name, string Preview, DateTimeOffset? CreatedAt, DateTimeOffset? UpdatedAt, string Status, [Nullable(2)] string Cwd, [Nullable(2)] string ClickySlug)
    {
      this.ThreadId = ThreadId;
      this.Name = Name;
      this.Preview = Preview;
      this.CreatedAt = CreatedAt;
      this.UpdatedAt = UpdatedAt;
      this.Status = Status;
      this.Cwd = Cwd;
      this.ClickySlug = ClickySlug;
      base..ctor();
    }

    // Token: 0x17000046 RID: 70
    // (get) Token: 0x060000C7 RID: 199 RVA: 0x000D869C File Offset: 0x000D869C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentThreadSummary);
      }
    }

    // Token: 0x17000047 RID: 71
    // (get) Token: 0x060000C8 RID: 200 RVA: 0x000D86A8 File Offset: 0x000D86A8
    // (set) Token: 0x060000C9 RID: 201 RVA: 0x000D86B0 File Offset: 0x000D86B0
    public string ThreadId { get; set; }

    // Token: 0x17000048 RID: 72
    // (get) Token: 0x060000CA RID: 202 RVA: 0x000D86BC File Offset: 0x000D86BC
    // (set) Token: 0x060000CB RID: 203 RVA: 0x000D86C4 File Offset: 0x000D86C4
    [Nullable(2)]
    public string Name
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000049 RID: 73
    // (get) Token: 0x060000CC RID: 204 RVA: 0x000D86D0 File Offset: 0x000D86D0
    // (set) Token: 0x060000CD RID: 205 RVA: 0x000D86D8 File Offset: 0x000D86D8
    public string Preview { get; set; }

    // Token: 0x1700004A RID: 74
    // (get) Token: 0x060000CE RID: 206 RVA: 0x000D86E4 File Offset: 0x000D86E4
    // (set) Token: 0x060000CF RID: 207 RVA: 0x000D86EC File Offset: 0x000D86EC
    public DateTimeOffset? CreatedAt { get; set; }

    // Token: 0x1700004B RID: 75
    // (get) Token: 0x060000D0 RID: 208 RVA: 0x000D86F8 File Offset: 0x000D86F8
    // (set) Token: 0x060000D1 RID: 209 RVA: 0x000D8700 File Offset: 0x000D8700
    public DateTimeOffset? UpdatedAt { get; set; }

    // Token: 0x1700004C RID: 76
    // (get) Token: 0x060000D2 RID: 210 RVA: 0x000D870C File Offset: 0x000D870C
    // (set) Token: 0x060000D3 RID: 211 RVA: 0x000D8714 File Offset: 0x000D8714
    public string Status { get; set; }

    // Token: 0x1700004D RID: 77
    // (get) Token: 0x060000D4 RID: 212 RVA: 0x000D8720 File Offset: 0x000D8720
    // (set) Token: 0x060000D5 RID: 213 RVA: 0x000D8728 File Offset: 0x000D8728
    [Nullable(2)]
    public string Cwd
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700004E RID: 78
    // (get) Token: 0x060000D6 RID: 214 RVA: 0x000D8734 File Offset: 0x000D8734
    // (set) Token: 0x060000D7 RID: 215 RVA: 0x000D873C File Offset: 0x000D873C
    [Nullable(2)]
    public string ClickySlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x060000D8 RID: 216 RVA: 0x000D8748 File Offset: 0x000D8748
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentThreadSummary");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060000D9 RID: 217 RVA: 0x000D8794 File Offset: 0x000D8794
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Preview = ");
      builder.Append(this.Preview);
      builder.Append(", CreatedAt = ");
      builder.Append(this.CreatedAt.ToString());
      builder.Append(", UpdatedAt = ");
      builder.Append(this.UpdatedAt.ToString());
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", Cwd = ");
      builder.Append(this.Cwd);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      return true;
    }

    // Token: 0x060000DA RID: 218 RVA: 0x000D888C File Offset: 0x000D888C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentThreadSummary left, AgentThreadSummary right)
    {
      return !(left == right);
    }

    // Token: 0x060000DB RID: 219 RVA: 0x000D8898 File Offset: 0x000D8898
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentThreadSummary left, AgentThreadSummary right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060000DC RID: 220 RVA: 0x000D88AC File Offset: 0x000D88AC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Preview>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<CreatedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<UpdatedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Cwd>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField);
    }

    // Token: 0x060000DD RID: 221 RVA: 0x000D8984 File Offset: 0x000D8984
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentThreadSummary);
    }

    // Token: 0x060000DE RID: 222 RVA: 0x000D8994 File Offset: 0x000D8994
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentThreadSummary other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Preview>k__BackingField, other.<Preview>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<CreatedAt>k__BackingField, other.<CreatedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<UpdatedAt>k__BackingField, other.<UpdatedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Cwd>k__BackingField, other.<Cwd>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField));
    }

    // Token: 0x060000E0 RID: 224 RVA: 0x000D8A94 File Offset: 0x000D8A94
    [CompilerGenerated]
    private AgentThreadSummary(AgentThreadSummary original)
    {
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Preview = original.<Preview>k__BackingField;
      this.CreatedAt = original.<CreatedAt>k__BackingField;
      this.UpdatedAt = original.<UpdatedAt>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.Cwd = original.<Cwd>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
    }

    // Token: 0x060000E1 RID: 225 RVA: 0x000D8B08 File Offset: 0x000D8B08
    [CompilerGenerated]
    public void Deconstruct(out string ThreadId, [Nullable(2)] out string Name, out string Preview, out DateTimeOffset? CreatedAt, out DateTimeOffset? UpdatedAt, out string Status, [Nullable(2)] out string Cwd, [Nullable(2)] out string ClickySlug)
    {
      ThreadId = this.ThreadId;
      Name = this.Name;
      Preview = this.Preview;
      CreatedAt = this.CreatedAt;
      UpdatedAt = this.UpdatedAt;
      Status = this.Status;
      Cwd = this.Cwd;
      ClickySlug = this.ClickySlug;
    }
  }
}
