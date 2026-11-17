using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200006D RID: 109
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class CompanionTurnMetrics : IEquatable<CompanionTurnMetrics>
  {
    // Token: 0x0600068A RID: 1674 RVA: 0x0035F100 File Offset: 0x0035F100
    public CompanionTurnMetrics(bool Typed, int TranscriptMs, int CaptureMs, int? BrainFirstByteMs, int? FirstSayLineMs, int? FirstAudioMs, int? FirstSpokenMs, int TotalMs, string Model)
    {
      this.Typed = Typed;
      this.TranscriptMs = TranscriptMs;
      this.CaptureMs = CaptureMs;
      this.BrainFirstByteMs = BrainFirstByteMs;
      this.FirstSayLineMs = FirstSayLineMs;
      this.FirstAudioMs = FirstAudioMs;
      this.FirstSpokenMs = FirstSpokenMs;
      this.TotalMs = TotalMs;
      this.Model = Model;
      base..ctor();
    }

    // Token: 0x17000170 RID: 368
    // (get) Token: 0x0600068B RID: 1675 RVA: 0x0035F158 File Offset: 0x0035F158
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(CompanionTurnMetrics);
      }
    }

    // Token: 0x17000171 RID: 369
    // (get) Token: 0x0600068C RID: 1676 RVA: 0x0035F164 File Offset: 0x0035F164
    // (set) Token: 0x0600068D RID: 1677 RVA: 0x0035F16C File Offset: 0x0035F16C
    public bool Typed { get; set; }

    // Token: 0x17000172 RID: 370
    // (get) Token: 0x0600068E RID: 1678 RVA: 0x0035F178 File Offset: 0x0035F178
    // (set) Token: 0x0600068F RID: 1679 RVA: 0x0035F180 File Offset: 0x0035F180
    public int TranscriptMs { get; set; }

    // Token: 0x17000173 RID: 371
    // (get) Token: 0x06000690 RID: 1680 RVA: 0x0035F18C File Offset: 0x0035F18C
    // (set) Token: 0x06000691 RID: 1681 RVA: 0x0035F194 File Offset: 0x0035F194
    public int CaptureMs { get; set; }

    // Token: 0x17000174 RID: 372
    // (get) Token: 0x06000692 RID: 1682 RVA: 0x0035F1A0 File Offset: 0x0035F1A0
    // (set) Token: 0x06000693 RID: 1683 RVA: 0x0035F1A8 File Offset: 0x0035F1A8
    public int? BrainFirstByteMs { get; set; }

    // Token: 0x17000175 RID: 373
    // (get) Token: 0x06000694 RID: 1684 RVA: 0x0035F1B4 File Offset: 0x0035F1B4
    // (set) Token: 0x06000695 RID: 1685 RVA: 0x0035F1BC File Offset: 0x0035F1BC
    public int? FirstSayLineMs { get; set; }

    // Token: 0x17000176 RID: 374
    // (get) Token: 0x06000696 RID: 1686 RVA: 0x0035F1C8 File Offset: 0x0035F1C8
    // (set) Token: 0x06000697 RID: 1687 RVA: 0x0035F1D0 File Offset: 0x0035F1D0
    public int? FirstAudioMs { get; set; }

    // Token: 0x17000177 RID: 375
    // (get) Token: 0x06000698 RID: 1688 RVA: 0x0035F1DC File Offset: 0x0035F1DC
    // (set) Token: 0x06000699 RID: 1689 RVA: 0x0035F1E4 File Offset: 0x0035F1E4
    public int? FirstSpokenMs { get; set; }

    // Token: 0x17000178 RID: 376
    // (get) Token: 0x0600069A RID: 1690 RVA: 0x0035F1F0 File Offset: 0x0035F1F0
    // (set) Token: 0x0600069B RID: 1691 RVA: 0x0035F1F8 File Offset: 0x0035F1F8
    public int TotalMs { get; set; }

    // Token: 0x17000179 RID: 377
    // (get) Token: 0x0600069C RID: 1692 RVA: 0x0035F204 File Offset: 0x0035F204
    // (set) Token: 0x0600069D RID: 1693 RVA: 0x0035F20C File Offset: 0x0035F20C
    public string Model { get; set; }

    // Token: 0x0600069E RID: 1694 RVA: 0x0035F218 File Offset: 0x0035F218
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CompanionTurnMetrics");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600069F RID: 1695 RVA: 0x0035F264 File Offset: 0x0035F264
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Typed = ");
      builder.Append(this.Typed.ToString());
      builder.Append(", TranscriptMs = ");
      builder.Append(this.TranscriptMs.ToString());
      builder.Append(", CaptureMs = ");
      builder.Append(this.CaptureMs.ToString());
      builder.Append(", BrainFirstByteMs = ");
      builder.Append(this.BrainFirstByteMs.ToString());
      builder.Append(", FirstSayLineMs = ");
      builder.Append(this.FirstSayLineMs.ToString());
      builder.Append(", FirstAudioMs = ");
      builder.Append(this.FirstAudioMs.ToString());
      builder.Append(", FirstSpokenMs = ");
      builder.Append(this.FirstSpokenMs.ToString());
      builder.Append(", TotalMs = ");
      builder.Append(this.TotalMs.ToString());
      builder.Append(", Model = ");
      builder.Append(this.Model);
      return true;
    }

    // Token: 0x060006A0 RID: 1696 RVA: 0x0035F3C8 File Offset: 0x0035F3C8
    [CompilerGenerated]
    public static bool operator !=(CompanionTurnMetrics left, CompanionTurnMetrics right)
    {
      return !(left == right);
    }

    // Token: 0x060006A1 RID: 1697 RVA: 0x0035F3D4 File Offset: 0x0035F3D4
    [CompilerGenerated]
    public static bool operator ==(CompanionTurnMetrics left, CompanionTurnMetrics right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060006A2 RID: 1698 RVA: 0x0035F3E8 File Offset: 0x0035F3E8
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Typed>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TranscriptMs>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<CaptureMs>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<BrainFirstByteMs>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<FirstSayLineMs>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<FirstAudioMs>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<FirstSpokenMs>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TotalMs>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField);
    }

    // Token: 0x060006A3 RID: 1699 RVA: 0x0035F4D4 File Offset: 0x0035F4D4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CompanionTurnMetrics);
    }

    // Token: 0x060006A4 RID: 1700 RVA: 0x0035F4E4 File Offset: 0x0035F4E4
    [CompilerGenerated]
    public bool Equals(CompanionTurnMetrics other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<bool>.Default.Equals(this.<Typed>k__BackingField, other.<Typed>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<TranscriptMs>k__BackingField, other.<TranscriptMs>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<CaptureMs>k__BackingField, other.<CaptureMs>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<BrainFirstByteMs>k__BackingField, other.<BrainFirstByteMs>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<FirstSayLineMs>k__BackingField, other.<FirstSayLineMs>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<FirstAudioMs>k__BackingField, other.<FirstAudioMs>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<FirstSpokenMs>k__BackingField, other.<FirstSpokenMs>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<TotalMs>k__BackingField, other.<TotalMs>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
    }

    // Token: 0x060006A6 RID: 1702 RVA: 0x0035F600 File Offset: 0x0035F600
    [CompilerGenerated]
    private CompanionTurnMetrics([Nullable(1)] CompanionTurnMetrics original)
    {
      this.Typed = original.<Typed>k__BackingField;
      this.TranscriptMs = original.<TranscriptMs>k__BackingField;
      this.CaptureMs = original.<CaptureMs>k__BackingField;
      this.BrainFirstByteMs = original.<BrainFirstByteMs>k__BackingField;
      this.FirstSayLineMs = original.<FirstSayLineMs>k__BackingField;
      this.FirstAudioMs = original.<FirstAudioMs>k__BackingField;
      this.FirstSpokenMs = original.<FirstSpokenMs>k__BackingField;
      this.TotalMs = original.<TotalMs>k__BackingField;
      this.Model = original.<Model>k__BackingField;
    }

    // Token: 0x060006A7 RID: 1703 RVA: 0x0035F680 File Offset: 0x0035F680
    [CompilerGenerated]
    public void Deconstruct(out bool Typed, out int TranscriptMs, out int CaptureMs, out int? BrainFirstByteMs, out int? FirstSayLineMs, out int? FirstAudioMs, out int? FirstSpokenMs, out int TotalMs, out string Model)
    {
      Typed = this.Typed;
      TranscriptMs = this.TranscriptMs;
      CaptureMs = this.CaptureMs;
      BrainFirstByteMs = this.BrainFirstByteMs;
      FirstSayLineMs = this.FirstSayLineMs;
      FirstAudioMs = this.FirstAudioMs;
      FirstSpokenMs = this.FirstSpokenMs;
      TotalMs = this.TotalMs;
      Model = this.Model;
    }
  }
}
