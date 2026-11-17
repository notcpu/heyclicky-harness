using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C2 RID: 450
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationResult : IEquatable<DictationResult>
  {
    // Token: 0x06001A1E RID: 6686 RVA: 0x003D5E90 File Offset: 0x003D5E90
    public DictationResult(string Text, DictationDeliveryOutcome Outcome, [Nullable(2)] string ClipboardReason, bool UsedBatch, bool HandsFree, bool UsedWarmConnection, int ReleaseToInsertMs)
    {
      this.Text = Text;
      this.Outcome = Outcome;
      this.ClipboardReason = ClipboardReason;
      this.UsedBatch = UsedBatch;
      this.HandsFree = HandsFree;
      this.UsedWarmConnection = UsedWarmConnection;
      this.ReleaseToInsertMs = ReleaseToInsertMs;
      base..ctor();
    }

    // Token: 0x170005C4 RID: 1476
    // (get) Token: 0x06001A1F RID: 6687 RVA: 0x003D5ED0 File Offset: 0x003D5ED0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationResult);
      }
    }

    // Token: 0x170005C5 RID: 1477
    // (get) Token: 0x06001A20 RID: 6688 RVA: 0x003D5EDC File Offset: 0x003D5EDC
    // (set) Token: 0x06001A21 RID: 6689 RVA: 0x003D5EE4 File Offset: 0x003D5EE4
    public string Text { get; set; }

    // Token: 0x170005C6 RID: 1478
    // (get) Token: 0x06001A22 RID: 6690 RVA: 0x003D5EF0 File Offset: 0x003D5EF0
    // (set) Token: 0x06001A23 RID: 6691 RVA: 0x003D5EF8 File Offset: 0x003D5EF8
    public DictationDeliveryOutcome Outcome { get; set; }

    // Token: 0x170005C7 RID: 1479
    // (get) Token: 0x06001A24 RID: 6692 RVA: 0x003D5F04 File Offset: 0x003D5F04
    // (set) Token: 0x06001A25 RID: 6693 RVA: 0x003D5F0C File Offset: 0x003D5F0C
    [Nullable(2)]
    public string ClipboardReason
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170005C8 RID: 1480
    // (get) Token: 0x06001A26 RID: 6694 RVA: 0x003D5F18 File Offset: 0x003D5F18
    // (set) Token: 0x06001A27 RID: 6695 RVA: 0x003D5F20 File Offset: 0x003D5F20
    public bool UsedBatch { get; set; }

    // Token: 0x170005C9 RID: 1481
    // (get) Token: 0x06001A28 RID: 6696 RVA: 0x003D5F2C File Offset: 0x003D5F2C
    // (set) Token: 0x06001A29 RID: 6697 RVA: 0x003D5F34 File Offset: 0x003D5F34
    public bool HandsFree { get; set; }

    // Token: 0x170005CA RID: 1482
    // (get) Token: 0x06001A2A RID: 6698 RVA: 0x003D5F40 File Offset: 0x003D5F40
    // (set) Token: 0x06001A2B RID: 6699 RVA: 0x003D5F48 File Offset: 0x003D5F48
    public bool UsedWarmConnection { get; set; }

    // Token: 0x170005CB RID: 1483
    // (get) Token: 0x06001A2C RID: 6700 RVA: 0x003D5F54 File Offset: 0x003D5F54
    // (set) Token: 0x06001A2D RID: 6701 RVA: 0x003D5F5C File Offset: 0x003D5F5C
    public int ReleaseToInsertMs { get; set; }

    // Token: 0x06001A2E RID: 6702 RVA: 0x003D5F68 File Offset: 0x003D5F68
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationResult");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001A2F RID: 6703 RVA: 0x003D5FB4 File Offset: 0x003D5FB4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Text = ");
      builder.Append(this.Text);
      builder.Append(", Outcome = ");
      builder.Append(this.Outcome.ToString());
      builder.Append(", ClipboardReason = ");
      builder.Append(this.ClipboardReason);
      builder.Append(", UsedBatch = ");
      builder.Append(this.UsedBatch.ToString());
      builder.Append(", HandsFree = ");
      builder.Append(this.HandsFree.ToString());
      builder.Append(", UsedWarmConnection = ");
      builder.Append(this.UsedWarmConnection.ToString());
      builder.Append(", ReleaseToInsertMs = ");
      builder.Append(this.ReleaseToInsertMs.ToString());
      return true;
    }

    // Token: 0x06001A30 RID: 6704 RVA: 0x003D60BC File Offset: 0x003D60BC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationResult left, DictationResult right)
    {
      return !(left == right);
    }

    // Token: 0x06001A31 RID: 6705 RVA: 0x003D60C8 File Offset: 0x003D60C8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationResult left, DictationResult right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001A32 RID: 6706 RVA: 0x003D60DC File Offset: 0x003D60DC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField)) * -1521134295 + EqualityComparer<DictationDeliveryOutcome>.Default.GetHashCode(this.<Outcome>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ClipboardReason>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UsedBatch>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<HandsFree>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<UsedWarmConnection>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ReleaseToInsertMs>k__BackingField);
    }

    // Token: 0x06001A33 RID: 6707 RVA: 0x003D619C File Offset: 0x003D619C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationResult);
    }

    // Token: 0x06001A34 RID: 6708 RVA: 0x003D61AC File Offset: 0x003D61AC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationResult other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField) && EqualityComparer<DictationDeliveryOutcome>.Default.Equals(this.<Outcome>k__BackingField, other.<Outcome>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ClipboardReason>k__BackingField, other.<ClipboardReason>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UsedBatch>k__BackingField, other.<UsedBatch>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<HandsFree>k__BackingField, other.<HandsFree>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<UsedWarmConnection>k__BackingField, other.<UsedWarmConnection>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ReleaseToInsertMs>k__BackingField, other.<ReleaseToInsertMs>k__BackingField));
    }

    // Token: 0x06001A36 RID: 6710 RVA: 0x003D6294 File Offset: 0x003D6294
    [CompilerGenerated]
    private DictationResult(DictationResult original)
    {
      this.Text = original.<Text>k__BackingField;
      this.Outcome = original.<Outcome>k__BackingField;
      this.ClipboardReason = original.<ClipboardReason>k__BackingField;
      this.UsedBatch = original.<UsedBatch>k__BackingField;
      this.HandsFree = original.<HandsFree>k__BackingField;
      this.UsedWarmConnection = original.<UsedWarmConnection>k__BackingField;
      this.ReleaseToInsertMs = original.<ReleaseToInsertMs>k__BackingField;
    }

    // Token: 0x06001A37 RID: 6711 RVA: 0x003D62FC File Offset: 0x003D62FC
    [CompilerGenerated]
    public void Deconstruct(out string Text, out DictationDeliveryOutcome Outcome, [Nullable(2)] out string ClipboardReason, out bool UsedBatch, out bool HandsFree, out bool UsedWarmConnection, out int ReleaseToInsertMs)
    {
      Text = this.Text;
      Outcome = this.Outcome;
      ClipboardReason = this.ClipboardReason;
      UsedBatch = this.UsedBatch;
      HandsFree = this.HandsFree;
      UsedWarmConnection = this.UsedWarmConnection;
      ReleaseToInsertMs = this.ReleaseToInsertMs;
    }
  }
}
