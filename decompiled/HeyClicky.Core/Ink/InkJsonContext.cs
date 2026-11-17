using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HeyClicky.Core.Ink
{
  // Token: 0x0200016E RID: 366
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
  [JsonSerializable(typeof(InkNoteSnapshot))]
  [JsonSerializable(typeof(InkActionsSnapshot))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  public class InkJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x17000517 RID: 1303
    // (get) Token: 0x06001641 RID: 5697 RVA: 0x003BC644 File Offset: 0x003BC644
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

    // Token: 0x06001642 RID: 5698 RVA: 0x003BC680 File Offset: 0x003BC680
    private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000518 RID: 1304
    // (get) Token: 0x06001643 RID: 5699 RVA: 0x003BC6AC File Offset: 0x003BC6AC
    [Nullable(0)]
    public JsonTypeInfo<bool?> NullableBoolean
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<bool?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableBoolean) == null)
        {
          jsonTypeInfo = (this._NullableBoolean = (JsonTypeInfo<bool?>)base.Options.GetTypeInfo(typeof(bool?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001644 RID: 5700 RVA: 0x003BC6E8 File Offset: 0x003BC6E8
    private JsonTypeInfo<bool?> Create_NullableBoolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool?> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<bool>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000519 RID: 1305
    // (get) Token: 0x06001645 RID: 5701 RVA: 0x003BC718 File Offset: 0x003BC718
    [Nullable(0)]
    public JsonTypeInfo<double> Double
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<double> jsonTypeInfo;
        if ((jsonTypeInfo = this._Double) == null)
        {
          jsonTypeInfo = (this._Double = (JsonTypeInfo<double>)base.Options.GetTypeInfo(typeof(double)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001646 RID: 5702 RVA: 0x003BC754 File Offset: 0x003BC754
    private JsonTypeInfo<double> Create_Double(JsonSerializerOptions options)
    {
      JsonTypeInfo<double> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<double>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double>(options, JsonMetadataServices.DoubleConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700051A RID: 1306
    // (get) Token: 0x06001647 RID: 5703 RVA: 0x003BC780 File Offset: 0x003BC780
    [Nullable(0)]
    public JsonTypeInfo<InkActionsSnapshot> InkActionsSnapshot
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<InkActionsSnapshot> jsonTypeInfo;
        if ((jsonTypeInfo = this._InkActionsSnapshot) == null)
        {
          jsonTypeInfo = (this._InkActionsSnapshot = (JsonTypeInfo<InkActionsSnapshot>)base.Options.GetTypeInfo(typeof(InkActionsSnapshot)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001648 RID: 5704 RVA: 0x003BC7BC File Offset: 0x003BC7BC
    private JsonTypeInfo<InkActionsSnapshot> Create_InkActionsSnapshot(JsonSerializerOptions options)
    {
      JsonTypeInfo<InkActionsSnapshot> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<InkActionsSnapshot>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<InkActionsSnapshot> jsonObjectInfoValues = new JsonObjectInfoValues<InkActionsSnapshot>();
        jsonObjectInfoValues.ObjectCreator = () => new InkActionsSnapshot();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => InkJsonContext.InkActionsSnapshotPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InkActionsSnapshot).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, InkActionsSnapshot>(this.InkActionsSnapshotSerializeHandler);
        JsonObjectInfoValues<InkActionsSnapshot> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<InkActionsSnapshot>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06001649 RID: 5705 RVA: 0x003BC890 File Offset: 0x003BC890
    private static JsonPropertyInfo[] InkActionsSnapshotPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<int> jsonPropertyInfoValues = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(InkActionsSnapshot);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((InkActionsSnapshot)obj).Version;
      jsonPropertyInfoValues.Setter = delegate(object obj, int value)
      {
        ((InkActionsSnapshot)obj).Version = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Version";
      jsonPropertyInfoValues.JsonPropertyName = "version";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InkActionsSnapshot).GetProperty("Version", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<Dictionary<string, List<InkSentActionFile>>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<Dictionary<string, List<InkSentActionFile>>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(InkActionsSnapshot);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((InkActionsSnapshot)obj).ActionsByThingId;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1, 1, 1 })] Dictionary<string, List<InkSentActionFile>> value)
      {
        ((InkActionsSnapshot)obj).ActionsByThingId = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ActionsByThingId";
      jsonPropertyInfoValues3.JsonPropertyName = "actionsByThingID";
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(InkActionsSnapshot).GetProperty("ActionsByThingId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Dictionary<string, List<InkSentActionFile>>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Dictionary<string, List<InkSentActionFile>>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<Dictionary<string, List<InkSentActionFile>>>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x0600164A RID: 5706 RVA: 0x003BCA80 File Offset: 0x003BCA80
    private void InkActionsSnapshotSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] InkActionsSnapshot value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteNumber(InkJsonContext.PropName_version, value.Version);
      Dictionary<string, List<InkSentActionFile>> actionsByThingId = value.ActionsByThingId;
      if (actionsByThingId != null)
      {
        writer.WritePropertyName(InkJsonContext.PropName_actionsByThingID);
        this.DictionaryStringListInkSentActionFileSerializeHandler(writer, actionsByThingId);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700051B RID: 1307
    // (get) Token: 0x0600164B RID: 5707 RVA: 0x003BCAD4 File Offset: 0x003BCAD4
    [Nullable(0)]
    public JsonTypeInfo<InkNoteSnapshot> InkNoteSnapshot
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<InkNoteSnapshot> jsonTypeInfo;
        if ((jsonTypeInfo = this._InkNoteSnapshot) == null)
        {
          jsonTypeInfo = (this._InkNoteSnapshot = (JsonTypeInfo<InkNoteSnapshot>)base.Options.GetTypeInfo(typeof(InkNoteSnapshot)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600164C RID: 5708 RVA: 0x003BCB10 File Offset: 0x003BCB10
    private JsonTypeInfo<InkNoteSnapshot> Create_InkNoteSnapshot(JsonSerializerOptions options)
    {
      JsonTypeInfo<InkNoteSnapshot> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<InkNoteSnapshot>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<InkNoteSnapshot> jsonObjectInfoValues = new JsonObjectInfoValues<InkNoteSnapshot>();
        jsonObjectInfoValues.ObjectCreator = () => new InkNoteSnapshot();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => InkJsonContext.InkNoteSnapshotPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InkNoteSnapshot).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, InkNoteSnapshot>(this.InkNoteSnapshotSerializeHandler);
        JsonObjectInfoValues<InkNoteSnapshot> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<InkNoteSnapshot>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600164D RID: 5709 RVA: 0x003BCBE4 File Offset: 0x003BCBE4
    private static JsonPropertyInfo[] InkNoteSnapshotPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<int> jsonPropertyInfoValues = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(InkNoteSnapshot);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((InkNoteSnapshot)obj).Version;
      jsonPropertyInfoValues.Setter = delegate(object obj, int value)
      {
        ((InkNoteSnapshot)obj).Version = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Version";
      jsonPropertyInfoValues.JsonPropertyName = "version";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InkNoteSnapshot).GetProperty("Version", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<List<InkNoteVersionFile>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<List<InkNoteVersionFile>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(InkNoteSnapshot);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((InkNoteSnapshot)obj).Versions;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<InkNoteVersionFile> value)
      {
        ((InkNoteSnapshot)obj).Versions = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Versions";
      jsonPropertyInfoValues3.JsonPropertyName = "versions";
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(InkNoteSnapshot).GetProperty("Versions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<InkNoteVersionFile>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<InkNoteVersionFile>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<List<InkNoteVersionFile>>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x0600164E RID: 5710 RVA: 0x003BCDD4 File Offset: 0x003BCDD4
    private void InkNoteSnapshotSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] InkNoteSnapshot value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteNumber(InkJsonContext.PropName_version, value.Version);
      List<InkNoteVersionFile> versions = value.Versions;
      if (versions != null)
      {
        writer.WritePropertyName(InkJsonContext.PropName_versions);
        this.ListInkNoteVersionFileSerializeHandler(writer, versions);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700051C RID: 1308
    // (get) Token: 0x0600164F RID: 5711 RVA: 0x003BCE28 File Offset: 0x003BCE28
    [Nullable(0)]
    public JsonTypeInfo<InkNoteVersionFile> InkNoteVersionFile
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<InkNoteVersionFile> jsonTypeInfo;
        if ((jsonTypeInfo = this._InkNoteVersionFile) == null)
        {
          jsonTypeInfo = (this._InkNoteVersionFile = (JsonTypeInfo<InkNoteVersionFile>)base.Options.GetTypeInfo(typeof(InkNoteVersionFile)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001650 RID: 5712 RVA: 0x003BCE64 File Offset: 0x003BCE64
    private JsonTypeInfo<InkNoteVersionFile> Create_InkNoteVersionFile(JsonSerializerOptions options)
    {
      JsonTypeInfo<InkNoteVersionFile> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<InkNoteVersionFile>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<InkNoteVersionFile> jsonObjectInfoValues = new JsonObjectInfoValues<InkNoteVersionFile>();
        jsonObjectInfoValues.ObjectCreator = () => new InkNoteVersionFile();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => InkJsonContext.InkNoteVersionFilePropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InkNoteVersionFile).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, InkNoteVersionFile>(this.InkNoteVersionFileSerializeHandler);
        JsonObjectInfoValues<InkNoteVersionFile> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<InkNoteVersionFile>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06001651 RID: 5713 RVA: 0x003BCF38 File Offset: 0x003BCF38
    private static JsonPropertyInfo[] InkNoteVersionFilePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[5];
      JsonPropertyInfoValues<double> jsonPropertyInfoValues = new JsonPropertyInfoValues<double>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(InkNoteVersionFile);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((InkNoteVersionFile)obj).At;
      jsonPropertyInfoValues.Setter = delegate(object obj, double value)
      {
        ((InkNoteVersionFile)obj).At = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "At";
      jsonPropertyInfoValues.JsonPropertyName = "at";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InkNoteVersionFile).GetProperty("At", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<double>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(InkNoteVersionFile);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((InkNoteVersionFile)obj).Text;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((InkNoteVersionFile)obj).Text = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Text";
      jsonPropertyInfoValues3.JsonPropertyName = "text";
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(InkNoteVersionFile).GetProperty("Text", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(InkNoteVersionFile);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((InkNoteVersionFile)obj).IsEdit;
      jsonPropertyInfoValues5.Setter = delegate(object obj, bool value)
      {
        ((InkNoteVersionFile)obj).IsEdit = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "IsEdit";
      jsonPropertyInfoValues5.JsonPropertyName = "isEdit";
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(InkNoteVersionFile).GetProperty("IsEdit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(InkNoteVersionFile);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((InkNoteVersionFile)obj).IsPartial;
      jsonPropertyInfoValues7.Setter = delegate(object obj, bool? value)
      {
        ((InkNoteVersionFile)obj).IsPartial = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "IsPartial";
      jsonPropertyInfoValues7.JsonPropertyName = "isPartial";
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(InkNoteVersionFile).GetProperty("IsPartial", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(InkNoteVersionFile);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((InkNoteVersionFile)obj).StartsNewEdit;
      jsonPropertyInfoValues9.Setter = delegate(object obj, bool? value)
      {
        ((InkNoteVersionFile)obj).StartsNewEdit = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "StartsNewEdit";
      jsonPropertyInfoValues9.JsonPropertyName = "startsNewEdit";
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(InkNoteVersionFile).GetProperty("StartsNewEdit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues10);
      return array;
    }

    // Token: 0x06001652 RID: 5714 RVA: 0x003BD40C File Offset: 0x003BD40C
    private void InkNoteVersionFileSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] InkNoteVersionFile value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteNumber(InkJsonContext.PropName_at, value.At);
      string text = value.Text;
      if (text != null)
      {
        writer.WriteString(InkJsonContext.PropName_text, text);
      }
      writer.WriteBoolean(InkJsonContext.PropName_isEdit, value.IsEdit);
      bool? isPartial = value.IsPartial;
      if (isPartial != null)
      {
        writer.WritePropertyName(InkJsonContext.PropName_isPartial);
        JsonSerializer.Serialize<bool?>(writer, isPartial, this.NullableBoolean);
      }
      bool? startsNewEdit = value.StartsNewEdit;
      if (startsNewEdit != null)
      {
        writer.WritePropertyName(InkJsonContext.PropName_startsNewEdit);
        JsonSerializer.Serialize<bool?>(writer, startsNewEdit, this.NullableBoolean);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700051D RID: 1309
    // (get) Token: 0x06001653 RID: 5715 RVA: 0x003BD4B8 File Offset: 0x003BD4B8
    [Nullable(0)]
    public JsonTypeInfo<InkSentActionFile> InkSentActionFile
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<InkSentActionFile> jsonTypeInfo;
        if ((jsonTypeInfo = this._InkSentActionFile) == null)
        {
          jsonTypeInfo = (this._InkSentActionFile = (JsonTypeInfo<InkSentActionFile>)base.Options.GetTypeInfo(typeof(InkSentActionFile)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001654 RID: 5716 RVA: 0x003BD4F4 File Offset: 0x003BD4F4
    private JsonTypeInfo<InkSentActionFile> Create_InkSentActionFile(JsonSerializerOptions options)
    {
      JsonTypeInfo<InkSentActionFile> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<InkSentActionFile>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<InkSentActionFile> jsonObjectInfoValues = new JsonObjectInfoValues<InkSentActionFile>();
        jsonObjectInfoValues.ObjectCreator = () => new InkSentActionFile();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => InkJsonContext.InkSentActionFilePropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InkSentActionFile).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, InkSentActionFile>(this.InkSentActionFileSerializeHandler);
        JsonObjectInfoValues<InkSentActionFile> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<InkSentActionFile>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06001655 RID: 5717 RVA: 0x003BD5C8 File Offset: 0x003BD5C8
    private static JsonPropertyInfo[] InkSentActionFilePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[6];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((InkSentActionFile)obj).Kind;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((InkSentActionFile)obj).Kind = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Kind";
      jsonPropertyInfoValues.JsonPropertyName = "kind";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("Kind", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((InkSentActionFile)obj).ObjectKey;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((InkSentActionFile)obj).ObjectKey = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ObjectKey";
      jsonPropertyInfoValues3.JsonPropertyName = "objectKey";
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("ObjectKey", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((InkSentActionFile)obj).MarginWords;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((InkSentActionFile)obj).MarginWords = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "MarginWords";
      jsonPropertyInfoValues5.JsonPropertyName = "marginWords";
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("MarginWords", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<double> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<double>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((InkSentActionFile)obj).SentAt;
      jsonPropertyInfoValues7.Setter = delegate(object obj, double value)
      {
        ((InkSentActionFile)obj).SentAt = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "SentAt";
      jsonPropertyInfoValues7.JsonPropertyName = "sentAt";
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("SentAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<double>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((InkSentActionFile)obj).WasSentWhileRunning;
      jsonPropertyInfoValues9.Setter = delegate(object obj, bool value)
      {
        ((InkSentActionFile)obj).WasSentWhileRunning = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "WasSentWhileRunning";
      jsonPropertyInfoValues9.JsonPropertyName = "wasSentWhileRunning";
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("WasSentWhileRunning", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<List<string>>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(InkSentActionFile);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((InkSentActionFile)obj).ReviewedItems;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<string> value)
      {
        ((InkSentActionFile)obj).ReviewedItems = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ReviewedItems";
      jsonPropertyInfoValues11.JsonPropertyName = "reviewedItems";
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(InkSentActionFile).GetProperty("ReviewedItems", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<List<string>>(options, jsonPropertyInfoValues12);
      return array;
    }

    // Token: 0x06001656 RID: 5718 RVA: 0x003BDBB0 File Offset: 0x003BDBB0
    private void InkSentActionFileSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] InkSentActionFile value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string kind = value.Kind;
      if (kind != null)
      {
        writer.WriteString(InkJsonContext.PropName_kind, kind);
      }
      string objectKey = value.ObjectKey;
      if (objectKey != null)
      {
        writer.WriteString(InkJsonContext.PropName_objectKey, objectKey);
      }
      string marginWords = value.MarginWords;
      if (marginWords != null)
      {
        writer.WriteString(InkJsonContext.PropName_marginWords, marginWords);
      }
      writer.WriteNumber(InkJsonContext.PropName_sentAt, value.SentAt);
      writer.WriteBoolean(InkJsonContext.PropName_wasSentWhileRunning, value.WasSentWhileRunning);
      List<string> reviewedItems = value.ReviewedItems;
      if (reviewedItems != null)
      {
        writer.WritePropertyName(InkJsonContext.PropName_reviewedItems);
        this.ListStringSerializeHandler(writer, reviewedItems);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700051E RID: 1310
    // (get) Token: 0x06001657 RID: 5719 RVA: 0x003BDC54 File Offset: 0x003BDC54
    [Nullable(0)]
    public JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>> DictionaryStringListInkSentActionFile
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>> jsonTypeInfo;
        if ((jsonTypeInfo = this._DictionaryStringListInkSentActionFile) == null)
        {
          jsonTypeInfo = (this._DictionaryStringListInkSentActionFile = (JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>>)base.Options.GetTypeInfo(typeof(Dictionary<string, List<InkSentActionFile>>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001658 RID: 5720 RVA: 0x003BDC90 File Offset: 0x003BDC90
    private JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>> Create_DictionaryStringListInkSentActionFile(JsonSerializerOptions options)
    {
      JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, List<InkSentActionFile>>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<Dictionary<string, List<InkSentActionFile>>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, List<InkSentActionFile>>>();
        jsonCollectionInfoValues.ObjectCreator = () => new Dictionary<string, List<InkSentActionFile>>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, Dictionary<string, List<InkSentActionFile>>>(this.DictionaryStringListInkSentActionFileSerializeHandler);
        JsonCollectionInfoValues<Dictionary<string, List<InkSentActionFile>>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, List<InkSentActionFile>>, string, List<InkSentActionFile>>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06001659 RID: 5721 RVA: 0x003BDD04 File Offset: 0x003BDD04
    private void DictionaryStringListInkSentActionFileSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1, 1, 1 })] Dictionary<string, List<InkSentActionFile>> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      foreach (KeyValuePair<string, List<InkSentActionFile>> keyValuePair in value)
      {
        writer.WritePropertyName(keyValuePair.Key);
        this.ListInkSentActionFileSerializeHandler(writer, keyValuePair.Value);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700051F RID: 1311
    // (get) Token: 0x0600165A RID: 5722 RVA: 0x003BDD7C File Offset: 0x003BDD7C
    [Nullable(0)]
    public JsonTypeInfo<List<InkNoteVersionFile>> ListInkNoteVersionFile
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<InkNoteVersionFile>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListInkNoteVersionFile) == null)
        {
          jsonTypeInfo = (this._ListInkNoteVersionFile = (JsonTypeInfo<List<InkNoteVersionFile>>)base.Options.GetTypeInfo(typeof(List<InkNoteVersionFile>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600165B RID: 5723 RVA: 0x003BDDB8 File Offset: 0x003BDDB8
    private JsonTypeInfo<List<InkNoteVersionFile>> Create_ListInkNoteVersionFile(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<InkNoteVersionFile>> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<InkNoteVersionFile>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<InkNoteVersionFile>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<InkNoteVersionFile>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<InkNoteVersionFile>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<InkNoteVersionFile>>(this.ListInkNoteVersionFileSerializeHandler);
        JsonCollectionInfoValues<List<InkNoteVersionFile>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<InkNoteVersionFile>, InkNoteVersionFile>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600165C RID: 5724 RVA: 0x003BDE2C File Offset: 0x003BDE2C
    private void ListInkNoteVersionFileSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<InkNoteVersionFile> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.InkNoteVersionFileSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000520 RID: 1312
    // (get) Token: 0x0600165D RID: 5725 RVA: 0x003BDE70 File Offset: 0x003BDE70
    [Nullable(0)]
    public JsonTypeInfo<List<InkSentActionFile>> ListInkSentActionFile
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<InkSentActionFile>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListInkSentActionFile) == null)
        {
          jsonTypeInfo = (this._ListInkSentActionFile = (JsonTypeInfo<List<InkSentActionFile>>)base.Options.GetTypeInfo(typeof(List<InkSentActionFile>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600165E RID: 5726 RVA: 0x003BDEAC File Offset: 0x003BDEAC
    private JsonTypeInfo<List<InkSentActionFile>> Create_ListInkSentActionFile(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<InkSentActionFile>> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<InkSentActionFile>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<InkSentActionFile>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<InkSentActionFile>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<InkSentActionFile>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<InkSentActionFile>>(this.ListInkSentActionFileSerializeHandler);
        JsonCollectionInfoValues<List<InkSentActionFile>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<InkSentActionFile>, InkSentActionFile>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600165F RID: 5727 RVA: 0x003BDF20 File Offset: 0x003BDF20
    private void ListInkSentActionFileSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<InkSentActionFile> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.InkSentActionFileSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000521 RID: 1313
    // (get) Token: 0x06001660 RID: 5728 RVA: 0x003BDF64 File Offset: 0x003BDF64
    [Nullable(0)]
    public JsonTypeInfo<List<string>> ListString
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<string>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListString) == null)
        {
          jsonTypeInfo = (this._ListString = (JsonTypeInfo<List<string>>)base.Options.GetTypeInfo(typeof(List<string>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001661 RID: 5729 RVA: 0x003BDFA0 File Offset: 0x003BDFA0
    private JsonTypeInfo<List<string>> Create_ListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<string>> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<string>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<string>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<string>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<string>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<string>>(this.ListStringSerializeHandler);
        JsonCollectionInfoValues<List<string>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<string>, string>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06001662 RID: 5730 RVA: 0x003BE014 File Offset: 0x003BE014
    private void ListStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<string> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        writer.WriteStringValue(value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000522 RID: 1314
    // (get) Token: 0x06001663 RID: 5731 RVA: 0x003BE058 File Offset: 0x003BE058
    [Nullable(0)]
    public JsonTypeInfo<int> Int32
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<int> jsonTypeInfo;
        if ((jsonTypeInfo = this._Int32) == null)
        {
          jsonTypeInfo = (this._Int32 = (JsonTypeInfo<int>)base.Options.GetTypeInfo(typeof(int)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001664 RID: 5732 RVA: 0x003BE094 File Offset: 0x003BE094
    private JsonTypeInfo<int> Create_Int32(JsonSerializerOptions options)
    {
      JsonTypeInfo<int> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<int>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int>(options, JsonMetadataServices.Int32Converter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000523 RID: 1315
    // (get) Token: 0x06001665 RID: 5733 RVA: 0x003BE0C0 File Offset: 0x003BE0C0
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

    // Token: 0x06001666 RID: 5734 RVA: 0x003BE0FC File Offset: 0x003BE0FC
    private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
    {
      JsonTypeInfo<string> jsonTypeInfo;
      if (!InkJsonContext.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000524 RID: 1316
    // (get) Token: 0x06001667 RID: 5735 RVA: 0x003BE128 File Offset: 0x003BE128
    public static InkJsonContext Default { get; } = new InkJsonContext(new JsonSerializerOptions(InkJsonContext.s_defaultOptions));

    // Token: 0x17000525 RID: 1317
    // (get) Token: 0x06001668 RID: 5736 RVA: 0x003BE130 File Offset: 0x003BE130
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = InkJsonContext.s_defaultOptions;

    // Token: 0x06001669 RID: 5737 RVA: 0x003BE138 File Offset: 0x003BE138
    public InkJsonContext()
      : base(null)
    {
    }

    // Token: 0x0600166A RID: 5738 RVA: 0x003BE14C File Offset: 0x003BE14C
    public InkJsonContext(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x0600166B RID: 5739 RVA: 0x003BE160 File Offset: 0x003BE160
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = InkJsonContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x0600166C RID: 5740 RVA: 0x003BE190 File Offset: 0x003BE190
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return InkJsonContext.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x0600166D RID: 5741 RVA: 0x003BE1D8 File Offset: 0x003BE1D8
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

    // Token: 0x0600166E RID: 5742 RVA: 0x003BE240 File Offset: 0x003BE240
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x0600166F RID: 5743 RVA: 0x003BE260 File Offset: 0x003BE260
    [return: Nullable(2)]
    JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
    {
      if (type == typeof(bool))
      {
        return this.Create_Boolean(options);
      }
      if (type == typeof(bool?))
      {
        return this.Create_NullableBoolean(options);
      }
      if (type == typeof(double))
      {
        return this.Create_Double(options);
      }
      if (type == typeof(InkActionsSnapshot))
      {
        return this.Create_InkActionsSnapshot(options);
      }
      if (type == typeof(InkNoteSnapshot))
      {
        return this.Create_InkNoteSnapshot(options);
      }
      if (type == typeof(InkNoteVersionFile))
      {
        return this.Create_InkNoteVersionFile(options);
      }
      if (type == typeof(InkSentActionFile))
      {
        return this.Create_InkSentActionFile(options);
      }
      if (type == typeof(Dictionary<string, List<InkSentActionFile>>))
      {
        return this.Create_DictionaryStringListInkSentActionFile(options);
      }
      if (type == typeof(List<InkNoteVersionFile>))
      {
        return this.Create_ListInkNoteVersionFile(options);
      }
      if (type == typeof(List<InkSentActionFile>))
      {
        return this.Create_ListInkSentActionFile(options);
      }
      if (type == typeof(List<string>))
      {
        return this.Create_ListString(options);
      }
      if (type == typeof(int))
      {
        return this.Create_Int32(options);
      }
      if (type == typeof(string))
      {
        return this.Create_String(options);
      }
      return null;
    }

    // Token: 0x040009B4 RID: 2484
    [Nullable(2)]
    private JsonTypeInfo<bool> _Boolean;

    // Token: 0x040009B5 RID: 2485
    [Nullable(2)]
    private JsonTypeInfo<bool?> _NullableBoolean;

    // Token: 0x040009B6 RID: 2486
    [Nullable(2)]
    private JsonTypeInfo<double> _Double;

    // Token: 0x040009B7 RID: 2487
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<InkActionsSnapshot> _InkActionsSnapshot;

    // Token: 0x040009B8 RID: 2488
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<InkNoteSnapshot> _InkNoteSnapshot;

    // Token: 0x040009B9 RID: 2489
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<InkNoteVersionFile> _InkNoteVersionFile;

    // Token: 0x040009BA RID: 2490
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<InkSentActionFile> _InkSentActionFile;

    // Token: 0x040009BB RID: 2491
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    private JsonTypeInfo<Dictionary<string, List<InkSentActionFile>>> _DictionaryStringListInkSentActionFile;

    // Token: 0x040009BC RID: 2492
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<InkNoteVersionFile>> _ListInkNoteVersionFile;

    // Token: 0x040009BD RID: 2493
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<InkSentActionFile>> _ListInkSentActionFile;

    // Token: 0x040009BE RID: 2494
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<string>> _ListString;

    // Token: 0x040009BF RID: 2495
    [Nullable(2)]
    private JsonTypeInfo<int> _Int32;

    // Token: 0x040009C0 RID: 2496
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<string> _String;

    // Token: 0x040009C1 RID: 2497
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Token: 0x040009C2 RID: 2498
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    // Token: 0x040009C5 RID: 2501
    private static readonly JsonEncodedText PropName_version = JsonEncodedText.Encode("version", null);

    // Token: 0x040009C6 RID: 2502
    private static readonly JsonEncodedText PropName_actionsByThingID = JsonEncodedText.Encode("actionsByThingID", null);

    // Token: 0x040009C7 RID: 2503
    private static readonly JsonEncodedText PropName_versions = JsonEncodedText.Encode("versions", null);

    // Token: 0x040009C8 RID: 2504
    private static readonly JsonEncodedText PropName_at = JsonEncodedText.Encode("at", null);

    // Token: 0x040009C9 RID: 2505
    private static readonly JsonEncodedText PropName_text = JsonEncodedText.Encode("text", null);

    // Token: 0x040009CA RID: 2506
    private static readonly JsonEncodedText PropName_isEdit = JsonEncodedText.Encode("isEdit", null);

    // Token: 0x040009CB RID: 2507
    private static readonly JsonEncodedText PropName_isPartial = JsonEncodedText.Encode("isPartial", null);

    // Token: 0x040009CC RID: 2508
    private static readonly JsonEncodedText PropName_startsNewEdit = JsonEncodedText.Encode("startsNewEdit", null);

    // Token: 0x040009CD RID: 2509
    private static readonly JsonEncodedText PropName_kind = JsonEncodedText.Encode("kind", null);

    // Token: 0x040009CE RID: 2510
    private static readonly JsonEncodedText PropName_objectKey = JsonEncodedText.Encode("objectKey", null);

    // Token: 0x040009CF RID: 2511
    private static readonly JsonEncodedText PropName_marginWords = JsonEncodedText.Encode("marginWords", null);

    // Token: 0x040009D0 RID: 2512
    private static readonly JsonEncodedText PropName_sentAt = JsonEncodedText.Encode("sentAt", null);

    // Token: 0x040009D1 RID: 2513
    private static readonly JsonEncodedText PropName_wasSentWhileRunning = JsonEncodedText.Encode("wasSentWhileRunning", null);

    // Token: 0x040009D2 RID: 2514
    private static readonly JsonEncodedText PropName_reviewedItems = JsonEncodedText.Encode("reviewedItems", null);
  }
}
