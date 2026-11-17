using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000071 RID: 113
  [NullableContext(1)]
  [Nullable(0)]
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  internal static class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities
  {
    // Token: 0x060002B1 RID: 689 RVA: 0x0051B300 File Offset: 0x0051B300
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPop(int[] stack, ref int pos, out int arg0, out int arg1)
    {
      int num = pos - 1;
      pos = num;
      arg0 = stack[num];
      num = pos - 1;
      pos = num;
      arg1 = stack[num];
    }

    // Token: 0x060002B2 RID: 690 RVA: 0x0051B328 File Offset: 0x0051B328
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0)
    {
      int[] array = stack;
      int num = pos;
      if (num < array.Length)
      {
        array[num] = arg0;
        pos++;
        return;
      }
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.<StackPush>g__WithResize|3_0(ref stack, ref pos, arg0);
    }

    // Token: 0x060002B3 RID: 691 RVA: 0x0051B354 File Offset: 0x0051B354
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0, int arg1)
    {
      int[] array = stack;
      int num = pos;
      if (num + 1 < array.Length)
      {
        array[num] = arg0;
        array[num + 1] = arg1;
        pos += 2;
        return;
      }
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.<StackPush>g__WithResize|4_0(ref stack, ref pos, arg0, arg1);
    }

    // Token: 0x060002B4 RID: 692 RVA: 0x0051B38C File Offset: 0x0051B38C
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void StackPush(ref int[] stack, ref int pos, int arg0, int arg1, int arg2)
    {
      int[] array = stack;
      int num = pos;
      if (num + 2 < array.Length)
      {
        array[num] = arg0;
        array[num + 1] = arg1;
        array[num + 2] = arg2;
        pos += 3;
        return;
      }
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.<StackPush>g__WithResize|5_0(ref stack, ref pos, arg0, arg1, arg2);
    }

    // Token: 0x060002B5 RID: 693 RVA: 0x0051B3CC File Offset: 0x0051B3CC
    // Note: this type is marked as 'beforefieldinit'.
    static <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities()
    {
      object data = AppContext.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
      TimeSpan timeSpan2;
      if (data is TimeSpan)
      {
        TimeSpan timeSpan = (TimeSpan)data;
        timeSpan2 = timeSpan;
      }
      else
      {
        timeSpan2 = Regex.InfiniteMatchTimeout;
      }
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout = timeSpan2;
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout != Regex.InfiniteMatchTimeout;
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_ascii_400000000008801000000 = SearchValues.Create("*[_`");
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_ascii_FF0300000000FEFFFF07 = SearchValues.Create("0123456789abcdefghijklmnopqrstuvwxyz");
      string text = "![";
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59 = SearchValues.Create(new ReadOnlySpan<string>(ref text), StringComparison.Ordinal);
    }

    // Token: 0x060002B6 RID: 694 RVA: 0x0051B458 File Offset: 0x0051B458
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|3_0(ref int[] stack, ref int pos, int arg0)
    {
      Array.Resize<int>(ref stack, pos * 2);
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref stack, ref pos, arg0);
    }

    // Token: 0x060002B7 RID: 695 RVA: 0x0051B46C File Offset: 0x0051B46C
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|4_0(ref int[] stack, ref int pos, int arg0, int arg1)
    {
      Array.Resize<int>(ref stack, (pos + 1) * 2);
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref stack, ref pos, arg0, arg1);
    }

    // Token: 0x060002B8 RID: 696 RVA: 0x0051B484 File Offset: 0x0051B484
    [CompilerGenerated]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void <StackPush>g__WithResize|5_0(ref int[] stack, ref int pos, int arg0, int arg1, int arg2)
    {
      Array.Resize<int>(ref stack, (pos + 2) * 2);
      <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref stack, ref pos, arg0, arg1, arg2);
    }

    // Token: 0x04000069 RID: 105
    internal static readonly TimeSpan s_defaultTimeout;

    // Token: 0x0400006A RID: 106
    internal static readonly bool s_hasTimeout;

    // Token: 0x0400006B RID: 107
    internal static readonly SearchValues<char> s_ascii_400000000008801000000;

    // Token: 0x0400006C RID: 108
    internal static readonly SearchValues<char> s_ascii_FF0300000000FEFFFF07;

    // Token: 0x0400006D RID: 109
    internal static readonly SearchValues<string> s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59;
  }
}
