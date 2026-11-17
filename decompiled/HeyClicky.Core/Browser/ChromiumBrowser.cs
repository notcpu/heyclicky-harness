using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E4 RID: 484
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ChromiumBrowser : IEquatable<ChromiumBrowser>
  {
    // Token: 0x06001BCF RID: 7119 RVA: 0x003DE704 File Offset: 0x003DE704
    public ChromiumBrowser([Nullable(1)] string Name, string RegistryPath, [Nullable(1)] string UserDataFolder, [Nullable(1)] IReadOnlyList<string> Executables, string AppPathsName = null, string PackageFamilyPrefix = null, string ProgIdPrefix = null)
    {
      this.Name = Name;
      this.RegistryPath = RegistryPath;
      this.UserDataFolder = UserDataFolder;
      this.Executables = Executables;
      this.AppPathsName = AppPathsName;
      this.PackageFamilyPrefix = PackageFamilyPrefix;
      this.ProgIdPrefix = ProgIdPrefix;
      base..ctor();
    }

    // Token: 0x17000623 RID: 1571
    // (get) Token: 0x06001BD0 RID: 7120 RVA: 0x003DE744 File Offset: 0x003DE744
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ChromiumBrowser);
      }
    }

    // Token: 0x17000624 RID: 1572
    // (get) Token: 0x06001BD1 RID: 7121 RVA: 0x003DE750 File Offset: 0x003DE750
    // (set) Token: 0x06001BD2 RID: 7122 RVA: 0x003DE758 File Offset: 0x003DE758
    [Nullable(1)]
    public string Name
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000625 RID: 1573
    // (get) Token: 0x06001BD3 RID: 7123 RVA: 0x003DE764 File Offset: 0x003DE764
    // (set) Token: 0x06001BD4 RID: 7124 RVA: 0x003DE76C File Offset: 0x003DE76C
    public string RegistryPath { get; set; }

    // Token: 0x17000626 RID: 1574
    // (get) Token: 0x06001BD5 RID: 7125 RVA: 0x003DE778 File Offset: 0x003DE778
    // (set) Token: 0x06001BD6 RID: 7126 RVA: 0x003DE780 File Offset: 0x003DE780
    [Nullable(1)]
    public string UserDataFolder
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000627 RID: 1575
    // (get) Token: 0x06001BD7 RID: 7127 RVA: 0x003DE78C File Offset: 0x003DE78C
    // (set) Token: 0x06001BD8 RID: 7128 RVA: 0x003DE794 File Offset: 0x003DE794
    [Nullable(1)]
    public IReadOnlyList<string> Executables
    {
      [NullableContext(1)]
      get;
      [NullableContext(1)]
      set;
    }

    // Token: 0x17000628 RID: 1576
    // (get) Token: 0x06001BD9 RID: 7129 RVA: 0x003DE7A0 File Offset: 0x003DE7A0
    // (set) Token: 0x06001BDA RID: 7130 RVA: 0x003DE7A8 File Offset: 0x003DE7A8
    public string AppPathsName { get; set; }

    // Token: 0x17000629 RID: 1577
    // (get) Token: 0x06001BDB RID: 7131 RVA: 0x003DE7B4 File Offset: 0x003DE7B4
    // (set) Token: 0x06001BDC RID: 7132 RVA: 0x003DE7BC File Offset: 0x003DE7BC
    public string PackageFamilyPrefix { get; set; }

    // Token: 0x1700062A RID: 1578
    // (get) Token: 0x06001BDD RID: 7133 RVA: 0x003DE7C8 File Offset: 0x003DE7C8
    // (set) Token: 0x06001BDE RID: 7134 RVA: 0x003DE7D0 File Offset: 0x003DE7D0
    public string ProgIdPrefix { get; set; }

    // Token: 0x06001BDF RID: 7135 RVA: 0x003DE7DC File Offset: 0x003DE7DC
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ChromiumBrowser");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001BE0 RID: 7136 RVA: 0x003DE828 File Offset: 0x003DE828
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Name = ");
      builder.Append(this.Name);
      builder.Append(", RegistryPath = ");
      builder.Append(this.RegistryPath);
      builder.Append(", UserDataFolder = ");
      builder.Append(this.UserDataFolder);
      builder.Append(", Executables = ");
      builder.Append(this.Executables);
      builder.Append(", AppPathsName = ");
      builder.Append(this.AppPathsName);
      builder.Append(", PackageFamilyPrefix = ");
      builder.Append(this.PackageFamilyPrefix);
      builder.Append(", ProgIdPrefix = ");
      builder.Append(this.ProgIdPrefix);
      return true;
    }

    // Token: 0x06001BE1 RID: 7137 RVA: 0x003DE8EC File Offset: 0x003DE8EC
    [CompilerGenerated]
    public static bool operator !=(ChromiumBrowser left, ChromiumBrowser right)
    {
      return !(left == right);
    }

    // Token: 0x06001BE2 RID: 7138 RVA: 0x003DE8F8 File Offset: 0x003DE8F8
    [CompilerGenerated]
    public static bool operator ==(ChromiumBrowser left, ChromiumBrowser right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001BE3 RID: 7139 RVA: 0x003DE90C File Offset: 0x003DE90C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<RegistryPath>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<UserDataFolder>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<Executables>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AppPathsName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<PackageFamilyPrefix>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ProgIdPrefix>k__BackingField);
    }

    // Token: 0x06001BE4 RID: 7140 RVA: 0x003DE9CC File Offset: 0x003DE9CC
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ChromiumBrowser);
    }

    // Token: 0x06001BE5 RID: 7141 RVA: 0x003DE9DC File Offset: 0x003DE9DC
    [CompilerGenerated]
    public bool Equals(ChromiumBrowser other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<RegistryPath>k__BackingField, other.<RegistryPath>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<UserDataFolder>k__BackingField, other.<UserDataFolder>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<Executables>k__BackingField, other.<Executables>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AppPathsName>k__BackingField, other.<AppPathsName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<PackageFamilyPrefix>k__BackingField, other.<PackageFamilyPrefix>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ProgIdPrefix>k__BackingField, other.<ProgIdPrefix>k__BackingField));
    }

    // Token: 0x06001BE7 RID: 7143 RVA: 0x003DEAC4 File Offset: 0x003DEAC4
    [CompilerGenerated]
    private ChromiumBrowser([Nullable(1)] ChromiumBrowser original)
    {
      this.Name = original.<Name>k__BackingField;
      this.RegistryPath = original.<RegistryPath>k__BackingField;
      this.UserDataFolder = original.<UserDataFolder>k__BackingField;
      this.Executables = original.<Executables>k__BackingField;
      this.AppPathsName = original.<AppPathsName>k__BackingField;
      this.PackageFamilyPrefix = original.<PackageFamilyPrefix>k__BackingField;
      this.ProgIdPrefix = original.<ProgIdPrefix>k__BackingField;
    }

    // Token: 0x06001BE8 RID: 7144 RVA: 0x003DEB2C File Offset: 0x003DEB2C
    [CompilerGenerated]
    public void Deconstruct([Nullable(1)] out string Name, out string RegistryPath, [Nullable(1)] out string UserDataFolder, [Nullable(1)] out IReadOnlyList<string> Executables, out string AppPathsName, out string PackageFamilyPrefix, out string ProgIdPrefix)
    {
      Name = this.Name;
      RegistryPath = this.RegistryPath;
      UserDataFolder = this.UserDataFolder;
      Executables = this.Executables;
      AppPathsName = this.AppPathsName;
      PackageFamilyPrefix = this.PackageFamilyPrefix;
      ProgIdPrefix = this.ProgIdPrefix;
    }
  }
}
