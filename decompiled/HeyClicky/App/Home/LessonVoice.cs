using System;
using System.Runtime.CompilerServices;
using HeyClicky.App.Wiring;
using HeyClicky.Platform.Audio;

namespace HeyClicky.App.Home
{
  // Token: 0x020002B5 RID: 693
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class LessonVoice : IDisposable
  {
    // Token: 0x06001C9B RID: 7323 RVA: 0x005FB0A4 File Offset: 0x005FB0A4
    public LessonVoice()
    {
      IAudioOutput audioOutput2;
      if (!(Environment.GetEnvironmentVariable("HEYCLICKY_MUTE_AUDIO") == "1"))
      {
        IAudioOutput audioOutput = this.device;
        audioOutput2 = audioOutput;
      }
      else
      {
        IAudioOutput audioOutput = new MutedAudioOutput(this.device);
        audioOutput2 = audioOutput;
      }
      this.output = audioOutput2;
    }

    // Token: 0x06001C9C RID: 7324 RVA: 0x005FB10C File Offset: 0x005FB10C
    public void Play([TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })] [Nullable(new byte[] { 0, 1 })] ValueTuple<byte[], int> track, double fromSeconds)
    {
      this.output.Flush();
      this.pcm = track.Item1;
      this.sampleRate = track.Item2;
      if (this.sampleRate != this.output.SampleRate)
      {
        string text = "lessons";
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
        defaultInterpolatedStringHandler.AppendLiteral("narration at ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.sampleRate);
        defaultInterpolatedStringHandler.AppendLiteral(" Hz, output at ");
        defaultInterpolatedStringHandler.AppendFormatted<int>(this.output.SampleRate);
        defaultInterpolatedStringHandler.AppendLiteral(" Hz: not played");
        Log.Warn(text, defaultInterpolatedStringHandler.ToStringAndClear());
        return;
      }
      int num = Math.Clamp((int)(fromSeconds * (double)this.sampleRate) * 2, 0, this.pcm.Length);
      this.startedAt = fromSeconds;
      if (num < this.pcm.Length)
      {
        this.output.Enqueue(this.pcm.AsSpan<byte>(num));
      }
    }

    // Token: 0x170004F6 RID: 1270
    // (get) Token: 0x06001C9D RID: 7325 RVA: 0x005FB1F8 File Offset: 0x005FB1F8
    public double Position
    {
      get
      {
        return this.startedAt + this.output.PlayedSeconds;
      }
    }

    // Token: 0x06001C9E RID: 7326 RVA: 0x005FB20C File Offset: 0x005FB20C
    public void Stop()
    {
      this.output.Flush();
    }

    // Token: 0x06001C9F RID: 7327 RVA: 0x005FB21C File Offset: 0x005FB21C
    public void Dispose()
    {
      this.output.Flush();
      this.device.Dispose();
    }

    // Token: 0x04000DAF RID: 3503
    private readonly IAudioOutput output;

    // Token: 0x04000DB0 RID: 3504
    private readonly WasapiAudioOutput device = new WasapiAudioOutput();

    // Token: 0x04000DB1 RID: 3505
    private byte[] pcm = Array.Empty<byte>();

    // Token: 0x04000DB2 RID: 3506
    private int sampleRate = 24000;

    // Token: 0x04000DB3 RID: 3507
    private double startedAt;
  }
}
