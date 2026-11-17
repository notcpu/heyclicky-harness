using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C6 RID: 454
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DictationLanguage : IEquatable<DictationLanguage>
  {
    // Token: 0x06001A4C RID: 6732 RVA: 0x003D67A0 File Offset: 0x003D67A0
    public DictationLanguage(string Code, string DisplayName, [Nullable(2)] string NativeName)
    {
      this.Code = Code;
      this.DisplayName = DisplayName;
      this.NativeName = NativeName;
      base..ctor();
    }

    // Token: 0x170005D3 RID: 1491
    // (get) Token: 0x06001A4D RID: 6733 RVA: 0x003D67C0 File Offset: 0x003D67C0
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DictationLanguage);
      }
    }

    // Token: 0x170005D4 RID: 1492
    // (get) Token: 0x06001A4E RID: 6734 RVA: 0x003D67CC File Offset: 0x003D67CC
    // (set) Token: 0x06001A4F RID: 6735 RVA: 0x003D67D4 File Offset: 0x003D67D4
    public string Code { get; set; }

    // Token: 0x170005D5 RID: 1493
    // (get) Token: 0x06001A50 RID: 6736 RVA: 0x003D67E0 File Offset: 0x003D67E0
    // (set) Token: 0x06001A51 RID: 6737 RVA: 0x003D67E8 File Offset: 0x003D67E8
    public string DisplayName { get; set; }

    // Token: 0x170005D6 RID: 1494
    // (get) Token: 0x06001A52 RID: 6738 RVA: 0x003D67F4 File Offset: 0x003D67F4
    // (set) Token: 0x06001A53 RID: 6739 RVA: 0x003D67FC File Offset: 0x003D67FC
    [Nullable(2)]
    public string NativeName
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06001A54 RID: 6740 RVA: 0x003D6808 File Offset: 0x003D6808
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DictationLanguage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001A55 RID: 6741 RVA: 0x003D6854 File Offset: 0x003D6854
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Code = ");
      builder.Append(this.Code);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", NativeName = ");
      builder.Append(this.NativeName);
      return true;
    }

    // Token: 0x06001A56 RID: 6742 RVA: 0x003D68B4 File Offset: 0x003D68B4
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DictationLanguage left, DictationLanguage right)
    {
      return !(left == right);
    }

    // Token: 0x06001A57 RID: 6743 RVA: 0x003D68C0 File Offset: 0x003D68C0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DictationLanguage left, DictationLanguage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001A58 RID: 6744 RVA: 0x003D68D4 File Offset: 0x003D68D4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<NativeName>k__BackingField);
    }

    // Token: 0x06001A59 RID: 6745 RVA: 0x003D6938 File Offset: 0x003D6938
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DictationLanguage);
    }

    // Token: 0x06001A5A RID: 6746 RVA: 0x003D6948 File Offset: 0x003D6948
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DictationLanguage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<NativeName>k__BackingField, other.<NativeName>k__BackingField));
    }

    // Token: 0x06001A5C RID: 6748 RVA: 0x003D69C4 File Offset: 0x003D69C4
    [CompilerGenerated]
    private DictationLanguage(DictationLanguage original)
    {
      this.Code = original.<Code>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.NativeName = original.<NativeName>k__BackingField;
    }

    // Token: 0x06001A5D RID: 6749 RVA: 0x003D69F0 File Offset: 0x003D69F0
    [CompilerGenerated]
    public void Deconstruct(out string Code, out string DisplayName, [Nullable(2)] out string NativeName)
    {
      Code = this.Code;
      DisplayName = this.DisplayName;
      NativeName = this.NativeName;
    }
  }
}
