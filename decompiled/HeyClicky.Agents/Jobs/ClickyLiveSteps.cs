using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000047 RID: 71
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLiveSteps
  {
    // Token: 0x14000017 RID: 23
    // (add) Token: 0x0600063C RID: 1596 RVA: 0x000F5798 File Offset: 0x000F5798
    // (remove) Token: 0x0600063D RID: 1597 RVA: 0x000F57D0 File Offset: 0x000F57D0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<string> Changed;

    // Token: 0x0600063E RID: 1598 RVA: 0x000F5808 File Offset: 0x000F5808
    private void RaiseChanged(string key)
    {
      try
      {
        Action<string> changed = this.Changed;
        if (changed != null)
        {
          changed(key);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Jobs", "live steps handler threw: " + ex.Message);
      }
    }

    // Token: 0x0600063F RID: 1599 RVA: 0x000F5858 File Offset: 0x000F5858
    [return: Nullable(2)]
    public ClickyInkWorkStep Note(string rawActivity, string key, DateTimeOffset? at = null)
    {
      ClickyInkWorkStep clickyInkWorkStep = ClickyLiveSteps.Step(rawActivity, at ?? DateTimeOffset.Now);
      if (clickyInkWorkStep == null)
      {
        return null;
      }
      object obj = this.gate;
      lock (obj)
      {
        ClickyInkWorkStep clickyInkWorkStep2;
        if (this.steps.TryGetValue(key, out clickyInkWorkStep2) && clickyInkWorkStep2.IsSame(clickyInkWorkStep))
        {
          return null;
        }
        this.steps[key] = clickyInkWorkStep;
      }
      this.Offer(ClickyLiveLine.FromStep(clickyInkWorkStep), key);
      this.RaiseChanged(key);
      return clickyInkWorkStep;
    }

    // Token: 0x06000640 RID: 1600 RVA: 0x000F58FC File Offset: 0x000F58FC
    public void Clear(string key)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.steps.Remove(key);
        this.lines.Remove(key);
        this.waitingLines.Remove(key);
        this.plans.Remove(key);
      }
      this.RaiseChanged(key);
    }

    // Token: 0x06000641 RID: 1601 RVA: 0x000F5974 File Offset: 0x000F5974
    public void NoteBatch(ClickyProgramBatch batch, string key, DateTimeOffset? at = null)
    {
      DateTimeOffset dateTimeOffset = at ?? DateTimeOffset.Now;
      ClickyLiveLine clickyLiveLine = ClickyLiveLine.FromBatch(batch, dateTimeOffset);
      if (clickyLiveLine == null)
      {
        return;
      }
      ClickyInkWorkStep clickyInkWorkStep = batch.WorkStep(dateTimeOffset);
      if (clickyInkWorkStep != null)
      {
        object obj = this.gate;
        lock (obj)
        {
          ClickyInkWorkStep clickyInkWorkStep2;
          if (!this.steps.TryGetValue(key, out clickyInkWorkStep2) || !clickyInkWorkStep2.IsSame(clickyInkWorkStep))
          {
            this.steps[key] = clickyInkWorkStep;
          }
        }
      }
      this.Offer(clickyLiveLine, key);
    }

    // Token: 0x06000642 RID: 1602 RVA: 0x000F5A14 File Offset: 0x000F5A14
    [return: Nullable(2)]
    public ClickyArcPlan Plan(string key)
    {
      object obj = this.gate;
      ClickyArcPlan valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.plans.GetValueOrDefault(key);
      }
      return valueOrDefault;
    }

    // Token: 0x06000643 RID: 1603 RVA: 0x000F5A5C File Offset: 0x000F5A5C
    [return: Nullable(2)]
    public ClickyArcPlan LastPlan(string key)
    {
      object obj = this.gate;
      ClickyArcPlan valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.lastPlans.GetValueOrDefault(key);
      }
      return valueOrDefault;
    }

    // Token: 0x06000644 RID: 1604 RVA: 0x000F5AA4 File Offset: 0x000F5AA4
    public void NotePlan(ClickyArcPlan plan, string key)
    {
      object obj = this.gate;
      lock (obj)
      {
        ClickyArcPlan clickyArcPlan;
        if (this.plans.TryGetValue(key, out clickyArcPlan) && clickyArcPlan.Equals(plan))
        {
          return;
        }
        this.plans[key] = plan;
        this.lastPlans[key] = plan;
      }
      this.RaiseChanged(key);
    }

    // Token: 0x06000645 RID: 1605 RVA: 0x000F5B1C File Offset: 0x000F5B1C
    public void RememberLastPlan(ClickyArcPlan plan, string key)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.lastPlans[key] = plan;
      }
    }

    // Token: 0x06000646 RID: 1606 RVA: 0x000F5B64 File Offset: 0x000F5B64
    [return: Nullable(2)]
    public ClickyLiveLine Line(string key)
    {
      object obj = this.gate;
      ClickyLiveLine valueOrDefault;
      lock (obj)
      {
        valueOrDefault = this.lines.GetValueOrDefault(key);
      }
      return valueOrDefault;
    }

    // Token: 0x06000647 RID: 1607 RVA: 0x000F5BAC File Offset: 0x000F5BAC
    public void NoteThinking(string heading, string key, DateTimeOffset? at = null)
    {
      string text = heading.Trim();
      if (text.Length == 0)
      {
        return;
      }
      this.Offer(new ClickyLiveLine(ClickyLiveLine.LineKind.Thinking, null, text, at ?? DateTimeOffset.Now), key);
    }

    // Token: 0x06000648 RID: 1608 RVA: 0x000F5BFC File Offset: 0x000F5BFC
    private void Offer(ClickyLiveLine line, string key)
    {
      DateTimeOffset now = DateTimeOffset.Now;
      object obj = this.gate;
      bool flag2;
      TimeSpan timeSpan;
      lock (obj)
      {
        ClickyLiveLine clickyLiveLine;
        if (this.lines.TryGetValue(key, out clickyLiveLine) && clickyLiveLine.Kind == line.Kind)
        {
          ClickyInkWorkStep.StepKind? stepKind = clickyLiveLine.StepKind;
          ClickyInkWorkStep.StepKind? stepKind2 = line.StepKind;
          if (((stepKind.GetValueOrDefault() == stepKind2.GetValueOrDefault()) & (stepKind != null == (stepKind2 != null))) && clickyLiveLine.Text == line.Text && clickyLiveLine.Icons.SequenceEqual<ClickyLiveLineIcon>(line.Icons) && clickyLiveLine.MoreIcons == line.MoreIcons)
          {
            return;
          }
        }
        if (clickyLiveLine != null)
        {
          string batchId = line.BatchId;
          if (batchId != null && clickyLiveLine.BatchId == batchId)
          {
            Dictionary<string, ClickyLiveLine> dictionary = this.lines;
            string key2 = key;
            ClickyLiveLine clickyLiveLine2 = line.<Clone>$();
            clickyLiveLine2.ShownAt = clickyLiveLine.ShownAt;
            dictionary[key2] = clickyLiveLine2;
            flag2 = false;
            timeSpan = TimeSpan.Zero;
            goto IL_01BB;
          }
        }
        if (!this.lines.TryGetValue(key, out clickyLiveLine) || now - clickyLiveLine.ShownAt >= ClickyLiveSteps.LineHold)
        {
          Dictionary<string, ClickyLiveLine> dictionary2 = this.lines;
          string key3 = key;
          ClickyLiveLine clickyLiveLine3 = line.<Clone>$();
          clickyLiveLine3.ShownAt = now;
          dictionary2[key3] = clickyLiveLine3;
          flag2 = false;
          timeSpan = TimeSpan.Zero;
        }
        else
        {
          flag2 = !this.waitingLines.ContainsKey(key);
          this.waitingLines[key] = line;
          if (!flag2)
          {
            return;
          }
          timeSpan = ClickyLiveSteps.LineHold - (now - clickyLiveLine.ShownAt);
        }
      }
      IL_01BB:
      if (!flag2)
      {
        this.RaiseChanged(key);
        return;
      }
      Task.Delay((timeSpan < TimeSpan.Zero) ? TimeSpan.Zero : timeSpan).ContinueWith(delegate(Task _)
      {
        object obj2 = this.gate;
        lock (obj2)
        {
          ClickyLiveLine clickyLiveLine4;
          if (!this.waitingLines.Remove(key, out clickyLiveLine4) || !this.lines.ContainsKey(key))
          {
            return;
          }
          Dictionary<string, ClickyLiveLine> dictionary3 = this.lines;
          string key4 = key;
          ClickyLiveLine clickyLiveLine5 = clickyLiveLine4.<Clone>$();
          clickyLiveLine5.ShownAt = DateTimeOffset.Now;
          dictionary3[key4] = clickyLiveLine5;
        }
        this.RaiseChanged(key);
      }, TaskScheduler.Default);
    }

    // Token: 0x06000649 RID: 1609 RVA: 0x000F5E24 File Offset: 0x000F5E24
    [return: Nullable(2)]
    public string Current(string key, DateTimeOffset? now = null)
    {
      ClickyInkWorkStep clickyInkWorkStep = this.CurrentStep(key, now);
      if (clickyInkWorkStep == null)
      {
        return null;
      }
      return clickyInkWorkStep.Present;
    }

    // Token: 0x0600064A RID: 1610 RVA: 0x000F5E3C File Offset: 0x000F5E3C
    [return: Nullable(2)]
    public ClickyInkWorkStep CurrentStep(string key, DateTimeOffset? now = null)
    {
      object obj = this.gate;
      ClickyInkWorkStep clickyInkWorkStep;
      lock (obj)
      {
        ClickyInkWorkStep clickyInkWorkStep2;
        clickyInkWorkStep = ((this.steps.TryGetValue(key, out clickyInkWorkStep2) && (now ?? DateTimeOffset.Now) - clickyInkWorkStep2.At < ClickyLiveSteps.FreshFor) ? clickyInkWorkStep2 : null);
      }
      return clickyInkWorkStep;
    }

    // Token: 0x0600064B RID: 1611 RVA: 0x000F5EC0 File Offset: 0x000F5EC0
    [return: Nullable(2)]
    public static string Phrase(string activity)
    {
      ClickyInkWorkStep clickyInkWorkStep = ClickyLiveSteps.Step(activity, DateTimeOffset.Now);
      if (clickyInkWorkStep == null)
      {
        return null;
      }
      return clickyInkWorkStep.Present;
    }

    // Token: 0x0600064C RID: 1612 RVA: 0x000F5ED8 File Offset: 0x000F5ED8
    [return: Nullable(2)]
    public static ClickyInkWorkStep Step(string activity, DateTimeOffset at)
    {
      ClickyLiveSteps.<>c__DisplayClass25_0 CS$<>8__locals1 = new ClickyLiveSteps.<>c__DisplayClass25_0();
      CS$<>8__locals1.at = at;
      CS$<>8__locals1.trimmed = activity.Trim();
      string text = CS$<>8__locals1.<Step>g__After|0("Search:");
      if (text != null)
      {
        if (text.Length == 0 || text == "the web")
        {
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Search, null);
        }
        if (ClickyLiveSteps.IsMath(text))
        {
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Math, null);
        }
        ValueTuple<string, string> valueTuple = ClickyLiveSteps.ReadableSearch(text);
        if (valueTuple.Item2.Length > 0)
        {
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Search, "“" + ClickyLiveSteps.Shortened(valueTuple.Item2, 34) + "”");
        }
        return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Search, valueTuple.Item1);
      }
      else
      {
        string text2 = CS$<>8__locals1.<Step>g__After|0("Reading:");
        if (text2 != null)
        {
          ClickyLiveSteps.<>c__DisplayClass25_0 CS$<>8__locals2 = CS$<>8__locals1;
          ClickyInkWorkStep.StepKind stepKind = ClickyInkWorkStep.StepKind.Read;
          string text3;
          if (!text2.StartsWith("www.", StringComparison.Ordinal))
          {
            text3 = text2;
          }
          else
          {
            string text4 = text2;
            text3 = text4.Substring(4, text4.Length - 4);
          }
          return CS$<>8__locals2.<Step>g__Make|1(stepKind, text3);
        }
        if (CS$<>8__locals1.<Step>g__After|0("Finding:") != null)
        {
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Look, null);
        }
        string text5 = CS$<>8__locals1.<Step>g__After|0("Editing:");
        if (text5 != null)
        {
          List<string> list = (from n in text5.Split(',', StringSplitOptions.None)
            select n.Trim() into n
            where n.Length > 0
            select n).ToList<string>();
          if (list.Count != 0)
          {
            if (!list.Any<string>((string n) => n.EndsWith("note.md", StringComparison.Ordinal)))
            {
              return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.File, list[0]);
            }
          }
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Note, null);
        }
        string text6 = CS$<>8__locals1.<Step>g__After|0("Using:");
        if (text6 != null)
        {
          return ClickyLiveSteps.ToolStep(text6.ToLowerInvariant(), new Func<ClickyInkWorkStep.StepKind, string, ClickyInkWorkStep>(CS$<>8__locals1.<Step>g__Make|1));
        }
        if (CS$<>8__locals1.trimmed.Contains("note.md", StringComparison.Ordinal))
        {
          return CS$<>8__locals1.<Step>g__Make|1(ClickyInkWorkStep.StepKind.Note, null);
        }
        return null;
      }
    }

    // Token: 0x0600064D RID: 1613 RVA: 0x000F60D4 File Offset: 0x000F60D4
    [return: Nullable(2)]
    private static ClickyInkWorkStep ToolStep(string tool, [Nullable(new byte[] { 1, 2, 1 })] Func<ClickyInkWorkStep.StepKind, string, ClickyInkWorkStep> make)
    {
      ClickyLiveSteps.<>c__DisplayClass26_0 CS$<>8__locals1;
      CS$<>8__locals1.tool = tool;
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "gmail", "outlook", "mail" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Apps, "your email");
      }
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "calendar" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Apps, "your calendar");
      }
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "screenshot", "window", "click", "type text", "launch app", "scroll", "hotkey", "press key", "desktop" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Mac, null);
      }
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "composio", "multi execute", "tool schema", "drive", "docs", "sheet", "notion", "linear", "github", "slack" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Apps, null);
      }
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "edit", "patch", "note", "write" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Note, null);
      }
      if (ClickyLiveSteps.<ToolStep>g__Has|26_0(new string[] { "search" }, ref CS$<>8__locals1))
      {
        return make(ClickyInkWorkStep.StepKind.Search, null);
      }
      return null;
    }

    // Token: 0x0600064E RID: 1614 RVA: 0x000F6268 File Offset: 0x000F6268
    public static bool IsMath(string query)
    {
      string text = query.ToLowerInvariant();
      if (text.StartsWith("calculator", StringComparison.Ordinal) || text.StartsWith("calc:", StringComparison.Ordinal))
      {
        return true;
      }
      Func<char, bool> func;
      if ((func = ClickyLiveSteps.<>O.<0>__IsDigit) == null)
      {
        func = (ClickyLiveSteps.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
      }
      bool flag = query.Any<char>(func);
      bool flag2 = query.IndexOfAny(new char[] { '+', '*', '/', '×', '÷', '=', '^' }) >= 0;
      if (flag && flag2)
      {
        return query.All<char>((char c) => "0123456789.,+-*/×÷x%=()^ $".Contains(c));
      }
      return false;
    }

    // Token: 0x0600064F RID: 1615 RVA: 0x000F6308 File Offset: 0x000F6308
    public static string Shortened(string words, int limit)
    {
      if (words.Length <= limit)
      {
        return words;
      }
      string text = words.Substring(0, limit);
      int num = text.LastIndexOf(' ');
      return ((num >= 0) ? text.Substring(0, num) : text).Trim() + "…";
    }

    // Token: 0x06000650 RID: 1616 RVA: 0x000F6350 File Offset: 0x000F6350
    [return: TupleElementNames(new string[] { "Site", "Words" })]
    [return: Nullable(new byte[] { 0, 2, 1 })]
    public static ValueTuple<string, string> ReadableSearch(string query)
    {
      string text = null;
      List<string> list = new List<string>();
      string[] array = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < array.Length; i++)
      {
        string text2 = array[i];
        string lowered = text2.ToLowerInvariant();
        if (lowered.StartsWith("site:", StringComparison.Ordinal))
        {
          string text3 = text2;
          string text4 = text3.Substring(5, text3.Length - 5);
          int num = text4.IndexOf('/');
          if (num >= 0)
          {
            text4 = text4.Substring(0, num);
          }
          if (text4.StartsWith("www.", StringComparison.Ordinal))
          {
            text3 = text4;
            text4 = text3.Substring(4, text3.Length - 4);
          }
          if (text == null && text4.Length > 0)
          {
            text = text4;
          }
        }
        else if (!new string[] { "intitle:", "inurl:", "intext:", "filetype:", "after:", "before:" }.Any<string>((string p) => lowered.StartsWith(p, StringComparison.Ordinal)))
        {
          bool flag = text2 == "OR" || text2 == "AND" || text2 == "|";
          if (!flag && (!text2.StartsWith('-') || text2.Length <= 1))
          {
            string text5 = text2.Replace("\"", "");
            if (text5.Length > 0)
            {
              list.Add(text5);
            }
          }
        }
      }
      return new ValueTuple<string, string>(text, string.Join(" ", list));
    }

    // Token: 0x06000653 RID: 1619 RVA: 0x000F656C File Offset: 0x000F656C
    [CompilerGenerated]
    internal static bool <ToolStep>g__Has|26_0(string[] words, ref ClickyLiveSteps.<>c__DisplayClass26_0 A_1)
    {
      return words.Any<string>(new Func<string, bool>(A_1.tool.Contains));
    }

    // Token: 0x04000254 RID: 596
    public static readonly TimeSpan FreshFor = TimeSpan.FromSeconds(90L);

    // Token: 0x04000255 RID: 597
    public static readonly TimeSpan LineHold = TimeSpan.FromSeconds(2.5);

    // Token: 0x04000256 RID: 598
    private readonly object gate = new object();

    // Token: 0x04000257 RID: 599
    private readonly Dictionary<string, ClickyInkWorkStep> steps = new Dictionary<string, ClickyInkWorkStep>();

    // Token: 0x04000258 RID: 600
    private readonly Dictionary<string, ClickyArcPlan> plans = new Dictionary<string, ClickyArcPlan>();

    // Token: 0x04000259 RID: 601
    private readonly Dictionary<string, ClickyArcPlan> lastPlans = new Dictionary<string, ClickyArcPlan>();

    // Token: 0x0400025A RID: 602
    private readonly Dictionary<string, ClickyLiveLine> lines = new Dictionary<string, ClickyLiveLine>();

    // Token: 0x0400025B RID: 603
    private readonly Dictionary<string, ClickyLiveLine> waitingLines = new Dictionary<string, ClickyLiveLine>();

    // Token: 0x0200016F RID: 367
    [CompilerGenerated]
    private static class <>O
    {
      // Token: 0x0400073D RID: 1853
      [Nullable(0)]
      public static Func<char, bool> <0>__IsDigit;
    }
  }
}
