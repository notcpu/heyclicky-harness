using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HeyClicky.App.Wiring;
using HeyClicky.Core.Settings;
using HeyClicky.Core.Voice;

namespace HeyClicky.App.Proactive
{
  // Token: 0x020000F5 RID: 245
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TapVoiceCapture
  {
    // Token: 0x06000CCB RID: 3275 RVA: 0x0056350C File Offset: 0x0056350C
    public TapVoiceCapture(RealStack real)
    {
      this.real = real;
    }

    // Token: 0x17000242 RID: 578
    // (get) Token: 0x06000CCC RID: 3276 RVA: 0x00563528 File Offset: 0x00563528
    public bool IsActive
    {
      get
      {
        object obj = this.gate;
        bool flag2;
        lock (obj)
        {
          flag2 = this.ears != null;
        }
        return flag2;
      }
    }

    // Token: 0x1400002F RID: 47
    // (add) Token: 0x06000CCD RID: 3277 RVA: 0x00563570 File Offset: 0x00563570
    // (remove) Token: 0x06000CCE RID: 3278 RVA: 0x005635A8 File Offset: 0x005635A8
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<bool> ActiveChanged;

    // Token: 0x06000CCF RID: 3279 RVA: 0x005635E0 File Offset: 0x005635E0
    public bool Start(Action<string> onFinalTranscript)
    {
      if (!this.real.Core.Auth.IsSignedIn)
      {
        return false;
      }
      object obj = this.gate;
      lock (obj)
      {
        if (this.ears != null)
        {
          return true;
        }
        AppSettings appSettings = this.real.Core.Settings.Current;
        this.ears = new DeepgramTranscriber(this.real.Core.Http, (bool force, CancellationToken ct) => this.real.Core.Auth.GetValidAccessTokenAsync(force, ct), appSettings.DictationLanguageCode, Array.Empty<string>(), "companion", this.real.Microphone.SampleRate, "one_brain");
        this.ears.Start(false);
        this.onTranscript = onFinalTranscript;
      }
      this.real.Microphone.Frame += this.OnFrame;
      this.real.MicrophoneStartCue.Play("textOpen", null);
      try
      {
        this.real.Microphone.Start();
      }
      catch (Exception ex)
      {
        Log.Warn("voice", "tap capture microphone: " + ex.Message);
        this.real.MicrophoneStartCue.Cancel();
        this.Cancel();
        return false;
      }
      Action<bool> activeChanged = this.ActiveChanged;
      if (activeChanged != null)
      {
        activeChanged(true);
      }
      return true;
    }

    // Token: 0x06000CD0 RID: 3280 RVA: 0x00563760 File Offset: 0x00563760
    [NullableContext(0)]
    private void OnFrame(ReadOnlyMemory<byte> frame)
    {
      object obj = this.gate;
      DeepgramTranscriber deepgramTranscriber;
      lock (obj)
      {
        deepgramTranscriber = this.ears;
      }
      if (deepgramTranscriber != null)
      {
        deepgramTranscriber.PushAudio(frame.Span);
      }
    }

    // Token: 0x06000CD1 RID: 3281 RVA: 0x005637B4 File Offset: 0x005637B4
    public void Stop()
    {
      TapVoiceCapture.<>c__DisplayClass12_0 CS$<>8__locals1 = new TapVoiceCapture.<>c__DisplayClass12_0();
      CS$<>8__locals1.<>4__this = this;
      object obj = this.gate;
      lock (obj)
      {
        CS$<>8__locals1.e = this.ears;
        CS$<>8__locals1.done = this.onTranscript;
        this.ears = null;
        this.onTranscript = null;
      }
      if (CS$<>8__locals1.e == null)
      {
        return;
      }
      this.StopMicrophone();
      Action<bool> activeChanged = this.ActiveChanged;
      if (activeChanged != null)
      {
        activeChanged(false);
      }
      Task.Run(delegate
      {
        TapVoiceCapture.<>c__DisplayClass12_0.<<Stop>b__0>d <<Stop>b__0>d;
        <<Stop>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
        <<Stop>b__0>d.<>4__this = CS$<>8__locals1;
        <<Stop>b__0>d.<>1__state = -1;
        <<Stop>b__0>d.<>t__builder.Start<TapVoiceCapture.<>c__DisplayClass12_0.<<Stop>b__0>d>(ref <<Stop>b__0>d);
        return <<Stop>b__0>d.<>t__builder.Task;
      });
    }

    // Token: 0x06000CD2 RID: 3282 RVA: 0x00563854 File Offset: 0x00563854
    public bool Cancel()
    {
      object obj = this.gate;
      DeepgramTranscriber deepgramTranscriber;
      lock (obj)
      {
        deepgramTranscriber = this.ears;
        this.ears = null;
        this.onTranscript = null;
      }
      if (deepgramTranscriber == null)
      {
        return false;
      }
      this.StopMicrophone();
      deepgramTranscriber.DisposeAsync().AsTask();
      Action<bool> activeChanged = this.ActiveChanged;
      if (activeChanged != null)
      {
        activeChanged(false);
      }
      return true;
    }

    // Token: 0x06000CD3 RID: 3283 RVA: 0x005638D0 File Offset: 0x005638D0
    private void StopMicrophone()
    {
      this.real.Microphone.Frame -= this.OnFrame;
      try
      {
        this.real.Microphone.Stop();
      }
      catch
      {
      }
    }

    // Token: 0x040004A8 RID: 1192
    private readonly RealStack real;

    // Token: 0x040004A9 RID: 1193
    private readonly object gate = new object();

    // Token: 0x040004AA RID: 1194
    [Nullable(2)]
    private DeepgramTranscriber ears;

    // Token: 0x040004AB RID: 1195
    [Nullable(new byte[] { 2, 1 })]
    private Action<string> onTranscript;
  }
}
