using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;

namespace HeyClicky.Core.Brain
{
  // Token: 0x02000202 RID: 514
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class OneBrainStats
  {
    // Token: 0x170006A3 RID: 1699
    // (get) Token: 0x06001DF7 RID: 7671 RVA: 0x003EAC6C File Offset: 0x003EAC6C
    public static OneBrainStats Shared { get; } = new OneBrainStats();

    // Token: 0x170006A4 RID: 1700
    // (get) Token: 0x06001DF8 RID: 7672 RVA: 0x003EAC74 File Offset: 0x003EAC74
    // (set) Token: 0x06001DF9 RID: 7673 RVA: 0x003EAC7C File Offset: 0x003EAC7C
    public Func<bool> IsEnabledProvider { get; set; } = () => false;

    // Token: 0x170006A5 RID: 1701
    // (get) Token: 0x06001DFA RID: 7674 RVA: 0x003EAC88 File Offset: 0x003EAC88
    public bool IsEnabled
    {
      get
      {
        return this.IsEnabledProvider();
      }
    }

    // Token: 0x1400003C RID: 60
    // (add) Token: 0x06001DFB RID: 7675 RVA: 0x003EAC98 File Offset: 0x003EAC98
    // (remove) Token: 0x06001DFC RID: 7676 RVA: 0x003EACD0 File Offset: 0x003EACD0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler Changed;

    // Token: 0x170006A6 RID: 1702
    // (get) Token: 0x06001DFD RID: 7677 RVA: 0x003EAD08 File Offset: 0x003EAD08
    // (set) Token: 0x06001DFE RID: 7678 RVA: 0x003EAD10 File Offset: 0x003EAD10
    internal Func<DateTimeOffset> Now { get; set; } = () => DateTimeOffset.Now;

    // Token: 0x170006A7 RID: 1703
    // (get) Token: 0x06001DFF RID: 7679 RVA: 0x003EAD1C File Offset: 0x003EAD1C
    public OneBrainStats.Session CurrentSession
    {
      get
      {
        object obj = this.gate;
        OneBrainStats.Session session;
        lock (obj)
        {
          session = this.session;
        }
        return session;
      }
    }

    // Token: 0x170006A8 RID: 1704
    // (get) Token: 0x06001E00 RID: 7680 RVA: 0x003EAD60 File Offset: 0x003EAD60
    [Nullable(2)]
    public OneBrainStats.Turn LastTurn
    {
      [NullableContext(2)]
      get
      {
        object obj = this.gate;
        OneBrainStats.Turn turn;
        lock (obj)
        {
          turn = this.lastTurn;
        }
        return turn;
      }
    }

    // Token: 0x170006A9 RID: 1705
    // (get) Token: 0x06001E01 RID: 7681 RVA: 0x003EADA4 File Offset: 0x003EADA4
    public IReadOnlyList<OneBrainStats.Event> Events
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<OneBrainStats.Event> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.events.ToList<OneBrainStats.Event>();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06001E02 RID: 7682 RVA: 0x003EADEC File Offset: 0x003EADEC
    public void UpdateSession(Func<OneBrainStats.Session, OneBrainStats.Session> update)
    {
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        OneBrainStats.Session session = update(this.session);
        flag2 = session != this.session;
        if (flag2)
        {
          this.session = session;
        }
      }
      if (flag2)
      {
        this.Raise();
      }
    }

