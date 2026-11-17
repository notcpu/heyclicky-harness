using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Agents.Harness;
using HeyClicky.Agents.Jobs;
using HeyClicky.Agents.Turns;

namespace HeyClicky.Agents.Runtime
{
  // Token: 0x02000024 RID: 36
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class CodexSession
  {
    // Token: 0x17000109 RID: 265
    // (get) Token: 0x060003EA RID: 1002 RVA: 0x000E7D54 File Offset: 0x000E7D54
    public int ActiveTurnCount
    {
      get
      {
        return this.activeByThread.Count;
      }
    }

    // Token: 0x060003EB RID: 1003 RVA: 0x000E7D64 File Offset: 0x000E7D64
    public bool IsThreadActive(string threadId)
    {
      return this.activeByThread.ContainsKey(threadId);
    }

    // Token: 0x060003EC RID: 1004 RVA: 0x000E7D74 File Offset: 0x000E7D74
    public bool Interrupt(AgentTask task, TurnOutcome outcome, string summary)
    {
      object gate = task.Gate;
      string threadId;
      lock (gate)
      {
        threadId = task.ThreadId;
      }
      AgentTask agentTask;
      if (threadId == null || !this.activeByThread.TryGetValue(threadId, out agentTask) || agentTask != task)
      {
        return false;
      }
      this.InterruptHarnessTurn(threadId);
      this.EndTurn(task, outcome, summary);
      return true;
    }

    // Token: 0x060003ED RID: 1005 RVA: 0x000E7DE0 File Offset: 0x000E7DE0
    private void EndTurn(AgentTask task, TurnOutcome outcome, [Nullable(2)] string message)
    {
      object gate = task.Gate;
      string threadId;
      string turnId;
      lock (gate)
      {
        threadId = task.ThreadId;
        turnId = task.TurnId;
      }
      if (threadId != null)
      {
        this.activeByThread.TryRemove(new KeyValuePair<string, AgentTask>(threadId, task));
      }
      if (turnId != null)
      {
        this.RememberFinishedTurn(turnId);
      }
      Action<AgentTask, TurnOutcome, string> onTurnEnded = this.OnTurnEnded;
      if (onTurnEnded == null)
      {
        return;
      }
      onTurnEnded(task, outcome, message);
    }

    // Token: 0x060003EE RID: 1006 RVA: 0x000E7E5C File Offset: 0x000E7E5C
    private void RememberFinishedTurn(string turnId)
    {
      this.finishedTurnIds[turnId] = 0;
      if (this.finishedTurnIds.Count > 256)
      {
        foreach (string text in this.finishedTurnIds.Keys.Take<string>(64))
        {
          byte b;
          this.finishedTurnIds.TryRemove(text, out b);
        }
      }
    }

    // Token: 0x060003EF RID: 1007 RVA: 0x000E7EDC File Offset: 0x000E7EDC
    private void HandleNotification(string method, [Nullable(2)] JsonObject p)
    {
      string text;
      if (p == null)
      {
        text = null;
      }
      else
      {
        JsonNode jsonNode = p["threadId"];
        text = ((jsonNode != null) ? jsonNode.ToString() : null);
      }
      string text2 = text;
      AgentTask agentTask2;
      AgentTask agentTask = ((text2 != null && this.activeByThread.TryGetValue(text2, out agentTask2)) ? agentTask2 : null);
      object obj;
      if (p == null)
      {
        obj = null;
      }
      else
      {
        JsonNode jsonNode2 = p["turnId"];
        obj = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
      }
      object obj2;
      if ((obj2 = obj) == null)
      {
        if (p == null)
        {
          obj2 = null;
        }
        else
        {
          JsonNode jsonNode3 = p["turn"];
          if (jsonNode3 == null)
          {
            obj2 = null;
          }
          else
          {
            JsonNode jsonNode4 = jsonNode3["id"];
            obj2 = ((jsonNode4 != null) ? jsonNode4.ToString() : null);
          }
        }
      }
      string text3 = obj2;
      if (text3 != null)
      {
        if (this.finishedTurnIds.ContainsKey(text3))
        {
          return;
        }
        if (agentTask != null)
        {
          object obj3 = agentTask.Gate;
          string turnId;
          lock (obj3)
          {
            turnId = agentTask.TurnId;
          }
          if (turnId != null && turnId != text3)
          {
            return;
          }
        }
      }
      if (method != null)
      {
        int num = method.Length;
        if (num <= 12)
        {
          if (num != 5)
          {
            if (num != 12)
            {
              return;
            }
            char c = method[0];
            if (c != 'i')
            {
              if (c != 't')
              {
                return;
              }
              if (!(method == "turn/started"))
              {
                return;
              }
              if (agentTask == null)
              {
                return;
              }
              object obj3 = agentTask.Gate;
              lock (obj3)
              {
                string text4;
                if (p == null)
                {
                  text4 = null;
                }
                else
                {
                  JsonNode jsonNode5 = p["turn"];
                  if (jsonNode5 == null)
                  {
                    text4 = null;
                  }
                  else
                  {
                    JsonNode jsonNode6 = jsonNode5["id"];
                    text4 = ((jsonNode6 != null) ? jsonNode6.ToString() : null);
                  }
                }
                string text5 = text4;
                if (text5 != null)
                {
                  agentTask.TurnId = text5;
                }
                agentTask.ProgressSummary = "Thinking…";
              }
              Action<AgentTask, AgentTaskEventKind, string> onTaskEvent = this.OnTaskEvent;
              if (onTaskEvent == null)
              {
                return;
              }
              onTaskEvent(agentTask, AgentTaskEventKind.ProgressChanged, "Thinking…");
              return;
            }
            else
            {
              if (!(method == "item/started"))
              {
                return;
              }
              if (agentTask == null)
              {
                return;
              }
              JsonObject jsonObject = ((p != null) ? p["item"] : null) as JsonObject;
              if (jsonObject != null)
              {
                this.OnItemStarted(agentTask, jsonObject);
                return;
              }
              return;
            }
          }
          else if (!(method == "error"))
          {
            return;
          }
        }
        else
        {
          if (num != 14)
          {
            object obj3;
            if (num != 17)
            {
              switch (num)
              {
              case 23:
                if (!(method == "item/agentMessage/delta"))
                {
                  return;
                }
                if (agentTask != null)
                {
                  string text6;
                  if (p == null)
                  {
                    text6 = null;
                  }
                  else
                  {
                    JsonNode jsonNode7 = p["delta"];
                    text6 = ((jsonNode7 != null) ? jsonNode7.ToString() : null);
                  }
                  string text7 = text6;
                  if (text7 != null)
                  {
                    obj3 = agentTask.Gate;
                    bool flag2;
                    lock (obj3)
                    {
                      flag2 = agentTask.CurrentMessagePhase == "commentary";
                      if (flag2)
                      {
                        agentTask.StreamingCommentary.Append(text7);
                      }
                      else
                      {
                        agentTask.ResponseAccumulator.Append(text7);
                      }
                      if (agentTask.FirstTokenTimestamp == 0L)
                      {
                        agentTask.FirstTokenTimestamp = Stopwatch.GetTimestamp();
                      }
                    }
                    Action<AgentTask, AgentTaskEventKind, string> onTaskEvent2 = this.OnTaskEvent;
                    if (onTaskEvent2 == null)
                    {
                      return;
                    }
                    onTaskEvent2(agentTask, flag2 ? AgentTaskEventKind.CommentaryDelta : AgentTaskEventKind.ResponseDelta, text7);
                    return;
                  }
                }
                return;
              case 24:
              case 26:
              case 29:
              case 30:
              case 32:
                return;
              case 25:
                if (!(method == "thread/tokenUsage/updated"))
                {
                  return;
                }
                if (agentTask != null)
                {
                  object obj4;
                  if (p == null)
                  {
                    obj4 = null;
                  }
                  else
                  {
                    JsonNode jsonNode8 = p["tokenUsage"];
                    obj4 = ((jsonNode8 != null) ? jsonNode8["total"] : null);
                  }
                  JsonObject jsonObject2 = obj4 as JsonObject;
                  if (jsonObject2 != null)
                  {
                    obj3 = agentTask.Gate;
                    lock (obj3)
                    {
                      agentTask.InputTokens = CodexSession.Long(jsonObject2["inputTokens"]);
                      agentTask.CachedInputTokens = CodexSession.Long(jsonObject2["cachedInputTokens"]);
                      agentTask.OutputTokens = CodexSession.Long(jsonObject2["outputTokens"]);
                      return;
                    }
                    goto IL_0A00;
                  }
                }
                return;
              case 27:
                if (!(method == "item/fileChange/outputDelta"))
                {
                  return;
                }
                break;
              case 28:
                if (!(method == "item/fileChange/patchUpdated"))
                {
                  return;
                }
                if (agentTask != null)
                {
                  JsonArray jsonArray = ((p != null) ? p["changes"] : null) as JsonArray;
                  if (jsonArray != null)
                  {
                    using (IEnumerator<JsonObject> enumerator = jsonArray.OfType<JsonObject>().GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        JsonObject jsonObject3 = enumerator.Current;
                        JsonNode jsonNode9 = jsonObject3["path"];
                        string text8 = ((jsonNode9 != null) ? jsonNode9.ToString() : null);
                        if (text8 != null)
                        {
                          JsonNode jsonNode10 = jsonObject3["diff"];
                          string text9 = ((jsonNode10 != null) ? jsonNode10.ToString() : null);
                          if (text9 != null)
                          {
                            JsonObject jsonObject4 = jsonObject3["kind"] as JsonObject;
                            string text10;
                            if (jsonObject4 == null)
                            {
                              text10 = null;
                            }
                            else
                            {
                              JsonNode jsonNode11 = jsonObject4["type"];
                              text10 = ((jsonNode11 != null) ? jsonNode11.ToString() : null);
                            }
                            string text11 = text10 ?? "update";
                            Action<AgentTask, string, string, string> onTaskFilePatchStreaming = this.OnTaskFilePatchStreaming;
                            if (onTaskFilePatchStreaming != null)
                            {
                              onTaskFilePatchStreaming(agentTask, text8, text11, text9);
                            }
                          }
                        }
                      }
                      return;
                    }
                    goto IL_031E;
                  }
                }
                return;
              case 31:
                if (!(method == "item/reasoning/summaryTextDelta"))
                {
                  return;
                }
                if (agentTask != null)
                {
                  string text12;
                  if (p == null)
                  {
                    text12 = null;
                  }
                  else
                  {
                    JsonNode jsonNode12 = p["delta"];
                    text12 = ((jsonNode12 != null) ? jsonNode12.ToString() : null);
                  }
                  string text13 = text12;
                  if (text13 != null)
                  {
                    obj3 = agentTask.Gate;
                    string text14;
                    lock (obj3)
                    {
                      JsonValue jsonValue = p["summaryIndex"] as JsonValue;
                      int num3;
                      int num2 = ((jsonValue != null && jsonValue.TryGetValue<int>(out num3)) ? num3 : 0);
                      int? reasoningSummaryIndex = agentTask.ReasoningSummaryIndex;
                      num = num2;
                      if (!((reasoningSummaryIndex.GetValueOrDefault() == num) & (reasoningSummaryIndex != null)))
                      {
                        agentTask.ReasoningSummaryText.Clear();
                      }
                      agentTask.ReasoningSummaryIndex = new int?(num2);
                      agentTask.ReasoningSummaryText.Append(text13);
                      if (agentTask.FirstTokenTimestamp == 0L)
                      {
                        agentTask.FirstTokenTimestamp = Stopwatch.GetTimestamp();
                      }
                      text14 = CodexSession.ReasoningHeading(agentTask.ReasoningSummaryText.ToString());
                      if (text14 != null && text14 != agentTask.ThinkingHeading)
                      {
                        agentTask.ThinkingHeading = text14;
                      }
                      else
                      {
                        text14 = null;
                      }
                    }
                    if (text14 == null)
                    {
                      return;
                    }
                    Action<AgentTask, AgentTaskEventKind, string> onTaskEvent3 = this.OnTaskEvent;
                    if (onTaskEvent3 == null)
                    {
                      return;
                    }
                    onTaskEvent3(agentTask, AgentTaskEventKind.ThinkingHeading, text14);
                    return;
                  }
                }
                return;
              case 33:
                if (!(method == "item/commandExecution/outputDelta"))
                {
                  return;
                }
                if (agentTask != null)
                {
                  string text15;
                  if (p == null)
                  {
                    text15 = null;
                  }
                  else
                  {
                    JsonNode jsonNode13 = p["delta"];
                    text15 = ((jsonNode13 != null) ? jsonNode13.ToString() : null);
                  }
                  string text16 = text15;
                  if (text16 != null)
                  {
                    string text17 = string.Join<string>('\n', text16.Split('\n', StringSplitOptions.None).Take<string>(3));
                    if (text17.Trim().Length > 0)
                    {
                      obj3 = agentTask.Gate;
                      lock (obj3)
                      {
                        agentTask.AddActivity(AgentActivityKind.ShellOutput, (text17.Length > 200) ? text17.Substring(0, 200) : text17);
                        return;
                      }
                      break;
                    }
                    return;
                  }
                }
                return;
              default:
                return;
              }
              if (agentTask != null)
              {
                string text18;
                if (p == null)
                {
                  text18 = null;
                }
                else
                {
                  JsonNode jsonNode14 = p["delta"];
                  text18 = ((jsonNode14 != null) ? jsonNode14.ToString() : null);
                }
                string text19 = text18;
                if (text19 != null)
                {
                  obj3 = agentTask.Gate;
                  lock (obj3)
                  {
                    agentTask.AddActivity(AgentActivityKind.FileChange, (text19.Length > 150) ? text19.Substring(0, 150) : text19);
                    return;
                  }
                  goto IL_0770;
                }
              }
              return;
            }
            if (!(method == "turn/plan/updated"))
            {
              return;
            }
            IL_031E:
            if (agentTask == null)
            {
              return;
            }
            JsonArray jsonArray2;
            if ((jsonArray2 = ((p != null) ? p["plan"] : null) as JsonArray) == null)
            {
              jsonArray2 = new JsonArray(null);
            }
            List<AgentPlanStep> list = (from s in jsonArray2.OfType<JsonObject>().Select<JsonObject, AgentPlanStep>(delegate(JsonObject s)
              {
                JsonNode jsonNode22 = s["step"];
                string text30;
                if (jsonNode22 == null)
                {
                  text30 = null;
                }
                else
                {
                  string text31 = jsonNode22.ToString();
                  text30 = ((text31 != null) ? text31.Trim() : null);
                }
                string text32 = text30 ?? "";
                JsonNode jsonNode23 = s["status"];
                return new AgentPlanStep(text32, ((jsonNode23 != null) ? jsonNode23.ToString() : null) ?? "pending");
              })
              where s.Text.Length > 0
              select s).ToList<AgentPlanStep>();
            if (list.Count == 0)
            {
              return;
            }
            obj3 = agentTask.Gate;
            lock (obj3)
            {
              agentTask.Plan = list;
            }
            Action<AgentTask, IReadOnlyList<AgentPlanStep>> onTaskPlanUpdated = this.OnTaskPlanUpdated;
            if (onTaskPlanUpdated == null)
            {
              return;
            }
            onTaskPlanUpdated(agentTask, list);
            return;
          }
          else
          {
            char c = method[0];
            if (c != 'i')
            {
              if (c != 't')
              {
                return;
              }
              if (!(method == "turn/completed"))
              {
                return;
              }
              if (text3 != null)
              {
                this.RememberFinishedTurn(text3);
              }
              if (agentTask == null)
              {
                return;
              }
              string text20;
              if (p == null)
              {
                text20 = null;
              }
              else
              {
                JsonNode jsonNode15 = p["turn"];
                if (jsonNode15 == null)
                {
                  text20 = null;
                }
                else
                {
                  JsonNode jsonNode16 = jsonNode15["status"];
                  text20 = ((jsonNode16 != null) ? jsonNode16.ToString() : null);
                }
              }
              string text21 = text20 ?? "completed";
              JsonObject jsonObject5 = ((p != null) ? p["turn"] : null) as JsonObject;
              JsonNode jsonNode17 = ((jsonObject5 != null) ? jsonObject5["error"] : null);
              JsonObject jsonObject6 = jsonNode17 as JsonObject;
              string text22;
              if (jsonObject6 == null)
              {
                text22 = ((jsonNode17 != null) ? jsonNode17.ToString() : null);
              }
              else
              {
                JsonNode jsonNode18 = jsonObject6["message"];
                text22 = ((jsonNode18 != null) ? jsonNode18.ToString() : null) ?? jsonObject6.ToJsonString(null);
              }
              string text23 = text22;
              object obj3 = agentTask.Gate;
              lock (obj3)
              {
                string text24;
                if (p == null)
                {
                  text24 = null;
                }
                else
                {
                  JsonNode jsonNode19 = p["turn"];
                  if (jsonNode19 == null)
                  {
                    text24 = null;
                  }
                  else
                  {
                    JsonNode jsonNode20 = jsonNode19["id"];
                    text24 = ((jsonNode20 != null) ? jsonNode20.ToString() : null);
                  }
                }
                string text25 = text24;
                if (text25 != null)
                {
                  agentTask.TurnId = text25;
                }
              }
              if (text21 == "interrupted")
              {
                obj3 = agentTask.Gate;
                bool requiresExtraUsageApproval;
                lock (obj3)
                {
                  requiresExtraUsageApproval = agentTask.RequiresExtraUsageApproval;
                  if (!requiresExtraUsageApproval && !CodexSession.IsExtraEffortError(text23) && agentTask.IsActive)
                  {
                    agentTask.InterruptedUnexpectedly = true;
                  }
                }
                this.EndTurn(agentTask, (requiresExtraUsageApproval || CodexSession.IsExtraEffortError(text23)) ? TurnOutcome.ExtraEffortRequired : TurnOutcome.Interrupted, text23 ?? "Agent thread was interrupted.");
                return;
              }
              if (!(text21 == "failed"))
              {
                this.EndTurn(agentTask, TurnOutcome.Completed, null);
                return;
              }
              this.EndTurn(agentTask, CodexSession.IsExtraEffortError(text23) ? TurnOutcome.ExtraEffortRequired : TurnOutcome.Failed, text23 ?? "Agent thread failed.");
              return;
            }
            else if (!(method == "item/completed"))
            {
              return;
            }
          }
          IL_0770:
          if (agentTask == null)
          {
            return;
          }
          JsonObject jsonObject7 = ((p != null) ? p["item"] : null) as JsonObject;
          if (jsonObject7 != null)
          {
            this.OnItemCompleted(agentTask, jsonObject7);
            return;
          }
          return;
        }
        IL_0A00:
        JsonObject jsonObject8 = ((p != null) ? p["error"] : null) as JsonObject;
        if (jsonObject8 == null)
        {
          return;
        }
        JsonNode jsonNode21 = jsonObject8["message"];
        string text26 = ((jsonNode21 != null) ? jsonNode21.ToString() : null) ?? "Unknown error";
        JsonValue jsonValue2 = p["willRetry"] as JsonValue;
        bool flag4;
        bool flag3 = jsonValue2 != null && jsonValue2.TryGetValue<bool>(out flag4) && flag4;
        string text27 = "Codex";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
        defaultInterpolatedStringHandler.AppendLiteral("error notification thread=");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendLiteral(" willRetry=");
        defaultInterpolatedStringHandler.AppendFormatted<bool>(flag3);
        defaultInterpolatedStringHandler.AppendLiteral(": ");
        defaultInterpolatedStringHandler.AppendFormatted(CodexSession.Truncate(text26, 500));
        Log.Warn(text27, defaultInterpolatedStringHandler.ToStringAndClear());
        if (agentTask == null)
        {
          return;
        }
        if (CodexSession.IsExtraEffortError(text26))
        {
          object obj3 = agentTask.Gate;
          lock (obj3)
          {
            agentTask.AddActivity(AgentActivityKind.Commentary, "This task requires extra effort");
          }
          this.Interrupt(agentTask, TurnOutcome.ExtraEffortRequired, "This task requires extra effort");
          return;
        }
        if (flag3)
        {
          string text28 = CodexSession.RetrySummary(text26);
          object obj3 = agentTask.Gate;
          lock (obj3)
          {
            agentTask.AddActivity(AgentActivityKind.Commentary, text28);
            agentTask.ProgressSummary = text28;
          }
          Action<AgentTask, AgentTaskEventKind, string> onTaskEvent4 = this.OnTaskEvent;
          if (onTaskEvent4 == null)
          {
            return;
          }
          onTaskEvent4(agentTask, AgentTaskEventKind.ProgressChanged, text28);
          return;
        }
        else
        {
          string text29 = CodexSession.UserFacingError(text26);
          object obj3 = agentTask.Gate;
          lock (obj3)
          {
            agentTask.AddActivity(AgentActivityKind.Error, text29);
          }
          this.EndTurn(agentTask, TurnOutcome.Failed, text29);
        }
      }
    }

