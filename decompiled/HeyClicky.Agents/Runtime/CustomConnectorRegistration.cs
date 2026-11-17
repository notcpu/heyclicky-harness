using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x0200001E RID: 30
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CustomConnectorRegistration : IEquatable<CustomConnectorRegistration>
  {
    // Token: 0x06000392 RID: 914 RVA: 0x000E6474 File Offset: 0x000E6474
    public CustomConnectorRegistration(string ServerKey, string DisplayName, string CapabilityDescription, CustomConnectorTransport Transport, IReadOnlyDictionary<string, string> Environment)
    {
      this.ServerKey = ServerKey;
      this.DisplayName = DisplayName;
      this.CapabilityDescription = CapabilityDescription;
      this.Transport = Transport;
      this.Environment = Environment;
      base..ctor();
    }

    // Token: 0x170000F8 RID: 248
    // (get) Token: 0x06000393 RID: 915 RVA: 0x000E64A4 File Offset: 0x000E64A4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(CustomConnectorRegistration);
      }
    }

    // Token: 0x170000F9 RID: 249
    // (get) Token: 0x06000394 RID: 916 RVA: 0x000E64B0 File Offset: 0x000E64B0
    // (set) Token: 0x06000395 RID: 917 RVA: 0x000E64B8 File Offset: 0x000E64B8
    public string ServerKey { get; set; }

    // Token: 0x170000FA RID: 250
    // (get) Token: 0x06000396 RID: 918 RVA: 0x000E64C4 File Offset: 0x000E64C4
    // (set) Token: 0x06000397 RID: 919 RVA: 0x000E64CC File Offset: 0x000E64CC
    public string DisplayName { get; set; }

    // Token: 0x170000FB RID: 251
    // (get) Token: 0x06000398 RID: 920 RVA: 0x000E64D8 File Offset: 0x000E64D8
    // (set) Token: 0x06000399 RID: 921 RVA: 0x000E64E0 File Offset: 0x000E64E0
    public string CapabilityDescription { get; set; }

    // Token: 0x170000FC RID: 252
    // (get) Token: 0x0600039A RID: 922 RVA: 0x000E64EC File Offset: 0x000E64EC
    // (set) Token: 0x0600039B RID: 923 RVA: 0x000E64F4 File Offset: 0x000E64F4
    public CustomConnectorTransport Transport { get; set; }

    // Token: 0x170000FD RID: 253
    // (get) Token: 0x0600039C RID: 924 RVA: 0x000E6500 File Offset: 0x000E6500
    // (set) Token: 0x0600039D RID: 925 RVA: 0x000E6508 File Offset: 0x000E6508
    public IReadOnlyDictionary<string, string> Environment { get; set; }

    // Token: 0x0600039E RID: 926 RVA: 0x000E6514 File Offset: 0x000E6514
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("CustomConnectorRegistration");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x0600039F RID: 927 RVA: 0x000E6560 File Offset: 0x000E6560
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("ServerKey = ");
      builder.Append(this.ServerKey);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", CapabilityDescription = ");
      builder.Append(this.CapabilityDescription);
      builder.Append(", Transport = ");
      builder.Append(this.Transport);
      builder.Append(", Environment = ");
      builder.Append(this.Environment);
      return true;
    }

    // Token: 0x060003A0 RID: 928 RVA: 0x000E65F0 File Offset: 0x000E65F0
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(CustomConnectorRegistration left, CustomConnectorRegistration right)
    {
      return !(left == right);
    }

    // Token: 0x060003A1 RID: 929 RVA: 0x000E65FC File Offset: 0x000E65FC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(CustomConnectorRegistration left, CustomConnectorRegistration right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060003A2 RID: 930 RVA: 0x000E6610 File Offset: 0x000E6610
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ServerKey>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CapabilityDescription>k__BackingField)) * -1521134295 + EqualityComparer<CustomConnectorTransport>.Default.GetHashCode(this.<Transport>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyDictionary<string, string>>.Default.GetHashCode(this.<Environment>k__BackingField);
    }

    // Token: 0x060003A3 RID: 931 RVA: 0x000E66A0 File Offset: 0x000E66A0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CustomConnectorRegistration);
    }

    // Token: 0x060003A4 RID: 932 RVA: 0x000E66B0 File Offset: 0x000E66B0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(CustomConnectorRegistration other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ServerKey>k__BackingField, other.<ServerKey>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CapabilityDescription>k__BackingField, other.<CapabilityDescription>k__BackingField) && EqualityComparer<CustomConnectorTransport>.Default.Equals(this.<Transport>k__BackingField, other.<Transport>k__BackingField) && EqualityComparer<IReadOnlyDictionary<string, string>>.Default.Equals(this.<Environment>k__BackingField, other.<Environment>k__BackingField));
    }

    // Token: 0x060003A6 RID: 934 RVA: 0x000E6760 File Offset: 0x000E6760
    [CompilerGenerated]
    private CustomConnectorRegistration(CustomConnectorRegistration original)
    {
      this.ServerKey = original.<ServerKey>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.CapabilityDescription = original.<CapabilityDescription>k__BackingField;
      this.Transport = original.<Transport>k__BackingField;
      this.Environment = original.<Environment>k__BackingField;
    }

    // Token: 0x060003A7 RID: 935 RVA: 0x000E67B0 File Offset: 0x000E67B0
    [CompilerGenerated]
    public void Deconstruct(out string ServerKey, out string DisplayName, out string CapabilityDescription, out CustomConnectorTransport Transport, out IReadOnlyDictionary<string, string> Environment)
    {
      ServerKey = this.ServerKey;
      DisplayName = this.DisplayName;
      CapabilityDescription = this.CapabilityDescription;
      Transport = this.Transport;
      Environment = this.Environment;
    }
  }
}
