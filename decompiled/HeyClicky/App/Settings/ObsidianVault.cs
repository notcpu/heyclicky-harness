using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Agents.Backend;
using HeyClicky.App.Interop;
using HeyClicky.Core.Settings;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000CE RID: 206
  [NullableContext(2)]
  [Nullable(0)]
  public static class ObsidianVault
  {
    // Token: 0x14000022 RID: 34
    // (add) Token: 0x060008ED RID: 2285 RVA: 0x005420C8 File Offset: 0x005420C8
    // (remove) Token: 0x060008EE RID: 2286 RVA: 0x005420FC File Offset: 0x005420FC
    public static event Action Changed;

    // Token: 0x060008EF RID: 2287 RVA: 0x00542130 File Offset: 0x00542130
    [NullableContext(1)]
    public static void Initialize(SettingsStore settings)
    {
      ObsidianVault.store = settings;
    }

    // Token: 0x17000171 RID: 369
    // (get) Token: 0x060008F0 RID: 2288 RVA: 0x00542138 File Offset: 0x00542138
    public static string PersistedPath
    {
      get
      {
        SettingsStore settingsStore = ObsidianVault.store;
        string text = ((settingsStore != null) ? settingsStore.Current.ObsidianVaultPath : null);
        if (text == null || text.Length <= 0)
        {
          return null;
        }
        return text;
      }
    }

    // Token: 0x17000172 RID: 370
    // (get) Token: 0x060008F1 RID: 2289 RVA: 0x0054216C File Offset: 0x0054216C
    public static string ConfiguredPath
    {
      get
      {
        string persistedPath = ObsidianVault.PersistedPath;
        if (persistedPath == null || !Directory.Exists(persistedPath))
        {
          return null;
        }
        return persistedPath;
      }
    }

    // Token: 0x060008F2 RID: 2290 RVA: 0x00542190 File Offset: 0x00542190
    [NullableContext(1)]
    public static AgentIntegration Row()
    {
      string configuredPath = ObsidianVault.ConfiguredPath;
      string text = ((configuredPath != null) ? "connected" : ((ObsidianVault.PersistedPath != null) ? "needs_reconnect" : "not_connected"));
      string text2 = ((configuredPath == null) ? null : Path.GetFileName(configuredPath.TrimEnd(new char[] { '\\', '/' })));
      string text3 = ((text2 != null) ? ("Local Markdown vault \"" + text2 + "\". HeyClicky adds the Obsidian skill to spawned agents.") : "Use a local Markdown vault through HeyClicky's Obsidian skill. No Composio account required.");
      return new AgentIntegration("obsidian", "Obsidian", text3, text, (configuredPath != null) ? 1 : 0, 1, new <>z__ReadOnlySingleElementList<AgentIntegrationToolkit>(new AgentIntegrationToolkit("obsidian", "Obsidian", null, configuredPath != null)), Array.Empty<AgentIntegrationAccount>());
    }

    // Token: 0x060008F3 RID: 2291 RVA: 0x00542234 File Offset: 0x00542234
    public static bool Configure(IntPtr owner)
    {
      if (ObsidianVault.store == null)
      {
        return false;
      }
      string text = ObsidianVault.ConfiguredPath ?? ObsidianVault.DefaultPickerFolder();
      string text2 = FolderDialog.Pick(owner, "Select Obsidian Vault", "Configure", text, true);
      if (text2 == null)
      {
        return false;
      }
      string path = Path.GetFullPath(text2);
      ObsidianVault.store.Update(delegate(AppSettings s)
      {
        s.ObsidianVaultPath = path;
      });
      Log.Info("settings", "obsidian vault configured (" + Path.GetFileName(path.TrimEnd('\\')) + ")");
      Action changed = ObsidianVault.Changed;
      if (changed != null)
      {
        changed();
      }
      return true;
    }

    // Token: 0x060008F4 RID: 2292 RVA: 0x005422D8 File Offset: 0x005422D8
    public static void Unset()
    {
      if (ObsidianVault.store == null)
      {
        return;
      }
      ObsidianVault.store.Update(delegate(AppSettings s)
      {
        s.ObsidianVaultPath = null;
      });
      Log.Info("settings", "obsidian vault removed");
      Action changed = ObsidianVault.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x060008F5 RID: 2293 RVA: 0x00542334 File Offset: 0x00542334
    public static string DefaultPickerFolder()
    {
      try
      {
        string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "obsidian", "obsidian.json");
        if (File.Exists(text))
        {
          JsonNode jsonNode = JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions));
          JsonObject jsonObject = ((jsonNode != null) ? jsonNode["vaults"] : null) as JsonObject;
          if (jsonObject != null)
          {
            ValueTuple<string, bool, long> valueTuple = (from v in jsonObject.Select<KeyValuePair<string, JsonNode>, JsonObject>((KeyValuePair<string, JsonNode> v) => v.Value as JsonObject).OfType<JsonObject>().Select<JsonObject, ValueTuple<string, bool, long>>(delegate(JsonObject v)
              {
                JsonNode jsonNode2 = v["path"];
                string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
                JsonValue jsonValue = v["open"] as JsonValue;
                bool flag2;
                bool flag = jsonValue != null && jsonValue.TryGetValue<bool>(out flag2) && flag2;
                JsonValue jsonValue2 = v["ts"] as JsonValue;
                long num;
                return new ValueTuple<string, bool, long>(text2, flag, (jsonValue2 != null && jsonValue2.TryGetValue<long>(out num)) ? num : 0L);
              })
              where v.Item1 != null && Directory.Exists(v.Item1)
              orderby v.Item2 descending, v.Item3 descending
              select v).FirstOrDefault<ValueTuple<string, bool, long>>();
            if (valueTuple.Item1 != null)
            {
              return valueTuple.Item1;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "obsidian.json unreadable: " + ex.Message);
      }
      return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    }

    // Token: 0x04000311 RID: 785
    private static SettingsStore store;
  }
}
