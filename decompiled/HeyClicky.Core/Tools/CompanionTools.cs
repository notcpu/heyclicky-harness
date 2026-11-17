using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.Core.Brain;

namespace HeyClicky.Core.Tools
{
  // Token: 0x0200009F RID: 159
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class CompanionTools
  {
    // Token: 0x06000A05 RID: 2565 RVA: 0x00372AA8 File Offset: 0x00372AA8
    public CompanionTools(LocalRootTools local, WorkerTools worker, AgentTools agents)
    {
      this.local = local;
      this.worker = worker;
      this.agents = agents;
    }

    // Token: 0x17000233 RID: 563
    // (get) Token: 0x06000A06 RID: 2566 RVA: 0x00372AC8 File Offset: 0x00372AC8
    public AgentTools Agents
    {
      get
      {
        return this.agents;
      }
    }

    // Token: 0x06000A07 RID: 2567 RVA: 0x00372AD0 File Offset: 0x00372AD0
    public async Task<string> HandleAsync(BrainToolCall call, CompanionToolContext context, CancellationToken ct)
    {
      IAgentToolHost host = this.agents.Host;
      BrainToolCall brainToolCall = call.<Clone>$();
      brainToolCall.Arguments = OneBrainJobIds.ArgumentsWithThreadIds(call.Arguments, new Func<string, ValueTuple<bool, string>>(host.ThreadIdForJobId));
      BrainToolCall brainToolCall2 = brainToolCall;
      return OneBrainJobIds.OutputWithJobIds(await this.HandleByThreadAsync(brainToolCall2, context, ct).ConfigureAwait(false), new Func<string, string>(host.JobIdForThread));
    }

    // Token: 0x06000A08 RID: 2568 RVA: 0x00372B2C File Offset: 0x00372B2C
    private Task<string> HandleByThreadAsync(BrainToolCall call, CompanionToolContext context, CancellationToken ct)
    {
      ToolArgs toolArgs = new ToolArgs(call.Arguments);
      if (LocalRootTools.Names.Contains(call.Name))
      {
        return this.local.RunAsync(call.Name, toolArgs, ct);
      }
      if (WorkerTools.Names.Contains(call.Name))
      {
        return this.worker.RunAsync(call.Name, toolArgs, ct);
      }
      if (AgentTools.Names.Contains(call.Name))
      {
        return this.agents.RunAsync(call.Name, toolArgs, context, ct);
      }
      Log.Warn("Tools", "unknown tool " + call.Name);
      JsonObject jsonObject = new JsonObject(null);
      jsonObject["error"] = "not_implemented";
      jsonObject["tool"] = call.Name;
      return Task.FromResult<string>(ToolResults.Json(jsonObject));
    }

    // Token: 0x04000489 RID: 1161
    public static readonly IReadOnlySet<string> ClientToolNames = new HashSet<string>(new string[] { "open_url", "copy_to_clipboard", "insert_text" }.Concat<string>(LocalRootTools.Names).Concat<string>(WorkerTools.Names).Concat<string>(AgentTools.Names));

    // Token: 0x0400048A RID: 1162
    private readonly LocalRootTools local;

    // Token: 0x0400048B RID: 1163
    private readonly WorkerTools worker;

    // Token: 0x0400048C RID: 1164
    private readonly AgentTools agents;
  }
}
