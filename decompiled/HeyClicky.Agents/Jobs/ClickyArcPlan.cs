using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004C RID: 76
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyArcPlan : IEquatable<ClickyArcPlan>
  {
    // Token: 0x060006C5 RID: 1733 RVA: 0x000F823C File Offset: 0x000F823C
    public ClickyArcPlan(IReadOnlyList<ClickyArcPlan.Step> Steps)
    {
      this.Steps = Steps;
      base..ctor();
    }

    // Token: 0x170001BF RID: 447
    // (get) Token: 0x060006C6 RID: 1734 RVA: 0x000F824C File Offset: 0x000F824C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyArcPlan);
      }
    }

    // Token: 0x170001C0 RID: 448
    // (get) Token: 0x060006C7 RID: 1735 RVA: 0x000F8258 File Offset: 0x000F8258
    // (set) Token: 0x060006C8 RID: 1736 RVA: 0x000F8260 File Offset: 0x000F8260
    public IReadOnlyList<ClickyArcPlan.Step> Steps { get; set; }

    // Token: 0x170001C1 RID: 449
    // (get) Token: 0x060006C9 RID: 1737 RVA: 0x000F826C File Offset: 0x000F826C
    public int DoneCount
    {
      get
      {
        return this.Steps.Count<ClickyArcPlan.Step>((ClickyArcPlan.Step s) => s.IsDone);
      }
    }

    // Token: 0x170001C2 RID: 450
    // (get) Token: 0x060006CA RID: 1738 RVA: 0x000F8298 File Offset: 0x000F8298
    public int? CurrentIndex
    {
      get
      {
        for (int i = 0; i < this.Steps.Count; i++)
        {
          if (this.Steps[i].IsNow && !this.Steps[i].IsDone)
          {
            return new int?(i);
          }
        }
        for (int j = 0; j < this.Steps.Count; j++)
        {
          if (!this.Steps[j].IsDone)
          {
            return new int?(j);
          }
        }
        return null;
      }
    }

    // Token: 0x170001C3 RID: 451
    // (get) Token: 0x060006CB RID: 1739 RVA: 0x000F8324 File Offset: 0x000F8324
    public bool IsEmpty
    {
      get
      {
        return this.Steps.Count == 0;
      }
    }

    // Token: 0x060006CC RID: 1740 RVA: 0x000F8334 File Offset: 0x000F8334
    public static ClickyArcPlan FromPlanTool(IEnumerable<AgentPlanStep> steps)
    {
      return new ClickyArcPlan(steps.Select<AgentPlanStep, ClickyArcPlan.Step>((AgentPlanStep s) => new ClickyArcPlan.Step(s.Text, s.IsDone, s.IsNow)).ToList<ClickyArcPlan.Step>());
    }

    // Token: 0x170001C4 RID: 452
    // (get) Token: 0x060006CD RID: 1741 RVA: 0x000F8368 File Offset: 0x000F8368
    public ClickyArcPlan Finalizing
    {
      get
      {
        return new ClickyArcPlan(this.Steps.Select<ClickyArcPlan.Step, ClickyArcPlan.Step>((ClickyArcPlan.Step s) => new ClickyArcPlan.Step(s.Text, true, false)).Append(new ClickyArcPlan.Step("Finalizing", false, true)).ToList<ClickyArcPlan.Step>());
      }
    }

    // Token: 0x060006CE RID: 1742 RVA: 0x000F83BC File Offset: 0x000F83BC
    [NullableContext(2)]
    public bool Equals(ClickyArcPlan other)
    {
      return other != null && this.Steps.SequenceEqual<ClickyArcPlan.Step>(other.Steps);
    }

    // Token: 0x060006CF RID: 1743 RVA: 0x000F83D4 File Offset: 0x000F83D4
    public override int GetHashCode()
    {
      return this.Steps.Aggregate(0, (int h, ClickyArcPlan.Step s) => HashCode.Combine<int, ClickyArcPlan.Step>(h, s));
    }

    // Token: 0x060006D0 RID: 1744 RVA: 0x000F8404 File Offset: 0x000F8404
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyArcPlan");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060006D1 RID: 1745 RVA: 0x000F8450 File Offset: 0x000F8450
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Steps = ");
      builder.Append(this.Steps);
      builder.Append(", DoneCount = ");
      builder.Append(this.DoneCount.ToString());
      builder.Append(", CurrentIndex = ");
      builder.Append(this.CurrentIndex.ToString());
      builder.Append(", IsEmpty = ");
      builder.Append(this.IsEmpty.ToString());
      builder.Append(", Finalizing = ");
      builder.Append(this.Finalizing);
      return true;
    }

    // Token: 0x060006D2 RID: 1746 RVA: 0x000F850C File Offset: 0x000F850C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyArcPlan left, ClickyArcPlan right)
    {
      return !(left == right);
    }

    // Token: 0x060006D3 RID: 1747 RVA: 0x000F8518 File Offset: 0x000F8518
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyArcPlan left, ClickyArcPlan right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060006D4 RID: 1748 RVA: 0x000F852C File Offset: 0x000F852C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyArcPlan);
    }

    // Token: 0x060006D6 RID: 1750 RVA: 0x000F8544 File Offset: 0x000F8544
    [CompilerGenerated]
    private ClickyArcPlan(ClickyArcPlan original)
    {
      this.Steps = original.<Steps>k__BackingField;
    }

    // Token: 0x060006D7 RID: 1751 RVA: 0x000F8558 File Offset: 0x000F8558
    [CompilerGenerated]
    public void Deconstruct(out IReadOnlyList<ClickyArcPlan.Step> Steps)
    {
      Steps = this.Steps;
    }

    // Token: 0x04000277 RID: 631
    public const string FinalizingStepText = "Finalizing";

    // Token: 0x0200017D RID: 381
    [Nullable(0)]
    public sealed class Step : IEquatable<ClickyArcPlan.Step>
    {
      // Token: 0x06000D83 RID: 3459 RVA: 0x001158F0 File Offset: 0x001158F0
      public Step(string Text, bool IsDone, bool IsNow = false)
      {
        this.Text = Text;
        this.IsDone = IsDone;
        this.IsNow = IsNow;
        base..ctor();
      }

      // Token: 0x170002C7 RID: 711
      // (get) Token: 0x06000D84 RID: 3460 RVA: 0x00115910 File Offset: 0x00115910
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyArcPlan.Step);
        }
      }

      // Token: 0x170002C8 RID: 712
      // (get) Token: 0x06000D85 RID: 3461 RVA: 0x0011591C File Offset: 0x0011591C
      // (set) Token: 0x06000D86 RID: 3462 RVA: 0x00115924 File Offset: 0x00115924
      public string Text { get; set; }

      // Token: 0x170002C9 RID: 713
      // (get) Token: 0x06000D87 RID: 3463 RVA: 0x00115930 File Offset: 0x00115930
      // (set) Token: 0x06000D88 RID: 3464 RVA: 0x00115938 File Offset: 0x00115938
      public bool IsDone { get; set; }

      // Token: 0x170002CA RID: 714
      // (get) Token: 0x06000D89 RID: 3465 RVA: 0x00115944 File Offset: 0x00115944
      // (set) Token: 0x06000D8A RID: 3466 RVA: 0x0011594C File Offset: 0x0011594C
      public bool IsNow { get; set; }

      // Token: 0x06000D8B RID: 3467 RVA: 0x00115958 File Offset: 0x00115958
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Step");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06000D8C RID: 3468 RVA: 0x001159A4 File Offset: 0x001159A4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Text = ");
        builder.Append(this.Text);
        builder.Append(", IsDone = ");
        builder.Append(this.IsDone.ToString());
        builder.Append(", IsNow = ");
        builder.Append(this.IsNow.ToString());
        return true;
      }

      // Token: 0x06000D8D RID: 3469 RVA: 0x00115A20 File Offset: 0x00115A20
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyArcPlan.Step left, ClickyArcPlan.Step right)
      {
        return !(left == right);
      }

      // Token: 0x06000D8E RID: 3470 RVA: 0x00115A2C File Offset: 0x00115A2C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyArcPlan.Step left, ClickyArcPlan.Step right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06000D8F RID: 3471 RVA: 0x00115A40 File Offset: 0x00115A40
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsDone>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsNow>k__BackingField);
      }

      // Token: 0x06000D90 RID: 3472 RVA: 0x00115AA4 File Offset: 0x00115AA4
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyArcPlan.Step);
      }

      // Token: 0x06000D91 RID: 3473 RVA: 0x00115AB4 File Offset: 0x00115AB4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyArcPlan.Step other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsDone>k__BackingField, other.<IsDone>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsNow>k__BackingField, other.<IsNow>k__BackingField));
      }

      // Token: 0x06000D93 RID: 3475 RVA: 0x00115B30 File Offset: 0x00115B30
      [CompilerGenerated]
      private Step(ClickyArcPlan.Step original)
      {
        this.Text = original.<Text>k__BackingField;
        this.IsDone = original.<IsDone>k__BackingField;
        this.IsNow = original.<IsNow>k__BackingField;
      }

      // Token: 0x06000D94 RID: 3476 RVA: 0x00115B5C File Offset: 0x00115B5C
      [CompilerGenerated]
      public void Deconstruct(out string Text, out bool IsDone, out bool IsNow)
      {
        Text = this.Text;
        IsDone = this.IsDone;
        IsNow = this.IsNow;
      }
    }
  }
}