    // Token: 0x060003F0 RID: 1008 RVA: 0x000E8B3C File Offset: 0x000E8B3C
    private void OnItemStarted(AgentTask task, JsonObject item)
    {
      JsonNode jsonNode = item["type"];
      string text = ((jsonNode != null) ? jsonNode.ToString() : null) ?? "unknown";
      JsonNode jsonNode2 = item["id"];
      string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
      string text3 = "Codex";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
      defaultInterpolatedStringHandler.AppendLiteral("item started: ");
      defaultInterpolatedStringHandler.AppendFormatted(text);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted<JsonNode>(item["phase"]);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      string text4;
      if (!(text == "commandExecution"))
      {
        text4 = "";
      }
      else
      {
        JsonNode jsonNode3 = item["command"];
        text4 = CodexSession.Truncate(((jsonNode3 != null) ? jsonNode3.ToJsonString(null) : null) ?? "", 400);
      }
      defaultInterpolatedStringHandler.AppendFormatted(text4);
      Log.Info(text3, defaultInterpolatedStringHandler.ToStringAndClear());
      string text5 = CodexSession.PublicToolActivityText(item);
      string text6 = null;
      object gate = task.Gate;
      string text7;
      lock (gate)
      {
        text7 = task.FinishStreamingCommentary(task.CurrentNarrationItemId, null);
        task.CurrentNarrationItemId = null;
        if (text5 != null)
        {
          task.RecordToolActivity(text2, text5, true);
          text6 = text5;
        }
        if (!(text == "reasoning"))
        {
          if (!(text == "commandExecution"))
          {
            if (text == "agentMessage")
            {
              JsonNode jsonNode4 = item["phase"];
              string text8 = ((jsonNode4 != null) ? jsonNode4.ToString() : null) ?? "final_answer";
              if (text8 == "commentary")
              {
                task.CurrentNarrationItemId = text2;
                text6 = "Sharing an update…";
              }
              else
              {
                if (task.ResponseAccumulator.Length > 0)
                {
                  task.CompletedReplies.Add(task.ResponseAccumulator.ToString());
                }
                task.ResponseAccumulator.Clear();
                text6 = "Writing…";
              }
              task.CurrentMessagePhase = text8;
            }
          }
          else
          {
            string text9 = CodexSession.ShellCommandDisplay(item["command"]);
            task.RecordToolActivity(text2, text9, true);
            text6 = "Running command: " + text9;
            text5 = text9;
          }
        }
        else
        {
          task.CurrentNarrationItemId = text2;
          task.ReasoningSummaryIndex = null;
          task.ReasoningSummaryText.Clear();
          text6 = "Thinking…";
          if (task.Activity.Count != 0)
          {
            List<ActivityRow> activity = task.Activity;
            if (activity[activity.Count - 1].Kind == AgentActivityKind.Thinking)
            {
              goto IL_025D;
            }
          }
          task.AddActivity(AgentActivityKind.Thinking, "Reasoning...");
        }
        IL_025D:
        if (text6 != null)
        {
          task.ProgressSummary = text6;
        }
      }
      if (text7 != null)
      {
        Action<AgentTask, AgentTaskEventKind, string> onTaskEvent = this.OnTaskEvent;
        if (onTaskEvent != null)
        {
          onTaskEvent(task, AgentTaskEventKind.Commentary, text7);
        }
      }
      if (text5 != null)
      {
        Action<AgentTask, AgentTaskEventKind, string> onTaskEvent2 = this.OnTaskEvent;
        if (onTaskEvent2 != null)
        {
          onTaskEvent2(task, AgentTaskEventKind.ToolActivity, text5);
        }
      }
      string text10 = CodexSession.PublicToolActivityText(item);
      if (text10 != null)
      {
        Action<AgentTask, string> onTaskToolStep = this.OnTaskToolStep;
        if (onTaskToolStep != null)
        {
          onTaskToolStep(task, text10);
        }
      }
      if (text6 != null)
      {
        Action<AgentTask, AgentTaskEventKind, string> onTaskEvent3 = this.OnTaskEvent;
        if (onTaskEvent3 != null)
        {
          onTaskEvent3(task, AgentTaskEventKind.ProgressChanged, text6);
        }
      }
      string text11 = IntegrationUse.ProviderKey(item);
      if (text11 != null)
      {
        Action<AgentTask, string> onIntegrationUsed = this.OnIntegrationUsed;
        if (onIntegrationUsed == null)
        {
          return;
        }
        onIntegrationUsed(task, text11);
      }
    }

