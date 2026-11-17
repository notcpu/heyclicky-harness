using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
  // Token: 0x0200002E RID: 46
  [GeneratedCode("System.Text.RegularExpressions.Generator", "9.0.15.2218")]
  [SkipLocalsInit]
  internal sealed class <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44 : Regex
  {
    // Token: 0x06000059 RID: 89 RVA: 0x0033232C File Offset: 0x0033232C
    private <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44()
    {
      this.pattern = "\\d\\s*-\\s*\\d";
      this.roptions = RegexOptions.None;
      Regex.ValidateMatchTimeout(<RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout);
      this.internalMatchTimeout = <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__Utilities.s_defaultTimeout;
      this.factory = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44.RunnerFactory();
      this.capsize = 1;
    }

    // Token: 0x0400002D RID: 45
    [Nullable(1)]
    internal static readonly <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44 Instance = new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44();

    // Token: 0x02000261 RID: 609
    private sealed class RunnerFactory : RegexRunnerFactory
    {
      // Token: 0x0600214C RID: 8524 RVA: 0x003F6F60 File Offset: 0x003F6F60
      [NullableContext(1)]
      protected override RegexRunner CreateInstance()
      {
        return new <RegexGenerator_g>F2A86C3D8F8653D6C99BE1B8441A5DADEC6C855822CB294D49492A00C67E55137__DigitRange_44.RunnerFactory.Runner();
      }

      // Token: 0x020006CB RID: 1739
      private sealed class Runner : RegexRunner
      {
        // Token: 0x06003AD7 RID: 15063 RVA: 0x00460F70 File Offset: 0x00460F70
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

        // Token: 0x06003AD8 RID: 15064 RVA: 0x00460FB0 File Offset: 0x00460FB0
        private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
        {
          int runtextpos = this.runtextpos;
          if (runtextpos <= inputSpan.Length - 3)
          {
            int num = inputSpan.Slice(runtextpos).IndexOfAnyDigit();
            if (num >= 0)
            {
              this.runtextpos = runtextpos + num;
              return true;
            }
          }
          this.runtextpos = inputSpan.Length;
          return false;
        }

        // Token: 0x06003AD9 RID: 15065 RVA: 0x00460FFC File Offset: 0x00460FFC
        private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
        {
          int num = this.runtextpos;
          int num2 = num;
          ReadOnlySpan<char> readOnlySpan = inputSpan.Slice(num);
          if (readOnlySpan.IsEmpty || !char.IsDigit((char)(*readOnlySpan[0])))
          {
            return false;
          }
          int num3 = 1;
          while (num3 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num3])))
          {
            num3++;
          }
          readOnlySpan = readOnlySpan.Slice(num3);
          num += num3;
          if (readOnlySpan.IsEmpty || *readOnlySpan[0] != 45)
          {
            return false;
          }
          int num4 = 1;
          while (num4 < readOnlySpan.Length && char.IsWhiteSpace((char)(*readOnlySpan[num4])))
          {
            num4++;
          }
          readOnlySpan = readOnlySpan.Slice(num4);
          num += num4;
          if (readOnlySpan.IsEmpty || !char.IsDigit((char)(*readOnlySpan[0])))
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
