using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000B1 RID: 177
  [NullableContext(1)]
  public interface ICrashReporter
  {
    // Token: 0x17000250 RID: 592
    // (get) Token: 0x06000B57 RID: 2903
    bool IsEnabled { get; }

    // Token: 0x06000B58 RID: 2904
    [NullableContext(2)]
    void AddBreadcrumb([Nullable(1)] string category, string message, CrashReportLevel level, JsonObject data);

    // Token: 0x06000B59 RID: 2905
    void CaptureMessage(string message, CrashReportLevel level, IReadOnlyList<string> fingerprint, IReadOnlyDictionary<string, string> tags, string contextName, JsonObject context, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachmentPaths = null);

    // Token: 0x06000B5A RID: 2906
    void CaptureException(Exception exception, string mechanism, bool handled);

    // Token: 0x06000B5B RID: 2907
    [NullableContext(2)]
    void SetUser(string id, string email, string name);

    // Token: 0x06000B5C RID: 2908
    void ClearBreadcrumbs();

    // Token: 0x06000B5D RID: 2909
    Task FlushAsync(TimeSpan timeout);
  }
}
