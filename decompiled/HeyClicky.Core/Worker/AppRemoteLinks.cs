using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000037 RID: 55
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AppRemoteLinks : IEquatable<AppRemoteLinks>
  {
    // Token: 0x060001C2 RID: 450 RVA: 0x0034F2BC File Offset: 0x0034F2BC
    public AppRemoteLinks(string FeatureRequest, string CommunityReddit, string CommunityWhatsApp, string Changelog)
    {
      this.FeatureRequest = FeatureRequest;
      this.CommunityReddit = CommunityReddit;
      this.CommunityWhatsApp = CommunityWhatsApp;
      this.Changelog = Changelog;
      base..ctor();
    }

    // Token: 0x17000058 RID: 88
    // (get) Token: 0x060001C3 RID: 451 RVA: 0x0034F2E4 File Offset: 0x0034F2E4
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(AppRemoteLinks);
      }
    }

    // Token: 0x17000059 RID: 89
    // (get) Token: 0x060001C4 RID: 452 RVA: 0x0034F2F0 File Offset: 0x0034F2F0
    // (set) Token: 0x060001C5 RID: 453 RVA: 0x0034F2F8 File Offset: 0x0034F2F8
    public string FeatureRequest { get; set; }

    // Token: 0x1700005A RID: 90
    // (get) Token: 0x060001C6 RID: 454 RVA: 0x0034F304 File Offset: 0x0034F304
    // (set) Token: 0x060001C7 RID: 455 RVA: 0x0034F30C File Offset: 0x0034F30C
    public string CommunityReddit { get; set; }

    // Token: 0x1700005B RID: 91
    // (get) Token: 0x060001C8 RID: 456 RVA: 0x0034F318 File Offset: 0x0034F318
    // (set) Token: 0x060001C9 RID: 457 RVA: 0x0034F320 File Offset: 0x0034F320
    public string CommunityWhatsApp { get; set; }

    // Token: 0x1700005C RID: 92
    // (get) Token: 0x060001CA RID: 458 RVA: 0x0034F32C File Offset: 0x0034F32C
    // (set) Token: 0x060001CB RID: 459 RVA: 0x0034F334 File Offset: 0x0034F334
    public string Changelog { get; set; }

    // Token: 0x060001CC RID: 460 RVA: 0x0034F340 File Offset: 0x0034F340
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppRemoteLinks");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x060001CD RID: 461 RVA: 0x0034F38C File Offset: 0x0034F38C
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("FeatureRequest = ");
      builder.Append(this.FeatureRequest);
      builder.Append(", CommunityReddit = ");
      builder.Append(this.CommunityReddit);
      builder.Append(", CommunityWhatsApp = ");
      builder.Append(this.CommunityWhatsApp);
      builder.Append(", Changelog = ");
      builder.Append(this.Changelog);
      return true;
    }

    // Token: 0x060001CE RID: 462 RVA: 0x0034F404 File Offset: 0x0034F404
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(AppRemoteLinks left, AppRemoteLinks right)
    {
      return !(left == right);
    }

    // Token: 0x060001CF RID: 463 RVA: 0x0034F410 File Offset: 0x0034F410
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(AppRemoteLinks left, AppRemoteLinks right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x060001D0 RID: 464 RVA: 0x0034F424 File Offset: 0x0034F424
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FeatureRequest>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CommunityReddit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CommunityWhatsApp>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Changelog>k__BackingField);
    }

    // Token: 0x060001D1 RID: 465 RVA: 0x0034F4A0 File Offset: 0x0034F4A0
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppRemoteLinks);
    }

    // Token: 0x060001D2 RID: 466 RVA: 0x0034F4B0 File Offset: 0x0034F4B0
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(AppRemoteLinks other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<FeatureRequest>k__BackingField, other.<FeatureRequest>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CommunityReddit>k__BackingField, other.<CommunityReddit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CommunityWhatsApp>k__BackingField, other.<CommunityWhatsApp>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Changelog>k__BackingField, other.<Changelog>k__BackingField));
    }

    // Token: 0x060001D4 RID: 468 RVA: 0x0034F544 File Offset: 0x0034F544
    [CompilerGenerated]
    private AppRemoteLinks(AppRemoteLinks original)
    {
      this.FeatureRequest = original.<FeatureRequest>k__BackingField;
      this.CommunityReddit = original.<CommunityReddit>k__BackingField;
      this.CommunityWhatsApp = original.<CommunityWhatsApp>k__BackingField;
      this.Changelog = original.<Changelog>k__BackingField;
    }

    // Token: 0x060001D5 RID: 469 RVA: 0x0034F57C File Offset: 0x0034F57C
    [CompilerGenerated]
    public void Deconstruct(out string FeatureRequest, out string CommunityReddit, out string CommunityWhatsApp, out string Changelog)
    {
      FeatureRequest = this.FeatureRequest;
      CommunityReddit = this.CommunityReddit;
      CommunityWhatsApp = this.CommunityWhatsApp;
      Changelog = this.Changelog;
    }

    // Token: 0x040001C6 RID: 454
    public static readonly AppRemoteLinks BakedInDefaults = new AppRemoteLinks("https://heyclicky.canny.io/feature-requests", "https://www.reddit.com/r/heyclicky_", "https://chat.whatsapp.com/KNyXe8etUWmFCdym6OWW8C?mode=gi_t", "https://heyclicky.com/changelog?p=windows");
  }
}
