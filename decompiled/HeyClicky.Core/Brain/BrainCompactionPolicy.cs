using System;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F2 RID: 498
  public static class BrainCompactionPolicy
  {
    // Token: 0x06001CEF RID: 7407 RVA: 0x003E51EC File Offset: 0x003E51EC
    public static bool ShouldAskWorker(int inputTokens, int baselineInputTokens, int nextCheckInputTokens)
    {
      if (inputTokens < nextCheckInputTokens)
      {
        return false;
      }
      if (baselineInputTokens <= 0)
      {
        return inputTokens >= 75000;
      }
      return inputTokens - baselineInputTokens >= 45000;
    }

    // Token: 0x06001CF0 RID: 7408 RVA: 0x003E5214 File Offset: 0x003E5214
    public static int NextCheckInputTokens(int afterWaitAtInputTokens, int tokensUntilFold)
    {
      return afterWaitAtInputTokens + Math.Max(1, tokensUntilFold);
    }

    // Token: 0x04000C67 RID: 3175
    public const int CompactionConversationTokenThreshold = 45000;

    // Token: 0x04000C68 RID: 3176
    public const int CompactionInputTokenThreshold = 75000;
  }
}
