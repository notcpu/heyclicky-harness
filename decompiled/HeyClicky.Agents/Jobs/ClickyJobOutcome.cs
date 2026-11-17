using System;
using System.Text.Json.Serialization;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000038 RID: 56
  [JsonConverter(typeof(JsonStringEnumConverter<ClickyJobOutcome>))]
  public enum ClickyJobOutcome
  {
    // Token: 0x040001F5 RID: 501
    [JsonStringEnumMemberName("working")]
    Working,
    // Token: 0x040001F6 RID: 502
    [JsonStringEnumMemberName("done")]
    Done,
    // Token: 0x040001F7 RID: 503
    [JsonStringEnumMemberName("failed")]
    Failed,
    // Token: 0x040001F8 RID: 504
    [JsonStringEnumMemberName("stopped")]
    Stopped
  }
}
