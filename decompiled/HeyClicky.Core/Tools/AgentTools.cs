using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Documents;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009B RID: 155
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class AgentTools
  {
    // Token: 0x14000015 RID: 21
    // (add) Token: 0x060009AD RID: 2477 RVA: 0x0036E57C File Offset: 0x0036E57C
    // (remove) Token: 0x060009AE RID: 2478 RVA: 0x0036E5B4 File Offset: 0x0036E5B4
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> LaunchedByBrain;

    // Token: 0x060009AF RID: 2479 RVA: 0x0036E5EC File Offset: 0x0036E5EC
    public AgentTools(IAgentToolHost host)
    {
      this.host = host;
    }

    // Token: 0x17000231 RID: 561
    // (get) Token: 0x060009B0 RID: 2480 RVA: 0x0036E5FC File Offset: 0x0036E5FC
    public IAgentToolHost Host
    {
      get
      {
        return this.host;
      }
    }

    // Token: 0x060009B1 RID: 2481 RVA: 0x0036E604 File Offset: 0x0036E604
    public Task<string> RunAsync(string toolName, ToolArgs args, CompanionToolContext context, CancellationToken ct = default(CancellationToken))
    {
      if (toolName != null)
      {
        switch (toolName.Length)
        {
        case 8:
          if (toolName == "routines")
          {
            return AgentTools.RoutinesAsync(args);
          }
          break;
        case 9:
          if (toolName == "find_jobs")
          {
            return this.host.FindJobsAsync(args.Str(new string[] { "query" }) ?? "", args.Int("limit").GetValueOrDefault(5));
          }
          break;
        case 11:
          if (toolName == "show_result")
          {
            Func<string, string> showResult = AgentToolHooks.ShowResult;
            string text;
            if ((text = ((showResult != null) ? showResult(args.Json.ToJsonString(null)) : null)) == null)
            {
              JsonObject jsonObject = new JsonObject(null);
              jsonObject["status"] = "error";
              jsonObject["message"] = "No job came back just now.";
              text = ToolResults.Json(jsonObject);
            }
            return Task.FromResult<string>(text);
          }
          break;
        case 12:
          if (toolName == "save_drawing")
          {
            return AgentTools.SaveDrawingAsync(args);
          }
          break;
        case 13:
        {
          char c = toolName[0];
          if (c != 'c')
          {
            if (c != 'o')
            {
              if (c == 's')
              {
                if (toolName == "sessions_send")
                {
                  return this.SendAsync(args, context, ct);
                }
              }
            }
            else if (toolName == "open_artifact")
            {
              return Task.FromResult<string>(this.ArtifactAction(args, false));
            }
          }
          else if (toolName == "clicky_memory")
          {
            return this.MemoryAsync(args, ct);
          }
          break;
        }
        case 14:
        {
          char c = toolName[9];
          if (c != 's')
          {
            if (c != 't')
            {
              if (c == 'y')
              {
                if (toolName == "sessions_yield")
                {
                  return this.YieldAsync(args, ct);
                }
              }
            }
            else if (toolName == "session_status")
            {
              return this.StatusAsync(ct);
            }
          }
          else if (toolName == "sessions_spawn")
          {
            return this.GuardedSpawnAsync(args, context, ct);
          }
          break;
        }
        case 15:
          if (toolName == "reveal_artifact")
          {
            return Task.FromResult<string>(this.ArtifactAction(args, true));
          }
          break;
        case 16:
          if (toolName == "clicky_artifacts")
          {
            return Task.Run<string>(() => this.Artifacts(args), ct);
          }
          break;
        case 18:
          if (toolName == "sessions_interrupt")
          {
            return this.InterruptAsync(args);
          }
          break;
        case 20:
          if (toolName == "open_agent_dashboard")
          {
            return Task.FromResult<string>(this.Dashboard(args));
          }
          break;
        case 23:
          if (toolName == "sessions_answer_request")
          {
            return this.AnswerRequestAsync(args, ct);
          }
          break;
        case 25:
          if (toolName == "skip_onboarding_interview")
          {
            return Task.FromResult<string>(this.SkipInterview());
          }
          break;
        }
      }
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["error"] = "not_implemented";
      jsonObject2["tool"] = toolName;
      return Task.FromResult<string>(ToolResults.Json(jsonObject2));
    }

    // Token: 0x060009B2 RID: 2482 RVA: 0x0036EA28 File Offset: 0x0036EA28
    private static async Task<string> RoutinesAsync(ToolArgs args)
    {
      Func<string, Task<string>> routines = AgentToolHooks.Routines;
      string text;
      if (routines == null)
      {
        text = ToolResults.Status("error", "Routines aren't available right now.");
      }
      else
      {
        try
        {
          text = await routines(args.Json.ToJsonString(null)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Warn("Tools", "routines: " + ex.Message);
          text = ToolResults.Status("error", "The routine couldn't be changed just now.");
        }
      }
      return text;
    }

    // Token: 0x060009B3 RID: 2483 RVA: 0x0036EA6C File Offset: 0x0036EA6C
    private static async Task<string> SaveDrawingAsync(ToolArgs args)
    {
      Func<string, Task<string>> saveDrawing = AgentToolHooks.SaveDrawing;
      string text;
      if (saveDrawing == null)
      {
        text = ToolResults.Status("failed", "The drawing couldn't be saved.");
      }
      else
      {
        try
        {
          text = await saveDrawing(args.Str(new string[] { "drawing" })).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Warn("Tools", "save_drawing: " + ex.Message);
          text = ToolResults.Status("failed", "The drawing couldn't be saved.");
        }
      }
      return text;
    }

    // Token: 0x060009B4 RID: 2484 RVA: 0x0036EAB0 File Offset: 0x0036EAB0
    private async Task<string> GuardedSpawnAsync(ToolArgs args, CompanionToolContext context, CancellationToken ct)
    {
      string spawnTask = args.Str(new string[] { "task" }) ?? "";
      string text = SpawnRepeatGuard.TryClaim(spawnTask);
      string text2;
      if (text != null)
      {
        text2 = text;
      }
      else
      {
        string text3 = await this.SpawnAsync(args, context, ct).ConfigureAwait(false);
        SpawnRepeatGuard.Settle(spawnTask, text3);
        text2 = text3;
      }
      return text2;
    }

    // Token: 0x060009B5 RID: 2485 RVA: 0x0036EB0C File Offset: 0x0036EB0C
    private async Task<string> SpawnAsync(ToolArgs args, CompanionToolContext context, CancellationToken ct)
    {
      string task = args.Str(new string[] { "task" });
      string text;
      if (task == null)
      {
        text = "{\"status\":\"error\",\"message\":\"missing task\"}";
      }
      else
      {
        string text2 = await this.host.HandleSuggestionActionAsync(task).ConfigureAwait(false);
        if (text2 != null)
        {
          text = text2;
        }
        else
        {
          Func<bool> isTourInterviewFoundingOnly = AgentToolHooks.IsTourInterviewFoundingOnly;
          if (isTourInterviewFoundingOnly != null && isTourInterviewFoundingOnly())
          {
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["status"] = "not_now";
            jsonObject["message"] = "Nothing starts during the welcome tour's interview. Keep the conversation going; their first jobs come after it.";
            text = jsonObject.ToJsonString(null);
          }
          else
          {
            string label = args.Str(new string[] { "label" }) ?? "";
            bool valueOrDefault = args.Bool("include_screenshot").GetValueOrDefault(true);
            string text3;
            if ((text3 = SpawnRepeatGuard.RosterSlug(args.Str(new string[] { "clicky_slug" }), delegate(string slug)
            {
              AgentClickyInfo agentClickyInfo6 = this.host.FindClicky(slug);
              return agentClickyInfo6 != null && !agentClickyInfo6.IsArchived;
            })) == null)
            {
              text3 = args.Str(new string[] { "clicky_slug" });
            }
            string requestedSlug = text3;
            if (AgentToolHooks.JobsEnabled)
            {
              if (requestedSlug != null && requestedSlug.Length > 0)
              {
                string text4 = requestedSlug;
                AgentClickyInfo agentClickyInfo = this.host.FindClicky(text4);
                if (agentClickyInfo != null && !agentClickyInfo.IsArchived)
                {
                  goto IL_02BD;
                }
              }
              return await this.SpawnJobAsync(task, label, valueOrDefault, context, ct, args.Str(new string[] { "chrome" }), SessionsSendContext.BuildsOnJobIds(args.Json)).ConfigureAwait(false);
            }
            IL_02BD:
            JsonObject newClicky = args.Object("new_clicky");
            string newName = null;
            string text5 = null;
            string text6 = null;
            bool flag = false;
            if (newClicky != null)
            {
              ToolArgs toolArgs = new ToolArgs(newClicky.ToJsonString(null));
              newName = toolArgs.Str(new string[] { "name" });
              text5 = toolArgs.Str(new string[] { "role" });
              text6 = toolArgs.Str(new string[] { "description", "one_liner", "oneLiner" });
              flag = toolArgs.Bool("start_idle").GetValueOrDefault();
            }
            AgentClickyInfo existing = ((requestedSlug == null) ? null : this.host.FindClicky(requestedSlug));
            if (existing != null && existing.IsArchived)
            {
              existing = null;
            }
            if (existing == null && requestedSlug == null && newName == null)
            {
              ValueTuple<string, string> valueTuple = FallbackClickyIdentity.For((label.Length > 0) ? label : task);
              newName = valueTuple.Item1;
              text5 = valueTuple.Item2;
              text5 = ((text5 == "Agent") ? null : text5);
            }
            if (existing == null && newName != null && !flag)
            {
              string wanted = FallbackClickyIdentity.NormalizedName(newName);
              existing = this.host.Clickys.FirstOrDefault<AgentClickyInfo>((AgentClickyInfo c) => !c.IsArchived && FallbackClickyIdentity.NormalizedName(c.Name) == wanted);
            }
            if (existing == null && newName != null && flag)
            {
              AgentClickyInfo agentClickyInfo2 = this.host.CreateClicky(newName, string.IsNullOrWhiteSpace(text5) ? "Helper" : text5, text6 ?? AgentTools.OneLinerFrom(task));
              JsonObject jsonObject2 = new JsonObject(null);
              jsonObject2["status"] = "founded";
              jsonObject2["clicky_slug"] = agentClickyInfo2.Slug;
              jsonObject2["clicky_name"] = agentClickyInfo2.Name;
              jsonObject2["founded_new_clicky"] = true;
              jsonObject2["message"] = "\"" + agentClickyInfo2.Name + "\" is made. It has NOT started anything: it waits for the user to tell it what to do. Say one short sentence introducing it by name and what it is for, and that it's ready for its first message. Never say it is working, starting, or on it.";
              text = ToolResults.Json(jsonObject2);
            }
            else
            {
              HashSet<string> knownSlugs = this.host.Clickys.Select<AgentClickyInfo, string>((AgentClickyInfo c) => c.Slug).ToHashSet<string>();
              string text7 = AgentTools.HandoffContext(context, task, valueOrDefault);
              string text8 = task;
              AgentClickyInfo agentClickyInfo3 = existing;
              string text9 = ((agentClickyInfo3 != null) ? agentClickyInfo3.Slug : null);
              string text10 = ((existing == null) ? newName : null);
              string text11 = (string.IsNullOrWhiteSpace(text5) ? "Helper" : text5);
              string text12 = text6;
              string text13 = ((text7 == null) ? "The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it." : ("The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it.\n\n" + text7));
              string text14 = (context.Typed ? "text" : "voice");
              AgentToolLaunch agentToolLaunch = new AgentToolLaunch(text8, text9, text10, text11, text12, text13, text14, await AgentTools.LaunchImagesAsync(context, valueOrDefault).ConfigureAwait(false), (context.AttachedFilePaths.Count > 0) ? context.AttachedFilePaths : null, "companion", false, null, AgentTools.NullIfEmpty(SessionsSendContext.BuildsOnJobIds(args.Json)));
              text8 = null;
              text9 = null;
              text10 = null;
              text11 = null;
              text12 = null;
              text13 = null;
              text14 = null;
              string text15;
              try
              {
                text15 = await this.host.LaunchAsync(agentToolLaunch, ct).ConfigureAwait(false);
              }
              catch (Exception ex) when (!(ex is OperationCanceledException))
              {
                JsonObject jsonObject3 = new JsonObject(null);
                jsonObject3["status"] = "blocked";
                jsonObject3["label"] = label;
                jsonObject3["message"] = "Couldn't start the agent: " + ex.Message + ". Tell the user in one short line; don't say it is on it.";
                return ToolResults.Json(jsonObject3);
              }
              Action<string> launchedByBrain = this.LaunchedByBrain;
              if (launchedByBrain != null)
              {
                launchedByBrain(text15);
              }
              object obj2 = await this.WaitForClickyAsync(text15, (existing == null && newName == null) ? 4000 : 1500, ct).ConfigureAwait(false);
              string text16;
              if ((text16 = ((obj2 != null) ? obj2.ClickySlug : null)) == null)
              {
                AgentClickyInfo agentClickyInfo4 = existing;
                text16 = ((agentClickyInfo4 != null) ? agentClickyInfo4.Slug : null);
              }
              string text17 = text16;
              string text18;
              if ((text18 = ((obj2 != null) ? obj2.ClickyName : null)) == null)
              {
                AgentClickyInfo agentClickyInfo5 = existing;
                text18 = ((agentClickyInfo5 != null) ? agentClickyInfo5.Name : null) ?? newName;
              }
              string text19 = text18;
              bool flag2 = text17 != null && !knownSlugs.Contains(text17);
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["status"] = "dispatched";
              jsonObject4["label"] = label;
              JsonObject jsonObject5 = jsonObject4;
              if (text17 != null)
              {
                jsonObject5["clicky_slug"] = text17;
              }
              if (text19 != null)
              {
                jsonObject5["clicky_name"] = text19;
              }
              if (flag2)
              {
                jsonObject5["founded_new_clicky"] = true;
              }
              if (newClicky == null && requestedSlug == null && text19 != null)
              {
                jsonObject5["message"] = (flag2 ? ("HeyClicky founded a new Clicky named \"" + text19 + "\" for this task and it is on it now. Mention it by name in one short sentence (it lives in the user's Home; it works when messaged and remembers).") : ("HeyClicky routed this task to the user's existing Clicky \"" + text19 + "\" — it is on it now. Say so by name in one short sentence."));
              }
              text = ToolResults.Json(jsonObject5);
            }
          }
        }
      }
      return text;
    }

    // Token: 0x060009B6 RID: 2486 RVA: 0x0036EB68 File Offset: 0x0036EB68
    private async Task<string> SpawnJobAsync(string task, string label, bool includeScreenshot, CompanionToolContext context, CancellationToken ct, [Nullable(2)] string chrome = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> buildsOnJobIds = null)
    {
      Func<string, string, Task<string>> startWaitingJobNamed = AgentToolHooks.StartWaitingJobNamed;
      bool flag = startWaitingJobNamed != null;
      string text;
      if (flag)
      {
        text = await startWaitingJobNamed(label, task).ConfigureAwait(false);
        flag = text != null;
      }
      string text2;
      if (flag)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["status"] = "dispatched";
        jsonObject["label"] = label;
        jsonObject["job_title"] = text;
        text2 = ToolResults.Json(jsonObject);
      }
      else
      {
        string text3 = AgentTools.HandoffContext(context, task, includeScreenshot);
        string text4 = task;
        string text5 = ((label.Trim().Length > 0) ? label.Trim() : null);
        string text6 = ((text3 == null) ? "The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it." : ("The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it.\n\n" + text3));
        string text7 = (context.Typed ? "text" : "voice");
        AgentToolLaunch agentToolLaunch = new AgentToolLaunch(text4, null, text5, "Helper", null, text6, text7, await AgentTools.LaunchImagesAsync(context, includeScreenshot).ConfigureAwait(false), (context.AttachedFilePaths.Count > 0) ? context.AttachedFilePaths : null, "companion", true, (chrome == "without") ? "without" : null, AgentTools.NullIfEmpty(buildsOnJobIds));
        text4 = null;
        text5 = null;
        text6 = null;
        text7 = null;
        if (chrome == "ask")
        {
          Func<AgentToolLaunch, string> holdJobForChrome = AgentToolHooks.HoldJobForChrome;
          string text8 = ((holdJobForChrome != null) ? holdJobForChrome(agentToolLaunch) : null);
          if (text8 != null)
          {
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["status"] = "waiting_on_chrome";
            jsonObject2["label"] = label;
            jsonObject2["job_title"] = text8;
            jsonObject2["message"] = "Not started yet: it waits on the dock for their Chrome choice and starts by itself once Chrome is connected, or right away without it if they pass. If your line hasn't asked yet, ask now in one short line whether you should set up their Chrome.";
            return ToolResults.Json(jsonObject2);
          }
        }
        string taskId;
        try
        {
          taskId = await this.host.LaunchAsync(agentToolLaunch, ct).ConfigureAwait(false);
        }
        catch (Exception ex) when (!(ex is OperationCanceledException))
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["status"] = "blocked";
          jsonObject3["label"] = label;
          jsonObject3["message"] = "Couldn't start the job: " + ex.Message + ". Tell the user in one short line; don't say it is on it.";
          return ToolResults.Json(jsonObject3);
        }
        Action<string> launchedByBrain = this.LaunchedByBrain;
        if (launchedByBrain != null)
        {
          launchedByBrain(taskId);
        }
        string text9 = "job_action";
        string text10 = null;
        string text11 = null;
        JsonObject jsonObject4 = new JsonObject(null);
        jsonObject4["action"] = "started";
        ClickyAnalytics.TrackProductEvent(text9, text10, text11, jsonObject4);
        AgentTaskInfo agentTaskInfo = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.TaskId == taskId);
        string text12 = ((agentTaskInfo != null) ? agentTaskInfo.Title : null) ?? label;
        JsonObject jsonObject5 = new JsonObject(null);
        jsonObject5["status"] = "dispatched";
        jsonObject5["label"] = label;
        jsonObject5["job_title"] = text12;
        text2 = ToolResults.Json(jsonObject5);
      }
      return text2;
    }

    // Token: 0x060009B7 RID: 2487 RVA: 0x0036EBE8 File Offset: 0x0036EBE8
    [return: Nullable(new byte[] { 1, 2 })]
    private async Task<AgentTaskInfo> WaitForClickyAsync(string taskId, int milliseconds, CancellationToken ct)
    {
      long deadline = Environment.TickCount64 + (long)milliseconds;
      Func<AgentTaskInfo, bool> <>9__0;
      AgentTaskInfo agentTaskInfo;
      for (;;)
      {
        IEnumerable<AgentTaskInfo> tasks = this.host.Tasks;
        Func<AgentTaskInfo, bool> func;
        if ((func = <>9__0) == null)
        {
          func = (<>9__0 = (AgentTaskInfo t) => t.TaskId == taskId);
        }
        agentTaskInfo = tasks.FirstOrDefault<AgentTaskInfo>(func);
        if (((agentTaskInfo != null) ? agentTaskInfo.ClickySlug : null) != null || Environment.TickCount64 > deadline)
        {
          break;
        }
        await Task.Delay(100, ct).ConfigureAwait(false);
      }
      return agentTaskInfo;
    }

    // Token: 0x060009B8 RID: 2488 RVA: 0x0036EC44 File Offset: 0x0036EC44
    [return: Nullable(new byte[] { 2, 1 })]
    private static IReadOnlyList<string> NullIfEmpty([Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> ids)
    {
      if (ids == null || ids.Count <= 0)
      {
        return null;
      }
      return ids;
    }

    // Token: 0x060009B9 RID: 2489 RVA: 0x0036EC58 File Offset: 0x0036EC58
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private static Task<IReadOnlyList<byte[]>> LaunchImagesAsync(CompanionToolContext context, bool includeScreenshot)
    {
      return AgentTools.LaunchImagesAsync(context, includeScreenshot, context.AttachedFilePaths);
    }

    // Token: 0x060009BA RID: 2490 RVA: 0x0036EC68 File Offset: 0x0036EC68
    [return: Nullable(new byte[] { 1, 2, 1 })]
    private static async Task<IReadOnlyList<byte[]>> LaunchImagesAsync(CompanionToolContext context, bool includeScreenshot, IReadOnlyList<string> attachedFilePaths)
    {
      List<byte[]> images = new List<byte[]>();
      if (includeScreenshot)
      {
        byte[] screenshotJpeg = context.ScreenshotJpeg;
        if (screenshotJpeg != null && screenshotJpeg.Length > 0)
        {
          images.Add(screenshotJpeg);
        }
      }
      IEnumerable<string> enumerable = Attachments.NormalizedPaths(attachedFilePaths);
      Func<string, bool> func;
      if ((func = AgentTools.<>O.<0>__IsImage) == null)
      {
        func = (AgentTools.<>O.<0>__IsImage = new Func<string, bool>(Attachments.IsImage));
      }
      foreach (string text in enumerable.Where<string>(func).Take<string>(4))
      {
        byte[] array = await Attachments.JpegAsync(text, 0.85, 4096).ConfigureAwait(false);
        if (array != null)
        {
          images.Add(array);
        }
      }
      IEnumerator<string> enumerator = null;
      return (images.Count > 0) ? images : null;
    }

    // Token: 0x060009BB RID: 2491 RVA: 0x0036ECBC File Offset: 0x0036ECBC
    [return: Nullable(2)]
    internal static string HandoffContext(CompanionToolContext context, string task, bool includeScreenshot)
    {
      List<string> list = new List<string>();
      string text = AgentTools.HandoffBlock(context.UserWords, task);
      if (text != null)
      {
        list.Add(text);
      }
      if (includeScreenshot)
      {
        string highlightGesture = context.HighlightGesture;
        if (highlightGesture != null && highlightGesture.Length > 0)
        {
          list.Add("The user highlighted part of the attached screenshot before handing this off — focus on what they marked. " + highlightGesture);
        }
      }
      PageReference page = context.Page;
      if (page != null)
      {
        list.Add(page.AgentPromptContextLine);
      }
      if (list.Count != 0)
      {
        return string.Join("\n\n", list);
      }
      return null;
    }

    // Token: 0x060009BC RID: 2492 RVA: 0x0036ED38 File Offset: 0x0036ED38
    [NullableContext(2)]
    internal static string HandoffBlock(string rawUserRequest, [Nullable(1)] string inferredTask)
    {
      string text = inferredTask.Trim();
      string text2 = ((rawUserRequest != null) ? rawUserRequest.Trim() : null);
      if (text.Length == 0 || string.IsNullOrEmpty(text2))
      {
        return null;
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(364, 7);
      defaultInterpolatedStringHandler.AppendLiteral("");
      defaultInterpolatedStringHandler.AppendFormatted("<AGENT_HANDOFF>");
      defaultInterpolatedStringHandler.AppendLiteral("\n");
      defaultInterpolatedStringHandler.AppendFormatted("Original user request (typed/STT text):");
      defaultInterpolatedStringHandler.AppendLiteral("\n");
      defaultInterpolatedStringHandler.AppendFormatted(text2);
      defaultInterpolatedStringHandler.AppendLiteral("\n\n");
      defaultInterpolatedStringHandler.AppendFormatted("HeyClicky's brief:");
      defaultInterpolatedStringHandler.AppendLiteral("\n");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral("\n\n");
      defaultInterpolatedStringHandler.AppendFormatted("How to read this:");
      defaultInterpolatedStringHandler.AppendLiteral(" the brief is HeyClicky's: do it. Where it disagrees with the user's own words, their words win. On a small ambiguity, take the likeliest reading and go; ask only when acting could hit the wrong account or project or do something risky. Answer in your reply, and make a file only when they asked for one or the work is itself a file (a site, an app, code).\n");
      defaultInterpolatedStringHandler.AppendFormatted("</AGENT_HANDOFF>");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x060009BD RID: 2493 RVA: 0x0036EE20 File Offset: 0x0036EE20
    [return: Nullable(2)]
    public static string HandOffArguments(string payload)
    {
      ToolArgs toolArgs = new ToolArgs(payload);
      string text = toolArgs.Str(new string[] { "task" });
      if (text == null)
      {
        return null;
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["task"] = text;
      jsonObject["label"] = toolArgs.Str(new string[] { "label" }) ?? "";
      jsonObject["include_screenshot"] = true;
      JsonObject jsonObject2 = jsonObject;
      string text2 = toolArgs.Str(new string[] { "clicky", "clicky_slug" });
      if (text2 != null)
      {
        jsonObject2["clicky_slug"] = text2;
      }
      else
      {
        JsonObject jsonObject3 = toolArgs.Object("newClicky") ?? toolArgs.Object("new_clicky");
        if (jsonObject3 != null)
        {
          ToolArgs toolArgs2 = new ToolArgs(jsonObject3.ToJsonString(null));
          string text3 = toolArgs2.Str(new string[] { "name" });
          if (text3 != null)
          {
            JsonNode jsonNode = jsonObject2;
            string text4 = "new_clicky";
            JsonObject jsonObject4 = new JsonObject(null);
            jsonObject4["name"] = text3;
            jsonObject4["role"] = toolArgs2.Exact("role") ?? "";
            jsonObject4["description"] = toolArgs2.Exact("description") ?? "";
            jsonNode[text4] = jsonObject4;
          }
        }
      }
      return jsonObject2.ToJsonString(null);
    }

    // Token: 0x060009BE RID: 2494 RVA: 0x0036EFB4 File Offset: 0x0036EFB4
    private static string OneLinerFrom(string task)
    {
      string text = string.Join(" ", task.Split(null, StringSplitOptions.RemoveEmptyEntries));
      if (text.Length <= 120)
      {
        return text;
      }
      return text.Substring(0, 120).TrimEnd() + "…";
    }

    // Token: 0x060009BF RID: 2495 RVA: 0x0036EFF8 File Offset: 0x0036EFF8
    private async Task<string> SendAsync(ToolArgs args, CompanionToolContext context, CancellationToken ct)
    {
      string text = args.Str(new string[] { "message" });
      string text2;
      if (text == null)
      {
        text2 = "{\"status\":\"error\",\"message\":\"missing message\"}";
      }
      else
      {
        string userWords = context.UserWords;
        string usersOwnWords = ((userWords != null) ? userWords.Trim() : null) ?? "";
        bool flag = SessionsSendContext.IsHeyClickysOwnInstruction(args.Str(new string[] { "from" }), text);
        bool forwardsUsersOwnWords = usersOwnWords.Length > 0 && !flag;
        string messageForAgent = (forwardsUsersOwnWords ? usersOwnWords : text);
        IReadOnlyList<string> readOnlyList;
        if (usersOwnWords.Length != 0)
        {
          readOnlyList = context.AttachedFilePaths;
        }
        else
        {
          IReadOnlyList<string> readOnlyList2 = Array.Empty<string>();
          readOnlyList = readOnlyList2;
        }
        IReadOnlyList<string> filesForAgent = readOnlyList;
        string launchContext = (forwardsUsersOwnWords ? SessionsSendContext.UsersOwnWordsLaunchContext(usersOwnWords, text, context.Typed) : ((usersOwnWords.Length > 0) ? SessionsSendContext.HeyClickyInstructionLaunchContext(usersOwnWords, context.Typed) : "HeyClicky is passing the user's message on to this work (sessions_send)."));
        IReadOnlyList<string> buildsOn = SessionsSendContext.BuildsOnJobIds(args.Json);
        IReadOnlyList<byte[]> readOnlyList3 = await AgentTools.LaunchImagesAsync(context, args.Bool("include_screenshot").GetValueOrDefault(true), filesForAgent).ConfigureAwait(false);
        string text3 = args.Str(new string[] { "clicky_slug" });
        string threadId = args.Str(new string[] { "codexThreadId", "childSessionId" });
        AgentClickyInfo agentClickyInfo;
        if (text3 != null)
        {
          string text4 = SpawnRepeatGuard.RosterSlug(text3, delegate(string s)
          {
            AgentClickyInfo agentClickyInfo4 = this.host.FindClicky(s);
            return agentClickyInfo4 != null && !agentClickyInfo4.IsArchived;
          });
          agentClickyInfo = ((text4 != null) ? this.host.FindClicky(text4) : null);
        }
        else
        {
          agentClickyInfo = null;
        }
        AgentClickyInfo clicky = agentClickyInfo;
        if (text3 != null && (clicky == null || clicky.IsArchived))
        {
          text2 = ToolResults.Status("error", "Unknown clicky_slug — check the [Your Clickys] note for valid slugs.");
        }
        else if (clicky == null && threadId == null)
        {
          text2 = ToolResults.Status("error", "missing clicky_slug or codexThreadId — address a persistent Clicky by clicky_slug; for one-off agents call session_status first and pass its codexThreadId.");
        }
        else
        {
          try
          {
            string taskId;
            if (clicky != null)
            {
              taskId = await this.host.LaunchAsync(new AgentToolLaunch(messageForAgent, clicky.Slug, null, "Helper", null, launchContext, context.Typed ? "text" : "voice", readOnlyList3, (filesForAgent.Count > 0) ? filesForAgent : null, "companion", false, null, AgentTools.NullIfEmpty(buildsOn)), ct).ConfigureAwait(false);
            }
            else
            {
              AgentTaskInfo agentTaskInfo = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ThreadId == threadId);
              taskId = ((agentTaskInfo != null) ? agentTaskInfo.TaskId : null) ?? this.host.OpenThread(threadId);
              string text5 = AgentTools.SteerText(messageForAgent, launchContext, forwardsUsersOwnWords, usersOwnWords.Length > 0, filesForAgent, (buildsOn.Count > 0) ? this.host.BuildsOnContext(buildsOn, threadId) : null);
              await this.host.SteerAsync(taskId, text5, readOnlyList3, ct).ConfigureAwait(false);
            }
            Action<string> launchedByBrain = this.LaunchedByBrain;
            if (launchedByBrain != null)
            {
              launchedByBrain(taskId);
            }
            AgentTaskInfo agentTaskInfo2 = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.TaskId == taskId);
            string text6;
            if ((text6 = ((agentTaskInfo2 != null) ? agentTaskInfo2.ThreadId : null)) == null)
            {
              AgentClickyInfo agentClickyInfo2 = clicky;
              if ((text6 = ((agentClickyInfo2 != null) ? agentClickyInfo2.ThreadId : null)) == null)
              {
                text6 = threadId ?? "";
              }
            }
            string text7 = text6;
            if (clicky == null && threadId != null)
            {
              string text8 = args.Str(new string[] { "label" });
              if (text8 != null && text8.Length > 0)
              {
                this.host.RetitleJob(threadId, text8);
              }
            }
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["status"] = "sent";
            jsonObject["codexThreadId"] = text7;
            jsonObject["sent_as"] = (forwardsUsersOwnWords ? "the user's own words, unchanged" : "your message");
            text2 = ToolResults.Json(jsonObject);
          }
          catch (Exception ex) when (!(ex is OperationCanceledException))
          {
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["status"] = "blocked";
            string text9 = "codexThreadId";
            string text10;
            if ((text10 = threadId) == null)
            {
              AgentClickyInfo agentClickyInfo3 = clicky;
              text10 = ((agentClickyInfo3 != null) ? agentClickyInfo3.ThreadId : null) ?? "";
            }
            jsonObject2[text9] = text10;
            jsonObject2["message"] = "Couldn't send the follow-up: " + ex.Message;
            text2 = ToolResults.Json(jsonObject2);
          }
        }
      }
      return text2;
    }

    // Token: 0x060009C0 RID: 2496 RVA: 0x0036F054 File Offset: 0x0036F054
    internal static string SteerText(string messageForAgent, string launchContext, bool forwardsUsersOwnWords, bool hadUsersWords, IReadOnlyList<string> filesForAgent, [Nullable(2)] string buildsOnContext)
    {
      List<string> list = new List<string> { messageForAgent };
      if (forwardsUsersOwnWords)
      {
        int num = launchContext.IndexOf("(their words win where the two differ): ", StringComparison.Ordinal);
        if (num >= 0)
        {
          List<string> list2 = list;
          string text = "HeyClicky's note on what this refers to and what was agreed with them (their words win where the two differ): ";
          int num2 = num + "(their words win where the two differ): ".Length;
          list2.Add(text + launchContext.Substring(num2, launchContext.Length - num2).Trim());
        }
      }
      else if (hadUsersWords)
      {
        list.Add(launchContext);
      }
      if (buildsOnContext != null && buildsOnContext.Length > 0)
      {
        list.Add(buildsOnContext);
      }
      if (filesForAgent.Count > 0)
      {
        list.Add("Files the user attached:\n" + string.Join("\n", filesForAgent.Select<string, string>((string path) => "- " + path)));
      }
      return string.Join("\n\n", list);
    }

    // Token: 0x060009C1 RID: 2497 RVA: 0x0036F12C File Offset: 0x0036F12C
    private async Task<string> InterruptAsync(ToolArgs args)
    {
      string threadId = args.Str(new string[] { "codexThreadId", "childSessionId" });
      string slug = args.Str(new string[] { "clicky_slug" });
      string text;
      if (threadId == null && slug == null)
      {
        text = ToolResults.Status("error", "missing codexThreadId — call session_status first to get the agent's codexThreadId, then pass it here.");
      }
      else
      {
        AgentTaskInfo target = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => (threadId != null && t.ThreadId == threadId) || (slug != null && t.ClickySlug == slug));
        if (target == null || (!target.IsWorking && target.Status != AgentTaskStatus.WaitingForUser))
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["status"] = "not_found";
          string text2 = "codexThreadId";
          string text3;
          if ((text3 = threadId) == null)
          {
            AgentTaskInfo agentTaskInfo = target;
            text3 = ((agentTaskInfo != null) ? agentTaskInfo.ThreadId : null) ?? "";
          }
          jsonObject[text2] = text3;
          text = ToolResults.Json(jsonObject);
        }
        else
        {
          await this.host.StopAsync(target.TaskId).ConfigureAwait(false);
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["status"] = "stopped";
          string text4 = "codexThreadId";
          string text5;
          if ((text5 = target.ThreadId) == null)
          {
            text5 = threadId ?? "";
          }
          jsonObject2[text4] = text5;
          text = ToolResults.Json(jsonObject2);
        }
      }
      return text;
    }

    // Token: 0x060009C2 RID: 2498 RVA: 0x0036F178 File Offset: 0x0036F178
    private async Task<string> YieldAsync(ToolArgs args, CancellationToken ct)
    {
      string threadId = args.Str(new string[] { "codexThreadId", "childSessionId" });
      string text;
      if (threadId == null)
      {
        text = await this.StatusAsync(ct).ConfigureAwait(false);
      }
      else
      {
        AgentTaskInfo agentTaskInfo = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ThreadId == threadId);
        if (agentTaskInfo != null)
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["codexThreadId"] = threadId;
          jsonObject["title"] = agentTaskInfo.Title;
          jsonObject["state"] = AgentTools.StateText(agentTaskInfo.Status);
          jsonObject["summary"] = (string.IsNullOrWhiteSpace(agentTaskInfo.Summary) ? agentTaskInfo.ProgressSummary : agentTaskInfo.Summary);
          jsonObject["status"] = "ok";
          text = ToolResults.Json(jsonObject);
        }
        else
        {
          AgentThreadInfo agentThreadInfo = (await this.SafeHistoryAsync(50, ct).ConfigureAwait(false)).FirstOrDefault<AgentThreadInfo>((AgentThreadInfo h) => h.ThreadId == threadId);
          if (agentThreadInfo != null)
          {
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["codexThreadId"] = threadId;
            jsonObject2["title"] = agentThreadInfo.Title ?? "Agent task";
            jsonObject2["state"] = agentThreadInfo.Status;
            jsonObject2["summary"] = agentThreadInfo.Preview;
            jsonObject2["status"] = "ok";
            text = ToolResults.Json(jsonObject2);
          }
          else
          {
            JsonObject jsonObject3 = new JsonObject(null);
            jsonObject3["status"] = "not_found";
            jsonObject3["codexThreadId"] = threadId;
            text = ToolResults.Json(jsonObject3);
          }
        }
      }
      return text;
    }

    // Token: 0x060009C3 RID: 2499 RVA: 0x0036F1CC File Offset: 0x0036F1CC
    private async Task<string> StatusAsync(CancellationToken ct)
    {
      AgentTools.<>c__DisplayClass32_0 CS$<>8__locals1 = new AgentTools.<>c__DisplayClass32_0();
      Dictionary<string, List<AgentTools.ArtifactCandidate>> artifactsByThread = (from a in this.ArtifactCandidates()
        group a by a.ThreadId ?? "").ToDictionary<IGrouping<string, AgentTools.ArtifactCandidate>, string, List<AgentTools.ArtifactCandidate>>((IGrouping<string, AgentTools.ArtifactCandidate> g) => g.Key, (IGrouping<string, AgentTools.ArtifactCandidate> g) => g.ToList<AgentTools.ArtifactCandidate>());
      IReadOnlyList<AgentTaskInfo> tasks = this.host.Tasks;
      JsonArray active = new JsonArray(null);
      foreach (AgentTaskInfo agentTaskInfo in tasks)
      {
        JsonArray jsonArray = active;
        string threadId = agentTaskInfo.ThreadId;
        string clickySlug = agentTaskInfo.ClickySlug;
        string text = agentTaskInfo.ClickyName ?? agentTaskInfo.Title;
        string text2 = agentTaskInfo.UserPrompts.LastOrDefault<string>() ?? "";
        string text3 = AgentTools.StateText(agentTaskInfo.Status);
        string text4 = (string.IsNullOrWhiteSpace(agentTaskInfo.Summary) ? agentTaskInfo.ProgressSummary : agentTaskInfo.Summary);
        string threadId2 = agentTaskInfo.ThreadId;
        List<AgentTools.ArtifactCandidate> list;
        jsonArray.Add<JsonObject>(AgentTools.Entry(threadId, clickySlug, text, text2, text3, text4, (threadId2 != null && artifactsByThread.TryGetValue(threadId2, out list)) ? list : new List<AgentTools.ArtifactCandidate>()));
      }
      CS$<>8__locals1.activeThreads = (from t in tasks
        select t.ThreadId into t
        where t != null
        select t).ToHashSet<string>();
      JsonArray previous = new JsonArray(null);
      IEnumerable<AgentThreadInfo> enumerable = await this.SafeHistoryAsync(25, ct).ConfigureAwait(false);
      Func<AgentThreadInfo, bool> func;
      if ((func = CS$<>8__locals1.<>9__5) == null)
      {
        AgentTools.<>c__DisplayClass32_0 CS$<>8__locals2 = CS$<>8__locals1;
        Func<AgentThreadInfo, bool> func2 = (AgentThreadInfo h) => !CS$<>8__locals1.activeThreads.Contains(h.ThreadId);
        CS$<>8__locals2.<>9__5 = func2;
        func = func2;
      }
      foreach (AgentThreadInfo agentThreadInfo in enumerable.Where<AgentThreadInfo>(func).OrderByDescending<AgentThreadInfo, DateTimeOffset>(delegate(AgentThreadInfo h)
      {
        DateTimeOffset? updatedAt = h.UpdatedAt;
        if (updatedAt == null)
        {
          return DateTimeOffset.MinValue;
        }
        return updatedAt.GetValueOrDefault();
      }).Take<AgentThreadInfo>(10))
      {
        List<AgentTools.ArtifactCandidate> list2;
        previous.Add<JsonObject>(AgentTools.Entry(agentThreadInfo.ThreadId, agentThreadInfo.ClickySlug, agentThreadInfo.Title ?? "Agent task", agentThreadInfo.Preview, agentThreadInfo.Status, agentThreadInfo.Preview, artifactsByThread.TryGetValue(agentThreadInfo.ThreadId, out list2) ? list2 : new List<AgentTools.ArtifactCandidate>()));
      }
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "ok";
      jsonObject["activeCount"] = active.Count;
      jsonObject["previousCount"] = previous.Count;
      jsonObject["activeAgents"] = active;
      jsonObject["previousAgents"] = previous;
      jsonObject["note"] = ((active.Count == 0 && previous.Count == 0) ? "No agents have run yet this session." : "These are the agents you've started. To open a listed artifact, call open_artifact with its codexThreadId or uri.");
      return ToolResults.Json(jsonObject);
    }

    // Token: 0x060009C4 RID: 2500 RVA: 0x0036F218 File Offset: 0x0036F218
    private static JsonObject Entry([Nullable(2)] string threadId, [Nullable(2)] string slug, string title, string prompt, string state, string summary, List<AgentTools.ArtifactCandidate> artifacts)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["title"] = title;
      jsonObject["prompt"] = prompt;
      jsonObject["state"] = state;
      jsonObject["summary"] = summary;
      JsonObject jsonObject2 = jsonObject;
      if (threadId != null)
      {
        jsonObject2["codexThreadId"] = threadId;
      }
      if (slug != null)
      {
        jsonObject2["clicky_slug"] = slug;
        jsonObject2["is_persistent_clicky"] = true;
      }
      if (artifacts.Count > 0)
      {
        jsonObject2["artifactCount"] = artifacts.Count;
        jsonObject2["artifacts"] = new JsonArray(artifacts.Take<AgentTools.ArtifactCandidate>(6).Select<AgentTools.ArtifactCandidate, JsonNode>(delegate(AgentTools.ArtifactCandidate a)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["title"] = a.Title;
          jsonObject3["uri"] = a.Uri;
          jsonObject3["kind"] = a.Kind;
          return jsonObject3;
        }).ToArray<JsonNode>());
      }
      return jsonObject2;
    }

    // Token: 0x060009C5 RID: 2501 RVA: 0x0036F318 File Offset: 0x0036F318
    private static string StateText(AgentTaskStatus status)
    {
      string text;
      switch (status)
      {
      case AgentTaskStatus.Starting:
        text = "queued";
        break;
      case AgentTaskStatus.Running:
        text = "running";
        break;
      case AgentTaskStatus.WaitingForUser:
        text = "waiting_for_user";
        break;
      case AgentTaskStatus.Finished:
        text = "completed";
        break;
      case AgentTaskStatus.Failed:
        text = "failed";
        break;
      default:
        text = "interrupted";
        break;
      }
      return text;
    }

    // Token: 0x060009C6 RID: 2502 RVA: 0x0036F370 File Offset: 0x0036F370
    private async Task<IReadOnlyList<AgentThreadInfo>> SafeHistoryAsync(int limit, CancellationToken ct)
    {
      IReadOnlyList<AgentThreadInfo> readOnlyList;
      try
      {
        readOnlyList = await this.host.HistoryAsync(limit, ct).WaitAsync(TimeSpan.FromSeconds(4L), ct).ConfigureAwait(false);
      }
      catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
      {
        Log.Warn("Tools", "agent history: " + ex.Message);
        readOnlyList = Array.Empty<AgentThreadInfo>();
      }
      return readOnlyList;
    }

    // Token: 0x060009C7 RID: 2503 RVA: 0x0036F3C4 File Offset: 0x0036F3C4
    private async Task<string> AnswerRequestAsync(ToolArgs args, CancellationToken ct)
    {
      string text = args.Str(new string[] { "request" });
      string text2 = ((text != null) ? text.Trim().ToLowerInvariant() : null) ?? "";
      string text3 = args.Str(new string[] { "decision" });
      string text4 = ((text3 != null) ? text3.Trim().ToLowerInvariant() : null) ?? "";
      string slug = args.Str(new string[] { "clicky_slug" });
      string threadId = args.Str(new string[] { "codexThreadId" });
      string text5;
      if (text2 == "chrome_setup")
      {
        text5 = await AgentTools.AnswerChromeSetupAsync(text4, threadId).ConfigureAwait(false);
      }
      else if (!(text2 == "extra_usage") && !(text2 == "computer_use"))
      {
        text5 = ToolResults.Status("error", "request must be extra_usage or computer_use");
      }
      else if (!(text4 == "approve") && !(text4 == "decline"))
      {
        text5 = ToolResults.Status("error", "decision must be approve or decline");
      }
      else if (text2 == "computer_use")
      {
        List<AgentTaskInfo> list = this.host.Tasks.Where<AgentTaskInfo>((AgentTaskInfo t) => !t.IsDemo && t.PendingComputerUseRequest != null).ToList<AgentTaskInfo>();
        AgentTaskInfo agentTaskInfo = ((slug != null) ? list.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ClickySlug == slug) : ((threadId != null) ? list.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ThreadId == threadId) : ((list.Count == 1) ? list[0] : null)));
        if (agentTaskInfo == null)
        {
          IEnumerable<string> enumerable = from n in list.Select<AgentTaskInfo, string>(delegate(AgentTaskInfo t)
            {
              if (t.ClickySlug != null)
              {
                return (t.ClickyName ?? t.Title) + " (clicky_slug: " + t.ClickySlug + ")";
              }
              if (t.ThreadId == null)
              {
                return null;
              }
              return t.Title + " (codexThreadId: " + t.ThreadId + ")";
            })
            where n != null
            select n;
          text5 = ToolResults.Status("error", (list.Count == 0) ? "No Clicky is waiting on a computer_use answer right now — nothing to approve or decline; tell the user so." : ("Several are waiting on a computer_use answer; pass the clicky_slug or codexThreadId of the one the user means: " + string.Join(", ", enumerable) + "."));
        }
        else
        {
          string text6 = agentTaskInfo.ClickyName ?? agentTaskInfo.Title;
          if (text4 == "approve")
          {
            this.host.ApproveComputerUse(agentTaskInfo.TaskId);
            JsonObject jsonObject = new JsonObject(null);
            jsonObject["status"] = "approved";
            jsonObject["clicky_name"] = text6;
            string text7 = "message";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(113, 2);
            defaultInterpolatedStringHandler.AppendLiteral("HeyClicky is driving the PC now for ");
            defaultInterpolatedStringHandler.AppendFormatted(text6);
            defaultInterpolatedStringHandler.AppendLiteral("'s step while the user watches; ");
            defaultInterpolatedStringHandler.AppendFormatted(text6);
            defaultInterpolatedStringHandler.AppendLiteral(" carries on after. Confirm in one short line.");
            jsonObject[text7] = defaultInterpolatedStringHandler.ToStringAndClear();
            text5 = ToolResults.Json(jsonObject);
          }
          else
          {
            this.host.DeclineComputerUse(agentTaskInfo.TaskId);
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["status"] = "declined";
            jsonObject2["clicky_name"] = text6;
            jsonObject2["message"] = "Declined: " + text6 + " will finish without controlling the PC. Confirm in one short line.";
            text5 = ToolResults.Json(jsonObject2);
          }
        }
      }
      else
      {
        List<AgentTaskInfo> list2 = this.host.Tasks.Where<AgentTaskInfo>((AgentTaskInfo t) => !t.IsDemo && t.RequiresExtraUsageApproval).ToList<AgentTaskInfo>();
        AgentTaskInfo agentTaskInfo2 = ((slug != null) ? list2.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ClickySlug == slug) : ((threadId != null) ? list2.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ThreadId == threadId) : ((list2.Count == 1) ? list2[0] : null)));
        if (agentTaskInfo2 == null)
        {
          IEnumerable<string> enumerable2 = from n in list2.Select<AgentTaskInfo, string>(delegate(AgentTaskInfo t)
            {
              if (t.ClickySlug != null)
              {
                return (t.ClickyName ?? t.Title) + " (clicky_slug: " + t.ClickySlug + ")";
              }
              if (t.ThreadId == null)
              {
                return null;
              }
              return t.Title + " (codexThreadId: " + t.ThreadId + ")";
            })
            where n != null
            select n;
          string text8 = "error";
          string text9;
          if (list2.Count != 0)
          {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(100, 2);
            defaultInterpolatedStringHandler.AppendLiteral("Several are waiting on a ");
            defaultInterpolatedStringHandler.AppendFormatted(text2);
            defaultInterpolatedStringHandler.AppendLiteral(" answer; pass the clicky_slug or codexThreadId of the one the user means: ");
            defaultInterpolatedStringHandler.AppendFormatted(string.Join(", ", enumerable2));
            defaultInterpolatedStringHandler.AppendLiteral(".");
            text9 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text9 = "No Clicky is waiting on a " + text2 + " answer right now — nothing to approve or decline; tell the user so.";
          }
          text5 = ToolResults.Status(text8, text9);
        }
        else
        {
          string name = agentTaskInfo2.ClickyName ?? agentTaskInfo2.Title;
          if (text4 == "decline")
          {
            JsonObject jsonObject3 = new JsonObject(null);
            jsonObject3["status"] = "left_paused";
            jsonObject3["clicky_name"] = name;
            jsonObject3["message"] = name + " stays paused; the Approve button stays on its card if the user changes their mind. Confirm in one short line.";
            text5 = ToolResults.Json(jsonObject3);
          }
          else
          {
            try
            {
              await this.host.ApproveExtraUsageAsync(agentTaskInfo2.TaskId, true, ct).ConfigureAwait(false);
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
              return ToolResults.Status("error", "The approval didn't go through (" + ex.Message + "). Tell the user in one short line.");
            }
            JsonObject jsonObject4 = new JsonObject(null);
            jsonObject4["status"] = "approved";
            jsonObject4["clicky_name"] = name;
            jsonObject4["message"] = "Approved: " + name + " is resuming and will run to completion. Confirm in one short line.";
            text5 = ToolResults.Json(jsonObject4);
          }
        }
      }
      return text5;
    }

    // Token: 0x060009C8 RID: 2504 RVA: 0x0036F418 File Offset: 0x0036F418
    private static async Task<string> AnswerChromeSetupAsync(string decision, [Nullable(2)] string threadId)
    {
      string text;
      if (!(decision == "approve"))
      {
        if (!(decision == "show_how"))
        {
          if (!(decision == "decline"))
          {
            text = null;
          }
          else
          {
            text = "not_now";
          }
        }
        else
        {
          text = "show_me_how";
        }
      }
      else
      {
        text = "do_it_for_me";
      }
      string text2 = text;
      string text3;
      if (text2 == null)
      {
        text3 = ToolResults.Status("error", "decision must be approve, show_how or decline");
      }
      else
      {
        Func<string, string, Task<string>> answerChromeSetup = AgentToolHooks.AnswerChromeSetup;
        if (answerChromeSetup != null)
        {
          text = await answerChromeSetup(string.IsNullOrWhiteSpace(threadId) ? null : threadId.Trim(), text2).ConfigureAwait(false);
        }
        else
        {
          text = null;
        }
        string text4 = text;
        if (text4 == null)
        {
          text3 = ToolResults.Status("error", "No job is waiting on a Chrome choice right now; nothing to answer. Tell the user so.");
        }
        else
        {
          Func<string> chromeSetupBrowserName = AgentToolHooks.ChromeSetupBrowserName;
          string text5 = ((chromeSetupBrowserName != null) ? chromeSetupBrowserName() : null) ?? "Chrome";
          if (!(text4 == "do_it_for_me"))
          {
            if (!(text4 == "show_me_how"))
            {
              text = "The job goes on without the Chrome extension and does the website part on their screen instead. Confirm in one short line, mentioning the extension makes this quicker next time.";
            }
            else
            {
              text = "HeyClicky opens its Chrome Web Store page in " + text5 + ", and you'll be asked to show them each click in a moment. Say one short line, like \"Here we go.\"";
            }
          }
          else
          {
            text = "HeyClicky is removing the old copy of its " + text5 + " extension on their screen now, then adding the new version; the job starts or carries on by itself once it's connected. Confirm in one short line.";
          }
          string text6 = text;
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["status"] = text4;
          jsonObject["message"] = text6;
          text3 = ToolResults.Json(jsonObject);
        }
      }
      return text3;
    }

    // Token: 0x060009C9 RID: 2505 RVA: 0x0036F464 File Offset: 0x0036F464
    private string SlugForJobToolArguments(ToolArgs args, string threadId)
    {
      string text = args.Str(new string[] { "clicky_slug" });
      string text2 = ((text != null) ? text.Trim() : null) ?? "";
      if (text2.Length > 0 || threadId.Length == 0 || this.host.FindJob(threadId) != null)
      {
        return text2;
      }
      return this.host.ClickySlugForThread(threadId) ?? "";
    }

    // Token: 0x060009CA RID: 2506 RVA: 0x0036F4D4 File Offset: 0x0036F4D4
    private static string Tail(string id)
    {
      if (id.Length <= 8)
      {
        return id;
      }
      int length = id.Length;
      int num = length - 8;
      return id.Substring(num, length - num);
    }

    // Token: 0x060009CB RID: 2507 RVA: 0x0036F504 File Offset: 0x0036F504
    private async Task<string> MemoryAsync(ToolArgs args, CancellationToken ct)
    {
      string text = args.Str(new string[] { "codexThreadId" });
      string requestedThreadId = ((text != null) ? text.Trim() : null) ?? "";
      string text2 = this.SlugForJobToolArguments(args, requestedThreadId);
      string text4;
      if (text2.Length == 0 && requestedThreadId.Length > 0)
      {
        JsonObject jsonObject = await this.JobResultPayloadAsync(requestedThreadId, ct).ConfigureAwait(false);
        string text3 = "Tools";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 2);
        defaultInterpolatedStringHandler.AppendLiteral("clicky_memory job thread=");
        defaultInterpolatedStringHandler.AppendFormatted(AgentTools.Tail(requestedThreadId));
        defaultInterpolatedStringHandler.AppendLiteral(" status=");
        defaultInterpolatedStringHandler.AppendFormatted<JsonNode>(jsonObject["status"]);
        Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
        text4 = ToolResults.Json(jsonObject);
      }
      else
      {
        AgentClickyInfo clicky = ((text2.Length == 0) ? null : this.host.FindClicky(text2));
        if (clicky == null)
        {
          JsonObject jsonObject2 = new JsonObject(null);
          jsonObject2["status"] = "unknown_clicky";
          jsonObject2["message"] = "No Clicky with that clicky_slug — use a slug from the [Your Clickys] note.";
          jsonObject2["known_slugs"] = new JsonArray(this.host.Clickys.Select<AgentClickyInfo, JsonNode>((AgentClickyInfo c) => c.Slug).ToArray<JsonNode>());
          text4 = ToolResults.Json(jsonObject2);
        }
        else
        {
          string memory = "";
          string text5 = this.host.WorkspacePath(clicky.Slug);
          if (text5 != null)
          {
            try
            {
              memory = File.ReadAllText(Path.Combine(text5, "AGENTS.md"));
            }
            catch
            {
            }
          }
          if (memory.Length > 6000)
          {
            memory = memory.Substring(0, 6000) + "\n…(memory file truncated)";
          }
          AgentTaskInfo live = this.host.Tasks.FirstOrDefault<AgentTaskInfo>((AgentTaskInfo t) => t.ClickySlug == clicky.Slug);
          AgentTaskInfo agentTaskInfo = live;
          bool working = agentTaskInfo != null && agentTaskInfo.IsWorking;
          string text6;
          if ((text6 = clicky.ThreadId) == null)
          {
            AgentTaskInfo agentTaskInfo2 = live;
            text6 = ((agentTaskInfo2 != null) ? agentTaskInfo2.ThreadId : null);
          }
          string threadId = text6;
          IReadOnlyList<ValueTuple<string, string>> finished = Array.Empty<ValueTuple<string, string>>();
          if (threadId != null)
          {
            try
            {
              finished = await this.host.RecentMessagesAsync(threadId, 20, ct).WaitAsync(TimeSpan.FromSeconds(4L), ct).ConfigureAwait(false);
            }
            catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
            {
              Log.Warn("Tools", "clicky_memory thread: " + ex.Message);
            }
          }
          string text7;
          if (live == null || string.IsNullOrWhiteSpace(live.Summary))
          {
            text7 = finished.LastOrDefault<ValueTuple<string, string>>(([TupleElementNames(new string[] { "Role", "Text" })] ValueTuple<string, string> m) => m.Item1 == "clicky").Item2 ?? "";
          }
          else
          {
            text7 = live.Summary;
          }
          string text8 = text7;
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["status"] = "ok";
          jsonObject3["clicky_slug"] = clicky.Slug;
          jsonObject3["clicky_name"] = clicky.Name;
          jsonObject3["role"] = clicky.RoleText;
          jsonObject3["description"] = clicky.OneLiner;
          jsonObject3["is_archived"] = clicky.IsArchived;
          jsonObject3["is_working_now"] = working;
          jsonObject3["has_conversation"] = threadId != null;
          jsonObject3["memory_markdown"] = ((memory.Length == 0) ? "(no memory file yet — this Clicky hasn't worked yet)" : memory);
          JsonObject jsonObject4 = jsonObject3;
          if (working)
          {
            jsonObject4["current_progress"] = (string.IsNullOrWhiteSpace(live.ProgressSummary) ? "working" : live.ProgressSummary.Trim());
          }
          if (text8.Length > 0)
          {
            jsonObject4["latest_reply"] = ((text8.Length > 2000) ? text8.Substring(0, 2000) : text8);
          }
          List<ValueTuple<string, string>> list = AgentTools.RecentMessages(finished, live, 20);
          if (list.Count > 0)
          {
            jsonObject4["recent_messages"] = new JsonArray(list.Select<ValueTuple<string, string>, JsonNode>(delegate([TupleElementNames(new string[] { "Role", "Text" })] ValueTuple<string, string> m)
            {
              JsonObject jsonObject5 = new JsonObject(null);
              jsonObject5["role"] = m.Item1;
              jsonObject5["text"] = m.Item2;
              return jsonObject5;
            }).ToArray<JsonNode>());
            JsonNode jsonNode = jsonObject4;
            string text9 = "recent_messages_note";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(185, 1);
            defaultInterpolatedStringHandler.AppendLiteral("The last ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
            defaultInterpolatedStringHandler.AppendLiteral(" messages of this Clicky's chat, oldest first. role user = what the user sent it; role clicky = its replies; role clicky_progress = a live progress text from the run in flight.");
            jsonNode[text9] = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          text4 = ToolResults.Json(jsonObject4);
        }
      }
      return text4;
    }

    // Token: 0x060009CC RID: 2508 RVA: 0x0036F558 File Offset: 0x0036F558
    private async Task<JsonObject> JobResultPayloadAsync(string threadId, CancellationToken ct)
    {
      AgentJobInfo job = this.host.FindJob(threadId);
      JsonObject jsonObject;
      if (job == null)
      {
        jsonObject = (JsonObject)JsonNode.Parse("{\"status\":\"unknown_job\",\"message\":\"No job with that codexThreadId: use one from the [Your jobs] note.\"}", null, default(JsonDocumentOptions));
      }
      else
      {
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["status"] = "ok";
        jsonObject2["codexThreadId"] = job.ThreadId ?? threadId;
        jsonObject2["job_title"] = job.Title;
        jsonObject2["state"] = job.State;
        jsonObject2["folder"] = job.FolderPath;
        JsonObject payload = jsonObject2;
        JsonArray jsonArray = new JsonArray(null);
        foreach (AgentJobRunInfo agentJobRunInfo in job.Runs)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["asked"] = ((agentJobRunInfo.Ask.Length > 400) ? agentJobRunInfo.Ask.Substring(0, 400) : agentJobRunInfo.Ask);
          jsonObject3["started"] = agentJobRunInfo.StartedAt.ToLocalTime().ToString("MMM d, HH:mm", CultureInfo.InvariantCulture);
          jsonObject3["outcome"] = agentJobRunInfo.Outcome;
          JsonObject jsonObject4 = jsonObject3;
          if (!string.IsNullOrEmpty(agentJobRunInfo.Summary))
          {
            jsonObject4["summary"] = ((agentJobRunInfo.Summary.Length > 300) ? agentJobRunInfo.Summary.Substring(0, 300) : agentJobRunInfo.Summary);
          }
          jsonArray.Add<JsonObject>(jsonObject4);
        }
        if (jsonArray.Count > 0)
        {
          payload["runs"] = jsonArray;
        }
        AgentTaskInfo live = job.Live;
        if (live != null && live.IsWorking)
        {
          payload["current_progress"] = (string.IsNullOrWhiteSpace(live.ProgressSummary) ? "working" : live.ProgressSummary.Trim());
        }
        string text = job.LatestReply.Trim();
        if (text.Length > 0)
        {
          payload["latest_reply"] = ((text.Length > 12000) ? (text.Substring(0, 12000) + "\n…(cut)") : text);
        }
        else if (!string.IsNullOrEmpty(job.Summary))
        {
          payload["latest_reply"] = job.Summary;
        }
        IReadOnlyList<ValueTuple<string, string>> finished = Array.Empty<ValueTuple<string, string>>();
        string threadId2 = job.ThreadId;
        if (threadId2 != null)
        {
          try
          {
            IReadOnlyList<ValueTuple<string, string>> readOnlyList = await this.host.RecentMessagesAsync(threadId2, 12, ct).WaitAsync(TimeSpan.FromSeconds(4L), ct).ConfigureAwait(false);
            finished = readOnlyList;
          }
          catch (Exception ex) when (!(ex is OperationCanceledException) || !ct.IsCancellationRequested)
          {
            Log.Warn("Tools", "clicky_memory job thread: " + ex.Message);
          }
        }
        List<ValueTuple<string, string>> list = AgentTools.RecentMessages(finished, job.Live, 12);
        if (list.Count > 0)
        {
          payload["recent_messages"] = new JsonArray(list.Select<ValueTuple<string, string>, JsonNode>(delegate([TupleElementNames(new string[] { "Role", "Text" })] ValueTuple<string, string> m)
          {
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["role"] = m.Item1;
            jsonObject5["text"] = m.Item2;
            return jsonObject5;
          }).ToArray<JsonNode>());
          JsonNode jsonNode = payload;
          string text2 = "recent_messages_note";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(164, 1);
          defaultInterpolatedStringHandler.AppendLiteral("The last ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
          defaultInterpolatedStringHandler.AppendLiteral(" messages of this job's newest conversation, oldest first: role user = what the user sent it, clicky = its replies, clicky_progress = a live progress line.");
          jsonNode[text2] = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        payload["note"] = "latest_reply is what the chat shows: speak to it in your own short words, never read it out. Its files: clicky_artifacts with this codexThreadId. A change goes to the job with sessions_send.";
        jsonObject = payload;
      }
      return jsonObject;
    }

    // Token: 0x060009CD RID: 2509 RVA: 0x0036F5AC File Offset: 0x0036F5AC
    [NullableContext(2)]
    [return: TupleElementNames(new string[] { "Role", "Text" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    internal static List<ValueTuple<string, string>> RecentMessages([TupleElementNames(new string[] { "Role", "Text" })] [Nullable(new byte[] { 1, 0, 1, 1 })] IReadOnlyList<ValueTuple<string, string>> finished, AgentTaskInfo live, int limit)
    {
      List<ValueTuple<string, string>> list = (from m in finished
        where !string.IsNullOrWhiteSpace(m.Item2)
        select new ValueTuple<string, string>(m.Item1, AgentTools.<RecentMessages>g__Capped|43_0(m.Item2))).ToList<ValueTuple<string, string>>();
      if (live != null && live.IsWorking)
      {
        string prompt = live.UserPrompts.LastOrDefault<string>();
        if (prompt != null && prompt.Length > 0 && !list.Any<ValueTuple<string, string>>(([TupleElementNames(new string[] { "Role", null })] ValueTuple<string, string> m) => m.Item1 == "user" && m.Item2 == AgentTools.<RecentMessages>g__Capped|43_0(prompt)))
        {
          list.Add(new ValueTuple<string, string>("user", AgentTools.<RecentMessages>g__Capped|43_0(prompt)));
        }
        foreach (string text in live.Commentary.Where<string>((string c) => !string.IsNullOrWhiteSpace(c)))
        {
          list.Add(new ValueTuple<string, string>("clicky_progress", AgentTools.<RecentMessages>g__Capped|43_0(text)));
        }
        if (!string.IsNullOrWhiteSpace(live.StreamingResponse))
        {
          list.Add(new ValueTuple<string, string>("clicky", AgentTools.<RecentMessages>g__Capped|43_0(live.StreamingResponse) + " (still writing)"));
        }
      }
      return list.TakeLast<ValueTuple<string, string>>(limit).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x060009CE RID: 2510 RVA: 0x0036F728 File Offset: 0x0036F728
    private string Artifacts(ToolArgs args)
    {
      string text = args.Str(new string[] { "codexThreadId" });
      string text2 = ((text != null) ? text.Trim() : null) ?? "";
      string text3 = this.SlugForJobToolArguments(args, text2);
      if (text3.Length == 0 && text2.Length > 0)
      {
        AgentJobInfo agentJobInfo = this.host.FindJob(text2);
        if (agentJobInfo == null)
        {
          return "{\"status\":\"unknown_job\",\"message\":\"No job with that codexThreadId: use one from the [Your jobs] note.\"}";
        }
        string jobFolder = agentJobInfo.FolderPath;
        IEnumerable<string> artifactEntries = agentJobInfo.ArtifactEntries;
        AgentTaskInfo live = agentJobInfo.Live;
        List<string> list = (from p in artifactEntries.Concat<string>(((live != null) ? live.Artifacts : null) ?? Array.Empty<string>())
          select this.AbsoluteArtifactPath(p, jobFolder)).ToList<string>();
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["codexThreadId"] = agentJobInfo.ThreadId ?? "";
        jsonObject["job_title"] = agentJobInfo.Title;
        JsonObject jsonObject2 = AgentTools.ArtifactsPayload(jsonObject, "\"" + agentJobInfo.Title + "\"", AgentTools.CollectArtifacts(list, Path.Combine(jobFolder, "output")), args.Str(new string[] { "file" }));
        string text4 = "Tools";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
        defaultInterpolatedStringHandler.AppendLiteral("clicky_artifacts job thread=");
        defaultInterpolatedStringHandler.AppendFormatted(AgentTools.Tail(text2));
        defaultInterpolatedStringHandler.AppendLiteral(" status=");
        defaultInterpolatedStringHandler.AppendFormatted<JsonNode>(jsonObject2["status"]);
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        return ToolResults.Json(jsonObject2);
      }
      else
      {
        AgentClickyInfo clicky = ((text3.Length == 0) ? null : this.host.FindClicky(text3));
        if (clicky == null)
        {
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["status"] = "unknown_clicky";
          jsonObject3["message"] = "No Clicky with that clicky_slug: use a slug from the [Your Clickys] note.";
          jsonObject3["known_slugs"] = new JsonArray(this.host.Clickys.Select<AgentClickyInfo, JsonNode>((AgentClickyInfo c) => c.Slug).ToArray<JsonNode>());
          return ToolResults.Json(jsonObject3);
        }
        string workspace = this.host.WorkspacePath(clicky.Slug);
        List<AgentTools.Artifact> list2 = AgentTools.CollectArtifacts((from p in this.host.Tasks.Where<AgentTaskInfo>((AgentTaskInfo t) => t.ClickySlug == clicky.Slug).SelectMany<AgentTaskInfo, string>((AgentTaskInfo t) => t.Artifacts)
          select this.AbsoluteArtifactPath(p, workspace)).ToList<string>(), (workspace == null) ? null : Path.Combine(workspace, "output"));
        JsonObject jsonObject4 = new JsonObject(null);
        jsonObject4["clicky_slug"] = clicky.Slug;
        jsonObject4["clicky_name"] = clicky.Name;
        return ToolResults.Json(AgentTools.ArtifactsPayload(jsonObject4, clicky.Name, list2, args.Str(new string[] { "file" })));
      }
    }

    // Token: 0x060009CF RID: 2511 RVA: 0x0036FA9C File Offset: 0x0036FA9C
    private static JsonObject ArtifactsPayload(JsonObject owner, string ownerName, List<AgentTools.Artifact> artifacts, [Nullable(2)] string requestedFile)
    {
      owner["status"] = "ok";
      if (artifacts.Count == 0)
      {
        owner["files"] = new JsonArray(null);
        owner["message"] = ownerName + " hasn't made any files yet. Its answers are in its replies (clicky_memory).";
        return owner;
      }
      string text = ((requestedFile != null) ? requestedFile.Trim() : null);
      if (text == null || text.Length <= 0)
      {
        JsonArray jsonArray = new JsonArray(null);
        int num = 0;
        foreach (AgentTools.Artifact artifact in artifacts)
        {
          JsonObject jsonObject = AgentTools.Summary(artifact);
          if (!artifact.IsLink && num < 3)
          {
            ValueTuple<string, bool>? valueTuple = AgentTools.ReadText(artifact.Path, 1200);
            if (valueTuple != null)
            {
              ValueTuple<string, bool> valueOrDefault = valueTuple.GetValueOrDefault();
              num++;
              jsonObject["opening_text"] = valueOrDefault.Item1;
              if (valueOrDefault.Item2)
              {
                jsonObject["more_text"] = true;
              }
            }
          }
          jsonArray.Add<JsonObject>(jsonObject);
        }
        owner["files"] = jsonArray;
        owner["note"] = "Newest first. Pass file (a name or path) to read one in full; open one with open_artifact (path); copy its text with copy_to_clipboard.";
        return owner;
      }
      AgentTools.Artifact artifact2 = AgentTools.BestMatch(text, artifacts);
      if (artifact2 == null)
      {
        owner["status"] = "not_found";
        string text2 = "message";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 2);
        defaultInterpolatedStringHandler.AppendLiteral("No file of ");
        defaultInterpolatedStringHandler.AppendFormatted(ownerName);
        defaultInterpolatedStringHandler.AppendLiteral("'s matches \"");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral("\".");
        owner[text2] = defaultInterpolatedStringHandler.ToStringAndClear();
        owner["files"] = new JsonArray(artifacts.Select<AgentTools.Artifact, JsonNode>((AgentTools.Artifact a) => AgentTools.Summary(a)).ToArray<JsonNode>());
        return owner;
      }
      JsonObject jsonObject2 = AgentTools.Summary(artifact2);
      if (!artifact2.IsLink)
      {
        ValueTuple<string, bool>? valueTuple = AgentTools.ReadText(artifact2.Path, 12000);
        if (valueTuple != null)
        {
          ValueTuple<string, bool> valueOrDefault2 = valueTuple.GetValueOrDefault();
          jsonObject2["text"] = valueOrDefault2.Item1;
          if (valueOrDefault2.Item2)
          {
            jsonObject2["truncated"] = true;
          }
        }
        else
        {
          jsonObject2["text_note"] = "This kind of file has no readable text (an image, an app, a binary). Open it with open_artifact to show it.";
        }
      }
      owner["file"] = jsonObject2;
      return owner;
    }

    // Token: 0x060009D0 RID: 2512 RVA: 0x0036FD58 File Offset: 0x0036FD58
    private static List<AgentTools.Artifact> CollectArtifacts(IEnumerable<string> named, [Nullable(2)] string outputFolder)
    {
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<AgentTools.Artifact> list = new List<AgentTools.Artifact>();
      List<AgentTools.Artifact> list2 = new List<AgentTools.Artifact>();
      List<string> list3 = new List<string>();
      if (outputFolder != null && Directory.Exists(outputFolder))
      {
        try
        {
          list3 = (from e in (from e in new DirectoryInfo(outputFolder).EnumerateFileSystemInfos("*", new EnumerationOptions
              {
                RecurseSubdirectories = true,
                MaxRecursionDepth = 2
              })
              where e is FileInfo
              orderby e.LastWriteTimeUtc descending
              select e).Take<FileSystemInfo>(12)
            select e.FullName).ToList<string>();
        }
        catch
        {
        }
      }
      foreach (string text in named.Concat<string>(list3))
      {
        string text2 = text.Trim();
        if (text2.Length != 0 && hashSet.Add(text2))
        {
          if (text2.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text2.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
          {
            list2.Add(new AgentTools.Artifact(text2, true, null, null));
          }
          else if (File.Exists(text2))
          {
            FileInfo fileInfo = new FileInfo(text2);
            list.Add(new AgentTools.Artifact(text2, false, new long?(fileInfo.Length), new DateTimeOffset?(fileInfo.LastWriteTime)));
          }
          else if (Directory.Exists(text2))
          {
            list.Add(new AgentTools.Artifact(text2, false, null, new DateTimeOffset?(new DirectoryInfo(text2).LastWriteTime)));
          }
        }
      }
      return list.OrderByDescending<AgentTools.Artifact, DateTimeOffset>(delegate(AgentTools.Artifact f)
      {
        DateTimeOffset? modified = f.Modified;
        if (modified == null)
        {
          return DateTimeOffset.MinValue;
        }
        return modified.GetValueOrDefault();
      }).Concat<AgentTools.Artifact>(list2).Take<AgentTools.Artifact>(8)
        .ToList<AgentTools.Artifact>();
    }

    // Token: 0x060009D1 RID: 2513 RVA: 0x0036FF94 File Offset: 0x0036FF94
    [return: Nullable(2)]
    private static AgentTools.Artifact BestMatch(string requested, List<AgentTools.Artifact> artifacts)
    {
      AgentTools.Artifact artifact = artifacts.FirstOrDefault<AgentTools.Artifact>((AgentTools.Artifact a) => string.Equals(a.Path, requested, StringComparison.OrdinalIgnoreCase));
      if (artifact != null)
      {
        return artifact;
      }
      string text = requested.ToLowerInvariant();
      string requestedName = Path.GetFileName(text.TrimEnd(new char[] { '\\', '/' }));
      AgentTools.Artifact artifact2 = artifacts.FirstOrDefault<AgentTools.Artifact>((AgentTools.Artifact a) => AgentTools.<BestMatch>g__Name|48_1(a) == requestedName);
      if (artifact2 != null)
      {
        return artifact2;
      }
      List<string> words = (from w in new string(text.Select<char, char>(delegate(char c)
        {
          if (!char.IsLetterOrDigit(c))
          {
            return ' ';
          }
          return c;
        }).ToArray<char>()).Split(' ', StringSplitOptions.RemoveEmptyEntries)
        where w.Length > 1
        select w).ToList<string>();
      if (words.Count == 0)
      {
        return null;
      }
      return artifacts.FirstOrDefault<AgentTools.Artifact>((AgentTools.Artifact a) => words.All<string>((string w) => AgentTools.<BestMatch>g__Name|48_1(a).Contains(w)));
    }

    // Token: 0x060009D2 RID: 2514 RVA: 0x00370094 File Offset: 0x00370094
    private static JsonObject Summary(AgentTools.Artifact artifact)
    {
      if (artifact.IsLink)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["name"] = artifact.Path;
        jsonObject["kind"] = "link";
        jsonObject["path"] = artifact.Path;
        return jsonObject;
      }
      string text = Path.GetExtension(artifact.Path).TrimStart('.').ToLowerInvariant();
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["name"] = Path.GetFileName(artifact.Path.TrimEnd(new char[] { '\\', '/' }));
      jsonObject2["kind"] = ((text.Length == 0) ? "folder or file" : text);
      jsonObject2["path"] = artifact.Path;
      JsonObject jsonObject3 = jsonObject2;
      long? bytes = artifact.Bytes;
      if (bytes != null)
      {
        long valueOrDefault = bytes.GetValueOrDefault();
        jsonObject3["size"] = Attachments.FormatBytes(valueOrDefault);
      }
      DateTimeOffset? modified = artifact.Modified;
      if (modified != null)
      {
        jsonObject3["modified"] = modified.GetValueOrDefault().ToString("yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture);
      }
      return jsonObject3;
    }

    // Token: 0x060009D3 RID: 2515 RVA: 0x003701FC File Offset: 0x003701FC
    [return: TupleElementNames(new string[] { "Text", "Truncated" })]
    [return: Nullable(new byte[] { 0, 1 })]
    private static ValueTuple<string, bool>? ReadText(string path, int limit)
    {
      PageContext pageContext = DocumentText.Extract(path, 60000);
      string text = pageContext.Text;
      string text2 = ((text != null) ? text.Trim() : null);
      if (string.IsNullOrEmpty(text2))
      {
        return null;
      }
      string text3 = Path.GetExtension(path).ToLowerInvariant();
      bool flag = text3 == ".html" || text3 == ".htm";
      if (flag)
      {
        text2 = DocumentText.TextWithoutMarkup(text2);
      }
      if (text2.Length == 0)
      {
        return null;
      }
      bool flag2 = text2.Length > limit || pageContext.IsTruncated;
      return new ValueTuple<string, bool>?(new ValueTuple<string, bool>((text2.Length > limit) ? (text2.Substring(0, limit) + "…") : text2, flag2));
    }

    // Token: 0x060009D4 RID: 2516 RVA: 0x003702C4 File Offset: 0x003702C4
    private List<AgentTools.ArtifactCandidate> ArtifactCandidates()
    {
      HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      List<AgentTools.ArtifactCandidate> list = new List<AgentTools.ArtifactCandidate>();
      foreach (AgentTaskInfo agentTaskInfo in this.host.Tasks)
      {
        string text;
        if ((text = agentTaskInfo.WorkspacePath) == null)
        {
          string clickySlug = agentTaskInfo.ClickySlug;
          text = ((clickySlug != null) ? this.host.WorkspacePath(clickySlug) : null);
        }
        string text2 = text;
        foreach (string text3 in agentTaskInfo.Artifacts)
        {
          string text4 = this.AbsoluteArtifactPath(text3, text2);
          if (text4.Length > 0 && hashSet.Add(text4))
          {
            list.Add(new AgentTools.ArtifactCandidate(agentTaskInfo.ThreadId, AgentTools.ArtifactTitle(text4), text4, AgentTools.ArtifactKind(text4), agentTaskInfo.StartedAt));
          }
        }
      }
      return list;
    }

    // Token: 0x060009D5 RID: 2517 RVA: 0x003703D4 File Offset: 0x003703D4
    private string ArtifactAction(ToolArgs args, bool reveal)
    {
      string text = (reveal ? "reveal_artifact" : "open_artifact");
      AgentTools.ArtifactCandidate artifactCandidate = this.ResolveArtifact(args);
      if (artifactCandidate == null)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["status"] = "not_found";
        jsonObject["summary"] = "No generated artifact matched that request.";
        jsonObject["toolName"] = text;
        return ToolResults.Json(jsonObject);
      }
      bool flag;
      if (artifactCandidate.Uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || artifactCandidate.Uri.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
      {
        flag = !reveal && this.host.OpenPath(artifactCandidate.Uri);
      }
      else
      {
        Uri uri;
        string text2 = ((artifactCandidate.Uri.StartsWith("file:", StringComparison.OrdinalIgnoreCase) && Uri.TryCreate(artifactCandidate.Uri, UriKind.Absolute, out uri)) ? uri.LocalPath : artifactCandidate.Uri);
        bool flag2 = File.Exists(text2) || Directory.Exists(text2);
        if (!reveal && flag2)
        {
          flag = this.host.OpenPath(text2);
        }
        else if (flag2)
        {
          flag = this.host.RevealPath(text2);
        }
        else
        {
          string text3 = AgentTools.NearestExistingFolder(text2);
          flag = text3 != null && this.host.OpenPath(text3);
        }
      }
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["title"] = artifactCandidate.Title;
      jsonObject2["uri"] = artifactCandidate.Uri;
      jsonObject2["kind"] = artifactCandidate.Kind;
      jsonObject2["opened"] = flag;
      JsonObject jsonObject3 = jsonObject2;
      if (artifactCandidate.ThreadId != null)
      {
        jsonObject3["threadId"] = artifactCandidate.ThreadId;
      }
      string text4 = (reveal ? "Revealed" : "Opened");
      JsonObject jsonObject4 = new JsonObject(null);
      jsonObject4["status"] = (flag ? "completed" : "failed");
      string text5 = "summary";
      string text6;
      if (!flag)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 2);
        defaultInterpolatedStringHandler.AppendLiteral("Could not ");
        defaultInterpolatedStringHandler.AppendFormatted(reveal ? "reveal" : "open");
        defaultInterpolatedStringHandler.AppendLiteral(" ");
        defaultInterpolatedStringHandler.AppendFormatted(artifactCandidate.Title);
        defaultInterpolatedStringHandler.AppendLiteral(".");
        text6 = defaultInterpolatedStringHandler.ToStringAndClear();
      }
      else
      {
        text6 = text4 + " " + artifactCandidate.Title + ".";
      }
      jsonObject4[text5] = text6;
      jsonObject4["toolName"] = text;
      jsonObject4["result"] = jsonObject3;
      return ToolResults.Json(jsonObject4);
    }

    // Token: 0x060009D6 RID: 2518 RVA: 0x0037068C File Offset: 0x0037068C
    [return: Nullable(2)]
    private AgentTools.ArtifactCandidate ResolveArtifact(ToolArgs args)
    {
      AgentTools.<>c__DisplayClass54_0 CS$<>8__locals1 = new AgentTools.<>c__DisplayClass54_0();
      CS$<>8__locals1.threadId = args.Str(new string[] { "codexThreadId", "threadId" });
      string text = args.Str(new string[] { "uri", "url", "path" });
      if (text != null)
      {
        string text2 = this.AbsoluteArtifactPath(text, null);
        return new AgentTools.ArtifactCandidate(CS$<>8__locals1.threadId, AgentTools.ArtifactTitle(text2), text2, AgentTools.ArtifactKind(text2), DateTimeOffset.Now);
      }
      string text3 = args.Str(new string[] { "artifactId", "id" });
      if (text3 != null && (Path.IsPathFullyQualified(text3) || text3.StartsWith('~') || text3.Contains("://", StringComparison.Ordinal)))
      {
        string text4 = this.AbsoluteArtifactPath(text3, null);
        return new AgentTools.ArtifactCandidate(CS$<>8__locals1.threadId, AgentTools.ArtifactTitle(text4), text4, AgentTools.ArtifactKind(text4), DateTimeOffset.Now);
      }
      AgentTools.<>c__DisplayClass54_0 CS$<>8__locals2 = CS$<>8__locals1;
      string text5 = args.Str(new string[] { "kind" });
      CS$<>8__locals2.kind = ((text5 != null) ? AgentTools.NormalizedKind(text5) : null);
      List<AgentTools.ArtifactCandidate> list = this.ArtifactCandidates();
      List<AgentTools.ArtifactCandidate> list2 = list.Where<AgentTools.ArtifactCandidate>((AgentTools.ArtifactCandidate c) => (CS$<>8__locals1.threadId == null || c.ThreadId == CS$<>8__locals1.threadId) && (CS$<>8__locals1.kind == null || c.Kind == CS$<>8__locals1.kind)).ToList<AgentTools.ArtifactCandidate>();
      if (list2.Count == 0 && (CS$<>8__locals1.threadId != null || CS$<>8__locals1.kind != null))
      {
        list2 = list.Where<AgentTools.ArtifactCandidate>((AgentTools.ArtifactCandidate c) => CS$<>8__locals1.threadId == null || c.ThreadId == CS$<>8__locals1.threadId).ToList<AgentTools.ArtifactCandidate>();
      }
      IEnumerable<AgentTools.ArtifactCandidate> enumerable = list2;
      Func<AgentTools.ArtifactCandidate, int> func;
      if ((func = AgentTools.<>O.<1>__OpenScore) == null)
      {
        func = (AgentTools.<>O.<1>__OpenScore = new Func<AgentTools.ArtifactCandidate, int>(AgentTools.OpenScore));
      }
      return enumerable.OrderByDescending<AgentTools.ArtifactCandidate, int>(func).ThenByDescending<AgentTools.ArtifactCandidate, DateTimeOffset>((AgentTools.ArtifactCandidate c) => c.UpdatedAt).FirstOrDefault<AgentTools.ArtifactCandidate>();
    }

    // Token: 0x060009D7 RID: 2519 RVA: 0x00370848 File Offset: 0x00370848
    private string AbsoluteArtifactPath(string raw, [Nullable(2)] string workspace)
    {
      string text = raw.Trim();
      if (text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https://", StringComparison.OrdinalIgnoreCase) || text.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
      {
        return text;
      }
      if (text.Length == 0)
      {
        return "";
      }
      string text3;
      try
      {
        string text2 = LocalRootTools.ResolvePath(text, workspace ?? this.host.ProjectsRoot);
        if (!Path.IsPathFullyQualified(text.Replace('/', '\\')) && workspace != null && !File.Exists(text2) && !Directory.Exists(text2))
        {
          text3 = LocalRootTools.ResolvePath(text, this.host.ProjectsRoot);
        }
        else
        {
          text3 = text2;
        }
      }
      catch
      {
        text3 = text;
      }
      return text3;
    }

    // Token: 0x060009D8 RID: 2520 RVA: 0x00370900 File Offset: 0x00370900
    private static string ArtifactTitle(string uri)
    {
      Uri uri2;
      bool flag = Uri.TryCreate(uri, UriKind.Absolute, out uri2);
      if (flag)
      {
        string scheme = uri2.Scheme;
        bool flag2 = scheme == "http" || scheme == "https";
        flag = flag2;
      }
      if (flag)
      {
        string text = uri2.Segments.LastOrDefault<string>();
        string text2 = ((text != null) ? text.Trim('/') : null) ?? "";
        if (text2.Length <= 0)
        {
          return uri2.Host;
        }
        return Uri.UnescapeDataString(text2);
      }
      else
      {
        string fileName = Path.GetFileName(uri.TrimEnd(new char[] { '\\', '/' }));
        if (fileName.Length != 0)
        {
          return fileName;
        }
        return uri;
      }
    }

    // Token: 0x060009D9 RID: 2521 RVA: 0x003709B0 File Offset: 0x003709B0
    private static string ArtifactKind(string uri)
    {
      if (uri.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || uri.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
      {
        return "url";
      }
      string text = Path.GetExtension(uri).TrimStart('.').ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 2:
          if (!(text == "md"))
          {
            goto IL_021B;
          }
          break;
        case 3:
        {
          char c = text[0];
          switch (c)
          {
          case 'd':
            if (!(text == "doc"))
            {
              goto IL_021B;
            }
            break;
          case 'e':
          case 'f':
          case 'i':
            goto IL_021B;
          case 'g':
            if (!(text == "gif"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          case 'h':
            if (!(text == "htm"))
            {
              goto IL_021B;
            }
            break;
          case 'j':
            if (!(text == "jpg"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          default:
            switch (c)
            {
            case 'p':
              if (!(text == "pdf"))
              {
                if (!(text == "png"))
                {
                  goto IL_021B;
                }
                goto IL_0213;
              }
              break;
            case 'q':
            case 's':
              goto IL_021B;
            case 'r':
              if (!(text == "rtf"))
              {
                goto IL_021B;
              }
              break;
            case 't':
              if (!(text == "txt"))
              {
                goto IL_021B;
              }
              break;
            default:
              goto IL_021B;
            }
            break;
          }
          break;
        }
        case 4:
        {
          char c = text[2];
          switch (c)
          {
          case 'b':
            if (!(text == "webp"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          case 'c':
            if (!(text == "docx"))
            {
              goto IL_021B;
            }
            break;
          case 'd':
          case 'g':
          case 'h':
            goto IL_021B;
          case 'e':
            if (!(text == "jpeg"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          case 'f':
            if (!(text == "tiff"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          case 'i':
            if (!(text == "heic"))
            {
              goto IL_021B;
            }
            goto IL_0213;
          default:
            if (c != 'm')
            {
              goto IL_021B;
            }
            if (!(text == "html"))
            {
              goto IL_021B;
            }
            break;
          }
          break;
        }
        case 5:
        case 6:
        case 7:
          goto IL_021B;
        case 8:
          if (!(text == "markdown"))
          {
            goto IL_021B;
          }
          break;
        default:
          goto IL_021B;
        }
        return "report";
        IL_0213:
        return "screenshot";
      }
      IL_021B:
      return "file";
    }

    // Token: 0x060009DA RID: 2522 RVA: 0x00370BE0 File Offset: 0x00370BE0
    private static string NormalizedKind(string raw)
    {
      string text = raw.Trim().ToLowerInvariant();
      if (text != null)
      {
        switch (text.Length)
        {
        case 3:
        {
          char c = text[0];
          if (c <= 'd')
          {
            if (c != 'a')
            {
              if (c != 'd')
              {
                goto IL_0196;
              }
              if (!(text == "doc"))
              {
                goto IL_0196;
              }
              goto IL_0176;
            }
            else
            {
              if (!(text == "app"))
              {
                goto IL_0196;
              }
              return "app";
            }
          }
          else if (c != 'p')
          {
            if (c != 'u')
            {
              goto IL_0196;
            }
            if (!(text == "url"))
            {
              goto IL_0196;
            }
          }
          else
          {
            if (!(text == "pdf"))
            {
              goto IL_0196;
            }
            goto IL_0176;
          }
          break;
        }
        case 4:
          if (!(text == "link"))
          {
            goto IL_0196;
          }
          break;
        case 5:
        {
          char c = text[0];
          if (c != 'i')
          {
            if (c != 'o')
            {
              goto IL_0196;
            }
            if (!(text == "other"))
            {
              goto IL_0196;
            }
            return "other";
          }
          else
          {
            if (!(text == "image"))
            {
              goto IL_0196;
            }
            goto IL_017E;
          }
          break;
        }
        case 6:
          if (!(text == "report"))
          {
            goto IL_0196;
          }
          goto IL_0176;
        case 7:
        case 9:
          goto IL_0196;
        case 8:
        {
          char c = text[0];
          if (c != 'd')
          {
            if (c != 'm')
            {
              goto IL_0196;
            }
            if (!(text == "markdown"))
            {
              goto IL_0196;
            }
            goto IL_0176;
          }
          else
          {
            if (!(text == "document"))
            {
              goto IL_0196;
            }
            goto IL_0176;
          }
          break;
        }
        case 10:
          if (!(text == "screenshot"))
          {
            goto IL_0196;
          }
          goto IL_017E;
        default:
          goto IL_0196;
        }
        return "url";
        IL_0176:
        return "report";
        IL_017E:
        return "screenshot";
      }
      IL_0196:
      return "file";
    }

    // Token: 0x060009DB RID: 2523 RVA: 0x00370D8C File Offset: 0x00370D8C
    private static int OpenScore(AgentTools.ArtifactCandidate artifact)
    {
      string text = artifact.Uri.ToLowerInvariant();
      string fileName = Path.GetFileName(text);
      string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
      string text2 = Path.GetExtension(fileName).TrimStart('.');
      string kind = artifact.Kind;
      int num;
      if (!(kind == "url"))
      {
        if (!(kind == "report") && !(kind == "screenshot"))
        {
          if (!(kind == "file"))
          {
            num = 20;
          }
          else
          {
            num = 70;
          }
        }
        else
        {
          num = 90;
        }
      }
      else
      {
        num = 180;
      }
      int num2 = num;
      if (text.Contains("127.0.0.1") || text.Contains("localhost"))
      {
        num2 += 80;
      }
      num = num2;
      int num3;
      if (!(fileNameWithoutExtension == "index"))
      {
        if (!(fileNameWithoutExtension == "main"))
        {
          if (!(fileNameWithoutExtension == "readme"))
          {
            num3 = 0;
          }
          else
          {
            num3 = 30;
          }
        }
        else
        {
          num3 = 50;
        }
      }
      else
      {
        num3 = 70;
      }
      num2 = num + num3;
      num3 = num2;
      if (text2 != null)
      {
        switch (text2.Length)
        {
        case 2:
          if (!(text2 == "md"))
          {
            goto IL_0338;
          }
          goto IL_0326;
        case 3:
        {
          char c = text2[0];
          switch (c)
          {
          case 'c':
            if (!(text2 == "csv"))
            {
              goto IL_0338;
            }
            goto IL_0332;
          case 'd':
            if (!(text2 == "doc"))
            {
              goto IL_0338;
            }
            goto IL_0326;
          case 'e':
          case 'f':
          case 'i':
            goto IL_0338;
          case 'g':
            if (!(text2 == "gif"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          case 'h':
            if (!(text2 == "htm"))
            {
              goto IL_0338;
            }
            break;
          case 'j':
            if (!(text2 == "jpg"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          default:
            switch (c)
            {
            case 'p':
              if (text2 == "pdf")
              {
                num = 42;
                goto IL_033B;
              }
              if (!(text2 == "png"))
              {
                goto IL_0338;
              }
              goto IL_032C;
            case 'q':
            case 's':
              goto IL_0338;
            case 'r':
              if (!(text2 == "rtf"))
              {
                goto IL_0338;
              }
              goto IL_0326;
            case 't':
              if (text2 == "txt")
              {
                goto IL_0326;
              }
              if (!(text2 == "tsv"))
              {
                goto IL_0338;
              }
              goto IL_0332;
            default:
              goto IL_0338;
            }
            break;
          }
          break;
        }
        case 4:
        {
          char c = text2[2];
          switch (c)
          {
          case 'b':
            if (!(text2 == "webp"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          case 'c':
            if (!(text2 == "docx"))
            {
              goto IL_0338;
            }
            goto IL_0326;
          case 'd':
          case 'g':
          case 'h':
            goto IL_0338;
          case 'e':
            if (!(text2 == "jpeg"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          case 'f':
            if (!(text2 == "tiff"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          case 'i':
            if (!(text2 == "heic"))
            {
              goto IL_0338;
            }
            goto IL_032C;
          default:
            if (c != 'm')
            {
              if (c != 's')
              {
                goto IL_0338;
              }
              if (!(text2 == "xlsx"))
              {
                goto IL_0338;
              }
              goto IL_0332;
            }
            else if (!(text2 == "html"))
            {
              goto IL_0338;
            }
            break;
          }
          break;
        }
        case 5:
        case 6:
        case 7:
          goto IL_0338;
        case 8:
          if (!(text2 == "markdown"))
          {
            goto IL_0338;
          }
          goto IL_0326;
        default:
          goto IL_0338;
        }
        num = 45;
        goto IL_033B;
        IL_0326:
        num = 36;
        goto IL_033B;
        IL_032C:
        num = 32;
        goto IL_033B;
        IL_0332:
        num = 24;
        goto IL_033B;
      }
      IL_0338:
      num = 5;
      IL_033B:
      return num3 + num;
    }

    // Token: 0x060009DC RID: 2524 RVA: 0x003710DC File Offset: 0x003710DC
    [return: Nullable(2)]
    private static string NearestExistingFolder(string path)
    {
      string text2;
      try
      {
        string text = Path.GetDirectoryName(path);
        while (!string.IsNullOrEmpty(text) && !Directory.Exists(text))
        {
          text = Path.GetDirectoryName(text);
        }
        text2 = (string.IsNullOrEmpty(text) ? null : text);
      }
      catch
      {
        text2 = null;
      }
      return text2;
    }

    // Token: 0x060009DD RID: 2525 RVA: 0x00371130 File Offset: 0x00371130
    private string Dashboard(ToolArgs args)
    {
      string text = args.Str(new string[] { "action" });
      if (((text != null) ? text.ToLowerInvariant() : null) == "close")
      {
        this.host.CloseHome();
        return "{\"status\":\"ok\",\"dashboard\":\"closed\"}";
      }
      string text2 = args.Str(new string[] { "codexThreadId" });
      this.host.OpenHome(text2);
      return "{\"status\":\"ok\",\"dashboard\":\"open\"}";
    }

    // Token: 0x060009DE RID: 2526 RVA: 0x003711A4 File Offset: 0x003711A4
    private string SkipInterview()
    {
      bool flag = this.host.SkipOnboardingInterview();
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = (flag ? "ok" : "not_in_interview");
      jsonObject["note"] = (flag ? "The interview is over. Say nothing more about it; the tour moves on by itself." : "The user is not in the onboarding interview right now, so there is nothing to skip.");
      return ToolResults.Json(jsonObject);
    }

    // Token: 0x060009E1 RID: 2529 RVA: 0x00371310 File Offset: 0x00371310
    [CompilerGenerated]
    internal static string <RecentMessages>g__Capped|43_0(string text)
    {
      string text2 = text.Trim();
      if (text2.Length <= 700)
      {
        return text2;
      }
      return text2.Substring(0, 700) + "…";
    }

    // Token: 0x060009E2 RID: 2530 RVA: 0x0037134C File Offset: 0x0037134C
    [CompilerGenerated]
    internal static string <BestMatch>g__Name|48_1(AgentTools.Artifact a)
    {
      return Path.GetFileName(a.Path.TrimEnd(new char[] { '\\', '/' })).ToLowerInvariant();
    }

    // Token: 0x0400046F RID: 1135
    public static readonly IReadOnlySet<string> Names = new HashSet<string>
    {
      "sessions_spawn", "sessions_send", "session_status", "sessions_interrupt", "sessions_yield", "sessions_answer_request", "clicky_memory", "clicky_artifacts", "open_artifact", "reveal_artifact",
      "open_agent_dashboard", "skip_onboarding_interview", "save_drawing", "show_result", "routines", "find_jobs"
    };

    // Token: 0x04000470 RID: 1136
    private const int ArtifactsListed = 8;

    // Token: 0x04000471 RID: 1137
    private const int ArtifactsExcerpted = 3;

    // Token: 0x04000472 RID: 1138
    private const int ExcerptCharacters = 1200;

    // Token: 0x04000473 RID: 1139
    private const int FullFileCharacters = 12000;

    // Token: 0x04000474 RID: 1140
    private const int DefaultFindJobsLimit = 5;

    // Token: 0x04000475 RID: 1141
    private const string SpawnContext = "The voice decided this needs background work and called sessions_spawn, so HeyClicky is starting a helper on it.";

    // Token: 0x04000476 RID: 1142
    private readonly IAgentToolHost host;

    // Token: 0x04000478 RID: 1144
    private const string UnknownJobJson = "{\"status\":\"unknown_job\",\"message\":\"No job with that codexThreadId: use one from the [Your jobs] note.\"}";

    // Token: 0x02000373 RID: 883
    [Nullable(0)]
    private sealed class Artifact : IEquatable<AgentTools.Artifact>
    {
      // Token: 0x060028D4 RID: 10452 RVA: 0x0041D64C File Offset: 0x0041D64C
      public Artifact(string Path, bool IsLink, long? Bytes, DateTimeOffset? Modified)
      {
        this.Path = Path;
        this.IsLink = IsLink;
        this.Bytes = Bytes;
        this.Modified = Modified;
        base..ctor();
      }

      // Token: 0x170007F2 RID: 2034
      // (get) Token: 0x060028D5 RID: 10453 RVA: 0x0041D674 File Offset: 0x0041D674
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentTools.Artifact);
        }
      }

      // Token: 0x170007F3 RID: 2035
      // (get) Token: 0x060028D6 RID: 10454 RVA: 0x0041D680 File Offset: 0x0041D680
      // (set) Token: 0x060028D7 RID: 10455 RVA: 0x0041D688 File Offset: 0x0041D688
      public string Path { get; set; }

      // Token: 0x170007F4 RID: 2036
      // (get) Token: 0x060028D8 RID: 10456 RVA: 0x0041D694 File Offset: 0x0041D694
      // (set) Token: 0x060028D9 RID: 10457 RVA: 0x0041D69C File Offset: 0x0041D69C
      public bool IsLink { get; set; }

      // Token: 0x170007F5 RID: 2037
      // (get) Token: 0x060028DA RID: 10458 RVA: 0x0041D6A8 File Offset: 0x0041D6A8
      // (set) Token: 0x060028DB RID: 10459 RVA: 0x0041D6B0 File Offset: 0x0041D6B0
      public long? Bytes { get; set; }

      // Token: 0x170007F6 RID: 2038
      // (get) Token: 0x060028DC RID: 10460 RVA: 0x0041D6BC File Offset: 0x0041D6BC
      // (set) Token: 0x060028DD RID: 10461 RVA: 0x0041D6C4 File Offset: 0x0041D6C4
      public DateTimeOffset? Modified { get; set; }

      // Token: 0x060028DE RID: 10462 RVA: 0x0041D6D0 File Offset: 0x0041D6D0
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Artifact");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060028DF RID: 10463 RVA: 0x0041D71C File Offset: 0x0041D71C
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Path = ");
        builder.Append(this.Path);
        builder.Append(", IsLink = ");
        builder.Append(this.IsLink.ToString());
        builder.Append(", Bytes = ");
        builder.Append(this.Bytes.ToString());
        builder.Append(", Modified = ");
        builder.Append(this.Modified.ToString());
        return true;
      }

      // Token: 0x060028E0 RID: 10464 RVA: 0x0041D7C0 File Offset: 0x0041D7C0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentTools.Artifact left, AgentTools.Artifact right)
      {
        return !(left == right);
      }

      // Token: 0x060028E1 RID: 10465 RVA: 0x0041D7CC File Offset: 0x0041D7CC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentTools.Artifact left, AgentTools.Artifact right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060028E2 RID: 10466 RVA: 0x0041D7E0 File Offset: 0x0041D7E0
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Path>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsLink>k__BackingField)) * -1521134295 + EqualityComparer<long?>.Default.GetHashCode(this.<Bytes>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<Modified>k__BackingField);
      }

      // Token: 0x060028E3 RID: 10467 RVA: 0x0041D85C File Offset: 0x0041D85C
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentTools.Artifact);
      }

      // Token: 0x060028E4 RID: 10468 RVA: 0x0041D86C File Offset: 0x0041D86C
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentTools.Artifact other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Path>k__BackingField, other.<Path>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsLink>k__BackingField, other.<IsLink>k__BackingField) && EqualityComparer<long?>.Default.Equals(this.<Bytes>k__BackingField, other.<Bytes>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<Modified>k__BackingField, other.<Modified>k__BackingField));
      }

      // Token: 0x060028E6 RID: 10470 RVA: 0x0041D900 File Offset: 0x0041D900
      [CompilerGenerated]
      private Artifact(AgentTools.Artifact original)
      {
        this.Path = original.<Path>k__BackingField;
        this.IsLink = original.<IsLink>k__BackingField;
        this.Bytes = original.<Bytes>k__BackingField;
        this.Modified = original.<Modified>k__BackingField;
      }

      // Token: 0x060028E7 RID: 10471 RVA: 0x0041D938 File Offset: 0x0041D938
      [CompilerGenerated]
      public void Deconstruct(out string Path, out bool IsLink, out long? Bytes, out DateTimeOffset? Modified)
      {
        Path = this.Path;
        IsLink = this.IsLink;
        Bytes = this.Bytes;
        Modified = this.Modified;
      }
    }

    // Token: 0x02000374 RID: 884
    [Nullable(0)]
    private sealed class ArtifactCandidate : IEquatable<AgentTools.ArtifactCandidate>
    {
      // Token: 0x060028E8 RID: 10472 RVA: 0x0041D964 File Offset: 0x0041D964
      public ArtifactCandidate([Nullable(2)] string ThreadId, string Title, string Uri, string Kind, DateTimeOffset UpdatedAt)
      {
        this.ThreadId = ThreadId;
        this.Title = Title;
        this.Uri = Uri;
        this.Kind = Kind;
        this.UpdatedAt = UpdatedAt;
        base..ctor();
      }

      // Token: 0x170007F7 RID: 2039
      // (get) Token: 0x060028E9 RID: 10473 RVA: 0x0041D994 File Offset: 0x0041D994
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(AgentTools.ArtifactCandidate);
        }
      }

      // Token: 0x170007F8 RID: 2040
      // (get) Token: 0x060028EA RID: 10474 RVA: 0x0041D9A0 File Offset: 0x0041D9A0
      // (set) Token: 0x060028EB RID: 10475 RVA: 0x0041D9A8 File Offset: 0x0041D9A8
      [Nullable(2)]
      public string ThreadId
      {
        [NullableContext(2)]
        get;
        [NullableContext(2)]
        set;
      }

      // Token: 0x170007F9 RID: 2041
      // (get) Token: 0x060028EC RID: 10476 RVA: 0x0041D9B4 File Offset: 0x0041D9B4
      // (set) Token: 0x060028ED RID: 10477 RVA: 0x0041D9BC File Offset: 0x0041D9BC
      public string Title { get; set; }

      // Token: 0x170007FA RID: 2042
      // (get) Token: 0x060028EE RID: 10478 RVA: 0x0041D9C8 File Offset: 0x0041D9C8
      // (set) Token: 0x060028EF RID: 10479 RVA: 0x0041D9D0 File Offset: 0x0041D9D0
      public string Uri { get; set; }

      // Token: 0x170007FB RID: 2043
      // (get) Token: 0x060028F0 RID: 10480 RVA: 0x0041D9DC File Offset: 0x0041D9DC
      // (set) Token: 0x060028F1 RID: 10481 RVA: 0x0041D9E4 File Offset: 0x0041D9E4
      public string Kind { get; set; }

      // Token: 0x170007FC RID: 2044
      // (get) Token: 0x060028F2 RID: 10482 RVA: 0x0041D9F0 File Offset: 0x0041D9F0
      // (set) Token: 0x060028F3 RID: 10483 RVA: 0x0041D9F8 File Offset: 0x0041D9F8
      public DateTimeOffset UpdatedAt { get; set; }

      // Token: 0x060028F4 RID: 10484 RVA: 0x0041DA04 File Offset: 0x0041DA04
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("ArtifactCandidate");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060028F5 RID: 10485 RVA: 0x0041DA50 File Offset: 0x0041DA50
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("ThreadId = ");
        builder.Append(this.ThreadId);
        builder.Append(", Title = ");
        builder.Append(this.Title);
        builder.Append(", Uri = ");
        builder.Append(this.Uri);
        builder.Append(", Kind = ");
        builder.Append(this.Kind);
        builder.Append(", UpdatedAt = ");
        builder.Append(this.UpdatedAt.ToString());
        return true;
      }

      // Token: 0x060028F6 RID: 10486 RVA: 0x0041DAF0 File Offset: 0x0041DAF0
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(AgentTools.ArtifactCandidate left, AgentTools.ArtifactCandidate right)
      {
        return !(left == right);
      }

      // Token: 0x060028F7 RID: 10487 RVA: 0x0041DAFC File Offset: 0x0041DAFC
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(AgentTools.ArtifactCandidate left, AgentTools.ArtifactCandidate right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060028F8 RID: 10488 RVA: 0x0041DB10 File Offset: 0x0041DB10
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ThreadId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Uri>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<UpdatedAt>k__BackingField);
      }

      // Token: 0x060028F9 RID: 10489 RVA: 0x0041DBA0 File Offset: 0x0041DBA0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as AgentTools.ArtifactCandidate);
      }

      // Token: 0x060028FA RID: 10490 RVA: 0x0041DBB0 File Offset: 0x0041DBB0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(AgentTools.ArtifactCandidate other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<ThreadId>k__BackingField, other.<ThreadId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Uri>k__BackingField, other.<Uri>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<DateTimeOffset>.Default.Equals(this.<UpdatedAt>k__BackingField, other.<UpdatedAt>k__BackingField));
      }

      // Token: 0x060028FC RID: 10492 RVA: 0x0041DC60 File Offset: 0x0041DC60
      [CompilerGenerated]
      private ArtifactCandidate(AgentTools.ArtifactCandidate original)
      {
        this.ThreadId = original.<ThreadId>k__BackingField;
        this.Title = original.<Title>k__BackingField;
        this.Uri = original.<Uri>k__BackingField;
        this.Kind = original.<Kind>k__BackingField;
        this.UpdatedAt = original.<UpdatedAt>k__BackingField;
      }

      // Token: 0x060028FD RID: 10493 RVA: 0x0041DCB0 File Offset: 0x0041DCB0
      [CompilerGenerated]
      public void Deconstruct([Nullable(2)] out string ThreadId, out string Title, out string Uri, out string Kind, out DateTimeOffset UpdatedAt)
      {
        ThreadId = this.ThreadId;
        Title = this.Title;
        Uri = this.Uri;
        Kind = this.Kind;
        UpdatedAt = this.UpdatedAt;
      }
    }

    // Token: 0x02000375 RID: 885
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040017F1 RID: 6129
      [Nullable(0)]
      public static Func<string, bool> <0>__IsImage;

      // Token: 0x040017F2 RID: 6130
      [Nullable(0)]
      public static Func<AgentTools.ArtifactCandidate, int> <1>__OpenScore;
    }
  }
}
