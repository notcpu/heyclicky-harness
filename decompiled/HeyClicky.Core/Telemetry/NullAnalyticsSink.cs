using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AB RID: 171
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class NullAnalyticsSink : IAnalyticsSink
  {
    // Token: 0x17000244 RID: 580
    // (get) Token: 0x06000A72 RID: 2674 RVA: 0x00374AC4 File Offset: 0x00374AC4
    public string Name
    {
      get
      {
        return "off";
      }
    }

    // Token: 0x06000A73 RID: 2675 RVA: 0x00374ACC File Offset: 0x00374ACC
    public void Enqueue(AnalyticsEvent analyticsEvent)
    {
    }

    // Token: 0x06000A74 RID: 2676 RVA: 0x00374AD0 File Offset: 0x00374AD0
    public Task FlushAsync()
    {
      return Task.CompletedTask;
    }

    // Token: 0x040004AF RID: 1199
    public static readonly NullAnalyticsSink Instance = new NullAnalyticsSink();
  }
}
