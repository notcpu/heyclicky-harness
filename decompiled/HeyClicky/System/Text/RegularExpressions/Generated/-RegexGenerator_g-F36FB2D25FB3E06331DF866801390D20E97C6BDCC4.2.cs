using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006A RID: 106
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1 : Regex
  {
    // Token: 0x060002A3 RID: 675 RVA: 0x0051AFE8 File Offset: 0x0051AFE8
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1()
    {
      this.pattern = "^(#{1,6})\\s+(.*)$";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000062 RID: 98
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1();

    // Token: 0x02000376 RID: 886
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026AB RID: 9899 RVA: 0x00640ABC File Offset: 0x00640ABC
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__HeadingRegex_1.RunnerFactory.Runner();
      }

      // Token: 0x02000AAA RID: 2730
      private sealed class Runner : RegexRunner
      {
        // Token: 0x0600418D RID: 16781 RVA: 0x006C7F90 File Offset: 0x006C7F90
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x0600418E RID: 16782 RVA: 0x006C7FB4 File Offset: 0x006C7FB4
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

        // Token: 0x0600418F RID: 16783 RVA: 0x006C7FE8 File Offset: 0x006C7FE8
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          int num3 = num;
          int num4 = 0;
          while (num4 < 6 && num4 < readOnlySpan.Length && *readOnlySpan[num4] == 35)
          {
            num4++;
          }
          if (num4 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          int num5 = num;
          int num6 = 0;
          while (num6 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num6])))
          {
            num6++;
          }
          if (num6 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          int num7 = num;
          num5++;
          for (;;)
          {
            int num8 = base.Crawlpos();
            int num9 = num;
            int num10 = num;
            int num11 = readOnlySpan.IndexOf('\n');
            if (num11 < 0)
            {
              num11 = readOnlySpan.Length;
            }
            readOnlySpan = readOnlySpan.Slice(num11);
            num += num11;
            int num12 = num;
            for (;;)
            {
              int num13 = base.Crawlpos();
              base.Capture(2, num9, num);
              if (num >= inputSpan.Length - 1 && (num >= inputSpan.Length || *inputSpan[num] == 10))
              {
                goto IL_01AD;
              }
              this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num13);
              if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
              {
                base.CheckTimeout();
              }
              if (num10 >= num12)
              {
                break;
              }
              num12 = (num = num12 - 1);
              readOnlySpan = inputSpan.Slice(num);
            }
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(num8);
            if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
            if (num5 >= num7)
            {
              break;
            }
            num7 = (num = num7 - 1);
            readOnlySpan = inputSpan.Slice(num);
          }
          this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
          return false;
          IL_01AD:
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x06004191 RID: 16785 RVA: 0x006C81BC File Offset: 0x006C81BC
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
