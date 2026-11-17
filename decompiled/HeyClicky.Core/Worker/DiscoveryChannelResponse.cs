using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000049 RID: 73
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class DiscoveryChannelResponse : IEquatable<DiscoveryChannelResponse>
  {
    // Token: 0x0600030F RID: 783 RVA: 0x00352C28 File Offset: 0x00352C28
    public DiscoveryChannelResponse(string DiscoveryChannel, string DiscoveryChannelOther)
    {
      this.DiscoveryChannel = DiscoveryChannel;
      this.DiscoveryChannelOther = DiscoveryChannelOther;
      base..ctor();
    }

    // Token: 0x170000A5 RID: 165
    // (get) Token: 0x06000310 RID: 784 RVA: 0x00352C40 File Offset: 0x00352C40
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(DiscoveryChannelResponse);
      }
    }

    // Token: 0x170000A6 RID: 166
    // (get) Token: 0x06000311 RID: 785 RVA: 0x00352C4C File Offset: 0x00352C4C
    // (set) Token: 0x06000312 RID: 786 RVA: 0x00352C54 File Offset: 0x00352C54
    public string DiscoveryChannel { get; set; }

    // Token: 0x170000A7 RID: 167
    // (get) Token: 0x06000313 RID: 787 RVA: 0x00352C60 File Offset: 0x00352C60
    // (set) Token: 0x06000314 RID: 788 RVA: 0x00352C68 File Offset: 0x00352C68
    public string DiscoveryChannelOther { get; set; }

    // Token: 0x06000315 RID: 789 RVA: 0x00352C74 File Offset: 0x00352C74
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DiscoveryChannelResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000316 RID: 790 RVA: 0x00352CC0 File Offset: 0x00352CC0
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("DiscoveryChannel = ");
      builder.Append(this.DiscoveryChannel);
      builder.Append(", DiscoveryChannelOther = ");
      builder.Append(this.DiscoveryChannelOther);
      return true;
    }

    // Token: 0x06000317 RID: 791 RVA: 0x00352CFC File Offset: 0x00352CFC
    [CompilerGenerated]
    public static bool operator !=(DiscoveryChannelResponse left, DiscoveryChannelResponse right)
    {
      return !(left == right);
    }

    // Token: 0x06000318 RID: 792 RVA: 0x00352D08 File Offset: 0x00352D08
    [CompilerGenerated]
    public static bool operator ==(DiscoveryChannelResponse left, DiscoveryChannelResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000319 RID: 793 RVA: 0x00352D1C File Offset: 0x00352D1C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscoveryChannel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscoveryChannelOther>k__BackingField);
    }

    // Token: 0x0600031A RID: 794 RVA: 0x00352D5C File Offset: 0x00352D5C
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DiscoveryChannelResponse);
    }

    // Token: 0x0600031B RID: 795 RVA: 0x00352D6C File Offset: 0x00352D6C
    [CompilerGenerated]
    public bool Equals(DiscoveryChannelResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<DiscoveryChannel>k__BackingField, other.<DiscoveryChannel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DiscoveryChannelOther>k__BackingField, other.<DiscoveryChannelOther>k__BackingField));
    }

    // Token: 0x0600031D RID: 797 RVA: 0x00352DD0 File Offset: 0x00352DD0
    [CompilerGenerated]
    private DiscoveryChannelResponse([Nullable(1)] DiscoveryChannelResponse original)
    {
      this.DiscoveryChannel = original.<DiscoveryChannel>k__BackingField;
      this.DiscoveryChannelOther = original.<DiscoveryChannelOther>k__BackingField;
    }

    // Token: 0x0600031E RID: 798 RVA: 0x00352DF0 File Offset: 0x00352DF0
    [CompilerGenerated]
    public void Deconstruct(out string DiscoveryChannel, out string DiscoveryChannelOther)
    {
      DiscoveryChannel = this.DiscoveryChannel;
      DiscoveryChannelOther = this.DiscoveryChannelOther;
    }
  }
}
