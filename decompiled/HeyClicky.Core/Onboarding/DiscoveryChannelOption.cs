using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000EF RID: 239
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DiscoveryChannelOption : IEquatable<DiscoveryChannelOption>
  {
    // Token: 0x06000F40 RID: 3904 RVA: 0x0038E374 File Offset: 0x0038E374
    public DiscoveryChannelOption(string Id, string Label)
    {
      this.Id = Id;
      this.Label = Label;
      base..ctor();
    }

    // Token: 0x17000372 RID: 882
    // (get) Token: 0x06000F41 RID: 3905 RVA: 0x0038E38C File Offset: 0x0038E38C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DiscoveryChannelOption);
      }
    }

    // Token: 0x17000373 RID: 883
    // (get) Token: 0x06000F42 RID: 3906 RVA: 0x0038E398 File Offset: 0x0038E398
    // (set) Token: 0x06000F43 RID: 3907 RVA: 0x0038E3A0 File Offset: 0x0038E3A0
    public string Id { get; set; }

    // Token: 0x17000374 RID: 884
    // (get) Token: 0x06000F44 RID: 3908 RVA: 0x0038E3AC File Offset: 0x0038E3AC
    // (set) Token: 0x06000F45 RID: 3909 RVA: 0x0038E3B4 File Offset: 0x0038E3B4
    public string Label { get; set; }

    // Token: 0x06000F46 RID: 3910 RVA: 0x0038E3C0 File Offset: 0x0038E3C0
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DiscoveryChannelOption");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000F47 RID: 3911 RVA: 0x0038E40C File Offset: 0x0038E40C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", Label = ");
      builder.Append(this.Label);
      return true;
    }

    // Token: 0x06000F48 RID: 3912 RVA: 0x0038E448 File Offset: 0x0038E448
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DiscoveryChannelOption left, DiscoveryChannelOption right)
    {
      return !(left == right);
    }

    // Token: 0x06000F49 RID: 3913 RVA: 0x0038E454 File Offset: 0x0038E454
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DiscoveryChannelOption left, DiscoveryChannelOption right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000F4A RID: 3914 RVA: 0x0038E468 File Offset: 0x0038E468
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Label>k__BackingField);
    }

    // Token: 0x06000F4B RID: 3915 RVA: 0x0038E4A8 File Offset: 0x0038E4A8
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DiscoveryChannelOption);
    }

    // Token: 0x06000F4C RID: 3916 RVA: 0x0038E4B8 File Offset: 0x0038E4B8
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DiscoveryChannelOption other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Label>k__BackingField, other.<Label>k__BackingField));
    }

    // Token: 0x06000F4E RID: 3918 RVA: 0x0038E51C File Offset: 0x0038E51C
    [CompilerGenerated]
    private DiscoveryChannelOption(DiscoveryChannelOption original)
    {
      this.Id = original.<Id>k__BackingField;
      this.Label = original.<Label>k__BackingField;
    }

    // Token: 0x06000F4F RID: 3919 RVA: 0x0038E53C File Offset: 0x0038E53C
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string Label)
    {
      Id = this.Id;
      Label = this.Label;
    }
  }
}
