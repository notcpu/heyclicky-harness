using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HeyClicky
{
  // Token: 0x0200001D RID: 29
  [NullableContext(1)]
  public interface ICompanionSession
  {
    // Token: 0x14000008 RID: 8
    // (add) Token: 0x06000130 RID: 304
    // (remove) Token: 0x06000131 RID: 305
    [Nullable(2)]
    event EventHandler StateChanged;

    // Token: 0x14000009 RID: 9
    // (add) Token: 0x06000132 RID: 306
    // (remove) Token: 0x06000133 RID: 307
    [Nullable(new byte[] { 2, 1 })]
    event EventHandler<string> UserTranscriptUpdated;

    // Token: 0x1400000A RID: 10
    // (add) Token: 0x06000134 RID: 308
    // (remove) Token: 0x06000135 RID: 309
    [Nullable(new byte[] { 2, 1 })]
    event EventHandler<string> ResponseTextUpdated;

    // Token: 0x1400000B RID: 11
    // (add) Token: 0x06000136 RID: 310
    // (remove) Token: 0x06000137 RID: 311
    [Nullable(new byte[] { 2, 1 })]
    event EventHandler<CompanionError> Failed;

    // Token: 0x17000055 RID: 85
    // (get) Token: 0x06000138 RID: 312
    VoiceState State { get; }

    // Token: 0x17000056 RID: 86
    // (get) Token: 0x06000139 RID: 313
    string ResponseText { get; }

    // Token: 0x0600013A RID: 314
    Task BeginPushToTalkAsync();

    // Token: 0x0600013B RID: 315
    Task EndPushToTalkAsync();

    // Token: 0x0600013C RID: 316
    Task SubmitTextAsync(string text);

    // Token: 0x0600013D RID: 317
    Task AskFromDrawingAsync(string text, string trigger);

    // Token: 0x0600013E RID: 318
    void Cancel();

    // Token: 0x0600013F RID: 319 RVA: 0x0001E9EC File Offset: 0x0001E9EC
    Task SubmitTextAsync(string text, IReadOnlyList<string> attachedFilePaths)
    {
      return this.SubmitTextAsync(text);
    }
  }
}
