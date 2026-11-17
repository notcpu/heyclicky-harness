using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Drive
{
  // Token: 0x0200018D RID: 397
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentDriveOutcome : IEquatable<AgentDriveOutcome>
  {
    // Token: 0x0600179C RID: 6044 RVA: 0x003C4D48 File Offset: 0x003C4D48
    public AgentDriveOutcome(AgentDriveEnding Ending, [Nullable(2)] string Summary, IReadOnlyList<string> Actions)
    {
      this.Ending = Ending;
      this.Summary = Summary;
      this.Actions = Actions;
      base..ctor();
    }

    // Token: 0x17000554 RID: 1364
    // (get) Token: 0x0600179D RID: 6045 RVA: 0x003C4D68 File Offset: 0x003C4D68
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AgentDriveOutcome);
      }
    }

    // Token: 0x17000555 RID: 1365
    // (get) Token: 0x0600179E RID: 6046 RVA: 0x003C4D74 File Offset: 0x003C4D74
    // (set) Token: 0x0600179F RID: 6047 RVA: 0x003C4D7C File Offset: 0x003C4D7C
    public AgentDriveEnding Ending { get; set; }

    // Token: 0x17000556 RID: 1366
    // (get) Token: 0x060017A0 RID: 6048 RVA: 0x003C4D88 File Offset: 0x003C4D88
    // (set) Token: 0x060017A1 RID: 6049 RVA: 0x003C4D90 File Offset: 0x003C4D90
    [Nullable(2)]
    public string Summary
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x17000557 RID: 1367
    // (get) Token: 0x060017A2 RID: 6050 RVA: 0x003C4D9C File Offset: 0x003C4D9C
    // (set) Token: 0x060017A3 RID: 6051 RVA: 0x003C4DA4 File Offset: 0x003C4DA4
    public IReadOnlyList<string> Actions { get; set; }

    // Token: 0x060017A4 RID: 6052 RVA: 0x003C4DB0 File Offset: 0x003C4DB0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AgentDriveOutcome");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060017A5 RID: 6053 RVA: 0x003C4DFC File Offset: 0x003C4DFC
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Ending = ");
      builder.Append(this.Ending.ToString());
      builder.Append(", Summary = ");
      builder.Append(this.Summary);
      builder.Append(", Actions = ");
      builder.Append(this.Actions);
      return true;
    }

    // Token: 0x060017A6 RID: 6054 RVA: 0x003C4E68 File Offset: 0x003C4E68
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AgentDriveOutcome left, AgentDriveOutcome right)
    {
      return !(left == right);
    }

    // Token: 0x060017A7 RID: 6055 RVA: 0x003C4E74 File Offset: 0x003C4E74
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AgentDriveOutcome left, AgentDriveOutcome right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060017A8 RID: 6056 RVA: 0x003C4E88 File Offset: 0x003C4E88
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<AgentDriveEnding>.Default.GetHashCode(this.<Ending>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Summary>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Actions>k__BackingField);
    }

    // Token: 0x060017A9 RID: 6057 RVA: 0x003C4EEC File Offset: 0x003C4EEC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AgentDriveOutcome);
    }

    // Token: 0x060017AA RID: 6058 RVA: 0x003C4EFC File Offset: 0x003C4EFC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AgentDriveOutcome other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<AgentDriveEnding>.Default.Equals(this.<Ending>k__BackingField, other.<Ending>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Summary>k__BackingField, other.<Summary>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Actions>k__BackingField, other.<Actions>k__BackingField));
    }

    // Token: 0x060017AC RID: 6060 RVA: 0x003C4F78 File Offset: 0x003C4F78
    [CompilerGenerated]
    private AgentDriveOutcome(AgentDriveOutcome original)
    {
      this.Ending = original.<Ending>k__BackingField;
      this.Summary = original.<Summary>k__BackingField;
      this.Actions = original.<Actions>k__BackingField;
    }

    // Token: 0x060017AD RID: 6061 RVA: 0x003C4FA4 File Offset: 0x003C4FA4
    [CompilerGenerated]
    public void Deconstruct(out AgentDriveEnding Ending, [Nullable(2)] out string Summary, out IReadOnlyList<string> Actions)
    {
      Ending = this.Ending;
      Summary = this.Summary;
      Actions = this.Actions;
    }
  }
}
