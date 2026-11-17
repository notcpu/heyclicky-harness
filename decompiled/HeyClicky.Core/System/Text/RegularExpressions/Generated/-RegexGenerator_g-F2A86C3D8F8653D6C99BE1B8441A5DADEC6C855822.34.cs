using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000023 RID: 35
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33 : Regex
  {
    // Token: 0x06000043 RID: 67 RVA: 0x00331F64 File Offset: 0x00331F64
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33()
    {
      this.pattern = "[ \\t]{2,}";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000022 RID: 34
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33();

    // Token: 0x02000256 RID: 598
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x06002136 RID: 8502 RVA: 0x003F6EB0 File Offset: 0x003F6EB0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__MultipleSpaces_33.RunnerFactory.Runner();
      }

      // Token: 0x020006C0 RID: 1728
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AA3 RID: 15011 RVA: 0x0045E6F0 File Offset: 0x0045E6F0
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

        // Token: 0x06003AA4 RID: 15012 RVA: 0x0045E730 File Offset: 0x0045E730
        private unsafe bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 2)
          {
            ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(runtextpos);
            for (int i = 0; i < readOnlySpan.Length - 1; i++)
            {
              int num = readOnlySpan.Slice(i).IndexOfAny('\t', ' ');
              if (num < 0)
              {
                break;
              }
              i += num;
              if (i + 1 >= readOnlySpan.Length)
              {
                break;
              }
              char c;
              if (((c = (char)(*readOnlySpan[i + 1])) == '\t') | (c == ' '))
              {
                this.runtextpos = runtextpos + i;
                return true;
              }
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AA5 RID: 15013 RVA: 0x0045E7C8 File Offset: 0x0045E7C8
        private bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          int num3 = readOnlySpan.IndexOfAnyExcept('\t', ' ');
          if (num3 < 0)
          {
            num3 = readOnlySpan.Length;
          }
          if (num3 < 2)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
