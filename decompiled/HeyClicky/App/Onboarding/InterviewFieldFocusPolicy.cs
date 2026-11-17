using System;

namespace HeyClicky.App.Onboarding
{
  // Token: 0x0200012A RID: 298
  public static class InterviewFieldFocusPolicy
  {
    // Token: 0x0600102B RID: 4139 RVA: 0x0058220C File Offset: 0x0058220C
    public static bool Allows(bool fromPointerOrKeyboard, bool requestedByStage)
    {
      return fromPointerOrKeyboard || requestedByStage;
    }
  }
}
