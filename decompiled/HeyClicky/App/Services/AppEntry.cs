using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E0 RID: 224
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppEntry : IEquatable<AppEntry>
  {
    // Token: 0x06000ABA RID: 2746 RVA: 0x00558830 File Offset: 0x00558830
    public AppEntry(string Id, string DisplayName, [Nullable(2)] string ToolkitSlug, [Nullable(2)] string LogoUrl, string Status, string Description, bool IsLocal = false)
    {
      this.Id = Id;
      this.DisplayName = DisplayName;
      this.ToolkitSlug = ToolkitSlug;
      this.LogoUrl = LogoUrl;
      this.Status = Status;
      this.Description = Description;
      this.IsLocal = IsLocal;
      base..ctor();
    }

    // Token: 0x170001C0 RID: 448
    // (get) Token: 0x06000ABB RID: 2747 RVA: 0x00558870 File Offset: 0x00558870
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AppEntry);
      }
    }

    // Token: 0x170001C1 RID: 449
    // (get) Token: 0x06000ABC RID: 2748 RVA: 0x0055887C File Offset: 0x0055887C
    // (set) Token: 0x06000ABD RID: 2749 RVA: 0x00558884 File Offset: 0x00558884
    public string Id { get; set; }

    // Token: 0x170001C2 RID: 450
    // (get) Token: 0x06000ABE RID: 2750 RVA: 0x00558890 File Offset: 0x00558890
    // (set) Token: 0x06000ABF RID: 2751 RVA: 0x00558898 File Offset: 0x00558898
    public string DisplayName { get; set; }

    // Token: 0x170001C3 RID: 451
    // (get) Token: 0x06000AC0 RID: 2752 RVA: 0x005588A4 File Offset: 0x005588A4
    // (set) Token: 0x06000AC1 RID: 2753 RVA: 0x005588AC File Offset: 0x005588AC
    [Nullable(2)]
    public string ToolkitSlug
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001C4 RID: 452
    // (get) Token: 0x06000AC2 RID: 2754 RVA: 0x005588B8 File Offset: 0x005588B8
    // (set) Token: 0x06000AC3 RID: 2755 RVA: 0x005588C0 File Offset: 0x005588C0
    [Nullable(2)]
    public string LogoUrl
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x170001C5 RID: 453
    // (get) Token: 0x06000AC4 RID: 2756 RVA: 0x005588CC File Offset: 0x005588CC
    // (set) Token: 0x06000AC5 RID: 2757 RVA: 0x005588D4 File Offset: 0x005588D4
    public string Status { get; set; }

    // Token: 0x170001C6 RID: 454
    // (get) Token: 0x06000AC6 RID: 2758 RVA: 0x005588E0 File Offset: 0x005588E0
    // (set) Token: 0x06000AC7 RID: 2759 RVA: 0x005588E8 File Offset: 0x005588E8
    public string Description { get; set; }

    // Token: 0x170001C7 RID: 455
    // (get) Token: 0x06000AC8 RID: 2760 RVA: 0x005588F4 File Offset: 0x005588F4
    // (set) Token: 0x06000AC9 RID: 2761 RVA: 0x005588FC File Offset: 0x005588FC
    public bool IsLocal { get; set; }

    // Token: 0x170001C8 RID: 456
    // (get) Token: 0x06000ACA RID: 2762 RVA: 0x00558908 File Offset: 0x00558908
    public bool IsConnected
    {
      get
      {
        string status = this.Status;
        return status == "connected" || status == "partial";
      }
    }

    // Token: 0x06000ACB RID: 2763 RVA: 0x00558940 File Offset: 0x00558940
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppEntry");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000ACC RID: 2764 RVA: 0x0055898C File Offset: 0x0055898C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Id = ");
      builder.Append(this.Id);
      builder.Append(", DisplayName = ");
      builder.Append(this.DisplayName);
      builder.Append(", ToolkitSlug = ");
      builder.Append(this.ToolkitSlug);
      builder.Append(", LogoUrl = ");
      builder.Append(this.LogoUrl);
      builder.Append(", Status = ");
      builder.Append(this.Status);
      builder.Append(", Description = ");
      builder.Append(this.Description);
      builder.Append(", IsLocal = ");
      builder.Append(this.IsLocal.ToString());
      builder.Append(", IsConnected = ");
      builder.Append(this.IsConnected.ToString());
      return true;
    }

    // Token: 0x06000ACD RID: 2765 RVA: 0x00558A84 File Offset: 0x00558A84
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AppEntry left, AppEntry right)
    {
      return !(left == right);
    }

    // Token: 0x06000ACE RID: 2766 RVA: 0x00558A90 File Offset: 0x00558A90
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AppEntry left, AppEntry right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000ACF RID: 2767 RVA: 0x00558AA4 File Offset: 0x00558AA4
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<DisplayName>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ToolkitSlug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<LogoUrl>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Status>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Description>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLocal>k__BackingField);
    }

    // Token: 0x06000AD0 RID: 2768 RVA: 0x00558B64 File Offset: 0x00558B64
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppEntry);
    }

    // Token: 0x06000AD1 RID: 2769 RVA: 0x00558B74 File Offset: 0x00558B74
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AppEntry other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<DisplayName>k__BackingField, other.<DisplayName>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ToolkitSlug>k__BackingField, other.<ToolkitSlug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<LogoUrl>k__BackingField, other.<LogoUrl>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Status>k__BackingField, other.<Status>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Description>k__BackingField, other.<Description>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLocal>k__BackingField, other.<IsLocal>k__BackingField));
    }

    // Token: 0x06000AD3 RID: 2771 RVA: 0x00558C5C File Offset: 0x00558C5C
    [CompilerGenerated]
    private AppEntry(AppEntry original)
    {
      this.Id = original.<Id>k__BackingField;
      this.DisplayName = original.<DisplayName>k__BackingField;
      this.ToolkitSlug = original.<ToolkitSlug>k__BackingField;
      this.LogoUrl = original.<LogoUrl>k__BackingField;
      this.Status = original.<Status>k__BackingField;
      this.Description = original.<Description>k__BackingField;
      this.IsLocal = original.<IsLocal>k__BackingField;
    }

    // Token: 0x06000AD4 RID: 2772 RVA: 0x00558CC4 File Offset: 0x00558CC4
    [CompilerGenerated]
    public void Deconstruct(out string Id, out string DisplayName, [Nullable(2)] out string ToolkitSlug, [Nullable(2)] out string LogoUrl, out string Status, out string Description, out bool IsLocal)
    {
      Id = this.Id;
      DisplayName = this.DisplayName;
      ToolkitSlug = this.ToolkitSlug;
      LogoUrl = this.LogoUrl;
      Status = this.Status;
      Description = this.Description;
      IsLocal = this.IsLocal;
    }
  }
}
