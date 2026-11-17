using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000AA RID: 170
  [NullableContext(1)]
  public interface IAnalyticsSink
  {
    // Token: 0x17000243 RID: 579
    // (get) Token: 0x06000A6F RID: 2671
    string Name { get; }

    // Token: 0x06000A70 RID: 2672
    void Enqueue(AnalyticsEvent analyticsEvent);

    // Token: 0x06000A71 RID: 2673
    Task FlushAsync();
  }
}
