using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200016A RID: 362
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyHomeInkNote : IEquatable<ClickyHomeInkNote>
  {
    // Token: 0x060015EF RID: 5615 RVA: 0x003BAC18 File Offset: 0x003BAC18
    public ClickyHomeInkNote(ClickyInkLiveNote Note, DateTimeOffset StartedAt)
    {
      this.Note = Note;
      this.StartedAt = StartedAt;
      base..ctor();
    }

    // Token: 0x17000507 RID: 1287
    // (get) Token: 0x060015F0 RID: 5616 RVA: 0x003BAC30 File Offset: 0x003BAC30
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyHomeInkNote);
      }
    }

    // Token: 0x17000508 RID: 1288
    // (get) Token: 0x060015F1 RID: 5617 RVA: 0x003BAC3C File Offset: 0x003BAC3C
    // (set) Token: 0x060015F2 RID: 5618 RVA: 0x003BAC44 File Offset: 0x003BAC44
    public ClickyInkLiveNote Note { get; set; }

    // Token: 0x17000509 RID: 1289
    // (get) Token: 0x060015F3 RID: 5619 RVA: 0x003BAC50 File Offset: 0x003BAC50
    // (set) Token: 0x060015F4 RID: 5620 RVA: 0x003BAC58 File Offset: 0x003BAC58
    public DateTimeOffset StartedAt { get; set; }

    // Token: 0x060015F5 RID: 5621 RVA: 0x003BAC64 File Offset: 0x003BAC64
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyHomeInkNote");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060015F6 RID: 5622 RVA: 0x003BACB0 File Offset: 0x003BACB0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Note = ");
      builder.Append(this.Note);
      builder.Append(", StartedAt = ");
      builder.Append(this.StartedAt.ToString());
      return true;
    }

    // Token: 0x060015F7 RID: 5623 RVA: 0x003BAD04 File Offset: 0x003BAD04
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyHomeInkNote left, ClickyHomeInkNote right)
    {
      return !(left == right);
    }

    // Token: 0x060015F8 RID: 5624 RVA: 0x003BAD10 File Offset: 0x003BAD10
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyHomeInkNote left, ClickyHomeInkNote right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060015F9 RID: 5625 RVA: 0x003BAD24 File Offset: 0x003BAD24
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<ClickyInkLiveNote>.Default.GetHashCode(this.<Note>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<StartedAt>k__BackingField);
    }

    // Token: 0x060015FA RID: 5626 RVA: 0x003BAD64 File Offset: 0x003BAD64
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyHomeInkNote);
    }

    // Token: 0x060015FB RID: 5627 RVA: 0x003BAD74 File Offset: 0x003BAD74
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyHomeInkNote other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<ClickyInkLiveNote>.Default.Equals(this.<Note>k__BackingField, other.<Note>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField));
    }

    // Token: 0x060015FD RID: 5629 RVA: 0x003BADD8 File Offset: 0x003BADD8
    [CompilerGenerated]
    private ClickyHomeInkNote(ClickyHomeInkNote original)
    {
      this.Note = original.<Note>k__BackingField;
      this.StartedAt = original.<StartedAt>k__BackingField;
    }

    // Token: 0x060015FE RID: 5630 RVA: 0x003BADF8 File Offset: 0x003BADF8
    [CompilerGenerated]
    public void Deconstruct(out ClickyInkLiveNote Note, out DateTimeOffset StartedAt)
    {
      Note = this.Note;
      StartedAt = this.StartedAt;
    }
  }
}
