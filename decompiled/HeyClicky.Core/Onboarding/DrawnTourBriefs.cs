using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using HeyClicky.Core.Proactive;
using HeyClicky.Core.Worker;

namespace HeyClicky.Core.Onboarding
{
  // Token: 0x020000E7 RID: 231
  [NullableContext(1)]
  [Nullable(0)]
  public static class DrawnTourBriefs
  {
    // Token: 0x06000EF1 RID: 3825 RVA: 0x0038C9EC File Offset: 0x0038C9EC
    public static JsonObject TourBrief(string userName, IReadOnlyList<InterviewLine> interview, string talkKeys, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ProactiveSuggestionCard> plannedCards, out Dictionary<string, ProactiveSuggestionCard> plannedById)
    {
      plannedById = (plannedCards ?? Array.Empty<ProactiveSuggestionCard>()).Take<ProactiveSuggestionCard>(3).Select<ProactiveSuggestionCard, ValueTuple<string, ProactiveSuggestionCard>>(delegate(ProactiveSuggestionCard card, int i)
      {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("c");
        defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
        return new ValueTuple<string, ProactiveSuggestionCard>(defaultInterpolatedStringHandler.ToStringAndClear(), card);
      }).ToDictionary<ValueTuple<string, ProactiveSuggestionCard>, string, ProactiveSuggestionCard>(([TupleElementNames(new string[] { "Key", "Card" })] ValueTuple<string, ProactiveSuggestionCard> p) => p.Item1, ([TupleElementNames(new string[] { "Key", "Card" })] ValueTuple<string, ProactiveSuggestionCard> p) => p.Item2);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["kind"] = "tour_finale";
      jsonObject["notch_showcase"] = true;
      jsonObject["user_name"] = userName;
      jsonObject["talk_keys"] = talkKeys;
      jsonObject["interview"] = new JsonArray(interview.Select<InterviewLine, JsonNode>(delegate(InterviewLine l)
      {
        JsonObject jsonObject2 = new JsonObject(null);
        jsonObject2["role"] = ((l.Role == "user") ? "user" : "assistant");
        jsonObject2["text"] = l.Text;
        return jsonObject2;
      }).ToArray<JsonNode>());
      jsonObject["planned_cards"] = new JsonArray(plannedById.OrderBy<KeyValuePair<string, ProactiveSuggestionCard>, string>((KeyValuePair<string, ProactiveSuggestionCard> p) => p.Key, StringComparer.Ordinal).Select<KeyValuePair<string, ProactiveSuggestionCard>, JsonNode>(delegate(KeyValuePair<string, ProactiveSuggestionCard> p)
      {
        JsonObject jsonObject3 = new JsonObject(null);
        jsonObject3["id"] = p.Key;
        jsonObject3["title"] = p.Value.Title;
        jsonObject3["description"] = p.Value.Description;
        JsonObject jsonObject4 = jsonObject3;
        ProactiveRoutine routine = p.Value.Routine;
        if (routine != null)
        {
          JsonNode jsonNode = jsonObject4;
          string text = "routine";
          JsonObject jsonObject5 = new JsonObject(null);
          jsonObject5["title"] = routine.Title;
          jsonObject5["every_minutes"] = routine.ClampedIntervalMinutes;
          jsonNode[text] = jsonObject5;
        }
        return jsonObject4;
      }).ToArray<JsonNode>());
      return jsonObject;
    }

