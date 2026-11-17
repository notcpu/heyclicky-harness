using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000074 RID: 116
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSerializable(typeof(List<List<string>>))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  internal sealed class EarsLanguageJson : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x1700019A RID: 410
    // (get) Token: 0x06000719 RID: 1817 RVA: 0x0036228C File Offset: 0x0036228C
    [Nullable(0)]
    public JsonTypeInfo<List<List<string>>> ListListString
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<List<string>>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListListString) == null)
        {
          jsonTypeInfo = (this._ListListString = (JsonTypeInfo<List<List<string>>>)base.Options.GetTypeInfo(typeof(List<List<string>>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600071A RID: 1818 RVA: 0x003622C8 File Offset: 0x003622C8
    private JsonTypeInfo<List<List<string>>> Create_ListListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<List<string>>> jsonTypeInfo;
      if (!EarsLanguageJson.TryGetTypeInfoForRuntimeCustomConverter<List<List<string>>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<List<string>>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<List<string>>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<List<string>>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<List<string>>>(this.ListListStringSerializeHandler);
        JsonCollectionInfoValues<List<List<string>>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<List<string>>, List<string>>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600071B RID: 1819 RVA: 0x0036233C File Offset: 0x0036233C
    private void ListListStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1, 1 })] List<List<string>> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.ListStringSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700019B RID: 411
    // (get) Token: 0x0600071C RID: 1820 RVA: 0x00362380 File Offset: 0x00362380
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

    // Token: 0x0600071D RID: 1821 RVA: 0x003623BC File Offset: 0x003623BC
    private JsonTypeInfo<List<string>> Create_ListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<string>> jsonTypeInfo;
      if (!EarsLanguageJson.TryGetTypeInfoForRuntimeCustomConverter<List<string>>(options, out jsonTypeInfo))
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

    // Token: 0x0600071E RID: 1822 RVA: 0x00362430 File Offset: 0x00362430
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

    // Token: 0x1700019C RID: 412
    // (get) Token: 0x0600071F RID: 1823 RVA: 0x00362474 File Offset: 0x00362474
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

    // Token: 0x06000720 RID: 1824 RVA: 0x003624B0 File Offset: 0x003624B0
    private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
    {
      JsonTypeInfo<string> jsonTypeInfo;
      if (!EarsLanguageJson.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700019D RID: 413
    // (get) Token: 0x06000721 RID: 1825 RVA: 0x003624DC File Offset: 0x003624DC
    public static EarsLanguageJson Default { get; } = new EarsLanguageJson(new JsonSerializerOptions(EarsLanguageJson.s_defaultOptions));

    // Token: 0x1700019E RID: 414
    // (get) Token: 0x06000722 RID: 1826 RVA: 0x003624E4 File Offset: 0x003624E4
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = EarsLanguageJson.s_defaultOptions;

    // Token: 0x06000723 RID: 1827 RVA: 0x003624EC File Offset: 0x003624EC
    public EarsLanguageJson()
      : base(null)
    {
    }

    // Token: 0x06000724 RID: 1828 RVA: 0x00362500 File Offset: 0x00362500
    public EarsLanguageJson(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x06000725 RID: 1829 RVA: 0x00362514 File Offset: 0x00362514
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = EarsLanguageJson.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x06000726 RID: 1830 RVA: 0x00362544 File Offset: 0x00362544
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return EarsLanguageJson.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x06000727 RID: 1831 RVA: 0x0036258C File Offset: 0x0036258C
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

    // Token: 0x06000728 RID: 1832 RVA: 0x003625F4 File Offset: 0x003625F4
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x06000729 RID: 1833 RVA: 0x00362614 File Offset: 0x00362614
    [return: Nullable(2)]
    JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
    {
      if (type == typeof(List<List<string>>))
      {
        return this.Create_ListListString(options);
      }
      if (type == typeof(List<string>))
      {
        return this.Create_ListString(options);
      }
      if (type == typeof(string))
      {
        return this.Create_String(options);
      }
      return null;
    }

    // Token: 0x0400034A RID: 842
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    private JsonTypeInfo<List<List<string>>> _ListListString;

    // Token: 0x0400034B RID: 843
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<string>> _ListString;

    // Token: 0x0400034C RID: 844
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<string> _String;

    // Token: 0x0400034D RID: 845
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions();

    // Token: 0x0400034E RID: 846
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
  }
}
