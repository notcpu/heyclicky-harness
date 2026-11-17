using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Browser
{
  // Token: 0x020001E1 RID: 481
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrowserTurnUsageTally
  {
    // Token: 0x06001BAF RID: 7087 RVA: 0x003DCB60 File Offset: 0x003DCB60
    public BrowserTurnUsageTally(Action<BrowserTurnUsage, int?> send, TimeSpan? tidyWait = null, [Nullable(2)] Func<DateTime> now = null)
    {
      this.send = send;
      this.tidyWait = tidyWait ?? BrowserTurnUsageTally.DefaultTidyWait;
      Func<DateTime> func = now;
      if (now == null && (func = BrowserTurnUsageTally.<>c.<>9__10_0) == null)
      {
        func = (BrowserTurnUsageTally.<>c.<>9__10_0 = () => DateTime.UtcNow);
      }
      this.now = func;
    }

    // Token: 0x06001BB0 RID: 7088 RVA: 0x003DCBFC File Offset: 0x003DCBFC
    public void NoteCommandSent(string task, string thread)
    {
      object obj = this.gate;
      lock (obj)
      {
        BrowserTurnUsage browserTurnUsage = this.Usage(task, thread);
        int commandsSentToBrowser = browserTurnUsage.CommandsSentToBrowser;
        browserTurnUsage.CommandsSentToBrowser = commandsSentToBrowser + 1;
      }
    }

    // Token: 0x06001BB1 RID: 7089 RVA: 0x003DCC50 File Offset: 0x003DCC50
    public void NoteTimeout(string task, string operation)
    {
      object obj = this.gate;
      lock (obj)
      {
        BrowserTurnUsage browserTurnUsage;
        if (this.turns.TryGetValue(task, out browserTurnUsage))
        {
          BrowserTurnUsage browserTurnUsage2 = browserTurnUsage;
          int timeoutCount = browserTurnUsage2.TimeoutCount;
          browserTurnUsage2.TimeoutCount = timeoutCount + 1;
          browserTurnUsage.TimeoutOperations[operation] = browserTurnUsage.TimeoutOperations.GetValueOrDefault(operation) + 1;
        }
      }
    }

    // Token: 0x06001BB2 RID: 7090 RVA: 0x003DCCC8 File Offset: 0x003DCCC8
    public void NoteAccessEndedByTimeout(string task)
    {
      object obj = this.gate;
      lock (obj)
      {
        BrowserTurnUsage browserTurnUsage;
        if (this.turns.TryGetValue(task, out browserTurnUsage))
        {
          browserTurnUsage.AccessEndedByTimeout = true;
        }
      }
    }

    // Token: 0x06001BB3 RID: 7091 RVA: 0x003DCD1C File Offset: 0x003DCD1C
    public void NoteStoppedByUser(IEnumerable<string> tasks)
    {
      object obj = this.gate;
      lock (obj)
      {
        foreach (string text in tasks)
        {
          BrowserTurnUsage browserTurnUsage;
          if (this.turns.TryGetValue(text, out browserTurnUsage))
          {
            browserTurnUsage.WasStoppedByUser = true;
          }
        }
      }
    }

    // Token: 0x06001BB4 RID: 7092 RVA: 0x003DCD9C File Offset: 0x003DCD9C
    [return: Nullable(2)]
    public string NoteCall(string task, string thread, JsonObject arguments, JsonObject result)
    {
      string text = BrowserTurnUsageTally.Str(arguments["operation"]) ?? ((arguments["steps"] is JsonArray) ? "batch" : "");
      string text2 = (ClickyBrowserBridge.AgentOperations.Contains(text) ? text : "other");
      object obj = this.gate;
      string text3;
      lock (obj)
      {
        if (!this.turns.ContainsKey(task) && this.turns.Count >= 64)
        {
          text3 = null;
        }
        else
        {
          BrowserTurnUsage browserTurnUsage = this.Usage(task, thread);
          BrowserTurnUsage browserTurnUsage2 = browserTurnUsage;
          int num = browserTurnUsage2.OperationCount;
          browserTurnUsage2.OperationCount = num + 1;
          browserTurnUsage.OperationCounts[text2] = browserTurnUsage.OperationCounts.GetValueOrDefault(text2) + 1;
          if (text2 == "batch")
          {
            BrowserTurnUsage browserTurnUsage3 = browserTurnUsage;
            int batchStepCount = browserTurnUsage3.BatchStepCount;
            JsonArray jsonArray = arguments["steps"] as JsonArray;
            browserTurnUsage3.BatchStepCount = batchStepCount + ((jsonArray != null) ? jsonArray.Count : 0);
          }
          string text4 = null;
          if (result.ContainsKey("error") && result["error"] != null)
          {
            BrowserTurnUsage browserTurnUsage4 = browserTurnUsage;
            num = browserTurnUsage4.ErrorCount;
            browserTurnUsage4.ErrorCount = num + 1;
            string text5 = BrowserTurnUsageTally.Str(result["error"]) ?? "";
            string text6 = BrowserErrorKinds.Kind(text5);
            browserTurnUsage.ErrorKinds[text6] = browserTurnUsage.ErrorKinds.GetValueOrDefault(text6) + 1;
            string text7 = BrowserErrorKinds.DetachReason(text5);
            if (text7 != null && browserTurnUsage.DetachReasons.Add(text7))
            {
              text4 = text7;
            }
          }
          bool flag3;
          bool flag2;
          if (text2 == "keep")
          {
            JsonValue jsonValue = result["kept"] as JsonValue;
            if (jsonValue != null)
            {
              flag2 = jsonValue.TryGetValue<bool>(out flag3);
              goto IL_01B3;
            }
          }
          flag2 = false;
          IL_01B3:
          if (flag2 && flag3 && !result.ContainsKey("note"))
          {
            BrowserTurnUsage browserTurnUsage5 = browserTurnUsage;
            num = browserTurnUsage5.TabsKept;
            browserTurnUsage5.TabsKept = num + 1;
          }
          if (BrowserTurnUsageTally.Str(result["saved"]) != null)
          {
            BrowserTurnUsage browserTurnUsage6 = browserTurnUsage;
            num = browserTurnUsage6.ScreenshotsSaved;
            browserTurnUsage6.ScreenshotsSaved = num + 1;
          }
          text3 = text4;
        }
      }
      return text3;
    }

    // Token: 0x06001BB5 RID: 7093 RVA: 0x003DCFD8 File Offset: 0x003DCFD8
    public void Finish(string task, IReadOnlyList<string> answerableTidyCommandIds, bool hasPeers)
    {
      BrowserTurnUsageTally.<>c__DisplayClass16_0 CS$<>8__locals1 = new BrowserTurnUsageTally.<>c__DisplayClass16_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.task = task;
      BrowserTurnUsage browserTurnUsage = null;
      int? num = null;
      object obj = this.gate;
      lock (obj)
      {
        BrowserTurnUsage browserTurnUsage2;
        if (!this.turns.Remove(CS$<>8__locals1.task, out browserTurnUsage2))
        {
          return;
        }
        browserTurnUsage2.EndedAt = new DateTime?(this.now());
        if (browserTurnUsage2.CommandsSentToBrowser == 0 || answerableTidyCommandIds.Count == 0 || !hasPeers)
        {
          browserTurnUsage = browserTurnUsage2;
          num = ((browserTurnUsage2.CommandsSentToBrowser > 0) ? null : new int?(0));
        }
        else
        {
          this.awaitingTidy[CS$<>8__locals1.task] = browserTurnUsage2;
          this.tidyRepliesAwaited[CS$<>8__locals1.task] = new ValueTuple<int, int>(answerableTidyCommandIds.Count, 0);
          foreach (string text in answerableTidyCommandIds)
          {
            this.tidyCommandTurns[text] = CS$<>8__locals1.task;
          }
        }
      }
      if (browserTurnUsage != null)
      {
        this.send(browserTurnUsage, num);
        return;
      }
      CS$<>8__locals1.ids = answerableTidyCommandIds.ToList<string>();
      CS$<>8__locals1.<Finish>g__ExpireAsync|0();
    }

    // Token: 0x06001BB6 RID: 7094 RVA: 0x003DD134 File Offset: 0x003DD134
    public bool NoteTidyReply(string commandId, int tabsClosed)
    {
      int? num = null;
      object obj = this.gate;
      string text;
      lock (obj)
      {
        if (!this.tidyCommandTurns.Remove(commandId, out text))
        {
          return false;
        }
        ValueTuple<int, int> valueTuple;
        if (!this.tidyRepliesAwaited.TryGetValue(text, out valueTuple))
        {
          return true;
        }
        valueTuple = new ValueTuple<int, int>(valueTuple.Item1 - 1, valueTuple.Item2 + tabsClosed);
        this.tidyRepliesAwaited[text] = valueTuple;
        if (valueTuple.Item1 <= 0)
        {
          num = new int?(valueTuple.Item2);
        }
      }
      if (num != null)
      {
        this.SendAwaitingTidy(text, num);
      }
      return true;
    }

    // Token: 0x06001BB7 RID: 7095 RVA: 0x003DD1F4 File Offset: 0x003DD1F4
    [NullableContext(2)]
    public static int TabsClosed(JsonNode result)
    {
      JsonObject jsonObject = result as JsonObject;
      JsonArray jsonArray = ((jsonObject != null) ? jsonObject["closed"] : null) as JsonArray;
      if (jsonArray == null)
      {
        return 0;
      }
      return jsonArray.Count;
    }

    // Token: 0x06001BB8 RID: 7096 RVA: 0x003DD22C File Offset: 0x003DD22C
    private void SendAwaitingTidy(string task, int? tabsClosed)
    {
      object obj = this.gate;
      BrowserTurnUsage browserTurnUsage;
      lock (obj)
      {
        this.tidyRepliesAwaited.Remove(task);
        if (!this.awaitingTidy.Remove(task, out browserTurnUsage))
        {
          return;
        }
      }
      this.send(browserTurnUsage, tabsClosed);
    }

    // Token: 0x06001BB9 RID: 7097 RVA: 0x003DD294 File Offset: 0x003DD294
    private BrowserTurnUsage Usage(string task, string thread)
    {
      BrowserTurnUsage browserTurnUsage;
      if (!this.turns.TryGetValue(task, out browserTurnUsage))
      {
        browserTurnUsage = (this.turns[task] = new BrowserTurnUsage(thread, this.now()));
      }
      return browserTurnUsage;
    }

    // Token: 0x06001BBA RID: 7098 RVA: 0x003DD2D4 File Offset: 0x003DD2D4
    public static JsonObject Properties(BrowserTurnUsage usage, int? tabsClosed, DateTime now)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["operation_count"] = usage.OperationCount;
      jsonObject["batch_step_count"] = usage.BatchStepCount;
      jsonObject["commands_sent_to_browser"] = usage.CommandsSentToBrowser;
      jsonObject["reached_browser"] = usage.CommandsSentToBrowser > 0;
      jsonObject["error_count"] = usage.ErrorCount;
      jsonObject["timeout_count"] = usage.TimeoutCount;
      jsonObject["tabs_kept"] = usage.TabsKept;
      jsonObject["screenshots_saved"] = usage.ScreenshotsSaved;
      jsonObject["was_stopped_by_user"] = usage.WasStoppedByUser;
      jsonObject["access_ended_by_timeout"] = usage.AccessEndedByTimeout;
      jsonObject["browser_seconds"] = (int)Math.Round((usage.EndedAt.GetValueOrDefault(now) - usage.FirstCallAt).TotalSeconds, MidpointRounding.AwayFromZero);
      jsonObject["tabs_closed_known"] = tabsClosed != null;
      JsonObject jsonObject2 = jsonObject;
      if (tabsClosed != null)
      {
        int valueOrDefault = tabsClosed.GetValueOrDefault();
        jsonObject2["tabs_closed"] = valueOrDefault;
      }
      foreach (KeyValuePair<string, int> keyValuePair in usage.OperationCounts)
      {
        string text;
        int num;
        keyValuePair.Deconstruct(out text, out num);
        string text2 = text;
        int num2 = num;
        jsonObject2["op_" + text2] = num2;
      }
      foreach (KeyValuePair<string, int> keyValuePair in usage.ErrorKinds)
      {
        string text;
        int num;
        keyValuePair.Deconstruct(out text, out num);
        string text3 = text;
        int num3 = num;
        jsonObject2["error_" + text3] = num3;
      }
      foreach (KeyValuePair<string, int> keyValuePair in usage.TimeoutOperations)
      {
        string text;
        int num;
        keyValuePair.Deconstruct(out text, out num);
        string text4 = text;
        int num4 = num;
        jsonObject2["timeout_" + text4] = num4;
      }
      if (usage.DetachReasons.Count > 0)
      {
        jsonObject2["detach_reasons"] = string.Join(",", usage.DetachReasons);
      }
      return jsonObject2;
    }

    // Token: 0x06001BBB RID: 7099 RVA: 0x003DD5B0 File Offset: 0x003DD5B0
    [NullableContext(2)]
    private static string Str(JsonNode node)
    {
      JsonValue jsonValue = node as JsonValue;
      if (jsonValue == null || jsonValue.GetValueKind() != JsonValueKind.String)
      {
        return null;
      }
      return jsonValue.GetValue<string>();
    }

    // Token: 0x04000BCE RID: 3022
    public const int MaxTurns = 64;

    // Token: 0x04000BCF RID: 3023
    public static readonly TimeSpan DefaultTidyWait = TimeSpan.FromSeconds(20L);

    // Token: 0x04000BD0 RID: 3024
    private readonly object gate = new object();

    // Token: 0x04000BD1 RID: 3025
    private readonly Action<BrowserTurnUsage, int?> send;

    // Token: 0x04000BD2 RID: 3026
    private readonly TimeSpan tidyWait;

    // Token: 0x04000BD3 RID: 3027
    private readonly Func<DateTime> now;

    // Token: 0x04000BD4 RID: 3028
    private readonly Dictionary<string, BrowserTurnUsage> turns = new Dictionary<string, BrowserTurnUsage>();

    // Token: 0x04000BD5 RID: 3029
    private readonly Dictionary<string, BrowserTurnUsage> awaitingTidy = new Dictionary<string, BrowserTurnUsage>();

    // Token: 0x04000BD6 RID: 3030
    private readonly Dictionary<string, string> tidyCommandTurns = new Dictionary<string, string>();

    // Token: 0x04000BD7 RID: 3031
    [TupleElementNames(new string[] { "Remaining", "TabsClosed" })]
    [Nullable(new byte[] { 1, 1, 0 })]
    private readonly Dictionary<string, ValueTuple<int, int>> tidyRepliesAwaited = new Dictionary<string, ValueTuple<int, int>>();
  }
}
