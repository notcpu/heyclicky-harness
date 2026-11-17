using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006E RID: 110
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5 : Regex
  {
    // Token: 0x060002AB RID: 683 RVA: 0x0051B148 File Offset: 0x0051B148
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5()
    {
      this.pattern = "!\\[([^\\]]*)\\]\\(([^)]+)\\)";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5.RunnerFactory();
      this.capsize = 3;
    }

    // Token: 0x04000066 RID: 102
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5();

    // Token: 0x0200037A RID: 890
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026B3 RID: 9907 RVA: 0x00640AFC File Offset: 0x00640AFC
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__ImageRegex_5.RunnerFactory.Runner();
      }

      // Token: 0x02000AAE RID: 2734
      private sealed class Runner : RegexRunner
      {
        // Token: 0x0600419F RID: 16799 RVA: 0x006C8830 File Offset: 0x006C8830
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x060041A0 RID: 16800 RVA: 0x006C8870 File Offset: 0x006C8870
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 6)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_indexOfString_A35D34A63ABD1CA9D7860076AA22A8E02AF70FE7A4FEF864363EC52527771C59);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x060041A1 RID: 16801 RVA: 0x006C88C0 File Offset: 0x006C88C0
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("!["))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num3 = num;
          int num4 = readOnlySpan.IndexOf(']');
          if (num4 < 0)
          {
            num4 = readOnlySpan.Length;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          base.Capture(1, num3, num);
          if (!readOnlySpan.StartsWith<char>("]("))
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num += 2;
          readOnlySpan = inputSpan.Slice(num);
          int num5 = num;
          int num6 = readOnlySpan.IndexOf(')');
          if (num6 < 0)
          {
            num6 = readOnlySpan.Length;
          }
          if (num6 == 0)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num6);
          num += num6;
          base.Capture(2, num5, num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 41)
          {
            this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }

        // Token: 0x060041A3 RID: 16803 RVA: 0x006C89EC File Offset: 0x006C89EC
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
