using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Harness
{
  // Token: 0x02000065 RID: 101
  public sealed class AgentHarnessException : Exception
  {
    // Token: 0x06000820 RID: 2080 RVA: 0x000FF640 File Offset: 0x000FF640
    [NullableContext(1)]
    public AgentHarnessException(string message, bool notAvailable = false, bool isRunnerStartFailure = false)
      : base(message)
    {
    }

    // Token: 0x1700020A RID: 522
    // (get) Token: 0x06000821 RID: 2081 RVA: 0x000FF658 File Offset: 0x000FF658
    public bool IsNotAvailable { get; } = notAvailable;

    // Token: 0x1700020B RID: 523
    // (get) Token: 0x06000822 RID: 2082 RVA: 0x000FF660 File Offset: 0x000FF660
    public bool IsRunnerStartFailure { get; } = isRunnerStartFailure;
  }
}
