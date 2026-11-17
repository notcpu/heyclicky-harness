using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200006C RID: 108
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3 : Regex
  {
    // Token: 0x060002A7 RID: 679 RVA: 0x0051B098 File Offset: 0x0051B098
    private <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3()
    {
      this.pattern = "^\\s*[-*]\\s+";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000064 RID: 100
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3 Instance = new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3();

    // Token: 0x02000378 RID: 888
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x060026AF RID: 9903 RVA: 0x00640ADC File Offset: 0x00640ADC
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F36FB2D25FB3E06331DF866801390D20E97C6BDCC4C519A6278D40F829CA940A3__BulletRegex_3.RunnerFactory.Runner();
      }

      // Token: 0x02000AAC RID: 2732
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06004197 RID: 16791 RVA: 0x006C8590 File Offset: 0x006C8590
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          if (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan))
          {
            this.runtextpos = inputSpan.Length;
          }
        }

        // Token: 0x06004198 RID: 16792 RVA: 0x006C85B4 File Offset: 0x006C85B4
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

        // Token: 0x06004199 RID: 16793 RVA: 0x006C85E8 File Offset: 0x006C85E8
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
          char c;
          if (readOnlySpan.IsEmpty || (((c = (char)(*readOnlySpan[0])) != '*') & (c != '-')))
          {
            return false;
          }
          num++;
          readOnlySpan = inputSpan.Slice(num);
          int num4 = 0;
          while (num4 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num4])))
          {
            num4++;
          }
          if (num4 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
