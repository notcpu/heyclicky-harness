using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006B RID: 107
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2 : Regex
  {
    // Token: 0x060002A5 RID: 677 RVA: 0x0051B040 File Offset: 0x0051B040
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2()
    {
      this.pattern = "^\\s*([-*_])(\\s*\\1){2,}\\s*$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000063 RID: 99
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2();

    // Token: 0x02000377 RID: 887
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026AD RID: 9901 RVA: 0x00640ACC File Offset: 0x00640ACC
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__RuleRegex_2.RunnerFactory.Runner();
      }

      // Token: 0x02000AAB RID: 2731
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06004192 RID: 16786 RVA: 0x006C81D0 File Offset: 0x006C81D0
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06004193 RID: 16787 RVA: 0x006C81F4 File Offset: 0x006C81F4
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos < inputSpan.Length && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06004194 RID: 16788 RVA: 0x006C8228 File Offset: 0x006C8228
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          int num6 = 0;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num7 = 0;
          while (num7 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num7])))
          {
            num7++;
          }
          readOnlySpan = readOnlySpan.Slice(num7);
          num += num7;
          int num8 = num;
          char c;
          if (readOnlySpan.IsEmpty || (((c = (char)(*readOnlySpan[0])) != '*') & (c != '-') & (c != '_')))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          base.Capture(1, num8, num);
          num5 = num;
          int num9 = 0;
          for (;;)
          {
            <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num6, base.Crawlpos(), num5, num);
            num5 = num;
            num9++;
            int num10 = num;
            num3 = num;
            int num11 = 0;
            while (num11 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num11])))
            {
              num11++;
            }
            readOnlySpan = readOnlySpan.Slice(num11);
            num += num11;
            num4 = num;
            for (;;)
            {
              <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num6, num3, num4, base.Crawlpos());
              if (base.IsMatched(1))
              {
                int num12 = base.MatchLength(1);
                if (readOnlySpan.Length >= num12 && inputSpan.Slice(base.MatchIndex(1), num12).SequenceEqual<char>(readOnlySpan.Slice(0, num12)))
                {
                  num += num12;
                  readOnlySpan = inputSpan.Slice(num);
                  base.Capture(2, num10, num);
                  <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPush(ref this.runstack, ref num6, num10);
                  if (num != num5)
                  {
                    break;
                  }
                  if (num9 < 2)
                  {
                    break;
                  }
                  goto IL_0297;
                }
              }
              IL_012B:
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num6]);
              <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPop(this.runstack, ref num6, out num4, out num3);
              if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num3 < num4)
              {
                num4 = (num = num4 - 1);
                readOnlySpan = inputSpan.Slice(num);
                continue;
              }
              if (--num9 < 0)
              {
                goto Block_12;
              }
              <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.StackPop(this.runstack, ref num6, out num, out num5);
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(this.runstack[--num6]);
              readOnlySpan = inputSpan.Slice(num);
              if (num9 == 0)
              {
                goto Block_13;
              }
              if (num9 >= 2)
              {
                goto IL_0297;
              }
              IL_01FD:
              num10 = this.runstack[--num6];
              goto IL_012B;
              IL_0297:
              int num13 = num;
              int num14 = 0;
              while (num14 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num14])))
              {
                num14++;
              }
              readOnlySpan = readOnlySpan.Slice(num14);
              num += num14;
              int num15 = num;
              for (;;)
              {
                int num16 = base.Crawlpos();
                if (num >= inputSpan.Length - 1 && (num >= inputSpan.Length || *inputSpan[num] == 10))
                {
                  goto IL_032E;
                }
                this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num16);
                if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
                {
                  base.CheckTimeout();
                }
                if (num13 >= num15)
                {
                  break;
                }
                num15 = (num = num15 - 1);
                readOnlySpan = inputSpan.Slice(num);
              }
              if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num9 == 0)
              {
                goto Block_16;
              }
              goto IL_01FD;
            }
          }
          Block_12:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_13:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          Block_16:
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_032E:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06004196 RID: 16790 RVA: 0x006C857C File Offset: 0x006C857C
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
