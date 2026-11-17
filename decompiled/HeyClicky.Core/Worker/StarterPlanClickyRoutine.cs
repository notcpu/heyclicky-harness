using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000052 RID: 82
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class StarterPlanClickyRoutine : IEquatable<StarterPlanClickyRoutine>
  {
    // Token: 0x060003C3 RID: 963 RVA: 0x00354684 File Offset: 0x00354684
    public StarterPlanClickyRoutine(string Title, string TaskPrompt, int? IntervalMinutes)
    {
      this.Title = Title;
      this.TaskPrompt = TaskPrompt;
      this.IntervalMinutes = IntervalMinutes;
      base..ctor();
    }

    // Token: 0x170000D2 RID: 210
    // (get) Token: 0x060003C4 RID: 964 RVA: 0x003546A4 File Offset: 0x003546A4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(StarterPlanClickyRoutine);
      }
    }

    // Token: 0x170000D3 RID: 211
    // (get) Token: 0x060003C5 RID: 965 RVA: 0x003546B0 File Offset: 0x003546B0
    // (set) Token: 0x060003C6 RID: 966 RVA: 0x003546B8 File Offset: 0x003546B8
    public string Title { get; set; }

    // Token: 0x170000D4 RID: 212
    // (get) Token: 0x060003C7 RID: 967 RVA: 0x003546C4 File Offset: 0x003546C4
    // (set) Token: 0x060003C8 RID: 968 RVA: 0x003546CC File Offset: 0x003546CC
    public string TaskPrompt { get; set; }

    // Token: 0x170000D5 RID: 213
    // (get) Token: 0x060003C9 RID: 969 RVA: 0x003546D8 File Offset: 0x003546D8
    // (set) Token: 0x060003CA RID: 970 RVA: 0x003546E0 File Offset: 0x003546E0
    public int? IntervalMinutes { get; set; }

    // Token: 0x060003CB RID: 971 RVA: 0x003546EC File Offset: 0x003546EC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("StarterPlanClickyRoutine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060003CC RID: 972 RVA: 0x00354738 File Offset: 0x00354738
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", TaskPrompt = ");
      builder.Append(this.TaskPrompt);
      builder.Append(", IntervalMinutes = ");
      builder.Append(this.IntervalMinutes.ToString());
      return true;
    }

    // Token: 0x060003CD RID: 973 RVA: 0x003547A4 File Offset: 0x003547A4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(StarterPlanClickyRoutine left, StarterPlanClickyRoutine right)
    {
      return !(left == right);
    }

    // Token: 0x060003CE RID: 974 RVA: 0x003547B0 File Offset: 0x003547B0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(StarterPlanClickyRoutine left, StarterPlanClickyRoutine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003CF RID: 975 RVA: 0x003547C4 File Offset: 0x003547C4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TaskPrompt>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<IntervalMinutes>k__BackingField);
    }

    // Token: 0x060003D0 RID: 976 RVA: 0x00354828 File Offset: 0x00354828
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as StarterPlanClickyRoutine);
    }

    // Token: 0x060003D1 RID: 977 RVA: 0x00354838 File Offset: 0x00354838
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(StarterPlanClickyRoutine other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TaskPrompt>k__BackingField, other.<TaskPrompt>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<IntervalMinutes>k__BackingField, other.<IntervalMinutes>k__BackingField));
    }

    // Token: 0x060003D3 RID: 979 RVA: 0x003548B4 File Offset: 0x003548B4
    [CompilerGenerated]
    private StarterPlanClickyRoutine(StarterPlanClickyRoutine original)
    {
      this.Title = original.<Title>k__BackingField;
      this.TaskPrompt = original.<TaskPrompt>k__BackingField;
      this.IntervalMinutes = original.<IntervalMinutes>k__BackingField;
    }

    // Token: 0x060003D4 RID: 980 RVA: 0x003548E0 File Offset: 0x003548E0
    [CompilerGenerated]
    public void Deconstruct(out string Title, out string TaskPrompt, out int? IntervalMinutes)
    {
      Title = this.Title;
      TaskPrompt = this.TaskPrompt;
      IntervalMinutes = this.IntervalMinutes;
    }
  }
}
