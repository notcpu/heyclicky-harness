using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using HeyClicky.App.Shell;
using HeyClicky.Core.Onboarding;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Telemetry;
using HeyClicky.Core.Worker;
using HeyClicky.Platform.Input;
using Microsoft.UI.Dispatching;

namespace HeyClicky.App.OneBrain
{
  // Token: 0x02000115 RID: 277
  [NullableContext(1)]
  [Nullable(0)]
  public static class DrawnOnboardingTour
  {
    // Token: 0x06000EA4 RID: 3748 RVA: 0x00575748 File Offset: 0x00575748
    [NullableContext(2)]
    public static JsonObject ConsumePendingBrief()
    {
      object obj = DrawnOnboardingTour.gate;
      JsonObject jsonObject2;
      lock (obj)
      {
        JsonObject jsonObject = DrawnOnboardingTour.pendingBrief;
        DrawnOnboardingTour.pendingBrief = null;
        jsonObject2 = jsonObject;
      }
      return jsonObject2;
    }

    // Token: 0x06000EA5 RID: 3749 RVA: 0x00575790 File Offset: 0x00575790
    public static void Arm(JsonObject brief)
    {
      object obj = DrawnOnboardingTour.gate;
      lock (obj)
      {
        DrawnOnboardingTour.pendingBrief = brief;
      }
    }

    // Token: 0x170002A9 RID: 681
    // (get) Token: 0x06000EA6 RID: 3750 RVA: 0x005757D0 File Offset: 0x005757D0
    // (set) Token: 0x06000EA7 RID: 3751 RVA: 0x005757DC File Offset: 0x005757DC
    public static bool IsRunning
    {
      get
      {
        return DrawnOnboardingTour.isRunning;
      }
      set
      {
        if (DrawnOnboardingTour.isRunning == value)
        {
          return;
        }
        DrawnOnboardingTour.isRunning = value;
        Action notchHoverSuppressionChanged = DrawnOnboardingTour.NotchHoverSuppressionChanged;
        if (notchHoverSuppressionChanged == null)
        {
          return;
        }
        notchHoverSuppressionChanged();
      }
    }

    // Token: 0x14000031 RID: 49
    // (add) Token: 0x06000EA8 RID: 3752 RVA: 0x00575800 File Offset: 0x00575800
    // (remove) Token: 0x06000EA9 RID: 3753 RVA: 0x00575834 File Offset: 0x00575834
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public static event Action NotchHoverSuppressionChanged;

    // Token: 0x06000EAA RID: 3754 RVA: 0x00575868 File Offset: 0x00575868
    public static void TrackFinaleStarted(string source, int plannedCardCount, int interviewAnswerCount, bool prefetched = false)
    {
      DrawnOnboardingTour.analyticsStartedAt = new long?(Environment.TickCount64);
      DrawnOnboardingTour.analyticsSource = source;
      string text = "started";
      bool flag = false;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["source"] = source;
      jsonObject["has_interview"] = interviewAnswerCount > 0;
      jsonObject["interview_answer_count"] = interviewAnswerCount;
      jsonObject["planned_card_count"] = plannedCardCount;
      jsonObject["prefetched"] = prefetched;
      ClickyTourJobCards.TrackFlowAction(text, flag, jsonObject);
    }

    // Token: 0x06000EAB RID: 3755 RVA: 0x005758FC File Offset: 0x005758FC
    public static void TrackFinaleEnded(string outcome, int cardsShownCount, int nextQuestionCount)
    {
      long? num = DrawnOnboardingTour.analyticsStartedAt;
      if (num != null)
      {
        long valueOrDefault = num.GetValueOrDefault();
        DrawnOnboardingTour.analyticsStartedAt = null;
        string text = "ended";
        bool flag = false;
        JsonObject jsonObject = new JsonObject(null);
        jsonObject["source"] = DrawnOnboardingTour.analyticsSource;
        jsonObject["outcome"] = outcome;
        jsonObject["duration_seconds"] = ClickyAnalytics.RoundedSeconds((double)(Environment.TickCount64 - valueOrDefault) / 1000.0);
        jsonObject["cards_shown_count"] = cardsShownCount;
        jsonObject["next_question_count"] = nextQuestionCount;
        ClickyTourJobCards.TrackFlowAction(text, flag, jsonObject);
        return;
      }
    }

