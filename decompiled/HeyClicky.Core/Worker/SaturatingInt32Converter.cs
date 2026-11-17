using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HeyClicky.Core.Worker
{
  // Token: 0x0200003D RID: 61
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class SaturatingInt32Converter : JsonConverter<int>
  {
    // Token: 0x06000221 RID: 545 RVA: 0x00350758 File Offset: 0x00350758
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.String)
      {
        double num;
        if (!double.TryParse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num))
        {
          throw new JsonException("not a number");
        }
        return SaturatingInt32Converter.Saturate(num);
      }
      else
      {
        if (reader.TokenType != JsonTokenType.Number)
        {
          throw new JsonException("not a number");
        }
        long num2;
        if (!reader.TryGetInt64(out num2))
        {
          return SaturatingInt32Converter.Saturate(reader.GetDouble());
        }
        return (int)Math.Clamp(num2, -2147483648L, 2147483647L);
      }
    }

    // Token: 0x06000222 RID: 546 RVA: 0x003507D8 File Offset: 0x003507D8
    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
      writer.WriteNumberValue(value);
    }

    // Token: 0x06000223 RID: 547 RVA: 0x003507E4 File Offset: 0x003507E4
    private static int Saturate(double value)
    {
      if (!double.IsNaN(value))
      {
        return (int)Math.Clamp(Math.Truncate(value), -2147483648.0, 2147483647.0);
      }
      return 0;
    }
  }
}
