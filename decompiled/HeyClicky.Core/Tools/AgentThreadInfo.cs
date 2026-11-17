using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Tools
{
  // Token: 0x02000094 RID: 148
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentThreadInfo : IEquatable<AgentThreadInfo>
  {
    // Token: 0x0600091A RID: 2330 RVA: 0x0036CF74 File Offset: 0x0036CF74
    public AgentThreadInfo(string ThreadId, [Nullable(2)] string Title, string Preview, DateTimeOffset? UpdatedAt, string Status, [Nullable(2)] string ClickySlug)
    {
      this.ThreadId = ThreadId;
      this.Title = Title;
      this.Preview = Preview;
      this.UpdatedAt = UpdatedAt;
      this.Status = Status;
      this.ClickySlug = ClickySlug;
      base..ctor();
    }

    // Token: 0x17000208 RID: 520
    // (get) Token: 0x0600091B RID: 2331 RVA: 0x0036CFAC File Offset: 0x0036CFAC
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentThreadInfo);
      }
    }

    // Token: 0x17000209 RID: 521
    // (get) Token: 0x0600091C RID: 2332 RVA: 0x0036CFB8 File Offset: 0x0036CFB8
    // (set) Token: 0x0600091D RID: 2333 RVA: 0x0036CFC0 File Offset: 0x0036CFC0
    public string ThreadId { get; set; }

    // Token: 0x1700020A RID: 522
    // (get) Token: 0x0600091E RID: 2334 RVA: 0x0036CFCC File Offset: 0x0036CFCC
    // (set) Token: 0x0600091F RID: 2335 RVA: 0x0036CFD4 File Offset: 0x0036CFD4
    [Nullable(2)]
    public string Title
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700020B RID: 523
    // (get) Token: 0x06000920 RID: 2336 RVA: 0x0036CFE0 File Offset: 0x0036CFE0
    // (set) Token: 0x06000921 RID: 2337 RVA: 0x0036CFE8 File Offset: 0x0036CFE8
    public string Preview { get; set; }

    // Token: 0x1700020C RID: 524
    // (get) Token: 0x06000922 RID: 2338 RVA: 0x0036CFF4 File Offset: 0x0036CFF4
    // (set) Token: 0x06000923 RID: 2339 RVA: 0x0036CFFC File Offset: 0x0036CFFC
    public DateTimeOffset? UpdatedAt { get; set; }

    // Token: 0x1700020D RID: 525
    // (get) Token: 0x06000924 RID: 2340 RVA: 0x0036D008 File Offset: 0x0036D008
    // (set) Token: 0x06000925 RID: 2341 RVA: 0x0036D010 File Offset: 0x0036D010
    public string Status { get; set; }

    // Token: 0x1700020E RID: 526
    // (get) Token: 0x06000926 RID: 2342 RVA: 0x0036D01C File Offset: 0x0036D01C
    // (set) Token: 0x06000927 RID: 2343 RVA: 0x0036D024 File Offset: 0x0036D024
    [Nullable(2)]
    public string ClickySlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000928 RID: 2344 RVA: 0x0036D030 File Offset: 0x0036D030
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentThreadInfo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000929 RID: 2345 RVA: 0x0036D07C File Offset: 0x0036D07C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Preview = ");
      builder.Append(this.Preview);
      builder.Append(", UpdatedAt = ");
      builder.Append(this.UpdatedAt.ToString());
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", ClickySlug = ");
      builder.Append(this.ClickySlug);
      return true;
    }

    // Token: 0x0600092A RID: 2346 RVA: 0x0036D134 File Offset: 0x0036D134
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentThreadInfo left, AgentThreadInfo right)
    {
      return !(left == right);
    }

    // Token: 0x0600092B RID: 2347 RVA: 0x0036D140 File Offset: 0x0036D140
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentThreadInfo left, AgentThreadInfo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600092C RID: 2348 RVA: 0x0036D154 File Offset: 0x0036D154
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Preview>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<UpdatedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClickySlug>k__BackingField);
    }

    // Token: 0x0600092D RID: 2349 RVA: 0x0036D1FC File Offset: 0x0036D1FC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentThreadInfo);
    }

    // Token: 0x0600092E RID: 2350 RVA: 0x0036D20C File Offset: 0x0036D20C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentThreadInfo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Preview>k__BackingField, other.<Preview>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<UpdatedAt>k__BackingField, other.<UpdatedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClickySlug>k__BackingField, other.<ClickySlug>k__BackingField));
    }

    // Token: 0x06000930 RID: 2352 RVA: 0x0036D2D8 File Offset: 0x0036D2D8
    [CompilerGenerated]
    private AgentThreadInfo(AgentThreadInfo original)
    {
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Preview = original.<Preview>k__BackingField;
      this.UpdatedAt = original.<UpdatedAt>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.ClickySlug = original.<ClickySlug>k__BackingField;
    }

    // Token: 0x06000931 RID: 2353 RVA: 0x0036D334 File Offset: 0x0036D334
    [CompilerGenerated]
    public void Deconstruct(out string ThreadId, [Nullable(2)] out string Title, out string Preview, out DateTimeOffset? UpdatedAt, out string Status, [Nullable(2)] out string ClickySlug)
    {
      ThreadId = this.ThreadId;
      Title = this.Title;
      Preview = this.Preview;
      UpdatedAt = this.UpdatedAt;
      Status = this.Status;
      ClickySlug = this.ClickySlug;
    }
  }
}
