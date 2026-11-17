using System;
using System.Runtime.CompilerServices;

namespace HeyClicky.Agents.Jobs
{
  // Token: 0x02000056 RID: 86
  [NullableContext(1)]
  public interface IClickyLiveNoteTurns
  {
    // Token: 0x0600076B RID: 1899
    [return: Nullable(2)]
    string NotePath(string clickySlug, string workspacePath);

    // Token: 0x0600076C RID: 1900
    string PromptSection(string notePath, [Nullable(2)] string userMessage, [Nullable(2)] string routineRun, ClickyLiveNoteWriter writer);

    // Token: 0x0600076D RID: 1901
    void FileActivity(string notePath, string path, [Nullable(2)] string streamedKind, [Nullable(2)] string streamedDiff);

    // Token: 0x0600076E RID: 1902
    void TurnEnded(string notePath);

    // Token: 0x0600076F RID: 1903
    void Forget(string notePath);
  }
}
