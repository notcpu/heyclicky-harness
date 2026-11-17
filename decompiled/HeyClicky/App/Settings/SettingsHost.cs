using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.App.Billing;
using HeyClicky.App.Services;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Input;

namespace HeyClicky.App.Settings
{
  // Token: 0x020000D5 RID: 213
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class SettingsHost
  {
    // Token: 0x17000183 RID: 387
    // (get) Token: 0x06000A10 RID: 2576 RVA: 0x00555940 File Offset: 0x00555940
    public AppServices Services { get; }

    // Token: 0x17000184 RID: 388
    // (get) Token: 0x06000A11 RID: 2577 RVA: 0x00555948 File Offset: 0x00555948
    public IAppActions Actions { get; }

    // Token: 0x17000185 RID: 389
    // (get) Token: 0x06000A12 RID: 2578 RVA: 0x00555950 File Offset: 0x00555950
    [Nullable(2)]
    public AppHost App
    {
      [NullableContext(2)]
      get;
    }

    // Token: 0x17000186 RID: 390
    // (get) Token: 0x06000A13 RID: 2579 RVA: 0x00555958 File Offset: 0x00555958
    public SettingsStore Store { get; }

    // Token: 0x17000187 RID: 391
    // (get) Token: 0x06000A14 RID: 2580 RVA: 0x00555960 File Offset: 0x00555960
    [Nullable(2)]
    public BillingUi Billing
    {
      [NullableContext(2)]
      get
      {
        return this.Services.Billing;
      }
    }

    // Token: 0x06000A15 RID: 2581 RVA: 0x00555970 File Offset: 0x00555970
    public SettingsHost(AppServices services, IAppActions actions)
    {
      this.Services = services;
      this.Actions = actions;
      this.App = actions as AppHost;
      AppHost app = this.App;
      SettingsStore settingsStore;
      if (app == null)
      {
        settingsStore = null;
      }
      else
      {
        RealStack real = app.Real;
        settingsStore = ((real != null) ? real.Core.Settings : null);
      }
      this.Store = settingsStore ?? SettingsHost.DemoStore.Value;
    }

    // Token: 0x17000188 RID: 392
    // (get) Token: 0x06000A16 RID: 2582 RVA: 0x005559D4 File Offset: 0x005559D4
    [Nullable(2)]
    public WorkerClient Worker
    {
      [NullableContext(2)]
      get
      {
        AppHost app = this.App;
        if (app == null)
        {
          return null;
        }
        RealStack real = app.Real;
        if (real == null)
        {
          return null;
        }
        return real.Core.Worker;
      }
    }

    // Token: 0x17000189 RID: 393
    // (get) Token: 0x06000A17 RID: 2583 RVA: 0x005559F8 File Offset: 0x005559F8
    public string WorkerBaseUrl
    {
      get
      {
        AppHost app = this.App;
        string text;
        if (app == null)
        {
          text = null;
        }
        else
        {
          RealStack real = app.Real;
          text = ((real != null) ? real.Core.Endpoints.WorkerBaseUrl.AbsoluteUri : null);
        }
        return text ?? "http://127.0.0.1:8787";
      }
    }

    // Token: 0x1700018A RID: 394
    // (get) Token: 0x06000A18 RID: 2584 RVA: 0x00555A30 File Offset: 0x00555A30
    [Nullable(2)]
    public AgentService Agents
    {
      [NullableContext(2)]
      get
      {
        AppHost app = this.App;
        RealStack realStack = ((app != null) ? app.Real : null);
        if (realStack == null || !realStack.IsComplete)
        {
          return null;
        }
        return realStack.Agents;
      }
    }

    // Token: 0x1700018B RID: 395
    // (get) Token: 0x06000A19 RID: 2585 RVA: 0x00555A64 File Offset: 0x00555A64
    [Nullable(2)]
    public ShortcutMonitor Monitor
    {
      [NullableContext(2)]
      get
      {
        AppHost app = this.App;
        RealStack realStack = ((app != null) ? app.Real : null);
        if (realStack == null || !realStack.IsComplete)
        {
          return null;
        }
        return realStack.Shortcuts;
      }
    }

    // Token: 0x1700018C RID: 396
    // (get) Token: 0x06000A1A RID: 2586 RVA: 0x00555A98 File Offset: 0x00555A98
    public bool IsSignedIn
    {
      get
      {
        return this.Services.Auth.IsSignedIn;
      }
    }

    // Token: 0x1700018D RID: 397
    // (get) Token: 0x06000A1B RID: 2587 RVA: 0x00555AAC File Offset: 0x00555AAC
    public AppSettings Current
    {
      get
      {
        return this.Store.Current;
      }
    }

    // Token: 0x06000A1C RID: 2588 RVA: 0x00555ABC File Offset: 0x00555ABC
    public T Read<[Nullable(2)] T>(Func<AppSettings, T> read)
    {
      return this.Store.Read<T>(read);
    }

    // Token: 0x06000A1D RID: 2589 RVA: 0x00555ACC File Offset: 0x00555ACC
    public void Set(string key, [Nullable(2)] object value, Action<AppSettings> change)
    {
      string text = "settings";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
      defaultInterpolatedStringHandler.AppendFormatted(key);
      defaultInterpolatedStringHandler.AppendLiteral(" = ");
      defaultInterpolatedStringHandler.AppendFormatted<object>(value ?? "null");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      this.Store.Update(change);
      SettingsHost.TrackSettingChanged(key, value);
    }

    // Token: 0x06000A1E RID: 2590 RVA: 0x00555B2C File Offset: 0x00555B2C
    public static void TrackSettingChanged(string key, [Nullable(2)] object value)
    {
      JsonObject jsonObject = new JsonObject(null);
      string text = key;
      string text2 = "home_space_settings";
      if (!(key == "show_in_taskbar"))
      {
        if (!(key == "agent_folder"))
        {
          if (!(key == "microphone"))
          {
            if (!(key == "dictation_dictionary"))
            {
              if (!(key == "shortcuts"))
              {
                if (key.StartsWith("shortcut_", StringComparison.Ordinal))
                {
                  int length = "shortcut_".Length;
                  string text3 = key.Substring(length, key.Length - length);
                  string text4;
                  if (!(text3 == "PushToTalk"))
                  {
                    if (!(text3 == "TextMode"))
                    {
                      if (!(text3 == "Dictation"))
                      {
                        if (!(text3 == "HandsFreeDictation"))
                        {
                          text4 = text3.ToLowerInvariant() + "_shortcut";
                        }
                        else
                        {
                          text4 = "hands_free_shortcut";
                        }
                      }
                      else
                      {
                        text4 = "dictation_shortcut";
                      }
                    }
                    else
                    {
                      text4 = "text_shortcut";
                    }
                  }
                  else
                  {
                    text4 = "voice_shortcut";
                  }
                  text = text4;
                  text2 = "shortcuts";
                  jsonObject["action"] = "saved";
                }
                else if (value != null)
                {
                  jsonObject["value"] = SettingsHost.<TrackSettingChanged>g__Json|30_0(value);
                }
              }
              else
              {
                text2 = "shortcuts";
                jsonObject["action"] = "reset_defaults";
              }
            }
            else
            {
              string text5 = value as string;
              if (text5 != null && text5.StartsWith("add", StringComparison.Ordinal))
              {
                jsonObject["action"] = "add";
                IEnumerable<char> enumerable = text5;
                Func<char, bool> func;
                if ((func = SettingsHost.<>O.<0>__IsAsciiDigit) == null)
                {
                  func = (SettingsHost.<>O.<0>__IsAsciiDigit = new Func<char, bool>(char.IsAsciiDigit));
                }
                int num;
                if (int.TryParse(new string(enumerable.Where<char>(func).ToArray<char>()), out num))
                {
                  jsonObject["word_count"] = num;
                }
              }
              else
              {
                jsonObject["action"] = "remove";
              }
            }
          }
        }
        else
        {
          JsonNode jsonNode = jsonObject;
          string text6 = "uses_custom_folder";
          string text7 = value as string;
          jsonNode[text6] = text7 != null && text7 != "default";
        }
      }
      else
      {
        text = "show_in_dock";
        jsonObject["value"] = SettingsHost.<TrackSettingChanged>g__Json|30_0(value);
      }
      jsonObject["setting"] = text;
      jsonObject["surface"] = text2;
      ClickyAnalytics.TrackProductEvent("setting_changed", null, null, jsonObject);
    }

    // Token: 0x1700018E RID: 398
    // (get) Token: 0x06000A1F RID: 2591 RVA: 0x00555DAC File Offset: 0x00555DAC
    public bool ShowsDeveloperSection
    {
      get
      {
        AgentService agents = this.Agents;
        return agents != null && agents.AllowsDeveloperModelSelection;
      }
    }

    // Token: 0x1700018F RID: 399
    // (get) Token: 0x06000A20 RID: 2592 RVA: 0x00555DC0 File Offset: 0x00555DC0
    public string BuildInfoLine
    {
      get
      {
        Version version = typeof(SettingsHost).Assembly.GetName().Version ?? new Version(0, 1, 0, 0);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 4);
        defaultInterpolatedStringHandler.AppendLiteral("HeyClicky ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(version.Major);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int>(version.Minor);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        defaultInterpolatedStringHandler.AppendFormatted<int>(version.Build);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(1, version.Revision));
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
    }

    // Token: 0x06000A21 RID: 2593 RVA: 0x00555E7C File Offset: 0x00555E7C
    [NullableContext(2)]
    public void OpenUrl(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
      {
        return;
      }
      Log.Info("settings", "open " + ((url.Length > 160) ? (url.Substring(0, 160) + "…") : url));
      if (Environment.GetEnvironmentVariable("HEYCLICKY_E2E_NO_BROWSER") == "1")
      {
        return;
      }
      this.Actions.OpenUrl(url);
    }

    // Token: 0x17000190 RID: 400
    // (get) Token: 0x06000A22 RID: 2594 RVA: 0x00555EF0 File Offset: 0x00555EF0
    public static AppConfigLinks Links
    {
      get
      {
        AppRemoteLinks links = AppRemoteConfig.Shared.Links;
        return new AppConfigLinks(links.FeatureRequest, links.CommunityReddit, links.CommunityWhatsApp, links.Changelog);
      }
    }

    // Token: 0x06000A23 RID: 2595 RVA: 0x00555F28 File Offset: 0x00555F28
    public Task RefreshLinksAsync()
    {
      WorkerClient worker = this.Worker;
      if (worker == null)
      {
        return Task.CompletedTask;
      }
      return AppRemoteConfig.Shared.RefreshAsync(worker, default(CancellationToken));
    }

    // Token: 0x06000A25 RID: 2597 RVA: 0x00555F78 File Offset: 0x00555F78
    [NullableContext(2)]
    [CompilerGenerated]
    internal static JsonNode <TrackSettingChanged>g__Json|30_0(object v)
    {
      JsonNode jsonNode;
      if (v != null)
      {
        if (v is bool)
        {
          bool flag = (bool)v;
          jsonNode = flag;
        }
        else if (v is int)
        {
          int num = (int)v;
          jsonNode = num;
        }
        else if (v is double)
        {
          double num2 = (double)v;
          jsonNode = num2;
        }
        else if (v is float)
        {
          float num3 = (float)v;
          jsonNode = (double)num3;
        }
        else
        {
          jsonNode = v.ToString();
        }
      }
      else
      {
        jsonNode = null;
      }
      return jsonNode;
    }

    // Token: 0x040003B4 RID: 948
    private static readonly Lazy<SettingsStore> DemoStore = new Lazy<SettingsStore>(() => new SettingsStore(AppPaths.Combine(new string[] { "demo", "core-settings.json" })));

    // Token: 0x0200053B RID: 1339
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040018E6 RID: 6374
      [Nullable(0)]
      public static Func<char, bool> <0>__IsAsciiDigit;
    }
  }
}
