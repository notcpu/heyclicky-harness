using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.Agents;
using HeyClicky.Agents.Jobs;
using HeyClicky.App.Browser;

namespace HeyClicky.App.Dock
{
  // Token: 0x020002FA RID: 762
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class ChromeSetupJobs : IChromeSetupJobs
  {
    // Token: 0x0600213D RID: 8509 RVA: 0x0061BBA4 File Offset: 0x0061BBA4
    public ChromeSetupJobs(AgentService agents, Action<string> presentJobNeedsYou)
    {
    }

    // Token: 0x0600213E RID: 8510 RVA: 0x0061BBBC File Offset: 0x0061BBBC
    [return: Nullable(2)]
    public string JobIdForThread(string threadId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.JobForThread(threadId);
      if (clickyJob == null)
      {
        return null;
      }
      return clickyJob.Id;
    }

    // Token: 0x0600213F RID: 8511 RVA: 0x0061BBDC File Offset: 0x0061BBDC
    public bool IsHeldForChrome(string jobId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
      return clickyJob != null && clickyJob.IsHeldForChrome;
    }

    // Token: 0x06002140 RID: 8512 RVA: 0x0061BBFC File Offset: 0x0061BBFC
    public IReadOnlyList<string> HeldJobIdsNewestFirst()
    {
      return (from j in this.<agents>P.Jobs.Jobs
        where j.IsHeldForChrome && !j.IsDismissed
        orderby j.CreatedAt descending
        select j.Id).ToList<string>();
    }

    // Token: 0x06002141 RID: 8513 RVA: 0x0061BC8C File Offset: 0x0061BC8C
    public void SetWaitsForChrome(string jobId, bool waits)
    {
      this.<agents>P.Jobs.SetWaitsForChrome(jobId, waits);
    }

    // Token: 0x06002142 RID: 8514 RVA: 0x0061BCA0 File Offset: 0x0061BCA0
    public void SetSkipsChrome(string jobId)
    {
      this.<agents>P.Jobs.SetSkipsChrome(jobId);
    }

    // Token: 0x06002143 RID: 8515 RVA: 0x0061BCB4 File Offset: 0x0061BCB4
    public bool SkipsChrome(string jobId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
      return clickyJob != null && clickyJob.SkipsChrome.GetValueOrDefault();
    }

    // Token: 0x06002144 RID: 8516 RVA: 0x0061BCE8 File Offset: 0x0061BCE8
    public void PresentNeedsYou(string jobId)
    {
      ClickyJob clickyJob = this.<agents>P.Jobs.Job(jobId);
      if (clickyJob != null)
      {
        this.<presentJobNeedsYou>P(clickyJob.RailKey);
      }
    }

    // Token: 0x06002145 RID: 8517 RVA: 0x0061BD1C File Offset: 0x0061BD1C
    public void StartFromSavedPrompt(string jobId, bool withoutChrome)
    {
      ChromeSetupJobs.<>c__DisplayClass10_0 CS$<>8__locals1 = new ChromeSetupJobs.<>c__DisplayClass10_0();
      CS$<>8__locals1.<>4__this = this;
      CS$<>8__locals1.job = this.<agents>P.Jobs.Job(jobId);
      if (CS$<>8__locals1.job == null)
      {
        return;
      }
      CS$<>8__locals1.context = "HeyClicky started this as background work while their Chrome wasn't connected." + (withoutChrome ? " They chose not to connect Chrome for this job: don't ask about Chrome again. For a step on a website they're signed into, call mac_step and HeyClicky does it on their screen while they watch." : "");
      Task.Run(delegate
      {
        ChromeSetupJobs.<>c__DisplayClass10_0.<<StartFromSavedPrompt>b__0>d <<StartFromSavedPrompt>b__0>d;
        <<StartFromSavedPrompt>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<StartFromSavedPrompt>b__0>d.<>4__this = CS$<>8__locals1;
        <<StartFromSavedPrompt>b__0>d.<>1__state = -1;
        <<StartFromSavedPrompt>b__0>d.<>t__builder.Start<ChromeSetupJobs.<>c__DisplayClass10_0.<<StartFromSavedPrompt>b__0>d>(ref <<StartFromSavedPrompt>b__0>d);
        return <<StartFromSavedPrompt>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06002146 RID: 8518 RVA: 0x0061BD88 File Offset: 0x0061BD88
    [return: Nullable(2)]
    public string CreatePreviewJob(string title, string prompt)
    {
      return this.<agents>P.Jobs.CreateJob(title, prompt, this.<agents>P.ProjectsRoot, true).Id;
    }

    // Token: 0x06002147 RID: 8519 RVA: 0x0061BDB0 File Offset: 0x0061BDB0
    public void Dismiss(string jobId)
    {
      this.<agents>P.Jobs.Dismiss(jobId);
    }

    // Token: 0x04001002 RID: 4098
    [CompilerGenerated]
    private AgentService <agents>P = agents;

    // Token: 0x04001003 RID: 4099
    [CompilerGenerated]
    private Action<string> <presentJobNeedsYou>P = presentJobNeedsYou;
  }
}
