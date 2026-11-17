using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200000C RID: 12
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10 : Regex
  {
    // Token: 0x06000015 RID: 21 RVA: 0x00331764 File Offset: 0x00331764
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10()
    {
      this.pattern = "<[^>]+>";
      this.roptions = RegexOptions.Singleline;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400000B RID: 11
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10();

    // Token: 0x0200023F RID: 575
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002108 RID: 8456 RVA: 0x003F6D40 File Offset: 0x003F6D40
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__AnyTag_10.RunnerFactory.Runner();
      }

      // Token: 0x020006A9 RID: 1705
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003A36 RID: 14902 RVA: 0x0045B5B4 File Offset: 0x0045B5B4
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

        // Token: 0x06003A37 RID: 14903 RVA: 0x0045B5F4 File Offset: 0x0045B5F4
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 3)
          {
            int num = inputSpan.Slice(runtextpos).IndexOf('<');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003A38 RID: 14904 RVA: 0x0045B644 File Offset: 0x0045B644
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 60)
          {
            return false;
          }
          int num3 = readOnlySpan.Slice(1).IndexOf('>');
          if (num3 < 0)
          {
            num3 = readOnlySpan.Length - 1;
          }
          if (num3 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (readOnlySpan.Length < 2 || *readOnlySpan[1] != 62)
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
