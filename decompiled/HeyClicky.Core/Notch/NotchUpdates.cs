using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using HeyClicky.Core.Telemetry;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200011C RID: 284
  [NullableContext(2)]
  [Nullable(0)]
  public sealed class NotchUpdates
  {
    // Token: 0x14000030 RID: 48
    // (add) Token: 0x0600129D RID: 4765 RVA: 0x003A24A8 File Offset: 0x003A24A8
    // (remove) Token: 0x0600129E RID: 4766 RVA: 0x003A24E0 File Offset: 0x003A24E0
    public event Action CurrentChanged;

    // Token: 0x14000031 RID: 49
    // (add) Token: 0x0600129F RID: 4767 RVA: 0x003A2518 File Offset: 0x003A2518
    // (remove) Token: 0x060012A0 RID: 4768 RVA: 0x003A2550 File Offset: 0x003A2550
    public event Action RemainingChanged;

    // Token: 0x060012A1 RID: 4769 RVA: 0x003A2588 File Offset: 0x003A2588
    public NotchUpdates(Func<DateTimeOffset> clock = null, bool autoTick = true)
    {
      Func<DateTimeOffset> func = clock;
      if (clock == null && (func = NotchUpdates.<>c.<>9__18_0) == null)
      {
        func = (NotchUpdates.<>c.<>9__18_0 = () => DateTimeOffset.UtcNow);
      }
      this.clock = func;
      this.lastTick = this.clock();
      if (autoTick)
      {
        this.timer = new Timer(delegate(object _)
        {
          this.Tick();
        }, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
      }
    }

    // Token: 0x17000455 RID: 1109
    // (get) Token: 0x060012A2 RID: 4770 RVA: 0x003A261C File Offset: 0x003A261C
    public NotchUpdate Current
    {
      get
      {
        object obj = this.gate;
        NotchUpdate notchUpdate;
        lock (obj)
        {
          notchUpdate = this.current;
        }
        return notchUpdate;
      }
    }

    // Token: 0x17000456 RID: 1110
    // (get) Token: 0x060012A3 RID: 4771 RVA: 0x003A2660 File Offset: 0x003A2660
    public TimeSpan? RemainingTime
    {
      get
      {
        object obj = this.gate;
        TimeSpan? timeSpan;
        lock (obj)
        {
          timeSpan = this.remaining;
        }
        return timeSpan;
      }
    }

    // Token: 0x17000457 RID: 1111
    // (get) Token: 0x060012A4 RID: 4772 RVA: 0x003A26A4 File Offset: 0x003A26A4
    public int WaitingCount
    {
      get
      {
        object obj = this.gate;
        int count;
        lock (obj)
        {
          count = this.waiting.Count;
        }
        return count;
      }
    }

    // Token: 0x060012A5 RID: 4773 RVA: 0x003A26EC File Offset: 0x003A26EC
    [NullableContext(1)]
    public void Present(string railKey, NotchUpdateKind kind)
    {
      object obj = this.gate;
      bool flag2;
      lock (obj)
      {
        string text = "NotchUpdates";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 4);
        defaultInterpolatedStringHandler.AppendLiteral("present ");
        defaultInterpolatedStringHandler.AppendFormatted(railKey);
        defaultInterpolatedStringHandler.AppendLiteral(" kind=");
        defaultInterpolatedStringHandler.AppendFormatted(kind.RawValue());
        defaultInterpolatedStringHandler.AppendLiteral(" current=");
        NotchUpdate notchUpdate = this.current;
        defaultInterpolatedStringHandler.AppendFormatted(((notchUpdate != null) ? notchUpdate.RailKey : null) ?? "nil");
        defaultInterpolatedStringHandler.AppendLiteral(" waiting=");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.waiting.Count);
        Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        NotchUpdate notchUpdate2 = new NotchUpdate(railKey, kind);
        this.waiting.RemoveAll((NotchUpdate w) => w.RailKey == railKey);
        NotchUpdate notchUpdate3 = this.current;
        if (((notchUpdate3 != null) ? notchUpdate3.RailKey : null) == railKey || this.current == null)
        {
          this.SetCurrent(notchUpdate2);
          flag2 = true;
        }
        else
        {
          if (kind == NotchUpdateKind.Done)
          {
            this.waiting.Add(notchUpdate2);
          }
          else
          {
            int num = this.waiting.FindIndex((NotchUpdate w) => w.Kind == NotchUpdateKind.Done);
            this.waiting.Insert((num < 0) ? this.waiting.Count : num, notchUpdate2);
          }
          flag2 = false;
        }
      }
      if (flag2)
      {
        this.Raise();
      }
    }

    // Token: 0x060012A6 RID: 4774 RVA: 0x003A289C File Offset: 0x003A289C
    public void DismissCurrent()
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.current == null)
        {
          return;
        }
        Log.Info("NotchUpdates", "dismissCurrent " + this.current.RailKey);
        this.DismissCurrentCore();
      }
      this.Raise();
    }

    // Token: 0x060012A7 RID: 4775 RVA: 0x003A290C File Offset: 0x003A290C
    private void DismissCurrentCore()
    {
      if (this.waiting.Count > 0)
      {
        this.nextCardAt = new DateTimeOffset?(this.clock() + NotchUpdates.BeatBetweenCards);
      }
      this.SetCurrent(null);
    }

    // Token: 0x060012A8 RID: 4776 RVA: 0x003A2944 File Offset: 0x003A2944
    [NullableContext(1)]
    public void Dismiss(string railKey)
    {
      bool flag = false;
      object obj = this.gate;
      lock (obj)
      {
        this.waiting.RemoveAll((NotchUpdate w) => w.RailKey == railKey);
        NotchUpdate notchUpdate = this.current;
        if (((notchUpdate != null) ? notchUpdate.RailKey : null) == railKey)
        {
          this.DismissCurrentCore();
          flag = true;
        }
      }
      if (flag)
      {
        this.Raise();
      }
    }

    // Token: 0x060012A9 RID: 4777 RVA: 0x003A29D8 File Offset: 0x003A29D8
    public void Reset()
    {
      object obj = this.gate;
      lock (obj)
      {
        Log.Info("NotchUpdates", "reset");
        this.waiting.Clear();
        this.nextCardAt = null;
        this.SetCurrent(null);
      }
      this.Raise();
    }

    // Token: 0x060012AA RID: 4778 RVA: 0x003A2A48 File Offset: 0x003A2A48
    public void NoteShowing(bool showing, bool hovered)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.isShowing = showing;
        this.isHovered = hovered;
        this.lastTick = this.clock();
      }
    }

    // Token: 0x060012AB RID: 4779 RVA: 0x003A2AA4 File Offset: 0x003A2AA4
    private void SetCurrent(NotchUpdate update)
    {
      if (((update == null) ? null : NotchUpdates.AnalyticsKey(update)) != this.shownTrackedKey)
      {
        this.shownTrackedKey = null;
        this.shownAt = null;
        this.shownSubject = null;
      }
      this.current = update;
      this.lastTick = this.clock();
      this.remaining = ((update != null && update.ClosesOnItsOwn) ? new TimeSpan?(NotchUpdates.AutoDismiss) : null);
      this.UpdateTimer();
    }

    // Token: 0x060012AC RID: 4780 RVA: 0x003A2B28 File Offset: 0x003A2B28
    private void UpdateTimer()
    {
      if (this.timer == null)
      {
        return;
      }
      TimeSpan? timeSpan = this.remaining;
      bool flag;
      if (timeSpan == null)
      {
        DateTimeOffset? dateTimeOffset = this.nextCardAt;
        flag = dateTimeOffset != null;
      }
      else
      {
        flag = true;
      }
      bool flag2 = flag;
      if (flag2 == this.ticking)
      {
        return;
      }
      this.ticking = flag2;
      if (flag2)
      {
        this.timer.Change(TimeSpan.FromMilliseconds(100L, 0L), TimeSpan.FromMilliseconds(100L, 0L));
        return;
      }
      this.timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }

    // Token: 0x060012AD RID: 4781 RVA: 0x003A2BB4 File Offset: 0x003A2BB4
    public void Tick()
    {
      bool flag = false;
      bool flag2 = false;
      object obj = this.gate;
      lock (obj)
      {
        DateTimeOffset dateTimeOffset = this.clock();
        TimeSpan timeSpan = dateTimeOffset - this.lastTick;
        this.lastTick = dateTimeOffset;
        if (timeSpan < TimeSpan.Zero)
        {
          timeSpan = TimeSpan.Zero;
        }
        if (this.current == null)
        {
          DateTimeOffset? dateTimeOffset2 = this.nextCardAt;
          if (dateTimeOffset2 != null)
          {
            DateTimeOffset valueOrDefault = dateTimeOffset2.GetValueOrDefault();
            if (dateTimeOffset >= valueOrDefault)
            {
              this.nextCardAt = null;
              if (this.waiting.Count > 0)
              {
                NotchUpdate notchUpdate = this.waiting[0];
                this.waiting.RemoveAt(0);
                this.SetCurrent(notchUpdate);
                flag = true;
              }
              this.UpdateTimer();
              goto IL_013D;
            }
          }
        }
        if (this.current != null)
        {
          TimeSpan? timeSpan2 = this.remaining;
          if (timeSpan2 != null)
          {
            TimeSpan valueOrDefault2 = timeSpan2.GetValueOrDefault();
            if (this.isShowing && !this.isHovered)
            {
              TimeSpan timeSpan3 = valueOrDefault2 - timeSpan;
              if (timeSpan3 <= TimeSpan.Zero)
              {
                this.TrackCore("auto_closed", null);
                this.DismissCurrentCore();
                flag = true;
              }
              else
              {
                this.remaining = new TimeSpan?(timeSpan3);
                flag2 = true;
              }
            }
          }
        }
      }
      IL_013D:
      this.FlushAnalytics();
      if (flag)
      {
        this.Raise();
        return;
      }
      if (flag2)
      {
        Action remainingChanged = this.RemainingChanged;
        if (remainingChanged == null)
        {
          return;
        }
        remainingChanged();
      }
    }

    // Token: 0x17000458 RID: 1112
    // (get) Token: 0x060012AE RID: 4782 RVA: 0x003A2D40 File Offset: 0x003A2D40
    // (set) Token: 0x060012AF RID: 4783 RVA: 0x003A2D48 File Offset: 0x003A2D48
    [Nullable(new byte[] { 2, 1, 2 })]
    public Func<string, string> JobThreadId
    {
      [return: Nullable(new byte[] { 2, 1, 2 })]
      get;
      [param: Nullable(new byte[] { 2, 1, 2 })]
      set;
    }

    // Token: 0x060012B0 RID: 4784 RVA: 0x003A2D54 File Offset: 0x003A2D54
    [NullableContext(1)]
    [return: Nullable(2)]
    public static string JobIdFromRailKey(string railKey)
    {
      if (!railKey.StartsWith("job:", StringComparison.Ordinal))
      {
        return null;
      }
      return railKey.Substring(4, railKey.Length - 4);
    }

    // Token: 0x060012B1 RID: 4785 RVA: 0x003A2D84 File Offset: 0x003A2D84
    [NullableContext(1)]
    private static string AnalyticsKey(NotchUpdate update)
    {
      return update.RailKey + "|" + update.Kind.AnalyticsName();
    }

    // Token: 0x060012B2 RID: 4786 RVA: 0x003A2DA4 File Offset: 0x003A2DA4
    [NullableContext(1)]
    public void TrackShownIfNeeded(string subject, JsonObject properties)
    {
      object obj = this.gate;
      lock (obj)
      {
        if (this.current == null)
        {
          return;
        }
        string text = NotchUpdates.AnalyticsKey(this.current);
        if (text == this.shownTrackedKey)
        {
          return;
        }
        this.shownTrackedKey = text;
        this.shownAt = new DateTimeOffset?(this.clock());
        this.shownSubject = subject;
        JsonObject jsonObject = properties.DeepClone().AsObject();
        jsonObject["waiting_count"] = this.waiting.Count;
        this.TrackCore("shown", jsonObject);
      }
      this.FlushAnalytics();
    }

    // Token: 0x060012B3 RID: 4787 RVA: 0x003A2E64 File Offset: 0x003A2E64
    [NullableContext(1)]
    public void Track(string action, [Nullable(2)] JsonObject properties = null)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.TrackCore(action, properties);
      }
      this.FlushAnalytics();
    }

    // Token: 0x060012B4 RID: 4788 RVA: 0x003A2EAC File Offset: 0x003A2EAC
    [NullableContext(1)]
    private void TrackCore(string action, [Nullable(2)] JsonObject extra)
    {
      if (this.current == null || (action != "shown" && this.shownTrackedKey == null))
      {
        return;
      }
      string text = NotchUpdates.JobIdFromRailKey(this.current.RailKey);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["action"] = action;
      jsonObject["kind"] = this.current.Kind.AnalyticsName();
      jsonObject["subject"] = this.shownSubject ?? ((text == null) ? "clicky" : "job");
      JsonObject jsonObject2 = jsonObject;
      if (action != "shown")
      {
        DateTimeOffset? dateTimeOffset = this.shownAt;
        if (dateTimeOffset != null)
        {
          DateTimeOffset valueOrDefault = dateTimeOffset.GetValueOrDefault();
          jsonObject2["visible_seconds"] = NotchChatAnalytics.Tenths(Math.Max(0.0, (this.clock() - valueOrDefault).TotalSeconds));
        }
      }
      if (extra != null)
      {
        foreach (KeyValuePair<string, JsonNode> keyValuePair in extra)
        {
          string text2;
          JsonNode jsonNode;
          keyValuePair.Deconstruct(out text2, out jsonNode);
          string text3 = text2;
          JsonNode jsonNode2 = jsonNode;
          jsonObject2[text3] = ((jsonNode2 != null) ? jsonNode2.DeepClone() : null);
        }
      }
      this.pendingAnalytics.Add(new ValueTuple<string, string, JsonObject>(this.current.RailKey, text, jsonObject2));
    }

    // Token: 0x060012B5 RID: 4789 RVA: 0x003A3038 File Offset: 0x003A3038
    private void FlushAnalytics()
    {
      object obj = this.gate;
      ValueTuple<string, string, JsonObject>[] array;
      lock (obj)
      {
        if (this.pendingAnalytics.Count == 0)
        {
          return;
        }
        array = this.pendingAnalytics.ToArray();
        this.pendingAnalytics.Clear();
      }
      foreach (ValueTuple<string, string, JsonObject> valueTuple in array)
      {
        string item = valueTuple.Item1;
        string item2 = valueTuple.Item2;
        JsonObject item3 = valueTuple.Item3;
        string text = "notch_update_card_action";
        string text2 = ((item2 == null) ? item : null);
        string text3;
        if (item2 != null)
        {
          Func<string, string> jobThreadId = this.JobThreadId;
          text3 = ((jobThreadId != null) ? jobThreadId(item2) : null);
        }
        else
        {
          text3 = null;
        }
        ClickyAnalytics.TrackProductEvent(text, text2, text3, item3);
      }
    }

    // Token: 0x060012B6 RID: 4790 RVA: 0x003A3100 File Offset: 0x003A3100
    private void Raise()
    {
      try
      {
        Action currentChanged = this.CurrentChanged;
        if (currentChanged != null)
        {
          currentChanged();
        }
      }
      catch (Exception ex)
      {
        Log.Warn("NotchUpdates", "CurrentChanged handler threw: " + ex.Message);
      }
      Action remainingChanged = this.RemainingChanged;
      if (remainingChanged == null)
      {
        return;
      }
      remainingChanged();
    }

    // Token: 0x04000856 RID: 2134
    public static readonly TimeSpan AutoDismiss = TimeSpan.FromSeconds(12L);

    // Token: 0x04000857 RID: 2135
    public static readonly TimeSpan BeatBetweenCards = TimeSpan.FromSeconds(0.45);

    // Token: 0x04000858 RID: 2136
    [Nullable(1)]
    private readonly object gate = new object();

    // Token: 0x04000859 RID: 2137
    [Nullable(1)]
    private readonly Func<DateTimeOffset> clock;

    // Token: 0x0400085A RID: 2138
    private readonly Timer timer;

    // Token: 0x0400085B RID: 2139
    [Nullable(1)]
    private readonly List<NotchUpdate> waiting = new List<NotchUpdate>();

    // Token: 0x0400085C RID: 2140
    private NotchUpdate current;

    // Token: 0x0400085D RID: 2141
    private TimeSpan? remaining;

    // Token: 0x0400085E RID: 2142
    private DateTimeOffset lastTick;

    // Token: 0x0400085F RID: 2143
    private DateTimeOffset? nextCardAt;

    // Token: 0x04000860 RID: 2144
    private bool isShowing;

    // Token: 0x04000861 RID: 2145
    private bool isHovered;

    // Token: 0x04000864 RID: 2148
    private bool ticking;

    // Token: 0x04000865 RID: 2149
    private string shownTrackedKey;

    // Token: 0x04000866 RID: 2150
    private DateTimeOffset? shownAt;

    // Token: 0x04000867 RID: 2151
    private string shownSubject;

    // Token: 0x04000868 RID: 2152
    [TupleElementNames(new string[] { "RailKey", "JobId", "Properties" })]
    [Nullable(new byte[] { 1, 0, 1, 2, 1 })]
    private readonly List<ValueTuple<string, string, JsonObject>> pendingAnalytics = new List<ValueTuple<string, string, JsonObject>>();
  }
}
