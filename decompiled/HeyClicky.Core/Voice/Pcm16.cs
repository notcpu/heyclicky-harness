using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeyClicky.Core.Voice
{
  // Token: 0x02000071 RID: 113
  public static class Pcm16
  {
    // Token: 0x06000700 RID: 1792 RVA: 0x00360A4C File Offset: 0x00360A4C
    [return: Nullable(1)]
    public unsafe static byte[] WavFromPcm16Mono(ReadOnlySpan<byte> pcm16, int sampleRate)
    {
      byte[] array = new byte[44 + pcm16.Length];
      Span<byte> span = array.AsSpan<byte>();
      new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B81FFE9A0176B5888A4651D91E4AF16BC4D56F4A24EDC8E5B3126B46EBD112B5), 4).CopyTo(span);
      ref Span<byte> ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(4, ptr.Length - 4), (uint)(36 + pcm16.Length));
      ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B6311D00BA4D5CF0D9459A6FA59565888E6A2A9360EB07ABB78778107E8A44AB), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(8, ptr.Length - 8));
      readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AEB65FD511E03FFB93D425EE0B2809E226827D1580B444DCFC4F03F22B4FC9B6), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(12, ptr.Length - 12));
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(16, ptr.Length - 16), 16U);
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(20, ptr.Length - 20), 1);
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(22, ptr.Length - 22), 1);
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(24, ptr.Length - 24), (uint)sampleRate);
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(28, ptr.Length - 28), (uint)(sampleRate * 2));
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(32, ptr.Length - 32), 2);
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(34, ptr.Length - 34), 16);
      readOnlySpan = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.0E8703AFC36AEC11160A11E1F9DE65A07F4622274FF5197C273FB7EC7C75FCF8), 4);
      ptr = ref span;
      readOnlySpan.CopyTo(ptr.Slice(36, ptr.Length - 36));
      ptr = ref span;
      BitConverter.TryWriteBytes(ptr.Slice(40, ptr.Length - 40), (uint)pcm16.Length);
      ptr = ref span;
      pcm16.CopyTo(ptr.Slice(44, ptr.Length - 44));
      return array;
    }

    // Token: 0x06000701 RID: 1793 RVA: 0x00360C18 File Offset: 0x00360C18
    public unsafe static float PowerLevel(ReadOnlySpan<byte> pcm16)
    {
      ReadOnlySpan<short> readOnlySpan = MemoryMarshal.Cast<byte, short>(pcm16);
      if (readOnlySpan.IsEmpty)
      {
        return 0f;
      }
      double num = 0.0;
      ReadOnlySpan<short> readOnlySpan2 = readOnlySpan;
      for (int i = 0; i < readOnlySpan2.Length; i++)
      {
        double num2 = (double)(*readOnlySpan2[i]) / 32768.0;
        num += num2 * num2;
      }
      return (float)Math.Clamp(Math.Sqrt(num / (double)readOnlySpan.Length) * 10.2, 0.0, 1.0);
    }
  }
}
