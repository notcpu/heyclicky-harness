using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HeyClicky.App.Home;
using HeyClicky.Drawing;
using HeyClicky.Platform.Overlay;
using Microsoft.UI.Dispatching;
using Windows.UI;

namespace HeyClicky.App.Notch.Peek
{
  // Token: 0x0200016E RID: 366
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class ClickyLessonInlinePlayer : IDisposable
  {
    // Token: 0x170003A3 RID: 931
    // (get) Token: 0x0600141B RID: 5147 RVA: 0x005A9080 File Offset: 0x005A9080
    public StoredLesson Stored { get; }

    // Token: 0x170003A4 RID: 932
    // (get) Token: 0x0600141C RID: 5148 RVA: 0x005A9088 File Offset: 0x005A9088
    public StoredLesson.Meta Lesson
    {
      get
      {
        return this.Stored.Data;
      }
    }

    // Token: 0x170003A5 RID: 933
    // (get) Token: 0x0600141D RID: 5149 RVA: 0x005A9098 File Offset: 0x005A9098
    // (set) Token: 0x0600141E RID: 5150 RVA: 0x005A90A0 File Offset: 0x005A90A0
    public bool IsPlaying { get; private set; }

    // Token: 0x170003A6 RID: 934
    // (get) Token: 0x0600141F RID: 5151 RVA: 0x005A90AC File Offset: 0x005A90AC
    // (set) Token: 0x06001420 RID: 5152 RVA: 0x005A90B4 File Offset: 0x005A90B4
    public bool HasEnded { get; private set; }

    // Token: 0x170003A7 RID: 935
    // (get) Token: 0x06001421 RID: 5153 RVA: 0x005A90C0 File Offset: 0x005A90C0
    // (set) Token: 0x06001422 RID: 5154 RVA: 0x005A90C8 File Offset: 0x005A90C8
    public int? CurrentBeat { get; private set; }

    // Token: 0x170003A8 RID: 936
    // (get) Token: 0x06001423 RID: 5155 RVA: 0x005A90D4 File Offset: 0x005A90D4
    // (set) Token: 0x06001424 RID: 5156 RVA: 0x005A90DC File Offset: 0x005A90DC
    public double Position { get; private set; }

    // Token: 0x170003A9 RID: 937
    // (get) Token: 0x06001425 RID: 5157 RVA: 0x005A90E8 File Offset: 0x005A90E8
    public double Duration
    {
      get
      {
        LessonPlaybackScript lessonPlaybackScript = this.script;
        if (lessonPlaybackScript == null)
        {
          return Math.Max(1.0, this.Lesson.Duration - this.Stored.ReplayShift);
        }
        return lessonPlaybackScript.Duration;
      }
    }

    // Token: 0x170003AA RID: 938
    // (get) Token: 0x06001426 RID: 5158 RVA: 0x005A9120 File Offset: 0x005A9120
    // (set) Token: 0x06001427 RID: 5159 RVA: 0x005A9128 File Offset: 0x005A9128
    [TupleElementNames(new string[] { "Width", "Height" })]
    [Nullable(0)]
    public ValueTuple<int, int> FrameSize
    {
      [NullableContext(0)]
      [return: TupleElementNames(new string[] { "Width", "Height" })]
      get;
      [NullableContext(0)]
      [param: TupleElementNames(new string[] { "Width", "Height" })]
      private set;
    }

    // Token: 0x14000055 RID: 85
    // (add) Token: 0x06001428 RID: 5160 RVA: 0x005A9134 File Offset: 0x005A9134
    // (remove) Token: 0x06001429 RID: 5161 RVA: 0x005A916C File Offset: 0x005A916C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Changed;

    // Token: 0x14000056 RID: 86
    // (add) Token: 0x0600142A RID: 5162 RVA: 0x005A91A4 File Offset: 0x005A91A4
    // (remove) Token: 0x0600142B RID: 5163 RVA: 0x005A91DC File Offset: 0x005A91DC
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action<double> ProgressChanged;

    // Token: 0x14000057 RID: 87
    // (add) Token: 0x0600142C RID: 5164 RVA: 0x005A9214 File Offset: 0x005A9214
    // (remove) Token: 0x0600142D RID: 5165 RVA: 0x005A924C File Offset: 0x005A924C
    [Nullable(new byte[] { 2, 1 })]
    [field: Nullable(new byte[] { 2, 1 })]
    public event Action<LessonPlaybackFrame> FrameReady;

    // Token: 0x0600142E RID: 5166 RVA: 0x005A9284 File Offset: 0x005A9284
    public ClickyLessonInlinePlayer(StoredLesson stored, LessonService service)
    {
      this.Stored = stored;
      this.dq = DispatcherQueue.GetForCurrentThread();
      this.script = stored.PlaybackScript();
      this.narration = stored.ReadNarration();
      if (this.narration != null)
      {
        this.voice = new LessonVoice();
      }
      this.penTint = ClickyLessonInlinePlayer.ParseHex(service.CursorColorHex) ?? Color.FromArgb(byte.MaxValue, 51, 128, byte.MaxValue);
      this.stage = service.NewPlaybackStage(stored);
      this.stage.ComposesOverScreen = true;
      this.stage.FrameReady += delegate(LessonPlaybackFrame frame)
      {
        this.FrameSize = new ValueTuple<int, int>(frame.Width, frame.Height);
        Action<LessonPlaybackFrame> frameReady = this.FrameReady;
        if (frameReady != null)
        {
          frameReady(frame);
        }
        this.dq.TryEnqueue(delegate
        {
          if (this.disposed)
          {
            return;
          }
          this.Position = Math.Min(frame.Position, this.Duration);
          Action<double> progressChanged = this.ProgressChanged;
          if (progressChanged == null)
          {
            return;
          }
          progressChanged(this.Position);
        });
      };
      this.stage.BeatChanged += delegate(int beat)
      {
        this.dq.TryEnqueue(delegate
        {
          if (this.disposed)
          {
            return;
          }
          this.CurrentBeat = ((beat < 0) ? null : new int?(beat));
          Action changed = this.Changed;
          if (changed == null)
          {
            return;
          }
          changed();
        });
      };
      this.stage.Ended += delegate
      {
        this.dq.TryEnqueue(new DispatcherQueueHandler(this.ReachEnd));
      };
      this.stage.PenReset += delegate
      {
        this.penRide = null;
      };
      this.stage.PenStrokes += delegate(IReadOnlyList<PenStrokePath> strokes, double now)
      {
        double pointWidth = this.stage.PointWidth;
        double pointHeight = this.stage.PointHeight;
        if (pointWidth <= 0.0 || strokes.Count == 0)
        {
          return;
        }
        if (this.penRide == null)
        {
          PenStrokePath penStrokePath = strokes.FirstOrDefault<PenStrokePath>((PenStrokePath s) => s.PointsPx.Count > 0);
          PointD? pointD = ((penStrokePath != null) ? new PointD?(penStrokePath.PointsPx[0]) : null);
          if (pointD == null)
          {
            return;
          }
          PointD valueOrDefault = pointD.GetValueOrDefault();
          this.penRide = new PenRideTrajectory();
          this.penRide.Begin(new PointD(Math.Max(20.0, Math.Min(valueOrDefault.X + 8.0, pointWidth - 20.0)), Math.Max(20.0, Math.Min(valueOrDefault.Y + 12.0, pointHeight - 20.0))), now, pointWidth, pointHeight);
        }
        this.penRide.Add(strokes.Select<PenStrokePath, ValueTuple<PointD[], double, double, double, double, double, double>>((PenStrokePath s) => new ValueTuple<PointD[], double, double, double, double, double, double>(s.PointsPx.ToArray<PointD>(), s.BeginTime, s.Duration, s.X1, s.Y1, s.X2, s.Y2)), now);
      };
      this.stage.PenPose = delegate(double time)
      {
        PenRideTrajectory penRideTrajectory = this.penRide;
        if (penRideTrajectory == null || penRideTrajectory.IsEmpty)
        {
          return null;
        }
        ValueTuple<PointD, PointD, double, double> valueTuple = penRideTrajectory.Sample(time);
        return new LessonPenPose?(new LessonPenPose(valueTuple.Item1.X, valueTuple.Item1.Y, valueTuple.Item3, valueTuple.Item4, this.penTint));
      };
    }

    // Token: 0x0600142F RID: 5167 RVA: 0x005A93BC File Offset: 0x005A93BC
    public async Task<bool> LoadAsync(int stageWidthInPixels)
    {
      bool flag;
      if (this.isLoaded)
      {
        flag = true;
      }
      else if (this.script == null)
      {
        flag = false;
      }
      else
      {
        bool flag2 = await this.stage.LoadAsync(this.script, stageWidthInPixels);
        this.isLoaded = flag2;
        flag = this.isLoaded;
      }
      return flag;
    }

    // Token: 0x06001430 RID: 5168 RVA: 0x005A9408 File Offset: 0x005A9408
    public void Play()
    {
      if (!this.isLoaded || this.disposed)
      {
        return;
      }
      if (this.HasEnded)
      {
        this.Position = 0.0;
        this.HasEnded = false;
      }
      this.stage.Play();
      ValueTuple<byte[], int>? valueTuple = this.narration;
      if (valueTuple != null)
      {
        ValueTuple<byte[], int> valueOrDefault = valueTuple.GetValueOrDefault();
        this.voice.Play(valueOrDefault, this.Position + this.Stored.ReplayShift);
      }
      this.IsPlaying = true;
      Action changed = this.Changed;
      if (changed != null)
      {
        changed();
      }
      string text = "lessons";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 2);
      defaultInterpolatedStringHandler.AppendLiteral("drawing window: playing from ");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.Position, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s");
      defaultInterpolatedStringHandler.AppendFormatted((this.narration == null) ? " (no recorded voice)" : "");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x06001431 RID: 5169 RVA: 0x005A9508 File Offset: 0x005A9508
    public void Pause()
    {
      if (!this.IsPlaying)
      {
        return;
      }
      this.stage.Pause();
      LessonVoice lessonVoice = this.voice;
      if (lessonVoice != null)
      {
        lessonVoice.Stop();
      }
      this.IsPlaying = false;
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001432 RID: 5170 RVA: 0x005A9548 File Offset: 0x005A9548
    public void TogglePlay()
    {
      if (this.IsPlaying)
      {
        this.Pause();
        return;
      }
      this.Play();
    }

    // Token: 0x06001433 RID: 5171 RVA: 0x005A9560 File Offset: 0x005A9560
    public void SeekToBeat(int index)
    {
      LessonPlaybackScript lessonPlaybackScript = this.script;
      IReadOnlyList<ValueTuple<double, double?>> readOnlyList = ((lessonPlaybackScript != null) ? lessonPlaybackScript.Lines : null);
      if (readOnlyList == null || index < 0 || index >= readOnlyList.Count)
      {
        return;
      }
      this.Seek(readOnlyList[index].Item1);
    }

    // Token: 0x06001434 RID: 5172 RVA: 0x005A95A4 File Offset: 0x005A95A4
    public void Seek(double position)
    {
      if (!this.isLoaded || this.disposed)
      {
        return;
      }
      this.Position = Math.Clamp(position, 0.0, this.Duration);
      this.HasEnded = false;
      this.stage.Seek(this.Position);
      if (this.IsPlaying)
      {
        ValueTuple<byte[], int>? valueTuple = this.narration;
        if (valueTuple != null)
        {
          ValueTuple<byte[], int> valueOrDefault = valueTuple.GetValueOrDefault();
          this.voice.Play(valueOrDefault, this.Position + this.Stored.ReplayShift);
        }
      }
      Action<double> progressChanged = this.ProgressChanged;
      if (progressChanged != null)
      {
        progressChanged(this.Position);
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001435 RID: 5173 RVA: 0x005A965C File Offset: 0x005A965C
    public void BeginCapturingSounds()
    {
      this.stage.BeginCapturingSounds();
    }

    // Token: 0x06001436 RID: 5174 RVA: 0x005A966C File Offset: 0x005A966C
    public Task<LessonPlaybackSounds> EndCapturingSoundsAsync()
    {
      return this.stage.EndCapturingSoundsAsync();
    }

    // Token: 0x170003AB RID: 939
    // (get) Token: 0x06001437 RID: 5175 RVA: 0x005A967C File Offset: 0x005A967C
    [TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })]
    [Nullable(new byte[] { 0, 1 })]
    public ValueTuple<byte[], int>? Narration
    {
      [return: TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })]
      [return: Nullable(new byte[] { 0, 1 })]
      get
      {
        return this.narration;
      }
    }