    // Token: 0x060003F1 RID: 1009 RVA: 0x000E8E64 File Offset: 0x000E8E64
    private void OnItemCompleted(AgentTask task, JsonObject item)
    {
      JsonNode jsonNode = item["type"];
      string text = ((jsonNode != null) ? jsonNode.ToString() : null) ?? "unknown";
      JsonNode jsonNode2 = item["id"];
      string text2 = ((jsonNode2 != null) ? jsonNode2.ToString() : null);
      string text3 = null;
      string text4 = null;
      string text5 = CodexSession.PublicToolActivityText(item);
      if (text == "fileChange")
      {
        JsonArray jsonArray = item["changes"] as JsonArray;
        if (jsonArray != null)
        {
          List<string> list = jsonArray.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject c)
          {
            JsonNode jsonNode4 = c["path"];
            if (jsonNode4 == null)
            {
              return null;
            }
            return jsonNode4.ToString();
          }).OfType<string>()
            .ToList<string>();
          if (list != null && list.Count > 0)
          {
            Action<AgentTask, IReadOnlyList<string>> onTaskFileChangeCompleted = this.OnTaskFileChangeCompleted;
            if (onTaskFileChangeCompleted != null)
            {
              onTaskFileChangeCompleted(task, list);
            }
          }
        }
      }
      if (text5 != null)
      {
        Action<AgentTask, string> onTaskToolStep = this.OnTaskToolStep;
        if (onTaskToolStep != null)
        {
          onTaskToolStep(task, text5);
        }
      }
      object gate = task.Gate;
      lock (gate)
      {
        if (text5 != null)
        {
          string text6 = text5;
          task.RecordToolActivity(text2, text6, false);
          task.ProgressSummary = "Working on it…";
        }
        else if (text == "commandExecution")
        {
          task.RecordToolActivity(text2, CodexSession.ShellCommandDisplay(item["command"]), false);
          task.ProgressSummary = "Working on it…";
        }
        if (text == "agentMessage")
        {
          JsonNode jsonNode3 = item["phase"];
          if (((jsonNode3 != null) ? jsonNode3.ToString() : null) == "commentary")
          {
            text3 = task.FinishStreamingCommentary(text2, CodexSession.ItemText(item));
            goto IL_0250;
          }
        }
        if (text == "agentMessage" && task.ResponseAccumulator.Length == 0)
        {
          string text7 = CodexSession.ItemText(item);
          if (text7 != null)
          {
            task.ResponseAccumulator.Append(text7);
            goto IL_0250;
          }
        }
        if (text == "reasoning")
        {
          JsonArray jsonArray2 = item["summary"] as JsonArray;
          object obj;
          if (jsonArray2 == null)
          {
            obj = null;
          }
          else
          {
            obj = jsonArray2.Select<JsonNode, string>(delegate(JsonNode n)
            {
              JsonObject jsonObject = n as JsonObject;
              if (jsonObject == null)
              {
                if (n == null)
                {
                  return null;
                }
                return n.ToString();
              }
              else
              {
                JsonNode jsonNode5 = jsonObject["text"];
                if (jsonNode5 == null)
                {
                  return null;
                }
                return jsonNode5.ToString();
              }
            }).OfType<string>().ToList<string>();
          }
          object obj2 = obj;
          string text8 = ((obj2 != null) ? obj2.LastOrDefault<string>() : null);
          if (text8 != null)
          {
            string text9 = CodexSession.ReasoningHeading(text8);
            if (text9 != null && text9 != task.ThinkingHeading)
            {
              text4 = (task.ThinkingHeading = text9);
            }
          }
        }
      }
      IL_0250:
      if (text3 != null)
      {
        Action<AgentTask, AgentTaskEventKind, string> onTaskEvent = this.OnTaskEvent;
        if (onTaskEvent != null)
        {
          onTaskEvent(task, AgentTaskEventKind.Commentary, text3);
        }
      }
      if (text4 != null)
      {
        Action<AgentTask, AgentTaskEventKind, string> onTaskEvent2 = this.OnTaskEvent;
        if (onTaskEvent2 == null)
        {
          return;
        }
        onTaskEvent2(task, AgentTaskEventKind.ThinkingHeading, text4);
      }
    }

    // Token: 0x060003F2 RID: 1010 RVA: 0x000E910C File Offset: 0x000E910C
    [return: Nullable(2)]
    internal static string ItemText(JsonObject item)
    {
      foreach (string text in new string[] { "text", "message" })
      {
        JsonNode jsonNode = item[text];
        if (jsonNode != null && jsonNode.GetValueKind() == JsonValueKind.String && item[text].ToString().Trim().Length > 0)
        {
          return item[text].ToString();
        }
      }
      JsonArray jsonArray = item["content"] as JsonArray;
      if (jsonArray != null)
      {
        string text2 = string.Join("\n", jsonArray.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject o)
        {
          JsonNode jsonNode2 = o["text"];
          if (jsonNode2 == null)
          {
            return null;
          }
          return jsonNode2.ToString();
        }).OfType<string>());
        if (text2.Trim().Length > 0)
        {
          return text2;
        }
      }
      return null;
    }

    // Token: 0x060003F3 RID: 1011 RVA: 0x000E91E4 File Offset: 0x000E91E4
    [return: Nullable(2)]
    internal static string PublicToolActivityText(JsonObject item)
    {
      JsonNode jsonNode = item["type"];
      string text = ((jsonNode != null) ? jsonNode.ToString() : null);
      if (text == "mcpToolCall" || text == "mcp_tool_call" || text == "dynamicToolCall")
      {
        string text2 = "Using: ";
        JsonNode jsonNode2 = item["tool"];
        return text2 + (((jsonNode2 != null) ? jsonNode2.ToString() : null) ?? "tool").Replace('_', ' ');
      }
      if (text == "webSearch")
      {
        JsonObject jsonObject = item["action"] as JsonObject;
        string text3;
        if (jsonObject == null)
        {
          text3 = null;
        }
        else
        {
          JsonNode jsonNode3 = jsonObject["type"];
          text3 = ((jsonNode3 != null) ? jsonNode3.ToString() : null);
        }
        string text4 = text3;
        if (text4 == "openPage")
        {
          string text5;
          if (jsonObject == null)
          {
            text5 = null;
          }
          else
          {
            JsonNode jsonNode4 = jsonObject["url"];
            text5 = ((jsonNode4 != null) ? jsonNode4.ToString() : null);
          }
          string text6 = text5;
          if (text6 != null)
          {
            Uri uri;
            return "Reading: " + (Uri.TryCreate(text6, UriKind.Absolute, out uri) ? uri.Host : text6);
          }
        }
        if (text4 == "findInPage")
        {
          string text7;
          if (jsonObject == null)
          {
            text7 = null;
          }
          else
          {
            JsonNode jsonNode5 = jsonObject["pattern"];
            text7 = ((jsonNode5 != null) ? jsonNode5.ToString() : null);
          }
          string text8 = text7;
          if (text8 != null)
          {
            return "Finding: " + text8;
          }
        }
        JsonNode jsonNode6 = item["query"];
        string text9;
        if ((text9 = ((jsonNode6 != null) ? jsonNode6.ToString() : null)) == null)
        {
          string text10;
          if (jsonObject == null)
          {
            text10 = null;
          }
          else
          {
            JsonNode jsonNode7 = jsonObject["query"];
            text10 = ((jsonNode7 != null) ? jsonNode7.ToString() : null);
          }
          text9 = text10 ?? "";
        }
        string text11 = text9;
        return "Search: " + ((text11.Length == 0) ? "the web" : text11);
      }
      if (!(text == "fileChange"))
      {
        return null;
      }
      JsonArray jsonArray = item["changes"] as JsonArray;
      List<string> list;
      if (jsonArray == null)
      {
        list = null;
      }
      else
      {
        IEnumerable<string> enumerable = jsonArray.OfType<JsonObject>().Select<JsonObject, string>(delegate(JsonObject c)
        {
          JsonNode jsonNode8 = c["path"];
          if (jsonNode8 == null)
          {
            return null;
          }
          return jsonNode8.ToString();
        }).OfType<string>();
        Func<string, string> func;
        if ((func = CodexSession.<>O.<0>__GetFileName) == null)
        {
          func = (CodexSession.<>O.<0>__GetFileName = new Func<string, string>(Path.GetFileName));
        }
        list = enumerable.Select<string, string>(func).ToList<string>();
      }
      List<string> list2 = list ?? new List<string>();
      return "Editing: " + ((list2.Count == 0) ? "files" : string.Join(", ", list2));
    }

    // Token: 0x060003F4 RID: 1012 RVA: 0x000E9440 File Offset: 0x000E9440
    internal static string ShellCommandDisplay([Nullable(2)] JsonNode commandNode)
    {
      JsonArray jsonArray = commandNode as JsonArray;
      string text;
      if (jsonArray == null)
      {
        if (commandNode != null)
        {
          text = commandNode.ToString();
        }
        else
        {
          text = "running command";
        }
      }
      else
      {
        text = string.Join<string>(' ', jsonArray.Select<JsonNode, string>(delegate(JsonNode n)
        {
          if (n == null)
          {
            return null;
          }
          return n.ToString();
        }));
      }
      string text2 = text;
      foreach (string text3 in new string[] { " -Command ", " -lc ", " -c " })
      {
        int num = text2.IndexOf(text3, StringComparison.OrdinalIgnoreCase);
        if (num >= 0 && num < 160)
        {
          text = text2;
          int num2 = num + text3.Length;
          text2 = CodexSession.FirstPosixWord(text.Substring(num2, text.Length - num2).TrimStart());
          break;
        }
      }
      if (text2.Length <= 300)
      {
        return text2;
      }
      return text2.Substring(0, 300) + "…";
    }

    // Token: 0x060003F5 RID: 1013 RVA: 0x000E9540 File Offset: 0x000E9540
    private static string FirstPosixWord(string s)
    {
      StringBuilder stringBuilder = new StringBuilder(s.Length);
      int num = 0;
      while (num < s.Length && !char.IsWhiteSpace(s[num]))
      {
        char c = s[num];
        if (c == '\'')
        {
          int num2 = s.IndexOf('\'', num + 1);
          if (num2 < 0)
          {
            stringBuilder.Append(s, num + 1, s.Length - num - 1);
            break;
          }
          stringBuilder.Append(s, num + 1, num2 - num - 1);
          num = num2 + 1;
        }
        else if (c == '"')
        {
          num++;
          while (num < s.Length && s[num] != '"')
          {
            bool flag = s[num] == '\\' && num + 1 < s.Length;
            if (flag)
            {
              char c2 = s[num + 1];
              if (c2 <= '$')
              {
                if (c2 != '"' && c2 != '$')
                {
                  goto IL_00C0;
                }
                goto IL_00BB;
              }
              else
              {
                if (c2 == '\\' || c2 == '`')
                {
                  goto IL_00BB;
                }
                goto IL_00C0;
              }
              IL_00C3:
              bool flag2;
              flag = flag2;
              goto IL_00C7;
              IL_00C0:
              flag2 = false;
              goto IL_00C3;
              IL_00BB:
              flag2 = true;
              goto IL_00C3;
            }
            IL_00C7:
            if (flag)
            {
              num++;
            }
            stringBuilder.Append(s[num++]);
          }
          num++;
        }
        else if (c == '\\' && num + 1 < s.Length)
        {
          stringBuilder.Append(s[num + 1]);
          num += 2;
        }
        else
        {
          stringBuilder.Append(c);
          num++;
        }
      }
      if (stringBuilder.Length != 0)
      {
        return stringBuilder.ToString();
      }
      return s;
    }

    // Token: 0x060003F6 RID: 1014 RVA: 0x000E96A8 File Offset: 0x000E96A8
    [return: Nullable(2)]
    internal static string ReasoningHeading(string summary)
    {
      string text = summary.Trim();
      if (!text.StartsWith("**", StringComparison.Ordinal))
      {
        return null;
      }
      int num = text.IndexOf("**", 2, StringComparison.Ordinal);
      if (num < 0)
      {
        return null;
      }
      string text2 = text.Substring(2, num - 2).Trim();
      int length = text2.Length;
      if (length <= 0 || length > 80 || text2.Contains('\n'))
      {
        return null;
      }
      return text2;
    }

    // Token: 0x060003F7 RID: 1015 RVA: 0x000E9710 File Offset: 0x000E9710
    [NullableContext(2)]
    internal static bool IsExtraEffortError(string text)
    {
      return text != null && (text.Contains("agent_extra_effort_required", StringComparison.OrdinalIgnoreCase) || text.Contains("agent_turn_limit_exceeded", StringComparison.OrdinalIgnoreCase) || text.Contains("this task requires extra effort", StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x060003F8 RID: 1016 RVA: 0x000E9744 File Offset: 0x000E9744
    private static string RetrySummary(string raw)
    {
      if (!raw.Contains("stream disconnected", StringComparison.OrdinalIgnoreCase) && !raw.Contains("reconnect", StringComparison.OrdinalIgnoreCase))
      {
        return "Retrying after a hiccup…";
      }
      return "Reconnecting to the model…";
    }

    // Token: 0x060003F9 RID: 1017 RVA: 0x000E9770 File Offset: 0x000E9770
    internal static string UserFacingError(string raw)
    {
      int num = raw.IndexOf('{');
      if (num >= 0)
      {
        try
        {
          string text = raw;
          int num2 = num;
          JsonObject jsonObject = JsonNode.Parse(text.Substring(num2, text.Length - num2), null, default(JsonDocumentOptions)) as JsonObject;
          object obj;
          if (jsonObject == null)
          {
            obj = null;
          }
          else
          {
            JsonNode jsonNode = jsonObject["message"];
            obj = ((jsonNode != null) ? jsonNode.ToString() : null);
          }
          object obj2;
          if ((obj2 = obj) == null)
          {
            object obj3;
            if (jsonObject == null)
            {
              obj3 = null;
            }
            else
            {
              JsonNode jsonNode2 = jsonObject["error"];
              if (jsonNode2 == null)
              {
                obj3 = null;
              }
              else
              {
                JsonNode jsonNode3 = jsonNode2["message"];
                obj3 = ((jsonNode3 != null) ? jsonNode3.ToString() : null);
              }
            }
            if ((obj2 = obj3) == null)
            {
              if (jsonObject == null)
              {
                obj2 = null;
              }
              else
              {
                JsonNode jsonNode4 = jsonObject["error"];
                obj2 = ((jsonNode4 != null) ? jsonNode4.ToString() : null);
              }
            }
          }
          string text2 = obj2;
          if (!string.IsNullOrWhiteSpace(text2))
          {
            return text2;
          }
        }
        catch (JsonException)
        {
        }
      }
      if (raw.Length <= 500)
      {
        return raw;
      }
      return raw.Substring(0, 500);
    }

    // Token: 0x060003FA RID: 1018 RVA: 0x000E9874 File Offset: 0x000E9874
    [NullableContext(2)]
    private static long Long(JsonNode n)
    {
      JsonValue jsonValue = n as JsonValue;
      long num;
      if (jsonValue == null || !jsonValue.TryGetValue<long>(out num))
      {
        return 0L;
      }
      return num;
    }

    // Token: 0x060003FB RID: 1019 RVA: 0x000E989C File Offset: 0x000E989C
    private static string Truncate(string s, int n)
    {
      if (s.Length <= n)
      {
        return s;
      }
      return s.Substring(0, n) + "…";
    }

    // Token: 0x060003FC RID: 1020 RVA: 0x000E98BC File Offset: 0x000E98BC
    public void StartHarnessTurn([TupleElementNames(new string[] { "Harness", "Place" })] [Nullable(0)] ValueTuple<AgentHarness, AgentHarnessPlace> choice, AgentTask task, string appThreadId, bool isNewAppThread, string userMessage, IReadOnlyList<string> imagePaths, string workingDirectory, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyDictionary<string, string> leaseMetadata, Func<string, AgentTask, string, CancellationToken, Task<AgentHarnessTurnContext>> contextProvider, CancellationToken ct)
    {
      CodexSession.<>c__DisplayClass34_0 CS$<>8__locals1 = new CodexSession.<>c__DisplayClass34_0();
      CS$<>8__locals1.appThreadId = appThreadId;
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.task = task;
      CS$<>8__locals1.choice = choice;
      CS$<>8__locals1.contextProvider = contextProvider;
      CS$<>8__locals1.workingDirectory = workingDirectory;
      CS$<>8__locals1.ct = ct;
      CS$<>8__locals1.isNewAppThread = isNewAppThread;
      CS$<>8__locals1.userMessage = userMessage;
      CS$<>8__locals1.imagePaths = imagePaths;
      CS$<>8__locals1.leaseMetadata = leaseMetadata;
      CodexSession.<>c__DisplayClass34_0 CS$<>8__locals2 = CS$<>8__locals1;
      IAgentHarnessRuntime agentHarnessRuntime = AgentHarnessRuntimes.Runtime(CS$<>8__locals1.choice.Item1, CS$<>8__locals1.choice.Item2);
      if (agentHarnessRuntime == null)
      {
        throw new AgentHarnessException(CS$<>8__locals1.choice.Item1.DisplayName() + " " + CS$<>8__locals1.choice.Item2.DisplayName().ToLowerInvariant() + " isn't available in this build.", true, false);
      }
      CS$<>8__locals2.runtime = agentHarnessRuntime;
      if (!this.activeByThread.TryAdd(CS$<>8__locals1.appThreadId, CS$<>8__locals1.task) && this.activeByThread[CS$<>8__locals1.appThreadId] != CS$<>8__locals1.task)
      {
        throw new InvalidOperationException("This job is still working on its current task.");
      }
      CS$<>8__locals1.turnId = "harness-turn-" + Guid.NewGuid().ToString("D").ToLowerInvariant();
      object gate = CS$<>8__locals1.task.Gate;
      lock (gate)
      {
        CS$<>8__locals1.task.TurnId = CS$<>8__locals1.turnId;
        CS$<>8__locals1.task.ProgressSummary = "Starting " + CS$<>8__locals1.choice.Item1.DisplayName() + "…";
      }
      Action<AgentTask, AgentTaskEventKind, string> onTaskEvent = this.OnTaskEvent;
      if (onTaskEvent != null)
      {
        onTaskEvent(CS$<>8__locals1.task, AgentTaskEventKind.ProgressChanged, "Starting " + CS$<>8__locals1.choice.Item1.DisplayName() + "…");
      }
      CS$<>8__locals1.harnessThreadId = AgentHarnessThreads.HarnessThreadId(CS$<>8__locals1.appThreadId, CS$<>8__locals1.choice.Item1, CS$<>8__locals1.choice.Item2);
      this.harnessThreadIdByAppThreadId[CS$<>8__locals1.appThreadId] = CS$<>8__locals1.harnessThreadId;
      string text = "Harness";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 4);
      defaultInterpolatedStringHandler.AppendLiteral("[harness] ");
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.choice.Item1.DisplayName());
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.choice.Item2.RawValue());
      defaultInterpolatedStringHandler.AppendLiteral(" turn on ");
      defaultInterpolatedStringHandler.AppendFormatted(CS$<>8__locals1.harnessThreadId);
      defaultInterpolatedStringHandler.AppendLiteral(" (app thread ");
      string appThreadId2 = CS$<>8__locals1.appThreadId;
      int num = Math.Min(8, CS$<>8__locals1.appThreadId.Length);
      int length = appThreadId2.Length;
      int num2 = length - num;
      defaultInterpolatedStringHandler.AppendFormatted(appThreadId2.Substring(num2, length - num2));
      defaultInterpolatedStringHandler.AppendLiteral(")");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      CodexSession.<>c__DisplayClass34_0 CS$<>8__locals3 = CS$<>8__locals1;
      string text2 = "turn/started";
      JsonObject jsonObject = new JsonObject(null);
      string text3 = "turn";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["id"] = CS$<>8__locals1.turnId;
      jsonObject[text3] = jsonObject2;
      CS$<>8__locals3.<StartHarnessTurn>g__Note|0(text2, jsonObject);
      CS$<>8__locals1.answerItemId = null;
      CS$<>8__locals1.answerText = new StringBuilder();
      CS$<>8__locals1.finished = false;
      CS$<>8__locals1.runnerDidNotStart = false;
      CS$<>8__locals1.turnStats = new Dictionary<string, double>();
      CS$<>8__locals1.turnStartedAt = Stopwatch.StartNew();
      CS$<>8__locals1.emitGate = new object();
      Task.Run(delegate
      {
        CodexSession.<>c__DisplayClass34_0.<<StartHarnessTurn>b__2>d <<StartHarnessTurn>b__2>d;
        <<StartHarnessTurn>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartHarnessTurn>b__2>d.<>4__this = CS$<>8__locals1;
        <<StartHarnessTurn>b__2>d.<>1__state = -1;
        <<StartHarnessTurn>b__2>d.<>t__builder.Start<CodexSession.<>c__DisplayClass34_0.<<StartHarnessTurn>b__2>d>(ref <<StartHarnessTurn>b__2>d);
        return <<StartHarnessTurn>b__2>d.<>t__builder.Task;
      }, CancellationToken.None);
    }

    // Token: 0x060003FD RID: 1021 RVA: 0x000E9C3C File Offset: 0x000E9C3C
    [NullableContext(2)]
    public bool IsHarnessTurnRunning(string threadId)
    {
      AgentTask agentTask;
      if (threadId == null || !this.activeByThread.TryGetValue(threadId, out agentTask))
      {
        return false;
      }
      object gate = agentTask.Gate;
      bool flag2;
      lock (gate)
      {
        string turnId = agentTask.TurnId;
        flag2 = turnId != null && turnId.StartsWith("harness-turn-", StringComparison.Ordinal);
      }
      return flag2;
    }

    // Token: 0x060003FE RID: 1022 RVA: 0x000E9CA8 File Offset: 0x000E9CA8
    public async Task<bool> SteerHarnessTurnAsync(string appThreadId, string text, CancellationToken ct, bool informational = false, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> imagePaths = null)
    {
      string text2;
      if (this.harnessThreadIdByAppThreadId.TryGetValue(appThreadId, out text2) && this.activeByThread.ContainsKey(appThreadId))
      {
        ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessPreferences.RuntimeChoice(text2);
        if (valueTuple != null)
        {
          ValueTuple<AgentHarness, AgentHarnessPlace> valueOrDefault = valueTuple.GetValueOrDefault();
          IAgentHarnessRuntime agentHarnessRuntime = AgentHarnessRuntimes.Runtime(valueOrDefault.Item1, valueOrDefault.Item2);
          if (agentHarnessRuntime != null)
          {
            return await agentHarnessRuntime.SteerAsync(text2, text, informational, ct, imagePaths).ConfigureAwait(false);
          }
        }
      }
      return false;
    }

    // Token: 0x060003FF RID: 1023 RVA: 0x000E9D18 File Offset: 0x000E9D18
    private bool InterruptHarnessTurn(string appThreadId)
    {
      string text;
      if (this.harnessThreadIdByAppThreadId.TryGetValue(appThreadId, out text) && this.activeByThread.ContainsKey(appThreadId))
      {
        ValueTuple<AgentHarness, AgentHarnessPlace>? valueTuple = AgentHarnessPreferences.RuntimeChoice(text);
        if (valueTuple != null)
        {
          ValueTuple<AgentHarness, AgentHarnessPlace> valueOrDefault = valueTuple.GetValueOrDefault();
          IAgentHarnessRuntime agentHarnessRuntime = AgentHarnessRuntimes.Runtime(valueOrDefault.Item1, valueOrDefault.Item2);
          if (agentHarnessRuntime != null)
          {
            agentHarnessRuntime.InterruptAsync(text);
            return true;
          }
        }
      }
      return false;
    }

    // Token: 0x06000400 RID: 1024 RVA: 0x000E9D7C File Offset: 0x000E9D7C
    private static JsonObject HarnessToolItem(string id, string tool, [Nullable(2)] string server, [Nullable(2)] string command)
    {
      if (command != null)
      {
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["type"] = "commandExecution";
        jsonObject["id"] = id;
        jsonObject["command"] = command;
        return jsonObject;
      }
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["type"] = "mcpToolCall";
      jsonObject2["id"] = id;
      jsonObject2["tool"] = tool;
      jsonObject2["server"] = server ?? "files";
      return jsonObject2;
    }

    // Token: 0x06000401 RID: 1025 RVA: 0x000E9E34 File Offset: 0x000E9E34
    private static string Cap(string s, int n)
    {
      if (s.Length <= n)
      {
        return s;
      }
      return s.Substring(0, n);
    }

    // Token: 0x06000403 RID: 1027 RVA: 0x000E9E78 File Offset: 0x000E9E78
    [CompilerGenerated]
    internal static JsonObject <StartHarnessTurn>g__SearchItem|34_3(ref CodexSession.<>c__DisplayClass34_1 A_0)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["type"] = "webSearch";
      jsonObject["id"] = A_0.<search>5__2.Id;
      jsonObject["query"] = A_0.<search>5__2.Query;
      JsonObject jsonObject2 = jsonObject;
      string url = A_0.<search>5__2.Url;
      if (url != null)
      {
        JsonNode jsonNode = jsonObject2;
        string text = "action";
        JsonObject jsonObject3 = new JsonObject(null);
        jsonObject3["type"] = "openPage";
        jsonObject3["url"] = url;
        jsonNode[text] = jsonObject3;
      }
      return jsonObject2;
    }

    // Token: 0x0400016C RID: 364
    private readonly ConcurrentDictionary<string, AgentTask> activeByThread = new ConcurrentDictionary<string, AgentTask>();

    // Token: 0x0400016D RID: 365
    private readonly ConcurrentDictionary<string, byte> finishedTurnIds = new ConcurrentDictionary<string, byte>();

    // Token: 0x0400016E RID: 366
    [Nullable(new byte[] { 2, 1, 2 })]
    public Action<AgentTask, AgentTaskEventKind, string> OnTaskEvent;

    // Token: 0x0400016F RID: 367
    [Nullable(new byte[] { 2, 1, 2 })]
    public Action<AgentTask, TurnOutcome, string> OnTurnEnded;

    // Token: 0x04000170 RID: 368
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<AgentTask, string> OnIntegrationUsed;

    // Token: 0x04000171 RID: 369
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public Action<AgentTask, IReadOnlyList<AgentPlanStep>> OnTaskPlanUpdated;

    // Token: 0x04000172 RID: 370
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<AgentTask, string> OnTaskToolStep;

    // Token: 0x04000173 RID: 371
    [Nullable(new byte[] { 2, 1, 1 })]
    public Action<AgentTask, ClickyProgramBatch> OnTaskProgramBatch;

    // Token: 0x04000174 RID: 372
    [Nullable(new byte[] { 2, 1, 1, 1, 1 })]
    public Action<AgentTask, string, string, string> OnTaskFilePatchStreaming;

    // Token: 0x04000175 RID: 373
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public Action<AgentTask, IReadOnlyList<string>> OnTaskFileChangeCompleted;

    // Token: 0x04000176 RID: 374
    public const string HarnessTurnIdPrefix = "harness-turn-";

    // Token: 0x04000177 RID: 375
    private readonly ConcurrentDictionary<string, string> harnessThreadIdByAppThreadId = new ConcurrentDictionary<string, string>();

    // Token: 0x04000178 RID: 376
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public Action<AgentHarness, AgentHarnessPlace, string, double, IReadOnlyDictionary<string, double>> OnHarnessTurnEnded;

    // Token: 0x04000179 RID: 377
    [Nullable(new byte[] { 2, 1, 1, 1 })]
    public Action<string, string, string> OnHarnessInternalError;

    // Token: 0x0400017A RID: 378
    [Nullable(2)]
    public Action OnHarnessConnectedAppsUnreachable;

    // Token: 0x02000106 RID: 262
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040005C9 RID: 1481
      [Nullable(0)]
      public static Func<string, string> <0>__GetFileName;
    }
  }
}
