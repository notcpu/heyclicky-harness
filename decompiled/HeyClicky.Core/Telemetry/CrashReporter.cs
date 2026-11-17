using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace HeyClicky.Core.Telemetry
{
  // Token: 0x020000B2 RID: 178
  [NullableContext(1)]
  [Nullable(0)]
  public static class CrashReporter
  {
    // Token: 0x17000251 RID: 593
    // (get) Token: 0x06000B5E RID: 2910 RVA: 0x0037AC94 File Offset: 0x0037AC94
    // (set) Token: 0x06000B5F RID: 2911 RVA: 0x0037AC9C File Offset: 0x0037AC9C
    public static ICrashReporter Current { get; private set; } = new CrashReporter.Off();

    // Token: 0x06000B60 RID: 2912 RVA: 0x0037ACA4 File Offset: 0x0037ACA4
    public static void Install(ICrashReporter reporter)
    {
      CrashReporter.Current = reporter;
    }

    // Token: 0x020003BE RID: 958
    [Nullable(0)]
    private sealed class Off : ICrashReporter
    {
      // Token: 0x1700081B RID: 2075
      // (get) Token: 0x06002A21 RID: 10785 RVA: 0x00425810 File Offset: 0x00425810
      public bool IsEnabled
      {
        get
        {
          return false;
        }
      }

      // Token: 0x06002A22 RID: 10786 RVA: 0x00425814 File Offset: 0x00425814
      [NullableContext(2)]
      public void AddBreadcrumb([Nullable(1)] string category, string message, CrashReportLevel level, JsonObject data)
      {
      }

      // Token: 0x06002A23 RID: 10787 RVA: 0x00425818 File Offset: 0x00425818
      public void CaptureMessage(string message, CrashReportLevel level, IReadOnlyList<string> fingerprint, IReadOnlyDictionary<string, string> tags, string contextName, JsonObject context, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<string> attachmentPaths = null)
      {
      }

      // Token: 0x06002A24 RID: 10788 RVA: 0x0042581C File Offset: 0x0042581C
      public void CaptureException(Exception exception, string mechanism, bool handled)
      {
      }

      // Token: 0x06002A25 RID: 10789 RVA: 0x00425820 File Offset: 0x00425820
      [NullableContext(2)]
      public void SetUser(string id, string email, string name)
      {
      }

      // Token: 0x06002A26 RID: 10790 RVA: 0x00425824 File Offset: 0x00425824
      public void ClearBreadcrumbs()
      {
      }

      // Token: 0x06002A27 RID: 10791 RVA: 0x00425828 File Offset: 0x00425828
      public Task FlushAsync(TimeSpan timeout)
      {
        return Task.CompletedTask;
      }
    }
  }
}
