using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000049 RID: 73
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLiveLine : IEquatable<ClickyLiveLine>
  {
    // Token: 0x0600066A RID: 1642 RVA: 0x000F6A94 File Offset: 0x000F6A94
    public ClickyLiveLine(ClickyLiveLine.LineKind Kind, ClickyInkWorkStep.StepKind? StepKind, string Text, DateTimeOffset At)
    {
      this.Kind = Kind;
      this.StepKind = StepKind;
      this.Text = Text;
      this.At = At;
      this.ShownAt = DateTimeOffset.MinValue;
      this.Icons = Array.Empty<ClickyLiveLineIcon>();
      base..ctor();
    }

    // Token: 0x170001A5 RID: 421
    // (get) Token: 0x0600066B RID: 1643 RVA: 0x000F6AD0 File Offset: 0x000F6AD0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyLiveLine);
      }
    }

    // Token: 0x170001A6 RID: 422
    // (get) Token: 0x0600066C RID: 1644 RVA: 0x000F6ADC File Offset: 0x000F6ADC
    // (set) Token: 0x0600066D RID: 1645 RVA: 0x000F6AE4 File Offset: 0x000F6AE4
    public ClickyLiveLine.LineKind Kind { get; set; }

    // Token: 0x170001A7 RID: 423
    // (get) Token: 0x0600066E RID: 1646 RVA: 0x000F6AF0 File Offset: 0x000F6AF0
    // (set) Token: 0x0600066F RID: 1647 RVA: 0x000F6AF8 File Offset: 0x000F6AF8
    public ClickyInkWorkStep.StepKind? StepKind { get; set; }

    // Token: 0x170001A8 RID: 424
    // (get) Token: 0x06000670 RID: 1648 RVA: 0x000F6B04 File Offset: 0x000F6B04
    // (set) Token: 0x06000671 RID: 1649 RVA: 0x000F6B0C File Offset: 0x000F6B0C
    public string Text { get; set; }

    // Token: 0x170001A9 RID: 425
    // (get) Token: 0x06000672 RID: 1650 RVA: 0x000F6B18 File Offset: 0x000F6B18
    // (set) Token: 0x06000673 RID: 1651 RVA: 0x000F6B20 File Offset: 0x000F6B20
    public DateTimeOffset At { get; set; }

    // Token: 0x170001AA RID: 426
    // (get) Token: 0x06000674 RID: 1652 RVA: 0x000F6B2C File Offset: 0x000F6B2C
    // (set) Token: 0x06000675 RID: 1653 RVA: 0x000F6B34 File Offset: 0x000F6B34
    public DateTimeOffset ShownAt { get; set; }

    // Token: 0x06000676 RID: 1654 RVA: 0x000F6B40 File Offset: 0x000F6B40
    public static ClickyLiveLine FromStep(ClickyInkWorkStep step)
    {
      string @object = step.Object;
      string text = ((@object != null) ? @object.Replace("“", "").Replace("”", "").Replace("…", "")
        .Trim() : null);
      if (string.IsNullOrEmpty(text))
      {
        text = null;
      }
      string text2;
      switch (step.Kind)
      {
      case ClickyInkWorkStep.StepKind.Search:
        text2 = text ?? "the web";
        break;
      case ClickyInkWorkStep.StepKind.Read:
        text2 = text ?? "a page";
        break;
      case ClickyInkWorkStep.StepKind.Look:
        text2 = "looking through the page";
        break;
      case ClickyInkWorkStep.StepKind.Math:
        text2 = "doing the math";
        break;
      case ClickyInkWorkStep.StepKind.Note:
        text2 = "writing the note";
        break;
      case ClickyInkWorkStep.StepKind.File:
        text2 = text ?? "a file";
        break;
      case ClickyInkWorkStep.StepKind.Apps:
        text2 = text ?? "your apps";
        break;
      default:
        text2 = "using your PC";
        break;
      }
      string text3 = text2;
      return new ClickyLiveLine(ClickyLiveLine.LineKind.Step, new ClickyInkWorkStep.StepKind?(step.Kind), text3, step.At);
    }

    // Token: 0x170001AB RID: 427
    // (get) Token: 0x06000677 RID: 1655 RVA: 0x000F6C30 File Offset: 0x000F6C30
    // (set) Token: 0x06000678 RID: 1656 RVA: 0x000F6C38 File Offset: 0x000F6C38
    public IReadOnlyList<ClickyLiveLineIcon> Icons { get; set; }

    // Token: 0x170001AC RID: 428
    // (get) Token: 0x06000679 RID: 1657 RVA: 0x000F6C44 File Offset: 0x000F6C44
    // (set) Token: 0x0600067A RID: 1658 RVA: 0x000F6C4C File Offset: 0x000F6C4C
    public int MoreIcons { get; set; }

    // Token: 0x170001AD RID: 429
    // (get) Token: 0x0600067B RID: 1659 RVA: 0x000F6C58 File Offset: 0x000F6C58
    // (set) Token: 0x0600067C RID: 1660 RVA: 0x000F6C60 File Offset: 0x000F6C60
    [Nullable(2)]
    public string BatchId
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x0600067D RID: 1661 RVA: 0x000F6C6C File Offset: 0x000F6C6C
    [return: Nullable(2)]
    public static ClickyLiveLine FromBatch(ClickyProgramBatch batch, DateTimeOffset at)
    {
      ValueTuple<string, ClickyInkWorkStep.StepKind>? summary = batch.Summary;
      if (summary != null)
      {
        ValueTuple<string, ClickyInkWorkStep.StepKind> valueOrDefault = summary.GetValueOrDefault();
        IReadOnlyList<ClickyLiveLineIcon> icons = batch.Icons;
        return new ClickyLiveLine(ClickyLiveLine.LineKind.Step, new ClickyInkWorkStep.StepKind?(valueOrDefault.Item2), valueOrDefault.Item1, at)
        {
          Icons = icons.Take<ClickyLiveLineIcon>(4).ToList<ClickyLiveLineIcon>(),
          MoreIcons = Math.Max(0, icons.Count - 4) + Math.Max(0, batch.SiteCount - batch.Sites.Count),
          BatchId = batch.Id
        };
      }
      return null;
    }

    // Token: 0x0600067E RID: 1662 RVA: 0x000F6D00 File Offset: 0x000F6D00
    [NullableContext(2)]
    public bool Equals(ClickyLiveLine other)
    {
      if (other != null && this.Kind == other.Kind)
      {
        ClickyInkWorkStep.StepKind? stepKind = this.StepKind;
        ClickyInkWorkStep.StepKind? stepKind2 = other.StepKind;
        if (((stepKind.GetValueOrDefault() == stepKind2.GetValueOrDefault()) & (stepKind != null == (stepKind2 != null))) && this.Text == other.Text && this.ShownAt == other.ShownAt && this.Icons.SequenceEqual<ClickyLiveLineIcon>(other.Icons))
        {
          return this.MoreIcons == other.MoreIcons;
        }
      }
      return false;
    }

    // Token: 0x0600067F RID: 1663 RVA: 0x000F6D9C File Offset: 0x000F6D9C
    public override int GetHashCode()
    {
      return HashCode.Combine<ClickyLiveLine.LineKind, ClickyInkWorkStep.StepKind?, string, DateTimeOffset, int, int>(this.Kind, this.StepKind, this.Text, this.ShownAt, this.Icons.Count, this.MoreIcons);
    }

    // Token: 0x06000680 RID: 1664 RVA: 0x000F6DCC File Offset: 0x000F6DCC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyLiveLine");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000681 RID: 1665 RVA: 0x000F6E18 File Offset: 0x000F6E18
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", StepKind = ");
      builder.Append(this.StepKind.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", At = ");
      builder.Append(this.At.ToString());
      builder.Append(", ShownAt = ");
      builder.Append(this.ShownAt.ToString());
      builder.Append(", Icons = ");
      builder.Append(this.Icons);
      builder.Append(", MoreIcons = ");
      builder.Append(this.MoreIcons.ToString());
      builder.Append(", BatchId = ");
      builder.Append(this.BatchId);
      return true;
    }

    // Token: 0x06000682 RID: 1666 RVA: 0x000F6F3C File Offset: 0x000F6F3C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyLiveLine left, ClickyLiveLine right)
    {
      return !(left == right);
    }

    // Token: 0x06000683 RID: 1667 RVA: 0x000F6F48 File Offset: 0x000F6F48
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyLiveLine left, ClickyLiveLine right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000684 RID: 1668 RVA: 0x000F6F5C File Offset: 0x000F6F5C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyLiveLine);
    }

    // Token: 0x06000686 RID: 1670 RVA: 0x000F6F74 File Offset: 0x000F6F74
    [CompilerGenerated]
    private ClickyLiveLine(ClickyLiveLine original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.StepKind = original.<StepKind>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.At = original.<At>k__BackingField;
      this.ShownAt = original.<ShownAt>k__BackingField;
      this.Icons = original.<Icons>k__BackingField;
      this.MoreIcons = original.<MoreIcons>k__BackingField;
      this.BatchId = original.<BatchId>k__BackingField;
    }

    // Token: 0x06000687 RID: 1671 RVA: 0x000F6FE8 File Offset: 0x000F6FE8
    [CompilerGenerated]
    public void Deconstruct(out ClickyLiveLine.LineKind Kind, out ClickyInkWorkStep.StepKind? StepKind, out string Text, out DateTimeOffset At)
    {
      Kind = this.Kind;
      StepKind = this.StepKind;
      Text = this.Text;
      At = this.At;
    }

    // Token: 0x02000176 RID: 374
    [NullableContext(0)]
    public enum LineKind
    {
      // Token: 0x04000753 RID: 1875
      Thinking,
      // Token: 0x04000754 RID: 1876
      Step
    }
  }
}
