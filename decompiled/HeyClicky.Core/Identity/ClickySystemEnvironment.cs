using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using Microsoft.Win32;

namespace HeyClicky.Core.Identity
{
  // Token: 0x0200018C RID: 396
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickySystemEnvironment
  {
    // Token: 0x06001797 RID: 6039 RVA: 0x003C4A04 File Offset: 0x003C4A04
    public static void Write(Utf8JsonWriter writer, string ianaTimeZone)
    {
      writer.WriteStartObject("environment");
      writer.WriteString("platform", "windows");
      writer.WriteString("timezone", ianaTimeZone);
      writer.WriteString("locale", CultureInfo.CurrentCulture.Name);
      writer.WriteString("os_version", ClickyClientIdentity.OsVersion());
      writer.WriteNumber("display_count", ClickySystemEnvironment.DisplayCount());
      IReadOnlyList<string> readOnlyList = ClickySystemEnvironment.PreferredLanguages();
      if (readOnlyList.Count > 0)
      {
        writer.WriteStartArray("preferred_languages");
        foreach (string text in readOnlyList)
        {
          writer.WriteStringValue(text);
        }
        writer.WriteEndArray();
      }
      string text2 = ClickySystemEnvironment.DeviceModel();
      if (text2 != null)
      {
        writer.WriteString("device_model", text2);
      }
      writer.WriteEndObject();
    }

    // Token: 0x06001798 RID: 6040
    [LibraryImport("user32.dll")]
    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern int GetSystemMetrics(int index);

    // Token: 0x06001799 RID: 6041 RVA: 0x003C4AE4 File Offset: 0x003C4AE4
    public static int DisplayCount()
    {
      int num;
      try
      {
        num = Math.Max(1, ClickySystemEnvironment.GetSystemMetrics(80));
      }
      catch
      {
        num = 1;
      }
      return num;
    }

    // Token: 0x0600179A RID: 6042 RVA: 0x003C4B18 File Offset: 0x003C4B18
    public static IReadOnlyList<string> PreferredLanguages()
    {
      List<string> list = new List<string>();
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International\\User Profile"))
        {
          string[] array = ((registryKey != null) ? registryKey.GetValue("Languages") : null) as string[];
          if (array != null)
          {
            foreach (string text in array)
            {
              if (!string.IsNullOrWhiteSpace(text) && !list.Contains(text.Trim()))
              {
                list.Add(text.Trim());
              }
            }
          }
        }
      }
      catch
      {
      }
      if (list.Count == 0)
      {
        list.Add(CultureInfo.CurrentUICulture.Name);
      }
      return list.Take<string>(10).ToList<string>();
    }

    // Token: 0x0600179B RID: 6043 RVA: 0x003C4BE8 File Offset: 0x003C4BE8
    [NullableContext(2)]
    public static string DeviceModel()
    {
      List<string> list = new List<string>();
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS"))
        {
          string text = ((registryKey != null) ? registryKey.GetValue("SystemProductName") : null) as string;
          string text2 = ((text != null) ? text.Trim() : null);
          if (text2 != null && text2.Length > 0)
          {
            list.Add(text2);
          }
          using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0"))
          {
            string text3 = ((registryKey2 != null) ? registryKey2.GetValue("ProcessorNameString") : null) as string;
            string text4 = ((text3 != null) ? text3.Trim() : null);
            if (text4 != null && text4.Length > 0)
            {
              list.Add(text4);
            }
          }
        }
      }
      catch
      {
      }
      string text5 = string.Join(", ", list);
      string text6 = RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant();
      text5 += ((text5.Length == 0) ? text6 : (" (" + text6 + ")"));
      if (text5.Length == 0)
      {
        return null;
      }
      if (text5.Length <= 160)
      {
        return text5;
      }
      return text5.Substring(0, 160);
    }

    // Token: 0x04000A2A RID: 2602
    private const int SM_CMONITORS = 80;
  }
}
