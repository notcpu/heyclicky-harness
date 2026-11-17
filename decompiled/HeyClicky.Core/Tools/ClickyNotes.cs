using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009C RID: 156
  [NullableContext(1)]
  [Nullable(0)]
  public static class ClickyNotes
  {
    // Token: 0x060009E3 RID: 2531 RVA: 0x00371374 File Offset: 0x00371374
    [return: Nullable(2)]
    public static string RosterNote(IReadOnlyList<AgentClickyInfo> clickys, IReadOnlyList<AgentTaskInfo> tasks, [Nullable(2)] ClickyNotes.Report lastReport, DateTimeOffset now)
    {
      if (clickys.Count == 0)
      {
        return null;
      }
      IEnumerable<string> enumerable = clickys.Select<AgentClickyInfo, string>(delegate(AgentClickyInfo clicky)
      {
        AgentTaskInfo agentTaskInfo = tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ClickySlug == clicky.Slug && t.IsWorking);
        string text3;
        if (agentTaskInfo != null)
        {
          string text2 = ClickyNotes.LiveWorkDescription(agentTaskInfo, new DateTimeOffset?(now));
          text3 = "WORKING NOW" + ((text2 == null) ? "" : (": " + text2));
        }
        else
        {
          text3 = ((clicky.ThreadId != null) ? "has history" : "fresh");
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(27, 5);
        defaultInterpolatedStringHandler2.AppendLiteral("- \"");
        defaultInterpolatedStringHandler2.AppendFormatted(clicky.Name);
        defaultInterpolatedStringHandler2.AppendLiteral("\" (");
        defaultInterpolatedStringHandler2.AppendFormatted(clicky.RoleText.ToLowerInvariant());
        defaultInterpolatedStringHandler2.AppendLiteral(": ");
        defaultInterpolatedStringHandler2.AppendFormatted(clicky.OneLiner);
        defaultInterpolatedStringHandler2.AppendLiteral(") — clicky_slug: ");
        defaultInterpolatedStringHandler2.AppendFormatted(clicky.Slug);
        defaultInterpolatedStringHandler2.AppendLiteral(", ");
        defaultInterpolatedStringHandler2.AppendFormatted(text3);
        return defaultInterpolatedStringHandler2.ToStringAndClear();
      });
      string text = "[Your Clickys] The user's persistent named agents — ongoing relationships, not one-off tasks. Clickys are ALWAYS addressed by clicky_slug (never by codexThreadId — their thread ids can change):\n" + string.Join("\n", enumerable) + "\nWhat a Clicky knows, found, decided, or did → clicky_memory with its clicky_slug (a read — answer from it; never guess a Clicky's findings). Routing: new background work that fits one of these → sessions_spawn with its clicky_slug. A follow-up or steer for one → sessions_send with its clicky_slug. Work that fits none of these runs as a one-off job on the right edge of the screen: sessions_spawn with just task and label (see [Your jobs]). Found a new Clicky (new_clicky {name, role, description}) only when the user asks for a Clicky, or for something lasting they will keep coming back to.";
      List<string> list = new List<string>();
      if (lastReport != null && clickys.Any<AgentClickyInfo>((AgentClickyInfo c) => c.Slug == lastReport.Slug))
      {
        List<string> list2 = list;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 4);
        defaultInterpolatedStringHandler.AppendLiteral("MOST RECENT REPORT: \"");
        defaultInterpolatedStringHandler.AppendFormatted(lastReport.Name);
        defaultInterpolatedStringHandler.AppendLiteral("\" (clicky_slug: ");
        defaultInterpolatedStringHandler.AppendFormatted(lastReport.Slug);
        defaultInterpolatedStringHandler.AppendLiteral(") ");
        defaultInterpolatedStringHandler.AppendFormatted(lastReport.Verb);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(ClickyNotes.Ago(now - lastReport.At));
        defaultInterpolatedStringHandler.AppendLiteral(". ");
        list2.Add(defaultInterpolatedStringHandler.ToStringAndClear() + "A reply, steer, or follow-up that names no Clicky is for THIS one: sessions_send with its clicky_slug, and answer questions about \"it\" from its report (clicky_memory for details).");
      }
      List<string> list3 = tasks.Where<AgentTaskInfo>((AgentTaskInfo t) => !t.IsDemo && t.ClickySlug != null && (t.RequiresExtraUsageApproval || t.PendingComputerUseRequest != null)).Select<AgentTaskInfo, string>(delegate(AgentTaskInfo t)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(40, 2);
        defaultInterpolatedStringHandler3.AppendLiteral("\"");
        defaultInterpolatedStringHandler3.AppendFormatted(t.ClickyName ?? t.Title);
        defaultInterpolatedStringHandler3.AppendLiteral("\" (clicky_slug: ");
        defaultInterpolatedStringHandler3.AppendFormatted(t.ClickySlug);
        defaultInterpolatedStringHandler3.AppendLiteral(") is waiting on a yes: ");
        return defaultInterpolatedStringHandler3.ToStringAndClear() + (t.RequiresExtraUsageApproval ? "extra_usage (paused: a big task needing more agent messages from the user's plan; approving resumes it to completion)" : "computer_use (it asked for HeyClicky to drive the PC for one step)");
      }).ToList<string>();
      if (list3.Count > 0)
      {
        list.Add(string.Concat(new string[]
        {
          "PENDING REQUEST",
          (list3.Count == 1) ? "" : "S",
          ": ",
          string.Join("; ", list3),
          ". A yes / approve / go ahead / allow from the user answers it: sessions_answer_request with that clicky_slug (decision approve); no / not now is decision decline. When exactly one request is pending, a bare yes or no means that one."
        }));
      }
      if (list.Count != 0)
      {
        return text + "\n" + string.Join("\n", list);
      }
      return text;
    }

    // Token: 0x060009E4 RID: 2532 RVA: 0x00371584 File Offset: 0x00371584
    [return: Nullable(2)]
    internal static string LiveWorkDescription(AgentTaskInfo task, DateTimeOffset? now = null)
    {
      List<string> list = new List<string>();
      string text = ClickyNotes.<LiveWorkDescription>g__Clipped|2_0(task.UserPrompts.LastOrDefault<string>() ?? "", 140);
      if (text.Length > 0)
      {
        list.Add("on \"" + text + "\"");
      }
      int num = (int)((now ?? DateTimeOffset.Now) - task.StartedAt).TotalMinutes;
      List<string> list2 = list;
      string text2;
      if (num >= 1)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
        defaultInterpolatedStringHandler.AppendLiteral("for ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(num);
        defaultInterpolatedStringHandler.AppendLiteral(" min");
        text2 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text2 = "for under a minute";
      }
      list2.Add(text2);
      string text3 = task.Commentary.LastOrDefault<string>((string c) => !string.IsNullOrWhiteSpace(c));
      if (text3 != null)
      {
        list.Add("latest update \"" + ClickyNotes.<LiveWorkDescription>g__Clipped|2_0(text3, 160) + "\"");
      }
      else if (task.Status == AgentTaskStatus.Starting)
      {
        list.Add("waiting to start");
      }
      return string.Join(", ", list);
    }

    // Token: 0x060009E5 RID: 2533 RVA: 0x003716B4 File Offset: 0x003716B4
    private static string Ago(TimeSpan elapsed)
    {
      if (elapsed < TimeSpan.FromMinutes(1L))
      {
        return "just now";
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (elapsed < TimeSpan.FromHours(1))
      {
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(11, 2);
        defaultInterpolatedStringHandler.AppendFormatted<int>((int)elapsed.TotalMinutes);
        defaultInterpolatedStringHandler.AppendLiteral(" minute");
        defaultInterpolatedStringHandler.AppendFormatted(((int)elapsed.TotalMinutes == 1) ? "" : "s");
        defaultInterpolatedStringHandler.AppendLiteral(" ago");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
      defaultInterpolatedStringHandler.AppendFormatted<int>((int)elapsed.TotalHours);
      defaultInterpolatedStringHandler.AppendLiteral(" hour");
      defaultInterpolatedStringHandler.AppendFormatted(((int)elapsed.TotalHours == 1) ? "" : "s");
      defaultInterpolatedStringHandler.AppendLiteral(" ago");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060009E6 RID: 2534 RVA: 0x00371794 File Offset: 0x00371794
    [NullableContext(2)]
    public static string PlanNote(PlanSnapshot plan, PlanUsageBucket displayedMessages = null)
    {
      if (plan == null || plan.IsFailOpenFallback.GetValueOrDefault())
      {
        return null;
      }
      string effectiveTier = plan.EffectiveTier;
      string text;
      if (!(effectiveTier == "max"))
      {
        if (!(effectiveTier == "pro"))
        {
          text = "Free";
        }
        else
        {
          text = "Pro";
        }
      }
      else
      {
        text = "Max";
      }
      string text2 = text;
      string text3 = ((plan.TierSource == "team") ? (" (a seat on the " + (plan.TeamName ?? "company") + " team plan)") : "");
      PlanUsageBucket agents = plan.Usage.Agents;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 5);
      defaultInterpolatedStringHandler.AppendLiteral("[Your plan] ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendFormatted(text3);
      defaultInterpolatedStringHandler.AppendLiteral(". Agent messages this period: ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(agents.Used);
      defaultInterpolatedStringHandler.AppendLiteral(" of ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(agents.Cap);
      defaultInterpolatedStringHandler.AppendLiteral(" used, ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(0, agents.Cap - agents.Used));
      defaultInterpolatedStringHandler.AppendLiteral(" left");
      string text4 = defaultInterpolatedStringHandler.ToStringAndClear();
      if (plan.Plan == "unlimited")
      {
        text4 += ". Talk messages (talking or typing to HeyClicky): unlimited";
      }
      else
      {
        PlanUsageBucket planUsageBucket = displayedMessages ?? plan.Usage.Messages;
        string text5 = text4;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(66, 3);
        defaultInterpolatedStringHandler.AppendLiteral(". Talk messages (talking or typing to HeyClicky): ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(planUsageBucket.Used);
        defaultInterpolatedStringHandler.AppendLiteral(" of ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(planUsageBucket.Cap);
        defaultInterpolatedStringHandler.AppendLiteral(" used, ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(0, planUsageBucket.Cap - planUsageBucket.Used));
        defaultInterpolatedStringHandler.AppendLiteral(" left");
        text4 = text5 + defaultInterpolatedStringHandler.ToStringAndClear();
      }
      string windowResetsAt = plan.Usage.WindowResetsAt;
      DateTimeOffset dateTimeOffset;
      if (windowResetsAt != null && DateTimeOffset.TryParse(windowResetsAt, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTimeOffset))
      {
        text4 = text4 + "; resets " + dateTimeOffset.ToLocalTime().ToString("MMMM d", CultureInfo.InvariantCulture);
      }
      return text4 + ". Silent context, not a message to answer.";
    }

    // Token: 0x060009E7 RID: 2535 RVA: 0x003719D8 File Offset: 0x003719D8
    [NullableContext(2)]
    [return: Nullable(1)]
    public static string EventNote(string clickyName, string clickySlug, [Nullable(1)] string title, [Nullable(1)] string @event, string summary, bool jobsWorld = false, string threadId = null, string jobTitle = null, string resultReport = null)
    {
      string text = title.Trim();
      string text2 = ((threadId != null && threadId.Length > 0) ? ClickyNotes.JobReference(threadId) : "");
      string text4;
      if (clickyName != null && clickyName.Length > 0 && clickySlug != null && clickySlug.Length > 0)
      {
        string text3;
        if (!jobsWorld)
        {
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
          defaultInterpolatedStringHandler.AppendLiteral("Clicky \"");
          defaultInterpolatedStringHandler.AppendFormatted(clickyName);
          defaultInterpolatedStringHandler.AppendLiteral("\" (clicky_slug: ");
          defaultInterpolatedStringHandler.AppendFormatted(clickySlug);
          defaultInterpolatedStringHandler.AppendLiteral(")");
          text3 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        else
        {
          text3 = "Job \"" + clickyName + "\"" + text2;
        }
        text4 = text3;
      }
      else if (jobTitle != null)
      {
        text4 = "Job \"" + ((jobTitle.Length > 0) ? jobTitle : ((text.Length > 0) ? text : "A task")) + "\"" + text2;
      }
      else
      {
        text4 = "\"" + ((text.Length == 0) ? "A task" : text) + "\"";
      }
      if (@event == "finished" && resultReport != null && resultReport.Length > 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
        defaultInterpolatedStringHandler.AppendLiteral("[App] ");
        defaultInterpolatedStringHandler.AppendFormatted(text4);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(@event);
        defaultInterpolatedStringHandler.AppendLiteral(". ");
        return defaultInterpolatedStringHandler.ToStringAndClear() + resultReport;
      }
      string text5 = (summary ?? "").Replace("\n", " ").Trim();
      if (text5.Length > 400)
      {
        text5 = text5.Substring(0, 400);
      }
      return string.Concat(new string[]
      {
        "[App] ",
        text4,
        " ",
        @event,
        (text5.Length == 0) ? "." : (": " + text5)
      });
    }

    // Token: 0x060009E8 RID: 2536 RVA: 0x00371BB8 File Offset: 0x00371BB8
    [NullableContext(2)]
    public static string ResultReport(string factsJson, string fullReply, [Nullable(1)] IReadOnlyList<string> files)
    {
      List<string> list = new List<string>();
      if (factsJson != null && factsJson.Length > 0)
      {
        list.Add("Its facts, to build their card from: " + factsJson);
      }
      if (fullReply != null && fullReply.Length > 0)
      {
        string text = ((fullReply.Length > 6000) ? (fullReply.Substring(0, 6000) + "… (cut here; clicky_memory has the rest)") : fullReply);
        list.Add("Its full report, which they can open from the chat:\n<report>\n" + text + "\n</report>");
      }
      if (files.Count > 0)
      {
        list.Add("Files it made: " + string.Join(", ", files.Take<string>(6)));
      }
      if (list.Count != 0)
      {
        return string.Join("\n", list);
      }
      return null;
    }

    // Token: 0x060009E9 RID: 2537 RVA: 0x00371C70 File Offset: 0x00371C70
    [return: Nullable(2)]
    public static string BrowserStateNote(bool connected, string browser, bool hasChromiumBrowser, bool addsAgain, bool canDrive)
    {
      if (connected)
      {
        return "Browser: their " + browser + " is connected to HeyClicky (jobs can work in their signed-in sites).";
      }
      if (hasChromiumBrowser)
      {
        string text;
        string text2;
        string text3;
        if (addsAgain)
        {
          text = "their " + browser + " extension needs swapping for the new version: HeyClicky's extension is in the Chrome Web Store now and updates itself, and their old preview copy no longer connects";
          text2 = "My " + browser + " extension got an upgrade, and it updates itself now. " + (canDrive ? "Want me to swap it in? Takes a few seconds." : "Want me to show you how to swap it in? Takes a few seconds.");
          text3 = (canDrive ? "approve = you remove the old one and add the new version on their screen, show_how = they do it with you showing them each step" : "approve or show_how = they add the new version with you showing them each step, then remove the old one");
        }
        else
        {
          text = "their " + browser + " isn't connected to HeyClicky yet (HeyClicky's extension is in the Chrome Web Store, a couple of clicks to add)";
          text2 = "This needs my " + browser + " extension. Want to add it? It takes a couple of clicks.";
          text3 = "approve or show_how = they add it from the Chrome Web Store with you showing them each click";
        }
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(780, 6);
        defaultInterpolatedStringHandler.AppendLiteral("Browser: ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(". Before starting a job that must act in their own account on a website (their cart, bank, a post or DM, a booking under their name) that no connected app covers, ask once: in your first line say it needs HeyClicky's ");
        defaultInterpolatedStringHandler.AppendFormatted(browser);
        defaultInterpolatedStringHandler.AppendLiteral(" extension and ask (\"");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendLiteral("\"), and call sessions_spawn with chrome \"ask\": the job waits for their choice and starts by itself once ");
        defaultInterpolatedStringHandler.AppendFormatted(browser);
        defaultInterpolatedStringHandler.AppendLiteral(" is connected. Public information the web search finds (flights, prices, news, research) and their connected apps never need it: start those as usual. Their answer is sessions_answer_request request chrome_setup (");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        defaultInterpolatedStringHandler.AppendLiteral(", decline = the job starts without it and does the website part on their screen). Use chrome \"without\" only when they already said they don't want it connected. Never drive the website yourself because ");
        defaultInterpolatedStringHandler.AppendFormatted(browser);
        defaultInterpolatedStringHandler.AppendLiteral(" isn't set up.");
        return defaultInterpolatedStringHandler.ToStringAndClear();
      }
      if (!canDrive)
      {
        return null;
      }
      return "Browser: this PC has no Chrome or other Chromium browser, so jobs can't work in websites; a website task is yours to drive.";
    }

    // Token: 0x060009EA RID: 2538 RVA: 0x00371DA8 File Offset: 0x00371DA8
    public static string ProgressNote(string displayName, [Nullable(2)] string clickySlug, [Nullable(2)] string threadId, string commentary, bool jobsWorld = false, bool isJob = false)
    {
      string text = (jobsWorld ? null : ((clickySlug != null) ? (" (clicky_slug: " + clickySlug + ")") : null)) ?? ((threadId != null) ? ClickyNotes.JobReference(threadId) : "");
      string text2 = ((jobsWorld || (clickySlug == null && isJob)) ? "Job" : "Clicky");
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 4);
      defaultInterpolatedStringHandler.AppendLiteral("[App] ");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral(" \"");
      defaultInterpolatedStringHandler.AppendFormatted(displayName);
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" is still working and last said: \"");
      defaultInterpolatedStringHandler.AppendFormatted(ClickyNotes.Condensed(commentary));
      defaultInterpolatedStringHandler.AppendLiteral("\"");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060009EB RID: 2539 RVA: 0x00371E74 File Offset: 0x00371E74
    public static string JobEventNote(string name, [Nullable(2)] string threadId, string @event, [Nullable(2)] string summary, [Nullable(2)] string report = null)
    {
      string text = "Job \"" + (string.IsNullOrWhiteSpace(name) ? "A task" : name.Trim()) + "\"" + ((threadId == null) ? "" : ClickyNotes.JobReference(threadId));
      if (@event == "finished" && report != null && report.Length > 0)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
        defaultInterpolatedStringHandler.AppendLiteral("[App] ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(@event);
        defaultInterpolatedStringHandler.AppendLiteral(". ");
        return defaultInterpolatedStringHandler.ToStringAndClear() + report;
      }
      string text2 = (summary ?? "").Replace("\n", " ").Trim();
      if (text2.Length > 400)
      {
        text2 = text2.Substring(0, 400);
      }
      return string.Concat(new string[]
      {
        "[App] ",
        text,
        " ",
        @event,
        (text2.Length == 0) ? "." : (": " + text2)
      });
    }

    // Token: 0x060009EC RID: 2540 RVA: 0x00371F98 File Offset: 0x00371F98
    public static string JobReference(string threadId)
    {
      string text = " (job_id: ";
      Func<string, string> jobIdForThread = AgentToolHooks.JobIdForThread;
      return text + (((jobIdForThread != null) ? jobIdForThread(threadId) : null) ?? threadId) + ")";
    }

    // Token: 0x060009ED RID: 2541 RVA: 0x00371FC4 File Offset: 0x00371FC4
    public static string ProgressNoteKey([Nullable(2)] string clickySlug, [Nullable(2)] string threadId, string taskId)
    {
      return "progress:" + (clickySlug ?? threadId ?? taskId);
    }

    // Token: 0x060009EE RID: 2542 RVA: 0x00371FDC File Offset: 0x00371FDC
    public static string Condensed(string commentary)
    {
      string text = string.Join(" ", from l in commentary.Split('\n', StringSplitOptions.None)
        select l.Trim() into l
        where l.Length > 0
        select l);
      if (text.Length <= 240)
      {
        return text;
      }
      return text.Substring(0, 240).Trim() + "…";
    }

    // Token: 0x060009EF RID: 2543 RVA: 0x00372070 File Offset: 0x00372070
    public static string FinishedLine([Nullable(2)] string clickyName, string summary, bool openedResult = false, bool hasAnotherTask = false)
    {
      string text = ClickyNotes.Subject(clickyName) + " is done.";
      string text2 = ClickyNotes.SpeakableSentence(summary, 240);
      if (text2.Length > 0 && text2 != "Codex child worker completed successfully.")
      {
        text = text + " " + text2;
      }
      if (openedResult)
      {
        text += " It's open on your screen.";
      }
      if (hasAnotherTask)
      {
        text += " It has another task to do next.";
      }
      return text;
    }

    // Token: 0x060009F0 RID: 2544 RVA: 0x003720DC File Offset: 0x003720DC
    public static string StartedLine(string startLine)
    {
      return ClickyNotes.SpeakableSentence(startLine, 240);
    }

    // Token: 0x060009F1 RID: 2545 RVA: 0x003720EC File Offset: 0x003720EC
    public static string AsksToUsePcLine([Nullable(2)] string clickyName, string request)
    {
      string text = ClickyNotes.SpeakableSentence(request, 110);
      string text4;
      if (text.Length != 0)
      {
        string text2 = text.Substring(0, 1).ToLowerInvariant();
        string text3 = text;
        text4 = text2 + text3.Substring(1, text3.Length - 1);
      }
      else
      {
        text4 = "";
      }
      string text5 = text4;
      return ClickyNotes.Subject(clickyName) + " needs one step on your PC: " + text5 + " Want me to do it?";
    }

    // Token: 0x060009F2 RID: 2546 RVA: 0x0037214C File Offset: 0x0037214C
    public static string NeedsChromeLine([Nullable(2)] string jobName, string browserName, bool addsAgain, bool canDoItForThem)
    {
      if (!addsAgain)
      {
        return ClickyNotes.Subject(jobName) + " needs HeyClicky's " + browserName + " extension. Want to add it? It takes a couple of clicks.";
      }
      return string.Concat(new string[]
      {
        ClickyNotes.Subject(jobName),
        " needs my ",
        browserName,
        " extension. It got an upgrade and now updates itself, ",
        canDoItForThem ? "so want me to swap it in?" : "so want me to show you how to swap it in?"
      });
    }

    // Token: 0x060009F3 RID: 2547 RVA: 0x003721B0 File Offset: 0x003721B0
    public static string PausedForExtraUsageLine([Nullable(2)] string clickyName)
    {
      return ClickyNotes.Subject(clickyName) + " is paused because it's a big task that needs more agent messages from your plan to finish. A yes from you keeps it going until it's done.";
    }

    // Token: 0x060009F4 RID: 2548 RVA: 0x003721C4 File Offset: 0x003721C4
    private static string Subject([Nullable(2)] string clickyName)
    {
      if (!string.IsNullOrWhiteSpace(clickyName))
      {
        return clickyName.Trim();
      }
      return "Your job";
    }

    // Token: 0x060009F5 RID: 2549 RVA: 0x003721DC File Offset: 0x003721DC
    public static string SpeakableSentence(string text, int limit = 240)
    {
      string text2 = string.Join(" ", text.Replace("`", "").Replace("*", "").Replace("_", "")
        .Replace("#", "")
        .Split(null, StringSplitOptions.RemoveEmptyEntries));
      if (text2.Length == 0)
      {
        return "";
      }
      if (text2.Length > limit)
      {
        string text3 = text2.Substring(0, limit);
        int num = text3.LastIndexOfAny(new char[] { '.', '!', '?' });
        if (num > 40)
        {
          text2 = text3.Substring(0, num + 1);
        }
        else
        {
          string[] array = text3.Split(' ', StringSplitOptions.None);
          text2 = ((array.Length > 1) ? string.Join(" ", RuntimeHelpers.GetSubArray<string>(array, Range.EndAt(new Index(1, true)))) : text3).TrimEnd(new char[] { ',', ';', ':', ' ' });
        }
      }
      string text4 = ".!?…";
      string text5 = text2;
      if (!text4.Contains(text5[text5.Length - 1]))
      {
        text2 += ".";
      }
      return text2;
    }

    // Token: 0x060009F6 RID: 2550 RVA: 0x003722F0 File Offset: 0x003722F0
    [CompilerGenerated]
    internal static string <LiveWorkDescription>g__Clipped|2_0(string text, int limit)
    {
      string text2 = text.Replace("\r", " ").Replace("\n", " ").Trim();
      if (text2.Length <= limit)
      {
        return text2;
      }
      return text2.Substring(0, limit) + "…";
    }

    // Token: 0x04000479 RID: 1145
    public const int ResultReportReplyLimit = 6000;

    // Token: 0x0400047A RID: 1146
    public const string EmptyReplyStandInSummary = "Codex child worker completed successfully.";

    // Token: 0x0400047B RID: 1147
    public const int ShortRequestCharacters = 110;

    // Token: 0x0400047C RID: 1148
    public const int MaximumSummaryCharacters = 240;

    // Token: 0x02000398 RID: 920
    [Nullable(0)]
    public sealed class Report : IEquatable<ClickyNotes.Report>
    {
      // Token: 0x0600296B RID: 10603 RVA: 0x00422758 File Offset: 0x00422758
      public Report(string Slug, string Name, string Verb, DateTimeOffset At)
      {
        this.Slug = Slug;
        this.Name = Name;
        this.Verb = Verb;
        this.At = At;
        base..ctor();
      }

      // Token: 0x170007FD RID: 2045
      // (get) Token: 0x0600296C RID: 10604 RVA: 0x00422780 File Offset: 0x00422780
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(ClickyNotes.Report);
        }
      }

      // Token: 0x170007FE RID: 2046
      // (get) Token: 0x0600296D RID: 10605 RVA: 0x0042278C File Offset: 0x0042278C
      // (set) Token: 0x0600296E RID: 10606 RVA: 0x00422794 File Offset: 0x00422794
      public string Slug { get; set; }

      // Token: 0x170007FF RID: 2047
      // (get) Token: 0x0600296F RID: 10607 RVA: 0x004227A0 File Offset: 0x004227A0
      // (set) Token: 0x06002970 RID: 10608 RVA: 0x004227A8 File Offset: 0x004227A8
      public string Name { get; set; }

      // Token: 0x17000800 RID: 2048
      // (get) Token: 0x06002971 RID: 10609 RVA: 0x004227B4 File Offset: 0x004227B4
      // (set) Token: 0x06002972 RID: 10610 RVA: 0x004227BC File Offset: 0x004227BC
      public string Verb { get; set; }

      // Token: 0x17000801 RID: 2049
      // (get) Token: 0x06002973 RID: 10611 RVA: 0x004227C8 File Offset: 0x004227C8
      // (set) Token: 0x06002974 RID: 10612 RVA: 0x004227D0 File Offset: 0x004227D0
      public DateTimeOffset At { get; set; }

      // Token: 0x06002975 RID: 10613 RVA: 0x004227DC File Offset: 0x004227DC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Report");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06002976 RID: 10614 RVA: 0x00422828 File Offset: 0x00422828
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Slug = ");
        builder.Append(this.Slug);
        builder.Append(", Name = ");
        builder.Append(this.Name);
        builder.Append(", Verb = ");
        builder.Append(this.Verb);
        builder.Append(", At = ");
        builder.Append(this.At.ToString());
        return true;
      }

      // Token: 0x06002977 RID: 10615 RVA: 0x004228B0 File Offset: 0x004228B0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(ClickyNotes.Report left, ClickyNotes.Report right)
      {
        return !(left == right);
      }

      // Token: 0x06002978 RID: 10616 RVA: 0x004228BC File Offset: 0x004228BC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(ClickyNotes.Report left, ClickyNotes.Report right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06002979 RID: 10617 RVA: 0x004228D0 File Offset: 0x004228D0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Slug>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Verb>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField);
      }

      // Token: 0x0600297A RID: 10618 RVA: 0x0042294C File Offset: 0x0042294C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as ClickyNotes.Report);
      }

      // Token: 0x0600297B RID: 10619 RVA: 0x0042295C File Offset: 0x0042295C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(ClickyNotes.Report other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Slug>k__BackingField, other.<Slug>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Verb>k__BackingField, other.<Verb>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField));
      }

      // Token: 0x0600297D RID: 10621 RVA: 0x004229F0 File Offset: 0x004229F0
      [CompilerGenerated]
      private Report(ClickyNotes.Report original)
      {
        this.Slug = original.<Slug>k__BackingField;
        this.Name = original.<Name>k__BackingField;
        this.Verb = original.<Verb>k__BackingField;
        this.At = original.<At>k__BackingField;
      }

      // Token: 0x0600297E RID: 10622 RVA: 0x00422A28 File Offset: 0x00422A28
      [CompilerGenerated]
      public void Deconstruct(out string Slug, out string Name, out string Verb, out DateTimeOffset At)
      {
        Slug = this.Slug;
        Name = this.Name;
        Verb = this.Verb;
        At = this.At;
      }
    }
  }
}
