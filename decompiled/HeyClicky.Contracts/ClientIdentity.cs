using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky
{
  // Token: 0x02000004 RID: 4
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClientIdentity : IEquatable<ClientIdentity>
  {
    // Token: 0x06000019 RID: 25 RVA: 0x0001C518 File Offset: 0x0001C518
    public ClientIdentity(string Platform, string DeviceId, string AppVersion, string AppBuild, string OsVersion, IReadOnlyList<string> Capabilities)
    {
      this.Platform = Platform;
      this.DeviceId = DeviceId;
      this.AppVersion = AppVersion;
      this.AppBuild = AppBuild;
      this.OsVersion = OsVersion;
      this.Capabilities = Capabilities;
      base..ctor();
    }

    // Token: 0x17000008 RID: 8
    // (get) Token: 0x0600001A RID: 26 RVA: 0x0001C550 File Offset: 0x0001C550
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(ClientIdentity);
      }
    }

    // Token: 0x17000009 RID: 9
    // (get) Token: 0x0600001B RID: 27 RVA: 0x0001C55C File Offset: 0x0001C55C
    // (set) Token: 0x0600001C RID: 28 RVA: 0x0001C564 File Offset: 0x0001C564
    public string Platform { get; set; }

    // Token: 0x1700000A RID: 10
    // (get) Token: 0x0600001D RID: 29 RVA: 0x0001C570 File Offset: 0x0001C570
    // (set) Token: 0x0600001E RID: 30 RVA: 0x0001C578 File Offset: 0x0001C578
    public string DeviceId { get; set; }

    // Token: 0x1700000B RID: 11
    // (get) Token: 0x0600001F RID: 31 RVA: 0x0001C584 File Offset: 0x0001C584
    // (set) Token: 0x06000020 RID: 32 RVA: 0x0001C58C File Offset: 0x0001C58C
    public string AppVersion { get; set; }

    // Token: 0x1700000C RID: 12
    // (get) Token: 0x06000021 RID: 33 RVA: 0x0001C598 File Offset: 0x0001C598
    // (set) Token: 0x06000022 RID: 34 RVA: 0x0001C5A0 File Offset: 0x0001C5A0
    public string AppBuild { get; set; }

    // Token: 0x1700000D RID: 13
    // (get) Token: 0x06000023 RID: 35 RVA: 0x0001C5AC File Offset: 0x0001C5AC
    // (set) Token: 0x06000024 RID: 36 RVA: 0x0001C5B4 File Offset: 0x0001C5B4
    public string OsVersion { get; set; }

    // Token: 0x1700000E RID: 14
    // (get) Token: 0x06000025 RID: 37 RVA: 0x0001C5C0 File Offset: 0x0001C5C0
    // (set) Token: 0x06000026 RID: 38 RVA: 0x0001C5C8 File Offset: 0x0001C5C8
    public IReadOnlyList<string> Capabilities { get; set; }

    // Token: 0x06000027 RID: 39 RVA: 0x0001C5D4 File Offset: 0x0001C5D4
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClientIdentity");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000028 RID: 40 RVA: 0x0001C620 File Offset: 0x0001C620
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Platform = ");
      builder.Append(this.Platform);
      builder.Append(", DeviceId = ");
      builder.Append(this.DeviceId);
      builder.Append(", AppVersion = ");
      builder.Append(this.AppVersion);
      builder.Append(", AppBuild = ");
      builder.Append(this.AppBuild);
      builder.Append(", OsVersion = ");
      builder.Append(this.OsVersion);
      builder.Append(", Capabilities = ");
      builder.Append(this.Capabilities);
      return true;
    }

    // Token: 0x06000029 RID: 41 RVA: 0x0001C6CC File Offset: 0x0001C6CC
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(ClientIdentity left, ClientIdentity right)
    {
      return !(left == right);
    }

    // Token: 0x0600002A RID: 42 RVA: 0x0001C6D8 File Offset: 0x0001C6D8
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(ClientIdentity left, ClientIdentity right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600002B RID: 43 RVA: 0x0001C6EC File Offset: 0x0001C6EC
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Platform>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DeviceId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppVersion>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppBuild>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OsVersion>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Capabilities>k__BackingField);
    }

    // Token: 0x0600002C RID: 44 RVA: 0x0001C794 File Offset: 0x0001C794
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClientIdentity);
    }

    // Token: 0x0600002D RID: 45 RVA: 0x0001C7A4 File Offset: 0x0001C7A4
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(ClientIdentity other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Platform>k__BackingField, other.<Platform>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DeviceId>k__BackingField, other.<DeviceId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppVersion>k__BackingField, other.<AppVersion>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppBuild>k__BackingField, other.<AppBuild>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OsVersion>k__BackingField, other.<OsVersion>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Capabilities>k__BackingField, other.<Capabilities>k__BackingField));
    }

    // Token: 0x0600002F RID: 47 RVA: 0x0001C870 File Offset: 0x0001C870
    [CompilerGenerated]
    private ClientIdentity(ClientIdentity original)
    {
      this.Platform = original.<Platform>k__BackingField;
      this.DeviceId = original.<DeviceId>k__BackingField;
      this.AppVersion = original.<AppVersion>k__BackingField;
      this.AppBuild = original.<AppBuild>k__BackingField;
      this.OsVersion = original.<OsVersion>k__BackingField;
      this.Capabilities = original.<Capabilities>k__BackingField;
    }

    // Token: 0x06000030 RID: 48 RVA: 0x0001C8CC File Offset: 0x0001C8CC
    [CompilerGenerated]
    public void Deconstruct(out string Platform, out string DeviceId, out string AppVersion, out string AppBuild, out string OsVersion, out IReadOnlyList<string> Capabilities)
    {
      Platform = this.Platform;
      DeviceId = this.DeviceId;
      AppVersion = this.AppVersion;
      AppBuild = this.AppBuild;
      OsVersion = this.OsVersion;
      Capabilities = this.Capabilities;
    }
  }
}