    // Token: 0x06001E03 RID: 7683 RVA: 0x003EAE5C File Offset: 0x003EAE5C
    public void Note(string text)
    {
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.events.Add(new OneBrainStats.Event(this.Now(), text));
        if (this.events.Count > 8)
        {
          this.events.RemoveRange(0, this.events.Count - 8);
        }
      }
      this.Raise();
    }

    // Token: 0x06001E04 RID: 7684 RVA: 0x003EAEE8 File Offset: 0x003EAEE8
    public void NoteKeyUp()
    {
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        this.pendingKeyUpAt = new DateTimeOffset?(this.Now());
      }
    }

    // Token: 0x06001E05 RID: 7685 RVA: 0x003EAF44 File Offset: 0x003EAF44
    public void NoteTurnSent(bool spoken, DateTimeOffset? previousTurnAt)
    {
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        DateTimeOffset dateTimeOffset = this.Now();
        DateTimeOffset? dateTimeOffset3;
        if (spoken)
        {
          DateTimeOffset? dateTimeOffset2 = this.pendingKeyUpAt;
          if (dateTimeOffset2 != null)
          {
            DateTimeOffset valueOrDefault = dateTimeOffset2.GetValueOrDefault();
            if (dateTimeOffset - valueOrDefault < TimeSpan.FromSeconds(15L))
            {
              dateTimeOffset3 = new DateTimeOffset?(valueOrDefault);
              goto IL_006D;
            }
          }
        }
        dateTimeOffset3 = null;
        IL_006D:
        DateTimeOffset? dateTimeOffset4 = dateTimeOffset3;
        this.pendingKeyUpAt = null;
        OneBrainStats.Turn turn = new OneBrainStats.Turn();
        turn.Kind = (spoken ? "spoken" : "app/click");
        turn.KeyUpAt = dateTimeOffset4;
        turn.SentAt = new DateTimeOffset?(dateTimeOffset);
        double? num;
        if (previousTurnAt != null)
        {
          DateTimeOffset valueOrDefault2 = previousTurnAt.GetValueOrDefault();
          if (valueOrDefault2 > DateTimeOffset.MinValue)
          {
            num = new double?((dateTimeOffset - valueOrDefault2).TotalMinutes);
            goto IL_00EB;
          }
        }
        num = null;
        IL_00EB:
        turn.IdleMinutesBefore = num;
        this.lastTurn = turn;
      }
      this.Raise();
    }

    // Token: 0x06001E06 RID: 7686 RVA: 0x003EB068 File Offset: 0x003EB068
    public void NoteRound(int round, [Nullable(2)] JsonObject usage, double? cost, string model, int? firstTokenMilliseconds)
    {
      OneBrainStats.<>c__DisplayClass34_0 CS$<>8__locals1;
      CS$<>8__locals1.usage = usage;
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        OneBrainStats.Turn turn = this.lastTurn;
        if (turn == null)
        {
          return;
        }
        OneBrainStats.Turn turn2 = turn.<Clone>$();
        turn2.Rounds = Math.Max(turn.Rounds, round + 1);
        turn2.Model = model;
        turn2.Cost = turn.Cost + cost.GetValueOrDefault();
        turn = turn2;
        if (round == 0)
        {
          OneBrainStats.Turn turn3 = turn.<Clone>$();
          turn3.Input = OneBrainStats.<NoteRound>g__Number|34_0("input", ref CS$<>8__locals1);
          turn3.Cached = OneBrainStats.<NoteRound>g__Number|34_0("cached", ref CS$<>8__locals1);
          turn3.Wrote = OneBrainStats.<NoteRound>g__Number|34_0("wrote", ref CS$<>8__locals1);
          turn3.Output = OneBrainStats.<NoteRound>g__Number|34_0("output", ref CS$<>8__locals1);
          turn3.Reasoning = OneBrainStats.<NoteRound>g__Number|34_0("reasoning", ref CS$<>8__locals1);
          turn3.FirstTokenMilliseconds = firstTokenMilliseconds;
          turn = turn3;
        }
        this.lastTurn = turn;
      }
      this.Raise();
    }

    // Token: 0x06001E07 RID: 7687 RVA: 0x003EB174 File Offset: 0x003EB174
    public void NoteFirstLineHeard()
    {
      if (!this.IsEnabled)
      {
        return;
      }
      object obj = this.gate;
      lock (obj)
      {
        OneBrainStats.Turn turn = this.lastTurn;
        if (turn == null || turn.FirstHeardAt != null)
        {
          return;
        }
        OneBrainStats.Turn turn2 = turn.<Clone>$();
        turn2.FirstHeardAt = new DateTimeOffset?(this.Now());
        this.lastTurn = turn2;
      }
      this.Raise();
    }

    // Token: 0x06001E08 RID: 7688 RVA: 0x003EB1FC File Offset: 0x003EB1FC
    public void ResetForNewSession()
    {
      object obj = this.gate;
      lock (obj)
      {
        this.lastTurn = null;
        this.pendingKeyUpAt = null;
      }
      this.Raise();
    }

    // Token: 0x06001E09 RID: 7689 RVA: 0x003EB250 File Offset: 0x003EB250
    private void Raise()
    {
      try
      {
        EventHandler changed = this.Changed;
        if (changed != null)
        {
          changed(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        Log.Warn("OneBrain", "stats listener threw: " + ex.Message);
      }
    }

    // Token: 0x06001E0A RID: 7690 RVA: 0x003EB2A4 File Offset: 0x003EB2A4
    [return: TupleElementNames(new string[] { "Lines", "Progress", "PastThreshold", "ProgressAfterLine", "Text", "Style" })]
    [return: Nullable(new byte[] { 0, 1, 0, 1, 1 })]
    public ValueTuple<List<ValueTuple<string, string>>, double, bool, int> Describe(DateTimeOffset now)
    {
      object obj = this.gate;
      OneBrainStats.Session session;
      OneBrainStats.Turn turn;
      List<OneBrainStats.Event> list;
      lock (obj)
      {
        session = this.session;
        turn = this.lastTurn;
        list = this.events.TakeLast<OneBrainStats.Event>(5).ToList<OneBrainStats.Event>();
      }
      List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>();
      int lastInputTokens = session.LastInputTokens;
      int baselineInputTokens = session.BaselineInputTokens;
      int num = ((baselineInputTokens > 0) ? (baselineInputTokens + 45000) : 75000);
      list2.Add(new ValueTuple<string, string>("ONE BRAIN  ·  session " + ((session.Id.Length > 8) ? session.Id.Substring(0, 8) : session.Id) + "  ·  " + OneBrainStats.SessionAge(session, now), "bold"));
      list2.Add(new ValueTuple<string, string>("Sent to the model: " + OneBrainStats.K(lastInputTokens) + " tokens", "bold"));
      if (baselineInputTokens > 0)
      {
        list2.Add(new ValueTuple<string, string>("= base " + OneBrainStats.K(baselineInputTokens) + " (system prompt, tools, memory, first turn)", "dim"));
        list2.Add(new ValueTuple<string, string>("+ conversation " + OneBrainStats.K(Math.Max(0, lastInputTokens - baselineInputTokens)) + " (turns, replies, screenshots, notes)", "dim"));
      }
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
      if (baselineInputTokens == 0 && lastInputTokens == 0)
      {
        list2.Add(new ValueTuple<string, string>("Compacts once the conversation reaches " + OneBrainStats.K(45000) + " on top of the base (measured from the first turn)", "dim"));
      }
      else
      {
        List<ValueTuple<string, string>> list3 = list2;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Compacts at ");
        defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.K(num));
        defaultInterpolatedStringHandler.AppendLiteral(" total");
        defaultInterpolatedStringHandler.AppendFormatted((baselineInputTokens == 0) ? " (base unknown, whole-input fallback)" : "");
        defaultInterpolatedStringHandler.AppendLiteral("  ·  ");
        defaultInterpolatedStringHandler.AppendFormatted(session.IsCompacting ? "compacting…" : (OneBrainStats.K(Math.Max(0, num - lastInputTokens)) + " to go"));
        list3.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), ""));
      }
      int count = list2.Count;
      double num2 = (double)Math.Min(lastInputTokens, num) / (double)Math.Max(num, 1);
      List<ValueTuple<string, string>> list4 = list2;
      defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 3);
      defaultInterpolatedStringHandler.AppendLiteral("Thread ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(session.ThreadItems);
      defaultInterpolatedStringHandler.AppendLiteral(" items  ·  ");
      defaultInterpolatedStringHandler.AppendFormatted<int>(session.ScreenshotsInThread);
      defaultInterpolatedStringHandler.AppendLiteral(" screenshots (~");
      defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.K(session.ScreenshotsInThread * 1400));
      defaultInterpolatedStringHandler.AppendLiteral(")");
      list4.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), ""));
      if (session.NextCompactionCheckInputTokens > 0)
      {
        list2.Add(new ValueTuple<string, string>("Worker said too little to fold: next ask at " + OneBrainStats.K(session.NextCompactionCheckInputTokens) + " total", "faint"));
      }
      list2.Add(new ValueTuple<string, string>("—", "divider"));
      if (turn != null)
      {
        int num3 = Math.Max(0, turn.Input - turn.Cached - turn.Wrote);
        int num4 = ((turn.Input > 0) ? ((int)((double)turn.Cached / (double)turn.Input * 100.0)) : 0);
        List<ValueTuple<string, string>> list5 = list2;
        defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 3);
        defaultInterpolatedStringHandler.AppendLiteral("Last turn (");
        defaultInterpolatedStringHandler.AppendFormatted(turn.Kind);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted((turn.Model.Length == 0) ? "…" : turn.Model);
        defaultInterpolatedStringHandler.AppendLiteral(")  ·  ");
        defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.Age(turn.SentAt, now));
        defaultInterpolatedStringHandler.AppendLiteral(" ago");
        list5.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), "bold"));
        if (turn.Input > 0)
        {
          double? idleMinutesBefore = turn.IdleMinutesBefore;
          string text;
          if (idleMinutesBefore != null)
          {
            double valueOrDefault = idleMinutesBefore.GetValueOrDefault();
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
            defaultInterpolatedStringHandler.AppendFormatted<double>(valueOrDefault, "0");
            defaultInterpolatedStringHandler.AppendLiteral(" min since the turn before");
            text = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            text = "first turn";
          }
          string text2 = text;
          List<ValueTuple<string, string>> list6 = list2;
          string text3;
          if (num4 < 50)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
            defaultInterpolatedStringHandler.AppendLiteral("Cache MISS: ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num4);
            defaultInterpolatedStringHandler.AppendLiteral("% read, rewrote the prompt  (");
            defaultInterpolatedStringHandler.AppendFormatted(text2);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          else
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(15, 2);
            defaultInterpolatedStringHandler.AppendLiteral("Cache HIT ");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num4);
            defaultInterpolatedStringHandler.AppendLiteral("%  (");
            defaultInterpolatedStringHandler.AppendFormatted(text2);
            defaultInterpolatedStringHandler.AppendLiteral(")");
            text3 = defaultInterpolatedStringHandler.ToStringAndClear();
          }
          list6.Add(new ValueTuple<string, string>(text3, (num4 >= 50) ? "hit" : "miss"));
          List<ValueTuple<string, string>> list7 = list2;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 3);
          defaultInterpolatedStringHandler.AppendLiteral("Cache read ");
          defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.K(turn.Cached));
          defaultInterpolatedStringHandler.AppendLiteral("  ·  wrote ");
          defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.K(turn.Wrote));
          defaultInterpolatedStringHandler.AppendLiteral("  ·  fresh ");
          defaultInterpolatedStringHandler.AppendFormatted(OneBrainStats.K(num3));
          list7.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), ""));
          List<ValueTuple<string, string>> list8 = list2;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 4);
          defaultInterpolatedStringHandler.AppendLiteral("Reasoning ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(turn.Reasoning);
          defaultInterpolatedStringHandler.AppendLiteral("  ·  output ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(turn.Output);
          defaultInterpolatedStringHandler.AppendLiteral("  ·  rounds ");
          defaultInterpolatedStringHandler.AppendFormatted<int>(turn.Rounds);
          defaultInterpolatedStringHandler.AppendLiteral("  ·  $");
          defaultInterpolatedStringHandler.AppendFormatted(turn.Cost.ToString("0.000", CultureInfo.InvariantCulture));
          list8.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), ""));
        }
        list2.Add(new ValueTuple<string, string>(OneBrainStats.TimingLine(turn), ""));
      }
      else
      {
        list2.Add(new ValueTuple<string, string>("No turn yet this session", "faint"));
      }
      if (list.Count > 0)
      {
        list2.Add(new ValueTuple<string, string>("—", "divider"));
        foreach (OneBrainStats.Event @event in list)
        {
          List<ValueTuple<string, string>> list9 = list2;
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
          defaultInterpolatedStringHandler.AppendFormatted<DateTimeOffset>(@event.At, "HH:mm:ss");
          defaultInterpolatedStringHandler.AppendLiteral("  ");
          defaultInterpolatedStringHandler.AppendFormatted(@event.Text);
          list9.Add(new ValueTuple<string, string>(defaultInterpolatedStringHandler.ToStringAndClear(), "event"));
        }
      }
      return new ValueTuple<List<ValueTuple<string, string>>, double, bool, int>(list2, num2, lastInputTokens >= num, count);
    }

    // Token: 0x06001E0B RID: 7691 RVA: 0x003EB994 File Offset: 0x003EB994
    private static string SessionAge(OneBrainStats.Session s, DateTimeOffset now)
    {
      DateTimeOffset? startedAt = s.StartedAt;
      if (startedAt != null)
      {
        DateTimeOffset valueOrDefault = startedAt.GetValueOrDefault();
        if (!(valueOrDefault <= DateTimeOffset.MinValue))
        {
          return OneBrainStats.Age(new DateTimeOffset?(valueOrDefault), now) + " old";
        }
      }
      if (s.Id.Length != 0)
      {
        return "resumed";
      }
      return "no turn yet";
    }

    // Token: 0x06001E0C RID: 7692 RVA: 0x003EB9F8 File Offset: 0x003EB9F8
    private static string TimingLine(OneBrainStats.Turn turn)
    {
      List<string> list = new List<string>();
      DateTimeOffset? dateTimeOffset = turn.KeyUpAt;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
        dateTimeOffset = turn.SentAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault2 = dateTimeOffset.GetValueOrDefault();
          list.Add("release→sent " + OneBrainStats.Seconds((valueOrDefault2 - valueOrDefault).TotalSeconds));
        }
      }
      int? firstTokenMilliseconds = turn.FirstTokenMilliseconds;
      if (firstTokenMilliseconds != null)
      {
        int valueOrDefault3 = firstTokenMilliseconds.GetValueOrDefault();
        list.Add("1st token " + OneBrainStats.Seconds((double)valueOrDefault3 / 1000.0));
      }
      dateTimeOffset = turn.FirstHeardAt;
      if (dateTimeOffset != null)
      {
        DateTimeOffset valueOrDefault4 = dateTimeOffset.GetValueOrDefault();
        dateTimeOffset = turn.SentAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault5 = dateTimeOffset.GetValueOrDefault();
          list.Add("heard " + OneBrainStats.Seconds((valueOrDefault4 - valueOrDefault5).TotalSeconds));
          dateTimeOffset = turn.KeyUpAt;
          if (dateTimeOffset != null)
          {
            DateTimeOffset valueOrDefault6 = dateTimeOffset.GetValueOrDefault();
            list.Add("TOTAL " + OneBrainStats.Seconds((valueOrDefault4 - valueOrDefault6).TotalSeconds));
          }
        }
      }
      if (list.Count != 0)
      {
        return string.Join("  ·  ", list);
      }
      return "waiting…";
    }

    // Token: 0x06001E0D RID: 7693 RVA: 0x003EBB54 File Offset: 0x003EBB54
    internal static string K(int tokens)
    {
      if (tokens < 1000)
      {
        return tokens.ToString(CultureInfo.InvariantCulture);
      }
      return ((double)tokens / 1000.0).ToString("0.0", CultureInfo.InvariantCulture) + "k";
    }

    // Token: 0x06001E0E RID: 7694 RVA: 0x003EBBA0 File Offset: 0x003EBBA0
    private static string Seconds(double value)
    {
      return value.ToString("0.00", CultureInfo.InvariantCulture) + "s";
    }

    // Token: 0x06001E0F RID: 7695 RVA: 0x003EBBC0 File Offset: 0x003EBBC0
    private static string Age(DateTimeOffset? date, DateTimeOffset now)
    {
      if (date != null)
      {
        DateTimeOffset valueOrDefault = date.GetValueOrDefault();
        if (!(valueOrDefault <= DateTimeOffset.MinValue))
        {
          int num = (int)(now - valueOrDefault).TotalSeconds;
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
          if (num < 60)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
            defaultInterpolatedStringHandler.AppendFormatted<int>(num);
            defaultInterpolatedStringHandler.AppendLiteral("s");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          if (num >= 3600)
          {
            defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
            defaultInterpolatedStringHandler.AppendFormatted<int>(num / 3600);
            defaultInterpolatedStringHandler.AppendLiteral("h");
            defaultInterpolatedStringHandler.AppendFormatted<int>(num % 3600 / 60);
            defaultInterpolatedStringHandler.AppendLiteral("m");
            return defaultInterpolatedStringHandler.ToStringAndClear();
          }
          defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
          defaultInterpolatedStringHandler.AppendFormatted<int>(num / 60);
          defaultInterpolatedStringHandler.AppendLiteral("m");
          return defaultInterpolatedStringHandler.ToStringAndClear();
        }
      }
      return "–";
    }

    // Token: 0x06001E12 RID: 7698 RVA: 0x003EBD30 File Offset: 0x003EBD30
    [CompilerGenerated]
    internal static int <NoteRound>g__Number|34_0(string key, ref OneBrainStats.<>c__DisplayClass34_0 A_1)
    {
      JsonObject usage = A_1.usage;
      JsonValue jsonValue = ((usage != null) ? usage[key] : null) as JsonValue;
      if (jsonValue == null)
      {
        return 0;
      }
      double num;
      if (jsonValue.TryGetValue<double>(out num))
      {
        return (int)num;
      }
      int num2;
      if (jsonValue.TryGetValue<int>(out num2))
      {
        return num2;
      }
      long num3;
      if (!jsonValue.TryGetValue<long>(out num3))
      {
        return 0;
      }
      return (int)num3;
    }

    // Token: 0x04000CE1 RID: 3297
    private readonly object gate = new object();

    // Token: 0x04000CE2 RID: 3298
    private OneBrainStats.Session session = new OneBrainStats.Session();

    // Token: 0x04000CE3 RID: 3299
    [Nullable(2)]
    private OneBrainStats.Turn lastTurn;

    // Token: 0x04000CE4 RID: 3300
    private readonly List<OneBrainStats.Event> events = new List<OneBrainStats.Event>();

    // Token: 0x04000CE5 RID: 3301
    private DateTimeOffset? pendingKeyUpAt;

    // Token: 0x0200065B RID: 1627
    [Nullable(0)]
    public sealed class Session : IEquatable<OneBrainStats.Session>
    {
      // Token: 0x170009D8 RID: 2520
      // (get) Token: 0x0600395F RID: 14687 RVA: 0x004563D0 File Offset: 0x004563D0
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(OneBrainStats.Session);
        }
      }

      // Token: 0x170009D9 RID: 2521
      // (get) Token: 0x06003960 RID: 14688 RVA: 0x004563DC File Offset: 0x004563DC
      // (set) Token: 0x06003961 RID: 14689 RVA: 0x004563E4 File Offset: 0x004563E4
      public string Id { get; set; }

      // Token: 0x170009DA RID: 2522
      // (get) Token: 0x06003962 RID: 14690 RVA: 0x004563F0 File Offset: 0x004563F0
      // (set) Token: 0x06003963 RID: 14691 RVA: 0x004563F8 File Offset: 0x004563F8
      public DateTimeOffset? StartedAt { get; set; }

      // Token: 0x170009DB RID: 2523
      // (get) Token: 0x06003964 RID: 14692 RVA: 0x00456404 File Offset: 0x00456404
      // (set) Token: 0x06003965 RID: 14693 RVA: 0x0045640C File Offset: 0x0045640C
      public DateTimeOffset? LastTurnAt { get; set; }

      // Token: 0x170009DC RID: 2524
      // (get) Token: 0x06003966 RID: 14694 RVA: 0x00456418 File Offset: 0x00456418
      // (set) Token: 0x06003967 RID: 14695 RVA: 0x00456420 File Offset: 0x00456420
      public int ThreadItems { get; set; }

      // Token: 0x170009DD RID: 2525
      // (get) Token: 0x06003968 RID: 14696 RVA: 0x0045642C File Offset: 0x0045642C
      // (set) Token: 0x06003969 RID: 14697 RVA: 0x00456434 File Offset: 0x00456434
      public int ScreenshotsInThread { get; set; }

      // Token: 0x170009DE RID: 2526
      // (get) Token: 0x0600396A RID: 14698 RVA: 0x00456440 File Offset: 0x00456440
      // (set) Token: 0x0600396B RID: 14699 RVA: 0x00456448 File Offset: 0x00456448
      public int LastInputTokens { get; set; }

      // Token: 0x170009DF RID: 2527
      // (get) Token: 0x0600396C RID: 14700 RVA: 0x00456454 File Offset: 0x00456454
      // (set) Token: 0x0600396D RID: 14701 RVA: 0x0045645C File Offset: 0x0045645C
      public int BaselineInputTokens { get; set; }

      // Token: 0x170009E0 RID: 2528
      // (get) Token: 0x0600396E RID: 14702 RVA: 0x00456468 File Offset: 0x00456468
      // (set) Token: 0x0600396F RID: 14703 RVA: 0x00456470 File Offset: 0x00456470
      public int NextCompactionCheckInputTokens { get; set; }

      // Token: 0x170009E1 RID: 2529
      // (get) Token: 0x06003970 RID: 14704 RVA: 0x0045647C File Offset: 0x0045647C
      // (set) Token: 0x06003971 RID: 14705 RVA: 0x00456484 File Offset: 0x00456484
      public bool IsCompacting { get; set; }

      // Token: 0x170009E2 RID: 2530
      // (get) Token: 0x06003972 RID: 14706 RVA: 0x00456490 File Offset: 0x00456490
      // (set) Token: 0x06003973 RID: 14707 RVA: 0x00456498 File Offset: 0x00456498
      public int TurnCount { get; set; }

      // Token: 0x06003974 RID: 14708 RVA: 0x004564A4 File Offset: 0x004564A4
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Session");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x06003975 RID: 14709 RVA: 0x004564F0 File Offset: 0x004564F0
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Id = ");
        builder.Append(this.Id);
        builder.Append(", StartedAt = ");
        builder.Append(this.StartedAt.ToString());
        builder.Append(", LastTurnAt = ");
        builder.Append(this.LastTurnAt.ToString());
        builder.Append(", ThreadItems = ");
        builder.Append(this.ThreadItems.ToString());
        builder.Append(", ScreenshotsInThread = ");
        builder.Append(this.ScreenshotsInThread.ToString());
        builder.Append(", LastInputTokens = ");
        builder.Append(this.LastInputTokens.ToString());
        builder.Append(", BaselineInputTokens = ");
        builder.Append(this.BaselineInputTokens.ToString());
        builder.Append(", NextCompactionCheckInputTokens = ");
        builder.Append(this.NextCompactionCheckInputTokens.ToString());
        builder.Append(", IsCompacting = ");
        builder.Append(this.IsCompacting.ToString());
        builder.Append(", TurnCount = ");
        builder.Append(this.TurnCount.ToString());
        return true;
      }

      // Token: 0x06003976 RID: 14710 RVA: 0x0045667C File Offset: 0x0045667C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(OneBrainStats.Session left, OneBrainStats.Session right)
      {
        return !(left == right);
      }

      // Token: 0x06003977 RID: 14711 RVA: 0x00456688 File Offset: 0x00456688
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(OneBrainStats.Session left, OneBrainStats.Session right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x06003978 RID: 14712 RVA: 0x0045669C File Offset: 0x0045669C
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<StartedAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<LastTurnAt>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ThreadItems>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<ScreenshotsInThread>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<LastInputTokens>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<BaselineInputTokens>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<NextCompactionCheckInputTokens>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<IsCompacting>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<TurnCount>k__BackingField);
      }

      // Token: 0x06003979 RID: 14713 RVA: 0x004567A0 File Offset: 0x004567A0
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as OneBrainStats.Session);
      }

      // Token: 0x0600397A RID: 14714 RVA: 0x004567B0 File Offset: 0x004567B0
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(OneBrainStats.Session other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<StartedAt>k__BackingField, other.<StartedAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<LastTurnAt>k__BackingField, other.<LastTurnAt>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ThreadItems>k__BackingField, other.<ThreadItems>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<ScreenshotsInThread>k__BackingField, other.<ScreenshotsInThread>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<LastInputTokens>k__BackingField, other.<LastInputTokens>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<BaselineInputTokens>k__BackingField, other.<BaselineInputTokens>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<NextCompactionCheckInputTokens>k__BackingField, other.<NextCompactionCheckInputTokens>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<IsCompacting>k__BackingField, other.<IsCompacting>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<TurnCount>k__BackingField, other.<TurnCount>k__BackingField));
      }

      // Token: 0x0600397C RID: 14716 RVA: 0x004568E8 File Offset: 0x004568E8
      [CompilerGenerated]
      private Session(OneBrainStats.Session original)
      {
        this.Id = original.<Id>k__BackingField;
        this.StartedAt = original.<StartedAt>k__BackingField;
        this.LastTurnAt = original.<LastTurnAt>k__BackingField;
        this.ThreadItems = original.<ThreadItems>k__BackingField;
        this.ScreenshotsInThread = original.<ScreenshotsInThread>k__BackingField;
        this.LastInputTokens = original.<LastInputTokens>k__BackingField;
        this.BaselineInputTokens = original.<BaselineInputTokens>k__BackingField;
        this.NextCompactionCheckInputTokens = original.<NextCompactionCheckInputTokens>k__BackingField;
        this.IsCompacting = original.<IsCompacting>k__BackingField;
        this.TurnCount = original.<TurnCount>k__BackingField;
      }

      // Token: 0x0600397D RID: 14717 RVA: 0x00456974 File Offset: 0x00456974
      public Session()
      {
        this.Id = "";
        base..ctor();
      }
    }

    // Token: 0x0200065C RID: 1628
    [Nullable(0)]
    public sealed class Turn : IEquatable<OneBrainStats.Turn>
    {
      // Token: 0x170009E3 RID: 2531
      // (get) Token: 0x0600397E RID: 14718 RVA: 0x00456988 File Offset: 0x00456988
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(OneBrainStats.Turn);
        }
      }

      // Token: 0x170009E4 RID: 2532
      // (get) Token: 0x0600397F RID: 14719 RVA: 0x00456994 File Offset: 0x00456994
      // (set) Token: 0x06003980 RID: 14720 RVA: 0x0045699C File Offset: 0x0045699C
      public string Kind { get; set; }

      // Token: 0x170009E5 RID: 2533
      // (get) Token: 0x06003981 RID: 14721 RVA: 0x004569A8 File Offset: 0x004569A8
      // (set) Token: 0x06003982 RID: 14722 RVA: 0x004569B0 File Offset: 0x004569B0
      public string Model { get; set; }

      // Token: 0x170009E6 RID: 2534
      // (get) Token: 0x06003983 RID: 14723 RVA: 0x004569BC File Offset: 0x004569BC
      // (set) Token: 0x06003984 RID: 14724 RVA: 0x004569C4 File Offset: 0x004569C4
      public DateTimeOffset? KeyUpAt { get; set; }

      // Token: 0x170009E7 RID: 2535
      // (get) Token: 0x06003985 RID: 14725 RVA: 0x004569D0 File Offset: 0x004569D0
      // (set) Token: 0x06003986 RID: 14726 RVA: 0x004569D8 File Offset: 0x004569D8
      public DateTimeOffset? SentAt { get; set; }

      // Token: 0x170009E8 RID: 2536
      // (get) Token: 0x06003987 RID: 14727 RVA: 0x004569E4 File Offset: 0x004569E4
      // (set) Token: 0x06003988 RID: 14728 RVA: 0x004569EC File Offset: 0x004569EC
      public int? FirstTokenMilliseconds { get; set; }

      // Token: 0x170009E9 RID: 2537
      // (get) Token: 0x06003989 RID: 14729 RVA: 0x004569F8 File Offset: 0x004569F8
      // (set) Token: 0x0600398A RID: 14730 RVA: 0x00456A00 File Offset: 0x00456A00
      public DateTimeOffset? FirstHeardAt { get; set; }

      // Token: 0x170009EA RID: 2538
      // (get) Token: 0x0600398B RID: 14731 RVA: 0x00456A0C File Offset: 0x00456A0C
      // (set) Token: 0x0600398C RID: 14732 RVA: 0x00456A14 File Offset: 0x00456A14
      public int Rounds { get; set; }

      // Token: 0x170009EB RID: 2539
      // (get) Token: 0x0600398D RID: 14733 RVA: 0x00456A20 File Offset: 0x00456A20
      // (set) Token: 0x0600398E RID: 14734 RVA: 0x00456A28 File Offset: 0x00456A28
      public int Input { get; set; }

      // Token: 0x170009EC RID: 2540
      // (get) Token: 0x0600398F RID: 14735 RVA: 0x00456A34 File Offset: 0x00456A34
      // (set) Token: 0x06003990 RID: 14736 RVA: 0x00456A3C File Offset: 0x00456A3C
      public int Cached { get; set; }

      // Token: 0x170009ED RID: 2541
      // (get) Token: 0x06003991 RID: 14737 RVA: 0x00456A48 File Offset: 0x00456A48
      // (set) Token: 0x06003992 RID: 14738 RVA: 0x00456A50 File Offset: 0x00456A50
      public int Wrote { get; set; }

      // Token: 0x170009EE RID: 2542
      // (get) Token: 0x06003993 RID: 14739 RVA: 0x00456A5C File Offset: 0x00456A5C
      // (set) Token: 0x06003994 RID: 14740 RVA: 0x00456A64 File Offset: 0x00456A64
      public int Output { get; set; }

      // Token: 0x170009EF RID: 2543
      // (get) Token: 0x06003995 RID: 14741 RVA: 0x00456A70 File Offset: 0x00456A70
      // (set) Token: 0x06003996 RID: 14742 RVA: 0x00456A78 File Offset: 0x00456A78
      public int Reasoning { get; set; }

      // Token: 0x170009F0 RID: 2544
      // (get) Token: 0x06003997 RID: 14743 RVA: 0x00456A84 File Offset: 0x00456A84
      // (set) Token: 0x06003998 RID: 14744 RVA: 0x00456A8C File Offset: 0x00456A8C
      public double Cost { get; set; }

      // Token: 0x170009F1 RID: 2545
      // (get) Token: 0x06003999 RID: 14745 RVA: 0x00456A98 File Offset: 0x00456A98
      // (set) Token: 0x0600399A RID: 14746 RVA: 0x00456AA0 File Offset: 0x00456AA0
      public double? IdleMinutesBefore { get; set; }

      // Token: 0x0600399B RID: 14747 RVA: 0x00456AAC File Offset: 0x00456AAC
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Turn");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x0600399C RID: 14748 RVA: 0x00456AF8 File Offset: 0x00456AF8
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("Kind = ");
        builder.Append(this.Kind);
        builder.Append(", Model = ");
        builder.Append(this.Model);
        builder.Append(", KeyUpAt = ");
        builder.Append(this.KeyUpAt.ToString());
        builder.Append(", SentAt = ");
        builder.Append(this.SentAt.ToString());
        builder.Append(", FirstTokenMilliseconds = ");
        builder.Append(this.FirstTokenMilliseconds.ToString());
        builder.Append(", FirstHeardAt = ");
        builder.Append(this.FirstHeardAt.ToString());
        builder.Append(", Rounds = ");
        builder.Append(this.Rounds.ToString());
        builder.Append(", Input = ");
        builder.Append(this.Input.ToString());
        builder.Append(", Cached = ");
        builder.Append(this.Cached.ToString());
        builder.Append(", Wrote = ");
        builder.Append(this.Wrote.ToString());
        builder.Append(", Output = ");
        builder.Append(this.Output.ToString());
        builder.Append(", Reasoning = ");
        builder.Append(this.Reasoning.ToString());
        builder.Append(", Cost = ");
        builder.Append(this.Cost.ToString());
        builder.Append(", IdleMinutesBefore = ");
        builder.Append(this.IdleMinutesBefore.ToString());
        return true;
      }

      // Token: 0x0600399D RID: 14749 RVA: 0x00456D14 File Offset: 0x00456D14
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(OneBrainStats.Turn left, OneBrainStats.Turn right)
      {
        return !(left == right);
      }

      // Token: 0x0600399E RID: 14750 RVA: 0x00456D20 File Offset: 0x00456D20
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(OneBrainStats.Turn left, OneBrainStats.Turn right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x0600399F RID: 14751 RVA: 0x00456D34 File Offset: 0x00456D34
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (((((((((((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Kind>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Model>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<KeyUpAt>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<SentAt>k__BackingField)) * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(this.<FirstTokenMilliseconds>k__BackingField)) * -1521134295 + EqualityComparer<DateTimeOffset?>.Default.GetHashCode(this.<FirstHeardAt>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Rounds>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Input>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Cached>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Wrote>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Output>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Reasoning>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Cost>k__BackingField)) * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(this.<IdleMinutesBefore>k__BackingField);
      }

      // Token: 0x060039A0 RID: 14752 RVA: 0x00456E94 File Offset: 0x00456E94
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as OneBrainStats.Turn);
      }

      // Token: 0x060039A1 RID: 14753 RVA: 0x00456EA4 File Offset: 0x00456EA4
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(OneBrainStats.Turn other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Kind>k__BackingField, other.<Kind>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Model>k__BackingField, other.<Model>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<KeyUpAt>k__BackingField, other.<KeyUpAt>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<SentAt>k__BackingField, other.<SentAt>k__BackingField) && EqualityComparer<int?>.Default.Equals(this.<FirstTokenMilliseconds>k__BackingField, other.<FirstTokenMilliseconds>k__BackingField) && EqualityComparer<DateTimeOffset?>.Default.Equals(this.<FirstHeardAt>k__BackingField, other.<FirstHeardAt>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Rounds>k__BackingField, other.<Rounds>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Input>k__BackingField, other.<Input>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Cached>k__BackingField, other.<Cached>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Wrote>k__BackingField, other.<Wrote>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Output>k__BackingField, other.<Output>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Reasoning>k__BackingField, other.<Reasoning>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Cost>k__BackingField, other.<Cost>k__BackingField) && EqualityComparer<double?>.Default.Equals(this.<IdleMinutesBefore>k__BackingField, other.<IdleMinutesBefore>k__BackingField));
      }

      // Token: 0x060039A3 RID: 14755 RVA: 0x00457048 File Offset: 0x00457048
      [CompilerGenerated]
      private Turn(OneBrainStats.Turn original)
      {
        this.Kind = original.<Kind>k__BackingField;
        this.Model = original.<Model>k__BackingField;
        this.KeyUpAt = original.<KeyUpAt>k__BackingField;
        this.SentAt = original.<SentAt>k__BackingField;
        this.FirstTokenMilliseconds = original.<FirstTokenMilliseconds>k__BackingField;
        this.FirstHeardAt = original.<FirstHeardAt>k__BackingField;
        this.Rounds = original.<Rounds>k__BackingField;
        this.Input = original.<Input>k__BackingField;
        this.Cached = original.<Cached>k__BackingField;
        this.Wrote = original.<Wrote>k__BackingField;
        this.Output = original.<Output>k__BackingField;
        this.Reasoning = original.<Reasoning>k__BackingField;
        this.Cost = original.<Cost>k__BackingField;
        this.IdleMinutesBefore = original.<IdleMinutesBefore>k__BackingField;
      }

      // Token: 0x060039A4 RID: 14756 RVA: 0x00457104 File Offset: 0x00457104
      public Turn()
      {
        this.Kind = "";
        this.Model = "";
        base..ctor();
      }
    }

    // Token: 0x0200065D RID: 1629
    [Nullable(0)]
    public sealed class Event : IEquatable<OneBrainStats.Event>
    {
      // Token: 0x060039A5 RID: 14757 RVA: 0x00457124 File Offset: 0x00457124
      public Event(DateTimeOffset At, string Text)
      {
        this.At = At;
        this.Text = Text;
        base..ctor();
      }

      // Token: 0x170009F2 RID: 2546
      // (get) Token: 0x060039A6 RID: 14758 RVA: 0x0045713C File Offset: 0x0045713C
      [CompilerGenerated]
      private Type EqualityContract
      {
        [CompilerGenerated]
        get
        {
          return typeof(OneBrainStats.Event);
        }
      }

      // Token: 0x170009F3 RID: 2547
      // (get) Token: 0x060039A7 RID: 14759 RVA: 0x00457148 File Offset: 0x00457148
      // (set) Token: 0x060039A8 RID: 14760 RVA: 0x00457150 File Offset: 0x00457150
      public DateTimeOffset At { get; set; }

      // Token: 0x170009F4 RID: 2548
      // (get) Token: 0x060039A9 RID: 14761 RVA: 0x0045715C File Offset: 0x0045715C
      // (set) Token: 0x060039AA RID: 14762 RVA: 0x00457164 File Offset: 0x00457164
      public string Text { get; set; }

      // Token: 0x060039AB RID: 14763 RVA: 0x00457170 File Offset: 0x00457170
      [CompilerGenerated]
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Event");
        stringBuilder.Append(" { ");
        if (this.PrintMembers(stringBuilder))
        {
          stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
      }

      // Token: 0x060039AC RID: 14764 RVA: 0x004571BC File Offset: 0x004571BC
      [CompilerGenerated]
      private bool PrintMembers(StringBuilder builder)
      {
        RuntimeHelpers.EnsureSufficientExecutionStack();
        builder.Append("At = ");
        builder.Append(this.At.ToString());
        builder.Append(", Text = ");
        builder.Append(this.Text);
        return true;
      }

      // Token: 0x060039AD RID: 14765 RVA: 0x00457210 File Offset: 0x00457210
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator !=(OneBrainStats.Event left, OneBrainStats.Event right)
      {
        return !(left == right);
      }

      // Token: 0x060039AE RID: 14766 RVA: 0x0045721C File Offset: 0x0045721C
      [NullableContext(2)]
      [CompilerGenerated]
      public static bool operator ==(OneBrainStats.Event left, OneBrainStats.Event right)
      {
        return left == right || (left != null && left.Equals(right));
      }

      // Token: 0x060039AF RID: 14767 RVA: 0x00457230 File Offset: 0x00457230
      [CompilerGenerated]
      public override int GetHashCode()
      {
        return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(this.<At>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Text>k__BackingField);
      }

      // Token: 0x060039B0 RID: 14768 RVA: 0x00457270 File Offset: 0x00457270
      [NullableContext(2)]
      [CompilerGenerated]
      public override bool Equals(object obj)
      {
        return this.Equals(obj as OneBrainStats.Event);
      }

      // Token: 0x060039B1 RID: 14769 RVA: 0x00457280 File Offset: 0x00457280
      [NullableContext(2)]
      [CompilerGenerated]
      public bool Equals(OneBrainStats.Event other)
      {
        return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<DateTimeOffset>.Default.Equals(this.<At>k__BackingField, other.<At>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Text>k__BackingField, other.<Text>k__BackingField));
      }

      // Token: 0x060039B3 RID: 14771 RVA: 0x004572E4 File Offset: 0x004572E4
      [CompilerGenerated]
      private Event(OneBrainStats.Event original)
      {
        this.At = original.<At>k__BackingField;
        this.Text = original.<Text>k__BackingField;
      }

      // Token: 0x060039B4 RID: 14772 RVA: 0x00457304 File Offset: 0x00457304
      [CompilerGenerated]
      public void Deconstruct(out DateTimeOffset At, out string Text)
      {
        At = this.At;
        Text = this.Text;
      }
    }
  }
}
