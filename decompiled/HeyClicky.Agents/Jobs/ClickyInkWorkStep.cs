using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000048 RID: 72
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkWorkStep : IEquatable<ClickyInkWorkStep>
  {
    // Token: 0x06000654 RID: 1620 RVA: 0x000F6588 File Offset: 0x000F6588
    [NullableContext(2)]
    public ClickyInkWorkStep(ClickyInkWorkStep.StepKind Kind, string Object, DateTimeOffset At)
    {
      this.Kind = Kind;
      this.Object = Object;
      this.At = At;
      base..ctor();
    }

    // Token: 0x1700019E RID: 414
    // (get) Token: 0x06000655 RID: 1621 RVA: 0x000F65A8 File Offset: 0x000F65A8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkWorkStep);
      }
    }

    // Token: 0x1700019F RID: 415
    // (get) Token: 0x06000656 RID: 1622 RVA: 0x000F65B4 File Offset: 0x000F65B4
    // (set) Token: 0x06000657 RID: 1623 RVA: 0x000F65BC File Offset: 0x000F65BC
    public ClickyInkWorkStep.StepKind Kind { get; set; }

    // Token: 0x170001A0 RID: 416
    // (get) Token: 0x06000658 RID: 1624 RVA: 0x000F65C8 File Offset: 0x000F65C8
    // (set) Token: 0x06000659 RID: 1625 RVA: 0x000F65D0 File Offset: 0x000F65D0
    [Nullable(2)]
    public string Object
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001A1 RID: 417
    // (get) Token: 0x0600065A RID: 1626 RVA: 0x000F65DC File Offset: 0x000F65DC
    // (set) Token: 0x0600065B RID: 1627 RVA: 0x000F65E4 File Offset: 0x000F65E4
    public DateTimeOffset At { get; set; }

    // Token: 0x170001A2 RID: 418
    // (get) Token: 0x0600065C RID: 1628 RVA: 0x000F65F0 File Offset: 0x000F65F0
    public string Present
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case ClickyInkWorkStep.StepKind.Search:
          text = "searching " + (this.Object ?? "the web");
          break;
        case ClickyInkWorkStep.StepKind.Read:
          text = "reading " + (this.Object ?? "a page");
          break;
        case ClickyInkWorkStep.StepKind.Look:
          text = "looking through the page";
          break;
        case ClickyInkWorkStep.StepKind.Math:
          text = "doing the math";
          break;
        case ClickyInkWorkStep.StepKind.Note:
          text = "writing the note";
          break;
        case ClickyInkWorkStep.StepKind.File:
          text = "working on " + (this.Object ?? "a file");
          break;
        case ClickyInkWorkStep.StepKind.Apps:
          text = "checking " + (this.Object ?? "your apps");
          break;
        default:
          text = "using your PC";
          break;
        }
        return text;
      }
    }

    // Token: 0x170001A3 RID: 419
    // (get) Token: 0x0600065D RID: 1629 RVA: 0x000F66BC File Offset: 0x000F66BC
    public string Past
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case ClickyInkWorkStep.StepKind.Search:
          text = "searched " + (this.Object ?? "the web");
          break;
        case ClickyInkWorkStep.StepKind.Read:
          text = "read " + (this.Object ?? "a page");
          break;
        case ClickyInkWorkStep.StepKind.Look:
          text = "looked through the page";
          break;
        case ClickyInkWorkStep.StepKind.Math:
          text = "did the math";
          break;
        case ClickyInkWorkStep.StepKind.Note:
          text = "wrote the note";
          break;
        case ClickyInkWorkStep.StepKind.File:
          text = "worked on " + (this.Object ?? "a file");
          break;
        case ClickyInkWorkStep.StepKind.Apps:
          text = "checked " + (this.Object ?? "your apps");
          break;
        default:
          text = "used your PC";
          break;
        }
        return text;
      }
    }

    // Token: 0x170001A4 RID: 420
    // (get) Token: 0x0600065E RID: 1630 RVA: 0x000F6788 File Offset: 0x000F6788
    public string Symbol
    {
      get
      {
        string text;
        switch (this.Kind)
        {
        case ClickyInkWorkStep.StepKind.Search:
          text = "magnifyingglass";
          break;
        case ClickyInkWorkStep.StepKind.Read:
          text = "doc.text";
          break;
        case ClickyInkWorkStep.StepKind.Look:
          text = "text.magnifyingglass";
          break;
        case ClickyInkWorkStep.StepKind.Math:
          text = "function";
          break;
        case ClickyInkWorkStep.StepKind.Note:
          text = "pencil.line";
          break;
        case ClickyInkWorkStep.StepKind.File:
          text = "doc";
          break;
        case ClickyInkWorkStep.StepKind.Apps:
          text = "square.grid.2x2";
          break;
        default:
          text = "cursorarrow";
          break;
        }
        return text;
      }
    }

    // Token: 0x0600065F RID: 1631 RVA: 0x000F6800 File Offset: 0x000F6800
    public bool IsSame(ClickyInkWorkStep other)
    {
      return this.Kind == other.Kind && this.Object == other.Object;
    }

    // Token: 0x06000660 RID: 1632 RVA: 0x000F6824 File Offset: 0x000F6824
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkWorkStep");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000661 RID: 1633 RVA: 0x000F6870 File Offset: 0x000F6870
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Object = ");
      builder.Append(this.Object);
      builder.Append(", At = ");
      builder.Append(this.At.ToString());
      builder.Append(", Present = ");
      builder.Append(this.Present);
      builder.Append(", Past = ");
      builder.Append(this.Past);
      builder.Append(", Symbol = ");
      builder.Append(this.Symbol);
      return true;
    }

    // Token: 0x06000662 RID: 1634 RVA: 0x000F6938 File Offset: 0x000F6938
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkWorkStep left, ClickyInkWorkStep right)
    {
      return !(left == right);
    }

    // Token: 0x06000663 RID: 1635 RVA: 0x000F6944 File Offset: 0x000F6944
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkWorkStep left, ClickyInkWorkStep right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000664 RID: 1636 RVA: 0x000F6958 File Offset: 0x000F6958
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkWorkStep.StepKind>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Object>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField);
    }

    // Token: 0x06000665 RID: 1637 RVA: 0x000F69BC File Offset: 0x000F69BC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkWorkStep);
    }

    // Token: 0x06000666 RID: 1638 RVA: 0x000F69CC File Offset: 0x000F69CC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkWorkStep other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkWorkStep.StepKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Object>k__BackingField, other.<Object>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
    }

    // Token: 0x06000668 RID: 1640 RVA: 0x000F6A48 File Offset: 0x000F6A48
    [CompilerGenerated]
    private ClickyInkWorkStep(ClickyInkWorkStep original)
    {
      this.Kind = original.<Kind>k__BackingField;
      this.Object = original.<Object>k__BackingField;
      this.At = original.<At>k__BackingField;
    }

    // Token: 0x06000669 RID: 1641 RVA: 0x000F6A74 File Offset: 0x000F6A74
    [NullableContext(2)]
    [CompilerGenerated]
    public void Deconstruct(out ClickyInkWorkStep.StepKind Kind, out string Object, out DateTimeOffset At)
    {
      Kind = this.Kind;
      Object = this.Object;
      At = this.At;
    }

    // Token: 0x04000260 RID: 608
    public const int Kept = 12;

    // Token: 0x02000175 RID: 373
    [NullableContext(0)]
    public enum StepKind
    {
      // Token: 0x0400074A RID: 1866
      Search,
      // Token: 0x0400074B RID: 1867
      Read,
      // Token: 0x0400074C RID: 1868
      Look,
      // Token: 0x0400074D RID: 1869
      Math,
      // Token: 0x0400074E RID: 1870
      Note,
      // Token: 0x0400074F RID: 1871
      File,
      // Token: 0x04000750 RID: 1872
      Apps,
      // Token: 0x04000751 RID: 1873
      Mac
    }
  }
}
