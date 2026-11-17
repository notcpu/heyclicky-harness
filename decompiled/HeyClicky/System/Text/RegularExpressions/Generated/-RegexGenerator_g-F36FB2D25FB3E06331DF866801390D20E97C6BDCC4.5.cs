using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006D RID: 109
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4 : Regex
  {
    // Token: 0x060002A9 RID: 681 RVA: 0x0051B0F0 File Offset: 0x0051B0F0
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4()
    {
      this.pattern = "^\\s*\\d+\\.\\s+";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000065 RID: 101
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4();

    // Token: 0x02000379 RID: 889
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026B1 RID: 9905 RVA: 0x00640AEC File Offset: 0x00640AEC
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__NumberRegex_4.RunnerFactory.Runner();
      }

      // Token: 0x02000AAD RID: 2733
      private sealed class Runner : RegexRunner
      {
        // Token: 0x0600419B RID: 16795 RVA: 0x006C86CC File Offset: 0x006C86CC
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x0600419C RID: 16796 RVA: 0x006C86F0 File Offset: 0x006C86F0
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 3 && runtextpos == 0)
          {
            return true;
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x0600419D RID: 16797 RVA: 0x006C8724 File Offset: 0x006C8724
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (num != 0)
          {
            return false;
          }
          int num3 = 0;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          int num4 = 0;
          while (num4 < readOnlySpan.Length && char.IsDigit((char)(*readOnlySpan[num4])))
          {
            num4++;
          }
          if (num4 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 46)
          {
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num5 = 0;
          while (num5 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num5])))
          {
            num5++;
          }
          if (num5 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num5);
          num += num5;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
