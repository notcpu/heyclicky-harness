using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200003E RID: 62
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyJobMessageRoute : IEquatable<ClickyJobMessageRoute>
  {
    // Token: 0x060005B4 RID: 1460 RVA: 0x000F23C0 File Offset: 0x000F23C0
    public ClickyJobMessageRoute(string TargetThreadId, string NewRunFollowsThreadId, bool ResumesFinishedRun = false)
    {
      this.TargetThreadId = TargetThreadId;
      this.NewRunFollowsThreadId = NewRunFollowsThreadId;
      this.ResumesFinishedRun = ResumesFinishedRun;
      base..ctor();
    }

    // Token: 0x17000188 RID: 392
    // (get) Token: 0x060005B5 RID: 1461 RVA: 0x000F23E0 File Offset: 0x000F23E0
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyJobMessageRoute);
      }
    }

    // Token: 0x17000189 RID: 393
    // (get) Token: 0x060005B6 RID: 1462 RVA: 0x000F23EC File Offset: 0x000F23EC
    // (set) Token: 0x060005B7 RID: 1463 RVA: 0x000F23F4 File Offset: 0x000F23F4
    public string TargetThreadId { get; set; }

    // Token: 0x1700018A RID: 394
    // (get) Token: 0x060005B8 RID: 1464 RVA: 0x000F2400 File Offset: 0x000F2400
    // (set) Token: 0x060005B9 RID: 1465 RVA: 0x000F2408 File Offset: 0x000F2408
    public string NewRunFollowsThreadId { get; set; }

    // Token: 0x1700018B RID: 395
    // (get) Token: 0x060005BA RID: 1466 RVA: 0x000F2414 File Offset: 0x000F2414
    // (set) Token: 0x060005BB RID: 1467 RVA: 0x000F241C File Offset: 0x000F241C
    public bool ResumesFinishedRun { get; set; }

    // Token: 0x060005BC RID: 1468 RVA: 0x000F2428 File Offset: 0x000F2428
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyJobMessageRoute");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060005BD RID: 1469 RVA: 0x000F2474 File Offset: 0x000F2474
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("TargetThreadId = ");
      builder.Append(this.TargetThreadId);
      builder.Append(", NewRunFollowsThreadId = ");
      builder.Append(this.NewRunFollowsThreadId);
      builder.Append(", ResumesFinishedRun = ");
      builder.Append(this.ResumesFinishedRun.ToString());
      return true;
    }

    // Token: 0x060005BE RID: 1470 RVA: 0x000F24E0 File Offset: 0x000F24E0
    [CompilerGenerated]
    public static bool operator !=(ClickyJobMessageRoute left, ClickyJobMessageRoute right)
    {
      return !(left == right);
    }

    // Token: 0x060005BF RID: 1471 RVA: 0x000F24EC File Offset: 0x000F24EC
    [CompilerGenerated]
    public static bool operator ==(ClickyJobMessageRoute left, ClickyJobMessageRoute right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060005C0 RID: 1472 RVA: 0x000F2500 File Offset: 0x000F2500
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TargetThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NewRunFollowsThreadId>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<ResumesFinishedRun>k__BackingField);
    }

    // Token: 0x060005C1 RID: 1473 RVA: 0x000F2564 File Offset: 0x000F2564
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyJobMessageRoute);
    }

    // Token: 0x060005C2 RID: 1474 RVA: 0x000F2574 File Offset: 0x000F2574
    [CompilerGenerated]
    public bool Equals(ClickyJobMessageRoute other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<TargetThreadId>k__BackingField, other.<TargetThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NewRunFollowsThreadId>k__BackingField, other.<NewRunFollowsThreadId>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<ResumesFinishedRun>k__BackingField, other.<ResumesFinishedRun>k__BackingField));
    }

    // Token: 0x060005C4 RID: 1476 RVA: 0x000F25F0 File Offset: 0x000F25F0
    [CompilerGenerated]
    private ClickyJobMessageRoute([Nullable(1)] ClickyJobMessageRoute original)
    {
      this.TargetThreadId = original.<TargetThreadId>k__BackingField;
      this.NewRunFollowsThreadId = original.<NewRunFollowsThreadId>k__BackingField;
      this.ResumesFinishedRun = original.<ResumesFinishedRun>k__BackingField;
    }

    // Token: 0x060005C5 RID: 1477 RVA: 0x000F261C File Offset: 0x000F261C
    [CompilerGenerated]
    public void Deconstruct(out string TargetThreadId, out string NewRunFollowsThreadId, out bool ResumesFinishedRun)
    {
      TargetThreadId = this.TargetThreadId;
      NewRunFollowsThreadId = this.NewRunFollowsThreadId;
      ResumesFinishedRun = this.ResumesFinishedRun;
    }
  }
}
