using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using HeyClicky.Core.Configuration;
using Microsoft.Win32;

namespace HeyClicky.Core.Identity
{
  // Token: 0x0200018B RID: 395
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyClientIdentity
  {
    // Token: 0x17000553 RID: 1363
    // (get) Token: 0x0600178F RID: 6031 RVA: 0x003C4664 File Offset: 0x003C4664
    public static ClientIdentity Current
    {
      get
      {
        ClientIdentity clientIdentity;
        if ((clientIdentity = ClickyClientIdentity.current) == null)
        {
          clientIdentity = (ClickyClientIdentity.current = ClickyClientIdentity.Create(null));
        }
        return clientIdentity;
      }
    }

    // Token: 0x06001790 RID: 6032 RVA: 0x003C467C File Offset: 0x003C467C
    public static ClientIdentity Create([Nullable(2)] Assembly appAssembly = null)
    {
      if (appAssembly == null)
      {
        appAssembly = Assembly.GetEntryAssembly() ?? typeof(ClickyClientIdentity).Assembly;
      }
      ValueTuple<string, string> valueTuple = ClickyClientIdentity.ReadVersion(appAssembly);
      string item = valueTuple.Item1;
      string item2 = valueTuple.Item2;
      return new ClientIdentity("windows", ClickyClientIdentity.DeviceKey(), item, item2, ClickyClientIdentity.OsVersion(), ClickyClientIdentity.Capabilities);
    }

    // Token: 0x06001791 RID: 6033 RVA: 0x003C46D4 File Offset: 0x003C46D4
    public static string DeviceKey()
    {
      string text = ClickyClientIdentity.ReadMachineGuid();
      if (!string.IsNullOrWhiteSpace(text))
      {
        return ClickyClientIdentity.HashDeviceKey(text);
      }
      string text2 = Path.Combine(AppBundleConfiguration.AppDataDirectory, "device-key");
      string text5;
      try
      {
        if (File.Exists(text2))
        {
          string text3 = File.ReadAllText(text2).Trim();
          if (text3.Length >= 8)
          {
            return text3;
          }
        }
        string text4 = Guid.NewGuid().ToString("N");
        Directory.CreateDirectory(Path.GetDirectoryName(text2));
        File.WriteAllText(text2, text4);
        text5 = text4;
      }
      catch
      {
        text5 = Guid.NewGuid().ToString("N");
      }
      return text5;
    }

    // Token: 0x06001792 RID: 6034 RVA: 0x003C4780 File Offset: 0x003C4780
    public unsafe static string HashDeviceKey(string hardwareId)
    {
      Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)32], 32);
      SHA256.HashData(Encoding.UTF8.GetBytes("heyclicky-device:" + hardwareId), span);
      return Convert.ToHexStringLower(span.Slice(0, 16));
    }

    // Token: 0x06001793 RID: 6035 RVA: 0x003C47D0 File Offset: 0x003C47D0
    [NullableContext(2)]
    private static string ReadMachineGuid()
    {
      string text;
      try
      {
        using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
        {
          using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
          {
            text = ((registryKey2 != null) ? registryKey2.GetValue("MachineGuid") : null) as string;
          }
        }
      }
      catch
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06001794 RID: 6036 RVA: 0x003C4854 File Offset: 0x003C4854
    [return: TupleElementNames(new string[] { "Version", "Build" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    private static ValueTuple<string, string> ReadVersion(Assembly assembly)
    {
      AssemblyInformationalVersionAttribute customAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
      string text = ((customAttribute != null) ? customAttribute.InformationalVersion : null);
      string text2 = ((text != null) ? text.Split('+', StringSplitOptions.None)[0] : null);
      Version version = assembly.GetName().Version;
      if (string.IsNullOrWhiteSpace(text2))
      {
        string text3;
        if (version != null)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
          defaultInterpolatedStringHandler.AppendFormatted<int>(version.Major);
          defaultInterpolatedStringHandler.AppendLiteral(".");
          defaultInterpolatedStringHandler.AppendFormatted<int>(version.Minor);
          defaultInterpolatedStringHandler.AppendLiteral(".");
          defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(0, version.Build));
          text3 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text3 = "unknown";
        }
        text2 = text3;
      }
      AssemblyFileVersionAttribute customAttribute2 = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
      string text4 = ((customAttribute2 != null) ? customAttribute2.Version : null);
      Version version2;
      string text5 = ((text4 != null && Version.TryParse(text4, out version2) && version2.Revision > 0) ? version2.Revision.ToString() : ((version != null && version.Revision > 0) ? version.Revision.ToString() : "0"));
      return new ValueTuple<string, string>(text2, text5);
    }

    // Token: 0x06001795 RID: 6037 RVA: 0x003C4960 File Offset: 0x003C4960
    public static string OsVersion()
    {
      Version version = Environment.OSVersion.Version;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      defaultInterpolatedStringHandler.AppendFormatted<int>(version.Major);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted<int>(version.Minor);
      defaultInterpolatedStringHandler.AppendLiteral(".");
      defaultInterpolatedStringHandler.AppendFormatted<int>(version.Build);
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x04000A22 RID: 2594
    public const string PlatformHeader = "X-Clicky-Platform";

    // Token: 0x04000A23 RID: 2595
    public const string DeviceHeader = "X-Clicky-Device";

    // Token: 0x04000A24 RID: 2596
    public const string AppVersionHeader = "X-Clicky-App-Version";

    // Token: 0x04000A25 RID: 2597
    public const string AppBuildHeader = "X-Clicky-App-Build";

    // Token: 0x04000A26 RID: 2598
    public const string OsVersionHeader = "X-Clicky-OS-Version";

    // Token: 0x04000A27 RID: 2599
    public const string Platform = "windows";

    // Token: 0x04000A28 RID: 2600
    public static readonly IReadOnlyList<string> Capabilities = new <>z__ReadOnlyArray<string>(new string[] { "voice", "dictation", "screen_drawing", "local_agents", "notch" });

    // Token: 0x04000A29 RID: 2601
    [Nullable(2)]
    private static ClientIdentity current;
  }
}
