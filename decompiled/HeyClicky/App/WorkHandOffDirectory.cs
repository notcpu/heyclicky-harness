using System;
using System.Linq;
using System.Runtime.CompilerServices;
using HeyClicky.Agents;
using HeyClicky.Agents.Clickys;
using HeyClicky.Agents.Jobs;
using HeyClicky.Core.Notch;

namespace HeyClicky.App
{
  // Token: 0x0200007C RID: 124
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class WorkHandOffDirectory : IChatHandOffDirectory
  {
    // Token: 0x060004A5 RID: 1189 RVA: 0x00529FB4 File Offset: 0x00529FB4
    public WorkHandOffDirectory(AgentService agents)
    {
    }

    // Token: 0x060004A6 RID: 1190 RVA: 0x00529FC4 File Offset: 0x00529FC4
    [return: TupleElementNames(new string[] { "Id", "Title" })]
    [return: Nullable(new byte[] { 0, 1, 1 })]
    public ValueTuple<string, string>? JobForThread(string threadId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.JobForThread(threadId);
      if (clickyJob == null)
      {
        return null;
      }
      return new ValueTuple<string, string>?(new ValueTuple<string, string>(clickyJob.Id, clickyJob.Title));
    }

    // Token: 0x060004A7 RID: 1191 RVA: 0x0052A008 File Offset: 0x0052A008
    [return: Nullable(2)]
    public string JobTitle(string jobId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
      if (clickyJob == null)
      {
        return null;
      }
      return clickyJob.Title;
    }

    // Token: 0x060004A8 RID: 1192 RVA: 0x0052A028 File Offset: 0x0052A028
    [return: Nullable(2)]
    public string ClickySlugForThread(string threadId)
    {
      ClickyInfo clickyInfo = this.<agents>P.Clickys.FirstOrDefault<ClickyInfo>((ClickyInfo c) => c.ThreadId == threadId);
      if (clickyInfo == null)
      {
        return null;
      }
      return clickyInfo.Slug;
    }

    // Token: 0x060004A9 RID: 1193 RVA: 0x0052A06C File Offset: 0x0052A06C
    [return: Nullable(2)]
    public string ClickyName(string slug)
    {
      ClickyInfo clickyInfo = this.<agents>P.FindClicky(slug);
      if (clickyInfo == null)
      {
        return null;
      }
      return clickyInfo.Name;
    }

    // Token: 0x040000E8 RID: 232
    [CompilerGenerated]
    private AgentService <agents>P = agents;
  }
}