    // Token: 0x170002AA RID: 682
    // (get) Token: 0x06000EAC RID: 3756 RVA: 0x005759B8 File Offset: 0x005759B8
    // (set) Token: 0x06000EAD RID: 3757 RVA: 0x005759C0 File Offset: 0x005759C0
    public static bool HoldsNotch { get; private set; }

    // Token: 0x170002AB RID: 683
    // (get) Token: 0x06000EAE RID: 3758 RVA: 0x005759C8 File Offset: 0x005759C8
    public static bool SuppressesNotchHover
    {
      get
      {
        return DrawnOnboardingTour.IsRunning || DrawnOnboardingTour.HoldsNotch;
      }
    }

    // Token: 0x06000EAF RID: 3759 RVA: 0x005759D8 File Offset: 0x005759D8
    public static void HoldNotchUntilPaywallAnswered(AppHost host)
    {
      DrawnOnboardingTour.ReleaseNotch();
      DrawnOnboardingTour.HoldsNotch = true;
      Action notchHoverSuppressionChanged = DrawnOnboardingTour.NotchHoverSuppressionChanged;
      if (notchHoverSuppressionChanged != null)
      {
        notchHoverSuppressionChanged();
      }
      int generation = ++DrawnOnboardingTour.holdGeneration;
      DispatcherQueueTimer timer = host.DispatcherQueue.CreateTimer().Rooted();
      timer.Interval = TimeSpan.FromSeconds(600L);
      timer.IsRepeating = false;
      timer.Tick += delegate(DispatcherQueueTimer _, object _)
      {
        timer.Stop();
        if (DrawnOnboardingTour.holdGeneration == generation)
        {
          DrawnOnboardingTour.ReleaseNotch();
        }
      };
      timer.Start();
      DispatcherQueue dispatcher = host.DispatcherQueue;
      DrawnOnboardingTour.notchClickWatch = PointerInput.WatchAllPresses(delegate(PointD press)
      {
        dispatcher.TryEnqueue(delegate
        {
          if (DrawnOnboardingTour.IsOnHeldNotch(host, press))
          {
            DrawnOnboardingTour.NudgeNotchClicked(host);
          }
        });
      });
    }

    // Token: 0x06000EB0 RID: 3760 RVA: 0x00575AA8 File Offset: 0x00575AA8
    public static void ReleaseNotch()
    {
      IDisposable disposable = DrawnOnboardingTour.notchClickWatch;
      if (disposable != null)
      {
        disposable.Dispose();
      }
      DrawnOnboardingTour.notchClickWatch = null;
      if (!DrawnOnboardingTour.HoldsNotch)
      {
        return;
      }
      DrawnOnboardingTour.HoldsNotch = false;
      DrawnOnboardingTour.holdGeneration++;
      Action notchHoverSuppressionChanged = DrawnOnboardingTour.NotchHoverSuppressionChanged;
      if (notchHoverSuppressionChanged == null)
      {
        return;
      }
      notchHoverSuppressionChanged();
    }

    // Token: 0x06000EB1 RID: 3761 RVA: 0x00575AF4 File Offset: 0x00575AF4
    private static bool IsOnHeldNotch(AppHost host, PointD press)
    {
      ValueTuple<int, int, int, int> valueTuple = host.NotchUi.SurfaceRectPx();
      if (valueTuple.Item3 <= 0)
      {
        return false;
      }
      Displays.Monitor monitor = Displays.At(valueTuple.Item1 + valueTuple.Item3 / 2, valueTuple.Item2 + Math.Max(1, valueTuple.Item4 / 2));
      double scale = monitor.Scale;
      double num = (double)monitor.Bounds.Left + (double)monitor.Bounds.Width / 2.0;
      if (DrawnOnboardingNotchShowcase.Shared.IsShowing)
      {
        return press.X >= num - 560.0 * scale && press.X <= num + 560.0 * scale && press.Y >= (double)monitor.Bounds.Top && press.Y <= (double)monitor.Bounds.Top + (double)monitor.Bounds.Height * 0.6;
      }
      return press.X >= (double)valueTuple.Item1 - 40.0 * scale && press.X <= (double)(valueTuple.Item1 + valueTuple.Item3) + 40.0 * scale && press.Y >= (double)monitor.Bounds.Top && press.Y <= (double)(valueTuple.Item2 + valueTuple.Item4) + 30.0 * scale;
    }

