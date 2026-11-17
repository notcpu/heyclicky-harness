using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008F RID: 143
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class VoiceLanguage : IEquatable<VoiceLanguage>
  {
    // Token: 0x060008A3 RID: 2211 RVA: 0x0036A13C File Offset: 0x0036A13C
    public VoiceLanguage(string Code, string Name)
    {
      this.Code = Code;
      this.Name = Name;
      base..ctor();
    }

    // Token: 0x170001E9 RID: 489
    // (get) Token: 0x060008A4 RID: 2212 RVA: 0x0036A154 File Offset: 0x0036A154
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(VoiceLanguage);
      }
    }

    // Token: 0x170001EA RID: 490
    // (get) Token: 0x060008A5 RID: 2213 RVA: 0x0036A160 File Offset: 0x0036A160
    // (set) Token: 0x060008A6 RID: 2214 RVA: 0x0036A168 File Offset: 0x0036A168
    public string Code { get; set; }

    // Token: 0x170001EB RID: 491
    // (get) Token: 0x060008A7 RID: 2215 RVA: 0x0036A174 File Offset: 0x0036A174
    // (set) Token: 0x060008A8 RID: 2216 RVA: 0x0036A17C File Offset: 0x0036A17C
    public string Name { get; set; }

    // Token: 0x170001EC RID: 492
    // (get) Token: 0x060008A9 RID: 2217 RVA: 0x0036A188 File Offset: 0x0036A188
    [Nullable(2)]
    public string NativeName
    {
      [NullableContext(2)]
      get
      {
        return VoiceCatalog.NativeName(this.Code);
      }
    }

    // Token: 0x060008AA RID: 2218 RVA: 0x0036A198 File Offset: 0x0036A198
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("VoiceLanguage");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060008AB RID: 2219 RVA: 0x0036A1E4 File Offset: 0x0036A1E4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Code = ");
      builder.Append(this.Code);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", NativeName = ");
      builder.Append(this.NativeName);
      return true;
    }

    // Token: 0x060008AC RID: 2220 RVA: 0x0036A244 File Offset: 0x0036A244
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(VoiceLanguage left, VoiceLanguage right)
    {
      return !(left == right);
    }

    // Token: 0x060008AD RID: 2221 RVA: 0x0036A250 File Offset: 0x0036A250
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(VoiceLanguage left, VoiceLanguage right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060008AE RID: 2222 RVA: 0x0036A264 File Offset: 0x0036A264
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Code>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField);
    }

    // Token: 0x060008AF RID: 2223 RVA: 0x0036A2A4 File Offset: 0x0036A2A4
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as VoiceLanguage);
    }

    // Token: 0x060008B0 RID: 2224 RVA: 0x0036A2B4 File Offset: 0x0036A2B4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(VoiceLanguage other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Code>k__BackingField, other.<Code>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField));
    }

    // Token: 0x060008B2 RID: 2226 RVA: 0x0036A318 File Offset: 0x0036A318
    [CompilerGenerated]
    private VoiceLanguage(VoiceLanguage original)
    {
      this.Code = original.<Code>k__BackingField;
      this.Name = original.<Name>k__BackingField;
    }

    // Token: 0x060008B3 RID: 2227 RVA: 0x0036A338 File Offset: 0x0036A338
    [CompilerGenerated]
    public void Deconstruct(out string Code, out string Name)
    {
      Code = this.Code;
      Name = this.Name;
    }
  }
}
