using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeyClicky.Core.Notch;

namespace HeyClicky.App.Services
{
  // Token: 0x020000E7 RID: 231
  [NullableContext(1)]
  public interface INotchActions
  {
    // Token: 0x06000B4C RID: 2892
    void SubmitChatText(string text, IReadOnlyList<string> files);

    // Token: 0x06000B4D RID: 2893
    [NullableContext(2)]
    void OpenChatArtifact([Nullable(1)] string entry, string threadId, string clickySlug);

    // Token: 0x06000B4E RID: 2894
    void SendChatFollowUp(ChatHandOff entry, string followUp);

    // Token: 0x06000B4F RID: 2895
    void SendTypedChatFollowUp(ChatHandOff entry, string text, IReadOnlyList<string> files, bool fromWindow = false);

    // Token: 0x06000B50 RID: 2896
    bool SendWindowAgentMessageDirectly(string text, IReadOnlyList<string> files, string agentKey, bool spoken);

    // Token: 0x06000B51 RID: 2897
    void ExpandNotchIntoWindow(string from);

    // Token: 0x06000B52 RID: 2898
    void OpenInItsApp(string path, [Nullable(2)] string threadId);

    // Token: 0x06000B53 RID: 2899
    void RevealInExplorer(string path);

    // Token: 0x06000B54 RID: 2900
    void PlayDrawing(string lessonId, bool onWhiteBoard);
  }
}
