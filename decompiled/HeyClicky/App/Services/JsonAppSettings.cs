using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.Win32;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E4 RID: 228
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class JsonAppSettings : IAppSettings
  {
    // Token: 0x14000029 RID: 41
    // (add) Token: 0x06000B1D RID: 2845 RVA: 0x0055A6EC File Offset: 0x0055A6EC
    // (remove) Token: 0x06000B1E RID: 2846 RVA: 0x0055A724 File Offset: 0x0055A724
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x06000B1F RID: 2847 RVA: 0x0055A75C File Offset: 0x0055A75C
    [NullableContext(2)]
    public JsonAppSettings(string directory = null)
    {
      string text = directory ?? AppPaths.Root;
      Directory.CreateDirectory(text);
      this.path = Path.Combine(text, "settings.json");
      try
      {
        this.data = (File.Exists(this.path) ? (JsonSerializer.Deserialize<JsonAppSettings.Data>(File.ReadAllText(this.path), JsonAppSettings.Ctx.Default.Data) ?? new JsonAppSettings.Data()) : new JsonAppSettings.Data());
      }
      catch (Exception ex)
      {
        Log.Warn("settings", "unreadable settings, using defaults: " + ex.Message);
        this.data = new JsonAppSettings.Data();
      }
    }

    // Token: 0x06000B20 RID: 2848 RVA: 0x0055A828 File Offset: 0x0055A828
    private void Save()
    {
      try
      {
        File.WriteAllText(this.path, JsonSerializer.Serialize<JsonAppSettings.Data>(this.data, JsonAppSettings.Ctx.Default.Data));
      }
      catch (Exception ex)
      {
        Log.Error("settings", "save failed", ex);
      }
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x170001E0 RID: 480
    // (get) Token: 0x06000B21 RID: 2849 RVA: 0x0055A890 File Offset: 0x0055A890
    // (set) Token: 0x06000B22 RID: 2850 RVA: 0x0055A8A0 File Offset: 0x0055A8A0
    public string CursorColorHex
    {
      get
      {
        return this.data.CursorColorHex;
      }
      set
      {
        this.data.CursorColorHex = value;
        this.Save();
      }
    }

    // Token: 0x170001E1 RID: 481
    // (get) Token: 0x06000B23 RID: 2851 RVA: 0x0055A8B4 File Offset: 0x0055A8B4
    // (set) Token: 0x06000B24 RID: 2852 RVA: 0x0055A8C4 File Offset: 0x0055A8C4
    public bool ScreenHighlightEnabled
    {
      get
      {
        return this.data.ScreenHighlightEnabled;
      }
      set
      {
        this.data.ScreenHighlightEnabled = value;
        this.Save();
      }
    }

    // Token: 0x170001E2 RID: 482
    // (get) Token: 0x06000B25 RID: 2853 RVA: 0x0055A8D8 File Offset: 0x0055A8D8
    // (set) Token: 0x06000B26 RID: 2854 RVA: 0x0055A8E8 File Offset: 0x0055A8E8
    public bool LibraryModeEnabled
    {
      get
      {
        return this.data.LibraryModeEnabled;
      }
      set
      {
        this.data.LibraryModeEnabled = value;
        this.Save();
      }
    }

    // Token: 0x170001E3 RID: 483
    // (get) Token: 0x06000B27 RID: 2855 RVA: 0x0055A8FC File Offset: 0x0055A8FC
    // (set) Token: 0x06000B28 RID: 2856 RVA: 0x0055A90C File Offset: 0x0055A90C
    public bool HasCompletedIntro
    {
      get
      {
        return this.data.HasCompletedIntro;
      }
      set
      {
        this.data.HasCompletedIntro = value;
        this.Save();
      }
    }

    // Token: 0x170001E4 RID: 484
    // (get) Token: 0x06000B29 RID: 2857 RVA: 0x0055A920 File Offset: 0x0055A920
    // (set) Token: 0x06000B2A RID: 2858 RVA: 0x0055A930 File Offset: 0x0055A930
    public bool HasCompletedOnboarding
    {
      get
      {
        return this.data.HasCompletedOnboarding;
      }
      set
      {
        this.data.HasCompletedOnboarding = value;
        this.Save();
      }
    }

    // Token: 0x170001E5 RID: 485
    // (get) Token: 0x06000B2B RID: 2859 RVA: 0x0055A944 File Offset: 0x0055A944
    // (set) Token: 0x06000B2C RID: 2860 RVA: 0x0055A954 File Offset: 0x0055A954
    public bool HasCompletedTutorial
    {
      get
      {
        return this.data.HasCompletedTutorial;
      }
      set
      {
        this.data.HasCompletedTutorial = value;
        this.Save();
      }
    }

    // Token: 0x170001E6 RID: 486
    // (get) Token: 0x06000B2D RID: 2861 RVA: 0x0055A968 File Offset: 0x0055A968
    // (set) Token: 0x06000B2E RID: 2862 RVA: 0x0055A978 File Offset: 0x0055A978
    [Nullable(2)]
    public string SquadPack
    {
      [NullableContext(2)]
      get
      {
        return this.data.SquadPack;
      }
      [NullableContext(2)]
      set
      {
        this.data.SquadPack = value;
        this.Save();
      }
    }

    // Token: 0x170001E7 RID: 487
    // (get) Token: 0x06000B2F RID: 2863 RVA: 0x0055A98C File Offset: 0x0055A98C
    // (set) Token: 0x06000B30 RID: 2864 RVA: 0x0055A99C File Offset: 0x0055A99C
    [Nullable(2)]
    public string DiscoveryChannel
    {
      [NullableContext(2)]
      get
      {
        return this.data.DiscoveryChannel;
      }
      [NullableContext(2)]
      set
      {
        this.data.DiscoveryChannel = value;
        this.Save();
      }
    }

    // Token: 0x170001E8 RID: 488
    // (get) Token: 0x06000B31 RID: 2865 RVA: 0x0055A9B0 File Offset: 0x0055A9B0
    // (set) Token: 0x06000B32 RID: 2866 RVA: 0x0055A9B8 File Offset: 0x0055A9B8
    public bool IsPaused
    {
      get
      {
        return this.paused;
      }
      set
      {
        this.paused = value;
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      }
    }

    // Token: 0x170001E9 RID: 489
    // (get) Token: 0x06000B33 RID: 2867 RVA: 0x0055A9D8 File Offset: 0x0055A9D8
    // (set) Token: 0x06000B34 RID: 2868 RVA: 0x0055A9E8 File Offset: 0x0055A9E8
    public bool CursorDocked
    {
      get
      {
        return this.data.CursorDocked;
      }
      set
      {
        this.data.CursorDocked = value;
        this.Save();
      }
    }

    // Token: 0x170001EA RID: 490
    // (get) Token: 0x06000B35 RID: 2869 RVA: 0x0055A9FC File Offset: 0x0055A9FC
    // (set) Token: 0x06000B36 RID: 2870 RVA: 0x0055AA0C File Offset: 0x0055AA0C
    public bool ShakeToDetachEnabled
    {
      get
      {
        return this.data.ShakeToDetachEnabled;
      }
      set
      {
        this.data.ShakeToDetachEnabled = value;
        this.Save();
      }
    }

    // Token: 0x170001EB RID: 491
    // (get) Token: 0x06000B37 RID: 2871 RVA: 0x0055AA20 File Offset: 0x0055AA20
    public string PushToTalkDisplay
    {
      get
      {
        return "Ctrl + Alt";
      }
    }

    // Token: 0x170001EC RID: 492
    // (get) Token: 0x06000B38 RID: 2872 RVA: 0x0055AA28 File Offset: 0x0055AA28
    // (set) Token: 0x06000B39 RID: 2873 RVA: 0x0055AA30 File Offset: 0x0055AA30
    public bool DictationAutoDetect { get; set; } = true;

    // Token: 0x170001ED RID: 493
    // (get) Token: 0x06000B3A RID: 2874 RVA: 0x0055AA3C File Offset: 0x0055AA3C
    // (set) Token: 0x06000B3B RID: 2875 RVA: 0x0055AA44 File Offset: 0x0055AA44
    public string DictationSpecificLanguageCode { get; set; } = "en";

    // Token: 0x170001EE RID: 494
    // (get) Token: 0x06000B3C RID: 2876 RVA: 0x0055AA50 File Offset: 0x0055AA50
    // (set) Token: 0x06000B3D RID: 2877 RVA: 0x0055AA58 File Offset: 0x0055AA58
    public string DictationCustomWordsText { get; set; } = "HeyClicky";

    // Token: 0x170001EF RID: 495
    // (get) Token: 0x06000B3E RID: 2878 RVA: 0x0055AA64 File Offset: 0x0055AA64
    public string TextModeDisplay
    {
      get
      {
        return "Ctrl";
      }
    }

    // Token: 0x170001F0 RID: 496
    // (get) Token: 0x06000B3F RID: 2879 RVA: 0x0055AA6C File Offset: 0x0055AA6C
    // (set) Token: 0x06000B40 RID: 2880 RVA: 0x0055AAC0 File Offset: 0x0055AAC0
    public bool LaunchAtLogin
    {
      get
      {
        bool flag;
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run"))
        {
          flag = ((registryKey != null) ? registryKey.GetValue("HeyClicky") : null) is string;
        }
        return flag;
      }
      set
      {
        using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run"))
        {
          if (value)
          {
            registryKey.SetValue("HeyClicky", "\"" + Environment.ProcessPath + "\" --background");
          }
          else
          {
            registryKey.DeleteValue("HeyClicky", false);
          }
          EventHandler changed = this.Changed;
          if (changed != null)
          {
            changed(this, EventArgs.Empty);
          }
        }
      }
    }

    // Token: 0x040003F9 RID: 1017
    private const string RunKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

    // Token: 0x040003FA RID: 1018
    private const string RunValue = "HeyClicky";

    // Token: 0x040003FB RID: 1019
    private readonly string path;

    // Token: 0x040003FC RID: 1020
    private readonly JsonAppSettings.Data data;

    // Token: 0x040003FD RID: 1021
    private bool paused;

    // Token: 0x02000567 RID: 1383
    [NullableContext(2)]
    [Nullable(0)]
    private sealed class Data
    {
      // Token: 0x17000732 RID: 1842
      // (get) Token: 0x06002DFB RID: 11771 RVA: 0x0065E420 File Offset: 0x0065E420
      // (set) Token: 0x06002DFC RID: 11772 RVA: 0x0065E428 File Offset: 0x0065E428
      [Nullable(1)]
      public string CursorColorHex
      {
        [NullableContext(1)]
        get;
        [NullableContext(1)]
        set;
      } = "#3380FF";

      // Token: 0x17000733 RID: 1843
      // (get) Token: 0x06002DFD RID: 11773 RVA: 0x0065E434 File Offset: 0x0065E434
      // (set) Token: 0x06002DFE RID: 11774 RVA: 0x0065E43C File Offset: 0x0065E43C
      public bool ScreenHighlightEnabled { get; set; } = true;

      // Token: 0x17000734 RID: 1844
      // (get) Token: 0x06002DFF RID: 11775 RVA: 0x0065E448 File Offset: 0x0065E448
      // (set) Token: 0x06002E00 RID: 11776 RVA: 0x0065E450 File Offset: 0x0065E450
      public bool LibraryModeEnabled { get; set; }

      // Token: 0x17000735 RID: 1845
      // (get) Token: 0x06002E01 RID: 11777 RVA: 0x0065E45C File Offset: 0x0065E45C
      // (set) Token: 0x06002E02 RID: 11778 RVA: 0x0065E464 File Offset: 0x0065E464
      public bool HasCompletedIntro { get; set; }

      // Token: 0x17000736 RID: 1846
      // (get) Token: 0x06002E03 RID: 11779 RVA: 0x0065E470 File Offset: 0x0065E470
      // (set) Token: 0x06002E04 RID: 11780 RVA: 0x0065E478 File Offset: 0x0065E478
      public bool HasCompletedOnboarding { get; set; }

      // Token: 0x17000737 RID: 1847
      // (get) Token: 0x06002E05 RID: 11781 RVA: 0x0065E484 File Offset: 0x0065E484
      // (set) Token: 0x06002E06 RID: 11782 RVA: 0x0065E48C File Offset: 0x0065E48C
      public bool HasCompletedTutorial { get; set; }

      // Token: 0x17000738 RID: 1848
      // (get) Token: 0x06002E07 RID: 11783 RVA: 0x0065E498 File Offset: 0x0065E498
      // (set) Token: 0x06002E08 RID: 11784 RVA: 0x0065E4A0 File Offset: 0x0065E4A0
      public string SquadPack { get; set; }

      // Token: 0x17000739 RID: 1849
      // (get) Token: 0x06002E09 RID: 11785 RVA: 0x0065E4AC File Offset: 0x0065E4AC
      // (set) Token: 0x06002E0A RID: 11786 RVA: 0x0065E4B4 File Offset: 0x0065E4B4
      public string DiscoveryChannel { get; set; }

      // Token: 0x1700073A RID: 1850
      // (get) Token: 0x06002E0B RID: 11787 RVA: 0x0065E4C0 File Offset: 0x0065E4C0
      // (set) Token: 0x06002E0C RID: 11788 RVA: 0x0065E4C8 File Offset: 0x0065E4C8
      public bool CursorDocked { get; set; }

      // Token: 0x1700073B RID: 1851
      // (get) Token: 0x06002E0D RID: 11789 RVA: 0x0065E4D4 File Offset: 0x0065E4D4
      // (set) Token: 0x06002E0E RID: 11790 RVA: 0x0065E4DC File Offset: 0x0065E4DC
      public bool ShakeToDetachEnabled { get; set; }
    }

    // Token: 0x02000568 RID: 1384
    [Nullable(0)]
    [JsonSerializable(typeof(JsonAppSettings.Data))]
    [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
    private sealed class Ctx : JsonSerializerContext, IJsonTypeInfoResolver
    {
      // Token: 0x1700073C RID: 1852
      // (get) Token: 0x06002E10 RID: 11792 RVA: 0x0065E504 File Offset: 0x0065E504
      [Nullable(0)]
      public JsonTypeInfo<bool> Boolean
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<bool> jsonTypeInfo;
          if ((jsonTypeInfo = this._Boolean) == null)
          {
            jsonTypeInfo = (this._Boolean = (JsonTypeInfo<bool>)base.Options.GetTypeInfo(typeof(bool)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002E11 RID: 11793 RVA: 0x0065E540 File Offset: 0x0065E540
      private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
      {
        JsonTypeInfo<bool> jsonTypeInfo;
        if (!JsonAppSettings.Ctx.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x1700073D RID: 1853
      // (get) Token: 0x06002E12 RID: 11794 RVA: 0x0065E56C File Offset: 0x0065E56C
      [Nullable(0)]
      public JsonTypeInfo<JsonAppSettings.Data> Data
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<JsonAppSettings.Data> jsonTypeInfo;
          if ((jsonTypeInfo = this._Data) == null)
          {
            jsonTypeInfo = (this._Data = (JsonTypeInfo<JsonAppSettings.Data>)base.Options.GetTypeInfo(typeof(JsonAppSettings.Data)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002E13 RID: 11795 RVA: 0x0065E5A8 File Offset: 0x0065E5A8
      private JsonTypeInfo<JsonAppSettings.Data> Create_Data(JsonSerializerOptions options)
      {
        JsonTypeInfo<JsonAppSettings.Data> jsonTypeInfo;
        if (!JsonAppSettings.Ctx.TryGetTypeInfoForRuntimeCustomConverter<JsonAppSettings.Data>(options, out jsonTypeInfo))
        {
          JsonObjectInfoValues<JsonAppSettings.Data> jsonObjectInfoValues = new JsonObjectInfoValues<JsonAppSettings.Data>();
          jsonObjectInfoValues.ObjectCreator = () => new JsonAppSettings.Data();
          jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
          jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => JsonAppSettings.Ctx.DataPropInit(options);
          jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
          jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
          jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, JsonAppSettings.Data>(this.DataSerializeHandler);
          JsonObjectInfoValues<JsonAppSettings.Data> jsonObjectInfoValues2 = jsonObjectInfoValues;
          jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<JsonAppSettings.Data>(options, jsonObjectInfoValues2);
          jsonTypeInfo.NumberHandling = null;
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x06002E14 RID: 11796 RVA: 0x0065E67C File Offset: 0x0065E67C
      private static JsonPropertyInfo[] DataPropInit(JsonSerializerOptions options)
      {
        JsonPropertyInfo[] array = new JsonPropertyInfo[10];
        JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues.IsProperty = true;
        jsonPropertyInfoValues.IsPublic = true;
        jsonPropertyInfoValues.IsVirtual = false;
        jsonPropertyInfoValues.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues.Converter = null;
        jsonPropertyInfoValues.Getter = (object obj) => ((JsonAppSettings.Data)obj).CursorColorHex;
        jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          ((JsonAppSettings.Data)obj).CursorColorHex = value;
        };
        jsonPropertyInfoValues.IgnoreCondition = null;
        jsonPropertyInfoValues.HasJsonInclude = false;
        jsonPropertyInfoValues.IsExtensionData = false;
        jsonPropertyInfoValues.NumberHandling = null;
        jsonPropertyInfoValues.PropertyName = "CursorColorHex";
        jsonPropertyInfoValues.JsonPropertyName = null;
        jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("CursorColorHex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
        array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
        array[0].IsGetNullable = false;
        array[0].IsSetNullable = false;
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues3.IsProperty = true;
        jsonPropertyInfoValues3.IsPublic = true;
        jsonPropertyInfoValues3.IsVirtual = false;
        jsonPropertyInfoValues3.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues3.Converter = null;
        jsonPropertyInfoValues3.Getter = (object obj) => ((JsonAppSettings.Data)obj).ScreenHighlightEnabled;
        jsonPropertyInfoValues3.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).ScreenHighlightEnabled = value;
        };
        jsonPropertyInfoValues3.IgnoreCondition = null;
        jsonPropertyInfoValues3.HasJsonInclude = false;
        jsonPropertyInfoValues3.IsExtensionData = false;
        jsonPropertyInfoValues3.NumberHandling = null;
        jsonPropertyInfoValues3.PropertyName = "ScreenHighlightEnabled";
        jsonPropertyInfoValues3.JsonPropertyName = null;
        jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("ScreenHighlightEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
        array[1] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues4);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues5.IsProperty = true;
        jsonPropertyInfoValues5.IsPublic = true;
        jsonPropertyInfoValues5.IsVirtual = false;
        jsonPropertyInfoValues5.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues5.Converter = null;
        jsonPropertyInfoValues5.Getter = (object obj) => ((JsonAppSettings.Data)obj).LibraryModeEnabled;
        jsonPropertyInfoValues5.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).LibraryModeEnabled = value;
        };
        jsonPropertyInfoValues5.IgnoreCondition = null;
        jsonPropertyInfoValues5.HasJsonInclude = false;
        jsonPropertyInfoValues5.IsExtensionData = false;
        jsonPropertyInfoValues5.NumberHandling = null;
        jsonPropertyInfoValues5.PropertyName = "LibraryModeEnabled";
        jsonPropertyInfoValues5.JsonPropertyName = null;
        jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("LibraryModeEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
        array[2] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues6);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues7.IsProperty = true;
        jsonPropertyInfoValues7.IsPublic = true;
        jsonPropertyInfoValues7.IsVirtual = false;
        jsonPropertyInfoValues7.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues7.Converter = null;
        jsonPropertyInfoValues7.Getter = (object obj) => ((JsonAppSettings.Data)obj).HasCompletedIntro;
        jsonPropertyInfoValues7.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).HasCompletedIntro = value;
        };
        jsonPropertyInfoValues7.IgnoreCondition = null;
        jsonPropertyInfoValues7.HasJsonInclude = false;
        jsonPropertyInfoValues7.IsExtensionData = false;
        jsonPropertyInfoValues7.NumberHandling = null;
        jsonPropertyInfoValues7.PropertyName = "HasCompletedIntro";
        jsonPropertyInfoValues7.JsonPropertyName = null;
        jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("HasCompletedIntro", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
        array[3] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues8);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues9.IsProperty = true;
        jsonPropertyInfoValues9.IsPublic = true;
        jsonPropertyInfoValues9.IsVirtual = false;
        jsonPropertyInfoValues9.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues9.Converter = null;
        jsonPropertyInfoValues9.Getter = (object obj) => ((JsonAppSettings.Data)obj).HasCompletedOnboarding;
        jsonPropertyInfoValues9.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).HasCompletedOnboarding = value;
        };
        jsonPropertyInfoValues9.IgnoreCondition = null;
        jsonPropertyInfoValues9.HasJsonInclude = false;
        jsonPropertyInfoValues9.IsExtensionData = false;
        jsonPropertyInfoValues9.NumberHandling = null;
        jsonPropertyInfoValues9.PropertyName = "HasCompletedOnboarding";
        jsonPropertyInfoValues9.JsonPropertyName = null;
        jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("HasCompletedOnboarding", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
        array[4] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues10);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues11.IsProperty = true;
        jsonPropertyInfoValues11.IsPublic = true;
        jsonPropertyInfoValues11.IsVirtual = false;
        jsonPropertyInfoValues11.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues11.Converter = null;
        jsonPropertyInfoValues11.Getter = (object obj) => ((JsonAppSettings.Data)obj).HasCompletedTutorial;
        jsonPropertyInfoValues11.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).HasCompletedTutorial = value;
        };
        jsonPropertyInfoValues11.IgnoreCondition = null;
        jsonPropertyInfoValues11.HasJsonInclude = false;
        jsonPropertyInfoValues11.IsExtensionData = false;
        jsonPropertyInfoValues11.NumberHandling = null;
        jsonPropertyInfoValues11.PropertyName = "HasCompletedTutorial";
        jsonPropertyInfoValues11.JsonPropertyName = null;
        jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("HasCompletedTutorial", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
        array[5] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues12);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues13.IsProperty = true;
        jsonPropertyInfoValues13.IsPublic = true;
        jsonPropertyInfoValues13.IsVirtual = false;
        jsonPropertyInfoValues13.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues13.Converter = null;
        jsonPropertyInfoValues13.Getter = (object obj) => ((JsonAppSettings.Data)obj).SquadPack;
        jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          ((JsonAppSettings.Data)obj).SquadPack = value;
        };
        jsonPropertyInfoValues13.IgnoreCondition = null;
        jsonPropertyInfoValues13.HasJsonInclude = false;
        jsonPropertyInfoValues13.IsExtensionData = false;
        jsonPropertyInfoValues13.NumberHandling = null;
        jsonPropertyInfoValues13.PropertyName = "SquadPack";
        jsonPropertyInfoValues13.JsonPropertyName = null;
        jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("SquadPack", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
        array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
        jsonPropertyInfoValues15.IsProperty = true;
        jsonPropertyInfoValues15.IsPublic = true;
        jsonPropertyInfoValues15.IsVirtual = false;
        jsonPropertyInfoValues15.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues15.Converter = null;
        jsonPropertyInfoValues15.Getter = (object obj) => ((JsonAppSettings.Data)obj).DiscoveryChannel;
        jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
        {
          ((JsonAppSettings.Data)obj).DiscoveryChannel = value;
        };
        jsonPropertyInfoValues15.IgnoreCondition = null;
        jsonPropertyInfoValues15.HasJsonInclude = false;
        jsonPropertyInfoValues15.IsExtensionData = false;
        jsonPropertyInfoValues15.NumberHandling = null;
        jsonPropertyInfoValues15.PropertyName = "DiscoveryChannel";
        jsonPropertyInfoValues15.JsonPropertyName = null;
        jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("DiscoveryChannel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
        array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues17.IsProperty = true;
        jsonPropertyInfoValues17.IsPublic = true;
        jsonPropertyInfoValues17.IsVirtual = false;
        jsonPropertyInfoValues17.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues17.Converter = null;
        jsonPropertyInfoValues17.Getter = (object obj) => ((JsonAppSettings.Data)obj).CursorDocked;
        jsonPropertyInfoValues17.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).CursorDocked = value;
        };
        jsonPropertyInfoValues17.IgnoreCondition = null;
        jsonPropertyInfoValues17.HasJsonInclude = false;
        jsonPropertyInfoValues17.IsExtensionData = false;
        jsonPropertyInfoValues17.NumberHandling = null;
        jsonPropertyInfoValues17.PropertyName = "CursorDocked";
        jsonPropertyInfoValues17.JsonPropertyName = null;
        jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("CursorDocked", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
        array[8] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues18);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<bool>();
        jsonPropertyInfoValues19.IsProperty = true;
        jsonPropertyInfoValues19.IsPublic = true;
        jsonPropertyInfoValues19.IsVirtual = false;
        jsonPropertyInfoValues19.DeclaringType = typeof(JsonAppSettings.Data);
        jsonPropertyInfoValues19.Converter = null;
        jsonPropertyInfoValues19.Getter = (object obj) => ((JsonAppSettings.Data)obj).ShakeToDetachEnabled;
        jsonPropertyInfoValues19.Setter = delegate(object obj, bool value)
        {
          ((JsonAppSettings.Data)obj).ShakeToDetachEnabled = value;
        };
        jsonPropertyInfoValues19.IgnoreCondition = null;
        jsonPropertyInfoValues19.HasJsonInclude = false;
        jsonPropertyInfoValues19.IsExtensionData = false;
        jsonPropertyInfoValues19.NumberHandling = null;
        jsonPropertyInfoValues19.PropertyName = "ShakeToDetachEnabled";
        jsonPropertyInfoValues19.JsonPropertyName = null;
        jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(JsonAppSettings.Data).GetProperty("ShakeToDetachEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
        JsonPropertyInfoValues<bool> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
        array[9] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues20);
        return array;
      }

      // Token: 0x06002E15 RID: 11797 RVA: 0x0065EFE0 File Offset: 0x0065EFE0
      private void DataSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] JsonAppSettings.Data value)
      {
        if (value == null)
        {
          writer.WriteNullValue();
          return;
        }
        writer.WriteStartObject();
        writer.WriteString(JsonAppSettings.Ctx.PropName_CursorColorHex, value.CursorColorHex);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_ScreenHighlightEnabled, value.ScreenHighlightEnabled);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_LibraryModeEnabled, value.LibraryModeEnabled);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_HasCompletedIntro, value.HasCompletedIntro);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_HasCompletedOnboarding, value.HasCompletedOnboarding);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_HasCompletedTutorial, value.HasCompletedTutorial);
        writer.WriteString(JsonAppSettings.Ctx.PropName_SquadPack, value.SquadPack);
        writer.WriteString(JsonAppSettings.Ctx.PropName_DiscoveryChannel, value.DiscoveryChannel);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_CursorDocked, value.CursorDocked);
        writer.WriteBoolean(JsonAppSettings.Ctx.PropName_ShakeToDetachEnabled, value.ShakeToDetachEnabled);
        writer.WriteEndObject();
      }

      // Token: 0x1700073E RID: 1854
      // (get) Token: 0x06002E16 RID: 11798 RVA: 0x0065F0B0 File Offset: 0x0065F0B0
      [Nullable(0)]
      public JsonTypeInfo<string> String
      {
        [NullableContext(0)]
        get
        {
          JsonTypeInfo<string> jsonTypeInfo;
          if ((jsonTypeInfo = this._String) == null)
          {
            jsonTypeInfo = (this._String = (JsonTypeInfo<string>)base.Options.GetTypeInfo(typeof(string)));
          }
          return jsonTypeInfo;
        }
      }

      // Token: 0x06002E17 RID: 11799 RVA: 0x0065F0EC File Offset: 0x0065F0EC
      private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
      {
        JsonTypeInfo<string> jsonTypeInfo;
        if (!JsonAppSettings.Ctx.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
        }
        jsonTypeInfo.OriginatingResolver = this;
        return jsonTypeInfo;
      }

      // Token: 0x1700073F RID: 1855
      // (get) Token: 0x06002E18 RID: 11800 RVA: 0x0065F118 File Offset: 0x0065F118
      public static JsonAppSettings.Ctx Default { get; } = new JsonAppSettings.Ctx(new JsonSerializerOptions(JsonAppSettings.Ctx.s_defaultOptions));

      // Token: 0x17000740 RID: 1856
      // (get) Token: 0x06002E19 RID: 11801 RVA: 0x0065F120 File Offset: 0x0065F120
      [Nullable(2)]
      protected override JsonSerializerOptions GeneratedSerializerOptions
      {
        [NullableContext(2)]
        get;
      } = JsonAppSettings.Ctx.s_defaultOptions;

      // Token: 0x06002E1A RID: 11802 RVA: 0x0065F128 File Offset: 0x0065F128
      public Ctx()
        : base(null)
      {
      }

      // Token: 0x06002E1B RID: 11803 RVA: 0x0065F13C File Offset: 0x0065F13C
      public Ctx(JsonSerializerOptions options)
        : base(options)
      {
      }

      // Token: 0x06002E1C RID: 11804 RVA: 0x0065F150 File Offset: 0x0065F150
      private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
      {
        JsonConverter runtimeConverterForType = JsonAppSettings.Ctx.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
        if (runtimeConverterForType != null)
        {
          jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
          return true;
        }
        jsonTypeInfo = null;
        return false;
      }

      // Token: 0x06002E1D RID: 11805 RVA: 0x0065F180 File Offset: 0x0065F180
      [return: Nullable(2)]
      private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
      {
        for (int i = 0; i < options.Converters.Count; i++)
        {
          JsonConverter jsonConverter = options.Converters[i];
          if (jsonConverter != null && jsonConverter.CanConvert(type))
          {
            return JsonAppSettings.Ctx.ExpandConverter(type, jsonConverter, options, false);
          }
        }
        return null;
      }

      // Token: 0x06002E1E RID: 11806 RVA: 0x0065F1C8 File Offset: 0x0065F1C8
      private static JsonConverter ExpandConverter(Type type, JsonConverter converter, JsonSerializerOptions options, bool validateCanConvert = true)
      {
        if (validateCanConvert && !converter.CanConvert(type))
        {
          throw new InvalidOperationException(string.Format("The converter '{0}' is not compatible with the type '{1}'.", converter.GetType(), type));
        }
        JsonConverterFactory jsonConverterFactory = converter as JsonConverterFactory;
        if (jsonConverterFactory != null)
        {
          converter = jsonConverterFactory.CreateConverter(type, options);
          if (converter == null || converter is JsonConverterFactory)
          {
            throw new InvalidOperationException(string.Format("The converter '{0}' cannot return null or a JsonConverterFactory instance.", jsonConverterFactory.GetType()));
          }
        }
        return converter;
      }

      // Token: 0x06002E1F RID: 11807 RVA: 0x0065F230 File Offset: 0x0065F230
      [return: Nullable(2)]
      public override JsonTypeInfo GetTypeInfo(Type type)
      {
        JsonTypeInfo jsonTypeInfo;
        base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
        return jsonTypeInfo;
      }

      // Token: 0x06002E20 RID: 11808 RVA: 0x0065F250 File Offset: 0x0065F250
      [return: Nullable(2)]
      JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
      {
        if (type == typeof(bool))
        {
          return this.Create_Boolean(options);
        }
        if (type == typeof(JsonAppSettings.Data))
        {
          return this.Create_Data(options);
        }
        if (type == typeof(string))
        {
          return this.Create_String(options);
        }
        return null;
      }

      // Token: 0x04001974 RID: 6516
      [Nullable(2)]
      private JsonTypeInfo<bool> _Boolean;

      // Token: 0x04001975 RID: 6517
      [Nullable(new byte[] { 2, 1 })]
      private JsonTypeInfo<JsonAppSettings.Data> _Data;

      // Token: 0x04001976 RID: 6518
      [Nullable(new byte[] { 2, 1 })]
      private JsonTypeInfo<string> _String;

      // Token: 0x04001977 RID: 6519
      private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions();

      // Token: 0x04001978 RID: 6520
      private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

      // Token: 0x0400197B RID: 6523
      private static readonly JsonEncodedText PropName_CursorColorHex = JsonEncodedText.Encode("CursorColorHex", null);

      // Token: 0x0400197C RID: 6524
      private static readonly JsonEncodedText PropName_ScreenHighlightEnabled = JsonEncodedText.Encode("ScreenHighlightEnabled", null);

      // Token: 0x0400197D RID: 6525
      private static readonly JsonEncodedText PropName_LibraryModeEnabled = JsonEncodedText.Encode("LibraryModeEnabled", null);

      // Token: 0x0400197E RID: 6526
      private static readonly JsonEncodedText PropName_HasCompletedIntro = JsonEncodedText.Encode("HasCompletedIntro", null);

      // Token: 0x0400197F RID: 6527
      private static readonly JsonEncodedText PropName_HasCompletedOnboarding = JsonEncodedText.Encode("HasCompletedOnboarding", null);

      // Token: 0x04001980 RID: 6528
      private static readonly JsonEncodedText PropName_HasCompletedTutorial = JsonEncodedText.Encode("HasCompletedTutorial", null);

      // Token: 0x04001981 RID: 6529
      private static readonly JsonEncodedText PropName_SquadPack = JsonEncodedText.Encode("SquadPack", null);

      // Token: 0x04001982 RID: 6530
      private static readonly JsonEncodedText PropName_DiscoveryChannel = JsonEncodedText.Encode("DiscoveryChannel", null);

      // Token: 0x04001983 RID: 6531
      private static readonly JsonEncodedText PropName_CursorDocked = JsonEncodedText.Encode("CursorDocked", null);

      // Token: 0x04001984 RID: 6532
      private static readonly JsonEncodedText PropName_ShakeToDetachEnabled = JsonEncodedText.Encode("ShakeToDetachEnabled", null);
    }
  }
}
