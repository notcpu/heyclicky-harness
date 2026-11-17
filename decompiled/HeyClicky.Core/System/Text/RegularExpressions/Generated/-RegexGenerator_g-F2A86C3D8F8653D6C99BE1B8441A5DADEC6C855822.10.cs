using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200000B RID: 11
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9 : Regex
  {
    // Token: 0x06000013 RID: 19 RVA: 0x0033170C File Offset: 0x0033170C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9()
    {
      this.pattern = "<text:tab\\s*/>";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400000A RID: 10
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9();

    // Token: 0x0200023E RID: 574
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002106 RID: 8454 RVA: 0x003F6D30 File Offset: 0x003F6D30
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__OdtTab_9.RunnerFactory.Runner();
      }

      // Token: 0x020006A8 RID: 1704
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A32 RID: 14898 RVA: 0x0045B48C File Offset: 0x0045B48C
        protected override void Scan(ReadOnlySpan<char> inputSpan)
        {
          while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
          {
            this.runtextpos++;
            if (<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_hasTimeout)
            {
              base.CheckTimeout();
            }
          }
        }

        // Token: 0x06003A33 RID: 14899 RVA: 0x0045B4CC File Offset: 0x0045B4CC
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 11)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_indexOfString_B6E25B48D6C5403C779E1AB3ECB2F42DD6921933466E65DB70DC7CFC9AC507AD);
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A34 RID: 14900 RVA: 0x0045B520 File Offset: 0x0045B520
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.StartsWith<char>("<text:tab"))
          {
            return false;
          }
          int num3 = 9;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (!readOnlySpan.StartsWith<char>("/>"))
          {
            return false;
          }
          num += 2;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
