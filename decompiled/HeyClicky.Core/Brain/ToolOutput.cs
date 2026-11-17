using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F5 RID: 501
  [NullableContext(1)]
  [Nullable(0)]
  public static class ToolOutput
  {
    // Token: 0x06001D01 RID: 7425 RVA: 0x003E5874 File Offset: 0x003E5874
    public static string Status(string status, [Nullable(2)] string message = null)
    {
      string @string;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
        {
          utf8JsonWriter.WriteStartObject();
          utf8JsonWriter.WriteString("status", status);
          if (message != null)
          {
            utf8JsonWriter.WriteString("message", message);
          }
          utf8JsonWriter.WriteEndObject();
        }
        @string = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
      }
      return @string;
    }

    // Token: 0x06001D02 RID: 7426 RVA: 0x003E590C File Offset: 0x003E590C
    public static string Error(string message)
    {
      return ToolOutput.Status("error", message);
    }
  }
}
