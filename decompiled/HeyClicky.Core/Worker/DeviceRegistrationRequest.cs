using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000044 RID: 68
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class DeviceRegistrationRequest : IEquatable<DeviceRegistrationRequest>
  {
    // Token: 0x060002AB RID: 683 RVA: 0x00351DAC File Offset: 0x00351DAC
    public DeviceRegistrationRequest([Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> Capabilities, string DisplayName, string Model)
    {
      this.Capabilities = Capabilities;
      this.DisplayName = DisplayName;
      this.Model = Model;
      base..ctor();
    }

    // Token: 0x1700008C RID: 140
    // (get) Token: 0x060002AC RID: 684 RVA: 0x00351DCC File Offset: 0x00351DCC
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(DeviceRegistrationRequest);
      }
    }

    // Token: 0x1700008D RID: 141
    // (get) Token: 0x060002AD RID: 685 RVA: 0x00351DD8 File Offset: 0x00351DD8
    // (set) Token: 0x060002AE RID: 686 RVA: 0x00351DE0 File Offset: 0x00351DE0
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> Capabilities
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700008E RID: 142
    // (get) Token: 0x060002AF RID: 687 RVA: 0x00351DEC File Offset: 0x00351DEC
    // (set) Token: 0x060002B0 RID: 688 RVA: 0x00351DF4 File Offset: 0x00351DF4
    public string DisplayName { get; set; }

    // Token: 0x1700008F RID: 143
    // (get) Token: 0x060002B1 RID: 689 RVA: 0x00351E00 File Offset: 0x00351E00
    // (set) Token: 0x060002B2 RID: 690 RVA: 0x00351E08 File Offset: 0x00351E08
    public string Model { get; set; }

    // Token: 0x060002B3 RID: 691 RVA: 0x00351E14 File Offset: 0x00351E14
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DeviceRegistrationRequest");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060002B4 RID: 692 RVA: 0x00351E60 File Offset: 0x00351E60
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Capabilities = ");
      builder.Append(this.Capabilities);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", Model = ");
      builder.Append(this.Model);
      return true;
    }

    // Token: 0x060002B5 RID: 693 RVA: 0x00351EC0 File Offset: 0x00351EC0
    [CompilerGenerated]
    public static bool operator !=(DeviceRegistrationRequest left, DeviceRegistrationRequest right)
    {
      return !(left == right);
    }

    // Token: 0x060002B6 RID: 694 RVA: 0x00351ECC File Offset: 0x00351ECC
    [CompilerGenerated]
    public static bool operator ==(DeviceRegistrationRequest left, DeviceRegistrationRequest right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060002B7 RID: 695 RVA: 0x00351EE0 File Offset: 0x00351EE0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Capabilities>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField);
    }

    // Token: 0x060002B8 RID: 696 RVA: 0x00351F44 File Offset: 0x00351F44
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DeviceRegistrationRequest);
    }

    // Token: 0x060002B9 RID: 697 RVA: 0x00351F54 File Offset: 0x00351F54
    [CompilerGenerated]
    public bool Equals(DeviceRegistrationRequest other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Capabilities>k__BackingField, other.<Capabilities>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField));
    }

    // Token: 0x060002BB RID: 699 RVA: 0x00351FD0 File Offset: 0x00351FD0
    [CompilerGenerated]
    private DeviceRegistrationRequest([Nullable(1)] DeviceRegistrationRequest original)
    {
      this.Capabilities = original.<Capabilities>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.Model = original.<Model>k__BackingField;
    }

    // Token: 0x060002BC RID: 700 RVA: 0x00351FFC File Offset: 0x00351FFC
    [CompilerGenerated]
    public void Deconstruct([Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> Capabilities, out string DisplayName, out string Model)
    {
      Capabilities = this.Capabilities;
      DisplayName = this.DisplayName;
      Model = this.Model;
    }
  }
}
