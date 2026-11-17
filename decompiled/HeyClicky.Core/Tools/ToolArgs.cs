using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Tools
{
  // Token: 0x020000A4 RID: 164
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ToolArgs
  {
    // Token: 0x1700023B RID: 571
    // (get) Token: 0x06000A39 RID: 2617 RVA: 0x00373D9C File Offset: 0x00373D9C
    public JsonObject Json { get; }

    // Token: 0x1700023C RID: 572
    // (get) Token: 0x06000A3A RID: 2618 RVA: 0x00373DA4 File Offset: 0x00373DA4
    public string Raw { get; }

    // Token: 0x06000A3B RID: 2619 RVA: 0x00373DAC File Offset: 0x00373DAC
    [NullableContext(2)]
    public ToolArgs(string arguments)
    {
      this.Raw = (string.IsNullOrWhiteSpace(arguments) ? "{}" : arguments);
      try
      {
        JsonObject jsonObject;
        if ((jsonObject = JsonNode.Parse(this.Raw, null, default(JsonDocumentOptions)) as JsonObject) == null)
        {
          jsonObject = new JsonObject(null);
        }
        this.Json = jsonObject;
      }
      catch (JsonException)
      {
        this.Json = new JsonObject(null);
      }
    }

    // Token: 0x06000A3C RID: 2620 RVA: 0x00373E38 File Offset: 0x00373E38
    [return: Nullable(2)]
    public string Str(params string[] keys)
    {
      foreach (string text in keys)
      {
        JsonValue jsonValue = this.Json[text] as JsonValue;
        string text2;
        if (jsonValue != null && jsonValue.TryGetValue<string>(out text2) && !string.IsNullOrWhiteSpace(text2))
        {
          return text2.Trim();
        }
      }
      return null;
    }

    // Token: 0x06000A3D RID: 2621 RVA: 0x00373E8C File Offset: 0x00373E8C
    [return: Nullable(2)]
    public string Exact(string key)
    {
      JsonValue jsonValue = this.Json[key] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000A3E RID: 2622 RVA: 0x00373EBC File Offset: 0x00373EBC
    public bool? Bool(string key)
    {
      JsonValue jsonValue = this.Json[key] as JsonValue;
      if (jsonValue == null)
      {
        return null;
      }
      bool flag;
      if (jsonValue.TryGetValue<bool>(out flag))
      {
        return new bool?(flag);
      }
      string text;
      bool flag2;
      if (jsonValue.TryGetValue<string>(out text) && bool.TryParse(text, out flag2))
      {
        return new bool?(flag2);
      }
      return null;
    }

    // Token: 0x06000A3F RID: 2623 RVA: 0x00373F20 File Offset: 0x00373F20
    public int? Int(string key)
    {
      JsonValue jsonValue = this.Json[key] as JsonValue;
      if (jsonValue == null)
      {
        return null;
      }
      int num;
      if (jsonValue.TryGetValue<int>(out num))
      {
        return new int?(num);
      }
      long num2;
      if (jsonValue.TryGetValue<long>(out num2))
      {
        return new int?((int)Math.Clamp(num2, -2147483648L, 2147483647L));
      }
      double num3;
      if (jsonValue.TryGetValue<double>(out num3))
      {
        return new int?((int)num3);
      }
      string text;
      int num4;
      if (jsonValue.TryGetValue<string>(out text) && int.TryParse(text, out num4))
      {
        return new int?(num4);
      }
      return null;
    }

    // Token: 0x06000A40 RID: 2624 RVA: 0x00373FBC File Offset: 0x00373FBC
    [return: Nullable(2)]
    public JsonObject Object(string key)
    {
      return this.Json[key] as JsonObject;
    }
  }
}
