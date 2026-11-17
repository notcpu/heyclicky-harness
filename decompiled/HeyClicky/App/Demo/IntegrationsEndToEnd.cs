using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Backend;
using HeyClicky.Agents.Runtime;
using HeyClicky.App.Interop;
using HeyClicky.App.Notch.Peek;
using HeyClicky.App.Services;
using HeyClicky.App.Settings;
using HeyClicky.App.UI;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Connectors;
using HeyClicky.Core.Documents;
using HeyClicky.Core.Notch;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Voice;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace HeyClicky.App.Demo
{
  // Token: 0x02000355 RID: 853
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntegrationsEndToEnd
  {
    // Token: 0x0600251C RID: 9500 RVA: 0x00639964 File Offset: 0x00639964
    public IntegrationsEndToEnd(AppHost host, string outDir, Action<string> say)
    {
    }

    // Token: 0x1700069F RID: 1695
    // (get) Token: 0x0600251D RID: 9501 RVA: 0x006399F0 File Offset: 0x006399F0
    private SettingsContent View
    {
      get
      {
        return NotchSettingsTab.ShownContent;
      }
    }

    // Token: 0x0600251E RID: 9502 RVA: 0x006399F8 File Offset: 0x006399F8
    private void Check(string name, bool ok, string detail = "")
    {
      if (ok)
      {
        this.passed++;
      }
      else
      {
        this.failed++;
      }
      Action<string> action = this.<say>P;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 3);
      defaultInterpolatedStringHandler.AppendLiteral("integrations: ");
      defaultInterpolatedStringHandler.AppendFormatted(ok ? "PASS" : "FAIL");
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(name);
      defaultInterpolatedStringHandler.AppendFormatted((detail.Length > 0) ? (" (" + detail + ")") : "");
      action(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x0600251F RID: 9503 RVA: 0x00639AA0 File Offset: 0x00639AA0
    private static Task Wait(int ms)
    {
      return Task.Delay(ms);
    }

    // Token: 0x06002520 RID: 9504 RVA: 0x00639AA8 File Offset: 0x00639AA8
    private static async Task<bool> Until(Func<bool> condition, double seconds)
    {
      Stopwatch sw = Stopwatch.StartNew();
      while (sw.Elapsed.TotalSeconds < seconds)
      {
        if (condition())
        {
          return true;
        }
        await Task.Delay(250);
      }
      return condition();
    }

    // Token: 0x06002521 RID: 9505 RVA: 0x00639AF4 File Offset: 0x00639AF4
    private static IEnumerable<DependencyObject> Descendants(DependencyObject root)
    {
      IntegrationsEndToEnd.<Descendants>d__17 <Descendants>d__ = new IntegrationsEndToEnd.<Descendants>d__17(-2);
      <Descendants>d__.<>3__root = root;
      return <Descendants>d__;
    }

    // Token: 0x06002522 RID: 9506 RVA: 0x00639B04 File Offset: 0x00639B04
    [NullableContext(2)]
    private TextBlock FindText([Nullable(1)] string text, DependencyObject within = null)
    {
      return IntegrationsEndToEnd.Descendants(within ?? this.View).OfType<TextBlock>().FirstOrDefault<TextBlock>((TextBlock t) => t.Text == text);
    }

    // Token: 0x06002523 RID: 9507 RVA: 0x00639B44 File Offset: 0x00639B44
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<T> Find<T>([Nullable(new byte[] { 1, 2 })] Func<T> find) where T : class
    {
      for (int attempt = 0; attempt < 15; attempt++)
      {
        this.View.UpdateLayout();
        T t = find();
        if (t != null)
        {
          return t;
        }
        await Task.Delay(200);
      }
      return default(T);
    }

    // Token: 0x06002524 RID: 9508 RVA: 0x00639B90 File Offset: 0x00639B90
    private async Task<bool> Click(string text, [Nullable(2)] DependencyObject within = null)
    {
      DependencyObject dependencyObject = await this.Find<TextBlock>(() => this.FindText(text, within));
      bool flag;
      if (dependencyObject == null)
      {
        Action<string> action = this.<say>P;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
        defaultInterpolatedStringHandler.AppendLiteral("integrations: no \"");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("\" on screen (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(IntegrationsEndToEnd.Descendants(within ?? this.View).OfType<TextBlock>().Count<TextBlock>());
        defaultInterpolatedStringHandler.AppendLiteral(" texts)");
        action(defaultInterpolatedStringHandler.ToStringAndClear());
        flag = false;
      }
      else
      {
        while (dependencyObject != null)
        {
          Pressable pressable = dependencyObject as Pressable;
          if (pressable != null)
          {
            pressable.RaiseClick();
            return true;
          }
          dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
        }
        flag = false;
      }
      return flag;
    }

    // Token: 0x06002525 RID: 9509 RVA: 0x00639BE4 File Offset: 0x00639BE4
    private async Task<bool> ClickInRow(string name, string button)
    {
      Func<TextBlock, bool> <>9__2;
      Func<Grid, bool> <>9__1;
      Grid grid = await this.Find<Grid>(delegate
      {
        IEnumerable<Grid> enumerable = IntegrationsEndToEnd.Descendants(this.View).OfType<Grid>();
        Func<Grid, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = delegate(Grid g)
          {
            string text = g.Tag as string;
            if (text != null && text.StartsWith("connector:", StringComparison.Ordinal))
            {
              IEnumerable<TextBlock> enumerable2 = IntegrationsEndToEnd.Descendants(g).OfType<TextBlock>();
              Func<TextBlock, bool> func2;
              if ((func2 = <>9__2) == null)
              {
                func2 = (<>9__2 = (TextBlock t) => t.Text == name);
              }
              return enumerable2.Any<TextBlock>(func2);
            }
            return false;
          });
        }
        return enumerable.FirstOrDefault<Grid>(func);
      });
      bool flag;
      if (grid == null)
      {
        Action<string> action = this.<say>P;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
        defaultInterpolatedStringHandler.AppendLiteral("integrations: no row for ");
        defaultInterpolatedStringHandler.AppendFormatted(name);
        defaultInterpolatedStringHandler.AppendLiteral(" (");
        defaultInterpolatedStringHandler.AppendFormatted<int>(IntegrationsEndToEnd.Descendants(this.View).OfType<Grid>().Count<Grid>(delegate(Grid g)
        {
          string text2 = g.Tag as string;
          return text2 != null && text2.StartsWith("connector:", StringComparison.Ordinal);
        }));
        defaultInterpolatedStringHandler.AppendLiteral(" connector rows)");
        action(defaultInterpolatedStringHandler.ToStringAndClear());
        flag = false;
      }
      else
      {
        flag = await this.Click(button, grid);
      }
      return flag;
    }

    // Token: 0x06002526 RID: 9510 RVA: 0x00639C38 File Offset: 0x00639C38
    private async Task Type(string placeholder, string text)
    {
      Func<TextBox, bool> <>9__1;
      Func<PasswordBox, bool> <>9__2;
      Control control = await this.Find<Control>(delegate
      {
        IEnumerable<TextBox> enumerable = IntegrationsEndToEnd.Descendants(this.View).OfType<TextBox>();
        Func<TextBox, bool> func;
        if ((func = <>9__1) == null)
        {
          func = (<>9__1 = (TextBox b) => b.PlaceholderText == placeholder);
        }
        TextBox textBox2;
        if ((textBox2 = enumerable.FirstOrDefault<TextBox>(func)) == null)
        {
          IEnumerable<PasswordBox> enumerable2 = IntegrationsEndToEnd.Descendants(this.View).OfType<PasswordBox>();
          Func<PasswordBox, bool> func2;
          if ((func2 = <>9__2) == null)
          {
            func2 = (<>9__2 = (PasswordBox b) => b.PlaceholderText == placeholder);
          }
          textBox2 = enumerable2.FirstOrDefault<PasswordBox>(func2);
        }
        return textBox2;
      });
      TextBox textBox = control as TextBox;
      if (textBox != null)
      {
        textBox.Text = text;
      }
      else
      {
        PasswordBox passwordBox = control as PasswordBox;
        if (passwordBox != null)
        {
          passwordBox.Password = text;
          List<PasswordBox> list = IntegrationsEndToEnd.Descendants(this.View).OfType<PasswordBox>().ToList<PasswordBox>();
          Action<string> action = this.<say>P;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(68, 3);
          defaultInterpolatedStringHandler.AppendLiteral("integrations: typed into a password box (");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" on screen, loaded=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(passwordBox.IsLoaded);
          defaultInterpolatedStringHandler.AppendLiteral(", same=");
          defaultInterpolatedStringHandler.AppendFormatted<bool>(list.FirstOrDefault<PasswordBox>() == passwordBox);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          action(defaultInterpolatedStringHandler.ToStringAndClear());
        }
        else
        {
          this.<say>P("integrations: no field \"" + placeholder + "\"");
        }
      }
    }

    // Token: 0x06002527 RID: 9511 RVA: 0x00639C8C File Offset: 0x00639C8C
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<JsonObject> MockState()
    {
      JsonObject jsonObject;
      try
      {
        jsonObject = JsonNode.Parse(await this.http.GetStringAsync(this.mock + "/__state"), null, default(JsonDocumentOptions)) as JsonObject;
      }
      catch (Exception ex)
      {
        this.<say>P("integrations: mock state: " + ex.Message);
        jsonObject = null;
      }
      return jsonObject;
    }

    // Token: 0x06002528 RID: 9512 RVA: 0x00639CD0 File Offset: 0x00639CD0
    private static int Int([Nullable(2)] JsonObject o, string key)
    {
      JsonValue jsonValue = ((o != null) ? o[key] : null) as JsonValue;
      int num;
      if (jsonValue == null || !jsonValue.TryGetValue<int>(out num))
      {
        return 0;
      }
      return num;
    }

    // Token: 0x06002529 RID: 9513 RVA: 0x00639D00 File Offset: 0x00639D00
    private async Task<int> OpenPendingAuthorizationUrlsAsync()
    {
      int num;
      if (!File.Exists(this.urlFile))
      {
        num = 0;
      }
      else
      {
        List<string> lines = (from l in File.ReadAllLines(this.urlFile)
          where l.Trim().Length > 0
          select l).ToList<string>();
        int opened = 0;
        while (this.urlLinesSeen < lines.Count)
        {
          string url = lines[this.urlLinesSeen].Trim();
          int i = opened;
          opened = i + 1;
          if (!(Environment.GetEnvironmentVariable("HEYCLICKY_E2E_BROWSER") == "edge"))
          {
            try
            {
              using (HttpClient browser = new HttpClient(new HttpClientHandler
              {
                AllowAutoRedirect = true
              })
              {
                Timeout = TimeSpan.FromSeconds(30L)
              })
              {
                string text = await browser.GetStringAsync(url);
                Action<string> action = this.<say>P;
                DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 3);
                defaultInterpolatedStringHandler.AppendLiteral("integrations: followed the sign-in (");
                defaultInterpolatedStringHandler.AppendFormatted(new Uri(url).Host);
                defaultInterpolatedStringHandler.AppendLiteral(":");
                defaultInterpolatedStringHandler.AppendFormatted<int>(new Uri(url).Port);
                defaultInterpolatedStringHandler.AppendLiteral(") → ");
                defaultInterpolatedStringHandler.AppendFormatted((text.Contains("Signed in", StringComparison.Ordinal) || text.Contains("Authentication complete", StringComparison.OrdinalIgnoreCase)) ? "callback page" : (text.Length.ToString() + " chars"));
                action(defaultInterpolatedStringHandler.ToStringAndClear());
              }
              HttpClient browser = null;
            }
            catch (Exception ex)
            {
              this.<say>P("integrations: browser step: " + ex.Message);
            }
            goto IL_0399;
          }
          IEnumerable<string> enumerable = new string[] { "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", "C:\\Program Files\\Microsoft\\Edge\\Application\\msedge.exe" };
          Func<string, bool> func;
          if ((func = IntegrationsEndToEnd.<>O.<0>__Exists) == null)
          {
            func = (IntegrationsEndToEnd.<>O.<0>__Exists = new Func<string, bool>(File.Exists));
          }
          string text2 = enumerable.FirstOrDefault<string>(func);
          if (text2 == null)
          {
            this.<say>P("integrations: no msedge.exe");
          }
          else
          {
            string text3 = Path.Combine(Path.GetTempPath(), "heyclicky-e2e-edge-" + Guid.NewGuid().ToString("N").Substring(0, 8));
            ProcessStartInfo processStartInfo = new ProcessStartInfo(text2)
            {
              UseShellExecute = false
            };
            foreach (string text4 in new string[]
            {
              "--user-data-dir=" + text3,
              "--guest",
              "--no-first-run",
              "--no-default-browser-check",
              "--new-window",
              url
            })
            {
              processStartInfo.ArgumentList.Add(text4);
            }
            Process process = Process.Start(processStartInfo);
            if (process != null)
            {
              this.browsers.Add(process);
              Action<string> action2 = this.<say>P;
              DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(57, 1);
              defaultInterpolatedStringHandler.AppendLiteral("integrations: opened the sign-in in msedge --guest (pid ");
              defaultInterpolatedStringHandler.AppendFormatted<int>(process.Id);
              defaultInterpolatedStringHandler.AppendLiteral(")");
              action2(defaultInterpolatedStringHandler.ToStringAndClear());
              goto IL_0399;
            }
            goto IL_0399;
          }
          IL_03A0:
          this.urlLinesSeen++;
          continue;
          IL_0399:
          url = null;
          goto IL_03A0;
        }
        num = opened;
      }
      return num;
    }

    // Token: 0x0600252A RID: 9514 RVA: 0x00639D44 File Offset: 0x00639D44
    private async Task<bool> SignInFlowAsync(string id, double seconds)
    {
      Stopwatch sw = Stopwatch.StartNew();
      while (sw.Elapsed.TotalSeconds < seconds)
      {
        await this.OpenPendingAuthorizationUrlsAsync();
        ConnectorSignInKind kind = ConnectorAuth.StateFor(id).Kind;
        bool flag;
        if (kind == ConnectorSignInKind.SignedIn)
        {
          flag = true;
        }
        else
        {
          if (kind != ConnectorSignInKind.SignInUnavailable && kind != ConnectorSignInKind.Failed)
          {
            await Task.Delay(300);
            continue;
          }
          Action<string> action = this.<say>P;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
          defaultInterpolatedStringHandler.AppendLiteral("integrations: sign-in ended ");
          defaultInterpolatedStringHandler.AppendFormatted<ConnectorSignInKind>(kind);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(ConnectorAuth.StateFor(id).Text);
          action(defaultInterpolatedStringHandler.ToStringAndClear());
          flag = false;
        }
        return flag;
      }
      return false;
    }

    // Token: 0x0600252B RID: 9515 RVA: 0x00639D98 File Offset: 0x00639D98
    private void CloseBrowsers()
    {
      foreach (Process process in this.browsers)
      {
        try
        {
          if (!process.HasExited)
          {
            process.Kill(true);
            Action<string> action = this.<say>P;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
            defaultInterpolatedStringHandler.AppendLiteral("integrations: closed msedge (pid ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(process.Id);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            action(defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
        catch (Exception ex)
        {
          Action<string> action2 = this.<say>P;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
          defaultInterpolatedStringHandler.AppendLiteral("integrations: close msedge ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(process.Id);
          defaultInterpolatedStringHandler.AppendLiteral(": ");
          defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
          action2(defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      this.browsers.Clear();
    }

    // Token: 0x0600252C RID: 9516 RVA: 0x00639EA4 File Offset: 0x00639EA4
    [return: Nullable(2)]
    private CustomConnector Connector(string name)
    {
      return CustomConnectorStore.Load().FirstOrDefault<CustomConnector>((CustomConnector c) => c.Name == name);
    }

    // Token: 0x0600252D RID: 9517 RVA: 0x00639ED4 File Offset: 0x00639ED4
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<CustomConnector> AddThroughEditorAsync(string name, string url, string auth, [Nullable(2)] string apiKey = null, [Nullable(2)] string clientId = null, [Nullable(2)] string headers = null)
    {
      await this.Click("Add custom connector", null);
      await IntegrationsEndToEnd.Wait(400);
      await this.Type("e.g. Meta Ads", name);
      await this.Type("e.g. https://mcp.example.com/mcp", url);
      await this.Click(auth, null);
      await IntegrationsEndToEnd.Wait(200);
      if (apiKey != null)
      {
        await this.Type("Paste your key", apiKey);
      }
      if (clientId != null || headers != null)
      {
        await this.Click("Advanced", null);
        await IntegrationsEndToEnd.Wait(250);
        if (clientId != null)
        {
          await this.Type("e.g. 123456789012345", clientId);
        }
        if (headers != null)
        {
          await this.Type("e.g. X-Api-Key: …", headers);
        }
      }
      await Snapshot.SaveAsync(this.View, "integrations-editor-" + name.Replace(' ', '-').ToLowerInvariant());
      await this.Click("Add connector", null);
      await IntegrationsEndToEnd.Wait(600);
      return this.Connector(name);
    }

    // Token: 0x0600252E RID: 9518 RVA: 0x00639F4C File Offset: 0x00639F4C
    public async Task<bool> RunAsync()
    {
      IntegrationsEndToEnd.<>c__DisplayClass30_0 CS$<>8__locals1 = new IntegrationsEndToEnd.<>c__DisplayClass30_0();
      CS$<>8__locals1.<>4__this = this;
      RealStack real = this.<host>P.Real;
      this.urlFile = Path.Combine(this.<outDir>P, "auth-urls.txt");
      File.WriteAllText(this.urlFile, "");
      Environment.SetEnvironmentVariable("HEYCLICKY_DEV_AUTH_URL_FILE", this.urlFile);
      Environment.SetEnvironmentVariable("HEYCLICKY_E2E_NO_BROWSER", "1");
      JsonObject jsonObject = await this.MockState();
      this.Check("mock MCP/OAuth server is up", jsonObject != null, this.mock);
      bool flag;
      if (jsonObject == null)
      {
        flag = false;
      }
      else
      {
        foreach (CustomConnector customConnector in from c in CustomConnectorStore.Load()
          where c.Name.StartsWith("Mock ", StringComparison.Ordinal)
          select c)
        {
          CustomConnectorStore.Remove(customConnector.Id);
          ConnectorAuth.Removed(customConnector.Id);
        }
        CS$<>8__locals1.originalVault = real.Core.Settings.Current.ObsidianVaultPath;
        this.<host>P.OpenSettings(NotchSettingsSection.Integrations);
        await IntegrationsEndToEnd.Wait(2500);
        await Snapshot.SaveAsync(this.View, "integrations-page");
        string text = "catalog loaded with remote logos";
        AgentIntegrationCatalog catalog = ConnectedApps.Catalog;
        bool flag2 = catalog != null && catalog.Integrations.Count > 0;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
        AgentIntegrationCatalog catalog2 = ConnectedApps.Catalog;
        defaultInterpolatedStringHandler.AppendFormatted<int>((catalog2 != null) ? catalog2.Integrations.Count : 0);
        defaultInterpolatedStringHandler.AppendLiteral(" apps");
        this.Check(text, flag2, defaultInterpolatedStringHandler.ToStringAndClear());
        CS$<>8__locals1.cache = AppPaths.Combine(new string[] { "logo-cache" });
        await IntegrationsEndToEnd.Until(() => Directory.Exists(CS$<>8__locals1.cache) && Directory.GetFiles(CS$<>8__locals1.cache).Length != 0, 15.0);
        string text2 = "remote catalog logos cached on disk";
        bool flag3 = Directory.Exists(CS$<>8__locals1.cache) && Directory.GetFiles(CS$<>8__locals1.cache).Length != 0;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
        defaultInterpolatedStringHandler.AppendFormatted<int>(Directory.Exists(CS$<>8__locals1.cache) ? Directory.GetFiles(CS$<>8__locals1.cache).Length : 0);
        defaultInterpolatedStringHandler.AppendLiteral(" files");
        this.Check(text2, flag3, defaultInterpolatedStringHandler.ToStringAndClear());
        JsonObject before = await this.MockState();
        CS$<>8__locals1.oauth = await this.AddThroughEditorAsync("Mock Tasks", this.mock + "/mcp", "Sign in with browser", null, null, null);
        this.Check("editor saved the browser sign-in connector", CS$<>8__locals1.oauth != null && CS$<>8__locals1.oauth.IsRemote && !CS$<>8__locals1.oauth.HasApiKey, "");
        if (CS$<>8__locals1.oauth != null)
        {
          bool signedIn = await this.SignInFlowAsync(CS$<>8__locals1.oauth.Id, 120.0);
          JsonObject jsonObject2 = await this.MockState();
          this.Check("browser sign-in (dynamic registration) completed", signedIn, ConnectorAuth.StateFor(CS$<>8__locals1.oauth.Id).ToString());
          this.Check("the server registered a client dynamically", IntegrationsEndToEnd.Int(jsonObject2, "registrations") > IntegrationsEndToEnd.Int(before, "registrations"), "");
          string text3 = "sign-in asked only for the resource's scopes";
          string text4;
          if (jsonObject2 == null)
          {
            text4 = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject2["lastScope"];
            text4 = ((jsonNode != null) ? jsonNode.ToString() : null);
          }
          bool flag4 = text4 == "mcp:read mcp:write";
          string text5;
          if (jsonObject2 == null)
          {
            text5 = null;
          }
          else
          {
            JsonNode jsonNode2 = jsonObject2["lastScope"];
            text5 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
          }
          this.Check(text3, flag4, text5 ?? "none");
          CustomConnectorSecrets signInSecrets = CustomConnectorStore.ReadSecrets(CS$<>8__locals1.oauth.Id);
          string text6 = "the sign-in's token and registered client are kept in the connector's secrets";
          string text7 = signInSecrets.BearerToken;
          bool flag5;
          if (text7 != null && text7.Length > 0)
          {
            text7 = signInSecrets.RegisteredClientId;
            if (text7 != null && text7.Length > 0)
            {
              CustomConnector customConnector2 = this.Connector("Mock Tasks");
              flag5 = customConnector2 != null && customConnector2.TokenFromSignIn;
              goto IL_0808;
            }
          }
          flag5 = false;
          IL_0808:
          this.Check(text6, flag5, "");
          await IntegrationsEndToEnd.Until(delegate
          {
            CustomConnector customConnector7 = CS$<>8__locals1.<>4__this.Connector("Mock Tasks");
            return ((customConnector7 != null) ? customConnector7.DerivedCapabilityDescription : null) != null;
          }, 20.0);
          string text8 = "capability one-liner from tools/list";
          CustomConnector customConnector3 = this.Connector("Mock Tasks");
          string text9 = ((customConnector3 != null) ? customConnector3.DerivedCapabilityDescription : null);
          bool flag6 = text9 != null && text9.StartsWith("Mock Tasks: find, create, and update", StringComparison.Ordinal);
          CustomConnector customConnector4 = this.Connector("Mock Tasks");
          this.Check(text8, flag6, ((customConnector4 != null) ? customConnector4.DerivedCapabilityDescription : null) ?? "none");
          CustomConnectorRegistration customConnectorRegistration = CustomConnectorStore.Registrations().FirstOrDefault<CustomConnectorRegistration>((CustomConnectorRegistration r) => r.ServerKey == CS$<>8__locals1.oauth.ServerKey);
          this.Check("a job gets the sign-in's token from the connector's secrets", customConnectorRegistration != null && customConnectorRegistration.Environment.Values.Contains(signInSecrets.BearerToken), "");
          await IntegrationsEndToEnd.Wait(800);
          await Snapshot.SaveAsync(this.View, "integrations-signed-in");
          this.CloseBrowsers();
          signInSecrets = null;
        }
        CS$<>8__locals1.keyed = await this.AddThroughEditorAsync("Mock Key", this.mock + "/mcp-key", "API key", "mock-api-key-123", null, null);
        this.Check("editor saved the API-key connector", CS$<>8__locals1.keyed != null && CS$<>8__locals1.keyed.HasApiKey && CS$<>8__locals1.keyed.UsesPlainBearerToken, "");
        if (CS$<>8__locals1.keyed != null)
        {
          await IntegrationsEndToEnd.Until(() => ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded, 20.0);
          this.Check("API key verified by the server", ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded, ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).ToString());
          await this.ClickInRow("Mock Key", "Edit");
          await IntegrationsEndToEnd.Wait(500);
          await this.Type("Paste your key", "wrong-key");
          await IntegrationsEndToEnd.Wait(300);
          await this.Click("Save changes", null);
          await IntegrationsEndToEnd.Until(() => ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.TokenRejected, 20.0);
          bool flag7 = ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.TokenRejected;
          if (flag7)
          {
            TaskAwaiter<TextBlock> taskAwaiter = this.Find<TextBlock>(() => CS$<>8__locals1.<>4__this.FindText("Edit token", null)).GetAwaiter();
            if (!taskAwaiter.IsCompleted)
            {
              await taskAwaiter;
              TaskAwaiter<TextBlock> taskAwaiter2;
              taskAwaiter = taskAwaiter2;
              taskAwaiter2 = default(TaskAwaiter<TextBlock>);
            }
            flag7 = taskAwaiter.GetResult() != null;
          }
          this.Check("a refused key reads Token rejected with Edit token", flag7, "");
          await Snapshot.SaveAsync(this.View, "integrations-token-rejected");
        }
        CS$<>8__locals1.headered = await this.AddThroughEditorAsync("Mock Header", this.plainMock + "/mcp-header", "None", null, null, "X-Api-Key: mock-header-secret");
        string text10 = "editor saved the custom-header connector";
        bool flag8;
        if (CS$<>8__locals1.headered != null)
        {
          IReadOnlyList<string> headerNames = CS$<>8__locals1.headered.HeaderNames;
          if (headerNames != null && headerNames.Count == 1)
          {
            flag8 = CustomConnectorStore.ReadSecrets(CS$<>8__locals1.headered.Id).Headers.GetValueOrDefault("X-Api-Key") == "mock-header-secret";
            goto IL_1025;
          }
        }
        flag8 = false;
        IL_1025:
        this.Check(text10, flag8, "");
        if (CS$<>8__locals1.headered != null)
        {
          await IntegrationsEndToEnd.Until(delegate
          {
            CustomConnector customConnector8 = CS$<>8__locals1.<>4__this.Connector("Mock Header");
            return ((customConnector8 != null) ? customConnector8.DerivedCapabilityDescription : null) != null;
          }, 20.0);
          string text11 = "header connector reached with its header";
          CustomConnector customConnector5 = this.Connector("Mock Header");
          this.Check(text11, ((customConnector5 != null) ? customConnector5.DerivedCapabilityDescription : null) != null, "");
          await IntegrationsEndToEnd.Until(() => ConnectorAuth.StateFor(CS$<>8__locals1.headered.Id).Kind == ConnectorSignInKind.NotNeeded, 40.0);
          this.Check("a server without OAuth needs no sign-in (probe)", ConnectorAuth.StateFor(CS$<>8__locals1.headered.Id).Kind == ConnectorSignInKind.NotNeeded, ConnectorAuth.StateFor(CS$<>8__locals1.headered.Id).ToString());
          CustomConnectorRegistration customConnectorRegistration2 = CustomConnectorStore.Registrations().FirstOrDefault<CustomConnectorRegistration>((CustomConnectorRegistration r) => r.ServerKey == CS$<>8__locals1.headered.ServerKey);
          string text12 = "header goes to the runtime as an env var reference";
          CustomConnectorTransport.Remote remote = ((customConnectorRegistration2 != null) ? customConnectorRegistration2.Transport : null) as CustomConnectorTransport.Remote;
          bool flag9;
          if (remote != null)
          {
            IReadOnlyDictionary<string, string> headerEnvironmentVariables = remote.HeaderEnvironmentVariables;
            if (headerEnvironmentVariables != null && headerEnvironmentVariables.Count == 1)
            {
              flag9 = customConnectorRegistration2.Environment.Values.Contains("mock-header-secret");
              goto IL_120C;
            }
          }
          flag9 = false;
          IL_120C:
          this.Check(text12, flag9, "");
        }
        CustomConnector client = await this.AddThroughEditorAsync("Mock Client", this.mock + "/mcp", "Sign in with browser", null, "preregistered-client", null);
        this.Check("editor saved the client-id connector", client != null && client.UsesBrowserSignIn, "");
        if (client != null)
        {
          this.Check("app-side sign-in (client id, PKCE, loopback 48611) completed", await this.SignInFlowAsync(client.Id, 90.0), ConnectorAuth.StateFor(client.Id).ToString());
          CustomConnectorSecrets signInSecrets = CustomConnectorStore.ReadSecrets(client.Id);
          string text13 = "tokens stored encrypted with refresh bookkeeping";
          string bearerToken = signInSecrets.BearerToken;
          this.Check(text13, bearerToken != null && bearerToken.StartsWith("at-", StringComparison.Ordinal) && signInSecrets.RefreshToken != null && signInSecrets.TokenEndpoint != null, "");
          byte[] array = File.ReadAllBytes(AppPaths.Combine(new string[] { "connectors.secrets" }));
          string text14 = "secrets file is DPAPI ciphertext";
          string bearerToken2 = signInSecrets.BearerToken;
          this.Check(text14, bearerToken2 != null && !Encoding.UTF8.GetString(array).Contains(bearerToken2, StringComparison.Ordinal), "");
          await this.http.GetStringAsync(this.mock + "/__expire");
          CustomConnectorStore.StoreSignInTokens(client.Id, new McpOAuthTokens(signInSecrets.BearerToken, signInSecrets.RefreshToken, new DateTimeOffset?(DateTimeOffset.UtcNow.AddMinutes(1.0))), new Uri(signInSecrets.TokenEndpoint), null, null);
          int refreshesBefore = IntegrationsEndToEnd.Int(await this.MockState(), "refreshes");
          await real.Agents.EnsureRuntimeReadyAsync(default(CancellationToken));
          CustomConnectorSecrets refreshed = CustomConnectorStore.ReadSecrets(client.Id);
          this.Check("an expiring token is refreshed before the runtime uses it", IntegrationsEndToEnd.Int(await this.MockState(), "refreshes") > refreshesBefore && refreshed.BearerToken != signInSecrets.BearerToken && refreshed.ExpiresAt > DateTimeOffset.UtcNow.AddMinutes(30.0), "");
          this.CloseBrowsers();
          signInSecrets = null;
          refreshed = null;
        }
        if (CS$<>8__locals1.keyed != null)
        {
          await this.ClickInRow("Mock Key", "Edit");
          await IntegrationsEndToEnd.Wait(500);
          await this.Type("Paste your key", "mock-api-key-123");
          await IntegrationsEndToEnd.Wait(300);
          await this.Click("Save changes", null);
          await IntegrationsEndToEnd.Until(() => ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded, 20.0);
          this.Check("a fixed key is accepted again", ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded, ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).ToString());
          ConnectorAuth.Removed(CS$<>8__locals1.keyed.Id);
          int refreshesBefore = IntegrationsEndToEnd.Int(await this.MockState(), "calls");
          await ConnectorAuth.OnLaunchAsync();
          await IntegrationsEndToEnd.Until(() => ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded, 15.0);
          bool flag7 = ConnectorAuth.StateFor(CS$<>8__locals1.keyed.Id).Kind == ConnectorSignInKind.NotNeeded;
          if (flag7)
          {
            TaskAwaiter<JsonObject> taskAwaiter3 = this.MockState().GetAwaiter();
            if (!taskAwaiter3.IsCompleted)
            {
              await taskAwaiter3;
              TaskAwaiter<JsonObject> taskAwaiter4;
              taskAwaiter3 = taskAwaiter4;
              taskAwaiter4 = default(TaskAwaiter<JsonObject>);
            }
            flag7 = IntegrationsEndToEnd.Int(taskAwaiter3.GetResult(), "calls") > refreshesBefore;
          }
          this.Check("launch-time token check asks the server", flag7, "");
        }
        CS$<>8__locals1.vault = Path.Combine(this.<outDir>P, "Test Vault");
        Directory.CreateDirectory(Path.Combine(CS$<>8__locals1.vault, ".obsidian"));
        File.WriteAllText(Path.Combine(CS$<>8__locals1.vault, "Welcome.md"), "# Welcome\n");
        Task.Run(delegate
        {
          IntegrationsEndToEnd.<>c__DisplayClass30_0.<<RunAsync>b__3>d <<RunAsync>b__3>d;
          <<RunAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
          <<RunAsync>b__3>d.<>4__this = CS$<>8__locals1;
          <<RunAsync>b__3>d.<>1__state = -1;
          <<RunAsync>b__3>d.<>t__builder.Start<IntegrationsEndToEnd.<>c__DisplayClass30_0.<<RunAsync>b__3>d>(ref <<RunAsync>b__3>d);
          return <<RunAsync>b__3>d.<>t__builder.Task;
        });
        AgentIntegration agentIntegration = ObsidianVault.Row();
        this.Check("Obsidian row before a vault", agentIntegration.Status == "not_connected", "");
        bool flag10 = ObsidianVault.Configure(Native.GetForegroundWindow());
        this.Check("picker cancelled leaves Obsidian unset", !flag10 && ObsidianVault.PersistedPath == null, "");
        real.Core.Settings.Update(delegate(AppSettings s)
        {
          s.ObsidianVaultPath = CS$<>8__locals1.vault;
        });
        await IntegrationsEndToEnd.Wait(500);
        await real.Agents.EnsureRuntimeReadyAsync(default(CancellationToken));
        string text15 = File.ReadAllText(Path.Combine(real.Agents.CodexHome, "clicky-model-instructions.md"));
        this.Check("vault reaches the agents' brief", text15.Contains(CS$<>8__locals1.vault, StringComparison.OrdinalIgnoreCase), "");
        this.View.Show(new SettingsWindow.Page?(SettingsWindow.Page.Integrations));
        await IntegrationsEndToEnd.Wait(1200);
        this.Check("Obsidian shows as connected", ObsidianVault.Row().Status == "connected" && this.FindText("Obsidian", null) != null, "");
        BrainUserContext brainUserContext = BrainContext.WithLocalApps(BrainUserContext.Empty);
        this.Check("user_context.custom_connectors filled", brainUserContext.CustomConnectors.Any<CustomConnectorDescription>((CustomConnectorDescription c) => c.Name == "Mock Tasks" && c.Description.Length > 0) && brainUserContext.CustomConnectors.Count >= 4, string.Join("; ", brainUserContext.CustomConnectors.Select<CustomConnectorDescription, string>((CustomConnectorDescription c) => c.Name)));
        string text16 = "user_context.connected_apps has connectors and obsidian";
        bool flag11;
        if (brainUserContext.ConnectedApps.Contains("obsidian"))
        {
          flag11 = brainUserContext.ConnectedApps.Any<string>((string a) => a.StartsWith("custom:", StringComparison.Ordinal));
        }
        else
        {
          flag11 = false;
        }
        this.Check(text16, flag11, string.Join(", ", brainUserContext.ConnectedApps));
        if (CS$<>8__locals1.headered != null)
        {
          await this.ClickInRow("Mock Header", "Remove");
          await IntegrationsEndToEnd.Wait(250);
          await this.ClickInRow("Mock Header", "Remove");
          await IntegrationsEndToEnd.Wait(300);
          bool signedIn = await this.Find<Grid>(delegate
          {
            IEnumerable<Grid> enumerable = IntegrationsEndToEnd.Descendants(CS$<>8__locals1.<>4__this.View).OfType<Grid>();
            Func<Grid, bool> func;
            if ((func = CS$<>8__locals1.<>9__25) == null)
            {
              func = (CS$<>8__locals1.<>9__25 = delegate(Grid g)
              {
                string text19 = g.Tag as string;
                return text19 != null && text19 == "departed:" + CS$<>8__locals1.headered.IntegrationId;
              });
            }
            return enumerable.FirstOrDefault<Grid>(func);
          }) != null && this.FindText("HeyClicky no longer has access.", null) != null;
          await Snapshot.SaveAsync(this.View, "integrations-departure");
          this.Check("removed connector shows the departure row", signedIn, "");
          await IntegrationsEndToEnd.Wait(1900);
          this.Check("departure row leaves after the beat", !IntegrationsEndToEnd.Descendants(this.View).OfType<Grid>().Any<Grid>(delegate(Grid g)
          {
            string text20 = g.Tag as string;
            return text20 != null && text20 == "departed:" + CS$<>8__locals1.headered.IntegrationId;
          }), "");
        }
        NotchPresenter presenter = this.<host>P.Services.Presenter as NotchPresenter;
        NotchPresenter notchPresenter = presenter;
        if (notchPresenter != null)
        {
          notchPresenter.Tabs.Select(NotchTab.Home);
        }
        await IntegrationsEndToEnd.Wait(1200);
        Grid root = this.<host>P.NotchUi.Window.Root;
        Pressable capsule = IntegrationsEndToEnd.Descendants(root).OfType<Pressable>().FirstOrDefault<Pressable>((Pressable p) => p.Tag as string == "notch-apps");
        int num;
        if (capsule != null)
        {
          num = IntegrationsEndToEnd.Descendants(capsule).OfType<FrameworkElement>().Count<FrameworkElement>(delegate(FrameworkElement e)
          {
            string text21 = e.Tag as string;
            return text21 != null && text21.StartsWith("app:", StringComparison.Ordinal);
          });
        }
        else
        {
          num = 0;
        }
        int num2 = num;
        string text17 = "Apps pill shows connected logos";
        bool flag12 = num2 == Math.Min(3, ConnectedApps.Connected().Count) && num2 > 0;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
        defaultInterpolatedStringHandler.AppendLiteral(" logos, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(ConnectedApps.Connected().Count);
        defaultInterpolatedStringHandler.AppendLiteral(" connected");
        this.Check(text17, flag12, defaultInterpolatedStringHandler.ToStringAndClear());
        await Snapshot.SaveAsync(root, "notch-home-apps-pill");
        Pressable pressable = capsule;
        if (pressable != null)
        {
          pressable.RaiseClick();
        }
        await IntegrationsEndToEnd.Wait(900);
        string text18 = "the pill opens Settings → Integrations";
        NotchPresenter notchPresenter2 = presenter;
        this.Check(text18, notchPresenter2 != null && notchPresenter2.Tabs.Selected == NotchTab.Settings && this.View.CurrentPage == SettingsWindow.Page.Integrations, "");
        string denied = Path.Combine(this.<outDir>P, "denied-notes.txt");
        File.WriteAllText(denied, "secret");
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        defaultInterpolatedStringHandler.AppendFormatted(denied);
        defaultInterpolatedStringHandler.AppendLiteral("\" /deny \"");
        defaultInterpolatedStringHandler.AppendFormatted(Environment.UserName);
        defaultInterpolatedStringHandler.AppendLiteral("\":(R)");
        IntegrationsEndToEnd.RunIcacls(defaultInterpolatedStringHandler.ToStringAndClear());
        PageContext pageContext = DocumentText.Extract(denied, 60000);
        this.Check("a file the account can't open reads as access denied", pageContext.AccessDenied, "");
        real.Core.Settings.Update(delegate(AppSettings s)
        {
          s.DocumentAccessNoticeSuppressed = false;
        });
        bool raised = real.RaiseDocumentAccessCardForTests(denied);
        await IntegrationsEndToEnd.Wait(1200);
        this.Check("the folder-access card is raised", raised && real.Core.Notch.HasLiveEvent(typeof(NotchLiveEvent.DocumentFolderAccessDenied)), "");
        this.CaptureDesktop("folder-access-card");
        Grid root2 = this.<host>P.NotchUi.Window.Root;
        Task.Run(delegate
        {
          IntegrationsEndToEnd.<>c__DisplayClass30_0.<<RunAsync>b__11>d <<RunAsync>b__11>d;
          <<RunAsync>b__11>d.<>t__builder = AsyncTaskMethodBuilder.Create();
          <<RunAsync>b__11>d.<>4__this = CS$<>8__locals1;
          <<RunAsync>b__11>d.<>1__state = -1;
          <<RunAsync>b__11>d.<>t__builder.Start<IntegrationsEndToEnd.<>c__DisplayClass30_0.<<RunAsync>b__11>d>(ref <<RunAsync>b__11>d);
          return <<RunAsync>b__11>d.<>t__builder.Task;
        });
        this.Check("Give permission is on the card", await this.Click("Give permission", root2), "");
        await IntegrationsEndToEnd.Wait(3500);
        this.Check("the card dismissed after the action", !real.Core.Notch.HasLiveEvent(typeof(NotchLiveEvent.DocumentFolderAccessDenied)), "");
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        defaultInterpolatedStringHandler.AppendFormatted(denied);
        defaultInterpolatedStringHandler.AppendLiteral("\" /remove:d \"");
        defaultInterpolatedStringHandler.AppendFormatted(Environment.UserName);
        defaultInterpolatedStringHandler.AppendLiteral("\"");
        IntegrationsEndToEnd.RunIcacls(defaultInterpolatedStringHandler.ToStringAndClear());
        try
        {
          File.Delete(denied);
        }
        catch
        {
        }
        foreach (CustomConnector customConnector6 in from c in CustomConnectorStore.Load()
          where c.Name.StartsWith("Mock ", StringComparison.Ordinal)
          select c)
        {
          CustomConnectorStore.Remove(customConnector6.Id);
          ConnectorAuth.Removed(customConnector6.Id);
        }
        real.Core.Settings.Update(delegate(AppSettings s)
        {
          s.ObsidianVaultPath = CS$<>8__locals1.originalVault;
        });
        this.CloseBrowsers();
        Environment.SetEnvironmentVariable("HEYCLICKY_DEV_AUTH_URL_FILE", null);
        NotchPresenter notchPresenter3 = presenter;
        if (notchPresenter3 != null)
        {
          notchPresenter3.DismissNotchAgentPeek();
        }
        Action<string> action = this.<say>P;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
        defaultInterpolatedStringHandler.AppendLiteral("integrations: ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.passed);
        defaultInterpolatedStringHandler.AppendLiteral(" passed, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.failed);
        defaultInterpolatedStringHandler.AppendLiteral(" failed");
        action(defaultInterpolatedStringHandler.ToStringAndClear());
        flag = this.failed == 0;
      }
      return flag;
    }

    // Token: 0x0600252F RID: 9519 RVA: 0x00639F90 File Offset: 0x00639F90
    private static void RunIcacls(string args)
    {
      using (Process process = Process.Start(new ProcessStartInfo("icacls.exe", args)
      {
        CreateNoWindow = true,
        UseShellExecute = false,
        RedirectStandardOutput = true
      }))
      {
        if (process != null)
        {
          process.WaitForExit(10000);
        }
      }
    }

    // Token: 0x06002530 RID: 9520 RVA: 0x00639FF0 File Offset: 0x00639FF0
    private void CaptureDesktop(string name)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_SCREENSHOT");
      if (environmentVariable == null || environmentVariable.Length <= 0 || !File.Exists(environmentVariable))
      {
        return;
      }
      string text = Path.Combine(this.<outDir>P, "desktop-" + name + ".png");
      string text2 = "powershell.exe";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
      defaultInterpolatedStringHandler.AppendLiteral("-NoProfile -ExecutionPolicy Bypass -File \"");
      defaultInterpolatedStringHandler.AppendFormatted(environmentVariable);
      defaultInterpolatedStringHandler.AppendLiteral("\" -Path \"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      using (Process process = Process.Start(new ProcessStartInfo(text2, defaultInterpolatedStringHandler.ToStringAndClear())
      {
        CreateNoWindow = true,
        UseShellExecute = false
      }))
      {
        if (process != null)
        {
          process.WaitForExit(15000);
        }
      }
    }

    // Token: 0x06002531 RID: 9521
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(IntegrationsEndToEnd.EnumProc cb, IntPtr l);

    // Token: 0x06002532 RID: 9522
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr h, out uint pid);

    // Token: 0x06002533 RID: 9523
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr h, StringBuilder s, int n);

    // Token: 0x06002534 RID: 9524
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetClassName(IntPtr h, StringBuilder s, int n);

    // Token: 0x06002535 RID: 9525 RVA: 0x0063A0CC File Offset: 0x0063A0CC
    private static IntPtr OwnDialog(Func<string, bool> title)
    {
      IntPtr found = (IntPtr)0;
      IntegrationsEndToEnd.EnumWindows(delegate(IntPtr h, IntPtr _)
      {
        uint num;
        IntegrationsEndToEnd.GetWindowThreadProcessId(h, out num);
        if ((ulong)num != (ulong)((long)Environment.ProcessId))
        {
          return true;
        }
        StringBuilder stringBuilder = new StringBuilder(64);
        StringBuilder stringBuilder2 = new StringBuilder(256);
        IntegrationsEndToEnd.GetClassName(h, stringBuilder, 64);
        IntegrationsEndToEnd.GetWindowText(h, stringBuilder2, 256);
        if (stringBuilder.ToString() == "#32770" && title(stringBuilder2.ToString()))
        {
          found = h;
          return false;
        }
        return true;
      }, (IntPtr)0);
      return found;
    }

    // Token: 0x06002536 RID: 9526
    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr w, IntPtr l);

    // Token: 0x040011AF RID: 4527
    [CompilerGenerated]
    private AppHost <host>P = host;

    // Token: 0x040011B0 RID: 4528
    [CompilerGenerated]
    private string <outDir>P = outDir;

    // Token: 0x040011B1 RID: 4529
    [CompilerGenerated]
    private Action<string> <say>P = say;

    // Token: 0x040011B2 RID: 4530
    private readonly string mock = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_MOCK_MCP") ?? "http://127.0.0.1:8790";

    // Token: 0x040011B3 RID: 4531
    private readonly string plainMock = Environment.GetEnvironmentVariable("HEYCLICKY_E2E_PLAIN_MCP") ?? "http://127.0.0.1:8791";

    // Token: 0x040011B4 RID: 4532
    private readonly HttpClient http = new HttpClient
    {
      Timeout = TimeSpan.FromSeconds(30L)
    };

    // Token: 0x040011B5 RID: 4533
    private readonly List<Process> browsers = new List<Process>();

    // Token: 0x040011B6 RID: 4534
    private string urlFile = "";

    // Token: 0x040011B7 RID: 4535
    private int urlLinesSeen;

    // Token: 0x040011B8 RID: 4536
    private int passed;

    // Token: 0x040011B9 RID: 4537
    private int failed;

    // Token: 0x02000A45 RID: 2629
    // (Invoke) Token: 0x06004093 RID: 16531
    [NullableContext(0)]
    private delegate bool EnumProc(IntPtr h, IntPtr l);

    // Token: 0x02000A46 RID: 2630
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04002C28 RID: 11304
      [Nullable(0)]
      public static Func<string, bool> <0>__Exists;
    }
  }
}
