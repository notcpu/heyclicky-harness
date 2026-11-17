using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI;

namespace HeyClicky.App.Portraits
{
  // Token: 0x02000106 RID: 262
  [NullableContext(1)]
  [Nullable(0)]
  public static class PortraitData
  {
    // Token: 0x06000D72 RID: 3442 RVA: 0x0056AD9C File Offset: 0x0056AD9C
    public static Color Rgb(uint hex)
    {
      return Color.FromArgb(byte.MaxValue, (byte)(hex >> 16), (byte)(hex >> 8), (byte)hex);
    }

    // Token: 0x06000D73 RID: 3443 RVA: 0x0056ADB4 File Offset: 0x0056ADB4
    public static Color Rgb(double r, double g, double b, double a = 1.0)
    {
      return Color.FromArgb((byte)Math.Round(Math.Clamp(a, 0.0, 1.0) * 255.0), (byte)Math.Round(Math.Clamp(r, 0.0, 1.0) * 255.0), (byte)Math.Round(Math.Clamp(g, 0.0, 1.0) * 255.0), (byte)Math.Round(Math.Clamp(b, 0.0, 1.0) * 255.0));
    }

    // Token: 0x06000D74 RID: 3444 RVA: 0x0056AE68 File Offset: 0x0056AE68
    public static bool IsCloud(this PortraitPack p)
    {
      return p - PortraitPack.CloudClub <= 1;
    }

    // Token: 0x06000D75 RID: 3445 RVA: 0x0056AE84 File Offset: 0x0056AE84
    public static string Id(this PortraitPack p)
    {
      string text;
      switch (p)
      {
      case PortraitPack.PuffPals:
        text = "puffPals";
        break;
      case PortraitPack.KiCrew:
        text = "kiCrew";
        break;
      case PortraitPack.ArcadeAllStars:
        text = "arcadeAllStars";
        break;
      case PortraitPack.CloudClub:
        text = "cloudClub";
        break;
      case PortraitPack.ClassicClouds:
        text = "classicClouds";
        break;
      default:
        text = "storybookSpirits";
        break;
      }
      return text;
    }

    // Token: 0x06000D76 RID: 3446 RVA: 0x0056AEDC File Offset: 0x0056AEDC
    [NullableContext(2)]
    public static PortraitPack? PackFromId(string id)
    {
      return Enum.GetValues<PortraitPack>().Cast<PortraitPack?>().FirstOrDefault<PortraitPack?>((PortraitPack? p) => p.Value.Id() == id);
    }

    // Token: 0x06000D77 RID: 3447 RVA: 0x0056AF14 File Offset: 0x0056AF14
    public static string Title(this PortraitPack p)
    {
      string text;
      switch (p)
      {
      case PortraitPack.PuffPals:
        text = "Puff Pals";
        break;
      case PortraitPack.KiCrew:
        text = "Ki Crew";
        break;
      case PortraitPack.ArcadeAllStars:
        text = "Arcade All-Stars";
        break;
      case PortraitPack.CloudClub:
        text = "Cloud Club";
        break;
      case PortraitPack.ClassicClouds:
        text = "Dreamy Cloud";
        break;
      default:
        text = "Storybook Spirits";
        break;
      }
      return text;
    }

    // Token: 0x06000D78 RID: 3448 RVA: 0x0056AF6C File Offset: 0x0056AF6C
    public static IReadOnlyList<Hairstyle> Hairstyles(this PortraitPack p)
    {
      return (from h in Enum.GetValues<Hairstyle>()
        where h.Pack() == p
        select h).ToList<Hairstyle>();
    }

