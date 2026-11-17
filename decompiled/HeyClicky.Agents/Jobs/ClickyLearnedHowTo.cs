using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000044 RID: 68
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLearnedHowTo : IEquatable<ClickyLearnedHowTo>
  {
    // Token: 0x06000619 RID: 1561 RVA: 0x000F4430 File Offset: 0x000F4430
    public ClickyLearnedHowTo(string Text, DateTimeOffset LearnedAt, [Nullable(2)] string JobId)
    {
      this.Text = Text;
      this.LearnedAt = LearnedAt;
      this.JobId = JobId;
      base..ctor();
    }

    // Token: 0x17000199 RID: 409
    // (get) Token: 0x0600061A RID: 1562 RVA: 0x000F4450 File Offset: 0x000F4450
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyLearnedHowTo);
      }
    }

    // Token: 0x1700019A RID: 410
    // (get) Token: 0x0600061B RID: 1563 RVA: 0x000F445C File Offset: 0x000F445C
    // (set) Token: 0x0600061C RID: 1564 RVA: 0x000F4464 File Offset: 0x000F4464
    public string Text { get; set; }

    // Token: 0x1700019B RID: 411
    // (get) Token: 0x0600061D RID: 1565 RVA: 0x000F4470 File Offset: 0x000F4470
    // (set) Token: 0x0600061E RID: 1566 RVA: 0x000F4478 File Offset: 0x000F4478
    public DateTimeOffset LearnedAt { get; set; }

    // Token: 0x1700019C RID: 412
    // (get) Token: 0x0600061F RID: 1567 RVA: 0x000F4484 File Offset: 0x000F4484
    // (set) Token: 0x06000620 RID: 1568 RVA: 0x000F448C File Offset: 0x000F448C
    [Nullable(2)]
    public string JobId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000621 RID: 1569 RVA: 0x000F4498 File Offset: 0x000F4498
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyLearnedHowTo");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000622 RID: 1570 RVA: 0x000F44E4 File Offset: 0x000F44E4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", LearnedAt = ");
      builder.Append(this.LearnedAt.ToString());
      builder.Append(", JobId = ");
      builder.Append(this.JobId);
      return true;
    }

    // Token: 0x06000623 RID: 1571 RVA: 0x000F4550 File Offset: 0x000F4550
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyLearnedHowTo left, ClickyLearnedHowTo right)
    {
      return !(left == right);
    }

    // Token: 0x06000624 RID: 1572 RVA: 0x000F455C File Offset: 0x000F455C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyLearnedHowTo left, ClickyLearnedHowTo right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000625 RID: 1573 RVA: 0x000F4570 File Offset: 0x000F4570
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<LearnedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<JobId>k__BackingField);
    }

    // Token: 0x06000626 RID: 1574 RVA: 0x000F45D4 File Offset: 0x000F45D4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyLearnedHowTo);
    }

    // Token: 0x06000627 RID: 1575 RVA: 0x000F45E4 File Offset: 0x000F45E4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyLearnedHowTo other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<LearnedAt>k__BackingField, other.<LearnedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<JobId>k__BackingField, other.<JobId>k__BackingField));
    }

    // Token: 0x06000629 RID: 1577 RVA: 0x000F4660 File Offset: 0x000F4660
    [CompilerGenerated]
    private ClickyLearnedHowTo(ClickyLearnedHowTo original)
    {
      this.Text = original.<Text>k__BackingField;
      this.LearnedAt = original.<LearnedAt>k__BackingField;
      this.JobId = original.<JobId>k__BackingField;
    }

    // Token: 0x0600062A RID: 1578 RVA: 0x000F468C File Offset: 0x000F468C
    [CompilerGenerated]
    public void Deconstruct(out string Text, out DateTimeOffset LearnedAt, [Nullable(2)] out string JobId)
    {
      Text = this.Text;
      LearnedAt = this.LearnedAt;
      JobId = this.JobId;
    }
  }
}
