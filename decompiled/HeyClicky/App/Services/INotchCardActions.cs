using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E8 RID: 232
  [NullableContext(1)]
  public interface INotchCardActions
  {
    // Token: 0x06000B55 RID: 2901
    void AnswerIntegrationSuggestion(string toolkit, IntegrationAnswer answer);

    // Token: 0x06000B56 RID: 2902
    void RemoveDictionaryWord(string word);

    // Token: 0x06000B57 RID: 2903
    void RestoreOutputVolume();

    // Token: 0x06000B58 RID: 2904
    void ResumeTutorial();

    // Token: 0x06000B59 RID: 2905
    void SendAgentFollowUp(string taskId, string text, IReadOnlyList<string> files);
  }
}
