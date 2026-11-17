using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.App.Portraits
{
  // Token: 0x02000109 RID: 265
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class PortraitCharacter : IEquatable<PortraitCharacter>
  {
    // Token: 0x06000DCC RID: 3532 RVA: 0x0056D2C4 File Offset: 0x0056D2C4
    public PortraitCharacter(string Name, Hairstyle Hairstyle, Colorway Colorway, string Introduction, Outfit OutfitStyle = Outfit.Classic, Colorway? OutfitColor = null, Colorway? AccentColor = null, SkinTone? SkinTone = null, Colorway? EyeColor = null)
    {
      this.Name = Name;
      this.Hairstyle = Hairstyle;
      this.Colorway = Colorway;
      this.Introduction = Introduction;
      this.OutfitStyle = OutfitStyle;
      this.OutfitColor = OutfitColor;
      this.AccentColor = AccentColor;
      this.SkinTone = SkinTone;
      this.EyeColor = EyeColor;
      base..ctor();
    }

    // Token: 0x17000276 RID: 630
    // (get) Token: 0x06000DCD RID: 3533 RVA: 0x0056D31C File Offset: 0x0056D31C
    [CompilerGenerated]
    private Type EqualityContract
    {
      [CompilerGenerated]
      get
      {
        return typeof(PortraitCharacter);
      }
    }

    // Token: 0x17000277 RID: 631
    // (get) Token: 0x06000DCE RID: 3534 RVA: 0x0056D328 File Offset: 0x0056D328
    // (set) Token: 0x06000DCF RID: 3535 RVA: 0x0056D330 File Offset: 0x0056D330
    public string Name { get; set; }

    // Token: 0x17000278 RID: 632
    // (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0056D33C File Offset: 0x0056D33C
    // (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0056D344 File Offset: 0x0056D344
    public Hairstyle Hairstyle { get; set; }

    // Token: 0x17000279 RID: 633
    // (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0056D350 File Offset: 0x0056D350
    // (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0056D358 File Offset: 0x0056D358
    public Colorway Colorway { get; set; }

    // Token: 0x1700027A RID: 634
    // (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0056D364 File Offset: 0x0056D364
    // (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0056D36C File Offset: 0x0056D36C
    public string Introduction { get; set; }

    // Token: 0x1700027B RID: 635
    // (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0056D378 File Offset: 0x0056D378
    // (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0056D380 File Offset: 0x0056D380
    public Outfit OutfitStyle { get; set; }

    // Token: 0x1700027C RID: 636
    // (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0056D38C File Offset: 0x0056D38C
    // (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0056D394 File Offset: 0x0056D394
    public Colorway? OutfitColor { get; set; }

    // Token: 0x1700027D RID: 637
    // (get) Token: 0x06000DDA RID: 3546 RVA: 0x0056D3A0 File Offset: 0x0056D3A0
    // (set) Token: 0x06000DDB RID: 3547 RVA: 0x0056D3A8 File Offset: 0x0056D3A8
    public Colorway? AccentColor { get; set; }

    // Token: 0x1700027E RID: 638
    // (get) Token: 0x06000DDC RID: 3548 RVA: 0x0056D3B4 File Offset: 0x0056D3B4
    // (set) Token: 0x06000DDD RID: 3549 RVA: 0x0056D3BC File Offset: 0x0056D3BC
    public SkinTone? SkinTone { get; set; }

    // Token: 0x1700027F RID: 639
    // (get) Token: 0x06000DDE RID: 3550 RVA: 0x0056D3C8 File Offset: 0x0056D3C8
    // (set) Token: 0x06000DDF RID: 3551 RVA: 0x0056D3D0 File Offset: 0x0056D3D0
    public Colorway? EyeColor { get; set; }

    // Token: 0x17000280 RID: 640
    // (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0056D3DC File Offset: 0x0056D3DC
    public PortraitPack Pack
    {
      get
      {
        return this.Hairstyle.Pack();
      }
    }

    // Token: 0x17000281 RID: 641
    // (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0056D3EC File Offset: 0x0056D3EC
    public PortraitAppearance Appearance
    {
      get
      {
        PortraitAppearance portraitAppearance = PortraitAppearance.Make(this.Hairstyle, this.Colorway);
        Colorway colorway = this.OutfitColor ?? this.Colorway;
        PortraitAppearance portraitAppearance2 = portraitAppearance.<Clone>$();
        portraitAppearance2.OutfitStyle = this.OutfitStyle;
        portraitAppearance2.OutfitColor = colorway;
        portraitAppearance2.AccentColor = this.AccentColor.GetValueOrDefault(colorway);
        portraitAppearance2.SkinTone = this.SkinTone ?? this.Colorway.DefaultSkinTone();
        portraitAppearance2.EyeColor = this.EyeColor ?? this.Colorway;
        return portraitAppearance2;
      }
    }

    // Token: 0x06000DE2 RID: 3554 RVA: 0x0056D4A8 File Offset: 0x0056D4A8
    public static PortraitCharacter For(string seed)
    {
      ulong num = 5381UL;
      foreach (byte b in Encoding.UTF8.GetBytes(seed))
      {
        num = num * 33UL + (ulong)b;
      }
      return PortraitCharacter.Cast[(int)(num % (ulong)((long)PortraitCharacter.Cast.Count))];
    }

    // Token: 0x06000DE3 RID: 3555 RVA: 0x0056D4FC File Offset: 0x0056D4FC
    public static IReadOnlyList<PortraitCharacter> CharactersOf(PortraitPack pack)
    {
      switch (pack)
      {
      case PortraitPack.PuffPals:
        return PortraitCharacter.PuffPalsDisplay;
      case PortraitPack.ArcadeAllStars:
        return PortraitCharacter.Arcade;
      case PortraitPack.CloudClub:
        return PortraitCharacter.CloudClub;
      case PortraitPack.ClassicClouds:
        return PortraitCharacter.ClassicClouds;
      case PortraitPack.StorybookSpirits:
        return PortraitCharacter.Storybook;
      }
      return PortraitCharacter.Ki;
    }

    // Token: 0x06000DE4 RID: 3556 RVA: 0x0056D558 File Offset: 0x0056D558
    [CompilerGenerated]
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("PortraitCharacter");
      stringBuilder.Append(" { ");
      if (this.PrintMembers(stringBuilder))
      {
        stringBuilder.Append(' ');
      }
      stringBuilder.Append('}');
      return stringBuilder.ToString();
    }

    // Token: 0x06000DE5 RID: 3557 RVA: 0x0056D5A4 File Offset: 0x0056D5A4
    [CompilerGenerated]
    private bool PrintMembers(StringBuilder builder)
    {
      RuntimeHelpers.EnsureSufficientExecutionStack();
      builder.Append("Name = ");
      builder.Append(this.Name);
      builder.Append(", Hairstyle = ");
      builder.Append(this.Hairstyle.ToString());
      builder.Append(", Colorway = ");
      builder.Append(this.Colorway.ToString());
      builder.Append(", Introduction = ");
      builder.Append(this.Introduction);
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
      builder.Append(", Pack = ");
      builder.Append(this.Pack.ToString());
      builder.Append(", Appearance = ");
      builder.Append(this.Appearance);
      return true;
    }

    // Token: 0x06000DE6 RID: 3558 RVA: 0x0056D73C File Offset: 0x0056D73C
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator !=(PortraitCharacter left, PortraitCharacter right)
    {
      return !(left == right);
    }

    // Token: 0x06000DE7 RID: 3559 RVA: 0x0056D748 File Offset: 0x0056D748
    [NullableContext(2)]
    [CompilerGenerated]
    public static bool operator ==(PortraitCharacter left, PortraitCharacter right)
    {
      return left == right || (left != null && left.Equals(right));
    }

    // Token: 0x06000DE8 RID: 3560 RVA: 0x0056D75C File Offset: 0x0056D75C
    [CompilerGenerated]
    public override int GetHashCode()
    {
      return ((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<Hairstyle>.Default.GetHashCode(this.<Hairstyle>k__BackingField)) * -1521134295 + EqualityComparer<Colorway>.Default.GetHashCode(this.<Colorway>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Introduction>k__BackingField)) * -1521134295 + EqualityComparer<Outfit>.Default.GetHashCode(this.<OutfitStyle>k__BackingField)) * -1521134295 + EqualityComparer<Colorway?>.Default.GetHashCode(this.<OutfitColor>k__BackingField)) * -1521134295 + EqualityComparer<Colorway?>.Default.GetHashCode(this.<AccentColor>k__BackingField)) * -1521134295 + EqualityComparer<HeyClicky.App.Portraits.SkinTone?>.Default.GetHashCode(this.<SkinTone>k__BackingField)) * -1521134295 + EqualityComparer<Colorway?>.Default.GetHashCode(this.<EyeColor>k__BackingField);
    }

    // Token: 0x06000DE9 RID: 3561 RVA: 0x0056D848 File Offset: 0x0056D848
    [NullableContext(2)]
    [CompilerGenerated]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as PortraitCharacter);
    }

    // Token: 0x06000DEA RID: 3562 RVA: 0x0056D858 File Offset: 0x0056D858
    [NullableContext(2)]
    [CompilerGenerated]
    public bool Equals(PortraitCharacter other)
    {
      return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<Hairstyle>.Default.Equals(this.<Hairstyle>k__BackingField, other.<Hairstyle>k__BackingField) && EqualityComparer<Colorway>.Default.Equals(this.<Colorway>k__BackingField, other.<Colorway>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Introduction>k__BackingField, other.<Introduction>k__BackingField) && EqualityComparer<Outfit>.Default.Equals(this.<OutfitStyle>k__BackingField, other.<OutfitStyle>k__BackingField) && EqualityComparer<Colorway?>.Default.Equals(this.<OutfitColor>k__BackingField, other.<OutfitColor>k__BackingField) && EqualityComparer<Colorway?>.Default.Equals(this.<AccentColor>k__BackingField, other.<AccentColor>k__BackingField) && EqualityComparer<HeyClicky.App.Portraits.SkinTone?>.Default.Equals(this.<SkinTone>k__BackingField, other.<SkinTone>k__BackingField) && EqualityComparer<Colorway?>.Default.Equals(this.<EyeColor>k__BackingField, other.<EyeColor>k__BackingField));
    }

    // Token: 0x06000DEC RID: 3564 RVA: 0x0056D974 File Offset: 0x0056D974
    [CompilerGenerated]
    private PortraitCharacter(PortraitCharacter original)
    {
      this.Name = original.<Name>k__BackingField;
      this.Hairstyle = original.<Hairstyle>k__BackingField;
      this.Colorway = original.<Colorway>k__BackingField;
      this.Introduction = original.<Introduction>k__BackingField;
      this.OutfitStyle = original.<OutfitStyle>k__BackingField;
      this.OutfitColor = original.<OutfitColor>k__BackingField;
      this.AccentColor = original.<AccentColor>k__BackingField;
      this.SkinTone = original.<SkinTone>k__BackingField;
      this.EyeColor = original.<EyeColor>k__BackingField;
    }

    // Token: 0x06000DED RID: 3565 RVA: 0x0056D9F4 File Offset: 0x0056D9F4
    [CompilerGenerated]
    public void Deconstruct(out string Name, out Hairstyle Hairstyle, out Colorway Colorway, out string Introduction, out Outfit OutfitStyle, out Colorway? OutfitColor, out Colorway? AccentColor, out SkinTone? SkinTone, out Colorway? EyeColor)
    {
      Name = this.Name;
      Hairstyle = this.Hairstyle;
      Colorway = this.Colorway;
      Introduction = this.Introduction;
      OutfitStyle = this.OutfitStyle;
      OutfitColor = this.OutfitColor;
      AccentColor = this.AccentColor;
      SkinTone = this.SkinTone;
      EyeColor = this.EyeColor;
    }

    // Token: 0x04000575 RID: 1397
    public static readonly PortraitCharacter Sprout = new PortraitCharacter("Sprout", Hairstyle.Pixie, Colorway.Mint, "Probably has a plan. Definitely has a cowlick.", Outfit.Classic, null, null, null, null);

    // Token: 0x04000576 RID: 1398
    public static readonly PortraitCharacter Sunny = new PortraitCharacter("Sunny", Hairstyle.TwinBuns, Colorway.Honey, "Pocket-sized sunshine. Two buns of optimism.", Outfit.Classic, null, null, null, null);

    // Token: 0x04000577 RID: 1399
    public static readonly PortraitCharacter EmberCharacter = new PortraitCharacter("Ember", Hairstyle.Curls, Colorway.Ember, "A spark of mischief in every curl.", Outfit.Classic, null, null, null, null);

    // Token: 0x04000578 RID: 1400
    public static readonly IReadOnlyList<PortraitCharacter> Cast = new PortraitCharacter[]
    {
      new PortraitCharacter("Blue", Hairstyle.Bob, Colorway.Blue, "A little curious. Very happy you're here.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Poppy", Hairstyle.Topknot, Colorway.Coral, "Big ideas. One very small bun.", Outfit.Classic, null, null, null, null),
      PortraitCharacter.Sprout,
      PortraitCharacter.Sunny,
      new PortraitCharacter("Lumi", Hairstyle.Waves, Colorway.Violet, "Dreamy hair. Surprisingly good focus.", Outfit.Classic, null, null, null, null),
      PortraitCharacter.EmberCharacter,
      new PortraitCharacter("Peppy", Hairstyle.Ponytail, Colorway.Rose, "Always up for it. Ponytail included.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Indie", Hairstyle.Undercut, Colorway.Cobalt, "Quiet confidence. A slightly rebellious fringe.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Coco", Hairstyle.Braids, Colorway.Peach, "Keeps your loose ends neatly braided.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Mochi", Hairstyle.Puff, Colorway.Moon, "Cloud-soft hair. An excellent listener.", Outfit.Classic, null, null, null, null)
    };

    // Token: 0x04000579 RID: 1401
    private static readonly IReadOnlyList<PortraitCharacter> PuffPalsDisplay = PortraitCharacter.Cast.Select<PortraitCharacter, PortraitCharacter>(delegate(PortraitCharacter c, int i)
    {
      if (i == 1)
      {
        return PortraitCharacter.Cast[3];
      }
      if (i != 3)
      {
        return c;
      }
      return PortraitCharacter.Cast[1];
    }).ToList<PortraitCharacter>();

    // Token: 0x0400057A RID: 1402
    private static readonly IReadOnlyList<PortraitCharacter> ClassicClouds = new PortraitCharacter[]
    {
      new PortraitCharacter("Sky", Hairstyle.ClassicSky, Colorway.Blue, "A familiar little face. A sky full of ideas.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Peaches", Hairstyle.ClassicPeach, Colorway.Coral, "A warm welcome, every time.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Meadow", Hairstyle.ClassicMeadow, Colorway.Mint, "A quiet moment and a fresh start.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Sunbeam", Hairstyle.ClassicSunbeam, Colorway.Honey, "A little sunshine for your day.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Twilight", Hairstyle.ClassicTwilight, Colorway.Violet, "Good ideas come in soft colors.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Apricot", Hairstyle.ClassicApricot, Colorway.Ember, "A tiny glow of encouragement.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Petal", Hairstyle.ClassicPetal, Colorway.Rose, "Gentle company. Thoughtful help.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Bluebell", Hairstyle.ClassicBluebell, Colorway.Cobalt, "A daydream with a plan.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Sherbet", Hairstyle.ClassicSherbet, Colorway.Peach, "A little color for the everyday.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Moonbeam", Hairstyle.ClassicMoonbeam, Colorway.Moon, "Keeping you company, day or night.", Outfit.Classic, null, null, null, null)
    };

    // Token: 0x0400057B RID: 1403
    private static readonly IReadOnlyList<PortraitCharacter> CloudClub = new PortraitCharacter[]
    {
      new PortraitCharacter("Nimbus", Hairstyle.CloudCumulus, Colorway.Blue, "The original daydreamer. Always here to help.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Mallow", Hairstyle.CloudCotton, Colorway.Moon, "Soft landing. Very good listener.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Dewdrop", Hairstyle.CloudRipple, Colorway.Mint, "A fresh little thought, floating your way.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Buttercup", Hairstyle.CloudBillow, Colorway.Honey, "A pocket of sunshine on a cloudy day.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Daydream", Hairstyle.CloudBloom, Colorway.Violet, "Head in the clouds. Heart in the work.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Sorbet", Hairstyle.CloudTuft, Colorway.Peach, "A tiny swirl of optimism.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Drift", Hairstyle.CloudDrift, Colorway.Cobalt, "Keeps things moving at a kinder pace.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Rosie", Hairstyle.CloudPillow, Colorway.Rose, "Small cloud. Warm welcome.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Blush", Hairstyle.CloudNimbus, Colorway.Coral, "A little bashful. A lot of bright ideas.", Outfit.Classic, null, null, null, null),
      new PortraitCharacter("Wisp", Hairstyle.CloudWisp, Colorway.Ember, "Light on the breeze. Quick on the uptake.", Outfit.Classic, null, null, null, null)
    };

    // Token: 0x0400057C RID: 1404
    private static readonly IReadOnlyList<PortraitCharacter> Storybook = new PortraitCharacter[]
    {
      new PortraitCharacter("Fern", Hairstyle.SeedHood, Colorway.Mint, "Knows a quiet shortcut through the woods.", Outfit.ForestPoncho, new Colorway?(Colorway.Mint), new Colorway?(Colorway.Honey), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Peach)),
      new PortraitCharacter("Miri", Hairstyle.RibbonBob, Colorway.Peach, "A small delivery. A whole new adventure.", Outfit.CourierDress, new Colorway?(Colorway.Cobalt), new Colorway?(Colorway.Coral), new SkinTone?(HeyClicky.App.Portraits.SkinTone.WarmTan), new Colorway?(Colorway.Peach)),
      new PortraitCharacter("Bramble", Hairstyle.SleepyEars, Colorway.Moon, "Carries an extra scarf, just in case.", Outfit.ForestPoncho, new Colorway?(Colorway.Moon), new Colorway?(Colorway.Ember), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Porcelain), new Colorway?(Colorway.Cobalt)),
      new PortraitCharacter("Tansy", Hairstyle.MeadowHat, Colorway.Honey, "Plants little ideas and remembers to water them.", Outfit.GardenApron, new Colorway?(Colorway.Mint), new Colorway?(Colorway.Peach), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Cocoa), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Nori", Hairstyle.CatKerchief, Colorway.Cobalt, "Excellent company for a rainy afternoon.", Outfit.CourierDress, new Colorway?(Colorway.Violet), new Colorway?(Colorway.Honey), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Mint)),
      new PortraitCharacter("Solvi", Hairstyle.WindPart, Colorway.Ember, "Keeps a map of places worth getting lost in.", Outfit.TravelCoat, new Colorway?(Colorway.Blue), new Colorway?(Colorway.Honey), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Caramel), new Colorway?(Colorway.Blue)),
      new PortraitCharacter("Wick", Hairstyle.CandleTuft, Colorway.Ember, "A warm little light when the work runs late.", Outfit.LanternRobe, new Colorway?(Colorway.Honey), new Colorway?(Colorway.Ember), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Peach), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Pippa", Hairstyle.BerryBraids, Colorway.Peach, "Big pockets for very small discoveries.", Outfit.GardenApron, new Colorway?(Colorway.Coral), new Colorway?(Colorway.Mint), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Deep), new Colorway?(Colorway.Peach)),
      new PortraitCharacter("Yarrow", Hairstyle.AcornCap, Colorway.Honey, "Every long journey starts with a tiny seed.", Outfit.TravelCoat, new Colorway?(Colorway.Ember), new Colorway?(Colorway.Mint), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Umber), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Lune", Hairstyle.MoonMantle, Colorway.Violet, "Listens to the rain and your unfinished thoughts.", Outfit.LanternRobe, new Colorway?(Colorway.Violet), new Colorway?(Colorway.Blue), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Porcelain), new Colorway?(Colorway.Violet))
    };

    // Token: 0x0400057D RID: 1405
    private static readonly IReadOnlyList<PortraitCharacter> Arcade = new PortraitCharacter[]
    {
      new PortraitCharacter("Dash", Hairstyle.HeroCap, Colorway.Coral, "Ready, set, let's make something.", Outfit.Adventurer, new Colorway?(Colorway.Blue), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Peach), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Pip", Hairstyle.FoxHood, Colorway.Ember, "Quick paws. Clever ideas.", Outfit.MascotSuit, new Colorway?(Colorway.Ember), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Mint)),
      new PortraitCharacter("Rook", Hairstyle.KnightHelm, Colorway.Moon, "Your brave little first line of defense.", Outfit.KnightArmor, new Colorway?(Colorway.Moon), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Deep), new Colorway?(Colorway.Blue)),
      new PortraitCharacter("Luma", Hairstyle.Crown, Colorway.Honey, "Royal manners. Mischievous plans.", Outfit.RoyalDress, new Colorway?(Colorway.Rose), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Caramel), new Colorway?(Colorway.Violet)),
      new PortraitCharacter("Bolt", Hairstyle.Robot, Colorway.Mint, "Beep. Boop. Beautifully done.", Outfit.SpaceSuit, new Colorway?(Colorway.Mint), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Cocoa), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Moss", Hairstyle.LeafCap, Colorway.Mint, "Takes the scenic route to good ideas.", Outfit.Adventurer, new Colorway?(Colorway.Mint), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.WarmTan), new Colorway?(Colorway.Peach)),
      new PortraitCharacter("Nova", Hairstyle.Pilot, Colorway.Violet, "Every small step is a giant leap.", Outfit.SpaceSuit, new Colorway?(Colorway.Violet), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Umber), new Colorway?(Colorway.Coral)),
      new PortraitCharacter("Bram", Hairstyle.BearHood, Colorway.Peach, "Big hugs. Surprisingly nimble.", Outfit.MascotSuit, new Colorway?(Colorway.Peach), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Porcelain), new Colorway?(Colorway.Cobalt)),
      new PortraitCharacter("Jinx", Hairstyle.Jester, Colorway.Rose, "Has a trick or three up each sleeve.", Outfit.RoyalDress, new Colorway?(Colorway.Violet), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Deep), new Colorway?(Colorway.Moon)),
      new PortraitCharacter("Finn", Hairstyle.Pirate, Colorway.Cobalt, "A pocket-sized captain of possibility.", Outfit.Adventurer, new Colorway?(Colorway.Coral), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Ember))
    };

    // Token: 0x0400057E RID: 1406
    private static readonly IReadOnlyList<PortraitCharacter> Ki = new PortraitCharacter[]
    {
      new PortraitCharacter("Kiro", Hairstyle.Flame, Colorway.Cobalt, "Small stance. Unreasonably big spirit.", Outfit.Gi, new Colorway?(Colorway.Cobalt), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Cobalt)),
      new PortraitCharacter("Sora", Hairstyle.Comet, Colorway.Honey, "Always chasing the next horizon.", Outfit.BattleArmor, new Colorway?(Colorway.Honey), new Colorway?(Colorway.Blue), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Cocoa), new Colorway?(Colorway.Honey)),
      new PortraitCharacter("Ren", Hairstyle.PartedSpikes, Colorway.Violet, "Cool head. A perfectly timed comeback.", Outfit.TrainingSuit, new Colorway?(Colorway.Violet), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Peach), new Colorway?(Colorway.Violet)),
      new PortraitCharacter("Aiko", Hairstyle.BladeTail, Colorway.Peach, "A soft smile and a sharp game plan.", Outfit.Gi, new Colorway?(Colorway.Peach), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Caramel), new Colorway?(Colorway.Peach)),
      new PortraitCharacter("Taro", Hairstyle.Crest, Colorway.Coral, "First to practice. Last to give up.", Outfit.BattleArmor, new Colorway?(Colorway.Coral), new Colorway?(Colorway.Peach), new SkinTone?(HeyClicky.App.Portraits.SkinTone.Deep), new Colorway?(Colorway.Coral)),
      new PortraitCharacter("Nami", Hairstyle.Crescent, Colorway.Blue, "Makes the hard stuff look effortless.", Outfit.FlightSuit, new Colorway?(Colorway.Blue), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Porcelain), new Colorway?(Colorway.Blue)),
      new PortraitCharacter("Zeno", Hairstyle.Storm, Colorway.Moon, "Quiet until it's time to power up.", Outfit.TrainingSuit, new Colorway?(Colorway.Moon), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Umber), new Colorway?(Colorway.Moon)),
      new PortraitCharacter("Kuma", Hairstyle.MonkKnot, Colorway.Ember, "Deep breath. Then one good move.", Outfit.MonkRobe, new Colorway?(Colorway.Ember), new Colorway?(Colorway.Peach), new SkinTone?(HeyClicky.App.Portraits.SkinTone.WarmTan), new Colorway?(Colorway.Ember)),
      new PortraitCharacter("Rumi", Hairstyle.WindTails, Colorway.Rose, "Twice the energy. Twice the hair ties.", Outfit.Gi, new Colorway?(Colorway.Rose), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Sand), new Colorway?(Colorway.Rose)),
      new PortraitCharacter("Iona", Hairstyle.HaloBob, Colorway.Mint, "A tiny explorer with cosmic ambition.", Outfit.FlightSuit, new Colorway?(Colorway.Mint), null, new SkinTone?(HeyClicky.App.Portraits.SkinTone.Cocoa), new Colorway?(Colorway.Mint))
    };
  }
}
