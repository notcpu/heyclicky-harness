using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000021 RID: 33
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class CodexHomeManager
  {
    // Token: 0x060003C5 RID: 965 RVA: 0x000E6B18 File Offset: 0x000E6B18
    public CodexHomeManager(AgentRuntimeOptions options)
    {
    }

    // Token: 0x17000103 RID: 259
    // (get) Token: 0x060003C6 RID: 966 RVA: 0x000E6B34 File Offset: 0x000E6B34
    // (set) Token: 0x060003C7 RID: 967 RVA: 0x000E6B3C File Offset: 0x000E6B3C
    [Nullable(2)]
    public string DeliveredModelInstructions
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x17000104 RID: 260
    // (get) Token: 0x060003C8 RID: 968 RVA: 0x000E6B48 File Offset: 0x000E6B48
    public string CodexHome
    {
      get
      {
        return this.<options>P.CodexHome;
      }
    }

    // Token: 0x17000105 RID: 261
    // (get) Token: 0x060003C9 RID: 969 RVA: 0x000E6B58 File Offset: 0x000E6B58
    private string SkillsDirectory
    {
      get
      {
        return Path.Combine(this.CodexHome, "skills");
      }
    }

    // Token: 0x17000106 RID: 262
    // (get) Token: 0x060003CA RID: 970 RVA: 0x000E6B6C File Offset: 0x000E6B6C
    private string BundledInstructionsPath
    {
      get
      {
        return Path.Combine(this.<options>P.BundledResourcesDirectory, "clicky-model-instructions.md");
      }
    }

    // Token: 0x17000107 RID: 263
    // (get) Token: 0x060003CB RID: 971 RVA: 0x000E6B84 File Offset: 0x000E6B84
    private string BundledSkillsDirectory
    {
      get
      {
        return Path.Combine(this.<options>P.BundledResourcesDirectory, "skills");
      }
    }

    // Token: 0x17000108 RID: 264
    // (get) Token: 0x060003CC RID: 972 RVA: 0x000E6B9C File Offset: 0x000E6B9C
    private string IntegrationSkillsDirectory
    {
      get
      {
        return Path.Combine(this.<options>P.BundledResourcesDirectory, "integration-skills");
      }
    }

    // Token: 0x060003CD RID: 973 RVA: 0x000E6BB4 File Offset: 0x000E6BB4
    public string TaskAssetsDirectory(Guid taskId)
    {
      string text = Path.Combine(this.CodexHome, "task-assets", taskId.ToString("D"));
      Directory.CreateDirectory(text);
      return text;
    }

    // Token: 0x060003CE RID: 974 RVA: 0x000E6BDC File Offset: 0x000E6BDC
    public void PrepareRuntimeConfiguration([Nullable(2)] ComposioMcpConfiguration composio, bool areConnectedAppsTemporarilyUnreachable, IReadOnlyList<CustomConnectorRegistration> customConnectors)
    {
      object obj = this.gate;
      lock (obj)
      {
        Directory.CreateDirectory(this.CodexHome);
        Directory.CreateDirectory(this.SkillsDirectory);
        Directory.CreateDirectory(this.<options>P.ProjectsRoot);
        Dictionary<string, string> dictionary = this.SynchronizeSkills(((composio != null) ? composio.Toolkits : null) ?? Array.Empty<string>());
        string text = this.BuildModelInstructions(composio, areConnectedAppsTemporarilyUnreachable, customConnectors);
        bool flag2 = text != null && CodexHomeManager.WriteIfChanged(Path.Combine(this.CodexHome, "clicky-model-instructions.md"), text);
        this.DeliveredModelInstructions = text;
        if (flag2)
        {
          string text2 = "Agents";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
          defaultInterpolatedStringHandler.AppendLiteral("agent brief written: ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
          defaultInterpolatedStringHandler.AppendLiteral(" chars, skills=[");
          defaultInterpolatedStringHandler.AppendFormatted(string.Join<string>(',', dictionary.Keys));
          defaultInterpolatedStringHandler.AppendLiteral("]");
          Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
    }

    // Token: 0x060003CF RID: 975 RVA: 0x000E6CEC File Offset: 0x000E6CEC
    [NullableContext(2)]
    private string BuildModelInstructions(ComposioMcpConfiguration composio, bool appsUnreachable, [Nullable(1)] IReadOnlyList<CustomConnectorRegistration> customConnectors)
    {
      if (!File.Exists(this.BundledInstructionsPath))
      {
        return null;
      }
      return WindowsInstructionAdapter.Adapt(File.ReadAllText(this.BundledInstructionsPath, Encoding.UTF8)) + "\n" + this.ExternalIntegrationsInstructions(composio, appsUnreachable, customConnectors) + "\n";
    }

    // Token: 0x060003D0 RID: 976 RVA: 0x000E6D2C File Offset: 0x000E6D2C
    private string ExternalIntegrationsInstructions([Nullable(2)] ComposioMcpConfiguration composio, bool appsUnreachable, IReadOnlyList<CustomConnectorRegistration> customConnectors)
    {
      List<string> list = new List<string> { "## Connected apps" };
      if (composio != null)
      {
        string text;
        if (composio.Toolkits.Count != 0)
        {
          text = string.Join(", ", composio.Toolkits.OrderBy<string, string>((string t) => t, StringComparer.Ordinal));
        }
        else
        {
          text = "connected toolkit list unavailable";
        }
        string text2 = text;
        list.Add("- When this chat started, these apps were connected through the `composio` MCP server: " + text2 + ". Its tools are the live source of truth, since the list can change later: search them before deciding an app can't do something. Google apps connect separately (gmail, googlesheets, googlecalendar, googledrive, googledocs), so name the specific one when it's missing.");
        list.Add("- If an app the task needs isn't connected, or a call comes back expired or unauthorized, name the app and tell them to connect or reconnect it in HeyClicky Settings -> Integrations. Never run OAuth, sign-in or an auth CLI yourself, and never act on HeyClicky's own settings screen.");
        list.Add("- If Gmail refuses to send, ask them to reconnect Gmail with send permission in HeyClicky Settings -> Integrations.");
        if (composio.MultiAccountLines.Count > 0)
        {
          list.Add("- Apps with more than one connected account:\n" + string.Join("\n", composio.MultiAccountLines.Select<string, string>((string l) => "  - " + l)) + "\n" + string.Join("\n", from l in CodexHomeManager.MultipleAccountsRules.Split('\n', StringSplitOptions.None)
            select "  - " + l));
        }
      }
      else if (appsUnreachable)
      {
        list.Add("- Their connected apps couldn't be reached when this chat started (a temporary connection problem; they ARE connected). If `composio` tools are available now, use them. Otherwise, when a task needs their email, calendar, docs or other app data, say the apps couldn't be reached just now and to try again in a moment. Never tell them to connect or reconnect apps for this, and never run OAuth, sign-in or an auth CLI yourself.");
      }
      else
      {
        list.Add("- No apps were connected when this chat started. If `composio` tools are available now, use them for their email, calendar, docs and other app data; otherwise tell them to connect the app in HeyClicky Settings -> Integrations. Never run OAuth, sign-in or an auth CLI yourself.");
      }
      string obsidianVaultPath = this.<options>P.ObsidianVaultPath;
      string text3 = ((obsidianVaultPath != null) ? obsidianVaultPath.Trim() : null);
      list.Add((!string.IsNullOrEmpty(text3)) ? ("- Obsidian is their local vault at " + text3 + " (also `$env:OBSIDIAN_VAULT_PATH`): plain Markdown files you read and edit with the shell, keeping frontmatter, [[wikilinks]] and tags intact. Ask before deleting, bulk-moving or renaming notes, or changing its `.obsidian` settings. It is not a Composio app, and it is the only vault root; don't look for notes elsewhere.") : "- Obsidian isn't set up. For Obsidian work, tell them to configure their vault in HeyClicky Settings -> Integrations -> Obsidian.");
      if (customConnectors.Count > 0)
      {
        IEnumerable<string> enumerable = customConnectors.Select<CustomConnectorRegistration, string>(delegate(CustomConnectorRegistration c)
        {
          string text4 = c.CapabilityDescription.Trim();
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(61, 2);
          defaultInterpolatedStringHandler.AppendLiteral("  - ");
          defaultInterpolatedStringHandler.AppendFormatted(c.DisplayName);
          defaultInterpolatedStringHandler.AppendLiteral(": MCP server `");
          defaultInterpolatedStringHandler.AppendFormatted(c.ServerKey);
          defaultInterpolatedStringHandler.AppendLiteral("` (its tools are prefixed with that name). ");
          return defaultInterpolatedStringHandler.ToStringAndClear() + ((text4.Length == 0) ? "the user did not describe it; discover what it does from its tool list" : text4);
        });
        list.Add("- Their own MCP connectors (Settings -> Integrations -> Custom connectors), one server each. Use them directly for tasks in their domain under the same approval rule, and also confirm first before sending messages to other people or changing ad spend:\n" + string.Join("\n", enumerable) + "\n- Their credentials are already configured: never ask for or print a token. If a connector's tools are missing or a call comes back unauthorized, say so and point them to its row in Settings -> Integrations -> Custom connectors (\"Sign in\", or the token under Edit). Don't go looking for it in other tools' configuration or call it over HTTP yourself.");
      }
      return string.Join("\n", list);
    }

    // Token: 0x060003D1 RID: 977 RVA: 0x000E6F14 File Offset: 0x000E6F14
    private Dictionary<string, string> SynchronizeSkills(IReadOnlyList<string> connectedToolkits)
    {
      HashSet<string> hashSet;
      if (!Directory.Exists(this.IntegrationSkillsDirectory))
      {
        hashSet = new HashSet<string>();
      }
      else
      {
        IEnumerable<string> directories = Directory.GetDirectories(this.IntegrationSkillsDirectory);
        Func<string, string> func;
        if ((func = CodexHomeManager.<>O.<0>__GetFileName) == null)
        {
          func = (CodexHomeManager.<>O.<0>__GetFileName = new Func<string, string>(Path.GetFileName));
        }
        hashSet = (from n in directories.Select<string, string>(func)
          where n != null && File.Exists(Path.Combine(this.IntegrationSkillsDirectory, n, "SKILL.md"))
          select (n)).ToHashSet<string>(StringComparer.Ordinal);
      }
      HashSet<string> hashSet2 = hashSet;
      HashSet<string> hashSet3 = connectedToolkits.Where<string>(new Func<string, bool>(hashSet2.Contains)).Except<string>(CodexHomeManager.BundledSkillNames).ToHashSet<string>(StringComparer.Ordinal);
      string text = Path.Combine(this.SkillsDirectory, ".system");
      foreach (string text2 in CodexHomeManager.BlockedSystemSkillNames)
      {
        CodexHomeManager.TryDeleteDirectory(Path.Combine(text, text2));
      }
      foreach (string text3 in CodexHomeManager.HiddenBundledSkillNames)
      {
        CodexHomeManager.TryDeleteDirectory(Path.Combine(this.SkillsDirectory, text3));
      }
      HashSet<string> hashSet4 = CodexHomeManager.BundledSkillNames.Concat<string>(hashSet3).ToHashSet<string>(StringComparer.OrdinalIgnoreCase);
      foreach (string text4 in Directory.GetDirectories(this.SkillsDirectory))
      {
        string fileName = Path.GetFileName(text4);
        if (!fileName.StartsWith('.') && !hashSet4.Contains(fileName))
        {
          CodexHomeManager.TryDeleteDirectory(text4);
        }
      }
      foreach (string text5 in hashSet3)
      {
        CodexHomeManager.SyncSkillFolder(Path.Combine(this.IntegrationSkillsDirectory, text5), Path.Combine(this.SkillsDirectory, text5));
      }
      Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
      foreach (string text6 in CodexHomeManager.BundledSkillNames)
      {
        string text7 = Path.Combine(this.BundledSkillsDirectory, text6);
        if (File.Exists(Path.Combine(text7, "SKILL.md")))
        {
          string text8 = Path.Combine(this.SkillsDirectory, text6);
          CodexHomeManager.SyncSkillFolder(text7, text8);
          dictionary[text6] = text8;
        }
      }
      return dictionary;
    }

    // Token: 0x060003D2 RID: 978 RVA: 0x000E716C File Offset: 0x000E716C
    private static void SyncSkillFolder(string source, string dest)
    {
      string text = Path.Combine(source, "SKILL.md");
      string text2 = Path.Combine(dest, "SKILL.md");
      if (File.Exists(text2) && File.ReadAllBytes(text2).AsSpan<byte>().SequenceEqual<byte>(File.ReadAllBytes(text)))
      {
        return;
      }
      CodexHomeManager.TryDeleteDirectory(dest);
      CodexHomeManager.CopyDirectory(source, dest);
    }

    // Token: 0x060003D3 RID: 979 RVA: 0x000E71C4 File Offset: 0x000E71C4
    private static void CopyDirectory(string source, string dest)
    {
      Directory.CreateDirectory(dest);
      foreach (string text in Directory.GetFiles(source))
      {
        File.Copy(text, Path.Combine(dest, Path.GetFileName(text)), true);
      }
      foreach (string text2 in Directory.GetDirectories(source))
      {
        CodexHomeManager.CopyDirectory(text2, Path.Combine(dest, Path.GetFileName(text2)));
      }
    }

    // Token: 0x060003D4 RID: 980 RVA: 0x000E7230 File Offset: 0x000E7230
    private static void TryDeleteDirectory(string dir)
    {
      try
      {
        if (Directory.Exists(dir))
        {
          Directory.Delete(dir, true);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Codex", "could not remove " + dir + ": " + ex.Message);
      }
    }

    // Token: 0x060003D5 RID: 981 RVA: 0x000E7284 File Offset: 0x000E7284
    private static bool WriteIfChanged(string path, string contents)
    {
      try
      {
        if (File.Exists(path) && File.ReadAllText(path, Encoding.UTF8) == contents)
        {
          return false;
        }
      }
      catch
      {
      }
      string text = path + ".tmp";
      File.WriteAllText(text, contents, new UTF8Encoding(false));
      File.Move(text, path, true);
      return true;
    }

    // Token: 0x060003D6 RID: 982 RVA: 0x000E72E8 File Offset: 0x000E72E8
    // Note: this type is marked as 'beforefieldinit'.
    unsafe static CodexHomeManager()
    {
      string text = "\n";
      <>y__InlineArray5<string> <>y__InlineArray = default(<>y__InlineArray5<string>);
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 0) = "Some apps have more than one connected account (for example a work Gmail and a personal Gmail). With more than one, every tool call for that app must name the account: pass the account id (or the account's address) as the tool's `account` field, e.g. COMPOSIO_MULTI_EXECUTE_TOOL tools[].account. A call that names no account fails with a list of the accounts; pick one and retry.";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 1) = "Reading (search, list, find, summarize): look in EVERY account of that app unless the user named one, and say which account each result came from.";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 2) = "Replying to or acting on something that already exists (an email thread, an event, a file): use the account it lives in. That is always known, so never ask.";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 3) = "Creating something new (a new email, event, doc) or sending: use the account the user named, or the one the context makes obvious (they said work or personal, the recipient is on the work account's domain, this Clicky's job is clearly one of them, or its memory already says which). If it is still unclear, ask ONE short question naming the accounts (\"Send this from your work or your personal Gmail?\") and wait. Never guess.";
      *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray5<string>, string>(ref <>y__InlineArray, 4) = "Once the user tells you which account a job uses, remember it in your notes so you never ask again.";
      CodexHomeManager.MultipleAccountsRules = string.Join(text, <PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray5<string>, string>(in <>y__InlineArray, 5));
    }

    // Token: 0x0400015F RID: 351
    [CompilerGenerated]
    private AgentRuntimeOptions <options>P = options;

    // Token: 0x04000160 RID: 352
    public static readonly string[] BundledSkillNames = new string[] { "clicky-browser", "vercel-deploy" };

    // Token: 0x04000161 RID: 353
    private static readonly string[] HiddenBundledSkillNames = new string[] { "nano-banana-slides" };

    // Token: 0x04000162 RID: 354
    private static readonly string[] BlockedSystemSkillNames = new string[] { "imagegen", "openai-docs", "plugin-creator", "review-agent", "skill-creator", "skill-installer" };

    // Token: 0x04000163 RID: 355
    private readonly object gate = new object();

    // Token: 0x04000165 RID: 357
    private static readonly string MultipleAccountsRules;

    // Token: 0x020000FF RID: 255
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040005A5 RID: 1445
      [Nullable(0)]
      public static Func<string, string> <0>__GetFileName;
    }
  }
}
