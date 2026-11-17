using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.App.Home;
using HeyClicky.Core.Telemetry;
using Windows.UI;

namespace HeyClicky.App.Portraits
{
  // Token: 0x020000F9 RID: 249
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyRoster
  {
    // Token: 0x17000256 RID: 598
    // (get) Token: 0x06000D0F RID: 3343 RVA: 0x005645D8 File Offset: 0x005645D8
    // (set) Token: 0x06000D10 RID: 3344 RVA: 0x005645E0 File Offset: 0x005645E0
    public static ClickyRoster Shared { get; private set; } = new ClickyRoster(ClickyRoster.DefaultPath());

    // Token: 0x17000257 RID: 599
    // (get) Token: 0x06000D11 RID: 3345 RVA: 0x005645E8 File Offset: 0x005645E8
    // (set) Token: 0x06000D12 RID: 3346 RVA: 0x005645F0 File Offset: 0x005645F0
    [Nullable(new byte[] { 1, 2 })]
    public Func<string> AssignedPackId
    {
      [return: Nullable(new byte[] { 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 2 })]
      set;
    } = () => null;

    // Token: 0x14000030 RID: 48
    // (add) Token: 0x06000D13 RID: 3347 RVA: 0x005645FC File Offset: 0x005645FC
    // (remove) Token: 0x06000D14 RID: 3348 RVA: 0x00564634 File Offset: 0x00564634
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x06000D15 RID: 3349 RVA: 0x0056466C File Offset: 0x0056466C
    public static void UseDirectory(string directory)
    {
      ClickyRoster.Shared = new ClickyRoster(Path.Combine(directory, "clickys.json"));
    }

    // Token: 0x06000D16 RID: 3350 RVA: 0x00564684 File Offset: 0x00564684
    private static string DefaultPath()
    {
      return AppPaths.Combine(new string[] { "clickys.json" });
    }

    // Token: 0x06000D17 RID: 3351 RVA: 0x0056469C File Offset: 0x0056469C
    private ClickyRoster(string path)
    {
      this.path = path;
      try
      {
        this.stored = (File.Exists(path) ? (JsonSerializer.Deserialize<ClickyRoster.Stored>(File.ReadAllText(path), ClickyRoster.Json) ?? new ClickyRoster.Stored()) : new ClickyRoster.Stored());
      }
      catch (Exception ex)
      {
        Log.Warn("roster", "unreadable " + path + ": " + ex.Message);
        this.stored = new ClickyRoster.Stored();
      }
      if (this.MoveUncustomizedClickysToHomeClouds())
      {
        this.Save();
      }
    }

    // Token: 0x06000D18 RID: 3352 RVA: 0x00564764 File Offset: 0x00564764
    private bool MoveUncustomizedClickysToHomeClouds()
    {
      object obj = this.gate;
      bool flag3;
      lock (obj)
      {
        HashSet<string> hashSet = new HashSet<string>(this.stored.UserCustomizedSlugs ?? new List<string>());
        bool flag2 = false;
        foreach (string text in this.stored.Appearances.Keys.Order<string>(StringComparer.Ordinal).ToList<string>())
        {
          if (!hashSet.Contains(text) && !this.stored.Appearances[text].IsHomeCloud)
          {
            this.stored.Appearances[text] = PortraitAppearance.RandomHomeCloud(this.stored.Appearances.Values, null);
            flag2 = true;
          }
        }
        flag3 = flag2;
      }
      return flag3;
    }

    // Token: 0x06000D19 RID: 3353 RVA: 0x00564864 File Offset: 0x00564864
    public void OutfitStarterCast(IEnumerable<string> castSlugs)
    {
      object obj = this.gate;
      lock (obj)
      {
        HashSet<string> hashSet = new HashSet<string>(this.stored.UserCustomizedSlugs ?? new List<string>());
        foreach (string text in castSlugs)
        {
          PortraitAppearance portraitAppearance;
          if (!hashSet.Contains(text) && (!this.stored.Appearances.TryGetValue(text, out portraitAppearance) || !portraitAppearance.IsHomeCloud))
          {
            this.stored.Appearances[text] = PortraitAppearance.RandomHomeCloud(this.stored.Appearances.Values, null);
          }
        }
      }
      this.Save();
    }

    // Token: 0x06000D1A RID: 3354 RVA: 0x00564940 File Offset: 0x00564940
    public IReadOnlyList<string> CloudColorsInUse()
    {
      return this.Clickys.Select<RosterClicky, string>((RosterClicky c) => this.AppearanceFor("clicky:" + c.Slug).HomeCloudColorName).OfType<string>().ToList<string>();
    }

    // Token: 0x06000D1B RID: 3355 RVA: 0x00564964 File Offset: 0x00564964
    private void Save()
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(this.path));
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = JsonSerializer.Serialize<ClickyRoster.Stored>(this.stored, ClickyRoster.Json);
        }
        File.WriteAllText(this.path, text);
      }
      catch (Exception ex)
      {
        Log.Warn("roster", "save: " + ex.Message);
      }
      EventHandler changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed(this, EventArgs.Empty);
    }

    // Token: 0x17000258 RID: 600
    // (get) Token: 0x06000D1C RID: 3356 RVA: 0x00564A10 File Offset: 0x00564A10
    public PortraitPack AssignedPack
    {
      get
      {
        return PortraitData.PackFromId(this.AssignedPackId()).GetValueOrDefault(PortraitPack.ClassicClouds);
      }
    }

    // Token: 0x17000259 RID: 601
    // (get) Token: 0x06000D1D RID: 3357 RVA: 0x00564A38 File Offset: 0x00564A38
    // (set) Token: 0x06000D1E RID: 3358 RVA: 0x00564A40 File Offset: 0x00564A40
    [Nullable(2)]
    public IHomeSource Source
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06000D1F RID: 3359 RVA: 0x00564A4C File Offset: 0x00564A4C
    public void Attach(IHomeSource source)
    {
      if (this.Source == source)
      {
        return;
      }
      this.Source = source;
      source.TasksChanged += delegate([Nullable(2)] object _, EventArgs _)
      {
        EventHandler changed = this.Changed;
        if (changed == null)
        {
          return;
        }
        changed(this, EventArgs.Empty);
      };
      object obj = this.gate;
      List<RosterClicky> list;
      lock (obj)
      {
        list = this.stored.Clickys.Where<RosterClicky>((RosterClicky c) => !source.KnowsClicky(c.Slug)).ToList<RosterClicky>();
      }
      if (list.Count == 0)
      {
        return;
      }
      foreach (RosterClicky rosterClicky in list)
      {
        try
        {
          ClickyInfo clickyInfo = source.CreateClicky(rosterClicky.Name, rosterClicky.RoleText, rosterClicky.OneLiner);
          if (clickyInfo.Slug != rosterClicky.Slug)
          {
            this.MoveSlug(rosterClicky.Slug, clickyInfo.Slug);
          }
          Log.Info("roster", "moved \"" + rosterClicky.Name + "\" into the agents' roster as " + clickyInfo.Slug);
        }
        catch (Exception ex)
        {
          Log.Warn("roster", "couldn't move " + rosterClicky.Slug + " to the agents: " + ex.Message);
        }
      }
      this.Save();
    }

    // Token: 0x06000D20 RID: 3360 RVA: 0x00564BE8 File Offset: 0x00564BE8
    private void MoveSlug(string from, string to)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.stored.Clickys.FindIndex((RosterClicky c) => c.Slug == from);
        if (num >= 0)
        {
          List<RosterClicky> clickys = this.stored.Clickys;
          int num2 = num;
          RosterClicky rosterClicky = this.stored.Clickys[num].<Clone>$();
          rosterClicky.Slug = to;
          clickys[num2] = rosterClicky;
        }
        PortraitAppearance portraitAppearance;
        if (this.stored.Appearances.Remove(from, out portraitAppearance))
        {
          this.stored.Appearances[to] = portraitAppearance;
        }
        IEnumerable<KeyValuePair<string, string>> taskOwners = this.stored.TaskOwners;
        Func<KeyValuePair<string, string>, bool> <>9__2;
        Func<KeyValuePair<string, string>, bool> func;
        if ((func = <>9__2) == null)
        {
          func = (<>9__2 = (KeyValuePair<string, string> kv) => kv.Value == from);
        }
        foreach (string text in (from kv in taskOwners.Where<KeyValuePair<string, string>>(func)
          select kv.Key).ToList<string>())
        {
          this.stored.TaskOwners[text] = to;
        }
        this.stored.StarterCards = this.stored.StarterCards.Select<StarterTaskCard, StarterTaskCard>(delegate(StarterTaskCard card)
        {
          if (!(card.ClickySlug == from))
          {
            return card;
          }
          StarterTaskCard starterTaskCard = card.<Clone>$();
          starterTaskCard.ClickySlug = to;
          return starterTaskCard;
        }).ToList<StarterTaskCard>();
      }
    }

    // Token: 0x1700025A RID: 602
    // (get) Token: 0x06000D21 RID: 3361 RVA: 0x00564DAC File Offset: 0x00564DAC
    public IReadOnlyList<RosterClicky> Clickys
    {
      get
      {
        IHomeSource source = this.Source;
        object obj;
        if (source == null)
        {
          obj = this.gate;
          lock (obj)
          {
            return this.stored.Clickys.ToList<RosterClicky>();
          }
        }
        IReadOnlyList<ClickyInfo> clickys = source.Clickys;
        obj = this.gate;
        IReadOnlyList<RosterClicky> readOnlyList;
        lock (obj)
        {
          readOnlyList = clickys.OrderBy<ClickyInfo, DateTimeOffset>((ClickyInfo c) => c.CreatedAt).Select<ClickyInfo, RosterClicky>(delegate(ClickyInfo c)
          {
            RosterClicky rosterClicky = this.stored.Clickys.FirstOrDefault<RosterClicky>((RosterClicky x) => x.Slug == c.Slug);
            if (rosterClicky == null)
            {
              return new RosterClicky(c.Slug, c.Name, c.RoleText, c.OneLiner, Array.Empty<string>(), Array.Empty<string>(), null, c.CreatedAt);
            }
            RosterClicky rosterClicky2 = rosterClicky.<Clone>$();
            rosterClicky2.Name = c.Name;
            rosterClicky2.RoleText = c.RoleText;
            rosterClicky2.OneLiner = c.OneLiner;
            return rosterClicky2;
          }).ToList<RosterClicky>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x1700025B RID: 603
    // (get) Token: 0x06000D22 RID: 3362 RVA: 0x00564E74 File Offset: 0x00564E74
    public IReadOnlyList<StarterTaskCard> StarterCards
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<StarterTaskCard> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.stored.StarterCards.ToList<StarterTaskCard>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x1700025C RID: 604
    // (get) Token: 0x06000D23 RID: 3363 RVA: 0x00564EC0 File Offset: 0x00564EC0
    [Nullable(2)]
    public string GoalLine
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string goalLine;
        lock (obj)
        {
          goalLine = this.stored.GoalLine;
        }
        return goalLine;
      }
    }

    // Token: 0x1700025D RID: 605
    // (get) Token: 0x06000D24 RID: 3364 RVA: 0x00564F08 File Offset: 0x00564F08
    public bool UserKnowsAgents
    {
      get
      {
        object obj = this.gate;
        bool valueOrDefault;
        lock (obj)
        {
          valueOrDefault = this.stored.UserKnowsAgents.GetValueOrDefault();
        }
        return valueOrDefault;
      }
    }

    // Token: 0x06000D25 RID: 3365 RVA: 0x00564F58 File Offset: 0x00564F58
    [return: Nullable(2)]
    public RosterClicky Clicky(string slug)
    {
      return this.Clickys.FirstOrDefault<RosterClicky>((RosterClicky c) => c.Slug == slug);
    }

    // Token: 0x06000D26 RID: 3366 RVA: 0x00564F8C File Offset: 0x00564F8C
    public void ForgetAll()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.stored.Clickys.Clear();
        this.stored.Appearances.Clear();
        this.stored.TaskOwners.Clear();
        this.stored.StarterCards.Clear();
        this.stored.GoalLine = null;
        this.stored.UserKnowsAgents = null;
      }
      this.Save();
    }

    // Token: 0x06000D27 RID: 3367 RVA: 0x0056502C File Offset: 0x0056502C
    public PortraitAppearance AppearanceFor(string seed)
    {
      string text = this.ResolveSeed(seed);
      object obj = this.gate;
      PortraitAppearance portraitAppearance;
      lock (obj)
      {
        PortraitAppearance portraitAppearance2;
        portraitAppearance = (this.stored.Appearances.TryGetValue(text, out portraitAppearance2) ? portraitAppearance2 : PortraitAppearance.SeededHomeCloud(text));
      }
      return portraitAppearance;
    }

    // Token: 0x06000D28 RID: 3368 RVA: 0x00565090 File Offset: 0x00565090
    public string ResolveSeed(string seed)
    {
      if (seed.StartsWith("clicky:", StringComparison.Ordinal))
      {
        string text = seed;
        return text.Substring(7, text.Length - 7);
      }
      object obj = this.gate;
      lock (obj)
      {
        string text2;
        if (this.stored.TaskOwners.TryGetValue(seed, out text2))
        {
          return text2;
        }
      }
      IHomeSource source = this.Source;
      string text3;
      if (source == null)
      {
        text3 = null;
      }
      else
      {
        AgentTaskDetail taskDetail = source.GetTaskDetail(seed);
        text3 = ((taskDetail != null) ? taskDetail.ClickySlug : null);
      }
      return text3 ?? seed;
    }

    // Token: 0x06000D29 RID: 3369 RVA: 0x0056512C File Offset: 0x0056512C
    public void SetSuggestedAsks(string slug, IReadOnlyList<string> asks)
    {
      List<string> list = (from a in asks
        select a.Trim() into a
        where a.Length > 0
        select a).DistinctBy<string, string>((string a) => a.ToLowerInvariant()).Take<string>(3).ToList<string>();
      object obj = this.gate;
      lock (obj)
      {
        int num = this.stored.Clickys.FindIndex((RosterClicky c) => c.Slug == slug);
        if (num >= 0)
        {
          List<RosterClicky> clickys = this.stored.Clickys;
          int num2 = num;
          RosterClicky rosterClicky = this.stored.Clickys[num].<Clone>$();
          rosterClicky.SuggestedAsks = list;
          clickys[num2] = rosterClicky;
        }
        else
        {
          RosterClicky rosterClicky2 = this.Clicky(slug);
          if (rosterClicky2 != null)
          {
            List<RosterClicky> clickys2 = this.stored.Clickys;
            RosterClicky rosterClicky3 = rosterClicky2.<Clone>$();
            rosterClicky3.SuggestedAsks = list;
            clickys2.Add(rosterClicky3);
          }
        }
      }
      this.Save();
    }

    // Token: 0x06000D2A RID: 3370 RVA: 0x00565278 File Offset: 0x00565278
    public Color HairColorFor(string seed)
    {
      return this.AppearanceFor(seed).Hair;
    }

    // Token: 0x06000D2B RID: 3371 RVA: 0x00565288 File Offset: 0x00565288
    public void SetAppearance(string slug, PortraitAppearance appearance)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.stored.Appearances[slug] = appearance;
        ClickyRoster.Stored stored = this.stored;
        if (stored.UserCustomizedSlugs == null)
        {
          stored.UserCustomizedSlugs = new List<string>();
        }
        if (!this.stored.UserCustomizedSlugs.Contains(slug))
        {
          this.stored.UserCustomizedSlugs.Add(slug);
        }
      }
      this.Save();
    }

    // Token: 0x06000D2C RID: 3372 RVA: 0x0056531C File Offset: 0x0056531C
    public void Rename(string slug, string name)
    {
      object obj = this.gate;
      lock (obj)
      {
        int num = this.stored.Clickys.FindIndex((RosterClicky c) => c.Slug == slug);
        if (num < 0 || this.stored.Clickys[num].Name == name)
        {
          return;
        }
        List<RosterClicky> clickys = this.stored.Clickys;
        int num2 = num;
        RosterClicky rosterClicky = this.stored.Clickys[num].<Clone>$();
        rosterClicky.Name = name;
        clickys[num2] = rosterClicky;
      }
      this.Save();
    }

    // Token: 0x06000D2D RID: 3373 RVA: 0x005653D8 File Offset: 0x005653D8
    public void BindTask(string taskId, string slug)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.stored.TaskOwners[taskId] = slug;
      }
      this.Save();
    }

    // Token: 0x06000D2E RID: 3374 RVA: 0x0056542C File Offset: 0x0056542C
    public PortraitAppearance MakeNewAppearance([Nullable(new byte[] { 2, 1 })] IEnumerable<PortraitAppearance> avoiding = null)
    {
      List<string> slugs = this.Clickys.Select<RosterClicky, string>((RosterClicky c) => c.Slug).ToList<string>();
      object obj = this.gate;
      List<PortraitAppearance> list;
      lock (obj)
      {
        slugs.AddRange(this.stored.Appearances.Keys.Where<string>((string k) => !slugs.Contains(k)));
        list = slugs.Select<string, PortraitAppearance>(delegate(string s)
        {
          PortraitAppearance portraitAppearance;
          if (!this.stored.Appearances.TryGetValue(s, out portraitAppearance))
          {
            return PortraitAppearance.SeededHomeCloud(s);
          }
          return portraitAppearance;
        }).ToList<PortraitAppearance>();
      }
      if (avoiding != null)
      {
        list.AddRange(avoiding);
      }
      return PortraitAppearance.RandomHomeCloud(list, null);
    }

    // Token: 0x06000D2F RID: 3375 RVA: 0x00565504 File Offset: 0x00565504
    public RosterClicky CreateClicky(string name, string roleText, string oneLiner, IReadOnlyList<string> introMessages, IReadOnlyList<string> suggestedAsks, [Nullable(2)] string revealLine, [Nullable(2)] PortraitAppearance initialAppearance = null, [Nullable(2)] string source = "agent")
    {
      PortraitAppearance portraitAppearance = initialAppearance ?? this.MakeNewAppearance(null);
      IHomeSource source2 = this.Source;
      ClickyInfo clickyInfo = ((source2 != null) ? source2.CreateClicky(name, roleText, oneLiner) : null);
      object obj = this.gate;
      RosterClicky rosterClicky;
      lock (obj)
      {
        ClickyRoster.<>c__DisplayClass49_0 CS$<>8__locals1 = new ClickyRoster.<>c__DisplayClass49_0();
        CS$<>8__locals1.slug = ((clickyInfo != null) ? clickyInfo.Slug : null);
        if (CS$<>8__locals1.slug == null)
        {
          string text = ClickyRoster.Slugify(name);
          CS$<>8__locals1.slug = text;
          int num = 2;
          for (;;)
          {
            IEnumerable<RosterClicky> clickys = this.stored.Clickys;
            Func<RosterClicky, bool> func;
            if ((func = CS$<>8__locals1.<>9__1) == null)
            {
              func = (CS$<>8__locals1.<>9__1 = (RosterClicky c) => c.Slug == CS$<>8__locals1.slug);
            }
            if (!clickys.Any<RosterClicky>(func))
            {
              break;
            }
            ClickyRoster.<>c__DisplayClass49_0 CS$<>8__locals2 = CS$<>8__locals1;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
            defaultInterpolatedStringHandler.AppendFormatted(text);
            defaultInterpolatedStringHandler.AppendLiteral("-");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            CS$<>8__locals2.slug = defaultInterpolatedStringHandler.ToStringAndClear();
            num++;
          }
        }
        rosterClicky = new RosterClicky(CS$<>8__locals1.slug, ((clickyInfo != null) ? clickyInfo.Name : null) ?? name, roleText, oneLiner, introMessages, suggestedAsks, revealLine, (clickyInfo != null) ? clickyInfo.CreatedAt : DateTimeOffset.Now);
        this.stored.Clickys.RemoveAll((RosterClicky c) => c.Slug == CS$<>8__locals1.slug);
        this.stored.Clickys.Add(rosterClicky);
        if (!this.stored.Appearances.ContainsKey(CS$<>8__locals1.slug) || clickyInfo == null || clickyInfo.CreatedAt > DateTimeOffset.Now.AddMinutes(-1.0))
        {
          this.stored.Appearances[CS$<>8__locals1.slug] = portraitAppearance;
        }
      }
      this.Save();
      if (source != null)
      {
        this.TrackCreated(rosterClicky.Slug, source, suggestedAsks.Count);
      }
      return rosterClicky;
    }

    // Token: 0x1700025E RID: 606
    // (get) Token: 0x06000D30 RID: 3376 RVA: 0x005656FC File Offset: 0x005656FC
    // (set) Token: 0x06000D31 RID: 3377 RVA: 0x00565704 File Offset: 0x00565704
    public static Func<bool> IsOnboardingEngaged { get; set; } = () => false;

    // Token: 0x06000D32 RID: 3378 RVA: 0x0056570C File Offset: 0x0056570C
    public void TrackCreated(string slug, string source, int firstAskCount = 0)
    {
      try
      {
        string text = "clicky_created";
        string text2 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["source"] = source;
        jsonObject["is_onboarding"] = ClickyRoster.IsOnboardingEngaged();
        jsonObject["pack"] = this.AppearanceFor("clicky:" + slug).Pack.Id();
        jsonObject["roster_count"] = this.Clickys.Count;
        jsonObject["first_ask_count"] = firstAskCount;
        ClickyAnalytics.TrackProductEvent(text, slug, text2, jsonObject);
      }
      catch (Exception ex)
      {
        Log.Warn("roster", "analytics clicky_created: " + ex.Message);
      }
    }

    // Token: 0x06000D33 RID: 3379 RVA: 0x005657E4 File Offset: 0x005657E4
    public void SetStarterPlan(IReadOnlyList<StarterTaskCard> cards, [Nullable(2)] string goalLine, bool userKnowsAgents)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.stored.StarterCards = cards.ToList<StarterTaskCard>();
        this.stored.GoalLine = goalLine;
        this.stored.UserKnowsAgents = new bool?(userKnowsAgents);
      }
      this.Save();
    }

    // Token: 0x06000D34 RID: 3380 RVA: 0x00565854 File Offset: 0x00565854
    public void RemoveStarterCard(StarterTaskCard card)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.stored.StarterCards.Remove(card);
      }
      this.Save();
    }

    // Token: 0x06000D35 RID: 3381 RVA: 0x005658A8 File Offset: 0x005658A8
    private static string Slugify(string name)
    {
      char[] array = name.Trim().ToLowerInvariant().Select<char, char>(delegate(char ch)
      {
        if (!char.IsLetterOrDigit(ch))
        {
          return '-';
        }
        return ch;
      })
        .ToArray<char>();
      string text = string.Join('-', new string(array).Split('-', StringSplitOptions.RemoveEmptyEntries));
      if (text.Length != 0)
      {
        return text;
      }
      return "clicky";
    }

    // Token: 0x040004BD RID: 1213
    private static readonly JsonSerializerOptions Json = new JsonSerializerOptions
    {
      WriteIndented = true,
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Token: 0x040004BE RID: 1214
    private readonly string path;

    // Token: 0x040004BF RID: 1215
    private readonly object gate = new object();

    // Token: 0x040004C0 RID: 1216
    private ClickyRoster.Stored stored;

    // Token: 0x020005C1 RID: 1473
    [Nullable(0)]
    private sealed class Stored
    {
      // Token: 0x1700074D RID: 1869
      // (get) Token: 0x06002F5E RID: 12126 RVA: 0x00663440 File Offset: 0x00663440
      // (set) Token: 0x06002F5F RID: 12127 RVA: 0x00663448 File Offset: 0x00663448
      public List<RosterClicky> Clickys { get; set; } = new List<RosterClicky>();

      // Token: 0x1700074E RID: 1870
      // (get) Token: 0x06002F60 RID: 12128 RVA: 0x00663454 File Offset: 0x00663454
      // (set) Token: 0x06002F61 RID: 12129 RVA: 0x0066345C File Offset: 0x0066345C
      public Dictionary<string, PortraitAppearance> Appearances { get; set; } = new Dictionary<string, PortraitAppearance>();

      // Token: 0x1700074F RID: 1871
      // (get) Token: 0x06002F62 RID: 12130 RVA: 0x00663468 File Offset: 0x00663468
      // (set) Token: 0x06002F63 RID: 12131 RVA: 0x00663470 File Offset: 0x00663470
      public Dictionary<string, string> TaskOwners { get; set; } = new Dictionary<string, string>();

      // Token: 0x17000750 RID: 1872
      // (get) Token: 0x06002F64 RID: 12132 RVA: 0x0066347C File Offset: 0x0066347C
      // (set) Token: 0x06002F65 RID: 12133 RVA: 0x00663484 File Offset: 0x00663484
      public List<StarterTaskCard> StarterCards { get; set; } = new List<StarterTaskCard>();

      // Token: 0x17000751 RID: 1873
      // (get) Token: 0x06002F66 RID: 12134 RVA: 0x00663490 File Offset: 0x00663490
      // (set) Token: 0x06002F67 RID: 12135 RVA: 0x00663498 File Offset: 0x00663498
      [Nullable(2)]
      public string GoalLine
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x17000752 RID: 1874
      // (get) Token: 0x06002F68 RID: 12136 RVA: 0x006634A4 File Offset: 0x006634A4
      // (set) Token: 0x06002F69 RID: 12137 RVA: 0x006634AC File Offset: 0x006634AC
      public bool? UserKnowsAgents { get; set; }

      // Token: 0x17000753 RID: 1875
      // (get) Token: 0x06002F6A RID: 12138 RVA: 0x006634B8 File Offset: 0x006634B8
      // (set) Token: 0x06002F6B RID: 12139 RVA: 0x006634C0 File Offset: 0x006634C0
      [Nullable(new byte[] { 2, 1 })]
      public List<string> UserCustomizedSlugs
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }
    }
  }
}