    // Token: 0x06000EB2 RID: 3762 RVA: 0x00575C78 File Offset: 0x00575C78
    public static void NudgeNotchClicked(AppHost host)
    {
      if (!DrawnOnboardingTour.HoldsNotch || Environment.TickCount64 - DrawnOnboardingTour.lastNotchClickNudgeAt <= 4000L)
      {
        return;
      }
      DrawnOnboardingTour.lastNotchClickNudgeAt = Environment.TickCount64;
      string text = "notch_clicked_during_tour";
      bool flag = false;
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["while_notch_shown"] = DrawnOnboardingNotchShowcase.Shared.IsShowing;
      ClickyTourJobCards.TrackFlowAction(text, flag, jsonObject);
      host.SayAtCursorAsync("try this after the demo!", true, 2.0, default(CancellationToken));
    }

    // Token: 0x06000EB3 RID: 3763 RVA: 0x00575CFC File Offset: 0x00575CFC
    public static void NudgeTourCardClicked(AppHost host)
    {
      if (Environment.TickCount64 - DrawnOnboardingTour.lastTourCardClickNudgeAt <= 4000L)
      {
        return;
      }
      DrawnOnboardingTour.lastTourCardClickNudgeAt = Environment.TickCount64;
      host.SayAtCursorAsync("you can start these from your notch once we're done here!", true, 2.0, default(CancellationToken));
    }