    // Token: 0x06000EF2 RID: 3826 RVA: 0x0038CB68 File Offset: 0x0038CB68
    public static string FittedTranscript(IReadOnlyList<InterviewLine> interview, string userName, int budget)
    {
      DrawnTourBriefs.<>c__DisplayClass1_0 CS$<>8__locals1 = new DrawnTourBriefs.<>c__DisplayClass1_0();
      CS$<>8__locals1.userName = userName;
      CS$<>8__locals1.lines = interview.Select<InterviewLine, ValueTuple<string, string>>((InterviewLine l) => new ValueTuple<string, string>((l.Role == "user") ? CS$<>8__locals1.userName : "Clicky", l.Text)).ToList<ValueTuple<string, string>>();
      if (CS$<>8__locals1.<FittedTranscript>g__Rendered|1().Length <= budget)
      {
        return CS$<>8__locals1.<FittedTranscript>g__Rendered|1();
      }
      CS$<>8__locals1.lines = CS$<>8__locals1.lines.Select<ValueTuple<string, string>, ValueTuple<string, string>>(delegate([TupleElementNames(new string[] { "Speaker", "Text" })] ValueTuple<string, string> l)
      {
        if (!(l.Item1 == "Clicky") || l.Item2.Length <= 80)
        {
          return l;
        }
        return new ValueTuple<string, string>(l.Item1, l.Item2.Substring(0, 80) + "…");
      }).ToList<ValueTuple<string, string>>();
      while (CS$<>8__locals1.<FittedTranscript>g__Rendered|1().Length > budget && CS$<>8__locals1.lines.Count > 0)
      {
        IEnumerable<int> enumerable = Enumerable.Range(0, CS$<>8__locals1.lines.Count);
        Func<int, int> func;
        if ((func = CS$<>8__locals1.<>9__4) == null)
        {
          func = (CS$<>8__locals1.<>9__4 = (int i) => CS$<>8__locals1.lines[i].Item2.Length);
        }
        int num = enumerable.MaxBy<int, int>(func);
        if (CS$<>8__locals1.lines[num].Item2.Length <= 120)
        {
          break;
        }
        CS$<>8__locals1.lines[num] = new ValueTuple<string, string>(CS$<>8__locals1.lines[num].Item1, CS$<>8__locals1.lines[num].Item2.Substring(0, CS$<>8__locals1.lines[num].Item2.Length - 40) + "…");
      }
      return CS$<>8__locals1.<FittedTranscript>g__Rendered|1();
    }

    // Token: 0x06000EF3 RID: 3827 RVA: 0x0038CCC0 File Offset: 0x0038CCC0
    public static JsonObject MorningBrief([Nullable(2)] string firstName, string researchedAgo, string curatedEvidence, [Nullable(2)] string headline, IReadOnlyList<string> todaysEvents, IReadOnlyList<ProactiveSuggestionCard> plateCards)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["kind"] = "morning";
      jsonObject["user_name"] = firstName ?? "them";
      jsonObject["researched_ago"] = researchedAgo;
      jsonObject["calendar"] = new JsonArray(todaysEvents.Select<string, JsonNode>((string e) => e).ToArray<JsonNode>());
      jsonObject["cards"] = new JsonArray(plateCards.Select<ProactiveSuggestionCard, JsonNode>(delegate(ProactiveSuggestionCard card, int i)
      {
        JsonObject jsonObject3 = new JsonObject(null);
        string text = "id";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
        defaultInterpolatedStringHandler.AppendLiteral("c");
        defaultInterpolatedStringHandler.AppendFormatted<int>(i + 1);
        jsonObject3[text] = defaultInterpolatedStringHandler.ToStringAndClear();
        jsonObject3["title"] = card.Title;
        jsonObject3["description"] = card.Description;
        jsonObject3["brief"] = card.AgentPrompt;
        return jsonObject3;
      }).ToArray<JsonNode>());
      jsonObject["evidence"] = curatedEvidence;
      JsonObject jsonObject2 = jsonObject;
      if (headline != null)
      {
        jsonObject2["headline"] = headline;
      }
      return jsonObject2;
    }

