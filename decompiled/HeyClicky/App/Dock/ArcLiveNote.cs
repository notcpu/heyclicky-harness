using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.App.Ink;
using HeyClicky.Core.Ink;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002F7 RID: 759
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ArcLiveNote : IEquatable<ArcLiveNote>
  {
    // Token: 0x0600210A RID: 8458 RVA: 0x0061B490 File Offset: 0x0061B490
    public ArcLiveNote(string NotePath, ClickyInkCardSubject Subject, ClickyInkNoteTarget Target, ClickyHomeInkNote Ink)
    {
      this.NotePath = NotePath;
      this.Subject = Subject;
      this.Target = Target;
      this.Ink = Ink;
      base..ctor();
    }

    // Token: 0x1700060C RID: 1548
    // (get) Token: 0x0600210B RID: 8459 RVA: 0x0061B4B8 File Offset: 0x0061B4B8
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ArcLiveNote);
      }
    }

    // Token: 0x1700060D RID: 1549
    // (get) Token: 0x0600210C RID: 8460 RVA: 0x0061B4C4 File Offset: 0x0061B4C4
    // (set) Token: 0x0600210D RID: 8461 RVA: 0x0061B4CC File Offset: 0x0061B4CC
    public string NotePath { get; set; }

    // Token: 0x1700060E RID: 1550
    // (get) Token: 0x0600210E RID: 8462 RVA: 0x0061B4D8 File Offset: 0x0061B4D8
    // (set) Token: 0x0600210F RID: 8463 RVA: 0x0061B4E0 File Offset: 0x0061B4E0
    public ClickyInkCardSubject Subject { get; set; }

    // Token: 0x1700060F RID: 1551
    // (get) Token: 0x06002110 RID: 8464 RVA: 0x0061B4EC File Offset: 0x0061B4EC
    // (set) Token: 0x06002111 RID: 8465 RVA: 0x0061B4F4 File Offset: 0x0061B4F4
    public ClickyInkNoteTarget Target { get; set; }

    // Token: 0x17000610 RID: 1552
    // (get) Token: 0x06002112 RID: 8466 RVA: 0x0061B500 File Offset: 0x0061B500
    // (set) Token: 0x06002113 RID: 8467 RVA: 0x0061B508 File Offset: 0x0061B508
    public ClickyHomeInkNote Ink { get; set; }

    // Token: 0x06002114 RID: 8468 RVA: 0x0061B514 File Offset: 0x0061B514
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ArcLiveNote");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06002115 RID: 8469 RVA: 0x0061B560 File Offset: 0x0061B560
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("NotePath = ");
      builder.Append(this.NotePath);
      builder.Append(", Subject = ");
      builder.Append(this.Subject);
      builder.Append(", Target = ");
      builder.Append(this.Target);
      builder.Append(", Ink = ");
      builder.Append(this.Ink);
      return true;
    }

    // Token: 0x06002116 RID: 8470 RVA: 0x0061B5D8 File Offset: 0x0061B5D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ArcLiveNote left, ArcLiveNote right)
    {
      return !(left == right);
    }

    // Token: 0x06002117 RID: 8471 RVA: 0x0061B5E4 File Offset: 0x0061B5E4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ArcLiveNote left, ArcLiveNote right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06002118 RID: 8472 RVA: 0x0061B5F8 File Offset: 0x0061B5F8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NotePath>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkCardSubject>.Default.GetHashCode(this.<Subject>k__BackingField)) * -1521134295 + EqualityComparer<ClickyInkNoteTarget>.Default.GetHashCode(this.<Target>k__BackingField)) * -1521134295 + EqualityComparer<ClickyHomeInkNote>.Default.GetHashCode(this.<Ink>k__BackingField);
    }

    // Token: 0x06002119 RID: 8473 RVA: 0x0061B674 File Offset: 0x0061B674
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ArcLiveNote);
    }

    // Token: 0x0600211A RID: 8474 RVA: 0x0061B684 File Offset: 0x0061B684
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ArcLiveNote other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<NotePath>k__BackingField, other.<NotePath>k__BackingField) && EqualityComparer<ClickyInkCardSubject>.Default.Equals(this.<Subject>k__BackingField, other.<Subject>k__BackingField) && EqualityComparer<ClickyInkNoteTarget>.Default.Equals(this.<Target>k__BackingField, other.<Target>k__BackingField) && EqualityComparer<ClickyHomeInkNote>.Default.Equals(this.<Ink>k__BackingField, other.<Ink>k__BackingField));
    }

    // Token: 0x0600211C RID: 8476 RVA: 0x0061B718 File Offset: 0x0061B718
    [CompilerGenerated]
    private ArcLiveNote(ArcLiveNote original)
    {
      this.NotePath = original.<NotePath>k__BackingField;
      this.Subject = original.<Subject>k__BackingField;
      this.Target = original.<Target>k__BackingField;
      this.Ink = original.<Ink>k__BackingField;
    }

    // Token: 0x0600211D RID: 8477 RVA: 0x0061B750 File Offset: 0x0061B750
    [CompilerGenerated]
    public void Deconstruct(out string NotePath, out ClickyInkCardSubject Subject, out ClickyInkNoteTarget Target, out ClickyHomeInkNote Ink)
    {
      NotePath = this.NotePath;
      Subject = this.Subject;
      Target = this.Target;
      Ink = this.Ink;
    }
  }
}
