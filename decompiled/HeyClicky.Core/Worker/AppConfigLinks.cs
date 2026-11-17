using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000041 RID: 65
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class AppConfigLinks : IEquatable<AppConfigLinks>
  {
    // Token: 0x0600027B RID: 635 RVA: 0x00351810 File Offset: 0x00351810
    public AppConfigLinks(string FeatureRequest, string CommunityReddit, string CommunityWhatsApp, string Changelog)
    {
      this.FeatureRequest = FeatureRequest;
      this.CommunityReddit = CommunityReddit;
      this.CommunityWhatsApp = CommunityWhatsApp;
      this.Changelog = Changelog;
      base..ctor();
    }

    // Token: 0x17000083 RID: 131
    // (get) Token: 0x0600027C RID: 636 RVA: 0x00351838 File Offset: 0x00351838
    [Nullable(1)]
    [CompilerGenerated]
    private Type EqualityContract
    {
      [NullableContext(1)]
      [CompilerGenerated]
      get
      {
        return typeof(AppConfigLinks);
      }
    }

    // Token: 0x17000084 RID: 132
    // (get) Token: 0x0600027D RID: 637 RVA: 0x00351844 File Offset: 0x00351844
    // (set) Token: 0x0600027E RID: 638 RVA: 0x0035184C File Offset: 0x0035184C
    [JsonPropertyName("featureRequest")]
    public string FeatureRequest { get; set; }

    // Token: 0x17000085 RID: 133
    // (get) Token: 0x0600027F RID: 639 RVA: 0x00351858 File Offset: 0x00351858
    // (set) Token: 0x06000280 RID: 640 RVA: 0x00351860 File Offset: 0x00351860
    [JsonPropertyName("communityReddit")]
    public string CommunityReddit { get; set; }

    // Token: 0x17000086 RID: 134
    // (get) Token: 0x06000281 RID: 641 RVA: 0x0035186C File Offset: 0x0035186C
    // (set) Token: 0x06000282 RID: 642 RVA: 0x00351874 File Offset: 0x00351874
    [JsonPropertyName("communityWhatsApp")]
    public string CommunityWhatsApp { get; set; }

    // Token: 0x17000087 RID: 135
    // (get) Token: 0x06000283 RID: 643 RVA: 0x00351880 File Offset: 0x00351880
    // (set) Token: 0x06000284 RID: 644 RVA: 0x00351888 File Offset: 0x00351888
    [JsonPropertyName("changelog")]
    public string Changelog { get; set; }

    // Token: 0x06000285 RID: 645 RVA: 0x00351894 File Offset: 0x00351894
    [NullableContext(1)]
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("AppConfigLinks");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000286 RID: 646 RVA: 0x003518E0 File Offset: 0x003518E0
    [NullableContext(1)]
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

    // Token: 0x06000287 RID: 647 RVA: 0x00351958 File Offset: 0x00351958
    [CompilerGenerated]
    public static bool operator !=(AppConfigLinks left, AppConfigLinks right)
    {
      return !(left == right);
    }

    // Token: 0x06000288 RID: 648 RVA: 0x00351964 File Offset: 0x00351964
    [CompilerGenerated]
    public static bool operator ==(AppConfigLinks left, AppConfigLinks right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000289 RID: 649 RVA: 0x00351978 File Offset: 0x00351978
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FeatureRequest>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CommunityReddit>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<CommunityWhatsApp>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Changelog>k__BackingField);
    }

    // Token: 0x0600028A RID: 650 RVA: 0x003519F4 File Offset: 0x003519F4
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AppConfigLinks);
    }

    // Token: 0x0600028B RID: 651 RVA: 0x00351A04 File Offset: 0x00351A04
    [CompilerGenerated]
    public bool Equals(AppConfigLinks other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<FeatureRequest>k__BackingField, other.<FeatureRequest>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CommunityReddit>k__BackingField, other.<CommunityReddit>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<CommunityWhatsApp>k__BackingField, other.<CommunityWhatsApp>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Changelog>k__BackingField, other.<Changelog>k__BackingField));
    }

    // Token: 0x0600028D RID: 653 RVA: 0x00351A98 File Offset: 0x00351A98
    [CompilerGenerated]
    private AppConfigLinks([Nullable(1)] AppConfigLinks original)
    {
      this.FeatureRequest = original.<FeatureRequest>k__BackingField;
      this.CommunityReddit = original.<CommunityReddit>k__BackingField;
      this.CommunityWhatsApp = original.<CommunityWhatsApp>k__BackingField;
      this.Changelog = original.<Changelog>k__BackingField;
    }

    // Token: 0x0600028E RID: 654 RVA: 0x00351AD0 File Offset: 0x00351AD0
    [CompilerGenerated]
    public void Deconstruct(out string FeatureRequest, out string CommunityReddit, out string CommunityWhatsApp, out string Changelog)
    {
      FeatureRequest = this.FeatureRequest;
      CommunityReddit = this.CommunityReddit;
      CommunityWhatsApp = this.CommunityWhatsApp;
      Changelog = this.Changelog;
    }
  }
}
