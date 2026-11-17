using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000D9 RID: 217
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNameCaseInsensitive = true, UseStringEnumConverter = true, WriteIndented = true)]
  [JsonSerializable(typeof(Dictionary<string, string>))]
  [JsonSerializable(typeof(ProactiveStoreData))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  internal sealed class ProactiveJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x1700030B RID: 779
    // (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00383DDC File Offset: 0x00383DDC
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

    // Token: 0x06000DC8 RID: 3528 RVA: 0x00383E18 File Offset: 0x00383E18
    private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700030C RID: 780
    // (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00383E44 File Offset: 0x00383E44
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

    // Token: 0x06000DCA RID: 3530 RVA: 0x00383E80 File Offset: 0x00383E80
    private JsonTypeInfo<bool?> Create_NullableBoolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool?> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<bool>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700030D RID: 781
    // (get) Token: 0x06000DCB RID: 3531 RVA: 0x00383EB0 File Offset: 0x00383EB0
    [Nullable(0)]
    public JsonTypeInfo<MorningDismissalHistory> MorningDismissalHistory
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<MorningDismissalHistory> jsonTypeInfo;
        if ((jsonTypeInfo = this._MorningDismissalHistory) == null)
        {
          jsonTypeInfo = (this._MorningDismissalHistory = (JsonTypeInfo<MorningDismissalHistory>)base.Options.GetTypeInfo(typeof(MorningDismissalHistory)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DCC RID: 3532 RVA: 0x00383EEC File Offset: 0x00383EEC
    private JsonTypeInfo<MorningDismissalHistory> Create_MorningDismissalHistory(JsonSerializerOptions options)
    {
      JsonTypeInfo<MorningDismissalHistory> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<MorningDismissalHistory>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<MorningDismissalHistory> jsonObjectInfoValues = new JsonObjectInfoValues<MorningDismissalHistory>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new MorningDismissalHistory((List<DateTime>)args[0], (DateTimeOffset?)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.MorningDismissalHistoryPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<0>__MorningDismissalHistoryCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<0>__MorningDismissalHistoryCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.MorningDismissalHistoryCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(MorningDismissalHistory).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(List<DateTime>),
          typeof(DateTimeOffset?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, MorningDismissalHistory>(this.MorningDismissalHistorySerializeHandler);
        JsonObjectInfoValues<MorningDismissalHistory> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<MorningDismissalHistory>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DCD RID: 3533 RVA: 0x00383FD8 File Offset: 0x00383FD8
    private static JsonPropertyInfo[] MorningDismissalHistoryPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<List<DateTime>> jsonPropertyInfoValues = new JsonPropertyInfoValues<List<DateTime>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(MorningDismissalHistory);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((MorningDismissalHistory)obj).DismissedDays;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] List<DateTime> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "DismissedDays";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(MorningDismissalHistory).GetProperty("DismissedDays", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<DateTime>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<DateTime>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<List<DateTime>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(MorningDismissalHistory);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((MorningDismissalHistory)obj).RestingUntil;
      jsonPropertyInfoValues3.Setter = delegate(object obj, DateTimeOffset? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "RestingUntil";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(MorningDismissalHistory).GetProperty("RestingUntil", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x06000DCE RID: 3534 RVA: 0x003841D0 File Offset: 0x003841D0
    private void MorningDismissalHistorySerializeHandler(Utf8JsonWriter writer, [Nullable(2)] MorningDismissalHistory value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      List<DateTime> dismissedDays = value.DismissedDays;
      if (dismissedDays != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_dismissed_days);
        this.ListDateTimeSerializeHandler(writer, dismissedDays);
      }
      DateTimeOffset? restingUntil = value.RestingUntil;
      if (restingUntil != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_resting_until);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, restingUntil, this.NullableDateTimeOffset);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DCF RID: 3535 RVA: 0x00384238 File Offset: 0x00384238
    private static JsonParameterInfoValues[] MorningDismissalHistoryCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "DismissedDays",
          ParameterType = typeof(List<DateTime>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "RestingUntil",
          ParameterType = typeof(DateTimeOffset?),
          Position = 1,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700030E RID: 782
    // (get) Token: 0x06000DD0 RID: 3536 RVA: 0x003842CC File Offset: 0x003842CC
    [Nullable(0)]
    public JsonTypeInfo<PendingSuggestionJob> PendingSuggestionJob
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PendingSuggestionJob> jsonTypeInfo;
        if ((jsonTypeInfo = this._PendingSuggestionJob) == null)
        {
          jsonTypeInfo = (this._PendingSuggestionJob = (JsonTypeInfo<PendingSuggestionJob>)base.Options.GetTypeInfo(typeof(PendingSuggestionJob)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DD1 RID: 3537 RVA: 0x00384308 File Offset: 0x00384308
    private JsonTypeInfo<PendingSuggestionJob> Create_PendingSuggestionJob(JsonSerializerOptions options)
    {
      JsonTypeInfo<PendingSuggestionJob> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PendingSuggestionJob>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PendingSuggestionJob> jsonObjectInfoValues = new JsonObjectInfoValues<PendingSuggestionJob>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PendingSuggestionJob((string)args[0], (DateTimeOffset)args[1], (ProactiveCheckReason)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.PendingSuggestionJobPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<1>__PendingSuggestionJobCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<1>__PendingSuggestionJobCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.PendingSuggestionJobCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PendingSuggestionJob).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(DateTimeOffset),
          typeof(ProactiveCheckReason)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PendingSuggestionJob>(this.PendingSuggestionJobSerializeHandler);
        JsonObjectInfoValues<PendingSuggestionJob> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PendingSuggestionJob>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DD2 RID: 3538 RVA: 0x003843F4 File Offset: 0x003843F4
    private static JsonPropertyInfo[] PendingSuggestionJobPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PendingSuggestionJob);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PendingSuggestionJob)obj).JobId;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "JobId";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PendingSuggestionJob).GetProperty("JobId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<DateTimeOffset>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PendingSuggestionJob);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PendingSuggestionJob)obj).StartedAt;
      jsonPropertyInfoValues3.Setter = delegate(object obj, DateTimeOffset value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "StartedAt";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PendingSuggestionJob).GetProperty("StartedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<ProactiveCheckReason> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<ProactiveCheckReason>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PendingSuggestionJob);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PendingSuggestionJob)obj).CheckReason;
      jsonPropertyInfoValues5.Setter = delegate(object obj, ProactiveCheckReason value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "CheckReason";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PendingSuggestionJob).GetProperty("CheckReason", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveCheckReason), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveCheckReason> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<ProactiveCheckReason>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x06000DD3 RID: 3539 RVA: 0x003846D8 File Offset: 0x003846D8
    private void PendingSuggestionJobSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PendingSuggestionJob value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string jobId = value.JobId;
      if (jobId != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_job_id, jobId);
      }
      writer.WriteString(ProactiveJsonContext.PropName_started_at, value.StartedAt);
      writer.WritePropertyName(ProactiveJsonContext.PropName_check_reason);
      JsonSerializer.Serialize<ProactiveCheckReason>(writer, value.CheckReason, this.ProactiveCheckReason);
      writer.WriteEndObject();
    }

    // Token: 0x06000DD4 RID: 3540 RVA: 0x00384740 File Offset: 0x00384740
    private static JsonParameterInfoValues[] PendingSuggestionJobCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "JobId",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "StartedAt",
          ParameterType = typeof(DateTimeOffset),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CheckReason",
          ParameterType = typeof(ProactiveCheckReason),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700030F RID: 783
    // (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00384810 File Offset: 0x00384810
    [Nullable(0)]
    public JsonTypeInfo<ProactiveAccountState> ProactiveAccountState
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveAccountState> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveAccountState) == null)
        {
          jsonTypeInfo = (this._ProactiveAccountState = (JsonTypeInfo<ProactiveAccountState>)base.Options.GetTypeInfo(typeof(ProactiveAccountState)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DD6 RID: 3542 RVA: 0x0038484C File Offset: 0x0038484C
    private JsonTypeInfo<ProactiveAccountState> Create_ProactiveAccountState(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveAccountState> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveAccountState>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveAccountState> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveAccountState>();
        jsonObjectInfoValues.ObjectCreator = () => new ProactiveAccountState();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveAccountStatePropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveAccountState).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveAccountState>(this.ProactiveAccountStateSerializeHandler);
        JsonObjectInfoValues<ProactiveAccountState> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveAccountState>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DD7 RID: 3543 RVA: 0x00384920 File Offset: 0x00384920
    private static JsonPropertyInfo[] ProactiveAccountStatePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<List<ProactiveSuggestionCard>> jsonPropertyInfoValues = new JsonPropertyInfoValues<List<ProactiveSuggestionCard>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveAccountState)obj).Cards;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<ProactiveSuggestionCard> value)
      {
        ((ProactiveAccountState)obj).Cards = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Cards";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("Cards", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<ProactiveSuggestionCard>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<ProactiveSuggestionCard>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<List<ProactiveSuggestionCard>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<List<QueuedProactiveTask>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<List<QueuedProactiveTask>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveAccountState)obj).Queue;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<QueuedProactiveTask> value)
      {
        ((ProactiveAccountState)obj).Queue = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Queue";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("Queue", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<QueuedProactiveTask>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<QueuedProactiveTask>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<List<QueuedProactiveTask>>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<PendingSuggestionJob> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<PendingSuggestionJob>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveAccountState)obj).PendingJob;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] PendingSuggestionJob value)
      {
        ((ProactiveAccountState)obj).PendingJob = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "PendingJob";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("PendingJob", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PendingSuggestionJob), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PendingSuggestionJob> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<PendingSuggestionJob>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<ProactiveSlotDayState> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<ProactiveSlotDayState>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ProactiveAccountState)obj).Slots;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] ProactiveSlotDayState value)
      {
        ((ProactiveAccountState)obj).Slots = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Slots";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("Slots", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveSlotDayState), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveSlotDayState> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<ProactiveSlotDayState>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ProactiveAccountState)obj).IgnoredSuggestionsBreakUntil;
      jsonPropertyInfoValues9.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveAccountState)obj).IgnoredSuggestionsBreakUntil = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "IgnoredSuggestionsBreakUntil";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("IgnoredSuggestionsBreakUntil", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ProactiveAccountState)obj).SuggestionsLastFetchedAt;
      jsonPropertyInfoValues11.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveAccountState)obj).SuggestionsLastFetchedAt = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "SuggestionsLastFetchedAt";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("SuggestionsLastFetchedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ProactiveAccountState);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ProactiveAccountState)obj).SuggestionsRowLastSeenAt;
      jsonPropertyInfoValues13.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveAccountState)obj).SuggestionsRowLastSeenAt = value;
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "SuggestionsRowLastSeenAt";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ProactiveAccountState).GetProperty("SuggestionsRowLastSeenAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues14);
      return array;
    }

    // Token: 0x06000DD8 RID: 3544 RVA: 0x00384FDC File Offset: 0x00384FDC
    private void ProactiveAccountStateSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveAccountState value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      List<ProactiveSuggestionCard> cards = value.Cards;
      if (cards != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_cards);
        this.ListProactiveSuggestionCardSerializeHandler(writer, cards);
      }
      List<QueuedProactiveTask> queue = value.Queue;
      if (queue != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_queue);
        this.ListQueuedProactiveTaskSerializeHandler(writer, queue);
      }
      PendingSuggestionJob pendingJob = value.PendingJob;
      if (pendingJob != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_pending_job);
        this.PendingSuggestionJobSerializeHandler(writer, pendingJob);
      }
      ProactiveSlotDayState slots = value.Slots;
      if (slots != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_slots);
        this.ProactiveSlotDayStateSerializeHandler(writer, slots);
      }
      DateTimeOffset? ignoredSuggestionsBreakUntil = value.IgnoredSuggestionsBreakUntil;
      if (ignoredSuggestionsBreakUntil != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_ignored_suggestions_break_until);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, ignoredSuggestionsBreakUntil, this.NullableDateTimeOffset);
      }
      DateTimeOffset? suggestionsLastFetchedAt = value.SuggestionsLastFetchedAt;
      if (suggestionsLastFetchedAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_suggestions_last_fetched_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, suggestionsLastFetchedAt, this.NullableDateTimeOffset);
      }
      DateTimeOffset? suggestionsRowLastSeenAt = value.SuggestionsRowLastSeenAt;
      if (suggestionsRowLastSeenAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_suggestions_row_last_seen_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, suggestionsRowLastSeenAt, this.NullableDateTimeOffset);
      }
      writer.WriteEndObject();
    }

    // Token: 0x17000310 RID: 784
    // (get) Token: 0x06000DD9 RID: 3545 RVA: 0x003850F4 File Offset: 0x003850F4
    [Nullable(0)]
    public JsonTypeInfo<ProactiveCheckReason> ProactiveCheckReason
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveCheckReason> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveCheckReason) == null)
        {
          jsonTypeInfo = (this._ProactiveCheckReason = (JsonTypeInfo<ProactiveCheckReason>)base.Options.GetTypeInfo(typeof(ProactiveCheckReason)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DDA RID: 3546 RVA: 0x00385130 File Offset: 0x00385130
    private JsonTypeInfo<ProactiveCheckReason> Create_ProactiveCheckReason(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveCheckReason> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveCheckReason>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = ProactiveJsonContext.ExpandConverter(typeof(ProactiveCheckReason), new JsonStringEnumConverter<ProactiveCheckReason>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ProactiveCheckReason>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000311 RID: 785
    // (get) Token: 0x06000DDB RID: 3547 RVA: 0x00385170 File Offset: 0x00385170
    [Nullable(0)]
    public JsonTypeInfo<ProactiveHistoryEntry> ProactiveHistoryEntry
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveHistoryEntry> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveHistoryEntry) == null)
        {
          jsonTypeInfo = (this._ProactiveHistoryEntry = (JsonTypeInfo<ProactiveHistoryEntry>)base.Options.GetTypeInfo(typeof(ProactiveHistoryEntry)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DDC RID: 3548 RVA: 0x003851AC File Offset: 0x003851AC
    private JsonTypeInfo<ProactiveHistoryEntry> Create_ProactiveHistoryEntry(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveHistoryEntry> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveHistoryEntry>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveHistoryEntry> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveHistoryEntry>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ProactiveHistoryEntry((string)args[0], (string)args[1], (ProactiveResolution)args[2], (DateTimeOffset)args[3], (string)args[4], (bool?)args[5]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveHistoryEntryPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<2>__ProactiveHistoryEntryCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<2>__ProactiveHistoryEntryCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.ProactiveHistoryEntryCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(ProactiveResolution),
          typeof(DateTimeOffset),
          typeof(string),
          typeof(bool?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveHistoryEntry>(this.ProactiveHistoryEntrySerializeHandler);
        JsonObjectInfoValues<ProactiveHistoryEntry> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveHistoryEntry>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DDD RID: 3549 RVA: 0x00385298 File Offset: 0x00385298
    private static JsonPropertyInfo[] ProactiveHistoryEntryPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveHistoryEntry)obj).Title;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Title";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveHistoryEntry)obj).Description;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Description";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<ProactiveResolution> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<ProactiveResolution>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveHistoryEntry)obj).Resolution;
      jsonPropertyInfoValues5.Setter = delegate(object obj, ProactiveResolution value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Resolution";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("Resolution", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveResolution), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveResolution> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<ProactiveResolution>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<DateTimeOffset>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ProactiveHistoryEntry)obj).DecidedAt;
      jsonPropertyInfoValues7.Setter = delegate(object obj, DateTimeOffset value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "DecidedAt";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("DecidedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ProactiveHistoryEntry)obj).DenyReason;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "DenyReason";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("DenyReason", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ProactiveHistoryEntry)obj).WasViewed;
      jsonPropertyInfoValues11.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "WasViewed";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("WasViewed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ProactiveHistoryEntry);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ProactiveHistoryEntry)obj).WireStatus;
      jsonPropertyInfoValues13.Setter = null;
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "WireStatus";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ProactiveHistoryEntry).GetProperty("WireStatus", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
      array[6].IsGetNullable = false;
      return array;
    }

    // Token: 0x06000DDE RID: 3550 RVA: 0x0038591C File Offset: 0x0038591C
    private void ProactiveHistoryEntrySerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveHistoryEntry value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_title, title);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_description, description);
      }
      writer.WritePropertyName(ProactiveJsonContext.PropName_resolution);
      JsonSerializer.Serialize<ProactiveResolution>(writer, value.Resolution, this.ProactiveResolution);
      writer.WriteString(ProactiveJsonContext.PropName_decided_at, value.DecidedAt);
      string denyReason = value.DenyReason;
      if (denyReason != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_deny_reason, denyReason);
      }
      bool? wasViewed = value.WasViewed;
      if (wasViewed != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_was_viewed);
        JsonSerializer.Serialize<bool?>(writer, wasViewed, this.NullableBoolean);
      }
      string wireStatus = value.WireStatus;
      if (wireStatus != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_wire_status, wireStatus);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DDF RID: 3551 RVA: 0x003859F0 File Offset: 0x003859F0
    private static JsonParameterInfoValues[] ProactiveHistoryEntryCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Description",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Resolution",
          ParameterType = typeof(ProactiveResolution),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DecidedAt",
          ParameterType = typeof(DateTimeOffset),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DenyReason",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "WasViewed",
          ParameterType = typeof(bool?),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000312 RID: 786
    // (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00385B80 File Offset: 0x00385B80
    [Nullable(0)]
    public JsonTypeInfo<ProactiveNewClicky> ProactiveNewClicky
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveNewClicky> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveNewClicky) == null)
        {
          jsonTypeInfo = (this._ProactiveNewClicky = (JsonTypeInfo<ProactiveNewClicky>)base.Options.GetTypeInfo(typeof(ProactiveNewClicky)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DE1 RID: 3553 RVA: 0x00385BBC File Offset: 0x00385BBC
    private JsonTypeInfo<ProactiveNewClicky> Create_ProactiveNewClicky(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveNewClicky> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveNewClicky>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveNewClicky> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveNewClicky>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ProactiveNewClicky((string)args[0], (string)args[1], (string)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveNewClickyPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<3>__ProactiveNewClickyCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<3>__ProactiveNewClickyCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.ProactiveNewClickyCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveNewClicky).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveNewClicky>(this.ProactiveNewClickySerializeHandler);
        JsonObjectInfoValues<ProactiveNewClicky> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveNewClicky>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DE2 RID: 3554 RVA: 0x00385CA8 File Offset: 0x00385CA8
    private static JsonPropertyInfo[] ProactiveNewClickyPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveNewClicky);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveNewClicky)obj).Name;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Name";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveNewClicky).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveNewClicky);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveNewClicky)obj).Role;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Role";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveNewClicky).GetProperty("Role", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveNewClicky);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveNewClicky)obj).Description;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Description";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveNewClicky).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000DE3 RID: 3555 RVA: 0x00385FB0 File Offset: 0x00385FB0
    private void ProactiveNewClickySerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveNewClicky value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string name = value.Name;
      if (name != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_name, name);
      }
      string role = value.Role;
      if (role != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_role, role);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_description, description);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DE4 RID: 3556 RVA: 0x00386018 File Offset: 0x00386018
    private static JsonParameterInfoValues[] ProactiveNewClickyCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Name",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Role",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Description",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000313 RID: 787
    // (get) Token: 0x06000DE5 RID: 3557 RVA: 0x003860E8 File Offset: 0x003860E8
    [Nullable(0)]
    public JsonTypeInfo<ProactiveResolution> ProactiveResolution
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveResolution> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveResolution) == null)
        {
          jsonTypeInfo = (this._ProactiveResolution = (JsonTypeInfo<ProactiveResolution>)base.Options.GetTypeInfo(typeof(ProactiveResolution)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DE6 RID: 3558 RVA: 0x00386124 File Offset: 0x00386124
    private JsonTypeInfo<ProactiveResolution> Create_ProactiveResolution(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveResolution> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveResolution>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = ProactiveJsonContext.ExpandConverter(typeof(ProactiveResolution), new JsonStringEnumConverter<ProactiveResolution>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<ProactiveResolution>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000314 RID: 788
    // (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00386164 File Offset: 0x00386164
    [Nullable(0)]
    public JsonTypeInfo<ProactiveRoutine> ProactiveRoutine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveRoutine> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveRoutine) == null)
        {
          jsonTypeInfo = (this._ProactiveRoutine = (JsonTypeInfo<ProactiveRoutine>)base.Options.GetTypeInfo(typeof(ProactiveRoutine)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DE8 RID: 3560 RVA: 0x003861A0 File Offset: 0x003861A0
    private JsonTypeInfo<ProactiveRoutine> Create_ProactiveRoutine(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveRoutine> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveRoutine>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveRoutine> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveRoutine>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ProactiveRoutine((int)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveRoutinePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<4>__ProactiveRoutineCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<4>__ProactiveRoutineCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.ProactiveRoutineCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveRoutine).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(int),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveRoutine>(this.ProactiveRoutineSerializeHandler);
        JsonObjectInfoValues<ProactiveRoutine> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveRoutine>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DE9 RID: 3561 RVA: 0x0038628C File Offset: 0x0038628C
    private static JsonPropertyInfo[] ProactiveRoutinePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[5];
      JsonPropertyInfoValues<int> jsonPropertyInfoValues = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveRoutine);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveRoutine)obj).EveryMinutes;
      jsonPropertyInfoValues.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "EveryMinutes";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveRoutine).GetProperty("EveryMinutes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveRoutine);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveRoutine)obj).Title;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Title";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveRoutine).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveRoutine);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveRoutine)obj).ClampedIntervalMinutes;
      jsonPropertyInfoValues5.Setter = null;
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "ClampedIntervalMinutes";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveRoutine).GetProperty("ClampedIntervalMinutes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ProactiveRoutine);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ProactiveRoutine)obj).CadenceLabel;
      jsonPropertyInfoValues7.Setter = null;
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "CadenceLabel";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ProactiveRoutine).GetProperty("CadenceLabel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ProactiveRoutine);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ProactiveRoutine)obj).CadenceSpokenLabel;
      jsonPropertyInfoValues9.Setter = null;
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "CadenceSpokenLabel";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ProactiveRoutine).GetProperty("CadenceSpokenLabel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      return array;
    }

    // Token: 0x06000DEA RID: 3562 RVA: 0x00386704 File Offset: 0x00386704
    private void ProactiveRoutineSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveRoutine value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteNumber(ProactiveJsonContext.PropName_every_minutes, value.EveryMinutes);
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_title, title);
      }
      writer.WriteNumber(ProactiveJsonContext.PropName_clamped_interval_minutes, value.ClampedIntervalMinutes);
      string cadenceLabel = value.CadenceLabel;
      if (cadenceLabel != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_cadence_label, cadenceLabel);
      }
      string cadenceSpokenLabel = value.CadenceSpokenLabel;
      if (cadenceSpokenLabel != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_cadence_spoken_label, cadenceSpokenLabel);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DEB RID: 3563 RVA: 0x0038678C File Offset: 0x0038678C
    private static JsonParameterInfoValues[] ProactiveRoutineCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "EveryMinutes",
          ParameterType = typeof(int),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000315 RID: 789
    // (get) Token: 0x06000DEC RID: 3564 RVA: 0x00386820 File Offset: 0x00386820
    [Nullable(0)]
    public JsonTypeInfo<ProactiveSlotDayState> ProactiveSlotDayState
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveSlotDayState> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveSlotDayState) == null)
        {
          jsonTypeInfo = (this._ProactiveSlotDayState = (JsonTypeInfo<ProactiveSlotDayState>)base.Options.GetTypeInfo(typeof(ProactiveSlotDayState)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DED RID: 3565 RVA: 0x0038685C File Offset: 0x0038685C
    private JsonTypeInfo<ProactiveSlotDayState> Create_ProactiveSlotDayState(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveSlotDayState> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveSlotDayState>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveSlotDayState> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveSlotDayState>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ProactiveSlotDayState((DateTimeOffset?)args[0], (DateTimeOffset?)args[1], (int?)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveSlotDayStatePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<5>__ProactiveSlotDayStateCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<5>__ProactiveSlotDayStateCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.ProactiveSlotDayStateCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveSlotDayState).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(DateTimeOffset?),
          typeof(DateTimeOffset?),
          typeof(int?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveSlotDayState>(this.ProactiveSlotDayStateSerializeHandler);
        JsonObjectInfoValues<ProactiveSlotDayState> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveSlotDayState>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DEE RID: 3566 RVA: 0x00386948 File Offset: 0x00386948
    private static JsonPropertyInfo[] ProactiveSlotDayStatePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveSlotDayState);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveSlotDayState)obj).MorningFiredAt;
      jsonPropertyInfoValues.Setter = delegate(object obj, DateTimeOffset? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "MorningFiredAt";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveSlotDayState).GetProperty("MorningFiredAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveSlotDayState);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveSlotDayState)obj).RetryAfter;
      jsonPropertyInfoValues3.Setter = delegate(object obj, DateTimeOffset? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "RetryAfter";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveSlotDayState).GetProperty("RetryAfter", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveSlotDayState);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveSlotDayState)obj).ConsecutiveFailedAttempts;
      jsonPropertyInfoValues5.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "ConsecutiveFailedAttempts";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveSlotDayState).GetProperty("ConsecutiveFailedAttempts", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x06000DEF RID: 3567 RVA: 0x00386C1C File Offset: 0x00386C1C
    private void ProactiveSlotDayStateSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveSlotDayState value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      DateTimeOffset? morningFiredAt = value.MorningFiredAt;
      if (morningFiredAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_morning_fired_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, morningFiredAt, this.NullableDateTimeOffset);
      }
      DateTimeOffset? retryAfter = value.RetryAfter;
      if (retryAfter != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_retry_after);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, retryAfter, this.NullableDateTimeOffset);
      }
      int? consecutiveFailedAttempts = value.ConsecutiveFailedAttempts;
      if (consecutiveFailedAttempts != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_consecutive_failed_attempts);
        JsonSerializer.Serialize<int?>(writer, consecutiveFailedAttempts, this.NullableInt32);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DF0 RID: 3568 RVA: 0x00386CB8 File Offset: 0x00386CB8
    private static JsonParameterInfoValues[] ProactiveSlotDayStateCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "MorningFiredAt",
          ParameterType = typeof(DateTimeOffset?),
          Position = 0,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "RetryAfter",
          ParameterType = typeof(DateTimeOffset?),
          Position = 1,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ConsecutiveFailedAttempts",
          ParameterType = typeof(int?),
          Position = 2,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000316 RID: 790
    // (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00386D88 File Offset: 0x00386D88
    [Nullable(0)]
    public JsonTypeInfo<ProactiveStoreData> ProactiveStoreData
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveStoreData> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveStoreData) == null)
        {
          jsonTypeInfo = (this._ProactiveStoreData = (JsonTypeInfo<ProactiveStoreData>)base.Options.GetTypeInfo(typeof(ProactiveStoreData)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DF2 RID: 3570 RVA: 0x00386DC4 File Offset: 0x00386DC4
    private JsonTypeInfo<ProactiveStoreData> Create_ProactiveStoreData(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveStoreData> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveStoreData>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveStoreData> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveStoreData>();
        jsonObjectInfoValues.ObjectCreator = () => new ProactiveStoreData();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveStoreDataPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveStoreData).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveStoreData>(this.ProactiveStoreDataSerializeHandler);
        JsonObjectInfoValues<ProactiveStoreData> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveStoreData>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DF3 RID: 3571 RVA: 0x00386E98 File Offset: 0x00386E98
    private static JsonPropertyInfo[] ProactiveStoreDataPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[11];
      JsonPropertyInfoValues<Dictionary<string, ProactiveAccountState>> jsonPropertyInfoValues = new JsonPropertyInfoValues<Dictionary<string, ProactiveAccountState>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveStoreData)obj).Accounts;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, ProactiveAccountState> value)
      {
        ((ProactiveStoreData)obj).Accounts = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Accounts";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("Accounts", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Dictionary<string, ProactiveAccountState>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Dictionary<string, ProactiveAccountState>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<Dictionary<string, ProactiveAccountState>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<List<ProactiveHistoryEntry>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<List<ProactiveHistoryEntry>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveStoreData)obj).History;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<ProactiveHistoryEntry> value)
      {
        ((ProactiveStoreData)obj).History = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "History";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("History", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<ProactiveHistoryEntry>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<ProactiveHistoryEntry>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<List<ProactiveHistoryEntry>>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveStoreData)obj).MorningGreetingLastDay;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((ProactiveStoreData)obj).MorningGreetingLastDay = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "MorningGreetingLastDay";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("MorningGreetingLastDay", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ProactiveStoreData)obj).MorningGreetingLastPresentedAt;
      jsonPropertyInfoValues7.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveStoreData)obj).MorningGreetingLastPresentedAt = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "MorningGreetingLastPresentedAt";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("MorningGreetingLastPresentedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ProactiveStoreData)obj).MorningSettledAt;
      jsonPropertyInfoValues9.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveStoreData)obj).MorningSettledAt = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "MorningSettledAt";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("MorningSettledAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<MorningDismissalHistory> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<MorningDismissalHistory>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ProactiveStoreData)obj).MorningDismissals;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] MorningDismissalHistory value)
      {
        ((ProactiveStoreData)obj).MorningDismissals = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "MorningDismissals";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("MorningDismissals", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(MorningDismissalHistory), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<MorningDismissalHistory> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<MorningDismissalHistory>(options, jsonPropertyInfoValues12);
      array[5].IsGetNullable = false;
      array[5].IsSetNullable = false;
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ProactiveStoreData)obj).OnboardingFirstCompletedAt;
      jsonPropertyInfoValues13.Setter = delegate(object obj, DateTimeOffset? value)
      {
        ((ProactiveStoreData)obj).OnboardingFirstCompletedAt = value;
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "OnboardingFirstCompletedAt";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("OnboardingFirstCompletedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<Dictionary<string, DateTimeOffset>> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<Dictionary<string, DateTimeOffset>>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((ProactiveStoreData)obj).IntegrationSnoozedUntil;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] Dictionary<string, DateTimeOffset> value)
      {
        ((ProactiveStoreData)obj).IntegrationSnoozedUntil = value;
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "IntegrationSnoozedUntil";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("IntegrationSnoozedUntil", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Dictionary<string, DateTimeOffset>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Dictionary<string, DateTimeOffset>> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<Dictionary<string, DateTimeOffset>>(options, jsonPropertyInfoValues16);
      array[7].IsGetNullable = false;
      array[7].IsSetNullable = false;
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<List<string>>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((ProactiveStoreData)obj).IntegrationNeverAsk;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<string> value)
      {
        ((ProactiveStoreData)obj).IntegrationNeverAsk = value;
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "IntegrationNeverAsk";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("IntegrationNeverAsk", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<List<string>>(options, jsonPropertyInfoValues18);
      array[8].IsGetNullable = false;
      array[8].IsSetNullable = false;
      JsonPropertyInfoValues<List<DateTimeOffset>> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<List<DateTimeOffset>>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((ProactiveStoreData)obj).IntegrationPromptTimestamps;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(2)] List<DateTimeOffset> value)
      {
        ((ProactiveStoreData)obj).IntegrationPromptTimestamps = value;
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "IntegrationPromptTimestamps";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("IntegrationPromptTimestamps", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<DateTimeOffset>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<DateTimeOffset>> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<List<DateTimeOffset>>(options, jsonPropertyInfoValues20);
      array[9].IsGetNullable = false;
      array[9].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(ProactiveStoreData);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((ProactiveStoreData)obj).SpotifyVoiceTipShown;
      jsonPropertyInfoValues21.Setter = delegate(object obj, bool value)
      {
        ((ProactiveStoreData)obj).SpotifyVoiceTipShown = value;
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "SpotifyVoiceTipShown";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(ProactiveStoreData).GetProperty("SpotifyVoiceTipShown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues22);
      return array;
    }

    // Token: 0x06000DF4 RID: 3572 RVA: 0x00387944 File Offset: 0x00387944
    private void ProactiveStoreDataSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveStoreData value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      Dictionary<string, ProactiveAccountState> accounts = value.Accounts;
      if (accounts != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_accounts);
        this.DictionaryStringProactiveAccountStateSerializeHandler(writer, accounts);
      }
      List<ProactiveHistoryEntry> history = value.History;
      if (history != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_history);
        this.ListProactiveHistoryEntrySerializeHandler(writer, history);
      }
      string morningGreetingLastDay = value.MorningGreetingLastDay;
      if (morningGreetingLastDay != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_morning_greeting_last_day, morningGreetingLastDay);
      }
      DateTimeOffset? morningGreetingLastPresentedAt = value.MorningGreetingLastPresentedAt;
      if (morningGreetingLastPresentedAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_morning_greeting_last_presented_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, morningGreetingLastPresentedAt, this.NullableDateTimeOffset);
      }
      DateTimeOffset? morningSettledAt = value.MorningSettledAt;
      if (morningSettledAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_morning_settled_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, morningSettledAt, this.NullableDateTimeOffset);
      }
      MorningDismissalHistory morningDismissals = value.MorningDismissals;
      if (morningDismissals != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_morning_dismissals);
        this.MorningDismissalHistorySerializeHandler(writer, morningDismissals);
      }
      DateTimeOffset? onboardingFirstCompletedAt = value.OnboardingFirstCompletedAt;
      if (onboardingFirstCompletedAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_onboarding_first_completed_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, onboardingFirstCompletedAt, this.NullableDateTimeOffset);
      }
      Dictionary<string, DateTimeOffset> integrationSnoozedUntil = value.IntegrationSnoozedUntil;
      if (integrationSnoozedUntil != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_integration_snoozed_until);
        this.DictionaryStringDateTimeOffsetSerializeHandler(writer, integrationSnoozedUntil);
      }
      List<string> integrationNeverAsk = value.IntegrationNeverAsk;
      if (integrationNeverAsk != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_integration_never_ask);
        this.ListStringSerializeHandler(writer, integrationNeverAsk);
      }
      List<DateTimeOffset> integrationPromptTimestamps = value.IntegrationPromptTimestamps;
      if (integrationPromptTimestamps != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_integration_prompt_timestamps);
        this.ListDateTimeOffsetSerializeHandler(writer, integrationPromptTimestamps);
      }
      writer.WriteBoolean(ProactiveJsonContext.PropName_spotify_voice_tip_shown, value.SpotifyVoiceTipShown);
      writer.WriteEndObject();
    }

    // Token: 0x17000317 RID: 791
    // (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00387AC4 File Offset: 0x00387AC4
    [Nullable(0)]
    public JsonTypeInfo<ProactiveSuggestionCard> ProactiveSuggestionCard
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ProactiveSuggestionCard> jsonTypeInfo;
        if ((jsonTypeInfo = this._ProactiveSuggestionCard) == null)
        {
          jsonTypeInfo = (this._ProactiveSuggestionCard = (JsonTypeInfo<ProactiveSuggestionCard>)base.Options.GetTypeInfo(typeof(ProactiveSuggestionCard)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DF6 RID: 3574 RVA: 0x00387B00 File Offset: 0x00387B00
    private JsonTypeInfo<ProactiveSuggestionCard> Create_ProactiveSuggestionCard(JsonSerializerOptions options)
    {
      JsonTypeInfo<ProactiveSuggestionCard> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ProactiveSuggestionCard>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ProactiveSuggestionCard> jsonObjectInfoValues = new JsonObjectInfoValues<ProactiveSuggestionCard>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ProactiveSuggestionCard
        {
          Id = (Guid)args[0],
          Title = (string)args[1],
          Description = (string)args[2],
          AgentPrompt = (string)args[3],
          CheckReason = (ProactiveCheckReason)args[4],
          ServerSuggestionId = (int?)args[5],
          ClickySlug = (string)args[6],
          NewClicky = (ProactiveNewClicky)args[7],
          Routine = (ProactiveRoutine)args[8],
          IntegrationName = (string)args[9],
          CreatedAt = (DateTimeOffset)args[10],
          ViewedAt = (DateTimeOffset?)args[11]
        };
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.ProactiveSuggestionCardPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<6>__ProactiveSuggestionCardCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<6>__ProactiveSuggestionCardCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.ProactiveSuggestionCardCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ProactiveSuggestionCard>(this.ProactiveSuggestionCardSerializeHandler);
        JsonObjectInfoValues<ProactiveSuggestionCard> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ProactiveSuggestionCard>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DF7 RID: 3575 RVA: 0x00387BEC File Offset: 0x00387BEC
    private static JsonPropertyInfo[] ProactiveSuggestionCardPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[13];
      JsonPropertyInfoValues<Guid> jsonPropertyInfoValues = new JsonPropertyInfoValues<Guid>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ProactiveSuggestionCard)obj).Id;
      jsonPropertyInfoValues.Setter = delegate(object obj, Guid value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Id";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Guid), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Guid> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<Guid>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ProactiveSuggestionCard)obj).Title;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Title";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsRequired = true;
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ProactiveSuggestionCard)obj).Description;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Description";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsRequired = true;
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ProactiveSuggestionCard)obj).AgentPrompt;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "AgentPrompt";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("AgentPrompt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsRequired = true;
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<ProactiveCheckReason> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<ProactiveCheckReason>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ProactiveSuggestionCard)obj).CheckReason;
      jsonPropertyInfoValues9.Setter = delegate(object obj, ProactiveCheckReason value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "CheckReason";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("CheckReason", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveCheckReason), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveCheckReason> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<ProactiveCheckReason>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ProactiveSuggestionCard)obj).ServerSuggestionId;
      jsonPropertyInfoValues11.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ServerSuggestionId";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("ServerSuggestionId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ProactiveSuggestionCard)obj).ClickySlug;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "ClickySlug";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("ClickySlug", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<ProactiveNewClicky> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<ProactiveNewClicky>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((ProactiveSuggestionCard)obj).NewClicky;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] ProactiveNewClicky value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "NewClicky";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("NewClicky", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveNewClicky), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveNewClicky> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<ProactiveNewClicky>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<ProactiveRoutine> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<ProactiveRoutine>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((ProactiveSuggestionCard)obj).Routine;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] ProactiveRoutine value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "Routine";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("Routine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveRoutine), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveRoutine> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<ProactiveRoutine>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((ProactiveSuggestionCard)obj).IntegrationName;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "IntegrationName";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("IntegrationName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<DateTimeOffset>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((ProactiveSuggestionCard)obj).CreatedAt;
      jsonPropertyInfoValues21.Setter = delegate(object obj, DateTimeOffset value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "CreatedAt";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((ProactiveSuggestionCard)obj).ViewedAt;
      jsonPropertyInfoValues23.Setter = delegate(object obj, DateTimeOffset? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "ViewedAt";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("ViewedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<DateTimeOffset?>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(ProactiveSuggestionCard);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = (object obj) => ((ProactiveSuggestionCard)obj).ExpiresAt;
      jsonPropertyInfoValues25.Setter = null;
      jsonPropertyInfoValues25.IgnoreCondition = null;
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "ExpiresAt";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(ProactiveSuggestionCard).GetProperty("ExpiresAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(DateTimeOffset?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<DateTimeOffset?> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<DateTimeOffset?>(options, jsonPropertyInfoValues26);
      return array;
    }

    // Token: 0x06000DF8 RID: 3576 RVA: 0x00388838 File Offset: 0x00388838
    private void ProactiveSuggestionCardSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ProactiveSuggestionCard value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteString(ProactiveJsonContext.PropName_id, value.Id);
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_title, title);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_description, description);
      }
      string agentPrompt = value.AgentPrompt;
      if (agentPrompt != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_agent_prompt, agentPrompt);
      }
      writer.WritePropertyName(ProactiveJsonContext.PropName_check_reason);
      JsonSerializer.Serialize<ProactiveCheckReason>(writer, value.CheckReason, this.ProactiveCheckReason);
      int? serverSuggestionId = value.ServerSuggestionId;
      if (serverSuggestionId != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_server_suggestion_id);
        JsonSerializer.Serialize<int?>(writer, serverSuggestionId, this.NullableInt32);
      }
      string clickySlug = value.ClickySlug;
      if (clickySlug != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_clicky_slug, clickySlug);
      }
      ProactiveNewClicky newClicky = value.NewClicky;
      if (newClicky != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_new_clicky);
        this.ProactiveNewClickySerializeHandler(writer, newClicky);
      }
      ProactiveRoutine routine = value.Routine;
      if (routine != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_routine);
        this.ProactiveRoutineSerializeHandler(writer, routine);
      }
      string integrationName = value.IntegrationName;
      if (integrationName != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_integration_name, integrationName);
      }
      writer.WriteString(ProactiveJsonContext.PropName_created_at, value.CreatedAt);
      DateTimeOffset? viewedAt = value.ViewedAt;
      if (viewedAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_viewed_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, viewedAt, this.NullableDateTimeOffset);
      }
      DateTimeOffset? expiresAt = value.ExpiresAt;
      if (expiresAt != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_expires_at);
        JsonSerializer.Serialize<DateTimeOffset?>(writer, expiresAt, this.NullableDateTimeOffset);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000DF9 RID: 3577 RVA: 0x003889CC File Offset: 0x003889CC
    private static JsonParameterInfoValues[] ProactiveSuggestionCardCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Id",
          ParameterType = typeof(Guid),
          Position = 0,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 1,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Description",
          ParameterType = typeof(string),
          Position = 2,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "AgentPrompt",
          ParameterType = typeof(string),
          Position = 3,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "CheckReason",
          ParameterType = typeof(ProactiveCheckReason),
          Position = 4,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ServerSuggestionId",
          ParameterType = typeof(int?),
          Position = 5,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ClickySlug",
          ParameterType = typeof(string),
          Position = 6,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "NewClicky",
          ParameterType = typeof(ProactiveNewClicky),
          Position = 7,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "Routine",
          ParameterType = typeof(ProactiveRoutine),
          Position = 8,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "IntegrationName",
          ParameterType = typeof(string),
          Position = 9,
          IsNullable = true,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "CreatedAt",
          ParameterType = typeof(DateTimeOffset),
          Position = 10,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "ViewedAt",
          ParameterType = typeof(DateTimeOffset?),
          Position = 11,
          IsNullable = true,
          IsMemberInitializer = true
        }
      };
    }

    // Token: 0x17000318 RID: 792
    // (get) Token: 0x06000DFA RID: 3578 RVA: 0x00388C88 File Offset: 0x00388C88
    [Nullable(0)]
    public JsonTypeInfo<QueuedProactiveTask> QueuedProactiveTask
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<QueuedProactiveTask> jsonTypeInfo;
        if ((jsonTypeInfo = this._QueuedProactiveTask) == null)
        {
          jsonTypeInfo = (this._QueuedProactiveTask = (JsonTypeInfo<QueuedProactiveTask>)base.Options.GetTypeInfo(typeof(QueuedProactiveTask)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000DFB RID: 3579 RVA: 0x00388CC4 File Offset: 0x00388CC4
    private JsonTypeInfo<QueuedProactiveTask> Create_QueuedProactiveTask(JsonSerializerOptions options)
    {
      JsonTypeInfo<QueuedProactiveTask> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<QueuedProactiveTask>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<QueuedProactiveTask> jsonObjectInfoValues = new JsonObjectInfoValues<QueuedProactiveTask>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new QueuedProactiveTask((ProactiveSuggestionCard)args[0], (string)args[1], (string)args[2], (bool)args[3], (bool)args[4])
        {
          IsPaused = (bool)args[5],
          IsLaunching = (bool)args[6]
        };
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => ProactiveJsonContext.QueuedProactiveTaskPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = ProactiveJsonContext.<>O.<7>__QueuedProactiveTaskCtorParamInit) == null)
        {
          func = (ProactiveJsonContext.<>O.<7>__QueuedProactiveTaskCtorParamInit = new Func<JsonParameterInfoValues[]>(ProactiveJsonContext.QueuedProactiveTaskCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(QueuedProactiveTask).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(ProactiveSuggestionCard),
          typeof(string),
          typeof(string),
          typeof(bool),
          typeof(bool)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, QueuedProactiveTask>(this.QueuedProactiveTaskSerializeHandler);
        JsonObjectInfoValues<QueuedProactiveTask> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<QueuedProactiveTask>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000DFC RID: 3580 RVA: 0x00388DB0 File Offset: 0x00388DB0
    private static JsonPropertyInfo[] QueuedProactiveTaskPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[8];
      JsonPropertyInfoValues<ProactiveSuggestionCard> jsonPropertyInfoValues = new JsonPropertyInfoValues<ProactiveSuggestionCard>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((QueuedProactiveTask)obj).Card;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] ProactiveSuggestionCard value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Card";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("Card", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(ProactiveSuggestionCard), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<ProactiveSuggestionCard> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<ProactiveSuggestionCard>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((QueuedProactiveTask)obj).ClickySlug;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ClickySlug";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("ClickySlug", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((QueuedProactiveTask)obj).AdjustmentText;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "AdjustmentText";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("AdjustmentText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((QueuedProactiveTask)obj).WasFounded;
      jsonPropertyInfoValues7.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "WasFounded";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("WasFounded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((QueuedProactiveTask)obj).WasRestored;
      jsonPropertyInfoValues9.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "WasRestored";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("WasRestored", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<Guid> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<Guid>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((QueuedProactiveTask)obj).Id;
      jsonPropertyInfoValues11.Setter = null;
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "Id";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Guid), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Guid> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<Guid>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((QueuedProactiveTask)obj).IsPaused;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "IsPaused";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("IsPaused", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(QueuedProactiveTask);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((QueuedProactiveTask)obj).IsLaunching;
      jsonPropertyInfoValues15.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "IsLaunching";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(QueuedProactiveTask).GetProperty("IsLaunching", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues16);
      return array;
    }

    // Token: 0x06000DFD RID: 3581 RVA: 0x0038952C File Offset: 0x0038952C
    private void QueuedProactiveTaskSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] QueuedProactiveTask value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      ProactiveSuggestionCard card = value.Card;
      if (card != null)
      {
        writer.WritePropertyName(ProactiveJsonContext.PropName_card);
        this.ProactiveSuggestionCardSerializeHandler(writer, card);
      }
      string clickySlug = value.ClickySlug;
      if (clickySlug != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_clicky_slug, clickySlug);
      }
      string adjustmentText = value.AdjustmentText;
      if (adjustmentText != null)
      {
        writer.WriteString(ProactiveJsonContext.PropName_adjustment_text, adjustmentText);
      }
      writer.WriteBoolean(ProactiveJsonContext.PropName_was_founded, value.WasFounded);
      writer.WriteBoolean(ProactiveJsonContext.PropName_was_restored, value.WasRestored);
      writer.WriteString(ProactiveJsonContext.PropName_id, value.Id);
      writer.WriteBoolean(ProactiveJsonContext.PropName_is_paused, value.IsPaused);
      writer.WriteBoolean(ProactiveJsonContext.PropName_is_launching, value.IsLaunching);
      writer.WriteEndObject();
    }

    // Token: 0x06000DFE RID: 3582 RVA: 0x003895F0 File Offset: 0x003895F0
    private static JsonParameterInfoValues[] QueuedProactiveTaskCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Card",
          ParameterType = typeof(ProactiveSuggestionCard),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ClickySlug",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "AdjustmentText",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "WasFounded",
          ParameterType = typeof(bool),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "WasRestored",
          ParameterType = typeof(bool),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "IsPaused",
          ParameterType = typeof(bool),
          Position = 5,
          IsNullable = false,
          IsMemberInitializer = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsLaunching",
          ParameterType = typeof(bool),
          Position = 6,
          IsNullable = false,
          IsMemberInitializer = true
        }
      };
    }

    // Token: 0x17000319 RID: 793
    // (get) Token: 0x06000DFF RID: 3583 RVA: 0x003897B0 File Offset: 0x003897B0
    [Nullable(0)]
    public JsonTypeInfo<Dictionary<string, ProactiveAccountState>> DictionaryStringProactiveAccountState
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<Dictionary<string, ProactiveAccountState>> jsonTypeInfo;
        if ((jsonTypeInfo = this._DictionaryStringProactiveAccountState) == null)
        {
          jsonTypeInfo = (this._DictionaryStringProactiveAccountState = (JsonTypeInfo<Dictionary<string, ProactiveAccountState>>)base.Options.GetTypeInfo(typeof(Dictionary<string, ProactiveAccountState>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E00 RID: 3584 RVA: 0x003897EC File Offset: 0x003897EC
    private JsonTypeInfo<Dictionary<string, ProactiveAccountState>> Create_DictionaryStringProactiveAccountState(JsonSerializerOptions options)
    {
      JsonTypeInfo<Dictionary<string, ProactiveAccountState>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, ProactiveAccountState>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<Dictionary<string, ProactiveAccountState>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, ProactiveAccountState>>();
        jsonCollectionInfoValues.ObjectCreator = () => new Dictionary<string, ProactiveAccountState>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, Dictionary<string, ProactiveAccountState>>(this.DictionaryStringProactiveAccountStateSerializeHandler);
        JsonCollectionInfoValues<Dictionary<string, ProactiveAccountState>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, ProactiveAccountState>, string, ProactiveAccountState>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E01 RID: 3585 RVA: 0x00389860 File Offset: 0x00389860
    private void DictionaryStringProactiveAccountStateSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, ProactiveAccountState> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      foreach (KeyValuePair<string, ProactiveAccountState> keyValuePair in value)
      {
        writer.WritePropertyName(keyValuePair.Key);
        this.ProactiveAccountStateSerializeHandler(writer, keyValuePair.Value);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700031A RID: 794
    // (get) Token: 0x06000E02 RID: 3586 RVA: 0x003898D8 File Offset: 0x003898D8
    [Nullable(0)]
    public JsonTypeInfo<Dictionary<string, DateTimeOffset>> DictionaryStringDateTimeOffset
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<Dictionary<string, DateTimeOffset>> jsonTypeInfo;
        if ((jsonTypeInfo = this._DictionaryStringDateTimeOffset) == null)
        {
          jsonTypeInfo = (this._DictionaryStringDateTimeOffset = (JsonTypeInfo<Dictionary<string, DateTimeOffset>>)base.Options.GetTypeInfo(typeof(Dictionary<string, DateTimeOffset>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E03 RID: 3587 RVA: 0x00389914 File Offset: 0x00389914
    private JsonTypeInfo<Dictionary<string, DateTimeOffset>> Create_DictionaryStringDateTimeOffset(JsonSerializerOptions options)
    {
      JsonTypeInfo<Dictionary<string, DateTimeOffset>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, DateTimeOffset>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<Dictionary<string, DateTimeOffset>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, DateTimeOffset>>();
        jsonCollectionInfoValues.ObjectCreator = () => new Dictionary<string, DateTimeOffset>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, Dictionary<string, DateTimeOffset>>(this.DictionaryStringDateTimeOffsetSerializeHandler);
        JsonCollectionInfoValues<Dictionary<string, DateTimeOffset>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, DateTimeOffset>, string, DateTimeOffset>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E04 RID: 3588 RVA: 0x00389988 File Offset: 0x00389988
    private void DictionaryStringDateTimeOffsetSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] Dictionary<string, DateTimeOffset> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      foreach (KeyValuePair<string, DateTimeOffset> keyValuePair in value)
      {
        writer.WriteString(keyValuePair.Key, keyValuePair.Value);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700031B RID: 795
    // (get) Token: 0x06000E05 RID: 3589 RVA: 0x003899FC File Offset: 0x003899FC
    [Nullable(0)]
    public JsonTypeInfo<Dictionary<string, string>> DictionaryStringString
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<Dictionary<string, string>> jsonTypeInfo;
        if ((jsonTypeInfo = this._DictionaryStringString) == null)
        {
          jsonTypeInfo = (this._DictionaryStringString = (JsonTypeInfo<Dictionary<string, string>>)base.Options.GetTypeInfo(typeof(Dictionary<string, string>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E06 RID: 3590 RVA: 0x00389A38 File Offset: 0x00389A38
    private JsonTypeInfo<Dictionary<string, string>> Create_DictionaryStringString(JsonSerializerOptions options)
    {
      JsonTypeInfo<Dictionary<string, string>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, string>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<Dictionary<string, string>> jsonCollectionInfoValues = new JsonCollectionInfoValues<Dictionary<string, string>>();
        jsonCollectionInfoValues.ObjectCreator = () => new Dictionary<string, string>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, Dictionary<string, string>>(this.DictionaryStringStringSerializeHandler);
        JsonCollectionInfoValues<Dictionary<string, string>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateDictionaryInfo<Dictionary<string, string>, string, string>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E07 RID: 3591 RVA: 0x00389AAC File Offset: 0x00389AAC
    private void DictionaryStringStringSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, string> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      foreach (KeyValuePair<string, string> keyValuePair in value)
      {
        writer.WriteString(keyValuePair.Key, keyValuePair.Value);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700031C RID: 796
    // (get) Token: 0x06000E08 RID: 3592 RVA: 0x00389B20 File Offset: 0x00389B20
    [Nullable(0)]
    public JsonTypeInfo<List<ProactiveHistoryEntry>> ListProactiveHistoryEntry
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<ProactiveHistoryEntry>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListProactiveHistoryEntry) == null)
        {
          jsonTypeInfo = (this._ListProactiveHistoryEntry = (JsonTypeInfo<List<ProactiveHistoryEntry>>)base.Options.GetTypeInfo(typeof(List<ProactiveHistoryEntry>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E09 RID: 3593 RVA: 0x00389B5C File Offset: 0x00389B5C
    private JsonTypeInfo<List<ProactiveHistoryEntry>> Create_ListProactiveHistoryEntry(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<ProactiveHistoryEntry>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<ProactiveHistoryEntry>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<ProactiveHistoryEntry>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<ProactiveHistoryEntry>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<ProactiveHistoryEntry>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<ProactiveHistoryEntry>>(this.ListProactiveHistoryEntrySerializeHandler);
        JsonCollectionInfoValues<List<ProactiveHistoryEntry>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<ProactiveHistoryEntry>, ProactiveHistoryEntry>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E0A RID: 3594 RVA: 0x00389BD0 File Offset: 0x00389BD0
    private void ListProactiveHistoryEntrySerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<ProactiveHistoryEntry> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.ProactiveHistoryEntrySerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700031D RID: 797
    // (get) Token: 0x06000E0B RID: 3595 RVA: 0x00389C14 File Offset: 0x00389C14
    [Nullable(0)]
    public JsonTypeInfo<List<ProactiveSuggestionCard>> ListProactiveSuggestionCard
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<ProactiveSuggestionCard>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListProactiveSuggestionCard) == null)
        {
          jsonTypeInfo = (this._ListProactiveSuggestionCard = (JsonTypeInfo<List<ProactiveSuggestionCard>>)base.Options.GetTypeInfo(typeof(List<ProactiveSuggestionCard>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E0C RID: 3596 RVA: 0x00389C50 File Offset: 0x00389C50
    private JsonTypeInfo<List<ProactiveSuggestionCard>> Create_ListProactiveSuggestionCard(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<ProactiveSuggestionCard>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<ProactiveSuggestionCard>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<ProactiveSuggestionCard>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<ProactiveSuggestionCard>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<ProactiveSuggestionCard>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<ProactiveSuggestionCard>>(this.ListProactiveSuggestionCardSerializeHandler);
        JsonCollectionInfoValues<List<ProactiveSuggestionCard>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<ProactiveSuggestionCard>, ProactiveSuggestionCard>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E0D RID: 3597 RVA: 0x00389CC4 File Offset: 0x00389CC4
    private void ListProactiveSuggestionCardSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<ProactiveSuggestionCard> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.ProactiveSuggestionCardSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700031E RID: 798
    // (get) Token: 0x06000E0E RID: 3598 RVA: 0x00389D08 File Offset: 0x00389D08
    [Nullable(0)]
    public JsonTypeInfo<List<QueuedProactiveTask>> ListQueuedProactiveTask
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<QueuedProactiveTask>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListQueuedProactiveTask) == null)
        {
          jsonTypeInfo = (this._ListQueuedProactiveTask = (JsonTypeInfo<List<QueuedProactiveTask>>)base.Options.GetTypeInfo(typeof(List<QueuedProactiveTask>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E0F RID: 3599 RVA: 0x00389D44 File Offset: 0x00389D44
    private JsonTypeInfo<List<QueuedProactiveTask>> Create_ListQueuedProactiveTask(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<QueuedProactiveTask>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<QueuedProactiveTask>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<QueuedProactiveTask>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<QueuedProactiveTask>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<QueuedProactiveTask>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<QueuedProactiveTask>>(this.ListQueuedProactiveTaskSerializeHandler);
        JsonCollectionInfoValues<List<QueuedProactiveTask>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<QueuedProactiveTask>, QueuedProactiveTask>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E10 RID: 3600 RVA: 0x00389DB8 File Offset: 0x00389DB8
    private void ListQueuedProactiveTaskSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] List<QueuedProactiveTask> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      for (int i = 0; i < value.Count; i++)
      {
        this.QueuedProactiveTaskSerializeHandler(writer, value[i]);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700031F RID: 799
    // (get) Token: 0x06000E11 RID: 3601 RVA: 0x00389DFC File Offset: 0x00389DFC
    [Nullable(0)]
    public JsonTypeInfo<List<DateTime>> ListDateTime
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<DateTime>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListDateTime) == null)
        {
          jsonTypeInfo = (this._ListDateTime = (JsonTypeInfo<List<DateTime>>)base.Options.GetTypeInfo(typeof(List<DateTime>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E12 RID: 3602 RVA: 0x00389E38 File Offset: 0x00389E38
    private JsonTypeInfo<List<DateTime>> Create_ListDateTime(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<DateTime>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<DateTime>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<DateTime>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<DateTime>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<DateTime>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<DateTime>>(this.ListDateTimeSerializeHandler);
        JsonCollectionInfoValues<List<DateTime>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<DateTime>, DateTime>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E13 RID: 3603 RVA: 0x00389EAC File Offset: 0x00389EAC
    private void ListDateTimeSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] List<DateTime> value)
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

    // Token: 0x17000320 RID: 800
    // (get) Token: 0x06000E14 RID: 3604 RVA: 0x00389EF0 File Offset: 0x00389EF0
    [Nullable(0)]
    public JsonTypeInfo<List<DateTimeOffset>> ListDateTimeOffset
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<List<DateTimeOffset>> jsonTypeInfo;
        if ((jsonTypeInfo = this._ListDateTimeOffset) == null)
        {
          jsonTypeInfo = (this._ListDateTimeOffset = (JsonTypeInfo<List<DateTimeOffset>>)base.Options.GetTypeInfo(typeof(List<DateTimeOffset>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E15 RID: 3605 RVA: 0x00389F2C File Offset: 0x00389F2C
    private JsonTypeInfo<List<DateTimeOffset>> Create_ListDateTimeOffset(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<DateTimeOffset>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<DateTimeOffset>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<List<DateTimeOffset>> jsonCollectionInfoValues = new JsonCollectionInfoValues<List<DateTimeOffset>>();
        jsonCollectionInfoValues.ObjectCreator = () => new List<DateTimeOffset>();
        jsonCollectionInfoValues.SerializeHandler = new Action<Utf8JsonWriter, List<DateTimeOffset>>(this.ListDateTimeOffsetSerializeHandler);
        JsonCollectionInfoValues<List<DateTimeOffset>> jsonCollectionInfoValues2 = jsonCollectionInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateListInfo<List<DateTimeOffset>, DateTimeOffset>(options, jsonCollectionInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000E16 RID: 3606 RVA: 0x00389FA0 File Offset: 0x00389FA0
    private void ListDateTimeOffsetSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] List<DateTimeOffset> value)
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

    // Token: 0x17000321 RID: 801
    // (get) Token: 0x06000E17 RID: 3607 RVA: 0x00389FE4 File Offset: 0x00389FE4
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

    // Token: 0x06000E18 RID: 3608 RVA: 0x0038A020 File Offset: 0x0038A020
    private JsonTypeInfo<List<string>> Create_ListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<string>> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<string>>(options, out jsonTypeInfo))
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

    // Token: 0x06000E19 RID: 3609 RVA: 0x0038A094 File Offset: 0x0038A094
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

    // Token: 0x17000322 RID: 802
    // (get) Token: 0x06000E1A RID: 3610 RVA: 0x0038A0D8 File Offset: 0x0038A0D8
    [Nullable(0)]
    public JsonTypeInfo<DateTime> DateTime
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DateTime> jsonTypeInfo;
        if ((jsonTypeInfo = this._DateTime) == null)
        {
          jsonTypeInfo = (this._DateTime = (JsonTypeInfo<DateTime>)base.Options.GetTypeInfo(typeof(DateTime)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E1B RID: 3611 RVA: 0x0038A114 File Offset: 0x0038A114
    private JsonTypeInfo<DateTime> Create_DateTime(JsonSerializerOptions options)
    {
      JsonTypeInfo<DateTime> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DateTime>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<DateTime>(options, JsonMetadataServices.DateTimeConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000323 RID: 803
    // (get) Token: 0x06000E1C RID: 3612 RVA: 0x0038A140 File Offset: 0x0038A140
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

    // Token: 0x06000E1D RID: 3613 RVA: 0x0038A17C File Offset: 0x0038A17C
    private JsonTypeInfo<DateTimeOffset> Create_DateTimeOffset(JsonSerializerOptions options)
    {
      JsonTypeInfo<DateTimeOffset> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DateTimeOffset>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<DateTimeOffset>(options, JsonMetadataServices.DateTimeOffsetConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000324 RID: 804
    // (get) Token: 0x06000E1E RID: 3614 RVA: 0x0038A1A8 File Offset: 0x0038A1A8
    [Nullable(0)]
    public JsonTypeInfo<DateTimeOffset?> NullableDateTimeOffset
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DateTimeOffset?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableDateTimeOffset) == null)
        {
          jsonTypeInfo = (this._NullableDateTimeOffset = (JsonTypeInfo<DateTimeOffset?>)base.Options.GetTypeInfo(typeof(DateTimeOffset?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E1F RID: 3615 RVA: 0x0038A1E4 File Offset: 0x0038A1E4
    private JsonTypeInfo<DateTimeOffset?> Create_NullableDateTimeOffset(JsonSerializerOptions options)
    {
      JsonTypeInfo<DateTimeOffset?> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DateTimeOffset?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<DateTimeOffset>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<DateTimeOffset?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000325 RID: 805
    // (get) Token: 0x06000E20 RID: 3616 RVA: 0x0038A214 File Offset: 0x0038A214
    [Nullable(0)]
    public JsonTypeInfo<Guid> Guid
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<Guid> jsonTypeInfo;
        if ((jsonTypeInfo = this._Guid) == null)
        {
          jsonTypeInfo = (this._Guid = (JsonTypeInfo<Guid>)base.Options.GetTypeInfo(typeof(Guid)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E21 RID: 3617 RVA: 0x0038A250 File Offset: 0x0038A250
    private JsonTypeInfo<Guid> Create_Guid(JsonSerializerOptions options)
    {
      JsonTypeInfo<Guid> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Guid>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<Guid>(options, JsonMetadataServices.GuidConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000326 RID: 806
    // (get) Token: 0x06000E22 RID: 3618 RVA: 0x0038A27C File Offset: 0x0038A27C
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

    // Token: 0x06000E23 RID: 3619 RVA: 0x0038A2B8 File Offset: 0x0038A2B8
    private JsonTypeInfo<int> Create_Int32(JsonSerializerOptions options)
    {
      JsonTypeInfo<int> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<int>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int>(options, JsonMetadataServices.Int32Converter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000327 RID: 807
    // (get) Token: 0x06000E24 RID: 3620 RVA: 0x0038A2E4 File Offset: 0x0038A2E4
    [Nullable(0)]
    public JsonTypeInfo<int?> NullableInt32
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<int?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableInt32) == null)
        {
          jsonTypeInfo = (this._NullableInt32 = (JsonTypeInfo<int?>)base.Options.GetTypeInfo(typeof(int?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000E25 RID: 3621 RVA: 0x0038A320 File Offset: 0x0038A320
    private JsonTypeInfo<int?> Create_NullableInt32(JsonSerializerOptions options)
    {
      JsonTypeInfo<int?> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<int?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<int>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000328 RID: 808
    // (get) Token: 0x06000E26 RID: 3622 RVA: 0x0038A350 File Offset: 0x0038A350
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

    // Token: 0x06000E27 RID: 3623 RVA: 0x0038A38C File Offset: 0x0038A38C
    private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
    {
      JsonTypeInfo<string> jsonTypeInfo;
      if (!ProactiveJsonContext.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000329 RID: 809
    // (get) Token: 0x06000E28 RID: 3624 RVA: 0x0038A3B8 File Offset: 0x0038A3B8
    public static ProactiveJsonContext Default { get; } = new ProactiveJsonContext(new JsonSerializerOptions(ProactiveJsonContext.s_defaultOptions));

    // Token: 0x1700032A RID: 810
    // (get) Token: 0x06000E29 RID: 3625 RVA: 0x0038A3C0 File Offset: 0x0038A3C0
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = ProactiveJsonContext.s_defaultOptions;

    // Token: 0x06000E2A RID: 3626 RVA: 0x0038A3C8 File Offset: 0x0038A3C8
    public ProactiveJsonContext()
      : base(null)
    {
    }

    // Token: 0x06000E2B RID: 3627 RVA: 0x0038A3DC File Offset: 0x0038A3DC
    public ProactiveJsonContext(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x06000E2C RID: 3628 RVA: 0x0038A3F0 File Offset: 0x0038A3F0
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = ProactiveJsonContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x06000E2D RID: 3629 RVA: 0x0038A420 File Offset: 0x0038A420
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return ProactiveJsonContext.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x06000E2E RID: 3630 RVA: 0x0038A468 File Offset: 0x0038A468
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

    // Token: 0x06000E2F RID: 3631 RVA: 0x0038A4D0 File Offset: 0x0038A4D0
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x06000E30 RID: 3632 RVA: 0x0038A4F0 File Offset: 0x0038A4F0
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
      if (type == typeof(MorningDismissalHistory))
      {
        return this.Create_MorningDismissalHistory(options);
      }
      if (type == typeof(PendingSuggestionJob))
      {
        return this.Create_PendingSuggestionJob(options);
      }
      if (type == typeof(ProactiveAccountState))
      {
        return this.Create_ProactiveAccountState(options);
      }
      if (type == typeof(ProactiveCheckReason))
      {
        return this.Create_ProactiveCheckReason(options);
      }
      if (type == typeof(ProactiveHistoryEntry))
      {
        return this.Create_ProactiveHistoryEntry(options);
      }
      if (type == typeof(ProactiveNewClicky))
      {
        return this.Create_ProactiveNewClicky(options);
      }
      if (type == typeof(ProactiveResolution))
      {
        return this.Create_ProactiveResolution(options);
      }
      if (type == typeof(ProactiveRoutine))
      {
        return this.Create_ProactiveRoutine(options);
      }
      if (type == typeof(ProactiveSlotDayState))
      {
        return this.Create_ProactiveSlotDayState(options);
      }
      if (type == typeof(ProactiveStoreData))
      {
        return this.Create_ProactiveStoreData(options);
      }
      if (type == typeof(ProactiveSuggestionCard))
      {
        return this.Create_ProactiveSuggestionCard(options);
      }
      if (type == typeof(QueuedProactiveTask))
      {
        return this.Create_QueuedProactiveTask(options);
      }
      if (type == typeof(Dictionary<string, ProactiveAccountState>))
      {
        return this.Create_DictionaryStringProactiveAccountState(options);
      }
      if (type == typeof(Dictionary<string, DateTimeOffset>))
      {
        return this.Create_DictionaryStringDateTimeOffset(options);
      }
      if (type == typeof(Dictionary<string, string>))
      {
        return this.Create_DictionaryStringString(options);
      }
      if (type == typeof(List<ProactiveHistoryEntry>))
      {
        return this.Create_ListProactiveHistoryEntry(options);
      }
      if (type == typeof(List<ProactiveSuggestionCard>))
      {
        return this.Create_ListProactiveSuggestionCard(options);
      }
      if (type == typeof(List<QueuedProactiveTask>))
      {
        return this.Create_ListQueuedProactiveTask(options);
      }
      if (type == typeof(List<DateTime>))
      {
        return this.Create_ListDateTime(options);
      }
      if (type == typeof(List<DateTimeOffset>))
      {
        return this.Create_ListDateTimeOffset(options);
      }
      if (type == typeof(List<string>))
      {
        return this.Create_ListString(options);
      }
      if (type == typeof(DateTime))
      {
        return this.Create_DateTime(options);
      }
      if (type == typeof(DateTimeOffset))
      {
        return this.Create_DateTimeOffset(options);
      }
      if (type == typeof(DateTimeOffset?))
      {
        return this.Create_NullableDateTimeOffset(options);
      }
      if (type == typeof(Guid))
      {
        return this.Create_Guid(options);
      }
      if (type == typeof(int))
      {
        return this.Create_Int32(options);
      }
      if (type == typeof(int?))
      {
        return this.Create_NullableInt32(options);
      }
      if (type == typeof(string))
      {
        return this.Create_String(options);
      }
      return null;
    }

    // Token: 0x040005CE RID: 1486
    [Nullable(2)]
    private JsonTypeInfo<bool> _Boolean;

    // Token: 0x040005CF RID: 1487
    [Nullable(2)]
    private JsonTypeInfo<bool?> _NullableBoolean;

    // Token: 0x040005D0 RID: 1488
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<MorningDismissalHistory> _MorningDismissalHistory;

    // Token: 0x040005D1 RID: 1489
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PendingSuggestionJob> _PendingSuggestionJob;

    // Token: 0x040005D2 RID: 1490
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveAccountState> _ProactiveAccountState;

    // Token: 0x040005D3 RID: 1491
    [Nullable(2)]
    private JsonTypeInfo<ProactiveCheckReason> _ProactiveCheckReason;

    // Token: 0x040005D4 RID: 1492
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveHistoryEntry> _ProactiveHistoryEntry;

    // Token: 0x040005D5 RID: 1493
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveNewClicky> _ProactiveNewClicky;

    // Token: 0x040005D6 RID: 1494
    [Nullable(2)]
    private JsonTypeInfo<ProactiveResolution> _ProactiveResolution;

    // Token: 0x040005D7 RID: 1495
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveRoutine> _ProactiveRoutine;

    // Token: 0x040005D8 RID: 1496
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveSlotDayState> _ProactiveSlotDayState;

    // Token: 0x040005D9 RID: 1497
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveStoreData> _ProactiveStoreData;

    // Token: 0x040005DA RID: 1498
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ProactiveSuggestionCard> _ProactiveSuggestionCard;

    // Token: 0x040005DB RID: 1499
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<QueuedProactiveTask> _QueuedProactiveTask;

    // Token: 0x040005DC RID: 1500
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    private JsonTypeInfo<Dictionary<string, ProactiveAccountState>> _DictionaryStringProactiveAccountState;

    // Token: 0x040005DD RID: 1501
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<Dictionary<string, DateTimeOffset>> _DictionaryStringDateTimeOffset;

    // Token: 0x040005DE RID: 1502
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    private JsonTypeInfo<Dictionary<string, string>> _DictionaryStringString;

    // Token: 0x040005DF RID: 1503
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<ProactiveHistoryEntry>> _ListProactiveHistoryEntry;

    // Token: 0x040005E0 RID: 1504
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<ProactiveSuggestionCard>> _ListProactiveSuggestionCard;

    // Token: 0x040005E1 RID: 1505
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<QueuedProactiveTask>> _ListQueuedProactiveTask;

    // Token: 0x040005E2 RID: 1506
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<List<DateTime>> _ListDateTime;

    // Token: 0x040005E3 RID: 1507
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<List<DateTimeOffset>> _ListDateTimeOffset;

    // Token: 0x040005E4 RID: 1508
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<string>> _ListString;

    // Token: 0x040005E5 RID: 1509
    [Nullable(2)]
    private JsonTypeInfo<DateTime> _DateTime;

    // Token: 0x040005E6 RID: 1510
    [Nullable(2)]
    private JsonTypeInfo<DateTimeOffset> _DateTimeOffset;

    // Token: 0x040005E7 RID: 1511
    [Nullable(2)]
    private JsonTypeInfo<DateTimeOffset?> _NullableDateTimeOffset;

    // Token: 0x040005E8 RID: 1512
    [Nullable(2)]
    private JsonTypeInfo<Guid> _Guid;

    // Token: 0x040005E9 RID: 1513
    [Nullable(2)]
    private JsonTypeInfo<int> _Int32;

    // Token: 0x040005EA RID: 1514
    [Nullable(2)]
    private JsonTypeInfo<int?> _NullableInt32;

    // Token: 0x040005EB RID: 1515
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<string> _String;

    // Token: 0x040005EC RID: 1516
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      PropertyNameCaseInsensitive = true,
      PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
      WriteIndented = true
    };

    // Token: 0x040005ED RID: 1517
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    // Token: 0x040005F0 RID: 1520
    private static readonly JsonEncodedText PropName_dismissed_days = JsonEncodedText.Encode("dismissed_days", null);

    // Token: 0x040005F1 RID: 1521
    private static readonly JsonEncodedText PropName_resting_until = JsonEncodedText.Encode("resting_until", null);

    // Token: 0x040005F2 RID: 1522
    private static readonly JsonEncodedText PropName_job_id = JsonEncodedText.Encode("job_id", null);

    // Token: 0x040005F3 RID: 1523
    private static readonly JsonEncodedText PropName_started_at = JsonEncodedText.Encode("started_at", null);

    // Token: 0x040005F4 RID: 1524
    private static readonly JsonEncodedText PropName_check_reason = JsonEncodedText.Encode("check_reason", null);

    // Token: 0x040005F5 RID: 1525
    private static readonly JsonEncodedText PropName_cards = JsonEncodedText.Encode("cards", null);

    // Token: 0x040005F6 RID: 1526
    private static readonly JsonEncodedText PropName_queue = JsonEncodedText.Encode("queue", null);

    // Token: 0x040005F7 RID: 1527
    private static readonly JsonEncodedText PropName_pending_job = JsonEncodedText.Encode("pending_job", null);

    // Token: 0x040005F8 RID: 1528
    private static readonly JsonEncodedText PropName_slots = JsonEncodedText.Encode("slots", null);

    // Token: 0x040005F9 RID: 1529
    private static readonly JsonEncodedText PropName_ignored_suggestions_break_until = JsonEncodedText.Encode("ignored_suggestions_break_until", null);

    // Token: 0x040005FA RID: 1530
    private static readonly JsonEncodedText PropName_suggestions_last_fetched_at = JsonEncodedText.Encode("suggestions_last_fetched_at", null);

    // Token: 0x040005FB RID: 1531
    private static readonly JsonEncodedText PropName_suggestions_row_last_seen_at = JsonEncodedText.Encode("suggestions_row_last_seen_at", null);

    // Token: 0x040005FC RID: 1532
    private static readonly JsonEncodedText PropName_title = JsonEncodedText.Encode("title", null);

    // Token: 0x040005FD RID: 1533
    private static readonly JsonEncodedText PropName_description = JsonEncodedText.Encode("description", null);

    // Token: 0x040005FE RID: 1534
    private static readonly JsonEncodedText PropName_resolution = JsonEncodedText.Encode("resolution", null);

    // Token: 0x040005FF RID: 1535
    private static readonly JsonEncodedText PropName_decided_at = JsonEncodedText.Encode("decided_at", null);

    // Token: 0x04000600 RID: 1536
    private static readonly JsonEncodedText PropName_deny_reason = JsonEncodedText.Encode("deny_reason", null);

    // Token: 0x04000601 RID: 1537
    private static readonly JsonEncodedText PropName_was_viewed = JsonEncodedText.Encode("was_viewed", null);

    // Token: 0x04000602 RID: 1538
    private static readonly JsonEncodedText PropName_wire_status = JsonEncodedText.Encode("wire_status", null);

    // Token: 0x04000603 RID: 1539
    private static readonly JsonEncodedText PropName_name = JsonEncodedText.Encode("name", null);

    // Token: 0x04000604 RID: 1540
    private static readonly JsonEncodedText PropName_role = JsonEncodedText.Encode("role", null);

    // Token: 0x04000605 RID: 1541
    private static readonly JsonEncodedText PropName_every_minutes = JsonEncodedText.Encode("every_minutes", null);

    // Token: 0x04000606 RID: 1542
    private static readonly JsonEncodedText PropName_clamped_interval_minutes = JsonEncodedText.Encode("clamped_interval_minutes", null);

    // Token: 0x04000607 RID: 1543
    private static readonly JsonEncodedText PropName_cadence_label = JsonEncodedText.Encode("cadence_label", null);

    // Token: 0x04000608 RID: 1544
    private static readonly JsonEncodedText PropName_cadence_spoken_label = JsonEncodedText.Encode("cadence_spoken_label", null);

    // Token: 0x04000609 RID: 1545
    private static readonly JsonEncodedText PropName_morning_fired_at = JsonEncodedText.Encode("morning_fired_at", null);

    // Token: 0x0400060A RID: 1546
    private static readonly JsonEncodedText PropName_retry_after = JsonEncodedText.Encode("retry_after", null);

    // Token: 0x0400060B RID: 1547
    private static readonly JsonEncodedText PropName_consecutive_failed_attempts = JsonEncodedText.Encode("consecutive_failed_attempts", null);

    // Token: 0x0400060C RID: 1548
    private static readonly JsonEncodedText PropName_accounts = JsonEncodedText.Encode("accounts", null);

    // Token: 0x0400060D RID: 1549
    private static readonly JsonEncodedText PropName_history = JsonEncodedText.Encode("history", null);

    // Token: 0x0400060E RID: 1550
    private static readonly JsonEncodedText PropName_morning_greeting_last_day = JsonEncodedText.Encode("morning_greeting_last_day", null);

    // Token: 0x0400060F RID: 1551
    private static readonly JsonEncodedText PropName_morning_greeting_last_presented_at = JsonEncodedText.Encode("morning_greeting_last_presented_at", null);

    // Token: 0x04000610 RID: 1552
    private static readonly JsonEncodedText PropName_morning_settled_at = JsonEncodedText.Encode("morning_settled_at", null);

    // Token: 0x04000611 RID: 1553
    private static readonly JsonEncodedText PropName_morning_dismissals = JsonEncodedText.Encode("morning_dismissals", null);

    // Token: 0x04000612 RID: 1554
    private static readonly JsonEncodedText PropName_onboarding_first_completed_at = JsonEncodedText.Encode("onboarding_first_completed_at", null);

    // Token: 0x04000613 RID: 1555
    private static readonly JsonEncodedText PropName_integration_snoozed_until = JsonEncodedText.Encode("integration_snoozed_until", null);

    // Token: 0x04000614 RID: 1556
    private static readonly JsonEncodedText PropName_integration_never_ask = JsonEncodedText.Encode("integration_never_ask", null);

    // Token: 0x04000615 RID: 1557
    private static readonly JsonEncodedText PropName_integration_prompt_timestamps = JsonEncodedText.Encode("integration_prompt_timestamps", null);

    // Token: 0x04000616 RID: 1558
    private static readonly JsonEncodedText PropName_spotify_voice_tip_shown = JsonEncodedText.Encode("spotify_voice_tip_shown", null);

    // Token: 0x04000617 RID: 1559
    private static readonly JsonEncodedText PropName_id = JsonEncodedText.Encode("id", null);

    // Token: 0x04000618 RID: 1560
    private static readonly JsonEncodedText PropName_agent_prompt = JsonEncodedText.Encode("agent_prompt", null);

    // Token: 0x04000619 RID: 1561
    private static readonly JsonEncodedText PropName_server_suggestion_id = JsonEncodedText.Encode("server_suggestion_id", null);

    // Token: 0x0400061A RID: 1562
    private static readonly JsonEncodedText PropName_clicky_slug = JsonEncodedText.Encode("clicky_slug", null);

    // Token: 0x0400061B RID: 1563
    private static readonly JsonEncodedText PropName_new_clicky = JsonEncodedText.Encode("new_clicky", null);

    // Token: 0x0400061C RID: 1564
    private static readonly JsonEncodedText PropName_routine = JsonEncodedText.Encode("routine", null);

    // Token: 0x0400061D RID: 1565
    private static readonly JsonEncodedText PropName_integration_name = JsonEncodedText.Encode("integration_name", null);

    // Token: 0x0400061E RID: 1566
    private static readonly JsonEncodedText PropName_created_at = JsonEncodedText.Encode("created_at", null);

    // Token: 0x0400061F RID: 1567
    private static readonly JsonEncodedText PropName_viewed_at = JsonEncodedText.Encode("viewed_at", null);

    // Token: 0x04000620 RID: 1568
    private static readonly JsonEncodedText PropName_expires_at = JsonEncodedText.Encode("expires_at", null);

    // Token: 0x04000621 RID: 1569
    private static readonly JsonEncodedText PropName_card = JsonEncodedText.Encode("card", null);

    // Token: 0x04000622 RID: 1570
    private static readonly JsonEncodedText PropName_adjustment_text = JsonEncodedText.Encode("adjustment_text", null);

    // Token: 0x04000623 RID: 1571
    private static readonly JsonEncodedText PropName_was_founded = JsonEncodedText.Encode("was_founded", null);

    // Token: 0x04000624 RID: 1572
    private static readonly JsonEncodedText PropName_was_restored = JsonEncodedText.Encode("was_restored", null);

    // Token: 0x04000625 RID: 1573
    private static readonly JsonEncodedText PropName_is_paused = JsonEncodedText.Encode("is_paused", null);

    // Token: 0x04000626 RID: 1574
    private static readonly JsonEncodedText PropName_is_launching = JsonEncodedText.Encode("is_launching", null);

    // Token: 0x020003EA RID: 1002
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04001A40 RID: 6720
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <0>__MorningDismissalHistoryCtorParamInit;

      // Token: 0x04001A41 RID: 6721
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <1>__PendingSuggestionJobCtorParamInit;

      // Token: 0x04001A42 RID: 6722
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <2>__ProactiveHistoryEntryCtorParamInit;

      // Token: 0x04001A43 RID: 6723
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <3>__ProactiveNewClickyCtorParamInit;

      // Token: 0x04001A44 RID: 6724
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <4>__ProactiveRoutineCtorParamInit;

      // Token: 0x04001A45 RID: 6725
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <5>__ProactiveSlotDayStateCtorParamInit;

      // Token: 0x04001A46 RID: 6726
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <6>__ProactiveSuggestionCardCtorParamInit;

      // Token: 0x04001A47 RID: 6727
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <7>__QueuedProactiveTaskCtorParamInit;
    }
  }
}
