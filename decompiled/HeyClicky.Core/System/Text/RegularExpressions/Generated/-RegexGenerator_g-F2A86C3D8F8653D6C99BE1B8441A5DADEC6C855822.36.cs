using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x02000025 RID: 37
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35 : Regex
  {
    // Token: 0x06000047 RID: 71 RVA: 0x00332014 File Offset: 0x00332014
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35()
    {
      this.pattern = "\\.?0+E";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x04000024 RID: 36
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35();

    // Token: 0x02000258 RID: 600
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600213A RID: 8506 RVA: 0x003F6ED0 File Offset: 0x003F6ED0
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__TrailingZerosBeforeExponent_35.RunnerFactory.Runner();
      }

      // Token: 0x020006C2 RID: 1730
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AAB RID: 15019 RVA: 0x0045E91C File Offset: 0x0045E91C
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

        // Token: 0x06003AAC RID: 15020 RVA: 0x0045E95C File Offset: 0x0045E95C
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 2)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAny('.', '0');
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AAD RID: 15021 RVA: 0x0045E9AC File Offset: 0x0045E9AC
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (!readOnlySpan.IsEmpty && *readOnlySpan[0] == 46)
          {
            readOnlySpan = readOnlySpan.Slice(1);
            num++;
          }
          int num3 = readOnlySpan.IndexOfAnyExcept('0');
          if (num3 < 0)
          {
            num3 = readOnlySpan.Length;
          }
          if (num3 == 0)
          {
            return false;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 69)
          {
            return false;
          }
          num++;
          this.runtextpos = num;
          base.Capture(0, num2, num);
          return true;
        }
      }
    }
  }
}
