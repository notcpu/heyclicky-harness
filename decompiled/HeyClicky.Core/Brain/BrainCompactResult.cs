using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001ED RID: 493
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class BrainCompactResult : IEquatable<BrainCompactResult>
  {
    // Token: 0x06001CC6 RID: 7366 RVA: 0x003E4C5C File Offset: 0x003E4C5C
    public BrainCompactResult(JsonNode NoteItem, int KeepFromIndex, int KeepLeadingItems, int DropLeadingItems)
    {
      this.NoteItem = NoteItem;
      this.KeepFromIndex = KeepFromIndex;
      this.KeepLeadingItems = KeepLeadingItems;
      this.DropLeadingItems = DropLeadingItems;
      base..ctor();
    }

    // Token: 0x17000660 RID: 1632
    // (get) Token: 0x06001CC7 RID: 7367 RVA: 0x003E4C84 File Offset: 0x003E4C84
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(BrainCompactResult);
      }
    }

    // Token: 0x17000661 RID: 1633
    // (get) Token: 0x06001CC8 RID: 7368 RVA: 0x003E4C90 File Offset: 0x003E4C90
    // (set) Token: 0x06001CC9 RID: 7369 RVA: 0x003E4C98 File Offset: 0x003E4C98
    public JsonNode NoteItem { get; set; }

    // Token: 0x17000662 RID: 1634
    // (get) Token: 0x06001CCA RID: 7370 RVA: 0x003E4CA4 File Offset: 0x003E4CA4
    // (set) Token: 0x06001CCB RID: 7371 RVA: 0x003E4CAC File Offset: 0x003E4CAC
    public int KeepFromIndex { get; set; }

    // Token: 0x17000663 RID: 1635
    // (get) Token: 0x06001CCC RID: 7372 RVA: 0x003E4CB8 File Offset: 0x003E4CB8
    // (set) Token: 0x06001CCD RID: 7373 RVA: 0x003E4CC0 File Offset: 0x003E4CC0
    public int KeepLeadingItems { get; set; }

    // Token: 0x17000664 RID: 1636
    // (get) Token: 0x06001CCE RID: 7374 RVA: 0x003E4CCC File Offset: 0x003E4CCC
    // (set) Token: 0x06001CCF RID: 7375 RVA: 0x003E4CD4 File Offset: 0x003E4CD4
    public int DropLeadingItems { get; set; }

    // Token: 0x06001CD0 RID: 7376 RVA: 0x003E4CE0 File Offset: 0x003E4CE0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("BrainCompactResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001CD1 RID: 7377 RVA: 0x003E4D2C File Offset: 0x003E4D2C
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("NoteItem = ");
      builder.Append(this.NoteItem);
      builder.Append(", KeepFromIndex = ");
      builder.Append(this.KeepFromIndex.ToString());
      builder.Append(", KeepLeadingItems = ");
      builder.Append(this.KeepLeadingItems.ToString());
      builder.Append(", DropLeadingItems = ");
      builder.Append(this.DropLeadingItems.ToString());
      return true;
    }

    // Token: 0x06001CD2 RID: 7378 RVA: 0x003E4DD0 File Offset: 0x003E4DD0
    [CompilerGenerated]
    public static bool operator !=(BrainCompactResult left, BrainCompactResult right)
    {
      return !(left == right);
    }

    // Token: 0x06001CD3 RID: 7379 RVA: 0x003E4DDC File Offset: 0x003E4DDC
    [CompilerGenerated]
    public static bool operator ==(BrainCompactResult left, BrainCompactResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001CD4 RID: 7380 RVA: 0x003E4DF0 File Offset: 0x003E4DF0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<JsonNode>.Default.GetHashCode(this.<NoteItem>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<KeepFromIndex>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<KeepLeadingItems>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<DropLeadingItems>k__BackingField);
    }

    // Token: 0x06001CD5 RID: 7381 RVA: 0x003E4E6C File Offset: 0x003E4E6C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as BrainCompactResult);
    }

    // Token: 0x06001CD6 RID: 7382 RVA: 0x003E4E7C File Offset: 0x003E4E7C
    [CompilerGenerated]
    public bool Equals(BrainCompactResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<JsonNode>.Default.Equals(this.<NoteItem>k__BackingField, other.<NoteItem>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<KeepFromIndex>k__BackingField, other.<KeepFromIndex>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<KeepLeadingItems>k__BackingField, other.<KeepLeadingItems>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<DropLeadingItems>k__BackingField, other.<DropLeadingItems>k__BackingField));
    }

    // Token: 0x06001CD8 RID: 7384 RVA: 0x003E4F10 File Offset: 0x003E4F10
    [CompilerGenerated]
    private BrainCompactResult([Nullable(1)] BrainCompactResult original)
    {
      this.NoteItem = original.<NoteItem>k__BackingField;
      this.KeepFromIndex = original.<KeepFromIndex>k__BackingField;
      this.KeepLeadingItems = original.<KeepLeadingItems>k__BackingField;
      this.DropLeadingItems = original.<DropLeadingItems>k__BackingField;
    }

    // Token: 0x06001CD9 RID: 7385 RVA: 0x003E4F48 File Offset: 0x003E4F48
    [CompilerGenerated]
    public void Deconstruct(out JsonNode NoteItem, out int KeepFromIndex, out int KeepLeadingItems, out int DropLeadingItems)
    {
      NoteItem = this.NoteItem;
      KeepFromIndex = this.KeepFromIndex;
      KeepLeadingItems = this.KeepLeadingItems;
      DropLeadingItems = this.DropLeadingItems;
    }
  }
}
