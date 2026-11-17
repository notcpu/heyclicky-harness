using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000090 RID: 144
  [NullableContext(1)]
  [Nullable(0)]
  public static class VoiceCatalog
  {
    // Token: 0x060008B4 RID: 2228 RVA: 0x0036A34C File Offset: 0x0036A34C
    [NullableContext(2)]
    public static bool IsGemini(string voiceId)
    {
      return voiceId != null && voiceId.StartsWith("gemini:", StringComparison.Ordinal);
    }

    // Token: 0x060008B5 RID: 2229 RVA: 0x0036A360 File Offset: 0x0036A360
    public static string GeminiName(string voiceId)
    {
      if (!VoiceCatalog.IsGemini(voiceId))
      {
        return voiceId;
      }
      int length = "gemini:".Length;
      return voiceId.Substring(length, voiceId.Length - length);
    }

    // Token: 0x060008B6 RID: 2230 RVA: 0x0036A394 File Offset: 0x0036A394
    [NullableContext(2)]
    public static ExpressiveVoice Expressive(string voiceId)
    {
      return VoiceCatalog.ExpressiveVoices.FirstOrDefault<ExpressiveVoice>((ExpressiveVoice v) => v.Id == voiceId);
    }

    // Token: 0x060008B7 RID: 2231 RVA: 0x0036A3C4 File Offset: 0x0036A3C4
    public static string OpenAIVoice(string voiceId)
    {
      if (!VoiceCatalog.IsGemini(voiceId))
      {
        return voiceId;
      }
      ExpressiveVoice expressiveVoice = VoiceCatalog.Expressive(voiceId);
      if (!(((expressiveVoice != null) ? expressiveVoice.Gender : null) == "female"))
      {
        return "cedar";
      }
      return "marin";
    }

    // Token: 0x060008B8 RID: 2232 RVA: 0x0036A3FC File Offset: 0x0036A3FC
    [NullableContext(2)]
    public static VoiceLanguage Language(string code)
    {
      if (code != null)
      {
        return VoiceCatalog.Languages.FirstOrDefault<VoiceLanguage>((VoiceLanguage l) => l.Code == code);
      }
      return null;
    }

    // Token: 0x060008B9 RID: 2233 RVA: 0x0036A438 File Offset: 0x0036A438
    [return: Nullable(2)]
    public static string NativeName(string code)
    {
      return VoiceCatalog.NativeNames.GetValueOrDefault(code);
    }

    // Token: 0x060008BA RID: 2234 RVA: 0x0036A448 File Offset: 0x0036A448
    public static IReadOnlyList<VoiceLanguage> PickerList([Nullable(2)] string search = null)
    {
      IEnumerable<string> popularCodes = VoiceCatalog.PopularCodes;
      Func<string, VoiceLanguage> func;
      if ((func = VoiceCatalog.<>O.<0>__Language) == null)
      {
        func = (VoiceCatalog.<>O.<0>__Language = new Func<string, VoiceLanguage>(VoiceCatalog.Language));
      }
      IEnumerable<VoiceLanguage> enumerable = popularCodes.Select<string, VoiceLanguage>(func).OfType<VoiceLanguage>().Concat<VoiceLanguage>(VoiceCatalog.Languages.Where<VoiceLanguage>((VoiceLanguage l) => !VoiceCatalog.PopularCodes.Contains(l.Code)));
      string query = VoiceCatalog.Fold(((search != null) ? search.Trim() : null) ?? "");
      if (query.Length != 0)
      {
        return enumerable.Where<VoiceLanguage>(delegate(VoiceLanguage l)
        {
          if (!VoiceCatalog.Fold(l.Name).Contains(query, StringComparison.Ordinal) && !VoiceCatalog.Fold(l.Code).StartsWith(query, StringComparison.Ordinal))
          {
            string nativeName = l.NativeName;
            return nativeName != null && VoiceCatalog.Fold(nativeName).Contains(query, StringComparison.Ordinal);
          }
          return true;
        }).ToList<VoiceLanguage>();
      }
      return enumerable.ToList<VoiceLanguage>();
    }

    // Token: 0x060008BB RID: 2235 RVA: 0x0036A500 File Offset: 0x0036A500
    private static string Fold(string text)
    {
      return new string((from c in text.ToLowerInvariant().Normalize(NormalizationForm.FormD)
        where CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark
        select c).ToArray<char>());
    }

    // Token: 0x060008BC RID: 2236 RVA: 0x0036A53C File Offset: 0x0036A53C
    [NullableContext(2)]
    public static bool IsEnglish(string code)
    {
      return code == "en";
    }

    // Token: 0x060008BD RID: 2237 RVA: 0x0036A54C File Offset: 0x0036A54C
    public static IReadOnlyList<string> PickedLanguages([Nullable(2)] string stored)
    {
      List<string> list = new List<string>();
      foreach (string text in (stored ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
      {
        if (VoiceCatalog.Language(text) != null && !list.Contains(text))
        {
          list.Add(text);
          if (list.Count == 5)
          {
            break;
          }
        }
      }
      return list;
    }

    // Token: 0x060008BE RID: 2238 RVA: 0x0036A5A8 File Offset: 0x0036A5A8
    [return: Nullable(2)]
    public static string StoredLanguages(IEnumerable<string> codes)
    {
      IReadOnlyList<string> readOnlyList = VoiceCatalog.PickedLanguages(string.Join(",", codes));
      if (readOnlyList.Count != 0)
      {
        return string.Join(",", readOnlyList);
      }
      return null;
    }

    // Token: 0x060008BF RID: 2239 RVA: 0x0036A5DC File Offset: 0x0036A5DC
    public static string SpokenList(IReadOnlyList<string> codes)
    {
      List<string> list = codes.Select<string, string>(delegate(string c)
      {
        VoiceLanguage voiceLanguage = VoiceCatalog.Language(c);
        if (voiceLanguage == null)
        {
          return null;
        }
        return voiceLanguage.Name;
      }).OfType<string>().ToList<string>();
      if (list.Count > 1)
      {
        string text = string.Join(", ", list.SkipLast<string>(1));
        string text2 = " and ";
        List<string> list2 = list;
        return text + text2 + list2[list2.Count - 1];
      }
      return list.FirstOrDefault<string>() ?? "";
    }

    // Token: 0x060008C0 RID: 2240 RVA: 0x0036A65C File Offset: 0x0036A65C
    [NullableContext(2)]
    public static VoiceLanguage LanguageForPreviews(string stored)
    {
      IEnumerable<string> enumerable = from c in VoiceCatalog.PickedLanguages(stored)
        where !VoiceCatalog.IsEnglish(c)
        select c;
      Func<string, VoiceLanguage> func;
      if ((func = VoiceCatalog.<>O.<0>__Language) == null)
      {
        func = (VoiceCatalog.<>O.<0>__Language = new Func<string, VoiceLanguage>(VoiceCatalog.Language));
      }
      return enumerable.Select<string, VoiceLanguage>(func).FirstOrDefault<VoiceLanguage>();
    }

    // Token: 0x060008C1 RID: 2241 RVA: 0x0036A6B8 File Offset: 0x0036A6B8
    [NullableContext(2)]
    public static string CatalogCode(string tag)
    {
      string[] array = (tag ?? "").Replace('_', '-').Split('-', StringSplitOptions.RemoveEmptyEntries);
      if (array.Length == 0)
      {
        return null;
      }
      string text = array[0].ToLowerInvariant();
      string text2 = array.Skip<string>(1).FirstOrDefault<string>((string p) => p.Length == 4);
      if (text2 != null)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("-");
        defaultInterpolatedStringHandler.AppendFormatted<char>(char.ToUpperInvariant(text2[0]));
        string text3 = text2;
        defaultInterpolatedStringHandler.AppendFormatted(text3.Substring(1, text3.Length - 1).ToLowerInvariant());
        string text4 = defaultInterpolatedStringHandler.ToStringAndClear();
        if (VoiceCatalog.Language(text4) != null)
        {
          return text4;
        }
      }
      if (text == "zh")
      {
        if (array.Skip<string>(1).Any<string>(delegate(string p)
        {
          string text6 = p.ToUpperInvariant();
          return text6 == "TW" || text6 == "HK" || text6 == "MO";
        }))
        {
          return "zh-Hant";
        }
      }
      if (VoiceCatalog.Language(text) != null)
      {
        return text;
      }
      string text5;
      if (!VoiceCatalog.TagAliases.TryGetValue(text, out text5) || VoiceCatalog.Language(text5) == null)
      {
        return null;
      }
      return text5;
    }

    // Token: 0x060008C2 RID: 2242 RVA: 0x0036A7E8 File Offset: 0x0036A7E8
    public static IReadOnlyList<string> SystemLanguageCodes(IEnumerable<string> tags)
    {
      Func<string, string> func;
      if ((func = VoiceCatalog.<>O.<1>__CatalogCode) == null)
      {
        func = (VoiceCatalog.<>O.<1>__CatalogCode = new Func<string, string>(VoiceCatalog.CatalogCode));
      }
      return tags.Select<string, string>(func).OfType<string>().Distinct<string>()
        .ToList<string>();
    }

    // Token: 0x060008C3 RID: 2243 RVA: 0x0036A81C File Offset: 0x0036A81C
    [NullableContext(2)]
    public static string SpokenLanguageNote(string stored)
    {
      IReadOnlyList<string> readOnlyList = VoiceCatalog.PickedLanguages(stored);
      if (readOnlyList.Count != 0)
      {
        VoiceLanguage voiceLanguage = VoiceCatalog.Language(readOnlyList[0]);
        if (voiceLanguage != null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
          if (readOnlyList.Count == 1)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(144, 2);
            defaultInterpolatedStringHandler.AppendLiteral("[Language] They picked ");
            defaultInterpolatedStringHandler.AppendFormatted(voiceLanguage.Name);
            defaultInterpolatedStringHandler.AppendLiteral(" in Settings → Voice: write every say() line and every word you draw in ");
            defaultInterpolatedStringHandler.AppendFormatted(voiceLanguage.Name);
            defaultInterpolatedStringHandler.AppendLiteral(", whatever language they speak or type to you in.");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(209, 2);
          defaultInterpolatedStringHandler.AppendLiteral("[Language] They picked ");
          defaultInterpolatedStringHandler.AppendFormatted(VoiceCatalog.SpokenList(readOnlyList));
          defaultInterpolatedStringHandler.AppendLiteral(" in Settings → Voice: write every say() line and every word you draw in whichever of these they are using with you, mixing them the way they do. If they use another language, answer in ");
          defaultInterpolatedStringHandler.AppendFormatted(voiceLanguage.Name);
          defaultInterpolatedStringHandler.AppendLiteral(".");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      return null;
    }

    // Token: 0x060008C4 RID: 2244 RVA: 0x0036A8F8 File Offset: 0x0036A8F8
    // Note: this type is marked as 'beforefieldinit'.
    static VoiceCatalog()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["sq"] = "Shqip";
      dictionary["am"] = "አማርኛ";
      dictionary["ar"] = "العربية";
      dictionary["hy"] = "Հայերեն";
      dictionary["as"] = "অসম\u09c0য\u09bc\u09be";
      dictionary["az"] = "Azərbaycan";
      dictionary["ba"] = "Башҡорт";
      dictionary["eu"] = "Euskara";
      dictionary["be"] = "Беларуская";
      dictionary["bem"] = "Ichibemba";
      dictionary["bn"] = "ব\u09be\u0982ল\u09be";
      dictionary["bho"] = "भ\u094bजप\u0941र\u0940";
      dictionary["bs"] = "Bosanski";
      dictionary["bg"] = "Български";
      dictionary["my"] = "မ\u103cန\u103aမ\u102c";
      dictionary["yue"] = "廣東話";
      dictionary["ca"] = "Català";
      dictionary["ckb"] = "کوردیی ناوەندی";
      dictionary["zh-Hans"] = "简体中文";
      dictionary["zh-Hant"] = "繁體中文";
      dictionary["hr"] = "Hrvatski";
      dictionary["cs"] = "Čeština";
      dictionary["da"] = "Dansk";
      dictionary["nl"] = "Nederlands";
      dictionary["dz"] = "ར\u0fab\u0f7cང་ཁ་";
      dictionary["et"] = "Eesti";
      dictionary["fi"] = "Suomi";
      dictionary["fr"] = "Français";
      dictionary["gl"] = "Galego";
      dictionary["lg"] = "Luganda";
      dictionary["ka"] = "ქართული";
      dictionary["de"] = "Deutsch";
      dictionary["el"] = "Ελληνικά";
      dictionary["gn"] = "Avañe’ẽ";
      dictionary["gu"] = "ગ\u0ac1જર\u0abeત\u0ac0";
      dictionary["ht"] = "Kreyòl Ayisyen";
      dictionary["he"] = "עברית";
      dictionary["hi"] = "ह\u093fन\u094dद\u0940";
      dictionary["hu"] = "Magyar";
      dictionary["is"] = "Íslenska";
      dictionary["id"] = "Indonesia";
      dictionary["it"] = "Italiano";
      dictionary["ja"] = "日本語";
      dictionary["jv"] = "Jawa";
      dictionary["kab"] = "Taqbaylit";
      dictionary["kam"] = "Kikamba";
      dictionary["kn"] = "ಕನ\u0ccdನಡ";
      dictionary["ks-Arab"] = "کٲش\u064fر (نسخ)";
      dictionary["ks-Deva"] = "क\u0949श\u0941र (द\u0947वन\u093eगर\u0940)";
      dictionary["kk"] = "Қазақ тілі";
      dictionary["km"] = "ខ\u17d2ម\u17c2រ";
      dictionary["ki"] = "Gikuyu";
      dictionary["rw"] = "Ikinyarwanda";
      dictionary["ko"] = "한국어";
      dictionary["ky"] = "Кыргызча";
      dictionary["lo"] = "ລາວ";
      dictionary["lv"] = "Latviešu";
      dictionary["ln"] = "Lingála";
      dictionary["lt"] = "Lietuvių";
      dictionary["lb"] = "Lëtzebuergesch";
      dictionary["mk"] = "Македонски";
      dictionary["mai"] = "म\u0948थ\u093fल\u0940";
      dictionary["ms"] = "Bahasa Melayu";
      dictionary["ml"] = "മലയ\u0d3eള\u0d02";
      dictionary["mt"] = "Malti";
      dictionary["mni"] = "ম\u09c8ত\u09c8ল\u09cbন\u09cd";
      dictionary["mr"] = "मर\u093eठ\u0940";
      dictionary["mn"] = "Монгол";
      dictionary["ne"] = "न\u0947प\u093eल\u0940";
      dictionary["nso"] = "Sesotho sa Leboa";
      dictionary["nb"] = "Norsk bokmål";
      dictionary["nn"] = "Norsk nynorsk";
      dictionary["or"] = "ଓଡ\u0b3c\u0b3fଆ";
      dictionary["ps"] = "پښتو";
      dictionary["fa"] = "فارسی";
      dictionary["pl"] = "Polski";
      dictionary["pt"] = "Português";
      dictionary["pa"] = "ਪ\u0a70ਜ\u0a3eਬ\u0a40";
      dictionary["ro"] = "Română";
      dictionary["ru"] = "Русский";
      dictionary["sat"] = "ᱥᱟᱱᱛᱟᱲᱤ";
      dictionary["sr"] = "Српски";
      dictionary["sd"] = "سنڌي";
      dictionary["si"] = "ස\u0dd2\u0d82හල";
      dictionary["sk"] = "Slovenčina";
      dictionary["sl"] = "Slovenščina";
      dictionary["so"] = "Soomaali";
      dictionary["st"] = "Sesotho";
      dictionary["es"] = "Español";
      dictionary["sw"] = "Kiswahili";
      dictionary["ss"] = "siSwati";
      dictionary["sv"] = "Svenska";
      dictionary["tg"] = "Тоҷикӣ";
      dictionary["ta"] = "தம\u0bbfழ\u0bcd";
      dictionary["te"] = "త\u0c46ల\u0c41గ\u0c41";
      dictionary["th"] = "ไทย";
      dictionary["ti"] = "ትግርኛ";
      dictionary["tr"] = "Türkçe";
      dictionary["ur"] = "اردو";
      dictionary["ug"] = "ئۇيغۇرچە";
      dictionary["uz"] = "O‘zbek";
      dictionary["vi"] = "Tiếng Việt";
      VoiceCatalog.NativeNames = dictionary;
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      dictionary2["no"] = "nb";
      dictionary2["tl"] = "fil";
      dictionary2["iw"] = "he";
      dictionary2["in"] = "id";
      dictionary2["zh"] = "zh-Hans";
      VoiceCatalog.TagAliases = dictionary2;
    }

    // Token: 0x04000414 RID: 1044
    public const string GeminiPrefix = "gemini:";

    // Token: 0x04000415 RID: 1045
    public static readonly IReadOnlyList<ExpressiveVoice> ExpressiveVoices = new <>z__ReadOnlyArray<ExpressiveVoice>(new ExpressiveVoice[]
    {
      new ExpressiveVoice("gemini:Puck", "Puck", "Upbeat", "male"),
      new ExpressiveVoice("gemini:Zephyr", "Zephyr", "Bright", "female"),
      new ExpressiveVoice("gemini:Aoede", "Aoede", "Breezy", "female"),
      new ExpressiveVoice("gemini:Fenrir", "Fenrir", "Excitable", "male"),
      new ExpressiveVoice("gemini:Leda", "Leda", "Youthful", "female"),
      new ExpressiveVoice("gemini:Sulafat", "Sulafat", "Warm", "female"),
      new ExpressiveVoice("gemini:Achird", "Achird", "Friendly", "male"),
      new ExpressiveVoice("gemini:Sadachbia", "Sadachbia", "Lively", "male"),
      new ExpressiveVoice("gemini:Kore", "Kore", "Firm", "female"),
      new ExpressiveVoice("gemini:Charon", "Charon", "Informative", "male"),
      new ExpressiveVoice("gemini:Orus", "Orus", "Firm", "male"),
      new ExpressiveVoice("gemini:Callirrhoe", "Callirrhoe", "Easy-going", "female"),
      new ExpressiveVoice("gemini:Autonoe", "Autonoe", "Bright", "female"),
      new ExpressiveVoice("gemini:Enceladus", "Enceladus", "Breathy", "male"),
      new ExpressiveVoice("gemini:Iapetus", "Iapetus", "Clear", "male"),
      new ExpressiveVoice("gemini:Umbriel", "Umbriel", "Easy-going", "male"),
      new ExpressiveVoice("gemini:Algieba", "Algieba", "Smooth", "male"),
      new ExpressiveVoice("gemini:Despina", "Despina", "Smooth", "female"),
      new ExpressiveVoice("gemini:Erinome", "Erinome", "Clear", "female"),
      new ExpressiveVoice("gemini:Algenib", "Algenib", "Gravelly", "male"),
      new ExpressiveVoice("gemini:Rasalgethi", "Rasalgethi", "Informative", "male"),
      new ExpressiveVoice("gemini:Laomedeia", "Laomedeia", "Upbeat", "female"),
      new ExpressiveVoice("gemini:Achernar", "Achernar", "Soft", "female"),
      new ExpressiveVoice("gemini:Alnilam", "Alnilam", "Firm", "male"),
      new ExpressiveVoice("gemini:Schedar", "Schedar", "Even", "male"),
      new ExpressiveVoice("gemini:Gacrux", "Gacrux", "Mature", "female"),
      new ExpressiveVoice("gemini:Pulcherrima", "Pulcherrima", "Forward", "female"),
      new ExpressiveVoice("gemini:Zubenelgenubi", "Zubenelgenubi", "Casual", "male"),
      new ExpressiveVoice("gemini:Vindemiatrix", "Vindemiatrix", "Gentle", "female"),
      new ExpressiveVoice("gemini:Sadaltager", "Sadaltager", "Knowledgeable", "male")
    });

    // Token: 0x04000416 RID: 1046
    public static readonly IReadOnlyList<VoiceLanguage> Languages = new <>z__ReadOnlyArray<VoiceLanguage>(new VoiceLanguage[]
    {
      new VoiceLanguage("ace-Arab", "Acehnese (Arabic script)"),
      new VoiceLanguage("af", "Afrikaans"),
      new VoiceLanguage("ak", "Akan"),
      new VoiceLanguage("sq", "Albanian"),
      new VoiceLanguage("am", "Amharic"),
      new VoiceLanguage("ar", "Arabic"),
      new VoiceLanguage("ar-Latn", "Arabic (Latin script)"),
      new VoiceLanguage("hy", "Armenian"),
      new VoiceLanguage("as", "Assamese"),
      new VoiceLanguage("awa", "Awadhi"),
      new VoiceLanguage("az", "Azerbaijani"),
      new VoiceLanguage("ban", "Balinese"),
      new VoiceLanguage("bjn-Latn", "Banjar"),
      new VoiceLanguage("bjn-Arab", "Banjar (Arabic script)"),
      new VoiceLanguage("ba", "Bashkir"),
      new VoiceLanguage("eu", "Basque"),
      new VoiceLanguage("be", "Belarusian"),
      new VoiceLanguage("bem", "Bemba"),
      new VoiceLanguage("bn", "Bengali"),
      new VoiceLanguage("bho", "Bhojpuri"),
      new VoiceLanguage("bs", "Bosnian"),
      new VoiceLanguage("bug", "Buginese"),
      new VoiceLanguage("bg", "Bulgarian"),
      new VoiceLanguage("my", "Burmese"),
      new VoiceLanguage("yue", "Cantonese"),
      new VoiceLanguage("ca", "Catalan"),
      new VoiceLanguage("ceb", "Cebuano"),
      new VoiceLanguage("ckb", "Central Kurdish"),
      new VoiceLanguage("hne", "Chhattisgarhi"),
      new VoiceLanguage("zh-Hans", "Chinese (Simplified)"),
      new VoiceLanguage("zh-Hant", "Chinese (Traditional)"),
      new VoiceLanguage("crh", "Crimean Tatar"),
      new VoiceLanguage("hr", "Croatian"),
      new VoiceLanguage("cs", "Czech"),
      new VoiceLanguage("da", "Danish"),
      new VoiceLanguage("prs", "Dari"),
      new VoiceLanguage("nl", "Dutch"),
      new VoiceLanguage("dyu", "Dyula"),
      new VoiceLanguage("dz", "Dzongkha"),
      new VoiceLanguage("arz", "Egyptian Arabic"),
      new VoiceLanguage("en", "English"),
      new VoiceLanguage("et", "Estonian"),
      new VoiceLanguage("fil", "Filipino"),
      new VoiceLanguage("fi", "Finnish"),
      new VoiceLanguage("fr", "French"),
      new VoiceLanguage("fuv", "Fulfulde"),
      new VoiceLanguage("gl", "Galician"),
      new VoiceLanguage("lg", "Ganda"),
      new VoiceLanguage("ka", "Georgian"),
      new VoiceLanguage("de", "German"),
      new VoiceLanguage("el", "Greek"),
      new VoiceLanguage("gn", "Guarani"),
      new VoiceLanguage("gu", "Gujarati"),
      new VoiceLanguage("ht", "Haitian Creole"),
      new VoiceLanguage("ha", "Hausa"),
      new VoiceLanguage("he", "Hebrew"),
      new VoiceLanguage("hi", "Hindi"),
      new VoiceLanguage("hu", "Hungarian"),
      new VoiceLanguage("is", "Icelandic"),
      new VoiceLanguage("ig", "Igbo"),
      new VoiceLanguage("ilo", "Ilocano"),
      new VoiceLanguage("id", "Indonesian"),
      new VoiceLanguage("it", "Italian"),
      new VoiceLanguage("ja", "Japanese"),
      new VoiceLanguage("jv", "Javanese"),
      new VoiceLanguage("kab", "Kabyle"),
      new VoiceLanguage("kam", "Kamba"),
      new VoiceLanguage("kn", "Kannada"),
      new VoiceLanguage("ks-Arab", "Kashmiri (Arabic script)"),
      new VoiceLanguage("ks-Deva", "Kashmiri (Devanagari)"),
      new VoiceLanguage("kk", "Kazakh"),
      new VoiceLanguage("km", "Khmer"),
      new VoiceLanguage("ki", "Kikuyu"),
      new VoiceLanguage("rw", "Kinyarwanda"),
      new VoiceLanguage("kg", "Kongo"),
      new VoiceLanguage("ko", "Korean"),
      new VoiceLanguage("ky", "Kyrgyz"),
      new VoiceLanguage("lo", "Lao"),
      new VoiceLanguage("ltg", "Latgalian"),
      new VoiceLanguage("lv", "Latvian"),
      new VoiceLanguage("ln", "Lingala"),
      new VoiceLanguage("lt", "Lithuanian"),
      new VoiceLanguage("lb", "Luxembourgish"),
      new VoiceLanguage("mk", "Macedonian"),
      new VoiceLanguage("mag", "Magahi"),
      new VoiceLanguage("mai", "Maithili"),
      new VoiceLanguage("ms", "Malay"),
      new VoiceLanguage("ml", "Malayalam"),
      new VoiceLanguage("mt", "Maltese"),
      new VoiceLanguage("mni", "Manipuri"),
      new VoiceLanguage("mr", "Marathi"),
      new VoiceLanguage("min", "Minangkabau"),
      new VoiceLanguage("min-Arab", "Minangkabau (Arabic script)"),
      new VoiceLanguage("lus", "Mizo"),
      new VoiceLanguage("mn", "Mongolian"),
      new VoiceLanguage("ne", "Nepali"),
      new VoiceLanguage("nso", "Northern Sotho"),
      new VoiceLanguage("nb", "Norwegian Bokmål"),
      new VoiceLanguage("nn", "Norwegian Nynorsk"),
      new VoiceLanguage("ny", "Nyanja"),
      new VoiceLanguage("oc", "Occitan"),
      new VoiceLanguage("or", "Odia"),
      new VoiceLanguage("pag", "Pangasinan"),
      new VoiceLanguage("ps", "Pashto"),
      new VoiceLanguage("fa", "Persian"),
      new VoiceLanguage("pl", "Polish"),
      new VoiceLanguage("pt", "Portuguese"),
      new VoiceLanguage("pa", "Punjabi"),
      new VoiceLanguage("ro", "Romanian"),
      new VoiceLanguage("ru", "Russian"),
      new VoiceLanguage("sat", "Santali"),
      new VoiceLanguage("sr", "Serbian"),
      new VoiceLanguage("sd", "Sindhi"),
      new VoiceLanguage("si", "Sinhala"),
      new VoiceLanguage("sk", "Slovak"),
      new VoiceLanguage("sl", "Slovenian"),
      new VoiceLanguage("so", "Somali"),
      new VoiceLanguage("azb", "South Azerbaijani"),
      new VoiceLanguage("st", "Southern Sotho"),
      new VoiceLanguage("es", "Spanish"),
      new VoiceLanguage("sw", "Swahili"),
      new VoiceLanguage("ss", "Swati"),
      new VoiceLanguage("sv", "Swedish"),
      new VoiceLanguage("tg", "Tajik"),
      new VoiceLanguage("ta", "Tamil"),
      new VoiceLanguage("te", "Telugu"),
      new VoiceLanguage("th", "Thai"),
      new VoiceLanguage("ti", "Tigrinya"),
      new VoiceLanguage("tr", "Turkish"),
      new VoiceLanguage("ur", "Urdu"),
      new VoiceLanguage("ug", "Uyghur"),
      new VoiceLanguage("uz", "Uzbek"),
      new VoiceLanguage("vi", "Vietnamese")
    });

    // Token: 0x04000417 RID: 1047
    public static readonly IReadOnlyList<string> PopularCodes = new <>z__ReadOnlyArray<string>(new string[]
    {
      "en", "es", "fr", "de", "pt", "it", "hi", "ja", "ko", "zh-Hans",
      "ar", "ru"
    });

    // Token: 0x04000418 RID: 1048
    private static readonly Dictionary<string, string> NativeNames;

    // Token: 0x04000419 RID: 1049
    public const int MaxPickedLanguages = 5;

    // Token: 0x0400041A RID: 1050
    private static readonly Dictionary<string, string> TagAliases;

    // Token: 0x02000366 RID: 870
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040017B9 RID: 6073
      [Nullable(0)]
      public static Func<string, VoiceLanguage> <0>__Language;

      // Token: 0x040017BA RID: 6074
      [Nullable(0)]
      public static Func<string, string> <1>__CatalogCode;
    }
  }
}
