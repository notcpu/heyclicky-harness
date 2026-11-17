using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AC RID: 172
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LogAnalyticsSink : IAnalyticsSink
  {
    // Token: 0x17000245 RID: 581
    // (get) Token: 0x06000A77 RID: 2679 RVA: 0x00374AEC File Offset: 0x00374AEC
    public string Name
    {
      get
      {
        return "log";
      }
    }

    // Token: 0x17000246 RID: 582
    // (get) Token: 0x06000A78 RID: 2680 RVA: 0x00374AF4 File Offset: 0x00374AF4
    public IReadOnlyList<AnalyticsEvent> Recent
    {
      get
      {
        object obj = this.gate;
        IReadOnlyList<AnalyticsEvent> readOnlyList;
        lock (obj)
        {
          readOnlyList = this.recent.ToArray();
        }
        return readOnlyList;
      }
    }

    // Token: 0x06000A79 RID: 2681 RVA: 0x00374B3C File Offset: 0x00374B3C
    public void Enqueue(AnalyticsEvent analyticsEvent)
    {
      object obj = this.gate;
      lock (obj)
      {
        this.recent.Add(analyticsEvent);
        if (this.recent.Count > 500)
        {
          this.recent.RemoveRange(0, this.recent.Count - 500);
        }
      }
      Log.Info("analytics", analyticsEvent.ToPostHogJson().ToJsonString(null));
    }

    // Token: 0x06000A7A RID: 2682 RVA: 0x00374BC8 File Offset: 0x00374BC8
    public Task FlushAsync()
    {
      return Task.CompletedTask;
    }

    // Token: 0x040004B0 RID: 1200
    private readonly object gate = new object();

    // Token: 0x040004B1 RID: 1201
    private readonly List<AnalyticsEvent> recent = new List<AnalyticsEvent>();
  }
}
