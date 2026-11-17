using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B3 RID: 691
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class HomeMetrics : IEquatable<HomeMetrics>
  {
    // Token: 0x06001C6A RID: 7274 RVA: 0x005FA674 File Offset: 0x005FA674
    public HomeMetrics(double Density)
    {
      this.Density = Density;
      base..ctor();
    }

    // Token: 0x170004D8 RID: 1240
    // (get) Token: 0x06001C6B RID: 7275 RVA: 0x005FA684 File Offset: 0x005FA684
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(HomeMetrics);
      }
    }

    // Token: 0x170004D9 RID: 1241
    // (get) Token: 0x06001C6C RID: 7276 RVA: 0x005FA690 File Offset: 0x005FA690
    // (set) Token: 0x06001C6D RID: 7277 RVA: 0x005FA698 File Offset: 0x005FA698
    public double Density { get; set; }

    // Token: 0x06001C6E RID: 7278 RVA: 0x005FA6A4 File Offset: 0x005FA6A4
    private static double Lerp(double compact, double full, double t)
    {
      return compact + (full - compact) * t;
    }

    // Token: 0x06001C6F RID: 7279 RVA: 0x005FA6B0 File Offset: 0x005FA6B0
    public static HomeMetrics For(double w, double h)
    {
      return new HomeMetrics(Math.Clamp(Math.Min((w - 820.0) / 430.0, (h - 560.0) / 240.0), 0.0, 1.0));
    }

    // Token: 0x170004DA RID: 1242
    // (get) Token: 0x06001C70 RID: 7280 RVA: 0x005FA708 File Offset: 0x005FA708
    public double RailWidth
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(220.0, 300.0, this.Density));
      }
    }

    // Token: 0x170004DB RID: 1243
    // (get) Token: 0x06001C71 RID: 7281 RVA: 0x005FA72C File Offset: 0x005FA72C
    public double RowAvatarSize
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(32.0, 40.0, this.Density));
      }
    }

    // Token: 0x170004DC RID: 1244
    // (get) Token: 0x06001C72 RID: 7282 RVA: 0x005FA750 File Offset: 0x005FA750
    public double PinnedAvatarCap
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(56.0, 72.0, this.Density));
      }
    }

    // Token: 0x170004DD RID: 1245
    // (get) Token: 0x06001C73 RID: 7283 RVA: 0x005FA774 File Offset: 0x005FA774
    public double HeaderReservedHeight
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(60.0, 76.0, this.Density));
      }
    }

    // Token: 0x170004DE RID: 1246
    // (get) Token: 0x06001C74 RID: 7284 RVA: 0x005FA798 File Offset: 0x005FA798
    public double HeaderIdentityAvatarSize
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(34.0, 42.0, this.Density));
      }
    }

    // Token: 0x170004DF RID: 1247
    // (get) Token: 0x06001C75 RID: 7285 RVA: 0x005FA7BC File Offset: 0x005FA7BC
    public double SettingsSidebarWidth
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(180.0, 228.0, this.Density));
      }
    }

    // Token: 0x170004E0 RID: 1248
    // (get) Token: 0x06001C76 RID: 7286 RVA: 0x005FA7E0 File Offset: 0x005FA7E0
    public double VoicePillHeight
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(42.0, 52.0, this.Density));
      }
    }

    // Token: 0x170004E1 RID: 1249
    // (get) Token: 0x06001C77 RID: 7287 RVA: 0x005FA804 File Offset: 0x005FA804
    public double VoicePillLabelFontSize
    {
      get
      {
        return 13.0 + this.Density;
      }
    }

    // Token: 0x170004E2 RID: 1250
    // (get) Token: 0x06001C78 RID: 7288 RVA: 0x005FA818 File Offset: 0x005FA818
    public double HeroGreetingFontSize
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(18.0, 22.0, this.Density));
      }
    }

    // Token: 0x170004E3 RID: 1251
    // (get) Token: 0x06001C79 RID: 7289 RVA: 0x005FA83C File Offset: 0x005FA83C
    public double HeroSubtitleFontSize
    {
      get
      {
        return 12.0 + this.Density;
      }
    }

    // Token: 0x170004E4 RID: 1252
    // (get) Token: 0x06001C7A RID: 7290 RVA: 0x005FA850 File Offset: 0x005FA850
    public double MascotFaceScale
    {
      get
      {
        return HomeMetrics.Lerp(0.62, 1.0, this.Density);
      }
    }

    // Token: 0x170004E5 RID: 1253
    // (get) Token: 0x06001C7B RID: 7291 RVA: 0x005FA870 File Offset: 0x005FA870
    public double ContentPadding
    {
      get
      {
        return 12.0 + 8.0 * this.Density;
      }
    }

    // Token: 0x170004E6 RID: 1254
    // (get) Token: 0x06001C7C RID: 7292 RVA: 0x005FA88C File Offset: 0x005FA88C
    public double ArtifactCardWidth
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(124.0, 158.0, this.Density));
      }
    }

    // Token: 0x170004E7 RID: 1255
    // (get) Token: 0x06001C7D RID: 7293 RVA: 0x005FA8B0 File Offset: 0x005FA8B0
    public double ArtifactPreviewHeight
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(88.0, 116.0, this.Density));
      }
    }

    // Token: 0x170004E8 RID: 1256
    // (get) Token: 0x06001C7E RID: 7294 RVA: 0x005FA8D4 File Offset: 0x005FA8D4
    public double SectionSpacing
    {
      get
      {
        return 12.0 + 8.0 * this.Density;
      }
    }

    // Token: 0x170004E9 RID: 1257
    // (get) Token: 0x06001C7F RID: 7295 RVA: 0x005FA8F0 File Offset: 0x005FA8F0
    public double SuggestionCharacterSize
    {
      get
      {
        return Math.Round(HomeMetrics.Lerp(110.0, 195.0, this.Density));
      }
    }

    // Token: 0x170004EA RID: 1258
    // (get) Token: 0x06001C80 RID: 7296 RVA: 0x005FA914 File Offset: 0x005FA914
    public double SuggestionTitleFontSize
    {
      get
      {
        return 14.0 + 4.0 * this.Density;
      }
    }

    // Token: 0x170004EB RID: 1259
    // (get) Token: 0x06001C81 RID: 7297 RVA: 0x005FA930 File Offset: 0x005FA930
    public double SuggestionBodyFontSize
    {
      get
      {
        return 12.0 + 1.5 * this.Density;
      }
    }

    // Token: 0x170004EC RID: 1260
    // (get) Token: 0x06001C82 RID: 7298 RVA: 0x005FA94C File Offset: 0x005FA94C
    public double SuggestionPortraitTopInset
    {
      get
      {
        return 16.0 + 16.0 * this.Density;
      }
    }

    // Token: 0x170004ED RID: 1261
    // (get) Token: 0x06001C83 RID: 7299 RVA: 0x005FA968 File Offset: 0x005FA968
    public double SuggestionHandWidth
    {
      get
      {
        return 18.0 + 10.0 * this.Density;
      }
    }

    // Token: 0x06001C84 RID: 7300 RVA: 0x005FA984 File Offset: 0x005FA984
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("HomeMetrics");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06001C85 RID: 7301 RVA: 0x005FA9D0 File Offset: 0x005FA9D0
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Density = ");
      builder.Append(this.Density.ToString());
      builder.Append(", RailWidth = ");
      builder.Append(this.RailWidth.ToString());
      builder.Append(", RowAvatarSize = ");
      builder.Append(this.RowAvatarSize.ToString());
      builder.Append(", PinnedAvatarCap = ");
      builder.Append(this.PinnedAvatarCap.ToString());
      builder.Append(", HeaderReservedHeight = ");
      builder.Append(this.HeaderReservedHeight.ToString());
      builder.Append(", HeaderIdentityAvatarSize = ");
      builder.Append(this.HeaderIdentityAvatarSize.ToString());
      builder.Append(", SettingsSidebarWidth = ");
      builder.Append(this.SettingsSidebarWidth.ToString());
      builder.Append(", VoicePillHeight = ");
      builder.Append(this.VoicePillHeight.ToString());
      builder.Append(", VoicePillLabelFontSize = ");
      builder.Append(this.VoicePillLabelFontSize.ToString());
      builder.Append(", HeroGreetingFontSize = ");
      builder.Append(this.HeroGreetingFontSize.ToString());
      builder.Append(", HeroSubtitleFontSize = ");
      builder.Append(this.HeroSubtitleFontSize.ToString());
      builder.Append(", MascotFaceScale = ");
      builder.Append(this.MascotFaceScale.ToString());
      builder.Append(", ContentPadding = ");
      builder.Append(this.ContentPadding.ToString());
      builder.Append(", ArtifactCardWidth = ");
      builder.Append(this.ArtifactCardWidth.ToString());
      builder.Append(", ArtifactPreviewHeight = ");
      builder.Append(this.ArtifactPreviewHeight.ToString());
      builder.Append(", SectionSpacing = ");
      builder.Append(this.SectionSpacing.ToString());
      builder.Append(", SuggestionCharacterSize = ");
      builder.Append(this.SuggestionCharacterSize.ToString());
      builder.Append(", SuggestionTitleFontSize = ");
      builder.Append(this.SuggestionTitleFontSize.ToString());
      builder.Append(", SuggestionBodyFontSize = ");
      builder.Append(this.SuggestionBodyFontSize.ToString());
      builder.Append(", SuggestionPortraitTopInset = ");
      builder.Append(this.SuggestionPortraitTopInset.ToString());
      builder.Append(", SuggestionHandWidth = ");
      builder.Append(this.SuggestionHandWidth.ToString());
      return true;
    }

    // Token: 0x06001C86 RID: 7302 RVA: 0x005FAD18 File Offset: 0x005FAD18
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(HomeMetrics left, HomeMetrics right)
    {
      return !(left == right);
    }

    // Token: 0x06001C87 RID: 7303 RVA: 0x005FAD24 File Offset: 0x005FAD24
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(HomeMetrics left, HomeMetrics right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06001C88 RID: 7304 RVA: 0x005FAD38 File Offset: 0x005FAD38
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Density>k__BackingField);
    }

    // Token: 0x06001C89 RID: 7305 RVA: 0x005FAD64 File Offset: 0x005FAD64
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as HomeMetrics);
    }

    // Token: 0x06001C8A RID: 7306 RVA: 0x005FAD74 File Offset: 0x005FAD74
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(HomeMetrics other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<double>.Default.Equals(this.<Density>k__BackingField, other.<Density>k__BackingField));
    }

    // Token: 0x06001C8C RID: 7308 RVA: 0x005FADB4 File Offset: 0x005FADB4
    [CompilerGenerated]
    private HomeMetrics(HomeMetrics original)
    {
      this.Density = original.<Density>k__BackingField;
    }

    // Token: 0x06001C8D RID: 7309 RVA: 0x005FADC8 File Offset: 0x005FADC8
    [CompilerGenerated]
    public void Deconstruct(out double Density)
    {
      Density = this.Density;
    }

    // Token: 0x04000DA9 RID: 3497
    public const double SidebarLeadingInset = 14.0;

    // Token: 0x04000DAA RID: 3498
    public const double FocusRailLeadingInset = 18.0;

    // Token: 0x04000DAB RID: 3499
    public const double RailToDetailSpacing = 9.0;

    // Token: 0x04000DAC RID: 3500
    public const double DetailTrailingPadding = 16.0;
  }
}
