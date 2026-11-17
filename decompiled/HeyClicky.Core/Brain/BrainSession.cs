using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Auth;
using HeyClicky.Core.Configuration;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F7 RID: 503
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class BrainSession : IDisposable
  {
    // Token: 0x17000669 RID: 1641
    // (get) Token: 0x06001D0A RID: 7434 RVA: 0x003E5B28 File Offset: 0x003E5B28
    // (set) Token: 0x06001D0B RID: 7435 RVA: 0x003E5B30 File Offset: 0x003E5B30
    public string SessionId { get; private set; } = Guid.NewGuid().ToString().ToUpperInvariant();

    // Token: 0x06001D0C RID: 7436 RVA: 0x003E5B3C File Offset: 0x003E5B3C
    [NullableContext(2)]
    public BrainSession([Nullable(1)] BrainClient client, [Nullable(new byte[] { 1, 1, 2 })] Func<CancellationToken, Task<string>> accessToken, string rootDirectory = null, Func<DateTimeOffset> clock = null)
    {
      this.client = client;
      this.accessToken = accessToken;
      this.rootDirectory = rootDirectory ?? Path.Combine(AppBundleConfiguration.AppDataDirectory, "OneBrain");
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = BrainSession.<>c.<>9__37_0) == null)
      {
        func = (BrainSession.<>c.<>9__37_0 = () => DateTimeOffset.UtcNow);
      }
      this.now = func;
    }

    // Token: 0x1700066A RID: 1642
    // (get) Token: 0x06001D0D RID: 7437 RVA: 0x003E5C24 File Offset: 0x003E5C24
    public int ThreadCount
    {
      get
      {
        object obj = this.gate;
        int count;
        lock (obj)
        {
          count = this.thread.Count;
        }
        return count;
      }
    }

    // Token: 0x1700066B RID: 1643
    // (get) Token: 0x06001D0E RID: 7438 RVA: 0x003E5C6C File Offset: 0x003E5C6C
    [Nullable(2)]
    public string UserLayer
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.userLayer;
        }
        return text;
      }
    }

    // Token: 0x1700066C RID: 1644
    // (get) Token: 0x06001D0F RID: 7439 RVA: 0x003E5CB0 File Offset: 0x003E5CB0
    [Nullable(2)]
    public string UserLayerSignature
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        string text;
        lock (obj)
        {
          text = this.userLayerSignature;
        }
        return text;
      }
    }

    // Token: 0x06001D10 RID: 7440 RVA: 0x003E5CF4 File Offset: 0x003E5CF4
    public List<JsonNode> ThreadSnapshot()
    {
      object obj = this.gate;
      List<JsonNode> list;
      lock (obj)
      {
        list = this.thread.ToList<JsonNode>();
      }
      return list;
    }

    // Token: 0x06001D11 RID: 7441 RVA: 0x003E5D3C File Offset: 0x003E5D3C
    public List<JsonNode> ThreadSnapshotForWorker()
    {
      object obj = this.gate;
      List<JsonNode> list;
      lock (obj)
      {
        list = BrainSession.ThreadForWorker(this.thread, this.screenshotFilesOffForSession);
      }
      return list;
    }

    // Token: 0x1700066D RID: 1645
    // (get) Token: 0x06001D12 RID: 7442 RVA: 0x003E5D8C File Offset: 0x003E5D8C
    public DateTimeOffset PreviousTurnAt
    {
      get
      {
        object obj = this.gate;
        DateTimeOffset dateTimeOffset;
        lock (obj)
        {
          dateTimeOffset = this.previousTurnAt;
        }
        return dateTimeOffset;
      }
    }

    // Token: 0x06001D13 RID: 7443 RVA: 0x003E5DD0 File Offset: 0x003E5DD0
    public void TurnOffScreenshotFilesForSession()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.screenshotFilesOffForSession = true;
      }
      Log.Info("OneBrain", "screenshots by reference refused: sending them in full for the rest of this session");
    }

    // Token: 0x06001D14 RID: 7444 RVA: 0x003E5E20 File Offset: 0x003E5E20
    private void PublishStats()
    {
      OneBrainStats shared = OneBrainStats.Shared;
      if (!shared.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      OneBrainStats.Session snapshot;
      lock (obj)
      {
        snapshot = new OneBrainStats.Session
        {
          Id = this.SessionId,
          StartedAt = new DateTimeOffset?(this.sessionStartedAt),
          LastTurnAt = new DateTimeOffset?(this.lastTurnAt),
          ThreadItems = this.thread.Count,
          ScreenshotsInThread = BrainSession.ScreenshotCount(this.thread),
          LastInputTokens = this.lastTurnInputTokens,
          BaselineInputTokens = this.sessionBaselineInputTokens,
          NextCompactionCheckInputTokens = this.compactionNextCheckInputTokens,
          IsCompacting = this.isCompacting,
          TurnCount = this.statsTurnCount
        };
      }
      shared.UpdateSession((OneBrainStats.Session _) => snapshot);
    }

    // Token: 0x06001D15 RID: 7445 RVA: 0x003E5F18 File Offset: 0x003E5F18
    [NullableContext(2)]
    public void AdoptAccount(string userId)
    {
      if (string.IsNullOrEmpty(userId))
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        if (!(userId == this.accountId))
        {
          if (this.accountId != null)
          {
            Log.Info("OneBrain", "another account signed in: starting clean");
            this.ResetSessionStateLocked();
          }
          this.accountId = userId;
          this.hasLookedForSavedSession = false;
        }
      }
    }

    // Token: 0x06001D16 RID: 7446 RVA: 0x003E5F98 File Offset: 0x003E5F98
    public void ResetForSignOut()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.ResetSessionStateLocked();
        this.accountId = null;
        this.hasLookedForSavedSession = false;
      }
    }

    // Token: 0x06001D17 RID: 7447 RVA: 0x003E5FE8 File Offset: 0x003E5FE8
    private void ResetSessionStateLocked()
    {
      Timer timer = this.idleSessionEndTimer;
      if (timer != null)
      {
        timer.Dispose();
      }
      this.idleSessionEndTimer = null;
      this.SessionId = BrainSession.NewSessionId();
      this.userLayer = null;
      this.userLayerSignature = null;
      this.thread.Clear();
      this.codeOfDrawingOnScreen.Clear();
      this.pendingAppNotes.Clear();
      this.lastTurnInputTokens = 0;
      this.sessionBaselineInputTokens = 0;
      this.sessionScreenshotFiles.Clear();
      this.screenshotFilesOffForSession = false;
      this.isFirstKeyDownAfterReload = false;
      this.compactionGeneration++;
      this.compactionNextCheckInputTokens = 0;
      this.lastTurnAt = DateTimeOffset.MinValue;
      this.sessionStartedAt = DateTimeOffset.MinValue;
      this.environmentSentForSessionId = null;
    }

    // Token: 0x06001D18 RID: 7448 RVA: 0x003E60A0 File Offset: 0x003E60A0
    private static string NewSessionId()
    {
      return Guid.NewGuid().ToString().ToUpperInvariant();
    }

    // Token: 0x06001D19 RID: 7449 RVA: 0x003E60C8 File Offset: 0x003E60C8
    public void NoteForNextTurn(string note, [Nullable(2)] string latestForKey = null)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (latestForKey != null)
        {
          this.pendingAppNotes.RemoveAll(([TupleElementNames(new string[] { "Key", "Text" })] [Nullable(new byte[] { 0, 2, 1 })] ValueTuple<string, string> n) => n.Item1 == latestForKey);
        }
        this.pendingAppNotes.Add(new ValueTuple<string, string>(latestForKey, note));
      }
    }

    // Token: 0x06001D1A RID: 7450 RVA: 0x003E6148 File Offset: 0x003E6148
    public void DropNoteForNextTurn(string key)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.pendingAppNotes.RemoveAll(([TupleElementNames(new string[] { "Key", "Text" })] [Nullable(new byte[] { 0, 2, 1 })] ValueTuple<string, string> n) => n.Item1 == key);
      }
    }

    // Token: 0x06001D1B RID: 7451 RVA: 0x003E61A8 File Offset: 0x003E61A8
    [NullableContext(0)]
    public void NoteKeyDown(bool usesOpenAIBrain, [TupleElementNames(new string[] { "Width", "Height" })] ValueTuple<int, int> cursorScreenCanvas, [Nullable(2)] BrainRequestFlags flags = null)
    {
      BrainSession.<>c__DisplayClass56_0 CS$<>8__locals1 = new BrainSession.<>c__DisplayClass56_0();
      CS$<>8__locals1.<>4__this = this;
      this.StartNewSessionIfIdle();
      object obj = this.gate;
      lock (obj)
      {
        CS$<>8__locals1.conversationTokens = BrainPrewarmPolicy.ConversationTokens(this.lastTurnInputTokens, this.sessionBaselineInputTokens);
        if (!BrainPrewarmPolicy.ShouldPrewarm(this.thread.Count, this.isFirstKeyDownAfterReload, this.now() - this.lastTurnAt, CS$<>8__locals1.conversationTokens, usesOpenAIBrain, this.now() - this.lastPrewarmAt) || this.userLayer == null || this.userLayerSignature == null)
        {
          return;
        }
        this.lastPrewarmAt = this.now();
        CS$<>8__locals1.layer = this.userLayer;
        CS$<>8__locals1.signature = this.userLayerSignature;
        CS$<>8__locals1.sessionId = this.SessionId;
        CS$<>8__locals1.threadCopy = BrainSession.ThreadForWorker(this.thread, this.screenshotFilesOffForSession);
        CS$<>8__locals1.canvas = this.lastCanvasSize.GetValueOrDefault(cursorScreenCanvas);
      }
      CS$<>8__locals1.requestFlags = flags ?? BrainRequestFlags.None;
      CS$<>8__locals1.idleMinutes = (int)(this.now() - this.lastTurnAt).TotalMinutes;
      Task.Run(delegate
      {
        BrainSession.<>c__DisplayClass56_0.<<NoteKeyDown>b__0>d <<NoteKeyDown>b__0>d;
        <<NoteKeyDown>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<NoteKeyDown>b__0>d.<>4__this = CS$<>8__locals1;
        <<NoteKeyDown>b__0>d.<>1__state = -1;
        <<NoteKeyDown>b__0>d.<>t__builder.Start<BrainSession.<>c__DisplayClass56_0.<<NoteKeyDown>b__0>d>(ref <<NoteKeyDown>b__0>d);
        return <<NoteKeyDown>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06001D1C RID: 7452 RVA: 0x003E6314 File Offset: 0x003E6314
    public void BeginTurnStartingNewSessionIfIdle()
    {
      this.StartNewSessionIfIdle();
      object obj = this.gate;
      lock (obj)
      {
        this.previousTurnAt = this.lastTurnAt;
        this.lastTurnAt = this.now();
        this.statsTurnCount++;
        this.ScheduleIdleSessionEndLocked();
      }
      this.PublishStats();
    }

    // Token: 0x06001D1D RID: 7453 RVA: 0x003E638C File Offset: 0x003E638C
    private void ScheduleIdleSessionEndLocked()
    {
      Timer timer = this.idleSessionEndTimer;
      if (timer != null)
      {
        timer.Dispose();
      }
      TimeSpan timeSpan = BrainSession.IdleBeforeNewSession + TimeSpan.FromSeconds(60L);
      this.idleSessionEndTimer = new Timer(delegate(object _)
      {
        this.StartNewSessionIfIdle();
      }, null, timeSpan, Timeout.InfiniteTimeSpan);
    }

    // Token: 0x06001D1E RID: 7454 RVA: 0x003E63DC File Offset: 0x003E63DC
    public void StartNewSessionIfIdle()
    {
      object obj = this.gate;
      BrainSession.Ended ended;
      lock (obj)
      {
        if (!this.hasLookedForSavedSession)
        {
          this.hasLookedForSavedSession = true;
          this.RestoreSavedSessionIfRecentLocked();
        }
        DateTimeOffset dateTimeOffset = ((this.lastTurnAt > this.sessionStartedAt) ? this.lastTurnAt : this.sessionStartedAt);
        if (this.now() - dateTimeOffset <= BrainSession.IdleBeforeNewSession)
        {
          return;
        }
        ended = this.BeginNewSessionLocked();
      }
      this.AfterNewSession(ended);
    }

    // Token: 0x06001D1F RID: 7455 RVA: 0x003E647C File Offset: 0x003E647C
    [NullableContext(2)]
    public void StartFreshSession(string userId)
    {
      this.AdoptAccount(userId);
      object obj = this.gate;
      BrainSession.Ended ended;
      lock (obj)
      {
        if (!this.hasLookedForSavedSession)
        {
          this.hasLookedForSavedSession = true;
          this.RestoreSavedSessionIfRecentLocked();
        }
        ended = this.BeginNewSessionLocked();
        this.pendingAppNotes.Clear();
        this.environmentSentForSessionId = null;
        this.lastTurnAt = this.now();
        this.ScheduleIdleSessionEndLocked();
      }
      OneBrainStats.Shared.Note("fresh session (Settings)");
      this.AfterNewSession(ended);
      this.SaveToDisk();
    }

    // Token: 0x06001D20 RID: 7456 RVA: 0x003E6520 File Offset: 0x003E6520
    private BrainSession.Ended BeginNewSessionLocked()
    {
      Timer timer = this.idleSessionEndTimer;
      if (timer != null)
      {
        timer.Dispose();
      }
      this.idleSessionEndTimer = null;
      BrainSession.Ended ended = new BrainSession.Ended(this.SessionId, BrainSession.WithoutScreenshots(this.thread), this.sessionScreenshotFiles.ToList<string>());
      this.sessionScreenshotFiles.Clear();
      this.screenshotFilesOffForSession = false;
      this.SessionId = BrainSession.NewSessionId();
      this.userLayer = null;
      this.userLayerSignature = null;
      this.thread.Clear();
      this.codeOfDrawingOnScreen.Clear();
      this.lastTurnInputTokens = 0;
      this.sessionBaselineInputTokens = 0;
      this.isFirstKeyDownAfterReload = false;
      this.compactionGeneration++;
      this.compactionNextCheckInputTokens = 0;
      this.sessionStartedAt = this.now();
      this.statsTurnCount = 0;
      Log.Info("OneBrain", "new session " + this.SessionId);
      return ended;
    }

    // Token: 0x06001D21 RID: 7457 RVA: 0x003E6604 File Offset: 0x003E6604
    [NullableContext(2)]
    private void AfterNewSession(BrainSession.Ended ended)
    {
      object obj = this.gate;
      string sessionId;
      lock (obj)
      {
        sessionId = this.SessionId;
      }
      OneBrainStats.Shared.ResetForNewSession();
      OneBrainStats.Shared.Note("new session " + ((sessionId.Length > 8) ? sessionId.Substring(0, 8) : sessionId));
      this.PublishStats();
      if (ended != null)
      {
        List<JsonNode> list = ended.Thread;
        if (list != null && list.Count > 0)
        {
          this.EndSessionOnWorker(ended.SessionId, ended.Thread, ended.ScreenshotFiles);
        }
      }
    }

    // Token: 0x06001D22 RID: 7458 RVA: 0x003E66AC File Offset: 0x003E66AC
    private void EndSessionOnWorker(string endedSessionId, List<JsonNode> endedThread, List<string> screenshotFiles)
    {
      BrainSession.<>c__DisplayClass64_0 CS$<>8__locals1 = new BrainSession.<>c__DisplayClass64_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.endedSessionId = endedSessionId;
      CS$<>8__locals1.endedThread = endedThread;
      CS$<>8__locals1.screenshotFiles = screenshotFiles;
      Task.Run(delegate
      {
        BrainSession.<>c__DisplayClass64_0.<<EndSessionOnWorker>b__0>d <<EndSessionOnWorker>b__0>d;
        <<EndSessionOnWorker>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<EndSessionOnWorker>b__0>d.<>4__this = CS$<>8__locals1;
        <<EndSessionOnWorker>b__0>d.<>1__state = -1;
        <<EndSessionOnWorker>b__0>d.<>t__builder.Start<BrainSession.<>c__DisplayClass64_0.<<EndSessionOnWorker>b__0>d>(ref <<EndSessionOnWorker>b__0>d);
        return <<EndSessionOnWorker>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06001D23 RID: 7459 RVA: 0x003E66E0 File Offset: 0x003E66E0
    [NullableContext(0)]
    [return: Nullable(1)]
    public BrainTurnContext BeginRequest([TupleElementNames(new string[] { "Width", "Height" })] ValueTuple<int, int> canvas, bool hasDrawingOnScreen, bool screenless = false)
    {
      object obj = this.gate;
      BrainTurnContext brainTurnContext;
      lock (obj)
      {
        bool flag2 = this.thread.Count == 0;
        foreach (ValueTuple<string, string> valueTuple in this.pendingAppNotes)
        {
          this.thread.Add(BrainSession.Message("developer", valueTuple.Item2));
        }
        this.pendingAppNotes.Clear();
        if (!screenless)
        {
          this.lastCanvasSize = new ValueTuple<int, int>?(canvas);
          if (!hasDrawingOnScreen)
          {
            this.codeOfDrawingOnScreen.Clear();
          }
        }
        bool flag3 = this.environmentSentForSessionId != this.SessionId;
        if (flag3)
        {
          this.environmentSentForSessionId = this.SessionId;
        }
        brainTurnContext = new BrainTurnContext(this.SessionId, flag2, flag3, hasDrawingOnScreen ? this.DrawingCodeMissingFromThreadLocked() : null);
      }
      return brainTurnContext;
    }

    // Token: 0x06001D24 RID: 7460 RVA: 0x003E67E8 File Offset: 0x003E67E8
    public void AdoptUserLayer(string layer, [Nullable(2)] string signature)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.userLayer == null)
        {
          this.userLayer = layer;
          this.userLayerSignature = signature;
        }
      }
    }

    // Token: 0x06001D25 RID: 7461 RVA: 0x003E683C File Offset: 0x003E683C
    [return: TupleElementNames(new string[] { "Role", "Text" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public List<ValueTuple<string, string>> RecentConversation(int limit)
    {
      object obj = this.gate;
      List<JsonNode> list;
      lock (obj)
      {
        list = this.thread.ToList<JsonNode>();
      }
      List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>();
      foreach (JsonNode jsonNode in list)
      {
        JsonObject jsonObject = jsonNode as JsonObject;
        if (jsonObject != null)
        {
          string text = (string)jsonObject["type"];
          if (text == null || !(text != "message"))
          {
            string text2 = (string)jsonObject["role"];
            bool flag = text2 == "user" || text2 == "assistant";
            if (flag)
            {
              JsonNode jsonNode2 = jsonObject["content"];
              JsonValue jsonValue = jsonNode2 as JsonValue;
              string text3;
              if (jsonValue == null)
              {
                JsonArray jsonArray = jsonNode2 as JsonArray;
                if (jsonArray == null)
                {
                  text3 = "";
                }
                else
                {
                  text3 = string.Join("\n", from p in jsonArray.OfType<JsonObject>()
                    select (string)p["text"] into t
                    where t != null
                    select t);
                }
              }
              else
              {
                text3 = ((string)jsonValue) ?? "";
              }
              string text4 = text3;
              if (text2 == "assistant")
              {
                List<string> list3 = SayLines.InOrder(text4);
                if (list3.Count == 0)
                {
                  continue;
                }
                text4 = string.Join(" ", list3);
              }
              else if (text4.StartsWith("[This turn]", StringComparison.Ordinal))
              {
                continue;
              }
              if (!string.IsNullOrWhiteSpace(text4))
              {
                list2.Add(new ValueTuple<string, string>(text2, text4.Trim()));
              }
            }
          }
        }
      }
      return list2.TakeLast<ValueTuple<string, string>>(limit).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x06001D26 RID: 7462 RVA: 0x003E6A5C File Offset: 0x003E6A5C
    public void NoteBaselineInputTokens(int firstInput)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.sessionBaselineInputTokens == 0 && firstInput > 0)
        {
          this.sessionBaselineInputTokens = firstInput;
        }
      }
    }

    // Token: 0x06001D27 RID: 7463 RVA: 0x003E6AAC File Offset: 0x003E6AAC
    [NullableContext(2)]
    public static bool IsSafetyRefusal(string failure)
    {
      return failure != null && failure.Contains("cyber_policy", StringComparison.Ordinal);
    }

    // Token: 0x06001D28 RID: 7464 RVA: 0x003E6AC0 File Offset: 0x003E6AC0
    public unsafe bool CommitTurn(string sessionIdAtStart, string note, string said, [Nullable(2)] string screenshotDataUrl, IReadOnlyList<JsonNode> items, IReadOnlyList<string> searchRecords, bool wasInterrupted, bool keepReasoning, [Nullable(2)] string drawingCode, [Nullable(2)] string screenshotFile = null, bool refusedBySafetyFilter = false)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.SessionId != sessionIdAtStart)
        {
          Log.Info("OneBrain", "the session changed during this turn (sign-out, another account or a fresh session): not kept");
          return false;
        }
        this.isFirstKeyDownAfterReload = false;
        this.thread.Add(BrainSession.Message("developer", note));
        if (screenshotDataUrl != null)
        {
          JsonObject jsonObject = new JsonObject(null);
          jsonObject["type"] = "input_image";
          jsonObject["image_url"] = screenshotDataUrl;
          JsonObject jsonObject2 = jsonObject;
          if (screenshotFile != null)
          {
            jsonObject2["clicky_file"] = screenshotFile;
            this.sessionScreenshotFiles.Add(screenshotFile);
          }
          List<JsonNode> list = this.thread;
          JsonObject jsonObject3 = new JsonObject(null);
          jsonObject3["type"] = "message";
          jsonObject3["role"] = "user";
          string text = "content";
          <>y__InlineArray2<JsonNode> <>y__InlineArray = default(<>y__InlineArray2<JsonNode>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 0) = jsonObject2;
          ref JsonNode ptr = ref <PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 1);
          JsonObject jsonObject4 = new JsonObject(null);
          jsonObject4["type"] = "input_text";
          jsonObject4["text"] = said;
          ptr = jsonObject4;
          jsonObject3[text] = new JsonArray(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<JsonNode>, JsonNode>(in <>y__InlineArray, 2));
          list.Add(jsonObject3);
          if (BrainSession.ScreenshotCount(this.thread) > 48)
          {
            List<JsonNode> list2 = BrainSession.WithNewestScreenshots(this.thread, 32);
            this.thread.Clear();
            this.thread.AddRange(list2);
            string text2 = "OneBrain";
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(54, 2);
            defaultInterpolatedStringHandler.AppendLiteral("more than ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(48);
            defaultInterpolatedStringHandler.AppendLiteral(" screenshots in the thread: kept the newest ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(32);
            Log.Info(text2, defaultInterpolatedStringHandler.ToStringAndClear());
          }
        }
        else
        {
          this.thread.Add(BrainSession.Message("user", said));
        }
        foreach (string text3 in searchRecords)
        {
          if (text3.Length > 0)
          {
            this.thread.Add(BrainSession.Message("developer", text3));
          }
        }
        HashSet<string> hashSet = (from i in items
          where (string)i["type"] == "function_call_output"
          select (string)i["call_id"]).ToHashSet<string>();
        foreach (JsonNode jsonNode in items)
        {
          string text4 = (string)jsonNode["type"];
          if (!(text4 == "message"))
          {
            if (!(text4 == "function_call"))
            {
              if (!(text4 == "function_call_output"))
              {
                if (text4 == "reasoning")
                {
                  if (keepReasoning)
                  {
                    string text5 = (string)jsonNode["encrypted_content"];
                    if (text5 != null && text5.Length > 0)
                    {
                      JsonObject jsonObject5 = new JsonObject(null);
                      jsonObject5["type"] = "reasoning";
                      jsonObject5["summary"] = new JsonArray(null);
                      jsonObject5["encrypted_content"] = text5;
                      JsonObject jsonObject6 = jsonObject5;
                      string text6 = (string)jsonNode["id"];
                      if (text6 != null && text6.Length > 0)
                      {
                        jsonObject6["id"] = text6;
                      }
                      this.thread.Add(jsonObject6);
                    }
                  }
                }
              }
              else
              {
                List<JsonNode> list3 = this.thread;
                JsonObject jsonObject7 = new JsonObject(null);
                jsonObject7["type"] = "function_call_output";
                jsonObject7["call_id"] = ((string)jsonNode["call_id"]) ?? "";
                jsonObject7["output"] = ((string)jsonNode["output"]) ?? "";
                list3.Add(jsonObject7);
              }
            }
            else if (hashSet.Contains((string)jsonNode["call_id"]))
            {
              List<JsonNode> list4 = this.thread;
              JsonObject jsonObject8 = new JsonObject(null);
              jsonObject8["type"] = "function_call";
              jsonObject8["call_id"] = ((string)jsonNode["call_id"]) ?? "";
              jsonObject8["name"] = ((string)jsonNode["name"]) ?? "";
              jsonObject8["arguments"] = ((string)jsonNode["arguments"]) ?? "{}";
              list4.Add(jsonObject8);
            }
          }
          else if (!((string)jsonNode["role"] != "assistant"))
          {
            JsonArray jsonArray = jsonNode["content"] as JsonArray;
            List<string> list5;
            if (jsonArray == null)
            {
              list5 = null;
            }
            else
            {
              list5 = (from c in jsonArray
                select (string)((c != null) ? c["text"] : null) into t
                where t != null
                select t).ToList<string>();
            }
            List<string> list6 = list5 ?? new List<string>();
            if (list6.Count != 0)
            {
              JsonObject jsonObject9 = new JsonObject(null);
              jsonObject9["type"] = "message";
              jsonObject9["role"] = "assistant";
              string text7 = "content";
              JsonObject jsonObject10 = new JsonObject(null);
              jsonObject10["type"] = "output_text";
              jsonObject10["text"] = string.Concat(list6);
              JsonNode jsonNode2 = jsonObject10;
              jsonObject9[text7] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode2));
              JsonObject jsonObject11 = jsonObject9;
              string text8 = (string)jsonNode["phase"];
              if (text8 != null && text8.Length > 0)
              {
                jsonObject11["phase"] = text8;
              }
              this.thread.Add(jsonObject11);
            }
          }
        }
        if (refusedBySafetyFilter)
        {
          this.thread.Add(BrainSession.Message("developer", "[App] The provider's safety filter refused this answer because it looked like hands-on security work, and the user was told so. If they ask why, say that plainly and offer to explain the concepts."));
        }
        else if (wasInterrupted)
        {
          this.thread.Add(BrainSession.Message("developer", "[App] The user cut this answer off here."));
        }
        if (!string.IsNullOrEmpty(drawingCode) && !wasInterrupted)
        {
          this.codeOfDrawingOnScreen.Add(drawingCode);
        }
        this.lastTurnAt = this.now();
      }
      return true;
    }

    // Token: 0x06001D29 RID: 7465 RVA: 0x003E723C File Offset: 0x003E723C
    public void AppendDeveloperNote(string sessionIdAtStart, string text)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.SessionId == sessionIdAtStart)
        {
          this.thread.Add(BrainSession.Message("developer", text));
        }
      }
    }

    // Token: 0x06001D2A RID: 7466 RVA: 0x003E729C File Offset: 0x003E729C
    public void AppendToolResult(BrainToolCall call, string output)
    {
      object obj = this.gate;
      lock (obj)
      {
        List<JsonNode> list = this.thread;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["type"] = "function_call";
        jsonObject["call_id"] = call.CallId;
        jsonObject["name"] = call.Name;
        jsonObject["arguments"] = call.Arguments;
        list.Add(jsonObject);
        List<JsonNode> list2 = this.thread;
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["type"] = "function_call_output";
        jsonObject2["call_id"] = call.CallId;
        jsonObject2["output"] = output;
        list2.Add(jsonObject2);
      }
    }

    // Token: 0x06001D2B RID: 7467 RVA: 0x003E739C File Offset: 0x003E739C
    public void ForgetDrawingOnScreen()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.codeOfDrawingOnScreen.Clear();
      }
    }

    // Token: 0x06001D2C RID: 7468 RVA: 0x003E73E4 File Offset: 0x003E73E4
    [NullableContext(2)]
    private string DrawingCodeMissingFromThreadLocked()
    {
      if (this.codeOfDrawingOnScreen.Count == 0)
      {
        return null;
      }
      List<string> assistantTexts = this.thread.Where<JsonNode>((JsonNode i) => (string)i["role"] == "assistant").Select<JsonNode, string>(delegate(JsonNode i)
      {
        JsonArray jsonArray = i["content"] as JsonArray;
        IEnumerable<string> enumerable;
        if (jsonArray == null)
        {
          enumerable = null;
        }
        else
        {
          enumerable = jsonArray.Select<JsonNode, string>((JsonNode c) => (string)((c != null) ? c["text"] : null));
        }
        return string.Concat(enumerable ?? Array.Empty<string>());
      }).ToList<string>();
      if (!this.codeOfDrawingOnScreen.Any<string>(delegate(string code)
      {
        string start = ((code.Length > 120) ? code.Substring(0, 120) : code);
        return !assistantTexts.Any<string>((string t) => t.Contains(start, StringComparison.Ordinal));
      }))
      {
        return null;
      }
      string text = string.Join("\n", this.codeOfDrawingOnScreen);
      if (text.Length <= 16000)
      {
        return text;
      }
      string text2 = text;
      int length = text2.Length;
      int num = length - 16000;
      return text2.Substring(num, length - num);
    }

    // Token: 0x06001D2D RID: 7469 RVA: 0x003E74B4 File Offset: 0x003E74B4
    public void CompactThreadIfLarge(int lastInputTokens)
    {
      BrainSession.<>c__DisplayClass76_0 CS$<>8__locals1 = new BrainSession.<>c__DisplayClass76_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.lastInputTokens = lastInputTokens;
      object obj = this.gate;
      int num;
      lock (obj)
      {
        if (CS$<>8__locals1.lastInputTokens > 0)
        {
          this.lastTurnInputTokens = CS$<>8__locals1.lastInputTokens;
        }
        num = this.sessionBaselineInputTokens;
        if (this.isCompacting || !BrainCompactionPolicy.ShouldAskWorker(CS$<>8__locals1.lastInputTokens, num, this.compactionNextCheckInputTokens))
        {
          CS$<>8__locals1.sentThread = null;
          CS$<>8__locals1.sentSessionId = null;
          CS$<>8__locals1.generation = 0;
          CS$<>8__locals1.sentCount = -1;
        }
        else
        {
          this.isCompacting = true;
          CS$<>8__locals1.sentThread = BrainSession.ThreadForWorker(this.thread, this.screenshotFilesOffForSession);
          CS$<>8__locals1.sentCount = this.thread.Count;
          CS$<>8__locals1.sentSessionId = this.SessionId;
          CS$<>8__locals1.generation = this.compactionGeneration;
        }
      }
      this.PublishStats();
      if (CS$<>8__locals1.sentCount < 0)
      {
        return;
      }
      OneBrainStats shared = OneBrainStats.Shared;
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
      defaultInterpolatedStringHandler.AppendLiteral("compacting at ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.lastInputTokens / 1000);
      defaultInterpolatedStringHandler.AppendLiteral("k input (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.sentCount);
      defaultInterpolatedStringHandler.AppendLiteral(" items)");
      shared.Note(defaultInterpolatedStringHandler.ToStringAndClear());
      this.PublishStats();
      string text = "OneBrain";
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(88, 3);
      defaultInterpolatedStringHandler.AppendLiteral("input reached ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.lastInputTokens);
      defaultInterpolatedStringHandler.AppendLiteral(" tokens (conversation ~");
      defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(0, CS$<>8__locals1.lastInputTokens - num));
      defaultInterpolatedStringHandler.AppendLiteral("): asking the Worker to compact the thread (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals1.sentCount);
      defaultInterpolatedStringHandler.AppendLiteral(" items)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
      Task.Run(delegate
      {
        BrainSession.<>c__DisplayClass76_0.<<CompactThreadIfLarge>b__0>d <<CompactThreadIfLarge>b__0>d;
        <<CompactThreadIfLarge>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<CompactThreadIfLarge>b__0>d.<>4__this = CS$<>8__locals1;
        <<CompactThreadIfLarge>b__0>d.<>1__state = -1;
        <<CompactThreadIfLarge>b__0>d.<>t__builder.Start<BrainSession.<>c__DisplayClass76_0.<<CompactThreadIfLarge>b__0>d>(ref <<CompactThreadIfLarge>b__0>d);
        return <<CompactThreadIfLarge>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06001D2E RID: 7470 RVA: 0x003E76A0 File Offset: 0x003E76A0
    internal void ApplyCompaction(BrainCompactReply reply, int sentCount, string sentSessionId, int generation, int inputTokens)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.SessionId != sentSessionId || this.compactionGeneration != generation)
        {
          Log.Info("OneBrain", "compaction came back after the thread moved on; not applied");
          return;
        }
        int? tokensUntilFold = reply.TokensUntilFold;
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
        if (tokensUntilFold != null)
        {
          int valueOrDefault = tokensUntilFold.GetValueOrDefault();
          this.compactionNextCheckInputTokens = BrainCompactionPolicy.NextCheckInputTokens(inputTokens, valueOrDefault);
          string text = "OneBrain";
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 1);
          defaultInterpolatedStringHandler.AppendLiteral("too little to fold yet; asking again at ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.compactionNextCheckInputTokens);
          defaultInterpolatedStringHandler.AppendLiteral(" input tokens");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
          OneBrainStats shared = OneBrainStats.Shared;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 1);
          defaultInterpolatedStringHandler.AppendLiteral("too little to fold; next ask at ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(this.compactionNextCheckInputTokens / 1000);
          defaultInterpolatedStringHandler.AppendLiteral("k");
          shared.Note(defaultInterpolatedStringHandler.ToStringAndClear());
          return;
        }
        BrainCompactResult compacted = reply.Compacted;
        if (compacted == null)
        {
          return;
        }
        if (this.thread.Count < sentCount || compacted.DropLeadingItems < 0 || compacted.DropLeadingItems > compacted.KeepLeadingItems || compacted.KeepLeadingItems > compacted.KeepFromIndex || compacted.KeepFromIndex > sentCount)
        {
          Log.Info("OneBrain", "compaction came back after the thread moved on; not applied");
          return;
        }
        int count = this.thread.Count;
        List<JsonNode> list = new List<JsonNode>();
        list.AddRange(this.thread.GetRange(compacted.DropLeadingItems, compacted.KeepLeadingItems - compacted.DropLeadingItems));
        if (compacted.NoteItem != null)
        {
          list.Add(compacted.NoteItem);
        }
        list.AddRange(this.thread.GetRange(compacted.KeepFromIndex, this.thread.Count - compacted.KeepFromIndex));
        this.thread.Clear();
        this.thread.AddRange(list);
        this.compactionGeneration++;
        this.compactionNextCheckInputTokens = 0;
        this.lastTurnInputTokens = 0;
        string text2;
        if (compacted.DropLeadingItems <= 0)
        {
          text2 = "";
        }
        else
        {
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
          defaultInterpolatedStringHandler.AppendLiteral(", the oldest ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(compacted.DropLeadingItems);
          defaultInterpolatedStringHandler.AppendLiteral(" notes left");
          text2 = defaultInterpolatedStringHandler.ToStringAndClear();
        }
        string text3 = text2;
        string text4 = "OneBrain";
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 3);
        defaultInterpolatedStringHandler.AppendLiteral("thread compacted: ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" -> ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.thread.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" items");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
        OneBrainStats shared2 = OneBrainStats.Shared;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
        defaultInterpolatedStringHandler.AppendLiteral("compacted ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(count);
        defaultInterpolatedStringHandler.AppendLiteral(" → ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.thread.Count);
        defaultInterpolatedStringHandler.AppendLiteral(" items");
        defaultInterpolatedStringHandler.AppendFormatted(text3);
        defaultInterpolatedStringHandler.AppendLiteral(" (next turn rewrites the cache)");
        shared2.Note(defaultInterpolatedStringHandler.ToStringAndClear());
      }
      this.SaveToDisk();
    }

    // Token: 0x06001D2F RID: 7471 RVA: 0x003E79F4 File Offset: 0x003E79F4
    [NullableContext(2)]
    private string AccountDirectoryLocked()
    {
      if (string.IsNullOrEmpty(this.accountId))
      {
        return null;
      }
      string text = new string(this.accountId.Where<char>((char c) => char.IsAsciiLetterOrDigit(c) || c == '-').ToArray<char>());
      if (text.Length != 0)
      {
        return Path.Combine(this.rootDirectory, text);
      }
      return null;
    }

    // Token: 0x06001D30 RID: 7472 RVA: 0x003E7A5C File Offset: 0x003E7A5C
    public unsafe void SaveToDisk()
    {
      object obj = this.gate;
      string fileName;
      byte[] bytes;
      string text;
      lock (obj)
      {
        text = this.AccountDirectoryLocked();
        if (text == null)
        {
          return;
        }
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["session_id"] = this.SessionId;
        jsonObject["last_turn_at"] = (double)this.lastTurnAt.ToUnixTimeMilliseconds() / 1000.0;
        jsonObject["user_layer"] = this.userLayer;
        jsonObject["user_layer_signature"] = this.userLayerSignature;
        jsonObject["thread"] = new JsonArray((from i in BrainSession.WithoutScreenshots(this.thread)
          select i.DeepClone()).ToArray<JsonNode>());
        jsonObject["drawing_code"] = new JsonArray(this.codeOfDrawingOnScreen.Select<string, JsonNode>((string c) => JsonValue.Create(c, null)).ToArray<JsonNode>());
        jsonObject["last_input_tokens"] = this.lastTurnInputTokens;
        jsonObject["baseline_input_tokens"] = this.sessionBaselineInputTokens;
        string text2 = "canvas";
        ValueTuple<int, int>? valueTuple = this.lastCanvasSize;
        JsonNode jsonNode;
        if (valueTuple != null)
        {
          ValueTuple<int, int> valueOrDefault = valueTuple.GetValueOrDefault();
          <>y__InlineArray2<JsonNode> <>y__InlineArray = default(<>y__InlineArray2<JsonNode>);
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 0) = valueOrDefault.Item1;
          *<PrivateImplementationDetails>.InlineArrayElementRef<<>y__InlineArray2<JsonNode>, JsonNode>(ref <>y__InlineArray, 1) = valueOrDefault.Item2;
          jsonNode = new JsonArray(<PrivateImplementationDetails>.InlineArrayAsReadOnlySpan<<>y__InlineArray2<JsonNode>, JsonNode>(in <>y__InlineArray, 2));
        }
        else
        {
          jsonNode = new JsonArray(null);
        }
        jsonObject[text2] = jsonNode;
        jsonObject["environment_sent"] = this.environmentSentForSessionId == this.SessionId;
        JsonObject jsonObject2 = jsonObject;
        bytes = JsonSerializer.SerializeToUtf8Bytes<JsonObject>(jsonObject2, JsonNodeContext.Default.JsonObject);
        fileName = this.SessionId + ".json";
      }
      string threads = Path.Combine(text, "threads");
      Task.Run(delegate
      {
        try
        {
          AtomicFile.WriteAllBytes(Path.Combine(threads, fileName), bytes);
        }
        catch (Exception ex)
        {
          Log.Warn("OneBrain", "thread not saved: " + ex.Message);
        }
      });
    }

    // Token: 0x06001D31 RID: 7473 RVA: 0x003E7CCC File Offset: 0x003E7CCC
    private void RestoreSavedSessionIfRecentLocked()
    {
      string text = this.AccountDirectoryLocked();
      if (text == null)
      {
        return;
      }
      string text2 = Path.Combine(text, "threads");
      if (!Directory.Exists(text2))
      {
        return;
      }
      try
      {
        FileInfo[] files = new DirectoryInfo(text2).GetFiles("*.json");
        foreach (FileInfo fileInfo in files.Where<FileInfo>((FileInfo f) => this.now() - f.LastWriteTimeUtc > BrainSession.SavedThreadRetention))
        {
          fileInfo.Delete();
        }
        FileInfo fileInfo2 = files.Where<FileInfo>((FileInfo f) => f.Exists && this.now() - f.LastWriteTimeUtc <= BrainSession.IdleBeforeNewSession).MaxBy<FileInfo, DateTime>((FileInfo f) => f.LastWriteTimeUtc);
        if (fileInfo2 != null)
        {
          JsonObject jsonObject = JsonNode.Parse(File.ReadAllBytes(fileInfo2.FullName), null, default(JsonDocumentOptions)) as JsonObject;
          if (jsonObject != null)
          {
            string text3 = (string)jsonObject["session_id"];
            if (text3 != null)
            {
              JsonArray jsonArray = jsonObject["thread"] as JsonArray;
              if (jsonArray != null)
              {
                JsonNode jsonNode = jsonObject["last_turn_at"];
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds((long)(((jsonNode != null) ? jsonNode.GetValue<double>() : 0.0) * 1000.0));
                if (!(this.now() - dateTimeOffset > BrainSession.IdleBeforeNewSession))
                {
                  this.SessionId = text3;
                  this.lastTurnAt = dateTimeOffset;
                  this.userLayer = (string)jsonObject["user_layer"];
                  this.userLayerSignature = (string)jsonObject["user_layer_signature"];
                  this.thread.Clear();
                  this.thread.AddRange(from i in jsonArray
                    where i != null
                    select i.DeepClone());
                  this.codeOfDrawingOnScreen.Clear();
                  JsonArray jsonArray2 = jsonObject["drawing_code"] as JsonArray;
                  if (jsonArray2 != null)
                  {
                    this.codeOfDrawingOnScreen.AddRange(from c in jsonArray2
                      select (string)c into c
                      where c != null
                      select c);
                  }
                  JsonNode jsonNode2 = jsonObject["last_input_tokens"];
                  this.lastTurnInputTokens = (int)((jsonNode2 != null) ? jsonNode2.GetValue<double>() : 0.0);
                  JsonNode jsonNode3 = jsonObject["baseline_input_tokens"];
                  this.sessionBaselineInputTokens = (int)((jsonNode3 != null) ? jsonNode3.GetValue<double>() : 0.0);
                  JsonArray jsonArray3 = jsonObject["canvas"] as JsonArray;
                  if (jsonArray3 != null && jsonArray3.Count == 2)
                  {
                    this.lastCanvasSize = new ValueTuple<int, int>?(new ValueTuple<int, int>((int)jsonArray3[0].GetValue<double>(), (int)jsonArray3[1].GetValue<double>()));
                  }
                  this.isFirstKeyDownAfterReload = true;
                  JsonNode jsonNode4 = jsonObject["environment_sent"];
                  if (jsonNode4 != null && jsonNode4.GetValue<bool>())
                  {
                    this.environmentSentForSessionId = text3;
                  }
                  string text4 = "OneBrain";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(53, 3);
                  defaultInterpolatedStringHandler.AppendLiteral("picked up session ");
                  defaultInterpolatedStringHandler.AppendFormatted(text3);
                  defaultInterpolatedStringHandler.AppendLiteral(" again (");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(this.thread.Count);
                  defaultInterpolatedStringHandler.AppendLiteral(" items, last turn ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>((int)(this.now() - this.lastTurnAt).TotalMinutes);
                  defaultInterpolatedStringHandler.AppendLiteral(" min ago)");
                  Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
                  OneBrainStats shared = OneBrainStats.Shared;
                  defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("picked up ");
                  defaultInterpolatedStringHandler.AppendFormatted((text3.Length > 8) ? text3.Substring(0, 8) : text3);
                  defaultInterpolatedStringHandler.AppendLiteral(" again (");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(this.thread.Count);
                  defaultInterpolatedStringHandler.AppendLiteral(" items, no screenshots)");
                  shared.Note(defaultInterpolatedStringHandler.ToStringAndClear());
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Log.Warn("OneBrain", "saved session not restored: " + ex.Message);
      }
    }

    // Token: 0x06001D32 RID: 7474 RVA: 0x003E8174 File Offset: 0x003E8174
    public void LogRecord(JsonObject record)
    {
      object obj = this.gate;
      string directory;
      string sessionId;
      lock (obj)
      {
        directory = this.AccountDirectoryLocked();
        sessionId = this.SessionId;
      }
      if (directory == null)
      {
        return;
      }
      record["session"] = sessionId;
      record["at"] = DateTimeOffset.UtcNow.ToString("O");
      string line = record.ToJsonString(null) + "\n";
      Task.Run(delegate
      {
        try
        {
          Directory.CreateDirectory(directory);
          File.AppendAllText(Path.Combine(directory, sessionId + ".jsonl"), line);
        }
        catch
        {
        }
      });
    }

    // Token: 0x06001D33 RID: 7475 RVA: 0x003E8238 File Offset: 0x003E8238
    public static JsonObject Message(string role, string text)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["type"] = "message";
      jsonObject["role"] = role;
      string text2 = "content";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["type"] = "input_text";
      jsonObject2["text"] = text;
      JsonNode jsonNode = jsonObject2;
      jsonObject[text2] = new JsonArray(new ReadOnlySpan<JsonNode>(ref jsonNode));
      return jsonObject;
    }

    // Token: 0x06001D34 RID: 7476 RVA: 0x003E82C8 File Offset: 0x003E82C8
    [NullableContext(2)]
    private static bool IsScreenshotBlock(JsonNode block)
    {
      return (string)((block != null) ? block["type"] : null) == "input_image";
    }

    // Token: 0x06001D35 RID: 7477 RVA: 0x003E82EC File Offset: 0x003E82EC
    public static int ScreenshotCount(IEnumerable<JsonNode> items)
    {
      return items.Sum<JsonNode>(delegate(JsonNode i)
      {
        JsonArray jsonArray = i["content"] as JsonArray;
        if (jsonArray == null)
        {
          return 0;
        }
        Func<JsonNode, bool> func;
        if ((func = BrainSession.<>O.<0>__IsScreenshotBlock) == null)
        {
          func = (BrainSession.<>O.<0>__IsScreenshotBlock = new Func<JsonNode, bool>(BrainSession.IsScreenshotBlock));
        }
        return jsonArray.Count<JsonNode>(func);
      });
    }

    // Token: 0x06001D36 RID: 7478 RVA: 0x003E8314 File Offset: 0x003E8314
    public static List<JsonNode> ThreadForWorker(IEnumerable<JsonNode> items, bool screenshotFilesOff)
    {
      Func<JsonNode, JsonNode> <>9__2;
      return items.Select<JsonNode, JsonNode>(delegate(JsonNode item)
      {
        JsonArray jsonArray = item["content"] as JsonArray;
        if (jsonArray != null)
        {
          if (jsonArray.Any<JsonNode>((JsonNode b) => ((b != null) ? b["clicky_file"] : null) != null))
          {
            JsonObject jsonObject = (JsonObject)item.DeepClone();
            string text = "content";
            IEnumerable<JsonNode> enumerable = jsonArray;
            Func<JsonNode, JsonNode> func;
            if ((func = <>9__2) == null)
            {
              func = (<>9__2 = delegate(JsonNode block)
              {
                JsonValue jsonValue = ((block != null) ? block["clicky_file"] : null) as JsonValue;
                if (jsonValue == null)
                {
                  if (block == null)
                  {
                    return null;
                  }
                  return block.DeepClone();
                }
                else
                {
                  if (screenshotFilesOff)
                  {
                    JsonObject jsonObject2 = (JsonObject)block.DeepClone();
                    jsonObject2.Remove("clicky_file");
                    return jsonObject2;
                  }
                  JsonObject jsonObject3 = new JsonObject(null);
                  jsonObject3["type"] = "input_image";
                  jsonObject3["clicky_file"] = jsonValue.DeepClone();
                  return jsonObject3;
                }
              });
            }
            jsonObject[text] = new JsonArray(enumerable.Select<JsonNode, JsonNode>(func).ToArray<JsonNode>());
            return jsonObject;
          }
        }
        return item;
      }).ToList<JsonNode>();
    }

    // Token: 0x06001D37 RID: 7479 RVA: 0x003E8348 File Offset: 0x003E8348
    public static List<JsonNode> WithoutScreenshots(IEnumerable<JsonNode> items)
    {
      return items.Select<JsonNode, JsonNode>(delegate(JsonNode item)
      {
        JsonArray jsonArray = item["content"] as JsonArray;
        if (jsonArray != null)
        {
          IEnumerable<JsonNode> enumerable = jsonArray;
          Func<JsonNode, bool> func;
          if ((func = BrainSession.<>O.<0>__IsScreenshotBlock) == null)
          {
            func = (BrainSession.<>O.<0>__IsScreenshotBlock = new Func<JsonNode, bool>(BrainSession.IsScreenshotBlock));
          }
          if (enumerable.Any<JsonNode>(func))
          {
            JsonObject jsonObject = (JsonObject)item.DeepClone();
            jsonObject["content"] = new JsonArray((from b in jsonArray
              where !BrainSession.IsScreenshotBlock(b)
              select b.DeepClone()).ToArray<JsonNode>());
            return jsonObject;
          }
        }
        return item;
      }).ToList<JsonNode>();
    }

    // Token: 0x06001D38 RID: 7480 RVA: 0x003E8374 File Offset: 0x003E8374
    public static List<JsonNode> WithNewestScreenshots(List<JsonNode> items, int keep)
    {
      int toDrop = BrainSession.ScreenshotCount(items) - keep;
      if (toDrop <= 0)
      {
        return items;
      }
      return items.Select<JsonNode, JsonNode>(delegate(JsonNode item)
      {
        if (toDrop > 0)
        {
          JsonArray jsonArray = item["content"] as JsonArray;
          if (jsonArray != null)
          {
            IEnumerable<JsonNode> enumerable = jsonArray;
            Func<JsonNode, bool> func;
            if ((func = BrainSession.<>O.<0>__IsScreenshotBlock) == null)
            {
              func = (BrainSession.<>O.<0>__IsScreenshotBlock = new Func<JsonNode, bool>(BrainSession.IsScreenshotBlock));
            }
            if (enumerable.Any<JsonNode>(func))
            {
              JsonObject jsonObject = (JsonObject)item.DeepClone();
              JsonArray jsonArray2 = new JsonArray(null);
              foreach (JsonNode jsonNode in jsonArray)
              {
                if (toDrop > 0 && BrainSession.IsScreenshotBlock(jsonNode))
                {
                  int toDrop2 = toDrop;
                  toDrop = toDrop2 - 1;
                }
                else
                {
                  jsonArray2.Add(jsonNode.DeepClone());
                }
              }
              jsonObject["content"] = jsonArray2;
              return jsonObject;
            }
          }
        }
        return item;
      }).ToList<JsonNode>();
    }

    // Token: 0x06001D39 RID: 7481 RVA: 0x003E83B8 File Offset: 0x003E83B8
    public static string StoredUserText(string words, string fact)
    {
      string text = BrainSession.<StoredUserText>g__Trimmed|88_0(words, 4000);
      if (text.Length != 0)
      {
        return text;
      }
      return "(" + BrainSession.<StoredUserText>g__Trimmed|88_0(fact, 1000) + " Here is the screen now.)";
    }

    // Token: 0x06001D3A RID: 7482 RVA: 0x003E83F8 File Offset: 0x003E83F8
    public void Dispose()
    {
      object obj = this.gate;
      lock (obj)
      {
        Timer timer = this.idleSessionEndTimer;
        if (timer != null)
        {
          timer.Dispose();
        }
      }
    }

    // Token: 0x06001D3F RID: 7487 RVA: 0x003E84C4 File Offset: 0x003E84C4
    [CompilerGenerated]
    internal static string <StoredUserText>g__Trimmed|88_0(string text, int limit)
    {
      string text2 = text.Trim();
      if (text2.Length <= limit)
      {
        return text2;
      }
      return text2.Substring(0, limit);
    }

    // Token: 0x04000C78 RID: 3192
    private const string Area = "OneBrain";

    // Token: 0x04000C79 RID: 3193
    public static readonly TimeSpan IdleBeforeNewSession = TimeSpan.FromHours(2);

    // Token: 0x04000C7A RID: 3194
    private static readonly TimeSpan SavedThreadRetention = TimeSpan.FromDays(14);

    // Token: 0x04000C7B RID: 3195
    public const int MaximumScreenshotsInThread = 48;

    // Token: 0x04000C7C RID: 3196
    public const int ScreenshotsKeptAfterTrim = 32;

    // Token: 0x04000C7D RID: 3197
    private readonly BrainClient client;

    // Token: 0x04000C7E RID: 3198
    [Nullable(new byte[] { 1, 1, 2 })]
    private readonly Func<CancellationToken, Task<string>> accessToken;

    // Token: 0x04000C7F RID: 3199
    private readonly string rootDirectory;

    // Token: 0x04000C80 RID: 3200
    private readonly Func<DateTimeOffset> now;

    // Token: 0x04000C81 RID: 3201
    private readonly object gate = new object();

    // Token: 0x04000C82 RID: 3202
    [Nullable(2)]
    private Timer idleSessionEndTimer;

    // Token: 0x04000C84 RID: 3204
    private readonly List<JsonNode> thread = new List<JsonNode>();

    // Token: 0x04000C85 RID: 3205
    [TupleElementNames(new string[] { "Key", "Text" })]
    [Nullable(new byte[] { 1, 0, 2, 1 })]
    private readonly List<ValueTuple<string, string>> pendingAppNotes = new List<ValueTuple<string, string>>();

    // Token: 0x04000C86 RID: 3206
    private readonly List<string> codeOfDrawingOnScreen = new List<string>();

    // Token: 0x04000C87 RID: 3207
    [Nullable(2)]
    private string userLayer;

    // Token: 0x04000C88 RID: 3208
    [Nullable(2)]
    private string userLayerSignature;

    // Token: 0x04000C89 RID: 3209
    [Nullable(2)]
    private string accountId;

    // Token: 0x04000C8A RID: 3210
    private bool hasLookedForSavedSession;

    // Token: 0x04000C8B RID: 3211
    private bool isCompacting;

    // Token: 0x04000C8C RID: 3212
    private int compactionGeneration;

    // Token: 0x04000C8D RID: 3213
    private int compactionNextCheckInputTokens;

    // Token: 0x04000C8E RID: 3214
    private int lastTurnInputTokens;

    // Token: 0x04000C8F RID: 3215
    private int sessionBaselineInputTokens;

    // Token: 0x04000C90 RID: 3216
    private bool isFirstKeyDownAfterReload;

    // Token: 0x04000C91 RID: 3217
    [TupleElementNames(new string[] { "Width", "Height" })]
    [Nullable(0)]
    private ValueTuple<int, int>? lastCanvasSize;

    // Token: 0x04000C92 RID: 3218
    private DateTimeOffset lastPrewarmAt = DateTimeOffset.MinValue;

    // Token: 0x04000C93 RID: 3219
    private DateTimeOffset sessionStartedAt = DateTimeOffset.MinValue;

    // Token: 0x04000C94 RID: 3220
    private DateTimeOffset lastTurnAt = DateTimeOffset.MinValue;

    // Token: 0x04000C95 RID: 3221
    private DateTimeOffset previousTurnAt = DateTimeOffset.MinValue;

    // Token: 0x04000C96 RID: 3222
    private int statsTurnCount;

    // Token: 0x04000C97 RID: 3223
    [Nullable(2)]
    private string environmentSentForSessionId;

    // Token: 0x04000C98 RID: 3224
    private readonly List<string> sessionScreenshotFiles = new List<string>();

    // Token: 0x04000C99 RID: 3225
    private bool screenshotFilesOffForSession;

    // Token: 0x04000C9A RID: 3226
    public const string SafetyRefusalLine = "My AI provider's safety filter blocked this because it looks like hands-on security work. I can still explain the concepts.";

    // Token: 0x02000647 RID: 1607
    [Nullable(0)]
    private sealed class Ended : IEquatable<BrainSession.Ended>
    {
      // Token: 0x06003908 RID: 14600 RVA: 0x00455810 File Offset: 0x00455810
      public Ended(string SessionId, List<JsonNode> Thread, List<string> ScreenshotFiles)
      {
        this.SessionId = SessionId;
        this.Thread = Thread;
        this.ScreenshotFiles = ScreenshotFiles;
        base..ctor();
      }

      // Token: 0x170009D4 RID: 2516
      // (get) Token: 0x06003909 RID: 14601 RVA: 0x00455830 File Offset: 0x00455830
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(BrainSession.Ended);
        }
      }

      // Token: 0x170009D5 RID: 2517
      // (get) Token: 0x0600390A RID: 14602 RVA: 0x0045583C File Offset: 0x0045583C
      // (set) Token: 0x0600390B RID: 14603 RVA: 0x00455844 File Offset: 0x00455844
      public string SessionId { get; set; }

      // Token: 0x170009D6 RID: 2518
      // (get) Token: 0x0600390C RID: 14604 RVA: 0x00455850 File Offset: 0x00455850
      // (set) Token: 0x0600390D RID: 14605 RVA: 0x00455858 File Offset: 0x00455858
      public List<JsonNode> Thread { get; set; }

      // Token: 0x170009D7 RID: 2519
      // (get) Token: 0x0600390E RID: 14606 RVA: 0x00455864 File Offset: 0x00455864
      // (set) Token: 0x0600390F RID: 14607 RVA: 0x0045586C File Offset: 0x0045586C
      public List<string> ScreenshotFiles { get; set; }

      // Token: 0x06003910 RID: 14608 RVA: 0x00455878 File Offset: 0x00455878
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Ended");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003911 RID: 14609 RVA: 0x004558C4 File Offset: 0x004558C4
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("SessionId = ");
        builder.Append(this.SessionId);
        builder.Append(", Thread = ");
        builder.Append(this.Thread);
        builder.Append(", ScreenshotFiles = ");
        builder.Append(this.ScreenshotFiles);
        return true;
      }

      // Token: 0x06003912 RID: 14610 RVA: 0x00455924 File Offset: 0x00455924
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(BrainSession.Ended left, BrainSession.Ended right)
      {
        return !(left == right);
      }

      // Token: 0x06003913 RID: 14611 RVA: 0x00455930 File Offset: 0x00455930
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(BrainSession.Ended left, BrainSession.Ended right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003914 RID: 14612 RVA: 0x00455944 File Offset: 0x00455944
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<SessionId>k__BackingField)) * -1521134295 + EqualityComparer<List<JsonNode>>.Default.GetHashCode(this.<Thread>k__BackingField)) * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(this.<ScreenshotFiles>k__BackingField);
      }

      // Token: 0x06003915 RID: 14613 RVA: 0x004559A8 File Offset: 0x004559A8
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as BrainSession.Ended);
      }

      // Token: 0x06003916 RID: 14614 RVA: 0x004559B8 File Offset: 0x004559B8
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(BrainSession.Ended other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<SessionId>k__BackingField, other.<SessionId>k__BackingField) && EqualityComparer<List<JsonNode>>.Default.Equals(this.<Thread>k__BackingField, other.<Thread>k__BackingField) && EqualityComparer<List<string>>.Default.Equals(this.<ScreenshotFiles>k__BackingField, other.<ScreenshotFiles>k__BackingField));
      }

      // Token: 0x06003918 RID: 14616 RVA: 0x00455A34 File Offset: 0x00455A34
      [CompilerGenerated]
      private Ended(BrainSession.Ended original)
      {
        this.SessionId = original.<SessionId>k__BackingField;
        this.Thread = original.<Thread>k__BackingField;
        this.ScreenshotFiles = original.<ScreenshotFiles>k__BackingField;
      }

      // Token: 0x06003919 RID: 14617 RVA: 0x00455A60 File Offset: 0x00455A60
      [CompilerGenerated]
      public void Deconstruct(out string SessionId, out List<JsonNode> Thread, out List<string> ScreenshotFiles)
      {
        SessionId = this.SessionId;
        Thread = this.Thread;
        ScreenshotFiles = this.ScreenshotFiles;
      }
    }

    // Token: 0x02000648 RID: 1608
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040024B3 RID: 9395
      [Nullable(0)]
      public static Func<JsonNode, bool> <0>__IsScreenshotBlock;
    }
  }
}
