using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using HeyClicky.Core.Configuration;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CD RID: 205
  [NullableContext(1)]
  [Nullable(0)]
  public static class DeveloperFlags
  {
    // Token: 0x14000021 RID: 33
    // (add) Token: 0x060008E5 RID: 2277 RVA: 0x00541E64 File Offset: 0x00541E64
    // (remove) Token: 0x060008E6 RID: 2278 RVA: 0x00541E98 File Offset: 0x00541E98
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action Changed;

    // Token: 0x17000170 RID: 368
    // (get) Token: 0x060008E7 RID: 2279 RVA: 0x00541ECC File Offset: 0x00541ECC
    // (set) Token: 0x060008E8 RID: 2280 RVA: 0x00541ED0 File Offset: 0x00541ED0
    public static bool FullAgentActivity
    {
      get
      {
        return false;
      }
      set
      {
        DeveloperFlags.Write("full_agent_activity_debug", value);
      }
    }

    // Token: 0x060008E9 RID: 2281 RVA: 0x00541EE0 File Offset: 0x00541EE0
    private static bool Read(string key)
    {
      bool flag;
      using (DeveloperFlags.Gate.EnterScope())
      {
        if (DeveloperFlags.data == null)
        {
          DeveloperFlags.data = DeveloperFlags.Load();
        }
        JsonValue jsonValue = DeveloperFlags.data[key] as JsonValue;
        bool flag2;
        flag = jsonValue != null && jsonValue.TryGetValue<bool>(out flag2) && flag2;
      }
      return flag;
    }

    // Token: 0x060008EA RID: 2282 RVA: 0x00541F48 File Offset: 0x00541F48
    private static void Write(string key, bool value)
    {
      using (DeveloperFlags.Gate.EnterScope())
      {
        if (DeveloperFlags.data == null)
        {
          DeveloperFlags.data = DeveloperFlags.Load();
        }
        DeveloperFlags.data[key] = value;
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(DeveloperFlags.FilePath));
          File.WriteAllText(DeveloperFlags.FilePath + ".tmp", DeveloperFlags.data.ToJsonString(null));
          File.Move(DeveloperFlags.FilePath + ".tmp", DeveloperFlags.FilePath, true);
        }
        catch (Exception ex)
        {
          Log.Warn("settings", "developer-flags.json write failed: " + ex.Message);
        }
      }
      Action changed = DeveloperFlags.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x060008EB RID: 2283 RVA: 0x00542020 File Offset: 0x00542020
    private static JsonObject Load()
    {
      try
      {
        if (File.Exists(DeveloperFlags.FilePath))
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(DeveloperFlags.FilePath), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            return jsonObject;
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "developer-flags.json unreadable: " + ex.Message);
      }
      return new JsonObject(null);
    }

    // Token: 0x0400030D RID: 781
    private static readonly string FilePath = Path.Combine(AppBundleConfiguration.AppDataDirectory, "developer-flags.json");

    // Token: 0x0400030E RID: 782
    private static readonly Lock Gate = new Lock();

    // Token: 0x0400030F RID: 783
    [Nullable(2)]
    private static JsonObject data;
  }
}
