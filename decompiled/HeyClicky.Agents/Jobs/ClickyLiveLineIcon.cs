using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x0200004A RID: 74
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class ClickyLiveLineIcon : IEquatable<ClickyLiveLineIcon>
  {
    // Token: 0x06000688 RID: 1672 RVA: 0x000F7014 File Offset: 0x000F7014
    public ClickyLiveLineIcon(string Host = null, string Toolkit = null, string Name = null, string LogoUrl = null)
    {
      this.Host = Host;
      this.Toolkit = Toolkit;
      this.Name = Name;
      this.LogoUrl = LogoUrl;
      base..ctor();
    }

    // Token: 0x170001AE RID: 430
    // (get) Token: 0x06000689 RID: 1673 RVA: 0x000F703C File Offset: 0x000F703C
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(ClickyLiveLineIcon);
      }
    }

    // Token: 0x170001AF RID: 431
    // (get) Token: 0x0600068A RID: 1674 RVA: 0x000F7048 File Offset: 0x000F7048
    // (set) Token: 0x0600068B RID: 1675 RVA: 0x000F7050 File Offset: 0x000F7050
    public string Host { get; set; }

    // Token: 0x170001B0 RID: 432
    // (get) Token: 0x0600068C RID: 1676 RVA: 0x000F705C File Offset: 0x000F705C
    // (set) Token: 0x0600068D RID: 1677 RVA: 0x000F7064 File Offset: 0x000F7064
    public string Toolkit { get; set; }

    // Token: 0x170001B1 RID: 433
    // (get) Token: 0x0600068E RID: 1678 RVA: 0x000F7070 File Offset: 0x000F7070
    // (set) Token: 0x0600068F RID: 1679 RVA: 0x000F7078 File Offset: 0x000F7078
    public string Name { get; set; }

    // Token: 0x170001B2 RID: 434
    // (get) Token: 0x06000690 RID: 1680 RVA: 0x000F7084 File Offset: 0x000F7084
    // (set) Token: 0x06000691 RID: 1681 RVA: 0x000F708C File Offset: 0x000F708C
    public string LogoUrl { get; set; }

    // Token: 0x06000692 RID: 1682 RVA: 0x000F7098 File Offset: 0x000F7098
    [NullableContext(1)]
    public static ClickyLiveLineIcon Site(string host)
    {
      return new ClickyLiveLineIcon(host, null, null, null);
    }

    // Token: 0x06000693 RID: 1683 RVA: 0x000F70A4 File Offset: 0x000F70A4
    [NullableContext(1)]
    public static ClickyLiveLineIcon App(string toolkit, string name, [Nullable(2)] string logoUrl)
    {
      return new ClickyLiveLineIcon(null, toolkit, name, logoUrl);
    }

    // Token: 0x06000694 RID: 1684 RVA: 0x000F70B0 File Offset: 0x000F70B0
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("ClickyLiveLineIcon");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000695 RID: 1685 RVA: 0x000F70FC File Offset: 0x000F70FC
    [NullableContext(1)]
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Host = ");
      builder.Append(this.Host);
      builder.Append(", Toolkit = ");
      builder.Append(this.Toolkit);
      builder.Append(", Name = ");
      builder.Append(this.Name);
      builder.Append(", LogoUrl = ");
      builder.Append(this.LogoUrl);
      return true;
    }

    // Token: 0x06000696 RID: 1686 RVA: 0x000F7174 File Offset: 0x000F7174
    [CompilerGenerated]
    public static bool operator !=(ClickyLiveLineIcon left, ClickyLiveLineIcon right)
    {
      return !(left == right);
    }

    // Token: 0x06000697 RID: 1687 RVA: 0x000F7180 File Offset: 0x000F7180
    [CompilerGenerated]
    public static bool operator ==(ClickyLiveLineIcon left, ClickyLiveLineIcon right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000698 RID: 1688 RVA: 0x000F7194 File Offset: 0x000F7194
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Host>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Toolkit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogoUrl>k__BackingField);
    }

    // Token: 0x06000699 RID: 1689 RVA: 0x000F7210 File Offset: 0x000F7210
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ClickyLiveLineIcon);
    }

    // Token: 0x0600069A RID: 1690 RVA: 0x000F7220 File Offset: 0x000F7220
    [CompilerGenerated]
    public bool Equals(ClickyLiveLineIcon other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Host>k__BackingField, other.<Host>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Toolkit>k__BackingField, other.<Toolkit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField));
    }

    // Token: 0x0600069C RID: 1692 RVA: 0x000F72B4 File Offset: 0x000F72B4
    [CompilerGenerated]
    private ClickyLiveLineIcon([Nullable(1)] ClickyLiveLineIcon original)
    {
      this.Host = original.<Host>k__BackingField;
      this.Toolkit = original.<Toolkit>k__BackingField;
      this.Name = original.<Name>k__BackingField;
      this.LogoUrl = original.<LogoUrl>k__BackingField;
    }

    // Token: 0x0600069D RID: 1693 RVA: 0x000F72EC File Offset: 0x000F72EC
    [CompilerGenerated]
    public void Deconstruct(out string Host, out string Toolkit, out string Name, out string LogoUrl)
    {
      Host = this.Host;
      Toolkit = this.Toolkit;
      Name = this.Name;
      LogoUrl = this.LogoUrl;
    }
  }
}
