using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HeyClicky.App.Portraits
{
  // Token: 0x02000108 RID: 264
  [NullableContext(1)]
  [Nullable(new byte[] { 0, 1 })]
  public sealed class PortraitAppearanceJsonConverter : JsonConverter<PortraitAppearance>
  {
    // Token: 0x06000DC7 RID: 3527 RVA: 0x0056CF4C File Offset: 0x0056CF4C
    public override PortraitAppearance Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      PortraitAppearance portraitAppearance2;
      using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
      {
        PortraitAppearanceJsonConverter.<>c__DisplayClass0_0 CS$<>8__locals1;
        CS$<>8__locals1.o = jsonDocument.RootElement;
        PortraitAppearance portraitAppearance = PortraitAppearance.Make(Hairstyle.Bob, Colorway.Blue);
        Hairstyle hairstyle = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Hairstyle>("hairstyle", portraitAppearance.Hairstyle, (Hairstyle h) => h.RawValue(), ref CS$<>8__locals1);
        Colorway colorway = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Colorway>("hairColor", portraitAppearance.HairColor, (Colorway c) => c.RawValue(), ref CS$<>8__locals1);
        Outfit outfit = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Outfit>("outfitStyle", hairstyle.Pack().Outfits()[0], (Outfit x) => x.RawValue(), ref CS$<>8__locals1);
        Colorway colorway2 = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Colorway>("outfitColor", colorway, (Colorway c) => c.RawValue(), ref CS$<>8__locals1);
        Colorway colorway3 = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Colorway>("accentColor", colorway2, (Colorway c) => c.RawValue(), ref CS$<>8__locals1);
        SkinTone skinTone = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<SkinTone>("skinTone", colorway.DefaultSkinTone(), (SkinTone s) => s.RawValue(), ref CS$<>8__locals1);
        Colorway colorway4 = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Colorway>("eyeColor", colorway2, (Colorway c) => c.RawValue(), ref CS$<>8__locals1);
        Colorway colorway5 = PortraitAppearanceJsonConverter.<Read>g__Opt|0_1<Colorway>("backgroundColor", (hairstyle.Pack() == PortraitPack.ClassicClouds) ? colorway.ClassicCloudBackground() : colorway, (Colorway c) => c.RawValue(), ref CS$<>8__locals1);
        portraitAppearance2 = new PortraitAppearance(hairstyle, colorway, outfit, colorway2, colorway3, skinTone, colorway4, colorway5);
      }
      return portraitAppearance2;
    }

    // Token: 0x06000DC8 RID: 3528 RVA: 0x0056D15C File Offset: 0x0056D15C
    public override void Write(Utf8JsonWriter w, PortraitAppearance a, JsonSerializerOptions options)
    {
      w.WriteStartObject();
      w.WriteString("hairstyle", a.Hairstyle.RawValue());
      w.WriteString("hairColor", a.HairColor.RawValue());
      w.WriteString("outfitStyle", a.OutfitStyle.RawValue());
      w.WriteString("outfitColor", a.OutfitColor.RawValue());
      w.WriteString("accentColor", a.AccentColor.RawValue());
      w.WriteString("skinTone", a.SkinTone.RawValue());
      w.WriteString("eyeColor", a.EyeColor.RawValue());
      w.WriteString("backgroundColor", a.BackgroundColor.RawValue());
      w.WriteNumber("backgroundPaletteVersion", 1);
      w.WriteEndObject();
    }

    // Token: 0x06000DCA RID: 3530 RVA: 0x0056D23C File Offset: 0x0056D23C
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <Read>g__S|0_0(string k, ref PortraitAppearanceJsonConverter.<>c__DisplayClass0_0 A_1)
    {
      JsonElement jsonElement;
      if (!A_1.o.TryGetProperty(k, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
      {
        return null;
      }
      return jsonElement.GetString();
    }

    // Token: 0x06000DCB RID: 3531 RVA: 0x0056D26C File Offset: 0x0056D26C
    [NullableContext(0)]
    [CompilerGenerated]
    internal static T <Read>g__Opt|0_1<T>([Nullable(1)] string key, T fallback, [Nullable(new byte[] { 1, 0, 1 })] Func<T, string> raw, ref PortraitAppearanceJsonConverter.<>c__DisplayClass0_0 A_3) where T : struct, Enum
    {
      string s = PortraitAppearanceJsonConverter.<Read>g__S|0_0(key, ref A_3);
      if (s == null)
      {
        return fallback;
      }
      return Enum.GetValues<T>().Cast<T?>().FirstOrDefault<T?>((T? v) => raw(v.Value) == s)
        .GetValueOrDefault(fallback);
    }
  }
}
