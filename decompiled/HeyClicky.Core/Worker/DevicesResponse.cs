using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000047 RID: 71
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DevicesResponse : IEquatable<DevicesResponse>
  {
    // Token: 0x060002F1 RID: 753 RVA: 0x003528F0 File Offset: 0x003528F0
    public DevicesResponse([Nullable(new byte[] { 2, 1 })] IReadOnlyList<RegisteredDevice> Devices)
    {
      this.Devices = Devices;
      base..ctor();
    }

    // Token: 0x170000A0 RID: 160
    // (get) Token: 0x060002F2 RID: 754 RVA: 0x00352900 File Offset: 0x00352900
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DevicesResponse);
      }
    }

    // Token: 0x170000A1 RID: 161
    // (get) Token: 0x060002F3 RID: 755 RVA: 0x0035290C File Offset: 0x0035290C
    // (set) Token: 0x060002F4 RID: 756 RVA: 0x00352914 File Offset: 0x00352914
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<RegisteredDevice> Devices
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x060002F5 RID: 757 RVA: 0x00352920 File Offset: 0x00352920
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DevicesResponse");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060002F6 RID: 758 RVA: 0x0035296C File Offset: 0x0035296C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Devices = ");
      builder.Append(this.Devices);
      return true;
    }

    // Token: 0x060002F7 RID: 759 RVA: 0x00352990 File Offset: 0x00352990
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DevicesResponse left, DevicesResponse right)
    {
      return !(left == right);
    }

    // Token: 0x060002F8 RID: 760 RVA: 0x0035299C File Offset: 0x0035299C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DevicesResponse left, DevicesResponse right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060002F9 RID: 761 RVA: 0x003529B0 File Offset: 0x003529B0
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<IReadOnlyList<RegisteredDevice>>.Default.GetHashCode(this.<Devices>k__BackingField);
    }

    // Token: 0x060002FA RID: 762 RVA: 0x003529DC File Offset: 0x003529DC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DevicesResponse);
    }

    // Token: 0x060002FB RID: 763 RVA: 0x003529EC File Offset: 0x003529EC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DevicesResponse other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<IReadOnlyList<RegisteredDevice>>.Default.Equals(this.<Devices>k__BackingField, other.<Devices>k__BackingField));
    }

    // Token: 0x060002FD RID: 765 RVA: 0x00352A2C File Offset: 0x00352A2C
    [CompilerGenerated]
    private DevicesResponse(DevicesResponse original)
    {
      this.Devices = original.<Devices>k__BackingField;
    }

    // Token: 0x060002FE RID: 766 RVA: 0x00352A40 File Offset: 0x00352A40
    [CompilerGenerated]
    public void Deconstruct([Nullable(new byte[] { 2, 1 })] out IReadOnlyList<RegisteredDevice> Devices)
    {
      Devices = this.Devices;
    }
  }
}
