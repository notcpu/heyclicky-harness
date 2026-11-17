using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Ink
{
  // Token: 0x02000169 RID: 361
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyInkNoteVersion : IEquatable<ClickyInkNoteVersion>
  {
    // Token: 0x060015D9 RID: 5593 RVA: 0x003BA86C File Offset: 0x003BA86C
    public ClickyInkNoteVersion(DateTimeOffset At, string Text, bool IsEdit, bool? IsPartial = null, bool? StartsNewEdit = null)
    {
      this.At = At;
      this.Text = Text;
      this.IsEdit = IsEdit;
      this.IsPartial = IsPartial;
      this.StartsNewEdit = StartsNewEdit;
      base..ctor();
    }

    // Token: 0x17000501 RID: 1281
    // (get) Token: 0x060015DA RID: 5594 RVA: 0x003BA89C File Offset: 0x003BA89C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClickyInkNoteVersion);
      }
    }

    // Token: 0x17000502 RID: 1282
    // (get) Token: 0x060015DB RID: 5595 RVA: 0x003BA8A8 File Offset: 0x003BA8A8
    // (set) Token: 0x060015DC RID: 5596 RVA: 0x003BA8B0 File Offset: 0x003BA8B0
    public DateTimeOffset At { get; set; }

    // Token: 0x17000503 RID: 1283
    // (get) Token: 0x060015DD RID: 5597 RVA: 0x003BA8BC File Offset: 0x003BA8BC
    // (set) Token: 0x060015DE RID: 5598 RVA: 0x003BA8C4 File Offset: 0x003BA8C4
    public string Text { get; set; }

    // Token: 0x17000504 RID: 1284
    // (get) Token: 0x060015DF RID: 5599 RVA: 0x003BA8D0 File Offset: 0x003BA8D0
    // (set) Token: 0x060015E0 RID: 5600 RVA: 0x003BA8D8 File Offset: 0x003BA8D8
    public bool IsEdit { get; set; }

    // Token: 0x17000505 RID: 1285
    // (get) Token: 0x060015E1 RID: 5601 RVA: 0x003BA8E4 File Offset: 0x003BA8E4
    // (set) Token: 0x060015E2 RID: 5602 RVA: 0x003BA8EC File Offset: 0x003BA8EC
    public bool? IsPartial { get; set; }

    // Token: 0x17000506 RID: 1286
    // (get) Token: 0x060015E3 RID: 5603 RVA: 0x003BA8F8 File Offset: 0x003BA8F8
    // (set) Token: 0x060015E4 RID: 5604 RVA: 0x003BA900 File Offset: 0x003BA900
    public bool? StartsNewEdit { get; set; }

    // Token: 0x060015E5 RID: 5605 RVA: 0x003BA90C File Offset: 0x003BA90C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyInkNoteVersion");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060015E6 RID: 5606 RVA: 0x003BA958 File Offset: 0x003BA958
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("At = ");
      builder.Append(this.At.ToString());
      builder.Append(", Text = ");
      builder.Append(this.Text);
      builder.Append(", IsEdit = ");
      builder.Append(this.IsEdit.ToString());
      builder.Append(", IsPartial = ");
      builder.Append(this.IsPartial.ToString());
      builder.Append(", StartsNewEdit = ");
      builder.Append(this.StartsNewEdit.ToString());
      return true;
    }

    // Token: 0x060015E7 RID: 5607 RVA: 0x003BAA20 File Offset: 0x003BAA20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClickyInkNoteVersion left, ClickyInkNoteVersion right)
    {
      return !(left == right);
    }

    // Token: 0x060015E8 RID: 5608 RVA: 0x003BAA2C File Offset: 0x003BAA2C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClickyInkNoteVersion left, ClickyInkNoteVersion right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060015E9 RID: 5609 RVA: 0x003BAA40 File Offset: 0x003BAA40
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsEdit>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<IsPartial>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<StartsNewEdit>k__BackingField);
    }

    // Token: 0x060015EA RID: 5610 RVA: 0x003BAAD0 File Offset: 0x003BAAD0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyInkNoteVersion);
    }

    // Token: 0x060015EB RID: 5611 RVA: 0x003BAAE0 File Offset: 0x003BAAE0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClickyInkNoteVersion other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsEdit>k__BackingField, other.<IsEdit>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<IsPartial>k__BackingField, other.<IsPartial>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<StartsNewEdit>k__BackingField, other.<StartsNewEdit>k__BackingField));
    }

    // Token: 0x060015ED RID: 5613 RVA: 0x003BAB90 File Offset: 0x003BAB90
    [CompilerGenerated]
    private ClickyInkNoteVersion(ClickyInkNoteVersion original)
    {
      this.At = original.<At>k__BackingField;
      this.Text = original.<Text>k__BackingField;
      this.IsEdit = original.<IsEdit>k__BackingField;
      this.IsPartial = original.<IsPartial>k__BackingField;
      this.StartsNewEdit = original.<StartsNewEdit>k__BackingField;
    }

    // Token: 0x060015EE RID: 5614 RVA: 0x003BABE0 File Offset: 0x003BABE0
    [CompilerGenerated]
    public void Deconstruct(out DateTimeOffset At, out string Text, out bool IsEdit, out bool? IsPartial, out bool? StartsNewEdit)
    {
      At = this.At;
      Text = this.Text;
      IsEdit = this.IsEdit;
      IsPartial = this.IsPartial;
      StartsNewEdit = this.StartsNewEdit;
    }
  }
}
