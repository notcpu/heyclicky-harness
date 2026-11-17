using System;

namespace HeyClicky
{
  // Token: 0x0200000B RID: 11
  public enum DictationInsertOutcome
  {
    // Token: 0x04000036 RID: 54
    Inserted,
    // Token: 0x04000037 RID: 55
    RefusedUnsafeTarget,
    // Token: 0x04000038 RID: 56
    FocusLost,
    // Token: 0x04000039 RID: 57
    ModifiersHeld,
    // Token: 0x0400003A RID: 58
    ClipboardWriteFailed,
    // Token: 0x0400003B RID: 59
    EmptyTranscript
  }
}
