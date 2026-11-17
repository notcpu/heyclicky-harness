using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Dictation
{
  // Token: 0x020001C7 RID: 455
  [NullableContext(1)]
  [Nullable(0)]
  public static class DictationLanguageCatalog
  {
    // Token: 0x06001A5E RID: 6750 RVA: 0x003D6A0C File Offset: 0x003D6A0C
    public static IReadOnlyList<DictationLanguage> ForPicker()
    {
      Dictionary<string, DictationLanguage> byCode = DictationLanguageCatalog.Specific.ToDictionary<DictationLanguage, string>((DictationLanguage l) => l.Code);
      IEnumerable<DictationLanguage> enumerable = from c in DictationLanguageCatalog.PopularCodes.Where<string>(new Func<string, bool>(byCode.ContainsKey))
        select byCode[c];
      List<DictationLanguage> list = new List<DictationLanguage>();
      list.AddRange(enumerable);
      list.AddRange(DictationLanguageCatalog.Specific.Where<DictationLanguage>((DictationLanguage l) => !DictationLanguageCatalog.PopularCodes.Contains(l.Code)).OrderBy<DictationLanguage, string>((DictationLanguage l) => l.DisplayName, StringComparer.Ordinal));
      return new <>z__ReadOnlyList<DictationLanguage>(list);
    }

    // Token: 0x06001A5F RID: 6751 RVA: 0x003D6AE8 File Offset: 0x003D6AE8
    [NullableContext(2)]
    public static bool IsKnownSpecific(string code)
    {
      return code != null && DictationLanguageCatalog.Specific.Any<DictationLanguage>((DictationLanguage l) => l.Code == code);
    }

    // Token: 0x04000B52 RID: 2898
    public const string MultilingualCode = "multi";

    // Token: 0x04000B53 RID: 2899
    public const string DefaultSpecificCode = "en";

    // Token: 0x04000B54 RID: 2900
    private static readonly string[] PopularCodes = new string[]
    {
      "en", "es", "fr", "de", "hi", "zh", "ja", "ko", "pt", "ru",
      "ar", "it"
    };

    // Token: 0x04000B55 RID: 2901
    public static readonly IReadOnlyList<DictationLanguage> Specific = new <>z__ReadOnlyArray<DictationLanguage>(new DictationLanguage[]
    {
      new DictationLanguage("af", "Afrikaans", null),
      new DictationLanguage("am", "Amharic", "አማርኛ"),
      new DictationLanguage("ar", "Arabic", "العربية"),
      new DictationLanguage("hy", "Armenian", "Հայերեն"),
      new DictationLanguage("rup", "Aromanian", "Armãneashti"),
      new DictationLanguage("as", "Assamese", "অসম\u09c0য\u09bc\u09be"),
      new DictationLanguage("az", "Azerbaijani", "Azərbaycanca"),
      new DictationLanguage("be", "Belarusian", "Беларуская"),
      new DictationLanguage("bn", "Bengali", "ব\u09be\u0982ল\u09be"),
      new DictationLanguage("bs", "Bosnian", "Bosanski"),
      new DictationLanguage("bg", "Bulgarian", "Български"),
      new DictationLanguage("my", "Burmese", "မ\u103cန\u103aမ\u102c"),
      new DictationLanguage("yue", "Cantonese", "粵語"),
      new DictationLanguage("kea", "Cape Verdean Creole", "Kabuverdianu"),
      new DictationLanguage("ca", "Catalan", "Català"),
      new DictationLanguage("ceb", "Cebuano", "Sinugboanon"),
      new DictationLanguage("zh", "Chinese", "中文"),
      new DictationLanguage("hr", "Croatian", "Hrvatski"),
      new DictationLanguage("cs", "Czech", "Čeština"),
      new DictationLanguage("da", "Danish", "Dansk"),
      new DictationLanguage("nl", "Dutch", "Nederlands"),
      new DictationLanguage("en", "English", null),
      new DictationLanguage("et", "Estonian", "Eesti"),
      new DictationLanguage("fi", "Finnish", "Suomi"),
      new DictationLanguage("fr", "French", "Français"),
      new DictationLanguage("gl", "Galician", "Galego"),
      new DictationLanguage("ka", "Georgian", "ქართული"),
      new DictationLanguage("de", "German", "Deutsch"),
      new DictationLanguage("el", "Greek", "Ελληνικά"),
      new DictationLanguage("gu", "Gujarati", "ગ\u0ac1જર\u0abeત\u0ac0"),
      new DictationLanguage("ha", "Hausa", null),
      new DictationLanguage("he", "Hebrew", "עברית"),
      new DictationLanguage("hi", "Hindi", "ह\u093fन\u094dद\u0940"),
      new DictationLanguage("hu", "Hungarian", "Magyar"),
      new DictationLanguage("is", "Icelandic", "Íslenska"),
      new DictationLanguage("id", "Indonesian", "Bahasa Indonesia"),
      new DictationLanguage("it", "Italian", "Italiano"),
      new DictationLanguage("ja", "Japanese", "日本語"),
      new DictationLanguage("jv", "Javanese", "Basa Jawa"),
      new DictationLanguage("kn", "Kannada", "ಕನ\u0ccdನಡ"),
      new DictationLanguage("kk", "Kazakh", "Қазақ тілі"),
      new DictationLanguage("km", "Khmer", "ខ\u17d2ម\u17c2រ"),
      new DictationLanguage("ko", "Korean", "한국어"),
      new DictationLanguage("ky", "Kyrgyz", "Кыргызча"),
      new DictationLanguage("lv", "Latvian", "Latviešu"),
      new DictationLanguage("ln", "Lingala", "Lingála"),
      new DictationLanguage("lt", "Lithuanian", "Lietuvių"),
      new DictationLanguage("mk", "Macedonian", "Македонски"),
      new DictationLanguage("ms", "Malay", "Bahasa Melayu"),
      new DictationLanguage("ml", "Malayalam", "മലയ\u0d3eള\u0d02"),
      new DictationLanguage("mt", "Maltese", "Malti"),
      new DictationLanguage("mr", "Marathi", "मर\u093eठ\u0940"),
      new DictationLanguage("mn", "Mongolian", "Монгол"),
      new DictationLanguage("ne", "Nepali", "न\u0947प\u093eल\u0940"),
      new DictationLanguage("no", "Norwegian", "Norsk"),
      new DictationLanguage("or", "Odia", "ଓଡ\u0b3c\u0b3fଆ"),
      new DictationLanguage("ps", "Pashto", "پښتو"),
      new DictationLanguage("fa", "Persian", "فارسی"),
      new DictationLanguage("pl", "Polish", "Polski"),
      new DictationLanguage("pt", "Portuguese", "Português"),
      new DictationLanguage("pa", "Punjabi", "ਪ\u0a70ਜ\u0a3eਬ\u0a40"),
      new DictationLanguage("ro", "Romanian", "Română"),
      new DictationLanguage("ru", "Russian", "Русский"),
      new DictationLanguage("sr", "Serbian", "Српски"),
      new DictationLanguage("sd", "Sindhi", "سنڌي"),
      new DictationLanguage("si", "Sinhala", "ස\u0dd2\u0d82හල"),
      new DictationLanguage("sk", "Slovak", "Slovenčina"),
      new DictationLanguage("sl", "Slovenian", "Slovenščina"),
      new DictationLanguage("so", "Somali", "Soomaali"),
      new DictationLanguage("es", "Spanish", "Español"),
      new DictationLanguage("sw", "Swahili", "Kiswahili"),
      new DictationLanguage("sv", "Swedish", "Svenska"),
      new DictationLanguage("tl", "Tagalog", "Tagalog"),
      new DictationLanguage("tg", "Tajik", "Тоҷикӣ"),
      new DictationLanguage("ta", "Tamil", "தம\u0bbfழ\u0bcd"),
      new DictationLanguage("te", "Telugu", "త\u0c46ల\u0c41గ\u0c41"),
      new DictationLanguage("th", "Thai", "ไทย"),
      new DictationLanguage("tr", "Turkish", "Türkçe"),
      new DictationLanguage("uk", "Ukrainian", "Українська"),
      new DictationLanguage("ur", "Urdu", "اردو"),
      new DictationLanguage("uz", "Uzbek", "Oʻzbekcha"),
      new DictationLanguage("vi", "Vietnamese", "Tiếng Việt"),
      new DictationLanguage("yo", "Yoruba", "Yorùbá")
    });
  }
}
