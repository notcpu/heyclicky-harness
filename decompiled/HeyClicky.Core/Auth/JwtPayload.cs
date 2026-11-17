using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace HeyClicky.Core.Auth
{
  // Token: 0x02000226 RID: 550
  public static class JwtPayload
  {
    // Token: 0x06002037 RID: 8247 RVA: 0x003F4A74 File Offset: 0x003F4A74
    [NullableContext(2)]
    public static bool TryRead(string jwt, out long expiresAtEpochSeconds, out string subject, out string email)
    {
      expiresAtEpochSeconds = 0L;
      subject = null;
      email = null;
      if (string.IsNullOrEmpty(jwt))
      {
        return false;
      }
      int num = jwt.IndexOf('.');
      if (num < 0)
      {
        return false;
      }
      int num2 = jwt.IndexOf('.', num + 1);
      ReadOnlySpan<char> readOnlySpan = ((num2 < 0) ? jwt.AsSpan(num + 1) : jwt.AsSpan(num + 1, num2 - num - 1));
      bool flag;
      try
      {
        StringBuilder stringBuilder = new StringBuilder(readOnlySpan.Length + 3).Append(readOnlySpan).Replace('-', '+').Replace('_', '/');
        while (stringBuilder.Length % 4 != 0)
        {
          stringBuilder.Append('=');
        }
        using (JsonDocument jsonDocument = JsonDocument.Parse(Convert.FromBase64String(stringBuilder.ToString()), default(JsonDocumentOptions)))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          if (rootElement.ValueKind != JsonValueKind.Object)
          {
            flag = false;
          }
          else
          {
            JsonElement jsonElement;
            if (rootElement.TryGetProperty("exp", out jsonElement) && jsonElement.ValueKind == JsonValueKind.Number)
            {
              expiresAtEpochSeconds = (long)jsonElement.GetDouble();
            }
            JsonElement jsonElement2;
            if (rootElement.TryGetProperty("sub", out jsonElement2) && jsonElement2.ValueKind == JsonValueKind.String)
            {
              subject = jsonElement2.GetString();
            }
            JsonElement jsonElement3;
            if (rootElement.TryGetProperty("email", out jsonElement3) && jsonElement3.ValueKind == JsonValueKind.String)
            {
              email = jsonElement3.GetString();
            }
            flag = true;
          }
        }
      }
      catch
      {
        flag = false;
      }
      return flag;
    }
  }
}
