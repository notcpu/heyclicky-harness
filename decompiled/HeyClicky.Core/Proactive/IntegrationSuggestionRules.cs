using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeyClicky.Core.Proactive
{
  // Token: 0x020000BC RID: 188
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class IntegrationSuggestionRules
  {
    // Token: 0x06000C71 RID: 3185 RVA: 0x0037D6DC File Offset: 0x0037D6DC
    private static IntegrationSuggestionRules.Entry H(string slug, string name, params string[] hosts)
    {
      return new IntegrationSuggestionRules.Entry(slug, name, hosts, Array.Empty<string>(), Array.Empty<string>(), null);
    }

    // Token: 0x06000C72 RID: 3186 RVA: 0x0037D6F4 File Offset: 0x0037D6F4
    public void SetAvailableToolkits(IEnumerable<string> slugs)
    {
      this.available = slugs.Select<string, string>((string s) => s.ToLowerInvariant()).ToHashSet<string>();
      foreach (string text in this.dwell.Keys.Where<string>((string k) => !this.available.Contains(k)).ToList<string>())
      {
        this.dwell.Remove(text);
      }
    }

    // Token: 0x170002B7 RID: 695
    // (get) Token: 0x06000C73 RID: 3187 RVA: 0x0037D798 File Offset: 0x0037D798
    public int AvailableToolkitCount
    {
      get
      {
        return this.available.Count;
      }
    }

    // Token: 0x170002B8 RID: 696
    // (get) Token: 0x06000C74 RID: 3188 RVA: 0x0037D7A8 File Offset: 0x0037D7A8
    private IEnumerable<IntegrationSuggestionRules.Entry> Active
    {
      get
      {
        return from e in IntegrationSuggestionRules.Popular.Concat<IntegrationSuggestionRules.Entry>(IntegrationSuggestionRules.Catalog)
          where this.available.Contains(e.Slug)
          select e;
      }
    }

    // Token: 0x06000C75 RID: 3189 RVA: 0x0037D7CC File Offset: 0x0037D7CC
    public static bool IsSpotify(ForegroundActivity a)
    {
      if (!a.ProcessName.Equals("spotify", StringComparison.OrdinalIgnoreCase))
      {
        if (a.IsBrowser)
        {
          string host = a.Host;
          if (host != null)
          {
            return host == "spotify.com" || host.EndsWith(".spotify.com", StringComparison.Ordinal);
          }
        }
        return false;
      }
      return true;
    }

    // Token: 0x06000C76 RID: 3190 RVA: 0x0037D820 File Offset: 0x0037D820
    [return: Nullable(2)]
    public IntegrationCandidate Detect(ForegroundActivity a)
    {
      IntegrationSuggestionRules.<>c__DisplayClass21_0 CS$<>8__locals1 = new IntegrationSuggestionRules.<>c__DisplayClass21_0();
      CS$<>8__locals1.a = a;
      List<IntegrationSuggestionRules.Entry> list = this.Active.ToList<IntegrationSuggestionRules.Entry>();
      if (list.Count == 0)
      {
        return null;
      }
      IntegrationSuggestionRules.Entry entry = list.FirstOrDefault<IntegrationSuggestionRules.Entry>(delegate(IntegrationSuggestionRules.Entry e)
      {
        IEnumerable<string> processes = e.Processes;
        Func<string, bool> func;
        if ((func = CS$<>8__locals1.<>9__8) == null)
        {
          func = (CS$<>8__locals1.<>9__8 = (string p) => p.Equals(CS$<>8__locals1.a.ProcessName, StringComparison.OrdinalIgnoreCase));
        }
        return processes.Any<string>(func);
      });
      if (entry != null)
      {
        return new IntegrationCandidate(entry.Slug, entry.Name, CS$<>8__locals1.a.AppName, null);
      }
      if (CS$<>8__locals1.a.IsBrowser)
      {
        CS$<>8__locals1.host = CS$<>8__locals1.a.Host;
        if (CS$<>8__locals1.host != null)
        {
          List<ValueTuple<IntegrationSuggestionRules.Entry, int?>> list2 = list.Select<IntegrationSuggestionRules.Entry, ValueTuple<IntegrationSuggestionRules.Entry, int?>>((IntegrationSuggestionRules.Entry e) => new ValueTuple<IntegrationSuggestionRules.Entry, int?>(e, e.HostSpecificity(CS$<>8__locals1.host))).Where<ValueTuple<IntegrationSuggestionRules.Entry, int?>>(delegate([TupleElementNames(new string[] { "Entry", "Score" })] ValueTuple<IntegrationSuggestionRules.Entry, int?> x)
          {
            int? item = x.Item2;
            return item != null;
          }).ToList<ValueTuple<IntegrationSuggestionRules.Entry, int?>>();
          if (list2.Count == 0)
          {
            return null;
          }
          CS$<>8__locals1.best = list2.Max<ValueTuple<IntegrationSuggestionRules.Entry, int?>>(([TupleElementNames(new string[] { "Entry", "Score" })] ValueTuple<IntegrationSuggestionRules.Entry, int?> x) => x.Item2.Value);
          List<IntegrationSuggestionRules.Entry> list3 = (from x in list2.Where<ValueTuple<IntegrationSuggestionRules.Entry, int?>>(delegate([TupleElementNames(new string[] { "Entry", "Score" })] ValueTuple<IntegrationSuggestionRules.Entry, int?> x)
            {
              int? item2 = x.Item2;
              int best = CS$<>8__locals1.best;
              return (item2.GetValueOrDefault() == best) & (item2 != null);
            })
            select x.Item1).ToList<IntegrationSuggestionRules.Entry>();
          IntegrationSuggestionRules.Entry entry2;
          if (list3.Count != 1)
          {
            CS$<>8__locals1.path = CS$<>8__locals1.a.Path;
            if ((entry2 = ((CS$<>8__locals1.path != null) ? list3.FirstOrDefault<IntegrationSuggestionRules.Entry>((IntegrationSuggestionRules.Entry e) => e.MatchesPath(CS$<>8__locals1.path)) : null)) == null)
            {
              IntegrationSuggestionRules.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
              string windowTitle = CS$<>8__locals1.a.WindowTitle;
              CS$<>8__locals2.title = ((windowTitle != null) ? windowTitle.ToLowerInvariant() : null);
              entry2 = ((CS$<>8__locals1.title != null) ? list3.FirstOrDefault<IntegrationSuggestionRules.Entry>((IntegrationSuggestionRules.Entry e) => e.MatchesTitle(CS$<>8__locals1.title)) : null);
            }
          }
          else
          {
            entry2 = list3[0];
          }
          IntegrationSuggestionRules.Entry entry3 = entry2;
          if (entry3 != null)
          {
            return new IntegrationCandidate(entry3.Slug, entry3.Name, CS$<>8__locals1.a.AppName, CS$<>8__locals1.host);
          }
          return null;
        }
      }
      return null;
    }

    // Token: 0x06000C77 RID: 3191 RVA: 0x0037DA08 File Offset: 0x0037DA08
    [return: TupleElementNames(new string[] { "Outcome", "Candidate" })]
    [return: Nullable(new byte[] { 0, 2 })]
    public ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate> Sample([Nullable(2)] ForegroundActivity a, DateTimeOffset now, bool suppressed, ProactiveStoreData state)
    {
      DateTimeOffset? dateTimeOffset = this.lastSample;
      this.lastSample = new DateTimeOffset?(now);
      if (suppressed || a == null)
      {
        return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.None, null);
      }
      TimeSpan timeSpan;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        timeSpan = now - valueOrDefault;
      }
      else
      {
        timeSpan = TimeSpan.Zero;
      }
      TimeSpan timeSpan2 = timeSpan;
      if (timeSpan2 <= TimeSpan.Zero)
      {
        return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.None, null);
      }
      if (timeSpan2 > IntegrationSuggestionRules.MaximumCreditedSample)
      {
        timeSpan2 = IntegrationSuggestionRules.MaximumCreditedSample;
      }
      if (!state.SpotifyVoiceTipShown && IntegrationSuggestionRules.IsSpotify(a))
      {
        this.spotifyDwell += timeSpan2;
        if (!(this.spotifyDwell >= IntegrationSuggestionRules.SpotifyDwellThreshold))
        {
          return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.None, null);
        }
        return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.SpotifyTipReady, null);
      }
      else
      {
        IntegrationCandidate integrationCandidate = this.Detect(a);
        if (integrationCandidate == null)
        {
          return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.None, null);
        }
        TimeSpan timeSpan3 = this.dwell.GetValueOrDefault(integrationCandidate.ToolkitSlug) + timeSpan2;
        this.dwell[integrationCandidate.ToolkitSlug] = timeSpan3;
        if (!(timeSpan3 >= IntegrationSuggestionRules.DwellThreshold) || !IntegrationSuggestionRules.ShouldPrompt(integrationCandidate.ToolkitSlug, now, state))
        {
          return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.None, null);
        }
        return new ValueTuple<IntegrationSuggestionRules.SampleOutcome, IntegrationCandidate>(IntegrationSuggestionRules.SampleOutcome.CandidateReady, integrationCandidate);
      }
    }

    // Token: 0x06000C78 RID: 3192 RVA: 0x0037DB34 File Offset: 0x0037DB34
    public static bool ShouldPrompt(string slug, DateTimeOffset now, ProactiveStoreData state)
    {
      DateTimeOffset dateTimeOffset;
      return !state.IntegrationNeverAsk.Contains(slug) && (!state.IntegrationSnoozedUntil.TryGetValue(slug, out dateTimeOffset) || !(dateTimeOffset > now)) && state.IntegrationPromptTimestamps.Count<DateTimeOffset>((DateTimeOffset t) => t >= now - IntegrationSuggestionRules.PromptsWindow) < 3;
    }

    // Token: 0x06000C79 RID: 3193 RVA: 0x0037DB98 File Offset: 0x0037DB98
    [NullableContext(2)]
    public static string SuppressionReason(in IntegrationSuggestionRules.SuppressionInputs s)
    {
      if (!s.SignedIn)
      {
        return "signedOut";
      }
      if (!s.PermissionsComplete)
      {
        return "permissionsIncomplete";
      }
      if (!s.OnboardingComplete)
      {
        return "onboardingIncomplete";
      }
      if (s.QuietReason != null)
      {
        return "quietContext";
      }
      if (s.NotchExpanded)
      {
        return "notchExpanded";
      }
      if (s.SpotifyTipActive)
      {
        return "spotifyVoiceTipAlreadyActive";
      }
      if (s.SuggestionActive)
      {
        return "suggestionAlreadyActive";
      }
      if (s.VoiceBusy)
      {
        return "voiceBusy";
      }
      if (s.IntroActive)
      {
        return "onboardingIntroActive";
      }
      if (s.TutorialEngaged)
      {
        return "onboardingTutorialEngaged";
      }
      if (s.PostOnboardingQuietPeriod)
      {
        return "postOnboardingQuietPeriod";
      }
      return null;
    }

    // Token: 0x06000C7A RID: 3194 RVA: 0x0037DC40 File Offset: 0x0037DC40
    [NullableContext(2)]
    public static string AmbientQuietReason(bool anotherAppUsesMicrophone, bool doNotDisturb)
    {
      if (anotherAppUsesMicrophone)
      {
        return "call_microphone_in_use_by_another_app";
      }
      if (!doNotDisturb)
      {
        return null;
      }
      return "do_not_disturb";
    }

    // Token: 0x06000C7B RID: 3195 RVA: 0x0037DC58 File Offset: 0x0037DC58
    public void ResetDwell(string slug)
    {
      this.dwell[slug] = TimeSpan.Zero;
    }

    // Token: 0x06000C7C RID: 3196 RVA: 0x0037DC6C File Offset: 0x0037DC6C
    public void ResetSpotifyDwell()
    {
      this.spotifyDwell = TimeSpan.Zero;
    }

    // Token: 0x06000C7D RID: 3197 RVA: 0x0037DC7C File Offset: 0x0037DC7C
    public void ResetAll()
    {
      this.dwell.Clear();
      this.spotifyDwell = TimeSpan.Zero;
      this.lastSample = null;
    }

    // Token: 0x0400054B RID: 1355
    public static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(2.5);

    // Token: 0x0400054C RID: 1356
    public static readonly TimeSpan DwellThreshold = TimeSpan.FromSeconds(10L);

    // Token: 0x0400054D RID: 1357
    public static readonly TimeSpan SpotifyDwellThreshold = TimeSpan.FromSeconds(7.5);

    // Token: 0x0400054E RID: 1358
    public static readonly TimeSpan NotNowSnooze = TimeSpan.FromHours(48);

    // Token: 0x0400054F RID: 1359
    public static readonly TimeSpan MaximumCreditedSample = TimeSpan.FromSeconds(30L);

    // Token: 0x04000550 RID: 1360
    public const int MaximumPromptsPerDay = 3;

    // Token: 0x04000551 RID: 1361
    public static readonly TimeSpan PromptsWindow = TimeSpan.FromHours(24);

    // Token: 0x04000552 RID: 1362
    private static readonly IntegrationSuggestionRules.Entry[] Popular = new IntegrationSuggestionRules.Entry[]
    {
      new IntegrationSuggestionRules.Entry("notion", "Notion", new string[] { "notion.so", "notion.site" }, new string[] { "notion" }, new string[] { "notion" }, null),
      new IntegrationSuggestionRules.Entry("linear", "Linear", new string[] { "linear.app" }, new string[] { "linear" }, new string[] { "linear" }, null),
      new IntegrationSuggestionRules.Entry("github", "GitHub", new string[] { "github.com" }, new string[] { "github" }, new string[] { "githubdesktop" }, null),
      new IntegrationSuggestionRules.Entry("gmail", "Gmail", new string[] { "mail.google.com" }, new string[] { "gmail" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("googlecalendar", "Google Calendar", new string[] { "calendar.google.com" }, new string[] { "google calendar" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("googledrive", "Google Drive", new string[] { "drive.google.com" }, new string[] { "google drive" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("googledocs", "Google Docs", new string[] { "docs.google.com" }, new string[] { "google docs" }, Array.Empty<string>(), new string[] { "/document" }),
      new IntegrationSuggestionRules.Entry("googlesheets", "Google Sheets", new string[] { "docs.google.com" }, new string[] { "google sheets" }, Array.Empty<string>(), new string[] { "/spreadsheets" }),
      new IntegrationSuggestionRules.Entry("slack", "Slack", new string[] { "app.slack.com", "slack.com" }, new string[] { "slack" }, new string[] { "slack" }, null),
      new IntegrationSuggestionRules.Entry("jira", "Jira", new string[] { "atlassian.net" }, new string[] { "jira" }, Array.Empty<string>(), new string[] { "/jira", "/browse", "/issues", "/secure", "/projects" }),
      new IntegrationSuggestionRules.Entry("asana", "Asana", new string[] { "app.asana.com", "asana.com" }, new string[] { "asana" }, new string[] { "asana" }, null),
      new IntegrationSuggestionRules.Entry("trello", "Trello", new string[] { "trello.com" }, new string[] { "trello" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("clickup", "ClickUp", new string[] { "clickup.com", "app.clickup.com" }, new string[] { "clickup" }, new string[] { "clickup" }, null),
      new IntegrationSuggestionRules.Entry("airtable", "Airtable", new string[] { "airtable.com" }, new string[] { "airtable" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("hubspot", "HubSpot", new string[] { "app.hubspot.com", "hubspot.com" }, new string[] { "hubspot" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("salesforce", "Salesforce", new string[] { "salesforce.com", "force.com" }, new string[] { "salesforce" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("zendesk", "Zendesk", new string[] { "zendesk.com" }, new string[] { "zendesk" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("discord", "Discord", new string[] { "discord.com", "discordapp.com" }, new string[] { "discord" }, new string[] { "discord", "discordcanary", "discordptb" }, null),
      new IntegrationSuggestionRules.Entry("dropbox", "Dropbox", new string[] { "dropbox.com" }, new string[] { "dropbox" }, Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("zoom", "Zoom", new string[] { "zoom.us" }, new string[] { "zoom" }, new string[] { "zoom" }, null)
    };

    // Token: 0x04000553 RID: 1363
    private static readonly IntegrationSuggestionRules.Entry[] Catalog = new IntegrationSuggestionRules.Entry[]
    {
      IntegrationSuggestionRules.H("apaleo", "Apaleo", new string[] { "apaleo.com" }),
      IntegrationSuggestionRules.H("attio", "Attio", new string[] { "attio.com" }),
      IntegrationSuggestionRules.H("basecamp", "Basecamp", new string[] { "basecamp.com" }),
      IntegrationSuggestionRules.H("bitbucket", "Bitbucket", new string[] { "bitbucket.org" }),
      IntegrationSuggestionRules.H("blackbaud", "Blackbaud", new string[] { "blackbaud.com" }),
      IntegrationSuggestionRules.H("boldsign", "Boldsign", new string[] { "boldsign.com" }),
      IntegrationSuggestionRules.H("box", "Box", new string[] { "box.com" }),
      IntegrationSuggestionRules.H("cal", "Cal", new string[] { "cal.com" }),
      IntegrationSuggestionRules.H("calendly", "Calendly", new string[] { "calendly.com" }),
      IntegrationSuggestionRules.H("canva", "Canva", new string[] { "canva.com" }),
      IntegrationSuggestionRules.H("capsule_crm", "Capsule CRM", new string[] { "capsulecrm.com" }),
      new IntegrationSuggestionRules.Entry("confluence", "Confluence", new string[] { "atlassian.net" }, new string[] { "confluence" }, Array.Empty<string>(), new string[] { "/wiki" }),
      IntegrationSuggestionRules.H("contentful", "Contentful", new string[] { "contentful.com" }),
      IntegrationSuggestionRules.H("crowdin", "Crowdin", new string[] { "crowdin.com" }),
      IntegrationSuggestionRules.H("dart", "Dart", new string[] { "itsdart.com" }),
      IntegrationSuggestionRules.H("dialpad", "Dialpad", new string[] { "dialpad.com" }),
      IntegrationSuggestionRules.H("digital_ocean", "DigitalOcean", new string[] { "digitalocean.com" }),
      IntegrationSuggestionRules.H("dub", "Dub", new string[] { "dub.co" }),
      IntegrationSuggestionRules.H("dynamics365", "Dynamics 365", new string[] { "dynamics.com" }),
      IntegrationSuggestionRules.H("eventbrite", "Eventbrite", new string[] { "eventbrite.com" }),
      new IntegrationSuggestionRules.Entry("excel", "Excel", new string[] { "excel.cloud.microsoft" }, Array.Empty<string>(), new string[] { "excel" }, null),
      IntegrationSuggestionRules.H("exist", "Exist", new string[] { "exist.io" }),
      IntegrationSuggestionRules.H("facebook", "Facebook", new string[] { "facebook.com" }),
      IntegrationSuggestionRules.H("fathom", "Fathom", new string[] { "fathom.video" }),
      new IntegrationSuggestionRules.Entry("figma", "Figma", new string[] { "figma.com" }, Array.Empty<string>(), new string[] { "figma" }, null),
      IntegrationSuggestionRules.H("freeagent", "Freeagent", new string[] { "freeagent.com" }),
      IntegrationSuggestionRules.H("freshbooks", "FreshBooks", new string[] { "freshbooks.com" }),
      IntegrationSuggestionRules.H("gitlab", "GitLab", new string[] { "gitlab.com" }),
      IntegrationSuggestionRules.H("gong", "Gong", new string[] { "gong.io" }),
      IntegrationSuggestionRules.H("google_analytics", "Google Analytics", new string[] { "analytics.google.com" }),
      IntegrationSuggestionRules.H("google_classroom", "Google Classroom", new string[] { "classroom.google.com" }),
      IntegrationSuggestionRules.H("google_search_console", "Google Search Console", new string[] { "search.google.com" }),
      IntegrationSuggestionRules.H("googleads", "Google Ads", new string[] { "ads.google.com" }),
      IntegrationSuggestionRules.H("googlemeet", "Google Meet", new string[] { "meet.google.com" }),
      IntegrationSuggestionRules.H("googlephotos", "Google Photos", new string[] { "photos.google.com" }),
      IntegrationSuggestionRules.H("googletasks", "Google Tasks", new string[] { "tasks.google.com" }),
      IntegrationSuggestionRules.H("gorgias", "Gorgias", new string[] { "gorgias.com" }),
      IntegrationSuggestionRules.H("gumroad", "Gumroad", new string[] { "gumroad.com" }),
      IntegrationSuggestionRules.H("harvest", "Harvest", new string[] { "getharvest.com" }),
      IntegrationSuggestionRules.H("hugging_face", "Hugging Face", new string[] { "huggingface.co" }),
      IntegrationSuggestionRules.H("instagram", "Instagram", new string[] { "instagram.com" }),
      IntegrationSuggestionRules.H("intercom", "Intercom", new string[] { "intercom.com" }),
      IntegrationSuggestionRules.H("kit", "Kit", new string[] { "kit.com" }),
      IntegrationSuggestionRules.H("linkedin", "LinkedIn", new string[] { "linkedin.com" }),
      IntegrationSuggestionRules.H("linkhut", "Linkhut", new string[] { "linkhut.org" }),
      IntegrationSuggestionRules.H("mailchimp", "Mailchimp", new string[] { "mailchimp.com" }),
      new IntegrationSuggestionRules.Entry("microsoft_teams", "Microsoft Teams", new string[] { "teams.microsoft.com" }, Array.Empty<string>(), new string[] { "ms-teams", "teams" }, null),
      IntegrationSuggestionRules.H("miro", "Miro", new string[] { "miro.com" }),
      IntegrationSuggestionRules.H("monday", "Monday", new string[] { "monday.com" }),
      IntegrationSuggestionRules.H("moneybird", "Moneybird", new string[] { "moneybird.com" }),
      IntegrationSuggestionRules.H("mural", "Mural", new string[] { "mural.co" }),
      IntegrationSuggestionRules.H("omnisend", "Omnisend", new string[] { "omnisend.com" }),
      new IntegrationSuggestionRules.Entry("one_drive", "OneDrive", new string[] { "onedrive.live.com" }, Array.Empty<string>(), Array.Empty<string>(), null),
      new IntegrationSuggestionRules.Entry("outlook", "Outlook", new string[] { "outlook.office.com", "outlook.live.com", "outlook.office365.com" }, Array.Empty<string>(), new string[] { "outlook", "olk" }, null),
      IntegrationSuggestionRules.H("pagerduty", "PagerDuty", new string[] { "pagerduty.com" }),
      IntegrationSuggestionRules.H("prisma", "Prisma", new string[] { "console.prisma.io" }),
      IntegrationSuggestionRules.H("productboard", "Productboard", new string[] { "productboard.com" }),
      IntegrationSuggestionRules.H("pushbullet", "Pushbullet", new string[] { "pushbullet.com" }),
      IntegrationSuggestionRules.H("quickbooks", "QuickBooks", new string[] { "quickbooks.intuit.com" }),
      IntegrationSuggestionRules.H("reddit", "Reddit", new string[] { "reddit.com" }),
      IntegrationSuggestionRules.H("reddit_ads", "Reddit Ads", new string[] { "ads.reddit.com" }),
      IntegrationSuggestionRules.H("roam", "Roam", new string[] { "ro.am" }),
      IntegrationSuggestionRules.H("sentry", "Sentry", new string[] { "sentry.io" }),
      IntegrationSuggestionRules.H("servicem8", "Servicem8", new string[] { "servicem8.com" }),
      IntegrationSuggestionRules.H("share_point", "SharePoint", new string[] { "sharepoint.com" }),
      IntegrationSuggestionRules.H("shippo", "Shippo", new string[] { "goshippo.com" }),
      IntegrationSuggestionRules.H("splitwise", "Splitwise", new string[] { "splitwise.com" }),
      IntegrationSuggestionRules.H("square", "Square", new string[] { "squareup.com" }),
      IntegrationSuggestionRules.H("stack_exchange", "Stack Exchange", new string[] { "stackexchange.com" }),
      IntegrationSuggestionRules.H("stripe", "Stripe", new string[] { "stripe.com" }),
      IntegrationSuggestionRules.H("supabase", "Supabase", new string[] { "supabase.com" }),
      IntegrationSuggestionRules.H("ticktick", "Ticktick", new string[] { "ticktick.com" }),
      IntegrationSuggestionRules.H("timely", "Timely", new string[] { "timelyapp.com" }),
      new IntegrationSuggestionRules.Entry("todoist", "Todoist", new string[] { "todoist.com" }, Array.Empty<string>(), new string[] { "todoist" }, null),
      IntegrationSuggestionRules.H("toneden", "Toneden", new string[] { "toneden.io" }),
      IntegrationSuggestionRules.H("typeform", "Typeform", new string[] { "typeform.com" }),
      IntegrationSuggestionRules.H("wakatime", "WakaTime", new string[] { "wakatime.com" }),
      IntegrationSuggestionRules.H("webex", "Webex", new string[] { "webex.com" }),
      new IntegrationSuggestionRules.Entry("whatsapp", "WhatsApp Business", new string[] { "web.whatsapp.com" }, Array.Empty<string>(), new string[] { "whatsapp" }, null),
      IntegrationSuggestionRules.H("wrike", "Wrike", new string[] { "wrike.com" }),
      IntegrationSuggestionRules.H("ynab", "YNAB", new string[] { "ynab.com" }),
      IntegrationSuggestionRules.H("youtube", "YouTube", new string[] { "youtube.com" }),
      IntegrationSuggestionRules.H("zeplin", "Zeplin", new string[] { "zeplin.io" }),
      IntegrationSuggestionRules.H("zoho_bigin", "Zoho Bigin", new string[] { "bigin.com" }),
      IntegrationSuggestionRules.H("zoho_books", "Zoho Books", new string[] { "books.zoho.com" }),
      IntegrationSuggestionRules.H("zoho_desk", "Zoho Desk", new string[] { "desk.zoho.com" }),
      IntegrationSuggestionRules.H("zoho_inventory", "Zoho Inventory", new string[] { "inventory.zoho.com" }),
      IntegrationSuggestionRules.H("zoho_invoice", "Zoho Invoice", new string[] { "invoice.zoho.com" }),
      IntegrationSuggestionRules.H("zoho_mail", "Zoho Mail", new string[] { "mail.zoho.com" })
    };

    // Token: 0x04000554 RID: 1364
    private HashSet<string> available = new HashSet<string>();

    // Token: 0x04000555 RID: 1365
    private readonly Dictionary<string, TimeSpan> dwell = new Dictionary<string, TimeSpan>();

    // Token: 0x04000556 RID: 1366
    private TimeSpan spotifyDwell;

    // Token: 0x04000557 RID: 1367
    private DateTimeOffset? lastSample;

    // Token: 0x020003CC RID: 972
    [Nullable(0)]
    private sealed class Entry : IEquatable<IntegrationSuggestionRules.Entry>
    {
      // Token: 0x06002A6A RID: 10858 RVA: 0x00426818 File Offset: 0x00426818
      public Entry(string Slug, string Name, string[] Hosts, string[] TitleNeedles, string[] Processes, [Nullable(new byte[] { 2, 1 })] string[] Paths = null)
      {
        this.Slug = Slug;
        this.Name = Name;
        this.Hosts = Hosts;
        this.TitleNeedles = TitleNeedles;
        this.Processes = Processes;
        this.Paths = Paths;
        base..ctor();
      }

      // Token: 0x17000822 RID: 2082
      // (get) Token: 0x06002A6B RID: 10859 RVA: 0x00426850 File Offset: 0x00426850
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(IntegrationSuggestionRules.Entry);
        }
      }

      // Token: 0x17000823 RID: 2083
      // (get) Token: 0x06002A6C RID: 10860 RVA: 0x0042685C File Offset: 0x0042685C
      // (set) Token: 0x06002A6D RID: 10861 RVA: 0x00426864 File Offset: 0x00426864
      public string Slug { get; set; }

      // Token: 0x17000824 RID: 2084
      // (get) Token: 0x06002A6E RID: 10862 RVA: 0x00426870 File Offset: 0x00426870
      // (set) Token: 0x06002A6F RID: 10863 RVA: 0x00426878 File Offset: 0x00426878
      public string Name { get; set; }

      // Token: 0x17000825 RID: 2085
      // (get) Token: 0x06002A70 RID: 10864 RVA: 0x00426884 File Offset: 0x00426884
      // (set) Token: 0x06002A71 RID: 10865 RVA: 0x0042688C File Offset: 0x0042688C
      public string[] Hosts { get; set; }

      // Token: 0x17000826 RID: 2086
      // (get) Token: 0x06002A72 RID: 10866 RVA: 0x00426898 File Offset: 0x00426898
      // (set) Token: 0x06002A73 RID: 10867 RVA: 0x004268A0 File Offset: 0x004268A0
      public string[] TitleNeedles { get; set; }

      // Token: 0x17000827 RID: 2087
      // (get) Token: 0x06002A74 RID: 10868 RVA: 0x004268AC File Offset: 0x004268AC
      // (set) Token: 0x06002A75 RID: 10869 RVA: 0x004268B4 File Offset: 0x004268B4
      public string[] Processes { get; set; }

      // Token: 0x17000828 RID: 2088
      // (get) Token: 0x06002A76 RID: 10870 RVA: 0x004268C0 File Offset: 0x004268C0
      // (set) Token: 0x06002A77 RID: 10871 RVA: 0x004268C8 File Offset: 0x004268C8
      [Nullable(new byte[] { 2, 1 })]
      public string[] Paths
      {
        [return: Nullable(new byte[] { 2, 1 })]
        get;
        [param: Nullable(new byte[] { 2, 1 })]
        set;
      }

      // Token: 0x06002A78 RID: 10872 RVA: 0x004268D4 File Offset: 0x004268D4
      public int? HostSpecificity(string host)
      {
        int? num = null;
        foreach (string text in this.Hosts)
        {
          if (host == text || host.EndsWith("." + text, StringComparison.Ordinal))
          {
            num = new int?(Math.Max(num.GetValueOrDefault(), text.Length));
          }
        }
        return num;
      }

      // Token: 0x06002A79 RID: 10873 RVA: 0x0042693C File Offset: 0x0042693C
      public bool MatchesPath(string path)
      {
        string[] paths = this.Paths;
        return paths != null && paths.Length > 0 && this.Paths.Any<string>((string p) => path.StartsWith(p, StringComparison.OrdinalIgnoreCase));
      }

      // Token: 0x06002A7A RID: 10874 RVA: 0x00426980 File Offset: 0x00426980
      public bool MatchesTitle(string lowered)
      {
        return this.TitleNeedles.Any<string>(new Func<string, bool>(lowered.Contains));
      }

      // Token: 0x06002A7B RID: 10875 RVA: 0x0042699C File Offset: 0x0042699C
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Entry");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A7C RID: 10876 RVA: 0x004269E8 File Offset: 0x004269E8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Hosts = ");
        builder.Append(this.Hosts);
        builder.Append(", TitleNeedles = ");
        builder.Append(this.TitleNeedles);
        builder.Append(", Processes = ");
        builder.Append(this.Processes);
        builder.Append(", Paths = ");
        builder.Append(this.Paths);
        return true;
      }

      // Token: 0x06002A7D RID: 10877 RVA: 0x00426A94 File Offset: 0x00426A94
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(IntegrationSuggestionRules.Entry left, IntegrationSuggestionRules.Entry right)
      {
        return !(left == right);
      }

      // Token: 0x06002A7E RID: 10878 RVA: 0x00426AA0 File Offset: 0x00426AA0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(IntegrationSuggestionRules.Entry left, IntegrationSuggestionRules.Entry right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002A7F RID: 10879 RVA: 0x00426AB4 File Offset: 0x00426AB4
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Hosts>k__BackingField)) * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<TitleNeedles>k__BackingField)) * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Processes>k__BackingField)) * -1521134295 + EqualityComparer<string[]>.Default.GetHashCode(this.<Paths>k__BackingField);
      }

      // Token: 0x06002A80 RID: 10880 RVA: 0x00426B5C File Offset: 0x00426B5C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as IntegrationSuggestionRules.Entry);
      }

      // Token: 0x06002A81 RID: 10881 RVA: 0x00426B6C File Offset: 0x00426B6C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(IntegrationSuggestionRules.Entry other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string[]>.Default.Equals(this.<Hosts>k__BackingField, other.<Hosts>k__BackingField) && EqualityComparer<string[]>.Default.Equals(this.<TitleNeedles>k__BackingField, other.<TitleNeedles>k__BackingField) && EqualityComparer<string[]>.Default.Equals(this.<Processes>k__BackingField, other.<Processes>k__BackingField) && EqualityComparer<string[]>.Default.Equals(this.<Paths>k__BackingField, other.<Paths>k__BackingField));
      }

      // Token: 0x06002A83 RID: 10883 RVA: 0x00426C38 File Offset: 0x00426C38
      [CompilerGenerated]
      private Entry(IntegrationSuggestionRules.Entry original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Hosts = original.<Hosts>k__BackingField;
        this.TitleNeedles = original.<TitleNeedles>k__BackingField;
        this.Processes = original.<Processes>k__BackingField;
        this.Paths = original.<Paths>k__BackingField;
      }

      // Token: 0x06002A84 RID: 10884 RVA: 0x00426C94 File Offset: 0x00426C94
      [CompilerGenerated]
      public void Deconstruct(out string Slug, out string Name, out string[] Hosts, out string[] TitleNeedles, out string[] Processes, [Nullable(new byte[] { 2, 1 })] out string[] Paths)
      {
        Slug = this.Slug;
        Name = this.Name;
        Hosts = this.Hosts;
        TitleNeedles = this.TitleNeedles;
        Processes = this.Processes;
        Paths = this.Paths;
      }
    }

    // Token: 0x020003CD RID: 973
    [NullableContext(0)]
    public enum SampleOutcome
    {
      // Token: 0x040019C9 RID: 6601
      None,
      // Token: 0x040019CA RID: 6602
      SpotifyTipReady,
      // Token: 0x040019CB RID: 6603
      CandidateReady
    }

    // Token: 0x020003CE RID: 974
    [NullableContext(2)]
    [Nullable(0)]
    public readonly struct SuppressionInputs : IEquatable<IntegrationSuggestionRules.SuppressionInputs>
    {
      // Token: 0x06002A85 RID: 10885 RVA: 0x00426CCC File Offset: 0x00426CCC
      public SuppressionInputs(bool SignedIn, bool PermissionsComplete, bool OnboardingComplete, string QuietReason, bool NotchExpanded, bool SpotifyTipActive, bool SuggestionActive, bool VoiceBusy, bool IntroActive, bool TutorialEngaged, bool PostOnboardingQuietPeriod)
      {
        this.SignedIn = SignedIn;
        this.PermissionsComplete = PermissionsComplete;
        this.OnboardingComplete = OnboardingComplete;
        this.QuietReason = QuietReason;
        this.NotchExpanded = NotchExpanded;
        this.SpotifyTipActive = SpotifyTipActive;
        this.SuggestionActive = SuggestionActive;
        this.VoiceBusy = VoiceBusy;
        this.IntroActive = IntroActive;
        this.TutorialEngaged = TutorialEngaged;
        this.PostOnboardingQuietPeriod = PostOnboardingQuietPeriod;
      }

      // Token: 0x17000829 RID: 2089
      // (get) Token: 0x06002A86 RID: 10886 RVA: 0x00426D30 File Offset: 0x00426D30
      // (set) Token: 0x06002A87 RID: 10887 RVA: 0x00426D38 File Offset: 0x00426D38
      public bool SignedIn { get; set; }

      // Token: 0x1700082A RID: 2090
      // (get) Token: 0x06002A88 RID: 10888 RVA: 0x00426D44 File Offset: 0x00426D44
      // (set) Token: 0x06002A89 RID: 10889 RVA: 0x00426D4C File Offset: 0x00426D4C
      public bool PermissionsComplete { get; set; }

      // Token: 0x1700082B RID: 2091
      // (get) Token: 0x06002A8A RID: 10890 RVA: 0x00426D58 File Offset: 0x00426D58
      // (set) Token: 0x06002A8B RID: 10891 RVA: 0x00426D60 File Offset: 0x00426D60
      public bool OnboardingComplete { get; set; }

      // Token: 0x1700082C RID: 2092
      // (get) Token: 0x06002A8C RID: 10892 RVA: 0x00426D6C File Offset: 0x00426D6C
      // (set) Token: 0x06002A8D RID: 10893 RVA: 0x00426D74 File Offset: 0x00426D74
      public string QuietReason { get; set; }

      // Token: 0x1700082D RID: 2093
      // (get) Token: 0x06002A8E RID: 10894 RVA: 0x00426D80 File Offset: 0x00426D80
      // (set) Token: 0x06002A8F RID: 10895 RVA: 0x00426D88 File Offset: 0x00426D88
      public bool NotchExpanded { get; set; }

      // Token: 0x1700082E RID: 2094
      // (get) Token: 0x06002A90 RID: 10896 RVA: 0x00426D94 File Offset: 0x00426D94
      // (set) Token: 0x06002A91 RID: 10897 RVA: 0x00426D9C File Offset: 0x00426D9C
      public bool SpotifyTipActive { get; set; }

      // Token: 0x1700082F RID: 2095
      // (get) Token: 0x06002A92 RID: 10898 RVA: 0x00426DA8 File Offset: 0x00426DA8
      // (set) Token: 0x06002A93 RID: 10899 RVA: 0x00426DB0 File Offset: 0x00426DB0
      public bool SuggestionActive { get; set; }

      // Token: 0x17000830 RID: 2096
      // (get) Token: 0x06002A94 RID: 10900 RVA: 0x00426DBC File Offset: 0x00426DBC
      // (set) Token: 0x06002A95 RID: 10901 RVA: 0x00426DC4 File Offset: 0x00426DC4
      public bool VoiceBusy { get; set; }

      // Token: 0x17000831 RID: 2097
      // (get) Token: 0x06002A96 RID: 10902 RVA: 0x00426DD0 File Offset: 0x00426DD0
      // (set) Token: 0x06002A97 RID: 10903 RVA: 0x00426DD8 File Offset: 0x00426DD8
      public bool IntroActive { get; set; }

      // Token: 0x17000832 RID: 2098
      // (get) Token: 0x06002A98 RID: 10904 RVA: 0x00426DE4 File Offset: 0x00426DE4
      // (set) Token: 0x06002A99 RID: 10905 RVA: 0x00426DEC File Offset: 0x00426DEC
      public bool TutorialEngaged { get; set; }

      // Token: 0x17000833 RID: 2099
      // (get) Token: 0x06002A9A RID: 10906 RVA: 0x00426DF8 File Offset: 0x00426DF8
      // (set) Token: 0x06002A9B RID: 10907 RVA: 0x00426E00 File Offset: 0x00426E00
      public bool PostOnboardingQuietPeriod { get; set; }

      // Token: 0x06002A9C RID: 10908 RVA: 0x00426E0C File Offset: 0x00426E0C
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SuppressionInputs");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002A9D RID: 10909 RVA: 0x00426E58 File Offset: 0x00426E58
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("SignedIn = ");
        builder.Append(this.SignedIn.ToString());
        builder.Append(", PermissionsComplete = ");
        builder.Append(this.PermissionsComplete.ToString());
        builder.Append(", OnboardingComplete = ");
        builder.Append(this.OnboardingComplete.ToString());
        builder.Append(", QuietReason = ");
        builder.Append(this.QuietReason);
        builder.Append(", NotchExpanded = ");
        builder.Append(this.NotchExpanded.ToString());
        builder.Append(", SpotifyTipActive = ");
        builder.Append(this.SpotifyTipActive.ToString());
        builder.Append(", SuggestionActive = ");
        builder.Append(this.SuggestionActive.ToString());
        builder.Append(", VoiceBusy = ");
        builder.Append(this.VoiceBusy.ToString());
        builder.Append(", IntroActive = ");
        builder.Append(this.IntroActive.ToString());
        builder.Append(", TutorialEngaged = ");
        builder.Append(this.TutorialEngaged.ToString());
        builder.Append(", PostOnboardingQuietPeriod = ");
        builder.Append(this.PostOnboardingQuietPeriod.ToString());
        return true;
      }

      // Token: 0x06002A9E RID: 10910 RVA: 0x00427008 File Offset: 0x00427008
      [CompilerGenerated]
      public static bool operator !=(IntegrationSuggestionRules.SuppressionInputs left, IntegrationSuggestionRules.SuppressionInputs right)
      {
        return !(left == right);
      }

      // Token: 0x06002A9F RID: 10911 RVA: 0x00427014 File Offset: 0x00427014
      [CompilerGenerated]
      public static bool operator ==(IntegrationSuggestionRules.SuppressionInputs left, IntegrationSuggestionRules.SuppressionInputs right)
      {
        return left.Equals(right);
      }

      // Token: 0x06002AA0 RID: 10912 RVA: 0x00427020 File Offset: 0x00427020
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((EqualityComparer<bool>.Default.GetHashCode(this.<SignedIn>k__BackingField) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PermissionsComplete>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<OnboardingComplete>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<QuietReason>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<NotchExpanded>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SpotifyTipActive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<SuggestionActive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<VoiceBusy>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IntroActive>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<TutorialEngaged>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<PostOnboardingQuietPeriod>k__BackingField);
      }

      // Token: 0x06002AA1 RID: 10913 RVA: 0x00427124 File Offset: 0x00427124
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is IntegrationSuggestionRules.SuppressionInputs && this.Equals((IntegrationSuggestionRules.SuppressionInputs)obj);
      }

      // Token: 0x06002AA2 RID: 10914 RVA: 0x0042713C File Offset: 0x0042713C
      [CompilerGenerated]
      public bool Equals(IntegrationSuggestionRules.SuppressionInputs other)
      {
        return EqualityComparer<bool>.Default.Equals(this.<SignedIn>k__BackingField, other.<SignedIn>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PermissionsComplete>k__BackingField, other.<PermissionsComplete>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<OnboardingComplete>k__BackingField, other.<OnboardingComplete>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<QuietReason>k__BackingField, other.<QuietReason>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<NotchExpanded>k__BackingField, other.<NotchExpanded>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SpotifyTipActive>k__BackingField, other.<SpotifyTipActive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<SuggestionActive>k__BackingField, other.<SuggestionActive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<VoiceBusy>k__BackingField, other.<VoiceBusy>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IntroActive>k__BackingField, other.<IntroActive>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<TutorialEngaged>k__BackingField, other.<TutorialEngaged>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<PostOnboardingQuietPeriod>k__BackingField, other.<PostOnboardingQuietPeriod>k__BackingField);
      }

      // Token: 0x06002AA3 RID: 10915 RVA: 0x00427260 File Offset: 0x00427260
      [CompilerGenerated]
      public void Deconstruct(out bool SignedIn, out bool PermissionsComplete, out bool OnboardingComplete, out string QuietReason, out bool NotchExpanded, out bool SpotifyTipActive, out bool SuggestionActive, out bool VoiceBusy, out bool IntroActive, out bool TutorialEngaged, out bool PostOnboardingQuietPeriod)
      {
        SignedIn = this.SignedIn;
        PermissionsComplete = this.PermissionsComplete;
        OnboardingComplete = this.OnboardingComplete;
        QuietReason = this.QuietReason;
        NotchExpanded = this.NotchExpanded;
        SpotifyTipActive = this.SpotifyTipActive;
        SuggestionActive = this.SuggestionActive;
        VoiceBusy = this.VoiceBusy;
        IntroActive = this.IntroActive;
        TutorialEngaged = this.TutorialEngaged;
        PostOnboardingQuietPeriod = this.PostOnboardingQuietPeriod;
      }
    }
  }
}
