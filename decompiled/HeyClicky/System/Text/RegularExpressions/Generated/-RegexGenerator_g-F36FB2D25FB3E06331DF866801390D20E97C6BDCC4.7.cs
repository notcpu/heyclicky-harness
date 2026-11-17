using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006F RID: 111
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6 : Regex
  {
    // Token: 0x060002AD RID: 685 RVA: 0x0051B1A0 File Offset: 0x0051B1A0
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6()
    {
      this.pattern = "^\\|?\\s*:?-{2,}:?\\s*(\\|\\s*:?-{2,}:?\\s*)*\\|?$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6.RunnerFactory();
      this.capsize = 2;
    }

    // Token: 0x04000067 RID: 103
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6();

    // Token: 0x0200037B RID: 891
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026B5 RID: 9909 RVA: 0x00640B0C File Offset: 0x00640B0C
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__TableRuleRegex_6.RunnerFactory.Runner();
      }

      // Token: 0x02000AAF RID: 2735
      private sealed class Runner : RegexRunner
      {
        // Token: 0x060041A4 RID: 16804 RVA: 0x006C8A00 File Offset: 0x006C8A00
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x060041A5 RID: 16805 RVA: 0x006C8A24 File Offset: 0x006C8A24
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 2 && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x060041A6 RID: 16806 RVA: 0x006C8A58 File Offset: 0x006C8A58
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          int num6 = 0;
          int num7 = 0;
          int num8 = 0;
          int num9 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 124)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          int num10 = 0;
          while (num10 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num10])))
          {
            num10++;
          }
          readOnlySpan = readOnlySpan.Slice(num10);
          num += num10;
          if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 58)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          int num11 = num;
          int num12 = readOnlySpan.IndexOfAnyExcept('-');
          if (num12 < 0)
          {
            num12 = readOnlySpan.Length;
          }
          if (num12 < 2)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num12);
          num += num12;
          int num13 = num;
          num11 += 2;
          for (;;)
          {
            int num14 = base.Crawlpos();
            int num15 = num;
            if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 58)
            {
              readOnlySpan = readOnlySpan.Slice(1);
              num++;
            }
            int num16 = num;
            for (;;)
            {
              int num17 = base.Crawlpos();
              int num18 = num;
              int num19 = 0;
              while (num19 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num19])))
              {
                num19++;
              }
              readOnlySpan = readOnlySpan.Slice(num19);
              num += num19;
              int num20 = num;
              for (;;)
              {
                int num21 = base.Crawlpos();
                int num22 = 0;
                for (;;)
                {
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num9, base.Crawlpos(), num);
                  num22++;
                  int num23 = num;
                  if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 124)
                  {
                    int num24 = 1;
                    while (num24 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num24])))
                    {
                      num24++;
                    }
                    readOnlySpan = readOnlySpan.Slice(num24);
                    num += num24;
                    if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 58)
                    {
                      readOnlySpan = readOnlySpan.Slice(1);
                      num++;
                    }
                    num3 = num;
                    int num25 = readOnlySpan.IndexOfAnyExcept('-');
                    if (num25 < 0)
                    {
                      num25 = readOnlySpan.Length;
                    }
                    if (num25 >= 2)
                    {
                      readOnlySpan = readOnlySpan.Slice(num25);
                      num += num25;
                      num4 = num;
                      num3 += 2;
                      goto IL_034C;
                    }
                  }
                  while (--num22 >= 0)
                  {
                    num = this.runstack[--num9];
                    this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num9]);
                    readOnlySpan = inputSpan.Slice(num);
                    if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 124)
                    {
                      readOnlySpan = readOnlySpan.Slice(1);
                      num++;
                    }
                    if (num >= inputSpan.Length - 1 && (num >= inputSpan.Length || *inputSpan[num] == 10))
                    {
                      goto IL_0562;
                    }
                    if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                    {
                      base.CheckTimeout();
                    }
                    if (num22 == 0)
                    {
                      break;
                    }
                    num23 = this.runstack[--num9];
                    this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num9]);
                    <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPop(this.runstack, ref num9, out num8, out num7);
                    if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                    {
                      base.CheckTimeout();
                    }
                    if (num7 < num8)
                    {
                      num8 = (num = num8 - 1);
                      readOnlySpan = inputSpan.Slice(num);
                      goto IL_047F;
                    }
                    this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num9]);
                    <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPop(this.runstack, ref num9, out num6, out num5);
                    if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                    {
                      base.CheckTimeout();
                    }
                    if (num5 < num6)
                    {
                      num6 = (num = num6 - 1);
                      readOnlySpan = inputSpan.Slice(num);
                      goto IL_03DC;
                    }
                    this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num9]);
                    <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPop(this.runstack, ref num9, out num4, out num3);
                    if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                    {
                      base.CheckTimeout();
                    }
                    if (num3 < num4)
                    {
                      num4 = (num = num4 - 1);
                      readOnlySpan = inputSpan.Slice(num);
                      goto IL_034C;
                    }
                  }
                  break;
                  IL_047F:
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num9, num7, num8, base.Crawlpos());
                  base.Capture(1, num23, num);
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num9, num23);
                  continue;
                  IL_03DC:
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num9, num5, num6, base.Crawlpos());
                  num7 = num;
                  int num26 = 0;
                  while (num26 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num26])))
                  {
                    num26++;
                  }
                  readOnlySpan = readOnlySpan.Slice(num26);
                  num += num26;
                  num8 = num;
                  goto IL_047F;
                  IL_034C:
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num9, num3, num4, base.Crawlpos());
                  num5 = num;
                  if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 58)
                  {
                    readOnlySpan = readOnlySpan.Slice(1);
                    num++;
                  }
                  num6 = num;
                  goto IL_03DC;
                }
                IL_01F1:
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num21);
                if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                {
                  base.CheckTimeout();
                }
                if (num18 < num20)
                {
                  num20 = (num = num20 - 1);
                  readOnlySpan = inputSpan.Slice(num);
                  continue;
                }
                break;
                goto IL_01F1;
              }
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num17);
              if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num15 >= num16)
              {
                break;
              }
              num16 = (num = num16 - 1);
              readOnlySpan = inputSpan.Slice(num);
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num14);
            if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num11 >= num13)
            {
              break;
            }
            num13 = (num = num13 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_0562:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x060041A8 RID: 16808 RVA: 0x006C8FE0 File Offset: 0x006C8FE0
        [CompilerGenerated]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void <TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(int capturePosition)
        {
          while (base.Crawlpos() > capturePosition)
          {
            base.Uncapture();
          }
        }
      }
    }
  }
}
