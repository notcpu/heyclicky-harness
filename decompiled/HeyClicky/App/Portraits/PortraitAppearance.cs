using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using Windows.UI;

namespace HeyClicky.App.Portraits
{
  // Token: 0x02000107 RID: 263
  [NullableContext(1)]
  [Nullable(0)]
  [JsonConverter(typeof(PortraitAppearanceJsonConverter))]
  public sealed class PortraitAppearance : IEquatable<PortraitAppearance>
  {
    // Token: 0x06000D95 RID: 3477 RVA: 0x0056BFD0 File Offset: 0x0056BFD0
    public PortraitAppearance(Hairstyle Hairstyle, Colorway HairColor, Outfit OutfitStyle, Colorway OutfitColor, Colorway AccentColor, SkinTone SkinTone, Colorway EyeColor, Colorway BackgroundColor)
    {
      this.Hairstyle = Hairstyle;
      this.HairColor = HairColor;
      this.OutfitStyle = OutfitStyle;
      this.OutfitColor = OutfitColor;
      this.AccentColor = AccentColor;
      this.SkinTone = SkinTone;
      this.EyeColor = EyeColor;
      this.BackgroundColor = BackgroundColor;
      base..ctor();
    }

    // Token: 0x17000263 RID: 611
    // (get) Token: 0x06000D96 RID: 3478 RVA: 0x0056C020 File Offset: 0x0056C020
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PortraitAppearance);
      }
    }

    // Token: 0x17000264 RID: 612
    // (get) Token: 0x06000D97 RID: 3479 RVA: 0x0056C02C File Offset: 0x0056C02C
    // (set) Token: 0x06000D98 RID: 3480 RVA: 0x0056C034 File Offset: 0x0056C034
    public Hairstyle Hairstyle { get; set; }

    // Token: 0x17000265 RID: 613
    // (get) Token: 0x06000D99 RID: 3481 RVA: 0x0056C040 File Offset: 0x0056C040
    // (set) Token: 0x06000D9A RID: 3482 RVA: 0x0056C048 File Offset: 0x0056C048
    public Colorway HairColor { get; set; }

    // Token: 0x17000266 RID: 614
    // (get) Token: 0x06000D9B RID: 3483 RVA: 0x0056C054 File Offset: 0x0056C054
    // (set) Token: 0x06000D9C RID: 3484 RVA: 0x0056C05C File Offset: 0x0056C05C
    public Outfit OutfitStyle { get; set; }

    // Token: 0x17000267 RID: 615
    // (get) Token: 0x06000D9D RID: 3485 RVA: 0x0056C068 File Offset: 0x0056C068
    // (set) Token: 0x06000D9E RID: 3486 RVA: 0x0056C070 File Offset: 0x0056C070
    public Colorway OutfitColor { get; set; }

    // Token: 0x17000268 RID: 616
    // (get) Token: 0x06000D9F RID: 3487 RVA: 0x0056C07C File Offset: 0x0056C07C
    // (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0056C084 File Offset: 0x0056C084
    public Colorway AccentColor { get; set; }

    // Token: 0x17000269 RID: 617
    // (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0056C090 File Offset: 0x0056C090
    // (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0056C098 File Offset: 0x0056C098
    public SkinTone SkinTone { get; set; }

    // Token: 0x1700026A RID: 618
    // (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0056C0A4 File Offset: 0x0056C0A4
    // (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0056C0AC File Offset: 0x0056C0AC
    public Colorway EyeColor { get; set; }

    // Token: 0x1700026B RID: 619
    // (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0056C0B8 File Offset: 0x0056C0B8
    // (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0056C0C0 File Offset: 0x0056C0C0
    public Colorway BackgroundColor { get; set; }

    // Token: 0x06000DA7 RID: 3495 RVA: 0x0056C0CC File Offset: 0x0056C0CC
    public static PortraitAppearance Make(Hairstyle hairstyle = Hairstyle.Bob, Colorway colorway = Colorway.Blue)
    {
      PortraitPack portraitPack = hairstyle.Pack();
      return new PortraitAppearance(hairstyle, colorway, portraitPack.Outfits()[0], colorway, colorway, colorway.DefaultSkinTone(), colorway, (portraitPack == PortraitPack.ClassicClouds) ? colorway.ClassicCloudBackground() : colorway);
    }

    // Token: 0x1700026C RID: 620
    // (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0056C10C File Offset: 0x0056C10C
    public PortraitPack Pack
    {
      get
      {
        return this.Hairstyle.Pack();
      }
    }

    // Token: 0x1700026D RID: 621
    // (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0056C11C File Offset: 0x0056C11C
    public Color Hair
    {
      get
      {
        Color color;
        switch (this.Pack)
        {
        case PortraitPack.CloudClub:
          color = this.HairColor.CloudTint();
          break;
        case PortraitPack.ClassicClouds:
          color = this.HairColor.ClassicCloudTint();
          break;
        case PortraitPack.StorybookSpirits:
          color = this.HairColor.StorybookHair();
          break;
        default:
          color = this.HairColor.Hair();
          break;
        }
        return color;
      }
    }

    // Token: 0x1700026E RID: 622
    // (get) Token: 0x06000DAA RID: 3498 RVA: 0x0056C180 File Offset: 0x0056C180
    public Color Outfit
    {
      get
      {
        if (this.Pack != PortraitPack.StorybookSpirits)
        {
          return this.OutfitColor.OutfitColor();
        }
        return this.OutfitColor.StorybookFabric();
      }
    }

    // Token: 0x1700026F RID: 623
    // (get) Token: 0x06000DAB RID: 3499 RVA: 0x0056C1A4 File Offset: 0x0056C1A4
    public Color Accent
    {
      get
      {
        return this.AccentColor.Accent();
      }
    }

    // Token: 0x17000270 RID: 624
    // (get) Token: 0x06000DAC RID: 3500 RVA: 0x0056C1B4 File Offset: 0x0056C1B4
    public Color Skin
    {
      get
      {
        return this.SkinTone.SkinColor();
      }
    }

    // Token: 0x17000271 RID: 625
    // (get) Token: 0x06000DAD RID: 3501 RVA: 0x0056C1C4 File Offset: 0x0056C1C4
    public Color Eyes
    {
      get
      {
        Color color;
        switch (this.Pack)
        {
        case PortraitPack.CloudClub:
          color = this.EyeColor.CloudTint();
          break;
        case PortraitPack.ClassicClouds:
          color = PortraitData.ClassicCloudEyeColor;
          break;
        case PortraitPack.StorybookSpirits:
          color = this.EyeColor.StorybookHair();
          break;
        default:
          color = this.EyeColor.OutfitColor();
          break;
        }
        return color;
      }
    }

    // Token: 0x17000272 RID: 626
    // (get) Token: 0x06000DAE RID: 3502 RVA: 0x0056C220 File Offset: 0x0056C220
    public Color Background
    {
      get
      {
        return this.BackgroundColor.AvatarBackground();
      }
    }

    // Token: 0x06000DAF RID: 3503 RVA: 0x0056C230 File Offset: 0x0056C230
    public static PortraitAppearance Seeded(string seed)
    {
      return PortraitCharacter.For(seed).Appearance;
    }

    // Token: 0x06000DB0 RID: 3504 RVA: 0x0056C240 File Offset: 0x0056C240
    public static PortraitAppearance Varied(IReadOnlyList<PortraitAppearance> existing, PortraitPack pack, [Nullable(2)] Random rng = null)
    {
      PortraitAppearance.<>c__DisplayClass51_0 CS$<>8__locals1 = new PortraitAppearance.<>c__DisplayClass51_0();
      CS$<>8__locals1.existing = existing;
      CS$<>8__locals1.rng = rng;
      if (CS$<>8__locals1.rng == null)
      {
        CS$<>8__locals1.rng = Random.Shared;
      }
      if (pack == PortraitPack.ClassicClouds)
      {
        return PortraitAppearance.VariedClassicCloud(CS$<>8__locals1.existing, CS$<>8__locals1.rng);
      }
      List<Hairstyle> list = CS$<>8__locals1.<Varied>g__LeastUsed|0<Hairstyle>((PortraitAppearance a) => a.Hairstyle, pack.Hairstyles());
      List<Colorway> list2 = CS$<>8__locals1.<Varied>g__LeastUsed|0<Colorway>((PortraitAppearance a) => a.HairColor, Enum.GetValues<Colorway>());
      List<Outfit> list3 = CS$<>8__locals1.<Varied>g__LeastUsed|0<Outfit>((PortraitAppearance a) => a.OutfitStyle, pack.Outfits());
      List<Colorway> list4 = CS$<>8__locals1.<Varied>g__LeastUsed|0<Colorway>((PortraitAppearance a) => a.OutfitColor, Enum.GetValues<Colorway>());
      List<SkinTone> list5 = CS$<>8__locals1.<Varied>g__LeastUsed|0<SkinTone>((PortraitAppearance a) => a.SkinTone, Enum.GetValues<SkinTone>());
      List<Colorway> list6 = CS$<>8__locals1.<Varied>g__LeastUsed|0<Colorway>((PortraitAppearance a) => a.EyeColor, Enum.GetValues<Colorway>());
      PortraitAppearance portraitAppearance = PortraitAppearance.Make(Hairstyle.Bob, Colorway.Blue);
      int num = -1;
      for (int i = 0; i < 32; i++)
      {
        PortraitAppearance.<>c__DisplayClass51_3 CS$<>8__locals2 = new PortraitAppearance.<>c__DisplayClass51_3();
        CS$<>8__locals2.candidate = PortraitAppearance.Make(CS$<>8__locals1.<Varied>g__Pick|7<Hairstyle>(list), CS$<>8__locals1.<Varied>g__Pick|7<Colorway>(list2));
        if (pack.IsCloud())
        {
          PortraitAppearance.<>c__DisplayClass51_3 CS$<>8__locals3 = CS$<>8__locals2;
          PortraitAppearance portraitAppearance2 = CS$<>8__locals2.candidate.<Clone>$();
          portraitAppearance2.EyeColor = CS$<>8__locals1.<Varied>g__Pick|7<Colorway>(list6);
          CS$<>8__locals3.candidate = portraitAppearance2;
        }
        else
        {
          PortraitAppearance.<>c__DisplayClass51_3 CS$<>8__locals4 = CS$<>8__locals2;
          PortraitAppearance portraitAppearance3 = CS$<>8__locals2.candidate.<Clone>$();
          portraitAppearance3.OutfitStyle = CS$<>8__locals1.<Varied>g__Pick|7<Outfit>(list3);
          portraitAppearance3.OutfitColor = CS$<>8__locals1.<Varied>g__Pick|7<Colorway>(list4);
          portraitAppearance3.SkinTone = CS$<>8__locals1.<Varied>g__Pick|7<SkinTone>(list5);
          portraitAppearance3.EyeColor = CS$<>8__locals1.<Varied>g__Pick|7<Colorway>(list6);
          CS$<>8__locals4.candidate = portraitAppearance3;
          PortraitAppearance.<>c__DisplayClass51_3 CS$<>8__locals5 = CS$<>8__locals2;
          PortraitAppearance portraitAppearance4 = CS$<>8__locals2.candidate.<Clone>$();
          portraitAppearance4.AccentColor = CS$<>8__locals2.candidate.OutfitColor;
          CS$<>8__locals5.candidate = portraitAppearance4;
        }
        int num2 = ((CS$<>8__locals1.existing.Count == 0) ? 14 : CS$<>8__locals1.existing.Min<PortraitAppearance>((PortraitAppearance e) => CS$<>8__locals2.candidate.VisualDifference(e)));
        if (num2 > num)
        {
          portraitAppearance = CS$<>8__locals2.candidate;
          num = num2;
        }
      }
      return portraitAppearance;
    }

    // Token: 0x06000DB1 RID: 3505 RVA: 0x0056C4B0 File Offset: 0x0056C4B0
    private static PortraitAppearance VariedClassicCloud(IReadOnlyList<PortraitAppearance> existing, Random rng)
    {
      PortraitAppearance.<>c__DisplayClass52_0 CS$<>8__locals1 = new PortraitAppearance.<>c__DisplayClass52_0();
      CS$<>8__locals1.existing = existing;
      CS$<>8__locals1.colors = Enum.GetValues<Colorway>();
      CS$<>8__locals1.styles = PortraitPack.ClassicClouds.Hairstyles();
      List<List<PortraitAppearance>> list = (from offset in Enumerable.Range(0, CS$<>8__locals1.colors.Length)
        select CS$<>8__locals1.styles.Select<Hairstyle, PortraitAppearance>((Hairstyle style, int index) => PortraitAppearance.Make(style, CS$<>8__locals1.colors[(index + offset) % CS$<>8__locals1.colors.Length])).ToList<PortraitAppearance>()).ToList<List<PortraitAppearance>>();
      List<List<PortraitAppearance>> list2 = (from r in list.Select<List<PortraitAppearance>, List<PortraitAppearance>>(delegate(List<PortraitAppearance> r)
        {
          Func<PortraitAppearance, bool> func;
          if ((func = CS$<>8__locals1.<>9__8) == null)
          {
            func = (CS$<>8__locals1.<>9__8 = (PortraitAppearance c) => base.<VariedClassicCloud>g__Uses|1(c) == 0);
          }
          return r.Where<PortraitAppearance>(func).ToList<PortraitAppearance>();
        })
        where r.Count > 0
        select r).ToList<List<PortraitAppearance>>();
      if (list2.Count > 0)
      {
        int smallest = list2.Min<List<PortraitAppearance>>((List<PortraitAppearance> r) => r.Count);
        List<List<PortraitAppearance>> list3 = list2.Where<List<PortraitAppearance>>((List<PortraitAppearance> r) => r.Count == smallest).ToList<List<PortraitAppearance>>();
        List<PortraitAppearance> list4 = list3[rng.Next(list3.Count)];
        return list4[rng.Next(list4.Count)];
      }
      List<PortraitAppearance> list5 = list.SelectMany<List<PortraitAppearance>, PortraitAppearance>((List<PortraitAppearance> r) => r).ToList<PortraitAppearance>();
      CS$<>8__locals1.least = list5.Min<PortraitAppearance>(new Func<PortraitAppearance, int>(CS$<>8__locals1.<VariedClassicCloud>g__Uses|1));
      List<PortraitAppearance> list6 = list5.Where<PortraitAppearance>((PortraitAppearance c) => base.<VariedClassicCloud>g__Uses|1(c) == CS$<>8__locals1.least).ToList<PortraitAppearance>();
      return list6[rng.Next(list6.Count)];
    }

    // Token: 0x06000DB2 RID: 3506 RVA: 0x0056C634 File Offset: 0x0056C634
    private int VisualDifference(PortraitAppearance other)
    {
      int num = 0;
      if (this.Hairstyle != other.Hairstyle)
      {
        num += 3;
      }
      if (this.HairColor != other.HairColor)
      {
        num += 3;
      }
      if (!this.Pack.IsCloud() && this.SkinTone != other.SkinTone)
      {
        num += 3;
      }
      if (!this.Pack.IsCloud() && this.OutfitStyle != other.OutfitStyle)
      {
        num += 2;
      }
      if (!this.Pack.IsCloud() && this.OutfitColor != other.OutfitColor)
      {
        num += 2;
      }
      if (this.Pack != PortraitPack.ClassicClouds && this.EyeColor != other.EyeColor)
      {
        num++;
      }
      return num;
    }

    // Token: 0x06000DB3 RID: 3507 RVA: 0x0056C6E0 File Offset: 0x0056C6E0
    public static PortraitAppearance HomeMascot(string cursorHex)
    {
      string text = cursorHex.ToUpperInvariant();
      PortraitAppearance portraitAppearance;
      if (!(text == "#FF4C4C"))
      {
        if (!(text == "#26C281"))
        {
          if (!(text == "#D4A017"))
          {
            portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicSky, Colorway.Blue);
          }
          else
          {
            portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicSunbeam, Colorway.Honey);
          }
        }
        else
        {
          portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicMeadow, Colorway.Mint);
        }
      }
      else
      {
        portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicPeach, Colorway.Coral);
      }
      return portraitAppearance;
    }

    // Token: 0x06000DB4 RID: 3508 RVA: 0x0056C748 File Offset: 0x0056C748
    public static PortraitAppearance HomeCloud(string colorName)
    {
      PortraitAppearance portraitAppearance;
      if (!(colorName == "red"))
      {
        if (!(colorName == "green"))
        {
          if (!(colorName == "yellow"))
          {
            portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicSky, Colorway.Blue);
          }
          else
          {
            portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicSunbeam, Colorway.Honey);
          }
        }
        else
        {
          portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicMeadow, Colorway.Mint);
        }
      }
      else
      {
        portraitAppearance = PortraitAppearance.Make(Hairstyle.ClassicPeach, Colorway.Coral);
      }
      return portraitAppearance;
    }

    // Token: 0x17000273 RID: 627
    // (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0056C7AC File Offset: 0x0056C7AC
    public static IReadOnlyList<PortraitAppearance> HomeClouds { get; } = PortraitAppearance.HomeCloudColorNames.Select<string, PortraitAppearance>(new Func<string, PortraitAppearance>(PortraitAppearance.HomeCloud)).ToList<PortraitAppearance>();

    // Token: 0x06000DB6 RID: 3510 RVA: 0x0056C7B4 File Offset: 0x0056C7B4
    private bool SameCloud(PortraitAppearance other)
    {
      return this.Hairstyle == other.Hairstyle && this.HairColor == other.HairColor;
    }

    // Token: 0x17000274 RID: 628
    // (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0056C7D4 File Offset: 0x0056C7D4
    public bool IsHomeCloud
    {
      get
      {
        return PortraitAppearance.HomeClouds.Any<PortraitAppearance>(new Func<PortraitAppearance, bool>(this.SameCloud));
      }
    }

    // Token: 0x17000275 RID: 629
    // (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0056C7EC File Offset: 0x0056C7EC
    [Nullable(2)]
    public string HomeCloudColorName
    {
      [NullableContext(2)]
      get
      {
        for (int i = 0; i < PortraitAppearance.HomeClouds.Count; i++)
        {
          if (this.SameCloud(PortraitAppearance.HomeClouds[i]))
          {
            return PortraitAppearance.HomeCloudColorNames[i];
          }
        }
        return null;
      }
    }

    // Token: 0x06000DB9 RID: 3513 RVA: 0x0056C830 File Offset: 0x0056C830
    public static PortraitAppearance RandomHomeCloud(IEnumerable<PortraitAppearance> existing, [Nullable(2)] Random rng = null)
    {
      PortraitAppearance.<>c__DisplayClass65_0 CS$<>8__locals1 = new PortraitAppearance.<>c__DisplayClass65_0();
      CS$<>8__locals1.looks = existing.ToList<PortraitAppearance>();
      CS$<>8__locals1.least = PortraitAppearance.HomeClouds.Min<PortraitAppearance>(new Func<PortraitAppearance, int>(CS$<>8__locals1.<RandomHomeCloud>g__Uses|0));
      List<PortraitAppearance> list = PortraitAppearance.HomeClouds.Where<PortraitAppearance>((PortraitAppearance c) => base.<RandomHomeCloud>g__Uses|0(c) == CS$<>8__locals1.least).ToList<PortraitAppearance>();
      return list[(rng ?? Random.Shared).Next(list.Count)];
    }

    // Token: 0x06000DBA RID: 3514 RVA: 0x0056C8A4 File Offset: 0x0056C8A4
    public static PortraitAppearance SeededHomeCloud(string seed)
    {
      return PortraitAppearance.HomeClouds[(int)(PortraitAppearance.SeedHash(seed) % (ulong)((long)PortraitAppearance.HomeClouds.Count))];
    }

    // Token: 0x06000DBB RID: 3515 RVA: 0x0056C8C4 File Offset: 0x0056C8C4
    public static ulong SeedHash(string seed)
    {
      ulong num = 5381UL;
      foreach (Rune rune in seed.EnumerateRunes())
      {
        num = num * 33UL + (ulong)((long)rune.Value);
      }
      return num;
    }

    // Token: 0x06000DBC RID: 3516 RVA: 0x0056C92C File Offset: 0x0056C92C
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PortraitAppearance");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000DBD RID: 3517 RVA: 0x0056C978 File Offset: 0x0056C978
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Hairstyle = ");
      builder.Append(this.Hairstyle.ToString());
      builder.Append(", HairColor = ");
      builder.Append(this.HairColor.ToString());
      builder.Append(", OutfitStyle = ");
      builder.Append(this.OutfitStyle.ToString());
      builder.Append(", OutfitColor = ");
      builder.Append(this.OutfitColor.ToString());
      builder.Append(", AccentColor = ");
      builder.Append(this.AccentColor.ToString());
      builder.Append(", SkinTone = ");
      builder.Append(this.SkinTone.ToString());
      builder.Append(", EyeColor = ");
      builder.Append(this.EyeColor.ToString());
      builder.Append(", BackgroundColor = ");
      builder.Append(this.BackgroundColor.ToString());
      builder.Append(", Pack = ");
      builder.Append(this.Pack.ToString());
      builder.Append(", Hair = ");
      builder.Append(this.Hair.ToString());
      builder.Append(", Outfit = ");
      builder.Append(this.Outfit.ToString());
      builder.Append(", Accent = ");
      builder.Append(this.Accent.ToString());
      builder.Append(", Skin = ");
      builder.Append(this.Skin.ToString());
      builder.Append(", Eyes = ");
      builder.Append(this.Eyes.ToString());
      builder.Append(", Background = ");
      builder.Append(this.Background.ToString());
      builder.Append(", IsHomeCloud = ");
      builder.Append(this.IsHomeCloud.ToString());
      builder.Append(", HomeCloudColorName = ");
      builder.Append(this.HomeCloudColorName);
      return true;
    }

    // Token: 0x06000DBE RID: 3518 RVA: 0x0056CC1C File Offset: 0x0056CC1C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PortraitAppearance left, PortraitAppearance right)
    {
      return !(left == right);
    }

    // Token: 0x06000DBF RID: 3519 RVA: 0x0056CC28 File Offset: 0x0056CC28
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PortraitAppearance left, PortraitAppearance right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000DC0 RID: 3520 RVA: 0x0056CC3C File Offset: 0x0056CC3C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return (((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<Hairstyle>.Default.GetHashCode(this.<Hairstyle>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<HairColor>k__BackingField)) * -1521134295 + EqualityComparer<HeyClicky.App.Portraits.Outfit>.Default.GetHashCode(this.<OutfitStyle>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<OutfitColor>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<AccentColor>k__BackingField)) * -1521134295 + EqualityComparer<SkinTone>.Default.GetHashCode(this.<SkinTone>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<EyeColor>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<BackgroundColor>k__BackingField);
    }

    // Token: 0x06000DC1 RID: 3521 RVA: 0x0056CD14 File Offset: 0x0056CD14
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PortraitAppearance);
    }

    // Token: 0x06000DC2 RID: 3522 RVA: 0x0056CD24 File Offset: 0x0056CD24
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PortraitAppearance other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<Hairstyle>.Default.Equals(this.<Hairstyle>k__BackingField, other.<Hairstyle>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<HairColor>k__BackingField, other.<HairColor>k__BackingField) && EqualityComparer<HeyClicky.App.Portraits.Outfit>.Default.Equals(this.<OutfitStyle>k__BackingField, other.<OutfitStyle>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<OutfitColor>k__BackingField, other.<OutfitColor>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<AccentColor>k__BackingField, other.<AccentColor>k__BackingField) && EqualityComparer<SkinTone>.Default.Equals(this.<SkinTone>k__BackingField, other.<SkinTone>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<EyeColor>k__BackingField, other.<EyeColor>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<BackgroundColor>k__BackingField, other.<BackgroundColor>k__BackingField));
    }

    // Token: 0x06000DC4 RID: 3524 RVA: 0x0056CE24 File Offset: 0x0056CE24
    [CompilerGenerated]
    private PortraitAppearance(PortraitAppearance original)
    {
      this.Hairstyle = original.<Hairstyle>k__BackingField;
      this.HairColor = original.<HairColor>k__BackingField;
      this.OutfitStyle = original.<OutfitStyle>k__BackingField;
      this.OutfitColor = original.<OutfitColor>k__BackingField;
      this.AccentColor = original.<AccentColor>k__BackingField;
      this.SkinTone = original.<SkinTone>k__BackingField;
      this.EyeColor = original.<EyeColor>k__BackingField;
      this.BackgroundColor = original.<BackgroundColor>k__BackingField;
    }

    // Token: 0x06000DC5 RID: 3525 RVA: 0x0056CE98 File Offset: 0x0056CE98
    [CompilerGenerated]
    public void Deconstruct(out Hairstyle Hairstyle, out Colorway HairColor, out Outfit OutfitStyle, out Colorway OutfitColor, out Colorway AccentColor, out SkinTone SkinTone, out Colorway EyeColor, out Colorway BackgroundColor)
    {
      Hairstyle = this.Hairstyle;
      HairColor = this.HairColor;
      OutfitStyle = this.OutfitStyle;
      OutfitColor = this.OutfitColor;
      AccentColor = this.AccentColor;
      SkinTone = this.SkinTone;
      EyeColor = this.EyeColor;
      BackgroundColor = this.BackgroundColor;
    }

    // Token: 0x0400056A RID: 1386
    public static readonly IReadOnlyList<string> HomeCloudColorNames = new <>z__ReadOnlyArray<string>(new string[] { "red", "blue", "yellow", "green" });
  }
}
