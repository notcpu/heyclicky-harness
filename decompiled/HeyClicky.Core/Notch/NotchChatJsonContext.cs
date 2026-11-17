using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HeyClicky.Core.Notch
{
  // Token: 0x02000101 RID: 257
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, UseStringEnumConverter = true)]
  [JsonSerializable(typeof(CachedConversation))]
  [JsonSerializable(typeof(List<ChatHandOff>))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  internal sealed class NotchChatJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x170003EC RID: 1004
    // (get) Token: 0x060010D6 RID: 4310 RVA: 0x00394A00 File Offset: 0x00394A00
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

    // Token: 0x060010D7 RID: 4311 RVA: 0x00394A3C File Offset: 0x00394A3C
    private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003ED RID: 1005
    // (get) Token: 0x060010D8 RID: 4312 RVA: 0x00394A68 File Offset: 0x00394A68
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

    // Token: 0x060010D9 RID: 4313 RVA: 0x00394AA4 File Offset: 0x00394AA4
    private JsonTypeInfo<bool?> Create_NullableBoolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool?> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<bool>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003EE RID: 1006
    // (get) Token: 0x060010DA RID: 4314 RVA: 0x00394AD4 File Offset: 0x00394AD4
    [Nullable(0)]
    public JsonTypeInfo<CachedConversation> CachedConversation
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<CachedConversation> jsonTypeInfo;
        if ((jsonTypeInfo = this._CachedConversation) == null)
        {
          jsonTypeInfo = (this._CachedConversation = (JsonTypeInfo<CachedConversation>)base.Options.GetTypeInfo(typeof(CachedConversation)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010DB RID: 4315 RVA: 0x00394B10 File Offset: 0x00394B10
    private JsonTypeInfo<CachedConversation> Create_CachedConversation(JsonSerializerOptions options)
    {
      JsonTypeInfo<CachedConversation> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<CachedConversation>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<CachedConversation> jsonObjectInfoValues = new JsonObjectInfoValues<CachedConversation>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new CachedConversation((IReadOnlyList<ConversationMessage>)args[0], (bool)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => NotchChatJsonContext.CachedConversationPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = NotchChatJsonContext.<>O.<0>__CachedConversationCtorParamInit) == null)
        {
          func = (NotchChatJsonContext.<>O.<0>__CachedConversationCtorParamInit = new Func<JsonParameterInfoValues[]>(NotchChatJsonContext.CachedConversationCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(CachedConversation).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(IReadOnlyList<ConversationMessage>),
          typeof(bool)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, CachedConversation>(this.CachedConversationSerializeHandler);
        JsonObjectInfoValues<CachedConversation> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<CachedConversation>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010DC RID: 4316 RVA: 0x00394BFC File Offset: 0x00394BFC
    private static JsonPropertyInfo[] CachedConversationPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<IReadOnlyList<ConversationMessage>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<ConversationMessage>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(CachedConversation);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((CachedConversation)obj).Messages;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ConversationMessage> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Messages";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(CachedConversation).GetProperty("Messages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<ConversationMessage>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<ConversationMessage>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<ConversationMessage>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(CachedConversation);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((CachedConversation)obj).HasEarlierMessages;
      jsonPropertyInfoValues3.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "HasEarlierMessages";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(CachedConversation).GetProperty("HasEarlierMessages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x060010DD RID: 4317 RVA: 0x00394DF4 File Offset: 0x00394DF4
    private void CachedConversationSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] CachedConversation value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<ConversationMessage> messages = value.Messages;
      if (messages != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_Messages);
        this.IReadOnlyListConversationMessageSerializeHandler(writer, messages);
      }
      writer.WriteBoolean(NotchChatJsonContext.PropName_HasEarlierMessages, value.HasEarlierMessages);
      writer.WriteEndObject();
    }

    // Token: 0x060010DE RID: 4318 RVA: 0x00394E48 File Offset: 0x00394E48
    private static JsonParameterInfoValues[] CachedConversationCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Messages",
          ParameterType = typeof(IReadOnlyList<ConversationMessage>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "HasEarlierMessages",
          ParameterType = typeof(bool),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x170003EF RID: 1007
    // (get) Token: 0x060010DF RID: 4319 RVA: 0x00394EDC File Offset: 0x00394EDC
    [Nullable(0)]
    public JsonTypeInfo<ChatHandOff> ChatHandOff
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ChatHandOff> jsonTypeInfo;
        if ((jsonTypeInfo = this._ChatHandOff) == null)
        {
          jsonTypeInfo = (this._ChatHandOff = (JsonTypeInfo<ChatHandOff>)base.Options.GetTypeInfo(typeof(ChatHandOff)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010E0 RID: 4320 RVA: 0x00394F18 File Offset: 0x00394F18
    private JsonTypeInfo<ChatHandOff> Create_ChatHandOff(JsonSerializerOptions options)
    {
      JsonTypeInfo<ChatHandOff> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ChatHandOff>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ChatHandOff> jsonObjectInfoValues = new JsonObjectInfoValues<ChatHandOff>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ChatHandOff
        {
          Id = (string)args[0],
          Kind = (ChatHandOffKind)args[1],
          JobId = (string)args[2],
          ClickySlug = (string)args[3],
          Title = (string)args[4],
          CreatedAt = (DateTimeOffset)args[5],
          IsFollowUp = (bool)args[6],
          IsSentFromWindow = (bool)args[7],
          Outcome = (ChatHandOffOutcome?)args[8],
          ReplyText = (string)args[9],
          LessonId = (string)args[10],
          DeliveredText = (string)args[11],
          DeliveredToApp = (string)args[12],
          WasCopied = (bool?)args[13],
          FullReply = (string)args[14],
          ArtifactEntries = (IReadOnlyList<string>)args[15],
          Answer = (JsonObject)args[16],
          ThreadId = (string)args[17],
          Card = (JsonObject)args[18],
          IsRoutineRun = (bool?)args[19],
          TaskTitle = (string)args[20],
          FollowUps = (IReadOnlyList<string>)args[21],
          SentText = (string)args[22],
          SentFiles = (IReadOnlyList<string>)args[23],
          SitesRead = (IReadOnlyList<string>)args[24]
        };
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => NotchChatJsonContext.ChatHandOffPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = NotchChatJsonContext.<>O.<1>__ChatHandOffCtorParamInit) == null)
        {
          func = (NotchChatJsonContext.<>O.<1>__ChatHandOffCtorParamInit = new Func<JsonParameterInfoValues[]>(NotchChatJsonContext.ChatHandOffCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ChatHandOff).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ChatHandOff>(this.ChatHandOffSerializeHandler);
        JsonObjectInfoValues<ChatHandOff> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ChatHandOff>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010E1 RID: 4321 RVA: 0x00395004 File Offset: 0x00395004
    private static JsonPropertyInfo[] ChatHandOffPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[26];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ChatHandOff)obj).Id;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Id";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsRequired = true;
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<ChatHandOffKind> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<ChatHandOffKind>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ChatHandOff)obj).Kind;
      jsonPropertyInfoValues3.Setter = delegate(object obj, ChatHandOffKind value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Kind";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Kind", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ChatHandOffKind), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ChatHandOffKind> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<ChatHandOffKind>(options, jsonPropertyInfoValues4);
      array[1].IsRequired = true;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ChatHandOff)obj).JobId;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "JobId";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("JobId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ChatHandOff)obj).ClickySlug;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "ClickySlug";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("ClickySlug", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ChatHandOff)obj).Title;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Title";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsRequired = true;
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<DateTimeOffset>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ChatHandOff)obj).CreatedAt;
      jsonPropertyInfoValues11.Setter = delegate(object obj, DateTimeOffset value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "CreatedAt";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues12);
      array[5].IsRequired = true;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ChatHandOff)obj).IsFollowUp;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "IsFollowUp";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("IsFollowUp", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((ChatHandOff)obj).IsSentFromWindow;
      jsonPropertyInfoValues15.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "IsSentFromWindow";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("IsSentFromWindow", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<ChatHandOffOutcome?> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<ChatHandOffOutcome?>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((ChatHandOff)obj).Outcome;
      jsonPropertyInfoValues17.Setter = delegate(object obj, ChatHandOffOutcome? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "Outcome";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Outcome", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ChatHandOffOutcome?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ChatHandOffOutcome?> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<ChatHandOffOutcome?>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((ChatHandOff)obj).ReplyText;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "ReplyText";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("ReplyText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((ChatHandOff)obj).LessonId;
      jsonPropertyInfoValues21.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "LessonId";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("LessonId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((ChatHandOff)obj).DeliveredText;
      jsonPropertyInfoValues23.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "DeliveredText";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("DeliveredText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = (object obj) => ((ChatHandOff)obj).DeliveredToApp;
      jsonPropertyInfoValues25.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues25.IgnoreCondition = null;
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "DeliveredToApp";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("DeliveredToApp", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues26);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues27.IsProperty = true;
      jsonPropertyInfoValues27.IsPublic = true;
      jsonPropertyInfoValues27.IsVirtual = false;
      jsonPropertyInfoValues27.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues27.Converter = null;
      jsonPropertyInfoValues27.Getter = (object obj) => ((ChatHandOff)obj).WasCopied;
      jsonPropertyInfoValues27.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues27.IgnoreCondition = null;
      jsonPropertyInfoValues27.HasJsonInclude = false;
      jsonPropertyInfoValues27.IsExtensionData = false;
      jsonPropertyInfoValues27.NumberHandling = null;
      jsonPropertyInfoValues27.PropertyName = "WasCopied";
      jsonPropertyInfoValues27.JsonPropertyName = null;
      jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("WasCopied", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
      array[13] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues28);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues29.IsProperty = true;
      jsonPropertyInfoValues29.IsPublic = true;
      jsonPropertyInfoValues29.IsVirtual = false;
      jsonPropertyInfoValues29.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues29.Converter = null;
      jsonPropertyInfoValues29.Getter = (object obj) => ((ChatHandOff)obj).FullReply;
      jsonPropertyInfoValues29.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues29.IgnoreCondition = null;
      jsonPropertyInfoValues29.HasJsonInclude = false;
      jsonPropertyInfoValues29.IsExtensionData = false;
      jsonPropertyInfoValues29.NumberHandling = null;
      jsonPropertyInfoValues29.PropertyName = "FullReply";
      jsonPropertyInfoValues29.JsonPropertyName = null;
      jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("FullReply", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
      array[14] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues30);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues31 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues31.IsProperty = true;
      jsonPropertyInfoValues31.IsPublic = true;
      jsonPropertyInfoValues31.IsVirtual = false;
      jsonPropertyInfoValues31.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues31.Converter = null;
      jsonPropertyInfoValues31.Getter = (object obj) => ((ChatHandOff)obj).ArtifactEntries;
      jsonPropertyInfoValues31.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues31.IgnoreCondition = null;
      jsonPropertyInfoValues31.HasJsonInclude = false;
      jsonPropertyInfoValues31.IsExtensionData = false;
      jsonPropertyInfoValues31.NumberHandling = null;
      jsonPropertyInfoValues31.PropertyName = "ArtifactEntries";
      jsonPropertyInfoValues31.JsonPropertyName = null;
      jsonPropertyInfoValues31.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("ArtifactEntries", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues32 = jsonPropertyInfoValues31;
      array[15] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues32);
      JsonPropertyInfoValues<JsonObject> jsonPropertyInfoValues33 = new JsonPropertyInfoValues<JsonObject>();
      jsonPropertyInfoValues33.IsProperty = true;
      jsonPropertyInfoValues33.IsPublic = true;
      jsonPropertyInfoValues33.IsVirtual = false;
      jsonPropertyInfoValues33.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues33.Converter = null;
      jsonPropertyInfoValues33.Getter = (object obj) => ((ChatHandOff)obj).Answer;
      jsonPropertyInfoValues33.Setter = delegate(object obj, [Nullable(2)] JsonObject value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues33.IgnoreCondition = null;
      jsonPropertyInfoValues33.HasJsonInclude = false;
      jsonPropertyInfoValues33.IsExtensionData = false;
      jsonPropertyInfoValues33.NumberHandling = null;
      jsonPropertyInfoValues33.PropertyName = "Answer";
      jsonPropertyInfoValues33.JsonPropertyName = null;
      jsonPropertyInfoValues33.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Answer", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(JsonObject), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<JsonObject> jsonPropertyInfoValues34 = jsonPropertyInfoValues33;
      array[16] = JsonMetadataServices.CreatePropertyInfo<JsonObject>(options, jsonPropertyInfoValues34);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues35 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues35.IsProperty = true;
      jsonPropertyInfoValues35.IsPublic = true;
      jsonPropertyInfoValues35.IsVirtual = false;
      jsonPropertyInfoValues35.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues35.Converter = null;
      jsonPropertyInfoValues35.Getter = (object obj) => ((ChatHandOff)obj).ThreadId;
      jsonPropertyInfoValues35.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues35.IgnoreCondition = null;
      jsonPropertyInfoValues35.HasJsonInclude = false;
      jsonPropertyInfoValues35.IsExtensionData = false;
      jsonPropertyInfoValues35.NumberHandling = null;
      jsonPropertyInfoValues35.PropertyName = "ThreadId";
      jsonPropertyInfoValues35.JsonPropertyName = null;
      jsonPropertyInfoValues35.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("ThreadId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues36 = jsonPropertyInfoValues35;
      array[17] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues36);
      JsonPropertyInfoValues<JsonObject> jsonPropertyInfoValues37 = new JsonPropertyInfoValues<JsonObject>();
      jsonPropertyInfoValues37.IsProperty = true;
      jsonPropertyInfoValues37.IsPublic = true;
      jsonPropertyInfoValues37.IsVirtual = false;
      jsonPropertyInfoValues37.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues37.Converter = null;
      jsonPropertyInfoValues37.Getter = (object obj) => ((ChatHandOff)obj).Card;
      jsonPropertyInfoValues37.Setter = delegate(object obj, [Nullable(2)] JsonObject value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues37.IgnoreCondition = null;
      jsonPropertyInfoValues37.HasJsonInclude = false;
      jsonPropertyInfoValues37.IsExtensionData = false;
      jsonPropertyInfoValues37.NumberHandling = null;
      jsonPropertyInfoValues37.PropertyName = "Card";
      jsonPropertyInfoValues37.JsonPropertyName = null;
      jsonPropertyInfoValues37.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("Card", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(JsonObject), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<JsonObject> jsonPropertyInfoValues38 = jsonPropertyInfoValues37;
      array[18] = JsonMetadataServices.CreatePropertyInfo<JsonObject>(options, jsonPropertyInfoValues38);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues39 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues39.IsProperty = true;
      jsonPropertyInfoValues39.IsPublic = true;
      jsonPropertyInfoValues39.IsVirtual = false;
      jsonPropertyInfoValues39.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues39.Converter = null;
      jsonPropertyInfoValues39.Getter = (object obj) => ((ChatHandOff)obj).IsRoutineRun;
      jsonPropertyInfoValues39.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues39.IgnoreCondition = null;
      jsonPropertyInfoValues39.HasJsonInclude = false;
      jsonPropertyInfoValues39.IsExtensionData = false;
      jsonPropertyInfoValues39.NumberHandling = null;
      jsonPropertyInfoValues39.PropertyName = "IsRoutineRun";
      jsonPropertyInfoValues39.JsonPropertyName = null;
      jsonPropertyInfoValues39.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("IsRoutineRun", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues40 = jsonPropertyInfoValues39;
      array[19] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues40);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues41 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues41.IsProperty = true;
      jsonPropertyInfoValues41.IsPublic = true;
      jsonPropertyInfoValues41.IsVirtual = false;
      jsonPropertyInfoValues41.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues41.Converter = null;
      jsonPropertyInfoValues41.Getter = (object obj) => ((ChatHandOff)obj).TaskTitle;
      jsonPropertyInfoValues41.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues41.IgnoreCondition = null;
      jsonPropertyInfoValues41.HasJsonInclude = false;
      jsonPropertyInfoValues41.IsExtensionData = false;
      jsonPropertyInfoValues41.NumberHandling = null;
      jsonPropertyInfoValues41.PropertyName = "TaskTitle";
      jsonPropertyInfoValues41.JsonPropertyName = null;
      jsonPropertyInfoValues41.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("TaskTitle", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues42 = jsonPropertyInfoValues41;
      array[20] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues42);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues43 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues43.IsProperty = true;
      jsonPropertyInfoValues43.IsPublic = true;
      jsonPropertyInfoValues43.IsVirtual = false;
      jsonPropertyInfoValues43.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues43.Converter = null;
      jsonPropertyInfoValues43.Getter = (object obj) => ((ChatHandOff)obj).FollowUps;
      jsonPropertyInfoValues43.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues43.IgnoreCondition = null;
      jsonPropertyInfoValues43.HasJsonInclude = false;
      jsonPropertyInfoValues43.IsExtensionData = false;
      jsonPropertyInfoValues43.NumberHandling = null;
      jsonPropertyInfoValues43.PropertyName = "FollowUps";
      jsonPropertyInfoValues43.JsonPropertyName = null;
      jsonPropertyInfoValues43.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("FollowUps", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues44 = jsonPropertyInfoValues43;
      array[21] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues44);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues45 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues45.IsProperty = true;
      jsonPropertyInfoValues45.IsPublic = true;
      jsonPropertyInfoValues45.IsVirtual = false;
      jsonPropertyInfoValues45.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues45.Converter = null;
      jsonPropertyInfoValues45.Getter = (object obj) => ((ChatHandOff)obj).SentText;
      jsonPropertyInfoValues45.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues45.IgnoreCondition = null;
      jsonPropertyInfoValues45.HasJsonInclude = false;
      jsonPropertyInfoValues45.IsExtensionData = false;
      jsonPropertyInfoValues45.NumberHandling = null;
      jsonPropertyInfoValues45.PropertyName = "SentText";
      jsonPropertyInfoValues45.JsonPropertyName = null;
      jsonPropertyInfoValues45.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("SentText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues46 = jsonPropertyInfoValues45;
      array[22] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues46);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues47 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues47.IsProperty = true;
      jsonPropertyInfoValues47.IsPublic = true;
      jsonPropertyInfoValues47.IsVirtual = false;
      jsonPropertyInfoValues47.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues47.Converter = null;
      jsonPropertyInfoValues47.Getter = (object obj) => ((ChatHandOff)obj).SentFiles;
      jsonPropertyInfoValues47.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues47.IgnoreCondition = null;
      jsonPropertyInfoValues47.HasJsonInclude = false;
      jsonPropertyInfoValues47.IsExtensionData = false;
      jsonPropertyInfoValues47.NumberHandling = null;
      jsonPropertyInfoValues47.PropertyName = "SentFiles";
      jsonPropertyInfoValues47.JsonPropertyName = null;
      jsonPropertyInfoValues47.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("SentFiles", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues48 = jsonPropertyInfoValues47;
      array[23] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues48);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues49 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues49.IsProperty = true;
      jsonPropertyInfoValues49.IsPublic = true;
      jsonPropertyInfoValues49.IsVirtual = false;
      jsonPropertyInfoValues49.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues49.Converter = null;
      jsonPropertyInfoValues49.Getter = (object obj) => ((ChatHandOff)obj).SitesRead;
      jsonPropertyInfoValues49.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues49.IgnoreCondition = null;
      jsonPropertyInfoValues49.HasJsonInclude = false;
      jsonPropertyInfoValues49.IsExtensionData = false;
      jsonPropertyInfoValues49.NumberHandling = null;
      jsonPropertyInfoValues49.PropertyName = "SitesRead";
      jsonPropertyInfoValues49.JsonPropertyName = null;
      jsonPropertyInfoValues49.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("SitesRead", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues50 = jsonPropertyInfoValues49;
      array[24] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues50);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues51 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues51.IsProperty = true;
      jsonPropertyInfoValues51.IsPublic = true;
      jsonPropertyInfoValues51.IsVirtual = false;
      jsonPropertyInfoValues51.DeclaringType = typeof(ChatHandOff);
      jsonPropertyInfoValues51.Converter = null;
      jsonPropertyInfoValues51.Getter = null;
      jsonPropertyInfoValues51.Setter = null;
      jsonPropertyInfoValues51.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues51.HasJsonInclude = false;
      jsonPropertyInfoValues51.IsExtensionData = false;
      jsonPropertyInfoValues51.NumberHandling = null;
      jsonPropertyInfoValues51.PropertyName = "TargetKey";
      jsonPropertyInfoValues51.JsonPropertyName = null;
      jsonPropertyInfoValues51.AttributeProviderFactory = () => typeof(ChatHandOff).GetProperty("TargetKey", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues52 = jsonPropertyInfoValues51;
      array[25] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues52);
      array[25].IsGetNullable = false;
      return array;
    }

    // Token: 0x060010E2 RID: 4322 RVA: 0x00396848 File Offset: 0x00396848
    private void ChatHandOffSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ChatHandOff value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string id = value.Id;
      if (id != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Id, id);
      }
      writer.WritePropertyName(NotchChatJsonContext.PropName_Kind);
      JsonSerializer.Serialize<ChatHandOffKind>(writer, value.Kind, this.ChatHandOffKind);
      string jobId = value.JobId;
      if (jobId != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_JobId, jobId);
      }
      string clickySlug = value.ClickySlug;
      if (clickySlug != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_ClickySlug, clickySlug);
      }
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Title, title);
      }
      writer.WriteString(NotchChatJsonContext.PropName_CreatedAt, value.CreatedAt);
      writer.WriteBoolean(NotchChatJsonContext.PropName_IsFollowUp, value.IsFollowUp);
      writer.WriteBoolean(NotchChatJsonContext.PropName_IsSentFromWindow, value.IsSentFromWindow);
      ChatHandOffOutcome? outcome = value.Outcome;
      if (outcome != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_Outcome);
        JsonSerializer.Serialize<ChatHandOffOutcome?>(writer, outcome, this.NullableChatHandOffOutcome);
      }
      string replyText = value.ReplyText;
      if (replyText != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_ReplyText, replyText);
      }
      string lessonId = value.LessonId;
      if (lessonId != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_LessonId, lessonId);
      }
      string deliveredText = value.DeliveredText;
      if (deliveredText != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_DeliveredText, deliveredText);
      }
      string deliveredToApp = value.DeliveredToApp;
      if (deliveredToApp != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_DeliveredToApp, deliveredToApp);
      }
      bool? wasCopied = value.WasCopied;
      if (wasCopied != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_WasCopied);
        JsonSerializer.Serialize<bool?>(writer, wasCopied, this.NullableBoolean);
      }
      string fullReply = value.FullReply;
      if (fullReply != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_FullReply, fullReply);
      }
      IReadOnlyList<string> artifactEntries = value.ArtifactEntries;
      if (artifactEntries != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_ArtifactEntries);
        this.IReadOnlyListStringSerializeHandler(writer, artifactEntries);
      }
      JsonObject answer = value.Answer;
      if (answer != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_Answer);
        JsonSerializer.Serialize<JsonObject>(writer, answer, this.JsonObject);
      }
      string threadId = value.ThreadId;
      if (threadId != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_ThreadId, threadId);
      }
      JsonObject card = value.Card;
      if (card != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_Card);
        JsonSerializer.Serialize<JsonObject>(writer, card, this.JsonObject);
      }
      bool? isRoutineRun = value.IsRoutineRun;
      if (isRoutineRun != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_IsRoutineRun);
        JsonSerializer.Serialize<bool?>(writer, isRoutineRun, this.NullableBoolean);
      }
      string taskTitle = value.TaskTitle;
      if (taskTitle != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_TaskTitle, taskTitle);
      }
      IReadOnlyList<string> followUps = value.FollowUps;
      if (followUps != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_FollowUps);
        this.IReadOnlyListStringSerializeHandler(writer, followUps);
      }
      string sentText = value.SentText;
      if (sentText != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_SentText, sentText);
      }
      IReadOnlyList<string> sentFiles = value.SentFiles;
      if (sentFiles != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_SentFiles);
        this.IReadOnlyListStringSerializeHandler(writer, sentFiles);
      }
      IReadOnlyList<string> sitesRead = value.SitesRead;
      if (sitesRead != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_SitesRead);
        this.IReadOnlyListStringSerializeHandler(writer, sitesRead);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060010E3 RID: 4323 RVA: 0x00396B24 File Offset: 0x00396B24
    private static JsonParameterInfoValues[] ChatHandOffCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Id",
          ParameterType = typeof(string),
          Position = 0,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Kind",
          ParameterType = typeof(ChatHandOffKind),
          Position = 1,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "JobId",
          ParameterType = typeof(string),
          Position = 2,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ClickySlug",
          ParameterType = typeof(string),
          Position = 3,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 4,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "CreatedAt",
          ParameterType = typeof(DateTimeOffset),
          Position = 5,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsFollowUp",
          ParameterType = typeof(bool),
          Position = 6,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsSentFromWindow",
          ParameterType = typeof(bool),
          Position = 7,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Outcome",
          ParameterType = typeof(ChatHandOffOutcome?),
          Position = 8,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ReplyText",
          ParameterType = typeof(string),
          Position = 9,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "LessonId",
          ParameterType = typeof(string),
          Position = 10,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "DeliveredText",
          ParameterType = typeof(string),
          Position = 11,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "DeliveredToApp",
          ParameterType = typeof(string),
          Position = 12,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "WasCopied",
          ParameterType = typeof(bool?),
          Position = 13,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "FullReply",
          ParameterType = typeof(string),
          Position = 14,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ArtifactEntries",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 15,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Answer",
          ParameterType = typeof(JsonObject),
          Position = 16,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ThreadId",
          ParameterType = typeof(string),
          Position = 17,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Card",
          ParameterType = typeof(JsonObject),
          Position = 18,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsRoutineRun",
          ParameterType = typeof(bool?),
          Position = 19,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "TaskTitle",
          ParameterType = typeof(string),
          Position = 20,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "FollowUps",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 21,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "SentText",
          ParameterType = typeof(string),
          Position = 22,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "SentFiles",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 23,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "SitesRead",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 24,
          IsNullable = true,
          IsMemberInitializer = true
        }
      };
    }

    // Token: 0x170003F0 RID: 1008
    // (get) Token: 0x060010E4 RID: 4324 RVA: 0x003970D0 File Offset: 0x003970D0
    [Nullable(0)]
    public JsonTypeInfo<ChatHandOffKind> ChatHandOffKind
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ChatHandOffKind> jsonTypeInfo;
        if ((jsonTypeInfo = this._ChatHandOffKind) == null)
        {
          jsonTypeInfo = (this._ChatHandOffKind = (JsonTypeInfo<ChatHandOffKind>)base.Options.GetTypeInfo(typeof(ChatHandOffKind)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010E5 RID: 4325 RVA: 0x0039710C File Offset: 0x0039710C
    private JsonTypeInfo<ChatHandOffKind> Create_ChatHandOffKind(JsonSerializerOptions options)
    {
      JsonTypeInfo<ChatHandOffKind> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ChatHandOffKind>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = NotchChatJsonContext.ExpandConverter(typeof(ChatHandOffKind), new JsonStringEnumConverter<ChatHandOffKind>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ChatHandOffKind>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003F1 RID: 1009
    // (get) Token: 0x060010E6 RID: 4326 RVA: 0x0039714C File Offset: 0x0039714C
    [Nullable(0)]
    public JsonTypeInfo<ChatHandOffOutcome> ChatHandOffOutcome
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ChatHandOffOutcome> jsonTypeInfo;
        if ((jsonTypeInfo = this._ChatHandOffOutcome) == null)
        {
          jsonTypeInfo = (this._ChatHandOffOutcome = (JsonTypeInfo<ChatHandOffOutcome>)base.Options.GetTypeInfo(typeof(ChatHandOffOutcome)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010E7 RID: 4327 RVA: 0x00397188 File Offset: 0x00397188
    private JsonTypeInfo<ChatHandOffOutcome> Create_ChatHandOffOutcome(JsonSerializerOptions options)
    {
      JsonTypeInfo<ChatHandOffOutcome> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ChatHandOffOutcome>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = NotchChatJsonContext.ExpandConverter(typeof(ChatHandOffOutcome), new JsonStringEnumConverter<ChatHandOffOutcome>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ChatHandOffOutcome>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003F2 RID: 1010
    // (get) Token: 0x060010E8 RID: 4328 RVA: 0x003971C8 File Offset: 0x003971C8
    [Nullable(0)]
    public JsonTypeInfo<ChatHandOffOutcome?> NullableChatHandOffOutcome
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ChatHandOffOutcome?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableChatHandOffOutcome) == null)
        {
          jsonTypeInfo = (this._NullableChatHandOffOutcome = (JsonTypeInfo<ChatHandOffOutcome?>)base.Options.GetTypeInfo(typeof(ChatHandOffOutcome?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010E9 RID: 4329 RVA: 0x00397204 File Offset: 0x00397204
    private JsonTypeInfo<ChatHandOffOutcome?> Create_NullableChatHandOffOutcome(JsonSerializerOptions options)
    {
      JsonTypeInfo<ChatHandOffOutcome?> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ChatHandOffOutcome?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<ChatHandOffOutcome>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ChatHandOffOutcome?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003F3 RID: 1011
    // (get) Token: 0x060010EA RID: 4330 RVA: 0x00397234 File Offset: 0x00397234
    [Nullable(0)]
    public JsonTypeInfo<ConversationMessage> ConversationMessage
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ConversationMessage> jsonTypeInfo;
        if ((jsonTypeInfo = this._ConversationMessage) == null)
        {
          jsonTypeInfo = (this._ConversationMessage = (JsonTypeInfo<ConversationMessage>)base.Options.GetTypeInfo(typeof(ConversationMessage)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010EB RID: 4331 RVA: 0x00397270 File Offset: 0x00397270
    private JsonTypeInfo<ConversationMessage> Create_ConversationMessage(JsonSerializerOptions options)
    {
      JsonTypeInfo<ConversationMessage> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ConversationMessage>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ConversationMessage> jsonObjectInfoValues = new JsonObjectInfoValues<ConversationMessage>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ConversationMessage
        {
          Id = (string)args[0],
          ServerId = (long?)args[1],
          Role = (string)args[2],
          Content = (string)args[3],
          Source = (string)args[4],
          CreatedAt = (DateTimeOffset)args[5],
          AttachmentPaths = (IReadOnlyList<string>)args[6],
          BubbleSegments = (IReadOnlyList<string>)args[7],
          ReplacedLocalId = (string)args[8],
          SentToAgentName = (string)args[9]
        };
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => NotchChatJsonContext.ConversationMessagePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = NotchChatJsonContext.<>O.<2>__ConversationMessageCtorParamInit) == null)
        {
          func = (NotchChatJsonContext.<>O.<2>__ConversationMessageCtorParamInit = new Func<JsonParameterInfoValues[]>(NotchChatJsonContext.ConversationMessageCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ConversationMessage).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ConversationMessage>(this.ConversationMessageSerializeHandler);
        JsonObjectInfoValues<ConversationMessage> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ConversationMessage>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010EC RID: 4332 RVA: 0x0039735C File Offset: 0x0039735C
    private static JsonPropertyInfo[] ConversationMessagePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[18];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ConversationMessage)obj).Id;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Id";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsRequired = true;
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<long?> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<long?>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ConversationMessage)obj).ServerId;
      jsonPropertyInfoValues3.Setter = delegate(object obj, long? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ServerId";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("ServerId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<long?> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<long?>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ConversationMessage)obj).Role;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Role";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("Role", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsRequired = true;
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ConversationMessage)obj).Content;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Content";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("Content", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsRequired = true;
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ConversationMessage)obj).Source;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Source";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("Source", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<DateTimeOffset>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ConversationMessage)obj).CreatedAt;
      jsonPropertyInfoValues11.Setter = delegate(object obj, DateTimeOffset value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "CreatedAt";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues12);
      array[5].IsRequired = true;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ConversationMessage)obj).AttachmentPaths;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "AttachmentPaths";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("AttachmentPaths", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((ConversationMessage)obj).BubbleSegments;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "BubbleSegments";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("BubbleSegments", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((ConversationMessage)obj).ReplacedLocalId;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "ReplacedLocalId";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("ReplacedLocalId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((ConversationMessage)obj).SentToAgentName;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "SentToAgentName";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("SentToAgentName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = null;
      jsonPropertyInfoValues21.Setter = null;
      jsonPropertyInfoValues21.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "IsPending";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("IsPending", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = null;
      jsonPropertyInfoValues23.Setter = null;
      jsonPropertyInfoValues23.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "IsUser";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("IsUser", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = null;
      jsonPropertyInfoValues25.Setter = null;
      jsonPropertyInfoValues25.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "RowId";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("RowId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues26);
      array[12].IsGetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues27.IsProperty = true;
      jsonPropertyInfoValues27.IsPublic = true;
      jsonPropertyInfoValues27.IsVirtual = false;
      jsonPropertyInfoValues27.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues27.Converter = null;
      jsonPropertyInfoValues27.Getter = null;
      jsonPropertyInfoValues27.Setter = null;
      jsonPropertyInfoValues27.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues27.HasJsonInclude = false;
      jsonPropertyInfoValues27.IsExtensionData = false;
      jsonPropertyInfoValues27.NumberHandling = null;
      jsonPropertyInfoValues27.PropertyName = "BubbleTexts";
      jsonPropertyInfoValues27.JsonPropertyName = null;
      jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("BubbleTexts", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
      array[13] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues28);
      array[13].IsGetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues29.IsProperty = true;
      jsonPropertyInfoValues29.IsPublic = true;
      jsonPropertyInfoValues29.IsVirtual = false;
      jsonPropertyInfoValues29.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues29.Converter = null;
      jsonPropertyInfoValues29.Getter = null;
      jsonPropertyInfoValues29.Setter = null;
      jsonPropertyInfoValues29.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues29.HasJsonInclude = false;
      jsonPropertyInfoValues29.IsExtensionData = false;
      jsonPropertyInfoValues29.NumberHandling = null;
      jsonPropertyInfoValues29.PropertyName = "BubbleIsOnlyAboutAttachments";
      jsonPropertyInfoValues29.JsonPropertyName = null;
      jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("BubbleIsOnlyAboutAttachments", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
      array[14] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues30);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues31 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues31.IsProperty = true;
      jsonPropertyInfoValues31.IsPublic = true;
      jsonPropertyInfoValues31.IsVirtual = false;
      jsonPropertyInfoValues31.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues31.Converter = null;
      jsonPropertyInfoValues31.Getter = null;
      jsonPropertyInfoValues31.Setter = null;
      jsonPropertyInfoValues31.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues31.HasJsonInclude = false;
      jsonPropertyInfoValues31.IsExtensionData = false;
      jsonPropertyInfoValues31.NumberHandling = null;
      jsonPropertyInfoValues31.PropertyName = "TypedWords";
      jsonPropertyInfoValues31.JsonPropertyName = null;
      jsonPropertyInfoValues31.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("TypedWords", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues32 = jsonPropertyInfoValues31;
      array[15] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues32);
      array[15].IsGetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues33 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues33.IsProperty = true;
      jsonPropertyInfoValues33.IsPublic = true;
      jsonPropertyInfoValues33.IsVirtual = false;
      jsonPropertyInfoValues33.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues33.Converter = null;
      jsonPropertyInfoValues33.Getter = null;
      jsonPropertyInfoValues33.Setter = null;
      jsonPropertyInfoValues33.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues33.HasJsonInclude = false;
      jsonPropertyInfoValues33.IsExtensionData = false;
      jsonPropertyInfoValues33.NumberHandling = null;
      jsonPropertyInfoValues33.PropertyName = "BubbleText";
      jsonPropertyInfoValues33.JsonPropertyName = null;
      jsonPropertyInfoValues33.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("BubbleText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues34 = jsonPropertyInfoValues33;
      array[16] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues34);
      array[16].IsGetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues35 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues35.IsProperty = true;
      jsonPropertyInfoValues35.IsPublic = true;
      jsonPropertyInfoValues35.IsVirtual = false;
      jsonPropertyInfoValues35.DeclaringType = typeof(ConversationMessage);
      jsonPropertyInfoValues35.Converter = null;
      jsonPropertyInfoValues35.Getter = null;
      jsonPropertyInfoValues35.Setter = null;
      jsonPropertyInfoValues35.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues35.HasJsonInclude = false;
      jsonPropertyInfoValues35.IsExtensionData = false;
      jsonPropertyInfoValues35.NumberHandling = null;
      jsonPropertyInfoValues35.PropertyName = "ActionCaption";
      jsonPropertyInfoValues35.JsonPropertyName = null;
      jsonPropertyInfoValues35.AttributeProviderFactory = () => typeof(ConversationMessage).GetProperty("ActionCaption", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues36 = jsonPropertyInfoValues35;
      array[17] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues36);
      return array;
    }

    // Token: 0x060010ED RID: 4333 RVA: 0x003982B0 File Offset: 0x003982B0
    private void ConversationMessageSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ConversationMessage value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string id = value.Id;
      if (id != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Id, id);
      }
      long? serverId = value.ServerId;
      if (serverId != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_ServerId);
        JsonSerializer.Serialize<long?>(writer, serverId, this.NullableInt64);
      }
      string role = value.Role;
      if (role != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Role, role);
      }
      string content = value.Content;
      if (content != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Content, content);
      }
      string source = value.Source;
      if (source != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_Source, source);
      }
      writer.WriteString(NotchChatJsonContext.PropName_CreatedAt, value.CreatedAt);
      IReadOnlyList<string> attachmentPaths = value.AttachmentPaths;
      if (attachmentPaths != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_AttachmentPaths);
        this.IReadOnlyListStringSerializeHandler(writer, attachmentPaths);
      }
      IReadOnlyList<string> bubbleSegments = value.BubbleSegments;
      if (bubbleSegments != null)
      {
        writer.WritePropertyName(NotchChatJsonContext.PropName_BubbleSegments);
        this.IReadOnlyListStringSerializeHandler(writer, bubbleSegments);
      }
      string replacedLocalId = value.ReplacedLocalId;
      if (replacedLocalId != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_ReplacedLocalId, replacedLocalId);
      }
      string sentToAgentName = value.SentToAgentName;
      if (sentToAgentName != null)
      {
        writer.WriteString(NotchChatJsonContext.PropName_SentToAgentName, sentToAgentName);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060010EE RID: 4334 RVA: 0x003983DC File Offset: 0x003983DC
    private static JsonParameterInfoValues[] ConversationMessageCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Id",
          ParameterType = typeof(string),
          Position = 0,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ServerId",
          ParameterType = typeof(long?),
          Position = 1,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Role",
          ParameterType = typeof(string),
          Position = 2,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Content",
          ParameterType = typeof(string),
          Position = 3,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Source",
          ParameterType = typeof(string),
          Position = 4,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "CreatedAt",
          ParameterType = typeof(DateTimeOffset),
          Position = 5,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "AttachmentPaths",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 6,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "BubbleSegments",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 7,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ReplacedLocalId",
          ParameterType = typeof(string),
          Position = 8,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "SentToAgentName",
          ParameterType = typeof(string),
          Position = 9,
          IsNullable = true,
          IsMemberInitializer = true
        }
      };
    }

    // Token: 0x170003F4 RID: 1012
    // (get) Token: 0x060010EF RID: 4335 RVA: 0x00398624 File Offset: 0x00398624
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<ConversationMessage>> IReadOnlyListConversationMessage
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<ConversationMessage>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListConversationMessage) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListConversationMessage = (JsonTypeInfo<IReadOnlyList<ConversationMessage>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<ConversationMessage>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010F0 RID: 4336 RVA: 0x00398660 File Offset: 0x00398660
    private JsonTypeInfo<IReadOnlyList<ConversationMessage>> Create_IReadOnlyListConversationMessage(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<ConversationMessage>> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<ConversationMessage>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<ConversationMessage>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<ConversationMessage>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<ConversationMessage>>(this.IReadOnlyListConversationMessageSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<ConversationMessage>, ConversationMessage>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010F1 RID: 4337 RVA: 0x003986B8 File Offset: 0x003986B8
    private void IReadOnlyListConversationMessageSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ConversationMessage> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (ConversationMessage conversationMessage in value)
      {
        this.ConversationMessageSerializeHandler(writer, conversationMessage);
      }
      writer.WriteEndArray();
    }

    // Token: 0x170003F5 RID: 1013
    // (get) Token: 0x060010F2 RID: 4338 RVA: 0x00398718 File Offset: 0x00398718
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<string>> IReadOnlyListString
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<string>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListString) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListString = (JsonTypeInfo<IReadOnlyList<string>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<string>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010F3 RID: 4339 RVA: 0x00398754 File Offset: 0x00398754
    private JsonTypeInfo<IReadOnlyList<string>> Create_IReadOnlyListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<string>> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<string>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<string>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<string>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<string>>(this.IReadOnlyListStringSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<string>, string>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010F4 RID: 4340 RVA: 0x003987AC File Offset: 0x003987AC
    private void IReadOnlyListStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (string text in value)
      {
        writer.WriteStringValue(text);
      }
      writer.WriteEndArray();
    }

    // Token: 0x170003F6 RID: 1014
    // (get) Token: 0x060010F5 RID: 4341 RVA: 0x0039880C File Offset: 0x0039880C
    [Nullable(0)]
    public JsonTypeInfo<List<ChatHandOff>> ListChatHandOff
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<ChatHandOff>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListChatHandOff) == null)
        {
          jsonTypeInfo = (this._ListChatHandOff = (JsonTypeInfo<List<ChatHandOff>>)base.Options.GetTypeInfo(typeof(List<ChatHandOff>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010F6 RID: 4342 RVA: 0x00398848 File Offset: 0x00398848
    private JsonTypeInfo<List<ChatHandOff>> Create_ListChatHandOff(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<ChatHandOff>> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<ChatHandOff>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<ChatHandOff>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<ChatHandOff>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<ChatHandOff>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<ChatHandOff>>(this.ListChatHandOffSerializeHandler);
        JsonCollectionInfoValues<List<ChatHandOff>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<ChatHandOff>, ChatHandOff>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060010F7 RID: 4343 RVA: 0x003988BC File Offset: 0x003988BC
    private void ListChatHandOffSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<ChatHandOff> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.ChatHandOffSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x170003F7 RID: 1015
    // (get) Token: 0x060010F8 RID: 4344 RVA: 0x00398900 File Offset: 0x00398900
    [Nullable(0)]
    public JsonTypeInfo<DateTimeOffset> DateTimeOffset
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DateTimeOffset> jsonTypeInfo;
        if ((jsonTypeInfo = this._DateTimeOffset) == null)
        {
          jsonTypeInfo = (this._DateTimeOffset = (JsonTypeInfo<DateTimeOffset>)base.Options.GetTypeInfo(typeof(DateTimeOffset)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010F9 RID: 4345 RVA: 0x0039893C File Offset: 0x0039893C
    private JsonTypeInfo<DateTimeOffset> Create_DateTimeOffset(JsonSerializerOptions options)
    {
      JsonTypeInfo<DateTimeOffset> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DateTimeOffset>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<DateTimeOffset>(options, JsonMetadataServices.DateTimeOffsetConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003F8 RID: 1016
    // (get) Token: 0x060010FA RID: 4346 RVA: 0x00398968 File Offset: 0x00398968
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

    // Token: 0x060010FB RID: 4347 RVA: 0x003989A4 File Offset: 0x003989A4
    private JsonTypeInfo<JsonObject> Create_JsonObject(JsonSerializerOptions options)
    {
      JsonTypeInfo<JsonObject> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<JsonObject>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<JsonObject>(options, JsonMetadataServices.JsonObjectConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003F9 RID: 1017
    // (get) Token: 0x060010FC RID: 4348 RVA: 0x003989D0 File Offset: 0x003989D0
    [Nullable(0)]
    public JsonTypeInfo<long> Int64
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<long> jsonTypeInfo;
        if ((jsonTypeInfo = this._Int64) == null)
        {
          jsonTypeInfo = (this._Int64 = (JsonTypeInfo<long>)base.Options.GetTypeInfo(typeof(long)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010FD RID: 4349 RVA: 0x00398A0C File Offset: 0x00398A0C
    private JsonTypeInfo<long> Create_Int64(JsonSerializerOptions options)
    {
      JsonTypeInfo<long> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<long>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<long>(options, JsonMetadataServices.Int64Converter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003FA RID: 1018
    // (get) Token: 0x060010FE RID: 4350 RVA: 0x00398A38 File Offset: 0x00398A38
    [Nullable(0)]
    public JsonTypeInfo<long?> NullableInt64
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<long?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableInt64) == null)
        {
          jsonTypeInfo = (this._NullableInt64 = (JsonTypeInfo<long?>)base.Options.GetTypeInfo(typeof(long?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060010FF RID: 4351 RVA: 0x00398A74 File Offset: 0x00398A74
    private JsonTypeInfo<long?> Create_NullableInt64(JsonSerializerOptions options)
    {
      JsonTypeInfo<long?> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<long?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<long>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<long?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003FB RID: 1019
    // (get) Token: 0x06001100 RID: 4352 RVA: 0x00398AA4 File Offset: 0x00398AA4
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

    // Token: 0x06001101 RID: 4353 RVA: 0x00398AE0 File Offset: 0x00398AE0
    private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
    {
      JsonTypeInfo<string> jsonTypeInfo;
      if (!NotchChatJsonContext.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x170003FC RID: 1020
    // (get) Token: 0x06001102 RID: 4354 RVA: 0x00398B0C File Offset: 0x00398B0C
    public static NotchChatJsonContext Default { get; } = new NotchChatJsonContext(new JsonSerializerOptions(NotchChatJsonContext.s_defaultOptions));

    // Token: 0x170003FD RID: 1021
    // (get) Token: 0x06001103 RID: 4355 RVA: 0x00398B14 File Offset: 0x00398B14
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = NotchChatJsonContext.s_defaultOptions;

    // Token: 0x06001104 RID: 4356 RVA: 0x00398B1C File Offset: 0x00398B1C
    public NotchChatJsonContext()
      : base(null)
    {
    }

    // Token: 0x06001105 RID: 4357 RVA: 0x00398B30 File Offset: 0x00398B30
    public NotchChatJsonContext(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x06001106 RID: 4358 RVA: 0x00398B44 File Offset: 0x00398B44
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = NotchChatJsonContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x06001107 RID: 4359 RVA: 0x00398B74 File Offset: 0x00398B74
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return NotchChatJsonContext.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x06001108 RID: 4360 RVA: 0x00398BBC File Offset: 0x00398BBC
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

    // Token: 0x06001109 RID: 4361 RVA: 0x00398C24 File Offset: 0x00398C24
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x0600110A RID: 4362 RVA: 0x00398C44 File Offset: 0x00398C44
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
      if (type == typeof(CachedConversation))
      {
        return this.Create_CachedConversation(options);
      }
      if (type == typeof(ChatHandOff))
      {
        return this.Create_ChatHandOff(options);
      }
      if (type == typeof(ChatHandOffKind))
      {
        return this.Create_ChatHandOffKind(options);
      }
      if (type == typeof(ChatHandOffOutcome))
      {
        return this.Create_ChatHandOffOutcome(options);
      }
      if (type == typeof(ChatHandOffOutcome?))
      {
        return this.Create_NullableChatHandOffOutcome(options);
      }
      if (type == typeof(ConversationMessage))
      {
        return this.Create_ConversationMessage(options);
      }
      if (type == typeof(IReadOnlyList<ConversationMessage>))
      {
        return this.Create_IReadOnlyListConversationMessage(options);
      }
      if (type == typeof(IReadOnlyList<string>))
      {
        return this.Create_IReadOnlyListString(options);
      }
      if (type == typeof(List<ChatHandOff>))
      {
        return this.Create_ListChatHandOff(options);
      }
      if (type == typeof(DateTimeOffset))
      {
        return this.Create_DateTimeOffset(options);
      }
      if (type == typeof(JsonObject))
      {
        return this.Create_JsonObject(options);
      }
      if (type == typeof(long))
      {
        return this.Create_Int64(options);
      }
      if (type == typeof(long?))
      {
        return this.Create_NullableInt64(options);
      }
      if (type == typeof(string))
      {
        return this.Create_String(options);
      }
      return null;
    }

    // Token: 0x04000737 RID: 1847
    [Nullable(2)]
    private JsonTypeInfo<bool> _Boolean;

    // Token: 0x04000738 RID: 1848
    [Nullable(2)]
    private JsonTypeInfo<bool?> _NullableBoolean;

    // Token: 0x04000739 RID: 1849
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<CachedConversation> _CachedConversation;

    // Token: 0x0400073A RID: 1850
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ChatHandOff> _ChatHandOff;

    // Token: 0x0400073B RID: 1851
    [Nullable(2)]
    private JsonTypeInfo<ChatHandOffKind> _ChatHandOffKind;

    // Token: 0x0400073C RID: 1852
    [Nullable(2)]
    private JsonTypeInfo<ChatHandOffOutcome> _ChatHandOffOutcome;

    // Token: 0x0400073D RID: 1853
    [Nullable(2)]
    private JsonTypeInfo<ChatHandOffOutcome?> _NullableChatHandOffOutcome;

    // Token: 0x0400073E RID: 1854
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ConversationMessage> _ConversationMessage;

    // Token: 0x0400073F RID: 1855
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<ConversationMessage>> _IReadOnlyListConversationMessage;

    // Token: 0x04000740 RID: 1856
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<string>> _IReadOnlyListString;

    // Token: 0x04000741 RID: 1857
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<ChatHandOff>> _ListChatHandOff;

    // Token: 0x04000742 RID: 1858
    [Nullable(2)]
    private JsonTypeInfo<DateTimeOffset> _DateTimeOffset;

    // Token: 0x04000743 RID: 1859
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<JsonObject> _JsonObject;

    // Token: 0x04000744 RID: 1860
    [Nullable(2)]
    private JsonTypeInfo<long> _Int64;

    // Token: 0x04000745 RID: 1861
    [Nullable(2)]
    private JsonTypeInfo<long?> _NullableInt64;

    // Token: 0x04000746 RID: 1862
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<string> _String;

    // Token: 0x04000747 RID: 1863
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Token: 0x04000748 RID: 1864
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    // Token: 0x0400074B RID: 1867
    private static readonly JsonEncodedText PropName_Messages = JsonEncodedText.Encode("Messages", null);

    // Token: 0x0400074C RID: 1868
    private static readonly JsonEncodedText PropName_HasEarlierMessages = JsonEncodedText.Encode("HasEarlierMessages", null);

    // Token: 0x0400074D RID: 1869
    private static readonly JsonEncodedText PropName_Id = JsonEncodedText.Encode("Id", null);

    // Token: 0x0400074E RID: 1870
    private static readonly JsonEncodedText PropName_Kind = JsonEncodedText.Encode("Kind", null);

    // Token: 0x0400074F RID: 1871
    private static readonly JsonEncodedText PropName_JobId = JsonEncodedText.Encode("JobId", null);

    // Token: 0x04000750 RID: 1872
    private static readonly JsonEncodedText PropName_ClickySlug = JsonEncodedText.Encode("ClickySlug", null);

    // Token: 0x04000751 RID: 1873
    private static readonly JsonEncodedText PropName_Title = JsonEncodedText.Encode("Title", null);

    // Token: 0x04000752 RID: 1874
    private static readonly JsonEncodedText PropName_CreatedAt = JsonEncodedText.Encode("CreatedAt", null);

    // Token: 0x04000753 RID: 1875
    private static readonly JsonEncodedText PropName_IsFollowUp = JsonEncodedText.Encode("IsFollowUp", null);

    // Token: 0x04000754 RID: 1876
    private static readonly JsonEncodedText PropName_IsSentFromWindow = JsonEncodedText.Encode("IsSentFromWindow", null);

    // Token: 0x04000755 RID: 1877
    private static readonly JsonEncodedText PropName_Outcome = JsonEncodedText.Encode("Outcome", null);

    // Token: 0x04000756 RID: 1878
    private static readonly JsonEncodedText PropName_ReplyText = JsonEncodedText.Encode("ReplyText", null);

    // Token: 0x04000757 RID: 1879
    private static readonly JsonEncodedText PropName_LessonId = JsonEncodedText.Encode("LessonId", null);

    // Token: 0x04000758 RID: 1880
    private static readonly JsonEncodedText PropName_DeliveredText = JsonEncodedText.Encode("DeliveredText", null);

    // Token: 0x04000759 RID: 1881
    private static readonly JsonEncodedText PropName_DeliveredToApp = JsonEncodedText.Encode("DeliveredToApp", null);

    // Token: 0x0400075A RID: 1882
    private static readonly JsonEncodedText PropName_WasCopied = JsonEncodedText.Encode("WasCopied", null);

    // Token: 0x0400075B RID: 1883
    private static readonly JsonEncodedText PropName_FullReply = JsonEncodedText.Encode("FullReply", null);

    // Token: 0x0400075C RID: 1884
    private static readonly JsonEncodedText PropName_ArtifactEntries = JsonEncodedText.Encode("ArtifactEntries", null);

    // Token: 0x0400075D RID: 1885
    private static readonly JsonEncodedText PropName_Answer = JsonEncodedText.Encode("Answer", null);

    // Token: 0x0400075E RID: 1886
    private static readonly JsonEncodedText PropName_ThreadId = JsonEncodedText.Encode("ThreadId", null);

    // Token: 0x0400075F RID: 1887
    private static readonly JsonEncodedText PropName_Card = JsonEncodedText.Encode("Card", null);

    // Token: 0x04000760 RID: 1888
    private static readonly JsonEncodedText PropName_IsRoutineRun = JsonEncodedText.Encode("IsRoutineRun", null);

    // Token: 0x04000761 RID: 1889
    private static readonly JsonEncodedText PropName_TaskTitle = JsonEncodedText.Encode("TaskTitle", null);

    // Token: 0x04000762 RID: 1890
    private static readonly JsonEncodedText PropName_FollowUps = JsonEncodedText.Encode("FollowUps", null);

    // Token: 0x04000763 RID: 1891
    private static readonly JsonEncodedText PropName_SentText = JsonEncodedText.Encode("SentText", null);

    // Token: 0x04000764 RID: 1892
    private static readonly JsonEncodedText PropName_SentFiles = JsonEncodedText.Encode("SentFiles", null);

    // Token: 0x04000765 RID: 1893
    private static readonly JsonEncodedText PropName_SitesRead = JsonEncodedText.Encode("SitesRead", null);

    // Token: 0x04000766 RID: 1894
    private static readonly JsonEncodedText PropName_ServerId = JsonEncodedText.Encode("ServerId", null);

    // Token: 0x04000767 RID: 1895
    private static readonly JsonEncodedText PropName_Role = JsonEncodedText.Encode("Role", null);

    // Token: 0x04000768 RID: 1896
    private static readonly JsonEncodedText PropName_Content = JsonEncodedText.Encode("Content", null);

    // Token: 0x04000769 RID: 1897
    private static readonly JsonEncodedText PropName_Source = JsonEncodedText.Encode("Source", null);

    // Token: 0x0400076A RID: 1898
    private static readonly JsonEncodedText PropName_AttachmentPaths = JsonEncodedText.Encode("AttachmentPaths", null);

    // Token: 0x0400076B RID: 1899
    private static readonly JsonEncodedText PropName_BubbleSegments = JsonEncodedText.Encode("BubbleSegments", null);

    // Token: 0x0400076C RID: 1900
    private static readonly JsonEncodedText PropName_ReplacedLocalId = JsonEncodedText.Encode("ReplacedLocalId", null);

    // Token: 0x0400076D RID: 1901
    private static readonly JsonEncodedText PropName_SentToAgentName = JsonEncodedText.Encode("SentToAgentName", null);

    // Token: 0x0200042D RID: 1069
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001BDC RID: 7132
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <0>__CachedConversationCtorParamInit;

      // Token: 0x04001BDD RID: 7133
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <1>__ChatHandOffCtorParamInit;

      // Token: 0x04001BDE RID: 7134
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <2>__ConversationMessageCtorParamInit;
    }
  }
}
