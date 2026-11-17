using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x0200033E RID: 830
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class FakeCompanionSession : ICompanionSession
  {
    // Token: 0x1400007F RID: 127
    // (add) Token: 0x06002352 RID: 9042 RVA: 0x0062E268 File Offset: 0x0062E268
    // (remove) Token: 0x06002353 RID: 9043 RVA: 0x0062E2A0 File Offset: 0x0062E2A0
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event EventHandler StateChanged;

    // Token: 0x14000080 RID: 128
    // (add) Token: 0x06002354 RID: 9044 RVA: 0x0062E2D8 File Offset: 0x0062E2D8
    // (remove) Token: 0x06002355 RID: 9045 RVA: 0x0062E310 File Offset: 0x0062E310
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> UserTranscriptUpdated;

    // Token: 0x14000081 RID: 129
    // (add) Token: 0x06002356 RID: 9046 RVA: 0x0062E348 File Offset: 0x0062E348
    // (remove) Token: 0x06002357 RID: 9047 RVA: 0x0062E380 File Offset: 0x0062E380
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<string> ResponseTextUpdated;

    // Token: 0x14000082 RID: 130
    // (add) Token: 0x06002358 RID: 9048 RVA: 0x0062E3B8 File Offset: 0x0062E3B8
    // (remove) Token: 0x06002359 RID: 9049 RVA: 0x0062E3F0 File Offset: 0x0062E3F0
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event EventHandler<CompanionError> Failed;

    // Token: 0x1700067A RID: 1658
    // (get) Token: 0x0600235A RID: 9050 RVA: 0x0062E428 File Offset: 0x0062E428
    // (set) Token: 0x0600235B RID: 9051 RVA: 0x0062E430 File Offset: 0x0062E430
    public VoiceState State { get; private set; }

    // Token: 0x1700067B RID: 1659
    // (get) Token: 0x0600235C RID: 9052 RVA: 0x0062E43C File Offset: 0x0062E43C
    // (set) Token: 0x0600235D RID: 9053 RVA: 0x0062E444 File Offset: 0x0062E444
    public string ResponseText { get; private set; } = "";

    // Token: 0x0600235E RID: 9054 RVA: 0x0062E450 File Offset: 0x0062E450
    private void Set(VoiceState s)
    {
      this.State = s;
      EventHandler stateChanged = this.StateChanged;
      if (stateChanged == null)
      {
        return;
      }
      stateChanged(this, EventArgs.Empty);
    }

    // Token: 0x0600235F RID: 9055 RVA: 0x0062E470 File Offset: 0x0062E470
    public Task BeginPushToTalkAsync()
    {
      CancellationTokenSource cancellationTokenSource = this.cts;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.Set(VoiceState.Listening);
      return Task.CompletedTask;
    }

    // Token: 0x06002360 RID: 9056 RVA: 0x0062E490 File Offset: 0x0062E490
    public Task EndPushToTalkAsync()
    {
      EventHandler<string> userTranscriptUpdated = this.UserTranscriptUpdated;
      if (userTranscriptUpdated != null)
      {
        userTranscriptUpdated(this, "what's on my screen?");
      }
      return this.RunTurn("Here's what I see on your screen:\n\n1. The **notch** sits top-center and grows on a spring.\n2. Everything outside it stays *click-through*.\n\nTry `Ctrl+Alt` to talk, or double-tap **Ctrl** to type. More in the [docs](https://heyclicky.com).", true);
    }

    // Token: 0x1700067C RID: 1660
    // (get) Token: 0x06002361 RID: 9057 RVA: 0x0062E4B8 File Offset: 0x0062E4B8
    // (set) Token: 0x06002362 RID: 9058 RVA: 0x0062E4C0 File Offset: 0x0062E4C0
    [Nullable(2)]
    public INotchPresenter Presenter
    {
      [NullableContext(2)]
      get;
      [NullableContext(2)]
      set;
    }

    // Token: 0x06002363 RID: 9059 RVA: 0x0062E4CC File Offset: 0x0062E4CC
    public Task SubmitTextAsync(string text)
    {
      INotchPresenter presenter = this.Presenter;
      if (presenter != null)
      {
        presenter.Present(new NotchPresentation.TextResponse(text, "", true));
      }
      return this.RunTurn("Here's what I see on your screen:\n\n1. The **notch** sits top-center and grows on a spring.\n2. Everything outside it stays *click-through*.\n\nTry `Ctrl+Alt` to talk, or double-tap **Ctrl** to type. More in the [docs](https://heyclicky.com).", false);
    }

    // Token: 0x06002364 RID: 9060 RVA: 0x0062E4F8 File Offset: 0x0062E4F8
    public Task AskFromDrawingAsync(string text, string trigger)
    {
      return this.RunTurn("Here's what I see on your screen:\n\n1. The **notch** sits top-center and grows on a spring.\n2. Everything outside it stays *click-through*.\n\nTry `Ctrl+Alt` to talk, or double-tap **Ctrl** to type. More in the [docs](https://heyclicky.com).", true);
    }

    // Token: 0x06002365 RID: 9061 RVA: 0x0062E508 File Offset: 0x0062E508
    public void Cancel()
    {
      CancellationTokenSource cancellationTokenSource = this.cts;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.Set(VoiceState.Idle);
    }

    // Token: 0x06002366 RID: 9062 RVA: 0x0062E524 File Offset: 0x0062E524
    private async Task RunTurn(string answer, bool voice)
    {
      CancellationTokenSource cancellationTokenSource = this.cts;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      CancellationTokenSource c = (this.cts = new CancellationTokenSource());
      try
      {
        this.Set(VoiceState.Processing);
        await Task.Delay(900, c.Token);
        this.Set(VoiceState.Responding);
        StringBuilder sb = new StringBuilder();
        foreach (string text in answer.Split(' ', StringSplitOptions.None))
        {
          sb.Append((sb.Length == 0) ? "" : " ").Append(text);
          this.ResponseText = sb.ToString();
          EventHandler<string> responseTextUpdated = this.ResponseTextUpdated;
          if (responseTextUpdated != null)
          {
            responseTextUpdated(this, this.ResponseText);
          }
          await Task.Delay(voice ? 70 : 35, c.Token);
        }
        string[] array = null;
        this.Set(VoiceState.Idle);
        sb = null;
      }
      catch (OperationCanceledException)
      {
      }
    }

    // Token: 0x0400115B RID: 4443
    [Nullable(2)]
    private CancellationTokenSource cts;

    // Token: 0x0400115C RID: 4444
    public const string SampleAnswer = "Here's what I see on your screen:\n\n1. The **notch** sits top-center and grows on a spring.\n2. Everything outside it stays *click-through*.\n\nTry `Ctrl+Alt` to talk, or double-tap **Ctrl** to type. More in the [docs](https://heyclicky.com).";
  }
}
