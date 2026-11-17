using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000077 RID: 119
  [NullableContext(2)]
  [Nullable(0)]
  public readonly struct GeminiMouthEvent : IEquatable<GeminiMouthEvent>
  {
    // Token: 0x0600072C RID: 1836 RVA: 0x003626DC File Offset: 0x003626DC
    public GeminiMouthEvent(GeminiMouthEventKind Kind, string Base64Audio = null, string UsageJson = null)
    {
      this.Kind = Kind;
      this.Base64Audio = Base64Audio;
      this.UsageJson = UsageJson;
    }

    // Token: 0x1700019F RID: 415
    // (get) Token: 0x0600072D RID: 1837 RVA: 0x003626F4 File Offset: 0x003626F4
    // (set) Token: 0x0600072E RID: 1838 RVA: 0x003626FC File Offset: 0x003626FC
    public GeminiMouthEventKind Kind { get; set; }

    // Token: 0x170001A0 RID: 416
    // (get) Token: 0x0600072F RID: 1839 RVA: 0x00362708 File Offset: 0x00362708
    // (set) Token: 0x06000730 RID: 1840 RVA: 0x00362710 File Offset: 0x00362710
    public string Base64Audio { get; set; }

    // Token: 0x170001A1 RID: 417
    // (get) Token: 0x06000731 RID: 1841 RVA: 0x0036271C File Offset: 0x0036271C
    // (set) Token: 0x06000732 RID: 1842 RVA: 0x00362724 File Offset: 0x00362724
    public string UsageJson { get; set; }

    // Token: 0x06000733 RID: 1843 RVA: 0x00362730 File Offset: 0x00362730
    [NullableContext(0)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("GeminiMouthEvent");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000734 RID: 1844 RVA: 0x0036277C File Offset: 0x0036277C
    [NullableContext(0)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      builder.Append("Kind = ");
      builder.Append(this.Kind.ToString());
      builder.Append(", Base64Audio = ");
      builder.Append(this.Base64Audio);
      builder.Append(", UsageJson = ");
      builder.Append(this.UsageJson);
      return true;
    }

    // Token: 0x06000735 RID: 1845 RVA: 0x003627E4 File Offset: 0x003627E4
    [CompilerGenerated]
    public static bool operator !=(GeminiMouthEvent left, GeminiMouthEvent right)
    {
      return !(left == right);
    }

    // Token: 0x06000736 RID: 1846 RVA: 0x003627F0 File Offset: 0x003627F0
    [CompilerGenerated]
    public static bool operator ==(GeminiMouthEvent left, GeminiMouthEvent right)
    {
      return left.Equals(right);
    }

    // Token: 0x06000737 RID: 1847 RVA: 0x003627FC File Offset: 0x003627FC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<GeminiMouthEventKind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Base64Audio>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageJson>k__BackingField);
    }

    // Token: 0x06000738 RID: 1848 RVA: 0x0036283C File Offset: 0x0036283C
    [NullableContext(0)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return obj is GeminiMouthEvent && this.Equals((GeminiMouthEvent)obj);
    }

    // Token: 0x06000739 RID: 1849 RVA: 0x00362854 File Offset: 0x00362854
    [CompilerGenerated]
    public bool Equals(GeminiMouthEvent other)
    {
      return EqualityComparer<GeminiMouthEventKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Base64Audio>k__BackingField, other.<Base64Audio>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageJson>k__BackingField, other.<UsageJson>k__BackingField);
    }

    // Token: 0x0600073A RID: 1850 RVA: 0x003628AC File Offset: 0x003628AC
    [CompilerGenerated]
    public void Deconstruct(out GeminiMouthEventKind Kind, out string Base64Audio, out string UsageJson)
    {
      Kind = this.Kind;
      Base64Audio = this.Base64Audio;
      UsageJson = this.UsageJson;
    }
  }
}
