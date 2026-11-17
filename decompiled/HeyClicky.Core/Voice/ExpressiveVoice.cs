using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200008E RID: 142
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ExpressiveVoice : IEquatable<ExpressiveVoice>
  {
    // Token: 0x0600088F RID: 2191 RVA: 0x00369E58 File Offset: 0x00369E58
    public ExpressiveVoice(string Id, string Name, string Subtitle, string Gender)
    {
      this.Id = Id;
      this.Name = Name;
      this.Subtitle = Subtitle;
      this.Gender = Gender;
      base..ctor();
    }

    // Token: 0x170001E4 RID: 484
    // (get) Token: 0x06000890 RID: 2192 RVA: 0x00369E80 File Offset: 0x00369E80
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ExpressiveVoice);
      }
    }

    // Token: 0x170001E5 RID: 485
    // (get) Token: 0x06000891 RID: 2193 RVA: 0x00369E8C File Offset: 0x00369E8C
    // (set) Token: 0x06000892 RID: 2194 RVA: 0x00369E94 File Offset: 0x00369E94
    public string Id { get; set; }

    // Token: 0x170001E6 RID: 486
    // (get) Token: 0x06000893 RID: 2195 RVA: 0x00369EA0 File Offset: 0x00369EA0
    // (set) Token: 0x06000894 RID: 2196 RVA: 0x00369EA8 File Offset: 0x00369EA8
    public string Name { get; set; }

    // Token: 0x170001E7 RID: 487
    // (get) Token: 0x06000895 RID: 2197 RVA: 0x00369EB4 File Offset: 0x00369EB4
    // (set) Token: 0x06000896 RID: 2198 RVA: 0x00369EBC File Offset: 0x00369EBC
    public string Subtitle { get; set; }

    // Token: 0x170001E8 RID: 488
    // (get) Token: 0x06000897 RID: 2199 RVA: 0x00369EC8 File Offset: 0x00369EC8
    // (set) Token: 0x06000898 RID: 2200 RVA: 0x00369ED0 File Offset: 0x00369ED0
    public string Gender { get; set; }

    // Token: 0x06000899 RID: 2201 RVA: 0x00369EDC File Offset: 0x00369EDC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ExpressiveVoice");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600089A RID: 2202 RVA: 0x00369F28 File Offset: 0x00369F28
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", Subtitle = ");
      builder.Append(this.Subtitle);
      builder.Append(", Gender = ");
      builder.Append(this.Gender);
      return true;
    }

    // Token: 0x0600089B RID: 2203 RVA: 0x00369FA0 File Offset: 0x00369FA0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ExpressiveVoice left, ExpressiveVoice right)
    {
      return !(left == right);
    }

    // Token: 0x0600089C RID: 2204 RVA: 0x00369FAC File Offset: 0x00369FAC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ExpressiveVoice left, ExpressiveVoice right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600089D RID: 2205 RVA: 0x00369FC0 File Offset: 0x00369FC0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Subtitle>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Gender>k__BackingField);
    }

    // Token: 0x0600089E RID: 2206 RVA: 0x0036A03C File Offset: 0x0036A03C
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ExpressiveVoice);
    }

    // Token: 0x0600089F RID: 2207 RVA: 0x0036A04C File Offset: 0x0036A04C
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ExpressiveVoice other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Subtitle>k__BackingField, other.<Subtitle>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Gender>k__BackingField, other.<Gender>k__BackingField));
    }

    // Token: 0x060008A1 RID: 2209 RVA: 0x0036A0E0 File Offset: 0x0036A0E0
    [CompilerGenerated]
    private ExpressiveVoice(ExpressiveVoice original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.Subtitle = original.<Subtitle>k__BackingField;
      this.Gender = original.<Gender>k__BackingField;
    }

    // Token: 0x060008A2 RID: 2210 RVA: 0x0036A118 File Offset: 0x0036A118
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Name, out string Subtitle, out string Gender)
    {
      Id = this.Id;
      Name = this.Name;
      Subtitle = this.Subtitle;
      Gender = this.Gender;
    }
  }
}
