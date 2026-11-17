using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Shell;
using HeyClicky.App.UI;
using HeyClicky.Core.Drive;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Voice;
using HeyClicky.Platform.Capture;
using HeyClicky.Platform.Input;
using HeyClicky.Platform.Win32;
using Microsoft.UI.Dispatching;
using Windows.UI;

namespace HeyClicky.App.Drive
{
  // Token: 0x020002C0 RID: 704
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyTakeOverController : IClickyDriveTurns
  {
    // Token: 0x17000533 RID: 1331
    // (get) Token: 0x06001D4F RID: 7503 RVA: 0x005FE320 File Offset: 0x005FE320
    // (set) Token: 0x06001D50 RID: 7504 RVA: 0x005FE328 File Offset: 0x005FE328
    [Nullable(2)]
    public static ClickyTakeOverController Current
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      private set;
    }

    // Token: 0x06001D51 RID: 7505 RVA: 0x005FE330 File Offset: 0x005FE330
    [NullableContext(2)]
    private static void TrackDrive([Nullable(1)] string action, string threadId = null, JsonObject properties = null)
    {
      ClickyDriveAnalytics.Track(action, threadId, properties);
    }

    // Token: 0x06001D52 RID: 7506 RVA: 0x005FE33C File Offset: 0x005FE33C
    private static void TrackOfferAnswered(string answer)
    {
      string text = "offer_answered";
      string text2 = null;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["answer"] = answer;
      ClickyTakeOverController.TrackDrive(text, text2, jsonObject);
    }

    // Token: 0x06001D53 RID: 7507 RVA: 0x005FE374 File Offset: 0x005FE374
    public ClickyTakeOverController(CompanionSession session, ScreenCaptureService capture, DispatcherQueue ui)
    {
      Action<string, double> action;
      if ((action = ClickyTakeOverController.<>O.<0>__Play) == null)
      {
        action = (ClickyTakeOverController.<>O.<0>__Play = new Action<string, double>(Sounds.Play));
      }
      this.PlayChime = action;
      this.IsAvailable = () => true;
      this.ChromeSetupIsRunning = () => false;
      this.MacStepBindingForBrainDrive = (string _) => null;
      base..ctor();
      this.session = session;
      this.capture = capture;
      this.ui = ui;
      ClickyTakeOverController.Current = this;
    }

    // Token: 0x17000534 RID: 1332
    // (get) Token: 0x06001D54 RID: 7508 RVA: 0x005FE538 File Offset: 0x005FE538
    // (set) Token: 0x06001D55 RID: 7509 RVA: 0x005FE540 File Offset: 0x005FE540
    public Func<Color> Accent { get; set; } = () => Color.FromArgb(byte.MaxValue, 59, 130, 246);

    // Token: 0x17000535 RID: 1333
    // (get) Token: 0x06001D56 RID: 7510 RVA: 0x005FE54C File Offset: 0x005FE54C
    // (set) Token: 0x06001D57 RID: 7511 RVA: 0x005FE554 File Offset: 0x005FE554
    public Action<string, double> PlayChime { get; set; }

    // Token: 0x17000536 RID: 1334
    // (get) Token: 0x06001D58 RID: 7512 RVA: 0x005FE560 File Offset: 0x005FE560
    // (set) Token: 0x06001D59 RID: 7513 RVA: 0x005FE568 File Offset: 0x005FE568
    public Func<bool> IsAvailable { get; set; }

    // Token: 0x17000537 RID: 1335
    // (get) Token: 0x06001D5A RID: 7514 RVA: 0x005FE574 File Offset: 0x005FE574
    // (set) Token: 0x06001D5B RID: 7515 RVA: 0x005FE57C File Offset: 0x005FE57C
    public Func<bool> ChromeSetupIsRunning { get; set; }

    // Token: 0x17000538 RID: 1336
    // (get) Token: 0x06001D5C RID: 7516 RVA: 0x005FE588 File Offset: 0x005FE588
    // (set) Token: 0x06001D5D RID: 7517 RVA: 0x005FE590 File Offset: 0x005FE590
    [Nullable(new byte[] { 1, 1, 2 })]
    public Func<string, AgentDriveBinding> MacStepBindingForBrainDrive
    {
      [return: Nullable(new byte[] { 1, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 1, 1, 2 })]
      set;
    }

    // Token: 0x17000539 RID: 1337
    // (get) Token: 0x06001D5E RID: 7518 RVA: 0x005FE59C File Offset: 0x005FE59C
    // (set) Token: 0x06001D5F RID: 7519 RVA: 0x005FE5A4 File Offset: 0x005FE5A4
    [Nullable(new byte[] { 2, 1, 1 })]
    public Func<string, Task> RefundEarlyStop
    {
      [return: Nullable(new byte[] { 2, 1, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 1 })]
      set;
    }

    // Token: 0x1700053A RID: 1338
    // (get) Token: 0x06001D60 RID: 7520 RVA: 0x005FE5B0 File Offset: 0x005FE5B0
    // (set) Token: 0x06001D61 RID: 7521 RVA: 0x005FE5B8 File Offset: 0x005FE5B8
    [Nullable(new byte[] { 2, 1 })]
    public Func<Task> RefreshBilling
    {
      [return: Nullable(new byte[] { 2, 1 })]
      get;
      [param: Nullable(new byte[] { 2, 1 })]
      set;
    }

    // Token: 0x1700053B RID: 1339
    // (get) Token: 0x06001D62 RID: 7522 RVA: 0x005FE5C4 File Offset: 0x005FE5C4
    public ClickyTakeOverController.Phase CurrentPhase
    {
      get
      {
        object obj = this.gate;
        ClickyTakeOverController.Phase phase;
        lock (obj)
        {
          phase = this.phase;
        }
        return phase;
      }
    }

