using System;

namespace HeyClicky.Core.Brain
{
  // Token: 0x020001F1 RID: 497
  public static class BrainPrewarmPolicy
  {
    // Token: 0x06001CEC RID: 7404 RVA: 0x003E5180 File Offset: 0x003E5180
    public static int ConversationTokens(int lastTurnInputTokens, int baselineInputTokens)
    {
      if (lastTurnInputTokens > 0 && baselineInputTokens > 0)
      {
        return Math.Max(0, lastTurnInputTokens - baselineInputTokens);
      }
      return 0;
    }

    // Token: 0x06001CED RID: 7405 RVA: 0x003E5198 File Offset: 0x003E5198
    public static bool ShouldPrewarm(int threadItemCount, bool isFirstKeyDownAfterReload, TimeSpan sinceLastTurn, int conversationTokens, bool usesOpenAIBrain, TimeSpan sinceLastPrewarm)
    {
      return usesOpenAIBrain && threadItemCount > 0 && conversationTokens >= 40000 && !(sinceLastPrewarm <= BrainPrewarmPolicy.MinimumTimeBetweenPrewarms) && (isFirstKeyDownAfterReload || sinceLastTurn > BrainPrewarmPolicy.PrewarmColdCacheGap);
    }

    // Token: 0x04000C64 RID: 3172
    public const int PrewarmMinimumConversationTokens = 40000;

    // Token: 0x04000C65 RID: 3173
    public static readonly TimeSpan PrewarmColdCacheGap = TimeSpan.FromMinutes(25L);

    // Token: 0x04000C66 RID: 3174
    public static readonly TimeSpan MinimumTimeBetweenPrewarms = TimeSpan.FromSeconds(60L);
  }
}
