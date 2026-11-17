using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000046 RID: 70
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class DeviceResponse : IEquatable<DeviceResponse>
  {
    // Token: 0x060002E3 RID: 739 RVA: 0x00352794 File Offset: 0x00352794
    public DeviceResponse(RegisteredDevice Device)
    {
      this.Device = Device;
      base..ctor();
    }

    // Token: 0x1700009E RID: 158
    // (get) Token: 0x060002E4 RID: 740 RVA: 0x003527A4 File Offset: 0x003527A4
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(DeviceResponse);
      }
    }

    // Token: 0x1700009F RID: 159
    // (get) Token: 0x060002E5 RID: 741 RVA: 0x003527B0 File Offset: 0x003527B0
    // (set) Token: 0x060002E6 RID: 742 RVA: 0x003527B8 File Offset: 0x003527B8
    public RegisteredDevice Device { get; set; }

    // Token: 0x060002E7 RID: 743 RVA: 0x003527C4 File Offset: 0x003527C4
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DeviceResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060002E8 RID: 744 RVA: 0x00352810 File Offset: 0x00352810
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Device = ");
      builder.Append(this.Device);
      return true;
    }

    // Token: 0x060002E9 RID: 745 RVA: 0x00352834 File Offset: 0x00352834
    [CompilerGenerated]
    public static bool operator !=(DeviceResponse left, DeviceResponse right)
    {
      return !(left == right);
    }

    // Token: 0x060002EA RID: 746 RVA: 0x00352840 File Offset: 0x00352840
    [CompilerGenerated]
    public static bool operator ==(DeviceResponse left, DeviceResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060002EB RID: 747 RVA: 0x00352854 File Offset: 0x00352854
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<RegisteredDevice>.Default.GetHashCode(this.<Device>k__BackingField);
    }

    // Token: 0x060002EC RID: 748 RVA: 0x00352880 File Offset: 0x00352880
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DeviceResponse);
    }

    // Token: 0x060002ED RID: 749 RVA: 0x00352890 File Offset: 0x00352890
    [CompilerGenerated]
    public bool Equals(DeviceResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<RegisteredDevice>.Default.Equals(this.<Device>k__BackingField, other.<Device>k__BackingField));
    }

    // Token: 0x060002EF RID: 751 RVA: 0x003528D0 File Offset: 0x003528D0
    [CompilerGenerated]
    private DeviceResponse([Nullable(1)] DeviceResponse original)
    {
      this.Device = original.<Device>k__BackingField;
    }

    // Token: 0x060002F0 RID: 752 RVA: 0x003528E4 File Offset: 0x003528E4
    [CompilerGenerated]
    public void Deconstruct(out RegisteredDevice Device)
    {
      Device = this.Device;
    }
  }
}