    // Token: 0x06000D79 RID: 3449 RVA: 0x0056AFA4 File Offset: 0x0056AFA4
    public static IReadOnlyList<Outfit> Outfits(this PortraitPack p)
    {
      switch (p)
      {
      case PortraitPack.PuffPals:
        return new Outfit[]
        {
          Outfit.Classic,
          Outfit.Hoodie,
          Outfit.Dress,
          Outfit.Overalls,
          Outfit.Jacket
        };
      case PortraitPack.KiCrew:
        return new Outfit[]
        {
          Outfit.Gi,
          Outfit.BattleArmor,
          Outfit.TrainingSuit,
          Outfit.MonkRobe,
          Outfit.FlightSuit
        };
      case PortraitPack.ArcadeAllStars:
        return new Outfit[]
        {
          Outfit.Adventurer,
          Outfit.KnightArmor,
          Outfit.RoyalDress,
          Outfit.SpaceSuit,
          Outfit.MascotSuit
        };
      case PortraitPack.StorybookSpirits:
        return new Outfit[]
        {
          Outfit.ForestPoncho,
          Outfit.CourierDress,
          Outfit.GardenApron,
          Outfit.TravelCoat,
          Outfit.LanternRobe
        };
      }
      return new Outfit[1];
    }

    // Token: 0x06000D7A RID: 3450 RVA: 0x0056B02C File Offset: 0x0056B02C
    public static PortraitPack Pack(this Hairstyle h)
    {
      PortraitPack portraitPack;
      if (h <= Hairstyle.CloudWisp)
      {
        if (h <= Hairstyle.HaloBob)
        {
          if (h > Hairstyle.Puff)
          {
            portraitPack = PortraitPack.KiCrew;
          }
          else
          {
            portraitPack = PortraitPack.PuffPals;
          }
        }
        else if (h > Hairstyle.Pirate)
        {
          portraitPack = PortraitPack.CloudClub;
        }
        else
        {
          portraitPack = PortraitPack.ArcadeAllStars;
        }
      }
      else if (h > Hairstyle.ClassicMoonbeam)
      {
        portraitPack = PortraitPack.StorybookSpirits;
      }
      else
      {
        portraitPack = PortraitPack.ClassicClouds;
      }
      return portraitPack;
    }

