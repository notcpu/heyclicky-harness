using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x0200002B RID: 43
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NotchAgentFollowUp : IEquatable<NotchAgentFollowUp>
  {
    // Token: 0x0600021C RID: 540 RVA: 0x00020138 File Offset: 0x00020138
    public NotchAgentFollowUp([Nullable(2)] string TaskId, string Title)
    {
      this.TaskId = TaskId;
      this.Title = Title;
      base..ctor();
    }

    // Token: 0x1700008D RID: 141
    // (get) Token: 0x0600021D RID: 541 RVA: 0x00020150 File Offset: 0x00020150
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(NotchAgentFollowUp);
      }
    }

    // Token: 0x1700008E RID: 142
    // (get) Token: 0x0600021E RID: 542 RVA: 0x0002015C File Offset: 0x0002015C
    // (set) Token: 0x0600021F RID: 543 RVA: 0x00020164 File Offset: 0x00020164
    [Nullable(2)]
    public string TaskId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x1700008F RID: 143
    // (get) Token: 0x06000220 RID: 544 RVA: 0x00020170 File Offset: 0x00020170
    // (set) Token: 0x06000221 RID: 545 RVA: 0x00020178 File Offset: 0x00020178
    public string Title { get; set; }

    // Token: 0x06000222 RID: 546 RVA: 0x00020184 File Offset: 0x00020184
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("NotchAgentFollowUp");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000223 RID: 547 RVA: 0x000201D0 File Offset: 0x000201D0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TaskId = ");
      builder.Append(this.TaskId);
      builder.Append(", Title = ");
      builder.Append(this.Title);
      return true;
    }

    // Token: 0x06000224 RID: 548 RVA: 0x0002020C File Offset: 0x0002020C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(NotchAgentFollowUp left, NotchAgentFollowUp right)
    {
      return !(left == right);
    }

    // Token: 0x06000225 RID: 549 RVA: 0x00020218 File Offset: 0x00020218
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(NotchAgentFollowUp left, NotchAgentFollowUp right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000226 RID: 550 RVA: 0x0002022C File Offset: 0x0002022C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField);
    }

    // Token: 0x06000227 RID: 551 RVA: 0x0002026C File Offset: 0x0002026C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NotchAgentFollowUp);
    }

    // Token: 0x06000228 RID: 552 RVA: 0x0002027C File Offset: 0x0002027C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(NotchAgentFollowUp other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TaskId>k__BackingField, other.<TaskId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField));
    }

    // Token: 0x0600022A RID: 554 RVA: 0x000202E0 File Offset: 0x000202E0
    [CompilerGenerated]
    private NotchAgentFollowUp(NotchAgentFollowUp original)
    {
      this.TaskId = original.<TaskId>k__BackingField;
      this.Title = original.<Title>k__BackingField;
    }

    // Token: 0x0600022B RID: 555 RVA: 0x00020300 File Offset: 0x00020300
    [CompilerGenerated]
    public void Deconstruct([Nullable(2)] out string TaskId, out string Title)
    {
      TaskId = this.TaskId;
      Title = this.Title;
    }
  }
}
