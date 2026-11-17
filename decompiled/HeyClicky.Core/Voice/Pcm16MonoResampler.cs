using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007A RID: 122
  [NullableContext(1)]
  [Nullable(0)]
  internal sealed class Pcm16MonoResampler
  {
    // Token: 0x0600076F RID: 1903 RVA: 0x00363F6C File Offset: 0x00363F6C
    public Pcm16MonoResampler(int inputRate, int outputRate)
    {
      this.inputRate = inputRate;
      this.outputRate = outputRate;
      int num = Pcm16MonoResampler.Gcd(inputRate, outputRate);
      this.step = (long)(inputRate / num);
      this.phaseCount = outputRate / num;
      this.cutoff = 0.5 * Math.Min(1.0, (double)outputRate / (double)inputRate) * 0.92;
      this.halfWidth = (int)Math.Ceiling(16.0 / (2.0 * this.cutoff));
      this.phases = new float[this.phaseCount][];
      this.history = new float[4096];
      this.historyLength = this.halfWidth;
      this.historyStart = (long)(-(long)this.halfWidth);
    }

    // Token: 0x06000770 RID: 1904 RVA: 0x00364038 File Offset: 0x00364038
    [NullableContext(0)]
    [return: Nullable(1)]
    public unsafe byte[] Process(ReadOnlySpan<byte> pcm16)
    {
      if (this.drained || pcm16.Length < 2)
      {
        return Array.Empty<byte>();
      }
      if (this.inputRate == this.outputRate)
      {
        return pcm16.Slice(0, pcm16.Length & -2).ToArray();
      }
      ReadOnlySpan<short> readOnlySpan = MemoryMarshal.Cast<byte, short>(pcm16);
      Span<float> span = this.Reserve(readOnlySpan.Length);
      for (int i = 0; i < readOnlySpan.Length; i++)
      {
        *span[i] = (float)(*readOnlySpan[i]) / 32768f;
      }
      this.inputCount += (long)readOnlySpan.Length;
      return this.Produce(this.inputCount - 1L, long.MaxValue);
    }

    // Token: 0x06000771 RID: 1905 RVA: 0x003640F4 File Offset: 0x003640F4
    public byte[] Drain()
    {
      if (this.drained)
      {
        return Array.Empty<byte>();
      }
      this.drained = true;
      if (this.inputRate == this.outputRate)
      {
        return Array.Empty<byte>();
      }
      this.Reserve(this.halfWidth).Clear();
      return this.Produce(this.inputCount - 1L + (long)this.halfWidth, this.inputCount);
    }

    // Token: 0x06000772 RID: 1906 RVA: 0x0036415C File Offset: 0x0036415C
    [NullableContext(0)]
    private Span<float> Reserve(int count)
    {
      if (this.historyLength + count > this.history.Length)
      {
        Array.Resize<float>(ref this.history, Math.Max(this.history.Length * 2, this.historyLength + count));
      }
      Span<float> span = this.history.AsSpan<float>(this.historyLength, count);
      this.historyLength += count;
      return span;
    }

    // Token: 0x06000773 RID: 1907 RVA: 0x003641C0 File Offset: 0x003641C0
    private byte[] Produce(long lastInputIndex, long endAt = 9223372036854775807L)
    {
      List<short> list = new List<short>();
      for (;;)
      {
        long num = this.outputIndex * this.step;
        long num2 = num / (long)this.phaseCount;
        if (num2 + (long)this.halfWidth > lastInputIndex || num2 >= endAt)
        {
          break;
        }
        float[] array = this.Taps((int)(num % (long)this.phaseCount));
        int num3 = (int)(num2 - (long)this.halfWidth + 1L - this.historyStart);
        double num4 = 0.0;
        for (int i = 0; i < array.Length; i++)
        {
          num4 += (double)(array[i] * this.history[num3 + i]);
        }
        list.Add((short)Math.Clamp(Math.Round(num4 * 32768.0), -32768.0, 32767.0));
        this.outputIndex += 1L;
      }
      int num5 = (int)Math.Clamp(this.outputIndex * this.step / (long)this.phaseCount - (long)this.halfWidth + 1L - this.historyStart, 0L, (long)this.historyLength);
      if (num5 > 0)
      {
        Array.Copy(this.history, num5, this.history, 0, this.historyLength - num5);
        this.historyLength -= num5;
        this.historyStart += (long)num5;
      }
      return MemoryMarshal.AsBytes<short>(CollectionsMarshal.AsSpan<short>(list)).ToArray();
    }

    // Token: 0x06000774 RID: 1908 RVA: 0x0036432C File Offset: 0x0036432C
    private float[] Taps(int phase)
    {
      float[] array = this.phases[phase];
      if (array != null)
      {
        return array;
      }
      double num = (double)phase / (double)this.phaseCount;
      float[] array2 = new float[2 * this.halfWidth];
      double num2 = 0.0;
      double[] array3 = new double[array2.Length];
      for (int i = 0; i < array2.Length; i++)
      {
        double num3 = (double)(i - this.halfWidth + 1) - num;
        double num4 = 2.0 * this.cutoff * num3;
        double num5 = ((Math.Abs(num4) < 1E-12) ? 1.0 : (Math.Sin(3.141592653589793 * num4) / (3.141592653589793 * num4)));
        double num6 = num3 / (double)this.halfWidth;
        double num7 = ((Math.Abs(num6) >= 1.0) ? 0.0 : (0.42 + 0.5 * Math.Cos(3.141592653589793 * num6) + 0.08 * Math.Cos(6.283185307179586 * num6)));
        array3[i] = num5 * num7;
        num2 += array3[i];
      }
      for (int j = 0; j < array2.Length; j++)
      {
        array2[j] = (float)(array3[j] / num2);
      }
      return this.phases[phase] = array2;
    }

    // Token: 0x06000775 RID: 1909 RVA: 0x00364498 File Offset: 0x00364498
    private static int Gcd(int a, int b)
    {
      while (b != 0)
      {
        int num = b;
        b = a % b;
        a = num;
      }
      return a;
    }

    // Token: 0x0400038C RID: 908
    private const int ZeroCrossings = 16;

    // Token: 0x0400038D RID: 909
    private readonly int inputRate;

    // Token: 0x0400038E RID: 910
    private readonly int outputRate;

    // Token: 0x0400038F RID: 911
    private readonly long step;

    // Token: 0x04000390 RID: 912
    private readonly int phaseCount;

    // Token: 0x04000391 RID: 913
    private readonly double cutoff;

    // Token: 0x04000392 RID: 914
    private readonly int halfWidth;

    // Token: 0x04000393 RID: 915
    [Nullable(new byte[] { 1, 2 })]
    private readonly float[][] phases;

    // Token: 0x04000394 RID: 916
    private float[] history;

    // Token: 0x04000395 RID: 917
    private int historyLength;

    // Token: 0x04000396 RID: 918
    private long historyStart;

    // Token: 0x04000397 RID: 919
    private long inputCount;

    // Token: 0x04000398 RID: 920
    private long outputIndex;

    // Token: 0x04000399 RID: 921
    private bool drained;
  }
}
