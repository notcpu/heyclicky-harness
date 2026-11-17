using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HeyClicky.App.DevFakes
{
  // Token: 0x02000347 RID: 839
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class WavFileMicrophone : IMicrophoneCapture, IDisposable
  {
    // Token: 0x14000088 RID: 136
    // (add) Token: 0x060023B7 RID: 9143 RVA: 0x0062F234 File Offset: 0x0062F234
    // (remove) Token: 0x060023B8 RID: 9144 RVA: 0x0062F26C File Offset: 0x0062F26C
    [Nullable(new byte[] { 2, 0 })]
    [field: Nullable(new byte[] { 2, 0 })]
    public event Action<ReadOnlyMemory<byte>> Frame;

    // Token: 0x1700068A RID: 1674
    // (get) Token: 0x060023B9 RID: 9145 RVA: 0x0062F2A4 File Offset: 0x0062F2A4
    public int SampleRate
    {
      get
      {
        return 24000;
      }
    }

    // Token: 0x1700068B RID: 1675
    // (get) Token: 0x060023BA RID: 9146 RVA: 0x0062F2AC File Offset: 0x0062F2AC
    public float Level
    {
      get
      {
        return Volatile.Read(ref this.level);
      }
    }

    // Token: 0x1700068C RID: 1676
    // (get) Token: 0x060023BB RID: 9147 RVA: 0x0062F2BC File Offset: 0x0062F2BC
    public bool IsRunning
    {
      get
      {
        return this.run != null;
      }
    }

    // Token: 0x1700068D RID: 1677
    // (get) Token: 0x060023BC RID: 9148 RVA: 0x0062F2CC File Offset: 0x0062F2CC
    public double DurationSeconds
    {
      get
      {
        return (double)this.samples.Length / 24000.0;
      }
    }

    // Token: 0x14000089 RID: 137
    // (add) Token: 0x060023BD RID: 9149 RVA: 0x0062F2E4 File Offset: 0x0062F2E4
    // (remove) Token: 0x060023BE RID: 9150 RVA: 0x0062F31C File Offset: 0x0062F31C
    [Nullable(2)]
    [method: NullableContext(2)]
    [field: Nullable(2)]
    public event Action Finished;

    // Token: 0x060023BF RID: 9151 RVA: 0x0062F354 File Offset: 0x0062F354
    public WavFileMicrophone(string path)
    {
      this.path = path;
      this.samples = WavFileMicrophone.Load(path);
      this.loadedWriteTime = File.GetLastWriteTimeUtc(path);
      string text = "Mic";
      DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
      defaultInterpolatedStringHandler.AppendLiteral("test WAV microphone: ");
      defaultInterpolatedStringHandler.AppendFormatted(Path.GetFileName(path));
      defaultInterpolatedStringHandler.AppendLiteral(" (");
      defaultInterpolatedStringHandler.AppendFormatted<double>(this.DurationSeconds, "0.00");
      defaultInterpolatedStringHandler.AppendLiteral(" s)");
      Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
    }

    // Token: 0x060023C0 RID: 9152 RVA: 0x0062F3E4 File Offset: 0x0062F3E4
    public void Start()
    {
      if (this.run != null)
      {
        return;
      }
      try
      {
        DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(this.path);
        if (lastWriteTimeUtc != this.loadedWriteTime)
        {
          this.samples = WavFileMicrophone.Load(this.path);
          this.loadedWriteTime = lastWriteTimeUtc;
          string text = "Mic";
          DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
          defaultInterpolatedStringHandler.AppendLiteral("test WAV reloaded (");
          defaultInterpolatedStringHandler.AppendFormatted<double>(this.DurationSeconds, "0.00");
          defaultInterpolatedStringHandler.AppendLiteral(" s)");
          Log.Info(text, defaultInterpolatedStringHandler.ToStringAndClear());
        }
      }
      catch (Exception ex)
      {
        Log.Warn("Mic", "test WAV reload: " + ex.Message);
      }
      CancellationTokenSource cts = (this.run = new CancellationTokenSource());
      new Thread(delegate
      {
        this.Pump(cts.Token);
      })
      {
        IsBackground = true,
        Name = "WavFileMicrophone"
      }.Start();
    }

    // Token: 0x060023C1 RID: 9153 RVA: 0x0062F4EC File Offset: 0x0062F4EC
    public void Stop()
    {
      CancellationTokenSource cancellationTokenSource = this.run;
      if (cancellationTokenSource != null)
      {
        cancellationTokenSource.Cancel();
      }
      this.run = null;
      Volatile.Write(ref this.level, 0f);
    }

    // Token: 0x060023C2 RID: 9154 RVA: 0x0062F518 File Offset: 0x0062F518
    private void Pump(CancellationToken ct)
    {
      Stopwatch stopwatch = Stopwatch.StartNew();
      byte[] array = new byte[960];
      int num = 0;
      int num2 = 0;
      bool flag = false;
      while (!ct.IsCancellationRequested)
      {
        double num3 = 0.0;
        for (int i = 0; i < 480; i++)
        {
          short num4 = ((num < this.samples.Length) ? this.samples[num++] : 0);
          array[i * 2] = (byte)num4;
          array[i * 2 + 1] = (byte)(num4 >> 8);
          num3 += (double)num4 * (double)num4;
        }
        Volatile.Write(ref this.level, (float)Math.Min(1.0, Math.Sqrt(num3 / 480.0) / 6000.0));
        Action<ReadOnlyMemory<byte>> frame = this.Frame;
        if (frame != null)
        {
          frame(array.ToArray<byte>());
        }
        if (!flag && num >= this.samples.Length)
        {
          flag = true;
          Action finished = this.Finished;
          if (finished != null)
          {
            finished();
          }
        }
        num2++;
        long num5 = (long)(num2 * 20) - stopwatch.ElapsedMilliseconds;
        if (num5 > 0L)
        {
          Thread.Sleep((int)num5);
        }
      }
    }

    // Token: 0x060023C3 RID: 9155 RVA: 0x0062F640 File Offset: 0x0062F640
    private static short[] Load(string path)
    {
      short[] array4;
      using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
      {
        if (new string(binaryReader.ReadChars(4)) != "RIFF")
        {
          throw new InvalidDataException("not a WAV file");
        }
        binaryReader.ReadInt32();
        if (new string(binaryReader.ReadChars(4)) != "WAVE")
        {
          throw new InvalidDataException("not a WAV file");
        }
        int num = 1;
        int num2 = 24000;
        int num3 = 16;
        byte[] array = null;
        while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
        {
          string text = new string(binaryReader.ReadChars(4));
          int num4 = binaryReader.ReadInt32();
          if (text == "fmt ")
          {
            binaryReader.ReadInt16();
            num = (int)binaryReader.ReadInt16();
            num2 = binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            binaryReader.ReadInt16();
            num3 = (int)binaryReader.ReadInt16();
            if (num4 > 16)
            {
              binaryReader.ReadBytes(num4 - 16);
            }
          }
          else
          {
            if (text == "data")
            {
              array = binaryReader.ReadBytes(num4);
              break;
            }
            binaryReader.ReadBytes(num4);
          }
        }
        if (array == null)
        {
          throw new InvalidDataException("WAV has no data chunk");
        }
        int num5 = num3 / 8;
        int num6 = array.Length / (num5 * num);
        double[] array2 = new double[num6];
        for (int i = 0; i < num6; i++)
        {
          double num7 = 0.0;
          for (int j = 0; j < num; j++)
          {
            int num8 = (i * num + j) * num5;
            num7 += (double)((num3 == 8) ? ((short)(array[num8] - 128) * 256) : BitConverter.ToInt16(array, num8));
          }
          array2[i] = num7 / (double)num;
        }
        int num9 = (int)((long)num6 * 24000L / (long)num2);
        short[] array3 = new short[num9];
        for (int k = 0; k < num9; k++)
        {
          double num10 = (double)k * (double)num2 / 24000.0;
          int num11 = (int)num10;
          double num12 = num10 - (double)num11;
          double num13 = ((num11 + 1 < num6) ? (array2[num11] * (1.0 - num12) + array2[num11 + 1] * num12) : array2[Math.Min(num11, num6 - 1)]);
          array3[k] = (short)Math.Clamp(num13, -32768.0, 32767.0);
        }
        array4 = array3;
      }
      return array4;
    }

    // Token: 0x060023C4 RID: 9156 RVA: 0x0062F8B4 File Offset: 0x0062F8B4
    public void Dispose()
    {
      this.Stop();
    }

    // Token: 0x0400116D RID: 4461
    public const int Rate = 24000;

    // Token: 0x0400116E RID: 4462
    private const int FrameSamples = 480;

    // Token: 0x0400116F RID: 4463
    private short[] samples;

    // Token: 0x04001170 RID: 4464
    private readonly string path;

    // Token: 0x04001171 RID: 4465
    private DateTime loadedWriteTime;

    // Token: 0x04001172 RID: 4466
    [Nullable(2)]
    private CancellationTokenSource run;

    // Token: 0x04001173 RID: 4467
    private float level;
  }
}
