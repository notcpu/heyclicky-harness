using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F3 RID: 499
  [NullableContext(1)]
  [Nullable(0)]
  public static class BrainToolCallPolicy
  {
    // Token: 0x06001CF1 RID: 7409 RVA: 0x003E5220 File Offset: 0x003E5220
    public static bool RunsInParallel(string name)
    {
      return BrainToolCallPolicy.ParallelToolNames.Contains(name);
    }

    // Token: 0x06001CF2 RID: 7410 RVA: 0x003E5230 File Offset: 0x003E5230
    public static bool RunsAfterTheUserMovedOn(string name)
    {
      return name == "show_result";
    }

    // Token: 0x06001CF3 RID: 7411 RVA: 0x003E5240 File Offset: 0x003E5240
    public static bool IsJobDispatch(string name)
    {
      return BrainToolCallPolicy.JobDispatchToolNames.Contains(name);
    }

    // Token: 0x06001CF4 RID: 7412 RVA: 0x003E5250 File Offset: 0x003E5250
    public static bool BringsAnotherAppForward(string name, bool isAsync)
    {
      return isAsync && BrainToolCallPolicy.ScreenChangingAsyncToolNames.Contains(name);
    }

    // Token: 0x06001CF5 RID: 7413 RVA: 0x003E5264 File Offset: 0x003E5264
    public static bool IsCleanAsyncSuccess(string name, string output)
    {
      string[] array;
      if (!BrainToolCallPolicy.AsyncSuccessStatuses.TryGetValue(name, out array))
      {
        return false;
      }
      bool flag;
      try
      {
        using (JsonDocument jsonDocument = JsonDocument.Parse(output, default(JsonDocumentOptions)))
        {
          JsonElement rootElement = jsonDocument.RootElement;
          JsonElement jsonElement;
          if (rootElement.ValueKind != JsonValueKind.Object || !rootElement.TryGetProperty("status", out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
          {
            flag = false;
          }
          else
          {
            JsonElement jsonElement2;
            flag = array.Contains(jsonElement.GetString()) && !rootElement.TryGetProperty("message", out jsonElement2);
          }
        }
      }
      catch (JsonException)
      {
        flag = false;
      }
      return flag;
    }

    // Token: 0x06001CF6 RID: 7414 RVA: 0x003E531C File Offset: 0x003E531C
    public static bool NeedsAnotherRound(IReadOnlyList<BrainToolCall> calls, IReadOnlyList<string> outputs)
    {
      for (int i = 0; i < calls.Count; i++)
      {
        if (!calls[i].IsAsync || i >= outputs.Count || !BrainToolCallPolicy.IsCleanAsyncSuccess(calls[i].Name, outputs[i]))
        {
          return true;
        }
      }
      return false;
    }

    // Token: 0x06001CF7 RID: 7415 RVA: 0x003E5370 File Offset: 0x003E5370
    // Note: this type is marked as 'beforefieldinit'.
    static BrainToolCallPolicy()
    {
      Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
      dictionary["open_url"] = new string[] { "opened" };
      dictionary["play_on_spotify"] = new string[] { "completed" };
      dictionary["copy_to_clipboard"] = new string[] { "ok" };
      dictionary["insert_text"] = new string[] { "inserted" };
      dictionary["open_agent_dashboard"] = new string[] { "ok" };
      dictionary["open_artifact"] = new string[] { "completed" };
      dictionary["reveal_artifact"] = new string[] { "completed" };
      dictionary["sessions_spawn"] = new string[] { "dispatched" };
      dictionary["sessions_send"] = new string[] { "sent" };
      dictionary["show_result"] = new string[] { "shown" };
      dictionary["routines"] = new string[] { "created", "updated", "paused", "resumed", "deleted", "started" };
      BrainToolCallPolicy.AsyncSuccessStatuses = dictionary;
      BrainToolCallPolicy.ScreenChangingAsyncToolNames = new HashSet<string> { "open_url", "open_artifact", "reveal_artifact", "open_agent_dashboard", "play_on_spotify" };
      BrainToolCallPolicy.ToolsHeldDuringTheTour = new HashSet<string> { "sessions_spawn", "sessions_send", "shell_exec", "insert_text" };
      BrainToolCallPolicy.JobDispatchToolNames = new HashSet<string> { "sessions_spawn", "sessions_send", "sessions_answer_request" };
    }

    // Token: 0x04000C69 RID: 3177
    public static readonly IReadOnlySet<string> ParallelToolNames = new HashSet<string> { "file_read", "file_list", "clicky_memory", "session_status", "clicky_artifacts", "google_calendar_find_events", "find_jobs" };

    // Token: 0x04000C6A RID: 3178
    public static readonly IReadOnlyDictionary<string, string[]> AsyncSuccessStatuses;

    // Token: 0x04000C6B RID: 3179
    public static readonly IReadOnlySet<string> ScreenChangingAsyncToolNames;

    // Token: 0x04000C6C RID: 3180
    public static readonly IReadOnlySet<string> ToolsHeldDuringTheTour;

    // Token: 0x04000C6D RID: 3181
    public static readonly IReadOnlySet<string> JobDispatchToolNames;

    // Token: 0x04000C6E RID: 3182
    public const string ReportTurnDispatchRefusal = "{\"status\":\"refused\",\"message\":\"Not in a report turn: the user hasn't said anything yet. Nothing was sent. Tell them what came back and what's left, and ask whether to have it keep going; their answer sends it.\"}";
  }
}
