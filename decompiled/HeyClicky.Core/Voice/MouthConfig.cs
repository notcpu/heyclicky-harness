using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007E RID: 126
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class MouthConfig : IEquatable<MouthConfig>
  {
    // Token: 0x0600078E RID: 1934 RVA: 0x00365824 File Offset: 0x00365824
    public MouthConfig(string VoiceIdentifier, double Speed, string UsageContext, [Nullable(2)] string Language = null)
    {
      this.VoiceIdentifier = VoiceIdentifier;
      this.Speed = Speed;
      this.UsageContext = UsageContext;
      this.Language = Language;
      base..ctor();
    }

    // Token: 0x170001AF RID: 431
    // (get) Token: 0x0600078F RID: 1935 RVA: 0x0036584C File Offset: 0x0036584C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(MouthConfig);
      }
    }

    // Token: 0x170001B0 RID: 432
    // (get) Token: 0x06000790 RID: 1936 RVA: 0x00365858 File Offset: 0x00365858
    // (set) Token: 0x06000791 RID: 1937 RVA: 0x00365860 File Offset: 0x00365860
    public string VoiceIdentifier { get; set; }

    // Token: 0x170001B1 RID: 433
    // (get) Token: 0x06000792 RID: 1938 RVA: 0x0036586C File Offset: 0x0036586C
    // (set) Token: 0x06000793 RID: 1939 RVA: 0x00365874 File Offset: 0x00365874
    public double Speed { get; set; }

    // Token: 0x170001B2 RID: 434
    // (get) Token: 0x06000794 RID: 1940 RVA: 0x00365880 File Offset: 0x00365880
    // (set) Token: 0x06000795 RID: 1941 RVA: 0x00365888 File Offset: 0x00365888
    public string UsageContext { get; set; }

    // Token: 0x170001B3 RID: 435
    // (get) Token: 0x06000796 RID: 1942 RVA: 0x00365894 File Offset: 0x00365894
    // (set) Token: 0x06000797 RID: 1943 RVA: 0x0036589C File Offset: 0x0036589C
    [Nullable(2)]
    public string Language
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000798 RID: 1944 RVA: 0x003658A8 File Offset: 0x003658A8
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("MouthConfig");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000799 RID: 1945 RVA: 0x003658F4 File Offset: 0x003658F4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("VoiceIdentifier = ");
      builder.Append(this.VoiceIdentifier);
      builder.Append(", Speed = ");
      builder.Append(this.Speed.ToString());
      builder.Append(", UsageContext = ");
      builder.Append(this.UsageContext);
      builder.Append(", Language = ");
      builder.Append(this.Language);
      return true;
    }

    // Token: 0x0600079A RID: 1946 RVA: 0x0036597C File Offset: 0x0036597C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(MouthConfig left, MouthConfig right)
    {
      return !(left == right);
    }

    // Token: 0x0600079B RID: 1947 RVA: 0x00365988 File Offset: 0x00365988
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(MouthConfig left, MouthConfig right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600079C RID: 1948 RVA: 0x0036599C File Offset: 0x0036599C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<VoiceIdentifier>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Speed>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UsageContext>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Language>k__BackingField);
    }

    // Token: 0x0600079D RID: 1949 RVA: 0x00365A18 File Offset: 0x00365A18
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MouthConfig);
    }

    // Token: 0x0600079E RID: 1950 RVA: 0x00365A28 File Offset: 0x00365A28
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(MouthConfig other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<VoiceIdentifier>k__BackingField, other.<VoiceIdentifier>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Speed>k__BackingField, other.<Speed>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UsageContext>k__BackingField, other.<UsageContext>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Language>k__BackingField, other.<Language>k__BackingField));
    }

    // Token: 0x060007A0 RID: 1952 RVA: 0x00365ABC File Offset: 0x00365ABC
    [CompilerGenerated]
    private MouthConfig(MouthConfig original)
    {
      this.VoiceIdentifier = original.<VoiceIdentifier>k__BackingField;
      this.Speed = original.<Speed>k__BackingField;
      this.UsageContext = original.<UsageContext>k__BackingField;
      this.Language = original.<Language>k__BackingField;
    }

    // Token: 0x060007A1 RID: 1953 RVA: 0x00365AF4 File Offset: 0x00365AF4
    [CompilerGenerated]
    public void Deconstruct(out string VoiceIdentifier, out double Speed, out string UsageContext, [Nullable(2)] out string Language)
    {
      VoiceIdentifier = this.VoiceIdentifier;
      Speed = this.Speed;
      UsageContext = this.UsageContext;
      Language = this.Language;
    }
  }
}
