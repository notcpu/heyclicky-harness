using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Billing;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core
{
  // Token: 0x02000035 RID: 53
  [NullableContext(1)]
  [Nullable(0)]
  [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString, UseStringEnumConverter = true)]
  [JsonSerializable(typeof(PaywallPayload))]
  [JsonSerializable(typeof(StoredAuthSession))]
  [JsonSerializable(typeof(StoredPendingSignIn))]
  [JsonSerializable(typeof(AppSettings))]
  [JsonSerializable(typeof(PlanSnapshot))]
  [JsonSerializable(typeof(AppConfigResponse))]
  [JsonSerializable(typeof(DeviceRegistrationRequest))]
  [JsonSerializable(typeof(DeviceResponse))]
  [JsonSerializable(typeof(DevicesResponse))]
  [JsonSerializable(typeof(DiscoveryChannelRequest))]
  [JsonSerializable(typeof(DiscoveryChannelResponse))]
  [JsonSerializable(typeof(UserMemoryResponse))]
  [JsonSerializable(typeof(SaveMemoryRequest))]
  [JsonSerializable(typeof(SaveMemoryResponse))]
  [JsonSerializable(typeof(ErrorReport))]
  [JsonSerializable(typeof(StarterPlanRequest))]
  [JsonSerializable(typeof(StarterPlanResponse))]
  [JsonSerializable(typeof(ImageSearchRequest))]
  [JsonSerializable(typeof(ImageSearchResponse))]
  [JsonSerializable(typeof(WebSearchRequest))]
  [JsonSerializable(typeof(WebSearchResponse))]
  [JsonSerializable(typeof(CheckoutRequest))]
  [JsonSerializable(typeof(CheckoutResponse))]
  [JsonSerializable(typeof(PortalResponse))]
  [JsonSerializable(typeof(ReferralClaimRequest))]
  [JsonSerializable(typeof(ReferralCodeRequest))]
  [JsonSerializable(typeof(ReferralOutcomeResponse))]
  [JsonSerializable(typeof(RealtimeSessionRequest))]
  [JsonSerializable(typeof(DeepgramTokenResponse))]
  [JsonSerializable(typeof(EarsTranscribeRequest))]
  [JsonSerializable(typeof(EarsTranscribeResponse))]
  [JsonSerializable(typeof(OnboardingProgress))]
  [JsonSerializable(typeof(DeviceRegistrationStamp))]
  [GeneratedCode("System.Text.Json.SourceGeneration", "9.0.15.2218")]
  internal sealed class CoreJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
  {
    // Token: 0x17000001 RID: 1
    // (get) Token: 0x0600006F RID: 111 RVA: 0x00332CB8 File Offset: 0x00332CB8
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

    // Token: 0x06000070 RID: 112 RVA: 0x00332CF4 File Offset: 0x00332CF4
    private JsonTypeInfo<bool> Create_Boolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool>(options, JsonMetadataServices.BooleanConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000002 RID: 2
    // (get) Token: 0x06000071 RID: 113 RVA: 0x00332D20 File Offset: 0x00332D20
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

    // Token: 0x06000072 RID: 114 RVA: 0x00332D5C File Offset: 0x00332D5C
    private JsonTypeInfo<bool?> Create_NullableBoolean(JsonSerializerOptions options)
    {
      JsonTypeInfo<bool?> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<bool?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<bool>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<bool?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000003 RID: 3
    // (get) Token: 0x06000073 RID: 115 RVA: 0x00332D8C File Offset: 0x00332D8C
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

    // Token: 0x06000074 RID: 116 RVA: 0x00332DC8 File Offset: 0x00332DC8
    private JsonTypeInfo<double> Create_Double(JsonSerializerOptions options)
    {
      JsonTypeInfo<double> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<double>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double>(options, JsonMetadataServices.DoubleConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000004 RID: 4
    // (get) Token: 0x06000075 RID: 117 RVA: 0x00332DF4 File Offset: 0x00332DF4
    [Nullable(0)]
    public JsonTypeInfo<double?> NullableDouble
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<double?> jsonTypeInfo;
        if ((jsonTypeInfo = this._NullableDouble) == null)
        {
          jsonTypeInfo = (this._NullableDouble = (JsonTypeInfo<double?>)base.Options.GetTypeInfo(typeof(double?)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000076 RID: 118 RVA: 0x00332E30 File Offset: 0x00332E30
    private JsonTypeInfo<double?> Create_NullableDouble(JsonSerializerOptions options)
    {
      JsonTypeInfo<double?> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<double?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<double>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<double?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000005 RID: 5
    // (get) Token: 0x06000077 RID: 119 RVA: 0x00332E60 File Offset: 0x00332E60
    [Nullable(0)]
    public JsonTypeInfo<StoredAuthSession> StoredAuthSession
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StoredAuthSession> jsonTypeInfo;
        if ((jsonTypeInfo = this._StoredAuthSession) == null)
        {
          jsonTypeInfo = (this._StoredAuthSession = (JsonTypeInfo<StoredAuthSession>)base.Options.GetTypeInfo(typeof(StoredAuthSession)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000078 RID: 120 RVA: 0x00332E9C File Offset: 0x00332E9C
    private JsonTypeInfo<StoredAuthSession> Create_StoredAuthSession(JsonSerializerOptions options)
    {
      JsonTypeInfo<StoredAuthSession> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StoredAuthSession>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StoredAuthSession> jsonObjectInfoValues = new JsonObjectInfoValues<StoredAuthSession>();
        jsonObjectInfoValues.ObjectCreator = () => new StoredAuthSession();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StoredAuthSessionPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StoredAuthSession).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StoredAuthSession>(this.StoredAuthSessionSerializeHandler);
        JsonObjectInfoValues<StoredAuthSession> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StoredAuthSession>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000079 RID: 121 RVA: 0x00332F70 File Offset: 0x00332F70
    private static JsonPropertyInfo[] StoredAuthSessionPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StoredAuthSession)obj).AccessToken;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).AccessToken = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "AccessToken";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("AccessToken", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StoredAuthSession)obj).RefreshToken;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).RefreshToken = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "RefreshToken";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("RefreshToken", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StoredAuthSession)obj).UserId;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).UserId = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "UserId";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("UserId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StoredAuthSession)obj).Email;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).Email = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Email";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("Email", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((StoredAuthSession)obj).DisplayName;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).DisplayName = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "DisplayName";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("DisplayName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((StoredAuthSession)obj).AvatarUrl;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredAuthSession)obj).AvatarUrl = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "AvatarUrl";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("AvatarUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<double?>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(StoredAuthSession);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((StoredAuthSession)obj).SignupDateEpochSeconds;
      jsonPropertyInfoValues13.Setter = delegate(object obj, double? value)
      {
        ((StoredAuthSession)obj).SignupDateEpochSeconds = value;
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "SignupDateEpochSeconds";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(StoredAuthSession).GetProperty("SignupDateEpochSeconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<double?>(options, jsonPropertyInfoValues14);
      return array;
    }

    // Token: 0x0600007A RID: 122 RVA: 0x003335F8 File Offset: 0x003335F8
    private void StoredAuthSessionSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StoredAuthSession value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string accessToken = value.AccessToken;
      if (accessToken != null)
      {
        writer.WriteString(CoreJsonContext.PropName_access_token, accessToken);
      }
      string refreshToken = value.RefreshToken;
      if (refreshToken != null)
      {
        writer.WriteString(CoreJsonContext.PropName_refresh_token, refreshToken);
      }
      string userId = value.UserId;
      if (userId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_user_id, userId);
      }
      string email = value.Email;
      if (email != null)
      {
        writer.WriteString(CoreJsonContext.PropName_email, email);
      }
      string displayName = value.DisplayName;
      if (displayName != null)
      {
        writer.WriteString(CoreJsonContext.PropName_display_name, displayName);
      }
      string avatarUrl = value.AvatarUrl;
      if (avatarUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_avatar_url, avatarUrl);
      }
      double? signupDateEpochSeconds = value.SignupDateEpochSeconds;
      if (signupDateEpochSeconds != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_signup_date_epoch_seconds);
        JsonSerializer.Serialize<double?>(writer, signupDateEpochSeconds, this.NullableDouble);
      }
      writer.WriteEndObject();
    }

    // Token: 0x17000006 RID: 6
    // (get) Token: 0x0600007B RID: 123 RVA: 0x003336D0 File Offset: 0x003336D0
    [Nullable(0)]
    public JsonTypeInfo<StoredPendingSignIn> StoredPendingSignIn
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StoredPendingSignIn> jsonTypeInfo;
        if ((jsonTypeInfo = this._StoredPendingSignIn) == null)
        {
          jsonTypeInfo = (this._StoredPendingSignIn = (JsonTypeInfo<StoredPendingSignIn>)base.Options.GetTypeInfo(typeof(StoredPendingSignIn)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600007C RID: 124 RVA: 0x0033370C File Offset: 0x0033370C
    private JsonTypeInfo<StoredPendingSignIn> Create_StoredPendingSignIn(JsonSerializerOptions options)
    {
      JsonTypeInfo<StoredPendingSignIn> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StoredPendingSignIn>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StoredPendingSignIn> jsonObjectInfoValues = new JsonObjectInfoValues<StoredPendingSignIn>();
        jsonObjectInfoValues.ObjectCreator = () => new StoredPendingSignIn();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StoredPendingSignInPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StoredPendingSignIn).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StoredPendingSignIn>(this.StoredPendingSignInSerializeHandler);
        JsonObjectInfoValues<StoredPendingSignIn> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StoredPendingSignIn>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600007D RID: 125 RVA: 0x003337E0 File Offset: 0x003337E0
    private static JsonPropertyInfo[] StoredPendingSignInPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StoredPendingSignIn);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StoredPendingSignIn)obj).State;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((StoredPendingSignIn)obj).State = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "State";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StoredPendingSignIn).GetProperty("State", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<double?>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StoredPendingSignIn);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StoredPendingSignIn)obj).StartedAtEpochSeconds;
      jsonPropertyInfoValues3.Setter = delegate(object obj, double? value)
      {
        ((StoredPendingSignIn)obj).StartedAtEpochSeconds = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "StartedAtEpochSeconds";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StoredPendingSignIn).GetProperty("StartedAtEpochSeconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<double?>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x0600007E RID: 126 RVA: 0x003339C8 File Offset: 0x003339C8
    private void StoredPendingSignInSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StoredPendingSignIn value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string state = value.State;
      if (state != null)
      {
        writer.WriteString(CoreJsonContext.PropName_state, state);
      }
      double? startedAtEpochSeconds = value.StartedAtEpochSeconds;
      if (startedAtEpochSeconds != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_started_at_epoch_seconds);
        JsonSerializer.Serialize<double?>(writer, startedAtEpochSeconds, this.NullableDouble);
      }
      writer.WriteEndObject();
    }

    // Token: 0x17000007 RID: 7
    // (get) Token: 0x0600007F RID: 127 RVA: 0x00333A2C File Offset: 0x00333A2C
    [Nullable(0)]
    public JsonTypeInfo<PaywallPayload> PaywallPayload
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PaywallPayload> jsonTypeInfo;
        if ((jsonTypeInfo = this._PaywallPayload) == null)
        {
          jsonTypeInfo = (this._PaywallPayload = (JsonTypeInfo<PaywallPayload>)base.Options.GetTypeInfo(typeof(PaywallPayload)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000080 RID: 128 RVA: 0x00333A68 File Offset: 0x00333A68
    private JsonTypeInfo<PaywallPayload> Create_PaywallPayload(JsonSerializerOptions options)
    {
      JsonTypeInfo<PaywallPayload> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PaywallPayload>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PaywallPayload> jsonObjectInfoValues = new JsonObjectInfoValues<PaywallPayload>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PaywallPayload((string)args[0], (string)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5], (int)args[6], (int)args[7], (string)args[8], (IReadOnlyList<PaywallPlanOption>)args[9], (string)args[10], (string)args[11]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PaywallPayloadPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<0>__PaywallPayloadCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<0>__PaywallPayloadCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PaywallPayloadCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PaywallPayload).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(int),
          typeof(int),
          typeof(string),
          typeof(IReadOnlyList<PaywallPlanOption>),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PaywallPayload>(this.PaywallPayloadSerializeHandler);
        JsonObjectInfoValues<PaywallPayload> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PaywallPayload>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000081 RID: 129 RVA: 0x00333B54 File Offset: 0x00333B54
    private static JsonPropertyInfo[] PaywallPayloadPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[15];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PaywallPayload)obj).Reason;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Reason";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Reason", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PaywallPayload)obj).Surface;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Surface";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Surface", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PaywallPayload)obj).Title;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Title";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((PaywallPayload)obj).Body;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Body";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Body", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((PaywallPayload)obj).CtaLabel;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "CtaLabel";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("CtaLabel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((PaywallPayload)obj).PriceDisplay;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "PriceDisplay";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("PriceDisplay", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      array[5].IsGetNullable = false;
      array[5].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((PaywallPayload)obj).CurrentUsage;
      jsonPropertyInfoValues13.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "CurrentUsage";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("CurrentUsage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((PaywallPayload)obj).Limit;
      jsonPropertyInfoValues15.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "Limit";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Limit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((PaywallPayload)obj).ResetAt;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "ResetAt";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("ResetAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((PaywallPayload)obj).Plans;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<PaywallPlanOption> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "Plans";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("Plans", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<PaywallPlanOption>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<PaywallPlanOption>>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((PaywallPayload)obj).YearlySavingsLabel;
      jsonPropertyInfoValues21.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "YearlySavingsLabel";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("YearlySavingsLabel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((PaywallPayload)obj).DefaultInterval;
      jsonPropertyInfoValues23.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "DefaultInterval";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("DefaultInterval", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = null;
      jsonPropertyInfoValues25.Setter = null;
      jsonPropertyInfoValues25.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "IsPaymentProblem";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("IsPaymentProblem", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues26);
      JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>>();
      jsonPropertyInfoValues27.IsProperty = true;
      jsonPropertyInfoValues27.IsPublic = true;
      jsonPropertyInfoValues27.IsVirtual = false;
      jsonPropertyInfoValues27.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues27.Converter = null;
      jsonPropertyInfoValues27.Getter = null;
      jsonPropertyInfoValues27.Setter = null;
      jsonPropertyInfoValues27.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues27.HasJsonInclude = false;
      jsonPropertyInfoValues27.IsExtensionData = false;
      jsonPropertyInfoValues27.NumberHandling = null;
      jsonPropertyInfoValues27.PropertyName = "UpgradeOptions";
      jsonPropertyInfoValues27.JsonPropertyName = null;
      jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("UpgradeOptions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<PaywallPlanOption>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<PaywallPlanOption>> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
      array[13] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<PaywallPlanOption>>(options, jsonPropertyInfoValues28);
      array[13].IsGetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues29.IsProperty = true;
      jsonPropertyInfoValues29.IsPublic = true;
      jsonPropertyInfoValues29.IsVirtual = false;
      jsonPropertyInfoValues29.DeclaringType = typeof(PaywallPayload);
      jsonPropertyInfoValues29.Converter = null;
      jsonPropertyInfoValues29.Getter = null;
      jsonPropertyInfoValues29.Setter = null;
      jsonPropertyInfoValues29.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues29.HasJsonInclude = false;
      jsonPropertyInfoValues29.IsExtensionData = false;
      jsonPropertyInfoValues29.NumberHandling = null;
      jsonPropertyInfoValues29.PropertyName = "SupportsYearly";
      jsonPropertyInfoValues29.JsonPropertyName = null;
      jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(PaywallPayload).GetProperty("SupportsYearly", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
      array[14] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues30);
      return array;
    }

    // Token: 0x06000082 RID: 130 RVA: 0x003348F0 File Offset: 0x003348F0
    private void PaywallPayloadSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PaywallPayload value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string reason = value.Reason;
      if (reason != null)
      {
        writer.WriteString(CoreJsonContext.PropName_reason, reason);
      }
      string surface = value.Surface;
      if (surface != null)
      {
        writer.WriteString(CoreJsonContext.PropName_surface, surface);
      }
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      string body = value.Body;
      if (body != null)
      {
        writer.WriteString(CoreJsonContext.PropName_body, body);
      }
      string ctaLabel = value.CtaLabel;
      if (ctaLabel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_cta_label, ctaLabel);
      }
      string priceDisplay = value.PriceDisplay;
      if (priceDisplay != null)
      {
        writer.WriteString(CoreJsonContext.PropName_price_display, priceDisplay);
      }
      writer.WriteNumber(CoreJsonContext.PropName_current_usage, value.CurrentUsage);
      writer.WriteNumber(CoreJsonContext.PropName_limit, value.Limit);
      string resetAt = value.ResetAt;
      if (resetAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_reset_at, resetAt);
      }
      IReadOnlyList<PaywallPlanOption> plans = value.Plans;
      if (plans != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_plans);
        this.IReadOnlyListPaywallPlanOptionSerializeHandler(writer, plans);
      }
      string yearlySavingsLabel = value.YearlySavingsLabel;
      if (yearlySavingsLabel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_yearly_savings_label, yearlySavingsLabel);
      }
      string defaultInterval = value.DefaultInterval;
      if (defaultInterval != null)
      {
        writer.WriteString(CoreJsonContext.PropName_default_interval, defaultInterval);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000083 RID: 131 RVA: 0x00334A2C File Offset: 0x00334A2C
    private static JsonParameterInfoValues[] PaywallPayloadCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Reason",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Surface",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Body",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CtaLabel",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "PriceDisplay",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CurrentUsage",
          ParameterType = typeof(int),
          Position = 6,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Limit",
          ParameterType = typeof(int),
          Position = 7,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ResetAt",
          ParameterType = typeof(string),
          Position = 8,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Plans",
          ParameterType = typeof(IReadOnlyList<PaywallPlanOption>),
          Position = 9,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "YearlySavingsLabel",
          ParameterType = typeof(string),
          Position = 10,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "DefaultInterval",
          ParameterType = typeof(string),
          Position = 11,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000008 RID: 8
    // (get) Token: 0x06000084 RID: 132 RVA: 0x00334D3C File Offset: 0x00334D3C
    [Nullable(0)]
    public JsonTypeInfo<PaywallPlanOption> PaywallPlanOption
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PaywallPlanOption> jsonTypeInfo;
        if ((jsonTypeInfo = this._PaywallPlanOption) == null)
        {
          jsonTypeInfo = (this._PaywallPlanOption = (JsonTypeInfo<PaywallPlanOption>)base.Options.GetTypeInfo(typeof(PaywallPlanOption)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000085 RID: 133 RVA: 0x00334D78 File Offset: 0x00334D78
    private JsonTypeInfo<PaywallPlanOption> Create_PaywallPlanOption(JsonSerializerOptions options)
    {
      JsonTypeInfo<PaywallPlanOption> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PaywallPlanOption>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PaywallPlanOption> jsonObjectInfoValues = new JsonObjectInfoValues<PaywallPlanOption>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PaywallPlanOption((string)args[0], (string)args[1], (string)args[2], (int)args[3], (string)args[4], (string)args[5], (bool)args[6], (string)args[7], (string)args[8]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PaywallPlanOptionPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<1>__PaywallPlanOptionCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<1>__PaywallPlanOptionCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PaywallPlanOptionCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PaywallPlanOption).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(int),
          typeof(string),
          typeof(string),
          typeof(bool),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PaywallPlanOption>(this.PaywallPlanOptionSerializeHandler);
        JsonObjectInfoValues<PaywallPlanOption> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PaywallPlanOption>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000086 RID: 134 RVA: 0x00334E64 File Offset: 0x00334E64
    private static JsonPropertyInfo[] PaywallPlanOptionPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[10];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PaywallPlanOption)obj).Key;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Key";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("Key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PaywallPlanOption)obj).Name;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Name";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PaywallPlanOption)obj).PriceDisplay;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "PriceDisplay";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("PriceDisplay", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((PaywallPlanOption)obj).AgentsCap;
      jsonPropertyInfoValues7.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "AgentsCap";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("AgentsCap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((PaywallPlanOption)obj).Tagline;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Tagline";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("Tagline", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((PaywallPlanOption)obj).CtaLabel;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "CtaLabel";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("CtaLabel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      array[5].IsGetNullable = false;
      array[5].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((PaywallPlanOption)obj).Recommended;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "Recommended";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("Recommended", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((PaywallPlanOption)obj).YearlyPriceDisplay;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "YearlyPriceDisplay";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("YearlyPriceDisplay", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((PaywallPlanOption)obj).YearlyBilledSummary;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "YearlyBilledSummary";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("YearlyBilledSummary", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(PaywallPlanOption);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = null;
      jsonPropertyInfoValues19.Setter = null;
      jsonPropertyInfoValues19.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "SupportsYearly";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(PaywallPlanOption).GetProperty("SupportsYearly", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues20);
      return array;
    }

    // Token: 0x06000087 RID: 135 RVA: 0x003357D0 File Offset: 0x003357D0
    private void PaywallPlanOptionSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PaywallPlanOption value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string key = value.Key;
      if (key != null)
      {
        writer.WriteString(CoreJsonContext.PropName_key, key);
      }
      string name = value.Name;
      if (name != null)
      {
        writer.WriteString(CoreJsonContext.PropName_name, name);
      }
      string priceDisplay = value.PriceDisplay;
      if (priceDisplay != null)
      {
        writer.WriteString(CoreJsonContext.PropName_price_display, priceDisplay);
      }
      writer.WriteNumber(CoreJsonContext.PropName_agents_cap, value.AgentsCap);
      string tagline = value.Tagline;
      if (tagline != null)
      {
        writer.WriteString(CoreJsonContext.PropName_tagline, tagline);
      }
      string ctaLabel = value.CtaLabel;
      if (ctaLabel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_cta_label, ctaLabel);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_recommended, value.Recommended);
      string yearlyPriceDisplay = value.YearlyPriceDisplay;
      if (yearlyPriceDisplay != null)
      {
        writer.WriteString(CoreJsonContext.PropName_yearly_price_display, yearlyPriceDisplay);
      }
      string yearlyBilledSummary = value.YearlyBilledSummary;
      if (yearlyBilledSummary != null)
      {
        writer.WriteString(CoreJsonContext.PropName_yearly_billed_summary, yearlyBilledSummary);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000088 RID: 136 RVA: 0x003358B8 File Offset: 0x003358B8
    private static JsonParameterInfoValues[] PaywallPlanOptionCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Key",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Name",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "PriceDisplay",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "AgentsCap",
          ParameterType = typeof(int),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Tagline",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CtaLabel",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Recommended",
          ParameterType = typeof(bool),
          Position = 6,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "YearlyPriceDisplay",
          ParameterType = typeof(string),
          Position = 7,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "YearlyBilledSummary",
          ParameterType = typeof(string),
          Position = 8,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000009 RID: 9
    // (get) Token: 0x06000089 RID: 137 RVA: 0x00335B04 File Offset: 0x00335B04
    [Nullable(0)]
    public JsonTypeInfo<OnboardingProgress> OnboardingProgress
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<OnboardingProgress> jsonTypeInfo;
        if ((jsonTypeInfo = this._OnboardingProgress) == null)
        {
          jsonTypeInfo = (this._OnboardingProgress = (JsonTypeInfo<OnboardingProgress>)base.Options.GetTypeInfo(typeof(OnboardingProgress)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600008A RID: 138 RVA: 0x00335B40 File Offset: 0x00335B40
    private JsonTypeInfo<OnboardingProgress> Create_OnboardingProgress(JsonSerializerOptions options)
    {
      JsonTypeInfo<OnboardingProgress> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<OnboardingProgress>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<OnboardingProgress> jsonObjectInfoValues = new JsonObjectInfoValues<OnboardingProgress>();
        jsonObjectInfoValues.ObjectCreator = () => new OnboardingProgress();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.OnboardingProgressPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(OnboardingProgress).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, OnboardingProgress>(this.OnboardingProgressSerializeHandler);
        JsonObjectInfoValues<OnboardingProgress> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<OnboardingProgress>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600008B RID: 139 RVA: 0x00335C14 File Offset: 0x00335C14
    private static JsonPropertyInfo[] OnboardingProgressPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[8];
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((OnboardingProgress)obj).PreSignInIntroDismissed;
      jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
      {
        ((OnboardingProgress)obj).PreSignInIntroDismissed = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "PreSignInIntroDismissed";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("PreSignInIntroDismissed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((OnboardingProgress)obj).HasAcknowledgedPermissionIntro;
      jsonPropertyInfoValues3.Setter = delegate(object obj, bool value)
      {
        ((OnboardingProgress)obj).HasAcknowledgedPermissionIntro = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "HasAcknowledgedPermissionIntro";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("HasAcknowledgedPermissionIntro", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((OnboardingProgress)obj).HasCompletedOnboarding;
      jsonPropertyInfoValues5.Setter = delegate(object obj, bool value)
      {
        ((OnboardingProgress)obj).HasCompletedOnboarding = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "HasCompletedOnboarding";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("HasCompletedOnboarding", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<double?>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((OnboardingProgress)obj).TutorialCompletedAtEpochSeconds;
      jsonPropertyInfoValues7.Setter = delegate(object obj, double? value)
      {
        ((OnboardingProgress)obj).TutorialCompletedAtEpochSeconds = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "TutorialCompletedAtEpochSeconds";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("TutorialCompletedAtEpochSeconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<double?>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((OnboardingProgress)obj).DiscoveryChannel;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((OnboardingProgress)obj).DiscoveryChannel = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "DiscoveryChannel";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("DiscoveryChannel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((OnboardingProgress)obj).DiscoveryChannelOther;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((OnboardingProgress)obj).DiscoveryChannelOther = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "DiscoveryChannelOther";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("DiscoveryChannelOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((OnboardingProgress)obj).DiscoveryChannelSynced;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        ((OnboardingProgress)obj).DiscoveryChannelSynced = value;
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "DiscoveryChannelSynced";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("DiscoveryChannelSynced", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(OnboardingProgress);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((OnboardingProgress)obj).SquadPackId;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((OnboardingProgress)obj).SquadPackId = value;
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "SquadPackId";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(OnboardingProgress).GetProperty("SquadPackId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
      return array;
    }

    // Token: 0x0600008C RID: 140 RVA: 0x00336388 File Offset: 0x00336388
    private void OnboardingProgressSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] OnboardingProgress value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteBoolean(CoreJsonContext.PropName_pre_sign_in_intro_dismissed, value.PreSignInIntroDismissed);
      writer.WriteBoolean(CoreJsonContext.PropName_has_acknowledged_permission_intro, value.HasAcknowledgedPermissionIntro);
      writer.WriteBoolean(CoreJsonContext.PropName_has_completed_onboarding, value.HasCompletedOnboarding);
      double? tutorialCompletedAtEpochSeconds = value.TutorialCompletedAtEpochSeconds;
      if (tutorialCompletedAtEpochSeconds != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_tutorial_completed_at_epoch_seconds);
        JsonSerializer.Serialize<double?>(writer, tutorialCompletedAtEpochSeconds, this.NullableDouble);
      }
      string discoveryChannel = value.DiscoveryChannel;
      if (discoveryChannel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel, discoveryChannel);
      }
      string discoveryChannelOther = value.DiscoveryChannelOther;
      if (discoveryChannelOther != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel_other, discoveryChannelOther);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_discovery_channel_synced, value.DiscoveryChannelSynced);
      string squadPackId = value.SquadPackId;
      if (squadPackId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_squad_pack_id, squadPackId);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700000A RID: 10
    // (get) Token: 0x0600008D RID: 141 RVA: 0x0033645C File Offset: 0x0033645C
    [Nullable(0)]
    public JsonTypeInfo<AppSettings> AppSettings
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<AppSettings> jsonTypeInfo;
        if ((jsonTypeInfo = this._AppSettings) == null)
        {
          jsonTypeInfo = (this._AppSettings = (JsonTypeInfo<AppSettings>)base.Options.GetTypeInfo(typeof(AppSettings)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600008E RID: 142 RVA: 0x00336498 File Offset: 0x00336498
    private JsonTypeInfo<AppSettings> Create_AppSettings(JsonSerializerOptions options)
    {
      JsonTypeInfo<AppSettings> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<AppSettings>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<AppSettings> jsonObjectInfoValues = new JsonObjectInfoValues<AppSettings>();
        jsonObjectInfoValues.ObjectCreator = () => new AppSettings();
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = null;
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.AppSettingsPropInit(options);
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = null;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(AppSettings).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, AppSettings>(this.AppSettingsSerializeHandler);
        JsonObjectInfoValues<AppSettings> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<AppSettings>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600008F RID: 143 RVA: 0x0033656C File Offset: 0x0033656C
    private static JsonPropertyInfo[] AppSettingsPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[60];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((AppSettings)obj).CursorColorHex;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).CursorColorHex = value;
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "CursorColorHex";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("CursorColorHex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((AppSettings)obj).SquadMemberId;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).SquadMemberId = value;
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "SquadMemberId";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("SquadMemberId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((AppSettings)obj).VoiceIdentifier;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).VoiceIdentifier = value;
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "VoiceIdentifier";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("VoiceIdentifier", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<double> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<double>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((AppSettings)obj).SpeechSpeed;
      jsonPropertyInfoValues7.Setter = delegate(object obj, double value)
      {
        ((AppSettings)obj).SpeechSpeed = value;
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "SpeechSpeed";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("SpeechSpeed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<double>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((AppSettings)obj).VoiceLanguageCode;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).VoiceLanguageCode = value;
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "VoiceLanguageCode";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("VoiceLanguageCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((AppSettings)obj).ScreenHighlightEnabled;
      jsonPropertyInfoValues11.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ScreenHighlightEnabled = value;
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ScreenHighlightEnabled";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ScreenHighlightEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((AppSettings)obj).CursorDocked;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).CursorDocked = value;
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "CursorDocked";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("CursorDocked", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((AppSettings)obj).ShakeToDetachEnabled;
      jsonPropertyInfoValues15.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ShakeToDetachEnabled = value;
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "ShakeToDetachEnabled";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ShakeToDetachEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((AppSettings)obj).LaunchAtLogin;
      jsonPropertyInfoValues17.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).LaunchAtLogin = value;
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "LaunchAtLogin";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("LaunchAtLogin", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((AppSettings)obj).ShowInTaskbar;
      jsonPropertyInfoValues19.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ShowInTaskbar = value;
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "ShowInTaskbar";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ShowInTaskbar", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((AppSettings)obj).TypedTurnsRespondWithVoice;
      jsonPropertyInfoValues21.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).TypedTurnsRespondWithVoice = value;
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "TypedTurnsRespondWithVoice";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("TypedTurnsRespondWithVoice", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((AppSettings)obj).DictationLanguageCode;
      jsonPropertyInfoValues23.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).DictationLanguageCode = value;
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "DictationLanguageCode";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationLanguageCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues24);
      array[11].IsGetNullable = false;
      array[11].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = (object obj) => ((AppSettings)obj).DictationSpecificLanguageCode;
      jsonPropertyInfoValues25.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).DictationSpecificLanguageCode = value;
      };
      jsonPropertyInfoValues25.IgnoreCondition = null;
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "DictationSpecificLanguageCode";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationSpecificLanguageCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues26);
      array[12].IsGetNullable = false;
      array[12].IsSetNullable = false;
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<double?>();
      jsonPropertyInfoValues27.IsProperty = true;
      jsonPropertyInfoValues27.IsPublic = true;
      jsonPropertyInfoValues27.IsVirtual = false;
      jsonPropertyInfoValues27.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues27.Converter = null;
      jsonPropertyInfoValues27.Getter = (object obj) => ((AppSettings)obj).DictationLastCompletedAtEpochSeconds;
      jsonPropertyInfoValues27.Setter = delegate(object obj, double? value)
      {
        ((AppSettings)obj).DictationLastCompletedAtEpochSeconds = value;
      };
      jsonPropertyInfoValues27.IgnoreCondition = null;
      jsonPropertyInfoValues27.HasJsonInclude = false;
      jsonPropertyInfoValues27.IsExtensionData = false;
      jsonPropertyInfoValues27.NumberHandling = null;
      jsonPropertyInfoValues27.PropertyName = "DictationLastCompletedAtEpochSeconds";
      jsonPropertyInfoValues27.JsonPropertyName = null;
      jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationLastCompletedAtEpochSeconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
      array[13] = JsonMetadataServices.CreatePropertyInfo<double?>(options, jsonPropertyInfoValues28);
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<List<string>>();
      jsonPropertyInfoValues29.IsProperty = true;
      jsonPropertyInfoValues29.IsPublic = true;
      jsonPropertyInfoValues29.IsVirtual = false;
      jsonPropertyInfoValues29.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues29.Converter = null;
      jsonPropertyInfoValues29.Getter = (object obj) => ((AppSettings)obj).DictationCustomWords;
      jsonPropertyInfoValues29.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] List<string> value)
      {
        ((AppSettings)obj).DictationCustomWords = value;
      };
      jsonPropertyInfoValues29.IgnoreCondition = null;
      jsonPropertyInfoValues29.HasJsonInclude = false;
      jsonPropertyInfoValues29.IsExtensionData = false;
      jsonPropertyInfoValues29.NumberHandling = null;
      jsonPropertyInfoValues29.PropertyName = "DictationCustomWords";
      jsonPropertyInfoValues29.JsonPropertyName = null;
      jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationCustomWords", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(List<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<List<string>> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
      array[14] = JsonMetadataServices.CreatePropertyInfo<List<string>>(options, jsonPropertyInfoValues30);
      array[14].IsGetNullable = false;
      array[14].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues31 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues31.IsProperty = true;
      jsonPropertyInfoValues31.IsPublic = true;
      jsonPropertyInfoValues31.IsVirtual = false;
      jsonPropertyInfoValues31.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues31.Converter = null;
      jsonPropertyInfoValues31.Getter = (object obj) => ((AppSettings)obj).DictationClaudeSeeded;
      jsonPropertyInfoValues31.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DictationClaudeSeeded = value;
      };
      jsonPropertyInfoValues31.IgnoreCondition = null;
      jsonPropertyInfoValues31.HasJsonInclude = false;
      jsonPropertyInfoValues31.IsExtensionData = false;
      jsonPropertyInfoValues31.NumberHandling = null;
      jsonPropertyInfoValues31.PropertyName = "DictationClaudeSeeded";
      jsonPropertyInfoValues31.JsonPropertyName = null;
      jsonPropertyInfoValues31.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationClaudeSeeded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues32 = jsonPropertyInfoValues31;
      array[15] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues32);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues33 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues33.IsProperty = true;
      jsonPropertyInfoValues33.IsPublic = true;
      jsonPropertyInfoValues33.IsVirtual = false;
      jsonPropertyInfoValues33.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues33.Converter = null;
      jsonPropertyInfoValues33.Getter = (object obj) => ((AppSettings)obj).DictationAccountNameSeeded;
      jsonPropertyInfoValues33.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).DictationAccountNameSeeded = value;
      };
      jsonPropertyInfoValues33.IgnoreCondition = null;
      jsonPropertyInfoValues33.HasJsonInclude = false;
      jsonPropertyInfoValues33.IsExtensionData = false;
      jsonPropertyInfoValues33.NumberHandling = null;
      jsonPropertyInfoValues33.PropertyName = "DictationAccountNameSeeded";
      jsonPropertyInfoValues33.JsonPropertyName = null;
      jsonPropertyInfoValues33.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DictationAccountNameSeeded", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues34 = jsonPropertyInfoValues33;
      array[16] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues34);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues35 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues35.IsProperty = true;
      jsonPropertyInfoValues35.IsPublic = true;
      jsonPropertyInfoValues35.IsVirtual = false;
      jsonPropertyInfoValues35.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues35.Converter = null;
      jsonPropertyInfoValues35.Getter = (object obj) => ((AppSettings)obj).DiscoveryChannelPendingSync;
      jsonPropertyInfoValues35.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).DiscoveryChannelPendingSync = value;
      };
      jsonPropertyInfoValues35.IgnoreCondition = null;
      jsonPropertyInfoValues35.HasJsonInclude = false;
      jsonPropertyInfoValues35.IsExtensionData = false;
      jsonPropertyInfoValues35.NumberHandling = null;
      jsonPropertyInfoValues35.PropertyName = "DiscoveryChannelPendingSync";
      jsonPropertyInfoValues35.JsonPropertyName = null;
      jsonPropertyInfoValues35.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DiscoveryChannelPendingSync", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues36 = jsonPropertyInfoValues35;
      array[17] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues36);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues37 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues37.IsProperty = true;
      jsonPropertyInfoValues37.IsPublic = true;
      jsonPropertyInfoValues37.IsVirtual = false;
      jsonPropertyInfoValues37.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues37.Converter = null;
      jsonPropertyInfoValues37.Getter = (object obj) => ((AppSettings)obj).MicrophoneDeviceName;
      jsonPropertyInfoValues37.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).MicrophoneDeviceName = value;
      };
      jsonPropertyInfoValues37.IgnoreCondition = null;
      jsonPropertyInfoValues37.HasJsonInclude = false;
      jsonPropertyInfoValues37.IsExtensionData = false;
      jsonPropertyInfoValues37.NumberHandling = null;
      jsonPropertyInfoValues37.PropertyName = "MicrophoneDeviceName";
      jsonPropertyInfoValues37.JsonPropertyName = null;
      jsonPropertyInfoValues37.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("MicrophoneDeviceName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues38 = jsonPropertyInfoValues37;
      array[18] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues38);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues39 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues39.IsProperty = true;
      jsonPropertyInfoValues39.IsPublic = true;
      jsonPropertyInfoValues39.IsVirtual = false;
      jsonPropertyInfoValues39.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues39.Converter = null;
      jsonPropertyInfoValues39.Getter = (object obj) => ((AppSettings)obj).AgentAnnouncementsVoiceEnabled;
      jsonPropertyInfoValues39.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).AgentAnnouncementsVoiceEnabled = value;
      };
      jsonPropertyInfoValues39.IgnoreCondition = null;
      jsonPropertyInfoValues39.HasJsonInclude = false;
      jsonPropertyInfoValues39.IsExtensionData = false;
      jsonPropertyInfoValues39.NumberHandling = null;
      jsonPropertyInfoValues39.PropertyName = "AgentAnnouncementsVoiceEnabled";
      jsonPropertyInfoValues39.JsonPropertyName = null;
      jsonPropertyInfoValues39.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("AgentAnnouncementsVoiceEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues40 = jsonPropertyInfoValues39;
      array[19] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues40);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues41 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues41.IsProperty = true;
      jsonPropertyInfoValues41.IsPublic = true;
      jsonPropertyInfoValues41.IsVirtual = false;
      jsonPropertyInfoValues41.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues41.Converter = null;
      jsonPropertyInfoValues41.Getter = (object obj) => ((AppSettings)obj).DocumentAccessNoticeShowCount;
      jsonPropertyInfoValues41.Setter = delegate(object obj, int value)
      {
        ((AppSettings)obj).DocumentAccessNoticeShowCount = value;
      };
      jsonPropertyInfoValues41.IgnoreCondition = null;
      jsonPropertyInfoValues41.HasJsonInclude = false;
      jsonPropertyInfoValues41.IsExtensionData = false;
      jsonPropertyInfoValues41.NumberHandling = null;
      jsonPropertyInfoValues41.PropertyName = "DocumentAccessNoticeShowCount";
      jsonPropertyInfoValues41.JsonPropertyName = null;
      jsonPropertyInfoValues41.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DocumentAccessNoticeShowCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues42 = jsonPropertyInfoValues41;
      array[20] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues42);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues43 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues43.IsProperty = true;
      jsonPropertyInfoValues43.IsPublic = true;
      jsonPropertyInfoValues43.IsVirtual = false;
      jsonPropertyInfoValues43.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues43.Converter = null;
      jsonPropertyInfoValues43.Getter = (object obj) => ((AppSettings)obj).DocumentAccessNoticeSuppressed;
      jsonPropertyInfoValues43.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DocumentAccessNoticeSuppressed = value;
      };
      jsonPropertyInfoValues43.IgnoreCondition = null;
      jsonPropertyInfoValues43.HasJsonInclude = false;
      jsonPropertyInfoValues43.IsExtensionData = false;
      jsonPropertyInfoValues43.NumberHandling = null;
      jsonPropertyInfoValues43.PropertyName = "DocumentAccessNoticeSuppressed";
      jsonPropertyInfoValues43.JsonPropertyName = null;
      jsonPropertyInfoValues43.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DocumentAccessNoticeSuppressed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues44 = jsonPropertyInfoValues43;
      array[21] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues44);
      JsonPropertyInfoValues<OnboardingProgress> jsonPropertyInfoValues45 = new JsonPropertyInfoValues<OnboardingProgress>();
      jsonPropertyInfoValues45.IsProperty = true;
      jsonPropertyInfoValues45.IsPublic = true;
      jsonPropertyInfoValues45.IsVirtual = false;
      jsonPropertyInfoValues45.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues45.Converter = null;
      jsonPropertyInfoValues45.Getter = (object obj) => ((AppSettings)obj).Onboarding;
      jsonPropertyInfoValues45.Setter = delegate(object obj, [Nullable(2)] OnboardingProgress value)
      {
        ((AppSettings)obj).Onboarding = value;
      };
      jsonPropertyInfoValues45.IgnoreCondition = null;
      jsonPropertyInfoValues45.HasJsonInclude = false;
      jsonPropertyInfoValues45.IsExtensionData = false;
      jsonPropertyInfoValues45.NumberHandling = null;
      jsonPropertyInfoValues45.PropertyName = "Onboarding";
      jsonPropertyInfoValues45.JsonPropertyName = null;
      jsonPropertyInfoValues45.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("Onboarding", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(OnboardingProgress), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<OnboardingProgress> jsonPropertyInfoValues46 = jsonPropertyInfoValues45;
      array[22] = JsonMetadataServices.CreatePropertyInfo<OnboardingProgress>(options, jsonPropertyInfoValues46);
      array[22].IsGetNullable = false;
      array[22].IsSetNullable = false;
      JsonPropertyInfoValues<HotkeyBindings> jsonPropertyInfoValues47 = new JsonPropertyInfoValues<HotkeyBindings>();
      jsonPropertyInfoValues47.IsProperty = true;
      jsonPropertyInfoValues47.IsPublic = true;
      jsonPropertyInfoValues47.IsVirtual = false;
      jsonPropertyInfoValues47.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues47.Converter = null;
      jsonPropertyInfoValues47.Getter = (object obj) => ((AppSettings)obj).Hotkeys;
      jsonPropertyInfoValues47.Setter = delegate(object obj, [Nullable(2)] HotkeyBindings value)
      {
        ((AppSettings)obj).Hotkeys = value;
      };
      jsonPropertyInfoValues47.IgnoreCondition = null;
      jsonPropertyInfoValues47.HasJsonInclude = false;
      jsonPropertyInfoValues47.IsExtensionData = false;
      jsonPropertyInfoValues47.NumberHandling = null;
      jsonPropertyInfoValues47.PropertyName = "Hotkeys";
      jsonPropertyInfoValues47.JsonPropertyName = null;
      jsonPropertyInfoValues47.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("Hotkeys", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBindings), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBindings> jsonPropertyInfoValues48 = jsonPropertyInfoValues47;
      array[23] = JsonMetadataServices.CreatePropertyInfo<HotkeyBindings>(options, jsonPropertyInfoValues48);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues49 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues49.IsProperty = true;
      jsonPropertyInfoValues49.IsPublic = true;
      jsonPropertyInfoValues49.IsVirtual = false;
      jsonPropertyInfoValues49.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues49.Converter = null;
      jsonPropertyInfoValues49.Getter = (object obj) => ((AppSettings)obj).MicrophoneDeviceId;
      jsonPropertyInfoValues49.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).MicrophoneDeviceId = value;
      };
      jsonPropertyInfoValues49.IgnoreCondition = null;
      jsonPropertyInfoValues49.HasJsonInclude = false;
      jsonPropertyInfoValues49.IsExtensionData = false;
      jsonPropertyInfoValues49.NumberHandling = null;
      jsonPropertyInfoValues49.PropertyName = "MicrophoneDeviceId";
      jsonPropertyInfoValues49.JsonPropertyName = null;
      jsonPropertyInfoValues49.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("MicrophoneDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues50 = jsonPropertyInfoValues49;
      array[24] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues50);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues51 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues51.IsProperty = true;
      jsonPropertyInfoValues51.IsPublic = true;
      jsonPropertyInfoValues51.IsVirtual = false;
      jsonPropertyInfoValues51.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues51.Converter = null;
      jsonPropertyInfoValues51.Getter = (object obj) => ((AppSettings)obj).OutputDeviceId;
      jsonPropertyInfoValues51.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).OutputDeviceId = value;
      };
      jsonPropertyInfoValues51.IgnoreCondition = null;
      jsonPropertyInfoValues51.HasJsonInclude = false;
      jsonPropertyInfoValues51.IsExtensionData = false;
      jsonPropertyInfoValues51.NumberHandling = null;
      jsonPropertyInfoValues51.PropertyName = "OutputDeviceId";
      jsonPropertyInfoValues51.JsonPropertyName = null;
      jsonPropertyInfoValues51.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("OutputDeviceId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues52 = jsonPropertyInfoValues51;
      array[25] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues52);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues53 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues53.IsProperty = true;
      jsonPropertyInfoValues53.IsPublic = true;
      jsonPropertyInfoValues53.IsVirtual = false;
      jsonPropertyInfoValues53.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues53.Converter = null;
      jsonPropertyInfoValues53.Getter = (object obj) => ((AppSettings)obj).DrawingSoundsEnabled;
      jsonPropertyInfoValues53.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DrawingSoundsEnabled = value;
      };
      jsonPropertyInfoValues53.IgnoreCondition = null;
      jsonPropertyInfoValues53.HasJsonInclude = false;
      jsonPropertyInfoValues53.IsExtensionData = false;
      jsonPropertyInfoValues53.NumberHandling = null;
      jsonPropertyInfoValues53.PropertyName = "DrawingSoundsEnabled";
      jsonPropertyInfoValues53.JsonPropertyName = null;
      jsonPropertyInfoValues53.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingSoundsEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues54 = jsonPropertyInfoValues53;
      array[26] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues54);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues55 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues55.IsProperty = true;
      jsonPropertyInfoValues55.IsPublic = true;
      jsonPropertyInfoValues55.IsVirtual = false;
      jsonPropertyInfoValues55.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues55.Converter = null;
      jsonPropertyInfoValues55.Getter = (object obj) => ((AppSettings)obj).DrawingVoiceCaptions;
      jsonPropertyInfoValues55.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DrawingVoiceCaptions = value;
      };
      jsonPropertyInfoValues55.IgnoreCondition = null;
      jsonPropertyInfoValues55.HasJsonInclude = false;
      jsonPropertyInfoValues55.IsExtensionData = false;
      jsonPropertyInfoValues55.NumberHandling = null;
      jsonPropertyInfoValues55.PropertyName = "DrawingVoiceCaptions";
      jsonPropertyInfoValues55.JsonPropertyName = null;
      jsonPropertyInfoValues55.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingVoiceCaptions", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues56 = jsonPropertyInfoValues55;
      array[27] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues56);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues57 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues57.IsProperty = true;
      jsonPropertyInfoValues57.IsPublic = true;
      jsonPropertyInfoValues57.IsVirtual = false;
      jsonPropertyInfoValues57.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues57.Converter = null;
      jsonPropertyInfoValues57.Getter = (object obj) => ((AppSettings)obj).KeepDrawingsInChat;
      jsonPropertyInfoValues57.Setter = delegate(object obj, bool? value)
      {
        ((AppSettings)obj).KeepDrawingsInChat = value;
      };
      jsonPropertyInfoValues57.IgnoreCondition = null;
      jsonPropertyInfoValues57.HasJsonInclude = false;
      jsonPropertyInfoValues57.IsExtensionData = false;
      jsonPropertyInfoValues57.NumberHandling = null;
      jsonPropertyInfoValues57.PropertyName = "KeepDrawingsInChat";
      jsonPropertyInfoValues57.JsonPropertyName = null;
      jsonPropertyInfoValues57.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("KeepDrawingsInChat", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues58 = jsonPropertyInfoValues57;
      array[28] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues58);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues59 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues59.IsProperty = true;
      jsonPropertyInfoValues59.IsPublic = true;
      jsonPropertyInfoValues59.IsVirtual = false;
      jsonPropertyInfoValues59.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues59.Converter = null;
      jsonPropertyInfoValues59.Getter = null;
      jsonPropertyInfoValues59.Setter = null;
      jsonPropertyInfoValues59.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues59.HasJsonInclude = false;
      jsonPropertyInfoValues59.IsExtensionData = false;
      jsonPropertyInfoValues59.NumberHandling = null;
      jsonPropertyInfoValues59.PropertyName = "KeepDrawingsInChatEffective";
      jsonPropertyInfoValues59.JsonPropertyName = null;
      jsonPropertyInfoValues59.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("KeepDrawingsInChatEffective", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues60 = jsonPropertyInfoValues59;
      array[29] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues60);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues61 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues61.IsProperty = true;
      jsonPropertyInfoValues61.IsPublic = true;
      jsonPropertyInfoValues61.IsVirtual = false;
      jsonPropertyInfoValues61.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues61.Converter = null;
      jsonPropertyInfoValues61.Getter = (object obj) => ((AppSettings)obj).DrawingPenSoundsEnabled;
      jsonPropertyInfoValues61.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DrawingPenSoundsEnabled = value;
      };
      jsonPropertyInfoValues61.IgnoreCondition = null;
      jsonPropertyInfoValues61.HasJsonInclude = false;
      jsonPropertyInfoValues61.IsExtensionData = false;
      jsonPropertyInfoValues61.NumberHandling = null;
      jsonPropertyInfoValues61.PropertyName = "DrawingPenSoundsEnabled";
      jsonPropertyInfoValues61.JsonPropertyName = null;
      jsonPropertyInfoValues61.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingPenSoundsEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues62 = jsonPropertyInfoValues61;
      array[30] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues62);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues63 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues63.IsProperty = true;
      jsonPropertyInfoValues63.IsPublic = true;
      jsonPropertyInfoValues63.IsVirtual = false;
      jsonPropertyInfoValues63.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues63.Converter = null;
      jsonPropertyInfoValues63.Getter = (object obj) => ((AppSettings)obj).DrawingJavaScriptJit;
      jsonPropertyInfoValues63.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).DrawingJavaScriptJit = value;
      };
      jsonPropertyInfoValues63.IgnoreCondition = null;
      jsonPropertyInfoValues63.HasJsonInclude = false;
      jsonPropertyInfoValues63.IsExtensionData = false;
      jsonPropertyInfoValues63.NumberHandling = null;
      jsonPropertyInfoValues63.PropertyName = "DrawingJavaScriptJit";
      jsonPropertyInfoValues63.JsonPropertyName = null;
      jsonPropertyInfoValues63.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingJavaScriptJit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues64 = jsonPropertyInfoValues63;
      array[31] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues64);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues65 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues65.IsProperty = true;
      jsonPropertyInfoValues65.IsPublic = true;
      jsonPropertyInfoValues65.IsVirtual = false;
      jsonPropertyInfoValues65.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues65.Converter = null;
      jsonPropertyInfoValues65.Getter = (object obj) => ((AppSettings)obj).DrawingSubtitles;
      jsonPropertyInfoValues65.Setter = delegate(object obj, bool? value)
      {
        ((AppSettings)obj).DrawingSubtitles = value;
      };
      jsonPropertyInfoValues65.IgnoreCondition = null;
      jsonPropertyInfoValues65.HasJsonInclude = false;
      jsonPropertyInfoValues65.IsExtensionData = false;
      jsonPropertyInfoValues65.NumberHandling = null;
      jsonPropertyInfoValues65.PropertyName = "DrawingSubtitles";
      jsonPropertyInfoValues65.JsonPropertyName = null;
      jsonPropertyInfoValues65.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingSubtitles", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues66 = jsonPropertyInfoValues65;
      array[32] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues66);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues67 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues67.IsProperty = true;
      jsonPropertyInfoValues67.IsPublic = true;
      jsonPropertyInfoValues67.IsVirtual = false;
      jsonPropertyInfoValues67.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues67.Converter = null;
      jsonPropertyInfoValues67.Getter = null;
      jsonPropertyInfoValues67.Setter = null;
      jsonPropertyInfoValues67.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues67.HasJsonInclude = false;
      jsonPropertyInfoValues67.IsExtensionData = false;
      jsonPropertyInfoValues67.NumberHandling = null;
      jsonPropertyInfoValues67.PropertyName = "DrawingSubtitlesEffective";
      jsonPropertyInfoValues67.JsonPropertyName = null;
      jsonPropertyInfoValues67.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DrawingSubtitlesEffective", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues68 = jsonPropertyInfoValues67;
      array[33] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues68);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues69 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues69.IsProperty = true;
      jsonPropertyInfoValues69.IsPublic = true;
      jsonPropertyInfoValues69.IsVirtual = false;
      jsonPropertyInfoValues69.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues69.Converter = null;
      jsonPropertyInfoValues69.Getter = (object obj) => ((AppSettings)obj).SaveDrawings;
      jsonPropertyInfoValues69.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).SaveDrawings = value;
      };
      jsonPropertyInfoValues69.IgnoreCondition = null;
      jsonPropertyInfoValues69.HasJsonInclude = false;
      jsonPropertyInfoValues69.IsExtensionData = false;
      jsonPropertyInfoValues69.NumberHandling = null;
      jsonPropertyInfoValues69.PropertyName = "SaveDrawings";
      jsonPropertyInfoValues69.JsonPropertyName = null;
      jsonPropertyInfoValues69.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("SaveDrawings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues70 = jsonPropertyInfoValues69;
      array[34] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues70);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues71 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues71.IsProperty = true;
      jsonPropertyInfoValues71.IsPublic = true;
      jsonPropertyInfoValues71.IsVirtual = false;
      jsonPropertyInfoValues71.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues71.Converter = null;
      jsonPropertyInfoValues71.Getter = null;
      jsonPropertyInfoValues71.Setter = null;
      jsonPropertyInfoValues71.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues71.HasJsonInclude = false;
      jsonPropertyInfoValues71.IsExtensionData = false;
      jsonPropertyInfoValues71.NumberHandling = null;
      jsonPropertyInfoValues71.PropertyName = "SaveDrawingsEffective";
      jsonPropertyInfoValues71.JsonPropertyName = null;
      jsonPropertyInfoValues71.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("SaveDrawingsEffective", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues72 = jsonPropertyInfoValues71;
      array[35] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues72);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues73 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues73.IsProperty = true;
      jsonPropertyInfoValues73.IsPublic = true;
      jsonPropertyInfoValues73.IsVirtual = false;
      jsonPropertyInfoValues73.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues73.Converter = null;
      jsonPropertyInfoValues73.Getter = (object obj) => ((AppSettings)obj).ShowAgentLabelsInActivityStrip;
      jsonPropertyInfoValues73.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ShowAgentLabelsInActivityStrip = value;
      };
      jsonPropertyInfoValues73.IgnoreCondition = null;
      jsonPropertyInfoValues73.HasJsonInclude = false;
      jsonPropertyInfoValues73.IsExtensionData = false;
      jsonPropertyInfoValues73.NumberHandling = null;
      jsonPropertyInfoValues73.PropertyName = "ShowAgentLabelsInActivityStrip";
      jsonPropertyInfoValues73.JsonPropertyName = null;
      jsonPropertyInfoValues73.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ShowAgentLabelsInActivityStrip", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues74 = jsonPropertyInfoValues73;
      array[36] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues74);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues75 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues75.IsProperty = true;
      jsonPropertyInfoValues75.IsPublic = true;
      jsonPropertyInfoValues75.IsVirtual = false;
      jsonPropertyInfoValues75.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues75.Converter = null;
      jsonPropertyInfoValues75.Getter = (object obj) => ((AppSettings)obj).ShowInScreenRecordings;
      jsonPropertyInfoValues75.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ShowInScreenRecordings = value;
      };
      jsonPropertyInfoValues75.IgnoreCondition = null;
      jsonPropertyInfoValues75.HasJsonInclude = false;
      jsonPropertyInfoValues75.IsExtensionData = false;
      jsonPropertyInfoValues75.NumberHandling = null;
      jsonPropertyInfoValues75.PropertyName = "ShowInScreenRecordings";
      jsonPropertyInfoValues75.JsonPropertyName = null;
      jsonPropertyInfoValues75.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ShowInScreenRecordings", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues76 = jsonPropertyInfoValues75;
      array[37] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues76);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues77 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues77.IsProperty = true;
      jsonPropertyInfoValues77.IsPublic = true;
      jsonPropertyInfoValues77.IsVirtual = false;
      jsonPropertyInfoValues77.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues77.Converter = null;
      jsonPropertyInfoValues77.Getter = (object obj) => ((AppSettings)obj).AgentFolderPath;
      jsonPropertyInfoValues77.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).AgentFolderPath = value;
      };
      jsonPropertyInfoValues77.IgnoreCondition = null;
      jsonPropertyInfoValues77.HasJsonInclude = false;
      jsonPropertyInfoValues77.IsExtensionData = false;
      jsonPropertyInfoValues77.NumberHandling = null;
      jsonPropertyInfoValues77.PropertyName = "AgentFolderPath";
      jsonPropertyInfoValues77.JsonPropertyName = null;
      jsonPropertyInfoValues77.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("AgentFolderPath", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues78 = jsonPropertyInfoValues77;
      array[38] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues78);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues79 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues79.IsProperty = true;
      jsonPropertyInfoValues79.IsPublic = true;
      jsonPropertyInfoValues79.IsVirtual = false;
      jsonPropertyInfoValues79.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues79.Converter = null;
      jsonPropertyInfoValues79.Getter = (object obj) => ((AppSettings)obj).ObsidianVaultPath;
      jsonPropertyInfoValues79.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).ObsidianVaultPath = value;
      };
      jsonPropertyInfoValues79.IgnoreCondition = null;
      jsonPropertyInfoValues79.HasJsonInclude = false;
      jsonPropertyInfoValues79.IsExtensionData = false;
      jsonPropertyInfoValues79.NumberHandling = null;
      jsonPropertyInfoValues79.PropertyName = "ObsidianVaultPath";
      jsonPropertyInfoValues79.JsonPropertyName = null;
      jsonPropertyInfoValues79.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ObsidianVaultPath", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues80 = jsonPropertyInfoValues79;
      array[39] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues80);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues81 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues81.IsProperty = true;
      jsonPropertyInfoValues81.IsPublic = true;
      jsonPropertyInfoValues81.IsVirtual = false;
      jsonPropertyInfoValues81.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues81.Converter = null;
      jsonPropertyInfoValues81.Getter = (object obj) => ((AppSettings)obj).AgentAutoApproveExtraUsage;
      jsonPropertyInfoValues81.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).AgentAutoApproveExtraUsage = value;
      };
      jsonPropertyInfoValues81.IgnoreCondition = null;
      jsonPropertyInfoValues81.HasJsonInclude = false;
      jsonPropertyInfoValues81.IsExtensionData = false;
      jsonPropertyInfoValues81.NumberHandling = null;
      jsonPropertyInfoValues81.PropertyName = "AgentAutoApproveExtraUsage";
      jsonPropertyInfoValues81.JsonPropertyName = null;
      jsonPropertyInfoValues81.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("AgentAutoApproveExtraUsage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues82 = jsonPropertyInfoValues81;
      array[40] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues82);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues83 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues83.IsProperty = true;
      jsonPropertyInfoValues83.IsPublic = true;
      jsonPropertyInfoValues83.IsVirtual = false;
      jsonPropertyInfoValues83.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues83.Converter = null;
      jsonPropertyInfoValues83.Getter = (object obj) => ((AppSettings)obj).AgentAlwaysApproveFileUploads;
      jsonPropertyInfoValues83.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).AgentAlwaysApproveFileUploads = value;
      };
      jsonPropertyInfoValues83.IgnoreCondition = null;
      jsonPropertyInfoValues83.HasJsonInclude = false;
      jsonPropertyInfoValues83.IsExtensionData = false;
      jsonPropertyInfoValues83.NumberHandling = null;
      jsonPropertyInfoValues83.PropertyName = "AgentAlwaysApproveFileUploads";
      jsonPropertyInfoValues83.JsonPropertyName = null;
      jsonPropertyInfoValues83.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("AgentAlwaysApproveFileUploads", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues84 = jsonPropertyInfoValues83;
      array[41] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues84);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues85 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues85.IsProperty = true;
      jsonPropertyInfoValues85.IsPublic = true;
      jsonPropertyInfoValues85.IsVirtual = false;
      jsonPropertyInfoValues85.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues85.Converter = null;
      jsonPropertyInfoValues85.Getter = (object obj) => ((AppSettings)obj).AgentCursorTextAnnouncementsEnabled;
      jsonPropertyInfoValues85.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).AgentCursorTextAnnouncementsEnabled = value;
      };
      jsonPropertyInfoValues85.IgnoreCondition = null;
      jsonPropertyInfoValues85.HasJsonInclude = false;
      jsonPropertyInfoValues85.IsExtensionData = false;
      jsonPropertyInfoValues85.NumberHandling = null;
      jsonPropertyInfoValues85.PropertyName = "AgentCursorTextAnnouncementsEnabled";
      jsonPropertyInfoValues85.JsonPropertyName = null;
      jsonPropertyInfoValues85.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("AgentCursorTextAnnouncementsEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues86 = jsonPropertyInfoValues85;
      array[42] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues86);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues87 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues87.IsProperty = true;
      jsonPropertyInfoValues87.IsPublic = true;
      jsonPropertyInfoValues87.IsVirtual = false;
      jsonPropertyInfoValues87.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues87.Converter = null;
      jsonPropertyInfoValues87.Getter = (object obj) => ((AppSettings)obj).OneBrainModel;
      jsonPropertyInfoValues87.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        ((AppSettings)obj).OneBrainModel = value;
      };
      jsonPropertyInfoValues87.IgnoreCondition = null;
      jsonPropertyInfoValues87.HasJsonInclude = false;
      jsonPropertyInfoValues87.IsExtensionData = false;
      jsonPropertyInfoValues87.NumberHandling = null;
      jsonPropertyInfoValues87.PropertyName = "OneBrainModel";
      jsonPropertyInfoValues87.JsonPropertyName = null;
      jsonPropertyInfoValues87.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("OneBrainModel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues88 = jsonPropertyInfoValues87;
      array[43] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues88);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues89 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues89.IsProperty = true;
      jsonPropertyInfoValues89.IsPublic = true;
      jsonPropertyInfoValues89.IsVirtual = false;
      jsonPropertyInfoValues89.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues89.Converter = null;
      jsonPropertyInfoValues89.Getter = (object obj) => ((AppSettings)obj).OneBrainReadsApps;
      jsonPropertyInfoValues89.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).OneBrainReadsApps = value;
      };
      jsonPropertyInfoValues89.IgnoreCondition = null;
      jsonPropertyInfoValues89.HasJsonInclude = false;
      jsonPropertyInfoValues89.IsExtensionData = false;
      jsonPropertyInfoValues89.NumberHandling = null;
      jsonPropertyInfoValues89.PropertyName = "OneBrainReadsApps";
      jsonPropertyInfoValues89.JsonPropertyName = null;
      jsonPropertyInfoValues89.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("OneBrainReadsApps", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues90 = jsonPropertyInfoValues89;
      array[44] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues90);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues91 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues91.IsProperty = true;
      jsonPropertyInfoValues91.IsPublic = true;
      jsonPropertyInfoValues91.IsVirtual = false;
      jsonPropertyInfoValues91.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues91.Converter = null;
      jsonPropertyInfoValues91.Getter = (object obj) => ((AppSettings)obj).OneBrainKeepsScreensInHistory;
      jsonPropertyInfoValues91.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).OneBrainKeepsScreensInHistory = value;
      };
      jsonPropertyInfoValues91.IgnoreCondition = null;
      jsonPropertyInfoValues91.HasJsonInclude = false;
      jsonPropertyInfoValues91.IsExtensionData = false;
      jsonPropertyInfoValues91.NumberHandling = null;
      jsonPropertyInfoValues91.PropertyName = "OneBrainKeepsScreensInHistory";
      jsonPropertyInfoValues91.JsonPropertyName = null;
      jsonPropertyInfoValues91.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("OneBrainKeepsScreensInHistory", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues92 = jsonPropertyInfoValues91;
      array[45] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues92);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues93 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues93.IsProperty = true;
      jsonPropertyInfoValues93.IsPublic = true;
      jsonPropertyInfoValues93.IsVirtual = false;
      jsonPropertyInfoValues93.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues93.Converter = null;
      jsonPropertyInfoValues93.Getter = (object obj) => ((AppSettings)obj).ShowOneBrainStats;
      jsonPropertyInfoValues93.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ShowOneBrainStats = value;
      };
      jsonPropertyInfoValues93.IgnoreCondition = null;
      jsonPropertyInfoValues93.HasJsonInclude = false;
      jsonPropertyInfoValues93.IsExtensionData = false;
      jsonPropertyInfoValues93.NumberHandling = null;
      jsonPropertyInfoValues93.PropertyName = "ShowOneBrainStats";
      jsonPropertyInfoValues93.JsonPropertyName = null;
      jsonPropertyInfoValues93.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ShowOneBrainStats", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues94 = jsonPropertyInfoValues93;
      array[46] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues94);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues95 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues95.IsProperty = true;
      jsonPropertyInfoValues95.IsPublic = true;
      jsonPropertyInfoValues95.IsVirtual = false;
      jsonPropertyInfoValues95.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues95.Converter = null;
      jsonPropertyInfoValues95.Getter = (object obj) => ((AppSettings)obj).MorningPaperEnabled;
      jsonPropertyInfoValues95.Setter = delegate(object obj, bool? value)
      {
        ((AppSettings)obj).MorningPaperEnabled = value;
      };
      jsonPropertyInfoValues95.IgnoreCondition = null;
      jsonPropertyInfoValues95.HasJsonInclude = false;
      jsonPropertyInfoValues95.IsExtensionData = false;
      jsonPropertyInfoValues95.NumberHandling = null;
      jsonPropertyInfoValues95.PropertyName = "MorningPaperEnabled";
      jsonPropertyInfoValues95.JsonPropertyName = null;
      jsonPropertyInfoValues95.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("MorningPaperEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues96 = jsonPropertyInfoValues95;
      array[47] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues96);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues97 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues97.IsProperty = true;
      jsonPropertyInfoValues97.IsPublic = true;
      jsonPropertyInfoValues97.IsVirtual = false;
      jsonPropertyInfoValues97.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues97.Converter = null;
      jsonPropertyInfoValues97.Getter = (object obj) => ((AppSettings)obj).StreamMode;
      jsonPropertyInfoValues97.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).StreamMode = value;
      };
      jsonPropertyInfoValues97.IgnoreCondition = null;
      jsonPropertyInfoValues97.HasJsonInclude = false;
      jsonPropertyInfoValues97.IsExtensionData = false;
      jsonPropertyInfoValues97.NumberHandling = null;
      jsonPropertyInfoValues97.PropertyName = "StreamMode";
      jsonPropertyInfoValues97.JsonPropertyName = null;
      jsonPropertyInfoValues97.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("StreamMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues98 = jsonPropertyInfoValues97;
      array[48] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues98);
      JsonPropertyInfoValues<Dictionary<string, string>> jsonPropertyInfoValues99 = new JsonPropertyInfoValues<Dictionary<string, string>>();
      jsonPropertyInfoValues99.IsProperty = true;
      jsonPropertyInfoValues99.IsPublic = true;
      jsonPropertyInfoValues99.IsVirtual = false;
      jsonPropertyInfoValues99.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues99.Converter = null;
      jsonPropertyInfoValues99.Getter = (object obj) => ((AppSettings)obj).DeveloperChatModels;
      jsonPropertyInfoValues99.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<string, string> value)
      {
        ((AppSettings)obj).DeveloperChatModels = value;
      };
      jsonPropertyInfoValues99.IgnoreCondition = null;
      jsonPropertyInfoValues99.HasJsonInclude = false;
      jsonPropertyInfoValues99.IsExtensionData = false;
      jsonPropertyInfoValues99.NumberHandling = null;
      jsonPropertyInfoValues99.PropertyName = "DeveloperChatModels";
      jsonPropertyInfoValues99.JsonPropertyName = null;
      jsonPropertyInfoValues99.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("DeveloperChatModels", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(Dictionary<string, string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<Dictionary<string, string>> jsonPropertyInfoValues100 = jsonPropertyInfoValues99;
      array[49] = JsonMetadataServices.CreatePropertyInfo<Dictionary<string, string>>(options, jsonPropertyInfoValues100);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues101 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues101.IsProperty = true;
      jsonPropertyInfoValues101.IsPublic = true;
      jsonPropertyInfoValues101.IsVirtual = false;
      jsonPropertyInfoValues101.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues101.Converter = null;
      jsonPropertyInfoValues101.Getter = (object obj) => ((AppSettings)obj).HasShownPaywallIntroVideo;
      jsonPropertyInfoValues101.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).HasShownPaywallIntroVideo = value;
      };
      jsonPropertyInfoValues101.IgnoreCondition = null;
      jsonPropertyInfoValues101.HasJsonInclude = false;
      jsonPropertyInfoValues101.IsExtensionData = false;
      jsonPropertyInfoValues101.NumberHandling = null;
      jsonPropertyInfoValues101.PropertyName = "HasShownPaywallIntroVideo";
      jsonPropertyInfoValues101.JsonPropertyName = null;
      jsonPropertyInfoValues101.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("HasShownPaywallIntroVideo", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues102 = jsonPropertyInfoValues101;
      array[50] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues102);
      JsonPropertyInfoValues<HotkeyBindings> jsonPropertyInfoValues103 = new JsonPropertyInfoValues<HotkeyBindings>();
      jsonPropertyInfoValues103.IsProperty = true;
      jsonPropertyInfoValues103.IsPublic = true;
      jsonPropertyInfoValues103.IsVirtual = false;
      jsonPropertyInfoValues103.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues103.Converter = null;
      jsonPropertyInfoValues103.Getter = null;
      jsonPropertyInfoValues103.Setter = null;
      jsonPropertyInfoValues103.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues103.HasJsonInclude = false;
      jsonPropertyInfoValues103.IsExtensionData = false;
      jsonPropertyInfoValues103.NumberHandling = null;
      jsonPropertyInfoValues103.PropertyName = "EffectiveHotkeys";
      jsonPropertyInfoValues103.JsonPropertyName = null;
      jsonPropertyInfoValues103.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("EffectiveHotkeys", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBindings), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBindings> jsonPropertyInfoValues104 = jsonPropertyInfoValues103;
      array[51] = JsonMetadataServices.CreatePropertyInfo<HotkeyBindings>(options, jsonPropertyInfoValues104);
      array[51].IsGetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues105 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues105.IsProperty = true;
      jsonPropertyInfoValues105.IsPublic = true;
      jsonPropertyInfoValues105.IsVirtual = false;
      jsonPropertyInfoValues105.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues105.Converter = null;
      jsonPropertyInfoValues105.Getter = (object obj) => ((AppSettings)obj).ProactiveAgentSuggestionsEnabled;
      jsonPropertyInfoValues105.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ProactiveAgentSuggestionsEnabled = value;
      };
      jsonPropertyInfoValues105.IgnoreCondition = null;
      jsonPropertyInfoValues105.HasJsonInclude = false;
      jsonPropertyInfoValues105.IsExtensionData = false;
      jsonPropertyInfoValues105.NumberHandling = null;
      jsonPropertyInfoValues105.PropertyName = "ProactiveAgentSuggestionsEnabled";
      jsonPropertyInfoValues105.JsonPropertyName = null;
      jsonPropertyInfoValues105.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ProactiveAgentSuggestionsEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues106 = jsonPropertyInfoValues105;
      array[52] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues106);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues107 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues107.IsProperty = true;
      jsonPropertyInfoValues107.IsPublic = true;
      jsonPropertyInfoValues107.IsVirtual = false;
      jsonPropertyInfoValues107.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues107.Converter = null;
      jsonPropertyInfoValues107.Getter = (object obj) => ((AppSettings)obj).UpcomingMeetingsEnabled;
      jsonPropertyInfoValues107.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).UpcomingMeetingsEnabled = value;
      };
      jsonPropertyInfoValues107.IgnoreCondition = null;
      jsonPropertyInfoValues107.HasJsonInclude = false;
      jsonPropertyInfoValues107.IsExtensionData = false;
      jsonPropertyInfoValues107.NumberHandling = null;
      jsonPropertyInfoValues107.PropertyName = "UpcomingMeetingsEnabled";
      jsonPropertyInfoValues107.JsonPropertyName = null;
      jsonPropertyInfoValues107.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("UpcomingMeetingsEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues108 = jsonPropertyInfoValues107;
      array[53] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues108);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues109 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues109.IsProperty = true;
      jsonPropertyInfoValues109.IsPublic = true;
      jsonPropertyInfoValues109.IsVirtual = false;
      jsonPropertyInfoValues109.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues109.Converter = null;
      jsonPropertyInfoValues109.Getter = (object obj) => ((AppSettings)obj).OnboardingLanguageStepEnabled;
      jsonPropertyInfoValues109.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).OnboardingLanguageStepEnabled = value;
      };
      jsonPropertyInfoValues109.IgnoreCondition = null;
      jsonPropertyInfoValues109.HasJsonInclude = false;
      jsonPropertyInfoValues109.IsExtensionData = false;
      jsonPropertyInfoValues109.NumberHandling = null;
      jsonPropertyInfoValues109.PropertyName = "OnboardingLanguageStepEnabled";
      jsonPropertyInfoValues109.JsonPropertyName = null;
      jsonPropertyInfoValues109.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("OnboardingLanguageStepEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues110 = jsonPropertyInfoValues109;
      array[54] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues110);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues111 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues111.IsProperty = true;
      jsonPropertyInfoValues111.IsPublic = true;
      jsonPropertyInfoValues111.IsVirtual = false;
      jsonPropertyInfoValues111.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues111.Converter = null;
      jsonPropertyInfoValues111.Getter = (object obj) => ((AppSettings)obj).RemoteTasksEnabled;
      jsonPropertyInfoValues111.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).RemoteTasksEnabled = value;
      };
      jsonPropertyInfoValues111.IgnoreCondition = null;
      jsonPropertyInfoValues111.HasJsonInclude = false;
      jsonPropertyInfoValues111.IsExtensionData = false;
      jsonPropertyInfoValues111.NumberHandling = null;
      jsonPropertyInfoValues111.PropertyName = "RemoteTasksEnabled";
      jsonPropertyInfoValues111.JsonPropertyName = null;
      jsonPropertyInfoValues111.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("RemoteTasksEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues112 = jsonPropertyInfoValues111;
      array[55] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues112);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues113 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues113.IsProperty = true;
      jsonPropertyInfoValues113.IsPublic = true;
      jsonPropertyInfoValues113.IsVirtual = false;
      jsonPropertyInfoValues113.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues113.Converter = null;
      jsonPropertyInfoValues113.Getter = (object obj) => ((AppSettings)obj).ProactiveDebugChordEnabled;
      jsonPropertyInfoValues113.Setter = delegate(object obj, bool value)
      {
        ((AppSettings)obj).ProactiveDebugChordEnabled = value;
      };
      jsonPropertyInfoValues113.IgnoreCondition = null;
      jsonPropertyInfoValues113.HasJsonInclude = false;
      jsonPropertyInfoValues113.IsExtensionData = false;
      jsonPropertyInfoValues113.NumberHandling = null;
      jsonPropertyInfoValues113.PropertyName = "ProactiveDebugChordEnabled";
      jsonPropertyInfoValues113.JsonPropertyName = null;
      jsonPropertyInfoValues113.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ProactiveDebugChordEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues114 = jsonPropertyInfoValues113;
      array[56] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues114);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues115 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues115.IsProperty = true;
      jsonPropertyInfoValues115.IsPublic = true;
      jsonPropertyInfoValues115.IsVirtual = false;
      jsonPropertyInfoValues115.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues115.Converter = null;
      jsonPropertyInfoValues115.Getter = (object obj) => ((AppSettings)obj).ProactiveDebugCadence;
      jsonPropertyInfoValues115.Setter = delegate(object obj, int value)
      {
        ((AppSettings)obj).ProactiveDebugCadence = value;
      };
      jsonPropertyInfoValues115.IgnoreCondition = null;
      jsonPropertyInfoValues115.HasJsonInclude = false;
      jsonPropertyInfoValues115.IsExtensionData = false;
      jsonPropertyInfoValues115.NumberHandling = null;
      jsonPropertyInfoValues115.PropertyName = "ProactiveDebugCadence";
      jsonPropertyInfoValues115.JsonPropertyName = null;
      jsonPropertyInfoValues115.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("ProactiveDebugCadence", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues116 = jsonPropertyInfoValues115;
      array[57] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues116);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues117 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues117.IsProperty = true;
      jsonPropertyInfoValues117.IsPublic = true;
      jsonPropertyInfoValues117.IsVirtual = false;
      jsonPropertyInfoValues117.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues117.Converter = null;
      jsonPropertyInfoValues117.Getter = (object obj) => ((AppSettings)obj).LiveNotesEnabled;
      jsonPropertyInfoValues117.Setter = delegate(object obj, bool? value)
      {
        ((AppSettings)obj).LiveNotesEnabled = value;
      };
      jsonPropertyInfoValues117.IgnoreCondition = null;
      jsonPropertyInfoValues117.HasJsonInclude = false;
      jsonPropertyInfoValues117.IsExtensionData = false;
      jsonPropertyInfoValues117.NumberHandling = null;
      jsonPropertyInfoValues117.PropertyName = "LiveNotesEnabled";
      jsonPropertyInfoValues117.JsonPropertyName = null;
      jsonPropertyInfoValues117.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("LiveNotesEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues118 = jsonPropertyInfoValues117;
      array[58] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues118);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues119 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues119.IsProperty = true;
      jsonPropertyInfoValues119.IsPublic = true;
      jsonPropertyInfoValues119.IsVirtual = false;
      jsonPropertyInfoValues119.DeclaringType = typeof(AppSettings);
      jsonPropertyInfoValues119.Converter = null;
      jsonPropertyInfoValues119.Getter = (object obj) => ((AppSettings)obj).LiveNotesStreamPatches;
      jsonPropertyInfoValues119.Setter = delegate(object obj, bool? value)
      {
        ((AppSettings)obj).LiveNotesStreamPatches = value;
      };
      jsonPropertyInfoValues119.IgnoreCondition = null;
      jsonPropertyInfoValues119.HasJsonInclude = false;
      jsonPropertyInfoValues119.IsExtensionData = false;
      jsonPropertyInfoValues119.NumberHandling = null;
      jsonPropertyInfoValues119.PropertyName = "LiveNotesStreamPatches";
      jsonPropertyInfoValues119.JsonPropertyName = null;
      jsonPropertyInfoValues119.AttributeProviderFactory = () => typeof(AppSettings).GetProperty("LiveNotesStreamPatches", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues120 = jsonPropertyInfoValues119;
      array[59] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues120);
      return array;
    }

    // Token: 0x06000090 RID: 144 RVA: 0x00339CB8 File Offset: 0x00339CB8
    private void AppSettingsSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] AppSettings value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string cursorColorHex = value.CursorColorHex;
      if (cursorColorHex != null)
      {
        writer.WriteString(CoreJsonContext.PropName_cursor_color_hex, cursorColorHex);
      }
      string squadMemberId = value.SquadMemberId;
      if (squadMemberId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_squad_member_id, squadMemberId);
      }
      string voiceIdentifier = value.VoiceIdentifier;
      if (voiceIdentifier != null)
      {
        writer.WriteString(CoreJsonContext.PropName_voice_identifier, voiceIdentifier);
      }
      writer.WriteNumber(CoreJsonContext.PropName_speech_speed, value.SpeechSpeed);
      string voiceLanguageCode = value.VoiceLanguageCode;
      if (voiceLanguageCode != null)
      {
        writer.WriteString(CoreJsonContext.PropName_voice_language_code, voiceLanguageCode);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_screen_highlight_enabled, value.ScreenHighlightEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_cursor_docked, value.CursorDocked);
      writer.WriteBoolean(CoreJsonContext.PropName_shake_to_detach_enabled, value.ShakeToDetachEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_launch_at_login, value.LaunchAtLogin);
      writer.WriteBoolean(CoreJsonContext.PropName_show_in_taskbar, value.ShowInTaskbar);
      writer.WriteBoolean(CoreJsonContext.PropName_typed_turns_respond_with_voice, value.TypedTurnsRespondWithVoice);
      string dictationLanguageCode = value.DictationLanguageCode;
      if (dictationLanguageCode != null)
      {
        writer.WriteString(CoreJsonContext.PropName_dictation_language_code, dictationLanguageCode);
      }
      string dictationSpecificLanguageCode = value.DictationSpecificLanguageCode;
      if (dictationSpecificLanguageCode != null)
      {
        writer.WriteString(CoreJsonContext.PropName_dictation_specific_language_code, dictationSpecificLanguageCode);
      }
      double? dictationLastCompletedAtEpochSeconds = value.DictationLastCompletedAtEpochSeconds;
      if (dictationLastCompletedAtEpochSeconds != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_dictation_last_completed_at_epoch_seconds);
        JsonSerializer.Serialize<double?>(writer, dictationLastCompletedAtEpochSeconds, this.NullableDouble);
      }
      List<string> dictationCustomWords = value.DictationCustomWords;
      if (dictationCustomWords != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_dictation_custom_words);
        this.ListStringSerializeHandler(writer, dictationCustomWords);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_dictation_claude_seeded, value.DictationClaudeSeeded);
      string dictationAccountNameSeeded = value.DictationAccountNameSeeded;
      if (dictationAccountNameSeeded != null)
      {
        writer.WriteString(CoreJsonContext.PropName_dictation_account_name_seeded, dictationAccountNameSeeded);
      }
      string discoveryChannelPendingSync = value.DiscoveryChannelPendingSync;
      if (discoveryChannelPendingSync != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel_pending_sync, discoveryChannelPendingSync);
      }
      string microphoneDeviceName = value.MicrophoneDeviceName;
      if (microphoneDeviceName != null)
      {
        writer.WriteString(CoreJsonContext.PropName_microphone_device_name, microphoneDeviceName);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_agent_announcements_voice_enabled, value.AgentAnnouncementsVoiceEnabled);
      writer.WriteNumber(CoreJsonContext.PropName_document_access_notice_show_count, value.DocumentAccessNoticeShowCount);
      writer.WriteBoolean(CoreJsonContext.PropName_document_access_notice_suppressed, value.DocumentAccessNoticeSuppressed);
      OnboardingProgress onboarding = value.Onboarding;
      if (onboarding != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_onboarding);
        this.OnboardingProgressSerializeHandler(writer, onboarding);
      }
      HotkeyBindings hotkeys = value.Hotkeys;
      if (hotkeys != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_hotkeys);
        this.HotkeyBindingsSerializeHandler(writer, hotkeys);
      }
      string microphoneDeviceId = value.MicrophoneDeviceId;
      if (microphoneDeviceId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_microphone_device_id, microphoneDeviceId);
      }
      string outputDeviceId = value.OutputDeviceId;
      if (outputDeviceId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_output_device_id, outputDeviceId);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_drawing_sounds_enabled, value.DrawingSoundsEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_drawing_voice_captions, value.DrawingVoiceCaptions);
      bool? keepDrawingsInChat = value.KeepDrawingsInChat;
      if (keepDrawingsInChat != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_keep_drawings_in_chat);
        JsonSerializer.Serialize<bool?>(writer, keepDrawingsInChat, this.NullableBoolean);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_drawing_pen_sounds_enabled, value.DrawingPenSoundsEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_drawing_java_script_jit, value.DrawingJavaScriptJit);
      bool? drawingSubtitles = value.DrawingSubtitles;
      if (drawingSubtitles != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_drawing_subtitles);
        JsonSerializer.Serialize<bool?>(writer, drawingSubtitles, this.NullableBoolean);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_save_drawings, value.SaveDrawings);
      writer.WriteBoolean(CoreJsonContext.PropName_show_agent_labels_in_activity_strip, value.ShowAgentLabelsInActivityStrip);
      writer.WriteBoolean(CoreJsonContext.PropName_show_in_screen_recordings, value.ShowInScreenRecordings);
      string agentFolderPath = value.AgentFolderPath;
      if (agentFolderPath != null)
      {
        writer.WriteString(CoreJsonContext.PropName_agent_folder_path, agentFolderPath);
      }
      string obsidianVaultPath = value.ObsidianVaultPath;
      if (obsidianVaultPath != null)
      {
        writer.WriteString(CoreJsonContext.PropName_obsidian_vault_path, obsidianVaultPath);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_agent_auto_approve_extra_usage, value.AgentAutoApproveExtraUsage);
      writer.WriteBoolean(CoreJsonContext.PropName_agent_always_approve_file_uploads, value.AgentAlwaysApproveFileUploads);
      writer.WriteBoolean(CoreJsonContext.PropName_agent_cursor_text_announcements_enabled, value.AgentCursorTextAnnouncementsEnabled);
      string oneBrainModel = value.OneBrainModel;
      if (oneBrainModel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_one_brain_model, oneBrainModel);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_one_brain_reads_apps, value.OneBrainReadsApps);
      writer.WriteBoolean(CoreJsonContext.PropName_one_brain_keeps_screens_in_history, value.OneBrainKeepsScreensInHistory);
      writer.WriteBoolean(CoreJsonContext.PropName_show_one_brain_stats, value.ShowOneBrainStats);
      bool? morningPaperEnabled = value.MorningPaperEnabled;
      if (morningPaperEnabled != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_morning_paper_enabled);
        JsonSerializer.Serialize<bool?>(writer, morningPaperEnabled, this.NullableBoolean);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_stream_mode, value.StreamMode);
      Dictionary<string, string> developerChatModels = value.DeveloperChatModels;
      if (developerChatModels != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_developer_chat_models);
        this.DictionaryStringStringSerializeHandler(writer, developerChatModels);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_has_shown_paywall_intro_video, value.HasShownPaywallIntroVideo);
      writer.WriteBoolean(CoreJsonContext.PropName_proactive_agent_suggestions_enabled, value.ProactiveAgentSuggestionsEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_upcoming_meetings_enabled, value.UpcomingMeetingsEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_onboarding_language_step_enabled, value.OnboardingLanguageStepEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_remote_tasks_enabled, value.RemoteTasksEnabled);
      writer.WriteBoolean(CoreJsonContext.PropName_proactive_debug_chord_enabled, value.ProactiveDebugChordEnabled);
      writer.WriteNumber(CoreJsonContext.PropName_proactive_debug_cadence, value.ProactiveDebugCadence);
      bool? liveNotesEnabled = value.LiveNotesEnabled;
      if (liveNotesEnabled != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_live_notes_enabled);
        JsonSerializer.Serialize<bool?>(writer, liveNotesEnabled, this.NullableBoolean);
      }
      bool? liveNotesStreamPatches = value.LiveNotesStreamPatches;
      if (liveNotesStreamPatches != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_live_notes_stream_patches);
        JsonSerializer.Serialize<bool?>(writer, liveNotesStreamPatches, this.NullableBoolean);
      }
      writer.WriteEndObject();
    }

    // Token: 0x1700000B RID: 11
    // (get) Token: 0x06000091 RID: 145 RVA: 0x0033A1CC File Offset: 0x0033A1CC
    [Nullable(0)]
    public JsonTypeInfo<AppConfig> AppConfig
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<AppConfig> jsonTypeInfo;
        if ((jsonTypeInfo = this._AppConfig) == null)
        {
          jsonTypeInfo = (this._AppConfig = (JsonTypeInfo<AppConfig>)base.Options.GetTypeInfo(typeof(AppConfig)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000092 RID: 146 RVA: 0x0033A208 File Offset: 0x0033A208
    private JsonTypeInfo<AppConfig> Create_AppConfig(JsonSerializerOptions options)
    {
      JsonTypeInfo<AppConfig> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<AppConfig>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<AppConfig> jsonObjectInfoValues = new JsonObjectInfoValues<AppConfig>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new AppConfig((AppConfigLinks)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.AppConfigPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<2>__AppConfigCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<2>__AppConfigCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.AppConfigCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(AppConfig).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(AppConfigLinks) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, AppConfig>(this.AppConfigSerializeHandler);
        JsonObjectInfoValues<AppConfig> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<AppConfig>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000093 RID: 147 RVA: 0x0033A2F4 File Offset: 0x0033A2F4
    private static JsonPropertyInfo[] AppConfigPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<AppConfigLinks> jsonPropertyInfoValues = new JsonPropertyInfoValues<AppConfigLinks>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(AppConfig);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((AppConfig)obj).Links;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] AppConfigLinks value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Links";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AppConfig).GetProperty("Links", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(AppConfigLinks), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<AppConfigLinks> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<AppConfigLinks>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000094 RID: 148 RVA: 0x0033A404 File Offset: 0x0033A404
    private void AppConfigSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] AppConfig value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      AppConfigLinks links = value.Links;
      if (links != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_links);
        this.AppConfigLinksSerializeHandler(writer, links);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000095 RID: 149 RVA: 0x0033A444 File Offset: 0x0033A444
    private static JsonParameterInfoValues[] AppConfigCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Links",
          ParameterType = typeof(AppConfigLinks),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700000C RID: 12
    // (get) Token: 0x06000096 RID: 150 RVA: 0x0033A498 File Offset: 0x0033A498
    [Nullable(0)]
    public JsonTypeInfo<AppConfigLinks> AppConfigLinks
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<AppConfigLinks> jsonTypeInfo;
        if ((jsonTypeInfo = this._AppConfigLinks) == null)
        {
          jsonTypeInfo = (this._AppConfigLinks = (JsonTypeInfo<AppConfigLinks>)base.Options.GetTypeInfo(typeof(AppConfigLinks)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000097 RID: 151 RVA: 0x0033A4D4 File Offset: 0x0033A4D4
    private JsonTypeInfo<AppConfigLinks> Create_AppConfigLinks(JsonSerializerOptions options)
    {
      JsonTypeInfo<AppConfigLinks> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<AppConfigLinks>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<AppConfigLinks> jsonObjectInfoValues = new JsonObjectInfoValues<AppConfigLinks>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new AppConfigLinks((string)args[0], (string)args[1], (string)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.AppConfigLinksPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<3>__AppConfigLinksCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<3>__AppConfigLinksCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.AppConfigLinksCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(AppConfigLinks).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, AppConfigLinks>(this.AppConfigLinksSerializeHandler);
        JsonObjectInfoValues<AppConfigLinks> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<AppConfigLinks>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000098 RID: 152 RVA: 0x0033A5C0 File Offset: 0x0033A5C0
    private static JsonPropertyInfo[] AppConfigLinksPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(AppConfigLinks);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((AppConfigLinks)obj).FeatureRequest;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "FeatureRequest";
      jsonPropertyInfoValues.JsonPropertyName = "featureRequest";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AppConfigLinks).GetProperty("FeatureRequest", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(AppConfigLinks);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((AppConfigLinks)obj).CommunityReddit;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "CommunityReddit";
      jsonPropertyInfoValues3.JsonPropertyName = "communityReddit";
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(AppConfigLinks).GetProperty("CommunityReddit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(AppConfigLinks);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((AppConfigLinks)obj).CommunityWhatsApp;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "CommunityWhatsApp";
      jsonPropertyInfoValues5.JsonPropertyName = "communityWhatsApp";
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(AppConfigLinks).GetProperty("CommunityWhatsApp", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(AppConfigLinks);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((AppConfigLinks)obj).Changelog;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Changelog";
      jsonPropertyInfoValues7.JsonPropertyName = "changelog";
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(AppConfigLinks).GetProperty("Changelog", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      return array;
    }

    // Token: 0x06000099 RID: 153 RVA: 0x0033A990 File Offset: 0x0033A990
    private void AppConfigLinksSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] AppConfigLinks value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string featureRequest = value.FeatureRequest;
      if (featureRequest != null)
      {
        writer.WriteString(CoreJsonContext.PropName_featureRequest, featureRequest);
      }
      string communityReddit = value.CommunityReddit;
      if (communityReddit != null)
      {
        writer.WriteString(CoreJsonContext.PropName_communityReddit, communityReddit);
      }
      string communityWhatsApp = value.CommunityWhatsApp;
      if (communityWhatsApp != null)
      {
        writer.WriteString(CoreJsonContext.PropName_communityWhatsApp, communityWhatsApp);
      }
      string changelog = value.Changelog;
      if (changelog != null)
      {
        writer.WriteString(CoreJsonContext.PropName_changelog, changelog);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600009A RID: 154 RVA: 0x0033AA0C File Offset: 0x0033AA0C
    private static JsonParameterInfoValues[] AppConfigLinksCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "FeatureRequest",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "CommunityReddit",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "CommunityWhatsApp",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Changelog",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700000D RID: 13
    // (get) Token: 0x0600009B RID: 155 RVA: 0x0033AB1C File Offset: 0x0033AB1C
    [Nullable(0)]
    public JsonTypeInfo<AppConfigResponse> AppConfigResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<AppConfigResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._AppConfigResponse) == null)
        {
          jsonTypeInfo = (this._AppConfigResponse = (JsonTypeInfo<AppConfigResponse>)base.Options.GetTypeInfo(typeof(AppConfigResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600009C RID: 156 RVA: 0x0033AB58 File Offset: 0x0033AB58
    private JsonTypeInfo<AppConfigResponse> Create_AppConfigResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<AppConfigResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<AppConfigResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<AppConfigResponse> jsonObjectInfoValues = new JsonObjectInfoValues<AppConfigResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new AppConfigResponse((AppConfig)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.AppConfigResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<4>__AppConfigResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<4>__AppConfigResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.AppConfigResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(AppConfigResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(AppConfig) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, AppConfigResponse>(this.AppConfigResponseSerializeHandler);
        JsonObjectInfoValues<AppConfigResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<AppConfigResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600009D RID: 157 RVA: 0x0033AC44 File Offset: 0x0033AC44
    private static JsonPropertyInfo[] AppConfigResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<AppConfig> jsonPropertyInfoValues = new JsonPropertyInfoValues<AppConfig>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(AppConfigResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((AppConfigResponse)obj).AppConfig;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] AppConfig value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "AppConfig";
      jsonPropertyInfoValues.JsonPropertyName = "appConfig";
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(AppConfigResponse).GetProperty("AppConfig", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(AppConfig), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<AppConfig> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<AppConfig>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      return array;
    }

    // Token: 0x0600009E RID: 158 RVA: 0x0033AD58 File Offset: 0x0033AD58
    private void AppConfigResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] AppConfigResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      AppConfig appConfig = value.AppConfig;
      if (appConfig != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_appConfig);
        this.AppConfigSerializeHandler(writer, appConfig);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600009F RID: 159 RVA: 0x0033AD98 File Offset: 0x0033AD98
    private static JsonParameterInfoValues[] AppConfigResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "AppConfig",
          ParameterType = typeof(AppConfig),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700000E RID: 14
    // (get) Token: 0x060000A0 RID: 160 RVA: 0x0033ADEC File Offset: 0x0033ADEC
    [Nullable(0)]
    public JsonTypeInfo<CheckoutRequest> CheckoutRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<CheckoutRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._CheckoutRequest) == null)
        {
          jsonTypeInfo = (this._CheckoutRequest = (JsonTypeInfo<CheckoutRequest>)base.Options.GetTypeInfo(typeof(CheckoutRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000A1 RID: 161 RVA: 0x0033AE28 File Offset: 0x0033AE28
    private JsonTypeInfo<CheckoutRequest> Create_CheckoutRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<CheckoutRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<CheckoutRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<CheckoutRequest> jsonObjectInfoValues = new JsonObjectInfoValues<CheckoutRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new CheckoutRequest((string)args[0], (string)args[1], (string)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.CheckoutRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<5>__CheckoutRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<5>__CheckoutRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.CheckoutRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(CheckoutRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, CheckoutRequest>(this.CheckoutRequestSerializeHandler);
        JsonObjectInfoValues<CheckoutRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<CheckoutRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000A2 RID: 162 RVA: 0x0033AF14 File Offset: 0x0033AF14
    private static JsonPropertyInfo[] CheckoutRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(CheckoutRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((CheckoutRequest)obj).Plan;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Plan";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(CheckoutRequest).GetProperty("Plan", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(CheckoutRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((CheckoutRequest)obj).Interval;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Interval";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(CheckoutRequest).GetProperty("Interval", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(CheckoutRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((CheckoutRequest)obj).CheckoutUi;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "CheckoutUi";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(CheckoutRequest).GetProperty("CheckoutUi", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(CheckoutRequest);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((CheckoutRequest)obj).DiscountCode;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "DiscountCode";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(CheckoutRequest).GetProperty("DiscountCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      return array;
    }

    // Token: 0x060000A3 RID: 163 RVA: 0x0033B30C File Offset: 0x0033B30C
    private void CheckoutRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] CheckoutRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string plan = value.Plan;
      if (plan != null)
      {
        writer.WriteString(CoreJsonContext.PropName_plan, plan);
      }
      string interval = value.Interval;
      if (interval != null)
      {
        writer.WriteString(CoreJsonContext.PropName_interval, interval);
      }
      string checkoutUi = value.CheckoutUi;
      if (checkoutUi != null)
      {
        writer.WriteString(CoreJsonContext.PropName_checkout_ui, checkoutUi);
      }
      string discountCode = value.DiscountCode;
      if (discountCode != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discount_code, discountCode);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000A4 RID: 164 RVA: 0x0033B388 File Offset: 0x0033B388
    private static JsonParameterInfoValues[] CheckoutRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Plan",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Interval",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CheckoutUi",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = true,
          DefaultValue = "web",
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DiscountCode",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700000F RID: 15
    // (get) Token: 0x060000A5 RID: 165 RVA: 0x0033B49C File Offset: 0x0033B49C
    [Nullable(0)]
    public JsonTypeInfo<CheckoutResponse> CheckoutResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<CheckoutResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._CheckoutResponse) == null)
        {
          jsonTypeInfo = (this._CheckoutResponse = (JsonTypeInfo<CheckoutResponse>)base.Options.GetTypeInfo(typeof(CheckoutResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000A6 RID: 166 RVA: 0x0033B4D8 File Offset: 0x0033B4D8
    private JsonTypeInfo<CheckoutResponse> Create_CheckoutResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<CheckoutResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<CheckoutResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<CheckoutResponse> jsonObjectInfoValues = new JsonObjectInfoValues<CheckoutResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new CheckoutResponse((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.CheckoutResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<6>__CheckoutResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<6>__CheckoutResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.CheckoutResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(CheckoutResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, CheckoutResponse>(this.CheckoutResponseSerializeHandler);
        JsonObjectInfoValues<CheckoutResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<CheckoutResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000A7 RID: 167 RVA: 0x0033B5C4 File Offset: 0x0033B5C4
    private static JsonPropertyInfo[] CheckoutResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(CheckoutResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((CheckoutResponse)obj).Url;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Url";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(CheckoutResponse).GetProperty("Url", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(CheckoutResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((CheckoutResponse)obj).DiscountCodeApplied;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "DiscountCodeApplied";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(CheckoutResponse).GetProperty("DiscountCodeApplied", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x060000A8 RID: 168 RVA: 0x0033B7BC File Offset: 0x0033B7BC
    private void CheckoutResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] CheckoutResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string url = value.Url;
      if (url != null)
      {
        writer.WriteString(CoreJsonContext.PropName_url, url);
      }
      string discountCodeApplied = value.DiscountCodeApplied;
      if (discountCodeApplied != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discount_code_applied, discountCodeApplied);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000A9 RID: 169 RVA: 0x0033B80C File Offset: 0x0033B80C
    private static JsonParameterInfoValues[] CheckoutResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Url",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DiscountCodeApplied",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000010 RID: 16
    // (get) Token: 0x060000AA RID: 170 RVA: 0x0033B8A0 File Offset: 0x0033B8A0
    [Nullable(0)]
    public JsonTypeInfo<DeepgramTokenResponse> DeepgramTokenResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DeepgramTokenResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._DeepgramTokenResponse) == null)
        {
          jsonTypeInfo = (this._DeepgramTokenResponse = (JsonTypeInfo<DeepgramTokenResponse>)base.Options.GetTypeInfo(typeof(DeepgramTokenResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000AB RID: 171 RVA: 0x0033B8DC File Offset: 0x0033B8DC
    private JsonTypeInfo<DeepgramTokenResponse> Create_DeepgramTokenResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<DeepgramTokenResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DeepgramTokenResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DeepgramTokenResponse> jsonObjectInfoValues = new JsonObjectInfoValues<DeepgramTokenResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DeepgramTokenResponse((string)args[0], (string)args[1], (string)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DeepgramTokenResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<7>__DeepgramTokenResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<7>__DeepgramTokenResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DeepgramTokenResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DeepgramTokenResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DeepgramTokenResponse>(this.DeepgramTokenResponseSerializeHandler);
        JsonObjectInfoValues<DeepgramTokenResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DeepgramTokenResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000AC RID: 172 RVA: 0x0033B9C8 File Offset: 0x0033B9C8
    private static JsonPropertyInfo[] DeepgramTokenResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DeepgramTokenResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DeepgramTokenResponse)obj).AccessToken;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "AccessToken";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DeepgramTokenResponse).GetProperty("AccessToken", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(DeepgramTokenResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((DeepgramTokenResponse)obj).Model;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Model";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DeepgramTokenResponse).GetProperty("Model", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(DeepgramTokenResponse);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((DeepgramTokenResponse)obj).Receipt;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Receipt";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DeepgramTokenResponse).GetProperty("Receipt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x060000AD RID: 173 RVA: 0x0033BCAC File Offset: 0x0033BCAC
    private void DeepgramTokenResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DeepgramTokenResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string accessToken = value.AccessToken;
      if (accessToken != null)
      {
        writer.WriteString(CoreJsonContext.PropName_access_token, accessToken);
      }
      string model = value.Model;
      if (model != null)
      {
        writer.WriteString(CoreJsonContext.PropName_model, model);
      }
      string receipt = value.Receipt;
      if (receipt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_receipt, receipt);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000AE RID: 174 RVA: 0x0033BD14 File Offset: 0x0033BD14
    private static JsonParameterInfoValues[] DeepgramTokenResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "AccessToken",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Model",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Receipt",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000011 RID: 17
    // (get) Token: 0x060000AF RID: 175 RVA: 0x0033BDE4 File Offset: 0x0033BDE4
    [Nullable(0)]
    public JsonTypeInfo<DeviceRegistrationRequest> DeviceRegistrationRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DeviceRegistrationRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._DeviceRegistrationRequest) == null)
        {
          jsonTypeInfo = (this._DeviceRegistrationRequest = (JsonTypeInfo<DeviceRegistrationRequest>)base.Options.GetTypeInfo(typeof(DeviceRegistrationRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000B0 RID: 176 RVA: 0x0033BE20 File Offset: 0x0033BE20
    private JsonTypeInfo<DeviceRegistrationRequest> Create_DeviceRegistrationRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<DeviceRegistrationRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DeviceRegistrationRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DeviceRegistrationRequest> jsonObjectInfoValues = new JsonObjectInfoValues<DeviceRegistrationRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DeviceRegistrationRequest((IReadOnlyList<string>)args[0], (string)args[1], (string)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DeviceRegistrationRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<8>__DeviceRegistrationRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<8>__DeviceRegistrationRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DeviceRegistrationRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DeviceRegistrationRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(IReadOnlyList<string>),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DeviceRegistrationRequest>(this.DeviceRegistrationRequestSerializeHandler);
        JsonObjectInfoValues<DeviceRegistrationRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DeviceRegistrationRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000B1 RID: 177 RVA: 0x0033BF0C File Offset: 0x0033BF0C
    private static JsonPropertyInfo[] DeviceRegistrationRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DeviceRegistrationRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DeviceRegistrationRequest)obj).Capabilities;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Capabilities";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DeviceRegistrationRequest).GetProperty("Capabilities", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(DeviceRegistrationRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((DeviceRegistrationRequest)obj).DisplayName;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "DisplayName";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DeviceRegistrationRequest).GetProperty("DisplayName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(DeviceRegistrationRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((DeviceRegistrationRequest)obj).Model;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Model";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(DeviceRegistrationRequest).GetProperty("Model", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x060000B2 RID: 178 RVA: 0x0033C1E0 File Offset: 0x0033C1E0
    private void DeviceRegistrationRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DeviceRegistrationRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<string> capabilities = value.Capabilities;
      if (capabilities != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_capabilities);
        this.IReadOnlyListStringSerializeHandler(writer, capabilities);
      }
      string displayName = value.DisplayName;
      if (displayName != null)
      {
        writer.WriteString(CoreJsonContext.PropName_display_name, displayName);
      }
      string model = value.Model;
      if (model != null)
      {
        writer.WriteString(CoreJsonContext.PropName_model, model);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000B3 RID: 179 RVA: 0x0033C24C File Offset: 0x0033C24C
    private static JsonParameterInfoValues[] DeviceRegistrationRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Capabilities",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "DisplayName",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Model",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000012 RID: 18
    // (get) Token: 0x060000B4 RID: 180 RVA: 0x0033C31C File Offset: 0x0033C31C
    [Nullable(0)]
    public JsonTypeInfo<DeviceRegistrationStamp> DeviceRegistrationStamp
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DeviceRegistrationStamp> jsonTypeInfo;
        if ((jsonTypeInfo = this._DeviceRegistrationStamp) == null)
        {
          jsonTypeInfo = (this._DeviceRegistrationStamp = (JsonTypeInfo<DeviceRegistrationStamp>)base.Options.GetTypeInfo(typeof(DeviceRegistrationStamp)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000B5 RID: 181 RVA: 0x0033C358 File Offset: 0x0033C358
    private JsonTypeInfo<DeviceRegistrationStamp> Create_DeviceRegistrationStamp(JsonSerializerOptions options)
    {
      JsonTypeInfo<DeviceRegistrationStamp> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DeviceRegistrationStamp>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DeviceRegistrationStamp> jsonObjectInfoValues = new JsonObjectInfoValues<DeviceRegistrationStamp>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DeviceRegistrationStamp((string)args[0], (long)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DeviceRegistrationStampPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<9>__DeviceRegistrationStampCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<9>__DeviceRegistrationStampCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DeviceRegistrationStampCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DeviceRegistrationStamp).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(long)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DeviceRegistrationStamp>(this.DeviceRegistrationStampSerializeHandler);
        JsonObjectInfoValues<DeviceRegistrationStamp> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DeviceRegistrationStamp>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000B6 RID: 182 RVA: 0x0033C444 File Offset: 0x0033C444
    private static JsonPropertyInfo[] DeviceRegistrationStampPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DeviceRegistrationStamp);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DeviceRegistrationStamp)obj).Fingerprint;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Fingerprint";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DeviceRegistrationStamp).GetProperty("Fingerprint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<long> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<long>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(DeviceRegistrationStamp);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((DeviceRegistrationStamp)obj).RegisteredAtEpochSeconds;
      jsonPropertyInfoValues3.Setter = delegate(object obj, long value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "RegisteredAtEpochSeconds";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DeviceRegistrationStamp).GetProperty("RegisteredAtEpochSeconds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(long), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<long> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<long>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x060000B7 RID: 183 RVA: 0x0033C63C File Offset: 0x0033C63C
    private void DeviceRegistrationStampSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DeviceRegistrationStamp value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string fingerprint = value.Fingerprint;
      if (fingerprint != null)
      {
        writer.WriteString(CoreJsonContext.PropName_fingerprint, fingerprint);
      }
      writer.WriteNumber(CoreJsonContext.PropName_registered_at_epoch_seconds, value.RegisteredAtEpochSeconds);
      writer.WriteEndObject();
    }

    // Token: 0x060000B8 RID: 184 RVA: 0x0033C688 File Offset: 0x0033C688
    private static JsonParameterInfoValues[] DeviceRegistrationStampCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Fingerprint",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "RegisteredAtEpochSeconds",
          ParameterType = typeof(long),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000013 RID: 19
    // (get) Token: 0x060000B9 RID: 185 RVA: 0x0033C71C File Offset: 0x0033C71C
    [Nullable(0)]
    public JsonTypeInfo<DeviceResponse> DeviceResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DeviceResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._DeviceResponse) == null)
        {
          jsonTypeInfo = (this._DeviceResponse = (JsonTypeInfo<DeviceResponse>)base.Options.GetTypeInfo(typeof(DeviceResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000BA RID: 186 RVA: 0x0033C758 File Offset: 0x0033C758
    private JsonTypeInfo<DeviceResponse> Create_DeviceResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<DeviceResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DeviceResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DeviceResponse> jsonObjectInfoValues = new JsonObjectInfoValues<DeviceResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DeviceResponse((RegisteredDevice)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DeviceResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<10>__DeviceResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<10>__DeviceResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DeviceResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DeviceResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(RegisteredDevice) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DeviceResponse>(this.DeviceResponseSerializeHandler);
        JsonObjectInfoValues<DeviceResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DeviceResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000BB RID: 187 RVA: 0x0033C844 File Offset: 0x0033C844
    private static JsonPropertyInfo[] DeviceResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<RegisteredDevice> jsonPropertyInfoValues = new JsonPropertyInfoValues<RegisteredDevice>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DeviceResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DeviceResponse)obj).Device;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] RegisteredDevice value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Device";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DeviceResponse).GetProperty("Device", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(RegisteredDevice), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<RegisteredDevice> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<RegisteredDevice>(options, jsonPropertyInfoValues2);
      return array;
    }

    // Token: 0x060000BC RID: 188 RVA: 0x0033C940 File Offset: 0x0033C940
    private void DeviceResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DeviceResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      RegisteredDevice device = value.Device;
      if (device != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_device);
        this.RegisteredDeviceSerializeHandler(writer, device);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000BD RID: 189 RVA: 0x0033C980 File Offset: 0x0033C980
    private static JsonParameterInfoValues[] DeviceResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Device",
          ParameterType = typeof(RegisteredDevice),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000014 RID: 20
    // (get) Token: 0x060000BE RID: 190 RVA: 0x0033C9D4 File Offset: 0x0033C9D4
    [Nullable(0)]
    public JsonTypeInfo<DevicesResponse> DevicesResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DevicesResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._DevicesResponse) == null)
        {
          jsonTypeInfo = (this._DevicesResponse = (JsonTypeInfo<DevicesResponse>)base.Options.GetTypeInfo(typeof(DevicesResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000BF RID: 191 RVA: 0x0033CA10 File Offset: 0x0033CA10
    private JsonTypeInfo<DevicesResponse> Create_DevicesResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<DevicesResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DevicesResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DevicesResponse> jsonObjectInfoValues = new JsonObjectInfoValues<DevicesResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DevicesResponse((IReadOnlyList<RegisteredDevice>)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DevicesResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<11>__DevicesResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<11>__DevicesResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DevicesResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DevicesResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(IReadOnlyList<RegisteredDevice>) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DevicesResponse>(this.DevicesResponseSerializeHandler);
        JsonObjectInfoValues<DevicesResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DevicesResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000C0 RID: 192 RVA: 0x0033CAFC File Offset: 0x0033CAFC
    private static JsonPropertyInfo[] DevicesResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<IReadOnlyList<RegisteredDevice>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<RegisteredDevice>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DevicesResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DevicesResponse)obj).Devices;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<RegisteredDevice> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Devices";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DevicesResponse).GetProperty("Devices", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<RegisteredDevice>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<RegisteredDevice>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<RegisteredDevice>>(options, jsonPropertyInfoValues2);
      return array;
    }

    // Token: 0x060000C1 RID: 193 RVA: 0x0033CBF8 File Offset: 0x0033CBF8
    private void DevicesResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DevicesResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<RegisteredDevice> devices = value.Devices;
      if (devices != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_devices);
        this.IReadOnlyListRegisteredDeviceSerializeHandler(writer, devices);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000C2 RID: 194 RVA: 0x0033CC38 File Offset: 0x0033CC38
    private static JsonParameterInfoValues[] DevicesResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Devices",
          ParameterType = typeof(IReadOnlyList<RegisteredDevice>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000015 RID: 21
    // (get) Token: 0x060000C3 RID: 195 RVA: 0x0033CC8C File Offset: 0x0033CC8C
    [Nullable(0)]
    public JsonTypeInfo<DiscoveryChannelRequest> DiscoveryChannelRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DiscoveryChannelRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._DiscoveryChannelRequest) == null)
        {
          jsonTypeInfo = (this._DiscoveryChannelRequest = (JsonTypeInfo<DiscoveryChannelRequest>)base.Options.GetTypeInfo(typeof(DiscoveryChannelRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000C4 RID: 196 RVA: 0x0033CCC8 File Offset: 0x0033CCC8
    private JsonTypeInfo<DiscoveryChannelRequest> Create_DiscoveryChannelRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<DiscoveryChannelRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DiscoveryChannelRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DiscoveryChannelRequest> jsonObjectInfoValues = new JsonObjectInfoValues<DiscoveryChannelRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DiscoveryChannelRequest((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DiscoveryChannelRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<12>__DiscoveryChannelRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<12>__DiscoveryChannelRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DiscoveryChannelRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DiscoveryChannelRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DiscoveryChannelRequest>(this.DiscoveryChannelRequestSerializeHandler);
        JsonObjectInfoValues<DiscoveryChannelRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DiscoveryChannelRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000C5 RID: 197 RVA: 0x0033CDB4 File Offset: 0x0033CDB4
    private static JsonPropertyInfo[] DiscoveryChannelRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DiscoveryChannelRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DiscoveryChannelRequest)obj).DiscoveryChannel;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "DiscoveryChannel";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DiscoveryChannelRequest).GetProperty("DiscoveryChannel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(DiscoveryChannelRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((DiscoveryChannelRequest)obj).DiscoveryChannelOther;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "DiscoveryChannelOther";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DiscoveryChannelRequest).GetProperty("DiscoveryChannelOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x060000C6 RID: 198 RVA: 0x0033CFAC File Offset: 0x0033CFAC
    private void DiscoveryChannelRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DiscoveryChannelRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string discoveryChannel = value.DiscoveryChannel;
      if (discoveryChannel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel, discoveryChannel);
      }
      string discoveryChannelOther = value.DiscoveryChannelOther;
      if (discoveryChannelOther != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel_other, discoveryChannelOther);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000C7 RID: 199 RVA: 0x0033CFFC File Offset: 0x0033CFFC
    private static JsonParameterInfoValues[] DiscoveryChannelRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "DiscoveryChannel",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DiscoveryChannelOther",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000016 RID: 22
    // (get) Token: 0x060000C8 RID: 200 RVA: 0x0033D090 File Offset: 0x0033D090
    [Nullable(0)]
    public JsonTypeInfo<DiscoveryChannelResponse> DiscoveryChannelResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<DiscoveryChannelResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._DiscoveryChannelResponse) == null)
        {
          jsonTypeInfo = (this._DiscoveryChannelResponse = (JsonTypeInfo<DiscoveryChannelResponse>)base.Options.GetTypeInfo(typeof(DiscoveryChannelResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000C9 RID: 201 RVA: 0x0033D0CC File Offset: 0x0033D0CC
    private JsonTypeInfo<DiscoveryChannelResponse> Create_DiscoveryChannelResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<DiscoveryChannelResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<DiscoveryChannelResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<DiscoveryChannelResponse> jsonObjectInfoValues = new JsonObjectInfoValues<DiscoveryChannelResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new DiscoveryChannelResponse((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.DiscoveryChannelResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<13>__DiscoveryChannelResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<13>__DiscoveryChannelResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.DiscoveryChannelResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(DiscoveryChannelResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, DiscoveryChannelResponse>(this.DiscoveryChannelResponseSerializeHandler);
        JsonObjectInfoValues<DiscoveryChannelResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<DiscoveryChannelResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000CA RID: 202 RVA: 0x0033D1B8 File Offset: 0x0033D1B8
    private static JsonPropertyInfo[] DiscoveryChannelResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(DiscoveryChannelResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((DiscoveryChannelResponse)obj).DiscoveryChannel;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "DiscoveryChannel";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(DiscoveryChannelResponse).GetProperty("DiscoveryChannel", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(DiscoveryChannelResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((DiscoveryChannelResponse)obj).DiscoveryChannelOther;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "DiscoveryChannelOther";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(DiscoveryChannelResponse).GetProperty("DiscoveryChannelOther", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x060000CB RID: 203 RVA: 0x0033D3A0 File Offset: 0x0033D3A0
    private void DiscoveryChannelResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] DiscoveryChannelResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string discoveryChannel = value.DiscoveryChannel;
      if (discoveryChannel != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel, discoveryChannel);
      }
      string discoveryChannelOther = value.DiscoveryChannelOther;
      if (discoveryChannelOther != null)
      {
        writer.WriteString(CoreJsonContext.PropName_discovery_channel_other, discoveryChannelOther);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000CC RID: 204 RVA: 0x0033D3F0 File Offset: 0x0033D3F0
    private static JsonParameterInfoValues[] DiscoveryChannelResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "DiscoveryChannel",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "DiscoveryChannelOther",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000017 RID: 23
    // (get) Token: 0x060000CD RID: 205 RVA: 0x0033D484 File Offset: 0x0033D484
    [Nullable(0)]
    public JsonTypeInfo<EarsTranscribeRequest> EarsTranscribeRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<EarsTranscribeRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._EarsTranscribeRequest) == null)
        {
          jsonTypeInfo = (this._EarsTranscribeRequest = (JsonTypeInfo<EarsTranscribeRequest>)base.Options.GetTypeInfo(typeof(EarsTranscribeRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000CE RID: 206 RVA: 0x0033D4C0 File Offset: 0x0033D4C0
    private JsonTypeInfo<EarsTranscribeRequest> Create_EarsTranscribeRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<EarsTranscribeRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<EarsTranscribeRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<EarsTranscribeRequest> jsonObjectInfoValues = new JsonObjectInfoValues<EarsTranscribeRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new EarsTranscribeRequest((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.EarsTranscribeRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<14>__EarsTranscribeRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<14>__EarsTranscribeRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.EarsTranscribeRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(EarsTranscribeRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, EarsTranscribeRequest>(this.EarsTranscribeRequestSerializeHandler);
        JsonObjectInfoValues<EarsTranscribeRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<EarsTranscribeRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000CF RID: 207 RVA: 0x0033D5AC File Offset: 0x0033D5AC
    private static JsonPropertyInfo[] EarsTranscribeRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(EarsTranscribeRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((EarsTranscribeRequest)obj).AudioWavBase64;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "AudioWavBase64";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(EarsTranscribeRequest).GetProperty("AudioWavBase64", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(EarsTranscribeRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((EarsTranscribeRequest)obj).Language;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Language";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(EarsTranscribeRequest).GetProperty("Language", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      return array;
    }

    // Token: 0x060000D0 RID: 208 RVA: 0x0033D7B8 File Offset: 0x0033D7B8
    private void EarsTranscribeRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] EarsTranscribeRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string audioWavBase = value.AudioWavBase64;
      if (audioWavBase != null)
      {
        writer.WriteString(CoreJsonContext.PropName_audio_wav_base64, audioWavBase);
      }
      string language = value.Language;
      if (language != null)
      {
        writer.WriteString(CoreJsonContext.PropName_language, language);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000D1 RID: 209 RVA: 0x0033D808 File Offset: 0x0033D808
    private static JsonParameterInfoValues[] EarsTranscribeRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "AudioWavBase64",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Language",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000018 RID: 24
    // (get) Token: 0x060000D2 RID: 210 RVA: 0x0033D89C File Offset: 0x0033D89C
    [Nullable(0)]
    public JsonTypeInfo<EarsTranscribeResponse> EarsTranscribeResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<EarsTranscribeResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._EarsTranscribeResponse) == null)
        {
          jsonTypeInfo = (this._EarsTranscribeResponse = (JsonTypeInfo<EarsTranscribeResponse>)base.Options.GetTypeInfo(typeof(EarsTranscribeResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000D3 RID: 211 RVA: 0x0033D8D8 File Offset: 0x0033D8D8
    private JsonTypeInfo<EarsTranscribeResponse> Create_EarsTranscribeResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<EarsTranscribeResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<EarsTranscribeResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<EarsTranscribeResponse> jsonObjectInfoValues = new JsonObjectInfoValues<EarsTranscribeResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new EarsTranscribeResponse((string)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.EarsTranscribeResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<15>__EarsTranscribeResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<15>__EarsTranscribeResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.EarsTranscribeResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(EarsTranscribeResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, EarsTranscribeResponse>(this.EarsTranscribeResponseSerializeHandler);
        JsonObjectInfoValues<EarsTranscribeResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<EarsTranscribeResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000D4 RID: 212 RVA: 0x0033D9C4 File Offset: 0x0033D9C4
    private static JsonPropertyInfo[] EarsTranscribeResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(EarsTranscribeResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((EarsTranscribeResponse)obj).Text;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Text";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(EarsTranscribeResponse).GetProperty("Text", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      return array;
    }

    // Token: 0x060000D5 RID: 213 RVA: 0x0033DAC0 File Offset: 0x0033DAC0
    private void EarsTranscribeResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] EarsTranscribeResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string text = value.Text;
      if (text != null)
      {
        writer.WriteString(CoreJsonContext.PropName_text, text);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000D6 RID: 214 RVA: 0x0033DAFC File Offset: 0x0033DAFC
    private static JsonParameterInfoValues[] EarsTranscribeResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Text",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000019 RID: 25
    // (get) Token: 0x060000D7 RID: 215 RVA: 0x0033DB50 File Offset: 0x0033DB50
    [Nullable(0)]
    public JsonTypeInfo<ErrorReport> ErrorReport
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ErrorReport> jsonTypeInfo;
        if ((jsonTypeInfo = this._ErrorReport) == null)
        {
          jsonTypeInfo = (this._ErrorReport = (JsonTypeInfo<ErrorReport>)base.Options.GetTypeInfo(typeof(ErrorReport)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000D8 RID: 216 RVA: 0x0033DB8C File Offset: 0x0033DB8C
    private JsonTypeInfo<ErrorReport> Create_ErrorReport(JsonSerializerOptions options)
    {
      JsonTypeInfo<ErrorReport> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ErrorReport>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ErrorReport> jsonObjectInfoValues = new JsonObjectInfoValues<ErrorReport>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ErrorReport((string)args[0], (string)args[1], (string)args[2], (string)args[3], (string)args[4], (int)args[5], (string)args[6], (string)args[7], (string)args[8]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ErrorReportPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<16>__ErrorReportCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<16>__ErrorReportCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ErrorReportCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ErrorReport).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(int),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ErrorReport>(this.ErrorReportSerializeHandler);
        JsonObjectInfoValues<ErrorReport> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ErrorReport>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000D9 RID: 217 RVA: 0x0033DC78 File Offset: 0x0033DC78
    private static JsonPropertyInfo[] ErrorReportPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[9];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ErrorReport)obj).Stage;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Stage";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("Stage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ErrorReport)obj).ErrorKind;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ErrorKind";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("ErrorKind", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ErrorReport)obj).ErrorMessage;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "ErrorMessage";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("ErrorMessage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ErrorReport)obj).UserFacingMessage;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "UserFacingMessage";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("UserFacingMessage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ErrorReport)obj).AppVersion;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "AppVersion";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("AppVersion", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ErrorReport)obj).ErrorCode;
      jsonPropertyInfoValues11.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ErrorCode";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("ErrorCode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((ErrorReport)obj).ErrorDomain;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "ErrorDomain";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("ErrorDomain", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((ErrorReport)obj).SupabaseUserId;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "SupabaseUserId";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("SupabaseUserId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(ErrorReport);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((ErrorReport)obj).UserEmail;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "UserEmail";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(ErrorReport).GetProperty("UserEmail", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues18);
      return array;
    }

    // Token: 0x060000DA RID: 218 RVA: 0x0033E534 File Offset: 0x0033E534
    private void ErrorReportSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ErrorReport value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string stage = value.Stage;
      if (stage != null)
      {
        writer.WriteString(CoreJsonContext.PropName_stage, stage);
      }
      string errorKind = value.ErrorKind;
      if (errorKind != null)
      {
        writer.WriteString(CoreJsonContext.PropName_error_kind, errorKind);
      }
      string errorMessage = value.ErrorMessage;
      if (errorMessage != null)
      {
        writer.WriteString(CoreJsonContext.PropName_error_message, errorMessage);
      }
      string userFacingMessage = value.UserFacingMessage;
      if (userFacingMessage != null)
      {
        writer.WriteString(CoreJsonContext.PropName_user_facing_message, userFacingMessage);
      }
      string appVersion = value.AppVersion;
      if (appVersion != null)
      {
        writer.WriteString(CoreJsonContext.PropName_app_version, appVersion);
      }
      writer.WriteNumber(CoreJsonContext.PropName_error_code, value.ErrorCode);
      string errorDomain = value.ErrorDomain;
      if (errorDomain != null)
      {
        writer.WriteString(CoreJsonContext.PropName_error_domain, errorDomain);
      }
      string supabaseUserId = value.SupabaseUserId;
      if (supabaseUserId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_supabase_user_id, supabaseUserId);
      }
      string userEmail = value.UserEmail;
      if (userEmail != null)
      {
        writer.WriteString(CoreJsonContext.PropName_user_email, userEmail);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000DB RID: 219 RVA: 0x0033E624 File Offset: 0x0033E624
    private static JsonParameterInfoValues[] ErrorReportCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Stage",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ErrorKind",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ErrorMessage",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "UserFacingMessage",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "AppVersion",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ErrorCode",
          ParameterType = typeof(int),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ErrorDomain",
          ParameterType = typeof(string),
          Position = 6,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "SupabaseUserId",
          ParameterType = typeof(string),
          Position = 7,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "UserEmail",
          ParameterType = typeof(string),
          Position = 8,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700001A RID: 26
    // (get) Token: 0x060000DC RID: 220 RVA: 0x0033E870 File Offset: 0x0033E870
    [Nullable(0)]
    public JsonTypeInfo<ImageSearchRequest> ImageSearchRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ImageSearchRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._ImageSearchRequest) == null)
        {
          jsonTypeInfo = (this._ImageSearchRequest = (JsonTypeInfo<ImageSearchRequest>)base.Options.GetTypeInfo(typeof(ImageSearchRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000DD RID: 221 RVA: 0x0033E8AC File Offset: 0x0033E8AC
    private JsonTypeInfo<ImageSearchRequest> Create_ImageSearchRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<ImageSearchRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ImageSearchRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ImageSearchRequest> jsonObjectInfoValues = new JsonObjectInfoValues<ImageSearchRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ImageSearchRequest((string)args[0], (int)args[1], (bool?)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ImageSearchRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<17>__ImageSearchRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<17>__ImageSearchRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ImageSearchRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ImageSearchRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(int),
          typeof(bool?),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ImageSearchRequest>(this.ImageSearchRequestSerializeHandler);
        JsonObjectInfoValues<ImageSearchRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ImageSearchRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000DE RID: 222 RVA: 0x0033E998 File Offset: 0x0033E998
    private static JsonPropertyInfo[] ImageSearchRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ImageSearchRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ImageSearchRequest)obj).Query;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Query";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ImageSearchRequest).GetProperty("Query", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ImageSearchRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ImageSearchRequest)obj).Limit;
      jsonPropertyInfoValues3.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Limit";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ImageSearchRequest).GetProperty("Limit", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ImageSearchRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ImageSearchRequest)obj).SubjectPhoto;
      jsonPropertyInfoValues5.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "SubjectPhoto";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ImageSearchRequest).GetProperty("SubjectPhoto", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ImageSearchRequest);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ImageSearchRequest)obj).PageUrl;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "PageUrl";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ImageSearchRequest).GetProperty("PageUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      return array;
    }

    // Token: 0x060000DF RID: 223 RVA: 0x0033ED6C File Offset: 0x0033ED6C
    private void ImageSearchRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ImageSearchRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string query = value.Query;
      if (query != null)
      {
        writer.WriteString(CoreJsonContext.PropName_query, query);
      }
      writer.WriteNumber(CoreJsonContext.PropName_limit, value.Limit);
      bool? subjectPhoto = value.SubjectPhoto;
      if (subjectPhoto != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_subject_photo);
        JsonSerializer.Serialize<bool?>(writer, subjectPhoto, this.NullableBoolean);
      }
      string pageUrl = value.PageUrl;
      if (pageUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_page_url, pageUrl);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000E0 RID: 224 RVA: 0x0033EDF4 File Offset: 0x0033EDF4
    private static JsonParameterInfoValues[] ImageSearchRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Query",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Limit",
          ParameterType = typeof(int),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "SubjectPhoto",
          ParameterType = typeof(bool?),
          Position = 2,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "PageUrl",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700001B RID: 27
    // (get) Token: 0x060000E1 RID: 225 RVA: 0x0033EF04 File Offset: 0x0033EF04
    [Nullable(0)]
    public JsonTypeInfo<ImageSearchResponse> ImageSearchResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ImageSearchResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._ImageSearchResponse) == null)
        {
          jsonTypeInfo = (this._ImageSearchResponse = (JsonTypeInfo<ImageSearchResponse>)base.Options.GetTypeInfo(typeof(ImageSearchResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000E2 RID: 226 RVA: 0x0033EF40 File Offset: 0x0033EF40
    private JsonTypeInfo<ImageSearchResponse> Create_ImageSearchResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<ImageSearchResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ImageSearchResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ImageSearchResponse> jsonObjectInfoValues = new JsonObjectInfoValues<ImageSearchResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ImageSearchResponse((IReadOnlyList<ImageSearchResult>)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ImageSearchResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<18>__ImageSearchResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<18>__ImageSearchResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ImageSearchResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ImageSearchResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(IReadOnlyList<ImageSearchResult>) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ImageSearchResponse>(this.ImageSearchResponseSerializeHandler);
        JsonObjectInfoValues<ImageSearchResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ImageSearchResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000E3 RID: 227 RVA: 0x0033F02C File Offset: 0x0033F02C
    private static JsonPropertyInfo[] ImageSearchResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<IReadOnlyList<ImageSearchResult>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<ImageSearchResult>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ImageSearchResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ImageSearchResponse)obj).Images;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ImageSearchResult> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Images";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ImageSearchResponse).GetProperty("Images", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<ImageSearchResult>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<ImageSearchResult>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<ImageSearchResult>>(options, jsonPropertyInfoValues2);
      return array;
    }

    // Token: 0x060000E4 RID: 228 RVA: 0x0033F128 File Offset: 0x0033F128
    private void ImageSearchResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ImageSearchResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<ImageSearchResult> images = value.Images;
      if (images != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_images);
        this.IReadOnlyListImageSearchResultSerializeHandler(writer, images);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000E5 RID: 229 RVA: 0x0033F168 File Offset: 0x0033F168
    private static JsonParameterInfoValues[] ImageSearchResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Images",
          ParameterType = typeof(IReadOnlyList<ImageSearchResult>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700001C RID: 28
    // (get) Token: 0x060000E6 RID: 230 RVA: 0x0033F1BC File Offset: 0x0033F1BC
    [Nullable(0)]
    public JsonTypeInfo<ImageSearchResult> ImageSearchResult
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ImageSearchResult> jsonTypeInfo;
        if ((jsonTypeInfo = this._ImageSearchResult) == null)
        {
          jsonTypeInfo = (this._ImageSearchResult = (JsonTypeInfo<ImageSearchResult>)base.Options.GetTypeInfo(typeof(ImageSearchResult)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000E7 RID: 231 RVA: 0x0033F1F8 File Offset: 0x0033F1F8
    private JsonTypeInfo<ImageSearchResult> Create_ImageSearchResult(JsonSerializerOptions options)
    {
      JsonTypeInfo<ImageSearchResult> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ImageSearchResult>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ImageSearchResult> jsonObjectInfoValues = new JsonObjectInfoValues<ImageSearchResult>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ImageSearchResult((string)args[0], (string)args[1], (string)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ImageSearchResultPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<19>__ImageSearchResultCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<19>__ImageSearchResultCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ImageSearchResultCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ImageSearchResult).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ImageSearchResult>(this.ImageSearchResultSerializeHandler);
        JsonObjectInfoValues<ImageSearchResult> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ImageSearchResult>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000E8 RID: 232 RVA: 0x0033F2E4 File Offset: 0x0033F2E4
    private static JsonPropertyInfo[] ImageSearchResultPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ImageSearchResult);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ImageSearchResult)obj).ThumbnailUrl;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "ThumbnailUrl";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ImageSearchResult).GetProperty("ThumbnailUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ImageSearchResult);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ImageSearchResult)obj).FullUrl;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "FullUrl";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ImageSearchResult).GetProperty("FullUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ImageSearchResult);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ImageSearchResult)obj).SourceUrl;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "SourceUrl";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ImageSearchResult).GetProperty("SourceUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ImageSearchResult);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ImageSearchResult)obj).Title;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Title";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ImageSearchResult).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      return array;
    }

    // Token: 0x060000E9 RID: 233 RVA: 0x0033F6EC File Offset: 0x0033F6EC
    private void ImageSearchResultSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ImageSearchResult value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string thumbnailUrl = value.ThumbnailUrl;
      if (thumbnailUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_thumbnail_url, thumbnailUrl);
      }
      string fullUrl = value.FullUrl;
      if (fullUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_full_url, fullUrl);
      }
      string sourceUrl = value.SourceUrl;
      if (sourceUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_source_url, sourceUrl);
      }
      string title = value.Title;
      if (title != null)
      {
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000EA RID: 234 RVA: 0x0033F768 File Offset: 0x0033F768
    private static JsonParameterInfoValues[] ImageSearchResultCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "ThumbnailUrl",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "FullUrl",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "SourceUrl",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Title",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700001D RID: 29
    // (get) Token: 0x060000EB RID: 235 RVA: 0x0033F878 File Offset: 0x0033F878
    [Nullable(0)]
    public JsonTypeInfo<InterviewLine> InterviewLine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<InterviewLine> jsonTypeInfo;
        if ((jsonTypeInfo = this._InterviewLine) == null)
        {
          jsonTypeInfo = (this._InterviewLine = (JsonTypeInfo<InterviewLine>)base.Options.GetTypeInfo(typeof(InterviewLine)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000EC RID: 236 RVA: 0x0033F8B4 File Offset: 0x0033F8B4
    private JsonTypeInfo<InterviewLine> Create_InterviewLine(JsonSerializerOptions options)
    {
      JsonTypeInfo<InterviewLine> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<InterviewLine>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<InterviewLine> jsonObjectInfoValues = new JsonObjectInfoValues<InterviewLine>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new InterviewLine((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.InterviewLinePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<20>__InterviewLineCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<20>__InterviewLineCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.InterviewLineCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(InterviewLine).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, InterviewLine>(this.InterviewLineSerializeHandler);
        JsonObjectInfoValues<InterviewLine> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<InterviewLine>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000ED RID: 237 RVA: 0x0033F9A0 File Offset: 0x0033F9A0
    private static JsonPropertyInfo[] InterviewLinePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(InterviewLine);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((InterviewLine)obj).Role;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Role";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(InterviewLine).GetProperty("Role", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(InterviewLine);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((InterviewLine)obj).Text;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Text";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(InterviewLine).GetProperty("Text", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      return array;
    }

    // Token: 0x060000EE RID: 238 RVA: 0x0033FBAC File Offset: 0x0033FBAC
    private void InterviewLineSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] InterviewLine value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string role = value.Role;
      if (role != null)
      {
        writer.WriteString(CoreJsonContext.PropName_role, role);
      }
      string text = value.Text;
      if (text != null)
      {
        writer.WriteString(CoreJsonContext.PropName_text, text);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000EF RID: 239 RVA: 0x0033FBFC File Offset: 0x0033FBFC
    private static JsonParameterInfoValues[] InterviewLineCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Role",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Text",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700001E RID: 30
    // (get) Token: 0x060000F0 RID: 240 RVA: 0x0033FC90 File Offset: 0x0033FC90
    [Nullable(0)]
    public JsonTypeInfo<PlanSnapshot> PlanSnapshot
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PlanSnapshot> jsonTypeInfo;
        if ((jsonTypeInfo = this._PlanSnapshot) == null)
        {
          jsonTypeInfo = (this._PlanSnapshot = (JsonTypeInfo<PlanSnapshot>)base.Options.GetTypeInfo(typeof(PlanSnapshot)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000F1 RID: 241 RVA: 0x0033FCCC File Offset: 0x0033FCCC
    private JsonTypeInfo<PlanSnapshot> Create_PlanSnapshot(JsonSerializerOptions options)
    {
      JsonTypeInfo<PlanSnapshot> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PlanSnapshot>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PlanSnapshot> jsonObjectInfoValues = new JsonObjectInfoValues<PlanSnapshot>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PlanSnapshot((string)args[0], (PlanUsage)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5], (PlanSubscription)args[6], (int?)args[7], (int?)args[8], (int?)args[9], (bool?)args[10], (bool?)args[11], (bool?)args[12]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PlanSnapshotPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<21>__PlanSnapshotCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<21>__PlanSnapshotCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PlanSnapshotCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PlanSnapshot).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(PlanUsage),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(PlanSubscription),
          typeof(int?),
          typeof(int?),
          typeof(int?),
          typeof(bool?),
          typeof(bool?),
          typeof(bool?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PlanSnapshot>(this.PlanSnapshotSerializeHandler);
        JsonObjectInfoValues<PlanSnapshot> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PlanSnapshot>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000F2 RID: 242 RVA: 0x0033FDB8 File Offset: 0x0033FDB8
    private static JsonPropertyInfo[] PlanSnapshotPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[15];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PlanSnapshot)obj).Plan;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Plan";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("Plan", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<PlanUsage> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<PlanUsage>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PlanSnapshot)obj).Usage;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] PlanUsage value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Usage";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("Usage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PlanUsage), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PlanUsage> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<PlanUsage>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PlanSnapshot)obj).BillingEnforcement;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "BillingEnforcement";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("BillingEnforcement", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((PlanSnapshot)obj).Tier;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Tier";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("Tier", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((PlanSnapshot)obj).TierSource;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "TierSource";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("TierSource", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((PlanSnapshot)obj).TeamName;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "TeamName";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("TeamName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<PlanSubscription> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<PlanSubscription>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((PlanSnapshot)obj).Subscription;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] PlanSubscription value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "Subscription";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("Subscription", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PlanSubscription), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PlanSubscription> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<PlanSubscription>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((PlanSnapshot)obj).UnlimitedAgentsCap;
      jsonPropertyInfoValues15.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "UnlimitedAgentsCap";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("UnlimitedAgentsCap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((PlanSnapshot)obj).ProAgentsCap;
      jsonPropertyInfoValues17.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "ProAgentsCap";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("ProAgentsCap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((PlanSnapshot)obj).MaxAgentsCap;
      jsonPropertyInfoValues19.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "MaxAgentsCap";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("MaxAgentsCap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((PlanSnapshot)obj).ProYearlyAvailable;
      jsonPropertyInfoValues21.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "ProYearlyAvailable";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("ProYearlyAvailable", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((PlanSnapshot)obj).MaxYearlyAvailable;
      jsonPropertyInfoValues23.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "MaxYearlyAvailable";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("MaxYearlyAvailable", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = (object obj) => ((PlanSnapshot)obj).IsFailOpenFallback;
      jsonPropertyInfoValues25.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues25.IgnoreCondition = null;
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "IsFailOpenFallback";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("IsFailOpenFallback", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues26);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues27 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues27.IsProperty = true;
      jsonPropertyInfoValues27.IsPublic = true;
      jsonPropertyInfoValues27.IsVirtual = false;
      jsonPropertyInfoValues27.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues27.Converter = null;
      jsonPropertyInfoValues27.Getter = null;
      jsonPropertyInfoValues27.Setter = null;
      jsonPropertyInfoValues27.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues27.HasJsonInclude = false;
      jsonPropertyInfoValues27.IsExtensionData = false;
      jsonPropertyInfoValues27.NumberHandling = null;
      jsonPropertyInfoValues27.PropertyName = "EffectiveTier";
      jsonPropertyInfoValues27.JsonPropertyName = null;
      jsonPropertyInfoValues27.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("EffectiveTier", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues28 = jsonPropertyInfoValues27;
      array[13] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues28);
      array[13].IsGetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues29 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues29.IsProperty = true;
      jsonPropertyInfoValues29.IsPublic = true;
      jsonPropertyInfoValues29.IsVirtual = false;
      jsonPropertyInfoValues29.DeclaringType = typeof(PlanSnapshot);
      jsonPropertyInfoValues29.Converter = null;
      jsonPropertyInfoValues29.Getter = null;
      jsonPropertyInfoValues29.Setter = null;
      jsonPropertyInfoValues29.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues29.HasJsonInclude = false;
      jsonPropertyInfoValues29.IsExtensionData = false;
      jsonPropertyInfoValues29.NumberHandling = null;
      jsonPropertyInfoValues29.PropertyName = "IsFreeUserOverMessageCap";
      jsonPropertyInfoValues29.JsonPropertyName = null;
      jsonPropertyInfoValues29.AttributeProviderFactory = () => typeof(PlanSnapshot).GetProperty("IsFreeUserOverMessageCap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues30 = jsonPropertyInfoValues29;
      array[14] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues30);
      return array;
    }

    // Token: 0x060000F3 RID: 243 RVA: 0x00340B70 File Offset: 0x00340B70
    private void PlanSnapshotSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PlanSnapshot value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string plan = value.Plan;
      if (plan != null)
      {
        writer.WriteString(CoreJsonContext.PropName_plan, plan);
      }
      PlanUsage usage = value.Usage;
      if (usage != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_usage);
        this.PlanUsageSerializeHandler(writer, usage);
      }
      string billingEnforcement = value.BillingEnforcement;
      if (billingEnforcement != null)
      {
        writer.WriteString(CoreJsonContext.PropName_billing_enforcement, billingEnforcement);
      }
      string tier = value.Tier;
      if (tier != null)
      {
        writer.WriteString(CoreJsonContext.PropName_tier, tier);
      }
      string tierSource = value.TierSource;
      if (tierSource != null)
      {
        writer.WriteString(CoreJsonContext.PropName_tier_source, tierSource);
      }
      string teamName = value.TeamName;
      if (teamName != null)
      {
        writer.WriteString(CoreJsonContext.PropName_team_name, teamName);
      }
      PlanSubscription subscription = value.Subscription;
      if (subscription != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_subscription);
        this.PlanSubscriptionSerializeHandler(writer, subscription);
      }
      int? unlimitedAgentsCap = value.UnlimitedAgentsCap;
      if (unlimitedAgentsCap != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_unlimited_agents_cap);
        JsonSerializer.Serialize<int?>(writer, unlimitedAgentsCap, this.NullableInt32);
      }
      int? proAgentsCap = value.ProAgentsCap;
      if (proAgentsCap != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_pro_agents_cap);
        JsonSerializer.Serialize<int?>(writer, proAgentsCap, this.NullableInt32);
      }
      int? maxAgentsCap = value.MaxAgentsCap;
      if (maxAgentsCap != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_max_agents_cap);
        JsonSerializer.Serialize<int?>(writer, maxAgentsCap, this.NullableInt32);
      }
      bool? proYearlyAvailable = value.ProYearlyAvailable;
      if (proYearlyAvailable != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_pro_yearly_available);
        JsonSerializer.Serialize<bool?>(writer, proYearlyAvailable, this.NullableBoolean);
      }
      bool? maxYearlyAvailable = value.MaxYearlyAvailable;
      if (maxYearlyAvailable != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_max_yearly_available);
        JsonSerializer.Serialize<bool?>(writer, maxYearlyAvailable, this.NullableBoolean);
      }
      bool? isFailOpenFallback = value.IsFailOpenFallback;
      if (isFailOpenFallback != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_is_fail_open_fallback);
        JsonSerializer.Serialize<bool?>(writer, isFailOpenFallback, this.NullableBoolean);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000F4 RID: 244 RVA: 0x00340D40 File Offset: 0x00340D40
    private static JsonParameterInfoValues[] PlanSnapshotCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Plan",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Usage",
          ParameterType = typeof(PlanUsage),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "BillingEnforcement",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Tier",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "TierSource",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "TeamName",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Subscription",
          ParameterType = typeof(PlanSubscription),
          Position = 6,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "UnlimitedAgentsCap",
          ParameterType = typeof(int?),
          Position = 7,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ProAgentsCap",
          ParameterType = typeof(int?),
          Position = 8,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "MaxAgentsCap",
          ParameterType = typeof(int?),
          Position = 9,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ProYearlyAvailable",
          ParameterType = typeof(bool?),
          Position = 10,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "MaxYearlyAvailable",
          ParameterType = typeof(bool?),
          Position = 11,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsFailOpenFallback",
          ParameterType = typeof(bool?),
          Position = 12,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700001F RID: 31
    // (get) Token: 0x060000F5 RID: 245 RVA: 0x00341090 File Offset: 0x00341090
    [Nullable(0)]
    public JsonTypeInfo<PlanSubscription> PlanSubscription
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PlanSubscription> jsonTypeInfo;
        if ((jsonTypeInfo = this._PlanSubscription) == null)
        {
          jsonTypeInfo = (this._PlanSubscription = (JsonTypeInfo<PlanSubscription>)base.Options.GetTypeInfo(typeof(PlanSubscription)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000F6 RID: 246 RVA: 0x003410CC File Offset: 0x003410CC
    private JsonTypeInfo<PlanSubscription> Create_PlanSubscription(JsonSerializerOptions options)
    {
      JsonTypeInfo<PlanSubscription> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PlanSubscription>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PlanSubscription> jsonObjectInfoValues = new JsonObjectInfoValues<PlanSubscription>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PlanSubscription((string)args[0], (bool)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5], (string)args[6]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PlanSubscriptionPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<22>__PlanSubscriptionCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<22>__PlanSubscriptionCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PlanSubscriptionCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PlanSubscription).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(bool),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PlanSubscription>(this.PlanSubscriptionSerializeHandler);
        JsonObjectInfoValues<PlanSubscription> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PlanSubscription>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000F7 RID: 247 RVA: 0x003411B8 File Offset: 0x003411B8
    private static JsonPropertyInfo[] PlanSubscriptionPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PlanSubscription)obj).Status;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Status";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("Status", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PlanSubscription)obj).CancelAtPeriodEnd;
      jsonPropertyInfoValues3.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "CancelAtPeriodEnd";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("CancelAtPeriodEnd", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PlanSubscription)obj).CurrentPeriodStart;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "CurrentPeriodStart";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("CurrentPeriodStart", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((PlanSubscription)obj).CurrentPeriodEnd;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "CurrentPeriodEnd";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("CurrentPeriodEnd", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((PlanSubscription)obj).ProductId;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "ProductId";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("ProductId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((PlanSubscription)obj).CancelAt;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "CancelAt";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("CancelAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(PlanSubscription);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((PlanSubscription)obj).BillingInterval;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "BillingInterval";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(PlanSubscription).GetProperty("BillingInterval", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
      return array;
    }

    // Token: 0x060000F8 RID: 248 RVA: 0x00341850 File Offset: 0x00341850
    private void PlanSubscriptionSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PlanSubscription value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string status = value.Status;
      if (status != null)
      {
        writer.WriteString(CoreJsonContext.PropName_status, status);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_cancel_at_period_end, value.CancelAtPeriodEnd);
      string currentPeriodStart = value.CurrentPeriodStart;
      if (currentPeriodStart != null)
      {
        writer.WriteString(CoreJsonContext.PropName_current_period_start, currentPeriodStart);
      }
      string currentPeriodEnd = value.CurrentPeriodEnd;
      if (currentPeriodEnd != null)
      {
        writer.WriteString(CoreJsonContext.PropName_current_period_end, currentPeriodEnd);
      }
      string productId = value.ProductId;
      if (productId != null)
      {
        writer.WriteString(CoreJsonContext.PropName_product_id, productId);
      }
      string cancelAt = value.CancelAt;
      if (cancelAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_cancel_at, cancelAt);
      }
      string billingInterval = value.BillingInterval;
      if (billingInterval != null)
      {
        writer.WriteString(CoreJsonContext.PropName_billing_interval, billingInterval);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000F9 RID: 249 RVA: 0x00341910 File Offset: 0x00341910
    private static JsonParameterInfoValues[] PlanSubscriptionCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Status",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CancelAtPeriodEnd",
          ParameterType = typeof(bool),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CurrentPeriodStart",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "CurrentPeriodEnd",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ProductId",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "CancelAt",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "BillingInterval",
          ParameterType = typeof(string),
          Position = 6,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000020 RID: 32
    // (get) Token: 0x060000FA RID: 250 RVA: 0x00341ADC File Offset: 0x00341ADC
    [Nullable(0)]
    public JsonTypeInfo<PlanUsage> PlanUsage
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PlanUsage> jsonTypeInfo;
        if ((jsonTypeInfo = this._PlanUsage) == null)
        {
          jsonTypeInfo = (this._PlanUsage = (JsonTypeInfo<PlanUsage>)base.Options.GetTypeInfo(typeof(PlanUsage)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x060000FB RID: 251 RVA: 0x00341B18 File Offset: 0x00341B18
    private JsonTypeInfo<PlanUsage> Create_PlanUsage(JsonSerializerOptions options)
    {
      JsonTypeInfo<PlanUsage> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PlanUsage>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PlanUsage> jsonObjectInfoValues = new JsonObjectInfoValues<PlanUsage>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PlanUsage((PlanUsageBucket)args[0], (PlanUsageBucket)args[1], (PlanUsageBucket)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PlanUsagePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<23>__PlanUsageCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<23>__PlanUsageCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PlanUsageCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PlanUsage).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(PlanUsageBucket),
          typeof(PlanUsageBucket),
          typeof(PlanUsageBucket),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PlanUsage>(this.PlanUsageSerializeHandler);
        JsonObjectInfoValues<PlanUsage> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PlanUsage>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x060000FC RID: 252 RVA: 0x00341C04 File Offset: 0x00341C04
    private static JsonPropertyInfo[] PlanUsagePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues = new JsonPropertyInfoValues<PlanUsageBucket>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PlanUsage);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PlanUsage)obj).Messages;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] PlanUsageBucket value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Messages";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PlanUsage).GetProperty("Messages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PlanUsageBucket), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<PlanUsageBucket>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<PlanUsageBucket>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PlanUsage);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((PlanUsage)obj).Agents;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] PlanUsageBucket value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Agents";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PlanUsage).GetProperty("Agents", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PlanUsageBucket), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<PlanUsageBucket>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<PlanUsageBucket>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(PlanUsage);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((PlanUsage)obj).Dictation;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] PlanUsageBucket value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Dictation";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(PlanUsage).GetProperty("Dictation", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(PlanUsageBucket), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<PlanUsageBucket> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<PlanUsageBucket>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(PlanUsage);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((PlanUsage)obj).WindowResetsAt;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "WindowResetsAt";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(PlanUsage).GetProperty("WindowResetsAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      return array;
    }

    // Token: 0x060000FD RID: 253 RVA: 0x00341FE8 File Offset: 0x00341FE8
    private void PlanUsageSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PlanUsage value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      PlanUsageBucket messages = value.Messages;
      if (messages != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_messages);
        JsonSerializer.Serialize<PlanUsageBucket>(writer, messages, this.PlanUsageBucket);
      }
      PlanUsageBucket agents = value.Agents;
      if (agents != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_agents);
        JsonSerializer.Serialize<PlanUsageBucket>(writer, agents, this.PlanUsageBucket);
      }
      PlanUsageBucket dictation = value.Dictation;
      if (dictation != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_dictation);
        JsonSerializer.Serialize<PlanUsageBucket>(writer, dictation, this.PlanUsageBucket);
      }
      string windowResetsAt = value.WindowResetsAt;
      if (windowResetsAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_window_resets_at, windowResetsAt);
      }
      writer.WriteEndObject();
    }

    // Token: 0x060000FE RID: 254 RVA: 0x00342088 File Offset: 0x00342088
    private static JsonParameterInfoValues[] PlanUsageCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Messages",
          ParameterType = typeof(PlanUsageBucket),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Agents",
          ParameterType = typeof(PlanUsageBucket),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Dictation",
          ParameterType = typeof(PlanUsageBucket),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "WindowResetsAt",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000021 RID: 33
    // (get) Token: 0x060000FF RID: 255 RVA: 0x00342198 File Offset: 0x00342198
    [Nullable(0)]
    public JsonTypeInfo<PlanUsageBucket> PlanUsageBucket
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PlanUsageBucket> jsonTypeInfo;
        if ((jsonTypeInfo = this._PlanUsageBucket) == null)
        {
          jsonTypeInfo = (this._PlanUsageBucket = (JsonTypeInfo<PlanUsageBucket>)base.Options.GetTypeInfo(typeof(PlanUsageBucket)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000100 RID: 256 RVA: 0x003421D4 File Offset: 0x003421D4
    private JsonTypeInfo<PlanUsageBucket> Create_PlanUsageBucket(JsonSerializerOptions options)
    {
      JsonTypeInfo<PlanUsageBucket> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PlanUsageBucket>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PlanUsageBucket> jsonObjectInfoValues = new JsonObjectInfoValues<PlanUsageBucket>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PlanUsageBucket((int)args[0], (int)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PlanUsageBucketPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<24>__PlanUsageBucketCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<24>__PlanUsageBucketCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PlanUsageBucketCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PlanUsageBucket).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(int),
          typeof(int)
        }, null);
        jsonObjectInfoValues.SerializeHandler = null;
        JsonObjectInfoValues<PlanUsageBucket> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PlanUsageBucket>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000101 RID: 257 RVA: 0x003422B8 File Offset: 0x003422B8
    private static JsonPropertyInfo[] PlanUsageBucketPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<int> jsonPropertyInfoValues = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PlanUsageBucket);
      jsonPropertyInfoValues.Converter = (JsonConverter<int>)CoreJsonContext.ExpandConverter(typeof(int), new SaturatingInt32Converter(), options, true);
      jsonPropertyInfoValues.Getter = (object obj) => ((PlanUsageBucket)obj).Used;
      jsonPropertyInfoValues.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Used";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PlanUsageBucket).GetProperty("Used", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(PlanUsageBucket);
      jsonPropertyInfoValues3.Converter = (JsonConverter<int>)CoreJsonContext.ExpandConverter(typeof(int), new SaturatingInt32Converter(), options, true);
      jsonPropertyInfoValues3.Getter = (object obj) => ((PlanUsageBucket)obj).Cap;
      jsonPropertyInfoValues3.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Cap";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(PlanUsageBucket).GetProperty("Cap", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x06000102 RID: 258 RVA: 0x003424D4 File Offset: 0x003424D4
    private static JsonParameterInfoValues[] PlanUsageBucketCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Used",
          ParameterType = typeof(int),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Cap",
          ParameterType = typeof(int),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000022 RID: 34
    // (get) Token: 0x06000103 RID: 259 RVA: 0x00342568 File Offset: 0x00342568
    [Nullable(0)]
    public JsonTypeInfo<PortalResponse> PortalResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<PortalResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._PortalResponse) == null)
        {
          jsonTypeInfo = (this._PortalResponse = (JsonTypeInfo<PortalResponse>)base.Options.GetTypeInfo(typeof(PortalResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000104 RID: 260 RVA: 0x003425A4 File Offset: 0x003425A4
    private JsonTypeInfo<PortalResponse> Create_PortalResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<PortalResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<PortalResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<PortalResponse> jsonObjectInfoValues = new JsonObjectInfoValues<PortalResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new PortalResponse((string)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.PortalResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<25>__PortalResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<25>__PortalResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.PortalResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(PortalResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, PortalResponse>(this.PortalResponseSerializeHandler);
        JsonObjectInfoValues<PortalResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<PortalResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000105 RID: 261 RVA: 0x00342690 File Offset: 0x00342690
    private static JsonPropertyInfo[] PortalResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(PortalResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((PortalResponse)obj).Url;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Url";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(PortalResponse).GetProperty("Url", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000106 RID: 262 RVA: 0x003427A0 File Offset: 0x003427A0
    private void PortalResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] PortalResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string url = value.Url;
      if (url != null)
      {
        writer.WriteString(CoreJsonContext.PropName_url, url);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000107 RID: 263 RVA: 0x003427DC File Offset: 0x003427DC
    private static JsonParameterInfoValues[] PortalResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Url",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000023 RID: 35
    // (get) Token: 0x06000108 RID: 264 RVA: 0x00342830 File Offset: 0x00342830
    [Nullable(0)]
    public JsonTypeInfo<RealtimeSessionPromptContext> RealtimeSessionPromptContext
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<RealtimeSessionPromptContext> jsonTypeInfo;
        if ((jsonTypeInfo = this._RealtimeSessionPromptContext) == null)
        {
          jsonTypeInfo = (this._RealtimeSessionPromptContext = (JsonTypeInfo<RealtimeSessionPromptContext>)base.Options.GetTypeInfo(typeof(RealtimeSessionPromptContext)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000109 RID: 265 RVA: 0x0034286C File Offset: 0x0034286C
    private JsonTypeInfo<RealtimeSessionPromptContext> Create_RealtimeSessionPromptContext(JsonSerializerOptions options)
    {
      JsonTypeInfo<RealtimeSessionPromptContext> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<RealtimeSessionPromptContext>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<RealtimeSessionPromptContext> jsonObjectInfoValues = new JsonObjectInfoValues<RealtimeSessionPromptContext>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new RealtimeSessionPromptContext((IReadOnlyList<string>)args[0], (IReadOnlyList<string>)args[1], (int)args[2], (string)args[3], (string)args[4], (string)args[5], (IReadOnlyList<JsonElement>)args[6], (string)args[7]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.RealtimeSessionPromptContextPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<26>__RealtimeSessionPromptContextCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<26>__RealtimeSessionPromptContextCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.RealtimeSessionPromptContextCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(IReadOnlyList<string>),
          typeof(IReadOnlyList<string>),
          typeof(int),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<JsonElement>),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, RealtimeSessionPromptContext>(this.RealtimeSessionPromptContextSerializeHandler);
        JsonObjectInfoValues<RealtimeSessionPromptContext> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<RealtimeSessionPromptContext>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600010A RID: 266 RVA: 0x00342958 File Offset: 0x00342958
    private static JsonPropertyInfo[] RealtimeSessionPromptContextPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[8];
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).ConnectedIntegrations;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "ConnectedIntegrations";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("ConnectedIntegrations", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).NeedsReconnectIntegrations;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "NeedsReconnectIntegrations";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("NeedsReconnectIntegrations", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).PriorMessageCount;
      jsonPropertyInfoValues5.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "PriorMessageCount";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("PriorMessageCount", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).TimeZone;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "TimeZone";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("TimeZone", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).CurrentLocalDatetime;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "CurrentLocalDatetime";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("CurrentLocalDatetime", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).VoiceGender;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "VoiceGender";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("VoiceGender", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      array[5].IsGetNullable = false;
      array[5].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<JsonElement>> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<IReadOnlyList<JsonElement>>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).ActiveSkills;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] IReadOnlyList<JsonElement> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "ActiveSkills";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("ActiveSkills", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<JsonElement>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<JsonElement>> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<JsonElement>>(options, jsonPropertyInfoValues14);
      array[6].IsGetNullable = false;
      array[6].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(RealtimeSessionPromptContext);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((RealtimeSessionPromptContext)obj).DictationHoldShortcut;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "DictationHoldShortcut";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(RealtimeSessionPromptContext).GetProperty("DictationHoldShortcut", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
      return array;
    }

    // Token: 0x0600010B RID: 267 RVA: 0x00343138 File Offset: 0x00343138
    private void RealtimeSessionPromptContextSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] RealtimeSessionPromptContext value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<string> connectedIntegrations = value.ConnectedIntegrations;
      if (connectedIntegrations != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_connected_integrations);
        this.IReadOnlyListStringSerializeHandler(writer, connectedIntegrations);
      }
      IReadOnlyList<string> needsReconnectIntegrations = value.NeedsReconnectIntegrations;
      if (needsReconnectIntegrations != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_needs_reconnect_integrations);
        this.IReadOnlyListStringSerializeHandler(writer, needsReconnectIntegrations);
      }
      writer.WriteNumber(CoreJsonContext.PropName_prior_message_count, value.PriorMessageCount);
      string timeZone = value.TimeZone;
      if (timeZone != null)
      {
        writer.WriteString(CoreJsonContext.PropName_time_zone, timeZone);
      }
      string currentLocalDatetime = value.CurrentLocalDatetime;
      if (currentLocalDatetime != null)
      {
        writer.WriteString(CoreJsonContext.PropName_current_local_datetime, currentLocalDatetime);
      }
      string voiceGender = value.VoiceGender;
      if (voiceGender != null)
      {
        writer.WriteString(CoreJsonContext.PropName_voice_gender, voiceGender);
      }
      IReadOnlyList<JsonElement> activeSkills = value.ActiveSkills;
      if (activeSkills != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_active_skills);
        this.IReadOnlyListJsonElementSerializeHandler(writer, activeSkills);
      }
      string dictationHoldShortcut = value.DictationHoldShortcut;
      if (dictationHoldShortcut != null)
      {
        writer.WriteString(CoreJsonContext.PropName_dictation_hold_shortcut, dictationHoldShortcut);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600010C RID: 268 RVA: 0x00343224 File Offset: 0x00343224
    private static JsonParameterInfoValues[] RealtimeSessionPromptContextCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "ConnectedIntegrations",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "NeedsReconnectIntegrations",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "PriorMessageCount",
          ParameterType = typeof(int),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "TimeZone",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "CurrentLocalDatetime",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "VoiceGender",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ActiveSkills",
          ParameterType = typeof(IReadOnlyList<JsonElement>),
          Position = 6,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "DictationHoldShortcut",
          ParameterType = typeof(string),
          Position = 7,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000024 RID: 36
    // (get) Token: 0x0600010D RID: 269 RVA: 0x00343430 File Offset: 0x00343430
    [Nullable(0)]
    public JsonTypeInfo<RealtimeSessionRequest> RealtimeSessionRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<RealtimeSessionRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._RealtimeSessionRequest) == null)
        {
          jsonTypeInfo = (this._RealtimeSessionRequest = (JsonTypeInfo<RealtimeSessionRequest>)base.Options.GetTypeInfo(typeof(RealtimeSessionRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600010E RID: 270 RVA: 0x0034346C File Offset: 0x0034346C
    private JsonTypeInfo<RealtimeSessionRequest> Create_RealtimeSessionRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<RealtimeSessionRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<RealtimeSessionRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<RealtimeSessionRequest> jsonObjectInfoValues = new JsonObjectInfoValues<RealtimeSessionRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new RealtimeSessionRequest((string)args[0], (double)args[1], (RealtimeSessionPromptContext)args[2], (IReadOnlyList<string>)args[3], (string)args[4], (string)args[5], (IReadOnlyList<string>)args[6]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.RealtimeSessionRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<27>__RealtimeSessionRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<27>__RealtimeSessionRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.RealtimeSessionRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(double),
          typeof(RealtimeSessionPromptContext),
          typeof(IReadOnlyList<string>),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<string>)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, RealtimeSessionRequest>(this.RealtimeSessionRequestSerializeHandler);
        JsonObjectInfoValues<RealtimeSessionRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<RealtimeSessionRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600010F RID: 271 RVA: 0x00343558 File Offset: 0x00343558
    private static JsonPropertyInfo[] RealtimeSessionRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((RealtimeSessionRequest)obj).Voice;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Voice";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("Voice", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<double> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<double>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((RealtimeSessionRequest)obj).Speed;
      jsonPropertyInfoValues3.Setter = delegate(object obj, double value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Speed";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("Speed", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<double>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<RealtimeSessionPromptContext> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<RealtimeSessionPromptContext>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((RealtimeSessionRequest)obj).PromptContext;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] RealtimeSessionPromptContext value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "PromptContext";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("PromptContext", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(RealtimeSessionPromptContext), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<RealtimeSessionPromptContext> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<RealtimeSessionPromptContext>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((RealtimeSessionRequest)obj).ClientCapabilities;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "ClientCapabilities";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("ClientCapabilities", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((RealtimeSessionRequest)obj).MouthProvider;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "MouthProvider";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("MouthProvider", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((RealtimeSessionRequest)obj).Language;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "Language";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("Language", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(RealtimeSessionRequest);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((RealtimeSessionRequest)obj).Languages;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "Languages";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(RealtimeSessionRequest).GetProperty("Languages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues14);
      return array;
    }

    // Token: 0x06000110 RID: 272 RVA: 0x00343C14 File Offset: 0x00343C14
    private void RealtimeSessionRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] RealtimeSessionRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string voice = value.Voice;
      if (voice != null)
      {
        writer.WriteString(CoreJsonContext.PropName_voice, voice);
      }
      writer.WriteNumber(CoreJsonContext.PropName_speed, value.Speed);
      RealtimeSessionPromptContext promptContext = value.PromptContext;
      if (promptContext != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_prompt_context);
        this.RealtimeSessionPromptContextSerializeHandler(writer, promptContext);
      }
      IReadOnlyList<string> clientCapabilities = value.ClientCapabilities;
      if (clientCapabilities != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_client_capabilities);
        this.IReadOnlyListStringSerializeHandler(writer, clientCapabilities);
      }
      string mouthProvider = value.MouthProvider;
      if (mouthProvider != null)
      {
        writer.WriteString(CoreJsonContext.PropName_mouth_provider, mouthProvider);
      }
      string language = value.Language;
      if (language != null)
      {
        writer.WriteString(CoreJsonContext.PropName_language, language);
      }
      IReadOnlyList<string> languages = value.Languages;
      if (languages != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_languages);
        this.IReadOnlyListStringSerializeHandler(writer, languages);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000111 RID: 273 RVA: 0x00343CE8 File Offset: 0x00343CE8
    private static JsonParameterInfoValues[] RealtimeSessionRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Voice",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Speed",
          ParameterType = typeof(double),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "PromptContext",
          ParameterType = typeof(RealtimeSessionPromptContext),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ClientCapabilities",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "MouthProvider",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Language",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Languages",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 6,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000025 RID: 37
    // (get) Token: 0x06000112 RID: 274 RVA: 0x00343EB4 File Offset: 0x00343EB4
    [Nullable(0)]
    public JsonTypeInfo<ReferralClaimRequest> ReferralClaimRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ReferralClaimRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._ReferralClaimRequest) == null)
        {
          jsonTypeInfo = (this._ReferralClaimRequest = (JsonTypeInfo<ReferralClaimRequest>)base.Options.GetTypeInfo(typeof(ReferralClaimRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000113 RID: 275 RVA: 0x00343EF0 File Offset: 0x00343EF0
    private JsonTypeInfo<ReferralClaimRequest> Create_ReferralClaimRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<ReferralClaimRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ReferralClaimRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ReferralClaimRequest> jsonObjectInfoValues = new JsonObjectInfoValues<ReferralClaimRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ReferralClaimRequest((string)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ReferralClaimRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<28>__ReferralClaimRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<28>__ReferralClaimRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ReferralClaimRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ReferralClaimRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ReferralClaimRequest>(this.ReferralClaimRequestSerializeHandler);
        JsonObjectInfoValues<ReferralClaimRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ReferralClaimRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000114 RID: 276 RVA: 0x00343FDC File Offset: 0x00343FDC
    private static JsonPropertyInfo[] ReferralClaimRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ReferralClaimRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ReferralClaimRequest)obj).Code;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Code";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ReferralClaimRequest).GetProperty("Code", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ReferralClaimRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ReferralClaimRequest)obj).Source;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Source";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ReferralClaimRequest).GetProperty("Source", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000115 RID: 277 RVA: 0x003441E8 File Offset: 0x003441E8
    private void ReferralClaimRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ReferralClaimRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string code = value.Code;
      if (code != null)
      {
        writer.WriteString(CoreJsonContext.PropName_code, code);
      }
      string source = value.Source;
      if (source != null)
      {
        writer.WriteString(CoreJsonContext.PropName_source, source);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000116 RID: 278 RVA: 0x00344238 File Offset: 0x00344238
    private static JsonParameterInfoValues[] ReferralClaimRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Code",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Source",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000026 RID: 38
    // (get) Token: 0x06000117 RID: 279 RVA: 0x003442CC File Offset: 0x003442CC
    [Nullable(0)]
    public JsonTypeInfo<ReferralCodeRequest> ReferralCodeRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ReferralCodeRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._ReferralCodeRequest) == null)
        {
          jsonTypeInfo = (this._ReferralCodeRequest = (JsonTypeInfo<ReferralCodeRequest>)base.Options.GetTypeInfo(typeof(ReferralCodeRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000118 RID: 280 RVA: 0x00344308 File Offset: 0x00344308
    private JsonTypeInfo<ReferralCodeRequest> Create_ReferralCodeRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<ReferralCodeRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ReferralCodeRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ReferralCodeRequest> jsonObjectInfoValues = new JsonObjectInfoValues<ReferralCodeRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ReferralCodeRequest((string)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ReferralCodeRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<29>__ReferralCodeRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<29>__ReferralCodeRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ReferralCodeRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ReferralCodeRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ReferralCodeRequest>(this.ReferralCodeRequestSerializeHandler);
        JsonObjectInfoValues<ReferralCodeRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ReferralCodeRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000119 RID: 281 RVA: 0x003443F4 File Offset: 0x003443F4
    private static JsonPropertyInfo[] ReferralCodeRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ReferralCodeRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ReferralCodeRequest)obj).Code;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Code";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ReferralCodeRequest).GetProperty("Code", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      return array;
    }

    // Token: 0x0600011A RID: 282 RVA: 0x00344504 File Offset: 0x00344504
    private void ReferralCodeRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ReferralCodeRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string code = value.Code;
      if (code != null)
      {
        writer.WriteString(CoreJsonContext.PropName_code, code);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600011B RID: 283 RVA: 0x00344540 File Offset: 0x00344540
    private static JsonParameterInfoValues[] ReferralCodeRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Code",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000027 RID: 39
    // (get) Token: 0x0600011C RID: 284 RVA: 0x00344594 File Offset: 0x00344594
    [Nullable(0)]
    public JsonTypeInfo<ReferralOutcomeResponse> ReferralOutcomeResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<ReferralOutcomeResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._ReferralOutcomeResponse) == null)
        {
          jsonTypeInfo = (this._ReferralOutcomeResponse = (JsonTypeInfo<ReferralOutcomeResponse>)base.Options.GetTypeInfo(typeof(ReferralOutcomeResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600011D RID: 285 RVA: 0x003445D0 File Offset: 0x003445D0
    private JsonTypeInfo<ReferralOutcomeResponse> Create_ReferralOutcomeResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<ReferralOutcomeResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<ReferralOutcomeResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<ReferralOutcomeResponse> jsonObjectInfoValues = new JsonObjectInfoValues<ReferralOutcomeResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new ReferralOutcomeResponse((string)args[0], (string)args[1], (double?)args[2], (string)args[3], (string)args[4], (string)args[5]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.ReferralOutcomeResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<30>__ReferralOutcomeResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<30>__ReferralOutcomeResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.ReferralOutcomeResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(double?),
          typeof(string),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, ReferralOutcomeResponse>(this.ReferralOutcomeResponseSerializeHandler);
        JsonObjectInfoValues<ReferralOutcomeResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<ReferralOutcomeResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600011E RID: 286 RVA: 0x003446BC File Offset: 0x003446BC
    private static JsonPropertyInfo[] ReferralOutcomeResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[6];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((ReferralOutcomeResponse)obj).Outcome;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Outcome";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("Outcome", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((ReferralOutcomeResponse)obj).Message;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Message";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("Message", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<double?>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((ReferralOutcomeResponse)obj).DiscountPercent;
      jsonPropertyInfoValues5.Setter = delegate(object obj, double? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "DiscountPercent";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("DiscountPercent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(double?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<double?> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<double?>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((ReferralOutcomeResponse)obj).Code;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Code";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("Code", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((ReferralOutcomeResponse)obj).ShareUrl;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "ShareUrl";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("ShareUrl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(ReferralOutcomeResponse);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((ReferralOutcomeResponse)obj).ShareDisplayText;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ShareDisplayText";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(ReferralOutcomeResponse).GetProperty("ShareDisplayText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      return array;
    }

    // Token: 0x0600011F RID: 287 RVA: 0x00344C68 File Offset: 0x00344C68
    private void ReferralOutcomeResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] ReferralOutcomeResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string outcome = value.Outcome;
      if (outcome != null)
      {
        writer.WriteString(CoreJsonContext.PropName_outcome, outcome);
      }
      string message = value.Message;
      if (message != null)
      {
        writer.WriteString(CoreJsonContext.PropName_message, message);
      }
      double? discountPercent = value.DiscountPercent;
      if (discountPercent != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_discount_percent);
        JsonSerializer.Serialize<double?>(writer, discountPercent, this.NullableDouble);
      }
      string code = value.Code;
      if (code != null)
      {
        writer.WriteString(CoreJsonContext.PropName_code, code);
      }
      string shareUrl = value.ShareUrl;
      if (shareUrl != null)
      {
        writer.WriteString(CoreJsonContext.PropName_share_url, shareUrl);
      }
      string shareDisplayText = value.ShareDisplayText;
      if (shareDisplayText != null)
      {
        writer.WriteString(CoreJsonContext.PropName_share_display_text, shareDisplayText);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000120 RID: 288 RVA: 0x00344D28 File Offset: 0x00344D28
    private static JsonParameterInfoValues[] ReferralOutcomeResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Outcome",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Message",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "DiscountPercent",
          ParameterType = typeof(double?),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Code",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ShareUrl",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ShareDisplayText",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000028 RID: 40
    // (get) Token: 0x06000121 RID: 289 RVA: 0x00344EB8 File Offset: 0x00344EB8
    [Nullable(0)]
    public JsonTypeInfo<RegisteredDevice> RegisteredDevice
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<RegisteredDevice> jsonTypeInfo;
        if ((jsonTypeInfo = this._RegisteredDevice) == null)
        {
          jsonTypeInfo = (this._RegisteredDevice = (JsonTypeInfo<RegisteredDevice>)base.Options.GetTypeInfo(typeof(RegisteredDevice)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000122 RID: 290 RVA: 0x00344EF4 File Offset: 0x00344EF4
    private JsonTypeInfo<RegisteredDevice> Create_RegisteredDevice(JsonSerializerOptions options)
    {
      JsonTypeInfo<RegisteredDevice> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<RegisteredDevice>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<RegisteredDevice> jsonObjectInfoValues = new JsonObjectInfoValues<RegisteredDevice>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new RegisteredDevice((string)args[0], (bool)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5], (string)args[6], (string)args[7], (IReadOnlyList<string>)args[8], (string)args[9], (string)args[10], (string)args[11], (int?)args[12]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.RegisteredDevicePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<31>__RegisteredDeviceCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<31>__RegisteredDeviceCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.RegisteredDeviceCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(RegisteredDevice).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(bool),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<string>),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(int?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, RegisteredDevice>(this.RegisteredDeviceSerializeHandler);
        JsonObjectInfoValues<RegisteredDevice> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<RegisteredDevice>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000123 RID: 291 RVA: 0x00344FE0 File Offset: 0x00344FE0
    private static JsonPropertyInfo[] RegisteredDevicePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[13];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((RegisteredDevice)obj).Id;
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
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((RegisteredDevice)obj).IsThisDevice;
      jsonPropertyInfoValues3.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "IsThisDevice";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("IsThisDevice", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((RegisteredDevice)obj).Platform;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Platform";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("Platform", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((RegisteredDevice)obj).DisplayName;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "DisplayName";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("DisplayName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((RegisteredDevice)obj).Model;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Model";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("Model", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((RegisteredDevice)obj).OsVersion;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "OsVersion";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("OsVersion", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((RegisteredDevice)obj).AppVersion;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "AppVersion";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("AppVersion", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((RegisteredDevice)obj).AppBuild;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "AppBuild";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("AppBuild", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues16);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues17 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues17.IsProperty = true;
      jsonPropertyInfoValues17.IsPublic = true;
      jsonPropertyInfoValues17.IsVirtual = false;
      jsonPropertyInfoValues17.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues17.Converter = null;
      jsonPropertyInfoValues17.Getter = (object obj) => ((RegisteredDevice)obj).Capabilities;
      jsonPropertyInfoValues17.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues17.IgnoreCondition = null;
      jsonPropertyInfoValues17.HasJsonInclude = false;
      jsonPropertyInfoValues17.IsExtensionData = false;
      jsonPropertyInfoValues17.NumberHandling = null;
      jsonPropertyInfoValues17.PropertyName = "Capabilities";
      jsonPropertyInfoValues17.JsonPropertyName = null;
      jsonPropertyInfoValues17.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("Capabilities", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues18 = jsonPropertyInfoValues17;
      array[8] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues18);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues19 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues19.IsProperty = true;
      jsonPropertyInfoValues19.IsPublic = true;
      jsonPropertyInfoValues19.IsVirtual = false;
      jsonPropertyInfoValues19.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues19.Converter = null;
      jsonPropertyInfoValues19.Getter = (object obj) => ((RegisteredDevice)obj).FirstSeenAt;
      jsonPropertyInfoValues19.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues19.IgnoreCondition = null;
      jsonPropertyInfoValues19.HasJsonInclude = false;
      jsonPropertyInfoValues19.IsExtensionData = false;
      jsonPropertyInfoValues19.NumberHandling = null;
      jsonPropertyInfoValues19.PropertyName = "FirstSeenAt";
      jsonPropertyInfoValues19.JsonPropertyName = null;
      jsonPropertyInfoValues19.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("FirstSeenAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues20 = jsonPropertyInfoValues19;
      array[9] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues20);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues21 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues21.IsProperty = true;
      jsonPropertyInfoValues21.IsPublic = true;
      jsonPropertyInfoValues21.IsVirtual = false;
      jsonPropertyInfoValues21.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues21.Converter = null;
      jsonPropertyInfoValues21.Getter = (object obj) => ((RegisteredDevice)obj).LastSeenAt;
      jsonPropertyInfoValues21.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues21.IgnoreCondition = null;
      jsonPropertyInfoValues21.HasJsonInclude = false;
      jsonPropertyInfoValues21.IsExtensionData = false;
      jsonPropertyInfoValues21.NumberHandling = null;
      jsonPropertyInfoValues21.PropertyName = "LastSeenAt";
      jsonPropertyInfoValues21.JsonPropertyName = null;
      jsonPropertyInfoValues21.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("LastSeenAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues22 = jsonPropertyInfoValues21;
      array[10] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues22);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues23 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues23.IsProperty = true;
      jsonPropertyInfoValues23.IsPublic = true;
      jsonPropertyInfoValues23.IsVirtual = false;
      jsonPropertyInfoValues23.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues23.Converter = null;
      jsonPropertyInfoValues23.Getter = (object obj) => ((RegisteredDevice)obj).LastActiveAt;
      jsonPropertyInfoValues23.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues23.IgnoreCondition = null;
      jsonPropertyInfoValues23.HasJsonInclude = false;
      jsonPropertyInfoValues23.IsExtensionData = false;
      jsonPropertyInfoValues23.NumberHandling = null;
      jsonPropertyInfoValues23.PropertyName = "LastActiveAt";
      jsonPropertyInfoValues23.JsonPropertyName = null;
      jsonPropertyInfoValues23.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("LastActiveAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues24 = jsonPropertyInfoValues23;
      array[11] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues24);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues25 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues25.IsProperty = true;
      jsonPropertyInfoValues25.IsPublic = true;
      jsonPropertyInfoValues25.IsVirtual = false;
      jsonPropertyInfoValues25.DeclaringType = typeof(RegisteredDevice);
      jsonPropertyInfoValues25.Converter = null;
      jsonPropertyInfoValues25.Getter = (object obj) => ((RegisteredDevice)obj).MessagesLast14Days;
      jsonPropertyInfoValues25.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues25.IgnoreCondition = null;
      jsonPropertyInfoValues25.HasJsonInclude = false;
      jsonPropertyInfoValues25.IsExtensionData = false;
      jsonPropertyInfoValues25.NumberHandling = null;
      jsonPropertyInfoValues25.PropertyName = "MessagesLast14Days";
      jsonPropertyInfoValues25.JsonPropertyName = null;
      jsonPropertyInfoValues25.AttributeProviderFactory = () => typeof(RegisteredDevice).GetProperty("MessagesLast14Days", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues26 = jsonPropertyInfoValues25;
      array[12] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues26);
      return array;
    }

    // Token: 0x06000124 RID: 292 RVA: 0x00345BFC File Offset: 0x00345BFC
    private void RegisteredDeviceSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] RegisteredDevice value)
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
        writer.WriteString(CoreJsonContext.PropName_id, id);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_is_this_device, value.IsThisDevice);
      string platform = value.Platform;
      if (platform != null)
      {
        writer.WriteString(CoreJsonContext.PropName_platform, platform);
      }
      string displayName = value.DisplayName;
      if (displayName != null)
      {
        writer.WriteString(CoreJsonContext.PropName_display_name, displayName);
      }
      string model = value.Model;
      if (model != null)
      {
        writer.WriteString(CoreJsonContext.PropName_model, model);
      }
      string osVersion = value.OsVersion;
      if (osVersion != null)
      {
        writer.WriteString(CoreJsonContext.PropName_os_version, osVersion);
      }
      string appVersion = value.AppVersion;
      if (appVersion != null)
      {
        writer.WriteString(CoreJsonContext.PropName_app_version, appVersion);
      }
      string appBuild = value.AppBuild;
      if (appBuild != null)
      {
        writer.WriteString(CoreJsonContext.PropName_app_build, appBuild);
      }
      IReadOnlyList<string> capabilities = value.Capabilities;
      if (capabilities != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_capabilities);
        this.IReadOnlyListStringSerializeHandler(writer, capabilities);
      }
      string firstSeenAt = value.FirstSeenAt;
      if (firstSeenAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_first_seen_at, firstSeenAt);
      }
      string lastSeenAt = value.LastSeenAt;
      if (lastSeenAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_last_seen_at, lastSeenAt);
      }
      string lastActiveAt = value.LastActiveAt;
      if (lastActiveAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_last_active_at, lastActiveAt);
      }
      int? messagesLast14Days = value.MessagesLast14Days;
      if (messagesLast14Days != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_messages_last14_days);
        JsonSerializer.Serialize<int?>(writer, messagesLast14Days, this.NullableInt32);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000125 RID: 293 RVA: 0x00345D68 File Offset: 0x00345D68
    private static JsonParameterInfoValues[] RegisteredDeviceCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Id",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "IsThisDevice",
          ParameterType = typeof(bool),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Platform",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "DisplayName",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Model",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "OsVersion",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "AppVersion",
          ParameterType = typeof(string),
          Position = 6,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "AppBuild",
          ParameterType = typeof(string),
          Position = 7,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Capabilities",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 8,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "FirstSeenAt",
          ParameterType = typeof(string),
          Position = 9,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "LastSeenAt",
          ParameterType = typeof(string),
          Position = 10,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "LastActiveAt",
          ParameterType = typeof(string),
          Position = 11,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "MessagesLast14Days",
          ParameterType = typeof(int?),
          Position = 12,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000029 RID: 41
    // (get) Token: 0x06000126 RID: 294 RVA: 0x003460B8 File Offset: 0x003460B8
    [Nullable(0)]
    public JsonTypeInfo<SaveMemoryRequest> SaveMemoryRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<SaveMemoryRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._SaveMemoryRequest) == null)
        {
          jsonTypeInfo = (this._SaveMemoryRequest = (JsonTypeInfo<SaveMemoryRequest>)base.Options.GetTypeInfo(typeof(SaveMemoryRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000127 RID: 295 RVA: 0x003460F4 File Offset: 0x003460F4
    private JsonTypeInfo<SaveMemoryRequest> Create_SaveMemoryRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<SaveMemoryRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<SaveMemoryRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<SaveMemoryRequest> jsonObjectInfoValues = new JsonObjectInfoValues<SaveMemoryRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new SaveMemoryRequest((string)args[0]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.SaveMemoryRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<32>__SaveMemoryRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<32>__SaveMemoryRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.SaveMemoryRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(SaveMemoryRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, SaveMemoryRequest>(this.SaveMemoryRequestSerializeHandler);
        JsonObjectInfoValues<SaveMemoryRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<SaveMemoryRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000128 RID: 296 RVA: 0x003461E0 File Offset: 0x003461E0
    private static JsonPropertyInfo[] SaveMemoryRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[1];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(SaveMemoryRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((SaveMemoryRequest)obj).Memory;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Memory";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(SaveMemoryRequest).GetProperty("Memory", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000129 RID: 297 RVA: 0x003462F0 File Offset: 0x003462F0
    private void SaveMemoryRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] SaveMemoryRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string memory = value.Memory;
      if (memory != null)
      {
        writer.WriteString(CoreJsonContext.PropName_memory, memory);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600012A RID: 298 RVA: 0x0034632C File Offset: 0x0034632C
    private static JsonParameterInfoValues[] SaveMemoryRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Memory",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700002A RID: 42
    // (get) Token: 0x0600012B RID: 299 RVA: 0x00346380 File Offset: 0x00346380
    [Nullable(0)]
    public JsonTypeInfo<SaveMemoryResponse> SaveMemoryResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<SaveMemoryResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._SaveMemoryResponse) == null)
        {
          jsonTypeInfo = (this._SaveMemoryResponse = (JsonTypeInfo<SaveMemoryResponse>)base.Options.GetTypeInfo(typeof(SaveMemoryResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600012C RID: 300 RVA: 0x003463BC File Offset: 0x003463BC
    private JsonTypeInfo<SaveMemoryResponse> Create_SaveMemoryResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<SaveMemoryResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<SaveMemoryResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<SaveMemoryResponse> jsonObjectInfoValues = new JsonObjectInfoValues<SaveMemoryResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new SaveMemoryResponse((bool)args[0], (string)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.SaveMemoryResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<33>__SaveMemoryResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<33>__SaveMemoryResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.SaveMemoryResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(SaveMemoryResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(bool),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, SaveMemoryResponse>(this.SaveMemoryResponseSerializeHandler);
        JsonObjectInfoValues<SaveMemoryResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<SaveMemoryResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600012D RID: 301 RVA: 0x003464A8 File Offset: 0x003464A8
    private static JsonPropertyInfo[] SaveMemoryResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(SaveMemoryResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((SaveMemoryResponse)obj).Saved;
      jsonPropertyInfoValues.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Saved";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(SaveMemoryResponse).GetProperty("Saved", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(SaveMemoryResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((SaveMemoryResponse)obj).Error;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Error";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(SaveMemoryResponse).GetProperty("Error", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x0600012E RID: 302 RVA: 0x00346690 File Offset: 0x00346690
    private void SaveMemoryResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] SaveMemoryResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WriteBoolean(CoreJsonContext.PropName_saved, value.Saved);
      string error = value.Error;
      if (error != null)
      {
        writer.WriteString(CoreJsonContext.PropName_error, error);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600012F RID: 303 RVA: 0x003466DC File Offset: 0x003466DC
    private static JsonParameterInfoValues[] SaveMemoryResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Saved",
          ParameterType = typeof(bool),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Error",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700002B RID: 43
    // (get) Token: 0x06000130 RID: 304 RVA: 0x00346770 File Offset: 0x00346770
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanClicky> StarterPlanClicky
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanClicky> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanClicky) == null)
        {
          jsonTypeInfo = (this._StarterPlanClicky = (JsonTypeInfo<StarterPlanClicky>)base.Options.GetTypeInfo(typeof(StarterPlanClicky)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000131 RID: 305 RVA: 0x003467AC File Offset: 0x003467AC
    private JsonTypeInfo<StarterPlanClicky> Create_StarterPlanClicky(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanClicky> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanClicky>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanClicky> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanClicky>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanClicky((string)args[0], (string)args[1], (string)args[2], (string)args[3], (IReadOnlyList<string>)args[4], (string)args[5], (IReadOnlyList<string>)args[6], (StarterPlanClickyRoutine)args[7]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanClickyPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<34>__StarterPlanClickyCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<34>__StarterPlanClickyCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanClickyCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanClicky).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<string>),
          typeof(string),
          typeof(IReadOnlyList<string>),
          typeof(StarterPlanClickyRoutine)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanClicky>(this.StarterPlanClickySerializeHandler);
        JsonObjectInfoValues<StarterPlanClicky> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanClicky>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000132 RID: 306 RVA: 0x00346898 File Offset: 0x00346898
    private static JsonPropertyInfo[] StarterPlanClickyPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[8];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanClicky)obj).Key;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Key";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("Key", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanClicky)obj).Name;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Name";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanClicky)obj).Role;
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
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("Role", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StarterPlanClicky)obj).Description;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Description";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((StarterPlanClicky)obj).IntroMessages;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "IntroMessages";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("IntroMessages", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((StarterPlanClicky)obj).RevealLine;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "RevealLine";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("RevealLine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((StarterPlanClicky)obj).FirstAsks;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "FirstAsks";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("FirstAsks", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<StarterPlanClickyRoutine> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<StarterPlanClickyRoutine>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(StarterPlanClicky);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((StarterPlanClicky)obj).Routine;
      jsonPropertyInfoValues15.Setter = delegate(object obj, [Nullable(2)] StarterPlanClickyRoutine value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "Routine";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(StarterPlanClicky).GetProperty("Routine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(StarterPlanClickyRoutine), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<StarterPlanClickyRoutine> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<StarterPlanClickyRoutine>(options, jsonPropertyInfoValues16);
      return array;
    }

    // Token: 0x06000133 RID: 307 RVA: 0x00347054 File Offset: 0x00347054
    private void StarterPlanClickySerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanClicky value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string key = value.Key;
      if (key != null)
      {
        writer.WriteString(CoreJsonContext.PropName_key, key);
      }
      string name = value.Name;
      if (name != null)
      {
        writer.WriteString(CoreJsonContext.PropName_name, name);
      }
      string role = value.Role;
      if (role != null)
      {
        writer.WriteString(CoreJsonContext.PropName_role, role);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(CoreJsonContext.PropName_description, description);
      }
      IReadOnlyList<string> introMessages = value.IntroMessages;
      if (introMessages != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_intro_messages);
        this.IReadOnlyListStringSerializeHandler(writer, introMessages);
      }
      string revealLine = value.RevealLine;
      if (revealLine != null)
      {
        writer.WriteString(CoreJsonContext.PropName_reveal_line, revealLine);
      }
      IReadOnlyList<string> firstAsks = value.FirstAsks;
      if (firstAsks != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_first_asks);
        this.IReadOnlyListStringSerializeHandler(writer, firstAsks);
      }
      StarterPlanClickyRoutine routine = value.Routine;
      if (routine != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_routine);
        this.StarterPlanClickyRoutineSerializeHandler(writer, routine);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000134 RID: 308 RVA: 0x00347148 File Offset: 0x00347148
    private static JsonParameterInfoValues[] StarterPlanClickyCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Key",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Name",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Role",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Description",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "IntroMessages",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "RevealLine",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "FirstAsks",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 6,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Routine",
          ParameterType = typeof(StarterPlanClickyRoutine),
          Position = 7,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700002C RID: 44
    // (get) Token: 0x06000135 RID: 309 RVA: 0x00347354 File Offset: 0x00347354
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanClickyInput> StarterPlanClickyInput
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanClickyInput> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanClickyInput) == null)
        {
          jsonTypeInfo = (this._StarterPlanClickyInput = (JsonTypeInfo<StarterPlanClickyInput>)base.Options.GetTypeInfo(typeof(StarterPlanClickyInput)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000136 RID: 310 RVA: 0x00347390 File Offset: 0x00347390
    private JsonTypeInfo<StarterPlanClickyInput> Create_StarterPlanClickyInput(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanClickyInput> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanClickyInput>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanClickyInput> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanClickyInput>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanClickyInput((string)args[0], (string)args[1], (string)args[2], (string)args[3], (IReadOnlyList<StarterPlanRoutine>)args[4], (IReadOnlyList<string>)args[5]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanClickyInputPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<35>__StarterPlanClickyInputCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<35>__StarterPlanClickyInputCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanClickyInputCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<StarterPlanRoutine>),
          typeof(IReadOnlyList<string>)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanClickyInput>(this.StarterPlanClickyInputSerializeHandler);
        JsonObjectInfoValues<StarterPlanClickyInput> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanClickyInput>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000137 RID: 311 RVA: 0x0034747C File Offset: 0x0034747C
    private static JsonPropertyInfo[] StarterPlanClickyInputPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[6];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanClickyInput)obj).Slug;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Slug";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("Slug", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanClickyInput)obj).Name;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Name";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanClickyInput)obj).Role;
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
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("Role", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StarterPlanClickyInput)obj).Description;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Description";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanRoutine>> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<IReadOnlyList<StarterPlanRoutine>>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((StarterPlanClickyInput)obj).Routines;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanRoutine> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Routines";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("Routines", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<StarterPlanRoutine>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanRoutine>> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<StarterPlanRoutine>>(options, jsonPropertyInfoValues10);
      array[4].IsGetNullable = false;
      array[4].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(StarterPlanClickyInput);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((StarterPlanClickyInput)obj).ThreadIds;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ThreadIds";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(StarterPlanClickyInput).GetProperty("ThreadIds", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues12);
      array[5].IsGetNullable = false;
      array[5].IsSetNullable = false;
      return array;
    }

    // Token: 0x06000138 RID: 312 RVA: 0x00347A84 File Offset: 0x00347A84
    private void StarterPlanClickyInputSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanClickyInput value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string slug = value.Slug;
      if (slug != null)
      {
        writer.WriteString(CoreJsonContext.PropName_slug, slug);
      }
      string name = value.Name;
      if (name != null)
      {
        writer.WriteString(CoreJsonContext.PropName_name, name);
      }
      string role = value.Role;
      if (role != null)
      {
        writer.WriteString(CoreJsonContext.PropName_role, role);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(CoreJsonContext.PropName_description, description);
      }
      IReadOnlyList<StarterPlanRoutine> routines = value.Routines;
      if (routines != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_routines);
        this.IReadOnlyListStarterPlanRoutineSerializeHandler(writer, routines);
      }
      IReadOnlyList<string> threadIds = value.ThreadIds;
      if (threadIds != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_thread_ids);
        this.IReadOnlyListStringSerializeHandler(writer, threadIds);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000139 RID: 313 RVA: 0x00347B40 File Offset: 0x00347B40
    private static JsonParameterInfoValues[] StarterPlanClickyInputCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Slug",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Name",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Role",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Description",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Routines",
          ParameterType = typeof(IReadOnlyList<StarterPlanRoutine>),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ThreadIds",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 5,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x1700002D RID: 45
    // (get) Token: 0x0600013A RID: 314 RVA: 0x00347CD0 File Offset: 0x00347CD0
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanClickyRoutine> StarterPlanClickyRoutine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanClickyRoutine> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanClickyRoutine) == null)
        {
          jsonTypeInfo = (this._StarterPlanClickyRoutine = (JsonTypeInfo<StarterPlanClickyRoutine>)base.Options.GetTypeInfo(typeof(StarterPlanClickyRoutine)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600013B RID: 315 RVA: 0x00347D0C File Offset: 0x00347D0C
    private JsonTypeInfo<StarterPlanClickyRoutine> Create_StarterPlanClickyRoutine(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanClickyRoutine> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanClickyRoutine>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanClickyRoutine> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanClickyRoutine>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanClickyRoutine((string)args[0], (string)args[1], (int?)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanClickyRoutinePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<36>__StarterPlanClickyRoutineCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<36>__StarterPlanClickyRoutineCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanClickyRoutineCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanClickyRoutine).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(int?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanClickyRoutine>(this.StarterPlanClickyRoutineSerializeHandler);
        JsonObjectInfoValues<StarterPlanClickyRoutine> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanClickyRoutine>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600013C RID: 316 RVA: 0x00347DF8 File Offset: 0x00347DF8
    private static JsonPropertyInfo[] StarterPlanClickyRoutinePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanClickyRoutine);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanClickyRoutine)obj).Title;
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
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanClickyRoutine).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanClickyRoutine);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanClickyRoutine)obj).TaskPrompt;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "TaskPrompt";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanClickyRoutine).GetProperty("TaskPrompt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanClickyRoutine);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanClickyRoutine)obj).IntervalMinutes;
      jsonPropertyInfoValues5.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "IntervalMinutes";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanClickyRoutine).GetProperty("IntervalMinutes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x0600013D RID: 317 RVA: 0x003480F0 File Offset: 0x003480F0
    private void StarterPlanClickyRoutineSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanClickyRoutine value)
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
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      string taskPrompt = value.TaskPrompt;
      if (taskPrompt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_task_prompt, taskPrompt);
      }
      int? intervalMinutes = value.IntervalMinutes;
      if (intervalMinutes != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_interval_minutes);
        JsonSerializer.Serialize<int?>(writer, intervalMinutes, this.NullableInt32);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600013E RID: 318 RVA: 0x00348168 File Offset: 0x00348168
    private static JsonParameterInfoValues[] StarterPlanClickyRoutineCtorParamInit()
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
          Name = "TaskPrompt",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "IntervalMinutes",
          ParameterType = typeof(int?),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x1700002E RID: 46
    // (get) Token: 0x0600013F RID: 319 RVA: 0x00348238 File Offset: 0x00348238
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanRequest> StarterPlanRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanRequest) == null)
        {
          jsonTypeInfo = (this._StarterPlanRequest = (JsonTypeInfo<StarterPlanRequest>)base.Options.GetTypeInfo(typeof(StarterPlanRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000140 RID: 320 RVA: 0x00348274 File Offset: 0x00348274
    private JsonTypeInfo<StarterPlanRequest> Create_StarterPlanRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanRequest> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanRequest((IReadOnlyList<InterviewLine>)args[0], (IReadOnlyList<string>)args[1], (IReadOnlyList<string>)args[2], (IReadOnlyList<StarterPlanClickyInput>)args[3], (string)args[4], (bool?)args[5])
        {
          Jobs = (bool)args[6]
        };
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<37>__StarterPlanRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<37>__StarterPlanRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(IReadOnlyList<InterviewLine>),
          typeof(IReadOnlyList<string>),
          typeof(IReadOnlyList<string>),
          typeof(IReadOnlyList<StarterPlanClickyInput>),
          typeof(string),
          typeof(bool?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanRequest>(this.StarterPlanRequestSerializeHandler);
        JsonObjectInfoValues<StarterPlanRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000141 RID: 321 RVA: 0x00348360 File Offset: 0x00348360
    private static JsonPropertyInfo[] StarterPlanRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[7];
      JsonPropertyInfoValues<IReadOnlyList<InterviewLine>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<InterviewLine>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanRequest)obj).Interview;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<InterviewLine> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Interview";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("Interview", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<InterviewLine>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<InterviewLine>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<InterviewLine>>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanRequest)obj).ConnectedIntegrations;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ConnectedIntegrations";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("ConnectedIntegrations", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanRequest)obj).AvailableIntegrations;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "AvailableIntegrations";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("AvailableIntegrations", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanClickyInput>> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<IReadOnlyList<StarterPlanClickyInput>>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StarterPlanRequest)obj).Clickys;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClickyInput> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Clickys";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("Clickys", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<StarterPlanClickyInput>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanClickyInput>> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<StarterPlanClickyInput>>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((StarterPlanRequest)obj).FirstAsksForClicky;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "FirstAsksForClicky";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("FirstAsksForClicky", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((StarterPlanRequest)obj).InterviewSaved;
      jsonPropertyInfoValues11.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "InterviewSaved";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("InterviewSaved", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<bool>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(StarterPlanRequest);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((StarterPlanRequest)obj).Jobs;
      jsonPropertyInfoValues13.Setter = delegate(object obj, bool value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "Jobs";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(StarterPlanRequest).GetProperty("Jobs", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<bool>(options, jsonPropertyInfoValues14);
      return array;
    }

    // Token: 0x06000142 RID: 322 RVA: 0x00348A1C File Offset: 0x00348A1C
    private void StarterPlanRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<InterviewLine> interview = value.Interview;
      if (interview != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_interview);
        this.IReadOnlyListInterviewLineSerializeHandler(writer, interview);
      }
      IReadOnlyList<string> connectedIntegrations = value.ConnectedIntegrations;
      if (connectedIntegrations != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_connected_integrations);
        this.IReadOnlyListStringSerializeHandler(writer, connectedIntegrations);
      }
      IReadOnlyList<string> availableIntegrations = value.AvailableIntegrations;
      if (availableIntegrations != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_available_integrations);
        this.IReadOnlyListStringSerializeHandler(writer, availableIntegrations);
      }
      IReadOnlyList<StarterPlanClickyInput> clickys = value.Clickys;
      if (clickys != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_clickys);
        this.IReadOnlyListStarterPlanClickyInputSerializeHandler(writer, clickys);
      }
      string firstAsksForClicky = value.FirstAsksForClicky;
      if (firstAsksForClicky != null)
      {
        writer.WriteString(CoreJsonContext.PropName_first_asks_for_clicky, firstAsksForClicky);
      }
      bool? interviewSaved = value.InterviewSaved;
      if (interviewSaved != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_interview_saved);
        JsonSerializer.Serialize<bool?>(writer, interviewSaved, this.NullableBoolean);
      }
      writer.WriteBoolean(CoreJsonContext.PropName_jobs, value.Jobs);
      writer.WriteEndObject();
    }

    // Token: 0x06000143 RID: 323 RVA: 0x00348B08 File Offset: 0x00348B08
    private static JsonParameterInfoValues[] StarterPlanRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Interview",
          ParameterType = typeof(IReadOnlyList<InterviewLine>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "ConnectedIntegrations",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "AvailableIntegrations",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Clickys",
          ParameterType = typeof(IReadOnlyList<StarterPlanClickyInput>),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "FirstAsksForClicky",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "InterviewSaved",
          ParameterType = typeof(bool?),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Jobs",
          ParameterType = typeof(bool),
          Position = 6,
          IsNullable = false,
          IsMemberInitializer = true
        }
      };
    }

    // Token: 0x1700002F RID: 47
    // (get) Token: 0x06000144 RID: 324 RVA: 0x00348CD0 File Offset: 0x00348CD0
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanResponse> StarterPlanResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanResponse) == null)
        {
          jsonTypeInfo = (this._StarterPlanResponse = (JsonTypeInfo<StarterPlanResponse>)base.Options.GetTypeInfo(typeof(StarterPlanResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000145 RID: 325 RVA: 0x00348D0C File Offset: 0x00348D0C
    private JsonTypeInfo<StarterPlanResponse> Create_StarterPlanResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanResponse> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanResponse((IReadOnlyList<StarterPlanClicky>)args[0], (IReadOnlyList<StarterPlanSuggestion>)args[1], (string)args[2], (string)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<38>__StarterPlanResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<38>__StarterPlanResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(IReadOnlyList<StarterPlanClicky>),
          typeof(IReadOnlyList<StarterPlanSuggestion>),
          typeof(string),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanResponse>(this.StarterPlanResponseSerializeHandler);
        JsonObjectInfoValues<StarterPlanResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000146 RID: 326 RVA: 0x00348DF8 File Offset: 0x00348DF8
    private static JsonPropertyInfo[] StarterPlanResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanClicky>> jsonPropertyInfoValues = new JsonPropertyInfoValues<IReadOnlyList<StarterPlanClicky>>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanResponse)obj).Clickys;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClicky> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Clickys";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanResponse).GetProperty("Clickys", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<StarterPlanClicky>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanClicky>> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<StarterPlanClicky>>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanSuggestion>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<IReadOnlyList<StarterPlanSuggestion>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanResponse)obj).SuggestedAgents;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanSuggestion> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "SuggestedAgents";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanResponse).GetProperty("SuggestedAgents", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<StarterPlanSuggestion>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<StarterPlanSuggestion>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<StarterPlanSuggestion>>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanResponse);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanResponse)obj).AgentFamiliarity;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "AgentFamiliarity";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanResponse).GetProperty("AgentFamiliarity", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StarterPlanResponse);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StarterPlanResponse)obj).GoalLine;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "GoalLine";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StarterPlanResponse).GetProperty("GoalLine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      return array;
    }

    // Token: 0x06000147 RID: 327 RVA: 0x003491B8 File Offset: 0x003491B8
    private void StarterPlanResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      IReadOnlyList<StarterPlanClicky> clickys = value.Clickys;
      if (clickys != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_clickys);
        this.IReadOnlyListStarterPlanClickySerializeHandler(writer, clickys);
      }
      IReadOnlyList<StarterPlanSuggestion> suggestedAgents = value.SuggestedAgents;
      if (suggestedAgents != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_suggested_agents);
        this.IReadOnlyListStarterPlanSuggestionSerializeHandler(writer, suggestedAgents);
      }
      string agentFamiliarity = value.AgentFamiliarity;
      if (agentFamiliarity != null)
      {
        writer.WriteString(CoreJsonContext.PropName_agent_familiarity, agentFamiliarity);
      }
      string goalLine = value.GoalLine;
      if (goalLine != null)
      {
        writer.WriteString(CoreJsonContext.PropName_goal_line, goalLine);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000148 RID: 328 RVA: 0x00349244 File Offset: 0x00349244
    private static JsonParameterInfoValues[] StarterPlanResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Clickys",
          ParameterType = typeof(IReadOnlyList<StarterPlanClicky>),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "SuggestedAgents",
          ParameterType = typeof(IReadOnlyList<StarterPlanSuggestion>),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "AgentFamiliarity",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "GoalLine",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000030 RID: 48
    // (get) Token: 0x06000149 RID: 329 RVA: 0x00349354 File Offset: 0x00349354
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanRoutine> StarterPlanRoutine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanRoutine> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanRoutine) == null)
        {
          jsonTypeInfo = (this._StarterPlanRoutine = (JsonTypeInfo<StarterPlanRoutine>)base.Options.GetTypeInfo(typeof(StarterPlanRoutine)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600014A RID: 330 RVA: 0x00349390 File Offset: 0x00349390
    private JsonTypeInfo<StarterPlanRoutine> Create_StarterPlanRoutine(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanRoutine> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanRoutine>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanRoutine> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanRoutine>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanRoutine((string)args[0], (int)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanRoutinePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<39>__StarterPlanRoutineCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<39>__StarterPlanRoutineCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanRoutineCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanRoutine).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(int)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanRoutine>(this.StarterPlanRoutineSerializeHandler);
        JsonObjectInfoValues<StarterPlanRoutine> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanRoutine>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600014B RID: 331 RVA: 0x0034947C File Offset: 0x0034947C
    private static JsonPropertyInfo[] StarterPlanRoutinePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanRoutine);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanRoutine)obj).Title;
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
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanRoutine).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<int> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<int>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanRoutine);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanRoutine)obj).EveryMinutes;
      jsonPropertyInfoValues3.Setter = delegate(object obj, int value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "EveryMinutes";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanRoutine).GetProperty("EveryMinutes", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<int>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x0600014C RID: 332 RVA: 0x00349674 File Offset: 0x00349674
    private void StarterPlanRoutineSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanRoutine value)
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
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      writer.WriteNumber(CoreJsonContext.PropName_every_minutes, value.EveryMinutes);
      writer.WriteEndObject();
    }

    // Token: 0x0600014D RID: 333 RVA: 0x003496C0 File Offset: 0x003496C0
    private static JsonParameterInfoValues[] StarterPlanRoutineCtorParamInit()
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
          Name = "EveryMinutes",
          ParameterType = typeof(int),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000031 RID: 49
    // (get) Token: 0x0600014E RID: 334 RVA: 0x00349754 File Offset: 0x00349754
    [Nullable(0)]
    public JsonTypeInfo<StarterPlanSuggestion> StarterPlanSuggestion
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<StarterPlanSuggestion> jsonTypeInfo;
        if ((jsonTypeInfo = this._StarterPlanSuggestion) == null)
        {
          jsonTypeInfo = (this._StarterPlanSuggestion = (JsonTypeInfo<StarterPlanSuggestion>)base.Options.GetTypeInfo(typeof(StarterPlanSuggestion)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600014F RID: 335 RVA: 0x00349790 File Offset: 0x00349790
    private JsonTypeInfo<StarterPlanSuggestion> Create_StarterPlanSuggestion(JsonSerializerOptions options)
    {
      JsonTypeInfo<StarterPlanSuggestion> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<StarterPlanSuggestion>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<StarterPlanSuggestion> jsonObjectInfoValues = new JsonObjectInfoValues<StarterPlanSuggestion>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new StarterPlanSuggestion((string)args[0], (string)args[1], (string)args[2], (string)args[3], (string)args[4], (string)args[5], (StarterPlanRoutine)args[6], (int?)args[7]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.StarterPlanSuggestionPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<40>__StarterPlanSuggestionCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<40>__StarterPlanSuggestionCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.StarterPlanSuggestionCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(StarterPlanRoutine),
          typeof(int?)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, StarterPlanSuggestion>(this.StarterPlanSuggestionSerializeHandler);
        JsonObjectInfoValues<StarterPlanSuggestion> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<StarterPlanSuggestion>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000150 RID: 336 RVA: 0x0034987C File Offset: 0x0034987C
    private static JsonPropertyInfo[] StarterPlanSuggestionPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[8];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((StarterPlanSuggestion)obj).Title;
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
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((StarterPlanSuggestion)obj).Description;
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
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((StarterPlanSuggestion)obj).AgentPrompt;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "AgentPrompt";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("AgentPrompt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((StarterPlanSuggestion)obj).Deliverable;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Deliverable";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("Deliverable", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((StarterPlanSuggestion)obj).ClickySlug;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "ClickySlug";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("ClickySlug", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues11 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues11.IsProperty = true;
      jsonPropertyInfoValues11.IsPublic = true;
      jsonPropertyInfoValues11.IsVirtual = false;
      jsonPropertyInfoValues11.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues11.Converter = null;
      jsonPropertyInfoValues11.Getter = (object obj) => ((StarterPlanSuggestion)obj).ClickyKey;
      jsonPropertyInfoValues11.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues11.IgnoreCondition = null;
      jsonPropertyInfoValues11.HasJsonInclude = false;
      jsonPropertyInfoValues11.IsExtensionData = false;
      jsonPropertyInfoValues11.NumberHandling = null;
      jsonPropertyInfoValues11.PropertyName = "ClickyKey";
      jsonPropertyInfoValues11.JsonPropertyName = null;
      jsonPropertyInfoValues11.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("ClickyKey", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues12 = jsonPropertyInfoValues11;
      array[5] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues12);
      JsonPropertyInfoValues<StarterPlanRoutine> jsonPropertyInfoValues13 = new JsonPropertyInfoValues<StarterPlanRoutine>();
      jsonPropertyInfoValues13.IsProperty = true;
      jsonPropertyInfoValues13.IsPublic = true;
      jsonPropertyInfoValues13.IsVirtual = false;
      jsonPropertyInfoValues13.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues13.Converter = null;
      jsonPropertyInfoValues13.Getter = (object obj) => ((StarterPlanSuggestion)obj).Routine;
      jsonPropertyInfoValues13.Setter = delegate(object obj, [Nullable(2)] StarterPlanRoutine value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues13.IgnoreCondition = null;
      jsonPropertyInfoValues13.HasJsonInclude = false;
      jsonPropertyInfoValues13.IsExtensionData = false;
      jsonPropertyInfoValues13.NumberHandling = null;
      jsonPropertyInfoValues13.PropertyName = "Routine";
      jsonPropertyInfoValues13.JsonPropertyName = null;
      jsonPropertyInfoValues13.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("Routine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(StarterPlanRoutine), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<StarterPlanRoutine> jsonPropertyInfoValues14 = jsonPropertyInfoValues13;
      array[6] = JsonMetadataServices.CreatePropertyInfo<StarterPlanRoutine>(options, jsonPropertyInfoValues14);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues15 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues15.IsProperty = true;
      jsonPropertyInfoValues15.IsPublic = true;
      jsonPropertyInfoValues15.IsVirtual = false;
      jsonPropertyInfoValues15.DeclaringType = typeof(StarterPlanSuggestion);
      jsonPropertyInfoValues15.Converter = null;
      jsonPropertyInfoValues15.Getter = (object obj) => ((StarterPlanSuggestion)obj).SuggestionId;
      jsonPropertyInfoValues15.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues15.IgnoreCondition = null;
      jsonPropertyInfoValues15.HasJsonInclude = false;
      jsonPropertyInfoValues15.IsExtensionData = false;
      jsonPropertyInfoValues15.NumberHandling = null;
      jsonPropertyInfoValues15.PropertyName = "SuggestionId";
      jsonPropertyInfoValues15.JsonPropertyName = null;
      jsonPropertyInfoValues15.AttributeProviderFactory = () => typeof(StarterPlanSuggestion).GetProperty("SuggestionId", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues16 = jsonPropertyInfoValues15;
      array[7] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues16);
      return array;
    }

    // Token: 0x06000151 RID: 337 RVA: 0x0034A028 File Offset: 0x0034A028
    private void StarterPlanSuggestionSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] StarterPlanSuggestion value)
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
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(CoreJsonContext.PropName_description, description);
      }
      string agentPrompt = value.AgentPrompt;
      if (agentPrompt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_agent_prompt, agentPrompt);
      }
      string deliverable = value.Deliverable;
      if (deliverable != null)
      {
        writer.WriteString(CoreJsonContext.PropName_deliverable, deliverable);
      }
      string clickySlug = value.ClickySlug;
      if (clickySlug != null)
      {
        writer.WriteString(CoreJsonContext.PropName_clicky_slug, clickySlug);
      }
      string clickyKey = value.ClickyKey;
      if (clickyKey != null)
      {
        writer.WriteString(CoreJsonContext.PropName_clicky_key, clickyKey);
      }
      StarterPlanRoutine routine = value.Routine;
      if (routine != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_routine);
        this.StarterPlanRoutineSerializeHandler(writer, routine);
      }
      int? suggestionId = value.SuggestionId;
      if (suggestionId != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_suggestion_id);
        JsonSerializer.Serialize<int?>(writer, suggestionId, this.NullableInt32);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000152 RID: 338 RVA: 0x0034A120 File Offset: 0x0034A120
    private static JsonParameterInfoValues[] StarterPlanSuggestionCtorParamInit()
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
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "AgentPrompt",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Deliverable",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ClickySlug",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ClickyKey",
          ParameterType = typeof(string),
          Position = 5,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Routine",
          ParameterType = typeof(StarterPlanRoutine),
          Position = 6,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "SuggestionId",
          ParameterType = typeof(int?),
          Position = 7,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000032 RID: 50
    // (get) Token: 0x06000153 RID: 339 RVA: 0x0034A32C File Offset: 0x0034A32C
    [Nullable(0)]
    public JsonTypeInfo<UserMemoryResponse> UserMemoryResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<UserMemoryResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._UserMemoryResponse) == null)
        {
          jsonTypeInfo = (this._UserMemoryResponse = (JsonTypeInfo<UserMemoryResponse>)base.Options.GetTypeInfo(typeof(UserMemoryResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000154 RID: 340 RVA: 0x0034A368 File Offset: 0x0034A368
    private JsonTypeInfo<UserMemoryResponse> Create_UserMemoryResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<UserMemoryResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<UserMemoryResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<UserMemoryResponse> jsonObjectInfoValues = new JsonObjectInfoValues<UserMemoryResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new UserMemoryResponse((string)args[0], (string)args[1], (string)args[2], (bool?)args[3], (string)args[4]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.UserMemoryResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<41>__UserMemoryResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<41>__UserMemoryResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.UserMemoryResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(UserMemoryResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(bool?),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, UserMemoryResponse>(this.UserMemoryResponseSerializeHandler);
        JsonObjectInfoValues<UserMemoryResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<UserMemoryResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000155 RID: 341 RVA: 0x0034A454 File Offset: 0x0034A454
    private static JsonPropertyInfo[] UserMemoryResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[5];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(UserMemoryResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((UserMemoryResponse)obj).Markdown;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Markdown";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(UserMemoryResponse).GetProperty("Markdown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(UserMemoryResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((UserMemoryResponse)obj).ProfileMarkdown;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "ProfileMarkdown";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(UserMemoryResponse).GetProperty("ProfileMarkdown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(UserMemoryResponse);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((UserMemoryResponse)obj).ContextMarkdown;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "ContextMarkdown";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(UserMemoryResponse).GetProperty("ContextMarkdown", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<bool?>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(UserMemoryResponse);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((UserMemoryResponse)obj).HasProfile;
      jsonPropertyInfoValues7.Setter = delegate(object obj, bool? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "HasProfile";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(UserMemoryResponse).GetProperty("HasProfile", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(bool?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<bool?> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<bool?>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(UserMemoryResponse);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((UserMemoryResponse)obj).UpdatedAt;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "UpdatedAt";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(UserMemoryResponse).GetProperty("UpdatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues10);
      return array;
    }

    // Token: 0x06000156 RID: 342 RVA: 0x0034A900 File Offset: 0x0034A900
    private void UserMemoryResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] UserMemoryResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string markdown = value.Markdown;
      if (markdown != null)
      {
        writer.WriteString(CoreJsonContext.PropName_markdown, markdown);
      }
      string profileMarkdown = value.ProfileMarkdown;
      if (profileMarkdown != null)
      {
        writer.WriteString(CoreJsonContext.PropName_profile_markdown, profileMarkdown);
      }
      string contextMarkdown = value.ContextMarkdown;
      if (contextMarkdown != null)
      {
        writer.WriteString(CoreJsonContext.PropName_context_markdown, contextMarkdown);
      }
      bool? hasProfile = value.HasProfile;
      if (hasProfile != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_has_profile);
        JsonSerializer.Serialize<bool?>(writer, hasProfile, this.NullableBoolean);
      }
      string updatedAt = value.UpdatedAt;
      if (updatedAt != null)
      {
        writer.WriteString(CoreJsonContext.PropName_updated_at, updatedAt);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000157 RID: 343 RVA: 0x0034A9A8 File Offset: 0x0034A9A8
    private static JsonParameterInfoValues[] UserMemoryResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Markdown",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ProfileMarkdown",
          ParameterType = typeof(string),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "ContextMarkdown",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "HasProfile",
          ParameterType = typeof(bool?),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "UpdatedAt",
          ParameterType = typeof(string),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000033 RID: 51
    // (get) Token: 0x06000158 RID: 344 RVA: 0x0034AAF8 File Offset: 0x0034AAF8
    [Nullable(0)]
    public JsonTypeInfo<WebSearchRequest> WebSearchRequest
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<WebSearchRequest> jsonTypeInfo;
        if ((jsonTypeInfo = this._WebSearchRequest) == null)
        {
          jsonTypeInfo = (this._WebSearchRequest = (JsonTypeInfo<WebSearchRequest>)base.Options.GetTypeInfo(typeof(WebSearchRequest)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000159 RID: 345 RVA: 0x0034AB34 File Offset: 0x0034AB34
    private JsonTypeInfo<WebSearchRequest> Create_WebSearchRequest(JsonSerializerOptions options)
    {
      JsonTypeInfo<WebSearchRequest> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<WebSearchRequest>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<WebSearchRequest> jsonObjectInfoValues = new JsonObjectInfoValues<WebSearchRequest>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new WebSearchRequest((string)args[0], (int?)args[1], (string)args[2]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.WebSearchRequestPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<42>__WebSearchRequestCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<42>__WebSearchRequestCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.WebSearchRequestCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(WebSearchRequest).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(int?),
          typeof(string)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, WebSearchRequest>(this.WebSearchRequestSerializeHandler);
        JsonObjectInfoValues<WebSearchRequest> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<WebSearchRequest>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600015A RID: 346 RVA: 0x0034AC20 File Offset: 0x0034AC20
    private static JsonPropertyInfo[] WebSearchRequestPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[3];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(WebSearchRequest);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((WebSearchRequest)obj).Query;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Query";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebSearchRequest).GetProperty("Query", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<int?>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(WebSearchRequest);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((WebSearchRequest)obj).Count;
      jsonPropertyInfoValues3.Setter = delegate(object obj, int? value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Count";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(WebSearchRequest).GetProperty("Count", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(int?), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<int?> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<int?>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(WebSearchRequest);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((WebSearchRequest)obj).Freshness;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Freshness";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(WebSearchRequest).GetProperty("Freshness", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      return array;
    }

    // Token: 0x0600015B RID: 347 RVA: 0x0034AF04 File Offset: 0x0034AF04
    private void WebSearchRequestSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] WebSearchRequest value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string query = value.Query;
      if (query != null)
      {
        writer.WriteString(CoreJsonContext.PropName_query, query);
      }
      int? count = value.Count;
      if (count != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_count);
        JsonSerializer.Serialize<int?>(writer, count, this.NullableInt32);
      }
      string freshness = value.Freshness;
      if (freshness != null)
      {
        writer.WriteString(CoreJsonContext.PropName_freshness, freshness);
      }
      writer.WriteEndObject();
    }

    // Token: 0x0600015C RID: 348 RVA: 0x0034AF7C File Offset: 0x0034AF7C
    private static JsonParameterInfoValues[] WebSearchRequestCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Query",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Count",
          ParameterType = typeof(int?),
          Position = 1,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Freshness",
          ParameterType = typeof(string),
          Position = 2,
          HasDefaultValue = true,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000034 RID: 52
    // (get) Token: 0x0600015D RID: 349 RVA: 0x0034B04C File Offset: 0x0034B04C
    [Nullable(0)]
    public JsonTypeInfo<WebSearchResponse> WebSearchResponse
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<WebSearchResponse> jsonTypeInfo;
        if ((jsonTypeInfo = this._WebSearchResponse) == null)
        {
          jsonTypeInfo = (this._WebSearchResponse = (JsonTypeInfo<WebSearchResponse>)base.Options.GetTypeInfo(typeof(WebSearchResponse)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600015E RID: 350 RVA: 0x0034B088 File Offset: 0x0034B088
    private JsonTypeInfo<WebSearchResponse> Create_WebSearchResponse(JsonSerializerOptions options)
    {
      JsonTypeInfo<WebSearchResponse> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<WebSearchResponse>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<WebSearchResponse> jsonObjectInfoValues = new JsonObjectInfoValues<WebSearchResponse>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new WebSearchResponse((string)args[0], (IReadOnlyList<WebSearchResult>)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.WebSearchResponsePropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<43>__WebSearchResponseCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<43>__WebSearchResponseCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.WebSearchResponseCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(WebSearchResponse).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(IReadOnlyList<WebSearchResult>)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, WebSearchResponse>(this.WebSearchResponseSerializeHandler);
        JsonObjectInfoValues<WebSearchResponse> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<WebSearchResponse>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600015F RID: 351 RVA: 0x0034B174 File Offset: 0x0034B174
    private static JsonPropertyInfo[] WebSearchResponsePropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[2];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(WebSearchResponse);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((WebSearchResponse)obj).Query;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Query";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebSearchResponse).GetProperty("Query", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<IReadOnlyList<WebSearchResult>> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<IReadOnlyList<WebSearchResult>>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(WebSearchResponse);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((WebSearchResponse)obj).Results;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<WebSearchResult> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Results";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(WebSearchResponse).GetProperty("Results", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<WebSearchResult>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<WebSearchResult>> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<WebSearchResult>>(options, jsonPropertyInfoValues4);
      return array;
    }

    // Token: 0x06000160 RID: 352 RVA: 0x0034B35C File Offset: 0x0034B35C
    private void WebSearchResponseSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] WebSearchResponse value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      string query = value.Query;
      if (query != null)
      {
        writer.WriteString(CoreJsonContext.PropName_query, query);
      }
      IReadOnlyList<WebSearchResult> results = value.Results;
      if (results != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_results);
        this.IReadOnlyListWebSearchResultSerializeHandler(writer, results);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000161 RID: 353 RVA: 0x0034B3B4 File Offset: 0x0034B3B4
    private static JsonParameterInfoValues[] WebSearchResponseCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Query",
          ParameterType = typeof(string),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Results",
          ParameterType = typeof(IReadOnlyList<WebSearchResult>),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000035 RID: 53
    // (get) Token: 0x06000162 RID: 354 RVA: 0x0034B448 File Offset: 0x0034B448
    [Nullable(0)]
    public JsonTypeInfo<WebSearchResult> WebSearchResult
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<WebSearchResult> jsonTypeInfo;
        if ((jsonTypeInfo = this._WebSearchResult) == null)
        {
          jsonTypeInfo = (this._WebSearchResult = (JsonTypeInfo<WebSearchResult>)base.Options.GetTypeInfo(typeof(WebSearchResult)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000163 RID: 355 RVA: 0x0034B484 File Offset: 0x0034B484
    private JsonTypeInfo<WebSearchResult> Create_WebSearchResult(JsonSerializerOptions options)
    {
      JsonTypeInfo<WebSearchResult> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<WebSearchResult>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<WebSearchResult> jsonObjectInfoValues = new JsonObjectInfoValues<WebSearchResult>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new WebSearchResult((string)args[0], (string)args[1], (string)args[2], (string)args[3], (IReadOnlyList<string>)args[4]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.WebSearchResultPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<44>__WebSearchResultCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<44>__WebSearchResultCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.WebSearchResultCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(WebSearchResult).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(string),
          typeof(IReadOnlyList<string>)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, WebSearchResult>(this.WebSearchResultSerializeHandler);
        JsonObjectInfoValues<WebSearchResult> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<WebSearchResult>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000164 RID: 356 RVA: 0x0034B570 File Offset: 0x0034B570
    private static JsonPropertyInfo[] WebSearchResultPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[5];
      JsonPropertyInfoValues<string> jsonPropertyInfoValues = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(WebSearchResult);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((WebSearchResult)obj).Title;
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
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(WebSearchResult).GetProperty("Title", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(WebSearchResult);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((WebSearchResult)obj).Url;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Url";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(WebSearchResult).GetProperty("Url", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(WebSearchResult);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((WebSearchResult)obj).Description;
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
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(WebSearchResult).GetProperty("Description", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues6);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(WebSearchResult);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((WebSearchResult)obj).Age;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] string value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "Age";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(WebSearchResult).GetProperty("Age", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues9 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues9.IsProperty = true;
      jsonPropertyInfoValues9.IsPublic = true;
      jsonPropertyInfoValues9.IsVirtual = false;
      jsonPropertyInfoValues9.DeclaringType = typeof(WebSearchResult);
      jsonPropertyInfoValues9.Converter = null;
      jsonPropertyInfoValues9.Getter = (object obj) => ((WebSearchResult)obj).Highlights;
      jsonPropertyInfoValues9.Setter = delegate(object obj, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues9.IgnoreCondition = null;
      jsonPropertyInfoValues9.HasJsonInclude = false;
      jsonPropertyInfoValues9.IsExtensionData = false;
      jsonPropertyInfoValues9.NumberHandling = null;
      jsonPropertyInfoValues9.PropertyName = "Highlights";
      jsonPropertyInfoValues9.JsonPropertyName = null;
      jsonPropertyInfoValues9.AttributeProviderFactory = () => typeof(WebSearchResult).GetProperty("Highlights", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues10 = jsonPropertyInfoValues9;
      array[4] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues10);
      return array;
    }

    // Token: 0x06000165 RID: 357 RVA: 0x0034BA40 File Offset: 0x0034BA40
    private void WebSearchResultSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] WebSearchResult value)
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
        writer.WriteString(CoreJsonContext.PropName_title, title);
      }
      string url = value.Url;
      if (url != null)
      {
        writer.WriteString(CoreJsonContext.PropName_url, url);
      }
      string description = value.Description;
      if (description != null)
      {
        writer.WriteString(CoreJsonContext.PropName_description, description);
      }
      string age = value.Age;
      if (age != null)
      {
        writer.WriteString(CoreJsonContext.PropName_age, age);
      }
      IReadOnlyList<string> highlights = value.Highlights;
      if (highlights != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_highlights);
        this.IReadOnlyListStringSerializeHandler(writer, highlights);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000166 RID: 358 RVA: 0x0034BADC File Offset: 0x0034BADC
    private static JsonParameterInfoValues[] WebSearchResultCtorParamInit()
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
          Name = "Url",
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
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Age",
          ParameterType = typeof(string),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        },
        new JsonParameterInfoValues
        {
          Name = "Highlights",
          ParameterType = typeof(IReadOnlyList<string>),
          Position = 4,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = true
        }
      };
    }

    // Token: 0x17000036 RID: 54
    // (get) Token: 0x06000167 RID: 359 RVA: 0x0034BC2C File Offset: 0x0034BC2C
    [Nullable(0)]
    public JsonTypeInfo<HotkeyBinding> HotkeyBinding
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<HotkeyBinding> jsonTypeInfo;
        if ((jsonTypeInfo = this._HotkeyBinding) == null)
        {
          jsonTypeInfo = (this._HotkeyBinding = (JsonTypeInfo<HotkeyBinding>)base.Options.GetTypeInfo(typeof(HotkeyBinding)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000168 RID: 360 RVA: 0x0034BC68 File Offset: 0x0034BC68
    private JsonTypeInfo<HotkeyBinding> Create_HotkeyBinding(JsonSerializerOptions options)
    {
      JsonTypeInfo<HotkeyBinding> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<HotkeyBinding>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<HotkeyBinding> jsonObjectInfoValues = new JsonObjectInfoValues<HotkeyBinding>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new HotkeyBinding((HotkeyModifiers)args[0], (HotkeyTrigger)args[1]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.HotkeyBindingPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<45>__HotkeyBindingCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<45>__HotkeyBindingCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.HotkeyBindingCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(HotkeyBinding).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(HotkeyModifiers),
          typeof(HotkeyTrigger)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, HotkeyBinding>(this.HotkeyBindingSerializeHandler);
        JsonObjectInfoValues<HotkeyBinding> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<HotkeyBinding>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000169 RID: 361 RVA: 0x0034BD54 File Offset: 0x0034BD54
    private static JsonPropertyInfo[] HotkeyBindingPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<HotkeyModifiers> jsonPropertyInfoValues = new JsonPropertyInfoValues<HotkeyModifiers>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(HotkeyBinding);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((HotkeyBinding)obj).Modifiers;
      jsonPropertyInfoValues.Setter = delegate(object obj, HotkeyModifiers value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "Modifiers";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(HotkeyBinding).GetProperty("Modifiers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyModifiers), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyModifiers> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<HotkeyModifiers>(options, jsonPropertyInfoValues2);
      JsonPropertyInfoValues<HotkeyTrigger> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<HotkeyTrigger>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(HotkeyBinding);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((HotkeyBinding)obj).Trigger;
      jsonPropertyInfoValues3.Setter = delegate(object obj, HotkeyTrigger value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "Trigger";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(HotkeyBinding).GetProperty("Trigger", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyTrigger), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyTrigger> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<HotkeyTrigger>(options, jsonPropertyInfoValues4);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<IReadOnlyList<string>>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(HotkeyBinding);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = null;
      jsonPropertyInfoValues5.Setter = null;
      jsonPropertyInfoValues5.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "KeyNames";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(HotkeyBinding).GetProperty("KeyNames", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(IReadOnlyList<string>), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<IReadOnlyList<string>> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<IReadOnlyList<string>>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      JsonPropertyInfoValues<string> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<string>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(HotkeyBinding);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = null;
      jsonPropertyInfoValues7.Setter = null;
      jsonPropertyInfoValues7.IgnoreCondition = new JsonIgnoreCondition?(JsonIgnoreCondition.Always);
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "DisplayText";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(HotkeyBinding).GetProperty("DisplayText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(string), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<string> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<string>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      return array;
    }

    // Token: 0x0600016A RID: 362 RVA: 0x0034C0A8 File Offset: 0x0034C0A8
    private void HotkeyBindingSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] HotkeyBinding value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      writer.WritePropertyName(CoreJsonContext.PropName_modifiers);
      JsonSerializer.Serialize<HotkeyModifiers>(writer, value.Modifiers, this.HotkeyModifiers);
      writer.WritePropertyName(CoreJsonContext.PropName_trigger);
      JsonSerializer.Serialize<HotkeyTrigger>(writer, value.Trigger, this.HotkeyTrigger);
      writer.WriteEndObject();
    }

    // Token: 0x0600016B RID: 363 RVA: 0x0034C108 File Offset: 0x0034C108
    private static JsonParameterInfoValues[] HotkeyBindingCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "Modifiers",
          ParameterType = typeof(HotkeyModifiers),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Trigger",
          ParameterType = typeof(HotkeyTrigger),
          Position = 1,
          HasDefaultValue = true,
          DefaultValue = HeyClicky.HotkeyTrigger.Hold,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000037 RID: 55
    // (get) Token: 0x0600016C RID: 364 RVA: 0x0034C1A0 File Offset: 0x0034C1A0
    [Nullable(0)]
    public JsonTypeInfo<HotkeyBindings> HotkeyBindings
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<HotkeyBindings> jsonTypeInfo;
        if ((jsonTypeInfo = this._HotkeyBindings) == null)
        {
          jsonTypeInfo = (this._HotkeyBindings = (JsonTypeInfo<HotkeyBindings>)base.Options.GetTypeInfo(typeof(HotkeyBindings)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600016D RID: 365 RVA: 0x0034C1DC File Offset: 0x0034C1DC
    private JsonTypeInfo<HotkeyBindings> Create_HotkeyBindings(JsonSerializerOptions options)
    {
      JsonTypeInfo<HotkeyBindings> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<HotkeyBindings>(options, out jsonTypeInfo))
      {
        JsonObjectInfoValues<HotkeyBindings> jsonObjectInfoValues = new JsonObjectInfoValues<HotkeyBindings>();
        jsonObjectInfoValues.ObjectCreator = null;
        jsonObjectInfoValues.ObjectWithParameterizedConstructorCreator = (object[] args) => new HotkeyBindings((HotkeyBinding)args[0], (HotkeyBinding)args[1], (HotkeyBinding)args[2], (HotkeyBinding)args[3]);
        jsonObjectInfoValues.PropertyMetadataInitializer = (JsonSerializerContext _) => CoreJsonContext.HotkeyBindingsPropInit(options);
        Func<JsonParameterInfoValues[]> func;
        if ((func = CoreJsonContext.<>O.<46>__HotkeyBindingsCtorParamInit) == null)
        {
          func = (CoreJsonContext.<>O.<46>__HotkeyBindingsCtorParamInit = new Func<JsonParameterInfoValues[]>(CoreJsonContext.HotkeyBindingsCtorParamInit));
        }
        jsonObjectInfoValues.ConstructorParameterMetadataInitializer = func;
        jsonObjectInfoValues.ConstructorAttributeProviderFactory = () => typeof(HotkeyBindings).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
        {
          typeof(HotkeyBinding),
          typeof(HotkeyBinding),
          typeof(HotkeyBinding),
          typeof(HotkeyBinding)
        }, null);
        jsonObjectInfoValues.SerializeHandler = new Action<Utf8JsonWriter, HotkeyBindings>(this.HotkeyBindingsSerializeHandler);
        JsonObjectInfoValues<HotkeyBindings> jsonObjectInfoValues2 = jsonObjectInfoValues;
        jsonTypeInfo = JsonMetadataServices.CreateObjectInfo<HotkeyBindings>(options, jsonObjectInfoValues2);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600016E RID: 366 RVA: 0x0034C2C8 File Offset: 0x0034C2C8
    private static JsonPropertyInfo[] HotkeyBindingsPropInit(JsonSerializerOptions options)
    {
      JsonPropertyInfo[] array = new JsonPropertyInfo[4];
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues = new JsonPropertyInfoValues<HotkeyBinding>();
      jsonPropertyInfoValues.IsProperty = true;
      jsonPropertyInfoValues.IsPublic = true;
      jsonPropertyInfoValues.IsVirtual = false;
      jsonPropertyInfoValues.DeclaringType = typeof(HotkeyBindings);
      jsonPropertyInfoValues.Converter = null;
      jsonPropertyInfoValues.Getter = (object obj) => ((HotkeyBindings)obj).PushToTalk;
      jsonPropertyInfoValues.Setter = delegate(object obj, [Nullable(2)] HotkeyBinding value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues.IgnoreCondition = null;
      jsonPropertyInfoValues.HasJsonInclude = false;
      jsonPropertyInfoValues.IsExtensionData = false;
      jsonPropertyInfoValues.NumberHandling = null;
      jsonPropertyInfoValues.PropertyName = "PushToTalk";
      jsonPropertyInfoValues.JsonPropertyName = null;
      jsonPropertyInfoValues.AttributeProviderFactory = () => typeof(HotkeyBindings).GetProperty("PushToTalk", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBinding), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues2 = jsonPropertyInfoValues;
      array[0] = JsonMetadataServices.CreatePropertyInfo<HotkeyBinding>(options, jsonPropertyInfoValues2);
      array[0].IsGetNullable = false;
      array[0].IsSetNullable = false;
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues3 = new JsonPropertyInfoValues<HotkeyBinding>();
      jsonPropertyInfoValues3.IsProperty = true;
      jsonPropertyInfoValues3.IsPublic = true;
      jsonPropertyInfoValues3.IsVirtual = false;
      jsonPropertyInfoValues3.DeclaringType = typeof(HotkeyBindings);
      jsonPropertyInfoValues3.Converter = null;
      jsonPropertyInfoValues3.Getter = (object obj) => ((HotkeyBindings)obj).TextMode;
      jsonPropertyInfoValues3.Setter = delegate(object obj, [Nullable(2)] HotkeyBinding value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues3.IgnoreCondition = null;
      jsonPropertyInfoValues3.HasJsonInclude = false;
      jsonPropertyInfoValues3.IsExtensionData = false;
      jsonPropertyInfoValues3.NumberHandling = null;
      jsonPropertyInfoValues3.PropertyName = "TextMode";
      jsonPropertyInfoValues3.JsonPropertyName = null;
      jsonPropertyInfoValues3.AttributeProviderFactory = () => typeof(HotkeyBindings).GetProperty("TextMode", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBinding), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues4 = jsonPropertyInfoValues3;
      array[1] = JsonMetadataServices.CreatePropertyInfo<HotkeyBinding>(options, jsonPropertyInfoValues4);
      array[1].IsGetNullable = false;
      array[1].IsSetNullable = false;
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues5 = new JsonPropertyInfoValues<HotkeyBinding>();
      jsonPropertyInfoValues5.IsProperty = true;
      jsonPropertyInfoValues5.IsPublic = true;
      jsonPropertyInfoValues5.IsVirtual = false;
      jsonPropertyInfoValues5.DeclaringType = typeof(HotkeyBindings);
      jsonPropertyInfoValues5.Converter = null;
      jsonPropertyInfoValues5.Getter = (object obj) => ((HotkeyBindings)obj).Dictation;
      jsonPropertyInfoValues5.Setter = delegate(object obj, [Nullable(2)] HotkeyBinding value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues5.IgnoreCondition = null;
      jsonPropertyInfoValues5.HasJsonInclude = false;
      jsonPropertyInfoValues5.IsExtensionData = false;
      jsonPropertyInfoValues5.NumberHandling = null;
      jsonPropertyInfoValues5.PropertyName = "Dictation";
      jsonPropertyInfoValues5.JsonPropertyName = null;
      jsonPropertyInfoValues5.AttributeProviderFactory = () => typeof(HotkeyBindings).GetProperty("Dictation", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBinding), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues6 = jsonPropertyInfoValues5;
      array[2] = JsonMetadataServices.CreatePropertyInfo<HotkeyBinding>(options, jsonPropertyInfoValues6);
      array[2].IsGetNullable = false;
      array[2].IsSetNullable = false;
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues7 = new JsonPropertyInfoValues<HotkeyBinding>();
      jsonPropertyInfoValues7.IsProperty = true;
      jsonPropertyInfoValues7.IsPublic = true;
      jsonPropertyInfoValues7.IsVirtual = false;
      jsonPropertyInfoValues7.DeclaringType = typeof(HotkeyBindings);
      jsonPropertyInfoValues7.Converter = null;
      jsonPropertyInfoValues7.Getter = (object obj) => ((HotkeyBindings)obj).HandsFreeDictation;
      jsonPropertyInfoValues7.Setter = delegate(object obj, [Nullable(2)] HotkeyBinding value)
      {
        throw new InvalidOperationException("Setting init-only properties is not supported in source generation mode.");
      };
      jsonPropertyInfoValues7.IgnoreCondition = null;
      jsonPropertyInfoValues7.HasJsonInclude = false;
      jsonPropertyInfoValues7.IsExtensionData = false;
      jsonPropertyInfoValues7.NumberHandling = null;
      jsonPropertyInfoValues7.PropertyName = "HandsFreeDictation";
      jsonPropertyInfoValues7.JsonPropertyName = null;
      jsonPropertyInfoValues7.AttributeProviderFactory = () => typeof(HotkeyBindings).GetProperty("HandsFreeDictation", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, typeof(HotkeyBinding), Array.Empty<Type>(), null);
      JsonPropertyInfoValues<HotkeyBinding> jsonPropertyInfoValues8 = jsonPropertyInfoValues7;
      array[3] = JsonMetadataServices.CreatePropertyInfo<HotkeyBinding>(options, jsonPropertyInfoValues8);
      array[3].IsGetNullable = false;
      array[3].IsSetNullable = false;
      return array;
    }

    // Token: 0x0600016F RID: 367 RVA: 0x0034C6D0 File Offset: 0x0034C6D0
    private void HotkeyBindingsSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] HotkeyBindings value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartObject();
      HotkeyBinding pushToTalk = value.PushToTalk;
      if (pushToTalk != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_push_to_talk);
        this.HotkeyBindingSerializeHandler(writer, pushToTalk);
      }
      HotkeyBinding textMode = value.TextMode;
      if (textMode != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_text_mode);
        this.HotkeyBindingSerializeHandler(writer, textMode);
      }
      HotkeyBinding dictation = value.Dictation;
      if (dictation != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_dictation);
        this.HotkeyBindingSerializeHandler(writer, dictation);
      }
      HotkeyBinding handsFreeDictation = value.HandsFreeDictation;
      if (handsFreeDictation != null)
      {
        writer.WritePropertyName(CoreJsonContext.PropName_hands_free_dictation);
        this.HotkeyBindingSerializeHandler(writer, handsFreeDictation);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06000170 RID: 368 RVA: 0x0034C768 File Offset: 0x0034C768
    private static JsonParameterInfoValues[] HotkeyBindingsCtorParamInit()
    {
      return new JsonParameterInfoValues[]
      {
        new JsonParameterInfoValues
        {
          Name = "PushToTalk",
          ParameterType = typeof(HotkeyBinding),
          Position = 0,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "TextMode",
          ParameterType = typeof(HotkeyBinding),
          Position = 1,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "Dictation",
          ParameterType = typeof(HotkeyBinding),
          Position = 2,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        },
        new JsonParameterInfoValues
        {
          Name = "HandsFreeDictation",
          ParameterType = typeof(HotkeyBinding),
          Position = 3,
          HasDefaultValue = false,
          DefaultValue = null,
          IsNullable = false
        }
      };
    }

    // Token: 0x17000038 RID: 56
    // (get) Token: 0x06000171 RID: 369 RVA: 0x0034C878 File Offset: 0x0034C878
    [Nullable(0)]
    public JsonTypeInfo<HotkeyModifiers> HotkeyModifiers
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<HotkeyModifiers> jsonTypeInfo;
        if ((jsonTypeInfo = this._HotkeyModifiers) == null)
        {
          jsonTypeInfo = (this._HotkeyModifiers = (JsonTypeInfo<HotkeyModifiers>)base.Options.GetTypeInfo(typeof(HotkeyModifiers)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000172 RID: 370 RVA: 0x0034C8B4 File Offset: 0x0034C8B4
    private JsonTypeInfo<HotkeyModifiers> Create_HotkeyModifiers(JsonSerializerOptions options)
    {
      JsonTypeInfo<HotkeyModifiers> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<HotkeyModifiers>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = CoreJsonContext.ExpandConverter(typeof(HotkeyModifiers), new JsonStringEnumConverter<HotkeyModifiers>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<HotkeyModifiers>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000039 RID: 57
    // (get) Token: 0x06000173 RID: 371 RVA: 0x0034C8F4 File Offset: 0x0034C8F4
    [Nullable(0)]
    public JsonTypeInfo<HotkeyTrigger> HotkeyTrigger
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<HotkeyTrigger> jsonTypeInfo;
        if ((jsonTypeInfo = this._HotkeyTrigger) == null)
        {
          jsonTypeInfo = (this._HotkeyTrigger = (JsonTypeInfo<HotkeyTrigger>)base.Options.GetTypeInfo(typeof(HotkeyTrigger)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000174 RID: 372 RVA: 0x0034C930 File Offset: 0x0034C930
    private JsonTypeInfo<HotkeyTrigger> Create_HotkeyTrigger(JsonSerializerOptions options)
    {
      JsonTypeInfo<HotkeyTrigger> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<HotkeyTrigger>(options, out jsonTypeInfo))
      {
        JsonConverter jsonConverter = CoreJsonContext.ExpandConverter(typeof(HotkeyTrigger), new JsonStringEnumConverter<HotkeyTrigger>(), options, true);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<HotkeyTrigger>(options, jsonConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700003A RID: 58
    // (get) Token: 0x06000175 RID: 373 RVA: 0x0034C970 File Offset: 0x0034C970
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

    // Token: 0x06000176 RID: 374 RVA: 0x0034C9AC File Offset: 0x0034C9AC
    private JsonTypeInfo<Dictionary<string, string>> Create_DictionaryStringString(JsonSerializerOptions options)
    {
      JsonTypeInfo<Dictionary<string, string>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<Dictionary<string, string>>(options, out jsonTypeInfo))
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

    // Token: 0x06000177 RID: 375 RVA: 0x0034CA20 File Offset: 0x0034CA20
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

    // Token: 0x1700003B RID: 59
    // (get) Token: 0x06000178 RID: 376 RVA: 0x0034CA94 File Offset: 0x0034CA94
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<PaywallPlanOption>> IReadOnlyListPaywallPlanOption
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<PaywallPlanOption>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListPaywallPlanOption) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListPaywallPlanOption = (JsonTypeInfo<IReadOnlyList<PaywallPlanOption>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<PaywallPlanOption>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000179 RID: 377 RVA: 0x0034CAD0 File Offset: 0x0034CAD0
    private JsonTypeInfo<IReadOnlyList<PaywallPlanOption>> Create_IReadOnlyListPaywallPlanOption(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<PaywallPlanOption>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<PaywallPlanOption>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<PaywallPlanOption>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<PaywallPlanOption>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<PaywallPlanOption>>(this.IReadOnlyListPaywallPlanOptionSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<PaywallPlanOption>, PaywallPlanOption>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600017A RID: 378 RVA: 0x0034CB28 File Offset: 0x0034CB28
    private void IReadOnlyListPaywallPlanOptionSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<PaywallPlanOption> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (PaywallPlanOption paywallPlanOption in value)
      {
        this.PaywallPlanOptionSerializeHandler(writer, paywallPlanOption);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700003C RID: 60
    // (get) Token: 0x0600017B RID: 379 RVA: 0x0034CB88 File Offset: 0x0034CB88
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<ImageSearchResult>> IReadOnlyListImageSearchResult
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<ImageSearchResult>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListImageSearchResult) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListImageSearchResult = (JsonTypeInfo<IReadOnlyList<ImageSearchResult>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<ImageSearchResult>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600017C RID: 380 RVA: 0x0034CBC4 File Offset: 0x0034CBC4
    private JsonTypeInfo<IReadOnlyList<ImageSearchResult>> Create_IReadOnlyListImageSearchResult(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<ImageSearchResult>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<ImageSearchResult>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<ImageSearchResult>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<ImageSearchResult>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<ImageSearchResult>>(this.IReadOnlyListImageSearchResultSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<ImageSearchResult>, ImageSearchResult>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600017D RID: 381 RVA: 0x0034CC1C File Offset: 0x0034CC1C
    private void IReadOnlyListImageSearchResultSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ImageSearchResult> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (ImageSearchResult imageSearchResult in value)
      {
        this.ImageSearchResultSerializeHandler(writer, imageSearchResult);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700003D RID: 61
    // (get) Token: 0x0600017E RID: 382 RVA: 0x0034CC7C File Offset: 0x0034CC7C
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<InterviewLine>> IReadOnlyListInterviewLine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<InterviewLine>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListInterviewLine) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListInterviewLine = (JsonTypeInfo<IReadOnlyList<InterviewLine>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<InterviewLine>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600017F RID: 383 RVA: 0x0034CCB8 File Offset: 0x0034CCB8
    private JsonTypeInfo<IReadOnlyList<InterviewLine>> Create_IReadOnlyListInterviewLine(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<InterviewLine>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<InterviewLine>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<InterviewLine>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<InterviewLine>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<InterviewLine>>(this.IReadOnlyListInterviewLineSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<InterviewLine>, InterviewLine>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000180 RID: 384 RVA: 0x0034CD10 File Offset: 0x0034CD10
    private void IReadOnlyListInterviewLineSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<InterviewLine> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (InterviewLine interviewLine in value)
      {
        this.InterviewLineSerializeHandler(writer, interviewLine);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700003E RID: 62
    // (get) Token: 0x06000181 RID: 385 RVA: 0x0034CD70 File Offset: 0x0034CD70
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<RegisteredDevice>> IReadOnlyListRegisteredDevice
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<RegisteredDevice>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListRegisteredDevice) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListRegisteredDevice = (JsonTypeInfo<IReadOnlyList<RegisteredDevice>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<RegisteredDevice>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000182 RID: 386 RVA: 0x0034CDAC File Offset: 0x0034CDAC
    private JsonTypeInfo<IReadOnlyList<RegisteredDevice>> Create_IReadOnlyListRegisteredDevice(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<RegisteredDevice>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<RegisteredDevice>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<RegisteredDevice>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<RegisteredDevice>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<RegisteredDevice>>(this.IReadOnlyListRegisteredDeviceSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<RegisteredDevice>, RegisteredDevice>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000183 RID: 387 RVA: 0x0034CE04 File Offset: 0x0034CE04
    private void IReadOnlyListRegisteredDeviceSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<RegisteredDevice> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (RegisteredDevice registeredDevice in value)
      {
        this.RegisteredDeviceSerializeHandler(writer, registeredDevice);
      }
      writer.WriteEndArray();
    }

    // Token: 0x1700003F RID: 63
    // (get) Token: 0x06000184 RID: 388 RVA: 0x0034CE64 File Offset: 0x0034CE64
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<StarterPlanClicky>> IReadOnlyListStarterPlanClicky
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<StarterPlanClicky>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListStarterPlanClicky) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListStarterPlanClicky = (JsonTypeInfo<IReadOnlyList<StarterPlanClicky>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<StarterPlanClicky>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000185 RID: 389 RVA: 0x0034CEA0 File Offset: 0x0034CEA0
    private JsonTypeInfo<IReadOnlyList<StarterPlanClicky>> Create_IReadOnlyListStarterPlanClicky(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<StarterPlanClicky>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<StarterPlanClicky>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<StarterPlanClicky>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<StarterPlanClicky>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<StarterPlanClicky>>(this.IReadOnlyListStarterPlanClickySerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<StarterPlanClicky>, StarterPlanClicky>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000186 RID: 390 RVA: 0x0034CEF8 File Offset: 0x0034CEF8
    private void IReadOnlyListStarterPlanClickySerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClicky> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (StarterPlanClicky starterPlanClicky in value)
      {
        this.StarterPlanClickySerializeHandler(writer, starterPlanClicky);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000040 RID: 64
    // (get) Token: 0x06000187 RID: 391 RVA: 0x0034CF58 File Offset: 0x0034CF58
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>> IReadOnlyListStarterPlanClickyInput
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListStarterPlanClickyInput) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListStarterPlanClickyInput = (JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<StarterPlanClickyInput>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000188 RID: 392 RVA: 0x0034CF94 File Offset: 0x0034CF94
    private JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>> Create_IReadOnlyListStarterPlanClickyInput(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<StarterPlanClickyInput>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<StarterPlanClickyInput>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<StarterPlanClickyInput>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<StarterPlanClickyInput>>(this.IReadOnlyListStarterPlanClickyInputSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<StarterPlanClickyInput>, StarterPlanClickyInput>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000189 RID: 393 RVA: 0x0034CFEC File Offset: 0x0034CFEC
    private void IReadOnlyListStarterPlanClickyInputSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanClickyInput> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (StarterPlanClickyInput starterPlanClickyInput in value)
      {
        this.StarterPlanClickyInputSerializeHandler(writer, starterPlanClickyInput);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000041 RID: 65
    // (get) Token: 0x0600018A RID: 394 RVA: 0x0034D04C File Offset: 0x0034D04C
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>> IReadOnlyListStarterPlanRoutine
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListStarterPlanRoutine) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListStarterPlanRoutine = (JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<StarterPlanRoutine>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600018B RID: 395 RVA: 0x0034D088 File Offset: 0x0034D088
    private JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>> Create_IReadOnlyListStarterPlanRoutine(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<StarterPlanRoutine>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<StarterPlanRoutine>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<StarterPlanRoutine>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<StarterPlanRoutine>>(this.IReadOnlyListStarterPlanRoutineSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<StarterPlanRoutine>, StarterPlanRoutine>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600018C RID: 396 RVA: 0x0034D0E0 File Offset: 0x0034D0E0
    private void IReadOnlyListStarterPlanRoutineSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanRoutine> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (StarterPlanRoutine starterPlanRoutine in value)
      {
        this.StarterPlanRoutineSerializeHandler(writer, starterPlanRoutine);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000042 RID: 66
    // (get) Token: 0x0600018D RID: 397 RVA: 0x0034D140 File Offset: 0x0034D140
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>> IReadOnlyListStarterPlanSuggestion
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListStarterPlanSuggestion) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListStarterPlanSuggestion = (JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<StarterPlanSuggestion>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600018E RID: 398 RVA: 0x0034D17C File Offset: 0x0034D17C
    private JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>> Create_IReadOnlyListStarterPlanSuggestion(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<StarterPlanSuggestion>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<StarterPlanSuggestion>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<StarterPlanSuggestion>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<StarterPlanSuggestion>>(this.IReadOnlyListStarterPlanSuggestionSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<StarterPlanSuggestion>, StarterPlanSuggestion>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x0600018F RID: 399 RVA: 0x0034D1D4 File Offset: 0x0034D1D4
    private void IReadOnlyListStarterPlanSuggestionSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<StarterPlanSuggestion> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (StarterPlanSuggestion starterPlanSuggestion in value)
      {
        this.StarterPlanSuggestionSerializeHandler(writer, starterPlanSuggestion);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000043 RID: 67
    // (get) Token: 0x06000190 RID: 400 RVA: 0x0034D234 File Offset: 0x0034D234
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<WebSearchResult>> IReadOnlyListWebSearchResult
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<WebSearchResult>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListWebSearchResult) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListWebSearchResult = (JsonTypeInfo<IReadOnlyList<WebSearchResult>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<WebSearchResult>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000191 RID: 401 RVA: 0x0034D270 File Offset: 0x0034D270
    private JsonTypeInfo<IReadOnlyList<WebSearchResult>> Create_IReadOnlyListWebSearchResult(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<WebSearchResult>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<WebSearchResult>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<WebSearchResult>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<WebSearchResult>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<WebSearchResult>>(this.IReadOnlyListWebSearchResultSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<WebSearchResult>, WebSearchResult>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000192 RID: 402 RVA: 0x0034D2C8 File Offset: 0x0034D2C8
    private void IReadOnlyListWebSearchResultSerializeHandler(Utf8JsonWriter writer, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<WebSearchResult> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (WebSearchResult webSearchResult in value)
      {
        this.WebSearchResultSerializeHandler(writer, webSearchResult);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000044 RID: 68
    // (get) Token: 0x06000193 RID: 403 RVA: 0x0034D328 File Offset: 0x0034D328
    [Nullable(0)]
    public JsonTypeInfo<IReadOnlyList<JsonElement>> IReadOnlyListJsonElement
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<IReadOnlyList<JsonElement>> jsonTypeInfo;
        if ((jsonTypeInfo = this._IReadOnlyListJsonElement) == null)
        {
          jsonTypeInfo = (this._IReadOnlyListJsonElement = (JsonTypeInfo<IReadOnlyList<JsonElement>>)base.Options.GetTypeInfo(typeof(IReadOnlyList<JsonElement>)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x06000194 RID: 404 RVA: 0x0034D364 File Offset: 0x0034D364
    private JsonTypeInfo<IReadOnlyList<JsonElement>> Create_IReadOnlyListJsonElement(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<JsonElement>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<JsonElement>>(options, out jsonTypeInfo))
      {
        JsonCollectionInfoValues<IReadOnlyList<JsonElement>> jsonCollectionInfoValues = new JsonCollectionInfoValues<IReadOnlyList<JsonElement>>
        {
          ObjectCreator = null,
          SerializeHandler = new Action<Utf8JsonWriter, IReadOnlyList<JsonElement>>(this.IReadOnlyListJsonElementSerializeHandler)
        };
        jsonTypeInfo = JsonMetadataServices.CreateIEnumerableInfo<IReadOnlyList<JsonElement>, JsonElement>(options, jsonCollectionInfoValues);
        jsonTypeInfo.NumberHandling = null;
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x06000195 RID: 405 RVA: 0x0034D3BC File Offset: 0x0034D3BC
    private void IReadOnlyListJsonElementSerializeHandler(Utf8JsonWriter writer, [Nullable(2)] IReadOnlyList<JsonElement> value)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }
      writer.WriteStartArray();
      foreach (JsonElement jsonElement in value)
      {
        JsonSerializer.Serialize<JsonElement>(writer, jsonElement, this.JsonElement);
      }
      writer.WriteEndArray();
    }

    // Token: 0x17000045 RID: 69
    // (get) Token: 0x06000196 RID: 406 RVA: 0x0034D420 File Offset: 0x0034D420
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

    // Token: 0x06000197 RID: 407 RVA: 0x0034D45C File Offset: 0x0034D45C
    private JsonTypeInfo<IReadOnlyList<string>> Create_IReadOnlyListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<IReadOnlyList<string>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<IReadOnlyList<string>>(options, out jsonTypeInfo))
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

    // Token: 0x06000198 RID: 408 RVA: 0x0034D4B4 File Offset: 0x0034D4B4
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

    // Token: 0x17000046 RID: 70
    // (get) Token: 0x06000199 RID: 409 RVA: 0x0034D514 File Offset: 0x0034D514
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

    // Token: 0x0600019A RID: 410 RVA: 0x0034D550 File Offset: 0x0034D550
    private JsonTypeInfo<List<string>> Create_ListString(JsonSerializerOptions options)
    {
      JsonTypeInfo<List<string>> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<List<string>>(options, out jsonTypeInfo))
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

    // Token: 0x0600019B RID: 411 RVA: 0x0034D5C4 File Offset: 0x0034D5C4
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

    // Token: 0x17000047 RID: 71
    // (get) Token: 0x0600019C RID: 412 RVA: 0x0034D608 File Offset: 0x0034D608
    [Nullable(0)]
    public JsonTypeInfo<JsonElement> JsonElement
    {
      [NullableContext(0)]
      get
      {
        JsonTypeInfo<JsonElement> jsonTypeInfo;
        if ((jsonTypeInfo = this._JsonElement) == null)
        {
          jsonTypeInfo = (this._JsonElement = (JsonTypeInfo<JsonElement>)base.Options.GetTypeInfo(typeof(JsonElement)));
        }
        return jsonTypeInfo;
      }
    }

    // Token: 0x0600019D RID: 413 RVA: 0x0034D644 File Offset: 0x0034D644
    private JsonTypeInfo<JsonElement> Create_JsonElement(JsonSerializerOptions options)
    {
      JsonTypeInfo<JsonElement> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<JsonElement>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<JsonElement>(options, JsonMetadataServices.JsonElementConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000048 RID: 72
    // (get) Token: 0x0600019E RID: 414 RVA: 0x0034D670 File Offset: 0x0034D670
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

    // Token: 0x0600019F RID: 415 RVA: 0x0034D6AC File Offset: 0x0034D6AC
    private JsonTypeInfo<int> Create_Int32(JsonSerializerOptions options)
    {
      JsonTypeInfo<int> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<int>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int>(options, JsonMetadataServices.Int32Converter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x17000049 RID: 73
    // (get) Token: 0x060001A0 RID: 416 RVA: 0x0034D6D8 File Offset: 0x0034D6D8
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

    // Token: 0x060001A1 RID: 417 RVA: 0x0034D714 File Offset: 0x0034D714
    private JsonTypeInfo<int?> Create_NullableInt32(JsonSerializerOptions options)
    {
      JsonTypeInfo<int?> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<int?>(options, out jsonTypeInfo))
      {
        JsonConverter nullableConverter = JsonMetadataServices.GetNullableConverter<int>(options);
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<int?>(options, nullableConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700004A RID: 74
    // (get) Token: 0x060001A2 RID: 418 RVA: 0x0034D744 File Offset: 0x0034D744
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

    // Token: 0x060001A3 RID: 419 RVA: 0x0034D780 File Offset: 0x0034D780
    private JsonTypeInfo<long> Create_Int64(JsonSerializerOptions options)
    {
      JsonTypeInfo<long> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<long>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<long>(options, JsonMetadataServices.Int64Converter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700004B RID: 75
    // (get) Token: 0x060001A4 RID: 420 RVA: 0x0034D7AC File Offset: 0x0034D7AC
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

    // Token: 0x060001A5 RID: 421 RVA: 0x0034D7E8 File Offset: 0x0034D7E8
    private JsonTypeInfo<string> Create_String(JsonSerializerOptions options)
    {
      JsonTypeInfo<string> jsonTypeInfo;
      if (!CoreJsonContext.TryGetTypeInfoForRuntimeCustomConverter<string>(options, out jsonTypeInfo))
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<string>(options, JsonMetadataServices.StringConverter);
      }
      jsonTypeInfo.OriginatingResolver = this;
      return jsonTypeInfo;
    }

    // Token: 0x1700004C RID: 76
    // (get) Token: 0x060001A6 RID: 422 RVA: 0x0034D814 File Offset: 0x0034D814
    public static CoreJsonContext Default { get; } = new CoreJsonContext(new JsonSerializerOptions(CoreJsonContext.s_defaultOptions));

    // Token: 0x1700004D RID: 77
    // (get) Token: 0x060001A7 RID: 423 RVA: 0x0034D81C File Offset: 0x0034D81C
    [Nullable(2)]
    protected override JsonSerializerOptions GeneratedSerializerOptions
    {
      [NullableContext(2)]
      get;
    } = CoreJsonContext.s_defaultOptions;

    // Token: 0x060001A8 RID: 424 RVA: 0x0034D824 File Offset: 0x0034D824
    public CoreJsonContext()
      : base(null)
    {
    }

    // Token: 0x060001A9 RID: 425 RVA: 0x0034D838 File Offset: 0x0034D838
    public CoreJsonContext(JsonSerializerOptions options)
      : base(options)
    {
    }

    // Token: 0x060001AA RID: 426 RVA: 0x0034D84C File Offset: 0x0034D84C
    private static bool TryGetTypeInfoForRuntimeCustomConverter<[Nullable(2)] TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
    {
      JsonConverter runtimeConverterForType = CoreJsonContext.GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
      if (runtimeConverterForType != null)
      {
        jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
        return true;
      }
      jsonTypeInfo = null;
      return false;
    }

    // Token: 0x060001AB RID: 427 RVA: 0x0034D87C File Offset: 0x0034D87C
    [return: Nullable(2)]
    private static JsonConverter GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
    {
      for (int i = 0; i < options.Converters.Count; i++)
      {
        JsonConverter jsonConverter = options.Converters[i];
        if (jsonConverter != null && jsonConverter.CanConvert(type))
        {
          return CoreJsonContext.ExpandConverter(type, jsonConverter, options, false);
        }
      }
      return null;
    }

    // Token: 0x060001AC RID: 428 RVA: 0x0034D8C4 File Offset: 0x0034D8C4
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

    // Token: 0x060001AD RID: 429 RVA: 0x0034D92C File Offset: 0x0034D92C
    [return: Nullable(2)]
    public override JsonTypeInfo GetTypeInfo(Type type)
    {
      JsonTypeInfo jsonTypeInfo;
      base.Options.TryGetTypeInfo(type, out jsonTypeInfo);
      return jsonTypeInfo;
    }

    // Token: 0x060001AE RID: 430 RVA: 0x0034D94C File Offset: 0x0034D94C
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
      if (type == typeof(double?))
      {
        return this.Create_NullableDouble(options);
      }
      if (type == typeof(StoredAuthSession))
      {
        return this.Create_StoredAuthSession(options);
      }
      if (type == typeof(StoredPendingSignIn))
      {
        return this.Create_StoredPendingSignIn(options);
      }
      if (type == typeof(PaywallPayload))
      {
        return this.Create_PaywallPayload(options);
      }
      if (type == typeof(PaywallPlanOption))
      {
        return this.Create_PaywallPlanOption(options);
      }
      if (type == typeof(OnboardingProgress))
      {
        return this.Create_OnboardingProgress(options);
      }
      if (type == typeof(AppSettings))
      {
        return this.Create_AppSettings(options);
      }
      if (type == typeof(AppConfig))
      {
        return this.Create_AppConfig(options);
      }
      if (type == typeof(AppConfigLinks))
      {
        return this.Create_AppConfigLinks(options);
      }
      if (type == typeof(AppConfigResponse))
      {
        return this.Create_AppConfigResponse(options);
      }
      if (type == typeof(CheckoutRequest))
      {
        return this.Create_CheckoutRequest(options);
      }
      if (type == typeof(CheckoutResponse))
      {
        return this.Create_CheckoutResponse(options);
      }
      if (type == typeof(DeepgramTokenResponse))
      {
        return this.Create_DeepgramTokenResponse(options);
      }
      if (type == typeof(DeviceRegistrationRequest))
      {
        return this.Create_DeviceRegistrationRequest(options);
      }
      if (type == typeof(DeviceRegistrationStamp))
      {
        return this.Create_DeviceRegistrationStamp(options);
      }
      if (type == typeof(DeviceResponse))
      {
        return this.Create_DeviceResponse(options);
      }
      if (type == typeof(DevicesResponse))
      {
        return this.Create_DevicesResponse(options);
      }
      if (type == typeof(DiscoveryChannelRequest))
      {
        return this.Create_DiscoveryChannelRequest(options);
      }
      if (type == typeof(DiscoveryChannelResponse))
      {
        return this.Create_DiscoveryChannelResponse(options);
      }
      if (type == typeof(EarsTranscribeRequest))
      {
        return this.Create_EarsTranscribeRequest(options);
      }
      if (type == typeof(EarsTranscribeResponse))
      {
        return this.Create_EarsTranscribeResponse(options);
      }
      if (type == typeof(ErrorReport))
      {
        return this.Create_ErrorReport(options);
      }
      if (type == typeof(ImageSearchRequest))
      {
        return this.Create_ImageSearchRequest(options);
      }
      if (type == typeof(ImageSearchResponse))
      {
        return this.Create_ImageSearchResponse(options);
      }
      if (type == typeof(ImageSearchResult))
      {
        return this.Create_ImageSearchResult(options);
      }
      if (type == typeof(InterviewLine))
      {
        return this.Create_InterviewLine(options);
      }
      if (type == typeof(PlanSnapshot))
      {
        return this.Create_PlanSnapshot(options);
      }
      if (type == typeof(PlanSubscription))
      {
        return this.Create_PlanSubscription(options);
      }
      if (type == typeof(PlanUsage))
      {
        return this.Create_PlanUsage(options);
      }
      if (type == typeof(PlanUsageBucket))
      {
        return this.Create_PlanUsageBucket(options);
      }
      if (type == typeof(PortalResponse))
      {
        return this.Create_PortalResponse(options);
      }
      if (type == typeof(RealtimeSessionPromptContext))
      {
        return this.Create_RealtimeSessionPromptContext(options);
      }
      if (type == typeof(RealtimeSessionRequest))
      {
        return this.Create_RealtimeSessionRequest(options);
      }
      if (type == typeof(ReferralClaimRequest))
      {
        return this.Create_ReferralClaimRequest(options);
      }
      if (type == typeof(ReferralCodeRequest))
      {
        return this.Create_ReferralCodeRequest(options);
      }
      if (type == typeof(ReferralOutcomeResponse))
      {
        return this.Create_ReferralOutcomeResponse(options);
      }
      if (type == typeof(RegisteredDevice))
      {
        return this.Create_RegisteredDevice(options);
      }
      if (type == typeof(SaveMemoryRequest))
      {
        return this.Create_SaveMemoryRequest(options);
      }
      if (type == typeof(SaveMemoryResponse))
      {
        return this.Create_SaveMemoryResponse(options);
      }
      if (type == typeof(StarterPlanClicky))
      {
        return this.Create_StarterPlanClicky(options);
      }
      if (type == typeof(StarterPlanClickyInput))
      {
        return this.Create_StarterPlanClickyInput(options);
      }
      if (type == typeof(StarterPlanClickyRoutine))
      {
        return this.Create_StarterPlanClickyRoutine(options);
      }
      if (type == typeof(StarterPlanRequest))
      {
        return this.Create_StarterPlanRequest(options);
      }
      if (type == typeof(StarterPlanResponse))
      {
        return this.Create_StarterPlanResponse(options);
      }
      if (type == typeof(StarterPlanRoutine))
      {
        return this.Create_StarterPlanRoutine(options);
      }
      if (type == typeof(StarterPlanSuggestion))
      {
        return this.Create_StarterPlanSuggestion(options);
      }
      if (type == typeof(UserMemoryResponse))
      {
        return this.Create_UserMemoryResponse(options);
      }
      if (type == typeof(WebSearchRequest))
      {
        return this.Create_WebSearchRequest(options);
      }
      if (type == typeof(WebSearchResponse))
      {
        return this.Create_WebSearchResponse(options);
      }
      if (type == typeof(WebSearchResult))
      {
        return this.Create_WebSearchResult(options);
      }
      if (type == typeof(HotkeyBinding))
      {
        return this.Create_HotkeyBinding(options);
      }
      if (type == typeof(HotkeyBindings))
      {
        return this.Create_HotkeyBindings(options);
      }
      if (type == typeof(HotkeyModifiers))
      {
        return this.Create_HotkeyModifiers(options);
      }
      if (type == typeof(HotkeyTrigger))
      {
        return this.Create_HotkeyTrigger(options);
      }
      if (type == typeof(Dictionary<string, string>))
      {
        return this.Create_DictionaryStringString(options);
      }
      if (type == typeof(IReadOnlyList<PaywallPlanOption>))
      {
        return this.Create_IReadOnlyListPaywallPlanOption(options);
      }
      if (type == typeof(IReadOnlyList<ImageSearchResult>))
      {
        return this.Create_IReadOnlyListImageSearchResult(options);
      }
      if (type == typeof(IReadOnlyList<InterviewLine>))
      {
        return this.Create_IReadOnlyListInterviewLine(options);
      }
      if (type == typeof(IReadOnlyList<RegisteredDevice>))
      {
        return this.Create_IReadOnlyListRegisteredDevice(options);
      }
      if (type == typeof(IReadOnlyList<StarterPlanClicky>))
      {
        return this.Create_IReadOnlyListStarterPlanClicky(options);
      }
      if (type == typeof(IReadOnlyList<StarterPlanClickyInput>))
      {
        return this.Create_IReadOnlyListStarterPlanClickyInput(options);
      }
      if (type == typeof(IReadOnlyList<StarterPlanRoutine>))
      {
        return this.Create_IReadOnlyListStarterPlanRoutine(options);
      }
      if (type == typeof(IReadOnlyList<StarterPlanSuggestion>))
      {
        return this.Create_IReadOnlyListStarterPlanSuggestion(options);
      }
      if (type == typeof(IReadOnlyList<WebSearchResult>))
      {
        return this.Create_IReadOnlyListWebSearchResult(options);
      }
      if (type == typeof(IReadOnlyList<JsonElement>))
      {
        return this.Create_IReadOnlyListJsonElement(options);
      }
      if (type == typeof(IReadOnlyList<string>))
      {
        return this.Create_IReadOnlyListString(options);
      }
      if (type == typeof(List<string>))
      {
        return this.Create_ListString(options);
      }
      if (type == typeof(JsonElement))
      {
        return this.Create_JsonElement(options);
      }
      if (type == typeof(int))
      {
        return this.Create_Int32(options);
      }
      if (type == typeof(int?))
      {
        return this.Create_NullableInt32(options);
      }
      if (type == typeof(long))
      {
        return this.Create_Int64(options);
      }
      if (type == typeof(string))
      {
        return this.Create_String(options);
      }
      return null;
    }

    // Token: 0x04000086 RID: 134
    [Nullable(2)]
    private JsonTypeInfo<bool> _Boolean;

    // Token: 0x04000087 RID: 135
    [Nullable(2)]
    private JsonTypeInfo<bool?> _NullableBoolean;

    // Token: 0x04000088 RID: 136
    [Nullable(2)]
    private JsonTypeInfo<double> _Double;

    // Token: 0x04000089 RID: 137
    [Nullable(2)]
    private JsonTypeInfo<double?> _NullableDouble;

    // Token: 0x0400008A RID: 138
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StoredAuthSession> _StoredAuthSession;

    // Token: 0x0400008B RID: 139
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StoredPendingSignIn> _StoredPendingSignIn;

    // Token: 0x0400008C RID: 140
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PaywallPayload> _PaywallPayload;

    // Token: 0x0400008D RID: 141
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PaywallPlanOption> _PaywallPlanOption;

    // Token: 0x0400008E RID: 142
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<OnboardingProgress> _OnboardingProgress;

    // Token: 0x0400008F RID: 143
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<AppSettings> _AppSettings;

    // Token: 0x04000090 RID: 144
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<AppConfig> _AppConfig;

    // Token: 0x04000091 RID: 145
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<AppConfigLinks> _AppConfigLinks;

    // Token: 0x04000092 RID: 146
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<AppConfigResponse> _AppConfigResponse;

    // Token: 0x04000093 RID: 147
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<CheckoutRequest> _CheckoutRequest;

    // Token: 0x04000094 RID: 148
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<CheckoutResponse> _CheckoutResponse;

    // Token: 0x04000095 RID: 149
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DeepgramTokenResponse> _DeepgramTokenResponse;

    // Token: 0x04000096 RID: 150
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DeviceRegistrationRequest> _DeviceRegistrationRequest;

    // Token: 0x04000097 RID: 151
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DeviceRegistrationStamp> _DeviceRegistrationStamp;

    // Token: 0x04000098 RID: 152
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DeviceResponse> _DeviceResponse;

    // Token: 0x04000099 RID: 153
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DevicesResponse> _DevicesResponse;

    // Token: 0x0400009A RID: 154
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DiscoveryChannelRequest> _DiscoveryChannelRequest;

    // Token: 0x0400009B RID: 155
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<DiscoveryChannelResponse> _DiscoveryChannelResponse;

    // Token: 0x0400009C RID: 156
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<EarsTranscribeRequest> _EarsTranscribeRequest;

    // Token: 0x0400009D RID: 157
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<EarsTranscribeResponse> _EarsTranscribeResponse;

    // Token: 0x0400009E RID: 158
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ErrorReport> _ErrorReport;

    // Token: 0x0400009F RID: 159
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ImageSearchRequest> _ImageSearchRequest;

    // Token: 0x040000A0 RID: 160
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ImageSearchResponse> _ImageSearchResponse;

    // Token: 0x040000A1 RID: 161
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ImageSearchResult> _ImageSearchResult;

    // Token: 0x040000A2 RID: 162
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<InterviewLine> _InterviewLine;

    // Token: 0x040000A3 RID: 163
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PlanSnapshot> _PlanSnapshot;

    // Token: 0x040000A4 RID: 164
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PlanSubscription> _PlanSubscription;

    // Token: 0x040000A5 RID: 165
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PlanUsage> _PlanUsage;

    // Token: 0x040000A6 RID: 166
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PlanUsageBucket> _PlanUsageBucket;

    // Token: 0x040000A7 RID: 167
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<PortalResponse> _PortalResponse;

    // Token: 0x040000A8 RID: 168
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<RealtimeSessionPromptContext> _RealtimeSessionPromptContext;

    // Token: 0x040000A9 RID: 169
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<RealtimeSessionRequest> _RealtimeSessionRequest;

    // Token: 0x040000AA RID: 170
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ReferralClaimRequest> _ReferralClaimRequest;

    // Token: 0x040000AB RID: 171
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ReferralCodeRequest> _ReferralCodeRequest;

    // Token: 0x040000AC RID: 172
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<ReferralOutcomeResponse> _ReferralOutcomeResponse;

    // Token: 0x040000AD RID: 173
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<RegisteredDevice> _RegisteredDevice;

    // Token: 0x040000AE RID: 174
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<SaveMemoryRequest> _SaveMemoryRequest;

    // Token: 0x040000AF RID: 175
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<SaveMemoryResponse> _SaveMemoryResponse;

    // Token: 0x040000B0 RID: 176
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanClicky> _StarterPlanClicky;

    // Token: 0x040000B1 RID: 177
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanClickyInput> _StarterPlanClickyInput;

    // Token: 0x040000B2 RID: 178
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanClickyRoutine> _StarterPlanClickyRoutine;

    // Token: 0x040000B3 RID: 179
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanRequest> _StarterPlanRequest;

    // Token: 0x040000B4 RID: 180
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanResponse> _StarterPlanResponse;

    // Token: 0x040000B5 RID: 181
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanRoutine> _StarterPlanRoutine;

    // Token: 0x040000B6 RID: 182
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<StarterPlanSuggestion> _StarterPlanSuggestion;

    // Token: 0x040000B7 RID: 183
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<UserMemoryResponse> _UserMemoryResponse;

    // Token: 0x040000B8 RID: 184
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<WebSearchRequest> _WebSearchRequest;

    // Token: 0x040000B9 RID: 185
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<WebSearchResponse> _WebSearchResponse;

    // Token: 0x040000BA RID: 186
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<WebSearchResult> _WebSearchResult;

    // Token: 0x040000BB RID: 187
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<HotkeyBinding> _HotkeyBinding;

    // Token: 0x040000BC RID: 188
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<HotkeyBindings> _HotkeyBindings;

    // Token: 0x040000BD RID: 189
    [Nullable(2)]
    private JsonTypeInfo<HotkeyModifiers> _HotkeyModifiers;

    // Token: 0x040000BE RID: 190
    [Nullable(2)]
    private JsonTypeInfo<HotkeyTrigger> _HotkeyTrigger;

    // Token: 0x040000BF RID: 191
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    private JsonTypeInfo<Dictionary<string, string>> _DictionaryStringString;

    // Token: 0x040000C0 RID: 192
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<PaywallPlanOption>> _IReadOnlyListPaywallPlanOption;

    // Token: 0x040000C1 RID: 193
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<ImageSearchResult>> _IReadOnlyListImageSearchResult;

    // Token: 0x040000C2 RID: 194
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<InterviewLine>> _IReadOnlyListInterviewLine;

    // Token: 0x040000C3 RID: 195
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<RegisteredDevice>> _IReadOnlyListRegisteredDevice;

    // Token: 0x040000C4 RID: 196
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<StarterPlanClicky>> _IReadOnlyListStarterPlanClicky;

    // Token: 0x040000C5 RID: 197
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<StarterPlanClickyInput>> _IReadOnlyListStarterPlanClickyInput;

    // Token: 0x040000C6 RID: 198
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<StarterPlanRoutine>> _IReadOnlyListStarterPlanRoutine;

    // Token: 0x040000C7 RID: 199
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<StarterPlanSuggestion>> _IReadOnlyListStarterPlanSuggestion;

    // Token: 0x040000C8 RID: 200
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<WebSearchResult>> _IReadOnlyListWebSearchResult;

    // Token: 0x040000C9 RID: 201
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<IReadOnlyList<JsonElement>> _IReadOnlyListJsonElement;

    // Token: 0x040000CA RID: 202
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<IReadOnlyList<string>> _IReadOnlyListString;

    // Token: 0x040000CB RID: 203
    [Nullable(new byte[] { 2, 1, 1 })]
    private JsonTypeInfo<List<string>> _ListString;

    // Token: 0x040000CC RID: 204
    [Nullable(2)]
    private JsonTypeInfo<JsonElement> _JsonElement;

    // Token: 0x040000CD RID: 205
    [Nullable(2)]
    private JsonTypeInfo<int> _Int32;

    // Token: 0x040000CE RID: 206
    [Nullable(2)]
    private JsonTypeInfo<int?> _NullableInt32;

    // Token: 0x040000CF RID: 207
    [Nullable(2)]
    private JsonTypeInfo<long> _Int64;

    // Token: 0x040000D0 RID: 208
    [Nullable(new byte[] { 2, 1 })]
    private JsonTypeInfo<string> _String;

    // Token: 0x040000D1 RID: 209
    private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      NumberHandling = JsonNumberHandling.AllowReadingFromString,
      PropertyNameCaseInsensitive = true,
      PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    // Token: 0x040000D2 RID: 210
    private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    // Token: 0x040000D5 RID: 213
    private static readonly JsonEncodedText PropName_access_token = JsonEncodedText.Encode("access_token", null);

    // Token: 0x040000D6 RID: 214
    private static readonly JsonEncodedText PropName_refresh_token = JsonEncodedText.Encode("refresh_token", null);

    // Token: 0x040000D7 RID: 215
    private static readonly JsonEncodedText PropName_user_id = JsonEncodedText.Encode("user_id", null);

    // Token: 0x040000D8 RID: 216
    private static readonly JsonEncodedText PropName_email = JsonEncodedText.Encode("email", null);

    // Token: 0x040000D9 RID: 217
    private static readonly JsonEncodedText PropName_display_name = JsonEncodedText.Encode("display_name", null);

    // Token: 0x040000DA RID: 218
    private static readonly JsonEncodedText PropName_avatar_url = JsonEncodedText.Encode("avatar_url", null);

    // Token: 0x040000DB RID: 219
    private static readonly JsonEncodedText PropName_signup_date_epoch_seconds = JsonEncodedText.Encode("signup_date_epoch_seconds", null);

    // Token: 0x040000DC RID: 220
    private static readonly JsonEncodedText PropName_state = JsonEncodedText.Encode("state", null);

    // Token: 0x040000DD RID: 221
    private static readonly JsonEncodedText PropName_started_at_epoch_seconds = JsonEncodedText.Encode("started_at_epoch_seconds", null);

    // Token: 0x040000DE RID: 222
    private static readonly JsonEncodedText PropName_reason = JsonEncodedText.Encode("reason", null);

    // Token: 0x040000DF RID: 223
    private static readonly JsonEncodedText PropName_surface = JsonEncodedText.Encode("surface", null);

    // Token: 0x040000E0 RID: 224
    private static readonly JsonEncodedText PropName_title = JsonEncodedText.Encode("title", null);

    // Token: 0x040000E1 RID: 225
    private static readonly JsonEncodedText PropName_body = JsonEncodedText.Encode("body", null);

    // Token: 0x040000E2 RID: 226
    private static readonly JsonEncodedText PropName_cta_label = JsonEncodedText.Encode("cta_label", null);

    // Token: 0x040000E3 RID: 227
    private static readonly JsonEncodedText PropName_price_display = JsonEncodedText.Encode("price_display", null);

    // Token: 0x040000E4 RID: 228
    private static readonly JsonEncodedText PropName_current_usage = JsonEncodedText.Encode("current_usage", null);

    // Token: 0x040000E5 RID: 229
    private static readonly JsonEncodedText PropName_limit = JsonEncodedText.Encode("limit", null);

    // Token: 0x040000E6 RID: 230
    private static readonly JsonEncodedText PropName_reset_at = JsonEncodedText.Encode("reset_at", null);

    // Token: 0x040000E7 RID: 231
    private static readonly JsonEncodedText PropName_plans = JsonEncodedText.Encode("plans", null);

    // Token: 0x040000E8 RID: 232
    private static readonly JsonEncodedText PropName_yearly_savings_label = JsonEncodedText.Encode("yearly_savings_label", null);

    // Token: 0x040000E9 RID: 233
    private static readonly JsonEncodedText PropName_default_interval = JsonEncodedText.Encode("default_interval", null);

    // Token: 0x040000EA RID: 234
    private static readonly JsonEncodedText PropName_key = JsonEncodedText.Encode("key", null);

    // Token: 0x040000EB RID: 235
    private static readonly JsonEncodedText PropName_name = JsonEncodedText.Encode("name", null);

    // Token: 0x040000EC RID: 236
    private static readonly JsonEncodedText PropName_agents_cap = JsonEncodedText.Encode("agents_cap", null);

    // Token: 0x040000ED RID: 237
    private static readonly JsonEncodedText PropName_tagline = JsonEncodedText.Encode("tagline", null);

    // Token: 0x040000EE RID: 238
    private static readonly JsonEncodedText PropName_recommended = JsonEncodedText.Encode("recommended", null);

    // Token: 0x040000EF RID: 239
    private static readonly JsonEncodedText PropName_yearly_price_display = JsonEncodedText.Encode("yearly_price_display", null);

    // Token: 0x040000F0 RID: 240
    private static readonly JsonEncodedText PropName_yearly_billed_summary = JsonEncodedText.Encode("yearly_billed_summary", null);

    // Token: 0x040000F1 RID: 241
    private static readonly JsonEncodedText PropName_pre_sign_in_intro_dismissed = JsonEncodedText.Encode("pre_sign_in_intro_dismissed", null);

    // Token: 0x040000F2 RID: 242
    private static readonly JsonEncodedText PropName_has_acknowledged_permission_intro = JsonEncodedText.Encode("has_acknowledged_permission_intro", null);

    // Token: 0x040000F3 RID: 243
    private static readonly JsonEncodedText PropName_has_completed_onboarding = JsonEncodedText.Encode("has_completed_onboarding", null);

    // Token: 0x040000F4 RID: 244
    private static readonly JsonEncodedText PropName_tutorial_completed_at_epoch_seconds = JsonEncodedText.Encode("tutorial_completed_at_epoch_seconds", null);

    // Token: 0x040000F5 RID: 245
    private static readonly JsonEncodedText PropName_discovery_channel = JsonEncodedText.Encode("discovery_channel", null);

    // Token: 0x040000F6 RID: 246
    private static readonly JsonEncodedText PropName_discovery_channel_other = JsonEncodedText.Encode("discovery_channel_other", null);

    // Token: 0x040000F7 RID: 247
    private static readonly JsonEncodedText PropName_discovery_channel_synced = JsonEncodedText.Encode("discovery_channel_synced", null);

    // Token: 0x040000F8 RID: 248
    private static readonly JsonEncodedText PropName_squad_pack_id = JsonEncodedText.Encode("squad_pack_id", null);

    // Token: 0x040000F9 RID: 249
    private static readonly JsonEncodedText PropName_cursor_color_hex = JsonEncodedText.Encode("cursor_color_hex", null);

    // Token: 0x040000FA RID: 250
    private static readonly JsonEncodedText PropName_squad_member_id = JsonEncodedText.Encode("squad_member_id", null);

    // Token: 0x040000FB RID: 251
    private static readonly JsonEncodedText PropName_voice_identifier = JsonEncodedText.Encode("voice_identifier", null);

    // Token: 0x040000FC RID: 252
    private static readonly JsonEncodedText PropName_speech_speed = JsonEncodedText.Encode("speech_speed", null);

    // Token: 0x040000FD RID: 253
    private static readonly JsonEncodedText PropName_voice_language_code = JsonEncodedText.Encode("voice_language_code", null);

    // Token: 0x040000FE RID: 254
    private static readonly JsonEncodedText PropName_screen_highlight_enabled = JsonEncodedText.Encode("screen_highlight_enabled", null);

    // Token: 0x040000FF RID: 255
    private static readonly JsonEncodedText PropName_cursor_docked = JsonEncodedText.Encode("cursor_docked", null);

    // Token: 0x04000100 RID: 256
    private static readonly JsonEncodedText PropName_shake_to_detach_enabled = JsonEncodedText.Encode("shake_to_detach_enabled", null);

    // Token: 0x04000101 RID: 257
    private static readonly JsonEncodedText PropName_launch_at_login = JsonEncodedText.Encode("launch_at_login", null);

    // Token: 0x04000102 RID: 258
    private static readonly JsonEncodedText PropName_show_in_taskbar = JsonEncodedText.Encode("show_in_taskbar", null);

    // Token: 0x04000103 RID: 259
    private static readonly JsonEncodedText PropName_typed_turns_respond_with_voice = JsonEncodedText.Encode("typed_turns_respond_with_voice", null);

    // Token: 0x04000104 RID: 260
    private static readonly JsonEncodedText PropName_dictation_language_code = JsonEncodedText.Encode("dictation_language_code", null);

    // Token: 0x04000105 RID: 261
    private static readonly JsonEncodedText PropName_dictation_specific_language_code = JsonEncodedText.Encode("dictation_specific_language_code", null);

    // Token: 0x04000106 RID: 262
    private static readonly JsonEncodedText PropName_dictation_last_completed_at_epoch_seconds = JsonEncodedText.Encode("dictation_last_completed_at_epoch_seconds", null);

    // Token: 0x04000107 RID: 263
    private static readonly JsonEncodedText PropName_dictation_custom_words = JsonEncodedText.Encode("dictation_custom_words", null);

    // Token: 0x04000108 RID: 264
    private static readonly JsonEncodedText PropName_dictation_claude_seeded = JsonEncodedText.Encode("dictation_claude_seeded", null);

    // Token: 0x04000109 RID: 265
    private static readonly JsonEncodedText PropName_dictation_account_name_seeded = JsonEncodedText.Encode("dictation_account_name_seeded", null);

    // Token: 0x0400010A RID: 266
    private static readonly JsonEncodedText PropName_discovery_channel_pending_sync = JsonEncodedText.Encode("discovery_channel_pending_sync", null);

    // Token: 0x0400010B RID: 267
    private static readonly JsonEncodedText PropName_microphone_device_name = JsonEncodedText.Encode("microphone_device_name", null);

    // Token: 0x0400010C RID: 268
    private static readonly JsonEncodedText PropName_agent_announcements_voice_enabled = JsonEncodedText.Encode("agent_announcements_voice_enabled", null);

    // Token: 0x0400010D RID: 269
    private static readonly JsonEncodedText PropName_document_access_notice_show_count = JsonEncodedText.Encode("document_access_notice_show_count", null);

    // Token: 0x0400010E RID: 270
    private static readonly JsonEncodedText PropName_document_access_notice_suppressed = JsonEncodedText.Encode("document_access_notice_suppressed", null);

    // Token: 0x0400010F RID: 271
    private static readonly JsonEncodedText PropName_onboarding = JsonEncodedText.Encode("onboarding", null);

    // Token: 0x04000110 RID: 272
    private static readonly JsonEncodedText PropName_hotkeys = JsonEncodedText.Encode("hotkeys", null);

    // Token: 0x04000111 RID: 273
    private static readonly JsonEncodedText PropName_microphone_device_id = JsonEncodedText.Encode("microphone_device_id", null);

    // Token: 0x04000112 RID: 274
    private static readonly JsonEncodedText PropName_output_device_id = JsonEncodedText.Encode("output_device_id", null);

    // Token: 0x04000113 RID: 275
    private static readonly JsonEncodedText PropName_drawing_sounds_enabled = JsonEncodedText.Encode("drawing_sounds_enabled", null);

    // Token: 0x04000114 RID: 276
    private static readonly JsonEncodedText PropName_drawing_voice_captions = JsonEncodedText.Encode("drawing_voice_captions", null);

    // Token: 0x04000115 RID: 277
    private static readonly JsonEncodedText PropName_keep_drawings_in_chat = JsonEncodedText.Encode("keep_drawings_in_chat", null);

    // Token: 0x04000116 RID: 278
    private static readonly JsonEncodedText PropName_drawing_pen_sounds_enabled = JsonEncodedText.Encode("drawing_pen_sounds_enabled", null);

    // Token: 0x04000117 RID: 279
    private static readonly JsonEncodedText PropName_drawing_java_script_jit = JsonEncodedText.Encode("drawing_java_script_jit", null);

    // Token: 0x04000118 RID: 280
    private static readonly JsonEncodedText PropName_drawing_subtitles = JsonEncodedText.Encode("drawing_subtitles", null);

    // Token: 0x04000119 RID: 281
    private static readonly JsonEncodedText PropName_save_drawings = JsonEncodedText.Encode("save_drawings", null);

    // Token: 0x0400011A RID: 282
    private static readonly JsonEncodedText PropName_show_agent_labels_in_activity_strip = JsonEncodedText.Encode("show_agent_labels_in_activity_strip", null);

    // Token: 0x0400011B RID: 283
    private static readonly JsonEncodedText PropName_show_in_screen_recordings = JsonEncodedText.Encode("show_in_screen_recordings", null);

    // Token: 0x0400011C RID: 284
    private static readonly JsonEncodedText PropName_agent_folder_path = JsonEncodedText.Encode("agent_folder_path", null);

    // Token: 0x0400011D RID: 285
    private static readonly JsonEncodedText PropName_obsidian_vault_path = JsonEncodedText.Encode("obsidian_vault_path", null);

    // Token: 0x0400011E RID: 286
    private static readonly JsonEncodedText PropName_agent_auto_approve_extra_usage = JsonEncodedText.Encode("agent_auto_approve_extra_usage", null);

    // Token: 0x0400011F RID: 287
    private static readonly JsonEncodedText PropName_agent_always_approve_file_uploads = JsonEncodedText.Encode("agent_always_approve_file_uploads", null);

    // Token: 0x04000120 RID: 288
    private static readonly JsonEncodedText PropName_agent_cursor_text_announcements_enabled = JsonEncodedText.Encode("agent_cursor_text_announcements_enabled", null);

    // Token: 0x04000121 RID: 289
    private static readonly JsonEncodedText PropName_one_brain_model = JsonEncodedText.Encode("one_brain_model", null);

    // Token: 0x04000122 RID: 290
    private static readonly JsonEncodedText PropName_one_brain_reads_apps = JsonEncodedText.Encode("one_brain_reads_apps", null);

    // Token: 0x04000123 RID: 291
    private static readonly JsonEncodedText PropName_one_brain_keeps_screens_in_history = JsonEncodedText.Encode("one_brain_keeps_screens_in_history", null);

    // Token: 0x04000124 RID: 292
    private static readonly JsonEncodedText PropName_show_one_brain_stats = JsonEncodedText.Encode("show_one_brain_stats", null);

    // Token: 0x04000125 RID: 293
    private static readonly JsonEncodedText PropName_morning_paper_enabled = JsonEncodedText.Encode("morning_paper_enabled", null);

    // Token: 0x04000126 RID: 294
    private static readonly JsonEncodedText PropName_stream_mode = JsonEncodedText.Encode("stream_mode", null);

    // Token: 0x04000127 RID: 295
    private static readonly JsonEncodedText PropName_developer_chat_models = JsonEncodedText.Encode("developer_chat_models", null);

    // Token: 0x04000128 RID: 296
    private static readonly JsonEncodedText PropName_has_shown_paywall_intro_video = JsonEncodedText.Encode("has_shown_paywall_intro_video", null);

    // Token: 0x04000129 RID: 297
    private static readonly JsonEncodedText PropName_proactive_agent_suggestions_enabled = JsonEncodedText.Encode("proactive_agent_suggestions_enabled", null);

    // Token: 0x0400012A RID: 298
    private static readonly JsonEncodedText PropName_upcoming_meetings_enabled = JsonEncodedText.Encode("upcoming_meetings_enabled", null);

    // Token: 0x0400012B RID: 299
    private static readonly JsonEncodedText PropName_onboarding_language_step_enabled = JsonEncodedText.Encode("onboarding_language_step_enabled", null);

    // Token: 0x0400012C RID: 300
    private static readonly JsonEncodedText PropName_remote_tasks_enabled = JsonEncodedText.Encode("remote_tasks_enabled", null);

    // Token: 0x0400012D RID: 301
    private static readonly JsonEncodedText PropName_proactive_debug_chord_enabled = JsonEncodedText.Encode("proactive_debug_chord_enabled", null);

    // Token: 0x0400012E RID: 302
    private static readonly JsonEncodedText PropName_proactive_debug_cadence = JsonEncodedText.Encode("proactive_debug_cadence", null);

    // Token: 0x0400012F RID: 303
    private static readonly JsonEncodedText PropName_live_notes_enabled = JsonEncodedText.Encode("live_notes_enabled", null);

    // Token: 0x04000130 RID: 304
    private static readonly JsonEncodedText PropName_live_notes_stream_patches = JsonEncodedText.Encode("live_notes_stream_patches", null);

    // Token: 0x04000131 RID: 305
    private static readonly JsonEncodedText PropName_links = JsonEncodedText.Encode("links", null);

    // Token: 0x04000132 RID: 306
    private static readonly JsonEncodedText PropName_featureRequest = JsonEncodedText.Encode("featureRequest", null);

    // Token: 0x04000133 RID: 307
    private static readonly JsonEncodedText PropName_communityReddit = JsonEncodedText.Encode("communityReddit", null);

    // Token: 0x04000134 RID: 308
    private static readonly JsonEncodedText PropName_communityWhatsApp = JsonEncodedText.Encode("communityWhatsApp", null);

    // Token: 0x04000135 RID: 309
    private static readonly JsonEncodedText PropName_changelog = JsonEncodedText.Encode("changelog", null);

    // Token: 0x04000136 RID: 310
    private static readonly JsonEncodedText PropName_appConfig = JsonEncodedText.Encode("appConfig", null);

    // Token: 0x04000137 RID: 311
    private static readonly JsonEncodedText PropName_plan = JsonEncodedText.Encode("plan", null);

    // Token: 0x04000138 RID: 312
    private static readonly JsonEncodedText PropName_interval = JsonEncodedText.Encode("interval", null);

    // Token: 0x04000139 RID: 313
    private static readonly JsonEncodedText PropName_checkout_ui = JsonEncodedText.Encode("checkout_ui", null);

    // Token: 0x0400013A RID: 314
    private static readonly JsonEncodedText PropName_discount_code = JsonEncodedText.Encode("discount_code", null);

    // Token: 0x0400013B RID: 315
    private static readonly JsonEncodedText PropName_url = JsonEncodedText.Encode("url", null);

    // Token: 0x0400013C RID: 316
    private static readonly JsonEncodedText PropName_discount_code_applied = JsonEncodedText.Encode("discount_code_applied", null);

    // Token: 0x0400013D RID: 317
    private static readonly JsonEncodedText PropName_model = JsonEncodedText.Encode("model", null);

    // Token: 0x0400013E RID: 318
    private static readonly JsonEncodedText PropName_receipt = JsonEncodedText.Encode("receipt", null);

    // Token: 0x0400013F RID: 319
    private static readonly JsonEncodedText PropName_capabilities = JsonEncodedText.Encode("capabilities", null);

    // Token: 0x04000140 RID: 320
    private static readonly JsonEncodedText PropName_fingerprint = JsonEncodedText.Encode("fingerprint", null);

    // Token: 0x04000141 RID: 321
    private static readonly JsonEncodedText PropName_registered_at_epoch_seconds = JsonEncodedText.Encode("registered_at_epoch_seconds", null);

    // Token: 0x04000142 RID: 322
    private static readonly JsonEncodedText PropName_device = JsonEncodedText.Encode("device", null);

    // Token: 0x04000143 RID: 323
    private static readonly JsonEncodedText PropName_devices = JsonEncodedText.Encode("devices", null);

    // Token: 0x04000144 RID: 324
    private static readonly JsonEncodedText PropName_audio_wav_base64 = JsonEncodedText.Encode("audio_wav_base64", null);

    // Token: 0x04000145 RID: 325
    private static readonly JsonEncodedText PropName_language = JsonEncodedText.Encode("language", null);

    // Token: 0x04000146 RID: 326
    private static readonly JsonEncodedText PropName_text = JsonEncodedText.Encode("text", null);

    // Token: 0x04000147 RID: 327
    private static readonly JsonEncodedText PropName_stage = JsonEncodedText.Encode("stage", null);

    // Token: 0x04000148 RID: 328
    private static readonly JsonEncodedText PropName_error_kind = JsonEncodedText.Encode("error_kind", null);

    // Token: 0x04000149 RID: 329
    private static readonly JsonEncodedText PropName_error_message = JsonEncodedText.Encode("error_message", null);

    // Token: 0x0400014A RID: 330
    private static readonly JsonEncodedText PropName_user_facing_message = JsonEncodedText.Encode("user_facing_message", null);

    // Token: 0x0400014B RID: 331
    private static readonly JsonEncodedText PropName_app_version = JsonEncodedText.Encode("app_version", null);

    // Token: 0x0400014C RID: 332
    private static readonly JsonEncodedText PropName_error_code = JsonEncodedText.Encode("error_code", null);

    // Token: 0x0400014D RID: 333
    private static readonly JsonEncodedText PropName_error_domain = JsonEncodedText.Encode("error_domain", null);

    // Token: 0x0400014E RID: 334
    private static readonly JsonEncodedText PropName_supabase_user_id = JsonEncodedText.Encode("supabase_user_id", null);

    // Token: 0x0400014F RID: 335
    private static readonly JsonEncodedText PropName_user_email = JsonEncodedText.Encode("user_email", null);

    // Token: 0x04000150 RID: 336
    private static readonly JsonEncodedText PropName_query = JsonEncodedText.Encode("query", null);

    // Token: 0x04000151 RID: 337
    private static readonly JsonEncodedText PropName_subject_photo = JsonEncodedText.Encode("subject_photo", null);

    // Token: 0x04000152 RID: 338
    private static readonly JsonEncodedText PropName_page_url = JsonEncodedText.Encode("page_url", null);

    // Token: 0x04000153 RID: 339
    private static readonly JsonEncodedText PropName_images = JsonEncodedText.Encode("images", null);

    // Token: 0x04000154 RID: 340
    private static readonly JsonEncodedText PropName_thumbnail_url = JsonEncodedText.Encode("thumbnail_url", null);

    // Token: 0x04000155 RID: 341
    private static readonly JsonEncodedText PropName_full_url = JsonEncodedText.Encode("full_url", null);

    // Token: 0x04000156 RID: 342
    private static readonly JsonEncodedText PropName_source_url = JsonEncodedText.Encode("source_url", null);

    // Token: 0x04000157 RID: 343
    private static readonly JsonEncodedText PropName_role = JsonEncodedText.Encode("role", null);

    // Token: 0x04000158 RID: 344
    private static readonly JsonEncodedText PropName_usage = JsonEncodedText.Encode("usage", null);

    // Token: 0x04000159 RID: 345
    private static readonly JsonEncodedText PropName_billing_enforcement = JsonEncodedText.Encode("billing_enforcement", null);

    // Token: 0x0400015A RID: 346
    private static readonly JsonEncodedText PropName_tier = JsonEncodedText.Encode("tier", null);

    // Token: 0x0400015B RID: 347
    private static readonly JsonEncodedText PropName_tier_source = JsonEncodedText.Encode("tier_source", null);

    // Token: 0x0400015C RID: 348
    private static readonly JsonEncodedText PropName_team_name = JsonEncodedText.Encode("team_name", null);

    // Token: 0x0400015D RID: 349
    private static readonly JsonEncodedText PropName_subscription = JsonEncodedText.Encode("subscription", null);

    // Token: 0x0400015E RID: 350
    private static readonly JsonEncodedText PropName_unlimited_agents_cap = JsonEncodedText.Encode("unlimited_agents_cap", null);

    // Token: 0x0400015F RID: 351
    private static readonly JsonEncodedText PropName_pro_agents_cap = JsonEncodedText.Encode("pro_agents_cap", null);

    // Token: 0x04000160 RID: 352
    private static readonly JsonEncodedText PropName_max_agents_cap = JsonEncodedText.Encode("max_agents_cap", null);

    // Token: 0x04000161 RID: 353
    private static readonly JsonEncodedText PropName_pro_yearly_available = JsonEncodedText.Encode("pro_yearly_available", null);

    // Token: 0x04000162 RID: 354
    private static readonly JsonEncodedText PropName_max_yearly_available = JsonEncodedText.Encode("max_yearly_available", null);

    // Token: 0x04000163 RID: 355
    private static readonly JsonEncodedText PropName_is_fail_open_fallback = JsonEncodedText.Encode("is_fail_open_fallback", null);

    // Token: 0x04000164 RID: 356
    private static readonly JsonEncodedText PropName_status = JsonEncodedText.Encode("status", null);

    // Token: 0x04000165 RID: 357
    private static readonly JsonEncodedText PropName_cancel_at_period_end = JsonEncodedText.Encode("cancel_at_period_end", null);

    // Token: 0x04000166 RID: 358
    private static readonly JsonEncodedText PropName_current_period_start = JsonEncodedText.Encode("current_period_start", null);

    // Token: 0x04000167 RID: 359
    private static readonly JsonEncodedText PropName_current_period_end = JsonEncodedText.Encode("current_period_end", null);

    // Token: 0x04000168 RID: 360
    private static readonly JsonEncodedText PropName_product_id = JsonEncodedText.Encode("product_id", null);

    // Token: 0x04000169 RID: 361
    private static readonly JsonEncodedText PropName_cancel_at = JsonEncodedText.Encode("cancel_at", null);

    // Token: 0x0400016A RID: 362
    private static readonly JsonEncodedText PropName_billing_interval = JsonEncodedText.Encode("billing_interval", null);

    // Token: 0x0400016B RID: 363
    private static readonly JsonEncodedText PropName_messages = JsonEncodedText.Encode("messages", null);

    // Token: 0x0400016C RID: 364
    private static readonly JsonEncodedText PropName_agents = JsonEncodedText.Encode("agents", null);

    // Token: 0x0400016D RID: 365
    private static readonly JsonEncodedText PropName_dictation = JsonEncodedText.Encode("dictation", null);

    // Token: 0x0400016E RID: 366
    private static readonly JsonEncodedText PropName_window_resets_at = JsonEncodedText.Encode("window_resets_at", null);

    // Token: 0x0400016F RID: 367
    private static readonly JsonEncodedText PropName_connected_integrations = JsonEncodedText.Encode("connected_integrations", null);

    // Token: 0x04000170 RID: 368
    private static readonly JsonEncodedText PropName_needs_reconnect_integrations = JsonEncodedText.Encode("needs_reconnect_integrations", null);

    // Token: 0x04000171 RID: 369
    private static readonly JsonEncodedText PropName_prior_message_count = JsonEncodedText.Encode("prior_message_count", null);

    // Token: 0x04000172 RID: 370
    private static readonly JsonEncodedText PropName_time_zone = JsonEncodedText.Encode("time_zone", null);

    // Token: 0x04000173 RID: 371
    private static readonly JsonEncodedText PropName_current_local_datetime = JsonEncodedText.Encode("current_local_datetime", null);

    // Token: 0x04000174 RID: 372
    private static readonly JsonEncodedText PropName_voice_gender = JsonEncodedText.Encode("voice_gender", null);

    // Token: 0x04000175 RID: 373
    private static readonly JsonEncodedText PropName_active_skills = JsonEncodedText.Encode("active_skills", null);

    // Token: 0x04000176 RID: 374
    private static readonly JsonEncodedText PropName_dictation_hold_shortcut = JsonEncodedText.Encode("dictation_hold_shortcut", null);

    // Token: 0x04000177 RID: 375
    private static readonly JsonEncodedText PropName_voice = JsonEncodedText.Encode("voice", null);

    // Token: 0x04000178 RID: 376
    private static readonly JsonEncodedText PropName_speed = JsonEncodedText.Encode("speed", null);

    // Token: 0x04000179 RID: 377
    private static readonly JsonEncodedText PropName_prompt_context = JsonEncodedText.Encode("prompt_context", null);

    // Token: 0x0400017A RID: 378
    private static readonly JsonEncodedText PropName_client_capabilities = JsonEncodedText.Encode("client_capabilities", null);

    // Token: 0x0400017B RID: 379
    private static readonly JsonEncodedText PropName_mouth_provider = JsonEncodedText.Encode("mouth_provider", null);

    // Token: 0x0400017C RID: 380
    private static readonly JsonEncodedText PropName_languages = JsonEncodedText.Encode("languages", null);

    // Token: 0x0400017D RID: 381
    private static readonly JsonEncodedText PropName_code = JsonEncodedText.Encode("code", null);

    // Token: 0x0400017E RID: 382
    private static readonly JsonEncodedText PropName_source = JsonEncodedText.Encode("source", null);

    // Token: 0x0400017F RID: 383
    private static readonly JsonEncodedText PropName_outcome = JsonEncodedText.Encode("outcome", null);

    // Token: 0x04000180 RID: 384
    private static readonly JsonEncodedText PropName_message = JsonEncodedText.Encode("message", null);

    // Token: 0x04000181 RID: 385
    private static readonly JsonEncodedText PropName_discount_percent = JsonEncodedText.Encode("discount_percent", null);

    // Token: 0x04000182 RID: 386
    private static readonly JsonEncodedText PropName_share_url = JsonEncodedText.Encode("share_url", null);

    // Token: 0x04000183 RID: 387
    private static readonly JsonEncodedText PropName_share_display_text = JsonEncodedText.Encode("share_display_text", null);

    // Token: 0x04000184 RID: 388
    private static readonly JsonEncodedText PropName_id = JsonEncodedText.Encode("id", null);

    // Token: 0x04000185 RID: 389
    private static readonly JsonEncodedText PropName_is_this_device = JsonEncodedText.Encode("is_this_device", null);

    // Token: 0x04000186 RID: 390
    private static readonly JsonEncodedText PropName_platform = JsonEncodedText.Encode("platform", null);

    // Token: 0x04000187 RID: 391
    private static readonly JsonEncodedText PropName_os_version = JsonEncodedText.Encode("os_version", null);

    // Token: 0x04000188 RID: 392
    private static readonly JsonEncodedText PropName_app_build = JsonEncodedText.Encode("app_build", null);

    // Token: 0x04000189 RID: 393
    private static readonly JsonEncodedText PropName_first_seen_at = JsonEncodedText.Encode("first_seen_at", null);

    // Token: 0x0400018A RID: 394
    private static readonly JsonEncodedText PropName_last_seen_at = JsonEncodedText.Encode("last_seen_at", null);

    // Token: 0x0400018B RID: 395
    private static readonly JsonEncodedText PropName_last_active_at = JsonEncodedText.Encode("last_active_at", null);

    // Token: 0x0400018C RID: 396
    private static readonly JsonEncodedText PropName_messages_last14_days = JsonEncodedText.Encode("messages_last14_days", null);

    // Token: 0x0400018D RID: 397
    private static readonly JsonEncodedText PropName_memory = JsonEncodedText.Encode("memory", null);

    // Token: 0x0400018E RID: 398
    private static readonly JsonEncodedText PropName_saved = JsonEncodedText.Encode("saved", null);

    // Token: 0x0400018F RID: 399
    private static readonly JsonEncodedText PropName_error = JsonEncodedText.Encode("error", null);

    // Token: 0x04000190 RID: 400
    private static readonly JsonEncodedText PropName_description = JsonEncodedText.Encode("description", null);

    // Token: 0x04000191 RID: 401
    private static readonly JsonEncodedText PropName_intro_messages = JsonEncodedText.Encode("intro_messages", null);

    // Token: 0x04000192 RID: 402
    private static readonly JsonEncodedText PropName_reveal_line = JsonEncodedText.Encode("reveal_line", null);

    // Token: 0x04000193 RID: 403
    private static readonly JsonEncodedText PropName_first_asks = JsonEncodedText.Encode("first_asks", null);

    // Token: 0x04000194 RID: 404
    private static readonly JsonEncodedText PropName_routine = JsonEncodedText.Encode("routine", null);

    // Token: 0x04000195 RID: 405
    private static readonly JsonEncodedText PropName_slug = JsonEncodedText.Encode("slug", null);

    // Token: 0x04000196 RID: 406
    private static readonly JsonEncodedText PropName_routines = JsonEncodedText.Encode("routines", null);

    // Token: 0x04000197 RID: 407
    private static readonly JsonEncodedText PropName_thread_ids = JsonEncodedText.Encode("thread_ids", null);

    // Token: 0x04000198 RID: 408
    private static readonly JsonEncodedText PropName_task_prompt = JsonEncodedText.Encode("task_prompt", null);

    // Token: 0x04000199 RID: 409
    private static readonly JsonEncodedText PropName_interval_minutes = JsonEncodedText.Encode("interval_minutes", null);

    // Token: 0x0400019A RID: 410
    private static readonly JsonEncodedText PropName_interview = JsonEncodedText.Encode("interview", null);

    // Token: 0x0400019B RID: 411
    private static readonly JsonEncodedText PropName_available_integrations = JsonEncodedText.Encode("available_integrations", null);

    // Token: 0x0400019C RID: 412
    private static readonly JsonEncodedText PropName_clickys = JsonEncodedText.Encode("clickys", null);

    // Token: 0x0400019D RID: 413
    private static readonly JsonEncodedText PropName_first_asks_for_clicky = JsonEncodedText.Encode("first_asks_for_clicky", null);

    // Token: 0x0400019E RID: 414
    private static readonly JsonEncodedText PropName_interview_saved = JsonEncodedText.Encode("interview_saved", null);

    // Token: 0x0400019F RID: 415
    private static readonly JsonEncodedText PropName_jobs = JsonEncodedText.Encode("jobs", null);

    // Token: 0x040001A0 RID: 416
    private static readonly JsonEncodedText PropName_suggested_agents = JsonEncodedText.Encode("suggested_agents", null);

    // Token: 0x040001A1 RID: 417
    private static readonly JsonEncodedText PropName_agent_familiarity = JsonEncodedText.Encode("agent_familiarity", null);

    // Token: 0x040001A2 RID: 418
    private static readonly JsonEncodedText PropName_goal_line = JsonEncodedText.Encode("goal_line", null);

    // Token: 0x040001A3 RID: 419
    private static readonly JsonEncodedText PropName_every_minutes = JsonEncodedText.Encode("every_minutes", null);

    // Token: 0x040001A4 RID: 420
    private static readonly JsonEncodedText PropName_agent_prompt = JsonEncodedText.Encode("agent_prompt", null);

    // Token: 0x040001A5 RID: 421
    private static readonly JsonEncodedText PropName_deliverable = JsonEncodedText.Encode("deliverable", null);

    // Token: 0x040001A6 RID: 422
    private static readonly JsonEncodedText PropName_clicky_slug = JsonEncodedText.Encode("clicky_slug", null);

    // Token: 0x040001A7 RID: 423
    private static readonly JsonEncodedText PropName_clicky_key = JsonEncodedText.Encode("clicky_key", null);

    // Token: 0x040001A8 RID: 424
    private static readonly JsonEncodedText PropName_suggestion_id = JsonEncodedText.Encode("suggestion_id", null);

    // Token: 0x040001A9 RID: 425
    private static readonly JsonEncodedText PropName_markdown = JsonEncodedText.Encode("markdown", null);

    // Token: 0x040001AA RID: 426
    private static readonly JsonEncodedText PropName_profile_markdown = JsonEncodedText.Encode("profile_markdown", null);

    // Token: 0x040001AB RID: 427
    private static readonly JsonEncodedText PropName_context_markdown = JsonEncodedText.Encode("context_markdown", null);

    // Token: 0x040001AC RID: 428
    private static readonly JsonEncodedText PropName_has_profile = JsonEncodedText.Encode("has_profile", null);

    // Token: 0x040001AD RID: 429
    private static readonly JsonEncodedText PropName_updated_at = JsonEncodedText.Encode("updated_at", null);

    // Token: 0x040001AE RID: 430
    private static readonly JsonEncodedText PropName_count = JsonEncodedText.Encode("count", null);

    // Token: 0x040001AF RID: 431
    private static readonly JsonEncodedText PropName_freshness = JsonEncodedText.Encode("freshness", null);

    // Token: 0x040001B0 RID: 432
    private static readonly JsonEncodedText PropName_results = JsonEncodedText.Encode("results", null);

    // Token: 0x040001B1 RID: 433
    private static readonly JsonEncodedText PropName_age = JsonEncodedText.Encode("age", null);

    // Token: 0x040001B2 RID: 434
    private static readonly JsonEncodedText PropName_highlights = JsonEncodedText.Encode("highlights", null);

    // Token: 0x040001B3 RID: 435
    private static readonly JsonEncodedText PropName_modifiers = JsonEncodedText.Encode("modifiers", null);

    // Token: 0x040001B4 RID: 436
    private static readonly JsonEncodedText PropName_trigger = JsonEncodedText.Encode("trigger", null);

    // Token: 0x040001B5 RID: 437
    private static readonly JsonEncodedText PropName_push_to_talk = JsonEncodedText.Encode("push_to_talk", null);

    // Token: 0x040001B6 RID: 438
    private static readonly JsonEncodedText PropName_text_mode = JsonEncodedText.Encode("text_mode", null);

    // Token: 0x040001B7 RID: 439
    private static readonly JsonEncodedText PropName_hands_free_dictation = JsonEncodedText.Encode("hands_free_dictation", null);

    // Token: 0x02000265 RID: 613
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000DF9 RID: 3577
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <0>__PaywallPayloadCtorParamInit;

      // Token: 0x04000DFA RID: 3578
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <1>__PaywallPlanOptionCtorParamInit;

      // Token: 0x04000DFB RID: 3579
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <2>__AppConfigCtorParamInit;

      // Token: 0x04000DFC RID: 3580
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <3>__AppConfigLinksCtorParamInit;

      // Token: 0x04000DFD RID: 3581
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <4>__AppConfigResponseCtorParamInit;

      // Token: 0x04000DFE RID: 3582
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <5>__CheckoutRequestCtorParamInit;

      // Token: 0x04000DFF RID: 3583
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <6>__CheckoutResponseCtorParamInit;

      // Token: 0x04000E00 RID: 3584
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <7>__DeepgramTokenResponseCtorParamInit;

      // Token: 0x04000E01 RID: 3585
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <8>__DeviceRegistrationRequestCtorParamInit;

      // Token: 0x04000E02 RID: 3586
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <9>__DeviceRegistrationStampCtorParamInit;

      // Token: 0x04000E03 RID: 3587
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <10>__DeviceResponseCtorParamInit;

      // Token: 0x04000E04 RID: 3588
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <11>__DevicesResponseCtorParamInit;

      // Token: 0x04000E05 RID: 3589
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <12>__DiscoveryChannelRequestCtorParamInit;

      // Token: 0x04000E06 RID: 3590
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <13>__DiscoveryChannelResponseCtorParamInit;

      // Token: 0x04000E07 RID: 3591
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <14>__EarsTranscribeRequestCtorParamInit;

      // Token: 0x04000E08 RID: 3592
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <15>__EarsTranscribeResponseCtorParamInit;

      // Token: 0x04000E09 RID: 3593
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <16>__ErrorReportCtorParamInit;

      // Token: 0x04000E0A RID: 3594
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <17>__ImageSearchRequestCtorParamInit;

      // Token: 0x04000E0B RID: 3595
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <18>__ImageSearchResponseCtorParamInit;

      // Token: 0x04000E0C RID: 3596
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <19>__ImageSearchResultCtorParamInit;

      // Token: 0x04000E0D RID: 3597
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <20>__InterviewLineCtorParamInit;

      // Token: 0x04000E0E RID: 3598
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <21>__PlanSnapshotCtorParamInit;

      // Token: 0x04000E0F RID: 3599
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <22>__PlanSubscriptionCtorParamInit;

      // Token: 0x04000E10 RID: 3600
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <23>__PlanUsageCtorParamInit;

      // Token: 0x04000E11 RID: 3601
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <24>__PlanUsageBucketCtorParamInit;

      // Token: 0x04000E12 RID: 3602
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <25>__PortalResponseCtorParamInit;

      // Token: 0x04000E13 RID: 3603
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <26>__RealtimeSessionPromptContextCtorParamInit;

      // Token: 0x04000E14 RID: 3604
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <27>__RealtimeSessionRequestCtorParamInit;

      // Token: 0x04000E15 RID: 3605
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <28>__ReferralClaimRequestCtorParamInit;

      // Token: 0x04000E16 RID: 3606
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <29>__ReferralCodeRequestCtorParamInit;

      // Token: 0x04000E17 RID: 3607
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <30>__ReferralOutcomeResponseCtorParamInit;

      // Token: 0x04000E18 RID: 3608
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <31>__RegisteredDeviceCtorParamInit;

      // Token: 0x04000E19 RID: 3609
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <32>__SaveMemoryRequestCtorParamInit;

      // Token: 0x04000E1A RID: 3610
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <33>__SaveMemoryResponseCtorParamInit;

      // Token: 0x04000E1B RID: 3611
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <34>__StarterPlanClickyCtorParamInit;

      // Token: 0x04000E1C RID: 3612
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <35>__StarterPlanClickyInputCtorParamInit;

      // Token: 0x04000E1D RID: 3613
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <36>__StarterPlanClickyRoutineCtorParamInit;

      // Token: 0x04000E1E RID: 3614
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <37>__StarterPlanRequestCtorParamInit;

      // Token: 0x04000E1F RID: 3615
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <38>__StarterPlanResponseCtorParamInit;

      // Token: 0x04000E20 RID: 3616
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <39>__StarterPlanRoutineCtorParamInit;

      // Token: 0x04000E21 RID: 3617
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <40>__StarterPlanSuggestionCtorParamInit;

      // Token: 0x04000E22 RID: 3618
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <41>__UserMemoryResponseCtorParamInit;

      // Token: 0x04000E23 RID: 3619
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <42>__WebSearchRequestCtorParamInit;

      // Token: 0x04000E24 RID: 3620
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <43>__WebSearchResponseCtorParamInit;

      // Token: 0x04000E25 RID: 3621
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <44>__WebSearchResultCtorParamInit;

      // Token: 0x04000E26 RID: 3622
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <45>__HotkeyBindingCtorParamInit;

      // Token: 0x04000E27 RID: 3623
      [Nullable(new byte[] { 0, 1, 1 })]
      public static Func<JsonParameterInfoValues[]> <46>__HotkeyBindingsCtorParamInit;
    }
  }
}