    // Token: 0x06000D7B RID: 3451 RVA: 0x0056B070 File Offset: 0x0056B070
    public static string RawValue(this Hairstyle h)
    {
      string text;
      switch (h)
      {
      case Hairstyle.Bob:
        text = "Soft bob";
        break;
      case Hairstyle.Topknot:
        text = "Spiral bun";
        break;
      case Hairstyle.Pixie:
        text = "Wild pixie";
        break;
      case Hairstyle.TwinBuns:
        text = "Space buns";
        break;
      case Hairstyle.Waves:
        text = "Long waves";
        break;
      case Hairstyle.Curls:
        text = "Curly crop";
        break;
      case Hairstyle.Ponytail:
        text = "High pony";
        break;
      case Hairstyle.Undercut:
        text = "Undercut";
        break;
      case Hairstyle.Braids:
        text = "Twin braids";
        break;
      case Hairstyle.Puff:
        text = "Cloud puff";
        break;
      case Hairstyle.Flame:
        text = "Flame spikes";
        break;
      case Hairstyle.Comet:
        text = "Comet sweep";
        break;
      case Hairstyle.PartedSpikes:
        text = "Split spikes";
        break;
      case Hairstyle.BladeTail:
        text = "Blade ponytail";
        break;
      case Hairstyle.Crest:
        text = "Rising crest";
        break;
      case Hairstyle.Crescent:
        text = "Crescent crop";
        break;
      case Hairstyle.Storm:
        text = "Storm mane";
        break;
      case Hairstyle.MonkKnot:
        text = "Temple knot";
        break;
      case Hairstyle.WindTails:
        text = "Wind tails";
        break;
      case Hairstyle.HaloBob:
        text = "Orbit bob";
        break;
      case Hairstyle.HeroCap:
        text = "Hero cap";
        break;
      case Hairstyle.FoxHood:
        text = "Fox hood";
        break;
      case Hairstyle.KnightHelm:
        text = "Knight helm";
        break;
      case Hairstyle.Crown:
        text = "Star crown";
        break;
      case Hairstyle.Robot:
        text = "Robot shell";
        break;
      case Hairstyle.LeafCap:
        text = "Leaf cap";
        break;
      case Hairstyle.Pilot:
        text = "Pilot helmet";
        break;
      case Hairstyle.BearHood:
        text = "Bear hood";
        break;
      case Hairstyle.Jester:
        text = "Jester hat";
        break;
      case Hairstyle.Pirate:
        text = "Captain hat";
        break;
      case Hairstyle.CloudCumulus:
        text = "Cumulus";
        break;
      case Hairstyle.CloudCotton:
        text = "Cotton";
        break;
      case Hairstyle.CloudRipple:
        text = "Ripple";
        break;
      case Hairstyle.CloudBillow:
        text = "Billow";
        break;
      case Hairstyle.CloudBloom:
        text = "Bloom";
        break;
      case Hairstyle.CloudTuft:
        text = "Tuft";
        break;
      case Hairstyle.CloudDrift:
        text = "Drift";
        break;
      case Hairstyle.CloudPillow:
        text = "Pillow";
        break;
      case Hairstyle.CloudNimbus:
        text = "Nimbus";
        break;
      case Hairstyle.CloudWisp:
        text = "Wisp";
        break;
      case Hairstyle.ClassicSky:
        text = "Sky wash";
        break;
      case Hairstyle.ClassicPeach:
        text = "Peach haze";
        break;
      case Hairstyle.ClassicMeadow:
        text = "Meadow mist";
        break;
      case Hairstyle.ClassicSunbeam:
        text = "Sunbeam blend";
        break;
      case Hairstyle.ClassicTwilight:
        text = "Twilight swirl";
        break;
      case Hairstyle.ClassicApricot:
        text = "Apricot glow";
        break;
      case Hairstyle.ClassicPetal:
        text = "Petal drift";
        break;
      case Hairstyle.ClassicBluebell:
        text = "Bluebell breeze";
        break;
      case Hairstyle.ClassicSherbet:
        text = "Sherbet skies";
        break;
      case Hairstyle.ClassicMoonbeam:
        text = "Moonbeam mist";
        break;
      case Hairstyle.SeedHood:
        text = "Seed hood";
        break;
      case Hairstyle.RibbonBob:
        text = "Ribbon bob";
        break;
      case Hairstyle.SleepyEars:
        text = "Sleepy ears";
        break;
      case Hairstyle.MeadowHat:
        text = "Meadow hat";
        break;
      case Hairstyle.CatKerchief:
        text = "Cat kerchief";
        break;
      case Hairstyle.WindPart:
        text = "Wind part";
        break;
      case Hairstyle.CandleTuft:
        text = "Candle tuft";
        break;
      case Hairstyle.BerryBraids:
        text = "Berry braids";
        break;
      case Hairstyle.AcornCap:
        text = "Acorn cap";
        break;
      default:
        text = "Moon mantle";
        break;
      }
      return text;
    }

    // Token: 0x06000D7C RID: 3452 RVA: 0x0056B3D4 File Offset: 0x0056B3D4
    public static string RawValue(this Colorway c)
    {
      return c.ToString();
    }

