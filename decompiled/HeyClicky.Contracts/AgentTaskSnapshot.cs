using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200002F RID: 47
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTaskSnapshot : IEquatable<AgentTaskSnapshot>
  {
    // Token: 0x0600023F RID: 575 RVA: 0x000203E4 File Offset: 0x000203E4
    public AgentTaskSnapshot(string TaskId, [Nullable(2)] string ThreadId, string Title, AgentTaskStatus Status, string LatestCommentary, [Nullable(2)] string FinalResponseMarkdown, DateTimeOffset StartedAt)
    {
      this.TaskId = TaskId;
      this.ThreadId = ThreadId;
      this.Title = Title;
      this.Status = Status;
      this.LatestCommentary = LatestCommentary;
      this.FinalResponseMarkdown = FinalResponseMarkdown;
      this.StartedAt = StartedAt;
      base..ctor();
    }

    // Token: 0x17000093 RID: 147
    // (get) Token: 0x06000240 RID: 576 RVA: 0x00020424 File Offset: 0x00020424
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentTaskSnapshot);
      }
    }

    // Token: 0x17000094 RID: 148
    // (get) Token: 0x06000241 RID: 577 RVA: 0x00020430 File Offset: 0x00020430
    // (set) Token: 0x06000242 RID: 578 RVA: 0x00020438 File Offset: 0x00020438
    public string TaskId { get; set; }

    // Token: 0x17000095 RID: 149
    // (get) Token: 0x06000243 RID: 579 RVA: 0x00020444 File Offset: 0x00020444
    // (set) Token: 0x06000244 RID: 580 RVA: 0x0002044C File Offset: 0x0002044C
    [Nullable(2)]
    public string ThreadId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000096 RID: 150
    // (get) Token: 0x06000245 RID: 581 RVA: 0x00020458 File Offset: 0x00020458
    // (set) Token: 0x06000246 RID: 582 RVA: 0x00020460 File Offset: 0x00020460
    public string Title { get; set; }

    // Token: 0x17000097 RID: 151
    // (get) Token: 0x06000247 RID: 583 RVA: 0x0002046C File Offset: 0x0002046C
    // (set) Token: 0x06000248 RID: 584 RVA: 0x00020474 File Offset: 0x00020474
    public AgentTaskStatus Status { get; set; }

    // Token: 0x17000098 RID: 152
    // (get) Token: 0x06000249 RID: 585 RVA: 0x00020480 File Offset: 0x00020480
    // (set) Token: 0x0600024A RID: 586 RVA: 0x00020488 File Offset: 0x00020488
    public string LatestCommentary { get; set; }

    // Token: 0x17000099 RID: 153
    // (get) Token: 0x0600024B RID: 587 RVA: 0x00020494 File Offset: 0x00020494
    // (set) Token: 0x0600024C RID: 588 RVA: 0x0002049C File Offset: 0x0002049C
    [Nullable(2)]
    public string FinalResponseMarkdown
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700009A RID: 154
    // (get) Token: 0x0600024D RID: 589 RVA: 0x000204A8 File Offset: 0x000204A8
    // (set) Token: 0x0600024E RID: 590 RVA: 0x000204B0 File Offset: 0x000204B0
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x0600024F RID: 591 RVA: 0x000204BC File Offset: 0x000204BC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentTaskSnapshot");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000250 RID: 592 RVA: 0x00020508 File Offset: 0x00020508
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", ThreadId = ");
      builder.Append(this.ThreadId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      builder.Append(", Status = ");
      builder.Append(this.Status.ToString());
      builder.Append(", LatestCommentary = ");
      builder.Append(this.LatestCommentary);
      builder.Append(", FinalResponseMarkdown = ");
      builder.Append(this.FinalResponseMarkdown);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      return true;
    }

    // Token: 0x06000251 RID: 593 RVA: 0x000205E8 File Offset: 0x000205E8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentTaskSnapshot left, AgentTaskSnapshot right)
    {
      return !(left == right);
    }

    // Token: 0x06000252 RID: 594 RVA: 0x000205F4 File Offset: 0x000205F4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentTaskSnapshot left, AgentTaskSnapshot right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000253 RID: 595 RVA: 0x00020608 File Offset: 0x00020608
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<AgentTaskStatus>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LatestCommentary>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FinalResponseMarkdown>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField);
    }

    // Token: 0x06000254 RID: 596 RVA: 0x000206C8 File Offset: 0x000206C8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentTaskSnapshot);
    }

    // Token: 0x06000255 RID: 597 RVA: 0x000206D8 File Offset: 0x000206D8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentTaskSnapshot other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<AgentTaskStatus>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LatestCommentary>k__BackingField, other.<LatestCommentary>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FinalResponseMarkdown>k__BackingField, other.<FinalResponseMarkdown>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField));
    }

    // Token: 0x06000257 RID: 599 RVA: 0x000207C0 File Offset: 0x000207C0
    [CompilerGenerated]
    private AgentTaskSnapshot(AgentTaskSnapshot original)
    {
      this.TaskId = original.<TaskId>k__BackingField;
      this.ThreadId = original.<ThreadId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.LatestCommentary = original.<LatestCommentary>k__BackingField;
      this.FinalResponseMarkdown = original.<FinalResponseMarkdown>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
    }

    // Token: 0x06000258 RID: 600 RVA: 0x00020828 File Offset: 0x00020828
    [CompilerGenerated]
    public void Deconstruct(out string TaskId, [Nullable(2)] out string ThreadId, out string Title, out AgentTaskStatus Status, out string LatestCommentary, [Nullable(2)] out string FinalResponseMarkdown, out DateTimeOffset StartedAt)
    {
      TaskId = this.TaskId;
      ThreadId = this.ThreadId;
      Title = this.Title;
      Status = this.Status;
      LatestCommentary = this.LatestCommentary;
      FinalResponseMarkdown = this.FinalResponseMarkdown;
      StartedAt = this.StartedAt;
    }
  }
}
