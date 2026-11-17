using System;

namespace HeyClicky.Core.Notch
{
  // Token: 0x0200010E RID: 270
  public sealed class MultiAgentRotationCursor
  {
    // Token: 0x17000420 RID: 1056
    // (get) Token: 0x060011A7 RID: 4519 RVA: 0x0039D6E4 File Offset: 0x0039D6E4
    // (set) Token: 0x060011A8 RID: 4520 RVA: 0x0039D6EC File Offset: 0x0039D6EC
    public int Index { get; private set; }

    // Token: 0x060011A9 RID: 4521 RVA: 0x0039D6F8 File Offset: 0x0039D6F8
    public void Restart(DateTimeOffset now)
    {
      this.lastAdvancedAt = now;
    }

    // Token: 0x060011AA RID: 4522 RVA: 0x0039D704 File Offset: 0x0039D704
    public bool AdvanceIfHoldElapsed(DateTimeOffset now, int activeAgentCount)
    {
      if (activeAgentCount <= 1)
      {
        return false;
      }
      if (now - this.lastAdvancedAt < MultiAgentRotationCursor.HoldDuration)
      {
        return false;
      }
      this.lastAdvancedAt = now;
      this.Index = (this.Index + 1) % activeAgentCount;
      return true;
    }

    // Token: 0x060011AB RID: 4523 RVA: 0x0039D740 File Offset: 0x0039D740
    public TimeSpan UntilDue(DateTimeOffset now)
    {
      TimeSpan timeSpan = MultiAgentRotationCursor.HoldDuration - (now - this.lastAdvancedAt);
      if (!(timeSpan < TimeSpan.Zero))
      {
        return timeSpan;
      }
      return TimeSpan.Zero;
    }

    // Token: 0x060011AC RID: 4524 RVA: 0x0039D778 File Offset: 0x0039D778
    public void Clamp(int activeAgentCount)
    {
      if (activeAgentCount > 0 && this.Index >= activeAgentCount)
      {
        this.Index %= activeAgentCount;
      }
    }

    // Token: 0x060011AD RID: 4525 RVA: 0x0039D798 File Offset: 0x0039D798
    public int IndexFor(int activeAgentCount, bool isMultiAgentPhase)
    {
      if (activeAgentCount > 0 && isMultiAgentPhase)
      {
        return this.Index % activeAgentCount;
      }
      return 0;
    }

    // Token: 0x040007AA RID: 1962
    public static readonly TimeSpan HoldDuration = TimeSpan.FromSeconds(5L);

    // Token: 0x040007AB RID: 1963
    private DateTimeOffset lastAdvancedAt = DateTimeOffset.MinValue;
  }
}
