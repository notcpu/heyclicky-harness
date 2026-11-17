using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000045 RID: 69
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class RegisteredDevice : IEquatable<RegisteredDevice>
  {
    // Token: 0x060002BD RID: 701 RVA: 0x00352018 File Offset: 0x00352018
    public RegisteredDevice(string Id, bool IsThisDevice, string Platform, string DisplayName, string Model, string OsVersion, string AppVersion, string AppBuild, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> Capabilities, string FirstSeenAt, string LastSeenAt, string LastActiveAt, int? MessagesLast14Days)
    {
      this.Id = Id;
      this.IsThisDevice = IsThisDevice;
      this.Platform = Platform;
      this.DisplayName = DisplayName;
      this.Model = Model;
      this.OsVersion = OsVersion;
      this.AppVersion = AppVersion;
      this.AppBuild = AppBuild;
      this.Capabilities = Capabilities;
      this.FirstSeenAt = FirstSeenAt;
      this.LastSeenAt = LastSeenAt;
      this.LastActiveAt = LastActiveAt;
      this.MessagesLast14Days = MessagesLast14Days;
      base..ctor();
    }

    // Token: 0x17000090 RID: 144
    // (get) Token: 0x060002BE RID: 702 RVA: 0x00352090 File Offset: 0x00352090
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(RegisteredDevice);
      }
    }

    // Token: 0x17000091 RID: 145
    // (get) Token: 0x060002BF RID: 703 RVA: 0x0035209C File Offset: 0x0035209C
    // (set) Token: 0x060002C0 RID: 704 RVA: 0x003520A4 File Offset: 0x003520A4
    public string Id { get; set; }

    // Token: 0x17000092 RID: 146
    // (get) Token: 0x060002C1 RID: 705 RVA: 0x003520B0 File Offset: 0x003520B0
    // (set) Token: 0x060002C2 RID: 706 RVA: 0x003520B8 File Offset: 0x003520B8
    public bool IsThisDevice { get; set; }

    // Token: 0x17000093 RID: 147
    // (get) Token: 0x060002C3 RID: 707 RVA: 0x003520C4 File Offset: 0x003520C4
    // (set) Token: 0x060002C4 RID: 708 RVA: 0x003520CC File Offset: 0x003520CC
    public string Platform { get; set; }

    // Token: 0x17000094 RID: 148
    // (get) Token: 0x060002C5 RID: 709 RVA: 0x003520D8 File Offset: 0x003520D8
    // (set) Token: 0x060002C6 RID: 710 RVA: 0x003520E0 File Offset: 0x003520E0
    public string DisplayName { get; set; }

    // Token: 0x17000095 RID: 149
    // (get) Token: 0x060002C7 RID: 711 RVA: 0x003520EC File Offset: 0x003520EC
    // (set) Token: 0x060002C8 RID: 712 RVA: 0x003520F4 File Offset: 0x003520F4
    public string Model { get; set; }

    // Token: 0x17000096 RID: 150
    // (get) Token: 0x060002C9 RID: 713 RVA: 0x00352100 File Offset: 0x00352100
    // (set) Token: 0x060002CA RID: 714 RVA: 0x00352108 File Offset: 0x00352108
    public string OsVersion { get; set; }

    // Token: 0x17000097 RID: 151
    // (get) Token: 0x060002CB RID: 715 RVA: 0x00352114 File Offset: 0x00352114
    // (set) Token: 0x060002CC RID: 716 RVA: 0x0035211C File Offset: 0x0035211C
    public string AppVersion { get; set; }

    // Token: 0x17000098 RID: 152
    // (get) Token: 0x060002CD RID: 717 RVA: 0x00352128 File Offset: 0x00352128
    // (set) Token: 0x060002CE RID: 718 RVA: 0x00352130 File Offset: 0x00352130
    public string AppBuild { get; set; }

    // Token: 0x17000099 RID: 153
    // (get) Token: 0x060002CF RID: 719 RVA: 0x0035213C File Offset: 0x0035213C
    // (set) Token: 0x060002D0 RID: 720 RVA: 0x00352144 File Offset: 0x00352144
    [Nullable(new byte[] { 2, 1 })]
    public IReadOnlyList<string> Capabilities
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700009A RID: 154
    // (get) Token: 0x060002D1 RID: 721 RVA: 0x00352150 File Offset: 0x00352150
    // (set) Token: 0x060002D2 RID: 722 RVA: 0x00352158 File Offset: 0x00352158
    public string FirstSeenAt { get; set; }

    // Token: 0x1700009B RID: 155
    // (get) Token: 0x060002D3 RID: 723 RVA: 0x00352164 File Offset: 0x00352164
    // (set) Token: 0x060002D4 RID: 724 RVA: 0x0035216C File Offset: 0x0035216C
    public string LastSeenAt { get; set; }

    // Token: 0x1700009C RID: 156
    // (get) Token: 0x060002D5 RID: 725 RVA: 0x00352178 File Offset: 0x00352178
    // (set) Token: 0x060002D6 RID: 726 RVA: 0x00352180 File Offset: 0x00352180
    public string LastActiveAt { get; set; }

    // Token: 0x1700009D RID: 157
    // (get) Token: 0x060002D7 RID: 727 RVA: 0x0035218C File Offset: 0x0035218C
    // (set) Token: 0x060002D8 RID: 728 RVA: 0x00352194 File Offset: 0x00352194
    public int? MessagesLast14Days { get; set; }

    // Token: 0x060002D9 RID: 729 RVA: 0x003521A0 File Offset: 0x003521A0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("RegisteredDevice");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060002DA RID: 730 RVA: 0x003521EC File Offset: 0x003521EC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", IsThisDevice = ");
      builder.Append(this.IsThisDevice.ToString());
      builder.Append(", Platform = ");
      builder.Append(this.Platform);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", Model = ");
      builder.Append(this.Model);
      builder.Append(", OsVersion = ");
      builder.Append(this.OsVersion);
      builder.Append(", AppVersion = ");
      builder.Append(this.AppVersion);
      builder.Append(", AppBuild = ");
      builder.Append(this.AppBuild);
      builder.Append(", Capabilities = ");
      builder.Append(this.Capabilities);
      builder.Append(", FirstSeenAt = ");
      builder.Append(this.FirstSeenAt);
      builder.Append(", LastSeenAt = ");
      builder.Append(this.LastSeenAt);
      builder.Append(", LastActiveAt = ");
      builder.Append(this.LastActiveAt);
      builder.Append(", MessagesLast14Days = ");
      builder.Append(this.MessagesLast14Days.ToString());
      return true;
    }

    // Token: 0x060002DB RID: 731 RVA: 0x00352360 File Offset: 0x00352360
    [CompilerGenerated]
    public static bool operator !=(RegisteredDevice left, RegisteredDevice right)
    {
      return !(left == right);
    }

    // Token: 0x060002DC RID: 732 RVA: 0x0035236C File Offset: 0x0035236C
    [CompilerGenerated]
    public static bool operator ==(RegisteredDevice left, RegisteredDevice right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060002DD RID: 733 RVA: 0x00352380 File Offset: 0x00352380
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsThisDevice>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Platform>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<OsVersion>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppVersion>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppBuild>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Capabilities>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FirstSeenAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LastSeenAt>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LastActiveAt>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<MessagesLast14Days>k__BackingField);
    }

    // Token: 0x060002DE RID: 734 RVA: 0x003524C8 File Offset: 0x003524C8
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RegisteredDevice);
    }

    // Token: 0x060002DF RID: 735 RVA: 0x003524D8 File Offset: 0x003524D8
    [CompilerGenerated]
    public bool Equals(RegisteredDevice other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsThisDevice>k__BackingField, other.<IsThisDevice>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Platform>k__BackingField, other.<Platform>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<OsVersion>k__BackingField, other.<OsVersion>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppVersion>k__BackingField, other.<AppVersion>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppBuild>k__BackingField, other.<AppBuild>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Capabilities>k__BackingField, other.<Capabilities>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FirstSeenAt>k__BackingField, other.<FirstSeenAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LastSeenAt>k__BackingField, other.<LastSeenAt>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LastActiveAt>k__BackingField, other.<LastActiveAt>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<MessagesLast14Days>k__BackingField, other.<MessagesLast14Days>k__BackingField));
    }

    // Token: 0x060002E1 RID: 737 RVA: 0x00352660 File Offset: 0x00352660
    [CompilerGenerated]
    private RegisteredDevice([Nullable(1)] RegisteredDevice original)
    {
      this.Id = original.<Id>k__BackingField;
      this.IsThisDevice = original.<IsThisDevice>k__BackingField;
      this.Platform = original.<Platform>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.Model = original.<Model>k__BackingField;
      this.OsVersion = original.<OsVersion>k__BackingField;
      this.AppVersion = original.<AppVersion>k__BackingField;
      this.AppBuild = original.<AppBuild>k__BackingField;
      this.Capabilities = original.<Capabilities>k__BackingField;
      this.FirstSeenAt = original.<FirstSeenAt>k__BackingField;
      this.LastSeenAt = original.<LastSeenAt>k__BackingField;
      this.LastActiveAt = original.<LastActiveAt>k__BackingField;
      this.MessagesLast14Days = original.<MessagesLast14Days>k__BackingField;
    }

    // Token: 0x060002E2 RID: 738 RVA: 0x00352710 File Offset: 0x00352710
    [CompilerGenerated]
    public void Deconstruct(out string Id, out bool IsThisDevice, out string Platform, out string DisplayName, out string Model, out string OsVersion, out string AppVersion, out string AppBuild, [Nullable(new byte[] { 2, 1 })] out IReadOnlyList<string> Capabilities, out string FirstSeenAt, out string LastSeenAt, out string LastActiveAt, out int? MessagesLast14Days)
    {
      Id = this.Id;
      IsThisDevice = this.IsThisDevice;
      Platform = this.Platform;
      DisplayName = this.DisplayName;
      Model = this.Model;
      OsVersion = this.OsVersion;
      AppVersion = this.AppVersion;
      AppBuild = this.AppBuild;
      Capabilities = this.Capabilities;
      FirstSeenAt = this.FirstSeenAt;
      LastSeenAt = this.LastSeenAt;
      LastActiveAt = this.LastActiveAt;
      MessagesLast14Days = this.MessagesLast14Days;
    }
  }
}