    // Token: 0x06001438 RID: 5176 RVA: 0x005A9684 File Offset: 0x005A9684
    private void ReachEnd()
    {
      if (this.disposed)
      {
        return;
      }
      this.HasEnded = true;
      this.IsPlaying = false;
      LessonVoice lessonVoice = this.voice;
      if (lessonVoice != null)
      {
        lessonVoice.Stop();
      }
      this.Position = this.Duration;
      Action<double> progressChanged = this.ProgressChanged;
      if (progressChanged != null)
      {
        progressChanged(this.Position);
      }
      Action changed = this.Changed;
      if (changed == null)
      {
        return;
      }
      changed();
    }

    // Token: 0x06001439 RID: 5177 RVA: 0x005A96EC File Offset: 0x005A96EC
    [NullableContext(2)]
    private static Color? ParseHex(string hex)
    {
      string text = (hex ?? "").TrimStart('#');
      uint num;
      if (text.Length != 6 || !uint.TryParse(text, NumberStyles.HexNumber, null, out num))
      {
        return null;
      }
      return new Color?(Color.FromArgb(byte.MaxValue, (byte)(num >> 16), (byte)(num >> 8), (byte)num));
    }

    // Token: 0x0600143A RID: 5178 RVA: 0x005A9748 File Offset: 0x005A9748
    public void Dispose()
    {
      if (this.disposed)
      {
        return;
      }
      this.Pause();
      this.disposed = true;
      LessonVoice lessonVoice = this.voice;
      if (lessonVoice != null)
      {
        lessonVoice.Dispose();
      }
      Task.Run(new Action(this.stage.Dispose));
    }

    // Token: 0x0400098C RID: 2444
    private readonly LessonPlaybackStage stage;

    // Token: 0x0400098D RID: 2445
    [Nullable(2)]
    private readonly LessonPlaybackScript script;

    // Token: 0x0400098E RID: 2446
    [TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })]
    [Nullable(new byte[] { 0, 1 })]
    private readonly ValueTuple<byte[], int>? narration;

    // Token: 0x0400098F RID: 2447
    [Nullable(2)]
    private readonly LessonVoice voice;

    // Token: 0x04000990 RID: 2448
    private readonly DispatcherQueue dq;

    // Token: 0x04000991 RID: 2449
    private readonly Color penTint;

    // Token: 0x04000992 RID: 2450
    [Nullable(2)]
    private PenRideTrajectory penRide;

    // Token: 0x04000993 RID: 2451
    private bool isLoaded;

    // Token: 0x04000994 RID: 2452
    private bool disposed;
  }
}
