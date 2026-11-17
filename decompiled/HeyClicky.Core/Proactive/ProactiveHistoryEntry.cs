using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000CD RID: 205
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ProactiveHistoryEntry : IEquatable<ProactiveHistoryEntry>
  {
    // Token: 0x06000D55 RID: 3413 RVA: 0x00381EB0 File Offset: 0x00381EB0
    public ProactiveHistoryEntry([Nullable(1)] string Title, string Description, ProactiveResolution Resolution, DateTimeOffset DecidedAt, string DenyReason = null, bool? WasViewed = null)
    {
      this.Title = Title;
      this.Description = Description;
      this.Resolution = Resolution;
      this.DecidedAt = DecidedAt;
      this.DenyReason = DenyReason;
      this.WasViewed = WasViewed;
      base..ctor();
    }

    // Token: 0x170002F2 RID: 754
    // (get) Token: 0x06000D56 RID: 3414 RVA: 0x00381EE8 File Offset: 0x00381EE8
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ProactiveHistoryEntry);
      }
    }

    // Token: 0x170002F3 RID: 755
    // (get) Token: 0x06000D57 RID: 3415 RVA: 0x00381EF4 File Offset: 0x00381EF4
    // (set) Token: 0x06000D58 RID: 3416 RVA: 0x00381EFC File Offset: 0x00381EFC
    [Nullable(1)]
    public string Title
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x170002F4 RID: 756
    // (get) Token: 0x06000D59 RID: 3417 RVA: 0x00381F08 File Offset: 0x00381F08
    // (set) Token: 0x06000D5A RID: 3418 RVA: 0x00381F10 File Offset: 0x00381F10
    public string Description { get; set; }

    // Token: 0x170002F5 RID: 757
    // (get) Token: 0x06000D5B RID: 3419 RVA: 0x00381F1C File Offset: 0x00381F1C
    // (set) Token: 0x06000D5C RID: 3420 RVA: 0x00381F24 File Offset: 0x00381F24
    public ProactiveResolution Resolution { get; set; }

    // Token: 0x170002F6 RID: 758
    // (get) Token: 0x06000D5D RID: 3421 RVA: 0x00381F30 File Offset: 0x00381F30
    // (set) Token: 0x06000D5E RID: 3422 RVA: 0x00381F38 File Offset: 0x00381F38
    public DateTimeOffset DecidedAt { get; set; }

    // Token: 0x170002F7 RID: 759
    // (get) Token: 0x06000D5F RID: 3423 RVA: 0x00381F44 File Offset: 0x00381F44
    // (set) Token: 0x06000D60 RID: 3424 RVA: 0x00381F4C File Offset: 0x00381F4C
    public string DenyReason { get; set; }

    // Token: 0x170002F8 RID: 760
    // (get) Token: 0x06000D61 RID: 3425 RVA: 0x00381F58 File Offset: 0x00381F58
    // (set) Token: 0x06000D62 RID: 3426 RVA: 0x00381F60 File Offset: 0x00381F60
    public bool? WasViewed { get; set; }

    // Token: 0x170002F9 RID: 761
    // (get) Token: 0x06000D63 RID: 3427 RVA: 0x00381F6C File Offset: 0x00381F6C
    [Nullable(1)]
    public string WireStatus
    {
      [NullableContext(1)]
      get
      {
        ProactiveResolution resolution = this.Resolution;
        string text;
        if (resolution != ProactiveResolution.Approved)
        {
          if (resolution != ProactiveResolution.Denied)
          {
            text = (this.WasViewed.GetValueOrDefault() ? "viewed_no_action" : ((this.Resolution == ProactiveResolution.Expired) ? "expired" : "replaced"));
          }
          else
          {
            text = "denied";
          }
        }
        else
        {
          text = "approved";
        }
        return text;
      }
    }

    // Token: 0x06000D64 RID: 3428 RVA: 0x00381FC8 File Offset: 0x00381FC8
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ProactiveHistoryEntry");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000D65 RID: 3429 RVA: 0x00382014 File Offset: 0x00382014
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Title = ");
      builder.Append(this.Title);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", Resolution = ");
      builder.Append(this.Resolution.ToString());
      builder.Append(", DecidedAt = ");
      builder.Append(this.DecidedAt.ToString());
      builder.Append(", DenyReason = ");
      builder.Append(this.DenyReason);
      builder.Append(", WasViewed = ");
      builder.Append(this.WasViewed.ToString());
      builder.Append(", WireStatus = ");
      builder.Append(this.WireStatus);
      return true;
    }

    // Token: 0x06000D66 RID: 3430 RVA: 0x00382100 File Offset: 0x00382100
    [CompilerGenerated]
    public static bool operator !=(ProactiveHistoryEntry left, ProactiveHistoryEntry right)
    {
      return !(left == right);
    }

    // Token: 0x06000D67 RID: 3431 RVA: 0x0038210C File Offset: 0x0038210C
    [CompilerGenerated]
    public static bool operator ==(ProactiveHistoryEntry left, ProactiveHistoryEntry right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000D68 RID: 3432 RVA: 0x00382120 File Offset: 0x00382120
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<ProactiveResolution>.Default.GetHashCode(this.<Resolution>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<DecidedAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DenyReason>k__BackingField)) * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(this.<WasViewed>k__BackingField);
    }

    // Token: 0x06000D69 RID: 3433 RVA: 0x003821C8 File Offset: 0x003821C8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ProactiveHistoryEntry);
    }

    // Token: 0x06000D6A RID: 3434 RVA: 0x003821D8 File Offset: 0x003821D8
    [CompilerGenerated]
    public bool Equals(ProactiveHistoryEntry other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<ProactiveResolution>.Default.Equals(this.<Resolution>k__BackingField, other.<Resolution>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<DecidedAt>k__BackingField, other.<DecidedAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DenyReason>k__BackingField, other.<DenyReason>k__BackingField) && EqualityComparer<bool?>.Default.Equals(this.<WasViewed>k__BackingField, other.<WasViewed>k__BackingField));
    }

    // Token: 0x06000D6C RID: 3436 RVA: 0x003822A4 File Offset: 0x003822A4
    [CompilerGenerated]
    private ProactiveHistoryEntry([Nullable(1)] ProactiveHistoryEntry original)
    {
      this.Title = original.<Title>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.Resolution = original.<Resolution>k__BackingField;
      this.DecidedAt = original.<DecidedAt>k__BackingField;
      this.DenyReason = original.<DenyReason>k__BackingField;
      this.WasViewed = original.<WasViewed>k__BackingField;
    }

    // Token: 0x06000D6D RID: 3437 RVA: 0x00382300 File Offset: 0x00382300
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Title, out string Description, out ProactiveResolution Resolution, out DateTimeOffset DecidedAt, out string DenyReason, out bool? WasViewed)
    {
      Title = this.Title;
      Description = this.Description;
      Resolution = this.Resolution;
      DecidedAt = this.DecidedAt;
      DenyReason = this.DenyReason;
      WasViewed = this.WasViewed;
    }
  }
}
