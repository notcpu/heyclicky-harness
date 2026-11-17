using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000048 RID: 72
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DiscoveryChannelRequest : IEquatable<DiscoveryChannelRequest>
  {
    // Token: 0x060002FF RID: 767 RVA: 0x00352A4C File Offset: 0x00352A4C
    public DiscoveryChannelRequest(string DiscoveryChannel, [Nullable(2)] string DiscoveryChannelOther)
    {
      this.DiscoveryChannel = DiscoveryChannel;
      this.DiscoveryChannelOther = DiscoveryChannelOther;
      base..ctor();
    }

    // Token: 0x170000A2 RID: 162
    // (get) Token: 0x06000300 RID: 768 RVA: 0x00352A64 File Offset: 0x00352A64
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DiscoveryChannelRequest);
      }
    }

    // Token: 0x170000A3 RID: 163
    // (get) Token: 0x06000301 RID: 769 RVA: 0x00352A70 File Offset: 0x00352A70
    // (set) Token: 0x06000302 RID: 770 RVA: 0x00352A78 File Offset: 0x00352A78
    public string DiscoveryChannel { get; set; }

    // Token: 0x170000A4 RID: 164
    // (get) Token: 0x06000303 RID: 771 RVA: 0x00352A84 File Offset: 0x00352A84
    // (set) Token: 0x06000304 RID: 772 RVA: 0x00352A8C File Offset: 0x00352A8C
    [Nullable(2)]
    public string DiscoveryChannelOther
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06000305 RID: 773 RVA: 0x00352A98 File Offset: 0x00352A98
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DiscoveryChannelRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000306 RID: 774 RVA: 0x00352AE4 File Offset: 0x00352AE4
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

    // Token: 0x06000307 RID: 775 RVA: 0x00352B20 File Offset: 0x00352B20
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DiscoveryChannelRequest left, DiscoveryChannelRequest right)
    {
      return !(left == right);
    }

    // Token: 0x06000308 RID: 776 RVA: 0x00352B2C File Offset: 0x00352B2C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DiscoveryChannelRequest left, DiscoveryChannelRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000309 RID: 777 RVA: 0x00352B40 File Offset: 0x00352B40
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscoveryChannel>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DiscoveryChannelOther>k__BackingField);
    }

    // Token: 0x0600030A RID: 778 RVA: 0x00352B80 File Offset: 0x00352B80
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DiscoveryChannelRequest);
    }

    // Token: 0x0600030B RID: 779 RVA: 0x00352B90 File Offset: 0x00352B90
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DiscoveryChannelRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<DiscoveryChannel>k__BackingField, other.<DiscoveryChannel>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DiscoveryChannelOther>k__BackingField, other.<DiscoveryChannelOther>k__BackingField));
    }

    // Token: 0x0600030D RID: 781 RVA: 0x00352BF4 File Offset: 0x00352BF4
    [CompilerGenerated]
    private DiscoveryChannelRequest(DiscoveryChannelRequest original)
    {
      this.DiscoveryChannel = original.<DiscoveryChannel>k__BackingField;
      this.DiscoveryChannelOther = original.<DiscoveryChannelOther>k__BackingField;
    }

    // Token: 0x0600030E RID: 782 RVA: 0x00352C14 File Offset: 0x00352C14
    [CompilerGenerated]
    public void Deconstruct(out string DiscoveryChannel, [Nullable(2)] out string DiscoveryChannelOther)
    {
      DiscoveryChannel = this.DiscoveryChannel;
      DiscoveryChannelOther = this.DiscoveryChannelOther;
    }
  }
}
