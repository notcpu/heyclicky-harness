using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003B RID: 59
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class DeviceRegistrationStamp : IEquatable<DeviceRegistrationStamp>
  {
    // Token: 0x06000201 RID: 513 RVA: 0x00350360 File Offset: 0x00350360
    public DeviceRegistrationStamp(string Fingerprint, long RegisteredAtEpochSeconds)
    {
      this.Fingerprint = Fingerprint;
      this.RegisteredAtEpochSeconds = RegisteredAtEpochSeconds;
      base..ctor();
    }

    // Token: 0x17000060 RID: 96
    // (get) Token: 0x06000202 RID: 514 RVA: 0x00350378 File Offset: 0x00350378
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(DeviceRegistrationStamp);
      }
    }

    // Token: 0x17000061 RID: 97
    // (get) Token: 0x06000203 RID: 515 RVA: 0x00350384 File Offset: 0x00350384
    // (set) Token: 0x06000204 RID: 516 RVA: 0x0035038C File Offset: 0x0035038C
    public string Fingerprint { get; set; }

    // Token: 0x17000062 RID: 98
    // (get) Token: 0x06000205 RID: 517 RVA: 0x00350398 File Offset: 0x00350398
    // (set) Token: 0x06000206 RID: 518 RVA: 0x003503A0 File Offset: 0x003503A0
    public long RegisteredAtEpochSeconds { get; set; }

    // Token: 0x06000207 RID: 519 RVA: 0x003503AC File Offset: 0x003503AC
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DeviceRegistrationStamp");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000208 RID: 520 RVA: 0x003503F8 File Offset: 0x003503F8
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Fingerprint = ");
      builder.Append(this.Fingerprint);
      builder.Append(", RegisteredAtEpochSeconds = ");
      builder.Append(this.RegisteredAtEpochSeconds.ToString());
      return true;
    }

    // Token: 0x06000209 RID: 521 RVA: 0x0035044C File Offset: 0x0035044C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(DeviceRegistrationStamp left, DeviceRegistrationStamp right)
    {
      return !(left == right);
    }

    // Token: 0x0600020A RID: 522 RVA: 0x00350458 File Offset: 0x00350458
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(DeviceRegistrationStamp left, DeviceRegistrationStamp right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x0600020B RID: 523 RVA: 0x0035046C File Offset: 0x0035046C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Fingerprint>k__BackingField)) * -1521134295 + EqualityComparer<long>.Default.GetHashCode(this.<RegisteredAtEpochSeconds>k__BackingField);
    }

    // Token: 0x0600020C RID: 524 RVA: 0x003504AC File Offset: 0x003504AC
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as DeviceRegistrationStamp);
    }

    // Token: 0x0600020D RID: 525 RVA: 0x003504BC File Offset: 0x003504BC
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(DeviceRegistrationStamp other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Fingerprint>k__BackingField, other.<Fingerprint>k__BackingField) && EqualityComparer<long>.Default.Equals(this.<RegisteredAtEpochSeconds>k__BackingField, other.<RegisteredAtEpochSeconds>k__BackingField));
    }

    // Token: 0x0600020F RID: 527 RVA: 0x00350520 File Offset: 0x00350520
    [CompilerGenerated]
    private DeviceRegistrationStamp(DeviceRegistrationStamp original)
    {
      this.Fingerprint = original.<Fingerprint>k__BackingField;
      this.RegisteredAtEpochSeconds = original.<RegisteredAtEpochSeconds>k__BackingField;
    }

    // Token: 0x06000210 RID: 528 RVA: 0x00350540 File Offset: 0x00350540
    [CompilerGenerated]
    public void Deconstruct(out string Fingerprint, out long RegisteredAtEpochSeconds)
    {
      Fingerprint = this.Fingerprint;
      RegisteredAtEpochSeconds = this.RegisteredAtEpochSeconds;
    }
  }
}