    // Token: 0x06000EF4 RID: 3828 RVA: 0x0038CDB8 File Offset: 0x0038CDB8
    public static string RelativeTime(DateTimeOffset date, DateTimeOffset now)
    {
      double totalSeconds = (now - date).TotalSeconds;
      bool flag = totalSeconds >= 0.0;
      double num = Math.Abs(totalSeconds);
      string text = ((num < 1.0) ? "" : ((num < 60.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num, "second") : ((num < 3600.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 60.0, "minute") : ((num < 86400.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 3600.0, "hour") : ((num < 604800.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 86400.0, "day") : ((num < 2592000.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 604800.0, "week") : ((num < 31536000.0) ? DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 2592000.0, "month") : DrawnTourBriefs.<RelativeTime>g__Unit|3_0(num / 31536000.0, "year"))))))));
      if (text.Length == 0)
      {
        return "now";
      }
      if (!flag)
      {
        return "in " + text;
      }
      return text + " ago";
    }

    // Token: 0x06000EF5 RID: 3829 RVA: 0x0038CF14 File Offset: 0x0038CF14
    [return: TupleElementNames(new string[] { "Text", "Plan" })]
    [return: Nullable(new byte[] { 1, 0, 1, 1 })]
    public static IReadOnlyList<ValueTuple<string, string>> ParseNextQuestions(string payload)
    {
      JsonArray jsonArray;
      try
      {
        jsonArray = JsonNode.Parse(payload, null, default(JsonDocumentOptions)) as JsonArray;
      }
      catch (JsonException)
      {
        return Array.Empty<ValueTuple<string, string>>();
      }
      if (jsonArray == null)
      {
        return Array.Empty<ValueTuple<string, string>>();
      }
      List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
      foreach (JsonObject jsonObject in jsonArray.OfType<JsonObject>())
      {
        DrawnTourBriefs.<>c__DisplayClass4_0 CS$<>8__locals1;
        CS$<>8__locals1.item = jsonObject;
        string text = DrawnTourBriefs.<ParseNextQuestions>g__Str|4_0("text", ref CS$<>8__locals1);
        string text2 = ((text != null) ? text.Trim() : null);
        if (text2 != null && text2.Length > 0)
        {
          string text3;
          if ((text3 = DrawnTourBriefs.<ParseNextQuestions>g__Str|4_0("plan", ref CS$<>8__locals1)) == null)
          {
            text3 = DrawnTourBriefs.<ParseNextQuestions>g__Str|4_0("ask", ref CS$<>8__locals1) ?? "";
          }
          string text4 = text3.Trim();
          list.Add(new ValueTuple<string, string>((text2.Length > 48) ? text2.Substring(0, 48) : text2, (text4 == text2) ? "" : ((text4.Length > 600) ? text4.Substring(0, 600) : text4)));
        }
      }
      return list.Take<ValueTuple<string, string>>(3).ToList<ValueTuple<string, string>>();
    }

    // Token: 0x06000EF6 RID: 3830 RVA: 0x0038D074 File Offset: 0x0038D074
    public static string WholeWordTitle(string title, int maximumLength = 80)
    {
      string text = title.Trim();
      if (text.Length <= maximumLength)
      {
        return text;
      }
      string text2 = text.Substring(0, maximumLength);
      int num = text2.LastIndexOf(' ');
      if (num < 0)
      {
        return text2;
      }
      return text2.Substring(0, num).Trim(new char[] { ' ', ',', '.', ';', ':', '-' });
    }

    // Token: 0x06000EF7 RID: 3831 RVA: 0x0038D0CC File Offset: 0x0038D0CC
    public static string PaperForTitle(string title, string summary = "")
    {
      DrawnTourBriefs.<>c__DisplayClass6_0 CS$<>8__locals1;
      CS$<>8__locals1.words = (title + " " + summary).ToLowerInvariant();
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "flight", "trip", "travel", "hotel", "fare", "lisbon", "tokyo", "airbnb" }, ref CS$<>8__locals1))
      {
        return "peach";
      }
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "inbox", "email", "mail", "invoice", "revenue", "price", "budget", "stock", "money", "$" }, ref CS$<>8__locals1))
      {
        return "mint";
      }
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "meeting", "board", "calendar", "sync", "prep", "agenda" }, ref CS$<>8__locals1))
      {
        return "periwinkle";
      }
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "draw", "sketch", "design", "photo" }, ref CS$<>8__locals1))
      {
        return "blush";
      }
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "buy", "shop", "list", "desk", "groceries" }, ref CS$<>8__locals1))
      {
        return "kraft";
      }
      if (DrawnTourBriefs.<PaperForTitle>g__Has|6_0(new string[] { "news", "digest", "brief", "summary", "trend" }, ref CS$<>8__locals1))
      {
        return "butter";
      }
      return "sky";
    }

    // Token: 0x06000EF8 RID: 3832 RVA: 0x0038D2A8 File Offset: 0x0038D2A8
    [CompilerGenerated]
    internal static string <RelativeTime>g__Unit|3_0(double value, string unit)
    {
      int num = (int)Math.Floor(value);
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
      defaultInterpolatedStringHandler.AppendFormatted<int>(num);
      defaultInterpolatedStringHandler.AppendLiteral(" ");
      defaultInterpolatedStringHandler.AppendFormatted(unit);
      defaultInterpolatedStringHandler.AppendFormatted((num == 1) ? "" : "s");
      return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    // Token: 0x06000EF9 RID: 3833 RVA: 0x0038D300 File Offset: 0x0038D300
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <ParseNextQuestions>g__Str|4_0(string key, ref DrawnTourBriefs.<>c__DisplayClass4_0 A_1)
    {
      JsonValue jsonValue = A_1.item[key] as JsonValue;
      string text;
      if (jsonValue == null || !jsonValue.TryGetValue<string>(out text))
      {
        return null;
      }
      return text;
    }

    // Token: 0x06000EFA RID: 3834 RVA: 0x0038D330 File Offset: 0x0038D330
    [CompilerGenerated]
    internal static bool <PaperForTitle>g__Has|6_0(string[] keys, ref DrawnTourBriefs.<>c__DisplayClass6_0 A_1)
    {
      return keys.Any<string>(new Func<string, bool>(A_1.words.Contains));
    }
  }
}
