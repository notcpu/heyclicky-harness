using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001CC RID: 460
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationLearnedCorrection : IEquatable<DictationLearnedCorrection>
  {
    // Token: 0x06001A94 RID: 6804 RVA: 0x003D82B8 File Offset: 0x003D82B8
    public DictationLearnedCorrection(string Original, string Corrected)
    {
      this.Original = Original;
      this.Corrected = Corrected;
      base..ctor();
    }

    // Token: 0x170005E1 RID: 1505
    // (get) Token: 0x06001A95 RID: 6805 RVA: 0x003D82D0 File Offset: 0x003D82D0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationLearnedCorrection);
      }
    }

    // Token: 0x170005E2 RID: 1506
    // (get) Token: 0x06001A96 RID: 6806 RVA: 0x003D82DC File Offset: 0x003D82DC
    // (set) Token: 0x06001A97 RID: 6807 RVA: 0x003D82E4 File Offset: 0x003D82E4
    public string Original { get; set; }

    // Token: 0x170005E3 RID: 1507
    // (get) Token: 0x06001A98 RID: 6808 RVA: 0x003D82F0 File Offset: 0x003D82F0
    // (set) Token: 0x06001A99 RID: 6809 RVA: 0x003D82F8 File Offset: 0x003D82F8
    public string Corrected { get; set; }

    // Token: 0x06001A9A RID: 6810 RVA: 0x003D8304 File Offset: 0x003D8304
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationLearnedCorrection");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001A9B RID: 6811 RVA: 0x003D8350 File Offset: 0x003D8350
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Original = ");
      builder.Append(this.Original);
      builder.Append(", Corrected = ");
      builder.Append(this.Corrected);
      return true;
    }

    // Token: 0x06001A9C RID: 6812 RVA: 0x003D838C File Offset: 0x003D838C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationLearnedCorrection left, DictationLearnedCorrection right)
    {
      return !(left == right);
    }

    // Token: 0x06001A9D RID: 6813 RVA: 0x003D8398 File Offset: 0x003D8398
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationLearnedCorrection left, DictationLearnedCorrection right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001A9E RID: 6814 RVA: 0x003D83AC File Offset: 0x003D83AC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Original>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Corrected>k__BackingField);
    }

    // Token: 0x06001A9F RID: 6815 RVA: 0x003D83EC File Offset: 0x003D83EC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationLearnedCorrection);
    }

    // Token: 0x06001AA0 RID: 6816 RVA: 0x003D83FC File Offset: 0x003D83FC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationLearnedCorrection other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Original>k__BackingField, other.<Original>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Corrected>k__BackingField, other.<Corrected>k__BackingField));
    }

    // Token: 0x06001AA2 RID: 6818 RVA: 0x003D8460 File Offset: 0x003D8460
    [CompilerGenerated]
    private DictationLearnedCorrection(DictationLearnedCorrection original)
    {
      this.Original = original.<Original>k__BackingField;
      this.Corrected = original.<Corrected>k__BackingField;
    }

    // Token: 0x06001AA3 RID: 6819 RVA: 0x003D8480 File Offset: 0x003D8480
    [CompilerGenerated]
    public void Deconstruct(out string Original, out string Corrected)
    {
      Original = this.Original;
      Corrected = this.Corrected;
    }
  }
}