    // Token: 0x1700053C RID: 1340
    // (get) Token: 0x06001D63 RID: 7523 RVA: 0x005FE608 File Offset: 0x005FE608
    public bool IsDriving
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.phase.Kind == ClickyTakeOverController.PhaseKind.Driving;
        }
        return flag2;
      }
    }

    // Token: 0x1700053D RID: 1341
    // (get) Token: 0x06001D64 RID: 7524 RVA: 0x005FE654 File Offset: 0x005FE654
    public DateTimeOffset LastEndedAt
    {
      get
      {
        object obj = this.gate;
        DateTimeOffset dateTimeOffset;
        lock (obj)
        {
          dateTimeOffset = this.lastTakeOverEndedAt;
        }
        return dateTimeOffset;
      }
    }

    // Token: 0x1700053E RID: 1342
    // (get) Token: 0x06001D65 RID: 7525 RVA: 0x005FE698 File Offset: 0x005FE698
    public IReadOnlyList<string> ActionLog
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<string> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.actionLog.ToList<string>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x1700053F RID: 1343
    // (get) Token: 0x06001D66 RID: 7526 RVA: 0x005FE6E0 File Offset: 0x005FE6E0
    public IReadOnlyList<JsonObject> LastCallResults
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<JsonObject> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.lastCallResults.ToList<JsonObject>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x17000540 RID: 1344
    // (get) Token: 0x06001D67 RID: 7527 RVA: 0x005FE728 File Offset: 0x005FE728
    public bool OffersDrive
    {
      get
      {
        return this.IsAvailable();
      }
    }

    // Token: 0x06001D68 RID: 7528 RVA: 0x005FE738 File Offset: 0x005FE738
    public bool IsDriveToolName(string name)
    {
      return ClickyTakeOverController.ToolNames.Contains(name);
    }

    // Token: 0x06001D69 RID: 7529 RVA: 0x005FE748 File Offset: 0x005FE748
    public JsonObject TakeDriveBillingRequestFields()
    {
      object obj = this.gate;
      JsonObject jsonObject;
      lock (obj)
      {
        if (this.phase.Kind != ClickyTakeOverController.PhaseKind.Driving || this.driveBillingSent)
        {
          jsonObject = new JsonObject(null);
        }
        else
        {
          this.driveBillingSent = true;
          jsonObject = ClickyDriveBilling.RequestFields(this.driveBilling, this.driveIdentifier, this.driveAskingTurnTokens);
        }
      }
      return jsonObject;
    }

    // Token: 0x06001D6A RID: 7530 RVA: 0x005FE7CC File Offset: 0x005FE7CC
    public void NoteDriveLeaseId(string leaseId)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.phase.Kind == ClickyTakeOverController.PhaseKind.Driving && this.driveBilling is ClickyDriveBilling.User)
        {
          this.driveLeaseId = leaseId;
        }
      }
    }

    // Token: 0x06001D6B RID: 7531 RVA: 0x005FE82C File Offset: 0x005FE82C
    [NullableContext(2)]
    public void NoteTurnBeforeTools(bool programSpoke, string turnToken)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.currentTurnProgramSpoke = programSpoke;
        this.currentTurnToken = turnToken;
      }
    }

    // Token: 0x06001D6C RID: 7532 RVA: 0x005FE874 File Offset: 0x005FE874
    public void StopForOutOfAgentMessages()
    {
      this.OnUi(delegate
      {
        if (!this.IsDriving)
        {
          return;
        }
        Log.Info("drive", "out of agent messages: handing back");
        object obj = this.gate;
        lock (obj)
        {
          this.driveEnding = AgentDriveEnding.OutOfAgentMessages;
        }
        this.Finish("You're out of agent messages for now, so I stopped here.", "agent-close");
      });
    }

    // Token: 0x06001D6D RID: 7533 RVA: 0x005FE888 File Offset: 0x005FE888
    public void NoteDriveRequestFailed()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.phase.Kind == ClickyTakeOverController.PhaseKind.Driving && this.driveEnding == AgentDriveEnding.Finished)
        {
          this.driveEnding = AgentDriveEnding.Failed;
        }
      }
    }

    // Token: 0x06001D6E RID: 7534 RVA: 0x005FE8E0 File Offset: 0x005FE8E0
    public void PreviewForDebug()
    {
      this.OnUi(delegate
      {
        ClickyTakeOverController.<>c__DisplayClass104_0 CS$<>8__locals1 = new ClickyTakeOverController.<>c__DisplayClass104_0();
        CS$<>8__locals1.<>4__this = this;
        if (this.CurrentPhase.Kind != ClickyTakeOverController.PhaseKind.Idle)
        {
          return;
        }
        CS$<>8__locals1.accent = this.Accent();
        this.consentCard.Present("add a bass line to the second track", "Want me to add the bass line while you watch?", CS$<>8__locals1.accent, delegate
        {
          ClickyTakeOverController.<>c__DisplayClass104_0.<<PreviewForDebug>b__1>d <<PreviewForDebug>b__1>d;
          <<PreviewForDebug>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
          <<PreviewForDebug>b__1>d.<>4__this = CS$<>8__locals1;
          <<PreviewForDebug>b__1>d.<>1__state = -1;
          <<PreviewForDebug>b__1>d.<>t__builder.Start<ClickyTakeOverController.<>c__DisplayClass104_0.<<PreviewForDebug>b__1>d>(ref <<PreviewForDebug>b__1>d);
        }, delegate
        {
        }, this.PlayChime, null, "Hands off the mouse while I drive. Esc stops me.", "Go ahead", false, 45, null, null);
      });
    }

    // Token: 0x06001D6F RID: 7535 RVA: 0x005FE8F4 File Offset: 0x005FE8F4
    internal void AcceptConsentForTest()
    {
      this.OnUi(new Action(this.consentCard.AcceptForTest));
    }

    // Token: 0x06001D70 RID: 7536 RVA: 0x005FE910 File Offset: 0x005FE910
    public Task<string> HandleToolAsync(string name, string argumentsJson)
    {
      return this.InvokeOnUi<string>(() => this.HandleTool(name, argumentsJson));
    }

    // Token: 0x06001D71 RID: 7537 RVA: 0x005FE94C File Offset: 0x005FE94C
    private unsafe string HandleTool(string name, string argumentsJson)
    {
      JsonObject jsonObject2;
      try
      {
        JsonObject jsonObject;
        if ((jsonObject = JsonNode.Parse(argumentsJson, null, default(JsonDocumentOptions)) as JsonObject) == null)
        {
          jsonObject = new JsonObject(null);
        }
        jsonObject2 = jsonObject;
      }
      catch (JsonException)
      {
        jsonObject2 = new JsonObject(null);
      }
      string text = (string)jsonObject2["goal"];
      string text2 = ((text != null) ? text.Trim() : null);
      if (!(name == "offer_screen_takeover"))
      {
        if (!(name == "start_screen_takeover"))
        {
          if (!(name == "end_screen_takeover"))
          {
            return ClickyTakeOverController.Result("error", "Unknown take-over tool.");
          }
          object obj = this.gate;
          lock (obj)
          {
            this.endSummary = ((string)jsonObject2["summary"]) ?? "done";
          }
          return ClickyTakeOverController.Result("ended", null);
        }
        else
        {
          object obj = this.gate;
          ClickyTakeOverController.Phase phase;
          DateTimeOffset dateTimeOffset;
          ValueTuple<string, DateTimeOffset>? valueTuple;
          lock (obj)
          {
            string text3 = this.currentTurnToken;
            if (text3 != null && !this.askingTurnTokens.Contains(text3))
            {
              this.askingTurnTokens.Add(text3);
            }
            phase = this.phase;
            dateTimeOffset = this.lastTakeOverEndedAt;
            valueTuple = this.offerAnsweredBySpeaking;
          }
          if (phase.Kind == ClickyTakeOverController.PhaseKind.Offered)
          {
            ClickyTakeOverController.TrackOfferAnswered("yes_spoken");
            obj = this.gate;
            lock (obj)
            {
              this.pendingAnalyticsOrigin = "offer_spoken";
            }
            this.Begin(string.IsNullOrEmpty(text2) ? phase.Goal : text2);
            return ClickyTakeOverController.Result("started", null);
          }
          if (phase.Kind != ClickyTakeOverController.PhaseKind.Driving && valueTuple != null)
          {
            ValueTuple<string, DateTimeOffset> valueOrDefault = valueTuple.GetValueOrDefault();
            if (DateTimeOffset.Now - valueOrDefault.Item2 < ClickyTakeOverController.SpokenAnswerWindow)
            {
              obj = this.gate;
              lock (obj)
              {
                this.offerAnsweredBySpeaking = null;
                this.pendingAnalyticsOrigin = "offer_spoken";
              }
              ClickyTakeOverController.TrackOfferAnswered("yes_spoken");
              this.Begin(string.IsNullOrEmpty(text2) ? valueOrDefault.Item1 : text2);
              return ClickyTakeOverController.Result("started", null);
            }
          }
          if (phase.Kind != ClickyTakeOverController.PhaseKind.Driving && DateTimeOffset.Now - dateTimeOffset < TimeSpan.FromSeconds(600L) && !string.IsNullOrEmpty(text2))
          {
            string text4 = "consent_skipped";
            string text5 = null;
            JsonObject jsonObject3 = new JsonObject(null);
            jsonObject3["reason"] = "recent_yes";
            ClickyTakeOverController.TrackDrive(text4, text5, jsonObject3);
            obj = this.gate;
            lock (obj)
            {
              this.pendingAnalyticsOrigin = "recent_yes";
            }
            this.Begin(text2);
            return ClickyTakeOverController.Result("started", null);
          }
          if (phase.Kind != ClickyTakeOverController.PhaseKind.Driving && !string.IsNullOrEmpty(text2))
          {
            AgentDriveBinding agentDriveBinding = this.MacStepBindingForBrainDrive(text2);
            if (agentDriveBinding != null)
            {
              obj = this.gate;
              lock (obj)
              {
                this.agentDriveEnded = agentDriveBinding.OnEnded;
                this.agentDriveClickyName = agentDriveBinding.ClickyName;
                this.pendingBilling = new ClickyDriveBilling.Job(agentDriveBinding.JobLeaseId);
                this.pendingAnalyticsOrigin = "job_step_spoken";
                this.pendingAnalyticsThreadId = agentDriveBinding.ThreadId;
              }
              string text6 = "consent_skipped";
              string threadId = agentDriveBinding.ThreadId;
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["reason"] = "job_card_was_the_ask";
              ClickyTakeOverController.TrackDrive(text6, threadId, jsonObject4);
              this.Begin(text2);
              return ClickyTakeOverController.Result("started", null);
            }
          }
          if (phase.Kind == ClickyTakeOverController.PhaseKind.Idle && !this.ChromeSetupIsRunning())
          {
            string offeredGoal2 = (string.IsNullOrEmpty(text2) ? "do these steps for you" : text2);
            obj = this.gate;
            lock (obj)
            {
              this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Offered, offeredGoal2, 0);
              this.offerAnsweredBySpeaking = null;
              this.offeredThen = null;
            }
            this.consentCard.Present(offeredGoal2, null, this.Accent(), delegate
            {
              ClickyTakeOverController.TrackOfferAnswered("yes_card");
              object obj2 = this.gate;
              lock (obj2)
              {
                this.pendingAnalyticsOrigin = "offer_card";
              }
              this.Begin(offeredGoal2);
            }, delegate
            {
              this.Declined("no_card");
            }, this.PlayChime, delegate
            {
              this.Declined("timed_out");
            }, "Hands off the mouse while I drive. Esc stops me.", "Go ahead", false, 45, null, null);
            string text7 = "offered";
            string text8 = null;
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["has_then"] = false;
            jsonObject5["origin"] = "start_without_offer";
            ClickyTakeOverController.TrackDrive(text7, text8, jsonObject5);
            return ClickyTakeOverController.Result("offered", "There was no yes to take over yet, so a card is asking for it now. Stop here; don't do any steps until they agree.");
          }
          obj = this.gate;
          lock (obj)
          {
            this.askingTurnTokens = new List<string>();
          }
          string text9 = "start_refused";
          string text10 = null;
          JsonObject jsonObject6 = new JsonObject(null);
          jsonObject6["reason"] = ((phase.Kind == ClickyTakeOverController.PhaseKind.Driving) ? "already_driving" : "no_offer");
          jsonObject6["origin"] = "brain";
          ClickyTakeOverController.TrackDrive(text9, text10, jsonObject6);
          return ClickyTakeOverController.Result("error", "Offer first with offer_screen_takeover; take over only after their yes.");
        }
      }
      else
      {
        if (this.IsDriving)
        {
          string text11 = "offer_refused";
          string text12 = null;
          JsonObject jsonObject7 = new JsonObject(null);
          jsonObject7["reason"] = "already_driving";
          ClickyTakeOverController.TrackDrive(text11, text12, jsonObject7);
          return ClickyTakeOverController.Result("error", "You are already driving.");
        }
        if (this.ChromeSetupIsRunning())
        {
          string text13 = "offer_refused";
          string text14 = null;
          JsonObject jsonObject8 = new JsonObject(null);
          jsonObject8["reason"] = "chrome_setup_running";
          ClickyTakeOverController.TrackDrive(text13, text14, jsonObject8);
          return ClickyTakeOverController.Result("error", "HeyClicky is setting up Chrome on their screen right now. Wait for it.");
        }
        if (this.CurrentPhase.Kind == ClickyTakeOverController.PhaseKind.Offered)
        {
          ClickyTakeOverController.TrackOfferAnswered("replaced");
        }
        string offeredGoal = (string.IsNullOrEmpty(text2) ? "do these steps for you" : text2);
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Offered, offeredGoal, 0);
          this.offerAnsweredBySpeaking = null;
          string text15 = this.currentTurnToken;
          List<string> list;
          if (text15 == null)
          {
            list = new List<string>();
          }
          else
          {
            int num = 1;
            List<string> list2 = new List<string>(num);
            CollectionsMarshal.SetCount<string>(list2, num);
            list = list2;
            Span<string> span = CollectionsMarshal.AsSpan<string>(list2);
            int num2 = 0;
            *span[num2] = text15;
          }
          this.askingTurnTokens = list;
          string text16 = (string)jsonObject2["then"];
          string text17 = ((text16 != null) ? text16.Trim() : null);
          this.offeredThen = ((text17 != null && text17.Length > 0) ? text17 : null);
          flag2 = this.currentTurnProgramSpoke;
        }
        string text18 = (string)jsonObject2["say"];
        string text19 = ((text18 != null) ? text18.Trim() : null);
        if (!flag2 && !string.IsNullOrEmpty(text19))
        {
          this.session.SpeakVerbatimLineAsync((text19.Length > 200) ? text19.Substring(0, 200) : text19, default(CancellationToken));
          obj = this.gate;
          lock (obj)
          {
            this.spokeForSilentTurn = true;
          }
        }
        string text20 = (string.IsNullOrEmpty(text19) ? null : ((text19.Length > 140) ? text19.Substring(0, 140) : text19));
        this.consentCard.Present(offeredGoal, text20, this.Accent(), delegate
        {
          ClickyTakeOverController.TrackOfferAnswered("yes_card");
          object obj3 = this.gate;
          lock (obj3)
          {
            this.pendingAnalyticsOrigin = "offer_card";
          }
          this.Begin(offeredGoal);
        }, delegate
        {
          this.Declined("no_card");
        }, this.PlayChime, delegate
        {
          this.Declined("timed_out");
        }, "Hands off the mouse while I drive. Esc stops me.", "Go ahead", false, 45, null, null);
        obj = this.gate;
        bool flag3;
        lock (obj)
        {
          flag3 = this.offeredThen != null;
        }
        string text21 = "offered";
        string text22 = null;
        JsonObject jsonObject9 = new JsonObject(null);
        jsonObject9["has_then"] = flag3;
        ClickyTakeOverController.TrackDrive(text21, text22, jsonObject9);
        return ClickyTakeOverController.Result("offered", "A card is asking for their yes. Stop here; don't do any steps until they agree.");
      }
    }

    // Token: 0x06001D72 RID: 7538 RVA: 0x005FF298 File Offset: 0x005FF298
    private static string Result(string status, [Nullable(2)] string message = null)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = status;
      JsonObject jsonObject2 = jsonObject;
      if (message != null)
      {
        jsonObject2["message"] = message;
      }
      return jsonObject2.ToJsonString(null);
    }

    // Token: 0x06001D73 RID: 7539 RVA: 0x005FF2E0 File Offset: 0x005FF2E0
    public bool DriveForAgent(string goal, string clickyName, [Nullable(2)] string jobLeaseId, Action<AgentDriveOutcome> onEnded, [Nullable(2)] string threadId = null)
    {
      if (this.CurrentPhase.Kind != ClickyTakeOverController.PhaseKind.Idle || this.ChromeSetupIsRunning())
      {
        string text = "start_refused";
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["reason"] = "busy";
        jsonObject["origin"] = "job_step_approved";
        ClickyTakeOverController.TrackDrive(text, threadId, jsonObject);
        return false;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.agentDriveEnded = onEnded;
        this.agentDriveClickyName = clickyName;
        this.askingTurnTokens = new List<string>();
        this.pendingBilling = new ClickyDriveBilling.Job(jobLeaseId);
        this.pendingAnalyticsOrigin = "job_step_approved";
        this.pendingAnalyticsThreadId = threadId;
      }
      this.Begin(goal);
      if (this.IsDriving)
      {
        return true;
      }
      obj = this.gate;
      lock (obj)
      {
        this.agentDriveEnded = null;
        this.agentDriveClickyName = null;
        this.pendingBilling = null;
        this.pendingAnalyticsOrigin = null;
        this.pendingAnalyticsThreadId = null;
      }
      string text2 = "start_refused";
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["reason"] = "busy";
      jsonObject2["origin"] = "job_step_approved";
      ClickyTakeOverController.TrackDrive(text2, threadId, jsonObject2);
      return false;
    }

    // Token: 0x06001D74 RID: 7540 RVA: 0x005FF458 File Offset: 0x005FF458
    public bool DriveForApp(string goal, [Nullable(2)] string targetProcessName, Action<AgentDriveOutcome> onEnded)
    {
      if (this.CurrentPhase.Kind != ClickyTakeOverController.PhaseKind.Idle)
      {
        string text = "start_refused";
        string text2 = null;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["reason"] = "busy";
        jsonObject["origin"] = "app_setup";
        ClickyTakeOverController.TrackDrive(text, text2, jsonObject);
        return false;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.agentDriveEnded = onEnded;
        this.targetProcessName = targetProcessName;
        this.askingTurnTokens = new List<string>();
        this.pendingBilling = ClickyDriveBilling.SetupDrive;
        this.pendingAnalyticsOrigin = "app_setup";
      }
      this.Begin(goal);
      if (this.IsDriving)
      {
        return true;
      }
      obj = this.gate;
      lock (obj)
      {
        this.agentDriveEnded = null;
        this.targetProcessName = null;
        this.pendingBilling = null;
        this.pendingAnalyticsOrigin = null;
      }
      string text3 = "start_refused";
      string text4 = null;
      JsonObject jsonObject2 = new JsonObject(null);
      jsonObject2["reason"] = "busy";
      jsonObject2["origin"] = "app_setup";
      ClickyTakeOverController.TrackDrive(text3, text4, jsonObject2);
      return false;
    }

    // Token: 0x06001D75 RID: 7541 RVA: 0x005FF5B0 File Offset: 0x005FF5B0
    [NullableContext(2)]
    private DisplayInfo WorkDisplay()
    {
      object obj = this.gate;
      string text;
      lock (obj)
      {
        text = this.targetProcessName;
      }
      IReadOnlyList<DisplayInfo> displays = this.capture.GetDisplays();
      if (text != null)
      {
        RectD? rectD = AppWindowLocator.FrontWindowFrame(text);
        if (rectD != null)
        {
          RectD valueOrDefault = rectD.GetValueOrDefault();
          return AppWindowLocator.Display(valueOrDefault, displays);
        }
      }
      return AppWindowLocator.FrontmostAppDisplay(displays);
    }

    // Token: 0x06001D76 RID: 7542 RVA: 0x005FF62C File Offset: 0x005FF62C
    [NullableContext(2)]
    private ScreenCaptureFrame CaptureForWork([Nullable(1)] IReadOnlyList<ScreenCaptureFrame> captures, ScreenCaptureFrame previous)
    {
      DisplayInfo display = this.WorkDisplay();
      PointD pointer = ClickyScreenActuator.CurrentPointer;
      object obj;
      if ((obj = ((display == null) ? null : captures.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame c) => c.Display.BoundsPx == display.BoundsPx))) == null && (obj = ((previous == null) ? null : captures.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame c) => c.Display.BoundsPx == previous.Display.BoundsPx))) == null && (obj = captures.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame c) => c.Display.BoundsPx.Contains(pointer))) == null)
      {
        obj = captures.FirstOrDefault<ScreenCaptureFrame>((ScreenCaptureFrame c) => c.ContainsCursor) ?? captures.FirstOrDefault<ScreenCaptureFrame>();
      }
      ScreenCaptureFrame screenCaptureFrame = obj;
      if (previous != null && screenCaptureFrame != null && screenCaptureFrame.Display.BoundsPx != previous.Display.BoundsPx && display != null)
      {
        Log.Info("drive", "the work moved to another display");
        object obj2 = this.gate;
        string status;
        lock (obj2)
        {
          status = this.overlayStatus;
        }
        Displays.Monitor monitor = ClickyTakeOverController.MonitorFor(display);
        this.OnUi(delegate
        {
          this.overlay.Show(monitor, this.Accent(), status);
        });
      }
      return screenCaptureFrame;
    }

    // Token: 0x06001D77 RID: 7543 RVA: 0x005FF7A8 File Offset: 0x005FF7A8
    private static Displays.Monitor MonitorFor(DisplayInfo display)
    {
      return Displays.At((int)(display.BoundsPx.X + display.BoundsPx.Width / 2.0), (int)(display.BoundsPx.Y + display.BoundsPx.Height / 2.0));
    }

    // Token: 0x06001D78 RID: 7544 RVA: 0x005FF80C File Offset: 0x005FF80C
    public void VoiceSilentToolTurn(string name, string argumentsJson)
    {
      JsonObject jsonObject2;
      try
      {
        JsonObject jsonObject;
        if ((jsonObject = JsonNode.Parse(argumentsJson, null, default(JsonDocumentOptions)) as JsonObject) == null)
        {
          jsonObject = new JsonObject(null);
        }
        jsonObject2 = jsonObject;
      }
      catch (JsonException)
      {
        jsonObject2 = new JsonObject(null);
      }
      if (name == "offer_screen_takeover")
      {
        object obj = this.gate;
        lock (obj)
        {
          if (this.spokeForSilentTurn)
          {
            this.spokeForSilentTurn = false;
            return;
          }
        }
        this.session.SpeakVerbatimLineAsync("Want me to just do it? I'll drive, you watch.", default(CancellationToken));
        return;
      }
      if (!(name == "end_screen_takeover"))
      {
        return;
      }
      string text = (string)jsonObject2["summary"];
      if (text != null && text.Length > 0)
      {
        this.session.SpeakVerbatimLineAsync(ClickyDriveLines.SummaryFirstSentence(text), default(CancellationToken));
      }
    }

    // Token: 0x06001D79 RID: 7545 RVA: 0x005FF91C File Offset: 0x005FF91C
    private void Declined(string answer)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.phase.Kind != ClickyTakeOverController.PhaseKind.Offered)
        {
          return;
        }
        this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Idle, "", 0);
        this.offeredThen = null;
        this.askingTurnTokens = new List<string>();
      }
      ClickyTakeOverController.TrackOfferAnswered(answer);
      Log.Info("drive", "declined");
      this.session.SubmitFactAsync("[App] The user chose to do it themselves (they declined the take-over). Keep guiding them.", false);
    }

    // Token: 0x06001D7A RID: 7546 RVA: 0x005FF9B4 File Offset: 0x005FF9B4
    public void Begin(string goal)
    {
      ClickyTakeOverController.<>c__DisplayClass116_0 CS$<>8__locals1 = new ClickyTakeOverController.<>c__DisplayClass116_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.goal = goal;
      if (!this.ui.HasThreadAccess)
      {
        this.ui.TryEnqueue(delegate
        {
          CS$<>8__locals1.<>4__this.Begin(CS$<>8__locals1.goal);
        });
        return;
      }
      if (this.IsDriving)
      {
        return;
      }
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        flag2 = this.agentDriveEnded == null;
      }
      if (flag2)
      {
        AgentDriveBinding agentDriveBinding = this.MacStepBindingForBrainDrive(CS$<>8__locals1.goal);
        if (agentDriveBinding != null)
        {
          obj = this.gate;
          lock (obj)
          {
            this.agentDriveEnded = agentDriveBinding.OnEnded;
            this.agentDriveClickyName = agentDriveBinding.ClickyName;
            this.pendingBilling = new ClickyDriveBilling.Job(agentDriveBinding.JobLeaseId);
            this.pendingAnalyticsThreadId = agentDriveBinding.ThreadId;
          }
        }
      }
      obj = this.gate;
      string text;
      JsonObject jsonObject;
      lock (obj)
      {
        this.driveBilling = this.pendingBilling ?? ClickyDriveBilling.UserDrive;
        this.pendingBilling = null;
        this.driveIdentifier = Guid.NewGuid().ToString().ToUpperInvariant();
        this.driveAskingTurnTokens = ((this.driveBilling is ClickyDriveBilling.User) ? this.askingTurnTokens : new List<string>());
        this.askingTurnTokens = new List<string>();
        this.driveStartedAt = DateTimeOffset.Now;
        this.driveLeaseId = null;
        this.driveBillingSent = false;
        this.driveThen = ((this.agentDriveEnded == null) ? this.offeredThen : null);
        this.offeredThen = null;
        this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Driving, CS$<>8__locals1.goal, 0);
        this.endSummary = null;
        this.actionLog.Clear();
        this.overlayStatus = "Getting started";
        CancellationTokenSource cancellationTokenSource = this.loopCancellation;
        if (cancellationTokenSource != null)
        {
          cancellationTokenSource.Cancel();
        }
        CS$<>8__locals1.cancellation = (this.loopCancellation = new CancellationTokenSource());
        this.driveAnalyticsOrigin = this.pendingAnalyticsOrigin ?? "offer_card";
        this.driveAnalyticsThreadId = this.pendingAnalyticsThreadId;
        this.pendingAnalyticsOrigin = null;
        this.pendingAnalyticsThreadId = null;
        this.driveTakenBackBy = null;
        this.hasClickyActedThisDrive = false;
        this.userMousePauseCount = 0;
        text = this.driveAnalyticsThreadId;
        jsonObject = ClickyDriveAnalytics.StartedProperties(this.driveIdentifier, this.driveAnalyticsOrigin, this.driveBilling, this.agentDriveEnded != null && !(this.driveBilling is ClickyDriveBilling.Setup), this.driveThen != null);
      }
      ClickyTakeOverController.TrackDrive("started", text, jsonObject);
      this.consentCard.Dismiss(false);
      ClickyDriveState.IsDrivingNow = true;
      Log.Info("drive", "begin: " + CS$<>8__locals1.goal);
      this.session.ClearDrawingForDrive();
      DisplayInfo displayInfo = this.WorkDisplay();
      this.overlay.Show((displayInfo == null) ? Displays.UnderCursor() : ClickyTakeOverController.MonitorFor(displayInfo), this.Accent(), "Getting started");
      this.PlayChime("agent-launch", 0.5);
      obj = this.gate;
      lock (obj)
      {
        this.movesIgnoredUntil = DateTimeOffset.Now + ClickyTakeOverController.BeginMoveGrace;
      }
      this.StartWatchingUserInput();
      Task.Run(delegate
      {
        ClickyTakeOverController.<>c__DisplayClass116_0.<<Begin>b__1>d <<Begin>b__1>d;
        <<Begin>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<Begin>b__1>d.<>4__this = CS$<>8__locals1;
        <<Begin>b__1>d.<>1__state = -1;
        <<Begin>b__1>d.<>t__builder.Start<ClickyTakeOverController.<>c__DisplayClass116_0.<<Begin>b__1>d>(ref <<Begin>b__1>d);
        return <<Begin>b__1>d.<>t__builder.Task;
      });
    }

    // Token: 0x06001D7B RID: 7547 RVA: 0x005FFD88 File Offset: 0x005FFD88
    private async Task DriveAsync(string goal)
    {
      if (this.IsDriving)
      {
        IReadOnlyList<ScreenCaptureFrame> readOnlyList;
        try
        {
          readOnlyList = await this.capture.CaptureAllAtDisplayPointSizeAsync(80, default(CancellationToken)).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          Log.Warn("drive", "capture failed: " + ex.Message);
          readOnlyList = Array.Empty<ScreenCaptureFrame>();
        }
        ScreenCaptureFrame screenCaptureFrame = this.CaptureForWork(readOnlyList, null);
        if (screenCaptureFrame == null)
        {
          await this.InvokeOnUi<bool>(delegate
          {
            object obj2 = this.gate;
            lock (obj2)
            {
              this.driveEnding = AgentDriveEnding.CouldNotSee;
            }
            this.Finish("I can't see the screen right now.", "agent-done");
            return true;
          }).ConfigureAwait(false);
        }
        else
        {
          object obj = this.gate;
          string text;
          lock (obj)
          {
            this.stepCapture = screenCaptureFrame;
            this.stepDisplays = this.capture.GetDisplays();
            text = this.agentDriveClickyName;
          }
          await this.session.RunDriveTurnAsync(ClickyDriveLines.DriveFact(goal, text), screenCaptureFrame, this.stepDisplays).ConfigureAwait(false);
          await this.InvokeOnUi<bool>(delegate
          {
            if (!this.IsDriving)
            {
              return false;
            }
            object obj3 = this.gate;
            AgentDriveEnding agentDriveEnding;
            lock (obj3)
            {
              agentDriveEnding = this.driveEnding;
            }
            if (agentDriveEnding == AgentDriveEnding.Failed)
            {
              this.Finish("I lost the connection partway, so I stopped there.", "agent-close");
            }
            else
            {
              this.Finish(null, "agent-done");
            }
            return true;
          }).ConfigureAwait(false);
        }
      }
    }

    // Token: 0x06001D7C RID: 7548 RVA: 0x005FFDD4 File Offset: 0x005FFDD4
    public void NoteSpokenStep(string sentence)
    {
      if (!this.IsDriving)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.overlayStatus = sentence;
      }
      this.OnUi(delegate
      {
        this.overlay.SetStatus(sentence);
      });
    }

    // Token: 0x06001D7D RID: 7549 RVA: 0x005FFE4C File Offset: 0x005FFE4C
    [return: Nullable(new byte[] { 1, 2 })]
    public async Task<ScreenCaptureFrame> PerformComputerActionsAsync(IReadOnlyList<JsonObject> actions, int audioPlaybackBeganBefore)
    {
      ClickyTakeOverController.<>c__DisplayClass119_0 CS$<>8__locals1 = new ClickyTakeOverController.<>c__DisplayClass119_0();
      CS$<>8__locals1.<>4__this = this;
      object obj = this.gate;
      ScreenCaptureFrame frame;
      lock (obj)
      {
        this.lastCallResults.Clear();
        frame = this.stepCapture;
      }
      ScreenCaptureFrame screenCaptureFrame;
      if (!this.IsDriving || frame == null)
      {
        screenCaptureFrame = null;
      }
      else if (ClickyDriveActions.IsObservationOnly(actions))
      {
        foreach (JsonObject jsonObject in actions)
        {
          Log.Info("drive", ClickyDriveActions.Describe(jsonObject));
          await this.PerformAsync(jsonObject, frame).ConfigureAwait(false);
        }
        IEnumerator<JsonObject> enumerator = null;
        if (!this.IsDriving)
        {
          screenCaptureFrame = null;
        }
        else
        {
          ScreenCaptureFrame screenCaptureFrame2 = this.CaptureForWork(await this.CaptureAllAsync().ConfigureAwait(false), frame);
          obj = this.gate;
          lock (obj)
          {
            this.stepCapture = screenCaptureFrame2;
          }
          screenCaptureFrame = (this.IsDriving ? screenCaptureFrame2 : null);
        }
      }
      else
      {
        obj = this.gate;
        lock (obj)
        {
          if (this.phase.Kind == ClickyTakeOverController.PhaseKind.Driving)
          {
            ClickyTakeOverController.Phase phase = this.phase;
            phase.Step = this.phase.Step + 1;
            this.phase = phase;
          }
        }
        DateTimeOffset lineDeadline = DateTimeOffset.Now.AddSeconds(1.6);
        while (this.session.AudioPlaybackBeganCount <= audioPlaybackBeganBefore && DateTimeOffset.Now < lineDeadline)
        {
          await Task.Delay(50).ConfigureAwait(false);
        }
        await this.WaitForUserMouseToRestAsync().ConfigureAwait(false);
        if (!this.IsDriving)
        {
          screenCaptureFrame = null;
        }
        else
        {
          obj = this.gate;
          lock (obj)
          {
            this.actuatingUntil = DateTimeOffset.Now.AddSeconds(30.0);
            this.hasClickyActedThisDrive = true;
          }
          this.OnUi(delegate
          {
            CS$<>8__locals1.<>4__this.overlay.SetActing(true);
          });
          try
          {
            foreach (JsonObject jsonObject2 in actions)
            {
              if (!this.IsDriving)
              {
                return null;
              }
              Log.Info("drive", ClickyDriveActions.Describe(jsonObject2));
              await this.PerformAsync(jsonObject2, frame).ConfigureAwait(false);
            }
            IEnumerator<JsonObject> enumerator = null;
          }
          finally
          {
            obj = this.gate;
            bool flag = false;
            try
            {
              Monitor.Enter(obj, ref flag);
              this.actuatingUntil = DateTimeOffset.Now.AddSeconds(0.3);
              this.userMouse.ForgetRun();
            }
            finally
            {
              int num;
              if (num < 0 && flag)
              {
                Monitor.Exit(obj);
              }
            }
            this.OnUi(delegate
            {
              CS$<>8__locals1.<>4__this.overlay.SetActing(false);
            });
          }
          if (!this.IsDriving)
          {
            screenCaptureFrame = null;
          }
          else
          {
            CS$<>8__locals1.settleDisplay = frame.Display.BoundsPx;
            try
            {
              await ScreenSettle.WaitForScreenToSettleAsync(delegate
              {
                ClickyTakeOverController.<>c__DisplayClass119_0.<<PerformComputerActionsAsync>b__2>d <<PerformComputerActionsAsync>b__2>d;
                <<PerformComputerActionsAsync>b__2>d.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
                <<PerformComputerActionsAsync>b__2>d.<>4__this = CS$<>8__locals1;
                <<PerformComputerActionsAsync>b__2>d.<>1__state = -1;
                <<PerformComputerActionsAsync>b__2>d.<>t__builder.Start<ClickyTakeOverController.<>c__DisplayClass119_0.<<PerformComputerActionsAsync>b__2>d>(ref <<PerformComputerActionsAsync>b__2>d);
                return <<PerformComputerActionsAsync>b__2>d.<>t__builder.Task;
              }, TimeSpan.FromMilliseconds(600L, 0L), CancellationToken.None).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
              Log.Warn("drive", "settle: " + ex.Message);
            }
            if (!this.IsDriving)
            {
              screenCaptureFrame = null;
            }
            else
            {
              ScreenCaptureFrame screenCaptureFrame3 = this.CaptureForWork(await this.CaptureAllAsync().ConfigureAwait(false), frame);
              obj = this.gate;
              lock (obj)
              {
                this.stepCapture = screenCaptureFrame3;
              }
              screenCaptureFrame = screenCaptureFrame3;
            }
          }
        }
      }
      return screenCaptureFrame;
    }

    // Token: 0x06001D7E RID: 7550 RVA: 0x005FFEA0 File Offset: 0x005FFEA0
    private async Task<IReadOnlyList<ScreenCaptureFrame>> CaptureAllAsync()
    {
      IReadOnlyList<ScreenCaptureFrame> readOnlyList;
      try
      {
        readOnlyList = await this.capture.CaptureAllAtDisplayPointSizeAsync(80, default(CancellationToken)).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "capture failed: " + ex.Message);
        readOnlyList = Array.Empty<ScreenCaptureFrame>();
      }
      return readOnlyList;
    }

    // Token: 0x06001D7F RID: 7551 RVA: 0x005FFEE4 File Offset: 0x005FFEE4
    private void NoteCallResult(JsonObject action, JsonObject fields)
    {
      double? num = ClickyDriveActions.Number(action, "claude_call_index");
      if (num != null)
      {
        double valueOrDefault = num.GetValueOrDefault();
        int callIndex = (int)valueOrDefault;
        object obj = this.gate;
        lock (obj)
        {
          if (!this.lastCallResults.Any<JsonObject>(delegate(JsonObject r)
          {
            int? num2 = (int?)r["call_index"];
            int callIndex2 = callIndex;
            return (num2.GetValueOrDefault() == callIndex2) & (num2 != null);
          }))
          {
            fields["call_index"] = callIndex;
            this.lastCallResults.Add(fields);
          }
        }
        return;
      }
    }

    // Token: 0x06001D80 RID: 7552 RVA: 0x005FFF8C File Offset: 0x005FFF8C
    private void NoteRefused(JsonObject action)
    {
      Log.Warn("drive", ClickyDriveActions.TypeOf(action) + ": the window in front refused HeyClicky's input, so it stopped");
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["text"] = "The window in front refuses HeyClicky's input (an app running as administrator), so this stopped. No key was left held down.";
      jsonObject["error"] = true;
      this.NoteCallResult(action, jsonObject);
    }

    // Token: 0x06001D81 RID: 7553 RVA: 0x005FFFF0 File Offset: 0x005FFFF0
    private void AddToActionLog(string line)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.actionLog.Add(line);
      }
    }

    // Token: 0x06001D82 RID: 7554 RVA: 0x00600038 File Offset: 0x00600038
    private async Task PerformAsync(JsonObject action, ScreenCaptureFrame frame)
    {
      ClickyTakeOverController.<>c__DisplayClass124_0 CS$<>8__locals1 = new ClickyTakeOverController.<>c__DisplayClass124_0();
      CS$<>8__locals1.frame = frame;
      string type = ClickyDriveActions.TypeOf(action);
      ClickyModifierKeys modifiers = ClickyModifierKeys.None;
      foreach (string text2 in ClickyDriveActions.Keys(action))
      {
        ClickyModifierKeys? clickyModifierKeys = ClickyKeyNames.Modifier(text2);
        ClickyModifierKeys valueOrDefault;
        bool flag;
        if (clickyModifierKeys != null)
        {
          valueOrDefault = clickyModifierKeys.GetValueOrDefault();
          flag = true;
        }
        else
        {
          flag = false;
        }
        bool flag2 = flag;
        if (flag2)
        {
          bool flag3 = text2 == "cmd" || text2 == "ctrl" || text2 == "opt" || text2 == "shift" || text2 == "win";
          flag2 = flag3;
        }
        if (flag2)
        {
          modifiers |= valueOrDefault;
        }
      }
      string text3 = type;
      string text;
      if (text3 != null)
      {
        ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter configuredTaskAwaiter;
        ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
        PointD? pointD;
        switch (text3.Length)
        {
        case 4:
        {
          char c = text3[0];
          if (c <= 'm')
          {
            if (c != 'd')
            {
              if (c != 'm')
              {
                goto IL_0EF0;
              }
              if (!(text3 == "move"))
              {
                goto IL_0EF0;
              }
            }
            else
            {
              if (!(text3 == "drag"))
              {
                goto IL_0EF0;
              }
              JsonArray jsonArray = action["path"] as JsonArray;
              List<PointD> list;
              if (jsonArray == null)
              {
                list = null;
              }
              else
              {
                list = (from step in jsonArray.OfType<JsonObject>()
                  select base.<PerformAsync>g__Point|0(ClickyDriveActions.Number(step, "x"), ClickyDriveActions.Number(step, "y")) into p
                  where p != null
                  select p.Value).ToList<PointD>();
              }
              List<PointD> path = list ?? new List<PointD>();
              if (path.Count < 2)
              {
                return;
              }
              await ClickyScreenActuator.GlideAsync(path[0], 0.45, default(CancellationToken)).ConfigureAwait(false);
              if (!this.IsDriving)
              {
                return;
              }
              configuredTaskAwaiter = ClickyScreenActuator.DragAsync(path).ConfigureAwait(false).GetAwaiter();
              if (!configuredTaskAwaiter.IsCompleted)
              {
                await configuredTaskAwaiter;
                configuredTaskAwaiter = configuredTaskAwaiter2;
                configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter);
              }
              if (configuredTaskAwaiter.GetResult() == ClickyActuation.Refused)
              {
                this.NoteRefused(action);
                goto IL_0EF0;
              }
              this.AddToActionLog("drag");
              goto IL_0EF0;
            }
          }
          else if (c != 't')
          {
            if (c != 'w')
            {
              if (c != 'z')
              {
                goto IL_0EF0;
              }
              if (!(text3 == "zoom"))
              {
                goto IL_0EF0;
              }
              RectD? rectD = ClickyDriveActions.ZoomRegionPx(action, CS$<>8__locals1.frame);
              if (rectD != null)
              {
                RectD valueOrDefault2 = rectD.GetValueOrDefault();
                try
                {
                  object obj = await this.capture.CaptureRegionAsync(CS$<>8__locals1.frame.Display, valueOrDefault2, 1600, default(CancellationToken)).ConfigureAwait(false);
                  byte[] item = obj.Item1;
                  int item2 = obj.Item2;
                  int item3 = obj.Item3;
                  JsonObject jsonObject = new JsonObject(null);
                  jsonObject["image_url"] = "data:image/jpeg;base64," + Convert.ToBase64String(item);
                  this.NoteCallResult(action, jsonObject);
                  string text4 = "drive";
                  DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 2);
                  defaultInterpolatedStringHandler.AppendLiteral("zoomed → ");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(item2);
                  defaultInterpolatedStringHandler.AppendLiteral("×");
                  defaultInterpolatedStringHandler.AppendFormatted<int>(item3);
                  Log.Info(text4, defaultInterpolatedStringHandler.ToStringAndClear());
                }
                catch (Exception ex)
                {
                  JsonObject jsonObject2 = new JsonObject(null);
                  jsonObject2["text"] = "Zoom failed: " + ex.Message;
                  jsonObject2["error"] = true;
                  this.NoteCallResult(action, jsonObject2);
                }
                goto IL_0EF0;
              }
              JsonObject jsonObject3 = new JsonObject(null);
              jsonObject3["text"] = "Zoom needs [x1, y1, x2, y2].";
              jsonObject3["error"] = true;
              this.NoteCallResult(action, jsonObject3);
              goto IL_0EF0;
            }
            else
            {
              if (!(text3 == "wait"))
              {
                goto IL_0EF0;
              }
              await Task.Delay(1000).ConfigureAwait(false);
              goto IL_0EF0;
            }
          }
          else
          {
            if (!(text3 == "type"))
            {
              goto IL_0EF0;
            }
            text = (string)action["text"];
            if (text == null || text.Length <= 0)
            {
              goto IL_0EF0;
            }
            CancellationTokenSource cancellationTokenSource = this.loopCancellation;
            configuredTaskAwaiter = ClickyScreenActuator.TypeAsync(text, (cancellationTokenSource != null) ? cancellationTokenSource.Token : new CancellationToken(true)).ConfigureAwait(false).GetAwaiter();
            if (!configuredTaskAwaiter.IsCompleted)
            {
              await configuredTaskAwaiter;
              configuredTaskAwaiter = configuredTaskAwaiter2;
              configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter);
            }
            if (configuredTaskAwaiter.GetResult() == ClickyActuation.Refused)
            {
              this.NoteRefused(action);
              goto IL_0EF0;
            }
            this.AddToActionLog(ClickyDriveActions.TypedLogLine(text));
            goto IL_0EF0;
          }
          break;
        }
        case 5:
          if (!(text3 == "click"))
          {
            goto IL_0EF0;
          }
          break;
        case 6:
          if (!(text3 == "scroll"))
          {
            goto IL_0EF0;
          }
          pointD = CS$<>8__locals1.<PerformAsync>g__Point|0(ClickyDriveActions.Number(action, "x"), ClickyDriveActions.Number(action, "y"));
          if (pointD != null)
          {
            PointD valueOrDefault3 = pointD.GetValueOrDefault();
            await ClickyScreenActuator.GlideAsync(valueOrDefault3, 0.3, default(CancellationToken)).ConfigureAwait(false);
          }
          configuredTaskAwaiter = ClickyScreenActuator.ScrollAsync(ClickyDriveActions.ScrollLines(ClickyDriveActions.Number(action, "scroll_y").GetValueOrDefault())).ConfigureAwait(false).GetAwaiter();
          if (!configuredTaskAwaiter.IsCompleted)
          {
            await configuredTaskAwaiter;
            configuredTaskAwaiter = configuredTaskAwaiter2;
            configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter);
          }
          if (configuredTaskAwaiter.GetResult() == ClickyActuation.Refused)
          {
            this.NoteRefused(action);
            goto IL_0EF0;
          }
          this.AddToActionLog("scroll");
          goto IL_0EF0;
        case 7:
        case 9:
        case 11:
        case 13:
        case 14:
          goto IL_0EF0;
        case 8:
        {
          char c = text3[0];
          if (c != 'h')
          {
            if (c != 'k')
            {
              if (c != 'm')
              {
                goto IL_0EF0;
              }
              if (!(text3 == "mouse_up"))
              {
                goto IL_0EF0;
              }
              goto IL_0B3E;
            }
            else
            {
              if (!(text3 == "keypress"))
              {
                goto IL_0EF0;
              }
              List<string> list2 = ClickyDriveActions.Keys(action);
              string chord = string.Join("+", list2);
              ClickyActuation clickyActuation = ((list2.Count <= 0) ? ClickyActuation.UnknownKey : (await ClickyScreenActuator.PressAsync(chord).ConfigureAwait(false)));
              if (clickyActuation == ClickyActuation.Done)
              {
                this.AddToActionLog("pressed " + chord);
                goto IL_0EF0;
              }
              if (clickyActuation == ClickyActuation.Refused)
              {
                this.NoteRefused(action);
                goto IL_0EF0;
              }
              JsonObject jsonObject4 = new JsonObject(null);
              jsonObject4["text"] = "Unknown key: " + chord + ". Nothing was pressed.";
              jsonObject4["error"] = true;
              this.NoteCallResult(action, jsonObject4);
              goto IL_0EF0;
            }
          }
          else
          {
            if (!(text3 == "hold_key"))
            {
              goto IL_0EF0;
            }
            List<string> list3 = ClickyDriveActions.Keys(action);
            string chord = string.Join("+", list3);
            ClickyActuation clickyActuation2 = await ClickyScreenActuator.HoldAsync(list3, ClickyDriveActions.Number(action, "seconds").GetValueOrDefault(1.0)).ConfigureAwait(false);
            if (clickyActuation2 == ClickyActuation.Done)
            {
              this.AddToActionLog("held " + chord);
              goto IL_0EF0;
            }
            if (clickyActuation2 == ClickyActuation.Refused)
            {
              this.NoteRefused(action);
              goto IL_0EF0;
            }
            JsonObject jsonObject5 = new JsonObject(null);
            jsonObject5["text"] = "Unknown key: " + chord + ". Nothing was held.";
            jsonObject5["error"] = true;
            this.NoteCallResult(action, jsonObject5);
            goto IL_0EF0;
          }
          break;
        }
        case 10:
          if (!(text3 == "mouse_down"))
          {
            goto IL_0EF0;
          }
          goto IL_0B3E;
        case 12:
        {
          char c = text3[0];
          if (c != 'd')
          {
            if (c != 't')
            {
              goto IL_0EF0;
            }
            if (!(text3 == "triple_click"))
            {
              goto IL_0EF0;
            }
          }
          else if (!(text3 == "double_click"))
          {
            goto IL_0EF0;
          }
          break;
        }
        case 15:
        {
          if (!(text3 == "cursor_position"))
          {
            goto IL_0EF0;
          }
          JsonObject jsonObject6 = new JsonObject(null);
          jsonObject6["text"] = ClickyDriveActions.CursorPositionText(ClickyScreenActuator.CurrentPointer, CS$<>8__locals1.frame);
          this.NoteCallResult(action, jsonObject6);
          goto IL_0EF0;
        }
        default:
          goto IL_0EF0;
        }
        pointD = CS$<>8__locals1.<PerformAsync>g__Point|0(ClickyDriveActions.Number(action, "x"), ClickyDriveActions.Number(action, "y"));
        PointD? pointD2 = ((pointD != null) ? pointD : ((type == "move") ? null : new PointD?(ClickyScreenActuator.CurrentPointer)));
        if (pointD2 == null)
        {
          return;
        }
        PointD point = pointD2.GetValueOrDefault();
        await ClickyScreenActuator.GlideAsync(point, 0.45, default(CancellationToken)).ConfigureAwait(false);
        if (!this.IsDriving || type == "move")
        {
          goto IL_0EF0;
        }
        string text5 = (((string)action["button"]) ?? "left").ToLowerInvariant();
        int num = ((type == "triple_click") ? 3 : ((type == "double_click") ? 2 : 1));
        ClickyMouseButton clickyMouseButton = ((text5 == "right") ? ClickyMouseButton.Right : ((text5 == "middle") ? ClickyMouseButton.Middle : ClickyMouseButton.Left));
        configuredTaskAwaiter = ClickyScreenActuator.ClickAsync(point, clickyMouseButton, num, modifiers).ConfigureAwait(false).GetAwaiter();
        if (!configuredTaskAwaiter.IsCompleted)
        {
          await configuredTaskAwaiter;
          configuredTaskAwaiter = configuredTaskAwaiter2;
          configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<ClickyActuation>.ConfiguredTaskAwaiter);
        }
        if (configuredTaskAwaiter.GetResult() == ClickyActuation.Refused)
        {
          this.NoteRefused(action);
          goto IL_0EF0;
        }
        this.AddToActionLog(ClickyDriveActions.ClickLogLine(type, action));
        goto IL_0EF0;
        IL_0B3E:
        if (ClickyScreenActuator.SetLeftButton(type == "mouse_down") == ClickyActuation.Refused)
        {
          this.NoteRefused(action);
        }
        else
        {
          this.AddToActionLog((type == "mouse_down") ? "mouse down" : "mouse up");
        }
      }
      IL_0EF0:
      text = null;
      await Task.Delay(120).ConfigureAwait(false);
    }

    // Token: 0x06001D83 RID: 7555 RVA: 0x0060008C File Offset: 0x0060008C
    public void StopForSafetyCheck(IReadOnlyList<JsonObject> checks)
    {
      this.OnUi(delegate
      {
        if (!this.IsDriving)
        {
          return;
        }
        JsonObject jsonObject = checks.FirstOrDefault<JsonObject>();
        string text = ((string)((jsonObject != null) ? jsonObject["message"] : null)) ?? "something on screen needs your decision";
        Log.Info("drive", "safety check: " + text);
        object obj = this.gate;
        lock (obj)
        {
          this.driveEnding = AgentDriveEnding.SafetyStop;
        }
        this.Finish(ClickyDriveLines.SafetyStop(text), "agent-close");
      });
    }

    // Token: 0x06001D84 RID: 7556 RVA: 0x006000C0 File Offset: 0x006000C0
    public bool HandleEscape()
    {
      if (this.CurrentPhase.Kind == ClickyTakeOverController.PhaseKind.Idle)
      {
        return false;
      }
      this.OnUi(delegate
      {
        ClickyTakeOverController.PhaseKind kind = this.CurrentPhase.Kind;
        if (kind != ClickyTakeOverController.PhaseKind.Offered)
        {
          if (kind == ClickyTakeOverController.PhaseKind.Driving)
          {
            this.StopForUser("escape");
            return;
          }
        }
        else
        {
          this.consentCard.Dismiss(true);
          this.Declined("no_escape");
        }
      });
      return true;
    }

    // Token: 0x17000541 RID: 1345
    // (get) Token: 0x06001D85 RID: 7557 RVA: 0x006000F4 File Offset: 0x006000F4
    public bool ClaimsEscape
    {
      get
      {
        return this.CurrentPhase.Kind > ClickyTakeOverController.PhaseKind.Idle;
      }
    }

    // Token: 0x06001D86 RID: 7558 RVA: 0x00600114 File Offset: 0x00600114
    private void StartWatchingUserInput()
    {
      ClickyTakeBackWatch clickyTakeBackWatch = this.userInputWatch;
      if (clickyTakeBackWatch != null)
      {
        clickyTakeBackWatch.Dispose();
      }
      this.userInputWatch = new ClickyTakeBackWatch(delegate(ClickyTakeBackWatch.Kind kind, PointD pointer)
      {
        if (!this.IsDriving)
        {
          return;
        }
        bool nudge = false;
        bool flag = false;
        object obj = this.gate;
        lock (obj)
        {
          DateTimeOffset now = DateTimeOffset.Now;
          if (now < this.actuatingUntil)
          {
            return;
          }
          ClickyTakeBackWatch.Kind kind2 = kind;
          bool flag3 = kind2 <= ClickyTakeBackWatch.Kind.Press;
          if (flag3 && now < this.movesIgnoredUntil)
          {
            return;
          }
          if (kind == ClickyTakeBackWatch.Kind.Move)
          {
            double num = Math.Max(0.5, Displays.At((int)pointer.X, (int)pointer.Y).Scale);
            ClickyDriveUserMouse.Verdict verdict = this.userMouse.NoteMove(pointer.X / num, pointer.Y / num, now, this.hasClickyActedThisDrive);
            if (!verdict.TakeOver)
            {
              nudge = verdict.Nudge;
              flag = this.isShowingUserMouseHint;
              if (!nudge && !flag)
              {
                return;
              }
            }
          }
        }
        if (nudge || flag)
        {
          this.OnUi(delegate
          {
            this.ShowUserMouseHint(nudge);
          });
          return;
        }
        this.OnUi(delegate
        {
          this.StopForUser(ClickyTakeOverController.TakeBackReason(kind));
        });
      });
    }

    // Token: 0x06001D87 RID: 7559 RVA: 0x00600140 File Offset: 0x00600140
    private static string TakeBackReason(ClickyTakeBackWatch.Kind kind)
    {
      string text;
      switch (kind)
      {
      case ClickyTakeBackWatch.Kind.Move:
        text = "mouse_move";
        break;
      case ClickyTakeBackWatch.Kind.Press:
        text = "click";
        break;
      case ClickyTakeBackWatch.Kind.Key:
        text = "key";
        break;
      case ClickyTakeBackWatch.Kind.Wheel:
        text = "scroll";
        break;
      default:
        text = "other";
        break;
      }
      return text;
    }

    // Token: 0x06001D88 RID: 7560 RVA: 0x0060018C File Offset: 0x0060018C
    private void ShowUserMouseHint(bool nudge)
    {
      if (!this.IsDriving)
      {
        return;
      }
      if (nudge)
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = !this.isShowingUserMouseHint;
          this.isShowingUserMouseHint = true;
          if (flag2)
          {
            this.userMousePauseCount++;
          }
        }
        if (flag2)
        {
          Log.Info("drive", "the user moved the mouse: waiting for it to rest");
        }
        this.overlay.SetWaitingForUserMouse(true, true);
      }
      CancellationTokenSource cancellationTokenSource = this.userMouseHintLinger;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource linger = (this.userMouseHintLinger = new CancellationTokenSource());
      Action <>9__1;
      Task.Delay(ClickyDriveUserMouse.HintLinger, linger.Token).ContinueWith(delegate(Task task)
      {
        if (task.IsCanceled)
        {
          return;
        }
        ClickyTakeOverController <>4__this = this;
        Action action;
        if ((action = <>9__1) == null)
        {
          action = (<>9__1 = delegate
          {
            if (linger.IsCancellationRequested)
            {
              return;
            }
            object obj2 = this.gate;
            lock (obj2)
            {
              if (!this.isShowingUserMouseHint)
              {
                return;
              }
              this.isShowingUserMouseHint = false;
            }
            this.overlay.SetWaitingForUserMouse(false, false);
          });
        }
        <>4__this.OnUi(action);
      }, TaskScheduler.Default);
    }

    // Token: 0x06001D89 RID: 7561 RVA: 0x00600274 File Offset: 0x00600274
    private async Task WaitForUserMouseToRestAsync()
    {
      DateTimeOffset waitStart = DateTimeOffset.Now;
      while (this.IsDriving)
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.userMouse.IsResting(DateTimeOffset.Now);
        }
        if (!flag2)
        {
          if (!(DateTimeOffset.Now - waitStart >= ClickyDriveUserMouse.WaitCap))
          {
            await Task.Delay(100).ConfigureAwait(false);
            continue;
          }
          await this.InvokeOnUi<bool>(delegate
          {
            this.StopForUser("mouse_move");
            return true;
          }).ConfigureAwait(false);
        }
        return;
      }
    }

    // Token: 0x06001D8A RID: 7562 RVA: 0x006002B8 File Offset: 0x006002B8
    private void StopWatchingUserInput()
    {
      ClickyTakeBackWatch clickyTakeBackWatch = this.userInputWatch;
      if (clickyTakeBackWatch != null)
      {
        clickyTakeBackWatch.Dispose();
      }
      this.userInputWatch = null;
      CancellationTokenSource cancellationTokenSource = this.userMouseHintLinger;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.userMouseHintLinger = null;
      object obj = this.gate;
      lock (obj)
      {
        this.userMouse.Reset();
        this.isShowingUserMouseHint = false;
      }
    }

    // Token: 0x06001D8B RID: 7563 RVA: 0x00600334 File Offset: 0x00600334
    public void StopBecauseUserSpoke()
    {
      if (this.CurrentPhase.Kind == ClickyTakeOverController.PhaseKind.Idle)
      {
        return;
      }
      this.OnUi(delegate
      {
        ClickyTakeOverController.PhaseKind kind = this.CurrentPhase.Kind;
        object obj;
        if (kind == ClickyTakeOverController.PhaseKind.Offered)
        {
          ClickyTakeOverController.TrackOfferAnswered("dismissed_by_voice");
          this.consentCard.Dismiss(true);
          obj = this.gate;
          lock (obj)
          {
            this.offerAnsweredBySpeaking = new ValueTuple<string, DateTimeOffset>?(new ValueTuple<string, DateTimeOffset>(this.phase.Goal, DateTimeOffset.Now));
            this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Idle, "", 0);
          }
          return;
        }
        if (kind != ClickyTakeOverController.PhaseKind.Driving)
        {
          return;
        }
        Log.Info("drive", "the user started talking: handing back");
        obj = this.gate;
        lock (obj)
        {
          this.driveEnding = AgentDriveEnding.UserTookOver;
          this.driveTakenBackBy = "voice";
        }
        this.session.InterruptForDrive();
        this.Teardown();
      });
    }

    // Token: 0x06001D8C RID: 7564 RVA: 0x00600364 File Offset: 0x00600364
    private void StopForUser(string reason)
    {
      if (!this.IsDriving)
      {
        return;
      }
      Log.Info("drive", "the user took back control (" + reason + ")");
      object obj = this.gate;
      lock (obj)
      {
        this.driveEnding = AgentDriveEnding.UserTookOver;
        this.driveTakenBackBy = reason;
      }
      this.session.InterruptForDrive();
      this.PlayChime("agent-close", 0.5);
      this.Teardown();
      this.session.SpeakVerbatimLineAsync("Okay, it's all yours.", default(CancellationToken));
    }

    // Token: 0x06001D8D RID: 7565 RVA: 0x00600414 File Offset: 0x00600414
    private void Finish([Nullable(2)] string line, string chime = "agent-done")
    {
      if (!this.IsDriving)
      {
        return;
      }
      Log.Info("drive", "finished" + ((line == null) ? "" : (": " + line)));
      this.PlayChime(chime, 0.55);
      this.Teardown();
      if (line != null)
      {
        this.session.SpeakVerbatimLineAsync(line, default(CancellationToken));
      }
    }

    // Token: 0x06001D8E RID: 7566 RVA: 0x00600488 File Offset: 0x00600488
    private void Teardown()
    {
      ClickyScreenActuator.ReleaseHeldButton();
      ClickyScreenActuator.ReleaseStrandedKeysAfterDrive("the drive ended");
      object obj = this.gate;
      int num;
      string text;
      string text2;
      string text3;
      string text4;
      int num2;
      AgentDriveEnding agentDriveEnding;
      ClickyDriveBilling clickyDriveBilling;
      string text5;
      DateTimeOffset dateTimeOffset;
      Action<AgentDriveOutcome> action;
      string text6;
      string text7;
      List<string> list;
      lock (obj)
      {
        num = ((this.phase.Kind == ClickyTakeOverController.PhaseKind.Driving) ? this.phase.Step : 0);
        text = this.driveIdentifier;
        text2 = this.driveAnalyticsOrigin;
        text3 = this.driveAnalyticsThreadId;
        text4 = this.driveTakenBackBy;
        num2 = this.userMousePauseCount;
        this.driveAnalyticsThreadId = null;
        this.driveTakenBackBy = null;
        this.lastTakeOverEndedAt = DateTimeOffset.Now;
        CancellationTokenSource cancellationTokenSource = this.loopCancellation;
        if (cancellationTokenSource != null)
        {
          cancellationTokenSource.Cancel();
        }
        this.loopCancellation = null;
        this.phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Idle, "", 0);
        this.stepCapture = null;
        this.targetProcessName = null;
        agentDriveEnding = this.driveEnding;
        this.driveEnding = AgentDriveEnding.Finished;
        clickyDriveBilling = this.driveBilling;
        text5 = this.driveLeaseId;
        dateTimeOffset = this.driveStartedAt;
        this.driveLeaseId = null;
        this.driveAskingTurnTokens = new List<string>();
        this.driveBilling = ClickyDriveBilling.UserDrive;
        this.agentDriveClickyName = null;
        action = this.agentDriveEnded;
        this.agentDriveEnded = null;
        text6 = this.driveThen;
        this.driveThen = null;
        text7 = this.endSummary;
        list = this.actionLog.ToList<string>();
      }
      ClickyDriveState.IsDrivingNow = false;
      this.StopWatchingUserInput();
      this.overlay.Hide(true);
      this.session.SettleAfterDrive();
      TimeSpan timeSpan = DateTimeOffset.Now - dateTimeOffset;
      bool flag2 = agentDriveEnding == AgentDriveEnding.UserTookOver && text5 != null && timeSpan <= TimeSpan.FromSeconds(10L);
      if (flag2)
      {
        this.RefundAndRefreshAsync(text5);
      }
      else if (!(clickyDriveBilling is ClickyDriveBilling.Setup))
      {
        Func<Task> refreshBilling = this.RefreshBilling;
        if (refreshBilling != null)
        {
          refreshBilling();
        }
      }
      ClickyTakeOverController.TrackDrive("ended", text3, ClickyDriveAnalytics.EndedProperties(text, text2, clickyDriveBilling, agentDriveEnding, timeSpan.TotalSeconds, list.Count, num, text7 != null, action != null && !(clickyDriveBilling is ClickyDriveBilling.Setup), flag2, text4, num2));
      if (action != null)
      {
        try
        {
          action(new AgentDriveOutcome(agentDriveEnding, text7, list));
        }
        catch (Exception ex)
        {
          Log.Warn("drive", "drive outcome handler failed: " + ex.Message);
        }
      }
      if (agentDriveEnding == AgentDriveEnding.Finished && text6 != null)
      {
        this.session.SubmitFactAsync(ClickyDriveLines.ThenFact(text7, text6), false);
      }
    }

    // Token: 0x06001D8F RID: 7567 RVA: 0x00600718 File Offset: 0x00600718
    private async Task RefundAndRefreshAsync(string leaseId)
    {
      try
      {
        Func<string, Task> refundEarlyStop = this.RefundEarlyStop;
        if (refundEarlyStop != null)
        {
          await refundEarlyStop(leaseId).ConfigureAwait(false);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("drive", "early-stop refund failed: " + ex.Message);
        ClickyAnalytics.ReportInternalError("clicky_drive", "early_stop_refund", ex, null, CrashReportLevel.Warning, null, null);
      }
      Func<Task> refreshBilling = this.RefreshBilling;
      if (refreshBilling != null)
      {
        await refreshBilling().ConfigureAwait(false);
      }
    }

    // Token: 0x06001D90 RID: 7568 RVA: 0x00600764 File Offset: 0x00600764
    private void OnUi(Action action)
    {
      if (this.ui.HasThreadAccess)
      {
        action();
        return;
      }
      this.ui.TryEnqueue(delegate
      {
        action();
      });
    }

    // Token: 0x06001D91 RID: 7569 RVA: 0x006007B0 File Offset: 0x006007B0
    private Task<T> InvokeOnUi<[Nullable(2)] T>(Func<T> action)
    {
      if (this.ui.HasThreadAccess)
      {
        return Task.FromResult<T>(action());
      }
      TaskCompletionSource<T> done = new TaskCompletionSource<T>(TaskCreationOptions.RunContinuationsAsynchronously);
      if (!this.ui.TryEnqueue(delegate
      {
        try
        {
          done.SetResult(action());
        }
        catch (Exception ex)
        {
          done.SetException(ex);
        }
      }))
      {
        done.SetCanceled();
      }
      return done.Task;
    }

    // Token: 0x04000E03 RID: 3587
    public static readonly IReadOnlySet<string> ToolNames = new HashSet<string> { "offer_screen_takeover", "start_screen_takeover", "end_screen_takeover" };

    // Token: 0x04000E04 RID: 3588
    private readonly CompanionSession session;

    // Token: 0x04000E05 RID: 3589
    private readonly ScreenCaptureService capture;

    // Token: 0x04000E06 RID: 3590
    private readonly DispatcherQueue ui;

    // Token: 0x04000E07 RID: 3591
    private readonly object gate = new object();

    // Token: 0x04000E08 RID: 3592
    private readonly TakeOverOverlay overlay = new TakeOverOverlay();

    // Token: 0x04000E09 RID: 3593
    private readonly TakeOverConsentCard consentCard = new TakeOverConsentCard();

    // Token: 0x04000E0A RID: 3594
    private ClickyTakeOverController.Phase phase = new ClickyTakeOverController.Phase(ClickyTakeOverController.PhaseKind.Idle, "", 0);

    // Token: 0x04000E0B RID: 3595
    [Nullable(2)]
    private CancellationTokenSource loopCancellation;

    // Token: 0x04000E0C RID: 3596
    [Nullable(2)]
    private ScreenCaptureFrame stepCapture;

    // Token: 0x04000E0D RID: 3597
    private IReadOnlyList<DisplayInfo> stepDisplays = Array.Empty<DisplayInfo>();

    // Token: 0x04000E0E RID: 3598
    [Nullable(2)]
    private string targetProcessName;

    // Token: 0x04000E0F RID: 3599
    private string overlayStatus = "Getting started";

    // Token: 0x04000E10 RID: 3600
    [Nullable(2)]
    private string endSummary;

    // Token: 0x04000E11 RID: 3601
    private DateTimeOffset actuatingUntil = DateTimeOffset.MinValue;

    // Token: 0x04000E12 RID: 3602
    private DateTimeOffset movesIgnoredUntil = DateTimeOffset.MinValue;

    // Token: 0x04000E13 RID: 3603
    private static readonly TimeSpan BeginMoveGrace = TimeSpan.FromSeconds(1L);

    // Token: 0x04000E14 RID: 3604
    [TupleElementNames(new string[] { "Goal", "At" })]
    [Nullable(new byte[] { 0, 1 })]
    private ValueTuple<string, DateTimeOffset>? offerAnsweredBySpeaking;

    // Token: 0x04000E15 RID: 3605
    private static readonly TimeSpan SpokenAnswerWindow = TimeSpan.FromSeconds(30L);

    // Token: 0x04000E16 RID: 3606
    private readonly ClickyDriveUserMouse userMouse = new ClickyDriveUserMouse();

    // Token: 0x04000E17 RID: 3607
    private bool hasClickyActedThisDrive;

    // Token: 0x04000E18 RID: 3608
    private bool isShowingUserMouseHint;

    // Token: 0x04000E19 RID: 3609
    private int userMousePauseCount;

    // Token: 0x04000E1A RID: 3610
    [Nullable(2)]
    private CancellationTokenSource userMouseHintLinger;

    // Token: 0x04000E1B RID: 3611
    [Nullable(2)]
    private ClickyTakeBackWatch userInputWatch;

    // Token: 0x04000E1C RID: 3612
    private DateTimeOffset lastTakeOverEndedAt = DateTimeOffset.MinValue;

    // Token: 0x04000E1D RID: 3613
    private bool currentTurnProgramSpoke;

    // Token: 0x04000E1E RID: 3614
    [Nullable(2)]
    private string currentTurnToken;

    // Token: 0x04000E1F RID: 3615
    private bool spokeForSilentTurn;

    // Token: 0x04000E20 RID: 3616
    private List<string> askingTurnTokens = new List<string>();

    // Token: 0x04000E21 RID: 3617
    [Nullable(2)]
    private ClickyDriveBilling pendingBilling;

    // Token: 0x04000E22 RID: 3618
    private ClickyDriveBilling driveBilling = ClickyDriveBilling.UserDrive;

    // Token: 0x04000E23 RID: 3619
    private string driveIdentifier = Guid.NewGuid().ToString().ToUpperInvariant();

    // Token: 0x04000E24 RID: 3620
    private List<string> driveAskingTurnTokens = new List<string>();

    // Token: 0x04000E25 RID: 3621
    private DateTimeOffset driveStartedAt = DateTimeOffset.MinValue;

    // Token: 0x04000E26 RID: 3622
    [Nullable(2)]
    private string driveLeaseId;

    // Token: 0x04000E27 RID: 3623
    private bool driveBillingSent;

    // Token: 0x04000E28 RID: 3624
    [Nullable(new byte[] { 2, 1 })]
    private Action<AgentDriveOutcome> agentDriveEnded;

    // Token: 0x04000E29 RID: 3625
    private AgentDriveEnding driveEnding;

    // Token: 0x04000E2A RID: 3626
    [Nullable(2)]
    private string agentDriveClickyName;

    // Token: 0x04000E2B RID: 3627
    [Nullable(2)]
    private string offeredThen;

    // Token: 0x04000E2C RID: 3628
    [Nullable(2)]
    private string driveThen;

    // Token: 0x04000E2D RID: 3629
    private readonly List<string> actionLog = new List<string>();

    // Token: 0x04000E2E RID: 3630
    private readonly List<JsonObject> lastCallResults = new List<JsonObject>();

    // Token: 0x04000E2F RID: 3631
    [Nullable(2)]
    private string pendingAnalyticsOrigin;

    // Token: 0x04000E30 RID: 3632
    private string driveAnalyticsOrigin = "offer_card";

    // Token: 0x04000E31 RID: 3633
    [Nullable(2)]
    private string pendingAnalyticsThreadId;

    // Token: 0x04000E32 RID: 3634
    [Nullable(2)]
    private string driveAnalyticsThreadId;

    // Token: 0x04000E33 RID: 3635
    [Nullable(2)]
    private string driveTakenBackBy;

    // Token: 0x020008BC RID: 2236
    [NullableContext(0)]
    public enum PhaseKind
    {
      // Token: 0x040024F2 RID: 9458
      Idle,
      // Token: 0x040024F3 RID: 9459
      Offered,
      // Token: 0x040024F4 RID: 9460
      Driving
    }

    // Token: 0x020008BD RID: 2237
    [Nullable(0)]
    public readonly struct Phase : IEquatable<ClickyTakeOverController.Phase>
    {
      // Token: 0x06003A78 RID: 14968 RVA: 0x0068EB40 File Offset: 0x0068EB40
      public Phase(ClickyTakeOverController.PhaseKind Kind, string Goal = "", int Step = 0)
      {
        this.Kind = Kind;
        this.Goal = Goal;
        this.Step = Step;
      }

      // Token: 0x17000820 RID: 2080
      // (get) Token: 0x06003A79 RID: 14969 RVA: 0x0068EB58 File Offset: 0x0068EB58
      // (set) Token: 0x06003A7A RID: 14970 RVA: 0x0068EB60 File Offset: 0x0068EB60
      public ClickyTakeOverController.PhaseKind Kind { get; set; }

      // Token: 0x17000821 RID: 2081
      // (get) Token: 0x06003A7B RID: 14971 RVA: 0x0068EB6C File Offset: 0x0068EB6C
      // (set) Token: 0x06003A7C RID: 14972 RVA: 0x0068EB74 File Offset: 0x0068EB74
      public string Goal { get; set; }

      // Token: 0x17000822 RID: 2082
      // (get) Token: 0x06003A7D RID: 14973 RVA: 0x0068EB80 File Offset: 0x0068EB80
      // (set) Token: 0x06003A7E RID: 14974 RVA: 0x0068EB88 File Offset: 0x0068EB88
      public int Step { get; set; }

      // Token: 0x06003A7F RID: 14975 RVA: 0x0068EB94 File Offset: 0x0068EB94
      [NullableContext(0)]
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Phase");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003A80 RID: 14976 RVA: 0x0068EBE0 File Offset: 0x0068EBE0
      [NullableContext(0)]
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        builder.Append("Kind = ");
        builder.Append(this.Kind.ToString());
        builder.Append(", Goal = ");
        builder.Append(this.Goal);
        builder.Append(", Step = ");
        builder.Append(this.Step.ToString());
        return true;
      }

      // Token: 0x06003A81 RID: 14977 RVA: 0x0068EC58 File Offset: 0x0068EC58
      [CompilerGenerated]
      public static bool operator !=(ClickyTakeOverController.Phase left, ClickyTakeOverController.Phase right)
      {
        return !(left == right);
      }

      // Token: 0x06003A82 RID: 14978 RVA: 0x0068EC64 File Offset: 0x0068EC64
      [CompilerGenerated]
      public static bool operator ==(ClickyTakeOverController.Phase left, ClickyTakeOverController.Phase right)
      {
        return left.Equals(right);
      }

      // Token: 0x06003A83 RID: 14979 RVA: 0x0068EC70 File Offset: 0x0068EC70
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<ClickyTakeOverController.PhaseKind>.Default.GetHashCode(this.<Kind>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Goal>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Step>k__BackingField);
      }

      // Token: 0x06003A84 RID: 14980 RVA: 0x0068ECB0 File Offset: 0x0068ECB0
      [NullableContext(0)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return obj is ClickyTakeOverController.Phase && this.Equals((ClickyTakeOverController.Phase)obj);
      }

      // Token: 0x06003A85 RID: 14981 RVA: 0x0068ECC8 File Offset: 0x0068ECC8
      [CompilerGenerated]
      public bool Equals(ClickyTakeOverController.Phase other)
      {
        return EqualityComparer<ClickyTakeOverController.PhaseKind>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Goal>k__BackingField, other.<Goal>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Step>k__BackingField, other.<Step>k__BackingField);
      }

      // Token: 0x06003A86 RID: 14982 RVA: 0x0068ED20 File Offset: 0x0068ED20
      [CompilerGenerated]
      public void Deconstruct(out ClickyTakeOverController.PhaseKind Kind, out string Goal, out int Step)
      {
        Kind = this.Kind;
        Goal = this.Goal;
        Step = this.Step;
      }
    }

    // Token: 0x020008BE RID: 2238
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x040024F8 RID: 9464
      [Nullable(new byte[] { 0, 1 })]
      public static Action<string, double> <0>__Play;
    }
  }
}
