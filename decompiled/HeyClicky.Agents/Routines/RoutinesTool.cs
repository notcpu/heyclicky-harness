using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using HeyClicky.Agents.Jobs;

namespace HeyClicky.Agents.Routines
{
  // Token: 0x0200002D RID: 45
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class RoutinesTool
  {
    // Token: 0x060004A8 RID: 1192 RVA: 0x000ED530 File Offset: 0x000ED530
    public RoutinesTool(RoutineScheduler routines, ClickyJobStore jobs, [Nullable(new byte[] { 1, 1, 2 })] Func<string, string> clickySlugForThread, [Nullable(new byte[] { 1, 1, 2 })] Func<string, string> clickyName, Func<string> projectsRoot)
    {
    }

    // Token: 0x060004A9 RID: 1193 RVA: 0x000ED560 File Offset: 0x000ED560
    public string ContextNote()
    {
      List<ClickyRoutine> list = this.<routines>P.Routines.OrderBy<ClickyRoutine, DateTimeOffset>((ClickyRoutine r) => r.CreatedAt).ToList<ClickyRoutine>();
      if (list.Count == 0)
      {
        return "[Your routines] none yet.";
      }
      IEnumerable<string> enumerable = list.Select<ClickyRoutine, string>(delegate(ClickyRoutine routine)
      {
        string jobId = routine.JobId;
        string text;
        if (jobId != null)
        {
          ClickyJob clickyJob = this.<jobs>P.Job(jobId);
          text = "job \"" + (((clickyJob != null) ? clickyJob.Title : null) ?? routine.Title) + "\"" + ((clickyJob != null) ? (" (job_id: " + clickyJob.Id + ")") : "");
        }
        else
        {
          text = "Clicky \"" + (this.<clickyName>P(routine.ClickySlug) ?? routine.ClickySlug) + "\"";
        }
        string text2 = (routine.IsPaused ? "PAUSED" : ("next run " + RoutinesTool.RunTime(routine.NextRunAt)));
        string text3 = string.Join<string>(' ', from l in routine.TaskPrompt.Split(RuntimeHelpers.CreateSpan<char>(fieldof(<PrivateImplementationDetails>.FBB68C637180E81FFB4AF91596B53064C159DDF8F56E330BDD31C0DE6472348D2).FieldHandle))
          select l.Trim() into l
          where l.Length > 0
          select l);
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 7);
        defaultInterpolatedStringHandler.AppendLiteral("- routine_id ");
        defaultInterpolatedStringHandler.AppendFormatted(routine.Id);
        defaultInterpolatedStringHandler.AppendLiteral(": \"");
        defaultInterpolatedStringHandler.AppendFormatted(routine.Title);
        defaultInterpolatedStringHandler.AppendLiteral("\" — ");
        defaultInterpolatedStringHandler.AppendFormatted(routine.ScheduleLabel.ToLowerInvariant());
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(text2);
        defaultInterpolatedStringHandler.AppendLiteral(", ran ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(routine.RunCount);
        defaultInterpolatedStringHandler.AppendLiteral("×, ");
        defaultInterpolatedStringHandler.AppendLiteral("runs as ");
        defaultInterpolatedStringHandler.AppendFormatted(text);
        defaultInterpolatedStringHandler.AppendLiteral(". Each run: ");
        defaultInterpolatedStringHandler.AppendFormatted(RoutinesTool.Prefix(text3, 400));
        return defaultInterpolatedStringHandler.ToStringAndClear();
      });
      return "[Your routines] Work that repeats on a schedule (the routines tool changes them; this list is all of them):\n" + string.Join("\n", enumerable);
    }

    // Token: 0x060004AA RID: 1194 RVA: 0x000ED5D8 File Offset: 0x000ED5D8
    public async Task<JsonObject> RunAsync(string argumentsJson)
    {
      RoutinesTool.<>c__DisplayClass7_0 CS$<>8__locals1;
      try
      {
        JsonObject jsonObject;
        if ((jsonObject = JsonNode.Parse(argumentsJson, null, default(JsonDocumentOptions)) as JsonObject) == null)
        {
          jsonObject = new JsonObject(null);
        }
        CS$<>8__locals1.arguments = jsonObject;
      }
      catch (JsonException)
      {
        CS$<>8__locals1.arguments = new JsonObject(null);
      }
      JsonValue jsonValue = CS$<>8__locals1.arguments["every_minutes"] as JsonValue;
      int num2;
      double num3;
      int? num = ((jsonValue != null) ? (jsonValue.TryGetValue<int>(out num2) ? new int?(num2) : (jsonValue.TryGetValue<double>(out num3) ? new int?((int)num3) : null)) : null);
      string text = RoutinesTool.<RunAsync>g__Text|7_0("at", ref CS$<>8__locals1);
      int? num4 = ((text != null) ? RoutinesTool.MinuteOfDay(text) : null);
      JsonObject jsonObject2;
      if (RoutinesTool.<RunAsync>g__Text|7_0("at", ref CS$<>8__locals1) != null && num4 == null)
      {
        jsonObject2 = RoutinesTool.Error("at must be a time like \"09:00\".");
      }
      else
      {
        JsonArray jsonArray = CS$<>8__locals1.arguments["days"] as JsonArray;
        List<string> list;
        if (jsonArray != null)
        {
          if (jsonArray.All<JsonNode>(delegate(JsonNode e)
          {
            JsonValue jsonValue2 = e as JsonValue;
            string text7;
            return jsonValue2 != null && jsonValue2.TryGetValue<string>(out text7);
          }))
          {
            list = jsonArray.Select<JsonNode, string>((JsonNode e) => e.GetValue<string>()).ToList<string>();
            goto IL_0190;
          }
        }
        list = null;
        IL_0190:
        List<string> list2 = list;
        List<string> list3;
        if ((list3 = list2) == null)
        {
          string text2 = RoutinesTool.<RunAsync>g__Text|7_0("weekday", ref CS$<>8__locals1);
          if (text2 == null)
          {
            list3 = null;
          }
          else
          {
            (list3 = new List<string>(1)).Add(text2);
          }
        }
        List<string> list4 = list3;
        List<int> list5;
        if (list4 == null)
        {
          list5 = null;
        }
        else
        {
          IEnumerable<string> enumerable = list4;
          Func<string, int?> func;
          if ((func = RoutinesTool.<>O.<0>__Weekday) == null)
          {
            func = (RoutinesTool.<>O.<0>__Weekday = new Func<string, int?>(RoutinesTool.Weekday));
          }
          list5 = enumerable.Select<string, int?>(func).OfType<int>().ToList<int>();
        }
        List<int> list6 = list5;
        List<int> list7 = ((list6 != null && list6.Count > 0) ? list6 : null);
        bool flag = list2 != null && list2.Count == 0;
        if (list4 != null && list4.Count > 0 && list7 == null)
        {
          jsonObject2 = RoutinesTool.Error("days must be weekday names like \"tuesday\".");
        }
        else
        {
          string text3 = RoutinesTool.<RunAsync>g__Text|7_0("action", ref CS$<>8__locals1) ?? "";
          if (text3 == "create")
          {
            jsonObject2 = await this.CreateAsync(RoutinesTool.<RunAsync>g__Text|7_0("title", ref CS$<>8__locals1), RoutinesTool.<RunAsync>g__Text|7_0("task", ref CS$<>8__locals1), num, num4, list7, RoutinesTool.<RunAsync>g__Text|7_0("codexThreadId", ref CS$<>8__locals1), RoutinesTool.<RunAsync>g__Text|7_0("first_run", ref CS$<>8__locals1) != "scheduled").ConfigureAwait(false);
          }
          else
          {
            ClickyRoutine clickyRoutine = this.RoutineNamed(RoutinesTool.<RunAsync>g__Text|7_0("routine_id", ref CS$<>8__locals1));
            if (clickyRoutine == null)
            {
              jsonObject2 = RoutinesTool.Error("No routine with that routine_id: use one from the [Your routines] note.");
            }
            else if (!(text3 == "update"))
            {
              if (!(text3 == "pause"))
              {
                if (!(text3 == "resume"))
                {
                  if (!(text3 == "delete"))
                  {
                    if (!(text3 == "run_now"))
                    {
                      jsonObject2 = RoutinesTool.Error("Unknown action " + text3 + ".");
                    }
                    else
                    {
                      ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.<routines>P.RunNowAsync(clickyRoutine.Id).ConfigureAwait(false).GetAwaiter();
                      if (!configuredTaskAwaiter.IsCompleted)
                      {
                        await configuredTaskAwaiter;
                        ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
                        configuredTaskAwaiter = configuredTaskAwaiter2;
                        configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
                      }
                      JsonObject jsonObject4;
                      if (!configuredTaskAwaiter.GetResult())
                      {
                        JsonObject jsonObject3 = new JsonObject(null);
                        jsonObject3["status"] = "not_started";
                        jsonObject4 = jsonObject3;
                        jsonObject3["message"] = "It couldn't start now (its job is busy, or the PC is offline); it will try again shortly.";
                      }
                      else
                      {
                        (jsonObject4 = new JsonObject(null))["status"] = "started";
                      }
                      jsonObject2 = jsonObject4;
                    }
                  }
                  else
                  {
                    this.<routines>P.Delete(clickyRoutine.Id, "one_brain");
                    JsonObject jsonObject5 = new JsonObject(null);
                    jsonObject5["status"] = "deleted";
                    jsonObject2 = jsonObject5;
                  }
                }
                else
                {
                  this.<routines>P.SetPaused(clickyRoutine.Id, false, "one_brain");
                  JsonObject jsonObject6 = new JsonObject(null);
                  jsonObject6["status"] = "resumed";
                  jsonObject6["routine"] = this.Summary(clickyRoutine.Id);
                  jsonObject2 = jsonObject6;
                }
              }
              else
              {
                this.<routines>P.SetPaused(clickyRoutine.Id, true, "one_brain");
                JsonObject jsonObject7 = new JsonObject(null);
                jsonObject7["status"] = "paused";
                jsonObject2 = jsonObject7;
              }
            }
            else
            {
              string text4 = RoutinesTool.<RunAsync>g__Text|7_0("title", ref CS$<>8__locals1);
              string text5 = RoutinesTool.<RunAsync>g__Text|7_0("task", ref CS$<>8__locals1);
              string text6 = ((text5 != null) ? ClickyRoutine.TaskPromptStrippingLeadingCadence(text5) : null);
              if (text4 != null || text6 != null)
              {
                this.<routines>P.UpdateDetails(clickyRoutine.Id, text4 ?? clickyRoutine.Title, text6 ?? clickyRoutine.TaskPrompt, "one_brain");
              }
              if (num != null || num4 != null || list7 != null || flag)
              {
                object obj;
                int? num5;
                if (num != null || flag)
                {
                  obj = null;
                }
                else if ((obj = clickyRoutine.ScheduledWeekdays) == null)
                {
                  num5 = clickyRoutine.ScheduledWeekday;
                  if (num5 != null)
                  {
                    int valueOrDefault = num5.GetValueOrDefault();
                    (obj = new List<int>(1)).Add(valueOrDefault);
                  }
                  else
                  {
                    obj = null;
                  }
                }
                IReadOnlyList<int> readOnlyList = obj;
                RoutineScheduler routineScheduler = this.<routines>P;
                string id = clickyRoutine.Id;
                int num6 = num ?? clickyRoutine.IntervalMinutes;
                num5 = num4;
                routineScheduler.SetSchedule(id, num6, (num5 != null) ? num5 : clickyRoutine.ScheduledMinuteOfDay, list7 ?? readOnlyList, "one_brain");
              }
              JsonObject jsonObject8 = new JsonObject(null);
              jsonObject8["status"] = "updated";
              jsonObject8["routine"] = this.Summary(clickyRoutine.Id);
              jsonObject2 = jsonObject8;
            }
          }
        }
      }
      return jsonObject2;
    }

    // Token: 0x060004AB RID: 1195 RVA: 0x000ED624 File Offset: 0x000ED624
    [NullableContext(2)]
    [return: Nullable(1)]
    private async Task<JsonObject> CreateAsync(string title, string task, int? everyMinutes, int? minuteOfDay, IReadOnlyList<int> weekdays, string threadId, bool runsNow)
    {
      if (task != null)
      {
        int? num = everyMinutes;
        int? num2 = ((num != null) ? num : ((weekdays != null) ? new int?(1440) : null));
        if (num2 != null)
        {
          int valueOrDefault = num2.GetValueOrDefault();
          if (valueOrDefault > 0)
          {
            string text = ClickyRoutine.TaskPromptStrippingLeadingCadence(task);
            string text2 = title ?? string.Join<string>(' ', task.Split(' ', StringSplitOptions.RemoveEmptyEntries).Take<string>(4));
            string text3 = null;
            string text4 = null;
            if (threadId != null)
            {
              ClickyJob clickyJob = this.<jobs>P.JobForThread(threadId);
              if (clickyJob != null)
              {
                text3 = clickyJob.Id;
              }
              else
              {
                string text5 = this.<clickySlugForThread>P(threadId);
                if (text5 == null)
                {
                  return RoutinesTool.Error("No job with that codexThreadId: leave it out to start a new job for the routine.");
                }
                text4 = text5;
              }
            }
            IReadOnlyList<ClickyRoutine> readOnlyList2;
            if (text3 == null)
            {
              if (text4 == null)
              {
                IReadOnlyList<ClickyRoutine> readOnlyList = Array.Empty<ClickyRoutine>();
                readOnlyList2 = readOnlyList;
              }
              else
              {
                readOnlyList2 = this.<routines>P.ForClicky(text4);
              }
            }
            else
            {
              readOnlyList2 = this.<routines>P.ForJob(text3);
            }
            IReadOnlyList<ClickyRoutine> readOnlyList3 = readOnlyList2;
            ClickyRoutine routine = readOnlyList3.FirstOrDefault<ClickyRoutine>();
            if (routine != null)
            {
              this.<routines>P.UpdateDetails(routine.Id, text2, text, "one_brain");
              this.<routines>P.SetSchedule(routine.Id, valueOrDefault, minuteOfDay, weekdays, "one_brain");
              if (routine.IsPaused)
              {
                this.<routines>P.SetPaused(routine.Id, false, "one_brain");
              }
              if (runsNow)
              {
                await this.<routines>P.RunNowAsync(routine.Id).ConfigureAwait(false);
              }
              JsonObject jsonObject = new JsonObject(null);
              jsonObject["status"] = "updated";
              jsonObject["routine"] = this.Summary(routine.Id);
              return jsonObject;
            }
            if (text3 == null && text4 == null)
            {
              text3 = this.<jobs>P.CreateJob(text2, text, this.<projectsRoot>P(), false).Id;
            }
            ClickyRoutine created = this.<routines>P.CreateRoutine(text4 ?? "", text2, task, valueOrDefault, false, text3);
            if (minuteOfDay != null || weekdays != null)
            {
              this.<routines>P.SetSchedule(created.Id, valueOrDefault, minuteOfDay, weekdays, "one_brain");
            }
            if (runsNow)
            {
              await this.<routines>P.RunNowAsync(created.Id).ConfigureAwait(false);
            }
            JsonObject jsonObject2 = new JsonObject(null);
            jsonObject2["status"] = "created";
            jsonObject2["routine"] = this.Summary(created.Id);
            return jsonObject2;
          }
        }
      }
      return RoutinesTool.Error("create needs task and every_minutes (or days).");
    }

    // Token: 0x060004AC RID: 1196 RVA: 0x000ED6A4 File Offset: 0x000ED6A4
    [NullableContext(2)]
    private ClickyRoutine RoutineNamed(string reference)
    {
      if (reference == null)
      {
        return null;
      }
      IReadOnlyList<ClickyRoutine> routines = this.<routines>P.Routines;
      return routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == reference) ?? routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => string.Equals(r.Title, reference, StringComparison.OrdinalIgnoreCase));
    }

    // Token: 0x060004AD RID: 1197 RVA: 0x000ED6FC File Offset: 0x000ED6FC
    private string Summary(string routineId)
    {
      ClickyRoutine clickyRoutine = this.<routines>P.Routines.FirstOrDefault<ClickyRoutine>((ClickyRoutine r) => r.Id == routineId);
      if (clickyRoutine == null)
      {
        return "";
      }
      return clickyRoutine.Title + ": " + clickyRoutine.ScheduleLabel.ToLowerInvariant() + (clickyRoutine.IsPaused ? ", paused" : (", next run " + RoutinesTool.RunTime(clickyRoutine.NextRunAt)));
    }

    // Token: 0x060004AE RID: 1198 RVA: 0x000ED77C File Offset: 0x000ED77C
    private static JsonObject Error(string message)
    {
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["status"] = "error";
      jsonObject["message"] = message;
      return jsonObject;
    }

    // Token: 0x060004AF RID: 1199 RVA: 0x000ED7C0 File Offset: 0x000ED7C0
    private static string RunTime(DateTimeOffset date)
    {
      return date.ToLocalTime().ToString("MMM d, yyyy 'at' h:mm tt", CultureInfo.GetCultureInfo("en-US"));
    }

    // Token: 0x060004B0 RID: 1200 RVA: 0x000ED7EC File Offset: 0x000ED7EC
    private static string Prefix(string text, int limit)
    {
      if (text.Length <= limit)
      {
        return text;
      }
      if (!char.IsHighSurrogate(text[limit - 1]))
      {
        return text.Substring(0, limit);
      }
      return text.Substring(0, limit - 1);
    }

    // Token: 0x060004B1 RID: 1201 RVA: 0x000ED81C File Offset: 0x000ED81C
    public static int? MinuteOfDay(string text)
    {
      string text2 = text.ToLowerInvariant().Replace(" ", "");
      bool flag = text2.EndsWith("pm", StringComparison.Ordinal);
      bool flag2 = text2.EndsWith("am", StringComparison.Ordinal);
      string text3;
      if (!flag && !flag2)
      {
        text3 = text2;
      }
      else
      {
        string text4 = text2;
        text3 = text4.Substring(0, text4.Length - 2);
      }
      string[] array = text3.Split(':', StringSplitOptions.RemoveEmptyEntries);
      int num = array.Length;
      bool flag3 = num < 1 || num > 2;
      int num2;
      if (flag3 || !RoutinesTool.TryWhole(array[0], out num2))
      {
        return null;
      }
      int num3 = 0;
      if (array.Length == 2 && !RoutinesTool.TryWhole(array[1], out num3))
      {
        return null;
      }
      flag3 = num3 < 0 || num3 >= 60;
      if (flag3)
      {
        return null;
      }
      if (flag || flag2)
      {
        flag3 = num2 < 1 || num2 > 12;
        if (flag3)
        {
          return null;
        }
        num2 = num2 % 12 + (flag ? 12 : 0);
      }
      flag3 = num2 < 0 || num2 >= 24;
      if (flag3)
      {
        return null;
      }
      return new int?(num2 * 60 + num3);
    }

    // Token: 0x060004B2 RID: 1202 RVA: 0x000ED958 File Offset: 0x000ED958
    private static bool TryWhole(string text, out int value)
    {
      return int.TryParse(text, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value);
    }

    // Token: 0x060004B3 RID: 1203 RVA: 0x000ED968 File Offset: 0x000ED968
    public static int? Weekday(string text)
    {
      string lowered = text.ToLowerInvariant().Trim();
      if (lowered.Length < 3)
      {
        return null;
      }
      int num = Array.FindIndex<string>(RoutinesTool.WeekdayNames, (string name) => name.StartsWith(lowered, StringComparison.Ordinal) || lowered.StartsWith(name, StringComparison.Ordinal));
      if (num >= 0)
      {
        return new int?(num + 1);
      }
      return null;
    }

    // Token: 0x060004B6 RID: 1206 RVA: 0x000EDC18 File Offset: 0x000EDC18
    [CompilerGenerated]
    [return: Nullable(2)]
    internal static string <RunAsync>g__Text|7_0(string key, ref RoutinesTool.<>c__DisplayClass7_0 A_1)
    {
      JsonValue jsonValue = A_1.arguments[key] as JsonValue;
      string text;
      if (jsonValue != null && jsonValue.TryGetValue<string>(out text))
      {
        string text2 = text.Trim();
        if (text2 != null && text2.Length > 0)
        {
          return text2;
        }
      }
      return null;
    }

    // Token: 0x040001B9 RID: 441
    [CompilerGenerated]
    private RoutineScheduler <routines>P = routines;

    // Token: 0x040001BA RID: 442
    [CompilerGenerated]
    private ClickyJobStore <jobs>P = jobs;

    // Token: 0x040001BB RID: 443
    [Nullable(new byte[] { 1, 1, 2 })]
    [CompilerGenerated]
    private Func<string, string> <clickySlugForThread>P = clickySlugForThread;

    // Token: 0x040001BC RID: 444
    [Nullable(new byte[] { 1, 1, 2 })]
    [CompilerGenerated]
    private Func<string, string> <clickyName>P = clickyName;

    // Token: 0x040001BD RID: 445
    [CompilerGenerated]
    private Func<string> <projectsRoot>P = projectsRoot;

    // Token: 0x040001BE RID: 446
    private static readonly string[] WeekdayNames = new string[] { "sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday" };

    // Token: 0x02000130 RID: 304
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400067E RID: 1662
      [Nullable(0)]
      public static Func<string, int?> <0>__Weekday;
    }
  }
}
