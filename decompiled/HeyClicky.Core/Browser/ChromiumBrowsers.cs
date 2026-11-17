using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Win32;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E6 RID: 486
  [NullableContext(1)]
  [Nullable(0)]
  public static class ChromiumBrowsers
  {
    // Token: 0x1700062D RID: 1581
    // (get) Token: 0x06001BEC RID: 7148 RVA: 0x003DEBE4 File Offset: 0x003DEBE4
    private static string LocalAppData
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      }
    }

    // Token: 0x1700062E RID: 1582
    // (get) Token: 0x06001BED RID: 7149 RVA: 0x003DEBF0 File Offset: 0x003DEBF0
    private static string ProgramFiles
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
      }
    }

    // Token: 0x1700062F RID: 1583
    // (get) Token: 0x06001BEE RID: 7150 RVA: 0x003DEBFC File Offset: 0x003DEBFC
    private static string ProgramFilesX86
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
      }
    }

    // Token: 0x06001BEF RID: 7151 RVA: 0x003DEC08 File Offset: 0x003DEC08
    private static IReadOnlyList<string> Installs(string relative, bool userLevel = true, bool machineLevel = true)
    {
      List<string> list = new List<string>();
      if (machineLevel)
      {
        list.Add(Path.Combine(ChromiumBrowsers.ProgramFiles, relative));
        list.Add(Path.Combine(ChromiumBrowsers.ProgramFilesX86, relative));
      }
      if (userLevel)
      {
        list.Add(Path.Combine(ChromiumBrowsers.LocalAppData, relative));
      }
      return list;
    }

    // Token: 0x17000630 RID: 1584
    // (get) Token: 0x06001BF0 RID: 7152 RVA: 0x003DEC58 File Offset: 0x003DEC58
    public static IReadOnlyList<ChromiumBrowser> All { get; } = new <>z__ReadOnlyArray<ChromiumBrowser>(new ChromiumBrowser[]
    {
      new ChromiumBrowser("Chrome", "Google\\Chrome", "Google\\Chrome\\User Data", ChromiumBrowsers.Installs("Google\\Chrome\\Application\\chrome.exe", true, true), "chrome.exe", null, "ChromeHTML"),
      new ChromiumBrowser("Edge", "Microsoft\\Edge", "Microsoft\\Edge\\User Data", ChromiumBrowsers.Installs("Microsoft\\Edge\\Application\\msedge.exe", true, true), "msedge.exe", null, "MSEdgeHTM"),
      new ChromiumBrowser("Brave", "BraveSoftware\\Brave-Browser", "BraveSoftware\\Brave-Browser\\User Data", ChromiumBrowsers.Installs("BraveSoftware\\Brave-Browser\\Application\\brave.exe", true, true), "brave.exe", null, "BraveHTML"),
      new ChromiumBrowser("Arc", null, "Packages", Array.Empty<string>(), null, "TheBrowserCompany.Arc_", "Arc"),
      new ChromiumBrowser("Vivaldi", "Vivaldi", "Vivaldi\\User Data", ChromiumBrowsers.Installs("Vivaldi\\Application\\vivaldi.exe", true, true), "vivaldi.exe", null, "VivaldiHTM"),
      new ChromiumBrowser("Chrome Beta", "Google\\Chrome Beta", "Google\\Chrome Beta\\User Data", ChromiumBrowsers.Installs("Google\\Chrome Beta\\Application\\chrome.exe", true, true), null, null, "ChromeBHTML"),
      new ChromiumBrowser("Chrome Dev", "Google\\Chrome Dev", "Google\\Chrome Dev\\User Data", ChromiumBrowsers.Installs("Google\\Chrome Dev\\Application\\chrome.exe", true, true), null, null, "ChromeDHTML"),
      new ChromiumBrowser("Chrome Canary", "Google\\Chrome SxS", "Google\\Chrome SxS\\User Data", ChromiumBrowsers.Installs("Google\\Chrome SxS\\Application\\chrome.exe", true, false), null, null, "ChromeSSHTM"),
      new ChromiumBrowser("Chromium", "Chromium", "Chromium\\User Data", ChromiumBrowsers.Installs("Chromium\\Application\\chrome.exe", true, true), null, null, "ChromiumHTM")
    });

    // Token: 0x17000631 RID: 1585
    // (get) Token: 0x06001BF1 RID: 7153 RVA: 0x003DEC60 File Offset: 0x003DEC60
    public static IReadOnlyList<string> ExecutableNames { get; } = new <>z__ReadOnlyArray<string>(new string[] { "chrome.exe", "msedge.exe", "brave.exe", "vivaldi.exe", "arc.exe" });

    // Token: 0x06001BF2 RID: 7154 RVA: 0x003DEC68 File Offset: 0x003DEC68
    [return: Nullable(2)]
    public static string ExecutablePath(ChromiumBrowser browser)
    {
      foreach (string text in browser.Executables)
      {
        if (File.Exists(text))
        {
          return text;
        }
      }
      string appPathsName = browser.AppPathsName;
      if (appPathsName != null)
      {
        foreach (RegistryKey registryKey in new RegistryKey[]
        {
          Registry.CurrentUser,
          Registry.LocalMachine
        })
        {
          try
          {
            using (RegistryKey registryKey2 = registryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + appPathsName))
            {
              object obj = ((registryKey2 != null) ? registryKey2.GetValue(null) : null);
              string path = obj as string;
              if (path != null && File.Exists(path.Trim('"')) && browser.Executables.Any<string>(delegate(string e)
              {
                string fileName = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(e)));
                return fileName != null && path.Contains(fileName, StringComparison.OrdinalIgnoreCase);
              }))
              {
                return path.Trim('"');
              }
            }
          }
          catch
          {
          }
        }
      }
      string packageFamilyPrefix = browser.PackageFamilyPrefix;
      if (packageFamilyPrefix != null)
      {
        try
        {
          if (Directory.EnumerateDirectories(Path.Combine(ChromiumBrowsers.LocalAppData, "Packages"), packageFamilyPrefix + "*").Any<string>())
          {
            string text2 = Path.Combine(ChromiumBrowsers.LocalAppData, "Microsoft\\WindowsApps\\Arc.exe");
            return File.Exists(text2) ? text2 : "";
          }
        }
        catch
        {
        }
      }
      return null;
    }

    // Token: 0x17000632 RID: 1586
    // (get) Token: 0x06001BF3 RID: 7155 RVA: 0x003DEE24 File Offset: 0x003DEE24
    public static IReadOnlyList<string> InstalledNames
    {
      get
      {
        return (from b in ChromiumBrowsers.All
          where ChromiumBrowsers.ExecutablePath(b) != null
          select b.Name).ToList<string>();
      }
    }

    // Token: 0x06001BF4 RID: 7156 RVA: 0x003DEE84 File Offset: 0x003DEE84
    public static IReadOnlyList<ChromiumBrowser> InstalledOrUsed()
    {
      return ChromiumBrowsers.All.Where<ChromiumBrowser>((ChromiumBrowser b) => ChromiumBrowsers.ExecutablePath(b) != null || (b.PackageFamilyPrefix == null && Directory.Exists(Path.Combine(ChromiumBrowsers.LocalAppData, b.UserDataFolder)))).ToList<ChromiumBrowser>();
    }

    // Token: 0x17000633 RID: 1587
    // (get) Token: 0x06001BF5 RID: 7157 RVA: 0x003DEEB4 File Offset: 0x003DEEB4
    // (set) Token: 0x06001BF6 RID: 7158 RVA: 0x003DEEC0 File Offset: 0x003DEEC0
    [Nullable(2)]
    public static string LastConnectedBrowserName
    {
      [NullableContext(2)]
      get
      {
        return ChromiumBrowsers.lastConnectedBrowserName;
      }
      [NullableContext(2)]
      set
      {
        ChromiumBrowsers.lastConnectedBrowserName = value;
        ChromiumBrowsers.preferredCache = null;
      }
    }

    // Token: 0x06001BF7 RID: 7159 RVA: 0x003DEED4 File Offset: 0x003DEED4
    [return: TupleElementNames(new string[] { "Name", "Executable" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string>? Preferred()
    {
      ChromiumBrowsers.PreferredCache preferredCache = ChromiumBrowsers.preferredCache;
      if (preferredCache != null && (DateTime.UtcNow - preferredCache.At).TotalSeconds < 10.0)
      {
        return preferredCache.Browser;
      }
      string text = ChromiumBrowsers.LastConnectedBrowserName;
      string text2 = ChromiumBrowsers.DefaultBrowserProgId();
      Func<ChromiumBrowser, double?> func;
      if ((func = ChromiumBrowsers.<>O.<0>__LastActiveTime) == null)
      {
        func = (ChromiumBrowsers.<>O.<0>__LastActiveTime = new Func<ChromiumBrowser, double?>(ChromiumBrowsers.LastActiveTime));
      }
      ValueTuple<string, string>? valueTuple = ChromiumBrowsers.ResolvePreferred(text, text2, func, null);
      ChromiumBrowsers.preferredCache = new ChromiumBrowsers.PreferredCache(DateTime.UtcNow, valueTuple);
      return valueTuple;
    }

    // Token: 0x06001BF8 RID: 7160 RVA: 0x003DEF58 File Offset: 0x003DEF58
    public static void ForgetPreferred()
    {
      ChromiumBrowsers.preferredCache = null;
    }

    // Token: 0x06001BF9 RID: 7161 RVA: 0x003DEF64 File Offset: 0x003DEF64
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Name", "Executable" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public static ValueTuple<string, string>? ResolvePreferred(string lastConnected, string defaultProgId, [Nullable(1)] Func<ChromiumBrowser, double?> lastActiveTime, [Nullable(new byte[] { 2, 1, 2 })] Func<ChromiumBrowser, string> executablePath = null)
    {
      ChromiumBrowsers.<>c__DisplayClass26_0 CS$<>8__locals1 = new ChromiumBrowsers.<>c__DisplayClass26_0();
      CS$<>8__locals1.executablePath = executablePath;
      CS$<>8__locals1.lastConnected = lastConnected;
      CS$<>8__locals1.lastActiveTime = lastActiveTime;
      if (CS$<>8__locals1.executablePath == null)
      {
        ChromiumBrowsers.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
        Func<ChromiumBrowser, string> func;
        if ((func = ChromiumBrowsers.<>O.<1>__ExecutablePath) == null)
        {
          func = (ChromiumBrowsers.<>O.<1>__ExecutablePath = new Func<ChromiumBrowser, string>(ChromiumBrowsers.ExecutablePath));
        }
        CS$<>8__locals2.executablePath = func;
      }
      List<ValueTuple<ChromiumBrowser, string>> list = (from b in ChromiumBrowsers.All
        select new ValueTuple<ChromiumBrowser, string>(b, CS$<>8__locals1.executablePath(b)) into b
        where b.Item2 != null
        select b).ToList<ValueTuple<ChromiumBrowser, string>>();
      if (list.Count == 0)
      {
        return null;
      }
      ValueTuple<ChromiumBrowser, string> valueTuple = list.FirstOrDefault<ValueTuple<ChromiumBrowser, string>>(([TupleElementNames(new string[] { "Browser", "Path" })] ValueTuple<ChromiumBrowser, string> b) => b.Item1.Name == CS$<>8__locals1.lastConnected);
      if (valueTuple.Item1 != null)
      {
        return new ValueTuple<string, string>?(ChromiumBrowsers.<ResolvePreferred>g__Of|26_2(valueTuple));
      }
      CS$<>8__locals1.defaultBrowser = ChromiumBrowsers.BrowserForProgId(defaultProgId);
      if (CS$<>8__locals1.defaultBrowser != null)
      {
        ValueTuple<ChromiumBrowser, string> valueTuple2 = list.FirstOrDefault<ValueTuple<ChromiumBrowser, string>>(([TupleElementNames(new string[] { "Browser", "Path" })] ValueTuple<ChromiumBrowser, string> b) => b.Item1 == CS$<>8__locals1.defaultBrowser);
        if (valueTuple2.Item1 != null)
        {
          return new ValueTuple<string, string>?(ChromiumBrowsers.<ResolvePreferred>g__Of|26_2(valueTuple2));
        }
      }
      ValueTuple<ChromiumBrowser, string, double?> valueTuple3 = (from b in list.Select<ValueTuple<ChromiumBrowser, string>, ValueTuple<ChromiumBrowser, string, double?>>(([TupleElementNames(new string[] { "Browser", "Path" })] ValueTuple<ChromiumBrowser, string> b) => new ValueTuple<ChromiumBrowser, string, double?>(b.Item1, b.Item2, CS$<>8__locals1.lastActiveTime(b.Item1))).Where<ValueTuple<ChromiumBrowser, string, double?>>(delegate([TupleElementNames(new string[] { "Browser", "Path", "Active" })] ValueTuple<ChromiumBrowser, string, double?> b)
        {
          double? item = b.Item3;
          return item != null;
        })
        orderby b.Item3 descending
        select b).FirstOrDefault<ValueTuple<ChromiumBrowser, string, double?>>();
      if (valueTuple3.Item1 != null)
      {
        return new ValueTuple<string, string>?(new ValueTuple<string, string>(valueTuple3.Item1.Name, valueTuple3.Item2));
      }
      return new ValueTuple<string, string>?(ChromiumBrowsers.<ResolvePreferred>g__Of|26_2(list[0]));
    }

    // Token: 0x06001BFA RID: 7162 RVA: 0x003DF104 File Offset: 0x003DF104
    [NullableContext(2)]
    public static ChromiumBrowser BrowserForProgId(string progId)
    {
      if (string.IsNullOrWhiteSpace(progId))
      {
        return null;
      }
      return (from b in ChromiumBrowsers.All
        where b.ProgIdPrefix != null
        orderby b.ProgIdPrefix.Length descending
        select b).FirstOrDefault<ChromiumBrowser>((ChromiumBrowser b) => progId.StartsWith(b.ProgIdPrefix, StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x06001BFB RID: 7163 RVA: 0x003DF18C File Offset: 0x003DF18C
    [NullableContext(2)]
    public static ChromiumBrowser BrowserForExecutable(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
      {
        return null;
      }
      ChromiumBrowser chromiumBrowser = ChromiumBrowsers.All.FirstOrDefault<ChromiumBrowser>(delegate(ChromiumBrowser b)
      {
        string text = ChromiumBrowsers.ExecutablePath(b);
        return text != null && text.Length > 0 && string.Equals(text, path, StringComparison.OrdinalIgnoreCase);
      });
      if (chromiumBrowser != null)
      {
        return chromiumBrowser;
      }
      string name = Path.GetFileName(path).ToLowerInvariant();
      ChromiumBrowser chromiumBrowser2;
      if ((chromiumBrowser2 = ChromiumBrowsers.All.FirstOrDefault<ChromiumBrowser>((ChromiumBrowser b) => b.AppPathsName == name)) == null)
      {
        if (!(name == "arc.exe"))
        {
          return null;
        }
        chromiumBrowser2 = ChromiumBrowsers.All.First<ChromiumBrowser>((ChromiumBrowser b) => b.Name == "Arc");
      }
      return chromiumBrowser2;
    }

    // Token: 0x06001BFC RID: 7164 RVA: 0x003DF240 File Offset: 0x003DF240
    [return: Nullable(2)]
    public static string ExecutablePathFor(string browserName)
    {
      ChromiumBrowser chromiumBrowser = ChromiumBrowsers.All.FirstOrDefault<ChromiumBrowser>((ChromiumBrowser b) => b.Name == browserName);
      if (chromiumBrowser == null)
      {
        return null;
      }
      return ChromiumBrowsers.ExecutablePath(chromiumBrowser);
    }

    // Token: 0x06001BFD RID: 7165 RVA: 0x003DF27C File Offset: 0x003DF27C
    public static string ExtensionsPageUrl(string browserName)
    {
      string text;
      if (!(browserName == "Edge"))
      {
        if (!(browserName == "Brave"))
        {
          text = "chrome://extensions/";
        }
        else
        {
          text = "brave://extensions/";
        }
      }
      else
      {
        text = "edge://extensions/";
      }
      return text;
    }

    // Token: 0x06001BFE RID: 7166 RVA: 0x003DF2BC File Offset: 0x003DF2BC
    [NullableContext(2)]
    private static string DefaultBrowserProgId()
    {
      string text;
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\https\\UserChoice"))
        {
          text = ((registryKey != null) ? registryKey.GetValue("ProgId") : null) as string;
        }
      }
      catch
      {
        text = null;
      }
      return text;
    }

    // Token: 0x06001BFF RID: 7167 RVA: 0x003DF320 File Offset: 0x003DF320
    private static double? LastActiveTime(ChromiumBrowser browser)
    {
      double? num;
      if (browser.PackageFamilyPrefix != null)
      {
        num = null;
        return num;
      }
      try
      {
        JsonObject jsonObject = JsonNode.Parse(File.ReadAllText(Path.Combine(ChromiumBrowsers.LocalAppData, browser.UserDataFolder, "Local State")), null, default(JsonDocumentOptions)) as JsonObject;
        if (jsonObject != null)
        {
          JsonNode jsonNode = jsonObject["profile"];
          JsonObject jsonObject2 = ((jsonNode != null) ? jsonNode["info_cache"] : null) as JsonObject;
          if (jsonObject2 != null)
          {
            double? num2 = null;
            foreach (KeyValuePair<string, JsonNode> keyValuePair in jsonObject2)
            {
              string text;
              JsonNode jsonNode2;
              keyValuePair.Deconstruct(out text, out jsonNode2);
              JsonNode jsonNode3 = jsonNode2;
              JsonValue jsonValue = ((jsonNode3 != null) ? jsonNode3["active_time"] : null) as JsonValue;
              if (jsonValue != null && jsonValue.GetValueKind() == JsonValueKind.Number)
              {
                double value = jsonValue.GetValue<double>();
                if (num2 != null)
                {
                  double num3 = value;
                  double? num4 = num2;
                  if (!((num3 > num4.GetValueOrDefault()) & (num4 != null)))
                  {
                    continue;
                  }
                }
                num2 = new double?(value);
              }
            }
            return num2;
          }
        }
        num = null;
        num = num;
      }
      catch
      {
        num = null;
      }
      return num;
    }

    // Token: 0x06001C00 RID: 7168 RVA: 0x003DF490 File Offset: 0x003DF490
    public static JsonObject Manifest(string helperPath, IReadOnlyList<string> extensionIds)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["name"] = "com.heyclicky.browser";
      jsonObject["description"] = "HeyClicky browser connection";
      jsonObject["path"] = helperPath;
      jsonObject["type"] = "stdio";
      jsonObject["allowed_origins"] = new JsonArray(extensionIds.Select<string, JsonNode>((string id) => "chrome-extension://" + id + "/").ToArray<JsonNode>());
      return jsonObject;
    }

    // Token: 0x06001C01 RID: 7169 RVA: 0x003DF538 File Offset: 0x003DF538
    public static void Register(string manifestPath, string helperPath, IReadOnlyList<string> extensionIds, [Nullable(2)] RegistryKey registryRoot = null)
    {
      Directory.CreateDirectory(Path.GetDirectoryName(manifestPath));
      string text = ChromiumBrowsers.Manifest(helperPath, extensionIds).ToJsonString(new JsonSerializerOptions
      {
        WriteIndented = true
      });
      string text2 = manifestPath + ".tmp";
      File.WriteAllText(text2, text);
      File.Move(text2, manifestPath, true);
      List<ChromiumBrowser> list = ChromiumBrowsers.InstalledOrUsed().ToList<ChromiumBrowser>();
      if (!list.Contains(ChromiumBrowsers.All[0]))
      {
        list.Insert(0, ChromiumBrowsers.All[0]);
      }
      using (RegistryKey registryKey = ((registryRoot == null) ? Registry.CurrentUser.CreateSubKey("Software") : null))
      {
        RegistryKey registryKey2 = registryRoot ?? registryKey;
        int num = 0;
        ChromiumBrowserRegistrationException ex = null;
        foreach (ChromiumBrowser chromiumBrowser in list)
        {
          string registryPath = chromiumBrowser.RegistryPath;
          if (registryPath != null)
          {
            string text3 = "HKCU\\Software\\" + registryPath + "\\NativeMessagingHosts\\com.heyclicky.browser";
            try
            {
              using (RegistryKey registryKey3 = registryKey2.CreateSubKey(registryPath + "\\NativeMessagingHosts\\com.heyclicky.browser"))
              {
                registryKey3.SetValue(null, manifestPath, RegistryValueKind.String);
                num++;
              }
            }
            catch (Exception ex2) when (ex2 is UnauthorizedAccessException || ex2 is SecurityException || ex2 is IOException)
            {
              string text4 = "Browser";
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 4);
              defaultInterpolatedStringHandler.AppendLiteral("native host registration failed for ");
              defaultInterpolatedStringHandler.AppendFormatted(chromiumBrowser.Name);
              defaultInterpolatedStringHandler.AppendLiteral(" at ");
              defaultInterpolatedStringHandler.AppendFormatted(text3);
              defaultInterpolatedStringHandler.AppendLiteral(": ");
              defaultInterpolatedStringHandler.AppendFormatted(ex2.GetType().Name);
              defaultInterpolatedStringHandler.AppendLiteral(" ");
              defaultInterpolatedStringHandler.AppendFormatted(ex2.Message);
              Log.Warn(text4, defaultInterpolatedStringHandler.ToStringAndClear());
              if (ex == null)
              {
                ex = new ChromiumBrowserRegistrationException(chromiumBrowser.Name, text3, ex2);
              }
            }
          }
        }
        if (num == 0 && ex != null)
        {
          throw ex;
        }
      }
    }

    // Token: 0x06001C02 RID: 7170 RVA: 0x003DF7B4 File Offset: 0x003DF7B4
    public static void Unregister(string manifestPath, [Nullable(2)] RegistryKey registryRoot = null)
    {
      using (RegistryKey registryKey = ((registryRoot == null) ? Registry.CurrentUser.OpenSubKey("Software", true) : null))
      {
        RegistryKey registryKey2 = registryRoot ?? registryKey;
        if (registryKey2 != null)
        {
          foreach (ChromiumBrowser chromiumBrowser in ChromiumBrowsers.All)
          {
            string registryPath = chromiumBrowser.RegistryPath;
            if (registryPath != null)
            {
              try
              {
                registryKey2.DeleteSubKeyTree(registryPath + "\\NativeMessagingHosts\\com.heyclicky.browser", false);
              }
              catch
              {
              }
            }
          }
        }
        try
        {
          File.Delete(manifestPath);
        }
        catch
        {
        }
      }
    }

    // Token: 0x06001C03 RID: 7171 RVA: 0x003DF874 File Offset: 0x003DF874
    public static bool IsRegistered(string manifestPath, [Nullable(2)] RegistryKey registryRoot = null)
    {
      bool flag;
      try
      {
        using (RegistryKey registryKey = ((registryRoot == null) ? Registry.CurrentUser.OpenSubKey("Software") : null))
        {
          RegistryKey registryKey2 = registryRoot ?? registryKey;
          using (RegistryKey registryKey3 = ((registryKey2 != null) ? registryKey2.OpenSubKey(ChromiumBrowsers.All[0].RegistryPath + "\\NativeMessagingHosts\\com.heyclicky.browser") : null))
          {
            flag = string.Equals(((registryKey3 != null) ? registryKey3.GetValue(null) : null) as string, manifestPath, StringComparison.OrdinalIgnoreCase);
          }
        }
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001C05 RID: 7173 RVA: 0x003DFB00 File Offset: 0x003DFB00
    [CompilerGenerated]
    [return: TupleElementNames(new string[] { "Name", "Executable" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    internal static ValueTuple<string, string> <ResolvePreferred>g__Of|26_2([TupleElementNames(new string[] { "Browser", "Path" })] [Nullable(new byte[] { 0, 1, 2 })] ValueTuple<ChromiumBrowser, string> b)
    {
      return new ValueTuple<string, string>(b.Item1.Name, b.Item2);
    }

    // Token: 0x04000BF3 RID: 3059
    public const string NativeHostName = "com.heyclicky.browser";

    // Token: 0x04000BF4 RID: 3060
    [Nullable(2)]
    private static volatile string lastConnectedBrowserName;

    // Token: 0x04000BF5 RID: 3061
    [Nullable(2)]
    private static volatile ChromiumBrowsers.PreferredCache preferredCache;

    // Token: 0x0200060B RID: 1547
    [Nullable(0)]
    private sealed class PreferredCache : IEquatable<ChromiumBrowsers.PreferredCache>
    {
      // Token: 0x060037E3 RID: 14307 RVA: 0x0044BAB8 File Offset: 0x0044BAB8
      public PreferredCache(DateTime At, [TupleElementNames(new string[] { "Name", "Executable" })] [Nullable(new byte[] { 0, 1, 1 })] ValueTuple<string, string>? Browser)
      {
        this.At = At;
        this.Browser = Browser;
        base..ctor();
      }

      // Token: 0x170009B2 RID: 2482
      // (get) Token: 0x060037E4 RID: 14308 RVA: 0x0044BAD0 File Offset: 0x0044BAD0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ChromiumBrowsers.PreferredCache);
        }
      }

      // Token: 0x170009B3 RID: 2483
      // (get) Token: 0x060037E5 RID: 14309 RVA: 0x0044BADC File Offset: 0x0044BADC
      // (set) Token: 0x060037E6 RID: 14310 RVA: 0x0044BAE4 File Offset: 0x0044BAE4
      public DateTime At { get; set; }

      // Token: 0x170009B4 RID: 2484
      // (get) Token: 0x060037E7 RID: 14311 RVA: 0x0044BAF0 File Offset: 0x0044BAF0
      // (set) Token: 0x060037E8 RID: 14312 RVA: 0x0044BAF8 File Offset: 0x0044BAF8
      [TupleElementNames(new string[] { "Name", "Executable" })]
      [Nullable(new byte[] { 0, 1, 1 })]
      public ValueTuple<string, string>? Browser
      {
        [return: TupleElementNames(new string[] { "Name", "Executable" })]
        [return: Nullable(new byte[] { 0, 1, 1 })]
        get;
        [param: TupleElementNames(new string[] { "Name", "Executable" })]
        [param: Nullable(new byte[] { 0, 1, 1 })]
        set;
      }

      // Token: 0x060037E9 RID: 14313 RVA: 0x0044BB04 File Offset: 0x0044BB04
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("PreferredCache");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060037EA RID: 14314 RVA: 0x0044BB50 File Offset: 0x0044BB50
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("At = ");
        builder.Append(this.At.ToString());
        builder.Append(", Browser = ");
        builder.Append(this.Browser.ToString());
        return true;
      }

      // Token: 0x060037EB RID: 14315 RVA: 0x0044BBB4 File Offset: 0x0044BBB4
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ChromiumBrowsers.PreferredCache left, ChromiumBrowsers.PreferredCache right)
      {
        return !(left == right);
      }

      // Token: 0x060037EC RID: 14316 RVA: 0x0044BBC0 File Offset: 0x0044BBC0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ChromiumBrowsers.PreferredCache left, ChromiumBrowsers.PreferredCache right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060037ED RID: 14317 RVA: 0x0044BBD4 File Offset: 0x0044BBD4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DateTime>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<ValueTuple<string, string>?>.Default.GetHashCode(this.<Browser>k__BackingField);
      }

      // Token: 0x060037EE RID: 14318 RVA: 0x0044BC14 File Offset: 0x0044BC14
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ChromiumBrowsers.PreferredCache);
      }

      // Token: 0x060037EF RID: 14319 RVA: 0x0044BC24 File Offset: 0x0044BC24
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ChromiumBrowsers.PreferredCache other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DateTime>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<ValueTuple<string, string>?>.Default.Equals(this.<Browser>k__BackingField, other.<Browser>k__BackingField));
      }

      // Token: 0x060037F1 RID: 14321 RVA: 0x0044BC88 File Offset: 0x0044BC88
      [CompilerGenerated]
      private PreferredCache(ChromiumBrowsers.PreferredCache original)
      {
        this.At = original.<At>k__BackingField;
        this.Browser = original.<Browser>k__BackingField;
      }

      // Token: 0x060037F2 RID: 14322 RVA: 0x0044BCA8 File Offset: 0x0044BCA8
      [CompilerGenerated]
      public void Deconstruct(out DateTime At, [TupleElementNames(new string[] { "Name", "Executable" })] [Nullable(new byte[] { 0, 1, 1 })] out ValueTuple<string, string>? Browser)
      {
        At = this.At;
        Browser = this.Browser;
      }
    }

    // Token: 0x0200060C RID: 1548
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400230C RID: 8972
      [Nullable(new byte[] { 0, 1 })]
      public static Func<ChromiumBrowser, double?> <0>__LastActiveTime;

      // Token: 0x0400230D RID: 8973
      [Nullable(new byte[] { 0, 1, 2 })]
      public static Func<ChromiumBrowser, string> <1>__ExecutablePath;
    }
  }
}
