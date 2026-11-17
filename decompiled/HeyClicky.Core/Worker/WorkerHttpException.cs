using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Worker
{
  // Token: 0x02000068 RID: 104
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WorkerHttpException : Exception
  {
    // Token: 0x0600056F RID: 1391 RVA: 0x003583D8 File Offset: 0x003583D8
    public WorkerHttpException(int status, string body, string path)
    {
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 3);
      defaultInterpolatedStringHandler.AppendLiteral("Worker ");
      defaultInterpolatedStringHandler.AppendFormatted(path);
      defaultInterpolatedStringHandler.AppendLiteral(" failed (");
      defaultInterpolatedStringHandler.AppendFormatted<int>(status);
      defaultInterpolatedStringHandler.AppendLiteral("): ");
      defaultInterpolatedStringHandler.AppendFormatted((body.Length > 200) ? body.Substring(0, 200) : body);
      base..ctor(defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x1700013A RID: 314
    // (get) Token: 0x06000570 RID: 1392 RVA: 0x00358468 File Offset: 0x00358468
    public int Status { get; } = status;

    // Token: 0x1700013B RID: 315
    // (get) Token: 0x06000571 RID: 1393 RVA: 0x00358470 File Offset: 0x00358470
    public string Body { get; } = body;

    // Token: 0x1700013C RID: 316
    // (get) Token: 0x06000572 RID: 1394 RVA: 0x00358478 File Offset: 0x00358478
    public string Path { get; } = path;
  }
}