    // Token: 0x06000D7D RID: 3453 RVA: 0x0056B3E4 File Offset: 0x0056B3E4
    public static string RawValue(this Outfit o)
    {
      char c = char.ToLowerInvariant(o.ToString()[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text = o.ToString();
      return readOnlySpan + text.Substring(1, text.Length - 1);
    }

    // Token: 0x06000D7E RID: 3454 RVA: 0x0056B438 File Offset: 0x0056B438
    public static string RawValue(this SkinTone s)
    {
      char c = char.ToLowerInvariant(s.ToString()[0]);
      ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(ref c);
      string text = s.ToString();
      return readOnlySpan + text.Substring(1, text.Length - 1);
    }

    // Token: 0x06000D7F RID: 3455 RVA: 0x0056B48C File Offset: 0x0056B48C
    public static Color Hair(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 4692924U;
        break;
      case Colorway.Coral:
        num = 16152689U;
        break;
      case Colorway.Mint:
        num = 1417081U;
        break;
      case Colorway.Honey:
        num = 15052328U;
        break;
      case Colorway.Violet:
        num = 8415159U;
        break;
      case Colorway.Ember:
        num = 9977643U;
        break;
      case Colorway.Rose:
        num = 14114962U;
        break;
      case Colorway.Cobalt:
        num = 3423096U;
        break;
      case Colorway.Peach:
        num = 5847875U;
        break;
      default:
        num = 12305883U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D80 RID: 3456 RVA: 0x0056B51C File Offset: 0x0056B51C
    public static Color AvatarBackground(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 16372148U;
        break;
      case Colorway.Coral:
        num = 14274293U;
        break;
      case Colorway.Mint:
        num = 16313784U;
        break;
      case Colorway.Honey:
        num = 12379384U;
        break;
      case Colorway.Violet:
        num = 13888974U;
        break;
      case Colorway.Ember:
        num = 12380127U;
        break;
      case Colorway.Rose:
        num = 13229559U;
        break;
      case Colorway.Cobalt:
        num = 16243893U;
        break;
      case Colorway.Peach:
        num = 15912926U;
        break;
      default:
        num = 14537461U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D81 RID: 3457 RVA: 0x0056B5AC File Offset: 0x0056B5AC
    public static Color OutfitColor(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 7392501U;
        break;
      case Colorway.Coral:
        num = 16750992U;
        break;
      case Colorway.Mint:
        num = 6869673U;
        break;
      case Colorway.Honey:
        num = 16175720U;
        break;
      case Colorway.Violet:
        num = 11772129U;
        break;
      case Colorway.Ember:
        num = 15573092U;
        break;
      case Colorway.Rose:
        num = 15641551U;
        break;
      case Colorway.Cobalt:
        num = 9215723U;
        break;
      case Colorway.Peach:
        num = 15970451U;
        break;
      default:
        num = 12704971U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D82 RID: 3458 RVA: 0x0056B63C File Offset: 0x0056B63C
    public static Color Accent(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
      case Colorway.Cobalt:
        num = 16370807U;
        goto IL_0056;
      case Colorway.Coral:
      case Colorway.Rose:
        num = 12576711U;
        goto IL_0056;
      case Colorway.Mint:
      case Colorway.Moon:
        num = 15842523U;
        goto IL_0056;
      case Colorway.Honey:
      case Colorway.Ember:
        num = 10148817U;
        goto IL_0056;
      }
      num = 16373131U;
      IL_0056:
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D83 RID: 3459 RVA: 0x0056B6A8 File Offset: 0x0056B6A8
    public static SkinTone DefaultSkinTone(this Colorway c)
    {
      SkinTone skinTone;
      switch (c)
      {
      case Colorway.Blue:
      case Colorway.Coral:
        skinTone = SkinTone.Peach;
        break;
      case Colorway.Mint:
      case Colorway.Violet:
        skinTone = SkinTone.WarmTan;
        break;
      case Colorway.Honey:
      case Colorway.Rose:
        skinTone = SkinTone.Sand;
        break;
      case Colorway.Ember:
      case Colorway.Peach:
        skinTone = SkinTone.Caramel;
        break;
      case Colorway.Cobalt:
        skinTone = SkinTone.Cocoa;
        break;
      default:
        skinTone = SkinTone.Deep;
        break;
      }
      return skinTone;
    }

    // Token: 0x06000D84 RID: 3460 RVA: 0x0056B6F8 File Offset: 0x0056B6F8
    public static Color SkinColor(this SkinTone s)
    {
      uint num;
      switch (s)
      {
      case SkinTone.Porcelain:
        num = 16244689U;
        break;
      case SkinTone.Peach:
        num = 16763309U;
        break;
      case SkinTone.Sand:
        num = 16107419U;
        break;
      case SkinTone.WarmTan:
        num = 15380879U;
        break;
      case SkinTone.Caramel:
        num = 12154449U;
        break;
      case SkinTone.Cocoa:
        num = 9065282U;
        break;
      case SkinTone.Umber:
        num = 7685935U;
        break;
      default:
        num = 6897975U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D85 RID: 3461 RVA: 0x0056B770 File Offset: 0x0056B770
    public static Color CloudTint(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 7585001U;
        break;
      case Colorway.Coral:
        num = 15637144U;
        break;
      case Colorway.Mint:
        num = 7456172U;
        break;
      case Colorway.Honey:
        num = 15188068U;
        break;
      case Colorway.Violet:
        num = 11507931U;
        break;
      case Colorway.Ember:
        num = 14592638U;
        break;
      case Colorway.Rose:
        num = 14391741U;
        break;
      case Colorway.Cobalt:
        num = 9018845U;
        break;
      case Colorway.Peach:
        num = 15708826U;
        break;
      default:
        num = 11913694U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D86 RID: 3462 RVA: 0x0056B800 File Offset: 0x0056B800
    public static Color CloudFeatureInk(this Colorway c)
    {
      Color color = c.CloudTint();
      return PortraitData.Rgb(0.12 + (double)color.R / 255.0 * 0.3, 0.12 + (double)color.G / 255.0 * 0.3, 0.12 + (double)color.B / 255.0 * 0.3, 1.0);
    }

    // Token: 0x06000D87 RID: 3463 RVA: 0x0056B894 File Offset: 0x0056B894
    public static Color CloudFill(this Colorway c, double lightness)
    {
      Color color = c.CloudTint();
      double num = (double)color.R / 255.0;
      double num2 = (double)color.G / 255.0;
      double num3 = (double)color.B / 255.0;
      return PortraitData.Rgb(num + (1.0 - num) * lightness, num2 + (1.0 - num2) * lightness, num3 + (1.0 - num3) * lightness, 1.0);
    }

    // Token: 0x06000D88 RID: 3464 RVA: 0x0056B91C File Offset: 0x0056B91C
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "Base", "Shade", "Highlight" })]
    public static ValueTuple<uint, uint, uint> ClassicCloudPalette(this Colorway c)
    {
      ValueTuple<uint, uint, uint> valueTuple;
      switch (c)
      {
      case Colorway.Blue:
        valueTuple = new ValueTuple<uint, uint, uint>(7980525U, 4427738U, 11856110U);
        break;
      case Colorway.Coral:
        valueTuple = new ValueTuple<uint, uint, uint>(15702673U, 15232633U, 16303012U);
        break;
      case Colorway.Mint:
        valueTuple = new ValueTuple<uint, uint, uint>(8833440U, 4630668U, 12574367U);
        break;
      case Colorway.Honey:
        valueTuple = new ValueTuple<uint, uint, uint>(15387504U, 14459971U, 16312233U);
        break;
      case Colorway.Violet:
        valueTuple = new ValueTuple<uint, uint, uint>(11638746U, 8873411U, 14401769U);
        break;
      case Colorway.Ember:
        valueTuple = new ValueTuple<uint, uint, uint>(15245678U, 14120779U, 16109203U);
        break;
      case Colorway.Rose:
        valueTuple = new ValueTuple<uint, uint, uint>(15113918U, 13593759U, 16041432U);
        break;
      case Colorway.Cobalt:
        valueTuple = new ValueTuple<uint, uint, uint>(8623053U, 5598386U, 12109036U);
        break;
      case Colorway.Peach:
        valueTuple = new ValueTuple<uint, uint, uint>(15778992U, 15044522U, 16245162U);
        break;
      default:
        valueTuple = new ValueTuple<uint, uint, uint>(12569302U, 9807548U, 14867949U);
        break;
      }
      return valueTuple;
    }

    // Token: 0x06000D89 RID: 3465 RVA: 0x0056BA4C File Offset: 0x0056BA4C
    public static double ClassicCloudHue(this Colorway c)
    {
      double num;
      switch (c)
      {
      case Colorway.Blue:
        num = 0.57;
        break;
      case Colorway.Coral:
        num = 0.02;
        break;
      case Colorway.Mint:
        num = 0.4;
        break;
      case Colorway.Honey:
        num = 0.13;
        break;
      case Colorway.Violet:
        num = 0.75;
        break;
      case Colorway.Ember:
        num = 0.07;
        break;
      case Colorway.Rose:
        num = 0.9;
        break;
      case Colorway.Cobalt:
        num = 0.65;
        break;
      case Colorway.Peach:
        num = 0.04;
        break;
      default:
        num = 0.52;
        break;
      }
      return num;
    }

    // Token: 0x06000D8A RID: 3466 RVA: 0x0056BAFC File Offset: 0x0056BAFC
    public static Colorway ClassicCloudBackground(this Colorway c)
    {
      Colorway colorway;
      switch (c)
      {
      case Colorway.Blue:
      case Colorway.Violet:
      case Colorway.Cobalt:
        colorway = Colorway.Mint;
        break;
      case Colorway.Coral:
      case Colorway.Ember:
      case Colorway.Rose:
      case Colorway.Peach:
        colorway = Colorway.Honey;
        break;
      case Colorway.Mint:
        colorway = Colorway.Peach;
        break;
      case Colorway.Honey:
        colorway = Colorway.Rose;
        break;
      default:
        colorway = Colorway.Blue;
        break;
      }
      return colorway;
    }

    // Token: 0x06000D8B RID: 3467 RVA: 0x0056BB48 File Offset: 0x0056BB48
    public static Color ClassicCloudTint(this Colorway c)
    {
      return PortraitData.Rgb(c.ClassicCloudPalette().Item1);
    }

    // Token: 0x06000D8C RID: 3468 RVA: 0x0056BB5C File Offset: 0x0056BB5C
    public static Color StorybookHair(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 4352627U;
        break;
      case Colorway.Coral:
        num = 11166290U;
        break;
      case Colorway.Mint:
        num = 5466189U;
        break;
      case Colorway.Honey:
        num = 10253886U;
        break;
      case Colorway.Violet:
        num = 6708856U;
        break;
      case Colorway.Ember:
        num = 8735538U;
        break;
      case Colorway.Rose:
        num = 10380149U;
        break;
      case Colorway.Cobalt:
        num = 3423562U;
        break;
      case Colorway.Peach:
        num = 4799029U;
        break;
      default:
        num = 9607564U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D8D RID: 3469 RVA: 0x0056BBEC File Offset: 0x0056BBEC
    public static Color StorybookFabric(this Colorway c)
    {
      uint num;
      switch (c)
      {
      case Colorway.Blue:
        num = 8497075U;
        break;
      case Colorway.Coral:
        num = 13930109U;
        break;
      case Colorway.Mint:
        num = 9546371U;
        break;
      case Colorway.Honey:
        num = 14597750U;
        break;
      case Colorway.Violet:
        num = 11182269U;
        break;
      case Colorway.Ember:
        num = 12421222U;
        break;
      case Colorway.Rose:
        num = 13146535U;
        break;
      case Colorway.Cobalt:
        num = 7899042U;
        break;
      case Colorway.Peach:
        num = 14401696U;
        break;
      default:
        num = 13094588U;
        break;
      }
      return PortraitData.Rgb(num);
    }

    // Token: 0x06000D8E RID: 3470 RVA: 0x0056BC7C File Offset: 0x0056BC7C
    public static double EyeOpenness(this PortraitExpression e, int index)
    {
      double num;
      switch (e)
      {
      case PortraitExpression.Curious:
        num = ((index == 0) ? 1.0 : 0.96);
        break;
      case PortraitExpression.Listening:
        num = 1.06;
        break;
      case PortraitExpression.Thinking:
        num = 0.86;
        break;
      case PortraitExpression.Happy:
        num = 0.92;
        break;
      case PortraitExpression.Sleepy:
      case PortraitExpression.Laughing:
        num = 0.06;
        break;
      case PortraitExpression.Surprised:
      case PortraitExpression.Excited:
        num = 1.09;
        break;
      case PortraitExpression.Skeptical:
        num = ((index == 0) ? 0.66 : 1.02);
        break;
      case PortraitExpression.Shy:
        num = 0.86;
        break;
      case PortraitExpression.Determined:
        num = 0.82;
        break;
      case PortraitExpression.Loving:
        num = 1.0;
        break;
      default:
        num = 0.85;
        break;
      }
      return num;
    }

    // Token: 0x06000D8F RID: 3471 RVA: 0x0056BD68 File Offset: 0x0056BD68
    public static bool HasClosedEyes(this PortraitExpression e)
    {
      return e == PortraitExpression.Sleepy || e == PortraitExpression.Laughing;
    }

    // Token: 0x06000D90 RID: 3472 RVA: 0x0056BD88 File Offset: 0x0056BD88
    public static double HeadTilt(this PortraitExpression e)
    {
      switch (e)
      {
      case PortraitExpression.Curious:
        return -0.035;
      case PortraitExpression.Listening:
        return 0.035;
      case PortraitExpression.Thinking:
        return -0.06;
      case PortraitExpression.Sleepy:
        return 0.07;
      case PortraitExpression.Skeptical:
        return -0.085;
      case PortraitExpression.Shy:
      case PortraitExpression.Sad:
        return 0.075;
      case PortraitExpression.Laughing:
        return -0.03;
      }
      return 0.0;
    }

    // Token: 0x06000D91 RID: 3473 RVA: 0x0056BE30 File Offset: 0x0056BE30
    [NullableContext(0)]
    [return: TupleElementNames(new string[] { "X", "Y" })]
    public static ValueTuple<double, double> RestingGaze(this PortraitExpression e)
    {
      if (e <= PortraitExpression.Skeptical)
      {
        if (e == PortraitExpression.Thinking)
        {
          return new ValueTuple<double, double>(-0.35, -0.25);
        }
        if (e == PortraitExpression.Skeptical)
        {
          return new ValueTuple<double, double>(-0.3, 0.0);
        }
      }
      else
      {
        if (e == PortraitExpression.Shy)
        {
          return new ValueTuple<double, double>(0.32, 0.3);
        }
        if (e == PortraitExpression.Sad)
        {
          return new ValueTuple<double, double>(0.0, 0.3);
        }
      }
      return new ValueTuple<double, double>(0.0, 0.0);
    }

    // Token: 0x06000D92 RID: 3474 RVA: 0x0056BED8 File Offset: 0x0056BED8
    public static PortraitExpression Expression(this PortraitMood m)
    {
      PortraitExpression portraitExpression;
      switch (m)
      {
      case PortraitMood.Running:
        portraitExpression = PortraitExpression.Thinking;
        break;
      case PortraitMood.Happy:
        portraitExpression = PortraitExpression.Happy;
        break;
      case PortraitMood.Sad:
        portraitExpression = PortraitExpression.Sad;
        break;
      default:
        portraitExpression = PortraitExpression.Sleepy;
        break;
      }
      return portraitExpression;
    }

    // Token: 0x06000D93 RID: 3475 RVA: 0x0056BF0C File Offset: 0x0056BF0C
    public static PortraitMood MoodFor(AgentTaskStatus status)
    {
      switch (status)
      {
      case AgentTaskStatus.Starting:
      case AgentTaskStatus.Running:
        return PortraitMood.Running;
      case AgentTaskStatus.Finished:
        return PortraitMood.Happy;
      case AgentTaskStatus.Failed:
        return PortraitMood.Sad;
      }
      return PortraitMood.Sleepy;
    }

    // Token: 0x0400055E RID: 1374
    public static readonly PortraitPack[] VisiblePacks = new PortraitPack[]
    {
      PortraitPack.ClassicClouds,
      PortraitPack.PuffPals,
      PortraitPack.StorybookSpirits
    };

    // Token: 0x0400055F RID: 1375
    public static readonly Color StorybookFaceInk = PortraitData.Rgb(4338479U);

    // Token: 0x04000560 RID: 1376
    public static readonly Color ClassicCloudEyeColor = PortraitData.Rgb(1.0, 1.0, 1.0, 0.95);

    // Token: 0x04000561 RID: 1377
    public static readonly Color Ink = PortraitData.Rgb(0.055, 0.07, 0.09, 1.0);
  }
}
