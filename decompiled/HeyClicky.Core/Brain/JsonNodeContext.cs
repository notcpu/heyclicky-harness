using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001FA RID: 506
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSerializable(typeof(JsonObject))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  internal sealed class JsonNodeContext : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x17000678 RID: 1656
    // (get) Token: 0x06001D6A RID: 7530 RVA: 0x003E8B8C File Offset: 0x003E8B8C
    [Nullable(0)]
    public JsonTypeInfo<JsonObject> JsonObject
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<JsonObject> jsonTypeInfo;
        if ((jsonTypeInfo = this._JsonObject) == null)
        {
          jsonTypeInfo = (this._JsonObject = (JsonTypeInfo<JsonObject>)base.Options.GetTypeInfo(typeof(JsonObject)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06001D6B RID: 7531 RVA: 0x003E8BC8 File Offset: 0x003E8BC8
    private JsonTypeInfo<JsonObject> Create_JsonObject(JsonSerializerOptions options)
    {
      JsonTypeInfo<JsonObject> jsonTypeInfo;
      if (!JsonNodeContext.TryGetTypeInfoForRuntimeCustomConverter<JsonObject>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<JsonObject>(options, JsonMetadataServices.JsonObjectConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000679 RID: 1657
    // (get) Token: 0x06001D6C RID: 7532 RVA: 0x003E8BF4 File Offset: 0x003E8BF4
    public static JsonNodeContext Default { get; } = new JsonNodeContext(new JsonSerializerOptions(JsonNodeContext.s_defaultOptions));

    // Token: 0x1700067A RID: 1658
    // (get) Token: 0x06001D6D RID: 7533 RVA: 0x003E8BFC File Offset: 0x003E8BFC
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = JsonNodeContext.s_defaultOptions;

    // Token: 0x06001D6E RID: 7534 RVA: 0x003E8C04 File Offset: 0x003E8C04
    public JsonNodeContext()
      : base(null)
    {
    }

    // Token: 0x06001D6F RID: 7535 RVA: 0x003E8C18 File Offset: 0x003E8C18
    public JsonNodeContext(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x06001D70 RID: 7536 RVA: 0x003E8C2C File Offset: 0x003E8C2C
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = JsonNodeContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x06001D71 RID: 7537 RVA: 0x003E8C5C File Offset: 0x003E8C5C
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return JsonNodeContext.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x06001D72 RID: 7538 RVA: 0x003E8CA4 File Offset: 0x003E8CA4
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

    // Token: 0x06001D73 RID: 7539 RVA: 0x003E8D0C File Offset: 0x003E8D0C
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x06001D74 RID: 7540 RVA: 0x003E8D2C File Offset: 0x003E8D2C
    [return: Nullable(2)]
    JsonTypeInfo IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
    {
      if (type == typeof(JsonObject))
      {
        return this.Create_JsonObject(options);
      }
      return null;
    }

    // Token: 0x04000CA4 RID: 3236
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<JsonObject> _JsonObject;

    // Token: 0x04000CA5 RID: 3237
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions();

    // Token: 0x04000CA6 RID: 3238
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
  }
}
