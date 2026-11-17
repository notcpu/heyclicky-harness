using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x0200007C RID: 124
  public static class LessonNarration
  {
    // Token: 0x0600077F RID: 1919 RVA: 0x00364FB4 File Offset: 0x00364FB4
    [NullableContext(1)]
    public static byte[] BuildTrack(IReadOnlyList<double> lineStarts, IReadOnlyList<byte[]> lineAudio, int sampleRate, double duration)
    {
      int num = Math.Max(0, (int)(duration * (double)sampleRate));
      int[] array = new int[num];
      for (int i = 0; i < Math.Min(lineStarts.Count, lineAudio.Count); i++)
      {
        int num2 = Math.Max(0, (int)(lineStarts[i] * (double)sampleRate));
        byte[] array2 = lineAudio[i];
        int num3 = 0;
        while (num3 < array2.Length / 2 && num2 + num3 < num)
        {
          array[num2 + num3] += (int)BinaryPrimitives.ReadInt16LittleEndian(array2.AsSpan<byte>(num3 * 2));
          num3++;
        }
      }
      byte[] array3 = new byte[num * 2];
      for (int j = 0; j < num; j++)
      {
        BinaryPrimitives.WriteInt16LittleEndian(array3.AsSpan<byte>(j * 2), (short)Math.Clamp(array[j], -32768, 32767));
      }
      return array3;
    }

    // Token: 0x06000780 RID: 1920 RVA: 0x0036508C File Offset: 0x0036508C
    [return: Nullable(1)]
    public unsafe static byte[] ToWav(ReadOnlySpan<byte> pcm16Mono, int sampleRate)
    {
      int num = pcm16Mono.Length & -2;
      byte[] array = new byte[44 + num];
      Span<byte> span = array.AsSpan<byte>();
      new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B81FFE9A0176B5888A4651D91E4AF16BC4D56F4A24EDC8E5B3126B46EBD112B5), 4).CopyTo(span);
      ref Span<byte> ptr = ref span;
      BinaryPrimitives.WriteInt32LittleEndian(ptr.Slice(4, ptr.Length - 4), 36 + num);
      ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B6311D00BA4D5CF0D9459A6FA59565888E6A2A9360EB07ABB78778107E8A44AB), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(8, ptr.Length - 8));
      readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AEB65FD511E03FFB93D425EE0B2809E226827D1580B444DCFC4F03F22B4FC9B6), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(12, ptr.Length - 12));
      ptr = ref span;
      BinaryPrimitives.WriteInt32LittleEndian(ptr.Slice(16, ptr.Length - 16), 16);
      ptr = ref span;
      BinaryPrimitives.WriteInt16LittleEndian(ptr.Slice(20, ptr.Length - 20), 1);
      ptr = ref span;
      BinaryPrimitives.WriteInt16LittleEndian(ptr.Slice(22, ptr.Length - 22), 1);
      ptr = ref span;
      BinaryPrimitives.WriteInt32LittleEndian(ptr.Slice(24, ptr.Length - 24), sampleRate);
      ptr = ref span;
      BinaryPrimitives.WriteInt32LittleEndian(ptr.Slice(28, ptr.Length - 28), sampleRate * 2);
      ptr = ref span;
      BinaryPrimitives.WriteInt16LittleEndian(ptr.Slice(32, ptr.Length - 32), 2);
      ptr = ref span;
      BinaryPrimitives.WriteInt16LittleEndian(ptr.Slice(34, ptr.Length - 34), 16);
      readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.0E8703AFC36AEC11160A11E1F9DE65A07F4622274FF5197C273FB7EC7C75FCF8), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(36, ptr.Length - 36));
      ptr = ref span;
      BinaryPrimitives.WriteInt32LittleEndian(ptr.Slice(40, ptr.Length - 40), num);
      readOnlySpan = pcm16Mono.Slice(0, num);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(44, ptr.Length - 44));
      return array;
    }

    // Token: 0x06000781 RID: 1921 RVA: 0x00365278 File Offset: 0x00365278
    [return: TupleElementNames(new string[] { "Pcm16Mono", "SampleRate" })]
    [return: Nullable(new byte[] { 0, 1 })]
    public unsafe static ValueTuple<byte[], int>? FromWav(ReadOnlySpan<byte> wav)
    {
      if (wav.Length < 12 || !wav.Slice(0, 4).SequenceEqual<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B81FFE9A0176B5888A4651D91E4AF16BC4D56F4A24EDC8E5B3126B46EBD112B5), 4)) || !wav.Slice(8, 4).SequenceEqual<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B6311D00BA4D5CF0D9459A6FA59565888E6A2A9360EB07ABB78778107E8A44AB), 4)))
      {
        return null;
      }
      int num = 0;
      bool flag = false;
      int num2 = 12;
      while (num2 + 8 <= wav.Length)
      {
        ReadOnlySpan<byte> readOnlySpan = wav.Slice(num2, 4);
        ref ReadOnlySpan<byte> ptr = ref wav;
        int num3 = num2 + 4;
        int num4 = BinaryPrimitives.ReadInt32LittleEndian(ptr.Slice(num3, ptr.Length - num3));
        int num5 = num2 + 8;
        if (num4 < 0 || num5 + num4 > wav.Length)
        {
          num4 = wav.Length - num5;
        }
        if (readOnlySpan.SequenceEqual<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AEB65FD511E03FFB93D425EE0B2809E226827D1580B444DCFC4F03F22B4FC9B6), 4)) && num4 >= 16)
        {
          ptr = ref wav;
          num3 = num5;
          if (BinaryPrimitives.ReadInt16LittleEndian(ptr.Slice(num3, ptr.Length - num3)) != 1)
          {
            goto IL_013A;
          }
          ptr = ref wav;
          num3 = num5 + 2;
          if (BinaryPrimitives.ReadInt16LittleEndian(ptr.Slice(num3, ptr.Length - num3)) != 1)
          {
            goto IL_013A;
          }
          ptr = ref wav;
          num3 = num5 + 14;
          bool flag2 = BinaryPrimitives.ReadInt16LittleEndian(ptr.Slice(num3, ptr.Length - num3)) == 16;
          IL_013B:
          flag = flag2;
          ptr = ref wav;
          num3 = num5 + 4;
          num = BinaryPrimitives.ReadInt32LittleEndian(ptr.Slice(num3, ptr.Length - num3));
          goto IL_01A9;
          IL_013A:
          flag2 = false;
          goto IL_013B;
        }
        if (readOnlySpan.SequenceEqual<byte>(new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.0E8703AFC36AEC11160A11E1F9DE65A07F4622274FF5197C273FB7EC7C75FCF8), 4)))
        {
          if (!flag || num <= 0)
          {
            return null;
          }
          return new ValueTuple<byte[], int>?(new ValueTuple<byte[], int>(wav.Slice(num5, num4 & -2).ToArray(), num));
        }
        IL_01A9:
        num2 = num5 + num4 + (num4 & 1);
      }
      return null;
    }

    // Token: 0x0400039B RID: 923
    [Nullable(1)]
    public const string FileName = "narration.wav";
  }
}
