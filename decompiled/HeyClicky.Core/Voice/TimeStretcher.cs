using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000085 RID: 133
  [NullableContext(1)]
  [Nullable(0)]
  public sealed class TimeStretcher
  {
    // Token: 0x170001C3 RID: 451
    // (get) Token: 0x06000800 RID: 2048 RVA: 0x00367F7C File Offset: 0x00367F7C
    // (set) Token: 0x06000801 RID: 2049 RVA: 0x00367F84 File Offset: 0x00367F84
    public double Rate { get; set; }

    // Token: 0x06000802 RID: 2050 RVA: 0x00367F90 File Offset: 0x00367F90
    public TimeStretcher(int sampleRate, double rate)
    {
      this.window = Math.Max(64, sampleRate * 30 / 1000 / 2 * 2);
      this.hop = this.window / 2;
      this.tolerance = Math.Max(8, sampleRate * 10 / 1000);
      this.hann = new float[this.window];
      for (int i = 0; i < this.window; i++)
      {
        this.hann[i] = (float)(0.5 - 0.5 * Math.Cos(6.283185307179586 * (double)i / (double)this.window));
      }
      this.accumulator = new float[this.window];
      this.Rate = rate;
    }

    // Token: 0x06000803 RID: 2051 RVA: 0x0036806C File Offset: 0x0036806C
    [NullableContext(0)]
    [return: Nullable(1)]
    public unsafe byte[] Process(ReadOnlySpan<byte> pcm16)
    {
      if (this.flushed)
      {
        throw new InvalidOperationException("the stretcher was flushed");
      }
      int num = pcm16.Length / 2;
      this.EnsureCapacity(this.inputCount + num);
      for (int i = 0; i < num; i++)
      {
        this.input[this.inputCount + i] = (float)((short)((int)(*pcm16[2 * i]) | ((int)(*pcm16[2 * i + 1]) << 8))) / 32768f;
      }
      this.inputCount += num;
      this.totalInput += (long)num;
      List<float> list = new List<float>();
      this.RunFrames(list, false, long.MaxValue);
      return TimeStretcher.ToPcm16(list);
    }

    // Token: 0x06000804 RID: 2052 RVA: 0x0036811C File Offset: 0x0036811C
    public byte[] Flush()
    {
      if (this.flushed)
      {
        return Array.Empty<byte>();
      }
      this.flushed = true;
      long num = this.inputBase + (long)this.inputCount;
      this.EnsureCapacity(this.inputCount + this.window + 2 * this.tolerance + this.hop);
      Array.Clear(this.input, this.inputCount, this.window + 2 * this.tolerance + this.hop);
      this.inputCount += this.window + 2 * this.tolerance + this.hop;
      List<float> list = new List<float>();
      this.RunFrames(list, true, num);
      if (this.previous >= 0L)
      {
        for (int i = 0; i < this.hop; i++)
        {
          list.Add(this.accumulator[i]);
        }
      }
      return TimeStretcher.ToPcm16(list);
    }

    // Token: 0x06000805 RID: 2053 RVA: 0x003681F8 File Offset: 0x003681F8
    private void RunFrames(List<float> output, bool endOfInput, long realEnd = 9223372036854775807L)
    {
      long num = this.inputBase + (long)this.inputCount;
      for (;;)
      {
        long num2 = (long)Math.Round(this.nominal);
        if (endOfInput && num2 >= realEnd)
        {
          break;
        }
        long num3 = Math.Max(this.inputBase, num2 - (long)this.tolerance);
        long num4 = num2 + (long)this.tolerance;
        if (num4 + (long)this.window > num)
        {
          return;
        }
        if (this.previous >= 0L && this.previous + (long)this.hop + (long)this.hop > num)
        {
          return;
        }
        long num5 = ((this.previous < 0L) ? Math.Max(this.inputBase, num2) : this.BestStart(num3, num4));
        this.AddFrame(num5, this.previous < 0L);
        for (int i = 0; i < this.hop; i++)
        {
          output.Add(this.accumulator[i]);
        }
        Array.Copy(this.accumulator, this.hop, this.accumulator, 0, this.window - this.hop);
        Array.Clear(this.accumulator, this.window - this.hop, this.hop);
        this.previous = num5;
        this.nominal += (double)this.hop * Math.Max(0.25, this.Rate);
        this.Trim();
      }
    }

    // Token: 0x06000806 RID: 2054 RVA: 0x00368350 File Offset: 0x00368350
    private long BestStart(long low, long high)
    {
      long num = this.previous + (long)this.hop;
      long num2 = low;
      double num3 = double.NegativeInfinity;
      for (long num4 = low; num4 <= high; num4 += 2L)
      {
        double num5 = this.Similarity(num4, num);
        if (num5 > num3)
        {
          num3 = num5;
          num2 = num4;
        }
      }
      foreach (long num6 in new long[]
      {
        num2 - 1L,
        num2 + 1L
      })
      {
        if (num6 >= low && num6 <= high)
        {
          double num7 = this.Similarity(num6, num);
          if (num7 > num3)
          {
            num3 = num7;
            num2 = num6;
          }
        }
      }
      return num2;
    }

    // Token: 0x06000807 RID: 2055 RVA: 0x003683EC File Offset: 0x003683EC
    private double Similarity(long candidate, long natural)
    {
      int num = (int)(candidate - this.inputBase);
      int num2 = (int)(natural - this.inputBase);
      double num3 = 0.0;
      double num4 = 1E-09;
      for (int i = 0; i < this.hop; i++)
      {
        float num5 = this.input[num + i];
        num3 += (double)(num5 * this.input[num2 + i]);
        num4 += (double)(num5 * num5);
      }
      return num3 / Math.Sqrt(num4);
    }

    // Token: 0x06000808 RID: 2056 RVA: 0x00368468 File Offset: 0x00368468
    private void AddFrame(long start, bool first)
    {
      int num = (int)(start - this.inputBase);
      for (int i = 0; i < this.window; i++)
      {
        this.accumulator[i] += this.input[num + i] * ((first && i < this.hop) ? 1f : this.hann[i]);
      }
    }

    // Token: 0x06000809 RID: 2057 RVA: 0x003684C8 File Offset: 0x003684C8
    private void Trim()
    {
      int num = (int)Math.Clamp(Math.Min(this.previous + (long)this.hop, (long)Math.Round(this.nominal) - (long)this.tolerance) - this.inputBase, 0L, (long)this.inputCount);
      if (num < 4096)
      {
        return;
      }
      Array.Copy(this.input, num, this.input, 0, this.inputCount - num);
      this.inputCount -= num;
      this.inputBase += (long)num;
    }

    // Token: 0x0600080A RID: 2058 RVA: 0x00368554 File Offset: 0x00368554
    private void EnsureCapacity(int needed)
    {
      if (needed <= this.input.Length)
      {
        return;
      }
      Array.Resize<float>(ref this.input, Math.Max(needed, this.input.Length * 2));
    }

    // Token: 0x0600080B RID: 2059 RVA: 0x00368580 File Offset: 0x00368580
    private static byte[] ToPcm16(List<float> samples)
    {
      byte[] array = new byte[samples.Count * 2];
      for (int i = 0; i < samples.Count; i++)
      {
        short num = (short)Math.Clamp(Math.Round((double)(samples[i] * 32768f)), -32768.0, 32767.0);
        array[2 * i] = (byte)num;
        array[2 * i + 1] = (byte)(num >> 8);
      }
      return array;
    }

    // Token: 0x040003E9 RID: 1001
    private readonly int window;

    // Token: 0x040003EA RID: 1002
    private readonly int hop;

    // Token: 0x040003EB RID: 1003
    private readonly int tolerance;

    // Token: 0x040003EC RID: 1004
    private readonly float[] hann;

    // Token: 0x040003ED RID: 1005
    private readonly float[] accumulator;

    // Token: 0x040003EE RID: 1006
    private float[] input = new float[8192];

    // Token: 0x040003EF RID: 1007
    private int inputCount;

    // Token: 0x040003F0 RID: 1008
    private long inputBase;

    // Token: 0x040003F1 RID: 1009
    private double nominal;

    // Token: 0x040003F2 RID: 1010
    private long previous = -1L;

    // Token: 0x040003F3 RID: 1011
    private long totalInput;

    // Token: 0x040003F4 RID: 1012
    private bool flushed;
  }
}
