using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;
using System.Threading;

namespace HeyClicky.Agents.Clickys
{
  // Token: 0x02000069 RID: 105
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ClickyStore
  {
    // Token: 0x17000215 RID: 533
    // (get) Token: 0x06000846 RID: 2118 RVA: 0x000FFEA0 File Offset: 0x000FFEA0
    // (set) Token: 0x06000847 RID: 2119 RVA: 0x000FFEAC File Offset: 0x000FFEAC
    public string ProjectsRoot
    {
      get
      {
        return this.projectsRoot;
      }
      set
      {
        this.projectsRoot = value;
      }
    }

    // Token: 0x06000848 RID: 2120 RVA: 0x000FFEB8 File Offset: 0x000FFEB8
    public ClickyStore(string stateDirectory, string projectsRoot)
    {
      Directory.CreateDirectory(stateDirectory);
      this.path = Path.Combine(stateDirectory, "clickys.json");
      this.projectsRoot = projectsRoot;
      this.state = ClickyStore.Load(this.path);
    }

    // Token: 0x06000849 RID: 2121 RVA: 0x000FFF08 File Offset: 0x000FFF08
    private static ClickyStore.State Load(string path)
    {
      try
      {
        if (File.Exists(path))
        {
          return JsonSerializer.Deserialize<ClickyStore.State>(File.ReadAllText(path), ClickyStore.Json) ?? new ClickyStore.State();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Agents", "clickys.json unreadable, starting fresh: " + ex.Message);
      }
      return new ClickyStore.State();
    }

    // Token: 0x0600084A RID: 2122 RVA: 0x000FFF70 File Offset: 0x000FFF70
    private void Save()
    {
      string text = this.path + ".tmp";
      File.WriteAllText(text, JsonSerializer.Serialize<ClickyStore.State>(this.state, ClickyStore.Json));
      File.Move(text, this.path, true);
    }

    // Token: 0x17000216 RID: 534
    // (get) Token: 0x0600084B RID: 2123 RVA: 0x000FFFA4 File Offset: 0x000FFFA4
    public IReadOnlyList<ClickyInfo> Clickys
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyInfo> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.state.Clickys.Where<ClickyInfo>((ClickyInfo c) => !c.IsArchived).ToList<ClickyInfo>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x0600084C RID: 2124 RVA: 0x00100014 File Offset: 0x00100014
    [return: Nullable(2)]
    public ClickyInfo Get(string slug)
    {
      object obj = this.gate;
      ClickyInfo clickyInfo;
      lock (obj)
      {
        clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.Slug == slug);
      }
      return clickyInfo;
    }

    // Token: 0x0600084D RID: 2125 RVA: 0x0010007C File Offset: 0x0010007C
    [return: Nullable(2)]
    public ClickyInfo ForThread(string threadId)
    {
      object obj = this.gate;
      bool flag = false;
      ClickyInfo clickyInfo2;
      try
      {
        Monitor.Enter(obj, ref flag);
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId);
        if (clickyInfo != null)
        {
          clickyInfo2 = clickyInfo;
        }
        else
        {
          string slug;
          clickyInfo2 = (this.state.FiledThreadOwners.TryGetValue(threadId, out slug) ? this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.Slug == slug) : null);
        }
      }
      finally
      {
        if (flag)
        {
          Monitor.Exit(obj);
        }
      }
      return clickyInfo2;
    }

    // Token: 0x0600084E RID: 2126 RVA: 0x00100134 File Offset: 0x00100134
    public ClickyInfo Found(string name, string roleText, string oneLiner)
    {
      object obj = this.gate;
      ClickyInfo clickyInfo4;
      lock (obj)
      {
        ClickyStore.<>c__DisplayClass16_1 CS$<>8__locals2 = new ClickyStore.<>c__DisplayClass16_1();
        name = (string.IsNullOrWhiteSpace(name) ? "Clicky Buddy" : name.Trim());
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.IsArchived && string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        if (clickyInfo != null)
        {
          ClickyInfo clickyInfo2 = clickyInfo.<Clone>$();
          clickyInfo2.IsArchived = false;
          ClickyInfo clickyInfo3 = clickyInfo2;
          this.Replace(clickyInfo3);
          this.Save();
          clickyInfo4 = clickyInfo3;
        }
        else
        {
          string text = ClickyStore.Slugify(name);
          CS$<>8__locals2.slug = text;
          int num = 2;
          for (;;)
          {
            IEnumerable<ClickyInfo> clickys = this.state.Clickys;
            Func<ClickyInfo, bool> func;
            if ((func = CS$<>8__locals2.<>9__1) == null)
            {
              func = (CS$<>8__locals2.<>9__1 = (ClickyInfo c) => c.Slug == CS$<>8__locals2.slug);
            }
            if (!clickys.Any<ClickyInfo>(func))
            {
              break;
            }
            ClickyStore.<>c__DisplayClass16_1 CS$<>8__locals3 = CS$<>8__locals2;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
            defaultInterpolatedStringHandler.AppendFormatted(text);
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            CS$<>8__locals3.slug = defaultInterpolatedStringHandler.ToStringAndClear();
            num++;
          }
          ClickyInfo clickyInfo5 = new ClickyInfo(CS$<>8__locals2.slug, name, roleText, oneLiner, null, DateTimeOffset.Now, false);
          this.state.Clickys.Add(clickyInfo5);
          this.Save();
          clickyInfo4 = clickyInfo5;
        }
      }
      return clickyInfo4;
    }

    // Token: 0x0600084F RID: 2127 RVA: 0x001002BC File Offset: 0x001002BC
    public void BindThread(string slug, string threadId)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo x) => x.Slug == slug);
        if (clickyInfo != null && !(clickyInfo.ThreadId == threadId))
        {
          string threadId2 = clickyInfo.ThreadId;
          if (threadId2 != null)
          {
            this.state.FiledThreadOwners[threadId2] = slug;
          }
          for (int i = 0; i < this.state.Clickys.Count; i++)
          {
            if (this.state.Clickys[i].ThreadId == threadId && this.state.Clickys[i].Slug != slug)
            {
              List<ClickyInfo> clickys = this.state.Clickys;
              int num = i;
              ClickyInfo clickyInfo2 = this.state.Clickys[i].<Clone>$();
              clickyInfo2.ThreadId = null;
              clickys[num] = clickyInfo2;
            }
          }
          ClickyInfo clickyInfo3 = clickyInfo.<Clone>$();
          clickyInfo3.ThreadId = threadId;
          this.Replace(clickyInfo3);
          this.Save();
        }
      }
    }

    // Token: 0x06000850 RID: 2128 RVA: 0x00100414 File Offset: 0x00100414
    public void UnbindThread(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo x) => x.Slug == slug);
        if (((clickyInfo != null) ? clickyInfo.ThreadId : null) != null)
        {
          ClickyInfo clickyInfo2 = clickyInfo.<Clone>$();
          clickyInfo2.ThreadId = null;
          this.Replace(clickyInfo2);
          this.Save();
        }
      }
    }

    // Token: 0x06000851 RID: 2129 RVA: 0x001004A4 File Offset: 0x001004A4
    public void Archive(string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo x) => x.Slug == slug);
        if (clickyInfo != null)
        {
          ClickyInfo clickyInfo2 = clickyInfo.<Clone>$();
          clickyInfo2.IsArchived = true;
          clickyInfo2.ThreadId = null;
          this.Replace(clickyInfo2);
          this.Save();
        }
      }
    }

    // Token: 0x06000852 RID: 2130 RVA: 0x00100530 File Offset: 0x00100530
    private void Replace(ClickyInfo updated)
    {
      int num = this.state.Clickys.FindIndex((ClickyInfo c) => c.Slug == updated.Slug);
      if (num >= 0)
      {
        this.state.Clickys[num] = updated;
        return;
      }
      this.state.Clickys.Add(updated);
    }

    // Token: 0x06000853 RID: 2131 RVA: 0x0010059C File Offset: 0x0010059C
    private static string PromptKey(string raw)
    {
      string text = raw.Trim().Replace('\n', ' ');
      if (text.Length <= 160)
      {
        return text;
      }
      return text.Substring(0, 160);
    }

    // Token: 0x06000854 RID: 2132 RVA: 0x001005D4 File Offset: 0x001005D4
    public void RecordDisplayPrompt(string rawPrompt, string display)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.state.DisplayPrompts[ClickyStore.PromptKey(rawPrompt)] = display;
        this.Save();
      }
    }

    // Token: 0x06000855 RID: 2133 RVA: 0x0010062C File Offset: 0x0010062C
    [return: Nullable(2)]
    public string DisplayFor(string text)
    {
      string text2 = ClickyStore.PromptKey(text);
      object obj = this.gate;
      lock (obj)
      {
        string text3;
        if (this.state.DisplayPrompts.TryGetValue(text2, out text3))
        {
          return text3;
        }
        foreach (KeyValuePair<string, string> keyValuePair in this.state.DisplayPrompts)
        {
          if (text2.StartsWith(keyValuePair.Key, StringComparison.Ordinal) || (keyValuePair.Key.StartsWith(text2, StringComparison.Ordinal) && text2.Length >= 40))
          {
            return keyValuePair.Value;
          }
        }
      }
      return null;
    }

    // Token: 0x06000856 RID: 2134 RVA: 0x00100700 File Offset: 0x00100700
    public bool IsThreadHidden(string threadId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.state.HiddenThreadIds.Contains(threadId);
      }
      return flag2;
    }

    // Token: 0x06000857 RID: 2135 RVA: 0x00100750 File Offset: 0x00100750
    public bool HideThread(string threadId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        if (!this.state.HiddenThreadIds.Add(threadId))
        {
          flag2 = false;
        }
        else
        {
          this.Save();
          flag2 = true;
        }
      }
      return flag2;
    }

    // Token: 0x06000858 RID: 2136 RVA: 0x001007AC File Offset: 0x001007AC
    public void ClearHiddenThreads()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.state.HiddenThreadIds.Count != 0)
        {
          this.state.HiddenThreadIds.Clear();
          this.Save();
        }
      }
    }

    // Token: 0x06000859 RID: 2137 RVA: 0x00100810 File Offset: 0x00100810
    [return: Nullable(2)]
    public string TitleForThread(string threadId)
    {
      object obj = this.gate;
      string valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.state.ThreadTitles.GetValueOrDefault(threadId);
      }
      return valueOrDefault;
    }

    // Token: 0x0600085A RID: 2138 RVA: 0x00100860 File Offset: 0x00100860
    public void SetThreadTitle(string threadId, string title)
    {
      object obj = this.gate;
      lock (obj)
      {
        string text;
        if (!this.state.ThreadTitles.TryGetValue(threadId, out text) || !(text == title))
        {
          this.state.ThreadTitles[threadId] = title;
          this.Save();
        }
      }
    }

    // Token: 0x0600085B RID: 2139 RVA: 0x001008D4 File Offset: 0x001008D4
    public void MarkThreadStartedWithInstructions(string threadId, string instructions)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.state.InstructionFingerprintByThread[threadId] = ClickyStore.Fingerprint(instructions);
        if (this.state.InstructionFingerprintByThread.Count > 2000)
        {
          this.state.InstructionFingerprintByThread.Remove(this.state.InstructionFingerprintByThread.Keys.First<string>());
        }
        this.Save();
      }
    }

    // Token: 0x0600085C RID: 2140 RVA: 0x00100968 File Offset: 0x00100968
    [NullableContext(2)]
    public string InstructionsToCarry(string threadId, string currentInstructions)
    {
      if (threadId == null || currentInstructions == null)
      {
        return null;
      }
      object obj = this.gate;
      string text;
      lock (obj)
      {
        string text2;
        text = ((this.state.InstructionFingerprintByThread.TryGetValue(threadId, out text2) && text2 == ClickyStore.Fingerprint(currentInstructions)) ? null : currentInstructions);
      }
      return text;
    }

    // Token: 0x0600085D RID: 2141 RVA: 0x001009D4 File Offset: 0x001009D4
    private static string Fingerprint(string text)
    {
      return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(text)));
    }

    // Token: 0x0600085E RID: 2142 RVA: 0x001009EC File Offset: 0x001009EC
    public string WorkspacePath(string slug)
    {
      return Path.Combine(this.projectsRoot, "agents", slug);
    }

    // Token: 0x0600085F RID: 2143 RVA: 0x00100A04 File Offset: 0x00100A04
    public string EnsureWorkspace(ClickyInfo clicky)
    {
      string text = this.WorkspacePath(clicky.Slug);
      Directory.CreateDirectory(Path.Combine(text, "output"));
      Directory.CreateDirectory(Path.Combine(text, "tmp"));
      string text2 = Path.Combine(text, "AGENTS.md");
      if (!File.Exists(text2))
      {
        File.WriteAllText(text2, ClickyStore.SeededAgentsFile(clicky), new UTF8Encoding(false));
      }
      return text;
    }

    // Token: 0x06000860 RID: 2144 RVA: 0x00100A68 File Offset: 0x00100A68
    private static string SeededAgentsFile(ClickyInfo c)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(798, 7);
      defaultInterpolatedStringHandler.AppendLiteral("# ");
      defaultInterpolatedStringHandler.AppendFormatted(c.Name);
      defaultInterpolatedStringHandler.AppendLiteral("\n\nYou are ");
      defaultInterpolatedStringHandler.AppendFormatted(c.Name);
      defaultInterpolatedStringHandler.AppendLiteral(", the user's persistent ");
      defaultInterpolatedStringHandler.AppendFormatted(c.RoleText.ToLowerInvariant());
      defaultInterpolatedStringHandler.AppendLiteral(" agent in HeyClicky.\nWhat you do: ");
      defaultInterpolatedStringHandler.AppendFormatted(c.OneLiner);
      defaultInterpolatedStringHandler.AppendLiteral(".\n\nThis folder is YOUR workspace: your notes live in this file and the\nfiles you produce belong here. You are not a one-off task runner —\nthe user returns to you, and you remember.\n\n## Identity\n- Name: ");
      defaultInterpolatedStringHandler.AppendFormatted(c.Name);
      defaultInterpolatedStringHandler.AppendLiteral("\n- Role: ");
      defaultInterpolatedStringHandler.AppendFormatted(c.RoleText);
      defaultInterpolatedStringHandler.AppendLiteral("\n- Speak as ");
      defaultInterpolatedStringHandler.AppendFormatted(c.Name);
      defaultInterpolatedStringHandler.AppendLiteral(" — consistent voice, first person.\n\n## Standing preferences\n(Durable instructions the user has given you. Keep each to one line.)\n\n## Notes\n(Your working memory. After any turn where you learn something durable\nabout this job — accounts, structures, the user's taste, decisions\nmade, ongoing threads of work — update this section: add the fact,\ndated, and prune anything stale or superseded. Keep it curated and\nshort; this file is read before every turn. Never store secrets,\npasswords, or tokens here.)\n");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000861 RID: 2145 RVA: 0x00100B4C File Offset: 0x00100B4C
    public static string Slugify(string name)
    {
      string text = ClickyStore.NonSlug().Replace(name.Trim().ToLowerInvariant(), "-").Trim('-');
      if (text.Length == 0)
      {
        return "clicky";
      }
      if (text.Length <= 40)
      {
        return text;
      }
      return text.Substring(0, 40).Trim('-');
    }

    // Token: 0x06000862 RID: 2146 RVA: 0x00100BA8 File Offset: 0x00100BA8
    [GeneratedRegex("[^a-z0-9]+")]
    [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
    private static Regex NonSlug()
    {
      return <RegexGenerator_g>F438A85ABF99E47EA4FBA640CE8836B84C8719F8A5025C90CE820D2BC67EAC31D__NonSlug_0.Instance;
    }

    // Token: 0x17000217 RID: 535
    // (get) Token: 0x06000863 RID: 2147 RVA: 0x00100BB0 File Offset: 0x00100BB0
    public IReadOnlyList<ClickyInfo> ArchivedClickys
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<ClickyInfo> readOnlyList;
        lock (obj)
        {
          readOnlyList = (from c in this.state.Clickys
            where c.IsArchived
            orderby c.CreatedAt descending
            select c).ToList<ClickyInfo>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06000864 RID: 2148 RVA: 0x00100C44 File Offset: 0x00100C44
    public static string NormalizedName(string name)
    {
      string text = string.Join(' ', (name ?? "").Split(null, StringSplitOptions.RemoveEmptyEntries));
      if (text.Length <= 40)
      {
        return text;
      }
      return text.Substring(0, 40).TrimEnd();
    }

    // Token: 0x06000865 RID: 2149 RVA: 0x00100C84 File Offset: 0x00100C84
    [return: Nullable(2)]
    public ClickyInfo Rename(string slug, string name)
    {
      string text = ClickyStore.NormalizedName(name);
      if (text.Length == 0)
      {
        return null;
      }
      object obj = this.gate;
      ClickyInfo clickyInfo2;
      lock (obj)
      {
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo x) => x.Slug == slug);
        if (clickyInfo == null)
        {
          clickyInfo2 = null;
        }
        else if (clickyInfo.Name == text)
        {
          clickyInfo2 = clickyInfo;
        }
        else
        {
          ClickyInfo clickyInfo3 = clickyInfo.<Clone>$();
          clickyInfo3.Name = text;
          ClickyInfo clickyInfo4 = clickyInfo3;
          this.Replace(clickyInfo4);
          this.Save();
          clickyInfo2 = clickyInfo4;
        }
      }
      return clickyInfo2;
    }

    // Token: 0x06000866 RID: 2150 RVA: 0x00100D3C File Offset: 0x00100D3C
    [return: Nullable(2)]
    public ClickyInfo Unarchive(string slug)
    {
      object obj = this.gate;
      ClickyInfo clickyInfo2;
      lock (obj)
      {
        ClickyInfo clickyInfo = this.state.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo x) => x.Slug == slug);
        if (clickyInfo == null || !clickyInfo.IsArchived)
        {
          clickyInfo2 = clickyInfo;
        }
        else
        {
          ClickyInfo clickyInfo3 = clickyInfo.<Clone>$();
          clickyInfo3.IsArchived = false;
          ClickyInfo clickyInfo4 = clickyInfo3;
          this.Replace(clickyInfo4);
          this.Save();
          clickyInfo2 = clickyInfo4;
        }
      }
      return clickyInfo2;
    }

    // Token: 0x06000867 RID: 2151 RVA: 0x00100DD4 File Offset: 0x00100DD4
    public bool OwnsThread(string threadId)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.state.Clickys.Any<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId) || this.state.FiledThreadOwners.ContainsKey(threadId);
      }
      return flag2;
    }

    // Token: 0x06000868 RID: 2152 RVA: 0x00100E54 File Offset: 0x00100E54
    public void FileThread(string threadId, string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        string text;
        if (!this.state.FiledThreadOwners.TryGetValue(threadId, out text) || !(text == slug))
        {
          this.state.FiledThreadOwners[threadId] = slug;
          this.Save();
        }
      }
    }

    // Token: 0x06000869 RID: 2153 RVA: 0x00100EC8 File Offset: 0x00100EC8
    public void AppendNotes(ClickyInfo clicky, string markdown)
    {
      if (string.IsNullOrWhiteSpace(markdown))
      {
        return;
      }
      string text = Path.Combine(this.EnsureWorkspace(clicky), "AGENTS.md");
      object obj = this.gate;
      lock (obj)
      {
        string text2 = (File.Exists(text) ? File.ReadAllText(text) : "");
        if (text2.Length > 0 && !text2.EndsWith('\n'))
        {
          text2 += "\n";
        }
        text2 = text2 + "\n" + markdown.Trim() + "\n";
        File.WriteAllText(text, text2, new UTF8Encoding(false));
      }
    }

    // Token: 0x0600086A RID: 2154 RVA: 0x00100F78 File Offset: 0x00100F78
    public string NotesPath(string slug)
    {
      return Path.Combine(this.WorkspacePath(slug), "AGENTS.md");
    }

    // Token: 0x0600086B RID: 2155 RVA: 0x00100F8C File Offset: 0x00100F8C
    public IReadOnlyList<string> DeleteAll()
    {
      object obj = this.gate;
      IReadOnlyList<string> readOnlyList2;
      lock (obj)
      {
        IEnumerable<string> enumerable = this.state.Clickys.Select<ClickyInfo, string>((ClickyInfo c) => this.WorkspacePath(c.Slug)).Distinct<string>(StringComparer.OrdinalIgnoreCase);
        Func<string, bool> func;
        if ((func = ClickyStore.<>O.<0>__Exists) == null)
        {
          func = (ClickyStore.<>O.<0>__Exists = new Func<string, bool>(Directory.Exists));
        }
        IReadOnlyList<string> readOnlyList = enumerable.Where<string>(func).ToList<string>();
        this.state.Clickys.Clear();
        this.state.FiledThreadOwners.Clear();
        this.Save();
        readOnlyList2 = readOnlyList;
      }
      return readOnlyList2;
    }

    // Token: 0x04000305 RID: 773
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    // Token: 0x04000306 RID: 774
    private readonly object gate = new object();

    // Token: 0x04000307 RID: 775
    private readonly string path;

    // Token: 0x04000308 RID: 776
    private volatile string projectsRoot;

    // Token: 0x04000309 RID: 777
    private ClickyStore.State state;

    // Token: 0x020001BC RID: 444
    [Nullable(0)]
    private sealed class State
    {
      // Token: 0x17000303 RID: 771
      // (get) Token: 0x06000F4B RID: 3915 RVA: 0x0011A5CC File Offset: 0x0011A5CC
      // (set) Token: 0x06000F4C RID: 3916 RVA: 0x0011A5D4 File Offset: 0x0011A5D4
      public List<ClickyInfo> Clickys { get; set; } = new List<ClickyInfo>();

      // Token: 0x17000304 RID: 772
      // (get) Token: 0x06000F4D RID: 3917 RVA: 0x0011A5E0 File Offset: 0x0011A5E0
      // (set) Token: 0x06000F4E RID: 3918 RVA: 0x0011A5E8 File Offset: 0x0011A5E8
      public Dictionary<string, string> ThreadTitles { get; set; } = new Dictionary<string, string>();

      // Token: 0x17000305 RID: 773
      // (get) Token: 0x06000F4F RID: 3919 RVA: 0x0011A5F4 File Offset: 0x0011A5F4
      // (set) Token: 0x06000F50 RID: 3920 RVA: 0x0011A5FC File Offset: 0x0011A5FC
      public Dictionary<string, string> InstructionFingerprintByThread { get; set; } = new Dictionary<string, string>();

      // Token: 0x17000306 RID: 774
      // (get) Token: 0x06000F51 RID: 3921 RVA: 0x0011A608 File Offset: 0x0011A608
      // (set) Token: 0x06000F52 RID: 3922 RVA: 0x0011A610 File Offset: 0x0011A610
      public Dictionary<string, string> FiledThreadOwners { get; set; } = new Dictionary<string, string>();

      // Token: 0x17000307 RID: 775
      // (get) Token: 0x06000F53 RID: 3923 RVA: 0x0011A61C File Offset: 0x0011A61C
      // (set) Token: 0x06000F54 RID: 3924 RVA: 0x0011A624 File Offset: 0x0011A624
      public Dictionary<string, string> DisplayPrompts { get; set; } = new Dictionary<string, string>();

      // Token: 0x17000308 RID: 776
      // (get) Token: 0x06000F55 RID: 3925 RVA: 0x0011A630 File Offset: 0x0011A630
      // (set) Token: 0x06000F56 RID: 3926 RVA: 0x0011A638 File Offset: 0x0011A638
      public HashSet<string> HiddenThreadIds { get; set; } = new HashSet<string>();
    }

    // Token: 0x020001BD RID: 445
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x04000849 RID: 2121
      [Nullable(0)]
      public static Func<string, bool> <0>__Exists;
    }
  }
}