    // Token: 0x06000EB4 RID: 3764 RVA: 0x00575D48 File Offset: 0x00575D48
    [return: TupleElementNames(new string[] { "Name", "Lines", "Index" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    public static ValueTuple<string, IReadOnlyList<InterviewLine>, int?>? RandomRealInterview()
    {
      ValueTuple<string, IReadOnlyList<InterviewLine>, int?>? valueTuple;
      try
      {
        string text = AppPaths.Combine(new string[] { "DrawnTourTestInterviews.json" });
        if (File.Exists(text))
        {
          JsonNode jsonNode = JsonNode.Parse(File.ReadAllText(text), null, default(JsonDocumentOptions));
          JsonArray jsonArray = ((jsonNode != null) ? jsonNode["interviews"] : null) as JsonArray;
          if (jsonArray != null && jsonArray.Count != 0)
          {
            int num = Random.Shared.Next(jsonArray.Count);
            JsonNode jsonNode2 = jsonArray[num];
            JsonArray jsonArray2 = ((jsonNode2 != null) ? jsonNode2["lines"] : null) as JsonArray;
            List<InterviewLine> list;
            if (jsonArray2 == null)
            {
              list = null;
            }
            else
            {
              list = jsonArray2.OfType<JsonObject>().Select<JsonObject, InterviewLine>(delegate(JsonObject l)
              {
                JsonNode jsonNode3 = l["role"];
                string text2 = ((((jsonNode3 != null) ? jsonNode3.GetValue<string>() : null) == "user") ? "user" : "assistant");
                JsonNode jsonNode4 = l["text"];
                return new InterviewLine(text2, ((jsonNode4 != null) ? jsonNode4.GetValue<string>() : null) ?? "");
              }).ToList<InterviewLine>();
            }
            List<InterviewLine> list2 = list ?? new List<InterviewLine>();
            return new ValueTuple<string, IReadOnlyList<InterviewLine>, int?>?(new ValueTuple<string, IReadOnlyList<InterviewLine>, int?>(DrawnOnboardingTour.PlaceholderNames[num % DrawnOnboardingTour.PlaceholderNames.Length], list2, new int?(num)));
          }
        }
        valueTuple = null;
        valueTuple = valueTuple;
      }
      catch (Exception ex) when (ex is JsonException || ex is IOException || ex is InvalidOperationException)
      {
        Log.Info("drawn tour", "test interviews unreadable: " + ex.Message);
        valueTuple = null;
      }
      return valueTuple;
    }

    // Token: 0x06000EB5 RID: 3765 RVA: 0x00575EC8 File Offset: 0x00575EC8
    [return: TupleElementNames(new string[] { "Name", "Lines", "Index" })]
    [return: Nullable(new byte[] { 0, 1, 1, 1 })]
    public static ValueTuple<string, IReadOnlyList<InterviewLine>, int?> TestInterview()
    {
      ValueTuple<string, IReadOnlyList<InterviewLine>, int?>? valueTuple = DrawnOnboardingTour.RandomRealInterview();
      if (valueTuple == null)
      {
        return new ValueTuple<string, IReadOnlyList<InterviewLine>, int?>("Sam", DrawnOnboardingTour.SampleInterview, null);
      }
      return valueTuple.GetValueOrDefault();
    }

    // Token: 0x170002AC RID: 684
    // (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00575F04 File Offset: 0x00575F04
    public static bool IsFinaleEnabled
    {
      get
      {
        return true;
      }
    }

    // Token: 0x170002AD RID: 685
    // (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00575F08 File Offset: 0x00575F08
    // (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00575F10 File Offset: 0x00575F10
    public static Dictionary<string, ProactiveSuggestionCard> PlannedCardsById { get; set; } = new Dictionary<string, ProactiveSuggestionCard>();

    // Token: 0x06000EB9 RID: 3769 RVA: 0x00575F18 File Offset: 0x00575F18
    public static JsonObject TourBrief(string userName, IReadOnlyList<InterviewLine> interview, string talkKeys, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ProactiveSuggestionCard> plannedCards = null)
    {
      Dictionary<string, ProactiveSuggestionCard> dictionary;
      JsonObject jsonObject = DrawnTourBriefs.TourBrief(userName, interview, talkKeys, plannedCards, out dictionary);
      DrawnOnboardingTour.PlannedCardsById = dictionary;
      return jsonObject;
    }

    // Token: 0x06000EBA RID: 3770 RVA: 0x00575F38 File Offset: 0x00575F38
    public static string FittedTranscript(IReadOnlyList<InterviewLine> interview, string userName, int budget)
    {
      return DrawnTourBriefs.FittedTranscript(interview, userName, budget);
    }

    // Token: 0x040005EA RID: 1514
    private const string Area = "drawn tour";

    // Token: 0x040005EB RID: 1515
    [Nullable(2)]
    private static JsonObject pendingBrief;

    // Token: 0x040005EC RID: 1516
    private static readonly object gate = new object();

    // Token: 0x040005ED RID: 1517
    private static volatile bool isRunning;

    // Token: 0x040005EF RID: 1519
    private static long? analyticsStartedAt;

    // Token: 0x040005F0 RID: 1520
    private static string analyticsSource = "onboarding";

    // Token: 0x040005F2 RID: 1522
    private static long lastNotchClickNudgeAt;

    // Token: 0x040005F3 RID: 1523
    private static int holdGeneration;

    // Token: 0x040005F4 RID: 1524
    [Nullable(2)]
    private static IDisposable notchClickWatch;

    // Token: 0x040005F5 RID: 1525
    private static long lastTourCardClickNudgeAt;

    // Token: 0x040005F6 RID: 1526
    public static readonly IReadOnlyList<InterviewLine> SampleInterview = new <>z__ReadOnlyArray<InterviewLine>(new InterviewLine[]
    {
      new InterviewLine("assistant", "Hi, I'm Clicky. I'm not real, of course, but I'm a buddy that exists to help you work on your ideas. So what are you working on these days?"),
      new InterviewLine("user", "I'm building my first app ever. It's a running app, kind of a coach for people who just started running. Like couch to 5k, but it actually adapts to how your runs went."),
      new InterviewLine("assistant", "Oh I love that. What would winning look like for you?"),
      new InterviewLine("user", "Honestly? Making my first dollar. One stranger paying me one dollar for something I built. I've never made money from my own thing."),
      new InterviewLine("assistant", "Where's the app at right now?"),
      new InterviewLine("user", "I have a half working SwiftUI prototype. It tracks a run and plays a voice cue every kilometer. No name, no website, and nobody's seen it. I keep tweaking the code instead of showing it to anyone."),
      new InterviewLine("assistant", "Last one. Have you used an AI agent before, like Claude Code or Codex, or mostly ChatGPT?"),
      new InterviewLine("user", "Mostly ChatGPT. I tried Cursor for the app a bit. I've never had something just go do work for me in the background.")
    });

    // Token: 0x040005F7 RID: 1527
    public const string RealInterviewsFileName = "DrawnTourTestInterviews.json";

    // Token: 0x040005F8 RID: 1528
    private static readonly string[] PlaceholderNames = new string[] { "Alex", "Maya", "Sam", "Priya", "Leo", "Nina", "Omar", "Jules", "Kai", "Rosa" };
  }
}
