using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.App.Notch
{
  // Token: 0x02000136 RID: 310
  [NullableContext(1)]
  [Nullable(0)]
  public static class NotchPreferences
  {
    // Token: 0x17000312 RID: 786
    // (get) Token: 0x060010E0 RID: 4320 RVA: 0x00589EB8 File Offset: 0x00589EB8
    private static JsonObject Data
    {
      get
      {
        if (NotchPreferences.data != null)
        {
          return NotchPreferences.data;
        }
        try
        {
          NotchPreferences.data = (File.Exists(NotchPreferences.FilePath) ? (JsonNode.Parse(File.ReadAllText(NotchPreferences.FilePath), null, default(JsonDocumentOptions)) as JsonObject) : null);
        }
        catch (Exception ex)
        {
          Log.Warn("notch", "notch.json unreadable: " + ex.Message);
        }
        if (NotchPreferences.data == null)
        {
          NotchPreferences.data = new JsonObject(null);
        }
        if (NotchPreferences.data["hasClickedHomeReferralButton"] == null)
        {
          NotchPreferences.data["hasClickedHomeReferralButton"] = NotchPreferences.ReadLegacyHomeFlag();
        }
        return NotchPreferences.data;
      }
    }

    // Token: 0x060010E1 RID: 4321 RVA: 0x00589F88 File Offset: 0x00589F88
    private static bool ReadLegacyHomeFlag()
    {
      bool flag2;
      try
      {
        string text = AppPaths.Combine(new string[] { "home.json" });
        bool flag;
        if (File.Exists(text))
        {
          JsonNode jsonNode = JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions));
          if (jsonNode == null)
          {
            flag = false;
          }
          else
          {
            JsonNode jsonNode2 = jsonNode["HasClickedReferralButton"];
            flag = ((jsonNode2 != null) ? new bool?(jsonNode2.GetValue<bool>()) : null).GetValueOrDefault();
          }
        }
        else
        {
          flag = false;
        }
        flag2 = flag;
      }
      catch
      {
        flag2 = false;
      }
      return flag2;
    }

    // Token: 0x17000313 RID: 787
    // (get) Token: 0x060010E2 RID: 4322 RVA: 0x0058A020 File Offset: 0x0058A020
    // (set) Token: 0x060010E3 RID: 4323 RVA: 0x0058A03C File Offset: 0x0058A03C
    public static bool HasClickedHomeReferralButton
    {
      get
      {
        JsonNode jsonNode = NotchPreferences.Data["hasClickedHomeReferralButton"];
        return jsonNode != null && jsonNode.GetValue<bool>();
      }
      set
      {
        NotchPreferences.Data["hasClickedHomeReferralButton"] = value;
        NotchPreferences.Save();
      }
    }

    // Token: 0x060010E4 RID: 4324 RVA: 0x0058A058 File Offset: 0x0058A058
    private static void Save()
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(NotchPreferences.FilePath));
        File.WriteAllText(NotchPreferences.FilePath + ".tmp", NotchPreferences.Data.ToJsonString(new JsonSerializerOptions
        {
          WriteIndented = true
        }));
        File.Move(NotchPreferences.FilePath + ".tmp", NotchPreferences.FilePath, true);
      }
      catch (Exception ex)
      {
        Log.Warn("notch", "notch.json write failed: " + ex.Message);
      }
    }

    // Token: 0x0400077E RID: 1918
    private static readonly string FilePath = AppPaths.Combine(new string[] { "notch.json" });

    // Token: 0x0400077F RID: 1919
    [Nullable(2)]
    private static JsonObject data;

    // Token: 0x04000780 RID: 1920
    public const bool IsProactiveAgentSuggestionsPausedForRelease = true;
  }
}
